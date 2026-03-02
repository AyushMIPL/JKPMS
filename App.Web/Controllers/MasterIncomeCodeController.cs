using App.Data;
using App.Data.Entities;
using App.Data.ViewModels;
using App.Web.Filters;
using App.Web.Helper;
using App.Web.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
  [AuthorizeEx()]
  public class MasterIncomeCodeController : BaseController
  {
    //private AppDbContext db = new AppDbContext();
    private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
    private AppDbContext db;

    public MasterIncomeCodeController()
    {
      db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
    }
    // GET: MasterIncomeCode
    public ActionResult Index()
    {
      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "MasterIncomeCode" && x.ActionName == "Index")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Income Code", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.AddPermission = model.AddPermssion;
        ViewBag.EditPermission = model.EditPermission;
      }
      return View();
    }
    public ActionResult AjaxHandler(JQueryDataTableParamModel param)
    {
      var incType = from x in incomeType() select new { Id = x.Value, Name = x.Key };
      List<MasterIncCodes> List = db.MasterIncCodes.ToList();
      IEnumerable<MasterIncCodes> filtered;


      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => c.inc_code.ToLower().Contains(param.sSearch.ToLower())
           || c.description.ToLower().Contains(param.sSearch.ToLower())
           || c.inc_type.ToLower().Contains(param.sSearch.ToLower()));
      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<MasterIncCodes, string> orderingFunction = (c => sortColumnIndex == 0 ? c.inc_code :
                                                                                      sortColumnIndex == 1 ? c.description :
                                                                                      sortColumnIndex == 2 ? c.inc_type :
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

                         c.inc_code,
                     c.description,
                   c.inc_type,
                   //  incType.Where(x=>x.Id+""==c.inc_type).Select(x=>x.Name).FirstOrDefault(),
               UrlEncryption.EncryptURL(Convert.ToString(c.inc_code_id))+""
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
    [HttpGet]
    public ActionResult Create()
    {
      var incType = from x in incomeType() select new { Id = x.Value, Name = x.Key };
      ViewBag.inc_type = new SelectList(incType, "Id", "Name");
      string[] defaultAccount = { "BELLIN", "EXPENS" };
      ViewBag.GLAccount = new SelectList(defaultAccount);
      var GLAccount = db.PayrollGLAccounts.Select(x => new { Id = x.acct_no, Name = x.acct_desc }).ToList();
      ViewBag.dflt_acct = new SelectList(GLAccount, "Id", "Name");
      return View();
    }
    [HttpPost]
    public ActionResult Create(MasterIncCodes model)
    {
      if (!db.MasterIncCodes.Where(x => x.inc_code == model.inc_code).Any())
      {
        if (ModelState.IsValid)
        {
          db.Entry(model).State = EntityState.Added;
          int result = db.SaveChanges();
          if (result > 0)
          {
            TempData["success"] = "Record Saved Successfully";
            return RedirectToAction("Index");
          }
          else
          {
            TempData["error"] = "There is some error. Please try again Later";
          }
        }
      }
      else
      {
        TempData["error"] = "This Income code already exist";
        return RedirectToAction("Index");
      }
      var incType = from x in incomeType() select new { Id = x.Value, Name = x.Key };
      ViewBag.inc_type = new SelectList(incType, "Id", "Name", model.inc_type);
      string[] defaultAccount = { "BELLIN", "EXPENS" };
      var dAccount = db.PayrollGLAccounts.Where(x => x.acct_no == model.dflt_acct).Select(x => x.acct_type).FirstOrDefault();
      ViewBag.GLAccount = new SelectList(defaultAccount, dAccount);
      var GLAccount = db.PayrollGLAccounts.Select(x => new { Id = x.acct_no, Name = x.acct_desc }).ToList();
      ViewBag.dflt_acct = new SelectList(GLAccount, "Id", "Name", model.dflt_acct);
      return View(model);
    }
    [HttpGet]
    public ActionResult Edit(string Id)
    {
      if (Id == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }
      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(Id));

      if (string.IsNullOrEmpty(IdUrl) || IdUrl == "Error")
      {
        return RedirectToAction("Index", "UnAuthorize");
        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      int id = Convert.ToInt32(IdUrl);
      MasterIncCodes entity = db.MasterIncCodes.Where(x => x.inc_code_id == id).FirstOrDefault();
      if (entity == null)
      {


        //return HttpNotFound();
        return RedirectToAction("Index", "UnAuthorize");
      }
      var incType = from x in incomeType() select new { Id = x.Value, Name = x.Key };
      ViewBag.inc_type = new SelectList(incType, "Id", "Name", entity.inc_type?.Trim());

      string[] defaultAccount = { "BELLIN", "EXPENS" };
      var dAccount = db.PayrollGLAccounts.Where(x => x.acct_no == entity.dflt_acct).Select(x => x.acct_type).FirstOrDefault();
      ViewBag.GLAccount = new SelectList(defaultAccount, dAccount);

      var GLAccount = db.PayrollGLAccounts.Select(x => new { Id = x.acct_no, Name = x.acct_desc }).ToList();
      ViewBag.dflt_acct = new SelectList(GLAccount, "Id", "Name", entity.dflt_acct);

      return View(entity);
    }
    [HttpPost]
    public ActionResult Edit(MasterIncCodesVM model)
    {
      //string Id = UrlEncryption.Decrypt(model.inc_code_id);
      //int id = Convert.ToInt32(Id);
      var MasterIncCodes = db.MasterIncCodes.FirstOrDefault(x => x.inc_code_id == model.inc_code_id);
      using (var _db = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
      {
        var inc_code = _db.MasterIncCodes.Find(model.inc_code_id).inc_code;
        if (_db.MasterEmployeeIncomes.Where(x => x.inc_code == inc_code).Any() && inc_code != model.inc_code)
        {
          TempData["error"] = "Code is already in use, you can't change this code.";
          return RedirectToAction("Index");
        }
      }

      if (!db.MasterIncCodes.Where(x => x.inc_code == model.inc_code && x.inc_code_id != model.inc_code_id).Any())
      {
        if (ModelState.IsValid)
        {
          MasterIncCodes.inc_code = model.inc_code;
          MasterIncCodes.description = model.description;
          MasterIncCodes.dflt_num = model.dflt_num;
          MasterIncCodes.dflt_rate = model.dflt_rate;
          MasterIncCodes.dflt_hours = model.dflt_hours;
          MasterIncCodes.inc_type = model.inc_type;
          MasterIncCodes.dflt_lo_inc_amt = model.dflt_lo_inc_amt;
          MasterIncCodes.dflt_hi_inc_amt = model.dflt_hi_inc_amt;
          MasterIncCodes.non_qual = model.non_qual;
          db.Entry(MasterIncCodes).State = EntityState.Modified;
          int result = db.SaveChanges();
          //DbContextHelper.dBsavechanges(db);
          if (result > 0)
          {
            TempData["success"] = "Record Updated Successfully";
            return RedirectToAction("Index");
          }
          else
          {
            TempData["error"] = "There is some error. Please try again Later";
            return RedirectToAction("Index");
          }
        }
      }
      else
      {
        TempData["error"] = "This Income code already exist";
        return RedirectToAction("Index");
      }
      var incType = from x in incomeType() select new { Id = x.Value, Name = x.Key };
      ViewBag.inc_type = new SelectList(incType, "Id", "Name", model.inc_type);
      string[] defaultAccount = { "BELLIN", "EXPENS" };
      var dAccount = db.PayrollGLAccounts.Where(x => x.acct_no == model.dflt_acct).Select(x => x.acct_type).FirstOrDefault();
      ViewBag.GLAccount = new SelectList(defaultAccount, dAccount);
      var GLAccount = db.PayrollGLAccounts.Select(x => new { Id = x.acct_no, Name = x.acct_desc }).ToList();
      ViewBag.dflt_acct = new SelectList(GLAccount, "Id", "Name", model.dflt_acct);
      return View(model);
    }
    [HttpGet]
    public ActionResult Details(string Id)
    {
      if(Id == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }
      string IdUrl = UrlEncryption.Decrypt(Convert.ToString(Id));
      if (string.IsNullOrEmpty(IdUrl) || IdUrl == "Error")
      {
        return RedirectToAction("Index", "UnAuthorize");
        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      int Idd = Convert.ToInt32(IdUrl);
      MasterIncCodes entity = db.MasterIncCodes.Where(x => x.inc_code_id == Idd).FirstOrDefault();
      string encryptedId = UrlEncryption.EncryptURL(entity.inc_code_id.ToString());
      ViewBag.EncryptedId = encryptedId;
      if (entity == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
        //return HttpNotFound();
      }

      /// ***** Code By Himanshu Rajput *****

      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "MasterIncomeCode")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel
                   {
                     RoleID = roleList.ToString(),
                     ModuleName = "MasterIncomeCode",
                     ParentId = c.ParentId,
                     ModuleID = c.Id,
                     ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission,
                     EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission,
                   }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.EditPermission = model.EditPermission;

      }
      return View(entity);
    }

    public Dictionary<string, char> incomeType()
    {
      Dictionary<string, char> incType = new Dictionary<string, char>();
      incType.Add("Hrly", 'H');
      incType.Add("Non Hrly", 'N');
      incType.Add("Exp", 'E');
      incType.Add("Soc.Sec./Levy Exempt", 'F');
      incType.Add("Advance", 'A');
      incType.Add("Non-FUTA", 'U');
      incType.Add("Non-FICA/FUTA", 'B');
      return incType;
    }

  }
}