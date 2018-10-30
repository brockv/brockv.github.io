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
        private readonly Person clientDB = new Person();

        public ActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Home(string searchString)
        {
            /* Get the results of the user's search */
            IEnumerable<Person> clients = from Person in db.People
                                          select Person;

            return View(clients.Where(Person => Person.FullName.ToUpper().Contains(searchString.ToUpper())));
        }

        public ActionResult ViewClientDetails()
        {
            return View();
        }
    }
}