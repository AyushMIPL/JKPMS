using App.Data.Entities;
using JKPS.COMMON;
using JKPS.DL;
using Quartz;
using Renci.SshNet;
using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Web.Helper
{
  public class BankValidationUtility : IJob
  {
    private readonly string[] AllRegions = { "JAMMU REGION", "KASHMIR REGION" };
    public void UpdateValidationResponse()
    {
      Execute(null);
    }
    public Task Execute(IJobExecutionContext context)
    {
      try
      {
        Dictionary<string, string> ftpSetting = Helper.GetFTPSetting();

        string localDirectory = ftpSetting["localFilePath"] + "\\DataFiles\\MasterEmployeeDownloads";

        bool IsFTP = Convert.ToBoolean(ftpSetting["IsFTP"]);

        if (IsFTP)
        {
          string ftpHost = ftpSetting["sftpServerUrl"];
          string ftpUsername = ftpSetting["sftpUsername"];
          string ftpPassword = ftpSetting["sftpPassword"];
          string remoteDirectory = ftpSetting["sftpFilePath"] + "/Validation/Inbox";

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
                  latestFile = files
                                        .Where(f => !f.EndsWith("/") && !string.IsNullOrEmpty(f))
                                        .Select(f => new
                                        {
                                          Name = f.Substring(f.LastIndexOf(' ') + 1),
                                          LastModified = DateTime.ParseExact((files[0].Substring(files[0].IndexOf(":") - 9, 12)), "MMM dd HH:mm", null)
                                        })
                                        .OrderByDescending(f => f.LastModified)
                                        .FirstOrDefault();
                }
              }
            }
            if (latestFile != null)
            {
              string csvFileName = latestFile.Name.Replace(".xlsx", ".csv").Replace(".xls", ".csv");

              bool isProcessed = FileIsProcessed(csvFileName);
              if (!isProcessed)
              {
                string path = ftpSetting["ftpServerUrl"] + ("/" + remoteDirectory + "/" + latestFile.Name);
                // Create the FTP request to download the latest file
                FtpWebRequest downloadRequest = (FtpWebRequest)WebRequest.Create(path);
                downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                downloadRequest.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

                //To get the location the assembly normally resides on disk or the install directory
                string currentDirectoryPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

                string serverMapPath = Path.GetDirectoryName(currentDirectoryPath).Replace("file:\\", "").Replace("\\bin", "") + "\\DataFile\\MasterEmployeeDownloads";

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

                bool isUploadDownloadedFile = UploadDownloadedFileOnDatabaseServer(filePath);
                if (isUploadDownloadedFile)
                {
                  string databaseFilePath = ftpSetting["localFilePath"] + "/DataFiles/MasterEmployeeUploads/" + csvFileName;
                  UpdateBankValidationExcelDatabase(databaseFilePath);
                  SaveDownloadMediaDetail(csvFileName, true, 0, 0, 0, 0, "");
                }
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
          string remoteDirectory = Helper.GetRegionBasedValidationPath(ftpSetting["sftpFilePath"]) + "/Inbox/";

          string inboxDestinationDir = Helper.GetRegionBasedValidationPath(ftpSetting["sftpFilePath"]) + "/Inbox/Processed/";
          string outboxSourceDir = Helper.GetRegionBasedValidationPath(ftpSetting["sftpFilePath"]) + "/Outbox/";
          string outboxDestinationDir = Helper.GetRegionBasedValidationPath(ftpSetting["sftpFilePath"]) + "/Outbox/Processed/";
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

                string serverMapPath = Path.GetDirectoryName(currentDirectoryPath).Replace("file:\\", "").Replace("\\bin", "") + "\\DataFile\\MasterEmployeeDownloads";

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

                bool isUploadDownloadedFile = UploadDownloadedFileOnDatabaseServer(filePath);
                if (isUploadDownloadedFile)
                {
                  string databaseFilePath = ftpSetting["localFilePath"] + "/DataFiles/MasterEmployeeUploads/" + csvFileName;
                  UpdateBankValidationExcelDatabase(databaseFilePath);
                  SaveDownloadMediaDetail(csvFileName, true, 0, 0, 0, 0, "");
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
                  //// Check if the destination file exists
                  if (sftpClient.Exists(Path.Combine(remoteDirectory, latestFile.Name)))
                  {
                    sftpClient.RenameFile(Path.Combine(remoteDirectory, latestFile.Name), Path.Combine(inboxDestinationDir, latestFile.Name));
                  }
                  // Check if the destination file exists
                  if (sftpClient.Exists(Path.Combine(outboxSourceDir, latestFile.Name)))
                  {
                    string sentFileName = GetSentFileName(latestFile.Name);
                    sftpClient.RenameFile(Path.Combine(outboxSourceDir, sentFileName), Path.Combine(outboxDestinationDir, latestFile.Name));
                  }
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

    public void SaveValidationArchiveFile(string FileName, NetworkCredential Credential, string ftpHost)
    {
      Dictionary<string, string> ftpSetting = Helper.GetFTPSetting();
      string DestinationFolderPath = ftpHost + ftpSetting["sftpFilePath"] + "/ValidationArchive/";
      List<string> SourceFileUrl = new List<string>
            {
               ftpHost + ftpSetting["sftpFilePath"] + "/Validation/Inbox/" + FileName,
               ftpHost + ftpSetting["sftpFilePath"] + "/Validation/Outbox/" + FileName
            };
      try
      {

        foreach (string SourcePath in SourceFileUrl)
        {
          Uri sourceUri = new Uri(SourcePath);
          Uri destinationUri = new Uri(ftpHost + ftpSetting["sftpFilePath"] + "/ValidationArchive/Outbox_" + FileName);
          if (SourcePath.ToLower().Trim().Contains("inbox"))
            destinationUri = new Uri(ftpHost + ftpSetting["sftpFilePath"] + "/ValidationArchive/Inbox_" + FileName);
          else
            destinationUri = new Uri(ftpHost + ftpSetting["sftpFilePath"] + "/ValidationArchive/Outbox_" + FileName);
          BankDisbursementUtility bankdisbursementutility = new BankDisbursementUtility();
          if (!bankdisbursementutility.FtpDirectoryExists(DestinationFolderPath, Credential))
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
    private void SaveDownloadMediaDetail(string fileName, bool IsProcessed, Int32 recordIdstr, Int32 benif_Count, Int32 Validated_count, Int32 Notvalidated_count, string remark = "")
    {
      try
      {
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

        object[] parameters = new object[18];
        parameters[0] = fileName; // FileName 
        parameters[1] = true; // HasDownoaded
        parameters[2] = IsProcessed; // IsProcessed
        parameters[3] = Environment.MachineName; // CreatedMachineInfo
        parameters[4] = 1; // CreatedBy
        parameters[5] = DateTime.Now; // CreatedOn
        parameters[6] = true; // IsActive
        parameters[7] = Convert.ToInt32(MediaType.Validation_res); // MediaType
        parameters[8] = Environment.MachineName; // ModifiedMachineInfo
        parameters[9] = 1; // ModifiedBy
        parameters[10] = DateTime.Now; // ModifiedOn
        parameters[11] = recordIdstr; // RecordID
        parameters[12] = Convert.ToInt32(benif_Count);
        parameters[13] = Convert.ToInt32(Validated_count);
        parameters[14] = Convert.ToInt32(Notvalidated_count);
        parameters[15] = remark;
        parameters[16] = "";
        parameters[17] = "";

        // Execute the stored procedure
        objDalBaseClass.ExecuteProcedure(ref parameters, "SaveDownloadMediaDetail");
      }
      catch (Exception ex)
      {
      }
    }
    private bool UploadDownloadedFileOnDatabaseServer(string filePath)
    {
      bool isUploadDownloadedFile = true;
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
        System.Data.DataTable dataTable = ConvertCsvToDataTable(csvFilePath);

        // Helper method to replace characters in a string
        Func<string, string> replaceCharacters = s => s.Replace("\r", "").Replace("\n", "");

        int benif_Count = dataTable.Rows.Count;

        int Validated_count = dataTable.AsEnumerable()
            .Where(row => StatusValidator.IsActive(row.Field<string>("ACCOUNT_STATUS"))
            && StatusValidator.Normalize(row.Field<string>("AADHAAR_STATUS")) == "AADHAAR SEEDED"
            && StatusValidator.Normalize(row.Field<string>("ACCT_SCHEME_TYPE")) == "SAVINGS ACCOUNT"
            && StatusValidator.Normalize(row.Field<string>("NAME_OF_APPLICANT")) == StatusValidator.Normalize(row.Field<string>("CBS_NAME"))
            && StatusValidator.Normalize(row.Field<string>("BENE_IFSC")) == StatusValidator.Normalize(row.Field<string>("BRANCH_CODE"))
          ).Count();

        int Notvalidated_count = benif_Count - Validated_count;

        StringBuilder stringBuilder = new StringBuilder();
        DateTime currentDateAndTime = DateTime.Now;

        // Format the date and time as "dd/MM/yyyy HH:mm"
        string formattedDateTime = currentDateAndTime.ToString("dd/MM/yyyy HH:mm");

        string userNameWithDate = "• [" + formattedDateTime + "] [" + AppUserManager.GetUserName() + "] ";

        DataColumnCollection columns = dataTable.Columns;
        if (!columns.Contains("APPLICATION_REFERENCE_NO"))
        {
          stringBuilder.AppendLine(userNameWithDate + ": APPLICATION_REFERENCE_NO Column is missing. <br />");
        }
        if (!columns.Contains("DISTRICT"))
        {
          stringBuilder.AppendLine(userNameWithDate + ": DISTRICT Column is missing. <br />");
        }
        if (!columns.Contains("BENE_IFSC"))
        {
          stringBuilder.AppendLine(userNameWithDate + ": BENE_IFSC Column is missing. <br />");
        }
        if (!columns.Contains("NAME_OF_APPLICANT"))
        {
          stringBuilder.AppendLine(userNameWithDate + ": NAME_OF_APPLICANT Column is missing. <br />");
        }
        if (!columns.Contains("ACCOUNTNO"))
        {
          stringBuilder.AppendLine(userNameWithDate + ": ACCOUNTNO Column is missing. <br />");
        }
        if (!columns.Contains("CATEGORY"))
        {
          stringBuilder.AppendLine(userNameWithDate + ": CATEGORY Column is missing. <br />");
        }
        if (!columns.Contains("CBS_NAME"))
        {
          stringBuilder.AppendLine(userNameWithDate + ": CBS_NAME Column is missing. <br />");
        }
        if (!columns.Contains("BRANCH_CODE"))
        {
          stringBuilder.AppendLine(userNameWithDate + ": BRANCH_CODE Column is missing. <br />");
        }
        if (!columns.Contains("ACCOUNT_STATUS"))
        {
          stringBuilder.AppendLine(userNameWithDate + ": ACCOUNT_STATUS Column is missing. <br />");
        }
        if (!columns.Contains("AADHAAR_STATUS"))
        {
          stringBuilder.AppendLine(userNameWithDate + ": AADHAAR_STATUS Column is missing. <br />");
        }
        if (!columns.Contains("ACCT_SCHEME_TYPE"))
        {
          stringBuilder.AppendLine(userNameWithDate + ": ACCT_SCHEME_TYPE Column is missing. <br />");
        }
        if (columns.Contains("APPLICATION_REFERENCE_NO"))
          if (dataTable.AsEnumerable().Where(row => string.IsNullOrEmpty(replaceCharacters(row.Field<string>("APPLICATION_REFERENCE_NO")))).Any())
          {
            stringBuilder.AppendLine(userNameWithDate + ": One or More APPLICATION_REFERENCE_NO is empty. <br />");
          }
        if (columns.Contains("DISTRICT"))
          if (dataTable.AsEnumerable().Where(row => string.IsNullOrEmpty(replaceCharacters(row.Field<string>("DISTRICT")))).Any())
          {
            stringBuilder.AppendLine(userNameWithDate + ": One or More DISTRICT is empty. <br />");
          }
        if (columns.Contains("BENE_IFSC"))
          if (dataTable.AsEnumerable().Where(row => string.IsNullOrEmpty(replaceCharacters(row.Field<string>("BENE_IFSC")))).Any())
          {
            stringBuilder.AppendLine(userNameWithDate + ": One or More BENE_IFSC is empty. <br />");
          }
        if (columns.Contains("APPLICATION_REFERENCE_NO"))
          if (dataTable.AsEnumerable().Where(row => string.IsNullOrEmpty(replaceCharacters(row.Field<string>("NAME_OF_APPLICANT")))).Any())
          {
            stringBuilder.AppendLine(userNameWithDate + ": One or More NAME_OF_APPLICANT is empty. <br />");
          }
        if (columns.Contains("APPLICATION_REFERENCE_NO"))
          if (dataTable.AsEnumerable().Where(row => string.IsNullOrEmpty(replaceCharacters(row.Field<string>("ACCOUNTNO")))).Any())
          {
            stringBuilder.AppendLine(userNameWithDate + ": One or More ACCOUNTNO is empty. <br />");
          }
        if (columns.Contains("APPLICATION_REFERENCE_NO"))
          if (dataTable.AsEnumerable().Where(row => string.IsNullOrEmpty(replaceCharacters(row.Field<string>("CATEGORY")))).Any())
          {
            stringBuilder.AppendLine(userNameWithDate + ": One or More CATEGORY is empty. <br />");
          }

        SaveDownloadMediaDetail(Path.GetFileName(csvFilePath), false, 0, benif_Count, Validated_count, Notvalidated_count, stringBuilder.ToString());
        if (!string.IsNullOrEmpty(stringBuilder.ToString()))
        {
          isUploadDownloadedFile = false;
        }
      }
      catch (Exception)
      {
        throw;
      }
      return isUploadDownloadedFile;
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

        using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read))
        {
          using (var package = new ExcelPackage(stream))
          {
            var worksheet = package.Workbook.Worksheets[1];
            var csvBuilder = new StringBuilder();
            if (worksheet.Dimension != null)
            {
              int rowCount = worksheet.Dimension.End.Row;
              int colCount = worksheet.Dimension.End.Column;

              for (int row = 1; row <= rowCount; row++)
              {
                var values = new List<string>();
                for (int col = 1; col <= colCount; col++)
                {
                  string text = worksheet.Cells[row, col].Value?.ToString() ?? "";
                  text = text.Replace("\"", "\"\"");
                  if (text.Contains(",") || text.Contains("\"") || text.Contains("\n"))
                    text = $"\"{text}\"";
                  values.Add(text);
                }
                csvBuilder.AppendLine(string.Join(",", values));
              }
            }
            File.WriteAllText(csvFilePath, csvBuilder.ToString(), Encoding.UTF8);
          }
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

        string ftpServerUrl = ftpSetting["ftpServerUrl"] + "/DataFiles/MasterEmployeeUploads/" + fileName;

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
    private void UpdateBankValidationExcelDatabase(string localDirectory)
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
        int result = objDalBaseClass.ExecuteProcedure(ref parameters, "UpdateEmpMasterEmpBankDetails");
      }
      catch (Exception ex)
      {
        throw;
      }

    }
    private Boolean FileIsProcessed(string fileName)
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
    private string GetSentFileName(string fileName)
    {
      int index = fileName.IndexOf('_', fileName.IndexOf('_') + 1);
      fileName = fileName.Substring(0, index);
      string sentFileName = "";
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      object[] parameters = new object[11];
      parameters[0] = fileName; // FileName 
                                // Execute the stored procedure
      var ds = objDalBaseClass.GetData("SELECT FilePath FROM [dbo].[Media_Queue] WHERE FilePath LIKE '%" + fileName + "%'");
      if (ds.Tables.Count > 0)
      {
        if (ds.Tables[0].Rows.Count > 0)
        {
          if (ds.Tables[0].Rows[0][0] != DBNull.Value)
          {
            sentFileName = Path.GetFileName(ds.Tables[0].Rows[0]["FilePath"].ToString());
          }
        }
      }
      return sentFileName;
    }
  }
}