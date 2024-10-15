<%@ Page Title="Employee Rights" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" AutoEventWireup="true" CodeBehind="EmployeeRights.aspx.cs" Inherits="InstituteManagement.Admin.EmployeeRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
          <div class="row divBottomMargin">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:Label ID="lab_message" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
           
        <div class="row">
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6" style="overflow:scroll">
                <asp:GridView ID="grdEmployee" runat="server" ForeColor="Black" BorderStyle="solid" Width="100%"
                    CssClass="col" BorderColor="#999999" BackColor="White" GridLines="Both" CellPadding="4" Height="100px"
                    BorderWidth="1px" AutoGenerateColumns="false" AllowSorting="True" CellSpacing="0" AllowPaging="true" PageSize="10" AutoGenerateSelectButton="true"
                    EnablePersistedSelection="True" DataKeyNames="employeekey" ViewStateMode="Enabled" OnPageIndexChanging="grdEmployee_PageIndexChanging"
                    OnSelectedIndexChanged="grdEmployee_SelectedIndexChanged">

                    <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                    <RowStyle BackColor="LightGray" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <%-- <RowStyle HorizontalAlign="Center" BorderColor="#CCCCCC" />--%>
                    <Columns>

                        <asp:BoundField DataField="employeekey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        
                        <asp:BoundField DataField="EmployeeID" HeaderText="ID" />


                        <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                        <asp:BoundField DataField="EmpPassword" HeaderText="Employee Password" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="Address" HeaderText="Address" />

                    </Columns>
                    <SelectedRowStyle BackColor="#000099" ForeColor="White" Font-Bold="True"></SelectedRowStyle>
                    <PagerStyle BackColor="White" ForeColor="Blue" HorizontalAlign="Left"
                        Font-Size="8pt" VerticalAlign="Middle" Font-Names="Verdana"></PagerStyle>
                    <HeaderStyle HorizontalAlign="Center" BackColor="Orange" ForeColor="White" Font-Bold="True"
                        CssClass="headerstyle"></HeaderStyle>
                    <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                    <%--<PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="100" Position="TopAndBottom"
                        FirstPageText="First Page" LastPageText="Last Page" />--%>
                    <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                </asp:GridView>
            </div>

            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6" style="overflow:scroll;height:450px;">
                <asp:GridView ID="grdRights" runat="server" ForeColor="Black" BorderStyle="solid" Width="100%"
                    CssClass="col" BorderColor="#999999" BackColor="White" GridLines="Both" CellPadding="4" Height="100px"
                    BorderWidth="1px" AutoGenerateColumns="false" AllowSorting="True" CellSpacing="0" AllowPaging="false" PageSize="18"
                    EnablePersistedSelection="True" DataKeyNames="menuKey, emprightsKey" ViewStateMode="Enabled" OnPageIndexChanging="grdRights_PageIndexChanging">

                    <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                    <RowStyle BackColor="LightGray" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <%-- <RowStyle HorizontalAlign="Center" BorderColor="#CCCCCC" />--%>
                    <Columns>
                        <asp:BoundField DataField="emprightsKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />

                        <asp:BoundField DataField="menuKey" HeaderText="MenuKey" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="menuName" HeaderText="ID" />
                        <%--<asp:BoundField DataField="erview" HeaderText="View" />
                        <asp:BoundField DataField="erAdd" HeaderText="Add" />
                        <asp:BoundField DataField="erEdit" HeaderText="Edit" />
                        <asp:BoundField DataField="erDelete" HeaderText="Delete" />--%>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblViewRights" runat="server" Text="View" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkView" runat="server" Checked='<%# Eval("erView") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblAddRights" runat="server" Text="Add" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAdd" runat="server" Checked='<%# Eval("erAdd") %>' OnCheckedChanged="chkAdd_CheckedChanged" AutoPostBack="true"/>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblEditRights" runat="server" Text="Edit" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEdit" runat="server" Checked='<%# Eval("erEdit") %>' OnCheckedChanged="chkEdit_CheckedChanged"  AutoPostBack="true"/>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblDeleteRights" runat="server" Text="Delete" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkDelete" runat="server" Checked='<%# Eval("erDelete") %>' OnCheckedChanged="chkDelete_CheckedChanged"  AutoPostBack="true"/>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <SelectedRowStyle BackColor="#000099" ForeColor="White" Font-Bold="True"></SelectedRowStyle>
                    <%--<PagerStyle BackColor="White" ForeColor="Blue" HorizontalAlign="Left"
                        Font-Size="8pt" VerticalAlign="Middle" Font-Names="Verdana"></PagerStyle>--%>
                    <HeaderStyle HorizontalAlign="Center" BackColor="Orange" ForeColor="White" Font-Bold="True"
                        CssClass="headerstyle"></HeaderStyle>
                    <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>
                    <%--<PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="100" Position="TopAndBottom"
                        FirstPageText="First Page" LastPageText="Last Page" />--%>
                    <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
        <div class="row divBottomMargin">

            <div class="col-xs-6 col-sm-12 col-md-3 col-lg-2">

                <asp:Button ID="btn_SaveRights" runat="server" Text="Save" Width="100px" OnClick="btn_SaveRights_Click" />
            </div>

        </div>
    </div>
</asp:Content>
