using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homework6.Models;
using Homework6.Models.ViewModels;
using Homework6.DAL;
using System.Net;
using System.IO;

namespace Homework6.Controllers
{
    public class PeopleController : Controller
    {
        /* Create an instance of the database for doing initial lookups */
        private readonly WideWorldImportersContext db = new WideWorldImportersContext();

        /// <summary>
        /// Builds a view to display information about a given person, using their ID.
        /// </summary>
        /// <param name="id">The ID of the person</param>
        /// <returns>A view displaying information about the given person.</returns>
        public ActionResult ViewDetails(int id)
        {
            WWIViewModel model = new WWIViewModel
            {
                /* See if there's an entry with the id that was passed in */
                VMPerson = db.People.Find(id)
            };

            /* If no entry was found, send the user to the 'NotFound' view */
            if (model.VMPerson == null)
            {
                return View("NotFound");
            }

            /* Check if this person is a PrimaryContactPerson for a company */
            if (model.VMPerson.Customers2.Count() > 0)
            {
                /* Set the bool flag to true */
                model.VMIsPrimaryContactPerson = true;

                /* Grab the data for this customer */
                model.VMCustomerID = model.VMPerson.Customers2.FirstOrDefault().CustomerID;
                model.VMCustomer = db.Customers.Find(model.VMCustomerID);

                /* Calculate the gross sales for this customer */
                model.VMGrossSales = model.VMCustomer.Orders.SelectMany(x => x.Invoices)
                    .SelectMany(x => x.InvoiceLines)
                    .Sum(x => x.ExtendedPrice);

                /* Calculate the gross profit for this customer*/
                model.VMGrossProfits = model.VMCustomer.Orders.SelectMany(x => x.Invoices)
                    .SelectMany(x => x.InvoiceLines)
                    .Sum(x => x.LineProfit);

                /* Grab the top 10 purchases by this customer, in descending order */
                model.VMInvoices = model.VMCustomer.Orders.SelectMany(x => x.Invoices)
                    .SelectMany(x => x.InvoiceLines)
                    .OrderByDescending(x => x.LineProfit)
                    .Take(10)
                    .ToList();

                /* Get the key for the map and store it to use in the view */
                StreamReader reader = new StreamReader("C:\\Users\\vance\\Documents\\School\\CS460\\brockv.github.io\\CS460\\HWK6\\Homework6\\Homework6\\super_secret_key.txt");
                ViewBag.DefinitelyNotASecretKey = reader.ReadLine();
            }

            /* Redirect to the 'ViewDetails' view */
            return View("ViewDetails", model);
        }
    }
}