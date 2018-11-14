using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homework8.DAL;
using Homework8.Models;
using Homework8.Models.ViewModels;
using Newtonsoft.Json;

namespace Homework8.Controllers
{
    public class BidsController : Controller
    {
        private AuctionHouseContext db = new AuctionHouseContext();

        // GET: Bids
        public ActionResult Index()
        {
            var bids = db.Bids.Include(b => b.Buyer1).Include(b => b.Item);
            return View(bids.ToList());
        }

        // GET: Bids/Create
        public ActionResult Create()
        {
            ViewBag.Buyer = new SelectList(db.Buyers, "Name", "Name");
            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name");
            return View();
        }

        // POST: Bids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ItemID,Buyer,BidAmount,BidTimestamp")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                db.Bids.Add(bid);
                db.SaveChanges();
                return RedirectToAction("Details/", "Items", new { id = bid.ItemID });
            }

            ViewBag.Buyer = new SelectList(db.Buyers, "Name", "Name", bid.Buyer);
            ViewBag.ItemID = new SelectList(db.Items, "ID", "Name", bid.ItemID);
            return View(bid);
        }

        public JsonResult GetBids(int id)
        {
            AuctionHouseVM vm = new AuctionHouseVM
            {
                //Find the Item with the id
                VMItem = db.Items.Find(id)
            };

            string jsonItem = "";
            if (vm.VMItem != null)
            {
                /* Get all bids associated with this item, listed highest to lowest */
                vm.VMBids = vm.VMItem.Bids
                    .Where(x => x.ItemID == id)
                    .OrderByDescending(x => x.BidAmount)
                    .ToList();

                /* Serialize the JSON object */
                jsonItem = JsonConvert.SerializeObject(vm.VMBids);
            }
            
            /* Send the JSON object back */
            return Json(jsonItem, JsonRequestBehavior.AllowGet);            
        }
    }
}
