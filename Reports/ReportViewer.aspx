<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="InstituteManagement.Reports.ReportViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container-fluid">
    <div class="row divBottomMargin">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                    Height="800" Width="100%" BestFitPage="False" HasExportButton="True" HasPrintButton="True" HasPageNavigationButtons="True" />
            </div>
        </div>
         </div>
</asp:Content>
