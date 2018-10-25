using Homework5.DAL;
using Homework5.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework5.Controllers
{
    public class HomeController : Controller
    {
        ServiceRequestFormContext requestFormDatabase = new ServiceRequestFormContext();

        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RequestForm()
        {
            ViewBag.Message = "Tennant Request Form";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestForm([Bind(Include = "ID, FirstName, LastName, PhoneNumber, ApartmentName," +
            "UnitNumber, RequestDescription, AllowEntry")] ServiceRequestForm newRequestForm)
        {
            /* Check if the model was handed to us in a valid state */
            if (ModelState.IsValid)
            {
                /* 
                 * If the model is in a valid state (i.e., all form elements have valid input), 
                 * add it to the list of requests
                 */
                requestFormDatabase.RequestForms.Add(newRequestForm);

                foreach (ServiceRequestForm x in requestFormDatabase.RequestForms)
                {
                    Debug.WriteLine(x);
                }

                /* TO-DO -- Redirect to "Thank you" page */
                return RedirectToAction("Home");
            }

            /* If the model wasn't in a valid state, just return the view */
            return View(newRequestForm);
        }

        [HttpGet]
        public ActionResult RequestLog()
        {
            ViewBag.Message = "Current Service Requests";

            return View(requestFormDatabase.RequestForms.ToList());
        }


    }
}