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
using System.Globalization;
using Postal;
using App.Web.Repository;
using App.Web.Filters;
namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class PaymentAndRefundController : BaseController
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;
        private AppDbContext _DbContext;

        public PaymentAndRefundController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _DbContext = DbContext;
        }

        // GET: PaymentAndRefund
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RefundAjaxHandler(App.Web.Models.JQueryDataTableParamModel param)
        {
            List<RefundIndexViewModel> List = (from RA in db.RefundApplications
                                               join C in db.MasterContributor.Where(x => x.JobStatusID == 1) on RA.PersonID equals C.Id.ToString()
                                               join CJ in db.MasterContributorJobDetails on C.PersonID equals CJ.PersonID
                                               where RA.IsActive == true
                                               select new RefundIndexViewModel
                                               {
                                                   Id = RA.Id,
                                                   EmployerName = C.Employer.EmployerName,
                                                   PersonID = RA.PersonID,
                                                   Name = C.FirstName + " " + C.MidName + " " + C.LastName,
                                                   Designation = CJ == null ? "N/A" : CJ.DesignationId == null ? "N/A" : CJ.Designation.Name,
                                                   FirstAppointmentDate = RA.FirstAppointmentDate,
                                                   RetirementOrResignationDate = RA.RetirementOrResignationDate,
                                                   JobStatus = C.Status.Name,
                                                   LengthOfServiceInMonths = RA.LengthOfServiceInMonths,
                                                   TotalEmployeeContribution = RA.TotalEmployeeContribution,
                                                   AnnualInterest = RA.AnnualInterest,
                                                   TotalEmployeeContributionWithInterest = RA.TotalEmployeeContributionWithInterest
                                               }).ToList();
            IEnumerable<RefundIndexViewModel> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.EmployerName.ToLower().Contains(param.sSearch.ToLower())
                   || c.Name.ToLower().Contains(param.sSearch.ToLower())
                   || c.PersonID.ToLower().Contains(param.sSearch.ToLower())
                   || c.FirstAppointmentDate.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.RetirementOrResignationDate.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.LengthOfServiceInMonths.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.TotalEmployeeContribution.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.AnnualInterest.ToString().ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<RefundIndexViewModel, string> orderingFunction = (c => sortColumnIndex == 0 ? c.EmployerName :
                                                                        sortColumnIndex == 1 ? c.Name :
                                                                        sortColumnIndex == 2 ? c.PersonID :
                                                                        sortColumnIndex == 3 ? c.FirstAppointmentDate + "" :
                                                                        sortColumnIndex == 4 ? c.RetirementOrResignationDate + "" :
                                                                        sortColumnIndex == 5 ? c.LengthOfServiceInMonths + "" :
                                                                        sortColumnIndex == 6 ? c.TotalEmployeeContribution + "" :
                                                                        sortColumnIndex == 7 ? c.AnnualInterest + "" :
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
                         c.PersonID,
                         c.Name,
                         c.EmployerName,
                         c.Designation,
                         String.Format("{0:MM/dd/yyyy}", c.FirstAppointmentDate),
                         String.Format("{0:MM/dd/yyyy}", c.RetirementOrResignationDate),
                         c.LengthOfServiceInMonths+"",
                         c.TotalEmployeeContribution.ToString("#,##0.00"),
                         c.AnnualInterest.ToString("#,##0.00"),
                         c.TotalEmployeeContributionWithInterest.ToString("#,##0.00"),
                         c.JobStatus,
                     c.Id + ""
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
        public ActionResult PrintApplication()
        {
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName");
            List<MasterContributor> Contributor = new List<MasterContributor>();
            ViewBag.PersonID = new SelectList(Contributor, "Id", "FirstName");
            var Types = from applicationType e in Enum.GetValues(typeof(applicationType))
                        select new { Id = e, Name = e.ToString() };
            ViewBag.ApplicationType = new SelectList(Types.OrderBy(x => x.Name), "Id", "Name");
            return View();
        }

        private DataTable dtPensionEstimate()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("LastAppointmentDate");
            dt.Columns.Add("DateOfBirth");
            dt.Columns.Add("DiscountedGratuity");
            dt.Columns.Add("ExpectedRetirementDate");
            dt.Columns.Add("FirstAppointmentDate");
            dt.Columns.Add("FullPension");
            dt.Columns.Add("FullPension1");
            dt.Columns.Add("FullPension2");
            dt.Columns.Add("Gratuity");
            dt.Columns.Add("LengthOfQualifyingService", typeof(int));
            dt.Columns.Add("LengthOfQualifyingServiceInMonthsFrom1Jan2014");
            dt.Columns.Add("LengthOfQualifyingServiceInMonthsTo31Dec2003");
            dt.Columns.Add("MaxPension");
            dt.Columns.Add("noOfMonthsWithContribution");
            dt.Columns.Add("PersonID");
            dt.Columns.Add("ReducedPension");
            dt.Columns.Add("RetirementOrResignationDate");
            dt.Columns.Add("SalaryAmount");
            dt.Columns.Add("Post");
            dt.Columns.Add("DiscountRate");
            return dt;
        }
        public ActionResult _PrintApplicationAjax(string Id, string retDate)
        {
            var PersonId = db.MasterContributor.Where(x => x.Id.ToString() == Id).FirstOrDefault();
            if (PersonId.FirstAppointmentDate == null)
            {
                return Json("jobDetails", JsonRequestBehavior.AllowGet);
            }
            else
            {
                DateTime startDate = Convert.ToDateTime(PersonId.FirstAppointmentDate);
                DateTime endDate = Convert.ToDateTime(retDate);
                double totalservicelengthinYear = endDate.Subtract(startDate).Days / (365.25);
                if ((totalservicelengthinYear >= 10 && PersonId.Employer.EmployerTypeID == (int)PensionType.Public) || (totalservicelengthinYear >= 2 && PersonId.Employer.EmployerTypeID == (int)PensionType.Police))
                {
                    int id = Convert.ToInt32(Id);
                    if (!db.MasterDiscountForGratuityMain.Where(x => x.DateFrom <= endDate && (!x.DateTo.HasValue ? (DateTime.Now > endDate ? DateTime.Now : endDate) : x.DateTo) >= endDate && x.EmployerTypeID == PersonId.Employer.EmployerTypeID && x.IsActive == true).Any())
                    {
                        return Json("discountedGratuity", JsonRequestBehavior.AllowGet);
                    }
                    PensionCalculationViewModel model = new PensionCalculationViewModel();
                    if (PersonId.Employer.EmployerTypeID == (int)PensionType.Police)
                        model = PensionAndRefundRepo.CalculatePolicePensionDetails(endDate, id, "No", "Yes");
                    else
                        model = PensionAndRefundRepo.CalculatePublicPensionDetails(endDate, id, "Yes");

                    DataTable dt = dtPensionEstimate();

                    if (model != null)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Name"] = PersonId.FirstName + " " + PersonId.MidName + " " + PersonId.LastName;
                        dr["LastAppointmentDate"] = String.Format("{0:MM/dd/yyyy}", PersonId.LastAppointmentDate);
                        dr["DateOfBirth"] = String.Format("{0:MM/dd/yyyy}", PersonId.DateOfBirth);
                        dr["DiscountedGratuity"] = model.DiscountedGratuity;
                        dr["DiscountRate"] = db.MasterDiscountForGratuityMain.Where(x => x.DateFrom <= endDate && (!x.DateTo.HasValue ? (DateTime.Now > endDate ? DateTime.Now : endDate) : x.DateTo) >= endDate && x.EmployerTypeID == PersonId.Employer.EmployerTypeID && x.IsActive == true).Select(x => x.DiscountFactorPercent).FirstOrDefault();
                        dr["ExpectedRetirementDate"] = model.ExpectedRetirementDate;
                        dr["FirstAppointmentDate"] = PersonId.FirstAppointmentDate;
                        //dr["LastAppointmentDate"] = PersonId.LastAppointmentDate;
                        dr["FullPension"] = model.FullPension;
                        dr["FullPension1"] = model.FullPension1;
                        dr["FullPension2"] = model.FullPension2;
                        dr["Gratuity"] = model.Gratuity;
                        dr["LengthOfQualifyingService"] = model.LengthOfQualifyingService == null ? 0 : model.LengthOfQualifyingService;
                        dr["LengthOfQualifyingServiceInMonthsFrom1Jan2014"] = model.LengthOfQualifyingServiceInMonthsFrom1Jan2014 == null ? 0 : model.LengthOfQualifyingServiceInMonthsFrom1Jan2014;
                        dr["LengthOfQualifyingServiceInMonthsTo31Dec2003"] = model.LengthOfQualifyingServiceInMonthsTo31Dec2003 == null ? 0 : model.LengthOfQualifyingServiceInMonthsTo31Dec2003;
                        dr["noOfMonthsWithContribution"] = model.noOfMonthsWithContribution == null ? 0 : model.noOfMonthsWithContribution;
                        dr["PersonID"] = model.PersonID;
                        dr["ReducedPension"] = model.ReducedPension;
                        dr["RetirementOrResignationDate"] = retDate;
                        dr["SalaryAmount"] = model.SalaryAmount;
                        dr["MaxPension"] = model.MaxPension;
                        var jobdetails = db.MasterContributorJobDetails.Where(x => x.PersonID == PersonId.PersonID && x.IsActive == true).FirstOrDefault();
                        if (jobdetails != null)
                        {
                            dr["Post"] = jobdetails.Designation == null ? "" : jobdetails.Designation.Name;
                        }
                        dt.Rows.Add(dr);
                        DataSet ds = new DataSet();
                        ds.Tables.Add(dt);

                        //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/Reports/DSPensionEstimate.xsd"));
                        //ds.WriteXmlSchema(writer);
                        //writer.Close();
                        if (PersonId.Employer.EmployerTypeID == (int)PensionType.Police)
                        {
                            this.HttpContext.Session["ReportName"] = "rptPolicePensionEstimate.rpt";
                        }
                        else
                        {
                            this.HttpContext.Session["ReportName"] = "rptPensionEstimate.rpt";
                        }
                        this.HttpContext.Session["rptSource"] = ds;
                        return Json("1", JsonRequestBehavior.AllowGet);
                    }
                    return Json("0", JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("DateShort", JsonRequestBehavior.AllowGet);
                }
            }

            //ViewBag.Error = "";
            //if ((retirementOrResignationDate.Date - FirstAppointmentDate).TotalDays < 3650)
            //{
            //  ViewBag.Error = "service Length should be greater than 10 years";
            //}
            //return View(model);
        }

        private DataTable dtRefundEstimate()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("FirstAppointmentDate");
            dt.Columns.Add("TotalMonth");
            dt.Columns.Add("TotalInterest");
            dt.Columns.Add("Total_Monthly_Cont");
            dt.Columns.Add("Total_Refund_Amt");
            dt.Columns.Add("RetirementOrResignationDate");
            dt.Columns.Add("Post");
            dt.Columns.Add("SubmittedBy");
            dt.Columns.Add("ApprovedBy");
            dt.Columns.Add("CertifiedforPayment");
            dt.Columns.Add("AuditedBy");
            dt.Columns.Add("Items");
            dt.Columns.Add("InterestRate");
            return dt;
        }
        public ActionResult _PrintApplicationsAjax(string Id, string retDate)
        {
            var date1 = db.MasterContributor.Where(x => x.Id.ToString() == Id).FirstOrDefault();
            if (date1.FirstAppointmentDate == null)
            {
                return Json("jobDetails", JsonRequestBehavior.AllowGet);
            }
            DateTime date2 = DateTime.Parse("01/01/2004");
            if (date1.FirstAppointmentDate < date2)
            {
                return Json("notEligible", JsonRequestBehavior.AllowGet);
            }
            string AppDate = String.Format("{0:MM/dd/yyyy}", date1.FirstAppointmentDate);
            var PersonId = db.MasterContributor.Where(x => x.Id.ToString() == Id).FirstOrDefault();
            int id = Convert.ToInt32(Id);
            DateTime startDate = Convert.ToDateTime(AppDate);
            DateTime endDate = Convert.ToDateTime(retDate);
            double totalservicelengthinYear = endDate.Subtract(startDate).Days / (365.25);
            if ((totalservicelengthinYear <= 10 && PersonId.Employer.EmployerTypeID == (int)PensionType.Public) || (totalservicelengthinYear <= 2 && PersonId.Employer.EmployerTypeID == (int)PensionType.Police))
            {
                refundViewModel tempValue = PensionAndRefundRepo.GetRefundEstimated(id, startDate, endDate, "Yes");
                if (tempValue != null)
                {
                    if (tempValue.error == "InterestRate Not defined for that period")
                    {
                        return Json("InterestRate", JsonRequestBehavior.AllowGet);
                    }
                    DataTable dt = dtRefundEstimate();
                    DataRow dr = dt.NewRow();
                    dr["Name"] = PersonId.FirstName + " " + PersonId.MidName + " " + PersonId.LastName;
                    dr["FirstAppointmentDate"] = String.Format("{0:MM/dd/yyyy}", PersonId.FirstAppointmentDate);
                    dr["TotalMonth"] = tempValue.ServiceLength;
                    dr["TotalInterest"] = tempValue.TotalInterestPerYear;
                    dr["Total_Monthly_Cont"] = tempValue.TotalContributionPerYear;
                    dr["Total_Refund_Amt"] = tempValue.TotalRefundAmountPerYear;
                    dr["RetirementOrResignationDate"] = retDate;
                    dr["InterestRate"] = tempValue.interestRate;
                    var jobdetails = db.MasterContributorJobDetails.Where(x => x.PersonID == PersonId.PersonID && x.IsActive == true).FirstOrDefault();
                    if (jobdetails != null)
                    {
                        dr["Post"] = jobdetails.Designation == null ? "" : jobdetails.Designation.Name;
                    }
                    dt.Rows.Add(dr);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    this.HttpContext.Session["ReportName"] = "rptRefundEstimate.rpt";
                    this.HttpContext.Session["rptSource"] = ds;
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (PersonId.Employer.EmployerTypeID == (int)PensionType.Public)
                {
                    return Json("public", JsonRequestBehavior.AllowGet);
                }
                return Json("police", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult AddRefundApplicationForm()
        {
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName");
            var Contributor = new List<MasterContributor>();
            ViewData["PersonID"] = new SelectList(Contributor, "Id", "FirstName");
            var jobStatus = db.MasterStatus.Where(x => x.IsActive == true && x.Id > 3).ToList();
            ViewBag.JobStatus = new SelectList(jobStatus.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true), "Id", "Name");
            int currentUser = AppUserManager.GetUserId();
            var allPermissionUser = (from level in db.ApprovalProcessLevel join users in db.ApprovalProcessAssignedUser on level.ApprovalProcessLevelId equals users.ApprovalProcessLevelId where level.ModuleId == 10 select users).FirstOrDefault();
            int userPermissiontoFirstLevel = Convert.ToInt32(allPermissionUser.ApprovalUser);
            if (currentUser != userPermissiontoFirstLevel)
            {
                ViewBag.RightForApproval = "No";
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRefundApplicationForm(RefundApplications model)
        {
            if (!db.PensionApplications.Any(x => x.PersonID == model.PersonID && x.IsActive == true))
            {
                var temp = (from a in db.ApprovalProcessLevel join b in db.ApprovalProcessAssignedUser on a.ApprovalProcessLevelId equals b.ApprovalProcessLevelId where a.ModuleId == 10 select b.ApprovalProcessId).ToList();
                int minId = temp.FirstOrDefault();
                int maxId = temp.LastOrDefault();
                double totalservicelengthinYear = model.RetirementOrResignationDate.Subtract(model.FirstAppointmentDate).Days / 365.25;
                if (totalservicelengthinYear < 10)
                {
                    if ((!db.RefundApplications.Where(x => x.PersonID == model.PersonID && x.EmployerID == model.EmployerID && x.IsActive == true).Any()) || ((db.RefundApplications.Where(x => x.PersonID == model.PersonID && x.EmployerID == model.EmployerID && x.IsActive == true).Any()) && db.ApplicationApprovalStatus.Where(x => x.ApplicationId == model.Id && x.ApprovalStatus == "R" && x.ApprovalProcessId <= maxId && x.ApprovalProcessId >= minId).Any()))
                    {
                        //var a = Request["RetirementOrResignationDate"];
                        if (ModelState.IsValid)
                        {
                            bool Approval = Request["InitialApproved"] != "" ? true : false;
                            string Name = Request["Notes"];
                            model.Name = null;
                            model.SubmittedBy = AppUserManager.GetUserId();
                            model.IsActive = true;
                            var employerTypeId = db.MasterEmployer.Where(x => x.Id == model.EmployerID).Select(x => x.EmployerTypeID).FirstOrDefault();
                            if (db.MasterInterestRate.Where(x => x.DateFrom <= model.RetirementOrResignationDate && (x.DateTo == null ? model.RetirementOrResignationDate : x.DateTo) >= model.RetirementOrResignationDate && x.EmployerTypeID == employerTypeId && x.IsActive == true).Any())
                            {
                                var interestRateDetails = db.MasterInterestRate.Where(x => x.DateFrom <= model.RetirementOrResignationDate && (x.DateTo == null ? model.RetirementOrResignationDate : x.DateTo) >= model.RetirementOrResignationDate && x.EmployerTypeID == employerTypeId && x.IsActive == true).Select(x => new { x.Id, x.InterestRate }).FirstOrDefault();
                                model.InterestRateId = interestRateDetails.Id;
                                model.InterestRate = interestRateDetails.InterestRate;
                            }
                            db.RefundApplications.Add(model);
                            int result = db.SaveChanges();
                            if (result > 0)
                            {
                                if (Approval)
                                {
                                    ApplicationApprovalStatus mstatus = new ApplicationApprovalStatus();
                                    mstatus.ApplicationId = model.Id;
                                    mstatus.ApprovalStatus = "A";
                                    mstatus.ApprovedDate = DateTime.Now;
                                    mstatus.ApprovalProcessId = 4;
                                    mstatus.Notes = Name;
                                    db.Entry(mstatus).State = EntityState.Added;
                                    db.SaveChanges();

                                    var firstapprovaluser = (from a in db.ApprovalProcessLevel join b in db.ApprovalProcessAssignedUser on a.ApprovalProcessLevelId equals b.ApprovalProcessLevelId where a.ModuleId == 10 orderby b.ApprovalProcessLevelId select b.ApprovalUser).Skip(1).FirstOrDefault();
                                    var approvaluser = db.UserProfiles.Where(x => x.UserId == firstapprovaluser).Select(x => new { Name = x.FirstName + " " + x.MiddleName + " " + x.LastName, x.Email }).FirstOrDefault();
                                    PensionAndRefundEmailRepo.sendMailProcedureRefund(approvaluser.Email, model.Id, approvaluser.Name, "Request For Refund Application Approval...");
                                }
                                else
                                {
                                    var firstapprovaluser = (from a in db.ApprovalProcessLevel join b in db.ApprovalProcessAssignedUser on a.ApprovalProcessLevelId equals b.ApprovalProcessLevelId where a.ModuleId == 10 orderby b.ApprovalProcessLevelId select b.ApprovalUser).FirstOrDefault();
                                    var approvaluser = db.UserProfiles.Where(x => x.UserId == firstapprovaluser).Select(x => new { Name = x.FirstName + " " + x.MiddleName + " " + x.LastName, x.Email }).FirstOrDefault();
                                    PensionAndRefundEmailRepo.sendMailProcedureRefund(approvaluser.Email, model.Id, approvaluser.Name, "Request For Refund Application Approval...");
                                }
                                var applicant = db.MasterContributor.Where(x => x.Id.ToString() == model.PersonID).Select(x => new { Name = x.FirstName + " " + x.MidName + " " + x.LastName, x.Email }).FirstOrDefault();
                                PensionAndRefundEmailRepo.sendMailProcedureRefund(applicant.Email, model.Id, applicant.Name, "NewApplication");

                            }
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["error"] = "Request Already Submitted for this Contributor";
                    }
                }
                else
                {
                    TempData["error"] = "Service Length Should be less than 10 Years";
                }
            }
            else
            {
                TempData["error"] = "Request For Pension is Already Submitted for this Contributor";
            }
            ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true), "Id", "Name", model.DesignationId);
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", model.EmployerID);
            var personId = db.MasterContributor.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName }).ToList();
            ViewBag.PersonID = new SelectList(personId.OrderBy(x => x.Name), "Id", "Name", model.PersonID);
            ViewBag.JobStatus = new SelectList(db.MasterStatus.Where(x => x.IsActive == true && x.Id > 3).OrderBy(x => x.Name), "Id", "Name", model.JobStatus);
            return View(model);
        }
        public JsonResult GetContributorAjax(string EmpId)
        {
            var empId = Convert.ToInt32(EmpId);
            var result = db.MasterContributor.Where(x => x.EmployerID == empId && x.IsActive == true && x.JobStatusID == 1).OrderBy(x => x.FirstName).Select(x => new { Value = x.Id, Text = x.FirstName + " " + x.MidName + " " + x.LastName + " ( " + x.PersonID + " )" });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult checkJobDetailsAjax(string EmpId)
        {
            string date = "";
            var empId = Convert.ToInt32(EmpId);
            var result = db.MasterContributor.Where(x => x.Id == empId && x.IsActive == true).FirstOrDefault();
            if (result.ExpectedRetirementDate == null)
            {
                date = "Job Details Not Available For this Contributor";
            }
            else
            {
                date = String.Format("{0:MM/dd/yyyy}", result.ExpectedRetirementDate);
            }
            return Json(date, JsonRequestBehavior.AllowGet);
        }

        public JsonResult checkEmployerTypeAjax(string EmpId)
        {
            var empId = Convert.ToInt32(EmpId);
            if (db.MasterEmployer.Where(x => x.Id == empId).Select(x => x.EmployerTypeID).FirstOrDefault() == 1)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppointmentDateAjax(int Id)
        {
            var date2 = DateTime.Parse("01/01/2004");
            var details = (from contributor in db.MasterContributor.Where(x => x.Id == Id && x.IsActive) join jobdetails in db.MasterContributorJobDetails.Where(x => x.IsActive) on contributor.PersonID equals jobdetails.PersonID select new { jobdetails, contributor }).FirstOrDefault();
            if (details != null)
            {
                if (details.jobdetails.JoiningDate != null)
                {
                    if (date2 <= details.jobdetails.JoiningDate)
                    {
                        var AppDate = String.Format("{0:MM/dd/yyyy}", details.jobdetails.JoiningDate);
                        var expDate = String.Format("{0:MM/dd/yyyy}", details.contributor.ExpectedRetirementDate);
                        var result = from a in db.MasterContributor.Where(x => x.Id == Id)
                                     join b in db.MasterContributorJobDetails on a.PersonID equals b.PersonID
                                     //join c in db.MasterDesignation on b.DesignationId equals c.Id
                                     where b.IsActive == true
                                     select new { DateofApp = AppDate, DesignnationId = b == null ? 0 : b.DesignationId == null ? 0 : b.DesignationId, expDate };

                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Not Eligible For Refund,joining date must be after 2004...", JsonRequestBehavior.AllowGet);
                    }
                }
                else if (details.jobdetails.HireDate != null)
                {
                    if (date2 <= details.jobdetails.HireDate)
                    {
                        var AppDate = String.Format("{0:MM/dd/yyyy}", details.jobdetails.HireDate);
                        var expDate = String.Format("{0:MM/dd/yyyy}", details.contributor.ExpectedRetirementDate);
                        var result = from a in db.MasterContributor.Where(x => x.Id == Id)
                                     join b in db.MasterContributorJobDetails on a.PersonID equals b.PersonID
                                     //join c in db.MasterDesignation on b.DesignationId equals c.Id
                                     where b.IsActive == true
                                     select new { DateofApp = AppDate, DesignnationId = b == null ? 0 : b.DesignationId == null ? 0 : b.DesignationId, expDate };

                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Not Eligible For Refund,hire date must be after 2004...", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("Check Hire Date in Job Details...", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("Please Check Job Details,No Details Found...", JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetRefundNewAjax(string PersonID, string StartDate, string EndDate)
        {
            int personId = Convert.ToInt32(PersonID);
            DateTime startDate = Convert.ToDateTime(StartDate);
            DateTime endDate = Convert.ToDateTime(EndDate);
            var result = PensionAndRefundRepo.GetRefundEstimated(personId, startDate, endDate, "No");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RefundDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefundApplications refundApplications = db.RefundApplications.Find(id);
            if (refundApplications == null)
            {
                return HttpNotFound();
            }
            var levels = (from level in db.ApprovalProcessLevel join users in db.ApprovalProcessAssignedUser on level.ApprovalProcessLevelId equals users.ApprovalProcessLevelId where level.ModuleId == 10 select users).ToList();
            int MaxLevel = levels.LastOrDefault().ApprovalProcessId;
            int MinLevel = levels.FirstOrDefault().ApprovalProcessId;
            if (db.ApplicationApprovalStatus.Where(x => x.ApplicationId == id && x.ApprovalProcessId >= MinLevel && x.ApprovalProcessId <= MaxLevel).Any())
            {
                ViewBag.EditAllowed = "No";
            }
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", refundApplications.EmployerID);
            var PersonId = db.MasterContributor.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName }).OrderBy(x => x.Name).ToList();
            ViewBag.PersonID = new SelectList(PersonId, "Id", "Name", refundApplications.PersonID);

            var jobStatus = db.MasterStatus.Where(x => x.IsActive == true && x.Id > 3).ToList();
            ViewBag.JobStatus = new SelectList(jobStatus.OrderBy(x => x.Name), "Id", "Name", refundApplications.JobStatus);

            ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true), "Id", "Name", refundApplications.DesignationId);

            var result = (from a in db.ApprovalProcessAssignedUser join b in db.ApprovalProcessLevel on a.ApprovalProcessLevelId equals b.ApprovalProcessLevelId where b.ModuleId == 16 orderby a.ApprovalProcessId descending select new { min = a.ApprovalProcessId }).FirstOrDefault();

            //var levels = (from level in db.ApprovalProcessLevel join users in db.ApprovalProcessAssignedUser on level.ApprovalProcessLevelId equals users.ApprovalProcessLevelId where level.ModuleId == 10 select users).ToList();
            //int MaxLevel = levels.LastOrDefault().ApprovalProcessId;
            //int MinLevel = levels.FirstOrDefault().ApprovalProcessId;
            //if (db.ApplicationApprovalStatus.Where(x => x.ApplicationId == id && x.ApprovalProcessId >= MinLevel && x.ApprovalProcessId <= MaxLevel).Any())
            //{
            //  ViewBag.UnderApproval = "Yes";
            //}
            //else
            //{
            //  ViewBag.UnderApproval = "No";
            //}
            //ViewBag.Id = id;
            //var PersonId = db.MasterContributor.Where(x => x.Id.ToString() == refundApplications.PersonID).Select(x => new { x.FirstName, x.MidName, x.LastName }).FirstOrDefault();
            //ViewBag.Name = PersonId.FirstName + " " + PersonId.MidName + " " + PersonId.LastName;
            return View(refundApplications);
        }
        public ActionResult RefundRejectedDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefundApplications refundApplications = db.RefundApplications.Find(id);
            if (refundApplications == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", refundApplications.EmployerID);
            var PersonId = db.MasterContributor.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName }).OrderBy(x => x.Name).ToList();
            ViewBag.PersonID = new SelectList(PersonId, "Id", "Name", refundApplications.PersonID);

            var jobStatus = db.MasterStatus.Where(x => x.IsActive == true && x.Id > 3).ToList();
            ViewBag.JobStatus = new SelectList(jobStatus.OrderBy(x => x.Name), "Id", "Name", refundApplications.JobStatus);

            ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true), "Id", "Name", refundApplications.DesignationId);

            return View(refundApplications);
        }
        public JsonResult _RefundDetailsAjax(int? Id)
        {
            RefundApplications refundApplications = db.RefundApplications.Find(Id);
            List<ReportText> text = db.ReportText.Where(x => x.ModuleId == 3).ToList();
            if (refundApplications != null)
            {
                var PersonId = db.MasterContributor.Where(x => x.Id.ToString() == refundApplications.PersonID).Select(x => new { x.FirstName, x.MidName, x.LastName }).FirstOrDefault();
                DataTable dt = dtRefundEstimate();
                DataRow dr = dt.NewRow();
                dr["Name"] = PersonId.FirstName + " " + PersonId.MidName + " " + PersonId.LastName;
                dr["FirstAppointmentDate"] = String.Format("{0:MM/dd/yyyy}", refundApplications.FirstAppointmentDate);
                dr["TotalMonth"] = refundApplications.LengthOfServiceInMonths;
                dr["TotalInterest"] = refundApplications.AnnualInterest;
                dr["Total_Monthly_Cont"] = refundApplications.TotalEmployeeContribution;
                dr["Total_Refund_Amt"] = refundApplications.TotalEmployeeContributionWithInterest;
                dr["RetirementOrResignationDate"] = refundApplications.RetirementOrResignationDate;
                dr["Post"] = refundApplications.Designation.Name;
                dr["SubmittedBy"] = text[0].Signature;
                dr["ApprovedBy"] = text[1].Signature;
                dr["Items"] = text[2].Signature;
                dr["CertifiedforPayment"] = text[3].Signature;
                dr["AuditedBy"] = text[4].Signature;
                dr["InterestRate"] = refundApplications.InterestRate;

                dt.Rows.Add(dr);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                this.HttpContext.Session["ReportName"] = "rptRefundApplication.rpt";
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private DateTime MonthNameToMonth(string MonthName, int year)
        {

            int month = DateTime.ParseExact(MonthName, "MMMM", CultureInfo.CurrentCulture).Month;
            DateTime dt = new DateTime(year, month, 1);
            return dt;
        }

        //public JsonResult GetRefundNew(int? PersonID, string StartDate, string EndDate)
        //{
        //  var tempCheckPoliceForce = db.MasterContributor.Where(x => x.Id == PersonID).Select(x => x.Employer.EmployerTypeID).FirstOrDefault();

        //  DateTime startDate = Convert.ToDateTime(StartDate);
        //  DateTime endDate = Convert.ToDateTime(EndDate);
        //  int totalservicelengthinMonth = Convert.ToInt32(endDate.Subtract(startDate).Days / (365.25 / 12));
        //  int totalservicelengthinYear = totalservicelengthinMonth / 12;
        //  //if ((endDate - startDate).TotalDays < 3650)
        //  if ((totalservicelengthinYear < 10 && tempCheckPoliceForce == (int)PensionType.Public) || (totalservicelengthinYear < 2 && tempCheckPoliceForce == (int)PensionType.Police))
        //  {
        //    double interestRatetemp = (db.MasterInterestRate.Any() ? (double)db.MasterInterestRate.FirstOrDefault().InterestRate : 0) / 100;
        //    double carriedfwd = 0.0;
        //    int ServiceLength = 0;
        //    double TotalContributionPerYear = 0.0;
        //    double TotalInterestPerYear = 0.0;
        //    double TotalRefundAmountPerYear = 0.0;
        //    int highestMonth = 0;

        //    ContributonSheetDetailsFinalise finalizeSheet = new ContributonSheetDetailsFinalise();
        //    List<ContributonSheetDetailsFinalise> finalizeSheetList = new List<ContributonSheetDetailsFinalise>();

        //    using (AppDbContext dbContext = new AppDbContext())
        //    {
        //      List<ContributonSheetDetailsFinalise> _ContributonSheetDetailsFinalise = _DbContext.ContributonSheetDetailsFinalise.Where(x => x.ContributorID == (PersonID == null ? 0 : PersonID)).ToList();
        //      var allYearList = from a in _ContributonSheetDetailsFinalise
        //                        join b in _DbContext.ContributonSheetHeaderFinalise on a.ContributonSheetHeaderFinaliseID equals b.Id
        //                        where a.ContributorID == (PersonID == null ? 0 : PersonID) && a.IsActive == true && b.IsActive == true
        //                        select new
        //                        {
        //                          b.Id,
        //                          b.ContributonSheetHeaderId,
        //                          a.EmployerContribution,
        //                          b.Month,
        //                          b.Year,
        //                          a.ContributorContribution,
        //                          a.EntryDate,
        //                        };

        //      foreach (var item in allYearList)
        //      {
        //        finalizeSheet = new ContributonSheetDetailsFinalise();
        //        int Month = DateTime.ParseExact(item.Month, "MMMM", CultureInfo.CurrentCulture).Month;
        //        DateTime date = Convert.ToDateTime(Month + "/01/" + item.Year);
        //        finalizeSheet.EntryDate = date;
        //        //finalizeSheet.ContributonSheetHeaderFinaliseID = item.ContributonSheetHeaderId;//This is commented by Neeraj on date 21/08/2015
        //        finalizeSheet.ContributonSheetHeaderFinaliseID = item.Id;
        //        //finalizeSheet.EntryDate = item.EntryDate; //MonthNameToMonth(item.Month, item.Year);
        //        finalizeSheet.ContributorContribution = item.ContributorContribution;

        //        finalizeSheetList.Add(finalizeSheet);
        //      }
        //    }

        //    //var newfinalizeSheetList = finalizeSheetList.OrderBy(x => x.EntryDate).Take(finalizeSheetList.Count - 1);
        //    var newfinalizeSheetList = finalizeSheetList.Where(x => x.EntryDate >= startDate && x.EntryDate <= endDate);
        //    //var newfinalizeSheetListfinal = newfinalizeSheetList.OrderBy(x => x.EntryDate).Take((newfinalizeSheetList.Count() - 1));
        //    var newfinalizeSheetListfinal = newfinalizeSheetList.OrderBy(x => x.EntryDate);

        //    for (int i = startDate.Year; i <= endDate.Year; i++)
        //    {
        //      //List Per Year
        //      var perYearList = (from perYear in newfinalizeSheetList
        //                         where Convert.ToDateTime(perYear.EntryDate).Year == i
        //                         orderby Convert.ToDateTime(perYear.EntryDate).Month
        //                         select perYear).ToList();

        //      if (perYearList.Any())
        //      {
        //        //Find Highest Month
        //        //var highestMonth = (from h in perYearList
        //        //										select Convert.ToDateTime(h.EntryDate).Month).Max();


        //        //Action Date Contribution
        //        if (i == endDate.Year)
        //        {
        //          highestMonth = endDate.Month - 1;
        //        }
        //        else
        //        {
        //          highestMonth = 12;
        //        }
        //        //Find Lowest Month
        //        var lowestMonth = (from l in perYearList
        //                           select Convert.ToDateTime(l.EntryDate).Month).Min();

        //        if (i == startDate.Year)
        //        {
        //          //Carried Fwd 0
        //          carriedfwd = 0.0;
        //        }
        //        else
        //        {
        //          carriedfwd = 0.0;
        //          carriedfwd = (TotalRefundAmountPerYear) * (interestRatetemp) * ((double)(highestMonth) / 12);
        //          TotalRefundAmountPerYear = TotalRefundAmountPerYear + carriedfwd;
        //          TotalInterestPerYear = TotalInterestPerYear + carriedfwd;
        //        }

        //        //Loop on Month 

        //        double InterestMonthly = 0.0;
        //        double RefundMonthly = 0.0;
        //        for (int month = lowestMonth; month <= highestMonth; month++)
        //        {
        //          //Get Monthly Contribution



        //          var ContributorContribution = from x in perYearList
        //                                        where x.EntryDate.Year == i && x.EntryDate.Month == month
        //                                        select new { x.ContributorContribution };



        //          if (ContributorContribution.Any())
        //          {
        //            decimal sum = ContributorContribution.Select(t => t.ContributorContribution).Sum();
        //            //Monthly Calculation
        //            //ServiceLength++;
        //            //InterestMonthly = (Convert.ToDouble(ContributorContribution.First().ContributorContribution)) * (interestRatetemp) * ((double)(highestMonth - month) / 12);
        //            //RefundMonthly = Convert.ToDouble(ContributorContribution.First().ContributorContribution) + InterestMonthly;
        //            ServiceLength++;
        //            InterestMonthly = (Convert.ToDouble(sum)) * (interestRatetemp) * ((double)(highestMonth - month) / 12);
        //            RefundMonthly = Convert.ToDouble(sum) + InterestMonthly;


        //            //Add Monthly Calculation
        //            //TotalContributionPerYear = TotalContributionPerYear + Convert.ToDouble(ContributorContribution.First().ContributorContribution);
        //            //TotalInterestPerYear = TotalInterestPerYear + InterestMonthly;
        //            //TotalRefundAmountPerYear = TotalRefundAmountPerYear + RefundMonthly;
        //            TotalContributionPerYear = TotalContributionPerYear + Convert.ToDouble(sum);
        //            TotalInterestPerYear = TotalInterestPerYear + InterestMonthly;
        //            TotalRefundAmountPerYear = TotalRefundAmountPerYear + RefundMonthly;
        //          }
        //        }
        //      }
        //      else
        //      {
        //        if (i != endDate.Year)
        //        {
        //          if (i != startDate.Year)
        //          {
        //            carriedfwd = 0.0;
        //            carriedfwd = (TotalRefundAmountPerYear) * (interestRatetemp);
        //            TotalRefundAmountPerYear = TotalRefundAmountPerYear + carriedfwd;
        //            TotalInterestPerYear = TotalInterestPerYear + carriedfwd;
        //          }
        //        }
        //        else
        //        {
        //          highestMonth = endDate.Month - 1;
        //          carriedfwd = 0.0;
        //          carriedfwd = (TotalRefundAmountPerYear) * (interestRatetemp) * ((double)(highestMonth) / 12);
        //          TotalRefundAmountPerYear = TotalRefundAmountPerYear + carriedfwd;
        //          TotalInterestPerYear = TotalInterestPerYear + carriedfwd;
        //        }
        //      }


        //    }

        //    var getPension = new { Total_Month = ServiceLength, Total_Interest = Math.Round(TotalInterestPerYear, 2), Total_Monthly_Cont = Math.Round(TotalContributionPerYear, 2), Total_Refund_Amt = Math.Round(TotalRefundAmountPerYear, 2) };
        //    return Json(getPension, JsonRequestBehavior.AllowGet);
        //  }
        //  else
        //  {
        //    if (tempCheckPoliceForce == (int)PensionType.Public)
        //    {
        //      return Json("public", JsonRequestBehavior.AllowGet);
        //    }
        //    return Json("police", JsonRequestBehavior.AllowGet);
        //  }

        //}

        private List<RefundApplications> validateRefundApplication()
        {
            int user = AppUserManager.GetUserId();
            List<RefundApplications> model;
            List<RefundApplications> temp = new List<RefundApplications>();
            var levels = (from level in db.ApprovalProcessLevel join users in db.ApprovalProcessAssignedUser on level.ApprovalProcessLevelId equals users.ApprovalProcessLevelId where level.ModuleId == 10 select users).ToList();
            if (levels.Where(x => x.ApprovalUser == user).Any())
            {
                model = db.RefundApplications.ToList();
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
        public ActionResult RefundApplicationApprovalIndex()
        {
            List<RefundApplications> list = validateRefundApplication();
            return View(list);
        }
        public ActionResult RefundApplicationApprovalDetails(int? id)
        {
            List<RefundApplications> list = validateRefundApplication();
            if (list.Count > 0)
                id = list.Any(x => x.Id == id) ? id : null;
            else
                id = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefundApplications refundApplications = db.RefundApplications.Find(id);
            if (refundApplications == null)
            {
                return HttpNotFound();
            }
            var Level = (from a in db.ApprovalProcessAssignedUser join b in db.ApprovalProcessLevel on a.ApprovalProcessLevelId equals b.ApprovalProcessLevelId where b.ModuleId == 10 select a.ApprovalProcessId).ToList();
            int maxLevel = Level.LastOrDefault();
            int approved = (from a in db.ApplicationApprovalStatus where a.ApplicationId == refundApplications.Id && a.ApprovalProcessId > 4 orderby a.ApprovalProcessId descending select a.ApprovalProcessId).FirstOrDefault();
            if (approved == (maxLevel - 1))
            {
                ViewBag.Max = maxLevel;
            }
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", refundApplications.EmployerID);
            var personId = db.MasterContributor.Where(x => x.IsActive == true).Select(x => new { PersonID = x.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName }).ToList();
            ViewBag.PersonID = new SelectList(personId.OrderBy(x => x.Name), "PersonID", "Name", refundApplications.PersonID);
            ViewBag.DesignationId = new SelectList(db.MasterDesignation.OrderBy(x => x.Name), "Id", "Name", refundApplications.DesignationId);
            ViewBag.JobStatus = new SelectList(db.MasterStatus.OrderBy(x => x.Name), "Id", "Name", refundApplications.JobStatus);
            return View(refundApplications);
        }

        public JsonResult AcceptedAjax(string[] Data)
        {
            var tempValue = Data[0].Split(',');
            string Result = tempValue[0];
            string Notes = tempValue[2];
            int Id = Convert.ToInt32(tempValue[1]);
            int result = 0;
            var peakstatus = (from a in db.ApprovalProcessAssignedUser join b in db.ApprovalProcessLevel on a.ApprovalProcessLevelId equals b.ApprovalProcessLevelId where b.ModuleId == 10 select a.ApprovalProcessId).ToList();
            int minLevel = peakstatus.FirstOrDefault();
            int maxLevel = peakstatus.LastOrDefault();
            var status = (from a in db.ApplicationApprovalStatus where a.ApplicationId == Id && a.ApprovalProcessId >= minLevel orderby a.ApprovalProcessId descending select a == null ? minLevel : a.ApprovalProcessId).FirstOrDefault();
            if (status == 0)
            {
                status = (minLevel - 1);
            }
            var rApp = (from a in db.RefundApplications join b in db.MasterContributor on a.PersonID equals b.Id.ToString() where a.Id == Id select new { refund = a, b.FirstName, b.MidName, b.LastName, b.Email }).FirstOrDefault();
            var pstatus = db.ApprovalProcessAssignedUser.Where(x => x.ApprovalProcessId > status).Select(x => x.ApprovalProcessId).FirstOrDefault();
            if (pstatus == maxLevel && Result == "A")
            {

                //var rApp = db.RefundApplications.Where(x => x.Id == Id).FirstOrDefault();
                //var Name = db.MasterContributor.Where(x => x.Id.ToString() == rApp.PersonID).FirstOrDefault();
                decimal refundAmt = Convert.ToDecimal(tempValue[6]);
                string paidStatus = tempValue[3].ToString();
                string paymentmethod = tempValue[4].ToString();
                string check_no = tempValue[5].ToString();
                RefundPaidDetails refundApp = new RefundPaidDetails();
                refundApp.RefundApplicationID = rApp.refund.Id;
                refundApp.PersonID = rApp.refund.PersonID;
                refundApp.FirstName = rApp.FirstName;
                refundApp.LastName = rApp.LastName;
                refundApp.MidName = rApp.MidName;
                refundApp.RefundAmt = refundAmt;
                refundApp.Notes = Notes;
                refundApp.Isactive = true;
                refundApp.PaidStatus = paidStatus;
                refundApp.Paymentmethod = paymentmethod;
                refundApp.Check_no = check_no;
                refundApp.ModifiedBy = AppUserManager.GetUserId();
                refundApp.ModifiedOn = DateTime.Now;
                refundApp.CreatedBy = AppUserManager.GetUserId();
                refundApp.CreatedOn = DateTime.Now;
                refundApp.CreatedmachineInfo = Request.UserHostName;
                db.Entry(refundApp).State = EntityState.Added;
                result = db.SaveChanges();

                MasterContributor contributor = db.MasterContributor.Where(x => x.Id.ToString() == refundApp.PersonID).FirstOrDefault();
                if (contributor != null)
                {
                    contributor.JobStatusID = rApp.refund.JobStatus;
                    contributor.RetirementOrResignationDate = rApp.refund.RetirementOrResignationDate;
                    db.Entry(contributor).State = EntityState.Modified;
                    result = db.SaveChanges();
                }
                RefundApplications refundModels = db.RefundApplications.Where(x => x.Id == rApp.refund.Id).FirstOrDefault();
                refundModels.IsActive = false;
                db.Entry(refundModels).State = EntityState.Modified;
                result = db.SaveChanges();
                string Name = rApp.FirstName + " " + rApp.MidName + " " + rApp.LastName;
                PensionAndRefundEmailRepo.sendMailProcedureRefund(rApp.Email, Id, Name, "ApplicationApproved");

            }
            ApplicationApprovalStatus model = new ApplicationApprovalStatus();
            model.ApprovalProcessId = pstatus;
            model.ApplicationId = Id;
            model.ApprovalStatus = Result;
            model.Notes = Notes;
            model.ApprovedDate = DateTime.Now;
            db.Entry(model).State = EntityState.Added;
            result = db.SaveChanges();

            var usersList = (from users in db.ApprovalProcessAssignedUser
                             join level in db.ApprovalProcessLevel.Where(x => x.ModuleId == 10) on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId
                             join statuss in db.ApplicationApprovalStatus.Where(x => x.ApplicationId == Id) on users.ApprovalProcessId equals statuss == null ? 0 : statuss.ApprovalProcessId into gj
                             from subpet in gj.DefaultIfEmpty()
                             select new { users.ApprovalUser, level = users.ApprovalProcessId, ApprovalProcessId = (subpet == null ? 0 : subpet.ApprovalProcessId) }).ToList(); ;

            if (result > 0 && pstatus != maxLevel && Result == "A")
            {
                var userId = usersList.Where(x => x.ApprovalProcessId == 0).Select(x => x.ApprovalUser).FirstOrDefault();
                var approvaluser = db.UserProfiles.Where(x => x.UserId == userId).Select(x => new { Name = x.FirstName + " " + x.MiddleName + " " + x.LastName, x.Email }).FirstOrDefault();
                PensionAndRefundEmailRepo.sendMailProcedureRefund(approvaluser.Email, Id, approvaluser.Name, "Request For Refund Application Approval...");
                TempData["success"] = "Application Approved Successfully";
            }
            else if (result > 0 && Result == "R")
            {
                RefundApplications refundModel = db.RefundApplications.Where(x => x.Id == rApp.refund.Id).FirstOrDefault();
                refundModel.IsActive = false;
                db.Entry(refundModel).State = EntityState.Modified;
                result = db.SaveChanges();

                var allusers = usersList.Where(x => x.ApprovalProcessId != 0).Select(x => x.ApprovalUser).ToList();
                if (allusers.Count > 0)
                {
                    foreach (var item in allusers)
                    {
                        var approvaluser = db.UserProfiles.Where(x => x.UserId == item).Select(x => new { Name = x.FirstName + " " + x.MiddleName + " " + x.LastName, x.Email }).FirstOrDefault();
                        PensionAndRefundEmailRepo.sendMailProcedureRefund(approvaluser.Email, Id, approvaluser.Name, "Request For Refund Application Rejected...");
                    }
                }

                string Name = rApp.FirstName + " " + rApp.MidName + " " + rApp.LastName;
                PensionAndRefundEmailRepo.sendMailProcedureRefund(rApp.Email, Id, Name, "ApplicationRejected");
                TempData["success"] = "Application Rejected Successfully";
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult _RejectedIndexAjax()
        {

            return View();
        }
        public ActionResult RejectedAjax(App.Web.Models.JQueryDataTableParamModel param)
        {
            var result = (from users in db.ApprovalProcessAssignedUser join level in db.ApprovalProcessLevel on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId where level.ModuleId == 10 select new { users.ApprovalProcessId, users.ApprovalUser }).ToList();
            int minId = result.FirstOrDefault().ApprovalProcessId;
            int maxId = result.LastOrDefault().ApprovalProcessId;
            int user = AppUserManager.GetUserId();
            List<App.Data.ViewModels.RRejectedViewModel> List = new List<App.Data.ViewModels.RRejectedViewModel>();
            if (result.Where(x => x.ApprovalUser.ToString() == user.ToString()).Any())
            {
                List = (from users in db.ApprovalProcessAssignedUser
                        join level in db.ApprovalProcessLevel on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId
                        join status in db.ApplicationApprovalStatus on users.ApprovalProcessId equals status == null ? 0 : status.ApprovalProcessId
                        join application in db.RefundApplications on status == null ? 0 : status.ApplicationId equals application.Id
                        where level.ModuleId == 10 && status.ApprovalProcessId >= minId && status.ApprovalProcessId <= maxId && status.ApprovalStatus == "R"
                        select new App.Data.ViewModels.RRejectedViewModel { ApplicationId = status.ApplicationId, AppliedDate = application.CreatedOn, ApprovalStatus = status.ApprovalStatus, ApprovalUser = users.ApprovalUser.ToString(), RejectedDate = status.ApprovedDate, Notes = status.Notes, approvalProcessId = status.ApprovalProcessId }).ToList();

            }
            IEnumerable<App.Data.ViewModels.RRejectedViewModel> filtered;


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
        public ActionResult _ApprovedIndexAjax()
        {

            return View();
        }
        public ActionResult ApprovedAjax(App.Web.Models.JQueryDataTableParamModel param)
        {
            var result = (from users in db.ApprovalProcessAssignedUser join level in db.ApprovalProcessLevel on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId where level.ModuleId == 10 select new { users.ApprovalProcessId, users.ApprovalUser }).ToList();
            int visiblePrint = result.Skip(1).FirstOrDefault().ApprovalProcessId;
            int minId = result.FirstOrDefault().ApprovalProcessId;
            int maxId = result.LastOrDefault().ApprovalProcessId;
            int user = AppUserManager.GetUserId();
            List<App.Data.ViewModels.RRejectedViewModel> List = new List<App.Data.ViewModels.RRejectedViewModel>();

            if (result.Where(x => x.ApprovalUser.ToString() == user.ToString()).Any())
            {
                List = (from users in db.ApprovalProcessAssignedUser
                        join level in db.ApprovalProcessLevel on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId
                        join status in db.ApplicationApprovalStatus on users.ApprovalProcessId equals status == null ? 0 : status.ApprovalProcessId
                        join application in db.RefundApplications on status == null ? 0 : status.ApplicationId equals application.Id
                        where level.ModuleId == 10 && status.ApprovalProcessId >= minId && status.ApprovalProcessId <= maxId
                        orderby application.Id descending
                        select new App.Data.ViewModels.RRejectedViewModel { ApplicationId = status.ApplicationId, AppliedDate = application.CreatedOn, ApprovalStatus = status.ApprovalStatus, ApprovalUser = users.ApprovalUser.ToString(), RejectedDate = status.ApprovedDate, Notes = status.Notes, approvalProcessId = status.ApprovalProcessId }).ToList();
                //Below Group Added by Neeraj on dated 12Jan2017 due to issue raiased by JKPS beacuse print button is not visible as per condiiton
                List = List.GroupBy(g => new { g.ApplicationId, g.approvalProcessId, g.ApprovalStatus }).Where(p => p.Max(m => m.approvalProcessId) >= maxId && p.Max(m => m.approvalProcessId) <= maxId).Select(g => g.Last()).ToList();
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
            IEnumerable<App.Data.ViewModels.RRejectedViewModel> filtered;


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
        public ActionResult _PrintRefundApplication(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RefundApplications refundApplications = db.RefundApplications.Find(id);
            if (refundApplications == null)
            {
                return HttpNotFound();
            }
            //var result = (from users in db.ApprovalProcessAssignedUser
            //              join level in db.ApprovalProcessLevel.Where(x => x.ModuleId == 10) on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId
            //              join status in db.ApplicationApprovalStatus.Where(x => x.ApplicationId == id) on users.ApprovalProcessId equals status == null ? 0 : status.ApprovalProcessId into gj
            //              from subpet in gj.DefaultIfEmpty()
            //              select new { users.ApprovalUser, ApprovalProcessId = (subpet == null ? 0 : subpet.ApprovalProcessId) }).ToList(); ;
            //if (result.Count > 0)
            //{
            //  int i = 0;
            //  string[] NameArray = new string[4];
            //  var userslist = db.UserProfiles.Select(x => new { Name = x.FirstName + " " + x.MiddleName + " " + x.LastName, Id = x.UserId }).ToList();
            //  foreach (var item in result.Where(x => x.ApprovalProcessId != 0))
            //  {
            //    NameArray[i] = userslist.Where(x => x.Id == item.ApprovalUser).Select(x => x.Name).FirstOrDefault();
            //    i++;
            //  }

            //  ViewBag.ApprovedBy = NameArray[0];
            //  ViewBag.Itemsverified = NameArray[1];
            //  ViewBag.ItemsPayment = NameArray[2];
            //  ViewBag.AuditedBy = NameArray[3];

            //}
            var PersonId = db.MasterContributor.Where(x => x.Id.ToString() == refundApplications.PersonID).Select(x => new { x.FirstName, x.MidName, x.LastName }).FirstOrDefault();
            ViewBag.Name = PersonId.FirstName + " " + PersonId.MidName + " " + PersonId.LastName;
            return View(refundApplications);

        }

        [HttpPost]
        public ActionResult EditDetails(RefundApplications model)
        {
            if (ModelState.IsValid)
            {
                model.SubmittedBy = AppUserManager.GetUserId();
                model.IsActive = true;
                var employerTypeId = db.MasterEmployer.Where(x => x.Id == model.EmployerID).Select(x => x.EmployerTypeID).FirstOrDefault();
                if (db.MasterInterestRate.Where(x => x.DateFrom <= model.RetirementOrResignationDate && (x.DateTo == null ? model.RetirementOrResignationDate : x.DateTo) >= model.RetirementOrResignationDate && x.EmployerTypeID == employerTypeId && x.IsActive == true).Any())
                {
                    var interestRateDetails = db.MasterInterestRate.Where(x => x.DateFrom <= model.RetirementOrResignationDate && (x.DateTo == null ? model.RetirementOrResignationDate : x.DateTo) >= model.RetirementOrResignationDate && x.EmployerTypeID == employerTypeId && x.IsActive == true).Select(x => new { x.Id, x.InterestRate }).FirstOrDefault();
                    model.InterestRateId = interestRateDetails.Id;
                    model.InterestRate = interestRateDetails.InterestRate;
                }
                db.Entry(model).State = EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    var applicant = db.MasterContributor.Where(x => x.Id.ToString() == model.PersonID).Select(x => new { Name = x.FirstName + " " + x.MidName + " " + x.LastName, x.Email }).FirstOrDefault();
                    PensionAndRefundEmailRepo.sendMailProcedureRefund(applicant.Email, model.Id, applicant.Name, "Application For Refund Submit Successfully");
                    return RedirectToAction("Index", new { id = model.Id });
                }
            }
            return RedirectToAction("RefundDetails", new { id = model.Id });
        }
    }
}
