using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homework6.Models;
using Homework6.Models.ViewModels;
using Homework6.DAL;
using System.Net;

namespace Homework6.Controllers
{
    public class PeopleController : Controller
    {
        private readonly WideWorldImportersContext db = new WideWorldImportersContext();

        public ActionResult ViewDetails(int id)
        {
            /* Initially hide the area we use to show extra information */
            ViewBag.ShowExtraStuff = false;

            MyViewModel model = new MyViewModel();

            /* See if there's an entry with the id that was passed in */
            model.MyViewModelPerson = db.People.Find(id);
            
            /* If no entry was found, send the user to the 'NotFound' view */
            if (model.MyViewModelPerson == null)
            {
                return View("NotFound");
            }

            /* Check if this person is the PrimaryContactPerson for a company */
            IQueryable<Customer> isPrimaryContactPerson = db.Customers.Where(x => x.PrimaryContactPersonID.Equals(model.MyViewModelPerson.PersonID));

            /* Check if the query found anything */
            if (isPrimaryContactPerson.Any())
            {
                /* Grab the data for this customer */
                model.MyViewModelCustomer = isPrimaryContactPerson.First();

                /* Calculate the gross sales for this customer */
                ViewBag.GrossSales = model.MyViewModelCustomer.Orders.SelectMany(x => x.Invoices)
                    .SelectMany(x => x.InvoiceLines)
                    .Sum(x => x.ExtendedPrice);

                /* Calculate the gross profit for this customer*/
                ViewBag.GrossProfits = model.MyViewModelCustomer.Orders.SelectMany(x => x.Invoices)
                    .SelectMany(x => x.InvoiceLines)
                    .Sum(x => x.LineProfit);

                /* Grab the top 10 purchases by this customer, in descending order */
                model.MyViewModelInvoice = model.MyViewModelCustomer.Orders.SelectMany(x => x.Invoices)
                    .SelectMany(x => x.InvoiceLines)
                    .OrderByDescending(x => x.LineProfit)
                    .Take(10)
                    .ToList();

                /* Switch the flag that controls the visibility of the extra information */
                ViewBag.ShowExtraStuff = true;
            }

            /* Redirect to the 'ViewDetails' view */
            return View("ViewDetails", model);
        }
    }
}