using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.API.Helper
{
    public class SiteHelper
    {
        public static string ProfileImagesPath
        {
            get
            {
                return HttpContext.Current.Request.MapPath("~/UserFiles/ProfileImages/");
            }
        }
        public static string GetProfileImagePath
        {
            get
            {
                return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/UserFiles/ProfileImages/";
            }
        }
        public static string GenerateFileName(int id)
        {
            return String.Format("{0}{1}.png", id, TextHelper.GenerateRandomText(6));
        }
        public static string IsTestEmail
        {
            get
            {
                try
                {
                    var test = ConfigurationManager.AppSettings["isTestMail"];
                    return test;
                }
                catch (Exception)
                {
                    return "1";
                }
            }
        }
        public static string WebsiteURL
        {
            get { return string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority); }
        }

        public static string TestEmail
        {
            get
            {
                try
                {
                    var test = ConfigurationManager.AppSettings["TestMail"];
                    return test;
                }
                catch (Exception)
                {
                    return "neeraj.p@mishainfotech.com";
                }
            }
        }
    }

    public class TextHelper
    {
        public static string GenerateRandomText(int length)
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder result = new StringBuilder(length);
            byte[] randomBytes = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            foreach (byte b in randomBytes)
            {
                result.Append(chars[b % chars.Length]);
            }

            return result.ToString();
        }
    }
}
