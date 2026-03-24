using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using System.IO;
using CrystalDecisions.Web;

namespace App.Web.ReportForms
{
    public partial class TransactionMonitoring1 : System.Web.UI.Page
    {
        ReportDocument rd;

        protected void Page_Init(object sender, EventArgs e)
        {
            ConfigureCrystalReports();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Remove CSP header if necessary for Crystal Reports viewer
            Response.Headers.Remove("Content-Security-Policy");
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (rd != null)
            {
                rd.Close();
                rd.Dispose();
            }
        }

        private void ConfigureCrystalReports()
        {
            try
            {
                if (Session["rptSource"] != null && Session["ReportName"] != null)
                {
                    string strReportName = Session["ReportName"].ToString();
                    DataSet ds = Session["rptSource"] as DataSet;

                    if (ds != null && !string.IsNullOrEmpty(strReportName))
                    {
                        // Bind Crystal Report
                        rd = new ReportDocument();
                        string strRptPath = Server.MapPath("~/Reports/" + strReportName);
                        rd.Load(strRptPath);
                        rd.SetDataSource(ds);
                        
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
                        CrystalReportViewer1.DisplayGroupTree = false;
                        rd.Refresh();
                        CrystalReportViewer1.RefreshReport();
                    }
                }
                else
                {
                    Response.Write("<h3>Report data session expired or not found. Please try again from the main page.</h3>");
                }
            }
            catch (Exception ex)
            {
                lblMessage("Error configuring report: " + ex.Message);
            }
        }


        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportReport(ExportFormatType.ExcelRecord, "TransactionMonitoringReport.xls");
        }

        protected void btnExportPdf_Click(object sender, EventArgs e)
        {
            ExportReport(ExportFormatType.PortableDocFormat, "TransactionMonitoringReport.pdf");
        }

        private void ExportReport(ExportFormatType formatType, string fileName)
        {
            try
            {
                if (Session["rptSource"] != null && Session["ReportName"] != null)
                {
                    string strReportName = Session["ReportName"].ToString();
                    DataSet ds = Session["rptSource"] as DataSet;

                    using (ReportDocument exportRd = new ReportDocument())
                    {
                        string strRptPath = Server.MapPath("~/Reports/" + strReportName);
                        exportRd.Load(strRptPath);
                        exportRd.SetDataSource(ds);

                        exportRd.ExportToHttpResponse(formatType, Response, true, Path.GetFileNameWithoutExtension(fileName));
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage(ex.Message);
            }
        }

        private void lblMessage(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message.Replace("'", "\\'") + "');", true);
        }
    }
}
