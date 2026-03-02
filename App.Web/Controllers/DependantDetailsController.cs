//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using App.Data;
//using App.Data.Entities;

//namespace App.Web.Controllers
//{
//    public class DependantDetailsController : Controller
//    {
//        private AppDbContext db = new AppDbContext();

//        // GET: DependantDetails
//        public ActionResult Index()
//        {
//            var dependantDetails = db.MasterDependantDetails.Include(d => d.Relationship);
//            return View(dependantDetails.ToList());
//        }

//        // GET: DependantDetails/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MasterDependantDetails dependantDetails = db.MasterDependantDetails.Find(id);
//            if (dependantDetails == null)
//            {
//                return HttpNotFound();
//            }
//            return View(dependantDetails);
//        }

//        // GET: DependantDetails/Create
//        public ActionResult Create()
//        {
//          ViewBag.RelationshipID = new SelectList(db.MasterRelationshipType.OrderBy(x=>x.Name), "Id", "Name");
//            return View();
//        }

//        // POST: DependantDetails/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,PersonId,FirstName,MidName,LastName,Gender,DateOfBirth,RelationshipID,TerminationDate,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterDependantDetails dependantDetails)
//        {
//            if (ModelState.IsValid)
//            {
//                db.MasterDependantDetails.Add(dependantDetails);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.RelationshipID = new SelectList(db.MasterRelationshipType.OrderBy(x => x.Name), "Id", "Name", dependantDetails.RelationshipID);
//            return View(dependantDetails);
//        }

//        // GET: DependantDetails/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MasterDependantDetails dependantDetails = db.MasterDependantDetails.Find(id);
//            if (dependantDetails == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.RelationshipID = new SelectList(db.MasterRelationshipType.OrderBy(x => x.Name), "Id", "Name", dependantDetails.RelationshipID);
//            return View(dependantDetails);
//        }

//        // POST: DependantDetails/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,PersonId,FirstName,MidName,LastName,Gender,DateOfBirth,RelationshipID,TerminationDate,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterDependantDetails dependantDetails)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(dependantDetails).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.RelationshipID = new SelectList(db.MasterRelationshipType.OrderBy(x => x.Name), "Id", "Name", dependantDetails.RelationshipID);
//            return View(dependantDetails);
//        }

//        // GET: DependantDetails/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MasterDependantDetails dependantDetails = db.MasterDependantDetails.Find(id);
//            if (dependantDetails == null)
//            {
//                return HttpNotFound();
//            }
//            return View(dependantDetails);
//        }

//        // POST: DependantDetails/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            MasterDependantDetails dependantDetails = db.MasterDependantDetails.Find(id);
//            db.MasterDependantDetails.Remove(dependantDetails);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
