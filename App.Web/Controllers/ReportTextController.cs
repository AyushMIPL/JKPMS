using App.Data;
using App.Data.Entities;
using App.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    public class ReportTextController : BaseController
    {
        //
        // GET: /ReportText/
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;
        private AppDbContext _DbContext;

        public ReportTextController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _DbContext = DbContext;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var List = db.ReportText.OrderBy(x => x.ModuleId);
            IEnumerable<ReportText> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => (c.Field + "").ToLower().Contains(param.sSearch.ToLower())
                   || (c.Signature + "").ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<ReportText, string> orderingFunction = (c => sortColumnIndex == 0 ? c.ModuleId + "" :
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
                         (c.ModuleId==1?"Pension":c.ModuleId==2?"Pension for Police":"Refund"),
                                         c.Field + "",
                                         c.Signature + "",
                                         c.Id+""
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

        // GET: ReportText/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportText reportText = db.ReportText.Find(id);
            if (reportText == null)
            {
                return HttpNotFound();
            }
            return View(reportText);
        }

        // GET: ReportText/Create
        public ActionResult Create()
        {
            var module = new[] { new { Id = "1", Name = "Pension" }, new { Id = "2", Name = "Pension for Police" }, new { Id = "3", Name = "Refund" } };
            ViewBag.ModuleId = new SelectList(module, "Id", "Name");
            return View();
        }

        // POST: ReportText/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReportText reportText)
        {
            if (ModelState.IsValid)
            {
                db.ReportText.Add(reportText);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    TempData["success"] = "Record Saved Successfully";
                    return RedirectToAction("Index");
                }
            }
            var module = new[] { new { Id = "1", Name = "Pension" }, new { Id = "2", Name = "Pension for Police" }, new { Id = "3", Name = "Refund" } };
            ViewBag.ModuleId = new SelectList(module, "Id", "Name", reportText.ModuleId);
            return View(reportText);
        }

        // GET: ReportText/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportText reportText = db.ReportText.Find(id);
            if (reportText == null)
            {
                return HttpNotFound();
            }
            var module = new[] { new { Id = "1", Name = "Pension" }, new { Id = "2", Name = "Pension for Police" }, new { Id = "3", Name = "Refund" } };
            ViewBag.ModuleId = new SelectList(module, "Id", "Name", reportText.ModuleId);
            return View(reportText);
        }

        // POST: ReportText/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReportText reportText)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportText).State = EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    TempData["success"] = "Record Saved Successfully";
                    return RedirectToAction("Index");
                }
            }
            var module = new[] { new { Id = "1", Name = "Pension" }, new { Id = "2", Name = "Pension for Police" }, new { Id = "3", Name = "Refund" } };
            ViewBag.ModuleId = new SelectList(module, "Id", "Name", reportText.ModuleId);
            return View(reportText);
        }

        // GET: ReportText/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportText reportText = db.ReportText.Find(id);
            if (reportText == null)
            {
                return HttpNotFound();
            }
            return View(reportText);
        }

        // POST: ReportText/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReportText reportText = db.ReportText.Find(id);
            db.ReportText.Remove(reportText);
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