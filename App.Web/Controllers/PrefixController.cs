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
    public class PrefixController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public PrefixController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // GET: Prefixes
        public ActionResult Index()
        {
            return View(db.MasterPrefix.ToList());
        }

        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var List = db.MasterPrefix.Where(x => x.IsActive == true);
            IEnumerable<MasterPrefix> filtered;


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

            Func<MasterPrefix, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name :
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
        // GET: Prefixes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterPrefix prefix = db.MasterPrefix.Find(id);
            if (prefix == null)
            {
                return HttpNotFound();
            }
            return View(prefix);
        }

        // GET: Prefixes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prefixes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterPrefix prefix)
        {
            var verify = db.MasterPrefix.Where(x => x.Name == prefix.Name && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { TempData["error"] = "This prefix already exist.."; }
            else
            {
                if (ModelState.IsValid)
                {
                    prefix.IsActive = true;
                    db.MasterPrefix.Add(prefix);
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

            return View(prefix);
        }

        // GET: Prefixes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterPrefix prefix = db.MasterPrefix.Find(id);
            if (prefix == null)
            {
                return HttpNotFound();
            }
            return View(prefix);
        }

        // POST: Prefixes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterPrefix prefix)
        {
            var verify = db.MasterPrefix.Where(x => x.Name == prefix.Name && x.Id != prefix.Id && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { TempData["error"] = "This prefix already exist.."; }
            else
            {
                if (ModelState.IsValid)
                {
                    prefix.IsActive = true;
                    db.Entry(prefix).State = EntityState.Modified;
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
            return View(prefix);
        }

        // GET: Prefixes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterPrefix prefix = db.MasterPrefix.Find(id);
            if (prefix == null)
            {
                return HttpNotFound();
            }
            return View(prefix);
        }

        // POST: Prefixes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterPrefix prefix = db.MasterPrefix.Find(id);
            db.MasterPrefix.Remove(prefix);
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
                    MasterPrefix entity = db.MasterPrefix.Where(x => x.Id == Id).FirstOrDefault();
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
