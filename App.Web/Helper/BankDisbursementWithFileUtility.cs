using App.Data;
using JKPS.BLL;
using JKPS.COMMON;
using JKPS.DL;
using log4net;
using OfficeOpenXml;
using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.Web.Helper
{
    public class BankDisbursementWithFileUtility : IJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BankDisbursementUtility));
        private CurrentRegionProvider currentRegionProvider = new CurrentRegionProvider();
        private AppDbContext db;

        public BankDisbursementWithFileUtility()
        {
            ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        public async Task<string> UpdateDisbursementWithFileResponse(string BatchId, string District, HttpPostedFileBase file, string tempFolderPath)
        {
            var Response = await Execute(null, BatchId, District, file, tempFolderPath);
            return Response;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() => true);
            return task;
        }
        public async Task<string> Execute(IJobExecutionContext context, string BatchId, string District, HttpPostedFileBase file
            , string tempFolderPath)
        {
            var AuditLogs = await db.AuditLogs.ToListAsync();
            string Message = string.Empty;
            try
            {
                try
                {
                    // verify file name(with batch id and district)
                    bool isFileNameVerified = VerifyFileName(BatchId, District, file.FileName);
                    if (isFileNameVerified)
                    {
                        Logger.Info("BankDisbursementUtility START");
                        Dictionary<string, string> ftpSetting = Helper.GetFTPSetting();
                        string csvFileName = file.FileName;// Replace(".xlsx", ".csv").Replace(".xls", ".csv");
                        bool isProcessed = FileIsProcessed(file.FileName);
                        if (!isProcessed)
                        {
                            UploadDownloadedFileOnDatabaseServer(file, tempFolderPath);
                            var regionDis = currentRegionProvider.GetCurrentRegion();
                            var directoryNamDis = regionDis == "KASHMIR REGION" ? "K_BankMediaFileUpload" : "J_BankMediaFileUpload";
                            string databaseFilePath = ftpSetting["localFilePath"] + $"/DataFiles/{directoryNamDis}/" + csvFileName;
                            UpdateBankMediaExcelDatabase(databaseFilePath);
                            //UpdatePostedStatus();
                            Update_PostedstatusByDistrict(District);
                            SaveDownloadMediaDetail(file.FileName, true);

                            // close batch only when all district disbusement files are processed 
                            int batchIdInt = int.Parse(BatchId);
                            int MediaQueueCount = db.Media_Queue.Where(x => x.RecordId == batchIdInt).Count();
                            int MediaDownloadCount = db.MediaDownloads.Where(x => x.RecordId == batchIdInt).Count();
                            bool isAllFileProcessed = false;
                            if (MediaQueueCount == MediaDownloadCount)
                            {
                                // is all file processed
                                bool isAllFileNotProcessed = db.MediaDownloads.Where(x => x.RecordId == batchIdInt).Any(y => !y.IsProcessed);
                                if (!isAllFileNotProcessed)
                                {
                                    isAllFileProcessed = true;
                                }
                            }

                            if (isAllFileProcessed)
                            {
                                CloseBatchProcessAjax();
                            }

                            Message = "Disbursement response updated successfully.";
                        }
                        else
                        {
                            Message = "Error: File already processed or File name is incorrect.";
                        }
                    }
                    else
                    {
                        Message = "Error: Invalid file name, Please upload the downloaded file for particular district.";
                    }
                }
                catch (WebException ex)
                {
                    Message = $"Error: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                Message = $"Error: {ex.Message}";
            }

            return Message;
        }

        public bool VerifyFileName(string BatchId, string District, string FileName)
        {
            bool isFileNameVerified = false;
            try
            {
                string[] FileNameArray = FileName.Split('_');
                if (FileNameArray.Length == 5)
                {
                    if (District.Trim().ToUpper() == FileNameArray[0].Trim().ToUpper()
                        && BatchId == FileNameArray[1])
                    {
                        isFileNameVerified = true;
                    }
                }
            }
            catch { }

            return isFileNameVerified;
        }

        public void SaveArchiveFile(string FileName, NetworkCredential Credential, string ftpHost)
        {
            Dictionary<string, string> ftpSetting = Helper.GetFTPSetting();
            var region = currentRegionProvider.GetCurrentRegion();
            var directoryNames = region == "KASHMIR REGION" ? "K_Archive" : "J_Archive";
            string DestinationFolderPath = ftpHost + ftpSetting["sftpFilePath"] + $"/{directoryNames}/";

            //var regionDis = currentRegionProvider.GetCurrentRegion();
            var regionDis = currentRegionProvider.GetCurrentRegion();
            var directoryNameDis = regionDis == "KASHMIR REGION" ? "K_Disbursement" : "J_Disbursement";

            //string DestinationFolderPath = ftpHost + ftpSetting["sftpFilePath"] + "/Archive/";
            List<string> SourceFileUrl = new List<string>
            {
                ftpHost + ftpSetting["sftpFilePath"] + $"/{directoryNameDis}/Inbox/" + FileName,
               ftpHost + ftpSetting["sftpFilePath"] + $"/{directoryNameDis}/Outbox/" + FileName

               //ftpHost + ftpSetting["sftpFilePath"] + "/Disbursement/Inbox/" + FileName,
               //ftpHost + ftpSetting["sftpFilePath"] + "/Disbursement/Outbox/" + FileName
            };
            try
            {

                foreach (string SourcePath in SourceFileUrl)
                {
                    Uri sourceUri = new Uri(SourcePath);
                    Uri destinationUri = new Uri(ftpHost + ftpSetting["sftpFilePath"] + $"/{directoryNames}/Outbox_" + FileName);
                    //Uri destinationUri = new Uri(ftpHost + ftpSetting["sftpFilePath"] + "/Archive/Outbox_" + FileName);
                    if (SourcePath.ToLower().Trim().Contains("inbox"))
                        destinationUri = new Uri(ftpHost + ftpSetting["sftpFilePath"] + $"/{directoryNames}/Inbox_" + FileName);
                    //destinationUri = new Uri(ftpHost + ftpSetting["sftpFilePath"] + "/Archive/Inbox_" + FileName);
                    else
                        destinationUri = new Uri(ftpHost + ftpSetting["sftpFilePath"] + $"/{directoryNames}/Outbox_" + FileName);
                    //destinationUri = new Uri(ftpHost + ftpSetting["sftpFilePath"] + "/Archive/Outbox_" + FileName);
                    if (!FtpDirectoryExists(DestinationFolderPath, Credential))
                    {
                        FtpWebRequest createDirectoryRequest = (FtpWebRequest)WebRequest.Create(DestinationFolderPath);
                        createDirectoryRequest.Credentials = Credential;
                        createDirectoryRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                        FtpWebResponse response = (FtpWebResponse)createDirectoryRequest.GetResponse();

                    }
                    FtpWebRequest sourceRequest = (FtpWebRequest)WebRequest.Create(sourceUri);
                    sourceRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                    sourceRequest.Credentials = Credential;
                    FtpWebRequest destinationRequest = (FtpWebRequest)WebRequest.Create(destinationUri);
                    destinationRequest.Method = WebRequestMethods.Ftp.UploadFile;
                    destinationRequest.Credentials = Credential;
                    long maxFileSize = 20 * 1024 * 1024; // 20 MB limit
                    long totalBytesRead = 0;
                    int bufferSize = 32768; // 32 KB buffer
                    byte[] buffer = new byte[bufferSize];
                    sourceRequest.Timeout = 300000; // 5 minutes timeout for connection
                    sourceRequest.ReadWriteTimeout = 300000; // 5 minutes read/write timeout

                    using (FtpWebResponse sourceResponse = (FtpWebResponse)sourceRequest.GetResponse())
                    using (Stream sourceStream = sourceResponse.GetResponseStream())
                    {
                        using (Stream destinationStream = destinationRequest.GetRequestStream())
                        {

                            int bytesRead;
                            while ((bytesRead = destinationStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                sourceStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public bool FtpDirectoryExists(string directoryPath, NetworkCredential credentials)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(directoryPath);
            request.Credentials = credentials;
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            try
            {
                using (request.GetResponse())
                {
                    return true;
                }
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return false;
                }
                throw;
            }
        }
        private void SaveDownloadMediaDetail(string fileName, bool IsProcessed, Int32 TotalBeneficiary = 0, Int32 TotalValidated = 0, Int32 TotalNotvalidated = 0, string remark = "")
        {
            try
            {
                string recordId = "0";
                if (fileName != "" && fileName != null)
                {
                    var fileNames = fileName.Split('_')[1];
                    recordId = fileNames.Split('_')[0];
                    //List<DVOPYBatchProcessStybatchr> lstDVOPYBatchProcessStybatchr = BLLPYBatchProcessStybatchr.GetActiveBatch();
                    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

                    object[] parameters = new object[16];
                    parameters[0] = fileName; // FileName 
                    parameters[1] = true; // HasDownoaded
                    parameters[2] = IsProcessed; // IsProcessed
                    parameters[3] = Environment.MachineName; // CreatedMachineInfo
                    parameters[4] = 1; // CreatedBy
                    parameters[5] = DateTime.Now; // CreatedOn
                    parameters[6] = true; // IsActive
                    parameters[7] = Convert.ToInt32(MediaType.RetTxnReport); // MediaType

                    parameters[8] = Environment.MachineName; // ModifiedMachineInfo
                    parameters[9] = 1; // ModifiedBy
                    parameters[10] = DateTime.Now; // ModifiedOn
                    parameters[11] = recordId; // RecordID

                    parameters[12] = TotalBeneficiary; // ModifiedBy
                    parameters[13] = TotalValidated; // ModifiedOn
                    parameters[14] = TotalNotvalidated; // RecordID
                    parameters[15] = remark; // RecordID

                    // Execute the stored procedure
                    objDalBaseClass.ExecuteProcedure(ref parameters, "SaveDownloadMediaDetail");
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void UploadDownloadedFileOnDatabaseServer(HttpPostedFileBase file, string tempFolderPath)
        {
            try
            {
                byte[] fileBytes = null;

                if (file != null && file.ContentLength > 0)
                {
                    using (var binaryReader = new System.IO.BinaryReader(file.InputStream))
                    {
                        fileBytes = binaryReader.ReadBytes(file.ContentLength);
                    }
                }

                string csvFilePath = string.Empty;
                string csvFileName = file.FileName;//.Replace(".xlsx", ".csv");
                csvFilePath = ConvertExcelBytesToCsv(fileBytes, csvFileName, file.FileName, tempFolderPath);

                byte[] bytes = System.IO.File.ReadAllBytes(csvFilePath);
                string failedMessage = UploadDataFile(bytes, Path.GetFileName(csvFilePath));
                // Convert csv file to data table to get counts
                DataTable dataTable = ConvertCsvToDataTable(csvFilePath);
                
                int benif_Count = dataTable.Rows.Count;
                int Validated_count = dataTable.AsEnumerable().Count(row => StatusValidator.IsValidSuccess(row.Field<string>("Status")));
                int Notvalidated_count = dataTable.AsEnumerable().Count(row => !StatusValidator.IsValidSuccess(row.Field<string>("Status")));
                SaveDownloadMediaDetail(file.FileName, false, benif_Count, Validated_count, Notvalidated_count, "");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string ConvertExcelBytesToCsv(
    byte[] excelFileBytes,
    string csvFileName,
    string excelFileName,
    string tempFolderPath)
        {
            try
            {
                //if (excelFileBytes == null || excelFileBytes.Length < 1024)
                //    return string.Empty;

                string extension = Path.GetExtension(excelFileName).ToLower();

                //string csvPath = Path.Combine(
                //        tempFolderPath,
                //        Guid.NewGuid() + "_" + DateTime.Now.Ticks + ".csv"
                //    );
                string csvPath = Path.Combine(tempFolderPath, Path.GetFileNameWithoutExtension(excelFileName) + ".csv");

                if (extension == ".csv")
                {
                    if (!Directory.Exists(tempFolderPath))
                        Directory.CreateDirectory(tempFolderPath);

                    File.WriteAllBytes(csvPath, excelFileBytes);
                    return csvPath;
                }
                else if (extension == ".xlsx")
                {
                    var csvBuilder = new StringBuilder();

                    using (var stream = new MemoryStream(excelFileBytes))
                    {
                        stream.Position = 0;  

                        using (var package = new ExcelPackage(stream))
                        {
                            if (package.Workbook == null || package.Workbook.Worksheets.Count == 0)
                                return string.Empty;

                            var worksheet = package.Workbook.Worksheets[1]; // EPPlus 4.5 is 1-based

                            if (worksheet.Dimension == null)
                                return string.Empty;

                            int rowCount = worksheet.Dimension.End.Row;
                            int colCount = worksheet.Dimension.End.Column;

                            for (int row = 1; row <= rowCount; row++)
                            {
                                var values = new List<string>();

                                for (int col = 1; col <= colCount; col++)
                                {
                                    string text = worksheet.Cells[row, col].Text ?? "";

                                    text = text.Replace("\"", "\"\"");

                                    if (text.Contains(",") || text.Contains("\"") || text.Contains("\n"))
                                        text = $"\"{text}\"";

                                    values.Add(text);
                                }

                                csvBuilder.AppendLine(string.Join(",", values));
                            }
                        }
                    }

                    File.WriteAllText(csvPath, csvBuilder.ToString(), Encoding.UTF8);
                    return csvPath;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }






        static System.Data.DataTable ConvertCsvToDataTable(string fileName)
        {
          System.Data.DataTable dataTable = new System.Data.DataTable();
          try
          {
            byte[] fileData = System.IO.File.ReadAllBytes(fileName);

            // Manually defined headers
            string[] headers = new string[]
            {
                "Application Reference No.", "Application Reference No1.", "Department", "Department Account No.",
                "Amount", "Date", "Department Bank Name", "Department Bank IFSC", "Name", "IFSC",
                "Account No.", "Scheme", "Status", "Transaction Refrence No.", "Transaction Date", "Reason/Remarks"
            };

            
            // ✅ Add a blank first row
            DataRow blankRow = dataTable.NewRow();
            dataTable.Rows.Add(blankRow);
            // Add columns to DataTable
            foreach (string header in headers)
            {
              dataTable.Columns.Add(header);
            }
            using (Stream memoryStream = new MemoryStream(fileData))
            using (StreamReader sr = new StreamReader(memoryStream))
            {
              while (!sr.EndOfStream)
              {
                string line = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] values = line.Split(',');

                // Skip malformed rows
                if (values.Length < headers.Length) continue;

                DataRow row = dataTable.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                  row[i] = values[i].Trim();
                }
                dataTable.Rows.Add(row);
              }
            }
          }
          catch (Exception)
          {
            throw;
          }

          return dataTable;
        }



    //static System.Data.DataTable ConvertCsvToDataTable(string fileName)
    //{
    //    System.Data.DataTable dataTable = new System.Data.DataTable();
    //    try
    //    {
    //        byte[] fileData = System.IO.File.ReadAllBytes(fileName);

    //        using (Stream memoryStream = new MemoryStream(fileData))
    //        {
    //            using (StreamReader sr = new StreamReader(memoryStream))
    //            {
    //                string[] headers = sr.ReadLine().Split(',').Where((item, index) => index != 0).ToArray();
    //                if (headers.Any())
    //                {
    //                    foreach (string header in headers)
    //                    {
    //                        dataTable.Columns.Add(header);
    //                    }
    //                }
    //                else
    //                {
    //                    for (int i = 0; i < headers.Length; i++)
    //                    {
    //                        dataTable.Columns.Add($"Column{i + 1}");
    //                    }
    //                    sr.BaseStream.Position = 0;
    //                }

    //                while (!sr.EndOfStream)
    //                {
    //                    string[] rows = sr.ReadLine().Split(',').Where((item, index) => index != 0).ToArray();
    //                    DataRow dataRow = dataTable.NewRow();
    //                    for (int i = 0; i < headers.Length; i++)
    //                    {
    //                        dataRow[i] = rows[i];
    //                    }
    //                    dataTable.Rows.Add(dataRow);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //    return dataTable;

    //}
        static void SaveExcelAsCsv(string excelFilePath, string csvFilePath)
        {
            try
            {
                /*
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(excelFilePath);
                wb.SaveAs(csvFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlCSVWindows);
                wb.Close(false);
                app.Quit();
                */

                using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var csvContent = new StringBuilder();
                    if (worksheet.Dimension != null)
                    {
                        int rowCount = worksheet.Dimension.End.Row;
                        int colCount = worksheet.Dimension.End.Column;

                        for (int row = 1; row <= rowCount; row++)
                        {
                            for (int col = 1; col <= colCount; col++)
                            {
                                string text = worksheet.Cells[row, col].Value?.ToString() ?? "";
                                if (text.Contains(",") || text.Contains("\"") || text.Contains("\n"))
                                {
                                    text = "\"" + text.Replace("\"", "\"\"") + "\"";
                                }
                                csvContent.Append(text + (col == colCount ? "" : ","));
                            }
                            csvContent.AppendLine();
                        }
                    }
                    File.WriteAllText(csvFilePath, csvContent.ToString(), Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private string UploadDataFile(byte[] fileContents, string fileName)
        {
            string failedMessage = string.Empty;
            try
            {
                Dictionary<string, string> ftpSetting = Helper.GetFTPSetting();
                // Get the file name

                //var region = GetRegionName();
                var region = currentRegionProvider.GetCurrentRegion();
                var directoryNames = region == "KASHMIR REGION" ? "K_BankMediaFileUpload" : "J_BankMediaFileUpload";

                string ftpServerUrl = ftpSetting["ftpServerUrl"] + $"/DataFiles/{directoryNames}/" + fileName;

                //string ftpServerUrl = ftpSetting["ftpServerUrl"] + "/DataFiles/BankMediaFileUpload/" + fileName;

                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpServerUrl);
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.Timeout = 600000;
                ftpRequest.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);
                
                using (Stream requestStream = ftpRequest.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }

                FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpResponse.Close();
            }
            catch (Exception ex)
            {
                failedMessage = ex.Message;
                throw;
            }
            return failedMessage;
        }
        private void UpdateBankMediaExcelDatabase(string localDirectory)
        {
            try
            {
                int userId = 1;
                // Define the parameters if needed (e.g., for input parameters)

                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

                object[] parameters = new object[2];
                parameters[0] = localDirectory;
                parameters[1] = userId;
                // Execute the stored procedure
                int result = objDalBaseClass.ExecuteProcedure(ref parameters, "UpdateBankMediaExcelDatabase");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void UpdatePostedStatus()
        {
            try
            {
                DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                DataSet ds = BLLPayrollAutopayNew.updatePostedstatus(ref objDVOPayrollProcess_PayEmployee);
            }
            catch (Exception ex)
            {

            }
        }

        private void Update_PostedstatusByDistrict(string District)
        {
            try
            {
                DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                DataSet ds = BLLPayrollAutopayNew.Update_PostedstatusByDistrict(ref objDVOPayrollProcess_PayEmployee, District);
            }
            catch (Exception ex)
            {

            }
        }
        private void CloseBatchProcessAjax()
        {
            try
            {
                List<DVOPYBatchProcessStybatchr> lstDVOPYBatchProcessStybatchr = BLLPYBatchProcessStybatchr.GetActiveBatch();

                DVOPYBatchProcessStybatchr pObjBatch = null;
                //Get Payroll Active batch,if any. 
                if (lstDVOPYBatchProcessStybatchr[0].pybatchid != null && lstDVOPYBatchProcessStybatchr[0].pybatchid >= 0)
                {
                    int userid = 1;
                    DVOPYBatchProcessStybatchr objDVOPYBatchProcessStybatchr = new DVOPYBatchProcessStybatchr();
                    objDVOPYBatchProcessStybatchr.pybatchid = lstDVOPYBatchProcessStybatchr[0].pybatchid;
                    objDVOPYBatchProcessStybatchr.updateby = userid;
                    objDVOPYBatchProcessStybatchr.insertmachineinfo = System.Environment.MachineName;
                    int UPDResult = BLLPYBatchProcessStybatchr.UPDATEBatchProcessStatus(ref objDVOPYBatchProcessStybatchr);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private Boolean FileIsProcessed(string fileName)
        {
            try
            {
                bool isProcessed = false;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

                object[] parameters = new object[11];
                parameters[0] = fileName; // FileName 
                                          // Execute the stored procedure
                var ds = objDalBaseClass.GetData("SELECT IsProcessed FROM [dbo].[MediaDownloads] WHERE FileName = " + "'" + fileName + "'");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                        {
                            isProcessed = Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                        }
                    }
                }
                return isProcessed;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}