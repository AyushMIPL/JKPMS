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
using DocumentFormat.OpenXml.Bibliography;
using OfficeOpenXml;
// using Microsoft.Office.Interop.Excel;

using System.Drawing;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class DistrictController : BaseController
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public DistrictController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // GET: Cities
        public ActionResult Index()
        {
            int userid = AppUserManager.GetUserId();
            var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
            var model = (from c in db.SecModule.Where(x => x.ControllerName == "District" && x.ActionName == "Index")
                         join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                         from p in ps.DefaultIfEmpty()
                         select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "District", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

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

            //var regionDistGet = GetRegionName();
            var regionDistGet = GetRegionName();

            var ReginDistGetId = db.MasterRegion.Where(x => x.Name == regionDistGet).Select(x => x.Id).FirstOrDefault();
            var List = db.MasterDistrict.Where(x => x.IsActive == true && x.RegionId == ReginDistGetId);
            IEnumerable<MasterDistrict> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.Regions.Name.ToLower().Contains(param.sSearch.ToLower())
                   || c.Name.ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MasterDistrict, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Regions.Name :
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
                         db.MasterRegion.FirstOrDefault(x=>x.IsActive && x.Id==c.RegionId)?.Name ??"",
                         c.Name,
                                         c.IsActive + "",
                                UrlEncryption.EncryptURL(Convert.ToString(c.Id))
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

            int districtId = Convert.ToInt32(IdUrl);

            MasterDistrict District = await db.MasterDistrict.Where(x => x.IsActive && x.Id == districtId).FirstOrDefaultAsync();
            if (District == null)
            {
                return RedirectToAction("Index", "UnAuthorize");
            }

            var region = GetRegionName();

            bool isValidDistrict = await IsDistrictValidForCurrentRegion(District.RegionId, region);
            if (!isValidDistrict)
            {
                return RedirectToAction("Index", "UnAuthorize");
            }

            ViewBag.EncryptedId = id;

            int userid = AppUserManager.GetUserId();
            var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
            var model = (from c in db.SecModule.Where(x => x.ControllerName == "District")
                         join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                         from p in ps.DefaultIfEmpty()
                         select new RoleModuleViewModel
                         {
                             RoleID = roleList.ToString(),
                             ModuleName = "District",
                             ParentId = c.ParentId,
                             ModuleID = c.Id,
                             ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission,
                             EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission,
                         }).FirstOrDefault();

            if (model != null)
            {
                ViewBag.EditPermission = model.EditPermission;

            }
            return View(District);
        }

        // GET: City/Create
        public ActionResult Create()
        {
            // code By Himanshu Rajput *** Region By Filter ***

            //var regionDist = GetRegionName();
            var regionDist = GetRegionName();
            var reginIdDist = db.MasterRegion.Where(x => x.Name == regionDist).Select(x => x.Id).FirstOrDefault();

            ViewBag.RegionId = new SelectList(db.MasterRegion.Where(x => x.IsActive == true && x.Id == reginIdDist).OrderBy(x => x.Name), "Id", "Name");
            return View();
        }

        // POST: City/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,RegionId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterDistrict district)
        {
            if (db.MasterDistrict.Where(x => x.Name == district.Name && x.IsActive == true).Any())
            {
                TempData["error"] = "This District already exist..";
                return RedirectToAction("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    district.IsActive = true;
                    db.MasterDistrict.Add(district);
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
      //ViewBag.RegionId = new SelectList(db.MasterDistrict.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", district.RegionId);

      var regionDist = GetRegionName();
      var reginIdDist = db.MasterRegion.Where(x => x.Name == regionDist).Select(x => x.Id).FirstOrDefault();

      ViewBag.RegionId = new SelectList(db.MasterRegion.Where(x => x.IsActive == true && x.Id == reginIdDist).OrderBy(x => x.Name), "Id", "Name", district.RegionId);
      //ViewBag.RegionId = new SelectList(db.MasterDistrict.Where(x => x.IsActive == true && x.RegionId == regionCreateId).OrderBy(x => x.Name), "Id", "Name", district.RegionId);

      return View(district);
        }

        public async Task<bool> IsDistrictValidForCurrentRegion(int regionId, string currentRegion)
        {
            bool isValid = false;
            try
            {
                var regionOfDistrict = await db.MasterRegion.Where(x => x.IsActive && x.Id == regionId).Select(x => x.Name).FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(regionOfDistrict) && !string.IsNullOrEmpty(currentRegion) && (regionOfDistrict.Trim().ToUpper() == currentRegion.Trim().ToUpper()))
                {
                    isValid = true;
                }
            }
            catch
            {
            }

            return isValid;
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

            int districtId = Convert.ToInt32(IdUrl);

            MasterDistrict District = await db.MasterDistrict.Where(x => x.IsActive && x.Id == districtId).FirstOrDefaultAsync();
            if (District == null)
            {
                return RedirectToAction("Index", "UnAuthorize");
            }

            var region = GetRegionName();

            bool isValidDistrict = await IsDistrictValidForCurrentRegion(District.RegionId, region);
            if (!isValidDistrict)
            {
                return RedirectToAction("Index", "UnAuthorize");
            }

            var ReginDistEditId = db.MasterRegion.Where(x => x.Name == region).Select(x => x.Id).FirstOrDefault();
            ViewBag.RegionId = new SelectList(db.MasterRegion.Where(x => x.IsActive == true && x.Id == ReginDistEditId).OrderBy(x => x.Name), "Id", "Name", District.RegionId);
            return View(District);
        }

        // POST: City/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,RegionId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterDistrictVM District)
        {
            string Id = UrlEncryption.Decrypt(District.Id);
            int id = Convert.ToInt32(Id);
            var MasterDISTRICT = db.MasterDistrict.FirstOrDefault(x => x.Id == id);
            if (db.MasterDistrict.Where(x => x.Name == District.Name && x.Id != id && x.IsActive == true).Any())
            {
                TempData["error"] = "This District already exist..";
                return RedirectToAction("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    MasterDISTRICT.Name = District.Name;
                    MasterDISTRICT.RegionId = District.RegionId;
                    MasterDISTRICT.IsActive = true;
                    db.Entry(MasterDISTRICT).State = EntityState.Modified;
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
      //ViewBag.RegionId = new SelectList(db.MasterState.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", District.RegionId);

      var regionDist = GetRegionName();
      var reginIdDist = db.MasterRegion.Where(x => x.Name == regionDist).Select(x => x.Id).FirstOrDefault();

      ViewBag.RegionId = new SelectList(db.MasterRegion.Where(x => x.IsActive == true && x.Id == reginIdDist).OrderBy(x => x.Name), "Id", "Name", District.RegionId);
      return View(MasterDISTRICT);
        }

        // GET: City/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterDistrict city = db.MasterDistrict.Find(id);
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
            MasterDistrict city = db.MasterDistrict.Find(id);
            db.MasterDistrict.Remove(city);
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
                    MasterDistrict entity = db.MasterDistrict.Where(x => x.Id == idd).FirstOrDefault();
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
