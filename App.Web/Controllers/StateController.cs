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
  public class StateController : BaseController
  {
    //private AppDbContext db = new AppDbContext();
    private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
    private AppDbContext db;

    public StateController()
    {
      db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
    }

    // GET: States
    public ActionResult Index()
    {
      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "State" && x.ActionName == "Index")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "State", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.AddPermission = model.AddPermssion;
        ViewBag.EditPermission = model.EditPermission;
        ViewBag.DeletePermission = model.DeletePermission;

      }
      return View();
    }
    public ActionResult AjaxHandler(JQueryDataTableParamModel param)
    {
      var List = db.MasterState.Where(x => x.IsActive == true);
      IEnumerable<MasterState> filtered;


      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => c.Country.Name.ToLower().Contains(param.sSearch.ToLower())
           || c.Name.ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<MasterState, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Country.Name :
                                                                            sortColumnIndex == 1 ? c.Name :
                                                                                      sortColumnIndex == 2 ? c.IsActive + "" :
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
                         c.Country.CountryCode,
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
    // GET: States/Details/5
    public ActionResult Details(string id)
    {
      if(id == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }
      string IdUrl = UrlEncryption.Decrypt(Convert.ToString(id));
      if (string.IsNullOrEmpty(IdUrl) || IdUrl == "Error")
      {
        return RedirectToAction("Index", "UnAuthorize");
        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      int stateId = Convert.ToInt32(IdUrl);
      MasterState state = db.MasterState.Where(x => x.IsActive == true && x.Id == stateId).FirstOrDefault();
      if (state == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
        //return HttpNotFound();
      }

      string encryptedId = UrlEncryption.EncryptURL(state.Id.ToString());
      ViewBag.EncryptedId = encryptedId;


      /// ***** Code By Himanshu Rajput *****

      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "State")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel
                   {
                     RoleID = roleList.ToString(),
                     ModuleName = "State",
                     ParentId = c.ParentId,
                     ModuleID = c.Id,
                     ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission,
                     EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission,
                   }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.EditPermission = model.EditPermission;

      }


      return View(state);
    }

    // GET: States/Create
    public ActionResult Create()
    {

      ViewBag.CountryId = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
      return View();
    }

    // POST: States/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,Name,CountryId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterState state)
    {
      var verify = db.MasterState.Where(x => x.Name == state.Name && x.IsActive == true).FirstOrDefault();
      if (verify != null)
      {
        TempData["error"] = "This state already exist..";
        return RedirectToAction("Index");
      }

      else
      {
        if (ModelState.IsValid)
        {
          state.IsActive = true;
          db.MasterState.Add(state);
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
      ViewBag.CountryId = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", state.CountryId);
      return View(state);
    }

    // GET: States/Edit/5
    public ActionResult Edit(string id)
    {
      if(id == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }
      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(id));
      if (string.IsNullOrEmpty(IdUrl) || IdUrl == "Error")
      {
        return RedirectToAction("Index", "UnAuthorize");
        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterState state = db.MasterState.Find(Convert.ToInt32(IdUrl));
      if (state == null)
      {
        //return HttpNotFound();
        return RedirectToAction("Index", "UnAuthorize");
      }
      ViewBag.CountryId = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", state.CountryId);
      return View(state);
    }

    // POST: States/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Name,CountryId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterStateVM state)
    {
      string Id = UrlEncryption.Decrypt(state.Id);
      int id = Convert.ToInt32(Id);
      var Masterstate = db.MasterState.FirstOrDefault(x => x.Id == id);
      var verify = db.MasterState.Where(x => x.Name == state.Name && x.Id != id && x.IsActive == true).FirstOrDefault();
      if (verify != null)
      {
        TempData["error"] = "This state already exist..";
        return RedirectToAction("Index");
      }

      else
      {
        if (ModelState.IsValid)
        {
          Masterstate.CountryId = state.CountryId;
          Masterstate.Name = state.Name;
          Masterstate.IsActive = true;
          db.Entry(Masterstate).State = EntityState.Modified;
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
      ViewBag.CountryId = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", state.CountryId);
      return View(state);
    }

    // GET: States/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterState state = db.MasterState.Find(id);
      if (state == null)
      {
        return HttpNotFound();
      }
      return View(state);
    }

    // POST: States/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      MasterState state = db.MasterState.Find(id);
      db.MasterState.Remove(state);
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
          MasterState entity = db.MasterState.Where(x => x.Id == id).FirstOrDefault();
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
