
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App.Data;
using App.Data.Entities;
using App.Web.Models;
using App.Web.Filters;
using App.Web.Repository;
using App.Web.Helper;
using DocumentFormat.OpenXml.Office2010.Excel;
using App.Data.ViewModels;

namespace App.Web.Controllers
{
  [AuthorizeEx()]
  public class CountryController : BaseController
  {
    //private AppDbContext db = new AppDbContext();
    private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
    private AppDbContext db;

    public CountryController()
    {
      db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
    }

    // GET: Countries
    public ActionResult Index()
    { 

      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "Country" && x.ActionName == "Index")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel
                   {
                     RoleID = roleList.ToString(),
                     ModuleName = "Country",
                     ParentId = c.ParentId,
                     ModuleID = c.Id,
                     ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission,
                     AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion,
                     EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission,
                     DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission
                   }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.AddPermission = model.AddPermssion;
        ViewBag.EditPermission = model.EditPermission;
        ViewBag.ViewPermission = model.ViewPermission;
        ViewBag.DeletePermission = model.DeletePermission;

      }
      return View();
    }
    public ActionResult AjaxHandler(JQueryDataTableParamModel param)
    {
      var countryList = db.MasterCountry.Where(x => x.IsActive == true);
      IEnumerable<MasterCountry> filtered;


      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = countryList
           .Where(c => c.CountryCode.ToLower().Contains(param.sSearch.ToLower())
           || c.Name.ToLower().Contains(param.sSearch.ToLower())
           || c.ISDCode.ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = countryList;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<MasterCountry, string> orderingFunction = (c => sortColumnIndex == 1 ? c.CountryCode :
                                                            sortColumnIndex == 0 ? c.Name :
                                                            sortColumnIndex == 2 ? c.ISDCode :
                                                            sortColumnIndex == 3 ? c.IsActive + "" : "");


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
                         c.CountryCode,
                         c.Name,
                                         c.ISDCode,
                                         c.IsActive + "",
                 Helper.UrlEncryption.EncryptURL(Convert.ToString(c.Id)) + ""
                                     };

      return Json(
                                  new
                                  {
                                    sEcho = param.sEcho,
                                    iTotalRecords = countryList.Count(),
                                    iTotalDisplayRecords = filtered.Count(),
                                    aaData = result
                                  }, JsonRequestBehavior.AllowGet);
    }

    // GET: Countries/Details/5
    public async Task<ActionResult> Details(string id)
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

      //MasterCountry country = await db.MasterCountry.FindAsync(Convert.ToInt32(IdUrl));
      int countryDetailsId = Convert.ToInt32(IdUrl);
      MasterCountry country = db.MasterCountry.Where(x => x.IsActive == true && x.Id == countryDetailsId).FirstOrDefault();
      if (country == null)
      {
        //return HttpNotFound();
        return RedirectToAction("Index", "UnAuthorize");
      }

      string encryptedId = UrlEncryption.EncryptURL(country.Id.ToString());
      ViewBag.EncryptedId = encryptedId;


      /// ***** Code By Himanshu Rajput *****

      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "Country")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel
                   {
                     RoleID = roleList.ToString(),
                     ModuleName = "Country",
                     ParentId = c.ParentId,
                     ModuleID = c.Id,
                     ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission,
                     EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission,
                   }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.EditPermission = model.EditPermission;

      }


      return View(country);
    }

    // GET: Countries/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Countries/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(MasterCountry country)
    {
      if (db.MasterCountry.Where(x => (x.Name == country.Name || x.CountryCode == country.CountryCode || x.ISDCode == country.ISDCode) && x.IsActive == true).Any())
      {
        TempData["error"] = "Already exist..";
        return RedirectToAction("Index");
      }
      else
      {
        if (ModelState.IsValid)
        {
          country.IsActive = true;

          db.MasterCountry.Add(country);
          // LogModifications();
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

      return View(country);
    }

    private void LogModifications()
    {
      var modified = db.ChangeTracker.Entries()
          .Where(p => p.State == EntityState.Modified || p.State == EntityState.Added || p.State == EntityState.Unchanged)
          .ToList();
      var dt = db.ChangeTracker.Entries().FirstOrDefault().State;
      // Log the modifications for 'modified' entries here
    }
    // GET: Countries/Edit/5

    public async Task<ActionResult> Edit(string id)
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
      int countryEditId = Convert.ToInt32(IdUrl);
      MasterCountry country = db.MasterCountry.Where(x => x.IsActive == true && x.Id == countryEditId).FirstOrDefault();
      //var country = await db.MasterCountry.FindAsync(Convert.ToInt32(IdUrl));
      if (country == null)
      {
        //return HttpNotFound();
        return RedirectToAction("Index", "UnAuthorize");
      }
      return View(country);
    }

    // POST: Countries/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "Id,CountryCode,Name,ISDCode,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] CountryVM country)
    {
      string Id = UrlEncryption.Decrypt(country.Id);
      int id = Convert.ToInt32(Id);
      var Mastercountry = db.MasterCountry.FirstOrDefault(x => x.Id == id);
      if (db.MasterCountry.Where(x => (x.Name == country.Name || x.CountryCode == country.CountryCode || x.ISDCode == country.ISDCode) && x.Id != id && x.IsActive == true).Any())
      {
        TempData["error"] = "Already exist..";

        return RedirectToAction("Index");
      }
      else
      {
        if (ModelState.IsValid)
        {
          Mastercountry.Id = id;
          Mastercountry.ISDCode = country.ISDCode;
          Mastercountry.CountryCode = country.CountryCode;
          Mastercountry.Name = country.Name;
          Mastercountry.IsActive = true;
          db.Entry(Mastercountry).State = EntityState.Modified;
          int result = await db.SaveChangesAsync();
          //DbContextHelper.dBsavechanges(db);
          if (result > 0)
          {
            TempData["success"] = "Record Saved Successfully";
            //ViewBag.Success = "Success";
            return RedirectToAction("Index");
          }
          else
          {
            TempData["error"] = "There is Some error. Please try again later";
          }
        }
      }
      return View(country);
    }

    // GET: Countries/Delete/5
    public async Task<ActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterCountry country = await db.MasterCountry.FindAsync(id);
      if (country == null)
      {
        return HttpNotFound();
      }
      return View(country);
    }

    // POST: Countries/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(string id)
    {
      string Id = UrlEncryption.Decrypt(id);
      MasterCountry country = await db.MasterCountry.FindAsync(Convert.ToInt32(Id));
      db.MasterCountry.Remove(country);
      await db.SaveChangesAsync();
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
          MasterCountry entity = db.MasterCountry.Where(x => x.Id == id).FirstOrDefault();
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
