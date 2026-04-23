using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using DocumentFormat.OpenXml.Math;
using LinqToExcel;
// using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease.Activities;

namespace App.Web.ReportForms
{
    public partial class ContributionByEmployer : System.Web.UI.Page
    {
        ReportDocument rptDoc;
        protected void Page_Init(object sender, EventArgs e)
        {
            string ShowCrystalReportsInIframe = ConfigurationManager.AppSettings["ShowCrystalReportsInIframe"];
            if (!string.IsNullOrEmpty(ShowCrystalReportsInIframe))
            {
                if (ShowCrystalReportsInIframe == "0")
                {
                    ExportFormatType formatType = ExportFormatType.PortableDocFormat;
                    ConfigureCrystalReports(formatType, true);
                }
                else
                {
                    ConfigureCrystalReports();
                }
            }
            else
            {

                ConfigureCrystalReports();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Headers.Remove("Content-Security-Policy");
        }

        protected void excelBtn_Click(object sender, EventArgs e)
        {
            ExportFormatType formatType = ExportFormatType.ExcelRecord;
            ConfigureCrystalReports(formatType, false);
        }
        protected void pdfBtn_Click(object sender, EventArgs e)
        {
            ExportFormatType formatType = ExportFormatType.PortableDocFormat;
            ConfigureCrystalReports(formatType, false);

        }
        //protected void wordBtn_Click(object sender, EventArgs e)
        //{
        //  ExportFormatType formatType = ExportFormatType.WordForWindows;
        //  ConfigureCrystalReports(formatType);
        //}
        //protected void PrintBtn_Click(object sender, EventArgs e)
        //{

        //}
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (rptDoc != null)
            {
                rptDoc.Dispose();
                rptDoc.Close();
            }
        }
        private string GetParamValue(string paramName, ReportDocument ReportDoc)
        {
            string tmpValue = "";
            for (int i = 0; i < ReportDoc.DataDefinition.FormulaFields.Count; i++)
            {
                if (ReportDoc.DataDefinition.FormulaFields[i].FormulaName ==
                    "{" + paramName + "}")
                    tmpValue = ReportDoc.DataDefinition.FormulaFields[i].Text;
            }
            return tmpValue;
        }
        private void SetParamValue(string paramName, string paramValue, ReportDocument ReportDoc)
        {
            for (int i = 0; i < ReportDoc.DataDefinition.FormulaFields.Count; i++)
                if (ReportDoc.DataDefinition.FormulaFields[i].FormulaName ==
                      "{" + paramName + "}")
                    ReportDoc.DataDefinition.FormulaFields[i].Text = "\"" + paramValue + "\"";
        }
        private void ConfigureCrystalReports()
        {
            try
            {
                if (Session["rptSource"] != null && Session["ReportName"] != null)
                {
                    bool isValid = true;
                    string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();
                    var rptSource = System.Web.HttpContext.Current.Session["rptSource"];
                    DataSet ds = null;
                    if (strReportName == "RptPrintExceptionReportWages.rpt" || strReportName == "RptPrintExceptionReport.rpt" || strReportName == "rptPaySlipsA4.rpt" || strReportName == "rptPaySlipsA4Duplicate.rpt")
                    {
                        ds = (DataSet)rptSource;
                    }
                    if (string.IsNullOrEmpty(strReportName))
                    {
                        isValid = false;
                    }
                    if (isValid)
                    {
                        ReportDocument rd = new ReportDocument();
                        string strRptPath = Server.MapPath("~/") + "Reports\\" + strReportName;
                        rd.Load(strRptPath);
                        if (rptSource != null && rptSource.GetType().ToString() != "System.String")
                        {
                            if (strReportName == "RptPrintExceptionReportWages.rpt" || strReportName == "RptPrintExceptionReport.rpt")
                            {
                                rd.SetDataSource(ds.Tables[0]);
                            }
                            else if (strReportName == "rptPaySlipsA4.rpt" || strReportName == "rptPaySlipsA4Duplicate.rpt")
                            {
                                rd.SetDataSource(ds.Tables["Header"]);
                            }
                            else if (strReportName == "rptPaymentHistory.rpt")
                            {
                                ds = (DataSet)rptSource;
                                rd.SetDataSource(ds.Tables[0]);
                            }
                            else if (strReportName == "rptDirectDepositsToBank.rpt")
                            {
                                ds = (DataSet)rptSource;
                                rd.SetDataSource(ds.Tables[0]);
                            }//Added PaymentSucces
                            else if (strReportName == "rptPaymentSuccessReport.rpt")
                            {
                                ds = (DataSet)rptSource;
                                rd.SetDataSource(ds.Tables[0]);
                            }
                            //Added PaymentFailure
                            else if (strReportName == "rptPaymentFailure.rpt")
                            {
                                ds = (DataSet)rptSource;
                                rd.SetDataSource(ds.Tables[0]);
                            }
                            else
                                rd.SetDataSource(rptSource);
                        }
                        CrystalReportViewer.ReportSource = rd;
                        if (strReportName == "RptPrintExceptionReportWages.rpt" || strReportName == "RptPrintExceptionReport.rpt")
                        {
                            rd.Subreports["subDtlExcep.rpt"].SetDataSource(ds.Tables[2]);
                            rd.Subreports["SubGLSumReport.rpt"].SetDataSource(ds.Tables[1]);
                            rd.Subreports["SubGLSumReport.rpt - 01"].SetDataSource(ds.Tables[1]);
                        }
                        else if (strReportName == "rptPaySlipsA4.rpt")
                        {
                            rd.Subreports["Incomes"].SetDataSource(ds.Tables["Incomes"]);
                            //rd.Subreports["Deductions"].SetDataSource(ds.Tables["Deductions"]);
                        }
                        else if (strReportName == "rptPaySlipsA4Duplicate.rpt")
                        {
                            rd.Subreports["Incomes"].SetDataSource(ds.Tables["Incomes"]);
                            //rd.Subreports["Deductions"].SetDataSource(ds.Tables["Deductions"]);
                        }
                        else if (strReportName == "rptDirectDepositeListing.rpt")
                        {
                            if (Session["rptSourceParam"] != null)
                            {
                                var param = System.Web.HttpContext.Current.Session["rptSourceParam"].ToString().Split('~');
                                if (param.Length >= 4)
                                {
                                    SetParamValue("@ChequeNo", param[0], rd);
                                    SetParamValue("@Preparedby", param[1], rd);
                                    SetParamValue("@Checkedby", param[2], rd);
                                    SetParamValue("@Approvedby", param[3], rd);
                                    SetParamValue("@Receivedby", param[4], rd);
                                }
                            }
                        }
                        else if (strReportName == "rptRefundApplication.rpt")
                        {
                            ds = (DataSet)rptSource;
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                var param = ds.Tables[0].Rows[0]["InterestRate"].ToString().Replace(".00", "") + "%";
                                SetParamValue("@InterestRate", param, rd);
                            }
                        }
                        CrystalReportViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
                        rd.Refresh();
                        CrystalReportViewer.RefreshReport();
                    }
                    else
                    {
                        Response.Write("<H2>Nothing Found; No Report name found</H2>");
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
            catch (Exception ex)
            {
                //Response.Write(ex.ToString());
                ShowMessage(ex);
            }
        }
        public void ConfigureCrystalReports(ExportFormatType formatType, bool showPdfInIframe)
        {
            if (Session["rptSource"] != null && Session["ReportName1"] != null)
            {
                try
                {

                    DataSet rptSource = System.Web.HttpContext.Current.Session["rptSource"] != null ? (DataSet)System.Web.HttpContext.Current.Session["rptSource"] : null;
                    string rptPath = System.Web.HttpContext.Current.Session["ReportName1"] != null ? System.Web.HttpContext.Current.Session["ReportName1"].ToString() : "";
                    if (string.IsNullOrEmpty(rptPath) || rptSource == null)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append("Session value not set");
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    }
                    if (Path.GetFileNameWithoutExtension(rptPath) == "rptPaymentSuccessReport" && formatType == ExportFormatType.ExcelRecord)
                    {
                        CustomExcelExport(rptSource, "rptPaymentSuccessReport");
                        //CustomExcelExport2(rptSource);
                    }


                    if (Path.GetFileNameWithoutExtension(rptPath) == "rptPaymentFailedReport" && formatType == ExportFormatType.ExcelRecord)
                    {
                        CustomExcelExport(rptSource, "rptPaymentFailedReport");
                        //CustomExcelExport2(rptSource);
                    }

                    if (Path.GetFileNameWithoutExtension(rptPath) == "rptPaymentHistory" && formatType == ExportFormatType.ExcelRecord)
                    {
                        CustomExcelExport(rptSource, "rptPaymentHistory");
                        //CustomExcelExport2(rptSource);
                    }
                    if (Path.GetFileNameWithoutExtension(rptPath) == "rptArrears" && formatType == ExportFormatType.ExcelRecord)
                    {
                        CustomExcelExport(rptSource, "rptArrears");
                        //CustomExcelExport2(rptSource);
                    }

                    else
                    {
                        rptDoc = new ReportDocument();
                        //rptSource.WriteXmlSchema(@"D:\Sujeet_Sir\Himanshu_Rajput\App.Web\Reports\DsPaymentSuccessReport.xsd");

                        rptDoc.Load(rptPath);
                        rptDoc.SetDataSource(rptSource);
                        // rptDoc.SetParameterValue("ImageUrl", Server.MapPath(@"Content/images/Logo2.png"));
                        CrystalReportViewer.DisplayGroupTree = false;
                        //if (System.Web.HttpContext.Current.Session["PrintAllPages"] != null)
                        //  CRReportViewer.SeparatePages = false;
                        CrystalReportViewer.ReportSource = rptDoc;
                        CrystalReportViewer.RefreshReport();
                        string filename = Path.GetFileNameWithoutExtension(rptPath) != "rptPaymentSuccessReport" ? Path.GetFileNameWithoutExtension(rptPath) : "rptPaymentSuccessReport";
                        //CRReportViewer.PrintToPrinter(new PrinterSettings(), new PageSettings(), false);
                        // Declare variables and get the export options.
                        ExportOptions exportOpts = new ExportOptions();
                        ExcelFormatOptions excelFormatOpts = new ExcelFormatOptions();
                        DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                        exportOpts = rptDoc.ExportOptions;
                        ExportOptions options = new ExportOptions()
                        {
                            ExportFormatType = formatType,

                        };
                        if (formatType == ExportFormatType.ExcelRecord)
                        {
                            var excelOpts = new ExcelFormatOptions()
                            {
                                ExcelUseConstantColumnWidth = true,
                                ExcelTabHasColumnHeadings = true,
                                ExcelConstantColumnWidth = 0.5,
                                ShowGridLines = true,
                                ExportPageHeadersAndFooters = ExportPageAreaKind.OncePerReport,
                            };
                            options.ExportFormatOptions = excelOpts;
                        }

                        // Set up the response headers for inline display
                        //Response.Clear();
                        //Response.Buffer = true;
                        //Response.ContentType = "application/pdf";
                        //Response.AddHeader("Content-Disposition", "inline; filename=Report.pdf");

                        //rptDoc.ExportToHttpResponse(options, Response, true, filename);

                        if (showPdfInIframe)
                        {
                            // Export to a MemoryStream instead of directly to the response
                            using (var stream = rptDoc.ExportToStream(ExportFormatType.PortableDocFormat))
                            {
                                // Set up the response headers
                                Response.Clear();
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("Content-Disposition", "inline; filename=" + filename + ".pdf");

                                // Write the stream content to the response
                                stream.Seek(0, SeekOrigin.Begin);
                                stream.CopyTo(Response.OutputStream);
                                Response.Flush();
                                Response.End();
                            }
                        }
                        else
                        {
                            rptDoc.ExportToHttpResponse(options, Response, true, filename);
                            Response.End();
                        }

                    }
                }
                catch (Exception ex)
                {
                    System.Web.HttpContext.Current.Session["PrintAllPages"] = null;
                    //Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(ex));
                    ShowMessage(ex);
                }
            }
        }

        public void ShowMessage(Exception ex)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(ex.Message);
            sb.Append(ex.InnerException);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }
        private void CustomExcelExport(DataSet PayReportData, string repoertName)
        {
            try
            {
                /*
                object missing = Type.Missing;
                Microsoft.Office.Interop.Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;
                oXL.SheetsInNewWorkbook = 1;
                Microsoft.Office.Interop.Excel.Workbook oWB = oXL.Workbooks.Add(missing);
                Microsoft.Office.Interop.Excel.Worksheet oSheet = oWB.Sheets.Add(missing, missing, 1, missing)
                as Microsoft.Office.Interop.Excel.Worksheet;
                Microsoft.Office.Interop.Excel.Range oRng;
                object misvalue = System.Reflection.Missing.Value;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;
                */

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add(repoertName == "rptPaymentSuccessReport" ? "Success Payments" : (repoertName == "rptPaymentFailedReport" ? "Payment Failed" : (repoertName == "rptPaymentHistory" ? "Payment History" : "Arrear Payment")));

                    int rowCount = PayReportData.Tables[0].Rows.Count + 1;

                    if (repoertName == "rptPaymentSuccessReport" || repoertName == "rptPaymentFailedReport" || repoertName == "rptArrears")
                    {
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
                        
                        var range = worksheet.Cells[1, 1, rowCount, 12];
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[1, 1, rowCount, 12].AutoFitColumns();
                    }
                    else if (repoertName == "rptPaymentHistory")
                    {
                        worksheet.Cells[1, 1].Value = "Name";
                        worksheet.Cells[1, 2].Value = "Application Ref No.";
                        worksheet.Cells[1, 3].Value = "Scheme";
                        worksheet.Cells[1, 4].Value = "Address";
                        worksheet.Cells[1, 5].Value = "Account No";
                        worksheet.Column(5).Style.Numberformat.Format = "@";
                        worksheet.Cells[1, 6].Value = "Bank Name";
                        worksheet.Cells[1, 7].Value = "IFSC Code";
                        worksheet.Cells[1, 8].Value = "Pension Generated On";
                        worksheet.Column(8).Style.Numberformat.Format = "MM/DD/YYYY";
                        worksheet.Cells[1, 9].Value = "Amount";
                        worksheet.Cells[1, 10].Value = "Status";
                        worksheet.Cells[1, 11].Value = "Reason";
                        worksheet.Cells[1, 12].Value = "TransactionRefrenceNo";

                        int i = 2;
                        foreach (DataRow item in PayReportData.Tables[0].Rows)
                        {
                            worksheet.Cells[i, 0 + 1].Value = item["ApplicantName"].ToString();
                            worksheet.Cells[i, 1 + 1].Value = item["ApprovalDate"].ToString();
                            worksheet.Cells[i, 2 + 1].Value = item["SchemeType"].ToString();
                            worksheet.Cells[i, 3 + 1].Value = item["Address"].ToString();
                            worksheet.Cells[i, 4 + 1].Value = item["AccountNo"].ToString();
                            worksheet.Cells[i, 5 + 1].Value = item["BankName"].ToString();
                            worksheet.Cells[i, 6 + 1].Value = item["IFSC_Code"].ToString();
                            worksheet.Cells[i, 7 + 1].Value = item["PaidOn"].ToString();
                            worksheet.Cells[i, 8 + 1].Value = item["Amount"].ToString();
                            worksheet.Cells[i, 9 + 1].Value = item["Status"].ToString();
                            worksheet.Cells[i, 10 + 1].Value = item["Reason"].ToString();
                            worksheet.Cells[i, 11 + 1].Value = item["TransactionRefrenceNo"].ToString();
                            i++;
                        }
                        
                        var range = worksheet.Cells[1, 1, rowCount, 12];
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[1, 1, rowCount, 12].AutoFitColumns();
                    }

                    string rptPath = System.Web.HttpContext.Current.Session["ReportName"] != null ? System.Web.HttpContext.Current.Session["ReportName"].ToString() : "";
                    var fileName = Path.GetFileNameWithoutExtension(rptPath) + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year;
                    fileName = fileName + ".xlsx";
                    bool exists = System.IO.Directory.Exists(Server.MapPath("~/DownloadedExcel"));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/DownloadedExcel"));
                    var path = Server.MapPath("~/DownloadedExcel/" + fileName);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    
                    package.SaveAs(new FileInfo(path));
                    
                    Response.ClearContent();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                    Response.TransmitFile(path);
                    Response.End();
                }
            }
            catch (ThreadAbortException ex)
            {
                throw;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public FileContentResult CustomExcelExportAjax()
        {
            try
            {
                DataSet PayReportData = System.Web.HttpContext.Current.Session["rptSource"] != null ? (DataSet)System.Web.HttpContext.Current.Session["rptSource"] : null;
                string rptNameWithExtension = System.Web.HttpContext.Current.Session["ReportName1"] != null ? System.Web.HttpContext.Current.Session["ReportName1"].ToString() : "";
                string repoertName = Path.GetFileNameWithoutExtension(rptNameWithExtension);

                if (string.IsNullOrEmpty(rptNameWithExtension) || PayReportData == null)
                {
                    return new FileContentResult(new byte[0], System.Net.Mime.MediaTypeNames.Application.Octet)
                    {
                        FileDownloadName = "Empty.xlsx"
                    };
                }

                if (repoertName == "rptPaymentSuccessReport" || repoertName == "rptPaymentFailedReport" || repoertName == "rptArrears" || repoertName == "rptPaymentHistory")
                {
                    /*
                    object missing = Type.Missing;
                    Microsoft.Office.Interop.Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
                    oXL.Visible = false;
                    oXL.SheetsInNewWorkbook = 1;
                    Microsoft.Office.Interop.Excel.Workbook oWB = oXL.Workbooks.Add(missing);
                    Microsoft.Office.Interop.Excel.Worksheet oSheet = oWB.Sheets.Add(missing, missing, 1, missing)
                    as Microsoft.Office.Interop.Excel.Worksheet;
                    Microsoft.Office.Interop.Excel.Range oRng;
                    object misvalue = System.Reflection.Missing.Value;
                    oXL = new Microsoft.Office.Interop.Excel.Application();
                    oXL.Visible = false;
                    */

                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add(repoertName == "rptPaymentSuccessReport" ? "Success Payments" : (repoertName == "rptPaymentFailedReport" ? "Payment Failed" : (repoertName == "rptPaymentHistory" ? "Payment History" : "Arrear Payment")));

                        int rowCount = PayReportData.Tables[0].Rows.Count + 1;

                        if (repoertName == "rptPaymentSuccessReport" || repoertName == "rptPaymentFailedReport" || repoertName == "rptArrears")
                        {
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

                            var range = worksheet.Cells[1, 1, rowCount, 12];
                            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[1, 1, rowCount, 12].AutoFitColumns();
                        }
                        else if (repoertName == "rptPaymentHistory")
                        {
                            worksheet.Cells[1, 1].Value = "Name";
                            worksheet.Cells[1, 2].Value = "Application Ref No.";
                            worksheet.Cells[1, 3].Value = "Scheme";
                            worksheet.Cells[1, 4].Value = "Address";
                            worksheet.Cells[1, 5].Value = "Account No";
                            worksheet.Column(5).Style.Numberformat.Format = "@";
                            worksheet.Cells[1, 6].Value = "Bank Name";
                            worksheet.Cells[1, 7].Value = "IFSC Code";
                            worksheet.Cells[1, 8].Value = "Pension Generated On";
                            worksheet.Column(8).Style.Numberformat.Format = "MM/DD/YYYY";
                            worksheet.Cells[1, 9].Value = "Amount";
                            worksheet.Cells[1, 10].Value = "Status";
                            worksheet.Cells[1, 11].Value = "Reason";
                            worksheet.Cells[1, 12].Value = "TransactionRefrenceNo";

                            int i = 2;
                            foreach (DataRow item in PayReportData.Tables[0].Rows)
                            {
                                worksheet.Cells[i, 1].Value = item["ApplicantName"].ToString();
                                worksheet.Cells[i, 2].Value = item["ApprovalDate"].ToString();
                                worksheet.Cells[i, 3].Value = item["SchemeType"].ToString();
                                worksheet.Cells[i, 4].Value = item["Address"].ToString();
                                worksheet.Cells[i, 5].Value = item["AccountNo"].ToString();
                                worksheet.Cells[i, 6].Value = item["BankName"].ToString();
                                worksheet.Cells[i, 7].Value = item["IFSC_Code"].ToString();
                                worksheet.Cells[i, 8].Value = item["PaidOn"].ToString();
                                worksheet.Cells[i, 9].Value = item["Amount"].ToString();
                                worksheet.Cells[i, 10].Value = item["Status"].ToString();
                                worksheet.Cells[i, 11].Value = item["Reason"].ToString();
                                worksheet.Cells[i, 12].Value = item["TransactionRefrenceNo"].ToString();
                                i++;
                            }

                            var range = worksheet.Cells[1, 1, rowCount, 12];
                            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[1, 1, rowCount, 12].AutoFitColumns();
                        }

                        string rptPath = System.Web.HttpContext.Current.Session["ReportName"] != null ? System.Web.HttpContext.Current.Session["ReportName"].ToString() : "";
                        var fileName = Path.GetFileNameWithoutExtension(rptPath) + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year;
                        fileName = fileName + ".xlsx";

                        byte[] fileBytes = package.GetAsByteArray();
                        FileContentResult bytesdata = new FileContentResult(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                        {
                            FileDownloadName = fileName
                        };

                        return bytesdata;
                    }
                }
                else
                {
                    rptDoc = new ReportDocument();
                    rptDoc.Load(rptNameWithExtension);
                    rptDoc.SetDataSource(PayReportData);
                    string filename = Path.GetFileNameWithoutExtension(rptNameWithExtension) != "rptPaymentSuccessReport" ? Path.GetFileNameWithoutExtension(rptNameWithExtension) : "rptPaymentSuccessReport";
                    filename = filename + ".xls";
                    byte[] fileBytesOfRpt = ExportReportToExcel(rptDoc);
                    FileContentResult bytesdata = new FileContentResult(fileBytesOfRpt, System.Net.Mime.MediaTypeNames.Application.Octet)
                    {
                        FileDownloadName = filename
                    };

                    return bytesdata;
                }
            }
            catch
            {
                return new FileContentResult(new byte[0], System.Net.Mime.MediaTypeNames.Application.Octet)
                {
                    FileDownloadName = "Empty.xlsx"
                };
            }
        }

        public byte[] ExportReportToExcel(ReportDocument report)
        {
            // Specify the export format
            ExportFormatType formatType = ExportFormatType.ExcelWorkbook; // Excel format compatible with newer Excel versions

            // Export the report to a stream
            using (Stream exportStream = report.ExportToStream(formatType))
            {
                // Convert the stream to a byte array
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    exportStream.CopyTo(memoryStream);
                    return memoryStream.ToArray(); // Convert to byte array
                }
            }
        }

        public byte[] ExportReportToPdf(ReportDocument report)
        {
            // Specify the export format as PDF
            ExportFormatType formatType = ExportFormatType.PortableDocFormat;

            // Export the report to a stream
            using (Stream exportStream = report.ExportToStream(formatType))
            {
                // Convert the stream to a byte array
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    exportStream.CopyTo(memoryStream);
                    return memoryStream.ToArray(); // Convert to byte array
                }
            }
        }
    }
}