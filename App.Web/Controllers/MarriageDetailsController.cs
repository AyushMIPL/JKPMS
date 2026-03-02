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
//using System.Threading.Tasks;

//namespace App.Web.Controllers
//{
//    public class MarriageDetailsController : Controller
//    {
//        private AppDbContext db = new AppDbContext();

//        // GET: MarriageDetails
//        public ActionResult Index()
//        {
//            var marriageDetails = db.MasterContributorMarriageDetails.Include(m => m.Country).Include(m => m.MaritalStatus);
//            return View(marriageDetails.ToList());
//        }

//        public async Task<ActionResult> MarriageDetails(string id)
//        {
//          List<MasterContributorMarriageDetails> model = await db.MasterContributorMarriageDetails.Where(o => o.PersonId == id).ToListAsync();
//          return PartialView("Index", model);
//        }
//        // GET: MarriageDetails/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MasterContributorMarriageDetails marriageDetails = db.MasterContributorMarriageDetails.Find(id);
//            if (marriageDetails == null)
//            {
//                return HttpNotFound();
//            }
//            return View(marriageDetails);
//        }

//        // GET: MarriageDetails/Create
//        public ActionResult Create(int? id)
//        {
//            ViewBag.PersonID = id;
//            ViewBag.CountryID = new SelectList(db.MasterCountry.OrderBy(x => x.Name), "Id", "Name");
//            ViewBag.MaritalStatusId = new SelectList(db.MasterMaritalStatus.OrderBy(x => x.Name), "Id", "Name");
//            return View();
//        }

//        // POST: MarriageDetails/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,PersonId,SpouseFirstName,MidName,LastName,CountryID,DateOfBirth,Phone,PhoneOffice,Mobile,Email,MarriageBeginDate,MarriageEndDate,MaritalStatusId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterContributorMarriageDetails marriageDetails)
//        {
//            if (ModelState.IsValid)
//            {
//                db.MasterContributorMarriageDetails.Add(marriageDetails);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.CountryID = new SelectList(db.MasterCountry.OrderBy(x => x.Name), "Id", "Name", marriageDetails.CountryID);
//            ViewBag.MaritalStatusId = new SelectList(db.MasterMaritalStatus.OrderBy(x => x.Name), "Id", "Name", marriageDetails.MaritalStatusId);
//            return View(marriageDetails);
//        }

//        // GET: MarriageDetails/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MasterContributorMarriageDetails marriageDetails = db.MasterContributorMarriageDetails.Find(id);
//            if (marriageDetails == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.CountryID = new SelectList(db.MasterCountry.OrderBy(x => x.Name), "Id", "Name", marriageDetails.CountryID);
//            ViewBag.MaritalStatusId = new SelectList(db.MasterMaritalStatus.OrderBy(x => x.Name), "Id", "Name", marriageDetails.MaritalStatusId);
//            return View(marriageDetails);
//        }

//        // POST: MarriageDetails/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,PersonId,SpouseFirstName,MidName,LastName,CountryID,DateOfBirth,Phone,PhoneOffice,Mobile,Email,MarriageBeginDate,MarriageEndDate,MaritalStatusId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterContributorMarriageDetails marriageDetails)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(marriageDetails).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.CountryID = new SelectList(db.MasterCountry.OrderBy(x => x.Name), "Id", "Name", marriageDetails.CountryID);
//            ViewBag.MaritalStatusId = new SelectList(db.MasterMaritalStatus.OrderBy(x => x.Name), "Id", "Name", marriageDetails.MaritalStatusId);
//            return View(marriageDetails);
//        }

//        // GET: MarriageDetails/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MasterContributorMarriageDetails marriageDetails = db.MasterContributorMarriageDetails.Find(id);
//            if (marriageDetails == null)
//            {
//                return HttpNotFound();
//            }
//            return View(marriageDetails);
//        }

//        // POST: MarriageDetails/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            MasterContributorMarriageDetails marriageDetails = db.MasterContributorMarriageDetails.Find(id);
//            db.MasterContributorMarriageDetails.Remove(marriageDetails);
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
