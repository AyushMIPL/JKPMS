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
using App.Data.ViewModels;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace App.Web.Controllers
{
  [AuthorizeEx()]
  public class MasterStatusController : BaseController
  {
    //private AppDbContext db = new AppDbContext();
    private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
    private AppDbContext db;

    public MasterStatusController()
    {
      db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
    }

    // GET: MasterStatus
    public ActionResult Index()
    {
      return View(db.MasterStatus.ToList());
    }

    // GET: MasterStatus/Details/5
    public ActionResult Details(string id)
    {
      if(id== null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }
      string IdUrl = UrlEncryption.Decrypt(Convert.ToString(id));
      if (string.IsNullOrEmpty(IdUrl) || IdUrl == "Error")
      {
        return RedirectToAction("Index", "UnAuthorize");
        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      //MasterStatus masterStatus = db.MasterStatus.Find(Convert.ToInt32(IdUrl));
      int statusId = Convert.ToInt32(IdUrl);
      MasterStatus masterStatus = db.MasterStatus.Where(x => x.IsActive == true && x.Id == statusId).FirstOrDefault();
      if (masterStatus == null)
      {
        //return HttpNotFound();
        return RedirectToAction("Index", "UnAuthorize");
      }
      string encryptedId = UrlEncryption.EncryptURL(masterStatus.Id.ToString());
      ViewBag.EncryptedId = encryptedId;

      return View(masterStatus);
    }

    // GET: MasterStatus/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: MasterStatus/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(MasterStatus masterStatus)
    {
      var verify = db.MasterStatus.Where(x => (x.Name == masterStatus.Name || x.StatusCode == masterStatus.StatusCode) && x.IsActive == true).FirstOrDefault();
      if (verify != null)
      {
        TempData["error"] = "This Status already exist..";
        return RedirectToAction("Index");
      }
      else
      {
        if (ModelState.IsValid)
        {
          db.MasterStatus.Add(masterStatus);
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

      return View(masterStatus);
    }

    // GET: MasterStatus/Edit/5
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
      //MasterStatus masterStatus = db.MasterStatus.Find(Convert.ToInt32(IdUrl));

      int statusEditId = Convert.ToInt32(IdUrl);
      MasterStatus masterStatus =db.MasterStatus.Where(x=> x.IsActive == true && x.Id==statusEditId).FirstOrDefault();
      if (masterStatus == null)
      {
        //return HttpNotFound();
        return RedirectToAction("Index", "UnAuthorize");
      }
      return View(masterStatus);
    }

    // POST: MasterStatus/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(MasterStatusVM masterStatus)
    {
      string Id = UrlEncryption.Decrypt(masterStatus.Id);
      int id = Convert.ToInt32(Id);
      var MasterMasterStatus = db.MasterStatus.FirstOrDefault(x => x.Id == id);
      var verify = db.MasterStatus.Where(x => (x.Name == masterStatus.Name || x.StatusCode == masterStatus.StatusCode) && x.Id != id && x.IsActive == true).FirstOrDefault();
      if (verify != null)
      {
        TempData["error"] = "This Status already exist..";
        return RedirectToAction("Index");
      }
      else
      {
        if (ModelState.IsValid)
        {
          MasterMasterStatus.Name = masterStatus.Name;
          MasterMasterStatus.StatusCode = masterStatus.StatusCode;
          db.Entry(MasterMasterStatus).State = EntityState.Modified;
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
      return View(masterStatus);
    }

    // GET: MasterStatus/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterStatus masterStatus = db.MasterStatus.Find(id);
      if (masterStatus == null)
      {
        return HttpNotFound();
      }
      return View(masterStatus);
    }

    // POST: MasterStatus/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      MasterStatus masterStatus = db.MasterStatus.Find(id);
      db.MasterStatus.Remove(masterStatus);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AjaxHandler(JQueryDataTableParamModel param)
    {
      var List = db.MasterStatus.Where(x => x.IsActive == true);
      IEnumerable<MasterStatus> filtered;


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

      Func<MasterStatus, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name :
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

    public JsonResult Remove(string Id)
    {
      int result = 0;
      string IdUrl = UrlEncryption.Decrypt(Id);
      if (Id != "0")
      {
        int id = Convert.ToInt32(IdUrl);
        MasterStatus entity = db.MasterStatus.Where(x => x.Id == id).FirstOrDefault();
        entity.IsActive = false;
        db.Entry(entity).State = EntityState.Modified;
        db.SaveChanges();
        result = 1;
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
