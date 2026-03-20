using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using App.Web.Models;
using App.Data;
using App.Data.Entities;
using App.Web.Helper;
using System.Configuration;
using System.Text;

namespace App.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Retrieve the string value from configuration
            string sessionExpirationTimeValue = ConfigurationManager.AppSettings["SessionExpireTimeSpan"];

            // Initialize a variable to store the parsed integer value
            int sessionExpirationTime = 30;

            // Try to parse the string value to an integer
            if (int.TryParse(sessionExpirationTimeValue, out sessionExpirationTime))
            {
            }
            else
            {
                sessionExpirationTime = 30; // default
            }

            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieName = "JKPSManagement",
                LoginPath = new PathString("/Account/Login"),
                // Set Cookie Options
                CookieHttpOnly = true, // Make the cookie HttpOnly
                CookieSecure = CookieSecureOption.SameAsRequest, // Ensure the cookie is only sent over HTTPS
                CookieSameSite = SameSiteMode.Strict, // Set the SameSite policy (Strict, Lax, or None)
                ExpireTimeSpan = TimeSpan.FromMinutes(sessionExpirationTime), // Set cookie to expire after 30 minutes
                SlidingExpiration = true, // Optional: refreshes expiration time on each request
                Provider = new CookieAuthenticationProvider
                {
                    OnException = context => { }
                }
            });

            //Middleware to modify the ASP.NET_SessionId cookie
            app.Use(async (context, next) =>
            {
                if (!context.Request.Path.Value.Contains("/ReportForms")) // Adjust path accordingly
                {
                    await next.Invoke();  // Continue processing the request

                    // Check if the session cookie is present
                    var sessionCookieValue = context.Request.Cookies["ASP.NET_SessionId"];
                    if (!string.IsNullOrEmpty(sessionCookieValue))
                    {
                        // Modify the session cookie and re-append it with the new settings
                        context.Response.Cookies.Append("ASP.NET_SessionId", sessionCookieValue, new CookieOptions
                        {
                            HttpOnly = true,  // Set HttpOnly
                            Secure = context.Request.IsSecure,  // Set Secure if using HTTPS
                                                                //Secure = true,  // Set Secure if using HTTPS
                            SameSite = SameSiteMode.Strict  // Set SameSite attribute
                        });
                    }
                }
                else
                {
                    await next.Invoke();  // Continue processing the request
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Add middleware to remove specific headers
            app.Use(async (context, next) =>
            {
                // Generate a random nonce
                string nonce = SecurityHelper.GenerateNonce();
                // Set nonce in OWIN context
                context.Set("styleNonce", nonce);

                // Add Content Security Policy header

                //context.Response.Headers.Add("Content-Security-Policy", new[] { "default-src 'self'" +
                //    "; img-src 'self' http: https: data:;" +
                //    "font-src 'self' https://fonts.googleapis.com https://fonts.gstatic.com http://fonts.googleapis.com http://fonts.gstatic.com; " +
                //    $"style-src 'self' 'unsafe-inline';"+
                //    //"style-src-elem 'self';"+
                //    "style-src-elem * 'unsafe-inline';"+
                //    $"script-src 'self' 'unsafe-inline' 'unsafe-eval' https://cdn.ckeditor.com http://localhost:* 'nonce-{nonce}';"+
                //    //"object-src 'none';"+
                //    "frame-ancestors 'self';"
                //});

                context.Response.Headers.Add("Content-Security-Policy", new[]
                {
                    "default-src 'self';" +
                    "img-src 'self' http: https: data:;" +
                    "font-src 'self' https://fonts.googleapis.com https://fonts.gstatic.com http://fonts.googleapis.com http://fonts.gstatic.com;" +
                    "style-src 'self' 'unsafe-inline';" +
                    "style-src-elem * 'unsafe-inline';" +
                    "script-src 'self' 'unsafe-inline' 'unsafe-eval' https://cdn.ckeditor.com http://localhost:*;" +
                    "frame-ancestors 'self';"
                });

                // Add HTTP Strict Transport Security (HSTS) header
                context.Response.Headers.Add("Strict-Transport-Security", new[] { "max-age=31536000; includeSubDomains" });

                // Add Referrer Policy header
                //context.Response.Headers.Add("Referrer-Policy", new[] { "no-referrer" });
                //context.Response.Headers.Add("Referrer-Policy", new[] { "same-origin" });
                //context.Response.Headers.Add("Referrer-Policy", new[] { "no-referrer-when-downgrade" });
                context.Response.Headers.Add("Referrer-Policy", new[] { "strict-origin-when-cross-origin" });

                // Add Permissions Policy header
                context.Response.Headers.Add("Permissions-Policy", new[] { "geolocation=(self), microphone=()" });

                // Continue processing the request
                await next();
            });

            //JobScheduler.Start();

        }
    }
}