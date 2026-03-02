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
using App.Data.ViewModels;
using System.Threading.Tasks;
using App.Web.Filters;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class ApprovalPermissionController : BaseController
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;
        private AppDbContext _DbContext;

        public ApprovalPermissionController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _DbContext = DbContext;
        }

        // GET: ApprovalPermission
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var user = db.Users.ToList();
            //var module = db.ApprovalProcessLevel.ToList();
            List<ApprovalProcessAssignedUserViewModel> List = new List<ApprovalProcessAssignedUserViewModel>();
            //var list = db.ApprovalProcessAssignedUser.Where(x => x.IsActive == true);
            var list = (from users in db.ApprovalProcessAssignedUser.Where(x => x.IsActive == true) join level in db.ApprovalProcessLevel.Where(x => x.IsActive == true) on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId select new { users, level }).ToList();
            foreach (var item in list)
            {
                ApprovalProcessAssignedUserViewModel tempList = new ApprovalProcessAssignedUserViewModel();
                tempList.ApprovalProcessId = item.users.ApprovalProcessId;
                tempList.LevelName = item.level.ApprovalProcessLevelName;
                //tempList.ApprovalProcessId = item.ApprovalProcessId;
                //tempList.LevelName = module.Where(x => x.ApprovalProcessLevelId == item.ApprovalProcessLevelId && x.IsActive == true).Select(x => x.ApprovalProcessLevelName).FirstOrDefault();
                tempList.Username = user.Where(x => x.Id == item.users.ApprovalUser).Select(x => x.UserName).FirstOrDefault();
                tempList.ApplicationType = item.users.ApprovalProcessLevelId > 3 ? "Refund" : "Pension";
                tempList.level = item.level.ApprovalLevel;
                List.Add(tempList);
            }
            IEnumerable<ApprovalProcessAssignedUserViewModel> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.LevelName.ToLower().Contains(param.sSearch.ToLower())
                   || c.Username.ToLower().Contains(param.sSearch.ToLower())
                   || c.ApplicationType.ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<ApprovalProcessAssignedUserViewModel, string> orderingFunction = (c => sortColumnIndex == 0 ? c.LevelName :
                                                                                  sortColumnIndex == 1 ? c.ApprovalProcessId + "" :
                                                                                            sortColumnIndex == 2 ? c.Username + "" :
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
                         c.LevelName,
                         //(c.ApprovalProcessId>3?(c.ApprovalProcessId-3):c.ApprovalProcessId)+"",
                         c.level+"",
                                         c.Username,
                                         c.ApplicationType + "",
                     c.ApprovalProcessId+""
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
        // GET: ApprovalPermission/Details/5


        // GET: ApprovalPermission/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApprovalProcessAssignedUser approvalProcessAssignedUser = db.ApprovalProcessAssignedUser.Where(x => x.ApprovalProcessId == id && x.IsActive == true).FirstOrDefault();
            if (approvalProcessAssignedUser == null)
            {
                return HttpNotFound();
            }
            var ModuleTypes = from applicationType e in Enum.GetValues(typeof(applicationType))
                              select new { Id = (int)e, Name = e.ToString() };
            ViewBag.ModuleType = new SelectList(ModuleTypes.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.ApprovalUser = new SelectList(db.Users, "Id", "UserName", approvalProcessAssignedUser.ApprovalUser);
            ViewBag.ApprovalProcessId = new SelectList(db.ApprovalProcessLevel.Where(x => x.IsActive == true), "ApprovalProcessLevelId", "ApprovalProcessLevelName", approvalProcessAssignedUser.ApprovalProcessLevelId);

            return View(approvalProcessAssignedUser);
        }

        // POST: ApprovalPermission/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApprovalProcessAssignedUser approvalProcessAssignedUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(approvalProcessAssignedUser).State = EntityState.Modified;
                int result = await db.SaveChangesAsync();
                if (result > 0)
                {
                    TempData["success"] = "Details Updates Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "There is some error, please try again later";
                }
            }
            var ModuleTypes = from applicationType e in Enum.GetValues(typeof(applicationType))
                              select new { Id = (int)e, Name = e.ToString() };
            ViewBag.ModuleType = new SelectList(ModuleTypes.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.ApprovalUser = new SelectList(db.Users, "Id", "UserName", approvalProcessAssignedUser.ApprovalUser);
            ViewBag.ApprovalProcessId = new SelectList(db.ApprovalProcessLevel.Where(x => x.IsActive == true), "ApprovalProcessLevelId", "ApprovalProcessLevelName", approvalProcessAssignedUser.ApprovalProcessLevelId);

            return View(approvalProcessAssignedUser);
        }

        // GET: ApprovalPermission/Delete/5
        public async Task<JsonResult> checkPasswordAjax(string p)
        {
            int result = 0;
            int users = AppUserManager.GetUserId();
            string UserName = db.Users.Where(x => x.Id == users).Select(x => x.UserName).FirstOrDefault();
            var user = await OwinUserManger.FindAsync(UserName, p);
            if (user != null)
            {
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
