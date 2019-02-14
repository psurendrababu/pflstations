using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using PipelineFeatureList.Models;

namespace PipelineFeatureList.Controllers
{
    public class ManufacturerController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /Manufacturer/

        public ActionResult Index()
        {
            return View(db.Manufacturers.OrderBy(m => m.ManufacturerItem).ToList());
        }

        //
        // GET: /Manufacturer/Details/5

        public ActionResult Details(int id = 0)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        //
        // GET: /Manufacturer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Manufacturer/Create

        [HttpPost]
        public ActionResult Create(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                manufacturer.CreatedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                manufacturer.ModifiedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                manufacturer.CreatedOn = DateTime.Now;
                manufacturer.ModifiedOn = DateTime.Now;

                db.Manufacturers.Add(manufacturer);
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Manufacturer", "Create", "", manufacturer.ManufacturerItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }

            return View(manufacturer);
        }

        //
        // GET: /Manufacturer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            var manfeatures = (from vf in db.ValveSectionFeatures
                               where vf.ManufacturerID == manufacturer.ManufacturerID
                               select new
                               {
                                   vf
                               }).ToList();


            if (manfeatures.Count > 0)
            {
                ModelState.AddModelError("ManufacturerItem", "Warning! This Manufacturer is assigned to Circuit feature(s).");
                ViewBag.HasError = "True";
            }
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            Session["CodeLookUpAduit_Oldvalue"] = manufacturer.ManufacturerItem;
            return View(manufacturer);
        }

        //
        // POST: /Manufacturer/Edit/5

        [HttpPost]
        public ActionResult Edit(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                manufacturer.ModifiedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                manufacturer.ModifiedOn = DateTime.Now;
                
                db.Entry(manufacturer).State = EntityState.Modified;
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Manufacturer", "Edit", Session["CodeLookUpAduit_Oldvalue"].ToString(), manufacturer.ManufacturerItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }

        //
        // GET: /Manufacturer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            var manfeatures = (from vf in db.ValveSectionFeatures
                               where vf.ManufacturerID == manufacturer.ManufacturerID
                               select new
                               {
                                   vf
                               }).ToList();


            if (manfeatures.Count > 0)
            {
                ModelState.AddModelError("ManufacturerItem", "This Manufacturer is assigned to Circuit feature(s) and cannot be deleted.");
                ViewBag.HasError = "True";
            }

            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        //
        // POST: /Manufacturer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);

            var assigned = (from v in db.ValveSectionFeatures
                            where v.ManufacturerID == id
                            select new { found = v.ValveSectionID }).ToList();

            if (assigned.Count != 0)
            {
                ModelState.AddModelError("", "Manufacturer is assigned to a Valve Section Feature and cannot be deleted.");
             
                if (manufacturer == null)
                {
                    return HttpNotFound();
                }
                return View(manufacturer);
            }

            db.Manufacturers.Remove(manufacturer);
            db.SaveChanges();
            if (Insert_CodeLookUp_Audit("Manufacturer", "Delete", manufacturer.ManufacturerItem, ""))
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