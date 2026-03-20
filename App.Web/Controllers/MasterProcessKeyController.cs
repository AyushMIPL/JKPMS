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
    //[AuthorizeEx()]
    public class MasterProcessKeyController : Controller
    {
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public MasterProcessKeyController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // GET: MasterProcessKey
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var List = db.MasterProcessKeys.Where(x => x.IsActive == true);
            IEnumerable<MasterProcessKey> filtered;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List.Where(c => c.ProcessKeyName.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MasterProcessKey, string> orderingFunction = (c => sortColumnIndex == 0 ? c.ProcessKeyName :
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
                             c.ProcessKeyName,
                             c.IsActive + "",
                             c.Id + ""
                         };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = List.Count(),
                iTotalDisplayRecords = filtered.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: MasterProcessKey/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterProcessKey/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProcessKeyName,IsActive")] MasterProcessKey masterProcessKey)
        {
            var verify = db.MasterProcessKeys.Where(x => x.ProcessKeyName == masterProcessKey.ProcessKeyName && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { 
                TempData["error"] = "This process key already exist."; 
            }
            else
            {
                if (ModelState.IsValid)
                {
                    masterProcessKey.IsActive = true;
                    db.MasterProcessKeys.Add(masterProcessKey);
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

            return View(masterProcessKey);
        }

        // GET: MasterProcessKey/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterProcessKey masterProcessKey = db.MasterProcessKeys.Find(id);
            if (masterProcessKey == null)
            {
                return HttpNotFound();
            }
            return View(masterProcessKey);
        }

        // POST: MasterProcessKey/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProcessKeyName,IsActive")] MasterProcessKey masterProcessKey)
        {
            var verify = db.MasterProcessKeys.Where(x => x.ProcessKeyName == masterProcessKey.ProcessKeyName && x.Id != masterProcessKey.Id && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { 
                TempData["error"] = "This process key already exist."; 
            }
            else
            {
                if (ModelState.IsValid)
                {
                    masterProcessKey.IsActive = true;
                    db.Entry(masterProcessKey).State = EntityState.Modified;
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
            return View(masterProcessKey);
        }

        public JsonResult Remove(int Id)
        {
            int result = 0;
            if (Id != 0)
            {
                MasterProcessKey entity = db.MasterProcessKeys.Where(x => x.Id == Id).FirstOrDefault();
                if (entity != null)
                {
                    entity.IsActive = false;
                    db.Entry(entity).State = EntityState.Modified;
                    result = db.SaveChanges();
                    if(result > 0) return Json(1, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(0, JsonRequestBehavior.AllowGet);
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
