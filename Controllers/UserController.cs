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
    public class UserController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /User/

        public ActionResult Index()
        {
            var model = (from u in db.Users
                         orderby u.FirstName
                         select new UsersData
                         {
                            UserData = u
                            
                         }).ToList();

            foreach (var item in model)
            {
                var usertypes = (from u in db.UsersTypes 
                                 join g in db.GroupClassifications on u.GroupClassificationID equals g.GroupClassificationID
                                 where u.UserID == item.UserData.UserID select new { g.GroupClassificationItem }).ToList();
                foreach (var typeitem in usertypes)
                {
                    item.UserTypes += typeitem.GroupClassificationItem + ", ";
                }
                if (item.UserTypes != null)
                    item.UserTypes = item.UserTypes.Substring(0, item.UserTypes.Length - 2);
            }

            return View(model);
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            UsersData userdata = new UsersData();

            var user = (from u in db.Users
                        where u.UserID == id
                        select new { userdata = u }).First();
            if (user == null)
            {
                return HttpNotFound();
            }

            userdata.UserData = user.userdata;
            userdata.UserTypesCheckBoxes = new List<CheckBoxes>();

            var GCmodel = db.GroupClassifications.ToList();
            foreach (var item in GCmodel)
            {
                var userstypes = (from u in db.UsersTypes
                                  where u.UserID == id && u.GroupClassificationID == item.GroupClassificationID
                                  select new { usertype = u });
                userdata.UserTypesCheckBoxes.Add(new CheckBoxes
                {
                    Text = item.GroupClassificationItem,
                    Value = Convert.ToInt32(item.GroupClassificationID),
                    Selected = (userstypes.Count() == 0 ? false : true)
                });
            }
            return View(userdata);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            UsersData model = (from u in db.Users
                              select new UsersData {}).First();

            model.UserTypesCheckBoxes = new List<CheckBoxes>();

            var GCmodel = db.GroupClassifications.ToList();
            foreach (var item in GCmodel)
            {
                model.UserTypesCheckBoxes.Add(new CheckBoxes { Text = item.GroupClassificationItem, Value = Convert.ToInt32(item.GroupClassificationID), Selected = false });
            }

            return View(model);
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(UsersData user)
        {
            if (ModelState.IsValid)
            {
                user.UserData.CreatedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                user.UserData.ModifiedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                user.UserData.CreatedOn = DateTime.Now;
                user.UserData.ModifiedOn = DateTime.Now;
                db.Users.Add(user.UserData);
                db.SaveChanges();

                foreach (var item in user.UserTypesCheckBoxes)
                {
                    if (item.Selected)
                    {
                        UsersType ut = new UsersType();
                        ut.UserID = user.UserData.UserID;
                        ut.GroupClassificationID = item.Value;
                        db.UsersTypes.Add(ut);
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UsersData userdata = new UsersData();

            var user = (from u in db.Users
                               where u.UserID == id
                               select new { userdata = u }).First();
            if (user == null)
            {
                return HttpNotFound();
            }

            userdata.UserData = user.userdata;
            userdata.UserTypesCheckBoxes = new List<CheckBoxes>();

            var GCmodel = db.GroupClassifications.ToList();
            foreach (var item in GCmodel)
            {
                var userstypes = (from u in db.UsersTypes
                                  where u.UserID == id && u.GroupClassificationID == item.GroupClassificationID
                                  select new { usertype = u });
                userdata.UserTypesCheckBoxes.Add(new CheckBoxes { Text = item.GroupClassificationItem, 
                    Value = Convert.ToInt32(item.GroupClassificationID), Selected = (userstypes.Count() == 0 ? false : true) });
            }

            return View(userdata);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(UsersData user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user.UserData).State = EntityState.Modified;
                db.SaveChanges();

                var deleteUsersTypes =
                from u in db.UsersTypes
                where u.UserID == user.UserData.UserID
                select u;

                foreach (var item in deleteUsersTypes)
                {
                    db.UsersTypes.Remove(item);
                }
                db.SaveChanges();

                foreach (var item in user.UserTypesCheckBoxes)
                {
                    if (item.Selected)
                    {
                        UsersType ut = new UsersType();
                        ut.UserID = user.UserData.UserID;
                        ut.GroupClassificationID = item.Value;
                        db.UsersTypes.Add(ut);
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UsersData userdata = new UsersData();

            var user = (from u in db.Users
                        where u.UserID == id
                        select new { userdata = u }).First();
            if (user == null)
            {
                return HttpNotFound();
            }

            userdata.UserData = user.userdata;
            userdata.UserTypesCheckBoxes = new List<CheckBoxes>();

            var GCmodel = db.GroupClassifications.ToList();
            foreach (var item in GCmodel)
            {
                var userstypes = (from u in db.UsersTypes
                                  where u.UserID == id && u.GroupClassificationID == item.GroupClassificationID
                                  select new { usertype = u });
                userdata.UserTypesCheckBoxes.Add(new CheckBoxes
                {
                    Text = item.GroupClassificationItem,
                    Value = Convert.ToInt32(item.GroupClassificationID),
                    Selected = (userstypes.Count() == 0 ? false : true)
                });
            }
            return View(userdata);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var assigned = (from v in db.ValveSection where v.BuilderID == id || v.QCID == id || v.EngineerID == id 
                           select new { found = v.ValveSectionID }).ToList();

            if (assigned.Count != 0)
            {
                ModelState.AddModelError("", "User is assigned to a Valve Section and cannot be deleted.");
                
                UsersData userdata = new UsersData();
                var user = (from u in db.Users
                            where u.UserID == id
                            select new { userdata = u }).First();
                if (user == null)
                {
                    return HttpNotFound();
                }

                userdata.UserData = user.userdata;
                userdata.UserTypesCheckBoxes = new List<CheckBoxes>();

                var GCmodel = db.GroupClassifications.ToList();
                foreach (var item in GCmodel)
                {
                    var userstypes = (from u in db.UsersTypes
                                      where u.UserID == id && u.GroupClassificationID == item.GroupClassificationID
                                      select new { usertype = u });
                    userdata.UserTypesCheckBoxes.Add(new CheckBoxes
                    {
                        Text = item.GroupClassificationItem,
                        Value = Convert.ToInt32(item.GroupClassificationID),
                        Selected = (userstypes.Count() == 0 ? false : true)
                    });
                }
                return View(userdata);
            }
            else
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();

                var deleteUsersTypes =
                    from u in db.UsersTypes
                    where u.UserID == id
                    select u;

                foreach (var item in deleteUsersTypes)
                {
                    db.UsersTypes.Remove(item);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}