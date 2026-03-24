<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionMonitoring.aspx.cs" Inherits="App.Web.ReportForms.TransactionMonitoring" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/endless/js/jquery-ui-map/demos/css/style.css" rel="stylesheet" />
    <link href="~/Content/endless/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.7.1.min.js"></script>
    <style type="text/css">
        html, body, form {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0
        }
    </style>
    <script type="text/javascript">
        var popupWindow = null;
        function Print() {
            var url = '/Reports/frmCRReportViwerPrint.aspx';
            var w = 700;
            var h = 500;
            var scroll = 'yes';
            LeftPosition = (screen.width) ? (screen.width - w) / 2 : 0;
            TopPosition = (screen.height) ? (screen.height - h) / 2 : 0;
            settings = 'height=' + h + ',width=' + w + ',top=' + TopPosition + ',left=' + LeftPosition + ',scrollbars=' + scroll + ',resizable';
            popupWindow = window.open(url, "_blank");
        }
        $(document).ready(function () {
            if ('<%= Session["ReportName"] %>' == 'rptBeneficiarypayment.rpt') {
                $('#excelBtn').hide();
                $('#openExcelBtn').show();
            }
            else {
                $('#openExcelBtn').hide();
                $('#excelBtn').show();
            }
        });
    </script>
</head>
<body>
    <form id="TransactionMonitoringForm" runat="server">
        <div class="form-horizontal">
            <div class="row">
                <div class="col-md-12">
                    <div class="btn-group btn-group-toggle pull-left" data-toggle="buttons">
                        <asp:Button ID="openExcelBtn" runat="server" CssClass="btn btn-danger reportBtn" Text="Export to Excel" OnClientClick="return false;" style="display:none;" />
                        <asp:Button runat="server" ID="excelBtn" CssClass="btn btn-danger reportBtn" Text="Export to Excel" OnClick="excelBtn_Click" />
                        <asp:Button runat="server" ID="pdfBtn" CssClass="btn btn-success reportBtn" Text="Export to PDF" OnClick="pdfBtn_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div id="dvReport">
            <CR:CrystalReportViewer ID="CrystalReportViewer" runat="server" SeparatePages="True" AutoDataBind="true" BestFitPage="False" HasRefreshButton="True" />
        </div>
    </form>
</body>
</html>