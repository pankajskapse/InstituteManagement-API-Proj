<%@ Page Title="Employee" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="InstituteManagement.Admin.Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta name="viewport" content="width=380, user-scalable=no" />

    <script type="text/javascript">
        function validate() {
            //if validation sucess return true otherwise return false.
            if (document.getElementById("txtEmployeeName").value != "") {
                return true;
                alert('Employee Name is compulsory field.');
            }
            
            else if (document.getElementById("txtUserID").value != "") {
                return true;
                alert('Employee ID is compulsory field.');
            }

            else if (document.getElementById("txtPassword").value != "") {
                return true;
                alert('Password is compulsory field.');
            }
            return false;
        }
    </script>

    <div class="container-fluid">
        <div class="row divBottomMargin">
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-4">
                <asp:Label ID="Lab_message" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6">
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-4">
                        <asp:Label ID="lblName" runat="server" Text="Name:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-8">
                        <asp:TextBox ID="txtEmployeeName" runat="server" Width="100%"></asp:TextBox>
                         <asp:TextBox ID="txtEmployeeKeyForEditMode" runat="server" Visible="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-4">
                        <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-8">
                        <asp:TextBox ID="txtAddress" runat="server" Width="100%" TextMode="MultiLine" Height="50px"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-4">
                        <asp:Label ID="lblDepartment" runat="server" Text="Department"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-8">
                        <asp:TextBox ID="txtDepartment" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-4">
                        <asp:Label ID="lblDesignation" runat="server" Text="Designation"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-8">
                        <asp:TextBox ID="txtDesignation" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-4">
                        <asp:Label ID="lblEmail" runat="server" Text="E-Mail ID"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-8">
                        <asp:TextBox ID="txtEmail" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-4">
                        <asp:Label ID="lblPhone" runat="server" Text="Phone No"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-8">
                        <asp:TextBox ID="txtPhone" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-4">
                        <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-8">
                        <div class="row">
                            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6">
                                <asp:RadioButton ID="rdoMale" Text="&nbspMale" GroupName="grpGender" runat="server" />
                            </div>
                            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6">
                                <asp:RadioButton ID="rdoFemale" Text="&nbspFemale" GroupName="grpGender" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-4">
                        <asp:Label ID="lblUserID" runat="server" Text="Employee ID:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-8">
                        <asp:TextBox ID="txtUserID" CssClass="uppercase" runat="server" Text="" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-4">
                        <asp:Label ID="Label1" runat="server" Text="Password"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-6 col-lg-8">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="100%"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6">

                <asp:GridView ID="grdEmployee" runat="server" ForeColor="Black" BorderStyle="solid" Width="100%"
                    CssClass="col" BorderColor="#999999" BackColor="White" GridLines="Both" CellPadding="4" Height="100px"
                    BorderWidth="1px" AutoGenerateColumns="false" AllowSorting="True" CellSpacing="0" AllowPaging="true" PageSize="7" AutoGenerateSelectButton="true"
                    EnablePersistedSelection="True" DataKeyNames="employeekey" ViewStateMode="Enabled" OnPageIndexChanging="grdEmployee_PageIndexChanging"
                    OnSelectedIndexChanged="grdEmployee_SelectedIndexChanged">

                    <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                    <RowStyle BackColor="LightGray" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <%-- <RowStyle HorizontalAlign="Center" BorderColor="#CCCCCC" />--%>
                    <Columns>

                        <%--<asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />--%>
                        <asp:BoundField DataField="employeekey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="EmployeeID" HeaderText="ID" />

                   
                        <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                         <asp:BoundField DataField="EmpPassword" HeaderText="Employee Password" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"/>

                       <%-- <asp:BoundField DataField="Emp_Email" HeaderText="Email" />
                        <asp:BoundField DataField="PhoneNo" HeaderText="Phone" />--%>
                        <asp:BoundField DataField="Address" HeaderText="Address" />
                       <%-- <asp:BoundField DataField="Department" HeaderText="Department" />
                        <asp:BoundField DataField="Designation" HeaderText="Designation" />--%>

                    </Columns>
                    <SelectedRowStyle BackColor="#000099" ForeColor="White" Font-Bold="True"></SelectedRowStyle>
                    <PagerStyle BackColor="White" ForeColor="Blue" HorizontalAlign="Left"
                        Font-Size="8pt" VerticalAlign="Middle" Font-Names="Verdana"></PagerStyle>
                    <HeaderStyle HorizontalAlign="Center" BackColor="Orange" ForeColor="White" Font-Bold="True"
                        CssClass="headerstyle"></HeaderStyle>
                    <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                    <PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="100" Position="TopAndBottom"
                        FirstPageText="First Page" LastPageText="Last Page" />
                    <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>

        <div class="row divBottomMargin divTopMargin">
            <div class="col-xs-6 col-sm-12 col-md-3 col-lg-2">
                <asp:Button ID="btn_Add" runat="server" OnClick="btn_Add_Click" Text="Add New" Width="100px" />
            </div>
            <div class="col-xs-6 col-sm-12 col-md-3 col-lg-2">

                <asp:Button ID="btn_SaveEmployee" runat="server" Text="Save" Width="100px" OnClick="btn_SaveEmployee_Click"  OnClientClick="return validate();"/>
            </div>
            <div class="col-xs-6 col-sm-6 col-md-3 col-lg-2">
                        <asp:Button ID="btnEdit" runat="server" Text="Update" Width="100px" OnClick="btnEdit_Click" /><%--OnClick="btn_EnquirySave_Click"--%>
                    </div>
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-2">
                <asp:Button ID="btn_Delete" runat="server" Text="Delete" Width="100px" OnClick="btn_Delete_Click"/>
            </div>
        </div>
        
    </div>

</asp:Content>
