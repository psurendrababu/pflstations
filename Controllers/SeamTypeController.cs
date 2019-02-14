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
    public class SeamTypeController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /SeamType/

        public ActionResult Index()
        {
            return View(db.SeamTypes.ToList());
        }

        //
        // GET: /SeamType/Details/5

        public ActionResult Details(int id = 0)
        {
            SeamType seamtype = db.SeamTypes.Find(id);
            if (seamtype == null)
            {
                return HttpNotFound();
            }
            return View(seamtype);
        }

        //
        // GET: /SeamType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SeamType/Create

        [HttpPost]
        public ActionResult Create(SeamType seamtype)
        {
            if (ModelState.IsValid)
            {
                db.SeamTypes.Add(seamtype);
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Seam Weld Type", "Create", "", seamtype.SeamTypeItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }

            return View(seamtype);
        }

        //
        // GET: /SeamType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SeamType seamtype = db.SeamTypes.Find(id);
            var seamfeatures = (from vf in db.ValveSectionFeatures
                                where vf.SeamWeldTypeID == seamtype.SeamTypeID
                                select new
                                {
                                    vf
                                }).ToList();


            if (seamfeatures.Count > 0)
            {
                ModelState.AddModelError("SeamTypeItem", "Warning! This Seam Weld Type is assigned to Circuit feature(s).");
                ViewBag.HasError = "True";
            }
            if (seamtype == null)
            {
                return HttpNotFound();
            }
            Session["CodeLookUpAduit_Oldvalue"] = seamtype.SeamTypeItem;
            return View(seamtype);
        }

        //
        // POST: /SeamType/Edit/5

        [HttpPost]
        public ActionResult Edit(SeamType seamtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seamtype).State = EntityState.Modified;
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Seam Weld Type", "Edit", Session["CodeLookUpAduit_Oldvalue"].ToString(), seamtype.SeamTypeItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }
            return View(seamtype);
        }

        //
        // GET: /SeamType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SeamType seamtype = db.SeamTypes.Find(id);
            var seamfeatures = (from vf in db.ValveSectionFeatures
                               where vf.SeamWeldTypeID == seamtype.SeamTypeID
                                select new
                               {
                                   vf
                               }).ToList();


            if (seamfeatures.Count > 0)
            {
                ModelState.AddModelError("SeamTypeItem", "This Seam Weld Type is assigned to Circuit feature(s) and cannot be deleted.");
                ViewBag.HasError = "True";
            }
            if (seamtype == null)
            {
                return HttpNotFound();
            }
            return View(seamtype);
        }

        //
        // POST: /SeamType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SeamType seamtype = db.SeamTypes.Find(id);
            db.SeamTypes.Remove(seamtype);
            db.SaveChanges();
            if (Insert_CodeLookUp_Audit("Seam Weld Type", "Delete", seamtype.SeamTypeItem, ""))
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