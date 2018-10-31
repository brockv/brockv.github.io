using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homework6.Models;
using Homework6.DAL;

namespace Homework6.Controllers
{
    public class PeopleController : Controller
    {
        private WideWorldImportersContext db = new WideWorldImportersContext();

        public ActionResult ViewDetails(int id)
        {
            Person person = db.People.Find(id);

            if (person == null)
            {
                return View("NotFound");
            }

            return View("ViewDetails", person);            
        }
    }
}