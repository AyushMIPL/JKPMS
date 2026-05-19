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
using App.Data.Extentions;
using App.Web.Filters;
using JKPS.COMMON;
using JKPS.BLL;
using Postal;
using App.Web.Repository;
using System.IO;
using System.Data.SqlClient;
using OfficeOpenXml;
// using Microsoft.Office.Interop.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using App.Data.ViewModels;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Data.Entity.Core;
using System.Text;
using System.Web.Configuration;
using App.Web.Entities;
using DocumentFormat.OpenXml.EMMA;
using static App.Web.Controllers.ContributorPersonalDetailsController;
using System.Reflection;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using App.Web.Helper;
using System.Web.WebPages;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Data.Entity.Migrations;
using JKPS.DL;
using Renci.SshNet;
using CrystalDecisions.Shared.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Bibliography;
using System.Net.Mail;
using Aspose.Email;
using Aspose.Email.PersonalInfo;
using static App.Web.Helper.Helper;
using ClosedXML.Excel;
using Castle.Core.Internal;
using System.IO.Compression;
using Microsoft.AspNet.Identity;
using System.Globalization;
using DocumentFormat.OpenXml.Vml.Spreadsheet;

namespace App.Web.Controllers
{
  [AuthorizeEx()]
  public class MasterPensionerController : BaseController
  {
    private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
    //private AppDbContext db = new AppDbContext();
    private AppDbContext db;
    private AppDbContext _DbContext;

    public MasterPensionerController()
    {
      db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
      _DbContext = DbContext;
    }

    #region Locking

    //***************************************************************************************
    // 1.Update LockCurrentRecord() and ReleaseCurrentRecord() function for DVO object in your form
    // 2.call LockCurrentRecord() function on UpdateClick event, if return is 1(i.e RecordLocked),
    //   UpdateClick event-handler will execute otherwise not.
    // 3.use TransactionObject object to update record process
    // 4.then ReleaseCurrentRecord() function with commit if update successful otherwise 
    //      ReleaseCurrentRecord() with rollback
    // 5.ReleaseCurrentRecord() function is also called in CancelClick event
    // 6.Assign Event-Handler frm_FormClosed(...) to FormClosed event of this form.
    //***************************************************************************************

    /// <summary>
    /// object to maintain the transaction object to execute update process
    ///  it is used to lock/update/release of current record
    /// </summary>
    object TransactionObject;

    /// <summary>
    /// status of Lock, if current record is locked than 1, otherwise 0.
    /// </summary>
    int _ChangeLockStatus = 0;

    /// <summary>
    /// Lock current record for updation
    /// </summary>
    /// <returns></returns>
    private int LockCurrentRecord()
    {
      DVOMasterEmployee objDVOMasterEmployee = new DVOMasterEmployee();
      objDVOMasterEmployee.RowID = CurrentRecordUniqueId;
      int LockStatus = BLLCommonUtilities.LockCurrentRecord(ref TransactionObject, (iDVO)objDVOMasterEmployee, (TempData["UserId"] == null ? 0 : Convert.ToInt32(TempData["UserId"])), System.Environment.MachineName);
      if (LockStatus != 1)
      {
        _ChangeLockStatus = 0;
        //DialogResult d = SatyaPayMain.CommonUtilities.Utilities.ShowMessage("Want to wait to release record?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //if (d == DialogResult.Yes)
        //{
        //  System.Threading.Thread.Sleep(10000);
        //  LockCurrentRecord();
        //}
      }
      else
      {
        List<DVOMasterEmployee> listDVOMasterEmployee = BLLMasterEmployee.GetData(ref objDVOMasterEmployee);
        if (listDVOMasterEmployee.Count > 0)
          if (listSearchResultDVOMasterEmployee != null)
            if (listSearchResultDVOMasterEmployee.Count > 0)
              listSearchResultDVOMasterEmployee[CurrentRecordIndex] = listDVOMasterEmployee[0];

        listDVOMasterEmployee = null;
        objDVOMasterEmployee = null;
        _ChangeLockStatus = 1;
      }

      return _ChangeLockStatus;
    }

    /// <summary>
    /// Release lock from current record after updation or cancel
    /// </summary>
    private void ReleaseCurrentRecord(bool IsCommit, bool IsRollback)
    {
      if (CurrentRecordRowId > 0)
        if (TransactionObject != null)
        {
          DVOMasterEmployee objDVOMasterEmployee = new DVOMasterEmployee();
          objDVOMasterEmployee.RowID = CurrentRecordUniqueId;
          BLLCommonUtilities.ReleaseLockCurrentRecord(ref TransactionObject, (iDVO)objDVOMasterEmployee, (TempData["UserId"] == null ? 0 : Convert.ToInt32(TempData["UserId"])), System.Environment.MachineName, IsCommit);
          if ((!IsCommit) && IsRollback)
            BLLCommonUtilities.LockTransaction_Rollback(ref TransactionObject);
          objDVOMasterEmployee = null;
          TransactionObject = null;
          _ChangeLockStatus = 0;
        }
    }

    #endregion Locking

    #region Private Members

    /// <summary>
    /// thread to load form's default data after form load event
    /// </summary>
    bool _Loading = false;

    /// <summary>
    /// list of DVOMasterEmployee objects to save result of search
    /// </summary>
    List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = null;

    /// <summary>
    /// list of income-codes of employee for which we are displaying information
    /// </summary>
    List<DVOMasterEmployeeIncomes> listSearchResultDVOMasterEmployeeIncomes = null;

    /// <summary>
    /// list of deduction-codes of employee for which we are displaying information
    /// </summary>
    List<DVOMasterEmployeeDeductions> listSearchResultDVOMasterEmployeeDeductions = null;

    /// <summary>
    /// list of obligation-codes of employee for which we are displaying information
    /// </summary>
    List<DVOMasterEmployeeObligations> listSearchResultDVOMasterEmployeeObligations = null;

    /// <summary>
    /// list of PositionHistory of employee for which we are displaying information
    /// </summary>
    List<DVOPREmployeePositionHistoryInyemppd> listSearchResultDVOPREmployeePositionHistoryInyemppd = null;

    /// <summary>
    /// list of DirectDeposit of employee for which we are displaying information
    /// </summary>
    List<DVOMasterEmpBankDetails> listSearchResultDVOMasterEmpBankDetails = null;

    /// <summary>
    /// DVOMasterEmployee type object to contain Search Criteria entered by user
    /// </summary>
    DVOMasterEmployee objSearchCriteriaDVOMasterEmployee = null;

    /// <summary>
    /// default settings of payable accounts
    /// </summary>
    DvoUpdatePayableDefDetails objDvoUpdatePayableDefDetails = null;

    /// <summary>
    /// Current record index selected by user after search
    /// </summary>
    private int CurrentRecordIndex = 0;

    /// <summary>
    /// Current RowId after search
    /// </summary>
    private int CurrentRecordRowId = 0;

    /// <summary>
    /// Current EmployeeCode after search
    /// </summary>
    private string CurrentRecordEmployeeCode = string.Empty;

    /// <summary>
    /// Current Record's Unique id in table of DVOARCashReceiptsStrcashe object, selected after search
    /// </summary>
    private int CurrentRecordUniqueId = 0;

    /// <summary>
    /// message to show error after completion of any process on this form,
    /// it will set after completion of process, if any error
    /// </summary>
    private string ErrorMessage = "There is some error.\nPlease try again.";

    string _SelectedEmployeeTypeOnInsertion = string.Empty;

    /// <summary>
    /// Notes entered for selected Employee
    /// </summary>
    //List<DVOstxnoted> listEmployeeNotes = new List<DVOstxnoted>();
    //string _EmployeeNotes = string.Empty;
    //int _EmployeeNotesRowId = 0;

    #endregion Private Members
    #region INotesImplement Members

    DVOMasterEmployee objDVOForNotes = new DVOMasterEmployee();
    public iDVO DVOName
    {
      get { return objDVOForNotes; }
      set { objDVOForNotes = (DVOMasterEmployee)value; }
    }

    List<DVOstxnoted> listCommonNotes = new List<DVOstxnoted>();
    public List<DVOstxnoted> Notes
    {
      get { return listCommonNotes; }
      set { listCommonNotes = value; }
    }

    #endregion INotesImplement Members
    // GET: MasterPensioner
    public ActionResult Index(string Tehsil = "", string BeneficiariesType = "", string Gender = "", string RegionNames = "", string AccountStatus = "", string bank_acct_no = "", string APPLICATION_REFERENCE_NO = "")
    {
      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.AsNoTracking().Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.AsNoTracking().Where(x => x.ControllerName == "MasterPensioner" && x.ActionName == "Index")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Beneficiary Details", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.AddPermission = model.AddPermssion;
        ViewBag.EditPermission = model.EditPermission;
        ViewBag.DeletePermission = model.DeletePermission;

      }
      try
      {
        List<DVOMasterEmployee> listSearch = new List<DVOMasterEmployee>();
        ViewBag.Group = GetUsersAssignedLocations();
        PensionProcessViewModel Paysearch = ShowActiveBatch();
        DVOMasterEmpTypes objDVOMasterEmpTypes = new DVOMasterEmpTypes();
        var defaultItem = new SelectListItem { Value = "ALL", Text = "ALL" };

        List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes)
                   .Select(s => new DVOMasterEmpTypes
                   {
                     type_code = s.type_code,
                     description = string.Format("{0} | {1}", s.type_code, s.description)
                   })
                   .ToList();

        listDVOMasterEmpTypes.Insert(0, new DVOMasterEmpTypes { type_code = "ALL", description = "ALL | ALL" }); // Insert the default item at the beginning

        var selectListItems = listDVOMasterEmpTypes
            .Select(empType => new SelectListItem
            {
              Value = empType.type_code,
              Text = empType.description
            })
            .ToList();
        objDVOMasterEmpTypes = null;
        ViewBag.BeneficiariesType = new SelectList(selectListItems, "Value", "Text", (string.IsNullOrEmpty(BeneficiariesType) ? null : BeneficiariesType));

        //var emplgenderList = db.MasterEmployees
        //.Where(x => x.gender != null)
        //.Select(x => new SelectListItem
        //{
        //    Value = x.gender,


        //})
        //.ToList();
        //if (string.IsNullOrEmpty(Gender))
        //{
        //    defaultItem.Selected = true;
        //    emplgenderList.Insert(0, defaultItem);
        //}
        //else
        //{
        //    emplgenderList.Insert(0, new SelectListItem { Value = Gender, Text = Gender, Selected = true });

        //}
        //ViewBag.Gender = emplgenderList;
        //ViewBag.ApplicantIFSCCode = new SelectList(db.MasterEmpBankDetails.AsNoTracking().GroupBy(x => x.APPLICANT_BANK_IFSC_CODE).Select(x => new { APPLICANT_BANK_IFSC_CODE = x.Key }).ToList(), "APPLICANT_BANK_IFSC_CODE", "APPLICANT_BANK_IFSC_CODE", string.Empty);

        //ViewBag.bank_acct_no = db.MasterEmpBankDetails.AsNoTracking().Where(x => x.bank_acct_no != null).Select(x => new SelectListItem
        //{
        //  Value = x.bank_acct_no,
        //  Text = x.bank_acct_no
        //}).ToList();

        //ViewBag.APPLICATION_REFERENCE_NO = db.MasterEmpBankDetails.AsNoTracking().Where(x => x.bank_acct_no != null).Select(x => new SelectListItem
        //{
        //  Value = x.APPLICATION_REFERENCE_NO,
        //  Text = x.APPLICATION_REFERENCE_NO
        //}).ToList();

        //var accountStatusList = db.MasterEmpBankDetails.AsNoTracking()
        //    .Where(x => x.ACCOUNT_STATUS != null)
        //    .Select(x => x.ACCOUNT_STATUS.ToString())
        //    .Distinct()
        //    .Select(status => new SelectListItem
        //    {
        //      Value = status,
        //      Text = status
        //    })
        //    .ToList();

        //accountStatusList.Insert(0, new SelectListItem { Value = "ALL", Text = "ALL" });

        return View();
      }
      catch (Exception ex)
      {
        throw;
      }

    }

    //Get Account no
    public JsonResult GetAccountNoAjax(string accountno)
   {
      try
      {
        if (string.IsNullOrWhiteSpace(accountno))
        {
          return Json(new { status = false, message = "Invalid account number" });
        }

        var AccountNo = db.MasterEmpBankDetails
            .Where(x => x.bank_acct_no.Contains(accountno.Trim()))
            .Select(x => new SelectListItem
            {
              Value = x.bank_acct_no,
              Text = x.bank_acct_no
            }).Take(1000)
            .ToList();

        return Json(AccountNo, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        return Json(new { status = false, message = ex.Message });
      }
    }



    //public JsonResult GetReferenceNosjax(string accountno)
    //{
    //    try
    //    {
    //        if (string.IsNullOrEmpty(accountno))
    //        {
    //            return Json(new { status = false, message = "Invalid account number" });
    //        }

    //        var AccountNo = db.MasterEmpBankDetails
    //            .Where(x => x.bank_acct_no.Contains(accountno))
    //            .Select(x => new SelectListItem
    //            {
    //                Value = x.bank_acct_no,
    //                Text = x.bank_acct_no
    //            })
    //            .ToList();

    //        return Json(AccountNo, JsonRequestBehavior.AllowGet);
    //    }
    //    catch (Exception ex)
    //    {

    //        return Json(new { status = false, message = ex.Message });
    //    }
    //}




    //get reference no
    public JsonResult GetReferenceNoAjax(string ReferenceNo)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(ReferenceNo))
        {
          return Json(new { status = false, message = "Invalid Reference number" });
        }

        var AccountNo = db.MasterEmpBankDetails
            .Where(x => x.APPLICATION_REFERENCE_NO.Contains(ReferenceNo))
            .Select(x => new SelectListItem
            {
              Value = x.APPLICATION_REFERENCE_NO,
              Text = x.APPLICATION_REFERENCE_NO
            }).Take(1000)
            .ToList();

        return Json(AccountNo, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {

        return Json(new { status = false, message = ex.Message });
      }
    }

    //public JsonResult GetAccountNosAjax(string AccountNo)
    //{
    //    try
    //    {
    //        if(string.IsNullOrEmpty(AccountNo))
    //        {
    //            return Json(new { status = false, message = "Invalid account No" });
    //        }
    //        var AccountNos = db.MasterEmpBankDetails.Where(x => x.bank_acct_no.Contains(AccountNo)).Select(x => new SelectListItem { Value = x.bank_acct_no,Text = x.bank_acct_no}).ToList();
    //        return Json(AccountNo,JsonRequestBehavior.AllowGet);
    //    }
    //    catch (Exception ex)
    //    {

    //        return Json(new { status = false, message = ex.Message });
    //    }
    //}

    public async Task<ActionResult> BeneficiaryAjaxHandler(JQueryDataTableParamModel param, string Tehsil = "", string BeneficiariesType = "", string Gender = "", string RegionNames = "", bool isDownload = false, string AccountStatus = "", string bank_acct_no = "", string APPLICATION_REFERENCE_NO = "", string CBS_NAME = "")

    {
      try
      {
        List<DVOMasterEmployee> listSearch = new List<DVOMasterEmployee>();


        if (isDownload)
        {
          param.iDisplayLength = 1000000;
        }
        int UserId = AppUserManager.GetUserId();
        int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
        if (AccountStatus == "ALL")
          AccountStatus = null;

        listSearch = BLLMasterEmployee.GetAllData(UserId, RoleId, Tehsil, BeneficiariesType, Gender, RegionNames, param.iDisplayStart, param.iDisplayLength, AccountStatus, bank_acct_no, APPLICATION_REFERENCE_NO, CBS_NAME).OrderBy(p=>p.City).ToList();

        // Apply search
        if (!string.IsNullOrEmpty(param.sSearch))
        {
          listSearch = listSearch
              .Where(c => c.ApplicationReferenceNo.ToLower().Contains(param.sSearch.ToLower())
                  || c.FirstName.ToLower().Contains(param.sSearch.ToLower())
                  || c.mailid.ToLower().Contains(param.sSearch.ToLower())
                  || c.Gender.ToLower().Contains(param.sSearch.ToLower())
                  || c.AgeInYears.ToString().ToLower().Contains(param.sSearch.ToLower())
                  //|| c.AccountNooftheApplicant.ToString().ToLower().Contains(param.sSearch.ToLower())
                  || c.Phone.ToString().ToLower().Contains(param.sSearch.ToLower())
                  || c.Address1.ToString().ToLower().Contains(param.sSearch.ToLower())
                  || c.IFSCCode.ToString().ToLower().Contains(param.sSearch.ToLower())
                  || c.HoldPayment.ToString().ToLower().Contains(param.sSearch.ToLower())
                  )
              .ToList();
        }

        if (isDownload)
        {
          FileContentResult bytesdata = JandKBeneficiaryReport(listSearch);
          return bytesdata;
        }


        Int32 totalRecords = listSearch.Count > 0 ? Convert.ToInt32(listSearch.Select(x => x.recordCount).FirstOrDefault()) : 0;

        var result = from x in listSearch
                     select new[]
                     {
                        x.ApplicationReferenceNo + "",//0
                        x.FirstName + "",//1
                        x.Gender + "",//2
                        x.AgeInYears + "",//3
                        x.Address1,//4
                        x.Phone,//5
                        x.mailid,//6
                        x.BankAcctNo,//7
                        x.IFSCCode,//8
                        x.BankName,//9
                        x.City.ToUpper(),//x.LastVerified,//10
                        x.type_desc,//11
                        x.MiddleName + "",//12
                        x.LastName + "",//13
                        x.NameoftheApplicant + "", //  14                    
                        // x.HoldPayment+"",
                        //x.ACCOUNT_STATUS+"",//15                        
                        x.ACCOUNT_STATUS.Trim() == "ACTIVE" ? "Validate" : "Not Validate",
                        //x.ACCOUNT_STATUS != "Active" ? "Validate" : "Not Validate",
                        TrimStart(x.ReasonForChange,"")+"",//16
                        x.HoldPayment+"",//17
                        UrlEncryption.EncryptURL(Convert.ToString(x.EmplCode)),//18
                        x.ACCOUNT_STATUS.Trim() == "ACTIVE" ? "Validate" : "Not Validate",//19
                        x.CBS_Name +"",  //20
                        //x.CBS_Status +"", //21
                       //x.CBS_Branch +"" , //22
                     };
        return Json(
            new
            {
              sEcho = param.sEcho,
              iTotalRecords = totalRecords,
              iTotalDisplayRecords = totalRecords,
              aaData = result,
            }, JsonRequestBehavior.AllowGet);

      }
      catch (Exception ex)
      {
        return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
      }
    }

    public byte[] CompressBytes(byte[] data)
    {
      using (var compressedStream = new MemoryStream())
      {
        using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress))
        {
          gzipStream.Write(data, 0, data.Length);
        }
        return compressedStream.ToArray();
      }
    }

    public string CompressString(string text)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(text);
      using (var compressedStream = new MemoryStream())
      {
        using (var gzip = new GZipStream(compressedStream, CompressionMode.Compress))
        {
          gzip.Write(bytes, 0, bytes.Length);
        }
        return Convert.ToBase64String(compressedStream.ToArray());
      }
    }

    public string TrimStart(string target, string trimString)
    {
      if (string.IsNullOrEmpty(trimString)) return target;

      if (string.IsNullOrEmpty(target)) return "";

      string result = target;
      while (result.StartsWith(trimString))
      {
        result = result.Substring(trimString.Length);
      }

      return result;
    }


    DVOPYBatchProcessStybatchr pObjBatch = null;
    private PensionProcessViewModel ShowActiveBatch()
    {
      try
      {
        PensionProcessViewModel Paysearch = new PensionProcessViewModel();
        Paysearch.PayrollDate = DateTime.Now;
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

          if (obj.searchcriteria.Trim().Length > 0)
          {

            string[] searchCriteria = obj.searchcriteria.Split(',');

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
                  Paysearch.LPayDate = DateTime.ParseExact(value, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                  Paysearch.LPayDateChecked = true;
                  break;
                case "End Of Period":
                  Paysearch.EOPDate = DateTime.ParseExact(value, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                  Paysearch.EOPDateChecked = true;
                  break;
                case "Payroll Date":
                  Paysearch.PayrollDate = DateTime.ParseExact(value, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
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
    /// <summary>
    /// Bind Combo Box of Cash-CheckingAccounts
    /// </summary>
    private void BindCombo_PayrollAccount()
    {
      //make object to pass as parameter of search function
      DVOOSCheckingAccount objDVOOSCheckingAccount = new DVOOSCheckingAccount();
      //call getDate function of BLL

      List<DVOOSCheckingAccount> listDVOOSCheckingAccount = BLLOSCheckingAccount.GetCheckingAccount(ref objDVOOSCheckingAccount);// (ref objDVOOSCheckingAccount);
      objDVOOSCheckingAccount = null;

      //check list is null or not
      if (listDVOOSCheckingAccount != null)
      {
        //make a blank object and insert into first position
        DVOOSCheckingAccount tmpDVOOSCheckingAccount = new DVOOSCheckingAccount();
        tmpDVOOSCheckingAccount.keyvalue = "-- Select --";
        tmpDVOOSCheckingAccount.acct_desc = string.Empty;
        tmpDVOOSCheckingAccount.acct_no = 0;
        listDVOOSCheckingAccount.Insert(0, tmpDVOOSCheckingAccount);
        if (objDvoUpdatePayableDefDetails.cd_cash_acct_no > 0)
        {
          //add default cash account
          tmpDVOOSCheckingAccount = new DVOOSCheckingAccount();
          tmpDVOOSCheckingAccount.keyvalue = objDvoUpdatePayableDefDetails.cd_keyvalue;
          tmpDVOOSCheckingAccount.acct_desc = objDvoUpdatePayableDefDetails.cd_accountdesc;
          tmpDVOOSCheckingAccount.acct_no = objDvoUpdatePayableDefDetails.cd_cash_acct_no;
          tmpDVOOSCheckingAccount.department = "000";
          listDVOOSCheckingAccount.Insert(1, tmpDVOOSCheckingAccount);
        }
        //check list has some items or not
        if (listDVOOSCheckingAccount.Count > 0)
        {
          //bind combo box with list
          /*mcgPayrollAccount0.DataSource = listDVOOSCheckingAccount;
          mcgPayrollAccount0.ColumnNames = "keyvalue;acct_desc;department;acct_no";
          mcgPayrollAccount0.ColumnWidths = "150;300;0;0";
          mcgPayrollAccount0.DisplayMember = "keyvalue";
          mcgPayrollAccount0.ValueMember = "acct_no";

          mcgPayrollAccount0.SelectedIndex = 0;*/
        }
      }
    }

    /// <summary>
    /// Bind MCGCombo Box of AccountTypes
    /// </summary>
    private void BindComboEmployeeType()
    {
      //make object to pass as parameter of search function
      DVOMasterEmpTypes objDVOMasterEmpTypes = new DVOMasterEmpTypes();
      //call getDate function of BLL
      List<DVOMasterEmpTypes> listDVOMasterEmpTypes0 = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes);
      objDVOMasterEmpTypes = null;
      //check list is null or not
      if (listDVOMasterEmpTypes0 != null)
      {
        //make a blank object and insert into first position
        DVOMasterEmpTypes tmpDVOMasterEmpTypes = new DVOMasterEmpTypes();
        listDVOMasterEmpTypes0.Insert(0, tmpDVOMasterEmpTypes);
        //check list has some items or not
        if (listDVOMasterEmpTypes0.Count > 0)
        {
          //bind combo box with list
          /*mcgEmployeeType0.DataSource = listDVOMasterEmpTypes0;
          mcgEmployeeType0.ColumnNames = "type_code;description;pay_period;empl_status;hold_pymnt";
          mcgEmployeeType0.ColumnWidths = "50;100;30;30;30";
          mcgEmployeeType0.DisplayMember = "type_code";
          mcgEmployeeType0.ValueMember = "type_code";
          mcgEmployeeType0.SelectedIndex = 0;*/
        }
      }
    }

    /// <summary>
    /// Bind Combo Box of Income codes
    /// </summary>
    private void BindCombo_IncomeCodes()
    {
      //make object to pass as parameter of search function
      DVOUpdateIncCode objDVOUpdateIncCode = new DVOUpdateIncCode();
      //call getDate function of BLL
      List<DVOUpdateIncCode> listDVOUpdateIncCode = BLLUpdateIncomeCodes.GetDetailedInfo(ref objDVOUpdateIncCode);
      //insert a blank object into first position
      listDVOUpdateIncCode.Insert(0, objDVOUpdateIncCode);
      //check list has some items or not
      if (listDVOUpdateIncCode.Count > 0)
      {
        //bind combo box with list
        /*mcgCombo.DataSource = listDVOUpdateIncCode;
        mcgCombo.ColumnNames = "inc_code;description;acct_no;dflt_rate;dflt_lo_inc_amt;dflt_hi_inc_amt;dfltaccounttype";
        mcgCombo.ColumnWidths = "50;200;0;50;0;0;0";
        mcgCombo.DisplayMember = "inc_code";
        mcgCombo.ValueMember = "inc_code";*/
      }
    }

    /// <summary>
    /// Bind Combo Box of Deduction codes
    /// </summary>
    private void BindCombo_DeductionCodes()
    {
      //make object to pass as parameter of search function
      DVOPRDeductionCodesMasterDedcodes objDVOPRDeductionCodesMasterDedcodes = new DVOPRDeductionCodesMasterDedcodes();
      //call getDate function of BLL
      List<DVOPRDeductionCodesMasterDedcodes> listDVOPRDeductionCodesMasterDedcodes = BLLPRDeductionCodesMasterDedcodes.GetData(ref objDVOPRDeductionCodesMasterDedcodes);

      //insert a blank object into first position
      listDVOPRDeductionCodesMasterDedcodes.Insert(0, objDVOPRDeductionCodesMasterDedcodes);
      //check list has some items or not
      if (listDVOPRDeductionCodesMasterDedcodes.Count > 0)
      {
        //bind combo box with list
        /*mcgCombo.DataSource = listDVOPRDeductionCodesMasterDedcodes;
        mcgCombo.ColumnNames = "ded_code;description;dflt_acct;dflt_rate;dflt_limit;dflt_pay_limit;yearrollover";
        mcgCombo.ColumnWidths = "50;200;0;0;0;0;0";
        mcgCombo.DisplayMember = "ded_code";
        mcgCombo.ValueMember = "ded_code";*/
      }
    }

    /// <summary>
    /// Bind Combo Box of Obligation codes
    /// </summary>
    private void BindCombo_ObligationCodes()
    {
      //make object to pass as parameter of search function
      DVOMasterOblCodes objDVOMasterOblCodes = new DVOMasterOblCodes();
      //call getDate function of BLL
      List<DVOMasterOblCodes> listDVOMasterOblCodes = BLLPRObligationCodesMasterOblCodes.GetData(ref objDVOMasterOblCodes);

      //insert a blank object into first position
      listDVOMasterOblCodes.Insert(0, objDVOMasterOblCodes);
      //check list has some items or not
      if (listDVOMasterOblCodes.Count > 0)
      {
        //bind combo box with list
        /*mcgCombo.DataSource = listDVOMasterOblCodes;
        mcgCombo.ColumnNames = "obl_code;description;dflt_rate;dflt_limit;dflt_acct;dflt_pay_limit;dflt_bacct;dfltaccounttype";
        mcgCombo.ColumnWidths = "50;200;0;0;0;0;0;0";
        mcgCombo.DisplayMember = "obl_code";
        mcgCombo.ValueMember = "obl_code";*/
      }
    }

    /// <summary>
    /// Bind Combo Box of Bank codes
    /// </summary>
    private void BindCombo_BankCodes()
    {
      //make object to pass as parameter of search function
      DVOBankCodesMasterBanks objDVOBankCodesMasterBanks = new DVOBankCodesMasterBanks();
      //call getDate function of BLL
      List<DVOBankCodesMasterBanks> listDVOBankCodesMasterBanks = BLLBankCodesMasterBanks.GetData(ref objDVOBankCodesMasterBanks);

      //insert a blank object into first position
      listDVOBankCodesMasterBanks.Insert(0, objDVOBankCodesMasterBanks);
      //check list has some items or not
      if (listDVOBankCodesMasterBanks.Count > 0)
      {
        //bind combo box with list
        /*mcgCombo.DataSource = listDVOBankCodesMasterBanks;
        mcgCombo.ColumnNames = "bank_code;bank_desc";
        mcgCombo.ColumnWidths = "50;200";
        mcgCombo.DisplayMember = "bank_code";
        mcgCombo.ValueMember = "bank_code";*/
      }
    }

    /// <summary>
    /// Bind Combo Box of Position codes
    /// </summary>
    private void BindCombo_PositionCodes()
    {
      //make object to pass as parameter of search function
      DVOUpdateSalaryPositions objDVOUpdateSalaryPositions = new DVOUpdateSalaryPositions();
      //call getDate function of BLL
      List<DVOUpdateSalaryPositions> listDVOUpdateSalaryPositions = BLLUpdateSalaryPositions.GetAllInfo();

      //insert a blank object into first position
      listDVOUpdateSalaryPositions.Insert(0, objDVOUpdateSalaryPositions);
      //check list has some items or not
      if (listDVOUpdateSalaryPositions.Count > 0)
      {
        //bind combo box with list
        /*mcgCombo.DataSource = listDVOUpdateSalaryPositions;
        mcgCombo.ColumnNames = "code;desc;dflt_cat_code;dflt_scale_code";
        mcgCombo.ColumnWidths = "50;200;0;0";
        mcgCombo.DisplayMember = "code";
        mcgCombo.ValueMember = "code";*/
      }
    }

    /// <summary>
    /// Bind Combo Box of Category codes
    /// </summary>
    private void BindCombo_CategoryCodes()
    {
      //make object to pass as parameter of search function
      DVOSalaryCategory objDVOSalaryCategory = new DVOSalaryCategory();
      //call getDate function of BLL
      List<DVOSalaryCategory> listDVOSalaryCategory = BLLSalaryCategory.GetSalaryCategory(ref objDVOSalaryCategory);

      //insert a blank object into first position
      listDVOSalaryCategory.Insert(0, objDVOSalaryCategory);
      //check list has some items or not
      if (listDVOSalaryCategory.Count > 0)
      {
        //bind combo box with list
        /*mcgCombo.DataSource = listDVOSalaryCategory;
        mcgCombo.ColumnNames = "Code;Desc";
        mcgCombo.ColumnWidths = "50;200";
        mcgCombo.DisplayMember = "Code";
        mcgCombo.ValueMember = "Code";*/
      }
    }

    /// <summary>
    /// Bind Combo Box of Scale codes
    /// </summary>
    private void BindCombo_ScaleCodes()
    {
      //make object to pass as parameter of search function
      DVOSalaryCode objDVOSalaryCode = new DVOSalaryCode();
      //call getDate function of BLL
      List<DVOSalaryCode> listDVOSalaryCode = BLLSalaryCode.GetSalaryCode(ref objDVOSalaryCode);

      //insert a blank object into first position
      listDVOSalaryCode.Insert(0, objDVOSalaryCode);
      //check list has some items or not
      if (listDVOSalaryCode.Count > 0)
      {
        //bind combo box with list
        /*mcgCombo.DataSource = listDVOSalaryCode;
        mcgCombo.ColumnNames = "Code;Per_anum";
        mcgCombo.ColumnWidths = "50;200";
        mcgCombo.DisplayMember = "Code";
        mcgCombo.ValueMember = "Code";*/
      }
    }

    /// <summary>
    /// Bind Combo Box of ApprovedBy
    /// </summary>
    private void BindCombo_ApprovedBy()
    {
      //make object to pass as parameter of search function
      DVOMasterEmployee objDVOMasterEmployee = new DVOMasterEmployee();
      //call getDate function of BLL
      int UserId = AppUserManager.GetUserId();
      int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
      List<DVOMasterEmployee> listDVOMasterEmployee = BLLMasterEmployee.GetAllData(UserId, RoleId);

      //insert a blank object into first position
      listDVOMasterEmployee.Insert(0, objDVOMasterEmployee);
      //check list has some items or not
      if (listDVOMasterEmployee.Count > 0)
      {
        //bind combo box with list
        /*mcgCombo.DataSource = listDVOMasterEmployee;
        mcgCombo.ColumnNames = "EmplCode;LastName;FirstName;MiddleName;JobTitle";
        mcgCombo.ColumnWidths = "50;100;100;100;100";
        mcgCombo.DisplayMember = "EmplCode";
        mcgCombo.ValueMember = "EmplCode";*/
      }
    }

    /// <summary>
    /// Bind Combo Boxes of SickLeaveIncomeCode,VacationIncomeCode,SickLeaveAccrual & VacationAccrual
    /// </summary>
    private void BindCombo_SickLeave_Vacation_IncomeCode_Accrual()
    {
      //make object to pass as parameter of search function
      DVOUpdateIncCode objDVOUpdateIncCode = new DVOUpdateIncCode();
      //call getDate function of BLL
      List<DVOUpdateIncCode> listDVOUpdateIncCode = BLLUpdateIncomeCodes.GetDetailedInfo(ref objDVOUpdateIncCode);

      //insert a blank object into first position
      listDVOUpdateIncCode.Insert(0, objDVOUpdateIncCode);
      //check list has some items or not
      if (listDVOUpdateIncCode.Count > 0)
      {
        //bind combo box with list
        /*mcgSickLeaveIncomeCode6.DataSource = listDVOUpdateIncCode;
        mcgSickLeaveIncomeCode6.ColumnNames = "inc_code;description;acct_no;dflt_rate";
        mcgSickLeaveIncomeCode6.ColumnWidths = "50;200;0;0";
        mcgSickLeaveIncomeCode6.DisplayMember = "inc_code";
        mcgSickLeaveIncomeCode6.ValueMember = "inc_code";*/

        //make copy of DVO-Obje//ctlist and bind with combo box
        List<DVOUpdateIncCode> listDVOUpdateIncCode2 = new List<DVOUpdateIncCode>(listDVOUpdateIncCode.ToArray());
        /*mcgVacationIncomeCode6.DataSource = listDVOUpdateIncCode2;
        mcgVacationIncomeCode6.ColumnNames = "inc_code;description;acct_no;dflt_rate";
        mcgVacationIncomeCode6.ColumnWidths = "50;200;0;0";
        mcgVacationIncomeCode6.DisplayMember = "inc_code";
        mcgVacationIncomeCode6.ValueMember = "inc_code";*/

        ////make copy of DVO-Obje//ctlist and bind with combo box
        //List<DVOUpdateIncCode> listDVOUpdateIncCode3 = new List<DVOUpdateIncCode>(listDVOUpdateIncCode.ToArray());
        //mcgSickAccrual6.DataSource = listDVOUpdateIncCode3;
        //mcgSickAccrual6.ColumnNames = "inc_code;description;acct_no;dflt_rate";
        //mcgSickAccrual6.ColumnWidths = "50;200;0;0";
        //mcgSickAccrual6.DisplayMember = "inc_code";
        //mcgSickAccrual6.ValueMember = "inc_code";

        ////make copy of DVO-Obje//ctlist and bind with combo box
        //List<DVOUpdateIncCode> listDVOUpdateIncCode4 = new List<DVOUpdateIncCode>(listDVOUpdateIncCode.ToArray());
        //mcgVacAccrual6.DataSource = listDVOUpdateIncCode4;
        //mcgVacAccrual6.ColumnNames = "inc_code;description;acct_no;dflt_rate";
        //mcgVacAccrual6.ColumnWidths = "50;200;0;0";
        //mcgVacAccrual6.DisplayMember = "inc_code";
        //mcgVacAccrual6.ValueMember = "inc_code";
      }
    }

    private void BindCombo_Accrual_Codes()
    {
      //make object to pass as parameter of search function
      DVOUpdAccrualCode objDVOUpdAccrualCode = new DVOUpdAccrualCode();
      //call getDate function of BLL
      List<DVOUpdAccrualCode> listDVOUpdAccrualCode = BLLUpdateAccrualCode.GetAccrualCodeAllSearch(ref objDVOUpdAccrualCode);

      //insert a blank object into first position
      listDVOUpdAccrualCode.Insert(0, objDVOUpdAccrualCode);
      //check list has some items or not
      if (listDVOUpdAccrualCode.Count > 0)
      {
        //bind combo box with list
        /*mcgSickAccrual6.DataSource = listDVOUpdAccrualCode;
        mcgSickAccrual6.ColumnNames = "accr_code;accr_desc";
        mcgSickAccrual6.ColumnWidths = "50;200";
        mcgSickAccrual6.DisplayMember = "accr_code";
        mcgSickAccrual6.ValueMember = "accr_code";*/

        //make copy of DVO-Obje//ctlist and bind with combo box
        List<DVOUpdAccrualCode> listDVOUpdAccrualCode2 = new List<DVOUpdAccrualCode>(listDVOUpdAccrualCode.ToArray());
        /*mcgVacAccrual6.DataSource = listDVOUpdAccrualCode2;
        mcgVacAccrual6.ColumnNames = "accr_code;accr_desc";
        mcgVacAccrual6.ColumnWidths = "50;200";
        mcgVacAccrual6.DisplayMember = "accr_code";
        mcgVacAccrual6.ValueMember = "accr_code";*/
      }
    }

    /// <summary>
    /// Bind Combo Boxes of StateTaxCode & LocalTaxCode
    /// </summary>
    private void BindCombo_State_Local_TaxCode()
    {
      //make object to pass as parameter of search function
      DVOPRDeductionCodesMasterDedcodes objDVOPRDeductionCodesMasterDedcodes = new DVOPRDeductionCodesMasterDedcodes();
      //call getDate function of BLL
      List<DVOPRDeductionCodesMasterDedcodes> listDVOPRDeductionCodesMasterDedcodes = BLLPRDeductionCodesMasterDedcodes.GetData(ref objDVOPRDeductionCodesMasterDedcodes);

      //insert a blank object into first position
      listDVOPRDeductionCodesMasterDedcodes.Insert(0, objDVOPRDeductionCodesMasterDedcodes);
      //check list has some items or not
      if (listDVOPRDeductionCodesMasterDedcodes.Count > 0)
      {
        //bind combo box with list
        /*mcgStateTaxCode6.DataSource = listDVOPRDeductionCodesMasterDedcodes;
        mcgStateTaxCode6.ColumnNames = "ded_code;description;dflt_acct;dflt_rate;yearrollover";
        mcgStateTaxCode6.ColumnWidths = "50;200;0;0;0";
        mcgStateTaxCode6.DisplayMember = "ded_code";
        mcgStateTaxCode6.ValueMember = "ded_code";*/

        //make copy of DVO-Obje//ctlist and bind with combo box
        List<DVOPRDeductionCodesMasterDedcodes> listDVOPRDeductionCodesMasterDedcodes2 = new List<DVOPRDeductionCodesMasterDedcodes>(listDVOPRDeductionCodesMasterDedcodes.ToArray());
        /*mcgLocatTaxCode6.DataSource = listDVOPRDeductionCodesMasterDedcodes2;
        mcgLocatTaxCode6.ColumnNames = "ded_code;description;dflt_acct;dflt_rate;yearrollover";
        mcgLocatTaxCode6.ColumnWidths = "50;200;0;0;0";
        mcgLocatTaxCode6.DisplayMember = "ded_code";
        mcgLocatTaxCode6.ValueMember = "ded_code";*/
      }
    }

    /// <summary>
    /// to Get Account's information based on assigned AccountNumber
    /// </summary>
    /// <param name="AccountNumber">Account Number for which you want to get information</param>
    /// <param name="KeyValue">out parameter to get KeyValue of account</param>
    /// <param name="AccountType">out parameter to get AccountType of account</param>
    /// <param name="AccountTypeId">out parameter to get AccountTypeId of account</param>
    /// <param name="AccountDescription">out parameter to get Description of account</param>
    private void GetAccountInformation(int AccountNumber, out string KeyValue, out string AccountType, out int AccountTypeId, out string AccountDescription)
    {
      //make object to send as parameter 
      DVOGeneralLedger objDVOGeneralLedger = new DVOGeneralLedger();
      //set account number of object for which you want to get information
      objDVOGeneralLedger.acct_no = AccountNumber;
      //call BLL class function to get information
      objDVOGeneralLedger = null;// ((List<DVOGeneralLedger>)BLLGeneralLedger.GetLedgerAccounts(objDVOGeneralLedger)).Count > 0 ? ((List<DVOGeneralLedger>)BLLGeneralLedger.GetLedgerAccounts(objDVOGeneralLedger))[0] : null;

      if (objDVOGeneralLedger != null)
      {
        //set variables with information 
        KeyValue = objDVOGeneralLedger.keyvalue;
        AccountType = objDVOGeneralLedger.acct_type;
        AccountTypeId = objDVOGeneralLedger.acct_type_id;
        AccountDescription = objDVOGeneralLedger.acct_desc;

        objDVOGeneralLedger = null;
      }
      else
      {
        KeyValue = string.Empty;
        AccountType = string.Empty;
        AccountTypeId = 0;
        AccountDescription = string.Empty;
      }
    }

    private System.Data.DataTable CreateDataTableForIncome()
    {
      System.Data.DataTable dtIncome = new System.Data.DataTable();
      dtIncome.Columns.Add("IncomeCode"); dtIncome.Columns.Add("Rate");
      dtIncome.Columns.Add("Number"); dtIncome.Columns.Add("Amount");
      dtIncome.Columns.Add("Hours"); dtIncome.Columns.Add("AccountNumber");
      dtIncome.Columns.Add("lineno"); dtIncome.Columns.Add("LowException");
      dtIncome.Columns.Add("HighException"); dtIncome.Columns.Add("Quarter1");
      dtIncome.Columns.Add("Quarter2"); dtIncome.Columns.Add("Quarter3");
      dtIncome.Columns.Add("Quarter4"); dtIncome.Columns.Add("YearToDate");
      dtIncome.Columns.Add("Keyvalue"); dtIncome.Columns.Add("accountid");
      dtIncome.Columns.Add("incaccounttype");
      return dtIncome;
    }

    private System.Data.DataTable CreateDataTableForDeduction()
    {
      System.Data.DataTable dtDeduction = new System.Data.DataTable();
      dtDeduction.Columns.Add("DeductionCode"); dtDeduction.Columns.Add("Rate");
      dtDeduction.Columns.Add("AnnualLimit"); dtDeduction.Columns.Add("PayLimit");
      dtDeduction.Columns.Add("Applied"); dtDeduction.Columns.Add("Frequency");
      dtDeduction.Columns.Add("AccountNumber"); dtDeduction.Columns.Add("lineno");
      dtDeduction.Columns.Add("LowException"); dtDeduction.Columns.Add("HighException");
      dtDeduction.Columns.Add("RollOver"); dtDeduction.Columns.Add("Balance");
      dtDeduction.Columns.Add("Quarter1"); dtDeduction.Columns.Add("Quarter2");
      dtDeduction.Columns.Add("Quarter3"); dtDeduction.Columns.Add("Quarter4");
      dtDeduction.Columns.Add("YearToDate"); dtDeduction.Columns.Add("Keyvalue");
      dtDeduction.Columns.Add("accountid"); dtDeduction.Columns.Add("dedaccounttype");
      return dtDeduction;
    }

    private System.Data.DataTable CreateDataTableForObligation()
    {
      System.Data.DataTable dtObligation = new System.Data.DataTable();
      dtObligation.Columns.Add("ObligationCode"); dtObligation.Columns.Add("Rate");
      dtObligation.Columns.Add("AnnualLimit"); dtObligation.Columns.Add("PayLimit");
      dtObligation.Columns.Add("ExpenseAccountNumber"); dtObligation.Columns.Add("lineno");
      dtObligation.Columns.Add("LiabilityAccountNumber"); dtObligation.Columns.Add("Quarter1");
      dtObligation.Columns.Add("Quarter2"); dtObligation.Columns.Add("Quarter3");
      dtObligation.Columns.Add("Quarter4"); dtObligation.Columns.Add("YearToDate");
      dtObligation.Columns.Add("ExpenseKeyvalue"); dtObligation.Columns.Add("LiabilityKeyvalue");
      dtObligation.Columns.Add("expaccountid"); dtObligation.Columns.Add("liabaccountid");
      dtObligation.Columns.Add("oblexpaccounttype"); dtObligation.Columns.Add("oblliabaccounttype");
      return dtObligation;
    }

    private System.Data.DataTable CreateDataTableForPositionHistory()
    {
      System.Data.DataTable dtPosHis = new System.Data.DataTable();
      dtPosHis.Columns.Add("PositionCode"); dtPosHis.Columns.Add("CategoryCode");
      dtPosHis.Columns.Add("ScaleCode"); dtPosHis.Columns.Add("StartDate");
      dtPosHis.Columns.Add("EndDate"); dtPosHis.Columns.Add("ApprovedBy");
      dtPosHis.Columns.Add("Rate"); dtPosHis.Columns.Add("Position");
      dtPosHis.Columns.Add("RowId"); dtPosHis.Columns.Add("PositionDescription");
      dtPosHis.Columns.Add("ApprovedByFirstName"); dtPosHis.Columns.Add("ApprovedByLastName");
      return dtPosHis;
    }

    private System.Data.DataTable CreateDataTableForDirectDeposit()
    {
      System.Data.DataTable dtDirDep = new System.Data.DataTable();
      dtDirDep.Columns.Add("BankCode"); dtDirDep.Columns.Add("BankDescription");
      dtDirDep.Columns.Add("CS"); dtDirDep.Columns.Add("AccountNo");
      dtDirDep.Columns.Add("T"); dtDirDep.Columns.Add("Amount");
      dtDirDep.Columns.Add("RowId");
      return dtDirDep;
    }

    private void GetAccountInfoForSelectedIncome(string IncomeCode, string IncomeAcctType, out int AccountNo, out int AccountTypeId, out string AccountDescription, out string AccountType, out string AccountKeyValue)
    {
      AccountNo = 0; AccountTypeId = 0;
      AccountDescription = string.Empty; AccountType = string.Empty; AccountKeyValue = string.Empty;

      DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
      objDVOFlexSegCommon.EntityType = "styinccr";
      objDVOFlexSegCommon.Code = IncomeCode;
      objDVOFlexSegCommon.AccountType = IncomeAcctType;
      string _keyvalue = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);
      string _flexdepartment = "To Do Text Box Payroll Department";//actPayrollDepartment0.KeyValue;

      //call flxMix function
      BLLPayrollFunctions objBLLPayrollFunctions = new BLLPayrollFunctions();
      objBLLPayrollFunctions.flxMix(IncomeAcctType, _keyvalue, "To Do Text Box Payroll Department", _flexdepartment,
              out AccountNo, out AccountType, out AccountKeyValue);
      if (AccountNo > 0)
        BLLCommonUtilities.GetAccountInformation(AccountNo, out AccountKeyValue, out AccountType, out AccountTypeId, out AccountDescription);
    }

    private void GetAccountInfoForSelectedObligation(string ObligationCode, string ObligationAcctType, out int AccountNo, out int AccountTypeId, out string AccountDescription, out string AccountType, out string AccountKeyValue)
    {
      AccountNo = 0; AccountTypeId = 0;
      AccountDescription = string.Empty; AccountType = string.Empty; AccountKeyValue = string.Empty;

      DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
      objDVOFlexSegCommon.EntityType = "MasterOblCodes";
      objDVOFlexSegCommon.Code = ObligationCode;
      objDVOFlexSegCommon.AccountType = ObligationAcctType;
      string _keyvalue = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);
      string _flexdepartment = "To Do Text Box Payroll Department";//actPayrollDepartment0.KeyValue;

      //call flxMix function
      BLLPayrollFunctions objBLLPayrollFunctions = new BLLPayrollFunctions();
      objBLLPayrollFunctions.flxMix(ObligationAcctType, _keyvalue, "To Do Text Box Payroll Department", _flexdepartment,
              out AccountNo, out AccountType, out AccountKeyValue);
      if (AccountNo > 0)
        BLLCommonUtilities.GetAccountInformation(AccountNo, out AccountKeyValue, out AccountType, out AccountTypeId, out AccountDescription);
    }

    #region Insertion

    /// <summary>
    /// To Insert data into database
    /// </summary>
    private int InsertEmployeeInformation(MasterPensioner objDVOMasterEmployeeModel, out string EmpCode, string actPayrollDepartmentKeyValue)
    {
      EmpCode = string.Empty;
      try
      {
        //make an object with values of appropriate properties to insert into database
        DVOMasterEmployee objDVOMasterEmployee = new DVOMasterEmployee();

        #region "Employee Information"
        //make object to pass as parameter of search function
        DVOMasterEmpTypes objDVOMasterEmpTypes = new DVOMasterEmpTypes();
        //call getDate function of BLL
        List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes);
        objDVOMasterEmpTypes = null;

        string cityname = null, marriage = "S";
        using (var _db = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
        {
          var marriagedetails = from x in _db.MasterContributorMarriageDetails where x.PersonId == objDVOMasterEmployeeModel.PersonID select new { x };
          marriage = marriagedetails == null ? "S" : "M";
        }
        using (var _db = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
        {
          var city = from x in _db.MasterCity where x.Id == objDVOMasterEmployeeModel.PermanentCityID select new { x.Name };
          cityname = city.Any() ? city.FirstOrDefault().Name : null;
        }

        objDVOMasterEmployee.EmplrCode = objDVOMasterEmployeeModel.EmployerID == null ? 0 : (int)objDVOMasterEmployeeModel.EmployerID;
        objDVOMasterEmployee.EmplCode = objDVOMasterEmployeeModel.PensionerID;
        objDVOMasterEmployee.HoldPayment = "N";// need to discuss about permission for hold payment//txtOnHold0.Text;
        objDVOMasterEmployee.SocSecNum = objDVOMasterEmployeeModel.SocialSecurityNo.ToString();
        objDVOMasterEmployee.FlexDeptAcctType = objDVOMasterEmployeeModel.FlexDeptAcctType == null ? "EXPENS" : objDVOMasterEmployeeModel.FlexDeptAcctType;
        if (listDVOMasterEmpTypes.Any())
        {
          objDVOMasterEmployee.TypeCode = listDVOMasterEmpTypes.FirstOrDefault().type_code;
          objDVOMasterEmployee.CashAcct = listDVOMasterEmpTypes.FirstOrDefault().cash_acct == 0 ? 1000 : listDVOMasterEmpTypes.FirstOrDefault().cash_acct;
        }
        objDVOMasterEmployee.LastName = objDVOMasterEmployeeModel.LastName;
        objDVOMasterEmployee.FirstName = objDVOMasterEmployeeModel.FirstName;
        objDVOMasterEmployee.MiddleName = objDVOMasterEmployeeModel.MidName;
        objDVOMasterEmployee.Address1 = objDVOMasterEmployeeModel.PermanentAddress;
        //objDVOMasterEmployee.Address2 = objDVOMasterEmployeeModel.Address2;
        objDVOMasterEmployee.City = cityname;
        //objDVOMasterEmployee.State = objDVOMasterEmployeeModel.State;
        //objDVOMasterEmployee.Zip = objDVOMasterEmployeeModel.Zip;
        objDVOMasterEmployee.Phone = objDVOMasterEmployeeModel.Phone;
        objDVOMasterEmployee.mailid = objDVOMasterEmployeeModel.Email;
        if (objDVOMasterEmployee.Birthdate != null)
          objDVOMasterEmployee.Birthdate = objDVOMasterEmployeeModel.DateOfBirth.ToString();
        objDVOMasterEmployee.Gender = objDVOMasterEmployeeModel.Gender;

        if (objDVOMasterEmployeeModel.LastPay != null)
          objDVOMasterEmployee.LastIncDate = objDVOMasterEmployeeModel.LastPay.ToString();// dtpLastIncrementDate0.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                                                                                          //if (objDVOMasterEmployeeModel.Terminated != "")
                                                                                          //objDVOMasterEmployee.Terminated = objDVOMasterEmployeeModel.Terminated;// dtpTerminationDate0.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        if (objDVOMasterEmployeeModel.FirstAppointmentDate != null)
          objDVOMasterEmployee.AppointDate = objDVOMasterEmployeeModel.FirstAppointmentDate.ToString();// dtpAppointmentDate0.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        objDVOMasterEmployee.Department = "000";
        objDVOMasterEmployee.Prefix = objDVOMasterEmployeeModel.Prefix != null ? objDVOMasterEmployeeModel.Prefix.Name : string.Empty;
        objDVOMasterEmployee.PostalAddress = objDVOMasterEmployeeModel.PostalAddress;
        objDVOMasterEmployee.Suffix = objDVOMasterEmployeeModel.Suffix != null ? objDVOMasterEmployeeModel.Suffix.Name : string.Empty;
        objDVOMasterEmployee.MaidenName = objDVOMasterEmployeeModel.MaidenName;
        objDVOMasterEmployee.Nationality = objDVOMasterEmployeeModel.Nationality != null ? objDVOMasterEmployeeModel.Nationality.Name : string.Empty;
        objDVOMasterEmployee.PhoneOffice = objDVOMasterEmployeeModel.PhoneOffice;
        objDVOMasterEmployee.Mobile = objDVOMasterEmployeeModel.Mobile;
        objDVOMasterEmployee.PensionerType = objDVOMasterEmployeeModel.PensionerTypeId != null ? objDVOMasterEmployeeModel.PensionerTypeId.ToString() : "1";
        objDVOMasterEmployee.PersonID = objDVOMasterEmployeeModel.PersonID != null ? objDVOMasterEmployeeModel.PersonID : string.Empty;


        #endregion "Employee Information"

        #region "Extended Information"
        //objDVOMasterEmployee.Allowances = objDVOMasterEmployeeModel.Allowances;// txtFedAllwncs6.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtFedAllwncs6.Text);
        objDVOMasterEmployee.PayPeriod = listDVOMasterEmpTypes.Any() ? listDVOMasterEmpTypes.FirstOrDefault().pay_period : "M";
        objDVOMasterEmployee.EmplStatus = "Y";// txtFullTime6.Text;
        if (objDVOMasterEmployeeModel.RetirementOrResignationDate != null)
          objDVOMasterEmployee.DateHired = objDVOMasterEmployeeModel.RetirementOrResignationDate.ToString();// dtpHired6.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                                                                                                            //objDVOMasterEmployee.StateAllow = objDVOMasterEmployeeModel.StateAllow;// txtStateAllwncs6.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtStateAllwncs6.Text);
        objDVOMasterEmployee.MaritalStat = marriage;
        //if (objDVOMasterEmployeeModel.StaTaxCode.Length > 0 && objDVOMasterEmployeeModel.StaTaxCode != null)
        //  objDVOMasterEmployee.StaTaxCode = objDVOMasterEmployeeModel.StaTaxCode;// mcgStateTaxCode6.SelectedValue.ToString();
        //if (objDVOMasterEmployee.LocTaxCode.Length > 0 && objDVOMasterEmployee.LocTaxCode != null)
        //  objDVOMasterEmployee.LocTaxCode = objDVOMasterEmployeeModel.LocTaxCode;// mcgLocatTaxCode6.SelectedValue.ToString();
        //if (objDVOMasterEmployee.LastPay!=null)
        //  objDVOMasterEmployee.LastPay = objDVOMasterEmployeeModel.LastPay;// dtpLastPay6.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        if (objDVOMasterEmployeeModel.SickCode != null)
          objDVOMasterEmployee.SickCode = objDVOMasterEmployeeModel.SickCode;// mcgSickLeaveIncomeCode6.SelectedValue.ToString();
        objDVOMasterEmployee.SickAllowed = objDVOMasterEmployeeModel.SickAllowed;// txtSickAccrued6.Text.Trim() == string.Empty ? null : (decimal?)Convert.ToDecimal(txtSickAccrued6.Text);
        objDVOMasterEmployee.SickUsed = objDVOMasterEmployeeModel.SickUsed;// txtSickUsed6.Text.Trim() == string.Empty ? null : (decimal?)Convert.ToDecimal(txtSickUsed6.Text);
        if (objDVOMasterEmployeeModel.VacCode != null)
          objDVOMasterEmployee.VacCode = objDVOMasterEmployeeModel.VacCode;// mcgVacationIncomeCode6.SelectedValue.ToString();
        objDVOMasterEmployee.VacAllowed = objDVOMasterEmployeeModel.VacAllowed;// txtVacAccrued6.Text.Trim() == string.Empty ? null : (decimal?)Convert.ToDecimal(txtVacAccrued6.Text);
        objDVOMasterEmployee.VacUsed = objDVOMasterEmployeeModel.VacUsed;// txtVacUsed6.Text.Trim() == string.Empty ? null : (decimal?)Convert.ToDecimal(txtVacUsed6.Text);
                                                                         //if (objDVOMasterEmployee.SickAccrCodr.Length > 0 && objDVOMasterEmployee.SickAccrCodr != null)
                                                                         //  objDVOMasterEmployee.SickAccrCodr = objDVOMasterEmployeeModel.SickAccrCodr;// mcgSickAccrual6.SelectedValue.ToString();
        objDVOMasterEmployee.SickAccrCtr = objDVOMasterEmployeeModel.SickAccrCtr;// txtSickCntr.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtSickCntr.Text);
        if (objDVOMasterEmployeeModel.VacAccrCode != null)
          objDVOMasterEmployee.VacAccrCode = objDVOMasterEmployeeModel.VacAccrCode;// mcgVacAccrual6.SelectedValue.ToString();
        objDVOMasterEmployee.VacAccrCtr = objDVOMasterEmployeeModel.VacAccrCtr;// txtVacCntr.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtVacCntr.Text);
        objDVOMasterEmployee.DirDept = objDVOMasterEmployeeModel.DirDept == "" || string.IsNullOrWhiteSpace(objDVOMasterEmployeeModel.DirDept) ? "N" : objDVOMasterEmployeeModel.DirDept;// txtDirectDeposit6.Text;

        objDVOMasterEmployee.InsertMachineInfo = System.Environment.MachineName;
        objDVOMasterEmployee.InsertBy = TempData["UserId"] == null ? 0 : Convert.ToInt32(TempData["UserId"]);
        objDVOMasterEmployee.InsertDate = System.DateTime.Now.ToString();
        #endregion "Extended Information"

        #region "Employee Income"

        //make object to pass as parameter of search function
        DVOUpdateIncCode objDVOUpdateIncCode = new DVOUpdateIncCode();
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
            if (drGrid.inc_code != null && drGrid.inc_code.Length > 0)
            {
              using (DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes = new DVOMasterEmployeeIncomes())
              {
                //assign appropriate values to detail-object
                objDVOMasterEmployeeIncomes.empl_code = objDVOMasterEmployeeModel.PersonID;
                objDVOMasterEmployeeIncomes.inc_code = drGrid.inc_code;
                objDVOMasterEmployeeIncomes.inc_rate = objDVOMasterEmployeeModel.AnnualAmount / 12;
                objDVOMasterEmployeeIncomes.inc_number = Convert.ToDecimal(1);
                objDVOMasterEmployeeIncomes.inc_hours = Convert.ToDecimal(1);
                if (drGrid.acct_no != null)
                  objDVOMasterEmployeeIncomes.acct_no = Convert.ToInt32(drGrid.acct_no);
                objDVOMasterEmployeeIncomes.acct_no_kv = "000";
                if (drGrid.dfltaccounttype != null)
                  objDVOMasterEmployeeIncomes.acct_no_type = drGrid.dfltaccounttype;

                objDVOMasterEmployeeIncomes.department = "000";
                objDVOMasterEmployeeIncomes.InsertMachineInfo = System.Environment.MachineName;
                objDVOMasterEmployeeIncomes.InsertBy = TempData["UserId"] == null ? 0 : Convert.ToInt32(TempData["UserId"]);
                objDVOMasterEmployeeIncomes.InsertDate = System.DateTime.Now.ToShortDateString();

                //add into list
                listDVOMasterEmployeeIncomes.Add(objDVOMasterEmployeeIncomes);
              }
            }
          }
        }


        #endregion "Employee Income"

        #region "Employee Deduction"

        //make list of detail-objects of main object
        List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions = new List<DVOMasterEmployeeDeductions>();
        ////check datagridview is enable or not if not, no need to add objects into list
        //if (dgvDeduction2.Enabled)
        //  //check datagridview has some items to add or not
        //  if (dgvDeduction2.Rows.Count > 0)
        //  {
        //    //if datagridview has some rows then make new object with values entered in rows
        //    foreach (DataGridViewRow drGrid in dgvDeduction2.Rows)
        //    {
        //      //check values entered into cells of row, if value is valid then add object into list
        //      //if (drGrid.Cells["DeductionCode2"].EditedFormattedValue != null)
        //      //    if (drGrid.Cells["DeductionCode2"].EditedFormattedValue.ToString().Trim().Length > 0 && drGrid.Cells["Rate2"].EditedFormattedValue != null)
        //      //        if (drGrid.Cells["Rate2"].EditedFormattedValue.ToString().Trim().Length > 0 && drGrid.Cells["AnnualLimit2"].EditedFormattedValue != null)
        //      //            if (drGrid.Cells["AnnualLimit2"].EditedFormattedValue.ToString().Trim().Length > 0)
        //      if (drGrid.Cells["DeductionCode2"].EditedFormattedValue != null && drGrid.Cells["DeductionCode2"].EditedFormattedValue.ToString().Trim().Length > 0)
        //      {
        //        using (DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions = new DVOMasterEmployeeDeductions())
        //        {
        //          //assign appropriate values to detail-object
        //          objDVOMasterEmployeeDeductions.empl_code = txtEmployeeIDCode0.Text;
        //          objDVOMasterEmployeeDeductions.ded_code = drGrid.Cells["DeductionCode2"].EditedFormattedValue.ToString();
        //          if (drGrid.Cells["Rate2"].EditedFormattedValue != null && drGrid.Cells["Rate2"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_rate = Convert.ToDecimal(drGrid.Cells["Rate2"].EditedFormattedValue);
        //          if (drGrid.Cells["AnnualLimit2"].EditedFormattedValue != null && drGrid.Cells["AnnualLimit2"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_limit = Convert.ToDecimal(drGrid.Cells["AnnualLimit2"].EditedFormattedValue);
        //          if (drGrid.Cells["PayLimit2"].EditedFormattedValue != null && drGrid.Cells["PayLimit2"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.pay_limit = Convert.ToDecimal(drGrid.Cells["PayLimit2"].EditedFormattedValue);

        //          //objDVOMasterEmployeeDeductions.ded_date = null;
        //          if (drGrid.Cells["Applied2"].EditedFormattedValue != null && drGrid.Cells["Applied2"].EditedFormattedValue.ToString().Trim().Length > 0)
        //          {
        //            string _dedDate = drGrid.Cells["Applied2"].EditedFormattedValue.ToString();
        //            if (_dedDate.Length == 10)
        //            {
        //              _dedDate = _dedDate.Substring(3, 3) + _dedDate.Substring(0, 3) + _dedDate.Substring(6);
        //              objDVOMasterEmployeeDeductions.ded_date = _dedDate;
        //            }
        //          }
        //          //if (drGrid.Cells[4].Value == null || drGrid.Cells[4].Value.ToString() == string.Empty)
        //          //    objDVOMasterEmployeeDeductions.ded_date = null;

        //          if (drGrid.Cells["Frequency2"].EditedFormattedValue != null && drGrid.Cells["Frequency2"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_apply = drGrid.Cells["Frequency2"].EditedFormattedValue.ToString();

        //          if (drGrid.Cells["AccountNumber2"].EditedFormattedValue != null && drGrid.Cells["AccountNumber2"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.acct_no = Convert.ToInt32(drGrid.Cells["AccountNumber2"].EditedFormattedValue);
        //          if (drGrid.Cells["Keyvalue2"].EditedFormattedValue != null)
        //            objDVOMasterEmployeeDeductions.acct_no_kv = drGrid.Cells["Keyvalue2"].EditedFormattedValue.ToString();
        //          if (drGrid.Cells["dedaccounttype"].EditedFormattedValue != null)
        //            objDVOMasterEmployeeDeductions.acct_no_type = drGrid.Cells["dedaccounttype"].EditedFormattedValue.ToString();
        //          //if (atxtDeductionAccountNumber.AccountNumber > 0)
        //          //    objDVOMasterEmployeeDeductions.acct_no = atxtDeductionAccountNumber.AccountNumber;

        //          if (drGrid.Cells["Quarter12"].EditedFormattedValue != null && drGrid.Cells["Quarter12"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_qtd1 = Convert.ToDecimal(drGrid.Cells["Quarter12"].EditedFormattedValue);
        //          if (drGrid.Cells["Quarter22"].EditedFormattedValue != null && drGrid.Cells["Quarter22"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_qtd2 = Convert.ToDecimal(drGrid.Cells["Quarter22"].EditedFormattedValue);
        //          if (drGrid.Cells["Quarter32"].EditedFormattedValue != null && drGrid.Cells["Quarter32"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_qtd3 = Convert.ToDecimal(drGrid.Cells["Quarter32"].EditedFormattedValue);
        //          if (drGrid.Cells["Quarter42"].EditedFormattedValue != null && drGrid.Cells["Quarter42"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_qtd4 = Convert.ToDecimal(drGrid.Cells["Quarter42"].EditedFormattedValue);
        //          if (drGrid.Cells["YearToDate2"].EditedFormattedValue != null && drGrid.Cells["YearToDate2"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_ytd = Convert.ToDecimal(drGrid.Cells["YearToDate2"].EditedFormattedValue);
        //          if (drGrid.Cells["LowException2"].EditedFormattedValue != null && drGrid.Cells["LowException2"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.lo_ded_amt = Convert.ToDecimal(drGrid.Cells["LowException2"].EditedFormattedValue);
        //          if (drGrid.Cells["HighException2"].EditedFormattedValue != null && drGrid.Cells["HighException2"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.hi_ded_amt = Convert.ToDecimal(drGrid.Cells["HighException2"].EditedFormattedValue);
        //          if (drGrid.Cells["Balance2"].EditedFormattedValue != null && drGrid.Cells["Balance2"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.balanceamt = Convert.ToDecimal(drGrid.Cells["Balance2"].EditedFormattedValue);

        //          objDVOMasterEmployeeDeductions.department = "000";
        //          objDVOMasterEmployeeDeductions.InsertMachineInfo = Program.MachineInfo;
        //          objDVOMasterEmployeeDeductions.InsertBy = Program.UserId;
        //          objDVOMasterEmployeeDeductions.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //          //add into list
        //          listDVOMasterEmployeeDeductions.Add(objDVOMasterEmployeeDeductions);
        //        }
        //      }
        //    }
        //  }

        #endregion "Employee Deduction"

        #region "Employee Obligation"

        ////make list of detail-objects of main object
        List<DVOMasterEmployeeObligations> listDVOMasterEmployeeObligations = new List<DVOMasterEmployeeObligations>();
        ////check datagridview is enable or not if not, no need to add objects into list
        //if (dgvObligation3.Enabled)
        //  //check datagridview has some items to add or not
        //  if (dgvObligation3.Rows.Count > 0)
        //  {
        //    //if datagridview has some rows then make new object with values entered in rows
        //    foreach (DataGridViewRow drGrid in dgvObligation3.Rows)
        //    {
        //      //check values entered into cells of row, if value is valid then add object into list
        //      //if (drGrid.Cells["ObligationCode3"].EditedFormattedValue != null)
        //      //    if (drGrid.Cells["ObligationCode3"].EditedFormattedValue.ToString().Trim().Length > 0 && drGrid.Cells["Rate3"].EditedFormattedValue != null)
        //      //        if (drGrid.Cells["Rate3"].EditedFormattedValue.ToString().Trim().Length > 0 && drGrid.Cells["AnnualLimit3"].EditedFormattedValue != null)
        //      //            if (drGrid.Cells["AnnualLimit3"].EditedFormattedValue.ToString().Trim().Length > 0)
        //      if (drGrid.Cells["ObligationCode3"].EditedFormattedValue != null && drGrid.Cells["ObligationCode3"].EditedFormattedValue.ToString().Trim().Length > 0)
        //      {
        //        using (DVOMasterEmployeeObligations objDVOMasterEmployeeObligations = new DVOMasterEmployeeObligations())
        //        {
        //          //assign appropriate values to detail-object
        //          objDVOMasterEmployeeObligations.empl_code = txtEmployeeIDCode0.Text;
        //          objDVOMasterEmployeeObligations.obl_code = drGrid.Cells["ObligationCode3"].EditedFormattedValue.ToString();
        //          if (drGrid.Cells["Rate3"].EditedFormattedValue != null && drGrid.Cells["Rate3"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_rate = Convert.ToDecimal(drGrid.Cells["Rate3"].EditedFormattedValue);
        //          if (drGrid.Cells["AnnualLimit3"].EditedFormattedValue != null && drGrid.Cells["AnnualLimit3"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_limit = Convert.ToDecimal(drGrid.Cells["AnnualLimit3"].EditedFormattedValue);
        //          if (drGrid.Cells["PayLimit3"].EditedFormattedValue != null && drGrid.Cells["PayLimit3"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.pay_limit = Convert.ToDecimal(drGrid.Cells["PayLimit3"].EditedFormattedValue);

        //          if (drGrid.Cells["ExpenseAccountNumber3"].EditedFormattedValue != null && drGrid.Cells["ExpenseAccountNumber3"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.acct_no = Convert.ToInt32(drGrid.Cells["ExpenseAccountNumber3"].EditedFormattedValue);
        //          if (drGrid.Cells["ExpenseKeyvalue3"].EditedFormattedValue != null)
        //            objDVOMasterEmployeeObligations.acct_no_kv = drGrid.Cells["ExpenseKeyvalue3"].EditedFormattedValue.ToString();
        //          if (drGrid.Cells["oblexpaccounttype"].EditedFormattedValue != null)
        //            objDVOMasterEmployeeObligations.acct_no_type = drGrid.Cells["oblexpaccounttype"].EditedFormattedValue.ToString();
        //          //if (atxtOblExpAccountNumber.AccountNumber > 0)
        //          //    objDVOMasterEmployeeObligations.acct_no = atxtOblExpAccountNumber.AccountNumber;

        //          if (drGrid.Cells["LiabilityAccountNumber3"].EditedFormattedValue != null && drGrid.Cells["LiabilityAccountNumber3"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.bal_acct_no = Convert.ToInt32(drGrid.Cells["LiabilityAccountNumber3"].EditedFormattedValue);
        //          if (drGrid.Cells["LiabilityKeyvalue3"].EditedFormattedValue != null)
        //            objDVOMasterEmployeeObligations.bal_acct_no_kv = drGrid.Cells["LiabilityKeyvalue3"].EditedFormattedValue.ToString();
        //          if (drGrid.Cells["oblliabaccounttype"].EditedFormattedValue != null)
        //            objDVOMasterEmployeeObligations.bal_acct_no_type = drGrid.Cells["oblliabaccounttype"].EditedFormattedValue.ToString();
        //          //if (atxtOblLiabAccountNumber.AccountNumber > 0)
        //          //    objDVOMasterEmployeeObligations.bal_acct_no = atxtOblLiabAccountNumber.AccountNumber;

        //          if (drGrid.Cells["Quarter13"].EditedFormattedValue != null && drGrid.Cells["Quarter13"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_qtd1 = Convert.ToDecimal(drGrid.Cells["Quarter13"].EditedFormattedValue);
        //          if (drGrid.Cells["Quarter23"].EditedFormattedValue != null && drGrid.Cells["Quarter23"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_qtd2 = Convert.ToDecimal(drGrid.Cells["Quarter23"].EditedFormattedValue);
        //          if (drGrid.Cells["Quarter33"].EditedFormattedValue != null && drGrid.Cells["Quarter33"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_qtd3 = Convert.ToDecimal(drGrid.Cells["Quarter33"].EditedFormattedValue);
        //          if (drGrid.Cells["Quarter43"].EditedFormattedValue != null && drGrid.Cells["Quarter43"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_qtd4 = Convert.ToDecimal(drGrid.Cells["Quarter43"].EditedFormattedValue);
        //          if (drGrid.Cells["YearToDate3"].EditedFormattedValue != null && drGrid.Cells["YearToDate3"].EditedFormattedValue.ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_ytd = Convert.ToDecimal(drGrid.Cells["YearToDate3"].EditedFormattedValue);

        //          objDVOMasterEmployeeObligations.department = "000";
        //          objDVOMasterEmployeeObligations.InsertMachineInfo = Program.MachineInfo;
        //          objDVOMasterEmployeeObligations.InsertBy = Program.UserId;
        //          objDVOMasterEmployeeObligations.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //          //add into list
        //          listDVOMasterEmployeeObligations.Add(objDVOMasterEmployeeObligations);
        //        }
        //      }
        //    }
        //  }

        #endregion "Employee Obligation"

        #region "Position History"

        ////make list of detail-objects of main object
        List<DVOPREmployeePositionHistoryInyemppd> listDVOPREmployeePositionHistoryInyemppd = new List<DVOPREmployeePositionHistoryInyemppd>();
        ////check datagridview is enable or not if not, no need to add objects into list
        //if (dgvPositionHistory.Enabled)
        //  //check datagridview has some items to add or not
        //  if (dgvPositionHistory.Rows.Count > 0)
        //  {
        //    //if datagridview has some rows then make new object with values entered in rows
        //    foreach (DataGridViewRow drGrid in dgvPositionHistory.Rows)
        //    {
        //      //check values entered into cells of row, if value is valid then add object into list
        //      if (drGrid.Cells["PositionCode4"].EditedFormattedValue != null)
        //        if (drGrid.Cells["PositionCode4"].EditedFormattedValue.ToString().Trim().Length > 0 && drGrid.Cells["CategoryCode4"].EditedFormattedValue != null)
        //          if (drGrid.Cells["CategoryCode4"].EditedFormattedValue.ToString().Trim().Length > 0 && drGrid.Cells["ScaleCode4"].EditedFormattedValue != null)
        //            if (drGrid.Cells["ScaleCode4"].EditedFormattedValue.ToString().Trim().Length > 0 && drGrid.Cells["StartDate4"].EditedFormattedValue != null)
        //              if (drGrid.Cells["StartDate4"].EditedFormattedValue.ToString().Trim().Length > 0 && drGrid.Cells["Position4"].EditedFormattedValue != null)
        //                if (drGrid.Cells["Position4"].EditedFormattedValue.ToString().Trim().Length > 0)
        //                {
        //                  using (DVOPREmployeePositionHistoryInyemppd objDVOPREmployeePositionHistoryInyemppd = new DVOPREmployeePositionHistoryInyemppd())
        //                  {
        //                    //assign appropriate values to detail-object
        //                    objDVOPREmployeePositionHistoryInyemppd.empl_code = txtEmployeeIDCode0.Text;
        //                    objDVOPREmployeePositionHistoryInyemppd.pos_code = drGrid.Cells["PositionCode4"].EditedFormattedValue.ToString();
        //                    objDVOPREmployeePositionHistoryInyemppd.cat_code = drGrid.Cells["CategoryCode4"].EditedFormattedValue.ToString();
        //                    objDVOPREmployeePositionHistoryInyemppd.scale_code = drGrid.Cells["ScaleCode4"].EditedFormattedValue.ToString();

        //                    objDVOPREmployeePositionHistoryInyemppd.start_date = null;
        //                    if (drGrid.Cells["StartDate4"].EditedFormattedValue != null && drGrid.Cells["StartDate4"].EditedFormattedValue.ToString().Trim().Length > 0)
        //                    {
        //                      string _startDate = drGrid.Cells["StartDate4"].EditedFormattedValue.ToString();
        //                      if (_startDate.Length == 10)
        //                      {
        //                        _startDate = _startDate.Substring(3, 2) + _startDate.Substring(2, 1) + _startDate.Substring(0, 2) + _startDate.Substring(5);
        //                        objDVOPREmployeePositionHistoryInyemppd.start_date = _startDate;
        //                      }
        //                    }
        //                    //objDVOPREmployeePositionHistoryInyemppd.start_date = drGrid.Cells[3].Value.ToString();
        //                    objDVOPREmployeePositionHistoryInyemppd.end_date = null;
        //                    if (drGrid.Cells["EndDate4"].EditedFormattedValue != null && drGrid.Cells["EndDate4"].EditedFormattedValue.ToString().Trim().Length > 0)
        //                    {
        //                      string _end_date = drGrid.Cells["EndDate4"].EditedFormattedValue.ToString();
        //                      if (_end_date.Length == 10)
        //                      {
        //                        _end_date = _end_date.Substring(3, 2) + _end_date.Substring(2, 1) + _end_date.Substring(0, 2) + _end_date.Substring(5);
        //                        objDVOPREmployeePositionHistoryInyemppd.end_date = _end_date;
        //                      }
        //                    }
        //                    //objDVOPREmployeePositionHistoryInyemppd.end_date = drGrid.Cells[4].Value.ToString();

        //                    if (drGrid.Cells["ApprovedBy4"].EditedFormattedValue != null && drGrid.Cells["ApprovedBy4"].EditedFormattedValue.ToString().Trim().Length > 0)
        //                      objDVOPREmployeePositionHistoryInyemppd.approved_by = drGrid.Cells["ApprovedBy4"].EditedFormattedValue.ToString();

        //                    if (drGrid.Cells["Rate4"].EditedFormattedValue != null && drGrid.Cells["Rate4"].EditedFormattedValue.ToString().Trim().Length > 0)
        //                      objDVOPREmployeePositionHistoryInyemppd.pay_rate = Convert.ToDecimal(drGrid.Cells["Rate4"].EditedFormattedValue);

        //                    if (drGrid.Cells["Position4"].EditedFormattedValue != null && drGrid.Cells["Position4"].EditedFormattedValue.ToString().Trim().Length > 0)
        //                      objDVOPREmployeePositionHistoryInyemppd.temporary = drGrid.Cells["Position4"].EditedFormattedValue.ToString();

        //                    objDVOPREmployeePositionHistoryInyemppd.InsertMachineInfo = Program.MachineInfo;
        //                    objDVOPREmployeePositionHistoryInyemppd.InsertBy = Program.UserId;
        //                    objDVOPREmployeePositionHistoryInyemppd.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //                    //add into list
        //                    listDVOPREmployeePositionHistoryInyemppd.Add(objDVOPREmployeePositionHistoryInyemppd);
        //                  }
        //                }
        //    }
        //  }

        #endregion "Position History"

        #region "Direct Deposit"

        ////make list of detail-objects of main object
        List<DVOMasterEmpBankDetails> listDVOMasterEmpBankDetails = new List<DVOMasterEmpBankDetails>();
        ////check datagridview is enable or not if not, no need to add objects into list
        //if (dgvDirectDeposit.Enabled)
        //  //check datagridview has some items to add or not
        //  if (dgvDirectDeposit.Rows.Count > 0)
        //  {
        //    //if datagridview has some rows then make new object with values entered in rows
        //    foreach (DataGridViewRow drGrid in dgvDirectDeposit.Rows)
        //    {
        //      //check values entered into cells of row, if value is valid then add object into list
        //      if (drGrid.Cells["BankCode5"].EditedFormattedValue != null)
        //        if (drGrid.Cells["BankCode5"].EditedFormattedValue.ToString().Trim().Length > 0 && drGrid.Cells["CS5"].EditedFormattedValue != null)
        //          if (drGrid.Cells["CS5"].EditedFormattedValue.ToString().Trim().Length > 0 && drGrid.Cells["AccountNo5"].EditedFormattedValue != null)
        //            if (drGrid.Cells["AccountNo5"].EditedFormattedValue.ToString().Trim().Length > 0 && drGrid.Cells["T5"].EditedFormattedValue != null)
        //              if (drGrid.Cells["T5"].EditedFormattedValue.ToString().Trim().Length > 0)
        //              {
        //                bool _ContinueProcess = false;
        //                if (drGrid.Cells["T5"].EditedFormattedValue.ToString().Trim().ToUpper() != "R")
        //                {
        //                  if (drGrid.Cells["Amount5"].EditedFormattedValue != null && drGrid.Cells["Amount5"].EditedFormattedValue.ToString().Trim().Length > 0)
        //                    _ContinueProcess = true;
        //                }
        //                else
        //                  _ContinueProcess = true;

        //                if (_ContinueProcess)
        //                  using (DVOMasterEmpBankDetails objDVOMasterEmpBankDetails = new DVOMasterEmpBankDetails())
        //                  {
        //                    //assign appropriate values to detail-object
        //                    objDVOMasterEmpBankDetails.empl_code = txtEmployeeIDCode0.Text;
        //                    objDVOMasterEmpBankDetails.line_no = 1;
        //                    objDVOMasterEmpBankDetails.bank_code = drGrid.Cells["BankCode5"].EditedFormattedValue.ToString();
        //                    objDVOMasterEmpBankDetails.bank_acct_no = drGrid.Cells["AccountNo5"].EditedFormattedValue.ToString();
        //                    objDVOMasterEmpBankDetails.type = drGrid.Cells["T5"].EditedFormattedValue.ToString();
        //                    objDVOMasterEmpBankDetails.typeofacct = drGrid.Cells["CS5"].EditedFormattedValue.ToString();

        //                    if (objDVOMasterEmpBankDetails.type.ToString().Trim().ToUpper() != "R")
        //                    {
        //                      if (drGrid.Cells["Amount5"].EditedFormattedValue != null && drGrid.Cells["Amount5"].EditedFormattedValue.ToString().Trim().Length > 0)
        //                        objDVOMasterEmpBankDetails.amount = Convert.ToDecimal(drGrid.Cells["Amount5"].EditedFormattedValue);
        //                    }

        //                    objDVOMasterEmpBankDetails.InsertMachineInfo = Program.MachineInfo;
        //                    objDVOMasterEmpBankDetails.InsertBy = Program.UserId;
        //                    objDVOMasterEmpBankDetails.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //                    //add into list
        //                    listDVOMasterEmpBankDetails.Add(objDVOMasterEmpBankDetails);
        //                  }
        //              }
        //    }
        //  }

        #endregion "Direct Deposit"

        #region "Employee Notes"

        //if (listEmployeeNotes.Count > 0)
        //    foreach (DVOstxnoted obj in listEmployeeNotes)
        //    {
        //        obj.filename = objDVOMasterEmployee.TABLE_NAME;
        //        obj.record_key = objDVOMasterEmployee.EmplCode;
        //    }

        //DVOstxnoted objDVOstxnoted = new DVOstxnoted();
        //objDVOstxnoted.filename = objDVOMasterEmployee.TABLE_NAME;
        //objDVOstxnoted.record_key = objDVOMasterEmployee.EmplCode;
        //objDVOstxnoted.line_no = 1;
        //objDVOstxnoted.data = _EmployeeNotes;

        #endregion "Employee Notes"

        //call insert function of BLL
        int i = BLLMasterEmployee.InsertEmployeeInformation(ref objDVOMasterEmployee,
            ref listDVOMasterEmployeeIncomes,
            ref listDVOMasterEmployeeDeductions,
            ref listDVOMasterEmployeeObligations,
            ref listDVOPREmployeePositionHistoryInyemppd,
            ref listDVOMasterEmpBankDetails,
            actPayrollDepartmentKeyValue,
            //ref listEmployeeNotes);
            ref listCommonNotes, out EmpCode);

        objDVOMasterEmployee = null;
        listDVOMasterEmployeeIncomes = null;
        listDVOMasterEmployeeDeductions = null;
        listDVOMasterEmployeeObligations = null;
        listDVOPREmployeePositionHistoryInyemppd = null;
        listDVOMasterEmpBankDetails = null;
        //if process is successfully completed, then set Success flag to true.
        return i;
      }
      catch (Exception ex)
      {
        //Success = false;
        ErrorMessage = ex.Message;
        return 0;
      }
    }

    #endregion Insertion

    #region Updation

    /// <summary>
    /// To Update selected Record after search
    /// </summary>
    public int UpdateEmployeeInformation(DVOMasterEmployee objDVOMasterEmployeeModel)
    {
      try
      {
        //make an object with values of appropriate properties to insert into database
        DVOMasterEmployee objDVOMasterEmployee = new DVOMasterEmployee();

        #region "Employee Information"
        //make object to pass as parameter of search function
        DVOMasterEmpTypes objDVOMasterEmpTypes = new DVOMasterEmpTypes();
        //call getDate function of BLL
        List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes);
        objDVOMasterEmpTypes = null;
        string cityname = null, marriage = "S";
        using (var _db = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
        {
          var marriagedetails = from x in _db.MasterContributorMarriageDetails where x.PersonId == objDVOMasterEmployeeModel.PersonID select new { x };
          marriage = marriagedetails == null ? "S" : "M";
        }
        using (var _db = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
        {
          var city = from x in _db.MasterCity where x.Name == objDVOMasterEmployeeModel.City select new { x.Name };
          cityname = city.Any() ? city.FirstOrDefault().Name : null;
        }

        objDVOMasterEmployee.EmplrCode = objDVOMasterEmployeeModel.EmplrCode == null ? 0 : (int)objDVOMasterEmployeeModel.EmplrCode;
        objDVOMasterEmployee.EmplCode = objDVOMasterEmployeeModel.EmplCode;
        objDVOMasterEmployee.HoldPayment = string.IsNullOrWhiteSpace(objDVOMasterEmployeeModel.HoldPayment) ? "N" : objDVOMasterEmployeeModel.HoldPayment;// "N";// need to discuss about permission for hold payment//txtOnHold0.Text;
        objDVOMasterEmployee.SocSecNum = objDVOMasterEmployeeModel.SocSecNum;
        objDVOMasterEmployee.FlexDeptAcctType = objDVOMasterEmployeeModel.FlexDeptAcctType == null ? "EXPENS" : objDVOMasterEmployeeModel.FlexDeptAcctType;
        if (listDVOMasterEmpTypes.Any())
        {
          int Emp_type_ID = Convert.ToInt32(objDVOMasterEmployeeModel.PensionerType);
          objDVOMasterEmployee.TypeCode = db.MasterEmpType.Where(x => x.Emp_type_ID == Emp_type_ID).FirstOrDefault()?.Type_Code;
          //objDVOMasterEmployee.TypeCode = objDVOMasterEmployeeModel.type_code;
          objDVOMasterEmployee.CashAcct = listDVOMasterEmpTypes.FirstOrDefault().cash_acct == 0 ? 1000 : listDVOMasterEmpTypes.FirstOrDefault().cash_acct;
        }
        objDVOMasterEmployee.LastName = objDVOMasterEmployeeModel.LastName;
        objDVOMasterEmployee.FirstName = objDVOMasterEmployeeModel.FirstName;
        objDVOMasterEmployee.MiddleName = objDVOMasterEmployeeModel.MiddleName;
        objDVOMasterEmployee.Address1 = objDVOMasterEmployeeModel.Address1;
        //objDVOMasterEmployee.Address2 = objDVOMasterEmployeeModel.Address2;
        objDVOMasterEmployee.City = objDVOMasterEmployeeModel.City;// city.Any() ? city.FirstOrDefault().Name : null;
                                                                   //objDVOMasterEmployee.State = objDVOMasterEmployeeModel.State;
                                                                   //objDVOMasterEmployee.Zip = objDVOMasterEmployeeModel.Zip;
        objDVOMasterEmployee.Phone = objDVOMasterEmployeeModel.Phone;
        objDVOMasterEmployee.mailid = objDVOMasterEmployeeModel.mailid;
        if (objDVOMasterEmployee.Birthdate != null)
          objDVOMasterEmployee.Birthdate = objDVOMasterEmployeeModel.Birthdate.ToString();
        objDVOMasterEmployee.Gender = objDVOMasterEmployeeModel.Gender;

        if (objDVOMasterEmployeeModel.LastPay != null)
          objDVOMasterEmployee.LastIncDate = objDVOMasterEmployeeModel.LastPay.ToString();// dtpLastIncrementDate0.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        if (objDVOMasterEmployeeModel.Terminated != "")
          objDVOMasterEmployee.Terminated = objDVOMasterEmployeeModel.Terminated;// dtpTerminationDate0.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        if (objDVOMasterEmployeeModel.AppointDate != null)
          objDVOMasterEmployee.AppointDate = objDVOMasterEmployeeModel.AppointDate.ToString();// dtpAppointmentDate0.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        objDVOMasterEmployee.Department = "000";
        objDVOMasterEmployee.Prefix = objDVOMasterEmployeeModel.Prefix;
        objDVOMasterEmployee.PostalAddress = objDVOMasterEmployeeModel.PostalAddress;
        objDVOMasterEmployee.Suffix = objDVOMasterEmployeeModel.Suffix;
        objDVOMasterEmployee.MaidenName = objDVOMasterEmployeeModel.MaidenName;
        objDVOMasterEmployee.Nationality = objDVOMasterEmployeeModel.Nationality != null ? (objDVOMasterEmployeeModel.Nationality).ToUpper() : string.Empty;
        objDVOMasterEmployee.PhoneOffice = objDVOMasterEmployeeModel.PhoneOffice;
        objDVOMasterEmployee.Mobile = objDVOMasterEmployeeModel.Mobile;


        //objDVOMasterEmployee.PensionerType = objDVOMasterEmployeeModel.PensionerType != null ? objDVOMasterEmployeeModel.PensionerType : "1";
        objDVOMasterEmployee.PensionerType = objDVOMasterEmployeeModel.PensionerType;

        // other new field ADD
        objDVOMasterEmployee.FatherOrHusbandOrGuardianName = objDVOMasterEmployeeModel.FatherOrHusbandOrGuardianName;
        objDVOMasterEmployee.PresentAddress = objDVOMasterEmployeeModel.PresentAddress;
        objDVOMasterEmployee.PresentDistrict = objDVOMasterEmployeeModel.PresentDistrict;
        objDVOMasterEmployee.PresentVillageName = objDVOMasterEmployeeModel.PresentVillageName;
        objDVOMasterEmployee.PermanentAddress = objDVOMasterEmployeeModel.PermanentAddress;
        //objDVOMasterEmployee.
        //= objDVOMasterEmployeeModel.District;
        objDVOMasterEmployee.SubmissionLocation = objDVOMasterEmployeeModel.SubmissionLocation;
        objDVOMasterEmployee.SubmissionDate = objDVOMasterEmployeeModel.SubmissionDate;
        objDVOMasterEmployee.SelectDistrict = objDVOMasterEmployeeModel.SelectDistrict;
        objDVOMasterEmployee.Category = objDVOMasterEmployeeModel.Category;
        objDVOMasterEmployee.LastTask = objDVOMasterEmployeeModel.LastTask;


        objDVOMasterEmployee.PersonID = objDVOMasterEmployeeModel.PersonID;
        // Add rohit
        objDVOMasterEmployee.ApplicationReferenceNo = objDVOMasterEmployeeModel.ApplicationReferenceNo;
        objDVOMasterEmployee.EMail = objDVOMasterEmployeeModel.EMail;
        objDVOMasterEmployee.PercentageofDisability = objDVOMasterEmployeeModel.PercentageofDisability;
        objDVOMasterEmployee.Zip = objDVOMasterEmployeeModel.Pincode;
        objDVOMasterEmployee.PresentHalqaPanchayatMunicipalityName = objDVOMasterEmployeeModel.PresentHalqaPanchayatMunicipalityName;
        objDVOMasterEmployee.PresentTehsil = objDVOMasterEmployeeModel.PresentTehsil;
        objDVOMasterEmployee.PermanentDistrict = objDVOMasterEmployeeModel.PermanentDistrict;
        objDVOMasterEmployee.PermanentTehsil = objDVOMasterEmployeeModel.PermanentTehsil;
        objDVOMasterEmployee.PermanentHalqaPanchayatMunicipalityName = objDVOMasterEmployeeModel.PermanentHalqaPanchayatMunicipalityName;
        objDVOMasterEmployee.PermanentVillageName = objDVOMasterEmployeeModel.PermanentVillageName;
        objDVOMasterEmployee.ApplicationSanctionedunderSchemeName = objDVOMasterEmployeeModel.ApplicationSanctionedunderSchemeName;
        objDVOMasterEmployee.CurrentTask = objDVOMasterEmployeeModel.CurrentTask;
        objDVOMasterEmployee.CurrentStatus = objDVOMasterEmployeeModel.CurrentStatus;
        //check
        objDVOMasterEmployee.AgeInYears = objDVOMasterEmployeeModel.AgeInYears;
        objDVOMasterEmployee.VersionNo = objDVOMasterEmployeeModel.VersionNo;
        //objDVOMasterEmployee.DoyouhaveBPLcard = (Convert.ToBoolean(objDVOMasterEmployeeModel.DoyouhaveBPLcard)==true)?"Yes":"No";
        objDVOMasterEmployee.DoyouhaveBPLcard = objDVOMasterEmployeeModel.DoyouhaveBPLcard;
        objDVOMasterEmployee.JKISSS = objDVOMasterEmployeeModel.JKISSS;
        objDVOMasterEmployee.CivilCondition = objDVOMasterEmployeeModel.CivilCondition;
        objDVOMasterEmployee.TSWO = objDVOMasterEmployeeModel.TSWO;
        #endregion "Employee Information"

        #region "Extended Information"
        //objDVOMasterEmployee.Allowances = objDVOMasterEmployeeModel.Allowances;// txtFedAllwncs6.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtFedAllwncs6.Text);
        objDVOMasterEmployee.PayPeriod = listDVOMasterEmpTypes.Any() ? listDVOMasterEmpTypes.FirstOrDefault().pay_period : "M";
        objDVOMasterEmployee.EmplStatus = objDVOMasterEmployeeModel.EmplStatus;//string.IsNullOrWhiteSpace(objDVOMasterEmployeeModel.HoldPayment) ? "N" : objDVOMasterEmployeeModel.HoldPayment;// txtFullTime6.Text;

        objDVOMasterEmployee.HoldPayment = string.IsNullOrWhiteSpace(objDVOMasterEmployeeModel.HoldPayment) ? "N" : objDVOMasterEmployeeModel.HoldPayment;// txtFullTime6.Text;

        if (objDVOMasterEmployeeModel.DateHired != null)
          objDVOMasterEmployee.DateHired = objDVOMasterEmployeeModel.DateHired.ToString();// dtpHired6.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                                                                                          //objDVOMasterEmployee.StateAllow = objDVOMasterEmployeeModel.StateAllow;// txtStateAllwncs6.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtStateAllwncs6.Text);
                                                                                          //objDVOMasterEmployee.MaritalStat = marriage;
        objDVOMasterEmployee.MaritalStat = objDVOMasterEmployeeModel.MaritalStat;

        //if (objDVOMasterEmployeeModel.StaTaxCode.Length > 0 && objDVOMasterEmployeeModel.StaTaxCode != null)
        //  objDVOMasterEmployee.StaTaxCode = objDVOMasterEmployeeModel.StaTaxCode;// mcgStateTaxCode6.SelectedValue.ToString();
        //if (objDVOMasterEmployee.LocTaxCode.Length > 0 && objDVOMasterEmployee.LocTaxCode != null)
        //  objDVOMasterEmployee.LocTaxCode = objDVOMasterEmployeeModel.LocTaxCode;// mcgLocatTaxCode6.SelectedValue.ToString();
        //if (objDVOMasterEmployee.LastPay!=null)
        //  objDVOMasterEmployee.LastPay = objDVOMasterEmployeeModel.LastPay;// dtpLastPay6.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        if (objDVOMasterEmployeeModel.SickCode != null)
          objDVOMasterEmployee.SickCode = objDVOMasterEmployeeModel.SickCode;// mcgSickLeaveIncomeCode6.SelectedValue.ToString();
        objDVOMasterEmployee.SickAllowed = objDVOMasterEmployeeModel.SickAllowed;// txtSickAccrued6.Text.Trim() == string.Empty ? null : (decimal?)Convert.ToDecimal(txtSickAccrued6.Text);
        objDVOMasterEmployee.SickUsed = objDVOMasterEmployeeModel.SickUsed;// txtSickUsed6.Text.Trim() == string.Empty ? null : (decimal?)Convert.ToDecimal(txtSickUsed6.Text);
        if (objDVOMasterEmployeeModel.VacCode != null)
          objDVOMasterEmployee.VacCode = objDVOMasterEmployeeModel.VacCode;// mcgVacationIncomeCode6.SelectedValue.ToString();
        objDVOMasterEmployee.VacAllowed = objDVOMasterEmployeeModel.VacAllowed;// txtVacAccrued6.Text.Trim() == string.Empty ? null : (decimal?)Convert.ToDecimal(txtVacAccrued6.Text);
        objDVOMasterEmployee.VacUsed = objDVOMasterEmployeeModel.VacUsed;// txtVacUsed6.Text.Trim() == string.Empty ? null : (decimal?)Convert.ToDecimal(txtVacUsed6.Text);
                                                                         //if (objDVOMasterEmployee.SickAccrCodr.Length > 0 && objDVOMasterEmployee.SickAccrCodr != null)
                                                                         //  objDVOMasterEmployee.SickAccrCodr = objDVOMasterEmployeeModel.SickAccrCodr;// mcgSickAccrual6.SelectedValue.ToString();
        objDVOMasterEmployee.SickAccrCtr = objDVOMasterEmployeeModel.SickAccrCtr;// txtSickCntr.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtSickCntr.Text);
        if (objDVOMasterEmployeeModel.VacAccrCode != null)
          objDVOMasterEmployee.VacAccrCode = objDVOMasterEmployeeModel.VacAccrCode;// mcgVacAccrual6.SelectedValue.ToString();
        objDVOMasterEmployee.VacAccrCtr = objDVOMasterEmployeeModel.VacAccrCtr;// txtVacCntr.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtVacCntr.Text);
        objDVOMasterEmployee.DirDept = objDVOMasterEmployeeModel.DirDept;// txtDirectDeposit6.Text;

        objDVOMasterEmployee.actPayrollDepartmentKeyValue = objDVOMasterEmployeeModel.actPayrollDepartmentKeyValue;

        objDVOMasterEmployee.InsertMachineInfo = System.Environment.MachineName;
        objDVOMasterEmployee.InsertBy = TempData["UserId"] == null ? 0 : Convert.ToInt32(TempData["UserId"]);
        objDVOMasterEmployee.InsertDate = System.DateTime.Now.ToString();
        objDVOMasterEmployee.ReasonForChange = "[" + AppUserManager.GetUserName() + "] : " + objDVOMasterEmployeeModel.ReasonForChange;
        objDVOMasterEmployee.Last_pay_date = objDVOMasterEmployeeModel.Last_pay_date;
        objDVOMasterEmployee.Application_approve_on = objDVOMasterEmployeeModel.Application_approve_on;
        #endregion "Extended Information"

        #region "Employee Income"

        //make list of detail-objects of main object
        List<DVOMasterEmployeeIncomes> listDVOMasterEmployeeIncomes = new List<DVOMasterEmployeeIncomes>();
        List<DVOMasterEmployeeIncomes> listNewDVOMasterEmployeeIncomes = new List<DVOMasterEmployeeIncomes>();
        List<DVOMasterEmployeeIncomes> listDeleteDVOMasterEmployeeIncomes = new List<DVOMasterEmployeeIncomes>();

        //if (dgvIncome1.DataSource is DataTable)
        //{
        //  DataTable dtincome = (DataTable)dgvIncome1.DataSource;
        //  foreach (DataRow dr in dtincome.Rows)
        //  {
        //    if (dr.RowState == DataRowState.Deleted)
        //    {
        //      if (dr["lineno", DataRowVersion.Original] != DBNull.Value && dr["lineno", DataRowVersion.Original].ToString().Trim().Length > 0)
        //      {
        //        using (DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes = new DVOMasterEmployeeIncomes())
        //        {
        //          objDVOMasterEmployeeIncomes.line_no = Convert.ToInt32(dr["lineno", DataRowVersion.Original]);
        //          objDVOMasterEmployeeIncomes.empl_code = txtEmployeeIDCode0.Text.Trim();
        //          if (dr["AccountNumber", DataRowVersion.Original] != DBNull.Value && dr["AccountNumber", DataRowVersion.Original].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.acct_no = Convert.ToInt32(dr["AccountNumber", DataRowVersion.Original]);
        //          if (dr["Keyvalue", DataRowVersion.Original] != DBNull.Value)
        //            objDVOMasterEmployeeIncomes.acct_no_kv = dr["Keyvalue", DataRowVersion.Original].ToString().Trim();
        //          if (dr["incaccounttype", DataRowVersion.Original] != DBNull.Value)
        //            objDVOMasterEmployeeIncomes.acct_no_type = dr["incaccounttype", DataRowVersion.Original].ToString().Trim();
        //          listDeleteDVOMasterEmployeeIncomes.Add(objDVOMasterEmployeeIncomes);
        //        }
        //      }
        //    }
        //    else if (dr.RowState == DataRowState.Added)
        //    {
        //      if (dr["IncomeCode"] != DBNull.Value && dr["IncomeCode"].ToString().Trim().Length > 0)
        //      {
        //        using (DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes = new DVOMasterEmployeeIncomes())
        //        {
        //          //assign appropriate values to detail-object
        //          if (dr["lineno"] != DBNull.Value && dr["lineno"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.line_no = Convert.ToInt32(dr["lineno"]);
        //          objDVOMasterEmployeeIncomes.empl_code = txtEmployeeIDCode0.Text.Trim();
        //          objDVOMasterEmployeeIncomes.inc_code = dr["IncomeCode"].ToString().Trim();
        //          if (dr["Rate"] != DBNull.Value && dr["Rate"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_rate = Convert.ToDecimal(dr["Rate"]);
        //          if (dr["Number"] != DBNull.Value && dr["Number"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_number = Convert.ToDecimal(dr["Number"]);
        //          if (dr["Hours"] != DBNull.Value && dr["Hours"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_hours = Convert.ToDecimal(dr["Hours"]);

        //          if (dr["AccountNumber"] != DBNull.Value && dr["AccountNumber"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.acct_no = Convert.ToInt32(dr["AccountNumber"]);
        //          if (dr["Keyvalue"] != DBNull.Value)
        //            objDVOMasterEmployeeIncomes.acct_no_kv = dr["Keyvalue"].ToString().Trim();
        //          if (dr["incaccounttype"] != DBNull.Value)
        //            objDVOMasterEmployeeIncomes.acct_no_type = dr["incaccounttype"].ToString().Trim();
        //          //if (atxtIncomeAccountNumber.AccountNumber > 0)
        //          //    objDVOMasterEmployeeIncomes.acct_no = atxtIncomeAccountNumber.AccountNumber;

        //          if (dr["Quarter1"] != DBNull.Value && dr["Quarter1"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_qtd1 = Convert.ToDecimal(dr["Quarter1"]);
        //          if (dr["Quarter2"] != DBNull.Value && dr["Quarter2"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_qtd2 = Convert.ToDecimal(dr["Quarter2"]);
        //          if (dr["Quarter3"] != DBNull.Value && dr["Quarter3"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_qtd3 = Convert.ToDecimal(dr["Quarter3"]);
        //          if (dr["Quarter4"] != DBNull.Value && dr["Quarter4"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_qtd4 = Convert.ToDecimal(dr["Quarter4"]);
        //          if (dr["YearToDate"] != DBNull.Value && dr["YearToDate"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_ytd = Convert.ToDecimal(dr["YearToDate"]);
        //          if (dr["LowException"] != DBNull.Value && dr["LowException"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.lo_inc_amt = Convert.ToDecimal(dr["LowException"]);
        //          if (dr["HighException"] != DBNull.Value && dr["HighException"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.hi_inc_amt = Convert.ToDecimal(dr["HighException"]);

        //          objDVOMasterEmployeeIncomes.department = "000";
        //          objDVOMasterEmployeeIncomes.UpdateMachineInfo = Program.MachineInfo;
        //          objDVOMasterEmployeeIncomes.UpdateBy = Program.UserId;
        //          objDVOMasterEmployeeIncomes.UpdateDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //          objDVOMasterEmployeeIncomes.InsertMachineInfo = Program.MachineInfo;
        //          objDVOMasterEmployeeIncomes.InsertBy = Program.UserId;
        //          objDVOMasterEmployeeIncomes.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //          //add into list
        //          listNewDVOMasterEmployeeIncomes.Add(objDVOMasterEmployeeIncomes);
        //        }
        //      }
        //    }
        //    else if (dr.RowState == DataRowState.Modified)
        //    {
        //      bool _rowDelete = true;
        //      if (dr["IncomeCode"] != DBNull.Value && dr["IncomeCode"].ToString().Trim().Length > 0)
        //      {
        //        _rowDelete = false;
        //        using (DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes = new DVOMasterEmployeeIncomes())
        //        {
        //          //assign appropriate values to detail-object
        //          if (dr["lineno"] != DBNull.Value && dr["lineno"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.line_no = Convert.ToInt32(dr["lineno"]);
        //          objDVOMasterEmployeeIncomes.empl_code = txtEmployeeIDCode0.Text.Trim();
        //          objDVOMasterEmployeeIncomes.inc_code = dr["IncomeCode"].ToString().Trim();
        //          if (dr["Rate"] != DBNull.Value && dr["Rate"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_rate = Convert.ToDecimal(dr["Rate"]);
        //          if (dr["Number"] != DBNull.Value && dr["Number"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_number = Convert.ToDecimal(dr["Number"]);
        //          if (dr["Hours"] != DBNull.Value && dr["Hours"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_hours = Convert.ToDecimal(dr["Hours"]);

        //          if (dr["AccountNumber"] != DBNull.Value && dr["AccountNumber"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.acct_no = Convert.ToInt32(dr["AccountNumber"]);
        //          if (dr["Keyvalue"] != DBNull.Value)
        //            objDVOMasterEmployeeIncomes.acct_no_kv = dr["Keyvalue"].ToString().Trim();
        //          if (dr["incaccounttype"] != DBNull.Value)
        //            objDVOMasterEmployeeIncomes.acct_no_type = dr["incaccounttype"].ToString().Trim();
        //          //if (atxtIncomeAccountNumber.AccountNumber > 0)
        //          //    objDVOMasterEmployeeIncomes.acct_no = atxtIncomeAccountNumber.AccountNumber;

        //          if (dr["Quarter1"] != DBNull.Value && dr["Quarter1"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_qtd1 = Convert.ToDecimal(dr["Quarter1"]);
        //          if (dr["Quarter2"] != DBNull.Value && dr["Quarter2"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_qtd2 = Convert.ToDecimal(dr["Quarter2"]);
        //          if (dr["Quarter3"] != DBNull.Value && dr["Quarter3"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_qtd3 = Convert.ToDecimal(dr["Quarter3"]);
        //          if (dr["Quarter4"] != DBNull.Value && dr["Quarter4"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_qtd4 = Convert.ToDecimal(dr["Quarter4"]);
        //          if (dr["YearToDate"] != DBNull.Value && dr["YearToDate"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.inc_ytd = Convert.ToDecimal(dr["YearToDate"]);
        //          if (dr["LowException"] != DBNull.Value && dr["LowException"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.lo_inc_amt = Convert.ToDecimal(dr["LowException"]);
        //          if (dr["HighException"] != DBNull.Value && dr["HighException"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeIncomes.hi_inc_amt = Convert.ToDecimal(dr["HighException"]);

        //          objDVOMasterEmployeeIncomes.department = "000";
        //          objDVOMasterEmployeeIncomes.UpdateMachineInfo = Program.MachineInfo;
        //          objDVOMasterEmployeeIncomes.UpdateBy = Program.UserId;
        //          objDVOMasterEmployeeIncomes.UpdateDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //          objDVOMasterEmployeeIncomes.InsertMachineInfo = Program.MachineInfo;
        //          objDVOMasterEmployeeIncomes.InsertBy = Program.UserId;
        //          objDVOMasterEmployeeIncomes.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //          //add into list
        //          listDVOMasterEmployeeIncomes.Add(objDVOMasterEmployeeIncomes);
        //        }
        //      }

        //      if (dr["lineno"] != DBNull.Value && dr["lineno"].ToString().Trim().Length > 0)
        //        if (_rowDelete)
        //        {
        //          using (DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes = new DVOMasterEmployeeIncomes())
        //          {
        //            objDVOMasterEmployeeIncomes.line_no = Convert.ToInt32(dr["lineno"]);
        //            objDVOMasterEmployeeIncomes.empl_code = txtEmployeeIDCode0.Text.Trim();
        //            if (dr["AccountNumber"] != DBNull.Value && dr["AccountNumber"].ToString().Trim().Length > 0)
        //              objDVOMasterEmployeeIncomes.acct_no = Convert.ToInt32(dr["AccountNumber"]);
        //            if (dr["Keyvalue"] != DBNull.Value)
        //              objDVOMasterEmployeeIncomes.acct_no_kv = dr["Keyvalue"].ToString().Trim();
        //            if (dr["incaccounttype"] != DBNull.Value)
        //              objDVOMasterEmployeeIncomes.acct_no_type = dr["incaccounttype"].ToString().Trim();
        //            listDeleteDVOMasterEmployeeIncomes.Add(objDVOMasterEmployeeIncomes);
        //          }
        //        }
        //    }
        //  }
        //}


        #endregion "Employee Income"

        #region "Employee Deduction"

        //make list of detail-objects of main object
        List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions = new List<DVOMasterEmployeeDeductions>();
        List<DVOMasterEmployeeDeductions> listDeleteDVOMasterEmployeeDeductions = new List<DVOMasterEmployeeDeductions>();
        List<DVOMasterEmployeeDeductions> listNewDVOMasterEmployeeDeductions = new List<DVOMasterEmployeeDeductions>();

        //if (dgvDeduction2.DataSource is DataTable)
        //{

        //  DataTable dtdeduction = (DataTable)dgvDeduction2.DataSource;
        //  foreach (DataRow dr in dtdeduction.Rows)
        //  {
        //    if (dr.RowState == DataRowState.Deleted)
        //    {
        //      if (dr["lineno", DataRowVersion.Original] != DBNull.Value && dr["lineno", DataRowVersion.Original].ToString().Trim().Length > 0)
        //      {
        //        using (DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions = new DVOMasterEmployeeDeductions())
        //        {
        //          objDVOMasterEmployeeDeductions.line_no = Convert.ToInt32(dr["lineno", DataRowVersion.Original]);
        //          objDVOMasterEmployeeDeductions.empl_code = txtEmployeeIDCode0.Text.Trim();
        //          if (dr["AccountNumber", DataRowVersion.Original] != DBNull.Value && dr["AccountNumber", DataRowVersion.Original].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.acct_no = Convert.ToInt32(dr["AccountNumber", DataRowVersion.Original]);
        //          if (dr["Keyvalue", DataRowVersion.Original] != DBNull.Value)
        //            objDVOMasterEmployeeDeductions.acct_no_kv = dr["Keyvalue", DataRowVersion.Original].ToString().Trim();
        //          if (dr["dedaccounttype", DataRowVersion.Original] != DBNull.Value)
        //            objDVOMasterEmployeeDeductions.acct_no_type = dr["dedaccounttype", DataRowVersion.Original].ToString().Trim();
        //          listDeleteDVOMasterEmployeeDeductions.Add(objDVOMasterEmployeeDeductions);
        //        }
        //      }
        //    }
        //    else if (dr.RowState == DataRowState.Added)
        //    {
        //      if (dr["DeductionCode"] != DBNull.Value && dr["DeductionCode"].ToString().Trim().Length > 0)
        //      {
        //        using (DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions = new DVOMasterEmployeeDeductions())
        //        {
        //          //assign appropriate values to detail-object
        //          if (dr["lineno"] != DBNull.Value && dr["lineno"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.line_no = Convert.ToInt32(dr["lineno"]);
        //          objDVOMasterEmployeeDeductions.empl_code = txtEmployeeIDCode0.Text.Trim();
        //          objDVOMasterEmployeeDeductions.ded_code = dr["DeductionCode"].ToString().Trim();
        //          if (dr["Rate"] != DBNull.Value && dr["Rate"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_rate = Convert.ToDecimal(dr["Rate"]);
        //          if (dr["AnnualLimit"] != DBNull.Value && dr["AnnualLimit"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_limit = Convert.ToDecimal(dr["AnnualLimit"]);
        //          if (dr["PayLimit"] != DBNull.Value && dr["PayLimit"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.pay_limit = Convert.ToDecimal(dr["PayLimit"]);

        //          //objDVOMasterEmployeeDeductions.ded_date = null;
        //          if (dr["Applied"] != DBNull.Value && dr["Applied"].ToString().Trim().Length > 0)
        //          {
        //            string _DeductDate = dr["Applied"].ToString().Trim();
        //            if (_DeductDate.Length == 10)
        //              objDVOMasterEmployeeDeductions.ded_date = _DeductDate.Substring(3, 3) + _DeductDate.Substring(0, 3) + _DeductDate.Substring(6, 4);
        //          }

        //          if (dr["Frequency"] != DBNull.Value && dr["Frequency"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_apply = dr["Frequency"].ToString().Trim();
        //          if (dr["AccountNumber"] != DBNull.Value && dr["AccountNumber"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.acct_no = Convert.ToInt32(dr["AccountNumber"]);
        //          if (dr["Keyvalue"] != DBNull.Value)
        //            objDVOMasterEmployeeDeductions.acct_no_kv = dr["Keyvalue"].ToString().Trim();
        //          if (dr["dedaccounttype"] != DBNull.Value)
        //            objDVOMasterEmployeeDeductions.acct_no_type = dr["dedaccounttype"].ToString().Trim();
        //          //if (atxtDeductionAccountNumber.AccountNumber > 0)
        //          //    objDVOMasterEmployeeDeductions.acct_no = atxtDeductionAccountNumber.AccountNumber;

        //          if (dr["Quarter1"] != DBNull.Value && dr["Quarter1"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_qtd1 = Convert.ToDecimal(dr["Quarter1"]);
        //          if (dr["Quarter2"] != DBNull.Value && dr["Quarter2"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_qtd2 = Convert.ToDecimal(dr["Quarter2"]);
        //          if (dr["Quarter3"] != DBNull.Value && dr["Quarter3"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_qtd3 = Convert.ToDecimal(dr["Quarter3"]);
        //          if (dr["Quarter4"] != DBNull.Value && dr["Quarter4"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_qtd4 = Convert.ToDecimal(dr["Quarter4"]);
        //          if (dr["YearToDate"] != DBNull.Value && dr["YearToDate"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_ytd = Convert.ToDecimal(dr["YearToDate"]);
        //          if (dr["LowException"] != DBNull.Value && dr["LowException"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.lo_ded_amt = Convert.ToDecimal(dr["LowException"]);
        //          if (dr["HighException"] != DBNull.Value && dr["HighException"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.hi_ded_amt = Convert.ToDecimal(dr["HighException"]);
        //          if (dr["Balance"] != DBNull.Value && dr["Balance"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.balanceamt = Convert.ToDecimal(dr["Balance"]);

        //          objDVOMasterEmployeeDeductions.department = "000";
        //          objDVOMasterEmployeeDeductions.UpdateMachineInfo = Program.MachineInfo;
        //          objDVOMasterEmployeeDeductions.UpdateBy = Program.UserId;
        //          objDVOMasterEmployeeDeductions.UpdateDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //          objDVOMasterEmployeeDeductions.InsertMachineInfo = Program.MachineInfo;
        //          objDVOMasterEmployeeDeductions.InsertBy = Program.UserId;
        //          objDVOMasterEmployeeDeductions.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //          //add into list
        //          listNewDVOMasterEmployeeDeductions.Add(objDVOMasterEmployeeDeductions);
        //        }
        //      }
        //    }
        //    else if (dr.RowState == DataRowState.Modified)
        //    {
        //      bool _rowDelete = true;
        //      if (dr["DeductionCode"] != DBNull.Value && dr["DeductionCode"].ToString().Trim().Length > 0)
        //      {
        //        _rowDelete = false;
        //        using (DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions = new DVOMasterEmployeeDeductions())
        //        {
        //          //assign appropriate values to detail-object
        //          if (dr["lineno"] != DBNull.Value && dr["lineno"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.line_no = Convert.ToInt32(dr["lineno"]);
        //          objDVOMasterEmployeeDeductions.empl_code = txtEmployeeIDCode0.Text.Trim();
        //          objDVOMasterEmployeeDeductions.ded_code = dr["DeductionCode"].ToString().Trim();
        //          if (dr["Rate"] != DBNull.Value && dr["Rate"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_rate = Convert.ToDecimal(dr["Rate"]);
        //          if (dr["AnnualLimit"] != DBNull.Value && dr["AnnualLimit"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_limit = Convert.ToDecimal(dr["AnnualLimit"]);
        //          if (dr["PayLimit"] != DBNull.Value && dr["PayLimit"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.pay_limit = Convert.ToDecimal(dr["PayLimit"]);

        //          //objDVOMasterEmployeeDeductions.ded_date = null;
        //          if (dr["Applied"] != DBNull.Value && dr["Applied"].ToString().Trim().Length > 0)
        //          {
        //            string _DeductDate = dr["Applied"].ToString().Trim();
        //            if (_DeductDate.Length == 10)
        //              objDVOMasterEmployeeDeductions.ded_date = _DeductDate.Substring(3, 3) + _DeductDate.Substring(0, 3) + _DeductDate.Substring(6, 4);
        //          }

        //          if (dr["Frequency"] != DBNull.Value && dr["Frequency"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_apply = dr["Frequency"].ToString().Trim();
        //          if (dr["AccountNumber"] != DBNull.Value && dr["AccountNumber"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.acct_no = Convert.ToInt32(dr["AccountNumber"]);
        //          if (dr["Keyvalue"] != DBNull.Value)
        //            objDVOMasterEmployeeDeductions.acct_no_kv = dr["Keyvalue"].ToString().Trim();
        //          if (dr["dedaccounttype"] != DBNull.Value)
        //            objDVOMasterEmployeeDeductions.acct_no_type = dr["dedaccounttype"].ToString().Trim();
        //          //if (atxtDeductionAccountNumber.AccountNumber > 0)
        //          //    objDVOMasterEmployeeDeductions.acct_no = atxtDeductionAccountNumber.AccountNumber;

        //          if (dr["Quarter1"] != DBNull.Value && dr["Quarter1"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_qtd1 = Convert.ToDecimal(dr["Quarter1"]);
        //          if (dr["Quarter2"] != DBNull.Value && dr["Quarter2"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_qtd2 = Convert.ToDecimal(dr["Quarter2"]);
        //          if (dr["Quarter3"] != DBNull.Value && dr["Quarter3"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_qtd3 = Convert.ToDecimal(dr["Quarter3"]);
        //          if (dr["Quarter4"] != DBNull.Value && dr["Quarter4"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_qtd4 = Convert.ToDecimal(dr["Quarter4"]);
        //          if (dr["YearToDate"] != DBNull.Value && dr["YearToDate"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.ded_ytd = Convert.ToDecimal(dr["YearToDate"]);
        //          if (dr["LowException"] != DBNull.Value && dr["LowException"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.lo_ded_amt = Convert.ToDecimal(dr["LowException"]);
        //          if (dr["HighException"] != DBNull.Value && dr["HighException"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.hi_ded_amt = Convert.ToDecimal(dr["HighException"]);
        //          if (dr["Balance"] != DBNull.Value && dr["Balance"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeDeductions.balanceamt = Convert.ToDecimal(dr["Balance"]);

        //          objDVOMasterEmployeeDeductions.department = "000";
        //          objDVOMasterEmployeeDeductions.UpdateMachineInfo = Program.MachineInfo;
        //          objDVOMasterEmployeeDeductions.UpdateBy = Program.UserId;
        //          objDVOMasterEmployeeDeductions.UpdateDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //          objDVOMasterEmployeeDeductions.InsertMachineInfo = Program.MachineInfo;
        //          objDVOMasterEmployeeDeductions.InsertBy = Program.UserId;
        //          objDVOMasterEmployeeDeductions.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //          //add into list
        //          listDVOMasterEmployeeDeductions.Add(objDVOMasterEmployeeDeductions);
        //        }
        //      }

        //      if (dr["lineno"] != DBNull.Value && dr["lineno"].ToString().Trim().Length > 0)
        //        if (_rowDelete)
        //        {
        //          using (DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions = new DVOMasterEmployeeDeductions())
        //          {
        //            objDVOMasterEmployeeDeductions.line_no = Convert.ToInt32(dr["lineno"]);
        //            objDVOMasterEmployeeDeductions.empl_code = txtEmployeeIDCode0.Text.Trim();
        //            if (dr["AccountNumber"] != DBNull.Value && dr["AccountNumber"].ToString().Trim().Length > 0)
        //              objDVOMasterEmployeeDeductions.acct_no = Convert.ToInt32(dr["AccountNumber"]);
        //            if (dr["Keyvalue"] != DBNull.Value)
        //              objDVOMasterEmployeeDeductions.acct_no_kv = dr["Keyvalue"].ToString().Trim();
        //            if (dr["dedaccounttype"] != DBNull.Value)
        //              objDVOMasterEmployeeDeductions.acct_no_type = dr["dedaccounttype"].ToString().Trim();
        //            listDeleteDVOMasterEmployeeDeductions.Add(objDVOMasterEmployeeDeductions);
        //          }
        //        }
        //    }
        //  }
        //}

        #endregion "Employee Deduction"

        #region "Employee Obligation"

        //make list of detail-objects of main object
        List<DVOMasterEmployeeObligations> listDVOMasterEmployeeObligations = new List<DVOMasterEmployeeObligations>();
        List<DVOMasterEmployeeObligations> listDeleteDVOMasterEmployeeObligations = new List<DVOMasterEmployeeObligations>();
        List<DVOMasterEmployeeObligations> listNewDVOMasterEmployeeObligations = new List<DVOMasterEmployeeObligations>();

        //if (dgvObligation3.DataSource is DataTable)
        //{
        //  DataTable dtObligation = (DataTable)dgvObligation3.DataSource;
        //  foreach (DataRow dr in dtObligation.Rows)
        //  {

        //    if (dr.RowState == DataRowState.Deleted)
        //    {
        //      if (dr["lineno", DataRowVersion.Original] != DBNull.Value && dr["lineno", DataRowVersion.Original].ToString().Trim().Length > 0)
        //      {
        //        using (DVOMasterEmployeeObligations objDVOMasterEmployeeObligations = new DVOMasterEmployeeObligations())
        //        {
        //          objDVOMasterEmployeeObligations.line_no = Convert.ToInt32(dr["lineno", DataRowVersion.Original]);
        //          objDVOMasterEmployeeObligations.empl_code = txtEmployeeIDCode0.Text.Trim();
        //          if (dr["ExpenseAccountNumber", DataRowVersion.Original] != DBNull.Value && dr["ExpenseAccountNumber", DataRowVersion.Original].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.acct_no = Convert.ToInt32(dr["ExpenseAccountNumber", DataRowVersion.Original]);
        //          if (dr["ExpenseKeyvalue", DataRowVersion.Original] != DBNull.Value)
        //            objDVOMasterEmployeeObligations.acct_no_kv = dr["ExpenseKeyvalue", DataRowVersion.Original].ToString().Trim();
        //          if (dr["oblexpaccounttype", DataRowVersion.Original] != DBNull.Value)
        //            objDVOMasterEmployeeObligations.acct_no_type = dr["oblexpaccounttype", DataRowVersion.Original].ToString().Trim();
        //          if (dr["LiabilityAccountNumber", DataRowVersion.Original] != DBNull.Value && dr["LiabilityAccountNumber", DataRowVersion.Original].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.bal_acct_no = Convert.ToInt32(dr["LiabilityAccountNumber", DataRowVersion.Original]);
        //          if (dr["LiabilityKeyvalue", DataRowVersion.Original] != DBNull.Value)
        //            objDVOMasterEmployeeObligations.bal_acct_no_kv = dr["LiabilityKeyvalue", DataRowVersion.Original].ToString().Trim();
        //          if (dr["oblliabaccounttype", DataRowVersion.Original] != DBNull.Value)
        //            objDVOMasterEmployeeObligations.bal_acct_no_type = dr["oblliabaccounttype", DataRowVersion.Original].ToString().Trim();

        //          listDeleteDVOMasterEmployeeObligations.Add(objDVOMasterEmployeeObligations);
        //        }
        //      }
        //    }
        //    else if (dr.RowState == DataRowState.Added)
        //    {
        //      if (dr["ObligationCode"] != DBNull.Value && dr["ObligationCode"].ToString().Trim().Length > 0)
        //      {
        //        using (DVOMasterEmployeeObligations objDVOMasterEmployeeObligations = new DVOMasterEmployeeObligations())
        //        {
        //          //assign appropriate values to detail-object
        //          if (dr["lineno"] != DBNull.Value && dr["lineno"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.line_no = Convert.ToInt32(dr["lineno"]);
        //          objDVOMasterEmployeeObligations.empl_code = txtEmployeeIDCode0.Text.Trim();
        //          objDVOMasterEmployeeObligations.obl_code = dr["ObligationCode"].ToString().Trim();
        //          if (dr["Rate"] != DBNull.Value && dr["Rate"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_rate = Convert.ToDecimal(dr["Rate"]);
        //          if (dr["AnnualLimit"] != DBNull.Value && dr["AnnualLimit"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_limit = Convert.ToDecimal(dr["AnnualLimit"]);

        //          if (dr["PayLimit"] != DBNull.Value && dr["PayLimit"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.pay_limit = Convert.ToDecimal(dr["PayLimit"]);

        //          if (dr["ExpenseAccountNumber"] != DBNull.Value && dr["ExpenseAccountNumber"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.acct_no = Convert.ToInt32(dr["ExpenseAccountNumber"]);
        //          if (dr["ExpenseKeyvalue"] != DBNull.Value)
        //            objDVOMasterEmployeeObligations.acct_no_kv = dr["ExpenseKeyvalue"].ToString().Trim();
        //          if (dr["oblexpaccounttype"] != DBNull.Value)
        //            objDVOMasterEmployeeObligations.acct_no_type = dr["oblexpaccounttype"].ToString().Trim();
        //          //if (atxtOblExpAccountNumber.AccountNumber > 0)
        //          //    objDVOMasterEmployeeObligations.acct_no = atxtOblExpAccountNumber.AccountNumber;

        //          if (dr["LiabilityAccountNumber"] != DBNull.Value && dr["LiabilityAccountNumber"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.bal_acct_no = Convert.ToInt32(dr["LiabilityAccountNumber"]);
        //          if (dr["LiabilityKeyvalue"] != DBNull.Value)
        //            objDVOMasterEmployeeObligations.bal_acct_no_kv = dr["LiabilityKeyvalue"].ToString().Trim();
        //          if (dr["oblliabaccounttype"] != DBNull.Value)
        //            objDVOMasterEmployeeObligations.bal_acct_no_type = dr["oblliabaccounttype"].ToString().Trim();
        //          //if (atxtOblLiabAccountNumber.AccountNumber > 0)
        //          //    objDVOMasterEmployeeObligations.bal_acct_no = atxtOblLiabAccountNumber.AccountNumber;

        //          if (dr["Quarter1"] != DBNull.Value && dr["Quarter1"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_qtd1 = Convert.ToDecimal(dr["Quarter1"]);
        //          if (dr["Quarter2"] != DBNull.Value && dr["Quarter2"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_qtd2 = Convert.ToDecimal(dr["Quarter2"]);
        //          if (dr["Quarter3"] != DBNull.Value && dr["Quarter3"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_qtd3 = Convert.ToDecimal(dr["Quarter3"]);
        //          if (dr["Quarter4"] != DBNull.Value && dr["Quarter4"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_qtd4 = Convert.ToDecimal(dr["Quarter4"]);
        //          if (dr["YearToDate"] != DBNull.Value && dr["YearToDate"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_ytd = Convert.ToDecimal(dr["YearToDate"]);

        //          objDVOMasterEmployeeObligations.department = "000";
        //          objDVOMasterEmployeeObligations.UpdateMachineInfo = Program.MachineInfo;
        //          objDVOMasterEmployeeObligations.Updateby = Program.UserId;
        //          objDVOMasterEmployeeObligations.UpdateDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //          objDVOMasterEmployeeObligations.InsertMachineInfo = Program.MachineInfo;
        //          objDVOMasterEmployeeObligations.InsertBy = Program.UserId;
        //          objDVOMasterEmployeeObligations.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //          //add into list
        //          listNewDVOMasterEmployeeObligations.Add(objDVOMasterEmployeeObligations);
        //        }
        //      }
        //    }
        //    else if (dr.RowState == DataRowState.Modified)
        //    {
        //      bool _rowDelete = true;
        //      //check values entered into cells of row, if value is valid then add object into list
        //      //if (dr["ObligationCode"] != DBNull.Value)
        //      //    if (dr["ObligationCode"].ToString().Trim().Length > 0 && dr["Rate"] != DBNull.Value)
        //      //        if (dr["Rate"].ToString().Trim().Length > 0 && dr["AnnualLimit"] != DBNull.Value)
        //      //            if (dr["AnnualLimit"].ToString().Trim().Length > 0)
        //      if (dr["ObligationCode"] != DBNull.Value && dr["ObligationCode"].ToString().Trim().Length > 0)
        //      {
        //        _rowDelete = false;
        //        using (DVOMasterEmployeeObligations objDVOMasterEmployeeObligations = new DVOMasterEmployeeObligations())
        //        {
        //          //assign appropriate values to detail-object
        //          if (dr["lineno"] != DBNull.Value && dr["lineno"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.line_no = Convert.ToInt32(dr["lineno"]);
        //          objDVOMasterEmployeeObligations.empl_code = txtEmployeeIDCode0.Text.Trim();
        //          objDVOMasterEmployeeObligations.obl_code = dr["ObligationCode"].ToString().Trim();
        //          if (dr["Rate"] != DBNull.Value && dr["Rate"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_rate = Convert.ToDecimal(dr["Rate"]);
        //          if (dr["AnnualLimit"] != DBNull.Value && dr["AnnualLimit"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_limit = Convert.ToDecimal(dr["AnnualLimit"]);
        //          if (dr["PayLimit"] != DBNull.Value && dr["PayLimit"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.pay_limit = Convert.ToDecimal(dr["PayLimit"]);
        //          if (dr["ExpenseAccountNumber"] != DBNull.Value && dr["ExpenseAccountNumber"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.acct_no = Convert.ToInt32(dr["ExpenseAccountNumber"]);
        //          if (dr["ExpenseKeyvalue"] != DBNull.Value)
        //            objDVOMasterEmployeeObligations.acct_no_kv = dr["ExpenseKeyvalue"].ToString().Trim();
        //          if (dr["oblexpaccounttype"] != DBNull.Value)
        //            objDVOMasterEmployeeObligations.acct_no_type = dr["oblexpaccounttype"].ToString().Trim();
        //          //if (atxtOblExpAccountNumber.AccountNumber > 0)
        //          //    objDVOMasterEmployeeObligations.acct_no = atxtOblExpAccountNumber.AccountNumber;

        //          if (dr["LiabilityAccountNumber"] != DBNull.Value && dr["LiabilityAccountNumber"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.bal_acct_no = Convert.ToInt32(dr["LiabilityAccountNumber"]);
        //          if (dr["LiabilityKeyvalue"] != DBNull.Value)
        //            objDVOMasterEmployeeObligations.bal_acct_no_kv = dr["LiabilityKeyvalue"].ToString().Trim();
        //          if (dr["oblliabaccounttype"] != DBNull.Value)
        //            objDVOMasterEmployeeObligations.bal_acct_no_type = dr["oblliabaccounttype"].ToString().Trim();
        //          //if (atxtOblLiabAccountNumber.AccountNumber > 0)
        //          //    objDVOMasterEmployeeObligations.bal_acct_no = atxtOblLiabAccountNumber.AccountNumber;

        //          if (dr["Quarter1"] != DBNull.Value && dr["Quarter1"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_qtd1 = Convert.ToDecimal(dr["Quarter1"]);
        //          if (dr["Quarter2"] != DBNull.Value && dr["Quarter2"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_qtd2 = Convert.ToDecimal(dr["Quarter2"]);
        //          if (dr["Quarter3"] != DBNull.Value && dr["Quarter3"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_qtd3 = Convert.ToDecimal(dr["Quarter3"]);
        //          if (dr["Quarter4"] != DBNull.Value && dr["Quarter4"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_qtd4 = Convert.ToDecimal(dr["Quarter4"]);
        //          if (dr["YearToDate"] != DBNull.Value && dr["YearToDate"].ToString().Trim().Length > 0)
        //            objDVOMasterEmployeeObligations.obl_ytd = Convert.ToDecimal(dr["YearToDate"]);

        //          objDVOMasterEmployeeObligations.department = "000";
        //          objDVOMasterEmployeeObligations.UpdateMachineInfo = Program.MachineInfo;
        //          objDVOMasterEmployeeObligations.Updateby = Program.UserId;
        //          objDVOMasterEmployeeObligations.UpdateDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //          objDVOMasterEmployeeObligations.InsertMachineInfo = Program.MachineInfo;
        //          objDVOMasterEmployeeObligations.InsertBy = Program.UserId;
        //          objDVOMasterEmployeeObligations.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //          //add into list
        //          listDVOMasterEmployeeObligations.Add(objDVOMasterEmployeeObligations);
        //        }
        //      }

        //      if (dr["lineno"] != DBNull.Value && dr["lineno"].ToString().Trim().Length > 0)
        //        if (_rowDelete)
        //        {
        //          using (DVOMasterEmployeeObligations objDVOMasterEmployeeObligations = new DVOMasterEmployeeObligations())
        //          {
        //            objDVOMasterEmployeeObligations.line_no = Convert.ToInt32(dr["lineno"]);
        //            objDVOMasterEmployeeObligations.empl_code = txtEmployeeIDCode0.Text.Trim();
        //            if (dr["ExpenseAccountNumber"] != DBNull.Value && dr["ExpenseAccountNumber"].ToString().Trim().Length > 0)
        //              objDVOMasterEmployeeObligations.acct_no = Convert.ToInt32(dr["ExpenseAccountNumber"]);
        //            if (dr["ExpenseKeyvalue"] != DBNull.Value)
        //              objDVOMasterEmployeeObligations.acct_no_kv = dr["ExpenseKeyvalue"].ToString().Trim();
        //            if (dr["oblexpaccounttype"] != DBNull.Value)
        //              objDVOMasterEmployeeObligations.acct_no_type = dr["oblexpaccounttype"].ToString().Trim();
        //            if (dr["LiabilityAccountNumber"] != DBNull.Value && dr["LiabilityAccountNumber"].ToString().Trim().Length > 0)
        //              objDVOMasterEmployeeObligations.bal_acct_no = Convert.ToInt32(dr["LiabilityAccountNumber"]);
        //            if (dr["LiabilityKeyvalue"] != DBNull.Value)
        //              objDVOMasterEmployeeObligations.bal_acct_no_kv = dr["LiabilityKeyvalue"].ToString().Trim();
        //            if (dr["oblliabaccounttype"] != DBNull.Value)
        //              objDVOMasterEmployeeObligations.bal_acct_no_type = dr["oblliabaccounttype"].ToString().Trim();

        //            listDeleteDVOMasterEmployeeObligations.Add(objDVOMasterEmployeeObligations);
        //          }
        //        }
        //    }
        //  }
        //}

        #endregion "Employee Obligation"

        #region "Position History"

        //make list of detail-objects of main object
        List<DVOPREmployeePositionHistoryInyemppd> listDeleteDVOPREmployeePositionHistoryInyemppd = new List<DVOPREmployeePositionHistoryInyemppd>();
        List<DVOPREmployeePositionHistoryInyemppd> listNewDVOPREmployeePositionHistoryInyemppd = new List<DVOPREmployeePositionHistoryInyemppd>();
        List<DVOPREmployeePositionHistoryInyemppd> listDVOPREmployeePositionHistoryInyemppd = new List<DVOPREmployeePositionHistoryInyemppd>();

        //if (dgvPositionHistory.DataSource is DataTable)
        //{
        //  DataTable dtpositionhistory = (DataTable)dgvPositionHistory.DataSource;
        //  foreach (DataRow dr in dtpositionhistory.Rows)
        //  {
        //    if (dr.RowState == DataRowState.Deleted)
        //    {
        //      if (dr["RowId", DataRowVersion.Original] != DBNull.Value)
        //        if (dr["RowId", DataRowVersion.Original].ToString().Trim().Length > 0)
        //        {
        //          using (DVOPREmployeePositionHistoryInyemppd objDVOPREmployeePositionHistoryInyemppd = new DVOPREmployeePositionHistoryInyemppd())
        //          {
        //            objDVOPREmployeePositionHistoryInyemppd.RowId = Convert.ToInt32(dr["RowId", DataRowVersion.Original]);
        //            objDVOPREmployeePositionHistoryInyemppd.empl_code = txtEmployeeIDCode0.Text.Trim();
        //            listDeleteDVOPREmployeePositionHistoryInyemppd.Add(objDVOPREmployeePositionHistoryInyemppd);
        //          }
        //        }
        //    }
        //    else if (dr.RowState == DataRowState.Added)
        //    {
        //      //check values entered into cells of row, if value is valid then add object into list
        //      if (dr["PositionCode"] != DBNull.Value)
        //        if (dr["PositionCode"].ToString().Trim().Length > 0 && dr["CategoryCode"] != DBNull.Value)
        //          if (dr["CategoryCode"].ToString().Trim().Length > 0 && dr["ScaleCode"] != DBNull.Value)
        //            if (dr["ScaleCode"].ToString().Trim().Length > 0 && dr["StartDate"] != DBNull.Value)
        //              if (dr["StartDate"].ToString().Trim().Length > 0 && dr["Position"] != DBNull.Value)
        //                if (dr["Position"].ToString().Trim().Length > 0)
        //                {
        //                  using (DVOPREmployeePositionHistoryInyemppd objDVOPREmployeePositionHistoryInyemppd = new DVOPREmployeePositionHistoryInyemppd())
        //                  {
        //                    //assign appropriate values to detail-object
        //                    if (dr["RowId"] != DBNull.Value && dr["RowId"].ToString().Trim().Length > 0)
        //                      objDVOPREmployeePositionHistoryInyemppd.RowId = Convert.ToInt32(dr["RowId"]);
        //                    objDVOPREmployeePositionHistoryInyemppd.empl_code = txtEmployeeIDCode0.Text.Trim();
        //                    if (dr["PositionCode"] != DBNull.Value)
        //                      objDVOPREmployeePositionHistoryInyemppd.pos_code = dr["PositionCode"].ToString().Trim();
        //                    if (dr["CategoryCode"] != DBNull.Value)
        //                      objDVOPREmployeePositionHistoryInyemppd.cat_code = dr["CategoryCode"].ToString().Trim();
        //                    if (dr["ScaleCode"] != DBNull.Value)
        //                      objDVOPREmployeePositionHistoryInyemppd.scale_code = dr["ScaleCode"].ToString().Trim();

        //                    objDVOPREmployeePositionHistoryInyemppd.start_date = null;
        //                    if (dr["StartDate"] != DBNull.Value && dr["StartDate"].ToString().Trim().Length > 0)
        //                    {
        //                      string _startDate = dr["StartDate"].ToString().Trim();
        //                      if (_startDate.Length == 10)
        //                        objDVOPREmployeePositionHistoryInyemppd.start_date = _startDate.Substring(3, 3) + _startDate.Substring(0, 3) + _startDate.Substring(6);
        //                    }

        //                    objDVOPREmployeePositionHistoryInyemppd.end_date = null;
        //                    if (dr["EndDate"] != DBNull.Value && dr["EndDate"].ToString().Trim().Length > 0)
        //                    {
        //                      string _endDate = dr["EndDate"].ToString().Trim();
        //                      if (_endDate.Length == 10)
        //                        objDVOPREmployeePositionHistoryInyemppd.end_date = _endDate.Substring(3, 3) + _endDate.Substring(0, 3) + _endDate.Substring(6);
        //                    }

        //                    if (dr["ApprovedBy"] != DBNull.Value)
        //                      objDVOPREmployeePositionHistoryInyemppd.approved_by = dr["ApprovedBy"].ToString().Trim();

        //                    if (dr["Rate"] != DBNull.Value && dr["Rate"].ToString().Trim().Length > 0)
        //                      objDVOPREmployeePositionHistoryInyemppd.pay_rate = Convert.ToDecimal(dr["Rate"]);

        //                    if (dr["Position"] != DBNull.Value)
        //                      objDVOPREmployeePositionHistoryInyemppd.temporary = dr["Position"].ToString().Trim();

        //                    objDVOPREmployeePositionHistoryInyemppd.UpdateMachineInfo = Program.MachineInfo;
        //                    objDVOPREmployeePositionHistoryInyemppd.UpdateBy = Program.UserId;
        //                    objDVOPREmployeePositionHistoryInyemppd.UpdateDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //                    objDVOPREmployeePositionHistoryInyemppd.InsertMachineInfo = Program.MachineInfo;
        //                    objDVOPREmployeePositionHistoryInyemppd.InsertBy = Program.UserId;
        //                    objDVOPREmployeePositionHistoryInyemppd.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //                    //add into list
        //                    listNewDVOPREmployeePositionHistoryInyemppd.Add(objDVOPREmployeePositionHistoryInyemppd);
        //                  }
        //                }
        //    }
        //    else if (dr.RowState == DataRowState.Modified)
        //    {
        //      bool _rowDelete = true;
        //      //check values entered into cells of row, if value is valid then add object into list
        //      if (dr["PositionCode"] != DBNull.Value)
        //        if (dr["PositionCode"].ToString().Trim().Length > 0 && dr["CategoryCode"] != DBNull.Value)
        //          if (dr["CategoryCode"].ToString().Trim().Length > 0 && dr["ScaleCode"] != DBNull.Value)
        //            if (dr["ScaleCode"].ToString().Trim().Length > 0 && dr["StartDate"] != DBNull.Value)
        //              if (dr["StartDate"].ToString().Trim().Length > 0 && dr["Position"] != DBNull.Value)
        //                if (dr["Position"].ToString().Trim().Length > 0)
        //                {
        //                  _rowDelete = false;
        //                  using (DVOPREmployeePositionHistoryInyemppd objDVOPREmployeePositionHistoryInyemppd = new DVOPREmployeePositionHistoryInyemppd())
        //                  {
        //                    //assign appropriate values to detail-object
        //                    if (dr["RowId"] != DBNull.Value && dr["RowId"].ToString().Trim().Length > 0)
        //                      objDVOPREmployeePositionHistoryInyemppd.RowId = Convert.ToInt32(dr["RowId"]);
        //                    objDVOPREmployeePositionHistoryInyemppd.empl_code = txtEmployeeIDCode0.Text.Trim();
        //                    if (dr["PositionCode"] != DBNull.Value)
        //                      objDVOPREmployeePositionHistoryInyemppd.pos_code = dr["PositionCode"].ToString().Trim();
        //                    if (dr["CategoryCode"] != DBNull.Value)
        //                      objDVOPREmployeePositionHistoryInyemppd.cat_code = dr["CategoryCode"].ToString().Trim();
        //                    if (dr["ScaleCode"] != DBNull.Value)
        //                      objDVOPREmployeePositionHistoryInyemppd.scale_code = dr["ScaleCode"].ToString().Trim();

        //                    objDVOPREmployeePositionHistoryInyemppd.start_date = null;
        //                    if (dr["StartDate"] != DBNull.Value && dr["StartDate"].ToString().Trim().Length > 0)
        //                    {
        //                      string _startDate = dr["StartDate"].ToString().Trim();
        //                      if (_startDate.Length == 10)
        //                        objDVOPREmployeePositionHistoryInyemppd.start_date = _startDate.Substring(3, 3) + _startDate.Substring(0, 3) + _startDate.Substring(6);
        //                    }

        //                    objDVOPREmployeePositionHistoryInyemppd.end_date = null;
        //                    if (dr["EndDate"] != DBNull.Value && dr["EndDate"].ToString().Trim().Length > 0)
        //                    {
        //                      string _endDate = dr["EndDate"].ToString().Trim();
        //                      if (_endDate.Length == 10)
        //                        objDVOPREmployeePositionHistoryInyemppd.end_date = _endDate.Substring(3, 3) + _endDate.Substring(0, 3) + _endDate.Substring(6);
        //                    }

        //                    if (dr["ApprovedBy"] != DBNull.Value)
        //                      objDVOPREmployeePositionHistoryInyemppd.approved_by = dr["ApprovedBy"].ToString().Trim();

        //                    if (dr["Rate"] != DBNull.Value && dr["Rate"].ToString().Trim().Length > 0)
        //                      objDVOPREmployeePositionHistoryInyemppd.pay_rate = Convert.ToDecimal(dr["Rate"]);

        //                    if (dr["Position"] != DBNull.Value)
        //                      objDVOPREmployeePositionHistoryInyemppd.temporary = dr["Position"].ToString().Trim();

        //                    objDVOPREmployeePositionHistoryInyemppd.UpdateMachineInfo = Program.MachineInfo;
        //                    objDVOPREmployeePositionHistoryInyemppd.UpdateBy = Program.UserId;
        //                    objDVOPREmployeePositionHistoryInyemppd.UpdateDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //                    objDVOPREmployeePositionHistoryInyemppd.InsertMachineInfo = Program.MachineInfo;
        //                    objDVOPREmployeePositionHistoryInyemppd.InsertBy = Program.UserId;
        //                    objDVOPREmployeePositionHistoryInyemppd.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //                    //add into list
        //                    listDVOPREmployeePositionHistoryInyemppd.Add(objDVOPREmployeePositionHistoryInyemppd);
        //                  }
        //                }

        //      if (dr["RowId"] != DBNull.Value)
        //        if (dr["RowId"].ToString().Trim().Length > 0)
        //          if (_rowDelete)
        //          {
        //            using (DVOPREmployeePositionHistoryInyemppd objDVOPREmployeePositionHistoryInyemppd = new DVOPREmployeePositionHistoryInyemppd())
        //            {
        //              objDVOPREmployeePositionHistoryInyemppd.RowId = Convert.ToInt32(dr["RowId"]);
        //              objDVOPREmployeePositionHistoryInyemppd.empl_code = txtEmployeeIDCode0.Text.Trim();
        //              listDeleteDVOPREmployeePositionHistoryInyemppd.Add(objDVOPREmployeePositionHistoryInyemppd);
        //            }
        //          }
        //    }
        //  }
        //}

        #endregion "Position History"

        #region "Direct Deposit"

        //make list of detail-objects of main object
        List<DVOMasterEmpBankDetails> listNewDVOMasterEmpBankDetails = new List<DVOMasterEmpBankDetails>();
        List<DVOMasterEmpBankDetails> listDeleteDVOMasterEmpBankDetails = new List<DVOMasterEmpBankDetails>();
        List<DVOMasterEmpBankDetails> listDVOMasterEmpBankDetails = new List<DVOMasterEmpBankDetails>();

        //if (dgvDirectDeposit.DataSource is DataTable)
        //{
        //  DataTable dtdirectdeposit = (DataTable)dgvDirectDeposit.DataSource;
        //  foreach (DataRow dr in dtdirectdeposit.Rows)
        //  {

        //    if (dr.RowState == DataRowState.Deleted)
        //    {
        //      if (dr["RowId", DataRowVersion.Original] != DBNull.Value && dr["RowId", DataRowVersion.Original].ToString().Trim().Length > 0)
        //      {
        //        using (DVOMasterEmpBankDetails objDVOMasterEmpBankDetails = new DVOMasterEmpBankDetails())
        //        {
        //          objDVOMasterEmpBankDetails.RowID = Convert.ToInt32(dr["RowId", DataRowVersion.Original]);
        //          objDVOMasterEmpBankDetails.empl_code = txtEmployeeIDCode0.Text.Trim();
        //          listDeleteDVOMasterEmpBankDetails.Add(objDVOMasterEmpBankDetails);
        //        }
        //      }
        //    }
        //    else if (dr.RowState == DataRowState.Added)
        //    {
        //      //check values entered into cells of row, if value is valid then add object into list
        //      if (dr["BankCode"] != DBNull.Value)
        //        if (dr["BankCode"].ToString().Trim().Length > 0 && dr["CS"] != DBNull.Value)
        //          if (dr["CS"].ToString().Trim().Length > 0 && dr["AccountNo"] != DBNull.Value)
        //            if (dr["AccountNo"].ToString().Trim().Length > 0 && dr["T"] != DBNull.Value)
        //              if (dr["T"].ToString().Trim().Length > 0)
        //              {
        //                bool _ContinueProcess = false;
        //                if (dr["T"].ToString().Trim().ToUpper() != "R")
        //                {
        //                  if (dr["Amount"] != DBNull.Value && dr["Amount"].ToString().Trim().Length > 0)
        //                    _ContinueProcess = true;
        //                }
        //                else
        //                  _ContinueProcess = true;

        //                if (_ContinueProcess)
        //                  using (DVOMasterEmpBankDetails objDVOMasterEmpBankDetails = new DVOMasterEmpBankDetails())
        //                  {
        //                    //assign appropriate values to detail-object
        //                    if (dr["RowId"] != DBNull.Value && dr["RowId"].ToString().Trim().Length > 0)
        //                      objDVOMasterEmpBankDetails.RowID = Convert.ToInt32(dr["RowId"]);
        //                    objDVOMasterEmpBankDetails.empl_code = txtEmployeeIDCode0.Text.Trim();
        //                    objDVOMasterEmpBankDetails.line_no = 1;
        //                    if (dr["BankCode"] != DBNull.Value)
        //                      objDVOMasterEmpBankDetails.bank_code = dr["BankCode"].ToString().Trim();
        //                    if (dr["AccountNo"] != DBNull.Value)
        //                      objDVOMasterEmpBankDetails.bank_acct_no = dr["AccountNo"].ToString().Trim();
        //                    if (dr["T"] != DBNull.Value)
        //                      objDVOMasterEmpBankDetails.type = dr["T"].ToString().Trim();
        //                    if (dr["CS"] != DBNull.Value)
        //                      objDVOMasterEmpBankDetails.typeofacct = dr["CS"].ToString().Trim();
        //                    if (dr["T"] != DBNull.Value && dr["T"].ToString().Trim().ToUpper() != "R")
        //                    {
        //                      if (dr["Amount"] != DBNull.Value && dr["Amount"].ToString().Trim().Length > 0)
        //                        objDVOMasterEmpBankDetails.amount = Convert.ToDecimal(dr["Amount"]);
        //                    }

        //                    objDVOMasterEmpBankDetails.UpdateMachineInfo = Program.MachineInfo;
        //                    objDVOMasterEmpBankDetails.UpdateBy = Program.UserId;
        //                    objDVOMasterEmpBankDetails.UpdateDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //                    objDVOMasterEmpBankDetails.InsertMachineInfo = Program.MachineInfo;
        //                    objDVOMasterEmpBankDetails.InsertBy = Program.UserId;
        //                    objDVOMasterEmpBankDetails.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //                    //add into list
        //                    listNewDVOMasterEmpBankDetails.Add(objDVOMasterEmpBankDetails);
        //                  }
        //              }
        //    }
        //    else if (dr.RowState == DataRowState.Modified)
        //    {
        //      bool _rowDelete = true;
        //      //check values entered into cells of row, if value is valid then add object into list
        //      if (dr["BankCode"] != DBNull.Value)
        //        if (dr["BankCode"].ToString().Trim().Length > 0 && dr["CS"] != DBNull.Value)
        //          if (dr["CS"].ToString().Trim().Length > 0 && dr["AccountNo"] != DBNull.Value)
        //            if (dr["AccountNo"].ToString().Trim().Length > 0 && dr["T"] != DBNull.Value)
        //              if (dr["T"].ToString().Trim().Length > 0)
        //              {
        //                _rowDelete = false;
        //                bool _ContinueProcess = false;
        //                if (dr["T"].ToString().Trim().ToUpper() != "R")
        //                {
        //                  if (dr["Amount"] != DBNull.Value && dr["Amount"].ToString().Trim().Length > 0)
        //                    _ContinueProcess = true;
        //                }
        //                else
        //                  _ContinueProcess = true;

        //                if (_ContinueProcess)
        //                  using (DVOMasterEmpBankDetails objDVOMasterEmpBankDetails = new DVOMasterEmpBankDetails())
        //                  {
        //                    //assign appropriate values to detail-object
        //                    if (dr["RowId"] != DBNull.Value && dr["RowId"].ToString().Trim().Length > 0)
        //                      objDVOMasterEmpBankDetails.RowID = Convert.ToInt32(dr["RowId"]);
        //                    objDVOMasterEmpBankDetails.empl_code = txtEmployeeIDCode0.Text.Trim();
        //                    objDVOMasterEmpBankDetails.line_no = 1;
        //                    if (dr["BankCode"] != DBNull.Value)
        //                      objDVOMasterEmpBankDetails.bank_code = dr["BankCode"].ToString().Trim();
        //                    if (dr["AccountNo"] != DBNull.Value)
        //                      objDVOMasterEmpBankDetails.bank_acct_no = dr["AccountNo"].ToString().Trim();
        //                    if (dr["T"] != DBNull.Value)
        //                      objDVOMasterEmpBankDetails.type = dr["T"].ToString().Trim();
        //                    if (dr["CS"] != DBNull.Value)
        //                      objDVOMasterEmpBankDetails.typeofacct = dr["CS"].ToString().Trim();

        //                    if (dr["T"] != DBNull.Value && dr["T"].ToString().Trim().ToUpper() != "R")
        //                    {
        //                      if (dr["Amount"] != DBNull.Value && dr["Amount"].ToString().Trim().Length > 0)
        //                        objDVOMasterEmpBankDetails.amount = Convert.ToDecimal(dr["Amount"]);
        //                    }

        //                    objDVOMasterEmpBankDetails.UpdateMachineInfo = Program.MachineInfo;
        //                    objDVOMasterEmpBankDetails.UpdateBy = Program.UserId;
        //                    objDVOMasterEmpBankDetails.UpdateDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //                    objDVOMasterEmpBankDetails.InsertMachineInfo = Program.MachineInfo;
        //                    objDVOMasterEmpBankDetails.InsertBy = Program.UserId;
        //                    objDVOMasterEmpBankDetails.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //                    //add into list
        //                    listDVOMasterEmpBankDetails.Add(objDVOMasterEmpBankDetails);
        //                  }
        //              }

        //      if (dr["RowId"] != DBNull.Value && dr["RowId"].ToString().Trim().Length > 0)
        //        if (_rowDelete)
        //        {
        //          using (DVOMasterEmpBankDetails objDVOMasterEmpBankDetails = new DVOMasterEmpBankDetails())
        //          {
        //            objDVOMasterEmpBankDetails.RowID = Convert.ToInt32(dr["RowId"]);
        //            objDVOMasterEmpBankDetails.empl_code = txtEmployeeIDCode0.Text.Trim();
        //            listDeleteDVOMasterEmpBankDetails.Add(objDVOMasterEmpBankDetails);
        //          }
        //        }
        //    }
        //  }
        //}

        #endregion "Direct Deposit"

        #region "Employee Notes"

        //foreach (DVOstxnoted obj in listEmployeeNotes)
        //{
        //    obj.filename = objDVOMasterEmployee.TABLE_NAME;
        //    obj.record_key = objDVOMasterEmployee.EmplCode;
        //}

        //DVOstxnoted objDVOstxnoted = new DVOstxnoted();
        //objDVOstxnoted.Rowid = _EmployeeNotesRowId;
        //objDVOstxnoted.filename = objDVOMasterEmployee.TABLE_NAME;
        //objDVOstxnoted.record_key = objDVOMasterEmployee.EmplCode;
        //objDVOstxnoted.line_no = 1;
        //objDVOstxnoted.data = _EmployeeNotes;

        #endregion "Employee Notes"
        string actPayrollDepartmentKeyValue = objDVOMasterEmployee.actPayrollDepartmentKeyValue + "######";
        //call update function of BLL
        DVOMasterEmployee objPreUpdDVOMasterEmployee = SearchAgainEmployeeInfo(objDVOMasterEmployee.EmplCode);
        objDVOMasterEmployee.UpdateMachineInfo = Environment.MachineName;
        objDVOMasterEmployee.UpdateBy = AppUserManager.GetUserId();
        int i = BLLMasterEmployee.UpdateEmployeeInformation(ref TransactionObject, ref objDVOMasterEmployee,
            ref listDVOMasterEmployeeIncomes,
            ref listDVOMasterEmployeeDeductions,
            ref listDVOMasterEmployeeObligations,
            ref listDVOPREmployeePositionHistoryInyemppd,
            ref listDVOMasterEmpBankDetails, actPayrollDepartmentKeyValue,
            ref objPreUpdDVOMasterEmployee,
            ref listSearchResultDVOMasterEmployeeIncomes,
            ref listSearchResultDVOMasterEmployeeDeductions,
            ref listSearchResultDVOMasterEmployeeObligations,
            ref listSearchResultDVOPREmployeePositionHistoryInyemppd,
            ref listSearchResultDVOMasterEmpBankDetails,
            //ref listEmployeeNotes,
            ref listCommonNotes,
            ref listDeleteDVOMasterEmployeeIncomes,
            ref listDeleteDVOMasterEmployeeDeductions,
            ref listDeleteDVOMasterEmployeeObligations,
            ref listDeleteDVOPREmployeePositionHistoryInyemppd,
            ref listDeleteDVOMasterEmpBankDetails,
            ref listNewDVOMasterEmployeeIncomes,
            ref listNewDVOMasterEmployeeDeductions,
            ref listNewDVOMasterEmployeeObligations,
            ref listNewDVOPREmployeePositionHistoryInyemppd,
            ref listNewDVOMasterEmpBankDetails);

        objDVOMasterEmployee = null;
        listDVOMasterEmployeeIncomes = null;
        listDVOMasterEmployeeDeductions = null;
        listDVOMasterEmployeeObligations = null;
        listDVOPREmployeePositionHistoryInyemppd = null;
        listDVOMasterEmpBankDetails = null;
        objPreUpdDVOMasterEmployee = null;
        //if process is successfully completed, then set Success flag to true.
        return i;
      }
      catch (Exception ex)
      {
        ErrorMessage = ex.Message;
        return 0;
      }
    }

    #endregion Updation

    #region Search

    /// <summary>
    /// To Search required data from database based on search criteria entered by user
    /// </summary>
    private List<DVOMasterEmployee> SearchEmployeeInformation(DVOMasterEmployee objSearchCriteriaDVOMasterEmployeeModel)
    {
      try
      {
        //set values of appropriate properties to search records from database
        objSearchCriteriaDVOMasterEmployee = new DVOMasterEmployee();
        objSearchCriteriaDVOMasterEmployee.EmplCode = objSearchCriteriaDVOMasterEmployeeModel.EmplCode;
        objSearchCriteriaDVOMasterEmployee.HoldPayment = objSearchCriteriaDVOMasterEmployeeModel.HoldPayment;
        objSearchCriteriaDVOMasterEmployee.SocSecNum = objSearchCriteriaDVOMasterEmployeeModel.SocSecNum;
        objSearchCriteriaDVOMasterEmployee.FlexDeptAcctType = objSearchCriteriaDVOMasterEmployeeModel.FlexDeptAcctType;
        objSearchCriteriaDVOMasterEmployee.TypeCode = objSearchCriteriaDVOMasterEmployeeModel.TypeCode;
        if (objSearchCriteriaDVOMasterEmployee.CashAcct != null && objSearchCriteriaDVOMasterEmployeeModel.CashAcct != 0)
          objSearchCriteriaDVOMasterEmployee.CashAcct = objSearchCriteriaDVOMasterEmployeeModel.CashAcct;


        objSearchCriteriaDVOMasterEmployee.LastName = objSearchCriteriaDVOMasterEmployeeModel.LastName;
        objSearchCriteriaDVOMasterEmployee.FirstName = objSearchCriteriaDVOMasterEmployeeModel.FirstName;
        objSearchCriteriaDVOMasterEmployee.MiddleName = objSearchCriteriaDVOMasterEmployeeModel.MiddleName;
        objSearchCriteriaDVOMasterEmployee.Address1 = objSearchCriteriaDVOMasterEmployeeModel.Address1;
        objSearchCriteriaDVOMasterEmployee.Address2 = objSearchCriteriaDVOMasterEmployeeModel.Address2;
        objSearchCriteriaDVOMasterEmployee.City = objSearchCriteriaDVOMasterEmployeeModel.City;
        objSearchCriteriaDVOMasterEmployee.State = objSearchCriteriaDVOMasterEmployeeModel.State;
        objSearchCriteriaDVOMasterEmployee.Zip = objSearchCriteriaDVOMasterEmployeeModel.Zip;
        objSearchCriteriaDVOMasterEmployee.Phone = objSearchCriteriaDVOMasterEmployeeModel.Phone;
        objSearchCriteriaDVOMasterEmployee.mailid = objSearchCriteriaDVOMasterEmployeeModel.mailid;
        if (objSearchCriteriaDVOMasterEmployee.Birthdate != null)
          objSearchCriteriaDVOMasterEmployee.Birthdate = objSearchCriteriaDVOMasterEmployeeModel.Birthdate;// dtpBirthDate0.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        objSearchCriteriaDVOMasterEmployee.Gender = objSearchCriteriaDVOMasterEmployeeModel.Gender;

        if (objSearchCriteriaDVOMasterEmployee.LastIncDate != null)
          objSearchCriteriaDVOMasterEmployee.LastIncDate = objSearchCriteriaDVOMasterEmployeeModel.LastIncDate;// dtpLastIncrementDate0.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        if (objSearchCriteriaDVOMasterEmployee.Terminated != null)
          objSearchCriteriaDVOMasterEmployee.Terminated = objSearchCriteriaDVOMasterEmployeeModel.Terminated;// dtpTerminationDate0.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        if (objSearchCriteriaDVOMasterEmployee.AppointDate != null)
          objSearchCriteriaDVOMasterEmployee.AppointDate = objSearchCriteriaDVOMasterEmployeeModel.AppointDate;// dtpAppointmentDate0.Value.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        objSearchCriteriaDVOMasterEmployee.PersonID = objSearchCriteriaDVOMasterEmployeeModel.PersonID;
        //get data from database and assign to list of objects
        listSearchResultDVOMasterEmployee = BLLMasterEmployee.GetData(ref objSearchCriteriaDVOMasterEmployee);
        return listSearchResultDVOMasterEmployee;
      }
      catch (Exception exception)
      {
        throw exception;
      }
    }

    ///// <summary>
    ///// To get document data after insertion
    ///// </summary>
    //private void SearchEmployeeAfterInsert()
    //{
    //    if (newDocNo > 0)
    //    {
    //        objSearchCriteriaDVOAPCheckProcessingStpcashe = new DVOAPCheckProcessingStpcashe();
    //        objSearchCriteriaDVOAPCheckProcessingStpcashe.doc_no = newDocNo;
    //        //get data from database and assign to list of objects
    //        listSearchResultDVOAPCheckProcessingStpcashe = BLLAPCheckProcessingStpcashe.GetDataByDataReader(ref objSearchCriteriaDVOAPCheckProcessingStpcashe);
    //    }
    //}

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

    /// <summary>
    /// To Show search result in form
    /// </summary>
    private void ShowSearchResult()
    {
      ////check if list of search result has some objects or not
      //if (listSearchResultDVOMasterEmployee.Count > 0)
      //  //check current record index is exist in list of objects or not
      //  if (listSearchResultDVOMasterEmployee.Count > CurrentRecordIndex)
      //  {
      //    DVOMasterEmployee sObj = listSearchResultDVOMasterEmployee[CurrentRecordIndex];
      //    //if index exist, set values of control with values got from database

      #region "Employee Information"

      //    CurrentRecordUniqueId = sObj.RowID;
      //    CurrentRecordRowId = sObj.RowID;
      //    CurrentEmployeeCode = sObj.EmplCode;
      //    CurrentRecordEmployeeCode = sObj.EmplCode;
      //    txtEmployeeIDCode0.Text = sObj.EmplCode;
      //    txtOnHold0.Text = sObj.HoldPayment;
      //    txtSocialSecurityNo0.Text = sObj.SocSecNum;
      //    actPayrollDepartment0.AccountType = sObj.FlexDeptAcctType;
      //    actPayrollDepartment0.KeyValue = BLLMasterEmployee.GetFlexDeptKeyvalue(ref sObj);
      //    String Employercode = "";
      //    String Programcode = "";
      //    if (actPayrollDepartment0.KeyValue != null && actPayrollDepartment0.KeyValue != string.Empty)
      //    {
      //      if (actPayrollDepartment0.KeyValue != "#####")
      //      {
      //        if (actPayrollDepartment0.KeyValue.ToString().Length > 5)
      //        {
      //          Employercode = actPayrollDepartment0.KeyValue.Substring(0, 3);
      //          Programcode = actPayrollDepartment0.KeyValue.Substring(3, 2);

      //          if (Employercode != "###")
      //          {
      //            lblEmployerName.Text = BLLMasterEmployee.GetFlexDeptName(Employercode);
      //          }
      //          else
      //          {
      //            lblEmployerName.Text = "No Employer Specified";
      //          }
      //          if (Programcode != "##")
      //          {
      //            lblprogram.Text = BLLMasterEmployee.GetFlexProgramName(Programcode);
      //          }
      //          else
      //          {
      //            lblprogram.Text = " No Program Specified";
      //          }
      //        }
      //      }
      //    }
      //    mcgEmployeeType0.SelectedValue = sObj.TypeCode;
      //    mcgPayrollAccount0.SelectedValue = sObj.CashAcct;
      //    txtLastName0.Text = sObj.LastName;
      //    txtFirstName0.Text = sObj.FirstName;
      //    txtMI0.Text = sObj.MiddleName;
      //    txtAddress10.Text = sObj.Address1;
      //    txtAddress20.Text = sObj.Address2;
      //    txtCity0.Text = sObj.City;
      //    txtState0.Text = sObj.State;
      //    txtZipCode0.Text = sObj.Zip;
      //    txtPhone0.Text = sObj.Phone;
      //    txtEMail.Text = sObj.mailid;
      //    dtpBirthDate0.Checked = false;
      //    if (Mode != dbOperation.Update)
      //    {
      //      dtpBirthDate0.Visible = false;
      //      txtBirthDate.Visible = true;
      //    }
      //    else
      //    {
      //      dtpBirthDate0.Visible = true;
      //      txtBirthDate.Visible = false;
      //    }
      //    if (sObj.Birthdate != null)
      //      if (sObj.Birthdate.Trim().Length > 0)
      //      {
      //        dtpBirthDate0.Visible = true;
      //        txtBirthDate.Visible = false;
      //        dtpBirthDate0.Value = DateTime.ParseExact(sObj.Birthdate, Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);// Convert.ToDateTime(sObj.Birthdate);
      //        //dtpBirthDate0.Text = Convert.ToDateTime(sObj.Birthdate).ToString("dd/MM/yyyy");
      //        dtpBirthDate0.Checked = true;
      //      }
      //    txtGender0.Text = sObj.Gender;
      //    dtpLastIncrementDate0.Checked = false;
      //    if (Mode != dbOperation.Update)
      //    {
      //      dtpLastIncrementDate0.Visible = false;
      //      txtLastIncrementDate.Visible = true;
      //    }
      //    else
      //    {
      //      dtpLastIncrementDate0.Visible = true;
      //      txtLastIncrementDate.Visible = false;
      //    }
      //    if (sObj.LastIncDate != null)
      //      if (sObj.LastIncDate.Trim().Length > 0)
      //      {
      //        dtpLastIncrementDate0.Visible = true;
      //        txtLastIncrementDate.Visible = false;
      //        dtpLastIncrementDate0.Value = DateTime.ParseExact(sObj.LastIncDate, Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);//Convert.ToDateTime(sObj.LastIncDate);
      //        //dtpLastIncrementDate0.Text = Convert.ToDateTime(sObj.LastIncDate).ToString("dd/MM/yyyy");
      //        dtpLastIncrementDate0.Checked = true;
      //      }
      //    dtpTerminationDate0.Checked = false;
      //    if (Mode != dbOperation.Update)
      //    {
      //      dtpTerminationDate0.Visible = false;
      //      txtTerminateDate.Visible = true;
      //    }
      //    else
      //    {
      //      dtpTerminationDate0.Visible = true;
      //      txtTerminateDate.Visible = false;
      //    }
      //    if (sObj.Terminated != null)
      //      if (sObj.Terminated.Trim().Length > 0)
      //      {
      //        dtpTerminationDate0.Visible = true;
      //        txtTerminateDate.Visible = false;
      //        dtpTerminationDate0.Value = DateTime.ParseExact(sObj.Terminated, Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);//Convert.ToDateTime(sObj.Terminated);
      //        //dtpTerminationDate0.Text = Convert.ToDateTime(sObj.Terminated).ToString("dd/MM/yyyy");
      //        dtpTerminationDate0.Checked = true;
      //      }
      //    dtpAppointmentDate0.Checked = false;
      //    if (Mode != dbOperation.Update)
      //    {
      //      dtpAppointmentDate0.Visible = false;
      //      txtAppointmentDate.Visible = true;
      //    }
      //    else
      //    {
      //      dtpAppointmentDate0.Visible = true;
      //      txtAppointmentDate.Visible = false;
      //    }
      //    if (sObj.AppointDate != null)
      //      if (sObj.AppointDate.Trim().Length > 0)
      //      {
      //        dtpAppointmentDate0.Visible = true;
      //        txtAppointmentDate.Visible = false;
      //        dtpAppointmentDate0.Value = DateTime.ParseExact(sObj.AppointDate, Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);//Convert.ToDateTime(sObj.AppointDate);
      //        //dtpAppointmentDate0.Text = Convert.ToDateTime(sObj.AppointDate).ToString("dd/MM/yyyy");
      //        dtpAppointmentDate0.Checked = true;
      //      }

      #endregion "Employee Information"

      #region "Employee Extended Information"

      //    txtFedAllwncs6.Text = sObj.Allowances.ToString();
      //    txtPayPeriod6.Text = sObj.PayPeriod;
      //    txtFullTime6.Text = sObj.EmplStatus;
      //    dtpHired6.Checked = false;
      //    if (Mode != dbOperation.Update)
      //    {
      //      dtpHired6.Visible = false;
      //      txtHired.Visible = true;
      //    }
      //    else
      //    {
      //      dtpHired6.Visible = true;
      //      txtHired.Visible = false;
      //    }
      //    if (sObj.DateHired != null)
      //      if (sObj.DateHired.Trim().Length > 0)
      //      {
      //        dtpHired6.Visible = true;
      //        txtHired.Visible = false;
      //        dtpHired6.Value = DateTime.ParseExact(sObj.DateHired, Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);//Convert.ToDateTime(sObj.DateHired);
      //        //dtpHired6.Text = Convert.ToDateTime(sObj.DateHired).ToString("dd/MM/yyyy");
      //        dtpHired6.Checked = true;
      //      }
      //    txtStateAllwncs6.Text = sObj.StateAllow.ToString();
      //    txtMarital6.Text = sObj.MaritalStat;
      //    mcgStateTaxCode6.SelectedValue = sObj.StaTaxCode;
      //    mcgLocatTaxCode6.SelectedValue = sObj.LocTaxCode;
      //    dtpLastPay6.Checked = false;
      //    if (Mode != dbOperation.Update)
      //    {
      //      dtpLastPay6.Visible = false;
      //      txtLastPay.Visible = true;
      //    }
      //    else
      //    {
      //      dtpLastPay6.Visible = true;
      //      txtLastPay.Visible = false;
      //    }
      //    if (sObj.LastPay != null)
      //      if (sObj.LastPay.Trim().Length > 0)
      //      {
      //        dtpLastPay6.Visible = true;
      //        txtLastPay.Visible = false;
      //        dtpLastPay6.Value = DateTime.ParseExact(sObj.LastPay, Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);//Convert.ToDateTime(sObj.LastPay);
      //        //dtpLastPay6.Text = Convert.ToDateTime(sObj.LastPay).ToString("dd/MM/yyyy");
      //        dtpLastPay6.Checked = true;
      //      }
      //    mcgSickLeaveIncomeCode6.SelectedValue = sObj.SickCode;
      //    txtSickAccrued6.Text = sObj.SickAllowed.HasValue ? sObj.SickAllowed.ToString() : string.Empty;
      //    txtSickUsed6.Text = sObj.SickUsed.HasValue ? sObj.SickUsed.ToString() : string.Empty;
      //    mcgVacationIncomeCode6.SelectedValue = sObj.VacCode;
      //    txtVacAccrued6.Text = sObj.VacAllowed.HasValue ? sObj.VacAllowed.ToString() : string.Empty;
      //    txtVacUsed6.Text = sObj.VacUsed.HasValue ? sObj.VacUsed.ToString() : string.Empty;
      //    mcgSickAccrual6.SelectedValue = sObj.SickAccrCodr;
      //    txtSickCntr.Text = sObj.SickAccrCtr.ToString();
      //    mcgVacAccrual6.SelectedValue = sObj.VacAccrCode;
      //    txtVacCntr.Text = sObj.VacAccrCtr.ToString();
      //    txtDirectDeposit6.Text = sObj.DirDept;

      #endregion "Employee Extended Information"

      #region "Employee Income"

      //create table to bind with datagridview
      System.Data.DataTable dtIncome = CreateDataTableForIncome();

      DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes = new DVOMasterEmployeeIncomes();
      objDVOMasterEmployeeIncomes.empl_code = "To Do EmplCode";//sObj.EmplCode;
                                                               //List<DVOMasterEmployeeIncomes> listDVOMasterEmployeeIncomes = BLLMasterEmployeeIncomes.GetData(ref objDVOMasterEmployeeIncomes);
      listSearchResultDVOMasterEmployeeIncomes = BLLMasterEmployeeIncomes.GetData(ref objDVOMasterEmployeeIncomes, "To Do sObj.FlexDeptAcctType");
      if (listSearchResultDVOMasterEmployeeIncomes.Count > 0)
      {
        //if (listSearchResultDVOMasterEmployeeIncomes.Count > 0)
        foreach (DVOMasterEmployeeIncomes objIncome in listSearchResultDVOMasterEmployeeIncomes)
        {
          //string _KeyValue = "", _AccountType = "", _AccountDescription = "";
          //int _AccountTypeId = 0;
          //if (objIncome.acct_no > 0)
          //    GetAccountInformation(objIncome.acct_no, out _KeyValue, out _AccountType, out _AccountTypeId, out _AccountDescription);

          DataRow dr = dtIncome.NewRow();
          dr["IncomeCode"] = objIncome.inc_code;
          dr["Rate"] = String.Format("{0:0.00000}", objIncome.inc_rate);
          dr["Number"] = String.Format("{0:0.00}", objIncome.inc_number);
          dr["Amount"] = String.Format("{0:0.00}", (objIncome.inc_rate * objIncome.inc_number));
          dr["Hours"] = objIncome.inc_hours;
          dr["AccountNumber"] = objIncome.acct_no;
          dr["lineno"] = objIncome.line_no;
          dr["LowException"] = String.Format("{0:0.00}", objIncome.lo_inc_amt);
          dr["HighException"] = String.Format("{0:0.00}", objIncome.hi_inc_amt);
          dr["Quarter1"] = String.Format("{0:0.00}", objIncome.inc_qtd1);
          dr["Quarter2"] = String.Format("{0:0.00}", objIncome.inc_qtd2);
          dr["Quarter3"] = String.Format("{0:0.00}", objIncome.inc_qtd3);
          dr["Quarter4"] = String.Format("{0:0.00}", objIncome.inc_qtd4);
          dr["YearToDate"] = String.Format("{0:0.00}", objIncome.inc_ytd);
          dr["Keyvalue"] = objIncome.acct_no_kv;//_KeyValue;
          dr["accountid"] = objIncome.acct_no_typeid;//_accountid;
          dr["incaccounttype"] = objIncome.acct_no_type;//_accounttype;

          dtIncome.Rows.Add(dr);
          dr = null;
        }
      }
      //dtIncome.AcceptChanges();
      //dgvIncome1.AutoGenerateColumns = false;
      //dgvIncome1.DataSource = dtIncome;
      //objDVOMasterEmployeeIncomes = null;
      //listDVOMasterEmployeeIncomes = null;

      #endregion "Employee Income"

      #region "Employee Deduction"

      //create table to bind with datagridview
      System.Data.DataTable dtDeduction = CreateDataTableForDeduction();

      DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions = new DVOMasterEmployeeDeductions();
      objDVOMasterEmployeeDeductions.empl_code = "To DO EmplCode";//sObj.EmplCode;
                                                                  //List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions = BLLMasterEmployeeDeductions.GetData(ref objDVOMasterEmployeeDeductions);
      listSearchResultDVOMasterEmployeeDeductions = BLLMasterEmployeeDeductions.GetData(ref objDVOMasterEmployeeDeductions, "To Do sObj.FlexDeptAcctType");
      if (listSearchResultDVOMasterEmployeeDeductions.Count > 0)
      {
        //if (listSearchResultDVOMasterEmployeeDeductions.Count > 0)
        foreach (DVOMasterEmployeeDeductions objDeduction in listSearchResultDVOMasterEmployeeDeductions)
        {
          //string _KeyValue = "", _AccountType = "", _AccountDescription = "";
          //int _AccountTypeId = 0;
          //if (objDeduction.acct_no > 0)
          //    GetAccountInformation(objDeduction.acct_no, out _KeyValue, out _AccountType, out _AccountTypeId, out _AccountDescription);

          DataRow dr = dtDeduction.NewRow();
          dr["DeductionCode"] = objDeduction.ded_code;
          dr["Rate"] = String.Format("{0:0.00}", objDeduction.ded_rate);
          dr["AnnualLimit"] = String.Format("{0:0.00}", objDeduction.ded_limit);
          dr["PayLimit"] = String.Format("{0:0.00}", objDeduction.pay_limit);
          if (objDeduction.ded_date != null && objDeduction.ded_date.Trim().Length == 10 && !objDeduction.ded_date.Trim().Contains("1900"))
            dr["Applied"] = objDeduction.ded_date.Trim().Substring(3, 3) + objDeduction.ded_date.Trim().Substring(0, 3) + objDeduction.ded_date.Trim().Substring(6, 4);
          dr["Frequency"] = objDeduction.ded_apply;
          dr["AccountNumber"] = objDeduction.acct_no;
          dr["lineno"] = objDeduction.line_no;
          dr["LowException"] = String.Format("{0:0.00}", objDeduction.lo_ded_amt);
          dr["HighException"] = String.Format("{0:0.00}", objDeduction.hi_ded_amt);
          //dr["RollOver"] = 
          dr["Balance"] = String.Format("{0:0.00}", objDeduction.balanceamt);
          dr["Quarter1"] = String.Format("{0:0.00}", objDeduction.ded_qtd1);
          dr["Quarter2"] = String.Format("{0:0.00}", objDeduction.ded_qtd2);
          dr["Quarter3"] = String.Format("{0:0.00}", objDeduction.ded_qtd3);
          dr["Quarter4"] = String.Format("{0:0.00}", objDeduction.ded_qtd4);
          dr["YearToDate"] = String.Format("{0:0.00}", objDeduction.ded_ytd);
          dr["Keyvalue"] = objDeduction.acct_no_kv;//_KeyValue;
          dr["accountid"] = objDeduction.acct_no_typeid;//accounttypeid;
          dr["dedaccounttype"] = objDeduction.acct_no_type;//accounttype;

          dtDeduction.Rows.Add(dr);
          dr = null;
        }
      }
      //dtDeduction.AcceptChanges();
      //dgvDeduction2.AutoGenerateColumns = false;
      //dgvDeduction2.DataSource = dtDeduction;
      //objDVOMasterEmployeeDeductions = null;
      //listDVOMasterEmployeeDeductions = null;

      #endregion "Employee Deduction"

      #region "Employee Obligation"

      //create table to bind with datagridview
      System.Data.DataTable dtObligation = CreateDataTableForObligation();

      DVOMasterEmployeeObligations objDVOMasterEmployeeObligations = new DVOMasterEmployeeObligations();
      objDVOMasterEmployeeObligations.empl_code = "To DO EmplCode";//sObj.EmplCode;
                                                                   //List<DVOMasterEmployeeObligations> listDVOMasterEmployeeObligations = BLLPREmployeeObligationCodesAndRateDetailStyempod.GetData(ref objDVOMasterEmployeeObligations);
      listSearchResultDVOMasterEmployeeObligations = BLLMasterEmployeeObligations.GetData(ref objDVOMasterEmployeeObligations, "To Do sObj.FlexDeptAcctType");
      if (listSearchResultDVOMasterEmployeeObligations.Count > 0)
      {
        //if (listSearchResultDVOMasterEmployeeObligations.Count > 0)
        foreach (DVOMasterEmployeeObligations objObligation in listSearchResultDVOMasterEmployeeObligations)
        {
          //string _ExpKeyValue = "", _ExpAccountType = "", _ExpAccountDescription = "";
          //int _ExpAccountTypeId = 0;
          //if (objObligation.acct_no > 0)
          //    GetAccountInformation(objObligation.acct_no, out _ExpKeyValue, out _ExpAccountType, out _ExpAccountTypeId, out _ExpAccountDescription);

          //string _LiabKeyValue = "", _LiabAccountType = "", _LiabAccountDescription = "";
          //int _LiabAccountTypeId = 0;
          //if (objObligation.bal_acct_no > 0)
          //    GetAccountInformation(objObligation.bal_acct_no, out _LiabKeyValue, out _LiabAccountType, out _LiabAccountTypeId, out _LiabAccountDescription);

          DataRow dr = dtObligation.NewRow();
          dr["ObligationCode"] = objObligation.obl_code;
          dr["Rate"] = String.Format("{0:0.00}", objObligation.obl_rate);
          dr["AnnualLimit"] = String.Format("{0:0.00}", objObligation.obl_limit);
          dr["PayLimit"] = String.Format("{0:0.00}", objObligation.pay_limit);
          dr["ExpenseAccountNumber"] = objObligation.acct_no;
          dr["lineno"] = objObligation.line_no;
          dr["LiabilityAccountNumber"] = objObligation.bal_acct_no;
          dr["Quarter1"] = String.Format("{0:0.00}", objObligation.obl_qtd1);
          dr["Quarter2"] = String.Format("{0:0.00}", objObligation.obl_qtd2);
          dr["Quarter3"] = String.Format("{0:0.00}", objObligation.obl_qtd3);
          dr["Quarter4"] = String.Format("{0:0.00}", objObligation.obl_qtd4);
          dr["YearToDate"] = String.Format("{0:0.00}", objObligation.obl_ytd);
          dr["ExpenseKeyvalue"] = objObligation.acct_no_kv;//_ExpKeyValue;
          dr["LiabilityKeyvalue"] = objObligation.bal_acct_no_kv;//_LiabKeyValue;
          dr["expaccountid"] = objObligation.acct_no_typeid;//_ExpAccounttypeid;
          dr["liabaccountid"] = objObligation.bal_acct_no_typeid;//_LiabAccounttypeid;
          dr["oblexpaccounttype"] = objObligation.acct_no_type;//_ExpAccounttype;
          dr["oblliabaccounttype"] = objObligation.bal_acct_no_type;//_LiabAccounttype;

          dtObligation.Rows.Add(dr);
          dr = null;
        }
      }
      //dtObligation.AcceptChanges();
      //dgvObligation3.AutoGenerateColumns = false;
      //dgvObligation3.DataSource = dtObligation;
      //objDVOMasterEmployeeObligations = null;
      //listDVOMasterEmployeeObligations = null;

      #endregion "Employee Obligation"

      #region "Employee Position History"

      //create table to bind with datagridview
      System.Data.DataTable dtPosHis = CreateDataTableForPositionHistory();

      DVOPREmployeePositionHistoryInyemppd objDVOPREmployeePositionHistoryInyemppd = new DVOPREmployeePositionHistoryInyemppd();
      objDVOPREmployeePositionHistoryInyemppd.empl_code = "To DO EmplCode";//sObj.EmplCode;
                                                                           //List<DVOPREmployeePositionHistoryInyemppd> listDVOPREmployeePositionHistoryInyemppd = BLLPREmployeePositionHistoryInyemppd.GetData(ref objDVOPREmployeePositionHistoryInyemppd);
      listSearchResultDVOPREmployeePositionHistoryInyemppd = BLLPREmployeePositionHistoryInyemppd.GetData(ref objDVOPREmployeePositionHistoryInyemppd);
      if (listSearchResultDVOPREmployeePositionHistoryInyemppd.Count > 0)
      {
        //if (listSearchResultDVOPREmployeePositionHistoryInyemppd.Count > 0)
        foreach (DVOPREmployeePositionHistoryInyemppd objPosition in listSearchResultDVOPREmployeePositionHistoryInyemppd)
        {
          DataRow dr = dtPosHis.NewRow();
          dr["PositionCode"] = objPosition.pos_code;
          dr["CategoryCode"] = objPosition.cat_code;
          dr["ScaleCode"] = objPosition.scale_code;
          if (objPosition.start_date != null && objPosition.start_date.Trim().Length == 10 && !objPosition.start_date.Trim().Contains("1900"))
            dr["StartDate"] = objPosition.start_date.Trim().Substring(3, 3) + objPosition.start_date.Trim().Substring(0, 3) + objPosition.start_date.Trim().Substring(6, 4);
          if (objPosition.end_date != null && objPosition.end_date.Trim().Length == 10 && !objPosition.end_date.Trim().Contains("1900"))
            dr["EndDate"] = objPosition.end_date.Trim().Substring(3, 3) + objPosition.end_date.Trim().Substring(0, 3) + objPosition.end_date.Trim().Substring(6, 4);
          dr["ApprovedBy"] = objPosition.approved_by;
          dr["Rate"] = String.Format("{0:0.00}", objPosition.pay_rate);
          dr["Position"] = objPosition.temporary;
          dr["RowId"] = objPosition.RowId;
          dr["PositionDescription"] = string.Empty;
          dr["ApprovedByFirstName"] = string.Empty;
          dr["ApprovedByLastName"] = string.Empty;

          dtPosHis.Rows.Add(dr);
          dr = null;
        }
      }
      //dtPosHis.AcceptChanges();
      //dgvPositionHistory.AutoGenerateColumns = false;
      //dgvPositionHistory.DataSource = dtPosHis;
      //objDVOPREmployeePositionHistoryInyemppd = null;
      //listDVOPREmployeePositionHistoryInyemppd = null;

      #endregion "Employee Position History"

      #region "Employee Direct Depostit"

      //create table to bind with datagridview
      System.Data.DataTable dtDirDep = CreateDataTableForDirectDeposit();

      DVOMasterEmpBankDetails objDVOMasterEmpBankDetails = new DVOMasterEmpBankDetails();
      objDVOMasterEmpBankDetails.empl_code = "To Do sObj.EmplCode";
      //List<DVOMasterEmpBankDetails> listDVOMasterEmpBankDetails = BLLMasterEmployeeDeductions.GetData(ref objDVOMasterEmpBankDetails);
      listSearchResultDVOMasterEmpBankDetails = BLLMasterEmpBankDetails.GetData(ref objDVOMasterEmpBankDetails);
      if (listSearchResultDVOMasterEmpBankDetails.Count > 0)
      {
        //if (listSearchResultDVOMasterEmpBankDetails.Count > 0)
        foreach (DVOMasterEmpBankDetails objDirDpst in listSearchResultDVOMasterEmpBankDetails)
        {
          DataRow dr = dtDirDep.NewRow();
          dr["BankCode"] = objDirDpst.bank_code;
          dr["BankDescription"] = objDirDpst.bank_desc;
          dr["CS"] = objDirDpst.typeofacct;
          dr["AccountNo"] = objDirDpst.bank_acct_no;
          dr["T"] = objDirDpst.type;
          if (objDirDpst.type != "R")
            dr["Amount"] = String.Format("{0:0.00}", objDirDpst.amount);
          dr["RowId"] = objDirDpst.RowID;

          dtDirDep.Rows.Add(dr);
          dr = null;
        }
      }
      //dtDirDep.AcceptChanges();
      //dgvDirectDeposit.AutoGenerateColumns = false;
      //dgvDirectDeposit.DataSource = dtDirDep;
      //objDVOMasterEmpBankDetails = null;
      //listDVOMasterEmpBankDetails = null;

      #endregion "Employee Direct Depostit"

      #region "Employee Notes"

      ////Notes_TableRecordId = sObj.EmplCode;
      //objDVOForNotes.NOTES_TABLE_RECORD_ID = sObj.EmplCode;

      //DVOstxnoted objDVOstxnoted = new DVOstxnoted();
      //objDVOstxnoted.filename = objDVOForNotes.TABLE_NAME; //Notes_TableName; //sObj.TABLE_NAME;
      //objDVOstxnoted.record_key = sObj.EmplCode;
      ////listEmployeeNotes = BLLScreenNotesStxnoted.GetData(ref objDVOstxnoted);
      //listCommonNotes = BLLScreenNotesStxnoted.GetData(ref objDVOstxnoted);

      ////List<DVOstxnoted> listDVOstxnoted = BLLScreenNotesStxnoted.GetData(ref objDVOstxnoted);
      ////if (listDVOstxnoted != null && listDVOstxnoted.Count > 0)
      ////{
      ////    _EmployeeNotes = listDVOstxnoted[0].data;
      ////    _EmployeeNotesRowId = listDVOstxnoted[0].Rowid;
      ////}

      #endregion "Employee Notes"
    }
    //}
    //}

    #endregion Search


    public ActionResult Comment(int? id)
    {
      ViewBag.PensionerID = id;
      return View();
    }
    public ActionResult CommentAjaxHandler(JQueryDataTableParamModel param)
    {
      var List = db.PensionerComments;
      IEnumerable<PensionerComments> filtered;


      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => c.CreatedOn.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.Comments.ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<PensionerComments, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Comments :
                                                                                      sortColumnIndex == 1 ? c.CreatedOn + "" :
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

                         c.Comments,
                         String.Format("{0:MM/dd/yyyy}", c.CreatedOn),
                     //c.CreatedOn + "" 
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
    public JsonResult AddComments(int PensionerID, string comments)
    {
      var model = new PensionerComments();
      model.PensionerID = PensionerID;
      model.Comments = comments;
      db.Entry(model).State = EntityState.Added;
      int result = db.SaveChanges();
      return Json(result, JsonRequestBehavior.AllowGet);
    }

    //create a list of DVOGLAccountTypeMaintenance type objects to make collection of account-types
    List<DVOGLAccountTypeMaintenance> objListGLAccountType = null;
    /// <summary>
    /// function to fill account-type in combo box
    /// </summary>
    public List<DVOGLAccountTypeMaintenance> FillAccountType()
    {
      try
      {
        //initialize DVOGLAccountTypeMaintenance type object to send as parameter in function to get Account-types
        DVOGLAccountTypeMaintenance objGLAccountType = new DVOGLAccountTypeMaintenance();
        //if (_Budgeted)
        //    objGLAccountType.AccountCategory = "U";
        //else
        //{
        //    if(!_AllAccounts && !IsAccountCreate)
        //        objGLAccountType.AccountCategory = "-U";
        //}
        //initialize list of DVOGLAccountTypeMaintenance type objects 
        objListGLAccountType = BLLGLAccountTypeMaintenance.GetAccountTypeMaintenance(ref objGLAccountType);
        if (objListGLAccountType != null && objListGLAccountType.Count > 0)
        {
          /*if (_Budgeted)
          {
            objListGLAccountType.RemoveAll(delegate(DVOGLAccountTypeMaintenance objParm)
            {
              return NonBudgetedAccountTypes.Contains("||" + objParm.accounttype.Trim() + "||");
            });
          }*/

          //make blank DVOGLAccountTypeMaintenance type object to set as default selected item in account-type combo box
          objGLAccountType.id = 0;
          objGLAccountType.accounttype = "-- Select Account Type --";
          objGLAccountType.desc = string.Empty;
          objListGLAccountType.Insert(0, objGLAccountType);

          //set datasource of combo box
          return objListGLAccountType;
        }
        else return objListGLAccountType;
      }
      catch { return objListGLAccountType; }
    }

    /// <summary>
    /// to create comboboxes dynamically for the entered Account TypeId
    /// </summary>
    /// <param name="accountTypeId">Account TypeId for which you want to make segment's combo boxes.</param>
    private void CreateComboBoxes(int accountTypeId)
    {
      //initialize DVOGLAccountTypeMaintenance type object to use as a parameter to get AccountSegments
      DVOGLAccountTypeMaintenance objGLAccountTypeMaintenance = new DVOGLAccountTypeMaintenance();
      //set Id to accountTypeId for which you want to create comboboxes
      objGLAccountTypeMaintenance.id = accountTypeId;

      //call GetAccountSegments function of BLLGLAccountTypeMaintenance to get AccountSegments in dataset.
      DataSet ds = BLLGLAccountTypeMaintenance.GetAccountSegments(ref objGLAccountTypeMaintenance);
      objGLAccountTypeMaintenance = null;

      //check dataset has tables and tables has rows or not.
      if (ds.Tables.Count > 0)
        if (ds.Tables[0].Rows.Count > 0)
        {
          //iterate through all rows of table 
          for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
          {
            //call AddComboBox function to add new combo box for acccount-segment
            //ds.Tables[0].Rows[i][4] --> v_desc           -- description of segment
            //ds.Tables[0].Rows[i][1] --> v_flexsegid      -- segmentid of segment 
            //ds.Tables[0].Rows[i][3] --> v_position       -- position of segment in accountkeyvalue
            //ds.Tables[0].Rows[i][2] --> v_length         -- length of segment key in accountkeyvalue
            //AddComboBox(i, ds.Tables[0].Rows[i][4].ToString().Trim(), Convert.ToInt32(ds.Tables[0].Rows[i][1]), Convert.ToInt32(ds.Tables[0].Rows[i][3]), Convert.ToInt32(ds.Tables[0].Rows[i][2]));
          }
          GetSegments();
          //call this function to fill values in first combobox
          //FillComboBox(0, 0);
        }
    }

    DataSet dsSegments;
    private DataSet GetSegments()
    {
      DVOFlexSegment objFlexSegment = new DVOFlexSegment();
      dsSegments = BLLFlexSegment.GetFlexSegmentItems(ref objFlexSegment);
      return dsSegments;
    }


    [HttpGet]
    public ActionResult Edit(string Id)
    {
      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();

      List<SessionViewModel> results = (List<SessionViewModel>)HttpContext.Session["List"];
      var model = results.Where(x => x.ControllerName == "MasterPensioner" && x.ActionName == "Edit").FirstOrDefault();
      if (model == null)
      {
        model = results.Where(x => x.ControllerName == "MasterPensioner").FirstOrDefault();
      }

      if (model != null)
      {
        ViewBag.AddPermission = model.AddPermssion;
        ViewBag.EditPermission = model.EditPermission;
        ViewBag.ViewPermission = model.ViewPermission;
      }
      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(Id));
      DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(IdUrl);
      if (objDVOMasterEmployee == null)
      {
        return HttpNotFound();
      }
      //fill combobox of account-type
      objListGLAccountType = FillAccountType();

      dsSegments = GetSegments();
      string Emplr = string.Empty, keyvalue = string.Empty;
      if (objDVOMasterEmployee.PensionerType != "1")
        Emplr = "SURVIVORS";
      else if (objDVOMasterEmployee.EmplrCode == 2)
        Emplr = "PUBLIC SERVICE";
      else if (objDVOMasterEmployee.EmplrCode == 9)
        Emplr = "POLICE FORCE";
      else Emplr = db.MasterEmployer.FirstOrDefault(x => x.IsActive == true && x.Id == objDVOMasterEmployee.EmplrCode)?.EmployerName.ToUpper();

      var rowColl = dsSegments.Tables[0].AsEnumerable();
      var keyvalueResult = (from r in rowColl
                            where r.Field<string>("v_segitm_desc").Contains(Emplr)
                            select r.Field<string>("v_keyvalue"));//.First<string>();
                                                                  //keyvalue = keyvalueResult.Any() ? keyvalueResult.First<string>() : string.Empty;

      @ViewBag.CId = Id;
      ViewBag.PensionerID = objDVOMasterEmployee.ApplicationReferenceNo;
      ViewBag.EmplCode = objDVOMasterEmployee.EmplCode;
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
      ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
      ViewBag.Nationality = new SelectList(db.MasterCountry.Where(x => x.IsActive == true && x.Name.Trim().ToLower()== "india").OrderBy(x => x.Name), ("CountryCode").ToUpper(), "Name", objDVOMasterEmployee.Nationality);
      ViewBag.PermanentCityID = new SelectList(db.MasterCity.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", objDVOMasterEmployee.City);
      ViewBag.PrefixId = new SelectList(db.MasterPrefix.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", objDVOMasterEmployee.Prefix);
      ViewBag.SuffixId = new SelectList(db.MasterSuffix.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", objDVOMasterEmployee.Suffix);
      //ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", objDVOMasterEmployee.EmplrCode);
      //ViewBag.actPayrollDepartmentKeyValue = Helper.Helper.ToSelectList(dsSegments.Tables[0], "v_keyvalue", "v_segitm_desc", keyvalue);

      //ViewBag.PensionerType = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", objDVOMasterEmployee.PensionerType);
      ViewBag.PensionerType = new SelectList(db.MasterEmpType.OrderBy(x => x.Type_Code), "Emp_type_ID", "Type_Code", objDVOMasterEmployee.PensionerType);

      var emplStatus = from x in EmplStatus()
                       select new { Id = x.Value, Name = x.Key };
      ViewBag.HoldPayment = new SelectList(emplStatus.OrderBy(x => x.Name), "Id", "Name", objDVOMasterEmployee.HoldPayment);//DirectDeposit

      var dirDept = from x in DirectDeposit()
                    select new { Id = x.Value, Name = x.Key };
      ViewBag.DirDept = new SelectList(dirDept.OrderBy(x => x.Name), "Id", "Name", objDVOMasterEmployee.DirDept);
      var District = db.MasterEmployees.Select(x => new { Id = x.SelectDistrict, Name = x.SelectDistrict })
     .Distinct().OrderBy(x => x.Name).ToList();
      ViewBag.SelectDistrict = new SelectList(District, "Id", "Name", objDVOMasterEmployee.SelectDistrict);


      var PresentTehsilList = db.MasterEmployees.Select(x => new { Id = x.PresentTehsil, Name = x.PresentTehsil })
       .Distinct().OrderBy(x => x.Name).ToList();
      ViewBag.PresentTehsil = new SelectList(PresentTehsilList, "Id", "Name", objDVOMasterEmployee.PresentTehsil);
      var PermanentTehsilList = db.MasterEmployees.Select(x => new { Id = x.PermanentTehsil, Name = x.PermanentTehsil })
     .Distinct().OrderBy(x => x.Name).ToList();
      ViewBag.PermanentTehsil = new SelectList(PermanentTehsilList, "Id", "Name", objDVOMasterEmployee.PresentTehsil);

     // var PermanentTehsilPermanentList = db.MasterEmployees.Select(x => new { Id = x.PermanentHalqaPanchayatOrMunicipalityName, Name = x.PermanentHalqaPanchayatOrMunicipalityName })
     //.Distinct().OrderBy(x => x.Name).ToList();

     // ViewBag.PermanentHalqaPanchayatMunicipalityName = new SelectList(PermanentTehsilPermanentList, "Id", "Name", objDVOMasterEmployee.PermanentHalqaPanchayatMunicipalityName);
      //ViewBag.PFRateID = new SelectList(db.MasterPFRate.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", objDVOMasterEmployee.PFRateID);


      //var PresentHalqaPanchayatMunicipalityName = db.MasterEmployees.Select(x => new { Id = x.PresentHalqaPanchayatOrMunicipalityName, Name = x.PresentHalqaPanchayatOrMunicipalityName })
      //  .Distinct().OrderBy(x => x.Name).ToList();
      //ViewBag.PresentHalqaPanchayatMunicipalityName = new SelectList(PresentHalqaPanchayatMunicipalityName, "Id", "Name", objDVOMasterEmployee.PresentHalqaPanchayatMunicipalityName);

      //int userid = AppUserManager.GetUserId();
      ViewBag.roleidlist = db.UserRole.Where(x => x.UserId == userid).FirstOrDefault().RoleId;
      //ViewBag.AgeInYears = objDVOMasterEmployee.AgeInYears;
      ViewBag.FatherOrHusbandOrGuardianName = objDVOMasterEmployee.FatherOrHusbandOrGuardianName;
      ViewBag.PresentAddress = objDVOMasterEmployee.PresentAddress;
      var PresentDistrict = db.MasterEmployees.Select(x => new { Id = x.PresentDistrict, Name = x.PresentDistrict })
        .Distinct().OrderBy(x => x.Name).ToList();

      ViewBag.PresentDistrict = new SelectList(PresentDistrict, "Id", "Name", objDVOMasterEmployee.PresentDistrict);
      ViewBag.PresentVillageName = objDVOMasterEmployee.PresentVillageName;
      ViewBag.PermanentAddress = objDVOMasterEmployee.PermanentAddress;

      ViewBag.SubmissionLocation = objDVOMasterEmployee.SubmissionLocation;
      ViewBag.SubmissionDate = objDVOMasterEmployee.SubmissionDate;
      ViewBag.Category = objDVOMasterEmployee.Category;
      ViewBag.LastTask = objDVOMasterEmployee.LastTask;
      objDVOMasterEmployee.ReasonForChange = TrimStart(objDVOMasterEmployee.ReasonForChange, "<br />");


      int RoleId = _DbContext.UserRole.FirstOrDefault(x => x.UserId == userid).RoleId;
      ViewBag.permitionlist = db.EditApplicantFields.Where(p => p.RoleID == RoleId).ToList();
      return View(objDVOMasterEmployee);

    }
    [HttpPost]
    public ActionResult Edit(DVOMasterEmployee objDVOMasterEmployee)
    {
      int userid = AppUserManager.GetUserId();
      // string Id = UrlEncryption.Decrypt(objDVOMasterEmployee.Id);
      if (ModelState.IsValid)
      {
        //db.Entry(objDVOMasterEmployee).State = EntityState.Modified;
        //db.SaveChanges();
        //TempData["success"] = "Details Updated Successfully";
        int result = 0;
        try
        {
          int i = UpdateEmployeeInformation(objDVOMasterEmployee);
          if (i > 0)
          {
            // Code By Himanshu Rajput find this Districts user Id base Email send 

            var userRole = db.UserRole.FirstOrDefault(x => x.UserId == userid);
            bool districtPermission = userRole != null && db.Roles.Any(x => x.Id == userRole.RoleId && x.Name == "Districts");
            if (districtPermission)
            {
              var mailSetting = db.MailSettings.FirstOrDefault(a => a.IsActive && (a.ProcessName + "").Trim().ToLower() == ("DistrictUser").ToLower());
              if (mailSetting != null)
              {
                MailSettings mailMessages = new MailSettings();
                try
                {


                  mailMessages.Subject = "District user changes in the beneficiary record Notification";
                  mailMessages.ProcessName = "";
                  mailMessages.MailTo = "";
                  dynamic email = new Email("Districtuser");
                  if (SiteHelper.IsTestEmail == "1")
                  {
                    email.To = SiteHelper.TestEmail;
                  }
                  else
                  {
                    email.To = mailSetting.MailTo;
                  }
                  email.CC = mailSetting.CC;
                  email.BCC = mailSetting.BCC;
                  //email.CustomerName = "";
                  email.hostURL = SiteHelper.WebsiteURL;

                  //To read Body from mail object.
                  string htmlText = string.Empty;
                  try
                  {
                    Postal.IEmailService emailService = new Postal.EmailService();
                    System.Net.Mail.MailMessage message = emailService.CreateMailMessage(email);
                    using (var StreamObj = message.AlternateViews.FirstOrDefault().ContentStream)
                    using (StreamReader reader = new StreamReader(StreamObj))
                    {
                      htmlText = reader.ReadToEnd();
                    }
                  }
                  catch (Exception ex)
                  {
                    htmlText = ex.Message;
                  }
                  mailMessages.Contents = htmlText;
                  ///////////


                  if (mailSetting != null && mailSetting.IsInstantMailing)
                  {
                    email.Send();
                    //mailMessages.IsInstantMailing = true;
                    //mailMessages.CreatedOn= DateTime.Now;
                  }
                }
                catch (Exception ex)
                {

                  // mailMessages.IsSent = false;
                  //mailMessages.ErrorDescription = ex.Message;
                }

              }


            }
            TempData["success"] = "Beneficiary Record Updated Successfully";
          }
          else
          {
            TempData["error"] = "Beneficiary Record Not Updated, try again";
          }
        }
        catch
        {
          TempData["error"] = "Exception";
          result = 0;
        }

        //return RedirectToAction("Index");
        //return RedirectToAction("Edit");
        return RedirectToAction("Edit", new { Id = UrlEncryption.EncryptURL(Convert.ToString(objDVOMasterEmployee.EmplCode)) });
      }
      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(objDVOMasterEmployee.Id));
      var objDVO = SearchAgainEmployeeInfo(IdUrl);
      dsSegments = GetSegments();
      string Emplr = string.Empty, keyvalue = string.Empty;
      if (objDVOMasterEmployee.PensionerType != "1")
        Emplr = "SURVIVORS";
      else if (objDVOMasterEmployee.EmplrCode == 2)
        Emplr = "PUBLIC SERVICE";
      else if (objDVOMasterEmployee.EmplrCode == 9)
        Emplr = "POLICE FORCE";
      else Emplr = db.MasterEmployer.Where(x => x.IsActive == true && x.Id == objDVOMasterEmployee.EmplrCode).FirstOrDefault().EmployerName.ToUpper();

      var rowColl = dsSegments.Tables[0].AsEnumerable();
      var keyvalueResult = (from r in rowColl
                            where r.Field<string>("v_segitm_desc").Contains(Emplr)
                            select r.Field<string>("v_keyvalue"));//.First<string>();
      keyvalue = keyvalueResult.Any() ? keyvalueResult.First<string>() : string.Empty;

      ViewBag.PensionerID = objDVOMasterEmployee.ApplicationReferenceNo;
      ViewBag.EmplCode = objDVOMasterEmployee.EmplCode;
      ViewBag.Nationality = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), ("CountryCode").ToUpper(), "Name", objDVOMasterEmployee.Nationality);
      var emplStatus = from x in EmplStatus()
                       select new { Id = x.Value, Name = x.Key };
      ViewBag.HoldPayment = new SelectList(emplStatus.OrderBy(x => x.Name), "Id", "Name", objDVOMasterEmployee.HoldPayment);//DirectDeposit
      var PresentTehsilList = db.MasterEmployees.Select(x => new { Id = x.PresentTehsil, Name = x.PresentTehsil })
      .Distinct().OrderBy(x => x.Name).ToList();
      ViewBag.PresentTehsil = new SelectList(PresentTehsilList, "Id", "Name", objDVOMasterEmployee.PresentTehsil);
      var PermanentTehsilList = db.MasterEmployees.Select(x => new { Id = x.PermanentTehsil, Name = x.PermanentTehsil })
     .Distinct().OrderBy(x => x.Name).ToList();

     // var PermanentTehsilPermanentList = db.MasterEmployees.Select(x => new { Id = x.PermanentHalqaPanchayatOrMunicipalityName, Name = x.PermanentHalqaPanchayatOrMunicipalityName })
     //.Distinct().OrderBy(x => x.Name).ToList();

     // ViewBag.PermanentHalqaPanchayatMunicipalityName = new SelectList(PermanentTehsilPermanentList, "Id", "Name", objDVOMasterEmployee.PermanentHalqaPanchayatMunicipalityName);


      //var PresentHalqaPanchayatMunicipalityName = db.MasterEmployees.Select(x => new { Id = x.PresentHalqaPanchayatOrMunicipalityName, Name = x.PresentHalqaPanchayatOrMunicipalityName })
      //  .Distinct().OrderBy(x => x.Name).ToList();
      //ViewBag.PresentHalqaPanchayatMunicipalityName = new SelectList(PresentHalqaPanchayatMunicipalityName, "Id", "Name", objDVOMasterEmployee.PresentHalqaPanchayatMunicipalityName);
      ViewBag.roleidlist = db.UserRole.Where(x => x.UserId == userid).FirstOrDefault().RoleId;

      int RoleId = _DbContext.UserRole.FirstOrDefault(x => x.UserId == userid).RoleId;
      ViewBag.permitionlist = db.EditApplicantFields.Where(p => p.RoleID == RoleId).ToList();

      ViewBag.PermanentTehsil = new SelectList(PermanentTehsilList, "Id", "Name", objDVOMasterEmployee.PresentTehsil);
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
      ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
      ViewBag.NationalityID = new SelectList(db.MasterNationality.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", objDVOMasterEmployee.Nationality);
      ViewBag.PermanentCityID = new SelectList(db.MasterCity.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", objDVOMasterEmployee.City);
      ViewBag.PrefixId = new SelectList(db.MasterPrefix.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", objDVOMasterEmployee.Prefix);
      ViewBag.SuffixId = new SelectList(db.MasterSuffix.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", objDVOMasterEmployee.Suffix);
      ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", objDVOMasterEmployee.EmplrCode);
      ViewBag.actPayrollDepartmentKeyValue = Helper.Helper.ToSelectList(dsSegments.Tables[0], "v_keyvalue", "v_segitm_desc", keyvalue);

      ViewBag.PensionerType = new SelectList(db.MasterPensionerType.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", objDVOMasterEmployee.PensionerType);

      ViewBag.EmplStatus = new SelectList(emplStatus.OrderBy(x => x.Name), "Id", "Name", objDVOMasterEmployee.EmplStatus);

      var dirDept = from x in DirectDeposit() select new { Id = x.Value, Name = x.Key };
      ViewBag.DirDept = new SelectList(dirDept.OrderBy(x => x.Name), "Id", "Name", objDVOMasterEmployee.DirDept);
      ViewBag.FatherOrHusbandOrGuardianName = objDVOMasterEmployee.FatherOrHusbandOrGuardianName;
      ViewBag.PresentAddress = objDVOMasterEmployee.PresentAddress;
      var PresentDistrict = db.MasterEmployees.Select(x => new { Id = x.PresentDistrict, Name = x.PresentDistrict })
      .Distinct().OrderBy(x => x.Name).ToList();

      ViewBag.PresentDistrict = new SelectList(PresentDistrict, "Id", "Name", objDVOMasterEmployee.PresentDistrict);
      ViewBag.PresentVillageName = objDVOMasterEmployee.PresentVillageName;

      ViewBag.PermanentAddress = objDVOMasterEmployee.PermanentAddress;

      ViewBag.SubmissionLocation = objDVOMasterEmployee.SubmissionLocation;
      ViewBag.SubmissionDate = objDVOMasterEmployee.SubmissionDate;
      ViewBag.Category = objDVOMasterEmployee.Category;
      ViewBag.LastTask = objDVOMasterEmployee.LastTask;
      var District = db.MasterEmployees.Select(x => new { Id = x.SelectDistrict, Name = x.SelectDistrict })
      .Distinct().OrderBy(x => x.Name).ToList();
      ViewBag.SelectDistrict = new SelectList(District, "Id", "Name", objDVOMasterEmployee.SelectDistrict);
      objDVOMasterEmployee.ReasonForChange = TrimStart(objDVO.ReasonForChange, "<br />");
      List<SessionViewModel> results = (List<SessionViewModel>)HttpContext.Session["List"];
      var model = results.Where(x => x.ControllerName == "MasterPensioner" && x.ActionName == "Edit").FirstOrDefault();
      if (model == null)
      {
        model = results.Where(x => x.ControllerName == "MasterPensioner").FirstOrDefault();
      }

      if (model != null)
      {
        ViewBag.AddPermission = model.AddPermssion;
        ViewBag.EditPermission = model.EditPermission;
        ViewBag.ViewPermission = model.ViewPermission;
      }
      return View(objDVOMasterEmployee);
    }


    [HttpGet]
    public ActionResult ActiveContributorIndex()
    {
      return View();
    }
    public ActionResult ActiveContributorAjaxHandler(JQueryDataTableParamModel param)
    {
      var List = db.MasterContributor.Where(x => x.IsActive == true && x.JobStatusID == 1);
      IEnumerable<MasterContributor> filtered;

      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => c.PersonID.ToLower().Contains(param.sSearch.ToLower())
           || c.OldPersonID.ToLower().Contains(param.sSearch.ToLower())
           || (c.FirstName + " " + c.MidName + " " + c.LastName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
           //|| c.MidName.ToLower().Contains(param.sSearch.ToLower())
           //|| c.LastName.ToLower().Contains(param.sSearch.ToLower())
           || c.Employer.EmployerName.ToLower().Contains(param.sSearch.ToLower())
           || c.SocialSecurityNo.ToLower().Contains(param.sSearch.ToLower())
           || c.DateOfBirth.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.PostalAddress.ToLower().Contains(param.sSearch.ToLower())
           || c.PhoneOffice.ToLower().Contains(param.sSearch.ToLower())
           || c.Status.Name.ToLower().Contains(param.sSearch.ToLower()));
      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<MasterContributor, string> orderingFunction = (c => sortColumnIndex == 0 ? c.FirstName :
                                                            sortColumnIndex == 1 ? c.MidName :
                                                            sortColumnIndex == 2 ? c.LastName :

                                                                                      sortColumnIndex == 3 ? c.Employer.EmployerName :
                                                                                      sortColumnIndex == 4 ? c.PersonID :
                                                                                      sortColumnIndex == 5 ? c.OldPersonID :
                                                                                      sortColumnIndex == 6 ? c.DateOfBirth + "" :
                                                                                      sortColumnIndex == 7 ? c.SocialSecurityNo :
                                                                                      sortColumnIndex == 8 ? c.PostalAddress :
                                                                                      sortColumnIndex == 9 ? c.PhoneOffice :
                                                                                      sortColumnIndex == 10 ? c.Status.Name :
                                                                                      "");

      var sortDirection = Request["sSortDir_0"]; // asc or desc
      if (sortDirection == "asc")
        filtered = filtered.OrderBy(orderingFunction);
      else
        filtered = filtered.OrderByDescending(orderingFunction);

      //Pagging
      var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

      //Select required columns
      var result = from c in displayed.OrderBy(x => x.EmployerID)
                   select new[] { 
                     //c.ProfilePath,
                         c.PersonID,
                         //c.OldPersonID,
										 c.FirstName+" "+c.MidName+" "+c.LastName,
                                         c.Employer.EmployerName,
                                         c.SocialSecurityNo,
                     c.Gender,
                     String.Format("{0:mm/dd/yyyy}", c.DateOfBirth),
                     c.PostalAddress,
                     c.Phone,
                     c.PhoneOffice,
                     //c.Status.Name,
                     c.Id+""
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
    public ActionResult ContributorDetails(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterContributor model = db.MasterContributor.Find(id);
      if (model == null)
      {
        return HttpNotFound();
      }
      return View(model);
    }

    [HttpGet]
    public ActionResult MoveToPensionerEdit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      dsSegments = GetSegments();
      ViewBag.PensionerTypeID = new SelectList(db.MasterPensionerType.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", 1);
      MasterContributor model = db.MasterContributor.Find(id);

      dsSegments = GetSegments();
      string Emplr = string.Empty, keyvalue = string.Empty;
      if (model.JobStatusID == 2)
        Emplr = "SURVIVORS";
      else if (model.EmployerID == 2)
        Emplr = "PUBLIC SERVICE";
      else if (model.EmployerID == 9)
        Emplr = "POLICE FORCE";
      else Emplr = model.Employer.EmployerName.ToUpper();

      var rowColl = dsSegments.Tables[0].AsEnumerable();
      var keyvalueResult = (from r in rowColl
                            where r.Field<string>("v_segitm_desc").Contains(Emplr)
                            select r.Field<string>("v_keyvalue"));//.First<string>();
      keyvalue = keyvalueResult.Any() ? keyvalueResult.First<string>() : string.Empty;

      ViewBag.EmployeeID = Helper.Helper.ToSelectList(dsSegments.Tables[0], "v_keyvalue", "v_segitm_desc", keyvalue);

      var dirDept = from x in DirectDeposit()
                    select new { Id = x.Value, Name = x.Key };
      ViewBag.DirDept = new SelectList(dirDept.OrderBy(x => x.Name), "Id", "Name");

      //ViewBag.actPayrollDepartmentKeyValue = Helper.Helper.ToSelectList(dsSegments.Tables[0], "v_keyvalue", "v_segitm_desc", objDVOMasterEmployee.PensionerType, objDVOMasterEmployee.EmplrCode);
      if (model == null)
      {
        return HttpNotFound();
      }
      return View(model);
    }
    public JsonResult checkApplicationPendingAjax(int? Id)
    {
      int result = 0;
      if (db.PensionApplications.Any(x => x.PersonID == Id.ToString() && x.IsActive == true) || db.RefundApplications.Any(x => x.PersonID == Id.ToString() && x.IsActive == true))
        result = 1;
      return Json(result, JsonRequestBehavior.AllowGet);
    }
    public JsonResult Create(int? Id, string ResignationDate, string actPayrollDepartmentKeyValue)
    {
      string error = "";
      int result = 0;
      try
      {

        dsSegments = GetSegments();
        ViewBag.PensionerTypeID = new SelectList(db.MasterPensionerType.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", 1);

        var objDVOMasterEmployeeModel = new MasterPensioner();
        var ContributorDetails = db.MasterContributor.Where(x => x.Id == Id).FirstOrDefault();

        dsSegments = GetSegments();
        string Emplr = string.Empty, keyvalue = string.Empty;
        if (ContributorDetails.JobStatusID == 2)
          Emplr = "SURVIVORS";
        else if (ContributorDetails.EmployerID == 2)
          Emplr = "PUBLIC SERVICE";
        else if (ContributorDetails.EmployerID == 9)
          Emplr = "POLICE FORCE";
        else Emplr = ContributorDetails.Employer.EmployerName.ToUpper();

        var rowColl = dsSegments.Tables[0].AsEnumerable();
        var keyvalueResult = (from r in rowColl
                              where r.Field<string>("v_segitm_desc").Contains(Emplr)
                              select r.Field<string>("v_keyvalue"));//.First<string>();
        keyvalue = keyvalueResult.Any() ? keyvalueResult.First<string>() : string.Empty;

        ViewBag.EmployeeID = Helper.Helper.ToSelectList(dsSegments.Tables[0], "v_keyvalue", "v_segitm_desc", keyvalue);

        var dirDept = from x in DirectDeposit()
                      select new { Id = x.Value, Name = x.Key };
        ViewBag.DirDept = new SelectList(dirDept.OrderBy(x => x.Name), "Id", "Name");

        ContributorDetails.JobStatusID = 3;
        ContributorDetails.RetirementOrResignationDate = DateTime.Parse(ResignationDate);
        if ((((ContributorDetails.RetirementOrResignationDate.Value.Date - ContributorDetails.FirstAppointmentDate.Value.Date).TotalDays) / 365) > 10)
        {
          ContributorDetails.MapTo(objDVOMasterEmployeeModel);

          DVOMasterEmployee objSearchCriteriaDVOMasterEmployeeModel = new DVOMasterEmployee();
          objSearchCriteriaDVOMasterEmployeeModel.PersonID = ContributorDetails.PersonID;
          var checkRecord = SearchEmployeeInformation(objSearchCriteriaDVOMasterEmployeeModel);// db.MasterPensioner.Where(x => x.PersonID == ContributorDetails.PersonID);
          if (!checkRecord.Any())
          {
            App.Data.ViewModels.PensionCalculationViewModel calculatedResult = new App.Data.ViewModels.PensionCalculationViewModel();
            DateTime date = Convert.ToDateTime(ContributorDetails.RetirementOrResignationDate);
            if (ContributorDetails.Employer.EmployerTypeID == (int)PensionType.Police)
              calculatedResult = PensionAndRefundRepo.CalculatePolicePensionDetails(date, ContributorDetails.Id, "No", "No");
            else
              calculatedResult = PensionAndRefundRepo.CalculatePublicPensionDetails(date, ContributorDetails.Id, "No");


            objDVOMasterEmployeeModel.LengthOfQualifyingServiceInMonthsTo31Dec2003 = calculatedResult.LengthOfQualifyingServiceInMonthsTo31Dec2003;
            objDVOMasterEmployeeModel.LengthOfQualifyingServiceInMonthsFrom1Jan2014 = calculatedResult.LengthOfQualifyingServiceInMonthsFrom1Jan2014;
            objDVOMasterEmployeeModel.AnnualAmount = calculatedResult.FullPension;
            objDVOMasterEmployeeModel.PensionerID = "P0000" + (db.MasterPensioner.Count() + 1);
            objDVOMasterEmployeeModel.PensionerTypeId = 1;

            /*db.Entry(entity).State = EntityState.Added;
            db.SaveChanges();
            result = entity.Id;*/


            string EmpCode = string.Empty;
            int i = InsertEmployeeInformation(objDVOMasterEmployeeModel, out EmpCode, actPayrollDepartmentKeyValue + "######");
            if (i > 0)
            {
              db.Entry(ContributorDetails).State = EntityState.Modified;
              db.SaveChanges();

              TempData["success"] = "Contributor moved to Beneficiary Successfully";
              result = Convert.ToInt32(EmpCode);
              return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
              error = "Contributor to Beneficiary movement not done, try again";
            }
            return Json(error, JsonRequestBehavior.AllowGet);
          }
          else
          {
            error = "This Record Already moved to Beneficiary";
          }
          return Json(error, JsonRequestBehavior.AllowGet);
        }
        else
        {
          error = "Not Eligible for pension";
          return Json(error, JsonRequestBehavior.AllowGet);
        }
      }
      catch (Exception ex)
      {
        error = ex.Message;
        return Json(error, JsonRequestBehavior.AllowGet);
      }
    }

    private object Calculation(string p, DateTime? nullable)
    {
      throw new NotImplementedException();
    }
    public ActionResult AddBankDetails(int? Id)
    {
      DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(Id.ToString());
      ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
      ViewBag.CId = Id;
      //var typeofacct = from typeofacct e in Enum.GetValues(typeof(typeofacct))
      //                 select new { Id = e, Name = e.ToString() };
      var typeofacct = from x in TypeOfAccount()
                       select new { Id = x.Value, Name = x.Key };
      ViewBag.typeofacct = new SelectList(typeofacct.OrderBy(x => x.Name), "Id", "Name");
      //var BankAmountType = from BankAmountType e in Enum.GetValues(typeof(BankAmountType))
      //           select new { Id = e, Name = e.ToString() };
      var bankAmountType = from x in TypeOfAmount()
                           select new { Id = x.Value, Name = x.Key };
      ViewBag.type = new SelectList(bankAmountType.OrderBy(x => x.Name), "Id", "Name");
      ViewBag.bank_code = new SelectList(db.MasterBanks.OrderBy(x => x.bank_desc), "bank_code", "bank_desc");
      return View();
    }
    [HttpPost]
    public ActionResult AddBankDetails(MasterEmpBankDetails model)
    {
      int id = model.emp_bank_ID;
      DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(model.empl_code);
      ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
      ViewBag.CId = model.empl_code;
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
      ViewBag.bank_code = new SelectList(db.MasterBanks.OrderBy(x => x.bank_desc), "bank_code", "bank_desc", model.bank_code);

      if (ModelState.IsValid)
      {
        decimal percentage = 0;
        decimal amount = 0;
        decimal income = 0;
        var incomes = db.MasterEmployeeIncomes.Where(x => x.Empl_Code == model.empl_code).Select(x => x.inc_rate).FirstOrDefault();
        if (incomes != null || incomes != 0)
        {
          income = Convert.ToDecimal(incomes);
        }
        var result = db.MasterEmpBankDetails.Where(x => x.empl_code == model.empl_code).ToList();
        foreach (var item in result)
        {
          if (item.type == "P")
          {
            percentage += (decimal)item.amount;
          }
          else
          {
            amount += (decimal)item.amount;
          }
        }
        if (model.type == "P")
        {
          percentage += (decimal)model.amount;
        }
        else
        {
          amount += (decimal)model.amount;
        }

        percentage += (amount / income) * 100;
        if (percentage <= 100 && amount <= income)
        {

          int count = result.Where(x => x.bank_code == model.bank_code).Count();
          if (count == 0)
          {

            #region "Direct Deposit"
            MasterEmpBankDetails objDVOMasterEmpBankDetails;
            //make object to pass as parameter of search function
            DVOUpdateBankCode objDVOUpdateBankCode = new DVOUpdateBankCode();
            objDVOUpdateBankCode.bank_code = model.bank_code;
            //call getDate function of BLL
            List<DVOUpdateBankCode> listDVOUpdateBankCode = BLLUpdateBankCode.GetBankCodeDetails(ref objDVOUpdateBankCode);

            //make list of detail-objects of main object
            List<DVOMasterEmpBankDetails> listDVOMasterEmpBankDetails = new List<DVOMasterEmpBankDetails>();
            //check datagridview is enable or not if not, no need to add objects into list
            //if (dgvIncome1.Enabled)
            //check datagridview has some items to add or not
            if (listDVOUpdateBankCode.Any())
            {
              //if datagridview has some rows then make new object with values entered in rows
              foreach (var drGrid in listDVOUpdateBankCode)
              {
                if (drGrid.bank_code != null && drGrid.bank_code.Length > 0 && drGrid.bank_code == model.bank_code)
                {
                  //var type = string.Empty;
                  //if (model.type == "Percentage")
                  //  type = "P";
                  //else if (model.type == "Amount")
                  //  type = "A";
                  //else if (model.type == "Remainder")
                  //  type = "R";

                  //var accountType = string.Empty;
                  //if (model.typeofacct == "Current")
                  //  accountType = "C";
                  //else if (model.typeofacct == "Saving")
                  //  accountType = "S";

                  objDVOMasterEmpBankDetails = new MasterEmpBankDetails();
                  //assign appropriate values to detail-object
                  objDVOMasterEmpBankDetails.empl_code = model.empl_code;
                  objDVOMasterEmpBankDetails.line_no = db.MasterEmpBankDetails.Count() + 1;
                  objDVOMasterEmpBankDetails.bank_code = drGrid.bank_code;
                  objDVOMasterEmpBankDetails.bank_acct_no = model.bank_acct_no;
                  objDVOMasterEmpBankDetails.type = model.type;
                  objDVOMasterEmpBankDetails.typeofacct = model.typeofacct;

                  if (objDVOMasterEmpBankDetails.type.ToString().Trim().ToUpper() != "R")
                  {
                    if (model.amount != null)
                      objDVOMasterEmpBankDetails.amount = Convert.ToDecimal(model.amount);
                  }

                  //objDVOMasterEmpBankDetails.InsertMachineInfo = Program.MachineInfo;
                  //objDVOMasterEmpBankDetails.InsertBy = Program.UserId;
                  //objDVOMasterEmpBankDetails.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                  //add into list
                  //listDVOMasterEmpBankDetails.Add(objDVOMasterEmpBankDetails);
                  db.Entry(objDVOMasterEmpBankDetails).State = EntityState.Added;
                  db.SaveChanges();
                }
              }
            }

            #endregion "Direct Deposit"

            return RedirectToAction("BankDetailsIndex", new { Id = model.empl_code });
          }
          else
          {
            TempData["error"] = "This Bank Code Already Exist";
          }
        }
        else
        {
          TempData["error"] = "total Percentage should be less than 100%";
        }
      }
      //var typeofacct = from typeofacct e in Enum.GetValues(typeof(typeofacct))
      //                 select new { Id = e, Name = e.ToString() };
      var typeofacct = from x in TypeOfAccount()
                       select new { Id = x.Value, Name = x.Key };
      ViewBag.typeofacct = new SelectList(typeofacct.OrderBy(x => x.Name), "Id", "Name", model.typeofacct);
      //var BankAmountType = from BankAmountType e in Enum.GetValues(typeof(BankAmountType))
      //                     select new { Id = e, Name = e.ToString() };
      var bankAmountType = from x in TypeOfAmount()
                           select new { Id = x.Value, Name = x.Key };
      ViewBag.type = new SelectList(bankAmountType.OrderBy(x => x.Name), "Id", "Name");
      return View();
    }

    public Dictionary<string, char> TypeOfAmount()
    {
      Dictionary<string, char> AmountType = new Dictionary<string, char>();
      AmountType.Add("Percentage", 'P');
      AmountType.Add("Amount", 'A');
      AmountType.Add("Remainder", 'R');
      return AmountType;
    }

    public Dictionary<string, char> DirectDeposit()
    {
      Dictionary<string, char> dir_dept = new Dictionary<string, char>();
      dir_dept.Add("Yes", 'Y');
      dir_dept.Add("No", 'N');
      return dir_dept;
    }

    public Dictionary<string, char> EmplStatus()
    {
      Dictionary<string, char> emplStatus = new Dictionary<string, char>();
      emplStatus.Add("Active", 'N');
      emplStatus.Add("InActive", 'Y');
      return emplStatus;
    }

    public Dictionary<string, char> TypeOfAccount()
    {
      Dictionary<string, char> AccountType = new Dictionary<string, char>();
      AccountType.Add("Current", 'C');
      AccountType.Add("Saving", 'S');
      return AccountType;
    }
    public Dictionary<string, char> dedFrequency()
    {
      Dictionary<string, char> Frequency = new Dictionary<string, char>();
      Frequency.Add("Weekly", 'W');
      Frequency.Add("Monthly", 'M');
      Frequency.Add("Yearly", 'Y');
      return Frequency;
    }

    public ActionResult EditBankDetails(string Id)
    {
      int userid = AppUserManager.GetUserId();
      //var userRole = db.UserRole.FirstOrDefault(x => x.UserId == userid);
      //bool districtPermission = userRole != null && db.Roles.Any(x => x.Id == userRole.RoleId && x.Name == "Districts");
      //ViewBag.districtPermission = districtPermission;

      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();

      List<SessionViewModel> results = (List<SessionViewModel>)HttpContext.Session["List"];
      var models = results.Where(x => x.ControllerName == "MasterPensioner" && x.ActionName == "Edit").FirstOrDefault();
      if (models == null)
      {
        models = results.Where(x => x.ControllerName == "MasterPensioner").FirstOrDefault();
      }

      if (models != null)
      {
        ViewBag.AddPermission = models.AddPermssion;
        ViewBag.EditPermission = models.EditPermission;
        ViewBag.ViewPermission = models.ViewPermission;
      }
      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(Id));
      int ids = Convert.ToInt32(IdUrl);
      if (Id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterEmpBankDetails model = db.MasterEmpBankDetails.Find(ids);
      if (model == null)
      {
        model = new MasterEmpBankDetails();
      }

      DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(model.empl_code);
      ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
      ViewBag.ApplicationReferenceNo = objDVOMasterEmployee.ApplicationReferenceNo;
      ViewBag.empl_code = model.empl_code;
      // var Empl_code = _DbContext.MasterEmployees.Where(x => x.ApplicationReferenceNo == model.empl_code).Select(x => x.Empl_Code);
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
      ViewBag.CId = Id;  //model.empl_code;
      ViewBag.bank_code = new SelectList(db.MasterBanks.OrderBy(x => x.bank_desc), "bank_code", "bank_desc", model.bank_code);
      @ViewBag.Id = Id;


      //var typeofacct = from typeofacct e in Enum.GetValues(typeof(typeofacct))
      //                 select new { Id = e, Name = e.ToString() };
      var typeofacct = from x in TypeOfAccount()
                       select new { Id = x.Value, Name = x.Key };

      ViewBag.typeofacct = new SelectList(typeofacct.OrderBy(x => x.Name), "Id", "Name", model.typeofacct);
      //var BankAmountType = from BankAmountType e in Enum.GetValues(typeof(BankAmountType))
      //select new { Id = e, Name = e.ToString() };
      var bankAmountType = from x in TypeOfAmount()
                           select new { Id = x.Value, Name = x.Key };
      ViewBag.Email = objDVOMasterEmployee.EMail;
      ViewBag.APPLICANT_BANK_IFSC_CODE = model.APPLICANT_BANK_IFSC_CODE;
      ViewBag.type = new SelectList(bankAmountType.OrderBy(x => x.Name), "Id", "Name", model.type);
      ViewBag.roleidlist = db.UserRole.Where(x => x.UserId == userid).FirstOrDefault().RoleId;
      model.Remarks = TrimStart(model.Remarks, "<br />");
      int RoleId = _DbContext.UserRole.FirstOrDefault(x => x.UserId == userid).RoleId;
      ViewBag.permitionlist = db.EditApplicantFields.Where(p => p.RoleID == RoleId).ToList();

      return View(model);
    }
    [HttpPost]
    public ActionResult EditBankDetails(MasterEmpBankDetailsVM model)
    {

      //var type = string.Empty;
      //if (model.type == "Percentage")
      //  type = "P";
      //else if (model.type == "Amount")
      //  type = "A";
      //else if (model.type == "Remainder")
      //  type = "R";

      //var accountType = string.Empty;
      //if (model.typeofacct == "Current")
      //  accountType = "C";
      //else if (model.typeofacct == "Saving")
      //  accountType = "S";
      //model.type = "A";



      try
      {


        // Check account number is unique
        bool isExists = db.MasterEmpBankDetails.Where(x => x.empl_code != model.empl_code && x.bank_acct_no == model.bank_acct_no).Any();
        if (isExists)
        {
          TempData["error"] = "This Bank Account Number Already Exist";
          return RedirectToAction("EditBankDetails", new { Id = UrlEncryption.EncryptURL(Convert.ToString(model.empl_code)) });
        }
        string id = UrlEncryption.Decrypt(model.emp_bank_ID);
        int Ids = Convert.ToInt32(id);

        //rohit check old or new changes data
        int userid = AppUserManager.GetUserId();
        var userRole = db.UserRole.FirstOrDefault(x => x.UserId == userid);
        bool districtPermission = userRole != null && db.Roles.Any(x => x.Id == userRole.RoleId && x.Name == "Districts");
        var existingData = db.MasterEmpBankDetails.FirstOrDefault(x => x.emp_bank_ID == Ids);
        if (districtPermission)
        {
          if (existingData != null)
          {
            if (existingData.bank_code != model.bank_code ||
                existingData.bank_acct_no != model.bank_acct_no ||
                // Add more properties to compare here
                existingData.ACCOUNT_STATUS != model.ACCOUNT_STATUS ||
                existingData.APPLICANT_BANK_IFSC_CODE != model.APPLICANT_BANK_IFSC_CODE ||
                existingData.AADHAAR_STATUS != model.AADHAAR_STATUS ||
                existingData.ACCT_SCHEME_TYPE != model.ACCT_SCHEME_TYPE ||
                existingData.CBS_NAME != model.CBS_NAME ||
                existingData.Remarks != model.Remarks ||
                //existingData.Bank_document != model.Bank_document||
                existingData.BRANCH_CODE != model.BRANCH_CODE)

            {
              if (model.document == null)
              {
                // return Json(new { Message = "Supportive document is required.", Status = false });
                ToastError("Supportive document is required.");
                return RedirectToAction("EditBankDetails", new { Id = UrlEncryption.EncryptURL(Convert.ToString(model.empl_code)) });
              }
            }
          }
        }


        //int id = model.emp_bank_ID;
        DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(model.empl_code);
        ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
        //ViewBag.ApplicationReferenceNo = objDVOMasterEmployee.EmplCode;
        ViewBag.CId = model.empl_code;
        ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
        ViewBag.bank_code = new SelectList(db.MasterBanks.OrderBy(x => x.bank_desc), "bank_code", "bank_desc", model.bank_code);
        //var typeofacct = from typeofacct e in Enum.GetValues(typeof(typeofacct))
        //                 select new { Id = e, Name = e.ToString() };
        var typeofacct = from x in TypeOfAccount()
                         select new { Id = x.Value, Name = x.Key };
        ViewBag.typeofacct = new SelectList(typeofacct.OrderBy(x => x.Name), "Id", "Name", model.typeofacct);

        //var BankAmountType = from BankAmountType e in Enum.GetValues(typeof(BankAmountType))
        //                     select new { Id = e, Name = e.ToString() };
        var bankAmountType = from x in TypeOfAmount()
                             select new { Id = x.Value, Name = x.Key };

        ViewBag.type = new SelectList(bankAmountType.OrderBy(x => x.Name), "Id", "Name", model.type);

        if (ModelState.IsValid)
        {
          decimal percentage = 0;
          decimal amount = 0;
          //var test = model.empl_code;
          decimal income = (decimal)db.MasterEmployeeIncomes.Where(x => x.Empl_Code == model.empl_code).Select(x => x.inc_rate).FirstOrDefault();
          var result = db.MasterEmpBankDetails.Where(x => x.empl_code == model.empl_code && x.emp_bank_ID != Ids).ToList();
          foreach (var item in result)
          {
            if (item.type == "P")
            {
              percentage += (decimal)item.amount;
            }
            else
            {
              amount += (decimal)item.amount;
            }
          }
          if (model.type == "P")
          {
            percentage += (decimal)model.amount;
          }
          else
          {
            amount += (decimal)model.amount;
          }
          // percentage += (amount / income) * 100;

          List<AuditLogs> auditLogs = new List<AuditLogs>();

          if (amount <= income)
          {
            MasterEmpBankDetails objDVOMasterEmpBankDetails = db.MasterEmpBankDetails.Find(Ids);
            if (ModelState.IsValid && !db.MasterEmpBankDetails.Where(x => x.empl_code == model.empl_code && x.emp_bank_ID != Ids && x.bank_code == model.bank_code).Any())
            {

              #region "Direct Deposit"
              //MasterEmpBankDetails objDVOMasterEmpBankDetails;
              //make object to pass as parameter of search function
              DVOUpdateBankCode objDVOUpdateBankCode = new DVOUpdateBankCode();
              objDVOUpdateBankCode.bank_code = model.bank_code;
              //call getDate function of BLL
              List<DVOUpdateBankCode> listDVOUpdateBankCode = BLLUpdateBankCode.GetBankCodeDetails(ref objDVOUpdateBankCode);

              //make list of detail-objects of main object
              List<DVOMasterEmpBankDetails> listDVOMasterEmpBankDetails = new List<DVOMasterEmpBankDetails>();
              //check datagridview is enable or not if not, no need to add objects into list
              //if (dgvIncome1.Enabled)


              //This code is working previously
              #region
              //if (listDVOUpdateBankCode.Any())
              //{
              //  //if datagridview has some rows then make new object with values entered in rows
              //  foreach (var drGrid in listDVOUpdateBankCode)
              //  {
              //    if (drGrid.bank_code != null && drGrid.bank_code.Length > 0 && drGrid.bank_code == model.bank_code)
              //    {

              //      //objDVOMasterEmpBankDetails = new MasterEmpBankDetails();
              //      //assign appropriate values to detail-object
              //      objDVOMasterEmpBankDetails.empl_code = model.empl_code;
              //      objDVOMasterEmpBankDetails.line_no = db.MasterEmpBankDetails.Where(x => x.empl_code == model.empl_code && x.emp_bank_ID == Ids).FirstOrDefault().line_no;
              //      objDVOMasterEmpBankDetails.bank_code = drGrid.bank_code;
              //      objDVOMasterEmpBankDetails.bank_acct_no = model.bank_acct_no;
              //      objDVOMasterEmpBankDetails.type = model.type;
              //      objDVOMasterEmpBankDetails.typeofacct = model.typeofacct;

              //      if (objDVOMasterEmpBankDetails.type.ToString().Trim().ToUpper() != "R")
              //      {
              //        if (model.amount != null)
              //          objDVOMasterEmpBankDetails.amount = Convert.ToDecimal(model.amount);
              //      }

              //      //objDVOMasterEmpBankDetails.InsertMachineInfo = Program.MachineInfo;
              //      //objDVOMasterEmpBankDetails.InsertBy = Program.UserId;
              //      //objDVOMasterEmpBankDetails.InsertDate = Program.CurrentDate.ToString(Program.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

              //      //add into list
              //      //listDVOMasterEmpBankDetails.Add(objDVOMasterEmpBankDetails);
              //      db.Entry(objDVOMasterEmpBankDetails).State = EntityState.Modified;
              //      db.SaveChanges();
              //      //DbContextHelper.dBsavechanges(db);

              //    }
              //  }
              //}
              #endregion

              model.Remarks = " [" + AppUserManager.GetUserName() + "] : " + model.Remarks;
              var filename = string.Empty;
              //supportive document Uploaded
              try
              {
                if (model.document != null && model.document.ContentLength > 0)
                {
                  var uploadPath = Server.MapPath("~/DataFiles/Supportivedocument/");

                  if (!Directory.Exists(uploadPath))
                  {
                    Directory.CreateDirectory(uploadPath);
                  }

                  filename = DateTime.Now.ToString("ddMMyyyyHHmmss") + "~" + model.document.FileName; // Generate a unique filename
                  var filePath = Path.Combine(uploadPath, filename);

                  model.document.SaveAs(filePath); // Save the file
                }

              }
              catch (Exception ex)
              {
                //return Json(new { success = false, message = "Error uploading file: " + ex.Message });
                ToastError("Error uploading file: " + ex.Message);
                return RedirectToAction("EditBankDetails", new { Id = UrlEncryption.EncryptURL(Convert.ToString(model.empl_code)) });
              }



              //Replaced by this code
              //string connectionString = WebConfigurationManager.AppSettings["SQLConn"];
              string connectionString = ConnectionStringProvider.GetConnectionString();
              string updateSql = "UPDATE MasterEmpBankDetails " +
                                "SET empl_code = @empl_code, " +
                                "    bank_acct_no = @bank_acct_no, " +
                                "    type = @type, " +
                                "    typeofacct = @typeofacct, " +
                                "    APPLICANT_BANK_IFSC_CODE = @APPLICANT_BANK_IFSC_CODE, " +
                                "    amount = @amount, " +
                                 "    CBS_NAME = @CBS_NAME ," +
                                  "    BRANCH_CODE = @BRANCH_CODE ," +
                                   "    ACCOUNT_STATUS = @ACCOUNT_STATUS, " +
                                    "    AADHAAR_STATUS = @AADHAAR_STATUS ," +
                                     "    ACCT_SCHEME_TYPE = @ACCT_SCHEME_TYPE ," +
                                "    Remarks = CONCAT(Remarks, (CASE WHEN Remarks = NULL OR Remarks = '' THEN '' ELSE '<br />' END), NCHAR(8226), ' [', FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm'), '] ', @Remarks) ," +
                                 "    Bank_document = @Bank_document " +
                                "WHERE empl_code = @empl_code AND emp_bank_ID = @emp_bank_ID";



              using (SqlConnection connection = new SqlConnection(connectionString))
              {
                connection.Open();

                foreach (var drGrid in listDVOUpdateBankCode)
                {
                  if (drGrid.bank_code != null && drGrid.bank_code.Length > 0 && drGrid.bank_code == model.bank_code)
                  {
                    if (model.bank_acct_no != null)
                      if (objDVOMasterEmpBankDetails.bank_acct_no != model.bank_acct_no)
                      {
                        AuditLogs objtmp = new AuditLogs
                        {
                          CreatedBy = AppUserManager.GetUserId(),
                          CreatedOn = DateTime.Now,
                          ModifiedBy = AppUserManager.GetUserId(),
                          ModifiedOn = DateTime.Now,
                          IsActive = true,
                          EventType = "Update",
                          TableName = "MasterEmpBankDetails",
                          ColumnName = "bank_acct_no",
                          OldValue = objDVOMasterEmpBankDetails.bank_acct_no.ToString(),
                          NewValue = model.bank_acct_no.ToString(),
                          RecordId = Convert.ToInt32(objDVOMasterEmpBankDetails.empl_code),
                        };
                        auditLogs.Add(objtmp);
                      }
                    if (model.type != null)
                      if (objDVOMasterEmpBankDetails.type != model.type)
                      {
                        AuditLogs objtmp = new AuditLogs
                        {
                          CreatedBy = AppUserManager.GetUserId(),
                          CreatedOn = DateTime.Now,
                          ModifiedBy = AppUserManager.GetUserId(),
                          ModifiedOn = DateTime.Now,
                          IsActive = true,
                          EventType = "Update",
                          TableName = "MasterEmpBankDetails",
                          ColumnName = "type",
                          OldValue = objDVOMasterEmpBankDetails.type,
                          NewValue = model.type,
                          RecordId = Convert.ToInt32(objDVOMasterEmpBankDetails.empl_code),
                        };
                        auditLogs.Add(objtmp);
                      }
                    if (model.typeofacct != null)
                      if (objDVOMasterEmpBankDetails.typeofacct != model.typeofacct)
                      {
                        AuditLogs objtmp = new AuditLogs
                        {
                          CreatedBy = AppUserManager.GetUserId(),
                          CreatedOn = DateTime.Now,
                          ModifiedBy = AppUserManager.GetUserId(),
                          ModifiedOn = DateTime.Now,
                          IsActive = true,
                          EventType = "Update",
                          TableName = "MasterEmpBankDetails",
                          ColumnName = "typeofacct",
                          OldValue = objDVOMasterEmpBankDetails.typeofacct,
                          NewValue = model.typeofacct,
                          RecordId = Convert.ToInt32(objDVOMasterEmpBankDetails.empl_code),
                        };
                        auditLogs.Add(objtmp);
                      }
                    if (model.amount != null)
                      if (objDVOMasterEmpBankDetails.amount != model.amount)
                      {
                        AuditLogs objtmp = new AuditLogs
                        {
                          CreatedBy = AppUserManager.GetUserId(),
                          CreatedOn = DateTime.Now,
                          ModifiedBy = AppUserManager.GetUserId(),
                          ModifiedOn = DateTime.Now,
                          IsActive = true,
                          EventType = "Update",
                          TableName = "MasterEmpBankDetails",
                          ColumnName = "amount",
                          OldValue = objDVOMasterEmpBankDetails.amount != null ? Convert.ToDecimal(objDVOMasterEmpBankDetails.amount).ToString("0.00") : "",
                          NewValue = model.amount != null ? Convert.ToDecimal(model.amount).ToString("0.00") : "",
                          RecordId = Convert.ToInt32(objDVOMasterEmpBankDetails.empl_code),
                        };
                        auditLogs.Add(objtmp);
                      }
                    if (model.APPLICANT_BANK_IFSC_CODE != null)
                      if (objDVOMasterEmpBankDetails.APPLICANT_BANK_IFSC_CODE != model.APPLICANT_BANK_IFSC_CODE)
                      {
                        AuditLogs objtmp = new AuditLogs
                        {
                          CreatedBy = AppUserManager.GetUserId(),
                          CreatedOn = DateTime.Now,
                          ModifiedBy = AppUserManager.GetUserId(),
                          ModifiedOn = DateTime.Now,
                          IsActive = true,
                          EventType = "Update",
                          TableName = "MasterEmpBankDetails",
                          ColumnName = "APPLICANT_BANK_IFSC_CODE",
                          OldValue = objDVOMasterEmpBankDetails.APPLICANT_BANK_IFSC_CODE,
                          NewValue = model.APPLICANT_BANK_IFSC_CODE,
                          RecordId = Convert.ToInt32(objDVOMasterEmpBankDetails.empl_code),
                        };
                        auditLogs.Add(objtmp);
                      }
                    if (model.Remarks != null)
                      if (objDVOMasterEmpBankDetails.Remarks != model.Remarks)
                      {
                        AuditLogs objtmp = new AuditLogs
                        {
                          CreatedBy = AppUserManager.GetUserId(),
                          CreatedOn = DateTime.Now,
                          ModifiedBy = AppUserManager.GetUserId(),
                          ModifiedOn = DateTime.Now,
                          IsActive = true,
                          EventType = "Update",
                          TableName = "MasterEmpBankDetails",
                          ColumnName = "Remarks",
                          OldValue = objDVOMasterEmpBankDetails.Remarks != null ? objDVOMasterEmpBankDetails.Remarks : "",
                          NewValue = model.Remarks != null ? "• [" + DateTime.Now.ToString("dd/MM/yyyy") + "] " + model.Remarks : "",
                          RecordId = Convert.ToInt32(objDVOMasterEmpBankDetails.empl_code),
                        };
                        auditLogs.Add(objtmp);
                      }
                    if (model.CBS_NAME != null)
                      if (objDVOMasterEmpBankDetails.CBS_NAME != model.CBS_NAME)
                      {
                        AuditLogs objtmp = new AuditLogs
                        {
                          CreatedBy = AppUserManager.GetUserId(),
                          CreatedOn = DateTime.Now,
                          ModifiedBy = AppUserManager.GetUserId(),
                          ModifiedOn = DateTime.Now,
                          IsActive = true,
                          EventType = "Update",
                          TableName = "MasterEmpBankDetails",
                          ColumnName = "CBS_NAME",
                          OldValue = objDVOMasterEmpBankDetails.CBS_NAME != null ? objDVOMasterEmpBankDetails.CBS_NAME : "",
                          NewValue = model.CBS_NAME != null ? model.CBS_NAME : "",
                          RecordId = Convert.ToInt32(objDVOMasterEmpBankDetails.empl_code),
                        };
                        auditLogs.Add(objtmp);
                      }

                    if (model.BRANCH_CODE != null)
                      if (objDVOMasterEmpBankDetails.BRANCH_CODE != model.BRANCH_CODE)
                      {
                        AuditLogs objtmp = new AuditLogs
                        {
                          CreatedBy = AppUserManager.GetUserId(),
                          CreatedOn = DateTime.Now,
                          ModifiedBy = AppUserManager.GetUserId(),
                          ModifiedOn = DateTime.Now,
                          IsActive = true,
                          EventType = "Update",
                          TableName = "MasterEmpBankDetails",
                          ColumnName = "BRANCH_CODE",
                          OldValue = objDVOMasterEmpBankDetails.BRANCH_CODE != null ? objDVOMasterEmpBankDetails.BRANCH_CODE : "",
                          NewValue = model.BRANCH_CODE != null ? model.BRANCH_CODE : "",
                          RecordId = Convert.ToInt32(objDVOMasterEmpBankDetails.empl_code),
                        };
                        auditLogs.Add(objtmp);
                      }

                    if (model.ACCOUNT_STATUS != null)
                      if (objDVOMasterEmpBankDetails.ACCOUNT_STATUS != model.ACCOUNT_STATUS)
                      {
                        AuditLogs objtmp = new AuditLogs
                        {
                          CreatedBy = AppUserManager.GetUserId(),
                          CreatedOn = DateTime.Now,
                          ModifiedBy = AppUserManager.GetUserId(),
                          ModifiedOn = DateTime.Now,
                          IsActive = true,
                          EventType = "Update",
                          TableName = "MasterEmpBankDetails",
                          ColumnName = "ACCOUNT_STATUS",
                          OldValue = objDVOMasterEmpBankDetails.ACCOUNT_STATUS != null ? objDVOMasterEmpBankDetails.ACCOUNT_STATUS : "",
                          NewValue = model.ACCOUNT_STATUS != null ? model.ACCOUNT_STATUS : "",
                          RecordId = Convert.ToInt32(objDVOMasterEmpBankDetails.empl_code),
                        };
                        auditLogs.Add(objtmp);
                      }

                    if (model.AADHAAR_STATUS != null)
                      if (objDVOMasterEmpBankDetails.AADHAAR_STATUS != model.AADHAAR_STATUS)
                      {
                        AuditLogs objtmp = new AuditLogs
                        {
                          CreatedBy = AppUserManager.GetUserId(),
                          CreatedOn = DateTime.Now,
                          ModifiedBy = AppUserManager.GetUserId(),
                          ModifiedOn = DateTime.Now,
                          IsActive = true,
                          EventType = "Update",
                          TableName = "MasterEmpBankDetails",
                          ColumnName = "AADHAAR_STATUS",
                          OldValue = objDVOMasterEmpBankDetails.AADHAAR_STATUS != null ? objDVOMasterEmpBankDetails.AADHAAR_STATUS : "",
                          NewValue = model.AADHAAR_STATUS != null ? model.AADHAAR_STATUS : "",
                          RecordId = Convert.ToInt32(objDVOMasterEmpBankDetails.empl_code),
                        };
                        auditLogs.Add(objtmp);
                      }

                    if (model.ACCT_SCHEME_TYPE != null)
                      if (objDVOMasterEmpBankDetails.ACCT_SCHEME_TYPE != model.ACCT_SCHEME_TYPE)
                      {
                        AuditLogs objtmp = new AuditLogs
                        {
                          CreatedBy = AppUserManager.GetUserId(),
                          CreatedOn = DateTime.Now,
                          ModifiedBy = AppUserManager.GetUserId(),
                          ModifiedOn = DateTime.Now,
                          IsActive = true,
                          EventType = "Update",
                          TableName = "MasterEmpBankDetails",
                          ColumnName = "ACCT_SCHEME_TYPE",
                          OldValue = objDVOMasterEmpBankDetails.ACCT_SCHEME_TYPE != null ? objDVOMasterEmpBankDetails.ACCT_SCHEME_TYPE : "",
                          NewValue = model.ACCT_SCHEME_TYPE != null ? model.ACCT_SCHEME_TYPE : "",
                          RecordId = Convert.ToInt32(objDVOMasterEmpBankDetails.empl_code),
                        };
                        auditLogs.Add(objtmp);
                      }


                    if (model.document != null)
                      if (objDVOMasterEmpBankDetails.Bank_document != model.document.FileName)
                      {
                        AuditLogs objtmp = new AuditLogs
                        {
                          CreatedBy = AppUserManager.GetUserId(),
                          CreatedOn = DateTime.Now,
                          ModifiedBy = AppUserManager.GetUserId(),
                          ModifiedOn = DateTime.Now,
                          IsActive = true,
                          EventType = "Update",
                          TableName = "MasterEmpBankDetails",
                          ColumnName = "Bank_document",
                          OldValue = objDVOMasterEmpBankDetails.Bank_document != null ? objDVOMasterEmpBankDetails.Bank_document : "",
                          NewValue = model.document.FileName != null ? model.document.FileName : "",
                          RecordId = Convert.ToInt32(objDVOMasterEmpBankDetails.empl_code),
                        };
                        auditLogs.Add(objtmp);
                      }


                    using (SqlCommand cmd = new SqlCommand(updateSql, connection))
                    {
                      cmd.Parameters.AddWithValue("@empl_code", model.empl_code);
                      cmd.Parameters.AddWithValue("@bank_acct_no", model.bank_acct_no);
                      cmd.Parameters.AddWithValue("@type", model.type);
                      cmd.Parameters.AddWithValue("@typeofacct", model.typeofacct);
                      cmd.Parameters.AddWithValue("@amount", model.amount);
                      cmd.Parameters.AddWithValue("@emp_bank_ID", Ids);
                      cmd.Parameters.AddWithValue("@APPLICANT_BANK_IFSC_CODE", model.APPLICANT_BANK_IFSC_CODE);
                      cmd.Parameters.AddWithValue("@CBS_NAME", model.CBS_NAME);
                      cmd.Parameters.AddWithValue("@BRANCH_CODE", model.BRANCH_CODE);
                      cmd.Parameters.AddWithValue("@ACCOUNT_STATUS", model.ACCOUNT_STATUS);
                      cmd.Parameters.AddWithValue("@AADHAAR_STATUS", model.AADHAAR_STATUS);
                      cmd.Parameters.AddWithValue("@ACCT_SCHEME_TYPE", model.ACCT_SCHEME_TYPE);
                      cmd.Parameters.AddWithValue("@Remarks", model.Remarks);
                      cmd.Parameters.AddWithValue("@Bank_document", filename == "" ? (model.Bank_document == null ? "" : model.Bank_document) : filename);

                      int rowsAffected = cmd.ExecuteNonQuery();

                      if (rowsAffected > 0)
                      {
                        string updateUserSql = "UPDATE MasterEmployee " +
                                "SET hold_pymnt = @hold_pymnt " +
                                "WHERE empl_code = @empl_code";

                        using (SqlCommand cmdU = new SqlCommand(updateUserSql, connection))
                        {
                          cmdU.Parameters.AddWithValue("@hold_pymnt", model.ACCOUNT_STATUS == "ACTIVE" ? "N" : "Y");
                          cmdU.Parameters.AddWithValue("@empl_code", model.empl_code);
                          cmdU.ExecuteNonQuery();
                        }
                        // Save audit logs
                        if (auditLogs.Any())
                        {
                          _DbContext.AuditLogs.AddRange(auditLogs);
                          _DbContext.SaveChanges();
                        }
                        auditLogs = new List<AuditLogs>();
                        return RedirectToAction("BankDetailsIndex", new { Id = UrlEncryption.EncryptURL(Convert.ToString(model.empl_code)) });
                      }
                      else
                      {
                        auditLogs = new List<AuditLogs>();
                        return Json(new { Message = "Data not found or no changes made.", Status = false });
                      }
                    }
                  }
                }
              }

              #endregion "Direct Deposit"

              return RedirectToAction("BankDetailsIndex", new { Id = UrlEncryption.EncryptURL(Convert.ToString(model.empl_code)) });
            }
            else
            {
              TempData["error"] = "This Bank Code Already Exist";
            }
          }
          else
          {
            if (model.type == "P")
            {
              ToastError("Total Percentage always less than 100%");
              return RedirectToAction("EditBankDetails", new { Id = UrlEncryption.EncryptURL(Convert.ToString(model.empl_code)) });
            }
            else
            {
              ToastError("Amount shouldn't less then Income. Pleae set Income first and then set Amount.");
              return RedirectToAction("EditBankDetails", new { Id = UrlEncryption.EncryptURL(Convert.ToString(model.empl_code)) });
            }
          }
        }
      }
      catch (Exception)
      {
        throw;
      }
      return RedirectToAction("BankDetailsIndex", new { Id = UrlEncryption.EncryptURL(Convert.ToString(model.empl_code)) });
    }

    public ActionResult Obligation(int? Id)
    {
      @ViewBag.Id = Id;
      DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(Id.ToString());
      ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
      return View();
    }

    public ActionResult DeductionIndex(int? Id)
    {
      @ViewBag.CId = Id;
      DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(Id.ToString());
      ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
      return View();
    }
    public ActionResult IncomeIndex(string Id)
    {
      string Ids = UrlEncryption.Decrypt(Id);
      @ViewBag.CId = Id;
      DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(Ids);
      ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
      ViewBag.ApplicationReferenceNo = objDVOMasterEmployee.ApplicationReferenceNo;
      ViewBag.Email = objDVOMasterEmployee.EMail;
      ViewBag.TypeCode = db.MasterEmpType.FirstOrDefault(x => x.Type_Code.ToLower().Trim() == objDVOMasterEmployee.TypeCode.ToLower().Trim())?.Description;
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);

      @ViewBag.CId = @ViewBag.CId == null ? string.Empty : @ViewBag.CId;
      ViewBag.PensionerID = ViewBag.PensionerID == null ? string.Empty : @ViewBag.PensionerID;
      ViewBag.ApplicationReferenceNo = ViewBag.ApplicationReferenceNo == null ? string.Empty : @ViewBag.ApplicationReferenceNo;
      ViewBag.Email = ViewBag.Email == null ? string.Empty : @ViewBag.Email;
      ViewBag.TypeCode = ViewBag.TypeCode == null ? string.Empty : @ViewBag.TypeCode;
      ViewBag.Name = ViewBag.Name == null ? string.Empty : @ViewBag.Name;
      return View();
    }
    public ActionResult BankDetailsIndex(string Id)
    {
      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(Id));
      //int id = Convert.ToInt32(IdUrl);
      @ViewBag.CId = Id;
      DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(IdUrl);
      ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
      ViewBag.ApplicationReferenceNo = objDVOMasterEmployee.ApplicationReferenceNo;
      ViewBag.TypeCode = db.MasterEmpType.FirstOrDefault(x => x.Type_Code.ToLower().Trim() == objDVOMasterEmployee.TypeCode.ToLower().Trim())?.Description;
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);

      @ViewBag.CId = @ViewBag.CId == null ? string.Empty : @ViewBag.CId;
      ViewBag.PensionerID = ViewBag.PensionerID == null ? string.Empty : @ViewBag.PensionerID;
      ViewBag.ApplicationReferenceNo = ViewBag.ApplicationReferenceNo == null ? string.Empty : @ViewBag.ApplicationReferenceNo;
      ViewBag.TypeCode = ViewBag.TypeCode == null ? string.Empty : @ViewBag.TypeCode;
      ViewBag.Name = ViewBag.Name == null ? string.Empty : @ViewBag.Name;
      return View();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    public ActionResult IncomeAjaxHandler(JQueryDataTableParamModel param, string PensionerID)
    {
      var List = db.MasterEmployeeIncomes.Where(x => x.Empl_Code == PensionerID);
      IEnumerable<MasterEmployeeIncomes> filtered;


      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => c.inc_code.ToLower().Contains(param.sSearch.ToLower())
           || c.inc_rate.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.hi_inc_amt.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.inc_number.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.Empl_Code.ToString().ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<MasterEmployeeIncomes, string> orderingFunction = (c => sortColumnIndex == 0 ? c.inc_code :
                                                                                      sortColumnIndex == 1 ? c.inc_rate + "" :
                                                                                       sortColumnIndex == 2 ? c.inc_number + "" :
                                                                                        sortColumnIndex == 3 ? c.hi_inc_amt + "" :

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
                     c.inc_number+"",
                     (c.inc_rate * c.inc_number).ToString(),
                     c.ReasonForChange==null?"": TrimStart(c.ReasonForChange, "<br />"),
             UrlEncryption.EncryptURL(Convert.ToString(c.EmpIncomeID))
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

    public ActionResult DeductionAjaxHandler(JQueryDataTableParamModel param, string PensionerID)
    {
      var List = db.MasterEmployeeDeductions.Where(x => x.Empl_Code == PensionerID);
      IEnumerable<MasterEmployeeDeductions> filtered;


      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => c.Ded_Code.ToLower().Contains(param.sSearch.ToLower())
           || c.ded_rate.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.ded_date.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.balanceamt.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.pay_limit.ToString().ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<MasterEmployeeDeductions, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Ded_Code :
                                                                                      sortColumnIndex == 1 ? c.ded_rate + "" :
                                                                                       sortColumnIndex == 2 ? c.ded_date + "" :
                                                                                        sortColumnIndex == 3 ? c.ded_limit + "" :
                                                                                        sortColumnIndex == 4 ? c.pay_limit + "" :
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

                         c.Ded_Code,
                                         c.ded_rate+"",
                     c.ded_limit+"",
                     c.pay_limit+"",
                      String.Format("{0:MM/dd/yyyy}", c.ded_date),
                     c.EmpDeductionID+""
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

    public ActionResult ObligationAjaxHandler(JQueryDataTableParamModel param, string PensionerID)
    {
      var List = db.MasterEmployeeObligations.Where(x => x.Empl_Code == PensionerID);
      IEnumerable<MasterEmployeeObligations> filtered;


      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => c.Obl_Code.ToLower().Contains(param.sSearch.ToLower())
           || c.Obl_Rate.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.pay_limit.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.Obl_limit.ToString().ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<MasterEmployeeObligations, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Obl_Code :
                                                                                      sortColumnIndex == 1 ? c.Obl_Rate + "" :
                                                                                       sortColumnIndex == 2 ? c.Obl_limit + "" :
                                                                                        sortColumnIndex == 3 ? c.pay_limit + "" :
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

                         c.Obl_Code,
                                         c.Obl_Rate+"",
                     c.Obl_limit+"",
                     c.pay_limit+""
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

    public ActionResult BankDetailsAjaxHandler(JQueryDataTableParamModel param, string PensionerID)
    {
      try
      {


        var List = db.MasterEmpBankDetails.Where(x => x.empl_code == PensionerID);
        IEnumerable<MasterEmpBankDetails> filtered;


        if (!string.IsNullOrEmpty(param.sSearch))
        {
          filtered = List
             .Where(c => c.bank_code.Trim().ToLower().Contains(param.sSearch.Trim().ToLower())
             //|| c.bank.bank_desc.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.bank_acct_no.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.amount.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.type.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.typeofacct.ToString().ToLower().Contains(param.sSearch.ToLower()));

        }
        else
        {
          filtered = List;
        }

        //Sorting through column index
        var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

        Func<MasterEmpBankDetails, string> orderingFunction = (c => sortColumnIndex == 0 ? c.bank_code :
                                                                    sortColumnIndex == 1 ? c.bank.bank_desc + "" :
                                                                    sortColumnIndex == 2 ? c.bank_acct_no + "" :
                                                                    sortColumnIndex == 3 ? c.amount + "" :
                                                                    sortColumnIndex == 4 ? c.type + "" :
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
                         c.bank.bank_code,
                         //c.typeofacct+"",
                         c.bank_acct_no+"",
                         c.CBS_NAME+"",
                         c.BRANCH_CODE+"",
                         c.ACCOUNT_STATUS+"",
                         c.AADHAAR_STATUS+"",
                         c.type+"",
                          //c.amount+"",
                          
                         c.bank.bank_desc,
                         TrimStart(c.Remarks, "<br />")+"",
                         c.Bank_document,
                       UrlEncryption.EncryptURL(Convert.ToString(c.emp_bank_ID)),



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
      catch (Exception ex)
      {
        throw;
      }
    }

    [HttpGet]
    public ActionResult AddDeductionDetails(int? Id)
    {
      @ViewBag.CId = Id;
      DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(Id.ToString());
      ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);

      ViewBag.ded_code = new SelectList(db.MasterDedcodes.OrderBy(x => x.description), "ded_code", "description");
      var dedfrequency = from x in dedFrequency()
                         select new { Id = x.Value, Name = x.Key };
      ViewBag.ded_apply = new SelectList(dedfrequency.OrderBy(x => x.Name), "Id", "Name");
      return View();
    }
    [HttpPost]
    public ActionResult AddDeductionDetails(MasterEmployeeDeductions model)
    {
      int id = model.EmpDeductionID;
      int userid = AppUserManager.GetUserId();
      ViewBag.PensionerID = model.Empl_Code;
      ViewBag.ded_code = new SelectList(db.MasterDedcodes.OrderBy(x => x.description), "ded_code", "description", model.Ded_Code);
      int count = db.MasterEmployeeDeductions.Where(x => x.Empl_Code == model.Empl_Code && x.Ded_Code == model.Ded_Code).Count();
      if (count == 0)
      {
        if (ModelState.IsValid)
        {

          #region "Employee Deduction"
          MasterEmployeeDeductions objDVOMasterEmployeeDeductions;
          DVOMasterEmployeeDeductions objDVOMasterEmployeeDeduction;
          //make object to pass as parameter of search function
          DVOPRDeductionCodesMasterDedcodes objDVOPRDeductionCodesMasterDedcodes = new DVOPRDeductionCodesMasterDedcodes();
          objDVOPRDeductionCodesMasterDedcodes.ded_code = model.Ded_Code;
          //call getDate function of BLL
          List<DVOPRDeductionCodesMasterDedcodes> listDVOPRDeductionCodesMasterDedcodes = BLLPRDeductionCodesMasterDedcodes.GetData(ref objDVOPRDeductionCodesMasterDedcodes);

          //make list of detail-objects of main object
          List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions = new List<DVOMasterEmployeeDeductions>();
          //check datagridview is enable or not if not, no need to add objects into list
          //if (dgvIncome1.Enabled)
          //check datagridview has some items to add or not
          if (listDVOPRDeductionCodesMasterDedcodes.Any())
          {
            //if datagridview has some rows then make new object with values entered in rows
            foreach (var drGrid in listDVOPRDeductionCodesMasterDedcodes)
            {
              if (drGrid.ded_code != null && drGrid.ded_code.Length > 0 && drGrid.ded_code == model.Ded_Code)
              {
                objDVOMasterEmployeeDeductions = new MasterEmployeeDeductions();
                objDVOMasterEmployeeDeduction = new DVOMasterEmployeeDeductions();
                //assign appropriate values to detail-object
                objDVOMasterEmployeeDeductions.Empl_Code = model.Empl_Code;
                objDVOMasterEmployeeDeduction.empl_code = model.Empl_Code;
                objDVOMasterEmployeeDeductions.Ded_Code = drGrid.ded_code;
                objDVOMasterEmployeeDeduction.ded_code = drGrid.ded_code;
                objDVOMasterEmployeeDeductions.ded_rate = model.ded_rate;//tOdO//objDVOMasterEmployeeModel.AnnualAmount / 12;
                objDVOMasterEmployeeDeduction.ded_rate = model.ded_rate;
                objDVOMasterEmployeeDeductions.ded_limit = model.ded_limit;
                objDVOMasterEmployeeDeduction.ded_limit = model.ded_limit;
                objDVOMasterEmployeeDeductions.pay_limit = model.pay_limit;
                objDVOMasterEmployeeDeduction.pay_limit = model.pay_limit;
                objDVOMasterEmployeeDeductions.ded_apply = model.ded_apply;
                objDVOMasterEmployeeDeduction.ded_apply = model.ded_apply;
                objDVOMasterEmployeeDeductions.ded_date = model.ded_date;
                objDVOMasterEmployeeDeduction.ded_date = model.ded_date == null ? string.Empty : model.ded_date.ToString();
                objDVOMasterEmployeeDeductions.line_no = (Int64)db.MasterEmployeeDeductions.Count() + 1;
                if (drGrid.dflt_acct != null)
                {
                  objDVOMasterEmployeeDeductions.acct_no = Convert.ToInt32(drGrid.dflt_acct);
                  objDVOMasterEmployeeDeduction.acct_no = Convert.ToInt32(drGrid.dflt_acct);
                }
                //objDVOMasterEmployeeIncomes.acct_no_kv = "000";
                //if (drGrid.dfltaccounttype != null)
                //objDVOMasterEmployeeIncomes.acct_no_type = drGrid.dfltaccounttype;

                objDVOMasterEmployeeDeductions.department = "000";
                objDVOMasterEmployeeDeduction.department = "000";
                objDVOMasterEmployeeDeduction.InsertMachineInfo = System.Environment.MachineName;
                objDVOMasterEmployeeDeduction.InsertBy = userid;
                objDVOMasterEmployeeDeduction.InsertDate = System.DateTime.Now.ToShortDateString();

                //add into list
                listDVOMasterEmployeeDeductions.Add(objDVOMasterEmployeeDeduction);

                db.Entry(objDVOMasterEmployeeDeductions).State = EntityState.Added;
                //db.SaveChanges();
              }
            }
            //Save List
            try
            {
              //call insert function of BLL
              int i = BLLMasterEmployee.InsertEmployeeDeductions(ref listDVOMasterEmployeeDeductions);
              listDVOMasterEmployeeDeductions = null;
            }
            catch (Exception ex)
            {
              //Success = false;
              ErrorMessage = ex.Message;
            }
          }
        }

        #endregion "Employee Deduction"
        return RedirectToAction("DeductionIndex", new { Id = model.Empl_Code });
      }
      @ViewBag.CId = model.Empl_Code;
      var dedfrequency = from x in dedFrequency()
                         select new { Id = x.Value, Name = x.Key };
      ViewBag.ded_apply = new SelectList(dedfrequency.OrderBy(x => x.Name), "Id", "Name", model.ded_apply);
      return View();
    }

    [HttpGet]
    public ActionResult EditDeductionDetails(int? Id)
    {
      if (Id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterEmployeeDeductions model = db.MasterEmployeeDeductions.Find(Id);
      if (model == null)
      {
        return HttpNotFound();
      }
      @ViewBag.Id = model.EmpDeductionID;
      DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(model.Empl_Code);
      ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
      ViewBag.CId = model.Empl_Code;
      var dedfrequency = from x in dedFrequency()
                         select new { Id = x.Value, Name = x.Key };
      ViewBag.ded_apply = new SelectList(dedfrequency.OrderBy(x => x.Name), "Id", "Name", model.ded_apply);
      ViewBag.ded_code = new SelectList(db.MasterDedcodes.OrderBy(x => x.description), "ded_code", "description", model.Ded_Code);
      return View(model);
    }

    [HttpPost]
    public ActionResult EditDeductionDetails(MasterEmployeeDeductions model)
    {
      int id = model.EmpDeductionID;
      int userid = AppUserManager.GetUserId();
      ViewBag.PensionerID = model.Empl_Code;
      ViewBag.ded_code = new SelectList(db.MasterDedcodes.OrderBy(x => x.description), "ded_code", "description", model.Ded_Code);
      ViewBag.CId = model.Empl_Code;
      if (ModelState.IsValid && db.MasterEmployeeDeductions.Where(x => x.Empl_Code == model.Empl_Code && x.EmpDeductionID == id).Any())
      {

        #region "Employee Deduction"

        MasterEmployeeDeductions objMasterEmployeeDeductions = db.MasterEmployeeDeductions.Find(model.EmpDeductionID);
        DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions;
        DVOMasterEmployeeDeductions objPreDVOMasterEmployeeDeductions;
        //make object to pass as parameter of search function
        DVOPRDeductionCodesMasterDedcodes objDVOPRDeductionCodesMasterDedcodes = new DVOPRDeductionCodesMasterDedcodes();
        objDVOPRDeductionCodesMasterDedcodes.ded_code = model.Ded_Code;
        //call getDate function of BLL
        List<DVOPRDeductionCodesMasterDedcodes> listDVOPRDeductionCodesMasterDedcodes = BLLPRDeductionCodesMasterDedcodes.GetData(ref objDVOPRDeductionCodesMasterDedcodes);

        //make list of detail-objects of main object
        List<DVOMasterEmployeeDeductions> listDVOMasterEmployeeDeductions = new List<DVOMasterEmployeeDeductions>();
        List<DVOMasterEmployeeDeductions> listPreDVOMasterEmployeeDeductions = new List<DVOMasterEmployeeDeductions>();
        List<DVOMasterEmployeeDeductions> listDeleteDVOMasterEmployeeDeductions = new List<DVOMasterEmployeeDeductions>();
        List<DVOMasterEmployeeDeductions> listNewDVOMasterEmployeeDeductions = new List<DVOMasterEmployeeDeductions>();
        //check datagridview is enable or not if not, no need to add objects into list
        //if (dgvIncome1.Enabled)
        //check datagridview has some items to add or not
        if (listDVOPRDeductionCodesMasterDedcodes.Any())
        {
          //if datagridview has some rows then make new object with values entered in rows
          foreach (var drGrid in listDVOPRDeductionCodesMasterDedcodes)
          {
            if (drGrid.ded_code != null && drGrid.ded_code.Length > 0 && drGrid.ded_code == model.Ded_Code)
            {
              objDVOMasterEmployeeDeductions = new DVOMasterEmployeeDeductions();
              objPreDVOMasterEmployeeDeductions = new DVOMasterEmployeeDeductions();
              //objDVOMasterEmployeeDeductions = new MasterEmployeeDeductions();
              //assign appropriate values to detail-object
              objPreDVOMasterEmployeeDeductions.empl_code = objMasterEmployeeDeductions.Empl_Code;
              objMasterEmployeeDeductions.Empl_Code = model.Empl_Code;
              objDVOMasterEmployeeDeductions.empl_code = objMasterEmployeeDeductions.Empl_Code;

              objPreDVOMasterEmployeeDeductions.ded_code = objMasterEmployeeDeductions.Ded_Code;
              objMasterEmployeeDeductions.Ded_Code = drGrid.ded_code;
              objDVOMasterEmployeeDeductions.ded_code = objMasterEmployeeDeductions.Ded_Code;

              objPreDVOMasterEmployeeDeductions.ded_rate = objMasterEmployeeDeductions.ded_rate;
              objMasterEmployeeDeductions.ded_rate = model.ded_rate;//tOdO//objDVOMasterEmployeeModel.AnnualAmount / 12;
              objDVOMasterEmployeeDeductions.ded_rate = objMasterEmployeeDeductions.ded_rate;

              objPreDVOMasterEmployeeDeductions.ded_limit = objMasterEmployeeDeductions.ded_limit;
              objMasterEmployeeDeductions.ded_limit = model.ded_limit;
              objDVOMasterEmployeeDeductions.ded_limit = objMasterEmployeeDeductions.ded_limit;

              objPreDVOMasterEmployeeDeductions.pay_limit = objMasterEmployeeDeductions.pay_limit;
              objMasterEmployeeDeductions.pay_limit = model.pay_limit;
              objDVOMasterEmployeeDeductions.pay_limit = objMasterEmployeeDeductions.pay_limit;

              objPreDVOMasterEmployeeDeductions.ded_apply = objMasterEmployeeDeductions.ded_apply;
              objMasterEmployeeDeductions.ded_apply = model.ded_apply;
              objDVOMasterEmployeeDeductions.ded_apply = objMasterEmployeeDeductions.ded_apply;

              objPreDVOMasterEmployeeDeductions.ded_date = objMasterEmployeeDeductions.ded_date == null ? string.Empty : objMasterEmployeeDeductions.ded_date.ToString();
              objMasterEmployeeDeductions.ded_date = model.ded_date;
              objDVOMasterEmployeeDeductions.ded_date = objMasterEmployeeDeductions.ded_date == null ? string.Empty : objMasterEmployeeDeductions.ded_date.ToString();
              objMasterEmployeeDeductions.line_no = db.MasterEmployeeDeductions.Where(x => x.Empl_Code == model.Empl_Code && x.EmpDeductionID == id).FirstOrDefault().line_no;

              objPreDVOMasterEmployeeDeductions.line_no = (int)objMasterEmployeeDeductions.line_no;
              if (drGrid.dflt_acct != null)
                objMasterEmployeeDeductions.acct_no = Convert.ToInt32(drGrid.dflt_acct);

              objDVOMasterEmployeeDeductions.acct_no = (int)objMasterEmployeeDeductions.acct_no;
              objPreDVOMasterEmployeeDeductions.acct_no = (int)objMasterEmployeeDeductions.acct_no;
              //objDVOMasterEmployeeIncomes.acct_no_kv = "000";
              //if (drGrid.dfltaccounttype != null)
              //objDVOMasterEmployeeIncomes.acct_no_type = drGrid.dfltaccounttype;

              objMasterEmployeeDeductions.department = "000";
              objPreDVOMasterEmployeeDeductions.department = "000";

              objDVOMasterEmployeeDeductions.department = objMasterEmployeeDeductions.department;
              objDVOMasterEmployeeDeductions.InsertMachineInfo = System.Environment.MachineName;
              objDVOMasterEmployeeDeductions.InsertBy = userid;
              objDVOMasterEmployeeDeductions.InsertDate = System.DateTime.Now.ToShortDateString();


              objDVOMasterEmployeeDeductions.UpdateMachineInfo = System.Environment.MachineName;
              objDVOMasterEmployeeDeductions.UpdateBy = userid;
              objDVOMasterEmployeeDeductions.UpdateDate = System.DateTime.Now.ToShortDateString();

              objPreDVOMasterEmployeeDeductions.department = objMasterEmployeeDeductions.department;
              objPreDVOMasterEmployeeDeductions.InsertMachineInfo = System.Environment.MachineName;
              objPreDVOMasterEmployeeDeductions.InsertBy = userid;
              objPreDVOMasterEmployeeDeductions.InsertDate = System.DateTime.Now.ToShortDateString();


              objPreDVOMasterEmployeeDeductions.UpdateMachineInfo = System.Environment.MachineName;
              objPreDVOMasterEmployeeDeductions.UpdateBy = userid;
              objPreDVOMasterEmployeeDeductions.UpdateDate = System.DateTime.Now.ToShortDateString();

              //add into list
              listDeleteDVOMasterEmployeeDeductions.Add(objPreDVOMasterEmployeeDeductions);
              listDVOMasterEmployeeDeductions.Add(objDVOMasterEmployeeDeductions);
              listPreDVOMasterEmployeeDeductions.Add(objPreDVOMasterEmployeeDeductions);
              db.Entry(objMasterEmployeeDeductions).State = EntityState.Modified;
              //db.SaveChanges();
            }
          }
          //Save List
          try
          {
            //call insert function of BLL
            int i = BLLMasterEmployee.UpdateEmployeeDeductions(ref TransactionObject, ref listDVOMasterEmployeeDeductions, "",
              ref listPreDVOMasterEmployeeDeductions, ref listDeleteDVOMasterEmployeeDeductions, ref listNewDVOMasterEmployeeDeductions);
            listDVOMasterEmployeeDeductions = null;
            listPreDVOMasterEmployeeDeductions = null;
            listDeleteDVOMasterEmployeeDeductions = null;
            listNewDVOMasterEmployeeDeductions = null;
          }
          catch (Exception ex)
          {
            //Success = false;
            ErrorMessage = ex.Message;
          }

        }

        #endregion "Employee Deduction"
      }
      var dedfrequency = from x in dedFrequency()
                         select new { Id = x.Value, Name = x.Key };
      ViewBag.ded_apply = new SelectList(dedfrequency.OrderBy(x => x.Name), "Id", "Name", model.ded_apply);
      return RedirectToAction("DeductionIndex", new { Id = model.Empl_Code });
    }

    [HttpGet]
    public ActionResult AddIncomeDetails(int? Id)
    {
      @ViewBag.CId = Id;
      DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(Id.ToString());
      ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);

      ViewBag.inc_code = new SelectList(db.MasterIncCodes.OrderBy(x => x.description), "inc_code", "description");
      return View();
    }
    [HttpPost]
    public ActionResult AddIncomeDetails(MasterEmployeeIncomes model)
    {
      int id = model.EmpIncomeID;
      ViewBag.PensionerID = model.Empl_Code;
      ViewBag.inc_code = new SelectList(db.MasterIncCodes.OrderBy(x => x.description), "inc_code", "description", model.inc_code);
      if (model.acct_no == null || model.hi_inc_amt == null || model.inc_code == null || model.inc_rate == null)
      {
        int count = db.MasterEmployeeIncomes.Where(x => x.Empl_Code == model.Empl_Code && x.inc_code == model.inc_code).Count();
        if (count == 0)
        {
          if (ModelState.IsValid)
          {

            #region "Employee Income"
            MasterEmployeeIncomes objDVOMasterEmployeeIncomes;
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
                if (drGrid.inc_code != null && drGrid.inc_code.Length > 0)
                {
                  objDVOMasterEmployeeIncomes = new MasterEmployeeIncomes();
                  //assign appropriate values to detail-object
                  objDVOMasterEmployeeIncomes.Empl_Code = model.Empl_Code;
                  objDVOMasterEmployeeIncomes.inc_code = drGrid.inc_code;
                  objDVOMasterEmployeeIncomes.inc_rate = model.inc_rate;//tOdO//objDVOMasterEmployeeModel.AnnualAmount / 12;
                  objDVOMasterEmployeeIncomes.inc_number = Convert.ToDecimal(1);
                  objDVOMasterEmployeeIncomes.inc_hours = Convert.ToDecimal(1);
                  objDVOMasterEmployeeIncomes.line_no = db.MasterEmployeeIncomes.Count() + 1;
                  if (drGrid.acct_no != null)
                    objDVOMasterEmployeeIncomes.acct_no = Convert.ToInt32(drGrid.acct_no);
                  //objDVOMasterEmployeeIncomes.acct_no_kv = "000";
                  //if (drGrid.dfltaccounttype != null)
                  //objDVOMasterEmployeeIncomes.acct_no_type = drGrid.dfltaccounttype;

                  objDVOMasterEmployeeIncomes.department = "000";
                  //objDVOMasterEmployeeIncomes.InsertMachineInfo = System.Environment.MachineName;
                  //objDVOMasterEmployeeIncomes.InsertBy = TempData["UserId"] == null ? 0 : Convert.ToInt32(TempData["UserId"]);
                  //objDVOMasterEmployeeIncomes.InsertDate = System.DateTime.Now.ToShortDateString();

                  //add into list
                  db.Entry(objDVOMasterEmployeeIncomes).State = EntityState.Added;
                  db.SaveChanges();
                }
              }
            }


            #endregion "Employee Income"

            //model.IsActive = true;
            //db.Entry(model).State = EntityState.Added;
            //db.SaveChanges();
            return RedirectToAction("IncomeIndex", new { Id = model.Empl_Code });
          }
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
      @ViewBag.CId = model.Empl_Code;
      return View();
    }

    [HttpGet]
    public ActionResult EditIncomeDetails(string Id)
    {

      int userid = AppUserManager.GetUserId();
      var userRole = db.UserRole.FirstOrDefault(x => x.UserId == userid);
      bool AdminOrSupperAminPerMition = userRole != null && db.Roles.Any(x => x.Id == userRole.RoleId && (x.Name == "Admin" || x.Name == "Director Finance (DFSW)"));
      ViewBag.AdminOrSupperAminPerMition = AdminOrSupperAminPerMition;
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();

      List<SessionViewModel> results = (List<SessionViewModel>)HttpContext.Session["List"];
      var models = results.Where(x => x.ControllerName == "MasterPensioner" && x.ActionName == "Edit").FirstOrDefault();
      if (models == null)
      {
        models = results.Where(x => x.ControllerName == "MasterPensioner").FirstOrDefault();
      }

      if (models != null)
      {
        ViewBag.AddPermission = models.AddPermssion;
        ViewBag.EditPermission = models.EditPermission;
        ViewBag.ViewPermission = models.ViewPermission;
      }


      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(Id));
      int id = Convert.ToInt32(IdUrl);
      if (Id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterEmployeeIncomes model = db.MasterEmployeeIncomes.Find(id);
      if (model == null)
      {
        return HttpNotFound();
      }
      @ViewBag.Id = Id;// id;//  Id;
      DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(model.Empl_Code);
      ViewBag.Email = objDVOMasterEmployee.EMail;
      ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
      ViewBag.ApplicationReferenceNo = objDVOMasterEmployee.ApplicationReferenceNo;
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
      ViewBag.CId = UrlEncryption.EncryptURL(Convert.ToString(model.Empl_Code));
      ViewBag.inc_code = new SelectList(db.MasterIncCodes.OrderBy(x => x.description), "inc_code", "description", model.inc_code);
      //int userid = AppUserManager.GetUserId();
      ViewBag.roleidlist = db.UserRole.Where(x => x.UserId == userid).FirstOrDefault().RoleId;
      model.hi_inc_amt = model.inc_number * model.inc_rate;
      model.ReasonForChange = TrimStart(model.ReasonForChange, "<br />");
      int RoleId = _DbContext.UserRole.FirstOrDefault(x => x.UserId == userid).RoleId;
      ViewBag.permitionlist = db.EditApplicantFields.Where(p => p.RoleID == RoleId).ToList();
      return View(model);
    }

    [HttpPost]
    public ActionResult EditIncomeDetails(MasterEmployeeIncomeVM model)
    {
      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();

      List<SessionViewModel> results = (List<SessionViewModel>)HttpContext.Session["List"];
      var models = results.Where(x => x.ControllerName == "MasterPensioner" && x.ActionName == "Edit").FirstOrDefault();
      if (models == null)
      {
        models = results.Where(x => x.ControllerName == "MasterPensioner").FirstOrDefault();
      }

      if (models != null)
      {
        ViewBag.AddPermission = models.AddPermssion;
        ViewBag.EditPermission = models.EditPermission;
        ViewBag.ViewPermission = models.ViewPermission;
      }
      string id = UrlEncryption.Decrypt(model.EmpIncomeID);
      int Ids = Convert.ToInt32(id);
      DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(model.Empl_Code);
      ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
      ViewBag.inc_code = new SelectList(db.MasterIncCodes.OrderBy(x => x.description), "inc_code", "description", model.inc_code);
      ViewBag.CId = model.Empl_Code;
      if (model.acct_no == null || model.hi_inc_amt == null || model.inc_code == null || model.inc_rate == null)
      {
        TempData["error"] = "Please Fill all the Details Before Submitting Page";
      }
      else
      {
        if (!db.MasterEmployeeIncomes.Where(x => x.Empl_Code == model.Empl_Code && x.inc_code == model.inc_code && x.EmpIncomeID != Ids).Any() && ModelState.IsValid)
        {
          #region "Employee Income"
          MasterEmployeeIncomes objDVOMasterEmployeeIncomes = db.MasterEmployeeIncomes.Find(Ids);
          //make object to pass as parameter of search function
          DVOUpdateIncCode objDVOUpdateIncCode = new DVOUpdateIncCode();
          objDVOUpdateIncCode.inc_code = model.inc_code;
          //call getDate function of BLL
          List<DVOUpdateIncCode> listDVOUpdateIncCode = BLLUpdateIncomeCodes.GetDetailedInfo(ref objDVOUpdateIncCode);

          //make list of detail-objects of main object
          List<DVOMasterEmployeeIncomes> listDVOMasterEmployeeIncomes = new List<DVOMasterEmployeeIncomes>();
          //check datagridview is enable or not if not, no need to add objects into list
          //if (dgvIncome1.Enabled)
          ////check datagridview has some items to add or not
          #region       //This code is working previously
          //if (listDVOUpdateIncCode.Any())
          //{
          //  //if datagridview has some rows then make new object with values entered in rows
          //  foreach (var drGrid in listDVOUpdateIncCode)
          //  {
          //    if (drGrid.inc_code != null && drGrid.inc_code.Length > 0 && model.inc_code == drGrid.inc_code)
          //    {
          //      //objDVOMasterEmployeeIncomes = new MasterEmployeeIncomes();
          //      //assign appropriate values to detail-object
          //      //objDVOMasterEmployeeIncomes.EmpIncomeID = model.EmpIncomeID;
          //      objDVOMasterEmployeeIncomes.Empl_Code = model.Empl_Code;
          //      objDVOMasterEmployeeIncomes.inc_code = drGrid.inc_code;
          //      objDVOMasterEmployeeIncomes.inc_rate = model.inc_rate;//tOdO//objDVOMasterEmployeeModel.AnnualAmount / 12;
          //      objDVOMasterEmployeeIncomes.hi_inc_amt = model.hi_inc_amt;
          //      objDVOMasterEmployeeIncomes.inc_number = Convert.ToDecimal(1);
          //      objDVOMasterEmployeeIncomes.inc_hours = Convert.ToDecimal(1);
          //      objDVOMasterEmployeeIncomes.line_no = db.MasterEmployeeIncomes.Where(x => x.Empl_Code == model.Empl_Code && x.EmpIncomeID == Ids).FirstOrDefault().line_no;
          //      if (drGrid.acct_no != null)
          //        objDVOMasterEmployeeIncomes.acct_no = db.MasterEmployeeIncomes.Where(x => x.Empl_Code == model.Empl_Code && x.EmpIncomeID == Ids).FirstOrDefault().acct_no;
          //      //objDVOMasterEmployeeIncomes.acct_no_kv = "000";
          //      //if (drGrid.dfltaccounttype != null)
          //      //objDVOMasterEmployeeIncomes.acct_no_type = drGrid.dfltaccounttype;

          //      objDVOMasterEmployeeIncomes.department = "000";
          //      //objDVOMasterEmployeeIncomes.InsertMachineInfo = System.Environment.MachineName;
          //      //objDVOMasterEmployeeIncomes.InsertBy = TempData["UserId"] == null ? 0 : Convert.ToInt32(TempData["UserId"]);
          //      //objDVOMasterEmployeeIncomes.InsertDate = System.DateTime.Now.ToShortDateString();

          //      //add into list
          //      db.Entry(objDVOMasterEmployeeIncomes).State = EntityState.Modified;
          //      db.SaveChanges();
          //      //DbContextHelper.dBsavechanges(db);

          //    }
          //  }
          //}

          #endregion
          List<AuditLogs> listDVOEmpTypLogEmpsaltyplog = new List<AuditLogs>();

          if (listDVOUpdateIncCode.Any())
          {
            // Fetch required data before the loop
            var existingIncome = db.MasterEmployeeIncomes
                .Where(x => x.Empl_Code == model.Empl_Code && x.EmpIncomeID == Ids)
                .FirstOrDefault();


            // Ensure that existing data is not null
            if (existingIncome != null)
            {
              if (model.inc_rate != null)
                if (existingIncome.inc_rate != model.inc_rate)
                {
                  AuditLogs objtmp = new AuditLogs
                  {
                    CreatedBy = AppUserManager.GetUserId(),
                    CreatedOn = DateTime.Now,
                    ModifiedBy = AppUserManager.GetUserId(),
                    ModifiedOn = DateTime.Now,
                    IsActive = true,
                    EventType = "Update",
                    TableName = "MasterEmployeeIncomes",
                    ColumnName = "inc_rate",
                    OldValue = existingIncome.inc_rate != null ? Convert.ToDecimal(existingIncome.inc_rate).ToString("0.00") : "",
                    NewValue = model.inc_rate != null ? Convert.ToDecimal(model.inc_rate).ToString("0.00") : "",
                    RecordId = Convert.ToInt32(existingIncome.Empl_Code),
                  };
                  listDVOEmpTypLogEmpsaltyplog.Add(objtmp);
                }
              //if (model.hi_inc_amt != null)
              //  if (existingIncome.hi_inc_amt != model.hi_inc_amt)
              //  {
              //    AuditLogs objtmp = new AuditLogs
              //    {
              //      CreatedBy = AppUserManager.GetUserId(),
              //      CreatedOn = DateTime.Now,
              //      ModifiedBy = AppUserManager.GetUserId(),
              //      ModifiedOn = DateTime.Now,
              //      IsActive = true,
              //      EventType = "Update",
              //      TableName = "MasterEmployeeIncomes",
              //      ColumnName = "hi_inc_amt",
              //      OldValue = existingIncome.hi_inc_amt != null ? Convert.ToDecimal(existingIncome.hi_inc_amt).ToString("0.00") : "",
              //      NewValue = model.hi_inc_amt != null ? Convert.ToDecimal(model.hi_inc_amt).ToString("0.00") : "",
              //      RecordId = Convert.ToInt32(existingIncome.Empl_Code),
              //    };
              //    listDVOEmpTypLogEmpsaltyplog.Add(objtmp);
              //  }
              //remarks added
              if (model.ReasonForChange != null)
                if (existingIncome.ReasonForChange != model.ReasonForChange)
                {
                  AuditLogs objtmp = new AuditLogs
                  {
                    CreatedBy = AppUserManager.GetUserId(),
                    CreatedOn = DateTime.Now,
                    ModifiedBy = AppUserManager.GetUserId(),
                    ModifiedOn = DateTime.Now,
                    IsActive = true,
                    EventType = "Update",
                    TableName = "MasterEmployeeIncomes",
                    ColumnName = "Remarks",
                    OldValue = existingIncome.ReasonForChange != null ? existingIncome.ReasonForChange : "",
                    NewValue = model.ReasonForChange != null ? "• [" + DateTime.Now.ToString("dd/MM/yyyy") + "] " + model.ReasonForChange : "",
                    RecordId = Convert.ToInt32(existingIncome.Empl_Code),
                  };
                  listDVOEmpTypLogEmpsaltyplog.Add(objtmp);
                }
              // Use a transaction for atomicity (optional)
              using (var transaction = db.Database.BeginTransaction())
              {
                try
                {
                  foreach (var drGrid in listDVOUpdateIncCode)
                  {
                    if (drGrid.inc_code != null && drGrid.inc_code.Length > 0 && model.inc_code == drGrid.inc_code)
                    {
                      // Update properties of existing entity
                      existingIncome.Empl_Code = model.Empl_Code;
                      existingIncome.inc_code = drGrid.inc_code;
                      existingIncome.inc_rate = model.inc_rate;
                      existingIncome.hi_inc_amt = model.inc_rate * Convert.ToDecimal(1);
                      existingIncome.inc_number = Convert.ToDecimal(1);
                      existingIncome.inc_hours = Convert.ToDecimal(1);
                      existingIncome.line_no = existingIncome.line_no; // You might want to set this property differently
                      existingIncome.ReasonForChange = model.ReasonForChange;
                      if (drGrid.acct_no != null)
                        existingIncome.acct_no = existingIncome.acct_no; // Set accordingly

                      existingIncome.department = "000";

                      existingIncome.ReasonForChange = " [" + AppUserManager.GetUserName() + "] : " + existingIncome.ReasonForChange;

                      // Generate raw SQL update query
                      string updateQuery = @"
                            UPDATE MasterEmployeeIncomes
                            SET Empl_Code = @Empl_Code,
                                inc_code = @inc_code,
                                inc_rate = @inc_rate,
                                hi_inc_amt = @hi_inc_amt,
                                inc_number = @inc_number,
                                inc_hours = @inc_hours,
                                line_no = @line_no,
                                acct_no = @acct_no,
                                department = @department,
                                ReasonForChange= CONCAT(ReasonForChange, (CASE WHEN ReasonForChange = NULL OR ReasonForChange = '' THEN '' ELSE '<br />' END), NCHAR(8226), ' [', FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm'), '] ', @ReasonForChange)
                            WHERE EmpIncomeID = @EmpIncomeID";

                      // Execute the raw SQL query
                      db.Database.ExecuteSqlCommand(updateQuery,
                          new SqlParameter("@Empl_Code", existingIncome.Empl_Code),
                          new SqlParameter("@inc_code", existingIncome.inc_code),
                          new SqlParameter("@inc_rate", existingIncome.inc_rate),
                          new SqlParameter("@hi_inc_amt", existingIncome.hi_inc_amt),
                          new SqlParameter("@inc_number", existingIncome.inc_number),
                          new SqlParameter("@inc_hours", existingIncome.inc_hours),
                          new SqlParameter("@line_no", existingIncome.line_no),
                          new SqlParameter("@acct_no", existingIncome.acct_no),
                          new SqlParameter("@department", existingIncome.department),
                          new SqlParameter("@ReasonForChange", existingIncome.ReasonForChange),
                          new SqlParameter("@EmpIncomeID", existingIncome.EmpIncomeID));

                      // You can add more parameters based on the properties you want to update...
                    }
                  }

                  // Commit the transaction
                  transaction.Commit();
                }
                catch (Exception)
                {
                  // Handle exceptions and roll back the transaction if needed
                  transaction.Rollback();
                  throw;
                }
              }

            }
          }
          if (listDVOEmpTypLogEmpsaltyplog.Any())
          {
            _DbContext.AuditLogs.AddRange(listDVOEmpTypLogEmpsaltyplog);
            _DbContext.SaveChanges();
          }

          #endregion "Employee Income"

          return RedirectToAction("IncomeIndex", new { Id = UrlEncryption.EncryptURL(Convert.ToString(model.Empl_Code)) });
        }
        else
        {
          TempData["error"] = "This Income Code Already Exist";
        }
      }

      // Add without Rate Update 

      var mstdata = db.MasterEmployeeIncomes.Find(Ids);
      model.ReasonForChange = TrimStart(mstdata.ReasonForChange, "<br />");

      return View(model);
    }
    [HttpGet]
    public ActionResult AddObligationDetails(int? Id)
    {
      @ViewBag.Id = Id;
      DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(Id.ToString());
      ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
      ViewBag.ObligationCode = new SelectList(db.MasterOblCodes.OrderBy(x => x.description), "Obl_code", "description");
      return View();
    }
    [HttpPost]
    public ActionResult AddObligationDetails(MasterEmployeeObligations model)
    {
      int id = model.EmpOblId;
      if (ModelState.IsValid)
      {
        //model.IsActive = true;
        db.Entry(model).State = EntityState.Added;
        db.SaveChanges();
        return RedirectToAction("Obligation", new { Id = model.Empl_Code });
      }
      ViewBag.PensionerID = model.Empl_Code;
      ViewBag.ObligationCode = new SelectList(db.MasterOblCodes.OrderBy(x => x.description), "Obl_code", "description");
      return View();
    }
    //used in obligation
    public JsonResult obligationDropDownBind(int? Value)
    {
      var result = db.MasterOblCodes.Where(x => x.Obl_Code_ID == Value).FirstOrDefault();
      return Json(result, JsonRequestBehavior.AllowGet);
    }
    public JsonResult IncomeDropDownAjax(string Value, string incomeCode)
    {
      int count = db.MasterEmployeeIncomes.Where(x => x.Empl_Code == Value && x.inc_code == incomeCode).Count();
      if (count != 0)
      {
        return Json("Error", JsonRequestBehavior.AllowGet);
      }
      DVOMasterEmployee tempmodel = SearchAgainEmployeeInfo(Value);
      decimal rate = tempmodel.AnnualSalary / 12;
      return Json(rate, JsonRequestBehavior.AllowGet);
    }
    public JsonResult BankDetailsDropDownAjax(string emplcode, string bankcode)
    {
      int count = db.MasterEmpBankDetails.Where(x => x.empl_code == emplcode && x.bank_code == bankcode).Count();
      if (count != 0)
      {
        return Json("Error", JsonRequestBehavior.AllowGet);
      }
      return Json("", JsonRequestBehavior.AllowGet);
    }

    public JsonResult BankDetailsAjax(string emplcode, string bankcode, string amount)
    {
      decimal val = string.IsNullOrWhiteSpace(amount) ? 0 : Convert.ToDecimal(amount);
      decimal? totalamount = db.MasterEmpBankDetails.Where(x => x.empl_code == emplcode).AsEnumerable().Sum(o => o.amount) + val;
      if (totalamount != null && totalamount > 100)
      {
        return Json("Error", JsonRequestBehavior.AllowGet);
      }
      return Json("", JsonRequestBehavior.AllowGet);
    }

    public JsonResult DeductionDropDownBindAjax(int? Value)
    {
      var result = db.MasterDedcodes.Where(x => x.ded_code_id == Value).FirstOrDefault();
      return Json(result, JsonRequestBehavior.AllowGet);
    }
    //public ActionResult PensionApplicationApprovalIndex()
    //{

    //  int user = AppUserManager.GetUserId();
    //  string approved = "A";
    //  List<PensionApplications> model;
    //  List<PensionApplications> temp = new List<PensionApplications>();
    //  if (db.ApprovalProcessAssignedUser.Where(x => x.ApprovalUser == user).Any())
    //  {
    //    model = db.PensionApplications.ToList();
    //    int maxLevel = (from a in db.ApprovalProcessLevel join b in db.ApprovalProcessAssignedUser on a.ApprovalProcessLevelId equals b.ApprovalProcessLevelId where a.ModuleId == 16 orderby b.ApprovalProcessId descending select b.ApprovalProcessId).FirstOrDefault();
    //    foreach (var item in model)
    //    {
    //      var status = (from a in db.ApplicationApprovalStatus where a.ApplicationId == item.Id && a.ApprovalProcessId<=maxLevel orderby a.ApprovalProcessId descending select a == null ? 0 : a.ApprovalProcessId).FirstOrDefault();

    //        if (status != 0)
    //        {
    //          approved = (from a in db.ApplicationApprovalStatus where a.ApplicationId == item.Id && a.ApprovalProcessId == status orderby a.ApprovalProcessId descending select a.ApprovalStatus).FirstOrDefault();
    //        }
    //        var pstatus = db.ApprovalProcessAssignedUser.Where(x => x.ApprovalProcessId > status).Select(x => new { x.ApprovalUser,x.ApprovalProcessId }).FirstOrDefault();
    //        if (user == pstatus.ApprovalUser && approved != "R" && pstatus.ApprovalProcessId<=maxLevel)
    //        {              
    //          temp.Add(item);
    //        }


    //    }
    //  }
    //  return View(temp);
    //  //int user = AppUserManager.GetUserId();
    //  //List<PensionApplications> objPensionApplications = new List<PensionApplications>();
    //  //List<PensionApplications> objPensionApplicationsApproved = new List<PensionApplications>();
    //  //if (db.ApprovalProcessAssignedUser.Where(x => x.ApprovalUser == user).Any())
    //  //{
    //  //  var approvalLevelUserDetails = (from APL in db.ApprovalProcessLevel
    //  //                                  join APAU in db.ApprovalProcessAssignedUser on APL.ApprovalProcessLevelId equals APAU.ApprovalProcessLevelId
    //  //                                  //join AAS in db.ApplicationApprovalStatus on APAU.ApprovalProcessId equals AAS.ApprovalProcessId into AASdef
    //  //                                  //           from AAS in AASdef.DefaultIfEmpty()
    //  //                                  where APAU.ApprovalUser == user && APL.ModuleId == 81 && APL.IsActive == true && APAU.IsActive == true
    //  //                                  select new
    //  //                                  {
    //  //                                    APL.ApprovalProcessLevelId,
    //  //                                    APL.ApprovalProcessLevelName,
    //  //                                    APL.ApprovalLevel,
    //  //                                    APAU.ApprovalProcessId,
    //  //                                    APAU.ApprovalUser,
    //  //                                  }).ToList();

    //  //  string ApprovalLevelAssigned = (string.Join(",", approvalLevelUserDetails.Select(x => x.ApprovalLevel).ToArray()));
    //  //  ViewBag.ApprovalLevelAssigned = ApprovalLevelAssigned;

    //  //  var maxminResult = db.ApprovalProcessLevel.Where(x => x.ModuleId == 81 && x.IsActive == true).GroupBy(r => r.ModuleId)
    //  //              .Select(grp => new
    //  //              {
    //  //                ID = grp.Key,
    //  //                Min = grp.Min(t => t.ApprovalLevel),
    //  //                Max = grp.Max(t => t.ApprovalLevel)
    //  //              }).ToList();

    //  //  int level = approvalLevelUserDetails.OrderByDescending(a => a.ApprovalLevel).FirstOrDefault().ApprovalLevel;
    //  //  if ((from x1 in approvalLevelUserDetails where maxminResult.Any(y => y.Min == x1.ApprovalLevel) select x1).Any())
    //  //  {
    //  //    objPensionApplicationsApproved = (from c in db.PensionApplications
    //  //                                      where (from o in db.ApplicationApprovalStatus
    //  //                                             where o.ApprovalStatus == "A" && o.ApprovalLevel < level
    //  //                                             select o.ApplicationId)
    //  //                                      .Contains(c.Id)
    //  //                                      select c).ToList();

    //  //    objPensionApplications = (from c in db.PensionApplications
    //  //                              where !(from o in db.ApplicationApprovalStatus
    //  //                                      where o.ApprovalStatus == "A"
    //  //                                      select o.ApplicationId)
    //  //                              .Contains(c.Id)
    //  //                              select c).ToList();
    //  //    objPensionApplications = objPensionApplications.Union(objPensionApplicationsApproved).ToList();
    //  //  }
    //  //  else
    //  //  {
    //  //    objPensionApplicationsApproved = (from c in db.PensionApplications
    //  //                                      where (from o in db.ApplicationApprovalStatus
    //  //                                             where o.ApprovalStatus == "A" && o.ApprovalLevel < level
    //  //                                             select o.ApplicationId)
    //  //                                      .Contains(c.Id)
    //  //                                      select c).ToList();
    //  //    //model = (from c in db.PensionApplications
    //  //    //         where !(from o in db.ApplicationApprovalStatus
    //  //    //                 where o.ApprovalStatus == "A"
    //  //    //                 select o.ApplicationId)
    //  //    //         .Contains(c.Id)
    //  //    //         select c).ToList();
    //  //    objPensionApplications.Concat(objPensionApplicationsApproved);
    //  //  }

    //  //  var model1 = (from x1 in approvalLevelUserDetails where maxminResult.Any(y => y.Min == x1.ApprovalLevel) select x1).Any();

    //  //}
    //  //else
    //  //{
    //  //  objPensionApplications = null;
    //  //}
    //  //return View(objPensionApplications);
    //}
    //public ActionResult PensionApplicationApprovalIndex()
    //{
    //  int user = AppUserManager.GetUserId();
    //  string approved = "A";
    //  List<PensionApplications> model;
    //  List<PensionApplications> temp = new List<PensionApplications>();
    //  if (db.ApprovalProcessAssignedUser.Where(x => x.ApprovalUser == user).Any())
    //  {
    //    model = db.PensionApplications.ToList();
    //    int maxLevel = (from a in db.ApprovalProcessLevel join b in db.ApprovalProcessAssignedUser on a.ApprovalProcessLevelId equals b.ApprovalProcessLevelId where a.ModuleId == 16 orderby b.ApprovalProcessId descending select b.ApprovalProcessId).FirstOrDefault();
    //    foreach (var item in model)
    //    {
    //      var status = (from a in db.ApplicationApprovalStatus where a.ApplicationId == item.Id && a.ApprovalProcessId <= maxLevel orderby a.ApprovalProcessId descending select a == null ? 0 : a.ApprovalProcessId).FirstOrDefault();

    //      if (status != 0)
    //      {
    //        approved = (from a in db.ApplicationApprovalStatus where a.ApplicationId == item.Id && a.ApprovalProcessId == status orderby a.ApprovalProcessId descending select a.ApprovalStatus).FirstOrDefault();
    //      }
    //      var pstatus = db.ApprovalProcessAssignedUser.Where(x => x.ApprovalProcessId > status).Select(x => new { x.ApprovalUser, x.ApprovalProcessId }).FirstOrDefault();
    //      if (user == pstatus.ApprovalUser && approved != "R" && pstatus.ApprovalProcessId <= maxLevel)
    //      {
    //        temp.Add(item);
    //      }


    //    }
    //  }
    //  return View(temp);

    //}
    private List<PensionApplications> authorizedPensionApplication()
    {
      int user = AppUserManager.GetUserId();
      List<PensionApplications> model;
      List<PensionApplications> temp = new List<PensionApplications>();
      var levels = (from level in db.ApprovalProcessLevel join users in db.ApprovalProcessAssignedUser on level.ApprovalProcessLevelId equals users.ApprovalProcessLevelId where level.ModuleId == 16 select users).ToList();
      if (levels.Where(x => x.ApprovalUser == user).Any())
      {
        model = db.PensionApplications.ToList();
        int MaxLevel = levels.LastOrDefault().ApprovalProcessId;
        int MinLevel = levels.FirstOrDefault().ApprovalProcessId;
        foreach (var item in model)
        {
          string approved = "A";
          int lastApprovedLevel = (from a in db.ApplicationApprovalStatus where a.ApplicationId == item.Id && a.ApprovalProcessId <= MaxLevel && a.ApprovalProcessId >= MinLevel orderby a.ApprovalProcessId descending select a == null ? 0 : a.ApprovalProcessId).FirstOrDefault();
          if (lastApprovedLevel != 0)
          {
            approved = (from a in db.ApplicationApprovalStatus where a.ApplicationId == item.Id && a.ApprovalProcessId == lastApprovedLevel orderby a.ApprovalProcessId descending select a.ApprovalStatus).FirstOrDefault();
          }
          if (lastApprovedLevel != MaxLevel)
          {
            var pstatus = levels.Where(x => x.ApprovalProcessId > lastApprovedLevel).Select(x => new { x.ApprovalUser, x.ApprovalProcessId }).FirstOrDefault();
            if (user == pstatus.ApprovalUser && approved != "R" && pstatus.ApprovalProcessId <= MaxLevel)
            {
              temp.Add(item);
            }
          }
        }
      }
      return temp;
    }
    public ActionResult PensionApplicationApprovalIndex()
    {
      List<PensionApplications> list = authorizedPensionApplication();
      return View(list);

    }

    public ActionResult _RejectedIndex()
    {
      return View();
    }
    public ActionResult RejectedAjax(JQueryDataTableParamModel param)
    {
      var result = (from users in db.ApprovalProcessAssignedUser join level in db.ApprovalProcessLevel on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId where level.ModuleId == 16 select new { users.ApprovalProcessId, users.ApprovalUser }).ToList();
      int minId = result.FirstOrDefault().ApprovalProcessId;
      int maxId = result.LastOrDefault().ApprovalProcessId;
      int user = AppUserManager.GetUserId();
      List<App.Data.ViewModels.RejectedViewModel> List = new List<App.Data.ViewModels.RejectedViewModel>();
      if (result.Where(x => x.ApprovalUser.ToString() == user.ToString()).Any())
      {
        List = (from users in db.ApprovalProcessAssignedUser
                join level in db.ApprovalProcessLevel on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId
                join status in db.ApplicationApprovalStatus on users.ApprovalProcessId equals status == null ? 0 : status.ApprovalProcessId
                join application in db.PensionApplications on status == null ? 0 : status.ApplicationId equals application.Id
                where level.ModuleId == 16 && status.ApprovalProcessId >= minId && status.ApprovalProcessId <= maxId && status.ApprovalStatus == "R"
                select new App.Data.ViewModels.RejectedViewModel { ApplicationId = status.ApplicationId, AppliedDate = application.CreatedOn, ApprovalStatus = status.ApprovalStatus, ApprovalUser = users.ApprovalUser.ToString(), RejectedDate = status.ApprovedDate, Notes = status.Notes, approvalProcessId = status.ApprovalProcessId }).ToList();

      }
      IEnumerable<App.Data.ViewModels.RejectedViewModel> filtered;


      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => (c.contributor.FirstName + " " + c.contributor.MidName + " " + c.contributor.LastName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
             //|| c.contributor.MidName.ToLower().Contains(param.sSearch.ToLower())
             //|| c.contributor.LastName.ToLower().Contains(param.sSearch.ToLower())
             || c.contributor.Employer.EmployerName.ToLower().Contains(param.sSearch.ToLower())
           || c.AppliedDate.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.ApprovalStatus.ToLower().Contains(param.sSearch.ToLower())
           || c.ApprovalUser.ToLower().Contains(param.sSearch.ToLower())
           || c.RejectedDate.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.Notes.ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      //var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      //Func<RejectedViewModel, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name :
      //                                                                                sortColumnIndex == 1 ? c.IsActive + "" :
      //                                                                                "");

      //var sortDirection = Request["sSortDir_0"]; // asc or desc
      //if (sortDirection == "asc")
      //  filtered = filtered.OrderBy(orderingFunction);
      //else
      //  filtered = filtered.OrderByDescending(orderingFunction);

      //Pagging
      var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

      //Select required columns
      var dresult = from c in displayed
                    select new[] {

                         c.contributor.FullName,
                         c.Notes,
                         c.contributor.Employer.EmployerName,
                         String.Format("{0:MM/dd/yyyy}", c.AppliedDate),
                         String.Format("{0:MM/dd/yyyy}", c.RejectedDate),
                         c.user.FullName,
                         c.ApprovalStatus,
                         c.ApplicationId + ""
                                  };

      return Json(
                                  new
                                  {
                                    sEcho = param.sEcho,
                                    iTotalRecords = List.Count(),
                                    iTotalDisplayRecords = filtered.Count(),
                                    aaData = dresult
                                  }, JsonRequestBehavior.AllowGet);
    }
    public ActionResult _ApprovedIndex()
    {

      return View();
    }
    public ActionResult ApprovedAjax(JQueryDataTableParamModel param)
    {
      var result = (from users in db.ApprovalProcessAssignedUser join level in db.ApprovalProcessLevel on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId where level.ModuleId == 16 select new { users.ApprovalProcessId, users.ApprovalUser }).ToList();
      int visiblePrint = result.Skip(1).FirstOrDefault().ApprovalProcessId;
      int minId = result.FirstOrDefault().ApprovalProcessId;
      int maxId = result.LastOrDefault().ApprovalProcessId;
      int user = AppUserManager.GetUserId();
      List<App.Data.ViewModels.RejectedViewModel> List = new List<App.Data.ViewModels.RejectedViewModel>();

      if (result.Where(x => x.ApprovalUser.ToString() == user.ToString()).Any())
      {

        List = (from users in db.ApprovalProcessAssignedUser
                join level in db.ApprovalProcessLevel on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId
                join status in db.ApplicationApprovalStatus on users.ApprovalProcessId equals status == null ? 0 : status.ApprovalProcessId
                join application in db.PensionApplications on status == null ? 0 : status.ApplicationId equals application.Id
                where level.ModuleId == 16 && status.ApprovalProcessId >= minId && status.ApprovalProcessId <= maxId
                orderby application.Id descending
                select new App.Data.ViewModels.RejectedViewModel { ApplicationId = status.ApplicationId, AppliedDate = application.CreatedOn, ApprovalStatus = status.ApprovalStatus, ApprovalUser = users.ApprovalUser.ToString(), RejectedDate = status.ApprovedDate, Notes = status.Notes, approvalProcessId = status.ApprovalProcessId }).OrderBy(x => x.approvalProcessId).ToList();
        var rejectedappid = List.Where(x => x.ApprovalStatus == "R").Select(x => x.ApplicationId).ToList();
        if (rejectedappid.Count > 0)
        {
          foreach (var item in rejectedappid)
          {
            List = List.Where(x => x.ApplicationId != item).GroupBy(x => x.ApplicationId).Select(g => g.Last()).ToList();
          }
        }
        else
        {
          List = List.GroupBy(x => x.ApplicationId).Select(g => g.Last()).ToList();
        }

      }
      IEnumerable<App.Data.ViewModels.RejectedViewModel> filtered;


      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => (c.contributor.FirstName + " " + c.contributor.MidName + " " + c.contributor.LastName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
             //|| c.contributor.MidName.ToLower().Contains(param.sSearch.ToLower())
             //|| c.contributor.LastName.ToLower().Contains(param.sSearch.ToLower())
             || c.contributor.Employer.EmployerName.ToLower().Contains(param.sSearch.ToLower())
           || c.AppliedDate.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.ApprovalStatus.ToLower().Contains(param.sSearch.ToLower())
           || c.ApprovalUser.ToLower().Contains(param.sSearch.ToLower())
           || c.RejectedDate.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.Notes.ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      filtered = filtered.OrderBy(x => x.contributor.Employer.Id);
      //Sorting through column index
      //var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      //Func<RejectedViewModel, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name :
      //                                                                                sortColumnIndex == 1 ? c.IsActive + "" :
      //                                                                                "");

      //var sortDirection = Request["sSortDir_0"]; // asc or desc
      //if (sortDirection == "asc")
      //  filtered = filtered.OrderBy(orderingFunction);
      //else
      //  filtered = filtered.OrderByDescending(orderingFunction);

      //Pagging
      var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

      //Select required columns
      var dresult = from c in displayed
                    select new[] {

                         c.contributor.FullName,
                         c.Notes,
                         c.contributor.Employer.EmployerName,
                         String.Format("{0:MM/dd/yyyy}", c.AppliedDate),
                         String.Format("{0:MM/dd/yyyy}", c.RejectedDate),
                         c.user.FullName,
                                              c.ApprovalStatus,
                                              c.ApplicationId + "",
                         c.approvalProcessId>=visiblePrint?"Yes":"No"
                                     };

      return Json(
                                  new
                                  {
                                    sEcho = param.sEcho,
                                    iTotalRecords = List.Count(),
                                    iTotalDisplayRecords = filtered.Count(),
                                    aaData = dresult
                                  }, JsonRequestBehavior.AllowGet);
    }
    public ActionResult PensionApplicationApprovalDetails(int? id)
    {
      List<PensionApplications> list = authorizedPensionApplication();
      if (list.Count > 0)
        id = list.Any(x => x.Id == id) ? id : null;
      else
        id = null;
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      PensionApplications pensionApplications = db.PensionApplications.Find(id);
      if (pensionApplications == null)
      {
        return HttpNotFound();
      }
      ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true), "Id", "EmployerName", pensionApplications.EmployerID);
      var BenefitTypes = from BenefitType e in Enum.GetValues(typeof(BenefitType))
                         select new { Id = (int)e, Name = e.ToString() };
      ViewBag.BenefitTypes = new SelectList(BenefitTypes.OrderBy(x => x.Name), "Id", "Name", pensionApplications.BenefitTypes);
      ViewBag.DesignationName = db.MasterDesignation.Where(x => x.Id == pensionApplications.DesignationId).Select(x => x.Name).FirstOrDefault();
      var personID = db.MasterContributor.Where(x => x.EmployerID == pensionApplications.EmployerID && x.IsActive == true).Select(x => new { Id = x.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName }).ToList();
      ViewBag.PersonID = new SelectList(personID.OrderBy(x => x.Name), "Id", "Name", pensionApplications.PersonID);
      return View(pensionApplications);
    }
    public JsonResult AcceptedAjax(string Result, int Id, string Notes, string dstatus)
    {
      int i = 1;
      string EmpCode = string.Empty;
      var maxminResult = (from users in db.ApprovalProcessAssignedUser
                          join level in db.ApprovalProcessLevel.Where(x => x.ModuleId == 16) on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId
                          join status in db.ApplicationApprovalStatus.Where(x => x.ApplicationId == Id) on users.ApprovalProcessId equals status == null ? 0 : status.ApprovalProcessId into gj
                          from subpet in gj.DefaultIfEmpty()
                          select new { users.ApprovalUser, level = users.ApprovalProcessId, ApprovalProcessId = (subpet == null ? 0 : subpet.ApprovalProcessId) }).ToList(); ;

      int maxLevel = maxminResult.LastOrDefault().level;
      int minLevel = maxminResult.FirstOrDefault().level;

      int ApprovedAtLevelStage = maxminResult.Where(x => x.ApprovalProcessId == 0).Select(x => x.level).FirstOrDefault();
      //var ApprovedAtLevelStage = db.ApprovalProcessAssignedUser.Where(x => x.ApprovalProcessId > status).Select(x => x.ApprovalProcessId).FirstOrDefault();
      var approvalLevelUserDetails = (from APL in db.ApprovalProcessLevel
                                      join APAU in db.ApprovalProcessAssignedUser on APL.ApprovalProcessLevelId equals APAU.ApprovalProcessLevelId
                                      where APAU.ApprovalProcessId == ApprovedAtLevelStage && APL.ModuleId == 16 && APL.IsActive == true && APAU.IsActive == true
                                      select new
                                      {
                                        APL.ApprovalProcessLevelId,
                                        APL.ApprovalProcessLevelName,
                                        APL.ApprovalLevel,
                                        APAU.ApprovalProcessId,
                                        APAU.ApprovalUser,
                                      }).FirstOrDefault();



      ApplicationApprovalStatus model = new ApplicationApprovalStatus();
      int result = 0;
      PensionApplications objPensionApplications = db.PensionApplications.Where(x => x.Id == Id).FirstOrDefault();

      if (approvalLevelUserDetails.ApprovalLevel == maxLevel && objPensionApplications != null && Result == "A")
      {
        //using (var transaction = db.Database.BeginTransaction())
        //{
        //  try
        //  {
        var objDVOMasterEmployeeModel = new MasterPensioner();

        //var ContributorDetails = db.MasterContributor.Where(x => x.Id == Convert.ToInt32(objPensionApplications.PersonID)).FirstOrDefault();
        int personId = Convert.ToInt32(objPensionApplications.PersonID);

        var ContributorDetails = db.MasterContributor.Where(x => x.Id == personId).FirstOrDefault();
        if (dstatus == "D")
        {
          ContributorDetails.JobStatusID = 2;
        }
        else
        {
          ContributorDetails.JobStatusID = 3;
        }
        ContributorDetails.RetirementOrResignationDate = objPensionApplications.RetirementOrResignationDate;
        ContributorDetails.MapTo(objDVOMasterEmployeeModel);

        DVOMasterEmployee objSearchCriteriaDVOMasterEmployeeModel = new DVOMasterEmployee();
        objSearchCriteriaDVOMasterEmployeeModel.PersonID = ContributorDetails.PersonID;
        var checkRecord = SearchEmployeeInformation(objSearchCriteriaDVOMasterEmployeeModel);// db.MasterPensioner.Where(x => x.PersonID == ContributorDetails.PersonID);


        if (!checkRecord.Any())
        {

          App.Data.ViewModels.PensionCalculationViewModel calculatedResult = new App.Data.ViewModels.PensionCalculationViewModel();
          DateTime date = Convert.ToDateTime(ContributorDetails.RetirementOrResignationDate);
          if (ContributorDetails.Employer.EmployerTypeID == (int)PensionType.Police)
            calculatedResult = PensionAndRefundRepo.CalculatePolicePensionDetails(date, ContributorDetails.Id, "No", "No");
          else
            calculatedResult = PensionAndRefundRepo.CalculatePublicPensionDetails(date, ContributorDetails.Id, "No");


          objDVOMasterEmployeeModel.LengthOfQualifyingServiceInMonthsTo31Dec2003 = calculatedResult.LengthOfQualifyingServiceInMonthsTo31Dec2003;
          objDVOMasterEmployeeModel.LengthOfQualifyingServiceInMonthsFrom1Jan2014 = calculatedResult.LengthOfQualifyingServiceInMonthsFrom1Jan2014;
          objDVOMasterEmployeeModel.AnnualAmount = calculatedResult.FullPension;
          int UserId = AppUserManager.GetUserId();
          int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
          var getAllEmployeeDetails_NotTerminated = BLLMasterEmployee.GetAllData(UserId, RoleId).Count();
          objDVOMasterEmployeeModel.PensionerID = String.Format("{0:100000}", (getAllEmployeeDetails_NotTerminated + 1));// "P10000" + (getAllEmployeeDetails_NotTerminated + 1);


          dsSegments = GetSegments();
          string actPayrollDepartmentKeyValue = string.Empty;
          string EmployerName = string.Empty;
          if (ContributorDetails.EmployerID == 2)
            EmployerName = "PUBLIC SERVICE";
          else if (ContributorDetails.EmployerID == 9)
            EmployerName = "POLICE FORCE";
          else
            EmployerName = ContributorDetails.Employer.EmployerName;
          actPayrollDepartmentKeyValue = dsSegments.Tables[0].Select().ToList().Where(row => row["v_segitm_desc"].ToString().ToLower().Contains(EmployerName.ToLower())).Select(x => x["v_keyvalue"].ToString()).FirstOrDefault() + "######";

          #region deathcase
          if (dstatus == "D")
          {
            actPayrollDepartmentKeyValue = dsSegments.Tables[0].Select().ToList().Where(row => row["v_segitm_desc"].ToString().Contains("SURVIVORS")).Select(x => x["v_keyvalue"].ToString()).FirstOrDefault() + "######";
            var dmodel = db.ContributorDependantPensionDetails.Where(x => x.PersonID == objPensionApplications.PersonID);
            using (var context = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
            {
              foreach (var item in dmodel)
              {
                if (item.RelationshipType == "S")
                {
                  var marriageModel = context.MasterContributorMarriageDetails.Where(x => x.Id == item.DependantID).FirstOrDefault();
                  objDVOMasterEmployeeModel.DateOfBirth = marriageModel.DateOfBirth;
                  objDVOMasterEmployeeModel.FirstName = marriageModel.SpouseFirstName;
                  objDVOMasterEmployeeModel.MidName = marriageModel.MidName;
                  objDVOMasterEmployeeModel.LastName = marriageModel.LastName;
                  objDVOMasterEmployeeModel.PensionerTypeId = 2;
                }
                else
                {
                  var dependantModel = context.MasterDependantDetails.Where(x => x.Id == item.DependantID).FirstOrDefault();
                  objDVOMasterEmployeeModel.DateOfBirth = dependantModel.DateOfBirth;
                  objDVOMasterEmployeeModel.FirstName = dependantModel.FirstName;
                  objDVOMasterEmployeeModel.MidName = dependantModel.MidName;
                  objDVOMasterEmployeeModel.LastName = dependantModel.LastName;
                  objDVOMasterEmployeeModel.PensionerTypeId = 3;
                }
                i = InsertEmployeeInformation(objDVOMasterEmployeeModel, out EmpCode, actPayrollDepartmentKeyValue);
              }
            }
          }
          #endregion
          else
          {
            //if (objPensionApplications.BenefitTypes == 1 )Commented and update conditions by Neeraj 11May2018
            if (objPensionApplications.BenefitTypes == 1)
            {
              i = InsertEmployeeInformation(objDVOMasterEmployeeModel, out EmpCode, actPayrollDepartmentKeyValue);
            }
            else if (objPensionApplications.BenefitTypes == 3 && ContributorDetails.JobStatusID == 3)//Added Condtions by Neeraj 11May2018
            {
              i = InsertEmployeeInformation(objDVOMasterEmployeeModel, out EmpCode, actPayrollDepartmentKeyValue);
            }
          }

          if (i > 0)
          {

            model.ApprovalStatus = Result;
            model.ApplicationId = Id;
            model.Notes = Notes;
            model.ApprovalProcessId = ApprovedAtLevelStage;
            model.ApprovedDate = DateTime.Now;
            db.Entry(model).State = EntityState.Added;
            result = db.SaveChanges();

            int user = AppUserManager.GetUserId();
            GratuityDetails objGratuityDetails = new GratuityDetails();
            objGratuityDetails.PersonID = objDVOMasterEmployeeModel.PersonID;
            objGratuityDetails.PensionApplicationId = Id;
            objGratuityDetails.FirstName = objDVOMasterEmployeeModel.FirstName;
            objGratuityDetails.MidName = objDVOMasterEmployeeModel.MidName;
            objGratuityDetails.LastName = objDVOMasterEmployeeModel.LastName;
            objGratuityDetails.GratuityAmt = calculatedResult.Gratuity;//to do objDVOMasterEmployeeModel.PersonID;
            objGratuityDetails.DiscountedGratuity = calculatedResult.DiscountedGratuity;//to doobjDVOMasterEmployeeModel.PersonID;
            objGratuityDetails.Notes = "";
            objGratuityDetails.PaidStatus = "U";
            objGratuityDetails.Paymentmethod = 0;
            objGratuityDetails.Check_no = "";

            objGratuityDetails.Isactive = true;
            objGratuityDetails.CreatedBy = user;
            objGratuityDetails.CreatedOn = DateTime.Now;
            objGratuityDetails.CreatedmachineInfo = System.Environment.MachineName; ;
            objGratuityDetails.ModifiedBy = user;
            objGratuityDetails.ModifiedOn = DateTime.Now;
            objGratuityDetails.ModifiedMachineInfo = System.Environment.MachineName;
            db.Entry(objGratuityDetails).State = EntityState.Added;
            result = db.SaveChanges();
            PensionApplications pensionModel = db.PensionApplications.Where(x => x.Id == Id).FirstOrDefault();
            pensionModel.IsActive = false;
            db.Entry(pensionModel).State = EntityState.Modified;
            db.SaveChanges();
            string Name = objDVOMasterEmployeeModel.FirstName + " " + objDVOMasterEmployeeModel.MidName + " " + objDVOMasterEmployeeModel.LastName;
            PensionAndRefundEmailRepo.sendMailProcedure(objDVOMasterEmployeeModel.Email, Id, Name, "ApplicationApproved");
            TempData["success"] = "Application Approved Successfully and Contributor moved to Beneficiary Successfully";

          }
          else
          {
            TempData["error"] = "Contributor to Beneficiary movement not done, try again";
          }
          //transaction.Commit();
          return Json(i, JsonRequestBehavior.AllowGet);
        }
        else
        {
          TempData["error"] = "This Record Already moved to Beneficiary";
        }
        //}
        //catch (Exception ex)
        //{
        //  transaction.Rollback();
        //  TempData["error"] = "There is some error, Pls Try Again Later. " + ex.Message;
        //}
        //}
      }

      model.ApprovalStatus = Result;
      model.ApplicationId = Id;
      model.Notes = Notes;
      model.ApprovalProcessId = ApprovedAtLevelStage;
      model.ApprovedDate = DateTime.Now;
      db.Entry(model).State = EntityState.Added;
      result = db.SaveChanges();

      var usersList = (from users in db.ApprovalProcessAssignedUser
                       join level in db.ApprovalProcessLevel.Where(x => x.ModuleId == 16) on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId
                       join status in db.ApplicationApprovalStatus.Where(x => x.ApplicationId == Id) on users.ApprovalProcessId equals status == null ? 0 : status.ApprovalProcessId into gj
                       from subpet in gj.DefaultIfEmpty()
                       select new { users.ApprovalUser, level = users.ApprovalProcessId, ApprovalProcessId = (subpet == null ? 0 : subpet.ApprovalProcessId) }).ToList(); ;

      //for mail   
      if (result > 0 && Result == "A" && ApprovedAtLevelStage != maxLevel)
      {
        var userId = usersList.Where(x => x.ApprovalProcessId == 0).Select(x => x.ApprovalUser).FirstOrDefault();
        var approvaluser = db.UserProfiles.Where(x => x.UserId == userId).Select(x => new { Name = x.FirstName + " " + x.MiddleName + " " + x.LastName, x.Email }).FirstOrDefault();
        PensionAndRefundEmailRepo.sendMailProcedure(approvaluser.Email, Id, approvaluser.Name, "Request For Pension Application Approval...");
        TempData["success"] = "Application Approved Successfully";
      }
      //else if (result > 0 && Result == "A" && ApprovedAtLevelStage == maxLevel)
      //{
      //  var applicant = db.MasterContributor.Where(x => x.Id.ToString() == objPensionApplications.PersonID).Select(x => new { Name = x.FirstName + " " + x.MidName + " " + x.LastName, x.Email }).FirstOrDefault();
      //  sendMailProcedure(applicant.Email, Id, applicant.Name, "Application For Pension Approved Successfully");
      //  TempData["success"] = "Application Approved Successfully";
      //}
      else if (result > 0 && Result == "R")
      {
        PensionApplications pensionModel = db.PensionApplications.Where(x => x.Id == Id).FirstOrDefault();
        pensionModel.IsActive = false;
        db.Entry(pensionModel).State = EntityState.Modified;
        db.SaveChanges();
        var allusers = usersList.Where(x => x.ApprovalProcessId != 0).Select(x => x.ApprovalUser).ToList();
        if (allusers.Count > 0)
        {
          foreach (var item in allusers)
          {
            var approvaluser = db.UserProfiles.Where(x => x.UserId == item).Select(x => new { Name = x.FirstName + " " + x.MiddleName + " " + x.LastName, x.Email }).FirstOrDefault();
            PensionAndRefundEmailRepo.sendMailProcedure(approvaluser.Email, Id, approvaluser.Name, "Request For Pension Application Rejected...");
          }
        }

        var applicant = db.MasterContributor.Where(x => x.Id.ToString() == objPensionApplications.PersonID).Select(x => new { Name = x.FirstName + " " + x.MidName + " " + x.LastName, x.Email }).FirstOrDefault();
        PensionAndRefundEmailRepo.sendMailProcedure(applicant.Email, Id, applicant.Name, "ApplicationRejected");
        TempData["success"] = "Application Rejected Successfully";
      }
      return Json(result, JsonRequestBehavior.AllowGet);


    }

    public ActionResult MasterEmployeeExcelUploadAjax(string regiontext)
    {
      if (!string.IsNullOrEmpty(regiontext))
      {
        regiontext = regiontext.Trim().ToUpper();
      }

      string formattedName = string.Empty;
      string fileName = string.Empty;
      string csvFileName = string.Empty;
      try
      {
        //  Get all files from Request object  
        HttpPostedFileBase file = Request.Files[0];

        if (file != null && file.ContentLength > 0)
        {
          // Verify Uploaded file for formula injection
          // only excel files will be uploaded
          // validate file size 
          byte[] fileData;

          // Read the file stream into a byte array
          using (var memoryStream = new MemoryStream())
          {
            byte[] buffer = new byte[81920]; // Buffer size of 80KB
            int bytesRead;
            while ((bytesRead = file.InputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
              memoryStream.Write(buffer, 0, bytesRead);
            }

            fileData = memoryStream.ToArray();
          }

          HttpPostedFileBase copiedFile = new MemoryPostedFile(fileData, file.FileName, file.ContentType);
          string VerifyUploadValidationFile = CheckExcelOrCsvFileForInjection.VerifyUploadValidationFile(copiedFile);
          if (VerifyUploadValidationFile != "Valid")
          {
            return Json(new { status = false, Error = VerifyUploadValidationFile }, JsonRequestBehavior.AllowGet);
          }

          fileName = Path.GetFileName(file.FileName);

          // Format the date and time as a string (e.g., "yyyyMMdd_HHmmss")
          formattedName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + Path.GetExtension(file.FileName);

          // Save data on local machine
          // Specify the file path where you want to save the uploaded file


          //var region = GetRegionName();
          var region = GetRegionName();
          var directoryName = region == "KASHMIR REGION" ? "K_MasterEmployeeUploads" : "J_MasterEmployeeUploads";
          //var serverMapPath = Server.MapPath($"~/DataFile/{directoryName}");
          string serverMapPath = Helper.Helper.SFTPMapPath(region, directoryName);


          //string serverMapPath = Server.MapPath("~/DataFile/MasterEmployeeUploads");
          if (!Directory.Exists(serverMapPath))
          {
            // Attempt to create the directory
            Directory.CreateDirectory(serverMapPath);
          }
          // Add file name with directory
          string filePath = Path.Combine(serverMapPath, formattedName);

          // Save the file to the server
          file.SaveAs(filePath);



          // ***** Code By Himanshu Rajput *****  Start

          string[] JAMMUREGION = { "DODA", "JAMMU", "KATHUA", "KISHTWAR", "POONCH", "RAJOURI", "RAMBAN", "REASI", "SAMBA", "UDHAMPUR" };
          string[] KASHMIRREGION = { "ANANTNAG", "BANDIPORA", "BARAMULLA", "BUDGAM", "GANDERBAL", "KULGAM", "KUPWARA", "PULWAMA", "SHOPIAN", "SRINAGAR" };

          List<CsvFileViewModel> List = new List<CsvFileViewModel>();
          DataSet dataSet = new DataSet();

          if (!string.IsNullOrEmpty(filePath))
          {
            System.Data.DataTable dataTables = upConvertExcelToDataTables(filePath);

            if (dataTables != null && dataTables.Columns.Contains("DISTRICT"))
            {
              dataSet.Tables.Add(dataTables);

              List = dataTables.AsEnumerable()
                  .Select(x => new CsvFileViewModel
                  {
                    SelectDistrict = x.IsNull("DISTRICT") ? "" : x["DISTRICT"].ToString(),
                  }).ToList();
            }
            else
            {
              Console.WriteLine(dataTables == null ? "DataTable is null." : "Column 'District' does not exist in the DataTable.");
            }
          }

          // bool districtIsValid = false;
          string[] regionToCheck = null;
          string regionErrorMessage = "";

          if (regiontext == "JAMMU REGION" || regiontext == "JAMMU ")
          {
            regionToCheck = JAMMUREGION;
            regionErrorMessage = " Please upload the correct Districts under Jammu Region.";
          }
          else
          {
            regionToCheck = KASHMIRREGION;
            regionErrorMessage = "Please upload the correct Districts under Kashmir Region.";
          }

          // Check if any district is invalid for the selected region
          if (List.Any(item => !regionToCheck.Contains(!string.IsNullOrEmpty(item.SelectDistrict.ToUpper()) ? item.SelectDistrict.Trim().ToUpper() : string.Empty)))
          {
            //TempData["error"] = regionErrorMessage;

            return Json(new { status = false, Error = regionErrorMessage }, JsonRequestBehavior.AllowGet);
          }

          // ***** Code By Himanshu Rajput *****  End





          string csvFilePath = filePath.Replace(".xlsx", ".csv").Replace(".xls", ".csv");
          csvFileName = Path.GetFileName(csvFilePath);
          SaveExcelAsCsv(filePath, csvFilePath);

          byte[] bytes = System.IO.File.ReadAllBytes(csvFilePath);
          string failedMessage = UploadDataFile(bytes, csvFileName);

          if (!string.IsNullOrEmpty(failedMessage))
          {
            return Json(failedMessage, JsonRequestBehavior.AllowGet);
          }

          // Convert csv file to data table to get counts
          System.Data.DataTable dataTable = ConvertCsvToDataTable(csvFilePath);

          int benif_Count = dataTable.Rows.Count;

          // Helper method to replace characters in a string
          Func<string, string> replaceCharacters = s => s.Replace("\r", "").Replace("\n", "");


          int Validated_count = dataTable.AsEnumerable()
            .Where(row => replaceCharacters(row.Field<string>("ACCOUNT_STATUS")) == "ACTIVE"
            && replaceCharacters(row.Field<string>("AADHAAR_STATUS")) == "AADHAAR SEEDED"
            && (replaceCharacters(row.Field<string>("ACCT_SCHEME_TYPE")) == "SAVINGS ACCOUNT" || (replaceCharacters(row.Field<string>("CATEGORY")) == "PCP" && replaceCharacters(row.Field<string>("ACCT_SCHEME_TYPE")) == "JOINT ACCOUNT"))
            && replaceCharacters(row.Field<string>("NAME_OF_APPLICANT")) == replaceCharacters(row.Field<string>("CBS_NAME"))
            && replaceCharacters(row.Field<string>("BENE_IFSC")) == replaceCharacters(row.Field<string>("BRANCH_CODE"))
          ).Count();

          int Notvalidated_count = benif_Count - Validated_count;

          StringBuilder stringBuilder = new StringBuilder();
          DateTime currentDateAndTime = DateTime.Now;

          // Format the date and time as "dd/MM/yyyy HH:mm"
          string formattedDateTime = currentDateAndTime.ToString("dd/MM/yyyy HH:mm");

          string userNameWithDate = "• [" + formattedDateTime + "] [" + AppUserManager.GetUserName() + "] ";

          DataColumnCollection columns = dataTable.Columns;
          if (!columns.Contains("APPLICATION_REFERENCE_NO"))
          {
            stringBuilder.AppendLine(userNameWithDate + ": APPLICATION_REFERENCE_NO Column is missing. <br />");
          }
          if (!columns.Contains("DISTRICT"))
          {
            stringBuilder.AppendLine(userNameWithDate + ": DISTRICT Column is missing. <br />");
          }
          if (!columns.Contains("BENE_IFSC"))
          {
            stringBuilder.AppendLine(userNameWithDate + ": BENE_IFSC Column is missing. <br />");
          }
          if (!columns.Contains("NAME_OF_APPLICANT"))
          {
            stringBuilder.AppendLine(userNameWithDate + ": NAME_OF_APPLICANT Column is missing. <br />");
          }
          if (!columns.Contains("ACCOUNTNO"))
          {
            stringBuilder.AppendLine(userNameWithDate + ": ACCOUNTNO Column is missing. <br />");
          }
          if (!columns.Contains("CATEGORY"))
          {
            stringBuilder.AppendLine(userNameWithDate + ": CATEGORY Column is missing. <br />");
          }
          if (!columns.Contains("CBS_NAME"))
          {
            stringBuilder.AppendLine(userNameWithDate + ": CBS_NAME Column is missing. <br />");
          }
          if (!columns.Contains("BRANCH_CODE"))
          {
            stringBuilder.AppendLine(userNameWithDate + ": BRANCH_CODE Column is missing. <br />");
          }
          if (!columns.Contains("ACCOUNT_STATUS"))
          {
            stringBuilder.AppendLine(userNameWithDate + ": ACCOUNT_STATUS Column is missing. <br />");
          }
          if (!columns.Contains("AADHAAR_STATUS"))
          {
            stringBuilder.AppendLine(userNameWithDate + ": AADHAAR_STATUS Column is missing. <br />");
          }
          if (!columns.Contains("ACCT_SCHEME_TYPE"))
          {
            stringBuilder.AppendLine(userNameWithDate + ": ACCT_SCHEME_TYPE Column is missing. <br />");
          }
          if (columns.Contains("APPLICATION_REFERENCE_NO"))
            if (dataTable.AsEnumerable().Where(row => string.IsNullOrEmpty(replaceCharacters(row.Field<string>("APPLICATION_REFERENCE_NO")))).Any())
            {
              stringBuilder.AppendLine(userNameWithDate + ": One or More APPLICATION_REFERENCE_NO is empty. <br />");
            }
          if (columns.Contains("DISTRICT"))
            if (dataTable.AsEnumerable().Where(row => string.IsNullOrEmpty(replaceCharacters(row.Field<string>("DISTRICT")))).Any())
            {
              stringBuilder.AppendLine(userNameWithDate + ": One or More DISTRICT is empty. <br />");
            }
          if (columns.Contains("BENE_IFSC"))
            if (dataTable.AsEnumerable().Where(row => string.IsNullOrEmpty(replaceCharacters(row.Field<string>("BENE_IFSC")))).Any())
            {
              stringBuilder.AppendLine(userNameWithDate + ": One or More BENE_IFSC is empty. <br />");
            }
          if (columns.Contains("APPLICATION_REFERENCE_NO"))
            if (dataTable.AsEnumerable().Where(row => string.IsNullOrEmpty(replaceCharacters(row.Field<string>("NAME_OF_APPLICANT")))).Any())
            {
              stringBuilder.AppendLine(userNameWithDate + ": One or More NAME_OF_APPLICANT is empty. <br />");
            }
          if (columns.Contains("APPLICATION_REFERENCE_NO"))
            if (dataTable.AsEnumerable().Where(row => string.IsNullOrEmpty(replaceCharacters(row.Field<string>("ACCOUNTNO")))).Any())
            {
              stringBuilder.AppendLine(userNameWithDate + ": One or More ACCOUNTNO is empty. <br />");
            }
          if (columns.Contains("APPLICATION_REFERENCE_NO"))
            if (dataTable.AsEnumerable().Where(row => string.IsNullOrEmpty(replaceCharacters(row.Field<string>("CATEGORY")))).Any())
            {
              stringBuilder.AppendLine(userNameWithDate + ": One or More CATEGORY is empty. <br />");
            }



          // Format the date and time as a string (e.g., "yyyyMMdd_HHmmss")
          formattedName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + Path.GetExtension(file.FileName);


          //string results = formattedName.Split('_')[0] + formattedName.Split('_')[1];
          //int length = 13;
          //int startIndex = results.Length - length;

          //int recordIdstr = int.Parse(Regex.Match(results.Substring(startIndex), @"\d+").Value);
          SaveDownloadMediaDetail(fileName, true, 0, benif_Count, Validated_count, Notvalidated_count, stringBuilder.ToString());

          if (!string.IsNullOrEmpty(stringBuilder.ToString()))
          {
            return Json(new { status = true, formattedName = "", Error = stringBuilder.ToString() }, JsonRequestBehavior.AllowGet);
          }
        }
      }
      catch (Exception ex)
      {
        return Json(new { status = false, formattedName = "", Error = ex.Message }, JsonRequestBehavior.AllowGet);
      }
      return Json(new { status = true, formattedName = csvFileName, Error = "" }, JsonRequestBehavior.AllowGet);
    }


    public static System.Data.DataTable upConvertExcelToDataTables(string filePath)
    {
      System.Data.DataTable dataTables = new System.Data.DataTable();

      try
      {
        using (var workbook = new XLWorkbook(filePath))
        {
          var worksheet = workbook.Worksheet(1); // Assumes data is in the first worksheet
          bool firstRow = true;

          foreach (var row in worksheet.RowsUsed())
          {
            // Skip empty rows
            if (row.IsEmpty())
            {
              continue;
            }

            if (firstRow)
            {
              foreach (var cell in row.CellsUsed())
              {
                dataTables.Columns.Add(cell.GetString());
              }
              firstRow = false;
            }
            else
            {
              var dataRow = dataTables.NewRow();
              int i = 0;

              foreach (var cell in row.CellsUsed())
              {
                dataRow[i] = cell.Value.ToString() ?? string.Empty;
                i++;
              }

              dataTables.Rows.Add(dataRow);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error: " + ex.Message);
        return null;
      }

      return dataTables;
    }
    private void SaveDownloadMediaDetail(string fileName, bool IsProcessed, Int32 recordIdstr, Int32 benif_Count, Int32 Validated_count, Int32 Notvalidated_count, string remark = "", string region = "", string period = "")
    {
      try
      {
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

        object[] parameters = new object[18];
        parameters[0] = fileName; // FileName 
        parameters[1] = true; // HasDownoaded
        parameters[2] = IsProcessed; // IsProcessed
        parameters[3] = Environment.MachineName; // CreatedMachineInfo
        parameters[4] = 1; // CreatedBy
        parameters[5] = DateTime.Now; // CreatedOn
        parameters[6] = true; // IsActive
        parameters[7] = Convert.ToInt32(MediaType.Validation_res); // MediaType
        parameters[8] = Environment.MachineName; // ModifiedMachineInfo
        parameters[9] = 1; // ModifiedBy
        parameters[10] = DateTime.Now; // ModifiedOn
        parameters[11] = recordIdstr;  // RecordID

        parameters[12] = Convert.ToInt32(benif_Count);
        parameters[13] = Convert.ToInt32(Validated_count);
        parameters[14] = Convert.ToInt32(Notvalidated_count);
        parameters[15] = remark;
        parameters[16] = region; // Region
        parameters[17] = period; // Period

        // Execute the stored procedure
        objDalBaseClass.ExecuteProcedure(ref parameters, "SaveDownloadMediaDetail");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    //Other Bank Multiple File Download
    public JsonResult MasterEmployeeExcelDownloadAjax(string BankName)
    {
      StringBuilder sb = new StringBuilder();
      List<FileContentResult> StringBuildobj = new List<FileContentResult>();
      try
      {
        BankName = BankName.Replace("ANDSYMBOL", "&");
        int UserId = AppUserManager.GetUserId();
        int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
        Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
        string[] Bank = BankName.Split(',');
        foreach (var Bankprocess in Bank)
        {

          // Format the date and time as a string (e.g., "yyyyMMdd_HHmmss")
          string formattedName = "ContributionCSV_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
          formattedName = formattedName.Replace(" ", "");

          var region = GetRegionName();
          var directoryName = (region.Trim().ToUpper() == "KASHMIR REGION" || region.Trim().ToUpper() == "KASHMIR") ? "K_MasterEmployeeDownloads" : "J_MasterEmployeeDownloads";
          //string filePath = ftpSetting["localFilePath"] + $"\\DataFiles\\{directoryName}\\" + formattedName;
          string filePath = Helper.Helper.AddExcelSheetIntoDatabasePath(region, directoryName, formattedName);

          sb.AppendLine(filePath);

          // Define the parameters if needed (e.g., for input parameters)
          var parameter1 = new SqlParameter("@RoleId", RoleId);
          var parameter2 = new SqlParameter("@filePathWithName", filePath);
          var parameter3 = new SqlParameter("@UserId", UserId);
          //var parameter3 = new SqlParameter("@ApplicantIFSCCode", ApplicantIFSCCode);
          var parameter4 = new SqlParameter("@BankName", Bankprocess);
          sb.AppendLine(Bankprocess);
          // Execute the stored procedure
          int result = db.Database.ExecuteSqlCommand("EXEC MasterEmployeeExcelExport @filePathWithName, @UserId,@BankName", parameter2, parameter3, parameter4);//parameter3,
          sb.AppendLine(result.ToString());
          // Create an FTP client
          WebClient ftpClient = new WebClient();
          ftpClient.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);
          //ftpClient.UseDefaultCredentials = true;



          string path = Helper.Helper.AddExcelSheetIntoDatabasePath1(region, directoryName, formattedName);

          // File path, attampt, dealy in attampt
          bool isExist = Helper.Helper.FileCheckInFTP(path, 3, 30);

          //bool isExist = System.IO.File.Exists(filePath);

          if (isExist)
          {
            // Download the file from the FTP server
            byte[] fileData = ftpClient.DownloadData(path);

            string serverMapPath = Helper.Helper.SFTPMapPath(region, directoryName);
            if (!Directory.Exists(serverMapPath))
            {
              // Attempt to create the directory
              Directory.CreateDirectory(serverMapPath);
            }
            // Add file name with directory
            string csvFilePath = Path.Combine(serverMapPath, formattedName);

            // Save the file to the server
            System.IO.File.WriteAllBytes(csvFilePath, fileData);

            // Convert csv file to data table to get counts
            System.Data.DataTable dataTable = ConvertCsvToDataTable(csvFilePath);

            int benif_Count = dataTable.Rows.Count;

            int Validated_count = dataTable.AsEnumerable().Count(row => row.Field<string>("ACCOUNT_STATUS") == "ACTIVE");
            int Notvalidated_count = dataTable.AsEnumerable().Count(row => row.Field<string>("ACCOUNT_STATUS") != "ACTIVE");

            string excelFilePath = csvFilePath.Replace(".csv", ".xlsx");
            /*
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(csvFilePath);
            wb.SaveAs(excelFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
            wb.Close(false);
            app.Quit();
            */

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                var format = new ExcelTextFormat
                {
                    Delimiter = ',',
                    Encoding = Encoding.UTF8
                };
                worksheet.Cells["A1"].LoadFromText(new FileInfo(csvFilePath), format);
                package.SaveAs(new FileInfo(excelFilePath));
            }
            //File Transfer To FstpServer

            // delete file in safe way
            FileHelper fileHelper = new FileHelper();
            bool isCsvFileDeleted = fileHelper.TryDeleteFile(csvFilePath);

            RegionProvider.Region = region;

            string regionFirstName = region.Trim().ToUpper().Replace("REGION", "").Trim();
            string uploadformattedName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{regionFirstName}_Validation{Path.GetExtension(excelFilePath)}";

            bool IsGeneratePensionWithoutSfp = IsGeneratePensionWithoutSftp;
            bool isSendValidationFileToSftp = IsSendValidationFileToSftp;
            if (!IsGeneratePensionWithoutSfp && isSendValidationFileToSftp)
            {
              UploadValidationFile(excelFilePath, uploadformattedName);
            }

            FileInfo fileInfo = new FileInfo(excelFilePath);

            // Get the size of the file in bytes
            long fileSizeInBytes = fileInfo.Length;
            double filesize = fileSizeInBytes / 1024.0;

            string results = uploadformattedName.Split('_')[0] + uploadformattedName.Split('_')[1];
            int length = 8;
            // Calculate the starting index for the last six characters
            int startIndex = results.Length - length;

            // Use Substring to get the last six characters
            string reducedString = results.Substring(startIndex);

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[20];
            /* parameters[0] = 0;*/ // Paysearch.pybatchid; // RecordId 
            parameters[0] = reducedString;
            parameters[1] = ftpSetting["sftpFilePath"] + $"/{directoryName}/Outbox/" + uploadformattedName; // FilePath
            parameters[2] = true; // IsUploaded
            parameters[3] = DateTime.Now; // UploadedDate
            parameters[4] = Environment.MachineName; // CreatedMachineInfo
            parameters[5] = AppUserManager.GetUserId(); // CreatedBy
            parameters[6] = DateTime.Now; // CreatedOn
            parameters[7] = true; // IsActive
            parameters[8] = string.Empty; // UploadErrors
            parameters[9] = false; // IsReUploaded
            parameters[10] = Convert.ToInt32(MediaType.Validation); // MediaType
            parameters[11] = Convert.ToInt32(benif_Count);
            parameters[12] = Convert.ToInt32(Validated_count);
            parameters[13] = Convert.ToInt32(Notvalidated_count);
            parameters[14] = string.Empty;
            parameters[15] = string.Empty;

            // Reupload Value  *** Code By Himanshu Rajput ***

            parameters[16] = false;   // IsReUploadedPermitted
            parameters[17] = 0;  // ReUploadedPermittedBy
            parameters[18] = null;  // ReUploadedPermittedDate
            parameters[19] = filesize;  // File Size


            // Execute the stored procedure
            objDalBaseClass.ExecuteProcedure(ref parameters, "SaveMediaQueueDetail");

            byte[] excelFileData = System.IO.File.ReadAllBytes(excelFilePath);

            // Specify the file's content type (MIME type)
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // Use the appropriate MIME type for your file
            if (IsGeneratePensionWithoutSfp || (!IsGeneratePensionWithoutSfp && !isSendValidationFileToSftp))
            {
              FileContentResult content = File(excelFileData, System.Net.Mime.MediaTypeNames.Application.Octet, uploadformattedName);
              StringBuildobj.Add(content);
              //return content;
            }
            else
            {
              var mailSetting = db.MailSettings.FirstOrDefault(a => a.IsActive && (a.ProcessName + "").Trim().ToLower() == "ForwardValidationFileSftpServer");
              if (mailSetting != null)
              {
                MailSettings mailMessages = new MailSettings();
                try
                {


                  mailMessages.Subject = "SFTP notification";
                  mailMessages.ProcessName = "";
                  mailMessages.MailTo = "";
                  dynamic email = new Email("ForwardValidationFileSftpServerMailSend");
                  if (SiteHelper.IsTestEmail == "1")
                  {
                    email.To = SiteHelper.TestEmail;
                  }
                  else
                  {
                    email.To = mailSetting.MailTo;
                  }
                  email.CC = mailSetting.CC;
                  email.BCC = mailSetting.BCC;
                  //email.CustomerName = "";
                  email.hostURL = SiteHelper.WebsiteURL;

                  //To read Body from mail object.
                  string htmlText = string.Empty;
                  try
                  {
                    Postal.IEmailService emailService = new Postal.EmailService();
                    System.Net.Mail.MailMessage message = emailService.CreateMailMessage(email);
                    using (var StreamObj = message.AlternateViews.FirstOrDefault().ContentStream)
                    using (StreamReader reader = new StreamReader(StreamObj))
                    {
                      htmlText = reader.ReadToEnd();
                    }
                  }
                  catch (Exception ex)
                  {
                    htmlText = ex.Message;
                  }
                  mailMessages.Contents = htmlText;
                  ///////////


                  if (mailSetting != null && mailSetting.IsInstantMailing)
                  {
                    email.Send();
                    //mailMessages.IsInstantMailing = true;
                    //mailMessages.CreatedOn= DateTime.Now;
                  }
                }
                catch (Exception ex)
                {

                  // mailMessages.IsSent = false;
                  //mailMessages.ErrorDescription = ex.Message;
                }
                // Return the file as a FileResult
                // return File(excelFileData, contentType, "Validation.xlsx"); // "file.txt" is the suggested file name for download
              }
            }

          }
        }
        //return StringBuildobj;
      }
      catch (Exception ex)

      {
        TempData["error"] = "Check Download Sheet And Try Again..." + ex.Message;
        ExceptionManagement.ExceptionManager.Publish(ex);
        //BLLPYBatchProcessStybatchr.WriteTextToFile(sb.ToString());
        return Json(new { Error = ex.Message }, JsonRequestBehavior.AllowGet);
      }
      // return Json("", JsonRequestBehavior.AllowGet);
      return Json(StringBuildobj.Select(file => new
      {
        FileName = file.FileDownloadName,
        FileContent = Convert.ToBase64String(file.FileContents),
        ContentType = file.ContentType
      }), JsonRequestBehavior.AllowGet);
    }
    //jk bank Download File 
    public ActionResult MasterJKBankEmployeeExcelDownloadAjax(string BankName)
    {
      StringBuilder sb = new StringBuilder();
      try
      {
        BankName = BankName.Replace("ANDSYMBOL", "&");
        int UserId = AppUserManager.GetUserId();
        int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
        Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();

        // Format the date and time as a string (e.g., "yyyyMMdd_HHmmss")
        string formattedName = "ContributionCSV_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
        formattedName = formattedName.Replace(" ", "");

        var region = GetRegionName();
        var directoryName = (region.Trim().ToUpper() == "KASHMIR REGION" || region.Trim().ToUpper() == "KASHMIR") ? "K_MasterEmployeeDownloads" : "J_MasterEmployeeDownloads";
        //string filePath = ftpSetting["localFilePath"] + $"\\DataFiles\\{directoryName}\\" + formattedName;
        string filePath = Helper.Helper.AddExcelSheetIntoDatabasePath(region, directoryName, formattedName);

        sb.AppendLine(filePath);

        // Define the parameters if needed (e.g., for input parameters)
        var parameter1 = new SqlParameter("@RoleId", RoleId);
        var parameter2 = new SqlParameter("@filePathWithName", filePath);
        var parameter3 = new SqlParameter("@UserId", UserId);
        //var parameter3 = new SqlParameter("@ApplicantIFSCCode", ApplicantIFSCCode);
        var parameter4 = new SqlParameter("@BankName", BankName);
        sb.AppendLine(BankName);
        // Execute the stored procedure
        int result = db.Database.ExecuteSqlCommand("EXEC MasterEmployeeExcelExport @filePathWithName, @UserId,@BankName", parameter2, parameter3, parameter4);//parameter3,
        sb.AppendLine(result.ToString());
        // Create an FTP client
        WebClient ftpClient = new WebClient();
        ftpClient.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);
        //ftpClient.UseDefaultCredentials = true;



        string path = Helper.Helper.AddExcelSheetIntoDatabasePath1(region, directoryName, formattedName);

        // File path, attampt, dealy in attampt
        bool isExist = Helper.Helper.FileCheckInFTP(path, 3, 30);

        //bool isExist = System.IO.File.Exists(filePath);

        if (isExist)
        {
          // Download the file from the FTP server
          byte[] fileData = ftpClient.DownloadData(path);

          string serverMapPath = Helper.Helper.SFTPMapPath(region, directoryName);
          if (!Directory.Exists(serverMapPath))
          {
            // Attempt to create the directory
            Directory.CreateDirectory(serverMapPath);
          }
          // Add file name with directory
          string csvFilePath = Path.Combine(serverMapPath, formattedName);

          // Save the file to the server
          System.IO.File.WriteAllBytes(csvFilePath, fileData);

          // Convert csv file to data table to get counts
          System.Data.DataTable dataTable = ConvertCsvToDataTable(csvFilePath);

          int benif_Count = dataTable.Rows.Count;

          int Validated_count = dataTable.AsEnumerable().Count(row => row.Field<string>("ACCOUNT_STATUS") == "ACTIVE");
          int Notvalidated_count = dataTable.AsEnumerable().Count(row => row.Field<string>("ACCOUNT_STATUS") != "ACTIVE");

          string excelFilePath = csvFilePath.Replace(".csv", ".xlsx");
          /*
          Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
          Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(csvFilePath);
          wb.SaveAs(excelFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
          wb.Close(false);
          app.Quit();
          */

          using (var package = new ExcelPackage())
          {
              var worksheet = package.Workbook.Worksheets.Add("Sheet1");
              var format = new ExcelTextFormat
              {
                  Delimiter = ',',
                  Encoding = Encoding.UTF8
              };
              worksheet.Cells["A1"].LoadFromText(new FileInfo(csvFilePath), format);
              package.SaveAs(new FileInfo(excelFilePath));
          }
          //File Transfer To FstpServer

          // delete file in safe way
          FileHelper fileHelper = new FileHelper();
          bool isCsvFileDeleted = fileHelper.TryDeleteFile(csvFilePath);

          RegionProvider.Region = region;

          string regionFirstName = region.Trim().ToUpper().Replace("REGION", "").Trim();
          string uploadformattedName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{regionFirstName}_Validation{Path.GetExtension(excelFilePath)}";

          bool IsGeneratePensionWithoutSfp = IsGeneratePensionWithoutSftp;
          bool isSendValidationFileToSftp = IsSendValidationFileToSftp;
          if (!IsGeneratePensionWithoutSfp && isSendValidationFileToSftp)
          {
            UploadValidationFile(excelFilePath, uploadformattedName);
          }

          FileInfo fileInfo = new FileInfo(excelFilePath);

          // Get the size of the file in bytes
          long fileSizeInBytes = fileInfo.Length;
          double filesize = fileSizeInBytes / 1024.0;

          string results = uploadformattedName.Split('_')[0] + uploadformattedName.Split('_')[1];
          int length = 8;
          // Calculate the starting index for the last six characters
          int startIndex = results.Length - length;

          // Use Substring to get the last six characters
          string reducedString = results.Substring(startIndex);

          DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
          DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
          object[] parameters = new object[20];
          /* parameters[0] = 0;*/ // Paysearch.pybatchid; // RecordId 
          parameters[0] = reducedString;
          parameters[1] = ftpSetting["sftpFilePath"] + $"/{directoryName}/Outbox/" + uploadformattedName; // FilePath
          parameters[2] = true; // IsUploaded
          parameters[3] = DateTime.Now; // UploadedDate
          parameters[4] = Environment.MachineName; // CreatedMachineInfo
          parameters[5] = AppUserManager.GetUserId(); // CreatedBy
          parameters[6] = DateTime.Now; // CreatedOn
          parameters[7] = true; // IsActive
          parameters[8] = string.Empty; // UploadErrors
          parameters[9] = false; // IsReUploaded
          parameters[10] = Convert.ToInt32(MediaType.Validation); // MediaType
          parameters[11] = Convert.ToInt32(benif_Count);
          parameters[12] = Convert.ToInt32(Validated_count);
          parameters[13] = Convert.ToInt32(Notvalidated_count);
          parameters[14] = string.Empty;
          parameters[15] = string.Empty;

          // Reupload Value  *** Code By Himanshu Rajput ***

          parameters[16] = false;   // IsReUploadedPermitted
          parameters[17] = 0;  // ReUploadedPermittedBy
          parameters[18] = null;  // ReUploadedPermittedDate
          parameters[19] = filesize;  // File Size


          // Execute the stored procedure
          objDalBaseClass.ExecuteProcedure(ref parameters, "SaveMediaQueueDetail");

          byte[] excelFileData = System.IO.File.ReadAllBytes(excelFilePath);

          // Specify the file's content type (MIME type)
          string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // Use the appropriate MIME type for your file
          if (IsGeneratePensionWithoutSfp || (!IsGeneratePensionWithoutSfp && !isSendValidationFileToSftp))
          {
            FileContentResult content = File(excelFileData, System.Net.Mime.MediaTypeNames.Application.Octet, uploadformattedName);
            return content;
          }
          else
          {
            var mailSetting = db.MailSettings.FirstOrDefault(a => a.IsActive && (a.ProcessName + "").Trim().ToLower() == "ForwardValidationFileSftpServer");
            if (mailSetting != null)
            {
              MailSettings mailMessages = new MailSettings();
              try
              {


                mailMessages.Subject = "SFTP notification";
                mailMessages.ProcessName = "";
                mailMessages.MailTo = "";
                dynamic email = new Email("ForwardValidationFileSftpServerMailSend");
                if (SiteHelper.IsTestEmail == "1")
                {
                  email.To = SiteHelper.TestEmail;
                }
                else
                {
                  email.To = mailSetting.MailTo;
                }
                email.CC = mailSetting.CC;
                email.BCC = mailSetting.BCC;
                //email.CustomerName = "";
                email.hostURL = SiteHelper.WebsiteURL;

                //To read Body from mail object.
                string htmlText = string.Empty;
                try
                {
                  Postal.IEmailService emailService = new Postal.EmailService();
                  System.Net.Mail.MailMessage message = emailService.CreateMailMessage(email);
                  using (var StreamObj = message.AlternateViews.FirstOrDefault().ContentStream)
                  using (StreamReader reader = new StreamReader(StreamObj))
                  {
                    htmlText = reader.ReadToEnd();
                  }
                }
                catch (Exception ex)
                {
                  htmlText = ex.Message;
                }
                mailMessages.Contents = htmlText;
                ///////////


                if (mailSetting != null && mailSetting.IsInstantMailing)
                {
                  email.Send();
                  //mailMessages.IsInstantMailing = true;
                  //mailMessages.CreatedOn= DateTime.Now;
                }
              }
              catch (Exception ex)
              {

                // mailMessages.IsSent = false;
                //mailMessages.ErrorDescription = ex.Message;
              }
              // Return the file as a FileResult
              return File(excelFileData, contentType, "Validation.xlsx"); // "file.txt" is the suggested file name for download
            }
          }

        }
      }
      catch (Exception ex)

      {
        TempData["error"] = "Check Download Sheet And Try Again..." + ex.Message;
        ExceptionManagement.ExceptionManager.Publish(ex);
        //BLLPYBatchProcessStybatchr.WriteTextToFile(sb.ToString());
      }
      return Json("", JsonRequestBehavior.AllowGet);
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
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw;
      }
      return dataTable;

    }

    //File Transfer To fstp Server

    private void UploadValidationFile(string excelFilePath, string formattedName)
    {
      string fileReturn = "";
      try
      {
        Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
        // Get the file name

        var region = GetRegionName();
        bool IsFTP = Convert.ToBoolean(ftpSetting["IsFTP"]);
        if (IsFTP)
        {
          //string formattedName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "JK_Disbursement" + (Path.GetExtension(excelFilePath));

          byte[] fileContents = System.IO.File.ReadAllBytes(excelFilePath);

          //var region = Session["RegionName"].ToString();

          //var region = GetRegionName();

          var directoryName = (region.Trim().ToUpper() == "KASHMIR REGION" || region.Trim().ToUpper() == "KASHMIR") ? "K_Validation" : "J_Validation";
          //string ftpServerUrl = ftpSetting["sftpServerUrl"] + $"/DataFiles/{directoryName}/Outbox/" + formattedName;
          string ftpServerUrl = Helper.Helper.UploadValidationFilePath(region, directoryName, formattedName);

          //string ftpServerUrl = ftpSetting["sftpServerUrl"] + "/DataFiles/Validation/Outbox/" + formattedName;
          FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpServerUrl);
          ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
          ftpRequest.Timeout = 600000;
          ftpRequest.Credentials = new NetworkCredential(ftpSetting["sftpUsername"], ftpSetting["sftpPassword"]);
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
          string remoteDirectory = Helper.Helper.GetSftpPath(ftpSetting["sftpFilePath"], region, Helper.Helper.SftpModule.Validation, Helper.Helper.SftpFolder.Request);
          string localfilepathSFTP = Server.MapPath("~/" + ftpSetting["sftpPrivateKeyPath"]);
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

            using (var fileStream = new FileStream(localFilePath, FileMode.Open))
            {
              // Upload the file
              sftpClient.UploadFile(fileStream, Path.Combine(remoteDirectory, formattedName));
            }

            // Disconnect from the SFTP server
            sftpClient.Disconnect();
          }
          fileReturn = Path.Combine(remoteDirectory, formattedName);
          //using (var client = new SftpClient(host, port, username, password))
          //{
          //  client.Connect();

          //  // Ensure the remote directory exists
          //  if (!client.Exists(remoteDirectory))
          //  {
          //    client.CreateDirectory(remoteDirectory);
          //  }

          //  using (var fileStream = new FileStream(localFilePath, FileMode.Open))
          //  {
          //    // Upload the file
          //    client.UploadFile(fileStream, Path.Combine(remoteDirectory, Path.GetFileName(localFilePath)));
          //  }

          //  client.Disconnect();
          //}
        }

        try
        {
          var placeholders = new Dictionary<string, string>
          {
            { "FilePath", fileReturn }
          };

          Services.EmailService.SendProcessEmail("Disbursement_FILE_UPLOAD", placeholders);
        }
        catch (Exception ex)
        {
          ExceptionManagement.ExceptionManager.Publish(ex);
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw;
      }
    }




    public ActionResult AddMasterEmployeeExcelDatabaseAjax()
    {
      string msg = "0";
      try
      {
        int UserId = AppUserManager.GetUserId();
        string fileName = Request.Form["formattedName"];

        if (fileName.Contains(".csv") || fileName.Contains(".xlsx") || fileName.Contains(".xls"))
        {
          Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
          // Get the file name

          // Define the path for sql

          //var region = GetRegionName();
          var region = GetRegionName();
          var directoryName = region == "KASHMIR REGION" ? "K_MasterEmployeeUploads" : "J_MasterEmployeeUploads";
          string filePath = Helper.Helper.AddExcelSheetIntoDatabasePath(region, directoryName, fileName);
          //string filePath = ftpSetting["localFilePath"] + $"\\DataFiles\\{directoryName}\\" + fileName;



          //string filePath = ftpSetting["localFilePath"] + "\\DataFiles\\MasterEmployeeUploads\\" + fileName;

          // Define the parameters if needed (e.g., for input parameters)
          var parameter1 = new SqlParameter("@filePathWithName", filePath);
          var parameter2 = new SqlParameter("@UserId", UserId);
          // Execute the stored procedure
          int result = db.Database.ExecuteSqlCommand("EXEC UpdateEmpMasterEmpBankDetails @filePathWithName , @UserId", parameter1, parameter2);
          msg = "1";
        }
        else
        {
          msg = "0";
        }

        #region
        // mail Send SFTP Server 

        var mailSetting = db.MailSettings.FirstOrDefault(a => a.IsActive && (a.ProcessName + "").Trim().ToLower() == "ResponseValidationFileBank");
        if (mailSetting != null)
        {
          MailSettings mailMessages = new MailSettings();
          try
          {
            mailMessages.Subject = "SFTP notification";
            mailMessages.ProcessName = "";
            mailMessages.MailTo = "";
            dynamic email = new Email("ResponseValidationFileBank");
            if (SiteHelper.IsTestEmail == "1")
            {
              email.To = SiteHelper.TestEmail;
            }
            else
            {
              email.To = mailSetting.MailTo;
            }
            email.CC = mailSetting.CC;
            email.BCC = mailSetting.BCC;
            //email.CustomerName = "";
            email.hostURL = SiteHelper.WebsiteURL;

            //To read Body from mail object.
            string htmlText = string.Empty;
            try
            {
              Postal.IEmailService emailService = new Postal.EmailService();
              System.Net.Mail.MailMessage messages = emailService.CreateMailMessage(email);
              using (var StreamObj = messages.AlternateViews.FirstOrDefault().ContentStream)
              using (StreamReader reader = new StreamReader(StreamObj))
              {
                htmlText = reader.ReadToEnd();
              }
            }
            catch (Exception ex)
            {
              htmlText = ex.Message;
            }
            mailMessages.Contents = htmlText;

            if (mailSetting != null && mailSetting.IsInstantMailing)
            {
              email.Send();
              //mailMessages.IsInstantMailing = true;
              //mailMessages.CreatedOn= DateTime.Now;
            }
          }
          catch (Exception ex)
          {
            // mailMessages.IsSent = false;
            //mailMessages.ErrorDescription = ex.Message;
          }
        }
        #endregion

      }
      catch (Exception ex)
      {
        msg = "0";
      }
      return Json(msg, JsonRequestBehavior.AllowGet);
    }
    static void SaveExcelAsCsv(string excelFilePath, string csvFilePath)
    {
      try
      {
        /*
        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(excelFilePath);
        wb.SaveAs(csvFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlCSVWindows);
        wb.Close(false);
        app.Quit();
        */

        using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read))
        {
          using (var package = new ExcelPackage(stream))
          {
            var worksheet = package.Workbook.Worksheets[1];
            var csvBuilder = new StringBuilder();
            if (worksheet.Dimension != null)
            {
              int rowCount = worksheet.Dimension.End.Row;
              int colCount = worksheet.Dimension.End.Column;

              for (int row = 1; row <= rowCount; row++)
              {
                var values = new List<string>();
                for (int col = 1; col <= colCount; col++)
                {
                  string text = worksheet.Cells[row, col].Value?.ToString() ?? "";
                  text = text.Replace("\"", "\"\"");
                  if (text.Contains(",") || text.Contains("\"") || text.Contains("\n"))
                    text = $"\"{text}\"";
                  values.Add(text);
                }
                csvBuilder.AppendLine(string.Join(",", values));
              }
            }
            System.IO.File.WriteAllText(csvFilePath, csvBuilder.ToString(), Encoding.UTF8);
          }
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }
    private string UploadDataFile(byte[] fileContents, string fileName)
    {
      string failedMessage = string.Empty;
      try
      {
        Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
        // Get the file name

        //var region = Session["RegionName"].ToString();

        //var region = GetRegionName();
        var region = GetRegionName();
        var directoryName = region == "KASHMIR REGION" ? "K_MasterEmployeeUploads" : "J_MasterEmployeeUploads";
        //string ftpServerUrl = ftpSetting["ftpServerUrl"] + $"/DataFiles/{directoryName}/" + fileName;
        string ftpServerUrl = Helper.Helper.GetUploadDataFile(region, directoryName, fileName);
        //string ftpServerUrl = ftpSetting["ftpServerUrl"] + "/DataFiles/MasterEmployeeUploads/" + fileName;

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

    //public ActionResult _PaymentHistory(string Id)
    //{
    //  string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(Id));
    //  @ViewBag.CId = Id;// IdUrl;
    //  DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(IdUrl);
    //  ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
    //  ViewBag.ApplicationReferenceNo = objDVOMasterEmployee.ApplicationReferenceNo;
    //  ViewBag.TypeCode = db.MasterEmpType.FirstOrDefault(x => x.Type_Code.ToLower().Trim() == objDVOMasterEmployee.TypeCode.ToLower().Trim())?.Description;
    //  ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
    //  return View();
    //}



    public ActionResult _PaymentHistoryHandlerAjax(JQueryDataTableParamModel param, string PensionerID, bool isDownload = false)
    {
      try
      {


        IEnumerable<PaymentModel> filtered;
        DataSet ds = new DataSet();
        StringBuilder SQL = new StringBuilder();

        SQL.Append(" SELECT eb.bank_acct_no AS AccountNo, eb.BankName, eb.APPLICANT_BANK_IFSC_CODE AS IFSCCode, ");
        SQL.Append(" CAST(dd.pay_date AS DATETIME) AS PaidOn, CAST(dd.amount AS VARCHAR(50)) AS amount, dd.[Status], dd.[Reason/Remarks], eb.empl_code, eb.Application_Reference_no, me.PresentAddress, eb.bank_code, eb.BranchName, CONCAT(me.first_Name, ' ', me.middle_name, ' ', me.last_Name) AS [Name], ");
        SQL.Append(" CAST(dd.pay_doc_no AS VARCHAR(50)) AS PayDocNo ");
        SQL.Append(" FROM Process_DirectDeposit_Details dd ");
        SQL.Append(" LEFT JOIN MasterEmpBankDetails eb ON dd.empl_code = eb.empl_code ");
        SQL.Append(" LEFT JOIN masterEmployee me ON me.Empl_Code = eb.Empl_code ");
        if (!string.IsNullOrEmpty(PensionerID))
          SQL.Append(" WHERE dd.empl_code = '" + PensionerID + "' ");

        SQL.Append(" UNION ALL ");

        SQL.Append(" SELECT eb.bank_acct_no AS AccountNo, eb.BankName, eb.APPLICANT_BANK_IFSC_CODE AS IFSCCode, ");
        SQL.Append(" TRY_CONVERT(DATETIME, td.TransactionDate, 105) AS PaidOn, CAST(td.Amount AS VARCHAR(50)) AS amount, td.[Status], td.Remarks AS [Reason/Remarks], eb.empl_code, eb.Application_Reference_no, me.PresentAddress, eb.bank_code, eb.BranchName, CONCAT(me.first_Name, ' ', me.middle_name, ' ', me.last_Name) AS [Name], ");
        SQL.Append(" CAST(td.TransactionReference AS VARCHAR(50)) AS PayDocNo ");
        SQL.Append(" FROM txnDetail td ");
        SQL.Append(" INNER JOIN MasterEmpBankDetails eb ON td.[Application Reference No#] = CAST(eb.Application_Reference_no AS NVARCHAR(50)) ");
        SQL.Append(" LEFT JOIN masterEmployee me ON me.Empl_Code = eb.Empl_code ");
        if (!string.IsNullOrEmpty(PensionerID))
          SQL.Append(" WHERE eb.empl_code = '" + PensionerID + "' ");
        //string con = WebConfigurationManager.AppSettings["SQLConn"];
        string con = ConnectionStringProvider.GetConnectionString();
        SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
        da.Fill(ds);
        List<PaymentModel> palList = new List<PaymentModel>();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          PaymentModel md = new PaymentModel();
          md.AccountNo = dr[0].ToString();
          md.BankName = dr[1].ToString();
          md.IFSCCode = dr[2].ToString();
          md.PaidOn = dr[3].ToString().Split(' ')[0];
          md.amount = dr[4].ToString();
          md.Status = dr[5].ToString();
          md.Reason = dr[6].ToString();
          md.EmplCode = dr[7].ToString();
          md.ApplicationReferenceNo = dr[8].ToString();
          md.PresentAddress = dr[9].ToString();
          md.BankCode = dr[10].ToString();
          md.BranchName = dr[11].ToString();
          md.Name = dr[12].ToString();
          md.PayDocNo = dr[13].ToString();
          palList.Add(md);
        }


        if (!string.IsNullOrEmpty(param.sSearch))
        {
          filtered = palList
             .Where(c => c.AccountNo.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.BankName.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.IFSCCode.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.PaidOn.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.amount.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.Status.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.Reason.ToString().ToLower().Contains(param.sSearch.ToLower()));

        }
        else
        {
          filtered = palList;
        }



        //Sorting through column index
        var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

        Func<PaymentModel, string> orderingFunction = (c => sortColumnIndex == 0 ? c.AccountNo :
         sortColumnIndex == 1 ? c.BankName + "" :
         sortColumnIndex == 1 ? c.IFSCCode + "" :
         sortColumnIndex == 1 ? c.PaidOn + "" :
         sortColumnIndex == 2 ? c.amount + "" :
         sortColumnIndex == 2 ? c.Status + "" :
         sortColumnIndex == 3 ? c.Reason + "" :
         "");

        var sortDirection = Request["sSortDir_0"]; // asc or desc
        if (sortDirection == "asc")
          filtered = filtered.OrderByDescending(orderingFunction);
        else
          filtered = filtered.OrderByDescending(orderingFunction);

        if (isDownload)
        {
          FileContentResult bytesdata = PaymentHistoryReport(filtered);
          return Json(bytesdata, JsonRequestBehavior.AllowGet);
        }
        //Pagging
        var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

        //Select required columns
        var result = from c in displayed
                     select new[] {
                     c.AccountNo,
                     c.BankName,
                     c.IFSCCode,
                     c.PaidOn,
                     c.amount,
                     c.Status,
                     c.Reason,
                     c.EmplCode,
                     c.ApplicationReferenceNo,
                     c.PresentAddress,
                     c.BankCode,
                     c.BranchName,
                     c.Name,
                     c.PayDocNo + ""
                      };

        return Json(
                    new
                    {
                      sEcho = param.sEcho,
                      iTotalRecords = palList.Count(),
                      iTotalDisplayRecords = filtered.Count(),
                      aaData = result
                    }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        throw;
      }
    }





    public ActionResult _OldBankHistory()
    {
      return View();
    }
    public ActionResult _OldBankHistoryAjaxHandler(JQueryDataTableParamModel param, string PensionerID)
    {
      DataSet ds = new DataSet();
      //DataTable ds = new DataTable();ist
      StringBuilder SQL = new StringBuilder();
      //var User = SQL.Append("select Id,Email from AppUser where Email==Email");
      SQL.Append("SELECT  AUC.Username as CreatedBy, A.CreatedOn, AUM.Username as ModifiedBy, A.ModifiedOn, A.EventType, A.TableName," +
        "  A.ColumnName, A.OldValue, A.NewValue, A.Url, A.Controller,A.Action, A.Area, A.IPAddress, A.RecordId, A.OldSystemId, A.Message FROM AuditLogs A   ");
      SQL.Append(" INNER JOIN AppUser AUC WITH (NOLOCK) ON AUC.Id= A.CreatedBy  ");
      SQL.Append(" LEFT JOIN  AppUser AUM WITH (NOLOCK) ON  AUM.Id  =A.ModifiedBy   ");
      SQL.Append(" Where  A.Oldvalue != A.NewValue");
      SQL.Append(" AND A.TableName = 'MasterEmpBankDetails'");
      //SQL.Append(" AND A.E = 'MasterEmployeeIncomes'");
      SQL.Append(" AND A.RecordId = '" + PensionerID + "' order by A.ModifiedOn desc");

      //string con = WebConfigurationManager.AppSettings["SQLConn"];
      string con = ConnectionStringProvider.GetConnectionString();
      SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
      da.Fill(ds);

      List<AuditLogs> LogsList = new List<AuditLogs>();
      foreach (DataRow dr in ds.Tables[0].Rows)
      {
        AuditLogs AuditLogs = new AuditLogs();

        AuditLogs.UsernameCrby = (dr[0] != DBNull.Value ? (dr[0]).ToString() : "");
        if (dr[1] != DBNull.Value && dr[1].ToString().Trim() != string.Empty)
          AuditLogs.CreatedOn = Convert.ToDateTime(dr[1]);
        AuditLogs.UsernameModby = (dr[2] != DBNull.Value ? (dr[2]).ToString() : "");
        if (dr[3] != DBNull.Value && dr[3].ToString().Trim() != string.Empty)
          AuditLogs.ModifiedOn = Convert.ToDateTime(dr[3]);
        //AuditLogs.ModifiedOn = Convert.ToDateTime(dr[3]);
        AuditLogs.EventType = (dr[4] != DBNull.Value ? (dr[4]).ToString() : "");
        AuditLogs.TableName = (dr[5] != DBNull.Value ? (dr[5]).ToString() : "");
        AuditLogs.ColumnName = (dr[6] != DBNull.Value ? (dr[6]).ToString() : "");
        AuditLogs.OldValue = (dr[7] != DBNull.Value ? (dr[7]).ToString() : "");
        AuditLogs.NewValue = (dr[8] != DBNull.Value ? (dr[8]).ToString() : "");
        //AuditLogs.Url = (dr[9] != DBNull.Value ? (dr[9]).ToString() : "");
        //AuditLogs.Controller = (dr[10] != DBNull.Value ? (dr[10]).ToString() : "");
        //AuditLogs.Action = (dr[11] != DBNull.Value ? (dr[11]).ToString() : "");
        //AuditLogs.Area = (dr[12] != DBNull.Value ? (dr[12]).ToString() : "");
        //AuditLogs.IPAddress = (dr[13] != DBNull.Value ? (dr[13]).ToString() : "");
        //AuditLogs.RecordId = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);
        //AuditLogs.OldSystemId = (dr[15] != DBNull.Value ? Convert.ToInt32(dr[15]) : 0);
        //AuditLogs.Message = (dr[16] != DBNull.Value ? (dr[16]).ToString() : "");

        LogsList.Add(AuditLogs);
      }

      IEnumerable<AuditLogs> filtered;
      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = LogsList
           //.Where(c => c.Id.ToString().Contains(param.sSearch.ToLower())
           .Where(c => c.UsernameCrby.ToString().Contains(param.sSearch.ToLower())
           || c.CreatedOn.ToString().Contains(param.sSearch.ToLower())
           || c.UsernameModby.ToString().Contains(param.sSearch.ToLower())
           || c.ModifiedOn.ToString().Contains(param.sSearch.ToLower())
           //|| c.IsActive.ToString().Contains(param.sSearch.ToLower())
           || c.EventType.ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
           || c.TableName.ToLower().Contains(param.sSearch.ToLower())
           || c.ColumnName.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.OldValue.ToLower().Contains(param.sSearch.ToLower())
           || c.NewValue.ToLower().Contains(param.sSearch.ToLower()));
        //|| c.Url.ToLower().Contains(param.sSearch.ToLower())
        //|| c.Controller.ToLower().Contains(param.sSearch.ToLower())
        //|| c.Action.ToLower().Contains(param.sSearch.ToLower())
        //|| c.Area.ToLower().Contains(param.sSearch.ToLower())
        //|| c.IPAddress.ToLower().Contains(param.sSearch.ToLower())
        //|| c.RecordId.ToString().Contains(param.sSearch.ToLower())
        //|| c.OldSystemId.ToString().Contains(param.sSearch.ToLower())
        //|| c.Message.ToString().Contains(param.sSearch.ToLower()));
      }
      else
      {
        filtered = LogsList;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<AuditLogs, string> orderingFunction = (c => sortColumnIndex == 0 ? c.UsernameModby + "" :
                                                                                      sortColumnIndex == 1 ? c.ModifiedOn + "" :
                                                                                      sortColumnIndex == 2 ? c.ColumnName + "" :
                                                                                      sortColumnIndex == 3 ? c.OldValue :
                                                                                      sortColumnIndex == 4 ? c.NewValue + "" :
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

                         //c.Id+"",
                         //c.UsernameCrby + "",
                         //c.CreatedOn + "",
                         c.UsernameModby + "",
                         c.ModifiedOn + "", 
                         //c.IsActive +"",
                         //c.EventType,
                         //c.TableName,
                         c.ColumnName,
                         TrimStart(c.OldValue, "<br />") + "",
                         TrimStart(c.NewValue, "<br />") + "",
                         //c.Url,
                         //c.Controller,
                         //c.Action,
                         //c.Area + "",
                         //c.IPAddress,
                         //c.RecordId + "",
                         //c.OldSystemId + "",
                         //c.Message
                   };

      return Json(
                                  new
                                  {
                                    sEcho = param.sEcho,
                                    iTotalRecords = LogsList.Count(),
                                    iTotalDisplayRecords = filtered.Count(),
                                    aaData = result
                                  }, JsonRequestBehavior.AllowGet);
    }



    //public ActionResult MasterEmployeeBankDetailUpload()
    //{
    //  string formattedName = string.Empty;
    //  string fileName = string.Empty;
    //  string csvFileName = string.Empty;
    //  try
    //  {
    //    //  Get all files from Request object  
    //    HttpPostedFileBase file = Request.Files[0];

    //    if (file != null && file.ContentLength > 0)
    //    {
    //      fileName = Path.GetFileName(file.FileName);

    //      // Format the date and time as a string (e.g., "yyyyMMdd_HHmmss")
    //      formattedName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + Path.GetExtension(file.FileName);

    //      // Save data on local machine
    //      // Specify the file path where you want to save the uploaded file
    //      string serverMapPath = Server.MapPath("~/DataFile/MasterEmployeeBankDetailUploads");
    //      if (!Directory.Exists(serverMapPath))
    //      {
    //        // Attempt to create the directory
    //        Directory.CreateDirectory(serverMapPath);
    //      }
    //      // Add file name with directory
    //      string filePath = Path.Combine(serverMapPath, formattedName);

    //      // Save the file to the server
    //      file.SaveAs(filePath);

    //      string csvFilePath = filePath.Replace(".xlsx", ".csv").Replace(".xls", ".csv");
    //      csvFileName = Path.GetFileName(csvFilePath);
    //      SaveExcelAsCsv(filePath, csvFilePath);

    //      byte[] bytes = System.IO.File.ReadAllBytes(csvFilePath);
    //      string failedMessage = UploadDataFile(bytes, csvFileName);

    //      if (!string.IsNullOrEmpty(failedMessage))
    //      {
    //        return Json(failedMessage, JsonRequestBehavior.AllowGet);
    //      }
    //    }
    //  }
    //  catch (Exception)
    //  {
    //    throw;
    //  }
    //  return Json(csvFileName, JsonRequestBehavior.AllowGet);
    //}

    //public ActionResult AddEmployeeBankDetailDatabase()
    //{
    //  string msg = "0";
    //  try
    //  {
    //    string fileName = Request.Form["formattedName"];

    //    if (fileName.Contains(".csv") || fileName.Contains(".xlsx") || fileName.Contains(".xls"))
    //    {
    //      Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
    //      // Get the file name

    //      // Define the path for sql
    //      string filePath = ftpSetting["localFilePath"] + "\\DataFiles\\MasterEmployeeUploads\\" + fileName;

    //      // Define the parameters if needed (e.g., for input parameters)
    //      var parameter1 = new SqlParameter("@filePathWithName", filePath);
    //      // Execute the stored procedure
    //      int result = db.Database.ExecuteSqlCommand("EXEC UpdateEmpMasterEmpBankDetails @filePathWithName", parameter1);
    //      msg = "1";
    //    }
    //    else
    //    {
    //      msg = "0";
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    msg = "0";
    //  }
    //  return Json(msg, JsonRequestBehavior.AllowGet);
    //}
    private FileContentResult JandKBeneficiaryReport(List<DVOMasterEmployee> listSearch)
    {
      try
      {
        var result = (from x in listSearch
                      select new DVOMasterEmployee
                      {

                        ApplicationReferenceNo = x.ApplicationReferenceNo + "",
                        FirstName = x.FirstName + "",
                        Gender = x.Gender + "",
                        // AgeInYears = x.AgeInYears + "",
                        AgeInYears_String = x.AgeInYears + "",
                        Address1 = x.Address1,
                        Phone = x.Phone,
                        mailid = x.mailid,
                        BankAcctNo = x.BankAcctNo,
                        IFSCCode = x.IFSCCode,
                        BankName = x.BankName,
                        //LastVerified = x.LastVerified,
                        //type_desc = x.type_desc,
                        //EmplCode = UrlEncryption.EncryptURL(Convert.ToString(x.EmplCode)),
                        //MiddleName = x.MiddleName + "",
                        //LastName = x.LastName + "",
                        //NameoftheApplicant = x.NameoftheApplicant + "",
                        ACCOUNT_STATUS = x.ACCOUNT_STATUS.Trim() == "ACTIVE" ? "Validated" : "Not Validated",
                        ReasonForChange = x.ReasonForChange + "",

                      }).ToList();

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

            Row headerRow = new Row();

            Cell cell0 = new Cell();
            cell0.DataType = CellValues.String;
            cell0.CellValue = new CellValue(" ApplicationReferenceNo");
            headerRow.AppendChild(cell0);

            Cell cell1 = new Cell();
            cell1.DataType = CellValues.String;
            cell1.CellValue = new CellValue("FirstName");
            headerRow.AppendChild(cell1);

            Cell cell2 = new Cell();
            cell2.DataType = CellValues.String;
            cell2.CellValue = new CellValue("Gender");
            headerRow.AppendChild(cell2);

            Cell cell3 = new Cell();
            cell3.DataType = CellValues.String;
            cell3.CellValue = new CellValue("AgeInYears");
            headerRow.AppendChild(cell3);

            Cell cell4 = new Cell();
            cell4.DataType = CellValues.String;
            cell4.CellValue = new CellValue("Address");
            headerRow.AppendChild(cell4);

            Cell cell5 = new Cell();
            cell5.DataType = CellValues.String;
            cell5.CellValue = new CellValue("Phone");
            headerRow.AppendChild(cell5);

            Cell cell6 = new Cell();
            cell6.DataType = CellValues.String;
            cell6.CellValue = new CellValue("mailid");
            headerRow.AppendChild(cell6);

            Cell cell7 = new Cell();
            cell7.DataType = CellValues.String;
            cell7.CellValue = new CellValue("BankAcctNo");
            headerRow.AppendChild(cell7);

            Cell cell8 = new Cell();
            cell8.DataType = CellValues.String;
            cell8.CellValue = new CellValue(" IFSCCode ");
            headerRow.AppendChild(cell8);

            Cell cell9 = new Cell();
            cell9.DataType = CellValues.String;
            cell9.CellValue = new CellValue("BankName");
            headerRow.AppendChild(cell9);

            Cell cell10 = new Cell();
            cell10.DataType = CellValues.String;
            cell10.CellValue = new CellValue("Account Status");
            headerRow.AppendChild(cell10);

            Cell cell11 = new Cell();
            cell11.DataType = CellValues.String;
            cell11.CellValue = new CellValue("Remarks/Reason");
            headerRow.AppendChild(cell11);

            sheetData.AppendChild(headerRow);

            foreach (var item in result)
            {
              Row dataRow = new Row();

              Cell cellR0 = new Cell();
              cellR0.DataType = CellValues.String;
              //cellR0.CellValue = new CellValue(item[0]);
              cellR0.CellValue = new CellValue(item.ApplicationReferenceNo);
              dataRow.AppendChild(cellR0);

              Cell cellR1 = new Cell();
              cellR1.DataType = CellValues.String;
              //cellR1.CellValue = new CellValue(item[1]);
              cellR1.CellValue = new CellValue(item.FirstName);
              dataRow.AppendChild(cellR1);

              Cell cellR2 = new Cell();
              cellR2.DataType = CellValues.String;
              //cellR2.CellValue = new CellValue(item[2]);
              cellR2.CellValue = new CellValue(item.Gender);
              dataRow.AppendChild(cellR2);

              Cell cellR3 = new Cell();
              cellR3.DataType = CellValues.String;
              //cellR3.CellValue = new CellValue(item[3]);
              cellR3.CellValue = new CellValue(item.AgeInYears_String);
              dataRow.AppendChild(cellR3);

              Cell cellR4 = new Cell();
              cellR4.DataType = CellValues.String;
              //cellR4.CellValue = new CellValue(item[4]);
              cellR4.CellValue = new CellValue(item.Address1);
              dataRow.AppendChild(cellR4);

              Cell cellR5 = new Cell();
              cellR5.DataType = CellValues.String;
              //cellR5.CellValue = new CellValue(item[5]);
              cellR5.CellValue = new CellValue(item.Phone);
              dataRow.AppendChild(cellR5);

              Cell cellR6 = new Cell();
              cellR6.DataType = CellValues.String;
              //cellR6.CellValue = new CellValue(item[6]);
              cellR6.CellValue = new CellValue(item.mailid);
              dataRow.AppendChild(cellR6);

              Cell cellR7 = new Cell();
              cellR7.DataType = CellValues.String;
              //cellR7.CellValue = new CellValue(item[7]);
              cellR7.CellValue = new CellValue(item.BankAcctNo);
              dataRow.AppendChild(cellR7);

              Cell cellR8 = new Cell();
              cellR8.DataType = CellValues.String;
              //cellR8.CellValue = new CellValue(item[8]);
              cellR8.CellValue = new CellValue(item.IFSCCode);
              dataRow.AppendChild(cellR8);

              Cell cellR9 = new Cell();
              cellR9.DataType = CellValues.String;
              //cellR9.CellValue = new CellValue(item[9]);
              cellR9.CellValue = new CellValue(item.BankName);
              dataRow.AppendChild(cellR9);

              Cell cellR10 = new Cell();
              cellR10.DataType = CellValues.String;
              //cellR10.CellValue = new CellValue(item[16]);
              cellR10.CellValue = new CellValue(item.ACCOUNT_STATUS);
              dataRow.AppendChild(cellR10);

              Cell cellR11 = new Cell { DataType = CellValues.String };
              // Split ReasonForChange by <br /> and join using a new line character for Excel
              if (!string.IsNullOrWhiteSpace(item.ReasonForChange))
              {
                string[] reasonForChangeArray = item.ReasonForChange.Split(new string[] { "<br />" }, StringSplitOptions.None);
                string reasonForChangeFormatted = string.Join("\n", reasonForChangeArray);
                cellR11.CellValue = new CellValue(reasonForChangeFormatted);
              }
              else
              {
                cellR11.CellValue = new CellValue("");
              }

              dataRow.AppendChild(cellR11);

              sheetData.AppendChild(dataRow);
            }
            workbookPart.Workbook.Save();
          }
          bytesdata = File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, "Beneficiary.xlsx");
        }
        return bytesdata;
      }
      catch (Exception ex)
      {

        throw;
      }
    }

    private FileContentResult PaymentHistoryReport(IEnumerable<dynamic> listSearch)
    {
      try
      {
        var result = from c in listSearch
                     select new[] {
                     c.AccountNo,
                     c.BankName,
                     c.IFSCCode,
                     c.PaidOn,
                     c.amount,
                     c.Status,
                     c.Reason,
                     c.ApplicationReferenceNo,
                     c.PresentAddress,
                     c.BankCode,
                     c.BranchName,
                     c.Name,
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

            Row headerRow = new Row();

            Cell cell0 = new Cell();
            cell0.DataType = CellValues.String;
            cell0.CellValue = new CellValue(" AccountNo");
            headerRow.AppendChild(cell0);

            Cell cell1 = new Cell();
            cell1.DataType = CellValues.String;
            cell1.CellValue = new CellValue("BankName");
            headerRow.AppendChild(cell1);

            Cell cell2 = new Cell();
            cell2.DataType = CellValues.String;
            cell2.CellValue = new CellValue("IFSCCode");
            headerRow.AppendChild(cell2);

            Cell cell3 = new Cell();
            cell3.DataType = CellValues.String;
            cell3.CellValue = new CellValue("PaidOn");
            headerRow.AppendChild(cell3);

            Cell cell4 = new Cell();
            cell4.DataType = CellValues.String;
            cell4.CellValue = new CellValue("amount");
            headerRow.AppendChild(cell4);

            Cell cell5 = new Cell();
            cell5.DataType = CellValues.String;
            cell5.CellValue = new CellValue("Status");
            headerRow.AppendChild(cell5);

            Cell cell6 = new Cell();
            cell6.DataType = CellValues.String;
            cell6.CellValue = new CellValue("Reason");
            headerRow.AppendChild(cell6);

            Cell cell7 = new Cell();
            cell7.DataType = CellValues.String;
            cell7.CellValue = new CellValue("ApplicationReferenceNo");
            headerRow.AppendChild(cell7);

            Cell cell8 = new Cell();
            cell8.DataType = CellValues.String;
            cell8.CellValue = new CellValue("PresentAddress");
            headerRow.AppendChild(cell8);

            Cell cell9 = new Cell();
            cell9.DataType = CellValues.String;
            cell9.CellValue = new CellValue("BankCode");
            headerRow.AppendChild(cell9);

            Cell cell10 = new Cell();
            cell10.DataType = CellValues.String;
            cell10.CellValue = new CellValue("BranchName");
            headerRow.AppendChild(cell10);

            Cell cell11 = new Cell();
            cell11.DataType = CellValues.String;
            cell11.CellValue = new CellValue("Name");
            headerRow.AppendChild(cell11);


            sheetData.AppendChild(headerRow);

            foreach (var item in result)
            {
              Row dataRow = new Row();

              Cell cellR0 = new Cell();
              cellR0.DataType = CellValues.String;
              cellR0.CellValue = new CellValue(item[0]);
              dataRow.AppendChild(cellR0);

              Cell cellR1 = new Cell();
              cellR1.DataType = CellValues.String;
              cellR1.CellValue = new CellValue(item[1]);
              dataRow.AppendChild(cellR1);

              Cell cellR2 = new Cell();
              cellR2.DataType = CellValues.String;
              cellR2.CellValue = new CellValue(item[2]);
              dataRow.AppendChild(cellR2);

              Cell cellR3 = new Cell();
              cellR3.DataType = CellValues.String;
              cellR3.CellValue = new CellValue(item[3]);
              dataRow.AppendChild(cellR3);

              Cell cellR4 = new Cell();
              cellR4.DataType = CellValues.String;
              cellR4.CellValue = new CellValue(item[4]);
              dataRow.AppendChild(cellR4);

              Cell cellR5 = new Cell();
              cellR5.DataType = CellValues.String;
              cellR5.CellValue = new CellValue(item[5]);
              dataRow.AppendChild(cellR5);

              Cell cellR6 = new Cell();
              cellR6.DataType = CellValues.String;
              cellR6.CellValue = new CellValue(item[6]);
              dataRow.AppendChild(cellR6);
              Cell cellR7 = new Cell();
              cellR7.DataType = CellValues.String;
              cellR7.CellValue = new CellValue(item[7]);
              dataRow.AppendChild(cellR7);

              Cell cellR8 = new Cell();
              cellR8.DataType = CellValues.String;
              cellR8.CellValue = new CellValue(item[8]);
              dataRow.AppendChild(cellR8);

              Cell cellR9 = new Cell();
              cellR9.DataType = CellValues.String;
              cellR9.CellValue = new CellValue(item[9]);
              dataRow.AppendChild(cellR9);

              Cell cellR10 = new Cell();
              cellR10.DataType = CellValues.String;
              cellR10.CellValue = new CellValue(item[10]);
              dataRow.AppendChild(cellR10);

              Cell cellR11 = new Cell();
              cellR11.DataType = CellValues.String;
              cellR11.CellValue = new CellValue(item[11]);
              dataRow.AppendChild(cellR11);

              sheetData.AppendChild(dataRow);
            }
            workbookPart.Workbook.Save();
          }
          bytesdata = File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, "Payment History Report.xlsx");
        }
        return bytesdata;
      }
      catch (Exception ex)
      {

        throw;
      }
    }


    //public ActionResult HoldPaymentStatus(string Id,string holdPaymentReason,string holdpaymentStatus)
    //{
    //  string id = UrlEncryption.Decrypt(Id);
    //  int Ids = Convert.ToInt32(id);
    //  var masterEmployee = db.MasterEmployees.FirstOrDefault(x => x.EmployeeID == Ids);
    //  if (masterEmployee != null)
    //  {
    //    try
    //    {
    //      masterEmployee.hold_pymnt = holdpaymentStatus;
    //      masterEmployee.ReasonForChange = holdPaymentReason;
    //      //    db.Entry(masterEmployee).State = EntityState.Modified;
    //      db.MasterEmployees.AddOrUpdate(masterEmployee);
    //      db.SaveChanges();
    //    }
    //    catch (Exception ex)
    //    {

    //      throw ;
    //    }    

    //  }
    //  else
    //    return Json(new { Message = "Data not found", Status = false });
    //  return Json(new {Message = "Data Updated Successfully.", Status = true });
    //}


    public ActionResult HoldPaymentStatusAjax(string Id, string holdPaymentReason, string holdpaymentStatus)
    {
      try
      {
        string id = UrlEncryption.Decrypt(Id);
        int Ids = Convert.ToInt32(id);
        holdPaymentReason = " [" + AppUserManager.GetUserName() + "] : " + holdPaymentReason + " <br />";
        //string connectionString = WebConfigurationManager.AppSettings["SQLConn"];
        string connectionString = ConnectionStringProvider.GetConnectionString();
        string updateSql = "UPDATE MasterEmployee SET hold_pymnt = @HoldPaymentStatus, ReasonForChange =  CONCAT(ReasonForChange, (CASE WHEN ReasonForChange = NULL OR ReasonForChange = '' THEN '' ELSE '' END), NCHAR(8226), ' [', FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm'), '] ', @HoldPaymentReason) WHERE EmployeeID = @EmployeeID";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          connection.Open();
          using (SqlCommand cmd = new SqlCommand(updateSql, connection))
          {
            cmd.Parameters.AddWithValue("@HoldPaymentStatus", holdpaymentStatus);
            cmd.Parameters.AddWithValue("@HoldPaymentReason", holdPaymentReason);
            cmd.Parameters.AddWithValue("@EmployeeID", Ids);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
              return Json(new { Message = "Data Updated Successfully.", Status = true });
            }
            else
            {
              return Json(new { Message = "Data not found or no changes made.", Status = false });
            }
          }
        }
      }
      catch (Exception ex)
      {
        return Json(new { Message = "An error occurred while updating data.", Status = false });
      }
    }


    ////stop

    public ActionResult StopPaymentStatusAjax(string Id, string stopPaymentReason, string stoppaymentStatus)
    {
      try
      {
        string id = UrlEncryption.Decrypt(Id);
        int Ids = Convert.ToInt32(id);

        //string connectionString = WebConfigurationManager.AppSettings["SQLConn"];
        string connectionString = ConnectionStringProvider.GetConnectionString();
        string updateSql = "UPDATE MasterEmployee SET hold_pymnt = @StopPaymentStatus, ReasonForChange = @StopPaymentReason WHERE EmployeeID = @EmployeeID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          connection.Open();

          using (SqlCommand cmd = new SqlCommand(updateSql, connection))
          {
            cmd.Parameters.AddWithValue("@StopPaymentStatus", stoppaymentStatus);
            cmd.Parameters.AddWithValue("@StopPaymentReason", stopPaymentReason);
            cmd.Parameters.AddWithValue("@EmployeeID", Ids);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
              return Json(new { Message = "Data Updated Successfully.", Status = true });
            }
            else
            {
              return Json(new { Message = "Data not found or no changes made.", Status = false });
            }
          }
        }
      }
      catch (Exception ex)
      {
        return Json(new { Message = "An error occurred while updating data.", Status = false });
      }
    }


    //Resume

    public ActionResult ResumePaymentStatusAjax(string Id, string resumePaymentReason, string resumepaymentStatus)
    {
      try
      {
        string id = UrlEncryption.Decrypt(Id);
        int Ids = Convert.ToInt32(id);
        resumePaymentReason = " [" + AppUserManager.GetUserName() + "] : " + resumePaymentReason + " <br />";

        //string connectionString = WebConfigurationManager.AppSettings["SQLConn"];
        string connectionString = ConnectionStringProvider.GetConnectionString();

        string updateSql = "UPDATE MasterEmployee SET hold_pymnt = @ResumePaymentStatus, ReasonForChange = CONCAT(ReasonForChange, (CASE WHEN ReasonForChange = NULL OR ReasonForChange = '' THEN '' ELSE '' END), NCHAR(8226), ' [', FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm'), '] ', @ResumePaymentReason) WHERE EmployeeID = @EmployeeID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          connection.Open();

          using (SqlCommand cmd = new SqlCommand(updateSql, connection))
          {
            cmd.Parameters.AddWithValue("@ResumePaymentStatus", resumepaymentStatus);
            cmd.Parameters.AddWithValue("@ResumePaymentReason", resumePaymentReason);
            cmd.Parameters.AddWithValue("@EmployeeID", Ids);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
              return Json(new { Message = "Data Updated Successfully.", Status = true });
            }
            else
            {
              return Json(new { Message = "Data not found or no changes made.", Status = false });
            }
          }
        }
      }
      catch (Exception ex)
      {
        return Json(new { Message = "An error occurred while updating data.", Status = false });
      }
    }

    //User Log

    ///History income index history
    public JsonResult MasterPensionerHistoryAjax(JQueryDataTableParamModel param, string Email = "", string EmplCode = "")
    {
      try
      {


        DataSet ds = new DataSet();
        //DataTable ds = new DataTable();ist
        StringBuilder SQL = new StringBuilder();
        //var User = SQL.Append("select Id,Email from AppUser where Email==Email");
        SQL.Append("SELECT  AUC.Username as CreatedBy, A.CreatedOn, AUM.Username as ModifiedBy, A.ModifiedOn, A.EventType, A.TableName," +
          "  A.ColumnName, A.OldValue, A.NewValue, A.Url, A.Controller,A.Action, A.Area, A.IPAddress, A.RecordId, A.OldSystemId, A.Message FROM AuditLogs A   ");
        SQL.Append(" INNER JOIN AppUser AUC WITH (NOLOCK) ON AUC.Id= A.CreatedBy  ");
        SQL.Append(" LEFT JOIN  AppUser AUM WITH (NOLOCK) ON  AUM.Id  =A.ModifiedBy   ");
        SQL.Append(" Where  A.Oldvalue != A.NewValue");
        SQL.Append(" AND A.TableName = 'MasterEmployeeIncomes'");
        //SQL.Append(" AND A.E = 'MasterEmployeeIncomes'");
        SQL.Append(" AND A.RecordId = '" + EmplCode + "'  order by A.ModifiedOn desc ");

        //string con = WebConfigurationManager.AppSettings["SQLConn"];
        string con = ConnectionStringProvider.GetConnectionString();
        SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
        da.Fill(ds);

        List<AuditLogs> LogsList = new List<AuditLogs>();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          AuditLogs AuditLogs = new AuditLogs();

          AuditLogs.UsernameCrby = (dr[0] != DBNull.Value ? (dr[0]).ToString() : "");
          if (dr[1] != DBNull.Value && dr[1].ToString().Trim() != string.Empty)
            AuditLogs.CreatedOn = Convert.ToDateTime(dr[1]);
          AuditLogs.UsernameModby = (dr[2] != DBNull.Value ? (dr[2]).ToString() : "");
          if (dr[3] != DBNull.Value && dr[3].ToString().Trim() != string.Empty)
            AuditLogs.ModifiedOn = Convert.ToDateTime(dr[3]);
          //AuditLogs.ModifiedOn = Convert.ToDateTime(dr[3]);
          AuditLogs.EventType = (dr[4] != DBNull.Value ? (dr[4]).ToString() : "");
          AuditLogs.TableName = (dr[5] != DBNull.Value ? (dr[5]).ToString() : "");
          AuditLogs.ColumnName = (dr[6] != DBNull.Value ? (dr[6]).ToString() : "");
          AuditLogs.OldValue = (dr[7] != DBNull.Value ? (dr[7]).ToString() : "");
          AuditLogs.NewValue = (dr[8] != DBNull.Value ? (dr[8]).ToString() : "");
          //AuditLogs.Url = (dr[9] != DBNull.Value ? (dr[9]).ToString() : "");
          //AuditLogs.Controller = (dr[10] != DBNull.Value ? (dr[10]).ToString() : "");
          //AuditLogs.Action = (dr[11] != DBNull.Value ? (dr[11]).ToString() : "");
          //AuditLogs.Area = (dr[12] != DBNull.Value ? (dr[12]).ToString() : "");
          //AuditLogs.IPAddress = (dr[13] != DBNull.Value ? (dr[13]).ToString() : "");
          //AuditLogs.RecordId = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);
          //AuditLogs.OldSystemId = (dr[15] != DBNull.Value ? Convert.ToInt32(dr[15]) : 0);
          //AuditLogs.Message = (dr[16] != DBNull.Value ? (dr[16]).ToString() : "");

          LogsList.Add(AuditLogs);
        }

        IEnumerable<AuditLogs> filtered;
        if (!string.IsNullOrEmpty(param.sSearch))
        {
          filtered = LogsList
             //.Where(c => c.Id.ToString().Contains(param.sSearch.ToLower())
             .Where(c => c.UsernameCrby.ToString().Contains(param.sSearch.ToLower())
             || c.CreatedOn.ToString().Contains(param.sSearch.ToLower())
             || c.UsernameModby.ToString().Contains(param.sSearch.ToLower())
             || c.ModifiedOn.ToString().Contains(param.sSearch.ToLower())
             //|| c.IsActive.ToString().Contains(param.sSearch.ToLower())
             || c.EventType.ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
             || c.TableName.ToLower().Contains(param.sSearch.ToLower())
             || c.ColumnName.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.OldValue.ToLower().Contains(param.sSearch.ToLower())
             || c.NewValue.ToLower().Contains(param.sSearch.ToLower()));
          //|| c.Url.ToLower().Contains(param.sSearch.ToLower())
          //|| c.Controller.ToLower().Contains(param.sSearch.ToLower())
          //|| c.Action.ToLower().Contains(param.sSearch.ToLower())
          //|| c.Area.ToLower().Contains(param.sSearch.ToLower())
          //|| c.IPAddress.ToLower().Contains(param.sSearch.ToLower())
          //|| c.RecordId.ToString().Contains(param.sSearch.ToLower())
          //|| c.OldSystemId.ToString().Contains(param.sSearch.ToLower())
          //|| c.Message.ToString().Contains(param.sSearch.ToLower()));
        }
        else
        {
          filtered = LogsList;
        }

        //Sorting through column index
        var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

        Func<AuditLogs, string> orderingFunction = (c => sortColumnIndex == 0 ? c.UsernameModby + "" :
                                                                                        sortColumnIndex == 1 ? c.ModifiedOn + "" :
                                                                                        sortColumnIndex == 2 ? c.ColumnName + "" :
                                                                                        sortColumnIndex == 3 ? c.OldValue :
                                                                                        sortColumnIndex == 4 ? c.NewValue + "" :
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

                         //c.Id+"",
                         //c.UsernameCrby + "",
                         //c.CreatedOn + "",
                         c.UsernameModby + "",
                         c.ModifiedOn + "", 
                         //c.IsActive +"",
                         //c.EventType,
                         //c.TableName,
                         c.ColumnName,
                         TrimStart(c.OldValue, "<br />") + "",
                         TrimStart(c.NewValue, "<br />") + "",
                         //c.Url,
                         //c.Controller,
                         //c.Action,
                         //c.Area + "",
                         //c.IPAddress,
                         //c.RecordId + "",
                         //c.OldSystemId + "",
                         //c.Message
                   };

        return Json(
                                    new
                                    {
                                      sEcho = param.sEcho,
                                      iTotalRecords = LogsList.Count(),
                                      iTotalDisplayRecords = filtered.Count(),
                                      aaData = result
                                    }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {

        throw;
      }

    }

    //this can be managed in this ajax MasterPensionerHistoryAjax 

    public JsonResult MasterBeneficiaryHistoryAjax(JQueryDataTableParamModel param, string EmplCode = "")
    {
      DataSet ds = new DataSet();
      //DataTable ds = new DataTable();ist  
      StringBuilder SQL = new StringBuilder();
      //var User = SQL.Append("select Id,Email from AppUser where Email==Email");
      SQL.Append("SELECT CONCAT(USD.FirstName , ' ' , USD.MiddleName , ' ' , USD.Lastname) as update_by, A.update_date, A.field_name, A.old_value, A.new_value FROM EmployeeUpdLog A ");
      SQL.Append(" INNER JOIN UserProfiles USD WITH (NOLOCK) ON USD.UserId= A.update_by ");
      //SQL.Append(" LEFT JOIN  AppUser AUM WITH (NOLOCK) ON  AUM.Id  =A.ModifiedBy   ");
      SQL.Append(" Where A.empl_code = '" + EmplCode + "' AND A.old_value IS NOT NULL AND A.old_value != '' AND A.new_value IS NOT NULL AND A.new_value != '' order by A.update_date desc");
      //SQL.Append(" AND A.TableName = 'MasterBeneficiariesDetails'");
      //SQL.Append(" AND A.E = 'MasterBeneficiariesDetails'");
      //SQL.Append(" AND A.RecordId = '" + EmplCode + "'");

      //string con = WebConfigurationManager.AppSettings["SQLConn"];
      string con = ConnectionStringProvider.GetConnectionString();
      SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
      da.Fill(ds);

      List<DVOEmployeeInfoLogEmpUpdLog> LogsList = new List<DVOEmployeeInfoLogEmpUpdLog>();
      foreach (DataRow dr in ds.Tables[0].Rows)
      {
        DVOEmployeeInfoLogEmpUpdLog AuditLogs = new DVOEmployeeInfoLogEmpUpdLog();

        //if (dr[0] != DBNull.Value && dr[0].ToString().Trim() != string.Empty)
        //    AuditLogs.update_by = ;

        // empl_code use for show name only updated by
        AuditLogs.empl_code = dr[0] != DBNull.Value ? dr[0].ToString() : "";

        AuditLogs.update_date = (dr[1] != DBNull.Value ? (dr[1]).ToString() : "");

        AuditLogs.field_name = (dr[2] != DBNull.Value ? (dr[2]).ToString() : "");

        AuditLogs.old_value = (dr[3] != DBNull.Value ? (dr[3]).ToString() : "");

        AuditLogs.new_value = (dr[4] != DBNull.Value ? (dr[4]).ToString() : "");


        //AuditLogs.ColumnName = (dr[6] != DBNull.Value ? (dr[6]).ToString() : "");
        //AuditLogs.OldValue = (dr[7] != DBNull.Value ? (dr[7]).ToString() : "");
        //AuditLogs.NewValue = (dr[8] != DBNull.Value ? (dr[8]).ToString() : "");
        //AuditLogs.Url = (dr[9] != DBNull.Value ? (dr[9]).ToString() : "");
        //AuditLogs.Controller = (dr[10] != DBNull.Value ? (dr[10]).ToString() : "");
        //AuditLogs.Action = (dr[11] != DBNull.Value ? (dr[11]).ToString() : "");
        //AuditLogs.Area = (dr[12] != DBNull.Value ? (dr[12]).ToString() : "");
        //AuditLogs.IPAddress = (dr[13] != DBNull.Value ? (dr[13]).ToString() : "");
        //AuditLogs.RecordId = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);
        //AuditLogs.OldSystemId = (dr[15] != DBNull.Value ? Convert.ToInt32(dr[15]) : 0);
        //AuditLogs.Message = (dr[16] != DBNull.Value ? (dr[16]).ToString() : "");

        LogsList.Add(AuditLogs);
      }

      IEnumerable<DVOEmployeeInfoLogEmpUpdLog> filtered;
      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = LogsList
           .Where(c => c.empl_code.ToString().Contains(param.sSearch.ToLower())
           || c.update_date.ToString().Contains(param.sSearch.ToLower())
           || c.field_name.ToString().Contains(param.sSearch.ToLower())
           || c.old_value.ToString().Contains(param.sSearch.ToLower())
           || c.new_value.ToLower().Contains(param.sSearch.Replace(" ", "").ToLower()));

        //|| c.TableName.ToLower().Contains(param.sSearch.ToLower())
        //|| c.ColumnName.ToString().ToLower().Contains(param.sSearch.ToLower())
        //|| c.OldValue.ToLower().Contains(param.sSearch.ToLower())
        //|| c.NewValue.ToLower().Contains(param.sSearch.ToLower()));
        //|| c.Url.ToLower().Contains(param.sSearch.ToLower())
        //|| c.Controller.ToLower().Contains(param.sSearch.ToLower())
        //|| c.Action.ToLower().Contains(param.sSearch.ToLower())
        //|| c.Area.ToLower().Contains(param.sSearch.ToLower())
        //|| c.IPAddress.ToLower().Contains(param.sSearch.ToLower())
        //|| c.RecordId.ToString().Contains(param.sSearch.ToLower())
        //|| c.OldSystemId.ToString().Contains(param.sSearch.ToLower())
        //|| c.Message.ToString().Contains(param.sSearch.ToLower()));
      }
      else
      {
        filtered = LogsList;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);


      Func<DVOEmployeeInfoLogEmpUpdLog, string> orderingFunction = (c =>
                                                                                      sortColumnIndex == 0 ? c.empl_code + "" :
                                                                                      sortColumnIndex == 1 ? c.update_date + "" :
                                                                                      sortColumnIndex == 2 ? c.field_name + "" :
                                                                                      //sortColumnIndex == 5 ? c.IsActive + "" :
                                                                                      sortColumnIndex == 3 ? c.old_value + "" :
                                                                                      sortColumnIndex == 4 ? c.new_value :
                                                                                      //sortColumnIndex == 8 ? c.ColumnName + "" :
                                                                                      //sortColumnIndex == 9 ? c.OldValue :
                                                                                      //sortColumnIndex == 10 ? c.NewValue + "" :
                                                                                      //sortColumnIndex == 11 ? c.Url + "" :
                                                                                      //sortColumnIndex == 12 ? c.Controller + "" :
                                                                                      //sortColumnIndex == 13 ? c.Action + "" :
                                                                                      //sortColumnIndex == 14 ? c.Area + "" :
                                                                                      //sortColumnIndex == 15 ? c.IPAddress + "" :
                                                                                      //sortColumnIndex == 16 ? c.RecordId + "" :
                                                                                      //sortColumnIndex == 17 ? c.OldSystemId + "" :
                                                                                      //sortColumnIndex == 18 ? c.Message + "" :
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

                         //c.Id+"",
                         c.empl_code + "",
                         c.update_date + "",
                         c.field_name + "",
                         TrimStart(c.old_value, "<br />") + "",
                         TrimStart(c.new_value, "<br />") +"",
                         //c.EventType,
                         //c.TableName,
                         //c.ColumnName,
                         //c.OldValue,
                         //c.NewValue,
                         //c.Url,
                         //c.Controller,
                         //c.Action,
                         //c.Area + "",
                         //c.IPAddress,
                         //c.RecordId + "",
                         //c.OldSystemId + "",
                         //c.Message
                   };

      return Json(
                                  new
                                  {
                                    sEcho = param.sEcho,
                                    iTotalRecords = LogsList.Count(),
                                    iTotalDisplayRecords = filtered.Count(),
                                    aaData = result
                                  }, JsonRequestBehavior.AllowGet);

    }

    public JsonResult GetPaymentdetailsAjax(string EmplCode, string PayDocNo)
    {
      try
      {
        //string conString = WebConfigurationManager.AppSettings["SQLConn"];
        string conString = ConnectionStringProvider.GetConnectionString();
        using (SqlConnection con = new SqlConnection(conString))
        {
          con.Open();
          using (SqlCommand cmd = new SqlCommand())
          {
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT " +
                              "    a.Empl_Code, " +
                              "    a.ApplicationReferenceNo, " +
                              "    a.first_name + ' ' + a.last_name AS FullName, " +
                               //"    a.bank_acct_no AS AccountNo, " +
                               "    b.[TransactionRefrenceNo.] AS TransactionRefrenceNo ," +
                                "   b.TransactionDate AS TransactionDate, " +
                                "    b.[Reason/Remarks] AS Remarks," +
                                "    b.Status AS Status, " +
                                "    b.pay_doc_no AS PayDocNo " +
                              "FROM " +
                              "    MasterEmployee a " +
                              "JOIN " +
                              "    Process_DirectDeposit_Details b ON a.Empl_Code = b.Empl_Code " +
                              "WHERE " +
                              "    b.Empl_Code = @EmplCode AND b.pay_doc_no = @pay_doc_no";

            cmd.Parameters.AddWithValue("@EmplCode", EmplCode);
            cmd.Parameters.AddWithValue("@pay_doc_no", PayDocNo);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
              System.Data.DataTable dt = new System.Data.DataTable();
              da.Fill(dt);

              var result = dt.AsEnumerable().Select(row => new
              {
                Empl_Code = row["Empl_Code"],
                ApplicationReferenceNo = row["ApplicationReferenceNo"],
                FullName = row["FullName"],
                //AccountNo = row["AccountNo"],
                TransactionRefrenceNo = row["TransactionRefrenceNo"],
                TransactionDate = row["TransactionDate"] == DBNull.Value ? "" : row["TransactionDate"],
                Remarks = row["Remarks"],
                Status = row["Status"],
                PayDocNo = row["PayDocNo"]
              }).Distinct().ToList();

              return Json(result, JsonRequestBehavior.AllowGet);
            }
          }
        }
      }
      catch (Exception ex)
      {
        return Json(new { error = "An error occurred while fetching data." });
      }
    }


    [HttpPost]
    public JsonResult UpdatePaymentStatusAjax(string Remarks, DateTime? TransactionDates, string paymentStatuses, string emplcodes, string ApplicationReferenceNo, string accountNos, string TransactionNos, string PayDocNo)
    {
      try
      {
        //string connectionString = WebConfigurationManager.AppSettings["SQLConn"];
        string connectionString = ConnectionStringProvider.GetConnectionString();
        // var data1 = db.MasterEmployees.FirstOrDefault(x => x.Empl_Code == emplcodes);
        //string sql = "UPDATE Process_DirectDeposit_Details SET Status = @status, TransactionDate = @transactionDate, [Reason/Remarks] = @remarks WHERE Empl_Code = " + emplcodes + " and bank_acct_no = '" + accountNos + "'";// and bank_acct_no = " + accountNos + "
        //string sql = "UPDATE Process_DirectDeposit_Details SET Status = @status, TransactionRefrenceNo=@TransactionRefrenceNo, TransactionDate = @transactionDate, [Reason/Remarks] = @remarks WHERE Empl_Code = " + emplcodes + " and bank_acct_no = '" + accountNos + "'";// and bank_acct_no = " + accountNos + "
        string sql = "UPDATE Process_DirectDeposit_Details SET Status = @status,[TransactionRefrenceNo.]=@TransactionRefrenceNo, TransactionDate = @transactionDate, [Reason/Remarks] = @remarks WHERE Empl_Code = " + emplcodes + " and pay_doc_no = '" + PayDocNo + "'";// and bank_acct_no = " + accountNos + "
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          connection.Open();

          using (SqlCommand cmd = new SqlCommand(sql, connection))
          {
            cmd.Parameters.AddWithValue("@status", paymentStatuses);
            cmd.Parameters.AddWithValue("@TransactionDate", TransactionDates ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@remarks", Remarks);
            cmd.Parameters.AddWithValue("@TransactionRefrenceNo", TransactionNos);
            //cmd.Parameters.AddWithValue("@emplCode", emplcodes); //Empl_Code = @emplCode";
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
              return Json(new { Message = "Data Updated Successfully.", Status = true });
            }
            else
            {
              return Json(new { Message = "Data not found or no changes made.", Status = false });
            }
          }
        }
      }
      catch (Exception ex)
      {
        return Json(new { Message = "An error occurred while updating data.", Status = false });
      }

    }


    //PaymentHistory
    public ActionResult PaymentHistoryAjax(string Id)
    {
      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();

      List<SessionViewModel> results = (List<SessionViewModel>)HttpContext.Session["List"];
      var model = results.Where(x => x.ControllerName == "MasterPensioner" && x.ActionName == "Edit").FirstOrDefault();
      if (model == null)
      {
        model = results.Where(x => x.ControllerName == "MasterPensioner").FirstOrDefault();
      }

      if (model != null)
      {
        ViewBag.AddPermission = model.AddPermssion;
        ViewBag.EditPermission = model.EditPermission;
        ViewBag.ViewPermission = model.ViewPermission;
      }

      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(Id));
      @ViewBag.CId = Id;// IdUrl;
      DVOMasterEmployee objDVOMasterEmployee = SearchAgainEmployeeInfo(IdUrl);
      ViewBag.PensionerID = objDVOMasterEmployee.EmplCode;
      ViewBag.ApplicationReferenceNo = objDVOMasterEmployee.ApplicationReferenceNo;
      ViewBag.TypeCode = db.MasterEmpType.FirstOrDefault(x => x.Type_Code.ToLower().Trim() == objDVOMasterEmployee.TypeCode.ToLower().Trim())?.Description;
      ViewBag.Name = String.Format("{0} {1} {2} {3} {4}", objDVOMasterEmployee.Prefix, objDVOMasterEmployee.Suffix, objDVOMasterEmployee.FirstName, objDVOMasterEmployee.MiddleName, objDVOMasterEmployee.LastName);
      //int userid = AppUserManager.GetUserId();
      ViewBag.roleidlist = db.UserRole.Where(x => x.UserId == userid).FirstOrDefault().RoleId;

      @ViewBag.CId = @ViewBag.CId == null ? string.Empty : @ViewBag.CId;
      ViewBag.PensionerID = ViewBag.PensionerID == null ? string.Empty : @ViewBag.PensionerID;
      ViewBag.ApplicationReferenceNo = ViewBag.ApplicationReferenceNo == null ? string.Empty : @ViewBag.ApplicationReferenceNo;
      ViewBag.TypeCode = ViewBag.TypeCode == null ? string.Empty : @ViewBag.TypeCode;
      ViewBag.Name = ViewBag.Name == null ? string.Empty : @ViewBag.Name;

      return View("~/Views/Shared/_PaymentHistory.cshtml");
    }


    public ActionResult DuplicateAccounts(string Tehsil = "", string BeneficiariesType = "", string Gender = "", string RegionNames = "", string AccountStatus = "", string bank_acct_no = "", string APPLICATION_REFERENCE_NO = "")
    {
      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "MasterPensioner" && x.ActionName == "DuplicateAccounts")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Duplicate Accounts", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.AddPermission = model.AddPermssion;
        ViewBag.EditPermission = model.EditPermission;
        ViewBag.DeletePermission = model.DeletePermission;

      }
      try
      {
        List<DVOMasterEmployee> listSearch = new List<DVOMasterEmployee>();
        //listSearch = BLLMasterEmployee.GetAllData(Tehsil, BeneficiariesType, Gender, RegionNames);
        ViewBag.Group = GetUsersAssignedLocations();
        PensionProcessViewModel Paysearch = ShowActiveBatch();
        DVOMasterEmpTypes objDVOMasterEmpTypes = new DVOMasterEmpTypes();
        //call getDate function of BLL
        var defaultItem = new SelectListItem { Value = "ALL", Text = "ALL" };

        List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes)
                   .Select(s => new DVOMasterEmpTypes
                   {
                     type_code = s.type_code,
                     description = string.Format("{0} | {1}", s.type_code, s.description)
                   })
                   .ToList();

        listDVOMasterEmpTypes.Insert(0, new DVOMasterEmpTypes { type_code = "ALL", description = "ALL | ALL" }); // Insert the default item at the beginning

        var selectListItems = listDVOMasterEmpTypes
            .Select(empType => new SelectListItem
            {
              Value = empType.type_code,
              Text = empType.description
            })
            .ToList();
        objDVOMasterEmpTypes = null;
        ViewBag.BeneficiariesType = new SelectList(selectListItems, "Value", "Text", (string.IsNullOrEmpty(BeneficiariesType) ? null : BeneficiariesType));
        //List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes).Select(s => new DVOMasterEmpTypes
        //{
        //    type_code = s.type_code,
        //    //description = string.Format("{0} | {1} | {2} | {3} | {4}", s.type_code, s.description, s.pay_period, s.empl_status, s.hold_pymnt)
        //    description = string.Format("{0} | {1}", s.type_code, s.description)
        //}).ToList();

        //objDVOMasterEmpTypes = null;
        //ViewBag.BeneficiariesType = new SelectList(listDVOMasterEmpTypes, "type_code", "description", (string.IsNullOrEmpty(BeneficiariesType) ? BeneficiariesType : ""));
        ViewBag.ApplicantIFSCCode = new SelectList(db.MasterEmpBankDetails.GroupBy(x => x.APPLICANT_BANK_IFSC_CODE).Select(x => new { APPLICANT_BANK_IFSC_CODE = x.Key }).ToList(), "APPLICANT_BANK_IFSC_CODE", "APPLICANT_BANK_IFSC_CODE", string.Empty);
        // Controller code to populate ViewBag.bank_acct_no
        //ViewBag.bank_acct_no = new SelectList(db.MasterEmpBankDetails  .Where(x => x.bank_acct_no != "")
        //    .OrderBy(x => x.bank_acct_no), "Id", "bank_acct_no", model.bank_acct_no);

        //ViewBag.bank_acct_no = db.MasterEmpBankDetails.Where(x => x.bank_acct_no != null).Select(x => new SelectListItem
        //{
        //    Value = x.bank_acct_no,
        //    Text = x.bank_acct_no
        //}).ToList();

        //ViewBag.APPLICATION_REFERENCE_NO = db.MasterEmpBankDetails.Where(x => x.bank_acct_no != null).Select(x => new SelectListItem
        //{
        //    Value = x.APPLICATION_REFERENCE_NO,
        //    Text = x.APPLICATION_REFERENCE_NO
        //}).ToList();

        // Assuming MasterEmpBankDetails has a property named ACCOUNT_STATUS
        //var accountStatusList = db.MasterEmpBankDetails
        //    .Where(x => x.ACCOUNT_STATUS != null)
        //    .Select(x => x.ACCOUNT_STATUS.ToString())
        //    .Distinct()
        //    .Select(status => new SelectListItem
        //    {
        //        Value = status,
        //        Text = status
        //    })
        //    .ToList();

        //accountStatusList.Insert(0, new SelectListItem { Value = "ALL", Text = "ALL" }); // Insert the default item at the beginning

        //ViewBag.AccountStatus = new SelectList(accountStatusList, "Value", "Text", (string.IsNullOrEmpty(AccountStatus) ? null : AccountStatus));
        //return View(listSearch);
        return View();
      }
      catch (Exception ex)
      {
        throw;
      }

    }


    public async Task<ActionResult> DuplicateAccountsAjaxHandler(JQueryDataTableParamModel param, string Tehsil = "", string BeneficiariesType = "", string Gender = "", string RegionNames = "", bool isDownload = false, string AccountStatus = "", string bank_acct_no = "", string APPLICATION_REFERENCE_NO = "")
    {
      try
      {
        List<DVOMasterEmployee> listSearch = new List<DVOMasterEmployee>();


        if (isDownload)
        {
          param.iDisplayLength = 1000000;
        }
        int UserId = AppUserManager.GetUserId();
        int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
        if (AccountStatus == "ALL")
          AccountStatus = null;

        listSearch = BLLMasterEmployee.GetAllDuplicateAccountsAcrossDatabases(UserId, RoleId, Tehsil, BeneficiariesType == "ALL" ? "" : BeneficiariesType, Gender, RegionNames, param.iDisplayStart, param.iDisplayLength, AccountStatus, bank_acct_no, APPLICATION_REFERENCE_NO, "", true);
        //listSearch = BLLMasterEmployee.GetAllData(UserId, RoleId, Tehsil, BeneficiariesType == "ALL" ? "" : BeneficiariesType, Gender, RegionNames, param.iDisplayStart, param.iDisplayLength, AccountStatus, bank_acct_no, APPLICATION_REFERENCE_NO);

        // Apply search

        if (!string.IsNullOrEmpty(param.sSearch))
        {
          //listSearch = listSearch
          //    .Where(c => c.ApplicationReferenceNo.ToLower().Contains(param.sSearch.ToLower())
          //        || c.FirstName.ToLower().Contains(param.sSearch.ToLower())
          //        || c.mailid.ToLower().Contains(param.sSearch.ToLower())
          //        //|| c.AccountNooftheApplicant.ToString().ToLower().Contains(param.sSearch.ToLower())
          //        //|| c.Phone.ToString().ToLower().Contains(param.sSearch.ToLower())
          //        || c.Address1.ToString().ToLower().Contains(param.sSearch.ToLower())
          //        || c.AgeInYears.ToString().ToLower().Contains(param.sSearch.ToLower())
          //        //|| c.IFSCCode.ToString().ToLower().Contains(param.sSearch.ToLower())
          //        || c.HoldPayment.ToString().ToLower().Contains(param.sSearch.ToLower())
          //        )
          //    .ToList();
          listSearch = listSearch
              .Where(c =>
                    (c.ApplicationReferenceNo ?? "").ToLower().Contains(param.sSearch.ToLower()) ||
                    (c.FirstName ?? "").ToLower().Contains(param.sSearch.ToLower()) ||
                    (c.mailid ?? "").ToLower().Contains(param.sSearch.ToLower()) ||
                    (c.Address1 ?? "").ToLower().Contains(param.sSearch.ToLower()) ||
                    (c.HoldPayment ?? "").ToLower().Contains(param.sSearch.ToLower())
              )
              .ToList();
        }

        var duplicatedccountRecord = Enumerable.Empty<DVOMasterEmployee>();
        if (string.IsNullOrEmpty(APPLICATION_REFERENCE_NO))
        {
          if (string.IsNullOrEmpty(param.sSearch))
          {
            //duplicatedccountRecord = listSearch.GroupBy(x => x.BankAcctNo).Where(group => group.Count() > 1).SelectMany(group => group);
            duplicatedccountRecord = listSearch;
          }
          else
          {
            duplicatedccountRecord = listSearch.Where(x => x.GetType().GetProperties().Any(prop => prop.GetValue(x)?.ToString().Contains(param.sSearch) == true));
            //duplicatedccountRecord = filteredList.GroupBy(x => x.BankAcctNo).Where(group => group.Count() > 1).SelectMany(group => group);
          }
        }
        else
        {
          duplicatedccountRecord = listSearch;
        }
        if (isDownload)
        {
          //var duplicateexedownload = listSearch.GroupBy(x => x.BankAcctNo).Where(group => group.Count() > 1).SelectMany(group => group);
          var duplicateexedownload = duplicatedccountRecord;
          FileContentResult bytesdata = JandKBeneficiaryReport(duplicateexedownload.ToList());
          return bytesdata;
          //return Json(bytesdata.FileContents, JsonRequestBehavior.AllowGet);
          //return bytesdata;
          //var base64data = Convert.ToBase64String(bytesdata.FileContents);
          //return Json(new { Filename = "Beneficiary.xlsx", Base64Data = base64data }, JsonRequestBehavior.AllowGet);
        }


        // Finding duplicate BankAcctNo records in the filtered list
        //var duplicatedccountRecord = filteredList.GroupBy(x => x.BankAcctNo).Where(group => group.Count() > 1).SelectMany(group => group);

        Int32 totalRecords = listSearch.Count > 0 ? Convert.ToInt32(listSearch.Select(x => x.recordCount).FirstOrDefault()) : 0;

        var result = from x in duplicatedccountRecord
                     select new[]
                     {
                        x.ApplicationReferenceNo + "",//0
                        x.FirstName + "",//1
                        x.Gender + "",//2
                        x.AgeInYears + "",//3
                        x.Address1,//4
                        x.Phone,//5
                        x.mailid,//6
                        x.City.ToUpper(),//7
                        x.IFSCCode,//8
                        x.BankName,//9
                        x.BankAcctNo,//x.LastVerified,//10
                        x.TypeCode,//11
                        x.MiddleName + "",//12
                        x.LastName + "",//13
                        x.NameoftheApplicant + "", //  14                    
                        // x.HoldPayment+"",
                        //x.ACCOUNT_STATUS+"",//15                        
                        x.ACCOUNT_STATUS.Trim() == "ACTIVE" ? "Validate" : "Not Validate",
                        //x.ACCOUNT_STATUS != "Active" ? "Validate" : "Not Validate",
                        TrimStart(x.ReasonForChange,"")+"",//16
                        x.HoldPayment+"",//17
                        UrlEncryption.EncryptURL(Convert.ToString(x.EmplCode)),//18
                        x.ACCOUNT_STATUS.Trim() == "ACTIVE" ? "Validate" : "Not Validate",//19
                     };
        return Json(
            new
            {
              sEcho = param.sEcho,
              //iTotalRecords = listSearch.Count(),
              //iTotalDisplayRecords = listSearch.Count(),
              //iTotalRecords = duplicatedccountRecord.Count(),
              iTotalRecords = totalRecords,
              iTotalDisplayRecords = totalRecords,
              aaData = result,
            }, JsonRequestBehavior.AllowGet);

      }
      catch (Exception ex)
      {
        return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
      }
    }




    public JsonResult GetPresentMunicipalityAjax(string term)
    {
      try
      {
        if (string.IsNullOrEmpty(term))
        {
          return Json(new { status = false, message = "Invalid input" });
        }

        // Using StartsWith to filter the names starting with Searchtext
        var query = (from md in db.MasterBeneficiariesDetails
                     join Ms in db.MasterEmployees on md.Id equals Ms.EmployeeID into Ms1
                     from Ms in Ms1.DefaultIfEmpty()
                     where md.IsActive && Ms.PresentHalqaPanchayatOrMunicipalityName.StartsWith(term)
                     select new
                     {
                       Key = Ms.EmployeeID,
                       Value = Ms.PresentHalqaPanchayatOrMunicipalityName,
                      
                     }).AsEnumerable().GroupBy(x => x.Value).Select(a => a.FirstOrDefault()).ToList();

        return Json(query, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        return Json(new { status = false, message = ex.Message });
      }
    }

    public JsonResult GetPermanentMunicipalityAjax(string term)
    {
      try
      {
        if (string.IsNullOrEmpty(term))
        {
          return Json(new { status = false, message = "Invalid input" });
        }
        // Using StartsWith to filter the names starting with Searchtext
        var query = (from md in db.MasterBeneficiariesDetails
                     join Ms in db.MasterEmployees on md.Id equals Ms.EmployeeID into Ms1
                     from Ms in Ms1.DefaultIfEmpty()
                     where md.IsActive && Ms.PermanentHalqaPanchayatOrMunicipalityName.StartsWith(term)
                     select new
                     {
                       Key = Ms.EmployeeID,
                       Value = Ms.PermanentHalqaPanchayatOrMunicipalityName,                      
                     }).AsEnumerable().GroupBy(p=>p.Value).Select(x=>x.FirstOrDefault()).ToList();

        return Json(query, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        return Json(new { status = false, message = ex.Message });
      }
    }

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
    public ActionResult AjaxHandlerDownloadAjax(JQueryDataTableParamModel param)
    {
      //var currentFinancialYear = GetCurrentSelectedFinancialYear();
      var currentFinancialYearDateby = stringCurrentSelectedFinancialYear();
      DateTime FromDateFinancialYear = Convert.ToDateTime(currentFinancialYearDateby.Split('-')[0], System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
      DateTime ToDateFinancialYear = Convert.ToDateTime(currentFinancialYearDateby.Split('-')[1], System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);

      int valid = Convert.ToInt32(MediaType.Validation);
      var Media_QueueDetailsList = db.Media_Queue.Where(x => x.MediaType == valid && DbFunctions.TruncateTime(x.UploadedDate) >= FromDateFinancialYear && DbFunctions.TruncateTime(x.UploadedDate) <= ToDateFinancialYear);
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
    public ActionResult UploadVerificationAjaxHandlerAjax(JQueryDataTableParamModel param)
    {
      int valid_res = Convert.ToInt32(MediaType.Validation_res);
      var currentFinancialYearDateby = stringCurrentSelectedFinancialYear();
      string FromDateFinancialYear = currentFinancialYearDateby.Split('-')[0];
      string ToDateFinancialYear = currentFinancialYearDateby.Split('-')[1];
      List<MediaDownloads> MediaDownloadsDetailsList = new List<MediaDownloads>();
      string connectionString = ConnectionStringProvider.GetConnectionString();
      //var currentFinancialYear = GetCurrentSelectedFinancialYear();

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

      string query = @" SELECT Id, FileName, HasDownoaded, IsProcessed, CreatedMachineInfo,CreatedBy, CreatedOn, ModifiedMachineInfo, ModifiedBy, ModifiedOn, 
                IsActive, MediaType, RecordId, TotalBeneficiary, TotalValidated,TotalNotValidated, Remark FROM [dbo].[MediaDownloads] 
            WHERE MediaType = @MediaType AND IsActive = 1  AND CreatedOn >= @FromDate  AND CreatedOn <= @ToDate";

      using (var connection = new SqlConnection(connectionString))
      using (var command = new SqlCommand(query, connection))
      {
        // Add parameters to avoid SQL injection
        command.Parameters.AddWithValue("@MediaType", valid_res);
        command.Parameters.AddWithValue("@FromDate", DateTime.ParseExact(FromDateFinancialYear, "dd/MM/yyyy", null));
        command.Parameters.AddWithValue("@ToDate", DateTime.ParseExact(ToDateFinancialYear, "dd/MM/yyyy", null));

        connection.Open();
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            var download = new MediaDownloads
            {
              FileName = reader["FileName"] as string,
              TotalBeneficiary = reader["TotalBeneficiary"] as int? ?? 0,
              TotalValidated = reader["TotalValidated"] as int? ?? 0,
              TotalNotvalidated = reader["TotalNotValidated"] as int? ?? 0,
              IsProcessed = reader["IsProcessed"] as bool? ?? false,
              CreatedOn = reader["CreatedOn"] as DateTime?,
              CreatedBy = reader["CreatedBy"] as int? ?? 0,
              Remark = reader["Remark"] as string
            };

            MediaDownloadsDetailsList.Add(download);
          }
        }
      }
      //using (DataSet ds = objDALBaseClass.GetData("SELECT Id, FileName, HasDownoaded, IsProcessed, CreatedMachineInfo, CreatedBy, CreatedOn, ModifiedMachineInfo, ModifiedBy, ModifiedOn, IsActive, MediaType, RecordId, TotalBeneficiary, TotalValidated, TotalNotValidated, Remark FROM  [dbo].[MediaDownloads] WHERE MediaType = " + valid_res + " And IsActive=1 and YEAR(CreatedOn)=" + currentFinancialYear))
      //{//(ref parameter,  objDVOPYBatchProcessStybatchrGET.FIND_QUERY);
      //  if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
      //    foreach (DataRow dr in ds.Tables[0].Rows)
      //    {
      //      MediaDownloads obj = new MediaDownloads();
      //      obj.FileName = (dr[1] != DBNull.Value ? dr[1].ToString() : "");
      //      obj.TotalBeneficiary = (dr[13] != DBNull.Value ? Convert.ToInt32(dr[13]) : 0);
      //      obj.TotalValidated = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);
      //      obj.TotalNotvalidated = (dr[15] != DBNull.Value ? Convert.ToInt32(dr[15]) : 0);
      //      obj.IsProcessed = (dr[3] != DBNull.Value ? Convert.ToBoolean(dr[3]) : false);
      //      obj.CreatedOn = (dr[6] != DBNull.Value ? Convert.ToDateTime(dr[6]) : (DateTime?)null);
      //      obj.CreatedBy = (dr[5] != DBNull.Value ? Convert.ToInt32(dr[5]) : 0);
      //      obj.Remark = (dr[16] != DBNull.Value ? dr[16].ToString() : "");
      //      MediaDownloadsDetailsList.Add(obj);
      //    }
      //}
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

    public ActionResult BeneficiaryPaymentDetailsAjax(JQueryDataTableParamModel param, bool isDownload = false)
    {
      try
      {
        int UserId = Convert.ToInt32(User.Identity.GetUserId());
        int RoleId = _DbContext.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
        Dictionary<string, string> PensionDetailByMonthYears = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear();
        var batch = PensionDetailByMonthYears == null ? "0" : PensionDetailByMonthYears["pybatchid"];
        var currentFinancialYear = GetCurrentSelectedFinancialYear();
        int batchno = Convert.ToInt32(batch);
        var selectDistrict = db.SecRoleLocationModule
              .Where(srlm => srlm.RoleID == RoleId && srlm.UserId == UserId)
              .Join(db.MasterDistrict, srlm => srlm.DistrictID, md => md.Id, (srlm, md) => md.Name).Distinct().ToList();

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
    public ActionResult UploadBankAjax(JQueryDataTableParamModel param)
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

    public ActionResult FindbeneficiaryDetails()
    {
      //int userid = AppUserManager.GetUserId();
      //var roleList = db.SecRoleLocationModule.AsNoTracking().Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      //var model = (from c in db.SecModule.AsNoTracking().Where(x => x.ControllerName == "MasterPensioner" && x.ActionName == "Index")
      //             join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
      //             from p in ps.DefaultIfEmpty()
      //             select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Beneficiary Details", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      //if (model != null)
      //{
      //  ViewBag.AddPermission = model.AddPermssion;
      //  ViewBag.EditPermission = model.EditPermission;
      //  ViewBag.DeletePermission = model.DeletePermission;

      //}
      try
      {       
        ViewBag.Group = GetUsersAssignedLocations();
        return View();
      }
      catch (Exception ex)
      {
        throw;
      }

    }
  }
}
