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
using App.Web.Repository;
using App.Web.Filters;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class MasterDiscountForGratuityMainController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public MasterDiscountForGratuityMainController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // GET: MasterDiscountForGratuityMain
        public ActionResult Index()
        {
            ViewBag.EmployerTypeID = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true), "Id", "Name");
            return View();
        }
        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var List = db.MasterDiscountForGratuityMain.Where(x => x.IsActive == true);
            IEnumerable<MasterDiscountForGratuityMain> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.DiscountFactorPercent.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.DateFrom.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.DateTo.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.MaxYearsToNormalRetirement.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.employerType.Name.ToString().ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MasterDiscountForGratuityMain, string> orderingFunction = (c => sortColumnIndex == 0 ? c.DiscountFactorPercent + "" :
                                                                                  sortColumnIndex == 1 ? c.DateFrom + "" :
                                                                                            sortColumnIndex == 2 ? c.DateTo + "" :
                                                                                            sortColumnIndex == 3 ? c.MaxYearsToNormalRetirement + "" :
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
                     String.Format("{0:0.00}", c.DiscountFactorPercent),
                     c.MaxYearsToNormalRetirement + "",
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
        // GET: MasterDiscountForGratuityMain/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterDiscountForGratuityMain masterDiscountForGratuityMain = db.MasterDiscountForGratuityMain.Find(id);
            if (masterDiscountForGratuityMain == null)
            {
                return HttpNotFound();
            }
            return View(masterDiscountForGratuityMain);
        }

        // GET: MasterDiscountForGratuityMain/Create
        public ActionResult Create()
        {
            ViewBag.EmployerTypeID = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true), "Id", "Name");
            return View();
        }

        // POST: MasterDiscountForGratuityMain/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterDiscountForGratuityMain masterDiscountForGratuityMain)
        {
            if (ModelState.IsValid)
            {
                string error = "";
                var tempData = db.MasterDiscountForGratuityMain.Where(x => x.IsActive == true && x.EmployerTypeID == masterDiscountForGratuityMain.EmployerTypeID).Select(x => new { x.Id, x.DateFrom, x.DateTo }).ToList();
                if (tempData.Where(x => !x.DateTo.HasValue).Any())
                    error = "Please Fill to Date of Last Record";
                else if (masterDiscountForGratuityMain.DateTo.HasValue)
                {
                    if (masterDiscountForGratuityMain.DateFrom > masterDiscountForGratuityMain.DateTo)
                        error = "Date to is always greater than from Date";
                    else if (tempData.Where(x => x.DateFrom <= masterDiscountForGratuityMain.DateFrom && x.DateTo >= masterDiscountForGratuityMain.DateFrom).Any() || tempData.Where(x => x.DateFrom <= masterDiscountForGratuityMain.DateTo && x.DateTo >= masterDiscountForGratuityMain.DateTo).Any() || tempData.Where(x => x.DateFrom >= masterDiscountForGratuityMain.DateFrom && x.DateFrom <= masterDiscountForGratuityMain.DateTo).Any())
                        error = "Interest Rate is Already Defined for this Period.Try Another One";
                }
                else if (!masterDiscountForGratuityMain.DateTo.HasValue)
                {
                    if (tempData.Where(x => x.DateFrom <= masterDiscountForGratuityMain.DateFrom && x.DateTo >= masterDiscountForGratuityMain.DateFrom).Any() || tempData.Where(x => x.DateFrom <= DateTime.Now && x.DateTo >= DateTime.Now).Any() || tempData.Where(x => x.DateFrom >= masterDiscountForGratuityMain.DateFrom && x.DateFrom <= DateTime.Now).Any())
                        error = "Interest Rate is Already Defined for this Period.Try Another One";
                }
                //else if (tempData.Where(x => x.DateFrom <= masterDiscountForGratuityMain.DateFrom && x.DateTo >= masterDiscountForGratuityMain.DateFrom).Any())
                //  error = "Interest Rate is Already Defined for this Period.Try Another One";
                //else if (masterDiscountForGratuityMain.DateTo.HasValue)
                //{
                //  if (tempData.Where(x => x.DateFrom <= masterDiscountForGratuityMain.DateTo && x.DateTo >= masterDiscountForGratuityMain.DateTo).Any())
                //    error = "Interest Rate is Already Defined for this Period.Try Another One";
                //  if (tempData.Where(x => x.DateFrom >= masterDiscountForGratuityMain.DateFrom && x.DateFrom <= masterDiscountForGratuityMain.DateTo).Any())
                //    error = "Interest Rate is Already Defined for this Period.Try Another One";

                //}
                //else if (!masterDiscountForGratuityMain.DateTo.HasValue)
                //  if (tempData.Where(x => x.DateFrom <= DateTime.Now && x.DateTo >= DateTime.Now).Any())
                //    error = "Interest Rate is Already Defined for this Period.Try Another One";

                if (error == "")
                {

                    masterDiscountForGratuityMain.IsActive = true;
                    db.MasterDiscountForGratuityMain.Add(masterDiscountForGratuityMain);
                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        double dFactor = Convert.ToDouble(1 + (masterDiscountForGratuityMain.DiscountFactorPercent / 100));
                        for (int i = 1; i <= masterDiscountForGratuityMain.MaxYearsToNormalRetirement; i++)
                        {
                            double power = i;
                            MasterDiscountForGratuityDetails entity = new MasterDiscountForGratuityDetails();
                            entity.DiscountFactor = Math.Round(Convert.ToDecimal(1 / Math.Pow(dFactor, power)), 4);
                            entity.IsActive = true;
                            entity.MainId = masterDiscountForGratuityMain.Id;
                            entity.YearsToNormalRetirement = i;
                            db.MasterDiscountForGratuityDetails.Add(entity);
                        }
                        db.SaveChanges();
                        TempData["success"] = "Record Saved Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "There is Some Error. Please try again later";
                    }
                }
                else
                {
                    TempData["error"] = error;
                }
            }
            ViewBag.EmployerTypeID = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true), "Id", "Name", masterDiscountForGratuityMain.EmployerTypeID);
            return View(masterDiscountForGratuityMain);
        }

        // GET: MasterDiscountForGratuityMain/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterDiscountForGratuityMain masterDiscountForGratuityMain = db.MasterDiscountForGratuityMain.Find(id);
            if (masterDiscountForGratuityMain == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployerTypeID = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true), "Id", "Name", masterDiscountForGratuityMain.EmployerTypeID);
            return View(masterDiscountForGratuityMain);
        }

        // POST: MasterDiscountForGratuityMain/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterDiscountForGratuityMain masterDiscountForGratuityMain)
        {
            if (ModelState.IsValid)
            {
                string error = "";
                var tempData = db.MasterDiscountForGratuityMain.Where(x => x.IsActive == true && x.EmployerTypeID == masterDiscountForGratuityMain.EmployerTypeID && x.Id != masterDiscountForGratuityMain.Id).Select(x => new { x.Id, x.DateFrom, x.DateTo }).ToList();
                if (tempData.Where(x => !x.DateTo.HasValue && x.Id != masterDiscountForGratuityMain.Id).Any())
                    error = "Please Fill to Date of Last Record";
                else if (masterDiscountForGratuityMain.DateTo.HasValue)
                {
                    if (masterDiscountForGratuityMain.DateFrom > masterDiscountForGratuityMain.DateTo)
                        error = "Date to is always greater than from Date";
                    else if (tempData.Where(x => x.DateFrom <= masterDiscountForGratuityMain.DateFrom && x.DateTo >= masterDiscountForGratuityMain.DateFrom).Any() || tempData.Where(x => x.DateFrom <= masterDiscountForGratuityMain.DateTo && x.DateTo >= masterDiscountForGratuityMain.DateTo).Any() || tempData.Where(x => x.DateFrom >= masterDiscountForGratuityMain.DateFrom && x.DateFrom <= masterDiscountForGratuityMain.DateTo).Any())
                        error = "Interest Rate is Already Defined for this Period.Try Another One";
                }
                else if (!masterDiscountForGratuityMain.DateTo.HasValue)
                {
                    if (tempData.Where(x => x.DateFrom <= masterDiscountForGratuityMain.DateFrom && x.DateTo >= masterDiscountForGratuityMain.DateFrom).Any() || tempData.Where(x => x.DateFrom <= DateTime.Now && x.DateTo >= DateTime.Now).Any() || tempData.Where(x => x.DateFrom >= masterDiscountForGratuityMain.DateFrom && x.DateFrom <= DateTime.Now).Any())
                        error = "Interest Rate is Already Defined for this Period.Try Another One";
                }
                //else if (tempData.Where(x => x.DateFrom <= masterDiscountForGratuityMain.DateFrom && x.DateTo >= masterDiscountForGratuityMain.DateFrom && x.Id != masterDiscountForGratuityMain.Id).Any())
                //  error = "Interest Rate is Already Defined for this Period.Try Another One";
                //else if (masterDiscountForGratuityMain.DateTo.HasValue)
                //{
                //  if (tempData.Where(x => x.DateFrom <= masterDiscountForGratuityMain.DateTo && x.DateTo >= masterDiscountForGratuityMain.DateTo && x.Id != masterDiscountForGratuityMain.Id).Any())
                //    error = "Interest Rate is Already Defined for this Period.Try Another One";
                //}
                //else if (!masterDiscountForGratuityMain.DateTo.HasValue)
                //  if (tempData.Where(x => x.DateFrom <= DateTime.Now && x.DateTo >= DateTime.Now && x.Id != masterDiscountForGratuityMain.Id).Any())
                //    error = "Interest Rate is Already Defined for this Period.Try Another One";

                if (error == "")
                {
                    masterDiscountForGratuityMain.IsActive = true;
                    db.Entry(masterDiscountForGratuityMain).State = EntityState.Modified;
                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        List<MasterDiscountForGratuityDetails> masterDiscountForGratuityDetails = db.MasterDiscountForGratuityDetails.Where(x => x.IsActive == true && x.MainId == masterDiscountForGratuityMain.Id).ToList();
                        foreach (var item in masterDiscountForGratuityDetails)
                        {
                            item.IsActive = false;
                            db.Entry(item).State = EntityState.Modified;
                            db.SaveChanges();
                        }

                        double dFactor = Convert.ToDouble(1 + (masterDiscountForGratuityMain.DiscountFactorPercent / 100));
                        for (int i = 1; i <= masterDiscountForGratuityMain.MaxYearsToNormalRetirement; i++)
                        {
                            double power = i;
                            MasterDiscountForGratuityDetails entity = new MasterDiscountForGratuityDetails();
                            entity.DiscountFactor = Math.Round(Convert.ToDecimal(1 / Math.Pow(dFactor, power)), 4);
                            entity.IsActive = true;
                            entity.MainId = masterDiscountForGratuityMain.Id;
                            entity.YearsToNormalRetirement = i;
                            db.MasterDiscountForGratuityDetails.Add(entity);
                        }
                        db.SaveChanges();
                        TempData["success"] = "Record Updated Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "There is Some Error. Please try again later";
                    }
                }
                else
                {
                    TempData["error"] = error;
                }
            }
            ViewBag.EmployerTypeID = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true), "Id", "Name", masterDiscountForGratuityMain.EmployerTypeID);
            return View(masterDiscountForGratuityMain);
        }

        // GET: MasterDiscountForGratuityMain/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterDiscountForGratuityMain masterDiscountForGratuityMain = db.MasterDiscountForGratuityMain.Find(id);
            if (masterDiscountForGratuityMain == null)
            {
                return HttpNotFound();
            }
            return View(masterDiscountForGratuityMain);
        }

        // POST: MasterDiscountForGratuityMain/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterDiscountForGratuityMain masterDiscountForGratuityMain = db.MasterDiscountForGratuityMain.Find(id);
            db.MasterDiscountForGratuityMain.Remove(masterDiscountForGratuityMain);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult AjaxDiscountedTable(double discountfactor, int maxYear)
        {
            List<MasterDiscountForGratuityDetails> result = new List<MasterDiscountForGratuityDetails>();
            double dFactor = Convert.ToDouble(1 + (discountfactor / 100));
            for (int i = 1; i <= maxYear; i++)
            {
                double power = i;
                MasterDiscountForGratuityDetails entity = new MasterDiscountForGratuityDetails();
                entity.DiscountFactor = Math.Round(Convert.ToDecimal(1 / Math.Pow(dFactor, power)), 4);
                entity.YearsToNormalRetirement = i;
                result.Add(entity);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AjaxEmployerType(int Id)
        {
            var result = db.MasterEmployerType.Where(x => x.Id == Id).Select(x => x.RetirementAge).FirstOrDefault();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Remove(int Id, string name)
        {
            int result = 0;
            if (Id != 0)
            {
                bool action = MasterDeleteRepo.AjaxDelete(Id, name);
                if (action)
                {
                    MasterDiscountForGratuityMain entity = db.MasterDiscountForGratuityMain.Where(x => x.Id == Id).FirstOrDefault();
                    entity.IsActive = false;
                    db.Entry(entity).State = EntityState.Modified;
                    result = db.SaveChanges();
                    if (result > 0)
                    {
                        List<MasterDiscountForGratuityDetails> details = db.MasterDiscountForGratuityDetails.Where(x => x.MainId == Id && x.IsActive == true).ToList();
                        foreach (var item in details)
                        {
                            item.IsActive = false;
                            db.Entry(item).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(2, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
