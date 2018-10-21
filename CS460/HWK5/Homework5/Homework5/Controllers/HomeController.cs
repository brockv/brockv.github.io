using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult RequestForm()
        {
            ViewBag.Message = "Tennant Request Form";

            return View();
        }

        public ActionResult RequestLog()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}