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
//    public class JobDetailsController : Controller
//    {
//        private AppDbContext db = new AppDbContext();

//        // GET: JobDetails
//        public ActionResult Index()
//        {
//          var jobDetails = db.MasterContributorJobDetails.Include(j => j.Department).Include(j => j.Designation).Include(j => j.Employer).Include(j => j.Grade).Include(j => j.JobTitle);
//            return View(jobDetails.ToList());
//        }

//        // GET: JobDetails/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MasterContributorJobDetails jobDetails = db.MasterContributorJobDetails.Find(id);
//            if (jobDetails == null)
//            {
//                return HttpNotFound();
//            }
//            return View(jobDetails);
//        }

//        // GET: JobDetails/Create
//        public ActionResult Create()
//        {
//          ViewBag.DepartmentId = new SelectList(db.MasterDepartment.OrderBy(x => x.Name), "Id", "Name");
//          ViewBag.DesignationId = new SelectList(db.MasterDesignation.OrderBy(x => x.Name), "Id", "Name");
//          ViewBag.EmployerID = new SelectList(db.MasterEmployer.OrderBy(x => x.EmployerName), "Id", "EmployerName");
//          ViewBag.GradeId = new SelectList(db.MasterGrade.OrderBy(x => x.Name), "Id", "Name");
//          ViewBag.JobTitleId = new SelectList(db.MasterJobTitle.OrderBy(x => x.Name), "Id", "Name");
//            return View();
//        }

//        // POST: JobDetails/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,PersonId,EmployerID,EmploymentType,DepartmentId,DesignationId,JobTitleId,GradeId,HireDate,PresentSalary,JobDescription,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterContributorJobDetails jobDetails)
//        {
//            if (ModelState.IsValid)
//            {
//                db.MasterContributorJobDetails.Add(jobDetails);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.DepartmentId = new SelectList(db.MasterDepartment.OrderBy(x => x.Name), "Id", "Name", jobDetails.DepartmentId);
//            ViewBag.DesignationId = new SelectList(db.MasterDesignation.OrderBy(x => x.Name), "Id", "Name", jobDetails.DesignationId);
//            ViewBag.EmployerID = new SelectList(db.MasterEmployer.OrderBy(x => x.EmployerName), "Id", "EmployerName", jobDetails.EmployerID);
//            ViewBag.GradeId = new SelectList(db.MasterGrade.OrderBy(x => x.Name), "Id", "Name", jobDetails.GradeId);
//            ViewBag.JobTitleId = new SelectList(db.MasterJobTitle.OrderBy(x => x.Name), "Id", "Name");
//            return View(jobDetails);
//        }

//        // GET: JobDetails/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MasterContributorJobDetails jobDetails = db.MasterContributorJobDetails.Find(id);
//            if (jobDetails == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.DepartmentId = new SelectList(db.MasterDepartment.OrderBy(x => x.Name), "Id", "Name", jobDetails.DepartmentId);
//            ViewBag.DesignationId = new SelectList(db.MasterDesignation.OrderBy(x => x.Name), "Id", "Name", jobDetails.DesignationId);
//            ViewBag.EmployerID = new SelectList(db.MasterEmployer.OrderBy(x => x.EmployerName), "Id", "EmployerName", jobDetails.EmployerID);
//            ViewBag.GradeId = new SelectList(db.MasterGrade.OrderBy(x => x.Name), "Id", "Name", jobDetails.GradeId);
//            ViewBag.JobTitleId = new SelectList(db.MasterJobTitle.OrderBy(x => x.Name), "Id", "Name");
//            return View(jobDetails);
//        }

//        // POST: JobDetails/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,PersonId,EmployerID,EmploymentType,DepartmentId,DesignationId,JobTitleId,GradeId,HireDate,PresentSalary,JobDescription,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterContributorJobDetails jobDetails)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(jobDetails).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.DepartmentId = new SelectList(db.MasterDepartment.OrderBy(x => x.Name), "Id", "Name", jobDetails.DepartmentId);
//            ViewBag.DesignationId = new SelectList(db.MasterDesignation.OrderBy(x => x.Name), "Id", "Name", jobDetails.DesignationId);
//            ViewBag.EmployerID = new SelectList(db.MasterEmployer.OrderBy(x => x.EmployerName), "Id", "EmployerName", jobDetails.EmployerID);
//            ViewBag.GradeId = new SelectList(db.MasterGrade.OrderBy(x => x.Name), "Id", "Name", jobDetails.GradeId);
//            ViewBag.JobTitleId = new SelectList(db.MasterJobTitle.OrderBy(x => x.Name), "Id", "Name");
//            return View(jobDetails);
//        }

//        // GET: JobDetails/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MasterContributorJobDetails jobDetails = db.MasterContributorJobDetails.Find(id);
//            if (jobDetails == null)
//            {
//                return HttpNotFound();
//            }
//            return View(jobDetails);
//        }

//        // POST: JobDetails/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            MasterContributorJobDetails jobDetails = db.MasterContributorJobDetails.Find(id);
//            db.MasterContributorJobDetails.Remove(jobDetails);
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
