using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;

namespace App.Web.ReportForms
{
    public partial class TransactionMonitoring : System.Web.UI.Page
    {
        ReportDocument rptDoc;

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                string ShowCrystalReportsInIframe = ConfigurationManager.AppSettings["ShowCrystalReportsInIframe"];
                if (!string.IsNullOrEmpty(ShowCrystalReportsInIframe) && ShowCrystalReportsInIframe == "0")
                {
                    ConfigureCrystalReports(ExportFormatType.PortableDocFormat, true);
                }
                else
                {
                    ConfigureCrystalReports();
                }
            }
            catch (Exception ex)
            {
                LogError("Page_Init", ex);
                ShowMessage(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Headers.Remove("Content-Security-Policy");
        }

        protected void excelBtn_Click(object sender, EventArgs e)
        {
            ConfigureCrystalReports(ExportFormatType.ExcelRecord, false);
        }

        protected void pdfBtn_Click(object sender, EventArgs e)
        {
            ConfigureCrystalReports(ExportFormatType.PortableDocFormat, false);
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (rptDoc != null)
            {
                rptDoc.Dispose();
                rptDoc.Close();
            }
        }

        private void ConfigureCrystalReports()
        {
            try
            {
                if (Session["rptSource"] != null && Session["ReportName"] != null)
                {
                    string strReportName = Session["ReportName"].ToString();
                    var rptSource = Session["rptSource"];

                    if (string.IsNullOrEmpty(strReportName))
                    {
                        Response.Write("<H2>Error: Report name is empty.</H2>");
                        return;
                    }

                    rptDoc = new ReportDocument();
                    string strRptPath = Path.Combine(Server.MapPath("~/Reports"), strReportName);

                    if (!File.Exists(strRptPath))
                    {
                        throw new FileNotFoundException("Report file not found at " + strRptPath);
                    }

                    rptDoc.Load(strRptPath);

                    if (rptSource != null)
                    {
                        if (rptSource is DataSet ds && ds.Tables.Count > 0)
                        {
                            rptDoc.SetDataSource(ds.Tables[0]);
                        }
                        else
                        {
                            rptDoc.SetDataSource(rptSource);
                        }
                    }

                    CrystalReportViewer.ReportSource = rptDoc;
                    CrystalReportViewer.ToolPanelView = ToolPanelViewType.None;
                    CrystalReportViewer.DataBind();
                }
                else
                {
                    Response.Redirect("~/Account/Login", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                LogError("ConfigureCrystalReports", ex);
                ShowMessage(ex);
            }
        }

        public void ConfigureCrystalReports(ExportFormatType formatType, bool showPdfInIframe)
        {
            try
            {
                if (Session["rptSource"] != null && Session["ReportName1"] != null)
                {
                    DataSet rptSource = Session["rptSource"] as DataSet;
                    string rptPath = Session["ReportName1"].ToString();

                    if (string.IsNullOrEmpty(rptPath) || rptSource == null)
                    {
                        throw new Exception("Session data (rptSource or ReportName1) is missing.");
                    }

                    rptDoc = new ReportDocument();
                    rptDoc.Load(rptPath);
                    rptDoc.SetDataSource(rptSource);

                    if (showPdfInIframe)
                    {
                        using (var stream = rptDoc.ExportToStream(ExportFormatType.PortableDocFormat))
                        {
                            Response.Clear();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("Content-Disposition", "inline; filename=Report.pdf");
                            stream.CopyTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                    else
                    {
                        string filename = Path.GetFileNameWithoutExtension(rptPath);
                        rptDoc.ExportToHttpResponse(formatType, Response, true, filename);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("ConfigureCrystalReports (Export)", ex);
                ShowMessage(ex);
            }
        }

        private void LogError(string method, Exception ex)
        {
            try
            {
                string logPath = Server.MapPath("~/debug_log.txt");
                string message = string.Format("\n[{0}] Error in {1}:\n{2}\n", DateTime.Now, method, ex.ToString());
                File.AppendAllText(logPath, message);
            }
            catch { }
        }

        public void ShowMessage(Exception ex)
        {
            string cleanMessage = ex.Message.Replace("'", "\\'").Replace("\r", " ").Replace("\n", " ");
            string script = string.Format("window.onload=function(){{ alert('{0}'); }};", cleanMessage);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script, true);
        }
    }
}