using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Homework8.DAL;
using Homework8.Models;
using Homework8.Models.ViewModels;
using Newtonsoft.Json;

namespace Homework8.Controllers
{
    public class BidsController : Controller
    {
        /* Initialize an instance of the database */
        private AuctionHouseContext db = new AuctionHouseContext();

        /// <summary>
        /// Uses the given id to construct a modal for the item associated with it.
        /// </summary>
        /// <param name="id">The id of the item to show the modal for.</param>
        /// <returns>A modal for the associated bid contained in a partial view.</returns>
        [HttpGet]
        public ActionResult Create(int? id)
        {
            Bid bid = new Bid
            {
                Item = db.Items.Find(id)
            };

            ViewBag.Buyer = new SelectList(db.Buyers, "Name", "Name");

            return PartialView(bid);
        }

        /// <summary>
        /// Creates a new Bid based on user information and saves it to the appropriate table.
        /// </summary>
        /// <param name="bid">The entry to create in the Bids table.</param>
        /// <returns>A JSON object containing the information from the form.</returns>
        [HttpPost]
        public JsonResult CreateJSON([Bind(Include = "ItemID,Buyer,BidAmount")] Bid bid)
        {
            /* Make sure the Model passed in is in a valid state before doing anything with it */
            if (ModelState.IsValid)
            {
                /* Set the time of the bid */
                bid.BidTimestamp = System.DateTime.Now;

                /* Add the bid to the Bids table and save the changes to the database */
                db.Bids.Add(bid);
                db.SaveChanges();
            }

            return Json(bid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Builds a list of bids for an item associated with the given id
        /// </summary>
        /// <param name="id">The id of the item to check for bids</param>
        /// <returns>If there are bids associated with the item found, a JSON object containing 
        /// information on each bid. Otherwise, an empty string.
        /// </returns>
        [HttpGet]
        public JsonResult GetBids(int id)
        {
            /* Initialize our ViewModel and attempt to find an item associated with the given id */
            AuctionHouseVM vm = new AuctionHouseVM
            {
                /* Attempt to find an item associated with the given id */
                VMItem = db.Items.Find(id)
            };

            /* If there was an item associated with the given id, check if it has any bids */
            string jsonItem = "";
            if (vm.VMItem != null)
            {
                /* Get all bids associated with this item and convert them to a list, from highest to lowest */
                vm.VMBids = vm.VMItem.Bids
                    .Where(x => x.ItemID == id)
                    .OrderByDescending(x => x.BidAmount)
                    .ToList();
            }

            /* Make sure the given item has bids before doing anything with the JSON object */
            if (vm.VMBids.Count > 0)
            {
                /* Serialize the JSON object */
                jsonItem = JsonConvert.SerializeObject(vm.VMBids);
            }
            
            /* Send the JSON object back -- If there were no bids, this will be an empty string */
            return Json(jsonItem, JsonRequestBehavior.AllowGet);            
        }

        [HttpGet]
        public JsonResult GetHighestBid(int? id)
        {
            /* Initialize our ViewModel and attempt to find an item associated with the given id */
            AuctionHouseVM vm = new AuctionHouseVM
            {
                /* Attempt to find an item associated with the given id */
                VMItem = db.Items.Find(id)
            };

            /* If there was an item associated with the given id, check if it has any bids */
            decimal highestBid = 0;
            if (vm.VMItem != null)
            {
                /* Get the highest bid associated with this item */
                vm.VMBids = vm.VMItem.Bids
                    .Where(x => x.ItemID == id)
                    .OrderByDescending(x => x.BidAmount)
                    .Take(1)
                    .ToList();

                /* Get the bid amount from the bid we just grabbed */
                highestBid = vm.VMBids.Select(x => x.BidAmount).FirstOrDefault();
            }

            return Json(highestBid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateBid()
        {
            ViewBag.Buyer = new SelectList(db.Buyers, "Name", "Name");
            return PartialView("Create", new Bid());
        }
    }
}
