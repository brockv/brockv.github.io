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

        /// <summary>
        /// GET method for the main page. From here the user can search for client's
        /// by name using the search bar.
        /// </summary>
        /// <returns>The view with just the search bar and button</returns>
        public ActionResult Home()
        {
            /* Initially hide the area we use to show search results */
            ViewBag.ShowSearchResults = false;

            return View();
        }

        /// <summary>
        /// POST method for the main page. This shows the results of the user's
        /// search, whether successful or not.
        /// </summary>
        /// <param name="searchString">The user's input from the search bar</param>
        /// <returns>The view with the results of the user's search</returns>
        [HttpPost]
        public ActionResult Home(string searchString)
        {
            /* Switch the flag that controls the table visibility */
            ViewBag.ShowSearchResults = true;

            /* Set an initial display message -- assume the search doesn't find anything */
            ViewBag.DisplayMessage = "I'm sorry, your search returned no results.";

            /* Check the search bar for a value */
            if (!String.IsNullOrEmpty(searchString))
            {
                /* Find all names that contain a substring of the user's search */
                var searchResults = db.People.Where(x => x.FullName.ToUpper().Contains(searchString.ToUpper()));              

                /* Check to see if anything was found, and handle it appropriately */
                if (searchResults.Count() > 0)
                {
                    /* Change the display message to include the user's search string */
                    ViewBag.DisplayMessage = "Names matching your search: \"" + searchString + "\"";

                    /* Return the view with the search results */
                    return View(searchResults);
                }
            }

            /* If the search bar was empty, just return the view */            
            return View();
        }
    }
}