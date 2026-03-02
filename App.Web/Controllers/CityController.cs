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
using DocumentFormat.OpenXml.Office2010.Excel;
using App.Data.ViewModels;
using DocumentFormat.OpenXml.Bibliography;
using static App.Web.Helper.Helper;
using Microsoft.Office.Interop.Excel;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class CityController : BaseController
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;
        private AppDbContext _DbContext;

        public CityController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _DbContext = DbContext;
        }

        // GET: Cities
        public ActionResult Index()
        {
            int userid = AppUserManager.GetUserId();
            var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
            var model = (from c in db.SecModule.Where(x => x.ControllerName == "City" && x.ActionName == "Index")
                         join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                         from p in ps.DefaultIfEmpty()
                         select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "City", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

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

            //var regionGet = GetRegionName();
            var regionGet = GetRegionName();
            var ReginGetId = db.MasterRegion.Where(x => x.Name == regionGet).Select(x => x.Id).FirstOrDefault();

            var cityList = db.MasterDistrict.Where(x => x.IsActive == true && x.RegionId == ReginGetId).Select(x => x.Id).ToList();

            var List = db.MasterCity.Where(x => x.IsActive == true && cityList.Contains(x.DistrictId)).ToList();
            IEnumerable<MasterCity> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.Districts.Name.ToLower().Contains(param.sSearch.ToLower())
                    || c.Name.ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MasterCity, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Districts.Name :
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
                       db.MasterDistrict.FirstOrDefault(x => x.IsActive && x.Id == c.DistrictId)?.Name ?? "",
                         c.Name ,
                                         c.IsActive + "", 
										 //c.Id + ""
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

            int cityId = Convert.ToInt32(IdUrl);

            var city = await db.MasterCity.Where(x => x.IsActive && x.Id == cityId).FirstOrDefaultAsync();
            if (city == null)
            {
                return RedirectToAction("Index", "UnAuthorize");
            }

            string region = GetRegionName();

            bool isValidCity = await IsCityValidForCurrentRegion(city.DistrictId, region);

            if (!isValidCity)
            {
                return RedirectToAction("Index", "UnAuthorize");
            }

            ViewBag.EncryptedId = id;

            int userid = AppUserManager.GetUserId();
            var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
            var model = (from c in db.SecModule.Where(x => x.ControllerName == "City")
                         join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                         from p in ps.DefaultIfEmpty()
                         select new RoleModuleViewModel
                         {
                             RoleID = roleList.ToString(),
                             ModuleName = "City",
                             ParentId = c.ParentId,
                             ModuleID = c.Id,
                             ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission,
                             EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission,
                         }).FirstOrDefault();

            if (model != null)
            {
                ViewBag.EditPermission = model.EditPermission;

            }


            string districtName = db.MasterDistrict
                                         .Where(x => x.IsActive == true && x.Id == city.DistrictId)
                                         .Select(x => x.Name)
                                         .FirstOrDefault();
            // Pass the district name to the view
            ViewBag.DistrictName = districtName;

            return View(city);
        }

        // GET: City/Create
        public ActionResult Create()
        {
            // code By Himanshu Rajput *** Region By Filter ***

            //var regions = GetRegionName();
            var regions = GetRegionName();

            var ReginId = db.MasterRegion.Where(x => x.Name == regions).Select(x => x.Id).FirstOrDefault();


            ViewBag.DistrictId = new SelectList(db.MasterDistrict.Where(x => x.IsActive == true && x.RegionId == ReginId).OrderBy(x => x.Name), "Id", "Name");
            return View();
        }

        // POST: City/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,DistrictId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterCity city)
        {
            if (db.MasterCity.Where(x => x.Name == city.Name && x.IsActive == true).Any())
            { TempData["error"] = "This City already exist.."; }
            else
            {
                if (ModelState.IsValid)
                {
                    city.IsActive = true;
                    db.MasterCity.Add(city);
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

      var regionCreate = GetRegionName();

      var regionCreateId = db.MasterRegion.Where(x => x.Name == regionCreate).Select(x => x.Id).FirstOrDefault();

      ViewBag.DistrictId = new SelectList(db.MasterDistrict.Where(x => x.IsActive == true && x.RegionId == regionCreateId).OrderBy(x => x.Name), "Id", "Name", city.DistrictId);



      //ViewBag.DistrictId = new SelectList(db.MasterDistrict.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", city.DistrictId);
            return View(city);
        }

        // GET: City/Edit/5

        public async Task<bool> IsCityValidForCurrentRegion(int districtId, string currentRegion)
        {
            bool isValid = false;
            try
            {
                var RegionId = await db.MasterDistrict.Where(x => x.IsActive && x.Id == districtId).Select(x => x.RegionId).FirstOrDefaultAsync();

                var regionOfCity = await db.MasterRegion.Where(x => x.IsActive && x.Id == RegionId).Select(x => x.Name).FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(regionOfCity) && !string.IsNullOrEmpty(currentRegion) && (regionOfCity.Trim().ToUpper() == currentRegion.Trim().ToUpper()))
                {
                    isValid = true;
                }
            }
            catch
            {
            }

            return isValid;
        }

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

            int cityId = Convert.ToInt32(IdUrl);

            var city = await db.MasterCity.Where(x => x.IsActive && x.Id == cityId).FirstOrDefaultAsync();
            if (city == null)
            {
                return RedirectToAction("Index", "UnAuthorize");
            }

            string region = GetRegionName();

            bool isValidCity = await IsCityValidForCurrentRegion(city.DistrictId, region);

            if (!isValidCity)
            {
                return RedirectToAction("Index", "UnAuthorize");
            }

            var reginIdEdit = db.MasterRegion.Where(x => x.Name == region.Trim().ToUpper()).Select(x => x.Id).FirstOrDefault();
            ViewBag.DistrictId = new SelectList(db.MasterDistrict.Where(x => x.IsActive == true && x.RegionId == reginIdEdit).OrderBy(x => x.Name), "Id", "Name", city.DistrictId);
            return View(city);
        }

        // POST: City/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,DistrictId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterCityVM city)
        {
            string Id = UrlEncryption.Decrypt(city.Id);
            int id = Convert.ToInt32(Id);
            //int id = Convert.ToInt32(city.Id);
            var Mastercity = db.MasterCity.FirstOrDefault(x => x.Id == id);

            if (db.MasterCity.Where(x => x.Name == city.Name && x.Id != id && x.IsActive == true).Any())
            {
                TempData["error"] = "This City already exist..";
                return RedirectToAction("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Mastercity.Name = city.Name;
                    Mastercity.DistrictId = city.DistrictId;
                    Mastercity.IsActive = true;
                    db.Entry(Mastercity).State = EntityState.Modified;
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
      //ViewBag.DistrictId = new SelectList(db.MasterDistrict.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", city.DistrictId);

      var regionCreate = GetRegionName();

      var regionCreateId = db.MasterRegion.Where(x => x.Name == regionCreate).Select(x => x.Id).FirstOrDefault();

      ViewBag.DistrictId = new SelectList(db.MasterDistrict.Where(x => x.IsActive == true && x.RegionId == regionCreateId).OrderBy(x => x.Name), "Id", "Name", city.DistrictId);


      return View(Mastercity);
        }

        // GET: City/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterCity city = db.MasterCity.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: City/Delete/5    [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterCity city = db.MasterCity.Find(id);
            db.MasterCity.Remove(city);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult AjaxRemove(string Id, string name)
        {
            int result = 0;
            string IdUrl = UrlEncryption.Decrypt(Id);
            int id = Convert.ToInt32(IdUrl);
            if (id != 0)
            {
                bool action = MasterDeleteRepo.AjaxDelete(id, name);
                if (action)
                {
                    MasterCity entity = db.MasterCity.Where(x => x.Id == id).FirstOrDefault();
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
