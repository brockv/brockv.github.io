using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Homework8.DAL;
using Homework8.Models;
using Homework8.Models.ViewModels;
using Newtonsoft.Json;

namespace Homework8.Controllers
{
    public class ItemsController : Controller
    {
        private AuctionHouseContext db = new AuctionHouseContext();

        // GET: Items
        public ActionResult Index()
        {
            /* Grab all of the items currently in the Items table */
            var items = db.Items.Include(i => i.Seller1);

            /* Return the items that were found in a list */
            return View(items.ToList());
        }

        // GET: Items/Details/5
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

        [HttpGet]
        public PartialViewResult Create()
        {
            /* Initialize a SelectList of all the Sellers so we can use a DropDownList */
            ViewBag.Seller = new SelectList(db.Sellers, "Name", "Name");

            /* Return the modal form contained in the partial view */
            return PartialView("Create", new Item());
        }

        [HttpPost]
        public JsonResult CreateJSON([Bind(Include = "ID,Name,Description,Seller")] Item item)
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

        [HttpPost]
        public JsonResult Delete(int? id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();

            /* Return to the JavaScript function */
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteJSON(Item item)
        {
            Item newItem = new Item();

            /* Make sure the model passed in is valid before doing anything with it */
            if (ModelState.IsValid)
            {
                /* Get the item with the ID that was passed in from the modal form */
                newItem = db.Items.Where(x => x.ID == item.ID).FirstOrDefault();

                /* Remove the item from the database and save the changes */
                db.Items.Remove(newItem);
                db.SaveChanges();
            }

            /* Return to the JavaScript function */
            return Json(newItem, JsonRequestBehavior.AllowGet);
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
