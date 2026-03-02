using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using App.Web.Models;
using App.Data.Entities;
using Microsoft.Owin.Host.SystemWeb;
using App.Data.Extentions;
using App.Web.Helper;
using System.Net;
using Postal;
using System.Text.RegularExpressions;
using System.Reflection;
using CrystalDecisions.CrystalReports.Engine;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using Newtonsoft.Json;
using Castle.Components.DictionaryAdapter.Xml;
using System.Web.Security;
using App.Data.ViewModels;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Antlr.Runtime.Tree;
using System.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Vml.Office;
using MvcSiteMapProvider.Reflection;
using App.Web.Repository;
using System.Security.Policy;
using App.Data;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using System.Security.Cryptography;
using System.Management.Instrumentation;
using static App.Web.Helper.Helper;
using JKPS.BLL;
using JKPS.COMMON;
using JKPS.DL;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using Renci.SshNet;
using App.Web.Filters;
using Microsoft.Office.Interop.Excel;
using ExceptionManagement;
using DocumentFormat.OpenXml.EMMA;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Configuration;
using System.Net.Configuration;
using System.Data.Entity.Validation;

namespace App.Web.Controllers
{
  [Authorize]
  public class AccountController : BaseController
  {
    private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
    private AppDbContext db;
    private AppDbContext _DbContext;
    private ExceptionLogHelper errorLogger = new ExceptionLogHelper();
    private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public AccountController()
    {
      db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
      _DbContext = DbContext;
    }

