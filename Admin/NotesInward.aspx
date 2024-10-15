<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" AutoEventWireup="true" CodeBehind="NotesInward.aspx.cs" Inherits="InstituteManagement.Admin.NotesInward" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblSubCourse" runat="server" Text="Subcourse:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-9">
                        <asp:RadioButtonList ID="rdoSubCourseList" runat="server" DataTextField="subcourseCode" DataValueField="subcourseKey" RepeatColumns="4" RepeatLayout="Table"
                            CssClass="test" OnSelectedIndexChanged="rdoSubCourseList_SelectedIndexChanged" AutoPostBack="true" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row divBottomMargin" runat="server" id="divGroupList">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblGroup" runat="server" Text="Select Group:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-9">
                        <asp:RadioButtonList ID="rdoGroupList" runat="server" DataTextField="groupsubcourseCode" DataValueField="groupsubcourseKey" RepeatColumns="4" RepeatLayout="Table"
                            CssClass="test" OnSelectedIndexChanged="rdoGroupList_SelectedIndexChanged" AutoPostBack="true" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                        </asp:RadioButtonList>
                    </div>
                </div>

</asp:Content>
