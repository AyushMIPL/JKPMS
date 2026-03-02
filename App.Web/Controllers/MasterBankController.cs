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
using App.Web.Helper;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Vml.Office;
using App.Data.ViewModels;

namespace App.Web.Controllers
{
  [AuthorizeEx()]
  public class MasterBankController : BaseController
  {
    //private AppDbContext db = new AppDbContext();
    private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
    private AppDbContext db;

    public MasterBankController()
    {
      db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
    }

    // GET: MasterBanks
    public ActionResult Index()
    {
      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "MasterBank" && x.ActionName == "Index")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Bank", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.AddPermission = model.AddPermssion;
        ViewBag.EditPermission = model.EditPermission;
      }
      return View();
    }
    public JsonResult Remove(string Id)
    {
      int result = 0;
      if (Id != "0")
      {
        //int id = Convert.ToInt32(Id);
        MasterBanks entity = db.MasterBanks.Where(x => x.bank_code == Id).FirstOrDefault();
        //entity.IsActive = false;
        db.Entry(entity).State = EntityState.Modified;
        db.SaveChanges();
        result = 1;
      }
      return Json(result, JsonRequestBehavior.AllowGet);
    }
    public ActionResult AjaxHandler(JQueryDataTableParamModel param)
    {
      var List = db.MasterBanks;
      IEnumerable<MasterBanks> filtered;

      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => c.bank_code.ToLower().Contains(param.sSearch.ToLower())
               || c.bank_desc.ToLower().Contains(param.sSearch.ToLower())
               || c.mag_media.ToLower().Contains(param.sSearch.ToLower())
               || c.cash_acct_no.ToString().ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<MasterBanks, string> orderingFunction = (c => sortColumnIndex == 0 ? c.bank_code :
                                                        sortColumnIndex == 1 ? c.bank_desc :
                                                        sortColumnIndex == 2 ? c.mag_media :
                                                        sortColumnIndex == 3 ? c.cash_acct_no.HasValue ? c.cash_acct_no.HasValue.ToString() : string.Empty :
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
                         c.bank_code,
                         c.bank_desc,
                         c.mag_media,
                         //c.MediaFormat,
										     //c.IsActive + "", 
										UrlEncryption.EncryptURL(Convert.ToString(c.Bank_Code_ID)) + ""
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

    // GET: MasterBank/Details/5
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
      int Idd = Convert.ToInt32(IdUrl);
      MasterBanks masterBank = db.MasterBanks.Find(Idd);
      if (masterBank == null)
      {
        //return HttpNotFound();
        return RedirectToAction("Index", "UnAuthorize");
      }
      string encryptedId = UrlEncryption.EncryptURL(masterBank.Bank_Code_ID.ToString());
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

      return View(masterBank);
    }

    // GET: MasterBank/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: MasterBank/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(MasterBanks masterBank)
    {
      var verify = db.MasterBanks.Where(x => x.bank_code == masterBank.bank_code).FirstOrDefault();
      if (verify != null)
      {
        TempData["error"] = "This Bank details already exist..";
        return RedirectToAction("Index");
      }
      else
      {
        if (ModelState.IsValid)
        {
          db.MasterBanks.Add(masterBank);
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
      return View(masterBank);
    }

    // GET: MasterBank/Edit/5
    public ActionResult Edit(string id)
    {
      if (id == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }
      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(id));
      if (string.IsNullOrEmpty(IdUrl) || IdUrl == "Error")
      {
        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        return RedirectToAction("Index", "UnAuthorize");
      }
      int idd = Convert.ToInt32(IdUrl);
      MasterBanks masterBank = db.MasterBanks.Find(idd);
      if (masterBank == null)
      {
        //return HttpNotFound();
        return RedirectToAction("Index", "UnAuthorize");
      }
      return View(masterBank);
    }

    // POST: MasterBank/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(MasterBanksVM masterBank)
    {
      var verify = db.MasterBanks.Where(x => x.bank_code == masterBank.bank_code && x.Bank_Code_ID != masterBank.Bank_Code_ID).FirstOrDefault();
      var Data = db.MasterBanks.Where(x => x.Bank_Code_ID == masterBank.Bank_Code_ID).FirstOrDefault();
      if (verify != null)
      {
        TempData["error"] = "This Bank Details already exist..";
        return RedirectToAction("Index");
      }
      else
      {
        if (ModelState.IsValid)
        {
          Data.bank_code = masterBank.bank_code;
          Data.bank_desc = masterBank.bank_desc;
          Data.co_bank_acct_no = masterBank.co_bank_acct_no;
          Data.cash_acct_no = masterBank.cash_acct_no;
          Data.mag_media = masterBank.mag_media;
          db.Entry(Data).State = EntityState.Modified;
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
            return RedirectToAction("Index");
          }
        }
      }
      return View(masterBank);
    }

    // GET: MasterBank/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterBanks masterBank = db.MasterBanks.Find(id);
      if (masterBank == null)
      {
        return HttpNotFound();
      }
      return View(masterBank);
    }

    // POST: MasterBank/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      MasterBanks masterBank = db.MasterBanks.Find(id);
      db.MasterBanks.Remove(masterBank);
      db.SaveChanges();
      return RedirectToAction("Index");
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
