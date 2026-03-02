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
  public class PensionerTypeController : BaseController
  {
    //private AppDbContext db = new AppDbContext();
    private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
    private AppDbContext db;

    public PensionerTypeController()
    {
      db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
    }

    // GET: PensionerType
    public ActionResult Index()
    {
      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionerType" && x.ActionName == "Index")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Pension Type", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.AddPermission = model.AddPermssion;
        ViewBag.EditPermission = model.EditPermission;
        ViewBag.DeletePermission = model.DeletePermission;

      }
      return View(db.MasterPensionerType.ToList());
    }
    public ActionResult AjaxHandler(JQueryDataTableParamModel param)
    {
      var List = db.MasterPensionerType.Where(x => x.IsActive == true);
      IEnumerable<MasterPensionerType> filtered;


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

      Func<MasterPensionerType, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name :
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
    // GET: PensionerType/Details/5
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
      int pensionDetailsId = Convert.ToInt32(IdUrl);
      MasterPensionerType pansionType = db.MasterPensionerType.Where(x=>x.IsActive == true && x.Id == pensionDetailsId).FirstOrDefault();
      if (pansionType == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }
      string encryptedId = UrlEncryption.EncryptURL(pansionType.Id.ToString());
      ViewBag.EncryptedId = encryptedId;


      /// ***** Code By Himanshu Rajput *****

      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionerType")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel
                   {
                     RoleID = roleList.ToString(),
                     ModuleName = "PensionerType",
                     ParentId = c.ParentId,
                     ModuleID = c.Id,
                     ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission,
                     EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission,
                   }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.EditPermission = model.EditPermission;

      }

      return View(pansionType);
    }

    // GET: PensionerType/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: PensionerType/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterPensionerType pansionType)
    {
      var verify = db.MasterPensionerType.Where(x => x.Name == pansionType.Name && x.IsActive == true).FirstOrDefault();
      if (verify != null)
      {
        TempData["error"] = "This pansionType already exist..";
        return RedirectToAction("Index");
      }
      else
      {
        if (ModelState.IsValid)
        {
          pansionType.IsActive = true;
          db.MasterPensionerType.Add(pansionType);
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
      return View(pansionType);
    }

    // GET: PensionerType/Edit/5
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
      int pensionId = Convert.ToInt32(IdUrl);
      MasterPensionerType pansionType = db.MasterPensionerType.Where(x=>x.IsActive ==true&& x.Id == pensionId).FirstOrDefault();
      if (pansionType == null)
      {
        //return HttpNotFound();
        return RedirectToAction("Index", "UnAuthorize");
      }
      return View(pansionType);
    }

    // POST: PensionerType/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterPensionerTypeVM pansionType)
    {
      string Id = UrlEncryption.Decrypt(pansionType.Id);
      int id = Convert.ToInt32(Id);
      var MasterPensionerType = db.MasterPensionerType.FirstOrDefault(x => x.Id == id);
      var verify = db.MasterPensionerType.Where(x => x.Name == pansionType.Name && x.Id != id && x.IsActive == true).FirstOrDefault();
      if (verify != null)
      {
        TempData["error"] = "This pansionType already exist..";
        return RedirectToAction("Index");
      }
      else
      {
        if (ModelState.IsValid)
        {
          MasterPensionerType.IsActive = true;
          MasterPensionerType.Name = pansionType.Name;
          db.Entry(MasterPensionerType).State = EntityState.Modified;
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
      return View(pansionType);
    }

    // GET: PensionerType/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterPensionerType pansionType = db.MasterPensionerType.Find(id);
      if (pansionType == null)
      {
        return HttpNotFound();
      }
      return View(pansionType);
    }

    // POST: PensionerType/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      MasterPensionerType pansionType = db.MasterPensionerType.Find(id);
      db.MasterPensionerType.Remove(pansionType);
      db.SaveChanges();
      return RedirectToAction("Index");
    }
    public JsonResult RemoveAjax(string Id, string name)
    {
      int result = 0;
      string IdUrl = UrlEncryption.Decrypt(Id);
      int id = Convert.ToInt32(IdUrl);
      if (id != 0)
      {
        bool action = MasterDeleteRepo.AjaxDelete(id, name);
        if (action)
        {
          MasterPensionerType entity = db.MasterPensionerType.Where(x => x.Id == id).FirstOrDefault();
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
