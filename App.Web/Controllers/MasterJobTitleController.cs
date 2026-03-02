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

namespace App.Web.Views.ContributorPersonalDetails
{
    [AuthorizeEx()]
    public class MasterJobTitlesController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public MasterJobTitlesController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // GET: MasterJobTitles
        public ActionResult Index()
        {
            return View(db.MasterJobTitle.ToList());
        }
        public JsonResult Remove(int Id, string name)
        {
            int result = 0;
            if (Id != 0)
            {
                bool action = MasterDeleteRepo.AjaxDelete(Id, name);
                if (action)
                {
                    MasterJobTitle entity = db.MasterJobTitle.Where(x => x.Id == Id).FirstOrDefault();
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
        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var List = db.MasterJobTitle.Where(x => x.IsActive == true);
            IEnumerable<MasterJobTitle> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.Name.ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MasterJobTitle, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name :
                                                                                            sortColumnIndex == 1 ? c.IsActive + "" :
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
                                         c.IsActive + "",
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

        // GET: MasterJobTitles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterJobTitle masterJobTitle = db.MasterJobTitle.Find(id);
            if (masterJobTitle == null)
            {
                return HttpNotFound();
            }
            return View(masterJobTitle);
        }

        // GET: MasterJobTitles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterJobTitles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterJobTitle masterJobTitle)
        {
            var verify = db.MasterJobTitle.Where(x => x.Name == masterJobTitle.Name && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { TempData["error"] = "This Job Title already exist.."; }
            else
            {
                if (ModelState.IsValid)
                {
                    masterJobTitle.IsActive = true;
                    db.MasterJobTitle.Add(masterJobTitle);
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
            return View(masterJobTitle);
        }

        // GET: MasterJobTitles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterJobTitle masterJobTitle = db.MasterJobTitle.Find(id);
            if (masterJobTitle == null)
            {
                return HttpNotFound();
            }
            return View(masterJobTitle);
        }

        // POST: MasterJobTitles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterJobTitle masterJobTitle)
        {
            var verify = db.MasterJobTitle.Where(x => x.Name == masterJobTitle.Name && x.Id != masterJobTitle.Id && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { TempData["error"] = "This Job Title already exist.."; }
            else
            {
                if (ModelState.IsValid)
                {
                    masterJobTitle.IsActive = true;
                    db.Entry(masterJobTitle).State = EntityState.Modified;
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
            return View(masterJobTitle);
        }

        // GET: MasterJobTitles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterJobTitle masterJobTitle = db.MasterJobTitle.Find(id);
            if (masterJobTitle == null)
            {
                return HttpNotFound();
            }
            return View(masterJobTitle);
        }

        // POST: MasterJobTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterJobTitle masterJobTitle = db.MasterJobTitle.Find(id);
            db.MasterJobTitle.Remove(masterJobTitle);
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
