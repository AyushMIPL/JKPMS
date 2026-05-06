using App.Data;
using App.Data.Entities;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using App.Data.ViewModels;
using App.Data.Extentions;
using System;
using System.Collections.Generic;
using System.Globalization;
using Postal;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNet.Identity.EntityFramework;
using DocumentFormat.OpenXml.Office2010.Excel;
using Castle.Core.Internal;
using static App.Web.Helper.Helper;
using System.Text;
using System.IO;
using System.Configuration;

namespace App.Web.Controllers
{
  public class BaseController : Controller
  {
    private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
    private CurrentRegionProvider CurrentRegionProvider = new CurrentRegionProvider();

    public const string GENERIC_MESSAGE = "Error occured,please try again";
    //readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    //private AppDbContext db = new AppDbContext();

    private AppDbContext db;
    private AppDbContext _DbContext;

    public BaseController()
    {
      db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
      _DbContext = DbContext;
    }
    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      // Retrieve the nonce from the OWIN context and pass it to the ViewBag
      var owinContext = filterContext.HttpContext.GetOwinContext();
      string nonce = owinContext.Get<string>("styleNonce");
      ViewBag.StyleNonce = nonce;
      Session["StyleNonce"] = nonce;

      base.OnActionExecuting(filterContext);
    }

    public AppDbContext DbContext
    {
      get
      {
        return new AppDbContext(ConnectionStringProvider.GetConnectionString());
      }
    }

