using App.Data;
using App.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;
using OfficeOpenXml;
// using Microsoft.Office.Interop.Excel;

using System.Net.Http;
using System.Web;
using Renci.SshNet;
using JKPS.DL;
using JKPS.COMMON;
using System.Text;
using Postal;
using App.API.Helper;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using System.Web.Http.Description;

//using App.Data.Entities;

namespace App.API.Controllers
{
    //[Authorize]
    [IpWhitelistAndApiKeyFilter]
    public class BeneficiariesDetailsController : ApiController
    {
        private AppDbContext db;
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();

        public string GetIp()
        {
            var configuredIpAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["Allowd_Ip_Address"];
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }

        [System.Web.Http.HttpGet]
        
        [System.Web.Http.Route("api/BeneficiariesDetails/GetIpAddress")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult GetIpAddress()
        {
            var configuredIpAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["Allowd_Ip_Address"];
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return Ok(new { StatusCode = HttpStatusCode.OK, ConfiguredIpAddress = configuredIpAddress, Ip = ip });
        }

        [System.Web.Http.HttpPost]

        [System.Web.Http.Route("api/BeneficiariesDetails/AddBeneficiaries")]
        public IHttpActionResult AddBeneficiariesDetails([FromBody] List<BeneficiariesRequest> requestedData)
        {
            var returnedData = new Dictionary<string, object>();
            try
            {
                if (requestedData == null)
                {
                    return BadRequest("Invalid json format. Please send valid json to continue.");
                }
                var clientIpAddress = GetIp();

                string[] validRegionNames = { "JAMMU", "jammu", "KASHMIR", "kashmir" };
                List<BeneficiariesRequest> BeneficiariesRequest = new List<BeneficiariesRequest>();
                var groupedRegionDataSets = requestedData.GroupBy(x => x.Region).ToList();
                foreach (var data in groupedRegionDataSets)
                {
                    List<MasterBeneficiariesDetails> BeneficiariesDetails = new List<MasterBeneficiariesDetails>();
                    foreach (var beneficiaries in data.ToList())
                    {
                        if (beneficiaries.BeneficiariesDetails != null && beneficiaries.BeneficiariesDetails.Count > 0)
                        {
                            BeneficiariesDetails.AddRange(beneficiaries.BeneficiariesDetails);
                        }
                    }
                    BeneficiariesRequest newRequestData = new BeneficiariesRequest
                    {
                        Region = data.Key,
                        BeneficiariesDetails = BeneficiariesDetails,
                    };

                    BeneficiariesRequest.Add(newRequestData);
                }

                foreach (var request in BeneficiariesRequest)
                {

                    if (request == null || request.BeneficiariesDetails == null || string.IsNullOrEmpty(request.Region))
                    {
                        return BadRequest("Invalid data. Region Name JAMMU / KASHMIR is mandatory");
                    }

                    if (!string.IsNullOrEmpty(request.Region))
                    {
                        if (validRegionNames.Any(x => x == request.Region.ToUpper()))
                        {
                            db = new AppDbContext(ConnectionStringProvider.GetConnectionStringApi(request.Region));

                            string[] JAMMUREGION = { "DODA", "JAMMU", "KATHUA", "KISHTWAR", "POONCH", "RAJOURI", "RAMBAN", "REASI", "SAMBA", "UDHAMPUR" };
                            string[] KASHMIRREGION = { "ANANTNAG", "BANDIPORA", "BARAMULLA", "BUDGAM", "GANDERBAL", "KULGAM", "KUPWARA", "PULWAMA", "SHOPIAN", "SRINAGAR" };

                            List<MasterBeneficiariesDetails> JAMMUREGIONList = new List<MasterBeneficiariesDetails>();
                            List<MasterBeneficiariesDetails> KASHMIRREGIONList = new List<MasterBeneficiariesDetails>();
                            List<MasterBeneficiariesDetails> ForwordList = new List<MasterBeneficiariesDetails>();

                            List<string> InvalidReginDist = new List<string>();

                            var requests = request.BeneficiariesDetails.ToList();
                            foreach (var item in requests)
                            {
                                if (request.Region.ToUpper() == "KASHMIR REGION" || request.Region == "KASHMIR")
                                {
                                    if (KASHMIRREGION.Contains((item.SelectDistrict).ToUpper()))
                                    {
                                        KASHMIRREGIONList.Add(item);
                                        ForwordList.Add(item);
                                    }
                                    else
                                    {
                                        JAMMUREGIONList.Add(item);
                                        InvalidReginDist.Add(item.ApplicationReferenceNo);
                                    }
                                }
                                else
                                {
                                    if (JAMMUREGION.Contains((item.SelectDistrict).ToUpper()))
                                    {
                                        JAMMUREGIONList.Add(item);
                                        ForwordList.Add(item);
                                    }
                                    else
                                    {
                                        KASHMIRREGIONList.Add(item);
                                        InvalidReginDist.Add(item.ApplicationReferenceNo);
                                    }
                                }
                            }

                            List<string> DuplicateAccount = new List<string>();
                            List<MasterBeneficiariesDetails> list_BeneficiariesDetails = new List<MasterBeneficiariesDetails>();

                            // Check the records already exist

                            foreach (var item1 in ForwordList)
                            {
                                var recordExist = db.MasterBeneficiariesDetails
                                    .FirstOrDefault(x => x.ApplicationReferenceNo == item1.ApplicationReferenceNo && x.SNo == item1.SNo);

                                if (recordExist != null)
                                {
                                    DuplicateAccount.Add(item1.ApplicationReferenceNo);
                                }
                                else
                                {
                                    list_BeneficiariesDetails.Add(item1);
                                }
                            }

                            int userId = 0;
                            if (list_BeneficiariesDetails != null)
                            {
                                var beneficiaries_record = new MasterBeneficiaries();
                                beneficiaries_record.Finalized = list_BeneficiariesDetails.Count();
                                beneficiaries_record.FilePath = "JSON";
                                beneficiaries_record.IsProcessed = false;
                                beneficiaries_record.IsActive = true;
                                beneficiaries_record.ProcessedBy = userId;
                                beneficiaries_record.ProcessedOn = DateTime.Now;
                                db.MasterBeneficiaries.Add(beneficiaries_record);
                                db.SaveChanges();
                                // Add beneficiaries details
                                foreach (var item in list_BeneficiariesDetails)
                                {
                                    var MasterBeneficiariesDetails = new MasterBeneficiariesDetails();
                                    MasterBeneficiariesDetails.SNo = item.SNo;
                                    MasterBeneficiariesDetails.MasterBeneficiariesId = beneficiaries_record.Id;
                                    MasterBeneficiariesDetails.ApplicationReferenceNo = item.ApplicationReferenceNo;
                                    MasterBeneficiariesDetails.SubmissionLocation = item.SubmissionLocation;
                                    MasterBeneficiariesDetails.SubmissionDate = item.SubmissionDate;
                                    MasterBeneficiariesDetails.AppliedBy = item.AppliedBy;
                                    MasterBeneficiariesDetails.SelectTehsilSocialWelfareOffice_TSWO = item.SelectTehsilSocialWelfareOffice_TSWO;
                                    MasterBeneficiariesDetails.SelectDistrict = item.SelectDistrict;
                                    MasterBeneficiariesDetails.NameOfTheApplicant = item.NameOfTheApplicant;
                                    MasterBeneficiariesDetails.DateOfBirth = item.DateOfBirth;
                                    MasterBeneficiariesDetails.Age_InYears = item.Age_InYears;
                                    MasterBeneficiariesDetails.MobileNumber = item.MobileNumber;
                                    MasterBeneficiariesDetails.DoYouHaveBPLcard = item.DoYouHaveBPLcard;
                                    MasterBeneficiariesDetails.FatherOrHusbandOrGuardianName = item.FatherOrHusbandOrGuardianName;
                                    MasterBeneficiariesDetails.EMail = item.EMail;
                                    MasterBeneficiariesDetails.Category = item.Category;
                                    MasterBeneficiariesDetails.Gender = item.Gender;
                                    MasterBeneficiariesDetails.PresentAddress = item.PresentAddress;
                                    MasterBeneficiariesDetails.PresentDistrict = item.PresentDistrict;
                                    MasterBeneficiariesDetails.PresentVillageName = item.PresentVillageName;
                                    MasterBeneficiariesDetails.Pincode = item.Pincode;
                                    MasterBeneficiariesDetails.PresentHalqaPanchayatOrMunicipalityName = item.PresentHalqaPanchayatOrMunicipalityName;
                                    MasterBeneficiariesDetails.PresentTehsil = item.PresentTehsil;
                                    MasterBeneficiariesDetails.PermanentAddress = item.PermanentAddress;
                                    MasterBeneficiariesDetails.PermanentDistrict = item.PermanentDistrict;
                                    MasterBeneficiariesDetails.PermanentTehsil = item.PermanentTehsil;
                                    MasterBeneficiariesDetails.PermanentHalqaPanchayatOrMunicipalityName = item.PermanentHalqaPanchayatOrMunicipalityName;
                                    MasterBeneficiariesDetails.PermanentVillageName = item.PermanentVillageName;
                                    MasterBeneficiariesDetails.BranchName = item.BranchName;
                                    MasterBeneficiariesDetails.IFSCCode = item.IFSCCode;
                                    MasterBeneficiariesDetails.AccountNoOfTheApplicant = item.AccountNoOfTheApplicant;
                                    MasterBeneficiariesDetails.BankName = item.BankName;
                                    MasterBeneficiariesDetails.SelectPensionType = item.SelectPensionType;
                                    MasterBeneficiariesDetails.PercentageofDisability = item.PercentageofDisability;
                                    MasterBeneficiariesDetails.CivilCondition = item.CivilCondition;
                                    MasterBeneficiariesDetails.AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP = item.AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP;
                                    MasterBeneficiariesDetails.BankName1 = item.BankName1;
                                    MasterBeneficiariesDetails.BranchName1 = item.BranchName1;
                                    MasterBeneficiariesDetails.IFSCCode1 = item.IFSCCode1;
                                    MasterBeneficiariesDetails.AccountNumber = item.AccountNumber;
                                    MasterBeneficiariesDetails.ApplicationSanctionedunderSchemeName = item.ApplicationSanctionedunderSchemeName;
                                    MasterBeneficiariesDetails.CurrentTask = item.CurrentTask;
                                    MasterBeneficiariesDetails.CurrentStatus = item.CurrentStatus;
                                    MasterBeneficiariesDetails.LastTask = item.LastTask;
                                    MasterBeneficiariesDetails.VersionNo = item.VersionNo;
                                    MasterBeneficiariesDetails.Last_pay_date = item.Last_pay_date;
                                    MasterBeneficiariesDetails.Application_approve_on = item.Application_approve_on;
                                    MasterBeneficiariesDetails.AADHAR = item.AADHAR;
                                    MasterBeneficiariesDetails.CreatedMachineInfo = clientIpAddress;
                                    MasterBeneficiariesDetails.CreatedMachineInfo = clientIpAddress;
                                    MasterBeneficiariesDetails.IsActive = true;
                                    db.MasterBeneficiariesDetails.Add(MasterBeneficiariesDetails);
                                }
                                db.SaveChanges();

                                // Finalize total contribution
                                try
                                {
                                    // Define the parameters if needed (e.g., for input parameters)
                                    var parameter1 = new SqlParameter("@Id", beneficiaries_record.Id);
                                    var parameter2 = new SqlParameter("@UserId", userId);
                                    // Execute the stored procedure
                                    int result = db.Database.ExecuteSqlCommand("EXEC USP_EmployeeIns_EmpIncomIns_EmpDirDpstIns @Id,@UserId", parameter1, parameter2);
                                }
                                catch (Exception ex)
                                {
                                    return Ok(new { StatusCode = HttpStatusCode.InternalServerError, Message = ex.Message, success = false });
                                }

                            }

                            #region//File Send SFTP JKB,THE JAMMU AND KASHMIR BANK LTD

                            var BankName = "JKB,THE JAMMU AND KASHMIR BANK LTD.";

                            BankName = BankName.Replace("ANDSYMBOL", "&");
                            int UserId = AppUserManager.GetUserId();
                            Dictionary<string, string> ftpSetting = Helper.Helper.GetFTPSetting();

                            string formattedName = "ContributionCSV_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                            formattedName = formattedName.Replace(" ", "");

                            // Define the path for sql create SVC

                            var region = request.Region.Trim().ToUpper();
                            var directoryName = (region == "KASHMIR REGION" || region == "KASHMIR") ? "K_MasterEmployeeDownloads" : "J_MasterEmployeeDownloads";
                            string filePath = Helper.Helper.AddExcelSheetIntoDatabasePath(region, directoryName, formattedName);

                            var parameter4 = new SqlParameter("@filePathWithName", filePath);
                            var parameter5 = new SqlParameter("@UserId", UserId);
                            var parameter6 = new SqlParameter("@BankName", BankName);
                            // Execute the stored procedure
                            int resultt = db.Database.ExecuteSqlCommand("EXEC MasterEmployeeExcelExport @filePathWithName, @UserId,@BankName", parameter4, parameter5, parameter6);//parameter3,

                            // Create an FTP client
                            WebClient ftpClient = new WebClient();
                            ftpClient.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);
                            string path = Helper.Helper.AddExcelSheetIntoDatabasePath1(region, directoryName, formattedName);

                            // File path, attampt, dealy in attampt
                            bool isExist = Helper.Helper.FileCheckInFTP(path, 3, 30);

                            if (isExist)
                            {
                                // Download the file from the FTP server
                                byte[] fileData = ftpClient.DownloadData(path);

                                // Specify the file path where you want to save the uploaded file
                                string serverMapPath = Helper.Helper.SFTPMapPath(region, directoryName);

                                if (!Directory.Exists(serverMapPath))
                                {
                                    // Attempt to create the directory
                                    Directory.CreateDirectory(serverMapPath);
                                }
                                // Add file name with directory
                                string csvFilePath = System.IO.Path.Combine(serverMapPath, formattedName);

                                // Save the file to the server
                                System.IO.File.WriteAllBytes(csvFilePath, fileData);

                                // Convert csv file to data table to get counts
                                System.Data.DataTable dataTable = ConvertCsvToDataTable(csvFilePath);

                                int benif_Count = dataTable.Rows.Count;

                                int Validated_count = dataTable.AsEnumerable().Count(row => row.Field<string>("ACCOUNT_STATUS") == "ACTIVE");
                                int Notvalidated_count = dataTable.AsEnumerable().Count(row => row.Field<string>("ACCOUNT_STATUS") != "ACTIVE");

                                string excelFilePath = csvFilePath.Replace(".csv", ".xlsx");
                                /*
                                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(csvFilePath);
                                wb.SaveAs(excelFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
                                wb.Close(false);
                                app.Quit();
                                */

                                using (var package = new ExcelPackage())
                                {
                                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                                    var format = new ExcelTextFormat
                                    {
                                        Delimiter = ',',
                                        Encoding = Encoding.UTF8
                                    };
                                    worksheet.Cells["A1"].LoadFromText(new FileInfo(csvFilePath), format);
                                    package.SaveAs(new FileInfo(excelFilePath));
                                }

                                //File Transfer To FstpServer

                                // delete file in safe way
                                FileHelper fileHelper = new FileHelper();
                                bool isCsvFileDeleted = fileHelper.TryDeleteFile(csvFilePath);

                                string uploadformattedName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{region}_Validation{System.IO.Path.GetExtension(excelFilePath)}";

                                UploadValidationFile(excelFilePath, uploadformattedName, request.Region.Trim().ToUpper());
                                FileInfo fileInfo = new FileInfo(excelFilePath);

                                // Get the size of the file in bytes
                                long fileSizeInBytes = fileInfo.Length;
                                double filesize = fileSizeInBytes / 1024.0;

                                string results = uploadformattedName.Split('_')[0] + uploadformattedName.Split('_')[1];
                                int length = 8;
                                // Calculate the starting index for the last six characters
                                int startIndex = results.Length - length;

                                // Use Substring to get the last six characters
                                string reducedString = results.Substring(startIndex);

                                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDALByRegion(request.Region.Trim().ToUpper());
                                object[] parameters = new object[20];
                                parameters[0] = reducedString;
                                parameters[1] = ftpSetting["sftpFilePath"] + $"/{directoryName}/Outbox/" + uploadformattedName; // FilePath
                                parameters[2] = true; // IsUploaded
                                parameters[3] = DateTime.Now; // UploadedDate
                                parameters[4] = Environment.MachineName; // CreatedMachineInfo
                                parameters[5] = AppUserManager.GetUserId(); // CreatedBy
                                parameters[6] = DateTime.Now; // CreatedOn
                                parameters[7] = true; // IsActive
                                parameters[8] = string.Empty; // UploadErrors
                                parameters[9] = false; // IsReUploaded
                                parameters[10] = Convert.ToInt32(MediaType.Validation); // MediaType
                                parameters[11] = Convert.ToInt32(benif_Count);
                                parameters[12] = Convert.ToInt32(Validated_count);
                                parameters[13] = Convert.ToInt32(Notvalidated_count);
                                parameters[14] = string.Empty;
                                parameters[15] = string.Empty;

                                // Reupload Value  *** Code By Himanshu Rajput ***

                                parameters[16] = false;   // IsReUploadedPermitted
                                parameters[17] = 0;  // ReUploadedPermittedBy
                                parameters[18] = null;  // ReUploadedPermittedDate
                                parameters[19] = filesize;  // File Size

                                // Execute the stored procedure
                                objDalBaseClass.ExecuteProcedure(ref parameters, "SaveMediaQueueDetail");

                                byte[] excelFileData = System.IO.File.ReadAllBytes(excelFilePath);

                                // Specify the file's content type (MIME type)
                                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // Use the appropriate MIME type for your file

                                #region
                                // mail Send SFTP Server 

                                var mailSetting = db.MailSettings.FirstOrDefault(a => a.IsActive && (a.ProcessName + "").Trim().ToLower() == "ForwardValidationFileSftpServer");
                                if (mailSetting != null)
                                {
                                    MailSettings mailMessages = new MailSettings();
                                    try
                                    {
                                        mailMessages.Subject = "SFTP notification";
                                        mailMessages.ProcessName = "";
                                        mailMessages.MailTo = "";
                                        dynamic email = new Email("ForwardValidationFileSftpServerMailSend");
                                        if (App.API.Helper.SiteHelper.IsTestEmail == "1")
                                        {
                                            email.To = SiteHelper.TestEmail;
                                        }
                                        else
                                        {
                                            email.To = mailSetting.MailTo;
                                        }
                                        email.CC = mailSetting.CC;
                                        email.BCC = mailSetting.BCC;
                                        email.hostURL = SiteHelper.WebsiteURL;

                                        //To read Body from mail object.
                                        string htmlText = string.Empty;
                                        try
                                        {
                                            Postal.IEmailService emailService = new Postal.EmailService();
                                            System.Net.Mail.MailMessage messages = emailService.CreateMailMessage(email);
                                            using (var StreamObj = messages.AlternateViews.FirstOrDefault().ContentStream)
                                            using (StreamReader reader = new StreamReader(StreamObj))
                                            {
                                                htmlText = reader.ReadToEnd();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            htmlText = ex.Message;
                                        }
                                        mailMessages.Contents = htmlText;

                                        if (mailSetting != null && mailSetting.IsInstantMailing)
                                        {
                                            email.Send();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                                #endregion

                                // Return the file as a FileResult

                                var response = new HttpResponseMessage(HttpStatusCode.OK)
                                {
                                    Content = new ByteArrayContent(excelFileData)
                                };
                                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                                {
                                    FileName = "Validation.xlsx"
                                };
                            }

                            #endregion

                            if (region.Trim().ToUpper() == "JAMMU REGION" || region.Trim().ToUpper() == "JAMMU")
                            {
                                var JAMMU = new object();
                                if (DuplicateAccount.Count != 0)
                                {
                                    JAMMU = new { StatusCode = HttpStatusCode.OK, Message = "Duplicate data found", success = true, DuplicateData = DuplicateAccount, SuccessBeneficiaries = list_BeneficiariesDetails.Count(), InvalidReginDists = InvalidReginDist };
                                }
                                else
                                {
                                    JAMMU = new { StatusCode = HttpStatusCode.OK, Message = "Data uploaded successfully", success = true, SuccessBeneficiaries = list_BeneficiariesDetails.Count(), InvalidReginDists = InvalidReginDist };
                                }

                                returnedData.Add("JAMMU", JAMMU);
                            }
                            else
                            {
                                var KASHMIR = new object();
                                if (DuplicateAccount.Count != 0)
                                {
                                    KASHMIR = new { StatusCode = HttpStatusCode.OK, Message = "Duplicate data found", success = true, DuplicateData = DuplicateAccount, SuccessBeneficiaries = list_BeneficiariesDetails.Count(), InvalidReginDists = InvalidReginDist };
                                }
                                else
                                {
                                    KASHMIR = new { StatusCode = HttpStatusCode.OK, Message = "Data uploaded successfully", success = true, SuccessBeneficiaries = list_BeneficiariesDetails.Count(), InvalidReginDists = InvalidReginDist };
                                }

                                returnedData.Add("KASHMIR", KASHMIR);
                            }


                        }
                        else
                        {
                            returnedData.Add(request.Region, new { StatusCode = HttpStatusCode.OK, Message = "Not a valid region", success = false });
                        }
                    }
                    else
                    {
                        returnedData.Add(request.Region, new { StatusCode = HttpStatusCode.OK, Message = "Not a valid region", success = false });
                    }
                }

                return Ok(returnedData);
            }
            catch (Exception ex)
            {
                return Ok(new { StatusCode = HttpStatusCode.InternalServerError, Message = ex.Message, success = false });
            }
        }
        public AppUserManager OwinUserManger
        {
            get
            {
                if (db.Database.Connection.ConnectionString != ConnectionStringProvider.GetConnectionString())
                {
                    var context = System.Web.HttpContext.Current.GetOwinContext();
                    return AppUserManager.Create(new IdentityFactoryOptions<AppUserManager>(), context);
                }
                else
                    return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
        public IAuthenticationManager OwinAuthenticationManager
        {
            get { return System.Web.HttpContext.Current.GetOwinContext().Authentication; }
        }
        private async Task SignInAsync(App.Data.Entities.AppUser user, bool isPersistent)
        {
            OwinAuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            OwinAuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent },
              await user.GenerateUserIdentityAsync(OwinUserManger));
        }
        public string GetToken(string userId)
        {
            var key = ConfigurationManager.AppSettings["JwtKey"];

            var issuer = ConfigurationManager.AppSettings["JwtIssuer"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("userid", "userId"));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddMonths(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }
        //File Transfer To fstp Server
        static System.Data.DataTable ConvertCsvToDataTable(string fileName)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            try
            {
                byte[] fileData = System.IO.File.ReadAllBytes(fileName);

                using (Stream memoryStream = new MemoryStream(fileData))
                {
                    using (StreamReader sr = new StreamReader(memoryStream))
                    {
                        string[] headers = sr.ReadLine().Split(',');
                        if (headers.Any())
                        {
                            foreach (string header in headers)
                            {
                                dataTable.Columns.Add(header);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < headers.Length; i++)
                            {
                                dataTable.Columns.Add($"Column{i + 1}");
                            }
                            sr.BaseStream.Position = 0;
                        }

                        while (!sr.EndOfStream)
                        {
                            string[] rows = sr.ReadLine().Split(',');
                            DataRow dataRow = dataTable.NewRow();
                            for (int i = 0; i < headers.Length; i++)
                            {
                                dataRow[i] = rows[i];
                            }
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return dataTable;

        }
        private void UploadValidationFile(string excelFilePath, string formattedName, string region)
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

                    var directoryName = (region == "KASHMIR REGION" || region == "KASHMIR") ? "K_Validation" : "J_Validation";
                    string ftpServerUrl = Helper.Helper.UploadValidationFilePath(region, directoryName, formattedName);

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
                    string remoteDirectory = Helper.Helper.GetRegionBasedValidationPath(ftpSetting["sftpFilePath"], region) + "/Outbox";

                    var keyFile = new PrivateKeyFile(ftpSetting["sftpPrivateKeyPath"]);
                    var keyFiles = new[] { keyFile };
                    var methods = new List<AuthenticationMethod>
            {
                new PasswordAuthenticationMethod(username, password),
                new PrivateKeyAuthenticationMethod(username, keyFiles)
            };

                    // Create a new connection info with public key authentication
                    Renci.SshNet.ConnectionInfo connectionInfo = new Renci.SshNet.ConnectionInfo(host, port, username, methods.ToArray());

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
                            sftpClient.UploadFile(fileStream, System.IO.Path.Combine(remoteDirectory, formattedName));
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
        }
    }
}
