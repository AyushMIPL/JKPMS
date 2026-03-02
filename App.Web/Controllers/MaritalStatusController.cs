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
  public class MaritalStatusController : BaseController
  {
    //private AppDbContext db = new AppDbContext();
    private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
    private AppDbContext db;

    public MaritalStatusController()
    {
      db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
    }

    // GET: MaritalStatus
    public ActionResult Index()
    {
      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "MaritalStatus" && x.ActionName == "Index")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "MaritalStatus", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.AddPermission = model.AddPermssion;
        ViewBag.EditPermission = model.EditPermission;
        ViewBag.DeletePermission = model.DeletePermission;

      }
      return View(db.MasterMaritalStatus.ToList());
    }
    public ActionResult AjaxHandler(JQueryDataTableParamModel param)
    {
      var List = db.MasterMaritalStatus.Where(x => x.IsActive == true);
      IEnumerable<MasterMaritalStatus> filtered;


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

      Func<MasterMaritalStatus, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name :
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
    // GET: MaritalStatus/Details/5
    public ActionResult Details(string id)
    {
      if (id == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }
      string IdUrl = UrlEncryption.Decrypt(Convert.ToString(id));
      if (string.IsNullOrEmpty(IdUrl) || IdUrl == "Error")
      {
        return RedirectToAction("Index", "UnAuthorize");
        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      int maritalDetailsId = Convert.ToInt32(IdUrl);
      MasterMaritalStatus maritalStatus = db.MasterMaritalStatus.Where(x => x.IsActive == true && x.Id == maritalDetailsId).FirstOrDefault();
      if (maritalStatus == null)
      {
        // return HttpNotFound();
        return RedirectToAction("Index", "UnAuthorize");

      }
      string encryptedId = UrlEncryption.EncryptURL(maritalStatus.Id.ToString());
      ViewBag.EncryptedId = encryptedId;


      /// ***** Code By Himanshu Rajput *****

      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "MaritalStatus")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel
                   {
                     RoleID = roleList.ToString(),
                     ModuleName = "MaritalStatus",
                     ParentId = c.ParentId,
                     ModuleID = c.Id,
                     ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission,
                     EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission,
                   }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.EditPermission = model.EditPermission;

      }

      return View(maritalStatus);
    }

    // GET: MaritalStatus/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: MaritalStatus/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterMaritalStatus maritalStatus)
    {
      var verify = db.MasterMaritalStatus.Where(x => x.Name == maritalStatus.Name && x.IsActive == true).FirstOrDefault();
      if (verify != null)
      {
        TempData["error"] = "This Marital Status already exist..";
        return RedirectToAction("Index");
      }
      else
      {
        if (ModelState.IsValid)
        {
          maritalStatus.IsActive = true;
          db.MasterMaritalStatus.Add(maritalStatus);
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

      return View(maritalStatus);
    }

    // GET: MaritalStatus/Edit/5
    public ActionResult Edit(string id)
    {
      if (id == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }
      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(id));
      if (string.IsNullOrEmpty(IdUrl) || IdUrl == "Error")
      {
        return RedirectToAction("Index", "UnAuthorize");
        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      int maritalId = Convert.ToInt32(IdUrl);
      MasterMaritalStatus maritalStatus = db.MasterMaritalStatus.Where(x => x.IsActive == true && x.Id == maritalId).FirstOrDefault();
      if (maritalStatus == null)
      {
        //return HttpNotFound();
        return RedirectToAction("Index", "UnAuthorize");
      }
      return View(maritalStatus);
    }

    // POST: MaritalStatus/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterMaritalStatusVM maritalStatus)
    {
      string Id = UrlEncryption.Decrypt(maritalStatus.Id);
      int id = Convert.ToInt32(Id);
      var MasterMaritalStatus = db.MasterMaritalStatus.FirstOrDefault(x => x.Id == id);
      var verify = db.MasterMaritalStatus.Where(x => x.Name == maritalStatus.Name && x.Id != id && x.IsActive == true).FirstOrDefault();
      if (verify != null)
      {
        TempData["error"] = "This Marital Status already exist..";
        return RedirectToAction("Index");
      }
      else
      {
        if (ModelState.IsValid)
        {
          MasterMaritalStatus.IsActive = true;
          MasterMaritalStatus.Name = maritalStatus.Name;
          db.Entry(MasterMaritalStatus).State = EntityState.Modified;
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
      return View(maritalStatus);
    }

    // GET: MaritalStatus/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterMaritalStatus maritalStatus = db.MasterMaritalStatus.Find(id);
      if (maritalStatus == null)
      {
        return HttpNotFound();
      }
      return View(maritalStatus);
    }

    // POST: MaritalStatus/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      MasterMaritalStatus maritalStatus = db.MasterMaritalStatus.Find(id);
      db.MasterMaritalStatus.Remove(maritalStatus);
      db.SaveChanges();
      return RedirectToAction("Index");
    }
    public JsonResult Remove(string Id, string name)
    {
      int result = 0;
      string IdUrl = UrlEncryption.Decrypt(Id);
      int id = Convert.ToInt32(IdUrl);
      if (id != 0)
      {
        bool action = MasterDeleteRepo.AjaxDelete(id, name);
        if (action)
        {
          MasterMaritalStatus entity = db.MasterMaritalStatus.Where(x => x.Id == id).FirstOrDefault();
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
