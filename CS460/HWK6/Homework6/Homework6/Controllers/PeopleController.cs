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
        private readonly WideWorldImportersContext db = new WideWorldImportersContext();

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

            /* Check if this person is the PrimaryContactPerson for a company */
            if (db.Customers.Where(x => x.PrimaryContactPersonID.Equals(model.VMPerson.PersonID)).Any() == true)
            {
                model.IsPrimaryContactPerson = true;
            }

            /* Check if the query found anything */
            if (model.IsPrimaryContactPerson)
            {
                /* Grab the data for this customer */
                model.VMCustomer = db.Customers.Where(x => x.PrimaryContactPersonID.Equals(model.VMPerson.PersonID)).First();

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

                StreamReader reader = new StreamReader("C:\\Users\\vance\\Documents\\School\\CS460\\brockv.github.io\\CS460\\HWK6\\Homework6\\Homework6\\super_secret_key.txt");
                ViewBag.DefinitelyNotASecretKey = reader.ReadLine();
            }


                /* Redirect to the 'ViewDetails' view */
                return View("ViewDetails", model);
            }
    }
}