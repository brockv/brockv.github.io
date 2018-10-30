using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homework6.Models;
using Homework6.DAL;

namespace Homework6.Controllers
{
    public class HomeController : Controller
    {
        /* Initialize an instance of the database */
        private WideWorldImportersContext db = new WideWorldImportersContext();

        public ActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Home(string searchString)
        {
            /* Get the results of the user's search */
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(db.People.Where(x => x.FullName.ToUpper().Contains(searchString.ToUpper())));
            }

            return View();
        }

        public ActionResult ViewClientDetails()
        {
            return View();
        }
    }
}