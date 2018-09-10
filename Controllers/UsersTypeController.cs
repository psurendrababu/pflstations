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
    public class UsersTypeController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /UsersType/

        public ActionResult Index()
        {
            return View(db.GroupClassifications.ToList());
        }

        //
        // GET: /UsersType/Details/5

        public ActionResult Details(int id = 0)
        {
            UsersType userstype = db.UsersTypes.Find(id);
            if (userstype == null)
            {
                return HttpNotFound();
            }
            return View(userstype);
        }

        //
        // GET: /UsersType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UsersType/Create

        [HttpPost]
        public ActionResult Create(UsersType userstype)
        {
            if (ModelState.IsValid)
            {
                db.UsersTypes.Add(userstype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userstype);
        }

        //
        // GET: /UsersType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UsersType userstype = db.UsersTypes.Find(id);
            if (userstype == null)
            {
                return HttpNotFound();
            }
            return View(userstype);
        }

        //
        // POST: /UsersType/Edit/5

        [HttpPost]
        public ActionResult Edit(UsersType userstype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userstype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userstype);
        }

        //
        // GET: /UsersType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UsersType userstype = db.UsersTypes.Find(id);
            if (userstype == null)
            {
                return HttpNotFound();
            }
            return View(userstype);
        }

        //
        // POST: /UsersType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UsersType userstype = db.UsersTypes.Find(id);
            db.UsersTypes.Remove(userstype);
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