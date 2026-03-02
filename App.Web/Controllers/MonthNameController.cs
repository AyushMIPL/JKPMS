using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App.Data;
using App.Data.Entities;
using App.Web.Filters;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class MonthNameController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public MonthNameController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // GET: MonthNames
        public ActionResult Index()
        {
            return View(db.MasterMonthName.ToList());
        }

        // GET: MonthNames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterMonthName monthName = db.MasterMonthName.Find(id);
            if (monthName == null)
            {
                return HttpNotFound();
            }
            return View(monthName);
        }

        // GET: MonthNames/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MonthNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterMonthName monthName)
        {
            if (ModelState.IsValid)
            {
                db.MasterMonthName.Add(monthName);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(monthName);
        }

        // GET: MonthNames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterMonthName monthName = db.MasterMonthName.Find(id);
            if (monthName == null)
            {
                return HttpNotFound();
            }
            return View(monthName);
        }

        // POST: MonthNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterMonthName monthName)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthName).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(monthName);
        }

        // GET: MonthNames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterMonthName monthName = db.MasterMonthName.Find(id);
            if (monthName == null)
            {
                return HttpNotFound();
            }
            return View(monthName);
        }

        // POST: MonthNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterMonthName monthName = db.MasterMonthName.Find(id);
            db.MasterMonthName.Remove(monthName);
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
