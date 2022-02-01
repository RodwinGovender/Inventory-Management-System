using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlumbingInventory.Models;

namespace PlumbingInventory.Controllers
{
    public class ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Items
        public ActionResult Index(Item item, string search)
        {
            var items = db.Items.Include(i => i.ItemCat);

            List<SelectListItem> list2 = new List<SelectListItem>();
            list2.Add(new SelectListItem()
            {
                Text = "All Items",
                Value = "999848499"
            });

            foreach (ItemCat itemcat in db.ItemCats)
            {
                list2.Add(new SelectListItem()
                {
                    Text = itemcat.ItemCat_Name,
                    Value = Convert.ToString(itemcat.ItemCat_ID)
                });

            }



            ViewBag.ItemCat_ID = list2;


            if (item.ItemCat_ID == 999848499)
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(db.Items.Where(x => x.Item_Name.Contains(search) || search == null).ToList());
            }

            if (search != null)
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(db.Items.Where(x => x.Item_Name.Contains(search) && x.ItemCat_ID == item.ItemCat_ID || search == null).ToList());
            }

        
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
            if (item == null)
            {
                return HttpNotFound();
            }
            

            item.Item_QtyUsed = 1;

            var jobs = (from l in db.Jobs where l.Job_Status == "Incomplete" select l);
            ViewBag.Item_Job_ID = jobs.Select(x => new SelectListItem
            {
                Text = x.Job_Name,
                Value = x.Job_ID.ToString()

            }).ToList();


            return View(item);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Item item)
        {
            if (ModelState.IsValid)
            {
                ItemRecord Ir = new ItemRecord();

                var exists = (from l in db.ItemRecords where l.Item_ID == item.Item_ID && l.Job_ID == item.Item_Job_ID select l).FirstOrDefault();
                var dbItem = db.Items.Find(item.Item_ID);
                if (exists == null)
                {
                    Ir.Job_ID = item.Item_Job_ID;
                    Ir.Item_ID = item.Item_ID;
                    Ir.ItemRecord_QtyUsed = item.Item_QtyUsed;
                    Ir.ItemRecord_Status = "Added";
                    dbItem.Item_Qty = dbItem.Item_Qty - item.Item_QtyUsed;
                    db.ItemRecords.Add(Ir);
                    db.Entry(dbItem).State = EntityState.Modified;
                    db.SaveChanges();
                }

                else
                {
  
                    exists.ItemRecord_QtyUsed += item.Item_QtyUsed;

                   

                    exists.Item.Item_Qty = dbItem.Item_Qty - item.Item_QtyUsed;


                    db.Entry(exists).State = EntityState.Modified;
                    db.SaveChanges();


                }
                return RedirectToAction("CatalogList");
            }



            var jobs = (from l in db.Jobs where l.Job_Status == "Incomplete" select l);
            ViewBag.Item_Job_ID = jobs.Select(x => new SelectListItem
            {
                Text = x.Job_Name,
                Value = x.Job_ID.ToString()

            }).ToList();
            //item.Item_Name = item.Item_Job_ID.ToString();
            return View(item);
        }






        public ActionResult EditQuantity(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemRecord item = db.ItemRecords.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            

            return View(item);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditQuantity(ItemRecord item)
        {
            if (ModelState.IsValid)
            {
                var old = db.ItemRecords.Find(item.ItemRecord_ID);
                var itemdb = db.Items.Find(item.Item_ID);


                if (item.ItemRecord_QtyUsed == 0)
                {

                    int totalItems = old.ItemRecord_QtyUsed;
                    itemdb.Item_Qty += totalItems;


                    db.ItemRecords.Remove(old);
                    db.Entry(itemdb).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("JobInfo", "Jobs", new { id = item.Job_ID});
                }
                else if (old.ItemRecord_QtyUsed> item.ItemRecord_QtyUsed)
                {
                    int totalItems = old.ItemRecord_QtyUsed - item.ItemRecord_QtyUsed;
                    

                    old.Item.Item_Qty += totalItems;
                    old.ItemRecord_QtyUsed = item.ItemRecord_QtyUsed;



                }
                else if (old.ItemRecord_QtyUsed < item.ItemRecord_QtyUsed)
                {


                    int totalItems = item.ItemRecord_QtyUsed - old.ItemRecord_QtyUsed;
                    old.Item.Item_Qty -= totalItems;
                    old.ItemRecord_QtyUsed = item.ItemRecord_QtyUsed;

                }

                else
                {

                    return RedirectToAction("JobInfo", "Jobs", new { id = item.Job_ID });
                }

                //db.Entry(itemdb).State = EntityState.Modified;
                db.Entry(old).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("JobInfo", "Jobs", new { id = item.Job_ID });
            }



            var jobs = (from l in db.Jobs select l);
            ViewBag.Item_Job_ID = jobs.Select(x => new SelectListItem
            {
                Text = x.Job_Name,
                Value = x.Job_ID.ToString()

            }).ToList();
            //item.Item_Name = item.Item_Job_ID.ToString();
            return RedirectToAction("Details", "Items", new { id = item.Item_ID });
        }























        //[HttpPost]
        //public ActionResult Details(Item item)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Item item = db.Items.Find(id);
        //    if (item == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    ViewBag.Job_ID = new SelectList(db.Jobs, "Job_ID", "Job_Name", item.Job_ID);


        //    return View(item);


        //}

        // GET: Items/Create
        public ActionResult Create()
        {

            ViewBag.ItemCat_ID = new SelectList(db.ItemCats, "ItemCat_ID", "ItemCat_Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item item, HttpPostedFileBase filelist)
        {
            if (ModelState.IsValid)
            {
                if (filelist != null && filelist.ContentLength > 0)
                {
                    item.Image = ConvertToBytes(filelist);
                }

                item.Item_Price = (float)Math.Round((double)item.Item_Price, 2);
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemCat_ID = new SelectList(db.ItemCats, "ItemCat_ID", "ItemCat_Name", item.ItemCat_ID);
            return View(item);
        }




        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }




        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ItemCat_ID = new SelectList(db.ItemCats, "ItemCat_ID", "ItemCat_Name", item.ItemCat_ID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item item, HttpPostedFileBase filelist)
        {
            if (ModelState.IsValid)
            {
                if (filelist != null && filelist.ContentLength > 0)
                {
                    item.Image = ConvertToBytes(filelist);
                }

                item.Item_Price = (float)Math.Round((double)item.Item_Price, 2);
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemCat_ID = new SelectList(db.ItemCats, "ItemCat_ID", "ItemCat_Name", item.ItemCat_ID);
            return View(item);
        }




        public ActionResult CatalogList(Item item, string search)
        {

            var list = (from l in db.Items where l.Item_Qty > 0 select l).ToList();

            List<SelectListItem> list2 = new List<SelectListItem>();
            list2.Add(new SelectListItem()
            {
                Text = "All Items",
                Value = "999848499"
            });

            foreach (ItemCat itemcat in db.ItemCats)
            {
                list2.Add(new SelectListItem()
                {
                    Text = itemcat.ItemCat_Name,
                    Value = Convert.ToString(itemcat.ItemCat_ID)
                });

            }



            ViewBag.ItemCat_ID = list2 ;


            if (item.ItemCat_ID == 999848499)
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(db.Items.Where(x => x.Item_Name.Contains(search) || search == null).ToList());
            }

            if (search != null )
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(db.Items.Where(x => x.Item_Name.Contains(search) && x.ItemCat_ID == item.ItemCat_ID || search == null).ToList());
            }




                return View(list);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToJob(ItemRecord item)
        {
            if (ModelState.IsValid)
            {
              
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemRecord_Job_ID = new SelectList(db.Jobs, "Job_ID", "Job", item.Job_ID);
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
