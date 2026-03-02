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
using System.Threading.Tasks;
using App.Web.Filters;

namespace App.Web.Controllers
{
  [AuthorizeEx()]
  public class ContributorResigneeController : BaseController
  {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;
        private AppDbContext _DbContext;

        public ContributorResigneeController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _DbContext = DbContext;
        }

        // GET: MasterContributors
        public ActionResult Index(int? Id, int? status)
    {
      if (Id == null)
        Id = 2;
      if (status == null)
        status = 4;
      var employer = db.MasterEmployer.Where(x => x.IsActive == true).Select(x => new { Id = x.Id, EmployerName = x.EmployerName }).OrderBy(x => x.EmployerName).ToList();
      employer.Insert(0, new { Id = 0, EmployerName = "All" });
      var allowedRejoins = new[] { new { Id = 3, Name = "Pensioner" }, new { Id = 4, Name = "Resignee" }, new { Id = 5, Name = "Supernumerary" }
                                , new { Id = 6, Name = "Treasury Unknowns" }, new { Id = 7, Name = "Unknown" }};
      ViewBag.Status = new SelectList(allowedRejoins, "Id", "Name", status);
      ViewBag.EmployerName = new SelectList(employer, "Id", "EmployerName", Id);
      return View();
    }
    public ActionResult ContributorPersonalDetailsAjaxHandler(App.Web.Models.JQueryDataTableParamModel param, int? Id, int? status)
    {
      var List = db.MasterContributor.Where(x => x.IsActive == true && x.JobStatusID == status && x.EmployerID == (Id == 0 ? x.EmployerID : Id)).ToList();
      //var List = db.MasterContributor.Where(x => x.IsActive == true && x.JobStatusID == status && x.EmployerID==Id) join emp in db.MasterEmployer.Where(x=>x.IsActive==true) on
      //            cont.EmployerID equals emp.Id join emptype in db.MasterEmployerType.Where(x=>x.IsActive==true && x.Id==Id) on emp.EmployerTypeID equals emptype.Id 
      //                where DbFunctions.AddYears(cont.DateOfBirth.Value,emptype.RetirementAgeAfter2004)>DateTime.Now
      //                select cont).ToList();
      IEnumerable<MasterContributor> filtered;
      //if (!string.IsNullOrEmpty(Id.ToString()) && Id != 0)
      //{
      //    List = List.Where(x => x.EmployerID == Id);
      //}
      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => c.PersonID.ToLower().Contains(param.sSearch.ToLower())
           || (c.OldPersonID + "").ToLower().Contains(param.sSearch.ToLower())
           || (c.FirstName + " " + c.MidName + " " + c.LastName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower())
           || (c.Employer.EmployerName + "").ToLower().Contains(param.sSearch.ToLower())
           || (c.SocialSecurityNo + "").ToLower().Contains(param.sSearch.ToLower())
           || (c.DateOfBirth + "").ToLower().Contains(param.sSearch.ToLower())
           || (c.PostalAddress + "").ToLower().Contains(param.sSearch.ToLower())
           || (c.PhoneOffice + "").ToLower().Contains(param.sSearch.ToLower())
           || (c.Status.Name + "").ToLower().Contains(param.sSearch.ToLower()));

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
                     c.ProfilePath,
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
                     (db.MasterContributor.Where(x => x.OldPersonID == c.PersonID).Any()?"Rejoined":c.Status.Name),
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

