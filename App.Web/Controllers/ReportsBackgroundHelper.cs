using System;
using System.Data;
using System.IO;
using System.Web.Mvc;
using System.Collections.Concurrent;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Linq;

namespace App.Web.Controllers
{
    public partial class ReportsController
    {
        public static ConcurrentDictionary<string, string> ReportJobs = new ConcurrentDictionary<string, string>();

        public JsonResult CheckReportStatus(string jobId)
        {
            if (ReportJobs.TryGetValue(jobId, out string status))
            {
                return Json(new { Status = status }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = "NotFound" }, JsonRequestBehavior.AllowGet);
        }

        // ... Keeping other static methods just in case ...
        public static void GenerateExcelToDisk(DataSet PayReportData, string filePath, string repoertName)
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add(repoertName);
                    int rowCount = PayReportData.Tables[0].Rows.Count + 1;

                    worksheet.Cells[1, 1].Value = "Application Ref No.";
                    worksheet.Cells[1, 2].Value = "Beneficiary Name";
                    worksheet.Cells[1, 3].Value = "Scheme";
                    worksheet.Cells[1, 4].Value = "Bank Name";
                    worksheet.Cells[1, 5].Value = "IFSC Code";
                    worksheet.Cells[1, 6].Value = "Account No";
                    worksheet.Column(6).Style.Numberformat.Format = "@";
                    worksheet.Cells[1, 7].Value = "Paid On";
                    worksheet.Column(7).Style.Numberformat.Format = "MM/DD/YYYY";
                    worksheet.Cells[1, 8].Value = "Amount";
                    worksheet.Cells[1, 9].Value = "District";
                    worksheet.Cells[1, 10].Value = "TransactionRefrenceNo";
                    worksheet.Cells[1, 11].Value = "TransactionDate";
                    worksheet.Column(11).Style.Numberformat.Format = "MM/DD/YYYY";
                    worksheet.Cells[1, 12].Value = "Reason/Remarks";

                    int i = 2;
                    foreach (DataRow item in PayReportData.Tables[0].Rows)
                    {
                        worksheet.Cells[i, 1].Value = item["ApplicationReferenceno"].ToString();
                        worksheet.Cells[i, 2].Value = item["ApplicantName"].ToString();
                        worksheet.Cells[i, 3].Value = item["type_code"].ToString();
                        worksheet.Cells[i, 4].Value = item["BankName"].ToString();
                        worksheet.Cells[i, 5].Value = item["IFSCCode"].ToString();
                        worksheet.Cells[i, 6].Value = item["bank_acct_no"].ToString();
                        worksheet.Cells[i, 7].Value = item["pay_date"].ToString();
                        worksheet.Cells[i, 8].Value = item["amount"].ToString();
                        worksheet.Cells[i, 9].Value = item["District"].ToString();
                        worksheet.Cells[i, 10].Value = item["TransactionRefrenceNo"].ToString();
                        worksheet.Cells[i, 11].Value = item["TransactionDate"].ToString();
                        worksheet.Cells[i, 12].Value = item["Reason/Remarks"].ToString();
                        i++;
                    }

                    package.SaveAs(new FileInfo(filePath));
                }
            }
            catch (Exception ex)
            {
                // handle
            }
        }

        public static void GeneratePdfToDisk(DataSet PayReportData, string filePath, string rptPath)
        {
            try
            {
                ReportDocument rptDoc = new ReportDocument();
                rptDoc.Load(rptPath);
                rptDoc.SetDataSource(PayReportData.Tables[0]);
                rptDoc.ExportToDisk(ExportFormatType.PortableDocFormat, filePath);
                rptDoc.Close();
                rptDoc.Dispose();
            }
            catch (Exception ex)
            {
                // handle
            }
        }
    }
}
