using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.Web.ReportForms
{
    public partial class ContributionsByContributors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void Page_Init(object sender, EventArgs e)
        {

            ConfigureCrystalReports();

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

                    if (string.IsNullOrEmpty(strReportName))
                    {
                        isValid = false;
                    }


                    if (isValid)
                    {
                        ReportDocument rd = new ReportDocument();
                        string strRptPath = Server.MapPath("~/") + "Reports\\" + strReportName;
                        //~/ReportForms/StandardReport.aspx
                        rd.Load(strRptPath);

                        if (rptSource != null && rptSource.GetType().ToString() != "System.String")
                            rd.SetDataSource(rptSource);

                        CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
                        //CrystalReportViewer1.Width = 1250;
                        //CrystalReportViewer1.Zoom(100);
                        //CrystalReportViewer1.AutoDataBind = true;
                        //CrystalReportViewer1.BestFitPage = false;
                        //CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.Pdf;
                        //CrystalReportViewer1.RefreshReport();
                        CrystalReportViewer1.ReportSource = rd;
                        CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;

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
    }
}