    public ActionResult AddRejoin(int? id)
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
      //int AddressStateId = (from s in db.City where s.Id == contributorPersonalDetails.CityID select s.StateId).Single();
      //int AddressCountryId = (from s in db.State where s.Id == AddressStateId select s.CountryId).Single();
      //int PermAddStateId = (from s in db.MasterCity where s.Id == contributorPersonalDetails.PermanentCityID select s.StateId).Single();
      //int PermAddCountryId = (from s in db.MasterState where s.Id == PermAddStateId select s.CountryId).Single();
      //ViewBag.JobStatusID = new SelectList(db.MasterStatus.Where(x => x.IsActive == true), "Id", "Name", contributorPersonalDetails.JobStatusID);
      //ViewBag.AddressStateId = AddressStateId;
      //ViewBag.AddressCountryId = AddressCountryId;
      //ViewBag.PermAddStateId = PermAddStateId;
      //ViewBag.PermAddCountryId = PermAddCountryId;

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
      var list = db.MasterPFRate.Where(x => x.IsActive == true).Where(x => x.PfType == "C").OrderBy(x => x.Name).ToList().Select(x => new { Id = x.Id, Value = string.Format("{0} - ({1} - {2})", x.Name, "PFRate @" + x.PFRate, " Effective Date :" + x.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + x.EffectiveEndDate.ToString("dd MMMM, yyyy")) }).ToList();
      ViewBag.PFRateID = new SelectList(list, "Id", "Value", contributorPersonalDetails.PFRateID);
      //ViewBag.PFRateID = new SelectList(db.MasterPFRate.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", contributorPersonalDetails.PFRateID);
      ViewBag.PFRate = contributorPersonalDetails.PFRate;

