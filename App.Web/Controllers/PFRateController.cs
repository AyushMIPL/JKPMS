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
using App.Data.ViewModels;
namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class PFRateController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        //private AppDbContext _db = new AppDbContext();
        //private AppDbContext _dbNew = new AppDbContext();

        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;
        private AppDbContext _db;
        private AppDbContext _dbNew;

        public PFRateController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _dbNew = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // GET: PFPercentages
        public ActionResult Index()
        {
            return View(db.MasterPFRate.ToList());
        }
        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var List = db.MasterPFRate.Where(x => x.IsActive == true).OrderByDescending(x => x.PfType);
            IEnumerable<MasterPFRate> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.Name.ToLower().Contains(param.sSearch.ToLower())
                   || c.PFRate.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.PfType.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.EffectiveDate.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || c.EffectiveEndDate.ToString().ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MasterPFRate, string> orderingFunction = (c => sortColumnIndex == 0 ? c.PfType :
                                                                                            //sortColumnIndex == 1 ? c.PFRate.ToString() :
                                                                                            //sortColumnIndex == 2 ? c.PfType.ToString() :
                                                                                            //      sortColumnIndex == 3 ? c.EffectiveDate.ToString() :
                                                                                            //      sortColumnIndex == 4 ? c.EffectiveEndDate.ToString() :
                                                                                            //                sortColumnIndex == 5 ? c.IsActive + "" :
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

                         c.Name,
                         c.PFRate.ToString(),
                         String.Format("{0:MM/dd/yyyy}", c.EffectiveDate),
                     String.Format("{0:MM/dd/yyyy}", c.EffectiveEndDate),
                     c.PfType+"",
                     c.IsActive + "",
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
        // GET: PFRate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterPFRate pFPercentage = db.MasterPFRate.Find(id);
            if (pFPercentage == null)
            {
                return HttpNotFound();
            }
            return View(pFPercentage);
        }

        // GET: PFRate/Create
        public ActionResult Create()
        {
            var PfType = Enum.GetValues(typeof(PfType)).Cast<PfType>().Select(v => new SelectListItem
            {
                Text = ((char)v).ToString(),
                Value = v.ToString().Replace("_", " ")

            });
            ViewBag.PfType = new SelectList(PfType, "Text", "Value");
            return View();
        }
        public enum PfType
        {
            Employer = 'E',
            Contributor = 'C'
        }

        // POST: PFRate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PFRate,PfType,EffectiveDate,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive,EffectiveEndDate")] MasterPFRate pFPercentage)
        {
            var PfType = Enum.GetValues(typeof(PfType)).Cast<PfType>().Select(v => new SelectListItem
            {
                Text = ((char)v).ToString(),
                Value = v.ToString().Replace("_", " ")

            });
            ViewBag.PfType = new SelectList(PfType, "Text", "Value", pFPercentage.PfType);
            if (pFPercentage.EffectiveEndDate < pFPercentage.EffectiveDate)
            {
                TempData["error"] = "Effective End Date Should Not Be Smaller Than Effective Start Date";
                return View(pFPercentage);
            }
            var verify = db.MasterPFRate.Where(x => x.Name == pFPercentage.Name && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { TempData["error"] = "This PF Rate already exist.."; }
            else
            {
                if (ModelState.IsValid)
                {
                    pFPercentage.IsActive = true;
                    db.MasterPFRate.Add(pFPercentage);
                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        TempData["success"] = "Record Saved Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "There is Some error. Please try again later";
                    }
                }
            }
            return View(pFPercentage);
        }

        // GET: PFRate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterPFRate pFPercentage = db.MasterPFRate.Find(id);
            var PfType = Enum.GetValues(typeof(PfType)).Cast<PfType>().Select(v => new SelectListItem
            {
                Text = ((char)v).ToString(),
                Value = v.ToString().Replace("_", " ")

            });
            ViewBag.PfType = new SelectList(PfType, "Text", "Value", pFPercentage.PfType);

            var employer = db.MasterEmployer.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, EmployerName = x.EmployerName }).OrderBy(x => x.EmployerName).ToList();
            employer.Insert(0, new { Id = 0, EmployerName = "All" });
            var jobDetails = db.MasterStatus.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = x.Name }).OrderBy(x => x.Name).ToList();
            jobDetails.Insert(0, new { Id = 0, Name = "All" });
            jobDetails.Add(new { Id = 8, Name = "In-Active" });
            ViewBag.EmployerName = new SelectList(employer, "Id", "EmployerName", 2);
            ViewBag.JobStatusID = new SelectList(jobDetails, "Id", "Name", 1);
            if (pFPercentage.PfType == "E")
            {
                var list = db.MasterPFRate.Where(x => x.IsActive == true).Where(x => x.PfType == "E").OrderBy(x => x.Name).ToList().Select(x => new { Id = x.Id, Value = string.Format("{0} - ({1} - {2})", x.Name, "PFRate @" + x.PFRate, " Effective Date :" + x.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + x.EffectiveEndDate.ToString("dd MMMM, yyyy")) }).ToList();
                ViewBag.PFRateID = new SelectList(list, "Id", "Value");
                ViewBag.PfRateIdContributor = new SelectList(list, "Id", "Value");
            }
            else
            {
                var list = db.MasterPFRate.Where(x => x.IsActive == true).Where(x => x.PfType == "C").OrderBy(x => x.Name).ToList().Select(x => new { Id = x.Id, Value = string.Format("{0} - ({1} - {2})", x.Name, "PFRate @" + x.PFRate, " Effective Date :" + x.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + x.EffectiveEndDate.ToString("dd MMMM, yyyy")) }).ToList();
                ViewBag.PFRateID = new SelectList(list, "Id", "Value");
                ViewBag.PfRateIdContributor = new SelectList(list, "Id", "Value");
            }
            if (pFPercentage == null)
            {
                return HttpNotFound();
            }
            return View(pFPercentage);
        }
        public ActionResult BindEmployerAjaxHandler(JQueryDataTableParamModel param, int? PfRateId)
        {
            var CurrentDate = DateTime.Now;
            int monthEndDate = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            DateTime date = Convert.ToDateTime(CurrentDate.Month + "/" + monthEndDate + "/" + CurrentDate.Year);
            var List = new List<EmployerViewModel>();
            if (PfRateId != null && PfRateId != 0)
            {
                List = (from me in db.MasterEmployer.Where(x => x.IsActive == true)
                        join pf in db.MasterPFRate on me.PFRateID equals pf.Id
                        join meh in db.MasterEmployerPFRateDetails.Where(x => x.IsActive == true && x.PFRateID == (int)PfRateId)
                        on me.UniqueID equals meh.UniqueID
                        into rt
                        from finalize in rt.DefaultIfEmpty()
                        select new EmployerViewModel
                        {
                            Id = me.Id,
                            UniqueID = me.UniqueID,
                            EmployerName = me.EmployerName,
                            EmployerAddress = me.EmployerAddress,
                            CityName = me.City.Name,
                            POBoxNo = me.POBoxNo,
                            ContactPerson = me.ContactPerson,
                            Mobile = me.Mobile,
                            PFRate = me.PFRate,
                            IsPfApplied = finalize == null ? false : true,
                            EffectiveStartDate = pf.EffectiveDate,
                            EffectiveEndDate = pf.EffectiveEndDate,
                            IsExpired = pf.EffectiveEndDate < date ? true : false,

                        }).ToList();
            }
            else
            {
                List = (from me in db.MasterEmployer.Where(x => x.IsActive == true)
                        join pf in db.MasterPFRate on me.PFRateID equals pf.Id
                        select new EmployerViewModel
                        {
                            Id = me.Id,
                            UniqueID = me.UniqueID,
                            EmployerName = me.EmployerName,
                            EmployerAddress = me.EmployerAddress,
                            CityName = me.City.Name,
                            POBoxNo = me.POBoxNo,
                            ContactPerson = me.ContactPerson,
                            Mobile = me.Mobile,
                            PFRate = me.PFRate,
                            IsPfApplied = false,
                            EffectiveStartDate = pf.EffectiveDate,
                            EffectiveEndDate = pf.EffectiveEndDate,
                            IsExpired = pf.EffectiveEndDate < date ? true : false,
                        }).ToList();
            }
            //if (IsApplied)
            //  List = List.Where(x => x.IsPfApplied).ToList();
            //else
            //  List = List.Where(x => x.IsPfApplied == false).ToList();

            //var List = db.MasterEmployer.Where(x => x.IsActive == true);
            IEnumerable<EmployerViewModel> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.UniqueID.ToLower().Contains(param.sSearch.ToLower())
                || c.EmployerName.ToLower().Contains(param.sSearch.ToLower())
                   || c.EmployerAddress.ToLower().Contains(param.sSearch.ToLower())
                   //|| c.CityName.ToLower().Contains(param.sSearch.ToLower())
                   || c.POBoxNo.ToLower().Contains(param.sSearch.ToLower())
                   || c.ContactPerson.ToLower().Contains(param.sSearch.ToLower())
                   || c.Mobile.ToLower().Contains(param.sSearch.ToLower())
                   || c.PFRate.ToString().ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<EmployerViewModel, string> orderingFunction = (c => sortColumnIndex == 0 ? c.UniqueID :
                                                                    sortColumnIndex == 1 ? c.EmployerName :
                                                                   sortColumnIndex == 2 ? c.EmployerAddress :
                                                                   //sortColumnIndex == 3 ? c.CityName :
                                                                   sortColumnIndex == 4 ? c.POBoxNo :
                                                                   sortColumnIndex == 5 ? c.ContactPerson :
                                                                   sortColumnIndex == 6 ? c.Mobile :
                                                                   sortColumnIndex == 7 ? c.PFRate.ToString() :
                                                                                            sortColumnIndex == 8 ? c.IsPfApplied + "" :
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
                         c.UniqueID,
                         c.EmployerName,
                         c.EmployerAddress,
                         //c.CityName,
                         c.POBoxNo,
                         c.ContactPerson,
                         c.Mobile,
                         c.PFRate.ToString(),
                     c.IsPfApplied + "",
                     c.Id + "",
                     c.IsPfApplied + "",
                     String.Format("{0:MM/dd/yyyy}", c.EffectiveStartDate),
                     String.Format("{0:MM/dd/yyyy}", c.EffectiveEndDate),
                     c.IsExpired+"",
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
        public ActionResult BindContributorAjaxHandler(JQueryDataTableParamModel param, int? Id, int? JobStatusID, int? PfRateId)
        {
            var CurrentDate = DateTime.Now;
            int monthEndDate = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            DateTime date = Convert.ToDateTime(CurrentDate.Month + "/" + monthEndDate + "/" + CurrentDate.Year);
            var List = new List<ContributorViewModel>();
            if (PfRateId != null && PfRateId != 0)
            {
                List = (from me in db.MasterContributor.Where(x => x.IsActive == true)
                        join pf in db.MasterPFRate on me.PFRateID equals pf.Id
                        join meh in db.MasterContributorPFRateDetails.Where(x => x.IsActive == true && x.PFRateID == (int)PfRateId)
                        on me.PersonID equals meh.PersonID
                        into rt
                        from finalize in rt.DefaultIfEmpty()
                        select new ContributorViewModel
                        {
                            Id = me.Id,
                            PersonID = me.PersonID,
                            ProfilePath = me.ProfilePath,
                            OldPersonID = me.OldPersonID,
                            FullName = me.FirstName + " " + me.MidName + " " + me.LastName,
                            EmployerName = me.Employer.EmployerName,
                            SocialSecurityNo = me.SocialSecurityNo,
                            DateOfBirth = me.DateOfBirth,
                            PostalAddress = me.PostalAddress,
                            Phone = me.Phone,
                            PhoneOffice = me.PhoneOffice,
                            IsActive = me.IsActive,
                            IsPfApplied = finalize == null ? false : true,
                            EmployerID = me.EmployerID,
                            JobStatusID = me.JobStatusID,
                            PFRate = me.PFRate,
                            EffectiveStartDate = pf.EffectiveDate,
                            EffectiveEndDate = pf.EffectiveEndDate,
                            IsExpired = pf.EffectiveEndDate < date ? true : false,
                        }).OrderBy(x => x.Id).ToList();
            }
            else
            {
                List = (from me in db.MasterContributor.Where(x => x.IsActive == true)
                        join pf in db.MasterPFRate on me.PFRateID equals pf.Id
                        select new ContributorViewModel
                        {
                            Id = me.Id,
                            PersonID = me.PersonID,
                            ProfilePath = me.ProfilePath,
                            OldPersonID = me.OldPersonID,
                            FullName = me.FirstName + " " + me.MidName + " " + me.LastName,
                            EmployerName = me.Employer.EmployerName,
                            SocialSecurityNo = me.SocialSecurityNo,
                            DateOfBirth = me.DateOfBirth,
                            PostalAddress = me.PostalAddress,
                            Phone = me.Phone,
                            PhoneOffice = me.PhoneOffice,
                            IsActive = me.IsActive,
                            IsPfApplied = false,
                            EmployerID = me.EmployerID,
                            JobStatusID = me.JobStatusID,
                            PFRate = me.PFRate,
                            EffectiveStartDate = pf.EffectiveDate,
                            EffectiveEndDate = pf.EffectiveEndDate,
                            IsExpired = pf.EffectiveEndDate < date ? true : false,
                        }).OrderBy(x => x.Id).ToList();
            }
            //IEnumerable<MasterContributor> List = db.MasterContributor.ToList();
            IEnumerable<ContributorViewModel> filtered;

            if (!string.IsNullOrEmpty(Id.ToString()) && Id != 0)
            {
                List = List.Where(x => x.EmployerID == Id).ToList();
            }
            if (!string.IsNullOrEmpty(JobStatusID.ToString()) && JobStatusID != 0 && JobStatusID != 8)
            {
                List = List.Where(x => x.JobStatusID == JobStatusID && x.IsActive == true).ToList();
            }
            else if (JobStatusID == 8)
            {
                List = List.Where(x => x.IsActive == false).ToList();
            }
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.PersonID.ToLower().Contains(param.sSearch.ToLower())
                   || (c.OldPersonID == null ? "" : c.OldPersonID).ToLower().Contains(param.sSearch.ToLower())
                   || (c.FullName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
                   || c.EmployerName.ToLower().Contains(param.sSearch.ToLower())
                   || c.SocialSecurityNo.ToLower().Contains(param.sSearch.ToLower())
                   || c.DateOfBirth.ToString().ToLower().Contains(param.sSearch.ToLower())
                   || (c.PostalAddress == null ? "" : c.PostalAddress).ToLower().Contains(param.sSearch.ToLower()));
                //|| (c.PhoneOffice == null ? "" : c.PhoneOffice).ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<ContributorViewModel, string> orderingFunction = (c => sortColumnIndex == 0 ? c.FullName :
                                                                                            sortColumnIndex == 1 ? c.EmployerName :
                                                                                            sortColumnIndex == 2 ? c.PersonID :
                                                                                            sortColumnIndex == 3 ? c.OldPersonID :
                                                                                            sortColumnIndex == 4 ? c.DateOfBirth + "" :
                                                                                            sortColumnIndex == 5 ? c.SocialSecurityNo :
                                                                                            sortColumnIndex == 6 ? c.PostalAddress :
                                                                                            //sortColumnIndex == 7 ? c.PhoneOffice :
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
                     c.ProfilePath,
                         c.PersonID,
                         c.OldPersonID,
                                         c.FullName,
                                         c.EmployerName,
                                         c.SocialSecurityNo,
                     c.Gender,
                     String.Format("{0:MM/dd/yyyy}", c.DateOfBirth),
                     c.PostalAddress,
                     c.Phone,
                     c.PhoneOffice,
                     //c.IsActive==false?"In-Active":c.JobStatusID+"",
                     c.IsPfApplied+"",
                     c.Id+"",
                     c.IsPfApplied+"",
                     c.PFRate+"",
                     String.Format("{0:MM/dd/yyyy}", c.EffectiveStartDate),
                     String.Format("{0:MM/dd/yyyy}", c.EffectiveEndDate),
                     c.IsExpired+"",
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
        // POST: PFRate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PFRate,PfType,EffectiveDate,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive,EffectiveEndDate")] MasterPFRate pFPercentage)
        {
            bool isEndDateChanged = false;
            var verify = db.MasterPFRate.Where(x => x.Name == pFPercentage.Name && x.Id != pFPercentage.Id && x.IsActive == true).FirstOrDefault();
            if (verify != null)
            { TempData["error"] = "This PF Rate already exist.."; }
            else
            {
                var PfType = Enum.GetValues(typeof(PfType)).Cast<PfType>().Select(v => new SelectListItem
                {
                    Text = ((char)v).ToString(),
                    Value = v.ToString().Replace("_", " ")

                });
                ViewBag.PfType = new SelectList(PfType, "Text", "Value", pFPercentage.PfType);

                var employer = db.MasterEmployer.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, EmployerName = x.EmployerName }).OrderBy(x => x.EmployerName).ToList();
                employer.Insert(0, new { Id = 0, EmployerName = "All" });
                var jobDetails = db.MasterStatus.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = x.Name }).OrderBy(x => x.Name).ToList();
                jobDetails.Insert(0, new { Id = 0, Name = "All" });
                jobDetails.Add(new { Id = 8, Name = "In-Active" });
                ViewBag.EmployerName = new SelectList(employer, "Id", "EmployerName", 2);
                ViewBag.JobStatusID = new SelectList(jobDetails, "Id", "Name", 1);
                if (pFPercentage.PfType == "E")
                {
                    var list = (from pf in db.MasterPFRate
                                where pf.PfType == "E"
                                select new PfRateViewModel
                                {
                                    Id = pf.Id,
                                    Value = pf.Name + " - (PFRate @" + pf.PFRate + " - Effective Date :",//string.Format("{0} - ({1} - {2})", pf.Name, "PFRate @" + pf.PFRate, " Effective Date :" + pf.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + pf.EffectiveEndDate.ToString("dd MMMM, yyyy")),
                                    EffectiveDate = pf.EffectiveDate,
                                    EffectiveEndDate = pf.EffectiveEndDate,
                                }).ToList();
                    foreach (var item in list)
                    {
                        item.Value = item.Value + item.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + item.EffectiveEndDate.ToString("dd MMMM, yyyy") + ")";
                    }
                    //var list = db.MasterPFRate.Where(x => x.IsActive == true).Where(x => x.PfType == "E").OrderBy(x => x.Name).ToList().Select(x => new { Id = x.Id, Value = string.Format("{0} - ({1} - {2})", x.Name, "PFRate @" + x.PFRate, " Effective Date :" + x.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + x.EffectiveEndDate.ToString("dd MMMM, yyyy")) }).ToList();
                    ViewBag.PFRateID = new SelectList(list, "Id", "Value");
                    ViewBag.PfRateIdContributor = new SelectList(list, "Id", "Value");
                }
                else
                {
                    var list = (from pf in db.MasterPFRate
                                where pf.PfType == "C"
                                select new PfRateViewModel
                                {
                                    Id = pf.Id,
                                    Value = pf.Name + " - (PFRate @" + pf.PFRate + " - Effective Date :",//string.Format("{0} - ({1} - {2})", pf.Name, "PFRate @" + pf.PFRate, " Effective Date :" + pf.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + pf.EffectiveEndDate.ToString("dd MMMM, yyyy")),
                                    EffectiveDate = pf.EffectiveDate,
                                    EffectiveEndDate = pf.EffectiveEndDate,
                                }).ToList();
                    foreach (var item in list)
                    {
                        item.Value = item.Value + item.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + item.EffectiveEndDate.ToString("dd MMMM, yyyy") + ")";
                    }
                    //var list = db.MasterPFRate.Where(x => x.IsActive == true).Where(x => x.PfType == "C").OrderBy(x => x.Name).ToList().Select(x => new { Id = x.Id, Value = string.Format("{0} - ({1} - {2})", x.Name, "PFRate @" + x.PFRate, " Effective Date :" + x.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + x.EffectiveEndDate.ToString("dd MMMM, yyyy")) }).ToList();
                    ViewBag.PFRateID = new SelectList(list, "Id", "Value");
                    ViewBag.PfRateIdContributor = new SelectList(list, "Id", "Value");
                }
                if (pFPercentage.EffectiveEndDate < pFPercentage.EffectiveDate)
                {
                    TempData["error"] = "Effective End Date Should Not Be Smaller Than Effective Start Date";
                    return View(pFPercentage);
                }
                //cannot change pf rate if contribution added
                var MasterPf = (from c in db.MasterPFRate
                                where c.Id == pFPercentage.Id
                                select new { c.PFRate, c.EffectiveDate, c.EffectiveEndDate }).FirstOrDefault();

                var IsEmployerContributionFound = db.ContributonSheetDetails.Any(x => x.IsActive && x.EmplrPFRateId == pFPercentage.Id);
                var IsContributorContributionFound = db.ContributonSheetDetails.Any(x => x.IsActive && x.EmplPFRateId == pFPercentage.Id);
                if (MasterPf.PFRate != pFPercentage.PFRate)
                {
                    if (IsEmployerContributionFound || IsContributorContributionFound)
                    {
                        TempData["error"] = "Can't change Pf Rate. Pf rate already applied to Employer or Contributor.";
                        return View(pFPercentage);
                    }
                }
                //if effective end date changed
                if (MasterPf.EffectiveEndDate != pFPercentage.EffectiveEndDate)
                {
                    //get contribution details of particular pf rate id
                    if (pFPercentage.PfType == "E" && IsEmployerContributionFound)
                    {
                        var MaxYear = (int)db.ContributonSheetDetails.Where(x => x.IsActive && x.EmplrPFRateId == pFPercentage.Id).Max(x => x.Year);
                        var MaxMonth = (int)db.ContributonSheetDetails.Where(x => x.IsActive && x.EmplrPFRateId == pFPercentage.Id && x.Year == MaxYear).Max(x => x.Month);
                        int monthEndDate = DateTime.DaysInMonth(MaxYear, MaxMonth);
                        DateTime date = Convert.ToDateTime(MaxMonth + "/" + monthEndDate + "/" + MaxYear);
                        if (pFPercentage.EffectiveEndDate < date)
                        {
                            TempData["error"] = "Effective End Date Should Not Be Smaller Than " + date.ToString("dd MMMM, yyyy");
                            return View(pFPercentage);
                        }
                    }
                    if (pFPercentage.PfType == "C" && IsContributorContributionFound)
                    {
                        var MaxYear = (int)db.ContributonSheetDetails.Where(x => x.IsActive && x.EmplPFRateId == pFPercentage.Id).Max(x => x.Year);
                        var MaxMonth = (int)db.ContributonSheetDetails.Where(x => x.IsActive && x.EmplPFRateId == pFPercentage.Id && x.Year == MaxYear).Max(x => x.Month);
                        int monthEndDate = DateTime.DaysInMonth(MaxYear, MaxMonth);
                        DateTime date = Convert.ToDateTime(MaxMonth + "/" + monthEndDate + "/" + MaxYear);
                        if (pFPercentage.EffectiveEndDate < date)
                        {
                            TempData["error"] = "Effective End Date Should Not Be Smaller Than " + date.ToString("dd MMMM, yyyy");
                            return View(pFPercentage);
                        }
                    }
                    isEndDateChanged = true;
                }

                if (ModelState.IsValid)
                {
                    int result = 0;
                    using (AppDbContext db = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
                    {
                        pFPercentage.IsActive = true;
                        db.Entry(pFPercentage).State = EntityState.Modified;
                        result = db.SaveChanges();
                    }

                    if (result > 0)
                    {
                        //if Effective End Date Changed Update EffectiveEndDate in Employer and Contributor History Table
                        if (isEndDateChanged)
                        {
                            if (pFPercentage.PfType == "E")
                            {
                                using (AppDbContext _db = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
                                {
                                    var AllEmployerHistoryHavingPfRate = _db.MasterEmployerPFRateDetails.Where(x => x.IsActive && x.PFRateID == pFPercentage.Id).ToList();
                                    foreach (var EmployerHistory in AllEmployerHistoryHavingPfRate)
                                    {
                                        EmployerHistory.EffectiveEndDate = pFPercentage.EffectiveEndDate;
                                        _db.Entry(EmployerHistory).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                using (AppDbContext _db = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
                                {
                                    var AllContributorHistoryHavingPfRate = _db.MasterContributorPFRateDetails.Where(x => x.IsActive && x.PFRateID == pFPercentage.Id).ToList();
                                    foreach (var ContributorHistory in AllContributorHistoryHavingPfRate)
                                    {
                                        ContributorHistory.EffectiveEndDate = pFPercentage.EffectiveEndDate;
                                        _db.Entry(ContributorHistory).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }
                            }
                        }
                        TempData["success"] = "Record Saved Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "There is Some error. Please try again later";
                    }
                }
            }

            return View(pFPercentage);
        }

        // GET: PFRate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterPFRate pFPercentage = db.MasterPFRate.Find(id);
            if (pFPercentage == null)
            {
                return HttpNotFound();
            }
            return View(pFPercentage);
        }

        // POST: PFRate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterPFRate pFPercentage = db.MasterPFRate.Find(id);
            db.MasterPFRate.Remove(pFPercentage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult Remove(int Id, string name)
        {
            int result = 0;
            if (Id != 0)
            {
                bool action = MasterDeleteRepo.AjaxDelete(Id, name);
                if (action)
                {
                    MasterPFRate entity = db.MasterPFRate.Where(x => x.Id == Id).FirstOrDefault();
                    entity.IsActive = false;
                    db.Entry(entity).State = EntityState.Modified;
                    result = db.SaveChanges();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(2, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ManageEmployerPfHistoryAjax(string UniqueIdsString, int PfRateId)
        {
            var UniqueIds = UniqueIdsString.Split(',');
            if (UniqueIds.Count() == 0)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var MasterPfRate = db.MasterPFRate.Where(x => x.IsActive && x.Id == PfRateId).FirstOrDefault();
                foreach (var uniqueid in UniqueIds)
                {
                    //update employer pf rate
                    var Employer = db.MasterEmployer.Where(x => x.IsActive && x.UniqueID == uniqueid).FirstOrDefault();
                    if (Employer != null)
                    {
                        Employer.PFRateID = PfRateId;
                        Employer.PFRate = MasterPfRate.PFRate;
                        db.Entry(Employer).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    //insert or update employer pf history
                    var EmployerPfRateHistorry = db.MasterEmployerPFRateDetails.Where(x => x.IsActive && x.UniqueID == uniqueid && x.PFRateID == PfRateId).FirstOrDefault();
                    if (EmployerPfRateHistorry == null)
                    {
                        var MasterEmployerPfRateDetails = new MasterEmployerPFRateDetails();
                        MasterEmployerPfRateDetails.UniqueID = uniqueid;
                        MasterEmployerPfRateDetails.PFRateID = PfRateId;
                        MasterEmployerPfRateDetails.PFRate = MasterPfRate.PFRate;
                        MasterEmployerPfRateDetails.EffectiveDate = MasterPfRate.EffectiveDate;
                        MasterEmployerPfRateDetails.EffectiveEndDate = MasterPfRate.EffectiveEndDate;
                        MasterEmployerPfRateDetails.IsActive = true;
                        db.MasterEmployerPFRateDetails.Add(MasterEmployerPfRateDetails);
                        db.SaveChanges();
                    }
                    else
                    {
                        EmployerPfRateHistorry.PFRateID = PfRateId;
                        EmployerPfRateHistorry.PFRate = MasterPfRate.PFRate;
                        EmployerPfRateHistorry.EffectiveDate = MasterPfRate.EffectiveDate;
                        EmployerPfRateHistorry.EffectiveEndDate = MasterPfRate.EffectiveEndDate;
                        db.Entry(EmployerPfRateHistorry).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            //return Json("1", JsonRequestBehavior.AllowGet);
        }
        public JsonResult ManageAllEmployerPfHistoryAjax(int PfRateId)
        {
            if (PfRateId == 0)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var MasterPfRate = db.MasterPFRate.Where(x => x.IsActive && x.Id == PfRateId).FirstOrDefault();
                var AllEmployer = db.MasterEmployer.Where(x => x.IsActive).ToList();
                foreach (var employer in AllEmployer)
                {
                    var EmployerPfRateHistorry = db.MasterEmployerPFRateDetails.Where(x => x.IsActive && x.UniqueID == employer.UniqueID && x.PFRateID == PfRateId).FirstOrDefault();
                    if (EmployerPfRateHistorry == null)
                    {
                        var MasterEmployerPfRateDetails = new MasterEmployerPFRateDetails();
                        MasterEmployerPfRateDetails.UniqueID = employer.UniqueID;
                        MasterEmployerPfRateDetails.PFRateID = PfRateId;
                        MasterEmployerPfRateDetails.PFRate = MasterPfRate.PFRate;
                        MasterEmployerPfRateDetails.EffectiveDate = MasterPfRate.EffectiveDate;
                        MasterEmployerPfRateDetails.EffectiveEndDate = MasterPfRate.EffectiveEndDate;
                        MasterEmployerPfRateDetails.IsActive = true;
                        db.MasterEmployerPFRateDetails.Add(MasterEmployerPfRateDetails);
                        db.SaveChanges();
                    }
                    else
                    {
                        EmployerPfRateHistorry.PFRateID = PfRateId;
                        EmployerPfRateHistorry.PFRate = MasterPfRate.PFRate;
                        EmployerPfRateHistorry.EffectiveDate = MasterPfRate.EffectiveDate;
                        EmployerPfRateHistorry.EffectiveEndDate = MasterPfRate.EffectiveEndDate;
                        db.Entry(EmployerPfRateHistorry).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return Json("1", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ManageContributorPfHistoryAjax(string PersonIdsString, int PfRateId)
        {
            var PersonIds = PersonIdsString.Split(',');
            if (PersonIds.Count() == 0)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var MasterPfRate = db.MasterPFRate.Where(x => x.IsActive && x.Id == PfRateId).FirstOrDefault();
                foreach (var personid in PersonIds)
                {
                    //update mastercontributor
                    using (AppDbContext db = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
                    {
                        var Contributor = db.MasterContributor.Where(x => x.PersonID == personid).FirstOrDefault();
                        if (Contributor != null)
                        {
                            Contributor.PFRateID = PfRateId;
                            Contributor.PFRate = MasterPfRate.PFRate;
                            db.Entry(Contributor).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    //insert or update contributor pf history
                    var ContributorPfRateHistorry = _db.MasterContributorPFRateDetails.Where(x => x.PersonID == personid && x.PFRateID == PfRateId).FirstOrDefault();
                    if (ContributorPfRateHistorry == null)
                    {
                        var MasterContributorPfRateDetails = new MasterContributorPFRateDetails();
                        MasterContributorPfRateDetails.PersonID = personid;
                        MasterContributorPfRateDetails.PFRateID = PfRateId;
                        MasterContributorPfRateDetails.PFRate = MasterPfRate.PFRate;
                        MasterContributorPfRateDetails.EffectiveDate = MasterPfRate.EffectiveDate;
                        MasterContributorPfRateDetails.EffectiveEndDate = MasterPfRate.EffectiveEndDate;
                        MasterContributorPfRateDetails.IsActive = true;
                        _db.MasterContributorPFRateDetails.Add(MasterContributorPfRateDetails);
                        _db.SaveChanges();
                    }
                    else
                    {
                        ContributorPfRateHistorry.PFRateID = PfRateId;
                        ContributorPfRateHistorry.PFRate = MasterPfRate.PFRate;
                        ContributorPfRateHistorry.EffectiveDate = MasterPfRate.EffectiveDate;
                        ContributorPfRateHistorry.EffectiveEndDate = MasterPfRate.EffectiveEndDate;
                        _db.Entry(ContributorPfRateHistorry).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            //return Json("1", JsonRequestBehavior.AllowGet);
        }
        public JsonResult ManageAllContributorPfHistoryAjax(int PfRateId, int? Id, int? JobStatusID)
        {
            if (PfRateId == 0)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var MasterPfRate = db.MasterPFRate.Where(x => x.IsActive && x.Id == PfRateId).FirstOrDefault();
                var AllContriButor = db.MasterContributor.ToList();
                if (!string.IsNullOrEmpty(Id.ToString()) && Id != 0)
                {
                    AllContriButor = AllContriButor.Where(x => x.EmployerID == Id).ToList();
                }
                if (!string.IsNullOrEmpty(JobStatusID.ToString()) && JobStatusID != 0 && JobStatusID != 8)
                {
                    AllContriButor = AllContriButor.Where(x => x.JobStatusID == JobStatusID && x.IsActive == true).ToList();
                }
                else if (JobStatusID == 8)
                {
                    AllContriButor = AllContriButor.Where(x => x.IsActive == false).ToList();
                }
                foreach (var ContriButor in AllContriButor)
                {
                    //update mastercontributor
                    using (AppDbContext db = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
                    {
                        var Contributors = db.MasterContributor.Where(x => x.PersonID == ContriButor.PersonID).FirstOrDefault();
                        if (Contributors != null)
                        {
                            ModelState.Remove("DateOfBirth");
                            Contributors.PFRateID = PfRateId;
                            Contributors.PFRate = MasterPfRate.PFRate;
                            db.Entry(Contributors).State = EntityState.Modified;

                            db.SaveChanges();
                        }
                    }
                    var PersonId = ContriButor.PersonID;
                    //update contributor Pf Rate
                    var ContributorPfRateHistorry = _db.MasterContributorPFRateDetails.Where(x => x.PersonID == PersonId && x.PFRateID == PfRateId).FirstOrDefault();
                    if (ContributorPfRateHistorry == null)
                    {
                        MasterContributorPFRateDetails MasterContributorPfRateDetails = new MasterContributorPFRateDetails();
                        MasterContributorPfRateDetails.PersonID = PersonId;
                        MasterContributorPfRateDetails.PFRateID = PfRateId;
                        MasterContributorPfRateDetails.PFRate = MasterPfRate.PFRate;
                        MasterContributorPfRateDetails.EffectiveDate = MasterPfRate.EffectiveDate;
                        MasterContributorPfRateDetails.EffectiveEndDate = MasterPfRate.EffectiveEndDate;
                        MasterContributorPfRateDetails.IsActive = true;
                        _db.MasterContributorPFRateDetails.Add(MasterContributorPfRateDetails);
                        _db.SaveChanges();
                    }
                    else
                    {
                        ContributorPfRateHistorry.PFRateID = PfRateId;
                        ContributorPfRateHistorry.PFRate = MasterPfRate.PFRate;
                        ContributorPfRateHistorry.EffectiveDate = MasterPfRate.EffectiveDate;
                        ContributorPfRateHistorry.EffectiveEndDate = MasterPfRate.EffectiveEndDate;
                        _db.Entry(ContributorPfRateHistorry).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
                return Json("1", JsonRequestBehavior.AllowGet);
            }
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
