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
using System.Threading.Tasks;
using App.Web.Helper;
using App.Data.ViewModels;
using static App.Web.Helper.Helper;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace App.Web.Controllers
{
  [AuthorizeEx()]
  public class RegionController : BaseController
  {
    //private AppDbContext db = new AppDbContext();
    private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
    private AppDbContext db;

    public RegionController()
    {
      db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
    }

    // GET: Cities
    public ActionResult Index()
    {
      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "Region" && x.ActionName == "Index")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Region", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

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

      // code By Himanshu Rajput *** Region By Filter ***

      var regionGet = GetRegionName();

      var reginGetId = db.MasterRegion.Where(x => x.Name == regionGet).Select(x => x.Id).FirstOrDefault();

      var List = db.MasterRegion.Where(x => x.IsActive == true && x.Id == reginGetId);
      IEnumerable<MasterRegion> filtered;


      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List.Where(c => c.States.Name.ToLower().Contains(param.sSearch.ToLower())
           || c.Name.ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<MasterRegion, string> orderingFunction = (c => sortColumnIndex == 0 ? c.States.Name :
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
                         db.MasterState.FirstOrDefault(x => x.IsActive && x.Id == c.StateId)?.Name ?? "",
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
    // GET: City/Details/5
    public ActionResult Details(string id)
    {
      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(id));
      int Id = Convert.ToInt32(IdUrl);
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      var regionDistDetails = GetRegionName();
      var ReginDistDetailsId = db.MasterRegion.Where(x => x.Name == regionDistDetails).Select(x => x.Id).FirstOrDefault();
      MasterRegion Region = db.MasterRegion.Where(x => x.Id == Id && x.Id == ReginDistDetailsId).FirstOrDefault();
      if (Region == null)
      {
        //return HttpNotFound();
        return RedirectToAction("Index", "UnAuthorize");
      }
      
      string encryptedId = UrlEncryption.EncryptURL(Region.Id.ToString());
      ViewBag.EncryptedId = encryptedId;
      if (Region == null)
      {
        return HttpNotFound();
      }

      /// ***** Code By Himanshu Rajput *****

      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "Region")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel
                   {
                     RoleID = roleList.ToString(),
                     ModuleName = "Region",
                     ParentId = c.ParentId,
                     ModuleID = c.Id,
                     ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission,
                     EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission,
                   }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.EditPermission = model.EditPermission;

      }

      return View(Region);
    }

    // GET: City/Create
    public ActionResult Create()
    {
      ViewBag.StateId = new SelectList(db.MasterState.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
      return View();
    }

    // POST: City/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind(Include = "Id,Name,StateId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterRegion Region)
    {
      if (db.MasterRegion.Where(x => x.Name == Region.Name && x.IsActive == true).Any())
      {
        TempData["error"] = "This Region already exist..";
        return RedirectToAction("Index");
      }
      else
      {
        if (ModelState.IsValid)
        {
          Region.IsActive = true;
          db.MasterRegion.Add(Region);
          int result = await db.SaveChangesAsync();
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
      ViewBag.StateId = new SelectList(db.MasterState.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", Region.StateId);
      return View(Region);
    }

    // GET: City/Edit/5
    public ActionResult Edit(string id)
    {

      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(id));

      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterRegion Region = db.MasterRegion.Find(Convert.ToInt32(IdUrl));
      if (Region == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }

      var regionDistDetails = GetRegionName();
      if (regionDistDetails.Trim().ToUpper() != Region.Name.Trim().ToUpper())
      {
        return RedirectToAction("Index", "UnAuthorize");
      }

      ViewBag.StateId = new SelectList(db.MasterState.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", Region.StateId);
      return View(Region);
    }

    // POST: City/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "Id,Name,StateId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterRegionVM Region)
    {
      string Id = UrlEncryption.Decrypt(Region.Id);
      int id = Convert.ToInt32(Id);
      var Masterregion = db.MasterRegion.FirstOrDefault(x => x.Id == id);
      if (db.MasterRegion.Where(x => x.Name == Region.Name && x.Id != id && x.IsActive == true).Any())
      {
        TempData["error"] = "This Region already exist..";
        return RedirectToAction("Index");
      }
      else
      {
        if (ModelState.IsValid)
        {
          Masterregion.Name = Region.Name;
          Masterregion.StateId = Region.StateId;
          Masterregion.IsActive = true;
          db.Entry(Masterregion).State = EntityState.Modified;
          int result = await db.SaveChangesAsync();
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
      ViewBag.StateId = new SelectList(db.MasterState.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", Region.StateId);
      return View(Region);
    }

    // GET: City/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterRegion city = db.MasterRegion.Find(id);
      if (city == null)
      {
        return HttpNotFound();
      }
      return View(city);
    }

    // POST: City/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      MasterRegion Region = db.MasterRegion.Find(id);
      db.MasterRegion.Remove(Region);
      db.SaveChanges();
      return RedirectToAction("Index");
    }
    public JsonResult RemoveAjax(string Id, string name)
    {
      int result = 0;
      string IdUrl = UrlEncryption.Decrypt(Id);
      int idd = Convert.ToInt32(IdUrl);
      if (idd != 0)
      {
        bool action = MasterDeleteRepo.AjaxDelete(idd, name);
        if (action)
        {
          MasterRegion entity = db.MasterRegion.Where(x => x.Id == idd).FirstOrDefault();
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
