<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionMonitoring1.aspx.cs" Inherits="App.Web.ReportForms.TransactionMonitoring" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Transaction Monitoring Report</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/endless/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.7.1.min.js"></script>
    <style type="text/css">
        html, body, form {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
        }
        .report-toolbar {
            padding: 15px;
            background-color: #fff;
            border-bottom: 1px solid #e0e0e0;
            margin-bottom: 20px;
        }
        .report-container {
            padding: 0 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-horizontal">
            <div class="report-toolbar">
                <div class="row">
                    <div class="col-md-12">
                        <div class="btn-group pull-left">
                            <asp:Button runat="server" ID="btnExportExcel" CssClass="btn btn-danger" Text="Export to Excel" OnClick="btnExportExcel_Click" />
                            <asp:Button runat="server" ID="btnExportPdf" CssClass="btn btn-success" Text="Export to PDF" OnClick="btnExportPdf_Click" />
                        </div>
                    </div>
                </div>
            </div>


            <div class="report-container">
                <div id="dvReport">
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                        SeparatePages="True" 
                        AutoDataBind="true" 
                        BestFitPage="False" 
                        HasRefreshButton="True" 
                        Width="100%" 
                        Height="100%"
                        ToolPanelView="None" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
