using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlumbingInventory.Models;

namespace PlumbingInventory.Controllers
{
    public class ItemCatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ItemCats
        public ActionResult Index()
        {
            return View(db.ItemCats.ToList());
        }

        // GET: ItemCats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCat itemCat = db.ItemCats.Find(id);
            if (itemCat == null)
            {
                return HttpNotFound();
            }
            return View(itemCat);
        }

        // GET: ItemCats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemCats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemCat_ID,ItemCat_Name")] ItemCat itemCat)
        {
            if (ModelState.IsValid)
            {
                
                db.ItemCats.Add(itemCat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(itemCat);
        }

        // GET: ItemCats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCat itemCat = db.ItemCats.Find(id);
            if (itemCat == null)
            {
                return HttpNotFound();
            }
            return View(itemCat);
        }

        // POST: ItemCats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemCat_ID,ItemCat_Name")] ItemCat itemCat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemCat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(itemCat);
        }

        // GET: ItemCats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCat itemCat = db.ItemCats.Find(id);
            if (itemCat == null)
            {
                return HttpNotFound();
            }
            return View(itemCat);
        }

        // POST: ItemCats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemCat itemCat = db.ItemCats.Find(id);
            db.ItemCats.Remove(itemCat);
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
