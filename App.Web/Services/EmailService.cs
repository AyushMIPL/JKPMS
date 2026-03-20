using App.Data;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using log4net;

namespace App.Web.Services
{
    public class EmailService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(EmailService));
        public static void SendProcessEmail(string processKey, Dictionary<string, string> variables)
        {
            try
            {
                using (var context = AppDbContext.Create())
                {
                    var templates = context.MailTemplates.Where(t => t.ProcessKey == processKey && t.IsActive);
                    
                    if (templates == null)
                        return;

                    foreach (var template in templates)
                    {
                        string subject = template.Subject;
                        string body = template.Body;

                        if (variables != null)
                            foreach (var kvp in variables)
                            {
                                subject = subject?.Replace("{" + kvp.Key + "}", kvp.Value);
                                body = body?.Replace("{" + kvp.Key + "}", kvp.Value);
                            }

                        var mailMessage = new MailMessage();
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;

                        if (!string.IsNullOrWhiteSpace(template.ToEmails))
                        {
                            var toAddrs = template.ToEmails.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var addr in toAddrs)
                            {
                                mailMessage.To.Add(new MailAddress(addr.Trim()));
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(template.CcEmails))
                        {
                            var ccAddrs = template.CcEmails.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var addr in ccAddrs)
                            {
                                mailMessage.CC.Add(new MailAddress(addr.Trim()));
                            }
                        }

                        if (mailMessage.To.Count > 0)
                        {
                            using (var smtpClient = new SmtpClient())
                            {
                                smtpClient.Send(mailMessage);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // In a real application, log the exception.
                Logger.Error("Error sending process email for key " + processKey, ex);
            }
        }
    }
}
