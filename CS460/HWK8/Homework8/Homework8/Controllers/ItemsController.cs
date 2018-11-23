using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Homework8.DAL;
using Homework8.Models;

namespace Homework8.Controllers
{
    public class ItemsController : Controller
    {
        /* Initialize an instance of the database */
        private AuctionHouseContext db = new AuctionHouseContext();

        /// <summary>
        /// Builds a list of all the items currently listed on the site.
        /// </summary>
        /// <returns>A list of all items currently listed on the site.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            /* Grab all of the items currently in the Items table */
            var items = db.Items.Include(i => i.Seller1);

            /* Return the items that were found in a list */
            return View(items.ToList());
        }

        /// <summary>
        /// Attempts to load a page containing additional details for an item associated
        /// with the given id.
        /// </summary>
        /// <param name="id">The id to use for looking up an item.</param>
        /// <returns>The page containing additional details for the item associated with the given id.</returns>
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            //var bids = db.Bids.Include(b => b.Buyer1).Include(b => b.Item);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        /// <summary>
        /// Builds a modal window to allow the user to create a new listing.
        /// </summary>
        /// <returns>A modal window containing the input elements necessary for creating a new listing.</returns>
        [HttpGet]
        public PartialViewResult Create()
        {
            /* Initialize a SelectList of all the Sellers so we can use a DropDownList */
            ViewBag.Seller = new SelectList(db.Sellers, "Name", "Name");

            /* Return the modal form contained in the partial view */
            return PartialView("Create", new Item());
        }

        /// <summary>
        /// Uses the information passed from the modal window to create a new entry in the Items table.
        /// </summary>
        /// <param name="item">The item to add to the Items table.</param>
        /// <returns>A JSON object containing information about the item.</returns>
        [HttpPost]
        public JsonResult CreateJSON([Bind(Include = "Name,Description,Seller")] Item item)
        {
            /* Check if the model passed in is valid before doing anything with it */
            if (ModelState.IsValid)
            {
                /* Add the new item to the table and save the changes */
                db.Items.Add(item);
                db.SaveChanges();
            }

            /* Return to the JavaScript function */
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// Builds a modal window to allow the user to edit a listing associated with the given id.
        /// </summary>
        /// <param name="id">The id of the item the user is trying to edit.</param>
        /// <returns>A modal window containing the input elements necessary for editing a listing.</returns>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            /* If the ID passed in is null, silently redirect to the listings page */
            if (id == null)
            {
                /* Silently return to the listings page */
                return RedirectToAction("Index", "Items");
            }            

            /* Grab the item associated with the ID passed in */
            Item newItem = db.Items.Where(x => x.ID == id).FirstOrDefault();

            /* If there wasn't an entry associated with the given ID, silently return to the listings page */
            if (newItem == null)
            {
                /* Silently return to the listings page */
                return RedirectToAction("Index", "Items");
            }

            /* Initialize a SelectList of all the Sellers so we can use a DropDownList */
            ViewBag.Seller = new SelectList(db.Sellers, "Name", "Name", newItem.Seller);

            /* Return the modal form contained in the partial view */
            return PartialView(newItem);
        }

        /// <summary>
        /// Uses the information from the modal window to update the item the user edited.
        /// </summary>
        /// <param name="item">The item the user edited.</param>
        /// <returns>A JSON object containing information about the item.</returns>
        [HttpPost]
        public JsonResult EditJSON(Item item)
        {
            /* Get the item with the ID that was passed in from the modal form */
            Item newItem = db.Items.Where(x => x.ID == item.ID).FirstOrDefault();

            /* Overwrite the values in the database*/
            newItem.ID = item.ID;
            newItem.Name = item.Name;
            newItem.Description = item.Description;
            newItem.Seller = item.Seller;

            /* Save the changes to the item */
            db.SaveChanges();

            /* Return to the JavaScript function */
            return Json(newItem, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Builds a modal window to allow the user to confirm deleting a listing.
        /// </summary>
        /// <param name="id">The id of the item the user is trying to delete.</param>
        /// <returns>A modal window containing information about the item the user is trying to delete.</returns>
        [HttpGet]
        public ActionResult ConfirmDelete(int? id)
        {
            /* Attempt to grab the item associated with the ID passed in */
            Item item = db.Items.Where(x => x.ID == id).FirstOrDefault();

            /* If the ID or item are null, silently redirect to the listings page */
            if (id == null || item == null)
            {
                /* Silently return to the listings page */
                return RedirectToAction("Index", "Items");
            }

            /* Return the modal form contained in the partial view */
            return PartialView(item);
        }

        /// <summary>
        /// Deletes the item associated with given id, and also removes it from the Bids table if it's there.
        /// </summary>
        /// <param name="id">The id of the item being deleted</param>
        /// <returns>A JSON object containing information for the item that was deleted.</returns>
        [HttpPost]
        public JsonResult Delete(int? id)
        {
            /* Get the item associated with the ID passed in */
            Item item = db.Items.Find(id);

            /* If item isn't null, check if it's in the Bids table */
            if (item != null)
            {
                /* Check if this item is in the Bids table */
                List<Bid> bids = db.Bids.Where(x => x.ItemID == id).ToList();

                /* If there are bids associated with this item, remove all of them */
                if (bids.Count > 0)
                {
                    /* Iterate through the bids for this item, removing each one from the Bids table */
                    foreach (Bid bid in bids)
                    {
                        db.Bids.Remove(bid);
                    }
                }

                /* Remove the item from the table and save the changes */
                db.Items.Remove(item);
                db.SaveChanges();
            }
            
            /* Return to the JavaScript function */
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
