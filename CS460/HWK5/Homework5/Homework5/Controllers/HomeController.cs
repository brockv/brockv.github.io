using Homework5.DAL;
using Homework5.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Homework5.Controllers
{
    public class HomeController : Controller
    {
        /* Initialize the list we'll store user's forms in */
        private ServiceRequestFormContext requestFormDatabase = new ServiceRequestFormContext();

        [HttpGet]
        public ActionResult Home()
        {
            /* Return the view for the Home page */
            return View();
        }

        [HttpGet]
        public ActionResult RequestForm()
        {          
            /* Return the view for the Request Form page */
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestForm(ServiceRequestForm newRequestForm)
        {
            /* Check if the model was handed to us in a valid state */
            if (ModelState.IsValid)
            {
                /* Add the service request to the database and save the changes */
                requestFormDatabase.RequestForms.Add(newRequestForm);
                requestFormDatabase.SaveChanges();

                /* Set a success message (displayed on the view), and redirect after a short delay */
                ViewData["Success"] = "Submitted successfully! You will now be redirected...";
                Response.AddHeader("REFRESH", "3;URL=http://localhost:56053/Home/RequestLog");
            }

            /* If the model wasn't in a valid state, just return the view, keeping all input already present */
            return View(newRequestForm);
        }

        [HttpGet]
        public ActionResult RequestLog()
        {
            ViewBag.Message = "Service Request Log";

            /* Generates a list of all the requests, ordered by oldest to newest request */
            return View(requestFormDatabase.RequestForms.ToList().OrderBy(x => x.RequestTimestamp));
        }


    }
}