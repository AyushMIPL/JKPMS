using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Timers;
using App.Data;
using App.Data.Entities;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.Entity;
using System.Net.Mail;
namespace JKPS.WindowService
{
    public partial class JKPSService : ServiceBase
    {
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        //AppDbContext db = new AppDbContext();
        AppDbContext db;
        string ErrorMessage = string.Empty;
        private Timer scheduleTimer = null;
        private DateTime lastRun;
        private bool flag;
        public JKPSService()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            InitializeComponent();
            try
            {
                scheduleTimer = new Timer();
                scheduleTimer.Interval = 1000 * 60 * 60 * 24;
                scheduleTimer.Elapsed += new ElapsedEventHandler(scheduleTimer_Elapsed);
            }
            catch (Exception ex)
            {
                string MailTo = ConfigurationSettings.AppSettings["MailToDeveloper"];
                string cc = ConfigurationSettings.AppSettings["MailToDeveloperInCC"];
                sendmail(MailTo, "Error In JKPS Window Service", ex.Message + " " + ex.StackTrace, cc);
            }
        }
        private void WriteToFile(string text)
        {
            string path = ConfigurationSettings.AppSettings["ServiceLogPath"];
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }
        protected override void OnStart(string[] args)
        {
            flag = true;
            lastRun = DateTime.Now;
            scheduleTimer.Enabled = true;
            scheduleTimer.Start();
            ServiceJKPSMethod();
        }
        protected void scheduleTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (flag == true)
                {
                    if (DateTime.Now > lastRun)
                    {
                        flag = false;
                        lastRun = DateTime.Now;
                        ServiceJKPSMethod();
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string MailTo = ConfigurationSettings.AppSettings["MailToDeveloper"];
                string cc = ConfigurationSettings.AppSettings["MailToDeveloperInCC"];
                sendmail(MailTo, "Error In JKPS Window Service", ex.Message + " " + ex.StackTrace, cc);
            }
        }
        private void ServiceJKPSMethod()
        {
            WriteToFile("Service method called at - " + DateTime.Now);
            string Body = "<p>Dear Team,</P><p>Following dependent has been terminated today</p>";
            DateTime date = DateTime.Now.Date;
            Body += "<table border='1' style='width : 100%; border: 1px solid black;border-collapse: collapse;'>";
            try
            {
                var dependantList = (from dep in db.MasterDependantDetails.Where(x => x.IsActive == true)
                                     join contributor in db.MasterContributor.Where(x => x.IsActive == true) on dep.PersonID equals contributor.PersonID
                                     join emp in db.MasterEmployer.Where(x => x.IsActive == true) on contributor.EmployerID equals emp.Id
                                     where dep.TerminationDate == date
                                     select new { DependantId = dep.Id, EmployerName = emp.EmployerName, EmployerId = emp.UniqueID, ContributorName = contributor.FirstName + " " + contributor.LastName }).GroupBy(x => new { x.EmployerName, x.EmployerId }).ToList();
                if (dependantList.Count > 0)
                {
                    foreach (var eachDependant in dependantList)
                    {
                        Body += "<tr><th colspan='5'>" + eachDependant.Key.EmployerName + "&nbsp;(" + eachDependant.Key.EmployerId + ")</th></tr>";
                        Body += string.Format("<tr><th>Contributor Name</th><th>Dependant Name</th><th>Gender</th><th>Date Of Birth</th><th>Termination Date</th></tr>");
                        foreach (var item in eachDependant)
                        {
                            var entity = db.MasterDependantDetails.Find(item.DependantId);
                            entity.IsActive = false;
                            string Gender = entity.Gender == "M" ? "Male" : "Female";
                            Body += string.Format("<tr><td>" + item.ContributorName + "</td><td>" + entity.FirstName + " " + entity.LastName + "</td><td>" + Gender + "</td><td>" + entity.DateOfBirth.Value.ToString("dd/MM/yyy") + "</td><td>" + entity.TerminationDate.Value.ToString("dd/MM/yyy") + "</td></tr>");

                            db.Entry(entity).State = EntityState.Modified;
                        }

                    }
                    db.SaveChanges();
                }

                if (dependantList.Count > 0)
                {
                    string MailTo = ConfigurationSettings.AppSettings["MailToJKPSTeam"];
                    bool isEmail = Regex.IsMatch(MailTo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                    if (isEmail)
                    {
                        string Subject = "Terminated Dependant Details"; Body += "</table>";

                        string cc = ConfigurationSettings.AppSettings["MailToJKPSTeamInCC"];
                        sendmail(MailTo, Subject, Body, cc);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteToFile(ex.Message + " " + ex.StackTrace);
                string MailTo = ConfigurationSettings.AppSettings["MailToDeveloper"];
                string cc = ConfigurationSettings.AppSettings["MailToDeveloperInCC"];
                sendmail(MailTo, "Error In JKPS Window Service", ex.Message + " " + ex.StackTrace, cc);
            }
        }
        public void sendmail(string MailTo, string subject, string body, string cc)
        {
            string[] CCId = cc.Split(',');
            string MailFrom = ConfigurationSettings.AppSettings["MailFrom"];
            string password = ConfigurationSettings.AppSettings["password"];
            string port = ConfigurationSettings.AppSettings["port"];
            string host = ConfigurationSettings.AppSettings["host"];
            bool ssl = string.IsNullOrWhiteSpace(ConfigurationSettings.AppSettings["enableSsl"]) ? false : Convert.ToBoolean(ConfigurationSettings.AppSettings["enableSsl"]);

            using (MailMessage mm = new MailMessage(MailFrom, MailTo))
            {
                if (cc.Count() > 0)
                {
                    foreach (string CCEmail in CCId)
                    {
                        mm.CC.Add(new MailAddress(CCEmail));
                    }
                }
                mm.Subject = subject;
                mm.Body = body;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = host;
                smtp.EnableSsl = ssl;
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
                credentials.UserName = MailFrom;
                credentials.Password = password;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = credentials;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }
        protected override void OnStop()
        {
            scheduleTimer.Enabled = false;
        }

    }
}
