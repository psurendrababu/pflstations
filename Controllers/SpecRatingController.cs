using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PipelineFeatureList.Models;
using System.Data.SqlClient;

namespace PipelineFeatureList.Controllers
{
    public class SpecRatingController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /SpecRating/

        public ActionResult Index()
        {
            return View(db.SpecRatings.ToList());
        }

        //
        // GET: /SpecRating/Details/5

        public ActionResult Details(int id = 0)
        {
            SpecRating specrating = db.SpecRatings.Find(id);
            if (specrating == null)
            {
                return HttpNotFound();
            }
            return View(specrating);
        }

        //
        // GET: /SpecRating/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SpecRating/Create

        [HttpPost]
        public ActionResult Create(SpecRating specrating)
        {
            if (ModelState.IsValid)
            {
                db.SpecRatings.Add(specrating);
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Specification", "Create", "", specrating.SpecRatingItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }

            return View(specrating);
        }

        //
        // GET: /SpecRating/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SpecRating specrating = db.SpecRatings.Find(id);
            var specfeatures = (from vf in db.ValveSectionFeatures
                                where vf.SpecRatingID == specrating.SpecRatingID
                                select new
                                {
                                    vf
                                }).ToList();


            if (specfeatures.Count > 0)
            {
                ModelState.AddModelError("SpecRatingItem", "Warning! This Specification is assigned to Circuit feature(s).");
                ViewBag.HasError = "True";
            }
            if (specrating == null)
            {
                return HttpNotFound();
            }
            Session["CodeLookUpAduit_Oldvalue"] = specrating.SpecRatingItem;
            return View(specrating);
        }

        //
        // POST: /SpecRating/Edit/5

        [HttpPost]
        public ActionResult Edit(SpecRating specrating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specrating).State = EntityState.Modified;
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Specification", "Edit", Session["CodeLookUpAduit_Oldvalue"].ToString(), specrating.SpecRatingItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }
            return View(specrating);
        }

        //
        // GET: /SpecRating/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SpecRating specrating = db.SpecRatings.Find(id);
            var specfeatures = (from vf in db.ValveSectionFeatures
                               where vf.SpecRatingID == specrating.SpecRatingID
                                select new
                               {
                                   vf
                               }).ToList();


            if (specfeatures.Count > 0)
            {
                ModelState.AddModelError("SpecRatingItem", "This Specification is assigned to Circuit feature(s) and cannot be deleted.");
                ViewBag.HasError = "True";
            }
            if (specrating == null)
            {
                return HttpNotFound();
            }
            return View(specrating);
        }

        //
        // POST: /SpecRating/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SpecRating specrating = db.SpecRatings.Find(id);
            db.SpecRatings.Remove(specrating);
            db.SaveChanges();
            if (Insert_CodeLookUp_Audit("Specification", "Delete", specrating.SpecRatingItem, ""))
            {
                //nothing to do at this point.
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public bool Insert_CodeLookUp_Audit(string codelookup_name, string act, string oldvalue, string newvalue)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PipelineFeatureListDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("spInsert_dbo_CodeLookUpAudit", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CodeLookUp_Name", codelookup_name));
            cmd.Parameters.Add(new SqlParameter("@Action", act));
            cmd.Parameters.Add(new SqlParameter("@Old_Value", oldvalue));
            cmd.Parameters.Add(new SqlParameter("@New_Value", newvalue));
            cmd.Parameters.Add(new SqlParameter("@Modified_User", Session["UserName"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@Modified_Date", DateTime.Now));
            try
            {
                cmd.BeginExecuteNonQuery(delegate (IAsyncResult ar)
                {
                    int rowCount = cmd.EndExecuteNonQuery(ar);
                }, cmd);
                return true;
            }
            catch (SqlException s)
            {
                throw s;

            }
            catch (Exception e)
            {
                throw e;

            }
        }
    }
}