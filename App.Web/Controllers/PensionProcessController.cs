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
using System.Text;
using App.Data.ViewModels;
using App.Data.Extentions;
using System.Reflection;
using JKPS.BLL;
using JKPS.CommonUtilities;
using JKPS.COMMON;
using System.IO;
using App.Web.Filters;
using App.Web.Repository;
using System.Data.SqlClient;
using CrystalDecisions.Shared.Json;
using System.Web.Script.Serialization;
using JKPS.DL;
using System.Collections;
using Renci.SshNet;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNet.Identity;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using System.Globalization;
using System.Web.SessionState;
using static App.Web.Helper.Helper;
using System.Web.WebPages;
using System.Printing;
using App.Web.Helper;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using System.Drawing;
using System.Web.Hosting;
using ClosedXML.Excel;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class PensionProcessController : BaseController
    {
        /// <summary>
        /// object to maintain the transaction object to execute update process
        ///  it is used to lock/update/release of current record
        /// </summary>
        object TransactionObject;
        /// <summary>
        /// message to show error after completion of any process on this form,
        /// it will set after completion of process, if any error
        /// </summary>
        private string ErrorMessage = "There is some error.\nPlease try again.";
        string _EmpFlexDeptAcctType = string.Empty;
        string _EmpFlexDeptKeyValue = string.Empty;

        string _EmpStateTaxCode = string.Empty;
        /// <summary>
        /// doc_no after inserting new document
        /// </summary>
        int newDocNo = 0;
        bool TimeCardUsed = false;
        int UserTimeCardNo = 0;
        public static bool responseok = false;
        //Session["responseok"] = false;

        bool IsPendioGenrated = false;
        DataTable dtAccountTypes = null;
        string _currentSelectedEmployee = string.Empty;
        //private AppDbContext db = new AppDbContext();
        GeneratePensionProcessModel generatePensionProcess;

        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;
        private AppDbContext _DbContext;

        public PensionProcessController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _DbContext = DbContext;
        }
        //
        // GET: /PensionProcess/
        public ActionResult Index()
        {
            return View("~/Views/PensionProcess/Batch/Index.cshtml", db.MasterPensioner.ToList());
        }
        public ActionResult Create()
        {
            return PartialView("~/Views/PensionProcess/Batch/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterPensioner pentionor)
        {
            if (ModelState.IsValid)
            {
                db.MasterPensioner.Add(pentionor);
                db.SaveChanges();
                return RedirectToAction("~/Views/PensionProcess/Batch/Index.cshtml", db.MasterPensioner.ToList());
            }

            return View("~/Views/PensionProcess/Batch/Create.cshtml", pentionor);
        }

        public ActionResult GeneratePensionProcess()
        {
            string disData = string.Empty;
            PensionProcessViewModel Paysearch = ShowActiveBatch();
            BindComboEmployeeType(Paysearch.EmpType);
            ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
            if (Paysearch.RegionNames != "" && Paysearch.RegionNames != null)
            {
                disData = Paysearch.RegionNames.ToLower().Trim();
            }

            ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);

            int userid = AppUserManager.GetUserId();
            var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
            var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionProcess" && x.ActionName == "GeneratePensionProcess")
                         join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                         from p in ps.DefaultIfEmpty()
                         select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Autogenerate Pensions", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

            if (model != null)
            {
                ViewBag.AddPermission = model.AddPermssion;
                ViewBag.EditPermission = model.EditPermission;
            }

            return PartialView("~/Views/PensionProcess/GeneratePensions/GeneratePensionProcess.cshtml", Paysearch);
        }

        private void BindComboEmployeeType(string type_code)
        {
            //make object to pass as parameter of search function
            DVOMasterEmpTypes objDVOMasterEmpTypes = new DVOMasterEmpTypes();
            //call getDate function of BLL
            var defaultItem = new SelectListItem { Value = "ALL", Text = "ALL" };
            List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes).Select(s => new DVOMasterEmpTypes
            {
                type_code = s.type_code,
                //description = string.Format("{0} | {1} | {2} | {3} | {4}", s.type_code, s.description, s.pay_period, s.empl_status, s.hold_pymnt)
                description = string.Format("{0} | {1}", s.type_code, s.description)
            }).ToList();
            listDVOMasterEmpTypes.Insert(0, new DVOMasterEmpTypes { type_code = "ALL", description = "ALL | ALL" }); // Insert the default item at the beginning
            var selectListItems = listDVOMasterEmpTypes
                    .Select(empType => new SelectListItem
                    {
                        Value = empType.type_code,
                        Text = empType.description
                    })
                    .ToList();

            objDVOMasterEmpTypes = null;
            //ViewBag.EmployeeType = new SelectList(selectListItems, "Value", "Text", (string.IsNullOrEmpty(EmployeeType) ? null : EmployeeType));

            ViewBag.EmployeeType = new SelectList(selectListItems, "type_code", "description", type_code);
            //check list is null or not
            if (listDVOMasterEmpTypes != null)
            {
                //make a blank object and insert into first position
                DVOMasterEmpTypes tmpDVOMasterEmpTypes = new DVOMasterEmpTypes();
                tmpDVOMasterEmpTypes.type_code = string.Empty;
                tmpDVOMasterEmpTypes.description = "-- Select --";
                tmpDVOMasterEmpTypes.pay_period = "";
                tmpDVOMasterEmpTypes.empl_status = "";
                tmpDVOMasterEmpTypes.hold_pymnt = "";
                listDVOMasterEmpTypes.Insert(0, tmpDVOMasterEmpTypes);
                //check list has some items or not
                if (listDVOMasterEmpTypes.Count > 0)
                {
                    //bind combo box with list
                    type_code = string.IsNullOrWhiteSpace(type_code) ? string.Empty : type_code;
                    ViewBag.EmployeeType = new SelectList(listDVOMasterEmpTypes, "type_code", "description", type_code);
                }
            }
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
                    pObjBatch = lstDVOPYBatchProcessStybatchr[0];
                    /*lblBatchID.Text = Convert.ToString(obj.pybatchid);
                    lblProcessStartedOn.Text = obj.startedon;
                    lblStartBy.Text = Convert.ToString(obj.insertby);
                    lblStartMachineInfo.Text = obj.insertmachineinfo;*/

                    Paysearch.pybatchid = obj.pybatchid;
                    Paysearch.searchcriteria = obj.searchcriteria;
                    //Paysearch.processstartedon = obj.startedon == null ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(obj.startedon);
                    //Paysearch.processstartedon = obj.endedon == null ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(obj.endedon);
                    Paysearch.errormessage = obj.errormessage;
                    Paysearch.status = obj.status;
                    Paysearch.RegionNames = obj.Districts;

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
                                    Paysearch.LPayDateChecked = true;
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

                    /*DVOPYBatchProcessDetailStybatchd objDVOPYBatchProcessDetailStybatchd = new DVOPYBatchProcessDetailStybatchd();
                    objDVOPYBatchProcessDetailStybatchd.pybatchid = obj.pybatchid;
                    List<DVOPYBatchProcessDetailStybatchd> listDVOPYBatchProcessDetailStybatchd = BLLPYBatchProcessDetailStybatchd.GetData(ref objDVOPYBatchProcessDetailStybatchd);
                    if (listDVOPYBatchProcessDetailStybatchd != null && listDVOPYBatchProcessDetailStybatchd.Count > 0)
                    {
                      //customDataGridview1.DataSource = listDVOPYBatchProcessDetailStybatchd;
                    }*/
                }
                //TempData["error"] = "No Active Batch for Pension Process,Please Create a New Batch & Refresh";
                //TempData["success"] = "No Active Batch for Pension Process,Generate the pension";

                return Paysearch;
            }
            catch (Exception Ex)
            {
                TempData["error"] = "Please Try Again.." + Ex.Message;
                //ExceptionManagement.ExceptionManager.Publish(Ex);
                //throw Ex;
                ExceptionManagement.ExceptionManager.Publish(Ex);
                return null;
            }

        }

        private PensionProcessViewModel ShowActiveBatch(string YN)
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
                    pObjBatch = lstDVOPYBatchProcessStybatchr[0];
                    /*lblBatchID.Text = Convert.ToString(obj.pybatchid);
                    lblProcessStartedOn.Text = obj.startedon;
                    lblStartBy.Text = Convert.ToString(obj.insertby);
                    lblStartMachineInfo.Text = obj.insertmachineinfo;*/

                    Paysearch.pybatchid = obj.pybatchid;
                    Paysearch.searchcriteria = obj.searchcriteria;
                    Paysearch.processstartedon = obj.startedon == null ? DVOApplicationUserInfo.ParseDateConvertion("01/01/0001") : DVOApplicationUserInfo.ParseDateConvertion(obj.startedon);
                    //Paysearch.processstartedon1 = obj.startedon == null ? "01/01/0001" : obj.startedon;

                    //Paysearch.processstartedon = obj.endedon == null ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(obj.endedon);
                    Paysearch.errormessage = obj.errormessage;
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
                                    Paysearch.LPayDateChecked = true;
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

                    /*DVOPYBatchProcessDetailStybatchd objDVOPYBatchProcessDetailStybatchd = new DVOPYBatchProcessDetailStybatchd();
                    objDVOPYBatchProcessDetailStybatchd.pybatchid = obj.pybatchid;
                    List<DVOPYBatchProcessDetailStybatchd> listDVOPYBatchProcessDetailStybatchd = BLLPYBatchProcessDetailStybatchd.GetData(ref objDVOPYBatchProcessDetailStybatchd);
                    if (listDVOPYBatchProcessDetailStybatchd != null && listDVOPYBatchProcessDetailStybatchd.Count > 0)
                    {
                      //customDataGridview1.DataSource = listDVOPYBatchProcessDetailStybatchd;
                    }*/
                }
                //TempData["error"] = "No Active Batch for Pension Process,Please Create a New Batch & Refresh";

                return Paysearch;
            }
            catch (Exception Ex)
            {
                TempData["error"] = "Please Try Again.." + Ex.Message;
                //ExceptionManagement.ExceptionManager.Publish(Ex);
                //throw Ex;
                return null;
            }

        }

        public JsonResult CheckProcessAddedAjax()
        {
            DVOPYBatchProcessDetailStybatchd objDVOPYBatchProcessDetailStybatchd = new DVOPYBatchProcessDetailStybatchd();
            objDVOPYBatchProcessDetailStybatchd.pybatchid = 0;
            List<DVOPYBatchProcessDetailStybatchd> listDVOPYBatchProcessDetailStybatchd = BLLPYBatchProcessDetailStybatchd.GetData(ref objDVOPYBatchProcessDetailStybatchd);
            if (listDVOPYBatchProcessDetailStybatchd != null && listDVOPYBatchProcessDetailStybatchd.Count > 0)
            {
                var entity = listDVOPYBatchProcessDetailStybatchd.Select(x => new { processendedon = x.processendedon, processstartedon = x.processstartedon, x.pybatchid, x.recordsprocessed, x.recordssearched, x.errormessage, x.processname }).ToList();
                return Json(entity, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
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
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        bool IsBatchSelected = false;
        // The pObjBatch object used to hold active batch information.
        DVOPYBatchProcessStybatchr pObjBatch = null;

        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public ActionResult GeneratePensionProcess([Bind(Include = "_AccountNumber,EmployeeCode,FirstName,LastName,EmpType,TxtEmpl_type,JobCode,PayPeriod,Title,LPayDate,LPayDateChecked,FullTime,PayrollDate,PayrollDateChecked,EOPDate,EOPDateChecked,pybatchid,batchprocessid,processname,processstartedon,processendedon,recordssearched,recordsprocessed,status,searchcriteria,errormessage,RegionNames")] PensionProcessViewModel pensionProcessHeader)
        {
            BindComboEmployeeType(pensionProcessHeader.EmpType);
            ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
            // **** Code By Himanshu Rajput****
            pensionProcessHeader.EOPDate = pensionProcessHeader.EOPDate;
            string disData = string.Empty;
            PensionProcessViewModel Paysearch = ShowActiveBatch();
            var RegionNames = pensionProcessHeader.RegionNames == null ? Paysearch.RegionNames : pensionProcessHeader.RegionNames;
            if (RegionNames != "" && RegionNames != null)
            {
                disData = RegionNames.ToLower().Trim();
            }

            ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);
            if (ModelState.IsValid)
            {
                #region Prepare Search Criteria

                DVOPayrollautopay objPayAuto = new DVOPayrollautopay();
                StringBuilder obj_stringbulder = new StringBuilder();
                pObjBatch = new DVOPYBatchProcessStybatchr();

                pObjBatch.searchcriteria = string.Empty;
                pObjBatch.pybatchid = pensionProcessHeader.pybatchid;

                objPayAuto.Process_TimeCard = "N";

                if (!string.IsNullOrWhiteSpace(pensionProcessHeader.EmployeeCode))
                {
                    objPayAuto.EmplCode = pensionProcessHeader.EmployeeCode.Trim();
                    obj_stringbulder.Append("[Employee Code: " + pensionProcessHeader.EmployeeCode.Trim() + " ]");
                    pObjBatch.searchcriteria += "Employee Code: " + pensionProcessHeader.EmployeeCode.Trim() + ",";
                }
                if (!string.IsNullOrWhiteSpace(pensionProcessHeader.FirstName))
                {
                    objPayAuto.FirstName = pensionProcessHeader.FirstName.ToString().Trim();
                    obj_stringbulder.Append("[First Name Like: " + pensionProcessHeader.FirstName.Trim() + " ]");
                    pObjBatch.searchcriteria += "First Name Like: " + pensionProcessHeader.FirstName.Trim() + ",";
                }
                if (!string.IsNullOrWhiteSpace(pensionProcessHeader.LastName))
                {
                    objPayAuto.LastName = pensionProcessHeader.LastName.ToString().Trim();
                    obj_stringbulder.Append("[Last Name Like: " + pensionProcessHeader.LastName.Trim() + " ]");
                    pObjBatch.searchcriteria += " Last Name Like: " + pensionProcessHeader.LastName.Trim() + ",";
                }
                if (!string.IsNullOrWhiteSpace(pensionProcessHeader.EmpType))
                {
                    objPayAuto.Employee_Type = pensionProcessHeader.EmpType.ToString().Trim();
                    obj_stringbulder.Append("[Employee Type Like: " + pensionProcessHeader.EmpType.Trim() + "]");
                    pObjBatch.searchcriteria += "Employee Type Like: " + pensionProcessHeader.EmpType.Trim() + ",";
                }
                if (!string.IsNullOrWhiteSpace(pensionProcessHeader.TxtEmpl_type))
                {
                    objPayAuto.Employee_Type = pensionProcessHeader.TxtEmpl_type.ToString().Trim();
                    obj_stringbulder.Append("[Employee Type Like: " + pensionProcessHeader.TxtEmpl_type.Trim() + "]");
                    pObjBatch.searchcriteria += "Employee Type Like: " + pensionProcessHeader.TxtEmpl_type.Trim() + ",";
                }
                if (!string.IsNullOrWhiteSpace(pensionProcessHeader.JobCode))
                {
                    objPayAuto.Job_Code = pensionProcessHeader.JobCode.ToString().Trim();
                    obj_stringbulder.Append("[Job Code Like: " + pensionProcessHeader.JobCode.Trim() + "]");
                    pObjBatch.searchcriteria += "Job Code Like: " + pensionProcessHeader.JobCode.Trim() + ",";
                }
                if (!string.IsNullOrWhiteSpace(pensionProcessHeader.PayPeriod))
                {
                    objPayAuto.PayPeriod = pensionProcessHeader.PayPeriod.ToString().Trim();
                    obj_stringbulder.Append("[Pay Period Like: " + pensionProcessHeader.PayPeriod.Trim() + " ]");
                    pObjBatch.searchcriteria += "Pay Period Like: " + pensionProcessHeader.PayPeriod.Trim() + ",";
                }
                if (!string.IsNullOrWhiteSpace(pensionProcessHeader.Title))
                {
                    objPayAuto.Title = pensionProcessHeader.Title.ToString().Trim();
                    obj_stringbulder.Append("[Title: " + pensionProcessHeader.Title.Trim() + " ]");
                    pObjBatch.searchcriteria += "Title: " + pensionProcessHeader.Title.Trim() + ",";
                }

                if (!string.IsNullOrWhiteSpace(pensionProcessHeader.FullTime))
                {
                    objPayAuto.FullTime = pensionProcessHeader.FullTime.ToString().Trim();
                    obj_stringbulder.Append("[Full Time: " + pensionProcessHeader.FullTime.Trim() + " ]");
                    pObjBatch.searchcriteria += "Full Time: " + pensionProcessHeader.FullTime.Trim() + ",";
                }
                pensionProcessHeader.PayrollDateChecked = pensionProcessHeader.PayrollDate == null ? false : true;
                pensionProcessHeader.EOPDateChecked = pensionProcessHeader.EOPDate == null ? false : true;
                pensionProcessHeader.LPayDateChecked = pensionProcessHeader.PayrollDate == null ? false : true;

                if (pensionProcessHeader.PayrollDateChecked)
                {
                    objPayAuto.Payroll_Date = DVOApplicationUserInfo.DateConvertion(pensionProcessHeader.PayrollDate);
                    obj_stringbulder.Append("[Payroll Date: " + DVOApplicationUserInfo.DateConvertionStr(pensionProcessHeader.PayrollDate) + "]");
                    pObjBatch.searchcriteria += "Payroll Date: " + DVOApplicationUserInfo.DateConvertionStr(pensionProcessHeader.PayrollDate) + ",";
                }
                if (pensionProcessHeader.EOPDateChecked)
                {
                    objPayAuto.EOP_Date = Convert.ToDateTime(pensionProcessHeader.EOPDate);
                }
                else
                {
                    objPayAuto.EOP_Date = DVOApplicationUserInfo.CurrentDate;
                }

                obj_stringbulder.Append("[EOP Date: " + DVOApplicationUserInfo.DateConvertionStr(pensionProcessHeader.EOPDate) + " ]");
                pObjBatch.searchcriteria += "EOP Date: " + DVOApplicationUserInfo.DateConvertionStr(pensionProcessHeader.EOPDate) + ",";

                if (pensionProcessHeader.LPayDateChecked)
                {
                    objPayAuto.LastPay = Convert.ToDateTime(pensionProcessHeader.LPayDate);
                    obj_stringbulder.Append("[Last Pay Date: " + DVOApplicationUserInfo.DateConvertionStr(pensionProcessHeader.LPayDate) + " ]");
                    pObjBatch.searchcriteria += "EOP Date: " + DVOApplicationUserInfo.DateConvertionStr(pensionProcessHeader.LPayDate) + ",";
                }
                if (!string.IsNullOrEmpty(pensionProcessHeader.RegionNames))
                {
                    var dList = pensionProcessHeader.RegionNames.Split(',');

                    string disList = string.Empty;
                    foreach (var item in dList)
                    {
                        disList = disList + "\'" + item.Trim() + "\'" + ",";
                    }
                    disList = disList.TrimEnd(',');
                    if (!string.IsNullOrWhiteSpace(pensionProcessHeader.RegionNames))
                    {
                        objPayAuto.District = disList;
                        obj_stringbulder.Append("[District IN: " + disList + " ]");
                        pObjBatch.searchcriteria += "District IN: (" + disList + ")";
                    }
                }

                /*if (satyaPayDTPicker1.Checked)
                {
                  objPayAuto.Start_date = satyaPayDTPicker1.Value;
                }*/
                objPayAuto.Start_date = DateTime.Now;

                #endregion

                #region Process Payroll..
                string keyvalue = string.Empty;
                //BLLPayrollAutopay objBLLPayrollAutopay = new BLLPayrollAutopay();
                BLLPayrollAutopayNew objBLLPayrollAutopay = new BLLPayrollAutopayNew();
                //BLLPayrollAutopayNewSingleTransaction objBLLPayrollAutopay = new BLLPayrollAutopayNewSingleTransaction();
                //MessageBox.Show(DateTime.Now.ToString());
                List<DVOPayrollProcess_PayEmployee> objDVOPayrollProcess_PayEmployeeList = objBLLPayrollAutopay.Autopay(ref objPayAuto, ref pObjBatch, true);
                //MessageBox.Show(DateTime.Now.ToString());

                List<DVOPayrollautopay> objListemplforprocess = objBLLPayrollAutopay.objListemplforprocess;

                if (objDVOPayrollProcess_PayEmployeeList.Count != 0)
                {
                    DataTable objDataTable = new DataTable();
                    objDataTable.TableName = "styemplr";
                    objDataTable.Columns.Add("v_empl_code");
                    objDataTable.Columns.Add("v_first_name");
                    objDataTable.Columns.Add("v_middle_name");
                    objDataTable.Columns.Add("v_last_name");
                    objDataTable.Columns.Add("v_eop_date");
                    objDataTable.Columns.Add("v_pay_date");
                    objDataTable.Columns.Add("v_cash_acct");
                    objDataTable.Columns.Add("v_department");
                    objDataTable.Columns.Add("v_total_hours", typeof(decimal));
                    objDataTable.Columns.Add("v_inc_gross", typeof(decimal));
                    objDataTable.Columns.Add("v_inc_taxable", typeof(decimal));
                    objDataTable.Columns.Add("v_inc_expense", typeof(decimal));
                    objDataTable.Columns.Add("v_check_amount", typeof(decimal));
                    objDataTable.Columns.Add("v_inc_net", typeof(decimal));
                    objDataTable.Columns.Add("v_ded_fica", typeof(decimal));
                    objDataTable.Columns.Add("v_ded_medicare", typeof(decimal));
                    objDataTable.Columns.Add("v_ded_fedtax", typeof(decimal));
                    objDataTable.Columns.Add("v_ded_statax", typeof(decimal));
                    objDataTable.Columns.Add("v_ded_loctax", typeof(decimal));
                    objDataTable.Columns.Add("v_ded_other", typeof(decimal));
                    objDataTable.Columns.Add("v_obl_fica", typeof(decimal));
                    objDataTable.Columns.Add("v_obl_medicare", typeof(decimal));
                    objDataTable.Columns.Add("v_obl_futa", typeof(decimal));
                    objDataTable.Columns.Add("v_obl_other", typeof(decimal));
                    objDataTable.Columns.Add("v_ded_total", typeof(decimal));
                    objDataTable.Columns.Add("v_obl_total", typeof(decimal));
                    objDataTable.Columns.Add("ErrorMessage1");
                    objDataTable.Columns.Add("ErrorMessage2");
                    objDataTable.Columns.Add("ErrorMessage3");
                    objDataTable.Columns.Add("SearchCriteria");
                    int lastacct_no = 0;
                    for (int i = 0; i < objDVOPayrollProcess_PayEmployeeList.Count; i++)
                    {
                        DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                        objDVOPayrollProcess_PayEmployee = objDVOPayrollProcess_PayEmployeeList[i];
                        if (objDVOPayrollProcess_PayEmployee.Cash_acct_no != lastacct_no || i == 0)
                        {
                            keyvalue = string.Empty;
                            int id = 0;
                            string acct_type = string.Empty;
                            string acct_desc = string.Empty;
                            BLLCommonUtilities.GetAccountInformation(objDVOPayrollProcess_PayEmployee.Cash_acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                        }
                        DataRow dr = objDataTable.NewRow();
                        dr["v_empl_code"] = objListemplforprocess[i].EmplCode;
                        dr["v_first_name"] = objListemplforprocess[i].FirstName;
                        dr["v_middle_name"] = null;
                        dr["v_last_name"] = objListemplforprocess[i].LastName;
                        dr["v_eop_date"] = objDVOPayrollProcess_PayEmployee.eop_date;
                        dr["v_pay_date"] = objDVOPayrollProcess_PayEmployee.pay_date;
                        dr["v_cash_acct"] = keyvalue;
                        dr["v_department"] = objDVOPayrollProcess_PayEmployee.Department;
                        dr["v_total_hours"] = objDVOPayrollProcess_PayEmployee.total_hours;
                        dr["v_inc_gross"] = objDVOPayrollProcess_PayEmployee.inc_gross;
                        dr["v_inc_taxable"] = objDVOPayrollProcess_PayEmployee.inc_taxable;
                        dr["v_inc_expense"] = objDVOPayrollProcess_PayEmployee.inc_expense;
                        dr["v_check_amount"] = objDVOPayrollProcess_PayEmployee.cash_amount;
                        dr["v_inc_net"] = objDVOPayrollProcess_PayEmployee.inc_net;
                        dr["v_ded_fica"] = objDVOPayrollProcess_PayEmployee.ded_fica;
                        dr["v_ded_medicare"] = objDVOPayrollProcess_PayEmployee.ded_medicare;
                        dr["v_ded_fedtax"] = objDVOPayrollProcess_PayEmployee.ded_fedtax;
                        dr["v_ded_statax"] = objDVOPayrollProcess_PayEmployee.ded_statax;
                        dr["v_ded_loctax"] = objDVOPayrollProcess_PayEmployee.ded_loctax;
                        dr["v_ded_other"] = objDVOPayrollProcess_PayEmployee.ded_other;
                        dr["v_obl_fica"] = objDVOPayrollProcess_PayEmployee.obl_fica;
                        dr["v_obl_medicare"] = objDVOPayrollProcess_PayEmployee.obl_medicare;
                        dr["v_obl_futa"] = objDVOPayrollProcess_PayEmployee.obl_futa;
                        dr["v_obl_other"] = objDVOPayrollProcess_PayEmployee.obl_other;
                        dr["v_ded_total"] = objDVOPayrollProcess_PayEmployee.ded_other;
                        dr["v_obl_total"] = objDVOPayrollProcess_PayEmployee.obl_total;
                        dr["ErrorMessage1"] = objBLLPayrollAutopay.ErrMsg1;
                        dr["ErrorMessage2"] = objBLLPayrollAutopay.ErrMsg2;
                        dr["ErrorMessage3"] = "";
                        dr["SearchCriteria"] = obj_stringbulder.ToString().Trim();
                        objDataTable.Rows.Add(dr);
                        lastacct_no = objDVOPayrollProcess_PayEmployee.Cash_acct_no;

                    }
                    this.HttpContext.Session["ReportName"] = "RptPrintAutoPayroll.rpt";
                    this.HttpContext.Session["rptSource"] = objDataTable;
                    @ViewBag.RptLoad = "RptPrintAutoPayroll";

                    // get the physical path of .Rpt file related to this form's report.
                    //string rptpath = Application.StartupPath + System.Configuration.ConfigurationSettings.AppSettings["ReportsPayrollChecks"].Trim() + @"\RptPrintAutoPayroll.rpt";
                    ////define ReportDocument type object.
                    //ReportDocument reportdocument = new ReportDocument();
                    ////load .Rpt file to reportDocument
                    //reportdocument.Load(rptpath);
                    ////assign datasource to reportDocument.
                    //reportdocument.SetDataSource(objDataTable);
                    ////set report-source of ReportViewer.
                    //ReportViewer.ReportSource = reportdocument;
                    ////make reportDocument to null.
                    //reportdocument = null;
                    //objDataTable.Dispose();
                    //SatyaPay.StyleUtility.Repoadminrts_Splasher.Close();
                }
                else
                {
                    //ReportViewer.ReportSource = null;
                    //SatyaPay.StyleUtility.Reports_Splasher.Close();
                    //Utilities.ShowMessage("No Element to Process", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TempData["error"] = "No Element to Process. ";
                    return View("~/Views/PensionProcess/GeneratePensions/GeneratePensionProcess.cshtml", pensionProcessHeader);
                }
                #endregion


                //TempData["success"] = "Generate Pension Process has been created successfully. ";

                return View("~/Views/PensionProcess/GeneratePensions/GeneratePensionProcess.cshtml", pensionProcessHeader);
                //return PartialView("~/Views/PensionProcess/GeneratePensions/Index.cshtml");
            }

            return View("~/Views/PensionProcess/GeneratePensions/GeneratePensionProcess.cshtml", pensionProcessHeader);
        }

        public ActionResult PrintExceptionReport()
        {
            int userid = AppUserManager.GetUserId();
            var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
            var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionProcess" && x.ActionName == "GeneratePaySlipDetails")
                         join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                         from p in ps.DefaultIfEmpty()
                         select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Generate Details For Payslip", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

            if (model != null)
            {
                ViewBag.AddPermission = model.AddPermssion;
                ViewBag.EditPermission = model.EditPermission;
            }
            string disData = string.Empty;
            PensionProcessViewModel Paysearch = ShowActiveBatch();
            BindComboEmployeeType(Paysearch.EmpType);
            ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
            if (Paysearch.RegionNames != "" && Paysearch.RegionNames != null)
            {
                disData = Paysearch.RegionNames.ToLower().Trim();
            }
            ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);


            return PartialView("~/Views/PensionProcess/Enteries/PrintExceptionReport.cshtml", Paysearch);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrintExceptionReport([Bind(Include = "_AccountNumber,EmployeeCode,FirstName,LastName,EmpType,TxtEmpl_type,JobCode,PayPeriod,Title,LPayDate,LPayDateChecked,FullTime,PayrollDate,PayrollDateChecked,EOPDate,EOPDateChecked,pybatchid,batchprocessid,processname,processstartedon,processendedon,recordssearched,recordsprocessed,status,searchcriteria,errormessage,RegionNames")] PensionProcessViewModel pensionProcessHeader)
        {
            DataSet ds = null;
            StringBuilder obj_stringbulder = null;
            DVOMasterEmployee objDVOSearcCtriaStyemplr = null;
            BLLPyPostExceptionNew objBLLPyPostException = null;
            DVOPayrollProcess_PayEmployee objDVOSearcCtriaProcess_PayEmployee = null;
            try
            {
                if (ModelState.IsValid)
                {
                    #region Prepare Search Criteria
                    List<DVOPYBatchProcessStybatchr> lstDVOPYBatchProcessStybatchr = BLLPYBatchProcessStybatchr.GetActiveBatch();
                    if (lstDVOPYBatchProcessStybatchr != null && lstDVOPYBatchProcessStybatchr.Count > 0)
                    {
                        pObjBatch = lstDVOPYBatchProcessStybatchr[0];
                    }
                    obj_stringbulder = new StringBuilder();
                    objDVOSearcCtriaStyemplr = new DVOMasterEmployee();
                    objDVOSearcCtriaProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();

                    pensionProcessHeader.PayrollDateChecked = pensionProcessHeader.PayrollDate == null ? false : true;
                    pensionProcessHeader.EOPDateChecked = pensionProcessHeader.EOPDate == null ? false : true;
                    pensionProcessHeader.LPayDateChecked = pensionProcessHeader.PayrollDate == null ? false : true;

                    if (!string.IsNullOrWhiteSpace(pensionProcessHeader.EmployeeCode))
                    {
                        objDVOSearcCtriaProcess_PayEmployee.EmplCode = pensionProcessHeader.EmployeeCode.ToString().Trim();
                        obj_stringbulder.Append("[Employee Code =" + pensionProcessHeader.EmployeeCode.ToString().Trim() + " ]");
                    }
                    if (!string.IsNullOrWhiteSpace(pensionProcessHeader.FirstName))
                    {
                        objDVOSearcCtriaStyemplr.FirstName = pensionProcessHeader.FirstName.ToString().Trim();
                        obj_stringbulder.Append("[First Name =" + pensionProcessHeader.FirstName.ToString().Trim() + " ]");
                    }
                    if (!string.IsNullOrWhiteSpace(pensionProcessHeader.LastName))
                    {
                        objDVOSearcCtriaStyemplr.LastName = pensionProcessHeader.LastName.ToString().Trim();
                        obj_stringbulder.Append("[Last Name =" + pensionProcessHeader.LastName.ToString().Trim() + " ]");
                    }
                    if (!string.IsNullOrWhiteSpace(pensionProcessHeader.EmpType))
                    {
                        objDVOSearcCtriaStyemplr.TypeCode = pensionProcessHeader.EmpType.ToString().Trim();
                        obj_stringbulder.Append("[Employee Type =" + pensionProcessHeader.EmpType.ToString().Trim() + " ]");
                    }
                    if (!string.IsNullOrWhiteSpace(pensionProcessHeader.TxtEmpl_type))
                    {
                        objDVOSearcCtriaStyemplr.TypeCode = pensionProcessHeader.TxtEmpl_type.ToString().Trim();
                        obj_stringbulder.Append("[Employee Type = " + pensionProcessHeader.TxtEmpl_type.Trim() + "]");
                    }
                    if (!string.IsNullOrWhiteSpace(pensionProcessHeader.JobCode))
                    {
                        objDVOSearcCtriaStyemplr.JobCode = pensionProcessHeader.JobCode.ToString().Trim();
                        obj_stringbulder.Append("[Job Code =" + pensionProcessHeader.JobCode.ToString().Trim() + " ]");
                    }
                    if (!string.IsNullOrWhiteSpace(pensionProcessHeader.PayPeriod))
                    {
                        objDVOSearcCtriaStyemplr.PayPeriod = pensionProcessHeader.PayPeriod.ToString().Trim();
                        obj_stringbulder.Append("[Pay Period =" + pensionProcessHeader.PayPeriod.ToString().Trim() + " ]");
                    }
                    if (!string.IsNullOrWhiteSpace(pensionProcessHeader.Title))
                    {
                        objDVOSearcCtriaStyemplr.JobTitle = pensionProcessHeader.Title.ToString().Trim();
                        obj_stringbulder.Append("[Job Title =" + pensionProcessHeader.Title.ToString().Trim() + " ]");
                    }
                    if (pensionProcessHeader.LPayDateChecked)
                    {
                        objDVOSearcCtriaProcess_PayEmployee.pay_date = Convert.ToDateTime(pensionProcessHeader.LPayDate);
                        obj_stringbulder.Append("[Last Pay Date =" + pensionProcessHeader.LPayDate.ToString().Trim() + " ]");
                    }
                    if (pensionProcessHeader.EOPDateChecked)
                    {
                        objDVOSearcCtriaProcess_PayEmployee.eop_date = Convert.ToDateTime(pensionProcessHeader.EOPDate);
                        obj_stringbulder.Append("[End Of Period =" + pensionProcessHeader.EOPDate.ToString().Trim() + " ]");
                    }

                    if (!string.IsNullOrEmpty(pensionProcessHeader.RegionNames))
                    {
                        var dList = pensionProcessHeader.RegionNames.Split(',');

                        string disList = string.Empty;
                        foreach (var item in dList)
                        {
                            disList = disList + "\'" + item.Trim() + "\'" + ",";
                        }
                        disList = disList.TrimEnd(',');
                        if (!string.IsNullOrWhiteSpace(pensionProcessHeader.RegionNames))
                        {
                            objDVOSearcCtriaStyemplr.District = disList;
                            obj_stringbulder.Append("[District IN: " + disList + " ]");
                        }
                    }

                    //if (!string.IsNullOrWhiteSpace(pensionProcessHeader.RegionNames))
                    //{
                    //  objDVOSearcCtriaStyemplr.District = pensionProcessHeader.RegionNames.ToString().Trim();
                    //  obj_stringbulder.Append("[District =" + pensionProcessHeader.RegionNames.ToString().Trim() + " ]");
                    //}


                    //if (mcg_cash_acct_no.Text != "")
                    //{
                    //  objDVOSearcCtriaProcess_PayEmployee.Cash_acct_no = Convert.ToInt32(mcg_cash_acct_no.SelectedValue);
                    //  obj_stringbulder.Append("[Cash Account Number =" + mcg_cash_acct_no.ColumnValues[0].ToString().Trim() + " ]");
                    //}

                    #endregion

                    #region Process

                    objBLLPyPostException = new BLLPyPostExceptionNew();
                    //BLLPyPostException objBLLPyPostException1 = new BLLPyPostException();
                    ds = objBLLPyPostException.GetExceptionReports(ref objDVOSearcCtriaProcess_PayEmployee, ref objDVOSearcCtriaStyemplr, "CHECK", ref pObjBatch);
                    //ds.WriteXmlSchema(@"D:\Sujeet_Sir\Project\JKPS\App.Web\Reports\DSExceptionReports.xsd");

                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            // get the physical path of .Rpt file related to this form's report.
                            string rptpath = "RptPrintExceptionReportWages.rpt";
                            if (!objDVOSearcCtriaStyemplr.TypeCode.ToString().Contains("WAG"))
                            {
                                rptpath = "RptPrintExceptionReport.rpt";
                            }

                            this.HttpContext.Session["ReportName"] = rptpath;
                            this.HttpContext.Session["rptSource"] = ds;
                            @ViewBag.RptLoad = rptpath;

                        }
                        else
                        {
                            this.HttpContext.Session["ReportName"] = null;
                            this.HttpContext.Session["rptSource"] = null;
                            @ViewBag.RptLoad = "";
                        }
                    #endregion

                    BindComboEmployeeType(pensionProcessHeader.EmpType);
                    ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
                    string disData = string.Empty;
                    PensionProcessViewModel Paysearch = ShowActiveBatch();
                    var RegionNames = pensionProcessHeader.RegionNames == null ? Paysearch.RegionNames : pensionProcessHeader.RegionNames;
                    if (RegionNames != "" && RegionNames != null)
                    {
                        disData = RegionNames.ToLower().Trim();
                    }

                    ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);

                    return View("~/Views/PensionProcess/Enteries/PrintExceptionReport.cshtml", pensionProcessHeader);
                }
            }
            catch (Exception ex)
            {
                this.HttpContext.Session["ReportName"] = null;
                this.HttpContext.Session["rptSource"] = null;
                @ViewBag.RptLoad = "";
                BindComboEmployeeType(pensionProcessHeader.EmpType);
                ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
                TempData["error"] = "Please Try Again..." + ex.Message;
                return View("~/Views/PensionProcess/Enteries/PrintExceptionReport.cshtml", pensionProcessHeader);
            }
            finally
            {
                if (ds != null)
                    ds.Dispose();
                if (objDVOSearcCtriaStyemplr != null)
                    objDVOSearcCtriaStyemplr.Dispose();
                if (objBLLPyPostException != null)
                    objBLLPyPostException.Dispose();
                if (objDVOSearcCtriaProcess_PayEmployee != null)
                    objDVOSearcCtriaProcess_PayEmployee.Dispose();

                ds = null;
                obj_stringbulder = null;
                objDVOSearcCtriaStyemplr = null;
                objBLLPyPostException = null;
                objDVOSearcCtriaProcess_PayEmployee = null;
                // Collect all generations of memory.
            }
            return View("~/Views/PensionProcess/Enteries/PrintExceptionReport.cshtml", pensionProcessHeader);

        }

        private void BindCombo_CashCheckingAccounts(string acct_no)
        {
            //make object to pass as parameter of search function
            DVOOSCheckingAccount objDVOOSCheckingAccount = new DVOOSCheckingAccount();
            //call getDate function of BLL
            List<DVOOSCheckingAccount> listDVOOSCheckingAccount = BLLOSCheckingAccount.GetCheckingAccount(ref objDVOOSCheckingAccount).Select(s => new DVOOSCheckingAccount
            {
                acct_no = s.acct_no,
                keyvalue = string.Format("{0} | {1} | {2}", s.keyvalue, s.acct_desc, s.accounttype)
            }).ToList();

            objDVOOSCheckingAccount = null;
            ViewBag.PayrollCashAccount = new SelectList(listDVOOSCheckingAccount, "acct_no", "keyvalue", acct_no);
            //check list is null or not
            if (listDVOOSCheckingAccount != null)
            {
                //make a blank object and insert into first position
                DVOOSCheckingAccount tmpDVOOSCheckingAccount = new DVOOSCheckingAccount();
                tmpDVOOSCheckingAccount.keyvalue = "-- Select --";
                tmpDVOOSCheckingAccount.acct_desc = string.Empty;
                tmpDVOOSCheckingAccount.accounttype = "";
                tmpDVOOSCheckingAccount.acct_no = 0;
                listDVOOSCheckingAccount.Insert(0, tmpDVOOSCheckingAccount);
                if (listDVOOSCheckingAccount.Count > 0)
                {
                    acct_no = string.IsNullOrWhiteSpace(acct_no) ? string.Empty : acct_no;
                    ViewBag.PayrollCashAccount = new SelectList(listDVOOSCheckingAccount, "acct_no", "keyvalue", acct_no);
                }
            }
        }

        private void BindCheckNo(string StartingChequeNo)
        {
            int check_no = ReportingUtilities.starting_check_no();
            ViewBag.check_no = string.IsNullOrWhiteSpace(StartingChequeNo) ? check_no.ToString() : StartingChequeNo;
        }


        public ActionResult GeneratePaySlipDetails()
        {
            string disData = string.Empty;
            PensionProcessViewModel Paysearch = ShowActiveBatch();
            BindCombo_CashCheckingAccounts(string.Empty);
            BindCheckNo(string.Empty);
            BindComboEmployeeType(Paysearch.EmpType);
            if (Paysearch.RegionNames != "" && Paysearch.RegionNames != null)
            {
                disData = Paysearch.RegionNames.ToLower().Trim();
            }
            GeneratePensionProcessModel generatePaySlipDetails = new GeneratePensionProcessModel();
            generatePaySlipDetails.DirectDepositsCheques = "Y";
            //generatePaySlipDetails.PayrollCashAccount = ViewBag.PayrollCashAccount;
            generatePaySlipDetails.StartingChequeNo = ViewBag.check_no;
            generatePaySlipDetails.EmployeeType = Paysearch.EmpType;
            ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);

            int userid = AppUserManager.GetUserId();
            var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
            var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionProcess" && x.ActionName == "GeneratePaySlipDetails")
                         join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                         from p in ps.DefaultIfEmpty()
                         select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Generate Details For Payslip", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

            if (model != null)
            {
                ViewBag.AddPermission = model.AddPermssion;
                ViewBag.EditPermission = model.EditPermission;
            }

            return PartialView("~/Views/PensionProcess/Payslips/GeneratePaySlipDetails.cshtml", generatePaySlipDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeneratePaySlipDetails([Bind(Include = "DirectDepositsCheques,PayrollCashAccount,EmpType,StartingChequeNo,RegionNames")] GeneratePensionProcessModel generatePensionProcessModel, FormCollection form)
        {
            try
            {
                BindCombo_CashCheckingAccounts(generatePensionProcessModel.PayrollCashAccount);
                BindCheckNo(generatePensionProcessModel.StartingChequeNo);
                string disData = string.Empty;
                PensionProcessViewModel Paysearch = ShowActiveBatch();
                BindComboEmployeeType(generatePensionProcessModel.EmpType);
                var RegionNames = generatePensionProcessModel.RegionNames == null ? Paysearch.RegionNames : generatePensionProcessModel.RegionNames;
                if (RegionNames != "" && RegionNames != null)
                {
                    disData = RegionNames.ToLower().Trim();
                }

                ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);

                #region Execute Report
                //Added by Neeraj on 16/07/2015 , To implement batch process, Nedd to add
                //A parameter 'ref pObjBatch' in 'GetPayslipDetails' function
                //and remove comment from the code written for batch process logic in 'PYBatchManagement' region.
                //and remove comment from 'pObjBatch.searchcriteria'.
                #region PYBatchManagement

                // The pObjBatch object used to hold active batch information.
                DVOPYBatchProcessStybatchr pObjBatch = null;
                //Get Payroll Active batch,if any. 
                List<DVOPYBatchProcessStybatchr> objBatchList = BLLPYBatchProcessStybatchr.GetActiveBatch();
                if (objBatchList != null && objBatchList.Count > 0)
                {
                    pObjBatch = objBatchList[0];
                }
                else
                {
                    //Utilities.ShowMessage("No Active Batch for Payroll Process,Please Create a New Batch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TempData["error"] = "No Active Batch for Payroll Process,Please Create a New Batch";
                    return PartialView("~/Views/PensionProcess/Payslips/GeneratePaySlipDetails.cshtml", generatePensionProcessModel);
                }
                #endregion PYBatchManagement

                pObjBatch.searchcriteria = string.Empty;
                DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.EmpType))
                {
                    objDVOPayrollProcess_PayEmployee.TypeCode = generatePensionProcessModel.EmpType.Trim().Replace("*", "");
                    pObjBatch.searchcriteria = "Type Code =" + objDVOPayrollProcess_PayEmployee.TypeCode + " ,";
                }
                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.DirectDepositsCheques))
                {
                    objDVOPayrollProcess_PayEmployee.deposit = generatePensionProcessModel.DirectDepositsCheques;
                    pObjBatch.searchcriteria = "Deposit=" + objDVOPayrollProcess_PayEmployee.deposit + ",";
                }

                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.PayrollCashAccount))
                {
                    objDVOPayrollProcess_PayEmployee.Cash_acct_no = Convert.ToInt32(generatePensionProcessModel.PayrollCashAccount);
                    pObjBatch.searchcriteria = "Cash Account No=" + objDVOPayrollProcess_PayEmployee.Cash_acct_no;
                }
                if (!string.IsNullOrEmpty(generatePensionProcessModel.RegionNames))
                {
                    var dList = generatePensionProcessModel.RegionNames.Split(',');

                    string disList = string.Empty;
                    foreach (var item in dList)
                    {
                        disList = disList + "\'" + item.Trim() + "\'" + ",";
                    }
                    disList = disList.TrimEnd(',');
                    if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.RegionNames))
                    {
                        objDVOPayrollProcess_PayEmployee.District = disList;
                        pObjBatch.searchcriteria = "District IN (" + objDVOPayrollProcess_PayEmployee.District + ")";

                    }
                }

                //if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.RegionNames))
                //{
                //    objDVOPayrollProcess_PayEmployee.District = generatePensionProcessModel.RegionNames;
                //    pObjBatch.searchcriteria = "District=" + objDVOPayrollProcess_PayEmployee.District;
                //}
                DataTable objDataTable = BLLGeneratePaySlipDetails.GetPayslipDetails(ref objDVOPayrollProcess_PayEmployee, ref pObjBatch);
                if (objDataTable.Rows.Count != 0)
                {
                    this.HttpContext.Session["ReportName"] = "rptGeneratePaySlipDetails.rpt";
                    this.HttpContext.Session["rptSource"] = objDataTable;
                    @ViewBag.RptLoad = "rptGeneratePaySlipDetails.rpt";
                }
                else
                {
                    //ReportViewer.ReportSource = null;
                    //SatyaPay.StyleUtility.Reports_Splasher.Close();
                    //Utilities.ShowMessage("No Elements to process", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TempData["error"] = "No Element to process.";
                }

                return PartialView("~/Views/PensionProcess/Payslips/GeneratePaySlipDetails.cshtml", generatePensionProcessModel);
                #endregion
            }
            catch (Exception ex)
            {
                //ReportViewer.ReportSource = null;
                //SatyaPay.StyleUtility.Reports_Splasher.Close();
                //ExceptionManagement.ExceptionManager.Publish(ex);
                //MessageBox.Show(ex.Message, "Satya Pay.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TempData["error"] = "Please Try Again..." + ex.Message;
                return PartialView("~/Views/PensionProcess/Payslips/GeneratePaySlipDetails.cshtml", generatePensionProcessModel);
            }
        }

        public ActionResult DirectDepositEntries()
        {
            string disData = string.Empty;
            PensionProcessViewModel Paysearch = ShowActiveBatch();
            BindComboEmployeeType(Paysearch.EmpType);
            generatePensionProcess = new GeneratePensionProcessModel();
            generatePensionProcess.EmpType = Paysearch.EmpType;

            if (Paysearch.RegionNames != "" && Paysearch.RegionNames != null)
            {
                disData = Paysearch.RegionNames.ToLower().Trim();
            }
            ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);

            return PartialView("~/Views/PensionProcess/DirectDeposits/DirectDepositEntries.cshtml", generatePensionProcess);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DirectDepositEntries([Bind(Include = "EmpType,RegionNames")] GeneratePensionProcessModel generatePensionProcessModel, FormCollection form)
        {
            try
            {
                string disData = string.Empty;
                PensionProcessViewModel Paysearch = ShowActiveBatch();
                BindComboEmployeeType(generatePensionProcessModel.EmpType);
                var RegionNames = generatePensionProcessModel.RegionNames == null ? Paysearch.RegionNames : generatePensionProcessModel.RegionNames;
                if (RegionNames != "" && RegionNames != null)
                {
                    disData = RegionNames.ToLower().Trim();
                }

                ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);

                #region Execute Report
                //Added by Neeraj on 16/07/2015 , To implement batch process, Nedd to add
                //A parameter 'ref pObjBatch' in 'GetDirectDepositeInfo' function
                //and remove comment from the code written for batch process logic in 'PYBatchManagement' region.
                #region PYBatchManagement


                // The pObjBatch object used to hold active batch information.
                DVOPYBatchProcessStybatchr pObjBatch = null;
                //Get Payroll Active batch,if any. 
                List<DVOPYBatchProcessStybatchr> objBatchList = BLLPYBatchProcessStybatchr.GetActiveBatch();
                if (objBatchList != null && objBatchList.Count > 0)
                {
                    pObjBatch = objBatchList[0];
                }
                else
                {
                    //Utilities.ShowMessage("No Active Batch for Payroll Process,Please Create a New Batch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TempData["error"] = "No Active Batch for Payroll Process,Please Create a New Batch";
                    return PartialView("~/Views/PensionProcess/DirectDeposits/DirectDepositEntries.cshtml", generatePensionProcessModel);
                }
                #endregion PYBatchManagement

                pObjBatch.searchcriteria = string.Empty;
                DvoDirectDepositEntries ObjDvoDirectDepositEntries = new DvoDirectDepositEntries();
                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.EmpType))
                {
                    ObjDvoDirectDepositEntries.type_code = generatePensionProcessModel.EmpType.Trim().Replace("*", "");
                    pObjBatch.searchcriteria = "Type Code =" + ObjDvoDirectDepositEntries.type_code + " ,";
                }
                if (!string.IsNullOrEmpty(generatePensionProcessModel.RegionNames))
                {
                    var dList = generatePensionProcessModel.RegionNames.Split(',');

                    string disList = string.Empty;
                    foreach (var item in dList)
                    {
                        disList = disList + "\'" + item.Trim() + "\'" + ",";
                    }
                    disList = disList.TrimEnd(',');
                    if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.RegionNames))
                    {
                        ObjDvoDirectDepositEntries.District = disList;
                        pObjBatch.searchcriteria = "District IN" + ObjDvoDirectDepositEntries.District;


                    }
                }

                //if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.RegionNames))
                //{
                //    ObjDvoDirectDepositEntries.District = generatePensionProcessModel.RegionNames.Trim().Replace("*", "");
                //    pObjBatch.searchcriteria = "District =" + ObjDvoDirectDepositEntries.District + " ,";
                //}
                DataTable objDataTable = ReportingUtilities.GetDirectDepositeInfo(ref ObjDvoDirectDepositEntries, ref pObjBatch);

                //objDataTable.WriteXmlSchema(@"D:\Sujeet_Sir\Himanshu_Rajput\App.Web\Reports\DSDirectDepositEntries.xsd");
                //objDataTable.WriteXmlSchema(@"D:\Sujeet_Sir\Himanshu_Rajput\App.Web\Reports\DataTable1.xsd");

                if (objDataTable.Rows.Count != 0)
                {
                    this.HttpContext.Session["ReportName"] = "RptDirectDepositEntries.rpt";
                    this.HttpContext.Session["rptSource"] = objDataTable;
                    @ViewBag.RptLoad = "RptDirectDepositEntries.rpt";
                }
                else
                {
                    //ReportViewer.ReportSource = null;
                    //SatyaPay.StyleUtility.Reports_Splasher.Close();
                    //Utilities.ShowMessage("No Elements to process", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TempData["error"] = "No Element to process.";
                }
                return PartialView("~/Views/PensionProcess/DirectDeposits/DirectDepositEntries.cshtml", generatePensionProcessModel);
                #endregion
            }
            catch (Exception ex)
            {
                //ReportViewer.ReportSource = null;
                //SatyaPay.StyleUtility.Reports_Splasher.Close();
                //ExceptionManagement.ExceptionManager.Publish(ex);
                //MessageBox.Show(ex.Message, "Satya Pay.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TempData["error"] = "Please Try Again..." + ex.Message;
                return PartialView("~/Views/PensionProcess/DirectDeposits/DirectDepositEntries.cshtml", generatePensionProcessModel);
            }
        }

        public ActionResult PrintCheques()
        {
            BindCheckNo(string.Empty);
            BindComboEmployeeType(string.Empty);
            BindCombo_CashCheckingAccounts(string.Empty);
            generatePensionProcess = new GeneratePensionProcessModel();
            generatePensionProcess.DirectDepositsCheques = "N";
            generatePensionProcess.StartingChequeNo = ViewBag.check_no;

            return PartialView("~/Views/PensionProcess/Payslips/PrintCheques.cshtml", generatePensionProcess);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrintCheques([Bind(Include = "DirectDepositsCheques,PayrollCashAccount,EmployeeType,StartingChequeNo")] GeneratePensionProcessModel generatePensionProcessModel, FormCollection form)
        {
            try
            {
                BindCheckNo(generatePensionProcessModel.StartingChequeNo);
                BindComboEmployeeType(generatePensionProcessModel.EmployeeType);
                BindCombo_CashCheckingAccounts(generatePensionProcessModel.PayrollCashAccount);
                #region Execute Report
                //Added by Neeraj on 16/07/2015 , To implement batch process, Nedd to add
                //A parameter 'ref pObjBatch' in 'GetPayslipDetails' function
                //and remove comment from the code written for batch process logic in 'PYBatchManagement' region.
                //and remove comment from 'pObjBatch.searchcriteria'.
                #region PYBatchManagement
                int recordsSearched = 0;
                // The pObjBatch object used to hold active batch information.
                DVOPYBatchProcessStybatchr pObjBatch = null;
                //Get Payroll Active batch,if any. 
                List<DVOPYBatchProcessStybatchr> objBatchList = BLLPYBatchProcessStybatchr.GetActiveBatch();
                if (objBatchList != null && objBatchList.Count > 0)
                {
                    pObjBatch = objBatchList[0];
                }
                else
                {
                    //Utilities.ShowMessage("No Active Batch for Payroll Process,Please Create a New Batch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TempData["error"] = "No Active Batch for Payroll Process,Please Create a New Batch";
                    return PartialView("~/Views/PensionProcess/Payslips/PrintCheques.cshtml", generatePensionProcessModel);
                }
                #endregion PYBatchManagement

                pObjBatch.searchcriteria = string.Empty;
                DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.EmployeeType))
                {
                    objDVOPayrollProcess_PayEmployee.TypeCode = generatePensionProcessModel.EmployeeType.Trim().Replace("*", "");
                    pObjBatch.searchcriteria = "Type Code =" + objDVOPayrollProcess_PayEmployee.TypeCode + " ,";
                }
                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.DirectDepositsCheques))
                {
                    objDVOPayrollProcess_PayEmployee.deposit = generatePensionProcessModel.DirectDepositsCheques.Trim();
                    pObjBatch.searchcriteria = "Deposit=" + objDVOPayrollProcess_PayEmployee.deposit + ",";
                }

                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.PayrollCashAccount))
                {
                    objDVOPayrollProcess_PayEmployee.Cash_acct_no = Convert.ToInt32(generatePensionProcessModel.PayrollCashAccount);
                    pObjBatch.searchcriteria = "Cash Account No=" + objDVOPayrollProcess_PayEmployee.Cash_acct_no;
                }
                DataTable objDataTable = BLLGeneratePaySlipDetails.ShowPayrollChecks(ref objDVOPayrollProcess_PayEmployee, ref pObjBatch, out recordsSearched);
                //objDataTable.WriteXmlSchema(@"D:\Sujeet_Sir\Project\JKPS\App.Web\Reports\DsPayslipDetail.xsd");
                if (objDataTable.Rows.Count != 0)
                {
                    this.HttpContext.Session["ReportName"] = "RptPrintPayrollChecksNew.rpt";
                    this.HttpContext.Session["rptSource"] = objDataTable;
                    @ViewBag.RptLoad = "RptPrintPayrollChecksNew.rpt";
                }
                else
                {
                    //ReportViewer.ReportSource = null;
                    //SatyaPay.StyleUtility.Reports_Splasher.Close();
                    //Utilities.ShowMessage("No Elements to process", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TempData["error"] = "No Elements to process.";
                }

                return PartialView("~/Views/PensionProcess/Payslips/PrintCheques.cshtml", generatePensionProcessModel);
                #endregion
            }
            catch (Exception ex)
            {
                //ReportViewer.ReportSource = null;
                //SatyaPay.StyleUtility.Reports_Splasher.Close();
                //ExceptionManagement.ExceptionManager.Publish(ex);
                //MessageBox.Show(ex.Message, "Satya Pay.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TempData["error"] = "Please Try Again..." + ex.Message;
                return PartialView("~/Views/PensionProcess/Payslips/PrintCheques.cshtml", generatePensionProcessModel);
            }
        }

        /// <summary>
        /// function to fill account-type in combo box
        /// </summary>
        public void FillAccountType()
        {
            try
            {
                string NonBudgetedAccountTypes = "||BELLIN||";
                bool _Budgeted = false;

                //initialize DVOGLAccountTypeMaintenance type object to send as parameter in function to get Account-types
                DVOGLAccountTypeMaintenance objGLAccountType = new DVOGLAccountTypeMaintenance();
                //initialize list of DVOGLAccountTypeMaintenance type objects 
                List<DVOGLAccountTypeMaintenance> objListGLAccountType = BLLGLAccountTypeMaintenance.GetAccountTypeMaintenance(ref objGLAccountType).Select(x =>
                  new DVOGLAccountTypeMaintenance
                  {
                      id = x.id,
                      desc = string.Format("{0} | {1} | {2}", x.id, x.accounttype, x.desc),
                  }).ToList();

                ViewBag.accounttype = new SelectList(objListGLAccountType, "id", "desc");
                if (objListGLAccountType != null && objListGLAccountType.Count > 0)
                {
                    if (_Budgeted)
                    {
                        objListGLAccountType.RemoveAll(delegate (DVOGLAccountTypeMaintenance objParm)
                        {
                            return NonBudgetedAccountTypes.Contains("||" + objParm.accounttype.Trim() + "||");
                        });
                    }
                    //make blank DVOGLAccountTypeMaintenance type object to set as default selected item in account-type combo box
                    objGLAccountType.id = 0;
                    objGLAccountType.accounttype = "-- Select Account Type --";
                    objGLAccountType.desc = string.Empty;
                    objListGLAccountType.Insert(0, objGLAccountType);
                    ViewBag.accounttype = new SelectList(objListGLAccountType, "id", "desc", (objListGLAccountType.Count > 1 ? 2 : objListGLAccountType.Count));
                }
            }
            catch { }
        }
        private void GetSegments()
        {
            DataSet dsSegments;
            string _selectq = "v_segmentid = " + 1;
            DVOFlexSegment objFlexSegment = new DVOFlexSegment();
            dsSegments = BLLFlexSegment.GetFlexSegmentItems(ref objFlexSegment);
            DataRow[] drs = dsSegments.Tables[0].Select(_selectq);
            DataTable dtseg = dsSegments.Tables[0].Clone();
            foreach (DataRow drseg in drs)
                dtseg.ImportRow(drseg);
            //if (dtseg.Rows.Count > 0)
            //{
            //  //make a new row as rows in dataset to make default selected item in new combo box
            //  DataRow drt = dtseg.NewRow();
            //  drt[1] = -1;
            //  drt[2] = 0;
            //  drt[3] = "-- Select --";
            //  dtseg.Rows.InsertAt(drt, 0);
            //}
            var result = dtseg.AsEnumerable().Select(x => new
            {
                v_keyvalue = x.Field<string>("v_keyvalue"),
                v_segitm_desc = string.Format("{0} | {1} | {2}", x.Field<string>("v_keyvalue"), x.Field<string>("v_segitm_desc"), x.Field<int>("v_id").ToString())
            }).ToList();
            result.Insert(0, new { v_keyvalue = string.Empty, v_segitm_desc = "-- Select --" });
            ViewBag.department = new SelectList(result, "v_keyvalue", "v_segitm_desc");
            //ViewBag.department = new SelectList(dtseg.DefaultView, dtseg.Columns[2].ColumnName, string.Format("{0} | {1} | {2}", dtseg.Columns[2].ColumnName, dtseg.Columns[3].ColumnName, dtseg.Columns[1].ColumnName));
        }

        public JsonResult GetSegmentsEmployeeAjax(string EmplrCode)
        {
            string _selectq = "keyvalue = " + EmplrCode;
            try
            {
                DVOMasterEmployee objPayEmployeeTypes = new DVOMasterEmployee();
                DataSet DSEmployeeList = ReportingUtilities.GetEmployerList(ref objPayEmployeeTypes);
                DataRow[] drs = DSEmployeeList.Tables[0].Select(_selectq);
                DataTable dtseg = DSEmployeeList.Tables[0].Clone();
                foreach (DataRow drseg in drs)
                    dtseg.ImportRow(drseg);
                var result = dtseg.AsEnumerable().Select(x => new
                {
                    Id = x.Field<string>("empl_code"),
                    name = string.Format("{0} {1} {2} - {3} - {4}", x.Field<string>("first_name"), x.Field<string>("middle_name"), x.Field<string>("last_name"), x.Field<string>("PermanentDistrict"), x.Field<string>("PermanentTehsil"))
                }).ToList();
                //result.Insert(0, new { Id = string.Empty, name = "-- Select --" });
                ViewBag.EmployeeId = new SelectList(result, "Id", "name", EmplrCode);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                //ExceptionManagement.ExceptionManager.Publish(Ex);
                //throw Ex;
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        //private void GetSegmentsEmployeeAjax(string code)
        //{
        //  string _selectq = "keyvalue = " + code;

        //  DVOMasterEmployee objPayEmployeeTypes = new DVOMasterEmployee();
        //  DataSet DSEmployeeList = ReportingUtilities.GetEmployerList(ref objPayEmployeeTypes);
        //  DataRow[] drs = DSEmployeeList.Tables[0].Select(_selectq);
        //  DataTable dtseg = DSEmployeeList.Tables[0].Clone();
        //  foreach (DataRow drseg in drs)
        //    dtseg.ImportRow(drseg);
        //  var result = dtseg.AsEnumerable().Select(x => new
        //  {
        //    empl_code = x.Field<string>("empl_code"),
        //    name = string.Format("{0}  {1}  {2} ({3})", x.Field<string>("first_name"), x.Field<string>("middle_name"), x.Field<string>("last_name"), x.Field<string>("empl_code"))
        //  }).ToList();
        //  result.Insert(0, new { empl_code = string.Empty, name = "-- Select --" });
        //  ViewBag.EmployeeId = new SelectList(result, "empl_code", "name", code);
        //}

        public ActionResult PrintPayslip()
        {
            BindCheckNo(string.Empty);
            BindComboEmployeeType(string.Empty);
            FillAccountType();
            GetSegments();
            generatePensionProcess = new GeneratePensionProcessModel();
            generatePensionProcess.StartingChequeNo = ViewBag.check_no;
            return PartialView("~/Views/PensionProcess/Payslips/PrintPayslip.cshtml", generatePensionProcess);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrintPayslip([Bind(Include = "PayDate,PayrollCashAccount,EmployeeType,Department")] GeneratePensionProcessModel generatePensionProcessModel, FormCollection form)
        {
            try
            {
                BindCheckNo(generatePensionProcessModel.StartingChequeNo);
                BindComboEmployeeType(generatePensionProcessModel.EmployeeType);
                FillAccountType();
                GetSegments();

                #region Execute Report
                //Added by Neeraj on 16/07/2015 , To implement batch process, Nedd to add
                //A parameter 'ref pObjBatch' in 'GetPayslipDetails' function
                //and remove comment from the code written for batch process logic in 'PYBatchManagement' region.
                //and remove comment from 'pObjBatch.searchcriteria'.
                #region PYBatchManagement
                // The pObjBatch object used to hold active batch information.
                DVOPYBatchProcessStybatchr pObjBatch = null;
                //Get Payroll Active batch,if any. 
                List<DVOPYBatchProcessStybatchr> objBatchList = BLLPYBatchProcessStybatchr.GetActiveBatch();
                if (objBatchList != null && objBatchList.Count > 0)
                {
                    pObjBatch = objBatchList[0];
                }
                else
                {
                    //Utilities.ShowMessage("No Active Batch for Payroll Process,Please Create a New Batch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TempData["error"] = "No Active Batch for Payroll Process,Please Create a New Batch";
                    return PartialView("~/Views/PensionProcess/Payslips/PrintPayslip.cshtml", generatePensionProcessModel);
                }
                #endregion PYBatchManagement

                pObjBatch.searchcriteria = string.Empty;
                string dept = string.Empty;
                string emptype = string.Empty;
                string paydate = string.Empty;
                string account = string.Empty;


                if (generatePensionProcessModel.PayDate == null)
                {
                    TempData["error"] = "Select Pay Date First";
                    return PartialView("~/Views/PensionProcess/Payslips/PrintPayslip.cshtml", generatePensionProcessModel);
                }

                if (string.IsNullOrWhiteSpace(generatePensionProcessModel.EmployeeType) || generatePensionProcessModel.EmployeeType == "0")
                {
                    TempData["error"] = "Select Scheme Name First";
                    return PartialView("~/Views/PensionProcess/Payslips/PrintPayslip.cshtml", generatePensionProcessModel);
                }
                if (string.IsNullOrWhiteSpace(generatePensionProcessModel.PayrollCashAccount) || generatePensionProcessModel.PayrollCashAccount == "0")
                {
                    TempData["error"] = "Select Payroll Cash Account First ";
                    return PartialView("~/Views/PensionProcess/Payslips/PrintPayslip.cshtml", generatePensionProcessModel);
                }

                if (string.IsNullOrWhiteSpace(generatePensionProcessModel.Department) || generatePensionProcessModel.Department == "0")
                {
                    TempData["error"] = "Select Department First";
                    return PartialView("~/Views/PensionProcess/Payslips/PrintPayslip.cshtml", generatePensionProcessModel);
                }


                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.Department))
                {
                    dept = generatePensionProcessModel.Department.Trim();
                }

                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.EmployeeType))
                {
                    pObjBatch.searchcriteria = "Employee Type=" + generatePensionProcessModel.EmployeeType.Trim() + " ,";
                    emptype = generatePensionProcessModel.EmployeeType.Trim();
                }
                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.PayrollCashAccount))
                {
                    pObjBatch.searchcriteria = "Account Type=" + generatePensionProcessModel.PayrollCashAccount.Trim();
                    account = generatePensionProcessModel.PayrollCashAccount.Trim();
                }
                if (generatePensionProcessModel.PayDate != null)
                {
                    string payDate = Convert.ToDateTime(generatePensionProcessModel.PayDate).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    //objDVOPayrollProcess_PayEmployee.pay_date = payDate;
                    pObjBatch.searchcriteria = "Pay Date =" + payDate + " ,";

                    paydate = payDate;
                }

                DataSet DSPaySlipsA4 = ReportingUtilities.GetPaySlipsA4Info(dept, emptype, paydate, ref pObjBatch);
                bool tablesExist = false;
                if (DSPaySlipsA4 != null)
                    if (DSPaySlipsA4.Tables.Count == 3)
                    {
                        if (DSPaySlipsA4.Tables["Header"].Rows.Count > 0)
                        {
                            tablesExist = true;
                            this.HttpContext.Session["ReportName"] = "rptPaySlipsA4.rpt";
                            this.HttpContext.Session["rptSource"] = DSPaySlipsA4;
                            @ViewBag.RptLoad = "rptPaySlipsA4.rpt";
                        }
                    }

                if (!tablesExist)
                {
                    //SatyaPay.StyleUtility.Reports_Splasher.Close();
                    //Utilities.ShowMessage("No record found.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //PrintPaySlipsReportViewer.ReportSource = null;
                    TempData["error"] = "No Record Found.";
                }

                return PartialView("~/Views/PensionProcess/Payslips/PrintPayslip.cshtml", generatePensionProcessModel);
                #endregion
            }
            catch (Exception ex)
            {
                //ReportViewer.ReportSource = null;
                //SatyaPay.StyleUtility.Reports_Splasher.Close();
                //ExceptionManagement.ExceptionManager.Publish(ex);
                //MessageBox.Show(ex.Message, "Satya Pay.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TempData["error"] = "Please Try Again..." + ex.Message;
                return PartialView("~/Views/PensionProcess/Payslips/PrintPayslip.cshtml", generatePensionProcessModel);
            }
        }

        public ActionResult PrintDuplicatePayslip()
        {
            BindCheckNo(string.Empty);
            BindComboEmployeeType(string.Empty);
            FillAccountType();
            GetSegments();
            //ViewBag.EmployerId = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true), "Id", "EmployerName");
            List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = new List<DVOMasterEmployee>();
            ViewBag.EmployeeId = new SelectList(listSearchResultDVOMasterEmployee, "EmplCode", "FirstName");
            generatePensionProcess = new GeneratePensionProcessModel();
            generatePensionProcess.StartingChequeNo = ViewBag.check_no;

            DVOMasterEmployee objPayEmployeeTypes = new DVOMasterEmployee();
            var result = App.Web.Repository.ListToDataset.ToList<DVOMasterEmployee>(ReportingUtilities.GetEmployerList(ref objPayEmployeeTypes).Tables[0]);

            return PartialView("~/Views/PensionProcess/Payslips/PrintDuplicatePayslip.cshtml", generatePensionProcess);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrintDuplicatePayslip([Bind(Include = "PayDate,PayDateTo,PayrollCashAccount,EmployeeType,Department,EmployeeId")] GeneratePensionProcessModel generatePensionProcessModel, FormCollection form)
        {
            try
            {
                BindCheckNo(generatePensionProcessModel.StartingChequeNo);
                BindComboEmployeeType(generatePensionProcessModel.EmployeeType);
                FillAccountType();
                GetSegments();
                if (string.IsNullOrWhiteSpace(generatePensionProcessModel.Department))
                {
                    List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = new List<DVOMasterEmployee>();
                    ViewBag.EmployeeId = new SelectList(listSearchResultDVOMasterEmployee, "EmplCode", "FirstName");
                }
                else
                {
                    GetSegmentsEmployeeAjax(generatePensionProcessModel.Department);
                }
                #region Execute Report
                //Added by Neeraj on 16/07/2015 , To implement batch process, Nedd to add
                //A parameter 'ref pObjBatch' in 'GetPayslipDetails' function
                //and remove comment from the code written for batch process logic in 'PYBatchManagement' region.
                //and remove comment from 'pObjBatch.searchcriteria'.

                //pObjBatch.searchcriteria = string.Empty;
                string dept = string.Empty;
                string emptype = string.Empty;
                string paydate = string.Empty;
                string paydateto = string.Empty;
                string account = string.Empty;


                if (generatePensionProcessModel.PayDate == null)
                {
                    TempData["error"] = "Select Pay Date First";
                    return PartialView("~/Views/PensionProcess/Payslips/PrintDuplicatePayslip.cshtml", generatePensionProcessModel);
                }
                if (generatePensionProcessModel.PayDateTo == null)
                {
                    TempData["error"] = "Select Pay Date To";
                    return PartialView("~/Views/PensionProcess/Payslips/PrintDuplicatePayslip.cshtml", generatePensionProcessModel);
                }
                if (string.IsNullOrWhiteSpace(generatePensionProcessModel.EmployeeType) || generatePensionProcessModel.EmployeeType == "0")
                {
                    TempData["error"] = "Select Scheme Name First";
                    return PartialView("~/Views/PensionProcess/Payslips/PrintDuplicatePayslip.cshtml", generatePensionProcessModel);
                }
                if (string.IsNullOrWhiteSpace(generatePensionProcessModel.PayrollCashAccount) || generatePensionProcessModel.PayrollCashAccount == "0")
                {
                    TempData["error"] = "Select Payroll Cash Account First ";
                    return PartialView("~/Views/PensionProcess/Payslips/PrintDuplicatePayslip.cshtml", generatePensionProcessModel);
                }

                if (string.IsNullOrWhiteSpace(generatePensionProcessModel.Department) || generatePensionProcessModel.Department == "0")
                {
                    TempData["error"] = "Select Department First";
                    return PartialView("~/Views/PensionProcess/Payslips/PrintDuplicatePayslip.cshtml", generatePensionProcessModel);
                }


                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.Department))
                {
                    dept = generatePensionProcessModel.Department.Trim();
                }

                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.EmployeeType))
                {
                    //pObjBatch.searchcriteria = "Employee Type=" + generatePensionProcessModel.EmployeeType.Trim() + " ,";
                    emptype = generatePensionProcessModel.EmployeeType.Trim();
                }
                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.PayrollCashAccount))
                {
                    //pObjBatch.searchcriteria = "Account Type=" + generatePensionProcessModel.PayrollCashAccount.Trim();
                    account = generatePensionProcessModel.PayrollCashAccount.Trim();
                }
                if (generatePensionProcessModel.PayDate != null)
                {
                    string payDate = Convert.ToDateTime(generatePensionProcessModel.PayDate).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    //objDVOPayrollProcess_PayEmployee.pay_date = payDate;
                    //pObjBatch.searchcriteria = "Pay Date =" + payDate + " ,";

                    paydate = payDate;
                }
                if (generatePensionProcessModel.PayDateTo != null)
                {
                    string payDateto = Convert.ToDateTime(generatePensionProcessModel.PayDateTo).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    //objDVOPayrollProcess_PayEmployee.pay_date = payDate;
                    //pObjBatch.searchcriteria = "Pay Date =" + payDate + " ,";

                    paydateto = payDateto;
                }
                DataSet DSPaySlipsA4 = ReportingUtilities.GetPaySlipsA4DuplicateInfo(dept, emptype, generatePensionProcessModel.EmployeeId, paydate, paydateto);
                bool tablesExist = false;
                if (DSPaySlipsA4 != null)
                    if (DSPaySlipsA4.Tables.Count == 3)
                    {
                        if (DSPaySlipsA4.Tables["Header"].Rows.Count > 0)
                        {
                            tablesExist = true;
                            this.HttpContext.Session["ReportName"] = "rptPaySlipsA4Duplicate.rpt";
                            this.HttpContext.Session["rptSource"] = DSPaySlipsA4;
                            @ViewBag.RptLoad = "rptPaySlipsA4Duplicate.rpt";
                        }
                    }

                if (!tablesExist)
                {
                    //SatyaPay.StyleUtility.Reports_Splasher.Close();
                    //Utilities.ShowMessage("No record found.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //PrintPaySlipsReportViewer.ReportSource = null;
                    TempData["error"] = "No Record Found.";
                }

                return PartialView("~/Views/PensionProcess/Payslips/PrintDuplicatePayslip.cshtml", generatePensionProcessModel);
                #endregion
            }
            catch (Exception ex)
            {
                //ReportViewer.ReportSource = null;
                //SatyaPay.StyleUtility.Reports_Splasher.Close();
                //ExceptionManagement.ExceptionManager.Publish(ex);
                //MessageBox.Show(ex.Message, "Satya Pay.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TempData["error"] = "Please Try Again..." + ex.Message;
                return PartialView("~/Views/PensionProcess/Payslips/PrintDuplicatePayslip.cshtml", generatePensionProcessModel);
            }
        }
        public JsonResult DGVCheckAjax(int Pybatchid)
        {
            string startDate = "01/01/1990", endDate = "11/11/2015";
            decimal _totalAmount = 0;
            int _count = 0;
            try
            {
                DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                objDVOPayrollProcess_PayEmployee.pay_date = Convert.ToDateTime(startDate);
                objDVOPayrollProcess_PayEmployee.eop_date = Convert.ToDateTime(endDate);
                DataSet ds = BLLPYBatchProcessStybatchr.GetChecks(ref objDVOPayrollProcess_PayEmployee);
                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _count = _count + 1;
                        _totalAmount = _totalAmount + (ds.Tables[0].Rows[i]["cash_amount"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[i]["cash_amount"]) : 0);
                    }
                    var entity = ds.Tables[0].AsEnumerable().
                                                            Select(x => new
                                                            {
                                                                DocNo = x.Field<Int64>("doc_no"),
                                                                CheckNo = x.Field<int>("check_no"),
                                                                PayDate = x.Field<DateTime>("pay_date"),
                                                                Employee = x.Field<string>("empl_name"),
                                                                Amount = x.Field<decimal>("cash_amount")
                                                            });


                    /*txtcheckCount.Text = _count.ToString().Trim();
                    txtCheckAmount.Text = _totalAmount.ToString().Trim();*/
                    return Json(entity, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                //ExceptionManagement.ExceptionManager.Publish(Ex);
                //throw Ex;
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdatePostedStatus()
        {
            PensionProcessViewModel Paysearch = ShowActiveBatch();
            BindCheckNo(string.Empty);
            BindComboEmployeeType(Paysearch.EmpType);
            return PartialView("~/Views/PensionProcess/Postings/UpdatePostedStatus.cshtml", Paysearch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePostedStatus([Bind(Include = "_AccountNumber,EmployeeCode,FirstName,LastName,EmpType,TxtEmpl_type,JobCode,PayPeriod,Title,LPayDate,LPayDateChecked,FullTime,PayrollDate,PayrollDateChecked,EOPDate,EOPDateChecked,pybatchid,batchprocessid,processname,processstartedon,processendedon,recordssearched,recordsprocessed,status,searchcriteria,errormessage")] PensionProcessViewModel pensionProcessHeader = null)
        {
            try
            {
                #region Execute Report
                //Added by Neeraj on 16/07/2015 , To implement batch process, Nedd to add
                //A parameter 'ref pObjBatch' in 'GetPayslipDetails' function
                //and remove comment from the code written for batch process logic in 'PYBatchManagement' region.
                //and remove comment from 'pObjBatch.searchcriteria'.
                #region PYBatchManagement

                // The pObjBatch object used to hold active batch information.
                DVOPYBatchProcessStybatchr pObjBatch = null;
                //Get Payroll Active batch,if any. 
                List<DVOPYBatchProcessStybatchr> objBatchList = BLLPYBatchProcessStybatchr.GetActiveBatch();
                if (objBatchList != null && objBatchList.Count > 0)
                {
                    pObjBatch = objBatchList[0];
                }
                else
                {
                    //Utilities.ShowMessage("No Active Batch for Payroll Process,Please Create a New Batch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TempData["error"] = "No Active Batch for Payroll Process,Please Create a New Batch";
                    return PartialView("~/Views/PensionProcess/Postings/UpdatePostedStatus.cshtml", new PensionProcessViewModel());
                }
                #endregion PYBatchManagement

                pObjBatch.searchcriteria = string.Empty;
                DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                //BLLPayrollAutopay obj = new BLLPayrollAutopay();
                DataSet ds = BLLPayrollAutopayNew.updatePostedstatus(ref objDVOPayrollProcess_PayEmployee);

                PensionProcessViewModel Paysearch = ShowActiveBatch();
                ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
                BindComboEmployeeType(Paysearch.EmpType);

                if (ds != null)
                {
                    TempData["success"] = "Payroll has been Posted Successfully";
                    return PartialView("~/Views/PensionProcess/Postings/UpdatePostedStatus.cshtml", Paysearch);
                }

                #endregion
            }
            catch (Exception ex)
            {
                //ReportViewer.ReportSource = null;
                //SatyaPay.StyleUtility.Reports_Splasher.Close();
                //ExceptionManagement.ExceptionManager.Publish(ex);
                //MessageBox.Show(ex.Message, "Satya Pay.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TempData["error"] = "Please Try Again..." + ex.Message;
            }
            return PartialView("~/Views/PensionProcess/Postings/UpdatePostedStatus.cshtml", new PensionProcessViewModel());
        }

        public JsonResult CheckOKToCancelStatusAjax()
        {
            int No = 0;
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            //DataSet I;
            SQL.Append("select count(ok_to_post) as [DataCount] from Process_payemployee where ok_to_post IN('Y','N')");
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), objDalBaseClass.ConnectionString);
            da.Fill(ds);
            if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
            {
                No = 1;
            }
            else
            {
                No = 0;
            }
            return Json(new { data = No }, JsonRequestBehavior.AllowGet);
        }

        //
        public JsonResult CheckOKToCloseStatusAjax()
        {
            int No = 0;
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            //DataSet I;
            SQL.Append("select count(ok_to_post) as [DataCount] from Process_payemployee where ok_to_post IN('P') ");
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), objDalBaseClass.ConnectionString);
            da.Fill(ds);
            if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
            {
                No = 1;
            }
            else
            {
                No = 0;
            }
            return Json(new { data = No }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CloseBatchProcess()
        {
            PensionProcessViewModel Paysearch = new PensionProcessViewModel();
            try
            {
                int userid = AppUserManager.GetUserId();
                var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
                var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionProcess" && x.ActionName == "CloseBatchProcess")
                             join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                             from p in ps.DefaultIfEmpty()
                             select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Close Batch Process", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

                if (model != null)
                {
                    ViewBag.AddPermission = model.AddPermssion;
                    ViewBag.EditPermission = model.EditPermission;
                }
                Paysearch = ShowActiveBatch("Y");
                ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
                BindComboEmployeeType(Paysearch.EmpType);
            }
            catch (Exception ex)
            {
                ToastError(ex.Message);
            }
            return PartialView("~/Views/PensionProcess/Batch/CloseBatchProcess.cshtml", Paysearch);
        }

        public JsonResult CloseBatchProcessAjax(PensionProcessViewModel pensionProcessHeader)
        {
            try
            {
                #region Execute Report
                //Added by Neeraj on 16/07/2015 , To implement batch process, Nedd to add
                //A parameter 'ref pObjBatch' in 'GetPayslipDetails' function
                //and remove comment from the code written for batch process logic in 'PYBatchManagement' region.
                //and remove comment from 'pObjBatch.searchcriteria'.
                #region PYBatchManagement
                // The pObjBatch object used to hold active batch information.
                DVOPYBatchProcessStybatchr pObjBatch = null;
                //Get Payroll Active batch,if any. 
                List<DVOPYBatchProcessStybatchr> objBatchList = BLLPYBatchProcessStybatchr.GetActiveBatch();
                if (pensionProcessHeader.pybatchid == null && pensionProcessHeader.pybatchid <= 0)
                {
                    //Utilities.ShowMessage("No Active Batch for Payroll Process,Please Create a New Batch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TempData["error"] = "No Active Batch for Payroll Process,Please Create a New Batch";
                    return Json(new PensionProcessViewModel(), JsonRequestBehavior.AllowGet);
                }
                #endregion PYBatchManagement
                int userid = AppUserManager.GetUserId();
                DVOPYBatchProcessStybatchr objDVOPYBatchProcessStybatchr = new DVOPYBatchProcessStybatchr();
                objDVOPYBatchProcessStybatchr.pybatchid = pensionProcessHeader.pybatchid;
                objDVOPYBatchProcessStybatchr.updateby = userid;
                objDVOPYBatchProcessStybatchr.insertmachineinfo = System.Environment.MachineName;
                int UPDResult = BLLPYBatchProcessStybatchr.UPDATEBatchProcessStatus(ref objDVOPYBatchProcessStybatchr);


                ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
                BindComboEmployeeType(string.Empty);

                if (UPDResult == 1)
                {
                    TempData["success"] = "Active Batch of Pension Process has been Closed Successfully.";

                    var response = new { message = TempData["success"] };
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
                else
                { PensionProcessViewModel Paysearch = ShowActiveBatch(); }


                #endregion
            }
            catch (Exception ex)
            {
                TempData["error"] = "Please try Again.." + ex.Message;
                //ReportViewer.ReportSource = null;
                //SatyaPay.StyleUtility.Reports_Splasher.Close();
                //ExceptionManagement.ExceptionManager.Publish(ex);
                //MessageBox.Show(ex.Message, "Satya Pay.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Json(pensionProcessHeader, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CancelBatchProcess()
        {
            PensionProcessViewModel Paysearch = new PensionProcessViewModel();
            try
            {
                int userid = AppUserManager.GetUserId();
                var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
                var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionProcess" && x.ActionName == "CancelBatchProcess")
                             join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                             from p in ps.DefaultIfEmpty()
                             select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Cancel Batch Process", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

                if (model != null)
                {
                    ViewBag.AddPermission = model.AddPermssion;
                    ViewBag.EditPermission = model.EditPermission;
                }
                Paysearch = ShowActiveBatch("Y");
                ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
                BindComboEmployeeType(Paysearch.EmpType);
            }
            catch (Exception ex)
            {
                ToastError(ex.Message);
            }
            return PartialView("~/Views/PensionProcess/Batch/CancelBatchProcess.cshtml", Paysearch);
        }

        public JsonResult CancelBatchProcessAjax(PensionProcessViewModel pensionProcessHeader)
        {
            try
            {
                #region Execute Report
                //Added by Neeraj on 16/07/2015 , To implement batch process, Nedd to add
                //A parameter 'ref pObjBatch' in 'GetPayslipDetails' function
                //and remove comment from the code written for batch process logic in 'PYBatchManagement' region.
                //and remove comment from 'pObjBatch.searchcriteria'.
                #region PYBatchManagement
                // The pObjBatch object used to hold active batch information.
                DVOPYBatchProcessStybatchr pObjBatch = null;
                //Get Payroll Active batch,if any. 
                List<DVOPYBatchProcessStybatchr> objBatchList = BLLPYBatchProcessStybatchr.GetActiveBatch();
                if (pensionProcessHeader.pybatchid == null && pensionProcessHeader.pybatchid <= 0)
                {
                    //Utilities.ShowMessage("No Active Batch for Payroll Process,Please Create a New Batch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TempData["error"] = "No Active Batch for Payroll Process,Please Create a New Batch";
                    return Json(new PensionProcessViewModel(), JsonRequestBehavior.AllowGet);
                }
                #endregion PYBatchManagement
                int userid = AppUserManager.GetUserId();
                DVOPYBatchProcessStybatchr objDVOPYBatchProcessStybatchr = new DVOPYBatchProcessStybatchr();
                objDVOPYBatchProcessStybatchr.pybatchid = pensionProcessHeader.pybatchid;
                objDVOPYBatchProcessStybatchr.updateby = userid;
                objDVOPYBatchProcessStybatchr.insertmachineinfo = System.Environment.MachineName;
                //int UPDResult = BLLPYBatchProcessStybatchr.CANCELBatchProcessStatus(ref objDVOPYBatchProcessStybatchr);
                DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                //BLLPayrollAutopay obj = new BLLPayrollAutopay();
                DataSet ds = BLLPayrollAutopayNew.updateCancelstatus(ref objDVOPayrollProcess_PayEmployee);

                ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
                BindComboEmployeeType(string.Empty);

                if (ds != null)
                {
                    //TempData["success"] = "Active Batch of Pension Process has been Cancelled Successfully.";
                    var response = "Active Batch of Pension Process has been Cancelled Successfully.";
                    return Json(response, JsonRequestBehavior.AllowGet);//new PensionProcessViewModel(),
                }
                else
                { PensionProcessViewModel Paysearch = ShowActiveBatch(); }


                #endregion
            }
            catch (Exception ex)
            {
                TempData["error"] = "Please try Again.." + ex.Message;
                //ReportViewer.ReportSource = null;
                //SatyaPay.StyleUtility.Reports_Splasher.Close();
                //ExceptionManagement.ExceptionManager.Publish(ex);
                //MessageBox.Show(ex.Message, "Satya Pay.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Json(pensionProcessHeader, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CancelBatchProcess1()
        {
            PensionProcessViewModel Paysearch = new PensionProcessViewModel();
            try
            {
                int userid = AppUserManager.GetUserId();
                var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
                var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionProcess" && x.ActionName == "CancelBatchProcess1")
                             join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                             from p in ps.DefaultIfEmpty()
                             select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Cancel Individual Payroll", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

                if (model != null)
                {
                    ViewBag.AddPermission = model.AddPermssion;
                    ViewBag.EditPermission = model.EditPermission;
                }
                Paysearch = ShowActiveBatch("Y");
                string disData = string.Empty;
                if (Paysearch.RegionNames != "" && Paysearch.RegionNames != null)
                {
                    disData = Paysearch.RegionNames.ToLower().Trim();
                }
                ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
                BindComboEmployeeType(Paysearch.EmpType);
                var ds = BLLPayrollAutopayNew.LoadPayrollEmp();
                //ViewBag.EmployeeCode = new SelectList(ds.Tables[0].DefaultView, "empl_code", "Pensioner");
                ViewBag.EmployeeCode = new SelectList(Enumerable.Empty<SelectListItem>());


                ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);
            }
            catch (Exception ex)
            {
                ToastError(ex.Message);
                ViewBag.EmployeeType = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
                ViewBag.EmployeeCode = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
            }
            return PartialView("~/Views/PensionProcess/Batch/CancelBatchProcess1.cshtml", Paysearch);
        }

        public JsonResult CancelBatchProcess1Ajax(string[] objs)
        {
            PensionProcessViewModel pensionProcessHeader = new PensionProcessViewModel();

            #region Declare Variables for Batch Process
            StringBuilder errorMassage = new StringBuilder();
            DVOPYBatchProcessDetailStybatchd objProcessDtl = new DVOPYBatchProcessDetailStybatchd();
            List<DVOPYBatchProcessDetailStybatchd> listDVOPYBatchProcessDetailStybatchd = new List<DVOPYBatchProcessDetailStybatchd>();
            int recordsSearched = 0;
            int recordsProcessed = 0;
            bool IsProessIns = false;
            #endregion

            if (objs != null && objs.Length > 1)
            {
                pensionProcessHeader.pybatchid = Convert.ToInt32(objs[0]);
                pensionProcessHeader.EmployeeCode = objs[2];
                recordsSearched = pensionProcessHeader.EmployeeCode.Split(',').Count();
                recordsProcessed = pensionProcessHeader.EmployeeCode.Split(',').Count();
            }
            try
            {
                #region Execute Report
                //Added by Neeraj on 16/07/2015 , To implement batch process, Nedd to add
                //A parameter 'ref pObjBatch' in 'GetPayslipDetails' function
                //and remove comment from the code written for batch process logic in 'PYBatchManagement' region.
                //and remove comment from 'pObjBatch.searchcriteria'.
                #region PYBatchManagement
                // The pObjBatch object used to hold active batch information.
                DVOPYBatchProcessStybatchr pObjBatch = null;
                //Get Payroll Active batch,if any. 
                List<DVOPYBatchProcessStybatchr> objBatchList = BLLPYBatchProcessStybatchr.GetActiveBatch();
                if (pensionProcessHeader.pybatchid == null && pensionProcessHeader.pybatchid <= 0)
                {
                    //Utilities.ShowMessage("No Active Batch for Payroll Process,Please Create a New Batch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TempData["error"] = "No Active Batch for Payroll Process,Please Create a New Batch";
                    return Json(new PensionProcessViewModel(), JsonRequestBehavior.AllowGet);
                }
                if (pensionProcessHeader.EmployeeCode == "null")
                {
                    TempData["error"] = "Please Select Beneficiary First";
                    return Json(new PensionProcessViewModel(), JsonRequestBehavior.AllowGet);
                }
                #endregion PYBatchManagement
                int userid = AppUserManager.GetUserId();
                //DVOPYBatchProcessStybatchr objDVOPYBatchProcessStybatchr = new DVOPYBatchProcessStybatchr();
                //objDVOPYBatchProcessStybatchr.pybatchid = pensionProcessHeader.pybatchid;
                //objDVOPYBatchProcessStybatchr.updateby = userid;
                //objDVOPYBatchProcessStybatchr.insertmachineinfo = System.Environment.MachineName;
                ////objDVOPYBatchProcessStybatchr.recordcound = 1;
                ////objDVOPYBatchProcessStybatchr.recordsearched = 1;
                //int UPDResult = BLLPYBatchProcessStybatchr.UPDATEBatchProcessStatus(ref objDVOPYBatchProcessStybatchr);


                #region Insert Process Start Info..

                object objTrx = null;
                List<DVOPYBatchProcessDetailStybatchd> objList = new List<DVOPYBatchProcessDetailStybatchd>();
                DVOPYBatchProcessDetailStybatchd obj = new DVOPYBatchProcessDetailStybatchd();
                obj.pybatchid = pensionProcessHeader.pybatchid;
                obj.processname = "Cancel Individual Payroll";
                obj.processstartedon = DateTime.Now.ToShortDateString();// pensionProcessHeader.processstartedon.ToString();
                obj.processendedon = DateTime.Now.ToShortDateString();//pensionProcessHeader.processendedon.ToString();
                obj.recordssearched = recordsSearched;
                obj.recordsprocessed = recordsProcessed;
                obj.status = 1;
                obj.searchcriteria = pensionProcessHeader.searchcriteria + "";
                obj.errormessage = errorMassage.ToString();
                obj.insertby = userid;
                obj.insertmachineinfo = System.Environment.MachineName;
                obj.updateby = userid;
                obj.updatemachineinfo = System.Environment.MachineName;
                objList.Add(obj);
                BLLPYBatchProcessDetailStybatchd.InsertData(ref objTrx, ref objList);

                IsProessIns = true;

                #endregion

                DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                //BLLPayrollAutopay obj = new BLLPayrollAutopay();
                objDVOPayrollProcess_PayEmployee.EmplCode = pensionProcessHeader.EmployeeCode;
                DataSet ds = BLLPayrollAutopayNew.updateCancelstatus1(ref objDVOPayrollProcess_PayEmployee);

                ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
                BindComboEmployeeType(string.Empty);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        TempData["success"] = "Active Batch Pension Process for Selected Beneficiary has been Cancelled Successfully.";
                    }
                    else
                    {
                        TempData["success"] = "Active Batch Pension Process for Selected Beneficiary has been already Cancelled.";
                    }
                    return Json(new PensionProcessViewModel(), JsonRequestBehavior.AllowGet);
                }
                else
                { PensionProcessViewModel Paysearch = ShowActiveBatch(); }


                #endregion
            }
            catch (Exception ex)
            {
                TempData["error"] = "Please try Again.." + ex.Message;
                //ReportViewer.ReportSource = null;
                //SatyaPay.StyleUtility.Reports_Splasher.Close();
                //ExceptionManagement.ExceptionManager.Publish(ex);
                //MessageBox.Show(ex.Message, "Satya Pay.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Json(pensionProcessHeader, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CloseBatchProcess(PensionProcessViewModel pensionProcessHeader)
        //{
        //  try
        //  {

        //    pensionProcessHeader.pybatchid = form["txtpybatchid"] == null ? 0 : Convert.ToInt32(form["txtpybatchid"]);
        //    #region Execute Report
        //    //Added by Neeraj on 16/07/2015 , To implement batch process, Nedd to add
        //    //A parameter 'ref pObjBatch' in 'GetPayslipDetails' function
        //    //and remove comment from the code written for batch process logic in 'PYBatchManagement' region.
        //    //and remove comment from 'pObjBatch.searchcriteria'.
        //    #region PYBatchManagement
        //    // The pObjBatch object used to hold active batch information.
        //    DVOPYBatchProcessStybatchr pObjBatch = null;
        //    //Get Payroll Active batch,if any. 
        //    List<DVOPYBatchProcessStybatchr> objBatchList = BLLPYBatchProcessStybatchr.GetActiveBatch();
        //    if (pensionProcessHeader.pybatchid == null && pensionProcessHeader.pybatchid <= 0)
        //    {
        //      //Utilities.ShowMessage("No Active Batch for Payroll Process,Please Create a New Batch", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //      TempData["error"] = "No Active Batch for Payroll Process,Please Create a New Batch";
        //      return PartialView("~/Views/PensionProcess/Batch/CloseBatchProcess.cshtml", new PensionProcessViewModel());
        //    }
        //    #endregion PYBatchManagement
        //    int userid = AppUserManager.GetUserId();
        //    DVOPYBatchProcessStybatchr objDVOPYBatchProcessStybatchr = new DVOPYBatchProcessStybatchr();
        //    objDVOPYBatchProcessStybatchr.pybatchid = pensionProcessHeader.pybatchid;
        //    objDVOPYBatchProcessStybatchr.updateby = userid;
        //    objDVOPYBatchProcessStybatchr.insertmachineinfo = System.Environment.MachineName;
        //    int UPDResult = BLLPYBatchProcessStybatchr.UPDATEBatchProcessStatus(ref objDVOPYBatchProcessStybatchr);

        //    List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BindComboEmployeeType();
        //    ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
        //    ViewBag.EmployeeType = new SelectList(listDVOMasterEmpTypes, "type_code", "description");

        //    if (UPDResult == 1)
        //    {
        //      TempData["success"] = "Active Batch of Pension Process has been Closed Successfully.";
        //      return PartialView("~/Views/PensionProcess/Batch/CloseBatchProcess.cshtml", new PensionProcessViewModel());
        //    }
        //    else
        //    { PensionProcessViewModel Paysearch = ShowActiveBatch(); }


        //    #endregion
        //  }
        //  catch (Exception ex)
        //  {
        //    TempData["error"] = "Please try Again.."+ex.Message;
        //    //ReportViewer.ReportSource = null;
        //    //SatyaPay.StyleUtility.Reports_Splasher.Close();
        //    //ExceptionManagement.ExceptionManager.Publish(ex);
        //    //MessageBox.Show(ex.Message, "Satya Pay.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //  }
        //  return PartialView("~/Views/PensionProcess/Batch/CloseBatchProcess.cshtml", new PensionProcessViewModel());
        //}

        private List<DVOUpdateBankCode> GetBanckCode(string bank_code)
        {
            DVOUpdateBankCode obj = new DVOUpdateBankCode();
            List<DVOUpdateBankCode> ObjList = BLLUpdateBankCode.GetBankCodeDetails(ref obj);
            return ObjList;
        }

        private void BindCmbBanckCode(string bank_code)
        {
            DVOUpdateBankCode obj = new DVOUpdateBankCode();
            List<DVOUpdateBankCode> ObjList = BLLUpdateBankCode.GetBankCodeDetails(ref obj).Select(x =>
                new DVOUpdateBankCode
                {
                    bank_code = x.bank_code,
                    bank_desc = string.Format("{0} | {1}", x.bank_code, x.bank_desc),
                }
                ).ToList();
            obj.bank_code = string.Empty;
            obj.bank_desc = "--Select--";
            ObjList.Sort(new DVOUpdateBankCode_BankCode_Comparer());
            ObjList.Insert(0, obj);
            bank_code = string.IsNullOrWhiteSpace(bank_code) ? string.Empty : bank_code;
            ViewBag.BanckCode = new SelectList(ObjList, "bank_code", "bank_desc", bank_code);

        }

        public ActionResult PrintBankListing()
        {
            generatePensionProcess = new GeneratePensionProcessModel();
            try
            {
                ViewBag.IsGeneratePensionWithoutSftp = IsGeneratePensionWithoutSftp;
                int userid = AppUserManager.GetUserId();
                var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();

                var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionProcess" && x.ActionName == "PrintBankListing")
                             join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                             from p in ps.DefaultIfEmpty()
                             select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Print Bank Listing", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

                if (model != null)
                {
                    ViewBag.AddPermission = model.AddPermssion;


                    ViewBag.EditPermission = model.EditPermission;
                }
                PensionProcessViewModel Paysearch = ShowActiveBatch();
                string disData = string.Empty;
                BindCmbBanckCode(string.Empty);
                BindComboEmployeeType(Paysearch.EmpType);

                generatePensionProcess.DepositDate = Paysearch.PayrollDate;

                generatePensionProcess.PayDate = DVOApplicationUserInfo.DateConvertion(DateTime.Now);
                // generatePensionProcess.PayDate = DVOApplicationUserInfo.DateConvertion(GetCurrentDateAsPerFinancialYear());
                ViewBag.pybatchid = Paysearch.pybatchid;
                Paysearch.RegionNames = Paysearch.RegionNames;
                if (Paysearch.RegionNames != "" && Paysearch.RegionNames != null)
                {
                    disData = Paysearch.RegionNames.ToLower().Trim();
                }
                ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);

                Dictionary<string, string> PensionDetailByMonthYear = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear();
                //List<int> CurrentFYearBatchList = GetCurrentFYearBatchList();
                //if (CurrentFYearBatchList.Count > 0 && PensionDetailByMonthYear != null)
                //{
                //    int pybatchid = 0;
                //    if (PensionDetailByMonthYear["pybatchid"] != null)
                //    {
                //        pybatchid = Convert.ToInt32(PensionDetailByMonthYear["pybatchid"]);
                //    }
                //    if (!CurrentFYearBatchList.Any(x => x == pybatchid))
                //    {
                //        PensionDetailByMonthYear = null;
                //    }
                //}
                //else
                //{
                //    PensionDetailByMonthYear = null;
                //}
                ViewBag.CurrentMonthBatch = PensionDetailByMonthYear;
                if (PensionDetailByMonthYear == null)
                {
                    ViewBag.pybatchid = 0;
                }
                else
                {
                    ViewBag.pybatchid = PensionDetailByMonthYear["pybatchid"];
                }
                if (PensionDetailByMonthYear != null)
                {

                    string[] regions = PensionDetailByMonthYear["Districts"].Split(',').Select(region => region.Trim()).ToArray();
                    var filteredRegions = regions.Where(region => region != "JAMMU REGION" && region != "KASHMIR REGION");
                    string Paydistricts = string.Join(", ", filteredRegions);

                    object[] parameters1 = new object[2];
                    parameters1[0] = PensionDetailByMonthYear["pybatchid"];
                    parameters1[1] = PensionDetailByMonthYear["Districts"];

                    DataSet ds_ = new DataSet();
                    StringBuilder SQL = new StringBuilder();
                    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                    object objTransaction = objDALBaseClassHelper.GetTransactionObject();
                    //SQL.Append("SELECT Districts FROM Payroll_Process_Header WHERE pybatchid=" + parameters1[0].ToString());
                    SQL.Append("SELECT Top 1 ok_to_post  FROM Process_PayEmployee PPE WITH (NOLOCK) JOIN MasterEmployee ME WITH (NOLOCK) ON ME.empl_code = PPE.empl_code JOIN Payroll_Process_Header PPH WITH (NOLOCK) ON PPE.pybatchid =PPH.pybatchid JOIN Payroll_Process_Details PPD WITH (NOLOCK) ON PPH.pybatchid =PPD.pybatchid JOIN Process_DirectDeposit_Header PDH WITH (NOLOCK) ON PPH.pybatchid = PDH.pybatchid JOIN Process_DirectDeposit_Details PDD WITH (NOLOCK) ON PDH.doc_no = PDD.doc_no WHERE PPE.ok_to_post IN ('N','Y') AND  ");

                    if (parameters1[0] != null)
                        SQL.Append("PPH.pybatchid='" + parameters1[0].ToString() + "'");

                    if (parameters1[1] != null)
                        SQL.Append(" and PPH.Districts IN  ('" + parameters1[1].ToString() + "')");


                    SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), objDalBaseClass.ConnectionString);
                    da.Fill(ds_);

                    if (ds_.Tables.Count > 0 && ds_.Tables[0].Rows.Count > 0)
                    {
                        ViewBag.responseok = true;
                        IsPendioGenrated = true;
                        ViewBag.ispensiongenerated = IsPendioGenrated;
                    }
                    else
                    {
                        ViewBag.responseok = false;
                        IsPendioGenrated = false;
                        ViewBag.ispensiongenerated = IsPendioGenrated;
                    }
                    ViewBag.ispensiongenerated = IsPendioGenrated;

                }
            }
            catch (Exception ex)
            {
                ToastError(ex.Message);
                ViewBag.EmployeeType = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
                ViewBag.accounttype = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
            }
            return PartialView("~/Views/PensionProcess/DirectDeposits/PrintBankListing.cshtml", generatePensionProcess);
        }

        public ActionResult DownloadDisbursement()
        {
            generatePensionProcess = new GeneratePensionProcessModel();
            try
            {
                ViewBag.IsGeneratePensionWithoutSftp = IsGeneratePensionWithoutSftp;
                int userid = AppUserManager.GetUserId();
                var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();

                var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionProcess" && x.ActionName == "PrintBankListing")
                             join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                             from p in ps.DefaultIfEmpty()
                             select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Print Bank Listing", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

                if (model != null)
                {
                    ViewBag.AddPermission = model.AddPermssion;


                    ViewBag.EditPermission = model.EditPermission;
                }
                PensionProcessViewModel Paysearch = ShowActiveBatch();
                string disData = string.Empty;
                BindCmbBanckCode(string.Empty);
                BindComboEmployeeType(Paysearch.EmpType);

                generatePensionProcess.DepositDate = Paysearch.PayrollDate;
                generatePensionProcess.PayDate = DVOApplicationUserInfo.DateConvertion(GetCurrentDateAsPerFinancialYear());
                ViewBag.pybatchid = Paysearch.pybatchid;
                Paysearch.RegionNames = Paysearch.RegionNames;
                if (Paysearch.RegionNames != "" && Paysearch.RegionNames != null)
                {
                    disData = Paysearch.RegionNames.ToLower().Trim();
                }
                ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);

                Dictionary<string, string> PensionDetailByMonthYear = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear();
                ViewBag.CurrentMonthBatch = PensionDetailByMonthYear;
                if (PensionDetailByMonthYear == null)
                {
                    ViewBag.pybatchid = 0;
                }
                else
                {
                    ViewBag.pybatchid = PensionDetailByMonthYear["pybatchid"];
                }
                if (PensionDetailByMonthYear != null)
                {

                    string[] regions = PensionDetailByMonthYear["Districts"].Split(',').Select(region => region.Trim()).ToArray();
                    var filteredRegions = regions.Where(region => region != "JAMMU REGION" && region != "KASHMIR REGION");
                    string Paydistricts = string.Join(", ", filteredRegions);

                    object[] parameters1 = new object[2];
                    parameters1[0] = PensionDetailByMonthYear["pybatchid"];
                    parameters1[1] = PensionDetailByMonthYear["Districts"];

                    DataSet ds_ = new DataSet();
                    StringBuilder SQL = new StringBuilder();
                    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                    object objTransaction = objDALBaseClassHelper.GetTransactionObject();
                    //SQL.Append("SELECT Districts FROM Payroll_Process_Header WHERE pybatchid=" + parameters1[0].ToString());
                    SQL.Append("SELECT Top 1 ok_to_post  FROM Process_PayEmployee PPE WITH (NOLOCK) JOIN MasterEmployee ME WITH (NOLOCK) ON ME.empl_code = PPE.empl_code JOIN Payroll_Process_Header PPH WITH (NOLOCK) ON PPE.pybatchid =PPH.pybatchid JOIN Payroll_Process_Details PPD WITH (NOLOCK) ON PPH.pybatchid =PPD.pybatchid JOIN Process_DirectDeposit_Header PDH WITH (NOLOCK) ON PPH.pybatchid = PDH.pybatchid JOIN Process_DirectDeposit_Details PDD WITH (NOLOCK) ON PDH.doc_no = PDD.doc_no WHERE PPE.ok_to_post IN ('N','Y') AND  ");

                    if (parameters1[0] != null)
                        SQL.Append("PPH.pybatchid='" + parameters1[0].ToString() + "'");

                    if (parameters1[1] != null)
                        SQL.Append(" and PPH.Districts IN  ('" + parameters1[1].ToString() + "')");


                    SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), objDalBaseClass.ConnectionString);
                    da.Fill(ds_);

                    if (ds_.Tables.Count > 0 && ds_.Tables[0].Rows.Count > 0)
                    {
                        ViewBag.responseok = true;
                        IsPendioGenrated = true;
                        ViewBag.ispensiongenerated = IsPendioGenrated;
                    }
                    else
                    {
                        ViewBag.responseok = false;
                        IsPendioGenrated = false;
                        ViewBag.ispensiongenerated = IsPendioGenrated;
                    }
                    ViewBag.ispensiongenerated = IsPendioGenrated;

                }
            }
            catch (Exception ex)
            {
                ToastError(ex.Message);
                ViewBag.EmployeeType = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
                ViewBag.accounttype = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
            }
            return PartialView("~/Views/PensionProcess/DirectDeposits/DownloadDisbursement.cshtml", generatePensionProcess);
        }

        // Get current financial year batch list
        public List<int> GetCurrentFYearBatchList()
        {
            List<int> BatchIds = new List<int>();
            try
            {
                string startDate = GetStartDateDateAsPerFinancialYear().ToString("yyyy-MM-dd");
                string endDate = GetEndDateDateAsPerFinancialYear().ToString("yyyy-MM-dd");

                List<TotalContributionSummaryViewModel> listCounts = new List<TotalContributionSummaryViewModel>();
                DataSet ds = new DataSet();
                StringBuilder SQL = new StringBuilder();
                SQL.Append(" SELECT DISTINCT PPH.pybatchid FROM Payroll_Process_Header AS PPH LEFT JOIN Process_PayEmployee PPE" +
                    " ON PPE.pybatchid = PPH.pybatchid WHERE PPE.ok_to_post IN ('P','N','Y') AND CONVERT(DATETIME, " +
                    "SUBSTRING(PPH.searchcriteria,CHARINDEX('Payroll Date: ', PPH.searchcriteria) + LEN('Payroll Date: '), 11), 103)" +
                    " BETWEEN '" + startDate.Trim().Replace("'", "''") + "' and '" + endDate.Trim().Replace("'", "''") + "'");

                string con = ConnectionStringProvider.GetConnectionString();
                SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BatchIds.Add(dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                    }
                }
            }
            catch
            {

            }
            return BatchIds;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrintBankListing([Bind(Include = "DepositDate,BanckCode,EmpType,ChequeNumber,Preparedby,Checkedby,Approvedby,Receivedby,RegionNames,PayDate")] GeneratePensionProcessModel generatePensionProcessModel, FormCollection form)
        {
            try
            {
                string query = @"SELECT TOP 1 1 AS IsActiveRecordExists  FROM [dbo].[MasterEmpBankDetails] WHERE ACCOUNT_STATUS = 'ACTIVE';";
                string con = ConnectionStringProvider.GetConnectionString();

                ViewBag.IsGeneratePensionWithoutSftp = IsGeneratePensionWithoutSftp;
                if (generatePensionProcessModel.PayDate == null)
                {
                    ViewBag.EmployeeType = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
                    ViewBag.accounttype = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
                    TempData["error"] = "Pay Date is required.";
                    Dictionary<string, string> PensionDetailByMonthYear = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear();
                    ViewBag.CurrentMonthBatch = PensionDetailByMonthYear;
                    if (PensionDetailByMonthYear == null)
                    {
                        ViewBag.pybatchid = 0;
                    }
                    else
                    {
                        ViewBag.pybatchid = PensionDetailByMonthYear["pybatchid"];
                    }
                    return PartialView("~/Views/PensionProcess/DirectDeposits/PrintBankListing.cshtml", generatePensionProcessModel);
                }
                // The pObjBatch object used to hold active batch information.
                DVOPYBatchProcessStybatchr pObjBatch = new DVOPYBatchProcessStybatchr();
                pObjBatch.searchcriteria = string.Empty;

                PensionProcessViewModel Paysearch = ShowActiveBatch();
                string disData = string.Empty;
                BindCmbBanckCode(generatePensionProcessModel.BanckCode);
                BindComboEmployeeType(generatePensionProcessModel.EmpType);
                var RegionNames = generatePensionProcessModel.RegionNames == null ? Paysearch.RegionNames : generatePensionProcessModel.RegionNames;
                if (RegionNames != "" && RegionNames != null)
                {
                    disData = RegionNames.ToLower().Trim();
                }
                ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);

                // new code 
                if (generatePensionProcessModel.PayDate != null)
                {
                    string paydate = generatePensionProcessModel.PayDate.Value.ToShortDateString();
                    DateTime parsedDate = Convert.ToDateTime(paydate, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);

                    string Paymonth = parsedDate.ToString("MM");
                    string Payyear = parsedDate.ToString("yyyy");
                    string[] regions = generatePensionProcessModel.RegionNames.Split(',').Select(region => region.Trim()).ToArray();
                    var filteredRegions = regions.Where(region => region != "JAMMU REGION" && region != "KASHMIR REGION");
                    string Paydistricts = string.Join(", ", filteredRegions);

                    object[] parameters1 = new object[3];
                    parameters1[0] = Paymonth;
                    parameters1[1] = Payyear;
                    parameters1[2] = Paydistricts;
                    DataSet ds_ = new DataSet();
                    StringBuilder SQL = new StringBuilder();
                    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                    object objTransaction = objDALBaseClassHelper.GetTransactionObject();
                    //SQL.Append("SELECT Districts FROM Payroll_Process_Header WHERE pybatchid=" + parameters1[0].ToString());
                    SQL.Append("SELECT ok_to_post  FROM Process_PayEmployee PPE WITH (NOLOCK) JOIN MasterEmployee ME WITH (NOLOCK) ON ME.empl_code = PPE.empl_code JOIN Payroll_Process_Header PPH WITH (NOLOCK) ON PPE.pybatchid =PPH.pybatchid JOIN Payroll_Process_Details PPD WITH (NOLOCK) ON PPH.pybatchid =PPD.pybatchid JOIN Process_DirectDeposit_Header PDH WITH (NOLOCK) ON PPH.pybatchid = PDH.pybatchid JOIN Process_DirectDeposit_Details PDD WITH (NOLOCK) ON PDH.doc_no = PDD.doc_no WHERE PPE.ok_to_post IN ('P', 'Y') AND  ");

                    //if (parameters1[0] != null)
                    //    SQL.Append("pybatchid=" + parameters1[0].ToString() AND);

                    if (parameters1[0] != null)
                        SQL.Append(" MONTH(CONVERT(DATE, SUBSTRING(PPH.searchcriteria, CHARINDEX('Payroll Date: ', PPH.searchcriteria) + LEN('Payroll Date: '), 11), 103)) = '" + parameters1[0].ToString() + "'");

                    if (parameters1[1] != null)
                        SQL.Append(" AND YEAR(CONVERT(DATE, SUBSTRING(PPH.searchcriteria, CHARINDEX('Payroll Date: ', PPH.searchcriteria) + LEN('Payroll Date: '), 11), 103)) = '" + parameters1[1].ToString() + "'");

                    if (parameters1[2] != null)
                        SQL.Append(" and PPH.Districts IN  ('" + parameters1[2].ToString() + "')");


                    SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), objDalBaseClass.ConnectionString);
                    da.Fill(ds_);

                    if (ds_.Tables.Count > 0 && ds_.Tables[0].Rows.Count > 0)
                    {
                        TempData["error"] = "Process is already in progress.";
                        Dictionary<string, string> PensionDetailByMonthYears = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear();
                        ViewBag.CurrentMonthBatch = PensionDetailByMonthYears;
                        if (PensionDetailByMonthYears == null)
                        {
                            ViewBag.pybatchid = 0;
                        }
                        else
                        {
                            ViewBag.pybatchid = PensionDetailByMonthYears["pybatchid"];
                        }
                        return PartialView("~/Views/PensionProcess/DirectDeposits/PrintBankListing.cshtml", generatePensionProcessModel);
                    }


                }

                // check Active Record available add by rohit
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        if (dataSet.Tables[0].Rows.Count == 0)
                        {
                            ViewBag.EmployeeType = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
                            ViewBag.accounttype = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
                            TempData["error"] = "Active Record is not available, so payroll is not generated.";
                            Dictionary<string, string> PensionDetailByMonthYear = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear();
                            ViewBag.CurrentMonthBatch = PensionDetailByMonthYear;
                            if (PensionDetailByMonthYear == null)
                            {
                                ViewBag.pybatchid = 0;
                            }
                            else
                            {
                                ViewBag.pybatchid = PensionDetailByMonthYear["pybatchid"];
                            }
                            return PartialView("~/Views/PensionProcess/DirectDeposits/PrintBankListing.cshtml", generatePensionProcessModel);

                        }
                    }
                }

                if (Paysearch.pybatchid != 0)
                {
                    TempData["error"] = "Process " + Paysearch.pybatchid + " is already in progress.";
                    Dictionary<string, string> PensionDetailByMonthYear = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear();
                    ViewBag.CurrentMonthBatch = PensionDetailByMonthYear;
                    if (PensionDetailByMonthYear == null)
                    {
                        ViewBag.pybatchid = 0;
                    }
                    else
                    {
                        ViewBag.pybatchid = PensionDetailByMonthYear["pybatchid"];
                    }
                    return PartialView("~/Views/PensionProcess/DirectDeposits/PrintBankListing.cshtml", generatePensionProcessModel);
                }
                Dictionary<string, string> ViewBagPensionDetailByMonthYear = null;
                try
                {
                    string disData1 = string.Empty;

                    var RegionName = generatePensionProcessModel.RegionNames == null ? Paysearch.RegionNames : generatePensionProcessModel.RegionNames;
                    if (RegionName != "" && RegionName != null)
                    {
                        disData1 = RegionName.Trim();
                    }
                    ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);
                    Dictionary<string, string> PensionDetailByMonthYear = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear(generatePensionProcessModel.PayDate);
                    ViewBagPensionDetailByMonthYear = PensionDetailByMonthYear;
                    // Implement condition to create new batch for the month.
                    // Batch may be fully cancelled or partially cancelled. 
                    // If batch fully cancelled then create new batch for the month.
                    // If batch partillay cancelled then generate new batch for cancelled record.
                    if (PensionDetailByMonthYear != null)
                    {
                        if (Convert.ToInt32(PensionDetailByMonthYear["totalCanceled"]) > 0)
                        {
                            PensionDetailByMonthYear = null;
                        }
                    }

                    if (PensionDetailByMonthYear == null)
                    {
                        if (generatePensionProcessModel.PayDate != null)
                        {
                            pObjBatch.searchcriteria += "Payroll Date: " + DVOApplicationUserInfo.DateConvertionStr(generatePensionProcessModel.PayDate) + ",";
                        }

                        if (!string.IsNullOrEmpty(disData))
                        {
                            pObjBatch.searchcriteria += "District IN: (" + disData + ")";
                        }

                        int userid = AppUserManager.GetUserId();
                        DVOPYBatchProcessStybatchr objDVOPYBatchProcessStybatchrINS = new DVOPYBatchProcessStybatchr();
                        objDVOPYBatchProcessStybatchrINS.searchcriteria = pObjBatch.searchcriteria;
                        objDVOPYBatchProcessStybatchrINS.insertby = userid;
                        objDVOPYBatchProcessStybatchrINS.insertmachineinfo = System.Environment.MachineName;
                        //objDVOPYBatchProcessStybatchrINS.startedon = DVOApplicationUserInfo.DateConvertionStr(DateTime.Now);
                        //objDVOPYBatchProcessStybatchrINS.startedon = DVOApplicationUserInfo.DateConvertionStr(generatePensionProcessModel.PayDate);
                        objDVOPYBatchProcessStybatchrINS.startedon = DVOApplicationUserInfo.DateConvertionStr(DateTime.Now);
                        objDVOPYBatchProcessStybatchrINS.Districts = disData1;
                        int INSResult = BLLPYBatchProcessStybatchr.INSERTBatchProcessInfo(ref objDVOPYBatchProcessStybatchrINS);
                    }
                    else
                    {
                        Dictionary<string, string> PensionDetailByMonthYears = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear();
                        TempData["error"] = "Process " + PensionDetailByMonthYear["pybatchid"] + " was generated for this month.";

                        ViewBag.CurrentMonthBatch = PensionDetailByMonthYears;
                        if (PensionDetailByMonthYear == null)
                        {
                            ViewBag.pybatchid = 0;
                        }
                        else
                        {
                            ViewBag.pybatchid = PensionDetailByMonthYear["pybatchid"];
                        }
                        return PartialView("~/Views/PensionProcess/DirectDeposits/PrintBankListing.cshtml", generatePensionProcessModel);
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    Dictionary<string, string> PensionDetailByMonthYear = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear();
                    ViewBag.CurrentMonthBatch = PensionDetailByMonthYear; if (PensionDetailByMonthYear == null)
                    {
                        ViewBag.pybatchid = 0;
                    }
                    else
                    {
                        ViewBag.pybatchid = PensionDetailByMonthYear["pybatchid"];
                    }
                    return PartialView("~/Views/PensionProcess/DirectDeposits/PrintBankListing.cshtml", generatePensionProcessModel);
                }

                Paysearch = ShowActiveBatch();

                #region Execute Report
                //Added by Neeraj on 16/07/2015 , To implement batch process, Nedd to add
                //A parameter 'ref pObjBatch' in 'GetPayslipDetails' function
                //and remove comment from the code written for batch process logic in 'PYBatchManagement' region.
                //and remove comment from 'pObjBatch.searchcriteria'.
                #region PYBatchManagement


                //Get Payroll Active batch,if any. 
                List<DVOPYBatchProcessStybatchr> objBatchList = BLLPYBatchProcessStybatchr.GetActiveBatch();


                if (generatePensionProcessModel.DepositDate == null)
                {
                    //TempData["error"] = "Select Deposit Date First";
                    generatePensionProcessModel.DepositDate = DVOApplicationUserInfo.DateConvertion(generatePensionProcessModel.DepositDate); ;
                    //return PartialView("~/Views/PensionProcess/DirectDeposits/PrintBankListing.cshtml", generatePensionProcessModel);
                }
                if (string.IsNullOrWhiteSpace(generatePensionProcessModel.BanckCode))
                {
                    //TempData["error"] = "Select Bank Code First";
                    //return PartialView("~/Views/PensionProcess/DirectDeposits/PrintBankListing.cshtml", generatePensionProcessModel);
                }

                if (objBatchList != null && objBatchList.Count > 0)
                {
                    pObjBatch = objBatchList[0];
                }
                else
                {
                    //Utilities.ShowMessage("No Active Batch for Payroll Process,Please Create a New Batch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TempData["error"] = "No Active Batch for Payroll Process,Please Create a New Batch";
                    //return PartialView("~/Views/PensionProcess/DirectDeposits/PrintBankListing.cshtml", generatePensionProcessModel);
                }
                #endregion PYBatchManagement


                //Get data from database and bind with report                 
                DVOddmStypddreAndStypddrd objSearch = new DVOddmStypddreAndStypddrd();


                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.EmpType))
                {
                    objSearch.type_code = generatePensionProcessModel.EmpType.Trim();
                }
                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.BanckCode))
                {
                    objSearch.BanckCode = generatePensionProcessModel.BanckCode.Trim();
                }
                if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.RegionNames))
                {
                    objSearch.District = generatePensionProcessModel.RegionNames.Replace(", ", ",");
                }
                if (generatePensionProcessModel.DepositDate != null)
                {
                    string payDate = Convert.ToDateTime(generatePensionProcessModel.DepositDate).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    objSearch.Date = Convert.ToDateTime(generatePensionProcessModel.DepositDate);
                }
                Paysearch.PayrollDate = DVOApplicationUserInfo.DateConvertion(generatePensionProcessModel.PayDate);
                if (generatePensionProcessModel.PayDate == null || generatePensionProcessModel.PayDate == DateTime.Parse("01/01/1900"))
                {
                    Paysearch.PayrollDate = DVOApplicationUserInfo.DateConvertion(DateTime.Now);
                }
                Paysearch.EOPDate = DVOApplicationUserInfo.DateConvertion(generatePensionProcessModel.PayDate);
                if (generatePensionProcessModel.PayDate == null || generatePensionProcessModel.PayDate == DateTime.Parse("01/01/1900"))
                {
                    Paysearch.EOPDate = DVOApplicationUserInfo.DateConvertion(DateTime.Now);
                }

                //function of BLL layer will return DataSet which contains result of search process.            
                DataSet ds = ReportingUtilities.GetDirectDepositeListing(ref objSearch, Paysearch, true);
                //ds.WriteXmlSchema(@"D:\Sujeet_Sir\Project\JKPS\App.Web\Reports\DSDDL.xsd");
                ViewBag.pybatchid = Paysearch.pybatchid;
                if (ds.Tables[0].Rows.Count != 0)
                {

                    //List<string> districtList = generatePensionProcessModel.RegionNames.Split(',').Select(d => d.Trim().ToUpper()).ToList();

                    //var District = db.MasterEmployees.Where(x => districtList.Any(b => b == x.SelectDistrict.Trim().ToUpper())).AsEnumerable().GroupBy(p => p.SelectDistrict.Trim().ToUpper()).Select(a => a.FirstOrDefault().SelectDistrict.Trim().ToUpper()).ToList();
                    var districtList = new HashSet<string>(generatePensionProcessModel.RegionNames.Split(',').Select(d => d.Trim().ToUpper()).ToList());
                    var District = db.MasterEmployees.AsNoTracking().Where(x => districtList.Contains(x.SelectDistrict.Trim().ToUpper()))
                        .Select(x => x.SelectDistrict.Trim().ToUpper()).Distinct().ToList();

                    bool GenerateCheques_user = false;

                    for (int i = 0; i < District.Count; i++)
                    {
                        if (i == District.Count - 1)
                        {
                            GenerateCheques_user = false;
                        }


                        DirectGenerateBankMedia(Paysearch, generatePensionProcessModel, District[i], GenerateCheques_user);
                    }

                    // code added on 19 sep 2024 to update Process_DirectDeposit_Header (create_date,used)
                    DataSet dsIsUsed_ = new DataSet();
                    StringBuilder SQLisUsed = new StringBuilder();
                    DALBaseClassHelper objDALBaseClassHelperisUsed = new DALBaseClassHelper();
                    DALBaseClass objDalBaseClass = objDALBaseClassHelperisUsed.GetDAL();
                    object objTransaction = objDALBaseClassHelperisUsed.GetTransactionObject();

                    SQLisUsed.Append("UPDATE Process_DirectDeposit_Header SET used='Y' where pybatchid=" + Paysearch.pybatchid.ToString() + "");

                    SqlDataAdapter daisUsed = new SqlDataAdapter(SQLisUsed.ToString(), objDalBaseClass.ConnectionString);
                    daisUsed.Fill(dsIsUsed_);
                    TempData["success"] = "File has been successfully sent.";
                }
                else
                {
                    TempData["error"] = "No records to process";
                }
                ViewBagPensionDetailByMonthYear = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear();
                ViewBag.CurrentMonthBatch = ViewBagPensionDetailByMonthYear;
                IsPendioGenrated = true;
                ViewBag.ispensiongenerated = IsPendioGenrated;
                return PartialView("~/Views/PensionProcess/DirectDeposits/PrintBankListing.cshtml", generatePensionProcessModel);
                #endregion
            }
            catch (Exception ex)
            {
                //ReportViewer.ReportSource = null;
                //SatyaPay.StyleUtility.Reports_Splasher.Close();
                //ExceptionManagement.ExceptionManager.Publish(ex);
                //MessageBox.Show(ex.Message, "Satya Pay.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TempData["error"] = "Please Try Again..." + ex.Message;
                return PartialView("~/Views/PensionProcess/DirectDeposits/PrintBankListing.cshtml", generatePensionProcessModel);
            }
        }


        public string DirectGenerateBankMedia(PensionProcessViewModel Paysearch, GeneratePensionProcessModel generatePensionProcessModel, string District, bool GenerateCheques_user)
        {
            try
            {
                string disData = string.Empty;
                var RegionNames = generatePensionProcessModel.RegionNames == null ? Paysearch.RegionNames : generatePensionProcessModel.RegionNames;
                if (RegionNames != "" && RegionNames != null)
                {
                    disData = RegionNames.ToLower().Trim();
                }

                int apBatchID = -1;
                // Set Default value
                generatePensionProcessModel.GenerateCheques = "N";
                if (generatePensionProcessModel.GenerateCheques.Trim() == "Y")
                {
                    apBatchID = -1;
                    bool IsMasterBatch = false;
                    string _APCurrentUser = string.Empty;
                    BLLBatchMaintenance.GetActiveBatchForReports(DVOApplicationUserInfo.LoginId, BatchTypes.APCashDeposit, out _APCurrentUser, out apBatchID, out IsMasterBatch);
                }
                bool sameMediaFormat = true;
                DVOMasterBankDetails objDVOMasterBankDetails = new DVOMasterBankDetails();
                List<DVOMasterBankDetails> listDVOMasterBankDetails = new List<DVOMasterBankDetails>();
                System.Collections.ArrayList _arrBankCodes = new System.Collections.ArrayList();
                string _selectedBankCodes = GetSelectedBankCodes(out _arrBankCodes, generatePensionProcessModel.BanckCode); //form["BanckCode"].Trim();
                                                                                                                            //if (_selectedBankCodes.Trim().Length > 0 && _arrBankCodes.Count > 0)
                listDVOMasterBankDetails = BLLPRBankMediaInybankd.GetData(ref objDVOMasterBankDetails, true, _selectedBankCodes);
                if (listDVOMasterBankDetails.Count > 1)
                {
                    if (listDVOMasterBankDetails.Count == _arrBankCodes.Count)
                    {
                        for (int i = 0; i < listDVOMasterBankDetails.Count - 1; i++)
                        {
                            if (listDVOMasterBankDetails[i].media_str.Trim() != listDVOMasterBankDetails[i + 1].media_str.Trim())
                            {
                                sameMediaFormat = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        sameMediaFormat = false;
                    }
                }
                else
                {
                    sameMediaFormat = true;
                }
                if (!sameMediaFormat)
                {
                    //SatyaPayMain.CommonUtilities.Utilities.ShowMessage("All selected Banks have not same Media-Format or some selected Bank has not Media-Format.\nSo you can't create Media for these Banks.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return "All selected Banks have not same Media-Format or some selected Bank has not Media-Format. So you can't create Media for these Banks.";
                }
                else
                {
                    string fileExtension = string.Empty;

                    // Format the date and time as a string (e.g., "yyyyMMdd_HHmmss")

                    //var region = Session["RegionName"].ToString();

                    var region = GetRegionName();
                    var directoryName = region == "KASHMIR REGION" ? "K_Disbursement.csv" : "J_Disbursement.csv";

                    string formattedName = $"{District}_{Paysearch.pybatchid}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}{directoryName}";


                    //string formattedName = $"{District}_{Paysearch.pybatchid}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}_JK_Disbursement.csv";

                    //ViewBag.FileName = generatePensionProcessModel.BanckCode + ".txt";
                    ViewBag.FileName = formattedName;
                    int TotalRowsCount = 0;
                    //Get data from database and bind with report                 
                    DVOddmStypddreAndStypddrd objDVOddmStypddreAndStypddrd = new DVOddmStypddreAndStypddrd();

                    foreach (string strBankCode in _arrBankCodes)
                    {
                        objDVOddmStypddreAndStypddrd.BanckCode = objDVOddmStypddreAndStypddrd.BanckCode + strBankCode + "^";
                    }
                    objDVOddmStypddreAndStypddrd.GenCheck = generatePensionProcessModel.GenerateCheques;
                    objDVOddmStypddreAndStypddrd.Date = generatePensionProcessModel.DepositDate != null ? Convert.ToDateTime(generatePensionProcessModel.DepositDate) : Convert.ToDateTime("01/01/1900");
                    //objDVOddmStypddreAndStypddrd.bank_code = generatePensionProcessModel.BanckCode;
                    Paysearch.PayrollDate = DVOApplicationUserInfo.DateConvertion(generatePensionProcessModel.PayDate);
                    if (generatePensionProcessModel.PayDate == null || generatePensionProcessModel.PayDate == DateTime.Parse("01/01/1900"))
                    {
                        Paysearch.PayrollDate = DVOApplicationUserInfo.DateConvertion(DateTime.Now);
                    }
                    string paydate = Paysearch.PayrollDate.Value.ToShortDateString();
                    DateTime parsedDate = Convert.ToDateTime(paydate, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                    objDVOddmStypddreAndStypddrd.District = disData;
                    string Paydate_value = parsedDate.ToString("MM/dd/yyyy");
                    DataTable objDataTable;

                    #region Create excel file with 4 empty column
                    int UserId = AppUserManager.GetUserId();
                    Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
                    // Define the path for sql create SVC

                    //var regions = Session["RegionName"].ToString();
                    var regions = GetRegionName();
                    var directorysName = regions == "KASHMIR REGION" ? "K_BankMediaFile" : "J_BankMediaFile";
                    //string filePath = ftpSetting["localFilePath"] + $"\\DataFiles\\{directorysName}\\" + formattedName;

                    string filePath = Helper.Helper.DirectGenerateBankMediaPath(region, directorysName, formattedName);

                    //string filePath = ftpSetting["localFilePath"] + "\\DataFiles\\BankMediaFile\\" + formattedName;

                    //List<MasterEmpBankDetails> bankIFSC = db.MasterEmpBankDetails.Where(x => x.ACCOUNT_STATUS == "ACTIVE").Distinct().ToList(); //.Select(x => new MasterEmpBankDetails { APPLICANT_BANK_IFSC_CODE = x.APPLICANT_BANK_IFSC_CODE, BankName = x.BankName }).Distinct().ToList();

                    //List<string> ifsc = bankIFSC.Select(x => x.APPLICANT_BANK_IFSC_CODE).Distinct().ToList();


                    //foreach (var item in ifsc)
                    //{
                    //string bankName = bankIFSC.Where(x => x.APPLICANT_BANK_IFSC_CODE == item).Select(x => x.BankName).FirstOrDefault();

                    // Define the parameters if needed (e.g., for input parameters)
                    SqlParameter[] parameter = {
                        //new SqlParameter("@DepositDate", generatePensionProcessModel.DepositDate.Value.ToShortDateString()),
                        //new SqlParameter("@BankCode", generatePensionProcessModel.BanckCode),
                        //new SqlParameter("@District", generatePensionProcessModel.DirectDepositsCheques),
                        //new SqlParameter("@EmployeeType", generatePensionProcessModel.EmpType),
                         new SqlParameter("@DepositDate", Paydate_value),
                        new SqlParameter("@filePathWithName", filePath),
                        new SqlParameter("@UserId", UserId),
                        //new SqlParameter("@IFSC", item),
                        new SqlParameter("@district", District),
                        };
                    // Execute the stored procedure
                    //var result = db.Database.SqlQuery<string>("EXEC GenerateBankMediaFile @filePathWithName, @UserId, @district", parameter).ToList();
                    var result = db.Database.SqlQuery<string>("EXEC GenerateBankMediaFile @DepositDate,@filePathWithName, @UserId, @district", parameter).ToList();
                    if (result.Any())
                    {
                        if (result[0] != "There is no record to create media.")
                        {
                            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                            object[] parameters = new object[20];
                            parameters[0] = Paysearch.pybatchid; // RecordId 
                            parameters[1] = string.Empty;//ftpSetting["sftpFilePath"] + "/Disbursement/Outbox/" + uploadformattedName; // FilePath
                            parameters[2] = true; // IsUploaded
                            parameters[3] = DateTime.Now; // UploadedDate
                            parameters[4] = Environment.MachineName; // CreatedMachineInfo
                            parameters[5] = AppUserManager.GetUserId(); // CreatedBy
                            parameters[6] = DateTime.Now; // CreatedOn
                            parameters[7] = true; // IsActive
                            parameters[8] = string.Empty; // UploadErrors
                            parameters[9] = true; // IsReUploaded
                            parameters[10] = Convert.ToInt32(MediaType.TxnReport); // MediaType
                                                                                   // Execute the stored procedure
                            parameters[11] = 0;
                            parameters[12] = 0;
                            parameters[13] = 0;
                            parameters[14] = formattedName; // FilePath;
                            parameters[15] = District;


                            // Reupload Value  *** Code By Himanshu Rajput ***

                            parameters[16] = false;   // IsReUploadedPermitted
                            parameters[17] = 0;  // ReUploadedPermittedBy
                            parameters[18] = null;  // ReUploadedPermittedDate
                            parameters[19] = 0; // FieSize

                            objDalBaseClass.ExecuteProcedure(ref parameters, "SaveMediaQueueDetail");
                            //objDalBaseClass.ExecuteStoredProcedure("USP_PaymentTotalCount_JOBS");
                        }

                    }

                    #endregion
                    if (GenerateCheques_user)
                    {
                        if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.GenerateCheques))
                        {
                            if (generatePensionProcessModel.GenerateCheques.Trim() == "Y")
                            {
                                objDataTable = BLLDirectDepositMedia.GetDirectDepositeData(ref objDVOddmStypddreAndStypddrd, true, apBatchID);
                            }
                            else
                            {
                                objDataTable = BLLDirectDepositMedia.GetDirectDepositeData(ref objDVOddmStypddreAndStypddrd, false, apBatchID);
                            }
                        }
                        else
                        {
                            objDataTable = BLLDirectDepositMedia.GetDirectDepositeData(ref objDVOddmStypddreAndStypddrd, false, apBatchID);
                        }
                        string strBankCodeN = _arrBankCodes[0].ToString().Trim();
                        if (objDataTable != null)
                            if (objDataTable.Rows.Count > 0)
                            {
                                TotalRowsCount += objDataTable.Rows.Count;
                                string mediaFormat = string.Empty;
                                objDVOMasterBankDetails = listDVOMasterBankDetails.Find(delegate (DVOMasterBankDetails objParm)
                                {
                                    return objParm.bank_code.Trim() == strBankCodeN.Trim();
                                });

                            }
                        if (TotalRowsCount <= 0)
                        {
                            return "There is no record to create media.";
                        }
                        else
                        {
                            return "File has been successfully saved.";
                        }
                    }
                }
                return "File has been successfully saved.";
            }
            catch (Exception ex)
            {
                return "Please Try Again..." + ex.Message;
            }
        }

        public ActionResult SFTPfileSendAjax(string batch_value, string distict_value)
        {
            try
            {
                // is reupload permission
                // is already uploaded 
                int batchId = 0;
                if (string.IsNullOrEmpty(batch_value))
                {
                    batch_value = "0";
                }

                batchId = Convert.ToInt32(batch_value);

                bool isReuploadPermission = db.Media_Queue.AsNoTracking().Where(x => x.RecordId == batchId
                && (x.Districts + "").Trim().ToUpper() == distict_value.Trim().ToUpper()).Select(y => y.IsReUploaded).FirstOrDefault();

                if (!isReuploadPermission)
                {
                    return Json("Already uploaded.");
                }

                object[] parameters1 = new object[2];
                parameters1[0] = batch_value;
                parameters1[1] = distict_value;

                DataSet ds = new DataSet();
                StringBuilder SQL = new StringBuilder();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object objTransaction = objDALBaseClassHelper.GetTransactionObject();
                string formattedName = "";

                SQL.Append("SELECT FTPFileName FROM Media_Queue WHERE ");
                if (parameters1[0] != null)
                    SQL.Append("RecordId=" + parameters1[0].ToString());

                if (parameters1[1] != null)
                    SQL.Append(" AND Districts='" + parameters1[1].ToString() + "'");

                // Execute the query and fill the DataSet
                SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), objDalBaseClass.ConnectionString);
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    formattedName = ds.Tables[0].Rows[0][0].ToString();
                }

                int UserId = AppUserManager.GetUserId();
                Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
                // Define the path for sql create SVC

                //var region = Session["RegionName"].ToString();
                var region = GetRegionName();
                var directoryName = region == "KASHMIR REGION" ? "K_BankMediaFile" : "J_BankMediaFile";
                //string filePath = ftpSetting["localFilePath"] + $"\\DataFiles\\{directoryName}\\" + formattedName;
                string filePath = Helper.Helper.AddExcelSheetIntoDatabasePath(region, directoryName, formattedName);

                //string filePath = ftpSetting["localFilePath"] + "\\DataFiles\\BankMediaFile\\" + formattedName;
                // Create an FTP client
                WebClient ftpClient = new WebClient();
                ftpClient.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);

                //string path = ftpSetting["ftpServerUrl"] + $"/DataFiles/{directoryName}/" + formattedName;

                string path = Helper.Helper.AddExcelSheetIntoDatabasePath1(region, directoryName, formattedName);

                //string path = ftpSetting["ftpServerUrl"] + ("/DataFiles/BankMediaFile/" + formattedName);

                // File path, attampt, dealy in attampt
                bool isExist = Helper.Helper.FileCheckInFTP(path, 3, 30);
                if (isExist)
                {
                    // Download the file from the FTP server
                    byte[] fileData = ftpClient.DownloadData(path);

                    // Specify the file path where you want to save the uploaded file

                    //DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/BankMediaFile"));
                    //DirectoryInfo dis = di.GetDirectories(DateTime.Now.ToShortDateString().Replace("/", "_")).Any() ? di.GetDirectories(DateTime.Now.ToShortDateString().Replace("/", "_")).FirstOrDefault() : di.CreateSubdirectory(DateTime.Now.ToShortDateString().Replace("/", "_"));
                    //string path = Path.Combine(dis.FullName, generatePensionProcessModel.BanckCode.Trim() + ".txt");

                    //var regions = Session["RegionName"].ToString();
                    var regions = GetRegionName();
                    var directoryNames = regions == "KASHMIR REGION" ? "K_Disbursement" : "J_Disbursement";
                    //string serverMapPath = Server.MapPath($"~/BankMediaFile/{directoryNames}");
                    string serverMapPath = Helper.Helper.GetAllFilesPath(region, directoryNames);

                    //string serverMapPath = Server.MapPath("~/BankMediaFile/JK_Disbursement");
                    if (!Directory.Exists(serverMapPath))
                    {
                        // Attempt to create the directory
                        Directory.CreateDirectory(serverMapPath);
                    }
                    // DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/BankMediaFile"));
                    //DirectoryInfo dis = di.GetDirectories(DateTime.Now.ToShortDateString().Replace("/", "_")).Any() ? di.GetDirectories(DateTime.Now.ToShortDateString().Replace("/", "_")).FirstOrDefault() : di.CreateSubdirectory(DateTime.Now.ToShortDateString().Replace("/", "_"));
                    // Add file name with directory
                    string csvFilePath = Path.Combine(serverMapPath, formattedName);

                    // Save the file to the server
                    System.IO.File.WriteAllBytes(csvFilePath, fileData);

                    string excelFilePath = csvFilePath.Replace(".csv", ".xlsx");

                    // delete file in safe way
                    FileHelper fileHelper = new FileHelper();
                    bool isFileDeleted = fileHelper.TryDeleteFile(excelFilePath);

                    //Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    //Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(csvFilePath);
                    //wb.SaveAs(excelFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
                    //wb.Close(false);
                    //app.Quit();
                    var workbook = new XLWorkbook();
                    var worksheet = workbook.Worksheets.Add("Sheet1");

                    string[] lines = System.IO.File.ReadAllLines(csvFilePath);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] values = lines[i].Split(',');
                        for (int j = 0; j < values.Length; j++)
                        {
                            worksheet.Cell(i + 1, j + 1).Value = values[j];
                        }
                    }

                    workbook.SaveAs(excelFilePath);

                    // delete file in safe way
                    bool isCsvFileDeleted = fileHelper.TryDeleteFile(csvFilePath);

                    //var regionss = Session["RegionName"].ToString();
                    var regionss = GetRegionName();
                    var directoryNamess = regionss == "KASHMIR REGION" ? "K_Disbursement.xlsx" : "J_Disbursement.xlsx";
                    string uploadformattedName = $"{distict_value}_{batch_value}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}{directoryNamess}";




                    //string uploadformattedName = $"{distict_value}_{batch_value}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}_JK_Disbursement.xlsx";

                    var ftpAndStpWritePath = UploadDisbursementFile(path, uploadformattedName);
                    //A/var ftpAndStpWritePath= UploadDisbursementFile(excelFilePath, uploadformattedName);


                    FileInfo fileInfo = new FileInfo(excelFilePath);

                    // Get the size of the file in bytes
                    long fileSizeInBytes = fileInfo.Length;
                    double filesize = fileSizeInBytes / 1024.0;

                    //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                    //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                    object[] parameters = new object[20];
                    parameters[0] = batch_value; // RecordId 
                    parameters[1] = ftpAndStpWritePath;//ftpSetting["sftpFilePath"] + $"/{directoryName}/Outbox/" + uploadformattedName; // FilePath
                    parameters[2] = true; // IsUploaded
                    parameters[3] = DateTime.Now; // UploadedDate
                    parameters[4] = Environment.MachineName; // CreatedMachineInfo
                    parameters[5] = AppUserManager.GetUserId(); // CreatedBy
                    parameters[6] = DateTime.Now; // CreatedOn
                    parameters[7] = true; // IsActive
                    parameters[8] = string.Empty; // UploadErrors
                    parameters[9] = false; // IsReUploaded
                    parameters[10] = Convert.ToInt32(MediaType.TxnReport); // MediaType
                                                                           // Execute the stored procedure
                    parameters[11] = 0;
                    parameters[12] = 0;
                    parameters[13] = 0;
                    parameters[14] = formattedName; // FilePath;
                    parameters[15] = distict_value;

                    // Reupload Value  *** Code By Himanshu Rajput ***

                    parameters[16] = false;   // IsReUploadedPermitted
                    parameters[17] = 0;  // ReUploadedPermittedBy
                    parameters[18] = null;  // ReUploadedPermittedDate
                    parameters[19] = filesize;  // File Size

                    objDalBaseClass.ExecuteProcedure(ref parameters, "SaveMediaQueueDetail");
                    //return Json("File has been successfully saved.");
                    return Json(ftpAndStpWritePath);
                }
                else {
                    return Json($"File does not exist on path '{path}'.");
                }
                    
            }
            catch (Exception ex)
            {
                return Json("Please Try Again..." + ex.Message);
            }
        }

        public ActionResult DownLoadBankDisbursementFileAjax(string batch_value, string distict_value)
        {
            try
            {
                if (string.IsNullOrEmpty(batch_value)) batch_value = "0";
                int batchId = Convert.ToInt32(batch_value);

                object[] parameters1 = new object[2];
                parameters1[0] = batch_value;
                parameters1[1] = distict_value;

                DataSet ds = new DataSet();
                StringBuilder SQL = new StringBuilder();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                string formattedName = "";

                SQL.Append("SELECT FTPFileName FROM Media_Queue WHERE ");
                SQL.Append("RecordId=" + parameters1[0]);
                if (!string.IsNullOrEmpty(distict_value))
                    SQL.Append(" AND Districts='" + distict_value + "'");

                SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), objDalBaseClass.ConnectionString);
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    formattedName = ds.Tables[0].Rows[0][0].ToString();
                }

                Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
                var region = GetRegionName();
                var directoryName = region == "KASHMIR REGION" ? "K_BankMediaFile" : "J_BankMediaFile";

                string ftpPath = Helper.Helper.AddExcelSheetIntoDatabasePath1(region, directoryName, formattedName);
                bool isExist = Helper.Helper.FileCheckInFTP(ftpPath, 3, 30);

                if (isExist)
                {
                    var directoryNames = region == "KASHMIR REGION" ? "K_Disbursement" : "J_Disbursement";
                    WebClient ftpClient = new WebClient();
                    ftpClient.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);
                    byte[] fileData = ftpClient.DownloadData(ftpPath);

                    string serverSavePath = Helper.Helper.GetAllFilesPath(region, directoryNames);
                    if (!Directory.Exists(serverSavePath))
                    {
                        Directory.CreateDirectory(serverSavePath);
                    }

                    string csvFilePath = Path.Combine(serverSavePath, formattedName);
                    System.IO.File.WriteAllBytes(csvFilePath, fileData);

                    long fileSizeInBytes = fileData.Length;
                    double fileSizeKB = fileSizeInBytes / 1024.0;

                    object[] parameters = new object[20];
                    parameters[0] = batch_value;
                    parameters[1] = ftpSetting["sftpFilePath"] + $"/{directoryName}/Outbox/" + formattedName;
                    parameters[2] = true;
                    parameters[3] = DateTime.Now;
                    parameters[4] = Environment.MachineName;
                    parameters[5] = AppUserManager.GetUserId();
                    parameters[6] = DateTime.Now;
                    parameters[7] = true;
                    parameters[8] = string.Empty;
                    parameters[9] = false;
                    parameters[10] = Convert.ToInt32(MediaType.RetTxnReport);
                    parameters[11] = 0;
                    parameters[12] = 0;
                    parameters[13] = 0;
                    parameters[14] = formattedName;
                    parameters[15] = distict_value;
                    parameters[16] = false;
                    parameters[17] = 0;
                    parameters[18] = null;
                    parameters[19] = fileSizeKB;

                    objDalBaseClass.ExecuteProcedure(ref parameters, "SaveMediaQueueDetail");

                    // Serve CSV file
                    return File(fileData, "text/csv", formattedName);
                }
                else
                {
                    return File(new byte[0], "text/csv", "blank.csv");
                }
            }
            catch
            {
                return File(new byte[0], "text/csv", "blank.csv");
            }
        }



        //public ActionResult DownLoadBankDisbursementFileAjax(string batch_value, string distict_value)
        //{
        //  try
        //  {
        //    int batchId = 0;
        //    if (string.IsNullOrEmpty(batch_value))
        //    {
        //      batch_value = "0";
        //    }

        //    batchId = Convert.ToInt32(batch_value);

        //    object[] parameters1 = new object[2];
        //    parameters1[0] = batch_value;
        //    parameters1[1] = distict_value;

        //    DataSet ds = new DataSet();
        //    StringBuilder SQL = new StringBuilder();
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        //    object objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //    string formattedName = "";

        //    SQL.Append("SELECT FTPFileName FROM Media_Queue WHERE ");
        //    if (parameters1[0] != null)
        //      SQL.Append("RecordId=" + parameters1[0].ToString());

        //    if (parameters1[1] != null)
        //      SQL.Append(" AND Districts='" + parameters1[1].ToString() + "'");

        //    SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), objDalBaseClass.ConnectionString);
        //    da.Fill(ds);
        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //      formattedName = ds.Tables[0].Rows[0][0].ToString();
        //    }

        //    int UserId = AppUserManager.GetUserId();
        //    Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();

        //    var region = GetRegionName();
        //    var directoryName = region == "KASHMIR REGION" ? "K_BankMediaFile" : "J_BankMediaFile";
        //    string filePath = Helper.Helper.AddExcelSheetIntoDatabasePath(region, directoryName, formattedName);
        //    WebClient ftpClient = new WebClient();
        //    ftpClient.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);
        //    string path = Helper.Helper.AddExcelSheetIntoDatabasePath1(region, directoryName, formattedName);
        //    bool isExist = Helper.Helper.FileCheckInFTP(path, 3, 30);

        //    if (isExist)
        //    {
        //      byte[] fileData = ftpClient.DownloadData(path);
        //      var regions = GetRegionName();
        //      var directoryNames = regions == "KASHMIR REGION" ? "K_Disbursement" : "J_Disbursement";
        //      string serverMapPath = Helper.Helper.GetAllFilesPath(region, directoryNames);
        //      if (!Directory.Exists(serverMapPath))
        //      {
        //        Directory.CreateDirectory(serverMapPath);
        //      }
        //      string csvFilePath = Path.Combine(serverMapPath, formattedName);

        //      System.IO.File.WriteAllBytes(csvFilePath, fileData);

        //      string excelFilePath = csvFilePath.Replace(".csv", ".xlsx");

        //      FileHelper fileHelper = new FileHelper();
        //      bool isFileDeleted = fileHelper.TryDeleteFile(excelFilePath);

        //      Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
        //      Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(csvFilePath);
        //      wb.SaveAs(excelFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
        //      wb.Close(false);
        //      app.Quit();

        //      bool isCsvFileDeleted = fileHelper.TryDeleteFile(csvFilePath);

        //      var regionss = GetRegionName();
        //      var directoryNamess = regionss == "KASHMIR REGION" ? "K_Disbursement.xlsx" : "J_Disbursement.xlsx";
        //      //string uploadformattedName = $"{distict_value}_{batch_value}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}{directoryNamess}";


        //      FileInfo fileInfo = new FileInfo(excelFilePath);

        //      // download file
        //      var uploadformattedName = Path.GetFileName(excelFilePath);
        //      byte[] fileBytes = System.IO.File.ReadAllBytes(fileInfo.FullName);
        //      FileContentResult content = File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, uploadformattedName);

        //      long fileSizeInBytes = fileInfo.Length;
        //      double filesize = fileSizeInBytes / 1024.0;

        //      object[] parameters = new object[20];
        //      parameters[0] = batch_value; // RecordId 
        //      parameters[1] = ftpSetting["sftpFilePath"] + $"/{directoryName}/Outbox/" + uploadformattedName; // FilePath
        //      parameters[2] = true; // IsUploaded
        //      parameters[3] = DateTime.Now; // UploadedDate
        //      parameters[4] = Environment.MachineName; // CreatedMachineInfo
        //      parameters[5] = AppUserManager.GetUserId(); // CreatedBy
        //      parameters[6] = DateTime.Now; // CreatedOn
        //      parameters[7] = true; // IsActive
        //      parameters[8] = string.Empty; // UploadErrors
        //      parameters[9] = false; // IsReUploaded
        //      parameters[10] = Convert.ToInt32(MediaType.RetTxnReport); // MediaType
        //      parameters[11] = 0;
        //      parameters[12] = 0;
        //      parameters[13] = 0;
        //      parameters[14] = formattedName; // FilePath;
        //      parameters[15] = distict_value;

        //      parameters[16] = false;   // IsReUploadedPermitted
        //      parameters[17] = 0;  // ReUploadedPermittedBy
        //      parameters[18] = null;  // ReUploadedPermittedDate
        //      parameters[19] = filesize;  // File Size

        //      objDalBaseClass.ExecuteProcedure(ref parameters, "SaveMediaQueueDetail");

        //      return content;
        //    }
        //    else
        //    {
        //      return File(new byte[0], System.Net.Mime.MediaTypeNames.Application.Octet, "blank.xlsx");
        //    }
        //  }
        //  catch
        //  {
        //    return File(new byte[0], System.Net.Mime.MediaTypeNames.Application.Octet, "blank.xlsx");
        //  }
        //}

        public FileContentResult ConvertCsvBytesToExcel(byte[] csvFileBytes, string csvFileName, string exelFileName)
        {
            // Temporary folder paths
            string tempFolderPath = Server.MapPath("~/TempFiles/");
            Directory.CreateDirectory(tempFolderPath);

            // Define paths for temporary CSV and Excel files
            string csvFilePath = Path.Combine(tempFolderPath, csvFileName ?? "temp.csv");
            string excelFilePath = Path.Combine(tempFolderPath, Path.GetFileNameWithoutExtension(csvFilePath) + ".xlsx");

            try
            {
                // Save the byte array to a temporary CSV file
                System.IO.File.WriteAllBytes(csvFilePath, csvFileBytes);

                // Start Excel application
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                // Open the CSV file
                Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Open(csvFilePath);

                // Save as Excel file (.xlsx)
                workbook.SaveAs(excelFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);

                // Close the workbook and quit the Excel application
                workbook.Close(false);
                excelApp.Quit();

                // Release COM objects
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                // Read the Excel file into a byte array
                byte[] excelFileBytes = System.IO.File.ReadAllBytes(excelFilePath);

                // Clean up temporary files
                System.IO.File.Delete(csvFilePath);
                System.IO.File.Delete(excelFilePath);

                // Return the Excel file as a download
                return File(excelFileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, exelFileName);
            }
            catch
            {
                // Log or handle exceptions
                return File(new byte[0], "");
            }
        }

        static System.Data.DataTable ConvertCsvToDataTable(string fileName)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            try
            {
                byte[] fileData = System.IO.File.ReadAllBytes(fileName);

                using (Stream memoryStream = new MemoryStream(fileData))
                {
                    using (StreamReader sr = new StreamReader(memoryStream))
                    {
                        string[] headers = sr.ReadLine().Split(',');
                        if (headers.Any())
                        {
                            foreach (string header in headers)
                            {
                                dataTable.Columns.Add(header);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < headers.Length; i++)
                            {
                                dataTable.Columns.Add($"Column{i + 1}");
                            }
                            sr.BaseStream.Position = 0;
                        }

                        while (!sr.EndOfStream)
                        {
                            string[] rows = sr.ReadLine().Split(',');
                            DataRow dataRow = dataTable.NewRow();
                            for (int i = 0; i < headers.Length; i++)
                            {
                                dataRow[i] = rows[i];
                            }
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dataTable;

        }

        private bool ValidateForm(GeneratePensionProcessModel generatePensionProcessModel)
        {
            try
            {

                if (generatePensionProcessModel.DepositDate == null)
                {
                    generatePensionProcessModel.DepositDate = DVOApplicationUserInfo.DateConvertion(DateTime.Now);
                    //TempData["error"] = "Please select Date of deposit to bank.";
                    //return false;
                }
                if (string.IsNullOrWhiteSpace(generatePensionProcessModel.BanckCode))
                {
                    string BanckCode = string.Empty;
                    DVOUpdateBankCode obj = new DVOUpdateBankCode();
                    List<DVOUpdateBankCode> ObjBankCodeDetails = BLLUpdateBankCode.GetBankCodeDetails(ref obj);
                    foreach (var item in ObjBankCodeDetails)
                    {
                        BanckCode = BanckCode + "\'" + item.bank_code.Trim() + "\'" + ",";
                    }
                    generatePensionProcessModel.BanckCode = BanckCode.TrimEnd(',');
                    //TempData["error"] = "Please select Bank-Code.";
                    //return false;
                }
                if (string.IsNullOrWhiteSpace(generatePensionProcessModel.EmpType))
                {
                    string EmpTypes = string.Empty;
                    DVOMasterEmpTypes objDVOMasterEmpTypes = new DVOMasterEmpTypes();
                    List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes);
                    foreach (var item in listDVOMasterEmpTypes)
                    {
                        EmpTypes = EmpTypes + "\'" + item.type_code.Trim() + "\'" + ",";
                    }
                    generatePensionProcessModel.EmpType = EmpTypes.TrimEnd(',');
                    //TempData["error"] = "Please select Scheme Name";
                    //return false;
                }
                //else if (form["file"].Trim() == string.Empty || form["file"].Trim() == "*.txt")
                //{
                //  //SatyaPayMain.CommonUtilities.Utilities.ShowMessage("Please select file-path to save media.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //  return false;
                //}
                else
                    return true;
            }
            catch (Exception ex)
            {
                //ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return true;
        }

        private string GetSelectedBankCodes(out System.Collections.ArrayList BankCodes, string code)
        {
            string _bankCodes = string.Empty;
            BankCodes = new System.Collections.ArrayList();
            foreach (var _bcode in GetBanckCode(code))
            {
                BankCodes.Add(_bcode.bank_code);
                //_bankCodes += "'" + _bcode.bank_code + "',";
                _bankCodes = _bankCodes + "\'" + _bcode.bank_code.Trim() + "\'" + ",";
            }
            if (_bankCodes.LastIndexOf(",") == _bankCodes.Length - 1)
                _bankCodes = _bankCodes.Substring(0, _bankCodes.Length - 1);
            return _bankCodes;
        }

        private string GetSelectedScheme(out System.Collections.ArrayList Scheme, string code)
        {
            string _Scheme = string.Empty;
            Scheme = new System.Collections.ArrayList();
            foreach (var _scheme in GetBanckCode(code))
            {
                Scheme.Add(_scheme.bank_code);
                //_bankCodes += "'" + _bcode.bank_code + "',";
                _Scheme = _Scheme + "\'" + _scheme.bank_code.Trim() + "\'" + ",";
            }
            if (_Scheme.LastIndexOf(",") == _Scheme.Length - 1)
                _Scheme = _Scheme.Substring(0, _Scheme.Length - 1);
            return _Scheme;
        }

        private string FilePath(string str)
        {
            string temp = "";
            string aString = str;
            int lastPos = aString.LastIndexOf('\\');
            var t1 = str.Substring(lastPos);
            int lastPosButOne = aString.LastIndexOf('\\', lastPos - 1);
            var t2 = str.Substring(lastPosButOne);
            temp = t2.Replace(t1, "").Replace("\\", "");
            return temp;
        }

        //public JsonResult GetAllFilesAjax()
        //{
        //  String[] allfiles = System.IO.Directory.GetFiles(Server.MapPath("~/BankMediaFile"), "*.*", System.IO.SearchOption.AllDirectories);
        //  var allfilesList = from x in allfiles select new { FileName = x.Replace(x.Substring(0, x.LastIndexOf('\\') + 1), ""), Bank = x.Replace(x.Substring(0, x.LastIndexOf('\\') + 1), "").Replace(".xlsx", "").Split('_')[0], Dated = FilePath(x) };
        //  ViewBag.allfiles = allfilesList;

        //  return Json(allfilesList, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetAllFilesAjax()
        {


            //var region = Session["RegionName"].ToString();
            var region = GetRegionName();
            var directoryName = region == "KASHMIR REGION" ? "K_Disbursement" : "J_Disbursement";
            //var directoryPath = Server.MapPath($"~/BankMediaFile/{directoryName}");
            string directoryPath = Helper.Helper.GetAllFilesPath(region, directoryName);


            // Old Code With Working 
            //var directoryPath = Server.MapPath("~/BankMediaFile");

            if (System.IO.Directory.Exists(directoryPath))
            {
                string[] allfiles = System.IO.Directory.GetFiles(directoryPath, "*.*", System.IO.SearchOption.AllDirectories);

                var allfilesList = allfiles.Select(x => new
                {
                    FileName = System.IO.Path.GetFileName(x),
                    Bank = System.IO.Path.GetFileNameWithoutExtension(x).Split('_')[1],
                    Dated = DateTime.Parse(System.IO.File.GetLastWriteTime(x).ToString()).ToString("MM_dd_yyyy"),
                    folderName = x.Split('\\')[x.Split('\\').Count() - 2]
                });

                ViewBag.allfiles = allfilesList.ToList();

                return Json(allfilesList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.allfiles = null;

                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GenerateBankMedia()
        {
            generatePensionProcess = new GeneratePensionProcessModel();
            try
            {
                ViewBag.IsGeneratePensionWithoutSftp = IsGeneratePensionWithoutSftp;
                PensionProcessViewModel Paysearch = ShowActiveBatch();
                string disData = string.Empty;
                BindCmbBanckCode(string.Empty);
                BindComboEmployeeType(Paysearch.EmpType);

                generatePensionProcess.GenerateCheques = "Y";
                generatePensionProcess.EmployeeType = Paysearch.EmpType;
                if (Paysearch.RegionNames != "" && Paysearch.RegionNames != null)
                {
                    disData = Paysearch.RegionNames.ToLower().Trim();
                }

                ViewBag.pybatchid = Paysearch.pybatchid;
                ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);

                int userid = AppUserManager.GetUserId();
                var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
                var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionProcess" && x.ActionName == "GenerateBankMedia")
                             join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                             from p in ps.DefaultIfEmpty()
                             select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Forward Disbursement File", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

                if (model != null)
                {
                    ViewBag.AddPermission = model.AddPermssion;
                    ViewBag.EditPermission = model.EditPermission;
                }

                Dictionary<string, string> PensionDetailByMonthYear = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear();
                ViewBag.CurrentMonthBatch = PensionDetailByMonthYear;
                if (PensionDetailByMonthYear == null)
                {
                    ViewBag.pybatchid = 0;
                }
                else
                {
                    ViewBag.pybatchid = PensionDetailByMonthYear["pybatchid"];
                }
            }
            catch (Exception ex)
            {
                ToastError(ex.Message);
                ViewBag.BanckCode = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
                ViewBag.EmployeeType = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
            }

            return PartialView("~/Views/PensionProcess/DirectDeposits/GenerateBankMedia.cshtml", generatePensionProcess);
        }

        public ActionResult UploadDisbursement()
        {
            generatePensionProcess = new GeneratePensionProcessModel();
            try
            {
                ViewBag.IsGeneratePensionWithoutSftp = IsGeneratePensionWithoutSftp;
                PensionProcessViewModel Paysearch = ShowActiveBatch();
                string disData = string.Empty;
                BindCmbBanckCode(string.Empty);
                BindComboEmployeeType(Paysearch.EmpType);

                generatePensionProcess.GenerateCheques = "Y";
                generatePensionProcess.EmployeeType = Paysearch.EmpType;
                if (Paysearch.RegionNames != "" && Paysearch.RegionNames != null)
                {
                    disData = Paysearch.RegionNames.ToLower().Trim();
                }

                ViewBag.pybatchid = Paysearch.pybatchid;
                ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);

                int userid = AppUserManager.GetUserId();
                var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
                var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionProcess" && x.ActionName == "GenerateBankMedia")
                             join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                             from p in ps.DefaultIfEmpty()
                             select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Forward Disbursement File", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

                if (model != null)
                {
                    ViewBag.AddPermission = model.AddPermssion;
                    ViewBag.EditPermission = model.EditPermission;
                }

                Dictionary<string, string> PensionDetailByMonthYear = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear();
                ViewBag.CurrentMonthBatch = PensionDetailByMonthYear;
                if (PensionDetailByMonthYear == null)
                {
                    ViewBag.pybatchid = 0;
                }
                else
                {
                    ViewBag.pybatchid = PensionDetailByMonthYear["pybatchid"];
                }
            }
            catch (Exception ex)
            {
                ToastError(ex.Message);
                ViewBag.BanckCode = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
                ViewBag.EmployeeType = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
            }

            return PartialView("~/Views/PensionProcess/DirectDeposits/UploadDisbursement.cshtml", generatePensionProcess);
        }

        public FileResult DownloadAjax(string date, string file)
        {
            string path = string.Empty;
            if (!string.IsNullOrWhiteSpace(date) && !string.IsNullOrWhiteSpace(file))
            {
                path = Path.Combine(Server.MapPath("~/BankMediaFile"), date, file);
                return File(path, "application/octet-stream", file);
            }
            else return File(path, "application/octet-stream", file);
        }

        public ActionResult UpdateDisbursementResponseAjax()
        {
            try
            {


                //var region = Session["RegionName"].ToString();
                //var directoryName = region == "KASHMIR REGION" ? "K_BankMediaFile" : "J_BankMediaFile";

                Helper.BankDisbursementUtility bankDisbursementUtility = new Helper.BankDisbursementUtility();
                bankDisbursementUtility.UpdateDisbursementResponse();
                //responseok = true;

                //Helper.BankValidationUtility bankValidationUtility = new Helper.BankValidationUtility();
                //bankValidationUtility.UpdateValidationResponse();
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json("File Uploaded", JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> UpdateDisbursmentfiledataAjax(string batch_value, string distict_value, HttpPostedFileBase file)
        {
            string Response = string.Empty;
            try
            {
                Helper.BankDisbursementWithFileUtility bankDisbursementUtility = new Helper.BankDisbursementWithFileUtility();
                //string tempFolderPath = Server.MapPath("~/TempFiles/");
                string tempFolderPath = HostingEnvironment.MapPath("~/TempFiles/");
                Response = await bankDisbursementUtility.UpdateDisbursementWithFileResponse(batch_value, distict_value, file, tempFolderPath);
            }
            catch (Exception ex)
            {
                Response = $"Error: {ex.Message}";
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }


        public ActionResult BankMediaExcelUploadAjax()
        {
            string formattedName = string.Empty;
            string fileName = string.Empty;
            string csvFileName = string.Empty;
            try
            {
                //  Get all files from Request object  
                HttpPostedFileBase file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName);

                    // Format the date and time as a string (e.g., "yyyyMMdd_HHmmss")
                    formattedName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + Path.GetExtension(file.FileName);

                    // Save data on local machine
                    // Specify the file path where you want to save the uploaded file



                    string serverMapPath = Server.MapPath("~/DataFile/BankMediaFileUpload");
                    if (!Directory.Exists(serverMapPath))
                    {
                        // Attempt to create the directory
                        Directory.CreateDirectory(serverMapPath);
                    }
                    // Add file name with directory
                    string filePath = Path.Combine(serverMapPath, formattedName);

                    // Save the file to the server
                    file.SaveAs(filePath);

                    string csvFilePath = filePath.Replace(".xlsx", ".csv").Replace(".xls", ".csv");


                    if (Path.GetExtension(filePath) == ".xls" || Path.GetExtension(filePath) == ".xlsx")
                    {
                        formattedName = Path.GetFileName(csvFilePath);
                        SaveExcelAsCsv(filePath, csvFilePath);
                    }
                    else
                    {
                        formattedName = Path.GetFileName(filePath);
                    }


                    byte[] bytes = System.IO.File.ReadAllBytes(csvFilePath);
                    string failedMessage = UploadDataFile(bytes, formattedName);

                    if (!string.IsNullOrEmpty(failedMessage))
                    {
                        return Json(failedMessage, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(formattedName, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Json(csvFileName, JsonRequestBehavior.AllowGet);
        }

        static void SaveExcelAsCsv(string excelFilePath, string csvFilePath)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(excelFilePath);
            wb.SaveAs(csvFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlCSVWindows);
            wb.Close(false);
            app.Quit();

        }
        private string UploadDataFile(byte[] fileContents, string fileName)
        {
            string failedMessage = string.Empty;
            try
            {
                Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
                // Get the file name

                //var region = Session["RegionName"].ToString();

                var region = GetRegionName();
                var directoryName = region == "KASHMIR REGION" ? "K_BankMediaFileUpload" : "J_BankMediaFileUpload";
                string ftpServerUrl = ftpSetting["ftpServerUrl"] + $"/DataFiles/{directoryName}/" + fileName;

                //string ftpServerUrl = ftpSetting["ftpServerUrl"] + "/DataFiles/BankMediaFileUpload/" + fileName;

                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpServerUrl);
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.Timeout = 600000;
                ftpRequest.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);

                using (Stream requestStream = ftpRequest.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }

                FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpResponse.Close();
            }
            catch (Exception ex)
            {
                failedMessage = ex.Message;
                throw;
            }
            return failedMessage;
        }

        public ActionResult AddBankMediaIntoDatabaseAjax()
        {
            string msg = "0";
            try

            {
                PensionProcessViewModel Paysearch = ShowActiveBatch();
                int UserId = AppUserManager.GetUserId();
                string fileName = Request.Form["formattedName"];

                if (fileName.Contains(".csv") || fileName.Contains(".xlsx") || fileName.Contains(".xls"))
                {
                    Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
                    // Get the file name

                    // Define the path for sql

                    //var region = Session["RegionName"].ToString();
                    var region = GetRegionName();
                    var directoryName = region == "KASHMIR REGION" ? "K_BankMediaFileUpload" : "J_BankMediaFileUpload";
                    string filePath = ftpSetting["localFilePath"] + $"\\DataFiles\\{directoryName}\\" + fileName;

                    //string filePath = ftpSetting["localFilePath"] + "\\DataFiles\\BankMediaFileUpload\\" + fileName;

                    // Define the parameters if needed (e.g., for input parameters)
                    var parameter1 = new SqlParameter("@filePathWithName", filePath);
                    var parameter2 = new SqlParameter("@UserId", UserId);
                    // Execute the stored procedure
                    int result = db.Database.ExecuteSqlCommand("EXEC UpdateBankMediaExcelDatabase @filePathWithName , @UserId ", parameter1, parameter2);
                    msg = "1";
                    // 20/11/2023 Post The Status after upload file
                    UpdatePostedStatus(null);
                    // 20/11/2023 Close the batch if response file uploaded
                    PensionProcessViewModel pensionProcessHeader = new PensionProcessViewModel();
                    pensionProcessHeader.pybatchid = Paysearch.pybatchid;
                    CloseBatchProcessAjax(pensionProcessHeader);
                }
                else
                {
                    msg = "0";
                }
            }
            catch (Exception ex)
            {
                msg = "0";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        // Depricated function
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateBankMedia([Bind(Include = "DepositDate,BanckCode,EmpType,GenerateCheques,RegionNames")] GeneratePensionProcessModel generatePensionProcessModel, FormCollection form)
        {
            try
            {
                ViewBag.IsGeneratePensionWithoutSftp = IsGeneratePensionWithoutSftp;
                BindCmbBanckCode(generatePensionProcessModel.BanckCode);
                string disData = string.Empty;
                PensionProcessViewModel Paysearch = ShowActiveBatch();
                BindComboEmployeeType(generatePensionProcessModel.EmpType);
                var RegionNames = generatePensionProcessModel.RegionNames == null ? Paysearch.RegionNames : generatePensionProcessModel.RegionNames;
                if (RegionNames != "" && RegionNames != null)
                {
                    disData = RegionNames.ToLower().Trim();
                }
                ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);

                int apBatchID = -1;
                if (generatePensionProcessModel.GenerateCheques.Trim() == "Y")
                {
                    apBatchID = -1;
                    bool IsMasterBatch = false;
                    string _APCurrentUser = string.Empty;
                    BLLBatchMaintenance.GetActiveBatchForReports(DVOApplicationUserInfo.LoginId, BatchTypes.APCashDeposit, out _APCurrentUser, out apBatchID, out IsMasterBatch);
                }
                if (ValidateForm(generatePensionProcessModel))
                {

                    bool sameMediaFormat = true;
                    DVOMasterBankDetails objDVOMasterBankDetails = new DVOMasterBankDetails();
                    List<DVOMasterBankDetails> listDVOMasterBankDetails = new List<DVOMasterBankDetails>();
                    System.Collections.ArrayList _arrBankCodes = new System.Collections.ArrayList();
                    string _selectedBankCodes = GetSelectedBankCodes(out _arrBankCodes, generatePensionProcessModel.BanckCode); //form["BanckCode"].Trim();
                                                                                                                                //if (_selectedBankCodes.Trim().Length > 0 && _arrBankCodes.Count > 0)
                    listDVOMasterBankDetails = BLLPRBankMediaInybankd.GetData(ref objDVOMasterBankDetails, true, _selectedBankCodes);
                    if (listDVOMasterBankDetails.Count > 1)
                    {
                        if (listDVOMasterBankDetails.Count == _arrBankCodes.Count)
                        {
                            for (int i = 0; i < listDVOMasterBankDetails.Count - 1; i++)
                            {
                                if (listDVOMasterBankDetails[i].media_str.Trim() != listDVOMasterBankDetails[i + 1].media_str.Trim())
                                {
                                    sameMediaFormat = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            sameMediaFormat = false;
                        }
                    }
                    else
                    {
                        sameMediaFormat = true;
                    }
                    if (!sameMediaFormat)
                    {
                        //SatyaPayMain.CommonUtilities.Utilities.ShowMessage("All selected Banks have not same Media-Format or some selected Bank has not Media-Format.\nSo you can't create Media for these Banks.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TempData["error"] = "All selected Banks have not same Media-Format or some selected Bank has not Media-Format. So you can't create Media for these Banks.";
                    }
                    else
                    {
                        string fileExtension = string.Empty;

                        // Format the date and time as a string (e.g., "yyyyMMdd_HHmmss")
                        string formattedName = Paysearch.pybatchid + "_JK_Disbursement.csv";

                        //ViewBag.FileName = generatePensionProcessModel.BanckCode + ".txt";
                        ViewBag.FileName = formattedName;
                        int TotalRowsCount = 0;
                        //Get data from database and bind with report                 
                        DVOddmStypddreAndStypddrd objDVOddmStypddreAndStypddrd = new DVOddmStypddreAndStypddrd();

                        foreach (string strBankCode in _arrBankCodes)
                        {
                            objDVOddmStypddreAndStypddrd.BanckCode = objDVOddmStypddreAndStypddrd.BanckCode + strBankCode + "^";
                        }
                        objDVOddmStypddreAndStypddrd.GenCheck = generatePensionProcessModel.GenerateCheques;
                        objDVOddmStypddreAndStypddrd.Date = generatePensionProcessModel.DepositDate != null ? Convert.ToDateTime(generatePensionProcessModel.DepositDate) : Convert.ToDateTime("01/01/1900");
                        //objDVOddmStypddreAndStypddrd.bank_code = generatePensionProcessModel.BanckCode;
                        objDVOddmStypddreAndStypddrd.District = disData;
                        DataTable objDataTable;

                        #region Create excel file with 4 empty column
                        int UserId = AppUserManager.GetUserId();
                        Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
                        // Define the path for sql create SVC

                        //var region = Session["RegionName"].ToString();
                        var region = GetRegionName();
                        var directoryName = region == "KASHMIR REGION" ? "K_BankMediaFile" : "J_BankMediaFile";
                        string filePath = ftpSetting["localFilePath"] + $"\\DataFiles\\{directoryName}\\" + formattedName;

                        //string filePath = ftpSetting["localFilePath"] + "\\DataFiles\\BankMediaFile\\" + formattedName;

                        //List<MasterEmpBankDetails> bankIFSC = db.MasterEmpBankDetails.Where(x => x.ACCOUNT_STATUS == "ACTIVE").Distinct().ToList(); //.Select(x => new MasterEmpBankDetails { APPLICANT_BANK_IFSC_CODE = x.APPLICANT_BANK_IFSC_CODE, BankName = x.BankName }).Distinct().ToList();

                        //List<string> ifsc = bankIFSC.Select(x => x.APPLICANT_BANK_IFSC_CODE).Distinct().ToList();


                        //foreach (var item in ifsc)
                        //{
                        //string bankName = bankIFSC.Where(x => x.APPLICANT_BANK_IFSC_CODE == item).Select(x => x.BankName).FirstOrDefault();

                        // Define the parameters if needed (e.g., for input parameters)
                        SqlParameter[] parameter = {
                    //new SqlParameter("@DepositDate", generatePensionProcessModel.DepositDate.Value.ToShortDateString()),
                    //new SqlParameter("@BankCode", generatePensionProcessModel.BanckCode),
                    //new SqlParameter("@District", generatePensionProcessModel.DirectDepositsCheques),
                    //new SqlParameter("@EmployeeType", generatePensionProcessModel.EmpType),
                    new SqlParameter("@filePathWithName", filePath),
                    new SqlParameter("@UserId", UserId),
                    //new SqlParameter("@IFSC", item),
                    new SqlParameter("@district", disData),
                    };
                        // Execute the stored procedure
                        var result = db.Database.SqlQuery<string>("EXEC GenerateBankMediaFile @filePathWithName, @UserId, @district", parameter).ToList();
                        if (result.Any())
                        {
                            if (result[0] != "There is no record to create media.")
                            {
                                // Create an FTP client
                                WebClient ftpClient = new WebClient();
                                ftpClient.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);

                                //var regions = Session["RegionName"].ToString();
                                var regions = GetRegionName();
                                var directoryNames = regions == "KASHMIR REGION" ? "K_BankMediaFile" : "J_BankMediaFile";
                                string path = ftpSetting["ftpServerUrl"] + $"/DataFiles/{directoryNames}/" + formattedName;

                                //string path = ftpSetting["ftpServerUrl"] + ("/DataFiles/BankMediaFile/" + formattedName);

                                // File path, attampt, dealy in attampt
                                bool isExist = Helper.Helper.FileCheckInFTP(path, 3, 30);

                                if (isExist)
                                {
                                    // Download the file from the FTP server
                                    byte[] fileData = ftpClient.DownloadData(path);

                                    // Specify the file path where you want to save the uploaded file

                                    //DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/BankMediaFile"));
                                    //DirectoryInfo dis = di.GetDirectories(DateTime.Now.ToShortDateString().Replace("/", "_")).Any() ? di.GetDirectories(DateTime.Now.ToShortDateString().Replace("/", "_")).FirstOrDefault() : di.CreateSubdirectory(DateTime.Now.ToShortDateString().Replace("/", "_"));
                                    //string path = Path.Combine(dis.FullName, generatePensionProcessModel.BanckCode.Trim() + ".txt");

                                    // New  Code 
                                    //var sregions = Session["RegionName"].ToString();
                                    var sregions = GetRegionName();
                                    var directorysNames = sregions == "KASHMIR REGION" ? "K_Disbursement" : "J_Disbursement";
                                    var serverMapPath = Server.MapPath($"~/BankMediaFile/{directorysNames}");

                                    // Old  Code Working 
                                    //string serverMapPath = Server.MapPath("~/BankMediaFile/JK_Disbursement");
                                    if (!Directory.Exists(serverMapPath))
                                    {
                                        // Attempt to create the directory
                                        Directory.CreateDirectory(serverMapPath);
                                    }
                                    // DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/BankMediaFile"));
                                    //DirectoryInfo dis = di.GetDirectories(DateTime.Now.ToShortDateString().Replace("/", "_")).Any() ? di.GetDirectories(DateTime.Now.ToShortDateString().Replace("/", "_")).FirstOrDefault() : di.CreateSubdirectory(DateTime.Now.ToShortDateString().Replace("/", "_"));
                                    // Add file name with directory
                                    string csvFilePath = Path.Combine(serverMapPath, formattedName);

                                    // Save the file to the server
                                    System.IO.File.WriteAllBytes(csvFilePath, fileData);

                                    string excelFilePath = csvFilePath.Replace(".csv", ".xlsx");

                                    // delete file in safe way
                                    FileHelper fileHelper = new FileHelper();
                                    bool isFileDeleted = fileHelper.TryDeleteFile(excelFilePath);

                                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                                    Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(csvFilePath);
                                    wb.SaveAs(excelFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
                                    wb.Close(false);
                                    app.Quit();

                                    // delete file in safe way
                                    bool isCsvFileDeleted = fileHelper.TryDeleteFile(csvFilePath);

                                    var regsions = GetRegionName();
                                    var directorysName = regsions == "KASHMIR REGION" ? "K_Disbursement" : "J_Disbursement";


                                    string uploadformattedName = Paysearch.pybatchid + $"{directorysName}" + (Path.GetExtension(excelFilePath));

                                    UploadDisbursementFile(excelFilePath, uploadformattedName);

                                    FileInfo fileInfo = new FileInfo(excelFilePath);
                                    // Get the size of the file in bytes
                                    long fileSizeInBytes = fileInfo.Length;
                                    double filesize = fileSizeInBytes / 1024.0;

                                    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                                    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                                    object[] parameters = new object[15];
                                    parameters[0] = Paysearch.pybatchid; //1 RecordId 
                                    parameters[1] = ftpSetting["sftpFilePath"] + $"/{directoryName}/Outbox/" + uploadformattedName; //2 FilePath
                                    parameters[2] = true; //3 IsUploaded
                                    parameters[3] = DateTime.Now; //4 UploadedDate
                                    parameters[4] = Environment.MachineName; //5 CreatedMachineInfo
                                    parameters[5] = AppUserManager.GetUserId(); //6 CreatedBy
                                    parameters[6] = DateTime.Now; //7 CreatedOn
                                    parameters[7] = true; //8 IsActive
                                    parameters[8] = string.Empty; //9 UploadErrors
                                    parameters[9] = false; //10 IsReUploaded
                                    parameters[10] = Convert.ToInt32(MediaType.TxnReport); //11 MediaType

                                    // Reupload Value  *** Code By Himanshu Rajput ***

                                    parameters[16] = false;   //12 IsReUploadedPermitted
                                    parameters[17] = 0;  //13 ReUploadedPermittedBy
                                    parameters[18] = null;  //14 ReUploadedPermittedDate
                                    parameters[19] = filesize;  //15 File Size

                                    // Execute the stored procedure
                                    objDalBaseClass.ExecuteProcedure(ref parameters, "SaveMediaQueueDetail");
                                }
                            }
                            //}
                        }

                        #endregion
                        if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.GenerateCheques))
                        {
                            if (generatePensionProcessModel.GenerateCheques.Trim() == "Y")
                            {
                                objDataTable = BLLDirectDepositMedia.GetDirectDepositeData(ref objDVOddmStypddreAndStypddrd, true, apBatchID);
                            }
                            else
                            {
                                objDataTable = BLLDirectDepositMedia.GetDirectDepositeData(ref objDVOddmStypddreAndStypddrd, false, apBatchID);
                            }
                        }
                        else
                        {
                            objDataTable = BLLDirectDepositMedia.GetDirectDepositeData(ref objDVOddmStypddreAndStypddrd, false, apBatchID);
                        }
                        string strBankCodeN = _arrBankCodes[0].ToString().Trim();
                        if (objDataTable != null)
                            if (objDataTable.Rows.Count > 0)
                            {
                                TotalRowsCount += objDataTable.Rows.Count;
                                string mediaFormat = string.Empty;
                                objDVOMasterBankDetails = listDVOMasterBankDetails.Find(delegate (DVOMasterBankDetails objParm)
                                {
                                    return objParm.bank_code.Trim() == strBankCodeN.Trim();
                                });

                            }
                        if (TotalRowsCount <= 0)
                        {
                            //SatyaPayMain.CommonUtilities.Utilities.ShowMessage("There is no record to create media.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TempData["error"] = "There is no record to create media.";
                        }
                        else
                            TempData["success"] = "File has been successfully saved.";
                        //SatyaPayMain.CommonUtilities.Utilities.ShowMessage("File has been successfully saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtFilePath.Clear();
                        //sw.Close();

                        return PartialView("~/Views/PensionProcess/DirectDeposits/GenerateBankMedia.cshtml", generatePensionProcessModel);
                    }

                }
            }
            catch (Exception ex)
            {
                //ReportViewer.ReportSource = null;
                //SatyaPay.StyleUtility.Reports_Splasher.Close();
                //ExceptionManagement.ExceptionManager.Publish(ex);
                //MessageBox.Show(ex.Message, "Satya Pay.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return PartialView("~/Views/PensionProcess/DirectDeposits/GenerateBankMedia.cshtml");
                TempData["error"] = "Please Try Again..." + ex.Message;
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return PartialView("~/Views/PensionProcess/DirectDeposits/GenerateBankMedia.cshtml", generatePensionProcessModel);
        }

        private string UploadDisbursementFile(string excelFilePath, string formattedName)
        {
            string fileReturn = "";
            try
            {
                Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
                // Get the file name

                bool IsFTP = Convert.ToBoolean(ftpSetting["IsFTP"]);
                if (IsFTP)
                {
                    //string formattedName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "JK_Disbursement" + (Path.GetExtension(excelFilePath));

                    byte[] fileContents = System.IO.File.ReadAllBytes(excelFilePath);

                    // Get the size of the file in bytes
                    //long fileSizeInBytes = fileContents.Length;
                    //fileSizeInKB = fileSizeInBytes / 1024.0;

                    //var region = Session["RegionName"].ToString();
                    var region = GetRegionName();
                    var directoryName = region == "KASHMIR REGION" ? "K_Disbursement" : "J_Disbursement";
                    //string ftpServerUrl = ftpSetting["sftpServerUrl"] + $"/DataFiles/{directoryName}/Outbox/" + formattedName;
                    string ftpServerUrl = Helper.Helper.GetUploadDataFile(region, directoryName, formattedName);

                    //string ftpServerUrl1 = ftpSetting["ftpServerUrl"] ++ "/DataFiles/Disbursement/Outbox/" + formattedName;

                    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpServerUrl);
                    ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                    ftpRequest.Timeout = 600000;
                    ftpRequest.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);
                    using (Stream requestStream = ftpRequest.GetRequestStream())
                    {
                        requestStream.Write(fileContents, 0, fileContents.Length);

                    }
                    FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                    ftpResponse.Close();
                    fileReturn = ftpServerUrl;
                }
                else
                {
                   
                        string host = ftpSetting["sftpServerUrl"];
                        int port = Convert.ToInt32(ftpSetting["sftpPort"]); //SFTP default port is 22
                        string username = ftpSetting["sftpUsername"];
                        string password = ftpSetting["sftpPassword"];
                        string localFilePath = excelFilePath;
                        string remoteDirectory = ftpSetting["sftpFilePath"] + "/PaymentFiles/Outbox";
                        string localfilepathSFTP = Server.MapPath("~/"+ ftpSetting["sftpPrivateKeyPath"]);

                        var nn = Path.Combine(remoteDirectory, formattedName);
                            //var keyFile = new PrivateKeyFile(ftpSetting["sftpPrivateKeyPath"]);
                            var keyFile = new PrivateKeyFile(localfilepathSFTP);
                            var keyFiles = new[] { keyFile };
                            var methods = new List<AuthenticationMethod>
                        {
                            new PasswordAuthenticationMethod(username, password),
                            new PrivateKeyAuthenticationMethod(username, keyFiles)
                        };

                        // Create a new connection info with public key authentication
                        ConnectionInfo connectionInfo = new ConnectionInfo(host, port, username, methods.ToArray());
                        //new PrivateKeyAuthenticationMethod(username, privateKeyFile));


                        // Create an SftpClient using the connection info
                        using (SftpClient sftpClient = new SftpClient(connectionInfo))
                        {
                            // Connect to the SFTP server
                            sftpClient.Connect();

                            // Ensure the remote directory exists
                            if (!sftpClient.Exists(remoteDirectory))
                            {
                                sftpClient.CreateDirectory(remoteDirectory);
                            }

                        //using (var fileStream = new FileStream(localFilePath, FileMode.Open))
                        //{
                        //    // Upload the file
                        //    sftpClient.UploadFile(fileStream, Path.Combine(remoteDirectory, formattedName));
                        //}

                        var ftpRequest = (FtpWebRequest)WebRequest.Create(localFilePath);
                        ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                        ftpRequest.Credentials =new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);
                        ftpRequest.UseBinary = true;

                        using (var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                        using (var ftpStream = ftpResponse.GetResponseStream())
                       

                            // Upload directly without saving locally
                            sftpClient.UploadFile(ftpStream, Path.Combine(remoteDirectory, formattedName));

                            sftpClient.Disconnect();
                        

                        // Disconnect from the SFTP server
                        sftpClient.Disconnect();
                        }
                        fileReturn = Path.Combine(remoteDirectory, formattedName);
                    //fileReturn = "File sent!";

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return fileReturn;
            //return fileSizeInKB;
        }
        // Old function to create text file and download
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult GenerateBankMedia([Bind(Include = "DepositDate,BanckCode,EmployeeType,GenerateCheques")] GeneratePensionProcessModel generatePensionProcessModel, FormCollection form)
        //{
        //  try
        //  {
        //    //ModelState["EmployeeType"].Errors.Clear();
        //    //ModelState["DirectDepositsCheques"].Errors.Clear();
        //    //ModelState["PayrollCashAccount"].Errors.Clear();
        //    //ModelState["StartingChequeNo"].Errors.Clear();
        //    //ModelState["DepositDate"].Errors.Clear();
        //    //ModelState["PayDate"].Errors.Clear();
        //    //ModelState["GenerateCheques"].Errors.Clear();
        //    //ModelState["Department"].Errors.Clear();
        //    //ModelState["BanckCode"].Errors.Clear();


        //    BindCmbBanckCode(generatePensionProcessModel.BanckCode);
        //    BindComboEmployeeType(generatePensionProcessModel.EmployeeType);

        //    //Added by Sarvjeet on 29/01/2010, To Get Active Ap Batch, will be used in creating check. 
        //    //********************************************************
        //    int apBatchID = -1;
        //    if (generatePensionProcessModel.GenerateCheques.Trim() == "Y")
        //    {
        //      apBatchID = -1;
        //      bool IsMasterBatch = false;
        //      string _APCurrentUser = string.Empty;
        //      BLLBatchMaintenance.GetActiveBatchForReports(DVOApplicationUserInfo.LoginId, BatchTypes.APCashDeposit, out _APCurrentUser, out apBatchID, out IsMasterBatch);
        //    }
        //    //**********************************************************
        //    if (ValidateForm(generatePensionProcessModel))
        //    {

        //      bool sameMediaFormat = true;
        //      DVOMasterBankDetails objDVOMasterBankDetails = new DVOMasterBankDetails();
        //      List<DVOMasterBankDetails> listDVOMasterBankDetails = new List<DVOMasterBankDetails>();
        //      System.Collections.ArrayList _arrBankCodes = new System.Collections.ArrayList();
        //      string _selectedBankCodes = GetSelectedBankCodes(out _arrBankCodes, generatePensionProcessModel.BanckCode.Trim()); //form["BanckCode"].Trim();
        //      if (_selectedBankCodes.Trim().Length > 0 && _arrBankCodes.Count > 0)
        //        listDVOMasterBankDetails = BLLPRBankMediaInybankd.GetData(ref objDVOMasterBankDetails, true, _selectedBankCodes);
        //      if (listDVOMasterBankDetails.Count > 1)
        //      {
        //        if (listDVOMasterBankDetails.Count == _arrBankCodes.Count)
        //        {
        //          for (int i = 0; i < listDVOMasterBankDetails.Count - 1; i++)
        //          {
        //            if (listDVOMasterBankDetails[i].media_str.Trim() != listDVOMasterBankDetails[i + 1].media_str.Trim())
        //            {
        //              sameMediaFormat = false;
        //              break;
        //            }
        //          }
        //        }
        //        else
        //        {
        //          sameMediaFormat = false;
        //        }
        //      }
        //      else
        //      {
        //        sameMediaFormat = true;
        //      }
        //      if (!sameMediaFormat)
        //      {
        //        //SatyaPayMain.CommonUtilities.Utilities.ShowMessage("All selected Banks have not same Media-Format or some selected Bank has not Media-Format.\nSo you can't create Media for these Banks.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        TempData["error"] = "All selected Banks have not same Media-Format or some selected Bank has not Media-Format. So you can't create Media for these Banks.";
        //      }
        //      else
        //      {
        //        string fileExtension = string.Empty;
        //        //if (form["file"].Trim().Substring(form["file"].Trim().Length - 4) != ".csv")
        //        //  form["file"] = form["file"].Trim() + ".csv";

        //        //string fileName = Path.GetFileName(file.FileName);

        //        // Create a reference to a directory.
        //        DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/BankMediaFile"));

        //        // Create the directory only if it does not already exist. 
        //        if (di.Exists == false)
        //          di.Create();

        //        // Create a subdirectory in the directory just created.
        //        DirectoryInfo dis = di.GetDirectories(DateTime.Now.ToShortDateString().Replace("/", "_")).Any() ? di.GetDirectories(DateTime.Now.ToShortDateString().Replace("/", "_")).FirstOrDefault() : di.CreateSubdirectory(DateTime.Now.ToShortDateString().Replace("/", "_"));
        //        //DirectoryInfo dis = di.CreateSubdirectory(DateTime.Now.ToShortDateString().Replace("/", "_"));
        //        string path = Path.Combine(dis.FullName, generatePensionProcessModel.BanckCode.Trim() + ".txt");

        //        StreamWriter sw = System.IO.File.CreateText(path);
        //        ViewBag.FileName = generatePensionProcessModel.BanckCode + ".txt";
        //        int TotalRowsCount = 0;
        //        //Get data from database and bind with report                 
        //        DVOddmStypddreAndStypddrd objDVOddmStypddreAndStypddrd = new DVOddmStypddreAndStypddrd();

        //        foreach (string strBankCode in _arrBankCodes)
        //        {
        //          objDVOddmStypddreAndStypddrd.BanckCode = objDVOddmStypddreAndStypddrd.BanckCode + strBankCode + "^";
        //        }
        //        objDVOddmStypddreAndStypddrd.GenCheck = generatePensionProcessModel.GenerateCheques;
        //        objDVOddmStypddreAndStypddrd.Date = generatePensionProcessModel.DepositDate != null ? Convert.ToDateTime(generatePensionProcessModel.DepositDate) : Convert.ToDateTime("01/01/1900");
        //        objDVOddmStypddreAndStypddrd.bank_code = generatePensionProcessModel.EmployeeType;
        //        DataTable objDataTable;
        //        if (!string.IsNullOrWhiteSpace(generatePensionProcessModel.GenerateCheques))
        //        {
        //          if (generatePensionProcessModel.GenerateCheques.Trim() == "Y")
        //          {
        //            objDataTable = BLLDirectDepositMedia.GetDirectDepositeData(ref objDVOddmStypddreAndStypddrd, true, apBatchID);
        //          }
        //          else
        //          {
        //            objDataTable = BLLDirectDepositMedia.GetDirectDepositeData(ref objDVOddmStypddreAndStypddrd, false, apBatchID);
        //          }
        //        }
        //        else
        //        {
        //          objDataTable = BLLDirectDepositMedia.GetDirectDepositeData(ref objDVOddmStypddreAndStypddrd, false, apBatchID);
        //        }
        //        string strBankCodeN = _arrBankCodes[0].ToString().Trim();
        //        if (objDataTable != null)
        //          if (objDataTable.Rows.Count > 0)
        //          {
        //            TotalRowsCount += objDataTable.Rows.Count;
        //            string mediaFormat = string.Empty;
        //            objDVOMasterBankDetails = listDVOMasterBankDetails.Find(delegate (DVOMasterBankDetails objParm)
        //            {
        //              return objParm.bank_code.Trim() == strBankCodeN.Trim();
        //            });

        //            mediaFormat = objDVOMasterBankDetails.media_str.Trim();

        //            string[] fields = new string[] {"BANK DESC","AMOUNT","BANK ACCT NO","CHK DIGIT","DFI DEST",
        //                                                    "EMP CODE","EMP NAME","PAY DATE","TRACE NO","TRANS CODE",
        //                                                    "BANK CODE","BATCH DATE","BATCH NO","DFI IMMED","DOC NO",
        //                                                    "ENTRY DESC","FILE ID","SVC CLASS","USED","SUPPLIER CODE",
        //                                                    "COMPANY NAME","PRIORITY CODE","FILE HASH","CREATE DATE",
        //                                                    "EIN NO","IMMED DEST DFI","IMMED CHK DIGIT","IMMED DEST NAME",
        //                                                    "RECORD SIZE","BLOCK FACTOR","FORMAT CODE","ENTRY CLASS","STATUS CODE","AMOUNTBDEC","AMOUNTADEC","ACTINFO","AMOUNTINCENTS","ACCOUNT TYPE"};

        //            string strInFormat = string.Empty, strLength = string.Empty;
        //            string strValue = string.Empty, cpMediaFormat = string.Empty, fieldNameInTable = string.Empty;
        //            int startIndex = 0, endIndex = 0, strFormatLength = 0;

        //            foreach (DataRow dr in objDataTable.Rows)
        //            {
        //              cpMediaFormat = mediaFormat;
        //              foreach (string field in fields)
        //              {
        //                startIndex = cpMediaFormat.IndexOf("[" + field + "(");
        //                if (startIndex >= 0)
        //                  endIndex = cpMediaFormat.IndexOf("]", startIndex) + 1;// +startIndex;
        //                if (startIndex >= 0 && endIndex > 0)
        //                {
        //                  strInFormat = cpMediaFormat.Substring(startIndex, endIndex - startIndex);
        //                  strLength = strInFormat.Substring(strInFormat.IndexOf("(") + 1, (strInFormat.IndexOf(")") - (strInFormat.IndexOf("(") + 1)));
        //                  if (strLength != string.Empty)
        //                    strFormatLength = Convert.ToInt32(strLength);

        //                  Int32 AmountType = 0;
        //                  switch (field)
        //                  {
        //                    case "BANK DESC":
        //                      fieldNameInTable = "v_bank_desc";
        //                      break;
        //                    case "AMOUNT":
        //                      fieldNameInTable = "v_amount";
        //                      // AmountType = 1;
        //                      break;
        //                    case "AMOUNTBDEC":
        //                      fieldNameInTable = "IntegralAmount";
        //                      // AmountType = 2;
        //                      break;
        //                    case "AMOUNTADEC":
        //                      fieldNameInTable = "FractionalAmount";
        //                      //AmountType = 3;
        //                      break;
        //                    case "AMOUNTINCENTS":
        //                      fieldNameInTable = "AMOUNTINCENTS";
        //                      break;
        //                    case "BANK ACCT NO":
        //                      fieldNameInTable = "v_bank_acct_no";
        //                      break;
        //                    case "ACTINFO":
        //                      fieldNameInTable = "ACTINFO";
        //                      break;
        //                    case "CHK DIGIT":
        //                      fieldNameInTable = "v_chk_digit";
        //                      break;
        //                    case "DFI DEST":
        //                      fieldNameInTable = "v_dfi_dest";
        //                      break;
        //                    case "EMP CODE":
        //                      fieldNameInTable = "v_empl_code";
        //                      break;
        //                    case "EMP NAME":
        //                      fieldNameInTable = "v_empl_name";
        //                      break;
        //                    case "PAY DATE":
        //                      fieldNameInTable = "v_pay_date";
        //                      break;
        //                    case "TRACE NO":
        //                      fieldNameInTable = "v_trace_number";
        //                      break;
        //                    case "TRANS CODE":
        //                      fieldNameInTable = "v_trans_code";
        //                      break;
        //                    case "BANK CODE":
        //                      fieldNameInTable = "v_bank_code";
        //                      break;
        //                    case "BATCH DATE":
        //                      fieldNameInTable = "v_batch_date";
        //                      break;
        //                    case "BATCH NO":
        //                      fieldNameInTable = "v_batch_no";
        //                      break;
        //                    case "DFI IMMED":
        //                      fieldNameInTable = "v_dfi_immed";
        //                      break;
        //                    case "DOC NO":
        //                      fieldNameInTable = "v_doc_no";
        //                      break;
        //                    case "ENTRY DESC":
        //                      fieldNameInTable = "v_entry_desc";
        //                      break;
        //                    case "FILE ID":
        //                      fieldNameInTable = "v_file_id";
        //                      break;
        //                    case "SVC CLASS":
        //                      fieldNameInTable = "v_svc_class";
        //                      break;
        //                    case "USED":
        //                      fieldNameInTable = "v_used";
        //                      break;
        //                    case "SUPPLIER CODE":
        //                      fieldNameInTable = "v_suppliercode";
        //                      break;
        //                    case "COMPANY NAME":
        //                      fieldNameInTable = "v_company_name";
        //                      break;
        //                    case "PRIORITY CODE":
        //                      fieldNameInTable = "priority_code";
        //                      break;
        //                    case "FILE HASH":
        //                      fieldNameInTable = "file_hash";
        //                      break;
        //                    case "CREATE DATE":
        //                      fieldNameInTable = "create_date";
        //                      break;
        //                    case "EIN NO":
        //                      fieldNameInTable = "ein_number";
        //                      break;
        //                    case "IMMED DEST DFI":
        //                      fieldNameInTable = "immed_dest_dfi";
        //                      break;
        //                    case "IMMED CHK DIGIT":
        //                      fieldNameInTable = "immed_chk_digit";
        //                      break;
        //                    case "IMMED DEST NAME":
        //                      fieldNameInTable = "immed_dest_name";
        //                      break;
        //                    case "RECORD SIZE":
        //                      fieldNameInTable = "recordsize";
        //                      break;
        //                    case "BLOCK FACTOR":
        //                      fieldNameInTable = "blockfactor";
        //                      break;
        //                    case "FORMAT CODE":
        //                      fieldNameInTable = "formatcode";
        //                      break;
        //                    case "ENTRY CLASS":
        //                      fieldNameInTable = "entryclass";
        //                      break;
        //                    case "STATUS CODE":
        //                      fieldNameInTable = "statuscode";
        //                      break;
        //                    case "ACCOUNT TYPE":
        //                      fieldNameInTable = "actttype";
        //                      break;
        //                  }

        //                  if (fieldNameInTable != string.Empty)
        //                  {
        //                    int length = 0;
        //                    if (dr[fieldNameInTable] != DBNull.Value)
        //                    {
        //                      length = dr[fieldNameInTable].ToString().Trim().Length;
        //                      if (strFormatLength > 0 && dr[fieldNameInTable] != DBNull.Value && dr[fieldNameInTable].ToString().Length > strFormatLength)
        //                      {
        //                        strValue = dr[fieldNameInTable].ToString().Substring(0, strFormatLength);
        //                      }
        //                      else if (strFormatLength > 0 && dr[fieldNameInTable] != DBNull.Value && dr[fieldNameInTable].ToString().Length < strFormatLength)
        //                      {
        //                        if (objDataTable.Columns[fieldNameInTable].DataType == Type.GetType("System.Int32") || objDataTable.Columns[fieldNameInTable].DataType == Type.GetType("System.Decimal"))
        //                        {
        //                          if (fieldNameInTable != "FractionalAmount")
        //                          {
        //                            for (int i = dr[fieldNameInTable].ToString().Length; i < strFormatLength; i++)
        //                            {
        //                              strValue = strValue + "_";
        //                            }
        //                            strValue = strValue + dr[fieldNameInTable].ToString();
        //                          }

        //                          else
        //                          {
        //                            for (int i = dr[fieldNameInTable].ToString().Length; i < strFormatLength; i++)
        //                            {
        //                              strValue = strValue + "0";
        //                            }
        //                            strValue = strValue + dr[fieldNameInTable].ToString();
        //                          }
        //                        }
        //                        else
        //                        {
        //                          strValue = strValue + dr[fieldNameInTable].ToString();
        //                          for (int i = dr[fieldNameInTable].ToString().Length; i < strFormatLength; i++)
        //                          {
        //                            strValue = strValue + "_";
        //                          }
        //                        }
        //                      }

        //                      else if (strFormatLength > 0 && dr[fieldNameInTable] == DBNull.Value)
        //                      {
        //                        strValue = strValue;
        //                        for (int i = 0; i < strFormatLength; i++)
        //                        {
        //                          strValue = strValue + "_";
        //                        }
        //                      }
        //                      else
        //                      {
        //                        strValue = dr[fieldNameInTable].ToString();
        //                      }
        //                    }
        //                    cpMediaFormat = cpMediaFormat.Replace(strInFormat, strValue);
        //                  }
        //                  fieldNameInTable = string.Empty;
        //                  strValue = string.Empty;
        //                }
        //              }
        //              cpMediaFormat = cpMediaFormat.Replace("_", " ");
        //              cpMediaFormat = cpMediaFormat.Replace("[TAB]", "\t");
        //              sw.WriteLine(cpMediaFormat);
        //              cpMediaFormat = string.Empty;
        //              startIndex = 0;
        //              endIndex = 0;

        //            }
        //          }
        //        if (TotalRowsCount <= 0)
        //        {
        //          //SatyaPayMain.CommonUtilities.Utilities.ShowMessage("There is no record to create media.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //          TempData["error"] = "There is no record to create media.";
        //        }
        //        else
        //          TempData["success"] = "File has been successfully saved.";
        //        //SatyaPayMain.CommonUtilities.Utilities.ShowMessage("File has been successfully saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //txtFilePath.Clear();
        //        sw.Close();

        //        String[] allfiles = System.IO.Directory.GetFiles(Server.MapPath("~/BankMediaFile"), "*.*", System.IO.SearchOption.AllDirectories);

        //        return PartialView("~/Views/PensionProcess/DirectDeposits/GenerateBankMedia.cshtml", generatePensionProcessModel);
        //      }

        //    }
        //  }
        //  catch (Exception ex)
        //  {
        //    //ReportViewer.ReportSource = null;
        //    //SatyaPay.StyleUtility.Reports_Splasher.Close();
        //    //ExceptionManagement.ExceptionManager.Publish(ex);
        //    //MessageBox.Show(ex.Message, "Satya Pay.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    //return PartialView("~/Views/PensionProcess/DirectDeposits/GenerateBankMedia.cshtml");
        //    TempData["error"] = "Please Try Again..." + ex.Message;
        //  }
        //  return PartialView("~/Views/PensionProcess/DirectDeposits/GenerateBankMedia.cshtml", generatePensionProcessModel);
        //}

        /// <summary>
        /// DVOPayrollProcess_PayEmployee type object to contain Search Criteria entered by user
        /// </summary>
        DVOPayrollProcess_PayEmployee objSearch = null;
        /// <summary>
        /// list of DVOPayrollProcess_PayEmployee objects to save result of search
        /// </summary>
        List<DVOPayrollProcess_PayEmployee> ListDVOPayrollProcess_PayEmployee = null;
        private DVOPayrollProcess_PayEmployee GetPayrollEntries(DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployee)
        {
            DVOPayrollProcess_PayEmployee obj = new DVOPayrollProcess_PayEmployee();
            objSearch = objPayrollProcess_PayEmployee;
            ListDVOPayrollProcess_PayEmployee = BLLUpdatePayrollEntries.GetPayrollEntriesData(ref objSearch);
            if (ListDVOPayrollProcess_PayEmployee != null)
                if (ListDVOPayrollProcess_PayEmployee.Count > 0)
                {
                    obj = ListDVOPayrollProcess_PayEmployee[3];
                    //SetControlsModeAfterAccept(dbOperation.Find, this);
                    ////make form's controls in next mode
                    //ControlsMode(dbOperation.Next);
                    //ShowSearchResult(false);
                    ////show message in status bar of form
                    //base.StatusMessage.Text = Convert.ToString(CurrentRecordIndex + 1) + " of " + ListDVOPayrollProcess_PayEmployee.Count.ToString() + " Records";
                }
                else
                {
                    ////otherwise show message that there is not record find by search
                    //Utilities.ShowMessage("No record found matching search creteria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //SetControlsModeAfterAccept(dbOperation.Cancel, this);
                    //ClearFormData();
                    //base.StatusMessage.Text = "No record found.";
                }
            return obj;
        }//

        public JsonResult PensionerSearchAjax(int EId)
        {
            clearPensionPayrollSession();
            int UserId = AppUserManager.GetUserId();
            int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            var result = BLLMasterEmployee.GetAllData(UserId, RoleId).Where(x => x.EmplrCode == EId).Select(x => new { Id = x.EmplCode, Name = x.FirstName + " " + x.MiddleName + " " + x.LastName }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UpdatePayrollEntries(int? Id)
        {
            ViewBag.EmployerId = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true), "Id", "EmployerName");
            List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = new List<DVOMasterEmployee>();
            ViewBag.EmployeeId = new SelectList(listSearchResultDVOMasterEmployee, "EmplCode", "FirstName");
            DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
            var yesNo = from x in YesNo()
                        select new { Id = x.Value, Name = x.Key };
            ViewBag.YesNo = new SelectList(yesNo.OrderBy(x => x.Name), "Id", "Name");

            var Frequency = from x in dedFrequency()
                            select new { Id = x.Value, Name = x.Key };
            ViewBag.Frequency = new SelectList(Frequency.OrderBy(x => x.Name), "Id", "Name");

            var okToPost = from x in OkToPost()
                           select new { Id = x.Value, Name = x.Key };
            ViewBag.OkToPost = new SelectList(okToPost.OrderBy(x => x.Name), "Id", "Name");

            if (Id != null)
            {
                objPayrollProcess_PayEmployee.EmplCode = Id.ToString();
                objPayrollProcess_PayEmployee = GetPayrollEntries(objPayrollProcess_PayEmployee);

                DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(Id.ToString());//to get employer details
                ViewBag.EmployerId = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true), "Id", "EmployerName", objDVOMasterEmployee.EmplrCode);
                ViewBag.PensionerID = objPayrollProcess_PayEmployee.EmplCode;
                ViewBag.Name = String.Format("{0} {1} {2}", objPayrollProcess_PayEmployee.FirstName, objPayrollProcess_PayEmployee.MiddleName, objPayrollProcess_PayEmployee.LastName);
                ViewBag.CId = objPayrollProcess_PayEmployee.EmplCode;
                @ViewBag.Id = objPayrollProcess_PayEmployee.Doc_no;
                ViewBag.YesNo = new SelectList(yesNo.OrderBy(x => x.Name), "Id", "Name", objPayrollProcess_PayEmployee.print_check);
                ViewBag.Frequency = new SelectList(Frequency.OrderBy(x => x.Name), "Id", "Name", objPayrollProcess_PayEmployee.PayPeriod);
                ViewBag.OkToPost = new SelectList(okToPost.OrderBy(x => x.Name), "Id", "Name", objPayrollProcess_PayEmployee.ok_to_post);
                //to get employee details
                int UserId = AppUserManager.GetUserId();
                int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
                var result = BLLMasterEmployee.GetAllData(UserId, RoleId).Where(x => x.EmplrCode == objDVOMasterEmployee.EmplrCode).Select(x => new { Id = x.EmplCode, Name = x.FirstName + " " + x.MiddleName + " " + x.LastName }).ToList();
                ViewBag.EmployeeId = new SelectList(result, "Id", "Name", Id);
            }

            //TempData["PayrollProcess_PayEmployee"] = objPayrollProcess_PayEmployee;
            return PartialView("~/Views/PensionProcess/GeneratePensions/UpdatePayrollEntries.cshtml", objPayrollProcess_PayEmployee);
        }
        [HttpGet]
        public ActionResult IncomeIndex(int? Id)
        {
            if (Id != null)
            {
                @ViewBag.CId = Id;
                DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(Id.ToString());
                ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
                ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);

                DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                objPayrollProcess_PayEmployee.EmplCode = objDVOMasterEmployee.EmplCode;
                DVOPayrollProcess_PayEmployee obj = GetPayrollEntries(objPayrollProcess_PayEmployee);

                //create table to bind with datagridview
                //DataTable dtIncome = CreateDataTableForIncome();
                List<DVOPayrollstypayid> ListDVOPayrollstypayid = new List<DVOPayrollstypayid>();
                ListDVOPayrollstypayid = BLLUpdatePayrollEntries.GetPayrollIncomesData(obj.Doc_no);

                var ObjList = db.MasterIncCodes.OrderBy(x => x.description).Select(x => new { inc_code = x.inc_code, description = x.description }).ToList();
                //TempData["ListDVOPayrollstypayid"] = ListDVOPayrollstypayid;
                ViewBag.inc_code = new SelectList(ObjList, "inc_code", "description");
                if (Session["NewIncome"] != null)
                    ListDVOPayrollstypayid = (List<DVOPayrollstypayid>)Session["NewIncome"];

                return PartialView("~/Views/PensionProcess/GeneratePensions/IncomeIndex.cshtml", ListDVOPayrollstypayid);
            }
            return RedirectToAction("UpdatePayrollEntries");
        }

        [HttpPost]
        public ActionResult IncomeIndex(List<DVOPayrollstypayid> model)
        {

            string _accounttype = string.Empty, _accountdescription = string.Empty, _accountkeyvalue = string.Empty;
            int _accounttypeid = 0;

            //List<DVOPayrollstypayid> OldListDVOPayrollstypayid = TempData["ListDVOPayrollstypayid"] != null ? (List<DVOPayrollstypayid>)TempData["ListDVOPayrollstypayid"] : new List<DVOPayrollstypayid>();
            //Session["OldIncome"] = OldListDVOPayrollstypayid;
            string Empl_Code = Request["CId"];
            //string Empl_Code = Request.Form["PensionerID"];
            //int id = model.RowID;
            //int userid = AppUserManager.GetUserId();
            //ViewBag.PensionerID = Empl_Code;
            //ViewBag.inc_co = new SelectList(db.MasterDedcodes.OrderBy(x => x.description), "ded_code", "description", model.ded_code);
            //create table to bind with datagridview
            //DataTable dtIncome = CreateDataTableForIncome();
            List<DVOPayrollstypayid> tempmodel = new List<DVOPayrollstypayid>();
            if (ModelState.IsValid && model != null)
            {
                if (model.Count > 0)
                {

                    foreach (DVOPayrollstypayid ObjIncome in model)
                    {
                        //DataRow dr = dtIncome.NewRow();
                        //dr["IncomeCode"] = ObjIncome.inc_code;
                        //dr["Rate"] = ObjIncome.inc_rate != null ? String.Format("{0:0.00000}", ObjIncome.inc_rate) : null;
                        //dr["Number"] = ObjIncome.number != null ? String.Format("{0:0.00}", ObjIncome.number) : null;
                        //dr["Amount"] = ObjIncome.amount != null ? String.Format("{0:0.00}", ObjIncome.amount) : null;
                        //dr["Hours"] = ObjIncome.hours != null ? String.Format("{0:0.00}", ObjIncome.hours) : null;
                        //dr["Acct_no"] = ObjIncome.acct_no;
                        //dr["lo_inc_amt"] = ObjIncome.lo_inc_amt;
                        //dr["hi_inc_amt"] = ObjIncome.hi_inc_amt;
                        //dr["line_no"] = ObjIncome.line_no;
                        //dr["rowid"] = ObjIncome.RowID;
                        //dr["inc_type"] = ObjIncome.inc_type.Trim();

                        if (ObjIncome.acct_no > 0)
                        {
                            BLLCommonUtilities.GetAccountInformation(ObjIncome.acct_no, out _accountkeyvalue, out _accounttype, out _accounttypeid, out _accountdescription);
                            ObjIncome.acctKeyvalue = _accountkeyvalue;
                            ObjIncome.acctAccountTypeId = _accounttypeid;
                            //dr["Keyvalue1"] = _accountkeyvalue;
                            //dr["accountid1"] = _accounttypeid;
                        }
                        tempmodel.Add(ObjIncome);
                        //dtIncome.Rows.Add(dr);
                        //dr = null;
                    }
                    //dtIncome.AcceptChanges();
                    //if (dtIncome.Rows.Count > 0)
                    //{

                    //}
                    //Calculate Amount Total ,Number Totals and Hours Totals
                    //CalulateTotals("I");
                }
                else
                {


                }


            }
            Session["NewIncome"] = tempmodel;
            var ObjList = db.MasterIncCodes.OrderBy(x => x.description).Select(x => new { inc_code = x.inc_code, description = x.description }).ToList();
            ViewBag.inc_code = new SelectList(ObjList, "inc_code", "description");
            @ViewBag.CId = Empl_Code;
            DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(Empl_Code.ToString());
            ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
            ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);

            DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
            objPayrollProcess_PayEmployee.EmplCode = objDVOMasterEmployee.EmplCode;
            DVOPayrollProcess_PayEmployee obj = GetPayrollEntries(objPayrollProcess_PayEmployee);

            //create table to bind with datagridview
            List<DVOPayrollstypayid> ListDVOPayrollstypayid = new List<DVOPayrollstypayid>();
            ListDVOPayrollstypayid = BLLUpdatePayrollEntries.GetPayrollIncomesData(obj.Doc_no);
            return RedirectToAction("DeductionIndex", new { id = objPayrollProcess_PayEmployee.EmplCode });
            //return PartialView("~/Views/PensionProcess/GeneratePensions/IncomeIndex.cshtml", ListDVOPayrollstypayid);
        }

        private List<DVOPayrollstypaydd> Payrollstypaydd(string PensionerID)
        {
            string _accounttype = string.Empty, _accountdescription = string.Empty, _accountkeyvalue = string.Empty;
            int _accounttypeid = 0;

            #region Payroll Deduction
            DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
            objPayrollProcess_PayEmployee.EmplCode = PensionerID;
            DVOPayrollProcess_PayEmployee obj = GetPayrollEntries(objPayrollProcess_PayEmployee);

            //create table to bind with datagridview
            DataTable dtDeduction = CreateDataTableForDeduction();

            List<DVOPayrollstypaydd> ListDVOPayrollstypaydd = new List<DVOPayrollstypaydd>();
            if (AllFromPayre)
                ListDVOPayrollstypaydd = obj.ListDVOPayrollstypaydd;
            else
                ListDVOPayrollstypaydd = BLLUpdatePayrollEntries.GetPayrollDeductionData(obj.Doc_no);

            if (ListDVOPayrollstypaydd.Count > 0)
            {
                foreach (DVOPayrollstypaydd ObjDeduction in ListDVOPayrollstypaydd)
                {
                    DataRow dr = dtDeduction.NewRow();
                    dr["DeductionCode"] = ObjDeduction.ded_code;
                    dr["Rate2"] = ObjDeduction.ded_rate != null ? String.Format("{0:0.00}", ObjDeduction.ded_rate) : null;
                    dr["Amount2"] = ObjDeduction.amount != null ? String.Format("{0:0.00}", ObjDeduction.amount) : null;
                    dr["lo_ded_amt"] = ObjDeduction.lo_ded_amt != null ? ObjDeduction.lo_ded_amt : null;
                    dr["hi_ded_amt"] = ObjDeduction.hi_ded_amt != null ? ObjDeduction.hi_ded_amt : null;
                    dr["acct_no2"] = ObjDeduction.acct_no;
                    dr["line_no2"] = ObjDeduction.line_no;
                    dr["rowid2"] = ObjDeduction.RowID;
                    dr["PayLimit2"] = ObjDeduction.pay_limit != null ? String.Format("{0:0.00}", ObjDeduction.pay_limit) : null;
                    //dr["Applied2"] = ObjDeduction.line_no;
                    dr["RollOver2"] = ObjDeduction.yearrollover;

                    if (ObjDeduction.acct_no > 0)
                    {
                        BLLCommonUtilities.GetAccountInformation(ObjDeduction.acct_no, out _accountkeyvalue, out _accounttype, out _accounttypeid, out _accountdescription);
                        dr["Keyvalue2"] = _accountkeyvalue;
                        dr["accountid2"] = _accounttypeid;
                    }

                    dr["ded_type"] = ObjDeduction.ded_type.Trim();
                    dr["ded_taxred"] = ObjDeduction.ded_taxred.Trim();
                    dr["ded_limit"] = ObjDeduction.ded_limit != null ? ObjDeduction.ded_limit : null;
                    dr["ded_ytd"] = ObjDeduction.ded_ytd != null ? ObjDeduction.ded_ytd : null;
                    dtDeduction.Rows.Add(dr);
                    dr = null;
                }
                dtDeduction.AcceptChanges();
                if (dtDeduction.Rows.Count > 0)
                {
                    //dgvDeduction.DataSource = dtDeduction;
                }
                //Calculate Amount Total
                CalulateTotals("D");
            }
            else
            {

            }
            #endregion
            return ListDVOPayrollstypaydd;
        }

        [HttpGet]
        public ActionResult DeductionIndex(int? Id)
        {
            if (Id != null)
            {
                @ViewBag.CId = Id;
                DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(Id.ToString());
                ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
                List<DVOPayrollstypaydd> PayrollstypayddList = Payrollstypaydd(objDVOMasterEmployee.EmplCode);
                ViewBag.DocNo = PayrollstypayddList.Any() ? PayrollstypayddList.FirstOrDefault().Doc_no : 0;
                ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
                var ObjList = db.MasterDedcodes.Select(x => new { ded_code = x.ded_code, description = x.description }).ToList().OrderBy(x => x.description);
                ViewBag.ded_code = new SelectList(ObjList, "ded_code", "description");
                if (Session["NewDeduction"] != null)
                    PayrollstypayddList = (List<DVOPayrollstypaydd>)Session["NewDeduction"];

                //TempData["ListDVOPayrollstypaydd"] = PayrollstypayddList;
                return PartialView("~/Views/PensionProcess/GeneratePensions/DeductionIndex.cshtml", PayrollstypayddList);
            }
            return RedirectToAction("UpdatePayrollEntries");
        }

        [HttpPost]
        public ActionResult DeductionIndex(List<DVOPayrollstypaydd> model)
        {

            string _accounttype = string.Empty, _accountdescription = string.Empty, _accountkeyvalue = string.Empty;
            int _accounttypeid = 0;

            //List<DVOPayrollstypaydd> OldListDVOPayrollstypaydd = TempData["ListDVOPayrollstypaydd"] != null ? (List<DVOPayrollstypaydd>)TempData["ListDVOPayrollstypaydd"] : new List<DVOPayrollstypaydd>();
            string Empl_Code = Request["CId"];
            @ViewBag.CId = Empl_Code;
            DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(Empl_Code);
            ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
            List<DVOPayrollstypaydd> PayrollstypayddList = Payrollstypaydd(objDVOMasterEmployee.EmplCode);
            ViewBag.DocNo = PayrollstypayddList.Any() ? PayrollstypayddList.FirstOrDefault().Doc_no : 0;
            ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
            var ObjList = db.MasterDedcodes.Select(x => new { ded_code = x.ded_code, description = x.description }).ToList().OrderBy(x => x.description);
            ViewBag.ded_code = new SelectList(ObjList, "ded_code", "description");
            //create table to bind with datagridview
            //DataTable dtDeduction = CreateDataTableForDeduction();

            if (model.Count > 0)
            {
                List<DVOPayrollstypaydd> tempList = new List<DVOPayrollstypaydd>();
                foreach (DVOPayrollstypaydd ObjDeduction in model)
                {
                    //DataRow dr = dtDeduction.NewRow();
                    //dr["DeductionCode"] = ObjDeduction.ded_code;
                    //dr["Rate2"] = ObjDeduction.ded_rate != null ? String.Format("{0:0.00}", ObjDeduction.ded_rate) : null;
                    //dr["Amount2"] = ObjDeduction.amount != null ? String.Format("{0:0.00}", ObjDeduction.amount) : null;
                    //dr["lo_ded_amt"] = ObjDeduction.lo_ded_amt != null ? ObjDeduction.lo_ded_amt : null;
                    //dr["hi_ded_amt"] = ObjDeduction.hi_ded_amt != null ? ObjDeduction.hi_ded_amt : null;
                    //dr["acct_no2"] = ObjDeduction.acct_no;
                    //dr["line_no2"] = ObjDeduction.line_no;
                    //dr["rowid2"] = ObjDeduction.RowID;
                    //dr["PayLimit2"] = ObjDeduction.pay_limit != null ? String.Format("{0:0.00}", ObjDeduction.pay_limit) : null;
                    ////dr["Applied2"] = ObjDeduction.line_no;
                    //dr["RollOver2"] = ObjDeduction.yearrollover;

                    if (ObjDeduction.acct_no > 0)
                    {
                        BLLCommonUtilities.GetAccountInformation(ObjDeduction.acct_no, out _accountkeyvalue, out _accounttype, out _accounttypeid, out _accountdescription);
                        ObjDeduction.acctKeyvalue = _accountkeyvalue;
                        ObjDeduction.accountTypeId = _accounttypeid; ;
                        //dr["Keyvalue2"] = _accountkeyvalue;
                        //dr["accountid2"] = _accounttypeid;
                    }

                    //dr["ded_type"] = ObjDeduction.ded_type.Trim();
                    //dr["ded_taxred"] = ObjDeduction.ded_taxred.Trim();
                    //dr["ded_limit"] = ObjDeduction.ded_limit != null ? ObjDeduction.ded_limit : null;
                    //dr["ded_ytd"] = ObjDeduction.ded_ytd != null ? ObjDeduction.ded_ytd : null;
                    //dtDeduction.Rows.Add(dr);
                    //dr = null;
                    tempList.Add(ObjDeduction);
                }
                Session["NewDeduction"] = tempList;
                //dtDeduction.AcceptChanges();
                //if (dtDeduction.Rows.Count > 0)
                //{
                //  //dgvDeduction.DataSource = dtDeduction;
                //}
                //Calculate Amount Total
                //CalulateTotals("D");
                return RedirectToAction("DirectDepositIndex", new { id = Empl_Code });
            }

            return PartialView("~/Views/PensionProcess/GeneratePensions/DeductionIndex.cshtml", PayrollstypayddList);
        }

        [HttpGet]
        public ActionResult DirectDepositIndex(int? Id)
        {
            if (Id != null)
            {
                if (Session["NewIncome"] == null)
                {
                    TempData["error"] = "Add Income Details";
                    return RedirectToAction("IncomeIndex", new { id = Id });
                }
                if (Session["NewDeduction"] == null)
                {
                    TempData["error"] = "Add Deduction Details";
                    return RedirectToAction("DeductionIndex", new { id = Id });
                }
                DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(Id.ToString());
                ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
                ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
                ViewBag.CId = Id;
                var bankAmountType = from BankAmountType e in Enum.GetValues(typeof(BankAmountType))
                                     select new { Id = (char)e, Name = e.ToString() };
                var typeofacct = from typeofacct e in Enum.GetValues(typeof(typeofacct))
                                 select new { Id = (char)e, Name = e.ToString() };
                ViewBag.typeofacct = new SelectList(typeofacct.OrderBy(x => x.Name), "Id", "Name");
                ViewBag.type = new SelectList(bankAmountType.OrderBy(x => x.Name), "Id", "Name");
                ViewBag.bank_code = new SelectList(db.MasterBanks.OrderBy(x => x.bank_desc), "bank_code", "bank_desc");
                var PayrollddList = db.MasterEmpBankDetails.Where(x => x.empl_code == Id.ToString()).ToList();
                return PartialView("~/Views/PensionProcess/GeneratePensions/DirectDepositIndex.cshtml", PayrollddList);
            }
            return RedirectToAction("UpdatePayrollEntries");
        }
        [HttpPost]
        public ActionResult DirectDepositIndex(List<MasterEmpBankDetails> model)
        {

            string Empl_Code = Request["CId"];
            @ViewBag.CId = Empl_Code;
            DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(Empl_Code.ToString());
            ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
            ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);

            bool Success = UpdatePayrollProcessInformation();
            if (Success)
            {
                clearPensionPayrollSession();
                TempData["success"] = "Details Updated SuccessFully";
                return RedirectToAction("UpdatePayrollEntries", new { Id = Empl_Code });
            }
            var bankAmountType = from BankAmountType e in Enum.GetValues(typeof(BankAmountType))
                                 select new { Id = (char)e, Name = e.ToString() };
            var typeofacct = from typeofacct e in Enum.GetValues(typeof(typeofacct))
                             select new { Id = (char)e, Name = e.ToString() };
            ViewBag.typeofacct = new SelectList(typeofacct.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.type = new SelectList(bankAmountType.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.bank_code = new SelectList(db.MasterBanks.OrderBy(x => x.bank_desc), "bank_code", "bank_desc");
            return PartialView("~/Views/PensionProcess/GeneratePensions/DirectDepositIndex.cshtml", model);
        }

        public void clearPensionPayrollSession()
        {
            Session["NewIncome"] = null;
            Session["NewDeduction"] = null;
        }
        private bool UpdatePayrollProcessInformation()
        {
            bool Success = false;
            try
            {

                #region Payroll Information

                //make an object with values of appropriate properties to insert into database
                DVOPayrollProcess_PayEmployee objUpdDVOPayrollProcess_PayEmployee = (DVOPayrollProcess_PayEmployee)Session["NewUpdateEntries"];

                #endregion Payroll Information

                #region Payroll Incomes

                //make list of detail-objects of main object
                List<DVOPayrollstypayid> newIncomeDetails = (List<DVOPayrollstypayid>)Session["NewIncome"];
                List<DVOPayrollstypayid> objNewlistDVOPayrollstypayid = newIncomeDetails.Where(x => x.RowID == 0 && x.description_MasterIncCodes != "deleted").ToList();
                List<DVOPayrollstypayid> objUpdatelistDVOPayrollstypayid = newIncomeDetails.Where(x => x.RowID != 0 && x.description_MasterIncCodes + "" != "deleted").ToList();
                List<DVOPayrollstypayid> objDeletelistDVOPayrollstypayid = newIncomeDetails.Where(x => x.RowID != 0 && x.description_MasterIncCodes + "" == "deleted").ToList();

                #endregion

                #region Payroll Deduction
                List<DVOPayrollstypaydd> newDeduction = (List<DVOPayrollstypaydd>)Session["NewDeduction"];
                //make list of detail-objects of main object
                List<DVOPayrollstypaydd> listNewDVOPayrollstypaydd = newDeduction.Where(x => x.RowID == 0 && x.description_MasterIncCodes + "" != "deleted").ToList();
                List<DVOPayrollstypaydd> listUpdateDVOPayrollstypaydd = newDeduction.Where(x => x.RowID != 0 && x.description_MasterIncCodes + "" != "deleted").ToList();
                List<DVOPayrollstypaydd> listDeleteDVOPayrollstypaydd = newDeduction.Where(x => x.RowID != 0 && x.description_MasterIncCodes + "" == "deleted").ToList();

                #endregion Payroll Deduction

                #region "Payroll Obligation"

                //make list of detail-objects of main object
                List<DVOPayrollStypayod> listUpdateDVOPayrollStypayod = new List<DVOPayrollStypayod>();
                List<DVOPayrollStypayod> listNewDVOPayrollStypayod = new List<DVOPayrollStypayod>();
                List<DVOPayrollStypayod> listDeleteDVOPayrollStypayod = new List<DVOPayrollStypayod>();



                #endregion "Payroll Obligation"

                //Call Update function of BLL
                int i = BLLUpdatePayrollEntries.UpdatePayrollEntries(ref TransactionObject,
                    ref objUpdDVOPayrollProcess_PayEmployee,
                    ref objNewlistDVOPayrollstypayid,
                    ref objUpdatelistDVOPayrollstypayid,
                    ref objDeletelistDVOPayrollstypayid,
                    ref listNewDVOPayrollstypaydd,
                    ref listUpdateDVOPayrollstypaydd,
                    ref listDeleteDVOPayrollstypaydd,
                    ref listNewDVOPayrollStypayod,
                    ref listUpdateDVOPayrollStypayod,
                    ref listDeleteDVOPayrollStypayod
                    );

                //if process is successfully completed, then set Success flag to true.
                if (i > 0)
                    Success = true;

                objUpdDVOPayrollProcess_PayEmployee = null;
                objNewlistDVOPayrollstypayid = null;
                objUpdatelistDVOPayrollstypayid = null;
                objDeletelistDVOPayrollstypayid = null;
                listNewDVOPayrollstypaydd = null;
                listUpdateDVOPayrollstypaydd = null;
                listDeleteDVOPayrollstypaydd = null;
                listNewDVOPayrollStypayod = null;
                listUpdateDVOPayrollStypayod = null;
                listDeleteDVOPayrollStypayod = null;
            }
            catch (Exception ex)
            {
                Success = false;
                ErrorMessage = ex.Message;
            }
            return Success;
        }
        [HttpGet]
        public ActionResult EditIncomeDetails(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterEmployeeIncomes model = db.MasterEmployeeIncomes.Find(Id);
            if (model == null)
            {
                return HttpNotFound();
            }
            @ViewBag.Id = Id;
            DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(model.Empl_Code);
            ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
            ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
            ViewBag.CId = model.Empl_Code;
            ViewBag.inc_code = new SelectList(db.MasterIncCodes.OrderBy(x => x.description), "inc_code", "description", model.inc_code);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditIncomeDetails(MasterEmployeeIncomes model)
        {
            int id = model.EmpIncomeID;
            DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(model.Empl_Code);
            ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
            ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
            ViewBag.inc_code = new SelectList(db.MasterIncCodes.OrderBy(x => x.description), "inc_code", "description", model.inc_code);
            ViewBag.CId = model.Empl_Code;
            if (model.acct_no == null || model.hi_inc_amt == null || model.inc_code == null || model.inc_rate == null)
            {
                if (!db.MasterEmployeeIncomes.Where(x => x.Empl_Code == model.Empl_Code && x.inc_code == model.inc_code && x.EmpIncomeID != id).Any() && ModelState.IsValid)
                {
                    #region "Employee Income"
                    MasterEmployeeIncomes objDVOMasterEmployeeIncomes = db.MasterEmployeeIncomes.Find(model.EmpIncomeID);
                    //make object to pass as parameter of search function
                    DVOUpdateIncCode objDVOUpdateIncCode = new DVOUpdateIncCode();
                    objDVOUpdateIncCode.inc_code = model.inc_code;
                    //call getDate function of BLL
                    List<DVOUpdateIncCode> listDVOUpdateIncCode = BLLUpdateIncomeCodes.GetDetailedInfo(ref objDVOUpdateIncCode);

                    //make list of detail-objects of main object
                    List<DVOMasterEmployeeIncomes> listDVOMasterEmployeeIncomes = new List<DVOMasterEmployeeIncomes>();
                    //check datagridview is enable or not if not, no need to add objects into list
                    //if (dgvIncome1.Enabled)
                    //check datagridview has some items to add or not
                    if (listDVOUpdateIncCode.Any())
                    {
                        //if datagridview has some rows then make new object with values entered in rows
                        foreach (var drGrid in listDVOUpdateIncCode)
                        {
                            if (drGrid.inc_code != null && drGrid.inc_code.Length > 0 && model.inc_code == drGrid.inc_code)
                            {
                                //objDVOMasterEmployeeIncomes = new MasterEmployeeIncomes();
                                //assign appropriate values to detail-object
                                //objDVOMasterEmployeeIncomes.EmpIncomeID = model.EmpIncomeID;
                                objDVOMasterEmployeeIncomes.Empl_Code = model.Empl_Code;
                                objDVOMasterEmployeeIncomes.inc_code = drGrid.inc_code;
                                objDVOMasterEmployeeIncomes.inc_rate = model.inc_rate;//tOdO//objDVOMasterEmployeeModel.AnnualAmount / 12;
                                objDVOMasterEmployeeIncomes.hi_inc_amt = model.hi_inc_amt;
                                objDVOMasterEmployeeIncomes.inc_number = Convert.ToDecimal(1);
                                objDVOMasterEmployeeIncomes.inc_hours = Convert.ToDecimal(1);
                                objDVOMasterEmployeeIncomes.line_no = db.MasterEmployeeIncomes.Where(x => x.Empl_Code == model.Empl_Code && x.EmpIncomeID == id).FirstOrDefault().line_no;
                                if (drGrid.acct_no != null)
                                    objDVOMasterEmployeeIncomes.acct_no = db.MasterEmployeeIncomes.Where(x => x.Empl_Code == model.Empl_Code && x.EmpIncomeID == id).FirstOrDefault().acct_no;
                                //objDVOMasterEmployeeIncomes.acct_no_kv = "000";
                                //if (drGrid.dfltaccounttype != null)
                                //objDVOMasterEmployeeIncomes.acct_no_type = drGrid.dfltaccounttype;

                                objDVOMasterEmployeeIncomes.department = "000";
                                //objDVOMasterEmployeeIncomes.InsertMachineInfo = System.Environment.MachineName;
                                //objDVOMasterEmployeeIncomes.InsertBy = TempData["UserId"] == null ? 0 : Convert.ToInt32(TempData["UserId"]);
                                //objDVOMasterEmployeeIncomes.InsertDate = System.DateTime.Now.ToShortDateString();

                                //add into list
                                db.Entry(objDVOMasterEmployeeIncomes).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }

                    #endregion "Employee Income"

                    return RedirectToAction("IncomeIndex", new { Id = model.Empl_Code });
                }
                else
                {
                    TempData["error"] = "This Income Code Already Exist";
                }
            }
            else
            {
                TempData["error"] = "Please Fill all the Details Before Submitting Page";
            }
            return View();
        }

        private DataTable CreateDataTableForIncome()
        {
            DataTable dtIncome = new DataTable();
            dtIncome.Columns.Add("IncomeCode"); dtIncome.Columns.Add("Rate");
            dtIncome.Columns.Add("Number"); dtIncome.Columns.Add("Amount");
            dtIncome.Columns.Add("Hours"); dtIncome.Columns.Add("Acct_no");
            dtIncome.Columns.Add("lo_inc_amt"); dtIncome.Columns.Add("hi_inc_amt");
            dtIncome.Columns.Add("line_no"); dtIncome.Columns.Add("rowid");//
            dtIncome.Columns.Add("inc_type"); dtIncome.Columns.Add("Keyvalue1");
            dtIncome.Columns.Add("accountid1"); dtIncome.Columns.Add("inc_add_code");
            dtIncome.Columns.Add("inc_edited");
            return dtIncome;
        }

        private DataTable CreateDataTableForDeduction()
        {
            DataTable dtDeduction = new DataTable();
            dtDeduction.Columns.Add("DeductionCode"); dtDeduction.Columns.Add("Rate2");
            dtDeduction.Columns.Add("Amount2"); dtDeduction.Columns.Add("lo_ded_amt");
            dtDeduction.Columns.Add("hi_ded_amt"); dtDeduction.Columns.Add("acct_no2");
            dtDeduction.Columns.Add("line_no2"); dtDeduction.Columns.Add("rowid2");
            dtDeduction.Columns.Add("PayLimit2"); dtDeduction.Columns.Add("Applied2");
            dtDeduction.Columns.Add("RollOver2"); dtDeduction.Columns.Add("Keyvalue2");
            dtDeduction.Columns.Add("Balance"); dtDeduction.Columns.Add("accountid2");
            dtDeduction.Columns.Add("ded_type"); dtDeduction.Columns.Add("ded_taxred");
            dtDeduction.Columns.Add("ded_limit"); dtDeduction.Columns.Add("ded_ytd");
            dtDeduction.Columns.Add("ded_add_code"); dtDeduction.Columns.Add("dedtaxcode");
            dtDeduction.Columns.Add("ded_edited");
            return dtDeduction;
        }

        /// <summary>
        /// Calculate tatals for incomes ,deduction and obligation
        /// </summary>
        /// <param name= pay_code 
        /// Here "I" - Incomes, "D"- Deduction,"O"-Obligation
        /// </param>
        /// 
        private void CalulateTotals(string pay_code)
        {
            switch (pay_code)
            {
                //#region Income
                //case "I":
                //  decimal _totAmount = 0;
                //  decimal _totNumber = 0;
                //  decimal _totHours = 0;

                //  //decimal GrossIncomeChange = 0, TaxableIncomeChange = 0, FicaWagesChange = 0, FutaWagesChange = 0;
                //  decimal IncomeAmount = 0;
                //  string IncomeType = string.Empty;

                //  if (dgvIncome.Rows.Count > 0)
                //    foreach (DataGridViewRow dvr in dgvIncome.Rows)
                //    {
                //      IncomeAmount = 0; //GrossIncomeChange = 0; TaxableIncomeChange = 0; FicaWagesChange = 0; FutaWagesChange = 0;
                //      IncomeType = string.Empty;

                //      if (dvr.Cells["IncomeCode"].EditedFormattedValue != null && dvr.Cells["IncomeCode"].EditedFormattedValue.ToString().Trim().Length > 0)
                //      {
                //        if (dvr.Cells["Amount"].EditedFormattedValue != null && dvr.Cells["Amount"].EditedFormattedValue.ToString().Trim().Length > 0)
                //        {
                //          _totAmount += Convert.ToDecimal(dvr.Cells["Amount"].EditedFormattedValue);
                //          IncomeAmount = Convert.ToDecimal(dvr.Cells["Amount"].EditedFormattedValue);
                //        }
                //        if (dvr.Cells["Number"].EditedFormattedValue != null && dvr.Cells["Number"].EditedFormattedValue.ToString().Trim().Length > 0)
                //          _totNumber += Convert.ToDecimal(dvr.Cells["Number"].EditedFormattedValue);
                //        if (dvr.Cells["Hours"].EditedFormattedValue != null && dvr.Cells["Hours"].EditedFormattedValue.ToString().Trim().Length > 0)
                //          _totHours += Convert.ToDecimal(dvr.Cells["Hours"].EditedFormattedValue);

                //        if (dvr.Cells[6].EditedFormattedValue != null && dvr.Cells[6].EditedFormattedValue.ToString().Trim().Length > 0)
                //          IncomeType = dvr.Cells[6].EditedFormattedValue.ToString().Trim();
                //      }

                //      //BLLPayrollAutopay.CalculateIncomeChanges(IncomeType, IncomeAmount, out GrossIncomeChange, out TaxableIncomeChange, out FicaWagesChange, out FutaWagesChange);
                //      //if (txtGrossWages.Text.Trim().Length > 0)
                //      //{
                //      //    txtGrossWages.Text = String.Format("{0:0.00}", Convert.ToDecimal(txtGrossWages.Text) + GrossIncomeChange);
                //      //    txtNetWages.Text = txtGrossWages.Text;
                //      //    txtCheckAmount.Text = txtGrossWages.Text;
                //      //}
                //      //if (txtTaxableWages.Text.Trim().Length > 0)
                //      //{
                //      //    txtTaxableWages.Text = String.Format("{0:0.00}", Convert.ToDecimal(txtTaxableWages.Text) + TaxableIncomeChange);
                //      //}
                //    }
                //  txtNumberTotals.Text = String.Format("{0:0.00}", _totNumber);
                //  txtAmountTotals.Text = String.Format("{0:0.00}", _totAmount);
                //  txtHoursTotals.Text = String.Format("{0:0.00}", _totHours);
                //  txtHoursWorked.Text = String.Format("{0:0.00}", _totHours);
                //  break;
                //#endregion Income

                //#region Deduction
                //case "D":
                //  decimal _totDedAmount = 0;
                //  if (dgvDeduction.Rows.Count > 0)
                //    foreach (DataGridViewRow dvr in dgvDeduction.Rows)
                //    {
                //      if (dvr.Cells["DeductionCode"].EditedFormattedValue != null && dvr.Cells["DeductionCode"].EditedFormattedValue.ToString().Trim().Length > 0)
                //      {
                //        if (dvr.Cells["Amount2"].EditedFormattedValue != null && dvr.Cells["Amount2"].EditedFormattedValue.ToString().Trim().Length > 0)
                //          _totDedAmount += Convert.ToDecimal(dvr.Cells["Amount2"].EditedFormattedValue);
                //      }
                //    }
                //  txtTotalDedAmount.Text = String.Format("{0:0.00}", _totDedAmount);
                //  break;
                //#endregion Deduction

                //#region Obligation
                //case "O":
                //  decimal _totOblAmount = 0;
                //  if (dgvObligation.Rows.Count > 0)
                //    foreach (DataGridViewRow dvr in dgvObligation.Rows)
                //    {
                //      if (dvr.Cells["ObligationCode"].EditedFormattedValue != null && dvr.Cells["ObligationCode"].EditedFormattedValue.ToString().Trim().Length > 0)
                //      {
                //        if (dvr.Cells["Amount3"].EditedFormattedValue != null && dvr.Cells["Amount3"].EditedFormattedValue.ToString().Trim().Length > 0)
                //          _totOblAmount += Convert.ToDecimal(dvr.Cells["Amount3"].EditedFormattedValue);
                //      }
                //    }
                //  txtTotalOblAomunt.Text = String.Format("{0:0.00}", _totOblAmount);
                //  break;
                //#endregion Obligation
            }
        }

        public ActionResult IncomeAjaxHandler(JQueryDataTableParamModel param, string PensionerID)
        {
            string _accounttype = string.Empty, _accountdescription = string.Empty, _accountkeyvalue = string.Empty;
            int _accounttypeid = 0;

            #region Payroll Incomes

            DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
            objPayrollProcess_PayEmployee.EmplCode = PensionerID;
            DVOPayrollProcess_PayEmployee obj = GetPayrollEntries(objPayrollProcess_PayEmployee);

            //create table to bind with datagridview
            DataTable dtIncome = CreateDataTableForIncome();
            List<DVOPayrollstypayid> ListDVOPayrollstypayid = new List<DVOPayrollstypayid>();
            if (AllFromPayre)
                ListDVOPayrollstypayid = obj.ListDVOPayrollstypayid;
            else
                ListDVOPayrollstypayid = BLLUpdatePayrollEntries.GetPayrollIncomesData(obj.Doc_no);

            if (ListDVOPayrollstypayid.Count > 0)
            {
                foreach (DVOPayrollstypayid ObjIncome in ListDVOPayrollstypayid)
                {
                    DataRow dr = dtIncome.NewRow();
                    dr["IncomeCode"] = ObjIncome.inc_code;
                    dr["Rate"] = ObjIncome.inc_rate != null ? String.Format("{0:0.00000}", ObjIncome.inc_rate) : null;
                    dr["Number"] = ObjIncome.number != null ? String.Format("{0:0.00}", ObjIncome.number) : null;
                    dr["Amount"] = ObjIncome.amount != null ? String.Format("{0:0.00}", ObjIncome.amount) : null;
                    dr["Hours"] = ObjIncome.hours != null ? String.Format("{0:0.00}", ObjIncome.hours) : null;
                    dr["Acct_no"] = ObjIncome.acct_no;
                    dr["lo_inc_amt"] = ObjIncome.lo_inc_amt;
                    dr["hi_inc_amt"] = ObjIncome.hi_inc_amt;
                    dr["line_no"] = ObjIncome.line_no;
                    dr["rowid"] = ObjIncome.RowID;
                    dr["inc_type"] = ObjIncome.inc_type.Trim();

                    if (ObjIncome.acct_no > 0)
                    {
                        BLLCommonUtilities.GetAccountInformation(ObjIncome.acct_no, out _accountkeyvalue, out _accounttype, out _accounttypeid, out _accountdescription);
                        dr["Keyvalue1"] = _accountkeyvalue;
                        dr["accountid1"] = _accounttypeid;
                    }

                    dtIncome.Rows.Add(dr);
                    dr = null;
                }
                dtIncome.AcceptChanges();
                if (dtIncome.Rows.Count > 0)
                {

                }
                //Calculate Amount Total ,Number Totals and Hours Totals
                CalulateTotals("I");
            }
            else
            {


            }


            #endregion

            var List = ListDVOPayrollstypayid;
            ListDVOPayrollstypayid = null;
            IEnumerable<DVOPayrollstypayid> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.inc_code.ToLower().Contains(param.sSearch.ToLower())
                   || c.inc_rate.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.number.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.amount.ToString().ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<DVOPayrollstypayid, string> orderingFunction = (c => sortColumnIndex == 0 ? c.inc_code :
                                                                                            sortColumnIndex == 1 ? c.inc_rate + "" :
                                                                                             sortColumnIndex == 2 ? c.number + "" :
                                                                                              sortColumnIndex == 3 ? c.amount + "" :

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
                                             c.inc_rate+"",
                         c.number+"",
                         c.amount+"",
                         c.RowID+""
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
        bool AllFromPayre;
        public ActionResult DeductionAjaxHandler(JQueryDataTableParamModel param, string PensionerID)
        {
            string _accounttype = string.Empty, _accountdescription = string.Empty, _accountkeyvalue = string.Empty;
            int _accounttypeid = 0;

            #region Payroll Deduction
            DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
            objPayrollProcess_PayEmployee.EmplCode = PensionerID;
            DVOPayrollProcess_PayEmployee obj = GetPayrollEntries(objPayrollProcess_PayEmployee);

            //create table to bind with datagridview
            DataTable dtDeduction = CreateDataTableForDeduction();

            List<DVOPayrollstypaydd> ListDVOPayrollstypaydd = new List<DVOPayrollstypaydd>();
            if (AllFromPayre)
                ListDVOPayrollstypaydd = obj.ListDVOPayrollstypaydd;
            else
                ListDVOPayrollstypaydd = BLLUpdatePayrollEntries.GetPayrollDeductionData(obj.Doc_no);

            if (ListDVOPayrollstypaydd.Count > 0)
            {
                foreach (DVOPayrollstypaydd ObjDeduction in ListDVOPayrollstypaydd)
                {
                    DataRow dr = dtDeduction.NewRow();
                    dr["DeductionCode"] = ObjDeduction.ded_code;
                    dr["Rate2"] = ObjDeduction.ded_rate != null ? String.Format("{0:0.00}", ObjDeduction.ded_rate) : null;
                    dr["Amount2"] = ObjDeduction.amount != null ? String.Format("{0:0.00}", ObjDeduction.amount) : null;
                    dr["lo_ded_amt"] = ObjDeduction.lo_ded_amt != null ? ObjDeduction.lo_ded_amt : null;
                    dr["hi_ded_amt"] = ObjDeduction.hi_ded_amt != null ? ObjDeduction.hi_ded_amt : null;
                    dr["acct_no2"] = ObjDeduction.acct_no;
                    dr["line_no2"] = ObjDeduction.line_no;
                    dr["rowid2"] = ObjDeduction.RowID;
                    dr["PayLimit2"] = ObjDeduction.pay_limit != null ? String.Format("{0:0.00}", ObjDeduction.pay_limit) : null;
                    //dr["Applied2"] = ObjDeduction.line_no;
                    dr["RollOver2"] = ObjDeduction.yearrollover;

                    if (ObjDeduction.acct_no > 0)
                    {
                        BLLCommonUtilities.GetAccountInformation(ObjDeduction.acct_no, out _accountkeyvalue, out _accounttype, out _accounttypeid, out _accountdescription);
                        dr["Keyvalue2"] = _accountkeyvalue;
                        dr["accountid2"] = _accounttypeid;
                    }

                    dr["ded_type"] = ObjDeduction.ded_type.Trim();
                    dr["ded_taxred"] = ObjDeduction.ded_taxred.Trim();
                    dr["ded_limit"] = ObjDeduction.ded_limit != null ? ObjDeduction.ded_limit : null;
                    dr["ded_ytd"] = ObjDeduction.ded_ytd != null ? ObjDeduction.ded_ytd : null;
                    dtDeduction.Rows.Add(dr);
                    dr = null;
                }
                dtDeduction.AcceptChanges();
                if (dtDeduction.Rows.Count > 0)
                {
                    //dgvDeduction.DataSource = dtDeduction;
                }
                //Calculate Amount Total
                CalulateTotals("D");
            }
            else
            {

            }

            #endregion
            TempData["ListDVOPayrollstypaydd"] = ListDVOPayrollstypaydd;
            var List = ListDVOPayrollstypaydd;
            ListDVOPayrollstypaydd = null;
            IEnumerable<DVOPayrollstypaydd> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.ded_code.ToLower().Contains(param.sSearch.ToLower())
                   || c.ded_rate.ToString().Contains(param.sSearch.ToLower())
                   || c.amount.ToString().Contains(param.sSearch.ToLower())).ToList();

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<DVOPayrollstypaydd, string> orderingFunction = (c => sortColumnIndex == 0 ? c.ded_code :
                                                                                            sortColumnIndex == 1 ? c.ded_rate + "" :
                                                                                             sortColumnIndex == 2 ? c.amount + "" :
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
                         c.ded_code,
                                             c.ded_rate+"",
                         c.amount+"",
                         c.RowID+""
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

        /// <summary>
        /// To Again-Search required data from database based on search criteria entered by user
        /// </summary>
        private DVOMasterEmployee SearchAgainEmployeeInfo(string EmplCode)
        {
            DVOMasterEmployee objtemp = new DVOMasterEmployee();
            objtemp.EmplCode = EmplCode;// listSearchResultDVOMasterEmployee[CurrentRecordIndex].EmplCode;
            List<DVOMasterEmployee> listAgainSearchEmpInfo = BLLMasterEmployee.GetData(ref objtemp);
            objtemp = null;
            //get data from database and assign to list of objects
            if (listAgainSearchEmpInfo.Count > 0)
            {
                return listAgainSearchEmpInfo[0];
            }
            else return new DVOMasterEmployee();
        }

        public Dictionary<string, char> dedFrequency()
        {
            Dictionary<string, char> Frequency = new Dictionary<string, char>();
            Frequency.Add("Weekly", 'W');
            Frequency.Add("Monthly", 'M');
            Frequency.Add("Yearly", 'Y');
            return Frequency;
        }

        public Dictionary<string, char> YesNo()
        {
            Dictionary<string, char> YesNo = new Dictionary<string, char>();
            YesNo.Add("Yes", 'Y');
            YesNo.Add("No", 'N');
            return YesNo;
        }

        public Dictionary<string, char> OkToPost()
        {
            Dictionary<string, char> OkToPost = new Dictionary<string, char>();
            OkToPost.Add("Y", 'Y');
            OkToPost.Add("C", 'C');
            OkToPost.Add("P", 'P');
            return OkToPost;
        }

        [HttpGet]
        public ActionResult EditDeductionDetails(int? Id, string pensionerID)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<DVOPayrollstypaydd> model = TempData["ListDVOPayrollstypaydd"] != null ? (List<DVOPayrollstypaydd>)TempData["ListDVOPayrollstypaydd"] : new List<DVOPayrollstypaydd>();

            if (model == null || model.Count == 0)
            {
                return HttpNotFound();
            }
            DVOPayrollstypaydd objDVOPayrollstypaydd = model.Where(x => x.RowID == Id).FirstOrDefault();
            string Empl_Code = Request.Form["PensionerID"];
            @ViewBag.Id = Request.Form["CId"];
            DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(Empl_Code);
            ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
            ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
            ViewBag.CId = Id;
            var dedfrequency = from x in dedFrequency()
                               select new { Id = x.Value, Name = x.Key };
            ViewBag.ded_apply = new SelectList(dedfrequency.OrderBy(x => x.Name), "Id", "Name", objDVOPayrollstypaydd.ded_type);
            ViewBag.ded_code = new SelectList(db.MasterDedcodes.OrderBy(x => x.description), "ded_code", "description", objDVOPayrollstypaydd.ded_code);
            return PartialView("~/Views/PensionProcess/GeneratePensions/EditDeductionDetails.cshtml", objDVOPayrollstypaydd);
        }

        [HttpPost]
        public ActionResult EditDeductionDetails(DVOPayrollstypaydd model)
        {
            string Empl_Code = Request.Form["PensionerID"];
            int id = model.RowID;
            int userid = AppUserManager.GetUserId();
            ViewBag.PensionerID = Empl_Code;
            ViewBag.ded_code = new SelectList(db.MasterDedcodes.OrderBy(x => x.description), "ded_code", "description", model.ded_code);
            ViewBag.CId = Empl_Code;
            if (ModelState.IsValid && model != null)
            {


            }
            var dedfrequency = from x in dedFrequency()
                               select new { Id = x.Value, Name = x.Key };
            //ViewBag.ded_apply = new SelectList(dedfrequency.OrderBy(x => x.Name), "Id", "Name", model.ded_apply);

            return PartialView("~/Views/PensionProcess/GeneratePensions/UpdatePayrollEntries.cshtml");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePayrollEntries(DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployee, FormCollection form)
        {

            Session["NewUpdateEntries"] = objPayrollProcess_PayEmployee;
            //DVOPayrollProcess_PayEmployee OldDVOPayrollProcess_PayEmployee = TempData["PayrollProcess_PayEmployee"] != null ? (DVOPayrollProcess_PayEmployee)TempData["PayrollProcess_PayEmployee"] : new DVOPayrollProcess_PayEmployee();
            DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(objPayrollProcess_PayEmployee.EmplCode);//to get employer details
            ViewBag.EmployerId = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true), "Id", "EmployerName", objDVOMasterEmployee.EmplrCode);
            int UserId = AppUserManager.GetUserId();
            int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            var result = BLLMasterEmployee.GetAllData(UserId, RoleId).Where(x => x.EmplrCode == objDVOMasterEmployee.EmplrCode).Select(x => new { Id = x.EmplCode, Name = x.FirstName + " " + x.MiddleName + " " + x.LastName }).ToList();
            ViewBag.EmployeeId = new SelectList(result, "Id", "Name", objPayrollProcess_PayEmployee.EmplCode);

            //List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = new List<DVOMasterEmployee>();
            //var details = listSearchResultDVOMasterEmployee.Select(x => new { x.EmplCode, Name = x.FirstName + " " + x.MiddleName + " " + x.LastName }).ToList();
            //ViewBag.EmployeeId = new SelectList(listSearchResultDVOMasterEmployee, "EmplCode", "Name", objPayrollProcess_PayEmployee.EmplCode);

            //DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployees = new DVOPayrollProcess_PayEmployee();
            //objPayrollProcess_PayEmployees.EmplCode = "100001";
            //objPayrollProcess_PayEmployees = GetPayrollEntries(objPayrollProcess_PayEmployees);
            //Session["OldUpdateEntries"] = OldDVOPayrollProcess_PayEmployee;

            ViewBag.PensionerID = objPayrollProcess_PayEmployee.EmplCode;
            ViewBag.Name = String.Format("{0} {1} {2}", objPayrollProcess_PayEmployee.FirstName, objPayrollProcess_PayEmployee.MiddleName, objPayrollProcess_PayEmployee.LastName);
            ViewBag.CId = objPayrollProcess_PayEmployee.EmplCode;
            @ViewBag.Id = objPayrollProcess_PayEmployee.Doc_no;

            var yesNo = from x in YesNo()
                        select new { Id = x.Value, Name = x.Key };
            ViewBag.YesNo = new SelectList(yesNo.OrderBy(x => x.Name), "Id", "Name");
            var okToPost = from x in OkToPost()
                           select new { Id = x.Value, Name = x.Key };
            ViewBag.OkToPost = new SelectList(okToPost.OrderBy(x => x.Name), "Id", "Name");

            var Frequency = from x in dedFrequency()
                            select new { Id = x.Value, Name = x.Key };
            ViewBag.Frequency = new SelectList(Frequency.OrderBy(x => x.Name), "Id", "Name");
            try
            {
                return RedirectToAction("IncomeIndex", new { id = objPayrollProcess_PayEmployee.EmplCode });
            }
            catch (Exception ex)
            {
                return PartialView("~/Views/PensionProcess/GeneratePensions/UpdatePayrollEntries.cshtml", objPayrollProcess_PayEmployee);
            }
        }



        public JsonResult ProcessAjax(string PId)
        {
            DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
            objPayrollProcess_PayEmployee.EmplCode = PId;
            objPayrollProcess_PayEmployee = GetPayrollEntries(objPayrollProcess_PayEmployee);
            return Json(objPayrollProcess_PayEmployee, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// To Update selected Record after search
        /// </summary>
        private void UpdateEmployeeInformation()
        {
            bool Success = false;
            int userid = AppUserManager.GetUserId();
            try
            {

                #region Payroll Information
                //make an object with values of appropriate properties to insert into database
                DVOPayrollProcess_PayEmployee objUpdDVOPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                /*objUpdDVOPayrollProcess_PayEmployee.EmplCode = stxtEmplCode.SelectedValue.Trim();// txtEmplCode.Text;
                if (txtDocNo.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.Doc_no = Convert.ToInt32(txtDocNo.Text);
                if (dtpPayrollDate.Checked) objUpdDVOPayrollProcess_PayEmployee.pay_date = dtpPayrollDate.Value;
                if (dtpEndingDate.Checked) objUpdDVOPayrollProcess_PayEmployee.eop_date = dtpEndingDate.Value;
                objUpdDVOPayrollProcess_PayEmployee.print_check = txtPrintCheck.Text;
                if (txtCheckNumber.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.check_no = Convert.ToInt32(txtCheckNumber.Text);
                if (mcgCashAccount.SelectedIndex > 0) objUpdDVOPayrollProcess_PayEmployee.Cash_acct_no = Convert.ToInt32(mcgCashAccount.SelectedValue);
                objUpdDVOPayrollProcess_PayEmployee.accrue_sick = txtAccrueSick.Text;
                objUpdDVOPayrollProcess_PayEmployee.accrue_vac = txtAccureVacation.Text;
                objUpdDVOPayrollProcess_PayEmployee.bonus = txtBonusCheck.Text;
                objUpdDVOPayrollProcess_PayEmployee.deposit = txtDeposit.Text;
                objUpdDVOPayrollProcess_PayEmployee.RowID = ListDVOPayrollProcess_PayEmployee[CurrentRecordIndex].RowID;
                objUpdDVOPayrollProcess_PayEmployee.doc_date = ListDVOPayrollProcess_PayEmployee[CurrentRecordIndex].doc_date;
                objUpdDVOPayrollProcess_PayEmployee.Department = "000";
                objUpdDVOPayrollProcess_PayEmployee.ok_to_post = txtOkToPast.Text;
                if (txtCheckAmount.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.cash_amount = Convert.ToDecimal(txtCheckAmount.Text);
                if (txtGrossWages.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.inc_gross = Convert.ToDecimal(txtGrossWages.Text);
                if (txtEmployeeFICA.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.ded_fica = Convert.ToDecimal(txtEmployeeFICA.Text);
                if (txtTaxableWages.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.inc_taxable = Convert.ToDecimal(txtTaxableWages.Text);
                if (txtEmployeeMedicare.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.ded_medicare = Convert.ToDecimal(txtEmployeeMedicare.Text);
                if (txtFederalIncomeTax.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.ded_fedtax = Convert.ToDecimal(txtFederalIncomeTax.Text);
                if (txtStateIncomeTax.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.ded_statax = Convert.ToDecimal(txtStateIncomeTax.Text);
                if (txtLocalTaxes.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.ded_loctax = Convert.ToDecimal(txtLocalTaxes.Text);
                if (txtOtherDeductions.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.ded_other = Convert.ToDecimal(txtOtherDeductions.Text);
                if (txtFUTALiability.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.obl_futa = Convert.ToDecimal(txtFUTALiability.Text);
                if (txtFICALiability.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.obl_fica = Convert.ToDecimal(txtFICALiability.Text);
                if (txtMedicareLiability.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.obl_medicare = Convert.ToDecimal(txtMedicareLiability.Text);
                if (txtOtherLiabilities.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.obl_other = Convert.ToDecimal(txtOtherLiabilities.Text);
                if (txtTotalLiabilities.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.obl_total = Convert.ToDecimal(txtTotalLiabilities.Text);
                if (txtNetWages.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.inc_net = Convert.ToDecimal(txtNetWages.Text);
                if (txtExpAdvn.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.inc_expense = Convert.ToDecimal(txtExpAdvn.Text);
                if (txtHoursWorked.Text.Trim().Length > 0) objUpdDVOPayrollProcess_PayEmployee.total_hours = Convert.ToDecimal(txtHoursWorked.Text);*/
                objUpdDVOPayrollProcess_PayEmployee.UpdateBy = userid;
                objUpdDVOPayrollProcess_PayEmployee.UpdateDate = DateTime.Now.ToShortDateString();
                objUpdDVOPayrollProcess_PayEmployee.UpdateMachineInfo = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);


                #endregion Payroll Information

                #region Payroll Incomes

                //make list of detail-objects of main object
                List<DVOPayrollstypayid> objUpdatelistDVOPayrollstypayid = new List<DVOPayrollstypayid>();
                List<DVOPayrollstypayid> objNewlistDVOPayrollstypayid = new List<DVOPayrollstypayid>();
                List<DVOPayrollstypayid> objDeletelistDVOPayrollstypayid = new List<DVOPayrollstypayid>();

                /*if (dgvIncome.DataSource is DataTable)
                {
                  DataTable dtincome = (DataTable)dgvIncome.DataSource;
                  foreach (DataRow dr in dtincome.Rows)
                  {
                    if (dr.RowState == DataRowState.Deleted)
                    {
                      if (dr["rowid", DataRowVersion.Original] != DBNull.Value && dr["rowid"].ToString().Trim().Length > 0)
                      {
                        using (DVOPayrollstypayid objDVOPayrollstypayid = new DVOPayrollstypayid())
                        {
                          objDVOPayrollstypayid.Doc_no = objUpdDVOPayrollProcess_PayEmployee.Doc_no;
                          objDVOPayrollstypayid.RowID = Convert.ToInt32(dr["rowid"]);
                          objDeletelistDVOPayrollstypayid.Add(objDVOPayrollstypayid);
                        }
                      }
                    }
                    else if (dr.RowState == DataRowState.Added)
                    {
                      if (dr["IncomeCode"] != DBNull.Value && dr["IncomeCode"].ToString().Trim().Length > 0)
                      {
                        using (DVOPayrollstypayid objDVOPayrollstypayid = new DVOPayrollstypayid())
                        {
                          //assign appropriate values to detail-object
                          objDVOPayrollstypayid.Doc_no = objUpdDVOPayrollProcess_PayEmployee.Doc_no;
                          objDVOPayrollstypayid.inc_code = dr["IncomeCode"].ToString().Trim();
                          if (dr["Rate"] != DBNull.Value && dr["Rate"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.inc_rate = Convert.ToDecimal(dr["Rate"]);
                          if (dr["Number"] != DBNull.Value && dr["Number"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.number = Convert.ToDecimal(dr["Number"]);
                          if (dr["Amount"] != DBNull.Value && dr["Amount"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.amount = Convert.ToDecimal(dr["Amount"]);
                          if (dr["Hours"] != DBNull.Value && dr["Hours"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.hours = Convert.ToDecimal(dr["Hours"]);
                          if (dr["Acct_no"] != DBNull.Value && dr["Acct_no"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.acct_no = Convert.ToInt32(dr["Acct_no"]);
                          if (dr["lo_inc_amt"] != DBNull.Value && dr["lo_inc_amt"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.lo_inc_amt = Convert.ToDecimal(dr["lo_inc_amt"]);
                          if (dr["hi_inc_amt"] != DBNull.Value && dr["hi_inc_amt"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.hi_inc_amt = Convert.ToDecimal(dr["hi_inc_amt"]);
                          if (dr["inc_add_code"] != DBNull.Value)
                            objDVOPayrollstypayid.add_code = dr["inc_add_code"].ToString().Trim();

                          objDVOPayrollstypayid.InsertMachineInfo = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                          objDVOPayrollstypayid.InsertBy = userid;
                          objDVOPayrollstypayid.InsertDate = DateTime.Now.ToShortDateString();

                          objDVOPayrollstypayid.UpdateMachineInfo = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                          objDVOPayrollstypayid.UpdateBy = userid;
                          objDVOPayrollstypayid.UpdateDate = DateTime.Now.ToShortDateString();

                          //add into list
                          objNewlistDVOPayrollstypayid.Add(objDVOPayrollstypayid);
                        }
                      }
                    }
                    else //if (dr.RowState == DataRowState.Modified)
                    {
                      bool _rowDelete = true;
                      if (dr["IncomeCode"] != DBNull.Value && dr["IncomeCode"].ToString().Trim().Length > 0)
                      {
                        _rowDelete = false;
                        using (DVOPayrollstypayid objDVOPayrollstypayid = new DVOPayrollstypayid())
                        {
                          //assign appropriate values to detail-object
                          objDVOPayrollstypayid.Doc_no = objUpdDVOPayrollProcess_PayEmployee.Doc_no;
                          objDVOPayrollstypayid.inc_code = dr["IncomeCode"].ToString().Trim();
                          if (dr["Rate"] != DBNull.Value && dr["Rate"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.inc_rate = Convert.ToDecimal(dr["Rate"]);
                          if (dr["Number"] != DBNull.Value && dr["Number"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.number = Convert.ToDecimal(dr["Number"]);
                          if (dr["Amount"] != DBNull.Value && dr["Amount"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.amount = Convert.ToDecimal(dr["Amount"]);
                          if (dr["Hours"] != DBNull.Value && dr["Hours"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.hours = Convert.ToDecimal(dr["Hours"]);
                          if (dr["Acct_no"] != DBNull.Value && dr["Acct_no"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.acct_no = Convert.ToInt32(dr["Acct_no"]);
                          if (dr["line_no"] != DBNull.Value && dr["line_no"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.line_no = Convert.ToInt32(dr["line_no"]);
                          if (dr["rowid"] != DBNull.Value && dr["rowid"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.RowID = Convert.ToInt32(dr["rowid"]);
                          if (dr["lo_inc_amt"] != DBNull.Value && dr["lo_inc_amt"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.lo_inc_amt = Convert.ToDecimal(dr["lo_inc_amt"]);
                          if (dr["hi_inc_amt"] != DBNull.Value && dr["hi_inc_amt"].ToString().Trim().Length > 0)
                            objDVOPayrollstypayid.hi_inc_amt = Convert.ToDecimal(dr["hi_inc_amt"]);
                          if (dr["inc_add_code"] != DBNull.Value)
                            objDVOPayrollstypayid.add_code = dr["inc_add_code"].ToString().Trim();

                          objDVOPayrollstypayid.InsertMachineInfo = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                          objDVOPayrollstypayid.InsertBy = userid;
                          objDVOPayrollstypayid.InsertDate = DateTime.Now.ToShortDateString();

                          objDVOPayrollstypayid.UpdateMachineInfo = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                          objDVOPayrollstypayid.UpdateBy = userid;
                          objDVOPayrollstypayid.UpdateDate = DateTime.Now.ToShortDateString();

                          //add into list
                          objUpdatelistDVOPayrollstypayid.Add(objDVOPayrollstypayid);
                        }
                      }
                      if (dr["rowid"] != DBNull.Value && dr["rowid"].ToString().Trim().Length > 0)
                        if (_rowDelete)
                        {
                          using (DVOPayrollstypayid objDVOPayrollstypayid = new DVOPayrollstypayid())
                          {
                            objDVOPayrollstypayid.Doc_no = objUpdDVOPayrollProcess_PayEmployee.Doc_no;
                            objDVOPayrollstypayid.RowID = Convert.ToInt32(dr["rowid"]);
                            objDeletelistDVOPayrollstypayid.Add(objDVOPayrollstypayid);
                          }
                        }
                    }
                  }
                }*/

                #endregion

                #region Payroll Deduction

                //make list of detail-objects of main object
                List<DVOPayrollstypaydd> listNewDVOPayrollstypaydd = new List<DVOPayrollstypaydd>();
                List<DVOPayrollstypaydd> listUpdateDVOPayrollstypaydd = new List<DVOPayrollstypaydd>();
                List<DVOPayrollstypaydd> listDeleteDVOPayrollstypaydd = new List<DVOPayrollstypaydd>();

                /*if (dgvDeduction.DataSource is DataTable)
                {
                  DataTable dtdeduction = (DataTable)dgvDeduction.DataSource;
                  foreach (DataRow dr in dtdeduction.Rows)
                  {
                    if (dr.RowState == DataRowState.Deleted)
                    {
                      if (dr["rowid2", DataRowVersion.Original] != DBNull.Value && dr["rowid2", DataRowVersion.Original].ToString().Trim().Length > 0)
                      {
                        using (DVOPayrollstypaydd objDVOPayrollstypaydd = new DVOPayrollstypaydd())
                        {
                          objDVOPayrollstypaydd.Doc_no = objUpdDVOPayrollProcess_PayEmployee.Doc_no;
                          objDVOPayrollstypaydd.RowID = Convert.ToInt32(dr["rowid2", DataRowVersion.Original]);
                          listDeleteDVOPayrollstypaydd.Add(objDVOPayrollstypaydd);
                        }
                      }
                    }
                    else if (dr.RowState == DataRowState.Added)
                    {
                      if (dr["DeductionCode"] != DBNull.Value && dr["DeductionCode"].ToString().Trim().Length > 0)
                      {
                        using (DVOPayrollstypaydd objDVOPayrollstypaydd = new DVOPayrollstypaydd())
                        {
                          //assign appropriate values to detail-object
                          objDVOPayrollstypaydd.Doc_no = objUpdDVOPayrollProcess_PayEmployee.Doc_no;
                          objDVOPayrollstypaydd.ded_code = dr["DeductionCode"].ToString().Trim();
                          if (dr["Rate2"] != DBNull.Value && dr["Rate2"].ToString().Trim().Length > 0)
                            objDVOPayrollstypaydd.ded_rate = Convert.ToDecimal(dr["Rate2"]);
                          if (dr["Amount2"] != DBNull.Value && dr["Amount2"].ToString().Trim().Length > 0)
                            objDVOPayrollstypaydd.amount = Convert.ToDecimal(dr["Amount2"]);
                          if (dr["lo_ded_amt"] != DBNull.Value && dr["lo_ded_amt"].ToString().Trim().Length > 0)
                            objDVOPayrollstypaydd.lo_ded_amt = Convert.ToDecimal(dr["lo_ded_amt"]);
                          if (dr["hi_ded_amt"] != DBNull.Value && dr["hi_ded_amt"].ToString().Trim().Length > 0)
                            objDVOPayrollstypaydd.hi_ded_amt = Convert.ToDecimal(dr["hi_ded_amt"]);
                          if (dr["acct_no2"] != DBNull.Value && dr["acct_no2"].ToString().Trim().Length > 0)
                            objDVOPayrollstypaydd.acct_no = Convert.ToInt32(dr["acct_no2"]);
                          if (dr["ded_add_code"] != DBNull.Value)
                            objDVOPayrollstypaydd.add_code = dr["ded_add_code"].ToString().Trim();

                          objDVOPayrollstypaydd.InsertMachineInfo = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                          objDVOPayrollstypaydd.InsertBy = userid;
                          objDVOPayrollstypaydd.InsertDate = DateTime.Now.ToShortDateString();

                          objDVOPayrollstypaydd.UpdateMachineInfo = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                          objDVOPayrollstypaydd.UpdateBy = userid;
                          objDVOPayrollstypaydd.UpdateDate = DateTime.Now.ToShortDateString();

                          //add into list
                          listNewDVOPayrollstypaydd.Add(objDVOPayrollstypaydd);
                        }
                      }
                    }
                    else //if (dr.RowState == DataRowState.Modified)
                    {
                      bool _rowDelete = true;
                      if (dr["DeductionCode"] != DBNull.Value && dr["DeductionCode"].ToString().Trim().Length > 0)
                      {
                        _rowDelete = false;
                        using (DVOPayrollstypaydd objDVOPayrollstypaydd = new DVOPayrollstypaydd())
                        {
                          //assign appropriate values to detail-object
                          objDVOPayrollstypaydd.Doc_no = objUpdDVOPayrollProcess_PayEmployee.Doc_no;
                          objDVOPayrollstypaydd.ded_code = dr["DeductionCode"].ToString().ToString();
                          if (dr["Rate2"] != DBNull.Value && dr["Rate2"].ToString().Trim().Length > 0)
                            objDVOPayrollstypaydd.ded_rate = Convert.ToDecimal(dr["Rate2"]);
                          if (dr["Amount2"] != DBNull.Value && dr["Amount2"].ToString().Trim().Length > 0)
                            objDVOPayrollstypaydd.amount = Convert.ToDecimal(dr["Amount2"]);
                          if (dr["lo_ded_amt"] != DBNull.Value && dr["lo_ded_amt"].ToString().Trim().Length > 0)
                            objDVOPayrollstypaydd.lo_ded_amt = Convert.ToDecimal(dr["lo_ded_amt"]);
                          if (dr["hi_ded_amt"] != DBNull.Value && dr["hi_ded_amt"].ToString().Trim().Length > 0)
                            objDVOPayrollstypaydd.hi_ded_amt = Convert.ToDecimal(dr["hi_ded_amt"]);
                          if (dr["acct_no2"] != DBNull.Value && dr["acct_no2"].ToString().Trim().Length > 0)
                            objDVOPayrollstypaydd.acct_no = Convert.ToInt32(dr["acct_no2"]);
                          if (dr["line_no2"] != DBNull.Value && dr["line_no2"].ToString().Trim().Length > 0)
                            objDVOPayrollstypaydd.line_no = Convert.ToInt32(dr["line_no2"]);
                          if (dr["rowid2"] != DBNull.Value && dr["rowid2"].ToString().Trim().Length > 0)
                            objDVOPayrollstypaydd.RowID = Convert.ToInt32(dr["rowid2"]);
                          if (dr["ded_add_code"] != DBNull.Value)
                            objDVOPayrollstypaydd.add_code = dr["ded_add_code"].ToString().Trim();

                          objDVOPayrollstypaydd.InsertMachineInfo = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                          objDVOPayrollstypaydd.InsertBy = userid;
                          objDVOPayrollstypaydd.InsertDate = DateTime.Now.ToShortDateString();

                          objDVOPayrollstypaydd.UpdateMachineInfo = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                          objDVOPayrollstypaydd.UpdateBy = userid;
                          objDVOPayrollstypaydd.UpdateDate = DateTime.Now.ToShortDateString();

                          //add into list
                          listUpdateDVOPayrollstypaydd.Add(objDVOPayrollstypaydd);
                        }
                      }

                      if (dr["rowid2"] != DBNull.Value && dr["rowid2"].ToString().Trim().Length > 0)
                        if (_rowDelete)
                        {
                          using (DVOPayrollstypaydd objDVOPayrollstypaydd = new DVOPayrollstypaydd())
                          {
                            objDVOPayrollstypaydd.Doc_no = objUpdDVOPayrollProcess_PayEmployee.Doc_no;
                            objDVOPayrollstypaydd.RowID = Convert.ToInt32(dr["rowid2"]);
                            listDeleteDVOPayrollstypaydd.Add(objDVOPayrollstypaydd);
                          }
                        }
                    }
                  }
                }*/

                #endregion Payroll Deduction

                #region "Payroll Obligation"

                //make list of detail-objects of main object
                List<DVOPayrollStypayod> listUpdateDVOPayrollStypayod = new List<DVOPayrollStypayod>();
                List<DVOPayrollStypayod> listNewDVOPayrollStypayod = new List<DVOPayrollStypayod>();
                List<DVOPayrollStypayod> listDeleteDVOPayrollStypayod = new List<DVOPayrollStypayod>();

                /*if (dgvObligation.DataSource is DataTable)
                {
                  DataTable dtobligation = (DataTable)dgvObligation.DataSource;
                  foreach (DataRow dr in dtobligation.Rows)
                  {
                    if (dr.RowState == DataRowState.Deleted)
                    {
                      if (dr["rowid3", DataRowVersion.Original] != DBNull.Value && dr["rowid3", DataRowVersion.Original].ToString().Trim().Length > 0)
                      {
                        using (DVOPayrollStypayod objDVOPayrollStypayod = new DVOPayrollStypayod())
                        {
                          objDVOPayrollStypayod.Doc_no = objUpdDVOPayrollProcess_PayEmployee.Doc_no;
                          objDVOPayrollStypayod.RowID = Convert.ToInt32(dr["rowid3", DataRowVersion.Original]);
                          listDeleteDVOPayrollStypayod.Add(objDVOPayrollStypayod);
                        }
                      }
                    }
                    else if (dr.RowState == DataRowState.Added)
                    {
                      if (dr["ObligationCode"] != DBNull.Value && dr["ObligationCode"].ToString().Trim().Length > 0)
                      {
                        using (DVOPayrollStypayod objDVOPayrollStypayod = new DVOPayrollStypayod())
                        {
                          //assign appropriate values to detail-object
                          objDVOPayrollStypayod.Doc_no = objUpdDVOPayrollProcess_PayEmployee.Doc_no;
                          objDVOPayrollStypayod.obl_code = dr["ObligationCode"].ToString().Trim();
                          if (dr["Rate3"] != DBNull.Value && dr["Rate3"].ToString().Trim().Length > 0)
                            objDVOPayrollStypayod.obl_rate = Convert.ToDecimal(dr["Rate3"]);
                          if (dr["Amount3"] != DBNull.Value && dr["Amount3"].ToString().Trim().Length > 0)
                            objDVOPayrollStypayod.amount = Convert.ToDecimal(dr["Amount3"]);
                          if (dr["ExpenseAccount3"] != DBNull.Value && dr["ExpenseAccount3"].ToString().Trim().Length > 0)
                            objDVOPayrollStypayod.acct_no = Convert.ToInt32(dr["ExpenseAccount3"]);
                          if (dr["LiabilityAccount3"] != DBNull.Value && dr["LiabilityAccount3"].ToString().Trim().Length > 0)
                            objDVOPayrollStypayod.bal_acct_no = Convert.ToInt32(dr["LiabilityAccount3"]);
                          if (dr["obl_add_code"] != DBNull.Value)
                            objDVOPayrollStypayod.add_code = dr["obl_add_code"].ToString().Trim();

                          objDVOPayrollStypayod.InsertMachineInfo = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                          objDVOPayrollStypayod.InsertBy = userid;
                          objDVOPayrollStypayod.InsertDate = DateTime.Now.ToShortDateString();

                          objDVOPayrollStypayod.UpdateMachineInfo = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                          objDVOPayrollStypayod.UpdateBy = userid;
                          objDVOPayrollStypayod.UpdateDate = DateTime.Now.ToShortDateString();

                          //add into list
                          listNewDVOPayrollStypayod.Add(objDVOPayrollStypayod);
                        }
                      }
                    }
                    else //if (dr.RowState == DataRowState.Modified)
                    {
                      bool _rowDelete = true;
                      if (dr["ObligationCode"] != DBNull.Value && dr["ObligationCode"].ToString().Trim().Length > 0)
                      {
                        _rowDelete = false;
                        using (DVOPayrollStypayod objDVOPayrollStypayod = new DVOPayrollStypayod())
                        {
                          //assign appropriate values to detail-object
                          objDVOPayrollStypayod.Doc_no = objUpdDVOPayrollProcess_PayEmployee.Doc_no;
                          objDVOPayrollStypayod.obl_code = dr["ObligationCode"].ToString().Trim();
                          if (dr["Rate3"] != DBNull.Value && dr["Rate3"].ToString().Trim().Length > 0)
                            objDVOPayrollStypayod.obl_rate = Convert.ToDecimal(dr["Rate3"]);
                          if (dr["Amount3"] != DBNull.Value && dr["Amount3"].ToString().Trim().Length > 0)
                            objDVOPayrollStypayod.amount = Convert.ToDecimal(dr["Amount3"]);
                          if (dr["ExpenseAccount3"] != DBNull.Value && dr["ExpenseAccount3"].ToString().Trim().Length > 0)
                            objDVOPayrollStypayod.acct_no = Convert.ToInt32(dr["ExpenseAccount3"]);
                          if (dr["LiabilityAccount3"] != DBNull.Value && dr["LiabilityAccount3"].ToString().Trim().Length > 0)
                            objDVOPayrollStypayod.bal_acct_no = Convert.ToInt32(dr["LiabilityAccount3"]);
                          if (dr["line_no3"] != DBNull.Value && dr["line_no3"].ToString().Trim().Length > 0)
                            objDVOPayrollStypayod.line_no = Convert.ToInt32(dr["line_no3"]);
                          if (dr["rowid3"] != DBNull.Value && dr["rowid3"].ToString().Trim().Length > 0)
                            objDVOPayrollStypayod.RowID = Convert.ToInt32(dr["rowid3"]);
                          if (dr["obl_add_code"] != DBNull.Value)
                            objDVOPayrollStypayod.add_code = dr["obl_add_code"].ToString().Trim();

                          objDVOPayrollStypayod.InsertMachineInfo = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                          objDVOPayrollStypayod.InsertBy = userid;
                          objDVOPayrollStypayod.InsertDate = DateTime.Now.ToShortDateString();

                          objDVOPayrollStypayod.UpdateMachineInfo = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                          objDVOPayrollStypayod.UpdateBy = userid;
                          objDVOPayrollStypayod.UpdateDate = DateTime.Now.ToShortDateString();

                          //add into list
                          listUpdateDVOPayrollStypayod.Add(objDVOPayrollStypayod);
                        }
                      }
                      if (dr["rowid3"] != DBNull.Value && dr["rowid3"].ToString().Trim().Length > 0)
                        if (_rowDelete)
                        {
                          using (DVOPayrollStypayod objDVOPayrollStypayod = new DVOPayrollStypayod())
                          {
                            objDVOPayrollStypayod.Doc_no = objUpdDVOPayrollProcess_PayEmployee.Doc_no;
                            objDVOPayrollStypayod.RowID = Convert.ToInt32(dr["rowid3"]);
                            listDeleteDVOPayrollStypayod.Add(objDVOPayrollStypayod);
                          }
                        }
                    }
                  }
                }*/

                #endregion "Payroll Obligation"

                //Call Update function of BLL
                int i = BLLUpdatePayrollEntries.UpdatePayrollEntries(ref TransactionObject,
                    ref objUpdDVOPayrollProcess_PayEmployee,
                    ref objNewlistDVOPayrollstypayid,
                    ref objUpdatelistDVOPayrollstypayid,
                    ref objDeletelistDVOPayrollstypayid,
                    ref listNewDVOPayrollstypaydd,
                    ref listUpdateDVOPayrollstypaydd,
                    ref listDeleteDVOPayrollstypaydd,
                    ref listNewDVOPayrollStypayod,
                    ref listUpdateDVOPayrollStypayod,
                    ref listDeleteDVOPayrollStypayod);

                //if process is successfully completed, then set Success flag to true.
                if (i > 0)
                    Success = true;

                objUpdDVOPayrollProcess_PayEmployee = null;
                objNewlistDVOPayrollstypayid = null;
                objUpdatelistDVOPayrollstypayid = null;
                objDeletelistDVOPayrollstypayid = null;
                listNewDVOPayrollstypaydd = null;
                listUpdateDVOPayrollstypaydd = null;
                listDeleteDVOPayrollstypaydd = null;
                listNewDVOPayrollStypayod = null;
                listUpdateDVOPayrollStypayod = null;
                listDeleteDVOPayrollStypayod = null;
            }
            catch (Exception ex)
            {
                Success = false;
                ErrorMessage = ex.Message;
            }
        }

        public ActionResult DownloadMediaFile()
        {
            ViewBag.DownloadNUploadFile = DownloadNUploadFile.DownloadbaknkMedia.ToString();
            return PartialView("~/Views/Shared/_DownloadUploadFile.cshtml");
        }

        public ActionResult UploadMediaFile()
        {
            int userid = AppUserManager.GetUserId();
            var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
            var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionProcess" && x.ActionName == "UploadMediaFile")
                         join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                         from p in ps.DefaultIfEmpty()
                         select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Response Payment File", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

            if (model != null)
            {
                ViewBag.AddPermission = model.AddPermssion;
                ViewBag.EditPermission = model.EditPermission;
            }

            ViewBag.DownloadNUploadFile = DownloadNUploadFile.UploadBankMedia.ToString();
            return PartialView("~/Views/Shared/_DownloadUploadFile.cshtml");
        }
        public ActionResult DownloadValidationsFile()
        {
            ViewBag.IsGeneratePensionWithoutSftp = IsGeneratePensionWithoutSftp;
            int userid = AppUserManager.GetUserId();
            var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
            var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionProcess" && x.ActionName == "DownloadValidationsFile")
                         join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                         from p in ps.DefaultIfEmpty()
                         select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Forward File - Unapproved Bank Account", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

            if (model != null)
            {
                ViewBag.AddPermission = model.AddPermssion;
                ViewBag.EditPermission = model.EditPermission;
            }
            ViewBag.DownloadNUploadFile = DownloadNUploadFile.DownloadVerification.ToString();//
                                                                                              // Get bank name thats not validated
            List<string> bName = new List<string>();
            bName.Add("JKB");
            bName.Add("THE JAMMU AND KASHMIR BANK LTD.");
            var notValidatedBankNames = db.MasterEmpBankDetails.Where(x => x.ACCOUNT_STATUS != "ACTIVE" && !bName.Contains(x.BankName)).Select(x => x.BankName).Distinct().ToList();
            ViewBag.BanksName = db.MasterBanks.Where(x => notValidatedBankNames.Contains(x.bank_desc)).Select(x => new SelectListItem
            {
                Value = x.bank_desc,
                Text = x.bank_desc
            }).ToList();

            //ViewBag.ApplicantIFSCCode = new SelectList(Enumerable.Empty<SelectListItem>());
            //  SelectList(db.MasterEmpBankDetails.GroupBy(x => new { x.APPLICANT_BANK_IFSC_CODE, x.BankName }).Select(x => new { APPLICANT_BANK_IFSC_CODE = x.Key.APPLICANT_BANK_IFSC_CODE + " | " + x.Key.BankName }).ToList(),
            //  "APPLICANT_BANK_IFSC_CODE", "APPLICANT_BANK_IFSC_CODE", string.Empty);
            return PartialView("~/Views/Shared/_DownloadUploadFile.cshtml");
        }

        //get ifsc code
        public ActionResult GetIFSCCodes(string schemeType)
        {
            var ifscCodes = db.MasterEmpBankDetails
                .Where(x => x.BankName == schemeType)
                .Select(x => new SelectListItem
                {
                    Value = x.APPLICANT_BANK_IFSC_CODE,
                    Text = x.APPLICANT_BANK_IFSC_CODE
                })
                .Distinct()
                .ToList();

            return Json(ifscCodes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadValidationsFile()
        {
            ViewBag.IsGeneratePensionWithoutSftp = IsGeneratePensionWithoutSftp;
            int userid = AppUserManager.GetUserId();
            var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
            var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionProcess" && x.ActionName == "UploadValidationsFile")
                         join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                         from p in ps.DefaultIfEmpty()
                         select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Response File - Verified Bank Account", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

            if (model != null)
            {
                ViewBag.AddPermission = model.AddPermssion;
                ViewBag.EditPermission = model.EditPermission;
            }

            ViewBag.DownloadNUploadFile = DownloadNUploadFile.UploadVerification.ToString();
            return PartialView("~/Views/Shared/_DownloadUploadFile.cshtml");
        }

        // 
        //Download Verification
        // old Code of Download File
        #region
        //public ActionResult AjaxHandlerDownload(JQueryDataTableParamModel param)
        //{
        //  var MasterEmpBankDetailsList = (from eb in db.MasterEmpBankDetails.Where(x => x.IsUpload == true)
        //                                  join me in db.MasterEmployees on eb.empl_code equals me.Empl_Code into me1
        //                                  from me in me1.DefaultIfEmpty()
        //                                  join et in db.MasterEmpType on me.Type_Code equals et.Type_Code into et1
        //                                  from et in et1.DefaultIfEmpty()
        //                                  select new DownloadedMediaVM
        //                                  {
        //                                    Area = me.SelectDistrict,
        //                                    SchemeType = et.Description,
        //                                    BankName = eb.BankName,
        //                                    IFSCCode = eb.APPLICANT_BANK_IFSC_CODE,
        //                                    DownloadedBy = eb.downloadedBy,
        //                                    DownloadedOn = eb.downloadedOn,
        //                                  }).ToList();
        //  //join md in db.MasterDistrict on me.SelectDistrict.ToLower().Trim() equals md.Name into md1 from md in md1.DefaultIfEmpty()
        //  //join mr in db.MasterRegion on md.RegionId equals mr.Id into mr1 from mr in mr1.DefaultIfEmpty()
        //  IEnumerable<DownloadedMediaVM> filtered;
        //  if (!string.IsNullOrEmpty(param.sSearch))
        //  {
        //    filtered = MasterEmpBankDetailsList
        //       .Where(c => c.IFSCCode.ToLower().Contains(param.sSearch.ToLower())
        //       || c.BankName.ToLower().Contains(param.sSearch.ToLower())
        //       || c.DownloadedBy.ToLower().Contains(param.sSearch.ToLower())
        //       || c.DownloadedOn.ToString().ToLower().Contains(param.sSearch.ToLower())
        //       || c.SchemeType.ToLower().Contains(param.sSearch.ToLower())
        //       || c.Area.ToLower().Contains(param.sSearch.ToLower())

        //       );

        //  }
        //  else
        //  {
        //    filtered = MasterEmpBankDetailsList;
        //  }

        //  //Sorting through column index
        //  var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

        //  Func<DownloadedMediaVM, string> orderingFunction = (c => sortColumnIndex == 1 ? c.empl_code :
        //                                                        sortColumnIndex == 0 ? c.BankName :
        //                                                        sortColumnIndex == 2 ? c.DownloadedBy :
        //                                                        sortColumnIndex == 3 ? c.DownloadedOn + "" :
        //                                                        sortColumnIndex == 4 ? c.SchemeType :
        //                                                        sortColumnIndex == 5 ? c.Area :
        //                                                        "");

        //  var sortDirection = Request["sSortDir_0"]; // asc or desc
        //  if (sortDirection == "asc")
        //    filtered = filtered.OrderBy(orderingFunction);
        //  else
        //    filtered = filtered.OrderByDescending(orderingFunction);

        //  //Pagging
        //  var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

        //  //Select required columns
        //  var result = from c in displayed
        //               let uploadedById = Convert.ToInt32(c.DownloadedBy)
        //               let userProfile = db.UserProfiles.FirstOrDefault(x => x.Id == uploadedById && x.IsActive == true)
        //               select new[] {
        //                     c.Area,
        //                     c.SchemeType,
        //                     c.BankName,
        //                     c.IFSCCode,
        //                 userProfile?.FirstName ?? "",
        //                 c.DownloadedOn+""
        //               };

        //  return Json(
        //                              new
        //                              {
        //                                sEcho = param.sEcho,
        //                                iTotalRecords = MasterEmpBankDetailsList.Count(),
        //                                iTotalDisplayRecords = filtered.Count(),
        //                                aaData = result
        //                              }, JsonRequestBehavior.AllowGet);
        //}
        #endregion
        // New Code of Download File
        public ActionResult UploadVerificationAjaxHandler(JQueryDataTableParamModel param)
        {
            int valid_res = Convert.ToInt32(MediaType.Validation_res);

            List<MediaDownloads> MediaDownloadsDetailsList = new List<MediaDownloads>();

            var currentFinancialYear = GetCurrentSelectedFinancialYear();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDALBaseClass.GetData("SELECT Id, FileName, HasDownoaded, IsProcessed, CreatedMachineInfo, CreatedBy, CreatedOn, ModifiedMachineInfo, ModifiedBy, ModifiedOn, IsActive, MediaType, RecordId, TotalBeneficiary, TotalValidated, TotalNotValidated, Remark FROM  [dbo].[MediaDownloads] WHERE MediaType = " + valid_res + " And IsActive=1 and YEAR(CreatedOn)=" + currentFinancialYear))
            {//(ref parameter,  objDVOPYBatchProcessStybatchrGET.FIND_QUERY);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        MediaDownloads obj = new MediaDownloads();
                        obj.FileName = (dr[1] != DBNull.Value ? dr[1].ToString() : "");
                        obj.TotalBeneficiary = (dr[13] != DBNull.Value ? Convert.ToInt32(dr[13]) : 0);
                        obj.TotalValidated = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);
                        obj.TotalNotvalidated = (dr[15] != DBNull.Value ? Convert.ToInt32(dr[15]) : 0);
                        obj.IsProcessed = (dr[3] != DBNull.Value ? Convert.ToBoolean(dr[3]) : false);
                        obj.CreatedOn = (dr[6] != DBNull.Value ? Convert.ToDateTime(dr[6]) : (DateTime?)null);
                        obj.CreatedBy = (dr[5] != DBNull.Value ? Convert.ToInt32(dr[5]) : 0);
                        obj.Remark = (dr[16] != DBNull.Value ? dr[16].ToString() : "");
                        MediaDownloadsDetailsList.Add(obj);
                    }
            }
            IEnumerable<MediaDownloads> filtered;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = MediaDownloadsDetailsList
                   .Where(c => c.FileName.ToLower().Contains(param.sSearch.ToLower())
                  || c.TotalBeneficiary.ToString().Contains(param.sSearch.ToLower())
                  || c.TotalValidated.ToString().Contains(param.sSearch.ToLower())
                  || c.TotalNotvalidated.ToString().Contains(param.sSearch.ToLower())
                  || c.IsProcessed.ToString().ToLower().Contains(param.sSearch.ToLower())
                  //|| c.IsActive.ToString().ToLower().Contains(param.sSearch.ToLower())
                  || c.CreatedOn.ToString().ToLower().Contains(param.sSearch.ToLower())
                  || c.CreatedBy.ToString().ToLower().Contains(param.sSearch.ToLower())
                  || c.Remark.ToString().ToLower().Contains(param.sSearch.ToLower()));



            }
            else
            {
                filtered = MediaDownloadsDetailsList;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MediaDownloads, string> orderingFunction = (c => sortColumnIndex == 0 ? c.FileName + "" :
                                                            sortColumnIndex == 1 ? c.TotalBeneficiary + "" :
                                                             sortColumnIndex == 2 ? c.TotalValidated + "" :
                                                              sortColumnIndex == 3 ? c.TotalNotvalidated + "" :
                                                                 sortColumnIndex == 4 ? c.IsProcessed + "" :
                                                                 //sortColumnIndex == 1 ? c.IsActive + "" :
                                                                 sortColumnIndex == 5 ? c.CreatedOn + "" :
                                                                 sortColumnIndex == 6 ? c.CreatedBy + "" :
                                                                 sortColumnIndex == 6 ? c.Remark + "" :
                                                                  "");


            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filtered = filtered.OrderBy(orderingFunction);
            else
                filtered = filtered.OrderByDescending(orderingFunction);

            //Pagging
            var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            //Select required columns
            //Select required columns
            var result = from c in displayed
                         let uploadedById = Convert.ToInt32(c.CreatedBy)
                         let userProfile = db.UserProfiles.FirstOrDefault(x => x.Id == uploadedById && x.IsActive == true)
                         select new[] {
                 c.FileName,
                 c.TotalBeneficiary.ToString(),
                 c.TotalValidated.ToString(),
                 c.TotalNotvalidated.ToString(),
                 c.IsProcessed ? "Yes" : "No",
                 //c.IsActive ? "Active" : "Inactive",
                 String.Format("{0:dd/MM/yyyy}", c.CreatedOn),
                 //c.UploadedDate + "" ,
                 userProfile?.FirstName ?? "",
                 c.Remark
             };


            return Json(
                                                new
                                                {
                                                    sEcho = param.sEcho,
                                                    iTotalRecords = MediaDownloadsDetailsList.Count(),
                                                    iTotalDisplayRecords = filtered.Count(),
                                                    aaData = result
                                                }, JsonRequestBehavior.AllowGet);
        }
        //uploadVerification
        // old Code of Upload File
        #region


        //public ActionResult UploadVerificationAjaxHandler(JQueryDataTableParamModel param)
        //{
        //    var MasterEmpBankDetailsList = db.MasterEmpBankDetails.Where(x => x.IsUpload == true);
        //    IEnumerable<MasterEmpBankDetails> filtered;


        //    if (!string.IsNullOrEmpty(param.sSearch))
        //    {
        //        filtered = MasterEmpBankDetailsList
        //           .Where(c => c.BankName.ToLower().Contains(param.sSearch.ToLower())
        //            || c.APPLICANT_BANK_IFSC_CODE.ToLower().Contains(param.sSearch.ToLower())
        //            || c.downloadedOn.ToString().ToLower().Contains(param.sSearch.ToLower())
        //            || c.downloadedBy.ToString().ToLower().Contains(param.sSearch.ToLower())
        //           //||c.UploadedBy.ToString().ToLower().Contains(param.sSearch.ToLower())

        //           );



        //    }
        //    else
        //    {
        //        filtered = MasterEmpBankDetailsList;
        //    }

        //    //Sorting through column index
        //    var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

        //    Func<MasterEmpBankDetails, string> orderingFunction = (c => sortColumnIndex == 1 ? c.BankName :
        //                                                          sortColumnIndex == 0 ? c.APPLICANT_BANK_IFSC_CODE :
        //                                                          sortColumnIndex == 2 ? c.downloadedOn + "" :
        //                                                          sortColumnIndex == 3 ? c.downloadedBy + "" :
        //                                                                                    //sortColumnIndex == 1 ? c.UploadedBy + "":
        //                                                                                    "");

        //    var sortDirection = Request["sSortDir_0"]; // asc or desc
        //    if (sortDirection == "asc")
        //        filtered = filtered.OrderBy(orderingFunction);
        //    else
        //        filtered = filtered.OrderByDescending(orderingFunction);

        //    //Pagging
        //    var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

        //    //Select required columns

        //    var result = from c in displayed
        //                 let uploadedById = Convert.ToInt32(c.UploadedBy)
        //                 let userProfile = db.UserProfiles.FirstOrDefault(x => x.Id == uploadedById && x.IsActive == true)
        //                 select new[] {
        //         c.BankName,
        //         c.APPLICANT_BANK_IFSC_CODE,
        //         c.downloadedBy + "",
        //         c.downloadedOn + "",
        //         c.IsUpload + "",
        //         userProfile?.FirstName ?? ""
        //     };

        //    return Json(new
        //    {
        //        sEcho = param.sEcho,
        //        iTotalRecords = MasterEmpBankDetailsList.Count(),
        //        iTotalDisplayRecords = filtered.Count(),
        //        aaData = result
        //    }, JsonRequestBehavior.AllowGet);
        //}
        #endregion
        //New Code of Upload File



        public ActionResult AjaxHandlerDownload(JQueryDataTableParamModel param)
        {
            var currentFinancialYear = GetCurrentSelectedFinancialYear();
            int valid = Convert.ToInt32(MediaType.Validation);
            var Media_QueueDetailsList = db.Media_Queue.Where(x => x.MediaType == valid && x.UploadedDate.Value.Year == currentFinancialYear);
            //var MasterEmpBankDetailsList = db.MasterEmpBankDetails.Where(x => x.IsUpload == true);

            IEnumerable<Media_Queue> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = (IEnumerable<Media_Queue>)Media_QueueDetailsList
                .Where(c => c.FilePath.ToLower().Contains(param.sSearch.ToLower())
                || c.TotalBeneficiary.ToString().Contains(param.sSearch.ToLower())
                || c.TotalValidated.ToString().Contains(param.sSearch.ToLower())
                || c.TotalNotvalidated.ToString().Contains(param.sSearch.ToLower())
                || c.IsUploaded.ToString().ToLower().Contains(param.sSearch.ToLower())
                || c.UploadedDate.ToString().ToLower().Contains(param.sSearch.ToLower())
                || c.CreatedBy.ToString().ToLower().Contains(param.sSearch.ToLower())

                );

            }
            else
            {
                filtered = (IEnumerable<Media_Queue>)Media_QueueDetailsList;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<Media_Queue, string> orderingFunction = (c => sortColumnIndex == 0 ? c.FilePath + "" :
                                                             sortColumnIndex == 1 ? c.TotalBeneficiary + "" :
                                                             sortColumnIndex == 2 ? c.TotalValidated + "" :
                                                              sortColumnIndex == 3 ? c.TotalNotvalidated + "" :
                                                              sortColumnIndex == 4 ? c.IsUploaded + "" :
                                                              //sortColumnIndex == 1 ? c.IsActive + "":
                                                              sortColumnIndex == 5 ? c.UploadedDate.ToString() :
                                                              //sortColumnIndex == 2 ? c.UploadedDate + "" :
                                                              sortColumnIndex == 6 ? c.CreatedBy + "" :

                                                                                           //sortColumnIndex == 1 ? c.UploadedBy + "":
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
                         let uploadedById = Convert.ToInt32(c.CreatedBy)
                         let userProfile = db.UserProfiles.FirstOrDefault(x => x.Id == uploadedById && x.IsActive == true)
                         select new[] {
                 Path.GetFileName(c.FilePath),
                 c.TotalBeneficiary.ToString(),
                 c.TotalValidated.ToString(),
                 c.TotalNotvalidated.ToString(),
                 c.IsUploaded ? "Yes" : "No",
                 //c.IsActive ? "Active" : "Inactive",
                 String.Format("{0:dd/MM/yyyy}", c.UploadedDate),
                 //c.UploadedDate + "" ,
                 userProfile?.FirstName ?? ""
             };



            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = Media_QueueDetailsList.Count(),
                iTotalDisplayRecords = filtered.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);


        }

        //binding uploadedbankmedia data
        //public ActionResult UploadBankAjaxHandler(JQueryDataTableParamModel param)
        //{
        //    try
        //    {
        //        var ProcessDirectDepositHeaderList = db.Process_DirectDeposit_Header.Where(x => x.IsUpload == true).ToList();
        //        IEnumerable<Process_DirectDeposit_Header> filtered;


        //        if (!string.IsNullOrEmpty(param.sSearch))
        //        {
        //            filtered = ProcessDirectDepositHeaderList
        //               .Where(c => c.company_name.ToLower().Contains(param.sSearch.ToLower()));//  || c.APPLICANT_BANK_IFSC_CODE.ToLower().Contains(param.sSearch.ToLower()

        //        }
        //        else
        //        {
        //            filtered = ProcessDirectDepositHeaderList;
        //        }

        //        //Sorting through column index
        //        var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

        //        Func<Process_DirectDeposit_Header, string> orderingFunction = (c => sortColumnIndex == 1 ? c.company_name : "");

        //        var sortDirection = Request["sSortDir_0"]; // asc or desc
        //        if (sortDirection == "asc")
        //            filtered = filtered.OrderBy(orderingFunction);
        //        else
        //            filtered = filtered.OrderByDescending(orderingFunction);

        //        //Pagging
        //        var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

        //        //Select required columns

        //        var result = from c in displayed
        //                     let uploadedById = Convert.ToInt32(c.UploadedBy)
        //                     let userProfile = db.UserProfiles.FirstOrDefault(x => x.Id == uploadedById && x.IsActive == true)
        //                     select new[] {
        //         c.company_name,
        //         c.DownloadedBy + "",
        //         c.DownloadedOn + "",
        //         c.IsUpload + "",
        //         userProfile?.FirstName ?? ""
        //     };

        //        return Json(new
        //        {
        //            sEcho = param.sEcho,
        //            iTotalRecords = ProcessDirectDepositHeaderList.Count(),
        //            iTotalDisplayRecords = filtered.Count(),
        //            aaData = result
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //        throw;
        //    }

        //}

        public ActionResult UploadBankAjaxHandler(JQueryDataTableParamModel param)
        {
            var currentFinancialYear = GetCurrentSelectedFinancialYear();
            int retTxnReport = Convert.ToInt32(MediaType.RetTxnReport);
            var Media_QueueDetailsList = db.MediaDownloads.Where(x => x.MediaType == retTxnReport && x.CreatedOn.Value.Year == currentFinancialYear);
            //var MasterEmpBankDetailsList = db.MasterEmpBankDetails.Where(x => x.IsUpload == true);

            IEnumerable<MediaDownloads> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //filtered = (IEnumerable<MediaDownloads>)Media_QueueDetailsList;
                filtered = Media_QueueDetailsList
                 .Where(c => c.FileName.ToLower().Contains(param.sSearch.ToLower())
                  || c.TotalBeneficiary.ToString().Contains(param.sSearch.ToLower())
                  || c.TotalValidated.ToString().Contains(param.sSearch.ToLower())
                  || c.TotalNotvalidated.ToString().Contains(param.sSearch.ToLower())
                  || c.HasDownoaded.ToString().ToLower().Contains(param.sSearch.ToLower())
                  //|| c.IsActive.ToString().ToLower().Contains(param.sSearch.ToLower())
                  || c.CreatedOn.ToString().ToLower().Contains(param.sSearch.ToLower())
                  || c.CreatedBy.ToString().ToLower().Contains(param.sSearch.ToLower())

                 );

            }
            else
            {
                //filtered = (IEnumerable<MediaDownloads>)Media_QueueDetailsList;
                filtered = Media_QueueDetailsList;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MediaDownloads, string> orderingFunction = (c => sortColumnIndex == 1 ? c.FileName + "" :
                                                                  sortColumnIndex == 0 ? c.TotalBeneficiary + "" :
                                                                  sortColumnIndex == 2 ? c.TotalValidated + "" :
                                                                  sortColumnIndex == 3 ? c.TotalNotvalidated + "" :
                                                                  sortColumnIndex == 4 ? c.HasDownoaded + "" :
                                                                  sortColumnIndex == 5 ? c.CreatedOn.ToString() :
                                                                  sortColumnIndex == 6 ? c.CreatedBy + "" :
                                                                                        "");

            // asc or desc
            var sortDirection = Request["sSortDir_0"];
            if (sortDirection == "asc")
                filtered = filtered.OrderBy(orderingFunction);
            else
                filtered = filtered.OrderByDescending(orderingFunction);

            //Pagging
            var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            //Select required columns

            var result = from c in displayed
                         let uploadedById = Convert.ToInt32(c.CreatedBy)
                         let userProfile = db.UserProfiles.FirstOrDefault(x => x.Id == uploadedById && x.IsActive == true)
                         select new[] {
                       c.FileName,
                       c.TotalValidated.ToString(),
                       c.TotalNotvalidated.ToString(),
                       c.TotalBeneficiary.ToString(),
                       c.HasDownoaded ? "Yes" : "No",
                       //c.IsActive ? "Active" : "Inactive",
                       String.Format("{0:dd/MM/yyyy}", c.CreatedOn),
                       //c.UploadedDate + "" ,
                       userProfile?.FirstName ?? ""
                   };




            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = Media_QueueDetailsList.Count(),
                iTotalDisplayRecords = filtered.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult PayrollProcessSummaryAjaxHandler(JQueryDataTableParamModel param)
        //{
        //  int UserId = Convert.ToInt32(User.Identity.GetUserId());
        //  int RoleId = _DbContext.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
        //  List<App.Data.ViewModels.TotalContributionSummaryViewModel> CountList = BLLMasterEmployee.GelAllPaymentTotalCount(UserId, RoleId);
        //  IEnumerable<TotalContributionSummaryViewModel> filtered;
        //  if (!string.IsNullOrEmpty(param.sSearch))
        //  {
        //    filtered = CountList.Where(x => x.EmployerId.ToString().Contains(param.sSearch.ToLower())
        //    || x.EmployerName.ToString().Contains(param.sSearch.ToLower())
        //    //|| x.TotalContributorContribution.ToString().Contains(param.sSearch.ToLower())
        //    || x.TotalMonthAmount.ToString().Contains(param.sSearch.ToLower()));
        //    //|| x.TotalEmployerContribution.ToString().Contains(param.sSearch.ToLower())
        //    //|| x.status.ToString().Contains(param.sSearch.ToLower())
        //    //|| x.NotUpdated.ToString().Contains(param.sSearch.ToLower()));
        //  }
        //  else
        //  {
        //    filtered = CountList;
        //  }
        //  decimal totalTotalMonthAmount = filtered.Sum(c => c.TotalMonthAmount ?? 0);
        //  var result = from c in filtered
        //               select new[]
        //               {
        //                 c.EmployerId,
        //                 c.EmployerName,
        //                 //string.IsNullOrEmpty(c.TotalEmployerContribution)?"0":c.TotalEmployerContribution,
        //                 //string.IsNullOrEmpty(c.status)?"0":c.status,
        //                 //string.IsNullOrEmpty(c.NotUpdated)?"0":c.NotUpdated,
        //                 c.TotalMonthAmount + "",


        //               };
        //  return Json(new
        //  {
        //    sEcho = param.sEcho,
        //    iTotalRecords = CountList.Count(),
        //    iTotalDisplayRecords = filtered.Count(),
        //    aaData = result
        //  }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult BeneficiaryPaymentDetailsAjaxHandler(JQueryDataTableParamModel param, bool isDownload = false)
        {
            try
            {
                var currentFinancialYear = GetCurrentSelectedFinancialYear();
                int UserId = Convert.ToInt32(User.Identity.GetUserId());
                int RoleId = _DbContext.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
                Dictionary<string, string> PensionDetailByMonthYears = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear();
                var batch = PensionDetailByMonthYears == null ? "0" : PensionDetailByMonthYears["pybatchid"];
                int batchno = Convert.ToInt32(batch);
                var selectDistrict = db.SecRoleLocationModule
                         .Where(srlm => srlm.RoleID == RoleId && srlm.UserId == UserId)
                         .Join(db.MasterDistrict,
                               srlm => srlm.DistrictID,
                               md => md.Id,
                               (srlm, md) => md.Name)
                         .Distinct()
                         .ToList();

                // Combine into a single string with comma-separated values
                string DistrictNames = string.Join(",", selectDistrict);

                List<App.Data.ViewModels.TotalContributionSummaryViewModel> CountList = BLLMasterEmployee.GelAllPaymentTotalCount(UserId, RoleId, batchno, DistrictNames, currentFinancialYear);
                IEnumerable<TotalContributionSummaryViewModel> filtered;
                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    filtered = CountList.Where(x => x.Region.ToString().Contains(param.sSearch.ToLower())
                    || x.District.ToString().ToLower().Contains(param.sSearch.ToLower())
                    || x.pybatchid.ToString().Contains(param.sSearch.ToLower())
                    || x.pay_date.ToString().Contains(param.sSearch.ToLower())
                    || x.TotalMonthAmount.ToString().Contains(param.sSearch.ToLower())
                    || x.TotalArreaMonthAmount.ToString().Contains(param.sSearch.ToLower())
                    || x.OAPCount.ToString().Contains(param.sSearch.ToLower())
                    || x.WIDCount.ToString().Contains(param.sSearch.ToLower())
                    || x.PCPCount.ToString().Contains(param.sSearch.ToLower())
                    || x.TGRCount.ToString().Contains(param.sSearch.ToLower())
                    || x.IsReUploaded.ToString().Contains(param.sSearch.ToLower())
                    || x.isProcessed.ToString().Contains(param.sSearch.ToLower())
                    );
                }
                else
                {
                    filtered = CountList;
                }
                if (isDownload)
                {
                    FileContentResult bytesdata = PaymentDetailsReportAjax(filtered);
                    return Json(bytesdata, JsonRequestBehavior.AllowGet);
                }
                var result = from c in filtered
                             select new[]
                             {
                     c.Region,   //0
                     c.District,  //1
                     c.PCPCount + "", //2
                     c.OAPCount + "", //3
                     c.WIDCount + "", //4
                     c.TGRCount + "", //5
                     c.BeneficiariesCount + "", //6
                     c.TotalMonthAmount.Value.ToString("0.00") + "", //7
                     c.TotalArreaMonthAmount.Value.ToString("0.00") + "", //8
                     c.pybatchid +"", //9
                     c.pay_date +"", //10
                     c.IsReUploaded +"", //11
                     c.isProcessed +"", //12

                   };
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = CountList.Count(),
                    iTotalDisplayRecords = filtered.Count(),
                    aaData = result
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private FileContentResult PaymentDetailsReportAjax(IEnumerable<TotalContributionSummaryViewModel> listSearch)
        {

            try
            {
                var result = from c in listSearch
                             select new[]
                             {
                     c.Region,
                     c.District,
                     c.PCPCount + "",
                     c.OAPCount + "",
                     c.WIDCount + "",
                     c.TGRCount + "",
                     c.BeneficiariesCount + "",
                     c.TotalMonthAmount.Value.ToString("0.00") + "",
                     c.TotalArreaMonthAmount.Value.ToString("0.00") + "",
                     c.pybatchid +"",
                     c.pay_date +"",

                   };


                FileContentResult bytesdata;
                using (MemoryStream stream = new MemoryStream())
                {
                    using (SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
                    {
                        WorkbookPart workbookPart = document.AddWorkbookPart();
                        workbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

                        WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                        worksheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(new SheetData());

                        DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = workbookPart.Workbook.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheets());
                        Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
                        sheets.Append(sheet);

                        SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                        // Get or create the MergeCells element
                        MergeCells mergeCells;
                        if (worksheetPart.Worksheet.Elements<MergeCells>().Count() > 0)
                        {
                            mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().First();
                        }
                        else
                        {
                            mergeCells = new MergeCells();
                            worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<SheetData>().First());
                        }

                        // Create a row for merged cells
                        // Merge cells A1:G1


                        Row mergedRow = new Row();
                        MergeCell mergeCell1 = new MergeCell() { Reference = new StringValue("A1:H1") };
                        mergeCells.AppendChild(mergeCell1);
                        Cell cellA1 = CreateCell("Beneficiary Payment Details");
                        mergedRow.AppendChild(cellA1);
                        worksheetPart.Worksheet.Elements<SheetData>().First().AppendChild(mergedRow);

                        // Merge cells A2:G2
                        var allDataList = listSearch.ToList();
                        var firstCellValues = allDataList.FirstOrDefault();
                        DateTime payDate = Convert.ToDateTime(firstCellValues.pay_date);

                        //string month = payDate.ToString("MMMM", CultureInfo.InvariantCulture);
                        //string year = payDate.ToString("yyyy", CultureInfo.InvariantCulture);

                        string date = payDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                        Row mergerow2 = new Row();
                        MergeCell mergeCell2 = new MergeCell() { Reference = new StringValue("A2:H2") };
                        mergeCells.AppendChild(mergeCell2);

                        // date Formte July - 2024
                        //Cell cellA2 = CreateCell("Period -" + month + " - " + year);

                        Cell cellA2 = CreateCell("Period " + date);
                        mergerow2.AppendChild(cellA2);
                        worksheetPart.Worksheet.Elements<SheetData>().First().AppendChild(mergerow2);




                        // Merge cells A3:G3                       
                        Row mergerow3 = new Row();
                        MergeCell mergeCell3 = new MergeCell() { Reference = new StringValue("A3:H3") };
                        mergeCells.AppendChild(mergeCell3);
                        Cell cellA3 = CreateCell("");
                        mergerow3.AppendChild(cellA3);
                        worksheetPart.Worksheet.Elements<SheetData>().First().AppendChild(mergerow3);


                        // Merge cells A4:A5                       
                        Row mergerow4 = new Row();
                        //MergeCell mergeCell4 = new MergeCell() { Reference = new StringValue("A4:A5") };
                        //mergeCells.AppendChild(mergeCell4);
                        //Cell cellA4 = CreateCell("District");
                        //mergerow4.AppendChild(cellA4);
                        //worksheetPart.Worksheet.Elements<SheetData>().First().AppendChild(mergerow4);
                        Cell cellG1 = CreateCell("District");
                        cellG1.CellReference = new StringValue("A4");
                        mergerow4.AppendChild(cellG1);
                        // Merge cells B4:F4                       

                        MergeCell mergeCellB = new MergeCell() { Reference = new StringValue("B4:F4") };
                        mergeCells.AppendChild(mergeCellB);
                        Cell cellB = CreateCell("No.of Beneficiary");
                        mergerow4.AppendChild(cellB);


                        // Merge cells G4:G5  
                        MergeCell mergeCellG = new MergeCell() { Reference = new StringValue("G4:H4") };
                        mergeCells.AppendChild(mergeCellG);
                        Cell cellg = CreateCell("Amount");
                        cellg.CellReference = new StringValue("G4");
                        mergerow4.AppendChild(cellg);

                        //Cell cellG = CreateCell("Amount");
                        //cellG.CellReference = new StringValue("G4");
                        //mergerow4.AppendChild(cellG);

                        worksheetPart.Worksheet.Elements<SheetData>().First().AppendChild(mergerow4);



                        Row individualRow = new Row();

                        // Add text to individual cells starting from B5
                        Cell cellB5 = CreateCell("PCP");
                        cellB5.CellReference = new StringValue("B5");
                        individualRow.AppendChild(cellB5);

                        Cell cellC5 = CreateCell("OAP");
                        cellC5.CellReference = new StringValue("C5");
                        individualRow.AppendChild(cellC5);

                        Cell cellD5 = CreateCell("WID");
                        cellD5.CellReference = new StringValue("D5");
                        individualRow.AppendChild(cellD5);

                        Cell cellE5 = CreateCell("TGR");
                        cellE5.CellReference = new StringValue("E5");
                        individualRow.AppendChild(cellE5);

                        Cell cellF5 = CreateCell("Total");
                        cellF5.CellReference = new StringValue("F5");
                        individualRow.AppendChild(cellF5);

                        Cell cellG5 = CreateCell("Basic");
                        cellG5.CellReference = new StringValue("G5");
                        individualRow.AppendChild(cellG5);

                        Cell cellH5 = CreateCell("Arrear");
                        cellH5.CellReference = new StringValue("H5");
                        individualRow.AppendChild(cellH5);

                        // Add the individual row to the worksheet
                        worksheetPart.Worksheet.Elements<SheetData>().First().AppendChild(individualRow);


                        int rowIndex = 5;

                        var ds = listSearch.GroupBy(x => x.Region).ToList();
                        int totalPCPCount = 0;
                        int totalOAPCount = 0;
                        int totalWIDCount = 0;
                        int totalTGRCount = 0;
                        int totalBeneficiariesCount = 0;
                        decimal totalAmount = 0.0m;
                        decimal totalArreaAmount = 0.0m;
                        int GtotalPCPCount = 0;
                        int GtotalOAPCount = 0;
                        int GtotalWIDCount = 0;
                        int GtotalTGRCount = 0;
                        int GtotalBeneficiariesCount = 0;
                        decimal GtotalAmount = 0.0m;
                        decimal GtotalArreaAmount = 0.0m;
                        foreach (var dataRow in ds)
                        {

                            // Check the value of the first cell in the current data row
                            var firstCellValue = dataRow.FirstOrDefault();


                            //if (firstCellValue.Region == "JAMMU REGION" || firstCellValue.Region == "KASHMIR REGION")
                            if (firstCellValue.Region != "")
                            {

                                var dataRowElement = new Row();
                                dataRowElement.AppendChild(CreateCell(firstCellValue.Region));
                                sheetData.InsertAt(dataRowElement, rowIndex);


                                rowIndex++;
                            }



                            foreach (var cellValue in dataRow)
                            {

                                var dataRowElement = new Row();

                                dataRowElement.AppendChild(CreateCell(cellValue.District));
                                dataRowElement.AppendChild(CreateCell(cellValue.PCPCount.ToString()));
                                dataRowElement.AppendChild(CreateCell(cellValue.OAPCount.ToString()));
                                dataRowElement.AppendChild(CreateCell(cellValue.WIDCount.ToString()));
                                dataRowElement.AppendChild(CreateCell(cellValue.TGRCount.ToString()));
                                dataRowElement.AppendChild(CreateCell(cellValue.BeneficiariesCount.ToString()));
                                dataRowElement.AppendChild(CreateCell(cellValue.TotalMonthAmount == null ? "0.00" : Convert.ToDecimal(cellValue.TotalMonthAmount).ToString("F2")));
                                dataRowElement.AppendChild(CreateCell(cellValue.TotalArreaMonthAmount == null ? "0.00" : Convert.ToDecimal(cellValue.TotalArreaMonthAmount).ToString("F2")));


                                totalPCPCount += cellValue.PCPCount;
                                totalOAPCount += cellValue.OAPCount;
                                totalWIDCount += cellValue.WIDCount;
                                totalTGRCount += cellValue.TGRCount;
                                totalBeneficiariesCount += cellValue.BeneficiariesCount;
                                totalAmount += Convert.ToDecimal(cellValue.TotalMonthAmount);
                                totalArreaAmount += Convert.ToDecimal(cellValue.TotalArreaMonthAmount);

                                sheetData.InsertAt(dataRowElement, rowIndex);

                                rowIndex++;

                            }

                            var totalRow = new Row();
                            totalRow.AppendChild(CreateCell("Grand Total"));
                            totalRow.AppendChild(CreateCell(totalPCPCount.ToString()));
                            totalRow.AppendChild(CreateCell(totalOAPCount.ToString()));
                            totalRow.AppendChild(CreateCell(totalWIDCount.ToString()));
                            totalRow.AppendChild(CreateCell(totalTGRCount.ToString()));
                            totalRow.AppendChild(CreateCell(totalBeneficiariesCount.ToString()));
                            totalRow.AppendChild(CreateCell(totalAmount.ToString("F2")));
                            totalRow.AppendChild(CreateCell(totalArreaAmount.ToString("F2")));

                            GtotalPCPCount += totalPCPCount;
                            GtotalOAPCount += totalOAPCount;
                            GtotalWIDCount += totalWIDCount;
                            GtotalTGRCount += totalTGRCount;
                            GtotalBeneficiariesCount += totalBeneficiariesCount;
                            GtotalAmount += Convert.ToDecimal(totalAmount);
                            GtotalArreaAmount += Convert.ToDecimal(totalArreaAmount);
                            totalPCPCount = 0;
                            totalOAPCount = 0;
                            totalWIDCount = 0;
                            totalTGRCount = 0;
                            totalBeneficiariesCount = 0;
                            totalAmount = 0;
                            totalArreaAmount = 0;
                            sheetData.InsertAt(totalRow, rowIndex);
                            workbookPart.Workbook.Save();
                            //rowIndex++;
                        }


                        //var totalRows = new Row();
                        //totalRows.AppendChild(CreateCell("Grand Total"));
                        //totalRows.AppendChild(CreateCell(GtotalPCPCount.ToString()));
                        //totalRows.AppendChild(CreateCell(GtotalOAPCount.ToString()));
                        //totalRows.AppendChild(CreateCell(GtotalWIDCount.ToString()));
                        //totalRows.AppendChild(CreateCell(GtotalTGRCount.ToString()));
                        //totalRows.AppendChild(CreateCell(GtotalBeneficiariesCount.ToString()));
                        //totalRows.AppendChild(CreateCell(GtotalAmount.ToString("F2")));
                        //totalRows.AppendChild(CreateCell(GtotalArreaAmount.ToString("F2")));

                        //sheetData.InsertAt(totalRows, rowIndex);


                        //workbookPart.Workbook.Save();

                    }
                    //bytesdata = File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, "Payment Detail Report.xlsx");
                    bytesdata = File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, "BeneficiaryPaymentDetails.xlsx");
                    return bytesdata;
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        static Cell CreateCell(string text)
        {
            var cell = new Cell(new CellValue(text));
            cell.DataType = new EnumValue<CellValues>(CellValues.String);
            return cell;
        }


        // new code by Rashi 

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



        public JsonResult MasterEmployeeExcelCancelUploadAjax()
        {
            var data = db.MediaDownloads.Where(e => e.IsActive).OrderByDescending(x => x.Id).FirstOrDefault();
            if (data != null)
            {
                data.IsActive = false;
                db.SaveChanges();
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }


    }
}