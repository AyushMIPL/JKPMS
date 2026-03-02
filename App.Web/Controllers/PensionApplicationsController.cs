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
using System.Globalization;
using App.Web.Filters;
using System.Collections;
using App.Web.Repository;
namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class PensionApplicationsController : BaseController
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;
        private AppDbContext _DbContext;

        public PensionApplicationsController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _DbContext = DbContext;
        }

        // GET: PensionApplications
        public ActionResult Index()
        {
            ViewBag.EmployerTypeID = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true), "Id", "Name", 2);
            return View();
        }
        public ActionResult AjaxHandler(App.Web.Models.JQueryDataTableParamModel param, int Id)
        {
            var result = from P in db.PensionApplications.Where(x => x.IsActive == true)
                             //join MP in db.MasterPensioner on P.PersonID equals MP.PensionerID
                         join MC in db.MasterContributor.Where(x => x.IsActive == true) on P.PersonID equals MC.Id.ToString()
                         join E in db.MasterEmployer.Where(x => x.IsActive == true && x.EmployerTypeID == Id) on MC.EmployerID equals E.Id
                         join C in db.MasterContributorJobDetails on MC.PersonID equals C.PersonID
                         //join D in db.MasterDesignation on C.DesignationId equals D.Id


                         //var result = from MP in db.MasterPensioner
                         //																			 join PA in db.PensionApplications on MP.PersonID equals PA.PersonID
                         //																			 join E in db.MasterEmployer on PA.EmployerID equals E.Id
                         //																			 join C in db.MasterContributorJobDetails on MP.EmployerID equals C.EmployerID
                         //																			 join D in db.MasterDesignation on C.DesignationId equals D.Id
                         select new
                         {
                             Id = P.Id,
                             EmployerID = P.EmployerID,
                             Employer = E,
                             FirstName = MC.FirstName,
                             PersonID = C.PersonID,
                             MiddleName = MC.MidName,
                             LastName = MC.LastName,
                             //DesignationId = P.DesignationId,
                             Designation = C.Designation,
                             DesignationId = C.DesignationId == null ? 0 : C.DesignationId,
                             PensionableStatus = P.PensionableStatus,
                             BenefitTypes = P.BenefitTypes,
                             LeaveDue = P.LeaveDue,
                             RetirementOrResignationDate = P.RetirementOrResignationDate,
                             DOB = P.DOB,
                             DateofAppointment1st = P.DateofAppointment1st,
                             //DateofAppointmentLast = P.DateofAppointmentLast,
                             LengthOfQualifyingServiceInMonthsTo31Dec2003 = P.LengthOfQualifyingServiceInMonthsTo31Dec2003,
                             LengthOfQualifyingServiceInMonthsFrom1Jan2014 = P.LengthOfQualifyingServiceInMonthsFrom1Jan2014,
                             LongerShorterReason = P.LongerShorterReason,
                             RetirementAnnualSalary1 = P.RetirementAnnualSalary1,
                             RetirementAnnualSalary2 = P.RetirementAnnualSalary2,
                             ApplicationSubmittedBy = P.ApplicationSubmittedBy,
                             //NotedBy = P.NotedBy,
                             FullpensionAmount1 = P.FullpensionAmount1,
                             FullpensionAmount2 = P.FullpensionAmount2,
                             TotalFullpension = P.TotalFullpension,
                             MaxPension = P.MaxPension,
                             Gratuity = P.Gratuity,
                         };
            List<PensionApplications> List = new List<PensionApplications>();
            PensionApplications PA;
            foreach (var list in result)
            {
                PA = new PensionApplications();
                PA.EmployerID = list.EmployerID;
                PA.Employer = list.Employer;
                PA.FirstName = list.FirstName;
                PA.PersonID = list.PersonID;
                PA.MiddleName = list.MiddleName;
                PA.LastName = list.LastName;
                PA.Id = list.Id;
                PA.DesignationId = Convert.ToInt32(list.DesignationId);
                PA.Designation = list.Designation;
                PA.PensionableStatus = list.PensionableStatus;
                PA.BenefitTypes = list.BenefitTypes;
                PA.LeaveDue = list.LeaveDue;
                PA.RetirementOrResignationDate = list.RetirementOrResignationDate;
                PA.DOB = list.DOB;
                PA.DateofAppointment1st = list.DateofAppointment1st;
                //PA.DateofAppointmentLast = list.DateofAppointmentLast;
                PA.LengthOfQualifyingServiceInMonthsTo31Dec2003 = list.LengthOfQualifyingServiceInMonthsTo31Dec2003;
                PA.LengthOfQualifyingServiceInMonthsFrom1Jan2014 = list.LengthOfQualifyingServiceInMonthsFrom1Jan2014;
                PA.LongerShorterReason = list.LongerShorterReason;
                PA.RetirementAnnualSalary1 = list.RetirementAnnualSalary1;
                PA.RetirementAnnualSalary2 = list.RetirementAnnualSalary2;
                PA.ApplicationSubmittedBy = list.ApplicationSubmittedBy;
                //PA.NotedBy = list.NotedBy;
                //PA.HROfficer = list.HROfficer;
                PA.FullpensionAmount1 = list.FullpensionAmount1;
                PA.FullpensionAmount2 = list.FullpensionAmount2;
                PA.TotalFullpension = list.TotalFullpension;
                PA.MaxPension = list.MaxPension;
                PA.Gratuity = list.Gratuity;
                //PA.PensionerID = list.PensionerID;
                List.Add(PA);
            }

            IEnumerable<PensionApplications> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.FirstName.ToLower().Contains(param.sSearch.ToLower())
                   || (c.MiddleName + "").ToLower().Contains(param.sSearch.ToLower())
                   || c.LastName.ToLower().Contains(param.sSearch.ToLower())
                   //|| c.Designation.Name.ToLower().Contains(param.sSearch.ToLower())
                   || c.Employer.EmployerName.ToLower().Contains(param.sSearch.ToLower())
                   || c.LengthOfQualifyingServiceInMonthsTo31Dec2003.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.LengthOfQualifyingServiceInMonthsFrom1Jan2014.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.RetirementAnnualSalary1.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.RetirementAnnualSalary2.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.TotalFullpension.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.MaxPension.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.Gratuity.ToString().ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            //var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            //Func<MasterDepartment, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name :
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

                         c.PersonID,
                                         c.FirstName + " "+c.MiddleName+" "+c.LastName,
                                         c.Employer.EmployerName,
                     c.Designation==null?"":c.Designation.Name,
                      c.LengthOfQualifyingServiceInMonthsTo31Dec2003+"",
                     c.LengthOfQualifyingServiceInMonthsFrom1Jan2014+"",
                     Convert.ToDecimal(c.FullpensionAmount1).ToString("#,##0.00"),
                     Convert.ToDecimal(c.FullpensionAmount2).ToString("#,##0.00"),
                     c.TotalFullpension.ToString("#,##0.00"),
                     c.MaxPension.ToString("#,##0.00"),
                     Convert.ToDecimal(c.Gratuity).ToString("#,##0.00"),
                     c.Id + ""
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

        public ActionResult ListAjaxHandler(App.Web.Models.JQueryDataTableParamModel param, int Id)
        {
            var result = from P in db.PensionApplications
                             //join MP in db.MasterPensioner on P.PersonID equals MP.PensionerID
                         join MC in db.MasterContributor.Where(x => x.IsActive == true) on P.PersonID equals MC.Id.ToString()
                         join E in db.MasterEmployer.Where(x => x.IsActive == true && x.EmployerTypeID == Id) on MC.EmployerID equals E.Id
                         join C in db.MasterContributorJobDetails on MC.PersonID equals C.PersonID
                         //join D in db.MasterDesignation on C.DesignationId equals D.Id


                         //var result = from MP in db.MasterPensioner
                         //																			 join PA in db.PensionApplications on MP.PersonID equals PA.PersonID
                         //																			 join E in db.MasterEmployer on PA.EmployerID equals E.Id
                         //																			 join C in db.MasterContributorJobDetails on MP.EmployerID equals C.EmployerID
                         //																			 join D in db.MasterDesignation on C.DesignationId equals D.Id
                         select new
                         {
                             Id = P.Id,
                             EmployerID = P.EmployerID,
                             Employer = E,
                             FirstName = MC.FirstName,
                             PersonID = C.PersonID,
                             MiddleName = MC.MidName,
                             LastName = MC.LastName,
                             //DesignationId = P.DesignationId,
                             Designation = C.Designation,
                             DesignationId = C.DesignationId == null ? 0 : C.DesignationId,
                             PensionableStatus = P.PensionableStatus,
                             BenefitTypes = P.BenefitTypes,
                             LeaveDue = P.LeaveDue,
                             RetirementOrResignationDate = P.RetirementOrResignationDate,
                             DOB = P.DOB,
                             DateofAppointment1st = P.DateofAppointment1st,
                             //DateofAppointmentLast = P.DateofAppointmentLast,
                             LengthOfQualifyingServiceInMonthsTo31Dec2003 = P.LengthOfQualifyingServiceInMonthsTo31Dec2003,
                             LengthOfQualifyingServiceInMonthsFrom1Jan2014 = P.LengthOfQualifyingServiceInMonthsFrom1Jan2014,
                             LongerShorterReason = P.LongerShorterReason,
                             RetirementAnnualSalary1 = P.RetirementAnnualSalary1,
                             RetirementAnnualSalary2 = P.RetirementAnnualSalary2,
                             ApplicationSubmittedBy = P.ApplicationSubmittedBy,
                             //NotedBy = P.NotedBy,
                             FullpensionAmount1 = P.FullpensionAmount1,
                             FullpensionAmount2 = P.FullpensionAmount2,
                             TotalFullpension = P.TotalFullpension,
                             MaxPension = P.MaxPension,
                             Gratuity = P.Gratuity,
                         };
            List<PensionApplications> List = new List<PensionApplications>();
            PensionApplications PA;
            foreach (var list in result)
            {
                PA = new PensionApplications();
                PA.EmployerID = list.EmployerID;
                PA.Employer = list.Employer;
                PA.FirstName = list.FirstName;
                PA.PersonID = list.PersonID;
                PA.MiddleName = list.MiddleName;
                PA.LastName = list.LastName;
                PA.Id = list.Id;
                PA.DesignationId = Convert.ToInt32(list.DesignationId);
                PA.Designation = list.Designation;
                PA.PensionableStatus = list.PensionableStatus;
                PA.BenefitTypes = list.BenefitTypes;
                PA.LeaveDue = list.LeaveDue;
                PA.RetirementOrResignationDate = list.RetirementOrResignationDate;
                PA.DOB = list.DOB;
                PA.DateofAppointment1st = list.DateofAppointment1st;
                //PA.DateofAppointmentLast = list.DateofAppointmentLast;
                PA.LengthOfQualifyingServiceInMonthsTo31Dec2003 = list.LengthOfQualifyingServiceInMonthsTo31Dec2003;
                PA.LengthOfQualifyingServiceInMonthsFrom1Jan2014 = list.LengthOfQualifyingServiceInMonthsFrom1Jan2014;
                PA.LongerShorterReason = list.LongerShorterReason;
                PA.RetirementAnnualSalary1 = list.RetirementAnnualSalary1;
                PA.RetirementAnnualSalary2 = list.RetirementAnnualSalary2;
                PA.ApplicationSubmittedBy = list.ApplicationSubmittedBy;
                //PA.NotedBy = list.NotedBy;
                //PA.HROfficer = list.HROfficer;
                PA.FullpensionAmount1 = list.FullpensionAmount1;
                PA.FullpensionAmount2 = list.FullpensionAmount2;
                PA.TotalFullpension = list.TotalFullpension;
                PA.MaxPension = list.MaxPension;
                PA.Gratuity = list.Gratuity;
                //PA.PensionerID = list.PensionerID;
                List.Add(PA);
            }

            IEnumerable<PensionApplications> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => (c.FirstName + " " + c.MiddleName + " " + c.LastName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
                   //|| (c.MiddleName + "").ToLower().Contains(param.sSearch.ToLower())
                   //|| c.LastName.ToLower().Contains(param.sSearch.ToLower())
                   //|| c.Designation.Name.ToLower().Contains(param.sSearch.ToLower())
                   || c.Employer.EmployerName.ToLower().Contains(param.sSearch.ToLower())
                   || c.LengthOfQualifyingServiceInMonthsTo31Dec2003.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.LengthOfQualifyingServiceInMonthsFrom1Jan2014.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.RetirementAnnualSalary1.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.RetirementAnnualSalary2.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.TotalFullpension.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.MaxPension.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.Gratuity.ToString().ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            //var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            //Func<MasterDepartment, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name :
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

                         c.PersonID,
                                         c.FirstName + " "+c.MiddleName+" "+c.LastName,
                                         c.Employer.EmployerName,
                     c.Designation==null?"":c.Designation.Name,
                      c.LengthOfQualifyingServiceInMonthsTo31Dec2003+"",
                     c.LengthOfQualifyingServiceInMonthsFrom1Jan2014+"",
                     Convert.ToDecimal(c.RetirementAnnualSalary1).ToString("#,##0.00"),
                     Convert.ToDecimal(c.TotalFullpension).ToString("#,##0.00"),
                     Convert.ToDecimal(c.ReducedPension).ToString("#,##0.00"),
                     //c.MaxPension.ToString("#,##0.00"),
                     Convert.ToDecimal(c.Gratuity).ToString("#,##0.00"),
                     c.Id + ""
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

        public ActionResult _PolicePensionEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PensionApplications model = db.PensionApplications.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            var levels = (from level in db.ApprovalProcessLevel join users in db.ApprovalProcessAssignedUser on level.ApprovalProcessLevelId equals users.ApprovalProcessLevelId where level.ModuleId == 16 select users).ToList();
            int MaxLevel = levels.LastOrDefault().ApprovalProcessId;
            int MinLevel = levels.FirstOrDefault().ApprovalProcessId;
            if (db.ApplicationApprovalStatus.Where(x => x.ApplicationId == id && x.ApprovalProcessId >= MinLevel && x.ApprovalProcessId <= MaxLevel).Any())
            {
                ViewBag.EditAllowed = "No";
            }
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", model.EmployerID);
            var BenefitTypes = from BenefitType e in Enum.GetValues(typeof(BenefitType))
                               select new { Id = (int)e, Name = e.ToString() };
            ViewBag.BenefitTypes = new SelectList(BenefitTypes.OrderBy(x => x.Name), "Id", "Name", model.BenefitTypes);
            var Contributor = db.MasterContributor.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName }).ToList();
            ViewBag.PersonID = new SelectList(Contributor, "Id", "Name", model.PersonID);
            ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true), "Id", "Name", model.DesignationId);
            return View(model);
        }
        public ActionResult _PublicPensionEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PensionApplications model = db.PensionApplications.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            var levels = (from level in db.ApprovalProcessLevel join users in db.ApprovalProcessAssignedUser on level.ApprovalProcessLevelId equals users.ApprovalProcessLevelId where level.ModuleId == 16 select users).ToList();
            int MaxLevel = levels.LastOrDefault().ApprovalProcessId;
            int MinLevel = levels.FirstOrDefault().ApprovalProcessId;
            if (db.ApplicationApprovalStatus.Where(x => x.ApplicationId == id && x.ApprovalProcessId >= MinLevel && x.ApprovalProcessId <= MaxLevel).Any())
            {
                ViewBag.EditAllowed = "No";
            }
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", model.EmployerID);
            var BenefitTypes = from BenefitType e in Enum.GetValues(typeof(BenefitType))
                               select new { Id = (int)e, Name = e.ToString() };
            ViewBag.BenefitTypes = new SelectList(BenefitTypes.OrderBy(x => x.Name), "Id", "Name", model.BenefitTypes);
            var Contributor = db.MasterContributor.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName }).ToList();
            ViewBag.PersonID = new SelectList(Contributor, "Id", "Name", model.PersonID);
            ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true), "Id", "Name", model.DesignationId);
            return View(model);
        }
        public JsonResult GetDetails(int CId)
        {
            var dateofbirth = db.MasterContributor.Where(x => x.Id == CId).Select(x => x.DateOfBirth).FirstOrDefault();
            var result1 = string.Format("{0:MM/dd/yyyy}", dateofbirth);
            var result = (from a in db.MasterContributor join b in db.MasterContributorJobDetails on a.PersonID equals b.PersonID where a.Id == CId select new { DesignationId = b.DesignationId, result1 }).ToList();

            //select t1.DateOfBirth, t3.Name
            //from MasterContributors t1
            //left outer join MasterJobDetails t2 on t1.JKPSUniqueID = t2.PersonId
            //left outer join MasterDesignation t3 on t2.DesignationId = t3.Id
            //Where t1.id = 5


            var result11 = from t1 in db.MasterContributor
                           join t2 in db.MasterContributorJobDetails on t1.PersonID equals t2.PersonID into ms
                           where t1.Id == CId
                           from t2 in ms.DefaultIfEmpty()
                           select new { dateofbirth = t1.DateOfBirth, designation = t2.Designation.Name };

            //var result1 = from t1 in db.MasterContributor
            //              join t2 in db.MasterJobDetails on t1.JKPSUniqueID equals t2.PersonId into ms
            //              where t1.Id == CId
            //              from t2 in ms.DefaultIfEmpty()
            //              select new { dateofbirth = t1.DateOfBirth, designation = t2.Designation.Name };




            //var result = db.MasterContributor.Where(x => x.Id == CId).FirstOrDefault();    //.Select(x => x.DateOfBirth).FirstOrDefault();
            //var resdob = string.Format("{0:MM/dd/yyyy}", result.DateOfBirth);

            //var resvname = "Ashish";
            //var results = new { dob = resdob, name = resvname };

            //var result = from a in db.MasterContributor 
            //						 join b in db.MasterContributorJobDetails on a.JKPSUniqueID equals b.PersonId 
            //						 select new { DesignationId = b.DesignationId };

            //return Json(result1.AsEnumerable().Select(x => new
            //{
            //  dateofbirth = x.dateofbirth.GetValueOrDefault().ToString("MM/dd/yyyy"),
            //  designation = (x.designation != null ? x.designation : "")
            //}), JsonRequestBehavior.AllowGet);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CalculatePublicPensionDetailsAjax(string RetirementOrResignationDate, string PersonID)
        {
            DateTime retirementOrResignationDate = Convert.ToDateTime(RetirementOrResignationDate);
            int personId = Convert.ToInt32(PersonID);
            var employerType = db.MasterContributor.Where(x => x.Id == personId).Select(x => x.Employer.EmployerTypeID).FirstOrDefault();
            if (!db.MasterDiscountForGratuityMain.Where(x => x.DateFrom <= retirementOrResignationDate && (!x.DateTo.HasValue ? (DateTime.Now > retirementOrResignationDate ? DateTime.Now : retirementOrResignationDate) : x.DateTo) >= retirementOrResignationDate && x.EmployerTypeID == employerType && x.IsActive == true).Any())
            {
                return Json("discountedGratuity", JsonRequestBehavior.AllowGet);
            }
            var result = PensionAndRefundRepo.CalculatePublicPensionDetails(retirementOrResignationDate, personId, "Yes");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalculatePolicePensionDetailsAjax(string RetirementOrResignationDate, string PersonID, string status)
        {
            DateTime retirementOrResignationDate = Convert.ToDateTime(RetirementOrResignationDate);
            int personId = Convert.ToInt32(PersonID);
            var employerType = db.MasterContributor.Where(x => x.Id == personId).Select(x => x.Employer.EmployerTypeID).FirstOrDefault();
            if (!db.MasterDiscountForGratuityMain.Where(x => x.DateFrom <= retirementOrResignationDate && (!x.DateTo.HasValue ? (DateTime.Now > retirementOrResignationDate ? DateTime.Now : retirementOrResignationDate) : x.DateTo) >= retirementOrResignationDate && x.EmployerTypeID == employerType && x.IsActive == true).Any())
            {
                return Json("discountedGratuity", JsonRequestBehavior.AllowGet);
            }
            var result = PensionAndRefundRepo.CalculatePolicePensionDetails(retirementOrResignationDate, personId, status, "Yes");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: PensionApplications/Create
        public ActionResult Create()
        {
            ViewBag.EmployerType = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true), "Id", "Name", 2);
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true && x.EmployerTypeID == (int)PensionType.Public).OrderBy(x => x.EmployerName), "Id", "EmployerName");
            var BenefitTypes = from BenefitType e in Enum.GetValues(typeof(BenefitType))
                               select new { Id = (int)e, Name = e.ToString() };
            ViewBag.BenefitTypes = new SelectList(BenefitTypes.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true), "Id", "Name");
            var Contributor = new List<MasterContributor>();
            ViewBag.PersonID = new SelectList(Contributor, "Id", "FirstName");
            int currentUser = AppUserManager.GetUserId();
            var allPermissionUser = (from level in db.ApprovalProcessLevel join users in db.ApprovalProcessAssignedUser on level.ApprovalProcessLevelId equals users.ApprovalProcessLevelId where level.ModuleId == 16 select users).FirstOrDefault();
            //       int userPermissiontoFirstLevel = Convert.ToInt32(allPermissionUser.ApprovalUser);
            //if (currentUser != userPermissiontoFirstLevel)
            //{
            //    ViewBag.RightForApproval = "No";
            //}
            return View();
        }
        public JsonResult GetEmployerAjax(int? id)
        {
            var employer = db.MasterEmployer.Where(x => x.EmployerTypeID == id && x.IsActive == true).Select(x => new { Id = x.Id, Name = x.EmployerName }).ToList();
            return Json(employer, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _PoliceForce(int? id)
        {
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true && x.EmployerTypeID == id).OrderBy(x => x.EmployerName), "Id", "EmployerName");
            var BenefitTypes = from BenefitType e in Enum.GetValues(typeof(BenefitType))
                               select new { Id = (int)e, Name = e.ToString() };
            ViewBag.BenefitTypes = new SelectList(BenefitTypes.OrderBy(x => x.Name), "Id", "Name");
            var Contributor = new List<MasterPensioner>();
            ViewData["PersonID"] = new SelectList(Contributor, "Id", "FirstName");
            ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true), "Id", "Name");
            int currentUser = AppUserManager.GetUserId();
            var allPermissionUser = (from level in db.ApprovalProcessLevel join users in db.ApprovalProcessAssignedUser on level.ApprovalProcessLevelId equals users.ApprovalProcessLevelId where level.ModuleId == 16 select users).FirstOrDefault();
            int userPermissiontoFirstLevel = Convert.ToInt32(allPermissionUser.ApprovalUser);
            if (currentUser != userPermissiontoFirstLevel)
            {
                ViewBag.RightForApproval = "No";
            }
            return View();
        }
        public ActionResult _PublicService(int? id)
        {
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true && x.EmployerTypeID == id).OrderBy(x => x.EmployerName), "Id", "EmployerName");
            var BenefitTypes = from BenefitType e in Enum.GetValues(typeof(BenefitType))
                               select new { Id = (int)e, Name = e.ToString() };
            ViewBag.BenefitTypes = new SelectList(BenefitTypes.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true), "Id", "Name");
            var Contributor = new List<MasterContributor>();
            ViewData["PersonID"] = new SelectList(Contributor, "Id", "FirstName");
            int currentUser = AppUserManager.GetUserId();
            var allPermissionUser = (from level in db.ApprovalProcessLevel join users in db.ApprovalProcessAssignedUser on level.ApprovalProcessLevelId equals users.ApprovalProcessLevelId where level.ModuleId == 16 select users).FirstOrDefault();
            int userPermissiontoFirstLevel = Convert.ToInt32(allPermissionUser.ApprovalUser);
            if (currentUser != userPermissiontoFirstLevel)
            {
                ViewBag.RightForApproval = "No";
            }
            return View();
        }
        public List<App.Data.ViewModels.DeathlistViewModel> deathPensiondistribution(int Id, string PensionAmount)
        {
            decimal pensionAmount = Convert.ToDecimal(PensionAmount);
            decimal spousePension = 0;
            decimal childPension = 0;
            string personID = db.MasterContributor.Where(x => x.Id == Id).Select(x => x.PersonID).FirstOrDefault();
            List<App.Data.ViewModels.DeathlistViewModel> List = new List<App.Data.ViewModels.DeathlistViewModel>();
            var date = DateTime.Now;
            var model = db.MasterDependantDetails.Where(x => x.IsActive == true && x.PersonID == personID && date < x.TerminationDate).ToList();
            var marriageModel = db.MasterContributorMarriageDetails.Where(x => x.IsActive == true && x.PersonId == personID && x.MarriageEndDate == null).FirstOrDefault();
            if (model.Count > 0)
            {
                int count = model.Count;
                if (marriageModel == null)
                {
                    if (count == 1)
                    {
                        childPension = pensionAmount / 4;
                    }
                    else
                    {
                        childPension = (pensionAmount / (2 * count));
                    }
                }
                else
                {
                    spousePension = pensionAmount / 2;
                    childPension = (pensionAmount / (2 * count));
                    int Age = (Convert.ToInt32(date.Subtract(marriageModel.DateOfBirth.Value).Days / (365.25 / 12))) / 12;
                    App.Data.ViewModels.DeathlistViewModel templist = new App.Data.ViewModels.DeathlistViewModel();
                    templist.Name = marriageModel.SpouseFirstName + " " + marriageModel.MidName + " " + marriageModel.LastName;
                    templist.Relationship = "S";
                    templist.Age = Age;
                    templist.Id = marriageModel.Id;
                    templist.PensionAmount = spousePension;
                    List.Add(templist);
                }
                foreach (var item in model)
                {
                    int Age = (Convert.ToInt32(date.Subtract(item.DateOfBirth.Value).Days / (365.25 / 12))) / 12;
                    App.Data.ViewModels.DeathlistViewModel templist = new App.Data.ViewModels.DeathlistViewModel();
                    templist.Name = item.FirstName + " " + item.MidName + " " + item.LastName;
                    templist.Relationship = "C";
                    templist.Age = Age;
                    templist.Id = item.Id;
                    templist.PensionAmount = childPension;
                    List.Add(templist);
                }

            }
            else
            {
                if (marriageModel != null)
                {
                    spousePension = pensionAmount / 2;
                    int Age = (Convert.ToInt32(date.Subtract(marriageModel.DateOfBirth.Value).Days / (365.25 / 12))) / 12;
                    App.Data.ViewModels.DeathlistViewModel templist = new App.Data.ViewModels.DeathlistViewModel();
                    templist.Name = marriageModel.SpouseFirstName + " " + marriageModel.MidName + " " + marriageModel.LastName;
                    templist.Relationship = "S";
                    templist.Age = Age;
                    templist.Id = marriageModel.Id;
                    templist.PensionAmount = spousePension;
                    List.Add(templist);
                }
            }
            return List;
        }
        public ActionResult DeathDependantAjax(App.Web.Models.JQueryDataTableParamModel param, int Id, string PensionAmount)
        {
            var List = deathPensiondistribution(Id, PensionAmount);
            IEnumerable<App.Data.ViewModels.DeathlistViewModel> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.Name.ToLower().Contains(param.sSearch.ToLower())
                   || c.Age.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.Relationship.ToLower().Contains(param.sSearch.ToLower())
                   || c.PensionAmount.ToString().ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Pagging
            var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            //Select required columns
            var result = from c in displayed
                         select new[] {
                         c.Name,
                         c.Relationship,
                                         c.Age + "",
                                         c.PensionAmount + ""
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
        public JsonResult GetPensionerAjax(int empId)
        {
            var result = db.MasterContributor.Where(x => x.EmployerID == empId && x.IsActive == true && x.JobStatusID == 1).OrderBy(x => x.FirstName).Select(x => new { Value = x.Id, Text = x.FirstName + " " + x.MidName + " " + x.LastName });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPostAjax(int? Id)
        {
            var result = (from x in db.MasterContributor
                          join b in db.ContributorSalaryHistory on x.PersonID equals b.PersonID
                          join j in db.MasterContributorJobDetails on x.PersonID equals j.PersonID
                          join d in db.MasterDesignation on j.DesignationId equals d.Id
                          where (x.IsActive == true && b.Default == true & x.Id == Id)
                          select new { b.SalaryAmount, x.DateOfBirth, x.FirstAppointmentDate, DesignationName = d.Name, DesignationId = d.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName, ExpectedRetirement = x.ExpectedRetirementDate, x.LastAppointmentDate }).FirstOrDefault();
            if (result != null)
            {
                var results = new { result.SalaryAmount, DateOfBirth = string.Format("{0:MM/dd/yyyy}", result.DateOfBirth), FirstAppointmentDate = string.Format("{0:MM/dd/yyyy}", result.FirstAppointmentDate), ExpectedRetirement = string.Format("{0:MM/dd/yyyy}", result.ExpectedRetirement), result.DesignationId, result.Name, LastAppointmentDate = (result.LastAppointmentDate == null ? "" : string.Format("{0:MM/dd/yyyy}", result.LastAppointmentDate)) };
                return Json(results, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }





        public JsonResult CheckPensionApplication(int PersonID)
        {
            var checkPensioner = from P in db.PensionApplications
                                 where P.PersonID == PersonID.ToString() && P.IsActive == true
                                 select new { CreateDate = P.CreatedOn };

            return Json(checkPensioner, JsonRequestBehavior.AllowGet);
        }

        // POST: PensionApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,ContributorID,FirstName,MiddleName,LastName,DesignationID,PensionableStatus,BenefitTypes,LeaveDue,ResignationDate,RetirementDate,DOB,DateofAppointment1st,DateofAppointmentLast,LengthOfQualifyingServiceInMonthsTo31Dec2003,LengthOfQualifyingServiceInMonthsFrom1Jan2014,LongerShorterReason,RetirementAnnualSalary1,RetirementAnnualSalary2,ApplicationSubmittedBy,NotedBy,HROfficer,FullpensionAmount1,FullpensionAmount2,TotalFullpension,MaxPension,GratuityReducedPension,pensionPerAnnum,ApprovalLevel1,ApprovalLevel2,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] 
        public ActionResult Create(PensionApplications pensionApplications)
        {
            var employerType = Convert.ToInt32(Request["EmployerType"]);
            if (!db.RefundApplications.Any(x => x.PersonID == pensionApplications.PersonID && x.IsActive == true))
            {

                if (ModelState.IsValid)
                {
                    double totalservicelengthinYear = pensionApplications.RetirementOrResignationDate.Subtract(pensionApplications.DateofAppointment1st).Days / 365.25;
                    //int totalservicelengthinMonth = Convert.ToInt32(pensionApplications.RetirementOrResignationDate.Subtract(pensionApplications.DateofAppointment1st).Days / (365.25 / 12));
                    //int totalservicelengthinYear = totalservicelengthinMonth / 12;
                    if ((totalservicelengthinYear >= 10 && employerType == (int)PensionType.Public) || (totalservicelengthinYear >= 2 && employerType == (int)PensionType.Police))
                    {
                        var res = db.PensionApplications.Where(x => x.PersonID == pensionApplications.PersonID && x.EmployerID == pensionApplications.EmployerID && x.IsActive == true).FirstOrDefault();
                        if (res == null)
                        {
                            bool Approval = pensionApplications.IsActive;
                            pensionApplications.pensionPerAnnum = (pensionApplications.TotalFullpension > pensionApplications.MaxPension ? pensionApplications.MaxPension : pensionApplications.TotalFullpension);
                            pensionApplications.IsActive = true;
                            if (db.MasterDiscountForGratuityMain.Where(x => x.DateFrom <= pensionApplications.RetirementOrResignationDate && (x.DateTo == null ? pensionApplications.RetirementOrResignationDate : x.DateTo) >= pensionApplications.RetirementOrResignationDate && x.EmployerTypeID == employerType && x.IsActive == true).Any())
                            {
                                var discountDetails = db.MasterDiscountForGratuityMain.Where(x => x.DateFrom <= pensionApplications.RetirementOrResignationDate && (x.DateTo == null ? pensionApplications.RetirementOrResignationDate : x.DateTo) >= pensionApplications.RetirementOrResignationDate && x.EmployerTypeID == employerType && x.IsActive == true).Select(x => new { x.Id, x.DiscountFactorPercent }).FirstOrDefault();
                                pensionApplications.DiscountForGratuityMainId = discountDetails.Id;
                                pensionApplications.GratuityRate = discountDetails.DiscountFactorPercent;
                            }
                            db.PensionApplications.Add(pensionApplications);
                            int result = db.SaveChanges();
                            if (result > 0)
                            {
                                int personId = Convert.ToInt32(pensionApplications.PersonID);
                                string pensionAmount = pensionApplications.TotalFullpension + "";
                                if (pensionApplications.DeathInjuryNormal == "D")
                                {
                                    var dependentDetails = deathPensiondistribution(personId, pensionAmount);
                                    if (dependentDetails.Count > 0)
                                    {
                                        foreach (var item in dependentDetails)
                                        {
                                            ContributorDependantPensionDetails tmodel = new ContributorDependantPensionDetails();
                                            tmodel.RelationshipType = item.Relationship;
                                            tmodel.PensionableAmount = item.PensionAmount;
                                            tmodel.DependantID = item.Id;
                                            tmodel.ApplicationId = pensionApplications.Id;
                                            tmodel.IsActive = true;
                                            tmodel.PersonID = pensionApplications.PersonID;
                                            db.Entry(tmodel).State = EntityState.Added;
                                            db.SaveChanges();
                                        }
                                    }
                                }
                                if (Approval)
                                {
                                    ApplicationApprovalStatus mstatus = new ApplicationApprovalStatus();
                                    mstatus.ApplicationId = pensionApplications.Id;
                                    mstatus.ApprovalStatus = "A";
                                    mstatus.ApprovedDate = DateTime.Now;
                                    mstatus.ApprovalProcessId = 1;
                                    mstatus.Notes = Request["Notes"].ToString();
                                    db.Entry(mstatus).State = EntityState.Added;
                                    db.SaveChanges();

                                    var firstapprovaluser = (from a in db.ApprovalProcessLevel join b in db.ApprovalProcessAssignedUser on a.ApprovalProcessLevelId equals b.ApprovalProcessLevelId where a.ModuleId == 16 orderby b.ApprovalProcessLevelId select b.ApprovalUser).Skip(1).FirstOrDefault();
                                    var approvaluser = db.UserProfiles.Where(x => x.UserId == firstapprovaluser).Select(x => new { Name = x.FirstName + " " + x.MiddleName + " " + x.LastName, x.Email }).FirstOrDefault();
                                    PensionAndRefundEmailRepo.sendMailProcedure(approvaluser.Email, pensionApplications.Id, approvaluser.Name, "New Application For Pension Approval...");

                                }
                                else
                                {
                                    var firstapprovaluser = (from a in db.ApprovalProcessLevel join b in db.ApprovalProcessAssignedUser on a.ApprovalProcessLevelId equals b.ApprovalProcessLevelId where a.ModuleId == 16 orderby b.ApprovalProcessLevelId select b.ApprovalUser).FirstOrDefault();
                                    var approvaluser = db.UserProfiles.Where(x => x.UserId == firstapprovaluser).Select(x => new { Name = x.FirstName + " " + x.MiddleName + " " + x.LastName, x.Email }).FirstOrDefault();
                                    PensionAndRefundEmailRepo.sendMailProcedure(approvaluser.Email, pensionApplications.Id, approvaluser.Name, "New Application For Pension Approval...");

                                }

                                var applicant = db.MasterContributor.Where(x => x.Id.ToString() == pensionApplications.PersonID).Select(x => new { Name = x.FirstName + " " + x.MidName + " " + x.LastName, x.Email }).FirstOrDefault();
                                PensionAndRefundEmailRepo.sendMailProcedure(applicant.Email, pensionApplications.Id, applicant.Name, "NewApplication");

                            }
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["error"] = "Application for this Pensioner is already Submitted on dated " + res.CreatedOn;
                        }
                    }
                    else
                    {
                        string error = "";
                        if (employerType == (int)PensionType.Police)
                        {
                            error = "Service Length Should be Greater than 2 Years";
                        }
                        error = "Service Length Should be Greater than 10 Years";
                        TempData["error"] = error;
                    }
                }
            }
            else
            {
                TempData["error"] = "Request For Refund is Already Submitted for this Contributor";
            }
            ViewBag.EmployerType = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true), "Id", "Name", employerType);
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", pensionApplications.EmployerID);
            var BenefitTypes = from BenefitType e in Enum.GetValues(typeof(BenefitType))
                               select new { Id = (int)e, Name = e.ToString() };
            ViewBag.BenefitTypes = new SelectList(BenefitTypes.OrderBy(x => x.Name), "Id", "Name", pensionApplications.BenefitTypes);
            ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true), "Id", "Name", pensionApplications.DesignationId);
            var contributor = db.MasterContributor.Where(x => x.IsActive == true && x.JobStatusID == 1).OrderBy(x => x.FirstName).Select(x => new { Id = x.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName });
            ViewBag.PersonID = new SelectList(contributor, "Id", "Name", pensionApplications.PersonID);

            return View(pensionApplications);
        }

        // GET: PensionApplications/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //  if (id == null)
        //  {
        //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //  }
        //  PensionApplications pensionApplications = db.PensionApplications.Find(id);
        //  if (pensionApplications == null)
        //  {
        //    return HttpNotFound();
        //  }
        //  return View(pensionApplications);
        //}

        // POST: PensionApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,ContributorID,FirstName,MiddleName,LastName,DesignationID,PensionableStatus,BenefitTypes,LeaveDue,ResignationDate,RetirementDate,DOB,DateofAppointment1st,DateofAppointmentLast,LengthOfQualifyingServiceInMonthsTo31Dec2003,LengthOfQualifyingServiceInMonthsFrom1Jan2014,LongerShorterReason,RetirementAnnualSalary1,RetirementAnnualSalary2,ApplicationSubmittedBy,NotedBy,HROfficer,FullpensionAmount1,FullpensionAmount2,TotalFullpension,MaxPension,GratuityReducedPension,pensionPerAnnum,ApprovalLevel1,ApprovalLevel2,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] PensionApplications pensionApplications)
        //{
        //  if (ModelState.IsValid)
        //  {
        //    db.Entry(pensionApplications).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //  }
        //  return View(pensionApplications);
        //}

        // GET: PensionApplications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PensionApplications pensionApplications = db.PensionApplications.Find(id);
            if (pensionApplications == null)
            {
                return HttpNotFound();
            }
            return View(pensionApplications);
        }

        // POST: PensionApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PensionApplications pensionApplications = db.PensionApplications.Find(id);
            db.PensionApplications.Remove(pensionApplications);
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

        //
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PensionApplications pensionApplications = db.PensionApplications.Find(id);
            if (pensionApplications == null)
            {
                return HttpNotFound();
            }
            var Employer = db.MasterEmployer.Where(x => x.IsActive == true).ToList();
            ViewBag.EmployerID = new SelectList(Employer.OrderBy(x => x.EmployerName), "Id", "EmployerName", pensionApplications.EmployerID);


            //var BenefitTypes = Enum.GetValues(typeof(BenefitType)).Cast<BenefitType>();
            //ViewBag.BenefitTypes = new SelectList(BenefitTypes).ToString();

            var BenefitTypes = from BenefitType e in Enum.GetValues(typeof(BenefitType))
                               select new { Id = (int)e, Name = e.ToString() };
            ViewBag.BenefitTypes = new SelectList(BenefitTypes.OrderBy(x => x.Name), "Id", "Name", pensionApplications.BenefitTypes);
            ViewBag.DesignationName = db.MasterDesignation.Where(x => x.Id == pensionApplications.DesignationId).Select(x => x.Name).FirstOrDefault();
            ViewBag.PersonID = new SelectList(db.MasterContributor, "Id", "FirstName", pensionApplications.Id);
            ViewBag.NotedBy = new SelectList(db.UserProfiles.Where(x => x.IsActive == true), "UserId", "FirstName");
            ViewBag.HROfficer = new SelectList(db.UserProfiles.Where(x => x.IsActive == true), "UserId", "FirstName");
            ViewBag.Accountant = new SelectList(db.UserProfiles.Where(x => x.IsActive == true), "UserId", "FirstName");
            ViewBag.AuditorTreasury = new SelectList(db.UserProfiles.Where(x => x.IsActive == true), "UserId", "FirstName");
            return View(pensionApplications);
        }
        public ActionResult _EditDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PensionApplications pensionApplications = db.PensionApplications.Find(id);
            if (pensionApplications == null)
            {
                return HttpNotFound();
            }
            var BenefitTypes = from BenefitType e in Enum.GetValues(typeof(BenefitType))
                               select new { Id = (int)e, Name = e.ToString() };
            ViewBag.BenefitTypes = new SelectList(BenefitTypes.OrderBy(x => x.Name), "Id", "Name", pensionApplications.BenefitTypes);
            ViewBag.DesignationName = db.MasterDesignation.Where(x => x.Id == pensionApplications.DesignationId).Select(x => x.Name).FirstOrDefault();

            return View(pensionApplications);
        }
        [HttpPost]
        public ActionResult Edit(PensionApplications model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Record Saved Successfully";
                return RedirectToAction("Details", "PensionApplications", new { id = @model.Id });
            }
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", model.EmployerID);

            var BenefitTypes = from BenefitType e in Enum.GetValues(typeof(BenefitType))
                               select new { Id = (int)e, Name = e.ToString() };
            ViewBag.BenefitTypes = new SelectList(BenefitTypes.OrderBy(x => x.Name), "Id", "Name", model.BenefitTypes);
            var PersonId = db.MasterContributor.Where(x => x.IsActive == true && x.JobStatusID == 1).Select(x => new { Id = x.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName }).ToList();
            ViewBag.PersonID = new SelectList(PersonId, "Id", "Name", model.PersonID);

            return View(model);
        }
        // GET: PensionApplications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PensionApplications model = db.PensionApplications.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            int currentUser = AppUserManager.GetUserId();
            var levels = (from level in db.ApprovalProcessLevel join users in db.ApprovalProcessAssignedUser on level.ApprovalProcessLevelId equals users.ApprovalProcessLevelId where level.ModuleId == 16 select users).ToList();
            int MaxUserLevel = levels.Where(x => x.ApprovalUser == currentUser).LastOrDefault().ApprovalProcessId;
            int MaxLevel = levels.LastOrDefault().ApprovalProcessId;
            int MinLevel = levels.FirstOrDefault().ApprovalProcessId;
            if (db.ApplicationApprovalStatus.Where(x => x.ApplicationId == id && x.ApprovalProcessId >= MinLevel && x.ApprovalProcessId <= MaxLevel).Any())
            {
                ViewBag.EditAllowed = "No";
            }
            var empType = db.MasterEmployer.Where(x => x.Id == model.EmployerID).Select(x => x.EmployerTypeID).FirstOrDefault();
            ViewBag.EmployerType = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true).ToList(), "Id", "Name", empType);
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", model.EmployerID);
            var BenefitTypes = from BenefitType e in Enum.GetValues(typeof(BenefitType))
                               select new { Id = (int)e, Name = e.ToString() };
            ViewBag.BenefitTypes = new SelectList(BenefitTypes.OrderBy(x => x.Name), "Id", "Name", model.BenefitTypes);
            var Contributor = db.MasterContributor.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName }).ToList();
            ViewBag.PersonID = new SelectList(Contributor, "Id", "Name", model.PersonID);
            ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true), "Id", "Name", model.DesignationId);
            return View(model);

        }
        public ActionResult pensionRejectedDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PensionApplications model = db.PensionApplications.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            var empType = db.MasterEmployer.Where(x => x.Id == model.EmployerID).Select(x => x.EmployerTypeID).FirstOrDefault();
            ViewBag.EmployerType = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true).ToList(), "Id", "Name", empType);
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", model.EmployerID);
            var BenefitTypes = from BenefitType e in Enum.GetValues(typeof(BenefitType))
                               select new { Id = (int)e, Name = e.ToString() };
            ViewBag.BenefitTypes = new SelectList(BenefitTypes.OrderBy(x => x.Name), "Id", "Name", model.BenefitTypes);
            var Contributor = db.MasterContributor.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName }).ToList();
            ViewBag.PersonID = new SelectList(Contributor, "Id", "Name", model.PersonID);
            ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true), "Id", "Name", model.DesignationId);
            return View(model);
        }
        [HttpPost]
        public ActionResult Details(PensionApplications pensionApplications)
        {
            var employerType = Convert.ToInt32(Request["EmployerType"]);
            if (ModelState.IsValid)
            {
                double totalservicelengthinYear = pensionApplications.RetirementOrResignationDate.Subtract(pensionApplications.DateofAppointment1st).Days / 365.25;
                //int totalservicelengthinMonth = Convert.ToInt32(pensionApplications.RetirementOrResignationDate.Subtract(pensionApplications.DateofAppointment1st).Days / (365.25 / 12));
                //int totalservicelengthinYear = totalservicelengthinMonth / 12;
                if ((totalservicelengthinYear >= 10 && employerType == (int)PensionType.Public) || (totalservicelengthinYear >= 2 && employerType == (int)PensionType.Police))
                {
                    pensionApplications.pensionPerAnnum = (pensionApplications.TotalFullpension > pensionApplications.MaxPension ? pensionApplications.MaxPension : pensionApplications.TotalFullpension);
                    pensionApplications.IsActive = true;
                    if (db.MasterDiscountForGratuityMain.Where(x => x.DateFrom <= pensionApplications.RetirementOrResignationDate && (x.DateTo == null ? pensionApplications.RetirementOrResignationDate : x.DateTo) >= pensionApplications.RetirementOrResignationDate && x.EmployerTypeID == employerType && x.IsActive == true).Any())
                    {
                        var discountDetails = db.MasterDiscountForGratuityMain.Where(x => x.DateFrom <= pensionApplications.RetirementOrResignationDate && (x.DateTo == null ? pensionApplications.RetirementOrResignationDate : x.DateTo) >= pensionApplications.RetirementOrResignationDate && x.EmployerTypeID == employerType && x.IsActive == true).Select(x => new { x.Id, x.DiscountFactorPercent }).FirstOrDefault();
                        pensionApplications.DiscountForGratuityMainId = discountDetails.Id;
                        pensionApplications.GratuityRate = discountDetails.DiscountFactorPercent;
                    }
                    db.Entry(pensionApplications).State = EntityState.Modified;
                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        int personId = Convert.ToInt32(pensionApplications.PersonID);
                        string pensionAmount = pensionApplications.TotalFullpension + "";
                        var dependentDetails = deathPensiondistribution(personId, pensionAmount);
                        if (dependentDetails.Count > 0 && pensionApplications.DeathInjuryNormal == "D")
                        {
                            foreach (var item in dependentDetails)
                            {
                                ContributorDependantPensionDetails tmodel = db.ContributorDependantPensionDetails.Where(x => x.PersonID == pensionApplications.PersonID && x.RelationshipType == item.Relationship && x.ApplicationId == pensionApplications.Id && x.DependantID == item.Id).FirstOrDefault();
                                if (tmodel != null)
                                {
                                    tmodel.PensionableAmount = item.PensionAmount;
                                    tmodel.IsActive = true;
                                    db.Entry(tmodel).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                        }
                        var firstapprovaluser = (from a in db.ApprovalProcessLevel join b in db.ApprovalProcessAssignedUser on a.ApprovalProcessLevelId equals b.ApprovalProcessLevelId where a.ModuleId == 16 orderby b.ApprovalProcessLevelId select b.ApprovalUser).FirstOrDefault();
                        var approvaluser = db.UserProfiles.Where(x => x.UserId == firstapprovaluser).Select(x => new { Name = x.FirstName + " " + x.MiddleName + " " + x.LastName, x.Email }).FirstOrDefault();
                        PensionAndRefundEmailRepo.sendMailProcedure(approvaluser.Email, pensionApplications.Id, approvaluser.Name, "New Application For Pension Approval...");

                        var applicant = db.MasterContributor.Where(x => x.Id.ToString() == pensionApplications.PersonID).Select(x => new { Name = x.FirstName + " " + x.MidName + " " + x.LastName, x.Email }).FirstOrDefault();
                        PensionAndRefundEmailRepo.sendMailProcedure(applicant.Email, pensionApplications.Id, applicant.Name, "Application For Pension Submitted Successfully");

                    }
                    return RedirectToAction("Index");

                }
                else
                {

                    string error = "";
                    if (employerType == (int)PensionType.Police)
                    {
                        error = "Service Length Should be Greater than 2 Years";
                    }
                    error = "Service Length Should be Greater than 10 Years";
                    TempData["error"] = error;
                }
            }
            ViewBag.EmployerType = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true).ToList(), "Id", "Name", employerType);
            ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", pensionApplications.EmployerID);
            var BenefitTypes = from BenefitType e in Enum.GetValues(typeof(BenefitType))
                               select new { Id = (int)e, Name = e.ToString() };
            ViewBag.BenefitTypes = new SelectList(BenefitTypes.OrderBy(x => x.Name), "Id", "Name", pensionApplications.BenefitTypes);
            var Contributor = db.MasterContributor.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName }).ToList();
            ViewBag.PersonID = new SelectList(Contributor, "Id", "Name", pensionApplications.PersonID);
            ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true), "Id", "Name", pensionApplications.DesignationId);

            return View(pensionApplications);
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
            dt.Columns.Add("PensionableStatus");
            dt.Columns.Add("BenefitTypes");
            dt.Columns.Add("LeaveDue");
            dt.Columns.Add("LongerShorterReason");
            dt.Columns.Add("SubmittedBy");
            dt.Columns.Add("VerifiedBy");
            dt.Columns.Add("NotedBy");
            dt.Columns.Add("CertifiedForPayment");
            dt.Columns.Add("AuditedBy");
            return dt;
        }
        public JsonResult _PensionApplicationAjax(int? Id)
        {
            PensionApplications model = db.PensionApplications.Find(Id);
            if (model != null)
            {
                var PersonId = db.MasterContributor.Where(x => x.Id.ToString() == model.PersonID).FirstOrDefault();
                DataTable dt = dtPensionEstimate();
                DataRow dr = dt.NewRow();
                dr["Name"] = PersonId.FirstName + " " + PersonId.MidName + " " + PersonId.LastName;
                dr["LastAppointmentDate"] = String.Format("{0:MM/dd/yyyy}", PersonId.LastAppointmentDate);
                dr["DateOfBirth"] = String.Format("{0:MM/dd/yyyy}", PersonId.DateOfBirth);
                dr["DiscountedGratuity"] = model.GratuityReducedPension;
                //dr["ExpectedRetirementDate"] = model.ExpectedRetirementDate;
                dr["FirstAppointmentDate"] = PersonId.FirstAppointmentDate;
                dr["FullPension"] = model.TotalFullpension;
                dr["FullPension1"] = model.FullpensionAmount1;
                dr["FullPension2"] = model.FullpensionAmount2;
                dr["Gratuity"] = model.Gratuity;
                //dr["LengthOfQualifyingService"] = model.LengthOfQualifyingService == null ? 0 : model.LengthOfQualifyingService;
                dr["LengthOfQualifyingServiceInMonthsFrom1Jan2014"] = model.LengthOfQualifyingServiceInMonthsFrom1Jan2014 == null ? 0 : model.LengthOfQualifyingServiceInMonthsFrom1Jan2014;
                dr["LengthOfQualifyingServiceInMonthsTo31Dec2003"] = model.LengthOfQualifyingServiceInMonthsTo31Dec2003 == null ? 0 : model.LengthOfQualifyingServiceInMonthsTo31Dec2003;
                //dr["noOfMonthsWithContribution"] = model.noOfMonthsWithContribution == null ? 0 : model.noOfMonthsWithContribution;
                dr["PersonID"] = model.PersonID;
                dr["ReducedPension"] = model.ReducedPension;
                dr["RetirementOrResignationDate"] = model.RetirementOrResignationDate;
                dr["SalaryAmount"] = model.RetirementAnnualSalary1;
                dr["Post"] = model.Designation.Name;
                dr["PensionableStatus"] = model.PensionableStatus;
                dr["LongerShorterReason"] = model.LongerShorterReason;
                dr["LeaveDue"] = model.LeaveDue;
                dr["BenefitTypes"] = model.BenefitTypes;
                dr["MaxPension"] = model.MaxPension;

                List<ReportText> text = new List<ReportText>();
                //System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/Reports/DSPensionEstimate.xsd"));
                //ds.WriteXmlSchema(writer);
                //writer.Close();
                if (PersonId.Employer.EmployerTypeID == (int)PensionType.Police)
                {
                    text = db.ReportText.Where(x => x.ModuleId == 2).ToList();
                    this.HttpContext.Session["ReportName"] = "rptPolicePensionApplication.rpt";
                }
                else
                {
                    text = db.ReportText.Where(x => x.ModuleId == 1).ToList();
                    this.HttpContext.Session["ReportName"] = "rptPensionApplication.rpt";
                }
                if (text.Count > 0)
                {
                    dr["SubmittedBy"] = text[0].Signature;
                    dr["VerifiedBy"] = text[1].Signature;
                    dr["NotedBy"] = text[2].Signature;
                    dr["AuditedBy"] = text[3].Signature;
                    dr["CertifiedForPayment"] = text[4].Signature;
                }
                dt.Rows.Add(dr);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                this.HttpContext.Session["rptSource"] = ds;
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("0", JsonRequestBehavior.AllowGet);

        }
        public ActionResult _PrintPensionApplication(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PensionApplications pensionApplications = db.PensionApplications.Find(id);
            if (pensionApplications == null)
            {
                return HttpNotFound();
            }
            var result = (from users in db.ApprovalProcessAssignedUser
                          join level in db.ApprovalProcessLevel.Where(x => x.ModuleId == 16) on users.ApprovalProcessLevelId equals level.ApprovalProcessLevelId
                          join status in db.ApplicationApprovalStatus.Where(x => x.ApplicationId == id) on users.ApprovalProcessId equals status == null ? 0 : status.ApprovalProcessId into gj
                          from subpet in gj.DefaultIfEmpty()
                          select new { users.ApprovalUser, ApprovalProcessId = (subpet == null ? 0 : subpet.ApprovalProcessId) }).ToList(); ;
            if (result.Count > 0)
            {
                int i = 0;
                string[] NameArray = new string[4];
                var userslist = db.UserProfiles.Select(x => new { Name = x.FirstName + " " + x.MiddleName + " " + x.LastName, Id = x.UserId }).ToList();
                foreach (var item in result.Where(x => x.ApprovalProcessId != 0))
                {
                    NameArray[i] = userslist.Where(x => x.Id == item.ApprovalUser).Select(x => x.Name).FirstOrDefault();
                    i++;
                }

                ViewBag.NotedBy = NameArray[0];
                ViewBag.Itemsverified = NameArray[1];
                ViewBag.ItemsPayment = NameArray[2];
                ViewBag.AuditedBy = NameArray[3];

            }
            var PersonId = db.MasterContributor.Where(x => x.Id.ToString() == pensionApplications.PersonID).Select(x => new { x.FirstName, x.MidName, x.LastName }).FirstOrDefault();
            ViewBag.Name = PersonId.FirstName + " " + PersonId.MidName + " " + PersonId.LastName;
            return View(pensionApplications);

        }
        //public ActionResult _serviceLengthContribution(string EId, string PId, string ret, string fdate, string id)
        //{
        //  List<App.Data.ViewModels.ExcelFileViewModel> no = new List<App.Data.ViewModels.ExcelFileViewModel>();
        //  if (id == "0")
        //  {
        //    DateTime FirstApp = Convert.ToDateTime(fdate);
        //    DateTime pdate = new DateTime(2003, 12, 31);
        //    if (FirstApp < pdate)
        //    {
        //      if (FirstApp.Day != 1)
        //      {
        //        FirstApp = FirstApp.AddMonths(+1).AddDays(-(FirstApp.Day - 1));
        //      }
        //      no = (from a in _DbContext.ContributonSheetHeaderFinalise
        //            join b in _DbContext.ContributonSheetDetailsFinalise on a.Id equals b.ContributonSheetHeaderFinaliseID
        //            where (b.ContributorID.ToString() == PId && b.EntryDate <= pdate && b.EntryDate >= FirstApp && a.EmployerId.ToString() == EId && b.ContributorID.ToString() == PId)
        //            select new App.Data.ViewModels.ExcelFileViewModel { Month = a.Month, Year = a.Year, ContributorID = b.ContributorID, EmployerContribution = b.EmployerContribution, ContributorContribution = b.ContributorContribution, SalaryAmount = b.SalaryAmount }).ToList();
        //    }
        //  }
        //  else
        //  {
        //    DateTime FirstApp = Convert.ToDateTime(ret);
        //    DateTime pdate = new DateTime(2004, 01, 01);
        //    var monthEndDate = DateTime.DaysInMonth(FirstApp.Year, FirstApp.Month);
        //    var endday = FirstApp.Day;
        //    if (endday != monthEndDate)
        //    {
        //      FirstApp = FirstApp.AddDays(-endday);
        //    }
        //    no = (from a in _DbContext.ContributonSheetHeaderFinalise
        //          join b in _DbContext.ContributonSheetDetailsFinalise on a.Id equals b.ContributonSheetHeaderFinaliseID
        //          where (b.ContributorID.ToString() == PId && b.EntryDate >= pdate && b.EntryDate <= FirstApp && a.EmployerId.ToString() == EId)
        //          select new App.Data.ViewModels.ExcelFileViewModel { Month = a.Month, Year = a.Year, ContributorID = b.ContributorID, EmployerContribution = b.EmployerContribution, ContributorContribution = b.ContributorContribution}).ToList();
        //  }
        //  return View(no);
        //}

        public ActionResult _serviceLengthContributionDetails()
        {
            return View();
        }

        public ActionResult EditDetailSearchAjax(App.Web.Models.JQueryDataTableParamModel param, string EId, string PId, string ret, string fdate, string id)
        {
            List<App.Data.ViewModels.ExcelFileViewModel> List = new List<App.Data.ViewModels.ExcelFileViewModel>();
            List<App.Data.ViewModels.ExcelFileViewModel> allYearList = (from a in _DbContext.ContributonSheetHeaderFinalise
                                                                        join b in _DbContext.ContributonSheetDetailsFinalise on a.Id equals b.ContributonSheetHeaderFinaliseID
                                                                        where (b.ContributorID.ToString() == PId && a.IsActive == true && b.IsActive == true)
                                                                        select new App.Data.ViewModels.ExcelFileViewModel { Contributor = b.Contributor, Month = a.Month, Year = a.Year, ContributorID = b.ContributorID, EmployerContribution = b.EmployerContribution, ContributorContribution = b.ContributorContribution, SalaryAmount = b.SalaryAmount }).ToList();

            foreach (var item in allYearList)
            {

                int Month = DateTime.ParseExact(item.Month, "MMMM", CultureInfo.CurrentCulture).Month;
                DateTime date = Convert.ToDateTime(Month + "/01/" + item.Year);
                item.EntryDate = date;
                List.Add(item);
            }
            if (id == "0")
            {
                DateTime FirstApp = Convert.ToDateTime(fdate);
                DateTime pdate = new DateTime(2003, 12, 31);
                if (FirstApp < pdate)
                {
                    if (FirstApp.Day != 1)
                    {
                        FirstApp = FirstApp.AddMonths(+1).AddDays(-(FirstApp.Day - 1));
                    }

                    List = List.Where(x => x.EntryDate >= FirstApp && x.EntryDate <= pdate).ToList();
                }
            }
            else
            {
                DateTime FirstApp = Convert.ToDateTime(ret);
                DateTime pdate = new DateTime(2004, 01, 01);
                var monthEndDate = DateTime.DaysInMonth(FirstApp.Year, FirstApp.Month);
                var endday = FirstApp.Day;
                if (endday != monthEndDate)
                {
                    FirstApp = FirstApp.AddDays(-endday);
                }
                List = List.Where(x => x.EntryDate <= FirstApp && x.EntryDate >= pdate).ToList();
            }
            IEnumerable<App.Data.ViewModels.ExcelFileViewModel> filtered;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List.Where(c => c.Contributor.PersonID.ToLower().Contains(param.sSearch.ToLower())
                                 || c.SalaryAmount.ToString().ToLower().Contains(param.sSearch.ToLower())
                                 || c.ContributorContribution.ToString().ToLower().Contains(param.sSearch.ToLower())
                                 || c.EmployerContribution.ToString().ToLower().Contains(param.sSearch.ToLower())
                                 || (c.Contributor.FirstName + " " + c.Contributor.MidName + " " + c.Contributor.LastName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
                                 //|| c.Contributor.MidName.ToLower().Contains(param.sSearch.ToLower())
                                 //|| c.Contributor.LastName.ToLower().Contains(param.sSearch.ToLower())
                                 );

            }
            else
            {
                filtered = List;
            }
            //Sorting through column index
            //var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            //Func<ExcelFileViewModel, string> orderingFunction = (c => sortColumnIndex == 0 ? c.PersonID.ToString() :
            //                                                            sortColumnIndex == 1 ? c.ContributorName :
            //                                                            sortColumnIndex == 2 ? c.SalaryAmount :
            //                                                            sortColumnIndex == 4 ? c.ContributorContribution :
            //                                                            sortColumnIndex == 6 ? c.EmployerContribution :

            //                                                                                "");

            //var sortDirection = Request["sSortDir_0"]; // asc or desc
            //if (sortDirection == "asc")
            //  filtered = filtered.OrderBy(orderingFunction);
            //else
            //  filtered = filtered.OrderByDescending(orderingFunction);

            //Pagging
            var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            ////Select required columns
            var result = from c in displayed
                         select new[] {
                         c.Contributor.PersonID,
                         c.Month,
                         c.Year+"",
                         (c.Contributor.SalaryAmount/12).ToString("#,##0.00"),
                        Math.Round(((c.Contributor.SalaryAmount/1200) * c.Contributor.PFRate),2).ToString("#,##0.00"),
                        Math.Round(((c.Contributor.SalaryAmount/1200) * c.Contributor.Employer.PFRate),2).ToString("#,##0.00"),
                        Convert.ToDecimal(c.Contributor.SalaryAmount).ToString("#,##0.00"),
                     Convert.ToDecimal(c.ContributorContribution).ToString("#,##0.00"),
                     Convert.ToDecimal(c.EmployerContribution).ToString("#,##0.00")
                   };

            return Json(
                                        new
                                        {
                                            sEcho = param.sEcho,
                                            iTotalRecords = List.Count(),
                                            iTotalDisplayRecords = filtered.Count(),
                                            aaData = result
                                        },
          JsonRequestBehavior.AllowGet);
        }

        public ActionResult _ContributorDetails()
        {
            return PartialView("_ContributorDetails");
        }
        public ActionResult ContributorAjaxHandler(App.Web.Models.JQueryDataTableParamModel param, int Id)
        {
            List<MasterContributor> list = new List<MasterContributor>();
            var model = _DbContext.MasterContributor.Where(x => x.IsActive == true && x.JobStatusID == 1 && x.EmployerID == Id).Select(x => new { x.ExpectedRetirementDate, x.Id }).ToList();
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
            IEnumerable<MasterContributor> filtered;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = list
                   .Where(c => c.PersonID.ToLower().Contains(param.sSearch.ToLower())
                   || (c.FirstName + " " + c.MidName + " " + c.LastName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
                   //|| c.MidName.ToLower().Contains(param.sSearch.ToLower())
                   //|| c.LastName.ToLower().Contains(param.sSearch.ToLower())
                   || c.Employer.EmployerName.ToLower().Contains(param.sSearch.ToLower())
                   || c.DateOfBirth.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.Mobile.ToLower().Contains(param.sSearch.ToLower())
                   || c.ExpectedRetirementDate.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.SocialSecurityNo.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filtered = list;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MasterContributor, string> orderingFunction = (c => sortColumnIndex == 0 ? c.PersonID :
                                                                                            sortColumnIndex == 1 ? c.FirstName + "" :
                                                                                            sortColumnIndex == 2 ? c.Employer.EmployerName :
                                                                                            sortColumnIndex == 3 ? c.DateOfBirth + "" :
                                                                                            sortColumnIndex == 4 ? c.Mobile :
                                                                                            sortColumnIndex == 5 ? c.ExpectedRetirementDate + "" :
                                                                                            sortColumnIndex == 6 ? c.SocialSecurityNo + "" :
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
                         c.FirstName+" "+c.MidName+" "+c.LastName,
                         c.Employer.EmployerName,
                         c.SocialSecurityNo,
                         c.Gender,
                         String.Format("{0:MM/dd/yyyy}", c.DateOfBirth),
                         c.Mobile,
                         String.Format("{0:MM/dd/yyyy}", c.ExpectedRetirementDate),
                         c.Id+""
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


    }
}
