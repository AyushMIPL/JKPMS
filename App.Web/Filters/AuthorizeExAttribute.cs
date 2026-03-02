using App.Data;
using App.Data.Entities;
using App.Data.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections;
using System.Data.Entity;
using Castle.Core.Internal;
using CrystalDecisions.Shared.Json;
using static App.Web.Helper.Helper;
using App.Web.Helper;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace App.Web.Filters
{
    public class AuthorizeExAttribute : AuthorizeAttribute
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private AppDbContext db;

        public AuthorizeExAttribute()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        private readonly string[] allowedroles;
        public AuthorizeExAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            #region 
            // Code By Himanshu Rajput  *** start ***

            string loggedInUserId = httpContext.User.Identity.GetUserId();

            if (loggedInUserId == null)
            {
                return false;
            }

            HttpCookie getCurrentRegion = HttpContext.Current.Request.Cookies["regionValues"];
            RegionProvider.Region = getCurrentRegion.Value;
            if (RegionProvider.Region == "KASHMIR REGION")
                ConnectionStringProvider.ConnectionName = "AppConnection1";
            else
                ConnectionStringProvider.ConnectionName = "AppConnection";
            var route = HttpContext.Current.Request.RequestContext.RouteData.Values;

            //*** End ***
            #endregion

            string actionName = route["action"].ToString();
            string controllerName = route["controller"].ToString();

            var roleStore = new RoleStore<AppRole, int, AppUserRole>(db);
            var roleManager = new RoleManager<AppRole, int>(roleStore);
            bool authorize = false;
            List<SessionViewModel> results = (List<SessionViewModel>)HttpContext.Current.Session["List"];
            if (results != null && results.Count > 0)
            {
                var result = results.Where(x => x.ControllerName == controllerName && x.ActionName == actionName).FirstOrDefault();
                if (result == null)
                {
                    result = results.Where(x => x.ControllerName == controllerName).FirstOrDefault();
                }

                // User can edit his/her profile irrespective of permission
                // User can also change password
                string userType = httpContext.Request.QueryString["userType"];
                if (!string.IsNullOrEmpty(userType) && userType == "r4MzcRB4jkD_4DrhGYDWBQ" && controllerName == "Account" && actionName == "UserEdit")
                {
                    string filePath = httpContext.Request.FilePath;
                    if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrEmpty(loggedInUserId))
                    {
                        string encryptedLoggedInUserId = UrlEncryption.EncryptURL(loggedInUserId);
                        string encryptedUserId = filePath.Split('/')[3];

                        if (encryptedLoggedInUserId == encryptedUserId)
                        {
                            authorize = true;
                        }
                    }
                }
                if (controllerName == "Account" && actionName == "ChangeUserPassword")
                {
                    authorize = true;
                }
                //else if ((actionName.Contains("Index") || actionName.Contains("Ajax") || actionName.Contains("Edit") || actionName.Contains("Details") || actionName == result.ActionName) && result.ViewPermission == true)
                else if (result.ViewPermission == true)
                {
                  authorize = true;
                }
               else if (actionName.Contains("Index") || actionName.Contains("Ajax") || actionName.Contains("Edit") || actionName.Contains("Details") || actionName == result.ActionName)
                {
                  authorize = true;
                }
               else if ((actionName.Contains("Create") || actionName.Contains("Add")) && result.AddPermssion == true)
                {
                    authorize = true;
                }
                else if (actionName.Contains("Edit") && result.EditPermission == true)
                {
                    authorize = true;
                }
                else if (actionName == "Remove" && result.DeletePermission == true)
                {
                    authorize = true;
                }
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User == null || !filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // Redirect to the login page
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { controller = "Account", action = "Login", returnUrl = filterContext.HttpContext.Request.Url?.AbsolutePath }
                    )
                );
            }
            else if (HttpContext.Current.Session["List"] != null)
            {
                //filterContext.Result = new HttpUnauthorizedResult();
                filterContext.Result = new RedirectToRouteResult(
                                             new RouteValueDictionary
                                           {
                                       { "action", "Index" },
                                       { "controller", "UnAuthorize" }
                                           });
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                                             new RouteValueDictionary
                                           {
                                       { "action", "LogOffManually" },
                                       { "controller", "Account" }
                                           });
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}