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
    public class NationalityController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public NationalityController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // GET: Nationalities
        public ActionResult Index()
        {
            return View(db.MasterNationality.ToList());
        }
        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var List = db.MasterNationality.Where(x => x.IsActive == true);
            IEnumerable<MasterNationality> filtered;


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

            Func<MasterNationality, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name :
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

        // GET: Nationalities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterNationality nationality = db.MasterNationality.Find(id);
            if (nationality == null)
            {
                return HttpNotFound();
            }
            return View(nationality);
        }

        // GET: Nationalities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nationalities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterNationality nationality)
        {
            var verify = db.MasterNationality.Where(x => x.Name == nationality.Name && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { TempData["error"] = "This nationality already exist.."; }
            else
            {
                if (ModelState.IsValid)
                {
                    nationality.IsActive = true;
                    db.MasterNationality.Add(nationality);
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

            return View(nationality);
        }

        // GET: Nationalities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterNationality nationality = db.MasterNationality.Find(id);
            if (nationality == null)
            {
                return HttpNotFound();
            }
            return View(nationality);
        }

        // POST: Nationalities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterNationality nationality)
        {
            var verify = db.MasterNationality.Where(x => x.Name == nationality.Name && x.Id != nationality.Id && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { TempData["error"] = "This nationality already exist.."; }
            else
            {
                if (ModelState.IsValid)
                {
                    nationality.IsActive = true;
                    db.Entry(nationality).State = EntityState.Modified;
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
            return View(nationality);
        }

        // GET: Nationalities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterNationality nationality = db.MasterNationality.Find(id);
            if (nationality == null)
            {
                return HttpNotFound();
            }
            return View(nationality);
        }

        // POST: Nationalities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterNationality nationality = db.MasterNationality.Find(id);
            db.MasterNationality.Remove(nationality);
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
                    MasterNationality entity = db.MasterNationality.Where(x => x.Id == Id).FirstOrDefault();
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
