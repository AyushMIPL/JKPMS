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

using App.Web.Helper;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Web;
using Renci.SshNet;
using JKPS.DL;
using JKPS.COMMON;
using System.Web.Mvc;
using WebGrease.Activities;
using Newtonsoft.Json;
using System.Text;
using Postal;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using static App.Web.Helper.Helper;
using App.Data.ViewModels;

namespace JKPS_Bank_API.Controllers
{
    public class MasterBeneficiariesDetailsAPIController : ApiController
    {
        //private AppDbContext db = new AppDbContext();
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        private CurrentRegionProvider currentRegionProvider = new CurrentRegionProvider();
        private AppDbContext db;

        public MasterBeneficiariesDetailsAPIController()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        // ***** Code By Himanshu Rajput ** 03/04/2024 **

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterBeneficiariesDetailsAPI/AddAndFinalize")]
        public IHttpActionResult AddBeneficiariesDetails([FromBody] BeneficiariesRequest request)
        {
            try
            {


                if (request == null || request.BeneficiariesDetails == null || string.IsNullOrEmpty(request.Region))
                {
                    return BadRequest("Invalid data.");
                }
                // Duplicate Account single Value return Json  

                //RegionProvider.Region = request.Region;
                //if (request.Region == "KASHMIR REGION")
                //    ConnectionStringProvider.ConnectionName = "AppConnection1";

                //else
                //    ConnectionStringProvider.ConnectionName = "AppConnection";

                RegionProvider.Region = request.Region;

                if (request.Region.ToUpper() == "KASHMIR REGION" || request.Region.ToUpper() == "KASHMIR")
                    ConnectionStringProvider.ConnectionName = "AppConnection1";

                else
                    ConnectionStringProvider.ConnectionName = "AppConnection";

                //#region 
                // code With Himanshu Rajput (Invalid Data Checking)

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

                //#endregion

                List<string> DuplicateAccount = new List<string>();




                List<MasterBeneficiariesDetails> list_BeneficiariesDetails = new List<MasterBeneficiariesDetails>();

                // Duplicate value retrun with Json Full model response

                //List<MasterBeneficiariesDetails> DuplicateAccount = new List<MasterBeneficiariesDetails>();

                // Check the records already exist

                foreach (var item1 in ForwordList)
                {
                    var recordExist = db.MasterBeneficiariesDetails
                        .FirstOrDefault(x => x.ApplicationReferenceNo == item1.ApplicationReferenceNo && x.SNo == item1.SNo);

                    if (recordExist != null)
                    {

                        DuplicateAccount.Add(item1.ApplicationReferenceNo);


                        //return Ok(new { StatusCode = HttpStatusCode.Conflict, success = false });
                        //return Ok(new { StatusCode = HttpStatusCode.Conflict, Message = "Record Already Exists", success = false });
                    }
                    else
                    {
                        list_BeneficiariesDetails.Add(item1);
                    }
                }

                int userId = AppUserManager.GetUserId();
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
                        MasterBeneficiariesDetails.MasterBeneficiariesId = beneficiaries_record.Id;
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
                        db.MasterBeneficiariesDetails.Add(item);
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

                //var BankName = "J & K GRAMEEN BANK";
                var BankName = "JKB,THE JAMMU AND KASHMIR BANK LTD.";

                BankName = BankName.Replace("ANDSYMBOL", "&");
                int UserId = AppUserManager.GetUserId();
                //int RoleId = db.UserRole.FirstOrDefault(x => x.UserId == UserId).RoleId;
                Dictionary<string, string> ftpSetting = Helper.GetFTPSetting();

                // Format the date and time as a string (e.g., "yyyyMMdd_HHmmss")
                string formattedName = "ContributionCSV_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                //string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                //string formattedName;

                //if (!string.IsNullOrEmpty(ApplicantIFSCCode))
                //{
                //    formattedName = $"{BankName}_{ApplicantIFSCCode}_{timestamp}.csv";
                //}
                //else
                //{
                //    formattedName = $"{BankName}_{timestamp}.csv";
                //}
                formattedName = formattedName.Replace(" ", "");
                // Define the path for sql create SVC

                //var region = GetRegionName();
                var region = currentRegionProvider.GetCurrentRegion();
                var directoryName = region == "KASHMIR REGION" ? "K_MasterEmployeeDownloads" : "J_MasterEmployeeDownloads";
                //string filePath = ftpSetting["localFilePath"] + $"\\DataFiles\\{directoryName}\\" + formattedName;
                string filePath = Helper.AddExcelSheetIntoDatabasePath(region, directoryName, formattedName);


                //string filePath = ftpSetting["localFilePath"] + "\\DataFiles\\MasterEmployeeDownloads\\" + formattedName;

                //string serverMapPatht = Server.MapPath("~/DataFile/MasterEmployeeDownloads");
                //string filePath = Path.Combine(serverMapPatht, formattedName);

                // Define the parameters if needed (e.g., for input parameters)
                //var parameter3 = new SqlParameter("@RoleId", RoleId);
                var parameter4 = new SqlParameter("@filePathWithName", filePath);
                var parameter5 = new SqlParameter("@UserId", UserId);
                //var parameter3 = new SqlParameter("@ApplicantIFSCCode", ApplicantIFSCCode);
                var parameter6 = new SqlParameter("@BankName", BankName);
                // Execute the stored procedure
                int resultt = db.Database.ExecuteSqlCommand("EXEC MasterEmployeeExcelExport @filePathWithName, @UserId,@BankName", parameter4, parameter5, parameter6);//parameter3,

                // Create an FTP client
                WebClient ftpClient = new WebClient();
                ftpClient.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);
                string path = Helper.AddExcelSheetIntoDatabasePath1(region, directoryName, formattedName);

                //string path = ftpSetting["ftpServerUrl"] + ("/DataFiles/MasterEmployeeDownloads/" + formattedName);

                // File path, attampt, dealy in attampt
                bool isExist = Helper.FileCheckInFTP(path, 3, 30);

                //bool isExist = System.IO.File.Exists(filePath);

                if (isExist)
                {
                    // Download the file from the FTP server
                    byte[] fileData = ftpClient.DownloadData(path);

                    //byte[] fileData = System.IO.File.ReadAllBytes(filePath);

                    // Specify the file path where you want to save the uploaded file
                    string serverMapPath = Helper.SFTPMapPath(region, directoryName);

                    //string serverMapPath = System.Web.HttpContext.Current.Server.MapPath("~/DataFile/MasterEmployeeDownloads");
                    if (!Directory.Exists(serverMapPath))
                    {
                        // Attempt to create the directory
                        Directory.CreateDirectory(serverMapPath);
                    }
                    // Add file name with directory
                    string csvFilePath = Path.Combine(serverMapPath, formattedName);

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

                    string uploadformattedName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_Validation" + (Path.GetExtension(excelFilePath));

                    UploadValidationFile(excelFilePath, uploadformattedName);

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
                    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                    object[] parameters = new object[20];
                    /* parameters[0] = 0;*/ // Paysearch.pybatchid; // RecordId 
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
                            if (SiteHelper.IsTestEmail == "1")
                            {
                                email.To = SiteHelper.TestEmail;
                            }
                            else
                            {
                                email.To = mailSetting.MailTo;
                            }
                            email.CC = mailSetting.CC;
                            email.BCC = mailSetting.BCC;
                            //email.CustomerName = "";
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
                                //mailMessages.IsInstantMailing = true;
                                //mailMessages.CreatedOn= DateTime.Now;
                            }
                        }
                        catch (Exception ex)
                        {
                            // mailMessages.IsSent = false;
                            //mailMessages.ErrorDescription = ex.Message;
                        }
                    }
                    #endregion




