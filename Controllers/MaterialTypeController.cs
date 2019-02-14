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
    public class MaterialTypeController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /MaterialType/

        public ActionResult Index()
        {
            return View(db.MaterialTypes.ToList());
        }

        //
        // GET: /MaterialType/Details/5

        public ActionResult Details(int id = 0)
        {
            MaterialType materialtype = db.MaterialTypes.Find(id);
            if (materialtype == null)
            {
                return HttpNotFound();
            }
            return View(materialtype);
        }

        //
        // GET: /MaterialType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MaterialType/Create

        [HttpPost]
        public ActionResult Create(MaterialType materialtype)
        {
            if (ModelState.IsValid)
            {
                db.MaterialTypes.Add(materialtype);
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Material Type", "Create", "", materialtype.MaterialTypeItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }

            return View(materialtype);
        }

        //
        // GET: /MaterialType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MaterialType materialtype = db.MaterialTypes.Find(id);
            var materialfeatures = (from vf in db.ValveSectionFeatures
                                    where vf.MaterialTypeID == materialtype.MaterialTypeID
                                    select new
                                    {
                                        vf
                                    }).ToList();


            if (materialfeatures.Count > 0)
            {
                ModelState.AddModelError("MaterialTypeItem", "Warning! This Material Type is assigned to Circuit feature(s).");
                ViewBag.HasError = "True";
            }
            if (materialtype == null)
            {
                return HttpNotFound();
            }
            Session["CodeLookUpAduit_Oldvalue"] = materialtype.MaterialTypeItem;
            return View(materialtype);
        }

        //
        // POST: /MaterialType/Edit/5

        [HttpPost]
        public ActionResult Edit(MaterialType materialtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materialtype).State = EntityState.Modified;
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Material Type", "Edit", Session["CodeLookUpAduit_Oldvalue"].ToString(), materialtype.MaterialTypeItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }
            return View(materialtype);
        }

        //
        // GET: /MaterialType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MaterialType materialtype = db.MaterialTypes.Find(id);
            var materialfeatures = (from vf in db.ValveSectionFeatures
                               where vf.MaterialTypeID == materialtype.MaterialTypeID
                                    select new
                               {
                                   vf
                               }).ToList();


            if (materialfeatures.Count > 0)
            {
                ModelState.AddModelError("MaterialTypeItem", "This Material Type is assigned to Circuit feature(s) and cannot be deleted.");
                ViewBag.HasError = "True";
            }

            if (materialtype == null)
            {
                return HttpNotFound();
            }
            return View(materialtype);
        }

        //
        // POST: /MaterialType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MaterialType materialtype = db.MaterialTypes.Find(id);
            db.MaterialTypes.Remove(materialtype);
            db.SaveChanges();
            if (Insert_CodeLookUp_Audit("Material Type", "Delete", materialtype.MaterialTypeItem, ""))
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