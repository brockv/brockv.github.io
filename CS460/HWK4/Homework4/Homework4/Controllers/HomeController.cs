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
                        miles = miles * Properties.Settings.Default.MILLIMETERS_PER_MILE; ;
                        break;
                    case "centimeters":
                        miles = miles * Properties.Settings.Default.CENTIMETERS_PER_MILE;
                        break;
                    case "meters":
                        miles = miles * Properties.Settings.Default.METERS_PER_MILE;
                        break;
                    case "kilometers":
                        miles = miles * Properties.Settings.Default.KILOMETERS_PER_MILE;
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