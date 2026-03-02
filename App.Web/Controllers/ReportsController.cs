using App.Data;
using App.Data.Entities;
using App.Data.ViewModels;
using App.Web.Entities;
using App.Web.Filters;
using App.Web.Helper;
using App.Web.Models;
using App.Web.Repository;
using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using JKPS.BLL;
using JKPS.COMMON;
using JKPS.DL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using static App.Web.Helper.Helper;
//using System.Windows.Forms;
//using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class ReportsController : BaseController
    {
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;
        private AppDbContext _DbContext;
        public ReportsController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _DbContext = DbContext;
        }
        //
        // GET: /Reports/
        public ActionResult ContributionByEmployer()
        {
            var employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            ViewBag.EmployeeId = new SelectList(employer, "Id", "EmployerName");
            var month = db.MasterMonthName.Where(x => x.IsActive == true).ToList();
            ViewBag.Month = new SelectList(month, "Id", "Name");

            int year = DateTime.Now.Year - 20;
            List<YearModel> yearModel = new List<YearModel>();
            for (int i = year; i <= DateTime.Now.Year; i++)
            {
                YearModel tempYear = new YearModel();
                tempYear.Id = i;
                tempYear.Name = i.ToString();
                yearModel.Add(tempYear);
            }
            ViewBag.Year = new SelectList(yearModel, "Id", "Name");
            //ViewBag.Month = new SelectList(months);
            //ViewBag.Year = new SelectList(years);
            return View();
        }

        //[HttpPost]
        public JsonResult ShowReport(string EmployeeID, string month, string year)
        {

            int sYear = year == "" ? 0 : Convert.ToInt32(year);
            int sMonth = month == "" ? 0 : Convert.ToInt32(month);
            int sEmployee = EmployeeID == "" ? 0 : Convert.ToInt32(EmployeeID);
            var q = db.MasterContributor.Where(x => EmployeeID == "" ? x.EmployerID > 0 : x.EmployerID == sEmployee);
            if (!String.IsNullOrWhiteSpace(year))
                q = q.Where(x => x.CreatedOn.Value.Year == sYear);
            if (!String.IsNullOrWhiteSpace(month))
                q = q.Where(x => x.CreatedOn.Value.Month == sMonth);


            var results = from item in q.ToList()
                          select new
                          {
                              Id = item.EmployerID == null ? 0 : Convert.ToInt32(item.EmployerID),
                              JKPSUniqueID = item.PersonID == null ? "" : item.PersonID,
                              SocialSecurityNo = item.SocialSecurityNo == null ? 0 : Convert.ToInt32(item.SocialSecurityNo),
                              PrefixId = item.PrefixId ?? 0,
                              SuffixId = item.SuffixId ?? 0,
                              DateOfBirth = Convert.ToDateTime(item.DateOfBirth),
                              EmployerID = item.EmployerID ?? 0,
                              FirstName = item.FirstName,
                              MidName = item.MidName,
                              LastName = item.LastName,
                              ContributerFullName = item.FirstName + " " + item.MidName + " " + item.LastName,
                              Gender = item.Gender == null ? "" : item.Gender.ToString() == "M" ? "Male" : item.Gender.ToString() == "F" ? "Female" : "Other",
                              CountryID = item.CountryID ?? 0,
                              CountryName = item.Country == null ? "" : item.Country.CountryCode,
                              Email = item.Email == null ? "" : item.Email,
                              Nationality = item.Nationality == null ? "" : item.Nationality.Name,
                              PrefixName = item.Prefix == null ? "" : item.Prefix.Name,
                              SufixName = item.Suffix == null ? "" : item.Suffix.Name,
                              EmployerName = item.Employer.EmployerName,
                              EmployerAddress = item.Employer.EmployerAddress,
                              EmployerCity = item.Employer.City.Name,
                              EmployerContractPerson = item.Employer.ContactPerson,
                              EmployerMobile = item.Employer.Mobile
                          };
            results = results.OrderByDescending(x => x.Id).Take(20);
            if (results.Any())
            {
                this.HttpContext.Session["ReportName"] = "rptContributionByEmployer.rpt";
                this.HttpContext.Session["rptSource"] = results;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ContributionByContributor()
        {
            var employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            ViewBag.EmployeeId = new SelectList(employer, "Id", "EmployerName");

            List<MasterContributor> contributor = new List<MasterContributor>();
            ViewBag.ContributorId = new SelectList(contributor, "Id", "FirstName");
            return View();
        }

        public JsonResult ShowReportUpdate(string EmployeeID, string month, string year)
        {
            int sYear = year == "" ? 0 : Convert.ToInt32(year);
            int sMonth = month == "" ? 0 : Convert.ToInt32(month);
            int sEmployee = EmployeeID == "" ? 0 : Convert.ToInt32(EmployeeID);
            var q = db.MasterContributor.Where(x => EmployeeID == "" ? x.EmployerID > 0 : x.EmployerID == sEmployee);
            if (!String.IsNullOrWhiteSpace(year))
                q = q.Where(x => x.CreatedOn.Value.Year == sYear);
            if (!String.IsNullOrWhiteSpace(month))
                q = q.Where(x => x.CreatedOn.Value.Month == sMonth);

            var results = from item in q.ToList()
                          select new
                          {
                              Id = item.EmployerID == null ? 0 : Convert.ToInt32(item.EmployerID),
                              JKPSUniqueID = item.PersonID == null ? "" : item.PersonID,
                              SocialSecurityNo = item.SocialSecurityNo == null ? 0 : Convert.ToInt32(item.SocialSecurityNo),
                              PrefixId = item.PrefixId ?? 0,
                              SuffixId = item.SuffixId ?? 0,
                              DateOfBirth = Convert.ToDateTime(item.DateOfBirth),
                              EmployerID = item.EmployerID ?? 0,
                              FirstName = item.FirstName,
                              MidName = item.MidName,
                              LastName = item.LastName,
                              ContributerFullName = item.FirstName + " " + item.MidName + " " + item.LastName,
                              Gender = item.Gender == null ? "" : item.Gender.ToString() == "M" ? "Male" : item.Gender.ToString() == "F" ? "Female" : "Other",
                              CountryID = item.CountryID ?? 0,
                              CountryName = item.Country == null ? "" : item.Country.CountryCode,
                              Email = item.Email == null ? "" : item.Email,
                              Nationality = item.Nationality == null ? "" : item.Nationality.Name,
                              PrefixName = item.Prefix == null ? "" : item.Prefix.Name,
                              SufixName = item.Suffix == null ? "" : item.Suffix.Name,
                              EmployerName = item.Employer.EmployerName,
                              EmployerAddress = item.Employer.EmployerAddress,
                              EmployerCity = item.Employer.City.Name,
                              EmployerContractPerson = item.Employer.ContactPerson,
                              EmployerMobile = item.Employer.Mobile
                          };
            results = results.OrderByDescending(x => x.Id).Take(20);
            if (results.Any())
            {
                this.HttpContext.Session["ReportName"] = "rptContributionByEmployer.rpt";
                this.HttpContext.Session["rptSource"] = results;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }


        public ActionResult MonthlyPensionSummary()
        {

            ViewBag.DistrictId = new SelectList(Enumerable.Empty<SelectListItem>());

            ViewBag.Group = GetUsersAssignedLocations();
            ViewBag.EmployeeId = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.Gender = new SelectList(Enumerable.Empty<SelectListItem>());
            List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = new List<DVOMasterEmployee>();//BLLMasterEmployee.GetAllData();
            var defaultItem = new SelectListItem { Value = "ALL", Text = "ALL | ALL" };

            var emplCodeList = db.MasterEmpType
                .Where(x => x.Type_Code != null)
                .Select(x => new SelectListItem
                {
                    Value = x.Type_Code,
                    Text = x.Type_Code + " | " + x.Description
                })
                .ToList();

            emplCodeList.Insert(0, defaultItem); // Insert the default item at the beginning

            ViewBag.EmplCode = emplCodeList;

            return View();

            //      ViewBag.EmplCode = db.MasterEmpType.Where(x => x.Type_Code != null).Select(x => new SelectListItem
            //{
            //  Value = x.Type_Code,
            //  Text = x.Type_Code + " | " + x.Description
            //}).ToList();
            //return View();
        }
        public ActionResult PaymentHistory()
        {
            // string regionname = (Session["RegionName"]).ToString();
            var regionname = GetRegionName();

            var reasionid = db.MasterRegion.Where(x => x.Name.Contains(regionname)).Select(x => x.Id).FirstOrDefault();
            List<YearModel> yearModel = new List<YearModel>();
            yearModel.Add(new YearModel()
            {
                Id = DateTime.Now.Year,
                Name = DateTime.Now.Year.ToString()
            });
            ViewBag.Year = new SelectList(yearModel, "Id", "Name");
            DVOMasterEmpTypes objDVOMasterEmpTypes = new DVOMasterEmpTypes();
            List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes).Select(s => new DVOMasterEmpTypes
            {
                type_code = s.type_code,
                description = string.Format("{0} | {1}", s.type_code, s.description)
            }).ToList();
            ViewBag.SchemeType = new SelectList(listDVOMasterEmpTypes, "type_code", "description");

            // District Show Only RoleBase
            var district = GetUsersAssignedLocations();
            ViewBag.District = new SelectList(Enumerable.Empty<SelectListItem>());
            if (district.Count > 0)
                ViewBag.District = new SelectList(district.FirstOrDefault().subs.Select(x => new { Value = x.title, Text = x.title }).Distinct().ToList(), "Value", "Text");

            //ViewBag.District = new SelectList(District.Where(p => p.RegionId == reasionid).Select(x => new { Value = x.Name, Text = x.Name }).Distinct().ToList(), "Value", "Text");

            // All District Show List

            //ViewBag.District = new SelectList(db.MasterDistrict.Where(p => p.RegionId == reasionid).Select(x => new { Value = x.Name, Text = x.Name }).Distinct().ToList(), "Value", "Text");

            ViewBag.Gender = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.EmployeeId = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.Criteria = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.Equality = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.CriteriaValue = new SelectList(Enumerable.Empty<SelectListItem>());
            return View();
        }
        public ActionResult Arear()
        {
            DVOMasterEmpTypes objDVOMasterEmpTypes = new DVOMasterEmpTypes();
            List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes).Select(s => new DVOMasterEmpTypes
            {
                type_code = s.type_code,
                description = string.Format("{0} | {1}", s.type_code, s.description)
            }).ToList();
            ViewBag.BeneficiariesType = new SelectList(listDVOMasterEmpTypes, "type_code", "description");
            ViewBag.DeductionType = new SelectList(Enumerable.Empty<SelectListItem>());
            return View();
        }
        public JsonResult ArearAjax(JQueryDataTableParamModel param, string SchemeType)
        {
            List<ArearModel> LogsList = ReportingUtilities.GetArearReportData(SchemeType);
            IEnumerable<ArearModel> filtered;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = LogsList
                   .Where(c => c.ApplicationReferenceNo.ToString().Contains(param.sSearch.ToLower())
                   || c.ApplicantName.ToString().Contains(param.sSearch.ToLower())
                   || c.Jan.ToString().Contains(param.sSearch.ToLower())
                   || c.Feb.ToString().Contains(param.sSearch.ToLower())
                   || c.Mar.ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
                   || c.Apr.ToLower().Contains(param.sSearch.ToLower())
                   || c.May.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.Jun.ToLower().Contains(param.sSearch.ToLower())
                   || c.Jul.ToLower().Contains(param.sSearch.ToLower())
                   || c.Aug.ToLower().Contains(param.sSearch.ToLower())
                   || c.Sep.ToLower().Contains(param.sSearch.ToLower())
                   || c.Oct.ToLower().Contains(param.sSearch.ToLower())
                   || c.Nov.ToLower().Contains(param.sSearch.ToLower())
                   || c.Dec.ToLower().Contains(param.sSearch.ToLower())
                   || c.GrossAmount.ToString().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filtered = LogsList;
            }
            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<ArearModel, string> orderingFunction = (c => sortColumnIndex == 1 ? c.ApplicationReferenceNo + "" :
                                                              sortColumnIndex == 2 ? c.ApplicantName + "" :
                                                              sortColumnIndex == 3 ? c.Jan + "" :
                                                              sortColumnIndex == 4 ? c.Feb + "" :
                                                              sortColumnIndex == 6 ? c.Mar + "" :
                                                              sortColumnIndex == 7 ? c.Apr :
                                                              sortColumnIndex == 8 ? c.May + "" :
                                                              sortColumnIndex == 9 ? c.Jun :
                                                              sortColumnIndex == 10 ? c.Jul + "" :
                                                              sortColumnIndex == 11 ? c.Aug + "" :
                                                              sortColumnIndex == 12 ? c.Sep + "" :
                                                              sortColumnIndex == 13 ? c.Oct + "" :
                                                              sortColumnIndex == 14 ? c.Nov + "" :
                                                              sortColumnIndex == 15 ? c.Dec + "" :
                                                              sortColumnIndex == 16 ? c.GrossAmount + "" :
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
                         c.ApplicationReferenceNo + "",
                         c.ApplicantName + "",
                         c.Jan + "",
                         c.Feb + "",
                         c.Mar,
                         c.Apr,
                         c.May,
                         c.Jun,
                         c.Jul,
                         c.Aug,
                         c.Sep,
                         c.Oct,
                         c.Nov,
                         c.Dec,
                         c.GrossAmount
                   };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = LogsList.Count(),
                iTotalDisplayRecords = filtered.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetQueryCriteriaAjax()
        {
            DataSet ds = new DataSet();
            StringBuilder SQL_Str = new StringBuilder();
            //SQL_Str.Append("select case COLUMN_NAME when 'ApplicationReferenceNo' then 'Application Reference No' when 'gender'then 'Gender'else''end as [COLUMN_NAME] ,case when DATA_TYPE in ('varchar','nvarchar','text') then 'string_' + TABLE_NAME + '|' + COLUMN_NAME  when DATA_TYPE in ('int','bigint','tinyint','smallint') then 'int_' + TABLE_NAME + '|' + COLUMN_NAME when DATA_TYPE in ('smalldatetime','datetime','datetime2','date') then 'Datetime_' + TABLE_NAME + '|' + COLUMN_NAME else '' end as DATA_TYPE  from information_schema.columns where table_name = 'MasterEmployee' and Column_name in ('ApplicationReferenceNo','gender') ORDER BY COLUMN_NAME ASC  ");

            // Old Code Working Payment History Age Culom Removed

            //SQL_Str.Append("select case COLUMN_NAME when 'ApplicationReferenceNo' then 'Application Reference No' when 'gender' then 'Gender' when 'SelectDistrict' then 'District' else '' end as [COLUMN_NAME] ,case when DATA_TYPE in ('varchar','nvarchar','text') then 'string_' + TABLE_NAME + '|' + COLUMN_NAME  when DATA_TYPE in ('int','bigint','tinyint','smallint') then 'int_' + TABLE_NAME + '|' + COLUMN_NAME when DATA_TYPE in ('smalldatetime','datetime','datetime2','date') then 'Datetime_' + TABLE_NAME + '|' + COLUMN_NAME else '' end as DATA_TYPE  from information_schema.columns where table_name = 'MasterEmployee' and Column_name in ('ApplicationReferenceNo','gender','SelectDistrict') ORDER BY COLUMN_NAME ASC  ");

            // New Code Filter By Age Working 

            SQL_Str.Append("select case COLUMN_NAME when 'ApplicationReferenceNo' then 'Application Reference No' when 'gender' then 'Gender' when 'SelectDistrict' then 'District' when 'Age_InYears' then 'Age_InYears' else '' end as [COLUMN_NAME] ,case when DATA_TYPE in ('varchar', 'nvarchar', 'text', 'int') then 'string_' + TABLE_NAME + '|' + COLUMN_NAME  when DATA_TYPE in ('int', 'bigint', 'tinyint', 'smallint') then 'int_' + TABLE_NAME + '|' + COLUMN_NAME when DATA_TYPE in ('smalldatetime', 'datetime', 'datetime2', 'date') then 'Datetime_' + TABLE_NAME + '|' + COLUMN_NAME else '' end as DATA_TYPE  from information_schema.columns where table_name = 'MasterEmployee' and Column_name in ('ApplicationReferenceNo', 'gender', 'SelectDistrict', 'Age_InYears') ORDER BY COLUMN_NAME ASC ");


            SQL_Str.Append("select case Column_name  when 'ACCOUNT_STATUS' then 'Account Status' when 'APPLICANT_BANK_IFSC_CODE' then 'IFSC CODE' when 'BankName' then 'Bank Name' else '' end as [COLUMN_NAME] ,case when DATA_TYPE in ('varchar','nvarchar','text') then 'string_' + TABLE_NAME + '|' + COLUMN_NAME  when DATA_TYPE in  ('int','bigint','tinyint','smallint') then 'int_' + TABLE_NAME + '|' + COLUMN_NAME when DATA_TYPE in ('smalldatetime','datetime','datetime2','date')  then 'Datetime_' + TABLE_NAME + '|' + COLUMN_NAME else '' end as DATA_TYPE  from information_schema.columns where table_name = 'MasterEmpBankDetails' and column_name in ('ACCOUNT_STATUS','BankName','APPLICANT_BANK_IFSC_CODE')");

            SQL_Str.Append("select iif(Column_name = 'pay_date','Paid On','') as [COLUMN_NAME],case when DATA_TYPE in ('varchar','nvarchar','text') then 'string_' + TABLE_NAME + '|' + COLUMN_NAME when DATA_TYPE in ('int','bigint','tinyint','smallint') then 'int_' + TABLE_NAME + '|' + COLUMN_NAME when DATA_TYPE in  ('smalldatetime','datetime','datetime2','date') then 'Datetime_' + TABLE_NAME + '|' + COLUMN_NAME else '' end as DATA_TYPE   from information_schema.columns where table_name = 'Process_DirectDeposit_Details' and column_name in ('pay_date')");

            SQL_Str.Append("select iif(Column_name = 'Description','Scheme Type','') as [COLUMN_NAME],case when DATA_TYPE in ('varchar','nvarchar','text') then  'string_' + TABLE_NAME + '|' + COLUMN_NAME when DATA_TYPE in ('int','bigint','tinyint','smallint') then 'int_' + TABLE_NAME + '|' + COLUMN_NAME when DATA_TYPE in ('smalldatetime','datetime','datetime2','date') then 'Datetime_' + TABLE_NAME + '|' + COLUMN_NAME else '' end as DATA_TYPE   from information_schema.columns where table_name = 'MasterEmpType' and column_name in ('Description')");

            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(SQL_Str.ToString(), con);
            da.TableMappings.Add("Table", "Pensioner");
            da.TableMappings.Add("Table1", "Pensioner Bank Details");
            da.TableMappings.Add("Table2", "Pensioner Deposit Details");
            da.TableMappings.Add("Table3", "Pensioner Scheme Type");
            da.Fill(ds);
            List<(string, List<(string, string)>)> list = new List<(string, List<(string, string)>)>();
            foreach (DataTable Tbl in ds.Tables)
            {
                var CriteriaINfo = new List<(string, string)>();
                foreach (DataRow row in Tbl.Rows)
                    CriteriaINfo.Add((row[0].ToString(), row[1].ToString()));
                if (CriteriaINfo.Count() > 0)
                    list.Add((Tbl.TableName, CriteriaINfo));
            }
            string Crit_Info = JsonConvert.SerializeObject(list);
            return Json(Crit_Info, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PensionerSearchAjax(string seletedType, string Gender)
        {
            string GenderType = string.Empty;
            switch (Gender.ToLower().Trim())
            {
                case "male":
                    GenderType = "M";
                    break;
                case "female":
                    GenderType = "F";
                    break;
                case "transgender":
                    GenderType = "T";
                    break;
            }
            DataSet ds = new DataSet();
            StringBuilder SQL_str = new StringBuilder();
            int UserId = AppUserManager.GetUserId();
            int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            SQL_str.Append(" with cte as(select empl_code,type_code,Gender,CONCAT(NULLIF(isnull(first_name,''), ''), CASE   ");
            SQL_str.Append(" WHEN middle_name IS NOT NULL AND middle_name != '' THEN ' ' + middle_name  ");
            SQL_str.Append(" ELSE '' END, CASE WHEN last_name IS NOT NULL AND last_name != '' THEN ' ' + last_name  ");
            SQL_str.Append(" ELSE '' END ) as ApplicantName, from MasterEmployee left join MasterDistrict md on md.[Name] = MasterEmployee.SelectDistrict where md.Id in (select DistrictId from SecRoleLocationModule where RoleId = " + RoleId + " and UserId = " + UserId + ")) ");
            SQL_str.Append("select * from cte where type_code = '" + seletedType.Trim().Replace("'", "''") + "' and Gender = '" + GenderType.Trim().Replace("'", "''") + "' ");
            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(SQL_str.ToString(), con);
            da.Fill(ds);
            var BeneficiariesList = ds.Tables[0].AsEnumerable().Select(dataRow => new
            {
                Key = dataRow.Field<string>("empl_code"),
                Value = dataRow.Field<string>("ApplicantName")
            }).ToList();
            return Json(BeneficiariesList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BeneficiariesSearchAjax(string seletedType)
        {
            try
            {
                int UserId = AppUserManager.GetUserId();
                int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
                var RoleDistrict = db.SecRoleLocationModule.Where(x => x.RoleID == RoleId && x.UserId == UserId).Select(x => x.DistrictID).Distinct().ToList();
                var query = (from md in db.MasterBeneficiariesDetails
                             join mt in db.MasterEmpType on md.SelectPensionType equals mt.Description
                             join mdi in db.MasterDistrict on md.SelectDistrict.ToLower().Trim() equals mdi.Name.ToLower().Trim()
                             where mt.Type_Code == seletedType && RoleDistrict.Contains(mdi.Id) //&& (string.IsNullOrEmpty(Gender) ? false : md.Gender.ToLower().Trim() == Gender.ToLower().Trim())
                             select new
                             {
                                 Key = md.Id,
                                 Value = string.Concat((md.NameOfTheApplicant == null ? "" : md.NameOfTheApplicant) + "-" + (md.PermanentDistrict == null ? "" : md.PermanentDistrict) + "-" + (md.PermanentTehsil == null ? "" : md.PermanentTehsil))
                             }).ToList();
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
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

        public ActionResult MonthlyFailedTransactions()
        {
            ViewBag.DistrictId = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.Group = GetUsersAssignedLocations();
            ViewBag.Gender = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.EmployeeId = new SelectList(Enumerable.Empty<SelectListItem>());
            List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = new List<DVOMasterEmployee>();//BLLMasterEmployee.GetAllData();
            var defaultItem = new SelectListItem { Value = "ALL", Text = "ALL | ALL" };
            var emplCodeList = db.MasterEmpType
                .Where(x => x.Type_Code != null)
                .Select(x => new SelectListItem
                {
                    Value = x.Type_Code,
                    Text = x.Type_Code + " | " + x.Description
                }).ToList();
            emplCodeList.Insert(0, defaultItem); // Insert the default item at the beginning
            ViewBag.EmplCode = emplCodeList;
            return View();
            //ViewBag.EmplCode = db.MasterEmpType.Where(x => x.Type_Code != null).Select(x => new SelectListItem
            //{
            //  Value = x.Type_Code,
            //  Text = x.Type_Code + " | " + x.Description
            //}).ToList();
            ////new SelectList(listSearchResultDVOMasterEmployee, "EmplCode", "FirstName");
            //return View();

            ;
            //var employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            //ViewBag.EmployeeId = new SelectList(employer, "Id", "EmployerName");
            //List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = new List<DVOMasterEmployee>();//BLLMasterEmployee.GetAllData();
            //ViewBag.EmplCode = new SelectList(listSearchResultDVOMasterEmployee, "EmplCode", "FirstName");
            //return View();
        }
        public ActionResult PaymentHistoryAjax(JQueryDataTableParamModel param, string WhereClause)
        {
            if (!string.IsNullOrEmpty(WhereClause))
            {
                int PageNumber = 1;
                int PageSize = 10;

                PageNumber = param.iDisplayStart;
                PageSize = param.iDisplayLength;

                var AjaxList = "List";
                int UserId = AppUserManager.GetUserId();
                int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
                DataSet ds = new DataSet();
                StringBuilder SQL = new StringBuilder();
                SQL.Append(" with cte as(select empl_code,ApplicationReferenceNo,type_code,  CAST(DATEDIFF(YEAR, birthdate, GETDATE())  AS VARCHAR(10)) AS Age_InYears,Gender,concat(nullif(isnull(PresentVillageName,''),''), ");
                SQL.Append(" case when PresentHalqaPanchayatOrMunicipalityName is not null or PresentHalqaPanchayatOrMunicipalityName != '' ");
                SQL.Append(" then ' ' + PresentHalqaPanchayatOrMunicipalityName else '' end, case when PresentTehsil is not null or PresentTehsil != '' ");
                SQL.Append(" then ' ' + PresentTehsil else '' end, case when PresentDistrict is not null or PresentDistrict != '' then ");
                SQL.Append(" ' ' + PresentDistrict else '' end) as [Address], CONCAT(NULLIF(isnull(first_name,''), ''), CASE ");
                SQL.Append(" WHEN middle_name IS NOT NULL AND middle_name != '' THEN ' ' + middle_name ");
                SQL.Append(" ELSE '' END, CASE WHEN last_name IS NOT NULL AND last_name != '' THEN ' ' + last_name ");
                SQL.Append(" ELSE '' END ) as ApplicantName, SelectDistrict from MasterEmployee LEFT JOIN MasterDistrict on MasterDistrict.[Name] = MasterEmployee.SelectDistrict where MasterDistrict.Id in (select DistrictId from SecRoleLocationModule where RoleId = " + RoleId + " and UserId = " + UserId + ") )select distinct me.empl_code,me.Gender,me.Age_InYears,me.ApplicantName,et.[Description] as SchemeType, ");
                SQL.Append(" me.ApplicationReferenceNo as ApprovalDate,me.[Address],eb.bank_acct_no as [AccountNo],eb.BankName,eb.APPLICANT_BANK_IFSC_CODE as [IFSC_Code], ");
                SQL.Append(" pe.pay_date as PaidOn,pe.amount as [Amount],isnull(pe.[Status],'Status Not Updated') as [Status],CONCAT(isnull(pe.[Reason/Remarks],''),' ',(CASE WHEN isnull(pe.[Status],'Status Not Updated') = 'OK' AND ISNULL(Process_PayIncomes.amount, 0) != 0 THEN CONCAT('( Arrear : ',CAST(FORMAT(ISNULL(Process_PayIncomes.amount, 0), 'N2') AS NVARCHAR(50)), ')') ELSE '' END) )  as [Reason], SelectDistrict as [District], pe.pay_doc_no AS PayDocNo, pe.[TransactionRefrenceNo.] as TransactionRefrenceNo from cte me ");
                SQL.Append(" left join Process_DirectDeposit_Details pe on  pe.empl_code = me.empl_code LEFT JOIN Process_PayIncomes ON Process_PayIncomes.doc_no = pe.pay_doc_no AND Process_PayIncomes.inc_code = (SELECT TOP 1 inc_code FROM MasterIncCodes WHERE [description] = 'Arrear') ");
                SQL.Append(" left join MasterEmpBankDetails eb on eb.empl_code = me.empl_code ");
                SQL.Append(" left join MasterEmpType et on et.type_code = me.type_code ");
                SQL.Append(" left join Process_PayEmployee PPE ON PPE.doc_no = pe.pay_doc_no AND PPE.ok_to_post = 'P' ");
                SQL.Append(" AND PPE.print_check = 'N'");
                SQL.Append(" AND PPE.deposit = 'Y' ");
                if (!string.IsNullOrEmpty(WhereClause))
                {
                    SQL.Append(" where " + WhereClause);
                }

                StringBuilder NewSQL = new StringBuilder();
                NewSQL.Append(" with cte as(select empl_code,ApplicationReferenceNo,type_code,  CAST(DATEDIFF(YEAR, birthdate, GETDATE())  AS VARCHAR(10)) AS Age_InYears,Gender,concat(nullif(isnull(PresentVillageName,''),''), ");
                NewSQL.Append(" case when PresentHalqaPanchayatOrMunicipalityName is not null or PresentHalqaPanchayatOrMunicipalityName != '' ");
                NewSQL.Append(" then ' ' + PresentHalqaPanchayatOrMunicipalityName else '' end, case when PresentTehsil is not null or PresentTehsil != '' ");
                NewSQL.Append(" then ' ' + PresentTehsil else '' end, case when PresentDistrict is not null or PresentDistrict != '' then ");
                NewSQL.Append(" ' ' + PresentDistrict else '' end) as [Address], CONCAT(NULLIF(isnull(first_name,''), ''), CASE ");
                NewSQL.Append(" WHEN middle_name IS NOT NULL AND middle_name != '' THEN ' ' + middle_name ");
                NewSQL.Append(" ELSE '' END, CASE WHEN last_name IS NOT NULL AND last_name != '' THEN ' ' + last_name ");
                NewSQL.Append(" ELSE '' END ) as ApplicantName, SelectDistrict from MasterEmployee LEFT JOIN MasterDistrict on MasterDistrict.[Name] = MasterEmployee.SelectDistrict where MasterDistrict.Id in (select DistrictId from SecRoleLocationModule where RoleId = " + RoleId + " and UserId = " + UserId + ") )select distinct me.empl_code,me.Gender,me.Age_InYears,me.ApplicantName,et.[Description] as SchemeType, ");
                NewSQL.Append(" me.ApplicationReferenceNo as ApprovalDate,me.[Address],eb.bank_acct_no as [AccountNo],eb.BankName,eb.APPLICANT_BANK_IFSC_CODE as [IFSC_Code], ");
                NewSQL.Append(" pe.pay_date as PaidOn,pe.amount as [Amount],isnull(pe.[Status],'Status Not Updated') as [Status],CONCAT(isnull(pe.[Reason/Remarks],''),' ',(CASE WHEN isnull(pe.[Status],'Status Not Updated') = 'OK' AND ISNULL(Process_PayIncomes.amount, 0) != 0 THEN CONCAT('( Arrear : ',CAST(FORMAT(ISNULL(Process_PayIncomes.amount, 0), 'N2') AS NVARCHAR(50)), ')') ELSE '' END) )  as [Reason], SelectDistrict as [District], pe.pay_doc_no AS PayDocNo, pe.[TransactionRefrenceNo.] as TransactionRefrenceNo, COUNT(*) OVER() AS TotalRecordCount from cte me ");
                NewSQL.Append(" left join Process_DirectDeposit_Details pe on  pe.empl_code = me.empl_code LEFT JOIN Process_PayIncomes ON Process_PayIncomes.doc_no = pe.pay_doc_no AND Process_PayIncomes.inc_code = (SELECT TOP 1 inc_code FROM MasterIncCodes WHERE [description] = 'Arrear') ");
                NewSQL.Append(" left join MasterEmpBankDetails eb on eb.empl_code = me.empl_code ");
                NewSQL.Append(" left join MasterEmpType et on et.type_code = me.type_code ");
                NewSQL.Append(" left join Process_PayEmployee PPE ON PPE.doc_no = pe.pay_doc_no AND PPE.ok_to_post = 'P' ");
                NewSQL.Append(" AND PPE.print_check = 'N'");
                NewSQL.Append(" AND PPE.deposit = 'Y' ");
                if (!string.IsNullOrEmpty(WhereClause))
                {
                    NewSQL.Append(" where " + WhereClause);
                }

                string financialYearStartDate = GetStartDateDateAsPerFinancialYear().ToString("yyyy-MM-dd");
                string financialYearEndDate = GetEndDateDateAsPerFinancialYear().ToString("yyyy-MM-dd");

                NewSQL.Append("AND pe.pay_date is not null and pe.pay_date between '" + financialYearStartDate + "' and '" + financialYearEndDate + "' ");

                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    param.sSearch = param.sSearch.Trim().ToLower();
                    if (string.IsNullOrEmpty(WhereClause))
                    {
                        NewSQL.Append(" where ");
                    }
                    else
                    {
                        NewSQL.Append(" and ");
                    }
                    NewSQL.Append(" (");

                    NewSQL.Append(" LOWER(TRIM(ISNULL(me.ApplicationReferenceNo,''))) like '" + param.sSearch + "%' or LOWER(TRIM(ISNULL(me.ApplicantName,''))) like '" + param.sSearch + "%' ");
                    NewSQL.Append(" or LOWER(TRIM(ISNULL(eb.bank_acct_no,''))) like '" + param.sSearch + "%' or LOWER(TRIM(ISNULL(eb.BankName,''))) like '" + param.sSearch + "%'  ");
                    NewSQL.Append(" or LOWER(TRIM(ISNULL(eb.APPLICANT_BANK_IFSC_CODE,''))) like '" + param.sSearch + "%' or LOWER(TRIM(ISNULL(me.Gender,''))) like '" + param.sSearch + "%'  ");
                    NewSQL.Append(" or LOWER(TRIM(ISNULL(me.Age_InYears,''))) like '" + param.sSearch + "%' or LOWER(TRIM(ISNULL(et.[Description],''))) like '" + param.sSearch + "%'  ");
                    NewSQL.Append(" or LOWER(TRIM(ISNULL(CAST(pe.amount as varchar),''))) like '" + param.sSearch + "%' or LOWER(TRIM(ISNULL(pe.[Status],'Status Not Updated'))) like '" + param.sSearch + "%'  ");
                    NewSQL.Append(" or LOWER(TRIM(CONCAT(isnull(pe.[Reason/Remarks],''),' ',(CASE WHEN isnull(pe.[Status],'Status Not Updated') = 'OK' AND ISNULL(Process_PayIncomes.amount, 0) != 0 THEN CONCAT('( Arrear : ',CAST(FORMAT(ISNULL(Process_PayIncomes.amount, 0), 'N2') AS NVARCHAR(50)), ')') ELSE '' END) ))) like '" + param.sSearch + "%'  ");
                    NewSQL.Append(" or LOWER(TRIM(ISNULL(me.SelectDistrict,''))) like '" + param.sSearch + "%' or LOWER(TRIM(ISNULL(pe.[TransactionRefrenceNo.],''))) like '" + param.sSearch + "%'   ");

                    NewSQL.Append(" )");
                }
                // page size
                int fetchRecords = (PageNumber - 1) * PageSize;
                NewSQL.AppendLine(" ORDER BY me.empl_code OFFSET " + PageNumber + " ROWS FETCH NEXT " + PageSize + " ROWS ONLY;");

                 string con = ConnectionStringProvider.GetConnectionString();
                //SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
                SqlDataAdapter da = new SqlDataAdapter(NewSQL.ToString(), con);
                da.SelectCommand.CommandTimeout = 300;
                da.Fill(ds);

                TempData["HistoryList"] = "True";
                if (AjaxList == "List")
                {
                    var dataList = ds.Tables[0].AsEnumerable().Select(row => new
                    {
                        ApplicationReferenceNo = row["ApprovalDate"] != DBNull.Value ? row["ApprovalDate"].ToString() : "",
                        ApplicantName = row["ApplicantName"] != DBNull.Value ? row["ApplicantName"].ToString() : "",
                        AccountNo = row["AccountNo"] != DBNull.Value ? row["AccountNo"].ToString() : "",
                        BankName = row["BankName"] != DBNull.Value ? row["BankName"].ToString() : "",
                        IFSC_Code = row["IFSC_Code"] != DBNull.Value ? row["IFSC_Code"].ToString() : "",
                        Gender = row["Gender"] != DBNull.Value ? row["Gender"].ToString() : "",
                        Age_InYears = row["Age_InYears"] != DBNull.Value ? row["Age_InYears"].ToString() : "",
                        SchemeType = row["SchemeType"] != DBNull.Value ? row["SchemeType"].ToString() : "",
                        Amount = row["Amount"] != DBNull.Value ? row["Amount"].ToString() : "",
                        PaidOn = row["PaidOn"] != DBNull.Value ? row["PaidOn"].ToString() : "",
                        Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : "",
                        Reason = row["Reason"] != DBNull.Value ? row["Reason"].ToString() : "",
                        EmplCode = row["empl_code"] != DBNull.Value ? row["empl_code"].ToString() : "",
                        District = row["District"] != DBNull.Value ? row["District"].ToString() : "",
                        PayDocNo = row["PayDocNo"] != DBNull.Value ? row["PayDocNo"].ToString() : "",
                        TransactionRefrenceNo = row["TransactionRefrenceNo"] != DBNull.Value ? row["TransactionRefrenceNo"].ToString() : "",
                        TotalRecordCount = row["TotalRecordCount"] != DBNull.Value ? row["TotalRecordCount"].ToString() : "",
                    }).ToList();
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.HttpContext.Session["ReportName"] = "rptPaymentHistory.rpt";
                        this.HttpContext.Session["ReportName1"] = Path.Combine(Server.MapPath("~/Reports/rptPaymentHistory.rpt"));
                        this.HttpContext.Session["rptSource"] = ds;
                    }

                    //var filtered = dataList;
                    //var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);
                    string totalRecords = dataList.Select(x => x.TotalRecordCount).FirstOrDefault();
                    var result = from c in dataList
                                 select new[] {
                             c.District?.Trim().ToUpper()+"",
                             c.ApplicationReferenceNo+"",
                             c.ApplicantName+"",
                             c.AccountNo + "",
                             c.BankName + "",
                             c.IFSC_Code + "",
                             c.Gender + "",
                             c.Age_InYears + "",
                             c.SchemeType + "",
                             c.Amount + "",
                             c.PaidOn + "",
                             c.Status + "",
                             c.Reason + "",
                             c.TransactionRefrenceNo + "",
                             c.EmplCode + "",
                             c.PayDocNo + "",
                             };

                    return Json(
                                      new
                                      {
                                          sEcho = param.sEcho,
                                          iTotalRecords = Convert.ToInt32(totalRecords),
                                          iTotalDisplayRecords = Convert.ToInt32(totalRecords),
                                          aaData = result
                                      }, JsonRequestBehavior.AllowGet);
                }

                //return Json(dataList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(
                                 new
                                 {
                                     sEcho = param.sEcho,
                                     iTotalRecords = 0,
                                     iTotalDisplayRecords = 0,
                                     aaData = new List<string[]>(),
                                 }, JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }
        //Download excel PaymentHistoryExcel 
        public FileContentResult PaymentHistoryExcelAjax(string WhereClause)
        {
            int UserId = AppUserManager.GetUserId();
            int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append(" with cte as(select empl_code,ApplicationReferenceNo,type_code,CAST(DATEDIFF(YEAR, birthdate, GETDATE())  AS VARCHAR(10)) AS Age_InYears,Gender,concat(nullif(isnull(PresentVillageName,''),''), ");
            SQL.Append(" case when PresentHalqaPanchayatOrMunicipalityName is not null or PresentHalqaPanchayatOrMunicipalityName != '' ");
            SQL.Append(" then ' ' + PresentHalqaPanchayatOrMunicipalityName else '' end, case when PresentTehsil is not null or PresentTehsil != '' ");
            SQL.Append(" then ' ' + PresentTehsil else '' end, case when PresentDistrict is not null or PresentDistrict != '' then ");
            SQL.Append(" ' ' + PresentDistrict else '' end) as [Address], CONCAT(NULLIF(isnull(first_name,''), ''), CASE ");
            SQL.Append(" WHEN middle_name IS NOT NULL AND middle_name != '' THEN ' ' + middle_name ");
            SQL.Append(" ELSE '' END, CASE WHEN last_name IS NOT NULL AND last_name != '' THEN ' ' + last_name ");
            SQL.Append(" ELSE '' END ) as ApplicantName from MasterEmployee LEFT JOIN MasterDistrict on MasterDistrict.[Name] = MasterEmployee.SelectDistrict where MasterDistrict.Id in (select DistrictId from SecRoleLocationModule where RoleId = " + RoleId + " and UserId = " + UserId + ") )select distinct me.empl_code,me.Gender,me.Age_InYears,me.ApplicantName,et.[Description] as SchemeType, ");
            SQL.Append(" me.ApplicationReferenceNo as ApprovalDate,me.[Address],eb.bank_acct_no as [AccountNo],eb.BankName,eb.APPLICANT_BANK_IFSC_CODE as [IFSC_Code], ");
            SQL.Append(" pe.pay_date as PaidOn,pe.amount as [Amount],isnull(pe.[Status],'Status Not Updated') as [Status],isnull(pe.[Reason/Remarks],'') as [Reason] from cte me ");
            SQL.Append(" left join Process_DirectDeposit_Details pe on  pe.empl_code = me.empl_code ");
            SQL.Append(" left join MasterEmpBankDetails eb on eb.empl_code = me.empl_code ");
            SQL.Append(" left join MasterEmpType et on et.type_code = me.type_code ");
            SQL.Append(" left join Process_PayEmployee PPE ON PPE.doc_no = pe.pay_doc_no AND PPE.ok_to_post = 'P' ");
            SQL.Append(" AND PPE.print_check = 'N'");
            SQL.Append(" AND PPE.deposit = 'Y' ");
            if (!string.IsNullOrEmpty(WhereClause))
                SQL.Append(" where " + WhereClause);
            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sheet1");
                    worksheet.Cell(1, 1).InsertTable(ds.Tables[0]);
                    using (var stream = new System.IO.MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        stream.Position = 0;
                        string fileName = "PaymentHistory.xlsx";
                        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                        return File(stream.ToArray(), contentType, fileName);
                    }
                }
            }

            return null;

            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //  this.HttpContext.Session["ReportName"] = "rptPaymentHistory.rpt";
            //  this.HttpContext.Session["rptSource"] = ds;
            //  return Json("1", JsonRequestBehavior.AllowGet);
            //}
            //return Json("0", JsonRequestBehavior.AllowGet);
        }


        //Download Pdf PaymentHistoryPdf 


        public FileContentResult PaymentHistoryPdfAjax(string WhereClause)
        {
            int UserId = AppUserManager.GetUserId();
            int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append(" with cte as(select empl_code,ApplicationReferenceNo,type_code,CAST(DATEDIFF(YEAR, birthdate, GETDATE())  AS VARCHAR(10)) AS Age_InYears,Gender,concat(nullif(isnull(PresentVillageName,''),''), ");
            SQL.Append(" case when PresentHalqaPanchayatOrMunicipalityName is not null or PresentHalqaPanchayatOrMunicipalityName != '' ");
            SQL.Append(" then ' ' + PresentHalqaPanchayatOrMunicipalityName else '' end, case when PresentTehsil is not null or PresentTehsil != '' ");
            SQL.Append(" then ' ' + PresentTehsil else '' end, case when PresentDistrict is not null or PresentDistrict != '' then ");
            SQL.Append(" ' ' + PresentDistrict else '' end) as [Address], CONCAT(NULLIF(isnull(first_name,''), ''), CASE ");
            SQL.Append(" WHEN middle_name IS NOT NULL AND middle_name != '' THEN ' ' + middle_name ");
            SQL.Append(" ELSE '' END, CASE WHEN last_name IS NOT NULL AND last_name != '' THEN ' ' + last_name ");
            SQL.Append(" ELSE '' END ) as ApplicantName from MasterEmployee LEFT JOIN MasterDistrict on MasterDistrict.[Name] = MasterEmployee.SelectDistrict where MasterDistrict.Id in (select DistrictId from SecRoleLocationModule where RoleId = " + RoleId + " and UserId = " + UserId + ") )select distinct me.empl_code,me.Gender,me.Age_InYears,me.ApplicantName,et.[Description] as SchemeType, ");
            SQL.Append(" me.ApplicationReferenceNo as ApprovalDate,me.[Address],eb.bank_acct_no as [AccountNo],eb.BankName,eb.APPLICANT_BANK_IFSC_CODE as [IFSC_Code], ");
            SQL.Append(" pe.pay_date as PaidOn,pe.amount as [Amount],isnull(pe.[Status],'Status Not Updated') as [Status],isnull(pe.[Reason/Remarks],'') as [Reason] from cte me ");
            SQL.Append(" left join Process_DirectDeposit_Details pe on  pe.empl_code = me.empl_code ");
            SQL.Append(" left join MasterEmpBankDetails eb on eb.empl_code = me.empl_code ");
            SQL.Append(" left join MasterEmpType et on et.type_code = me.type_code ");
            SQL.Append(" left join Process_PayEmployee PPE ON PPE.doc_no = pe.pay_doc_no AND PPE.ok_to_post = 'P' ");
            SQL.Append(" AND PPE.print_check = 'N'");
            SQL.Append(" AND PPE.deposit = 'Y' ");
            if (!string.IsNullOrEmpty(WhereClause))
                SQL.Append(" where " + WhereClause);
            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);
            var dataList = ds.Tables[0].AsEnumerable().Select(row => new
            {
                ApplicantName = row["ApplicantName"] != DBNull.Value ? row["ApplicantName"].ToString() : "",
                AccountNo = row["AccountNo"] != DBNull.Value ? row["AccountNo"].ToString() : "",
                BankName = row["BankName"] != DBNull.Value ? row["BankName"].ToString() : "",
                IFSC_Code = row["IFSC_Code"] != DBNull.Value ? row["IFSC_Code"].ToString() : "",
                Gender = row["Gender"] != DBNull.Value ? row["Gender"].ToString() : "",
                Age_InYears = row["Age_InYears"] != DBNull.Value ? row["Age_InYears"].ToString() : "",
                SchemeType = row["SchemeType"] != DBNull.Value ? row["SchemeType"].ToString() : "",
                Amount = row["Amount"] != DBNull.Value ? row["Amount"].ToString() : "",
                PaidOn = row["PaidOn"] != DBNull.Value ? row["PaidOn"].ToString() : "",
                Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : "",
                Reason = row["Reason"] != DBNull.Value ? row["Reason"].ToString() : ""
            }).ToList();
            DataTable dataTable = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
            StringBuilder htmlString = new StringBuilder();
            htmlString.Append(@"<html>
                                    <head>

                                    </head>
                                    <body>
                                        <div class='row' style='background-color:lightgreen'>
                                        <div class='col-md-12'>
                                        <div style='text-align:center;font-size:80px;font-weight:bold;'>Payment History Report</div>
                                           </div>
                                         </div>
                                        <table style='td{border:1px};font-size:40px;' border='' cellspacing='30' cellpadding=''>
                                         
                                            <tr>
                                                       <th style='font-weight: bold;'>ApplicantName</th>
                                                        <th style=' font-weight: bold;'>AccountNo</th>
                                                        <th style=' font-weight: bold;'>BankName</th>
                                                        <th style=' font-weight: bold;'>IFSC_Code</th>
                                                        <th style=' font-weight: bold;'>Gender</th>
                                                        <th style=' font-weight: bold;'>Age_InYears</th>
                                                        <th style=' font-weight: bold;'>SchemeType</th>
                                                        <th style=' font-weight: bold;'>Amount</th>
                                                        <th style=' font-weight: bold;'>PaidOn</th>
                                                        <th style=' font-weight: bold;'>Status</th>
                                                        <th style=' font-weight: bold;'>Reason</th>
                                               
                                            </tr>");

            foreach (var item in dataList)
            {
                htmlString.AppendFormat(@"<tr>
                                            <td>{0}</td>
                                            <td>{1}</td>
                                            <td>{2}</td>
                                            <td>{3}</td>
                                            <td>{4}</td>
                                            <td>{5}</td>
                                            <td>{6}</td>
                                            <td>{7}</td>
                                            <td>{8}</td>
                                            <td>{9}</td>
                                            <td>{10}</td>
                                           
                                            
                                          </tr>",
                              item.ApplicantName, //0 
                              item.AccountNo, //1
                              item.BankName, //2
                              item.IFSC_Code,  //3
                              item.Gender,  //4
                              item.Age_InYears,  //5
                              item.SchemeType,  //6
                              item.Amount,
                              item.PaidOn,//7
                              item.Status,//8
                              item.Reason   //


                                         );
            }

            htmlString.Append(@"</table>
        </body>
        </html>");
            try
            {
                string pathFolder = HttpContext.Server.MapPath("~/App.Web/PaymentHistory");
                Byte[] res = null;
                if (!Directory.Exists(pathFolder))
                {
                    Directory.CreateDirectory(pathFolder);
                }
                string baseUrl = GetBaseUrl();
                string filePath = Path.Combine(pathFolder, "PaymentHistoryreport.pdf");
                string pdfUrl = $"{baseUrl}/App.Web/PaymentHistory/PaymentHistoryreport.pdf";

                using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        var htm = htmlString.ToString();
                        //var pdf = PdfGenerator.GeneratePdf(htmlString.ToString(), PdfSharp.PageSize.A4);
                        //var pdf = PdfGenerator.GeneratePdf(htmlString.ToString(), (PdfSharp.PageSize)(int)PageOrientation.Portrait);
                        // pdf.Save(ms);
                        res = ms.ToArray();

                        file.Write(res, 0, res.Length);
                        ms.Close();
                    }
                    file.Close();
                }

                return File(res, "application/pdf", "PaymentHistoryreport.pdf");
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        private string GetBaseUrl()
        {
            var request = HttpContext.Request;
            var baseUrl = $"{request.Url.Scheme}://{request.Url.Authority}";
            return baseUrl;
        }

        public JsonResult PensionByMonthAjax(string emplrId, string empl_code, string date1, string date2, string gender, int? ageInYears, string RegionNames, string finacialYear)
        {
            string GenderType = string.Empty;
            string fyear = finacialYear.Replace("_", "-");
            switch (gender.ToLower().Trim())
            {
                case "male":
                    GenderType = "M";
                    break;
                case "female":
                    GenderType = "F";
                    break;
                case "transgender":
                    GenderType = "T";
                    break;
            }

            // get current date1 and date2 as per financial year if date1 or date2 is null or empty
            if (string.IsNullOrEmpty(date1))
            {
                date1 = GetStartDateDateAsPerFinancialYear().ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(date2))
            {
                date2 = GetEndDateDateAsPerFinancialYear().ToString("yyyy-MM-dd");
            }

            int UserId = AppUserManager.GetUserId();
            int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append("select distinct ApplicationReferenceno,CONCAT(NULLIF(isnull(me.first_name,''), ''), CASE  ");
            SQL.Append("WHEN me.middle_name IS NOT NULL AND me.middle_name != '' THEN ' ' + me.middle_name ELSE '' END, CASE WHEN me.last_name IS NOT NULL AND me.last_name != '' THEN ' ' + me.last_name  ");
            SQL.Append("ELSE '' END ) as ApplicantName,CONVERT(varchar, dd.pay_date, 103) AS pay_date,dd.amount,et.[Description] as type_code,CAST(DATEDIFF(YEAR, birthdate, GETDATE())  AS VARCHAR(10)) AS Age_InYears,Gender,concat(nullif(isnull(PresentVillageName,''),''), case when PresentHalqaPanchayatOrMunicipalityName is not null or PresentHalqaPanchayatOrMunicipalityName != '' ");
            SQL.Append("then ' ' + PresentHalqaPanchayatOrMunicipalityName else '' end, case when PresentTehsil is not null or PresentTehsil != '' ");
            SQL.Append("then ' ' + PresentTehsil else '' end, case when PresentDistrict is not null or PresentDistrict != '' then ");
            SQL.Append("' ' + PresentDistrict else '' end) as [Address],dd.bank_acct_no,BankName,APPLICANT_BANK_IFSC_CODE as [IFSCCode],dd.[Status],me.SelectDistrict as [District],MONTH(PAY_DATE) AS MONTH,YEAR(PAY_DATE) AS YEAR, '" + fyear + "' AS FinacialYear, dd.[Reason/Remarks], dd.[TransactionRefrenceNo.] as TransactionRefrenceNo , CONVERT(varchar, dd.TransactionDate, 103) as TransactionDate  from Process_DirectDeposit_Details dd ");
            SQL.Append("left join masterEmployee me on me.Empl_code = dd.Empl_code left join MasterEmpBankDetails mb on mb.empl_code = me.Empl_code ");
            SQL.Append(" left join MasterEmpType et on me.type_code = et.type_code ");
            SQL.Append("left join MasterDistrict md on md.[Name] = me.SelectDistrict where Status = 'ok' and md.Id in (select DistrictId from SecRoleLocationModule where RoleId = " + RoleId + " and UserId = " + UserId + ") ");
            if (!string.IsNullOrEmpty(emplrId) && emplrId == "ALL")
                SQL.Append("");
            //SQL.Append(" AND me.Type_Code = '" + "*" +  "'");
            else if (!string.IsNullOrEmpty(emplrId))
                SQL.Append(" AND  me.Type_Code = '" + emplrId.Trim().Replace("'", "''") + "'");

            if (!string.IsNullOrEmpty(empl_code))
                SQL.Append(" AND dd.empl_code in (" + empl_code.Trim() + ")");

            if (!string.IsNullOrEmpty(date1))//YearTo
                SQL.Append(" AND cast(dd.pay_date as date) >= '" + date1.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(date2))//YearTo
                SQL.Append(" AND cast(dd.pay_date as date) <= '" + date2.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(gender))// Gender
                SQL.Append(" AND me.Gender = '" + GenderType.Trim() + "'");
            if (ageInYears != null && ageInYears != 0)//Age In Years
                SQL.Append(" AND CAST(DATEDIFF(YEAR, me.birthdate, GETDATE())  AS VARCHAR(10)) = '" + ageInYears + "'");
            if (!string.IsNullOrEmpty(RegionNames))
                SQL.Append(" AND me.SelectDistrict in (SELECT * FROM [SplitString] ('" + RegionNames.Trim() + "'))");
            //if (!string.IsNullOrEmpty(emplrId))
            //  SQL.Append("where dd.empl_code = " + emplrId + " and Status = 'ok'");
            //SQL.Append("where dd.empl_code = "+ emplrId + "  and Status = 'ok'");


            //string con = WebConfigurationManager.AppSettings["SQLConn"];

            string con = ConnectionStringProvider.GetConnectionString();
         
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.SelectCommand.CommandTimeout = 300;
            da.Fill(ds);
            //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/reports/DsPaymentSuccess.xsd"));
            //ds.WriteXmlSchema(writer);
            //writer.Close();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //this.HttpContext.Session["ReportName"] = "rptPaymentSuccessReport.rpt";
                this.HttpContext.Session["ReportName"] = "rptPaymentSuccessReport.rpt";
                this.HttpContext.Session["ReportName1"] = Path.Combine(Server.MapPath("~/Reports/rptPaymentSuccessReport.rpt"));
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }


        //public JsonResult PaymentSuccess(string emplrId, string empl_code, string date1, string date2, string gender, int? ageInYears)
        //{
        //  string GenderType = string.Empty;
        //  switch (gender.ToLower().Trim())
        //  {
        //    case "male":
        //      GenderType = "M";
        //      break;
        //    case "female":
        //      GenderType = "F";
        //      break;
        //    case "transgender":
        //      GenderType = "T";
        //      break;
        //  }

        //  DataSet ds = new DataSet();
        //  StringBuilder SQL = new StringBuilder();
        //  SQL.Append("select ApplicationReferenceno,CONCAT(NULLIF(isnull(me.first_name,''), ''), CASE  ");
        //  SQL.Append("WHEN me.middle_name IS NOT NULL AND me.middle_name != '' THEN ' ' + me.middle_name ELSE '' END, CASE WHEN me.last_name IS NOT NULL AND me.last_name != '' THEN ' ' + me.last_name  ");
        //  SQL.Append("ELSE '' END ) as ApplicantName,dd.amount,type_code,Age_InYears,Gender,concat(nullif(isnull(PresentVillageName,''),''), case when PresentHalqaPanchayatOrMunicipalityName is not null or PresentHalqaPanchayatOrMunicipalityName != '' ");
        //  SQL.Append("then ' ' + PresentHalqaPanchayatOrMunicipalityName else '' end, case when PresentTehsil is not null or PresentTehsil != '' ");
        //  SQL.Append("then ' ' + PresentTehsil else '' end, case when PresentDistrict is not null or PresentDistrict != '' then ");
        //  SQL.Append("' ' + PresentDistrict else '' end) as [Address],dd.bank_acct_no,BankName,APPLICANT_BANK_IFSC_CODE as [IFSCCode],dd.[Status] from Process_DirectDeposit_Details dd ");
        //  SQL.Append("left join masterEmployee me on me.Empl_code = dd.Empl_code left join MasterEmpBankDetails mb on mb.empl_code = me.Empl_code ");
        //  SQL.Append("left join MasterDistrict md on md.[Name] = me.SelectDistrict ");
        //  SQL.Append("where dd.empl_code = 1 ");//and Status = 'ok' 
        //  string con = WebConfigurationManager.AppSettings["SQLConn"];
        //  SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
        //  da.Fill(ds);

        //  if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //  {
        //    this.HttpContext.Session["ReportName"] = "rptPaymentSuccess.rpt";
        //    this.HttpContext.Session["rptSource"] = ds;
        //    return Json("1", JsonRequestBehavior.AllowGet);
        //  }
        //  return Json("0", JsonRequestBehavior.AllowGet);
        //}


        public JsonResult PensionByYearAjax(string emplrId, string empl_code, string date1, string date2, string gender, int? ageInYears, string RegionNames, string finacialYear)
        {
            string GenderType = string.Empty;
            string fyear = finacialYear.Replace("_", "-");
            switch (gender.ToLower().Trim())
            {
                case "male":
                    GenderType = "M";
                    break;
                case "female":
                    GenderType = "F";
                    break;
                case "transgender":
                    GenderType = "T";
                    break;
            }

            // get current date1 and date2 as per financial year if date1 or date2 is null or empty
            if (string.IsNullOrEmpty(date1))
            {
                date1 = GetStartDateDateAsPerFinancialYear().ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(date2))
            {
                date2 = GetEndDateDateAsPerFinancialYear().ToString("yyyy-MM-dd");
            }

            int UserId = AppUserManager.GetUserId();
            int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append("select distinct ApplicationReferenceno,CONCAT(NULLIF(isnull(me.first_name,''), ''), CASE  ");
            SQL.Append("WHEN me.middle_name IS NOT NULL AND me.middle_name != '' THEN ' ' + me.middle_name ELSE '' END, CASE WHEN me.last_name IS NOT NULL AND me.last_name != '' THEN ' ' + me.last_name  ");
            SQL.Append("ELSE '' END ) as ApplicantName,CONVERT(varchar, dd.pay_date, 103) AS pay_date,dd.amount,et.[Description] as type_code,CAST(DATEDIFF(YEAR, birthdate, GETDATE())  AS VARCHAR(10)) AS Age_InYears,Gender,concat(nullif(isnull(PresentVillageName,''),''), case when PresentHalqaPanchayatOrMunicipalityName is not null or PresentHalqaPanchayatOrMunicipalityName != '' ");
            SQL.Append("then ' ' + PresentHalqaPanchayatOrMunicipalityName else '' end, case when PresentTehsil is not null or PresentTehsil != '' ");
            SQL.Append("then ' ' + PresentTehsil else '' end, case when PresentDistrict is not null or PresentDistrict != '' then ");
            SQL.Append("' ' + PresentDistrict else '' end) as [Address],dd.bank_acct_no,BankName,APPLICANT_BANK_IFSC_CODE as [IFSCCode],dd.[Status],me.SelectDistrict as [District],MONTH(ISNULL(TransactionDate, dd.pay_date)) AS Month,YEAR(ISNULL(TransactionDate, dd.pay_date)) AS Year, '" + fyear + "' AS FinacialYear,dd.[Reason/Remarks],[TransactionRefrenceNo.] as TransactionRefrenceNo, CONVERT(varchar, dd.TransactionDate, 103) as TransactionDate from Process_DirectDeposit_Details dd ");
            SQL.Append("left join masterEmployee me on me.Empl_code = dd.Empl_code left join MasterEmpBankDetails mb on mb.empl_code = me.Empl_code ");
            SQL.Append(" left join MasterEmpType et on me.type_code = et.type_code ");
            SQL.Append("left join MasterDistrict md on md.[Name] = me.SelectDistrict where Status = 'fail' and md.Id in (select DistrictId from SecRoleLocationModule where RoleId = " + RoleId + " and UserId = " + UserId + ")");

            //"AND 
            //if (!string.IsNullOrEmpty(emplrId) && emplrId == "ALL")
            //    SQL.Append(" AND me.Type_Code = '" + "*" + "'");

            if (!string.IsNullOrEmpty(emplrId) && emplrId == "ALL")
                SQL.Append("");
            else if (!string.IsNullOrEmpty(emplrId))
                SQL.Append(" AND  me.Type_Code = '" + emplrId.Trim().Replace("'", "''") + "'");

            //if (!string.IsNullOrEmpty(emplrId))
            //  SQL.Append(" AND  me.Type_Code = '" + emplrId.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(empl_code))
                SQL.Append(" AND dd.empl_code in (" + empl_code.Trim() + ")");
            if (!string.IsNullOrEmpty(date1))//YearTo
                SQL.Append(" AND cast(dd.pay_date as date) >= '" + date1.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(date2))//YearTo
                SQL.Append(" AND cast(dd.pay_date as date) <= '" + date2.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(gender))// Gender
                SQL.Append(" AND me.Gender = '" + GenderType.Trim() + "'");
            if (ageInYears != null && ageInYears != 0)//Age In Years
                SQL.Append(" AND CAST(DATEDIFF(YEAR, me.birthdate, GETDATE()) AS VARCHAR(10)) = '" + ageInYears + "'");
            if (!string.IsNullOrEmpty(RegionNames))
                SQL.Append(" AND me.SelectDistrict in (SELECT * FROM [SplitString] ('" + RegionNames.Trim() + "'))");
            //if (!string.IsNullOrEmpty(emplrId))
            //  SQL.Append("where dd.empl_code = "+ emplrId + " and Status = 'fail'"); 

            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();

            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);

            //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/reports/DsPaymentFailureReport.xsd"));
            //ds.WriteXmlSchema(writer);
            //writer.Close();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                /*this.HttpContext.Session["ReportName"] = "rptPaymentFailedReport.rpt";*/      //"rptPensionDetailsByYear.rpt";
                this.HttpContext.Session["ReportName"] = "rptPaymentFailedReport.rpt";      //"rptPensionDetailsByYear.rpt";
                this.HttpContext.Session["ReportName1"] = Path.Combine(Server.MapPath("~/Reports/rptPaymentFailedReport.rpt"));
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }
        public ActionResult DirectDepositsToBank()
        {
            ViewBag.Gender = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.EmployeeId = new SelectList(Enumerable.Empty<SelectListItem>());
            List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = new List<DVOMasterEmployee>();//BLLMasterEmployee.GetAllData();
            ViewBag.EmplCode = db.MasterEmpType.Where(x => x.Type_Code != null).Select(x => new SelectListItem
            {
                Value = x.Type_Code,
                Text = x.Type_Code + " | " + x.Description
            }).ToList();
            //new SelectList(listSearchResultDVOMasterEmployee, "EmplCode", "FirstName");
            return View();
            //var employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            //ViewBag.EmployeeId = new SelectList(employer, "Id", "EmployerName");
            //List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = new List<DVOMasterEmployee>();//BLLMasterEmployee.GetAllData();
            //ViewBag.EmplCode = new SelectList(listSearchResultDVOMasterEmployee, "EmplCode", "FirstName");
            //return View();
        }

        public JsonResult DirectDepositsToBankAjax(string emplrId, string empl_code, string date1, string date2, string gender, int? ageInYears)
        {
            string GenderType = string.Empty;
            switch (gender.ToLower().Trim())
            {
                case "male":
                    GenderType = "M";
                    break;
                case "female":
                    GenderType = "F";
                    break;
                case "transgender":
                    GenderType = "T";
                    break;
            }
            int UserId = AppUserManager.GetUserId();
            int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append("SELECT distinct M.ApplicationReferenceNo,M.EmployerID, M.Soc_Sec_Num, M.birthdate, isnull(M.first_name,'')+' '+isnull(M.middle_name,'')+' '+isnull(M.last_name,''), M.gender,CAST(DATEDIFF(YEAR, M.birthdate, GETDATE())  AS VARCHAR(10)) AS Age_InYears,");
            SQL.Append(" E.[Description] EmployerName, M.date_hired, PH.doc_no,PD.chk_digit, PD.pay_date, PD.bank_acct_no, PD.amount,PH.bank_code");
            SQL.Append(" FROM  Process_DirectDeposit_Details PD  WITH (NOLOCK) INNER JOIN Process_DirectDeposit_Header PH  WITH (NOLOCK) ON PD.doc_no = PH.doc_no INNER JOIN");
            SQL.Append(" MasterEmployee M  WITH (NOLOCK) ON PD.empl_code = M.Empl_Code ");
            SQL.Append(" LEFT JOIN  MasterDistrict md WITH (NOLOCK) ON md.[Name] = M.SelectDistrict  ");
            SQL.Append(" INNER JOIN MasterEmpType E  WITH (NOLOCK) ON M.type_code = E.type_code  ");
            SQL.Append(" INNER JOIN Process_PayEmployee PPE  WITH (NOLOCK) ON PD.empl_code = PPE.Empl_Code ");
            SQL.Append(" Where 1=1 and md.Id in (select DistrictId from SecRoleLocationModule where RoleId = " + RoleId + " and UserId = " + UserId + ") AND PPE.ok_to_post='P' ");
            if (!string.IsNullOrEmpty(emplrId))
                SQL.Append(" AND  M.Type_Code = '" + emplrId.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(empl_code))
                SQL.Append(" AND PD.empl_code in (" + empl_code.Trim() + ")");
            if (!string.IsNullOrEmpty(date1))//YearTo
                SQL.Append(" and cast(PD.pay_date as date) >= '" + date1.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(date2))//YearTo
                SQL.Append(" and cast(PD.pay_date as date) <= '" + date2.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(gender))// Gender
                SQL.Append(" AND M.Gender = '" + GenderType.Trim() + "'");
            if (ageInYears != null && ageInYears != 0)//Age In Years
                SQL.Append(" AND CAST(DATEDIFF(YEAR, M.birthdate, GETDATE()) -1 AS VARCHAR(10)) = '" + ageInYears + "'");

            //string con = WebConfigurationManager.AppSettings["SQLConn"];

            string con = ConnectionStringProvider.GetConnectionString();

            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.HttpContext.Session["ReportName"] = "rptDirectDepositsToBank.rpt";
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }
        public JsonResult ShowgrantedRefunds(string date1, string date2, string month, string year)
        {
            DateTime sdate1 = Convert.ToDateTime(date1);
            DateTime sdate2 = Convert.ToDateTime(date2);
            int sYear = year == "" ? 0 : Convert.ToInt32(year);
            int sMonth = month == "" ? 0 : Convert.ToInt32(month);

            var results = from x in db.MasterPensioner
                          join y in db.ProcessPayPensioner on x.PensionerID equals y.PensionerID
                          select new

                          {
                              PensionerID = x.PensionerID,
                              FirstName = x.FirstName,
                              LastName = x.LastName,
                              MidName = x.MidName,
                              PayDate = y.PayDate,
                              IncGross = y.IncGross,
                              IncNet = y.IncNet,
                              Deposit = y.Deposit

                          };



            results = results.OrderByDescending(x => x.PayDate).Take(20);
            if (results.Any())
            {
                this.HttpContext.Session["ReportName"] = "RptPensionpayments.rpt";
                this.HttpContext.Session["rptSource"] = results;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowPensionPayments(string date1, string date2, string month, string year)
        {

            DateTime sdate1 = Convert.ToDateTime(date1);
            DateTime sdate2 = Convert.ToDateTime(date2);
            int sYear = year == "" ? 0 : Convert.ToInt32(year);
            int sMonth = month == "" ? 0 : Convert.ToInt32(month);

            var results = from x in db.MasterPensioner
                          join y in db.ProcessPayPensioner on x.PensionerID equals y.PensionerID
                          select new

                          {
                              PensionerID = x.PensionerID,
                              FirstName = x.FirstName,
                              LastName = x.LastName,
                              MidName = x.MidName,
                              PayDate = y.PayDate,
                              IncGross = y.IncGross,
                              IncNet = y.IncNet,
                              Deposit = y.Deposit

                          };



            results = results.OrderByDescending(x => x.PayDate).Take(20);
            if (results.Any())
            {
                this.HttpContext.Session["ReportName"] = "RptPensionpayments.rpt";
                this.HttpContext.Session["rptSource"] = results;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowMonthlypensionsummary(string date1, string date2, string month, string year)
        {
            DateTime sdate1 = Convert.ToDateTime(date1);
            DateTime sdate2 = Convert.ToDateTime(date2);
            int sYear = year == "" ? 0 : Convert.ToInt32(year);
            int sMonth = month == "" ? 0 : Convert.ToInt32(month);

            var results = from x in db.MasterPensioner
                          join y in db.ProcessPayPensioner on x.PensionerID equals y.PensionerID
                          select new

                          {
                              PensionerID = x.PensionerID,
                              FirstName = x.FirstName,
                              LastName = x.LastName,
                              MidName = x.MidName,
                              PayDate = y.PayDate,
                              IncGross = y.IncGross,
                              IncNet = y.IncNet,
                              Deposit = y.Deposit

                          };



            results = results.OrderByDescending(x => x.PayDate).Take(20);
            if (results.Any())
            {
                this.HttpContext.Session["ReportName"] = "RptPensionpayments.rpt";
                this.HttpContext.Session["rptSource"] = results;
                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowYearlypensionSummary(string date1, string date2, string month, string year)
        {
            DateTime sdate1 = Convert.ToDateTime(date1);
            DateTime sdate2 = Convert.ToDateTime(date2);
            int sYear = year == "" ? 0 : Convert.ToInt32(year);
            int sMonth = month == "" ? 0 : Convert.ToInt32(month);

            var results = from x in db.MasterPensioner
                          join y in db.ProcessPayPensioner on x.PensionerID equals y.PensionerID
                          select new

                          {
                              PensionerID = x.PensionerID,
                              FirstName = x.FirstName,
                              LastName = x.LastName,
                              MidName = x.MidName,
                              PayDate = y.PayDate,
                              IncGross = y.IncGross,
                              IncNet = y.IncNet,
                              Deposit = y.Deposit

                          };



            results = results.OrderByDescending(x => x.PayDate).Take(20);
            if (results.Any())
            {
                this.HttpContext.Session["ReportName"] = "RptPensionpayments.rpt";
                this.HttpContext.Session["rptSource"] = results;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }


        #region AshishWorks


        public ActionResult GetAllID()
        {
            var employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            ViewBag.EmployerId = new SelectList(employer, "Id", "EmployerName");

            var contributor = db.MasterContributor.Where(x => x.IsActive == true).ToList();
            ViewBag.ContributorId = new SelectList(contributor, "Id", "FirstName");

            var pensioner = db.MasterPensioner.Where(x => x.IsActive == true).ToList();
            ViewBag.PensionerId = new SelectList(pensioner, "Id", "FirstName");


            var month = db.MasterMonthName.Where(x => x.IsActive == true).ToList();
            ViewBag.Month = new SelectList(month, "Id", "Name");

            return View();
        }

        public ActionResult NewContributor()
        {
            var employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            ViewBag.EmployeeId = new SelectList(employer, "Id", "EmployerName");

            List<MasterContributor> contributor = new List<MasterContributor>();
            ViewBag.ContributorId = new SelectList(contributor, "Id", "FirstName");

            //var pensioner = db.MasterPensioner.Where(x => x.IsActive == true).ToList();
            //ViewBag.PensionerId = new SelectList(pensioner, "Id", "FirstName");

            //var month = db.MasterMonthName.Where(x => x.IsActive == true).ToList();
            //ViewBag.Month = new SelectList(month, "Id", "Name");

            //int year = DateTime.Now.Year - 20;
            //List<YearModel> yearModel = new List<YearModel>();
            //for (int i = year; i <= DateTime.Now.Year; i++)
            //{
            //  YearModel tempYear = new YearModel();
            //  tempYear.Id = i;
            //  tempYear.Name = i.ToString();
            //  yearModel.Add(tempYear);
            //}
            //ViewBag.Year = new SelectList(yearModel, "Id", "Name");
            return View();
        }

        public JsonResult ContributiorSearchAjax(int EId)
        {
            var result = db.MasterContributor.Where(x => x.EmployerID == EId && x.IsActive == true).Select(x => new { Id = x.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName + " ( " + x.PersonID + " )" }).OrderBy(o => o.Name).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ContributiorByMonthAjax(string emplrId, string contributorid, string date1, string date2)
        {
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append("SELECT C.PersonID,C.EmployerID,E.EmployerName,C.SocialSecurityNo,C.FirstName,C.MidName,C.LastName,C.DateOfBirth,C.Gender,C.FirstAppointmentDate ");
            SQL.Append(" FROM MasterContributor C  WITH (NOLOCK) INNER JOIN MasterEmployer E  WITH (NOLOCK) ON C.EmployerID = E.Id");
            SQL.Append(" Where 1=1");
            if (emplrId != null && emplrId != string.Empty)
                SQL.Append(" AND E.Id = " + emplrId.ToString());
            if (contributorid != null && contributorid != string.Empty)
                SQL.Append(" AND C.Id = '" + contributorid.Trim().Replace("'", "''") + "'");
            if (date1 != null && date1 != string.Empty)//YearTo
                SQL.Append(" and C.FirstAppointmentDate >= '" + date1.Trim().Replace("'", "''") + "'");
            if (date2 != null && date2 != string.Empty)//YearTo
                SQL.Append(" and C.FirstAppointmentDate <= '" + date2.Trim().Replace("'", "''") + "'");
            SQL.Append(" Order by C.FirstAppointmentDate ");
            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);

            //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/Reports/DSNewContributor.xsd"));
            //ds.WriteXmlSchema(writer);
            //writer.Close();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.HttpContext.Session["ReportName"] = "rptNewContributor.rpt";
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }


        public JsonResult ContributionByMonthAjax(string emplrId, string contributorid, string date1, string date2)
        {
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append("SELECT DISTINCT  C.PersonID,C.EmployerID,E.EmployerName,C.SocialSecurityNo,isnull(C.FirstName,'')+' '+isnull(C.MidName,'')+' '+isnull(C.LastName,''),C.DateOfBirth,C.Gender,");
            SQL.Append(" C.FirstAppointmentDate , H.Month,H.Year, MONTH(H.Month + ' 1 2015')MonthNo,D.EmployerContribution,D.ContributorContribution");
            SQL.Append(" FROM MasterContributor C  WITH (NOLOCK) ");
            SQL.Append(" INNER JOIN ContributonSheetDetailsFinalise D  WITH (NOLOCK) ON D.ContributorID=C.Id ");
            SQL.Append(" INNER JOIN ContributonSheetHeaderFinalise H  WITH (NOLOCK) ON H.Id=D.ContributonSheetHeaderFinaliseID INNER JOIN MasterEmployer E ON H.EmployerID = E.Id");
            SQL.Append(" Where 1=1 AND H.isactive='true' AND D.isactive='true' ");
            //Commented By Neeraj on dated 25.08.2015
            /*if (emplrId != null && emplrId != string.Empty)
              SQL.Append(" AND E.Id = " + emplrId.ToString());*/
            if (contributorid != null && contributorid != string.Empty)
                SQL.Append(" AND C.Id = '" + contributorid.Trim().Replace("'", "''") + "'");
            if (date1 != null && date1 != string.Empty)//YearTo
                SQL.Append(" and EntryDate >= '" + date1.Trim().Replace("'", "''") + "'");
            if (date2 != null && date2 != string.Empty)//YearTo
                SQL.Append(" and EntryDate <= '" + date2.Trim().Replace("'", "''") + "'");
            //if (date1 != null && date1 != string.Empty)//YearTo
            //  SQL.Append(" and Convert(datetime,('01/'+[month]+'/'+CONVERT(varchar(10), [Year])),6) >= '" + date1.Trim().Replace("'", "''") + "'");
            //if (date2 != null && date2 != string.Empty)//YearTo
            //  SQL.Append(" and Convert(datetime,('20/'+[month]+'/'+CONVERT(varchar(10), [Year])),6) <= '" + date2.Trim().Replace("'", "''") + "'");
            //SQL.Append(" and Convert(datetime,(CONVERT(varchar(2),DAY(EOMONTH(Convert(datetime,('01/'+[month]+'/'+CONVERT(varchar(10), [Year])),6))))+'/'+[month]+'/'+CONVERT(varchar(10), [Year])),6) <= '" + date2.Trim().Replace("'", "''") + "'");
            SQL.Append(" Order by C.EmployerID,MONTH(H.Month + ' 1 2015'),H.Year");
            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);

            //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/Reports/DSContributorContribution.xsd"));
            //ds.WriteXmlSchema(writer);
            //writer.Close();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.HttpContext.Session["ReportName"] = "rptContributorContribution.rpt";
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }
        public ActionResult ContributionsDetails()
        {
            var employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            ViewBag.EmployeeId = new SelectList(employer, "Id", "EmployerName");

            List<MasterContributor> contributor = new List<MasterContributor>();
            ViewBag.ContributorId = new SelectList(contributor, "Id", "FirstName");
            return View();
        }

        public JsonResult ContributionsDetailsAjax(string emplrId, string contributorid, string date1, string date2)
        {
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append("SELECT DISTINCT C.PersonID,C.EmployerID,E.EmployerName,C.SocialSecurityNo,isnull(C.FirstName,'')+' '+isnull(C.MidName,'')+' '+isnull(C.LastName,'') AS ContributorName,C.DateOfBirth,C.Gender,");
            SQL.Append(" count(C.FirstAppointmentDate)FirstAppointmentDate , count(H.Month)Month,count(H.Year)Year,sum(D.EmployerContribution)EmployerContribution,sum(D.ContributorContribution)ContributorContribution");
            SQL.Append(" FROM MasterContributor C WITH (NOLOCK) INNER JOIN MasterEmployer E ON C.EmployerID = E.Id");
            SQL.Append(" INNER JOIN ContributonSheetDetailsFinalise D WITH (NOLOCK) ON D.ContributorID=C.Id");
            SQL.Append(" INNER JOIN ContributonSheetHeaderFinalise H WITH (NOLOCK) ON H.Id=D.ContributonSheetHeaderFinaliseID ");
            SQL.Append(" Where 1=1 AND H.isactive='true'  AND D.isactive='true' ");
            //Commented By Neeraj on dated 25.08.2015
            /*if (emplrId != null && emplrId != string.Empty)
              SQL.Append(" AND E.Id = " + emplrId.ToString());*/
            if (contributorid != null && contributorid != string.Empty)
                SQL.Append(" AND C.Id = '" + contributorid.Trim().Replace("'", "''") + "'");
            if (date1 != null && date1 != string.Empty)//YearTo
                SQL.Append(" and EntryDate >= '" + date1.Trim().Replace("'", "''") + "'");
            if (date2 != null && date2 != string.Empty)//YearTo
                SQL.Append(" and EntryDate <= '" + date2.Trim().Replace("'", "''") + "'");
            //SQL.Append(" and Convert(datetime,(CONVERT(varchar(2),DAY(EOMONTH(Convert(datetime,('01/'+[month]+'/'+CONVERT(varchar(10), [Year])),6))))+'/'+[month]+'/'+CONVERT(varchar(10), [Year])),6) <= '" + date2.Trim().Replace("'", "''") + "'");
            SQL.Append(" Group By C.PersonID,C.EmployerID,E.EmployerName,C.SocialSecurityNo,");
            SQL.Append(" isnull(C.FirstName,'')+' '+isnull(C.MidName,'')+' '+isnull(C.LastName,''), ");
            SQL.Append(" C.DateOfBirth,C.Gender");
            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);

            //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/Reports/DSContributionsDetails.xsd"));
            //ds.WriteXmlSchema(writer);
            //writer.Close();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.HttpContext.Session["ReportName"] = "rptContributionsDetails.rpt";
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListofGrantedRefunds()
        {
            var employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            ViewBag.EmployeeId = new SelectList(employer, "Id", "EmployerName");

            List<MasterContributor> contributor = new List<MasterContributor>();
            ViewBag.ContributorId = new SelectList(contributor, "Id", "FirstName");
            return View();
        }

        public ActionResult RetirementDetails()
        {
            var employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            ViewBag.EmployeeId = new SelectList(employer, "Id", "EmployerName");

            List<MasterContributor> contributor = new List<MasterContributor>();
            ViewBag.ContributorId = new SelectList(contributor, "Id", "FirstName");
            return View();
        }

        public JsonResult RetirementDetailsAjax(string emplrId, string contributorid, string date1, string date2)
        {
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append("SELECT C.PersonID,C.EmployerID,E.EmployerName,C.SocialSecurityNo,C.FirstName,C.MidName,C.LastName,C.DateOfBirth,C.Gender,C.FirstAppointmentDate,C.ExpectedRetirementDate ");
            SQL.Append(" FROM MasterContributor C  WITH (NOLOCK) INNER JOIN MasterEmployer E  WITH (NOLOCK) ON C.EmployerID = E.Id");
            SQL.Append(" Where 1=1");
            if (emplrId != null && emplrId != string.Empty)
                SQL.Append(" AND E.Id = " + emplrId.ToString());
            if (contributorid != null && contributorid != string.Empty)
                SQL.Append(" AND C.Id = '" + contributorid.Trim().Replace("'", "''") + "'");
            if (date1 != null && date1 != string.Empty)//YearTo
                SQL.Append(" and C.FirstAppointmentDate >= '" + date1.Trim().Replace("'", "''") + "'");
            if (date2 != null && date2 != string.Empty)//YearTo
                SQL.Append(" and C.FirstAppointmentDate <= '" + date2.Trim().Replace("'", "''") + "'");
            SQL.Append(" Order by C.FirstAppointmentDate ");
            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);

            //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/Reports/DSRetirementDetails.xsd"));
            //ds.WriteXmlSchema(writer);
            //writer.Close();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.HttpContext.Session["ReportName"] = "rptRetirementDetails.rpt";
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GrantedRefundsAjax(string emplrId, string contributorid, string date1, string date2)
        {
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append("SELECT Distinct ME.EmployerName, RA.PersonID, isnull(MC.FirstName,'')+' '+isnull(MC.MidName,'')+' '+isnull(MC.LastName,''), MC.Gender,");
            SQL.Append(" MC.DateOfBirth, RA.RetirementOrResignationDate, ");
            SQL.Append(" RA.LengthOfServiceInMonths, RA.TotalEmployeeContribution, RA.AnnualInterest, ");
            SQL.Append(" RA.TotalEmployeeContributionWithInterest,  ");
            SQL.Append(" AAS.ApprovalProcessId, AAS.ApprovalStatus,Notes,ApprovedDate  ");
            //SQL.Append(" (select CASE WHEN MAX(ApprovalProcessId) = 4 THEN 'IInd Level Approval (Verified by) is still pending' ");
            //SQL.Append(" WHEN MAX(ApprovalProcessId) = 5 THEN 'IIIrd Level Approval (Certified For Payment by) is still pending' ");
            //SQL.Append(" WHEN MAX(ApprovalProcessId) >= 6 THEN '' ELSE 'Approval is Pending for All Process' END ");
            //SQL.Append(" from ApplicationApprovalStatus where ApplicationId=AAS.ApplicationId AND ApprovalStatus='A') ApprovalProcessId ");
            //SQL.Append(" ,APAU.ApprovalProcessId  ");
            SQL.Append(" FROM  RefundApplications RA INNER JOIN");
            SQL.Append(" MasterEmployer ME  WITH (NOLOCK) ON RA.EmployerID = ME.Id INNER JOIN");
            SQL.Append(" ApprovalProcessLevel APL  WITH (NOLOCK) INNER JOIN");
            SQL.Append(" ApprovalProcessAssignedUser APAU  WITH (NOLOCK) ON APL.ApprovalProcessLevelId = APAU.ApprovalProcessLevelId INNER JOIN");
            SQL.Append(" ApplicationApprovalStatus AAS  WITH (NOLOCK) ON APAU.ApprovalProcessId = AAS.ApprovalProcessId ON ");
            SQL.Append(" RA.Id = AAS.ApplicationId INNER JOIN");
            SQL.Append(" MasterContributor MC  WITH (NOLOCK) ON ME.Id = MC.EmployerID AND RA.PersonID = MC.Id");
            SQL.Append(" Where APL.ModuleId=10  ");//AAS.ApprovalStatus='A' AND 
                                                   //SQL.Append(" AND APAU.ApprovalProcessId in");
                                                   //SQL.Append(" (SELECT ApprovalProcessId FROM ApprovalProcessLevel APL  WITH (NOLOCK) INNER JOIN");
                                                   //SQL.Append(" ApprovalProcessAssignedUser APAU  WITH (NOLOCK) ON APL.ApprovalProcessLevelId = APAU.ApprovalProcessLevelId");
                                                   //SQL.Append(" WHERE ApprovalLevel=(SELECT MAX(ApprovalLevel) FROM ApprovalProcessLevel  WITH (NOLOCK) GROUP BY ModuleId having ModuleId=10) AND ModuleId=10 ) ");

            if (emplrId != null && emplrId != string.Empty)
                SQL.Append(" AND ME.Id = " + emplrId.ToString());
            if (contributorid != null && contributorid != string.Empty)
                SQL.Append(" AND RA.PersonID = '" + contributorid.Trim().Replace("'", "''") + "'");
            if (date1 != null && date1 != string.Empty)//YearTo
                SQL.Append(" and ApprovedDate >= '" + date1.Trim().Replace("'", "''") + "'");
            if (date2 != null && date2 != string.Empty)//YearTo
                SQL.Append(" and ApprovedDate <= '" + date2.Trim().Replace("'", "''") + "'");

            SQL.Append(" Order By isnull(MC.FirstName,'')+' '+isnull(MC.MidName,'')+' '+isnull(MC.LastName,''),ApprovedDate,AAS.ApprovalProcessId ");

            //SQL.Append(" GROUP BY ME.EmployerName, RA.PersonID, isnull(MC.FirstName,'')+' '+isnull(MC.MidName,'')+' '+isnull(MC.LastName,''), MC.Gender, MC.DateOfBirth,  ");
            //SQL.Append(" RA.RetirementOrResignationDate,  RA.LengthOfServiceInMonths, RA.TotalEmployeeContribution, RA.AnnualInterest,  RA.TotalEmployeeContributionWithInterest, ");
            //SQL.Append(" convert(datetime, CONVERT(VARCHAR(10), ApprovedDate, 101) ),(TotalEmployeeContributionWithInterest) ");

            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);

            //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/Reports/DSGrantedRefunds.xsd"));
            //ds.WriteXmlSchema(writer);
            //writer.Close();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.HttpContext.Session["ReportName"] = "rptGrantedRefunds.rpt";
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RetirementsApproved()
        {
            var employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            ViewBag.EmployeeId = new SelectList(employer, "Id", "EmployerName");

            List<MasterContributor> contributor = new List<MasterContributor>();
            ViewBag.ContributorId = new SelectList(contributor, "Id", "FirstName");
            return View();
        }

        //public JsonResult RetirementsApprovedAjax(string emplrId, string contributorid, string date1, string date2)
        //{
        //    //DataSet ds = new DataSet();
        //    //StringBuilder SQL = new StringBuilder();
        //    //SQL.Append("SELECT ME.EmployerName, PA.PersonID, isnull(MC.FirstName,'')+' '+isnull(MC.MidName,'')+' '+isnull(MC.LastName,''), MC.Gender,");
        //    //SQL.Append(" MC.DateOfBirth, PA.PensionableStatus, PA.BenefitTypes, PA.RetirementOrResignationDate, ");
        //    //SQL.Append(" PA.LengthOfQualifyingServiceInMonthsTo31Dec2003, PA.LengthOfQualifyingServiceInMonthsFrom1Jan2014, PA.DateofAppointment1st, ");
        //    //SQL.Append(" PA.DateofAppointmentLast, PA.QualifyingServiceRate1, PA.QualifyingServiceRate2, PA.RetirementAnnualSalary1,  ");
        //    //SQL.Append(" PA.ApplicationSubmittedBy, PA.RetirementAnnualSalary2, PA.FullpensionAmount1, PA.FullpensionAmount2, ");
        //    //SQL.Append(" PA.TotalFullpension, PA.MaxPension, PA.GratuityReducedPension, PA.PensionPerAnnum,ApprovedDate ");
        //    //SQL.Append(" FROM  PensionApplications PA WITH (NOLOCK) INNER JOIN");
        //    //SQL.Append(" MasterEmployer ME WITH (NOLOCK) ON PA.EmployerID = ME.Id INNER JOIN");
        //    //SQL.Append(" ApprovalProcessLevel APL WITH (NOLOCK) INNER JOIN");
        //    //SQL.Append(" ApprovalProcessAssignedUser APAU WITH (NOLOCK) ON APL.ApprovalProcessLevelId = APAU.ApprovalProcessLevelId INNER JOIN");
        //    //SQL.Append(" ApplicationApprovalStatus AAS WITH (NOLOCK) ON APAU.ApprovalProcessId = AAS.ApprovalProcessId ON ");
        //    //SQL.Append(" PA.Id = AAS.ApplicationId INNER JOIN");
        //    //SQL.Append(" MasterContributor MC  WITH (NOLOCK) ON ME.Id = MC.EmployerID AND PA.PersonID = MC.Id");
        //    //SQL.Append(" Where AAS.ApprovalStatus='A' AND APL.ModuleId=16 AND APAU.ApprovalProcessId in");
        //    //SQL.Append(" (SELECT ApprovalProcessId FROM ApprovalProcessLevel APL  WITH (NOLOCK) INNER JOIN");
        //    //SQL.Append(" ApprovalProcessAssignedUser APAU WITH (NOLOCK) ON APL.ApprovalProcessLevelId = APAU.ApprovalProcessLevelId");
        //    ////SQL.Append(" WHERE ApprovalLevel=(SELECT MAX(ApprovalLevel) FROM ApprovalProcessLevel WITH (NOLOCK) GROUP BY ModuleId having ModuleId=16) AND ModuleId=16 ) ");
        //    //SQL.Append(" WHERE ApprovalLevel IN (SELECT TOP 1 ApprovalLevel FROM ApprovalProcessLevel WITH (NOLOCK) WHERE ModuleId=16) AND ModuleId=16 ) ");

        //    //if (emplrId != null && emplrId != string.Empty)
        //    //    SQL.Append(" AND ME.Id = " + emplrId.ToString());
        //    //if (contributorid != null && contributorid != string.Empty)
        //    //    SQL.Append(" AND PA.PersonID = '" + contributorid.Trim().Replace("'", "''") + "'");
        //    //if (date1 != null && date1 != string.Empty)//YearTo
        //    //    SQL.Append(" and ApprovedDate >= '" + date1.Trim().Replace("'", "''") + "'");
        //    //if (date2 != null && date2 != string.Empty)//YearTo
        //    //    SQL.Append(" and ApprovedDate <= '" + date2.Trim().Replace("'", "''") + "'");


        //    var result = (from users in db.ApprovalProcessAssignedUser join level in db.ApprovalProcessLevel on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId where level.ModuleId == 16 select new { users.ApprovalProcessId, users.ApprovalUser }).ToList();
        //    //int visiblePrint = result.Skip(1).FirstOrDefault().ApprovalProcessId;
        //    int minId = result.FirstOrDefault().ApprovalProcessId;
        //    int maxId = result.LastOrDefault().ApprovalProcessId;
        //    //int user = AppUserManager.GetUserId();
        //    //List<App.Data.ViewModels.RejectedViewModel> List = new List<App.Data.ViewModels.RejectedViewModel>();

        //    //if (result.Where(x => x.ApprovalUser.ToString() == user.ToString()).Any())
        //    //{
        //    DateTime? startDate = null;
        //    DateTime? endDate = null;
        //    if (date1 != null && date1 != string.Empty)//YearTo
        //    {
        //        startDate = Convert.ToDateTime(date1);
        //    }
        //    if (date2 != null && date2 != string.Empty)//YearTo
        //    {
        //        endDate = Convert.ToDateTime(date2);
        //    }

        //    var List = (from users in db.ApprovalProcessAssignedUser
        //                join level in db.ApprovalProcessLevel on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId
        //                join status in db.ApplicationApprovalStatus on users.ApprovalProcessId equals status == null ? 0 : status.ApprovalProcessId
        //                join application in db.PensionApplications on status == null ? 0 : status.ApplicationId equals application.Id
        //                join contributor in db.MasterContributor on application.PersonID equals contributor.Id.ToString()
        //                join employer in db.MasterEmployer on contributor.EmployerID equals employer.Id
        //                join usermaster in db.Users on users.ApprovalUser equals usermaster.Id
        //                where level.ModuleId == 16 && status.ApprovalProcessId >= minId && status.ApprovalProcessId <= maxId
        //                && (emplrId + "" == "" ? employer.Id == employer.Id : employer.Id.ToString() == emplrId)
        //                && (contributorid + "" == "" ? contributor.Id == contributor.Id : contributor.Id.ToString() == contributorid)
        //                && (startDate == null ? status.ApprovedDate == status.ApprovedDate : status.ApprovedDate >= startDate)
        //                && (endDate == null ? status.ApprovedDate == status.ApprovedDate : status.ApprovedDate <= endDate)
        //                orderby application.Id descending
        //                select new
        //                {
        //                    employer.EmployerName,
        //                    EmployerId = employer.Id,
        //                    contributor.PersonID,
        //                    Contributor = contributor.FirstName + "" + " " + contributor.MidName + "" + " " + contributor.LastName + "",
        //                    contributor.Gender,
        //                    contributor.DateOfBirth,
        //                    application.PensionableStatus,
        //                    application.BenefitTypes,
        //                    application.RetirementOrResignationDate,
        //                    application.LengthOfQualifyingServiceInMonthsTo31Dec2003,
        //                    application.LengthOfQualifyingServiceInMonthsFrom1Jan2014,
        //                    application.DateofAppointment1st,
        //                    application.DateofAppointmentLast,
        //                    application.QualifyingServiceRate1,
        //                    application.QualifyingServiceRate2,
        //                    application.ApplicationSubmittedBy,
        //                    application.RetirementAnnualSalary1,
        //                    application.RetirementAnnualSalary2,
        //                    application.FullpensionAmount1,
        //                    application.FullpensionAmount2,
        //                    application.TotalFullpension,
        //                    application.MaxPension,
        //                    application.pensionPerAnnum,
        //                    application.Gratuity,
        //                    ApplicationId = status.ApplicationId,
        //                    AppliedDate = application.CreatedOn,
        //                    ApprovalStatus = status.ApprovalStatus,
        //                    ApprovalUser = users.ApprovalUser.ToString(),
        //                    status.ApprovedDate,
        //                    Notes = status.Notes,
        //                    approvalProcessId = status.ApprovalProcessId,
        //                    ApprovalLevel = status.ApprovalLevel,
        //                    UserName = usermaster.UserName,
        //                }).ToList();
        //    //Below code is commented by neeraj on dated 30Apr2016
        //    /*var rejectedappid = List.Where(x => x.ApprovalStatus == "R").Select(x => x.ApplicationId).ToList();
        //    if (rejectedappid.Count > 0)
        //    {
        //        List = List.Where(x => !rejectedappid.Contains(x.ApplicationId)).GroupBy(x => x.ApplicationId).Select(g => g.Last()).ToList();
        //        //foreach (var item in rejectedappid)
        //        //{

        //        //}
        //    }
        //    else
        //    {
        //        List = List.GroupBy(x => x.ApplicationId).Select(g => g.Last()).ToList();
        //    }*/

        //    /*if (emplrId != null && emplrId != string.Empty)
        //      List.Where(x => x.EmployerId.ToString() == emplrId);
        //    if (contributorid != null && contributorid != string.Empty)
        //      List.Where(x => x.PersonID.ToString() == contributorid);
        //    if (date1 != null && date1 != string.Empty)//YearTo
        //    {
        //      DateTime startDate = Convert.ToDateTime(date1);
        //      List.Where(x => x.ApprovedDate >= startDate);
        //    } if (date2 != null && date2 != string.Empty)//YearTo
        //    {
        //      DateTime endDate = Convert.ToDateTime(date2);
        //      List.Where(x => x.ApprovedDate <= endDate);
        //    }*/

        //    //}
        //    //string con = WebConfigurationManager.AppSettings["SQLConn"];
        //    //SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
        //    //da.Fill(ds);

        //    //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/Reports/DSRetirementsApproved.xsd"));
        //    //ds.WriteXmlSchema(writer);
        //    //writer.Close();
        //    DataTable ds = App.Web.Repository.ListToDataset.ToDataTable(List);
        //    if (ds.Rows.Count > 0)
        //    {
        //        this.HttpContext.Session["ReportName"] = "rptRetirementsApproved.rpt";
        //        this.HttpContext.Session["rptSource"] = ds;
        //        return Json("1", JsonRequestBehavior.AllowGet);
        //    }

        //    return Json("0", JsonRequestBehavior.AllowGet);
        //}

        public JsonResult RetirementsApprovedAjax(string emplrId, string contributorid, string date1, string date2)
        {
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append("SELECT Distinct ME.EmployerName, RA.PersonID, isnull(MC.FirstName,'')+' '+isnull(MC.MidName,'')+' '+isnull(MC.LastName,''), MC.Gender,");
            SQL.Append(" MC.DateOfBirth, RA.RetirementOrResignationDate, ");
            SQL.Append(" RA.LengthOfServiceInMonths, RA.TotalEmployeeContribution, RA.AnnualInterest, ");
            SQL.Append(" RA.TotalEmployeeContributionWithInterest,  ");
            SQL.Append(" AAS.ApprovalProcessId, AAS.ApprovalStatus,Notes,ApprovedDate  ");
            //SQL.Append(" (select CASE WHEN MAX(ApprovalProcessId) = 4 THEN 'IInd Level Approval (Verified by) is still pending' ");
            //SQL.Append(" WHEN MAX(ApprovalProcessId) = 5 THEN 'IIIrd Level Approval (Certified For Payment by) is still pending' ");
            //SQL.Append(" WHEN MAX(ApprovalProcessId) >= 6 THEN '' ELSE 'Approval is Pending for All Process' END ");
            //SQL.Append(" from ApplicationApprovalStatus where ApplicationId=AAS.ApplicationId AND ApprovalStatus='A') ApprovalProcessId ");
            //SQL.Append(" ,APAU.ApprovalProcessId  ");
            SQL.Append(" FROM  RefundApplications RA INNER JOIN");
            SQL.Append(" MasterEmployer ME  WITH (NOLOCK) ON RA.EmployerID = ME.Id INNER JOIN");
            SQL.Append(" ApprovalProcessLevel APL  WITH (NOLOCK) INNER JOIN");
            SQL.Append(" ApprovalProcessAssignedUser APAU  WITH (NOLOCK) ON APL.ApprovalProcessLevelId = APAU.ApprovalProcessLevelId INNER JOIN");
            SQL.Append(" ApplicationApprovalStatus AAS  WITH (NOLOCK) ON APAU.ApprovalProcessId = AAS.ApprovalProcessId ON ");
            SQL.Append(" RA.Id = AAS.ApplicationId INNER JOIN");
            SQL.Append(" MasterContributor MC  WITH (NOLOCK) ON ME.Id = MC.EmployerID AND RA.PersonID = MC.Id");
            SQL.Append(" Where APL.ModuleId=16  ");//AAS.ApprovalStatus='A' AND 
                                                   //SQL.Append(" AND APAU.ApprovalProcessId in");
                                                   //SQL.Append(" (SELECT ApprovalProcessId FROM ApprovalProcessLevel APL  WITH (NOLOCK) INNER JOIN");
                                                   //SQL.Append(" ApprovalProcessAssignedUser APAU  WITH (NOLOCK) ON APL.ApprovalProcessLevelId = APAU.ApprovalProcessLevelId");
                                                   //SQL.Append(" WHERE ApprovalLevel=(SELECT MAX(ApprovalLevel) FROM ApprovalProcessLevel  WITH (NOLOCK) GROUP BY ModuleId having ModuleId=10) AND ModuleId=10 ) ");

            if (emplrId != null && emplrId != string.Empty)
                SQL.Append(" AND ME.Id = " + emplrId.ToString());
            if (contributorid != null && contributorid != string.Empty)
                SQL.Append(" AND RA.PersonID = '" + contributorid.Trim().Replace("'", "''") + "'");
            if (date1 != null && date1 != string.Empty)//YearTo
                SQL.Append(" and ApprovedDate >= '" + date1.Trim().Replace("'", "''") + "'");
            if (date2 != null && date2 != string.Empty)//YearTo
                SQL.Append(" and ApprovedDate <= '" + date2.Trim().Replace("'", "''") + "'");

            SQL.Append(" Order By isnull(MC.FirstName,'')+' '+isnull(MC.MidName,'')+' '+isnull(MC.LastName,''),ApprovedDate,AAS.ApprovalProcessId ");

            //SQL.Append(" GROUP BY ME.EmployerName, RA.PersonID, isnull(MC.FirstName,'')+' '+isnull(MC.MidName,'')+' '+isnull(MC.LastName,''), MC.Gender, MC.DateOfBirth,  ");
            //SQL.Append(" RA.RetirementOrResignationDate,  RA.LengthOfServiceInMonths, RA.TotalEmployeeContribution, RA.AnnualInterest,  RA.TotalEmployeeContributionWithInterest, ");
            //SQL.Append(" convert(datetime, CONVERT(VARCHAR(10), ApprovedDate, 101) ),(TotalEmployeeContributionWithInterest) ");

            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);

            System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/Reports/DSRetirementsApproved.xsd"));
            ds.WriteXmlSchema(writer);
            writer.Close();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.HttpContext.Session["ReportName"] = "rptRetirementsApproved.rpt";
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GratuityPayments()
        {
            var employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            ViewBag.EmployeeId = new SelectList(employer, "Id", "EmployerName");

            List<MasterContributor> contributor = new List<MasterContributor>();
            ViewBag.ContributorId = new SelectList(contributor, "Id", "FirstName");
            return View();
        }

        public JsonResult GratuityPaymentsAjax(string emplrId, string contributorid, string date1, string date2)
        {
            /*DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append("SELECT ME.EmployerName, PA.PersonID, isnull(G.FirstName,'')+' '+isnull(G.MidName,'')+' '+isnull(G.LastName,'') AS Beneficiar, MC.Gender,");
            SQL.Append(" MC.DateOfBirth, PA.PensionableStatus, PA.BenefitTypes, PA.RetirementOrResignationDate, ");
            SQL.Append(" PA.LengthOfQualifyingServiceInMonthsTo31Dec2003, PA.LengthOfQualifyingServiceInMonthsFrom1Jan2014, PA.DateofAppointment1st, ");
            SQL.Append(" PA.DateofAppointmentLast, PA.QualifyingServiceRate1, PA.QualifyingServiceRate2, PA.RetirementAnnualSalary1,  ");
            SQL.Append(" PA.ApplicationSubmittedBy, PA.RetirementAnnualSalary2, PA.FullpensionAmount1, PA.FullpensionAmount2, ");
            SQL.Append(" PA.TotalFullpension, PA.MaxPension, PA.GratuityReducedPension, PA.PensionPerAnnum,ApprovedDate, ");
            SQL.Append(" G.GratuityAmt,G.DiscountedGratuity,G.PaidStatus,G.Paymentmethod,G.Check_no ");
            SQL.Append(" FROM  PensionApplications PA WITH (NOLOCK) INNER JOIN");
            SQL.Append(" GratuityDetails G WITH (NOLOCK) ON PA.Id = G.PensionApplicationId INNER JOIN");
            SQL.Append(" MasterEmployer ME WITH (NOLOCK) ON PA.EmployerID = ME.Id INNER JOIN");
            SQL.Append(" ApprovalProcessLevel APL WITH (NOLOCK) INNER JOIN");
            SQL.Append(" ApprovalProcessAssignedUser APAU WITH (NOLOCK) ON APL.ApprovalProcessLevelId = APAU.ApprovalProcessLevelId INNER JOIN");
            SQL.Append(" ApplicationApprovalStatus AAS WITH (NOLOCK) ON APAU.ApprovalProcessId = AAS.ApprovalProcessId ON ");
            SQL.Append(" PA.Id = AAS.ApplicationId INNER JOIN");
            SQL.Append(" MasterContributor MC WITH (NOLOCK) ON ME.Id = MC.EmployerID AND PA.PersonID = MC.Id");
            SQL.Append(" Where AAS.ApprovalStatus='A' AND G.Isactive=1 AND APL.ModuleId=16 AND APAU.ApprovalProcessId in");
            SQL.Append(" (SELECT ApprovalProcessId FROM ApprovalProcessLevel APL WITH (NOLOCK) INNER JOIN");
            SQL.Append(" ApprovalProcessAssignedUser APAU WITH (NOLOCK) ON APL.ApprovalProcessLevelId = APAU.ApprovalProcessLevelId");
            SQL.Append(" WHERE ApprovalLevel=(SELECT MAX(ApprovalLevel) FROM ApprovalProcessLevel WITH (NOLOCK) GROUP BY ModuleId having ModuleId=16) AND ModuleId=16 ) ");

            if (emplrId != null && emplrId != string.Empty)
              SQL.Append(" AND ME.Id = " + emplrId.ToString());
            if (contributorid != null && contributorid != string.Empty)
              SQL.Append(" AND PA.PersonID = '" + contributorid.Trim().Replace("'", "''") + "'");
            if (date1 != null && date1 != string.Empty)//YearTo
              SQL.Append(" and ApprovedDate >= '" + date1.Trim().Replace("'", "''") + "'");
            if (date2 != null && date2 != string.Empty)//YearTo
              SQL.Append(" and ApprovedDate <= '" + date2.Trim().Replace("'", "''") + "'");

            string con = WebConfigurationManager.AppSettings["SQLConn"];
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);*/

            var result = (from users in db.ApprovalProcessAssignedUser join level in db.ApprovalProcessLevel on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId where level.ModuleId == 16 select new { users.ApprovalProcessId, users.ApprovalUser }).ToList();
            int minId = result.FirstOrDefault().ApprovalProcessId;
            int maxId = result.LastOrDefault().ApprovalProcessId;

            DateTime? startDate = null;
            DateTime? endDate = null;
            if (date1 != null && date1 != string.Empty)//YearTo
            {
                startDate = Convert.ToDateTime(date1);
            }
            if (date2 != null && date2 != string.Empty)//YearTo
            {
                endDate = Convert.ToDateTime(date2);
            }

            var List = (from users in db.ApprovalProcessAssignedUser
                        join level in db.ApprovalProcessLevel on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId
                        join status in db.ApplicationApprovalStatus on users.ApprovalProcessId equals status == null ? 0 : status.ApprovalProcessId
                        join application in db.PensionApplications on status == null ? 0 : status.ApplicationId equals application.Id
                        join contributor in db.MasterContributor on application.PersonID equals contributor.Id.ToString()
                        join employer in db.MasterEmployer on contributor.EmployerID equals employer.Id
                        join g in db.GratuityDetails on application.Id equals g.PensionApplicationId into temp
                        from gratuity in temp.DefaultIfEmpty()
                        where level.ModuleId == 16 && status.ApprovalProcessId >= minId && status.ApprovalProcessId <= maxId
                        && (emplrId + "" == "" ? employer.Id == employer.Id : employer.Id.ToString() == emplrId)
                        && (contributorid + "" == "" ? contributor.Id == contributor.Id : contributor.Id.ToString() == contributorid)
                        && (startDate == null ? status.ApprovedDate == status.ApprovedDate : status.ApprovedDate >= startDate)
                        && (endDate == null ? status.ApprovedDate == status.ApprovedDate : status.ApprovedDate <= endDate)
                        orderby application.Id descending
                        select new
                        {
                            employer.EmployerName,
                            EmployerId = employer.Id,
                            application.PersonID,
                            Beneficiar = contributor.FirstName + "" + " " + contributor.MidName + "" + " " + contributor.LastName + "",
                            contributor.Gender,
                            contributor.DateOfBirth,
                            application.PensionableStatus,
                            application.BenefitTypes,
                            application.RetirementOrResignationDate,
                            application.LengthOfQualifyingServiceInMonthsTo31Dec2003,
                            application.LengthOfQualifyingServiceInMonthsFrom1Jan2014,
                            application.DateofAppointment1st,
                            application.DateofAppointmentLast,
                            application.QualifyingServiceRate1,
                            application.QualifyingServiceRate2,
                            application.ApplicationSubmittedBy,
                            application.RetirementAnnualSalary1,
                            application.RetirementAnnualSalary2,
                            application.FullpensionAmount1,
                            application.FullpensionAmount2,
                            application.TotalFullpension,
                            application.MaxPension,
                            application.pensionPerAnnum,
                            GratuityAmt = application.Gratuity,
                            ApplicationId = status.ApplicationId,
                            AppliedDate = application.CreatedOn,
                            ApprovalStatus = status.ApprovalStatus,
                            ApprovalUser = users.ApprovalUser.ToString(),
                            status.ApprovedDate,
                            Notes = status.Notes,
                            approvalProcessId = status.ApprovalProcessId,
                            GratuityAmtPaid = gratuity.GratuityAmt,
                            gratuity.DiscountedGratuity,
                            gratuity.PaidStatus,
                            gratuity.Paymentmethod,
                            gratuity.Check_no,
                        }).ToList();
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

            DataTable ds = App.Web.Repository.ListToDataset.ToDataTable(List);

            //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/Reports/DSGratuityPayments.xsd"));
            //ds.WriteXmlSchema(writer);
            //writer.Close();

            if (ds.Rows.Count > 0)
            {
                this.HttpContext.Session["ReportName"] = "rptGratuityPayments.rpt";
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }
        public ActionResult RefundDetails()
        {
            var employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            ViewBag.EmployeeId = new SelectList(employer, "Id", "EmployerName");

            List<MasterContributor> contributor = new List<MasterContributor>();
            ViewBag.ContributorId = new SelectList(contributor, "Id", "FirstName");
            return View();
        }

        public JsonResult RefundDetailsAjax(string emplrId, string contributorid, string date1, string date2)
        {
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append("SELECT distinct ME.EmployerName, RA.PersonID, isnull(MC.FirstName,'')+' '+isnull(MC.MidName,'')+' '+isnull(MC.LastName,'') AS Beneficiar, MC.Gender,");
            SQL.Append(" MC.DateOfBirth, RA.RetirementOrResignationDate, ");
            SQL.Append(" RA.LengthOfServiceInMonths, RA.TotalEmployeeContribution, RA.AnnualInterest, ");
            SQL.Append(" RA.TotalEmployeeContributionWithInterest,(SELECT MAX(convert(datetime, CONVERT(VARCHAR(10), ApprovedDate, 101) )) from ApplicationApprovalStatus where ApplicationId=AAS.ApplicationId AND ApprovalStatus='A') ApprovedDate,(TotalEmployeeContributionWithInterest) RefundAmt  ");//
            SQL.Append(" ,(select CASE WHEN MAX(ApprovalProcessId) = 4 THEN 'IInd Level Approval (Verified by) is still pending'  ");
            SQL.Append(" WHEN MAX(ApprovalProcessId) = 5 THEN 'IIIrd Level Approval (Certified For Payment by) is still pending'  ");
            SQL.Append(" WHEN MAX(ApprovalProcessId) >= 6 THEN '' ELSE 'Approval is Pending for All Process' END ");
            SQL.Append(" from ApplicationApprovalStatus where ApplicationId=AAS.ApplicationId AND ApprovalStatus='A') ApprovalProcessId ");
            SQL.Append(" FROM  RefundApplications RA WITH (NOLOCK) LEFT JOIN");
            SQL.Append(" RefundPaidDetails RP WITH (NOLOCK) ON RA.Id = RP.RefundApplicationId INNER JOIN");
            SQL.Append(" MasterEmployer ME WITH (NOLOCK) ON RA.EmployerID = ME.Id INNER JOIN");
            SQL.Append(" ApprovalProcessLevel APL WITH (NOLOCK) INNER JOIN");
            SQL.Append(" ApprovalProcessAssignedUser APAU WITH (NOLOCK) ON APL.ApprovalProcessLevelId = APAU.ApprovalProcessLevelId INNER JOIN");
            SQL.Append(" ApplicationApprovalStatus AAS WITH (NOLOCK) ON APAU.ApprovalProcessId = AAS.ApprovalProcessId ON ");
            SQL.Append(" RA.Id = AAS.ApplicationId INNER JOIN");
            SQL.Append(" MasterContributor MC WITH (NOLOCK) ON ME.Id = MC.EmployerID AND RA.PersonID = MC.Id");
            SQL.Append(" Where AAS.ApprovalStatus='A' AND APL.ModuleId=10 ");

            //SQL.Append("SELECT distinct ME.EmployerName, RA.PersonID, isnull(RP.FirstName,'')+' '+isnull(RP.MidName,'')+' '+isnull(RP.LastName,'') AS Beneficiar, MC.Gender,");
            //SQL.Append(" MC.DateOfBirth, RA.RetirementOrResignationDate, ");
            //SQL.Append(" RA.LengthOfServiceInMonths, RA.TotalEmployeeContribution, RA.AnnualInterest, ");
            //SQL.Append(" RA.TotalEmployeeContributionWithInterest,ApprovedDate,RP.RefundAmt  ");//
            //SQL.Append(" ,(CASE WHEN APAU.ApprovalProcessId >= 6 THEN '' ELSE 'Approval Pending for Certified For Payment by' END) ApprovalProcessId ");
            //SQL.Append(" FROM  RefundApplications RA WITH (NOLOCK) LEFT JOIN");
            //SQL.Append(" RefundPaidDetails RP WITH (NOLOCK) ON RA.Id = RP.RefundApplicationId INNER JOIN");
            //SQL.Append(" MasterEmployer ME WITH (NOLOCK) ON RA.EmployerID = ME.Id INNER JOIN");
            //SQL.Append(" ApprovalProcessLevel APL WITH (NOLOCK) INNER JOIN");
            //SQL.Append(" ApprovalProcessAssignedUser APAU WITH (NOLOCK) ON APL.ApprovalProcessLevelId = APAU.ApprovalProcessLevelId INNER JOIN");
            //SQL.Append(" ApplicationApprovalStatus AAS WITH (NOLOCK) ON APAU.ApprovalProcessId = AAS.ApprovalProcessId ON ");
            //SQL.Append(" RA.Id = AAS.ApplicationId INNER JOIN");
            //SQL.Append(" MasterContributor MC WITH (NOLOCK) ON ME.Id = MC.EmployerID AND RA.PersonID = MC.Id");
            //SQL.Append(" Where AAS.ApprovalStatus='A' AND APL.ModuleId=10 ");
            //SQL.Append(" AND APAU.ApprovalProcessId in (SELECT ApprovalProcessId FROM ApprovalProcessLevel APL WITH (NOLOCK) INNER JOIN");
            //SQL.Append(" ApprovalProcessAssignedUser APAU WITH (NOLOCK) ON APL.ApprovalProcessLevelId = APAU.ApprovalProcessLevelId");
            //SQL.Append(" WHERE ApprovalLevel=(SELECT MAX(ApprovalLevel) FROM ApprovalProcessLevel WITH (NOLOCK) GROUP BY ModuleId having ModuleId=10) AND ModuleId=10 ) ");

            if (emplrId != null && emplrId != string.Empty)
                SQL.Append(" AND ME.Id = " + emplrId.ToString());
            if (contributorid != null && contributorid != string.Empty)
                SQL.Append(" AND RA.PersonID = '" + contributorid.Trim().Replace("'", "''") + "'");
            if (date1 != null && date1 != string.Empty)//YearTo
                SQL.Append(" and ApprovedDate >= '" + date1.Trim().Replace("'", "''") + "'");
            if (date2 != null && date2 != string.Empty)//YearTo
                SQL.Append(" and ApprovedDate <= '" + date2.Trim().Replace("'", "''") + "'");


            //SQL.Append(" GROUP BY ME.EmployerName, RA.PersonID, isnull(MC.FirstName,'')+' '+isnull(MC.MidName,'')+' '+isnull(MC.LastName,''), MC.Gender, MC.DateOfBirth,  ");
            //SQL.Append(" RA.RetirementOrResignationDate,  RA.LengthOfServiceInMonths, RA.TotalEmployeeContribution, RA.AnnualInterest,  RA.TotalEmployeeContributionWithInterest, ");
            //SQL.Append(" convert(datetime, CONVERT(VARCHAR(10), ApprovedDate, 101) ),(TotalEmployeeContributionWithInterest) ");

            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);

            //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/Reports/DSRefundDetails.xsd"));
            //ds.WriteXmlSchema(writer);
            //writer.Close();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.HttpContext.Session["ReportName"] = "rptRefundDetails.rpt";
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployerContribution()
        {
            var employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            ViewBag.EmployeeId = new SelectList(employer, "Id", "EmployerName");

            List<MasterContributor> contributor = new List<MasterContributor>();
            ViewBag.ContributorId = new SelectList(contributor, "Id", "FirstName");
            return View();
        }
        public JsonResult EmployerContributionAjax(string emplrId, string contributorid, string date1, string date2, string rpt)
        {

            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            if (rpt == "Detailed")
            {
                SQL.Append("SELECT DISTINCT C.PersonID,C.EmployerID,E.EmployerName,C.SocialSecurityNo,isnull(C.FirstName,'')+' '+isnull(C.MidName,'')+' '+isnull(C.LastName,'') AS ContributorName,C.DateOfBirth,C.Gender,");
                SQL.Append(" C.FirstAppointmentDate FirstAppointmentDate , H.Month, H.Year Year, D.EmployerContribution EmployerContribution, D.ContributorContribution ContributorContribution,MONTH(H.Month + ' 1 2014') MonthNo,S.Name SourceName");
                SQL.Append(" FROM MasterContributor C WITH (NOLOCK) INNER JOIN MasterEmployer E ON C.EmployerID = E.Id");
                SQL.Append(" INNER JOIN ContributonSheetDetailsFinalise D WITH (NOLOCK) ON D.ContributorID=C.Id");
                SQL.Append(" INNER JOIN ContributonSheetHeaderFinalise H WITH (NOLOCK) ON H.Id=D.ContributonSheetHeaderFinaliseID  ");
                SQL.Append(" INNER JOIN MasterSource S WITH (NOLOCK) ON S.Id=D.SourceID  ");
                SQL.Append(" Where 1=1 AND H.isactive='true' AND D.isactive='true' AND S.isactive='true'");
                //Commented By Neeraj on dated 25.08.2015
                if (emplrId != null && emplrId != string.Empty)
                    SQL.Append(" AND E.Id = " + emplrId.ToString());
                if (contributorid != null && contributorid != string.Empty)
                    SQL.Append(" AND C.Id = '" + contributorid.Trim().Replace("'", "''") + "'");
                if (date1 != null && date1 != string.Empty)//YearTo
                    SQL.Append(" and EntryDate >= '" + date1.Trim().Replace("'", "''") + "'");
                if (date2 != null && date2 != string.Empty)//YearTo
                    SQL.Append(" and EntryDate <= '" + date2.Trim().Replace("'", "''") + "'");
                //SQL.Append(" and Convert(datetime,(CONVERT(varchar(2),DAY(EOMONTH(Convert(datetime,('01/'+[month]+'/'+CONVERT(varchar(10), [Year])),6))))+'/'+[month]+'/'+CONVERT(varchar(10), [Year])),6) <= '" + date2.Trim().Replace("'", "''") + "'");
                SQL.Append(" Order By E.EmployerName,isnull(C.FirstName,'')+' '+isnull(C.MidName,'')+' '+isnull(C.LastName,''),");
                SQL.Append(" H.Year, MONTH(H.Month + ' 1 2014') ");
            }
            else
            {
                SQL.Append("SELECT DISTINCT  E.UniqueID as Id, E.EmployerName, (RAN.PersonID)Month,(RAN.PersonID)Year,sum(D.EmployerContribution) EmployerContribution");
                SQL.Append(" FROM masteremployer E WITH (NOLOCK) INNER JOIN ContributonSheetHeaderFinalise H WITH (NOLOCK) ON E.Id=H.EmployerId ");
                SQL.Append(" INNER JOIN ContributonSheetDetailsFinalise D WITH (NOLOCK) ON H.Id=D.ContributonSheetHeaderFinaliseID INNER JOIN");
                SQL.Append(" (select count(PersonID)PersonID,EmployerID,JobStatusID from MasterContributor WITH (NOLOCK) Group By EmployerID,JobStatusID) RAN ON RAN.EmployerID=E.Id ");
                SQL.Append(" Where 1=1 AND H.isactive='true' AND D.isactive='true' ");
                if (emplrId != null && emplrId != string.Empty)
                    SQL.Append(" AND E.ID = '" + emplrId.Trim().Replace("'", "''") + "'");
                SQL.Append(" AND H.isactive='true'  AND RAN.JobStatusID=1 ");
                if (date1 != null && date1 != string.Empty)//YearTo
                    SQL.Append(" AND ISNULL(EntryDate,'1900/01/01') >= '" + date1.Trim().Replace("'", "''") + "'");
                if (date2 != null && date2 != string.Empty)//YearTo
                    SQL.Append(" AND ISNULL(EntryDate,'1900/01/01') <= '" + date2.Trim().Replace("'", "''") + "'");

                //if (date1 != null && date1 != string.Empty)//YearTo
                //  SQL.Append(" and Convert(datetime,('01/'+[month]+'/'+CONVERT(varchar(10), [Year])),6) >= '" + date1.Trim().Replace("'", "''") + "'");
                //if (date2 != null && date2 != string.Empty)//YearTo
                //  SQL.Append(" and Convert(datetime,('20/'+[month]+'/'+CONVERT(varchar(10), [Year])),6) <= '" + date2.Trim().Replace("'", "''") + "'");
                ////SQL.Append(" and Convert(datetime,(CONVERT(varchar(2),DAY(EOMONTH(Convert(datetime,('01/'+[month]+'/'+CONVERT(varchar(10), [Year])),6))))+'/'+[month]+'/'+CONVERT(varchar(10), [Year])),6) <= '" + date2.Trim().Replace("'", "''") + "'");
                SQL.Append(" Group By  E.UniqueID, E.EmployerName,(RAN.PersonID)");
                SQL.Append(" Order by E.EmployerName ");
            }
            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);

            //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/Reports/DSContributionsDetails.xsd"));
            //ds.WriteXmlSchema(writer);
            //writer.Close();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.HttpContext.Session["ReportName"] = (rpt == "Detailed" ? "rptEmpContributionsDetails.rpt" : "rptEmployerContribution.rpt");
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }
        //ContributionByMonth
        public JsonResult ContributiorByMonth(string EmployeeID, DateTime StartDate)
        {
            int sEmployee = EmployeeID == "" ? 0 : Convert.ToInt32(EmployeeID);

            var demo = from C in db.MasterContributor
                       join E in db.MasterEmployer on C.EmployerID equals E.Id
                       where C.EmployerID == sEmployee && C.FirstAppointmentDate == StartDate
                       select new
                       {
                           EmployerId = E.Id,
                           EmployeeName = E.EmployerName,
                           ContributorId = C.Id,
                           PersonId = C.PersonID,
                           SSNo = C.SocialSecurityNo,
                           ContributorName = C.FirstName + " " + C.MidName + " " + C.LastName,
                           FirstAppointmentDate = C.FirstAppointmentDate,
                           //List of all field according to View
                       };

            demo = demo.OrderByDescending(x => x.FirstAppointmentDate);
            if (demo.Any())
            {
                this.HttpContext.Session["ReportName"] = "ToDo.rpt";
                this.HttpContext.Session["rptSource"] = demo;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        //Contributions by Employer
        [HttpPost]
        public JsonResult ContributionByEmployer(string EmployeeID)
        {
            int sEmployee = EmployeeID == "" ? 0 : Convert.ToInt32(EmployeeID);

            var demo = from E in db.MasterEmployer
                       join C in db.MasterContributor on E.UniqueID equals C.Employer.UniqueID
                       join CSH in db.ContributonSheetHeaderFinalise on E.Id equals CSH.EmployerId
                       join CSD in db.ContributonSheetDetailsFinalise on C.Id equals CSD.ContributorID
                       join CT in db.ContributonTransactions on CSH.Id equals CT.CSDFID
                       where E.Id == sEmployee
                       select new
                       {
                           EmployerId = E.Id,
                           EmployeeName = E.EmployerName,
                           ContributorId = C.Id,
                           PersonId = C.PersonID,
                           SSNo = C.SocialSecurityNo,
                           ContributorName = C.FirstName + " " + C.MidName + " " + C.LastName,
                           SalaryAmount = CSD.SalaryAmount,
                           EmployerContribution = CSD.EmployerContribution,
                           ContributorContribution = CSD.ContributorContribution,
                           Month = CSH.Month,
                           Year = CSH.Year
                           //List of all field according to View
                       };

            demo = demo.OrderBy(x => x.EmployerId).Take(20);
            if (demo.Any())
            {
                this.HttpContext.Session["ReportName"] = "ToDo.rpt";
                this.HttpContext.Session["rptSource"] = demo;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        //Contributions by Contributor
        public JsonResult ContributionByContributer(string ContributorId)
        {
            int sContributor = ContributorId == "" ? 0 : Convert.ToInt32(ContributorId);

            var demo = from C in db.MasterContributor
                       join E in db.MasterEmployer on C.EmployerID equals E.Id
                       join CSH in db.ContributonSheetHeaderFinalise on C.EmployerID equals CSH.EmployerId
                       join CSD in db.ContributonSheetDetailsFinalise on C.Id equals CSD.ContributorID
                       join CT in db.ContributonTransactions on CSD.Id equals CT.CSDFID
                       where C.Id == sContributor
                       select new
                       {
                           EmployerId = E.Id,
                           EmployeeName = E.EmployerName,
                           ContributorId = C.Id,
                           PersonId = C.PersonID,
                           SSNo = C.SocialSecurityNo,
                           ContributorName = C.FirstName + " " + C.MidName + " " + C.LastName,
                           SalaryAmount = CSD.SalaryAmount,
                           EmployerContribution = CSD.EmployerContribution,
                           ContributorContribution = CSD.ContributorContribution,
                           Month = CSH.Month,
                           Year = CSH.Year
                           //List of all field according to View
                       };

            demo = demo.OrderBy(x => x.ContributorId).Take(20);
            if (demo.Any())
            {
                this.HttpContext.Session["ReportName"] = "ToDo.rpt";
                this.HttpContext.Session["rptSource"] = demo;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        //Direct Deposit to Bank
        public JsonResult DirectDepositeToBank(string PensionerId)
        {
            int sPensioner = PensionerId == "" ? 0 : Convert.ToInt32(PensionerId);

            var demo = from P in db.MasterPensioner
                       join B in db.MasterBanks on P.BankID equals B.Bank_Code_ID
                       join PDDD in db.ProcessDirectDepositDetails on P.PensionerID equals PDDD.PensionerID
                       join PDDH in db.ProcessDirectDepositHeader on PDDD.DocNo equals PDDH.DocNo
                       where P.PensionerID == PensionerId
                       select new
                       {
                           PensionerId = P.PensionerID,
                           //List of all field according to View
                       };

            demo = demo.OrderBy(x => x.PensionerId).Take(20);
            if (demo.Any())
            {
                this.HttpContext.Session["ReportName"] = "ToDo.rpt";
                this.HttpContext.Session["rptSource"] = demo;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult PensionPayments()
        {

            ViewBag.Gender = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.EmployeeId = new SelectList(Enumerable.Empty<SelectListItem>());
            List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = new List<DVOMasterEmployee>();//BLLMasterEmployee.GetAllData();
            ViewBag.EmplCode = db.MasterEmpType.Where(x => x.Type_Code != null).Select(x => new SelectListItem
            {
                Value = x.Type_Code,
                Text = x.Type_Code + " | " + x.Description
            })
         .ToList();
            //new SelectList(listSearchResultDVOMasterEmployee, "EmplCode", "FirstName");
            return View();


            //var employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            //ViewBag.EmployeeId = new SelectList(employer, "Id", "EmployerName");

            //ViewBag.EmployeeId = new SelectList(Enumerable.Empty<SelectListItem>());
            //var PermanentTehsilPermanentList = db.MasterEmployees.Select(x => new { Id = x.EmployeeID, Name = x.first_name + " " + x.last_name + "-" + x.PermanentDistrict + " - " + x.PermanentTehsil })
            // .Distinct().OrderBy(x => x.Name).ToList();
            //ViewBag.EmployeeId = new SelectList(PermanentTehsilPermanentList, "Id", "Name");

            //ViewBag.EmplCode = new SelectList(Enumerable.Empty<SelectListItem>());

            ////      List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = new List<DVOMasterEmployee>();//BLLMasterEmployee.GetAllData();
            ////ViewBag.EmplCode = new SelectList(listSearchResultDVOMasterEmployee, "EmplCode", "FirstName");
            //return View();
        }

        public JsonResult PensionPaymentsAjax(string emplrId, string empl_code, string date1, string date2, string gender, int? ageInYears)
        {
            string GenderType = string.Empty;
            switch (gender.ToLower().Trim())
            {
                case "male":
                    GenderType = "M";
                    break;
                case "female":
                    GenderType = "F";
                    break;
                case "transgender":
                    GenderType = "T";
                    break;
            }
            int UserId = AppUserManager.GetUserId();
            int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append("SELECT distinct M.ApplicationReferenceNo,M.EmployerID, M.Soc_Sec_Num, M.birthdate, isnull(M.first_name,'')+' '+isnull(M.middle_name,'')+' '+isnull(M.last_name,'') Pensioner,E.[Description] EmployerName, M.date_hired,M.gender,CAST(DATEDIFF(YEAR, M.birthdate, GETDATE())  AS VARCHAR(10)) AS Age_InYears, ");
            SQL.Append(" P.empl_code, P.pay_date, P.print_check, P.inc_gross,(ded_fica+ded_medicare+ded_fedtax+ded_statax+ded_loctax+ded_other) ded_total,obl_total,inc_net");
            SQL.Append(" FROM MasterEmployee M  WITH (NOLOCK) INNER JOIN Process_PayEmployee P  WITH (NOLOCK) ON M.Empl_Code = P.empl_code");
            SQL.Append(" LEFT JOIN MasterDistrict md  WITH (NOLOCK) on md.[Name] = M.SelectDistrict ");
            SQL.Append(" INNER JOIN MasterEmpType E  WITH (NOLOCK) ON M.type_code = E.type_code ");
            SQL.Append(" Where 1=1 AND md.Id in (select DistrictId from SecRoleLocationModule where RoleId = " + RoleId + " and UserId = " + UserId + ") AND P.ok_to_post='P' ");
            if (!string.IsNullOrEmpty(emplrId))
                SQL.Append(" AND  M.Type_Code = '" + emplrId.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(empl_code))
                SQL.Append(" AND M.empl_code in (" + empl_code.Trim() + ")");
            if (!string.IsNullOrEmpty(date1))//YearTo
                SQL.Append(" and cast(pay_date as date) >= '" + date1.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(date2))//YearTo
                SQL.Append(" and cast(pay_date as date) <= '" + date2.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(gender))// Gender
                SQL.Append(" AND M.Gender = '" + GenderType.Trim().Replace("'", "''") + "'");
            if (ageInYears > 0)//Age In Years
                SQL.Append(" AND CAST(DATEDIFF(YEAR, M.birthdate, GETDATE()) AS VARCHAR(10)) = '" + ageInYears + "'");

            //string con = WebConfigurationManager.AppSettings["SQLConn"];

            string con = ConnectionStringProvider.GetConnectionString();

            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.HttpContext.Session["ReportName"] = "rptPensionPayments.rpt";
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public JsonResult TotalPensionPaymentsAjax(string emplrId, string empl_code, string date1, string date2)
        {
            int UserId = AppUserManager.GetUserId();
            int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append("SELECT distinct M.EmployerID, M.Soc_Sec_Num, M.birthdate, isnull(M.first_name,'')+' '+isnull(M.middle_name,'')+' '+isnull(M.last_name,'') Pensioner,E.[Description] EmployerName, M.date_hired,");
            SQL.Append(" P.empl_code, P.pay_date, P.print_check, P.inc_gross,(ded_fica+ded_medicare+ded_fedtax+ded_statax+ded_loctax+ded_other) ded_total,obl_total,inc_net");
            SQL.Append(" FROM MasterEmployee M  WITH (NOLOCK) INNER JOIN Process_PayEmployee P  WITH (NOLOCK) ON M.Empl_Code = P.empl_code");
            SQL.Append(" LEFT JOIN MasterDistrict md  WITH (NOLOCK) ON md.[Name] = M.SelectDistrict ");
            SQL.Append(" INNER JOIN MasterEmpType E  WITH (NOLOCK) ON M.type_code = E.type_code ");
            SQL.Append(" Where 1=1 AND md.Id in (select DistrictId from SecRoleLocationModule where RoleId = " + RoleId + " and UserId = " + UserId + ") AND P.ok_to_post='P' ");
            if (!string.IsNullOrEmpty(emplrId))
                SQL.Append(" AND  M.Type_Code = '" + emplrId.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(empl_code))
                SQL.Append(" AND M.empl_code in (" + empl_code.Trim() + ")");
            if (!string.IsNullOrEmpty(date1))//YearTo
                SQL.Append(" and cast(pay_date as date) >= '" + date1.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(date2))//YearTo
                SQL.Append(" and cast(pay_date as date) <= '" + date2.Trim().Replace("'", "''") + "'");


            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();

            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.HttpContext.Session["ReportName"] = "rptTotalPensionPayments.rpt";
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public ActionResult TotalPensionPayments()
        {
            ViewBag.EmployeeId = new SelectList(Enumerable.Empty<SelectListItem>());
            List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = new List<DVOMasterEmployee>();//BLLMasterEmployee.GetAllData();
            ViewBag.EmplCode = db.MasterEmpType.Where(x => x.Type_Code != null).Select(x => new SelectListItem
            {
                Value = x.Type_Code,
                Text = x.Type_Code + " | " + x.Description
            }).ToList();
            return View();
        }

        public JsonResult retReport()
        {

            var results = db.MasterContributor.OrderBy(x => x.PersonID).Take(20);
            if (results.Any())
            {
                this.HttpContext.Session["ReportName"] = "rptRetirementsDetails.rpt";
                this.HttpContext.Session["rptSource"] = results;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }
        private void BindCmbMonth(string month)
        {
            List<DVODistinctMonth> objMonthList = BLLPayRollSocSecRpt.GetDistinctMonth_stxperdr();
            DVODistinctMonth obj = new DVODistinctMonth();
            obj.month = string.Empty;
            objMonthList.Insert(0, obj);
            ViewBag.MonthList = new SelectList(objMonthList, "month", "month");
        }

        private void BindCmbYear(string year)
        {
            List<DVODistinctYear> objYearList = BLLPayRollSocSecRpt.GetDistinctYear_stxperdr();
            DVODistinctYear obj = new DVODistinctYear();
            obj.year = string.Empty;
            objYearList.Insert(0, obj);
            ViewBag.YearList = new SelectList(objYearList, "year", "year");
        }

        private void BindCmbPst(string Pst)
        {
            DataTable objDataTable = new DataTable();
            objDataTable.Columns.Add("code");
            objDataTable.Columns.Add("Desc");
            DataRow drA = objDataTable.NewRow();
            drA["code"] = "A";
            drA["Desc"] = "All Entry";
            objDataTable.Rows.Add(drA);

            DataRow drP = objDataTable.NewRow();
            drP["code"] = "P";
            drP["Desc"] = "Posted";
            objDataTable.Rows.Add(drP);
            DataRow drC = objDataTable.NewRow();
            drC["code"] = "C";
            drC["Desc"] = "Current";
            objDataTable.Rows.Add(drC);
            ViewBag.Pst = objDataTable;

        }

        private void BindComboEmployeeType(string type_code)
        {
            //make object to pass as parameter of search function
            DVOMasterEmpTypes objDVOMasterEmpTypes = new DVOMasterEmpTypes();
            //call getDate function of BLL
            List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes).Select(s => new DVOMasterEmpTypes
            {
                type_code = s.type_code,
                //description = string.Format("{0} | {1} | {2} | {3} | {4}", s.type_code, s.description, s.pay_period, s.empl_status, s.hold_pymnt)
                description = string.Format("{0} | {1}", s.type_code, s.description)
            }).ToList();

            objDVOMasterEmpTypes = null;
            ViewBag.EmployeeType = new SelectList(listDVOMasterEmpTypes, "type_code", "description", type_code);
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

        public ActionResult MonthlyDeductions()
        {
            BindCmbMonth(string.Empty);
            BindCmbYear(string.Empty);
            BindCmbPst(string.Empty);
            BindComboEmployeeType(string.Empty);
            BindCombodeductions(string.Empty);
            MonthlyDeductionModel monthlyDeductionModel = new MonthlyDeductionModel();

            return View(monthlyDeductionModel);
        }

        private void BindCombodeductions(string ded_code)
        {
            //make object to pass as parameter of search function
            DVOPRDeductionCodesMasterDedcodes objDVOPRDeductionCodesMasterDedcodes = new DVOPRDeductionCodesMasterDedcodes();
            //call getDate function of BLL
            List<DVOPRDeductionCodesMasterDedcodes> listDVOPRDeductionCodesMasterDedcodes = new List<DVOPRDeductionCodesMasterDedcodes>();

            listDVOPRDeductionCodesMasterDedcodes = BLLPRDeductionCodesMasterDedcodes.GetAllData().Select(s => new DVOPRDeductionCodesMasterDedcodes
            {
                ded_code = s.ded_code,
                description = string.Format("{0} | {1} | {2}", s.RowID, s.ded_code, s.description)
            }).ToList(); ;
            // objDVOPRDeductionCodesMasterDedcodes = null;

            //check list is null or not
            if (listDVOPRDeductionCodesMasterDedcodes != null)
            {
                //make a blank object and insert into first position
                // DVODeductionsGet tmpDVOmasterlocalities = new DVODeductionsGet();
                listDVOPRDeductionCodesMasterDedcodes.Insert(0, objDVOPRDeductionCodesMasterDedcodes);
                //check list has some items or not
                if (listDVOPRDeductionCodesMasterDedcodes.Count > 0)
                {
                    string _ded_code = string.IsNullOrWhiteSpace(ded_code) ? string.Empty : ded_code;
                    ViewBag.Ductions = new SelectList(listDVOPRDeductionCodesMasterDedcodes, "ded_code", "description", _ded_code);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MonthlyDeductions([Bind(Include = "EmpType,Months,Years,DeductionType")] MonthlyDeductionModel monthlyDeductionModel)
        {
            DataSet ds = null;
            BindCmbMonth(monthlyDeductionModel.Months);
            BindCmbYear(monthlyDeductionModel.Years);
            BindCmbPst(monthlyDeductionModel.EmpType);
            BindComboEmployeeType(monthlyDeductionModel.EmpType);
            BindCombodeductions(monthlyDeductionModel.DeductionType);
            if (ModelState.IsValid)
            {
                try
                {
                    //Set Global Code 
                    string[] GlobCode = new string[6];
                    if (monthlyDeductionModel.EmpType == "SALARY")
                    {
                        GlobCode[0] = "'NHC'";                 //gloAllDed
                                                               //gloSevPay
                    }
                    else if (monthlyDeductionModel.EmpType == "WAGES")
                    {
                        GlobCode[0] = "'NHC'";                //gloAllDed
                    }
                    else if (monthlyDeductionModel.EmpType == "PENS" && !string.IsNullOrWhiteSpace(monthlyDeductionModel.DeductionType))
                    {
                        GlobCode[0] = monthlyDeductionModel.DeductionType;
                        //gloAllDed
                    }

                    DVOPaymentToEmployee objSeach = new DVOPaymentToEmployee();
                    objSeach.type_code = monthlyDeductionModel.EmpType;
                    //    objSeach.ok_to_post = cmbPst.SelectedValue.ToString().Trim();
                    objSeach.period = monthlyDeductionModel.Months;
                    objSeach.year = monthlyDeductionModel.Years;

                    ds = BLLPayRollSocSecRpt.GetMonthlyDeductionRptData(ref objSeach, ref GlobCode);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        this.HttpContext.Session["ReportName"] = "rptMonthlyDeductions.rpt";
                        this.HttpContext.Session["rptSource"] = ds;
                    }
                    else
                    {
                        TempData["error"] = "No Elements to process";
                    }
                }
                catch (Exception ex)
                {
                    //ExceptionManagement.ExceptionManager.Publish(ex);
                    TempData["error"] = ex.Message;
                    return View(monthlyDeductionModel);
                }
            }
            return View(monthlyDeductionModel);
        }

        public JsonResult MonthlyDeductionsAjax(string EmpType, string datefrom, string dateto, string DeductionType)
        {
            try
            {
                //Set Global Code 
                string[] GlobCode = new string[6];

                if (!string.IsNullOrWhiteSpace(DeductionType))
                {
                    GlobCode[0] = DeductionType;
                }

                DVOPaymentToEmployee objSeach = new DVOPaymentToEmployee();
                objSeach.type_code = EmpType;
                //    objSeach.ok_to_post = cmbPst.SelectedValue.ToString().Trim();
                if (!string.IsNullOrWhiteSpace(datefrom))
                {
                    //DateTime.Now.ToString(DVOApplicationUserInfo.LocalDateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    objSeach.startdate = datefrom;
                }
                if (!string.IsNullOrWhiteSpace(dateto))
                {
                    objSeach.enddate = dateto;
                }
                BindCombodeductions(string.Empty);

                //DataSet ds = BLLPayRollSocSecRpt.GetMonthlyDeductionRptData(ref objSeach, ref GlobCode);
                DataSet ds = BLLPayRollSocSecRpt.GetDeductionRptData(ref objSeach, ref GlobCode);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count != 0)
                {
                    /*System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/Reports/DSMonthlyDeductions.xsd"));
                    ds.WriteXmlSchema(writer);
                    writer.Close();*/

                    this.HttpContext.Session["ReportName"] = "rptMonthlyDeductions.rpt";
                    this.HttpContext.Session["rptSource"] = ds;
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    TempData["error"] = "No Elements to process";
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //ExceptionManagement.ExceptionManager.Publish(ex);
                TempData["error"] = ex.Message;
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AnnualContributionStatement()
        {
            AnnualContributionStatementModel annualContributionStatementModel = new AnnualContributionStatementModel();
            var employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            ViewBag.EmployeeId = new SelectList(employer.OrderBy(x => x.EmployerName), "Id", "EmployerName");

            var Year = from p in db.ContributonSheetHeaderFinalise
                       where p.IsActive == true
                       group p by p.Year into g
                       select new { Year = g.Key };


            ViewBag.Year = new SelectList(Year.OrderByDescending(x => x.Year), "Year", "Year");

            List<MasterContributor> contributor = new List<MasterContributor>();
            ViewBag.ContributorId = new SelectList(contributor.OrderBy(x => x.FirstName), "Id", "FirstName");
            return View(annualContributionStatementModel);
        }


        public JsonResult AnnualContributionStatementAjax(string emplrId, string contributorid, string Year)
        {
            //if (emplrId == "" || contributorid == "" || Year == "")
            //{ TempData["error"] = "All fidlds are mandatory"; return Json("0", JsonRequestBehavior.AllowGet); }

            string PersonID = db.MasterContributor.Where(x => x.Id.ToString() == contributorid).Select(y => y.PersonID).FirstOrDefault();


            string dtFrom = "01/01/" + Year;
            string dtTo = "12/" + DateTime.DaysInMonth(Convert.ToInt32(Year), 12).ToString() + "/" + Year;
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();

            SQL.Append(" SELECT FirstName, MidName,LastName,PermanentAddress, SocialSecurityNo, PersonID,UniqueID, DateOfBirth, MaritalStatus, ");
            SQL.Append(" EmployerName, HireDate,NormalRetirementAge,ExpectedRetirementDate, Month, Year, ");
            SQL.Append(" sum(ContributorContribution)ContributorContribution,  Gender, PostalAddress,TotalContributorContribution,EntryDate,EmployerID,  MonthNo  ");
            SQL.Append(" from(  ");
            SQL.Append("SELECT DISTINCT C.FirstName, C.MidName, C.LastName,(C.PermanentAddress+' '+isnull(MasterCity.Name,'')+' '+isnull(MC.Name,''))PermanentAddress,  ");
            SQL.Append(" C.SocialSecurityNo, C.PersonID, E.UniqueID,C.DateOfBirth,MasterMaritalStatus.Name As MaritalStatus,E.EmployerName,MCJD.HireDate,");
            SQL.Append(" (Case When MCJD.HireDate<CAST('01/01/2004' AS datetime) Then 60 Else 65 END)NormalRetirementAge,C.ExpectedRetirementDate,");
            SQL.Append(" CSHD.Month, CSHD.Year,     CSDF.ContributorContribution,  C.Gender, C.PostalAddress,Cal.TotalContributorContribution, ");
            SQL.Append(" CONVERT(char(7), CSDF.EntryDate, 121)+'-'+right('00' + cast(datepart(day, cast(EOMONTH (CSDF.EntryDate) as nvarchar(12))) as varchar(2)), 2) EntryDate,");
            SQL.Append(" C.EmployerID, MONTH(CSHD.Month + ' 1 2015') MonthNo ");
            SQL.Append(" FROM	MasterContributor C LEFT JOIN MasterCountry MC on C.CountryID=MC.Id ");
            SQL.Append(" LEFT JOIN MasterCity ON C.PermanentCityID=MasterCity.Id");
            SQL.Append(" LEFT JOIN MasterContributorMarriageDetails M ON M.PersonId=C.PersonID AND M.IsActive=1 AND ");
            SQL.Append(" M.ModifiedOn =(Select TOP 1 Max(ModifiedOn) ModifiedOn From [dbo].[MasterContributorMarriageDetails] where PersonId='" + PersonID + "'  Order By ModifiedOn Desc ) LEFT JOIN");
            SQL.Append(" MasterMaritalStatus ON MasterMaritalStatus.Id=M.MaritalStatusId");
            SQL.Append(" INNER JOIN MasterEmployer E on C.EmployerID=E.Id");
            SQL.Append(" INNER JOIN MasterContributorJobDetails MCJD ON C.PersonID = MCJD.PersonID AND MCJD.ID=");
            SQL.Append(" (select TOP 1 ID from MasterContributorJobDetails WHERE  IsActive=1 AND PersonID='" + PersonID + "' AND HireDate in (SELECT MAX(HireDate) FROM MasterContributorJobDetails WHERE IsActive=1 AND PersonID='" + PersonID + "') ORDER BY HIREDate,JoiningDate,PresentSalary ASC)");
            SQL.Append(" INNER JOIN ContributonSheetDetailsFinalise CSDF ON CSDF.ContributorID=C.Id And CSDF.isactive='true' ");
            SQL.Append(" INNER JOIN ContributonSheetHeaderFinalise CSHD ON CSHD.Id=CSDF.ContributonSheetHeaderFinaliseID AND CSHD.isactive='true' INNER JOIN");
            SQL.Append(" (SELECT ContributonSheetDetailsFinalise.ContributorID,");
            SQL.Append(" Sum(ContributonSheetDetailsFinalise.ContributorContribution)TotalContributorContribution");
            SQL.Append(" FROM ContributonSheetHeaderFinalise INNER JOIN");
            SQL.Append(" ContributonSheetDetailsFinalise ON ContributonSheetHeaderFinalise.Id = ContributonSheetDetailsFinalise.ContributonSheetHeaderFinaliseID  Where 1=1 AND ContributonSheetDetailsFinalise.isactive='true' AND ContributonSheetHeaderFinalise.isactive='true' ");
            if (contributorid != null && contributorid != string.Empty)
                SQL.Append(" AND ContributonSheetDetailsFinalise.ContributorID = '" + contributorid.Trim().Replace("'", "''") + "'");
            if (Year != null && Year != string.Empty)//YearTo
                SQL.Append(" and EntryDate <= '" + dtTo.Trim().Replace("'", "''") + "'");
            SQL.Append(" Group By ContributonSheetDetailsFinalise.ContributorID");
            SQL.Append(" ) Cal on Cal.ContributorID=C.ID");

            SQL.Append(" Where 1=1 ");
            if (emplrId != null && emplrId != string.Empty)
                SQL.Append(" AND  C.EmployerID = " + emplrId.ToString());
            if (contributorid != null && contributorid != string.Empty)
                SQL.Append(" AND C.ID = '" + contributorid.Trim().Replace("'", "''") + "'");
            if (Year != null && Year != string.Empty)//YearTo
                SQL.Append(" and MONTH(CSHD.Month + ' 1 2015') >= 1 and MONTH(CSHD.Month + ' 1 2015')  <= 12 and CSHD.Year=" + Year + "");
            //SQL.Append(" and EntryDate >= '" + dtFrom.Replace("'", "''") + "'");
            //if (Year != null && Year != string.Empty)//YearTo
            //    SQL.Append(" and MONTH(CSHD.Month + ' 1 2015')  <= 12");
            //SQL.Append(" and EntryDate <= '" + dtTo.Trim().Replace("'", "''") + "'");

            SQL.Append(") as nested ");
            SQL.Append(" group by FirstName, MidName,LastName,PermanentAddress, SocialSecurityNo, PersonID,UniqueID, DateOfBirth, MaritalStatus, EmployerName, HireDate, ");
            SQL.Append(" NormalRetirementAge,ExpectedRetirementDate, Month, Year,Gender, PostalAddress,TotalContributorContribution,EntryDate,EmployerID,  MonthNo  ");
            SQL.Append(" Order by MonthNo,Year");



            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();


            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(ds);

            //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/Reports/DSAnnualContributionStatement.xsd"));
            //ds.WriteXmlSchema(writer);
            //writer.Close();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.HttpContext.Session["ReportName"] = "rptAnnualContributionStatement.rpt";
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }



        public ActionResult TransactionHistory()
        {

            ViewBag.updlogid = db.Users.Select(x => new SelectListItem
            {
                Value = x.UserName,
                Text = x.UserName
            })
           .ToList();

            return View();

        }

        public JsonResult TransactionHistoryAjax(JQueryDataTableParamModel param, string Username, string Date1, string Date2, bool isDownload = false)
        {

            DataSet ds = new DataSet();
            //DataTable ds = new DataTable();
            StringBuilder SQL = new StringBuilder();

            SQL.Append("SELECT  AUC.Username as CreatedBy, A.CreatedOn, AUM.Username as ModifiedBy, A.ModifiedOn, A.EventType, A.TableName,  A.ColumnName, A.OldValue, A.NewValue, A.Url, A.Controller,A.Action, A.Area, A.IPAddress, A.RecordId, A.OldSystemId, A.Message FROM AuditLogs A   ");
            SQL.Append(" INNER JOIN AppUser AUC WITH (NOLOCK) ON AUC.Id= A.CreatedBy  ");
            SQL.Append(" LEFT JOIN  AppUser AUM WITH (NOLOCK) ON  AUM.Id  =A.ModifiedBy   ");
            //SQL.Append(" Where A.isactive=1 ");
            SQL.Append(" Where  A.Oldvalue != A.NewValue");
            if (!string.IsNullOrEmpty(Username))
                SQL.Append(" AND AUC.UserName = '" + Username.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(Date1))
                SQL.Append("  and cast(A.CreatedOn as date) >= '" + Date1.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(Date2))
                SQL.Append(" and cast(A.CreatedOn as date) <= '" + Date2.Trim().Replace("'", "''") + "'");

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
                AuditLogs.Url = (dr[9] != DBNull.Value ? (dr[9]).ToString() : "");
                AuditLogs.Controller = (dr[10] != DBNull.Value ? (dr[10]).ToString() : "");
                AuditLogs.Action = (dr[11] != DBNull.Value ? (dr[11]).ToString() : "");
                AuditLogs.Area = (dr[12] != DBNull.Value ? (dr[12]).ToString() : "");
                AuditLogs.IPAddress = (dr[13] != DBNull.Value ? (dr[13]).ToString() : "");
                AuditLogs.RecordId = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);
                AuditLogs.OldSystemId = (dr[15] != DBNull.Value ? Convert.ToInt32(dr[15]) : 0);
                AuditLogs.Message = (dr[16] != DBNull.Value ? (dr[16]).ToString() : "");

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
                   || c.NewValue.ToLower().Contains(param.sSearch.ToLower())
                   || c.Url.ToLower().Contains(param.sSearch.ToLower())
                   || c.Controller.ToLower().Contains(param.sSearch.ToLower())
                   || c.Action.ToLower().Contains(param.sSearch.ToLower())
                   || c.Area.ToLower().Contains(param.sSearch.ToLower())
                   || c.IPAddress.ToLower().Contains(param.sSearch.ToLower())
                   || c.RecordId.ToString().Contains(param.sSearch.ToLower())
                   || c.OldSystemId.ToString().Contains(param.sSearch.ToLower())
                   || c.Message.ToString().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filtered = LogsList;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<AuditLogs, string> orderingFunction = (c => /*sortColumnIndex == 0 ? c.Id + "" :*/
                                                                                            sortColumnIndex == 1 ? c.UsernameCrby + "" :
                                                                                            sortColumnIndex == 2 ? c.CreatedOn + "" :
                                                                                            sortColumnIndex == 3 ? c.UsernameModby + "" :
                                                                                            sortColumnIndex == 4 ? c.ModifiedOn + "" :
                                                                                            //sortColumnIndex == 5 ? c.IsActive + "" :
                                                                                            sortColumnIndex == 6 ? c.EventType + "" :
                                                                                            sortColumnIndex == 7 ? c.TableName :
                                                                                            sortColumnIndex == 8 ? c.ColumnName + "" :
                                                                                            sortColumnIndex == 9 ? c.OldValue :
                                                                                            sortColumnIndex == 10 ? c.NewValue + "" :
                                                                                            sortColumnIndex == 11 ? c.Url + "" :
                                                                                            sortColumnIndex == 12 ? c.Controller + "" :
                                                                                            sortColumnIndex == 13 ? c.Action + "" :
                                                                                            sortColumnIndex == 14 ? c.Area + "" :
                                                                                            sortColumnIndex == 15 ? c.IPAddress + "" :
                                                                                            sortColumnIndex == 16 ? c.RecordId + "" :
                                                                                            sortColumnIndex == 17 ? c.OldSystemId + "" :
                                                                                            sortColumnIndex == 18 ? c.Message + "" :
                                                                                            "");

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filtered = filtered.OrderBy(orderingFunction);
            else
                filtered = filtered.OrderByDescending(orderingFunction);

            //isdownload
            if (isDownload)
            {
                FileContentResult bytesdata = JandKReturnReport(filtered);
                return Json(bytesdata, JsonRequestBehavior.AllowGet);
            }

            //Pagging
            var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            //Select required columns
            var result = from c in displayed
                         select new[] {

                         //c.Id+"",
                         c.UsernameCrby + "",
                         c.CreatedOn + "",
                         c.UsernameModby + "",
                         c.ModifiedOn + "", 
                         //c.IsActive +"",
                         c.EventType,
                         c.TableName,
                         c.ColumnName,
                         c.OldValue,
                         c.NewValue,
                         c.Url,
                         c.Controller,
                         c.Action,
                         c.Area + "",
                         c.IPAddress,
                         c.RecordId + "",
                         c.OldSystemId + "",
                         c.Message
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


        private FileContentResult JandKReturnReport(IEnumerable<dynamic> filtered)
        {
            try
            {
                var result = from c in filtered
                             select new[]
                             {
                         c.UsernameCrby + "",
                         c.CreatedOn + "",
                         c.UsernameModby + "",
                         c.ModifiedOn + "", 
                         //c.IsActive +"",
                         c.EventType,
                         c.TableName,
                         c.ColumnName,
                         c.OldValue,
                         c.NewValue,
                         c.Url,
                         c.Controller,
                         c.Action,
                         c.Area + "",
                         c.IPAddress,
                         c.RecordId + "",
                         c.OldSystemId + "",
                         c.Message
                  };
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
                        cell0.CellValue = new CellValue(" Created By");
                        headerRow.AppendChild(cell0);

                        Cell cell1 = new Cell();
                        cell1.DataType = CellValues.String;
                        cell1.CellValue = new CellValue("Created On");
                        headerRow.AppendChild(cell1);

                        Cell cell2 = new Cell();
                        cell2.DataType = CellValues.String;
                        cell2.CellValue = new CellValue("Modified By");
                        headerRow.AppendChild(cell2);

                        Cell cell3 = new Cell();
                        cell3.DataType = CellValues.String;
                        cell3.CellValue = new CellValue("Modified On");
                        headerRow.AppendChild(cell3);

                        Cell cell4 = new Cell();
                        cell4.DataType = CellValues.String;
                        cell4.CellValue = new CellValue("Event Type");
                        headerRow.AppendChild(cell4);

                        Cell cell5 = new Cell();
                        cell5.DataType = CellValues.String;
                        cell5.CellValue = new CellValue("Table Name");
                        headerRow.AppendChild(cell5);

                        Cell cell6 = new Cell();
                        cell6.DataType = CellValues.String;
                        cell6.CellValue = new CellValue("Column Name");
                        headerRow.AppendChild(cell6);

                        Cell cell7 = new Cell();
                        cell7.DataType = CellValues.String;
                        cell7.CellValue = new CellValue("Old Value");
                        headerRow.AppendChild(cell7);

                        Cell cell8 = new Cell();
                        cell8.DataType = CellValues.String;
                        cell8.CellValue = new CellValue(" New Value ");
                        headerRow.AppendChild(cell8);

                        Cell cell9 = new Cell();
                        cell9.DataType = CellValues.String;
                        cell9.CellValue = new CellValue("Url");
                        headerRow.AppendChild(cell9);

                        Cell cell10 = new Cell();
                        cell10.DataType = CellValues.String;
                        cell10.CellValue = new CellValue("Controller");
                        headerRow.AppendChild(cell10);

                        Cell cell11 = new Cell();
                        cell11.DataType = CellValues.String;
                        cell11.CellValue = new CellValue("Action");
                        headerRow.AppendChild(cell11);

                        Cell cell12 = new Cell();
                        cell12.DataType = CellValues.String;
                        cell12.CellValue = new CellValue("Area");
                        headerRow.AppendChild(cell12);

                        Cell cell13 = new Cell();
                        cell13.DataType = CellValues.String;
                        cell13.CellValue = new CellValue("IP Address");
                        headerRow.AppendChild(cell13);

                        Cell cell14 = new Cell();
                        cell14.DataType = CellValues.String;
                        cell14.CellValue = new CellValue("Record Id");
                        headerRow.AppendChild(cell14);

                        Cell cell15 = new Cell();
                        cell15.DataType = CellValues.String;
                        cell15.CellValue = new CellValue("Old SystemId");
                        headerRow.AppendChild(cell15);

                        Cell cell16 = new Cell();
                        cell16.DataType = CellValues.String;
                        cell16.CellValue = new CellValue("Message");
                        headerRow.AppendChild(cell16);




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

                            Cell cellR12 = new Cell();
                            cellR12.DataType = CellValues.String;
                            cellR12.CellValue = new CellValue(item[12]);
                            dataRow.AppendChild(cellR12);

                            Cell cellR13 = new Cell();
                            cellR13.DataType = CellValues.String;
                            cellR13.CellValue = new CellValue(item[13]);
                            dataRow.AppendChild(cellR13);

                            Cell cellR14 = new Cell();
                            cellR14.DataType = CellValues.String;
                            cellR14.CellValue = new CellValue(item[14]);
                            dataRow.AppendChild(cellR14);

                            Cell cellR15 = new Cell();
                            cellR15.DataType = CellValues.String;
                            cellR15.CellValue = new CellValue(item[15]);
                            dataRow.AppendChild(cellR15);

                            Cell cellR16 = new Cell();
                            cellR16.DataType = CellValues.String;
                            cellR16.CellValue = new CellValue(item[16]);
                            dataRow.AppendChild(cellR16);

                            //Cell cellR17 = new Cell();
                            //cellR17.DataType = CellValues.String;
                            //cellR17.CellValue = new CellValue(item[17]);
                            //dataRow.AppendChild(cellR17);


                            sheetData.AppendChild(dataRow);
                        }
                        workbookPart.Workbook.Save();
                    }
                    bytesdata = File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, "Transaction History Log.xlsx");
                }
                return bytesdata;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        ///******* Code By Himanshu Rajput *******

        public ActionResult MediaDownloads()
        {
            //ViewBag.EmployeeId = new SelectList(Enumerable.Empty<SelectListItem>());
            //ViewBag.Gender = new SelectList(Enumerable.Empty<SelectListItem>());
            //List<DVOMediaDownloads> listSearchResultDVOMediaDownloads = new List<DVOMediaDownloads>();//BLLMediaDownloads.GetAllData();

            //ViewBag.MediaDownload = db.MediaDownloads.Select(x => new SelectListItem
            //{
            //    Value = x.FileName,
            //    Text = x.FileName
            //}).ToList();
            return View();
        }

        public JsonResult MediaDownloadsAjax(JQueryDataTableParamModel param, string Date1, string Date2)
        {

            DataSet ds = new DataSet();
            //DataTable ds = new DataTable();
            StringBuilder SQL = new StringBuilder();
            IEnumerable<MediaDownloads> filtered;

            SQL.Append("SELECT  MediaDownloads.FileName,MediaDownloads.HasDownoaded,MediaDownloads.IsProcessed,MediaDownloads.CreatedOn FROM MediaDownloads WHERE MediaDownloads.MediaType = 2");

            if (!string.IsNullOrEmpty(Date1))
                SQL.Append("  and cast(MediaDownloads.CreatedOn as date) >= '" + Date1.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(Date2))
                SQL.Append(" and cast(MediaDownloads.CreatedOn as date) <= '" + Date2.Trim().Replace("'", "''") + "'");

            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);
            List<MediaDownloads> MediaList = App.Web.Repository.ListToDataset.ToList<MediaDownloads>(ds.Tables[0]);

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        MediaDownloads yy = new MediaDownloads();
            //        yy.FileName = "ttt";
            //        MediaList.Add(yy);
            //    }
            //}



            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = MediaList
                   .Where(c => c.FileName.ToString().Contains(param.sSearch.ToLower())
                   || c.HasDownoaded.ToString().Contains(param.sSearch.ToLower())
                   || c.IsProcessed.ToString().Contains(param.sSearch.ToLower())
                   || c.CreatedOn.ToString().Contains(param.sSearch.ToLower())
                   );
            }
            else
            {
                filtered = MediaList;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MediaDownloads, string> orderingFunction = (c => /*sortColumnIndex == 0 ? c.Id + "" :*/
                                                                                            sortColumnIndex == 0 ? c.FileName + "" :
                                                                                            sortColumnIndex == 1 ? c.HasDownoaded + "" :
                                                                                            sortColumnIndex == 2 ? c.IsProcessed + "" :
                                                                                            sortColumnIndex == 3 ? c.CreatedOn + "" :

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
                         c.FileName + "",
                         c.HasDownoaded + "",
                         c.IsProcessed + "",
                         c.CreatedOn +""

                   };

            return Json(
                                        new
                                        {
                                            sEcho = param.sEcho,
                                            iTotalRecords = MediaList.Count(),
                                            iTotalDisplayRecords = filtered.Count(),
                                            aaData = result
                                        }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult MediaQueue()
        {

            return View();
        }

        public JsonResult MediaQueueAjax(JQueryDataTableParamModel param, string Date1, string Date2)
        {

            DataSet ds = new DataSet();
            //DataTable ds = new DataTable();
            StringBuilder SQL = new StringBuilder();
            IEnumerable<Media_Queue> filtered;

            SQL.Append("SELECT  Media_Queue.FilePath,Media_Queue.IsUploaded,Media_Queue.UploadedDate,Media_Queue.UploadErrors,Media_Queue.CreatedOn FROM Media_Queue WHERE Media_Queue.MediaType = 3");

            if (!string.IsNullOrEmpty(Date1))
                SQL.Append("  and cast(Media_Queue.CreatedOn as date) >= '" + Date1.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(Date2))
                SQL.Append(" and cast(Media_Queue.CreatedOn as date) <= '" + Date2.Trim().Replace("'", "''") + "'");

            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();

            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);
            List<Media_Queue> MediaList = App.Web.Repository.ListToDataset.ToList<Media_Queue>(ds.Tables[0]);

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = MediaList
                   .Where(c => c.FilePath.ToString().Contains(param.sSearch.ToLower())
                   || c.IsUploaded.ToString().Contains(param.sSearch.ToLower())
                   || c.UploadedDate.ToString().Contains(param.sSearch.ToLower())
                   || c.UploadErrors.ToString().Contains(param.sSearch.ToLower())
                   || c.CreatedOn.ToString().Contains(param.sSearch.ToLower())
                   );
            }
            else
            {
                filtered = MediaList;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<Media_Queue, string> orderingFunction = (c => /*sortColumnIndex == 0 ? c.Id + "" :*/
                                                                                            sortColumnIndex == 0 ? c.FilePath + "" :
                                                                                            sortColumnIndex == 1 ? c.IsUploaded + "" :
                                                                                            sortColumnIndex == 2 ? c.UploadedDate + "" :
                                                                                            sortColumnIndex == 3 ? c.UploadErrors + "" :
                                                                                            sortColumnIndex == 4 ? c.CreatedOn + "" :

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
                         c.FilePath + "",
                         c.IsUploaded + "",
                         c.UploadedDate + "",
                         c.UploadErrors +"",
                         c.CreatedOn +""

                   };

            return Json(
                                        new
                                        {
                                            sEcho = param.sEcho,
                                            iTotalRecords = MediaList.Count(),
                                            iTotalDisplayRecords = filtered.Count(),
                                            aaData = result
                                        }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Arrears()
        {
            ViewBag.DistrictId = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.Group = GetUsersAssignedLocations();
            ViewBag.Gender = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.EmployeeId = new SelectList(Enumerable.Empty<SelectListItem>());
            List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = new List<DVOMasterEmployee>();//BLLMasterEmployee.GetAllData();
            var defaultItem = new SelectListItem { Value = "ALL", Text = "ALL | ALL" };
            var emplCodeList = db.MasterEmpType
                .Where(x => x.Type_Code != null)
                .Select(x => new SelectListItem
                {
                    Value = x.Type_Code,
                    Text = x.Type_Code + " | " + x.Description
                }).ToList();
            emplCodeList.Insert(0, defaultItem); // Insert the default item at the beginning
            ViewBag.EmplCode = emplCodeList;
            return View();
        }

        public JsonResult ArrearsAjax(string emplrId, string empl_code, string date1, string date2, int? ageInYears, string gender, string finacialYear, string RegionNames)
        {
            string GenderType = string.Empty;
            string fyear = finacialYear.Replace("_", "-");
            switch (gender.ToLower().Trim())
            {
                case "male":
                    GenderType = "M";
                    break;
                case "female":
                    GenderType = "F";
                    break;
                case "transgender":
                    GenderType = "T";
                    break;
            }

            // get current date1 and date2 as per financial year if date1 or date2 is null or empty
            if (string.IsNullOrEmpty(date1))
            {
                date1 = GetStartDateDateAsPerFinancialYear().ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(date2))
            {
                date2 = GetEndDateDateAsPerFinancialYear().ToString("yyyy-MM-dd");
            }

            int UserId = AppUserManager.GetUserId();
            int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append("select distinct ApplicationReferenceno,CONCAT(NULLIF(isnull(me.first_name,''), ''), CASE  ");
            SQL.Append("WHEN me.middle_name IS NOT NULL AND me.middle_name != '' THEN ' ' + me.middle_name ELSE '' END, CASE WHEN me.last_name IS NOT NULL AND me.last_name != '' THEN ' ' + me.last_name  ");
            SQL.Append("ELSE '' END ) as ApplicantName,Process_PayIncomes.amount,CONVERT(varchar, dd.pay_date, 103) AS pay_date,et.[Description] as type_code,DATEDIFF(YEAR, birthdate, GETDATE()) - CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, birthdate, GETDATE()), birthdate) > GETDATE() THEN 1 ELSE 0 END AS Age_InYears,Gender,concat(nullif(isnull(PresentVillageName,''),''), case when PresentHalqaPanchayatOrMunicipalityName is not null or PresentHalqaPanchayatOrMunicipalityName != '' ");
            SQL.Append("then ' ' + PresentHalqaPanchayatOrMunicipalityName else '' end, case when PresentTehsil is not null or PresentTehsil != '' ");
            SQL.Append("then ' ' + PresentTehsil else '' end, case when PresentDistrict is not null or PresentDistrict != '' then ");
            SQL.Append("' ' + PresentDistrict else '' end) as [Address],dd.bank_acct_no,BankName,APPLICANT_BANK_IFSC_CODE as [IFSCCode],dd.[Status],me.SelectDistrict as [District],MONTH(ISNULL(TransactionDate, dd.pay_date)) AS Month,YEAR(ISNULL(TransactionDate, dd.pay_date)) AS Year, '" + fyear + "' AS FinacialYear , dd.[TransactionRefrenceNo.] as TransactionRefrenceNo, CONVERT(varchar, dd.TransactionDate, 103) as TransactionDate, dd.[Reason/Remarks]   from Process_DirectDeposit_Details dd JOIN Process_PayIncomes ON Process_PayIncomes.doc_no = dd.pay_doc_no AND Process_PayIncomes.inc_code = (SELECT TOP 1 inc_code FROM MasterIncCodes WHERE [description] = 'Arrear') ");
            SQL.Append("left join masterEmployee me on me.Empl_code = dd.Empl_code left join MasterEmpBankDetails mb on mb.empl_code = me.Empl_code ");
            SQL.Append(" left join MasterEmpType et on me.type_code = et.type_code ");
            SQL.Append("left join MasterDistrict md on md.[Name] = me.SelectDistrict where Status = 'OK' and md.Id in (select DistrictId from SecRoleLocationModule where RoleId = " + RoleId + " and UserId = " + UserId + ")");

            //"AND 
            //if (!string.IsNullOrEmpty(emplrId) && emplrId == "ALL")
            //    SQL.Append(" AND me.Type_Code = '" + "*" + "'");

            if (!string.IsNullOrEmpty(emplrId) && emplrId == "ALL")
                SQL.Append("");
            else if (!string.IsNullOrEmpty(emplrId))
                SQL.Append(" AND  me.Type_Code = '" + emplrId.Trim().Replace("'", "''") + "'");

            //if (!string.IsNullOrEmpty(emplrId))
            //  SQL.Append(" AND  me.Type_Code = '" + emplrId.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(empl_code))
                SQL.Append(" AND dd.empl_code in (" + empl_code.Trim() + ")");
            if (!string.IsNullOrEmpty(date1))//YearTo
                SQL.Append(" and cast(dd.pay_date as date) >= '" + date1.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(date2))//YearTo
                SQL.Append(" and cast(dd.pay_date as date) <= '" + date2.Trim().Replace("'", "''") + "'");
            if (!string.IsNullOrEmpty(gender))// Gender
                SQL.Append(" AND me.Gender = '" + GenderType.Trim() + "'");

            //if (!string.IsNullOrEmpty(date1))//YearTo
            //    SQL.Append(" and pay_date between '" + date1.Trim().Replace("'", "''") + "' and '" + date2.Trim().Replace("'", "''") + "'");

            if (ageInYears != null && ageInYears != 0)//Age In Years
                SQL.Append(" AND CAST(DATEDIFF(YEAR, me.birthdate, GETDATE())  AS VARCHAR(10)) = '" + ageInYears + "'");
            if (!string.IsNullOrEmpty(RegionNames))
                SQL.Append(" AND me.SelectDistrict in (SELECT * FROM [SplitString] ('" + RegionNames.Trim() + "'))");
            //if (!string.IsNullOrEmpty(emplrId))
            //  SQL.Append("where dd.empl_code = "+ emplrId + " and Status = 'fail'"); 

            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);

            //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/reports/DsArrears.xsd"));
            //ds.WriteXmlSchema(writer);
            //writer.Close();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                /*this.HttpContext.Session["ReportName"] = "rptPaymentFailedReport.rpt";*/      //"rptPensionDetailsByYear.rpt";
                this.HttpContext.Session["ReportName"] = "rptArrears.rpt";      //"rptPensionDetailsByYear.rpt";
                this.HttpContext.Session["ReportName1"] = Path.Combine(Server.MapPath("~/Reports/rptArrears.rpt"));
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        //new report Beneficiary Payment Details
        public ActionResult BeneficiaryPayment()
        {
            return View();
        }
        public JsonResult GetBatchAjax(string date1, string date2)
        {
            DateTime fromDate = Convert.ToDateTime(date1, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
            DateTime toDate = Convert.ToDateTime(date2, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);

            //string Payfromdt = fromDate.ToString("dd/MM/yyyy");
            //string Paytodt = toDate.ToString("dd/MM/yyyy");
            string Payfromdt = fromDate.ToString("yyyy-MM-dd");
            string Paytodt = toDate.ToString("yyyy-MM-dd");

            List<TotalContributionSummaryViewModel> listCounts = new List<TotalContributionSummaryViewModel>();
            DataSet ds = new DataSet();
            StringBuilder SQL = new StringBuilder();
            SQL.Append(" SELECT DISTINCT PPH.pybatchid FROM Payroll_Process_Header AS PPH LEFT JOIN Process_PayEmployee PPE ON PPE.pybatchid = PPH.pybatchid WHERE PPE.ok_to_post IN ('P','N','Y') AND CONVERT(DATETIME, SUBSTRING(PPH.searchcriteria,CHARINDEX('Payroll Date: ', PPH.searchcriteria) + LEN('Payroll Date: '), 11), 103) BETWEEN '" + Payfromdt.Trim().Replace("'", "''") + "' and '" + Paytodt.Trim().Replace("'", "''") + "'");

            //string con = WebConfigurationManager.AppSettings["SQLConn"];
            string con = ConnectionStringProvider.GetConnectionString();
            SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
            da.Fill(ds);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TotalContributionSummaryViewModel ContributorCont = new TotalContributionSummaryViewModel();
                    ContributorCont.pybatchid = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);

                    listCounts.Add(ContributorCont);
                }
            }
            return Json(new { listCounts }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BeneficiaryPaymentAjax(int batchno, string finacialYear)
        {
            try
            {
                string fyear = finacialYear.Replace("_", "-");
                int UserId = AppUserManager.GetUserId();
                int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

                object[] parameters = new object[3];
                parameters[0] = RoleId;
                parameters[1] = UserId;
                parameters[2] = batchno;

                DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(TotalContributionSummaryViewModel), "USP_PaymentTotalCount");

                ds.Tables[0].Columns.Add(new DataColumn("Total", typeof(Int32)));
                ds.Tables[0].Columns.Add(new DataColumn("FinancialYear", typeof(string)));
                ds.Tables[0].Columns.Add(new DataColumn("MonthName", typeof(string)));


                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int total = 0;

                    total += (row["PCP"] != DBNull.Value) ? Convert.ToInt32(row["PCP"]) : 0;
                    total += (row["OAP"] != DBNull.Value) ? Convert.ToInt32(row["OAP"]) : 0;
                    total += (row["WID"] != DBNull.Value) ? Convert.ToInt32(row["WID"]) : 0;
                    total += (row["TGR"] != DBNull.Value) ? Convert.ToInt32(row["TGR"]) : 0;


                    row["Total"] = total;
                    row["FinancialYear"] = fyear;
                    //DateTime payDate = Convert.ToDateTime(row["pay_date"]);
                    //string monthName = payDate.ToString("MMMM");
                    //row["MonthName"] = monthName;

                    int PayDateMonth = 0;
                    string PatDate = (row["pay_date"] != DBNull.Value) ? row["pay_date"].ToString() : "";
                    string[] PayDateArray = PatDate.Split('/');
                    if (PayDateArray.Length > 2)
                    {
                        PayDateMonth = Convert.ToInt32(PayDateArray[1]);
                    }

                    if (PayDateMonth != 0)
                    {
                        row["MonthName"] = new DateTime(1, PayDateMonth, 1).ToString("MMMM");
                    }
                    else
                    {
                        row["MonthName"] = "";
                    }
                }
                //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/reports/DsBeneficiarypayments.xsd"));
                //ds.WriteXmlSchema(writer);
                //writer.Close();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    this.HttpContext.Session["ReportName"] = "rptBeneficiarypayment.rpt";      //"rptPensionDetailsByYear.rpt";
                    this.HttpContext.Session["ReportName1"] = Path.Combine(Server.MapPath("~/Reports/rptBeneficiarypayment.rpt"));
                    this.HttpContext.Session["rptSource"] = ds;
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BeneficiaryStatus(string Tehsil = "", string BeneficiariesType = "", string Gender = "", string RegionNames = "", string AccountStatus = "", string bank_acct_no = "", string APPLICATION_REFERENCE_NO = "")
        {
            int userid = AppUserManager.GetUserId();
            var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
            var model = (from c in db.SecModule.Where(x => x.ControllerName == "MasterPensioner" && x.ActionName == "Index")
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
                //listSearch = BLLMasterEmployee.GetAllData(Tehsil, BeneficiariesType, Gender, RegionNames);
                ViewBag.Group = GetUsersAssignedLocations();
                //PensionProcessViewModel Paysearch = ShowActiveBatch();
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


                //ViewBag.ApplicantIFSCCode = new SelectList(db.MasterEmpBankDetails.GroupBy(x => x.APPLICANT_BANK_IFSC_CODE).Select(x => new { APPLICANT_BANK_IFSC_CODE = x.Key }).ToList(), "APPLICANT_BANK_IFSC_CODE", "APPLICANT_BANK_IFSC_CODE", string.Empty);

                // Controller code to populate ViewBag.bank_acct_no
                //ViewBag.bank_acct_no = new SelectList(db.MasterEmpBankDetails  .Where(x => x.bank_acct_no != "")
                //    .OrderBy(x => x.bank_acct_no), "Id", "bank_acct_no", model.bank_acct_no);


                //ViewBag.bank_acct_no = db.MasterEmpBankDetails.Where(x => x.bank_acct_no != null).Select(x => new SelectListItem
                //{
                //  Value = x.bank_acct_no,
                //  Text = x.bank_acct_no
                //}).ToList();

                //ViewBag.APPLICATION_REFERENCE_NO = db.MasterEmpBankDetails.Where(x => x.bank_acct_no != null).Select(x => new SelectListItem
                //{
                //  Value = x.APPLICATION_REFERENCE_NO,
                //  Text = x.APPLICATION_REFERENCE_NO
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

        public JsonResult BeneficiaryStatusAjax(JQueryDataTableParamModel param, string Tehsil = "", string BeneficiariesType = "", string Gender = "", string RegionNames = "", bool isDownload = false, string AccountStatus = "", string bank_acct_no = "", string APPLICATION_REFERENCE_NO = "")
        {
            List<DVOMasterEmployee> listSearch = new List<DVOMasterEmployee>();
            DataSet ds = null;

            int UserId = AppUserManager.GetUserId();
            int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
            if (AccountStatus == "ALL")
                AccountStatus = null;

            ds = BLLMasterEmployee.GetAllBeneficiaryStatusReportData(UserId, RoleId, Tehsil, BeneficiariesType, Gender, RegionNames, 0, 10000, AccountStatus, bank_acct_no, APPLICATION_REFERENCE_NO);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                // Get the value of Column1
                string fullName = (row["first_name"] == DBNull.Value ? "" : row["first_name"].ToString()) + (row["middle_name"] == DBNull.Value ? "" : " " + row["middle_name"].ToString()) + (row["last_name"] == DBNull.Value ? "" : " " + row["last_name"].ToString());

                // Update Column2 based on Column1
                row["first_name"] = fullName;
            }
            //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/reports/DsBeneficiaryStatusDetail.xsd"));
            //ds.WriteXmlS  chema(writer);
            //writer.Close();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    /*this.HttpContext.Session["ReportName"] = "rptPaymentFailedReport.rpt";*/      //"rptPensionDetailsByYear.rpt";
                    this.HttpContext.Session["ReportName"] = "rptBeneficiaryStatusDetail.rpt";      //"rptPensionDetailsByYear.rpt";
                    this.HttpContext.Session["ReportName1"] = Path.Combine(Server.MapPath("~/Reports/rptBeneficiaryStatusDetail.rpt"));
                    this.HttpContext.Session["rptSource"] = ds;
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        //**** Code By Himanshu Rajput ****

        [HttpGet]
        public ActionResult Exception_log()
        {
            try
            {
                ViewBag.FromDate = DateTime.Now.Date.AddDays(-7).ToString("dd/MM/yyyy");
                ViewBag.ToDate = (DateTime.Now.Date).ToString("dd/MM/yyyy");

                return View();
            }
            catch (Exception ex)
            {
                // Log the exception
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
        }

        // End

        // **** Code By Himanshu Rajput ***
        private FileContentResult ExceptionLogExce(IEnumerable<dynamic> listSearch)
        {
            try
            {
                var result = from c in listSearch
                             select new[] {
                 c.ErrorMessage,
                 c.DateOccurred,
                 c.StackTrace,
                 c.UserName,
                 c.MachineName,
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
                        cell0.CellValue = new CellValue("Error Message");
                        headerRow.AppendChild(cell0);

                        Cell cell1 = new Cell();
                        cell1.DataType = CellValues.Date;
                        cell1.CellValue = new CellValue("Date");
                        headerRow.AppendChild(cell1);

                        Cell cell2 = new Cell();
                        cell2.DataType = CellValues.String;
                        cell2.CellValue = new CellValue("Error Location");
                        headerRow.AppendChild(cell2);


                        Cell cell3 = new Cell();
                        cell3.DataType = CellValues.String;
                        cell3.CellValue = new CellValue("User Name");
                        headerRow.AppendChild(cell3);

                        Cell cell4 = new Cell();
                        cell4.DataType = CellValues.String;
                        cell4.CellValue = new CellValue("Machine Info");
                        headerRow.AppendChild(cell4);


                        sheetData.AppendChild(headerRow);

                        foreach (var item in result)
                        {
                            Row dataRow = new Row();

                            Cell cellR0 = new Cell();
                            cellR0.DataType = CellValues.String;
                            cellR0.CellValue = new CellValue(item[0]);
                            dataRow.AppendChild(cellR0);

                            Cell cellR1 = new Cell();
                            cellR1.DataType = CellValues.String; // Change to String for custom formatting
                            DateTime dateValue = (DateTime)item[1]; // Assuming item[1] is a DateTime
                            cellR1.CellValue = new CellValue(dateValue.ToString("dd/MM/yyyy HH:mm:ss")); // Customize the format string as per your requirement
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

                            sheetData.AppendChild(dataRow);
                        }
                        workbookPart.Workbook.Save();
                    }
                    bytesdata = File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, "ExceptionSummary.xlsx");
                }
                return bytesdata;
            }
            catch (Exception ex)
            {
                // Log the exception
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
        }

        // End


        // ***** Code By Himanshu Rajput
        public JsonResult Exception_logAjax(JQueryDataTableParamModel param, string date1, string date2, bool isDownload = false)
        {
            try
            {

                DataSet ds = new DataSet();
                StringBuilder SQL = new StringBuilder();
                SQL.Append("SELECT  E.ErrorMessage,E.DateOccurred, E.StackTrace,U.UserName,MachineName from ErrorLogs E join AppUser U on E.UserId = U.Id  ");

                if (!string.IsNullOrEmpty(date1))//FromDate
                {
                    SQL.Append(" where cast(E.DateOccurred as date) >= CONVERT(date,'" + date1 + "',103)");
                }
                if (!string.IsNullOrEmpty(date2))//FromDate
                {
                    if (!string.IsNullOrEmpty(date1))
                    {
                        SQL.Append(" and cast(E.DateOccurred as date) <= CONVERT(date,'" + date2 + "',103)");
                    }
                    else
                    {
                        SQL.Append(" Where cast(E.DateOccurred as date) <= CONVERT(date,'" + date2 + "',103)");
                    }
                }

                // Adding ORDER BY clause for descending order based on ID
                SQL.Append("ORDER BY E.Id DESC");


                //string con = WebConfigurationManager.AppSettings["SQLConn"];
                string con = ConnectionStringProvider.GetConnectionString();
                SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
                da.Fill(ds);

                List<ErrorLogs> LogsList = new List<ErrorLogs>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ErrorLogs Exception_logs = new ErrorLogs();

                    Exception_logs.ErrorMessage = (dr[0] != DBNull.Value ? (dr[0]).ToString() : "");

                    if (dr[1] != DBNull.Value && dr[1].ToString().Trim() != string.Empty)
                        Exception_logs.DateOccurred = Convert.ToDateTime(dr[1]);

                    Exception_logs.StackTrace = (dr[2] != DBNull.Value ? (dr[2]).ToString() : "");

                    Exception_logs.UserName = (dr[3] != DBNull.Value ? (dr[3]).ToString() : "");
                    Exception_logs.MachineName = (dr[4] != DBNull.Value ? (dr[4]).ToString() : "");


                    LogsList.Add(Exception_logs);
                }

                IEnumerable<ErrorLogs> filtered;
                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    filtered = LogsList
                       .Where(c => c.ErrorMessage.ToString().ToLower().Contains(param.sSearch.ToLower())
                       || c.DateOccurred.ToString().Contains(param.sSearch.ToLower())
                       || c.StackTrace.ToString().ToLower().Contains(param.sSearch.ToLower())
                       || c.UserName.ToString().ToLower().Contains(param.sSearch.ToLower())
                       || c.MachineName.ToString().ToLower().Contains(param.sSearch.ToLower())

                       );
                }
                else
                {
                    filtered = LogsList;
                    if (isDownload)
                    {
                        FileContentResult bytesdata = ExceptionLogExce(filtered);
                        return Json(bytesdata, JsonRequestBehavior.AllowGet);
                    }
                }

                //Sorting through column index
                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);


                Func<ErrorLogs, string> orderingFunction = (c =>
                                                                                                sortColumnIndex == 0 ? c.ErrorMessage + "" :
                                                                                                 sortColumnIndex == 1 ? c.DateOccurred + "" :
                                                                                                sortColumnIndex == 2 ? c.StackTrace + "" :
                                                                                                sortColumnIndex == 3 ? c.UserName + "" :
                                                                                                sortColumnIndex == 4 ? c.MachineName + "" :

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
                 c.ErrorMessage + "",
                 c.DateOccurred +"",
                 c.StackTrace + "",
                 c.UserName + "",
                 c.MachineName + "",

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
                // Log the exception
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw;
            }

        }

        //End


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

        public ActionResult PaymentsSummary(string Tehsil = "", string BeneficiariesType = "", string Gender = "", string RegionNames = "", string ACCOUNT_STATUS = "", string bank_acct_no = "", string APPLICATION_REFERENCE_NO = "")
        {
            int userid = AppUserManager.GetUserId();
            var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
            var model = (from c in db.SecModule.Where(x => x.ControllerName == "MasterPensioner" && x.ActionName == "Index")
                         join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                         from p in ps.DefaultIfEmpty()
                         select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Beneficiary Details", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();


            ViewBag.ACCOUNT_STATUS = ACCOUNT_STATUS;
            ViewBag.Gender = Gender;

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
                               type_code = s.description,
                               description = string.Format("{0} | {1}", s.type_code, s.description)
                           })
                           .ToList();

                //listDVOMasterEmpTypes.Insert(0, new DVOMasterEmpTypes { type_code = "ALL", description = "ALL | ALL" }); // Insert the default item at the beginning

                var selectListItems = listDVOMasterEmpTypes
                    .Select(empType => new SelectListItem
                    {
                        Value = empType.type_code,
                        Text = empType.description
                    })
                    .ToList();
                objDVOMasterEmpTypes = null;
                ViewBag.BeneficiariesType = new SelectList(selectListItems, "Value", "Text", (string.IsNullOrEmpty(BeneficiariesType) ? "PHYSICALLY CHALLENGED PERSON" : BeneficiariesType));




                //List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes).Select(s => new DVOMasterEmpTypes
                //{
                //    type_code = s.type_code,
                //    //description = string.Format("{0} | {1} | {2} | {3} | {4}", s.type_code, s.description, s.pay_period, s.empl_status, s.hold_pymnt)
                //    description = string.Format("{0} | {1}", s.type_code, s.description)
                //}).ToList();

                //objDVOMasterEmpTypes = null;
                //ViewBag.BeneficiariesType = new SelectList(listDVOMasterEmpTypes, "type_code", "description", (string.IsNullOrEmpty(BeneficiariesType) ? BeneficiariesType : ""));
               // ViewBag.ApplicantIFSCCode = new SelectList(db.MasterEmpBankDetails.GroupBy(x => x.APPLICANT_BANK_IFSC_CODE).Select(x => new { APPLICANT_BANK_IFSC_CODE = x.Key }).ToList(), "APPLICANT_BANK_IFSC_CODE", "APPLICANT_BANK_IFSC_CODE", string.Empty);
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

                return View();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private FileContentResult JandKBeneficiaryReport(IEnumerable<dynamic> listSearch)
        {
            try
            {
                var result = from x in listSearch
                             select new[]
                             {

                            x.ApplicationReferenceNo + "",
                            x.FirstName + "",
                            x.Gender + "",
                            x.AgeInYears + "",
                            x.Address1,
                            x.Phone,
                            x.mailid,
                            x.BankAcctNo,
                            x.IFSCCode,
                            x.BankName,
                            x.LastVerified,
                            x.type_desc,
                            UrlEncryption.EncryptURL(Convert.ToString(x.EmplCode)),
                            x.MiddleName + "",
                            x.LastName + "",
                            x.NameoftheApplicant + "",
                            x.ACCOUNT_STATUS.Trim() == "ACTIVE" ? "Validated" : "",
                            x.ReasonForChange + "",

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
                        cell4.CellValue = new CellValue("Address1");
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
                            cellR10.CellValue = new CellValue(item[16]);
                            dataRow.AppendChild(cellR10);

                            Cell cellR11 = new Cell();
                            cellR11.DataType = CellValues.String;
                            cellR11.CellValue = new CellValue(item[17]);
                            dataRow.AppendChild(cellR11);

                            //Cell cellr10 = new Cell();
                            //cellr10.datatype = Cellvalues.string;
                            //cellr10.cellvalue = new cellvalue(item[10]);
                            //datarow.appendchild(cellr10);

                            //cell cellr11 = new cell();
                            //cellr11.datatype = cellvalues.string;
                            //cellr11.cellvalue = new cellvalue(item[11]);
                            //datarow.appendchild(cellr11);

                            //Cell cellR12 = new Cell();
                            //cellR12.DataType = CellValues.String;
                            //cellR12.CellValue = new CellValue(item[12]);
                            //dataRow.AppendChild(cellR12);

                            //Cell cellR13 = new Cell();
                            //cellR13.DataType = CellValues.String;
                            //cellR13.CellValue = new CellValue(item[13]);
                            //dataRow.AppendChild(cellR13);

                            //Cell cellR14 = new Cell();
                            //cellR14.DataType = CellValues.String;
                            //cellR14.CellValue = new CellValue(item[14]);
                            //dataRow.AppendChild(cellR14);

                            //Cell cellR15 = new Cell();
                            //cellR15.DataType = CellValues.String;
                            //cellR15.CellValue = new CellValue(item[15]);
                            //dataRow.AppendChild(cellR15);

                            //Cell cellR16 = new Cell();
                            //cellR16.DataType = CellValues.String;
                            //cellR16.CellValue = new CellValue(item[16]);
                            //dataRow.AppendChild(cellR16);

                            //Cell cellR17 = new Cell();
                            //cellR17.DataType = CellValues.String;
                            //cellR17.CellValue = new CellValue(item[17]);
                            //dataRow.AppendChild(cellR17);


                            sheetData.AppendChild(dataRow);
                        }
                        workbookPart.Workbook.Save();
                    }
                    bytesdata = File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, "Beneficiary Report.xlsx");
                }
                return bytesdata;
            }
            catch (Exception ex)
            {

                throw;
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

        public async Task<ActionResult> PaymentsSummaryAjaxHandler(JQueryDataTableParamModel param, string Tehsil = "", string BeneficiariesType = "", string Gender = "", bool isDownload = false, string ACCOUNT_STATUS = "")
        {
            try
            {
              List<BeneficiaryDTO> listSearch = new List<BeneficiaryDTO>();


              if (isDownload)
                      {
                          param.iDisplayLength = 1000000;
                      }
                      int UserId = AppUserManager.GetUserId();
                      int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;


        //var regionDistDetails = GetRegionName();
        //var regionId = db.MasterRegion.Where(x => x.Name == regionDistDetails).Select(x => x.Id).FirstOrDefault();

        //var districtId = db.MasterDistrict.Where(x => x.RegionId == regionId).Select(x => x.Id).FirstOrDefault().ToString();
        //listSearch = db.MasterBeneficiariesDetails.Where(p => p.IsActive == true && p.SelectDistrict == districtId).ToList();


        //listSearch = db.MasterBeneficiariesDetails.AsNoTracking().Where(p => p.IsActive == true).ToList();

        // Fetch total record count directly from the database
        int totalRecords = await db.MasterBeneficiariesDetails
                                   .AsNoTracking()
                                   .Where(p => p.IsActive == true && p.SelectPensionType == BeneficiariesType)
                                   .CountAsync();

        // Apply pagination and fetch data
        listSearch = await db.MasterBeneficiariesDetails
                                 .AsNoTracking()
                                 .Where(p => p.IsActive == true && p.SelectPensionType == BeneficiariesType)
                                 .OrderBy(x => x.Id) // Add an OrderBy clause to ensure consistent pagination
                                 .Skip(param.iDisplayStart)
                                 .Take(param.iDisplayLength)
                                 .Select(x => new BeneficiaryDTO
                                 {
                                   ApplicationReferenceNo = x.ApplicationReferenceNo,
                                   NameOfTheApplicant = x.NameOfTheApplicant,
                                   Gender = x.Gender,
                                   Age_InYears = x.Age_InYears,
                                   PermanentAddress = x.PermanentAddress,
                                   MobileNumber = x.MobileNumber,
                                   EMail = x.EMail ?? string.Empty,
                                   AccountNoOfTheApplicant = x.AccountNoOfTheApplicant,
                                   IFSCCode = x.IFSCCode,
                                   BankName = x.BankName,
                                   CurrentStatus = x.CurrentStatus,
                                   SelectPensionType = x.SelectPensionType,
                                   Id = x.Id
                                 })
                                 .ToListAsync();


        //listSearch = BLLMasterEmployee.GetAllData(UserId, RoleId, Tehsil, BeneficiariesType, Gender);


        if (!string.IsNullOrEmpty(param.sSearch))
                {
                    listSearch = listSearch
                        .Where(c => c.ApplicationReferenceNo.ToLower().Contains(param.sSearch.ToLower())
                            || c.NameOfTheApplicant.ToLower().Contains(param.sSearch.ToLower())
                            || c.Gender.ToLower().Contains(param.sSearch.ToLower())
                            || c.Age_InYears.ToLower().Contains(param.sSearch.ToLower())
                            || c.PermanentAddress.ToLower().Contains(param.sSearch.ToLower())
                            || c.MobileNumber.ToLower().Contains(param.sSearch.ToLower())
                            || c.EMail.ToLower().Contains(param.sSearch.ToLower())
                            || c.AccountNoOfTheApplicant.ToString().ToLower().Contains(param.sSearch.ToLower())
                            || c.IFSCCode.ToString().ToLower().Contains(param.sSearch.ToLower())
                            || c.BankName.ToString().ToLower().Contains(param.sSearch.ToLower())
                            || c.CurrentStatus.ToString().ToLower().Contains(param.sSearch.ToLower())
                            )
                        .ToList();


                }

                // Sorting through column index
                //int sortColumnIndex;
                //if (!int.TryParse(Request["iSortCol_0"], out sortColumnIndex))
                //{
                //    // Default to the first column if the column index is not valid
                //    sortColumnIndex = 0;
                //}

                //Func<MasterBeneficiariesDetails, string> orderingFunction = (c =>
                //    sortColumnIndex == 0 ? c.NameOfTheApplicant :
                //    sortColumnIndex == 1 ? c.ApplicationReferenceNo :
                //    sortColumnIndex == 2 ? c.Gender :
                //    sortColumnIndex == 3 ? c.Age_InYears.ToString() :
                //    "");

                //// Sort direction: asc or desc
                //var sortDirection = Request["sSortDir_0"];
                //if (sortDirection == "asc")
                //{
                //    listSearch = listSearch.OrderBy(orderingFunction).ToList();
                //}
                //else
                //{
                //    listSearch = listSearch.OrderByDescending(orderingFunction).ToList();
                //}





                //if(!string.IsNullOrEmpty(BeneficiariesType))
                //  {
                //          listSearch = listSearch.Where(p => p.SelectPensionType.ToLower() == BeneficiariesType.ToLower()).ToList();
                //  }

                //if (!string.IsNullOrEmpty(BeneficiariesType))
                //{
                //    if (BeneficiariesType.ToUpper() != "ALL")
                //    {
                //        listSearch = listSearch.Where(p => p.SelectPensionType.ToLower() == BeneficiariesType.ToLower()).ToList();
                //    }
                //}

                if (!string.IsNullOrEmpty(ACCOUNT_STATUS))
                {
                    try
                    {
                        List<string> dataList = new List<string>();
                        DateTime lastpydate = DateTime.Now.AddDays(-30);
                        //string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["SQLConn"].ToString();
                        string ConnectionString = ConnectionStringProvider.GetConnectionString();
                        DataSet dsDT = new DataSet();
                        string sqlcommand = @"SELECT * 
                      FROM Process_DirectDeposit_Details dd 
                      WHERE dd.[Status] = 'OK'
                      AND MONTH(dd.pay_date) = MONTH(DATEADD(MONTH, -1, DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)))";
                        using (SqlConnection con = new SqlConnection(ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand(sqlcommand, con))
                            {
                                SqlDataAdapter custAdapterDT = new SqlDataAdapter(cmd);
                                custAdapterDT.Fill(dsDT);
                            }
                        }

                        foreach (DataRow row in dsDT.Tables[0].Rows)
                        {
                            string value = row["empl_code"].ToString();
                            dataList.Add(value);
                        }
                        //var lastmonthpaylist = db.ProcessDirectDepositDetails.Where(p => p.Status == "OK" && p.PayDate == lastpydate).Select(p => p.Empl_Code).ToList();

                        var intEmpCodes = dataList.Select(code => Convert.ToInt32(code)).ToList();
                        listSearch = listSearch.Where(p => intEmpCodes.Any(x => x == p.Id)).ToList();

                        if (!string.IsNullOrEmpty(Gender))
                        {
                            listSearch = listSearch.Where(p => p.Gender.ToUpper() == Gender.ToUpper()).ToList();
                        }


                    }
                    catch (Exception ex)
                    {
                        // Log or inspect the exception details
                        Console.WriteLine("Error: " + ex.Message);
                        Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                        throw; // rethrow the exception if needed
                    }
                }
                if (isDownload)
                {
                    FileContentResult bytesdata = JandKBeneficiaryReport(listSearch);
                    return Json(bytesdata, JsonRequestBehavior.AllowGet);
                }

               // Int32 totalRecords = listSearch.Count > 0 ? Convert.ToInt32(listSearch.Select(x => x.Id).FirstOrDefault()) : 0;
              

                var result = from x in listSearch
                             select new[]
                             {
                        x.ApplicationReferenceNo + " ",//0
                        x.NameOfTheApplicant??" " + " ",//1
                        x.Gender +"", //2
                        x.Age_InYears +"", //3
                        x.PermanentAddress +"",// 4
                        x.MobileNumber,//5
                        x.EMail + "",//6
                        x.AccountNoOfTheApplicant +"", //7
                        x.IFSCCode,//8
                        x.BankName +"",//9
                        x.CurrentStatus +"", //10 
                        UrlEncryption.EncryptURL(Convert.ToString(x.Id)),//11
                        
                     };
                  return Json(new
                  {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = result
                  }, JsonRequestBehavior.AllowGet);


      }
      catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //**** Code By Himanshu Rajput ****

        [HttpGet]
        public ActionResult Login_log()
        {
            try
            {
                ViewBag.FromDate = DateTime.Now.Date.AddDays(-7).ToString("dd/MM/yyyy");
                ViewBag.ToDate = (DateTime.Now.Date).ToString("dd/MM/yyyy");

                return View();
            }
            catch (Exception ex)
            {
                // Log the exception
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
        }

        // End

        // ***** Code By Himanshu Rajput
        public JsonResult Login_logAjax(JQueryDataTableParamModel param)
        {
            try
            {

                DataSet ds = new DataSet();
                StringBuilder SQL = new StringBuilder();
                SQL.Append("SELECT  U.UserName,E.RegionName,E.LoginTime, E.LogoutTime from LoginLog E join AppUser U on E.UserId = U.Id  ");

                // Adding ORDER BY clause for descending order based on ID
                SQL.Append("ORDER BY E.Id DESC");


                //string con = WebConfigurationManager.AppSettings["SQLConn"];
                string con = ConnectionStringProvider.GetConnectionString();
                SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
                da.Fill(ds);

                List<LoginLog> LogsList = new List<LoginLog>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    LoginLog Login_logs = new LoginLog();

                    Login_logs.UserName = (dr[0] != DBNull.Value ? (dr[0]).ToString() : "");
                    Login_logs.RegionName = (dr[1] != DBNull.Value ? (dr[1]).ToString() : "");

                    if (dr[2] != DBNull.Value && dr[2].ToString().Trim() != string.Empty)
                        Login_logs.LoginTime = Convert.ToDateTime(dr[2]);

                    if (dr[3] != DBNull.Value && dr[3].ToString().Trim() != string.Empty)
                        Login_logs.LogoutTime = Convert.ToDateTime(dr[3]);

                    LogsList.Add(Login_logs);
                }

                IEnumerable<LoginLog> filtered;
                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    filtered = LogsList
                       .Where(c => c.UserName.ToString().ToLower().Contains(param.sSearch.ToLower())
                       || c.RegionName.ToString().Contains(param.sSearch.ToLower())
                       || c.LoginTime.ToString().ToLower().Contains(param.sSearch.ToLower())
                       || c.LogoutTime.ToString().ToLower().Contains(param.sSearch.ToLower())

                       );
                }
                else
                {
                    filtered = LogsList;
                    //FileContentResult bytesdata = ExceptionLogExce(filtered);
                    //return Json(bytesdata, JsonRequestBehavior.AllowGet);

                }

                //Sorting through column index
                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);


                Func<LoginLog, string> orderingFunction = (c =>
                                                           sortColumnIndex == 0 ? c.UserName + "" :
                                                           sortColumnIndex == 1 ? c.RegionName + "" :
                                                           sortColumnIndex == 2 ? c.LoginTime + "" :
                                                           sortColumnIndex == 3 ? c.LogoutTime + "" :


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
                 c.UserName + "",
                 c.RegionName +"",
                 c.LoginTime + "",
                 c.LogoutTime + "",

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
                // Log the exception
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw;
            }

        }

        //End

    }
      public class BeneficiaryDTO
      {
        public string ApplicationReferenceNo { get; set; }
        public string NameOfTheApplicant { get; set; }
        public string Gender { get; set; }
        public string Age_InYears { get; set; }
        public string PermanentAddress { get; set; }
        public string MobileNumber { get; set; }
        public string EMail { get; set; }
        public Int64 AccountNoOfTheApplicant { get; set; }
        public string IFSCCode { get; set; }
        public string BankName { get; set; }
        public string CurrentStatus { get; set; }
        public string SelectPensionType { get; set; }
        public int Id { get; set; }
      }

}