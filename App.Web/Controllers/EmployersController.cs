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
using App.Data.ViewModels;
using App.Data.Extentions;
using App.Web.Filters;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class EmployersController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public EmployersController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // GET: Employers
        public ActionResult Index()
        {
            return View();
        }
        //public JsonResult Remove(string Id)
        //{
        //  int result = 0;
        //  if (Id != "0")
        //  {
        //    int id = Convert.ToInt32(Id);
        //    MasterEmployer entity = db.MasterEmployer.Where(x => x.Id == id).FirstOrDefault();
        //    entity.IsActive = false;
        //    db.Entry(entity).District = EntityState.Modified;
        //    db.SaveChanges();
        //    result = 1;
        //  }
        //  return Json(result, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            //var CurrentDate = DateTime.Now;
            // int monthEndDate = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            ////DateTime date = Convert.ToDateTime(CurrentDate.Month + "/" + monthEndDate + "/" + CurrentDate.Year);
            var CurrentDate = DateTime.Now;
            int monthEndDate = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            DateTime date = new DateTime(CurrentDate.Year, CurrentDate.Month, monthEndDate);

            //var List = db.MasterEmployer.Where(x => x.IsActive == true);
            var List = (from me in db.MasterEmployer.Where(x => x.IsActive == true)
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
                            IsActive = me.IsActive,
                        }).ToList();

            IEnumerable<EmployerViewModel> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.UniqueID.ToLower().Contains(param.sSearch.ToLower())
                || c.EmployerName.ToLower().Contains(param.sSearch.ToLower())
                   || c.EmployerAddress.ToLower().Contains(param.sSearch.ToLower())
                   //|| c.CityName.ToLower().Contains(param.sSearch.ToLower())
                   /*|| c.ZipCode.ToLower().Contains(param.sSearch.ToLower())*/
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
                                                                                            sortColumnIndex == 8 ? c.IsActive + "" :
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
                     c.IsActive + "",
                     c.Id + "",
                     c.IsExpired+"",
                     String.Format("{0:MM/dd/yyyy}", c.EffectiveStartDate),
                     String.Format("{0:MM/dd/yyyy}", c.EffectiveEndDate),
                     c.Id + "",
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
        // GET: Employers/Details/5
        //public ActionResult Details(int? id)
        //{
        //  if (id == null)
        //  {
        //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //  }
        //  MasterEmployer employer = db.MasterEmployer.Find(id);
        //  if (employer == null)
        //  {
        //    return HttpNotFound();
        //  }
        //  return View(employer);
        //}

        // GET: Employers/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", 7);
            ViewBag.CityID1 = new SelectList(db.MasterCity.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", 1);
            //List<MasterCity> city = new List<MasterCity>();
            //ViewBag.CityID = new SelectList(db.MasterCity.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", 7);// new SelectList(city, "Id", "Name");
            ViewBag.EmployerTypeID = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
            var list = db.MasterPFRate.Where(x => x.IsActive == true).Where(x => x.PfType == "Z").OrderBy(x => x.Name).ToList().Select(x => new { Id = x.Id, Value = string.Format("{0} - ({1} - {2})", x.Name, "PFRate @" + x.PFRate, " Effective Date :" + x.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + x.EffectiveEndDate.ToString("dd MMMM, yyyy")) }).ToList();
            //ViewBag.PFRateID = new SelectList(db.MasterPFRate.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
            //  ViewBag.PFRateID = new SelectList(list, "Id", "Value");
            ViewBag.PFRateID = new SelectList(db.MasterPFRate.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", 7);
            return View();
        }

        // POST: Employers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MasterEmployer model)
        {
            var city = new SelectList(db.MasterCity.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name");
            var result = db.MasterEmployer;
            if (!result.Where(x => x.UniqueID == model.UniqueID).Any())
            {
                if (!result.Where(x => x.EmployerName.ToLower() == model.EmployerName.ToLower() && x.EmployerAddress.ToLower() == model.EmployerAddress.ToLower() && x.Mobile == model.Mobile && x.IsActive == true).Any())
                {
                    if (ModelState.IsValid)
                    {
                        //var entity = new MasterEmployer();
                        //model.MapTo(entity);
                        model.CityID = model.CityID1;

                        db.MasterEmployer.Add(model);
                        int saveStatus = await db.SaveChangesAsync();
                        if (saveStatus > 0)
                        {
                            //save employer pf rate details in employer pf rate history rable
                            var MasterPfRate = db.MasterPFRate.FirstOrDefault(x => x.Id == model.PFRateID);
                            var MasterEmployerPfRateDetails = new MasterEmployerPFRateDetails();
                            MasterEmployerPfRateDetails.UniqueID = model.UniqueID;
                            MasterEmployerPfRateDetails.PFRateID = model.PFRateID;
                            MasterEmployerPfRateDetails.PFRate = model.PFRate;
                            MasterEmployerPfRateDetails.EffectiveDate = MasterPfRate.EffectiveDate;
                            MasterEmployerPfRateDetails.EffectiveEndDate = MasterPfRate.EffectiveEndDate;
                            MasterEmployerPfRateDetails.IsActive = true;
                            db.MasterEmployerPFRateDetails.Add(MasterEmployerPfRateDetails);
                            db.SaveChanges();
                            TempData["success"] = "Record Saved Successfully";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["error"] = "There is some error, please try again later";
                        }
                    }
                }
                else
                {
                    TempData["error"] = "Record Already Exist";
                }
            }
            else
            {
                TempData["error"] = "Please Refresh Page And Try Again...";
            }
            ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.CountryID);
            ViewBag.CityID = new SelectList(db.MasterCity.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.CityID);
            ViewBag.EmployerTypeID = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.EmployerTypeID);
            //ViewBag.PFRateID = new SelectList(db.MasterPFRate.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.PFRateID);
            var list = db.MasterPFRate.Where(x => x.IsActive == true).Where(x => x.PfType == "E").OrderBy(x => x.Name).ToList().Select(x => new { Id = x.Id, Value = string.Format("{0} - ({1} - {2})", x.Name, "PFRate @" + x.PFRate, " Effective Date :" + x.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + x.EffectiveEndDate.ToString("dd MMMM, yyyy")) }).ToList();
            ViewBag.PFRateID = new SelectList(list, "Id", "Value", model.PFRateID);
            return View(model);
        }

        // GET: Employers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterEmployer employer = db.MasterEmployer.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            var CurrentContributionDate = DateTime.Now.Date;
            //var employers = new MasterEmployer();
            //employer.MapTo(employers);
            //var CurrentDate = DateTime.Now;
            //int monthEndDate = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            //DateTime date = Convert.ToDateTime(CurrentDate.Month + "/" + monthEndDate + "/" + CurrentDate.Year);
            var CurrentDate = DateTime.Now;
            int monthEndDate = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            DateTime date = new DateTime(CurrentDate.Year, CurrentDate.Month, monthEndDate);


            ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", employer.CountryID);
            ViewBag.CityID = new SelectList(db.MasterCity.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", employer.CityID);
            ViewBag.EmployerTypeID = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", employer.EmployerTypeID);
            //ViewBag.PFRateID = new SelectList(db.MasterPFRate.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", employer.PFRateID);
            var list = db.MasterPFRate.Where(x => x.IsActive == true).Where(x => x.PfType == "Z").OrderBy(x => x.Name).ToList().Select(x => new { Id = x.Id, Value = string.Format("{0} - ({1} - {2})", x.Name, "PFRate @" + x.PFRate, " Effective Date :" + x.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + x.EffectiveEndDate.ToString("dd MMMM, yyyy")) }).ToList();
            ViewBag.PFRateID = new SelectList(list, "Id", "Value", employer.PFRateID);
            employer.OldPFRateID = employer.PFRateID;
            //var PfRate = db.MasterPFRate.FirstOrDefault(x => x.IsActive && x.Id == employer.PFRateID);
            //if (PfRate != null)
            //{
            //  employer.PFRate = PfRate.PFRate;
            //}
            if (db.MasterContributor.Any(x => x.EmployerID == id && x.IsActive == true))
            {
                ViewBag.EId = employer.EmployerTypeID;
            }
            //set list of Changed PF Rates from [MasterEmployerPFRateDetails] Table to ViewBag
            var ChangedPfRateList = (from me in db.MasterEmployerPFRateDetails
                                     join pf in db.MasterPFRate
                                     on me.PFRateID equals pf.Id
                                     where me.UniqueID == employer.UniqueID
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
            employer.ShowTable = false;
            if (ChangedPfRateList.Count() > 0)
            {
                employer.ShowTable = true;
            }
            return View(employer);
        }

        // POST: Employers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MasterEmployer model)
        {
            if (!db.MasterEmployer.Where(x => x.EmployerName == model.EmployerName && x.EmployerAddress == model.EmployerAddress && x.Mobile == model.Mobile && x.Id != model.Id && x.IsActive == true).Any())
            {
                if (ModelState.IsValid)
                {
                    //var entity = db.MasterEmployer.FirstOrDefault(x => x.Id == model.Id);
                    //model.MapTo(entity);

                    db.Entry(model).State = EntityState.Modified;
                    int result = await db.SaveChangesAsync();
                    if (result > 0)
                    {
                        //find if current pfrateid is available in history table

                        //code added to manage history of Employer Pf Details [Add corresponding entry in MasterEmployerPfRateDetails Table]
                        var MasterPfRate = db.MasterPFRate.FirstOrDefault(x => x.Id == model.PFRateID);
                        var AlreadyInHistory = db.MasterEmployerPFRateDetails.Where(x => x.PFRateID == model.PFRateID && x.UniqueID == model.UniqueID).FirstOrDefault();
                        if (AlreadyInHistory == null)
                        {
                            var MasterEmployerPfRateDetails = new MasterEmployerPFRateDetails();
                            MasterEmployerPfRateDetails.UniqueID = model.UniqueID;
                            MasterEmployerPfRateDetails.PFRateID = model.PFRateID;
                            MasterEmployerPfRateDetails.PFRate = MasterPfRate.PFRate;
                            MasterEmployerPfRateDetails.EffectiveDate = MasterPfRate.EffectiveDate;
                            MasterEmployerPfRateDetails.EffectiveEndDate = MasterPfRate.EffectiveEndDate;
                            MasterEmployerPfRateDetails.IsActive = true;
                            db.MasterEmployerPFRateDetails.Add(MasterEmployerPfRateDetails);
                            db.SaveChanges();
                        }
                        else
                        {
                            db.Entry(AlreadyInHistory).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        TempData["success"] = "Record Updated Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "There is some error, please try again later";
                    }
                }
            }
            else
            {
                TempData["error"] = "Record Already Exist";
            }
            ViewBag.CountryID = new SelectList(db.MasterCountry.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.CountryID);
            ViewBag.CityID = new SelectList(db.MasterCity.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.CityID);
            ViewBag.EmployerTypeID = new SelectList(db.MasterEmployerType.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.EmployerTypeID);
            //ViewBag.PFRateID = new SelectList(db.MasterPFRate.Where(x => x.IsActive == true).OrderBy(x => x.Name), "Id", "Name", model.PFRateID);
            var list = db.MasterPFRate.Where(x => x.IsActive == true).Where(x => x.PfType == "E").OrderBy(x => x.Name).ToList().Select(x => new { Id = x.Id, Value = string.Format("{0} - ({1} - {2})", x.Name, "PFRate @" + x.PFRate, " Effective Date :" + x.EffectiveDate.ToString("dd MMMM, yyyy") + " - " + x.EffectiveEndDate.ToString("dd MMMM, yyyy")) }).ToList();
            ViewBag.PFRateID = new SelectList(list, "Id", "Value", model.PFRateID);
            return View(model);
        }

        // GET: Employers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterEmployer employer = db.MasterEmployer.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            return View(employer);
        }

        // POST: Employers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterEmployer employer = db.MasterEmployer.Find(id);
            db.MasterEmployer.Remove(employer);
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

        ////
        public JsonResult PFAjax(String Id)
        {
            int id = Convert.ToInt32(Id);
            var pfrate = db.MasterPFRate.Where(x => x.Id == id).Select(x => x.PFRate).FirstOrDefault();
            return Json(pfrate, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CityAjax(String Id)
        {
            int countryId = Id == null ? 0 : Convert.ToInt32(Id);
            var result = from a in db.MasterState join b in db.MasterCity on a.Id equals b.DistrictId where (a.IsActive == true && b.IsActive == true && a.CountryId == countryId) orderby b.Name select new { Value = b.Id, Text = b.Name };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EmployerNameAjax(String EmployerName, string EmployerAddress, string Mobile)
        {
            var result = db.MasterEmployer.Where(x => x.EmployerName == EmployerName && x.EmployerAddress == EmployerAddress && x.Mobile == Mobile && x.IsActive == true).Count();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UniqueIDAjax()
        {
            string result = "";
            int PersonID = db.MasterEmployer.OrderByDescending(x => x.Id).Select(x => x == null ? 0 : x.Id).FirstOrDefault();
            if (PersonID != 0)
            {

                result = "E00" + (PersonID + 1);
            }
            else
            {
                result = "E001";
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}
