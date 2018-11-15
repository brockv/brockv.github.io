﻿using System;
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
            var items = db.Items.Include(i => i.Seller1);
            return View(items.ToList());
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            ViewBag.Seller = new SelectList(db.Sellers, "Name", "Name");
            return PartialView("Create", new Item());
        }

        [HttpPost]
        public JsonResult CreateJSON([Bind(Include = "ID,Name,Description,Seller")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
            }

            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult Edit(int? id)
        {
            /* Initialize a SelectList of all the Sellers so we can use a DropDownList */
            ViewBag.Seller = new SelectList(db.Sellers, "Name", "Name");

            /* Grab the item associated with the ID passed in */
            Item newItem = db.Items.Where(x => x.ID == id).FirstOrDefault();

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

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
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
