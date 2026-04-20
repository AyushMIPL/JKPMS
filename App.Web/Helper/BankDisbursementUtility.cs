

using App.Data;
using App.Data.Entities;
using JKPS.BLL;
using JKPS.COMMON;
using JKPS.DL;
using log4net;
using Quartz;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;
using static App.Web.Helper.Helper;

namespace App.Web.Helper
{
    public class BankDisbursementUtility : IJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BankDisbursementUtility));
        private CurrentRegionProvider currentRegionProvider = new CurrentRegionProvider();

        public void UpdateDisbursementResponse()
        {
            Execute(null);
        }
        public Task Execute(IJobExecutionContext context)
        {
            try
            {

                Logger.Info("BankDisbursementUtility START");

                Dictionary<string, string> ftpSetting = Helper.GetFTPSetting();


                //var regions = GetRegionName();
                var regions = currentRegionProvider.GetCurrentRegion();
                var directoryName = regions == "KASHMIR REGION" ? "K_BankMediaFile" : "J_BankMediaFile";

                //string localDirectory = ftpSetting["localFilePath"] + $"\\DataFiles\\{directoryName}";
                string localDirectory = Helper.ExecutePathLocal(regions, directoryName);


                //string localDirectory = ftpSetting["localFilePath"] + "\\DataFiles\\BankMediaFile";

                bool IsFTP = Convert.ToBoolean(ftpSetting["IsFTP"]);

                if (IsFTP)
                {
                    string ftpHost = ftpSetting["sftpServerUrl"];
                    string ftpUsername = ftpSetting["sftpUsername"];
                    string ftpPassword = ftpSetting["sftpPassword"];
                    //string remoteDirectory = ftpSetting["ftpFilePath"] + "/Inbox";

                    //var region = GetRegionName();
                    var region = currentRegionProvider.GetCurrentRegion();
                    var directoryNames = region == "KASHMIR REGION" ? "K_Disbursement" : "J_Disbursement";

                    //string remoteDirectory = ftpSetting["sftpFilePath"] + $"/{directoryNames}/Inbox";
                    string remoteDirectory = Helper.BankDisbursementPathServer(regions, directoryNames);


                    //string remoteDirectory = ftpSetting["sftpFilePath"] + "/Disbursement/Inbox";


                    // Create the FTP request to list directory
                    FtpWebRequest listRequest = (FtpWebRequest)WebRequest.Create(ftpHost + "/" + remoteDirectory);
                    listRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                    listRequest.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                    dynamic latestFile = null;
                    try
                    {
                        using (FtpWebResponse listResponse = (FtpWebResponse)listRequest.GetResponse())
                        {
                            using (Stream listStream = listResponse.GetResponseStream())
                            {
                                using (StreamReader listReader = new StreamReader(listStream))
                                {
                                    // Read the list of files with details
                                    List<string> files = new List<string>();
                                    // Read the list of files with details
                                    string line;

                                    while ((line = listReader.ReadLine()) != null)
                                    {
                                        if (!string.IsNullOrWhiteSpace(line))
                                        {
                                            files.Add(line);
                                        }
                                    }

                                    // Extract file details and find the latest file based on modification time
                                    if (files != null)
                                    {
                                        latestFile = files
                                            .Where(f => !f.EndsWith("/") && !string.IsNullOrEmpty(f))
                                            .Select(f => new
                                            {
                                                //Name = f.Substring(f.LastIndexOf(' ') + 1),
                                                //LastModified = DateTime.ParseExact((f.Substring(f.IndexOf(":") - 9, 12)), "MMM dd HH:mm", null)

                                                Name = f.Substring(f.LastIndexOf(' ') + 1),
                                                LastModified = f.Contains(":")
                                                ? DateTime.ParseExact((f.Substring(f.IndexOf(":") - 9, 12)), "MMM dd HH:mm", null)
                                                : DateTime.MinValue
                                            })
                                            .OrderByDescending(f => f.LastModified)
                                            .FirstOrDefault();
                                    }
                                }
                            }
                        }
                        if (latestFile != null)
                        {
                            string csvFileName = latestFile.Name.Replace(".xlsx", ".csv").Replace(".xls", ".csv");

                            bool isProcessed = FileIsProcessed(latestFile.Name);
                            if (!isProcessed)
                            {
                                string path = ftpHost + ("/" + remoteDirectory + "/" + latestFile.Name);
                                // Create the FTP request to download the latest file
                                FtpWebRequest downloadRequest = (FtpWebRequest)WebRequest.Create(path);
                                downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                                downloadRequest.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

                                //To get the location the assembly normally resides on disk or the install directory
                                string currentDirectoryPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

                                //var regiones = GetRegionName();
                                var regiones = currentRegionProvider.GetCurrentRegion();
                                var directoryNamees = regiones == "KASHMIR REGION" ? "K_Disbursement" : "J_Disbursement";
                                string serverMapPath = Path.Combine(Path.GetDirectoryName(currentDirectoryPath).Replace("file:\\", "").Replace("\\bin", ""), $"\\DataFile\\BankMediaFile\\{directoryNames}");


                                //string serverMapPath = Path.GetDirectoryName(currentDirectoryPath).Replace("file:\\", "").Replace("\\bin", "") + "\\DataFile\\BankMediaFile\\JK_Disbursement";


                                //string serverMapPath = Path.Combine(Path.GetDirectoryName(currentDirectoryPath).Replace("file:\\", "").Replace("\\bin", ""), "\\DataFile\\BankMediaFile\\JK_Disbursement");
                                if (!Directory.Exists(serverMapPath))
                                {
                                    // Attempt to create the directory
                                    Directory.CreateDirectory(serverMapPath);
                                }
                                string filePath = Path.Combine(serverMapPath, latestFile.Name);

                                // delete file in safe way
                                FileHelper fileHelper = new FileHelper();
                                bool isFileDeleted = fileHelper.TryDeleteFile(filePath);
                                bool isCsvFileDeleted = fileHelper.TryDeleteFile(filePath.Replace(".xlsx", ".csv").Replace(".xls", ".csv"));

                                // Get the response and download the file
                                long maxFileSize = 20 * 1024 * 1024; // 20 MB limit
                                long totalBytesRead = 0;
                                int bufferSize = 32768; // 32 KB buffer
                                byte[] buffer = new byte[bufferSize];
                                downloadRequest.Timeout = 300000; // 5 minutes timeout for connection
                                downloadRequest.ReadWriteTimeout = 300000; // 5 minutes read/write timeout

                                using (FtpWebResponse downloadResponse = (FtpWebResponse)downloadRequest.GetResponse())
                                {
                                    using (Stream downloadStream = downloadResponse.GetResponseStream())
                                    {
                                        using (FileStream fileStream = File.Create(filePath))
                                        {
                                            int bytesRead;
                                            while ((bytesRead = downloadStream.Read(buffer, 0, buffer.Length)) > 0)
                                            {
                                                fileStream.Write(buffer, 0, bytesRead);
                                            }
                                        }
                                    }
                                }

                                UploadDownloadedFileOnDatabaseServer(filePath);

                                //var regionDis = GetRegionName();
                                var regionDis = currentRegionProvider.GetCurrentRegion();
                                var directoryNamDis = regionDis == "KASHMIR REGION" ? "K_BankMediaFileUpload" : "J_BankMediaFileUpload";

                                string databaseFilePath = ftpSetting["localFilePath"] + $"/DataFiles/{directoryNamDis}/" + csvFileName;

                                //string databaseFilePath = ftpSetting["localFilePath"] + "/DataFiles/BankMediaFileUpload/" + csvFileName;

                                UpdateBankMediaExcelDatabase(databaseFilePath);
                                SaveDownloadMediaDetail(latestFile.Name, true);
                                UpdatePostedStatus();
                                CloseBatchProcessAjax();
                                SaveArchiveFile(latestFile.Name, new NetworkCredential(ftpUsername, ftpPassword), ftpHost);
                            }
                        }
                        else
                        {
                        }
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    string host = ftpSetting["sftpServerUrl"];
                    int port = Convert.ToInt32(ftpSetting["sftpPort"]); //SFTP default port is 22
                    string username = ftpSetting["sftpUsername"];
                    string password = ftpSetting["sftpPassword"];
                    string remoteDirectory = ftpSetting["sftpFilePath"] + "/PaymentFiles/Inbox/";

                    string inboxDestinationDir = ftpSetting["sftpFilePath"] + "/PaymentFiles/Inbox/Processed/";
                    string outboxSourceDir = ftpSetting["sftpFilePath"] + "/PaymentFiles/Outbox/";
                    string outboxDestinationDir = ftpSetting["sftpFilePath"] + "/PaymentFiles/Outbox/Processed/";
                    string currentDirectoryPath1 = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "").Replace("\\bin", "");
                    string localfilepathSFTP = Path.Combine(currentDirectoryPath1 + ftpSetting["sftpPrivateKeyPath"]);
                    var keyFile = new PrivateKeyFile(localfilepathSFTP);
                    var keyFiles = new[] { keyFile };
                    var methods = new List<AuthenticationMethod>
            {
                new PasswordAuthenticationMethod(username, password),
                new PrivateKeyAuthenticationMethod(username, keyFiles)
            };

                    // Create a new connection info with public key authentication
                    ConnectionInfo connectionInfo = new ConnectionInfo(host, port, username, methods.ToArray());

                    // Create an SftpClient using the connection info
                    using (SftpClient sftpClient = new SftpClient(connectionInfo))
                    {
                        // Connect to the SFTP server
                        sftpClient.Connect();

                        var files = sftpClient.ListDirectory(remoteDirectory);

                        // Filter out directories and find the latest file
                        var latestFile = files
                            .Where(f => !f.IsDirectory)
                            .OrderByDescending(f => f.LastWriteTime)
                            .FirstOrDefault();

                        if (latestFile != null)
                        {
                            string csvFileName = latestFile.Name.Replace(".xlsx", ".csv").Replace(".xls", ".csv");

                            bool isProcessed = FileIsProcessed(csvFileName);
                            if (!isProcessed)
                            {
                                //To get the location the assembly normally resides on disk or the install directory
                                string currentDirectoryPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

                                //var region = GetRegionName();
                                var region = currentRegionProvider.GetCurrentRegion();
                                var directoryNames = region == "KASHMIR REGION" ? "K_MasterEmployeeDownloads" : "J_MasterEmployeeDownloads";
                                string serverMapPath = Path.GetDirectoryName(currentDirectoryPath).Replace("file:\\", "").Replace("\\bin", "") + $"\\DataFile\\{directoryNames}";


                                //string serverMapPath = Path.GetDirectoryName(currentDirectoryPath).Replace("file:\\", "").Replace("\\bin", "") + "\\DataFile\\MasterEmployeeDownloads";

                                if (!Directory.Exists(serverMapPath))
                                {
                                    // Attempt to create the directory
                                    Directory.CreateDirectory(serverMapPath);
                                }
                                string filePath = Path.Combine(serverMapPath, latestFile.Name);

                                // delete file in safe way
                                FileHelper fileHelper = new FileHelper();
                                bool isFileDeleted = fileHelper.TryDeleteFile(filePath);
                                bool isCsvFileDeleted = fileHelper.TryDeleteFile(filePath.Replace(".xlsx", ".csv").Replace(".xls", ".csv"));

                                // Download the latest file
                                using (var fileStream = File.Create(filePath))
                                {
                                    sftpClient.DownloadFile(latestFile.FullName, fileStream);
                                }

                                UploadDownloadedFileOnDatabaseServer(filePath);
                                //var regionDiss = GetRegionName();
                                var regionDiss = currentRegionProvider.GetCurrentRegion();
                                var directoryNameDiss = regionDiss == "KASHMIR REGION" ? "K_BankMediaFileUpload" : "J_BankMediaFileUpload";
                                //string databaseFilePath = ftpSetting["ftpServerUrl"] + $"/DataFiles/{directoryNameDiss}/" + csvFileName;

                                string databaseFilePath = ftpSetting["localFilePath"] + $"\\DataFiles\\{directoryNameDiss}\\" + csvFileName;
                                //string databaseFilePath = ftpSetting["localFilePath"] + "/DataFiles/BankMediaFileUpload/" + csvFileName;
                                try
                                {
                                    UpdateBankMediaExcelDatabase(databaseFilePath);
                                }
                                catch (Exception ex)
                                {

                                   
                                }
                                SaveDownloadMediaDetail(latestFile.Name, true);
                                UpdatePostedStatus();
                                CloseBatchProcessAjax();

                                // Check if the destination file exists
                                if (sftpClient.Exists(Path.Combine(inboxDestinationDir, latestFile.Name)))
                                {
                                    // Optionally, delete the existing file or handle the overwrite behavior
                                    sftpClient.DeleteFile(Path.Combine(inboxDestinationDir, latestFile.Name));
                                }
                                // Check if the destination file exists
                                if (sftpClient.Exists(Path.Combine(outboxDestinationDir, latestFile.Name)))
                                {
                                    // Optionally, delete the existing file or handle the overwrite behavior
                                    sftpClient.DeleteFile(Path.Combine(outboxDestinationDir, latestFile.Name));
                                }
                                // Check if the destination file exists
                                if (sftpClient.Exists(Path.Combine(remoteDirectory, latestFile.Name)))
                                {
                                    sftpClient.RenameFile(Path.Combine(remoteDirectory, latestFile.Name), Path.Combine(inboxDestinationDir, latestFile.Name));
                                }
                                // Check if the destination file exists
                                if (sftpClient.Exists(Path.Combine(outboxSourceDir, latestFile.Name)))
                                {
                                    sftpClient.RenameFile(Path.Combine(outboxSourceDir, latestFile.Name), Path.Combine(outboxDestinationDir, latestFile.Name));
                                }
                            }
                        }


                        // Disconnect from the SFTP server
                        sftpClient.Disconnect();
                    }
                }


                var task = Task.Run(() => true);
                return task;
            }
            catch (Exception ex)
            {
                //throw;
                return null;
            }
        }

        public void SaveArchiveFile(string FileName, NetworkCredential Credential, string ftpHost)
        {
            Dictionary<string, string> ftpSetting = Helper.GetFTPSetting();
            //var region = GetRegionName();
            var region = currentRegionProvider.GetCurrentRegion();
            var directoryNames = region == "KASHMIR REGION" ? "K_Archive" : "J_Archive";
            string DestinationFolderPath = ftpHost + ftpSetting["sftpFilePath"] + $"/{directoryNames}/";


            //var regionDis = GetRegionName();
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
        private void UploadDownloadedFileOnDatabaseServer(string filePath)
        {
            try
            {
                string csvFilePath = filePath.Replace(".xlsx", ".csv").Replace(".xls", ".csv");
                if (Path.GetExtension(filePath) == ".xls" || Path.GetExtension(filePath) == ".xlsx")
                {
                    SaveExcelAsCsv(filePath, csvFilePath);
                }
                byte[] bytes = System.IO.File.ReadAllBytes(csvFilePath);
                string failedMessage = UploadDataFile(bytes, Path.GetFileName(csvFilePath));
                // Convert csv file to data table to get counts
                //A/DataTable dataTable = ConvertCsvToDataTable(csvFilePath);
                DataTable dataTable = ConvertCsvToDataTableWithOutHeaders(csvFilePath);

                int benif_Count = dataTable.Rows.Count;

                //A/int Validated_count = dataTable.AsEnumerable().Count(row => row.Field<string>("Status") == "OK");
                //A/int Notvalidated_count = dataTable.AsEnumerable().Count(row => row.Field<string>("Status") != "OK");
                
                // Enhanced validation logic using StatusValidator for synonyms and normalization
                // Check multiple potential status columns (11, 12, 13, 14, 15) to be robust
                int[] potentialStatusIndices = { 11, 12, 13, 14, 15 };
                
                int Validated_count = dataTable.AsEnumerable()
                    .Count(row =>
                    {
                        foreach (int idx in potentialStatusIndices)
                        {
                            if (idx < row.Table.Columns.Count)
                            {
                                var status = row[idx]?.ToString();
                                if (StatusValidator.IsValidSuccess(status))
                                {
                                    return true;
                                }
                            }
                        }
                        return false;
                    });

                int Notvalidated_count = dataTable.Rows.Count - Validated_count;
                
                Logger.Info($"File: {Path.GetFileName(filePath)} | Total: {benif_Count} | Validated: {Validated_count} | NotValidated: {Notvalidated_count}");

                SaveDownloadMediaDetail(Path.GetFileName(filePath), false, benif_Count, Validated_count, Notvalidated_count, "");

            }
            catch (Exception ex)
            {

                throw;
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
                        string[] headers = sr.ReadLine().Split(',').Where((item, index) => index != 0).ToArray();
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
                            string[] rows = sr.ReadLine().Split(',').Where((item, index) => index != 0).ToArray();
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
            catch (Exception)
            {
                throw;
            }
            return dataTable;

        }

        static DataTable ConvertCsvToDataTableWithOutHeaders(string fileName)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (var sr = new StreamReader(fileName))
                {
                    bool columnsCreated = false;

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        // Try to detect delimiter (Comma or Tab)
                        char delimiter = ',';
                        if (line.Contains("\t") && line.Split('\t').Length > line.Split(',').Length)
                        {
                            delimiter = '\t';
                        }

                        // Split and skip first column (if it's a legacy requirement/row index)
                        string[] values = line.Split(delimiter)
                                               .Where((item, index) => index != 0)
                                               .ToArray();

                        // Create columns dynamically from first data row
                        if (!columnsCreated)
                        {
                            for (int i = 0; i < values.Length; i++)
                            {
                                dataTable.Columns.Add($"Column{i + 1}");
                            }
                            columnsCreated = true;
                        }

                        if (values.Length > 0)
                        {
                            DataRow row = dataTable.NewRow();
                            for (int i = 0; i < Math.Min(values.Length, dataTable.Columns.Count); i++)
                            {
                                row[i] = values[i];
                            }
                            dataTable.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error converting CSV to DataTable", ex);
            }

            return dataTable;
        }
        static void SaveExcelAsCsv(string excelFilePath, string csvFilePath)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(excelFilePath);
                wb.SaveAs(csvFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlCSVWindows);
                wb.Close(false);
                app.Quit();

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