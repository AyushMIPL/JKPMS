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
namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class SuffixController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public SuffixController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // GET: Suffixes
        public ActionResult Index()
        {
            return View(db.MasterSuffix.ToList());
        }

        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var List = db.MasterSuffix.Where(x => x.IsActive == true);
            IEnumerable<MasterSuffix> filtered;


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

            Func<MasterSuffix, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name :
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

        // GET: Suffixes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterSuffix suffix = db.MasterSuffix.Find(id);
            if (suffix == null)
            {
                return HttpNotFound();
            }
            return View(suffix);
        }

        // GET: Suffixes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suffixes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterSuffix suffix)
        {
            var verify = db.MasterSuffix.Where(x => x.Name == suffix.Name && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { TempData["error"] = "This suffix already exist.."; }
            else
            {
                if (ModelState.IsValid)
                {
                    suffix.IsActive = true;
                    db.MasterSuffix.Add(suffix);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(suffix);
        }

        // GET: Suffixes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterSuffix suffix = db.MasterSuffix.Find(id);
            if (suffix == null)
            {
                return HttpNotFound();
            }
            return View(suffix);
        }

        // POST: Suffixes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterSuffix suffix)
        {
            var verify = db.MasterSuffix.Where(x => x.Name == suffix.Name && x.Id != suffix.Id && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { TempData["error"] = "This suffix already exist.."; }
            else
            {
                if (ModelState.IsValid)
                {
                    suffix.IsActive = true;
                    db.Entry(suffix).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(suffix);
        }

        // GET: Suffixes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterSuffix suffix = db.MasterSuffix.Find(id);
            if (suffix == null)
            {
                return HttpNotFound();
            }
            return View(suffix);
        }

        // POST: Suffixes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterSuffix suffix = db.MasterSuffix.Find(id);
            db.MasterSuffix.Remove(suffix);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult Remove(int Id, string name)
        {
            int result = 0;
            if (Id != 0)
            {
                bool action = MasterDeleteRepo.AjaxDelete(Id, name);
                if (action)
                {
                    MasterSuffix entity = db.MasterSuffix.Where(x => x.Id == Id).FirstOrDefault();
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
