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
    public class ANSIRatingController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /ANSIRating/

        public ActionResult Index()
        {
            return View(db.ANSIRatings.ToList());
        }

        //
        // GET: /ANSIRating/Details/5

        public ActionResult Details(int id = 0)
        {
            ANSIRating ansirating = db.ANSIRatings.Find(id);
            if (ansirating == null)
            {
                return HttpNotFound();
            }
            return View(ansirating);
        }

        //
        // GET: /ANSIRating/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ANSIRating/Create

        [HttpPost]
        public ActionResult Create(ANSIRating ansirating)
        {
            if (ModelState.IsValid)
            {
                db.ANSIRatings.Add(ansirating);
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Rating Class", "Create", "", ansirating.ANSIRatingItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }

            return View(ansirating);
        }

        //
        // GET: /ANSIRating/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ANSIRating ansirating = db.ANSIRatings.Find(id);
            var ansiratings = (from vf in db.ValveSectionFeatures
                               where vf.ANSIRatingID == ansirating.ANSIRatingID
                               select new
                               {
                                   vf
                               }).ToList();


            if (ansiratings.Count > 0)
            {
                ModelState.AddModelError("ANSIRatingItem", "Warning! This Rating Class is assigned to Circuit feature(s).");
                ViewBag.HasError = "True";
            }
            if (ansirating == null)
            {
                return HttpNotFound();
            }
            Session["CodeLookUpAduit_Oldvalue"] = ansirating.ANSIRatingItem;
            return View(ansirating);
        }

        //
        // POST: /ANSIRating/Edit/5

        [HttpPost]
        public ActionResult Edit(ANSIRating ansirating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ansirating).State = EntityState.Modified;
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Rating Class", "Edit", Session["CodeLookUpAduit_Oldvalue"].ToString(), ansirating.ANSIRatingItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }
            return View(ansirating);
        }

        //
        // GET: /ANSIRating/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ANSIRating ansirating = db.ANSIRatings.Find(id);

            var ansiratings = (from vf in db.ValveSectionFeatures
                               where vf.ANSIRatingID == ansirating.ANSIRatingID                               
                               select new
                                {
                                   vf
                                }).ToList();


            if (ansiratings.Count > 0)
            {
                ModelState.AddModelError("ANSIRatingItem", "This Rating Class is assigned to Circuit feature(s) and cannot be deleted.");
                ViewBag.HasError = "True";
            }

            if (ansirating == null)
            {
                return HttpNotFound();
            }
            return View(ansirating);
        }

        //
        // POST: /ANSIRating/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ANSIRating ansirating = db.ANSIRatings.Find(id);
            db.ANSIRatings.Remove(ansirating);
            db.SaveChanges();
            if (Insert_CodeLookUp_Audit("Rating Class", "Delete", ansirating.ANSIRatingItem, ""))
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