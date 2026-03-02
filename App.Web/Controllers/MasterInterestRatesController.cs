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
using App.Web.Models;
using System.Linq.Dynamic;
using App.Data.ViewModels;
using App.Web.Repository;
using App.Web.Filters;
namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class MasterInterestRatesController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public MasterInterestRatesController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // GET: MasterInterestRates
        public ActionResult Index()
        {
            ViewBag.EmployerTypeID = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true), "Id", "Name");
            return View();
        }
        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var List = db.MasterInterestRate.Where(x => x.IsActive == true);
            IEnumerable<MasterInterestRate> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.InterestRate.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.DateFrom.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.DateTo.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.employerType.Name.ToString().ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MasterInterestRate, string> orderingFunction = (c => sortColumnIndex == 0 ? c.InterestRate + "" :
                                                                                  sortColumnIndex == 1 ? c.DateFrom + "" :
                                                                                            sortColumnIndex == 2 ? c.DateTo + "" :
                                                                                            "");

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filtered = filtered.OrderBy(orderingFunction);
            else
                filtered = filtered.OrderByDescending(orderingFunction);

            //Pagging
            var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            //Select required columns
            var result = from c in displayed
                         select new[] {
                     String.Format("{0:MM/dd/yyyy}", c.DateFrom)+" - "+(c.DateTo==null?"Future date":String.Format("{0:MM/dd/yyyy}", c.DateTo)),
                     c.employerType.Name,
                         c.InterestRate+"",
                                         c.Id + ""
                                     };

            return Json(
                                        new
                                        {
                                            sEcho = param.sEcho,
                                            iTotalRecords = List.Count(),
                                            iTotalDisplayRecords = filtered.Count(),
                                            aaData = result
                                        }, JsonRequestBehavior.AllowGet);
        }
        // GET: MasterInterestRates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterInterestRate masterInterestRate = db.MasterInterestRate.Find(id);
            if (masterInterestRate == null)
            {
                return HttpNotFound();
            }
            return View(masterInterestRate);
        }

        // GET: MasterInterestRates/Create
        public ActionResult Create()
        {
            ViewBag.EmployerTypeID = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true), "Id", "Name");
            return View("Index");
        }

        // POST: MasterInterestRates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterInterestRate masterInterestRate)
        {
            string error = "";
            var tempData = db.MasterInterestRate.Where(x => x.IsActive == true && x.EmployerTypeID == masterInterestRate.EmployerTypeID).Select(x => new { x.Id, x.DateFrom, x.DateTo }).ToList();
            if (tempData.Where(x => !x.DateTo.HasValue).Any())
                error = "Please Fill to Date of Last Record";
            else if (masterInterestRate.DateTo.HasValue)
            {
                if (masterInterestRate.DateFrom > masterInterestRate.DateTo)
                    error = "Date to is always greater than from Date";
                else if (tempData.Where(x => x.DateFrom <= masterInterestRate.DateFrom && x.DateTo >= masterInterestRate.DateFrom).Any() || tempData.Where(x => x.DateFrom <= masterInterestRate.DateTo && x.DateTo >= masterInterestRate.DateTo).Any() || tempData.Where(x => x.DateFrom >= masterInterestRate.DateFrom && x.DateFrom <= masterInterestRate.DateTo).Any())
                    error = "Interest Rate is Already Defined for this Period.Try Another One";
            }
            else if (!masterInterestRate.DateTo.HasValue)
            {
                if (tempData.Where(x => x.DateFrom <= masterInterestRate.DateFrom && x.DateTo >= masterInterestRate.DateFrom).Any() || tempData.Where(x => x.DateFrom <= DateTime.Now && x.DateTo >= DateTime.Now).Any() || tempData.Where(x => x.DateFrom >= masterInterestRate.DateFrom && x.DateFrom <= DateTime.Now).Any())
                    error = "Interest Rate is Already Defined for this Period.Try Another One";
            }
            //else if (tempData.Where(x => x.DateFrom <= masterInterestRate.DateFrom && x.DateTo >= masterInterestRate.DateFrom).Any())
            //  error = "Interest Rate is Already Defined for this Period.Try Another One";
            //else if (masterInterestRate.DateTo.HasValue)
            //{
            //  if (tempData.Where(x => x.DateFrom <= masterInterestRate.DateTo && x.DateTo >= masterInterestRate.DateTo).Any())
            //    error = "Interest Rate is Already Defined for this Period.Try Another One";
            //}
            //else if (!masterInterestRate.DateTo.HasValue)
            //  if (tempData.Where(x => x.DateFrom <= DateTime.Now && x.DateTo >= DateTime.Now).Any())
            //    error = "Interest Rate is Already Defined for this Period.Try Another One";

            if (error == "")
            {
                if (ModelState.IsValid)
                {
                    masterInterestRate.IsActive = true;
                    db.MasterInterestRate.Add(masterInterestRate);
                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        TempData["success"] = "Record Saved Successfully";
                    }
                    else
                    {
                        TempData["error"] = "There is Some Error. Please try again later";
                    }
                }
            }
            else
            {
                TempData["error"] = error;
            }
            ViewBag.EmployerTypeID = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true), "Id", "Name", masterInterestRate.EmployerTypeID);
            return View("Index");
        }

        // GET: MasterInterestRates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterInterestRate masterInterestRate = db.MasterInterestRate.Find(id);
            if (masterInterestRate == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployerTypeID = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true), "Id", "Name", masterInterestRate.EmployerTypeID);
            return View(masterInterestRate);
        }

        // POST: MasterInterestRates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterInterestRate masterInterestRate)
        {
            string error = "";
            var tempData = db.MasterInterestRate.Where(x => x.IsActive == true && x.EmployerTypeID == masterInterestRate.EmployerTypeID && x.Id != masterInterestRate.Id).Select(x => new { x.Id, x.DateFrom, x.DateTo }).ToList();
            if (tempData.Where(x => !x.DateTo.HasValue && x.Id != masterInterestRate.Id).Any())
                error = "Please Fill to Date of Last Record";
            else if (masterInterestRate.DateTo.HasValue)
            {
                if (masterInterestRate.DateFrom > masterInterestRate.DateTo)
                    error = "Date to is always greater than from Date";
                else if (tempData.Where(x => x.DateFrom <= masterInterestRate.DateFrom && x.DateTo >= masterInterestRate.DateFrom).Any() || tempData.Where(x => x.DateFrom <= masterInterestRate.DateTo && x.DateTo >= masterInterestRate.DateTo).Any() || tempData.Where(x => x.DateFrom >= masterInterestRate.DateFrom && x.DateFrom <= masterInterestRate.DateTo).Any())
                    error = "Interest Rate is Already Defined for this Period.Try Another One";
            }
            else if (!masterInterestRate.DateTo.HasValue)
            {
                if (tempData.Where(x => x.DateFrom <= masterInterestRate.DateFrom && x.DateTo >= masterInterestRate.DateFrom).Any() || tempData.Where(x => x.DateFrom <= DateTime.Now && x.DateTo >= DateTime.Now).Any() || tempData.Where(x => x.DateFrom >= masterInterestRate.DateFrom && x.DateFrom <= DateTime.Now).Any())
                    error = "Interest Rate is Already Defined for this Period.Try Another One";
            }
            //else if (tempData.Where(x => x.DateFrom <= masterInterestRate.DateFrom && x.DateTo >= masterInterestRate.DateFrom && x.Id != masterInterestRate.Id).Any())
            //  error = "Interest Rate is Already Defined for this Period.Try Another One";
            //else if (masterInterestRate.DateTo.HasValue)
            //{
            //  if (tempData.Where(x => x.DateFrom <= masterInterestRate.DateTo && x.DateTo >= masterInterestRate.DateTo && x.Id != masterInterestRate.Id).Any())
            //    error = "Interest Rate is Already Defined for this Period.Try Another One";
            //}
            //else if (!masterInterestRate.DateTo.HasValue)
            //  if (tempData.Where(x => x.DateFrom <= DateTime.Now && x.DateTo >= DateTime.Now && x.Id != masterInterestRate.Id).Any())
            //    error = "Interest Rate is Already Defined for this Period.Try Another One";

            if (error == "")
            {
                if (ModelState.IsValid)
                {
                    masterInterestRate.IsActive = true;
                    db.Entry(masterInterestRate).State = EntityState.Modified;
                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        TempData["success"] = "Record Saved Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "There is Some Error. Please try again later";
                    }
                }
            }
            else
            {
                TempData["error"] = error;
            }
            ViewBag.EmployerTypeID = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true), "Id", "Name", masterInterestRate.EmployerTypeID);
            return View(masterInterestRate);
        }

        // GET: MasterInterestRates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterInterestRate masterInterestRate = db.MasterInterestRate.Find(id);
            if (masterInterestRate == null)
            {
                return HttpNotFound();
            }
            return View(masterInterestRate);
        }

        // POST: MasterInterestRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterInterestRate masterInterestRate = db.MasterInterestRate.Find(id);
            db.MasterInterestRate.Remove(masterInterestRate);
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
        public JsonResult Remove(int Id, string name)
        {
            int result = 0;
            if (Id != 0)
            {
                bool action = MasterDeleteRepo.AjaxDelete(Id, name);
                if (action)
                {
                    MasterInterestRate entity = db.MasterInterestRate.Where(x => x.Id == Id).FirstOrDefault();
                    entity.IsActive = false;
                    db.Entry(entity).State = EntityState.Modified;
                    result = db.SaveChanges();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(2, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
