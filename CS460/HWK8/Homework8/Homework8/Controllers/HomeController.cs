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
        private AuctionHouseContext db = new AuctionHouseContext();

        public ActionResult Index()
        {

            List<Bid> recentBids = db.Bids.Take(10).OrderByDescending(x => x.BidTimestamp).ToList();

            return View(recentBids);
        }
    }
}