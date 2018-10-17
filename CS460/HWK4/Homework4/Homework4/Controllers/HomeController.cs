using System;
using System.Web.Mvc;

namespace Homework4.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// GET for Home.cshtml
        /// </summary>
        /// <returns>The view - Home.cshtml</returns>
        public ActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// GET for Converter.cshtml
        /// </summary>
        /// <returns>The view - Converter.cshtml</returns>
        public ActionResult Converter()
        {            
            double miles = 0;
            string userInput = Request["miles_to_convert"];
            string unit = Request["units"];

            /* Attempt to parse the input we grabbed from the form */
            if (Double.TryParse(userInput, out miles))
            {
                /* If we parsed it successfully, do the appropriate conversion */
                switch (unit)
                {
                    case "millimeters":
                        miles = miles * 1609344;
                        break;
                    case "centimeters":
                        miles = miles * 160934.4;
                        break;
                    case "meters":
                        miles = miles * 1609.34;
                        break;
                    case "kilometers":
                        miles = miles * 1.60934;
                        break;
                    default:
                        break;
                }

                /* Construct a message to display relating to the conversion */
                ViewBag.Message = (userInput + " mile(s) is equal to " + miles + " " + unit + ".");
            }
            
            /* Return the view (Converter.cshtml) */
            return View();            
        }
    }
}