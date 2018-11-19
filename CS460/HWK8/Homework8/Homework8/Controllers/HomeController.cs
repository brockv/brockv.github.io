using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homework8.Models;
using Homework8.DAL;

namespace Homework8.Controllers
{
    public class HomeController : Controller
    {
        /* Initialize an instance of the database */
        private AuctionHouseContext db = new AuctionHouseContext();

        /// <summary>
        /// Builds a list of the ten most recent
        /// bids on all items listed on the site.
        /// </summary>
        /// <returns>A list of the ten most recent bids on all items, listed in chronological order (newest to oldest).</returns>
        [HttpGet]
        public ActionResult Index()
        {

            List<Bid> recentBids = db.Bids.OrderByDescending(x => x.BidTimestamp).Take(10).ToList();

            return View(recentBids);
        }
    }
}