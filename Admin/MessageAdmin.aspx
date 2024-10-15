<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" AutoEventWireup="true" CodeBehind="MessageAdmin.aspx.cs" Inherits="InstituteManagement.Admin.MessageAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
            New Mesage - Create a new Message<br />
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
                        <asp:Label ID="LabelMessageType" runat="server" Text="Message Type"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:dropdownlist ID="DDLMessage" runat="server" >
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>OTP</asp:ListItem>
                            <asp:ListItem>Fee Reminder</asp:ListItem>
                            <asp:ListItem>General</asp:ListItem>
                        </asp:dropdownlist>
                    </td>
                    <td class="auto-style4">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style3">
                        <asp:Label ID="LabelMessage" runat="server" Text="Message"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txt_Message" runat="server" Width="400px"></asp:TextBox>
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
                        <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Save" Width="100px" />
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
