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
  public class DepartmentController : Controller
  {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public DepartmentController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // GET: Department
        public ActionResult Index()
    {
      return View();
    }
    public ActionResult AjaxHandler(JQueryDataTableParamModel param)
    {
      var List = db.MasterDepartment.Where(x => x.IsActive == true);
      IEnumerable<MasterDepartment> filtered;


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

      Func<MasterDepartment, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name :
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
    // GET: Department/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterDepartment department = db.MasterDepartment.Find(id);
      if (department == null)
      {
        return HttpNotFound();
      }
      return View(department);
    }

    // GET: Department/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Department/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterDepartment department)
    {
      if (db.MasterDepartment.Where(x => x.Name == department.Name && x.IsActive == true).Any())
      { TempData["error"] = "This Department already exist.."; }
      else
      {
        if (ModelState.IsValid)
        {
          department.IsActive = true;
          db.MasterDepartment.Add(department);
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

      return View(department);
    }

    // GET: Department/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterDepartment department = db.MasterDepartment.Find(id);
      if (department == null)
      {
        return HttpNotFound();
      }
      return View(department);
    }

    // POST: Department/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterDepartment department)
    {
      if (db.MasterDepartment.Where(x => x.Name == department.Name && x.Id != department.Id && x.IsActive == true).Any())
      { TempData["error"] = "This Department already exist.."; }
      else
      {
        if (ModelState.IsValid)
        {
          department.IsActive = true;
          db.Entry(department).State = EntityState.Modified;
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
      return View(department);
    }

    // GET: Department/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterDepartment department = db.MasterDepartment.Find(id);
      if (department == null)
      {
        return HttpNotFound();
      }
      return View(department);
    }

    // POST: Department/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      MasterDepartment department = db.MasterDepartment.Find(id);
      db.MasterDepartment.Remove(department);
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
          MasterDepartment entity = db.MasterDepartment.Where(x => x.Id == Id).FirstOrDefault();
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
