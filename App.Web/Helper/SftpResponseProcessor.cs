using App.Data;
using App.Data.Entities;
using JKPS.COMMON;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using JKPS.DL;
using ExcelDataReader;

namespace App.Web.Helper
{
    public class SftpProcessResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class SftpResponseProcessor
    {
        private readonly AppDbContext _db;
        private readonly CurrentRegionProvider _regionProvider = new CurrentRegionProvider();
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(SftpResponseProcessor));

        public SftpResponseProcessor(AppDbContext context)
        {
            _db = context;
            EnsureSftpResponseHistoryColumnsExist();
        }

        private void EnsureSftpResponseHistoryColumnsExist()
        {
            try
            {
                _db.Database.ExecuteSqlCommand(@"
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SftpResponseHistory]') AND name = N'DistrictId')
                    BEGIN
                        ALTER TABLE [dbo].[SftpResponseHistory] ADD [DistrictId] INT NULL;
                    END

                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SftpResponseHistory]') AND name = N'Region')
                    BEGIN
                        ALTER TABLE [dbo].[SftpResponseHistory] ADD [Region] NVARCHAR(150) NULL;
                    END

                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SftpResponseHistory]') AND name = N'UploadHistoryId')
                    BEGIN
                        ALTER TABLE [dbo].[SftpResponseHistory] ADD [UploadHistoryId] INT NULL;
                    END
                ");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to alter SftpResponseHistory table", ex);
            }
        }

        private PensionFileUploadHistory GetMatchingUploadHistory(string responseFileName)
        {
            try
            {
                // Extract the unique timestamp prefix (first two segments of filename separated by '_')
                // e.g., "20260507_120000" from "20260507_120000_DODA_response.xlsx"
                int firstUnderscore = responseFileName.IndexOf('_');
                if (firstUnderscore > 0)
                {
                    int secondUnderscore = responseFileName.IndexOf('_', firstUnderscore + 1);
                    if (secondUnderscore > 0)
                    {
                        string prefix = responseFileName.Substring(0, secondUnderscore);
                        
                        // Query the database for an upload history record whose file path contains this prefix
                        var match = _db.PensionFileUploadHistory
                                       .AsNoTracking()
                                       .FirstOrDefault(x => x.FilePath.Contains(prefix));
                        if (match != null)
                        {
                            return match;
                        }
                    }
                }

                // Fallback: Check if responseFileName contains any original sent file name as a substring
                string respNameWithoutExt = Path.GetFileNameWithoutExtension(responseFileName).ToUpper();
                var recentUploads = _db.PensionFileUploadHistory
                                       .AsNoTracking()
                                       .OrderByDescending(x => x.Id)
                                       .Take(1000)
                                       .ToList();

                foreach (var upload in recentUploads)
                {
                    if (!string.IsNullOrEmpty(upload.FileName))
                    {
                        string sentNameWithoutExt = Path.GetFileNameWithoutExtension(upload.FileName).ToUpper();
                        if (!string.IsNullOrEmpty(sentNameWithoutExt) && sentNameWithoutExt.Length >= 3)
                        {
                            if (respNameWithoutExt.Contains(sentNameWithoutExt))
                            {
                                return upload;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(upload.FilePath))
                    {
                        string sentNameFromPath = Path.GetFileNameWithoutExtension(upload.FilePath).ToUpper();
                        if (!string.IsNullOrEmpty(sentNameFromPath) && sentNameFromPath.Length >= 3)
                        {
                            if (respNameWithoutExt.Contains(sentNameFromPath))
                            {
                                return upload;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error matching upload history for file: " + responseFileName, ex);
            }
            return null;
        }

        public SftpProcessResult FetchAndProcessResponses(string responseType, string selectedRegion = null)
        {
            try
            {
                var ftpSetting = Helper.GetFTPSetting();
                string host = ftpSetting["sftpServerUrl"];
                int port = Convert.ToInt32(ftpSetting["sftpPort"]);
                string username = ftpSetting["sftpUsername"];
                string password = ftpSetting["sftpPassword"];

                string district = string.IsNullOrEmpty(selectedRegion) ? _regionProvider.GetCurrentRegion() : selectedRegion;

                string parentRegion = "JAMMU REGION"; // Default fallback
                var distObj = _db.MasterDistrict.FirstOrDefault(d => d.Name == district);
                if (distObj != null)
                {
                    var regObj = _db.MasterRegion.FirstOrDefault(r => r.Id == distObj.RegionId);
                    if (regObj != null)
                    {
                        parentRegion = regObj.Name;
                    }
                }
                else
                {
                    // Fallback to current region provider
                    parentRegion = _regionProvider.GetCurrentRegion();
                }

                bool isKashmir = parentRegion.ToUpper().Contains("KASHMIR");

                string remoteDirectory = "";
                string sftpBase = ftpSetting["sftpFilePath"]; // usually "/JKBSWD" or similar
                string targetRegionName = isKashmir ? "KASHMIR REGION" : "JAMMU REGION";

                if (responseType == "Disbursement")
                {
                    remoteDirectory = Helper.GetSftpPath(sftpBase, targetRegionName, Helper.SftpModule.Payment, Helper.SftpFolder.Response);
                }
                else
                {
                    remoteDirectory = Helper.GetSftpPath(sftpBase, targetRegionName, Helper.SftpModule.Validation, Helper.SftpFolder.Response);
                }

                string currentDirectoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "").Replace("\\bin", "");
                string privateKeyPath = Path.Combine(currentDirectoryPath + ftpSetting["sftpPrivateKeyPath"]);

                var methods = new List<AuthenticationMethod>();
                methods.Add(new PasswordAuthenticationMethod(username, password));
                if (File.Exists(privateKeyPath))
                {
                    var keyFile = new PrivateKeyFile(privateKeyPath);
                    methods.Add(new PrivateKeyAuthenticationMethod(username, keyFile));
                }

                ConnectionInfo connectionInfo = new ConnectionInfo(host, port, username, methods.ToArray());

                using (SftpClient sftpClient = new SftpClient(connectionInfo))
                {
                    sftpClient.Connect();
                    var files = sftpClient.ListDirectory(remoteDirectory)
                        .Where(f => !f.IsDirectory && (f.Name.EndsWith(".csv") || f.Name.EndsWith(".xlsx") || f.Name.EndsWith(".xls")))
                        .OrderByDescending(f => f.LastWriteTime)
                        .ToList();

                    // Filter files based on District metadata mapping
                    var filesToProcess = new List<Renci.SshNet.Sftp.SftpFile>();

                    foreach (var file in files)
                    {
                        var matchingUpload = GetMatchingUploadHistory(file.Name);
                        if (matchingUpload != null)
                        {
                            // If we have a matching upload record, filter by its DistrictId or District Name
                            if (!string.IsNullOrEmpty(district))
                            {
                                bool matchesDistrict = false;
                                if (matchingUpload.DistrictId.HasValue)
                                {
                                    var selDist = _db.MasterDistrict.FirstOrDefault(d => d.Name == district);
                                    if (selDist != null && matchingUpload.DistrictId.Value == selDist.Id)
                                    {
                                        matchesDistrict = true;
                                    }
                                }
                                if (!matchesDistrict && matchingUpload.Region != null && matchingUpload.Region.Trim().ToUpper() == district.Trim().ToUpper())
                                {
                                    matchesDistrict = true;
                                }

                                if (matchesDistrict)
                                {
                                    filesToProcess.Add(file);
                                }
                            }
                            else
                            {
                                filesToProcess.Add(file);
                            }
                        }
                        else
                        {
                            // Backward Compatibility Fallback: If no database mapping is found, fall back to filename-based prefix matching
                            if (!string.IsNullOrEmpty(district))
                            {
                                string districtPrefix = district.Trim().ToUpper() + "_";
                                if (file.Name.ToUpper().StartsWith(districtPrefix))
                                {
                                    filesToProcess.Add(file);
                                }
                            }
                            else
                            {
                                filesToProcess.Add(file);
                            }
                        }
                    }

                    if (!filesToProcess.Any()) return new SftpProcessResult { Success = false, Message = "No new response files found on SFTP." };

                    int successCount = 0;
                    foreach (var file in filesToProcess)
                    {
                        if (IsFileAlreadyProcessed(file.Name)) continue;

                        string localDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "SftpResponses");
                        if (!Directory.Exists(localDir)) Directory.CreateDirectory(localDir);
                        string fileNameWithTimestamp = $"{DateTime.Now:yyyyMMdd_HHmmss}_{file.Name}";
                        string localPath = Path.Combine(localDir, fileNameWithTimestamp);

                        using (var fileStream = File.Create(localPath))
                        {
                            sftpClient.DownloadFile(file.FullName, fileStream);
                        }
                        
                        FileInfo fileInfo = new FileInfo(localPath);
                        if (fileInfo.Length == 0)
                        {
                            // Empty file
                            File.Delete(localPath);
                            continue;
                        }

                        SftpProcessResult processResult;
                        if (responseType == "Disbursement")
                            processResult = ProcessDisbursementFile(localPath, file.Name, parentRegion, ftpSetting);
                        else
                            processResult = ProcessValidationFile(localPath, file.Name, parentRegion, ftpSetting);

                        var matchingUpload = GetMatchingUploadHistory(file.Name);
                        int? finalDistrictId = null;
                        string finalRegionName = null;

                        if (matchingUpload != null)
                        {
                            finalDistrictId = matchingUpload.DistrictId;
                            finalRegionName = matchingUpload.Region;
                        }
                        else if (!string.IsNullOrEmpty(district))
                        {
                            var selDist = _db.MasterDistrict.FirstOrDefault(d => d.Name == district);
                            finalDistrictId = selDist?.Id;
                            finalRegionName = district;
                        }

                        // Log history with metadata
                        var history = new SftpResponseHistory
                        {
                            FileName = file.Name,
                            FileType = responseType,
                            ProcessDate = DateTime.Now,
                             Status = processResult.Success ? "Success" : 
                                      (processResult.Message.Contains("required 16 columns") ? "Column Validation Failed" : 
                                      (processResult.Message.Contains("expected response format") ? "Header Validation Failed" : "Failed")),
                             Remarks = processResult.Message,
                            FilePath = localPath,
                            RecordCount = 0,
                            DistrictId = finalDistrictId,
                            Region = finalRegionName,
                            UploadHistoryId = matchingUpload?.Id
                        };

                        try {
                            DataTable dtCount = ConvertCsvToDataTable(localPath.Replace(".xlsx", ".csv").Replace(".xls", ".csv"));
                            history.RecordCount = dtCount.Rows.Count;
                        } catch { }

                        _db.SftpResponseHistory.Add(history);
                        _db.SaveChanges();

                        if (processResult.Success)
                        {
                            successCount++;
                            MoveToProcessed(sftpClient, remoteDirectory, file.Name, responseType, ftpSetting);
                        }
                    }

                    sftpClient.Disconnect();
                    return new SftpProcessResult { Success = successCount > 0 ? true : false, Message = $"Processed {successCount} file(s)." };
                }
            }
            catch (Exception ex)
            {
                Logger.Error("SFTP Processing Error", ex);
                return new SftpProcessResult { Success = false, Message = "Error: " + ex.Message };
            }
        }

        private bool IsFileAlreadyProcessed(string fileName)
        {
            string targetNameNoExt = Path.GetFileNameWithoutExtension(fileName).ToUpper();
            string prefix = targetNameNoExt.Length > 3 ? targetNameNoExt.Substring(0, 3) : targetNameNoExt;

            var successFiles = _db.SftpResponseHistory
                                  .Where(x => x.Status == "Success" && x.FileName.StartsWith(prefix))
                                  .Select(x => x.FileName)
                                  .ToList();

            return successFiles.Any(x => Path.GetFileNameWithoutExtension(x).ToUpper() == targetNameNoExt);
        }

        private List<string> ListFtpFiles(string host, string user, string pass, string dir)
        {
            List<string> files = new List<string>();
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(host + "/" + dir);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(user, pass);
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && (line.EndsWith(".csv") || line.EndsWith(".xlsx") || line.EndsWith(".xls")))
                            files.Add(line);
                    }
                }
            } catch { }
            return files;
        }

        private List<string> ListSftpFiles(string host, string user, string pass, string dir)
        {
            List<string> files = new List<string>();
            try
            {
                using (var client = new SftpClient(host, user, pass))
                {
                    client.Connect();
                    var list = client.ListDirectory(dir);
                    foreach (var file in list)
                    {
                        if (!file.IsDirectory && (file.Name.EndsWith(".csv") || file.Name.EndsWith(".xlsx") || file.Name.EndsWith(".xls")))
                            files.Add(file.Name);
                    }
                    client.Disconnect();
                }
            } catch { }
            return files;
        }

        private string DownloadFile(string host, string user, string pass, string dir, string file, bool isFtp)
        {
            string localDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "TempSftp");
            if (!Directory.Exists(localDir)) Directory.CreateDirectory(localDir);
            string localPath = Path.Combine(localDir, file);

            try
            {
                if (isFtp)
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Credentials = new NetworkCredential(user, pass);
                        client.DownloadFile(host + "/" + dir + "/" + file, localPath);
                    }
                }
                else
                {
                    using (var client = new SftpClient(host, user, pass))
                    {
                        client.Connect();
                        using (var fileStream = File.Create(localPath))
                        {
                            client.DownloadFile(dir + "/" + file, fileStream);
                        }
                        client.Disconnect();
                    }
                }
                return localPath;
            } catch { return null; }
        }

        private SftpProcessResult ProcessValidationFile(string localPath, string fileName, string region, Dictionary<string, string> ftpSetting)
        {
            try
            {
                string csvFileName = fileName.Replace(".xlsx", ".csv").Replace(".xls", ".csv");
                string csvFilePath = localPath.Replace(".xlsx", ".csv").Replace(".xls", ".csv");

                if (Path.GetExtension(localPath).ToLower() != ".csv")
                {
                    SaveExcelAsCsv(localPath, csvFilePath);
                }

                // Validation against headers (Enforced strictly at response processing stage)
                DataTable dtVal = ConvertCsvToDataTable(csvFilePath);
                string[] expectedHeaders = { 
                    "APPLICATION_REFERENCE_NO", 
                    "DISTRICT", 
                    "BENE_IFSC", 
                    "NAME_OF_APPLICANT", 
                    "ACCOUNTNO", 
                    "CATEGORY", 
                    "CBS_NAME", 
                    "BRANCH_CODE", 
                    "ACCOUNT_STATUS", 
                    "AADHAAR_STATUS", 
                    "ACCT_SCHEME_TYPE" 
                };
                
                bool headerValidationPassed = true;
                foreach (var h in expectedHeaders)
                {
                    if (!dtVal.Columns.Contains(h))
                    {
                        headerValidationPassed = false;
                        break;
                    }
                }

                if (!headerValidationPassed)
                {
                    string finalUserMessage = "Account Validation response file processing failed because the file structure does not match the expected response format. Required headers are missing or invalid. The file has been saved to history for review.";
                    Logger.Error($"Account Validation file validation failed for {fileName}. Required headers were missing or invalid.");
                    return new SftpProcessResult { Success = false, Message = finalUserMessage };
                }

                //foreach (DataRow row in dtVal.Rows)
                //{
                //    foreach (var h in expectedHeaders)
                //    {
                //        if (string.IsNullOrWhiteSpace(row[h]?.ToString()))
                //        {
                //            string finalUserMessage = "Account Validation response file processing failed because the file structure does not match the expected response format. Required headers are missing or invalid. The file has been saved to history for review.";
                //            Logger.Error($"Account Validation file data check failed: missing value in required column '{h}' at reference {row["APPLICATION_REFERENCE_NO"]}.");
                //            return new SftpProcessResult { Success = false, Message = finalUserMessage };
                //        }
                //    }
                //}
                var dirName = region == "KASHMIR REGION" ? "K_MasterEmployeeUploads" : "J_MasterEmployeeUploads";
                byte[] bytes = File.ReadAllBytes(csvFilePath);
                UploadToDatabaseServer(bytes, csvFileName, dirName, ftpSetting);

                string dbPath = ftpSetting["localFilePath"] + $"/DataFiles/{dirName}/" + csvFileName;
                
                DALBaseClass objDal = new DALBaseClassHelper().GetDAL();
                object[] spParams = new object[3];
                spParams[0] = dbPath;
                spParams[1] = 1; // Default User ID
                spParams[2] = fileName;
                objDal.ExecuteProcedure(ref spParams, "USP_ProcessValidationResponseFile");

                // Get counts for history
                DataTable dt = ConvertCsvToDataTable(csvFilePath);
                int total = dt.Rows.Count;
                int validated = 0; // In real logic we'd calculate this based on StatusValidator
                SaveDownloadMediaDetail(csvFileName, true, 0, total, validated, total - validated, "Processed via Centralized SFTP", (int)MediaType.Validation_res);

                return new SftpProcessResult { Success = true, Message = $"Processed validation file: {fileName}" };
            }
            catch (Exception ex)
            {
                return new SftpProcessResult { Success = false, Message = "Validation Error: " + ex.Message };
            }
        }

        private SftpProcessResult ProcessDisbursementFile(string localPath, string fileName, string region, Dictionary<string, string> ftpSetting)
        {
            try
            {
                string csvFileName = fileName.Replace(".xlsx", ".csv").Replace(".xls", ".csv");
                string csvFilePath = localPath.Replace(".xlsx", ".csv").Replace(".xls", ".csv");

                if (Path.GetExtension(localPath).ToLower() != ".csv")
                {
                    SaveExcelAsCsv(localPath, csvFilePath);
                }

                // Strictly validate Disbursement column structure (Expected: exactly 16 columns per row)
                int actualCols = 0;
                string validationError = "";
                if (!ValidateDisbursementColumnCount(csvFilePath, out actualCols, out validationError))
                {
                    string userErrorMessage = "File validation failed. The uploaded disbursement response file does not contain the required 16 columns and cannot be processed. The file has been saved to history for review and can be downloaded for correction.";
                    return new SftpProcessResult
                    {
                        Success = false,
                        Message = userErrorMessage
                    };
                }

                var dirName = region == "KASHMIR REGION" ? "K_BankMediaFileUpload" : "J_BankMediaFileUpload";
                byte[] bytes = File.ReadAllBytes(csvFilePath);
                UploadToDatabaseServer(bytes, csvFileName, dirName, ftpSetting);

                string dbPath = ftpSetting["localFilePath"] + $"/DataFiles/{dirName}/" + csvFileName;
                
                DALBaseClass objDal = new DALBaseClassHelper().GetDAL();
                object[] spParams = new object[3];
                spParams[0] = dbPath;
                spParams[1] = 1; // Default User ID
                spParams[2] = fileName;
                objDal.ExecuteProcedure(ref spParams, "USP_ProcessDisbursementResponseFile");

                // Legacy status updates
                try {
                    var objPay = new DVOPayrollProcess_PayEmployee();
                    JKPS.BLL.BLLPayrollAutopayNew.updatePostedstatus(ref objPay);
                    var activeBatch = JKPS.BLL.BLLPYBatchProcessStybatchr.GetActiveBatch();
                    if (activeBatch != null && activeBatch.Count > 0)
                    {
                        var batch = activeBatch[0];
                        batch.updateby = 1;
                        batch.insertmachineinfo = Environment.MachineName;
                        JKPS.BLL.BLLPYBatchProcessStybatchr.UPDATEBatchProcessStatus(ref batch);
                    }
                } catch { }

                string period = GetPeriodFromBatch(fileName);
                SaveDownloadMediaDetail(fileName, true, 0, 0, 0, 0, "", (int)MediaType.RetTxnReport, region, period);

                return new SftpProcessResult { Success = true, Message = $"Processed disbursement file: {fileName}" };
            }
            catch (Exception ex)
            {
                return new SftpProcessResult { Success = false, Message = "Disbursement Error: " + ex.Message };
            }
        }

        private void MoveToProcessed(SftpClient client, string remoteDir, string fileName, string type, Dictionary<string, string> ftpSetting)
        {
            try
            {
                string inboxProcessed = remoteDir + "Processed/";
                if (!client.Exists(inboxProcessed)) client.CreateDirectory(inboxProcessed);
                client.RenameFile(remoteDir + fileName, inboxProcessed + fileName);

                bool isKashmir = remoteDir.ToUpper().Contains("KASHMIR");
                string sftpBase = ftpSetting["sftpFilePath"];
                string targetRegionName = isKashmir ? "KASHMIR REGION" : "JAMMU REGION";

                string outboxSource = "";
                if (type == "Validation")
                {
                    outboxSource = Helper.GetSftpPath(sftpBase, targetRegionName, Helper.SftpModule.Validation, Helper.SftpFolder.Request);
                }
                else if (type == "Disbursement")
                {
                    outboxSource = Helper.GetSftpPath(sftpBase, targetRegionName, Helper.SftpModule.Payment, Helper.SftpFolder.Request);
                }

                if (!string.IsNullOrEmpty(outboxSource) && client.Exists(outboxSource))
                {
                    var sentFile = GetSentFileName(fileName);
                    if (!string.IsNullOrEmpty(sentFile) && client.Exists(outboxSource + sentFile))
                    {
                        string outboxProcessed = outboxSource + "Processed/";
                        if (!client.Exists(outboxProcessed)) client.CreateDirectory(outboxProcessed);
                        client.RenameFile(outboxSource + sentFile, outboxProcessed + fileName);
                    }
                }
            } catch { }
        }

        private void UploadToDatabaseServer(byte[] contents, string fileName, string subDir, Dictionary<string, string> ftpSetting)
        {
            string url = ftpSetting["ftpServerUrl"] + "/DataFiles/" + subDir + "/" + fileName;
            
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            int maxRetries = 3;
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);
                    request.KeepAlive = false;
                    request.UseBinary = true;
                    request.UsePassive = (i % 2 == 0); // Try Passive, then Active on retry

                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(contents, 0, contents.Length);
                    }
                    using (var resp = (FtpWebResponse)request.GetResponse()) { }
                    
                    break; // Success
                }
                catch (Exception ex)
                {
                    if (i == maxRetries - 1)
                    {
                        Logger.Error($"Failed to upload to DB server via FTP after {maxRetries} attempts: {url}", ex);
                        throw;
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        private void SaveExcelAsCsv(string excelPath, string csvPath)
        {
            string ext = Path.GetExtension(excelPath).ToLower();
            if (ext == ".xls")
            {
                SaveXlsAsCsv(excelPath, csvPath);
            }
            else
            {
                using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(excelPath)))
                {
                    var worksheet = package.Workbook.Worksheets[1];
                    var csvBuilder = new StringBuilder();
                    if (worksheet.Dimension != null)
                    {
                        for (int r = 1; r <= worksheet.Dimension.End.Row; r++)
                        {
                            var values = new List<string>();
                            for (int c = 1; c <= worksheet.Dimension.End.Column; c++)
                            {
                                string val = worksheet.Cells[r, c].Value?.ToString() ?? "";
                                if (val.Contains(",") || val.Contains("\"") || val.Contains("\n"))
                                    val = $"\"{val.Replace("\"", "\"\"")}\"";
                                values.Add(val);
                            }
                            csvBuilder.AppendLine(string.Join(",", values));
                        }
                    }
                    File.WriteAllText(csvPath, csvBuilder.ToString(), Encoding.UTF8);
                }
            }
        }

        private void SaveXlsAsCsv(string excelPath, string csvPath)
        {
            try
            {
                using (var stream = File.Open(excelPath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataReader.ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataReader.ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = false
                            }
                        });

                        if (result.Tables.Count > 0)
                        {
                            var dt = result.Tables[0];
                            WriteDataTableToCsv(dt, csvPath);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error converting XLS to CSV using ExcelDataReader", ex);
                throw new Exception("Error reading XLS file: " + ex.Message, ex);
            }
        }

        private void WriteDataTableToCsv(DataTable dt, string csvPath)
        {
            var csvBuilder = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                var values = new List<string>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string val = row[i]?.ToString() ?? "";
                    if (val.Contains(",") || val.Contains("\"") || val.Contains("\n"))
                        val = $"\"{val.Replace("\"", "\"\"")}\"";
                    values.Add(val);
                }
                csvBuilder.AppendLine(string.Join(",", values));
            }
            File.WriteAllText(csvPath, csvBuilder.ToString(), Encoding.UTF8);
        }

        private void SaveDownloadMediaDetail(string fileName, bool isProcessed, int recordId, int total, int validated, int notValidated, string remark, int mediaType, string region = "", string period = "")
        {
            try {
                DALBaseClass objDal = new DALBaseClassHelper().GetDAL();
                object[] parameters = new object[18];
                parameters[0] = fileName;
                parameters[1] = true; // HasDownloaded
                parameters[2] = isProcessed;
                parameters[3] = Environment.MachineName;
                parameters[4] = 1; // UserId
                parameters[5] = DateTime.Now;
                parameters[6] = true; // IsActive
                parameters[7] = mediaType;
                parameters[8] = Environment.MachineName;
                parameters[9] = 1;
                parameters[10] = DateTime.Now;
                parameters[11] = recordId;
                parameters[12] = total;
                parameters[13] = validated;
                parameters[14] = notValidated;
                parameters[15] = remark;
                parameters[16] = region;
                parameters[17] = period;
                objDal.ExecuteProcedure(ref parameters, "SaveDownloadMediaDetail");
            } catch { }
        }

        private string GetSentFileName(string fileName)
        {
            try {
                var matchingUpload = GetMatchingUploadHistory(fileName);
                if (matchingUpload != null)
                {
                    if (!string.IsNullOrEmpty(matchingUpload.FileName))
                    {
                        return matchingUpload.FileName;
                    }
                    if (!string.IsNullOrEmpty(matchingUpload.FilePath))
                    {
                        return Path.GetFileName(matchingUpload.FilePath);
                    }
                }

                // Fallback to old behavior
                int index = fileName.IndexOf('_', fileName.IndexOf('_') + 1);
                if (index > 0)
                {
                    string prefix = fileName.Substring(0, index);
                    DALBaseClass objDal = new DALBaseClassHelper().GetDAL();
                    var ds = objDal.GetData("SELECT TOP 1 FilePath FROM [dbo].[Media_Queue] WHERE FilePath LIKE '%" + prefix + "%' ORDER BY CreatedOn DESC");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        return Path.GetFileName(ds.Tables[0].Rows[0]["FilePath"].ToString());
                }
            } catch { }
            return "";
        }

        private string GetPeriodFromBatch(string fileName)
        {
            try {
                if (fileName.Contains("_")) {
                    var parts = fileName.Split('_');
                    if (parts.Length > 1) {
                        int batchId;
                        if (int.TryParse(parts[1], out batchId)) {
                             DALBaseClass objDal = new DALBaseClassHelper().GetDAL();
                             var ds = objDal.GetData("SELECT Period FROM PY_BatchProcessStybatchr WHERE pybatchid = " + batchId);
                             if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                 return ds.Tables[0].Rows[0]["Period"].ToString();
                        }
                    }
                }
            } catch { }
            return "";
        }
        private DataTable ConvertCsvToDataTable(string filePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(filePath))
            {
                string[] headers = null;
                var csvSplitPattern = new System.Text.RegularExpressions.Regex(",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    
                    string[] fields = csvSplitPattern.Split(line).Select(s => s.Trim().Trim('"').Replace("\"\"", "\"")).ToArray();
                    if (fields.All(string.IsNullOrWhiteSpace)) continue;
                    
                    int nonEmptyCount = fields.Count(f => !string.IsNullOrWhiteSpace(f));
                    if (nonEmptyCount <= 1) continue; // skip title row
                    
                    headers = fields;
                    foreach (string header in headers)
                    {
                        string colName = header;
                        int copyIdx = 1;
                        while(dt.Columns.Contains(colName)) { colName = header + "_" + copyIdx++; }
                        dt.Columns.Add(colName);
                    }
                    break;
                }

                if (headers == null) return dt;

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    
                    string[] fields = csvSplitPattern.Split(line).Select(s => s.Trim().Trim('"').Replace("\"\"", "\"")).ToArray();
                    if (fields.All(string.IsNullOrWhiteSpace)) continue;
                    
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < Math.Min(headers.Length, fields.Length); i++)
                    {
                        dr[i] = fields[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        private bool ValidateDisbursementColumnCount(string csvFilePath, out int columnCount, out string errorMessage)
        {
            columnCount = 0;
            errorMessage = "";
            try
            {
                if (!File.Exists(csvFilePath))
                {
                    errorMessage = "File does not exist.";
                    return false;
                }

                using (var sr = new StreamReader(csvFilePath))
                {
                    int lineNumber = 0;
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        lineNumber++;
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] columns = line.Split(',');
                        columnCount = columns.Length;

                        if (columnCount != 16)
                        {
                            errorMessage = $"Disbursement file validation failed. Expected 16 columns but found {columnCount} columns at line {lineNumber}.";
                            Logger.Error(errorMessage);
                            return false;
                        }
                    }
                    
                    if (lineNumber == 0)
                    {
                        errorMessage = "Disbursement file is empty.";
                        Logger.Error(errorMessage);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Error validating column count: " + ex.Message;
                Logger.Error(errorMessage, ex);
                return false;
            }
        }
    }
}
