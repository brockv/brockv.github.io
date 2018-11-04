using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homework6.Models;
using Homework6.DAL;
using PagedList;

namespace Homework6.Controllers
{
    public class HomeController : Controller
    {
        /* Initialize an instance of the database */
        private WideWorldImportersContext db = new WideWorldImportersContext();

        /// <summary>
        /// Main method for the search page. This shows the results of the user's
        /// search, whether successful or not.
        /// </summary>
        /// <param name="searchString">The user's input from the search bar</param>
        /// <param name="currentFilter">String to store the user's search between pages</param>
        /// <param name="page">The current page the user is viewing</param>
        /// <returns>The view with the results of the user's search</returns>                 
        public ActionResult Home(string searchString, string currentFilter, int? page)
        {
            /* Determine how to handle this request based on the search string */
            if (searchString != null)
            {
                /* Reset the page to 1 if the search string changed */
                page = 1;

                /* Switch the flag that controls the table visibility */
                ViewBag.ShowSearchResults = true;
            }
            else
            {
                /* Update the search string to what's in the filter */
                searchString = currentFilter;
            }

            /* Store the search so we can use it between page views */
            ViewBag.CurrentFilter = searchString;

            /* Check the search bar for a value */
            if (!String.IsNullOrEmpty(searchString))
            {
                /* Find all names that contain a substring of the user's search */
                IQueryable<Person> searchResults = db.People.Where(x => x.FullName.ToUpper().Contains(searchString.ToUpper()));              

                /* Check to see if anything was found, and handle it appropriately */
                if (searchResults.Count() > 0)
                {
                    int pageSize = 10;
                    int pageNumber = (page ?? 1);

                    /* Return the view with the search results */
                    return View(searchResults.OrderBy(x => x.FullName).ToPagedList(pageNumber, pageSize));
                }
            }

            /* If the search bar was empty, just return the view */            
            return View();
        }
    }
}