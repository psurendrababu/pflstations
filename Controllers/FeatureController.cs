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
    public class FeatureController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /Feature/

        public ActionResult Index()
        {
            return View(db.Features.ToList());
        }

        //
        // GET: /Feature/Details/5

        public ActionResult Details(int id = 0)
        {
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        //
        // GET: /Feature/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Feature/Create

        [HttpPost]
        public ActionResult Create(Feature feature)
        {
            if (ModelState.IsValid)
            {
                db.Features.Add(feature);
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Feature", "Create", "", feature.FeatureItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }
            
            return View(feature);
        }

        //
        // GET: /Feature/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Feature feature = db.Features.Find(id);
            var features = (from vf in db.ValveSectionFeatures
                            where vf.FeatureID == feature.FeatureID
                            select new
                            {
                                vf
                            }).ToList();

            if (features.Count > 0)
            {
                ModelState.AddModelError("FeatureItem", "Warning! This Feature is assigned to Circuit feature(s).");
                ViewBag.HasError = "True";
            }
            if (feature == null)
            {
                return HttpNotFound();
            }
            Session["CodeLookUpAduit_Oldvalue"] = feature.FeatureItem;

            return View(feature);
        }

        //
        // POST: /Feature/Edit/5

        [HttpPost]
        public ActionResult Edit(Feature feature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feature).State = EntityState.Modified;
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Feature", "Edit", Session["CodeLookUpAduit_Oldvalue"].ToString(), feature.FeatureItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }
            return View(feature);
        }

        //
        // GET: /Feature/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Feature feature = db.Features.Find(id);
            var features = (from vf in db.ValveSectionFeatures
                               where vf.FeatureID == feature.FeatureID
                        select new
                               {
                                   vf
                               }).ToList();


            if (features.Count > 0)
            {
                ModelState.AddModelError("FeatureItem", "This Feature is assigned to Circuit feature(s) and cannot be deleted.");
                ViewBag.HasError = "True";
            }
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        //
        // POST: /Feature/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Feature feature = db.Features.Find(id);
            db.Features.Remove(feature);
            db.SaveChanges();
            if (Insert_CodeLookUp_Audit("Feature", "Delete", feature.FeatureItem, ""))
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