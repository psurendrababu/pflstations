using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PipelineFeatureList.Models;

namespace PipelineFeatureList.Controllers
{
    public class GradeController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /Grade/

        public ActionResult Index()
        {
            return View(db.Grades.ToList());
        }

        //
        // GET: /Grade/Details/5

        public ActionResult Details(int id = 0)
        {
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        //
        // GET: /Grade/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Grade/Create

        [HttpPost]
        public ActionResult Create(Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Grades.Add(grade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grade);
        }

        //
        // GET: /Grade/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Grade grade = db.Grades.Find(id);
            var gradefeatures = (from vf in db.ValveSectionFeatures
                                 where vf.GradeID == grade.GradeID
                                 select new
                                 {
                                     vf
                                 }).ToList();


            if (gradefeatures.Count > 0)
            {
                ModelState.AddModelError("GradeItem", "Warning! This Grade is assigned to Circuit feature(s).");
                ViewBag.HasError = "True";               
            }
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        //
        // POST: /Grade/Edit/5

        [HttpPost]
        public ActionResult Edit(Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grade);
        }

        //
        // GET: /Grade/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Grade grade = db.Grades.Find(id);
            var gradefeatures = (from vf in db.ValveSectionFeatures
                               where vf.GradeID == grade.GradeID
                                 select new
                               {
                                   vf
                               }).ToList();


            if (gradefeatures.Count > 0)
            {
                ModelState.AddModelError("GradeItem", "This Grade is assigned to Circuit feature(s) and cannot be deleted.");
                ViewBag.HasError = "True";
            }
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        //
        // POST: /Grade/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Grade grade = db.Grades.Find(id);
            db.Grades.Remove(grade);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}