using App.Data.Entities;
using JKPS.BLL;
using JKPS.COMMON;
using JKPS.DL;
using Quartz;
using Renci.SshNet;
using OfficeOpenXml;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JKPS.Schedular.Scheduler
{
  public class BankDisbursementUtility : IJob
  {
    public Task Execute(IJobExecutionContext context)
    {
      try
      {

        Dictionary<string, string> ftpSetting = Helper.GetFTPSetting();

        string localDirectory = ftpSetting["localFilePath"] + "\\DataFiles\\BankMediaFile";

        bool IsFTP = Convert.ToBoolean(ftpSetting["IsFTP"]);

        if (IsFTP)
        {
          string ftpHost = ftpSetting["ftpServerUrl"];
          string ftpUsername = ftpSetting["ftpUsername"];
          string ftpPassword = ftpSetting["ftpPassword"];
          //string remoteDirectory = ftpSetting["ftpFilePath"] + "/Inbox";
          string remoteDirectory = "DataFiles/Disbursement/Outbox";


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
              string path = ftpSetting["ftpServerUrl"] + ("/" + remoteDirectory + "/" + latestFile.Name);
              // Create the FTP request to download the latest file
              FtpWebRequest downloadRequest = (FtpWebRequest)WebRequest.Create(path);
              downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
              downloadRequest.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

              //To get the location the assembly normally resides on disk or the install directory
              string currentDirectoryPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

              string serverMapPath = Path.GetDirectoryName(currentDirectoryPath).Replace("file:\\", "").Replace("\\bin", "") + "\\DataFile\\BankMediaFile\\JK_Disbursement";

              //string serverMapPath = Path.Combine(Path.GetDirectoryName(currentDirectoryPath).Replace("file:\\", "").Replace("\\bin", ""), "\\DataFile\\BankMediaFile\\JK_Disbursement");
              if (!Directory.Exists(serverMapPath))
              {
                // Attempt to create the directory
                Directory.CreateDirectory(serverMapPath);
              }
              string filePath = Path.Combine(serverMapPath, latestFile.Name);

              FileInfo efile = new FileInfo(filePath);
              if (efile.Exists)//check file exsit or not  
              {
                efile.Delete();
              }
              FileInfo csvfile = new FileInfo(filePath.Replace(".xlsx", ".csv").Replace(".xls", ".csv"));
              if (csvfile.Exists)//check file exsit or not  
              {
                csvfile.Delete();
              }

              // Get the response and download the file
              using (FtpWebResponse downloadResponse = (FtpWebResponse)downloadRequest.GetResponse())
              {
                using (Stream downloadStream = downloadResponse.GetResponseStream())
                {
                  using (FileStream fileStream = File.Create(filePath))
                  {
                    downloadStream.CopyTo(fileStream);
                  }
                }
              }

              UploadDownloadedFileOnDatabaseServer(filePath);
              string csvFileName = latestFile.Name.Replace(".xlsx", ".csv").Replace(".xls", ".csv");

              string databaseFilePath = ftpSetting["localFilePath"] + "/DataFiles/BankMediaFileUpload/" + csvFileName;
              UpdateBankMediaExcelDatabase(databaseFilePath);
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
          int port = Convert.ToInt32(ftpSetting["ftpUsername"]); //SFTP default port is 22
          string username = ftpSetting["sftpUsername"];
          string password = ftpSetting["sftpPassword"];
          string remoteDirectory = ftpSetting["sftpFilePath"] + "/Disbursement/Inbox";

          using (var client = new SftpClient(host, port, username, password))
          {
            client.Connect();

            var files = client.ListDirectory(remoteDirectory);

            // Filter out directories and find the latest file
            var latestFile = files
                .Where(f => !f.IsDirectory)
                .OrderByDescending(f => f.LastWriteTime)
                .FirstOrDefault();

            if (latestFile != null)
            {
              // Download the latest file
              using (var fileStream = File.Create((localDirectory + latestFile.Name)))
              {
                client.DownloadFile(latestFile.FullName, fileStream);
              }
            }
            client.Disconnect();
          }
        }


        var task = Task.Run(() => true);
        return task;
      }
      catch (Exception ex)
      {
        throw;
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
      }
      catch (Exception)
      {

        throw;
      }
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

        string ftpServerUrl = ftpSetting["ftpServerUrl"] + "/DataFiles/BankMediaFileUpload/" + fileName;

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
      int userId = 1;
      // Define the parameters if needed (e.g., for input parameters)

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

      object[] parameters = new object[2];
      parameters[0] = localDirectory;
      parameters[1] = userId;
      // Execute the stored procedure
      int result = objDalBaseClass.ExecuteProcedure(ref parameters, "UpdateBankMediaExcelDatabase");

      UpdatePostedStatus();
      CloseBatchProcessAjax();
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
        List<DVOPYBatchProcessStybatchr> objBatchList = BLLPYBatchProcessStybatchr.GetActiveBatch();
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
  }
}
