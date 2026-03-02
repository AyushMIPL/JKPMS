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
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using JKPS.BLL;
using JKPS.COMMON;
//using Renci.SshNet.Common;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class MenuListController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public MenuListController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }


        public ActionResult Index()
        {

            //string[] disData = { };
            PensionProcessViewModel Paysearch = ShowActiveBatch();

            //      List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BindComboEmployeeType();
            //ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
            //ViewBag.EmployeeType = new SelectList(listDVOMasterEmpTypes, "type_code", "description", Paysearch.EmpType);
            //      if (Paysearch.searchcriteria != "" && Paysearch.searchcriteria != null)
            //      {
            //          disData = Paysearch.searchcriteria.ToLower().Trim().Split(',');
            //      }
            //      int UserId = AppUserManager.GetUserId();
            //      List<int> roleId = DbContext.UserRole.Where(x => x.UserId == UserId).Select(x => x.RoleId).ToList();

            //      ViewBag.Group = GetUsersAssignedLocations();

            PensionProcessViewModelTest Model = new PensionProcessViewModelTest();
            var secmodule = db.SecModule.Where(x => x.ParentId == 14 && x.IsActive && x.Id != 18).Select(x => new SecModuleVMTest
            {
                // Id = Convert.ToString(x.Id),
                ModuleName = x.ModuleName,
                ModuleDesc = x.ModuleDesc,
                ParentId = x.ParentId,
                Url = x.Url,
                ActionName = x.ActionName,
                ControllerName = x.ControllerName,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                ModifiedBy = x.ModifiedBy,
                ModifiedOn = x.ModifiedOn,
                IsActive = x.IsActive,
                ModuleClass = x.ModuleClass,
                DisplayOrder = x.DisplayOrder,
            }).OrderBy(x => x.DisplayOrder).ToList();
            Model.SecmoduleList = secmodule;
            Model.EmployeeCode = "";
            Model.EmpType = "";
            Model.EOPDate = DateTime.Now;
            Model.FirstName = "";
            Model.LastName = "";
            Model.RegionNames = "";
            ////Model.pybatchid= Paysearch.pybatchid==0?0: Paysearch.pybatchid;
            return View(Model);
        }


        //

        //private List<DVOMasterEmpTypes> BindComboEmployeeType()
        //{
        //    //make object to pass as parameter of search function
        //    DVOMasterEmpTypes objDVOMasterEmpTypes = new DVOMasterEmpTypes();
        //    //call getDate function of BLL
        //    List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes);
        //    objDVOMasterEmpTypes = null;
        //    return listDVOMasterEmpTypes;
        //}

        ///creating batch process
        ///

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
                    Paysearch.processstartedon = obj.startedon == null ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(obj.startedon);
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



        //close batch

        #region

        //DVOPYBatchProcessStybatchr pObjBatch = null;
        //public JsonResult CloseBatchProcessAjax(PensionProcessViewModel pensionProcessHeader)
        //{
        //  try
        //  {
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
        //      return Json(new PensionProcessViewModel(), JsonRequestBehavior.AllowGet);
        //    }
        //    #endregion PYBatchManagement
        //    int userid = AppUserManager.GetUserId();
        //    DVOPYBatchProcessStybatchr objDVOPYBatchProcessStybatchr = new DVOPYBatchProcessStybatchr();
        //    objDVOPYBatchProcessStybatchr.pybatchid = pensionProcessHeader.pybatchid;
        //    objDVOPYBatchProcessStybatchr.updateby = userid;
        //    objDVOPYBatchProcessStybatchr.insertmachineinfo = System.Environment.MachineName;
        //    int UPDResult = BLLPYBatchProcessStybatchr.UPDATEBatchProcessStatus(ref objDVOPYBatchProcessStybatchr);


        //    ViewBag.StatusID = new SelectList(db.MasterStatus, "Id", "Name");
        //    BindComboEmployeeType(string.Empty);

        //    if (UPDResult == 1)
        //    {
        //      TempData["success"] = "Active Batch of Pension Process has been Closed Successfully.";

        //      var response = new { message = TempData["success"] };
        //      return Json(response, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    { PensionProcessViewModel Paysearch = ShowActiveBatch(); }


        //    #endregion
        //  }
        //  catch (Exception ex)
        //  {
        //    TempData["error"] = "Please try Again.." + ex.Message;
        //    //ReportViewer.ReportSource = null;
        //    //SatyaPay.StyleUtility.Reports_Splasher.Close();
        //    //ExceptionManagement.ExceptionManager.Publish(ex);
        //    //MessageBox.Show(ex.Message, "Satya Pay.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //  }
        //  return Json(pensionProcessHeader, JsonRequestBehavior.AllowGet);
        //}


        //private void BindComboEmployeeType(string type_code)
        //{
        //  //make object to pass as parameter of search function
        //  DVOMasterEmpTypes objDVOMasterEmpTypes = new DVOMasterEmpTypes();
        //  //call getDate function of BLL
        //  List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes).Select(s => new DVOMasterEmpTypes
        //  {
        //    type_code = s.type_code,
        //    //description = string.Format("{0} | {1} | {2} | {3} | {4}", s.type_code, s.description, s.pay_period, s.empl_status, s.hold_pymnt)
        //    description = string.Format("{0} | {1}", s.type_code, s.description)
        //  }).ToList();

        //  objDVOMasterEmpTypes = null;
        //  ViewBag.EmployeeType = new SelectList(listDVOMasterEmpTypes, "type_code", "description", type_code);
        //  //check list is null or not
        //  if (listDVOMasterEmpTypes != null)
        //  {
        //    //make a blank object and insert into first position
        //    DVOMasterEmpTypes tmpDVOMasterEmpTypes = new DVOMasterEmpTypes();
        //    tmpDVOMasterEmpTypes.type_code = string.Empty;
        //    tmpDVOMasterEmpTypes.description = "-- Select --";
        //    tmpDVOMasterEmpTypes.pay_period = "";
        //    tmpDVOMasterEmpTypes.empl_status = "";
        //    tmpDVOMasterEmpTypes.hold_pymnt = "";
        //    listDVOMasterEmpTypes.Insert(0, tmpDVOMasterEmpTypes);
        //    //check list has some items or not
        //    if (listDVOMasterEmpTypes.Count > 0)
        //    {
        //      //bind combo box with list
        //      type_code = string.IsNullOrWhiteSpace(type_code) ? string.Empty : type_code;
        //      ViewBag.EmployeeType = new SelectList(listDVOMasterEmpTypes, "type_code", "description", type_code);
        //    }
        //  }
        //}

        //private PensionProcessViewModel ShowActiveBatch()
        //{
        //  try
        //  {
        //    PensionProcessViewModel Paysearch = new PensionProcessViewModel();
        //    /*payrollSearchControl1.ClearControls();
        //    dtEOPDate.Checked = false;
        //    dtPayrollDate.Checked = false;
        //    btnstart.Enabled = true;
        //    btnstart.Text = "No Active Batch for Payroll Process, Click to Start New Batch.";*/

        //    // lstDVOPYBatchProcessStybatchr.
        //    List<DVOPYBatchProcessStybatchr> lstDVOPYBatchProcessStybatchr = BLLPYBatchProcessStybatchr.GetActiveBatch();
        //    if (lstDVOPYBatchProcessStybatchr != null && lstDVOPYBatchProcessStybatchr.Count > 0)
        //    {
        //      DVOPYBatchProcessStybatchr obj = lstDVOPYBatchProcessStybatchr[0];
        //      pObjBatch = lstDVOPYBatchProcessStybatchr[0];
        //      /*lblBatchID.Text = Convert.ToString(obj.pybatchid);
        //      lblProcessStartedOn.Text = obj.startedon;
        //      lblStartBy.Text = Convert.ToString(obj.insertby);
        //      lblStartMachineInfo.Text = obj.insertmachineinfo;*/

        //      Paysearch.pybatchid = obj.pybatchid;
        //      Paysearch.searchcriteria = obj.searchcriteria;
        //      //Paysearch.processstartedon = obj.startedon == null ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(obj.startedon);
        //      //Paysearch.processstartedon = obj.endedon == null ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(obj.endedon);
        //      Paysearch.errormessage = obj.errormessage;
        //      Paysearch.status = obj.status;

        //      if (obj.searchcriteria.Trim().Length > 0)
        //      {

        //        string[] searchCriteria = obj.searchcriteria.Split(new string[] { "_R_" }, StringSplitOptions.None);

        //        string field = string.Empty;
        //        string value = string.Empty;
        //        foreach (string str in searchCriteria)
        //        {
        //          string[] sca = str.Split('=');
        //          if (sca.Length > 1)
        //          {
        //            field = sca[0].Trim();
        //            value = sca[1].Trim();
        //          }
        //          switch (field)
        //          {
        //            case "District":
        //              Paysearch.RegionNames = value;
        //              break;
        //            case "First Name":
        //              Paysearch.FirstName = value;
        //              break;

        //            case "Last Name":
        //              Paysearch.LastName = value;
        //              break;
        //            case "SPay Period":
        //              Paysearch.PayPeriod = value;
        //              break;
        //            case "Full Time":
        //              Paysearch.FullTime = value;
        //              break;
        //            case "Pensioner Type":
        //              Paysearch.EmpType = value;
        //              break;
        //            case "Job Code":
        //              Paysearch.JobCode = value;
        //              break;
        //            case "Job Title":
        //              Paysearch.Title = value;
        //              break;
        //            case "Pensioner Code":
        //              Paysearch.EmployeeCode = value;
        //              break;
        //            case "Last Pay Date":
        //              Paysearch.LPayDate = DateTime.ParseExact(value, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        //              Paysearch.LPayDateChecked = true;
        //              break;
        //            case "End Of Period":
        //              Paysearch.EOPDate = DateTime.ParseExact(value, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        //              Paysearch.EOPDateChecked = true;
        //              break;
        //            case "Payroll Date":
        //              Paysearch.PayrollDate = DateTime.ParseExact(value, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        //              Paysearch.PayrollDateChecked = true;
        //              break;
        //          }
        //        }
        //      }
        //      if (obj.status == 1)
        //      {

        //        return Paysearch;
        //      }

        //    }
        //    TempData["error"] = "No Active Batch for Pension Process,Please Create a New Batch & Refresh";

        //    return Paysearch;
        //  }
        //  catch (Exception Ex)
        //  {
        //    TempData["error"] = "Please Try Again.." + Ex.Message;
        //    //ExceptionManagement.ExceptionManager.Publish(Ex);
        //    //throw Ex;
        //    return null;
        //  }

        //}

        #endregion


    }
}

