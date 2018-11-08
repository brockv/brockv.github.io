using Homework7.DAL;
using Homework7.Models;
using System;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace Homework7.Controllers
{
    public class MainController : Controller
    {

        /// <summary>
        /// Default method. Only called when the application first starts, or
        /// if the page is refreshed.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}