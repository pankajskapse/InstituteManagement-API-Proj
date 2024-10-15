<%@ Page Title="Faculty" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" AutoEventWireup="true" CodeBehind="FacultyPage.aspx.cs" Inherits="InstituteManagement.Admin.FacultyPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function validate() {
            //if validation sucess return true otherwise return false.
            if (document.getElementById("txt_FacultyCode").value != "") {
                return true;
                alert('Faculty code is compulsory field.');
            }

            else if (document.getElementById("txt_FacultyDesc").value != "") {
                return true;
                alert('Faculty Description is compulsory field.');
            }

            return false;
        }
    </script>

    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:Label ID="Lab_message" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6">
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="LabelFacultyCode" runat="server" Text="Faculty Code:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:TextBox ID="txt_FacultyCode" runat="server" Width="60%"></asp:TextBox>
                        <asp:TextBox ID="txtFacultyKeyForEditMode" runat="server" Visible="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="LabelFacultyDesc" runat="server" Text="Faculty Desc.:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:TextBox ID="txt_FacultyDesc" runat="server" Width="80%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblCourse" runat="server" Text="Select Course:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-9">
                        <asp:RadioButtonList ID="rdoListCourse" runat="server" DataTextField="courseCode" DataValueField="courseKey" RepeatColumns="4" RepeatLayout="Table"
                            CssClass="test" OnSelectedIndexChanged="rdoListCourse_SelectedIndexChanged" AutoPostBack="true" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblSubCourse" runat="server" Text="Subcourse/Subject:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-9">
                        <asp:RadioButtonList ID="rdoSubCourseList" runat="server" DataTextField="subcourseCode" DataValueField="subcourseKey" RepeatColumns="3" RepeatLayout="Table"
                            CssClass="test" OnSelectedIndexChanged="rdoSubCourseList_SelectedIndexChanged" AutoPostBack="true" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row divBottomMargin" runat="server" id="divGroupList">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblGroup" runat="server" Text="Select Group:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-9">
                        <asp:RadioButtonList ID="rdoGroupList" runat="server" DataTextField="groupsubcourseCode" DataValueField="groupsubcourseKey" RepeatColumns="3" RepeatLayout="Table"
                            CssClass="test" OnSelectedIndexChanged="rdoGroupList_SelectedIndexChanged" AutoPostBack="true" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="LabelSubject" runat="server" Text="Subject:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:DropDownList ID="DDL_Subject" runat="server" DataValueField="subjectKey" DataTextField="subjectCode" Width="100%"></asp:DropDownList>
                    </div>
                </div>

                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lab_CreatedBy" runat="server" Text="Created By:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:Label ID="lab_CreatedByText" runat="server"></asp:Label>
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
                        <asp:Label ID="Label2" runat="server" Text="Created On:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-9">
                        <asp:Label ID="lab_CreatedOnText" runat="server"></asp:Label>
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
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6">

                <asp:GridView ID="grdFaculty" runat="server" ForeColor="Black" BorderStyle="solid" Width="100%"
                    CssClass="col" BorderColor="#999999" BackColor="White" GridLines="Both" CellPadding="4" Height="100px"
                    BorderWidth="1px" AutoGenerateColumns="false" AllowSorting="True" CellSpacing="0" AllowPaging="true" PageSize="7" AutoGenerateSelectButton="true"
                    EnablePersistedSelection="True" DataKeyNames="facultyKey" ViewStateMode="Enabled" OnPageIndexChanging="grdFaculty_PageIndexChanging"
                    OnSelectedIndexChanged="grdFaculty_SelectedIndexChanged">

                    <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                    <RowStyle BackColor="LightGray" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <%-- <RowStyle HorizontalAlign="Center" BorderColor="#CCCCCC" />--%>
                    <Columns>

                        <%--<asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />--%>
                        <asp:BoundField DataField="facultyKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />

                        <asp:BoundField DataField="facultyName" HeaderText="Faculty Code" />
                        <asp:BoundField DataField="facultyDesc" HeaderText="Faculty Description" />
                        <asp:BoundField DataField="subjectCode" HeaderText="Subject Code" />
                        <asp:BoundField DataField="CreatedOn" HeaderText="Created On" DataFormatString="{0:dd/MMM/yyyy}"/>
                        <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On" DataFormatString="{0:dd/MMM/yyyy}"/>
                        <asp:BoundField DataField="subjectKey" HeaderText="SubjectKey" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"/>

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
        <div class="row divBottomMargin divTopMargin">
            <div class="col-xs-6 col-sm-6 col-md-3 col-lg-2">
                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add New" Width="100px" />
            </div>
            <div class="col-xs-6 col-sm-6 col-md-3 col-lg-2">

                <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Save" Width="100px" OnClientClick="return validate();" />
            </div>
            <div class="col-xs-6 col-sm-6 col-md-3 col-lg-2">
                        <asp:Button ID="btnEdit" runat="server" Text="Update" Width="100px" OnClick="btnEdit_Click" /><%--OnClick="btn_EnquirySave_Click"--%>
                    </div>
            <div class="col-xs-6 col-sm-6 col-md-3 col-lg-2">
                <asp:Button ID="btn_Delete" runat="server" Text="Delete" Width="100px" OnClick="btn_Delete_Click"/>
            </div>
        </div>

    </div>

    <div>


        <br />
    </div>
</asp:Content>

