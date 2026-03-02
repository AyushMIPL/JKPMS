using App.Data;
using App.Data.Entities;
using App.Web.Filters;
using App.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    [AuthorizeEx()]
    public class MasterDeductionCodeController : BaseController
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;
        private AppDbContext _DbContext;

        public MasterDeductionCodeController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            _DbContext = DbContext;
        }
        // GET: MasterDeductionCode
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var dedType = from x in deductiontype() select new { Id = x.Value, Name = x.Key };
            List<MasterDedcodes> List = db.MasterDedcodes.ToList();
            IEnumerable<MasterDedcodes> filtered;


            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filtered = List
                   .Where(c => c.ded_code.ToLower().Contains(param.sSearch.ToLower())
                   || c.description.ToLower().Contains(param.sSearch.ToLower())
                   || c.ded_type.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filtered = List;
            }

            //Sorting through column index
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<MasterDedcodes, string> orderingFunction = (c => sortColumnIndex == 0 ? c.ded_code :
                                                                                            sortColumnIndex == 1 ? c.description :
                                                                                            sortColumnIndex == 2 ? c.ded_type :
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

                         c.ded_code,
                                         c.description,
                     dedType.Where(x=>x.Id+""==c.ded_type).Select(x=>x.Name).FirstOrDefault(),
                     c.ded_code_id+""
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
        public ActionResult Create()
        {
            var dedType = from x in deductiontype() select new { Id = x.Value, Name = x.Key };
            ViewBag.ded_type = new SelectList(dedType, "Id", "Name");
            string[] taxStatus = { "F", "T", "U", "N", "A", "B", "C", "D" };
            ViewBag.ded_taxred = new SelectList(taxStatus);
            var frequency = from x in deductionFrequency() select new { Id = x.Value, Name = x.Key };
            ViewBag.dflt_apply = new SelectList(frequency, "Id", "Name");
            string[] defaultAccount = { "BELLIN", "EXPENS" };
            ViewBag.GLAccount = new SelectList(defaultAccount);
            var GLAccount = db.PayrollGLAccounts.Select(x => new { Id = x.acct_no, Name = x.acct_desc }).ToList();
            ViewBag.dflt_acct = new SelectList(GLAccount, "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(MasterDedcodes model)
        {
            if (!db.MasterDedcodes.Where(x => x.ded_code == model.ded_code).Any())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(model).State = EntityState.Added;
                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        TempData["success"] = "Record Saved Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "There is some error. Please try again Later";
                    }
                }
            }
            else
            {
                TempData["error"] = "This Deduction code already exist";
            }
            var dedType = from x in deductiontype() select new { Id = x.Value, Name = x.Key };
            ViewBag.ded_type = new SelectList(dedType, "Id", "Name", model.ded_type);
            string[] taxStatus = { "F", "T", "U", "N", "A", "B", "C", "D" };
            ViewBag.ded_taxred = new SelectList(taxStatus, model.ded_taxred);
            var frequency = from x in deductionFrequency() select new { Id = x.Value, Name = x.Key };
            ViewBag.dflt_apply = new SelectList(frequency, "Id", "Name", model.dflt_apply);
            string[] defaultAccount = { "BELLIN", "EXPENS" };
            var dAccount = db.PayrollGLAccounts.Where(x => x.acct_no == model.dflt_acct).Select(x => x.acct_type).FirstOrDefault();
            ViewBag.GLAccount = new SelectList(defaultAccount, dAccount);
            var GLAccount = db.PayrollGLAccounts.Select(x => new { Id = x.acct_no, Name = x.acct_desc }).ToList();
            ViewBag.dflt_acct = new SelectList(GLAccount, "Id", "Name", model.dflt_acct);
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterDedcodes entity = db.MasterDedcodes.Where(x => x.ded_code_id == Id).FirstOrDefault();
            if (entity == null)
            {
                return HttpNotFound();
            }
            var dedType = from x in deductiontype() select new { Id = x.Value, Name = x.Key };
            ViewBag.ded_type = new SelectList(dedType, "Id", "Name", entity.ded_type);
            string[] taxStatus = { "F", "T", "U", "N", "A", "B", "C", "D" };
            ViewBag.ded_taxred = new SelectList(taxStatus, entity.ded_taxred);
            var frequency = from x in deductionFrequency() select new { Id = x.Value, Name = x.Key };
            ViewBag.dflt_apply = new SelectList(frequency, "Id", "Name", entity.dflt_apply);
            string[] defaultAccount = { "BELLIN", "EXPENS" };
            var dAccount = db.PayrollGLAccounts.Where(x => x.acct_no == entity.dflt_acct).Select(x => x.acct_type).FirstOrDefault();
            ViewBag.GLAccount = new SelectList(defaultAccount, dAccount);
            var GLAccount = db.PayrollGLAccounts.Select(x => new { Id = x.acct_no, Name = x.acct_desc }).ToList();
            ViewBag.dflt_acct = new SelectList(GLAccount, "Id", "Name", entity.dflt_acct);
            return View(entity);
        }
        [HttpPost]
        public ActionResult Edit(MasterDedcodes model)
        {
            using (var _db = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
            {
                var ded_code = _db.MasterDedcodes.Find(model.ded_code_id).ded_code;
                if (_db.MasterEmployeeDeductions.Where(x => x.Ded_Code == ded_code).Any())
                {
                    TempData["error"] = "Code is already in use, you can't change this code.";
                    return RedirectToAction("Index");
                }
            }

            if (!db.MasterDedcodes.Where(x => x.ded_code == model.ded_code && x.ded_code_id != model.ded_code_id).Any())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(model).State = EntityState.Modified;
                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        TempData["success"] = "Record Updated Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "There is some error. Please try again Later";
                    }
                }
            }
            else
            {
                TempData["error"] = "This Deduction code already exist";
            }
            var dedType = from x in deductiontype() select new { Id = x.Value, Name = x.Key };
            ViewBag.ded_type = new SelectList(dedType, "Id", "Name", model.ded_type);
            string[] taxStatus = { "F", "T", "U", "N", "A", "B", "C", "D" };
            ViewBag.ded_taxred = new SelectList(taxStatus, model.ded_taxred);
            var frequency = from x in deductionFrequency() select new { Id = x.Value, Name = x.Key };
            ViewBag.dflt_apply = new SelectList(frequency, "Id", "Name", model.dflt_apply);
            string[] defaultAccount = { "BELLIN", "EXPENS" };
            var dAccount = db.PayrollGLAccounts.Where(x => x.acct_no == model.dflt_acct).Select(x => x.acct_type).FirstOrDefault();
            ViewBag.GLAccount = new SelectList(defaultAccount, dAccount);
            var GLAccount = db.PayrollGLAccounts.Select(x => new { Id = x.acct_no, Name = x.acct_desc }).ToList();
            ViewBag.dflt_acct = new SelectList(GLAccount, "Id", "Name", model.dflt_acct);
            return View(model);
        }
        [HttpGet]
        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterDedcodes entity = db.MasterDedcodes.Where(x => x.ded_code_id == Id).FirstOrDefault();
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        public Dictionary<string, char> deductiontype()
        {
            Dictionary<string, char> dedType = new Dictionary<string, char>();
            dedType.Add("Gross", 'G');
            dedType.Add("Tax", 'T');
            dedType.Add("Hours", 'H');
            dedType.Add("Flat Rate", 'N');
            dedType.Add("Social Security Wages", 'F');
            dedType.Add("FUTA Wages", 'U');
            return dedType;
        }
        public Dictionary<string, char> deductionFrequency()
        {
            Dictionary<string, char> dedFrequency = new Dictionary<string, char>();
            dedFrequency.Add("Always", 'A');
            dedFrequency.Add("Monthly", 'M');
            dedFrequency.Add("Quarterly", 'Q');
            dedFrequency.Add("Yearly", 'Y');
            dedFrequency.Add("Never", 'N');
            dedFrequency.Add("FUTA Wages", 'U');
            return dedFrequency;
        }

        public JsonResult checkAccountExistAjax(int id, int account)
        {
            int result = db.MasterDedcodes.Where(x => x.ded_code_id != id && x.dflt_acct == account).Count();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}