      return View(contributorPersonalDetails);

    }

    [HttpPost]
    public async Task<ActionResult> AddRejoin(MasterContributor contributorPersonalDetails, HttpPostedFileBase[] NewfileUploadProfile, string Rejoined)
    {
      if (ModelState.IsValid)
      {
        DateTime _rejoined = Convert.ToDateTime(Rejoined);
        bool _CanRejoin = (from cont in db.MasterContributor.Where(x => x.IsActive == true && x.Id == contributorPersonalDetails.Id)
                           join emp in db.MasterEmployer.Where(x => x.IsActive == true && x.Id == contributorPersonalDetails.EmployerID) on
                               cont.EmployerID equals emp.Id
                           join emptype in db.MasterEmployerType.Where(x => x.IsActive == true) on emp.EmployerTypeID equals emptype.Id
                           where DbFunctions.AddYears(cont.DateOfBirth.Value, emptype.RetirementAgeAfter2004) > _rejoined
                           select cont).Any();
        if (_CanRejoin)
        {
          //#region Ankit code for in-active old contributor data when contributor re-join
          //var oldContributorEntity = db.MasterContributor.Find(contributorPersonalDetails.Id);
          //oldContributorEntity.IsActive = false;
          //db.Entry(oldContributorEntity).State = EntityState.Modified;
          //db.SaveChanges();
          //#endregion

          int tempId = db.MasterContributor.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();

          contributorPersonalDetails.SalaryAmount = 0;
          contributorPersonalDetails.FirstAppointmentDate = null;
          contributorPersonalDetails.LastAppointmentDate = null;
          contributorPersonalDetails.ExpectedRetirementDate = null;
          contributorPersonalDetails.RetirementOrResignationDate = null;
          contributorPersonalDetails.JobStatusID = 1;
          contributorPersonalDetails.MonthlySalary = 0;
          contributorPersonalDetails.AnnualAmount = 0;
          contributorPersonalDetails.Balance = 0;
          contributorPersonalDetails.QuarterlyAmount = 0;
          contributorPersonalDetails.OldPersonID = contributorPersonalDetails.PersonID;
          contributorPersonalDetails.PersonID = "PF00" + (tempId + 1);
          db.Entry(contributorPersonalDetails).State = EntityState.Added;
          int result = db.SaveChanges();
          if (result > 0)
          {
            var marriageModel = db.MasterContributorMarriageDetails.Where(x => x.PersonId == contributorPersonalDetails.OldPersonID).ToList();
            if (marriageModel != null && marriageModel.Count() > 0)
            {
              foreach (var marriage in marriageModel)
              {
                MasterContributorMarriageDetails marriageUpdateModel = db.MasterContributorMarriageDetails.Find(marriage.Id);
                marriageUpdateModel.PersonId = contributorPersonalDetails.PersonID;
                db.Entry(marriageUpdateModel).State = EntityState.Added;
                result = db.SaveChanges();
              }
            }
            var dependantModel = db.MasterDependantDetails.Where(x => x.PersonID == contributorPersonalDetails.OldPersonID).ToList();
            if (dependantModel != null && dependantModel.Count() > 0)
            {
              foreach (var dependant in dependantModel)
              {
                MasterDependantDetails dependantUpdateModel = db.MasterDependantDetails.Find(dependant.Id);
                dependantUpdateModel.PersonID = contributorPersonalDetails.PersonID;
                db.Entry(dependantUpdateModel).State = EntityState.Added;
                result = db.SaveChanges();
              }
            }
            if (NewfileUploadProfile != null && NewfileUploadProfile.Length > 0)
            {
              await UpdateProfileImage(contributorPersonalDetails, NewfileUploadProfile);

            }
            return RedirectToAction("AddJobDetails", "ContributorPersonalDetails", new { id = contributorPersonalDetails.Id });
          }
        }
        else
          ModelState.AddModelError("Not Eligible", "Date of Rejoin Exceed the Max Retirement Age");
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
      //ViewBag.PFRateID = new SelectList(db.MasterPFRate.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", contributorPersonalDetails.PFRateID);
      ViewBag.PFRate = contributorPersonalDetails.PFRate;
      var list = db.MasterPFRate.Where(x => x.IsActive == true).Where(x => x.PfType == "C").OrderBy(x => x.Name).ToList().Select(x => new { Id = x.Id, Value = string.Format("{0} - ({1} - {2})", x.Name, "PFRate @" + x.PFRate, " Effective Date :" + x.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + x.EffectiveEndDate.ToString("dd MMMM, yyyy")) }).ToList();
      ViewBag.PFRateID = new SelectList(list, "Id", "Value", contributorPersonalDetails.PFRateID);
      return View(contributorPersonalDetails);
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
            var entity = await db.MasterContributor.FirstOrDefaultAsync(x => x.Id == model.Id);
            entity.ProfilePath = fileName;
            db.Entry(entity).State = EntityState.Modified;
            result = await db.SaveChangesAsync();

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

    // GET: MasterContributors/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterContributor masterContributor = db.MasterContributor.Find(id);
      if (masterContributor == null)
      {
        return HttpNotFound();
      }
      return View(masterContributor);
    }

    // GET: MasterContributors/Create
    public ActionResult Create()
    {
      ViewBag.CountryID = new SelectList(db.MasterCountry, "Id", "CountryCode");
      ViewBag.EmployerID = new SelectList(db.MasterEmployer, "Id", "UniqueID");
      ViewBag.PFRateID = new SelectList(db.MasterPFRate, "Id", "Name");
      ViewBag.NationalityID = new SelectList(db.MasterNationality, "Id", "Name");
      ViewBag.PermanentCityID = new SelectList(db.MasterCity, "Id", "Name");
      ViewBag.PrefixId = new SelectList(db.MasterPrefix, "Id", "Name");
      ViewBag.JobStatusID = new SelectList(db.MasterStatus, "Id", "Name");
      ViewBag.SuffixId = new SelectList(db.MasterSuffix, "Id", "Name");
      return View();
    }

    // POST: MasterContributors/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,PersonID,OldPersonID,EmployerID,SocialSecurityNo,PrefixId,SuffixId,FirstName,MidName,LastName,MaidenName,Gender,DateOfBirth,ExpectedRetirementDate,CountryID,NationalityID,PermanentAddress,PermanentCityID,PostalAddress,Phone,PhoneOffice,PhoneOfficeExt,Mobile,Email,SalaryAmount,MonthlySalary,PFRateID,PFRate,FirstAppointmentDate,LastAppointmentDate,RetirementOrResignationDate,Reason,Balance,QuarterlyAmount,AnnualAmount,JobStatusID,ProfilePath,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterContributor masterContributor)
    {
      if (ModelState.IsValid)
      {
        db.MasterContributor.Add(masterContributor);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      ViewBag.CountryID = new SelectList(db.MasterCountry, "Id", "CountryCode", masterContributor.CountryID);
      ViewBag.EmployerID = new SelectList(db.MasterEmployer, "Id", "UniqueID", masterContributor.EmployerID);
      ViewBag.PFRateID = new SelectList(db.MasterPFRate, "Id", "Name", masterContributor.PFRateID);
      ViewBag.NationalityID = new SelectList(db.MasterNationality, "Id", "Name", masterContributor.NationalityID);
      ViewBag.PermanentCityID = new SelectList(db.MasterCity, "Id", "Name", masterContributor.PermanentCityID);
      ViewBag.PrefixId = new SelectList(db.MasterPrefix, "Id", "Name", masterContributor.PrefixId);
      ViewBag.JobStatusID = new SelectList(db.MasterStatus, "Id", "Name", masterContributor.JobStatusID);
      ViewBag.SuffixId = new SelectList(db.MasterSuffix, "Id", "Name", masterContributor.SuffixId);
      return View(masterContributor);
    }

    // GET: MasterContributors/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterContributor masterContributor = db.MasterContributor.Find(id);
      if (masterContributor == null)
      {
        return HttpNotFound();
      }
      ViewBag.CountryID = new SelectList(db.MasterCountry, "Id", "CountryCode", masterContributor.CountryID);
      ViewBag.EmployerID = new SelectList(db.MasterEmployer, "Id", "UniqueID", masterContributor.EmployerID);
      ViewBag.PFRateID = new SelectList(db.MasterPFRate, "Id", "Name", masterContributor.PFRateID);
      ViewBag.NationalityID = new SelectList(db.MasterNationality, "Id", "Name", masterContributor.NationalityID);
      ViewBag.PermanentCityID = new SelectList(db.MasterCity, "Id", "Name", masterContributor.PermanentCityID);
      ViewBag.PrefixId = new SelectList(db.MasterPrefix, "Id", "Name", masterContributor.PrefixId);
      ViewBag.JobStatusID = new SelectList(db.MasterStatus, "Id", "Name", masterContributor.JobStatusID);
      ViewBag.SuffixId = new SelectList(db.MasterSuffix, "Id", "Name", masterContributor.SuffixId);
      return View(masterContributor);
    }

    // POST: MasterContributors/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,PersonID,OldPersonID,EmployerID,SocialSecurityNo,PrefixId,SuffixId,FirstName,MidName,LastName,MaidenName,Gender,DateOfBirth,ExpectedRetirementDate,CountryID,NationalityID,PermanentAddress,PermanentCityID,PostalAddress,Phone,PhoneOffice,PhoneOfficeExt,Mobile,Email,SalaryAmount,MonthlySalary,PFRateID,PFRate,FirstAppointmentDate,LastAppointmentDate,RetirementOrResignationDate,Reason,Balance,QuarterlyAmount,AnnualAmount,JobStatusID,ProfilePath,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive")] MasterContributor masterContributor)
    {
      if (ModelState.IsValid)
      {
        db.Entry(masterContributor).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.CountryID = new SelectList(db.MasterCountry, "Id", "CountryCode", masterContributor.CountryID);
      ViewBag.EmployerID = new SelectList(db.MasterEmployer, "Id", "UniqueID", masterContributor.EmployerID);
      ViewBag.PFRateID = new SelectList(db.MasterPFRate, "Id", "Name", masterContributor.PFRateID);
      ViewBag.NationalityID = new SelectList(db.MasterNationality, "Id", "Name", masterContributor.NationalityID);
      ViewBag.PermanentCityID = new SelectList(db.MasterCity, "Id", "Name", masterContributor.PermanentCityID);
      ViewBag.PrefixId = new SelectList(db.MasterPrefix, "Id", "Name", masterContributor.PrefixId);
      ViewBag.JobStatusID = new SelectList(db.MasterStatus, "Id", "Name", masterContributor.JobStatusID);
      ViewBag.SuffixId = new SelectList(db.MasterSuffix, "Id", "Name", masterContributor.SuffixId);
      return View(masterContributor);
    }

    // GET: MasterContributors/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MasterContributor masterContributor = db.MasterContributor.Find(id);
      if (masterContributor == null)
      {
        return HttpNotFound();
      }
      return View(masterContributor);
    }

    // POST: MasterContributors/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      MasterContributor masterContributor = db.MasterContributor.Find(id);
      db.MasterContributor.Remove(masterContributor);
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
  }
}
