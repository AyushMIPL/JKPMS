using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App.Data;
using App.Data.Entities;
using System.Web.Mvc.Ajax;
using System.Threading.Tasks;
using App.Data.ViewModels;
using System.IO;
using LinqToExcel;
using App.Data.Extentions;
using App.Web.Models;
using App.Web.Filters;
using System.Text.RegularExpressions;
using System.Globalization;
using JKPS.COMMON;
using JKPS.BLL;
using App.Web.Repository;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Threading;
using CsvHelper;
using System.Data.OleDb;
using System.Web.DynamicData;
using Postal;
using ClosedXML.Excel;
using Castle.Core.Internal;
using System.Reflection;
using App.Web.Helper;
using Microsoft.Owin;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Newtonsoft.Json;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Spatial;
using DocumentFormat.OpenXml.Office2013.Excel;
using System.Text;
using System.Reflection.Emit;
using CrystalDecisions.Shared.Json;
using System.Web.Http.Results;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using Row = DocumentFormat.OpenXml.Spreadsheet.Row;
using Cell = DocumentFormat.OpenXml.Spreadsheet.Cell;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Drawing;
using System.Windows.Documents;
using static App.Web.Helper.Helper;
using OfficeOpenXml;
using Microsoft.AspNetCore.Http;

namespace App.Web.Controllers
{
  [AuthorizeEx()]
  public class ContributorPersonalDetailsController : BaseController
  {
    private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
    private AppDbContext db;
    private AppDbContext _DbContext;
    private readonly int MaxFileSizeToUploadInMB;

    public ContributorPersonalDetailsController()
    {
      db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
      _DbContext = DbContext;
      MaxFileSizeToUploadInMB = int.TryParse(ConfigurationManager.AppSettings["MaxFileSizeToUploadInMB"], out var fileSize) ? fileSize : 50;
    }

    // GET: ContributorPersonalDetails
    public ActionResult Index(int? Id, int? jobStatusId)
    {
      if (Id == null)
      {
        Id = 2;
      }
      if (jobStatusId == null)
      {
        jobStatusId = 1;
      }
      var employer = db.MasterEmployer.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, EmployerName = x.EmployerName }).OrderBy(x => x.EmployerName).ToList();
      employer.Insert(0, new { Id = 0, EmployerName = "All" });
      var jobDetails = db.MasterStatus.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = x.Name }).OrderBy(x => x.Name).ToList();
      jobDetails.Insert(0, new { Id = 0, Name = "All" });
      jobDetails.Add(new { Id = 8, Name = "In-Active" });
      ViewBag.EmployerName = new SelectList(employer, "Id", "EmployerName", Id);
      ViewBag.JobStatusID = new SelectList(jobDetails, "Id", "Name", jobStatusId);
      return View();
    }
    //Index
    public ActionResult ContributorPersonalDetailsAjaxHandler(JQueryDataTableParamModel param, int? Id, int? JobStatusID)
    {
      var CurrentDate = DateTime.Now;
      int monthEndDate = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
      DateTime date = Convert.ToDateTime(CurrentDate.Month + "/" + monthEndDate + "/" + CurrentDate.Year);
      //IEnumerable<MasterContributor> List = db.MasterContributor.ToList();
      var List = (from me in db.MasterContributor.Where(x => x.IsActive == true)
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
                    Gender = me.Gender,
                    DateOfBirth = me.DateOfBirth,
                    PostalAddress = me.PostalAddress,
                    Phone = me.Phone,
                    PhoneOffice = me.PhoneOffice,
                    IsActive = me.IsActive,
                    EmployerID = me.EmployerID,
                    JobStatusID = me.JobStatusID,
                    PFRate = me.PFRate,
                    EffectiveStartDate = pf.EffectiveDate,
                    EffectiveEndDate = pf.EffectiveEndDate,
                    IsExpired = pf.EffectiveEndDate < date ? true : false,
                  }).ToList();
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
           //|| (c.FirstName + " " + c.MidName + " " + c.LastName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
           || (c.FullName.ToLower().Contains(param.sSearch.ToLower()))
           //|| (c.MidName == null ? "" : c.MidName).ToLower().Contains(param.sSearch.ToLower())
           //|| c.LastName.ToLower().Contains(param.sSearch.ToLower())
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

      Func<ContributorViewModel, string> orderingFunction = (c => sortColumnIndex == 0 ? c.FirstName :
                                                            sortColumnIndex == 1 ? c.MidName :
                                                            sortColumnIndex == 2 ? c.LastName :
                                                            sortColumnIndex == 3 ? c.EmployerName :
                                                            sortColumnIndex == 4 ? c.PersonID :
                                                            sortColumnIndex == 5 ? c.OldPersonID :
                                                            sortColumnIndex == 6 ? c.DateOfBirth + "" :
                                                            sortColumnIndex == 7 ? c.SocialSecurityNo :
                                                            sortColumnIndex == 8 ? c.PostalAddress :
                                                                                      //sortColumnIndex == 9 ? c.PhoneOffice :
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
                     //c.Phone,
                     //c.PhoneOffice,
                     c.IsActive==false?"In-Active":c.JobStatusID+"",
                     c.Id+"",
                     c.PFRate+"",
                     c.IsExpired+"",
                     String.Format("{0:MM/dd/yyyy}", c.EffectiveStartDate),
                     String.Format("{0:MM/dd/yyyy}", c.EffectiveEndDate),
                     c.IsActive==false?"In-Active":c.JobStatusID+"",
                     c.Id+"",

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
    //AddMarriageDetails
    public ActionResult ContributorMarriageDetailsAjaxHandler(JQueryDataTableParamModel param, int? Id)
    {
      var List = db.MasterContributor.Where(x => x.IsActive == true && x.JobStatusID == 1);
      IEnumerable<MasterContributor> filtered;

      if (!string.IsNullOrEmpty(Id.ToString()) && Id != 0)
      {
        List = List.Where(x => x.Id != Id);
      }
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
      var result = from c in displayed
                   select new[] {
                         c.PersonID,
                         c.OldPersonID,
                                         c.FirstName+" "+c.MidName+" "+c.LastName,
                                         c.Employer.EmployerName,
                                         c.SocialSecurityNo,
                     c.Gender,
                     String.Format("{0:MM/dd/yyyy}", c.DateOfBirth),
                     c.PostalAddress,
                     c.Phone,
                     c.PhoneOffice,
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

    //public JsonResult Remove(string Id)
    //{
    //  int result = 0;
    //  if (Id != "0")
    //  {
    //    int id = Convert.ToInt32(Id);
    //    MasterContributor entity = db.MasterContributor.Where(x => x.Id == id).FirstOrDefault();
    //    entity.IsActive = false;
    //    db.Entry(entity).State = EntityState.Modified;
    //    db.SaveChanges();
    //    result = 1;
    //  }
    //  return Json(result, JsonRequestBehavior.AllowGet);
    //}

    //Index
    public JsonResult changeStatusAjax(int? Id, int? jobstatus)
    {
      MasterContributor contributor = db.MasterContributor.Where(x => x.Id == Id).FirstOrDefault();
      contributor.JobStatusID = jobstatus;
      db.Entry(contributor).State = EntityState.Modified;
      int result = db.SaveChanges();
      return Json(result, JsonRequestBehavior.AllowGet);
    }
    //Index
    public JsonResult inActiveToActiveAjax(int? Id, int? status)
    {
      MasterContributor contributor = db.MasterContributor.Where(x => x.Id == Id).FirstOrDefault();
      contributor.IsActive = status == 1 ? true : false;
      db.Entry(contributor).State = EntityState.Modified;
      int result = db.SaveChanges();
      return Json(result, JsonRequestBehavior.AllowGet);
    }
    //public ActionResult AjaxHandler(JQueryDataTableParamModel param)
    //{
    //  var List = db.MasterContributor.Where(x=>x.JobStatusID==1);
    //  IEnumerable<MasterContributor> filtered;


    //  if (!string.IsNullOrEmpty(param.sSearch))
    //  {
    //    filtered = List
    //       .Where(c =>c.PersonID.ToLower().Contains(param.sSearch.ToLower())
    //       || c.Prefix.Name.ToLower().Contains(param.sSearch.ToLower())
    //       || c.FirstName.ToLower().Contains(param.sSearch.ToLower())
    //       || c.LastName.ToLower().Contains(param.sSearch.ToLower())
    //       || c.MidName.ToLower().Contains(param.sSearch.ToLower())
    //       || c.SocialSecurityNo.ToString().ToLower().Contains(param.sSearch.ToLower())
    //       || c.Gender.ToLower().Contains(param.sSearch.ToLower())
    //       || c.DateOfBirth.ToString().ToLower().Contains(param.sSearch.ToLower())
    //       || c.PostalAddress.ToLower().Contains(param.sSearch.ToLower())
    //       || c.Phone.ToLower().Contains(param.sSearch.ToLower())
    //       || c.PhoneOffice.ToLower().Contains(param.sSearch.ToLower()));

    //  }
    //  else
    //  {
    //    filtered = List;
    //  }

    //  //Sorting through column index
    //  var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

    //  Func<MasterContributor, string> orderingFunction = (c => sortColumnIndex == 0 ? c.PersonID :
    //                                                         sortColumnIndex == 1 ? c.Prefix.Name :
    //                                                         sortColumnIndex == 2 ? c.FullName :
    //                                                         sortColumnIndex == 3 ? c.SocialSecurityNo.ToString() :
    //                                                         sortColumnIndex == 4 ? c.Gender :
    //                                                         sortColumnIndex == 5 ? c.DateOfBirth.ToString() :
    //                                                         sortColumnIndex == 6 ? c.PostalAddress :
    //    //sortColumnIndex == 6 ? c.PermanentAddress :
    //                                                         sortColumnIndex == 7 ? c.Phone :
    //                                                         sortColumnIndex == 8 ? c.PhoneOffice :
    //    //sortColumnIndex == 9 ? c.Mobile :
    //    //sortColumnIndex == 10 ? c.Email :

    //                                                                                  sortColumnIndex == 8 ? c.IsActive + "" :
    //                                                                                  "");

    //  var sortDirection = Request["sSortDir_0"]; // asc or desc
    //  if (sortDirection == "asc")
    //    filtered = filtered.OrderBy(orderingFunction);
    //  else
    //    filtered = filtered.OrderByDescending(orderingFunction);

    //  //Pagging
    //  var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

    //  //Select required columns
    //  var result = from c in displayed
    //               select new[] {                       

    //                     c.PersonID,
    //                     (c.Prefix==null?"": c.Prefix.Name) +" "+c.FullName,
    //                     c.Employer.EmployerName,
    //                     c.SocialSecurityNo.ToString(),
    //                     c.Gender,
    //                     String.Format("{0:MM/dd/yyyy}", c.DateOfBirth),
    //                     c.PostalAddress,
    //                     c.Phone,
    //                     c.PhoneOffice,
    //                 c.IsActive + "", 
    //                 c.Id + ""
    //               };

    //  return Json(
    //                              new
    //                              {
    //                                sEcho = param.sEcho,
    //                                iTotalRecords = List.Count(),
    //                                iTotalDisplayRecords = filtered.Count(),
    //                                aaData = result
    //                              }, JsonRequestBehavior.AllowGet);
    //}


    // GET: ContributorPersonalDetails/Details/5
    //public ActionResult Details(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  MasterContributor contributorPersonalDetails = db.MasterContributor.Find(id);
    //  if (contributorPersonalDetails == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(contributorPersonalDetails);
    //}

    // GET: ContributorPersonalDetails/Create
    //public ActionResult Create()
    //{
    //  DoBind(null);
    //  return View();
    //}

    // POST: ContributorPersonalDetails/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //public ActionResult Create(MasterContributor model)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.MasterContributor.Add(model);
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //  }

    //  DoBind(model);
    //  return View(model);
    //}

    private void DoBind(MasterContributor model)
    {
      var clist = db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name);
      var setModel = model ?? new MasterContributor();
      //ViewBag.CityID = new SelectList(db.City, "Id", "Name", setModel.CityID);
      ViewBag.CountryID = new SelectList(clist, "Id", "Name", setModel.CountryID);
      ViewBag.NationalityID = new SelectList(db.MasterNationality.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", setModel.NationalityID);
      ViewBag.PrefixId = new SelectList(db.MasterPrefix.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", setModel.PrefixId);
      ViewBag.SuffixId = new SelectList(db.MasterSuffix.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", setModel.SuffixId);
      ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", setModel.EmployerID);
      ViewBag.PFRateID = new SelectList(db.MasterPFRate.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", setModel.PFRateID);
      ViewBag.PFRate = "";
      ViewBag.JobStatusID = new SelectList(db.MasterStatus.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", 1);
      ViewBag.AddressCountry = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
      ViewBag.AddressState = new SelectList(db.MasterState.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
      ViewBag.PermAddCountry = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
      ViewBag.PermAddState = new SelectList(db.MasterState.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
    }

    //public JsonResult StateList(int Id)
    //{
    //  var state = from s in db.MasterState
    //              where s.CountryId == Id
    //              select s;
    //  return Json(new SelectList(state.ToArray(), "Id", "Name"), JsonRequestBehavior.AllowGet);
    //}

    //public JsonResult Citylist(int id)
    //{
    //  var city = from c in db.MasterCity
    //             where c.StateId == id
    //             select c;
    //  return Json(new SelectList(city.ToArray(), "Id", "Name"), JsonRequestBehavior.AllowGet);
    //}
    //public IList<MasterState> Getstate(int CountryId)
    //{
    //  return db.MasterState.Where(m => m.CountryId == CountryId && m.IsActive == true).ToList();
    //}

    //[AcceptVerbs(HttpVerbs.Get)]
    //public JsonResult LoadClassesByCountryId(string CountryName)
    //{
    //  var stateList = this.Getstate(Convert.ToInt32(CountryName));
    //  var stateData = stateList.Select(m => new SelectListItem()
    //  {
    //    Text = m.Name,
    //    Value = m.CountryId.ToString(),
    //  });
    //  return Json(stateData, JsonRequestBehavior.AllowGet);
    //}

    // GET: ContributorPersonalDetails/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterContributor contributorPersonalDetails = db.MasterContributor.Find(id);
      if (contributorPersonalDetails == null)
      {
        return HttpNotFound();
      }
      var CurrentContributionDate = DateTime.Now.Date;

      var CurrentDate = DateTime.Now;
      int monthEndDate = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
      DateTime date = Convert.ToDateTime(CurrentDate.Month + "/" + monthEndDate + "/" + CurrentDate.Year);

      ViewBag.CId = contributorPersonalDetails.Id;
      ViewBag.PersonId = contributorPersonalDetails.PersonID;
      ViewBag.Name = (contributorPersonalDetails.PrefixId == null ? "" : contributorPersonalDetails.Prefix.Name) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      //ViewBag.CityID = new SelectList(db.City, "Id", "Name", contributorPersonalDetails.CityID);
      ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", contributorPersonalDetails.CountryID);
      ViewBag.NationalityID = new SelectList(db.MasterNationality.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", contributorPersonalDetails.NationalityID);
      ViewBag.PermanentCityID = new SelectList(db.MasterCity.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", contributorPersonalDetails.PermanentCityID);
      ViewBag.PrefixId = new SelectList(db.MasterPrefix.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", contributorPersonalDetails.PrefixId);
      ViewBag.SuffixId = new SelectList(db.MasterSuffix.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", contributorPersonalDetails.SuffixId);
      ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", contributorPersonalDetails.EmployerID);
      //ViewBag.PFRateID = new SelectList(db.MasterPFRate.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", contributorPersonalDetails.PFRateID);
      var list = db.MasterPFRate.Where(x => x.IsActive == true).Where(x => x.PfType == "C").OrderBy(x => x.Name).ToList().Select(x => new { Id = x.Id, Value = string.Format("{0} - ({1} - {2})", x.Name, "PFRate @" + x.PFRate, " Effective Date :" + x.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + x.EffectiveEndDate.ToString("dd MMMM, yyyy")) }).ToList();
      ViewBag.PFRateID = new SelectList(list, "Id", "Value", contributorPersonalDetails.PFRateID);
      ViewBag.PFRate = contributorPersonalDetails.PFRate;
      contributorPersonalDetails.OldPFRateID = contributorPersonalDetails.PFRateID;

      //var PfRate = db.MasterPFRate.FirstOrDefault(x => x.IsActive && x.Id == contributorPersonalDetails.PFRateID);
      //if (PfRate != null)
      //{
      //  contributorPersonalDetails.EmplrPFRate = PfRate.EmplrPFRate;
      //}

      if (contributorPersonalDetails.JobStatusID == (int)jobStatus.DeathInService || ((contributorPersonalDetails.JobStatusID == (int)jobStatus.Pensioner || contributorPersonalDetails.JobStatusID == (int)jobStatus.Resignee) && (contributorPersonalDetails.DateOfBirth == null ? Convert.ToDateTime("01/01/1900") : contributorPersonalDetails.DateOfBirth) > Convert.ToDateTime("01/01/1900")))
      {
        ViewBag.ActiveContributor = "No";
      }
      //ViewBag.AddressCountry = new SelectList(db.Countries, "Id", "Name", AddressCountryId);
      //ViewBag.AddressState = new SelectList(db.States, "Id", "Name", AddressStateId);
      //ViewBag.PermAddCountry = new SelectList(db.MasterCountry.Where(x => x.IsActive == true), "Id", "Name", PermAddCountryId);
      //ViewBag.PermAddState = new SelectList(db.MasterState.Where(x => x.IsActive == true), "Id", "Name", PermAddStateId);
      if ((Request.ServerVariables["HTTP_REFERER"] + "").Contains("errorsInContributorDetails"))
        ViewBag.RequestFrom = true;
      else
        ViewBag.RequestFrom = false;

      //set list of Changed PF Rates from [MasterEmployerPFRateDetails] Table to ViewBag
      var ChangedPfRateList = (from me in db.MasterContributorPFRateDetails
                               join pf in db.MasterPFRate
                               on me.PFRateID equals pf.Id
                               where me.PersonID == contributorPersonalDetails.PersonID
                               select new EmployerPfChangedViewModel
                               {
                                 Id = me.Id,
                                 PfRateName = pf.Name,
                                 PFRate = me.PFRate,
                                 EffiectiveDate = me.EffectiveDate,
                                 EffectiveEndDate = me.EffectiveEndDate,
                                 ModifiedOn = me.ModifiedOn,
                                 CreatedOn = me.CreatedOn,
                                 IsExpired = pf.EffectiveEndDate < date ? true : false,
                               }).OrderByDescending(x => x.EffiectiveDate).ToList();
      foreach (var item in ChangedPfRateList)
      {
        item.StringEffiectiveDate = item.EffiectiveDate.ToString("dd MMMM, yyyy");
        item.StringEffectiveEndDate = item.EffectiveEndDate.ToString("dd MMMM, yyyy");
        item.StringModifiedOn = item.ModifiedOn == null ? "" : item.ModifiedOn.Value.ToString("dd MMMM, yyyy");
        item.StringCreatedOn = item.CreatedOn == null ? "" : item.CreatedOn.Value.ToString("dd MMMM, yyyy");
      }
      var CurrentEffectivePfRate = ChangedPfRateList.Where(x => x.EffiectiveDate <= CurrentContributionDate && x.EffectiveEndDate >= CurrentContributionDate).OrderByDescending(x => x.EffiectiveDate).FirstOrDefault();
      if (CurrentEffectivePfRate != null)
      {
        CurrentEffectivePfRate.IsApplied = true;
      }
      ViewBag.ChangedPfRateList = ChangedPfRateList;
      contributorPersonalDetails.ShowTable = false;
      if (ChangedPfRateList.Count() > 0)
      {
        contributorPersonalDetails.ShowTable = true;
      }
      return View(contributorPersonalDetails);
    }

    // POST: ContributorPersonalDetails/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    private string validateEdit(int Id)
    {
      var emptype = db.MasterContributor.Where(x => x.Id == Id).Select(x => new { x.Employer.EmployerTypeID, x.EmployerID, x.JobStatusID, x.DateOfBirth }).FirstOrDefault();
      string error = "";
      //if (!db.MasterContributor.Where(x => x.JobStatusID == 1 && x.IsActive == true && x.Id == Id).Any())
      if (emptype.JobStatusID == (int)jobStatus.DeathInService || ((emptype.JobStatusID == (int)jobStatus.Pensioner || emptype.JobStatusID == (int)jobStatus.Resignee) && (emptype.DateOfBirth == null ? Convert.ToDateTime("01/01/1900") : emptype.DateOfBirth) > Convert.ToDateTime("01/01/1900")))
        error = "You Are Not An Active Contributor, So Details Can't Be Edited";
      else if (db.PensionApplications.Where(x => x.PersonID == Id.ToString() && x.IsActive == true).Any() || db.RefundApplications.Where(x => x.PersonID == Id.ToString() && x.IsActive == true).Any())
        error = "Pension / Refund Application for the Contributor has been Submitted So employer of this Contributor can't be changed.";
      else if (db.ContributonSheetDetails.Where(x => x.ContributorID == Id && x.IsActive == true).Any() && db.MasterEmployer.Where(x => x.Id == emptype.EmployerID && x.EmployerTypeID != emptype.EmployerTypeID && x.IsActive == true).Any())
        error = "Contributor can change Employer which have same Employer type";
      return error;
    }

    private int updateContributorDetails(int Id)
    {
      var tempupdate = db.MasterContributor.Where(x => x.Id == Id && x.IsActive == true).FirstOrDefault();
      MasterContributorUpdLog masterContributorUpdLog = new MasterContributorUpdLog();
      tempupdate.MapTo(masterContributorUpdLog);
      masterContributorUpdLog.MasterContributorId = tempupdate.Id;
      db.Entry(masterContributorUpdLog).State = EntityState.Added;
      int result = db.SaveChanges();
      return result;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(MasterContributor contributorPersonalDetails, HttpPostedFileBase[] NewfileUploadProfile)
    {
      if (ModelState.IsValid)
      {
        string error = validateEdit(contributorPersonalDetails.Id);
        if (error.Length > 0)
        {
          TempData["error"] = error;
        }
        else
        {
          using (var dbTransaction = db.Database.BeginTransaction())
          {
            try
            {
              int result = updateContributorDetails(contributorPersonalDetails.Id);
              if (result > 0)
              {
                //contributorPersonalDetails.JobStatusID = 1;
                using (var context = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
                {
                  context.Entry(contributorPersonalDetails).State = EntityState.Modified;
                  result = await context.SaveChangesAsync();

                  var MasterPfRate = db.MasterPFRate.FirstOrDefault(x => x.IsActive && x.Id == contributorPersonalDetails.PFRateID);
                  var AlreadyInHistory = db.MasterContributorPFRateDetails.Where(x => x.PFRateID == contributorPersonalDetails.PFRateID && x.PersonID == contributorPersonalDetails.PersonID).FirstOrDefault();
                  if (AlreadyInHistory == null)
                  {
                    var MasterContributorPFRateDetails = new MasterContributorPFRateDetails();
                    MasterContributorPFRateDetails.PersonID = contributorPersonalDetails.PersonID;
                    MasterContributorPFRateDetails.PFRateID = contributorPersonalDetails.PFRateID;
                    MasterContributorPFRateDetails.PFRate = MasterPfRate.PFRate;
                    MasterContributorPFRateDetails.EffectiveDate = MasterPfRate.EffectiveDate;
                    MasterContributorPFRateDetails.EffectiveEndDate = MasterPfRate.EffectiveEndDate;
                    MasterContributorPFRateDetails.IsActive = true;
                    db.MasterContributorPFRateDetails.Add(MasterContributorPFRateDetails);
                    db.SaveChanges();

                  }
                  else
                  {
                    db.Entry(AlreadyInHistory).State = EntityState.Modified;
                    db.SaveChanges();
                    //DbContextHelper.dBsavechanges(db);
                  }
                }
                if (result > 0)
                {
                  if (NewfileUploadProfile != null && NewfileUploadProfile.Length > 0)
                  {
                    await UpdateProfileImage(contributorPersonalDetails, NewfileUploadProfile);
                  }
                  TempData["success"] = "Details Saved Successfully";
                  dbTransaction.Commit();
                  if (Request["RequestFrom"] == "True")
                    return RedirectToAction("errorsInContributorDetails");
                  if (contributorPersonalDetails.IsActive)
                    return RedirectToAction("MarriageDetailsIndex", new { id = contributorPersonalDetails.Id });
                  else
                    return RedirectToAction("Index");
                }
              }
              else
              {
                TempData["error"] = "Please Try Again Later";
              }
            }
            catch (Exception ex)
            {
              dbTransaction.Rollback();
              TempData["error"] = "There is some error, try again later. " + ex.Message;
            }
          }
        }
      }
      //ViewBag.JobStatusID = new SelectList(db.MasterStatus.Where(x => x.IsActive == true), "Id", "Name", contributorPersonalDetails.JobStatusID);
      ViewBag.CId = contributorPersonalDetails.Id;
      ViewBag.PersonId = contributorPersonalDetails.PersonID;
      //ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      //ViewBag.CityID = new SelectList(db.City, "Id", "Name", contributorPersonalDetails.CityID);
      ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", contributorPersonalDetails.CountryID);
      ViewBag.NationalityID = new SelectList(db.MasterNationality.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", contributorPersonalDetails.NationalityID);
      ViewBag.PermanentCityID = new SelectList(db.MasterCity.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", contributorPersonalDetails.PermanentCityID);
      ViewBag.PrefixId = new SelectList(db.MasterPrefix.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", contributorPersonalDetails.PrefixId);
      ViewBag.SuffixId = new SelectList(db.MasterSuffix.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", contributorPersonalDetails.SuffixId);
      ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", contributorPersonalDetails.EmployerID);
      // ViewBag.PFRateID = new SelectList(db.MasterPFRate.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", contributorPersonalDetails.PFRateID);
      var list = db.MasterPFRate.Where(x => x.IsActive == true).Where(x => x.PfType == "C").OrderBy(x => x.Name).ToList().Select(x => new { Id = x.Id, Value = string.Format("{0} - ({1} - {2})", x.Name, "PFRate @" + x.PFRate, " Effective Date :" + x.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + x.EffectiveEndDate.ToString("dd MMMM, yyyy")) }).ToList();
      ViewBag.PFRateID = new SelectList(list, "Id", "Value", contributorPersonalDetails.PFRateID);
      ViewBag.PFRate = contributorPersonalDetails.PFRate;
      return View(contributorPersonalDetails);
    }

    // GET: ContributorPersonalDetails/Delete/5
    //public ActionResult Delete(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  MasterContributor contributorPersonalDetails = db.MasterContributor.Find(id);
    //  if (contributorPersonalDetails == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(contributorPersonalDetails);
    //}

    // POST: ContributorPersonalDetails/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public ActionResult DeleteConfirmed(int id)
    //{
    //  MasterContributor contributorPersonalDetails = db.MasterContributor.Find(id);
    //  db.MasterContributor.Remove(contributorPersonalDetails);
    //  db.SaveChanges();
    //  return RedirectToAction("Index");
    //}

    //public ActionResult AddContributorPersonalDetails()
    //{
    //  DoBind(null);
    //  //ViewBag.PersonID = string.Format("{0:C:00000}", db.MasterContributor.Count() + 1);
    //  //ViewBag.PFRateID = new SelectList(db.MasterPFRate.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");

    //  return View();
    //}

    [HttpGet]
    public ActionResult AddPersonalDetails()
    {
      //ViewBag.PFRateID = new SelectList(db.MasterPFRate.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
      var list = db.MasterPFRate.Where(x => x.IsActive == true).Where(x => x.PfType == "C").OrderBy(x => x.Name).ToList().Select(x => new { Id = x.Id, Value = string.Format("{0} - ({1} - {2})", x.Name, "PFRate @" + x.PFRate, " Effective Date :" + x.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + x.EffectiveEndDate.ToString("dd MMMM, yyyy")) }).ToList();
      ViewBag.PFRateID = new SelectList(list, "Id", "Value");
      ViewBag.PrefixId = new SelectList(db.MasterPrefix.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
      ViewBag.SuffixId = new SelectList(db.MasterSuffix.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
      ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName");
      ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", 7);
      ViewBag.NationalityID = new SelectList(db.MasterNationality.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", 3);
      List<MasterCity> city = new List<MasterCity>();
      ViewBag.PermanentCityID = new SelectList(city, "Id", "Name");

      return View();
    }
    [HttpPost]
    public async Task<ActionResult> AddPersonalDetails(MasterContributor model, HttpPostedFileBase[] NewfileUploadProfile)
    {
      if (!db.MasterContributor.Where(x => x.IsActive == true && x.JobStatusID == 1 && x.PersonID == model.PersonID).Any())
      {
        if (ModelState.IsValid)
        {
          model.JobStatusID = 1;
          db.MasterContributor.Add(model);
          int result = await db.SaveChangesAsync();
          if (result > 0)
          {
            var MasterPfRate = db.MasterPFRate.FirstOrDefault(x => x.IsActive && x.Id == model.PFRateID);
            var MasterContributorPFRateDetails = new MasterContributorPFRateDetails();
            MasterContributorPFRateDetails.PersonID = model.PersonID;
            MasterContributorPFRateDetails.PFRateID = model.PFRateID;
            MasterContributorPFRateDetails.PFRate = model.PFRate;
            MasterContributorPFRateDetails.EffectiveDate = MasterPfRate.EffectiveDate;
            MasterContributorPFRateDetails.EffectiveEndDate = MasterPfRate.EffectiveEndDate;
            MasterContributorPFRateDetails.IsActive = true;
            db.MasterContributorPFRateDetails.Add(MasterContributorPFRateDetails);
            db.SaveChanges();

            if (NewfileUploadProfile != null && NewfileUploadProfile.Length > 0)
            {
              await UpdateProfileImage(model, NewfileUploadProfile);

            }
            TempData["success"] = "Personal Details Saved Successfully";
            return RedirectToAction("Edit", new { id = model.Id });
          }
          else
          {
            TempData["error"] = "Please Try Again...";
          }
        }
      }
      else
      {
        TempData["error"] = "Please Refresh Page And Try Again...";
      }
      //ViewBag.PFRateID = new SelectList(db.MasterPFRate.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.PFRateID);
      var list = db.MasterPFRate.Where(x => x.IsActive == true).Where(x => x.PfType == "C").OrderBy(x => x.Name).ToList().Select(x => new { Id = x.Id, Value = string.Format("{0} - ({1} - {2})", x.Name, "PFRate @" + x.PFRate, " Effective Date :" + x.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + x.EffectiveEndDate.ToString("dd MMMM, yyyy")) }).ToList();
      ViewBag.PFRateID = new SelectList(list, "Id", "Value", model.PFRateID);
      ViewBag.PrefixId = new SelectList(db.MasterPrefix.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.PrefixId);
      ViewBag.SuffixId = new SelectList(db.MasterSuffix.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.SuffixId);
      ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", model.EmployerID);
      ViewBag.PFRateID = new SelectList(db.MasterPFRate.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.PFRateID);
      ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.CountryID);
      ViewBag.NationalityID = new SelectList(db.MasterNationality.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.Nationality);
      ViewBag.PermanentCityID = new SelectList(db.MasterCity.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.PermanentCityID);
      return View(model);
    }
    //AddPersonalDetails
    public JsonResult PersonIDAjax()
    {
      string result = "";
      int PersonID = db.MasterContributor.OrderByDescending(x => x.Id).Select(x => x == null ? 0 : x.Id).FirstOrDefault();
      if (PersonID != 0)
      {

        result = "PF00" + (PersonID + 1);
      }
      else
      {
        result = "PF001";
      }
      return Json(result, JsonRequestBehavior.AllowGet);
    }

    public async Task<int> UpdateProfileImage(MasterContributor model, HttpPostedFileBase[] fileUploadProfile)
    {
      int result = 1;
      try
      {
        var fileName = App.Web.Helper.SiteHelper.GenerateFileName(model.Id);
        var path = App.Web.Helper.SiteHelper.ProfileImagesPath + fileName;

        foreach (var item in fileUploadProfile)
        {
          if (item != null)
          {
            item.SaveAs(path);
            var entity = await _DbContext.MasterContributor.FirstOrDefaultAsync(x => x.Id == model.Id);
            entity.ProfilePath = fileName;
            _DbContext.Entry(entity).State = EntityState.Modified;
            result = await _DbContext.SaveChangesAsync();

          }
        }
        return result;
        //return true;
      }
      catch (Exception ex)
      {
        //throw ex;
        return 0;
      }
    }

    //[HttpGet]
    //public ActionResult _PartialAddMarriageDetails(string id)
    //{
    //  ViewBag.PersonID = id;
    //  ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
    //  ViewBag.MaritalStatusId = new SelectList(db.MasterMaritalStatus.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
    //  return View();
    //}

    //[HttpPost]
    //public ActionResult _PartialAddMarriageDetails(MasterContributorMarriageDetails model)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.MasterContributorMarriageDetails.Add(model);
    //    db.SaveChanges();
    //    return RedirectToAction("_PartialAddDependantDetails", new { id = model.PersonId });
    //  }
    //  ViewBag.PersonID = model.PersonId;
    //  ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.CountryID);
    //  ViewBag.MaritalStatusId = new SelectList(db.MasterMaritalStatus.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.MaritalStatusId);
    //  return View(model);
    //}

    public ActionResult MarriageDetailsIndex(int? id)
    {
      var contributorPersonalDetails = db.MasterContributor.Where(x => x.Id == id && x.IsActive == true).FirstOrDefault();
      //ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.Name = (contributorPersonalDetails.PrefixId == null ? "" : contributorPersonalDetails.Prefix.Name) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      List<MasterContributorMarriageDetails> model = db.MasterContributorMarriageDetails.Where(o => o.PersonId == contributorPersonalDetails.PersonID && o.IsActive == true).ToList();
      ViewBag.CId = id;
      ViewBag.PersonID = contributorPersonalDetails.PersonID;
      if (contributorPersonalDetails.JobStatusID != 1)
      {
        ViewBag.ActiveContributor = "No";
      }
      return View(model);
    }

    //[HttpGet]
    //public ActionResult _PartialAddDependantDetails(string id)
    //{
    //  ViewBag.PersonID = id;
    //  ViewBag.RelationshipID = new SelectList(db.MasterRelationshipType.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
    //  return View();
    //}

    //[HttpPost]
    //public ActionResult _PartialAddDependantDetails(MasterDependantDetails model)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.MasterDependantDetails.Add(model);
    //    db.SaveChanges();
    //    return RedirectToAction("_PartialAddJobDetails", new { id = model.PersonID });
    //  }

    //  ViewBag.PersonID = model.PersonID;
    //  ViewBag.RelationshipID = new SelectList(db.MasterRelationshipType.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.RelationshipID);
    //  return View(model);
    //}

    //[HttpGet]
    //public ActionResult _PartialAddJobDetails(int? id)
    //{
    //  ViewBag.PersonID = id;
    //  ViewBag.DepartmentId = new SelectList(db.MasterDepartment.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
    //  ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
    //  ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName");
    //  ViewBag.GradeId = new SelectList(db.MasterGrade.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
    //  ViewBag.JobTitleID = new SelectList(db.MasterJobTitle.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
    //  return View();
    //}

    //[HttpPost]
    //public ActionResult _PartialAddJobDetails(MasterContributorJobDetails model)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.MasterContributorJobDetails.Add(model);
    //    db.SaveChanges();
    //    return RedirectToAction("Edit", new { id = model.PersonID });
    //  }

    //  ViewBag.PersonID = model.PersonID;
    //  ViewBag.DepartmentId = new SelectList(db.MasterDepartment.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.DepartmentId);
    //  ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.DesignationId);
    //  ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", model.EmployerID);
    //  ViewBag.GradeId = new SelectList(db.MasterGrade.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.GradeId);
    //  ViewBag.JobTitleID = new SelectList(db.MasterJobTitle.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.JobTitleID);
    //  return View(model);
    //}

    public ActionResult AddMarriageDetails(int id)
    {
      MasterContributor contributorPersonalDetails = db.MasterContributor.Where(x => x.Id == id && x.IsActive == true).FirstOrDefault();
      ViewBag.ContributorID = contributorPersonalDetails.Id;
      ViewBag.PersonID = contributorPersonalDetails.PersonID;
      ViewBag.CId = id;
      ViewBag.Name = (contributorPersonalDetails.PrefixId == null ? "" : contributorPersonalDetails.Prefix.Name) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      //ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", 7);
      ViewBag.MaritalStatusId = new SelectList(db.MasterMaritalStatus.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
      ViewBag.BeginDate = contributorPersonalDetails.DateOfBirth.Value.AddYears(+18);
      var marriageDetails = db.MasterContributorMarriageDetails.Where(x => x.PersonId == contributorPersonalDetails.PersonID && x.IsActive == true).ToList();
      if (marriageDetails.Any())
      {
        if (!marriageDetails.LastOrDefault().MarriageEndDate.HasValue)
        {
          ViewBag.preMarriage = "Yes";
        }
        else
        {
          ViewBag.BeginDate = marriageDetails.LastOrDefault().MarriageEndDate;
        }
      }
      if ((Request.ServerVariables["HTTP_REFERER"] + "").Contains("errorsInContributorDetails"))
        ViewBag.RequestFrom = true;
      else
        ViewBag.RequestFrom = false;
      return View();
    }

    // POST: MarriageDetails/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddMarriageDetails(MasterContributorMarriageDetails marriageDetails)
    {
      var masterMArriage = db.MasterContributorMarriageDetails.Where(x => x.PersonId == marriageDetails.PersonId && x.IsActive == true);
      if (!db.MasterContributor.Where(x => x.PersonID == marriageDetails.PersonId && x.JobStatusID != 1).Any())
      {
        if (!masterMArriage.Where(x => x.PersonId == marriageDetails.PersonId && x.SpouseFirstName == marriageDetails.SpouseFirstName).Any())
        {
          if ((!masterMArriage.Any()) || (masterMArriage.Any() && masterMArriage.Where(x => x.MarriageBeginDate <= marriageDetails.MarriageBeginDate).Any()))
          {
            if (ModelState.IsValid)
            {
              db.MasterContributorMarriageDetails.Add(marriageDetails);
              int result = db.SaveChanges();
              if (result > 0)
              {
                TempData["success"] = "Marriage Details Added Successfully";
                MasterContributor contributorPersonalDetails = db.MasterContributor.Where(x => x.PersonID == marriageDetails.PersonId).FirstOrDefault();
                if (Request["RequestFrom"] == "True")
                  return RedirectToAction("errorsInContributorDetails");
                else
                  return RedirectToAction("MarriageDetailsIndex", new { id = contributorPersonalDetails.Id });
              }
              else
              {
                TempData["error"] = "There is some error. Please try Again Later";
              }

            }
          }
          else
          {
            TempData["error"] = "Marriage Date Always Greater than Existing Dates";
          }

        }
        else
        {
          TempData["error"] = "Marriage Details With That Name Already Added";
        }
      }
      else
      {
        TempData["error"] = "You Are Not An Active Contributor, So Details Can't Be Added ";
      }
      ViewBag.PersonId = marriageDetails.PersonId;
      MasterContributor contributorPersonalDetail = db.MasterContributor.Where(x => x.PersonID == marriageDetails.PersonId && x.IsActive == true).FirstOrDefault();
      ViewBag.CId = contributorPersonalDetail.Id;
      //ViewBag.Name = db.MasterPrefix.Where(x => x.Id == contributorPersonalDetail.PrefixId).FirstOrDefault().Name + " " + contributorPersonalDetail.FirstName + " " + contributorPersonalDetail.MidName + " " + contributorPersonalDetail.LastName;
      ViewBag.Name = (contributorPersonalDetail.PrefixId == null ? "" : contributorPersonalDetail.Prefix.Name) + " " + contributorPersonalDetail.FirstName + " " + contributorPersonalDetail.MidName + " " + contributorPersonalDetail.LastName;
      ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", marriageDetails.CountryID);
      ViewBag.MaritalStatusId = new SelectList(db.MasterMaritalStatus.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", marriageDetails.MaritalStatusId);
      return View(marriageDetails);
    }
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult AddMarriageDetails(MasterContributorMarriageDetails marriageDetails)
    //{

    //  if (ModelState.IsValid)
    //  {
    //    string error = marriageDetailsValidate(marriageDetails.PersonId, marriageDetails.SpouseFirstName, marriageDetails.MarriageBeginDate, marriageDetails.Id);
    //    if (error.Length > 0)
    //    {
    //      TempData["error"] = error;
    //    }
    //    else
    //    {
    //      db.MasterContributorMarriageDetails.Add(marriageDetails);
    //      int result = db.SaveChanges();
    //      if (result > 0)
    //      {
    //        TempData["success"] = "Marriage Details Added Successfully";
    //        MasterContributor contributorPersonalDetails = db.MasterContributor.Where(x => x.PersonID == marriageDetails.PersonId).FirstOrDefault();
    //        return RedirectToAction("MarriageDetailsIndex", new { id = contributorPersonalDetails.Id });
    //      }
    //      else
    //      {
    //        TempData["error"] = "There is some error. Please try Again Later";
    //      }
    //    }
    //  }

    //  ViewBag.PersonId = marriageDetails.PersonId;
    //  MasterContributor contributorPersonalDetail = db.MasterContributor.Where(x => x.PersonID == marriageDetails.PersonId && x.IsActive == true).FirstOrDefault();
    //  ViewBag.CId = contributorPersonalDetail.Id;
    //  //ViewBag.Name = db.MasterPrefix.Where(x => x.Id == contributorPersonalDetail.PrefixId).FirstOrDefault().Name + " " + contributorPersonalDetail.FirstName + " " + contributorPersonalDetail.MidName + " " + contributorPersonalDetail.LastName;
    //  ViewBag.Name = (contributorPersonalDetail.PrefixId == null ? "" : contributorPersonalDetail.Prefix.Name) + " " + contributorPersonalDetail.FirstName + " " + contributorPersonalDetail.MidName + " " + contributorPersonalDetail.LastName;
    //  ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", marriageDetails.CountryID);
    //  ViewBag.MaritalStatusId = new SelectList(db.MasterMaritalStatus.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", marriageDetails.MaritalStatusId);
    //  return View(marriageDetails);
    //}
    //private string marriageDetailsValidate(string personId, string firstName, DateTime? marriageBeginDate, int Id)
    //{
    //  string error = "";
    //  if (db.MasterContributor.Where(x => x.PersonID == personId && x.JobStatusID != 1).Any())
    //    error = "You Are Not An Active Contributor, So Details Can't Be Added";
    //  else if (db.MasterContributorMarriageDetails.Where(x => x.PersonId == personId && x.SpouseFirstName == firstName && x.IsActive == true && x.Id != Id).Any())
    //    error = "Marriage Details With That Name Already Added";
    //  else if (db.MasterContributorMarriageDetails.Where(x => x.MarriageBeginDate > marriageBeginDate && x.PersonId == personId && x.IsActive == true && x.Id != Id).Any())
    //    error = "Marriage Date Always Greater than Existing Dates";
    //  //else if (Id != 0)
    //  //{
    //  //  if (db.MasterContributorMarriageDetails.Where(x => x.PersonId == personId && x.MarriageBeginDate > marriageBeginDate && x.Id != Id).Any())
    //  //    error = "Marriage Date Always Greater than Existing Dates";
    //  //}
    //  return error;
    //}
    public ActionResult _ContributorListForMarriage()
    {

      return View("_ContributorListForMarriage");
    }
    public ActionResult ContributorMarriageAjaxHandler(JQueryDataTableParamModel param, int? Id)
    {
      var List = db.MasterContributor.Where(x => x.IsActive == true && x.JobStatusID == 1);
      IEnumerable<MasterContributor> filtered;
      if (!string.IsNullOrEmpty(Id.ToString()) && Id != 0)
      {
        List = List.Where(x => x.Id != Id);
      }
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
           || c.PhoneOffice.ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      //var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      //Func<MasterContributor, string> orderingFunction = (c => sortColumnIndex == 0 ? c.FirstName :
      //                                                      sortColumnIndex == 1 ? c.MidName :
      //                                                      sortColumnIndex == 2 ? c.LastName :

      //                                                                                sortColumnIndex == 3 ? c.Employer.EmployerName :
      //                                                                                sortColumnIndex == 4 ? c.PersonID :
      //                                                                                sortColumnIndex == 5 ? c.OldPersonID :
      //                                                                                sortColumnIndex == 6 ? c.DateOfBirth + "" :
      //                                                                                sortColumnIndex == 7 ? c.SocialSecurityNo :
      //                                                                                sortColumnIndex == 8 ? c.PostalAddress :
      //                                                                                sortColumnIndex == 9 ? c.PhoneOffice :
      //                                                                                sortColumnIndex == 10 ? c.Status.Name :
      //                                                                                "");

      //var sortDirection = Request["sSortDir_0"]; // asc or desc
      //if (sortDirection == "asc")
      //  filtered = filtered.OrderBy(orderingFunction);
      //else
      //  filtered = filtered.OrderByDescending(orderingFunction);

      //Pagging
      var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

      //Select required columns
      var result = from c in displayed
                   select new[] {
                         c.PersonID,
                         c.OldPersonID,
                                         c.FirstName+" "+c.MidName+" "+c.LastName,
                                         c.Employer.EmployerName,
                                         c.SocialSecurityNo,
                     c.Gender,
                     String.Format("{0:MM/dd/yyyy}", c.DateOfBirth),
                     c.PostalAddress,
                     c.Phone,
                     c.PhoneOffice,
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

    // GET: MarriageDetails/Edit/5
    public ActionResult EditMarriageDetails(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterContributorMarriageDetails marriageDetails = db.MasterContributorMarriageDetails.Find(id);
      if (marriageDetails == null)
      {
        return HttpNotFound();
      }
      ViewBag.PersonId = marriageDetails.PersonId;
      MasterContributor contributorPersonalDetails = db.MasterContributor.Where(x => x.PersonID == marriageDetails.PersonId && x.IsActive == true).FirstOrDefault();
      ViewBag.CId = contributorPersonalDetails.Id;
      //ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.Name = (contributorPersonalDetails.PrefixId == null ? "" : contributorPersonalDetails.Prefix.Name) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", marriageDetails.CountryID);
      ViewBag.MaritalStatusId = new SelectList(db.MasterMaritalStatus.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", marriageDetails.MaritalStatusId);
      ViewBag.BeginDate = contributorPersonalDetails.DateOfBirth.Value.AddYears(+18);
      if (contributorPersonalDetails.JobStatusID != 1)
      {
        ViewBag.ActiveContributor = "No";
      }
      if ((Request.ServerVariables["HTTP_REFERER"] + "").Contains("errorsInContributorDetails"))
        ViewBag.RequestFrom = true;
      else
        ViewBag.RequestFrom = false;
      return View(marriageDetails);
    }

    // POST: MarriageDetails/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditMarriageDetails(MasterContributorMarriageDetails marriageDetails)
    {
      if (!db.MasterContributor.Where(x => x.PersonID == marriageDetails.PersonId && x.JobStatusID != 1).Any())
      {
        if (!db.MasterContributorMarriageDetails.Where(x => x.PersonId == marriageDetails.PersonId && x.SpouseFirstName == marriageDetails.SpouseFirstName && x.Id != marriageDetails.Id && x.IsActive == true).Any())
        {
          if (!db.MasterContributorMarriageDetails.Where(x => x.PersonId == marriageDetails.PersonId && x.MarriageBeginDate <= marriageDetails.MarriageBeginDate && x.MarriageEndDate >= marriageDetails.MarriageEndDate && x.Id != marriageDetails.Id && x.IsActive == true).Any())
          {
            if (ModelState.IsValid)
            {
              db.Entry(marriageDetails).State = EntityState.Modified;
              int result = db.SaveChanges();
              if (result > 0)
              {
                TempData["success"] = "Marriage Details Added Successfully";
                MasterContributor contributorPersonalDetail = db.MasterContributor.Where(x => x.PersonID == marriageDetails.PersonId && x.IsActive == true).FirstOrDefault();
                if (Request["RequestFrom"] == "True")
                  return RedirectToAction("errorsInContributorDetails");
                else
                  return RedirectToAction("MarriageDetailsIndex", new { id = contributorPersonalDetail.Id });
              }
              else
              {
                TempData["error"] = "There is some error. Please try Again Later";
              }
            }
          }
          else
          {
            TempData["error"] = "Marriage Date Always Greater than Existing Dates";
          }
        }
        else
        {
          TempData["error"] = "Marriage Details With That Name Already Added";
        }
      }
      else
      {
        TempData["error"] = "You Are Not An Active Contributor, So Details Can't Be Edited ";
      }
      ViewBag.PersonId = marriageDetails.PersonId;
      MasterContributor contributorPersonalDetails = db.MasterContributor.Where(x => x.PersonID == marriageDetails.PersonId && x.IsActive == true).FirstOrDefault();
      //ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.Name = (contributorPersonalDetails.PrefixId == null ? "" : contributorPersonalDetails.Prefix.Name) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.CId = contributorPersonalDetails.Id;
      ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", marriageDetails.CountryID);
      ViewBag.MaritalStatusId = new SelectList(db.MasterMaritalStatus.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", marriageDetails.MaritalStatusId);
      return View(marriageDetails);
    }
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult EditMarriageDetails(MasterContributorMarriageDetails marriageDetails)
    //{

    //  if (ModelState.IsValid)
    //  {
    //    string error = marriageDetailsValidate(marriageDetails.PersonId, marriageDetails.SpouseFirstName, marriageDetails.MarriageBeginDate, marriageDetails.Id);
    //    if (error.Length > 0)
    //    {
    //      TempData["error"] = error;
    //    }
    //    else
    //    {
    //      db.Entry(marriageDetails).State = EntityState.Modified;
    //      int result = db.SaveChanges();
    //      if (result > 0)
    //      {
    //        TempData["success"] = "Marriage Details Added Successfully";
    //        MasterContributor contributorPersonalDetail = db.MasterContributor.Where(x => x.PersonID == marriageDetails.PersonId && x.IsActive == true).FirstOrDefault();
    //        return RedirectToAction("MarriageDetailsIndex", new { id = contributorPersonalDetail.Id });
    //      }
    //      else
    //      {
    //        TempData["error"] = "There is some error. Please try Again Later";
    //      }
    //    }
    //  }


    //  ViewBag.PersonId = marriageDetails.PersonId;
    //  MasterContributor contributorPersonalDetails = db.MasterContributor.Where(x => x.PersonID == marriageDetails.PersonId && x.IsActive == true).FirstOrDefault();
    //  //ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
    //  ViewBag.Name = (contributorPersonalDetails.PrefixId == null ? "" : contributorPersonalDetails.Prefix.Name) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
    //  ViewBag.CId = contributorPersonalDetails.Id;
    //  ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", marriageDetails.CountryID);
    //  ViewBag.MaritalStatusId = new SelectList(db.MasterMaritalStatus.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", marriageDetails.MaritalStatusId);
    //  return View(marriageDetails);
    //}

    // GET: MarriageDetails/Delete/5
    //public ActionResult DeleteMarriageDetails(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  MasterContributorMarriageDetails marriageDetails = db.MasterContributorMarriageDetails.Find(id);
    //  if (marriageDetails == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  ViewBag.PersonId = marriageDetails.PersonId;
    //  return View(marriageDetails);
    //}

    public ActionResult DependantDetailsIndex(int? Id)
    {

      var contributorPersonalDetails = db.MasterContributor.Where(x => x.Id == Id && x.IsActive == true).FirstOrDefault();
      //ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.Name = (contributorPersonalDetails.PrefixId == null ? "" : contributorPersonalDetails.Prefix.Name) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      var dependantDetails = db.MasterDependantDetails.Where(x => x.PersonID == contributorPersonalDetails.PersonID && x.IsActive == true).Include(d => d.Relationship);
      ViewBag.CId = Id;
      ViewBag.PersonId = contributorPersonalDetails.PersonID;
      if (contributorPersonalDetails.JobStatusID != 1)
      {
        ViewBag.ActiveContributor = "No";
      }

      return View(dependantDetails.ToList());
    }

    // GET: DependantDetails/Details/5
    //public ActionResult DependantDetails(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  MasterDependantDetails dependantDetails = db.MasterDependantDetails.Find(id);
    //  if (dependantDetails == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(dependantDetails);
    //}

    // GET: DependantDetails/Create
    public ActionResult AddDependantDetails(int Id)
    {
      ViewBag.CId = Id;
      var contributorPersonalDetails = (from C in db.MasterContributor
                                        join E in db.MasterEmployer on C.EmployerID equals E.Id
                                        join ET in db.MasterEmployerType on E.EmployerTypeID equals ET.Id
                                        where C.Id == Id
                                        select new { C.PersonID, ET.DependantTerminationAge, C.PrefixId, C.FirstName, C.MidName, C.LastName }).FirstOrDefault();
      //MasterContributor contributorPersonalDetails = db.MasterContributor.Where(x => x.Id == Id).FirstOrDefault();
      ViewBag.PersonID = contributorPersonalDetails.PersonID;
      ViewBag.DependantTerminationAge = contributorPersonalDetails.DependantTerminationAge;
      //ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.Name = (contributorPersonalDetails.PrefixId == null ? "" : db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Select(x => x.Name).FirstOrDefault()) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.RelationshipID = new SelectList(db.MasterRelationshipType.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
      if ((Request.ServerVariables["HTTP_REFERER"] + "").Contains("errorsInContributorDetails"))
        ViewBag.RequestFrom = true;
      else
        ViewBag.RequestFrom = false;
      return View();
    }

    // POST: DependantDetails/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddDependantDetails(MasterDependantDetails dependantDetails)
    {
      if (!db.MasterContributor.Where(x => x.PersonID == dependantDetails.PersonID && x.JobStatusID != 1).Any())
      {
        if (!db.MasterDependantDetails.Where(x => x.PersonID == dependantDetails.PersonID && x.FirstName == dependantDetails.FirstName && x.LastName == dependantDetails.LastName && x.MidName == dependantDetails.MidName).Any())
        {
          if (ModelState.IsValid)
          {
            db.MasterDependantDetails.Add(dependantDetails);
            db.SaveChanges();
            MasterContributor contributorPersonalDetail = db.MasterContributor.Where(x => x.PersonID == dependantDetails.PersonID && x.IsActive == true).FirstOrDefault();
            if (Request["RequestFrom"] == "True")
              return RedirectToAction("errorsInContributorDetails");
            else
              return RedirectToAction("DependantDetailsIndex", new { Id = contributorPersonalDetail.Id });
          }
        }
        else
        {
          TempData["error"] = "Dependant Details Already Added With that Name";
        }
      }
      else
      {
        TempData["error"] = "You Are Not An Active Contributor, So Details Can't Be Added ";
      }
      ViewBag.PersonId = dependantDetails.PersonID;
      var contributorPersonalDetails = (from C in db.MasterContributor
                                        join E in db.MasterEmployer on C.EmployerID equals E.Id
                                        join ET in db.MasterEmployerType on E.EmployerTypeID equals ET.Id
                                        where C.PersonID == dependantDetails.PersonID
                                        select new { C.PersonID, ET.DependantTerminationAge, C.PrefixId, C.FirstName, C.MidName, C.LastName, C.Id }).FirstOrDefault();
      //MasterContributor contributorPersonalDetails = db.MasterContributor.Where(x => x.PersonID == dependantDetails.PersonID).FirstOrDefault();
      ViewBag.DependantTerminationAge = contributorPersonalDetails.DependantTerminationAge;
      //ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.Name = (contributorPersonalDetails.PrefixId == null ? "" : db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Select(x => x.Name).FirstOrDefault()) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.RelationshipID = new SelectList(db.MasterRelationshipType.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", dependantDetails.RelationshipID);
      ViewBag.CId = contributorPersonalDetails.Id;
      return View(dependantDetails);
    }

    // GET: DependantDetails/Edit/5
    public ActionResult EditDependantDetails(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterDependantDetails dependantDetails = db.MasterDependantDetails.Find(id);
      if (dependantDetails == null)
      {
        return HttpNotFound();
      }
      ViewBag.PersonId = dependantDetails.PersonID;
      var contributorPersonalDetails = (from C in db.MasterContributor
                                        join E in db.MasterEmployer on C.EmployerID equals E.Id
                                        join ET in db.MasterEmployerType on E.EmployerTypeID equals ET.Id
                                        where C.PersonID == dependantDetails.PersonID
                                        select new { C.PersonID, ET.DependantTerminationAge, C.PrefixId, C.FirstName, C.MidName, C.LastName, C.Id, C.JobStatusID }).FirstOrDefault();

      ViewBag.DependantTerminationAge = contributorPersonalDetails.DependantTerminationAge;
      //MasterContributor contributorPersonalDetails = db.MasterContributor.Where(x => x.PersonID == dependantDetails.PersonID).FirstOrDefault();
      ViewBag.CId = contributorPersonalDetails.Id;
      //ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.Name = (contributorPersonalDetails.PrefixId == null ? "" : db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Select(x => x.Name).FirstOrDefault()) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.RelationshipID = new SelectList(db.MasterRelationshipType.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", dependantDetails.RelationshipID);
      if (contributorPersonalDetails.JobStatusID != 1)
      {
        ViewBag.ActiveContributor = "No";
      }
      if ((Request.ServerVariables["HTTP_REFERER"] + "").Contains("errorsInContributorDetails"))
        ViewBag.RequestFrom = true;
      else
        ViewBag.RequestFrom = false;
      return View(dependantDetails);
    }

    // POST: DependantDetails/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditDependantDetails(MasterDependantDetails dependantDetails)
    {
      if (!db.MasterContributor.Where(x => x.PersonID == dependantDetails.PersonID && x.JobStatusID != 1).Any())
      {
        if (!db.MasterDependantDetails.Where(x => x.PersonID == dependantDetails.PersonID && x.FirstName == dependantDetails.FirstName && x.LastName == dependantDetails.LastName && x.MidName == dependantDetails.MidName && x.Id != dependantDetails.Id).Any())
        {
          if (ModelState.IsValid)
          {
            db.Entry(dependantDetails).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.PersonId = dependantDetails.PersonID;
            MasterContributor contributorPersonalDetail = db.MasterContributor.Where(x => x.PersonID == dependantDetails.PersonID && x.IsActive == true).FirstOrDefault();
            if (Request["RequestFrom"] == "True")
              return RedirectToAction("errorsInContributorDetails");
            else
              return RedirectToAction("DependantDetailsIndex", new { Id = contributorPersonalDetail.Id });
          }
        }
        else
        {
          TempData["error"] = "Dependant Details Already Added With that Name";
        }
      }
      else
      {
        TempData["error"] = "You Are Not An Active Contributor, So Details Can't Be Edited ";
      }
      ViewBag.PersonId = dependantDetails.PersonID;
      //MasterContributor contributorPersonalDetails = db.MasterContributor.Where(x => x.PersonID == dependantDetails.PersonID).FirstOrDefault();
      var contributorPersonalDetails = (from C in db.MasterContributor
                                        join E in db.MasterEmployer on C.EmployerID equals E.Id
                                        join ET in db.MasterEmployerType on E.EmployerTypeID equals ET.Id
                                        where C.PersonID == dependantDetails.PersonID
                                        select new { C.PersonID, ET.DependantTerminationAge, C.PrefixId, C.FirstName, C.MidName, C.LastName, C.Id }).FirstOrDefault();

      ViewBag.DependantTerminationAge = contributorPersonalDetails.DependantTerminationAge;

      ViewBag.CId = contributorPersonalDetails.Id;
      //ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.Name = (contributorPersonalDetails.PrefixId == null ? "" : db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Select(x => x.Name).FirstOrDefault()) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.RelationshipID = new SelectList(db.MasterRelationshipType.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", dependantDetails.RelationshipID);
      return View(dependantDetails);
    }

    // GET: DependantDetails/Delete/5
    //public ActionResult DeleteDependantDetails(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  MasterDependantDetails dependantDetails = db.MasterDependantDetails.Find(id);
    //  if (dependantDetails == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  ViewBag.PersonId = dependantDetails.PersonID;
    //  return View(dependantDetails);
    //}

    //public ActionResult JobDetailsList(string id)
    //{
    //  var jobDetails = db.MasterContributorJobDetails.Where(x => x.PersonID == id).Include(j => j.Department).Include(j => j.Designation).Include(j => j.Employer).Include(j => j.Grade);
    //  ViewBag.Id = id;
    //  ViewBag.PersonId = id;
    //  var contributorPersonalDetails = db.MasterContributor.Where(x => x.PersonID == id).FirstOrDefault();
    //  ViewBag.Name = contributorPersonalDetails.PrefixId==null?string.Empty:(db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any()? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name:string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
    //  return View(jobDetails);
    //}

    // GET: JobDetails/Details/5
    //public ActionResult JobDetails(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  MasterContributorJobDetails jobDetails = db.MasterContributorJobDetails.Find(id);
    //  if (jobDetails == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(jobDetails);
    //}

    // GET: JobDetails/Create
    public ActionResult AddJobDetails(int? Id)
    {
      var contributorPersonalDetails = (from c in db.MasterContributor
                                        join e in db.MasterEmployer on c.EmployerID equals e.Id
                                        join et in db.MasterEmployerType on e.EmployerTypeID equals et.Id
                                        where c.Id == Id
                                        select new { minHiringAge = et.MinimumHiringAge, c.PersonID, c.PrefixId, c.FirstName, c.MidName, c.LastName, c.FirstAppointmentDate, c.LastAppointmentDate, c.DateOfBirth, c.EmployerID, c.Employer.EmployerName, c.JobStatusID, c.OldPersonID }).FirstOrDefault();
      //db.MasterContributor.Where(x => x.Id == Id).FirstOrDefault();
      //ViewBag.CurrentStatus = contributorPersonalDetails == null ? 0 : contributorPersonalDetails.JobStatusID;
      MasterContributorJobDetails model = db.MasterContributorJobDetails.Where(x => x.PersonID == contributorPersonalDetails.PersonID && x.IsActive == true).FirstOrDefault();
      if (model == null)
      {
        model = new MasterContributorJobDetails();
        model.HireDate = DateTime.Now;
        model.JoiningDate = DateTime.Now;
        model.ContributionStartDate = DateTime.Now;
        model.IsActive = true;
      }
      ViewBag.MinimumHiringAge = contributorPersonalDetails.minHiringAge;
      //ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.Name = (contributorPersonalDetails.PrefixId == null ? "" : db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Select(x => x.Name).FirstOrDefault()) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.DepartmentId = new SelectList(db.MasterDepartment.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.DepartmentId);
      ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true), "Id", "Name", model.DesignationId);
      //ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true), "Id", "EmployerName", contributorPersonalDetails.EmployerID);
      ViewBag.EmployerID = contributorPersonalDetails.EmployerID;
      ViewBag.EmployerName = contributorPersonalDetails.EmployerName;
      ViewBag.GradeId = new SelectList(db.MasterGrade.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.GradeId);
      //ViewBag.JobTitleID = new SelectList(db.MasterJobTitle.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.JobTitleID);
      ViewBag.DateOfBirth = contributorPersonalDetails.DateOfBirth;
      ViewBag.FirstAppointmentDated = contributorPersonalDetails.FirstAppointmentDate;
      ViewBag.LastAppointmentDated = contributorPersonalDetails.LastAppointmentDate;
      ViewBag.PersonId = contributorPersonalDetails.PersonID;
      ViewBag.CId = Id;

      var EmployerType = from EmployerType e in Enum.GetValues(typeof(EmployerType))
                         select new { Id = (int)e, Name = e.ToString() };
      ViewBag.EmployerType = new SelectList(EmployerType.OrderBy(x => x.Name), "Id", "Name", model.EmployerType);

      #region rejoin
      ViewBag.Rejoinee = "";
      if (db.MasterContributor.Where(x => x.PersonID == contributorPersonalDetails.PersonID).Any())
      {
        var temp = db.MasterContributor.Where(x => x.PersonID == contributorPersonalDetails.PersonID);
        ViewBag.Rejoinee = temp.Max(x => x.RetirementOrResignationDate);
      }
      #endregion
      if (contributorPersonalDetails.JobStatusID == (int)jobStatus.DeathInService || ((contributorPersonalDetails.JobStatusID == (int)jobStatus.Pensioner || contributorPersonalDetails.JobStatusID == (int)jobStatus.Resignee) && (contributorPersonalDetails.DateOfBirth == null ? Convert.ToDateTime("01/01/1900") : contributorPersonalDetails.DateOfBirth) > Convert.ToDateTime("01/01/1900")))
      {
        ViewBag.ActiveContributor = "No";
      }
      if ((Request.ServerVariables["HTTP_REFERER"] + "").Contains("errorsInContributorDetails"))
        ViewBag.RequestFrom = true;
      else
        ViewBag.RequestFrom = false;

      return View(model);
    }

    // POST: JobDetails/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddJobDetails(MasterContributorJobDetails jobDetails)
    {
      //if (!db.MasterContributor.Where(x => x.PersonID == jobDetails.PersonID && x.JobStatusID != 1).Any())
      if (!db.MasterContributor.Where(x => x.PersonID == jobDetails.PersonID && (x.JobStatusID == (int)jobStatus.DeathInService || ((x.JobStatusID == (int)jobStatus.Pensioner || x.JobStatusID == (int)jobStatus.Resignee) && (x.DateOfBirth == null ? DbFunctions.CreateDateTime(1990, 1, 1, 0, 0, 0) : x.DateOfBirth) > DbFunctions.CreateDateTime(1990, 1, 1, 0, 0, 0)))).Any())
      {
        if (ModelState.IsValid)
        {
          int Id = db.MasterContributorJobDetails.Where(x => x.PersonID == jobDetails.PersonID && x.IsActive == true).Select(x => x.Id).FirstOrDefault();
          if (Id != 0)
          {
            jobDetails.Id = Id;
            jobDetails.IsActive = true;
            db.Entry(jobDetails).State = EntityState.Modified;
          }
          else
          {
            jobDetails.IsActive = true;
            db.Entry(jobDetails).State = EntityState.Added;
          }
          int result = db.SaveChanges();
          if (result > 0)
          {
            TempData["success"] = "Job Details Updated Successfully";
          }


          int id = db.MasterContributor.Where(x => x.PersonID == jobDetails.PersonID && x.IsActive == true).Select(x => x.Id).FirstOrDefault();
          if (id != 0)
          {
            MasterContributor model = db.MasterContributor.Find(id);
            model.ExpectedRetirementDate = Convert.ToDateTime(Request["ExpectedRetirementDate"]);
            model.FirstAppointmentDate = jobDetails.JoiningDate;
            if (Request["LastAppointmentDate"] != "" && Request["LastAppointmentDate"] != null)
            {
              model.LastAppointmentDate = Convert.ToDateTime(Request["LastAppointmentDate"]);
            }
            model.SalaryAmount = jobDetails.PresentSalary;
            model.MonthlySalary = Math.Round(((jobDetails.PresentSalary) / 12), 2, MidpointRounding.AwayFromZero);
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
          }
          var tempActive = db.ContributorSalaryHistory.Where(x => x.PersonID == jobDetails.PersonID && x.IsActive == true).ToList();
          if (tempActive != null && tempActive.Count > 0)
          {
            var pActive = db.ContributorSalaryHistory.Where(x => x.PersonID == jobDetails.PersonID && x.Active == true).FirstOrDefault();
            if (pActive != null)
            {
              pActive.Active = false;
              db.Entry(pActive).State = EntityState.Modified;
              db.SaveChanges();
            }
            var item = tempActive.Where(x => x.SalaryAmount == jobDetails.PresentSalary && x.IsActive == true).FirstOrDefault();
            if (item != null)
            {
              item.Active = true;
              db.Entry(pActive).State = EntityState.Modified;
              db.SaveChanges();
            }
            else
            {
              var salaryHistory = new ContributorSalaryHistory();
              salaryHistory.PersonID = jobDetails.PersonID;
              salaryHistory.Active = true;
              salaryHistory.IsActive = true;
              salaryHistory.EffectiveDate = DateTime.Now;
              salaryHistory.SalaryAmount = jobDetails.PresentSalary;
              db.Entry(salaryHistory).State = EntityState.Added;
              db.SaveChanges();
            }
          }
          else
          {
            var salaryHistory = new ContributorSalaryHistory();
            salaryHistory.PersonID = jobDetails.PersonID;
            salaryHistory.Active = true;
            salaryHistory.IsActive = true;
            salaryHistory.Default = true;
            salaryHistory.EffectiveDate = DateTime.Now;
            salaryHistory.SalaryAmount = jobDetails.PresentSalary;
            db.Entry(salaryHistory).State = EntityState.Added;
            db.SaveChanges();
          }


          MasterContributor contributorPersonalDetail = db.MasterContributor.Where(x => x.PersonID == jobDetails.PersonID && x.IsActive == true).FirstOrDefault();
          if (Request["RequestFrom"] == "True")
            return RedirectToAction("errorsInContributorDetails");
          else
            return RedirectToAction("AddJobDetails", new { Id = contributorPersonalDetail.Id });
        }
      }
      else
      {
        TempData["error"] = "You Are Not An Active Contributor, So Details Can't Be Edited ";
      }
      ViewBag.PersonId = jobDetails.PersonID;
      var contributorPersonalDetails = (from c in db.MasterContributor
                                        join e in db.MasterEmployer on c.EmployerID equals e.Id
                                        join et in db.MasterEmployerType on e.EmployerTypeID equals et.Id
                                        where c.PersonID == jobDetails.PersonID
                                        select new { minHiringAge = et.MinimumHiringAge, c.PersonID, c.PrefixId, c.FirstName, c.MidName, c.LastName, c.FirstAppointmentDate, c.LastAppointmentDate, c.DateOfBirth, c.Id, c.Employer.EmployerName, c.EmployerID }).FirstOrDefault();

      //MasterContributor contributorPersonalDetails = db.MasterContributor.Where(x => x.PersonID == jobDetails.PersonID).FirstOrDefault();

      ViewBag.MinimumHiringAge = contributorPersonalDetails.minHiringAge;
      //ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.Name = (contributorPersonalDetails.PrefixId == null ? "" : db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Select(x => x.Name).FirstOrDefault()) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.DepartmentId = new SelectList(db.MasterDepartment.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", jobDetails.DepartmentId);
      ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true), "Id", "Name", jobDetails.DesignationId);
      //ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true), "Id", "EmployerName", jobDetails.EmployerID);
      ViewBag.EmployerID = contributorPersonalDetails.EmployerID;
      ViewBag.EmployerName = contributorPersonalDetails.EmployerName;
      ViewBag.GradeId = new SelectList(db.MasterGrade.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", jobDetails.GradeId);
      //ViewBag.JobTitleID = new SelectList(db.MasterJobTitle.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", jobDetails.JobTitleID);
      ViewBag.CId = contributorPersonalDetails.Id;
      ViewBag.DateOfBirth = contributorPersonalDetails.DateOfBirth;
      ViewBag.FirstAppointmentDated = contributorPersonalDetails.FirstAppointmentDate;
      ViewBag.LastAppointmentDated = contributorPersonalDetails.LastAppointmentDate;

      var EmployerType = from EmployerType e in Enum.GetValues(typeof(EmployerType))
                         select new { Id = (int)e, Name = e.ToString() };
      ViewBag.EmployerType = new SelectList(EmployerType.OrderBy(x => x.Name), "Id", "Name", jobDetails.EmployerType);
      return View(jobDetails);
    }

    // GET: JobDetails/Edit/5
    //public ActionResult EditJobDetails(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  MasterContributorJobDetails jobDetails = db.MasterContributorJobDetails.Find(id);
    //  if (jobDetails == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  ViewBag.PersonId = jobDetails.PersonID;
    //  MasterContributor contributorPersonalDetails = db.MasterContributor.Where(x => x.PersonID == jobDetails.PersonID && x.IsActive == true).FirstOrDefault();
    //  ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
    //  ViewBag.DepartmentId = new SelectList(db.MasterDepartment.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", jobDetails.DepartmentId);
    //  ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", jobDetails.DesignationId);
    //  ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", jobDetails.EmployerID);
    //  ViewBag.GradeId = new SelectList(db.MasterGrade.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", jobDetails.GradeId);
    //  ViewBag.JobTitleID = new SelectList(db.MasterJobTitle.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", jobDetails.JobTitleID);
    //  var EmployerType = from EmployerType e in Enum.GetValues(typeof(EmployerType))
    //                     select new { Id = (int)e, Name = e.ToString() };
    //  ViewBag.EmployerType = new SelectList(EmployerType.OrderBy(x => x.Name), "Id", "Name", jobDetails.EmployerType);
    //  return View(jobDetails);
    //}

    //// POST: JobDetails/Edit/5
    //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult EditJobDetails([Bind(Include = "Id,PersonId,EmployerID,EmploymentType,DepartmentId,DesignationId,JobTitle,GradeId,HireDate,PresentSalary,JobDescription,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterContributorJobDetails jobDetails)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.Entry(jobDetails).State = EntityState.Modified;
    //    db.SaveChanges();
    //    return RedirectToAction("JobDetailsList", new { Id = jobDetails.PersonID });
    //  }
    //  ViewBag.PersonId = jobDetails.PersonID;
    //  MasterContributor contributorPersonalDetails = db.MasterContributor.Where(x => x.PersonID == jobDetails.PersonID && x.IsActive == true).FirstOrDefault();
    //  ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
    //  ViewBag.DepartmentId = new SelectList(db.MasterDepartment.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", jobDetails.DepartmentId);
    //  ViewBag.DesignationId = new SelectList(db.MasterDesignation.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", jobDetails.DesignationId);
    //  ViewBag.EmployerID = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", jobDetails.EmployerID);
    //  ViewBag.GradeId = new SelectList(db.MasterGrade.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", jobDetails.GradeId);
    //  ViewBag.JobTitleID = new SelectList(db.MasterJobTitle.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", jobDetails.JobTitleID);
    //  var EmployerType = from EmployerType e in Enum.GetValues(typeof(EmployerType))
    //                     select new { Id = (int)e, Name = e.ToString() };
    //  ViewBag.EmployerType = new SelectList(EmployerType.OrderBy(x => x.Name), "Id", "Name", jobDetails.EmployerType);
    //  return View(jobDetails);
    //}

    //// GET: JobDetails/Delete/5
    //public ActionResult DeleteJobDetails(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  MasterContributorJobDetails jobDetails = db.MasterContributorJobDetails.Find(id);
    //  ViewBag.PersonId = jobDetails.PersonID;
    //  if (jobDetails == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(jobDetails);
    //}

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }




    //----------------------------------------------------------
    //get method
    public ActionResult ContributionExcelUpload(string BeneficiariesType = "", string ACCOUNT_STATUS = "", string gender = "")
    {
      //Session["PensionAndRefundList"] = null;
      //if (Session["excellist"] != null)
      //{
      //  ViewBag.PensionRefund = "";
      //  List<ExcelFileViewModel> list = ((List<ExcelFileViewModel>)Session["excellist"]);
      //  list = list.Where(x => x.error != null).ToList();
      //  if (list.Any(x => x.error.Contains("Pension") && x.IsActive != false))
      //  {
      //    ViewBag.PensionRefund = "Yes";
      //    list = list.Where(x => x.error.Contains("Pension")).ToList();
      //    List<PensionOrRefundViewModel> List = new List<PensionOrRefundViewModel>();
      //    List<MasterContributor> contributor = db.MasterContributor.ToList();
      //    List<RefundPaidDetails> Refund = db.RefundPaidDetails.ToList();
      //    foreach (var item in list)
      //    {
      //      PensionOrRefundViewModel log = new PensionOrRefundViewModel();
      //      var cPersonId = contributor.Where(x => x.PersonID == item.PersonId).Select(x => new { x.Id, Status = x.Status.Name, x.PersonID, Name = x.FirstName + " " + x.MidName + " " + x.LastName }).FirstOrDefault();
      //      if (cPersonId.Status == "Resignee")
      //      {
      //        #region refund
      //        RefundPaidDetails refDetails = Refund.Where(x => x.PersonID == cPersonId.Id.ToString()).FirstOrDefault();
      //        log.ApplicationType = "R";
      //        log.Id = refDetails.RefundID.ToString();//refundpaiddetails Id
      //        log.contributorId = cPersonId.Id.ToString();
      //        log.Name = cPersonId.Name;
      //        log.OldAmount = refDetails.RefundAmt;
      //        log.Month = item.Month;
      //        log.Year = item.Year;
      //        //log.NewAmount = refDetails.RefundAmt;
      //        #endregion
      //      }
      //      else
      //      {
      //        #region Pensioner
      //        DVOMasterEmployee objSearchCriteriaDVOMasterEmployeeModel = new DVOMasterEmployee();
      //        objSearchCriteriaDVOMasterEmployeeModel.PersonID = cPersonId.PersonID;
      //        var checkRecord = SearchEmployeeInformation(objSearchCriteriaDVOMasterEmployeeModel);
      //        if (checkRecord.Any())
      //        {
      //          log.Id = checkRecord.FirstOrDefault().EmplCode;//pensioner emplcode
      //          log.contributorId = cPersonId.Id.ToString();
      //          log.Name = cPersonId.Name;
      //          log.ApplicationType = "P";
      //          log.OldAmount = checkRecord.FirstOrDefault().AnnualSalary;
      //          log.Month = item.Month;
      //          log.Year = item.Year;
      //          //log.NewAmount = checkRecord.FirstOrDefault().AnnualSalary;
      //        }
      //        #endregion
      //      }
      //      log.recalculated = false;
      //      List.Add(log);
      //    }
      //    Session["PensionAndRefundList"] = List;
      //  }
      //}
      var defaultItem = new SelectListItem { Value = "ALL", Text = "ALL | ALL" };

      var emplCodeList = db.MasterEmpType
          .Where(x => x.Type_Code != null)
          .Select(x => new SelectListItem
          {
            Value = x.Description,
            Text = x.Type_Code + " | " + x.Description
          })
          .ToList();
      if (string.IsNullOrEmpty(BeneficiariesType))
      {
        defaultItem.Selected = true;
        emplCodeList.Insert(0, defaultItem);
      }
      else
      {
        if (!emplCodeList.Any(x => x.Value == BeneficiariesType))
        {
          emplCodeList.Insert(0, new SelectListItem { Value = BeneficiariesType, Text = BeneficiariesType, Selected = true });
        }
        else
        {
          foreach (var item in emplCodeList)
          {
            item.Selected = item.Value == BeneficiariesType;
          }
        }
        emplCodeList.Insert(0, new SelectListItem { Value = "ALL", Text = "ALL" });
        //emplCodeList.Insert(0, new SelectListItem { Value = BeneficiariesType, Text = BeneficiariesType, Selected = true });

      }
      ViewBag.EmplCode = emplCodeList;
      //var defaultItemsa = new SelectListItem { Value = "ALL", Text = "ALL | ALL" };

      //var emplgenderList = db.MasterEmployees
      //    .Where(x => x.gender != null)
      //    .Select(x => new SelectListItem
      //    {
      //      Value = x.gender,

      //    }).AsEnumerable().GroupBy(p=>p.Value).Select(n=>n.FirstOrDefault()).ToList();
      //if (string.IsNullOrEmpty(gender))
      //{
      //  defaultItem.Selected = true;
      //  emplgenderList.Insert(0, defaultItem);
      //}
      //else
      //{
      //  emplgenderList.Insert(0, new SelectListItem { Value = gender, Text = gender, Selected = true });

      //}
      //ViewBag.Gender = emplgenderList;
      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "ContributorPersonalDetails" && x.ActionName == "ContributionExcelUpload")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Data Upload", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.AddPermission = model.AddPermssion;
      }

      ViewBag.Group = GetUsersAssignedLocations();

      ViewBag.EmployerTotalContribution = "EC$ 0.00";
      ViewBag.ContributorTotalContribution = "EC$ 0.00";
      ViewBag.SystemEmployerTotalContribution = "EC$ 0.00";
      ViewBag.SystemContributorTotalContribution = "EC$ 0.00";
      Session["excellist"] = null;
      ViewBag.ACCOUNT_STATUS = ACCOUNT_STATUS;
      ViewBag.Redirectgender = gender;
      ViewBag.RedirectBeneficiariesType = BeneficiariesType;
      return View();
    }
    public ActionResult CheckForExistingRecordAjax(string FileName)
    {
      if (!string.IsNullOrEmpty(FileName))
      {
        Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
        // Define the path for sql

        //var region = GetRegionName();
        var region = GetRegionName();
        var directoryName = region == "KASHMIR REGION" ? "K_Uploads" : "J_Uploads";
        //string FilePath = ftpSetting["localFilePath"] + $"\\DataFiles\\{directoryName}\\" + FileName;

        string FilePath = Helper.Helper.CheckForExistingRecordPath(region, directoryName, FileName);


        //string FilePath = ftpSetting["localFilePath"] + "\\DataFiles\\Uploads\\" + FileName;
        //string serverMapPath = Server.MapPath("~/DataFile/Uploads");
        //string FilePath = Path.Combine(serverMapPath, FileName);
        TempData["Isfilesave"] = "True";
        int UserId = AppUserManager.GetUserId();
        int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
        List<string> list = BLLPayEmployeeTypes.CHeckExistingRecord(FilePath, UserId, RoleId);

        int userid = AppUserManager.GetUserId();
        var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
        var model = (from c in db.SecModule.Where(x => x.ControllerName == "ContributorPersonalDetails" && x.ActionName == "ContributionExcelUpload")
                     join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                     from p in ps.DefaultIfEmpty()
                     select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Data Upload", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

        if (model != null)
        {
          ViewBag.AddPermission = model.AddPermssion;
        }

        return Json(JsonConvert.SerializeObject(list.FirstOrDefault()), JsonRequestBehavior.AllowGet);
      }
      else
      {
        return Json("error", JsonRequestBehavior.AllowGet);
      }
    }
    //private string checkFileName()
    //{
    //  return "";
    //}
    #region excelUpload
    //[HttpPost]
    //public ActionResult ContributionExcelUpload(HttpPostedFileBase file, List<ExcelFileViewModel> model)
    //{
    //  int Year;
    //  string error = "";
    //  DateTime cDate;
    //  string path = "";
    //  string fileName = "";
    //  string[] tempValue;
    //  string employerName;
    //  var list = new List<ExcelFileViewModel>();
    //  var tempListForDuplicate = new List<ExcelFileViewModel>();
    //  var docfiles = new List<string>();
    //  try
    //  {
    //    if (file != null && file.ContentLength > 0)
    //    {
    //      #region errorinFormat
    //      fileName = Path.GetFileName(file.FileName);
    //      tempValue = fileName.Split('_');
    //      if (tempValue.Length < 2)
    //      {
    //        TempData["error"] = "Incorrect File Format, It Should be Like EmployerName_Month_Year. Please correct and re-upload";
    //        return View(model);
    //      }

    //      if (Int32.TryParse(Regex.Replace(tempValue[2], "[^0-9]", ""), out Year))
    //      { }
    //      else
    //      {
    //        error = "Check Year";
    //      }
    //      string MonthName = tempValue[1];
    //      if (!db.MasterMonthName.Where(x => x.Name.ToLower() == MonthName.ToLower()).Any())
    //      {
    //        error = "Check Month Name";
    //      }

    //      #endregion

    //      if (error.Length < 1)
    //      {
    //        DateTime.TryParse("01/" + tempValue[1] + "/" + Year, out cDate);
    //        DateTime todayDate = DateTime.Now;
    //        DateTime retDate = DateTime.Now;
    //        if (todayDate.Date >= cDate.Date)
    //        {
    //          employerName = tempValue[0];
    //          var result = db.MasterEmployer.Where(x => x.EmployerName.ToLower() == employerName.ToLower() && x.IsActive == true).FirstOrDefault();
    //          if (result != null)
    //          {
    //            #region CreateDirectoryAndSaveFile
    //            // Create a reference to a directory.
    //            DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/MonthlyReceivingFiles"));
    //            // Create the directory only if it does not already exist. 
    //            if (di.Exists == false) di.Create();
    //            // Create a subdirectory in the directory just created.
    //            DirectoryInfo dis = di.GetDirectories(result.EmployerName).Any() ? di.GetDirectories(result.EmployerName).FirstOrDefault() : di.CreateSubdirectory(result.EmployerName);
    //            dis = dis.GetDirectories(tempValue[1]).Any() ? dis.GetDirectories(tempValue[1]).FirstOrDefault() : dis.CreateSubdirectory(tempValue[1]);

    //            path = Path.Combine(dis.FullName, fileName);
    //            file.SaveAs(path);
    //            docfiles.Add(path);
    //            #endregion

    //            //var contributor = db.MasterContributor.Where(x => x.JobStatusID == 1 && x.IsActive == true).ToList();
    //            var contributor = db.MasterContributor.Where(x => x.IsActive == true).ToList();
    //            var contributorList = contributor.Where(x => x.EmployerID == result.Id && (x.JobStatusID == 1 || x.JobStatusID == 3)).ToList();//&& x.JobStatusID == 3
    //            string pathToExcelFile = "" + docfiles[0];
    //            var excel = new ExcelQueryFactory(pathToExcelFile);
    //            var allRows = excel.WorksheetNoHeader();
    //            var excelRowList = allRows.ToList();

    //            /*string errorinExcel = "";
    //            if (excelRowList[0][0].Value.ToString().ToLower() == "personid")
    //            { errorinExcel = excelRowList[0][0].Value.ToString().ToLower(); }
    //            else if (excelRowList[0][2].Value.ToString().ToLower() == "source")
    //            { errorinExcel = excelRowList[0][2].Value.ToString().ToLower(); }
    //            else if (excelRowList[0][1].Value.ToString().ToLower() == "contributorname")
    //            { errorinExcel = excelRowList[0][1].Value.ToString().ToLower(); }
    //            else if (excelRowList[0][3].Value.ToString().ToLower() == "salaryamount")
    //            { errorinExcel = excelRowList[0][3].Value.ToString().ToLower(); }
    //            else if (excelRowList[0][4].Value.ToString().ToLower() == "contributorcontribution")
    //            { errorinExcel = excelRowList[0][4].Value.ToString().ToLower(); }
    //            else if (excelRowList[0][5].Value.ToString().ToLower() == "employercontribution")
    //            { errorinExcel = excelRowList[0][5].Value.ToString().ToLower(); }*/


    //            if (excelRowList[0][0].Value.ToString().ToLower().Trim() == "personid" && excelRowList[0][2].Value.ToString().ToLower().Trim() == "source" && excelRowList[0][1].Value.ToString().ToLower().Trim() == "contributorname" && excelRowList[0][3].Value.ToString().ToLower().Trim() == "salaryamount" && excelRowList[0][4].Value.ToString().ToLower().Trim() == "contributorcontribution" && excelRowList[0][5].Value.ToString().ToLower().Trim() == "employercontribution")
    //            {
    //              int maxCheckCount = 0;
    //              for (int i = 1; i < excelRowList.Count(); i++)
    //              {
    //                #region breakLoopWhenPersonIdIsBlankForContinous20Rows
    //                string personID = excelRowList[i][0].Value.ToString() == "" ? "" : excelRowList[i][0].Value.ToString().Trim();
    //                if (String.IsNullOrWhiteSpace(personID))//to break when blank personid found
    //                {
    //                  if (maxCheckCount == 20) break;

    //                  maxCheckCount++;
    //                  continue;
    //                }
    //                #endregion

    //                else
    //                {
    //                  maxCheckCount = 0;
    //                  var master = new ExcelFileViewModel();
    //                  master.SourceName = excelRowList[i][2].Value.ToString().Trim() == "" ? "Salary" : excelRowList[i][2].Value.ToString();
    //                  //string[] tempName;
    //                  var ContributorName = excelRowList[i][1].Value.ToString() == "" ? "" : excelRowList[i][1].Value.ToString().Trim();
    //                  //tempName = ContributorName.Split(null);
    //                  //string TempFirstName = tempName.FirstOrDefault().ToLower();
    //                  //string TempLastName = tempName.Count() == 3 && tempName[2] == "" ? tempName[1].ToLower() : tempName.LastOrDefault().ToLower();
    //                  var checkContributor = contributorList.Where(x => x.PersonID == personID).FirstOrDefault();
    //                  string CFirstName = checkContributor == null ? "" : checkContributor.FirstName == null ? "" : checkContributor.FirstName.ToLower();
    //                  string CLastName = checkContributor == null ? "" : checkContributor.LastName == null ? "" : checkContributor.LastName.ToLower();

    //                  var sysName = (CFirstName + " " + CLastName);
    //                  if (checkContributor != null && (ContributorName.ToLower().Contains(CFirstName.ToLower()) || ContributorName.ToLower().Contains(CLastName.ToLower())))//if contributor match
    //                  {
    //                    var jobDetails = db.MasterContributorJobDetails.Where(x => x.PersonID == checkContributor.PersonID && x.IsActive == true).FirstOrDefault();
    //                    #region CheckJobDetails
    //                    if (jobDetails != null)
    //                    {
    //                      #region contributionAfterJoiningDate
    //                      if (checkContributor.JobStatusID != 1)
    //                      {
    //                        if (checkContributor.RetirementOrResignationDate.HasValue)
    //                        {
    //                          retDate = checkContributor.RetirementOrResignationDate.Value;
    //                        }
    //                      }
    //                      DateTime JoiningDate;
    //                      DateTime.TryParse(jobDetails.JoiningDate.Month + "/01/" + jobDetails.JoiningDate.Year, out JoiningDate);
    //                      if (JoiningDate <= cDate.Date && cDate.Date <= retDate)
    //                      {
    //                        master.IsActive = true;
    //                        master.ContributorName = checkContributor.FullName;
    //                        decimal SystemSalaryAmount = Math.Round(checkContributor.SalaryAmount / 12, 2);
    //                        master.SystemSalaryAmount = SystemSalaryAmount;
    //                        decimal SystemContributorContribution = Math.Round((SystemSalaryAmount * checkContributor.PFRate) / 100, 2);
    //                        master.SystemContributorContribution = SystemContributorContribution;
    //                        decimal SalaryAmount = Math.Round(decimal.Parse(excelRowList[i][3].Value.ToString(), NumberStyles.Currency), 2);
    //                        decimal epf = result.PFRate;
    //                        decimal SystemEmployerContribution = Math.Round(SystemSalaryAmount * epf / 100, 2);
    //                        master.SystemEmployerContribution = SystemEmployerContribution;
    //                        decimal ContributorContribution = Math.Round(decimal.Parse(excelRowList[i][4].Value.ToString(), NumberStyles.Currency), 2);
    //                        master.ContributorContribution = ContributorContribution;
    //                        decimal EmployerContribution = Math.Round(decimal.Parse(excelRowList[i][5].Value.ToString(), NumberStyles.Currency), 2);
    //                        master.EmployerContribution = EmployerContribution;
    //                        decimal tempemp = Math.Round(SalaryAmount * epf / 100, 2);
    //                        master.TempEmployerContribution = tempemp;
    //                        decimal tempcont = Math.Round((SalaryAmount * checkContributor.PFRate) / 100, 2);
    //                        master.TempContributorContribution = tempcont;
    //                        if (SystemSalaryAmount != SalaryAmount)
    //                        {
    //                          if ((ContributorContribution != tempcont) || (EmployerContribution != tempemp))
    //                          {
    //                            master.IsActive = false;
    //                            master.error += checkContributor.FirstName + " " + checkContributor.LastName + "(" + checkContributor.PersonID + ") Employee’s Contribution or Employer Contribution is Not as per defined PF Rate ";
    //                          }
    //                        }
    //                        else
    //                        {
    //                          if ((SystemEmployerContribution != EmployerContribution) || (SystemContributorContribution != ContributorContribution))
    //                          {
    //                            master.IsActive = false;
    //                            master.error += checkContributor.FirstName + " " + checkContributor.LastName + "(" + checkContributor.PersonID + ") Employee’s Contribution & Employer Contribution is Not as per defined System Contribution ";
    //                          }
    //                        }
    //                        if (checkContributor.JobStatusID == (int)jobStatus.Pensioner)
    //                        {
    //                          if (master.error != null && master.error != "")
    //                            master.error += " & Please ReCalCulate Pension ";
    //                          else
    //                            master.error = checkContributor.FirstName + " " + checkContributor.LastName + "(" + checkContributor.PersonID + ") Status:-" + checkContributor.Status.Name + " Please ReCalCulate Pension ";
    //                        }
    //                      }
    //                      else
    //                      {
    //                        master.error = checkContributor.FirstName + " " + checkContributor.LastName + "(" + checkContributor.PersonID + ") Contribution Cannot be before Joining Date ";
    //                        if (checkContributor.JobStatusID == (int)jobStatus.Pensioner)
    //                        {
    //                          master.error = checkContributor.FirstName + " " + checkContributor.LastName + "(" + checkContributor.PersonID + ") Contribution Cannot be before Joining Date and After Retirement Date";
    //                        }
    //                        master.IsActive = false;
    //                        master.ContributorName = excelRowList[i][1].Value.ToString();
    //                        master.SystemSalaryAmount = Math.Round(checkContributor.SalaryAmount / 12, 2);
    //                        master.SystemContributorContribution = Math.Round(((checkContributor.SalaryAmount * checkContributor.PFRate) / 1200), 2);
    //                        master.SystemEmployerContribution = Math.Round(((checkContributor.SalaryAmount * checkContributor.Employer.PFRate) / 1200), 2);
    //                        //master.SystemSalaryAmount = 0;
    //                        //master.SystemContributorContribution = 0;
    //                        master.SalaryAmount = decimal.Parse(excelRowList[i][3].Value.ToString(), NumberStyles.Currency);
    //                        master.ContributorContribution = decimal.Parse(excelRowList[i][4].Value.ToString(), NumberStyles.Currency);
    //                        //master.SystemEmployerContribution = 0;
    //                        master.EmployerContribution = decimal.Parse(excelRowList[i][5].Value.ToString(), NumberStyles.Currency);

    //                      }
    //                      #endregion
    //                    }
    //                    else
    //                    {
    //                      master.error = checkContributor.FirstName + " " + checkContributor.MidName + " " + checkContributor.LastName + "(" + checkContributor.PersonID + ") Job Details Not Available";
    //                      master.IsActive = false;
    //                      master.ContributorName = excelRowList[i][1].Value.ToString();
    //                      master.SystemSalaryAmount = Math.Round(checkContributor.SalaryAmount / 12, 2);
    //                      master.SystemContributorContribution = Math.Round(((checkContributor.SalaryAmount * checkContributor.PFRate) / 1200), 2);
    //                      master.SystemEmployerContribution = Math.Round(((checkContributor.SalaryAmount * checkContributor.Employer.PFRate) / 1200), 2);
    //                      //master.SystemSalaryAmount = 0;
    //                      //master.SystemContributorContribution = 0;
    //                      master.SalaryAmount = decimal.Parse(excelRowList[i][3].Value.ToString(), NumberStyles.Currency);
    //                      master.ContributorContribution = decimal.Parse(excelRowList[i][4].Value.ToString(), NumberStyles.Currency);
    //                      //master.SystemEmployerContribution = 0;
    //                      master.EmployerContribution = decimal.Parse(excelRowList[i][5].Value.ToString(), NumberStyles.Currency);
    //                    }
    //                    #endregion

    //                  }
    //                  else
    //                  {
    //                    master.IsActive = false;
    //                    master.ContributorName = excelRowList[i][1].Value.ToString();
    //                    master.SystemSalaryAmount = 0;
    //                    master.SystemContributorContribution = 0;
    //                    master.SalaryAmount = decimal.Parse(excelRowList[i][3].Value.ToString(), NumberStyles.Currency);
    //                    master.ContributorContribution = decimal.Parse(excelRowList[i][4].Value.ToString(), NumberStyles.Currency);
    //                    master.SystemEmployerContribution = 0;
    //                    master.EmployerContribution = decimal.Parse(excelRowList[i][5].Value.ToString(), NumberStyles.Currency);
    //                    if (checkContributor != null)
    //                    {
    //                      if (!ContributorName.ToLower().Contains(CFirstName.ToLower()))
    //                      {
    //                        master.SystemSalaryAmount = Math.Round(checkContributor.SalaryAmount / 12, 2);
    //                        master.SystemContributorContribution = Math.Round(((checkContributor.SalaryAmount * checkContributor.PFRate) / 1200), 2);
    //                        master.SystemEmployerContribution = Math.Round(((checkContributor.SalaryAmount * checkContributor.Employer.PFRate) / 1200), 2);
    //                        master.error = "JKPS ID(" + personID + ") Contributor Name is incorrect as it should be " + checkContributor.FirstName + " " + checkContributor.LastName;
    //                      }
    //                    }
    //                    else
    //                    {
    //                      master.error = "JKPS ID(" + personID + ") Doesnot Exist";
    //                      if (contributor.Where(x => x.PersonID == personID).Any())
    //                      {
    //                        MasterContributor mc = contributor.Where(x => x.PersonID == personID).FirstOrDefault();
    //                        master.SystemSalaryAmount = Math.Round(mc.SalaryAmount / 12, 2);
    //                        master.SystemContributorContribution = Math.Round(((mc.SalaryAmount * mc.PFRate) / 1200), 2);
    //                        master.SystemEmployerContribution = Math.Round(((mc.SalaryAmount * mc.Employer.PFRate) / 1200), 2);
    //                        master.error = "JKPS ID(" + personID + ") Doesnot Belong To this Employer";
    //                      }
    //                    }
    //                  }
    //                  master.EmployerID = result.Id;
    //                  master.EmployerName = result.EmployerName;
    //                  master.Month = tempValue[1];
    //                  master.Year = Year;
    //                  master.FilePath = path;
    //                  master.PersonId = excelRowList[i][0].Value.ToString() == "" ? "" : excelRowList[i][0].Value.ToString().Trim();
    //                  master.SalaryAmount = decimal.Parse(excelRowList[i][3].Value.ToString(), NumberStyles.Currency);// Convert.ToDecimal(excelRowList[i][3].Value);
    //                  list.Add(master);
    //                }
    //              }

    //              if (list.Any())
    //              {
    //                #region DuplicateEntry
    //                var query = list.GroupBy(x => new { x.PersonId, x.SourceName }).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
    //                if (query != null && query.Count > 0)
    //                {
    //                  foreach (var item in list)
    //                  {
    //                    tempListForDuplicate.Add(item);
    //                  }
    //                  list.Clear();
    //                  foreach (var item in tempListForDuplicate)
    //                  {
    //                    if (query.Where(x => x.PersonId == item.PersonId && x.SourceName == item.SourceName).Any())
    //                    {
    //                      item.IsActive = false;
    //                      item.error = item.PersonId + "( " + item.ContributorName + " ) have Duplicate JKPS ID";
    //                    }
    //                    list.Add(item);
    //                  }
    //                }
    //                #endregion

    //                if (list.Any(x => x.IsActive == false))
    //                {
    //                  ViewBag.InValid = "false";
    //                }
    //                ViewBag.Employer = list[0].EmployerName;
    //                ViewBag.EId = result == null ? "InValid Employer" : result.UniqueID;
    //                ViewBag.Month = list[0].Month;
    //                ViewBag.Year = list[0].Year;
    //                Session["excellist"] = list;
    //                //FileInfo fi = new FileInfo(path);
    //                //if (fi.Exists) fi.Delete();
    //                ViewBag.EmployerTotalContribution = "EC$ " + Convert.ToDecimal(list.Sum(x => x.ContributorContribution)).ToString("#,##0.00");
    //                ViewBag.ContributorTotalContribution = "EC$ " + Convert.ToDecimal(list.Sum(x => x.EmployerContribution)).ToString("#,##0.00");

    //                #region categorizethemIntoNew/Finalize/Unfinalize
    //                List<ExcelFileViewModel> Verify = new List<ExcelFileViewModel>();
    //                List<ExcelFileViewModel> tempVerify = new List<ExcelFileViewModel>();
    //                List<ExcelFileViewModel> temp2Verify = new List<ExcelFileViewModel>();
    //                var header = (from sheetHeader in db.ContributonSheetHeader.Where(x => x.IsActive == true) join sheetHeaderFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true) on sheetHeader.Id equals sheetHeaderFinalize.ContributonSheetHeaderId into rt from finalize in rt.DefaultIfEmpty() where sheetHeader.Month == MonthName && sheetHeader.Year == Year && sheetHeader.EmployerId == result.Id select new { headerId = sheetHeader == null ? 0 : sheetHeader.Id, headerFinalize = finalize == null ? 0 : finalize.Id }).FirstOrDefault();
    //                if (header == null)
    //                {
    //                  var myAnonInstance = new
    //                  {
    //                    headerId = 0,
    //                    headerFinalize = 0,
    //                  };

    //                  header = myAnonInstance;
    //                }
    //                if (header != null && header.headerId == 0)
    //                {
    //                  list = list.Where(x => x.status == null).Select(c => { c.status = "New Entry"; return c; }).ToList();
    //                }
    //                else if (header != null && header.headerFinalize == 0)
    //                {
    //                  Verify = db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == header.headerId && x.IsActive == true).Select(x => new ExcelFileViewModel
    //                  {
    //                    Contributor = x.Contributor,
    //                    Year = 0,
    //                    Source = x.Source,
    //                    Final = "0",
    //                    Id = x.Id,
    //                  }).ToList();
    //                  if (Verify.Count > 0)
    //                  {
    //                    List<ExcelFileViewModel> tempList = new List<ExcelFileViewModel>();
    //                    foreach (var item in list)
    //                    {
    //                      var TempData = Verify.Where(x => x.Source.Name.ToLower() == item.SourceName.ToLower() && x.Contributor.PersonID == item.PersonId).FirstOrDefault();
    //                      if (TempData != null)
    //                      {
    //                        item.status = "Unfinalized";
    //                        if (item.error != null && item.error != "")
    //                          item.error += "& Record Already Exist in Unfinalized Details";
    //                        else
    //                          item.error = item.ContributorName + " (" + item.PersonId + ") Record Already Exist in Unfinalized Details";
    //                      }
    //                      else
    //                      {
    //                        item.status = "New Entry";
    //                      }
    //                      tempList.Add(item);
    //                    }
    //                    list = tempList;
    //                  }
    //                  else
    //                  {
    //                    list = list.Where(x => x.status == null).Select(c => { c.status = "New Entry"; return c; }).ToList();
    //                  }

    //                }
    //                else
    //                {

    //                  Verify = db.ContributonSheetDetailsFinalise.Where(x => x.ContributonSheetHeaderFinaliseID == header.headerFinalize && x.IsActive == true).Select(x => new ExcelFileViewModel { Contributor = x.Contributor, Year = 0, Source = x.Source, Final = "1", Id = x.Id }).ToList();
    //                  tempVerify = db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == header.headerId && x.IsActive == true).Select(x => new ExcelFileViewModel
    //                  {
    //                    Contributor = x.Contributor,
    //                    Year = 0,
    //                    Source = x.Source,
    //                    Final = "0",
    //                    Id = x.Id,
    //                  }).ToList();
    //                  if (Verify.Count > 0)
    //                  {
    //                    List<ExcelFileViewModel> tempList = new List<ExcelFileViewModel>();
    //                    foreach (var item in list)
    //                    {
    //                      var TempData = Verify.Where(x => x.Source.Name.ToLower() == item.SourceName.ToLower() && x.Contributor.PersonID == item.PersonId).FirstOrDefault();
    //                      if (TempData != null)
    //                      {
    //                        item.status = "Finalized Record";
    //                        if (item.error != null && item.error != "")
    //                          item.error += " & Record Already Finalized";
    //                        else
    //                          item.error = item.ContributorName + " (" + item.PersonId + ") Record Already Finalized";
    //                      }
    //                      else
    //                      {
    //                        var TempDat = tempVerify.Where(x => x.Source.Name.ToLower() == item.SourceName.ToLower() && x.Contributor.PersonID == item.PersonId).FirstOrDefault();
    //                        if (TempDat != null)
    //                        {
    //                          item.status = "Unfinalized";
    //                          if (item.error != null && item.error != "")
    //                            item.error += " & Record Already Exist in Unfinalized Details";
    //                          else
    //                            item.error = item.ContributorName + " (" + item.PersonId + ") Record Already Exist in Unfinalized Details";
    //                        }
    //                        else
    //                        {
    //                          item.status = "New Entry";
    //                        }

    //                      }
    //                      tempList.Add(item);
    //                    }
    //                    list = tempList;
    //                  }
    //                  else
    //                  {
    //                    list = list.Where(x => x.status == null).Select(c => { c.status = "New Entry"; return c; }).ToList();
    //                  }


    //                }
    //                ViewBag.FinalizedRecord = list.Where(x => x.status == "Finalized Record").Count();
    //                ViewBag.NewEntry = list.Where(x => x.status == "New Entry").Count();
    //                ViewBag.Unfinalized = list.Where(x => x.status == "Unfinalized").Count();
    //                #endregion



    //                Session["excellist"] = list;
    //              }
    //            }

    //            else
    //            {
    //              ViewBag.Error = "fileformat";
    //            }

    //          }
    //          else
    //          {
    //            TempData["error"] = "This Employer doesn't exist";
    //            //FileInfo fi = new FileInfo(path);
    //            //if (fi.Exists) fi.Delete();
    //          }
    //        }
    //        else
    //        {
    //          TempData["error"] = "Sheet Only Uploaded to Current Month";
    //        }
    //      }
    //      else
    //      {
    //        TempData["error"] = error;
    //      }

    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    TempData["error"] = "Check Uploaded Sheet And Try Again..." + ex.Message;
    //    //FileInfo fi = new FileInfo(path);
    //    //if (fi.Exists) fi.Delete();
    //  }

    //  return View();
    //}
    #endregion

    public string ValidateExcelSheetName(string fileName)
    {
      string error = "";
      int Year;
      string[] tempValue = fileName.Split('_');
      if (tempValue.Length != 3)
        error = "Incorrect File Name, It Should be Like EmployerName_Month_Year. Please correct and re-upload";
      else
      {
        string Month = tempValue[1];
        string year = tempValue[2];
        if (!db.MasterMonthName.Where(x => x.Name.ToLower() == Month.ToLower()).Any())
          error = "Incorrect File Name, Please Check Month Name";
        else if (!(Int32.TryParse(Regex.Replace(year, "[^0-9]", ""), out Year)))
          error = "Incorrect File Name, Please Check Year";
        else if (DateTime.Now.Date < Convert.ToDateTime("01/" + tempValue[1] + "/" + Year))
          error = "Excel Sheet Only Uploaded to current Month";
      }
      return error;
    }
    public List<string> createDirectoryAndSaveFile(string employerName, string month, HttpPostedFileBase file, string fileName)
    {
      var docfiles = new List<string>();
      DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/MonthlyReceivingFiles"));
      // Create the directory only if it does not already exist. 
      if (di.Exists == false) di.Create();
      // Create a subdirectory in the directory just created.
      DirectoryInfo dis = di.GetDirectories(employerName).Any() ? di.GetDirectories(employerName).FirstOrDefault() : di.CreateSubdirectory(employerName);
      dis = dis.GetDirectories(month).Any() ? dis.GetDirectories(month).FirstOrDefault() : dis.CreateSubdirectory(month);

      string path = Path.Combine(dis.FullName, fileName);
      file.SaveAs(path);
      docfiles.Add(path);
      return docfiles;
    }
    public int createSource(string sourceName)
    {
      int id = db.MasterSource.Where(x => x.Name.ToLower() == sourceName.ToLower() && x.IsActive == true).Select(x => x == null ? 0 : x.Id).FirstOrDefault();
      if (id == 0)
      {
        MasterSource entity = new MasterSource();
        entity.Name = sourceName;
        entity.IsActive = true;
        db.Entry(entity).State = EntityState.Added;
        db.SaveChanges();
        id = entity.Id;
      }
      return id;
    }
    public MasterEmployerPFRateDetails GetEmployerPfRateForExcellUpload(DateTime Date, string UniqueId)
    {
      return db.MasterEmployerPFRateDetails.Where(x => x.IsActive && x.UniqueID == UniqueId && x.EffectiveDate <= Date && x.EffectiveEndDate >= Date).OrderByDescending(x => x.EffectiveDate).FirstOrDefault();
    }
    public MasterContributorPFRateDetails GetContributorPfRateForExcellUpload(DateTime Date, string PersonId)
    {
      return db.MasterContributorPFRateDetails.Where(x => x.IsActive && x.PersonID == PersonId && x.EffectiveDate <= Date && x.EffectiveEndDate >= Date).OrderByDescending(x => x.EffectiveDate).FirstOrDefault();
    }

    static DataTable ConvertCsvToDataTable(string fileName)
    {
      DataTable dataTable = new DataTable();
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

        // Create an FTP client
        //WebClient ftpClient = new WebClient();
        //Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
        //ftpClient.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);
        //string path = ftpSetting["ftpServerUrl"] + ("/DataFiles/Uploads/" + fileName);
        //// File path, attampt, dealy in attampt
        //bool isExist = Helper.Helper.FileCheckInFTP(path, 3, 30);
        //if (isExist)
        //{
        //  // Download the file from the FTP server
        //  byte[] fileData = ftpClient.DownloadData(path);
        //  using (Stream memoryStream = new MemoryStream(fileData))
        //  {
        //    using (StreamReader sr = new StreamReader(memoryStream))
        //    {
        //      string[] headers = sr.ReadLine().Split(',');
        //      if (headers.Any())
        //      {
        //        foreach (string header in headers)
        //        {
        //          dataTable.Columns.Add(header);
        //        }
        //      }
        //      else
        //      {
        //        for (int i = 0; i < headers.Length; i++)
        //        {
        //          dataTable.Columns.Add($"Column{i + 1}");
        //        }
        //        sr.BaseStream.Position = 0;
        //      }
        //      while (!sr.EndOfStream)
        //      {
        //        string[] rows = sr.ReadLine().Split(',');
        //        DataRow dataRow = dataTable.NewRow();
        //        for (int i = 0; i < headers.Length; i++)
        //        {
        //          dataRow[i] = rows[i];
        //        }
        //        dataTable.Rows.Add(dataRow);
        //      }
        //    }
        //  }
        //}

      }
      catch (Exception)
      {
        throw;
      }
      return dataTable;

    }
    public static DataTable upConvertExcelToDataTable(string filePath)
    {
      DataTable dataTable = new DataTable();
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
                dataTable.Columns.Add(cell.GetString());
              }
              firstRow = false;
            }
            else
            {
              var dataRow = dataTable.NewRow();
              int i = 0;

              foreach (var cell in row.CellsUsed())
              {
                dataRow[i] = cell.Value.ToString() ?? string.Empty;
                i++;
              }

              dataTable.Rows.Add(dataRow);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error: " + ex.Message);
        return null;
      }


      return dataTable;
    }
    [HttpPost]
    public ActionResult ContributionExcelUpload(HttpPostedFileBase file, List<ExcelFileViewModel> model, string RegionNames, string regiontext)
    {
      string error = "";
      string fileName = "";
      try
      {
        var defaultItem = new SelectListItem { Value = "ALL", Text = "ALL | ALL" };

        var emplCodeList = db.MasterEmpType
            .Where(x => x.Type_Code != null)
            .Select(x => new SelectListItem
            {
              Value = x.Description,
              Text = x.Type_Code + " | " + x.Description
            })
            .ToList();

        emplCodeList.Insert(0, defaultItem); // Insert the default item at the beginning

        int userid = AppUserManager.GetUserId();
        var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
        var AddPermission = (from c in db.SecModule.Where(x => x.ControllerName == "ContributorPersonalDetails" && x.ActionName == "ContributionExcelUpload")
                             join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                             from p in ps.DefaultIfEmpty()
                             select new RoleModuleViewModel { AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion }).FirstOrDefault();

        if (AddPermission != null)
        {
          ViewBag.AddPermission = AddPermission.AddPermssion;
        }
        else
        {
          ViewBag.AddPermission = false;
        }

        ViewBag.EmplCode = emplCodeList;

        //HttpPostedFileBase verifyFile = new HttpPostedFileBase();
        //verifyFile = file;
        // validate excel file
        // Example of reading the file stream
        // Create a byte array to hold the file data
        byte[] fileData;

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

        string VerifyExcelFile = CheckExcelOrCsvFileForInjection.VerifyFile(copiedFile);
        if (VerifyExcelFile != "Valid")
        {
          TempData["error"] = VerifyExcelFile;
          return View();
        }

        if (file != null && file.ContentLength > 0)
        {
          fileName = Path.GetFileName(file.FileName);

          if (!fileName.Contains(".csv") && !fileName.Contains(".xls") && !fileName.Contains(".xlsx"))
          {
            TempData["error"] = error;
            return View();
          }

          TempData["Isfilesave"] = "false";

          // Need to implement to validate file data according to user role and accessbility
          //string errorMessage = ValidateCSVFile(file);

          // Format the date and time as a string (e.g., "yyyyMMdd_HHmmss")
          string formattedName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + Path.GetExtension(file.FileName);



          // Save data on local machine
          // Specify the file path where you want to save the uploaded file

          //var region = GetRegionName();
          var region = GetRegionName();
          var directoryName = region == "KASHMIR REGION" ? "K_Uploads" : "J_Uploads";
          //string serverMapPath = Server.MapPath($"~/DataFile/{directoryName}");
          string serverMapPath = Helper.Helper.SFTPMapPath(region, directoryName);



          //string serverMapPath = Server.MapPath("~/DataFile/Uploads");
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
            DataTable dataTable = upConvertExcelToDataTable(filePath);

            if (dataTable != null && dataTable.Columns.Contains("Select District"))
            {
              dataSet.Tables.Add(dataTable);

              List = dataTable.AsEnumerable()
                  .Select(x => new CsvFileViewModel
                  {
                    SelectDistrict = !x.IsNull("Select District") && !string.IsNullOrWhiteSpace(x["Select District"].ToString())
                                ? x["Select District"].ToString().ToUpper() : "",
            }).ToList();
            }
            else
            {
              Console.WriteLine(dataTable == null ? "DataTable is null." : "Column 'Select District' does not exist in the DataTable.");
            }
          }

          // bool districtIsValid = false;
          string[] regionToCheck = null;
          string regionErrorMessage = "";

          if (regiontext.Trim() == "JAMMU REGION")
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
          if (List.Any(item => !regionToCheck.Contains(item.SelectDistrict?.Trim().ToUpper())))
          {

            var defaultItem1 = new SelectListItem { Value = "ALL", Text = "ALL | ALL" };
            var emplCodeList1 = db.MasterEmpType
         .Where(x => x.Type_Code != null)
         .Select(x => new SelectListItem
         {
           Value = x.Description,
           Text = x.Type_Code + " | " + x.Description
         })
         .ToList();

            emplCodeList1.Insert(0, defaultItem1); // Insert the default item at the beginning
            ViewBag.EmplCode = emplCodeList1;
            ViewBag.InValid = "true";
            ViewBag.UploadClicked = "true";
            ViewBag.NewEntry = 1;
            ViewBag.Group = GetUsersAssignedLocations();
            TempData["error"] = regionErrorMessage;
            Session["excellist"] = null;
            if (System.IO.File.Exists(filePath))
            {
              System.IO.File.Delete(filePath);
            }
            ViewBag.isSave = false;
            return View();
          }

          // ***** Code By Himanshu Rajput *****  End

          string csvFilePath = filePath.Replace(".xlsx", ".csv").Replace(".xls", ".csv");

          string csvFileName = Path.GetFileName(csvFilePath);


          SaveExcelAsCsv(filePath, csvFilePath);

          //ViewBag.formattedName = formattedName;
          ViewBag.formattedName = csvFileName;

          byte[] bytes = System.IO.File.ReadAllBytes(csvFilePath);
          string failedMessage = UploadDataFile(bytes, csvFileName);


          //var dt = ConvertCsvToDataTable(filePath);
          ViewBag.InValid = "true";
          ViewBag.UploadClicked = "true";
          ViewBag.NewEntry = 1;
          ViewBag.Group = GetUsersAssignedLocations();
        }
      }
      catch (Exception ex)
      {
        TempData["error"] = "Check Uploaded Sheet And Try Again..." + ex.Message;
        //FileInfo fi = new FileInfo(path);
      }

      return View();
    }

    public void SaveExcelAsCsv(string excelFilePath, string csvFilePath)
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
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
    }


    private string UploadDataFile(byte[] fileContents, string fileName)
    {
      string failedMessage = string.Empty;
      try
      {
        Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
        // Get the file name

        // var region = GetRegionName();
        var region = GetRegionName();
        var directoryName = region == "KASHMIR REGION" ? "K_Uploads" : "J_Uploads";
        //string ftpServerUrl = ftpSetting["ftpServerUrl"] + $"/DataFiles/{directoryName}/" + fileName;
        string ftpServerUrl = Helper.Helper.GetUploadDataFile(region, directoryName, fileName);
        //string ftpServerUrl = ftpSetting["ftpServerUrl"] + "/DataFiles/Uploads/" + fileName;

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
    private string UploadDataFile(HttpPostedFileBase file, string fileName)
    {
      string failedMessage = string.Empty;
      try
      {
        Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
        // Get the file name
        //var region = GetRegionName();
        var region = GetRegionName();
        var directoryName = region == "KASHMIR REGION" ? "K_Uploads" : "J_Uploads";
        string ftpServerUrl = ftpSetting["ftpServerUrl"] + $"/DataFiles/{directoryName}/" + fileName;


        //string ftpServerUrl = ftpSetting["ftpServerUrl"] + $"/DataFiles/Uploads/" + fileName;
        FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpServerUrl);
        ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
        ftpRequest.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);
        byte[] fileContents;
        // Create a MemoryStream to store the file content in memory
        using (var memoryStream = new MemoryStream())
        {
          byte[] buffer = new byte[81920]; // Buffer size of 80KB
          int bytesRead;
          while ((bytesRead = file.InputStream.Read(buffer, 0, buffer.Length)) > 0)
          {
            memoryStream.Write(buffer, 0, bytesRead);
          }

          fileContents = memoryStream.ToArray();
        }
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

    public ActionResult ContributionExcelDownload()
    {
      try
      {
        Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
        // Format the date and time as a string (e.g., "yyyyMMdd_HHmmss")
        string formattedName = "ContributionCSV_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
        // Define the path for sql create SVC

        //var region = GetRegionName();
        var region = GetRegionName();

        var directoryName = region == "KASHMIR REGION" ? "K_Downloads" : "J_Downloads";
        string filePath = ftpSetting["localFilePath"] + $"\\DataFiles\\{directoryName}\\" + formattedName;


        //string filePath = ftpSetting["localFilePath"] + "\\DataFiles\\Downloads\\" + formattedName;
        // Define the parameters if needed (e.g., for input parameters)
        var parameter1 = new SqlParameter("@filePathWithName", filePath);
        // Execute the stored procedure
        int result = db.Database.ExecuteSqlCommand("EXEC SaveDataIntoCSV @filePathWithName", parameter1);
        // Create an FTP client
        WebClient ftpClient = new WebClient();
        ftpClient.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);
        string path = ftpSetting["ftpServerUrl"] + ($"/DataFiles/{directoryName}/" + formattedName);
        // File path, attampt, dealy in attampt
        bool isExist = Helper.Helper.FileCheckInFTP(path, 3, 30);
        if (isExist)
        {
          // Download the file from the FTP server
          byte[] fileData = ftpClient.DownloadData(path);
          // Specify the file path where you want to save the uploaded file
          string serverMapPath = Server.MapPath($"~/DataFile/{directoryName}");
          if (!Directory.Exists(serverMapPath))
          {
            // Attempt to create the directory
            Directory.CreateDirectory(serverMapPath);
          }
          // Add file name with directory
          string csvFilePath = Path.Combine(serverMapPath, formattedName);
          // Save the file to the server
          System.IO.File.WriteAllBytes(csvFilePath, fileData);
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
          byte[] excelFileData = System.IO.File.ReadAllBytes(excelFilePath);
          // Specify the file's content type (MIME type)
          string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // Use the appropriate MIME type for your file
                                                                                                    // Return the file as a FileResult
          return File(excelFileData, contentType, Path.GetFileName(excelFilePath)); // "file.txt" is the suggested file name for download
        }
      }
      catch (Exception ex)
      {
        TempData["error"] = "Check Uploaded Sheet And Try Again..." + ex.Message;
        //FileInfo fi = new FileInfo(path);
      }
      return View();
    }
    //[HttpPost]
    //public ActionResult ContributionExcelUpload(HttpPostedFileBase file, List<ExcelFileViewModel> model)
    //{
    //  int Year;
    //  string error = "";
    //  DateTime cDate;
    //  string path = "";
    //  string fileName = "";
    //  string[] tempValue;
    //  string employerName;
    //  var list = new List<ExcelFileViewModel>();
    //  var tempListForDuplicate = new List<ExcelFileViewModel>();
    //  var docfiles = new List<string>();
    //  try
    //  {
    //    if (file != null && file.ContentLength > 0)
    //    {
    //      #region errorinFormat
    //      fileName = Path.GetFileName(file.FileName);
    //      tempValue = fileName.Split('_');
    //      if (tempValue.Length < 2)
    //      {
    //        TempData["error"] = "Incorrect File Format, It Should be Like EmployerName_Month_Year. Please correct and re-upload";
    //        return View(model);
    //      }
    //      if (Int32.TryParse(Regex.Replace(tempValue[2], "[^0-9]", ""), out Year))
    //      { }
    //      else
    //      {
    //        error = "Check Year";
    //      }
    //      string MonthName = tempValue[1];
    //      if (!db.MasterMonthName.Where(x => x.Name.ToLower() == MonthName.ToLower()).Any())
    //      {
    //        error = "Check Month Name";
    //      }
    //      #endregion
    //      if (error.Length < 1)
    //      {
    //        DateTime.TryParse("01/" + tempValue[1] + "/" + Year, out cDate);
    //        DateTime todayDate = DateTime.Now;
    //        if (todayDate.Date >= cDate.Date)
    //        {
    //          employerName = tempValue[0];
    //          var result = db.MasterEmployer.Where(x => x.EmployerName.ToLower() == employerName.ToLower()).FirstOrDefault();
    //          if (result != null)
    //          {
    //            #region CreateDirectoryAndSaveFile
    //            // Create a reference to a directory.
    //            DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/MonthlyReceivingFiles"));
    //            // Create the directory only if it does not already exist. 
    //            if (di.Exists == false) di.Create();
    //            // Create a subdirectory in the directory just created.
    //            DirectoryInfo dis = di.GetDirectories(result.EmployerName).Any() ? di.GetDirectories(result.EmployerName).FirstOrDefault() : di.CreateSubdirectory(result.EmployerName);
    //            dis = dis.GetDirectories(tempValue[1]).Any() ? dis.GetDirectories(tempValue[1]).FirstOrDefault() : dis.CreateSubdirectory(tempValue[1]);
    //            path = Path.Combine(dis.FullName, fileName);
    //            file.SaveAs(path);
    //            docfiles.Add(path);
    //            #endregion
    //            //var contributor = db.MasterContributor.Where(x => x.JobStatusID == 1 && x.IsActive == true).ToList();
    //            var contributor = db.MasterContributor.Where(x => x.IsActive == true).ToList();
    //            var contributorList = contributor.Where(x => x.EmployerID == result.Id).ToList();
    //            string pathToExcelFile = "" + docfiles[0];
    //            var excel = new ExcelQueryFactory(pathToExcelFile);
    //            var allRows = excel.WorksheetNoHeader();
    //            var excelRowList = allRows.ToList();
    //            if (excelRowList[0][0].Value.ToString().ToLower() == "personid" && excelRowList[0][2].Value.ToString().ToLower() == "source" && excelRowList[0][1].Value.ToString().ToLower() == "contributorname" && excelRowList[0][3].Value.ToString().ToLower() == "salaryamount" && excelRowList[0][4].Value.ToString().ToLower() == "contributorcontribution" && excelRowList[0][5].Value.ToString().ToLower() == "employercontribution")
    //            {
    //              int maxCheckCount = 0;
    //              for (int i = 1; i < excelRowList.Count(); i++)
    //              {
    //                #region breakLoopWhenPersonIdIsBlankForContinous20Rows
    //                string personID = excelRowList[i][0].Value.ToString() == "" ? "" : excelRowList[i][0].Value.ToString().Trim();
    //                if (String.IsNullOrWhiteSpace(personID))//to break when blank row found
    //                {
    //                  if (maxCheckCount == 20) break;
    //                  maxCheckCount++;
    //                  continue;
    //                }
    //                #endregion
    //                else
    //                {
    //                  var master = new ExcelFileViewModel();
    //                  master.SourceName = excelRowList[i][2].Value.ToString() == "" ? "Salary" : excelRowList[i][2].Value.ToString();
    //                  //string[] tempName;
    //                  var ContributorName = excelRowList[i][1].Value.ToString() == "" ? "" : excelRowList[i][1].Value.ToString().Trim();
    //                  //tempName = ContributorName.Split(null);
    //                  //string TempFirstName = tempName.FirstOrDefault().ToLower();
    //                  //string TempLastName = tempName.Count() == 3 && tempName[2] == "" ? tempName[1].ToLower() : tempName.LastOrDefault().ToLower();
    //                  var checkContributor = contributorList.Where(x => x.PersonID == personID).FirstOrDefault();
    //                  string CFirstName = checkContributor == null ? "" : checkContributor.FirstName == null ? "" : checkContributor.FirstName.ToLower();
    //                  string CLastName = checkContributor == null ? "" : checkContributor.LastName == null ? "" : checkContributor.LastName.ToLower();
    //                  var sysName = (CFirstName + " " + CLastName);
    //                  if (checkContributor != null && (ContributorName.ToLower().Contains(CFirstName.ToLower()) || ContributorName.ToLower().Contains(CLastName.ToLower())))//if contributor match
    //                  {
    //                    var jobDetails = db.MasterContributorJobDetails.Where(x => x.PersonID == checkContributor.PersonID).FirstOrDefault();
    //                    #region CheckJobDetails
    //                    if (jobDetails != null)
    //                    {
    //                      #region contributionAfterJoiningDate
    //                      if (jobDetails.JoiningDate <= cDate.Date)
    //                      {
    //                        master.IsActive = true;
    //                        master.ContributorName = checkContributor.FullName;
    //                        decimal SystemSalaryAmount = Math.Round(checkContributor.SalaryAmount / 12, 2);
    //                        master.SystemSalaryAmount = SystemSalaryAmount;
    //                        decimal SystemContributorContribution = Math.Round((SystemSalaryAmount * checkContributor.PFRate) / 100, 2);
    //                        master.SystemContributorContribution = SystemContributorContribution;
    //                        decimal SalaryAmount = Math.Round(decimal.Parse(excelRowList[i][3].Value.ToString(), NumberStyles.Currency), 2);
    //                        decimal epf = result.PFRate;
    //                        decimal SystemEmployerContribution = Math.Round(SystemSalaryAmount * epf / 100, 2);
    //                        master.SystemEmployerContribution = SystemEmployerContribution;
    //                        decimal ContributorContribution = Math.Round(decimal.Parse(excelRowList[i][4].Value.ToString(), NumberStyles.Currency), 2);
    //                        master.ContributorContribution = ContributorContribution;
    //                        decimal EmployerContribution = Math.Round(decimal.Parse(excelRowList[i][5].Value.ToString(), NumberStyles.Currency), 2);
    //                        master.EmployerContribution = EmployerContribution;
    //                        decimal tempemp = Math.Round(SalaryAmount * epf / 100, 2);
    //                        master.TempEmployerContribution = tempemp;
    //                        decimal tempcont = Math.Round((SalaryAmount * checkContributor.PFRate) / 100, 2);
    //                        master.TempContributorContribution = tempcont;
    //                        if (SystemSalaryAmount != SalaryAmount)
    //                        {
    //                          if ((ContributorContribution != tempcont) || (EmployerContribution != tempemp))
    //                          {
    //                            master.IsActive = false;
    //                            master.error += checkContributor.FirstName + " " + checkContributor.LastName + "(" + checkContributor.PersonID + ") Employee’s Contribution or Employer Contribution is Not as per defined PF Rate ";
    //                          }
    //                        }
    //                        else
    //                        {
    //                          if ((SystemEmployerContribution != EmployerContribution) || (SystemContributorContribution != ContributorContribution))
    //                          {
    //                            master.IsActive = false;
    //                            master.error += checkContributor.FirstName + " " + checkContributor.LastName + "(" + checkContributor.PersonID + ") Employee’s Contribution & Employer Contribution is Not as per defined System Contribution ";
    //                          }
    //                        }
    //                        if (checkContributor.JobStatusID != 1)
    //                        {
    //                          if (master.error != null && master.error != "")
    //                            master.error += " & Please ReCalCulate Pension/Refund ";
    //                          else
    //                            master.error = checkContributor.FirstName + " " + checkContributor.LastName + "(" + checkContributor.PersonID + ") Status:-" + checkContributor.Status.Name + " Please ReCalCulate Pension/Refund ";
    //                        }
    //                      }
    //                      else
    //                      {
    //                        master.error = checkContributor.FirstName + " " + checkContributor.LastName + "(" + checkContributor.PersonID + ") Contribution Cannot be before Joining Date ";
    //                        master.IsActive = false;
    //                        master.ContributorName = excelRowList[i][1].Value.ToString();
    //                        master.SystemSalaryAmount = Math.Round(checkContributor.SalaryAmount / 12, 2);
    //                        master.SystemContributorContribution = Math.Round(((checkContributor.SalaryAmount * checkContributor.PFRate) / 100), 2);
    //                        master.SystemEmployerContribution = Math.Round(((checkContributor.SalaryAmount * checkContributor.Employer.PFRate) / 100), 2);
    //                        //master.SystemSalaryAmount = 0;
    //                        //master.SystemContributorContribution = 0;
    //                        master.SalaryAmount = decimal.Parse(excelRowList[i][3].Value.ToString(), NumberStyles.Currency);
    //                        master.ContributorContribution = decimal.Parse(excelRowList[i][4].Value.ToString(), NumberStyles.Currency);
    //                        //master.SystemEmployerContribution = 0;
    //                        master.EmployerContribution = decimal.Parse(excelRowList[i][5].Value.ToString(), NumberStyles.Currency);
    //                      }
    //                      #endregion
    //                    }
    //                    else
    //                    {
    //                      master.error = checkContributor.FirstName + " " + checkContributor.MidName + " " + checkContributor.LastName + "(" + checkContributor.PersonID + ") Job Details Not Available";
    //                      master.IsActive = false;
    //                      master.ContributorName = excelRowList[i][1].Value.ToString();
    //                      master.SystemSalaryAmount = Math.Round(checkContributor.SalaryAmount / 12, 2);
    //                      master.SystemContributorContribution = Math.Round(((checkContributor.SalaryAmount * checkContributor.PFRate) / 100), 2);
    //                      master.SystemEmployerContribution = Math.Round(((checkContributor.SalaryAmount * checkContributor.Employer.PFRate) / 100), 2);
    //                      //master.SystemSalaryAmount = 0;
    //                      //master.SystemContributorContribution = 0;
    //                      master.SalaryAmount = decimal.Parse(excelRowList[i][3].Value.ToString(), NumberStyles.Currency);
    //                      master.ContributorContribution = decimal.Parse(excelRowList[i][4].Value.ToString(), NumberStyles.Currency);
    //                      //master.SystemEmployerContribution = 0;
    //                      master.EmployerContribution = decimal.Parse(excelRowList[i][5].Value.ToString(), NumberStyles.Currency);
    //                    }
    //                    #endregion
    //                  }
    //                  else
    //                  {
    //                    master.IsActive = false;
    //                    master.ContributorName = excelRowList[i][1].Value.ToString();
    //                    master.SystemSalaryAmount = 0;
    //                    master.SystemContributorContribution = 0;
    //                    master.SalaryAmount = decimal.Parse(excelRowList[i][3].Value.ToString(), NumberStyles.Currency);
    //                    master.ContributorContribution = decimal.Parse(excelRowList[i][4].Value.ToString(), NumberStyles.Currency);
    //                    master.SystemEmployerContribution = 0;
    //                    master.EmployerContribution = decimal.Parse(excelRowList[i][5].Value.ToString(), NumberStyles.Currency);
    //                    if (checkContributor != null)
    //                    {
    //                      if (!ContributorName.ToLower().Contains(CFirstName.ToLower()))
    //                      {
    //                        master.SystemSalaryAmount = Math.Round(checkContributor.SalaryAmount / 12, 2);
    //                        master.SystemContributorContribution = Math.Round(((checkContributor.SalaryAmount * checkContributor.PFRate) / 100), 2);
    //                        master.SystemEmployerContribution = Math.Round(((checkContributor.SalaryAmount * checkContributor.Employer.PFRate) / 100), 2);
    //                        master.error = "JKPS ID(" + personID + ") Contributor Name is incorrect as it should be " + checkContributor.FirstName + " " + checkContributor.LastName;
    //                      }
    //                    }
    //                    else
    //                    {
    //                      master.error = "JKPS ID(" + personID + ") Doesnot Exist";
    //                      if (contributor.Where(x => x.PersonID == personID).Any())
    //                      {
    //                        master.SystemSalaryAmount = Math.Round(checkContributor.SalaryAmount / 12, 2);
    //                        master.SystemContributorContribution = Math.Round(((checkContributor.SalaryAmount * checkContributor.PFRate) / 100), 2);
    //                        master.SystemEmployerContribution = Math.Round(((checkContributor.SalaryAmount * checkContributor.Employer.PFRate) / 100), 2);
    //                        master.error = "JKPS ID(" + personID + ") Doesnot Belong To this Employer";
    //                      }
    //                    }
    //                  }
    //                  master.EmployerID = result.Id;
    //                  master.EmployerName = result.EmployerName;
    //                  master.Month = tempValue[1];
    //                  master.Year = Year;
    //                  master.FilePath = path;
    //                  master.PersonId = excelRowList[i][0].Value.ToString();
    //                  master.SalaryAmount = decimal.Parse(excelRowList[i][3].Value.ToString(), NumberStyles.Currency);// Convert.ToDecimal(excelRowList[i][3].Value);
    //                  list.Add(master);
    //                }
    //              }
    //              if (list.Any())
    //              {
    //                #region DuplicateEntry
    //                var query = list.GroupBy(x => new { x.PersonId, x.SourceName }).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
    //                if (query != null && query.Count > 0)
    //                {
    //                  foreach (var item in list)
    //                  {
    //                    tempListForDuplicate.Add(item);
    //                  }
    //                  list.Clear();
    //                  foreach (var item in tempListForDuplicate)
    //                  {
    //                    if (query.Where(x => x.PersonId == item.PersonId && x.SourceName == item.SourceName).Any())
    //                    {
    //                      item.IsActive = false;
    //                      item.error = item.PersonId + "( " + item.ContributorName + " ) have Duplicate Entries";
    //                    }
    //                    list.Add(item);
    //                  }
    //                }
    //                #endregion
    //                if (list.Any(x => x.IsActive == false))
    //                {
    //                  ViewBag.InValid = "false";
    //                }
    //                ViewBag.Employer = list[0].EmployerName;
    //                ViewBag.EId = result == null ? "InValid Employer" : result.UniqueID;
    //                ViewBag.Month = list[0].Month;
    //                ViewBag.Year = list[0].Year;
    //                Session["excellist"] = list;
    //                FileInfo fi = new FileInfo(path);
    //                if (fi.Exists) fi.Delete();
    //                ViewBag.EmployerTotalContribution = "EC$ " + Convert.ToDecimal(list.Sum(x => x.ContributorContribution)).ToString("#,##0.00");
    //                ViewBag.ContributorTotalContribution = "EC$ " + Convert.ToDecimal(list.Sum(x => x.EmployerContribution)).ToString("#,##0.00");
    //                #region categorizethemIntoNew/Finalize/Unfinalize
    //                List<ExcelFileViewModel> Verify = new List<ExcelFileViewModel>();
    //                List<ExcelFileViewModel> tempVerify = new List<ExcelFileViewModel>();
    //                List<ExcelFileViewModel> temp2Verify = new List<ExcelFileViewModel>();
    //                var header = (from sheetHeader in db.ContributonSheetHeader.Where(x => x.IsActive == true) join sheetHeaderFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true) on sheetHeader.Id equals sheetHeaderFinalize.ContributonSheetHeaderId into rt from finalize in rt.DefaultIfEmpty() where sheetHeader.Month == MonthName && sheetHeader.Year == Year && sheetHeader.EmployerId == result.Id select new { headerId = sheetHeader == null ? 0 : sheetHeader.Id, headerFinalize = finalize == null ? 0 : finalize.Id }).FirstOrDefault();
    //                if (header == null)
    //                {
    //                  var myAnonInstance = new
    //                  {
    //                    headerId = 0,
    //                    headerFinalize = 0,
    //                  };
    //                  header = myAnonInstance;
    //                }
    //                if (header != null && header.headerId == 0)
    //                {
    //                  list = list.Where(x => x.status == null).Select(c => { c.status = "New Entry"; return c; }).ToList();
    //                }
    //                else if (header != null && header.headerFinalize == 0)
    //                {
    //                  Verify = db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == header.headerId && x.IsActive == true).Select(x => new ExcelFileViewModel
    //                  {
    //                    Contributor = x.Contributor,
    //                    Year = 0,
    //                    Source = x.Source,
    //                    Final = "0",
    //                    Id = x.Id,
    //                  }).ToList();
    //                  if (Verify.Count > 0)
    //                  {
    //                    List<ExcelFileViewModel> tempList = new List<ExcelFileViewModel>();
    //                    foreach (var item in list)
    //                    {
    //                      tempList = new List<ExcelFileViewModel>();
    //                      var TempData = Verify.Where(x => x.Source.Name.ToLower() == item.SourceName.ToLower() && x.Contributor.PersonID == item.PersonId).FirstOrDefault();
    //                      if (TempData != null)
    //                      {
    //                        item.status = "Unfinalized";
    //                        if (item.error != null && item.error != "")
    //                          item.error += "& Record Already Exist in Unfinalized Details";
    //                        else
    //                          item.error = item.ContributorName + " (" + item.PersonId + ") Record Already Exist in Unfinalized Details";
    //                      }
    //                      else
    //                      {
    //                        item.status = "New Entry";
    //                      }
    //                      tempList.Add(item);
    //                    }
    //                    list = tempList;
    //                  }
    //                  else
    //                  {
    //                    list = list.Where(x => x.status == null).Select(c => { c.status = "New Entry"; return c; }).ToList();
    //                  }
    //                }
    //                else
    //                {
    //                  Verify = db.ContributonSheetDetailsFinalise.Where(x => x.ContributonSheetHeaderFinaliseID == header.headerFinalize && x.IsActive == true).Select(x => new ExcelFileViewModel { Contributor = x.Contributor, Year = 0, Source = x.Source, Final = "1", Id = x.Id }).ToList();
    //                  tempVerify = db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == header.headerId && x.IsActive == true).Select(x => new ExcelFileViewModel
    //                  {
    //                    Contributor = x.Contributor,
    //                    Year = 0,
    //                    Source = x.Source,
    //                    Final = "0",
    //                    Id = x.Id,
    //                  }).ToList();
    //                  if (Verify.Count > 0)
    //                  {
    //                    List<ExcelFileViewModel> tempList = new List<ExcelFileViewModel>();
    //                    foreach (var item in list)
    //                    {
    //                      var TempData = Verify.Where(x => x.Source.Name.ToLower() == item.SourceName.ToLower() && x.Contributor.PersonID == item.PersonId).FirstOrDefault();
    //                      if (TempData != null)
    //                      {
    //                        item.status = "Finalized Record";
    //                        if (item.error != null && item.error != "")
    //                          item.error += " & Record Already Finalized";
    //                        else
    //                          item.error = item.ContributorName + " (" + item.PersonId + ") Record Already Finalized";
    //                      }
    //                      else
    //                      {
    //                        var TempDat = tempVerify.Where(x => x.Source.Name.ToLower() == item.SourceName.ToLower() && x.Contributor.PersonID == item.PersonId).FirstOrDefault();
    //                        if (TempDat != null)
    //                        {
    //                          item.status = "Unfinalized";
    //                          if (item.error != null && item.error != "")
    //                            item.error += " & Record Already Exist in Unfinalized Details";
    //                          else
    //                            item.error = item.ContributorName + " (" + item.PersonId + ") Record Already Exist in Unfinalized Details";
    //                        }
    //                        else
    //                        {
    //                          item.status = "New Entry";
    //                        }
    //                      }
    //                      tempList.Add(item);
    //                    }
    //                    list = tempList;
    //                  }
    //                  else
    //                  {
    //                    list = list.Where(x => x.status == null).Select(c => { c.status = "New Entries"; return c; }).ToList();
    //                  }
    //                }
    //                ViewBag.FinalizedRecord = list.Where(x => x.status == "Finalized Record").Count();
    //                ViewBag.NewEntry = list.Where(x => x.status == "New Entry").Count();
    //                ViewBag.Unfinalized = list.Where(x => x.status == "Unfinalized").Count();
    //                #endregion
    //                Session["excellist"] = list;
    //              }
    //            }
    //            else
    //            {
    //              ViewBag.Error = "fileformat";
    //            }
    //          }
    //          else
    //          {
    //            TempData["error"] = "This Employer doesn't exist";
    //            //FileInfo fi = new FileInfo(path);
    //            //if (fi.Exists) fi.Delete();
    //          }
    //        }
    //        else
    //        {
    //          TempData["error"] = "Sheet Only Uploaded to Current Month";
    //        }
    //      }
    //      else
    //      {
    //        TempData["error"] = error;
    //      }
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    TempData["error"] = "Check Uploaded Sheet And Try Again..." + ex.Message;
    //    FileInfo fi = new FileInfo(path);
    //    if (fi.Exists) fi.Delete();
    //  }
    //  return View();
    //}`
    public class ErrorSummary
    {
      public string PropertyName { get; set; }
      public int RowIndex { get; set; }
    }

    public async Task<ActionResult> ExcelAjaxHandler(JQueryDataTableParamModel param, string formattedName, string RegionNames, string Gender, string SchemeType, string ACCOUNT_STATUS,string allSummary)
    {
      try
      {
        bool isfile = false;
        int? oldRecordsCount = 0;
        List<ErrorSummary> ErorSummary = new List<ErrorSummary>();
        List<CsvFileViewModel> List = new List<CsvFileViewModel>();
        IEnumerable<CsvFileViewModel> filter = null; int id = AppUserManager.GetUserId();
        int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == id).RoleId;
        var data = db.SecRoleLocationModule.Where(x => x.RoleID == RoleId && x.UserId == id).Select(x => x.DistrictID).Distinct().ToList();
        var districtname = db.MasterDistrict.Where(x => data.Contains(x.Id)).Select(x => x.Name.ToLower().Trim()).Distinct().ToList();
        if (!string.IsNullOrEmpty(formattedName))
        {
          isfile = true;


          //var region = GetRegionName();
          var region = GetRegionName();
          var directoryName = region == "KASHMIR REGION" ? "K_Uploads" : "J_Uploads";
          //string serverMapPath = Server.MapPath($"~/DataFile/{directoryName}");
          string serverMapPath = Helper.Helper.SFTPMapPath(region, directoryName);

          //string serverMapPath = Server.MapPath("~/DataFile/Uploads");
          string filePath = Path.Combine(serverMapPath, formattedName);
          /// ****** Code By Himanshu Rajput *******

          DataTable ColumnCheckDt = new DataTable();
          ColumnCheckDt.Columns.Add("S.No");
          ColumnCheckDt.Columns.Add("Application Reference No");
          ColumnCheckDt.Columns.Add("Submission Location");
          ColumnCheckDt.Columns.Add("Submission Date");
          ColumnCheckDt.Columns.Add("Applied By");
          ColumnCheckDt.Columns.Add("Select Tehsil Social Welfare Office (TSWO)");
          ColumnCheckDt.Columns.Add("Select District");
          ColumnCheckDt.Columns.Add("Name of the Applicant");
          ColumnCheckDt.Columns.Add("Date of Birth");
          ColumnCheckDt.Columns.Add("Age (In Years)");
          ColumnCheckDt.Columns.Add("Mobile Number");
          ColumnCheckDt.Columns.Add("Do you have BPL card");
          ColumnCheckDt.Columns.Add("Father / Husband / Guardian Name");
          ColumnCheckDt.Columns.Add("E-Mail");
          ColumnCheckDt.Columns.Add("Category");
          ColumnCheckDt.Columns.Add("Gender");
          ColumnCheckDt.Columns.Add("Present Address");
          ColumnCheckDt.Columns.Add("Present District");
          ColumnCheckDt.Columns.Add("Present Village Name");
          ColumnCheckDt.Columns.Add("Pincode");
          ColumnCheckDt.Columns.Add("Present Halqa Panchayat / Municipality Name");
          ColumnCheckDt.Columns.Add("Present Tehsil");
          ColumnCheckDt.Columns.Add("Permanent Address");
          ColumnCheckDt.Columns.Add("Permanent District");
          ColumnCheckDt.Columns.Add("Permanent Tehsil");
          ColumnCheckDt.Columns.Add("Permanent Halqa Panchayat / Municipality Name");
          ColumnCheckDt.Columns.Add("Permanent Village Name");
          ColumnCheckDt.Columns.Add("Branch Name");
          ColumnCheckDt.Columns.Add("IFSC Code");
          ColumnCheckDt.Columns.Add("Account No. of the Applicant");
          ColumnCheckDt.Columns.Add("Bank Name");
          ColumnCheckDt.Columns.Add("Select Pension Type");
          ColumnCheckDt.Columns.Add("Percentage of Disability");
          ColumnCheckDt.Columns.Add("Civil Condition");
          ColumnCheckDt.Columns.Add("Are you previously taking Pension from JK-ISSS / GOI-NSAP");
          ColumnCheckDt.Columns.Add("Bank Name.");
          ColumnCheckDt.Columns.Add("Branch Name.");
          ColumnCheckDt.Columns.Add("IFSC Code.");
          ColumnCheckDt.Columns.Add("Account Number.");
          ColumnCheckDt.Columns.Add("Application Sanctioned under Scheme Name");
          ColumnCheckDt.Columns.Add("Current Task");
          ColumnCheckDt.Columns.Add("Current Status");
          ColumnCheckDt.Columns.Add("Last Task");
          ColumnCheckDt.Columns.Add("Version No");
          ColumnCheckDt.Columns.Add("Last_pay_date");
          ColumnCheckDt.Columns.Add("Application_approve_on");
          ColumnCheckDt.Columns.Add("ActionOnDate");
          //ColumnCheckDt.Columns.Add("AADHAR");


          DataSet dataSet = new DataSet();

          DataTable dataTable = ConvertCsvToDataTable(filePath);
          string[] ExcelcolumnNames = (from dc in dataTable.Columns.Cast<DataColumn>()
                                       select dc.ColumnName).ToArray();

          string[] mainDtColumn = (from dc in ColumnCheckDt.Columns.Cast<DataColumn>()
                                   select dc.ColumnName).ToArray();

          //string[] matchFound = (from dc in ColumnCheckDt.Columns.Cast<DataColumn>()
          //select dc.ColumnName).ToArray();


          string ErrorcolumnName = "Invalid column name ";
          string MissingcolumnName = "Add Column Name ";
          bool excelColumnCheck = true;

          if (ExcelcolumnNames.Count() > mainDtColumn.Count())
          {
            MissingcolumnName = "";
            for (int i = 0; i < mainDtColumn.Count(); i++)
            {
              for (int j = 0; j < ExcelcolumnNames.Count(); j++)
              {
                if (mainDtColumn[i].ToString() != ExcelcolumnNames[i].ToString())
                {
                  excelColumnCheck = false;
                  if (!ErrorcolumnName.Contains(ExcelcolumnNames[i].ToString()))
                  {
                    ErrorcolumnName += ExcelcolumnNames[i].ToString() + ",";
                  }
                }
              }
            }
          }
          if (mainDtColumn.Count() > ExcelcolumnNames.Count())
          {
            ErrorcolumnName = "";
            for (int i = 0; i < ExcelcolumnNames.Count(); i++)
            {

              for (int j = 0; j < mainDtColumn.Count(); j++)
              {
                if (ExcelcolumnNames[i].ToString() != mainDtColumn[i].ToString())
                {
                  excelColumnCheck = false;
                  if (!MissingcolumnName.Contains(mainDtColumn[i].ToString()))
                  {
                    MissingcolumnName += mainDtColumn[i].ToString() + ",";
                  }
                }
              }

            }
          }


          if (!excelColumnCheck)
          {
            return Json(new
            {
              errorMessage = ErrorcolumnName == "" ? MissingcolumnName : ErrorcolumnName
            }, JsonRequestBehavior.AllowGet);
          }
          var district = RegionNames.ToLower().Split(',').Select(x => x.Trim()).ToArray();


          dataSet.Tables.Add(dataTable);

          StringBuilder str = new StringBuilder();

          bool excelColumnValidateCheck = true;

          string ApplicationReferenceNo = "Invalid Application Reference No in";
          string SubmissionDate = "Invalid Submission Date in";
          string AppliedBy = "Invalid AppliedBy in";
          string SelectDistrict = "Invalid Select District in";
          string NameOfTheApplicant = "Invalid Name Of The Applicant in";
          string DateOfBirth = "Invalid Date Of Birth in";
          string Age_InYears = "Invalid Age In Years in";
          string MobileNumber = "Invalid Mobile Number in";


          int datatableCount = dataTable.Rows.Count;
          int index = 0;
          foreach (DataRow item in dataTable.Rows)
          {
            index++;
            if (item[1].ToString() == "" || item[1].ToString() == null)
            {
              excelColumnValidateCheck = false;
              ApplicationReferenceNo += item[1].ToString();
              ApplicationReferenceNo += " row " + index;

            }
            // Submission date is not required
            //if (item[3].ToString() == "" || item[3].ToString() == null)
            //{
            //    excelColumnValidateCheck = false;
            //    SubmissionDate += item[3].ToString();
            //    SubmissionDate += " row " + index;

            //}

            if (item[4].ToString() == "" || item[4].ToString() == null)
            {
              excelColumnValidateCheck = false;
              AppliedBy += item[4].ToString();
              AppliedBy += " row " + index;
            }

            if (item[6].ToString() == "" || item[6].ToString() == null)
            {
              excelColumnValidateCheck = false;
              SelectDistrict += item[6].ToString();
              SelectDistrict += " row " + index;
            }

            if (item[7].ToString() == "" || item[7].ToString() == null)
            {
              excelColumnValidateCheck = false;
              NameOfTheApplicant += item[7].ToString() + ",";
              NameOfTheApplicant += " row " + index;
            }

            if (item[8].ToString() == "" || item[8].ToString() == null)
            {
              excelColumnValidateCheck = false;
              DateOfBirth += item[8].ToString();
              DateOfBirth += " row " + index;
            }
            // Age is not required
            //if (item[9].ToString() == "" || item[9].ToString() == null)
            //{
            //    excelColumnValidateCheck = false;
            //    Age_InYears += item[9].ToString();
            //    Age_InYears += " row " + index;
            //}

            if (item[10].ToString() == "" || item[10].ToString() == null)
            {
              excelColumnValidateCheck = false;
              MobileNumber += item[10].ToString();
              MobileNumber += " row " + index;
            }
            if (datatableCount == index)
            {
              ApplicationReferenceNo += "##";
              SubmissionDate += "##";
              AppliedBy += "##";
              SelectDistrict += "##";
              NameOfTheApplicant += "##";
              DateOfBirth += "##";
              Age_InYears += "##";
              MobileNumber += "##";
            }
          }
          if (ApplicationReferenceNo != "Invalid Application Reference No in##")
          {
            str.Append(ApplicationReferenceNo);
          }
          if (SubmissionDate != "Invalid Submission Date in##")
          {
            str.Append(SubmissionDate);
          }
          if (AppliedBy != "Invalid AppliedBy in##")
          {
            str.Append(AppliedBy);
          }
          if (SelectDistrict != "Invalid Select District in##")
          {
            str.Append(SelectDistrict);
          }
          if (NameOfTheApplicant != "Invalid Name Of The Applicant in##")
          {
            str.Append(NameOfTheApplicant);

          }
          if (DateOfBirth != "Invalid Date Of Birth in##")
          {
            str.Append(DateOfBirth);
          }
          if (Age_InYears != "Invalid Age In Years in##")
          {
            str.Append(Age_InYears);
          }

          if (MobileNumber != "Invalid Mobile Number in##")
          {
            str.Append(MobileNumber);
          }

          if (!excelColumnValidateCheck)
          {
            return Json(new
            {
              errorList = str.ToString()
            }, JsonRequestBehavior.AllowGet);
          }

          /// ****** Code By Himanshu Rajput (End Code) *******
          List = dataTable.AsEnumerable()
  .Select(x => new CsvFileViewModel
  {
    SNo = x.IsNull("S.No") ? Convert.ToInt32("0") : Convert.ToInt32(x["S.No"].ToString()),
    IsValidate = !district.Contains(x["Select District"].ToString().ToLower().Trim()) ? false : true,
    ApplicationReferenceNo = x.IsNull("Application Reference No") ? "" : x["Application Reference No"].ToString(),
    SubmissionLocation = x.IsNull("Submission Location") ? "" : x["Submission Location"].ToString(),
    SubmissionDate = x.IsNull("Submission Date") ? "" : x["Submission Date"].ToString(),
    AppliedBy = x.IsNull("Applied By") ? "" : x["Applied By"].ToString(),
    SelectTehsilSocialWelfareOffice_TSWO = x.IsNull("Select Tehsil Social Welfare Office (TSWO)") ? "" : x["Select Tehsil Social Welfare Office (TSWO)"].ToString(),
    SelectDistrict = x.IsNull("Select District") ? "" : x["Select District"].ToString(),
    NameOfTheApplicant = x.IsNull("Name of the Applicant") ? "" : x["Name of the Applicant"].ToString(),
    DateOfBirth = x.IsNull("Date of Birth") ? "" : x["Date of Birth"].ToString(),
    Age_InYears = x.IsNull("Age (In Years)") ? "" : x["Age (In Years)"].ToString(),
    MobileNumber = x.IsNull("Mobile Number") ? "" : x["Mobile Number"].ToString(),
    DoYouHaveBPLcard = x.IsNull("Do you have BPL card") ? "" : x["Do you have BPL card"].ToString(),
    FatherOrHusbandOrGuardianName = x.IsNull("Father / Husband / Guardian Name") ? "" : x["Father / Husband / Guardian Name"].ToString(),
    EMail = x.IsNull("E-Mail") ? "" : x["E-Mail"].ToString(),
    Category = x.IsNull("Category") ? "" : x["Category"].ToString(),
    Gender = x.IsNull("Gender") ? "" : x["Gender"].ToString(),
    PresentAddress = x.IsNull("Present Address") ? "" : x["Present Address"].ToString(),
    PresentDistrict = x.IsNull("Present District") ? "" : x["Present District"].ToString(),
    PresentVillageName = x.IsNull("Present Village Name") ? "" : x["Present Village Name"].ToString(),
    Pincode = x.IsNull("Pincode") ? "" : x["Pincode"].ToString(),
    PresentHalqaPanchayatOrMunicipalityName = x.IsNull("Present Halqa Panchayat / Municipality Name") ? "" : x["Present Halqa Panchayat / Municipality Name"].ToString(),
    PresentTehsil = x.IsNull("Present Tehsil") ? "" : x["Present Tehsil"].ToString(),
    PermanentAddress = x.IsNull("Permanent Address") ? "" : x["Permanent Address"].ToString(),
    PermanentDistrict = x.IsNull("Permanent District") ? "" : x["Permanent District"].ToString(),
    PermanentTehsil = x.IsNull("Permanent Tehsil") ? "" : x["Permanent Tehsil"].ToString(),
    PermanentHalqaPanchayatOrMunicipalityName = x.IsNull("Permanent Halqa Panchayat / Municipality Name") ? "" : x["Permanent Halqa Panchayat / Municipality Name"].ToString(),
    PermanentVillageName = x.IsNull("Permanent Village Name") ? "" : x["Permanent Village Name"].ToString(),
    BranchName = x.IsNull("Branch Name") ? "" : x["Branch Name"].ToString(),
    IFSCCode = x.IsNull("IFSC Code") ? "" : x["IFSC Code"].ToString(),
    AccountNoOfTheApplicant = x.IsNull("Account No. of the Applicant") ? "" : x["Account No. of the Applicant"].ToString(),
    BankName = x.IsNull("Bank Name") ? "" : x["Bank Name"].ToString(),
    SelectPensionType = x.IsNull("Select Pension Type") ? "" : x["Select Pension Type"].ToString(),
    PercentageofDisability = x.IsNull("Percentage of Disability") ? "" : x["Percentage of Disability"].ToString(),
    CivilCondition = x.IsNull("Civil Condition") ? "" : x["Civil Condition"].ToString(),
    AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP = x.IsNull("Are you previously taking Pension from JK-ISSS / GOI-NSAP") ? "" : x["Are you previously taking Pension from JK-ISSS / GOI-NSAP"].ToString(),
    BankName1 = x.IsNull("Bank Name.") ? "" : x["Bank Name."].ToString(),
    BranchName1 = x.IsNull("Branch Name.") ? "" : x["Branch Name."].ToString(),
    IFSCCode1 = x.IsNull("IFSC Code.") ? "" : x["IFSC Code."].ToString(),
    AccountNumber = x.IsNull("Account Number.") ? "" : x["Account Number."].ToString(),
    ApplicationSanctionedunderSchemeName = x.IsNull("Application Sanctioned under Scheme Name") ? "" : x["Application Sanctioned under Scheme Name"].ToString(),
    CurrentTask = x.IsNull("Current Task") ? "" : x["Current Task"].ToString(),
    CurrentStatus = x.IsNull("Current Status") ? "" : x["Current Status"].ToString(),
    LastTask = x.IsNull("Last Task") ? "" : x["Last Task"].ToString(),
    VersionNo = x.IsNull("Version No") ? "" : x["Version No"].ToString(),
    Last_pay_date = x.Table.Columns.Contains("Last_pay_date") && !x.IsNull("Last_pay_date") ? x["Last_pay_date"].ToString() : "",
    Application_approve_on = x.Table.Columns.Contains("Application_approve_on") && !x.IsNull("Application_approve_on") ? x["Application_approve_on"].ToString() : "",
    ActionOnDate = x.Table.Columns.Contains("ActionOnDate") && !x.IsNull("ActionOnDate") ? x["ActionOnDate"].ToString() : "",
    // AADHAR = x.IsNull("AADHAR") ? "" : x["AADHAR"].ToString(),
  }).ToList();

          int ind = 0;
          string[] formats = {
                              "MM/dd/yyyy HH:mm:ss",
                              "yyyy-MM-ddTHH:mm:ss",
                              "dd-MM-yyyy HH:mm:ss", 
                              // Add more formats as needed
                          };
          foreach (var dat in List)
          {
            Type dataType = dat.GetType();
            PropertyInfo[] properties = dataType.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
              string propertyName = prop.Name;
              string propertyType = prop.PropertyType.ToString();
              if (propertyType.ToLower().Trim().Contains("int"))
              {
                if (!string.IsNullOrEmpty(prop.GetValue(dat).ToString()) && !int.TryParse(prop.GetValue(dat).ToString(), out int PropertyValue))
                  ErorSummary.Add(new ErrorSummary()
                  {
                    PropertyName = propertyName,
                    RowIndex = ind
                  });
              }
              //else if (propertyName.ToLower().Trim().Contains("submissiondate"))
              //{
              //  if (!string.IsNullOrEmpty(prop.GetValue(dat).ToString()) && !DateTime.TryParseExact(prop.GetValue(dat).ToString(),formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime PropertyValue))
              //    ErorSummary.Add(new ErrorSummary()
              //    {
              //      PropertyName = propertyName,
              //      RowIndex = ind
              //    });
              //}
              //else if (propertyName.ToLower().Trim().Contains("dateofbirth"))
              //{
              //  if (!string.IsNullOrEmpty(prop.GetValue(dat).ToString()) && !DateTime.TryParse(prop.GetValue(dat).ToString(), out DateTime PropertyValue))
              //    ErorSummary.Add(new ErrorSummary()
              //    {
              //      PropertyName = propertyName,
              //      RowIndex = ind
              //    });
              //}
            }
            ind++;
          }
          var dta = db.MasterBeneficiariesDetails.ToList();
          oldRecordsCount = dta?.Count();
        }
        else
        {
          if (!string.IsNullOrEmpty(ACCOUNT_STATUS))
          {
            // Fail Records
            // working Query Showing Data Unapproved Beneficiary Total Records

            var Empcode = db.MasterEmpBankDetails.Where(x => (x.ACCOUNT_STATUS.ToLower() == "fail" && x.IsUpload == true) || (x.ACCOUNT_STATUS == null) || (x.ACCOUNT_STATUS.ToUpper() == "NOT ACTIVE")).Select(p => p.empl_code).ToList();

            // working Query but Account Status and IsUpload true only showing data

            //var Empcode = db.MasterEmpBankDetails.Where(x => x.ACCOUNT_STATUS == null && x.IsUpload == true).Select(p => p.empl_code).ToList();


            var intEmpCodes = Empcode.Select(code => Convert.ToInt32(code)).ToList();

            List = db.MasterBeneficiariesDetails.Where(d => RegionNames.ToLower().Contains(d.SelectDistrict.ToLower())).OrderByDescending(x => x.SNo)
            .Where(x => x.IsActive == true && intEmpCodes.Contains(x.Id))
            .Select(x => new CsvFileViewModel
            {
              SNo = x.SNo,
              IsValidate = true,
              ApplicationReferenceNo = x.ApplicationReferenceNo,
              SubmissionLocation = x.SubmissionLocation,
              SubmissionDate = x.SubmissionDate,
              AppliedBy = x.AppliedBy,
              SelectTehsilSocialWelfareOffice_TSWO = x.SelectTehsilSocialWelfareOffice_TSWO,
              SelectDistrict = x.SelectDistrict,
              NameOfTheApplicant = x.NameOfTheApplicant,
              DateOfBirth = x.DateOfBirth,
              Age_InYears = x.Age_InYears,
              MobileNumber = x.MobileNumber,
              DoYouHaveBPLcard = x.DoYouHaveBPLcard,
              FatherOrHusbandOrGuardianName = x.FatherOrHusbandOrGuardianName,
              EMail = x.EMail,
              Category = x.Category,
              Gender = x.Gender,
              PresentAddress = x.PresentAddress,
              PresentDistrict = x.PresentDistrict,
              PresentVillageName = x.PresentVillageName,
              Pincode = x.Pincode,
              PresentHalqaPanchayatOrMunicipalityName = x.PresentHalqaPanchayatOrMunicipalityName,
              PresentTehsil = x.PresentTehsil,
              PermanentAddress = x.PermanentAddress,
              PermanentDistrict = x.PermanentDistrict,
              PermanentTehsil = x.PermanentTehsil,
              PermanentHalqaPanchayatOrMunicipalityName = x.PermanentHalqaPanchayatOrMunicipalityName,
              PermanentVillageName = x.PermanentVillageName,
              BranchName = x.BranchName,
              IFSCCode = x.IFSCCode,
              AccountNoOfTheApplicant = x.AccountNoOfTheApplicant.ToString(),
              BankName = x.BankName,
              SelectPensionType = x.SelectPensionType,
              PercentageofDisability = x.PercentageofDisability,
              CivilCondition = x.CivilCondition,
              AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP = x.AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP,
              BankName1 = x.BankName1,
              BranchName1 = x.BranchName1,
              IFSCCode1 = x.IFSCCode1,
              AccountNumber = x.AccountNumber,
              ApplicationSanctionedunderSchemeName = x.ApplicationSanctionedunderSchemeName,
              CurrentTask = x.CurrentTask,
              CurrentStatus = x.CurrentStatus,
              LastTask = x.LastTask,
              VersionNo = x.VersionNo,
              Last_pay_date = x.Last_pay_date,
              Application_approve_on = x.Application_approve_on,
              ActionOnDate = x.ActionOnDate,
              // AADHAR = x.AADHAR.ToString(),

            }).ToList();


            if (SchemeType != "" && SchemeType != null && SchemeType != "ALL")
            {
              List = List.Where(p => p.SelectPensionType.ToLower() == SchemeType.ToLower()).ToList();
            }
            if (Gender != "" && Gender != null && Gender != "ALL")
            {
              List = List.Where(p => p.Gender.ToLower() == Gender.ToLower()).ToList();
            }
            ViewBag.RecordCount = List.Count();
          }
          else
          {
            if (allSummary=="true")
            {               
              List = db.MasterBeneficiariesDetails.Where(d => RegionNames.ToLower().Contains(d.SelectDistrict.ToLower())).OrderByDescending(x => x.SNo)
              .Where(x => x.IsActive == true)
              .Select(x => new CsvFileViewModel
              {
                SNo = x.SNo,
                IsValidate = true,
                ApplicationReferenceNo = x.ApplicationReferenceNo,
                SubmissionLocation = x.SubmissionLocation,
                SubmissionDate = x.SubmissionDate,
                AppliedBy = x.AppliedBy,
                SelectTehsilSocialWelfareOffice_TSWO = x.SelectTehsilSocialWelfareOffice_TSWO,
                SelectDistrict = x.SelectDistrict,
                NameOfTheApplicant = x.NameOfTheApplicant,
                DateOfBirth = x.DateOfBirth,
                Age_InYears = x.Age_InYears,
                MobileNumber = x.MobileNumber,
                DoYouHaveBPLcard = x.DoYouHaveBPLcard,
                FatherOrHusbandOrGuardianName = x.FatherOrHusbandOrGuardianName,
                EMail = x.EMail,
                Category = x.Category,
                Gender = x.Gender,
                PresentAddress = x.PresentAddress,
                PresentDistrict = x.PresentDistrict,
                PresentVillageName = x.PresentVillageName,
                Pincode = x.Pincode,
                PresentHalqaPanchayatOrMunicipalityName = x.PresentHalqaPanchayatOrMunicipalityName,
                PresentTehsil = x.PresentTehsil,
                PermanentAddress = x.PermanentAddress,
                PermanentDistrict = x.PermanentDistrict,
                PermanentTehsil = x.PermanentTehsil,
                PermanentHalqaPanchayatOrMunicipalityName = x.PermanentHalqaPanchayatOrMunicipalityName,
                PermanentVillageName = x.PermanentVillageName,
                BranchName = x.BranchName,
                IFSCCode = x.IFSCCode,
                AccountNoOfTheApplicant = x.AccountNoOfTheApplicant.ToString(),
                BankName = x.BankName,
                SelectPensionType = x.SelectPensionType,
                PercentageofDisability = x.PercentageofDisability,
                CivilCondition = x.CivilCondition,
                AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP = x.AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP,
                BankName1 = x.BankName1,
                BranchName1 = x.BranchName1,
                IFSCCode1 = x.IFSCCode1,
                AccountNumber = x.AccountNumber,
                ApplicationSanctionedunderSchemeName = x.ApplicationSanctionedunderSchemeName,
                CurrentTask = x.CurrentTask,
                CurrentStatus = x.CurrentStatus,
                LastTask = x.LastTask,
                VersionNo = x.VersionNo,
                Last_pay_date = x.Last_pay_date,
                Application_approve_on = x.Application_approve_on,
                ActionOnDate = x.ActionOnDate,
                // AADHAR = x.AADHAR.ToString(),

              }).ToList();
            }
            else
            {
              int masterid = db.MasterBeneficiaries.Where(p => p.IsActive == true).OrderByDescending(n => n.Id).Select(x => x.Id).FirstOrDefault();
              List = db.MasterBeneficiariesDetails.Where(d => RegionNames.ToLower().Contains(d.SelectDistrict.ToLower()) && d.MasterBeneficiariesId == masterid).OrderByDescending(x => x.SNo)
              .Where(x => x.IsActive == true)
              .Select(x => new CsvFileViewModel
              {
                SNo = x.SNo,
                IsValidate = true,
                ApplicationReferenceNo = x.ApplicationReferenceNo,
                SubmissionLocation = x.SubmissionLocation,
                SubmissionDate = x.SubmissionDate,
                AppliedBy = x.AppliedBy,
                SelectTehsilSocialWelfareOffice_TSWO = x.SelectTehsilSocialWelfareOffice_TSWO,
                SelectDistrict = x.SelectDistrict,
                NameOfTheApplicant = x.NameOfTheApplicant,
                DateOfBirth = x.DateOfBirth,
                Age_InYears = x.Age_InYears,
                MobileNumber = x.MobileNumber,
                DoYouHaveBPLcard = x.DoYouHaveBPLcard,
                FatherOrHusbandOrGuardianName = x.FatherOrHusbandOrGuardianName,
                EMail = x.EMail,
                Category = x.Category,
                Gender = x.Gender,
                PresentAddress = x.PresentAddress,
                PresentDistrict = x.PresentDistrict,
                PresentVillageName = x.PresentVillageName,
                Pincode = x.Pincode,
                PresentHalqaPanchayatOrMunicipalityName = x.PresentHalqaPanchayatOrMunicipalityName,
                PresentTehsil = x.PresentTehsil,
                PermanentAddress = x.PermanentAddress,
                PermanentDistrict = x.PermanentDistrict,
                PermanentTehsil = x.PermanentTehsil,
                PermanentHalqaPanchayatOrMunicipalityName = x.PermanentHalqaPanchayatOrMunicipalityName,
                PermanentVillageName = x.PermanentVillageName,
                BranchName = x.BranchName,
                IFSCCode = x.IFSCCode,
                AccountNoOfTheApplicant = x.AccountNoOfTheApplicant.ToString(),
                BankName = x.BankName,
                SelectPensionType = x.SelectPensionType,
                PercentageofDisability = x.PercentageofDisability,
                CivilCondition = x.CivilCondition,
                AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP = x.AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP,
                BankName1 = x.BankName1,
                BranchName1 = x.BranchName1,
                IFSCCode1 = x.IFSCCode1,
                AccountNumber = x.AccountNumber,
                ApplicationSanctionedunderSchemeName = x.ApplicationSanctionedunderSchemeName,
                CurrentTask = x.CurrentTask,
                CurrentStatus = x.CurrentStatus,
                LastTask = x.LastTask,
                VersionNo = x.VersionNo,
                Last_pay_date = x.Last_pay_date,
                Application_approve_on = x.Application_approve_on,
                ActionOnDate = x.ActionOnDate,
                // AADHAR = x.AADHAR.ToString(),

              }).ToList();
            }
            if (SchemeType != "" && SchemeType != null && SchemeType != "ALL")
            {
              List = List.Where(p => p.SelectPensionType
              .ToLower() == SchemeType.ToLower()).ToList();
            }
            if (Gender != "" && Gender != null && Gender != "ALL")
            {
              List = List.Where(p => p.Gender.ToLower() == Gender.ToLower()).ToList();
            }
            ViewBag.RecordCount = List.Count();
          }
        }



        if (!string.IsNullOrEmpty(param.sSearch))
        {
          filter = List.OrderByDescending(x => x.SNo).Where(c =>
                  c.SNo.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.IsValidate.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.ApplicationReferenceNo.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.SubmissionLocation.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.SubmissionDate.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.AppliedBy.ToString().Contains(param.sSearch.ToLower())
              || c.SelectTehsilSocialWelfareOffice_TSWO.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.SelectDistrict.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.NameOfTheApplicant.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.DateOfBirth.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.Age_InYears.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.MobileNumber.ToString().ToLower().ToLower().Contains(param.sSearch.ToLower())
              || c.FatherOrHusbandOrGuardianName.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.EMail.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.Category.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.Gender.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.PresentAddress.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.PresentDistrict.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.PresentVillageName.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.Pincode.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.PresentHalqaPanchayatOrMunicipalityName.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.PresentTehsil.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.PermanentAddress.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.PermanentDistrict.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.PermanentTehsil.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.BranchName.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.IFSCCode.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.AccountNoOfTheApplicant.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.BankName.ToString().ToLower().Contains(param.sSearch.ToLower())
              || c.SelectPensionType.ToString().ToLower().Contains(param.sSearch.ToLower())
              );
        }
        else
        {
          filter = List;
        }

        if (Gender != "" && Gender != null)
        {
          List = List.Where(p => p.Gender.ToLower() == Gender.ToLower()).ToList();
        }

        var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
        Func<CsvFileViewModel, string> orderingFunction = (c => sortColumnIndex == 0 ? c.SNo + "" :
                                sortColumnIndex == 1 ? c.IsValidate + "" :
                                sortColumnIndex == 2 ? c.ApplicationReferenceNo + "" :
                                sortColumnIndex == 3 ? c.SubmissionLocation + "" :
                                sortColumnIndex == 4 ? c.SubmissionDate + "" :
                                sortColumnIndex == 5 ? c.AppliedBy + "" :
                                sortColumnIndex == 6 ? c.SelectTehsilSocialWelfareOffice_TSWO + "" :
                                sortColumnIndex == 7 ? c.SelectDistrict + "" :
                                sortColumnIndex == 8 ? c.NameOfTheApplicant + "" :
                                sortColumnIndex == 9 ? c.DateOfBirth + "" :
                                sortColumnIndex == 10 ? c.Age_InYears + "" :
                                sortColumnIndex == 11 ? c.MobileNumber + "" :
                                sortColumnIndex == 12 ? c.DoYouHaveBPLcard + "" :
                                sortColumnIndex == 13 ? c.FatherOrHusbandOrGuardianName + "" :
                                sortColumnIndex == 14 ? c.EMail + "" :
                                sortColumnIndex == 15 ? c.Category + "" :
                                sortColumnIndex == 16 ? c.Gender + "" :
                                sortColumnIndex == 17 ? c.PresentAddress + "" :
                                sortColumnIndex == 18 ? c.PresentDistrict + "" :
                                sortColumnIndex == 19 ? c.PresentVillageName + "" :
                                sortColumnIndex == 20 ? c.Pincode + "" :
                                sortColumnIndex == 21 ? c.PresentHalqaPanchayatOrMunicipalityName + "" :
                                sortColumnIndex == 22 ? c.PresentTehsil + "" :
                                sortColumnIndex == 23 ? c.PermanentAddress + "" :
                                sortColumnIndex == 24 ? c.PermanentDistrict + "" :
                                sortColumnIndex == 25 ? c.PermanentTehsil + "" :
                                sortColumnIndex == 26 ? c.PermanentHalqaPanchayatOrMunicipalityName + "" :
                                sortColumnIndex == 27 ? c.PermanentVillageName + "" :
                                sortColumnIndex == 28 ? c.BranchName + "" :
                                sortColumnIndex == 29 ? c.IFSCCode + "" :
                                sortColumnIndex == 30 ? c.AccountNoOfTheApplicant + "" :
                                sortColumnIndex == 31 ? c.BankName + "" :
                                sortColumnIndex == 32 ? c.SelectPensionType + "" :
                                sortColumnIndex == 33 ? c.PercentageofDisability + "" :
                                sortColumnIndex == 34 ? c.CivilCondition + "" :
                                sortColumnIndex == 35 ? c.AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP + "" :
                                sortColumnIndex == 36 ? c.BankName1 + "" :
                                sortColumnIndex == 37 ? c.BranchName1 + "" :
                                sortColumnIndex == 38 ? c.IFSCCode1 + "" :
                                sortColumnIndex == 39 ? c.AccountNumber + "" :
                                sortColumnIndex == 40 ? c.ApplicationSanctionedunderSchemeName + "" :
                                sortColumnIndex == 41 ? c.CurrentTask + "" :
                                sortColumnIndex == 42 ? c.CurrentStatus + "" :
                                sortColumnIndex == 43 ? c.LastTask + "" :
                                sortColumnIndex == 44 ? c.VersionNo + "" :
                                sortColumnIndex == 45 ? c.Last_pay_date + "" :
                                sortColumnIndex == 46 ? c.Application_approve_on + "" :
                                sortColumnIndex == 47 ? c.ActionOnDate + "" :
                                //sortColumnIndex == 46 ? c.AADHAR + "" :
                                "");

        //var sortDirection = Request["sSortDir_0"]; // asc or desc
        //if (sortColumnIndex == 0)
        //{
        //    sortDirection = "desc";
        //}
        //if (sortDirection == "asc")
        //    filter = filter.OrderBy(orderingFunction);
        //else
        //    filter = filter.OrderByDescending(orderingFunction);

        IEnumerable<CsvFileViewModel> displayed = null;
        displayed = filter.Skip(param.iDisplayStart).Take(param.iDisplayLength);

        IEnumerable<CsvFileViewModel> AgeList = CalculateAge(displayed).ToList();
        //        var result = from x in AgeList
        var result = from x in AgeList
                     orderby x.SNo descending
                     select new[] {x.SNo + "",
                                   x.IsValidate + "",
                                   x.ApplicationReferenceNo,
                                   x.SubmissionLocation,
                                   x.SubmissionDate,
                                   x.AppliedBy,
                                   x.SelectTehsilSocialWelfareOffice_TSWO,
                                   x.SelectDistrict,
                                   x.NameOfTheApplicant,
                                   x.DateOfBirth,
                                   x.Age_InYears,
                                   x.MobileNumber,
                                   x.DoYouHaveBPLcard,
                                   x.FatherOrHusbandOrGuardianName,
                                   x.EMail,
                                   x.Category,
                                   x.Gender,
                                   x.PresentAddress,
                                   x.PresentDistrict,
                                   x.PresentVillageName,
                                   x.Pincode,
                                   x.PresentHalqaPanchayatOrMunicipalityName,
                                   x.PresentTehsil,
                                   x.PermanentAddress,
                                   x.PermanentDistrict,
                                   x.PermanentTehsil,
                                   x.PermanentHalqaPanchayatOrMunicipalityName,
                                   x.PermanentVillageName,
                                   x.BranchName,
                                   x.IFSCCode,
                                   x.AccountNoOfTheApplicant + "",
                                   x.BankName,
                                   x.SelectPensionType,
                                   x.PercentageofDisability,
                                   x.CivilCondition,
                                   x.AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP,
                                   x.BankName1,
                                   x.BranchName1,
                                   x.IFSCCode1,
                                   x.AccountNumber,
                                   x.ApplicationSanctionedunderSchemeName,
                                   x.CurrentTask,
                                   x.CurrentStatus,
                                   x.LastTask,
                                   x.VersionNo,
                                   x.Last_pay_date,
                                   x.Application_approve_on,
                                   x.ActionOnDate
                                   //x.AADHAR
                     };
        return Json(new
        {
          sEcho = param.sEcho,
          iTotalRecords = List.Count(),
          iTotalDisplayRecords = List.Count(),
          aaData = result,
          isfile = isfile,
          oldRecordsCount = oldRecordsCount,
          eSummary = ErorSummary.GroupBy(x => x.PropertyName).ToList()
        }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }



    //Serching filter  Not Working 


    //public async Task<ActionResult> ExcelAjaxHandler(JQueryDataTableParamModel param, string formattedName, string RegionNames)
    //{
    //    try
    //    {
    //        bool isfile = false;
    //        int? oldRecordsCount = 0;
    //        List<ErrorSummary> ErorSummary = new List<ErrorSummary>();
    //        List<CsvFileViewModel> List = new List<CsvFileViewModel>();
    //        IQueryable<CsvFileViewModel> filtered = null;
    //        if (!string.IsNullOrEmpty(formattedName))
    //        {
    //            isfile = true;
    //            string serverMapPath = Server.MapPath("~/DataFile/Uploads");
    //            string filePath = Path.Combine(serverMapPath, formattedName);
    //            DataSet dataSet = new DataSet();
    //            DataTable dataTable = ConvertCsvToDataTable(filePath);
    //            var district = RegionNames.ToLower().Split(',').Select(x => x.Trim()).ToArray();
    //            dataSet.Tables.Add(dataTable);
    //            filtered = dataTable.AsEnumerable()
    //              .Select(x => new CsvFileViewModel
    //              {
    //                  SNo = x.IsNull("S.No") ? Convert.ToInt32("0") : Convert.ToInt32(x["S.No"].ToString()),
    //                  IsValidate = !district.Contains(x["Select District"].ToString().ToLower().Trim()) ? false : true,
    //                  ApplicationReferenceNo = x.IsNull("Application Reference No") ? "" : x["Application Reference No"].ToString(),
    //                  SubmissionLocation = x.IsNull("Submission Location") ? "" : x["Submission Location"].ToString(),
    //                  SubmissionDate = x.IsNull("Submission Date") ? "" : x["Submission Date"].ToString(),
    //                  AppliedBy = x.IsNull("Applied By") ? "" : x["Applied By"].ToString(),
    //                  SelectTehsilSocialWelfareOffice_TSWO = x.IsNull("Select Tehsil Social Welfare Office (TSWO)") ? "" : x["Select Tehsil Social Welfare Office (TSWO)"].ToString(),
    //                  SelectDistrict = x.IsNull("Select District") ? "" : x["Select District"].ToString(),
    //                  NameOfTheApplicant = x.IsNull("Name of the Applicant") ? "" : x["Name of the Applicant"].ToString(),
    //                  DateOfBirth = x.IsNull("Date of Birth") ? "" : x["Date of Birth"].ToString(),

    //                  //      Age_InYears = x.IsNull("Age (In Years)") ? "" : x["Age (In Years)"].ToString(),
    //                  MobileNumber = x.IsNull("Mobile Number") ? "" : x["Mobile Number"].ToString(),
    //                  DoYouHaveBPLcard = x.IsNull("Do you have BPL card") ? "" : x["Do you have BPL card"].ToString(),
    //                  FatherOrHusbandOrGuardianName = x.IsNull("Father / Husband / Guardian Name") ? "" : x["Father / Husband / Guardian Name"].ToString(),
    //                  EMail = x.IsNull("E-Mail") ? "" : x["E-Mail"].ToString(),
    //                  Category = x.IsNull("Category") ? "" : x["Category"].ToString(),
    //                  Gender = x.IsNull("Gender") ? "" : x["Gender"].ToString(),
    //                  PresentAddress = x.IsNull("Present Address") ? "" : x["Present Address"].ToString(),
    //                  PresentDistrict = x.IsNull("Present District") ? "" : x["Present District"].ToString(),
    //                  PresentVillageName = x.IsNull("Present Village Name") ? "" : x["Present Village Name"].ToString(),
    //                  Pincode = x.IsNull("Pincode") ? "" : x["Pincode"].ToString(),
    //                  PresentHalqaPanchayatOrMunicipalityName = x.IsNull("Present Halqa Panchayat / Municipality Name") ? "" : x["Present Halqa Panchayat / Municipality Name"].ToString(),
    //                  PresentTehsil = x.IsNull("Present Tehsil") ? "" : x["Present Tehsil"].ToString(),
    //                  PermanentAddress = x.IsNull("Permanent Address") ? "" : x["Permanent Address"].ToString(),
    //                  PermanentDistrict = x.IsNull("Permanent District") ? "" : x["Permanent District"].ToString(),
    //                  PermanentTehsil = x.IsNull("Permanent Tehsil") ? "" : x["Permanent Tehsil"].ToString(),
    //                  PermanentHalqaPanchayatOrMunicipalityName = x.IsNull("Permanent Halqa Panchayat / Municipality Name") ? "" : x["Permanent Halqa Panchayat / Municipality Name"].ToString(),
    //                  PermanentVillageName = x.IsNull("Permanent Village Name") ? "" : x["Permanent Village Name"].ToString(),
    //                  BranchName = x.IsNull("Branch Name") ? "" : x["Branch Name"].ToString(),
    //                  IFSCCode = x.IsNull("IFSC Code") ? "" : x["IFSC Code"].ToString(),
    //                  AccountNoOfTheApplicant = x.IsNull("Account No. of the Applicant") ? "" : x["Account No. of the Applicant"].ToString(),
    //                  BankName = x.IsNull("Bank Name") ? "" : x["Bank Name"].ToString(),
    //                  SelectPensionType = x.IsNull("Select Pension Type") ? "" : x["Select Pension Type"].ToString(),
    //                  PercentageofDisability = x.IsNull("Percentage of Disability") ? "" : x["Percentage of Disability"].ToString(),
    //                  CivilCondition = x.IsNull("Civil Condition") ? "" : x["Civil Condition"].ToString(),
    //                  AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP = x.IsNull("Are you previously taking Pension from JK-ISSS / GOI-NSAP") ? "" : x["Are you previously taking Pension from JK-ISSS / GOI-NSAP"].ToString(),
    //                  BankName1 = x.IsNull("Bank Name.") ? "" : x["Bank Name."].ToString(),
    //                  BranchName1 = x.IsNull("Branch Name.") ? "" : x["Branch Name."].ToString(),
    //                  IFSCCode1 = x.IsNull("IFSC Code.") ? "" : x["IFSC Code."].ToString(),
    //                  AccountNumber = x.IsNull("Account Number.") ? "" : x["Account Number."].ToString(),
    //                  ApplicationSanctionedunderSchemeName = x.IsNull("Application Sanctioned under Scheme Name") ? "" : x["Application Sanctioned under Scheme Name"].ToString(),
    //                  CurrentTask = x.IsNull("Current Task") ? "" : x["Current Task"].ToString(),
    //                  CurrentStatus = x.IsNull("Current Status") ? "" : x["Current Status"].ToString(),
    //                  LastTask = x.IsNull("Last Task") ? "" : x["Last Task"].ToString(),
    //                  VersionNo = x.IsNull("Version No") ? "" : x["Version No"].ToString(),
    //              }).AsQueryable();





    //            int ind = 0;
    //            string[] formats = {
    //                          "MM/dd/yyyy HH:mm:ss",
    //                          "yyyy-MM-ddTHH:mm:ss",
    //                          "dd-MM-yyyy HH:mm:ss", 
    //                          // Add more formats as needed
    //                      };


    //            foreach (var dat in filtered)
    //            {
    //                Type dataType = dat.GetType();
    //                PropertyInfo[] properties = dataType.GetProperties();
    //                foreach (PropertyInfo prop in properties)
    //                {
    //                    string propertyName = prop.Name;
    //                    string propertyType = prop.PropertyType.ToString();
    //                    if (propertyType.ToLower().Trim().Contains("int"))
    //                    {
    //                        if (!string.IsNullOrEmpty(prop.GetValue(dat).ToString()) && !int.TryParse(prop.GetValue(dat).ToString(), out int PropertyValue))
    //                            ErorSummary.Add(new ErrorSummary()
    //                            {
    //                                PropertyName = propertyName,
    //                                RowIndex = ind
    //                            });
    //                    }
    //                    //else if (propertyName.ToLower().Trim().Contains("submissiondate"))
    //                    //{
    //                    //  if (!string.IsNullOrEmpty(prop.GetValue(dat).ToString()) && !DateTime.TryParseExact(prop.GetValue(dat).ToString(),formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime PropertyValue))
    //                    //    ErorSummary.Add(new ErrorSummary()
    //                    //    {
    //                    //      PropertyName = propertyName,
    //                    //      RowIndex = ind
    //                    //    });
    //                    //}
    //                    //else if (propertyName.ToLower().Trim().Contains("dateofbirth"))
    //                    //{
    //                    //  if (!string.IsNullOrEmpty(prop.GetValue(dat).ToString()) && !DateTime.TryParse(prop.GetValue(dat).ToString(), out DateTime PropertyValue))
    //                    //    ErorSummary.Add(new ErrorSummary()
    //                    //    {
    //                    //      PropertyName = propertyName,
    //                    //      RowIndex = ind
    //                    //    });
    //                    //}
    //                }
    //                ind++;
    //            }
    //            var dta = db.MasterBeneficiariesDetails.ToList();
    //            oldRecordsCount = dta?.Count();
    //        }
    //        else
    //        {
    //            filtered = db.MasterBeneficiariesDetails
    //              .Where(x => x.IsActive == true)
    //              .Select(x => new CsvFileViewModel
    //              {
    //                  SNo = x.SNo,
    //                  IsValidate = true,
    //                  ApplicationReferenceNo = x.ApplicationReferenceNo,
    //                  SubmissionLocation = x.SubmissionLocation,
    //                  SubmissionDate = x.SubmissionDate,
    //                  AppliedBy = x.AppliedBy,
    //                  SelectTehsilSocialWelfareOffice_TSWO = x.SelectTehsilSocialWelfareOffice_TSWO,
    //                  SelectDistrict = x.SelectDistrict,
    //                  NameOfTheApplicant = x.NameOfTheApplicant,
    //                  DateOfBirth = x.DateOfBirth,
    //                  //Age_InYears = x.Age_InYears,
    //                  MobileNumber = x.MobileNumber,
    //                  DoYouHaveBPLcard = x.DoYouHaveBPLcard,
    //                  FatherOrHusbandOrGuardianName = x.FatherOrHusbandOrGuardianName,
    //                  EMail = x.EMail,
    //                  Category = x.Category,
    //                  Gender = x.Gender,
    //                  PresentAddress = x.PresentAddress,
    //                  PresentDistrict = x.PresentDistrict,
    //                  PresentVillageName = x.PresentVillageName,
    //                  Pincode = x.Pincode,
    //                  PresentHalqaPanchayatOrMunicipalityName = x.PresentHalqaPanchayatOrMunicipalityName,
    //                  PresentTehsil = x.PresentTehsil,
    //                  PermanentAddress = x.PermanentAddress,
    //                  PermanentDistrict = x.PermanentDistrict,
    //                  PermanentTehsil = x.PermanentTehsil,
    //                  PermanentHalqaPanchayatOrMunicipalityName = x.PermanentHalqaPanchayatOrMunicipalityName,
    //                  PermanentVillageName = x.PermanentVillageName,
    //                  BranchName = x.BranchName,
    //                  IFSCCode = x.IFSCCode,
    //                  AccountNoOfTheApplicant = x.AccountNoOfTheApplicant.ToString(),
    //                  BankName = x.BankName,
    //                  SelectPensionType = x.SelectPensionType,
    //                  PercentageofDisability = x.PercentageofDisability,
    //                  CivilCondition = x.CivilCondition,
    //                  AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP = x.AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP,
    //                  BankName1 = x.BankName1,
    //                  BranchName1 = x.BranchName1,
    //                  IFSCCode1 = x.IFSCCode1,
    //                  AccountNumber = x.AccountNumber,
    //                  ApplicationSanctionedunderSchemeName = x.ApplicationSanctionedunderSchemeName,
    //                  CurrentTask = x.CurrentTask,
    //                  CurrentStatus = x.CurrentStatus,
    //                  LastTask = x.LastTask,
    //                  VersionNo = x.VersionNo
    //              });
    //            ViewBag.RecordCount = filtered.Count();
    //        }




    //        Int32 totalRecords = filtered.Count();
    //        List = filtered.OrderByDescending(x => x.SNo).Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
    //        var tt = ErorSummary.GroupBy(x => x.PropertyName).ToList();





    //        List<CsvFileViewModel> AgeList = CalculateAge(List);


    //        var result = from x in AgeList
    //                     select new[] {
    //                        x.SNo + "",
    //                        x.IsValidate + "",
    //                        x.ApplicationReferenceNo,
    //                        x.SubmissionLocation,
    //                        x.SubmissionDate,
    //                        x.AppliedBy,
    //                        x.SelectTehsilSocialWelfareOffice_TSWO,
    //                        x.SelectDistrict,
    //                        x.NameOfTheApplicant,
    //                        x.DateOfBirth,
    //                        x.Age_InYears,
    //                        x.MobileNumber,
    //                        x.DoYouHaveBPLcard,
    //                        x.FatherOrHusbandOrGuardianName,
    //                        x.EMail,
    //                        x.Category,
    //                        x.Gender,
    //                        x.PresentAddress,
    //                        x.PresentDistrict,
    //                        x.PresentVillageName,
    //                        x.Pincode,
    //                        x.PresentHalqaPanchayatOrMunicipalityName,
    //                        x.PresentTehsil,
    //                        x.PermanentAddress,
    //                        x.PermanentDistrict,
    //                        x.PermanentTehsil,
    //                        x.PermanentHalqaPanchayatOrMunicipalityName,
    //                        x.PermanentVillageName,
    //                        x.BranchName,
    //                        x.IFSCCode,
    //                        x.AccountNoOfTheApplicant + "",
    //                        x.BankName,
    //                        x.SelectPensionType,
    //                        x.PercentageofDisability,
    //                        x.CivilCondition,
    //                        x.AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP,
    //                        x.BankName1,
    //                        x.BranchName1,
    //                        x.IFSCCode1,
    //                        x.AccountNumber,
    //                        x.ApplicationSanctionedunderSchemeName,
    //                        x.CurrentTask,
    //                        x.CurrentStatus,
    //                        x.LastTask,
    //                        x.VersionNo
    //                       };
    //        return Json(
    //                    new
    //                    {
    //                        sEcho = param.sEcho,
    //                        iTotalRecords = totalRecords,
    //                        iTotalDisplayRecords = totalRecords,
    //                        aaData = result,
    //                        isfile = isfile,
    //                        oldRecordsCount = oldRecordsCount,
    //                        eSummary = ErorSummary.GroupBy(x => x.PropertyName).ToList()
    //                    }, JsonRequestBehavior.AllowGet);
    //    }

    //    catch (Exception ex)
    //    {

    //        throw;
    //    }
    //}
    public IEnumerable<CsvFileViewModel> CalculateAge(IEnumerable<CsvFileViewModel> benefeciry)
    {
      try
      {
        foreach (var item in benefeciry)
        {
          DateTime dateOfBirth = DateTime.ParseExact(item.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);/* // Assuming DateOfBirth is a DateTime property*/
          DateTime currentDate = DateTime.Now;
          TimeSpan ageTimeSpan = currentDate - dateOfBirth;
          int ageInYears = (int)(ageTimeSpan.Days / 365.25); // Approximate number of days in a year
                                                             //ageInYears = ageInYears - 1;
          string Age_InYears = ageInYears.ToString();
          item.Age_InYears = Age_InYears;
        }
        return benefeciry;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    public JsonResult PensionAndRefundAjaxHandler(int? empId, string month, int? year)
    {
      var contributorList = db.MasterContributor.Where(x => x.IsActive == true).ToList();
      var tempId = (from sheetHeader in db.ContributonSheetHeader.Where(x => x.IsActive == true) join sheetHeaderFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true) on sheetHeader.Id equals sheetHeaderFinalize.ContributonSheetHeaderId into rt from finalize in rt.DefaultIfEmpty() where sheetHeader.Month == month && sheetHeader.Year == year && sheetHeader.EmployerId == empId select new { Id = sheetHeader.Id, finalizedId = finalize.Id }).FirstOrDefault();
      var finalizedDetailsContribution = (from finalizedContribution in db.ContributonSheetDetailsFinalise.Where(x => x.ContributonSheetHeaderFinaliseID == tempId.finalizedId && x.IsActive == true) join contributor in db.MasterContributor.Where(x => x.IsActive == true) on finalizedContribution.ContributorID equals contributor.Id select finalizedContribution).ToList();
      List<PensionOrRefundViewModel> List = new List<PensionOrRefundViewModel>();
      int i = 0;
      foreach (var item in finalizedDetailsContribution)
      {
        var status = contributorList.Where(x => x.Id == item.ContributorID).Select(x => new { x.JobStatusID, x.PersonID, Name = x.FirstName + " " + x.MidName + " " + x.LastName, x.Id }).FirstOrDefault();
        PensionOrRefundViewModel log = new PensionOrRefundViewModel();
        if (!List.Where(x => x.contributorId + "" == status.PersonID).Any())
        {
          if (status.JobStatusID != (int)jobStatus.Pensioner && db.PensionApplications.Where(x => x.PersonID == status.Id.ToString() && x.IsActive == true).Any())
          {
            PensionApplications penDetails = db.PensionApplications.Where(x => x.PersonID == status.Id.ToString() && x.IsActive == true).FirstOrDefault();
            log.ApplicationType = "P";
            log.Id = i + "";
            log.contributorId = status.PersonID;
            log.Name = status.Name;
            log.OldAmount = penDetails.pensionPerAnnum;
            List.Add(log);

            //RefundPaidDetails refDetails = db.RefundPaidDetails.Where(x => x.PersonID == item.ContributorID.ToString()).FirstOrDefault();
            //log.ApplicationType = "R";
            //log.Id = i + "";
            //log.contributorId = status.PersonID;
            //log.Name = status.Name;
            //log.OldAmount = refDetails.RefundAmt;
            //List.Add(log);
          }
          else if (status.JobStatusID == (int)jobStatus.Pensioner)
          {
            DVOMasterEmployee objSearchCriteriaDVOMasterEmployeeModel = new DVOMasterEmployee();
            objSearchCriteriaDVOMasterEmployeeModel.PersonID = status.PersonID;
            var checkRecord = SearchEmployeeInformation(objSearchCriteriaDVOMasterEmployeeModel);
            if (checkRecord.Any())
            {
              log.Id = i + "";
              log.contributorId = status.PersonID;
              log.Name = status.Name;
              log.ApplicationType = "P";
              log.OldAmount = checkRecord.FirstOrDefault().AnnualSalary;
              List.Add(log);
            }
          }
          i++;
        }
      }
      return Json(List, JsonRequestBehavior.AllowGet);
    }
    //public ActionResult PensionAndRefundAjaxHandler(JQueryDataTableParamModel param)
    //{
    //  List<PensionOrRefundViewModel> List = new List<PensionOrRefundViewModel>();
    //  if (Session["PensionAndRefundList"] != null)
    //  {
    //    List = ((List<PensionOrRefundViewModel>)Session["PensionAndRefundList"]);
    //  }
    //  IEnumerable<PensionOrRefundViewModel> filtered;

    //  if (!string.IsNullOrEmpty(param.sSearch))
    //  {
    //    filtered = List
    //       .Where(c => c.Name.ToLower().Contains(param.sSearch.ToLower())
    //       || c.OldAmount.ToString().ToLower().Contains(param.sSearch.ToLower())
    //       || c.NewAmount.ToString().ToLower().Contains(param.sSearch.ToLower()));

    //  }
    //  else
    //  {
    //    filtered = List;
    //  }

    //  //Pagging
    //  var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

    //  //Select required columns
    //  var result = from c in displayed
    //               select new[] { 
    //                 c.Id+"_"+c.contributorId+"_"+c.ApplicationType+"_"+c.recalculated+"_"+c.OldAmount,
    //                     c.Name,
    //                     c.ApplicationType,
    //                     c.OldAmount+"", 
    //                 c.NewAmount+"", 
    //               };

    //  return Json(
    //                              new
    //                              {
    //                                sEcho = param.sEcho,
    //                                iTotalRecords = List.Count(),
    //                                iTotalDisplayRecords = filtered.Count(),
    //                                aaData = result
    //                              }, JsonRequestBehavior.AllowGet);
    //}


    //public JsonResult finalizeReCalculationAjax(string[] data)
    //{
    //  int result = 0;
    //  string[] tempValue;
    //  List<PensionOrRefundViewModel> List = ((List<PensionOrRefundViewModel>)Session["PensionAndRefundList"]);
    //  List<PensionOrRefundViewModel> NewList = new List<PensionOrRefundViewModel>();
    //  for (int i = 0; i < data.Length; i++)
    //  {
    //    tempValue = data[i].Split('_');
    //    string personId = tempValue[1];
    //    var empId = db.MasterContributor.Where(x => x.Id.ToString() == personId).Select(x => x.EmployerID).FirstOrDefault();
    //    foreach (var item in List)
    //    {
    //      var header = db.ContributonSheetHeader.Where(x => x.EmployerId == empId && x.IsActive == true && x.Month == item.Month && x.Year == item.Year).FirstOrDefault();
    //      var Unfinalizedcontribution = db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == header.Id && x.ContributorID.ToString() == personId).ToList();
    //      var FinalizedHeader = db.ContributonSheetHeaderFinalise.Where(x => x.ContributonSheetHeaderId == header.Id && x.IsActive == true).FirstOrDefault();
    //      if (FinalizedHeader != null)
    //      {
    //        foreach (var unfinalizedetails in Unfinalizedcontribution)
    //        {
    //          var finalizedDetails = db.ContributonSheetDetailsFinalise.Where(x => x.SourceID == unfinalizedetails.SourceID && x.ContributorID == unfinalizedetails.ContributorID && x.ContributonSheetHeaderFinaliseID == FinalizedHeader.Id).FirstOrDefault();
    //          if (finalizedDetails == null)
    //          {
    //            ContributonSheetDetailsFinalise modelDetailsfinalized = new ContributonSheetDetailsFinalise();
    //            modelDetailsfinalized.ContributonSheetHeaderFinaliseID = FinalizedHeader.Id;
    //            modelDetailsfinalized.ContributorContribution = unfinalizedetails.ContributorContribution;
    //            modelDetailsfinalized.EmployerContribution = unfinalizedetails.EmployerContribution;
    //            modelDetailsfinalized.ContributorID = unfinalizedetails.ContributorID;
    //            modelDetailsfinalized.EntryDate = Convert.ToDateTime(unfinalizedetails.EntryDate);
    //            modelDetailsfinalized.IsActive = true;
    //            modelDetailsfinalized.SalaryAmount = unfinalizedetails.SalaryAmount;
    //            modelDetailsfinalized.SourceID = unfinalizedetails.SourceID;
    //            db.Entry(modelDetailsfinalized).State = EntityState.Added;
    //            result = db.SaveChanges();
    //          }
    //        }
    //      }
    //      else
    //      {
    //        ContributonSheetHeaderFinalise modelfinalized = new ContributonSheetHeaderFinalise();
    //        modelfinalized.ContributonSheetHeaderId = header.Id;
    //        modelfinalized.EmployerId = Convert.ToInt32(empId);
    //        modelfinalized.IsActive = true;
    //        modelfinalized.Month = item.Month;
    //        modelfinalized.Year = item.Year;
    //        db.Entry(modelfinalized).State = EntityState.Added;
    //        result = db.SaveChanges();

    //        foreach (var detail in Unfinalizedcontribution)
    //        {
    //          ContributonSheetDetailsFinalise modelDetailsfinalized = new ContributonSheetDetailsFinalise();
    //          modelDetailsfinalized.ContributonSheetHeaderFinaliseID = modelfinalized.Id;
    //          modelDetailsfinalized.ContributorContribution = detail.ContributorContribution;
    //          modelDetailsfinalized.EmployerContribution = detail.EmployerContribution;
    //          modelDetailsfinalized.ContributorID = detail.ContributorID;
    //          modelDetailsfinalized.EntryDate = Convert.ToDateTime(detail.EntryDate);
    //          modelDetailsfinalized.IsActive = true;
    //          modelDetailsfinalized.SalaryAmount = detail.SalaryAmount;
    //          modelDetailsfinalized.SourceID = detail.SourceID;
    //          db.Entry(modelDetailsfinalized).State = EntityState.Added;
    //          result = db.SaveChanges();
    //        }

    //      }
    //    }
    //    var tempList = List.Where(x => x.ApplicationType == tempValue[2] && x.contributorId == personId).FirstOrDefault();
    //    NewList.Add(tempList);
    //  }
    //  Session["PensionAndRefundList"] = NewList;
    //  return Json(result, JsonRequestBehavior.AllowGet);
    //}
    public JsonResult saveCalculationAjax(string[] data)
    {
      int result = 0;
      string[] tempValue;
      for (int i = 0; i < data.Length; i++)
      {
        tempValue = data[i].Split('_');
        string personId = tempValue[0];
        decimal oldValue = Convert.ToDecimal(tempValue[2]);
        decimal NewAmount = Convert.ToDecimal(tempValue[3]);
        if (tempValue[1] == "Pension")
        {
          var contributor = db.MasterContributor.Where(x => x.PersonID == personId).FirstOrDefault();

          //to update in employee table
          DVOMasterEmployee objSearchCriteriaDVOMasterEmployeeModel = new DVOMasterEmployee();
          objSearchCriteriaDVOMasterEmployeeModel.PersonID = personId;
          DVOMasterEmployee objDVOMasterEmployee = SearchEmployeeInformation(objSearchCriteriaDVOMasterEmployeeModel).FirstOrDefault();
          objDVOMasterEmployee.AnnualSalary = NewAmount;
          result = UpdateEmployeeDetailsRepo.UpdateEmployeeInformation(objDVOMasterEmployee);

          if (result > 0)
          {
            int pensionId = db.PensionApplications.Where(x => x.PersonID == contributor.Id.ToString() && x.IsActive == true).Select(x => x.Id).FirstOrDefault();
            ApplicationUpdLog applog = new ApplicationUpdLog();
            applog.update_date = DateTime.Now;
            applog.update_by = AppUserManager.GetUserId();
            applog.App_Id = pensionId;
            applog.Form_Type = "P";
            applog.field_name = "AnnualSalary";
            applog.old_value = oldValue + "";
            applog.new_value = NewAmount.ToString();
            db.Entry(applog).State = EntityState.Added;
            db.SaveChanges();
          }
        }
        else if (tempValue[1] == "PensionApplication")
        {
          PensionCalculationViewModel pmodel = new PensionCalculationViewModel();
          var contributor = db.MasterContributor.Where(x => x.PersonID == personId).FirstOrDefault();
          PensionApplications penDetails = db.PensionApplications.Where(x => x.PersonID == contributor.Id.ToString() && x.IsActive == true).FirstOrDefault();
          if (penDetails != null)
          {
            DateTime date = Convert.ToDateTime(penDetails.RetirementOrResignationDate);
            if (contributor.Employer.EmployerTypeID == (int)PensionType.Public)
              pmodel = PensionAndRefundRepo.CalculatePublicPensionDetails(date, contributor.Id, "No");
            else
              pmodel = PensionAndRefundRepo.CalculatePolicePensionDetails(date, contributor.Id, "No", "No");

            penDetails.TotalFullpension = pmodel.FullPension;
            penDetails.FullpensionAmount1 = pmodel.FullPension1;
            penDetails.FullpensionAmount2 = pmodel.FullPension2;
            penDetails.GratuityReducedPension = pmodel.DiscountedGratuity;
            penDetails.LengthOfQualifyingServiceInMonthsFrom1Jan2014 = pmodel.LengthOfQualifyingServiceInMonthsFrom1Jan2014;
            penDetails.LengthOfQualifyingServiceInMonthsTo31Dec2003 = pmodel.LengthOfQualifyingServiceInMonthsTo31Dec2003;
            penDetails.MaxPension = pmodel.MaxPension;
            penDetails.pensionPerAnnum = (pmodel.MaxPension > pmodel.FullPension ? pmodel.FullPension : pmodel.MaxPension);
            penDetails.Gratuity = pmodel.Gratuity;
            penDetails.ReducedPension = pmodel.ReducedPension;
            penDetails.RetirementAnnualSalary1 = pmodel.SalaryAmount;
            penDetails.RetirementAnnualSalary2 = pmodel.SalaryAmount;
            db.Entry(penDetails).State = EntityState.Modified;
            result = db.SaveChanges();
            if (result > 0)
            {
              ApplicationUpdLog applog = new ApplicationUpdLog();
              applog.update_date = DateTime.Now;
              applog.update_by = AppUserManager.GetUserId();
              applog.App_Id = penDetails.Id;
              applog.Form_Type = "P";
              applog.field_name = "TotalFullpension";
              applog.old_value = oldValue + "";
              applog.new_value = NewAmount.ToString();
              db.Entry(applog).State = EntityState.Added;
              db.SaveChanges();
            }
          }
        }
        else
        {
          var contributor = db.MasterContributor.Where(x => x.Id.ToString() == personId).FirstOrDefault();
          var refDetails = db.RefundPaidDetails.Where(x => x.PersonID == contributor.Id.ToString()).FirstOrDefault();
          refDetails.RefundAmt = NewAmount;
          db.Entry(refDetails).State = EntityState.Modified;
          result = db.SaveChanges();

          if (result > 0)
          {
            ApplicationUpdLog applog = new ApplicationUpdLog();
            applog.update_date = DateTime.Now;
            applog.update_by = AppUserManager.GetUserId();
            applog.App_Id = refDetails.RefundApplicationID;
            applog.Form_Type = "R";
            applog.field_name = "RefundAmt";
            applog.old_value = oldValue + "";
            applog.new_value = NewAmount.ToString();
            db.Entry(applog).State = EntityState.Added;
            db.SaveChanges();
          }
        }

      }
      return Json(result, JsonRequestBehavior.AllowGet);
    }
    //public JsonResult cancelCalculationAjax(string[] data)
    //{
    //  int result = 0;
    //  string[] tempValue;
    //  List<PensionOrRefundViewModel> List = ((List<PensionOrRefundViewModel>)Session["PensionAndRefundList"]);
    //  List<PensionOrRefundViewModel> NewList = new List<PensionOrRefundViewModel>();
    //  for (int i = 0; i < data.Length; i++)
    //  {
    //    tempValue = data[i].Split('_');
    //    string personId = tempValue[1];
    //    foreach (var item in List)
    //    {
    //      if (item.ApplicationType == tempValue[2] && item.contributorId == personId)
    //      {
    //        item.NewAmount = null;
    //        item.recalculated = false;
    //        result = 1;
    //        NewList.Add(item);
    //      }
    //    }
    //  }
    //  foreach (var item in List)
    //  {

    //    if (!NewList.Where(x => x.ApplicationType == item.ApplicationType && x.contributorId == item.contributorId).Any())
    //    {
    //      NewList.Add(item);
    //    }

    //  }
    //  Session["PensionAndRefundList"] = NewList;
    //  return Json(result, JsonRequestBehavior.AllowGet);
    //}
    public JsonResult checkCalculationAjax(string[] data)
    {
      ArrayList newAmount = new ArrayList();
      string[] tempValue;
      for (int i = 0; i < data.Length; i++)
      {
        tempValue = data[i].Split('_');
        string personId = tempValue[0];
        if (tempValue[1] == "Pension")
        {
          PensionCalculationViewModel pmodel = new PensionCalculationViewModel();
          var contributor = db.MasterContributor.Where(x => x.PersonID == personId).FirstOrDefault();
          DateTime date = Convert.ToDateTime(contributor.RetirementOrResignationDate);
          if (contributor.Employer.EmployerTypeID == (int)PensionType.Public)
            pmodel = PensionAndRefundRepo.CalculatePublicPensionDetails(date, contributor.Id, "No");
          else
            pmodel = PensionAndRefundRepo.CalculatePolicePensionDetails(date, contributor.Id, "No", "No");
          newAmount.Add(Math.Round(pmodel.FullPension, 2, MidpointRounding.AwayFromZero));
        }
        else if (tempValue[1] == "PensionApplication")
        {
          PensionCalculationViewModel pmodel = new PensionCalculationViewModel();
          var contributor = db.MasterContributor.Where(x => x.PersonID == personId).FirstOrDefault();
          PensionApplications pensionDetails = db.PensionApplications.Where(x => x.PersonID == contributor.Id.ToString() && x.IsActive == true).FirstOrDefault();
          if (pensionDetails != null)
          {
            DateTime date = Convert.ToDateTime(pensionDetails.RetirementOrResignationDate);
            if (contributor.Employer.EmployerTypeID == (int)PensionType.Public)
              pmodel = PensionAndRefundRepo.CalculatePublicPensionDetails(date, contributor.Id, "No");
            else
              pmodel = PensionAndRefundRepo.CalculatePolicePensionDetails(date, contributor.Id, "No", "No");
            newAmount.Add(Math.Round(pmodel.FullPension, 2, MidpointRounding.AwayFromZero));
          }
        }
        else
        {

          var contributor = db.MasterContributor.Where(x => x.PersonID == personId).FirstOrDefault();
          DateTime enddate = Convert.ToDateTime(contributor.RetirementOrResignationDate);
          DateTime startDate = Convert.ToDateTime(contributor.FirstAppointmentDate);
          refundViewModel refmodel = PensionAndRefundRepo.GetRefundEstimated(contributor.Id, startDate, enddate, "No");
          newAmount.Add(Math.Round(Convert.ToDecimal(refmodel.TotalRefundAmountPerYear), 2, MidpointRounding.AwayFromZero));
        }

      }
      return Json(newAmount, JsonRequestBehavior.AllowGet);
    }

    private List<DVOMasterEmployee> SearchEmployeeInformation(DVOMasterEmployee objSearchCriteriaDVOMasterEmployeeModel)
    {
      DVOMasterEmployee objSearchCriteriaDVOMasterEmployee = new DVOMasterEmployee();
      List<DVOMasterEmployee> listSearchResultDVOMasterEmployee = new List<DVOMasterEmployee>();
      try
      {
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

    public void saveModifiedRecord(List<ExcelFileViewModel> updateRecords, int id)
    {
      if (updateRecords.Count > 0)
      {
        string tmpTable = "create table #tmpUpdateContribution (Id	int,ContributonSheetHeaderID int,ContributorID	int,SourceID int,SalaryAmount	decimal(18, 2),ContributorContribution	decimal(18, 2),EmployerContribution	decimal(18, 2),EntryDate	datetime,CreatedBy	int,CreatedOn	smalldatetime,ModifiedBy	int,ModifiedOn	smalldatetime,IsActive	bit,EmplPFRateId int,EmplPfRate decimal(18,2),EmplEffectiveStartDate smalldatetime,EmplEffectiveEndDate smalldatetime, EmplrPFRateId int,EmplrPfRate decimal(18,2),EmplrEffectiveStartDate smalldatetime,EmplrEffectiveEndDate smalldatetime,Month int , Year int)";
        int userId = AppUserManager.GetUserId();
        var filteredData = updateRecords.Select(c => new ContributonSheetDetails { ContributonSheetHeaderID = id, CreatedBy = userId, ModifiedOn = DateTime.Now, ModifiedBy = userId, CreatedOn = DateTime.Now, IsActive = true, ContributorID = c.ContributorID, SalaryAmount = (decimal)c.SalaryAmount, EmployerContribution = (decimal)c.EmployerContribution, ContributorContribution = (decimal)c.ContributorContribution, SourceID = c.SourceID, EntryDate = c.EntryDate, EmplPFRateId = c.EmplPFRateId, EmplPfRate = c.EmplPFRate, EmplEffectiveStartDate = c.EmplEffectiveStartDate, EmplEffectiveEndDate = c.EmplEffectiveEndDate, EmplrPFRateId = c.EmplrPFRateId, EmplrPfRate = c.EmplrPFRate, EmplrEffectiveStartDate = c.EmplrEffectiveStartDate, EmplrEffectiveEndDate = c.EmplrEffectiveEndDate, Month = c.MonthInDigit, Year = c.Year }).ToList();
        DataTable dt = App.Web.Repository.ListToDataset.ToDataTable(filteredData);
        //string conString = System.Web.Configuration.WebConfigurationManager.AppSettings["SQLConn"];
        string conString = ConnectionStringProvider.GetConnectionString();
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString);
        con.Open();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(tmpTable, con);
        cmd.ExecuteNonQuery();
        using (System.Data.SqlClient.SqlBulkCopy bulk = new System.Data.SqlClient.SqlBulkCopy(con))
        {
          bulk.DestinationTableName = "#tmpUpdateContribution";
          bulk.ColumnMappings.Add("ContributorID", "ContributorID");
          bulk.ColumnMappings.Add("ContributonSheetHeaderID", "ContributonSheetHeaderID");
          bulk.ColumnMappings.Add("ContributorContribution", "ContributorContribution");
          bulk.ColumnMappings.Add("EmployerContribution", "EmployerContribution");
          bulk.ColumnMappings.Add("SalaryAmount", "SalaryAmount");
          bulk.ColumnMappings.Add("EntryDate", "EntryDate");
          bulk.ColumnMappings.Add("IsActive", "IsActive");
          bulk.ColumnMappings.Add("SourceID", "SourceID");
          bulk.ColumnMappings.Add("CreatedBy", "CreatedBy");
          bulk.ColumnMappings.Add("CreatedOn", "CreatedOn");
          bulk.ColumnMappings.Add("ModifiedBy", "ModifiedBy");
          bulk.ColumnMappings.Add("ModifiedOn", "ModifiedOn");

          bulk.ColumnMappings.Add("Month", "Month");
          bulk.ColumnMappings.Add("Year", "Year");

          bulk.ColumnMappings.Add("EmplPFRateId", "EmplPFRateId");
          bulk.ColumnMappings.Add("EmplPfRate", "EmplPfRate");
          bulk.ColumnMappings.Add("EmplEffectiveStartDate", "EmplEffectiveStartDate");
          bulk.ColumnMappings.Add("EmplEffectiveEndDate", "EmplEffectiveEndDate");

          bulk.ColumnMappings.Add("EmplrPFRateId", "EmplrPFRateId");
          bulk.ColumnMappings.Add("EmplrPfRate", "EmplrPfRate");
          bulk.ColumnMappings.Add("EmplrEffectiveStartDate", "EmplrEffectiveStartDate");
          bulk.ColumnMappings.Add("EmplrEffectiveEndDate", "EmplrEffectiveEndDate");

          bulk.WriteToServer(dt);
        }
        string mergeSql = "merge into ContributonSheetDetails as Target " +
                          "using #tmpUpdateContribution as Source " +
                          "on " +
                          "Target.ContributorID=Source.ContributorID " +
                          "and Target.ContributonSheetHeaderID = Source.ContributonSheetHeaderID " +
                          "and Target.IsActive=Source.IsActive " +
                          "and Target.SourceID = Source.SourceID " +
                          "when matched then " +
                          "update set Target.SalaryAmount=Source.SalaryAmount ,Target.ContributorContribution = Source.ContributorContribution , Target.EmployerContribution = Source.EmployerContribution , Target.EmplPFRateId = Source.EmplPFRateId , Target.EmplPfRate = Source.EmplPfRate ,Target.EmplEffectiveStartDate = Source.EmplEffectiveStartDate ,Target.EmplEffectiveEndDate = Source.EmplEffectiveEndDate , Target.EmplrPFRateId = Source.EmplrPFRateId , Target.EmplrPfRate = Source.EmplrPfRate, Target.EmplrEffectiveStartDate = Source.EmplrEffectiveStartDate, Target.EmplrEffectiveEndDate = Source.EmplrEffectiveEndDate, Target.Month = Source.Month, Target.Year = Source.Year ;";// +
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          //"when not matched then " +
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          //"insert (Symbol,Price,Timestamp) values (Source.Symbol,Source.Price,Source.Timestamp);";

        cmd.CommandText = mergeSql;
        cmd.ExecuteNonQuery();
        cmd.CommandText = "drop table #tmpUpdateContribution";
        cmd.ExecuteNonQuery();
        con.Close();
        //foreach (var item in updateRecords)
        //{
        //    var Details = db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == id && x.ContributorID == item.ContributorID && x.SourceID == item.SourceID && x.IsActive == true).FirstOrDefault();
        //    Details.ContributorContribution = Convert.ToDecimal(item.ContributorContribution);
        //    Details.EmployerContribution = Convert.ToDecimal(item.EmployerContribution);
        //    Details.SalaryAmount = Convert.ToDecimal(item.SalaryAmount);
        //    Details.IsActive = true;
        //    db.Entry(Details).State = EntityState.Modified;
        //}
        //db.SaveChanges();
      }
    }
    public void addNewRecords(List<ExcelFileViewModel> newRecords, int Id)
    {
      if (newRecords.Count > 0)
      {
        int userId = AppUserManager.GetUserId();
        var filteredData = newRecords.Select(c => new ContributonSheetDetails { ContributonSheetHeaderID = Id, CreatedBy = userId, ModifiedOn = DateTime.Now, ModifiedBy = userId, CreatedOn = DateTime.Now, IsActive = true, ContributorID = c.ContributorID, SalaryAmount = (decimal)c.SalaryAmount, EmployerContribution = (decimal)c.EmployerContribution, ContributorContribution = (decimal)c.ContributorContribution, SourceID = c.SourceID, EntryDate = c.EntryDate, EmplPFRateId = c.EmplPFRateId, EmplPfRate = c.EmplPFRate, EmplEffectiveStartDate = c.EmplEffectiveStartDate, EmplEffectiveEndDate = c.EmplEffectiveEndDate, EmplrPFRateId = c.EmplrPFRateId, EmplrPfRate = c.EmplrPFRate, EmplrEffectiveStartDate = c.EmplrEffectiveStartDate, EmplrEffectiveEndDate = c.EmplrEffectiveEndDate, Month = c.MonthInDigit, Year = c.Year }).ToList();
        DataTable dt = App.Web.Repository.ListToDataset.ToDataTable(filteredData);
        //string conString = System.Web.Configuration.WebConfigurationManager.AppSettings["SQLConn"];
        string conString = ConnectionStringProvider.GetConnectionString();
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString);
        System.Data.SqlClient.SqlBulkCopy objbulk = new System.Data.SqlClient.SqlBulkCopy(con);
        objbulk.DestinationTableName = "ContributonSheetDetails";
        objbulk.ColumnMappings.Add("ContributorID", "ContributorID");
        objbulk.ColumnMappings.Add("ContributonSheetHeaderID", "ContributonSheetHeaderID");
        objbulk.ColumnMappings.Add("ContributorContribution", "ContributorContribution");
        objbulk.ColumnMappings.Add("EmployerContribution", "EmployerContribution");
        objbulk.ColumnMappings.Add("SalaryAmount", "SalaryAmount");
        objbulk.ColumnMappings.Add("EntryDate", "EntryDate");
        objbulk.ColumnMappings.Add("IsActive", "IsActive");
        objbulk.ColumnMappings.Add("SourceID", "SourceID");
        objbulk.ColumnMappings.Add("CreatedBy", "CreatedBy");
        objbulk.ColumnMappings.Add("CreatedOn", "CreatedOn");
        objbulk.ColumnMappings.Add("ModifiedBy", "ModifiedBy");
        objbulk.ColumnMappings.Add("ModifiedOn", "ModifiedOn");

        objbulk.ColumnMappings.Add("Month", "Month");
        objbulk.ColumnMappings.Add("Year", "Year");

        objbulk.ColumnMappings.Add("EmplPFRateId", "EmplPFRateId");
        objbulk.ColumnMappings.Add("EmplPfRate", "EmplPfRate");
        objbulk.ColumnMappings.Add("EmplEffectiveStartDate", "EmplEffectiveStartDate");
        objbulk.ColumnMappings.Add("EmplEffectiveEndDate", "EmplEffectiveEndDate");

        objbulk.ColumnMappings.Add("EmplrPFRateId", "EmplrPFRateId");
        objbulk.ColumnMappings.Add("EmplrPfRate", "EmplrPfRate");
        objbulk.ColumnMappings.Add("EmplrEffectiveStartDate", "EmplrEffectiveStartDate");
        objbulk.ColumnMappings.Add("EmplrEffectiveEndDate", "EmplrEffectiveEndDate");

        con.Open();
        objbulk.WriteToServer(dt);
        con.Close();
      }
    }
    [HttpPost]
    public ActionResult AddExcelSheetIntoDatabase()
    {
      StringBuilder sb = new StringBuilder();
      try
      {
        string fileName = Request.Form["formattedName"];

        if (fileName.Contains(".csv") || fileName.Contains(".xlsx"))
        {
          Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
          TempData["Isfilesave"] = "True";

          var region = GetRegionName();
          var directoryName = region == "KASHMIR REGION" ? "K_Uploads" : "J_Uploads";
          string filePath = Helper.Helper.AddExcelSheetIntoDatabasePath(region, directoryName, fileName);
          sb.AppendLine(filePath);
          var parameter1 = new SqlParameter("@filePathWithName", filePath);
          var parameter2 = new SqlParameter("@RoleId", 1);
          var parameter3 = new SqlParameter("@UserId", 1);

          int result = db.Database.ExecuteSqlCommand("EXEC UploadDataUsingCSV @filePathWithName , @RoleId , @UserId", parameter1, parameter2, parameter3);
          
          if(result>0)
          {
            TempData["success"] = "Successfully Uploaded";
          }

        }
        else
        {
          TempData["error"] = "No Record Found.";
        }
      }
      catch (Exception ex)
      {
        TempData["error"] = "There is some error, Please try again later. " + ex.Message;

        var defaultItem1 = new SelectListItem { Value = "ALL", Text = "ALL | ALL" };

        var emplCodeList1 = db.MasterEmpType
            .Where(x => x.Type_Code != null)
            .Select(x => new SelectListItem
            {
              Value = x.Description,
              Text = x.Type_Code + " | " + x.Description
            })
            .ToList();

        emplCodeList1.Insert(0, defaultItem1); // Insert the default item at the beginning

        ViewBag.EmplCode = emplCodeList1;
        ViewBag.Group = GetUsersAssignedLocations();
        int userid1 = AppUserManager.GetUserId();
        var roleList1 = db.SecRoleLocationModule.Where(x => x.UserId == userid1).Select(x => x.RoleID).FirstOrDefault();
        var model1 = (from c in db.SecModule.Where(x => x.ControllerName == "ContributorPersonalDetails" && x.ActionName == "ContributionExcelUpload")
                     join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList1 && x.IsActive == true) on c.Id equals p.ModuleID into ps
                     from p in ps.DefaultIfEmpty()
                     select new RoleModuleViewModel { RoleID = roleList1.ToString(), ModuleName = "Data Upload", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

        if (model1 != null)
        {
          ViewBag.AddPermission = model1.AddPermssion;
        }
        return View("ContributionExcelUpload");
        //BLLPYBatchProcessStybatchr.WriteTextToFile(sb.ToString());
      }
      var defaultItem = new SelectListItem { Value = "ALL", Text = "ALL | ALL" };

      var emplCodeList = db.MasterEmpType
          .Where(x => x.Type_Code != null)
          .Select(x => new SelectListItem
          {
            Value = x.Description,
            Text = x.Type_Code + " | " + x.Description
          })
          .ToList();

      emplCodeList.Insert(0, defaultItem); // Insert the default item at the beginning

      ViewBag.EmplCode = emplCodeList;
      ViewBag.Group = GetUsersAssignedLocations();
      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "ContributorPersonalDetails" && x.ActionName == "ContributionExcelUpload")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Data Upload", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.AddPermission = model.AddPermssion;
      }
      return View("ContributionExcelUpload");
    }
    #region excelsheet save

    //[HttpPost]
    //public ActionResult AddExcelSheetIntoDatabase()
    //{

    //    if (Session["excellist"] != null)
    //    {
    //        List<ExcelFileViewModel> model = ((List<ExcelFileViewModel>)Session["excellist"]).Where(x => x.status == "New Entry" || x.status == "Unfinalized").ToList();
    //        if (model.Count > 0)
    //        {
    //            int EmployerId = Convert.ToInt32(model.Any() ? model.FirstOrDefault().EmployerID : 0);
    //            //var contributor = db.MasterContributor.Where(x => x.IsActive == true && x.JobStatusID == 1 && x.EmployerID == EmployerId).ToList();

    //            var contributor = db.MasterContributor.Where(x => x.IsActive == true && x.EmployerID == EmployerId).ToList();
    //            string month = model.Any() ? model.FirstOrDefault().Month : string.Empty;
    //            //int year = model.Any() ? model.FirstOrDefault().Year : 0;
    //            int monthInDigit = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;
    //            int year = Convert.ToInt32(model.FirstOrDefault().Year);
    //            int monthEndDate = DateTime.DaysInMonth(year, monthInDigit);
    //            DateTime cDate = Convert.ToDateTime(monthInDigit + "/" + monthEndDate + "/" + year);
    //            if (year == DateTime.Now.Year && monthInDigit == DateTime.Now.Month)
    //            {
    //                cDate = DateTime.Now;
    //            }//int eId = (int)contributor.FirstOrDefault().EmployerID;
    //            //string PersonID = model.Any() ? model.FirstOrDefault().PersonID : string.Empty;
    //            //DateTime cDate = Convert.ToDateTime("01/" + month + "/" + year);
    //            DateTime todayDate = DateTime.Now;
    //            #region contributionOnlytoCurrentMonth
    //            if (todayDate.Date >= cDate.Date)
    //            {
    //                using (var transaction = db.Database.BeginTransaction())
    //                {
    //                    try
    //                    {
    //                        var result = db.ContributonSheetHeader.Where(x => x.Month == month && x.Year == year && x.EmployerId == EmployerId && x.IsActive == true).FirstOrDefault();
    //                        List<MasterSource> source = db.MasterSource.Where(x => x.IsActive == true).ToList();
    //                        if (result != null)
    //                        {
    //                            foreach (var item in model)
    //                            {
    //                                int sid = source.Where(x => x.Name.ToLower() == item.SourceName.ToLower() && x.IsActive == true).Select(x => x == null ? 0 : x.Id).FirstOrDefault();
    //                                if (sid == 0 && item.SourceName != "")
    //                                {
    //                                    MasterSource entity = new MasterSource();
    //                                    entity.Name = item.SourceName;
    //                                    entity.IsActive = true;
    //                                    db.Entry(entity).State = EntityState.Added;
    //                                    db.SaveChanges();
    //                                    sid = entity.Id;

    //                                }

    //                                int cID = contributor.Where(x => x.PersonID == item.PersonId).Select(x => x.Id).FirstOrDefault();
    //                                var Details = db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == result.Id && x.ContributorID == cID && x.SourceID == sid && x.IsActive == true).FirstOrDefault();
    //                                if (Details != null)
    //                                {
    //                                    Details.ContributorContribution = Convert.ToDecimal(item.ContributorContribution);
    //                                    Details.EmployerContribution = Convert.ToDecimal(item.EmployerContribution);
    //                                    Details.SalaryAmount = Convert.ToDecimal(item.SalaryAmount);
    //                                    //Details.EntryDate = DateTime.Now;
    //                                    if (sid != 0)
    //                                        Details.SourceID = sid;
    //                                    Details.IsActive = true;
    //                                    db.Entry(Details).State = EntityState.Modified;
    //                                    //db.SaveChanges();
    //                                }
    //                                else
    //                                {
    //                                    int id = contributor.Where(x => x.PersonID == item.PersonId).Select(x => x.Id).FirstOrDefault();
    //                                    ContributonSheetDetails sDetails = new ContributonSheetDetails();
    //                                    sDetails.ContributorID = id;
    //                                    sDetails.ContributonSheetHeaderID = result.Id;
    //                                    sDetails.ContributorContribution = Convert.ToDecimal(item.ContributorContribution);
    //                                    sDetails.EmployerContribution = Convert.ToDecimal(item.EmployerContribution);
    //                                    sDetails.SalaryAmount = Convert.ToDecimal(item.SalaryAmount);
    //                                    sDetails.EntryDate = cDate;
    //                                    if (sid != 0)
    //                                        sDetails.SourceID = sid;
    //                                    sDetails.IsActive = true;
    //                                    db.Entry(sDetails).State = EntityState.Added;
    //                                    //db.SaveChanges();
    //                                }

    //                            }
    //                            db.SaveChanges();
    //                        }
    //                        else
    //                        {
    //                            ContributonSheetHeader header = new ContributonSheetHeader();
    //                            header.EmployerId = EmployerId;
    //                            header.Month = model[0].Month;
    //                            header.Year = model[0].Year;
    //                            header.Succeeded = 1;
    //                            header.IsActive = true;
    //                            header.FilePath = model[0].FilePath;
    //                            db.Entry(header).State = EntityState.Added;
    //                            db.SaveChanges();

    //                            foreach (var item in model)
    //                            {
    //                                int id = contributor.Where(x => x.PersonID == item.PersonId).Select(x => x.Id).FirstOrDefault();
    //                                int sid = source.Where(x => x.Name.ToLower() == item.SourceName.ToLower() && x.IsActive == true).Select(x => x == null ? 0 : x.Id).FirstOrDefault();
    //                                if (sid == 0 && item.SourceName != "")
    //                                {
    //                                    MasterSource entity = new MasterSource();
    //                                    entity.Name = item.SourceName;
    //                                    entity.IsActive = true;
    //                                    db.Entry(entity).State = EntityState.Added;
    //                                    db.SaveChanges();
    //                                    sid = entity.Id;
    //                                }


    //                                //var Details = db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == header.Id && x.ContributorID == id && x.SourceID == sid && x.IsActive == true).FirstOrDefault();
    //                                //if (Details != null)
    //                                //{
    //                                //  Details.ContributorContribution = Convert.ToDecimal(item.ContributorContribution);
    //                                //  Details.EmployerContribution = Convert.ToDecimal(item.EmployerContribution);
    //                                //  Details.SalaryAmount = Convert.ToDecimal(item.SalaryAmount);
    //                                //  //Details.EntryDate = DateTime.Now;
    //                                //  if (sid != 0)
    //                                //    Details.SourceID = sid;
    //                                //  Details.IsActive = true;
    //                                //  db.Entry(Details).State = EntityState.Modified;
    //                                //  //db.SaveChanges();
    //                                //}
    //                                //else
    //                                //{

    //                                ContributonSheetDetails sDetails = new ContributonSheetDetails();
    //                                sDetails.ContributorID = id;
    //                                sDetails.ContributonSheetHeaderID = header.Id;
    //                                sDetails.ContributorContribution = Convert.ToDecimal(item.ContributorContribution);
    //                                sDetails.EmployerContribution = Convert.ToDecimal(item.EmployerContribution);
    //                                sDetails.SalaryAmount = Convert.ToDecimal(item.SalaryAmount);
    //                                sDetails.EntryDate = cDate;
    //                                sDetails.IsActive = true;
    //                                if (sid != 0)
    //                                    sDetails.SourceID = sid;
    //                                db.Entry(sDetails).State = EntityState.Added;
    //                                //db.SaveChanges();
    //                                //}
    //                            }
    //                            db.SaveChanges();
    //                        }
    //                        transaction.Commit();
    //                        TempData["success"] = "Contribution saved successfully";
    //                        return RedirectToAction("ContributionExcelUpload", "ContributorPersonalDetails");
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        transaction.Rollback();
    //                        TempData["error"] = "There is some error, Please try again later. " + ex.Message;
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                TempData["error"] = "Sheet Only Uploaded to Current Month";
    //            }
    //            #endregion


    //        }
    //        else
    //        {
    //            TempData["error"] = "No Record Found..Only New Entries & Existing Un-Finalized Records are uploaded";
    //        }
    //    }

    //    return View("ContributionExcelUpload");
    //}
    #endregion



    //[HttpPost]
    //public ActionResult AddExcelSheetIntoDatabase()
    //{

    //  if (Session["excellist"] != null)
    //  {
    //    List<ExcelFileViewModel> model = ((List<ExcelFileViewModel>)Session["excellist"]).Where(x => x.status == "New Entry" || x.status == "Unfinalized").ToList();
    //    if (model.Count > 0)
    //    {
    //      int EmployerId = Convert.ToInt32(model.Any() ? model.FirstOrDefault().EmployerID : 0);
    //      //var contributor = db.MasterContributor.Where(x => x.IsActive == true && x.JobStatusID == 1 && x.EmployerID == EmployerId).ToList();

    //      var contributor = db.MasterContributor.Where(x => x.IsActive == true && x.EmployerID == EmployerId).ToList();
    //      string month = model.Any() ? model.FirstOrDefault().Month : string.Empty;
    //      //int year = model.Any() ? model.FirstOrDefault().Year : 0;
    //      int monthInDigit = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;
    //      int year = Convert.ToInt32(model.FirstOrDefault().Year);
    //      int monthEndDate = DateTime.DaysInMonth(year, monthInDigit);
    //      DateTime cDate = Convert.ToDateTime(monthInDigit + "/" + monthEndDate + "/" + year);
    //      if (year == DateTime.Now.Year && monthInDigit == DateTime.Now.Month)
    //      {
    //        cDate = DateTime.Now;
    //      }//int eId = (int)contributor.FirstOrDefault().EmployerID;
    //      //string PersonID = model.Any() ? model.FirstOrDefault().PersonID : string.Empty;
    //      //DateTime cDate = Convert.ToDateTime("01/" + month + "/" + year);
    //      DateTime todayDate = DateTime.Now;
    //      #region contributionOnlytoCurrentMonth
    //      if (todayDate.Date >= cDate.Date)
    //      {
    //        var result = db.ContributonSheetHeader.Where(x => x.Month == month && x.Year == year && x.EmployerId == EmployerId).FirstOrDefault();
    //        List<MasterSource> source = db.MasterSource.Where(x => x.IsActive == true).ToList();
    //        if (result != null)
    //        {
    //          foreach (var item in model)
    //          {
    //            int sid = source.Where(x => x.Name.ToLower() == item.SourceName.ToLower() && x.IsActive == true).Select(x => x == null ? 0 : x.Id).FirstOrDefault();
    //            if (sid == 0 && item.SourceName != "")
    //            {
    //              MasterSource entity = new MasterSource();
    //              entity.Name = item.SourceName;
    //              entity.IsActive = true;
    //              db.Entry(entity).State = EntityState.Added;
    //              db.SaveChanges();
    //              sid = entity.Id;

    //            }

    //            int cID = contributor.Where(x => x.PersonID == item.PersonId).Select(x => x.Id).FirstOrDefault();
    //            var Details = db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == result.Id && x.ContributorID == cID && x.SourceID == sid && x.IsActive == true).FirstOrDefault();
    //            if (Details != null)
    //            {
    //              Details.ContributorContribution = Convert.ToDecimal(item.ContributorContribution);
    //              Details.EmployerContribution = Convert.ToDecimal(item.EmployerContribution);
    //              Details.SalaryAmount = Convert.ToDecimal(item.SalaryAmount);
    //              //Details.EntryDate = DateTime.Now;
    //              if (sid != 0)
    //                Details.SourceID = sid;
    //              Details.IsActive = true;
    //              db.Entry(Details).State = EntityState.Modified;
    //              //db.SaveChanges();
    //            }
    //            else
    //            {
    //              int id = contributor.Where(x => x.PersonID == item.PersonId).Select(x => x.Id).FirstOrDefault();
    //              ContributonSheetDetails sDetails = new ContributonSheetDetails();
    //              sDetails.ContributorID = id;
    //              sDetails.ContributonSheetHeaderID = result.Id;
    //              sDetails.ContributorContribution = Convert.ToDecimal(item.ContributorContribution);
    //              sDetails.EmployerContribution = Convert.ToDecimal(item.EmployerContribution);
    //              sDetails.SalaryAmount = Convert.ToDecimal(item.SalaryAmount);
    //              sDetails.EntryDate = cDate;
    //              if (sid != 0)
    //                sDetails.SourceID = sid;
    //              sDetails.IsActive = true;
    //              db.Entry(sDetails).State = EntityState.Added;
    //              //db.SaveChanges();
    //            }
    //            db.SaveChanges();
    //          }
    //        }
    //        else
    //        {
    //          ContributonSheetHeader header = new ContributonSheetHeader();
    //          header.EmployerId = EmployerId;
    //          header.Month = model[0].Month;
    //          header.Year = model[0].Year;
    //          header.Succeeded = 1;
    //          header.IsActive = true;
    //          header.FilePath = model[0].FilePath;
    //          db.Entry(header).State = EntityState.Added;
    //          db.SaveChanges();

    //          foreach (var item in model)
    //          {
    //            int id = contributor.Where(x => x.PersonID == item.PersonId).Select(x => x.Id).FirstOrDefault();
    //            int sid = source.Where(x => x.Name.ToLower() == item.SourceName.ToLower() && x.IsActive == true).Select(x => x == null ? 0 : x.Id).FirstOrDefault();
    //            if (sid == 0 && item.SourceName != "")
    //            {
    //              MasterSource entity = new MasterSource();
    //              entity.Name = item.SourceName;
    //              entity.IsActive = true;
    //              db.Entry(entity).State = EntityState.Added;
    //              db.SaveChanges();
    //              sid = entity.Id;
    //            }


    //            var Details = db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == header.Id && x.ContributorID == id && x.SourceID == sid && x.IsActive == true).FirstOrDefault();
    //            if (Details != null)
    //            {
    //              Details.ContributorContribution = Convert.ToDecimal(item.ContributorContribution);
    //              Details.EmployerContribution = Convert.ToDecimal(item.EmployerContribution);
    //              Details.SalaryAmount = Convert.ToDecimal(item.SalaryAmount);
    //              //Details.EntryDate = DateTime.Now;
    //              if (sid != 0)
    //                Details.SourceID = sid;
    //              Details.IsActive = true;
    //              db.Entry(Details).State = EntityState.Modified;
    //              //db.SaveChanges();
    //            }
    //            else
    //            {

    //              ContributonSheetDetails sDetails = new ContributonSheetDetails();
    //              sDetails.ContributorID = id;
    //              sDetails.ContributonSheetHeaderID = header.Id;
    //              sDetails.ContributorContribution = Convert.ToDecimal(item.ContributorContribution);
    //              sDetails.EmployerContribution = Convert.ToDecimal(item.EmployerContribution);
    //              sDetails.SalaryAmount = Convert.ToDecimal(item.SalaryAmount);
    //              sDetails.EntryDate = cDate;
    //              sDetails.IsActive = true;
    //              if (sid != 0)
    //                sDetails.SourceID = sid;
    //              db.Entry(sDetails).State = EntityState.Added;
    //              //db.SaveChanges();
    //            }
    //          }
    //          db.SaveChanges();
    //        }

    //        TempData["success"] = "Contribution saved successfully";
    //        return RedirectToAction("ContributionExcelUpload", "ContributorPersonalDetails");
    //      }
    //      else
    //      {
    //        TempData["error"] = "Sheet Only Uploaded to Current Month";
    //      }
    //      #endregion


    //    }
    //    else
    //    {
    //      TempData["error"] = "No Record Found..Only New Entries & Existing Un-Finalized Records are uploaded";
    //    }
    //  }

    //  return View("ContributionExcelUpload");
    //}


    //public JsonResult CheckOverwriteAjax()
    //{
    //  int tresult = 0;
    //  List<MasterContributor> cont = db.MasterContributor.Where(x => x.JobStatusID == 1).ToList();
    //  if (Session["excellist"] != null)
    //  {
    //    List<ExcelFileViewModel> list = ((List<ExcelFileViewModel>)Session["excellist"]);
    //    ExcelFileViewModel flist = list.FirstOrDefault();
    //    var result = db.ContributonSheetHeader.Where(x => x.Month == flist.Month && x.Year == flist.Year && x.EmployerId == flist.EmployerID && x.IsActive == true).FirstOrDefault();
    //    if (result != null && result.Finalized == 1)
    //    {
    //      List<MasterSource> source = db.MasterSource.Where(x => x.IsActive == true).ToList();
    //      var temp = db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == result.Id && x.IsActive == true);
    //      int monthInDigit = DateTime.ParseExact(flist.Month, "MMMM", CultureInfo.CurrentCulture).Month;
    //      int year = flist.Year;
    //      //DateTime cDate = new DateTime(monthInDigit, DateTime.Now.Day, year, 0, 0, 0);
    //      DateTime cDate = Convert.ToDateTime(monthInDigit + "/" + DateTime.Now.Day + "/" + year);
    //      foreach (var templist in list)
    //      {
    //        int ConId = cont.Where(x => x.PersonID == templist.PersonId).Select(x => x.Id).FirstOrDefault();
    //        if (!temp.Where(x => x.ContributorID == ConId && x.Source.Name.ToLower() == templist.SourceName.ToLower()).Any())
    //        {
    //          int sid = source.Where(x => x.Name.ToLower() == templist.SourceName.ToLower() && x.IsActive == true).Select(x => x == null ? 0 : x.Id).FirstOrDefault();
    //          if (sid == 0)
    //          {
    //            MasterSource entity = new MasterSource();
    //            entity.Name = templist.SourceName;
    //            entity.IsActive = true;
    //            db.Entry(entity).State = EntityState.Added;
    //            db.SaveChanges();
    //            sid = entity.Id;
    //          }
    //          ContributonSheetDetails detailsModel = new ContributonSheetDetails();
    //          detailsModel.ContributonSheetHeaderID = result.Id;
    //          detailsModel.ContributorContribution = Convert.ToDecimal(templist.ContributorContribution);
    //          detailsModel.EmployerContribution = Convert.ToDecimal(templist.EmployerContribution);
    //          detailsModel.ContributorID = ConId;
    //          detailsModel.EntryDate = cDate;
    //          detailsModel.IsActive = true;
    //          detailsModel.SourceID = sid;
    //          detailsModel.SalaryAmount = Convert.ToDecimal(templist.SalaryAmount);
    //          db.Entry(detailsModel).State = EntityState.Added;
    //          int cres = db.SaveChanges();
    //          tresult += cres;
    //        }

    //      }
    //      if (tresult > 0)
    //      {
    //        Session["excellist"] = null;
    //        return Json(tresult + " Record Uploaded Successfully", JsonRequestBehavior.AllowGet);
    //      }
    //      else
    //      {
    //        Session["excellist"] = null;
    //        return Json("Record Already Finalized For This Month...", JsonRequestBehavior.AllowGet);
    //      }
    //    }
    //    else if (result != null)
    //    {
    //      return Json(1, JsonRequestBehavior.AllowGet);
    //    }
    //    else
    //    {
    //      return Json(0, JsonRequestBehavior.AllowGet);
    //    }
    //  }
    //  else
    //  {
    //    return Json("Please Upload Sheet Again...", JsonRequestBehavior.AllowGet);
    //  }

    //}




    //save result that is uploaded by excel sheet in Contributor excel upload

    //public JsonResult AddDetails(string[] data)
    //{
    //  int duplicateRecord = 1;
    //  for (int i = 0; i < data.Length; i++)
    //  {
    //    var tempValue = data[i].Split('#');
    //    string employerUniqueId = tempValue[0];
    //    int employerId = db.MasterEmployer.Where(x => x.UniqueID == employerUniqueId).Select(x => x.Id).FirstOrDefault();
    //    string Month = tempValue[1];
    //    var Year = Convert.ToInt32(tempValue[2]);
    //    string personUniqueId = tempValue[3];
    //    int personId = db.MasterContributor.Where(x => x.PersonID == personUniqueId).Select(x => x.Id).FirstOrDefault();
    //    var result = (from a in db.ContributonSheetHeader
    //                  join b in db.ContributonSheetDetails
    //                    on a.Id equals b.ContributonSheetHeaderID
    //                  where (a.EmployerId == employerId && a.Month == Month && a.Year == Year && b.ContributorID == personId && a.IsActive == true)
    //                  select a).FirstOrDefault();
    //    if (result != null && result.Finalized == 1)
    //    {
    //      duplicateRecord = 2;
    //    }
    //    else if (result == null)
    //    {
    //      //DateTime? joinDate = db.MasterContributor.Where(x => x.PersonID == personUniqueId).Select(x => x.FirstAppointmentDate).FirstOrDefault();

    //      DateTime cDate = Convert.ToDateTime("01/" + tempValue[1] + "/" + tempValue[2]);

    //      DateTime todayDate = DateTime.Now;
    //      if (todayDate.Date >= cDate.Date)
    //      {
    //        var entity = new ContributonSheetHeader();
    //        entity.EmployerId = employerId;
    //        entity.Month = tempValue[1];
    //        entity.Year = Convert.ToInt32(tempValue[2]);
    //        entity.Succeeded = 1;
    //        entity.FilePath = Session["Path"].ToString();
    //        entity.IsActive = true;
    //        db.Entry(entity).State = EntityState.Added;
    //        db.SaveChanges();
    //        var model = new ContributonSheetDetails();
    //        model.ContributonSheetHeaderID = entity.Id;
    //        model.EmployerContribution = string.IsNullOrWhiteSpace(tempValue[10]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[10]);
    //        model.ContributorContribution = string.IsNullOrWhiteSpace(tempValue[8]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[8]);
    //        model.EntryDate = DateTime.Now;
    //        model.ContributorID = personId;
    //        model.SalaryAmount = string.IsNullOrWhiteSpace(tempValue[6]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[6]);
    //        model.IsActive = true;
    //        db.Entry(model).State = EntityState.Added;
    //        db.SaveChanges();
    //        duplicateRecord = 0;
    //      }
    //      else
    //      {
    //        return Json("Error", JsonRequestBehavior.AllowGet);
    //      }

    //    }
    //  }
    //  return Json(duplicateRecord, JsonRequestBehavior.AllowGet);
    //}
    //when duplicate record in contributor excel upload
    //public async Task<JsonResult> OverwriteAjax(string[] data)
    //{
    //  int InActiveRecord = 0;
    //  try
    //  {
    //    for (int i = 0; i < data.Length; i++)
    //    {
    //      var tempValue = data[i].Split('#');
    //      string employerUniqueId = tempValue[0];
    //      int employerId = db.MasterEmployer.Where(x => x.UniqueID == employerUniqueId).Select(x => x.Id).FirstOrDefault();
    //      string Month = tempValue[1];
    //      var Year = Convert.ToInt32(tempValue[2]);
    //      var result = await db.ContributonSheetHeader.Where(a => a.EmployerId == employerId && a.Month == Month && a.Year == Year && a.IsActive == true).ToListAsync();
    //      if (result != null)
    //      {
    //        foreach (var item in result)
    //        {
    //          var model = await db.ContributonSheetHeader.Where(x => x.Id == item.Id).FirstOrDefaultAsync();
    //          model.IsActive = false;
    //          db.Entry(model).State = EntityState.Modified;
    //          await db.SaveChangesAsync();

    //          var entity = await db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == item.Id).FirstOrDefaultAsync();
    //          entity.IsActive = false;
    //          db.Entry(entity).State = EntityState.Modified;
    //          await db.SaveChangesAsync();
    //          InActiveRecord = 1;
    //        }
    //      }
    //    }
    //  }
    //  catch
    //  { InActiveRecord = 0; }
    //  return Json(InActiveRecord, JsonRequestBehavior.AllowGet);
    //}
    //public JsonResult Overwrite()
    //{
    //  int a = 0;

    //  if (TempData["Repeat"] != null)
    //  {
    //    foreach (var s in TempData["Repeat"] as List<ExcelFileViewModel>)
    //    {
    //      var model = db.ContributonSheetHeader.Where(x => x.EmployerId == s.EmployerId && x.Month == s.Month && x.Year == s.Year && x.IsActive == true).FirstOrDefault();
    //      model.IsActive = false;
    //      db.Entry(model).State = EntityState.Modified;
    //      db.SaveChanges();

    //      var entity = db.ContributonSheetDetails.Where(x => x.ContributorID == model.Id && x.IsActive == true).FirstOrDefault();
    //      entity.IsActive = false;
    //      db.Entry(entity).State = EntityState.Modified;
    //      db.SaveChanges();

    //    }
    //    foreach (var s in TempData["Repeat"] as List<ExcelFileViewModel>)
    //    {
    //      if (s.IsActive)
    //      {
    //        var entity = new ContributonSheetHeader();
    //        s.MapTo(entity);
    //        entity.Succeeded = 1;
    //        db.Entry(entity).State = EntityState.Added;
    //        db.SaveChanges();

    //        var model = new ContributonSheetDetails();
    //        s.MapTo(model);
    //        model.ContributorID = entity.Id;
    //        model.EntryDate = DateTime.Now;
    //        db.Entry(model).State = EntityState.Added;
    //        db.SaveChanges();

    //        a = 1;
    //      }
    //    }
    //  }
    //  return Json(a, JsonRequestBehavior.AllowGet);
    //}

    //get method as well as month ,year ,emp id get from add employee details view after saving details



    //[HttpPost]
    //public async Task<ActionResult> FinalizedDetails(ExcelFileViewModel model)
    //{
    //  List<ExcelFileViewModel> sModel = new List<ExcelFileViewModel>();

    //  if (Request["EmployerId"] == "" || Request["Month"].ToString() == "" || Request["Year"] == "")
    //  {
    //    TempData["error"] = "Pls select all details to search";
    //  }
    //  else
    //  {
    //    int employeeId = Request["EmployerId"] == null ? 0 : Convert.ToInt32(Request["EmployerId"]);
    //    var month = Request["Month"].ToString();
    //    var year = Request["Year"].ToString();
    //    var result = await (from a in db.ContributonSheetDetails
    //                        join b in db.ContributonSheetHeader on a.ContributorID equals b.Id
    //                        where (b.Finalized == 0 && b.Month == month && b.Year == year && b.EmployerId == employeeId && a.IsActive == true)
    //                        select a).ToListAsync();

    //    foreach (var item in result)
    //    {
    //      ExcelFileViewModel tempModel = new ExcelFileViewModel();
    //      item.MapTo(tempModel);
    //      tempModel.Year = year;
    //      tempModel.Month = month;
    //      sModel.Add(tempModel);
    //    }
    //  }
    //  ViewBag.EmployerId = new SelectList(db.MasterEmployer, "Id", "EmployerName");
    //  var months = db.ContributonSheetHeader.Select(x => x.Month).Distinct();
    //  var years = db.ContributonSheetHeader.Select(x => x.Year).Distinct();
    //  ViewBag.Month = new SelectList(months);
    //  ViewBag.Year = new SelectList(years);

    //  return View(sModel);
    //}

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
    public decimal DecimalConvert(decimal value)
    {
      var d = (Math.Round(value, 2, MidpointRounding.AwayFromZero));
      return d;
    }
    public ActionResult EditContributorFinalizedDetails(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ExcelFileViewModel Verify = (from a in db.ContributonSheetDetailsFinalise
                                   join b in db.ContributonSheetHeaderFinalise on a.ContributonSheetHeaderFinaliseID equals b.Id
                                   where (a.IsActive == true && a.Id == id)
                                   select new ExcelFileViewModel
                                   {
                                     ContributorID = a.ContributorID,
                                     SalaryAmount = a.SalaryAmount,
                                     SystemSalaryAmount = (Math.Round((a.Contributor.SalaryAmount / 12), 2)),
                                     //SystemContributorContribution = Math.Round(((a.Contributor.SalaryAmount / 1200) * (a.Contributor.PFRate)), 2),
                                     //SystemEmployerContribution = Math.Round(((a.Contributor.SalaryAmount / 1200) * (a.Contributor.Employer.PFRate)), 2),
                                     SystemContributorContribution = Math.Round(((a.Contributor.SalaryAmount / 1200) * ((decimal)a.EmplPfRate)), 2),
                                     SystemEmployerContribution = Math.Round(((a.Contributor.SalaryAmount / 1200) * ((decimal)a.EmplrPfRate)), 2),
                                     ContributorContribution = a.ContributorContribution,
                                     EmployerContribution = a.EmployerContribution,
                                     Month = b.Month,
                                     Year = b.Year,
                                     Id = a.Id,
                                     SourceID = a.SourceID,
                                     EmployerID = b.EmployerId,
                                     EmployerName = a.Contributor.Employer.EmployerName,
                                     ContributorName = a.Contributor.FirstName + " " + a.Contributor.MidName + " " + a.Contributor.LastName

                                   }).FirstOrDefault();
      if (Verify == null)
      {
        return HttpNotFound();
      }
      ViewBag.Source = new SelectList(db.MasterSource.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", Verify.SourceID);
      return View(Verify);
    }
    [HttpPost]
    public ActionResult EditContributorFinalizedDetails(ExcelFileViewModel model)
    {
      if (model != null)
      {

        ContributonSheetDetailsFinalise details = db.ContributonSheetDetailsFinalise.Find(model.Id);
        details.ContributorContribution = model.ContributorContribution == null ? 0 : Convert.ToDecimal(model.ContributorContribution);
        details.EmployerContribution = model.EmployerContribution == null ? 0 : Convert.ToDecimal(model.EmployerContribution);
        details.SalaryAmount = model.SalaryAmount == null ? 0 : Convert.ToDecimal(model.SalaryAmount);
        details.SourceID = model.SourceID;
        db.Entry(details).State = EntityState.Modified;
        int result = db.SaveChanges();

        if (result > 0)
        {
          TempData["success"] = "Record Saved Successfully";
          return RedirectToAction("ModifyFinalizedDetails", new { Month = model.Month, Year = model.Year, EmpId = model.EmployerID });
        }
        else
        {
          TempData["error"] = "Please Try Again";
        }

      }
      ViewBag.Source = new SelectList(db.MasterSource.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.SourceID);
      return View(model);
    }

    //private int MonthToNumber(string month)
    //{
    //  int monthInDigit = DateTime.ParseExact(month, "MMM", CultureInfo.InvariantCulture).Month;
    //  return monthInDigit;
    //}


    [HttpGet]
    public ActionResult FinalizedDetails(string Month, int? Year, int? EmpId)
    {
      //ViewBag.EmployerId = new SelectList(db.MasterEmployer.OrderBy(x => x.EmployerName), "Id", "EmployerName");
      //var months = (from x in db.ContributonSheetHeader
      //              join y in db.MasterMonthName on x.Month equals y.Name
      //              where x.IsActive == true
      //              select new
      //              {
      //                month = y.Id,
      //                monthname = x.Month
      //              }).OrderBy(o => o.month).Distinct();
      ////var months = db.ContributonSheetHeader.OrderBy(x => x.Month).Select(x => x.Month).Distinct();
      //var years = db.ContributonSheetHeader.OrderByDescending(x => x.Year).Select(x => x.Year).Distinct();
      //ViewBag.Month = new SelectList(months, "monthname", "monthname");
      //ViewBag.Year = new SelectList(years);

      int userid = AppUserManager.GetUserId();
      var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in db.SecModule.Where(x => x.ControllerName == "ContributorPersonalDetails" && x.ActionName == "FinalizedDetails")
                   join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Upload Finalization", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.AddPermission = model.AddPermssion;
      }

      ViewBag.Month = new SelectList(db.MasterMonthName.Where(x => x.IsActive == true), "Name", "Name", Month);
      ViewBag.EmployerId = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", EmpId);
      int year = 2022;
      List<YearModel> yearModel = new List<YearModel>();
      for (int i = year; i <= DateTime.Now.Year; i++)
      {
        YearModel tempYear = new YearModel();
        tempYear.Id = i;
        tempYear.Name = i.ToString();
        yearModel.Add(tempYear);
      }
      ViewBag.Year = new SelectList(yearModel.OrderByDescending(x => x.Id), "Id", "Name", Year);
      return View();
    }
    [HttpGet]
    public ActionResult FinalizedContributionErrorsDetails(int? jobStatusId)
    {
      ViewBag.EmployerId = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName");
      var employee = new List<MasterContributor>();
      ViewBag.EmployeeId = new SelectList(employee, "Id", "FirstName");
      return View();
    }
    public ActionResult missingContributionSearchAjax(JQueryDataTableParamModel param, string employerId, string employeeId, DateTime fromDate, DateTime to)
    {
      string startDate = fromDate.ToString("MM/dd/yyyy");
      string endDate = to.ToString("MM/dd/yyyy");
      //List<ExcelFileViewModel> Verify = new List<ExcelFileViewModel>();
      List<ExcelFileViewModel> tempVerify = new List<ExcelFileViewModel>();
      using (AppDbContext dbContext = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
      {
        DataSet ds = new DataSet();
        System.Text.StringBuilder SQL = new System.Text.StringBuilder();
        SQL.Append("Select GF.Month as HMonth,GF.Year as Hyear,ME.Id as EmployerId,PersonId=MC.PersonId,GF.ContributorId as conId,ME.EmployerName,ContributorName=isnull(MC.FirstName,'')+' '+isnull(MC.MidName,'')+' '+isnull(MC.LastName,''),MDM.Name as Month,MDM.Year as Year,MDM.ID as ContributorId from (select tempMonth.Name, tempYear.Year, tempCont.ID from(select Name from MasterMonthName) tempMonth cross join(select distinct year from ContributonSheetHeaderFinalise) tempYear cross join(select Id from MasterContributor where isactive=1 and JobStatusID=1) tempCont) MDM left join (Select HF.Month, HF.Year, DF.ContributorId from ContributonSheetHeaderFinalise HF inner join ContributonSheetDetailsFinalise DF on HF.Id = DF.ContributonSheetHeaderFinaliseID where HF.isactive=1 and DF.isactive=1) GF on GF.Month = MDM.Name and GF.Year = MDM.Year and GF.ContributorId = MDM.ID inner join MasterContributor MC on MC.Id = MDM.Id inner join MasterEmployer ME on MC.employerid = ME.id inner join MasterMonthName MMN on MDM.Name = MMN.Name left join (select personid,contributionstartdate from MasterContributorJobDetails where isactive=1 and ContributionStartDate is not null) MJD on MC.PersonId = MJD.PersonId where ( convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1)) >= '" + startDate + "')  AND(convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1)) <= '" + endDate + "') and( isnull(MJD.ContributionStartDate,isnull(MC.FirstAppointmentDate, GetDate())) <= convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1))) and GF.Month is null and GF.Year is null");

        //SQL.Append("Select GF.Month as HMonth,GF.Year as Hyear,ME.Id as EmployerId,PersonId=MC.PersonId,GF.ContributorId as conId,ME.EmployerName,ContributorName=isnull(MC.FirstName,'')+' '+isnull(MC.MidName,'')+' '+isnull(MC.LastName,''),MDM.Name as Month,MDM.Year as Year,MDM.ID as ContributorId from (select tempMonth.Name, tempYear.Year, tempCont.ID from(select Name from MasterMonthName) tempMonth cross join(select distinct year from ContributonSheetHeaderFinalise) tempYear cross join(select Id from MasterContributor) tempCont) MDM left join (Select HF.Month, HF.Year, DF.ContributorId from ContributonSheetHeaderFinalise HF inner join ContributonSheetDetailsFinalise DF on HF.Id = DF.ContributonSheetHeaderFinaliseID) GF on GF.Month = MDM.Name and GF.Year = MDM.Year and GF.ContributorId = MDM.ID inner join MasterContributor MC on MC.Id = MDM.Id inner join MasterEmployer ME on MC.employerid = ME.id inner join MasterMonthName MMN on MDM.Name = MMN.Name where(convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1)) >= '" + startDate + "')  AND(convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1)) <= '" + endDate + "') and(isnull(MC.FirstAppointmentDate, GetDate()) <= convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1))) and GF.Month is null and GF.Year is null");
        //SQL.Append("Select GF.Month as HMonth,GF.Year as Hyear,ME.Id as EmployerId,PersonId=MC.PersonId,GF.ContributorId as conId,ME.EmployerName,ContributorName=isnull(MC.FirstName,'')+' '+isnull(MC.MidName,'')+' '+isnull(MC.LastName,''),MDM.Name as Month,MDM.Year as Year,MDM.ID as ContributorId from (select tempMonth.Name, tempYear.Year, tempCont.ID from(select Name from MasterMonthName) tempMonth cross join(select distinct year from ContributonSheetHeaderFinalise) tempYear cross join(select Id from MasterContributor) tempCont) MDM left join (Select HF.Month, HF.Year, DF.ContributorId from ContributonSheetHeaderFinalise HF inner join ContributonSheetDetailsFinalise DF on HF.Id = DF.ContributonSheetHeaderFinaliseID) GF on GF.Month = MDM.Name and GF.Year = MDM.Year and GF.ContributorId = MDM.ID inner join MasterContributor MC on MC.Id = MDM.Id inner join MasterEmployer ME on MC.employerid = ME.id inner join MasterMonthName MMN on MDM.Name = MMN.Name where(convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1)) >= '" + startDate + "')  AND(convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1)) <= '" + endDate + "') and(isnull(MC.FirstAppointmentDate, GetDate()) <= convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1))) and GF.Month is null and GF.Year is null");
        //SQL.Append("Select GF.Month as HMonth,GF.Year as Hyear,ME.Id as EmployerId,PersonId=MC.PersonId,GF.ContributorId as conId,ME.EmployerName,ContributorName=isnull(MC.FirstName,'')+' '+isnull(MC.MidName,'')+' '+isnull(MC.LastName,''),MDM.Name as Month,MDM.Year as Year,MDM.ID as ContributorId from (select tempMonth.Name, tempYear.Year, tempCont.ID from(select Name from MasterMonthName) tempMonth cross join(select distinct year from ContributonSheetHeaderFinalise) tempYear cross join(select Id from MasterContributor) tempCont) MDM left join (Select HF.Month, HF.Year, DF.ContributorId from ContributonSheetHeaderFinalise HF inner join ContributonSheetDetailsFinalise DF on HF.Id = DF.ContributonSheetHeaderFinaliseID) GF on GF.Month = MDM.Name and GF.Year = MDM.Year and GF.ContributorId = MDM.ID inner join MasterContributor MC on MC.Id = MDM.Id inner join MasterEmployer ME on MC.employerid = ME.id inner join MasterMonthName MMN on MDM.Name = MMN.Name where(convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1)) >= '" + startDate + "')  AND(convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1)) <= '" + endDate + "') and(isnull(MC.FirstAppointmentDate, GetDate()) <= convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1))) and GF.Month is null and GF.Year is null");
        //SQL.Append("Select GF.Month as HMonth,GF.Year as Hyear,ME.Id as EmployerId,PersonId=MC.PersonId,GF.ContributorId as conId,ME.EmployerName,ContributorName=isnull(MC.FirstName,'')+' '+isnull(MC.MidName,'')+' '+isnull(MC.LastName,''),MDM.Name as Month,MDM.Year as Year,MDM.ID as ContributorId from (select tempMonth.Name, tempYear.Year, tempCont.ID from(select Name from MasterMonthName) tempMonth cross join(select distinct year from ContributonSheetHeaderFinalise) tempYear cross join(select Id from MasterContributor) tempCont) MDM left join (Select HF.Month, HF.Year, DF.ContributorId from ContributonSheetHeaderFinalise HF inner join ContributonSheetDetailsFinalise DF on HF.Id = DF.ContributonSheetHeaderFinaliseID) GF on GF.Month = MDM.Name and GF.Year = MDM.Year and GF.ContributorId = MDM.ID inner join MasterContributor MC on MC.Id = MDM.Id inner join MasterEmployer ME on MC.employerid = ME.id inner join MasterMonthName MMN on MDM.Name = MMN.Name where(convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1)) >= '" + startDate + "')  AND(convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1)) <= '" + endDate + "') and(isnull(MC.FirstAppointmentDate, GetDate()) <= convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1))) and GF.Month is null and GF.Year is null");
        //SQL.Append("Select GF.Month as HMonth,GF.Year as Hyear,ME.Id as EmployerId,PersonId=MC.PersonId,GF.ContributorId as conId,ME.EmployerName,ContributorName=isnull(MC.FirstName,'')+' '+isnull(MC.MidName,'')+' '+isnull(MC.LastName,''),MDM.Name as Month,MDM.Year as Year,MDM.ID as ContributorId from (select tempMonth.Name, tempYear.Year, tempCont.ID from(select Name from MasterMonthName) tempMonth cross join(select distinct year from ContributonSheetHeaderFinalise) tempYear cross join(select Id from MasterContributor) tempCont) MDM left join (Select HF.Month, HF.Year, DF.ContributorId from ContributonSheetHeaderFinalise HF inner join ContributonSheetDetailsFinalise DF on HF.Id = DF.ContributonSheetHeaderFinaliseID) GF on GF.Month = MDM.Name and GF.Year = MDM.Year and GF.ContributorId = MDM.ID inner join MasterContributor MC on MC.Id = MDM.Id inner join MasterEmployer ME on MC.employerid = ME.id inner join MasterMonthName MMN on MDM.Name = MMN.Name where(convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1)) >= '" + startDate + "')  AND(convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1)) <= '" + endDate + "') and(isnull(MC.FirstAppointmentDate, GetDate()) <= convert(datetime, DATEFROMPARTS(MDM.year, MMN.Id, 1))) and GF.Month is null and GF.Year is null");

        if (!string.IsNullOrWhiteSpace(employerId))
          SQL.Append(" and ME.Id=" + employerId);
        if (!string.IsNullOrWhiteSpace(employeeId))
          SQL.Append(" and MDM.ID=" + employeeId);
        SQL.Append(" order by MDM.ID,MDM.Year,MMN.Id ");
        //string con = System.Web.Configuration.WebConfigurationManager.AppSettings["SQLConn"];
        string con = ConnectionStringProvider.GetConnectionString();
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(SQL.ToString(), con);
        da.SelectCommand.CommandTimeout = 0;
        da.Fill(ds);
        tempVerify = App.Web.Repository.ListToDataset.ToList<ExcelFileViewModel>(ds.Tables[0]);

      }
      IEnumerable<ExcelFileViewModel> filtered;
      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = tempVerify.Where(c => c.PersonId.ToLower().Contains(param.sSearch.ToLower())
                         || c.Month.ToString().ToLower().Contains(param.sSearch.ToLower())
                         || c.Year.ToString().ToLower().Contains(param.sSearch.ToLower())
                         || (c.ContributorName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
                         || (c.EmployerName).ToLower().Contains(param.sSearch.ToLower())
                         //|| c.Contributor.LastName.ToLower().Contains(param.sSearch.ToLower())
                         );

      }
      else
      {
        filtered = tempVerify;
      }

      //Pagging
      var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

      ////Select required columns
      var result = from c in displayed
                   select new[] {
                         c.PersonId,
                         c.EmployerName,
                         c.ContributorName,
                         c.Month,
                         c.Year+""
                   };

      return Json(
                                  new
                                  {
                                    sEcho = param.sEcho,
                                    iTotalRecords = tempVerify.Count(),
                                    iTotalDisplayRecords = filtered.Count(),
                                    aaData = result
                                  },
    JsonRequestBehavior.AllowGet);
    }
    //public ActionResult DuplicateContributionWithDifferentSourceAjax(JQueryDataTableParamModel param, string employerId, string employeeId, DateTime fromDate, DateTime to)
    //{
    //    string startDate = fromDate.ToString("MM/dd/yyyy");
    //    string endDate = to.ToString("MM/dd/yyyy");
    //    //List<ExcelFileViewModel> Verify = new List<ExcelFileViewModel>();
    //    List<ExcelFileViewModel> tempVerify = new List<ExcelFileViewModel>();
    //    using (AppDbContext dbContext = new AppDbContext())
    //    {
    //        DataSet ds = new DataSet();
    //        System.Text.StringBuilder SQL = new System.Text.StringBuilder();
    //        SQL.Append("select GF.Month,GF.Year,GF.ContributorId, MS.Name AS SourceName,MDF.SalaryAmount, MDF.ContributorContribution, MDF.EmployerContribution, MDF.Id,  ME.EmployerName, PersonId = MC.PersonID, (isnull(MC.FirstName, '') + ' ' +  isnull(MC.MidName, '') + ' ' + isnull(MC.LastName, '')) as ContributorName,  MM.Id As MonthNumber, SystemSalaryAmount = (MC.SalaryAmount / 12),  SystemContributorContribution = (MC.SalaryAmount / 1200) * MC.PFRate,  SystemEmployerContribution = (MC.SalaryAmount / 1200) * ME.PFRate  from MasterContributor MC  inner  join (SELECT        COUNT(*) as count, HF.Month, HF.Year, HF.EmployerId, HF.IsActive AS HFActive, DF.IsActive AS DFActive, DF.ContributorID, DF.ContributonSheetHeaderFinaliseID FROM            dbo.ContributonSheetDetailsFinalise AS DF INNER JOIN  dbo.ContributonSheetHeaderFinalise AS HF ON DF.ContributonSheetHeaderFinaliseID = HF.Id WHERE(HF.IsActive = 1) AND(DF.IsActive = 1) GROUP BY HF.Month, HF.Year, HF.EmployerId, HF.IsActive, DF.IsActive, DF.ContributorID, DF.ContributonSheetHeaderFinaliseID HAVING(COUNT(*) > 1)) GF ON MC.Id = GF.ContributorID  Inner join MasterMonthName MM ON GF.Month = MM.Name   inner join ContributonSheetDetailsFinalise MDF on GF.ContributonSheetHeaderFinaliseID = MDF.ContributonSheetHeaderFinaliseID  inner join MasterSource MS ON MDF.SourceID = MS.Id inner join MasterEmployer ME on GF.EmployerId = ME.Id");
    //        SQL.Append(" where MDF.isactive = 1 AND(convert(datetime, DATEFROMPARTS(GF.Year, MM.Id, 1)) >='" + startDate + "') AND(convert(datetime, DATEFROMPARTS(GF.Year, MM.Id, 1)) <='" + endDate + "')  ");
    //        if (!string.IsNullOrWhiteSpace(employerId))
    //            SQL.Append(" and GF.EmployerId=" + employerId);
    //        if (!string.IsNullOrWhiteSpace(employeeId))
    //            SQL.Append(" and GF.ContributorID=" + employeeId);
    //        SQL.Append(" ORDER BY GF.Year DESC, MonthNumber DESC");
    //        string con = System.Web.Configuration.WebConfigurationManager.AppSettings["SQLConn"];
    //        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(SQL.ToString(), con);
    //        da.Fill(ds);
    //        tempVerify = App.Web.Repository.ListToDataset.ToList<ExcelFileViewModel>(ds.Tables[0]);
    //        //Verify = tempVerify;
    //        //tempVerify = (from headerFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true && x.Year >= fromDate.Year && x.Year <= to.Year) join detailsFinalize in db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true) on headerFinalize.Id equals detailsFinalize.ContributonSheetHeaderFinaliseID select new ExcelFileViewModel { Contributor = detailsFinalize.Contributor, SalaryAmount = detailsFinalize.SalaryAmount, ContributorContribution = detailsFinalize.ContributorContribution, EmployerContribution = detailsFinalize.EmployerContribution, Employer = detailsFinalize.Contributor.Employer, Year = headerFinalize.Year, Source = detailsFinalize.Source, Id = detailsFinalize.Id, Month = headerFinalize.Month, EntryDate = detailsFinalize.EntryDate, PersonId = detailsFinalize.ContributorID.ToString(), EmployerID = headerFinalize.EmployerId }).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).ToList();
    //        ////tempVerify = (from headerFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true && x.Year >= fromDate.Year && x.Year <= to.Year && x.EmployerId.ToString() == (employerId == "" ? x.EmployerId.ToString() : employerId)) join detailsFinalize in db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true && x.ContributorID.ToString() == (employeeId == "" ? x.ContributorID.ToString() : employeeId)) on headerFinalize.Id equals detailsFinalize.ContributonSheetHeaderFinaliseID join months in db.MasterMonthName on headerFinalize.Month.ToLower() equals months.Name.ToLower() where ((headerFinalize.Year == fromDate.Year && months.Id >= fromDate.Month) || (headerFinalize.Year == to.Year && months.Id <= to.Month) || (headerFinalize.Year > fromDate.Year && headerFinalize.Year < to.Year)) select new ExcelFileViewModel { Contributor = detailsFinalize.Contributor, SalaryAmount = detailsFinalize.SalaryAmount, ContributorContribution = detailsFinalize.ContributorContribution, EmployerContribution = detailsFinalize.EmployerContribution, Employer = detailsFinalize.Contributor.Employer, Year = headerFinalize.Year, Source = detailsFinalize.Source, Id = detailsFinalize.Id, Month = headerFinalize.Month, EntryDate = detailsFinalize.EntryDate, PersonId = detailsFinalize.ContributorID.ToString(), EmployerID = headerFinalize.EmployerId, Final = months.Id.ToString() }).OrderByDescending(x => x.Year).ThenByDescending(x => x.Final).ToList();
    //        //foreach (var item in tempVerify)
    //        //{
    //        //    int Month = DateTime.ParseExact(item.Month, "MMMM", CultureInfo.CurrentCulture).Month;
    //        //    int monthLastDate = DateTime.DaysInMonth(item.Year, Month);
    //        //    DateTime date = Convert.ToDateTime(Month + "/" + monthLastDate + "/" + item.Year);
    //        //    if (date >= fromDate && date <= to)
    //        //    {
    //        //        Verify.Add(item);
    //        //    }
    //        //}
    //        ////Verify = (from monthName in db.MasterMonthName join headerFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true && x.Year >= fromDate.Year && x.Year <= to.Year) on monthName.Name.ToLower() equals headerFinalize.Month.ToLower() join detailsFinalize in db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true) on headerFinalize.Id equals detailsFinalize.ContributonSheetHeaderFinaliseID where monthName.Id>=3 && monthName.Id<=12 select new ExcelFileViewModel { Contributor = detailsFinalize.Contributor, SalaryAmount = detailsFinalize.SalaryAmount, ContributorContribution = detailsFinalize.ContributorContribution, EmployerContribution = detailsFinalize.EmployerContribution, Employer = detailsFinalize.Contributor.Employer, Year = headerFinalize.Year, Source = detailsFinalize.Source, Id = detailsFinalize.Id, Month = headerFinalize.Month, EntryDate = detailsFinalize.EntryDate, PersonId = detailsFinalize.ContributorID.ToString(), EmployerID = headerFinalize.EmployerId }).ToList();
    //        //if (!string.IsNullOrWhiteSpace(employerId))
    //        //    Verify = Verify.Where(x => x.EmployerID.ToString() == employerId).ToList();
    //        //if (!string.IsNullOrWhiteSpace(employeeId))
    //        //    Verify = Verify.Where(x => x.PersonId == employeeId).ToList();

    //    }
    //    IEnumerable<ExcelFileViewModel> filtered;
    //    if (!string.IsNullOrEmpty(param.sSearch))
    //    {
    //        filtered = tempVerify.Where(c => c.PersonId.ToLower().Contains(param.sSearch.ToLower())
    //                         || c.SalaryAmount.ToString().ToLower().Contains(param.sSearch.ToLower())
    //                         || c.ContributorContribution.ToString().ToLower().Contains(param.sSearch.ToLower())
    //                         || c.EmployerContribution.ToString().ToLower().Contains(param.sSearch.ToLower())
    //                         || (c.ContributorName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
    //                         || (c.EmployerName).ToLower().Contains(param.sSearch.ToLower())
    //                         //|| c.Contributor.LastName.ToLower().Contains(param.sSearch.ToLower())
    //                         );

    //    }
    //    else
    //    {
    //        filtered = tempVerify;
    //    }

    //    //Pagging
    //    var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

    //    ////Select required columns
    //    var result = from c in displayed
    //                 select new[] {
    //                 c.PersonId,
    //                 c.EmployerName,
    //                 c.ContributorName,
    //                 c.SourceName,
    //                 Convert.ToDecimal(c.SystemSalaryAmount).ToString("#,##0.00"),
    //                Convert.ToDecimal(c.SystemContributorContribution).ToString("#,##0.00"),
    //                Convert.ToDecimal(c.SystemEmployerContribution).ToString("#,##0.00"),
    //                 Convert.ToDecimal(c.SalaryAmount).ToString("#,##0.00"),
    //             Convert.ToDecimal(c.ContributorContribution).ToString("#,##0.00"),
    //             Convert.ToDecimal(c.EmployerContribution).ToString("#,##0.00"),
    //             c.Month+" - "+c.Year,
    //            c.Id+""
    //           };

    //    return Json(
    //                                new
    //                                {
    //                                    sEcho = param.sEcho,
    //                                    iTotalRecords = tempVerify.Count(),
    //                                    iTotalDisplayRecords = filtered.Count(),
    //                                    aaData = result
    //                                },
    //  JsonRequestBehavior.AllowGet);
    //}
    public ActionResult DuplicateContributionAjax(JQueryDataTableParamModel param, string employerId, string employeeId, DateTime fromDate, DateTime to)
    {
      string startDate = fromDate.ToString("MM/dd/yyyy");
      string endDate = to.ToString("MM/dd/yyyy");
      //List<ExcelFileViewModel> Verify = new List<ExcelFileViewModel>();
      List<ExcelFileViewModel> tempVerify = new List<ExcelFileViewModel>();
      using (AppDbContext dbContext = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
      {
        DataSet ds = new DataSet();
        System.Text.StringBuilder SQL = new System.Text.StringBuilder();
        SQL.Append("select GF.Month,GF.Year,GF.ContributorId, MS.Name AS SourceName,MDF.SalaryAmount, MDF.ContributorContribution, MDF.EmployerContribution, MDF.Id,  ME.EmployerName, PersonId = MC.PersonID, (isnull(MC.FirstName, '') + ' ' +  isnull(MC.MidName, '') + ' ' + isnull(MC.LastName, '')) as ContributorName,  MM.Id As MonthNumber, SystemSalaryAmount = (MC.SalaryAmount / 12),  SystemContributorContribution = (MC.SalaryAmount / 1200) * MC.PFRate,  SystemEmployerContribution = (MC.SalaryAmount / 1200) * ME.PFRate  from MasterContributor MC  inner  join (SELECT        COUNT(*) as count, HF.Month, HF.Year, HF.EmployerId, HF.IsActive AS HFActive, DF.IsActive AS DFActive, DF.SourceID, DF.ContributorID, DF.ContributonSheetHeaderFinaliseID FROM            dbo.ContributonSheetDetailsFinalise AS DF INNER JOIN  dbo.ContributonSheetHeaderFinalise AS HF ON DF.ContributonSheetHeaderFinaliseID = HF.Id WHERE(HF.IsActive = 1) AND(DF.IsActive = 1) GROUP BY HF.Month, HF.Year, HF.EmployerId, HF.IsActive, DF.IsActive, DF.SourceID, DF.ContributorID, DF.ContributonSheetHeaderFinaliseID HAVING(COUNT(*) > 1)) GF ON MC.Id = GF.ContributorID  Inner join MasterMonthName MM ON GF.Month = MM.Name  inner join MasterSource MS ON GF.SourceID = MS.Id  inner join ContributonSheetDetailsFinalise MDF on GF.ContributonSheetHeaderFinaliseID = MDF.ContributonSheetHeaderFinaliseID  inner join MasterEmployer ME on GF.EmployerId = ME.Id");
        SQL.Append(" where MDF.isactive = 1 AND(convert(datetime, DATEFROMPARTS(GF.Year, MM.Id, 1)) >='" + startDate + "') AND(convert(datetime, DATEFROMPARTS(GF.Year, MM.Id, 1)) <='" + endDate + "')  ");
        if (!string.IsNullOrWhiteSpace(employerId))
          SQL.Append(" and GF.EmployerId=" + employerId);
        if (!string.IsNullOrWhiteSpace(employeeId))
          SQL.Append(" and GF.ContributorID=" + employeeId);
        SQL.Append(" ORDER BY GF.Year DESC, MonthNumber DESC");
        //string con = System.Web.Configuration.WebConfigurationManager.AppSettings["SQLConn"];
        string con = ConnectionStringProvider.GetConnectionString();
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(SQL.ToString(), con);
        da.Fill(ds);
        tempVerify = App.Web.Repository.ListToDataset.ToList<ExcelFileViewModel>(ds.Tables[0]);
        //Verify = tempVerify;
        //tempVerify = (from headerFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true && x.Year >= fromDate.Year && x.Year <= to.Year) join detailsFinalize in db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true) on headerFinalize.Id equals detailsFinalize.ContributonSheetHeaderFinaliseID select new ExcelFileViewModel { Contributor = detailsFinalize.Contributor, SalaryAmount = detailsFinalize.SalaryAmount, ContributorContribution = detailsFinalize.ContributorContribution, EmployerContribution = detailsFinalize.EmployerContribution, Employer = detailsFinalize.Contributor.Employer, Year = headerFinalize.Year, Source = detailsFinalize.Source, Id = detailsFinalize.Id, Month = headerFinalize.Month, EntryDate = detailsFinalize.EntryDate, PersonId = detailsFinalize.ContributorID.ToString(), EmployerID = headerFinalize.EmployerId }).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).ToList();
        ////tempVerify = (from headerFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true && x.Year >= fromDate.Year && x.Year <= to.Year && x.EmployerId.ToString() == (employerId == "" ? x.EmployerId.ToString() : employerId)) join detailsFinalize in db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true && x.ContributorID.ToString() == (employeeId == "" ? x.ContributorID.ToString() : employeeId)) on headerFinalize.Id equals detailsFinalize.ContributonSheetHeaderFinaliseID join months in db.MasterMonthName on headerFinalize.Month.ToLower() equals months.Name.ToLower() where ((headerFinalize.Year == fromDate.Year && months.Id >= fromDate.Month) || (headerFinalize.Year == to.Year && months.Id <= to.Month) || (headerFinalize.Year > fromDate.Year && headerFinalize.Year < to.Year)) select new ExcelFileViewModel { Contributor = detailsFinalize.Contributor, SalaryAmount = detailsFinalize.SalaryAmount, ContributorContribution = detailsFinalize.ContributorContribution, EmployerContribution = detailsFinalize.EmployerContribution, Employer = detailsFinalize.Contributor.Employer, Year = headerFinalize.Year, Source = detailsFinalize.Source, Id = detailsFinalize.Id, Month = headerFinalize.Month, EntryDate = detailsFinalize.EntryDate, PersonId = detailsFinalize.ContributorID.ToString(), EmployerID = headerFinalize.EmployerId, Final = months.Id.ToString() }).OrderByDescending(x => x.Year).ThenByDescending(x => x.Final).ToList();
        //foreach (var item in tempVerify)
        //{
        //    int Month = DateTime.ParseExact(item.Month, "MMMM", CultureInfo.CurrentCulture).Month;
        //    int monthLastDate = DateTime.DaysInMonth(item.Year, Month);
        //    DateTime date = Convert.ToDateTime(Month + "/" + monthLastDate + "/" + item.Year);
        //    if (date >= fromDate && date <= to)
        //    {
        //        Verify.Add(item);
        //    }
        //}
        ////Verify = (from monthName in db.MasterMonthName join headerFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true && x.Year >= fromDate.Year && x.Year <= to.Year) on monthName.Name.ToLower() equals headerFinalize.Month.ToLower() join detailsFinalize in db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true) on headerFinalize.Id equals detailsFinalize.ContributonSheetHeaderFinaliseID where monthName.Id>=3 && monthName.Id<=12 select new ExcelFileViewModel { Contributor = detailsFinalize.Contributor, SalaryAmount = detailsFinalize.SalaryAmount, ContributorContribution = detailsFinalize.ContributorContribution, EmployerContribution = detailsFinalize.EmployerContribution, Employer = detailsFinalize.Contributor.Employer, Year = headerFinalize.Year, Source = detailsFinalize.Source, Id = detailsFinalize.Id, Month = headerFinalize.Month, EntryDate = detailsFinalize.EntryDate, PersonId = detailsFinalize.ContributorID.ToString(), EmployerID = headerFinalize.EmployerId }).ToList();
        //if (!string.IsNullOrWhiteSpace(employerId))
        //    Verify = Verify.Where(x => x.EmployerID.ToString() == employerId).ToList();
        //if (!string.IsNullOrWhiteSpace(employeeId))
        //    Verify = Verify.Where(x => x.PersonId == employeeId).ToList();

      }
      IEnumerable<ExcelFileViewModel> filtered;
      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = tempVerify.Where(c => c.PersonId.ToLower().Contains(param.sSearch.ToLower())
                         || c.SalaryAmount.ToString().ToLower().Contains(param.sSearch.ToLower())
                         || c.ContributorContribution.ToString().ToLower().Contains(param.sSearch.ToLower())
                         || c.EmployerContribution.ToString().ToLower().Contains(param.sSearch.ToLower())
                         || (c.ContributorName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
                         || (c.EmployerName).ToLower().Contains(param.sSearch.ToLower())
                         //|| c.Contributor.LastName.ToLower().Contains(param.sSearch.ToLower())
                         );

      }
      else
      {
        filtered = tempVerify;
      }

      //Pagging
      var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

      ////Select required columns
      var result = from c in displayed
                   select new[] {
                         c.PersonId,
                         c.EmployerName,
                         c.ContributorName,
                         c.SourceName,
                         Convert.ToDecimal(c.SystemSalaryAmount).ToString("#,##0.00"),
                        Convert.ToDecimal(c.SystemContributorContribution).ToString("#,##0.00"),
                        Convert.ToDecimal(c.SystemEmployerContribution).ToString("#,##0.00"),
                         Convert.ToDecimal(c.SalaryAmount).ToString("#,##0.00"),
                     Convert.ToDecimal(c.ContributorContribution).ToString("#,##0.00"),
                     Convert.ToDecimal(c.EmployerContribution).ToString("#,##0.00"),
                     c.Month+" - "+c.Year,
                    c.Id+""
                   };

      return Json(
                                  new
                                  {
                                    sEcho = param.sEcho,
                                    iTotalRecords = tempVerify.Count(),
                                    iTotalDisplayRecords = filtered.Count(),
                                    aaData = result
                                  },
    JsonRequestBehavior.AllowGet);
    }
    //public ActionResult EditDetailSearchAjax1(JQueryDataTableParamModel param, string employerId, string employeeId, DateTime fromDate, DateTime to)
    //{
    //    string startDate = fromDate.ToString("MM/dd/yyyy");
    //    string endDate = to.ToString("MM/dd/yyyy");
    //    //List<ExcelFileViewModel> Verify = new List<ExcelFileViewModel>();
    //    List<ExcelFileViewModel> tempVerify = new List<ExcelFileViewModel>();
    //    using (AppDbContext dbContext = new AppDbContext())
    //    {
    //        DataSet ds = new DataSet();
    //        System.Text.StringBuilder SQL = new System.Text.StringBuilder();
    //        SQL.Append("select dbo.ContributonSheetHeaderFinalise.Month, dbo.ContributonSheetHeaderFinalise.Year, dbo.MasterSource.Name AS SourceName, dbo.ContributonSheetDetailsFinalise.SalaryAmount, dbo.ContributonSheetDetailsFinalise.ContributorContribution, dbo.ContributonSheetDetailsFinalise.EmployerContribution , dbo.ContributonSheetDetailsFinalise.Id AS Id, dbo.MasterEmployer.EmployerName, PersonId=dbo.MasterContributor.PersonID,");
    //        SQL.Append("(isnull(dbo.MasterContributor.FirstName, '') + ' ' + isnull(dbo.MasterContributor.MidName, '') + ' ' + isnull(dbo.MasterContributor.LastName, '')) as ContributorName");
    //        SQL.Append(",dbo.MasterMonthName.Id As MonthNumber,SystemSalaryAmount = (dbo.MasterContributor.SalaryAmount / 12), SystemContributorContribution = (dbo.MasterContributor.SalaryAmount / 1200) * dbo.MasterContributor.PFRate, SystemEmployerContribution = (dbo.MasterContributor.SalaryAmount / 1200) * dbo.MasterEmployer.PFRate FROM            dbo.ContributonSheetDetailsFinalise INNER JOIN                         dbo.ContributonSheetHeaderFinalise ON dbo.ContributonSheetDetailsFinalise.ContributonSheetHeaderFinaliseID = dbo.ContributonSheetHeaderFinalise.Id   INNER JOIN dbo.MasterSource ON dbo.ContributonSheetDetailsFinalise.SourceID =  dbo.MasterSource.Id INNER JOIN                         dbo.MasterMonthName ON dbo.ContributonSheetHeaderFinalise.Month = dbo.MasterMonthName.Name INNER JOIN                         dbo.MasterContributor ON dbo.ContributonSheetDetailsFinalise.ContributorID = dbo.MasterContributor.Id INNER JOIN                         dbo.MasterEmployer ON dbo.ContributonSheetHeaderFinalise.EmployerId = dbo.MasterEmployer.Id WHERE(dbo.ContributonSheetHeaderFinalise.IsActive = 1) AND(convert(datetime, DATEFROMPARTS(ContributonSheetHeaderFinalise.Year, MasterMonthName.Id, 1))>='" + startDate + "') AND(convert(datetime, DATEFROMPARTS(ContributonSheetHeaderFinalise.Year, MasterMonthName.Id, 1))<='" + endDate + "') AND (dbo.ContributonSheetDetailsFinalise.IsActive = 1)");
    //        if (!string.IsNullOrWhiteSpace(employerId))
    //            SQL.Append("and dbo.ContributonSheetHeaderFinalise.EmployerId=" + employerId);
    //        if (!string.IsNullOrWhiteSpace(employeeId))
    //            SQL.Append("and dbo.ContributonSheetDetailsFinalise.ContributorID=" + employeeId);
    //        SQL.Append(" ORDER BY dbo.ContributonSheetHeaderFinalise.Year DESC, MonthNumber DESC");
    //        string con = System.Web.Configuration.WebConfigurationManager.AppSettings["SQLConn"];
    //        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(SQL.ToString(), con);
    //        da.Fill(ds);
    //        tempVerify = App.Web.Repository.ListToDataset.ToList<ExcelFileViewModel>(ds.Tables[0]);
    //        //Verify = tempVerify;
    //        //tempVerify = (from headerFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true && x.Year >= fromDate.Year && x.Year <= to.Year) join detailsFinalize in db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true) on headerFinalize.Id equals detailsFinalize.ContributonSheetHeaderFinaliseID select new ExcelFileViewModel { Contributor = detailsFinalize.Contributor, SalaryAmount = detailsFinalize.SalaryAmount, ContributorContribution = detailsFinalize.ContributorContribution, EmployerContribution = detailsFinalize.EmployerContribution, Employer = detailsFinalize.Contributor.Employer, Year = headerFinalize.Year, Source = detailsFinalize.Source, Id = detailsFinalize.Id, Month = headerFinalize.Month, EntryDate = detailsFinalize.EntryDate, PersonId = detailsFinalize.ContributorID.ToString(), EmployerID = headerFinalize.EmployerId }).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).ToList();
    //        ////tempVerify = (from headerFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true && x.Year >= fromDate.Year && x.Year <= to.Year && x.EmployerId.ToString() == (employerId == "" ? x.EmployerId.ToString() : employerId)) join detailsFinalize in db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true && x.ContributorID.ToString() == (employeeId == "" ? x.ContributorID.ToString() : employeeId)) on headerFinalize.Id equals detailsFinalize.ContributonSheetHeaderFinaliseID join months in db.MasterMonthName on headerFinalize.Month.ToLower() equals months.Name.ToLower() where ((headerFinalize.Year == fromDate.Year && months.Id >= fromDate.Month) || (headerFinalize.Year == to.Year && months.Id <= to.Month) || (headerFinalize.Year > fromDate.Year && headerFinalize.Year < to.Year)) select new ExcelFileViewModel { Contributor = detailsFinalize.Contributor, SalaryAmount = detailsFinalize.SalaryAmount, ContributorContribution = detailsFinalize.ContributorContribution, EmployerContribution = detailsFinalize.EmployerContribution, Employer = detailsFinalize.Contributor.Employer, Year = headerFinalize.Year, Source = detailsFinalize.Source, Id = detailsFinalize.Id, Month = headerFinalize.Month, EntryDate = detailsFinalize.EntryDate, PersonId = detailsFinalize.ContributorID.ToString(), EmployerID = headerFinalize.EmployerId, Final = months.Id.ToString() }).OrderByDescending(x => x.Year).ThenByDescending(x => x.Final).ToList();
    //        //foreach (var item in tempVerify)
    //        //{
    //        //    int Month = DateTime.ParseExact(item.Month, "MMMM", CultureInfo.CurrentCulture).Month;
    //        //    int monthLastDate = DateTime.DaysInMonth(item.Year, Month);
    //        //    DateTime date = Convert.ToDateTime(Month + "/" + monthLastDate + "/" + item.Year);
    //        //    if (date >= fromDate && date <= to)
    //        //    {
    //        //        Verify.Add(item);
    //        //    }
    //        //}
    //        ////Verify = (from monthName in db.MasterMonthName join headerFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true && x.Year >= fromDate.Year && x.Year <= to.Year) on monthName.Name.ToLower() equals headerFinalize.Month.ToLower() join detailsFinalize in db.ContributonSheetDetailsFinalise.Where(x => x.IsActive == true) on headerFinalize.Id equals detailsFinalize.ContributonSheetHeaderFinaliseID where monthName.Id>=3 && monthName.Id<=12 select new ExcelFileViewModel { Contributor = detailsFinalize.Contributor, SalaryAmount = detailsFinalize.SalaryAmount, ContributorContribution = detailsFinalize.ContributorContribution, EmployerContribution = detailsFinalize.EmployerContribution, Employer = detailsFinalize.Contributor.Employer, Year = headerFinalize.Year, Source = detailsFinalize.Source, Id = detailsFinalize.Id, Month = headerFinalize.Month, EntryDate = detailsFinalize.EntryDate, PersonId = detailsFinalize.ContributorID.ToString(), EmployerID = headerFinalize.EmployerId }).ToList();
    //        //if (!string.IsNullOrWhiteSpace(employerId))
    //        //    Verify = Verify.Where(x => x.EmployerID.ToString() == employerId).ToList();
    //        //if (!string.IsNullOrWhiteSpace(employeeId))
    //        //    Verify = Verify.Where(x => x.PersonId == employeeId).ToList();

    //    }
    //    IEnumerable<ExcelFileViewModel> filtered;
    //    if (!string.IsNullOrEmpty(param.sSearch))
    //    {
    //        filtered = tempVerify.Where(c => c.PersonId.ToLower().Contains(param.sSearch.ToLower())
    //                         || c.SalaryAmount.ToString().ToLower().Contains(param.sSearch.ToLower())
    //                         || c.ContributorContribution.ToString().ToLower().Contains(param.sSearch.ToLower())
    //                         || c.EmployerContribution.ToString().ToLower().Contains(param.sSearch.ToLower())
    //                         || (c.ContributorName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
    //                         || (c.EmployerName).ToLower().Contains(param.sSearch.ToLower())
    //                         //|| c.Contributor.LastName.ToLower().Contains(param.sSearch.ToLower())
    //                         );

    //    }
    //    else
    //    {
    //        filtered = tempVerify;
    //    }

    //    //Pagging
    //    var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

    //    ////Select required columns
    //    var result = from c in displayed
    //                 select new[] {
    //                 c.PersonId,
    //                 c.EmployerName,
    //                 c.ContributorName,
    //                 c.SourceName,
    //                 Convert.ToDecimal(c.SystemSalaryAmount).ToString("#,##0.00"),
    //                Convert.ToDecimal(c.SystemContributorContribution).ToString("#,##0.00"),
    //                Convert.ToDecimal(c.SystemEmployerContribution).ToString("#,##0.00"),
    //                 Convert.ToDecimal(c.SalaryAmount).ToString("#,##0.00"),
    //             Convert.ToDecimal(c.ContributorContribution).ToString("#,##0.00"),
    //             Convert.ToDecimal(c.EmployerContribution).ToString("#,##0.00"),
    //             c.Month+" - "+c.Year,
    //            c.Id+""
    //           };

    //    return Json(
    //                                new
    //                                {
    //                                    sEcho = param.sEcho,
    //                                    iTotalRecords = tempVerify.Count(),
    //                                    iTotalDisplayRecords = filtered.Count(),
    //                                    aaData = result
    //                                },
    //  JsonRequestBehavior.AllowGet);
    //}
    public ActionResult EditContributorFinalizedDetails1(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ExcelFileViewModel Verify = (from a in db.ContributonSheetDetailsFinalise
                                   join b in db.ContributonSheetHeaderFinalise on a.ContributonSheetHeaderFinaliseID equals b.Id
                                   where (a.IsActive == true && a.Id == id)
                                   select new ExcelFileViewModel
                                   {
                                     ContributorID = a.ContributorID,
                                     SalaryAmount = a.SalaryAmount,
                                     SystemSalaryAmount = (Math.Round((a.Contributor.SalaryAmount / 12), 2)),
                                     SystemContributorContribution = Math.Round(((a.Contributor.SalaryAmount / 1200) * (a.Contributor.PFRate)), 2),
                                     SystemEmployerContribution = Math.Round(((a.Contributor.SalaryAmount / 1200) * (a.Contributor.Employer.PFRate)), 2),
                                     ContributorContribution = a.ContributorContribution,
                                     EmployerContribution = a.EmployerContribution,
                                     Month = b.Month,
                                     Year = b.Year,
                                     Id = a.Id,
                                     SourceID = a.SourceID,
                                     EmployerID = b.EmployerId,
                                     EmployerName = a.Contributor.Employer.EmployerName,
                                     ContributorName = a.Contributor.FirstName + " " + a.Contributor.MidName + " " + a.Contributor.LastName

                                   }).FirstOrDefault();
      if (Verify == null)
      {
        return HttpNotFound();
      }
      ViewBag.Source = new SelectList(db.MasterSource.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", Verify.SourceID);
      return View(Verify);
    }
    [HttpPost]
    public ActionResult EditContributorFinalizedDetails1(ExcelFileViewModel model)
    {
      if (model != null)
      {

        ContributonSheetDetailsFinalise details = db.ContributonSheetDetailsFinalise.Find(model.Id);
        details.ContributorContribution = model.ContributorContribution == null ? 0 : Convert.ToDecimal(model.ContributorContribution);
        details.EmployerContribution = model.EmployerContribution == null ? 0 : Convert.ToDecimal(model.EmployerContribution);
        details.SalaryAmount = model.SalaryAmount == null ? 0 : Convert.ToDecimal(model.SalaryAmount);
        details.SourceID = model.SourceID;
        db.Entry(details).State = EntityState.Modified;
        int result = db.SaveChanges();

        if (result > 0)
        {
          TempData["success"] = "Record Saved Successfully";
          return RedirectToAction("FinalizedContributionErrorsDetails");
        }
        else
        {
          TempData["error"] = "Please Try Again";
        }

      }
      ViewBag.Source = new SelectList(db.MasterSource.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.SourceID);
      return View(model);
    }
    [HttpGet]
    public ActionResult ModifyFinalizedDetails(string Month, int? Year, int? EmpId)
    {
      //var months = (from x in db.ContributonSheetHeader
      //              join y in db.MasterMonthName on x.Month equals y.Name
      //              where x.IsActive == true
      //              select new
      //              {
      //                month = y.Id,
      //                monthname = x.Month
      //              }).OrderBy(o => o.month).Distinct();

      //ViewBag.Month = new SelectList(months, "monthname", "monthname");
      ViewBag.Month = new SelectList(db.MasterMonthName.Where(x => x.IsActive == true), "Name", "Name", Month);
      ViewBag.EmployerId = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", EmpId);
      int year = DateTime.Now.Year - 20;
      List<YearModel> yearModel = new List<YearModel>();
      for (int i = year; i <= DateTime.Now.Year; i++)
      {
        YearModel tempYear = new YearModel();
        tempYear.Id = i;
        tempYear.Name = i.ToString();
        yearModel.Add(tempYear);
      }
      ViewBag.Year = new SelectList(yearModel.OrderByDescending(x => x.Id), "Id", "Name", Year);
      return View();
    }

    //finalized method of finalized details
    public async Task<ActionResult> AddFinalize(string Month, int Year, int EmpId)
    {
      int monthInDigit = DateTime.ParseExact(Month, "MMMM", CultureInfo.CurrentCulture).Month;
      int monthEndDate = DateTime.DaysInMonth(Year, monthInDigit);
      DateTime date = Convert.ToDateTime(monthInDigit + "/" + monthEndDate + "/" + Year);
      if (Year == DateTime.Now.Year && monthInDigit == DateTime.Now.Month)
      {
        date = DateTime.Now;
      }
      int result = 0;
      bool Pensioner = false;
      var tempId = (from sheetHeader in db.ContributonSheetHeader.Where(x => x.IsActive == true) join sheetHeaderFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true) on sheetHeader.Id equals sheetHeaderFinalize.ContributonSheetHeaderId into rt from finalize in rt.DefaultIfEmpty() where sheetHeader.Month == Month && sheetHeader.Year == Year && sheetHeader.EmployerId == EmpId select new { Id = sheetHeader == null ? 0 : sheetHeader.Id, finalizedId = finalize == null ? 0 : finalize.Id }).FirstOrDefault();
      if (tempId == null)
      {
        var myAnonInstance = new
        {
          Id = 0,
          finalizedId = 0,
        };

        tempId = myAnonInstance;
      }
      //var mastercontributor = db.MasterContributor.Where(x => x.EmployerID.ToString() == EmployeeId);
      //var masteremployer = db.MasterEmployer.Where(x => x.Id.ToString() == EmployeeId);
      //var tempId = (from header in db.ContributonSheetHeader.Where(h => h.Month == Month && h.Year.ToString() == Year && h.EmployerId.ToString() == EmployeeId && h.IsActive == true)
      //              join finalizedheader in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true) on header.Id equals finalizedheader.ContributonSheetHeaderId into rt
      //              from detail in rt.DefaultIfEmpty()
      //              select new { Id = header.Id, finalizedId = detail == null ? 0 : detail.Id }).FirstOrDefault();
      List<ExcelFileViewModel> models = new List<ExcelFileViewModel>();
      if (tempId != null && tempId.Id != 0)
      {
        using (AppDbContext dbContext = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
        {
          models = await (from details in _DbContext.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == tempId.Id && x.IsActive == true)
                          where !_DbContext.ContributonSheetDetailsFinalise.Any(x => x.ContributonSheetHeaderFinaliseID == tempId.finalizedId && x.ContributorID == details.ContributorID && x.SourceID == details.SourceID && x.IsActive == true)
                          select new ExcelFileViewModel
                          {
                            ContributorID = details == null ? 0 : details.Contributor.Id,
                            SalaryAmount = details.SalaryAmount,
                            ContributorContribution = details.ContributorContribution,
                            EmployerContribution = details.EmployerContribution,
                            //Month = details.SourceID.ToString(),
                            Year = 0,
                            SourceID = details.SourceID,
                            //ContributorName = details == null ? "" : (details.Contributor.FirstName + " " + details.Contributor.MidName + " " + details.Contributor.LastName),

                            Id = details.Id,

                            EmplPFRateId = (int)details.EmplPFRateId,
                            EmplPFRate = (decimal)details.EmplPfRate,
                            EmplEffectiveStartDate = (DateTime)details.EmplEffectiveStartDate,
                            EmplEffectiveEndDate = (DateTime)details.EmplEffectiveEndDate,

                            EmplrPFRateId = (int)details.EmplrPFRateId,
                            EmplrPFRate = (decimal)details.EmplrPfRate,
                            EmplrEffectiveStartDate = (DateTime)details.EmplrEffectiveStartDate,
                            EmplrEffectiveEndDate = (DateTime)details.EmplrEffectiveEndDate
                          }).ToListAsync();

        }
      }




      //var tempId = (from header in db.ContributonSheetHeader.Where(h => h.Month == Month && h.Year == Year && h.EmployerId == EmpId)
      //              join finalizedheader in db.ContributonSheetHeaderFinalise on header.Id equals finalizedheader.ContributonSheetHeaderId into rt
      //              from detail in rt.DefaultIfEmpty()
      //              select new { Id = header.Id, finalizedId = detail == null ? 0 : detail.Id }).FirstOrDefault();
      //List<ExcelFileViewModel> models = new List<ExcelFileViewModel>();
      //if (tempId != null)
      //{
      //  models = (from details in db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == tempId.Id && x.IsActive == true)
      //            where !db.ContributonSheetDetailsFinalise.Any(x => x.ContributonSheetHeaderFinaliseID == tempId.finalizedId && x.ContributorID == details.ContributorID && x.SourceID == details.SourceID && x.IsActive == true)
      //            select new ExcelFileViewModel
      //            {
      //              ContributorID = details == null ? 0 : details.Contributor.Id,
      //              SalaryAmount = details.SalaryAmount,
      //              ContributorContribution = details.ContributorContribution,
      //              EmployerContribution = details.EmployerContribution,
      //              //Month = details.SourceID.ToString(),
      //              Year = 0,
      //              SourceID = details.SourceID,
      //              //ContributorName = details == null ? "" : (details.Contributor.FirstName + " " + details.Contributor.MidName + " " + details.Contributor.LastName),

      //              Id = details.Id,
      //            }).ToList();


      //}
      //List<ExcelFileViewModel> models = (from h in db.ContributonSheetHeader
      //                                   join d in db.ContributonSheetDetails.Where(x => x.IsActive == true) on h.Id equals d.ContributonSheetHeaderID into dt
      //                                   from details in dt.DefaultIfEmpty()
      //                                   join hf in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true) on h.Id equals hf.ContributonSheetHeaderId into chf
      //                                   from headerfinalize in chf.DefaultIfEmpty()
      //                                   join hd in db.ContributonSheetDetailsFinalise on headerfinalize.ContributonSheetHeaderId equals hd.ContributonSheetHeaderFinaliseID into cdf
      //                                   from detailsfinalize in cdf.DefaultIfEmpty()
      //                                   where (h.Month == Month && h.Year == Year && h.EmployerId == EmpId && detailsfinalize == null)
      //                                   select new ExcelFileViewModel
      //                                   {
      //                                     PersonID = details == null ? "" : details.ContributorID == null ? "" : details.ContributorID + "",
      //                                     SalaryAmount = details.SalaryAmount.ToString(),
      //                                     ContributorContribution = details.ContributorContribution.ToString(),
      //                                     EmployerContribution = details.EmployerContribution.ToString(),
      //                                     Month = h.Month,
      //                                     Year = h.Year,
      //                                     Source = details.SourceID + "",
      //                                     EmployerId = h.EmployerId.ToString(),
      //                                     ContributorName = details.Contributor.FirstName + " " + details.Contributor.MidName + " " + details.Contributor.LastName,
      //                                     Final = h.Finalized.ToString(),
      //                                     Id = details.Id,
      //                                   }).OrderByDescending(o => o.EmployerId).ToList();

      //var models = await (from a in db.ContributonSheetDetails
      //                    join b in db.ContributonSheetHeader on a.ContributonSheetHeaderID equals b.Id
      //                    where (b.Month == Month && b.Year == Year && b.EmployerId == EmpId && a.IsActive == true)
      //                    select new { a, b }).ToListAsync();
      if (models != null)
      {

        int dId = models.FirstOrDefault().Id;
        int id = db.ContributonSheetDetails.Where(x => x.Id == dId).Select(x => x.ContributonSheetHeaderID).FirstOrDefault();
        var modelfinalized = await db.ContributonSheetHeader.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (modelfinalized.Finalized != 1)
        {
          modelfinalized.Finalized = 1;
          db.Entry(modelfinalized).State = EntityState.Modified;
          db.SaveChanges();
        }
        var finalizedModel = db.ContributonSheetHeaderFinalise.Where(x => x.ContributonSheetHeaderId == modelfinalized.Id).FirstOrDefault();
        if (finalizedModel == null)
        {
          finalizedModel = new ContributonSheetHeaderFinalise();
          modelfinalized.MapTo(finalizedModel);
          finalizedModel.ContributonSheetHeaderId = modelfinalized.Id;
          db.Entry(finalizedModel).State = EntityState.Added;
          db.SaveChanges();
        }
        //get PFRate of Employer 
        //var EmployerPFRate = db.MasterEmployer.Where(x => x.IsActive && x.Id == EmpId).FirstOrDefault();
        foreach (var item in models)
        {
          //get PFRate of Contributor 
          //var ContributorPFRate = db.MasterContributor.Where(x => x.IsActive && x.Id == item.ContributorID).FirstOrDefault();

          var finalizeddetailsModel = new ContributonSheetDetailsFinalise();
          finalizeddetailsModel.ContributorContribution = Convert.ToDecimal(item.ContributorContribution);
          finalizeddetailsModel.EmployerContribution = Convert.ToDecimal(item.EmployerContribution);
          finalizeddetailsModel.SourceID = item.SourceID;
          finalizeddetailsModel.SalaryAmount = Convert.ToDecimal(item.SalaryAmount);
          finalizeddetailsModel.ContributonSheetHeaderFinaliseID = finalizedModel.Id;
          finalizeddetailsModel.ContributorID = item.ContributorID;
          finalizeddetailsModel.EntryDate = date;
          finalizeddetailsModel.IsActive = true;

          finalizeddetailsModel.EmplrPFRateId = item.EmplrPFRateId;
          finalizeddetailsModel.EmplrPfRate = item.EmplrPFRate;
          finalizeddetailsModel.EmplrEffectiveStartDate = item.EmplrEffectiveStartDate;
          finalizeddetailsModel.EmplrEffectiveEndDate = item.EmplrEffectiveEndDate;


          finalizeddetailsModel.EmplPFRateId = item.EmplPFRateId;
          finalizeddetailsModel.EmplPfRate = item.EmplPFRate;
          finalizeddetailsModel.EmplEffectiveStartDate = item.EmplEffectiveStartDate;
          finalizeddetailsModel.EmplEffectiveEndDate = item.EmplEffectiveEndDate;

          //finalizeddetailsModel.SourceID = Convert.ToInt32(item.Source);
          db.Entry(finalizeddetailsModel).State = EntityState.Added;
          result = db.SaveChanges();

          var contributorTransaction = new ContributonTransactions();
          contributorTransaction.CSDFID = finalizeddetailsModel.Id;
          contributorTransaction.TransactionType = "C";
          contributorTransaction.TransactionName = "Contributor's Amount";
          contributorTransaction.TransactionAmount = Convert.ToDecimal(item.ContributorContribution);
          db.Entry(contributorTransaction).State = EntityState.Added;
          db.SaveChanges();
          var masterContributor = db.MasterContributor.Where(x => x.Id == item.ContributorID).FirstOrDefault();
          //try
          //{            
          //  var pfrate = masterContributor.PFRate;
          //  var Contribution = (masterContributor.SalaryAmount / 100) * pfrate;

          //  if (Month == "January" || Month == "April" || Month == "July" || Month == "October")
          //  {
          //    masterContributor.Balance = Math.Round((masterContributor.Balance + Contribution), 2);
          //    masterContributor.QuarterlyAmount = Math.Round(Contribution, 2);
          //  }
          //  else
          //  {
          //    masterContributor.Balance = Math.Round((masterContributor.Balance + Contribution), 2);
          //    masterContributor.QuarterlyAmount = Math.Round((masterContributor.QuarterlyAmount + Contribution), 2);
          //    masterContributor.AnnualAmount = Math.Round((masterContributor.AnnualAmount + Contribution), 2);
          //  }
          //  if (Month == "April")
          //  {
          //    masterContributor.AnnualAmount = Math.Round(Contribution, 2);
          //  }

          //  db.Entry(masterContributor).State = EntityState.Modified;
          //  result = db.SaveChanges();
          //}
          //catch { }
          if (!Pensioner)
          {
            if (masterContributor.JobStatusID == (int)jobStatus.Pensioner || db.PensionApplications.Where(x => x.PersonID == masterContributor.Id.ToString() && x.IsActive == true).Any())
            {
              Pensioner = true;
            }
          }
        }
      }
      if (result > 0)
      {
        if (Pensioner)
        {
          var contributorList = db.MasterContributor.Where(x => x.IsActive == true).ToList();
          List<PensionOrRefundViewModel> List = new List<PensionOrRefundViewModel>();
          int i = 0;
          foreach (var item in models)
          {
            var status = contributorList.Where(x => x.Id == item.ContributorID).Select(x => new { x.JobStatusID, x.PersonID, Name = x.FirstName + " " + x.MidName + " " + x.LastName, x.Id }).FirstOrDefault();
            PensionOrRefundViewModel log = new PensionOrRefundViewModel();
            if (!List.Where(x => x.contributorId + "" == status.PersonID).Any())
            {
              if (status.JobStatusID != (int)jobStatus.Pensioner && db.PensionApplications.Where(x => x.PersonID == status.Id.ToString() && x.IsActive == true).Any())
              {
                PensionApplications penDetails = db.PensionApplications.Where(x => x.PersonID == status.Id.ToString() && x.IsActive == true).FirstOrDefault();
                log.ApplicationType = "P";
                log.Id = i + "";
                log.contributorId = status.PersonID;
                log.Name = status.Name;
                log.OldAmount = penDetails.pensionPerAnnum;
                List.Add(log);

                //RefundPaidDetails refDetails = db.RefundPaidDetails.Where(x => x.PersonID == item.ContributorID.ToString()).FirstOrDefault();
                //log.ApplicationType = "R";
                //log.Id = i + "";
                //log.contributorId = status.PersonID;
                //log.Name = status.Name;
                //log.OldAmount = refDetails.RefundAmt;
                //List.Add(log);
              }
              else if (status.JobStatusID == (int)jobStatus.Pensioner)
              {
                DVOMasterEmployee objSearchCriteriaDVOMasterEmployeeModel = new DVOMasterEmployee();
                objSearchCriteriaDVOMasterEmployeeModel.PersonID = status.PersonID;
                var checkRecord = SearchEmployeeInformation(objSearchCriteriaDVOMasterEmployeeModel);
                if (checkRecord.Any())
                {
                  log.Id = i + "";
                  log.contributorId = status.PersonID;
                  log.Name = status.Name;
                  log.ApplicationType = "P";
                  log.OldAmount = checkRecord.FirstOrDefault().AnnualSalary;
                  List.Add(log);
                }
              }
              i++;
            }
          }
          return Json(List, JsonRequestBehavior.AllowGet);
        }
        TempData["success"] = "Record Finalized successfully";
      }
      return Json(result, JsonRequestBehavior.AllowGet);

    }


    //for searching in finalised details
    //public async Task<JsonResult> DetailINSERTBatchProcessInfoAjax(int EmpId, string Month, int Year)
    //{
    //  var result = await (from a in db.ContributonSheetDetails
    //                      join b in db.ContributonSheetHeader on a.ContributonSheetHeaderID equals b.Id
    //                      where (b.Finalized == 0 && b.Month == Month && b.Year == Year && b.EmployerId == EmpId && a.IsActive == true)
    //                      select new { personId = a.Contributor.PersonID, contributorName = a.Contributor.FirstName + " " + a.Contributor.MidName + " " + a.Contributor.LastName, conContribution = a.ContributorContribution.ToString(), empContribution = a.EmployerContribution.ToString(), salary = a.SalaryAmount.ToString(), systemSalary = a.Contributor.SalaryAmount.ToString(), conPf = a.Contributor.PFRate.ToString(), empPf = a.Contributor.Employer.PFRate.ToString() }).ToListAsync();
    //  return Json(result, JsonRequestBehavior.AllowGet);

    //}

    public ActionResult getFinalizeTotalContribution(string empId, string month, string year)
    {
      if (empId == null && !string.IsNullOrWhiteSpace(month) && year == null)
      {
        month = String.Format("{0:MMMM}", DateTime.Now);
        year = DateTime.Now.Year.ToString();
        empId = "2";
      }
      var tempId = (from sheetHeader in db.ContributonSheetHeader.Where(x => x.IsActive == true) join sheetHeaderFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true) on sheetHeader.Id equals sheetHeaderFinalize.ContributonSheetHeaderId into rt from finalize in rt.DefaultIfEmpty() where sheetHeader.Month == month && sheetHeader.Year.ToString() == year && sheetHeader.EmployerId.ToString() == empId select new { Id = sheetHeader == null ? 0 : sheetHeader.Id, finalizedId = finalize == null ? 0 : finalize.Id }).FirstOrDefault();
      if (tempId == null)
      {
        var myAnonInstance = new
        {
          Id = 0,
          finalizedId = 0,
        };

        tempId = myAnonInstance;
      }

      List<ExcelFileViewModel> Verify = new List<ExcelFileViewModel>();
      List<ExcelFileViewModel> tempVerify = new List<ExcelFileViewModel>();
      List<ExcelFileViewModel> temp2Verify = new List<ExcelFileViewModel>();
      if (tempId.Id != 0)
      {
        tempVerify = (from details in db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == tempId.Id && x.IsActive == true)
                      where !db.ContributonSheetDetailsFinalise.Any(x => x.ContributonSheetHeaderFinaliseID == tempId.finalizedId && x.ContributorID == details.ContributorID && x.SourceID == details.SourceID && x.IsActive == true)
                      select new ExcelFileViewModel
                      {
                        Contributor = details.Contributor,
                        SalaryAmount = details.SalaryAmount,
                        ContributorContribution = details.ContributorContribution,
                        EmployerContribution = details.EmployerContribution,
                        Employer = details.Contributor.Employer,
                        Year = 0,
                        Source = details.Source,
                        //ContributorName = details == null ? "" : (details.Contributor.FirstName + " " + details.Contributor.MidName + " " + details.Contributor.LastName),
                        Final = "0",
                        //Id = details.Id,
                      }).ToList();

        if (tempId.finalizedId != 0)
        {
          temp2Verify = db.ContributonSheetDetailsFinalise.Where(x => x.ContributonSheetHeaderFinaliseID == tempId.finalizedId && x.IsActive == true).Select(x =>
                         new ExcelFileViewModel
                         {
                           Contributor = x.Contributor,
                           SalaryAmount = x.SalaryAmount,
                           ContributorContribution = x.ContributorContribution,
                           EmployerContribution = x.EmployerContribution,
                           Employer = x.Contributor.Employer,
                           Year = 0,
                           Source = x.Source,
                           //ContributorName = details == null ? "" : (details.Contributor.FirstName + " " + details.Contributor.MidName + " " + details.Contributor.LastName),
                           Final = "1",
                           //Id = x.Id,
                         }).ToList();

        }
        Verify = tempVerify.Union(temp2Verify).ToList();

      }
      foreach (var details in Verify)
      {
        details.SystemContributorContribution = Convert.ToDecimal(details.Contributor == null ? "N/A" : Math.Round((details.Contributor.SalaryAmount / 1200) * (details.Contributor.PFRate), 2, MidpointRounding.AwayFromZero).ToString("#,##0.00"));
        details.SystemEmployerContribution = Convert.ToDecimal(details.Contributor == null ? "N/A" : Math.Round((details.Contributor.SalaryAmount / 1200) * (details.Employer.PFRate), 2, MidpointRounding.AwayFromZero).ToString("#,##0.00"));
      }
      string EmployerTotalContribution = "EC$ " + Convert.ToDecimal(Verify.Sum(x => x.EmployerContribution)).ToString("#,##0.00");
      string ContributorTotalContribution = "EC$ " + Convert.ToDecimal(Verify.Sum(x => x.ContributorContribution)).ToString("#,##0.00");

      string SystemEmployerTotalContribution = "EC$ " + Convert.ToDecimal(Verify.Sum(x => x.SystemEmployerContribution)).ToString("#,##0.00");
      string SystemContributorTotalContribution = "EC$ " + Convert.ToDecimal(Verify.Sum(x => x.SystemContributorContribution)).ToString("#,##0.00");
      return Json(EmployerTotalContribution + "#" + ContributorTotalContribution + "#" + SystemEmployerTotalContribution + "#" + SystemContributorTotalContribution, JsonRequestBehavior.AllowGet);
    }
    public ActionResult DetailContributionSearchAjax(JQueryDataTableParamModel param)
    {
      try
      {
        //var display = db.MasterBeneficiaries.OrderByDescending(x => x.CreatedOn).Skip(0).Take(1).Where(x => x.IsProcessed == true).ToList();
        //var currentFinancialYear = GetCurrentSelectedFinancialYear();
      
        var currentFinancialYearDateby = stringCurrentSelectedFinancialYear();
        DateTime FromDateFinancialYear = Convert.ToDateTime(currentFinancialYearDateby.Split('-')[0], System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
        DateTime ToDateFinancialYear = Convert.ToDateTime(currentFinancialYearDateby.Split('-')[1], System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
        var display = db.MasterBeneficiaries.Where(x => x.IsProcessed == true && DbFunctions.TruncateTime(x.CreatedOn) >= FromDateFinancialYear && DbFunctions.TruncateTime(x.CreatedOn) <= ToDateFinancialYear).OrderByDescending(x => x.Id).ToList();


        IEnumerable<MasterBeneficiaries> filtered;

        if (!string.IsNullOrEmpty(param.sSearch))
        {
          filtered = display
             .Where(c => c.Id.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.Finalized.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.IsProcessed.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.FilePath.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.CreatedOn.ToString().ToLower().Contains(param.sSearch.ToLower())

             );

        }
        else
        {
          filtered = display;
        }
        //Sorting through column index
        var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

        Func<MasterBeneficiaries, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Id + "" :
                                                              sortColumnIndex == 1 ? c.Finalized + "" :
                                                              sortColumnIndex == 2 ? c.IsProcessed + "" :
                                                              sortColumnIndex == 3 ? c.FilePath + "" :
                                                              sortColumnIndex == 4 ? c.CreatedOn + "" : "");

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
                  c.Id + "",
                   Path.GetFileName(c.FilePath) + "",
                  c.Finalized + "",
                  c.CreatedOn+"",
                  //c.IsActive +"",
                   c.IsProcessed + "",
            };

        return Json(
                new
                {
                  sEcho = param.sEcho,
                  iTotalRecords = display.Count(),
                  iTotalDisplayRecords = filtered.Count(),
                  aaData = result
                },
      JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public ActionResult DetailSearchAjax(JQueryDataTableParamModel param, string Month, string Year, string EmployeeId)
    {
      try
      {
        // show List Only one  Record

        //var display = db.MasterBeneficiaries.OrderByDescending(x => x.CreatedOn).Skip(0).Take(1).Where(x => x.IsProcessed == false).ToList();

        // Show to All List Record 
        var currentFinancialYearDateby = stringCurrentSelectedFinancialYear();
        //DateTime FromDateFinancialYear = Convert.ToDateTime(currentFinancialYearDateby.Split('-')[0]).Date;
        //DateTime ToDateFinancialYear = Convert.ToDateTime(currentFinancialYearDateby.Split('-')[1]).Date;

        string FromDateFinancialYear =currentFinancialYearDateby.Split('-')[0];
        string ToDateFinancialYear = currentFinancialYearDateby.Split('-')[1];

        //var display = db.MasterBeneficiaries.OrderByDescending(x => x.CreatedOn ).Where(x => x.IsProcessed == false && DbFunctions.TruncateTime(x.CreatedOn) >= FromDateFinancialYear && DbFunctions.TruncateTime(x.CreatedOn) <= DbFunctions.TruncateTime(ToDateFinancialYear)).ToList();
        List<MasterBeneficiaries> display = new List<MasterBeneficiaries>();
        string  conString= ConnectionStringProvider.GetConnectionString();

        string sqlQuery = @"SELECT *  FROM MasterBeneficiaries  WHERE IsProcessed = 0 And IsActive=1  AND CONVERT(DATE, CreatedOn) >= @FromDateFinancialYear
                               AND CONVERT(DATE, CreatedOn) <= @ToDateFinancialYear
                               ORDER BY CreatedOn DESC";

        using (var connection = new SqlConnection(conString))
        using (var command = new SqlCommand(sqlQuery, connection))
        {
          // Add parameters to the SQL command
          command.Parameters.AddWithValue("@FromDateFinancialYear", DateTime.ParseExact(FromDateFinancialYear, "dd/MM/yyyy", CultureInfo.InvariantCulture));
          command.Parameters.AddWithValue("@ToDateFinancialYear", DateTime.ParseExact(ToDateFinancialYear, "dd/MM/yyyy", CultureInfo.InvariantCulture));

          connection.Open();
          using (var reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              // Create a new instance of MasterBeneficiaries
              MasterBeneficiaries beneficiary = new MasterBeneficiaries
              {
                FilePath = reader["FilePath"] != DBNull.Value ? reader["FilePath"].ToString() : string.Empty,
                Finalized = reader["Finalized"] != DBNull.Value ? Convert.ToInt32(reader["Finalized"]) : 0,
                IsProcessed = reader["IsProcessed"] != DBNull.Value ? Convert.ToBoolean(reader["IsProcessed"]) : false,
                Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0
              };

              // Add the beneficiary to the list
              display.Add(beneficiary);
            }
          }
        }
        IEnumerable<MasterBeneficiaries> filtered;

        if (!string.IsNullOrEmpty(param.sSearch))
        {
          filtered = display.Where(c => c.FilePath.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.Finalized.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.IsProcessed.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.Id.ToString().ToLower().Contains(param.sSearch.ToLower()));
        }
        else
        {
          filtered = display;
        }

        //Sorting through column index
        var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

        Func<MasterBeneficiaries, string> orderingFunction = (c => sortColumnIndex == 0 ? c.FilePath + "" :
                                                                   sortColumnIndex == 1 ? c.Finalized + "" :
                                                                   sortColumnIndex == 2 ? c.IsProcessed + "" :
                                                                   sortColumnIndex == 3 ? c.Id + "" :
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
                         Path.GetFileName(c.FilePath) + "",
                         c.Finalized + "",
                         c.IsProcessed + "",
                         c.Id + ""
                   };

        return Json(
                new
                {
                  sEcho = param.sEcho,
                  iTotalRecords = display.Count(),
                  iTotalDisplayRecords = filtered.Count(),
                  aaData = result
                },
      JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public ActionResult FinalizeTotalContributionAjax(Int32 Id)
      {
      try
      {
        int UserId = AppUserManager.GetUserId();
        // Define the parameters if needed (e.g., for input parameters)
        var parameter1 = new SqlParameter("@Id", Id);
        var parameter2 = new SqlParameter("@UserId", UserId);
        // Execute the stored procedure
        int result = db.Database.ExecuteSqlCommand("EXEC USP_EmployeeIns_EmpIncomIns_EmpDirDpstIns @Id,@UserId", parameter1, parameter2);

        return Json("Finalized", JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        return Json(ex.Message, JsonRequestBehavior.AllowGet);
      }
    }

    public ActionResult getModifyFinalizeTotalContribution(string empId, string month, string year)
    {
      if (empId == null && !string.IsNullOrWhiteSpace(month) && year == null)
      {
        month = String.Format("{0:MMMM}", DateTime.Now);
        year = DateTime.Now.Year.ToString();
        empId = "2";
      }
      var tempId = (from sheetHeader in db.ContributonSheetHeader.Where(x => x.IsActive == true) join sheetHeaderFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true) on sheetHeader.Id equals sheetHeaderFinalize.ContributonSheetHeaderId into rt from finalize in rt.DefaultIfEmpty() where sheetHeader.Month == month && sheetHeader.Year.ToString() == year && sheetHeader.EmployerId.ToString() == empId select new { Id = sheetHeader == null ? 0 : sheetHeader.Id, finalizedId = finalize == null ? 0 : finalize.Id }).FirstOrDefault();
      if (tempId == null)
      {
        var myAnonInstance = new
        {
          Id = 0,
          finalizedId = 0,
        };

        tempId = myAnonInstance;
      }
      List<ExcelFileViewModel> Verify = new List<ExcelFileViewModel>();
      if (tempId != null)
      {
        using (AppDbContext dbContext = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
        {
          if (tempId.finalizedId != 0)
          {
            //Changed by Neeraj 27Oct2018
            var finalisedata = db.ContributonSheetDetailsFinalise.Where(x => x.ContributonSheetHeaderFinaliseID == tempId.finalizedId && x.IsActive == true).ToList();
            foreach (var x in finalisedata)
            {
              var data = new ExcelFileViewModel { Contributor = x.Contributor, SalaryAmount = x.SalaryAmount, ContributorContribution = x.ContributorContribution, EmployerContribution = x.EmployerContribution, Employer = x.Contributor.Employer, Year = 0, Source = x.Source, Id = x.Id };
              Verify.Add(data);
            }
            //Commented by Neeraj 27Oct2018
            //Verify = db.ContributonSheetDetailsFinalise.Where(x => x.ContributonSheetHeaderFinaliseID == tempId.finalizedId && x.IsActive == true).Select(x => new ExcelFileViewModel { Contributor = x.Contributor, SalaryAmount = x.SalaryAmount, ContributorContribution = x.ContributorContribution, EmployerContribution = x.EmployerContribution, Employer = x.Contributor.Employer, Year = 0, Source = x.Source, Id = x.Id }).ToList();
          }
        }
      }
      foreach (var details in Verify)
      {
        details.SystemContributorContribution = Convert.ToDecimal(details.Contributor == null ? "N/A" : Math.Round((details.Contributor.SalaryAmount / 1200) * (details.Contributor.PFRate), 2, MidpointRounding.AwayFromZero).ToString("#,##0.00"));
        details.SystemEmployerContribution = Convert.ToDecimal(details.Contributor == null ? "N/A" : Math.Round((details.Contributor.SalaryAmount / 1200) * (details.Employer.PFRate), 2, MidpointRounding.AwayFromZero).ToString("#,##0.00"));
      }
      string EmployerTotalContribution = "EC$ " + Convert.ToDecimal(Verify.Sum(x => x.EmployerContribution)).ToString("#,##0.00");
      string ContributorTotalContribution = "EC$ " + Convert.ToDecimal(Verify.Sum(x => x.ContributorContribution)).ToString("#,##0.00");

      string SystemEmployerTotalContribution = "EC$ " + Convert.ToDecimal(Verify.Sum(x => x.SystemEmployerContribution)).ToString("#,##0.00");
      string SystemContributorTotalContribution = "EC$ " + Convert.ToDecimal(Verify.Sum(x => x.SystemContributorContribution)).ToString("#,##0.00");
      return Json(EmployerTotalContribution + "#" + ContributorTotalContribution + "#" + SystemEmployerTotalContribution + "#" + SystemContributorTotalContribution, JsonRequestBehavior.AllowGet);
    }

    public ActionResult EditDetailSearchAjax(JQueryDataTableParamModel param, string Month, string Year, string EmployeeId)
    {
      //var mastercontributor = db.MasterContributor.Where(x => x.EmployerID.ToString() == EmployeeId);
      //var masteremployer = db.MasterEmployer.Where(x => x.Id.ToString() == EmployeeId);
      var tempId = (from sheetHeader in db.ContributonSheetHeader.Where(x => x.IsActive == true) join sheetHeaderFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true) on sheetHeader.Id equals sheetHeaderFinalize.ContributonSheetHeaderId into rt from finalize in rt.DefaultIfEmpty() where sheetHeader.Month == Month && sheetHeader.Year.ToString() == Year && sheetHeader.EmployerId.ToString() == EmployeeId select new { Id = sheetHeader == null ? 0 : sheetHeader.Id, finalizedId = finalize == null ? 0 : finalize.Id }).FirstOrDefault();
      if (tempId == null)
      {
        var myAnonInstance = new
        {
          Id = 0,
          finalizedId = 0,
        };

        tempId = myAnonInstance;
      }
      List<ExcelFileViewModel> Verify = new List<ExcelFileViewModel>();
      if (tempId != null)
      {
        using (AppDbContext dbContext = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
        {
          if (tempId.finalizedId != 0)
          {
            //Changed by Neeraj 27Oct2018
            var finalisedata = db.ContributonSheetDetailsFinalise.Where(x => x.ContributonSheetHeaderFinaliseID == tempId.finalizedId && x.IsActive == true).ToList();
            foreach (var x in finalisedata)
            {
              var data = new ExcelFileViewModel { Contributor = x.Contributor, SalaryAmount = x.SalaryAmount, ContributorContribution = x.ContributorContribution, EmployerContribution = x.EmployerContribution, Employer = x.Contributor.Employer, Year = 0, Source = x.Source, Id = x.Id };
              Verify.Add(data);
            }
            //Commented by Neeraj 27Oct2018
            //Verify = db.ContributonSheetDetailsFinalise.Where(x => x.ContributonSheetHeaderFinaliseID == tempId.finalizedId && x.IsActive == true).Select(x => new ExcelFileViewModel { Contributor = x.Contributor, SalaryAmount = x.SalaryAmount, ContributorContribution = x.ContributorContribution, EmployerContribution = x.EmployerContribution, Employer = x.Contributor.Employer, Year = 0, Source = x.Source, Id = x.Id }).ToList();
          }
          //Verify = (from details in db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == tempId.Id && x.IsActive == true)
          //          where db.ContributonSheetDetailsFinalise.Any(x => x.ContributonSheetHeaderFinaliseID == tempId.finalizedId && x.ContributorID == details.ContributorID && x.SourceID == details.SourceID && x.IsActive == true)
          //          select new ExcelFileViewModel
          //          {
          //            PersonID = details == null ? "" : details.Contributor.PersonID,
          //            SalaryAmount = details.SalaryAmount.ToString(),
          //            ContributorContribution = details.ContributorContribution.ToString(),
          //            EmployerContribution = details.EmployerContribution.ToString(),
          //            EmployerId = details.Contributor.EmployerID + "",
          //            Year = 0,
          //            Source = details.Source.Name.ToString(),
          //            ContributorName = details == null ? "" : (details.Contributor.FirstName + " " + details.Contributor.MidName + " " + details.Contributor.LastName),
          //            Id = details.Id,
          //          }).ToList();

        }
      }

      IEnumerable<ExcelFileViewModel> filtered;
      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = Verify.Where(c => c.Contributor.PersonID.ToLower().Contains(param.sSearch.ToLower())
                         || c.SalaryAmount.ToString().ToLower().Contains(param.sSearch.ToLower())
                         || c.ContributorContribution.ToString().ToLower().Contains(param.sSearch.ToLower())
                         || c.EmployerContribution.ToString().ToLower().Contains(param.sSearch.ToLower())
                         || (c.Contributor.FirstName + " " + c.Contributor.MidName + " " + c.Contributor.LastName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
                         //|| (c.Contributor.MidName + "").ToLower().Contains(param.sSearch.ToLower())
                         //|| c.Contributor.LastName.ToLower().Contains(param.sSearch.ToLower())
                         );

      }
      else
      {
        filtered = Verify;
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
      var result = from c in displayed.OrderBy(x => x.Final)
                   select new[] {
                         c.Contributor.PersonID,
                         c.Contributor.FirstName+" "+c.Contributor.MidName+" "+c.Contributor.LastName,
                         c.Source.Name,
                         (c.Contributor.SalaryAmount/12).ToString("#,##0.00"),
                        Math.Round(((c.Contributor.SalaryAmount/1200) * c.Contributor.PFRate),2, MidpointRounding.AwayFromZero).ToString("#,##0.00"),
                        Math.Round(((c.Contributor.SalaryAmount/1200) * c.Employer.PFRate),2, MidpointRounding.AwayFromZero).ToString("#,##0.00"),
                        Convert.ToDecimal(c.SalaryAmount).ToString("#,##0.00"),
                     Convert.ToDecimal(c.ContributorContribution).ToString("#,##0.00"),
                     Convert.ToDecimal(c.EmployerContribution).ToString("#,##0.00"),
                    c.Id+""
                   };

      return Json(
                                  new
                                  {
                                    sEcho = param.sEcho,
                                    iTotalRecords = Verify.Count(),
                                    iTotalDisplayRecords = filtered.Count(),
                                    aaData = result
                                  },
    JsonRequestBehavior.AllowGet);
    }

    public JsonResult RemoveAjax(string Id)
    {
      int result = 0;
      if (Id != "0")
      {
        #region ContributonSheetHeaderDeleteDetails
        int id = Convert.ToInt32(Id);
        ContributonSheetDetails entity = db.ContributonSheetDetails.Where(x => x.Id == id).FirstOrDefault();
        entity.IsActive = false;
        db.Entry(entity).State = EntityState.Modified;
        result = db.SaveChanges();
        if (!db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == entity.ContributonSheetHeaderID && x.IsActive == true).Any())
        {
          var model = db.ContributonSheetHeader.Where(x => x.Id == entity.ContributonSheetHeaderID && x.IsActive == true).FirstOrDefault();
          if (model != null)
          {
            model.IsActive = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
          }
        }

        #endregion

      }

      return Json(result, JsonRequestBehavior.AllowGet);
    }
    public JsonResult RemoveAjaxFinalize(string Id)
    {
      int result = 0;
      if (Id != "0")
      {
        int id = Convert.ToInt32(Id);
        ContributonSheetDetailsFinalise entity = db.ContributonSheetDetailsFinalise.Where(x => x.Id == id).FirstOrDefault();
        entity.IsActive = false;
        db.Entry(entity).State = EntityState.Modified;
        result = db.SaveChanges();
        if (result > 0)
        {
          int fhId = db.ContributonSheetHeaderFinalise.Where(x => x.Id == entity.ContributonSheetHeaderFinaliseID).Select(x => x.ContributonSheetHeaderId).FirstOrDefault();
          ContributonSheetDetails details = db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == fhId && x.ContributorID == entity.ContributorID && x.IsActive == true).FirstOrDefault();
          if (details != null)
          {
            details.IsActive = false;
            db.Entry(details).State = EntityState.Modified;
            db.SaveChanges();
          }
        }
      }
      return Json(result, JsonRequestBehavior.AllowGet);
    }
    public ActionResult EditContributorUploadedDetails(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ExcelFileViewModel Verify = (from a in db.ContributonSheetDetails
                                   join b in db.ContributonSheetHeader on a.ContributonSheetHeaderID equals b.Id
                                   where (a.IsActive == true && a.Id == id)
                                   select new ExcelFileViewModel
                                   {
                                     ContributorID = a.ContributorID,
                                     SalaryAmount = a.SalaryAmount,
                                     SystemSalaryAmount = (Math.Round((a.Contributor.SalaryAmount / 12), 2)),
                                     //SystemContributorContribution = Math.Round(((a.Contributor.SalaryAmount / 1200) * (a.Contributor.PFRate)), 2),
                                     //SystemEmployerContribution = Math.Round(((a.Contributor.SalaryAmount / 1200) * (a.Contributor.Employer.PFRate)), 2),
                                     SystemContributorContribution = Math.Round(((a.Contributor.SalaryAmount / 1200) * ((decimal)a.EmplPfRate)), 2),
                                     SystemEmployerContribution = Math.Round(((a.Contributor.SalaryAmount / 1200) * ((decimal)a.EmplrPfRate)), 2),
                                     ContributorContribution = a.ContributorContribution,
                                     EmployerContribution = a.EmployerContribution,
                                     SourceID = a.SourceID,
                                     Month = b.Month,
                                     Year = b.Year,
                                     Id = a.Id,
                                     EmployerID = b.EmployerId,
                                     EmployerName = a.Contributor.Employer.EmployerName,
                                     ContributorName = a.Contributor.FirstName + " " + a.Contributor.MidName + " " + a.Contributor.LastName,
                                     Final = b.Finalized.ToString(),
                                     EmplPFRateId = (int)a.EmplPFRateId,
                                     EmplrPFRateId = (int)a.EmplrPFRateId,
                                     EmplPFRate = (decimal)a.EmplPfRate,
                                     EmplrPFRate = (decimal)a.EmplrPfRate
                                   }).FirstOrDefault();
      if (Verify == null)
      {
        return HttpNotFound();
      }
      ViewBag.Source = new SelectList(db.MasterSource.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", Verify.SourceID);
      return View(Verify);
    }
    [HttpPost]
    public ActionResult EditContributorUploadedDetails(ExcelFileViewModel model)
    {
      if (model != null)
      {
        ContributonSheetDetails details = db.ContributonSheetDetails.Find(model.Id);
        details.ContributorContribution = model.ContributorContribution == null ? 0 : Convert.ToDecimal(model.ContributorContribution);
        details.EmployerContribution = model.EmployerContribution == null ? 0 : Convert.ToDecimal(model.EmployerContribution);
        details.SalaryAmount = model.SalaryAmount == null ? 0 : Convert.ToDecimal(model.SalaryAmount);
        details.SourceID = model.SourceID;
        db.Entry(details).State = EntityState.Modified;
        int result = db.SaveChanges();
        if (result > 0)
        {
          TempData["success"] = "Record updated Successfully";
          return RedirectToAction("ContributonDetailsList", new { Month = model.Month, Year = model.Year, EmpId = model.EmployerID });
        }
        else
        {
          TempData["error"] = "Please Try Again Later";
        }
      }
      ViewBag.Source = new SelectList(db.MasterSource.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.Source);
      return View(model);
    }
    //get method
    public ActionResult AddContributionDetails()
    {
      List<MasterContributor> Contributor = new List<MasterContributor>();
      ViewBag.PersonID = new SelectList(Contributor, "Id", "FirstName");
      ViewBag.Month = new SelectList(db.MasterMonthName, "Name", "Name");
      ViewBag.EmployerId = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName");
      ViewBag.Source = new SelectList(db.MasterSource.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
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
      return View();
    }

    //save result of AddContributionDetails
    public async Task<JsonResult> SaveAddContributionDetails(string[] data)
    {
      string[] tempValue;
      var results = 0;
      tempValue = data[0].Split(',');
      var month = tempValue[5];

      //int monthInDigit = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;
      int year = Convert.ToInt32(tempValue[6]);
      //DateTime date = Convert.ToDateTime(monthInDigit + "/" + DateTime.Now.Day + "/" + year);
      //DateTime date = new DateTime(monthInDigit, DateTime.Now.Day, year, 0, 0, 0);
      int personId = Convert.ToInt32(tempValue[1]);
      int employerId = Convert.ToInt32(tempValue[0]);
      int source = Convert.ToInt32(tempValue[7]);
      var result = db.ContributonSheetHeader.Where(x => x.IsActive == true && x.Month == month && x.Year == year && x.EmployerId == employerId).FirstOrDefault();
      if (result != null)
      {
        var model = db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == result.Id && x.IsActive == true && x.SourceID == source && x.ContributorID == personId).FirstOrDefault();
        if (model != null)
        {
          model.SalaryAmount = string.IsNullOrWhiteSpace(tempValue[2]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[2]);
          model.EmployerContribution = string.IsNullOrWhiteSpace(tempValue[3]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[3]);
          model.ContributorContribution = string.IsNullOrWhiteSpace(tempValue[4]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[4]);
          db.Entry(model).State = EntityState.Modified;
          results = await db.SaveChangesAsync();
          if (results > 0)
          {
            var notActive = db.MasterContributor.Where(x => x.Id == model.ContributorID).Select(x => x.JobStatusID).FirstOrDefault();
            if (notActive != null && notActive == (int)jobStatus.Pensioner)
              TempData["success"] = "Record Saved Successfully,Please Recalculate Pension";
            else
              TempData["success"] = "Record Updated Successfully";
          }
        }

      }

      return Json(results, JsonRequestBehavior.AllowGet);
    }



    public async Task<JsonResult> OverwritedetailsAjax(string[] data)
    {
      var results = 0;
      string[] tempValue;
      tempValue = data[0].Split(',');
      var month = tempValue[5];
      //var year = int.Parse(tempValue[6]);
      int source = Convert.ToInt32(tempValue[7]);
      int personId = Convert.ToInt32(tempValue[1]);
      int epfrateid = Convert.ToInt32(tempValue[8]);
      int cpfrateid = Convert.ToInt32(tempValue[9]);
      int monthInDigit = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;
      int year = Convert.ToInt32(tempValue[6]);
      if (db.MasterContributor.Where(x => x.Id == personId && (x.JobStatusID == (int)jobStatus.Pensioner || x.JobStatusID == (int)jobStatus.ActiveContributor)).Any())
      {
        int employerId = Convert.ToInt32(tempValue[0]);

        int monthEndDate = DateTime.DaysInMonth(year, monthInDigit);
        DateTime date = Convert.ToDateTime(monthInDigit + "/" + monthEndDate + "/" + year);
        if (year == DateTime.Now.Year && monthInDigit == DateTime.Now.Month)
        {
          date = DateTime.Now;
        }

        //get pfrate of employer and contriburor
        var EmployerPfRate = db.MasterPFRate.Where(x => x.IsActive && x.Id == epfrateid).FirstOrDefault();
        var ContributorPfRate = db.MasterPFRate.Where(x => x.IsActive && x.Id == cpfrateid).FirstOrDefault();


        List<ExcelFileViewModel> Verify = new List<ExcelFileViewModel>();
        List<ExcelFileViewModel> tempVerify = new List<ExcelFileViewModel>();
        List<ExcelFileViewModel> temp2Verify = new List<ExcelFileViewModel>();
        var header = (from sheetHeader in db.ContributonSheetHeader.Where(x => x.IsActive == true) join sheetHeaderFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true) on sheetHeader.Id equals sheetHeaderFinalize.ContributonSheetHeaderId into rt from finalize in rt.DefaultIfEmpty() where sheetHeader.Month == month && sheetHeader.Year == year && sheetHeader.EmployerId == employerId select new { headerId = sheetHeader == null ? 0 : sheetHeader.Id, headerFinalize = finalize == null ? 0 : finalize.Id }).FirstOrDefault();
        if (header == null)
        {
          var myAnonInstance = new
          {
            headerId = 0,
            headerFinalize = 0,
          };

          header = myAnonInstance;
        }

        if (header.headerId == 0)
        {
          var contributonSheetHeader = new ContributonSheetHeader();
          contributonSheetHeader.EmployerId = employerId;
          contributonSheetHeader.Month = tempValue[5];
          contributonSheetHeader.Year = Convert.ToInt32(tempValue[6]);
          contributonSheetHeader.IsActive = true;
          contributonSheetHeader.Succeeded = 1;
          contributonSheetHeader.Finalized = 0;
          db.Entry(contributonSheetHeader).State = EntityState.Added;
          await db.SaveChangesAsync();
          var contributonSheetDetails = new ContributonSheetDetails();
          contributonSheetDetails.ContributonSheetHeaderID = contributonSheetHeader.Id;
          contributonSheetDetails.ContributorID = personId;
          contributonSheetDetails.SalaryAmount = string.IsNullOrWhiteSpace(tempValue[2]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[2]);
          contributonSheetDetails.EmployerContribution = string.IsNullOrWhiteSpace(tempValue[3]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[3]);
          contributonSheetDetails.ContributorContribution = string.IsNullOrWhiteSpace(tempValue[4]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[4]);
          contributonSheetDetails.IsActive = true;
          contributonSheetDetails.SourceID = source;
          //contributonSheetDetails.EntryDate = DateTime.Now;
          contributonSheetDetails.EntryDate = date;

          //set month and year
          contributonSheetDetails.Month = monthInDigit;
          contributonSheetDetails.Year = year;

          //employer pf 
          contributonSheetDetails.EmplrPFRateId = EmployerPfRate.Id;
          contributonSheetDetails.EmplrPfRate = EmployerPfRate.PFRate;
          contributonSheetDetails.EmplrEffectiveStartDate = EmployerPfRate.EffectiveDate;
          contributonSheetDetails.EmplrEffectiveEndDate = EmployerPfRate.EffectiveEndDate;
          //employee pf
          contributonSheetDetails.EmplPFRateId = ContributorPfRate.Id;
          contributonSheetDetails.EmplPfRate = ContributorPfRate.PFRate;
          contributonSheetDetails.EmplEffectiveStartDate = ContributorPfRate.EffectiveDate;
          contributonSheetDetails.EmplEffectiveEndDate = ContributorPfRate.EffectiveEndDate;

          db.Entry(contributonSheetDetails).State = EntityState.Added;
          results = await db.SaveChangesAsync();
        }
        else if (header.headerFinalize == 0)
        {
          var entity = db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == header.headerId && x.SourceID == source && x.ContributorID == personId && x.IsActive == true).FirstOrDefault();
          if (entity != null)
          {
            results = 0;
          }
          else
          {
            var contributonSheetDetails = new ContributonSheetDetails();
            contributonSheetDetails.ContributonSheetHeaderID = header.headerId;
            contributonSheetDetails.ContributorID = personId;
            contributonSheetDetails.SalaryAmount = string.IsNullOrWhiteSpace(tempValue[2]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[2]);
            contributonSheetDetails.EmployerContribution = string.IsNullOrWhiteSpace(tempValue[3]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[3]);
            contributonSheetDetails.ContributorContribution = string.IsNullOrWhiteSpace(tempValue[4]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[4]);
            contributonSheetDetails.IsActive = true;
            contributonSheetDetails.SourceID = source;
            contributonSheetDetails.EntryDate = date;

            //set month and year
            contributonSheetDetails.Month = monthInDigit;
            contributonSheetDetails.Year = year;

            //employer pf 
            contributonSheetDetails.EmplrPFRateId = EmployerPfRate.Id;
            contributonSheetDetails.EmplrPfRate = EmployerPfRate.PFRate;
            contributonSheetDetails.EmplrEffectiveStartDate = EmployerPfRate.EffectiveDate;
            contributonSheetDetails.EmplrEffectiveEndDate = EmployerPfRate.EffectiveEndDate;
            //employee pf
            contributonSheetDetails.EmplPFRateId = ContributorPfRate.Id;
            contributonSheetDetails.EmplPfRate = ContributorPfRate.PFRate;
            contributonSheetDetails.EmplEffectiveStartDate = ContributorPfRate.EffectiveDate;
            contributonSheetDetails.EmplEffectiveEndDate = ContributorPfRate.EffectiveEndDate;

            db.Entry(contributonSheetDetails).State = EntityState.Added;
            results = await db.SaveChangesAsync();
          }

        }
        else
        {
          tempVerify = (from details in db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == header.headerId && x.IsActive == true)
                        where !db.ContributonSheetDetailsFinalise.Any(x => x.ContributonSheetHeaderFinaliseID == header.headerFinalize && x.ContributorID == details.ContributorID && x.SourceID == details.SourceID && x.IsActive == true)
                        select new ExcelFileViewModel
                        {
                          Contributor = details.Contributor,
                          Year = 0,
                          Source = details.Source,
                          Final = "0",
                          Id = details.Id,
                        }).ToList();


          temp2Verify = (from details in db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == header.headerId && x.IsActive == true)
                         where db.ContributonSheetDetailsFinalise.Any(x => x.ContributonSheetHeaderFinaliseID == header.headerFinalize && x.ContributorID == details.ContributorID && x.SourceID == details.SourceID && x.IsActive == true)
                         select new ExcelFileViewModel
                         {
                           Contributor = details.Contributor,
                           Year = 0,
                           Source = details.Source,
                           Final = "1",
                           Id = details.Id,
                         }).ToList();
          Verify = tempVerify.Union(temp2Verify).ToList();
          if (Verify.Count > 0)
          {
            var hdetails = Verify.Where(x => x.Contributor.Id == personId && x.Source.Id == source).FirstOrDefault();
            if (hdetails != null)
            {
              if (hdetails.Final == "1")
              {
                results = 2;
              }
              else
              {
                results = 0;
              }
            }
            else
            {
              var contributonSheetDetails = new ContributonSheetDetails();
              contributonSheetDetails.ContributonSheetHeaderID = header.headerId;
              contributonSheetDetails.ContributorID = personId;
              contributonSheetDetails.SalaryAmount = string.IsNullOrWhiteSpace(tempValue[2]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[2]);
              contributonSheetDetails.EmployerContribution = string.IsNullOrWhiteSpace(tempValue[3]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[3]);
              contributonSheetDetails.ContributorContribution = string.IsNullOrWhiteSpace(tempValue[4]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[4]);
              contributonSheetDetails.IsActive = true;
              contributonSheetDetails.SourceID = source;
              contributonSheetDetails.EntryDate = date;

              //set month and year
              contributonSheetDetails.Month = monthInDigit;
              contributonSheetDetails.Year = year;
              //employer pf 
              contributonSheetDetails.EmplrPFRateId = EmployerPfRate.Id;
              contributonSheetDetails.EmplrPfRate = EmployerPfRate.PFRate;
              contributonSheetDetails.EmplrEffectiveStartDate = EmployerPfRate.EffectiveDate;
              contributonSheetDetails.EmplrEffectiveEndDate = EmployerPfRate.EffectiveEndDate;
              //employee pf
              contributonSheetDetails.EmplPFRateId = ContributorPfRate.Id;
              contributonSheetDetails.EmplPfRate = ContributorPfRate.PFRate;
              contributonSheetDetails.EmplEffectiveStartDate = ContributorPfRate.EffectiveDate;
              contributonSheetDetails.EmplEffectiveEndDate = ContributorPfRate.EffectiveEndDate;

              db.Entry(contributonSheetDetails).State = EntityState.Added;
              results = await db.SaveChangesAsync();
            }
          }
          else
          {
            var contributonSheetDetails = new ContributonSheetDetails();
            contributonSheetDetails.ContributonSheetHeaderID = header.headerId;
            contributonSheetDetails.ContributorID = personId;
            contributonSheetDetails.SalaryAmount = string.IsNullOrWhiteSpace(tempValue[2]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[2]);
            contributonSheetDetails.EmployerContribution = string.IsNullOrWhiteSpace(tempValue[3]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[3]);
            contributonSheetDetails.ContributorContribution = string.IsNullOrWhiteSpace(tempValue[4]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[4]);
            contributonSheetDetails.IsActive = true;
            contributonSheetDetails.SourceID = source;
            contributonSheetDetails.EntryDate = date;

            //set month and year
            contributonSheetDetails.Month = monthInDigit;
            contributonSheetDetails.Year = year;

            //employer pf 
            contributonSheetDetails.EmplrPFRateId = EmployerPfRate.Id;
            contributonSheetDetails.EmplrPfRate = EmployerPfRate.PFRate;
            contributonSheetDetails.EmplrEffectiveStartDate = EmployerPfRate.EffectiveDate;
            contributonSheetDetails.EmplrEffectiveEndDate = EmployerPfRate.EffectiveEndDate;
            //employee pf
            contributonSheetDetails.EmplPFRateId = ContributorPfRate.Id;
            contributonSheetDetails.EmplPfRate = ContributorPfRate.PFRate;
            contributonSheetDetails.EmplEffectiveStartDate = ContributorPfRate.EffectiveDate;
            contributonSheetDetails.EmplEffectiveEndDate = ContributorPfRate.EffectiveEndDate;

            db.Entry(contributonSheetDetails).State = EntityState.Added;
            results = await db.SaveChangesAsync();
          }

        }
        if (results == 1)
        {
          var notActive = db.MasterContributor.Where(x => x.Id == personId).Select(x => x.JobStatusID).FirstOrDefault();
          if (notActive != null && notActive == (int)jobStatus.Pensioner)
            TempData["success"] = "Record Saved Successfully,Please Recalculate Pension";
          else
            TempData["success"] = "Record Saved Successfully";
        }
      }
      else
      {
        results = 4;
      }
      return Json(results, JsonRequestBehavior.AllowGet);
    }
















    //if duplicate record in AddContributorDetails
    //public async Task<JsonResult> OverwritedetailsAjax(string[] data)
    //{
    //  var results = 0;
    //  string[] tempValue;
    //  for (int i = 0; i < data.Length; i++)
    //  {
    //    tempValue = data[i].Split(',');
    //    var month = tempValue[5];
    //    var year = int.Parse(tempValue[6]);
    //    int source = Convert.ToInt32(tempValue[7]);
    //    int personId = Convert.ToInt32(tempValue[1]);
    //    int employerId = Convert.ToInt32(tempValue[0]);
    //    var result = await (from a in db.ContributonSheetDetails
    //                        join b in db.ContributonSheetHeader on a.ContributonSheetHeaderID equals b.Id
    //                        where (b.IsActive == true && b.Month == month && b.Year == year && b.EmployerId == employerId && a.ContributorID == personId && a.IsActive == true && a.SourceID == source)
    //                        select new { a, b }).FirstOrDefaultAsync();
    //    //var model = await db.ContributonSheetHeader.Where(x => x.Id == result.ContributonSheetHeaderID).FirstOrDefaultAsync();
    //    //model.IsActive = false;
    //    //db.Entry(model).State = EntityState.Modified;
    //    if (result != null)
    //    {
    //      var entity = await db.ContributonSheetDetails.Where(x => x.IsActive == true && x.ContributonSheetHeaderID == result.a.ContributonSheetHeaderID && x.ContributorID == result.a.ContributorID).FirstOrDefaultAsync();
    //      //entity.ContributorID = personId;
    //      entity.SalaryAmount = string.IsNullOrWhiteSpace(tempValue[2]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[2]);
    //      entity.EmployerContribution = string.IsNullOrWhiteSpace(tempValue[3]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[3]);
    //      entity.ContributorContribution = string.IsNullOrWhiteSpace(tempValue[4]) ? Convert.ToDecimal(0) : Convert.ToDecimal(tempValue[4]);
    //      entity.SourceID = source;
    //      entity.IsActive = true;
    //      //entity.EntryDate = DateTime.Now;
    //      db.Entry(entity).State = EntityState.Modified;
    //      results = await db.SaveChangesAsync();
    //    }
    //  }

    //  return Json(results, JsonRequestBehavior.AllowGet);
    //}
    //get method of contributor Details List
    [HttpGet]
    public ActionResult ContributonDetailsList(string Month, int? Year, int? EmpId)
    {
      ViewBag.EmployerId = new SelectList(db.MasterEmployer.Where(x => x.IsActive == true).OrderBy(x => x.EmployerName), "Id", "EmployerName", EmpId);
      ViewBag.Month = new SelectList(db.MasterMonthName.Where(x => x.IsActive == true), "Name", "Name", Month);
      int year = DateTime.Now.Year - 20;
      List<YearModel> yearModel = new List<YearModel>();
      for (int i = year; i <= DateTime.Now.Year; i++)
      {
        YearModel tempYear = new YearModel();
        tempYear.Id = i;
        tempYear.Name = i.ToString();
        yearModel.Add(tempYear);
      }
      ViewBag.Year = new SelectList(yearModel.OrderByDescending(x => x.Id), "Id", "Name", Year);



      return View();
    }

    //list on contributor Details List
    public async Task<ActionResult> ContributorAjaxHandler(JQueryDataTableParamModel param, string Month, string Year, string EmployeeId)
    {
      var tempId = (from sheetHeader in db.ContributonSheetHeader.Where(x => x.IsActive == true) join sheetHeaderFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true) on sheetHeader.Id equals sheetHeaderFinalize.ContributonSheetHeaderId into rt from finalize in rt.DefaultIfEmpty() where sheetHeader.Month == Month && sheetHeader.Year.ToString() == Year && sheetHeader.EmployerId.ToString() == EmployeeId select new { Id = sheetHeader == null ? 0 : sheetHeader.Id, finalizedId = finalize == null ? 0 : finalize.Id }).FirstOrDefault();
      if (tempId == null)
      {
        var myAnonInstance = new
        {
          Id = 0,
          finalizedId = 0,
        };

        tempId = myAnonInstance;
      }
      //var mastercontributor = db.MasterContributor.Where(x => x.EmployerID.ToString() == EmployeeId);
      //var masteremployer = db.MasterEmployer.Where(x => x.Id.ToString() == EmployeeId);
      //var tempId = (from header in db.ContributonSheetHeader.Where(h => h.Month == Month && h.Year.ToString() == Year && h.EmployerId.ToString() == EmployeeId && h.IsActive == true)
      //              join finalizedheader in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true) on header.Id equals finalizedheader.ContributonSheetHeaderId into rt
      //              from detail in rt.DefaultIfEmpty()
      //              select new { Id = header.Id, finalizedId = detail == null ? 0 : detail.Id }).FirstOrDefault();
      List<ExcelFileViewModel> Verify = new List<ExcelFileViewModel>();
      if (tempId != null && tempId.Id != 0)
      {
        using (AppDbContext dbContext = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
        {
          Verify = await (from details in _DbContext.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == tempId.Id && x.IsActive == true)
                          where !_DbContext.ContributonSheetDetailsFinalise.Any(x => x.ContributonSheetHeaderFinaliseID == tempId.finalizedId && x.ContributorID == details.ContributorID && x.SourceID == details.SourceID && x.IsActive == true)
                          select new ExcelFileViewModel
                          {
                            Contributor = details.Contributor,
                            SalaryAmount = details.SalaryAmount,
                            ContributorContribution = details.ContributorContribution,
                            EmployerContribution = details.EmployerContribution,
                            Employer = details.Contributor.Employer,
                            Year = 0,
                            Source = details.Source,
                            //ContributorName = details == null ? "" : (details.Contributor.FirstName + " " + details.Contributor.MidName + " " + details.Contributor.LastName),

                            Id = details.Id,
                            EmplPFRate = details.EmplPfRate == null ? 0 : (decimal)details.EmplPfRate,
                            EmplrPFRate = details.EmplrPfRate == null ? 0 : (decimal)details.EmplrPfRate,
                          }).ToListAsync();

        }
      }

      IEnumerable<ExcelFileViewModel> filtered;
      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = Verify.Where(c => c.Contributor.PersonID.ToLower().Contains(param.sSearch.ToLower())
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
        filtered = Verify;
      }
      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<ExcelFileViewModel, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Contributor.PersonID.ToString() :
                                                                  sortColumnIndex == 1 ? c.Contributor.FirstName :
                                                                   sortColumnIndex == 2 ? c.Source.Name :
                                                                  sortColumnIndex == 3 ? c.Contributor.SalaryAmount.ToString() :
                                                                   //sortColumnIndex == 4 ? c.Contributor == null ? "N/A" : Math.Round((c.Contributor.SalaryAmount / 1200) * (c.Contributor.PFRate), 2, MidpointRounding.AwayFromZero).ToString("#,##0.00") :
                                                                   //sortColumnIndex == 5 ? c.Contributor == null ? "N/A" : Math.Round((c.Contributor.SalaryAmount / 1200) * (c.Employer.PFRate), 2, MidpointRounding.AwayFromZero).ToString("#,##0.00") :
                                                                   sortColumnIndex == 4 ? c.Contributor == null ? "N/A" : Math.Round((c.Contributor.SalaryAmount / 1200) * (c.EmplPFRate), 2, MidpointRounding.AwayFromZero).ToString("#,##0.00") :
                                                                  sortColumnIndex == 5 ? c.Contributor == null ? "N/A" : Math.Round((c.Contributor.SalaryAmount / 1200) * (c.EmplrPFRate), 2, MidpointRounding.AwayFromZero).ToString("#,##0.00") :
                                                                  sortColumnIndex == 6 ? Convert.ToDecimal(c.SalaryAmount).ToString("#,##0.00") :
                                                                  sortColumnIndex == 7 ? Convert.ToDecimal(c.ContributorContribution).ToString("#,##0.00") :
                                                                   sortColumnIndex == 8 ? Convert.ToDecimal(c.EmployerContribution).ToString("#,##0.00") :
                                                                                      "");

      var sortDirection = Request["sSortDir_0"]; // asc or desc
      if (sortDirection == "asc")
        filtered = filtered.OrderBy(orderingFunction);
      else
        filtered = filtered.OrderByDescending(orderingFunction);

      //Pagging
      var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

      ////Select required columns
      var result = from c in displayed
                   select new[] {
                         c.Contributor==null?"":c.Contributor.PersonID,
                         c.Contributor==null?"N/A":(c.Contributor.FirstName+" "+c.Contributor.MidName+" "+c.Contributor.LastName),
                         c.Source==null?"":c.Source.Name,
                         c.Contributor==null?"N/A":(c.Contributor.SalaryAmount/12).ToString("#,##0.00"),
                         //c.Contributor==null?"N/A":Math.Round((c.Contributor.SalaryAmount/1200)*(c.Contributor.PFRate),2, MidpointRounding.AwayFromZero).ToString("#,##0.00"),
                         //c.Contributor==null?"N/A":Math.Round((c.Contributor.SalaryAmount/1200)*(c.Employer.PFRate),2, MidpointRounding.AwayFromZero).ToString("#,##0.00"),
                         c.Contributor==null?"N/A":Math.Round((c.Contributor.SalaryAmount/1200)*(c.EmplPFRate),2, MidpointRounding.AwayFromZero).ToString("#,##0.00"),
                         c.Contributor==null?"N/A":Math.Round((c.Contributor.SalaryAmount/1200)*(c.EmplrPFRate),2, MidpointRounding.AwayFromZero).ToString("#,##0.00"),
                         Convert.ToDecimal(c.SalaryAmount).ToString("#,##0.00"),
                      Convert.ToDecimal(c.ContributorContribution).ToString("#,##0.00"),
                      Convert.ToDecimal(c.EmployerContribution).ToString("#,##0.00"),
                    c.Id+""
                   };

      return Json(
                                  new
                                  {
                                    sEcho = param.sEcho,
                                    iTotalRecords = Verify.Count(),
                                    iTotalDisplayRecords = filtered.Count(),
                                    aaData = result
                                  },
    JsonRequestBehavior.AllowGet);
    }


    public ActionResult getTotalContribution(string empId, string month, string year)
    {
      if (empId == null && !string.IsNullOrWhiteSpace(month) && year == null)
      {
        month = String.Format("{0:MMMM}", DateTime.Now);
        year = DateTime.Now.Year.ToString();
        empId = "2";
      }
      var tempId = (from sheetHeader in db.ContributonSheetHeader.Where(x => x.IsActive == true) join sheetHeaderFinalize in db.ContributonSheetHeaderFinalise.Where(x => x.IsActive == true) on sheetHeader.Id equals sheetHeaderFinalize.ContributonSheetHeaderId into rt from finalize in rt.DefaultIfEmpty() where sheetHeader.Month == month && sheetHeader.Year.ToString() == year.ToString() && sheetHeader.EmployerId.ToString() == empId.ToString() select new { Id = sheetHeader == null ? 0 : sheetHeader.Id, finalizedId = finalize == null ? 0 : finalize.Id }).FirstOrDefault();
      if (tempId == null)
      {
        var myAnonInstance = new
        {
          Id = 0,
          finalizedId = 0,
        };

        tempId = myAnonInstance;
      }

      List<ExcelFileViewModel> Verify = new List<ExcelFileViewModel>();
      if (tempId != null && tempId.Id != 0)
      {
        using (AppDbContext dbContext = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
        {
          Verify = (from details in _DbContext.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == tempId.Id && x.IsActive == true)
                    where !_DbContext.ContributonSheetDetailsFinalise.Any(x => x.ContributonSheetHeaderFinaliseID == tempId.finalizedId && x.ContributorID == details.ContributorID && x.SourceID == details.SourceID && x.IsActive == true)
                    select new ExcelFileViewModel
                    {
                      Contributor = details.Contributor,
                      SalaryAmount = details.SalaryAmount,

                      ContributorContribution = details.ContributorContribution,
                      EmployerContribution = details.EmployerContribution,
                      Employer = details.Contributor.Employer,

                      Year = 0,
                      Source = details.Source,
                      //ContributorName = details == null ? "" : (details.Contributor.FirstName + " " + details.Contributor.MidName + " " + details.Contributor.LastName),

                      Id = details.Id,
                      EmplPFRate = details.EmplPfRate == null ? 0 : (decimal)details.EmplPfRate,
                      EmplrPFRate = details.EmplrPfRate == null ? 0 : (decimal)details.EmplrPfRate,
                    }).ToList();

        }
      }
      foreach (var details in Verify)
      {
        //details.SystemContributorContribution = Convert.ToDecimal(details.Contributor == null ? "N/A" : Math.Round((details.Contributor.SalaryAmount / 1200) * (details.Contributor.PFRate), 2, MidpointRounding.AwayFromZero).ToString("#,##0.00"));
        //details.SystemEmployerContribution = Convert.ToDecimal(details.Contributor == null ? "N/A" : Math.Round((details.Contributor.SalaryAmount / 1200) * (details.Employer.PFRate), 2, MidpointRounding.AwayFromZero).ToString("#,##0.00"));
        details.SystemContributorContribution = Convert.ToDecimal(details.Contributor == null ? "N/A" : Math.Round((details.Contributor.SalaryAmount / 1200) * (details.EmplPFRate), 2, MidpointRounding.AwayFromZero).ToString("#,##0.00"));
        details.SystemEmployerContribution = Convert.ToDecimal(details.Contributor == null ? "N/A" : Math.Round((details.Contributor.SalaryAmount / 1200) * (details.EmplrPFRate), 2, MidpointRounding.AwayFromZero).ToString("#,##0.00"));
      }
      string EmployerTotalContribution = "EC$ " + Convert.ToDecimal(Verify.Sum(x => x.EmployerContribution)).ToString("#,##0.00");
      string ContributorTotalContribution = "EC$ " + Convert.ToDecimal(Verify.Sum(x => x.ContributorContribution)).ToString("#,##0.00");

      string SystemEmployerTotalContribution = "EC$ " + Convert.ToDecimal(Verify.Sum(x => x.SystemEmployerContribution)).ToString("#,##0.00");
      string SystemContributorTotalContribution = "EC$ " + Convert.ToDecimal(Verify.Sum(x => x.SystemContributorContribution)).ToString("#,##0.00");
      return Json(EmployerTotalContribution + "#" + ContributorTotalContribution + "#" + SystemEmployerTotalContribution + "#" + SystemContributorTotalContribution, JsonRequestBehavior.AllowGet);
    }

    //public JsonResult DependantTerminationDateIndex(String PersonId)
    //{
    //  var TerminationAtAge = (from employer in db.MasterEmployer
    //                          join contributor in db.MasterContributor on employer.UniqueID equals contributor.Employer.UniqueID
    //                          where (employer.IsActive == true && contributor.IsActive == true && contributor.PersonID == PersonId)
    //                          select new { TerminationAtAge = employer.EmployerTypeID == 1 ? 19 : 18 }).FirstOrDefault();
    //  return Json(TerminationAtAge, JsonRequestBehavior.AllowGet);
    //}

    //JobDetails
    public JsonResult CalculateContributionAjax(string PersonID, string SalaryAmount)
    {
      decimal monthSal = Math.Round((Convert.ToDecimal(SalaryAmount) / 12), 2, MidpointRounding.AwayFromZero);
      //var contributor = db.MasterContributor.Where(x => x.PersonID == PersonID).Select(x => new { x.PFRate ,x.EmployerID}).FirstOrDefault();
      //var employer = db.MasterEmployer.Where(x => x.Id == contributor.EmployerID).Select(x => new { x.PFRate}).FirstOrDefault();
      var result = (from a in db.MasterContributor
                    join b in db.MasterEmployer on a.EmployerID equals b.Id
                    where (a.PersonID == PersonID)
                    select new
                    {
                      monthSalary = monthSal,
                      employerContribution = Math.Round(((monthSal * b.PFRate) / 100), 2),
                      contributionContribution = Math.Round(((monthSal * a.PFRate) / 100), 2)
                    }).FirstOrDefault();


      return Json(result, JsonRequestBehavior.AllowGet);
    }
    public JsonResult SocialSecurityNoAjax(string Number, string personID, string oldPersonID)
    {
      var result = db.MasterContributor.Where(x => x.SocialSecurityNo == Number && x.PersonID != personID && x.PersonID != oldPersonID).Count();
      return Json(result, JsonRequestBehavior.AllowGet);
    }

    //public JsonResult SocialSecurityNoAjax(SocialSecurityViewModel Data)
    //{
    //    int result = 0;
    //    var date = Convert.ToDateTime(Data.DateOfBirth);
    //    if (db.MasterContributor.Where(x => x.FirstName == Data.firstName && x.LastName == Data.lastName && x.SocialSecurityNo == Data.number && DbFunctions.TruncateTime(x.DateOfBirth.Value) == DbFunctions.TruncateTime(date)).Any())
    //        result = 0;
    //    else
    //        result = db.MasterContributor.Where(x => x.SocialSecurityNo == Data.number && x.PersonID != Data.PersonID && x.PersonID != Data.OldPersonID).Count();
    //    return Json(result, JsonRequestBehavior.AllowGet);
    //}
    //AddPersonalDetails
    public JsonResult EmailAjax(string Email, string personID, string oldPersonID)
    {
      var result = db.MasterContributor.Where(x => x.Email == Email && x.PersonID != personID && x.PersonID != oldPersonID).Count();
      return Json(result, JsonRequestBehavior.AllowGet);
    }
    //public JsonResult UniqueID()
    //{
    //  var result = db.MasterEmployer.Count();
    //  return Json(result, JsonRequestBehavior.AllowGet);
    //}
    //public JsonResult GetMonth(String Id)
    //{
    //  var result = from a in db.ContributonSheetHeader join b in db.ContributonSheetDetails on a.Id equals b.ContributonSheetHeaderID where (a.IsActive == true) select new { Value = a.Month, Text = a.Month };
    //  return Json(result, JsonRequestBehavior.AllowGet);
    //}

    public ActionResult MarriageAjaxHandler(JQueryDataTableParamModel param, string PersonId)
    {
      var List = db.MasterContributorMarriageDetails.Where(x => x.IsActive == true && x.PersonId == PersonId);
      IEnumerable<MasterContributorMarriageDetails> filtered;


      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => c.PersonId.ToString().ToLower().Contains(param.sSearch.ToLower())
           || (c.SpouseFirstName + " " + c.MidName + " " + c.LastName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
           //|| c.MidName.ToLower().Contains(param.sSearch.ToLower())
           //|| c.LastName.ToLower().Contains(param.sSearch.ToLower())
           || c.DateOfBirth.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.Phone.ToLower().Contains(param.sSearch.ToLower())
           || c.MarriageBeginDate.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.MarriageEndDate.ToString().ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<MasterContributorMarriageDetails, string> orderingFunction = (c => sortColumnIndex == 0 ? c.PersonId.ToString() :
                                                              sortColumnIndex == 1 ? c.SpouseFirstName : sortColumnIndex == 2 ? c.DateOfBirth.ToString() : sortColumnIndex == 3 ? c.Phone :
                                                                                      sortColumnIndex == 4 ? c.MarriageBeginDate.ToString() + "" :
                                                                                        sortColumnIndex == 5 ? c.MarriageBeginDate.ToString() + "" :
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
                         
                         //c.PersonId.ToString(),
                         c.SpouseFirstName+" "+c.MidName+" "+c.LastName+"",
                         String.Format("{0:MM/dd/yyyy}", c.DateOfBirth),
                         c.Phone,
                         String.Format("{0:MM/dd/yyyy}", c.MarriageBeginDate),
                           c.MarriageEndDate  == null ? "N/A":   String.Format("{0:MM/dd/yyyy}", c.MarriageEndDate),
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

    public ActionResult DepandantAjaxHandler(JQueryDataTableParamModel param, string PersonId)
    {
      var List = db.MasterDependantDetails.Where(x => x.IsActive == true && x.PersonID == PersonId);
      IEnumerable<MasterDependantDetails> filtered;


      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => c.Relationship.Name.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.PersonID.ToString().ToLower().Contains(param.sSearch.ToLower())
           || (c.FirstName + " " + c.MidName + " " + c.LastName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
           //|| c.MidName.ToLower().Contains(param.sSearch.ToLower())
           //|| c.LastName.ToLower().Contains(param.sSearch.ToLower())
           || c.Gender.ToLower().Contains(param.sSearch.ToLower())
           || c.DateOfBirth.ToString().ToLower().Contains(param.sSearch.ToLower())
           || c.TerminationDate.ToString().ToLower().Contains(param.sSearch.ToLower())
          );

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<MasterDependantDetails, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Relationship.Name :
                                                              sortColumnIndex == 1 ? c.PersonID.ToString() :
                                                              sortColumnIndex == 2 ? c.FirstName :
                                                              sortColumnIndex == 3 ? c.MidName :
                                                              sortColumnIndex == 4 ? c.LastName :
                                                              sortColumnIndex == 5 ? c.Gender :
                                                              sortColumnIndex == 6 ? c.DateOfBirth.ToString() :
                                                              sortColumnIndex == 7 ? c.TerminationDate + "" :
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
                         c.Relationship.Name,
                         //c.PersonId.ToString(),
                         c.FirstName,
                         c.MidName,
                         c.LastName,
                         c.Gender,
                         String.Format("{0:MM/dd/yyyy}", c.DateOfBirth),
                         String.Format("{0:MM/dd/yyyy}", c.TerminationDate),
                     //    c.DateOfBirth.Value.ToShortDateString(),
                     //c.TerminationDate.Value.ToShortDateString(),
                     c.IsActive+"",
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

    //public ActionResult JobAjaxHandler(JQueryDataTableParamModel param, string PersonId)
    //{
    //  var List = db.MasterContributorJobDetails.Where(x => x.IsActive == true && x.PersonID == PersonId);
    //  IEnumerable<MasterContributorJobDetails> filtered;


    //  if (!string.IsNullOrEmpty(param.sSearch))
    //  {
    //    filtered = List
    //       .Where(c => c.Department.Name.ToLower().Contains(param.sSearch.ToLower())
    //         || c.Designation.Name.ToLower().Contains(param.sSearch.ToLower())
    //         || c.Employer.EmployerName.ToLower().Contains(param.sSearch.ToLower())
    //       || c.Grade.Name.ToLower().Contains(param.sSearch.ToLower())
    //       || c.EmployerType.ToString().ToLower().Contains(param.sSearch.ToLower())
    //       || c.JobTitle.Name.ToLower().Contains(param.sSearch.ToLower())
    //       || c.HireDate.ToString().ToLower().Contains(param.sSearch.ToLower())
    //       || c.PresentSalary.ToString().ToLower().Contains(param.sSearch.ToLower())
    //       || c.JobDescription.ToLower().Contains(param.sSearch.ToLower())
    //      );

    //  }
    //  else
    //  {
    //    filtered = List;
    //  }

    //  //Sorting through column index
    //  var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

    //  Func<MasterContributorJobDetails, string> orderingFunction = (c =>
    //                                                          sortColumnIndex == 0 ? c.Department.Name :
    //                                                          sortColumnIndex == 1 ? c.Designation.Name :
    //                                                          sortColumnIndex == 2 ? c.Employer.EmployerName :
    //                                                            //sortColumnIndex == 3 ? c.Grade.Name :
    //                                                          sortColumnIndex == 4 ? c.EmployerType.ToString() :
    //                                                            //sortColumnIndex == 5 ? c.JobTitle.Name :
    //                                                          sortColumnIndex == 6 ? c.HireDate.ToString() :
    //                                                          sortColumnIndex == 7 ? c.PresentSalary.ToString() :
    //                                                            //sortColumnIndex == 8 ? c.JobDescription :
    //                                                                                  "");

    //  var sortDirection = Request["sSortDir_0"]; // asc or desc
    //  if (sortDirection == "asc")
    //    filtered = filtered.OrderBy(orderingFunction);
    //  else
    //    filtered = filtered.OrderByDescending(orderingFunction);

    //  //Pagging
    //  var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

    //  //Select required columns
    //  var result = from c in displayed
    //               select new[] { 
    //                     c.Employer.EmployerName,
    //                     c.Department.Name,
    //                     c.Designation.Name,
    //                     //c.Grade.Name,
    //                     c.EmployerType.ToString(),
    //                     //c.JobTitle.Name,
    //                     String.Format("{0:MM/dd/yyyy}", c.HireDate),
    //                     //c.HireDate.ToShortDateString(),
    //                     c.PresentSalary.ToString(),
    //                     //c.JobDescription,
    //                 c.IsActive+"",
    //                 c.Id + ""
    //               };

    //  return Json(
    //                              new
    //                              {
    //                                sEcho = param.sEcho,
    //                                iTotalRecords = List.Count(),
    //                                iTotalDisplayRecords = filtered.Count(),
    //                                aaData = result
    //                              }, JsonRequestBehavior.AllowGet);
    //}


    //used in AddEmployeeDetails View to bind employer with contributor
    public JsonResult GetContributorAjax(int EmpId)
    {
      var result = db.MasterContributor.Where(x => x.Employer.Id == EmpId && x.IsActive == true).Select(x => new { x.Id, Name = x.FirstName + " " + x.MidName + " " + x.LastName + " ( " + x.PersonID + " )" });
      return Json(result, JsonRequestBehavior.AllowGet);
    }

    public string DecimalFormat(decimal value)
    {
      //Math.Round(((c.Contributor.SalaryAmount / 1200) * c.Contributor.PFRate), 2, MidpointRounding.AwayFromZero).ToString("#,##0.00")
      string result = Math.Round(value, 2, MidpointRounding.AwayFromZero).ToString("#,##0.00");
      return result;
    }
    public class ContributorDetails
    {
      public string salary { get; set; }
      public string cpf { get; set; }
      public string epf { get; set; }
      public string date { get; set; }
      public bool status { get; set; }
      public string message { get; set; }
      public int epfrateid { get; set; }
      public int cpfrateid { get; set; }
      public decimal CpfRate { get; set; }
      public decimal EpfRate { get; set; }

    }
    //used in AddEmployeeDetails View for salary and others info
    public JsonResult GetContributorDetailsAjax(int CId, int EId, string month, int? year1)
    {
      var data = new ContributorDetails();
      try
      {
        decimal CpfRate = 0;
        decimal EpfRate = 0;
        int epfrateid = 0;
        int cpfrateid = 0;

        int monthInDigit = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;
        int year = Convert.ToInt32(year1);
        //int monthEndDate = DateTime.DaysInMonth(year, monthInDigit);
        DateTime ContributionDate = Convert.ToDateTime(monthInDigit + "/" + 1 + "/" + year);

        var result = (from a in db.MasterContributor
                      join b in db.MasterEmployer on a.EmployerID equals b.Id
                      where (a.Id == CId && b.Id == EId)
                      select new { sal = a.SalaryAmount, cpfrateid = a.PFRateID, cpf = a.PFRate, epfrateid = b.PFRateID, epf = b.PFRate, date = a.FirstAppointmentDate, uniqueid = b.UniqueID, personid = a.PersonID }).FirstOrDefault();

        //get employer pf rate from [MasterEmployerPFRateDetails] (employer pf rate history table) on basis of effective date
        var IsAnyHistory = db.MasterEmployerPFRateDetails.Any(x => x.IsActive && x.UniqueID == result.uniqueid);
        if (IsAnyHistory)
        {
          var AllPfRateHistoryEmployer = db.MasterEmployerPFRateDetails.Where(x => x.IsActive && x.EffectiveDate <= ContributionDate && x.EffectiveEndDate >= ContributionDate && x.UniqueID == result.uniqueid).OrderByDescending(x => x.EffectiveDate).FirstOrDefault();
          if (AllPfRateHistoryEmployer != null)
          {
            var RateToBeAppliedOnEmployer = db.MasterEmployerPFRateDetails.Where(x => x.IsActive && x.EffectiveDate == AllPfRateHistoryEmployer.EffectiveDate && x.UniqueID == result.uniqueid).FirstOrDefault();
            EpfRate = RateToBeAppliedOnEmployer.PFRate;
            epfrateid = RateToBeAppliedOnEmployer.PFRateID;
          }
          else
          {
            data = new ContributorDetails
            {
              status = false,
              message = "Can't add contribution. Please maintain Employer Pf History for selected period."
            };
            return Json(data, JsonRequestBehavior.AllowGet);
          }
        }
        else
        {
          data = new ContributorDetails
          {
            status = false,
            message = "Can't add contribution. Please maintain Employer Pf History for selected period."
          };
          return Json(data, JsonRequestBehavior.AllowGet);
        }

        //get contributor pf rate from [MasterContributorPFRateDetails] (contributor pf rate history table) on basis of effective date
        var IsAnyHistoryContributor = db.MasterContributorPFRateDetails.Any(x => x.IsActive && x.PersonID == result.personid);
        if (IsAnyHistoryContributor)
        {
          var AllPfRateHistoryContributor = db.MasterContributorPFRateDetails.Where(x => x.IsActive && x.EffectiveDate <= ContributionDate && x.EffectiveEndDate >= ContributionDate && x.PersonID == result.personid).OrderByDescending(x => x.EffectiveDate).FirstOrDefault();
          if (AllPfRateHistoryContributor != null)
          {
            var RateToBeAppliedOnContributor = db.MasterContributorPFRateDetails.Where(x => x.IsActive && x.EffectiveDate == AllPfRateHistoryContributor.EffectiveDate && x.PersonID == result.personid).FirstOrDefault();
            CpfRate = RateToBeAppliedOnContributor.PFRate;
            cpfrateid = RateToBeAppliedOnContributor.PFRateID;
          }
          else
          {
            data = new ContributorDetails
            {
              status = false,
              message = "Can't add contribution. Please maintain Contributor Pf History for selected period."
            };
            return Json(data, JsonRequestBehavior.AllowGet);
          }
        }
        else
        {
          data = new ContributorDetails
          {
            status = false,
            message = "Can't add contribution. Please maintain Contributor Pf History for selected period."
          };
          return Json(data, JsonRequestBehavior.AllowGet);
        }

        //get contributor and employer current pf effective date
        //var EmployerPfRate = db.MasterPFRate.Where(x => x.Id == result.epfrateid).FirstOrDefault();
        //var ContributorPfRate = db.MasterPFRate.Where(x => x.Id == result.cpfrateid).FirstOrDefault();
        //if (ContributionDate >= EmployerPfRate.EffectiveDate)
        //{
        //  EpfRate = EmployerPfRate.PFRate;
        //  epfrateid = EmployerPfRate.Id;
        //}
        //else
        //{
        //  //get employer pf rate from [MasterEmployerPFRateDetails] (employer pf rate history table) on basis of effective date
        //  var IsAnyHistory = db.MasterEmployerPFRateDetails.Any(x => x.IsActive && x.UniqueID == result.uniqueid);
        //  if (IsAnyHistory)
        //  {
        //    var AllPfRateHistoryEmployer = db.MasterEmployerPFRateDetails.Where(x => x.IsActive && x.EffectiveDate < ContributionDate && x.UniqueID == result.uniqueid).OrderByDescending(x => x.EffectiveDate).FirstOrDefault();
        //    if (AllPfRateHistoryEmployer != null)
        //    {
        //      var RateToBeAppliedOnEmployer = db.MasterEmployerPFRateDetails.Where(x => x.IsActive && x.EffectiveDate == AllPfRateHistoryEmployer.EffectiveDate && x.UniqueID == result.uniqueid).FirstOrDefault();
        //      EpfRate = RateToBeAppliedOnEmployer.PFRate;
        //      epfrateid = RateToBeAppliedOnEmployer.PFRateID;
        //    }
        //    else
        //    {
        //      data = new ContributorDetails
        //      {
        //        status = false,
        //        message = "Can't add contribution. Selected Month and Year for contribution is not greater than current effective date of Employer pf as well as there are no history of Employer pf rates."
        //      };
        //      return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //  }
        //  else
        //  {
        //    data = new ContributorDetails
        //    {
        //      status = false,
        //      message = "Can't add contribution. Selected Month and Year for contribution is not greater than current effective date of Employer pf as well as there are no history of Employer pf rates."
        //    };
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //  }
        //}

        //if (ContributionDate >= ContributorPfRate.EffectiveDate)
        //{
        //  CpfRate = ContributorPfRate.PFRate;
        //  cpfrateid = ContributorPfRate.Id;
        //}
        //else
        //{
        //  //get contributor pf rate from [MasterContributorPFRateDetails] (contributor pf rate history table) on basis of effective date
        //  var IsAnyHistory = db.MasterContributorPFRateDetails.Any(x => x.IsActive && x.PersonID == result.personid);
        //  if (IsAnyHistory)
        //  {
        //    var AllPfRateHistoryContributor = db.MasterContributorPFRateDetails.Where(x => x.IsActive && x.EffectiveDate < ContributionDate && x.PersonID == result.personid).OrderByDescending(x => x.EffectiveDate).FirstOrDefault();
        //    if (AllPfRateHistoryContributor != null)
        //    {
        //      var RateToBeAppliedOnContributor = db.MasterContributorPFRateDetails.Where(x => x.IsActive && x.EffectiveDate == AllPfRateHistoryContributor.EffectiveDate && x.PersonID == result.personid).FirstOrDefault();
        //      CpfRate = RateToBeAppliedOnContributor.PFRate;
        //      cpfrateid = RateToBeAppliedOnContributor.PFRateID;
        //    }
        //    else
        //    {
        //      data = new ContributorDetails
        //      {
        //        status = false,
        //        message = "Can't add contribution. Selected Month and Year for contribution is not greater than current effective date of Contributor pf as well as there are no history of Contributor pf rates."
        //      };
        //      return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //  }
        //  else
        //  {
        //    data = new ContributorDetails
        //    {
        //      status = false,
        //      message = "Can't add contribution. Selected Month and Year for contribution is not greater than current effective date of Contributor pf as well as there are no history of Contributor pf rates."
        //    };
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //  }
        //}
        data = new ContributorDetails
        {
          salary = DecimalFormat(result.sal / 12),
          cpf = DecimalFormat((result.sal / 1200) * CpfRate),
          epf = DecimalFormat((result.sal / 1200) * EpfRate),
          date = String.Format("{0:MM/dd/yyyy}", result.date),
          status = true,
          epfrateid = epfrateid,
          cpfrateid = cpfrateid,
          CpfRate = CpfRate,
          EpfRate = EpfRate
        };
        //data = new ContributorDetails
        //{
        //  salary = DecimalFormat(result.sal / 12),
        //  cpf = DecimalFormat((result.sal / 1200) * result.cpf),
        //  epf = DecimalFormat((result.sal / 1200) * result.epf),
        //  date = String.Format("{0:MM/dd/yyyy}", result.date)
        //};
      }
      catch (Exception ex)
      {
        data = new ContributorDetails
        {
          status = false,
          message = "Error : " + ex.Message
        };
      }
      return Json(data, JsonRequestBehavior.AllowGet);
    }
    public JsonResult GetContributorDetailsAfterSalaryAjax(int CId, int EId, string salary, string month, int? year1)

    {
      var data = new ContributorDetails();
      try
      {
        decimal CpfRate = 0;
        decimal EpfRate = 0;
        int epfrateid = 0;
        int cpfrateid = 0;

        int monthInDigit = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;
        int year = Convert.ToInt32(year1);
        DateTime ContributionDate = Convert.ToDateTime(monthInDigit + "/" + 1 + "/" + year);
        decimal salaryc;
        decimal.TryParse(salary.ToString(), out salaryc);
        var result = (from a in db.MasterContributor
                      join b in db.MasterEmployer on a.EmployerID equals b.Id
                      where (a.Id == CId && b.Id == EId)
                      select new { sal = a.SalaryAmount, cpfrateid = a.PFRateID, cpf = a.PFRate, epfrateid = b.PFRateID, epf = b.PFRate, date = a.FirstAppointmentDate, uniqueid = b.UniqueID, personid = a.PersonID }).FirstOrDefault();
        //get employer pf rate from [MasterEmployerPFRateDetails] (employer pf rate history table) on basis of effective date
        var IsAnyHistory = db.MasterEmployerPFRateDetails.Any(x => x.IsActive && x.UniqueID == result.uniqueid);
        if (IsAnyHistory)
        {
          var AllPfRateHistoryEmployer = db.MasterEmployerPFRateDetails.Where(x => x.IsActive && x.EffectiveDate <= ContributionDate && x.EffectiveEndDate >= ContributionDate && x.UniqueID == result.uniqueid).OrderByDescending(x => x.EffectiveDate).FirstOrDefault();
          if (AllPfRateHistoryEmployer != null)
          {
            var RateToBeAppliedOnEmployer = db.MasterEmployerPFRateDetails.Where(x => x.IsActive && x.EffectiveDate == AllPfRateHistoryEmployer.EffectiveDate && x.UniqueID == result.uniqueid).FirstOrDefault();
            EpfRate = RateToBeAppliedOnEmployer.PFRate;
            epfrateid = RateToBeAppliedOnEmployer.PFRateID;
          }
          else
          {
            data = new ContributorDetails
            {
              status = false,
              message = "Can't add contribution. Please maintain Employer Pf History for selected period."
            };
            return Json(data, JsonRequestBehavior.AllowGet);
          }
        }
        else
        {
          data = new ContributorDetails
          {
            status = false,
            message = "Can't add contribution. Please maintain Employer Pf History for selected period."
          };
          return Json(data, JsonRequestBehavior.AllowGet);
        }

        //get contributor pf rate from [MasterContributorPFRateDetails] (contributor pf rate history table) on basis of effective date
        var IsAnyHistoryContributor = db.MasterContributorPFRateDetails.Any(x => x.IsActive && x.PersonID == result.personid);
        if (IsAnyHistoryContributor)
        {
          var AllPfRateHistoryContributor = db.MasterContributorPFRateDetails.Where(x => x.IsActive && x.EffectiveDate <= ContributionDate && x.EffectiveEndDate >= ContributionDate && x.PersonID == result.personid).OrderByDescending(x => x.EffectiveDate).FirstOrDefault();
          if (AllPfRateHistoryContributor != null)
          {
            var RateToBeAppliedOnContributor = db.MasterContributorPFRateDetails.Where(x => x.IsActive && x.EffectiveDate == AllPfRateHistoryContributor.EffectiveDate && x.PersonID == result.personid).FirstOrDefault();
            CpfRate = RateToBeAppliedOnContributor.PFRate;
            cpfrateid = RateToBeAppliedOnContributor.PFRateID;
          }
          else
          {
            data = new ContributorDetails
            {
              status = false,
              message = "Can't add contribution. Please maintain Contributor Pf History for selected period."
            };
            return Json(data, JsonRequestBehavior.AllowGet);
          }
        }
        else
        {
          data = new ContributorDetails
          {
            status = false,
            message = "Can't add contribution. Please maintain Contributor Pf History for selected period."
          };
          return Json(data, JsonRequestBehavior.AllowGet);
        }

        //get contributor and employer current pf effective date
        //var EmployerPfRate = db.MasterPFRate.Where(x => x.Id == result.epfrateid).FirstOrDefault();
        //var ContributorPfRate = db.MasterPFRate.Where(x => x.Id == result.cpfrateid).FirstOrDefault();
        //if (ContributionDate >= EmployerPfRate.EffectiveDate)
        //{
        //  EpfRate = EmployerPfRate.PFRate;
        //  epfrateid = EmployerPfRate.Id;
        //}
        //else
        //{
        //  //get employer pf rate from [MasterEmployerPFRateDetails] (employer pf rate history table) on basis of effective date
        //  var IsAnyHistory = db.MasterEmployerPFRateDetails.Any(x => x.IsActive && x.UniqueID == result.uniqueid);
        //  if (IsAnyHistory)
        //  {
        //    var AllPfRateHistoryEmployer = db.MasterEmployerPFRateDetails.Where(x => x.IsActive && x.EffectiveDate < ContributionDate && x.UniqueID == result.uniqueid).OrderByDescending(x => x.EffectiveDate).FirstOrDefault();
        //    if (AllPfRateHistoryEmployer != null)
        //    {
        //      var RateToBeAppliedOnEmployer = db.MasterEmployerPFRateDetails.Where(x => x.IsActive && x.EffectiveDate == AllPfRateHistoryEmployer.EffectiveDate && x.UniqueID == result.uniqueid).FirstOrDefault();
        //      EpfRate = RateToBeAppliedOnEmployer.PFRate;
        //      epfrateid = RateToBeAppliedOnEmployer.PFRateID;
        //    }
        //    else
        //    {
        //      data = new ContributorDetails
        //      {
        //        status = false,
        //        message = "Can't add contribution. Selected Month and Year for contribution is not greater than current effective date of Employer pf as well as there are no history of Employer pf rates."
        //      };
        //      return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //  }
        //  else
        //  {
        //    data = new ContributorDetails
        //    {
        //      status = false,
        //      message = "Can't add contribution. Selected Month and Year for contribution is not greater than current effective date of Employer pf as well as there are no history of Employer pf rates."
        //    };
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //  }
        //}

        //if (ContributionDate >= ContributorPfRate.EffectiveDate)
        //{
        //  CpfRate = ContributorPfRate.PFRate;
        //  cpfrateid = ContributorPfRate.Id;
        //}
        //else
        //{
        //  //get contributor pf rate from [MasterContributorPFRateDetails] (contributor pf rate history table) on basis of effective date
        //  var IsAnyHistory = db.MasterContributorPFRateDetails.Any(x => x.IsActive && x.PersonID == result.personid);
        //  if (IsAnyHistory)
        //  {
        //    var AllPfRateHistoryContributor = db.MasterContributorPFRateDetails.Where(x => x.IsActive && x.EffectiveDate < ContributionDate && x.PersonID == result.personid).OrderByDescending(x => x.EffectiveDate).FirstOrDefault();
        //    if (AllPfRateHistoryContributor != null)
        //    {
        //      var RateToBeAppliedOnContributor = db.MasterContributorPFRateDetails.Where(x => x.IsActive && x.EffectiveDate == AllPfRateHistoryContributor.EffectiveDate && x.PersonID == result.personid).FirstOrDefault();
        //      CpfRate = RateToBeAppliedOnContributor.PFRate;
        //      cpfrateid = RateToBeAppliedOnContributor.PFRateID;
        //    }
        //    else
        //    {
        //      data = new ContributorDetails
        //      {
        //        status = false,
        //        message = "Can't add contribution. Selected Month and Year for contribution is not greater than current effective date of Contributor pf as well as there are no history of Contributor pf rates."
        //      };
        //      return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //  }
        //  else
        //  {
        //    data = new ContributorDetails
        //    {
        //      status = false,
        //      message = "Can't add contribution. Selected Month and Year for contribution is not greater than current effective date of Contributor pf as well as there are no history of Contributor pf rates."
        //    };
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //  }
        //}

        data = new ContributorDetails
        {
          salary = DecimalFormat(salaryc),
          cpf = DecimalFormat((salaryc / 100) * CpfRate),
          epf = DecimalFormat((salaryc / 100) * EpfRate),
          date = String.Format("{0:MM/dd/yyyy}", result.date),
          status = true,
          CpfRate = CpfRate,
          EpfRate = EpfRate
        };

        //data = new ContributorDetails
        //{
        //  salary = DecimalFormat(salaryc),
        //  cpf = DecimalFormat((salaryc / 100) * result.cpf),
        //  epf = DecimalFormat((salaryc / 100) * result.epf),
        //  date = String.Format("{0:MM/dd/yyyy}", result.date)
        //};
      }
      catch (Exception ex)
      {
        data = new ContributorDetails
        {
          status = false,
          message = "Error : " + ex.Message
        };
      }
      return Json(data, JsonRequestBehavior.AllowGet);
    }
    //used in finalised details (diff method due to check finalized details)
    //public JsonResult MonthSearching(string EmpId, string Month)
    //{
    //  if (Month == null)
    //  {
    //    var result = db.ContributonSheetHeader.Where(x => x.EmployerId.ToString() == EmpId && x.IsActive == true && x.Finalized == 0).Select(x => x.Month).Distinct().ToList();
    //    return Json(result, JsonRequestBehavior.AllowGet);
    //  }
    //  else
    //  {
    //    var result = db.ContributonSheetHeader.Where(x => x.EmployerId.ToString() == EmpId && x.Month == Month && x.IsActive == true && x.Finalized == 0).Select(x => x.Year).Distinct().ToList();
    //    return Json(result, JsonRequestBehavior.AllowGet);
    //  }

    //}

    //used in contributor details list
    public JsonResult MonthSearchAjax(int EmpId, string Month)
    {
      if (Month == null)
      {
        var months = (from y in db.MasterMonthName.Where(x => x.IsActive == true)
                      join x in db.ContributonSheetHeader.Where(x => x.IsActive == true && x.EmployerId == EmpId) on y.Name equals x.Month
                      select new
                      {
                        month = y.Id,
                        monthname = x.Month
                      }).OrderBy(o => o.month).Distinct();
        var result = months.Select(x => x.monthname).ToList();
        return Json(result, JsonRequestBehavior.AllowGet);
      }
      else
      {
        var result = db.ContributonSheetHeader.Where(x => x.EmployerId == EmpId && x.Month == Month && x.IsActive == true).Select(x => x.Year).Distinct().OrderByDescending(x => x).ToList();
        return Json(result, JsonRequestBehavior.AllowGet);
      }

    }

    //for comment in profile container
    //public ActionResult Comment(string id)
    //{
    //  ViewBag.ContributorID = id;
    //  return View();
    //}
    //[HttpPost]
    //public async Task<ActionResult> Comment(ContributorComment model)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    model.IsActive = true;
    //    db.Entry(model).State = EntityState.Added;
    //    int result = await db.SaveChangesAsync();
    //    if (result > 0)
    //    {
    //      TempData["success"] = "Comment Added Successfully";
    //      return RedirectToAction("CommentList");
    //    }
    //    else
    //    {
    //      TempData["error"] = "Error..Pls Try Again Later";
    //    }
    //  }

    //  return View();
    //}
    public ActionResult AddComment(int? id)
    {

      MasterContributor contributorPersonalDetails = db.MasterContributor.Where(x => x.Id == id).FirstOrDefault();
      //ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.Name = (contributorPersonalDetails.PrefixId == null ? "" : contributorPersonalDetails.Prefix.Name) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.CId = id;
      ViewBag.PersonID = contributorPersonalDetails.PersonID;
      if (contributorPersonalDetails.JobStatusID != 1)
      {
        ViewBag.ActiveContributor = "No";
      }

      return View();
    }
    public JsonResult AddComments(string PersonID, string comments)
    {
      var model = new ContributorComments();
      model.PersonID = PersonID;
      model.Comments = comments;
      db.Entry(model).State = EntityState.Added;
      int result = db.SaveChanges();
      return Json(result, JsonRequestBehavior.AllowGet);
    }




    public ActionResult CommentAjaxHandler(JQueryDataTableParamModel param, string PersonID)
    {
      var List = db.ContributorComments.Where(x => x.PersonID == PersonID).ToList();
      IEnumerable<ContributorComments> filtered;


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

      Func<ContributorComments, string> orderingFunction = (c => sortColumnIndex == 0 ? c.CreatedOn + "" : sortColumnIndex == 1 ? c.Comments :

                                                                                      "");

      var sortDirection = Request["sSortDir_0"]; // asc or desc
      if (sortDirection == "desc")
        filtered = filtered.OrderBy(orderingFunction);
      else
        filtered = filtered.OrderByDescending(orderingFunction);

      //Pagging
      var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

      //Select required columns
      var result = from c in displayed
                   select new[] {

                         c.Comments,
                                         String.Format("{0:MM/dd/yyyy}", c.CreatedOn)
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





    // used in addmarriage details
    public ActionResult SelectDetailsAjax(int id)
    {
      var results = db.MasterContributor.Where(x => x.Id == id).Select(x => x.DateOfBirth).FirstOrDefault();
      var DateOfBirth = String.Format("{0:MM/dd/yyyy}", results);
      var result = db.MasterContributor.Where(x => x.Id == id).Select(x => new { x.FirstName, x.LastName, x.MidName, x.Mobile, DateOfBirth, x.Email, x.Phone, x.PhoneOffice, x.CountryID }).FirstOrDefault();
      return Json(result, JsonRequestBehavior.AllowGet);
    }

    [HttpGet]
    public ActionResult SalaryHistoryIndex(int? id)
    {
      MasterContributor contributorPersonalDetails = db.MasterContributor.Where(x => x.Id == id).FirstOrDefault();
      //ViewBag.Name = contributorPersonalDetails.PrefixId == null ? string.Empty : (db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).Any() ? db.MasterPrefix.Where(x => x.Id == contributorPersonalDetails.PrefixId).FirstOrDefault().Name : string.Empty) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.Name = (contributorPersonalDetails.PrefixId == null ? "" : contributorPersonalDetails.Prefix.Name) + " " + contributorPersonalDetails.FirstName + " " + contributorPersonalDetails.MidName + " " + contributorPersonalDetails.LastName;
      ViewBag.PersonID = contributorPersonalDetails.PersonID;
      ViewBag.CId = id;
      if (contributorPersonalDetails.JobStatusID != 1)
      {
        ViewBag.ActiveContributor = "No";
      }
      return View();
    }
    public JsonResult AddSalary(string PersonID, decimal Salary)
    {
      if (!db.MasterContributorJobDetails.Where(x => x.PersonID == PersonID).Any())
      {
        if (!db.ContributorSalaryHistory.Where(x => x.PersonID == PersonID && x.SalaryAmount == Salary).Any())
        {
          var model = new ContributorSalaryHistory();
          model.PersonID = PersonID;
          model.SalaryAmount = Salary;
          model.EffectiveDate = DateTime.Now;
          db.Entry(model).State = EntityState.Added;
          int result = db.SaveChanges();
          return Json(result, JsonRequestBehavior.AllowGet);
        }
        return Json("error", JsonRequestBehavior.AllowGet);
      }
      return Json("joberror", JsonRequestBehavior.AllowGet);
    }
    public ActionResult SalaryAjaxHandler(JQueryDataTableParamModel param, string PersonID)
    {
      var List = db.ContributorSalaryHistory.Where(x => x.PersonID == PersonID).ToList();
      IEnumerable<ContributorSalaryHistory> filtered;


      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => c.SalaryAmount.ToString().ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<ContributorSalaryHistory, string> orderingFunction = (c => sortColumnIndex == 0 ? c.SalaryAmount + "" :
                                                                                      "");

      var sortDirection = Request["sSortDir_0"]; // asc or desc
      if (sortDirection == "desc")
        filtered = filtered.OrderBy(orderingFunction);
      else
        filtered = filtered.OrderByDescending(orderingFunction);

      //Pagging
      var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

      //Select required columns
      var result = from c in displayed
                   select new[] {

                         c.SalaryAmount.ToString("#,##0.00"),
                                         c.Active+"",
                     c.Default+"",
                     String.Format("{0:MM/dd/yyyy}", c.EffectiveDate),
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
    public ActionResult CheckSalaryAjax(string PersonID, int Id)
    {
      var model = db.ContributorSalaryHistory.Where(x => x.Active == true && x.PersonID == PersonID).FirstOrDefault();
      if (model != null)
      {
        model.Active = false;
        db.Entry(model).State = EntityState.Modified;
        db.SaveChanges();
      }
      var entity = db.ContributorSalaryHistory.Where(x => x.Id == Id).FirstOrDefault();
      entity.Active = true;
      db.Entry(entity).State = EntityState.Modified;
      int result = db.SaveChanges();

      MasterContributor contributorModel = db.MasterContributor.Where(x => x.PersonID == PersonID).FirstOrDefault();
      if (contributorModel != null)
      {
        contributorModel.SalaryAmount = entity.SalaryAmount;
        db.Entry(contributorModel).State = EntityState.Modified;
        db.SaveChanges();
      }
      var jobDetailsModel = db.MasterContributorJobDetails.Where(x => x.PersonID == PersonID).FirstOrDefault();
      if (jobDetailsModel != null)
      {
        jobDetailsModel.PresentSalary = entity.SalaryAmount;
        db.Entry(jobDetailsModel).State = EntityState.Modified;
        db.SaveChanges();
      }
      return Json(result, JsonRequestBehavior.AllowGet);
    }
    public ActionResult CheckDefaultAjax(string PersonID, int Id)
    {
      var model = db.ContributorSalaryHistory.Where(x => x.Default == true && x.PersonID == PersonID).FirstOrDefault();
      if (model != null)
      {
        model.Default = false;
        db.Entry(model).State = EntityState.Modified;
      }
      var entity = db.ContributorSalaryHistory.Where(x => x.Id == Id).FirstOrDefault();
      entity.Default = true;
      db.Entry(entity).State = EntityState.Modified;
      int result = db.SaveChanges();
      return Json(result, JsonRequestBehavior.AllowGet);
    }
    public JsonResult ExpectedAjax(string hireDate, string empType, string dob)
    {
      if (hireDate.Length > 0)
      {
        int retirementAge = 0;
        DateTime date = Convert.ToDateTime(hireDate);
        DateTime bdate = Convert.ToDateTime(dob);
        int EmpType = Convert.ToInt32(empType);
        var result = (from a in db.MasterEmployer join b in db.MasterEmployerType on a.EmployerTypeID equals b.Id where a.Id == EmpType select new { b.RetirementAge, b.RetirementAgeAfter2004 }).FirstOrDefault();

        DateTime date1 = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        DateTime date2 = new DateTime(2004, 1, 1, 0, 0, 0);
        int expectedRetirementAge = DateTime.Compare(date1, date2);
        if (expectedRetirementAge < 0)
        {
          retirementAge = result.RetirementAge;
        }
        else
        {
          retirementAge = result.RetirementAgeAfter2004;
        }
        string retireDate = String.Format("{0:MM/dd/yyyy}", bdate.AddYears(+retirementAge));
        return Json(retireDate, JsonRequestBehavior.AllowGet);
      }
      return Json(0, JsonRequestBehavior.AllowGet);
    }
    public JsonResult getOfficePhoneAjax(int? id)
    {
      var result = db.MasterEmployer.Where(x => x.Id == id).Select(x => new { x.Mobile, x.EmployerTypeID }).FirstOrDefault();
      return Json(result, JsonRequestBehavior.AllowGet);
    }
    public ActionResult _ErrorInExcelSheetAjax()
    {
      return View();
    }
    public ActionResult ErrorListAjaxHandler(JQueryDataTableParamModel param)
    {
      List<ExcelFileViewModel> List = new List<ExcelFileViewModel>();
      if (Session["excellist"] != null)
      {
        List = (List<ExcelFileViewModel>)Session["excellist"];
        List = List.Where(x => x.error != null).ToList();
      }
      IEnumerable<ExcelFileViewModel> filtered;

      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => c.error.ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      //var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      //Func<ContributorSalaryHistory, string> orderingFunction = (c => sortColumnIndex == 0 ? c.SalaryAmount + "" :
      //                                                                                "");

      //var sortDirection = Request["sSortDir_0"]; // asc or desc
      //if (sortDirection == "desc")
      //  filtered = filtered.OrderBy(orderingFunction);
      //else
      //  filtered = filtered.OrderByDescending(orderingFunction);

      //Pagging
      var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

      //Select required columns
      var result = from c in displayed
                   select new[] {
                         c.error,
                         c.status
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



    public ActionResult HelpForErrorAjax()
    {
      return View();
    }


    public JsonResult RejoinAjax(int Id)
    {
      int tempId = db.MasterContributor.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
      MasterContributor cmodel = db.MasterContributor.Find(Id);
      cmodel.SalaryAmount = 0;
      cmodel.FirstAppointmentDate = null;
      cmodel.LastAppointmentDate = null;
      cmodel.ExpectedRetirementDate = null;
      cmodel.JobStatusID = 1;
      cmodel.MonthlySalary = 0;
      cmodel.AnnualAmount = 0;
      cmodel.Balance = 0;
      cmodel.QuarterlyAmount = 0;
      cmodel.OldPersonID = cmodel.PersonID;
      cmodel.PersonID = "PF00" + (tempId + 1);
      db.Entry(cmodel).State = EntityState.Added;
      int result = db.SaveChanges();
      if (result > 0)
      {
        var marriageModel = db.MasterContributorMarriageDetails.Where(x => x.PersonId == cmodel.OldPersonID).ToList();
        if (marriageModel != null && marriageModel.Count() > 0)
        {
          foreach (var marriage in marriageModel)
          {
            MasterContributorMarriageDetails marriageUpdateModel = db.MasterContributorMarriageDetails.Find(marriage.Id);
            marriageUpdateModel.PersonId = cmodel.OldPersonID;
            db.Entry(marriageUpdateModel).State = EntityState.Added;
            result = db.SaveChanges();
          }
        }
        var dependantModel = db.MasterDependantDetails.Where(x => x.PersonID == cmodel.OldPersonID).ToList();
        if (dependantModel != null && dependantModel.Count() > 0)
        {
          foreach (var dependant in dependantModel)
          {
            MasterDependantDetails dependantUpdateModel = db.MasterDependantDetails.Find(dependant.Id);
            dependantUpdateModel.PersonID = cmodel.OldPersonID;
            db.Entry(dependantUpdateModel).State = EntityState.Added;
            result = db.SaveChanges();
          }
        }
      }
      if (result == 1)
      {
        TempData["success"] = "Rejoined Successfully";
        result = cmodel.Id;
      }
      return Json(result, JsonRequestBehavior.AllowGet);
    }

    public ActionResult errorsInContributorDetails(int Id = 2, int jobStatusId = 1)
    {
      var employer = db.MasterEmpType.Where(x => x.Type_Code != null).Select(x => new { Id = x.Emp_type_ID, EmployerName = x.Type_Code }).OrderBy(x => x.EmployerName).ToList();
      employer.Insert(0, new { Id = 0, EmployerName = "All" });
      var jobDetails = db.MasterStatus.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, Name = x.Name }).OrderBy(x => x.Name).ToList();
      jobDetails.Insert(0, new { Id = 0, Name = "All" });
      ViewBag.EmployerName = new SelectList(employer, "Id", "EmployerName", Id);
      ViewBag.JobStatusID = new SelectList(jobDetails, "Id", "Name", jobStatusId);
      return View();
    }
    public ActionResult contributorDetailsErrorAjaxHandler(JQueryDataTableParamModel param, int? Id, int? JobStatusID)
    {
      DataSet ds = new DataSet();
      System.Text.StringBuilder SQL = new System.Text.StringBuilder();
      SQL.Append("SELECT EmployerId=MasterEmployer.Id,EmployerName=MasterEmployer.EmployerName,dbo.MasterContributor.JobStatusId,dbo.MasterContributor.DateOfBirth,dbo.MasterContributor.Personid,dbo.MasterContributor.Id,DependantId=isnull(dbo.MasterDependantDetails.Id,0),MarriageId=isnull(dbo.MasterContributorMarriageDetails.Id,0),JobDetailId=isnull(dbo.MasterContributorJobDetails.Id,0),Name = (isnull(dbo.MasterContributor.FirstName, '') + ' ' + isnull(dbo.MasterContributor.MidName, '') + '' + isnull(dbo.MasterContributor.LastName, '')),Error = isnull(CAST(dbo.MasterContributor.EmployerID AS VARCHAR(50)), 'CE') + isnull(CAST(dbo.MasterContributor.SocialSecurityNo AS VARCHAR(50)), 'CS') + isnull(CAST(dbo.MasterContributor.FirstName AS VARCHAR(50)), 'CF') + isnull(CAST(dbo.MasterContributor.LastName AS VARCHAR(50)), 'CL') + isnull(CAST(dbo.MasterContributor.DateOfBirth AS VARCHAR(50)), 'CD') + isnull(CAST(dbo.MasterContributor.ExpectedRetirementDate AS VARCHAR(50)), 'CX') + isnull(CAST(dbo.MasterContributor.CountryID AS VARCHAR(50)), 'CC') + isnull(CAST(dbo.MasterContributor.PermanentCityID AS VARCHAR(50)), 'CP') + isnull(CAST(dbo.MasterContributor.FirstAppointmentDate AS VARCHAR(50)), 'CA') + isnull(CAST(dbo.MasterContributor.PFRateID AS VARCHAR(50)), 'CR') + isnull(CAST(dbo.MasterContributorJobDetails.DepartmentId AS VARCHAR(50)), 'JD') + isnull(CAST(dbo.MasterContributorJobDetails.DesignationId AS VARCHAR(50)), 'JDD') + isnull(CAST(dbo.MasterContributorJobDetails.JoiningDate AS VARCHAR(50)), 'JJ') +  isnull(CAST(dbo.MasterContributorJobDetails.PresentSalary AS VARCHAR(50)), 'JP') + isnull(CAST(dbo.MasterContributorJobDetails.HireDate AS VARCHAR(50)), 'JH') + isnull(dbo.MasterContributorMarriageDetails.SpouseFirstName, 'MS') + isnull(dbo.MasterContributorMarriageDetails.LastName, 'ML') + isnull(CAST(dbo.MasterContributorMarriageDetails.CountryID AS VARCHAR(50)), 'MC') + isnull(CAST(dbo.MasterContributorMarriageDetails.DateOfBirth AS VARCHAR(50)), 'MD') + isnull(CAST(dbo.MasterContributorMarriageDetails.DateOfBirth AS VARCHAR(50)), 'MP') + isnull(CAST(dbo.MasterContributorMarriageDetails.Mobile AS VARCHAR(50)), 'MM') + isnull(CAST(dbo.MasterContributorMarriageDetails.MarriageBeginDate AS VARCHAR(50)), 'MBD') + isnull(CAST(dbo.MasterContributorMarriageDetails.MaritalStatusId AS VARCHAR(50)), 'MMS') +isnull(dbo.MasterDependantDetails.FirstName,'DF')+isnull(dbo.MasterDependantDetails.LastName,'DL')+ isnull(CAST(dbo.MasterDependantDetails.DateOfBirth AS VARCHAR(50)),'DD')+ isnull(CAST(dbo.MasterDependantDetails.RelationshipID AS VARCHAR(50)),'DR')+ isnull(CAST(dbo.MasterDependantDetails.TerminationDate AS VARCHAR(50)),'DT') FROM dbo.MasterContributor inner join masteremployer on MasterContributor.EmployerId=MasterEmployer.Id  left JOIN dbo.MasterContributorJobDetails ON dbo.MasterContributor.PersonID = dbo.MasterContributorJobDetails.PersonID left JOIN  dbo.MasterContributorMarriageDetails ON dbo.MasterContributor.PersonID = dbo.MasterContributorMarriageDetails.PersonId left JOIN dbo.MasterDependantDetails ON dbo.MasterContributor.PersonID = dbo.MasterDependantDetails.PersonID WHERE ((dbo.MasterContributor.EmployerID IS NULL) OR(dbo.MasterContributor.SocialSecurityNo IS NULL) OR(dbo.MasterContributor.FirstName IS NULL) OR(dbo.MasterContributor.LastName IS NULL) OR (dbo.MasterContributor.DateOfBirth IS NULL) OR(dbo.MasterContributor.ExpectedRetirementDate IS NULL) OR(dbo.MasterContributor.CountryID IS NULL) OR(dbo.MasterContributor.PermanentCityID IS NULL) OR(dbo.MasterContributor.FirstAppointmentDate IS NULL) OR(dbo.MasterContributor.PFRateID IS NULL)  OR (dbo.MasterContributorMarriageDetails.MaritalStatusId IS NULL) OR(dbo.MasterContributorMarriageDetails.MarriageBeginDate IS NULL) OR(dbo.MasterContributorMarriageDetails.Mobile IS NULL) OR (dbo.MasterContributorMarriageDetails.Phone IS NULL) OR(dbo.MasterContributorMarriageDetails.DateOfBirth IS NULL) OR(dbo.MasterContributorMarriageDetails.CountryID IS NULL) OR (dbo.MasterContributorMarriageDetails.LastName IS NULL) OR(dbo.MasterContributorMarriageDetails.SpouseFirstName IS NULL) OR(dbo.MasterContributorJobDetails.HireDate IS NULL) OR (dbo.MasterContributorJobDetails.PresentSalary IS NULL) OR(dbo.MasterContributorJobDetails.JoiningDate IS NULL) OR(dbo.MasterContributorJobDetails.DesignationId IS NULL) OR (dbo.MasterContributorJobDetails.DepartmentId IS NULL)) AND (dbo.MasterContributor.IsActive=1)");
      if (!string.IsNullOrEmpty(Id.ToString()) && Id != 0)
        SQL.Append(" And MasterEmployer.Id=" + Id);
      if (!string.IsNullOrEmpty(JobStatusID.ToString()) && JobStatusID != 0 && JobStatusID != 8)
        SQL.Append(" And dbo.MasterContributor.JobStatusId=" + JobStatusID);
      SQL.Append(" order by masteremployer.id");
      //string con = System.Web.Configuration.WebConfigurationManager.AppSettings["SQLConn"];
      string con = ConnectionStringProvider.GetConnectionString();
      System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(SQL.ToString(), con);
      da.Fill(ds);
      List<ErrprInContributorDetailsViewModel> List = App.Web.Repository.ListToDataset.ToList<ErrprInContributorDetailsViewModel>(ds.Tables[0]);
      IEnumerable<ErrprInContributorDetailsViewModel> filtered;
      //if (!string.IsNullOrEmpty(Id.ToString()) && Id != 0)
      //{
      //    List = List.Where(x => x.EmployerId == Id).ToList();
      //}
      //if (!string.IsNullOrEmpty(JobStatusID.ToString()) && JobStatusID != 0 && JobStatusID != 8)
      //{
      //    List = List.Where(x => x.JobStatusId == JobStatusID).ToList();
      //}

      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => (c.Name).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
           || c.Personid.ToLower().Contains(param.sSearch.ToLower())
           || c.EmployerName.ToLower().Contains(param.sSearch.ToLower())
           );

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<ErrprInContributorDetailsViewModel, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name :
                                                                            sortColumnIndex == 1 ? c.Personid :
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
                         c.Personid,
                         c.Name,
                         c.Id+"#"+c.Error+"#"+c.JobStatusId+'#'+c.DateOfBirth,
                         c.JobDetailId+"#"+c.Id+"#"+c.Error+"#"+c.JobStatusId,
                         c.MarriageId+"#"+c.Id+"#"+c.Error+"#"+c.JobStatusId,
                         c.DependantId+"#"+c.Id+"#"+c.Error+"#"+c.JobStatusId,
                         c.EmployerName
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
    public ActionResult GetseletedTypeAjax(string term, string seletedType)
    {
      int UserId = AppUserManager.GetUserId();
      int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
      List<int> DistrictIds = db.SecRoleLocationModule.Where(x => x.RoleID == RoleId && x.UserId == UserId).Select(x => x.DistrictID).Distinct().ToList();
      var query = (from md in db.MasterBeneficiariesDetails
                   join mt in db.MasterEmpType on md.SelectPensionType equals mt.Description into mt1
                   from mt in mt1.DefaultIfEmpty()
                   join di in db.MasterDistrict on md.SelectDistrict.ToLower().Trim() equals di.Name.ToLower().Trim() into di1
                   from di in di1.DefaultIfEmpty()
                   where /*mt.Type_Code == seletedType*/(seletedType == "ALL" || mt.Type_Code == seletedType)  && md.NameOfTheApplicant.StartsWith(term) && DistrictIds.Contains(di.Id)
                   select new
                   {
                     Key = md.Id,
                     Value = string.Concat((md.NameOfTheApplicant == null ? "" : md.NameOfTheApplicant) + "-" + (md.PermanentDistrict == null ? "" : md.PermanentDistrict) + "-" + (md.PermanentTehsil == null ? "" : md.PermanentTehsil))
                   }).ToList();
      return Json(query, JsonRequestBehavior.AllowGet);
    }



    [HttpGet]
    public ActionResult ContributionDetails()
    {
      List<MasterContributor> Contributor = new List<MasterContributor>();
      ViewBag.PersonID = new SelectList(Contributor, "Id", "FirstName");
      ViewBag.EmployerId = db.MasterEmpType.Where(x => x.Type_Code != null).Select(x => new SelectListItem
      {
        Value = x.Type_Code,
        Text = x.Type_Code + " | " + x.Description
      })
   .ToList();
      return View();
    }
    public ActionResult EditDetailSearchAjax1(JQueryDataTableParamModel param, string empId, string conId)
    {

      //List<ExcelFileViewModel> Verify = new List<ExcelFileViewModel>();
      List<ExcelFileViewModel> tempVerify = new List<ExcelFileViewModel>();
      using (AppDbContext dbContext = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
      {
        DataSet ds = new DataSet();
        System.Text.StringBuilder SQL = new System.Text.StringBuilder();
        SQL.Append("select dbo.ContributonSheetHeaderFinalise.Month, dbo.ContributonSheetHeaderFinalise.Year, dbo.MasterSource.Name AS SourceName, dbo.ContributonSheetDetailsFinalise.SalaryAmount, dbo.ContributonSheetDetailsFinalise.ContributorContribution, dbo.ContributonSheetDetailsFinalise.EmployerContribution , dbo.ContributonSheetDetailsFinalise.Id AS Id, dbo.MasterEmployer.EmployerName, PersonId=dbo.MasterContributor.PersonID,");
        SQL.Append("(isnull(dbo.MasterContributor.FirstName, '') + ' ' + isnull(dbo.MasterContributor.MidName, '') + ' ' + isnull(dbo.MasterContributor.LastName, '')) as ContributorName");
        SQL.Append(",dbo.MasterMonthName.Id As MonthNumber,SystemSalaryAmount = (dbo.MasterContributor.SalaryAmount / 12), SystemContributorContribution = (dbo.MasterContributor.SalaryAmount / 1200) * dbo.MasterContributor.PFRate, SystemEmployerContribution = (dbo.MasterContributor.SalaryAmount / 1200) * dbo.MasterEmployer.PFRate FROM            dbo.ContributonSheetDetailsFinalise INNER JOIN                         dbo.ContributonSheetHeaderFinalise ON dbo.ContributonSheetDetailsFinalise.ContributonSheetHeaderFinaliseID = dbo.ContributonSheetHeaderFinalise.Id   INNER JOIN dbo.MasterSource ON dbo.ContributonSheetDetailsFinalise.SourceID =  dbo.MasterSource.Id INNER JOIN                         dbo.MasterMonthName ON dbo.ContributonSheetHeaderFinalise.Month = dbo.MasterMonthName.Name INNER JOIN                         dbo.MasterContributor ON dbo.ContributonSheetDetailsFinalise.ContributorID = dbo.MasterContributor.Id INNER JOIN                         dbo.MasterEmployer ON dbo.ContributonSheetHeaderFinalise.EmployerId = dbo.MasterEmployer.Id WHERE(dbo.ContributonSheetHeaderFinalise.IsActive = 1) AND (dbo.ContributonSheetDetailsFinalise.IsActive = 1)");
        if (!string.IsNullOrWhiteSpace(empId))
          SQL.Append("and dbo.ContributonSheetHeaderFinalise.EmployerId=" + empId);
        if (!string.IsNullOrWhiteSpace(conId))
          SQL.Append("and dbo.ContributonSheetDetailsFinalise.ContributorID=" + conId);
        SQL.Append(" ORDER BY dbo.ContributonSheetHeaderFinalise.Year DESC, MonthNumber DESC");
        //string con = System.Web.Configuration.WebConfigurationManager.AppSettings["SQLConn"];
        string con = ConnectionStringProvider.GetConnectionString();
        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(SQL.ToString(), con);
        da.Fill(ds);
        tempVerify = App.Web.Repository.ListToDataset.ToList<ExcelFileViewModel>(ds.Tables[0]);
      }
      string employerCont = tempVerify.Any() ? "EC$ " + (tempVerify.Sum(x => x.EmployerContribution)) + "" : "";
      string contributorCont = tempVerify.Any() ? "EC$ " + (tempVerify.Sum(x => x.ContributorContribution)) + "" : "";
      string monthCont = tempVerify.Any() ? (tempVerify.Select(x => new { x.Month, x.Year }).Distinct().Count()) + "" : "";
      string SystemEmployerTotalContribution = "EC$ " + Convert.ToDecimal(tempVerify.Sum(x => x.SystemEmployerContribution)).ToString("#,##0.00");
      string SystemContributorTotalContribution = "EC$ " + Convert.ToDecimal(tempVerify.Sum(x => x.SystemContributorContribution)).ToString("#,##0.00");
      IEnumerable<ExcelFileViewModel> filtered;
      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = tempVerify.Where(c => c.PersonId.ToLower().Contains(param.sSearch.ToLower())
                         || c.SalaryAmount.ToString().ToLower().Contains(param.sSearch.ToLower())
                         || c.ContributorContribution.ToString().ToLower().Contains(param.sSearch.ToLower())
                         || c.EmployerContribution.ToString().ToLower().Contains(param.sSearch.ToLower())
                         || (c.ContributorName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
                         || (c.EmployerName).ToLower().Contains(param.sSearch.ToLower())
                         //|| c.Contributor.LastName.ToLower().Contains(param.sSearch.ToLower())
                         );

      }
      else
      {
        filtered = tempVerify;
      }

      //Pagging
      var displayed = filtered.Skip(param.iDisplayStart).Take(param.iDisplayLength);

      ////Select required columns
      var result = from c in displayed
                   select new[] {
                             c.Id+"",
                         c.PersonId,
                         //c.EmployerName,
                         c.ContributorName,
                         c.SourceName,
                         Convert.ToDecimal(c.SystemSalaryAmount).ToString("#,##0.00"),
                        Convert.ToDecimal(c.SystemContributorContribution).ToString("#,##0.00"),
                        Convert.ToDecimal(c.SystemEmployerContribution).ToString("#,##0.00"),
                         Convert.ToDecimal(c.SalaryAmount).ToString("#,##0.00"),
                     Convert.ToDecimal(c.ContributorContribution).ToString("#,##0.00"),
                     Convert.ToDecimal(c.EmployerContribution).ToString("#,##0.00"),
                     c.Month+" - "+c.Year,
                    c.Id+"",
                    employerCont,
                    contributorCont,
                    monthCont,
                    SystemEmployerTotalContribution,
                    SystemContributorTotalContribution
                   };

      return Json(
                                  new
                                  {
                                    sEcho = param.sEcho,
                                    iTotalRecords = tempVerify.Count(),
                                    iTotalDisplayRecords = filtered.Count(),
                                    aaData = result
                                  },
    JsonRequestBehavior.AllowGet);
    }

    //public JsonResult deleteAllContributionAjax(string Value)
    //{
    //    DataSet ds = new DataSet();
    //    System.Text.StringBuilder SQL = new System.Text.StringBuilder();
    //    SQL.Append("delete from ContributonSheetDetailsFinalise where id in(" + Value + ")");
    //    string con = System.Web.Configuration.WebConfigurationManager.AppSettings["SQLConn"];
    //    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(SQL.ToString(), con);
    //    da.Fill(ds);
    //    return Json(1, JsonRequestBehavior.AllowGet);
    //}

    public JsonResult deleteAllContributionAjax(string Value, string Notes)
    {
      var temp = Value.Split(',');
      int result = 0;
      if (temp.Length > 0)
      {
        foreach (var Id in temp)
        {
          int id = Convert.ToInt32(Id);
          ContributonSheetDetailsFinalise entity = db.ContributonSheetDetailsFinalise.Where(x => x.Id == id).FirstOrDefault();
          entity.IsActive = false;
          entity.Notes = Notes;
          db.Entry(entity).State = EntityState.Modified;
          result = db.SaveChanges();
          if (result > 0)
          {
            int fhId = db.ContributonSheetHeaderFinalise.Where(x => x.Id == entity.ContributonSheetHeaderFinaliseID).Select(x => x.ContributonSheetHeaderId).FirstOrDefault();
            ContributonSheetDetails details = db.ContributonSheetDetails.Where(x => x.ContributonSheetHeaderID == fhId && x.ContributorID == entity.ContributorID && x.IsActive == true).FirstOrDefault();
            if (details != null)
            {
              details.IsActive = false;
              db.Entry(details).State = EntityState.Modified;
              db.SaveChanges();
            }
          }
        }
      }
      return Json(result, JsonRequestBehavior.AllowGet);
    }

    // **** Code By Himanshu Rajput ****
    // UploadExceldownload
    private FileContentResult JandKBeneficiaryReport(List<MasterBeneficiariesDetails> dta)
    {
      try
      {
        //var result = from x in dta
        //             select new[]
        //             {
        //            x.SNo,
        //            x.ApplicationReferenceNo + "",
        //            x.SubmissionLocation + "",
        //            x.SubmissionDate + "",
        //            x.AppliedBy + "",
        //            x.SelectTehsilSocialWelfareOffice_TSWO,
        //            x.SelectDistrict,
        //            x.NameOfTheApplicant,
        //            x.DateOfBirth,
        //            x.Age_InYears,
        //            x.MobileNumber,
        //            x.DoYouHaveBPLcard,
        //            x.FatherOrHusbandOrGuardianName,
        //            x.EMail + "",
        //            x.Category + "",
        //            x.Gender + "",
        //            x.PresentAddress+"",
        //            x.PresentDistrict+"",
        //            x.PresentVillageName+"",
        //            x.Pincode+"",
        //            x.PresentHalqaPanchayatOrMunicipalityName+"",
        //            x.PresentTehsil+"",
        //            x.PermanentAddress+"",
        //            x.PermanentDistrict+"",
        //            x.PermanentTehsil+"",
        //            x.PermanentHalqaPanchayatOrMunicipalityName+"",
        //            x.PermanentVillageName+"",
        //            x.BranchName+"",
        //            x.IFSCCode+"",
        //            x.AccountNoOfTheApplicant+"",
        //            x.BankName+"",
        //            x.SelectPensionType+"",
        //            x.PercentageofDisability+"",
        //            x.CivilCondition+"",
        //            x.AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP+"",
        //            x.BankName1+"",
        //            x.BranchName1+"",
        //            x.IFSCCode1+"",
        //            x.AccountNumber+"",
        //            x.ApplicationSanctionedunderSchemeName+"",
        //            x.CurrentTask+"",
        //            x.CurrentStatus+"",
        //            x.LastTask+"",
        //            x.VersionNo+"",
        //            x.Last_pay_date+"",
        //            x.Application_approve_on+"",

        //           };

        var result = dta;

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
            cell0.CellValue = new CellValue(" SNo");
            headerRow.AppendChild(cell0);

            Cell cell1 = new Cell();
            cell1.DataType = CellValues.String;
            cell1.CellValue = new CellValue("Application Reference No");
            headerRow.AppendChild(cell1);

            Cell cell2 = new Cell();
            cell2.DataType = CellValues.String;
            cell2.CellValue = new CellValue("Submission Location");
            headerRow.AppendChild(cell2);

            Cell cell3 = new Cell();
            cell3.DataType = CellValues.String;
            cell3.CellValue = new CellValue("Submission Date");
            headerRow.AppendChild(cell3);

            Cell cell4 = new Cell();
            cell4.DataType = CellValues.String;
            cell4.CellValue = new CellValue("AppliedBy");
            headerRow.AppendChild(cell4);

            Cell cell5 = new Cell();
            cell5.DataType = CellValues.String;
            cell5.CellValue = new CellValue("Select Tehsil Social WelfareOffice_TSWO");
            headerRow.AppendChild(cell5);

            Cell cell6 = new Cell();
            cell6.DataType = CellValues.String;
            cell6.CellValue = new CellValue("Select District");
            headerRow.AppendChild(cell6);

            Cell cell7 = new Cell();
            cell7.DataType = CellValues.String;
            cell7.CellValue = new CellValue("Name Of The Applicant");
            headerRow.AppendChild(cell7);

            Cell cell8 = new Cell();
            cell8.DataType = CellValues.String;
            cell8.CellValue = new CellValue("Date Of Birth");
            headerRow.AppendChild(cell8);

            Cell cell9 = new Cell();
            cell9.DataType = CellValues.String;
            cell9.CellValue = new CellValue("Age InYears");
            headerRow.AppendChild(cell9);

            Cell cell10 = new Cell();
            cell10.DataType = CellValues.String;
            cell10.CellValue = new CellValue("Mobile Number");
            headerRow.AppendChild(cell10);

            Cell cell11 = new Cell();
            cell11.DataType = CellValues.String;
            cell11.CellValue = new CellValue("Do You Have BPL Card");
            headerRow.AppendChild(cell11);

            Cell cell12 = new Cell();
            cell12.DataType = CellValues.String;
            cell12.CellValue = new CellValue("Father Or Husband Or Guardian Name");
            headerRow.AppendChild(cell12);

            Cell cell13 = new Cell();
            cell13.DataType = CellValues.String;
            cell13.CellValue = new CellValue("EMail");
            headerRow.AppendChild(cell13);

            Cell cell14 = new Cell();
            cell14.DataType = CellValues.String;
            cell14.CellValue = new CellValue("Category");
            headerRow.AppendChild(cell14);

            Cell cell15 = new Cell();
            cell15.DataType = CellValues.String;
            cell15.CellValue = new CellValue("Gender");
            headerRow.AppendChild(cell15);

            Cell cell16 = new Cell();
            cell16.DataType = CellValues.String;
            cell16.CellValue = new CellValue("Present Address");
            headerRow.AppendChild(cell16);

            Cell cell17 = new Cell();
            cell17.DataType = CellValues.String;
            cell17.CellValue = new CellValue("Present District");
            headerRow.AppendChild(cell17);

            Cell cell18 = new Cell();
            cell18.DataType = CellValues.String;
            cell18.CellValue = new CellValue("Present Village Name");
            headerRow.AppendChild(cell18);

            Cell cell19 = new Cell();
            cell19.DataType = CellValues.String;
            cell19.CellValue = new CellValue("Pincode");
            headerRow.AppendChild(cell19);

            Cell cell20 = new Cell();
            cell20.DataType = CellValues.String;
            cell20.CellValue = new CellValue("Present HalqaPanchayat Or Municipality Name");
            headerRow.AppendChild(cell20);

            Cell cell21 = new Cell();
            cell21.DataType = CellValues.String;
            cell21.CellValue = new CellValue("Present Tehsil");
            headerRow.AppendChild(cell21);

            Cell cell22 = new Cell();
            cell22.DataType = CellValues.String;
            cell22.CellValue = new CellValue("Permanent Address");
            headerRow.AppendChild(cell22);

            Cell cell23 = new Cell();
            cell23.DataType = CellValues.String;
            cell23.CellValue = new CellValue("Permanent District");
            headerRow.AppendChild(cell23);

            Cell cell24 = new Cell();
            cell24.DataType = CellValues.String;
            cell24.CellValue = new CellValue("Permanent Tehsil");
            headerRow.AppendChild(cell24);

            Cell cell25 = new Cell();
            cell25.DataType = CellValues.String;
            cell25.CellValue = new CellValue("Permanent HalqaPanchayat Or Municipality Name");
            headerRow.AppendChild(cell25);

            Cell cell26 = new Cell();
            cell26.DataType = CellValues.String;
            cell26.CellValue = new CellValue("Permanent Village Name");
            headerRow.AppendChild(cell26);

            Cell cell27 = new Cell();
            cell27.DataType = CellValues.String;
            cell27.CellValue = new CellValue("Branch Name");
            headerRow.AppendChild(cell27);

            Cell cell28 = new Cell();
            cell28.DataType = CellValues.String;
            cell28.CellValue = new CellValue("IFSC Code");
            headerRow.AppendChild(cell28);

            Cell cell29 = new Cell();
            cell29.DataType = CellValues.String;
            cell29.CellValue = new CellValue("Account No Of The Applicant");
            headerRow.AppendChild(cell29);

            Cell cell30 = new Cell();
            cell30.DataType = CellValues.String;
            cell30.CellValue = new CellValue("Bank Name");
            headerRow.AppendChild(cell30);

            Cell cell31 = new Cell();
            cell31.DataType = CellValues.String;
            cell31.CellValue = new CellValue("Select Pension Type");
            headerRow.AppendChild(cell31);

            Cell cell32 = new Cell();
            cell32.DataType = CellValues.String;
            cell32.CellValue = new CellValue("Percent age of Disability");
            headerRow.AppendChild(cell32);

            Cell cell33 = new Cell();
            cell33.DataType = CellValues.String;
            cell33.CellValue = new CellValue("Civil Condition");
            headerRow.AppendChild(cell33);

            Cell cell34 = new Cell();
            cell34.DataType = CellValues.String;
            cell34.CellValue = new CellValue("Are You Previously Taking Pension FromJK_ISSS_GOI_NSAP");
            headerRow.AppendChild(cell34);

            Cell cell35 = new Cell();
            cell35.DataType = CellValues.String;
            cell35.CellValue = new CellValue("Bank Name");
            headerRow.AppendChild(cell35);

            Cell cell36 = new Cell();
            cell36.DataType = CellValues.String;
            cell36.CellValue = new CellValue("Branch Name");
            headerRow.AppendChild(cell36);

            Cell cell37 = new Cell();
            cell37.DataType = CellValues.String;
            cell37.CellValue = new CellValue("IFSC Code");
            headerRow.AppendChild(cell37);

            Cell cell38 = new Cell();
            cell38.DataType = CellValues.String;
            cell38.CellValue = new CellValue("Account Number");
            headerRow.AppendChild(cell38);

            Cell cell39 = new Cell();
            cell39.DataType = CellValues.String;
            cell39.CellValue = new CellValue("Application Sanctionedunder Scheme Name");
            headerRow.AppendChild(cell39);

            Cell cell40 = new Cell();
            cell40.DataType = CellValues.String;
            cell40.CellValue = new CellValue("Current Task");
            headerRow.AppendChild(cell40);

            Cell cell41 = new Cell();
            cell41.DataType = CellValues.String;
            cell41.CellValue = new CellValue("Current Status");
            headerRow.AppendChild(cell41);

            Cell cell42 = new Cell();
            cell42.DataType = CellValues.String;
            cell42.CellValue = new CellValue("Last Task");
            headerRow.AppendChild(cell42);

            Cell cell43 = new Cell();
            cell43.DataType = CellValues.String;
            cell43.CellValue = new CellValue("Version No");
            headerRow.AppendChild(cell43);

            Cell cell44 = new Cell();
            cell44.DataType = CellValues.String;
            cell44.CellValue = new CellValue("Last Pay Date");
            headerRow.AppendChild(cell44);

            Cell cell45 = new Cell();
            cell45.DataType = CellValues.String;
            cell45.CellValue = new CellValue("Application Approve On");
            headerRow.AppendChild(cell45);


            sheetData.AppendChild(headerRow);
            foreach (var item in result)
            {
              Row dataRow = new Row();

              Cell cellR0 = new Cell();
              cellR0.DataType = CellValues.String;
              cellR0.CellValue = new CellValue(item.SNo + "");
              dataRow.AppendChild(cellR0);

              Cell cellR1 = new Cell();
              cellR1.DataType = CellValues.String;
              cellR1.CellValue = new CellValue(item.ApplicationReferenceNo + "");
              dataRow.AppendChild(cellR1);

              Cell cellR2 = new Cell();
              cellR2.DataType = CellValues.String;
              cellR2.CellValue = new CellValue(item.SubmissionLocation + "");
              dataRow.AppendChild(cellR2);

              Cell cellR3 = new Cell();
              cellR3.DataType = CellValues.String;
              cellR3.CellValue = new CellValue(item.SubmissionDate + "");
              dataRow.AppendChild(cellR3);

              Cell cellR4 = new Cell();
              cellR4.DataType = CellValues.String;
              cellR4.CellValue = new CellValue(item.AppliedBy + "");
              dataRow.AppendChild(cellR4);

              Cell cellR5 = new Cell();
              cellR5.DataType = CellValues.String;
              cellR5.CellValue = new CellValue(item.SelectTehsilSocialWelfareOffice_TSWO + "");
              dataRow.AppendChild(cellR5);

              Cell cellR6 = new Cell();
              cellR6.DataType = CellValues.String;
              cellR6.CellValue = new CellValue(item.SelectDistrict + "");
              dataRow.AppendChild(cellR6);

              Cell cellR7 = new Cell();
              cellR7.DataType = CellValues.String;
              cellR7.CellValue = new CellValue(item.NameOfTheApplicant + "");
              dataRow.AppendChild(cellR7);

              Cell cellR8 = new Cell();
              cellR8.DataType = CellValues.String;
              cellR8.CellValue = new CellValue(item.DateOfBirth + "");
              dataRow.AppendChild(cellR8);

              Cell cellR9 = new Cell();
              cellR9.DataType = CellValues.String;
              cellR9.CellValue = new CellValue(item.Age_InYears + "");
              dataRow.AppendChild(cellR9);

              Cell cellR10 = new Cell();
              cellR10.DataType = CellValues.String;
              cellR10.CellValue = new CellValue(item.MobileNumber + "");
              dataRow.AppendChild(cellR10);

              Cell cellR11 = new Cell();
              cellR11.DataType = CellValues.String;
              cellR11.CellValue = new CellValue(item.DoYouHaveBPLcard + "");
              dataRow.AppendChild(cellR11);

              Cell cellR12 = new Cell();
              cellR12.DataType = CellValues.String;
              cellR12.CellValue = new CellValue(item.FatherOrHusbandOrGuardianName + "");
              dataRow.AppendChild(cellR12);

              Cell cellR13 = new Cell();
              cellR13.DataType = CellValues.String;
              cellR13.CellValue = new CellValue(item.EMail + "");
              dataRow.AppendChild(cellR13);

              Cell cellR14 = new Cell();
              cellR14.DataType = CellValues.String;
              cellR14.CellValue = new CellValue(item.Category + "");
              dataRow.AppendChild(cellR14);

              Cell cellR15 = new Cell();
              cellR15.DataType = CellValues.String;
              cellR15.CellValue = new CellValue(item.Gender + "");
              dataRow.AppendChild(cellR15);

              Cell cellR16 = new Cell();
              cellR16.DataType = CellValues.String;
              cellR16.CellValue = new CellValue(item.PresentAddress + "");
              dataRow.AppendChild(cellR16);

              Cell cellR17 = new Cell();
              cellR17.DataType = CellValues.String;
              cellR17.CellValue = new CellValue(item.PresentDistrict + "");
              dataRow.AppendChild(cellR17);

              Cell cellR18 = new Cell();
              cellR18.DataType = CellValues.String;
              cellR18.CellValue = new CellValue(item.PresentVillageName + "");
              dataRow.AppendChild(cellR18);

              Cell cellR19 = new Cell();
              cellR19.DataType = CellValues.String;
              cellR19.CellValue = new CellValue(item.Pincode + "");
              dataRow.AppendChild(cellR19);

              Cell cellR20 = new Cell();
              cellR20.DataType = CellValues.String;
              cellR20.CellValue = new CellValue(item.PresentHalqaPanchayatOrMunicipalityName + "");
              dataRow.AppendChild(cellR20);

              Cell cellR21 = new Cell();
              cellR21.DataType = CellValues.String;
              cellR21.CellValue = new CellValue(item.PresentTehsil + "");
              dataRow.AppendChild(cellR21);

              Cell cellR22 = new Cell();
              cellR22.DataType = CellValues.String;
              cellR22.CellValue = new CellValue(item.PermanentAddress + "");
              dataRow.AppendChild(cellR22);

              Cell cellR23 = new Cell();
              cellR23.DataType = CellValues.String;
              cellR23.CellValue = new CellValue(item.PermanentDistrict + "");
              dataRow.AppendChild(cellR23);

              Cell cellR24 = new Cell();
              cellR24.DataType = CellValues.String;
              cellR24.CellValue = new CellValue(item.PermanentTehsil + "");
              dataRow.AppendChild(cellR24);

              Cell cellR25 = new Cell();
              cellR25.DataType = CellValues.String;
              cellR25.CellValue = new CellValue(item.PermanentHalqaPanchayatOrMunicipalityName + "");
              dataRow.AppendChild(cellR25);

              Cell cellR26 = new Cell();
              cellR26.DataType = CellValues.String;
              cellR26.CellValue = new CellValue(item.PermanentVillageName + "");
              dataRow.AppendChild(cellR26);

              Cell cellR27 = new Cell();
              cellR27.DataType = CellValues.String;
              cellR27.CellValue = new CellValue(item.BranchName + "");
              dataRow.AppendChild(cellR27);

              Cell cellR28 = new Cell();
              cellR28.DataType = CellValues.String;
              cellR28.CellValue = new CellValue(item.IFSCCode + "");
              dataRow.AppendChild(cellR28);

              Cell cellR29 = new Cell();
              cellR29.DataType = CellValues.String;
              cellR29.CellValue = new CellValue(item.AccountNoOfTheApplicant + "");
              dataRow.AppendChild(cellR29);

              Cell cellR30 = new Cell();
              cellR30.DataType = CellValues.String;
              cellR30.CellValue = new CellValue(item.BankName + "");
              dataRow.AppendChild(cellR30);

              Cell cellR31 = new Cell();
              cellR31.DataType = CellValues.String;
              cellR31.CellValue = new CellValue(item.SelectPensionType + "");
              dataRow.AppendChild(cellR31);

              Cell cellR32 = new Cell();
              cellR32.DataType = CellValues.String;
              cellR32.CellValue = new CellValue(item.PercentageofDisability + "");
              dataRow.AppendChild(cellR32);

              Cell cellR33 = new Cell();
              cellR33.DataType = CellValues.String;
              cellR33.CellValue = new CellValue(item.CivilCondition + "");
              dataRow.AppendChild(cellR33);

              Cell cellR34 = new Cell();
              cellR34.DataType = CellValues.String;
              cellR34.CellValue = new CellValue(item.AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP + "");
              dataRow.AppendChild(cellR34);

              Cell cellR35 = new Cell();
              cellR35.DataType = CellValues.String;
              cellR35.CellValue = new CellValue(item.BankName1 + "");
              dataRow.AppendChild(cellR35);

              Cell cellR36 = new Cell();
              cellR36.DataType = CellValues.String;
              cellR36.CellValue = new CellValue(item.BranchName1 + "");
              dataRow.AppendChild(cellR36);

              Cell cellR37 = new Cell();
              cellR37.DataType = CellValues.String;
              cellR37.CellValue = new CellValue(item.IFSCCode1 + "");
              dataRow.AppendChild(cellR37);

              Cell cellR38 = new Cell();
              cellR38.DataType = CellValues.String;
              cellR38.CellValue = new CellValue(item.AccountNumber + "");
              dataRow.AppendChild(cellR38);

              Cell cellR39 = new Cell();
              cellR39.DataType = CellValues.String;
              cellR39.CellValue = new CellValue(item.ApplicationSanctionedunderSchemeName + "");
              dataRow.AppendChild(cellR39);

              Cell cellR40 = new Cell();
              cellR40.DataType = CellValues.String;
              cellR40.CellValue = new CellValue(item.CurrentTask + "");
              dataRow.AppendChild(cellR40);

              Cell cellR41 = new Cell();
              cellR41.DataType = CellValues.String;
              cellR41.CellValue = new CellValue(item.CurrentStatus + "");
              dataRow.AppendChild(cellR41);

              Cell cellR42 = new Cell();
              cellR42.DataType = CellValues.String;
              cellR42.CellValue = new CellValue(item.LastTask + "");
              dataRow.AppendChild(cellR42);

              Cell cellR43 = new Cell();
              cellR43.DataType = CellValues.String;
              cellR43.CellValue = new CellValue(item.VersionNo + "");
              dataRow.AppendChild(cellR43);

              Cell cellR44 = new Cell();
              cellR44.DataType = CellValues.String;
              cellR44.CellValue = new CellValue(item.Last_pay_date + "");
              dataRow.AppendChild(cellR44);

              Cell cellR45 = new Cell();
              cellR45.DataType = CellValues.String;
              cellR45.CellValue = new CellValue(item.Application_approve_on + "");
              dataRow.AppendChild(cellR45);


              sheetData.AppendChild(dataRow);
            }
            workbookPart.Workbook.Save();
          }
          bytesdata = File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, "Beneficiary Details.xlsx");
        }
        return bytesdata;
      }
      catch (Exception ex)
      {

        throw;
      }
    }

    // **** Code By Himanshu Rajput ****
    public async Task<ActionResult> UploadExcelAjaxHandler(JQueryDataTableParamModel param, string Tehsil = "", string SchemeType = "", string Gender = "", string RegionNames = "", string ACCOUNT_STATUS = "", bool isDownload = false)
    {
      try
      {
        List<MasterBeneficiariesDetails> listSearch = new List<MasterBeneficiariesDetails>();
        List<MasterBeneficiariesDetails> dta = new List<MasterBeneficiariesDetails>();

        if (!string.IsNullOrEmpty(ACCOUNT_STATUS))
        {
          var Empcode = await db.MasterEmpBankDetails.Where(x => (x.ACCOUNT_STATUS == "fail" && x.IsUpload == true) || (x.ACCOUNT_STATUS == null)).Select(p => p.empl_code).ToListAsync();

          var intEmpCodes = Empcode.Select(code => Convert.ToInt32(code)).ToList();

          dta = await db.MasterBeneficiariesDetails.Where(d => RegionNames.ToLower().Contains(d.SelectDistrict.ToLower())).OrderByDescending(x => x.SNo)
          .Where(x => x.IsActive == true && intEmpCodes.Contains(x.Id)).ToListAsync();
        }
        else
        {
          dta = await db.MasterBeneficiariesDetails.Where(d => RegionNames.ToLower().Contains(d.SelectDistrict.ToLower())).OrderByDescending(x => x.SNo)
          .Where(x => x.IsActive == true).ToListAsync();
        }

        //var dta = db.MasterBeneficiariesDetails.ToList();

        if (Gender != "" && Gender != null && Gender != "ALL")
        {
          dta = dta.Where(p => p.Gender.ToLower() == Gender.ToLower()).ToList();
        }

        if (SchemeType != "" && SchemeType != null && SchemeType != "ALL")
        {
          dta = dta.Where(p => p.SelectPensionType.ToLower() == SchemeType.ToLower()).ToList();
        }

        if (dta != null && dta.Any())
        {
          if (isDownload)
          {
            param.iDisplayLength = 1000000;
            FileContentResult bytesdata = JandKBeneficiaryReport(dta);
            return bytesdata;
          }
        }

        Int32 totalRecords = listSearch.Count > 0 ? Convert.ToInt32(listSearch.Select(x => x.SNo).FirstOrDefault()) : 0;

        var result = from x in listSearch
                     select new[]
                     {
                        x.SNo + "",//0
                        x.ApplicationReferenceNo + "",//1
                        x.SubmissionLocation + "",//2
                        x.SubmissionDate + "",//3
                        x.AppliedBy,//4
                        x.SelectTehsilSocialWelfareOffice_TSWO,//5
                        x.SelectDistrict,//6
                        x.NameOfTheApplicant,//7
                        x.DateOfBirth,//8
                        x.Age_InYears,//9
                        x.MobileNumber,//x.LastVerified,//10
                        x.DoYouHaveBPLcard,//11
                        x.FatherOrHusbandOrGuardianName + "",//12
                        x.EMail + "",//13
                        x.Category + "", //  14                    
                        x.Gender + "", //  15
                        x.PresentAddress+"",//16
                        x.PresentDistrict+"",//17
                        x.PresentVillageName+"",//18
                        x.Pincode+"",//19
                        x.PresentHalqaPanchayatOrMunicipalityName+"",//20
                        x.PresentTehsil+"",//21
                        x.PermanentAddress+"",//22
                        x.PermanentDistrict+"",//23
                        x.PermanentTehsil+"",//24
                        x.PermanentHalqaPanchayatOrMunicipalityName+"",//25
                        x.PermanentVillageName+"",//26
                        x.BranchName+"",//27
                        x.IFSCCode+"",//28
                        x.AccountNoOfTheApplicant+"",//29
                        x.BankName+"",//30
                        x.SelectPensionType+"",//31
                        x.PercentageofDisability+"",//32
                        x.CivilCondition+"",//33
                        x.AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP+"",//34
                        x.BankName1+"",//35
                        x.BranchName1+"",//36
                        x.IFSCCode1+"",//37
                        x.AccountNumber+"",//38
                        x.ApplicationSanctionedunderSchemeName+"",//39
                        x.CurrentTask+"",//40
                        x.CurrentStatus+"",//41
                        x.LastTask+"",//42
                        x.VersionNo+"",//43
                        x.Last_pay_date+"",//44
                        x.Application_approve_on+"",//45
                        x.ActionOnDate+"",//46
                        
                        



 
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

    public ActionResult DetailContributionSearchOnDateAjax(JQueryDataTableParamModel param, string From, string To)
    {
      try
      {
        DataSet ds = new DataSet();
        StringBuilder SQL = new StringBuilder();
        SQL.Append("SELECT  * from MasterBeneficiaries E  ");
        if (!string.IsNullOrEmpty(From))
          SQL.Append("where cast(E.CreatedOn as date) >= '" + From + "'");
        SQL.Append(" and cast(E.CreatedOn as date) <= '" + To + "'");

        //string con = WebConfigurationManager.AppSettings["SQLConn"];
        string con = ConnectionStringProvider.GetConnectionString();
        SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), con);
        da.Fill(ds);

        List<MasterBeneficiaries> LogsList = new List<MasterBeneficiaries>();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          MasterBeneficiaries MasterBeneficiaries = new MasterBeneficiaries();

          MasterBeneficiaries.Id = Convert.ToInt32(dr["Id"]);
          MasterBeneficiaries.Finalized = Convert.ToInt32(dr["Finalized"]);
          MasterBeneficiaries.IsProcessed = Convert.ToBoolean(dr["IsProcessed"]);
          MasterBeneficiaries.FilePath = dr["FilePath"].ToString();
          MasterBeneficiaries.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);

          LogsList.Add(MasterBeneficiaries);
        }


        IEnumerable<MasterBeneficiaries> filtered;

        if (!string.IsNullOrEmpty(param.sSearch))
        {
          filtered = LogsList
             .Where(c => c.Id.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.Finalized.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.IsProcessed.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.FilePath.ToString().ToLower().Contains(param.sSearch.ToLower())
             || c.CreatedOn.ToString().ToLower().Contains(param.sSearch.ToLower())

             );

        }
        else
        {
          filtered = LogsList;
        }
        //Sorting through column index
        var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

        Func<MasterBeneficiaries, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Id + "" :
                                                              sortColumnIndex == 1 ? c.Finalized + "" :
                                                              sortColumnIndex == 2 ? c.IsProcessed + "" :
                                                              sortColumnIndex == 3 ? c.FilePath + "" :
                                                              sortColumnIndex == 4 ? c.CreatedOn + "" : "");

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
                  c.Id + "",
                   Path.GetFileName(c.FilePath) + "",
                  c.Finalized + "",
                  c.CreatedOn+"",
                  //c.IsActive +"",
                   c.IsProcessed + "",
            };

        return Json(
                new
                {
                  sEcho = param.sEcho,
                  iTotalRecords = LogsList.Count(),
                  iTotalDisplayRecords = filtered.Count(),
                  aaData = result
                },
      JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        throw;
      }
    }


  }

}



