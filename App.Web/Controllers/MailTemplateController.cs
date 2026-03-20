using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Data;
using App.Data.Entities;

namespace App.Web.Controllers
{
    [Authorize]
    public class MailTemplateController : BaseController
    {
        private AppDbContext db = new AppDbContext(App.Data.ConnectionStringProvider.ConnectionName);

        // GET: MailTemplate
        public ActionResult Index()
        {
            var templates = db.MailTemplates.ToList();
            return View(templates);
        }

        // GET: MailTemplate/Create
        public ActionResult Create()
        {
            PopulateDropdowns();
            return View(new MailTemplate { IsActive = true });
        }

        // POST: MailTemplate/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MailTemplate mailTemplate, string[] ToEmailsList, string[] CcEmailsList)
        {
            if (ToEmailsList != null && ToEmailsList.Length > 0)
                mailTemplate.ToEmails = string.Join(";", ToEmailsList);
            else if (Request.Form["ToEmails"] != null)
                mailTemplate.ToEmails = Request.Form["ToEmails"].Replace(",", ";");
            else
                mailTemplate.ToEmails = null;

            if (CcEmailsList != null && CcEmailsList.Length > 0)
                mailTemplate.CcEmails = string.Join(";", CcEmailsList);
            else if (Request.Form["CcEmails"] != null)
                mailTemplate.CcEmails = Request.Form["CcEmails"].Replace(",", ";");
            else
                mailTemplate.CcEmails = null;

            if (!string.IsNullOrEmpty(mailTemplate.ToEmails) && !AreEmailsValid(mailTemplate.ToEmails))
            {
                ModelState.AddModelError("ToEmails", "One or more email addresses in 'To Emails' are invalid.");
            }
            if (!string.IsNullOrEmpty(mailTemplate.CcEmails) && !AreEmailsValid(mailTemplate.CcEmails))
            {
                ModelState.AddModelError("CcEmails", "One or more email addresses in 'Cc Emails' are invalid.");
            }

            if (ModelState.IsValid)
            {
                mailTemplate.CreatedOn = DateTime.Now;
                db.MailTemplates.Add(mailTemplate);
                db.SaveChanges();
                TempData["Success"] = "Mail Template created successfully.";
                return RedirectToAction("Index");
            }
            PopulateDropdowns();
            return View(mailTemplate);
        }

        // GET: MailTemplate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            MailTemplate mailTemplate = db.MailTemplates.Find(id);
            if (mailTemplate == null)
            {
                return HttpNotFound();
            }
            PopulateDropdowns();
            return View(mailTemplate);
        }

        // POST: MailTemplate/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MailTemplate mailTemplate, string[] ToEmailsList, string[] CcEmailsList)
        {
            if (ToEmailsList != null && ToEmailsList.Length > 0)
                mailTemplate.ToEmails = string.Join(";", ToEmailsList);
            else if (Request.Form["ToEmails"] != null)
                mailTemplate.ToEmails = Request.Form["ToEmails"].Replace(",", ";");
            else
                mailTemplate.ToEmails = null;

            if (CcEmailsList != null && CcEmailsList.Length > 0)
                mailTemplate.CcEmails = string.Join(";", CcEmailsList);
            else if (Request.Form["CcEmails"] != null)
                mailTemplate.CcEmails = Request.Form["CcEmails"].Replace(",", ";");
            else
                mailTemplate.CcEmails = null;

            if (!string.IsNullOrEmpty(mailTemplate.ToEmails) && !AreEmailsValid(mailTemplate.ToEmails))
            {
                ModelState.AddModelError("ToEmails", "One or more email addresses in 'To Emails' are invalid.");
            }
            if (!string.IsNullOrEmpty(mailTemplate.CcEmails) && !AreEmailsValid(mailTemplate.CcEmails))
            {
                ModelState.AddModelError("CcEmails", "One or more email addresses in 'Cc Emails' are invalid.");
            }

            if (ModelState.IsValid)
            {
                var existing = db.MailTemplates.Find(mailTemplate.Id);
                if (existing != null)
                {
                    existing.TemplateName = mailTemplate.TemplateName;
                    existing.ProcessKey = mailTemplate.ProcessKey;
                    existing.Subject = mailTemplate.Subject;
                    existing.Body = mailTemplate.Body;
                    existing.ToEmails = mailTemplate.ToEmails;
                    existing.CcEmails = mailTemplate.CcEmails;
                    existing.IsActive = mailTemplate.IsActive;
                    existing.ModifiedOn = DateTime.Now;

                    db.Entry(existing).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Success"] = "Mail Template updated successfully.";
                    return RedirectToAction("Index");
                }
            }
            PopulateDropdowns();
            return View(mailTemplate);
        }

        // GET: MailTemplate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            MailTemplate mailTemplate = db.MailTemplates.Find(id);
            if (mailTemplate == null)
            {
                return HttpNotFound();
            }
            return View(mailTemplate);
        }

        // POST: MailTemplate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MailTemplate mailTemplate = db.MailTemplates.Find(id);
            if (mailTemplate != null)
            {
                db.MailTemplates.Remove(mailTemplate);
                db.SaveChanges();
                TempData["Success"] = "Mail Template deleted successfully.";
            }
            return RedirectToAction("Index");
        }

        private void PopulateDropdowns()
        {
            ViewBag.ProcessKeys = db.MasterProcessKeys.Select(m => m.ProcessKeyName).Distinct().ToList();
            
            ViewBag.Emails = db.MasterEmails.Select(m => m.EmailAddress).Distinct().ToList();
        }

        private bool AreEmailsValid(string emailString)
        {
            if (string.IsNullOrWhiteSpace(emailString))
                return true;

            var emails = emailString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var email in emails)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email.Trim());
                    if (addr.Address != email.Trim())
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return true;
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
