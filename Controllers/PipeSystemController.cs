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
    public class PipeSystemController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /PipeSystem/

        public ActionResult Index()
        {
            return View(db.PipeSystems.ToList());
        }

        //
        // GET: /PipeSystem/Details/5

        public ActionResult Details(int id = 0)
        {
            PipeSystem pipesystem = db.PipeSystems.Find(id);
            if (pipesystem == null)
            {
                return HttpNotFound();
            }
            return View(pipesystem);
        }

        //
        // GET: /PipeSystem/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PipeSystem/Create

        [HttpPost]
        public ActionResult Create(PipeSystem pipesystem)
        {
            if (ModelState.IsValid)
            {
                db.PipeSystems.Add(pipesystem);
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Station Location", "Create", "", pipesystem.PipeSystemItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }

            return View(pipesystem);
        }

        //
        // GET: /PipeSystem/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PipeSystem pipesystem = db.PipeSystems.Find(id);
            var psfeatures = (from vf in db.ValveSection
                              where vf.PipeSystemID == pipesystem.PipeSystemID
                              select new
                              {
                                  vf
                              }).ToList();


            if (psfeatures.Count > 0)
            {
                ModelState.AddModelError("PipeSystemItem", "Warning! This Station Location is assigned to Circuit(s).");
                ViewBag.HasError = "True";
            }
            if (pipesystem == null)
            {
                return HttpNotFound();
            }
            Session["CodeLookUpAduit_Oldvalue"] = pipesystem.PipeSystemItem;
            return View(pipesystem);
        }

        //
        // POST: /PipeSystem/Edit/5

        [HttpPost]
        public ActionResult Edit(PipeSystem pipesystem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pipesystem).State = EntityState.Modified;
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Station Location", "Edit", Session["CodeLookUpAduit_Oldvalue"].ToString(), pipesystem.PipeSystemItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }
            return View(pipesystem);
        }

        //
        // GET: /PipeSystem/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PipeSystem pipesystem = db.PipeSystems.Find(id);
            var psfeatures = (from vf in db.ValveSection
                              where vf.PipeSystemID == pipesystem.PipeSystemID
                              select new
                              {
                                  vf
                              }).ToList();


            if (psfeatures.Count > 0)
            {
                ModelState.AddModelError("PipeSystemItem", "This Station Location is assigned to Circuit(s) and cannot be deleted.");
                ViewBag.HasError = "True";
            }

            if (pipesystem == null)
            {
                return HttpNotFound();
            }
            return View(pipesystem);
        }

        //
        // POST: /PipeSystem/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PipeSystem pipesystem = db.PipeSystems.Find(id);
            db.PipeSystems.Remove(pipesystem);
            db.SaveChanges();
            if (Insert_CodeLookUp_Audit("Station Location", "Delete", pipesystem.PipeSystemItem, ""))
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