    public AppUserManager OwinUserManger
    {
      get
      {
        var context = HttpContext.GetOwinContext();
        return AppUserManager.Create(new IdentityFactoryOptions<AppUserManager>(), context);
      }
    }
    public IAuthenticationManager OwinAuthenticationManager
    {
      get { return HttpContext.GetOwinContext().Authentication; }
    }
    public void ToastSuccess(string message = GENERIC_MESSAGE)
    {
      TempData["success"] = message;
    }
    public void ToastError(string message = GENERIC_MESSAGE)
    {
      TempData["error"] = message;
    }
    public static DateTime DateConvertion(string dateString)
    {
      DateTime DateValue = Convert.ToDateTime(dateString, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
      return DateValue;
    }
    public List<int> RoleIds
    {
      get
      {
        var userRoles = db.Roles.Include(r => r.Users).ToList();
        var userRoleNames = (from r in userRoles
                             from u in r.Users
                             where u.UserId == Convert.ToInt32(User.Identity.GetUserId())
                             select (r.Name.Trim())).FirstOrDefault();
        var RoleList = new List<string>();
        if (userRoleNames == "Admin")
          RoleList = new List<string> { "director finance (dfsw)", "directorate jammu (dj)", "directorate kashmir (dk)", "districts", "support" };
        else if (userRoleNames == "Director Finance (DFSW)")
          RoleList = new List<string> { "directorate jammu (dj)", "directorate kashmir (dk)", "Districts", "support" };
        else if (userRoleNames == "Directorate Jammu (DJ)")
          RoleList = new List<string> { "Districts" };
        else if (userRoleNames == "Directorate Kashmir (DK)")
          RoleList = new List<string> { "Districts" };
        else
          RoleList = new List<string>();
        return _DbContext.Roles.Where(x => RoleList.Contains(x.Name)).Select(x => x.Id).ToList();
      }
    }
    public List<string> RoleData
    {
      get
      {
        var userRoles = db.Roles.Include(r => r.Users).ToList();
        var userRoleNames = (from r in userRoles
                             from u in r.Users
                             where u.UserId == Convert.ToInt32(User.Identity.GetUserId())
                             select (r.Name.Trim())).FirstOrDefault();
        if (userRoleNames == "Admin")
          return new List<string> { "admin", "director finance (dfsw)", "directorate jammu (dj)", "directorate kashmir (dk)", "districts", "tehsil","jk bank","nic apis", "support" };
        else if (userRoleNames == "Director Finance (DFSW)")
          return new List<string> { "directorate jammu (dj)", "directorate kashmir (dk)", "districts", "districts", "tehsil","jk bank", "support" };
        else if (userRoleNames == "Directorate Jammu (DJ)")
          return new List<string> { "districts", "Tehsil" };
        else if (userRoleNames == "Directorate Kashmir (DK)")
          return new List<string> { "districts", "Tehsil" };
        else
          return new List<string>();
      }
    }

    public string GetRegionName()
    {
      string RegionName = "KASHMIR REGION";
      try
      {
        var Region = Session["RegionName"];

        if (Region != null)
        {
          RegionName = Session["RegionName"] as string;
        }
      }
      catch (Exception ex)
      {
      }

      return RegionName;
    }

    public void appendText(string text)
    {
      try
      {
        string filePath = @"D:\J & K\App.Web\UserFiles\ProfileImages\test.txt";
        System.IO.File.AppendAllLines(filePath, new string[] { text });
      }
      catch (Exception ex)
      {
      }
    }

    public List<Region> GetUsersAssignedLocations(string RoleName = "", int UId = 0, int RId = 0, string DistrictName = "")
    {
      try
      {
        StringBuilder sb = new StringBuilder();
        var regionname = GetRegionName();// RegionProvider.Region;
        sb.AppendLine(regionname);
        //var regionname = CurrentRegionProvider.GetCurrentRegion();
        var UserID = User.Identity.GetUserId();
        int Id = Convert.ToInt32(User.Identity.GetUserId());
        var userRoles = db.Roles.Include(r => r.Users).ToList();
        var userRoleNames = (from r in userRoles
                             from u in r.Users
                             where u.UserId == Id
                             select (r.Id, r.Name, u.UserId)).FirstOrDefault();
        int UserId = (UId > 0 ? UId : userRoleNames.UserId);
        int RoleId = (RId > 0 ? RId : userRoleNames.Id);
        string RoleNames = (!string.IsNullOrEmpty(RoleName) ? RoleName.Trim() : userRoleNames.Name.Trim());
        //var data = App.Web.Repository.CommomRepo.GetUserAssignedDistrict(RoleId, RoleNames, UserId, DistrictName, regionname);
        //sb.AppendLine(data.Count.ToString());
        //appendText(sb.ToString());
        return App.Web.Repository.CommomRepo.GetUserAssignedDistrict(RoleId, RoleNames, UserId, DistrictName, regionname);
      }
      catch (Exception ex)

      {
        ToastError(ex.Message);
        return null;
      }
    }
    public List<Region> GetAssignedDistrictForEdit(int UId, int RId, string regionname)
    {
      try
      {
        return App.Web.Repository.CommomRepo.GetAssignedDistrictForEdit(RId, UId, regionname);
      }
      catch (Exception ex)
      {
        ToastError(ex.Message);
        return null;
      }
    }

    public int GetCurrentSelectedFinancialYear()
    {
      int FinancialYear = DateTime.Now.Year;
      try
      {
        if (Session["FinancialYear"] != null)
        {
          FinancialYear = Convert.ToInt32(Session["FinancialYear"]);
        }
      }
      catch
      {

      }

      return FinancialYear;
    }

    public string  stringCurrentSelectedFinancialYear()
    {
      string FinancialYear ="";
      try
      {
        if (Session["FinancialYear"] != null)
        {
          int FromFinancialYear = Convert.ToInt32(Session["FinancialYear"]);
          int ToFinancialYear = Convert.ToInt32(Session["FinancialYear"])+1;
          FinancialYear = "01/04/" + FromFinancialYear + "-" + "31/03/" + ToFinancialYear;
        }
        else
        {
          string CurrentFinancialYear = DateTime.Now.Year.ToString();
          DateTime CurrentDate = DateTime.Now.Date;
          if (CurrentDate.Month < 4)
          {
            int FromFinancialYear = +DateTime.Now.Year - 1;
            int ToFinancialYear = +FromFinancialYear + 1;
            FinancialYear = "01/04/" + FromFinancialYear + "-" + "31/03/" + ToFinancialYear;
          }
        }
      }
      catch
      {

      }

      return FinancialYear;
    }


    public DateTime GetCurrentDateAsPerFinancialYear()
    {
      DateTime CurrentDateAsPerFinancialYear = DateTime.Now;
      try
      {
        int FinancialYear = DateTime.Now.Year;
        if (Session["FinancialYear"] != null)
        {
          FinancialYear = Convert.ToInt32(Session["FinancialYear"]);
        }

        DateTime currentDateTime = DateTime.Now;
        // Create a new DateTime with the desired year
        CurrentDateAsPerFinancialYear = new DateTime(FinancialYear, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
      }
      catch
      {

      }

      return CurrentDateAsPerFinancialYear;
    }

    public DateTime GetStartDateDateAsPerFinancialYear()
    {
      DateTime CurrentDateAsPerFinancialYear = DateTime.Now;
      try
      {
        int FinancialYear = DateTime.Now.Year;
        if (Session["FinancialYear"] != null)
        {
          FinancialYear = Convert.ToInt32(Session["FinancialYear"]);
        }

        DateTime currentDateTime = DateTime.Now;
        // Create a new DateTime with the desired year
        CurrentDateAsPerFinancialYear = new DateTime(FinancialYear, 4, 1);
      }
      catch
      {

      }

      return CurrentDateAsPerFinancialYear;
    }

    public DateTime GetEndDateDateAsPerFinancialYear()
    {
      DateTime CurrentDateAsPerFinancialYear = DateTime.Now;
      try
      {
        int FinancialYear = DateTime.Now.Year;
        if (Session["FinancialYear"] != null)
        {
          FinancialYear = Convert.ToInt32(Session["FinancialYear"]);
        }

        DateTime currentDateTime = DateTime.Now;
        // Create a new DateTime with the desired year
        CurrentDateAsPerFinancialYear = new DateTime(FinancialYear + 1, 3, 31);
      }
      catch
      {

      }

      return CurrentDateAsPerFinancialYear;
    }

    public bool IsGeneratePensionWithoutSftp
    {
      get
      {
        bool IsGeneratePensionWithoutSftp = false;
        string IsGeneratePensionWithoutSftp_String = ConfigurationManager.AppSettings["IsGeneratePensionWithoutSftp"];
        if (!string.IsNullOrEmpty(IsGeneratePensionWithoutSftp_String))
        {
          if (IsGeneratePensionWithoutSftp_String == "1")
          {
            IsGeneratePensionWithoutSftp = true;
          }
        }

        return IsGeneratePensionWithoutSftp;
      }
    }

    public bool IsSendValidationFileToSftp
    {
      get
      {
        bool IsSendValidationFileToSftp = true;
        string IsSendValidationFileToSftp_String = ConfigurationManager.AppSettings["IsSendValidationFileToSftp"];
        if (!string.IsNullOrEmpty(IsSendValidationFileToSftp_String))
        {
          if (IsSendValidationFileToSftp_String == "0" || IsSendValidationFileToSftp_String.ToLower() == "false")
          {
            IsSendValidationFileToSftp = false;
          }
        }

        return IsSendValidationFileToSftp;
      }
    }
  }
}