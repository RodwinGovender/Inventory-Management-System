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
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jobs
        public ActionResult Index(Job job, string search)
        {


            List<SelectListItem> list2 = new List<SelectListItem>();

            list2.Add(new SelectListItem()
            {
                Text = "Any",
                Value = "Any"
            });

            list2.Add(new SelectListItem()
            {
                Text = "Complete",
                Value = "Complete"
            });

            list2.Add(new SelectListItem()
            {
                Text = "Incomplete",
                Value = "Incomplete"
            });







            ViewBag.Job_Status = list2;


            if (job.Job_Status == "Complete")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(db.Jobs.Where(x => x.Job_Name.Contains(search) && x.Job_Status == "Complete" || search == null).ToList());
            }

            else if (job.Job_Status == "Incomplete")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(db.Jobs.Where(x => x.Job_Name.Contains(search) && x.Job_Status == "Incomplete" || search == null).ToList());
            }
            else if (job.Job_Status == "Any")
            {

                return View(db.Jobs.Where(x => x.Job_Name.Contains(search) || search == null).ToList());
            }


            return View(db.Jobs.ToList());

        }



        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }



        public ActionResult JobInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            JobVM jvm = new JobVM();

            jvm.Job_IDs = job.Job_ID;
            jvm.Job_Names = job.Job_Name;
            jvm.Job_Dates = job.Job_Date;
            jvm.Job_Statuss = job.Job_Status;

            jvm.ItemRecords = (from l in db.ItemRecords where l.Job_ID == job.Job_ID select l).ToList();

            return View(jvm);
        }

        public ActionResult UpdateStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            if(job.Job_Status == "Incomplete")
            {

                job.Job_Status = "Complete";

            }
            else if(job.Job_Status == "Complete")
            {

                job.Job_Status = "Incomplete";

            }
            else
            {

                job.Job_Status = "Complete";
            }
            db.Entry(job).State = EntityState.Modified;
            db.SaveChanges();
            
            return RedirectToAction("JobInfo", new { id = job.Job_ID });
        }







        // GET: Jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Job_ID,Job_Name,Job_Date")] Job job)
        {
            if (ModelState.IsValid)
            {
                job.Job_Status = "Incomplete";
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("JobInfo", new { id = job.Job_ID });
            }

            return View(job);
        }


        //public ActionResult JobDetails(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Job job = db.Jobs.Find(id);
        //    if (job == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var vm = new JobVM();
        //    vm.Job_ID = job.Job_ID;

        //    vm.Job_Name = job.Job_Name;

        //    vm.Job_Date = job.Job_Date;

        //    vm.RecordData = (from d in db.ItemRecords where d.Job_ID == id select d).ToList();

        //    vm.ItemData = (db.Items).ToList();

        //    return View(vm);
        //}

        public ActionResult AddToCart(int? id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            else
            {

                var item = db.Items.Find(id);

                item.Item_Qty = 30;


                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();

            }
            if (Session["cart"] == null)
            {
                List<ItemRecord> cart = new List<ItemRecord>();
                var product = db.Items.Find(id);
                cart.Add(new ItemRecord()
                {
                    ItemRecord_Status = "Incomplete",
                    Item_ID = (int)id
                });
                Session["cart"] = cart;
            }
            else
            {
                List<ItemRecord> cart = (List<ItemRecord>)Session["cart"];
                var product = db.Items.Find(id);
                cart.Add(new ItemRecord()
                {
                    ItemRecord_Status = "Incomplete",
                    Item_ID = (int)id
                });
                Session["cart"] = cart;
            }
            return Redirect("JobDetails");
        }



        //[HttpPost]
        //public ActionResult JobDetails(Item item)
        //{

        //    ItemRecord ir = new ItemRecord();


        //    ir.Item_ID = item.Item_ID;
        //    ir.ItemRecord_QtyUsed = item.Item_Qty;
        //    ir.ItemRecord_Status = "Incomplete";

        //    db.ItemRecords.Add(ir);
        //    db.SaveChanges();


        //    return View();
        //}



        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Job_ID,Job_Name,Job_Date")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
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
