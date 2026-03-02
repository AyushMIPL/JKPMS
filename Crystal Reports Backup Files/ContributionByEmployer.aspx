<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContributionByEmployer.aspx.cs" Inherits="App.Web.ReportForms.ContributionByEmployer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%--<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>--%>
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
    <script type="text/javascript">        var popupWindow = null;        function Print() {            //var dvReport = document.getElementById("dvReport");            //var frame1 = dvReport.getElementsByTagName("iframe")[0];            //if (navigator.appName.indexOf("Internet Explorer") != -1 || navigator.appVersion.indexOf("Trident") != -1) {            //  frame1.name = frame1.id;            //  window.frames[frame1.id].focus();            //  window.frames[frame1.id].print();            //}            //else {            //  var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;            //  frameDoc.print();            //}            var url = '/Reports/frmCRReportViwerPrint.aspx';            var w = 700;            var h = 500;            var scroll = 'yes';            LeftPosition = (screen.width) ? (screen.width - w) / 2 : 0;            TopPosition = (screen.height) ? (screen.height - h) / 2 : 0;            settings = 'height=' + h + ',width=' + w + ',top=' + TopPosition + ',left=' + LeftPosition + ',scrollbars=' + scroll + ',resizable';            //popupWindow = window.open(url, "PrintWindow", settings);            popupWindow = window.open(url, "_blank");        }        $(document).ready(function () {            if ('<%= Session["ReportName"] %>' == 'rptBeneficiarypayment.rpt') {                $('#excelBtn').hide();                $('#openExcelBtn').show();            }            else {                $('#openExcelBtn').hide();                $('#excelBtn').show();            }            /*$('#dvReport').css('height', $('#CRReportViewer__UI').height() + 'px');*/            //var frame1 = dvReport.getElementsByTagName("iframe")[0];            //setIframeHeightDynamic(frame1);            function downloadExcel() {                try {                    $.ajax({                        type: "POST",                        data: { isDownload: true },                        url: '/PensionProcess/BeneficiaryPaymentDetailsAjaxHandler', //'@Url.Action("BeneficiaryPaymentDetailsAjaxHandler", "PensionProcess")',                        cache: false,                        success: function (result) {                            if (result != null) {                                var bytes = new Uint8Array(result.FileContents);                                var blob = new Blob([bytes], { type: '@System.Net.Mime.MediaTypeNames.Application.Octet' });                                if (window.navigator.msSaveOrOpenBlob) {                                    // IE11                                    window.navigator.msSaveOrOpenBlob(blob, result.FileDownloadName);                                } else {                                    // Google chome, Firefox, ....                                    var link = document.createElement('a');                                    link.href = window.URL.createObjectURL(blob);                                    link.download = result.FileDownloadName;                                    link.click();                                }                            }                            else {                            }                        },                        error: function (xhr, status, error) {                        }                    });                }                catch (e) {                }            };            $('#openExcelBtn').on('click', function () {                downloadExcel();            });        });        function DownloadExcelAjax() {            debugger
            var xhr = new XMLHttpRequest();
            xhr.open('Get', '/ReportForms/ContributionByEmployer.aspx/CustomExcelExportAjax', true);
            xhr.responseType = 'arraybuffer';

            xhr.onload = function () {
                debugger
                if (xhr.status === 200) {
                    debugger
                    var blob = new Blob([xhr.response], { type: 'application/octet-stream' });
                    var url = window.URL.createObjectURL(blob);
                    var a = document.createElement('a');
                    a.href = url;
                    a.download = 'Beneficiary.xlsx'; // Default filename

                    var disposition = xhr.getResponseHeader('Content-Disposition');
                    if (disposition) {
                        var matches = /filename="([^"]*)"/.exec(disposition);
                        if (matches != null && matches[1]) {
                            a.download = matches[1];
                        }
                    }

                    document.body.appendChild(a);
                    a.click();

                    window.URL.revokeObjectURL(url);
                    document.body.removeChild(a);
                } else {
                    console.error('Error downloading file:', xhr.statusText);
                }
            };

            xhr.onerror = function () {
                debugger
                console.error('Network error.');
            };

            xhr.send();
        }        function setIframeHeightDynamic(iframe) {            if (iframe) {                var iframeWin = iframe.contentWindow || iframe.contentDocument.parentWindow;                if (iframeWin.document.body) {                    iframe.height = 17 + iframeWin.document.documentElement.scrollHeight || iframeWin.document.body.scrollHeight;                    iframe.css('height', iframe.height + 'px');                    alert(iframe.height);                }            }        };    </script>
</head>
<body>
    <form id="ContributionByEmployer" runat="server">
        <div class="form-horizontal">
            <div class="row">
                <div class="col-md-12">

                    <div class="btn-group btn-group-toggle pull-left" data-toggle="buttons">
                        <asp:Button ID="openExcelBtn" runat="server" CssClass="btn btn-danger reportBtn" Text="Export to Excel" OnClientClick="return false;" />
                        <asp:Button runat="server" ID="excelBtn" CssClass="btn btn-danger reportBtn" Text="Export to Excel" OnClick="excelBtn_Click" />
                        <%--<button id="excelBtnAjax" class="btn btn-danger reportBtn" onclick="DownloadExcelAjax()">Export to Excel Aj</button>--%>
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
