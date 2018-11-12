using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework8.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Items()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Bids()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}