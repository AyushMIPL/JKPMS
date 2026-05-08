using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Data;
using System.Data;
using System.Net;
using System.IO;
using System.Threading;
using System.Transactions;
using System.Configuration;
using Remotion.Linq.Parsing;

namespace App.Web.Helper
{
    public static class Helper
    {
        public static MvcHtmlString FormatGender(this HtmlHelper html, string gender)
        {
            var htmlString = "";
            if (gender == "T")
                htmlString = "Transgender";
            else if (gender == "M")
                htmlString = "Male";
            else
                htmlString = "Female";

            return new MvcHtmlString(htmlString);
        }
        public static MvcHtmlString FormatDate(this HtmlHelper html, DateTime? dateOfBirth)
        {
            var htmlString = "";
            if (dateOfBirth != null)
            {
                htmlString = String.Format("{0:dd/MM/yyyy}", dateOfBirth);
            }
            return new MvcHtmlString(htmlString);
        }
        public static MvcHtmlString FormatProcess(this HtmlHelper html, int Status)
        {
            var htmlString = "";

            if (Status == 1)
                htmlString = "Active";
            else if (Status == 2)
                htmlString = "Processed";
            else
                htmlString = "InActive";

            return new MvcHtmlString(htmlString);
        }
        public static MvcHtmlString StatusLable(this HtmlHelper html, string status)
        {
            var htmlString = "";

            if (status.Contains("Active"))
                htmlString = "<span class='badge badge-success'>" + status + "</span>";
            else
                htmlString = "<span class='badge badge-danger'>" + status + "</span>";

            return new MvcHtmlString(htmlString);
        }

        public static MvcHtmlString FormatSeparator(this HtmlHelper html, decimal status)
        {
            var htmlString = status.ToString("#,##0.00");
            return new MvcHtmlString(htmlString);
        }


        public static SelectList ToSelectList(this DataTable table, string valueField, string textField, string keyvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();


            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text", keyvalue);
        }
        public static MvcHtmlString EmployerName(this HtmlHelper html, string name)
        {
            var htmlString = "";
            if (name == "OAP")
            {
                htmlString = "Pension for Old Age Person";
            }
            else if (name == "WID")
            {
                htmlString = "Pension for Women in Distress";
            }
            else if (name == "PCP")
            {
                htmlString = "Pension for Physically Challenged Person";
            }
            else if (name == "TRANSGENDER PERSON")
            {
                htmlString = "Pension to Transgenders";
            }
            else
            {
                htmlString = name;
            }
            return new MvcHtmlString(htmlString);
        }
        public static MvcHtmlString FormatDateRange(this HtmlHelper html, DateTime? startDate, DateTime? endDate)
        {
            var htmlString = "";
            if (startDate != null)
            {
                htmlString = String.Format("{0:dd/MM/yyyy}", startDate) + " - ";
                htmlString += endDate == null ? String.Format("{0:dd/MM/yyyy}", DateTime.Now) : String.Format("{0:dd/MM/yyyy}", endDate);
            }
            return new MvcHtmlString(htmlString);
        }

        public static string GetRegionBasedPaymentPath(string basePath, string region = null)
        {
            if (string.IsNullOrEmpty(region))
            {
                var provider = new App.Data.CurrentRegionProvider();
                region = provider.GetCurrentRegion();
            }
            else
            {
                try
                {
                    using (var db = new AppDbContext(new ConnectionStringProvider().GetConnectionString()))
                    {
                        var district = db.MasterDistrict.FirstOrDefault(d => d.Name.ToUpper() == region.Trim().ToUpper());
                        if (district != null)
                        {
                            var parentRegionObj = db.MasterRegion.FirstOrDefault(r => r.Id == district.RegionId);
                            if (parentRegionObj != null)
                            {
                                region = parentRegionObj.Name;
                            }
                        }
                    }
                }
                catch { }
            }
            
            return (region.Trim().ToUpper() == "KASHMIR REGION" || region.Trim().ToUpper() == "KASHMIR")
                ? $"{basePath}/Kashmir/PaymentFiles"
                : $"{basePath}/Jammu/PaymentFiles";
        }

