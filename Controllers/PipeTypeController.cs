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
    public class PipeTypeController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /PipeType/

        public ActionResult Index()
        {    
            var featuretypes = (from ft in db.PipeTypes
                                 join f in db.Features on ft.FeatureID equals f.FeatureID
                                 select new
                                 {
                                     PipeType = ft,
                                     Feature = f
                                 }).ToList();

            List<PipeType> lpt = new List<PipeType>();


            foreach (var ft in featuretypes) {
                lpt.Add(ft.PipeType);
            }

            return View(lpt);

            //return View(db.PipeTypes.ToList());
        }

        //
        // GET: /PipeType/Details/5

        public ActionResult Details(int id = 0)
        {
            var featuretypes = (from ft in db.PipeTypes
                                join f in db.Features on ft.FeatureID equals f.FeatureID
                                where ft.PipeTypeID == id
                                select new
                                {
                                    PipeType = ft,
                                    Feature = f
                                }).FirstOrDefault();

            //PipeType pipetype = db.PipeTypes.Find(id);

            if (featuretypes == null)
            {
                return HttpNotFound();
            }
            ViewBag.SelectedFeatureID = new SelectList(db.Features, "FeatureID", "FeatureItem", featuretypes.Feature.FeatureID);
            PipeType pipetype = featuretypes.PipeType;
            return View(pipetype);
        }

        //
        // GET: /PipeType/Create

        public ActionResult Create()
        {
            ViewBag.FeatureID = new SelectList(db.Features, "FeatureID", "FeatureItem");
            return View();
        }

        //
        // POST: /PipeType/Create

        [HttpPost]
        public ActionResult Create(PipeType pipetype)
        {
            if (pipetype.FeatureID == 0)
            {                
                ViewBag.FeatureID = new SelectList(db.Features, "FeatureID", "FeatureItem");
                return View(pipetype);
                
            }
            if (pipetype.PipeTypeItem == null)
            {
                ModelState.AddModelError("PipeTypeItem", "Feature Type is required.");
                ViewBag.FeatureID = new SelectList(db.Features, "FeatureID", "FeatureItem");
                return View(pipetype);                
            }

            if (ModelState.IsValid)
            {
                db.PipeTypes.Add(pipetype);
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Feature Type", "Create", "",  pipetype.PipeTypeItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }
           

            return View(pipetype);
        }

        //
        // GET: /PipeType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //ViewBag.FeatureID = new SelectList(db.Features, "FeatureID", "FeatureItem");

            var featuretypes = (from ft in db.PipeTypes
                                join f in db.Features on ft.FeatureID equals f.FeatureID
                                where ft.PipeTypeID == id
                                select new
                                {
                                    PipeType = ft,
                                    Feature = f
                                }).FirstOrDefault();

            ViewBag.SelectedFeatureID = new SelectList(db.Features, "FeatureID", "FeatureItem", featuretypes.Feature.FeatureID);

            PipeType pipetype = db.PipeTypes.Find(id);
            var pipefeatures = (from vf in db.ValveSectionFeatures
                                where vf.TypeID == pipetype.PipeTypeID
                                select new
                                {
                                    vf
                                }).ToList();


            if (pipefeatures.Count > 0)
            {
                ModelState.AddModelError("PipeTypeItem", "Warning! This Feature Type is assigned to Circuit feature(s).");
                ViewBag.HasError = "True";
            }
            if (pipetype == null)
            {
                return HttpNotFound();
            }
            Session["CodeLookUpAduit_Oldvalue"] = pipetype.PipeTypeItem;
            return View(pipetype);
        }

        //
        // POST: /PipeType/Edit/5

        [HttpPost]
        public ActionResult Edit(PipeType pipetype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pipetype).State = EntityState.Modified;
                db.SaveChanges();
                if (Insert_CodeLookUp_Audit("Feature Type", "Edit", Session["CodeLookUpAduit_Oldvalue"].ToString(),  pipetype.PipeTypeItem))
                {
                    //nothing to do at this point.
                }
                return RedirectToAction("Index");
            }
            return View(pipetype);
        }

        //
        // GET: /PipeType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var featuretypes = (from ft in db.PipeTypes
                                join f in db.Features on ft.FeatureID equals f.FeatureID
                                where ft.PipeTypeID == id
                                select new
                                {
                                    PipeType = ft,
                                    Feature = f
                                }).FirstOrDefault();

            ViewBag.SelectedFeatureID = new SelectList(db.Features, "FeatureID", "FeatureItem", featuretypes.Feature.FeatureID);

            PipeType pipetype = db.PipeTypes.Find(id);

            var pipefeatures = (from vf in db.ValveSectionFeatures
                               where vf.TypeID == pipetype.PipeTypeID
                                select new
                               {
                                   vf
                               }).ToList();


            if (pipefeatures.Count > 0)
            {
                ModelState.AddModelError("PipeTypeItem", "This Feature Type is assigned to Circuit feature(s) and cannot be deleted.");
                ViewBag.HasError = "True";
            }

            if (pipetype == null)
            {
                return HttpNotFound();
            }
            return View(pipetype);
        }

        //
        // POST: /PipeType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PipeType pipetype = db.PipeTypes.Find(id);
            db.PipeTypes.Remove(pipetype);
            db.SaveChanges();
            if (Insert_CodeLookUp_Audit("Feature Type", "Delete",  pipetype.PipeTypeItem, ""))
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