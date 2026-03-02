using App.Data;
using App.Data.Entities;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using System.Web;
using System.Configuration;
using System.Text.RegularExpressions;
//using App.Data.Entities;


using App.Data;
using App.Data.Entities;
using App.Data.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
 




namespace App.API.Controllers
{
    public class BaseApiController : ApiController
    {
        //readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private AppDbContext DbContext = new AppDbContext();
        // GET: BaseController
        public AppDbContext db
        {
            get { return DbContext; }
        }
        public string GetCustomerGPOURL()
        {
            return ConfigurationManager.AppSettings["CustomerWebsiteURL"].ToString();
        }
        public AppUserManager OwinUserManger
        {
            get
            {
                if (db.Database.Connection.ConnectionString != ConnectionStringProvider.GetConnectionString())
                {
                    var context = System.Web.HttpContext.Current.GetOwinContext();
                    return AppUserManager.Create(new Microsoft.AspNet.Identity.Owin.IdentityFactoryOptions<AppUserManager>(), context);
                }
                else
                {
                    return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
                }
            }
        }
        public IAuthenticationManager OwinAuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        //public void LogElmahException(Exception ex)
        //{
        //    try
        //    {
        //        Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(ex));
        //        Core.Repository.MailMessageRepo _mailMessage = new Core.Repository.MailMessageRepo(new AppDbContext());
        //        MailMessages mailObj = new MailMessages()
        //        {
        //            ProcessName = "ApplicationExceptions",
        //            Subject = "Post Office : Application Exceptions",
        //            Body = ex.Message,
        //            Recipient = SiteHelper.TestEmail,
        //            AccountNo = "",
        //            AttatchmentFile = null,
        //            CustomerID = null,
        //            DispatchNumber = null,
        //            ErrorDescription = null,
        //            IsRead = false,
        //            IsSent = false,
        //            IsStarred = false,
        //            Name = "Neeraj",
        //            Priority = null,
        //            SentOn = null,
        //            TotalFreight = null,
        //            WRNumber = null,
        //        };
        //        _mailMessage.Create(mailObj);
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}


        //protected override void Dispose(bool disposing)
        //{
        //  if (disposing)
        //  {
        //    db.Dispose();
        //  }
        //  base.Dispose(disposing);
        //}
        //public void ToasterError(string msg)
        //{
        //  TempData["error"] = msg;
        //}
        //public void ToasterSuccess(string msg)
        //{
        //  TempData["success"] = msg;
        //}
        //public void ToasterInfo(string msg)
        //{
        //  TempData["info"] = msg;
        //}
        //public void ToasterWarning(string msg)
        //{
        //  TempData["warning"] = msg;
        //}

        public string Passvalidate(int length, bool Ucase, bool Lcase, bool Digit, bool Scase, string pass)
        {
            string result = string.Empty;
            if (pass.Length < length)
            {
                result += " Password must be at least " + length + " characters long,";
            }
            if (Digit)
            {
                string PasswordPattern = ".*\\d+.*";
                if (!Regex.IsMatch(pass, PasswordPattern))
                {

                    result += " The Password must have at least one numeric character,";

                }

            }
            if (Ucase)
            {
                string PasswordPattern = "(?=.*?[A-Z])";
                if (!Regex.IsMatch(pass, PasswordPattern))
                {

                    result += " The Password must have at least one uppercase character,";

                }

            }
            if (Lcase)
            {
                string PasswordPattern = "(?=.*?[a-z])";
                if (!Regex.IsMatch(pass, PasswordPattern))
                {

                    result += " The Password must have at least one lowercase character,";

                }

            }
            if (Scase)
            {
                string PasswordPattern = "(?=.*?[#?!@$%^&*-])";
                if (!Regex.IsMatch(pass, PasswordPattern))
                {
                    result += " The Password must have at least one special character,";
                }

            }
            return result;
        }

    }
}
