<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" AutoEventWireup="true" CodeBehind="InventoryFlowChart.aspx.cs" Inherits="InstituteManagement.Inventory.InventoryFlowChart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
            Inventory Flow Chart <br />
            <br />
            <table class="auto-style1">
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                    <td>                    
                    </td>
                    <td>&nbsp;</td>
                </tr>
                 <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">
                        <asp:Label ID="lab_Batch" runat="server" Text="Batch"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:dropdownlist runat="server" ID="DDL_Batch" Width="100px">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>Batch 1</asp:ListItem>
                            <asp:ListItem>Batch 2</asp:ListItem>
                        </asp:dropdownlist>
                    </td>
                    <td class="auto-style4">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td class="auto-style2">
                        &nbsp;</td>
                    <td class="auto-style4">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td class="auto-style2">
                        <asp:Label ID="LabelStage1" runat="server" Text="Stage One"></asp:Label>
                    </td>
                    <td class="auto-style4">
                        <asp:Label ID="LabelStage2" runat="server" Text="Stage Two"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelStage3" runat="server" Text="Stage Three"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelStage4" runat="server" Text="Stage Four"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td class="auto-style2">
                        <asp:checkbox runat="server" ID="chkBox_Stage1" Text="Submited in office"></asp:checkbox>
                    </td>
                    <td class="auto-style4">
                        <asp:checkbox runat="server" ID="chkBox_Stage2" Text="Email sent"></asp:checkbox>
                    </td>
                    <td>
                        <asp:checkbox runat="server" ID="chkBox_Stage3" Text="Printing done"></asp:checkbox>
                    </td>
                    <td>
                        <asp:checkbox runat="server" ID="chkBox_Stage4" Text="Notes Received"></asp:checkbox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td class="auto-style2">
                        &nbsp;</td>
                    <td class="auto-style4">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">
                        <asp:Label ID="LabelRemarks" runat="server" Text="Remarks"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txt_Message" runat="server" Width="400px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">&nbsp;&nbsp;&nbsp;&nbsp; </td>
                    <td class="auto-style4">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">
                        <asp:Label ID="lab_CreatedBy" runat="server" Text="Created By:"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lab_CreatedByText" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style4">
                        <asp:Label ID="label3" runat="server" Text="Modified By:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lab_ModifiedByText" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">
                        <asp:Label ID="Label2" runat="server" Text="Created On:"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lab_CreatedOnText" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style4">
                        <asp:Label ID="Label4" runat="server" Text="Modified On:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lab_ModifiedOnText" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">
                        <asp:Button ID="btn_Save" runat="server" Text="Save" Width="100px" />
                    </td>
                    <td class="auto-style4">
                        <asp:Button ID="btn_Delete" runat="server" Text="Delete" Width="100px" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="4">
                        <asp:Label ID="Lab_message" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <br />
        </div>
</asp:Content>
