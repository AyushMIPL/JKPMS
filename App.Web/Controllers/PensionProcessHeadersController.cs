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
using App.Data.ViewModels;
using App.Data.Extentions;
using JKPS.COMMON;
using JKPS.BLL;
using App.Web.Filters;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class PensionProcessHeadersController : BaseController
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;
        private AppDbContext _DbContext;

        public PensionProcessHeadersController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _DbContext = DbContext;
        }

        // GET: PensionProcessHeaders
        public ActionResult Index()
        {
            int userid = AppUserManager.GetUserId();
            var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
            var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionProcessHeaders" && x.ActionName == "Index")
                         join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                         from p in ps.DefaultIfEmpty()
                         select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Create New Process", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

            if (model != null)
            {
                ViewBag.AddPermission = model.AddPermssion;
                ViewBag.EditPermission = model.EditPermission;
            }
            string disData = string.Empty;
            PensionProcessViewModel Paysearch = ShowActiveBatch();
            List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BindComboEmployeeType();
            ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
            ViewBag.EmployeeType = new SelectList(listDVOMasterEmpTypes, "type_code", "description", Paysearch.EmpType);
            if (Paysearch.RegionNames != "" && Paysearch.RegionNames != null)
            {
                disData = Paysearch.RegionNames;
            }
            int UserId = AppUserManager.GetUserId();
            List<int> roleId = _DbContext.UserRole.Where(x => x.UserId == UserId).Select(x => x.RoleId).ToList();
            ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);
            Dictionary<string, string> PensionDetailByMonthYear = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear();
            ViewBag.CurrentMonthBatch = PensionDetailByMonthYear;
            if (Paysearch.PayrollDate == null)
            {
                Paysearch.PayrollDate = DateTime.Now;
            }
            return View(Paysearch);
        }
        private List<DVOMasterEmpTypes> BindComboEmployeeType()
        {
            //make object to pass as parameter of search function
            DVOMasterEmpTypes objDVOMasterEmpTypes = new DVOMasterEmpTypes();
            //call getDate function of BLL
            List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes);
            objDVOMasterEmpTypes = null;
            return listDVOMasterEmpTypes;
        }

        private PensionProcessViewModel ShowActiveBatch()
        {
            try
            {
                PensionProcessViewModel Paysearch = new PensionProcessViewModel();
                /*payrollSearchControl1.ClearControls();
                dtEOPDate.Checked = false;
                dtPayrollDate.Checked = false;
                btnstart.Enabled = true;
                btnstart.Text = "No Active Batch for Payroll Process, Click to Start New Batch.";*/

                // lstDVOPYBatchProcessStybatchr.
                List<DVOPYBatchProcessStybatchr> lstDVOPYBatchProcessStybatchr = BLLPYBatchProcessStybatchr.GetActiveBatch();
                if (lstDVOPYBatchProcessStybatchr != null && lstDVOPYBatchProcessStybatchr.Count > 0)
                {
                    DVOPYBatchProcessStybatchr obj = lstDVOPYBatchProcessStybatchr[0];
                    /*lblBatchID.Text = Convert.ToString(obj.pybatchid);
                    lblProcessStartedOn.Text = obj.startedon;
                    lblStartBy.Text = Convert.ToString(obj.insertby);
                    lblStartMachineInfo.Text = obj.insertmachineinfo;*/

                    Paysearch.pybatchid = obj.pybatchid;
                    Paysearch.searchcriteria = obj.searchcriteria;
                    Paysearch.processstartedon = DVOApplicationUserInfo.ParseDateConvertion(obj.startedon);
                    //Paysearch.processstartedon = obj.endedon == null || obj.endedon == "" ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(obj.endedon);
                    Paysearch.errormessage = obj.errormessage;
                    Paysearch.insertbyName = getUserName(obj.insertby);
                    Paysearch.updateby = obj.updateby;

                    // Added by Sujeet
                    Paysearch.status = obj.status;


                    if (obj.searchcriteria.Trim().Length > 0)
                    {

                        string[] searchCriteria = obj.searchcriteria.Split(new string[] { "_R_" }, StringSplitOptions.None);

                        string field = string.Empty;
                        string value = string.Empty;
                        foreach (string str in searchCriteria)
                        {
                            string[] sca = str.Split('=');
                            if (sca.Length > 1)
                            {
                                field = sca[0].Trim();
                                value = sca[1].Trim();
                            }
                            switch (field)
                            {
                                case "District":
                                    Paysearch.RegionNames = value;
                                    break;
                                case "First Name":
                                    Paysearch.FirstName = value;
                                    break;
                                case "Last Name":
                                    Paysearch.LastName = value;
                                    break;
                                case "SPay Period":
                                    Paysearch.PayPeriod = value;
                                    break;
                                case "Full Time":
                                    Paysearch.FullTime = value;
                                    break;
                                case "Pensioner Type":
                                    Paysearch.EmpType = value;
                                    break;
                                case "Job Code":
                                    Paysearch.JobCode = value;
                                    break;
                                case "Job Title":
                                    Paysearch.Title = value;
                                    break;
                                case "Pensioner Code":
                                    Paysearch.EmployeeCode = value;
                                    break;
                                case "Last Pay Date":
                                    Paysearch.LPayDate = DVOApplicationUserInfo.ParseDateConvertion(value);
                                    break;
                                case "End Of Period":
                                    Paysearch.EOPDate = DVOApplicationUserInfo.ParseDateConvertion(value);
                                    Paysearch.EOPDateChecked = true;
                                    break;
                                case "Payroll Date":
                                    Paysearch.PayrollDate = DVOApplicationUserInfo.ParseDateConvertion(value);
                                    Paysearch.PayrollDateChecked = true;
                                    break;
                            }
                        }
                    }
                    if (obj.status == 1)
                    {
                        /*lblStatus.Text = "ACTIVE";
                        btnstart.Enabled = false;
                        payrollSearchControl1.Enabled = false;
                        dtEOPDate.Enabled = false;
                        dtPayrollDate.Enabled = false;
                        btnstart.Text = "Batch : " + obj.pybatchid.ToString() + " is Active for Payroll Process.";*/
                        return Paysearch;
                    }

                    DVOPYBatchProcessDetailStybatchd objDVOPYBatchProcessDetailStybatchd = new DVOPYBatchProcessDetailStybatchd();
                    objDVOPYBatchProcessDetailStybatchd.pybatchid = obj.pybatchid;
                    List<DVOPYBatchProcessDetailStybatchd> listDVOPYBatchProcessDetailStybatchd = BLLPYBatchProcessDetailStybatchd.GetData(ref objDVOPYBatchProcessDetailStybatchd);
                    if (listDVOPYBatchProcessDetailStybatchd != null && listDVOPYBatchProcessDetailStybatchd.Count > 0)
                    {
                        //customDataGridview1.DataSource = listDVOPYBatchProcessDetailStybatchd;
                    }
                }
                return Paysearch;
            }
            catch (Exception Ex)
            {
                TempData["error"] = "Please Try Again..." + Ex.Message;
                //ExceptionManagement.ExceptionManager.Publish(Ex);
                //throw Ex;
                return null;
            }

        }

        private string getUserName(int Id)
        {
            string result = db.UserProfiles.Where(x => x.UserId == Id).Select(x => x.FirstName).FirstOrDefault();
            if (result == null)
            {
                return "N/A";
            }
            else
            {
                return result;
            }
        }


        public JsonResult INSERTBatchProcessInfoAjax(string[] data)
        {

            int userid = AppUserManager.GetUserId();
            DVOPYBatchProcessStybatchr objDVOPYBatchProcessStybatchrINS = new DVOPYBatchProcessStybatchr();
            objDVOPYBatchProcessStybatchrINS.searchcriteria = data[0].ToString();
            objDVOPYBatchProcessStybatchrINS.insertby = userid;
            objDVOPYBatchProcessStybatchrINS.insertmachineinfo = System.Environment.MachineName;
            int INSResult = BLLPYBatchProcessStybatchr.INSERTBatchProcessInfo(ref objDVOPYBatchProcessStybatchrINS);
            if (INSResult == 1)
            {
                PensionProcessViewModel Paysearch = ShowActiveBatch();
                //TempData["success"] = "New Batch For Pension Process has been created successfully.";
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DisplayAjax(int Pybatchid)
        {
            DVOPYBatchProcessDetailStybatchd objDVOPYBatchProcessDetailStybatchd = new DVOPYBatchProcessDetailStybatchd();
            objDVOPYBatchProcessDetailStybatchd.pybatchid = Pybatchid;
            List<DVOPYBatchProcessDetailStybatchd> listDVOPYBatchProcessDetailStybatchd = BLLPYBatchProcessDetailStybatchd.GetData(ref objDVOPYBatchProcessDetailStybatchd);
            if (listDVOPYBatchProcessDetailStybatchd != null && listDVOPYBatchProcessDetailStybatchd.Count > 0)
            {
                // By Sujeet
                // Remove the condition for showing data. It may be activate after discussion
                //var entity = listDVOPYBatchProcessDetailStybatchd.Where(x => x.processname == "Cancel Individual Payroll").Select(x => new { processendedon = x.processendedon, processstartedon = x.processstartedon, x.pybatchid, x.recordsprocessed, x.recordssearched, x.errormessage, x.processname }).ToList();
                //return Json(entity, JsonRequestBehavior.AllowGet);
                //return Json(listDVOPYBatchProcessDetailStybatchd, JsonRequestBehavior.AllowGet);
                return Json(listDVOPYBatchProcessDetailStybatchd, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
        // GET: PensionProcessHeaders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PensionProcessHeader pensionProcessHeader = db.PensionProcessHeader.Find(id);
            if (pensionProcessHeader == null)
            {
                return HttpNotFound();
            }
            return View(pensionProcessHeader);
        }

        // GET: PensionProcessHeaders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PensionProcessHeaders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,pybatchid,startedon,endedon,searchcriteria,recordssearched,recordcound,status,errormessage,insertby,insertdate,insertmachineinfo,updateby,updatedate,updatemachineinfo,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] PensionProcessHeader pensionProcessHeader)
        {
            if (ModelState.IsValid)
            {
                db.PensionProcessHeader.Add(pensionProcessHeader);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pensionProcessHeader);
        }

        // GET: PensionProcessHeaders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PensionProcessHeader pensionProcessHeader = db.PensionProcessHeader.Find(id);
            if (pensionProcessHeader == null)
            {
                return HttpNotFound();
            }
            return View(pensionProcessHeader);
        }

        // POST: PensionProcessHeaders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,pybatchid,startedon,endedon,searchcriteria,recordssearched,recordcound,status,errormessage,insertby,insertdate,insertmachineinfo,updateby,updatedate,updatemachineinfo,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] PensionProcessHeader pensionProcessHeader)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pensionProcessHeader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pensionProcessHeader);
        }

        // GET: PensionProcessHeaders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PensionProcessHeader pensionProcessHeader = db.PensionProcessHeader.Find(id);
            if (pensionProcessHeader == null)
            {
                return HttpNotFound();
            }
            return View(pensionProcessHeader);
        }

        // POST: PensionProcessHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PensionProcessHeader pensionProcessHeader = db.PensionProcessHeader.Find(id);
            db.PensionProcessHeader.Remove(pensionProcessHeader);
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

        public JsonResult AutoCompleteAjax(string term)
        {
            int UserId = AppUserManager.GetUserId();
            int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            var result = BLLMasterEmployee.GetAllData(UserId, RoleId).Where(x => x.EmplCode.ToLower().Contains(term.ToLower()) || x.FirstName.ToLower().Contains(term.ToLower()) || x.LastName.ToLower().Contains(term.ToLower())).Select(x => new { x.EmplCode, x.FirstName, x.LastName }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
