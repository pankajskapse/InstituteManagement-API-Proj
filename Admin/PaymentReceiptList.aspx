<%@ Page Title="Payment Ratio" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" AutoEventWireup="true" CodeBehind="PaymentReceiptList.aspx.cs" Inherits="InstituteManagement.Admin.PaymentReceiptList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
          <%--Jquery Code For Check/UnCheck the Checkboxes of Treeview--%>



        function OpenAdmissionList() {
            window.open('../Admin/AdmissionList.aspx?ForPopup=true', this, 'width=800,height=640,toolbar=no,statusbar=no,model=yes, scrollbars=yes,');
            return false;
            //var popupStyle = "dialogheight=300px;dialogwidth=450px;dialogleft:200px;dialogtop:200px;status:no;help:no;";
            //var var1 = window.showModalDialog('../Admin/EnquiryList.aspx', this, '', popupStyle);
        }






    </script>

    <div class="container-fluid">
        <div class="row divBottomMargin">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:Label ID="lab_message" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="row divBottomMargin">
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lblCourse" runat="server" Text="Select Course:*"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-9">
                <asp:RadioButtonList ID="rdoListCourse" runat="server" DataTextField="courseCode" DataValueField="courseKey" RepeatColumns="5" RepeatLayout="Table"
                    CssClass="test" OnSelectedIndexChanged="rdoListCourse_SelectedIndexChanged" AutoPostBack="true" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                </asp:RadioButtonList>
            </div>
        </div>
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
        <div class="row divBottomMargin">
            <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lblBatch" runat="server" Text="Batch:*"></asp:Label>
            </div>
            <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                <asp:DropDownList ID="ddlBatchList" runat="server" DataTextField="batchDesc" DataValueField="batchKey" Width="100%">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row divBottomMargin">
            <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                <asp:Button ID="btnCalCulateRatio" runat="server" Text="Calculate Ratio" Width="150px" OnClick="btnCalCulateRatio_Click" /><%--OnClick="btnAdd_Click"--%>
            </div>
            <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                <asp:Button ID="btnExportStudentWise" runat="server" Text="Generate Student Wise" Width="100%" OnClick="btnExportStudentWise_Click" /><%--OnClick="btnAdd_Click"--%>
            </div>
            <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" Width="100%" OnClick="btnExportToExcel_Click" /><%--OnClick="btnAdd_Click"--%>
            </div>
        </div>
        <div class="row divBottomMargin">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:GridView ID="grdSubjectList" runat="server" ForeColor="Black" BorderStyle="solid"
                            CssClass="auto-style5" BorderColor="#999999" BackColor="White" GridLines="Both" CellPadding="4" Height="100px"
                            BorderWidth="1px" AutoGenerateColumns="false" AllowSorting="True" CellSpacing="0" AllowPaging="false"
                            PageSize="7" ViewStateMode="Enabled"  ShowFooter="true"
                            DataKeyNames="subjectKey"  >

                            <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                            <RowStyle BackColor="LightGray" VerticalAlign="Middle" HorizontalAlign="Center" />

                            <Columns>

                                <asp:BoundField DataField="subjectKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />

                                <asp:BoundField DataField="subjectCode" HeaderText="Subject Code" ApplyFormatInEditMode="false" />
                                <asp:BoundField DataField="subjectFees" HeaderText="Subject Fee" ApplyFormatInEditMode="false" />
                                <asp:BoundField DataField="FeeByfargation" HeaderText="Fees By Fargation" ApplyFormatInEditMode="false" />
                                
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
        </div>

        <div class="row divBottomMargin">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:GridView ID="grdStudentWiseFragment" runat="server" ForeColor="Black" BorderStyle="solid"
                            CssClass="auto-style5" BorderColor="#999999" BackColor="White" GridLines="Both" CellPadding="4" Height="100px"
                            BorderWidth="1px" AutoGenerateColumns="true" AllowSorting="True" CellSpacing="0" AllowPaging="false"
                            PageSize="7" ViewStateMode="Enabled"  ShowFooter="false" OnSorting="grdStudentWiseFragment_Sorting"
                             >

                            <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                            <RowStyle BackColor="LightGray" VerticalAlign="Middle" HorizontalAlign="Center" />

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
        </div>

    </div>
</asp:Content>
