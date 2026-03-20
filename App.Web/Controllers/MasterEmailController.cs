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
    public class MasterEmailController : Controller
    {
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public MasterEmailController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // GET: MasterEmail
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var List = db.MasterEmails.Where(x => x.IsActive == true);
            IEnumerable<MasterEmail> filtered;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List.Where(c => c.EmailAddress.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MasterEmail, string> orderingFunction = (c => sortColumnIndex == 0 ? c.EmailAddress :
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
                             c.EmailAddress,
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

        // GET: MasterEmail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterEmail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmailAddress,IsActive")] MasterEmail masterEmail)
        {
            var verify = db.MasterEmails.Where(x => x.EmailAddress == masterEmail.EmailAddress && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { 
                TempData["error"] = "This email already exist."; 
            }
            else
            {
                if (ModelState.IsValid)
                {
                    masterEmail.IsActive = true;
                    db.MasterEmails.Add(masterEmail);
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

            return View(masterEmail);
        }

        // GET: MasterEmail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterEmail masterEmail = db.MasterEmails.Find(id);
            if (masterEmail == null)
            {
                return HttpNotFound();
            }
            return View(masterEmail);
        }

        // POST: MasterEmail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmailAddress,IsActive")] MasterEmail masterEmail)
        {
            var verify = db.MasterEmails.Where(x => x.EmailAddress == masterEmail.EmailAddress && x.Id != masterEmail.Id && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { 
                TempData["error"] = "This email already exist."; 
            }
            else
            {
                if (ModelState.IsValid)
                {
                    masterEmail.IsActive = true;
                    db.Entry(masterEmail).State = EntityState.Modified;
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
            return View(masterEmail);
        }

        public JsonResult Remove(int Id)
        {
            int result = 0;
            if (Id != 0)
            {
                MasterEmail entity = db.MasterEmails.Where(x => x.Id == Id).FirstOrDefault();
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