                    // Return the file as a FileResult
                    // return FileResult(excelFileData, contentType, "Validation.xlsx"); // "file.txt" is the suggested file name
                    // 
                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(excelFileData)
                    };
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "Validation.xlsx"
                    };

                    //response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

                    ////return ResponseMessage(response);
                    //var message = new { StatusCode = HttpStatusCode.OK, Message = "successfully", success = true};

                    //// You can send the message as JSON in the response body
                    //var messageJson = JsonConvert.SerializeObject(message);
                    //response.Content = new StringContent(messageJson, Encoding.UTF8, "application/json");

                    //// Return the response message
                    //return ResponseMessage(response);



                    // Second File 

                    //// Set the content type header
                    //response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

                    //// Set the content disposition header to trigger file download
                    //response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    //{
                    //    FileName = "Validation.xlsx" // Specify the file name
                    //};

                    //// Optionally, include a message in the response headers or body
                    //var message = new { StatusCode = HttpStatusCode.OK, Message = "Successfully", success = true };

                    //// Add the message as headers
                    //foreach (var property in message.GetType().GetProperties())
                    //{
                    //    response.Headers.Add(property.Name, property.GetValue(message).ToString());
                    //}

                    //// Return the response message
                    //return ResponseMessage(response);
                }

                #endregion
                if (DuplicateAccount.Count != 0)
                {
                    return Ok(new { StatusCode = HttpStatusCode.OK, Message = "Duplicate data found", success = true, DuplicateData = DuplicateAccount, SuccessBeneficiaries = list_BeneficiariesDetails.Count(), InvalidReginDists = InvalidReginDist });
                }
                return Ok(new { StatusCode = HttpStatusCode.OK, Message = "Data uploaded successfully", success = true, SuccessBeneficiaries = list_BeneficiariesDetails.Count(), InvalidReginDists = InvalidReginDist });
            }
            catch (Exception ex)
            {
                return Ok(new { StatusCode = HttpStatusCode.InternalServerError, Message = ex.Message, success = false });
            }
        }




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
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw;
            }
            return dataTable;

        }

        //File Transfer To fstp Server

        private void UploadValidationFile(string excelFilePath, string formattedName)
        {
            try
            {
                Dictionary<string, string> ftpSetting = Helper.GetFTPSetting();
                // Get the file name

                bool IsFTP = Convert.ToBoolean(ftpSetting["IsFTP"]);
                if (IsFTP)
                {
                    //string formattedName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "JK_Disbursement" + (Path.GetExtension(excelFilePath));

                    byte[] fileContents = System.IO.File.ReadAllBytes(excelFilePath);

                    //var region = GetRegionName();
                    var region = currentRegionProvider.GetCurrentRegion();
                    var directoryName = region == "KASHMIR REGION" ? "K_Validation" : "J_Validation";
                    //string ftpServerUrl = ftpSetting["sftpServerUrl"] + $"/DataFiles/{directoryName}/Outbox/" + formattedName;
                    string ftpServerUrl = Helper.UploadValidationFilePath(region, directoryName, formattedName);


                    //string ftpServerUrl = ftpSetting["sftpServerUrl"] + "/DataFiles/Validation/Outbox/" + formattedName;

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
                    string remoteDirectory = Helper.GetRegionBasedValidationPath(ftpSetting["sftpFilePath"], region) + "/Outbox";

                    var keyFile = new PrivateKeyFile(ftpSetting["sftpPrivateKeyPath"]);
                    var keyFiles = new[] { keyFile };
                    var methods = new List<AuthenticationMethod>
            {
                new PasswordAuthenticationMethod(username, password),
                new PrivateKeyAuthenticationMethod(username, keyFiles)
            };

                    // Create a new connection info with public key authentication
                    Renci.SshNet.ConnectionInfo connectionInfo = new Renci.SshNet.ConnectionInfo(host, port, username, methods.ToArray());
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
                    //using (var client = new SftpClient(host, port, username, password))
                    //{
                    //  client.Connect();

                    //  // Ensure the remote directory exists
                    //  if (!client.Exists(remoteDirectory))
                    //  {
                    //    client.CreateDirectory(remoteDirectory);
                    //  }

                    //  using (var fileStream = new FileStream(localFilePath, FileMode.Open))
                    //  {
                    //    // Upload the file
                    //    client.UploadFile(fileStream, Path.Combine(remoteDirectory, Path.GetFileName(localFilePath)));
                    //  }

                    //  client.Disconnect();
                    //}
                }
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw;
            }
        }









        //[HttpPost]
        //[Route("api/MasterBeneficiariesDetailsAPI/AddBeneficiariesDetails")]
        //public IHttpActionResult AddBeneficiariesDetails(List<MasterBeneficiariesDetails> fileUpload_BeneficiariesDetails, int? Id)
        //{
        //    try
        //    {
        //        // Check the records already exist

        //        foreach (var item in fileUpload_BeneficiariesDetails)
        //        {
        //            var RecordExist = db.MasterBeneficiariesDetails
        //                .Where(x => x.ApplicationReferenceNo == item.ApplicationReferenceNo && x.SNo == item.SNo)
        //                .FirstOrDefault();

        //            if (RecordExist == null)
        //            {
        //                db.MasterBeneficiariesDetails.Add(item);
        //                db.SaveChanges();
        //            }

        //            else
        //            {
        //                return Ok(new { StatusCode = HttpStatusCode.Conflict, Message = "Record Already Exist", success = false });
        //            }
        //        }

        //        return Ok(new { StatusCode = HttpStatusCode.OK, Message = "Successfully", success = true });
        //    }
        //    catch (Exception ex)
        //    {

        //        return Ok(new { StatusCode = HttpStatusCode.InternalServerError, ex.Message});
        //    }
        //}




        //[HttpPost]
        //[Route("api/MasterBeneficiariesDetailsAPI/FinalizeTotalContribution")]
        //public IHttpActionResult FinalizeTotalContribution(int? Id)
        //{
        //    try
        //    {
        //        int UserId = AppUserManager.GetUserId();
        //        // Define the parameters if needed (e.g., for input parameters)
        //        var parameter1 = new SqlParameter("@Id", Id);
        //        var parameter2 = new SqlParameter("@UserId", UserId);
        //        // Execute the stored procedure
        //        int result = db.Database.ExecuteSqlCommand("EXEC USP_EmployeeIns_EmpIncomIns_EmpDirDpstIns @Id,@UserId", parameter1, parameter2);

        //        return Ok(new { StatusCode = HttpStatusCode.OK, Message = "Finalized", success = true });

        //        //return Ok("Finalized");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new { StatusCode = HttpStatusCode.InternalServerError, ex.Message });

        //        //return Ok(ex.Message);
        //    }
        //}
    }
}
