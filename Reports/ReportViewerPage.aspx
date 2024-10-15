<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewerPage.aspx.cs" Inherits="InstituteManagement.Reports.ReportViewerPage" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- Tell the browser to be responsive to screen width -->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/MainCss.css" rel="stylesheet" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" />
    <link href="../Styles/jquery-ui.min.css" rel="stylesheet" />
    <link href="../Scripts/style.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/jquery-3.3.1.min.js"></script>
  

    <script src="../js/jquery-ui.min.js"></script>
    <script src="../js/waves.js"></script>
    <!--Menu sidebar -->
    <script src="../js/sidebarmenu.js"></script>
    <!--Custom JavaScript -->
    <script src="../js/custom.min.js"></script>
    <!--This page JavaScript -->
    <!-- <script src="../../dist/js/pages/dashboards/dashboard1.js"></script> -->
    <!-- Charts js Files -->
   
    <%--<script src="../js/pages/chart/chart-page-init.js"></script>--%>
    <link href="../assets/libs/flot/css/float-chart.css" rel="stylesheet" />
    <script src="../crystalreportviewers13/js/crviewer/crv.js"></script>

</head>


<body>
    <form id="form1" runat="server">
        <div class="row divBottomMargin">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                    Height="800" Width="100%" BestFitPage="False" HasExportButton="True" HasPrintButton="True" HasPageNavigationButtons="True" />
            </div>
        </div>
        
    </form>
</body>
</html>