        public static string GetRegionBasedValidationPath(string basePath, string region = null)
        {
            if (string.IsNullOrEmpty(region))
            {
                var provider = new App.Data.CurrentRegionProvider();
                region = provider.GetCurrentRegion();
            }
            else
            {
                try
                {
                    using (var db = new AppDbContext(new ConnectionStringProvider().GetConnectionString()))
                    {
                        var district = db.MasterDistrict.FirstOrDefault(d => d.Name.ToUpper() == region.Trim().ToUpper());
                        if (district != null)
                        {
                            var parentRegionObj = db.MasterRegion.FirstOrDefault(r => r.Id == district.RegionId);
                            if (parentRegionObj != null)
                            {
                                region = parentRegionObj.Name;
                            }
                        }
                    }
                }
                catch { }
            }
            
            return (region.Trim().ToUpper() == "KASHMIR REGION" || region.Trim().ToUpper() == "KASHMIR")
                ? $"{basePath}/Kashmir/AccountValidation"
                : $"{basePath}/Jammu/AccountValidation";
        }

        public static Dictionary<string, string> GetFTPSetting()
        {
            string ftpServerUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["ftpServerUrl"];
            string ftpUsername = System.Web.Configuration.WebConfigurationManager.AppSettings["ftpUsername"];
            string ftpPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["ftpPassword"];
            string localFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["localFilePath"];

            string ftpServerUrl1 = System.Web.Configuration.WebConfigurationManager.AppSettings["ftpServerUrl1"];
            string ftpUsername1 = System.Web.Configuration.WebConfigurationManager.AppSettings["ftpUsername1"];
            string ftpPassword1 = System.Web.Configuration.WebConfigurationManager.AppSettings["ftpPassword1"];

            //string ftptransfermode = System.Web.Configuration.WebConfigurationManager.AppSettings["ftptransfermode"];
            //string ftpUsePassive = System.Web.Configuration.WebConfigurationManager.AppSettings["ftpUsePassive"];

            string IsFTP = System.Web.Configuration.WebConfigurationManager.AppSettings["IsFTP"];
            string sftpServerUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["sftpServerUrl"];
            string sftpUsername = System.Web.Configuration.WebConfigurationManager.AppSettings["sftpUsername"];
            string sftpPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["sftpPassword"];
            string sftpFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["sftpFilePath"];

            //string sftptransfermode = System.Web.Configuration.WebConfigurationManager.AppSettings["sftptransfermode"];
            //string sftpUsePassive = System.Web.Configuration.WebConfigurationManager.AppSettings["sftpUsePassive"];

            string sftpPort = System.Web.Configuration.WebConfigurationManager.AppSettings["sftpPort"];
            string sftpPrivateKeyPath = System.Web.Configuration.WebConfigurationManager.AppSettings["sftpPrivateKeyPath"];

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
      {
          { "ftpServerUrl", ftpServerUrl },
          { "ftpUsername", UrlEncryption.Decrypt(ftpUsername) },
          { "ftpPassword", UrlEncryption.Decrypt(ftpPassword) },
          { "localFilePath", localFilePath },
          { "ftpServerUrl1", ftpServerUrl1 },
          { "ftpUsername1", UrlEncryption.Decrypt(ftpUsername1) },
          { "ftpPassword1", UrlEncryption.Decrypt(ftpPassword1) },
          //{ "ftptransfermode", ftptransfermode },
          //{ "ftpUsePassive", ftpUsePassive },

          { "IsFTP", IsFTP },
          { "sftpServerUrl", sftpServerUrl },
          //{ "sftpUsername", UrlEncryption.Decrypt(sftpUsername) },
          //{ "sftpPassword", UrlEncryption.Decrypt(sftpPassword) },
          { "sftpUsername", sftpUsername },
          { "sftpPassword", sftpPassword },
          { "sftpFilePath", sftpFilePath },
          //{ "sftptransfermode", sftptransfermode },
          //{ "sftpUsePassive", sftpUsePassive },
          { "sftpPort", sftpPort },
          { "sftpPrivateKeyPath", sftpPrivateKeyPath }

      };

            return keyValuePairs;
        }
        public static bool FileCheckInFTP(string path, Int32 attampt = 5, Int32 timeInSec = 60)
        {
            bool isExist = false;

            Dictionary<string, string> ftpSetting = GetFTPSetting();

            var request = (FtpWebRequest)WebRequest.Create(path);
            request.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            int retryDelayMs = timeInSec * 1000; // Delay between retries in milliseconds

            int currentRetry = 0;

            while (currentRetry < attampt)
            {
                try
                {
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    isExist = true;
                    break;
                }
                catch (WebException ex)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        // The condition is not met; retry after the delay
                        currentRetry++;
                        Thread.Sleep(retryDelayMs);

                    }
                }
            }
            return isExist;
        }

        public static TransactionScope CreateTransaction()
        {
            return new TransactionScope(TransactionScopeOption.Required,
              new TransactionOptions()
              {
                  IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
              });
        }

        public static class RegionProvider
        {
            public static string Region = "KASHMIR REGION";

        }


        public static string SFTPMapPath(string region, string directoryName)
        {
            string serverMapPath = "";
            if (region.ToUpper() == "JAMMU REGION" || region.ToUpper() == "JAMMU")
            {
                serverMapPath = HttpContext.Current.Server.MapPath($"~/DataFile/{directoryName}");
            }
            else
            {
                serverMapPath = HttpContext.Current.Server.MapPath($"~/DataFile/{directoryName}");

            }

            return serverMapPath;
        }


        public static string GetUploadDataFile(string region, string directoryName, string fileName)
        {
            string fullPath = "";
            if (region.ToUpper() == "JAMMU REGION" || region.ToUpper() == "JAMMU")
            {
                string basePath = ConfigurationManager.AppSettings["ftpServerUrl"];
                fullPath = Path.Combine(basePath, "DataFiles", directoryName, fileName).Replace("\\", "/");

            }
            else
            {
                string basePath = ConfigurationManager.AppSettings["ftpServerUrl"];
                fullPath = Path.Combine(basePath, "DataFiles", directoryName, fileName).Replace("\\", "/");
            }

            return fullPath;

        }

        public static string CheckForExistingRecordPath(string region, string directoryName, string fileName)
        {
            string fullPath = "";
            if (region.ToUpper() == "JAMMU REGION" || region.ToUpper() == "JAMMU")
            {
                string basePath = ConfigurationManager.AppSettings["localFilePath"];
                fullPath = Path.Combine(basePath, "DataFiles", directoryName, fileName);
            }
            else
            {
                string basePath = ConfigurationManager.AppSettings["localFilePath"];
                fullPath = Path.Combine(basePath, "DataFiles", directoryName, fileName);

            }

            return fullPath;

        }

        public static string AddExcelSheetIntoDatabasePath(string region, string directoryName, string fileName)
        {

            string fullPath = "";
            if (region.ToUpper() == "JAMMU REGION" || region.ToUpper() == "JAMMU")
            {
                string basePath = ConfigurationManager.AppSettings["localFilePath"];
                fullPath = Path.Combine(basePath, "DataFiles", directoryName, fileName);

            }

            else
            {
                string basePath = ConfigurationManager.AppSettings["localFilePath"];
                fullPath = Path.Combine(basePath, "DataFiles", directoryName, fileName);
            }

            return fullPath;

        }

        public static string GetAllFilesPath(string region, string directoryName)
        {
            string serverMapPath = "";
            if (region.ToUpper() == "JAMMU REGION" || region.ToUpper() == "JAMMU")
            {
                serverMapPath = HttpContext.Current.Server.MapPath($"~/BankMediaFile/{directoryName}");
            }
            else
            {
                serverMapPath = HttpContext.Current.Server.MapPath($"~/BankMediaFile/{directoryName}");

            }

            return serverMapPath;
        }

        public static string AddExcelSheetIntoDatabasePath1(string region, string directoryName, string fileName)
        {

            string fullPath = "";
            if (region.ToUpper() == "JAMMU REGION" || region.ToUpper() == "JAMMU")
            {
                string basePath = ConfigurationManager.AppSettings["ftpServerUrl"];
                fullPath = Path.Combine(basePath, "DataFiles", directoryName, fileName).Replace("\\", "/");

            }

            else
            {
                string basePath = ConfigurationManager.AppSettings["ftpServerUrl"];
                fullPath = Path.Combine(basePath, "DataFiles", directoryName, fileName).Replace("\\", "/");
            }

            return fullPath;

        }
        public static string SFTPMapPath1(string region, string directoryName)
        {
            string serverMapPath = "";
            if (region.ToUpper() == "JAMMU REGION" || region.ToUpper() == "JAMMU")
            {
                serverMapPath = HttpContext.Current.Server.MapPath($"~/DataFile/{directoryName}");
            }
            else
            {
                serverMapPath = HttpContext.Current.Server.MapPath($"~/DataFile/{directoryName}");

            }

            return serverMapPath;
        }

        public static string UploadValidationFilePath(string region, string directoryName, string fileName)
        {
            string fullPath = "";
            if (region.ToUpper() == "JAMMU REGION" || region.ToUpper() == "JAMMU")
            {
                string basePath = ConfigurationManager.AppSettings["sftpServerUrl"];
                fullPath = Path.Combine(basePath, "DataFiles", directoryName, "Outbox", fileName).Replace("\\", "/");

            }
            else
            {
                string basePath = ConfigurationManager.AppSettings["sftpServerUrl"];
                fullPath = Path.Combine(basePath, "DataFiles", directoryName, "Outbox", fileName).Replace("\\", "/");
            }

            return fullPath;

        }

        public static string DirectGenerateBankMediaPath(string region, string directoryName, string fileName)
        {

            string fullPath = "";
            if (region.ToUpper() == "JAMMU REGION" || region.ToUpper() == "JAMMU")
            {
                string basePath = ConfigurationManager.AppSettings["localFilePath"];
                fullPath = Path.Combine(basePath, "DataFiles", directoryName, fileName);

            }
            else
            {
                string basePath = ConfigurationManager.AppSettings["localFilePath"];
                fullPath = Path.Combine(basePath, "DataFiles", directoryName, fileName);

            }

            return fullPath;
        }

        public static string ExecutePathLocal(string region, string directoryName)
        {
            string fullPath = "";
            if (region.ToUpper() == "JAMMU REGION" || region.ToUpper() == "JAMMU")
            {
                string basePath = ConfigurationManager.AppSettings["localFilePath"];
                fullPath = Path.Combine(basePath, "DataFiles", directoryName);
            }
            else
            {
                string basePath = ConfigurationManager.AppSettings["localFilePath"];
                fullPath = Path.Combine(basePath, "DataFiles", directoryName);

            }

            return fullPath;
        }

        public static string BankDisbursementPathServer(string region, string directoryName)
        {
            string fullPath = "";
            if (region.ToUpper() == "JAMMU REGION" || region.ToUpper() == "JAMMU")
            {
                string basePath = ConfigurationManager.AppSettings["sftpFilePath"];
                fullPath = Path.Combine(basePath, directoryName, "Inbox").Replace("\\", "/");
            }
            else
            {
                string basePath = ConfigurationManager.AppSettings["sftpFilePath"];
                fullPath = Path.Combine(basePath, directoryName, "Inbox").Replace("\\", "/");
            }

            return fullPath;

        }
    }

}