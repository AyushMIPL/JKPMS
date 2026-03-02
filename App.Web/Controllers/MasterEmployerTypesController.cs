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
using App.Web.Filters;
using App.Web.Repository;
using App.Web.Helper;
using App.Data.ViewModels;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class MasterEmployerTypesController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public MasterEmployerTypesController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // GET: MasterEmployerTypes
        public ActionResult Index()
        {
            return View(db.MasterEmployerType.ToList());
        }
        public JsonResult Remove(string Id, string name)
        {
            int result = 0;
            string IdUrl = UrlEncryption.Decrypt(Id);
            if (Id != "0")
            {
                int id = Convert.ToInt32(IdUrl);
                bool action = MasterDeleteRepo.AjaxDelete(id, name);
                if (action)
                {
                    MasterGrade entity = db.MasterGrade.Where(x => x.Id == id).FirstOrDefault();
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
        // GET: MasterEmployerTypes/Details/5
        public ActionResult Details(string id)
        {
            string IdUrl = UrlEncryption.Decrypt(Convert.ToString(id));
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterEmployerType masterEmployerType = db.MasterEmployerType.Find(Convert.ToInt32(IdUrl));
            string encryptedId = UrlEncryption.EncryptURL(masterEmployerType.Id.ToString());
            ViewBag.EncryptedId = encryptedId;
            if (masterEmployerType == null)
            {
                return HttpNotFound();
            }
            return View(masterEmployerType);
        }

        // GET: MasterEmployerTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterEmployerTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterEmployerType masterEmployerType)
        {
            var verify = db.MasterEmployerType.Where(x => x.Name == masterEmployerType.Name && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { TempData["error"] = "This Employer Type already exist.."; }
            else
            {
                if (ModelState.IsValid)
                {
                    masterEmployerType.IsActive = true;
                    db.MasterEmployerType.Add(masterEmployerType);
                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        TempData["success"] = "Record Saved Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "There is Some error. Please try again later";
                    }
                }
            }

            return View(masterEmployerType);
        }

        // GET: MasterEmployerTypes/Edit/5
        public ActionResult Edit(string id)
        {
            string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(id));
            int Idd = Convert.ToInt32(IdUrl);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!db.MasterEmployer.Where(x => x.IsActive == true && x.EmployerTypeID == Idd).Any())
            {
                MasterEmployerType masterEmployerType = db.MasterEmployerType.Find(Convert.ToInt32(IdUrl));
                if (masterEmployerType == null)
                {
                    return HttpNotFound();
                }
                return View(masterEmployerType);
            }
            else
            {
                TempData["error"] = "Details Can't be changed as already in used";
                return RedirectToAction("Index");
            }
        }

        // POST: MasterEmployerTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MasterEmployerTypeVM masterEmployerType)
        {
            string Id = UrlEncryption.Decrypt(masterEmployerType.Id);
            int id = Convert.ToInt32(Id);
            var MasterEmployerType = db.MasterEmployerType.FirstOrDefault(x => x.Id == id);
            var verify = db.MasterEmployerType.Where(x => x.Name == masterEmployerType.Name && x.Id != id && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { TempData["error"] = "This Employer Type already exist.."; }
            else
            {
                if (ModelState.IsValid)
                {
                    MasterEmployerType.Name = masterEmployerType.Name;
                    MasterEmployerType.RetirementAge = masterEmployerType.RetirementAge;
                    MasterEmployerType.RetirementAgeAfter2004 = masterEmployerType.RetirementAgeAfter2004;
                    MasterEmployerType.MinimumHiringAge = masterEmployerType.MinimumHiringAge;
                    MasterEmployerType.DependantTerminationAge = masterEmployerType.DependantTerminationAge;
                    MasterEmployerType.IsActive = true;
                    db.Entry(MasterEmployerType).State = EntityState.Modified;
                    int result = db.SaveChanges();
                    //DbContextHelper.dBsavechanges(db);
                    if (result > 0)
                    {
                        TempData["success"] = "Record Saved Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "There is Some error. Please try again later";
                    }
                }
            }
            return View(masterEmployerType);
        }

        // GET: MasterEmployerTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterEmployerType masterEmployerType = db.MasterEmployerType.Find(id);
            if (masterEmployerType == null)
            {
                return HttpNotFound();
            }
            return View(masterEmployerType);
        }

        // POST: MasterEmployerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterEmployerType masterEmployerType = db.MasterEmployerType.Find(id);
            db.MasterEmployerType.Remove(masterEmployerType);
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

        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var List = db.MasterEmployerType.Where(x => x.IsActive == true);
            IEnumerable<MasterEmployerType> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.Name.ToLower().Contains(param.sSearch.ToLower())
                   || c.RetirementAge.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.MinimumHiringAge.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.RetirementAgeAfter2004.ToString().ToLower().Contains(param.sSearch.ToLower())
                || c.DependantTerminationAge.ToString().ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MasterEmployerType, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name :
                                                                      sortColumnIndex == 1 ? c.RetirementAge + "" :
                                                                      //sortColumnIndex == 2 ? c.PFRate + "" :
                                                                      sortColumnIndex == 2 ? c.RetirementAgeAfter2004 + "" :
                                                                      sortColumnIndex == 3 ? c.MinimumHiringAge + "" :
                                                                      sortColumnIndex == 4 ? c.DependantTerminationAge + "" :
                                                                      sortColumnIndex == 5 ? c.IsActive + "" :
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
                         c.Name,
                         c.RetirementAge+"",
                         c.RetirementAgeAfter2004+"",
                         c.MinimumHiringAge+"",
                         c.DependantTerminationAge+"",
                         //c.PFRate+"",
										     c.IsActive + "",
                                          UrlEncryption.EncryptURL(Convert.ToString(c.Id)) + ""
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

    }
}
