using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Sample aplication";

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "About Page";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "About Page";

            return View();
        }
    }
}
