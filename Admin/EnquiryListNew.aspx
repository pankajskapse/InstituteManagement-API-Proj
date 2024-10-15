<%@ Page Title="Enquiry List" Language="C#" MasterPageFile="~/MasterPage/BlankMaster.Master" AutoEventWireup="true" CodeBehind="EnquiryListNew.aspx.cs" 
    Inherits="InstituteManagement.Admin.EnquiryListNew" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row divBottomMargin">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:Label ID="lab_message" runat="server" ForeColor="Red" Text=""></asp:Label>
            </div>
        </div>
        <div class="row divBottomMargin">
            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-3">
                <asp:CheckBox ID="chkFromToDateFilter" runat="server" Text="Enquiry From Date-To" OnCheckedChanged="chkFromToDateFilter_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
            </div>
            <%-- </div>
        <div class="row divBottomMargin">--%>
            <div class="col-xs-12 col-sm-12 col-md-8 col-lg-9">
                <asp:Panel ID="pnlFromToDate" runat="server" BorderStyle="Groove">
                    <div class="row divBottomMargin">
                        <div class="col-xs-6 col-sm-2 col-md-2 col-lg-1">
                            <asp:Label ID="lblFrom" runat="server" Text="From:"></asp:Label>
                        </div>
                        <div class="col-xs-6 col-sm-4 col-md-4 col-lg-3">
                            <asp:TextBox ID="txtFromDate" runat="server" Width="100%" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-xs-6 col-sm-2 col-md-2 col-lg-1">
                            <asp:Label ID="lblFromTo" runat="server" Text="To:"></asp:Label>
                        </div>
                        <div class="col-xs-6 col-sm-4 col-md-4 col-lg-3">
                            <asp:TextBox ID="txtToDate" runat="server" Width="100%" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>

        <div class="row divBottomMargin">
            <div class="col-xs-5 col-sm-6 col-md-4 col-lg-3">
                <asp:CheckBox ID="chkCourse" runat="server" Text="Course Wise" OnCheckedChanged="chkCourse_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
            </div>

            <div class="col-xs-5 col-sm-6 col-md-8 col-lg-3">
                <asp:DropDownList ID="ddlCourse" runat="server" DataTextField="courseCode" DataValueField="courseKey" Width="100%"
                    OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="col-xs-5 col-sm-6 col-md-4 col-lg-2">
                <asp:CheckBox ID="chkSubCourse" runat="server" Text="Sub-Course Wise" OnCheckedChanged="chkSubCourse_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
            </div>

            <div class="col-xs-5 col-sm-6 col-md-8 col-lg-4">
                <asp:DropDownList ID="ddlSubCourse" runat="server" DataTextField="subcourseCode" DataValueField="subcourseKey" Width="100%"
                    OnSelectedIndexChanged="ddlSubCourse_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </div>
        </div>

        <div class="row divBottomMargin" id="divGroupList" runat="server">
            <div class="col-xs-5 col-sm-6 col-md-4 col-lg-3">
                <asp:CheckBox ID="chkGroup" runat="server" Text="Group Wise" OnCheckedChanged="chkGroup_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
            </div>

            <div class="col-xs-5 col-sm-6 col-md-8 col-lg-3">
                <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="groupsubcourseCode" DataValueField="groupsubcourseKey" Width="100%"
                    OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="col-xs-5 col-sm-6 col-md-4 col-lg-2">
                <asp:CheckBox ID="chkSubject" runat="server" Text="Subject Wise" OnCheckedChanged="chkSubject_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
            </div>

            <div class="col-xs-5 col-sm-6 col-md-8 col-lg-4">
                <asp:DropDownList ID="ddlSubject" runat="server" DataTextField="subjectCode" DataValueField="subjectKey" Width="100%">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row divBottomMargin">
            <div class="col-xs-5 col-sm-6 col-md-4 col-lg-3">
                <asp:CheckBox ID="chkBatchWise" runat="server" Text="Batch Wise" OnCheckedChanged="chkBatchWise_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
            </div>

            <div class="col-xs-5 col-sm-6 col-md-8 col-lg-4">
                <asp:DropDownList ID="ddlBatchList" runat="server" DataTextField="batchDesc" DataValueField="batchKey" Width="100%">
                </asp:DropDownList>
            </div>
            <div class="col-xs-5 col-sm-6 col-md-8 col-lg-4">
                <asp:CheckBox ID="chkClosed" runat="server" Text="Show Closed Enquiry Too"></asp:CheckBox>

            </div>

        </div>
        <div class="row divBottomMargin">
            <div class="col-xs-5 col-sm-6 col-md-4 col-lg-2">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </div>
            <div class="col-xs-5 col-sm-6 col-md-4 col-lg-2">
                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click"/>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-6 col-sm-12 col-md-12 col-lg-12">
                <%--<div class="row divBottomMargin">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">--%>
                <asp:GridView ID="grdEnquiryList" runat="server" ForeColor="Black" BorderStyle="solid" Width="100%"
                    CssClass="col" BorderColor="#999999" BackColor="White" GridLines="Both" CellPadding="4" Height="100px"
                    BorderWidth="1px" AutoGenerateColumns="false" AllowSorting="True" CellSpacing="0" AllowPaging="true" PageSize="8"
                    EnablePersistedSelection="True" DataKeyNames="enquiryKey" ViewStateMode="Enabled" AutoGenerateSelectButton="true"
                    OnSelectedIndexChanged="grdEnquiryList_SelectedIndexChanged" OnPageIndexChanging="grdEnquiryList_PageIndexChanging">

                    <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                    <RowStyle BackColor="LightGray" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <%-- <RowStyle HorizontalAlign="Center" BorderColor="#CCCCCC" />--%>
                    <Columns>

                        <asp:BoundField DataField="enquiryKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />

                        <asp:BoundField DataField="enquiryCode" HeaderText="Enquiry Code" />
                        <asp:BoundField DataField="fName" HeaderText="First Name" />
                        <asp:BoundField DataField="lName" HeaderText="Last Name" />
                        <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                        <asp:BoundField DataField="enquiryDate" HeaderText="Enquiry Date" DataFormatString="{0:dd/MMM/yyyy}" />
                        <asp:BoundField DataField="emailID" HeaderText="Email" />
                        <asp:BoundField DataField="gender" HeaderText="Gender" />
                        <asp:BoundField DataField="collegeName" HeaderText="college Name" />
                        <asp:BoundField DataField="CreatedOn" HeaderText="Created On" DataFormatString="{0:dd/MMM/yyyy}" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On" DataFormatString="{0:dd/MMM/yyyy}" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="enquiryTypeKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="batchKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="Notes" HeaderText="Remark" />
                        <asp:BoundField DataField="EstimatedFees" HeaderText="EstimatedFees" DataFormatString="{0:n}"/>
                        <asp:BoundField DataField="FinalFees" HeaderText="FinalFees" DataFormatString="{0:n}"/>

                        <asp:BoundField DataField="courseCode" HeaderText="Course Code" />
                        <asp:BoundField DataField="subcourseCode" HeaderText="sub-course Code" />
                        <asp:BoundField DataField="groupsubcourseCode" HeaderText="Group Code" />

                        <asp:BoundField DataField="refByName" HeaderText="Refer By Name" />
                        <asp:BoundField DataField="refByPhoneNo" HeaderText="Refer By Phone No" />
                        <asp:BoundField DataField="altPhoneNo" HeaderText="Alternate Phone No" />

                    </Columns>
                    <SelectedRowStyle BackColor="DodgerBlue" ForeColor="White" Font-Bold="True"></SelectedRowStyle>
                    <PagerStyle BackColor="White" ForeColor="Blue" HorizontalAlign="Left" Width="180px"
                        Font-Size="8pt" VerticalAlign="Middle" Font-Names="Verdana"></PagerStyle>
                    <HeaderStyle HorizontalAlign="Center" BackColor="chocolate" ForeColor="White" Font-Bold="True"
                        CssClass="headerstyle"></HeaderStyle>
                    <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                    <PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="100" Position="TopAndBottom"
                        FirstPageText="First Page" LastPageText="Last Page" />
                    <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                </asp:GridView>
            </div>

        </div>
        <div class="row">
            <div class="col-xs-6 col-sm-6 col-md-4 col-lg-2">
                <asp:Label ID="lblTotalCount" runat="server" Text="Total Records"></asp:Label>
            </div>
            <div class="col-xs-6 col-sm-6 col-md-4 col-lg-2">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <%--</div>
</div>--%>
</asp:Content>
