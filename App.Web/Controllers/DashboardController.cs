using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using App.Data.Entities;
using System.Data.Entity;
using App.Web.Filters;
using App.Web.Models;
using App.Data;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Web.Configuration;
using App.Data.ViewModels;
using MvcSiteMapProvider.Reflection;
using JKPS.COMMON;
using App.Web.Helper;
using JKPS.BLL;
using JKPS.DL;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.IO;
using Microsoft.Owin.Security.Notifications;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.ComponentModel;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class DashboardController : BaseController
    {
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;
        private AppDbContext _DbContext;

        public DashboardController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _DbContext = DbContext;
        }
        // GET: Dashboard
        //
        public ActionResult Index()
        {
            var model = new WidgetsViewModel();
            ViewBag.NewContributor = _DbContext.MasterContributor.Where(x => x.CreatedOn.Value.Year == DateTime.Now.Year && x.CreatedOn.Value.Month == DateTime.Now.Month).Count();
            int UserId = Convert.ToInt32(User.Identity.GetUserId());
            int RoleId = _DbContext.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            var widgets = db.Widgets.Where(p => p.IsActive && p.RoleId == RoleId).ToList();
            if (widgets != null)
            {
              model.Total_Registered = widgets.Any(x => x.WidgetId == 1 && x.IsDisplay);
              model.Total_Approved = widgets.Any(x => x.WidgetId == 2 && x.IsDisplay);
              model.Total_Unapproved = widgets.Any(x => x.WidgetId == 3 && x.IsDisplay);
              model.Beneficiary_Paid = widgets.Any(x => x.WidgetId == 4 && x.IsDisplay);
              model.RegisteredBeneficiary_DistrictWise = widgets.Any(x => x.WidgetId == 5 && x.IsDisplay);
              model.Beneficiaries_PaymentSummary = widgets.Any(x => x.WidgetId == 6 && x.IsDisplay);
            }
            string con = ConnectionStringProvider.GetConnectionString();
            using (SqlConnection conn = new SqlConnection(con))
            {
                string databaseName = conn.Database;
                //SqlCommand sqlCommand = new SqlCommand("Usp_GetDashboardDataCount_V2", conn);
                SqlCommand sqlCommand = new SqlCommand("Usp_GetDashboardDataCount_V3", conn);
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                sqlCommand.Parameters.AddWithValue("@RoleId", RoleId);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 3000;
                SqlDataAdapter adap = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                adap.Fill(ds);

                /// ***** Code By Himanshu Rajput *****
                #region 


                //if ((ds?.Tables?.Count ?? 0) > 0 && ds.Tables[0].Rows.Count == 1 && ds.Tables[0].Columns.Count == 32)
                //{
                //    ViewBag.TotalRegisteredBeneficiary = ds.Tables[0].Rows[0][0];
                //    ////  ** RegisteredEmployees **

                //    ViewBag.RPCP = ds.Tables[0].Rows[0][1];
                //    ViewBag.RWID = ds.Tables[0].Rows[0][2];
                //    ViewBag.ROAP = ds.Tables[0].Rows[0][3];
                //    ViewBag.RTransgender = ds.Tables[0].Rows[0][4];

                //    ViewBag.TotalApprovedBeneficiary = ds.Tables[0].Rows[0][5];
                //    ////  ** ApprovedEmployees **

                //    ViewBag.APCP = ds.Tables[0].Rows[0][6];
                //    ViewBag.AWID = ds.Tables[0].Rows[0][7];
                //    ViewBag.AOAP = ds.Tables[0].Rows[0][8];
                //    ViewBag.ATransgender = ds.Tables[0].Rows[0][9];

                //    ViewBag.TotalUnapprovedBeneficiary = ds.Tables[0].Rows[0][10];
                //    ////  ** UnapprovedEmployees **

                //    ViewBag.UPCP = ds.Tables[0].Rows[0][11];
                //    ViewBag.UWID = ds.Tables[0].Rows[0][12];
                //    ViewBag.UOAP = ds.Tables[0].Rows[0][13];
                //    ViewBag.UTransgender = ds.Tables[0].Rows[0][14];

                //    ViewBag.BeneficiaryPaidLastMonth = ds.Tables[0].Rows[0][15];


                //    ////  ** EmployeedPaidLastMonth **

                //    ViewBag.PMPCP = ds.Tables[0].Rows[0][16];
                //    ViewBag.PMWID = ds.Tables[0].Rows[0][17];
                //    ViewBag.PMOAP = ds.Tables[0].Rows[0][18];
                //    ViewBag.PMTransgender = ds.Tables[0].Rows[0][19];






                //}


                //else
                //{
                //    ViewBag.TotalRegisteredBeneficiary = 0;
                //    ViewBag.TotalApprovedBeneficiary = 0;
                //    ViewBag.TotalUnapprovedBeneficiary = 0;
                //    ViewBag.BeneficiaryPaidLastMonth = 0;

                //    ////  ** RegisteredEmployees **

                //    ViewBag.RPCP = 0;
                //    ViewBag.RWID = 0;
                //    ViewBag.ROAP = 0;
                //    ViewBag.RTransgender = 0;

                //    ////  ** ApprovedEmployees **

                //    ViewBag.APCP = 0;
                //    ViewBag.AWID = 0;
                //    ViewBag.AOAP = 0;
                //    ViewBag.ATransgender = 0;

                //    ////  ** UnapprovedEmployees **

                //    ViewBag.UPCP = 0;
                //    ViewBag.UWID = 0;
                //    ViewBag.UOAP = 0;
                //    ViewBag.UTransgender = 0;

                //    ////  ** EmployeedPaidLastMonth **

                //    ViewBag.PMPCP = 0;
                //    ViewBag.PMWID = 0;
                //    ViewBag.PMOAP = 0;
                //    ViewBag.PMTransgender = 0;
                //}
                #endregion
                if ((ds?.Tables?.Count ?? 0) > 0 && ds.Tables[0].Rows.Count == 1 && ds.Tables[0].Columns.Count == 32)
                {
                    ViewBag.TotalRegisteredBeneficiary = ds.Tables[0].Rows[0][0];
                    ViewBag.TotalRegBenefMale = ds.Tables[0].Rows[0][20];
                    ViewBag.TotalRegBenefFemale = ds.Tables[0].Rows[0][24];
                    ViewBag.TotalRegBenefTrans = ds.Tables[0].Rows[0][28];
                    ////  ** RegisteredEmployees **

                    ViewBag.RPCP = ds.Tables[0].Rows[0][1];
                    ViewBag.RWID = ds.Tables[0].Rows[0][2];
                    ViewBag.ROAP = ds.Tables[0].Rows[0][3];
                    ViewBag.RTransgender = ds.Tables[0].Rows[0][4];

                    ViewBag.TotalApprovedBeneficiary = ds.Tables[0].Rows[0][5];
                    ViewBag.TotalApprBenefMale = ds.Tables[0].Rows[0][21];
                    ViewBag.TotalApprBenefFemale = ds.Tables[0].Rows[0][25];
                    ViewBag.TotalApprBenefTrans = ds.Tables[0].Rows[0][29];
                    ////  ** ApprovedEmployees **

                    ViewBag.APCP = ds.Tables[0].Rows[0][6];
                    ViewBag.AWID = ds.Tables[0].Rows[0][7];
                    ViewBag.AOAP = ds.Tables[0].Rows[0][8];
                    ViewBag.ATransgender = ds.Tables[0].Rows[0][9];

                    ViewBag.TotalUnapprovedBeneficiary = ds.Tables[0].Rows[0][10];
                    ViewBag.TotalUnapprBenefMale = ds.Tables[0].Rows[0][22];
                    ViewBag.TotalUnapprBenefFemale = ds.Tables[0].Rows[0][26];
                    ViewBag.TotalUnapprBenefTrans = ds.Tables[0].Rows[0][30];
                    ////  ** UnapprovedEmployees **

                    ViewBag.UPCP = ds.Tables[0].Rows[0][11];
                    ViewBag.UWID = ds.Tables[0].Rows[0][12];
                    ViewBag.UOAP = ds.Tables[0].Rows[0][13];
                    ViewBag.UTransgender = ds.Tables[0].Rows[0][14];

                    ViewBag.BeneficiaryPaidLastMonth = ds.Tables[0].Rows[0][15];
                    ViewBag.BenefPaidLastMonthMale = ds.Tables[0].Rows[0][23];
                    ViewBag.BenefPaidLastMonthFemale = ds.Tables[0].Rows[0][27];
                    ViewBag.BenefPaidLastMonthTrans = ds.Tables[0].Rows[0][31];

                    ////  ** EmployeedPaidLastMonth **

                    ViewBag.PMPCP = ds.Tables[0].Rows[0][16];
                    ViewBag.PMWID = ds.Tables[0].Rows[0][17];
                    ViewBag.PMOAP = ds.Tables[0].Rows[0][18];
                    ViewBag.PMTransgender = ds.Tables[0].Rows[0][19];

                }
                else
                {
                    ViewBag.TotalRegisteredBeneficiary = 0;
                    ViewBag.TotalApprovedBeneficiary = 0;
                    ViewBag.TotalUnapprovedBeneficiary = 0;
                    ViewBag.BeneficiaryPaidLastMonth = 0;

                    ////  ** RegisteredEmployees **

                    ViewBag.RPCP = 0;
                    ViewBag.RWID = 0;
                    ViewBag.ROAP = 0;
                    ViewBag.RTransgender = 0;

                    ViewBag.TotalRegBenefMale = 0;
                    ViewBag.TotalRegBenefFemale = 0;
                    ViewBag.TotalRegBenefTrans = 0;

                    ////  ** ApprovedEmployees **

                    ViewBag.APCP = 0;
                    ViewBag.AWID = 0;
                    ViewBag.AOAP = 0;
                    ViewBag.ATransgender = 0;

                    ViewBag.TotalApprBenefMale = 0;
                    ViewBag.TotalApprBenefFemale = 0;
                    ViewBag.TotalApprBenefTrans = 0;
                    ////  ** UnapprovedEmployees **

                    ViewBag.UPCP = 0;
                    ViewBag.UWID = 0;
                    ViewBag.UOAP = 0;
                    ViewBag.UTransgender = 0;

                    ViewBag.TotalUnapprBenefMale = 0;
                    ViewBag.TotalUnapprBenefFemale = 0;
                    ViewBag.TotalUnapprBenefTrans = 0;

                    ////  ** EmployeedPaidLastMonth **

                    ViewBag.PMPCP = 0;
                    ViewBag.PMWID = 0;
                    ViewBag.PMOAP = 0;
                    ViewBag.PMTransgender = 0;

                    ViewBag.BenefPaidLastMonthMale = 0;
                    ViewBag.BenefPaidLastMonthFemale = 0;
                    ViewBag.BenefPaidLastMonthTrans = 0;

                }
            }
            return View(model);
        }
        public ActionResult _ContributorDetails()
        {
            return PartialView("_ContributorDetails");
        }
        public List<MasterContributor> contributor()
        {
            List<MasterContributor> list = new List<MasterContributor>();
            var model = _DbContext.MasterContributor.Where(x => x.IsActive == true && x.JobStatusID == 1).Select(x => new { x.ExpectedRetirementDate, x.Id }).ToList();
            foreach (var item in model)
            {
                DateTime? retirementDate = item.ExpectedRetirementDate;
                var date = DateTime.Now;
                var difference = retirementDate == null ? null : (retirementDate - date);
                var TotalDay = difference == null ? 0 : Convert.ToInt32(difference.Value.TotalDays);
                if (TotalDay < 90 && TotalDay > 0)
                {
                    var tempmodel = _DbContext.MasterContributor.FirstOrDefault(x => x.Id == item.Id);
                    list.Add(tempmodel);
                }
            }
            return list;
        }
        public List<MasterContributor> Beneficiaries()
        {
            List<MasterContributor> list = new List<MasterContributor>();
            var model = _DbContext.MasterContributor.Where(x => x.IsActive == true && x.JobStatusID == 1).Select(x => new { x.ExpectedRetirementDate, x.Id }).ToList();
            foreach (var item in model)
            {
                DateTime? retirementDate = item.ExpectedRetirementDate;
                var date = DateTime.Now;
                var difference = retirementDate == null ? null : (retirementDate - date);
                var TotalDay = difference == null ? 0 : Convert.ToInt32(difference.Value.TotalDays);
                if (TotalDay < 90 && TotalDay > 0)
                {
                    var tempmodel = _DbContext.MasterContributor.FirstOrDefault(x => x.Id == item.Id);
                    list.Add(tempmodel);
                }
            }
            return list;
        }

        public ActionResult ContributorAjaxHandler(JQueryDataTableParamModel param)
        {
            DataSet ds = new DataSet();
            StringBuilder sql = new StringBuilder();
            sql.Append("select me.ApplicationReferenceNo,me.PresentDistrict,eb.APPLICANT_BANK_IFSC_CODE,NameOfTheApplicant,eb.APPLICANT_ACCOUNT_NO ");
            sql.Append(" ,iif(tt.ok_to_post = 'Y','Active','Deactive') AccountStatus,et.[Description] from MasterEmployee me ");
            sql.Append(" left join MasterEmpBankDetails eb on eb.empl_code = me.Empl_Code ");
            sql.Append(" left join MasterBeneficiariesDetails mbd on mbd.ApplicationReferenceNo = me.ApplicationReferenceNo ");
            sql.Append(" left join MasterEmpType et on et.type_code = me.type_code ");
            sql.Append(" left join (select * from (select *,row_number() over (partition by empl_code  order by pay_start_date desc) rn from Process_PayEmployee) t where t.rn = 1) tt on tt.empl_code = me.Empl_Code ");
            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(sql.ToString(), con);
            da.Fill(ds);
            List<App.Data.ViewModels.BeneficiriesDetail> model = new List<App.Data.ViewModels.BeneficiriesDetail>();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BeneficiriesDetail BenDetails = new BeneficiriesDetail();
                    BenDetails.ApplicationReferenceNo = dr[0]?.ToString();
                    BenDetails.PresentDistrict = dr[1]?.ToString();
                    BenDetails.IFSCCode = dr[2]?.ToString();
                    BenDetails.NameOfTheApplicant = dr[3]?.ToString();
                    BenDetails.AccountNumber = dr[4]?.ToString();
                    BenDetails.CurrentStatus = dr[5]?.ToString();
                    BenDetails.SelectPensionType = dr[6]?.ToString();
                    model.Add(BenDetails);
                }
            }
            List<MasterContributor> list = contributor();
            //List<MasterContributor> list = new List<MasterContributor>();
            //var model = _DbContext.MasterContributor.Where(x => x.IsActive == true && x.JobStatusID == 1).Select(x => new { x.ExpectedRetirementDate, x.Id }).ToList();
            //foreach (var item in model)
            //{
            //  DateTime? retirementDate = item.ExpectedRetirementDate;
            //  var date = DateTime.Now;
            //  var difference = retirementDate == null ? null : (retirementDate - date);
            //  var TotalDay = difference == null ? 0 : Convert.ToInt32(difference.Value.TotalDays);
            //  if (TotalDay < 90 && TotalDay > 0)
            //  {
            //    var tempmodel = _DbContext.MasterContributor.FirstOrDefault(x => x.Id == item.Id);
            //    list.Add(tempmodel);
            //  }
            //}
            IEnumerable<BeneficiriesDetail> filtered;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = model
                   .Where(c => c.ApplicationReferenceNo.ToLower().Contains(param.sSearch.ToLower())
                   || (c.PresentDistrict).ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
                   //|| c.MidName.ToLower().Contains(param.sSearch.ToLower())
                   //|| c.LastName.ToLower().Contains(param.sSearch.ToLower())
                   || c.IFSCCode.ToLower().Contains(param.sSearch.ToLower())
                   || c.NameOfTheApplicant.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.AccountNumber.ToLower().Contains(param.sSearch.ToLower())
                   || c.CurrentStatus.ToLower().Contains(param.sSearch.ToLower())
                   || c.SelectPensionType.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filtered = model;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<BeneficiriesDetail, string> orderingFunction = (c => sortColumnIndex == 0 ? c.ApplicationReferenceNo :
                                                                                            sortColumnIndex == 1 ? c.PresentDistrict + "" :
                                                                                            sortColumnIndex == 2 ? c.IFSCCode :
                                                                                            sortColumnIndex == 3 ? c.NameOfTheApplicant + "" :
                                                                                            sortColumnIndex == 4 ? c.AccountNumber :
                                                                                            sortColumnIndex == 5 ? c.CurrentStatus + "" :
                                                                                            sortColumnIndex == 6 ? c.SelectPensionType + "" :
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

                         c.ApplicationReferenceNo,
                         c.PresentDistrict,
                         c.IFSCCode,
                         c.NameOfTheApplicant,
                         c.AccountNumber,
                         c.CurrentStatus,
                         c.SelectPensionType
                   };

            return Json(
                                        new
                                        {
                                            sEcho = param.sEcho,
                                            iTotalRecords = list.Count(),
                                            iTotalDisplayRecords = filtered.Count(),
                                            aaData = result
                                        }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _DependantDetails()
        {
            return PartialView("_DependantDetails");
        }
        public List<MasterDependantDetails> dependant()
        {
            List<MasterDependantDetails> list = new List<MasterDependantDetails>();

            var model = _DbContext.MasterDependantDetails.Where(x => x.IsActive == true).Select(x => new { x.TerminationDate, x.Id }).ToList();
            foreach (var item in model)
            {
                DateTime? terminationDate = item.TerminationDate;
                var date = DateTime.Now;
                var difference = terminationDate == null ? null : (terminationDate - date);
                var TotalDay = difference == null ? 0 : Convert.ToInt32(difference.Value.TotalDays);
                if (TotalDay < 90 && TotalDay > 0)
                {
                    var tempmodel = _DbContext.MasterDependantDetails.Where(x => x.Id == item.Id).FirstOrDefault();
                    list.Add(tempmodel);
                }
            }
            return list;
        }
        public ActionResult DependantAjaxHandler(JQueryDataTableParamModel param)
        {
            List<MasterDependantDetails> list = dependant();
            //List<MasterDependantDetails> list = new List<MasterDependantDetails>();

            //var model = _DbContext.MasterDependantDetails.Where(x => x.IsActive == true).Select(x => new { x.TerminationDate, x.Id }).ToList();
            //foreach (var item in model)
            //{
            //  DateTime? terminationDate = item.TerminationDate;
            //  var date = DateTime.Now;
            //  var difference = terminationDate == null ? null : (terminationDate - date);
            //  var TotalDay = difference == null ? 0 : Convert.ToInt32(difference.Value.TotalDays);
            //  if (TotalDay < 90 && TotalDay > 0)
            //  {
            //    var tempmodel = _DbContext.MasterDependantDetails.Where(x => x.Id == item.Id).FirstOrDefault();
            //    list.Add(tempmodel);
            //  }
            //}
            IEnumerable<MasterDependantDetails> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = list
                   .Where(c => c.Relationship.Name.ToLower().Contains(param.sSearch.ToLower())
                   || (c.FirstName + " " + c.MidName + " " + c.LastName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
                   //|| (c.MidName + "").ToLower().Contains(param.sSearch.ToLower())
                   //|| c.LastName.ToLower().Contains(param.sSearch.ToLower())
                   || c.Contributor.FirstName.ToLower().Contains(param.sSearch.ToLower())
                   || c.Contributor.MidName.ToLower().Contains(param.sSearch.ToLower())
                   || c.Contributor.LastName.ToLower().Contains(param.sSearch.ToLower())
                   || c.DateOfBirth.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.TerminationDate.ToString().ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filtered = list;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MasterDependantDetails, string> orderingFunction = (c => sortColumnIndex == 0 ? c.PersonID :

                                                                                            sortColumnIndex == 1 ? c.FirstName + "" :
                                                                                            sortColumnIndex == 2 ? c.Contributor.FirstName :
                                                                                            sortColumnIndex == 3 ? c.DateOfBirth + "" :
                                                                                            sortColumnIndex == 4 ? c.TerminationDate + "" :
                                                                                            sortColumnIndex == 4 ? c.Relationship.Name :
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

                         c.FirstName+" "+c.MidName+" "+c.LastName,
                         c.Relationship.Name,
                         c.Contributor.FirstName+" "+c.Contributor.MidName+" "+c.Contributor.LastName,
                         c.Gender,
                         String.Format("{0:MM/dd/yyyy}", c.DateOfBirth),
                         String.Format("{0:MM/dd/yyyy}", c.TerminationDate)
                   };

            return Json(
                                        new
                                        {
                                            sEcho = param.sEcho,
                                            iTotalRecords = list.Count(),
                                            iTotalDisplayRecords = filtered.Count(),
                                            aaData = result
                                        }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _EmployerSummaryAjax()
        {
            DataSet ds = new DataSet();
            StringBuilder sql = new StringBuilder();
            sql.Append("select et.type_code,et.[Description],count(me.EmployeeID) from MasterEmpType et ");
            sql.Append(" left join MasterEmployee me on et.type_code = me.type_code");
            sql.Append(" group by et.type_code,et.[Description]");
            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(sql.ToString(), con);
            da.Fill(ds);
            List<App.Data.ViewModels.EmployerSummaryViewModel> model = new List<App.Data.ViewModels.EmployerSummaryViewModel>();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    EmployerSummaryViewModel EmpSummary = new EmployerSummaryViewModel();
                    EmpSummary.EmployerId = dr[0]?.ToString();//"type_code"
                    EmpSummary.EmployerName = dr[1]?.ToString();//"Description"
                    EmpSummary.Count = Convert.ToInt32(dr[2]);//"EmployeeCount"
                    model.Add(EmpSummary);
                }
            }
            return PartialView("_EmployerSummaryAjax", model);
        }
        //public ActionResult _EmployerSummary()
        //{
        //  return PartialView("_EmployerSummary");
        //}
        public JsonResult employerSummaryAjax()
        {
            List<App.Data.ViewModels.EmployerSummaryViewModel> model = _DbContext.MasterContributor.Where(x => x.IsActive == true && x.JobStatusID == 1).GroupBy(x => x.EmployerID).Select(x => new App.Data.ViewModels.EmployerSummaryViewModel { EmployerId = x.Key.ToString(), Count = x.Count() }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult contributionSummaryAjax()
        {
            List<App.Data.ViewModels.TotalContributionSummaryViewModel> model = (from a in _DbContext.ContributonSheetDetailsFinalise
                                                                                 join b in _DbContext.ContributonSheetHeaderFinalise
                                                                                   on a.ContributonSheetHeaderFinaliseID equals b.Id
                                                                                 group a by b.EmployerId into grp
                                                                                 select new App.Data.ViewModels.TotalContributionSummaryViewModel { EmployerId = grp.Key.ToString(), TotalContributorContribution = grp.Sum(x => x.ContributorContribution), TotalEmployerContribution = grp.Sum(x => x.EmployerContribution).ToString() }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _ContributionSummaryAjax()
        {
            return PartialView("_ContributionSummaryAjax");
        }
        public ActionResult ContributionSummaryAjaxHandler(JQueryDataTableParamModel param, Int32 financialYear)
        {
            financialYear = GetCurrentSelectedFinancialYear();
            int UserId = Convert.ToInt32(User.Identity.GetUserId());
            int RoleId = _DbContext.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            List<App.Data.ViewModels.TotalContributionSummaryViewModel> CountList = BLLMasterEmployee.GelAllPaymentCount(UserId, RoleId, financialYear);
            IEnumerable<TotalContributionSummaryViewModel> filtered;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = CountList.Where(x => x.EmployerId.ToString().Contains(param.sSearch.ToLower())
                || x.EmployerName.ToString().ToLower().Contains(param.sSearch.ToLower())
                || x.TotalContributorContribution.ToString().Contains(param.sSearch.ToLower())
                || x.TotalMonthAmount.ToString().Contains(param.sSearch.ToLower())
                || x.PayMonthInYear.ToString().Contains(param.sSearch.ToLower())
                || x.LastPayMonthInYear.ToString().Contains(param.sSearch.ToLower())
                || x.TotalEmployerContribution.ToString().Contains(param.sSearch.ToLower())
                || x.status.ToString().Contains(param.sSearch.ToLower())
                || x.NotUpdated.ToString().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filtered = CountList;
            }
            var total = "";
            var result = from c in filtered
                         select new[]
                         {
                     c.EmployerId,
                     c.EmployerName,
                     string.IsNullOrEmpty(c.TotalEmployerContribution)?"0":c.TotalEmployerContribution,
                     string.IsNullOrEmpty(c.status)?"0":c.status,
                     string.IsNullOrEmpty(c.NotUpdated)?"0":c.NotUpdated,
                     total= Convert.ToInt32(string.IsNullOrEmpty(c.TotalEmployerContribution)?"0":c.TotalEmployerContribution) + Convert.ToInt32(string.IsNullOrEmpty(c.status)?"0":c.status) + Convert.ToInt32(string.IsNullOrEmpty(c.NotUpdated)?"0":c.NotUpdated) + "",
                     Math.Round(c.TotalMonthAmount ?? 0, 2) + "",
                     Math.Round(c.TotalContributorContribution ?? 0, 2) + "",
                     c.PayMonthInYear + "",
                     c.LastPayMonthInYear + ""

                   };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = CountList.Count(),
                iTotalDisplayRecords = filtered.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);

        }
        public async Task<ActionResult> PaymentDetailsAjaxHandler(JQueryDataTableParamModel param, string Status, string District, bool isDownload = false)
        {
            try
            {
                DataSet ds = new DataSet();
                StringBuilder sql = new StringBuilder();
                sql.Append(" select me.ApplicationReferenceNo, CONCAT(NULLIF(isnull(me.first_name,''), ''), CASE WHEN me.middle_name IS NOT NULL AND me.middle_name != '' THEN ' ' + me.middle_name ");
                sql.Append(" ELSE '' END, CASE WHEN me.last_name IS NOT NULL AND me.last_name != '' THEN ' ' + me.last_name ELSE '' END ) as ApplicantName,case me.Gender when 'M' then 'Male' when ");
                sql.Append(" 'F' then 'Female' when 'T' then 'Transgender' else '' end as Gender, CAST(DATEDIFF(YEAR, me.birthdate, GETDATE())  AS VARCHAR(10)) as [AgeInYears],concat(nullif(isnull(me.PresentVillageName,''),''), case when ");
                sql.Append(" me.PresentHalqaPanchayatOrMunicipalityName is not null or me.PresentHalqaPanchayatOrMunicipalityName != '' then ' ' + me.PresentHalqaPanchayatOrMunicipalityName  ");
                sql.Append(" else '' end, case when me.PresentTehsil is not null or me.PresentTehsil != '' then ' ' + me.PresentTehsil else '' end, case when me.SelectDistrict is not null or ");
                sql.Append(" me.SelectDistrict != '' then ' ' + me.SelectDistrict else '' end) as [Address],me.Phone,me.Email,meb.bank_acct_no as [BankAcctNo],meb.APPLICANT_BANK_IFSC_CODE as [IFSCCode],meb.BankName,meb.V_DATE as [LastVerified], ");
                sql.Append(" et.[Description] as [SchemeType],me.empl_code as [EmplCode],[Reason/Remarks] as ReasonForChange from MasterEmployee me left join MasterEmpBankDetails meb on me.Empl_Code = meb.empl_code LEFT JOIN  MasterEmpType et ON me.type_code=et.type_code   ");
                sql.Append(" inner join (SELECT distinct * FROM ( SELECT Row_number() OVER ( partition BY empl_code ORDER BY pay_date DESC) AS [rn],* FROM   process_directdeposit_details) cte WHERE  ");
                sql.Append(" rn = 1 ) dd on dd.empl_code = me.empl_code where me.SelectDistrict = '" + District + "' ");
                if (Status.ToLower().Trim() == "statusnotupdated")
                    sql.Append("and dd.[Status] is null");
                else
                    sql.Append("and dd.[Status] = '" + Status + "' ");
                //string con = WebConfigurationManager.AppSettings["SQLConn"];
                string con = ConnectionStringProvider.GetConnectionString();
                SqlDataAdapter da = new SqlDataAdapter(sql.ToString(), con);
                da.Fill(ds);
                List<DVOMasterEmployee> BeneficiariesList = ds.Tables[0].AsEnumerable().Select(dr => new DVOMasterEmployee
                {
                    ApplicationReferenceNo = dr.Field<string>("ApplicationReferenceNo"),
                    FirstName = dr.Field<string>("ApplicantName"),
                    Gender = dr.Field<string>("Gender"),
                    AgeInYears = string.IsNullOrEmpty(dr.Field<string>("AgeInYears")) ? 0 : Convert.ToInt32(dr.Field<string>("AgeInYears")),
                    Address1 = dr.Field<string>("Address"),
                    Phone = dr.Field<string>("Phone"),
                    mailid = dr.Field<string>("Email"),
                    BankAcctNo = dr.Field<string>("BankAcctNo"),
                    IFSCCode = dr.Field<string>("IFSCCode"),
                    BankName = dr.Field<string>("BankName"),
                    LastVerified = dr.Field<string>("LastVerified"),
                    type_desc = dr.Field<string>("SchemeType"),
                    //EmplCode = dr.Field<string>("EmplCode"),
                    ReasonForChange = dr.Field<string>("ReasonForChange")
                }).ToList();
                IEnumerable<DVOMasterEmployee> filtered;
                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    filtered = BeneficiariesList.Where(c =>
                           (c.ApplicationReferenceNo ?? "").ToLower().Contains(param.sSearch.ToLower())
                           || (c.FirstName ?? "").ToLower().Contains(param.sSearch.ToLower())
                           || (c.Gender ?? "").ToLower().Contains(param.sSearch.ToLower())
                           || (c.AgeInYears.ToString() ?? "").Contains(param.sSearch.ToLower())
                           || (c.Address1 ?? "").ToLower().Replace(" ", "").Contains(param.sSearch.ToLower())
                           || (c.Phone ?? "").ToLower().Contains(param.sSearch.ToLower())
                           || (c.mailid ?? "").ToLower().Contains(param.sSearch.ToLower())
                           || (c.BankAcctNo ?? "").ToLower().Contains(param.sSearch.ToLower())
                           || (c.IFSCCode ?? "").ToLower().Contains(param.sSearch.ToLower())
                           || (c.BankName ?? "").ToLower().Contains(param.sSearch.ToLower())
                           || (c.LastVerified ?? "").ToLower().Contains(param.sSearch.ToLower())
                           || (c.type_desc ?? "").ToLower().Contains(param.sSearch.ToLower())
                           || (c.ReasonForChange ?? "").ToLower().Contains(param.sSearch.ToLower())
                       );

                }
                else
                {
                    filtered = BeneficiariesList;
                }
                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
                Func<DVOMasterEmployee, string> orderingFunction = (c => sortColumnIndex == 1 ? c.ApplicationReferenceNo + "" :
                                                                  sortColumnIndex == 2 ? c.FirstName + "" :
                                                                  sortColumnIndex == 3 ? c.Gender + "" :
                                                                  sortColumnIndex == 4 ? c.AgeInYears + "" :
                                                                  sortColumnIndex == 6 ? c.Address1 + "" :
                                                                  sortColumnIndex == 7 ? c.Phone :
                                                                  sortColumnIndex == 8 ? c.mailid + "" :
                                                                  sortColumnIndex == 9 ? c.BankAcctNo :
                                                                  sortColumnIndex == 10 ? c.IFSCCode + "" :
                                                                  sortColumnIndex == 11 ? c.BankName + "" :
                                                                  sortColumnIndex == 12 ? c.LastVerified + "" :
                                                                  sortColumnIndex == 13 ? c.type_desc + "" :
                                                                  sortColumnIndex == 15 ? c.ReasonForChange + "" : ""
                                                                  );
                var sortDirection = Request["sSortDir_0"];
                if (sortDirection == "asc")
                    filtered = filtered.OrderBy(orderingFunction);
                else
                    filtered = filtered.OrderByDescending(orderingFunction);

                //isdownload
                if (isDownload)
                {
                    FileContentResult bytesdata = PaymentDetailsDashReport(filtered.ToList());
                    return bytesdata;
                }
                var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);
                var result = from c in displayed
                             select new[] {
                         c.ApplicationReferenceNo,
                         c.FirstName,
                         c.Gender,
                         c.AgeInYears + "",
                         c.Address1,
                         c.Phone,
                         c.mailid,
                         c.BankAcctNo,
                         c.IFSCCode,
                         c.BankName,
                         c.LastVerified,
                         c.type_desc,
                         //c.EmplCode,
                         c.ReasonForChange
                   };
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = BeneficiariesList.Count(),
                    iTotalDisplayRecords = filtered.Count(),
                    aaData = result,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ActionResult _SurvivorDetails()
        {
            return PartialView("_SurvivorDetails");
        }
        public List<MasterDependantDetails> survivor()
        {
            List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = new List<DVOMasterEmployee>();
            int UserId = AppUserManager.GetUserId();
            int RoleId = _DbContext.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            listSearchResultDVOMasterEmployee = JKPS.BLL.BLLMasterEmployee.GetAllData(UserId, RoleId);

            //using (var scope = Helper.Helper.CreateTransaction())
            //{
            //  scope.Complete();
            //}

            var dependentAmongList = listSearchResultDVOMasterEmployee.Where(x => x.PensionerType == ((int)PensionerType.Child).ToString() && x.PersonID != null && x.PersonID != "").ToList();
            List<MasterDependantDetails> list = new List<MasterDependantDetails>();
            if (dependentAmongList.Count > 0)
            {
                foreach (var item in dependentAmongList)
                {
                    MasterDependantDetails dep = _DbContext.MasterDependantDetails.Where(x => x.IsActive == true && x.PersonID == item.PersonID && x.FirstName.ToLower().Contains(item.FirstName.ToLower())).FirstOrDefault();
                    if (dep != null)
                    {
                        if (dep.TerminationDate.HasValue)
                        {
                            DateTime? terminationDate = dep.TerminationDate;
                            var date = DateTime.Now;
                            var difference = terminationDate == null ? null : (terminationDate - date);
                            var TotalDay = difference == null ? 0 : Convert.ToInt32(difference.Value.TotalDays);
                            if (TotalDay < 90 && TotalDay > 0)
                            {
                                list.Add(dep);
                            }
                        }
                    }
                }
            }
            return list;
        }
        public ActionResult SurvivorAjaxHandler(JQueryDataTableParamModel param)
        {
            List<MasterDependantDetails> list = survivor();
            //var listSearchResultDVOMasterEmployee = JKPS.BLL.BLLMasterEmployee.GetAllData();
            //var dependentAmongList = listSearchResultDVOMasterEmployee.Where(x => x.PensionerType == ((int)PensionerType.Child).ToString() && x.PersonID != null && x.PersonID != "").ToList();
            //List<MasterDependantDetails> list = new List<MasterDependantDetails>();
            //if (dependentAmongList.Count > 0)
            //{
            //  foreach (var item in dependentAmongList)
            //  {
            //    MasterDependantDetails dep = _DbContext.MasterDependantDetails.Where(x => x.IsActive == true && x.PersonID == item.PersonID && x.FirstName.ToLower().Contains(item.FirstName.ToLower())).FirstOrDefault();
            //    if (dep != null)
            //    {
            //      DateTime? terminationDate = dep.TerminationDate;
            //      var date = DateTime.Now;
            //      var difference = terminationDate == null ? null : (terminationDate - date);
            //      var TotalDay = difference == null ? 0 : Convert.ToInt32(difference.Value.TotalDays);
            //      if (TotalDay < 90 && TotalDay > 0)
            //      {
            //        list.Add(dep);
            //      }
            //    }
            //  }
            //}

            IEnumerable<MasterDependantDetails> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = list
                   .Where(c => c.Relationship.Name.ToLower().Contains(param.sSearch.ToLower())
                   || (c.FirstName + " " + c.MidName + " " + c.LastName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
                   //|| (c.MidName + "").ToLower().Contains(param.sSearch.ToLower())
                   //|| c.LastName.ToLower().Contains(param.sSearch.ToLower())
                   || (c.Contributor.FirstName + " " + c.Contributor.MidName + " " + c.Contributor.LastName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
                   //|| c.Contributor.MidName.ToLower().Contains(param.sSearch.ToLower())
                   //|| c.Contributor.LastName.ToLower().Contains(param.sSearch.ToLower())
                   || c.DateOfBirth.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.TerminationDate.ToString().ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filtered = list;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MasterDependantDetails, string> orderingFunction = (c => sortColumnIndex == 0 ? c.PersonID :

                                                                                            sortColumnIndex == 1 ? c.FirstName + "" :
                                                                                            sortColumnIndex == 2 ? c.Contributor.FirstName :
                                                                                            sortColumnIndex == 3 ? c.DateOfBirth + "" :
                                                                                            sortColumnIndex == 4 ? c.TerminationDate + "" :
                                                                                            sortColumnIndex == 4 ? c.Relationship.Name :
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

                         c.FirstName+" "+c.MidName+" "+c.LastName,
                         c.Relationship.Name,
                         c.Contributor.FirstName+" "+c.Contributor.MidName+" "+c.Contributor.LastName,
                         c.Gender,
                         String.Format("{0:MM/dd/yyyy}", c.DateOfBirth),
                         String.Format("{0:MM/dd/yyyy}", c.TerminationDate)
                   };

            return Json(
                                        new
                                        {
                                            sEcho = param.sEcho,
                                            iTotalRecords = list.Count(),
                                            iTotalDisplayRecords = filtered.Count(),
                                            aaData = result
                                        }, JsonRequestBehavior.AllowGet);
        }


        private FileContentResult PaymentDetailsDashReport(List<DVOMasterEmployee> filtered)
        {
            try
            {
                //var result = from c in filtered
                //             select new[] {
                //             c.ApplicationReferenceNo,
                //             c.FirstName,
                //             c.Gender,
                //             c.AgeInYears + "",
                //             c.Address1,
                //             c.Phone,
                //             c.mailid,
                //             c.BankAcctNo,
                //             c.IFSCCode,
                //             c.BankName,
                //             c.LastVerified,
                //             c.type_desc,
                //             //c.EmplCode
                //             c.ReasonForChange
                //};

                var result = (from x in filtered
                              select new DVOMasterEmployee
                              {

                                  ApplicationReferenceNo = x.ApplicationReferenceNo + "",
                                  FirstName = x.FirstName + "",
                                  Gender = x.Gender + "",
                                  AgeInYears = x.AgeInYears,
                                  Address1 = x.Address1,
                                  Phone = x.Phone,
                                  mailid = x.mailid,
                                  BankAcctNo = x.BankAcctNo,
                                  IFSCCode = x.IFSCCode,
                                  BankName = x.BankName,
                                  LastVerified = x.LastVerified,
                                  type_desc = x.type_desc,
                                  ReasonForChange = x.ReasonForChange + "",
                              }).ToList();

                FileContentResult bytesdata;
                using (MemoryStream stream = new MemoryStream())
                {
                    using (SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
                    {
                        WorkbookPart workbookPart = document.AddWorkbookPart();
                        workbookPart.Workbook = new Workbook();

                        WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                        worksheetPart.Worksheet = new Worksheet(new SheetData());

                        Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                        Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
                        sheets.Append(sheet);

                        SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                        Row headerRow = new Row();

                        Cell cell0 = new Cell();
                        cell0.DataType = CellValues.String;
                        cell0.CellValue = new CellValue("ApplicationReferenceNo");
                        headerRow.AppendChild(cell0);

                        Cell cell1 = new Cell();
                        cell1.DataType = CellValues.String;
                        cell1.CellValue = new CellValue("Name");
                        headerRow.AppendChild(cell1);

                        Cell cell2 = new Cell();
                        cell2.DataType = CellValues.String;
                        cell2.CellValue = new CellValue("Gender");
                        headerRow.AppendChild(cell2);

                        Cell cell3 = new Cell();
                        cell3.DataType = CellValues.String;
                        cell3.CellValue = new CellValue("Age");
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
                        cell6.CellValue = new CellValue("Mailid");
                        headerRow.AppendChild(cell6);

                        Cell cell7 = new Cell();
                        cell7.DataType = CellValues.String;
                        cell7.CellValue = new CellValue("Account No");
                        headerRow.AppendChild(cell7);

                        Cell cell8 = new Cell();
                        cell8.DataType = CellValues.String;
                        cell8.CellValue = new CellValue("IFSC Code");
                        headerRow.AppendChild(cell8);

                        Cell cell9 = new Cell();
                        cell9.DataType = CellValues.String;
                        cell9.CellValue = new CellValue("Bank Name");
                        headerRow.AppendChild(cell9);

                        Cell cell10 = new Cell();
                        cell10.DataType = CellValues.String;
                        cell10.CellValue = new CellValue("Last Verified");
                        headerRow.AppendChild(cell10);

                        Cell cell11 = new Cell();
                        cell11.DataType = CellValues.String;
                        cell11.CellValue = new CellValue("Scheme Type");
                        headerRow.AppendChild(cell11);

                        Cell cell12 = new Cell();
                        cell12.DataType = CellValues.String;
                        cell12.CellValue = new CellValue("Remarks/Reason");
                        headerRow.AppendChild(cell12);

                        //Cell cell13 = new Cell();
                        //cell13.DataType = CellValues.String;
                        //cell13.CellValue = new CellValue("IP Address");
                        //headerRow.AppendChild(cell13);

                        //Cell cell14 = new Cell();
                        //cell14.DataType = CellValues.String;
                        //cell14.CellValue = new CellValue("Record Id");
                        //headerRow.AppendChild(cell14);

                        //Cell cell15 = new Cell();
                        //cell15.DataType = CellValues.String;
                        //cell15.CellValue = new CellValue("Old SystemId");
                        //headerRow.AppendChild(cell15);

                        //Cell cell16 = new Cell();
                        //cell16.DataType = CellValues.String;
                        //cell16.CellValue = new CellValue("Message");
                        //headerRow.AppendChild(cell16);


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
                            cellR3.CellValue = new CellValue(item.AgeInYears.ToString());
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
                            //cellR10.CellValue = new CellValue(item[10]);
                            cellR10.CellValue = new CellValue(item.LastVerified);
                            dataRow.AppendChild(cellR10);

                            Cell cellR11 = new Cell();
                            cellR11.DataType = CellValues.String;
                            //cellR11.CellValue = new CellValue(item[11]);
                            cellR11.CellValue = new CellValue(item.type_desc);
                            dataRow.AppendChild(cellR11);

                            Cell cellR12 = new Cell();
                            cellR12.DataType = CellValues.String;
                            //cellR12.CellValue = new CellValue(item[12]);
                            cellR12.CellValue = new CellValue(item.ReasonForChange);
                            dataRow.AppendChild(cellR12);
                            sheetData.AppendChild(dataRow);
                        }
                        workbookPart.Workbook.Save();
                    }
                    bytesdata = File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, "Payment Details Report.xlsx");
                }
                return bytesdata;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //District wise data code function
        public ActionResult BeneficiaryDetails_summAjaxHandler(int BeneficiaryData)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Identity.GetUserId());
                int RoleId = _DbContext.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
                //string con = WebConfigurationManager.ConnectionStrings["AppConnection"].ConnectionString;
                string con = ConnectionStringProvider.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(con))
                {
                    SqlCommand sqlCommand2 = new SqlCommand("Usp_GetGenderCount_Distwise", conn);
                    sqlCommand2.Parameters.Clear();
                    sqlCommand2.Parameters.AddWithValue("@UserId", UserId);
                    sqlCommand2.Parameters.AddWithValue("@RoleId", RoleId);
                    sqlCommand2.Parameters.AddWithValue("@BeneficiaryData", BeneficiaryData);
                    sqlCommand2.CommandType = CommandType.StoredProcedure;
                    sqlCommand2.CommandTimeout = 3000;
                    SqlDataAdapter adap2 = new SqlDataAdapter(sqlCommand2);
                    DataSet ds2 = new DataSet();
                    adap2.Fill(ds2);
                    List<DistrictCount> result = DataSetToJson(ds2);

                    return Json(result);
                }
            }
            catch (Exception ex)
            {

                return Json(new { error = "An error occurred while processing the request." });
            }
        }
        private List<DistrictCount> DataSetToJson(DataSet dataSet)
        {
            var HeadGroups = dataSet.Tables[0].AsEnumerable().GroupBy(row => row["District"].ToString()?.ToUpper()).ToList();
            List<DistrictCount> distcount = new List<DistrictCount>();

            foreach (var item in HeadGroups)
            {
                DistrictCount district_c = new DistrictCount();
                district_c.District = item.Key;

                foreach (var i in item)
                {
                    empstatus singlerow = new empstatus();
                    singlerow.gender1 = i.ItemArray[1].ToString();
                    singlerow.count1 = i.ItemArray[2].ToString();
                    district_c.EmpStatusList.Add(singlerow);
                }
                distcount.Add(district_c);
            }
            return distcount;
        }

    }
}