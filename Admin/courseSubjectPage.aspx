<%@ Page Title="Subject" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" AutoEventWireup="true" CodeBehind="courseSubjectPage.aspx.cs" Inherits="InstituteManagement.Admin.courseSubjectPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:Label ID="Lab_message" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6">
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-5">
                        <asp:Label ID="labCourse" runat="server" Text="Course Code:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-7">
                        <asp:DropDownList runat="server" ID="CourseDDL" DataTextField="courseCode" DataValueField="courseKey" Width="100%"
                            OnSelectedIndexChanged="CourseDDL_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-5">
                        <asp:Label ID="labSubCourse" runat="server" Text="Sub-Course Code:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-7">
                        <asp:DropDownList runat="server" ID="SubCourseDDL" DataValueField="subcourseKey" DataTextField="subcourseCode" Width="100%"
                            OnSelectedIndexChanged="SubCourseDDL_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-5">
                        <asp:Label ID="labGroup" runat="server" Text="Group"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-7">
                        <asp:DropDownList runat="server" ID="GroupDDL" DataValueField="groupsubcourseKey" DataTextField="groupsubcoursecode" Width="100%">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-5">
                        <asp:Label ID="labSubjectCourse" runat="server" Text="Subject Code:*"></asp:Label>
                        <asp:TextBox ID="txtSubjectKeyForEditMode" runat="server" Visible="false"></asp:TextBox>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-7">
                        <asp:TextBox ID="txt_SubjectCourse" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-5">
                        <asp:Label ID="labSubjectCourseDesc" runat="server" Text="Subject Desc.:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-7">
                        <asp:TextBox ID="txt_SubjectCourseDesc" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-5">
                        <asp:Label ID="lblFees" runat="server" Text="Subject Fees"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-7">
                        <%--<asp:TextBox ID="txtSubjectFee" runat="server" Width="100%" TextMode="Number" Text="0"></asp:TextBox>--%>
                        <input id="txtSubjectFee" runat="server" type="number" step=".01" style="width: 100%" />
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-5">
                        <asp:Label ID="lab_CreatedBy" runat="server" Text="Created By:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-7">
                        <asp:Label ID="lab_CreatedByText" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-5">
                        <asp:Label ID="label3" runat="server" Text="Modified By:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-7">
                        <asp:Label ID="lab_ModifiedByText" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-5">
                        <asp:Label ID="Label2" runat="server" Text="Created On:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-7">
                        <asp:Label ID="lab_CreatedOnText" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-5">
                        <asp:Label ID="Label4" runat="server" Text="Modified On:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-7">
                        <asp:Label ID="lab_ModifiedOnText" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6">
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="Label1" runat="server" Text="Subject Code"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-4">
                        <asp:TextBox ID="txtSubjectCodeSearch" runat="server" Width="100%"></asp:TextBox>
                    </div>
                    <div class="col-xs-5 col-sm-6 col-md-4 col-lg-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                    <div class="col-xs-5 col-sm-6 col-md-4 col-lg-2">
                        <asp:Button ID="btnClearSearch" runat="server" Text="Clear Search" OnClick="btnClearSearch_Click" />
                    </div>
                </div>
                <div class="row divBottomMargin" style="overflow: auto">
                    <asp:GridView ID="grdSubject" runat="server" ForeColor="Black" BorderStyle="solid" Width="100%"
                        CssClass="col" BorderColor="#999999" BackColor="White" GridLines="Both" CellPadding="4" Height="100px"
                        BorderWidth="1px" AllowSorting="True" CellSpacing="0" AllowPaging="true" PageSize="4" AutoGenerateColumns="false"
                        EnablePersistedSelection="True" DataKeyNames="subjectKey, groupsubcourseKey" ViewStateMode="Enabled" AutoGenerateSelectButton="true"
                        OnSelectedIndexChanged="grdSubject_SelectedIndexChanged" OnPageIndexChanging="grdSubject_PageIndexChanging">

                        <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                        <RowStyle BackColor="LightGray" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <%-- <RowStyle HorizontalAlign="Center" BorderColor="#CCCCCC" />--%>
                        <Columns>

                            <%--<asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />--%>
                            <asp:BoundField DataField="subjectKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />

                            <asp:BoundField DataField="subjectCode" HeaderText="Subject Code" />
                            <asp:BoundField DataField="subjectDesc" HeaderText="Subject Desc" />
                            <asp:BoundField DataField="courseKey" HeaderText="courseKey" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                            <asp:BoundField DataField="courseCode" HeaderText="course Code" />
                            <asp:BoundField DataField="subcourseKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                            <asp:BoundField DataField="subcourseCode" HeaderText="SubCourse Code" />
                            <asp:BoundField DataField="groupsubcourseKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                            <asp:BoundField DataField="groupsubcourseCode" HeaderText="Group SubCourse Code" />
                            <asp:BoundField DataField="subjectFees" HeaderText="Fees" />
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
                        <PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="100" Position="TopAndBottom" FirstPageText="First Page" LastPageText="Last Page" />
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
            <div class="col-xs-6 col-sm-12 col-md-3 col-lg-2">
                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add New" Width="100px" />
            </div>
            <div class="col-xs-6 col-sm-12 col-md-3 col-lg-2">

                <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Save" Width="100px" OnClientClick="return validate();" />
            </div>
            <div class="col-xs-6 col-sm-6 col-md-3 col-lg-2">
                <asp:Button ID="btnEdit" runat="server" Text="Update" Width="100px" OnClick="btnEdit_Click" /><%--OnClick="btn_EnquirySave_Click"--%>
            </div>
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-2">
                <asp:Button ID="btn_Delete" runat="server" Text="Delete" Width="100px" OnClick="btn_Delete_Click" />
            </div>
        </div>



    </div>
</asp:Content>
