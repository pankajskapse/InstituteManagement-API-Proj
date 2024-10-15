<%@ Page Title="Course" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" AutoEventWireup="true" CodeBehind="coursePage.aspx.cs" Inherits="InstituteManagement.Admin.coursePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 940px;
        }

        .auto-style2 {
            width: 9px;
        }

        .auto-style3 {
            width: 11px;
        }

        .auto-style4 {
            width: 13px;
        }

        .auto-style5 {
            position: relative;
            width: 1453%;
            -ms-flex-preferred-size: 0;
            flex-basis: 0;
            -ms-flex-positive: 1;
            flex-grow: 1;
            max-width: 100%;
            min-height: 1px;
            -webkit-box-flex: 1;
            left: 2px;
            top: 0px;
            padding-left: 10px;
            padding-right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:Label ID="Lab_message" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="row divgapBetweenControlButton">
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6">
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="labCourse" runat="server" Text="Course Code"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:TextBox ID="txt_Course" runat="server" Width="100%"></asp:TextBox>
                        <asp:TextBox ID="txtCourseKeyForEditMode" runat="server" Visible="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="labCourseDesc" runat="server" Text="Course Desc."></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:TextBox ID="txt_CourseDesc" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="Label1" runat="server" Text="Created By:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:Label ID="lab_CreatedByText" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lab_Createdon" runat="server" Text="Created On:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:Label ID="lab_CreatedOnText" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="label3" runat="server" Text="Modified By:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:Label ID="lab_ModifiedByText" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="Label4" runat="server" Text="Modified On:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:Label ID="lab_ModifiedOnText" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6" >
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="Label2" runat="server" Text="Course Code"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-4">
                        <asp:TextBox ID="txtCourseCodeSearch" runat="server" Width="100%"></asp:TextBox>
                    </div>
                    <div class="col-xs-5 col-sm-6 col-md-4 col-lg-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                     <div class="col-xs-5 col-sm-6 col-md-4 col-lg-2">
                        <asp:Button ID="btnClearSearch" runat="server" Text="Clear Search" OnClick="btnClearSearch_Click" />
                    </div>
                </div>
                <div class="row divBottomMargin" style="overflow: auto">
                    <asp:GridView ID="grdCourse" runat="server" ForeColor="Black" BorderStyle="solid"
                        CssClass="auto-style5" BorderColor="#999999" BackColor="White" GridLines="Both" CellPadding="4" Height="100px"
                        BorderWidth="1px" AutoGenerateColumns="false" AllowSorting="True" CellSpacing="0" AllowPaging="true" AutoGenerateSelectButton="true"
                        PageSize="7" OnPageIndexChanging="grdCourse_PageIndexChanging" ViewStateMode="Enabled"
                        EnablePersistedSelection="True" DataKeyNames="courseKey" OnSelectedIndexChanged="grdCourse_SelectedIndexChanged">

                        <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                        <RowStyle BackColor="LightGray" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <%-- <RowStyle HorizontalAlign="Center" BorderColor="#CCCCCC" />--%>
                        <Columns>

                            <%--<asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />--%>
                            <asp:BoundField DataField="courseKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />

                            <asp:BoundField DataField="courseCode" HeaderText="Course Code" />
                            <asp:BoundField DataField="courseDesc" HeaderText="Course Description" />
                            <asp:BoundField DataField="CreatedOn" HeaderText="Created On" DataFormatString="{0:dd/MMM/yyyy}" />
                            <asp:BoundField DataField="CreatedByEmpName" HeaderText="Created Emp" />
                            <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On" DataFormatString="{0:dd/MMM/yyyy}" />
                            <asp:BoundField DataField="ModifiedByEmpName" HeaderText="Modified Emp" />

                        </Columns>
                        <SelectedRowStyle BackColor="DodgerBlue" ForeColor="White" Font-Bold="True"></SelectedRowStyle>
                        <PagerStyle BackColor="White" ForeColor="Blue" HorizontalAlign="Left"
                            Font-Size="8pt" VerticalAlign="Middle" Font-Names="Verdana"></PagerStyle>
                        <HeaderStyle HorizontalAlign="Center" BackColor="chocolate" ForeColor="White" Font-Bold="True"
                            CssClass="headerstyle"></HeaderStyle>
                        <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                        <PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="100" Position="TopAndBottom"
                            FirstPageText="First Page" LastPageText="Last Page" />
                        <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-6 col-md-4 col-lg-2">
                        <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row divBottomMargin divTopMargin">
            <div class="col-xs-6 col-sm-6 col-md-3 col-lg-2">
                <asp:Button ID="btn_Add" runat="server" OnClick="btn_Add_Click" Text="Add New" Width="100px" />
            </div>
            <div class="col-xs-6 col-sm-6 col-md-3 col-lg-2">

                <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Save" Width="100px" OnClientClick="return validate();" />
            </div>
            <div class="col-xs-6 col-sm-6 col-md-3 col-lg-2">
                <asp:Button ID="btnEdit" runat="server" Text="Update" Width="100px" OnClick="btnEdit_Click" /><%--OnClick="btn_EnquirySave_Click"--%>
            </div>
            <div class="col-xs-6 col-sm-6 col-md-3 col-lg-2">
                <asp:Button ID="btn_Delete" runat="server" Text="Delete" Width="100px" OnClick="btn_Delete_Click" />
            </div>
        </div>



    </div>
</asp:Content>