    GeneratePensionProcessModel generatePensionProcess;
    bool IsPendioGenrated = false;
    DVOPYBatchProcessStybatchr pObjBatch = null;
    private async Task SignInAsync(App.Data.Entities.AppUser user, bool isPersistent)
    {
      OwinAuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
      OwinAuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent },
        await user.GenerateUserIdentityAsync(OwinUserManger));
    }

    // GET: /Account/Login
    [AllowAnonymous]
    public ActionResult Login(string returnUrl)
    {
      App.Web.Models.LoginViewModel model = new App.Web.Models.LoginViewModel();
      ViewBag.ReturnUrl = returnUrl;
      HttpCookie existingCookie = Request.Cookies["userName"];
      ViewBag.RegionNames = GetRegions();

      //var sftpuser = "jkpsftp";
      //var sftPass = "ru@YeLlow@1";

      //var u = UrlEncryption.EncryptURL(sftpuser);
      //var p = UrlEncryption.EncryptURL(sftPass);


      if (existingCookie != null)
      {
        model.UserName = existingCookie.Values.Get("username");
        model.Password = existingCookie.Values.Get("password");

      }
      return View(model);
    }

    [AllowAnonymous]
    public ActionResult ChooseFinancialYear()
    {
      string UserName = GetCookieValueByName("_N_Cookie");
      string Password = GetCookieValueByName("_P_Cookie");
      string Region = GetCookieValueByName("_R_Cookie");

      if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Region))
      {
        // Return to login Page
        return RedirectToAction("Login", "Account");
      }

      List<string> FinancialYearList = new List<string>();
      List<string> AllFinancialYears = GetFinancialYears();
      if (AllFinancialYears.Count > 0)
      {
        FinancialYearList.AddRange(AllFinancialYears);
      }

      // Get current financial year
      string CurrentFinancialYearString = string.Empty;
      int CurrentFinancialYear = DateTime.Now.Year;
      DateTime CurrentDate = DateTime.Now;
      if (CurrentDate.Month < 4)
      {
        CurrentFinancialYear = DateTime.Now.Year - 1;
      }

      CurrentFinancialYearString = CurrentFinancialYear + "-" + (CurrentFinancialYear + 1);

      if (!FinancialYearList.Any(x => x == CurrentFinancialYearString))
      {
        FinancialYearList.Add(CurrentFinancialYearString);
      }

      ViewBag.FinancialYear = FinancialYearList;
      return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> ChooseFinancialYear(ChooseFinancialYearViewModel model)
    {
      ViewBag.FinancialYear = GetFinancialYears();
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      model.UserName = GetCookieValueByName("_N_Cookie");
      model.Password = GetCookieValueByName("_P_Cookie");
      model.Region = GetCookieValueByName("_R_Cookie");

      if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.Region))
      {
        // Return to login Page
        return RedirectToAction("Login", "Account");
      }

      var user = await OwinUserManger.FindAsync(model.UserName, model.Password);

      if (user != null)
      {
        TempData["UserId"] = user.Id;
        await SignInAsync(user, false);

        Session["List"] = "";
        int userId = _DbContext.Users.Where(x => x.UserName == model.UserName).Select(x => x.Id).FirstOrDefault();
        int roleId = _DbContext.UserRole.Where(x => x.UserId == userId).Select(x => x.RoleId).FirstOrDefault();
        var result = (from a in _DbContext.SecModule.Where(x => x.IsActive == true) join b in _DbContext.SecRoleModule.AsNoTracking().Where(x => x.IsActive == true) on a.Id equals b.ModuleID where b.RoleID == roleId select new App.Data.ViewModels.SessionViewModel { ActionName = a.ActionName, ControllerName = a.ControllerName, ViewPermission = b.ViewPermission, AddPermssion = b.AddPermssion, EditPermission = b.EditPermission, DeletePermission = b.DeletePermission, ParentId = a.ParentId, ModuleID = b.ModuleID, RoleID = b.RoleID, ModuleName = a.ModuleName, ModuleClass = a.ModuleClass, DisplayOrder = a.DisplayOrder, Region = model.Region }).ToList();
        Session["List"] = result;

        // Get financial Year
        string[] FinancialYearArray = model.FinancialYear.Split('-');

        Session["FinancialYear"] = Convert.ToInt32(FinancialYearArray[0]);

        LoginLog(user.Id, user.UserName, model.Region, false);

        return RedirectToAction("Index", "Dashboard");
      }
      else
      {
        return RedirectToAction("Login", "Account");
      }
    }

    private List<string> GetFinancialYears()
    {
      List<string> FinancialYearList = new List<string>();

      DataSet ds = new DataSet();
      StringBuilder SQL = new StringBuilder();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      SQL.Append("SELECT DISTINCT CASE WHEN MONTH(pay_date) >= 4 THEN CONCAT(YEAR(pay_date), '-', YEAR(pay_date) + 1) ");
      SQL.Append("ELSE CONCAT(YEAR(pay_date) - 1, '-', YEAR(pay_date)) ");
      SQL.Append("END AS FinancialYear FROM process_directdeposit_details");

      // Execute the query and fill the DataSet
      SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), objDalBaseClass.ConnectionString);
      da.Fill(ds);

      if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
      {
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
          FinancialYearList.Add(ds.Tables[0].Rows[i][0].ToString());
        }
      }

      return FinancialYearList;
    }

    private List<string> GetRegions()
    {

      var regionNames = _DbContext.MasterRegion.Where(x => x.IsActive == true).Select(x => x.Name).ToList();
      return regionNames;
    }
    [AllowAnonymous]
    public async Task<ActionResult> GetCaptchaAudio()
    {
      string captchaText = (string)Session["CaptchaCode"] ?? "Error";
      using (var synthesizer = new System.Speech.Synthesis.SpeechSynthesizer())
      using (var ms = new MemoryStream())
      {
        await Task.Run(() =>
        {
          synthesizer.SetOutputToWaveStream(ms);
          // Speak each character with distinction for uppercase and lowercase
          foreach (char c in captchaText)
          {
            if (char.IsUpper(c))
            {
              synthesizer.Speak($"Capital {c}");
            }
            else if (char.IsLower(c))
            {
              synthesizer.Speak($"Small {c}");
            }
            else
            {
              synthesizer.Speak(c.ToString()); // For numbers or special characters
            }

            synthesizer.Speak(" "); // Pause between characters
          }
        });

        ms.Seek(0, SeekOrigin.Begin);
        return File(ms.ToArray(), "audio/wav");
      }
    }
    //[AllowAnonymous]
    //public async Task<ActionResult> GetCaptchaAudio()
    //{
    //  string captchaText = (string)Session["CaptchaCode"] ?? "Error";

    //  using (var synthesizer = new System.Speech.Synthesis.SpeechSynthesizer())
    //  using (var ms = new MemoryStream())
    //  {
    //    await Task.Run(() =>
    //    {
    //      synthesizer.SetOutputToWaveStream(ms);
    //      string[] words = captchaText.Split(' '); 
    //      foreach (string word in words)
    //      {
    //        synthesizer.Speak(word);
    //        synthesizer.Speak("   "); // Add a pause between words
    //      }
    //    });

    //    ms.Seek(0, SeekOrigin.Begin);
    //    return File(ms.ToArray(), "audio/wav");
    //  }
    //}


    [AllowAnonymous]
    public ActionResult GenerateCaptcha()
    {
      // Generate a random CAPTCHA code
      var captchaCode = TextHelper.GenerateRandomText(5); // Generate 6-character code

      // Save the CAPTCHA code in Session for later verification
      Session["CaptchaCode"] = captchaCode;

      // Path to the background image
      string bgImagePath = Server.MapPath("~/Images/captchaBackground.png");

      using (var background = new Bitmap(bgImagePath))
      //using (var bitmap = new Bitmap(background.Width, background.Height))
      using (var bitmap = new Bitmap(150, 50))
      using (var graphics = Graphics.FromImage(bitmap))
      {
        // Draw the background image onto the new bitmap
        graphics.DrawImage(background, 0, 0, background.Width, background.Height);

        // Set the cursive font style for the CAPTCHA text
        using (var font = new System.Drawing.Font("Ariel", 24, FontStyle.Bold | FontStyle.Italic))
        {
          // Define a brush for the text color
          var textColorBrush = Brushes.DeepSkyBlue;

          // Draw the CAPTCHA code on top of the background
          graphics.DrawString(captchaCode, font, textColorBrush, new PointF(10, 10));
        }

        using (var stream = new MemoryStream())
        {
          bitmap.Save(stream, ImageFormat.Png);
          return File(stream.ToArray(), "image/png");
        }
      }
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    //[CaptchaMvc.Attributes.CaptchaVerify("Captcha is not valid")]
    public async Task<ActionResult> Login(App.Web.Models.LoginViewModel model, string returnUrl, string region)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          model.UserName = model.EnteredUserName;
          model.Password = model.EnteredPassword;
          ViewBag.RegionNames = GetRegions();
          return View(model);

        }
        string OriginalCaptcha = string.Empty;
        if (Session["CaptchaCode"] != null)
        {
          OriginalCaptcha = Session["CaptchaCode"] as string;
        }
        if (OriginalCaptcha != model.CaptchaCode)
        {
          model.UserName = model.EnteredUserName;
          model.Password = model.EnteredPassword;
          ModelState.AddModelError("", "Invalid Captcha.");
          ViewBag.RegionNames = GetRegions();
          return View(model);
        }

        byte[] data1 = Convert.FromBase64String(model.UserName);
        string originalLoginName = Encoding.UTF8.GetString(data1);
        model.UserName = originalLoginName;
        byte[] data = Convert.FromBase64String(model.Password);
        string originalPassword = Encoding.UTF8.GetString(data);
        string message = string.Empty;
        model.Password = originalPassword;
        if (!RateLimit.IsRequestAllowed())
        {
          // Set a more informative and user-friendly message
          ModelState.AddModelError("", "You have made too many login attempts. Please wait a few minutes before trying again.");

          // Optionally, you can include information about the time when the user can try again
          var retryAfter = RateLimit.GetRetryAfterTime();
          if (retryAfter.HasValue)
          {
            ModelState.AddModelError("", $"You can try logging in again after {retryAfter.Value.ToString("mm':'ss")} minutes.");
          }
          ViewBag.RegionNames = GetRegions();
          model.UserName = model.EnteredUserName;
          model.Password = model.EnteredPassword;
          return View(model);
        }

        string storedOtp = Session["OTP"]?.ToString();
        DateTime? expiryTime = Session["OTPExpireTime"] as DateTime?;

        //if (string.IsNullOrEmpty(storedOtp) || expiryTime == null || DateTime.Now > expiryTime)
        //{
        //  ModelState.AddModelError("", $"OTP has expired or is invalid!");
        //  ViewBag.RegionNames = GetRegions();
        //  model.UserName = model.EnteredUserName;
        //  model.Password = model.EnteredPassword;
        //  return View(model);
        //}
        //if (storedOtp != model.OTP?.Trim())
        //{
        //  ModelState.AddModelError("", $"Incorrect OTP!");
        //  ViewBag.RegionNames = GetRegions();
        //  model.UserName = model.EnteredUserName;
        //  model.Password = model.EnteredPassword;
        //  return View(model);
        //}

        RegionProvider.Region = region;
        if (region == "KASHMIR REGION")
          ConnectionStringProvider.ConnectionName = "AppConnection1";

        else
          ConnectionStringProvider.ConnectionName = "AppConnection";

        Session["RegionName"] = region;

        #region 
        // Code By Himanshu Rajput  *** start ***

        HttpCookie regionCookie = new HttpCookie("regionValues");
        regionCookie.Value = region;
        // Set cookie options
        regionCookie.HttpOnly = true; // Prevents JavaScript access
        if (HttpContext.Request.IsSecureConnection)
        {
          regionCookie.Secure = true;  // Set to true if the connection is HTTPS
        }
        else
        {
          regionCookie.Secure = false; // Set to false if the connection is HTTP
        }
        regionCookie.SameSite = SameSiteMode.Strict; // SameSite option (Strict, Lax, or None)
                                                     //newCookie.Expires = DateTime.Today.AddMonths(2);
        Response.Cookies.Add(regionCookie);

        //*** End ***
        #endregion

        AppDbContext _DbContext = DbContext;

        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, change to shouldLockout: true
        //var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
        var user = await OwinUserManger.FindAsync(model.UserName, model.Password);

        var allusers = await OwinUserManger.Users.ToListAsync();

        // Use the database context to get the user by username
        var id = _DbContext.Users.Where(x => x.UserName == model.UserName).Select(x => x.Id).FirstOrDefault();
        bool isActive = _DbContext.UserProfiles.Where(x => x.UserId == id).Select(x => x.IsActive).FirstOrDefault();

        if (user != null)
        {
          var roles = user.Roles.Select(ur => ur.RoleId);

          var verify = _DbContext.Roles.Where(r => roles.Contains(r.Id)).Select(x => x.Name).FirstOrDefault();

          if (string.IsNullOrEmpty(region))
          {
            ModelState.AddModelError("", "Please select a Region.");
            ViewBag.RegionNames = GetRegions();
            model.UserName = model.EnteredUserName;
            model.Password = model.EnteredPassword;
            return View(model);
          }

          if (isActive == true)
          {
            List<string> FinancialYearList = GetFinancialYears();
            int CurrentFinancialYear = DateTime.Now.Year;
            DateTime CurrentDate = DateTime.Now;
            if (CurrentDate.Month < 4)
            {
              CurrentFinancialYear = DateTime.Now.Year - 1;
            }

            bool isCurrentFinancialYear = false;
            if (FinancialYearList.Count == 1)
            {
              string FinancialYearString = CurrentFinancialYear + "-" + (CurrentFinancialYear + 1);
              if (FinancialYearList.FirstOrDefault() == FinancialYearString)
              {
                isCurrentFinancialYear = true;
              }
            }
            //Impliment by rohit forword between date

            //List<string> FinancialYearList = GetFinancialYears();
            //string CurrentFinancialYear = DateTime.Now.Year.ToString();
            //DateTime CurrentDate = DateTime.Now.Date;
            //if (CurrentDate.Month < 4)
            //{
            //  int FromFinancialYear = +DateTime.Now.Year - 1;
            //  int ToFinancialYear = +FromFinancialYear + 1;
            //  CurrentFinancialYear = "01/04/" + FromFinancialYear + "-" + "31/03/" + ToFinancialYear;
            //}

            //bool isCurrentFinancialYear = false;
            //if (FinancialYearList.Count == 1)
            //{
            //string FinancialYearString = CurrentFinancialYear;
            //  if (FinancialYearList.FirstOrDefault() == FinancialYearString)
            //  {
            //    isCurrentFinancialYear = true;
            //  }
            //}
            if (FinancialYearList.Count == 0 || isCurrentFinancialYear)
            {
              TempData["UserId"] = user.Id;
              await SignInAsync(user, model.RememberMe);

              Session["List"] = "";
              int userId = _DbContext.Users.Where(x => x.UserName == model.UserName).Select(x => x.Id).FirstOrDefault();
              int roleId = _DbContext.UserRole.Where(x => x.UserId == userId).Select(x => x.RoleId).FirstOrDefault();
              var result = (from a in _DbContext.SecModule.Where(x => x.IsActive == true) join b in _DbContext.SecRoleModule.AsNoTracking().Where(x => x.IsActive == true) on a.Id equals b.ModuleID where b.RoleID == roleId select new App.Data.ViewModels.SessionViewModel { ActionName = a.ActionName, ControllerName = a.ControllerName, ViewPermission = b.ViewPermission, AddPermssion = b.AddPermssion, EditPermission = b.EditPermission, DeletePermission = b.DeletePermission, ParentId = a.ParentId, ModuleID = b.ModuleID, RoleID = b.RoleID, ModuleName = a.ModuleName, ModuleClass = a.ModuleClass, DisplayOrder = a.DisplayOrder, Region = region }).ToList();
              Session["List"] = result;

              Session["FinancialYear"] = CurrentFinancialYear;
              RateLimit.ClearRateLimit();
              return RedirectToLocal(returnUrl);
            }
            else
            {
              RateLimit.ClearRateLimit();
              SetCookiesForFinancialYear(model.UserName, model.Password, region);
              return RedirectToAction("ChooseFinancialYear", "Account");
            }
          }
          else
          {
            ViewBag.RegionNames = GetRegions();
            ModelState.AddModelError("", "Your account is inactive. Please contact Admin.");
            model.UserName = model.EnteredUserName;
            model.Password = model.EnteredPassword;
            return View(model);
          }
        }

        // If we got this far, something failed, redisplay form
        ViewBag.RegionNames = GetRegions();
        ModelState.AddModelError("", "Invalid login attempt. Please ensure that your username, password, and region are correct.");
      }
      catch (Exception ex)
      {
      }

      model.UserName = model.EnteredUserName;
      model.Password = model.EnteredPassword;
      return View(model);
    }

    public static void WriteTextToFile(string Text, HttpServerUtilityBase server)
    {
      try
      {
        string relativeFolderPath = "~/Log file";

        // Map the relative path to the server's full path
        string folderPath = server.MapPath(relativeFolderPath);

        // Combine the folder path with the file name to get the full path
        string path = Path.Combine(folderPath, "jkps_log.txt");
        //string path = @"D:\J & K\App.Web\Log file\jkps_log.txt";// Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Log file", "jkps_log.txt");
        //string path = @"C:\inetpub\wwwroot\JKPS_Web\Log file\jkps_log.txt";// Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Log file", "jkps_log.txt");
        System.IO.File.WriteAllText(path, Text);
      }
      catch (Exception ex)
      {

      }
    }

    // *** Code By Himanshu Rajput ***
    [HttpPost]
    public void LoginLog(int UserId, string UserName, string RegionName, bool isLogOut)
    {
      try
      {

        var preViousLogin = db.LoginLog.Where(l => l.UserId == UserId).OrderByDescending(o => o.LoginTime).FirstOrDefault();
        if (preViousLogin != null && preViousLogin.LogoutTime == null)
        {
          preViousLogin.LogoutTime = preViousLogin.LoginTime.AddMinutes(30) > DateTime.Now ? DateTime.Now : preViousLogin.LoginTime.AddMinutes(30);
          preViousLogin.ModifiedBy = UserId;
          preViousLogin.ModifiedOn = DateTime.Now;
          db.Entry(preViousLogin).State = EntityState.Modified;
        }
        if (!isLogOut)
        {
          LoginLog model = new LoginLog();
          model.UserId = UserId;
          model.UserName = UserName;
          model.CreatedBy = UserId;
          model.CreatedOn = DateTime.Now;
          model.LoginTime = DateTime.Now;
          model.IsActive = true;
          model.RegionName = RegionName;
          db.LoginLog.Add(model);
        }
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        errorLogger.LogErrorToDatabase(ex);
      }

    }

    public void SetCookiesForFinancialYear(string UserName, string Password, string Region)
    {
      // User Name Cookie
      HttpCookie UserNameCookie = new HttpCookie("_N_Cookie");
      UserNameCookie.Value = UserName;
      UserNameCookie.HttpOnly = true;
      if (HttpContext.Request.IsSecureConnection)
      {
        UserNameCookie.Secure = true;
      }
      else
      {
        UserNameCookie.Secure = false;
      }
      UserNameCookie.SameSite = SameSiteMode.Strict;
      Response.Cookies.Add(UserNameCookie);

      // Passord Cookie
      HttpCookie PasswordCookie = new HttpCookie("_P_Cookie");
      PasswordCookie.Value = Password;
      PasswordCookie.HttpOnly = true;
      if (HttpContext.Request.IsSecureConnection)
      {
        PasswordCookie.Secure = true;
      }
      else
      {
        PasswordCookie.Secure = false;
      }
      PasswordCookie.SameSite = SameSiteMode.Strict;
      Response.Cookies.Add(PasswordCookie);

      // Region Cookie
      HttpCookie RegionCookie = new HttpCookie("_R_Cookie");
      RegionCookie.Value = Region;
      RegionCookie.HttpOnly = true;
      if (HttpContext.Request.IsSecureConnection)
      {
        RegionCookie.Secure = true;
      }
      else
      {
        RegionCookie.Secure = false;
      }
      RegionCookie.SameSite = SameSiteMode.Strict;
      Response.Cookies.Add(RegionCookie);
    }

    public string GetCookieValueByName(string cookieName)
    {
      string cookieValue = string.Empty;
      try
      {
        // Retrieve the cookie by its name
        HttpCookie cookie = Request.Cookies[cookieName];

        if (cookie != null)
        {
          // Get the cookie value
          cookieValue = cookie.Value;
        }
      }
      catch
      {

      }

      return cookieValue;

    }

    // GET: /Account/Register
    [AuthorizeEx]
    [AllowAnonymous]
    public ActionResult AddUser()
    {
      ViewBag.Role = new SelectList(_DbContext.Roles.Where(x => RoleData.Contains(x.Name.ToLower().Trim())).OrderBy(x => x.Name), "Id", "Name");
      var rt = Session["UserDetails"];
      return View();
    }


    //POST: /Account/Register
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> AddUser(App.Data.ViewModels.UserRegisterViewModel model, HttpPostedFileBase[] NewfileUploadProfile)
    {
      ViewBag.Role = new SelectList(_DbContext.Roles.Where(x => RoleData.Contains(x.Name.ToLower().Trim())).OrderBy(x => x.Name), "Id", "Name", model.Role);
      if (ModelState.IsValid)
      {
        var user = new AppUser { UserName = model.UserName, Email = model.Email, EmailConfirmed = true };
        var result = await OwinUserManger.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
          string roleName = _DbContext.Roles.Where(x => x.Id.ToString() == model.Role).Select(x => x.Name).FirstOrDefault();
          var results = await OwinUserManger.AddToRolesAsync(user.Id, roleName);
          var entity = new UserProfile();
          model.MapTo(entity);
          entity.IsActive = true;
          entity.UserId = user.Id;
          entity.CreatedBy = AppUserManager.GetUserId();
          entity.CreatedOn = DateTime.Now;
          _DbContext.UserProfiles.Add(entity);
          await _DbContext.SaveChangesAsync();
          var DistrictArr = model.RegionNames.ToLower().Trim().Split(',');
          for (int i = 0; i < DistrictArr.Length; i++)
            DistrictArr[i] = DistrictArr[i].Trim();
          var DistrictList = _DbContext.MasterDistrict.Where(x => DistrictArr.Contains(x.Name.ToLower().Trim())).Select(x => new { x.RegionId, x.Id }).ToList();
          foreach (var data in DistrictList)
          {
            SecRoleLocationModule SecRoleLocationModule = new SecRoleLocationModule();
            SecRoleLocationModule.IsActive = true;
            SecRoleLocationModule.CreatedBy = Convert.ToInt32(User.Identity.GetUserId());
            SecRoleLocationModule.CreatedOn = DateTime.Now;
            SecRoleLocationModule.ModifiedBy = Convert.ToInt32(User.Identity.GetUserId());
            SecRoleLocationModule.ModifiedOn = null;
            SecRoleLocationModule.UserId = user.Id;
            SecRoleLocationModule.RoleID = Convert.ToInt32(model.Role);
            SecRoleLocationModule.RegionID = data.RegionId;
            SecRoleLocationModule.DistrictID = data.Id;
            _DbContext.SecRoleLocationModule.Add(SecRoleLocationModule);
          }
          await _DbContext.SaveChangesAsync();
          if (NewfileUploadProfile != null && NewfileUploadProfile.Length > 0)
          {
            App.Data.ViewModels.UserRegisterViewModel ProfileModel = new App.Data.ViewModels.UserRegisterViewModel();
            ProfileModel.Id = entity.Id;
            //ProfileModel.UserId = user.Id;
            await UpdateProfileImage(ProfileModel, NewfileUploadProfile);

          }

          return RedirectToAction("UserProfiles", "Account");
        }
        AddErrors(result);
      }
      return View(model);
    }
    public ActionResult BindDistrict(string RoleName)
    {
      return Json(JsonConvert.SerializeObject(GetUsersAssignedLocations(RoleName)), JsonRequestBehavior.AllowGet);
    }
    public async Task<int> UpdateProfileImage(App.Data.ViewModels.UserRegisterViewModel model, HttpPostedFileBase[] fileUploadProfile)
    {
      int result = 1;
      try
      {
        var fileName = SiteHelper.GenerateFileName(model.Id);
        var path = SiteHelper.ProfileImagesPath + fileName;
        foreach (var item in fileUploadProfile)
        {
          if (item != null)
          {
            item.SaveAs(path);
            var entity = await _DbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == model.Id);
            entity.ProfilePath = fileName;
            _DbContext.Entry(entity).State = EntityState.Modified;
            result = await _DbContext.SaveChangesAsync();
          }
        }
        return result;
      }
      catch (Exception ex)
      {
        return 0;
      }
    }
    [AllowAnonymous]
    public async Task<ActionResult> ConfirmEmail(int userId, string code)
    {
      if (userId == null || code == null)
      {
        return View("Error");
      }
      var result = await OwinUserManger.ConfirmEmailAsync(userId, code);
      return View(result.Succeeded ? "ConfirmEmail" : "Error");
    }
    [AllowAnonymous]
    public ActionResult ForgotPassword()
    {
      return View();
    }
    [AuthorizeEx]
    [HttpGet]
    public async Task<ActionResult> UserDetails(string id)
    {
      if (id == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }
      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(id));
      if (string.IsNullOrEmpty(IdUrl) || IdUrl == "Error")
      {
        return RedirectToAction("Index", "UnAuthorize");


        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      UserProfile entity = await _DbContext.UserProfiles.FindAsync(Convert.ToInt32(IdUrl));
      if (entity == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
        //return HttpNotFound();
      }


      string encryptedId = UrlEncryption.EncryptURL(entity.Id.ToString());
      ViewBag.EncryptedId = encryptedId;


      /// ***** Code By Himanshu Rajput *****

      int userid = AppUserManager.GetUserId();
      var roleList = _DbContext.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in _DbContext.SecModule.Where(x => x.ControllerName == "Account")
                   join p in _DbContext.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel
                   {
                     RoleID = roleList.ToString(),
                     ModuleName = "Account",
                     ParentId = c.ParentId,
                     ModuleID = c.Id,
                     ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission,
                     EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission,
                   }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.EditPermission = model.EditPermission;

      }




      return View(entity);
    }



    [AuthorizeEx]
    [HttpGet]
    public async Task<ActionResult> UserEdit(string id, string userType)
    {
      if (id == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }


      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(id));
      if (string.IsNullOrEmpty(IdUrl) || IdUrl == "Error")
      {
        return RedirectToAction("Index", "UnAuthorize");

      }
      var regionname = GetRegionName();

      // old Code  Working But New User Profile Edit Not Working 
      //UserProfile entity = await _DbContext.UserProfiles.FindAsync(Convert.ToInt32(IdUrl));

      // new Code Rashi Commited 
      int Id = Convert.ToInt32(IdUrl);

      // get userid based on above id from database    // ** Code By Himanshu Rajput **

      var userId = await _DbContext.UserProfiles.Where(e => e.Id == Id).Select(e => e.UserId).FirstOrDefaultAsync();

      UserProfile entity = await _DbContext.UserProfiles.SingleOrDefaultAsync(up => up.UserId == userId && up.Id == Id);

      if (entity == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
        //return HttpNotFound();
      }
      int RoleId = entity.user.Roles.FirstOrDefault().RoleId;
      string RoleName = _DbContext.Roles.FirstOrDefault(x => x.Id == RoleId).Name;
      ViewBag.RegionNames = GetAssignedDistrictForEdit(entity.UserId, RoleId, regionname);
      ViewBag.UserName = _DbContext.Users.Where(x => x.Id == entity.UserId).OrderBy(x => x.UserName).Select(x => x.UserName).FirstOrDefault();
      int roleId = _DbContext.UserRole.Where(x => x.UserId == entity.UserId).Select(x => x.RoleId).FirstOrDefault();
      ViewBag.Role = new SelectList(_DbContext.Roles.OrderBy(x => x.Name), "Id", "Name", roleId);
      ViewBag.userType = userType;
      return View(entity);
    }
    [HttpPost]
    public async Task<ActionResult> UserEdit(App.Data.ViewModels.UserProfileVM model, HttpPostedFileBase[] NewfileUploadProfile, string RegionNames, string userType = "")
    {
      string Id = UrlEncryption.Decrypt(model.Id);
      int id = Convert.ToInt32(Id);
      //int res = 0;
      DbContext dbc = new AppDbContext(ConnectionStringProvider.GetConnectionString());
      try
      {
        using (AppDbContext scope = new AppDbContext(ConnectionStringProvider.GetConnectionString()))
        {
          var MasterUserProfiles = scope.UserProfiles.FirstOrDefault(x => x.Id == id);
          if (ModelState.IsValid)
          {
            MasterUserProfiles.FirstName = model.FirstName;
            MasterUserProfiles.MiddleName = model.MiddleName;
            MasterUserProfiles.LastName = model.LastName;
            MasterUserProfiles.Email = model.Email;
            MasterUserProfiles.DOB = model.DOB;
            MasterUserProfiles.EmployeeCode = model.EmployeeCode;
            MasterUserProfiles.HireDate = model.HireDate;
            MasterUserProfiles.SocialSecurityNumber = model.SocialSecurityNumber;
            MasterUserProfiles.Gender = model.Gender;
            MasterUserProfiles.IsActive = model.IsActive;
            scope.Entry(MasterUserProfiles).State = EntityState.Modified;
            int RoleId = MasterUserProfiles.user.Roles.FirstOrDefault().RoleId;
            string RoleName = scope.Roles.FirstOrDefault(x => x.Id == RoleId).Name;
            var RemoveData = scope.SecRoleLocationModule.Where(x => x.RoleID == RoleId && x.UserId == MasterUserProfiles.UserId).ToList();
            scope.SecRoleLocationModule.RemoveRange(RemoveData);
            var DistrictArr = RegionNames.ToLower().Trim().Split(',');
            for (int i = 0; i < DistrictArr.Length; i++)
              DistrictArr[i] = DistrictArr[i].Trim();
            var DistrictList = _DbContext.MasterDistrict.Where(x => DistrictArr.Contains(x.Name.ToLower().Trim())).Select(x => new { x.RegionId, x.Id }).ToList();
            foreach (var data in DistrictList)
            {
              SecRoleLocationModule SecRoleLocationModule = new SecRoleLocationModule();
              SecRoleLocationModule.IsActive = true;
              SecRoleLocationModule.CreatedBy = Convert.ToInt32(User.Identity.GetUserId());
              SecRoleLocationModule.CreatedOn = DateTime.Now;
              SecRoleLocationModule.ModifiedBy = Convert.ToInt32(User.Identity.GetUserId());
              SecRoleLocationModule.ModifiedOn = null;
              SecRoleLocationModule.UserId = MasterUserProfiles.UserId;
              SecRoleLocationModule.RoleID = Convert.ToInt32(RoleId);
              SecRoleLocationModule.RegionID = data.RegionId;
              SecRoleLocationModule.DistrictID = data.Id;
              scope.SecRoleLocationModule.Add(SecRoleLocationModule);
              //res = await scope.SaveChangesAsync();
            }
            int res = await scope.SaveChangesAsync();
            //DbContextHelper.dBsavechanges(DbContext);
            if (NewfileUploadProfile != null && NewfileUploadProfile.Length > 0)
            {
              App.Data.ViewModels.UserRegisterViewModel ProfileModel = new App.Data.ViewModels.UserRegisterViewModel();
              ProfileModel.Id = id;
              ProfileModel.UserId = model.UserId;
              await UpdateProfileImage(ProfileModel, NewfileUploadProfile);
            }
            if (res > 0 && userType == "")
            {
              return RedirectToAction("UserProfiles");
            }
            else
            {
              return RedirectToAction("Index", "Dashboard");

            }

          }

          ViewBag.UserName = _DbContext.Users.Where(x => x.Id == model.UserId).OrderBy(x => x.UserName).Select(x => x.UserName).FirstOrDefault();
          int roleId = _DbContext.UserRole.Where(x => x.UserId == model.UserId).Select(x => x.RoleId).FirstOrDefault();
          ViewBag.Role = new SelectList(_DbContext.Roles.OrderBy(x => x.Name), "Id", "Name", roleId);
        }
      }
      catch (Exception ex)
      {

      }
      return View(model);
    }
    // POST: /Account/ForgotPassword
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ForgotPassword(App.Web.Models.ForgotPasswordViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = await OwinUserManger.FindByEmailAsync(model.Email);
        if (user == null || !(await OwinUserManger.IsEmailConfirmedAsync(user.Id)))
        {
          ModelState.AddModelError("", "User does not exist");
          return View(model);
          //return View("ForgotPasswordConfirmation");
        }
        string code = await OwinUserManger.GeneratePasswordResetTokenAsync(user.Id);
        var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        string pass = System.Web.Security.Membership.GeneratePassword(10, 3);
        String hashedNewPassword = OwinUserManger.PasswordHasher.HashPassword(pass);
        try
        {
          //dynamic emails = new Email("ForgetPassword");
          //emails.To = model.Email;
          //emails.Name = user.UserName;
          //emails.Password = pass;
          //emails.ConfirmLink = callbackUrl;//SiteHelper.GetRegisterConfirmationLink();
          //emails.Send();
        }
        catch { }

        return RedirectToAction("ForgotPasswordConfirmation", "Account");
        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        // Send an email with this link
        // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
        // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
        // return RedirectToAction("ForgotPasswordConfirmation", "Account");
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }

    //
    // GET: /Account/ForgotPasswordConfirmation
    [AllowAnonymous]
    public ActionResult ForgotPasswordConfirmation()
    {
      return View();
    }

    //
    // GET: /Account/ResetPassword
    [AuthorizeEx]
    [AllowAnonymous]
    public ActionResult ResetPassword(string code)
    {
      int userid = AppUserManager.GetUserId();
      var roleList = _DbContext.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in _DbContext.SecModule.Where(x => x.ControllerName == "Account" && x.ActionName == "ResetPassword")
                   join p in _DbContext.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Reset Password", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.EditPermission = model.EditPermission;
      }
      return View();
    }
    [AuthorizeEx]
    [HttpGet]
    public async Task<ActionResult> ResEditUserPassword(string Id)
    {
      if (Id == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
      }
      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(Id));
      if (string.IsNullOrEmpty(IdUrl) || IdUrl == "Error")
      {
        return RedirectToAction("Index", "UnAuthorize");
      }
      UserProfile entity = await _DbContext.UserProfiles.FindAsync(Convert.ToInt32(IdUrl));
      if (entity == null)
      {
        return RedirectToAction("Index", "UnAuthorize");
        //return HttpNotFound();

      }
      var model = new App.Data.ViewModels.ForgotViewModel();
      model.Email = entity.Email;
      return View(model);
    }
    [HttpPost]
    public async Task<ActionResult> ResEditUserPassword(App.Data.ViewModels.ForgotViewModel model)
    {
      try
      {
        if (ModelState.IsValid)
        {
          var user = await OwinUserManger.FindByEmailAsync(model.Email);
          if (user == null || !(await OwinUserManger.IsEmailConfirmedAsync(user.Id)))
          {
            ModelState.AddModelError("", "User does not exist");
            return View(model);
          }
          await OwinUserManger.RemovePasswordAsync(user.Id);
          //string code = await OwinUserManger.GeneratePasswordResetTokenAsync(user.Id);
          var callbackUrl = string.Format("{0}://{1}{2}{3}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"), "Account/Login"); //Url.Action("Login", "Account");

          string pass = (System.Web.Security.Membership.GeneratePassword(9, 3)) + "1";
          var result = await OwinUserManger.AddPasswordAsync(user.Id, pass);
          if (result.Succeeded)
          {
            try
            {
              dynamic emails = new Email("ForgetPassword");
              emails.To = model.Email;
              emails.Name = user.UserName;
              emails.Password = pass;
              emails.ConfirmLink = callbackUrl;
              emails.Send();
              //DbContextHelper.dBsavechanges(DbContext);
              TempData["success"] = "Password sent to registered email id...";
            }
            catch (Exception ex)
            {
              TempData["error"] = "Password not sent to registered email id, Please contact your administrator. Reason being " + ex.Message;
            }

          }
          else
          {
            TempData["error"] = "There is some error. Please try again.";
          }
          return RedirectToAction("ResetPassword", "Account");
        }
      }
      catch (Exception ex)
      {

      }

      return View(model);
    }
    //
    // POST: /Account/ResetPassword
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ResetPassword(App.Web.Models.ResetPasswordViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      var user = await OwinUserManger.FindByNameAsync(model.Email);
      if (user == null)
      {
        // Don't reveal that the user does not exist
        return RedirectToAction("ResetPasswordConfirmation", "Account");
      }
      var result = await OwinUserManger.ResetPasswordAsync(user.Id, model.Code, model.Password);
      if (result.Succeeded)
      {
        return RedirectToAction("ResetPasswordConfirmation", "Account");
      }
      AddErrors(result);
      return View();
    }

    //
    // GET: /Account/ResetPasswordConfirmation
    [AllowAnonymous]
    public ActionResult ResetPasswordConfirmation()
    {
      return View();
    }
    /* Not Used Commented by ashu
    //
    // POST: /Account/ExternalLogin
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult ExternalLogin(string provider, string returnUrl)
    {
      // Request a redirect to the external login provider
      return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
    }

    //
    // GET: /Account/SendCode
    [AllowAnonymous]
    public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
    {
      var userId = await SignInManager.GetVerifiedUserIdAsync();
      if (userId == null)
      {
        return View("Error");
      }
      var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
      var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
      return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
    }

    //
    // POST: /Account/SendCode
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> SendCode(SendCodeViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      // Generate the token and send it
      if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
      {
        return View("Error");
      }
      return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
    }

    //
    // GET: /Account/ExternalLoginCallback
    [AllowAnonymous]
    public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
    {
      var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
      if (loginInfo == null)
      {
        return RedirectToAction("Login");
      }

      // Sign in the user with this external login provider if the user already has a login
      var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
      switch (result)
      {
        case SignInStatus.Success:
          return RedirectToLocal(returnUrl);
        case SignInStatus.LockedOut:
          return View("Lockout");
        case SignInStatus.RequiresVerification:
          return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
        case SignInStatus.Failure:
        default:
          // If the user does not have an account, then prompt the user to create an account
          ViewBag.ReturnUrl = returnUrl;
          ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
          return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
      }
    }

    //
    // POST: /Account/ExternalLoginConfirmation
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
    {
      if (User.Identity.IsAuthenticated)
      {
        return RedirectToAction("Index", "Manage");
      }

      if (ModelState.IsValid)
      {
        // Get the information about the user from the external login provider
        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
          return View("ExternalLoginFailure");
        }
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await UserManager.CreateAsync(user);
        if (result.Succeeded)
        {
          result = await UserManager.AddLoginAsync(user.Id, info.Login);
          if (result.Succeeded)
          {
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            return RedirectToLocal(returnUrl);
          }
        }
        AddErrors(result);
      }

      ViewBag.ReturnUrl = returnUrl;
      return View(model);
    }
    */
    //
    // POST: /Account/LogOff
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult LogOff()
    {
      Session.Clear();
      Session.RemoveAll();
      Session.Abandon();
      AuthenticationManager.SignOut();
      LoginLog(AppUserManager.GetUserId(), AppUserManager.GetUserName(), "", true);
      return RedirectToAction("Login", "Account");
    }

    public ActionResult LogOffManually()
    {
      Session.Clear();
      Session.RemoveAll();
      Session.Abandon();
      AuthenticationManager.SignOut();
      return RedirectToAction("Login", "Account");
    }

    //
    // GET: /Account/ExternalLoginFailure
    [AllowAnonymous]
    public ActionResult ExternalLoginFailure()
    {
      return View();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (OwinUserManger != null)
        {
          OwinUserManger.Dispose();
          //_userManager = null;
        }

        if (OwinUserManger != null)
        {
          OwinUserManger.Dispose();
          //_userManager = null;
        }
      }

      base.Dispose(disposing);
    }
    [AuthorizeEx]
    public ActionResult UserProfiles(string userType)
    {
      if (string.IsNullOrEmpty(userType))
      {

        int userid = AppUserManager.GetUserId();
        var roleList = _DbContext.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
        var model = (from c in _DbContext.SecModule.Where(x => x.ControllerName == "Account" && x.ActionName == "UserProfiles")
                     join p in _DbContext.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                     from p in ps.DefaultIfEmpty()
                     select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Users", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

        if (model != null)
        {
          ViewBag.AddPermission = model.AddPermssion;
          ViewBag.EditPermission = model.EditPermission;
          ViewBag.ViewPermission = model.ViewPermission;
          ViewBag.DeletePermission = model.DeletePermission;
        }
        return View();
      }
      else
      {
        return RedirectToAction("Index", "Dashboard");


      }
    }

    #region Helpers
    // Used for XSRF protection when adding external logins
    private const string XsrfKey = "XsrfId";

    private IAuthenticationManager AuthenticationManager
    {
      get
      {
        return HttpContext.GetOwinContext().Authentication;
      }
    }

    private void AddErrors(IdentityResult result)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError("", error);
      }
    }

    private ActionResult RedirectToLocal(string returnUrl)
    {
      //return RedirectToAction("Index", "Admin");
      return RedirectToAction("Index", "Dashboard");

      //TODO: Role issue
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      if (User.IsInRole("Admin"))
      {
        return RedirectToAction("Index", "Dashboard");
      }
      else
      {
        return RedirectToAction("Index", "Dashboard");
      }
    }

    internal class ChallengeResult : HttpUnauthorizedResult
    {
      public ChallengeResult(string provider, string redirectUri)
        : this(provider, redirectUri, null)
      {
      }

      public ChallengeResult(string provider, string redirectUri, string userId)
      {
        LoginProvider = provider;
        RedirectUri = redirectUri;
        UserId = userId;
      }

      public string LoginProvider { get; set; }
      public string RedirectUri { get; set; }
      public string UserId { get; set; }

      public override void ExecuteResult(ControllerContext context)
      {
        var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
        if (UserId != null)
        {
          properties.Dictionary[XsrfKey] = UserId;
        }
        context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
      }
    }
    #endregion

    public ActionResult AjaxHandler(App.Web.Models.JQueryDataTableParamModel param)
    {
      int UserId = AppUserManager.GetUserId();
      //int RoleId = _DbContext.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
      var UserIds = _DbContext.SecRoleLocationModule.Where(s => _DbContext.SecRoleLocationModule.Where(inner => inner.UserId == UserId)
              .Select(inner => inner.DistrictID).Distinct().Contains(s.DistrictID)).Select(s => s.UserId).Distinct().ToList();
      var userLists = _DbContext.UserProfiles.Where(x => UserIds.Contains(x.Id)).ToList();
      IEnumerable<UserProfile> filteredUser;
      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filteredUser = userLists
                .Where(c => (c.FirstName + " " + c.MiddleName + " " + c.LastName).Replace(" ", "").ToLower().Contains(param.sSearch.Replace(" ", "").ToLower()) ||
                                      c.user.UserName.ToLower().Contains(param.sSearch.ToLower()) ||
                                      c.SocialSecurityNumber.ToLower().Contains(param.sSearch.ToLower()) ||
                                     c.DOB.ToString().ToLower().Contains(param.sSearch.ToLower()) ||
                                     c.Email.ToString().ToLower().Contains(param.sSearch.ToLower()) ||
                                     c.HireDate.ToString().ToLower().Contains(param.sSearch.ToLower()));
      }
      else
      {
        filteredUser = userLists;
      }
      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
      Func<UserProfile, string> orderingFunction = (c =>
                                                      sortColumnIndex == 0 ? c.FullName :
                                                      sortColumnIndex == 1 ? c.UserId.ToString() :
                                                      sortColumnIndex == 2 ? c.SocialSecurityNumber :
                                                      sortColumnIndex == 3 ? c.DOB.ToString() :
                                                      sortColumnIndex == 4 ? c.Email :
                                                      sortColumnIndex == 5 ? c.HireDate.ToString() :
                                                      sortColumnIndex == 6 ? c.IsActive + "" : "");
      var sortDirection = Request["sSortDir_0"]; // asc or desc
      if (sortDirection == "asc")
        filteredUser = filteredUser.OrderBy(orderingFunction);
      else
        filteredUser = filteredUser.OrderByDescending(orderingFunction);
      //Pagging
      var displayed = filteredUser.Skip(param.iDisplayStart).Take(param.iDisplayLength);
      //Select required columns
      var result = from c in displayed
                   select new[] {
                     c.ProfilePath,
                     c.FirstName+" "+c.MiddleName+" "+c.LastName,
                     c.user.UserName,
                     c.SocialSecurityNumber,
                     String.Format("{0:MM/dd/yyyy}", c.DOB),
                     c.Email,
                     String.Format("{0:MM/dd/yyyy}", c.HireDate),
                     c.IsActive +"",
                     UrlEncryption.EncryptURL(Convert.ToString(c.Id))};
      return Json(new
      {
        sEcho = param.sEcho,
        iTotalRecords = userLists.Count(),
        iTotalDisplayRecords = filteredUser.Count(),
        aaData = result
      }, JsonRequestBehavior.AllowGet);
    }
    [AuthorizeEx]
    [HttpGet]
    public ActionResult RoleModule(string Id)
    {
      if (!string.IsNullOrEmpty(Id))
      {
        Id = UrlEncryption.Decrypt(Id);
      }
      else if (Id == null)
      {
        Id = "1";
      }
      int RoleID = Convert.ToInt32(Id);
      List<App.Data.ViewModels.RoleModuleViewModel> model = new List<App.Data.ViewModels.RoleModuleViewModel>();
      if (Id != null)
      {
        model = (from c in _DbContext.SecModule.Where(x => x.IsActive == true)
                 join p in _DbContext.SecRoleModule.AsNoTracking().Where(x => x.RoleID == RoleID && x.IsActive == true) on c.Id equals p.ModuleID into ps
                 from p in ps.DefaultIfEmpty()
                 select new App.Data.ViewModels.RoleModuleViewModel { RoleID = RoleID.ToString(), ModuleName = c.ModuleName, ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).ToList();
      }
      foreach (var item in model)
      {
        item.RoleID = UrlEncryption.EncryptURL(item.RoleID.ToString());
      }
      var roleListTemp = _DbContext.Roles.Select(x => new RoleModuleViewModel { RoleID = x.Id.ToString(), ModuleName = x.Name }).OrderBy(x => x.ModuleName).ToList();
      foreach (var item in roleListTemp)
      {
        item.RoleID = UrlEncryption.EncryptURL(item.RoleID.ToString());
      }
      var roleList = new SelectList(roleListTemp, "RoleID", "ModuleName", UrlEncryption.EncryptURL(Id));
      ViewBag.RoleID = new SelectList(roleList.Where(x => RoleData.Contains(x.Text.ToLower().Trim())).OrderBy(x => x.Text), "Value", "Text", UrlEncryption.EncryptURL(Id));
      //ViewBag.RoleID = new SelectList(_DbContext.Roles.Where(x => RoleData.Contains(x.Name.ToLower().Trim())).OrderBy(x => x.Name), "Id", "Name"); ;
      return View(model);
    }
    [HttpPost]
    public ActionResult RoleModule(List<App.Data.ViewModels.RoleModuleViewModel> model)
    {
      if (model != null && model.Count > 0)
      {
        model = model.Where(x => x != null).ToList();
      }
      using (var tran = _DbContext.Database.BeginTransaction())
      {
        int Roleid = 0;
        try
        {
          if (ModelState.IsValid)
          {
            if (model != null)
            {
              Roleid = Convert.ToInt32(UrlEncryption.Decrypt(model[0].RoleID));
              var result = _DbContext.SecRoleModule.AsNoTracking().Where(x => x.RoleID == Roleid && x.IsActive == true).ToList();
              if (!result.Any())
              {
                var vmodel = _DbContext.SecModule.Where(x => x.IsActive == true).ToList();
                foreach (var item in vmodel)
                {
                  var rmodel = new SecRoleModule();
                  rmodel.RoleID = Roleid;
                  rmodel.ModuleID = item.Id;
                  rmodel.IsActive = true;
                  _DbContext.Entry(rmodel).State = EntityState.Added;
                  _DbContext.SaveChanges();
                }
              }
              foreach (var items in model)
              {
                var results = _DbContext.SecRoleModule.AsNoTracking().Where(x => x.RoleID == Roleid && x.ModuleID == items.ModuleID && x.IsActive == true).FirstOrDefault();
                if (results != null)
                {
                  results.ViewPermission = items.ViewPermission;
                  results.EditPermission = items.EditPermission;
                  results.AddPermssion = items.AddPermssion;
                  results.DeletePermission = items.DeletePermission;
                  _DbContext.Entry(results).State = EntityState.Modified;
                }
                else
                {
                  var rmodel = new SecRoleModule();
                  rmodel.RoleID = Roleid;
                  rmodel.ModuleID = items.ModuleID;
                  rmodel.IsActive = true;
                  rmodel.ViewPermission = items.ViewPermission;
                  rmodel.EditPermission = items.EditPermission;
                  rmodel.AddPermssion = items.AddPermssion;
                  rmodel.DeletePermission = items.DeletePermission;
                  _DbContext.Entry(rmodel).State = EntityState.Added;
                }

                _DbContext.SaveChanges();
              }
              TempData["success"] = "Roles Assigned Successfully";
              int userid = AppUserManager.GetUserId();
              int currentUserRoleId = _DbContext.UserRole.Where(x => x.UserId == userid).Select(x => x.RoleId).FirstOrDefault();
              if (currentUserRoleId == Roleid)
              {
                string CurrentRegion = GetRegionName();
                var list = (from a in _DbContext.SecModule.Where(x => x.IsActive == true) join b in _DbContext.SecRoleModule.AsNoTracking().Where(x => x.IsActive == true) on a.Id equals b.ModuleID where b.RoleID == Roleid select new App.Data.ViewModels.SessionViewModel { ActionName = a.ActionName, ControllerName = a.ControllerName, ViewPermission = b.ViewPermission, AddPermssion = b.AddPermssion, EditPermission = b.EditPermission, DeletePermission = b.DeletePermission, ParentId = a.ParentId, ModuleID = b.ModuleID, RoleID = b.RoleID, ModuleName = a.ModuleName, ModuleClass = a.ModuleClass, Region = CurrentRegion }).ToList();
                Session["List"] = list;
              }
            }
          }
          tran.Commit();
          foreach (var item in model)
          {
            item.RoleID = UrlEncryption.EncryptURL(item.RoleID.ToString());
          }
          var roleListTemp = _DbContext.Roles.Select(x => new RoleModuleViewModel { RoleID = x.Id.ToString(), ModuleName = x.Name }).OrderBy(x => x.ModuleName).ToList();
          foreach (var item in roleListTemp)
          {
            item.RoleID = UrlEncryption.EncryptURL(item.RoleID.ToString());
          }
          var roleList = new SelectList(roleListTemp, "RoleID", "ModuleName", UrlEncryption.EncryptURL(Roleid.ToString()));
          ViewBag.RoleID = new SelectList(roleList.Where(x => RoleData.Contains(x.Text.ToLower().Trim())).OrderBy(x => x.Text), "Value", "Text", UrlEncryption.EncryptURL(Roleid.ToString()));
          //return View(model);
        }
        catch (Exception ex)
        {
          tran.Rollback();
          TempData["error"] = "Exception is occurring while saving data, something is wrong.";
          ViewBag.RoleID = new SelectList(_DbContext.Roles.OrderBy(x => x.Name), "Id", "Name", Roleid);
          //return View(model);
        }

        return Json("");
      }
    }
    //public JsonResult AddPermission(string[] Data, int RoleId)
    //{
    //  int moduleid = 0;
    //  string[] tempValue;
    //  var result = _DbContext.SecRoleModule.Where(x => x.RoleID == RoleId).ToList();
    //  if (!result.Any())
    //  {
    //    var model = _DbContext.SecModule.ToList();
    //    foreach (var item in model)
    //    {
    //      var rmodel = new SecRoleModule();
    //      rmodel.RoleID = RoleId;
    //      rmodel.ModuleID = item.Id;
    //      _DbContext.Entry(rmodel).State = EntityState.Added;
    //      _DbContext.SaveChanges();
    //    }
    //  }
    //  else
    //  {
    //    foreach (var item in result.Where(x => x.ViewPermission == true || x.EditPermission == true || x.DeletePermission == true || x.AddPermssion == true))
    //    {
    //      item.ViewPermission = false;
    //      item.EditPermission = false;
    //      item.AddPermssion = false;
    //      item.DeletePermission = false;
    //      _DbContext.Entry(item).State = EntityState.Modified;
    //      _DbContext.SaveChanges();
    //    }

    //  }
    //  for (int i = 0; i < Data.Length; i++)
    //  {
    //    tempValue = Data[i].Split(',');
    //    for (int j = 0; j < tempValue.Length; j++)
    //    {
    //      if (tempValue[j].Contains("ViewPermission"))
    //      {
    //        moduleid = Convert.ToInt32(Regex.Replace(tempValue[j], "[^0-9]", "")) + 1;
    //        var vmodel = _DbContext.SecRoleModule.Where(x => x.RoleID == RoleId && x.ModuleID == moduleid).FirstOrDefault();
    //        vmodel.ViewPermission = true;
    //        _DbContext.Entry(vmodel).State = EntityState.Modified;
    //      }
    //      if (tempValue[j].Contains("AddPermssion"))
    //      {
    //        moduleid = Convert.ToInt32(Regex.Replace(tempValue[j], "[^0-9]", "")) + 1;
    //        var vmodel = _DbContext.SecRoleModule.Where(x => x.RoleID == RoleId && x.ModuleID == moduleid).FirstOrDefault();
    //        vmodel.AddPermssion = true;
    //        _DbContext.Entry(vmodel).State = EntityState.Modified;
    //      }
    //      if (tempValue[j].Contains("EditPermission"))
    //      {
    //        moduleid = Convert.ToInt32(Regex.Replace(tempValue[j], "[^0-9]", "")) + 1;
    //        var vmodel = _DbContext.SecRoleModule.Where(x => x.RoleID == RoleId && x.ModuleID == moduleid).FirstOrDefault();
    //        vmodel.EditPermission = true;
    //        _DbContext.Entry(vmodel).State = EntityState.Modified;
    //      }
    //      if (tempValue[j].Contains("DeletePermission"))
    //      {
    //        moduleid = Convert.ToInt32(Regex.Replace(tempValue[j], "[^0-9]", "")) + 1;
    //        var vmodel = _DbContext.SecRoleModule.Where(x => x.RoleID == RoleId && x.ModuleID == moduleid).FirstOrDefault();
    //        vmodel.DeletePermission = true;
    //        _DbContext.Entry(vmodel).State = EntityState.Modified;
    //      }
    //    }
    //    _DbContext.SaveChanges();
    //  }

    //  return Json(1, JsonRequestBehavior.AllowGet);
    //}

    public JsonResult RoleViewIndex(int Id)
    {
      var result = _DbContext.SecModule.Where(x => x.ParentId == Id && x.IsActive == true).Select(x => x.Id).ToList();
      return Json(result, JsonRequestBehavior.AllowGet);
    }
    [AuthorizeEx]
    [HttpGet]
    public ActionResult AddModule()
    {
      int userid = AppUserManager.GetUserId();
      var roleList = _DbContext.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var data1 = (from c in _DbContext.SecModule.Where(x => x.ControllerName == "Account" && x.ActionName == "AddModule")
                   join p in _DbContext.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Module", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      if (data1 != null)
      {
        ViewBag.EditPermission = data1.EditPermission;
      }
      Assembly asm = Assembly.GetExecutingAssembly();
      var controller = asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type)).Select(x => new { Name = x.Name.Remove(x.Name.Length - 10) }).ToList();
      ViewBag.ControllerName = new SelectList(controller, "Name", "Name");
      var action = new List<App.Data.ViewModels.RoleModuleViewModel>();
      ViewBag.ActionName = new SelectList(action, "ActionName", "ActionName");
      var parentList = _DbContext.SecModule.Where(x => x.ParentId == 0 && x.IsActive == true).OrderBy(x => x.ModuleName).Select(x => new { Id = x.Id, Name = x.ModuleName }).ToList();
      parentList.Insert(0, new { Id = 0, Name = "No Parent" });
      ViewBag.ParentId = new SelectList(parentList, "Id", "Name");
      var Databasedata = _DbContext.SecModule.Where(x => x.IsActive == true).ToList();
      List<SecModuleVM> model = Databasedata
.Select(x => new SecModuleVM
{
  Id = UrlEncryption.EncryptURL(Convert.ToString(x.Id)),
  ModuleName = x.ModuleName,
  ModuleDesc = x.ModuleDesc,
  ParentId = x.ParentId,
  ParentIdString = UrlEncryption.EncryptURL(Convert.ToString(x.ParentId)),
  Url = x.Url,
  ActionName = x.ActionName,
  ControllerName = x.ControllerName,
  CreatedBy = x.CreatedBy,
  CreatedOn = x.CreatedOn,
  ModifiedBy = x.ModifiedBy,
  ModifiedOn = x.ModifiedOn,
  IsActive = x.IsActive,
  ModuleClass = x.ModuleClass,
  DisplayOrder = x.DisplayOrder
}).ToList();
      return View(model);
    }



    [HttpPost]
    public ActionResult AddModule(App.Data.ViewModels.SessionViewModel model)
    {
      if (ModelState.IsValid)
      {
        var secmodule = new SecModule();
        secmodule.ActionName = model.ActionName;
        secmodule.ParentId = model.ParentId;
        secmodule.ModuleName = model.ModuleName;
        secmodule.ControllerName = model.ControllerName;
        secmodule.ModuleClass = model.ModuleClass;
        secmodule.IsActive = true;
        _DbContext.Entry(secmodule).State = EntityState.Added;
        _DbContext.SaveChanges();
        int secmoduleCount = (_DbContext.SecRoleModule.AsNoTracking().AsNoTracking().Where(x => x.RoleID == 1 && x.IsActive == true)).Count();
        int secrolemoduleCount = (_DbContext.SecRoleModule.AsNoTracking().Where(x => x.RoleID == 2 && x.IsActive == true)).Count();
        if (secmoduleCount > 0)
        {
          var secRoleModule = new SecRoleModule();
          secRoleModule.ModuleID = secmodule.Id;
          secRoleModule.RoleID = 1;
          secRoleModule.IsActive = true;
          _DbContext.Entry(secRoleModule).State = EntityState.Added;
        }
        if (secrolemoduleCount > 0)
        {
          var secRoleModule = new SecRoleModule();
          secRoleModule.ModuleID = secmodule.Id;
          secRoleModule.RoleID = 2;
          secRoleModule.IsActive = true;
          _DbContext.Entry(secRoleModule).State = EntityState.Added;
        }
        _DbContext.SaveChanges();
        TempData["success"] = "Record Saved Successfully";
        return RedirectToAction("AddModule");
      }
      Assembly asm = Assembly.GetExecutingAssembly();
      var controller = asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type)).Select(x => new { Name = x.Name.Remove(x.Name.Length - 10) }).ToList();
      ViewBag.ControllerName = new SelectList(controller, "Name", "Name", model.ControllerName);
      var action = ((asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type))).Where(x => x.Name == model.ControllerName + "Controller")//filter controllers
          .SelectMany(type => type.GetMethods())
          .Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType)).Select(x => x.Name)).Distinct();
      ViewBag.ActionName = new SelectList(action, "Name", "Name", model.ActionName);
      ViewBag.ParentId = new SelectList(_DbContext.SecModule.Where(x => x.ParentId == 0 && x.IsActive == true).OrderBy(x => x.ModuleName), "Id", "ModuleName", model.ParentId);
      TempData["error"] = "Please try Again...";
      return View(model);
    }
    [AuthorizeEx]
    [HttpGet]
    public ActionResult EditModule(string Id)
    {
      string IdUrl = Helper.UrlEncryption.Decrypt(Convert.ToString(Id));
      if (Id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      SecModule secModule = _DbContext.SecModule.Find(Convert.ToInt32(IdUrl));
      if (secModule == null)
      {
        return HttpNotFound();
      }
      Assembly asm = Assembly.GetExecutingAssembly();
      var controller = asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type)).Select(x => new { Name = x.Name.Remove(x.Name.Length - 10) }).ToList();
      ViewBag.ControllerName = new SelectList(controller, "Name", "Name", secModule.ControllerName);
      var action = ((asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type))).Where(x => x.Name == secModule.ControllerName + "Controller")//filter controllers
          .SelectMany(type => type.GetMethods())
          .Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType)).Select(x => new { Name = x.Name })).Distinct();
      ViewBag.ActionName = new SelectList(action, "Name", "Name", secModule.ActionName);
      var parentList = _DbContext.SecModule.Where(x => x.ParentId == 0 && x.IsActive == true).OrderBy(x => x.ModuleName).Select(x => new { Id = x.Id, Name = x.ModuleName }).ToList();
      parentList.Insert(0, new { Id = 0, Name = "No Parent" });
      ViewBag.ParentId = new SelectList(parentList, "Id", "Name", secModule.ParentId);

      return View(secModule);
    }
    public JsonResult ModuleClassAjax()
    {
      var list = _DbContext.SecModule.OrderBy(x => x.ModuleClass).Select(x => x.ModuleClass).Distinct().ToList();
      return Json(list, JsonRequestBehavior.AllowGet);
    }
    [HttpPost]
    public ActionResult EditModule(SecModuleVM model)
    {
      string Id = UrlEncryption.Decrypt(model.Id);
      int id = Convert.ToInt32(Id);
      var MasterSecModule = _DbContext.SecModule.FirstOrDefault(x => x.Id == id);
      if (ModelState.IsValid)
      {
        MasterSecModule.ModuleName = model.ModuleName;
        MasterSecModule.ModuleDesc = model.ModuleDesc;
        MasterSecModule.ActionName = model.ActionName;
        MasterSecModule.ControllerName = model.ControllerName;
        MasterSecModule.ModuleClass = model.ModuleClass;
        MasterSecModule.ParentId = model.ParentId;
        MasterSecModule.IsActive = true;
        _DbContext.Entry(MasterSecModule).State = EntityState.Modified;
        int result = _DbContext.SaveChanges();
        //DbContextHelper.dBsavechanges(DbContext);
        if (result > 0)
        {
          TempData["success"] = "Record Saved Successfully";
          return RedirectToAction("AddModule");
        }
      }
      Assembly asm = Assembly.GetExecutingAssembly();
      var controller = asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type)).Select(x => new { Name = x.Name.Remove(x.Name.Length - 10) }).ToList();
      ViewBag.ControllerName = new SelectList(controller, "Name", "Name", model.ControllerName);
      var action = ((asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type))).Where(x => x.Name == model.ControllerName + "Controller")//filter controllers
          .SelectMany(type => type.GetMethods())
          .Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType)).Select(x => new { Name = x.Name })).Distinct();
      ViewBag.ActionName = new SelectList(action, "Name", "Name", model.ActionName);
      var parentList = _DbContext.SecModule.Where(x => x.ParentId == 0 && x.IsActive == true).OrderBy(x => x.ModuleName).Select(x => new { Id = x.Id, Name = x.ModuleName }).ToList();
      parentList.Insert(0, new { Id = 0, Name = "No Parent" });
      ViewBag.ParentId = new SelectList(parentList, "Id", "Name", model.ParentId);

      TempData["error"] = "Please try Again...";
      return View(model);
    }
    public JsonResult FindAction(string ControllerName)
    {
      Assembly asm = Assembly.GetExecutingAssembly();
      var action = ((asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type))).Where(x => x.Name == ControllerName + "Controller")//filter controllers
          .SelectMany(type => type.GetMethods())
          .Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType)).Select(x => x.Name)).Distinct();

      return Json(action, JsonRequestBehavior.AllowGet);
    }

    public ActionResult ModuleAjaxHandler(JQueryDataTableParamModel param)
    {
      var List = _DbContext.SecModule;
      IEnumerable<SecModule> filtered;


      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => c.ModuleName.ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<SecModule, string> orderingFunction = (c => sortColumnIndex == 0 ? c.ModuleName : "");

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
                         c.ModuleName
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
    public JsonResult SocialSecurityNoIndex(string Number, int UserId)
    {
      int result = 0;
      if (UserId != 0)
      {
        result = _DbContext.UserProfiles.Where(x => x.SocialSecurityNumber == Number && x.UserId != UserId && x.IsActive == true).Count();
      }
      else
      {
        result = _DbContext.UserProfiles.Where(x => x.SocialSecurityNumber == Number && x.IsActive == true).Count();
      }
      return Json(result, JsonRequestBehavior.AllowGet);
    }

    public ActionResult RoleIndex()
    {
      return View();
    }
    public ActionResult AddRoleAjaxHandler(JQueryDataTableParamModel param)
    {
      var List = _DbContext.Roles;
      IEnumerable<AppRole> filtered;


      if (!string.IsNullOrEmpty(param.sSearch))
      {
        filtered = List
           .Where(c => c.Name.ToLower().Contains(param.sSearch.ToLower()));

      }
      else
      {
        filtered = List;
      }

      //Sorting through column index
      var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

      Func<AppRole, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name :
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
                     c.Id + "",
                         c.Name,
                         ""
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
    public ActionResult RoleEdit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      AppRole role = _DbContext.Roles.Find(id);
      if (role == null)
      {
        return HttpNotFound();
      }
      return View(role);
    }
    [HttpPost]
    public ActionResult RoleEdit(AppRole role)
    {
      var verify = _DbContext.Roles.Where(x => x.Name == role.Name && x.Id != role.Id).FirstOrDefault();
      if (verify != null)
      { TempData["error"] = "This role already exist.."; }
      else
      {
        if (ModelState.IsValid)
        {
          _DbContext.Entry(role).State = EntityState.Modified;
          _DbContext.SaveChanges();
          //DbContextHelper.dBsavechanges(DbContext);
          return RedirectToAction("RoleIndex");
        }
      }
      return View(role);
    }
    public ActionResult RoleAdd()
    {
      return View();
    }
    [HttpPost]
    public ActionResult RoleAdd(AppRole role)
    {
      var verify = _DbContext.Roles.Where(x => x.Name == role.Name).FirstOrDefault();
      if (verify != null)
      { TempData["error"] = "This Role already exist.."; }
      else
      {
        if (ModelState.IsValid)
        {
          _DbContext.Roles.Add(role);
          _DbContext.SaveChanges();
          //DbContextHelper.dBsavechanges(DbContext);
          return RedirectToAction("RoleIndex");
        }
      }
      return View(role);
    }
    [AuthorizeEx]
    [HttpGet]
    public ActionResult ChangeUserPassword(int? Id)
    {
      int userid = AppUserManager.GetUserId();
      var roleList = _DbContext.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
      var model = (from c in _DbContext.SecModule.Where(x => x.ControllerName == "Account" && x.ActionName == "ChangeUserPassword")
                   join p in _DbContext.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                   from p in ps.DefaultIfEmpty()
                   select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Change Password", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      if (model != null)
      {
        ViewBag.AddPermission = model.AddPermssion;
        ViewBag.EditPermission = model.EditPermission;
      }
      ViewBag.ProfilePage = Id;
      return View();
    }
    [HttpPost]
    public async Task<ActionResult> ChangeUserPassword(App.Web.Models.ChangePasswordViewModel model)
    {
      int users = AppUserManager.GetUserId();
      if (ModelState.IsValid)
      {
        string UserName = _DbContext.Users.Where(x => x.Id == users).Select(x => x.UserName).FirstOrDefault();
        var user = await OwinUserManger.FindAsync(UserName, model.OldPassword);
        if (user == null || !(await OwinUserManger.IsEmailConfirmedAsync(user.Id)))
        {
          TempData["error"] = "Please Check Your Old Password, And Try Again";
          return RedirectToAction("ChangeUserPassword");
        }
        if (model.NewPassword == model.OldPassword)
        {
          TempData["error"] = "New password cannot be the same as the old password. Please change New password.";
          return RedirectToAction("ChangeUserPassword");
        }

        await OwinUserManger.RemovePasswordAsync(user.Id);
        var result = await OwinUserManger.AddPasswordAsync(user.Id, model.NewPassword);
        if (result != null)
        {
          TempData["success"] = "Password Change Successfully..Login With New Password";
        }
        return RedirectToAction("LogOffManually");
      }
      var roleList = _DbContext.SecRoleLocationModule.Where(x => x.UserId == users).Select(x => x.RoleID).FirstOrDefault();
      var model1 = (from c in _DbContext.SecModule.Where(x => x.ControllerName == "Account" && x.ActionName == "ChangeUserPassword")
                    join p in _DbContext.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
                    from p in ps.DefaultIfEmpty()
                    select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Change Password", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      if (model1 != null)
      {
        ViewBag.AddPermission = model1.AddPermssion;
        ViewBag.EditPermission = model1.EditPermission;
      }
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> ChangeUserPasswordAjax(string changePasswordUserId_Hdn, /*string OldPassword,*/ string NewPassword, string ConfirmPassword)
    {
      try
      {
        int userProfileId = Convert.ToInt32(UrlEncryption.Decrypt(changePasswordUserId_Hdn));
        int userId = _DbContext.UserProfiles.Where(x => x.Id == userProfileId).Select(x => x.UserId).FirstOrDefault();
        string UserName = _DbContext.Users.Where(x => x.Id == userId).Select(x => x.UserName).FirstOrDefault();
        var user = await OwinUserManger.FindByNameAsync(UserName);
        //if (user == null || !(await OwinUserManger.IsEmailConfirmedAsync(user.Id)))
        //{
        //  return Json(new { status = false, message = "Please Check Your Old Password, And Try Again" }, JsonRequestBehavior.AllowGet);
        //}

        await OwinUserManger.RemovePasswordAsync(user.Id);
        var result = await OwinUserManger.AddPasswordAsync(user.Id, NewPassword);
        if (result.Succeeded)
        {
          return Json(new { status = true, message = "Password Changed Successfully." }, JsonRequestBehavior.AllowGet);
        }
        else
        {
          StringBuilder sb = new StringBuilder();
          foreach (var error in result.Errors)
          {
            sb.AppendLine(error);
          }

          return Json(new { status = false, message = sb.ToString() }, JsonRequestBehavior.AllowGet);
        }
      }
      catch (Exception ex)
      {
        return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
      }
    }

    public JsonResult AjaxRemove(string Id, string name)
    {
      int result = 0;
      string IdUrl = UrlEncryption.Decrypt(Id);
      int id = Convert.ToInt32(IdUrl);
      if (id != 0)
      {
        bool action = MasterDeleteRepo.AjaxDelete(id, name);
        if (action)
        {
          //UserProfile entity = await _DbContext.UserProfiles.FindAsync(Convert.ToInt32(IdUrl));

          UserProfile entity = db.UserProfiles.Where(x => x.Id == id).FirstOrDefault();
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
    // new work edit applicant
    [HttpGet]
    [AuthorizeEx]
    public ActionResult ApplicantsEdit(string Id, string TableName)
    {

      if (!string.IsNullOrEmpty(Id))
      {
        Id = UrlEncryption.Decrypt(Id);
      }
      else if (Id == null)
      {
        int userid = AppUserManager.GetUserId();
        var Role_id = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();
        Id = Role_id.ToString();
        //Id = "1";
        TableName = "MasterEmployee";
      }
      var items = new List<SelectListItem>
        {
            new SelectListItem { Text = "Beneficiary Details", Value = "MasterEmployee" },
            new SelectListItem { Text = "Monthly Pension", Value = "MasterEmployeeIncomes" },
            new SelectListItem { Text = "Bank Details", Value = "MasterEmpBankDetails" },
            new SelectListItem { Text = "Incomes Details", Value = "MasterEmployeeIncomes" }
        };
      ViewBag.TableName = new SelectList(items, "Value", "Text", "MasterEmployee");

      int RoleID = Convert.ToInt32(Id);
      List<App.Data.ViewModels.EditApplicantFieldsViewModel> model = new List<App.Data.ViewModels.EditApplicantFieldsViewModel>();
      if (Id != null)
      {

        model = _DbContext.EditApplicantFields.Where(p2 => p2.RoleID == RoleID && p2.TableName.Trim() == TableName.Trim())
                 .Select(p2 => new App.Data.ViewModels.EditApplicantFieldsViewModel
                 {
                   Id = p2.Id,
                   RoleID = RoleID.ToString(),
                   FieldName = p2.FieldName,
                   TableName = p2.TableName,
                   AllowEdit = p2.AllowEdit == null ? false : (bool)p2.AllowEdit,
                 })
                 .ToList();
      }

      foreach (var item in model)
      {
        item.RoleID = UrlEncryption.EncryptURL(item.RoleID.ToString());
      }
      var roleListTemp = _DbContext.Roles.Select(x => new RoleModuleViewModel { RoleID = x.Id.ToString(), ModuleName = x.Name }).OrderBy(x => x.ModuleName).ToList();
      foreach (var item in roleListTemp)
      {
        item.RoleID = UrlEncryption.EncryptURL(item.RoleID.ToString());
      }
      string p = UrlEncryption.EncryptURL(Id);
      var roleList = new SelectList(roleListTemp, "RoleID", "ModuleName", p);
      ViewBag.RoleID = new SelectList(roleList.Where(x => RoleData.Contains(x.Text.ToLower().Trim())).OrderBy(x => x.Text), "Value", "Text", UrlEncryption.EncryptURL(Id));

      ////ViewBag.RoleID = new SelectList(_DbContext.Roles.Where(x => RoleData.Contains(x.Name.ToLower().Trim())).OrderBy(x => x.Name), "Id", "Name"); 
      return View(model);
    }
    [HttpPost]
    public ActionResult ApplicantsEdit(List<App.Data.ViewModels.EditApplicantFieldsViewModel> model, string TableName)
    {

      int Roleid = 0;
      int id = 0;
      int userId = 0;

      var tableNames = new List<SelectListItem>
        {
            new SelectListItem { Text = "Beneficiary Details", Value = "MasterEmployee" },
            new SelectListItem { Text = "Monthly Pension", Value = "MasterEmployeeIncomes" },
            new SelectListItem { Text = "Bank Details", Value = "MasterEmpBankDetails" },
            new SelectListItem { Text = "Incomes Details", Value = "MasterEmployeeIncomes" }
        };
      ViewBag.TableName = new SelectList(tableNames, "Value", "Text", TableName);

      try
      {
        if (ModelState.IsValid)
        {
          foreach (var items in model)
          {
            userId = AppUserManager.GetUserId();
            Roleid = Convert.ToInt32(UrlEncryption.Decrypt(items.RoleID));
            id = Convert.ToInt32(items.Id);
            var results = _DbContext.EditApplicantFields.AsNoTracking().Where(x => x.RoleID == Roleid && x.Id == id)
               .ToList();
            foreach (var it in results)
            {
              if (items.FieldName == it.FieldName && items.AllowEdit != it.AllowEdit)
              {
                //if (items.AllowEdit != it.AllowEdit)
                //{
                it.AllowEdit = items.AllowEdit;
                it.ModifiedBy = userId;
                it.ModifiedOn = DateTime.Now;
                //}

                _DbContext.Entry(it).State = EntityState.Modified;
                //_DbContext.SaveChanges();
              }
            }

            _DbContext.SaveChanges();

          }

          int result = _DbContext.SaveChanges();

          TempData["success"] = "Roles Assigned Successfully";
          return RedirectToAction("ApplicantsEdit");
        }

        foreach (var item in model)
        {
          item.RoleID = UrlEncryption.EncryptURL(item.RoleID.ToString());
        }
        var roleListTemp = _DbContext.Roles.Select(x => new RoleModuleViewModel { RoleID = x.Id.ToString(), ModuleName = x.Name }).OrderBy(x => x.ModuleName).ToList();
        foreach (var item in roleListTemp)
        {
          item.RoleID = UrlEncryption.EncryptURL(item.RoleID.ToString());
        }
        var roleList = new SelectList(roleListTemp, "RoleID", "ModuleName", UrlEncryption.EncryptURL(Roleid.ToString()));
        //ViewBag.RoleID = roleList;
        ViewBag.RoleID = new SelectList(roleList.Where(x => RoleData.Contains(x.Text.ToLower().Trim())).OrderBy(x => x.Text), "Value", "Text", UrlEncryption.EncryptURL(Roleid.ToString()));
        return View(model);
        // return RedirectToAction("ApplicantsEdit");
      }
      catch (Exception ex)
      {

        TempData["error"] = "Exception is occurring while saving data, something is wrong.";
        ViewBag.RoleID = new SelectList(_DbContext.Roles.OrderBy(x => x.Name), "Id", "Name", Roleid);
        return View(model);
      }



    }

    [HttpGet]
    [AuthorizeEx]
    public ActionResult ReuploadPermission()
    {
      int userid = AppUserManager.GetUserId();
      //var roleList = db.SecRoleLocationModule.Where(x => x.UserId == userid).Select(x => x.RoleID).FirstOrDefault();

      //var model = (from c in db.SecModule.Where(x => x.ControllerName == "PensionProcess" && x.ActionName == "PrintBankListing")
      //             join p in db.SecRoleModule.AsNoTracking().Where(x => x.RoleID == roleList && x.IsActive == true) on c.Id equals p.ModuleID into ps
      //             from p in ps.DefaultIfEmpty()
      //             select new RoleModuleViewModel { RoleID = roleList.ToString(), ModuleName = "Print Bank Listing", ParentId = c.ParentId, ModuleID = c.Id, ViewPermission = p.ViewPermission == null ? false : (bool)p.ViewPermission, AddPermssion = p.AddPermssion == null ? false : (bool)p.AddPermssion, EditPermission = p.EditPermission == null ? false : (bool)p.EditPermission, DeletePermission = p.DeletePermission == null ? false : (bool)p.DeletePermission }).FirstOrDefault();

      //if (model != null)
      //{
      //  ViewBag.AddPermission = model.AddPermssion;
      //  ViewBag.EditPermission = model.EditPermission;
      //}
      PensionProcessViewModel Paysearch = ShowActiveBatch();
      string disData = string.Empty;
      //BindCmbBanckCode(string.Empty);
      //BindComboEmployeeType(Paysearch.EmpType);
      generatePensionProcess = new GeneratePensionProcessModel();
      generatePensionProcess.DepositDate = Paysearch.PayrollDate;

      //generatePensionProcess.PayDate = DVOApplicationUserInfo.DateConvertion(DateTime.Now);
      ViewBag.pybatchid = Paysearch.pybatchid;
      //Paysearch.RegionNames = Paysearch.RegionNames;
      //if (Paysearch.RegionNames != "" && Paysearch.RegionNames != null)
      //{
      //  disData = Paysearch.RegionNames.ToLower().Trim();
      //}
      //ViewBag.Group = GetUsersAssignedLocations("", 0, 0, disData);

      Dictionary<string, string> PensionDetailByMonthYear = BLLPYBatchProcessStybatchr.PensionDetailByMonthYear();
      ViewBag.CurrentMonthBatch = PensionDetailByMonthYear;
      if (PensionDetailByMonthYear == null)
      {
        ViewBag.pybatchid = 0;
      }
      else
      {
        ViewBag.pybatchid = PensionDetailByMonthYear["pybatchid"];
      }
      //if (PensionDetailByMonthYear != null)
      //{

      //  string[] regions = PensionDetailByMonthYear["Districts"].Split(',').Select(region => region.Trim()).ToArray();
      //  var filteredRegions = regions.Where(region => region != "JAMMU REGION" && region != "KASHMIR REGION");
      //  string Paydistricts = string.Join(", ", filteredRegions);

      //  object[] parameters1 = new object[2];
      //  parameters1[0] = PensionDetailByMonthYear["pybatchid"];
      //  parameters1[1] = PensionDetailByMonthYear["Districts"];

      //  DataSet ds_ = new DataSet();
      //  StringBuilder SQL = new StringBuilder();
      //  DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      //  DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //  object objTransaction = objDALBaseClassHelper.GetTransactionObject();
      //  //SQL.Append("SELECT Districts FROM Payroll_Process_Header WHERE pybatchid=" + parameters1[0].ToString());
      //  SQL.Append("SELECT ok_to_post  FROM Process_PayEmployee PPE WITH (NOLOCK) JOIN MasterEmployee ME WITH (NOLOCK) ON ME.empl_code = PPE.empl_code JOIN Payroll_Process_Header PPH WITH (NOLOCK) ON PPE.pybatchid =PPH.pybatchid JOIN Payroll_Process_Details PPD WITH (NOLOCK) ON PPH.pybatchid =PPD.pybatchid JOIN Process_DirectDeposit_Header PDH WITH (NOLOCK) ON PPH.pybatchid = PDH.pybatchid JOIN Process_DirectDeposit_Details PDD WITH (NOLOCK) ON PDH.doc_no = PDD.doc_no WHERE PPE.ok_to_post IN ('N','Y') AND  ");

      //  if (parameters1[0] != null)
      //    SQL.Append("PPH.pybatchid='" + parameters1[0].ToString() + "'");

      //  if (parameters1[1] != null)
      //    SQL.Append(" and PPH.Districts IN  ('" + parameters1[1].ToString() + "')");


      //  SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), objDalBaseClass.ConnectionString);
      //  da.Fill(ds_);

      //  if (ds_.Tables.Count > 0 && ds_.Tables[0].Rows.Count > 0)
      //  {
      //    ViewBag.responseok = true;
      //    IsPendioGenrated = true;
      //    ViewBag.ispensiongenerated = IsPendioGenrated;
      //  }
      //  else
      //  {
      //    ViewBag.responseok = false;
      //    IsPendioGenrated = false;
      //    ViewBag.ispensiongenerated = IsPendioGenrated;
      //  }
      //  ViewBag.ispensiongenerated = IsPendioGenrated;

      //}
      //return PartialView("~/Views/PensionProcess/DirectDeposits/PrintBankListing.cshtml", generatePensionProcess);
      return View(generatePensionProcess);
    }

    private PensionProcessViewModel ShowActiveBatch()
    {
      try
      {
        PensionProcessViewModel Paysearch = new PensionProcessViewModel();
        /*payrollSearchControl1.ClearControls();
        dtEOPDate.Checked = false;
        dtPayrollDate.Checked = false;
        btnstart.Enabled = true;
        btnstart.Text = "No Active Batch for Payroll Process, Click to Start New Batch.";*/

        // lstDVOPYBatchProcessStybatchr.
        List<DVOPYBatchProcessStybatchr> lstDVOPYBatchProcessStybatchr = BLLPYBatchProcessStybatchr.GetActiveBatch();
        if (lstDVOPYBatchProcessStybatchr != null && lstDVOPYBatchProcessStybatchr.Count > 0)
        {
          DVOPYBatchProcessStybatchr obj = lstDVOPYBatchProcessStybatchr[0];
          pObjBatch = lstDVOPYBatchProcessStybatchr[0];
          /*lblBatchID.Text = Convert.ToString(obj.pybatchid);
          lblProcessStartedOn.Text = obj.startedon;
          lblStartBy.Text = Convert.ToString(obj.insertby);
          lblStartMachineInfo.Text = obj.insertmachineinfo;*/

          Paysearch.pybatchid = obj.pybatchid;
          Paysearch.searchcriteria = obj.searchcriteria;
          //Paysearch.processstartedon = obj.startedon == null ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(obj.startedon);
          //Paysearch.processstartedon = obj.endedon == null ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(obj.endedon);
          Paysearch.errormessage = obj.errormessage;
          Paysearch.status = obj.status;
          Paysearch.RegionNames = obj.Districts;

          if (obj.searchcriteria.Trim().Length > 0)
          {

            string[] searchCriteria = obj.searchcriteria.Split(new string[] { "_R_" }, StringSplitOptions.None);

            string field = string.Empty;
            string value = string.Empty;
            foreach (string str in searchCriteria)
            {
              string[] sca = str.Split('=');
              if (sca.Length > 1)
              {
                field = sca[0].Trim();
                value = sca[1].Trim();
              }
              switch (field)
              {
                case "District":
                  Paysearch.RegionNames = value;
                  break;
                case "First Name":
                  Paysearch.FirstName = value;
                  break;

                case "Last Name":
                  Paysearch.LastName = value;
                  break;
                case "SPay Period":
                  Paysearch.PayPeriod = value;
                  break;
                case "Full Time":
                  Paysearch.FullTime = value;
                  break;
                case "Pensioner Type":
                  Paysearch.EmpType = value;
                  break;
                case "Job Code":
                  Paysearch.JobCode = value;
                  break;
                case "Job Title":
                  Paysearch.Title = value;
                  break;
                case "Pensioner Code":
                  Paysearch.EmployeeCode = value;
                  break;
                case "Last Pay Date":
                  Paysearch.LPayDate = DVOApplicationUserInfo.ParseDateConvertion(value);
                  Paysearch.LPayDateChecked = true;
                  break;
                case "End Of Period":
                  Paysearch.EOPDate = DVOApplicationUserInfo.ParseDateConvertion(value);
                  Paysearch.EOPDateChecked = true;
                  break;
                case "Payroll Date":
                  Paysearch.PayrollDate = DVOApplicationUserInfo.ParseDateConvertion(value);
                  Paysearch.PayrollDateChecked = true;
                  break;
              }
            }
          }
          if (obj.status == 1)
          {
            /*lblStatus.Text = "ACTIVE";
            btnstart.Enabled = false;
            payrollSearchControl1.Enabled = false;
            dtEOPDate.Enabled = false;
            dtPayrollDate.Enabled = false;
            btnstart.Text = "Batch : " + obj.pybatchid.ToString() + " is Active for Payroll Process.";*/

            return Paysearch;
          }

          /*DVOPYBatchProcessDetailStybatchd objDVOPYBatchProcessDetailStybatchd = new DVOPYBatchProcessDetailStybatchd();
          objDVOPYBatchProcessDetailStybatchd.pybatchid = obj.pybatchid;
          List<DVOPYBatchProcessDetailStybatchd> listDVOPYBatchProcessDetailStybatchd = BLLPYBatchProcessDetailStybatchd.GetData(ref objDVOPYBatchProcessDetailStybatchd);
          if (listDVOPYBatchProcessDetailStybatchd != null && listDVOPYBatchProcessDetailStybatchd.Count > 0)
          {
            //customDataGridview1.DataSource = listDVOPYBatchProcessDetailStybatchd;
          }*/
        }
        //TempData["error"] = "No Active Batch for Pension Process,Please Create a New Batch & Refresh";
        //TempData["success"] = "No Active Batch for Pension Process,Generate the pension";

        return Paysearch;
      }
      catch (Exception Ex)
      {
        TempData["error"] = "Please Try Again.." + Ex.Message;
        //ExceptionManagement.ExceptionManager.Publish(Ex);
        //throw Ex;
        ExceptionManagement.ExceptionManager.Publish(Ex);
        return null;
      }

    }

    private void BindCmbBanckCode(string bank_code)
    {
      DVOUpdateBankCode obj = new DVOUpdateBankCode();
      List<DVOUpdateBankCode> ObjList = BLLUpdateBankCode.GetBankCodeDetails(ref obj).Select(x =>
          new DVOUpdateBankCode
          {
            bank_code = x.bank_code,
            bank_desc = string.Format("{0} | {1}", x.bank_code, x.bank_desc),
          }
          ).ToList();
      obj.bank_code = string.Empty;
      obj.bank_desc = "--Select--";
      ObjList.Sort(new DVOUpdateBankCode_BankCode_Comparer());
      ObjList.Insert(0, obj);
      bank_code = string.IsNullOrWhiteSpace(bank_code) ? string.Empty : bank_code;
      ViewBag.BanckCode = new SelectList(ObjList, "bank_code", "bank_desc", bank_code);

    }

    private void BindComboEmployeeType(string type_code)
    {
      //make object to pass as parameter of search function
      DVOMasterEmpTypes objDVOMasterEmpTypes = new DVOMasterEmpTypes();
      //call getDate function of BLL
      var defaultItem = new SelectListItem { Value = "ALL", Text = "ALL" };
      List<DVOMasterEmpTypes> listDVOMasterEmpTypes = BLLPayEmployeeTypes.GetEmployeeTypes(ref objDVOMasterEmpTypes).Select(s => new DVOMasterEmpTypes
      {
        type_code = s.type_code,
        //description = string.Format("{0} | {1} | {2} | {3} | {4}", s.type_code, s.description, s.pay_period, s.empl_status, s.hold_pymnt)
        description = string.Format("{0} | {1}", s.type_code, s.description)
      }).ToList();
      listDVOMasterEmpTypes.Insert(0, new DVOMasterEmpTypes { type_code = "ALL", description = "ALL | ALL" }); // Insert the default item at the beginning
      var selectListItems = listDVOMasterEmpTypes
              .Select(empType => new SelectListItem
              {
                Value = empType.type_code,
                Text = empType.description
              })
              .ToList();

      objDVOMasterEmpTypes = null;
      //ViewBag.EmployeeType = new SelectList(selectListItems, "Value", "Text", (string.IsNullOrEmpty(EmployeeType) ? null : EmployeeType));

      ViewBag.EmployeeType = new SelectList(selectListItems, "type_code", "description", type_code);
      //check list is null or not
      if (listDVOMasterEmpTypes != null)
      {
        //make a blank object and insert into first position
        DVOMasterEmpTypes tmpDVOMasterEmpTypes = new DVOMasterEmpTypes();
        tmpDVOMasterEmpTypes.type_code = string.Empty;
        tmpDVOMasterEmpTypes.description = "-- Select --";
        tmpDVOMasterEmpTypes.pay_period = "";
        tmpDVOMasterEmpTypes.empl_status = "";
        tmpDVOMasterEmpTypes.hold_pymnt = "";
        listDVOMasterEmpTypes.Insert(0, tmpDVOMasterEmpTypes);
        //check list has some items or not
        if (listDVOMasterEmpTypes.Count > 0)
        {
          //bind combo box with list
          type_code = string.IsNullOrWhiteSpace(type_code) ? string.Empty : type_code;
          ViewBag.EmployeeType = new SelectList(listDVOMasterEmpTypes, "type_code", "description", type_code);
        }
      }
    }

    public string SFTPfileSendAjax(string batch_value, string distict_value, GeneratePensionProcessModel generatePensionProcessModel)
    {
      try
      {
        object[] parameters1 = new object[2];
        parameters1[0] = batch_value;
        parameters1[1] = distict_value;

        DataSet ds = new DataSet();
        StringBuilder SQL = new StringBuilder();
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        object objTransaction = objDALBaseClassHelper.GetTransactionObject();
        string formattedName = "";

        SQL.Append("SELECT FTPFileName FROM Media_Queue WHERE ");
        if (parameters1[0] != null)
          SQL.Append("RecordId=" + parameters1[0].ToString());

        if (parameters1[1] != null)
          SQL.Append(" AND Districts='" + parameters1[1].ToString() + "'");

        // Execute the query and fill the DataSet
        SqlDataAdapter da = new SqlDataAdapter(SQL.ToString(), objDalBaseClass.ConnectionString);
        da.Fill(ds);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
          formattedName = ds.Tables[0].Rows[0][0].ToString();
        }

        int UserId = AppUserManager.GetUserId();
        Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
        // Define the path for sql create SVC

        //var region = GetRegionName();
        var region = GetRegionName();
        var directoryName = region == "KASHMIR REGION" ? "K_BankMediaFile" : "J_BankMediaFile";
        //string filePath = ftpSetting["localFilePath"] + $"\\DataFiles\\{directoryName}\\" + formattedName;
        string filePath = Helper.Helper.AddExcelSheetIntoDatabasePath(region, directoryName, formattedName);

        //string filePath = ftpSetting["localFilePath"] + "\\DataFiles\\BankMediaFile\\" + formattedName;
        // Create an FTP client
        WebClient ftpClient = new WebClient();
        ftpClient.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);

        //string path = ftpSetting["ftpServerUrl"] + $"/DataFiles/{directoryName}/" + formattedName;

        string path = Helper.Helper.AddExcelSheetIntoDatabasePath1(region, directoryName, formattedName);

        //string path = ftpSetting["ftpServerUrl"] + ("/DataFiles/BankMediaFile/" + formattedName);

        // File path, attampt, dealy in attampt
        bool isExist = Helper.Helper.FileCheckInFTP(path, 3, 30);

        if (isExist)
        {
          // Download the file from the FTP server
          byte[] fileData = ftpClient.DownloadData(path);

          // Specify the file path where you want to save the uploaded file

          //DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/BankMediaFile"));
          //DirectoryInfo dis = di.GetDirectories(DateTime.Now.ToShortDateString().Replace("/", "_")).Any() ? di.GetDirectories(DateTime.Now.ToShortDateString().Replace("/", "_")).FirstOrDefault() : di.CreateSubdirectory(DateTime.Now.ToShortDateString().Replace("/", "_"));
          //string path = Path.Combine(dis.FullName, generatePensionProcessModel.BanckCode.Trim() + ".txt");

          //var regions = GetRegionName();
          var regions = GetRegionName();
          var directoryNames = regions == "KASHMIR REGION" ? "K_Disbursement" : "J_Disbursement";
          //string serverMapPath = Server.MapPath($"~/BankMediaFile/{directoryNames}");
          string serverMapPath = Helper.Helper.GetAllFilesPath(region, directoryNames);




          //string serverMapPath = Server.MapPath("~/BankMediaFile/JK_Disbursement");
          if (!Directory.Exists(serverMapPath))
          {
            // Attempt to create the directory
            Directory.CreateDirectory(serverMapPath);
          }
          // DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/BankMediaFile"));
          //DirectoryInfo dis = di.GetDirectories(DateTime.Now.ToShortDateString().Replace("/", "_")).Any() ? di.GetDirectories(DateTime.Now.ToShortDateString().Replace("/", "_")).FirstOrDefault() : di.CreateSubdirectory(DateTime.Now.ToShortDateString().Replace("/", "_"));
          // Add file name with directory
          string csvFilePath = Path.Combine(serverMapPath, formattedName);

          // Save the file to the server
          System.IO.File.WriteAllBytes(csvFilePath, fileData);

          string excelFilePath = csvFilePath.Replace(".csv", ".xlsx");

          // delete file in safe way
          FileHelper fileHelper = new FileHelper();
          bool isFileDeleted = fileHelper.TryDeleteFile(excelFilePath);

          Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
          Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(csvFilePath);
          wb.SaveAs(excelFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
          wb.Close(false);
          app.Quit();

          bool isCsvFileDeleted = fileHelper.TryDeleteFile(csvFilePath);

          //var regionss = GetRegionName();
          var regionss = GetRegionName();
          var directoryNamess = regionss == "KASHMIR REGION" ? "K_Disbursement.xlsx" : "J_Disbursement.xlsx";
          string uploadformattedName = $"{distict_value}_{batch_value}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}{directoryNamess}";




          //string uploadformattedName = $"{distict_value}_{batch_value}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}_JK_Disbursement.xlsx";

          UploadDisbursementFile(excelFilePath, uploadformattedName);


          FileInfo fileInfo = new FileInfo(excelFilePath);

          // Get the size of the file in bytes
          long fileSizeInBytes = fileInfo.Length;
          double filesize = fileSizeInBytes / 1024.0;

          //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
          //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
          object[] parameters = new object[20];
          parameters[0] = batch_value; // RecordId 
          parameters[1] = ftpSetting["sftpFilePath"] + $"/{directoryName}/Outbox/" + uploadformattedName; // FilePath
          parameters[2] = true; // IsUploaded
          parameters[3] = DateTime.Now; // UploadedDate
          parameters[4] = Environment.MachineName; // CreatedMachineInfo
          parameters[5] = AppUserManager.GetUserId(); // CreatedBy
          parameters[6] = DateTime.Now; // CreatedOn
          parameters[7] = true; // IsActive
          parameters[8] = string.Empty; // UploadErrors
          parameters[9] = false; // IsReUploaded
          parameters[10] = Convert.ToInt32(MediaType.TxnReport); // MediaType
                                                                 // Execute the stored procedure
          parameters[11] = 0;
          parameters[12] = 0;
          parameters[13] = 0;
          parameters[14] = formattedName; // FilePath;
          parameters[15] = distict_value;

          // Reupload Value  *** Code By Himanshu Rajput ***

          parameters[16] = true;   // IsReUploadedPermitted
          parameters[17] = AppUserManager.GetUserId();  // ReUploadedPermittedBy
          parameters[18] = DateTime.Now;  // ReUploadedPermittedDate
          parameters[19] = filesize;  // File Size



          objDalBaseClass.ExecuteProcedure(ref parameters, "SaveMediaQueueDetail");
        }

        return "File has been successfully saved.";
      }
      catch (Exception ex)
      {
        return "Please Try Again..." + ex.Message;
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
    }

    private void UploadDisbursementFile(string excelFilePath, string formattedName)
    {

      try
      {
        Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();
        // Get the file name

        bool IsFTP = Convert.ToBoolean(ftpSetting["IsFTP"]);
        if (IsFTP)
        {
          //string formattedName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "JK_Disbursement" + (Path.GetExtension(excelFilePath));

          byte[] fileContents = System.IO.File.ReadAllBytes(excelFilePath);

          // Get the size of the file in bytes
          //long fileSizeInBytes = fileContents.Length;
          //fileSizeInKB = fileSizeInBytes / 1024.0;

          //var region = GetRegionName();
          var region = GetRegionName();
          var directoryName = region == "KASHMIR REGION" ? "K_Disbursement" : "J_Disbursement";
          //string ftpServerUrl = ftpSetting["sftpServerUrl"] + $"/DataFiles/{directoryName}/Outbox/" + formattedName;
          string ftpServerUrl = Helper.Helper.UploadValidationFilePath(region, directoryName, formattedName);

          //string ftpServerUrl = ftpSetting["sftpServerUrl"] + "/DataFiles/Disbursement/Outbox/" + formattedName;
          FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpServerUrl);
          ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
          ftpRequest.Timeout = 600000;
          ftpRequest.Credentials = new NetworkCredential(ftpSetting["sftpUsername"], ftpSetting["sftpPassword"]);
          using (Stream requestStream = ftpRequest.GetRequestStream())
          {
            requestStream.Write(fileContents, 0, fileContents.Length);

          }
          FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
          ftpResponse.Close();
        }
        else
        {
          string host = ftpSetting["sftpServerUrl"];
          int port = Convert.ToInt32(ftpSetting["sftpPort"]); //SFTP default port is 22
          string username = ftpSetting["sftpUsername"];
          string password = ftpSetting["sftpPassword"];
          string localFilePath = excelFilePath;
          string remoteDirectory = ftpSetting["sftpFilePath"] + "/PaymentFiles/Outbox";

          var keyFile = new PrivateKeyFile(ftpSetting["sftpPrivateKeyPath"]);
          var keyFiles = new[] { keyFile };
          var methods = new List<AuthenticationMethod>
            {
                new PasswordAuthenticationMethod(username, password),
                new PrivateKeyAuthenticationMethod(username, keyFiles)
            };

          // Create a new connection info with public key authentication
          ConnectionInfo connectionInfo = new ConnectionInfo(host, port, username, methods.ToArray());
          //new PrivateKeyAuthenticationMethod(username, privateKeyFile));


          // Create an SftpClient using the connection info
          using (SftpClient sftpClient = new SftpClient(connectionInfo))
          {
            // Connect to the SFTP server
            sftpClient.Connect();

            // Ensure the remote directory exists
            if (!sftpClient.Exists(remoteDirectory))
            {
              sftpClient.CreateDirectory(remoteDirectory);
            }

            using (var fileStream = new FileStream(localFilePath, FileMode.Open))
            {
              // Upload the file
              sftpClient.UploadFile(fileStream, Path.Combine(remoteDirectory, formattedName));
            }

            // Disconnect from the SFTP server
            sftpClient.Disconnect();
          }
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      //return fileSizeInKB;
    }



    [HttpPost]
    [AllowAnonymous]
    public JsonResult SendotpEmail(string username)
    {
      try
      {
        if (string.IsNullOrEmpty(username))
        {
          return Json(new { success = false, message = "Username is required!" });
        }
        AppUser users = null;
        ConnectionStringProvider.ConnectionName = "AppConnection1";
        AppDbContext _DbContext = DbContext;
        users = _DbContext.Users.Where(x => x.UserName == username).FirstOrDefault();
        if(users==null)
        {
          ConnectionStringProvider.ConnectionName = "AppConnection";
          users = _DbContext.Users.Where(x => x.UserName == username).FirstOrDefault();
        }             
        if (users != null)
        {
          // Generate OTP
          string otp = TextHelper.GenerateOTP();
          // Save OTP to the session or database for validation
          Session["OTP"] = otp;
          Session["OTPExpireTime"] = DateTime.Now.AddHours(5); // Set OTP expiry to 5 hours

          // Send email logic
          string subject = "Your OTP Code";
          string body = $"Your OTP is: {otp}. It is valid for 5 hours.";
          try
          {
            SentOtpMail(users.Email, subject, body); // Call your email sending method here
          }
          catch (Exception ex)
          {
            return Json(new { success = false, message = ex.Message });
          }

          return Json(new { success = true, message = "OTP sent successfully!" });
        }
        return Json(new { success = false, message = "Users details is not found" });
      }
      catch (Exception ex)
      {
        return Json(new { success = false, message = "Error sending OTP: " + ex.Message });
      }
    }
    public void SentOtpMail(string MailTo, string subject, string body)
    {

      var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
      SmtpClient smtpClient = new SmtpClient
      {
        Host = smtpSection.Network.Host,
        Port = smtpSection.Network.Port,
        EnableSsl = smtpSection.Network.EnableSsl,
        Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password)
      };
      // Email content
      var mailMessage = new MailMessage
      {
        From = new MailAddress(smtpSection.From),
        Subject = subject,
        Body = body,
        IsBodyHtml = true,
      };
      if (App.Web.Helper.SiteHelper.IsTestEmail == "1")
      {
        mailMessage.To.Add(SiteHelper.TestEmail);
      }
      else
      {
        mailMessage.To.Add(MailTo);
      }

      try
      {
        smtpClient.Send(mailMessage);
      }
      catch (Exception ex)
      {
      }

    }
    public ActionResult Widgets(string Role_id)
    {
      int UserId = Convert.ToInt32(User.Identity.GetUserId());
      int defaultRoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId)?.RoleId ?? 0;
      int selectedRoleId = string.IsNullOrEmpty(Role_id) ? defaultRoleId : Convert.ToInt32(UrlEncryption.Decrypt(Role_id));
      var model = new WidgetsViewModel();
      var widgets = db.Widgets
          .Where(x => x.RoleId == (selectedRoleId == 0 ? defaultRoleId : selectedRoleId))
          .Select(x => new { x.WidgetId, x.IsDisplay })
          .ToList();

      if (widgets?.Any() == true)
      {
        model.Total_Registered = widgets.Any(x => x.WidgetId == 1 && x.IsDisplay);
        model.Total_Approved = widgets.Any(x => x.WidgetId == 2 && x.IsDisplay);
        model.Total_Unapproved = widgets.Any(x => x.WidgetId == 3 && x.IsDisplay);
        model.Beneficiary_Paid = widgets.Any(x => x.WidgetId == 4 && x.IsDisplay);
        model.RegisteredBeneficiary_DistrictWise = widgets.Any(x => x.WidgetId == 5 && x.IsDisplay);
        model.Beneficiaries_PaymentSummary = widgets.Any(x => x.WidgetId == 6 && x.IsDisplay);
      }

      string encryptedRoleId = UrlEncryption.EncryptURL(selectedRoleId.ToString());
      var roles = _DbContext.Roles
          .Where(x => RoleData.Contains(x.Name.ToLower().Trim()))
          .OrderBy(x => x.Name)
          .ToList() 
          .Select(x => new { Id = UrlEncryption.EncryptURL(x.Id.ToString()), Name = x.Name }); 
      ViewBag.Role = new SelectList(roles, "Id", "Name", encryptedRoleId);
      ViewBag.RegionNames = Session["RegionName"];
      return View(model);
    }
    public ActionResult ChangeStatus(int id, string Roleid)
    {
      int Role = Convert.ToInt32(UrlEncryption.Decrypt(Roleid));
      var entity = _DbContext.Widgets.FirstOrDefault(x => x.WidgetId == id && x.RoleId == Role);
      if (entity != null)
      {
        entity.IsDisplay = !entity.IsDisplay;
        _DbContext.Widgets.Attach(entity);
        _DbContext.Entry(entity).State = EntityState.Modified;

        try
        {
          _DbContext.Database.Log = Console.WriteLine; // Log SQL queries
          int changes = _DbContext.SaveChanges();
          if (changes == 0)
          {
            return Json(new { success = false, message = "No changes were made." }, JsonRequestBehavior.AllowGet);
          }
          return Json(new { success = true, isDisplay = entity.IsDisplay }, JsonRequestBehavior.AllowGet);
        }
        catch (DbEntityValidationException ex)
        {
          foreach (var validationError in ex.EntityValidationErrors)
          {
            foreach (var error in validationError.ValidationErrors)
            {
              return Json(new { success = false, message = $"Property: {error.PropertyName} Error: {error.ErrorMessage}" }, JsonRequestBehavior.AllowGet);
            }
          }
        }
      }
      return Json(new { success = false, message = "Entity not found." }, JsonRequestBehavior.AllowGet);
    }

  }
}
