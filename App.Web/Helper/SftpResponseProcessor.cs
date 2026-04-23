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
        }

        public SftpProcessResult FetchAndProcessResponses(string responseType)
        {
            try
            {
                var ftpSetting = Helper.GetFTPSetting();
                string host = ftpSetting["sftpServerUrl"];
                int port = Convert.ToInt32(ftpSetting["sftpPort"]);
                string username = ftpSetting["sftpUsername"];
                string password = ftpSetting["sftpPassword"];
                string region = _regionProvider.GetCurrentRegion();

                string remoteDirectory = "";
                if (responseType == "Disbursement")
                {
                    remoteDirectory = ftpSetting["sftpFilePath"] + "/PaymentFiles/Inbox/";
                }
                else
                {
                    remoteDirectory = ftpSetting["sftpFilePath"] + "/AccountValidation/Inbox/";
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

                    if (!files.Any()) return new SftpProcessResult { Success = false, Message = "No new response files found on SFTP." };

                    int successCount = 0;
                    foreach (var file in files)
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

                        SftpProcessResult processResult;
                        if (responseType == "Disbursement")
                            processResult = ProcessDisbursementFile(localPath, file.Name, region, ftpSetting);
                        else
                            processResult = ProcessValidationFile(localPath, file.Name, region, ftpSetting);

                        // Log history
                        var history = new SftpResponseHistory
                        {
                            FileName = file.Name,
                            FileType = responseType,
                            ProcessDate = DateTime.Now,
                            Status = processResult.Success ? "Success" : "Failed",
                            Remarks = processResult.Message,
                            FilePath = localPath
                        };
                        _db.SftpResponseHistory.Add(history);
                        _db.SaveChanges();

                        if (processResult.Success)
                        {
                            successCount++;
                            MoveToProcessed(sftpClient, remoteDirectory, file.Name, responseType, ftpSetting);
                        }

                        // We don't delete localPath here anymore because we want it available for download
                    }

                    sftpClient.Disconnect();
                    return new SftpProcessResult { Success = true, Message = $"Processed {successCount} file(s)." };
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
            return _db.SftpResponseHistory.Any(x => x.FileName == fileName && x.Status == "Success");
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

                byte[] bytes = File.ReadAllBytes(csvFilePath);
                UploadToDatabaseServer(bytes, csvFileName, "MasterEmployeeUploads", ftpSetting);

                string dbPath = ftpSetting["localFilePath"] + "/DataFiles/MasterEmployeeUploads/" + csvFileName;
                
                DALBaseClass objDal = new DALBaseClassHelper().GetDAL();
                object[] spParams = new object[2];
                spParams[0] = dbPath;
                spParams[1] = 1; // Default User ID
                objDal.ExecuteProcedure(ref spParams, "UpdateEmpMasterEmpBankDetails");

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

                var dirName = region == "KASHMIR REGION" ? "K_BankMediaFileUpload" : "J_BankMediaFileUpload";
                byte[] bytes = File.ReadAllBytes(csvFilePath);
                UploadToDatabaseServer(bytes, csvFileName, dirName, ftpSetting);

                string dbPath = ftpSetting["localFilePath"] + $"/DataFiles/{dirName}/" + csvFileName;
                
                DALBaseClass objDal = new DALBaseClassHelper().GetDAL();
                object[] spParams = new object[2];
                spParams[0] = dbPath;
                spParams[1] = 1; // Default User ID
                objDal.ExecuteProcedure(ref spParams, "UpdateBankMediaExcelDatabase");

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

                // For validation, also handle outbox renaming if exists
                if (type == "Validation")
                {
                    string outboxSource = ftpSetting["sftpFilePath"] + "/AccountValidation/Outbox/";
                    string outboxProcessed = outboxSource + "Processed/";
                    if (client.Exists(outboxSource))
                    {
                        var sentFile = GetSentFileName(fileName);
                        if (!string.IsNullOrEmpty(sentFile) && client.Exists(outboxSource + sentFile))
                        {
                            if (!client.Exists(outboxProcessed)) client.CreateDirectory(outboxProcessed);
                            client.RenameFile(outboxSource + sentFile, outboxProcessed + fileName);
                        }
                    }
                }
            } catch { }
        }

        private void UploadToDatabaseServer(byte[] contents, string fileName, string subDir, Dictionary<string, string> ftpSetting)
        {
            string url = ftpSetting["ftpServerUrl"] + "/DataFiles/" + subDir + "/" + fileName;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpSetting["ftpUsername"], ftpSetting["ftpPassword"]);
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(contents, 0, contents.Length);
            }
            using (var resp = (FtpWebResponse)request.GetResponse()) { }
        }

        private void SaveExcelAsCsv(string excelPath, string csvPath)
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
                int index = fileName.IndexOf('_', fileName.IndexOf('_') + 1);
                string prefix = fileName.Substring(0, index);
                DALBaseClass objDal = new DALBaseClassHelper().GetDAL();
                var ds = objDal.GetData("SELECT TOP 1 FilePath FROM [dbo].[Media_Queue] WHERE FilePath LIKE '%" + prefix + "%' ORDER BY CreatedOn DESC");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return Path.GetFileName(ds.Tables[0].Rows[0]["FilePath"].ToString());
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
                string line = sr.ReadLine();
                if (string.IsNullOrEmpty(line)) return dt;
                string[] headers = line.Split(',');
                foreach (string header in headers) dt.Columns.Add(header.Trim());
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < Math.Min(headers.Length, rows.Length); i++) dr[i] = rows[i].Trim();
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}
