using App.Data.Entities;
using App.Data;
using App.Web.Helper;
using App.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using JKPS.COMMON;
using App.Web.Filters;
using App.Data.ViewModels;


namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class MailSettingsController : BaseController
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;
        private AppDbContext _DbContext;

        public MailSettingsController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _DbContext = DbContext;
        }

        // GET: Admin/MailSettings
        //MailSettingsRepo MailSettingsRepoobj = new MailSettingsRepo(new AppDbContext());
        [HttpGet]
        [AuthorizeEx()]
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public ActionResult MailSettingListAjax(JQueryDataTableParamModel param)
        {
            var MailSettings = _DbContext.MailSettings.ToList();
            IEnumerable<MailSettings> filtered;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = MailSettings.Where(c =>
                                            (c.ProcessName + "").ToLower().Contains(param.sSearch.ToLower()) ||
                                            (c.MailTo + "").ToLower().Contains(param.sSearch.ToLower()) ||
                                            (c.CC + "").ToLower().Contains(param.sSearch.ToLower()) ||
                                            (c.BCC + "").ToLower().Contains(param.sSearch.ToLower()) ||
                                            (c.Subject + "").ToLower().Contains(param.sSearch.ToLower()) ||
                                            (c.Contents + "").ToLower().Contains(param.sSearch.ToLower()) ||
                                            (c.IsSendNotificationAlert + "").ToLower().Contains(param.sSearch.ToLower()) ||
                                            (c.ProcessWillStartAt + "").ToLower().Contains(param.sSearch.ToLower()) ||
                                            //(c.ProcessWillStartAtStr + "").ToLower().Contains(param.sSearch.ToLower()) ||
                                            (c.DelayTime + "").ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filtered = MailSettings;
            }
            var srtIndex = Convert.ToInt32(Request["iSortCol_0"]); //SiteHelpers.TypeCasting<int>(Request["iSortCol_0"] + "", CultureInfo.CurrentCulture);//int.Parse(Request["iSortCol_0"]);//param.iSortingCols;
            Func<MailSettings, object> orderingFunction = (c =>
                                                              srtIndex == 0 ? c.ProcessName :
                                                              srtIndex == 1 ? c.MailTo :
                                                              srtIndex == 2 ? c.CC :
                                                              srtIndex == 3 ? c.BCC :
                                                              srtIndex == 4 ? c.Subject :
                                                              srtIndex == 5 ? c.Contents :
                                                              srtIndex == 6 ? c.IsSendNotificationAlert :
                                                              srtIndex == 7 ? c.ProcessWillStartAt :
                                                              //srtIndex == 8 ? c.ProcessWillStartAtStr  :
                                                              srtIndex == 8 ? c.DelayTime : (object)"");
            var sortDirection = Request["sSortDir_0"];
            if (sortDirection == "asc")
                filtered = filtered.OrderBy(orderingFunction).ToList();
            else
                filtered = filtered.OrderByDescending(orderingFunction).ToList();
            IEnumerable<MailSettings> displayed;
            if (param.iDisplayLength == -1)
                displayed = filtered;
            else
                displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var data = (from c in displayed
                            // orderby c.ModifiedOn descending
                        select new[]
                                   {
                                    c.ProcessName+"",//0
                                    c.MailTo+"",//1
                                    c.CC+"",//2
                                    c.BCC+"",//3
                                    c.Subject+"",//4
                                    c.Contents+"",//5
                                    c.IsSendNotificationAlert+"",//6
                                    c.ProcessWillStartAt+"",//7
                                                            //c.ProcessWillStartAtStr+"",
                                    c.DelayTime+"",//8
                                    c.IsActive+"",//9
                                    UrlEncryption.EncryptURL(c.Id.ToString()),//10
                                    c.IsInstantMailing+"",//11
                                   });
            return Json(
                        new
                        {
                            sEcho = param.sEcho,
                            iTotalRecords = MailSettings.Count(),
                            iTotalDisplayRecords = filtered.Count(),
                            aaData = data
                        }, JsonRequestBehavior.AllowGet);
        }
        // GET: Admin/MailSettings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailSettings mailSettings = await db.MailSettings.FindAsync(id);
            if (mailSettings == null)
            {
                return HttpNotFound();
            }
            return View(mailSettings);
        }

        // GET: Admin/MailSettings/Create
        public ActionResult CreateAjax()
        {
            MailSettings mailSettings = new MailSettings();
            var ListItem2 = Enum.GetValues(typeof(CustomMailPriority)).Cast<CustomMailPriority>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            });
            ViewBag.Priority = new SelectList(ListItem2, "Value", "Text");
            return PartialView("_MasterMailSetting", mailSettings);
        }

        // POST: Admin/MailSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAjax(MailSettings mailSettings)
        {

            if (ModelState.IsValid)
            {
                if (db.MailSettings.Where(x => x.ProcessName == mailSettings.ProcessName).Any())
                {
                    //TempData["error"] = "This Process Name already exist..";
                    return Json(new { status = false, msg = "This Process Name already exist.." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.MailSettings.Add(mailSettings);
                    await db.SaveChangesAsync();
                    return Json(new { status = true, msg = "Saved Successfully " }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { status = false, msg = "Field must be requried" }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/MailSettings/Edit/5

        public async Task<ActionResult> EditAjax(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            id = UrlEncryption.Decrypt(id);
            int dId = Convert.ToInt32(id);
            MailSettings mailSettings = await db.MailSettings.FindAsync(dId);
            if (mailSettings == null)
            {
                return HttpNotFound();
            }
            var ListItem2 = Enum.GetValues(typeof(CustomMailPriority)).Cast<CustomMailPriority>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            });
            ViewBag.Priority = new SelectList(ListItem2, "Value", "Text", mailSettings.Priority);
            return PartialView("_MasterMailSetting", mailSettings);
        }

        // POST: Admin/MailSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MailSettings model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {

                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Json(new { status = true, msg = "Saved Successfully " }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { status = false, msg = "Invalid Data" }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/MailSettings/Delete/5

        public async Task<ActionResult> DeleteAjax(string id)
        {
            int res = 0;
            Int32 dId = Convert.ToInt32(UrlEncryption.Decrypt(id));
            var data = db.MailSettings.FirstOrDefault(a => a.Id == dId);
            data.IsActive = !data.IsActive;
            db.Entry(data).State = EntityState.Modified;
            res = db.SaveChanges();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        // POST: Admin/MailSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Int32 dId = Convert.ToInt32(UrlEncryption.Decrypt(id));
            MailSettings mailSettings = await db.MailSettings.FindAsync(dId);
            db.MailSettings.Remove(mailSettings);
            await db.SaveChangesAsync();
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