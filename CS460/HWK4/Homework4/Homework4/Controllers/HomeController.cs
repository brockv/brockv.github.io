using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework4.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Home()
        {
            return View();
        }

        // GET: Converter
        public ActionResult Converter()
        {
            return View();
        }

        // GET: Color Mixer
        public ActionResult ColorMixer()
        {
            return View();
        }
    }
}