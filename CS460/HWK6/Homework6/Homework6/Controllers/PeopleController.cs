using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homework6.Models;
using Homework6.Models.ViewModels;
using Homework6.DAL;

namespace Homework6.Controllers
{
    public class PeopleController : Controller
    {
        private readonly WideWorldImportersContext db = new WideWorldImportersContext();

        public ActionResult ViewDetails(int id)
        {
            /* Initially hide the area we use to show extra information */
            ViewBag.ShowExtraStuff = false;

            MyViewModel model = new MyViewModel
            {
                MyViewModelPerson = new Person()
            };

            /* See if there's an entry with the id that was passed in */
            model.MyViewModelPerson = db.People.Find(id);
            
            /* If no entry was found, send the user to the 'NotFound' view */
            if (model.MyViewModelPerson == null)
            {
                return View("NotFound");
            }

            /* Check if this person is the PrimaryContactPerson for a company */
            var isPrimaryContactPerson = db.Customers.Where(x => x.PrimaryContactPersonID.Equals(model.MyViewModelPerson.PersonID));

            /* If the query found anything, switch the flag for */
            if (isPrimaryContactPerson.Any())
            {
                model.MyViewModelCustomer = isPrimaryContactPerson.First();
                ViewBag.ShowExtraStuff = true;
            }

            /* Redirect to the 'ViewDetails' view */
            return View("ViewDetails", model);
        }
    }
}