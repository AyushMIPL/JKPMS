using App.Data;
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
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Text;


namespace App.Web.Helper
{
  public class BankDisbursementUtilityJobScheduler : IJob
  {
    private static readonly ILog Logger = LogManager.GetLogger(typeof(BankDisbursementUtilityJobScheduler));
    private CurrentRegionProvider currentRegionProvider = new CurrentRegionProvider();
    private readonly string[] AllRegions = { "JAMMU REGION", "KASHMIR REGION" };

    public Task Execute(IJobExecutionContext context)
    {
      try
      {
        // loop through both regions
        foreach (var region in AllRegions)
        {

          Logger.Info("BankDisbursementUtility START");

          Dictionary<string, string> ftpSetting = Helper.GetFTPSetting();

          var directoryName = region == "KASHMIR REGION" ? "K_BankMediaFile" : "J_BankMediaFile";

          string localDirectory = Helper.ExecutePathLocal(region, directoryName);

          bool IsFTP = Convert.ToBoolean(ftpSetting["IsFTP"]);

          if (IsFTP)
          {
            string ftpHost = ftpSetting["sftpServerUrl"];
            string ftpUsername = ftpSetting["sftpUsername"];
            string ftpPassword = ftpSetting["sftpPassword"];

            var directoryNames = region == "KASHMIR REGION" ? "K_Disbursement" : "J_Disbursement";

            string remoteDirectory = Helper.BankDisbursementPathServer(region, directoryNames);

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
                    var files = listReader.ReadToEnd().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    // Extract file details and find the latest file based on modification time
                    if (files != null)
                    {
                      latestFile = files
                          .Where(f => !f.EndsWith("/") && !string.IsNullOrEmpty(f))
                          .Select(f => new
                          {
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

                bool isProcessed = FileIsProcessed(latestFile.Name, region);
                if (!isProcessed)
                {
                  string path = ftpHost + ("/" + remoteDirectory + "/" + latestFile.Name);
                  // Create the FTP request to download the latest file
                  FtpWebRequest downloadRequest = (FtpWebRequest)WebRequest.Create(path);
                  downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                  downloadRequest.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                            
                  //To get the location the assembly normally resides on disk or the install directory
                  string currentDirectoryPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                      
                  var directoryNamees = region == "KASHMIR REGION" ? "K_Disbursement" : "J_Disbursement";
                  string serverMapPath = Path.Combine(Path.GetDirectoryName(currentDirectoryPath).Replace("file:\\", "").Replace("\\bin", ""), $"\\DataFile\\BankMediaFile\\{directoryNames}");

                  if (!Directory.Exists(serverMapPath))
                  {
                    // Attempt to create the directory
                    Directory.CreateDirectory(serverMapPath);
                  }
                  string filePath = Path.Combine(serverMapPath, latestFile.Name);

                  // delete file in safe way
                  bool isFileDeleted = TryDeleteFile(filePath);
                  bool isCsvFileDeleted = TryDeleteFile(filePath.Replace(".xlsx", ".csv").Replace(".xls", ".csv"));

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

                  UploadDownloadedFileOnDatabaseServer(filePath, region);

                  var directoryNamDis = region == "KASHMIR REGION" ? "K_BankMediaFileUpload" : "J_BankMediaFileUpload";

                  string databaseFilePath = ftpSetting["localFilePath"] + $"/DataFiles/{directoryNamDis}/" + csvFileName;
                             
                  UpdateBankMediaExcelDatabase(databaseFilePath, region);
                  SaveDownloadMediaDetail(latestFile.Name, true, region);
                  UpdatePostedStatus();
                  CloseBatchProcessAjax();
                  SaveArchiveFile(latestFile.Name, new NetworkCredential(ftpUsername, ftpPassword), ftpHost, region);
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


            var keyFile = new PrivateKeyFile(ftpSetting["sftpPrivateKeyPath"]);
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

                bool isProcessed = FileIsProcessed(csvFileName, region);
                if (!isProcessed)
                {
                  //To get the location the assembly normally resides on disk or the install directory
                  string currentDirectoryPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

                  var directoryNames = region == "KASHMIR REGION" ? "K_MasterEmployeeDownloads" : "J_MasterEmployeeDownloads";
                  string serverMapPath = Path.GetDirectoryName(currentDirectoryPath).Replace("file:\\", "").Replace("\\bin", "") + $"\\DataFile\\{directoryNames}";

                  if (!Directory.Exists(serverMapPath))
                  {
                    // Attempt to create the directory
                    Directory.CreateDirectory(serverMapPath);
                  }
                  string filePath = Path.Combine(serverMapPath, latestFile.Name);


                  // delete file in safe way
                  bool isFileDeleted = TryDeleteFile(filePath);
                  bool isCsvFileDeleted = TryDeleteFile(filePath.Replace(".xlsx", ".csv").Replace(".xls", ".csv"));

                  // Download the latest file
                  using (var fileStream = File.Create(filePath))
                  {
                    sftpClient.DownloadFile(latestFile.FullName, fileStream);
                  }

                  UploadDownloadedFileOnDatabaseServer(filePath, region);
                  var directoryNameDiss = region == "KASHMIR REGION" ? "K_BankMediaFileUpload" : "J_BankMediaFileUpload";
                  string databaseFilePath = ftpSetting["localFilePath"] + $"/DataFiles/{directoryNameDiss}/" + csvFileName;

                  UpdateBankMediaExcelDatabase(databaseFilePath, region);
                  SaveDownloadMediaDetail(latestFile.Name, true, region);
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

    bool TryDeleteFile(string filePath)
    {
      try
      {
        // Try opening the file with exclusive access
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
        {
          // If we can open it, no other process is using it, so we can delete it
          fs.Close(); // Close the file before deleting
        }

        // Delete the file
        File.Delete(filePath);
        return true; // Success
      }
      catch (IOException)
      {
        // The file is in use or cannot be accessed
        return false; // Deletion failed
      }
      catch (UnauthorizedAccessException)
      {
        // Handle permissions issues
        return false; // Deletion failed due to access issues
      }
    }

    public void SaveArchiveFile(string FileName, NetworkCredential Credential, string ftpHost, string region)
    {
      Dictionary<string, string> ftpSetting = Helper.GetFTPSetting();
      var directoryNames = region == "KASHMIR REGION" ? "K_Archive" : "J_Archive";
      string DestinationFolderPath = ftpHost + ftpSetting["sftpFilePath"] + $"/{directoryNames}/";

      var directoryNameDis = region == "KASHMIR REGION" ? "K_Disbursement" : "J_Disbursement";

      List<string> SourceFileUrl = new List<string>
            {
                ftpHost + ftpSetting["sftpFilePath"] + $"/{directoryNameDis}/Inbox/" + FileName,
               ftpHost + ftpSetting["sftpFilePath"] + $"/{directoryNameDis}/Outbox/" + FileName
            };
      try
      {
        foreach (string SourcePath in SourceFileUrl)
        {
          Uri sourceUri = new Uri(SourcePath);
          Uri destinationUri = new Uri(ftpHost + ftpSetting["sftpFilePath"] + $"/{directoryNames}/Outbox_" + FileName);
          if (SourcePath.ToLower().Trim().Contains("inbox"))
            destinationUri = new Uri(ftpHost + ftpSetting["sftpFilePath"] + $"/{directoryNames}/Inbox_" + FileName);
          else
            destinationUri = new Uri(ftpHost + ftpSetting["sftpFilePath"] + $"/{directoryNames}/Outbox_" + FileName);
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
          using (FtpWebResponse sourceResponse = (FtpWebResponse)sourceRequest.GetResponse())
          using (Stream sourceStream = sourceResponse.GetResponseStream())
          {
            using (Stream destinationStream = destinationRequest.GetRequestStream())
            {
              sourceStream.CopyTo(destinationStream);
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
    private void SaveDownloadMediaDetail(string fileName, bool IsProcessed, string region, Int32 TotalBeneficiary = 0, Int32 TotalValidated = 0, Int32 TotalNotvalidated = 0, string remark = "")
    {
      try
      {
        string recordId = "0";
        if (fileName != "" && fileName != null)
        {
          var fileNames = fileName.Split('_')[1];
          recordId = fileNames.Split('_')[0];
          DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
          DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDALByRegion(region);

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
    private void UploadDownloadedFileOnDatabaseServer(string filePath, string region)
    {
      try
      {
        string csvFilePath = filePath.Replace(".xlsx", ".csv").Replace(".xls", ".csv");
        if (Path.GetExtension(filePath) == ".xls" || Path.GetExtension(filePath) == ".xlsx")
        {
          SaveExcelAsCsv(filePath, csvFilePath);
        }
        byte[] bytes = System.IO.File.ReadAllBytes(csvFilePath);
        string failedMessage = UploadDataFile(bytes, Path.GetFileName(csvFilePath), region);
        // Convert csv file to data table to get counts
        DataTable dataTable = ConvertCsvToDataTable(csvFilePath);
        int benif_Count = dataTable.Rows.Count;
        int Validated_count = dataTable.AsEnumerable().Count(row => row.Field<string>("Status") == "OK");
        int Notvalidated_count = dataTable.AsEnumerable().Count(row => row.Field<string>("Status") != "OK");
        SaveDownloadMediaDetail(Path.GetFileName(filePath), false, region, benif_Count, Validated_count, Notvalidated_count, "");

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
            int rowCount = worksheet.Dimension.Rows;
            int colCount = worksheet.Dimension.Columns;

            for (int row = 1; row <= rowCount; row++)
            {
                for (int col = 1; col <= colCount; col++)
                {
                    var cellValue = worksheet.Cells[row, col].Value?.ToString() ?? "";
                    if (cellValue.Contains(",") || cellValue.Contains("\"") || cellValue.Contains("\n"))
                    {
                        cellValue = "\"" + cellValue.Replace("\"", "\"\"") + "\"";
                    }
                    csvContent.Append(cellValue + (col == colCount ? "" : ","));
                }
                csvContent.AppendLine();
            }
            File.WriteAllText(csvFilePath, csvContent.ToString(), Encoding.UTF8);
        }
      }
      catch (Exception ex)
      {

        throw;
      }
    }
    private string UploadDataFile(byte[] fileContents, string fileName, string region)
    {
      string failedMessage = string.Empty;
      try
      {
        Dictionary<string, string> ftpSetting = Helper.GetFTPSetting();
        // Get the file name

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
    private void UpdateBankMediaExcelDatabase(string localDirectory, string region)
    {
      try
      {
        int userId = 1;

        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDALByRegion(region);

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

    private Boolean FileIsProcessed(string fileName, string region)
    {
      try
      {
        bool isProcessed = false;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDALByRegion(region);

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