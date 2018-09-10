using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;
using PipelineFeatureList.Models;
using System.Data.Objects.SqlClient;


namespace PipelineFeatureList.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
            //try
            //{
            //    if (Session["GroupClassificationAdmin"].ToString() == "Administrator")
            //        return RedirectToAction("Index", "Admin");
            //    else
            //        return RedirectToAction("Assigned", "ValveSection");
            //}
            //catch
            //{
            //    ViewBag.Message = "";
            //    return View();
            //}
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
