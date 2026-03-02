using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using App.Data;
using App.Web.Controllers;
using App.Web.Helper;
using App.Web.Models;
using CaptchaMvc.Infrastructure;
using CaptchaMvc.Interface;
using CaptchaMvc.Models;

namespace App.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleTable.EnableOptimizations = false;
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.Register();
            //--Captcha-- 
            var captchaManager = (DefaultCaptchaManager)CaptchaUtils.CaptchaManager;
            //-- this will generate  alphanumeric string------------------
            captchaManager.CharactersFactory = () => "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            captchaManager.PlainCaptchaPairFactory = length =>
            {
                string randomText = RandomText.Generate(captchaManager.CharactersFactory(), length);
                bool ignoreCase = false;//This parameter is responsible for ignoring case.
                return new KeyValuePair<string, ICaptchaValue>(Guid.NewGuid().ToString("N"),
                    new StringCaptchaValue(randomText, randomText, ignoreCase));
            };

        }
        protected void Application_Error()
        {
            System.Web.HttpContext context = HttpContext.Current;
            System.Exception exception = Context.Server.GetLastError();


            // Log the error to ELMAH
            //Elmah.ErrorSignal.FromCurrentContext().Raise(exception);

            // Create an instance of your custom error logger
            var errorLogger = new ExceptionLogHelper();

            // Log the error to the database
            errorLogger.LogErrorToDatabase(exception);

            //Response.Redirect(String.Format("~/Error/NotFound"));
            context.Server.ClearError();


            //if (!string.IsNullOrEmpty(exception.Message) && !(context.Request.RawUrl.Contains("account")))
            //{
            //    if (exception.Message.Contains("not found"))
            //        Response.Redirect(String.Format("~/Error/NotFound"));
            //    else
            //        Response.Redirect(String.Format("~/Error/TechnicalError"));
            //}
            //else
            //{
            //    if (exception.InnerException != null)
            //        Response.Redirect(String.Format("~/Error/OuterError?errorMessage={0}&innerException={1}", HttpUtility.UrlEncode(exception.Message), HttpUtility.UrlEncode(exception.InnerException.Message)));
            //    else
            //        Response.Redirect(String.Format("~/Error/OuterError?errorMessage={0}&innerException={1}", HttpUtility.UrlEncode(exception.Message), ""));
            //}
        }
        //protected void Application_EndRequest()
        //{
        //    var antiForgeryCookie = Response.Cookies["__RequestVerificationToken"];
        //    if (antiForgeryCookie != null)
        //    {
        //        antiForgeryCookie.HttpOnly = true;
        //        antiForgeryCookie.Secure = false; // Set true for HTTPS
        //        antiForgeryCookie.SameSite = SameSiteMode.Lax; // Set SameSite attribute to Strict
        //    }
        //}

        //protected void Application_BeginRequest()
        //{
        //    if (Request.Cookies["__RequestVerificationToken"] != null)
        //    {
        //        HttpCookie antiForgeryCookieRequest = Request.Cookies["__RequestVerificationToken"];
        //        antiForgeryCookieRequest.HttpOnly = true;
        //        antiForgeryCookieRequest.Secure = Request.IsSecureConnection; // Ensure this is set to true for production environments
        //        antiForgeryCookieRequest.SameSite = SameSiteMode.Strict; // Set SameSite mode
        //    }
        //}

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
            HttpContext.Current.Response.Headers.Remove("Server");
            HttpContext.Current.Response.Headers.Remove("X-Frame-Options"); // Ensure it's removed
            HttpContext.Current.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN"); // Set DENY
        }
        protected void Application_BeginRequest()
        {      
          var cultureInfo = new System.Globalization.CultureInfo("en-IN");
          cultureInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"; // Force the desired format
          cultureInfo.DateTimeFormat.DateSeparator = "/"; // Ensure separator is "/"
          // Apply the culture to the current thread
          System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
          System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }


  }
}
