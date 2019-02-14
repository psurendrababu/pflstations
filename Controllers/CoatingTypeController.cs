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
    public class CoatingTypeController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /CoatingType/

        public ActionResult Index()
        {
            return View(db.CoatingTypes.ToList());
        }

        //
        // GET: /CoatingType/Details/5

        public ActionResult Details(int id = 0)
        {
            CoatingType coatingtype = db.CoatingTypes.Find(id);
            if (coatingtype == null)
            {
                return HttpNotFound();
            }
            return View(coatingtype);
        }

        //
        // GET: /CoatingType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CoatingType/Create

        [HttpPost]
        public ActionResult Create(CoatingType coatingtype)
        {
            if (ModelState.IsValid)
            {
                db.CoatingTypes.Add(coatingtype);
                db.SaveChanges();

                if (Insert_CodeLookUp_Audit("Coating Type", "Create", "", coatingtype.CoatingTypeItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }

            return View(coatingtype);
        }

        //
        // GET: /CoatingType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CoatingType coatingtype = db.CoatingTypes.Find(id);
            var coatingfeatiures = (from vf in db.ValveSectionFeatures
                                    where vf.CoatingTypeID == coatingtype.CoatingTypeID
                                    select new
                                    {
                                        vf
                                    }).ToList();


            if (coatingfeatiures.Count > 0)
            {
                ModelState.AddModelError("CoatingTypeItem", "Warning! This Coating Type is assigned to Circuit feature(s).");
                ViewBag.HasError = "True";                
            }
            if (coatingtype == null)
            {
                return HttpNotFound();
            }
            Session["CodeLookUpAduit_Oldvalue"] = coatingtype.CoatingTypeItem;
            return View(coatingtype);
        }

        //
        // POST: /CoatingType/Edit/5

        [HttpPost]
        public ActionResult Edit(CoatingType coatingtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coatingtype).State = EntityState.Modified;
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Coating Type", "Edit", Session["CodeLookUpAduit_Oldvalue"].ToString(), coatingtype.CoatingTypeItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }
            return View(coatingtype);
        }

        //
        // GET: /CoatingType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CoatingType coatingtype = db.CoatingTypes.Find(id);
            var coatingfeatiures = (from vf in db.ValveSectionFeatures
                               where vf.CoatingTypeID == coatingtype.CoatingTypeID
                                    select new
                               {
                                   vf
                               }).ToList();


            if (coatingfeatiures.Count > 0)
            {
                ModelState.AddModelError("CoatingTypeItem", "This Coating Type is assigned to Circuit feature(s) and cannot be deleted.");
                ViewBag.HasError = "True";               
            }
            if (coatingtype == null)
            {
                return HttpNotFound();
            }
            return View(coatingtype);
        }

        //
        // POST: /CoatingType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CoatingType coatingtype = db.CoatingTypes.Find(id);
            db.CoatingTypes.Remove(coatingtype);
            db.SaveChanges();
            if (Insert_CodeLookUp_Audit("Coating Type", "Delete", coatingtype.CoatingTypeItem, ""))
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