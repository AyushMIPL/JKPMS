using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using DocumentFormat.OpenXml.Math;
using LinqToExcel;
using Microsoft.Office.Interop.Excel;
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

                int row = PayReportData.Tables[0].Rows.Count + 1;

                if (repoertName == "rptPaymentSuccessReport" || repoertName == "rptPaymentFailedReport" || repoertName == "rptArrears")
                {
                    oSheet.Name = repoertName == "rptPaymentSuccessReport" ? "Success Payments" : (repoertName == "rptPaymentFailedReport" ? "Payment Failed" : "Arrear Payment");
                    oSheet.Cells[1, 1] = "Application Ref No.";
                    oSheet.Cells[1, 2] = "Beneficiary Name";
                    oSheet.Cells[1, 3] = "Scheme";
                    oSheet.Cells[1, 4] = "Bank Name";
                    oSheet.Cells[1, 5] = "IFSC Code";
                    oSheet.Cells[1, 6] = "Account No";
                    oSheet.Cells[1, 6].EntireColumn.NumberFormat = "@";
                    oSheet.Cells[1, 7] = "Paid On";
                    oSheet.Cells[1, 7].EntireColumn.NumberFormat = "MM/DD/YYYY";
                    oSheet.Cells[1, 8] = "Amount";
                    oSheet.Cells[1, 9] = "District";
                    oSheet.Cells[1, 10] = "TransactionRefrenceNo";
                    oSheet.Cells[1, 11] = "TransactionDate";
                    oSheet.Cells[1, 11].EntireColumn.NumberFormat = "MM/DD/YYYY";
                    oSheet.Cells[1, 12] = "Reason/Remarks";
                    dynamic[,] saNames = new dynamic[row, 12];
                    int i = 0;
                    if (PayReportData.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in PayReportData.Tables[0].Rows)
                        {
                            saNames[i, 0] = item["ApplicationReferenceno"].ToString();
                            saNames[i, 1] = item["ApplicantName"].ToString();
                            saNames[i, 2] = item["type_code"].ToString();
                            saNames[i, 3] = item["BankName"].ToString();
                            saNames[i, 4] = item["IFSCCode"].ToString();
                            saNames[i, 5] = item["bank_acct_no"].ToString();
                            saNames[i, 6] = item["pay_date"].ToString();
                            saNames[i, 7] = item["amount"].ToString();
                            saNames[i, 8] = item["District"].ToString();
                            saNames[i, 9] = item["TransactionRefrenceNo"].ToString();
                            saNames[i, 10] = item["TransactionDate"].ToString();
                            saNames[i, 11] = item["Reason/Remarks"].ToString();
                            i++;
                        }
                        oSheet.get_Range("A2", $"L{row}").Value2 = saNames;
                    }
                }
                else if (repoertName == "rptPaymentHistory")
                {
                    oSheet.Name = "Payment History";
                    oSheet.Cells[1, 1] = "Name";
                    oSheet.Cells[1, 2] = "Application Ref No.";
                    oSheet.Cells[1, 3] = "Scheme";
                    oSheet.Cells[1, 4] = "Address";
                    oSheet.Cells[1, 5] = "Account No";
                    oSheet.Cells[1, 5].EntireColumn.NumberFormat = "@";
                    oSheet.Cells[1, 6] = "Bank Name";
                    oSheet.Cells[1, 7] = "IFSC Code";
                    oSheet.Cells[1, 8] = "Pension Generated On";
                    oSheet.Cells[1, 8].EntireColumn.NumberFormat = "MM/DD/YYYY";
                    oSheet.Cells[1, 9] = "Amount";
                    oSheet.Cells[1, 10] = "Status";
                    oSheet.Cells[1, 11] = "Reason";
                    //oSheet.Cells[1, 12] = "TransactionDate";
                    //oSheet.Cells[1, 12].EntireColumn.NumberFormat = "MM/DD/YYYY";
                    oSheet.Cells[1, 12] = "TransactionRefrenceNo";

                    dynamic[,] saNames = new dynamic[row, 13];
                    int i = 0;
                    if (PayReportData.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in PayReportData.Tables[0].Rows)
                        {
                            saNames[i, 0] = item["ApplicantName"].ToString();
                            saNames[i, 1] = item["ApprovalDate"].ToString();
                            saNames[i, 2] = item["SchemeType"].ToString();
                            saNames[i, 3] = item["Address"].ToString();
                            saNames[i, 4] = item["AccountNo"].ToString();
                            saNames[i, 5] = item["BankName"].ToString();
                            saNames[i, 6] = item["IFSC_Code"].ToString();
                            saNames[i, 7] = item["PaidOn"].ToString();
                            saNames[i, 8] = item["Amount"].ToString();
                            saNames[i, 9] = item["Status"].ToString();
                            saNames[i, 10] = item["Reason"].ToString();
                            //saNames[i, 11] = item["TransactionDate"].ToString();
                            saNames[i, 11] = item["TransactionRefrenceNo"].ToString();
                            i++;
                        }
                        oSheet.get_Range("A2", $"M{row}").Value2 = saNames;
                    }
                }


                Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range("A2", $"M{row}");
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                oRng = oSheet.get_Range("A2", $"M{row}");
                oRng.EntireColumn.AutoFit();
                oXL.Visible = false;
                oXL.UserControl = false;
                string rptPath = System.Web.HttpContext.Current.Session["ReportName"] != null ? System.Web.HttpContext.Current.Session["ReportName"].ToString() : "";
                var fileName = Path.GetFileNameWithoutExtension(rptPath) + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year;//TextHelper.GenerateRandomText(12);
                fileName = fileName + ".xls";
                bool exists = System.IO.Directory.Exists(Server.MapPath("~/DownloadedExcel"));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/DownloadedExcel"));
                var path = Server.MapPath("~/DownloadedExcel/" + fileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                oWB.SaveAs(Server.MapPath("~/DownloadedExcel/" + fileName), Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                oWB.Close();
                Response.ClearContent();
                Response.ContentType = "application/xlsx";
                Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                Response.TransmitFile(path);
                Response.End();
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

                    int row = PayReportData.Tables[0].Rows.Count + 1;

                    if (repoertName == "rptPaymentSuccessReport" || repoertName == "rptPaymentFailedReport" || repoertName == "rptArrears")
                    {
                        oSheet.Name = repoertName == "rptPaymentSuccessReport" ? "Success Payments" : (repoertName == "rptPaymentFailedReport" ? "Payment Failed" : "Arrear Payment");
                        oSheet.Cells[1, 1] = "Application Ref No.";
                        oSheet.Cells[1, 2] = "Beneficiary Name";
                        oSheet.Cells[1, 3] = "Scheme";
                        oSheet.Cells[1, 4] = "Bank Name";
                        oSheet.Cells[1, 5] = "IFSC Code";
                        oSheet.Cells[1, 6] = "Account No";
                        oSheet.Cells[1, 6].EntireColumn.NumberFormat = "@";
                        oSheet.Cells[1, 7] = "Paid On";
                        oSheet.Cells[1, 7].EntireColumn.NumberFormat = "MM/DD/YYYY";
                        oSheet.Cells[1, 8] = "Amount";
                        oSheet.Cells[1, 9] = "District";
                        oSheet.Cells[1, 10] = "TransactionRefrenceNo";
                        oSheet.Cells[1, 11] = "TransactionDate";
                        oSheet.Cells[1, 11].EntireColumn.NumberFormat = "MM/DD/YYYY";
                        oSheet.Cells[1, 12] = "Reason/Remarks";
                        dynamic[,] saNames = new dynamic[row, 12];
                        int i = 0;
                        if (PayReportData.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow item in PayReportData.Tables[0].Rows)
                            {
                                saNames[i, 0] = item["ApplicationReferenceno"].ToString();
                                saNames[i, 1] = item["ApplicantName"].ToString();
                                saNames[i, 2] = item["type_code"].ToString();
                                saNames[i, 3] = item["BankName"].ToString();
                                saNames[i, 4] = item["IFSCCode"].ToString();
                                saNames[i, 5] = item["bank_acct_no"].ToString();
                                saNames[i, 6] = item["pay_date"].ToString();
                                saNames[i, 7] = item["amount"].ToString();
                                saNames[i, 8] = item["District"].ToString();
                                saNames[i, 9] = item["TransactionRefrenceNo"].ToString();
                                saNames[i, 10] = item["TransactionDate"].ToString();
                                saNames[i, 11] = item["Reason/Remarks"].ToString();
                                i++;
                            }
                            oSheet.get_Range("A2", $"L{row}").Value2 = saNames;
                        }
                    }
                    else if (repoertName == "rptPaymentHistory")
                    {
                        oSheet.Name = "Payment History";
                        oSheet.Cells[1, 1] = "Name";
                        oSheet.Cells[1, 2] = "Application Ref No.";
                        oSheet.Cells[1, 3] = "Scheme";
                        oSheet.Cells[1, 4] = "Address";
                        oSheet.Cells[1, 5] = "Account No";
                        oSheet.Cells[1, 5].EntireColumn.NumberFormat = "@";
                        oSheet.Cells[1, 6] = "Bank Name";
                        oSheet.Cells[1, 7] = "IFSC Code";
                        oSheet.Cells[1, 8] = "Pension Generated On";
                        oSheet.Cells[1, 8].EntireColumn.NumberFormat = "MM/DD/YYYY";
                        oSheet.Cells[1, 9] = "Amount";
                        oSheet.Cells[1, 10] = "Status";
                        oSheet.Cells[1, 11] = "Reason";
                        //oSheet.Cells[1, 12] = "TransactionDate";
                        //oSheet.Cells[1, 12].EntireColumn.NumberFormat = "MM/DD/YYYY";
                        oSheet.Cells[1, 12] = "TransactionRefrenceNo";

                        dynamic[,] saNames = new dynamic[row, 13];
                        int i = 0;
                        if (PayReportData.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow item in PayReportData.Tables[0].Rows)
                            {
                                saNames[i, 0] = item["ApplicantName"].ToString();
                                saNames[i, 1] = item["ApprovalDate"].ToString();
                                saNames[i, 2] = item["SchemeType"].ToString();
                                saNames[i, 3] = item["Address"].ToString();
                                saNames[i, 4] = item["AccountNo"].ToString();
                                saNames[i, 5] = item["BankName"].ToString();
                                saNames[i, 6] = item["IFSC_Code"].ToString();
                                saNames[i, 7] = item["PaidOn"].ToString();
                                saNames[i, 8] = item["Amount"].ToString();
                                saNames[i, 9] = item["Status"].ToString();
                                saNames[i, 10] = item["Reason"].ToString();
                                //saNames[i, 11] = item["TransactionDate"].ToString();
                                saNames[i, 11] = item["TransactionRefrenceNo"].ToString();
                                i++;
                            }
                            oSheet.get_Range("A2", $"M{row}").Value2 = saNames;
                        }
                    }

                    Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range("A2", $"M{row}");
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    oRng = oSheet.get_Range("A2", $"M{row}");
                    oRng.EntireColumn.AutoFit();
                    oXL.Visible = false;
                    oXL.UserControl = false;
                    string rptPath = System.Web.HttpContext.Current.Session["ReportName"] != null ? System.Web.HttpContext.Current.Session["ReportName"].ToString() : "";
                    var fileName = Path.GetFileNameWithoutExtension(rptPath) + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year;//TextHelper.GenerateRandomText(12);
                    fileName = fileName + ".xls";
                    bool exists = System.IO.Directory.Exists(Server.MapPath("~/DownloadedExcel"));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/DownloadedExcel"));
                    var path = Server.MapPath("~/DownloadedExcel/" + fileName);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    string ReportPath = Server.MapPath("~/DownloadedExcel/" + fileName);

                    byte[] fileBytes = File.ReadAllBytes(ReportPath);
                    FileContentResult bytesdata = new FileContentResult(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet)
                    {
                        FileDownloadName = fileName
                    };

                    return bytesdata;
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