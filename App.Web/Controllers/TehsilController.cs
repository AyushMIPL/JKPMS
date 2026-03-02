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
using System.Drawing;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace App.Web.Controllers
{
  [AuthorizeEx()]
  public class TehsilController : BaseController
  {
    //private AppDbContext db = new AppDbContext();
    private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
    private AppDbContext db;

    public TehsilController()
    {
      db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
    }

    // GET: Cities
    public ActionResult Index()
    {
      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "Tehsil" && x.ActionName == "Index")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Tehsil", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

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

      var ReginGetId = db.MasterRegion.Where(x => x.Name == regionGet).Select(x => x.Id).FirstOrDefault();

      var distList = db.MasterDistrict.Where(x => x.IsActive == true && x.RegionId == ReginGetId).Select(x => x.Id).ToList();

      var List = db.MasterTehsil.Where(x => x.IsActive == true && distList.Contains(x.DistrictId)).ToList();
      IEnumerable<MasterTehsil> filtered;





      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List.Where(c => c.District.Name.ToLower().Contains(param.sSearch.ToLower())
           || c.Name.ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<MasterTehsil, string> orderingFunction = (c => sortColumnIndex == 0 ? c.District.Name :
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
                         db.MasterDistrict.FirstOrDefault(x=>x.IsActive && x.Id==c.DistrictId)?.Name ??"",
                         c.Name,
                                         c.IsActive + "",
                        UrlEncryption.EncryptURL(Convert.ToString(c.Id)) +""
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

    public async Task<bool> IsTehsilValidForCurrentRegion(int districtId, string currentRegion)
    {
      bool isValid = false;
      try
      {
        var RegionId = await db.MasterDistrict.Where(x => x.IsActive && x.Id == districtId).Select(x => x.RegionId).FirstOrDefaultAsync();

        var regionOfTehsil = await db.MasterRegion.Where(x => x.IsActive && x.Id == RegionId).Select(x => x.Name).FirstOrDefaultAsync();

        if (!string.IsNullOrEmpty(regionOfTehsil) && !string.IsNullOrEmpty(currentRegion) && (regionOfTehsil.Trim().ToUpper() == currentRegion.Trim().ToUpper()))
        {
          isValid = true;
        }
      }
      catch
      {
      }

      return isValid;
    }

    // GET: City/Details/5
    public async Task<ActionResult> Details(string id)
    {
      if (id == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }
      string IdUrl = Helper.UrlEncryption.Decrypt(id);
      if (string.IsNullOrEmpty(IdUrl) || IdUrl == "Error")
      {
        return RedirectToAction("Index", "UnAuthorize");
      }

      int tehsilId = Convert.ToInt32(IdUrl);

      MasterTehsil Tehsil = await db.MasterTehsil.Where(x => x.IsActive && x.Id == tehsilId).FirstOrDefaultAsync();
      if (Tehsil == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }

      string region = GetRegionName();

      bool isValidTehsil = await IsTehsilValidForCurrentRegion(Tehsil.DistrictId, region);

      if (!isValidTehsil)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }

      ViewBag.EncryptedId = id;

      return View(Tehsil);
    }

    // GET: City/Create
    public ActionResult Create()
    {
      // code By Himanshu Rajput *** Region By Filter ***

      var regionCreate = GetRegionName();

      var regionCreateId = db.MasterRegion.Where(x => x.Name == regionCreate).Select(x => x.Id).FirstOrDefault();

      ViewBag.DistrictId = new SelectList(db.MasterDistrict.Where(x => x.IsActive == true && x.RegionId == regionCreateId).OrderBy(x => x.Name), "Id", "Name");
      return View();
    }

    // POST: City/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind(Include = "Id,Name,DistrictId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterTehsil Tehsil)
    {
      if (db.MasterTehsil.Where(x => x.Name == Tehsil.Name && x.IsActive == true).Any())
      {
        TempData["error"] = "This Tehsil already exist..";
        return RedirectToAction("Index");
      }
      else
      {
        if (ModelState.IsValid)
        {
          Tehsil.IsActive = true;
          db.MasterTehsil.Add(Tehsil);
          int result = await db.SaveChangesAsync();
          if (result > 0)
          {
            TempData["success"] = "Record Saved Successfully";
            return RedirectToAction("Index");
          }
          else
          {
            TempData["error"] = "There is Some error. Please try again later";
            return RedirectToAction("Index");
          }
        }
      }
      //ViewBag.DistrictId = new SelectList(db.MasterDistrict.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", Tehsil.DistrictId);
      var regionCreate = GetRegionName();

      var regionCreateId = db.MasterRegion.Where(x => x.Name == regionCreate).Select(x => x.Id).FirstOrDefault();

      ViewBag.DistrictId = new SelectList(db.MasterDistrict.Where(x => x.IsActive == true && x.RegionId == regionCreateId).OrderBy(x => x.Name), "Id", "Name", Tehsil.DistrictId);
      return View(Tehsil);
    }

    // GET: City/Edit/5
    public async Task<ActionResult> Edit(string id)
    {
      if (id == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }
      string IdUrl = Helper.UrlEncryption.Decrypt(id);
      if (string.IsNullOrEmpty(IdUrl) || IdUrl == "Error")
      {
        return RedirectToAction("Index", "UnAuthorize");
      }

      int tehsilId = Convert.ToInt32(IdUrl);

      MasterTehsil Tehsil = await db.MasterTehsil.Where(x => x.IsActive && x.Id == tehsilId).FirstOrDefaultAsync();
      if (Tehsil == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }

      string region = GetRegionName();

      bool isValidTehsil = await IsTehsilValidForCurrentRegion(Tehsil.DistrictId, region);

      if (!isValidTehsil)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }

      var reginEditId = db.MasterRegion.Where(x => x.Name == region).Select(x => x.Id).FirstOrDefault();

      ViewBag.DistrictId = new SelectList(db.MasterDistrict.Where(x => x.IsActive == true && x.RegionId == reginEditId).OrderBy(x => x.Name), "Id", "Name", Tehsil.DistrictId);
      return View(Tehsil);
    }

    // POST: City/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "Id,Name,DistrictId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterTehsilVM Tehsil)
    {
      string Id = UrlEncryption.Decrypt(Tehsil.Id);
      int id = Convert.ToInt32(Id);
      var Masterregion = db.MasterTehsil.FirstOrDefault(x => x.Id == id);
      Masterregion.Name = Tehsil.Name;
      Masterregion.DistrictId = Tehsil.DistrictId;
      if (db.MasterTehsil.Where(x => x.Name == Tehsil.Name && x.Id != id && x.IsActive == true).Any())
      {
        TempData["error"] = "This Tehsil already exist..";
        return RedirectToAction("Index");
      }
      else
      {
        if (ModelState.IsValid)
        {
          Masterregion.Name = Tehsil.Name;
          Masterregion.DistrictId = Tehsil.DistrictId;
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
            return RedirectToAction("Index");
          }
        }
      }
      var regionCreate = GetRegionName();

      var regionCreateId = db.MasterRegion.Where(x => x.Name == regionCreate).Select(x => x.Id).FirstOrDefault();

      ViewBag.DistrictId = new SelectList(db.MasterDistrict.Where(x => x.IsActive == true && x.RegionId == regionCreateId).OrderBy(x => x.Name), "Id", "Name", Tehsil.DistrictId);


      //ViewBag.DistrictId = new SelectList(db.MasterDistrict.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", Tehsil.DistrictId);
      //return View(Tehsil);
      return View(Masterregion);
    }

    // GET: City/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterTehsil city = db.MasterTehsil.Find(id);
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
      MasterTehsil Region = db.MasterTehsil.Find(id);
      db.MasterTehsil.Remove(Region);
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
          MasterTehsil entity = db.MasterTehsil.Where(x => x.Id == idd).FirstOrDefault();
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
