<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Enquiry Form.aspx.cs" Inherits="InstituteManagement.Admin.Enquiry_Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            width: 144px;
        }
        .auto-style4 {
            width: 148px;
        }
        .auto-style5 {
            width: 37px;
        }
        .auto-style6 {
            width: 196px;
        }
        .auto-style7 {
            width: 37px;
            height: 23px;
        }
        .auto-style8 {
            width: 148px;
            height: 23px;
        }
        .auto-style9 {
            width: 196px;
            height: 23px;
        }
        .auto-style10 {
            width: 144px;
            height: 23px;
        }
        .auto-style11 {
            height: 23px;
        }
        .auto-style12 {
            width: 198px;
        }
        .auto-style13 {
            height: 23px;
            width: 198px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h4 id="enq_card_title" class="card-title" style="box-sizing: inherit; outline: none; margin-top: 0px; margin-bottom: 0.5rem; font-family: Poppins, sans-serif; font-weight: 600; line-height: 18px; color: rgb(69, 90, 100); font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;">New Enquiry - Enter Candidate Mobile Number</h4>
&nbsp;&nbsp;&nbsp;
            <br />
            <table class="auto-style1">
                <tr>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style4">Enquiry Unique ID:</td>
                    <td class="auto-style6">
                        <asp:TextBox ID="txtEnqID" runat="server" Width="185px"></asp:TextBox>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style12">&nbsp;</td>
                    <td>
                        <asp:Button ID="btn_Back" runat="server" OnClick="btn_Back_Click" Text="Back" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style4">
                        <asp:Label ID="labMobileNo" runat="server" Text="Mobile Number:"></asp:Label>
&nbsp;</td>
                    <td class="auto-style6">
                        <asp:TextBox ID="txtMobileNo" runat="server" Width="185px"></asp:TextBox>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style12">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style4">
                        <asp:Button ID="btnSendOTP" runat="server" Text="Send OTP"  Enabled="false" OnClick="btnSendOTP_Click" EnableViewState="False"/>
&nbsp;
                        <asp:Label ID="labOTP" runat="server" Text="- - - - "></asp:Label>
                    </td>
                    <td class="auto-style6">
                        <asp:Button ID="btnVerifyOTP" runat="server" Text="Verify OTP"  Enabled="false" OnClick="btnVerifyOTP_Click" EnableViewState="False" />
&nbsp;<asp:TextBox ID="txtOTP" runat="server" Width="85px"></asp:TextBox>
                    </td>
                    <td class="auto-style3">
                        <asp:Label ID="labOTPVeriMessage" runat="server" ForeColor="#990000"></asp:Label>
                    </td>
                    <td class="auto-style12">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7"></td>
                    <td class="auto-style8"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style10"></td>
                    <td class="auto-style13"></td>
                    <td class="auto-style11"></td>
                </tr>
                <tr>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style4"><span style="color: rgb(33, 37, 41); font-family: Roboto, &quot;Open Sans&quot;, Arial, sans-serif; font-size: 12.48px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: right; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Enquiry Date:*</span></td>
                    <td class="auto-style6">
                        <asp:TextBox ID="TextBox2" runat="server" Width="185px"></asp:TextBox>
                    </td>
                    <td class="auto-style3"><span style="color: rgb(33, 37, 41); font-family: Roboto, &quot;Open Sans&quot;, Arial, sans-serif; font-size: 12.48px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: right; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Enquiry Type::</span></td>
                    <td class="auto-style12">&nbsp;<asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem>Telephonic</asp:ListItem>
                        <asp:ListItem>Walk-in</asp:ListItem>
                        <asp:ListItem>Digital Media</asp:ListItem>
                        <asp:ListItem>Others</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td rowspan="4">
                        <asp:GridView ID="GridViewOriginalFees" runat="server">
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style4"><span style="color: rgb(33, 37, 41); font-family: Roboto, &quot;Open Sans&quot;, Arial, sans-serif; font-size: 12.48px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: right; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">First Name:*</span></td>
                    <td class="auto-style6">
                        <asp:TextBox ID="txtFirstName" runat="server" Width="185px"></asp:TextBox>
                    </td>
                    <td class="auto-style3"><span style="color: rgb(33, 37, 41); font-family: Roboto, &quot;Open Sans&quot;, Arial, sans-serif; font-size: 12.48px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: right; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Last Name:</span></td>
                    <td class="auto-style12">
                        <asp:TextBox ID="txtLastName" runat="server" Width="185px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style4"><span style="color: rgb(33, 37, 41); font-family: Roboto, &quot;Open Sans&quot;, Arial, sans-serif; font-size: 12.48px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: right; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Email:*</span></td>
                    <td class="auto-style6">
                        <asp:TextBox ID="txtemailID" runat="server" Width="185px"></asp:TextBox>
                    </td>
                    <td class="auto-style3"><span style="color: rgb(33, 37, 41); font-family: Roboto, &quot;Open Sans&quot;, Arial, sans-serif; font-size: 12.48px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: right; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Alt Phone/Mobile No</span></td>
                    <td class="auto-style12">
                        <asp:TextBox ID="txtaltmobileno" runat="server" Width="185px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style4"><span style="color: rgb(33, 37, 41); font-family: Roboto, &quot;Open Sans&quot;, Arial, sans-serif; font-size: 12.48px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: right; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Gender:</span></td>
                    <td colspan="3">
                        <asp:RadioButton ID="RadioButtonM" runat="server" Text="Male" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="RadioButtonF" runat="server" Text="Female" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style4"><span style="color: rgb(33, 37, 41); font-family: Roboto, &quot;Open Sans&quot;, Arial, sans-serif; font-size: 12.48px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: right; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">College Name:</span></td>
                    <td colspan="3">
                        <asp:TextBox ID="TextBox3" runat="server" Width="525px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style4"><span style="color: rgb(33, 37, 41); font-family: Roboto, &quot;Open Sans&quot;, Arial, sans-serif; font-size: 12.48px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: right; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Course-Subject Selection:*</span></td>
                    <td class="auto-style6">
                        <asp:RadioButton ID="RadioButtonCA" runat="server" Checked="True" Text="CA" />
                    </td>
                    <td class="auto-style3">
                        <asp:RadioButton ID="RadioButtonCS" runat="server" Text="CS" />
                    </td>
                    <td class="auto-style12">
                        <asp:RadioButton ID="RadioButtonJC" runat="server" Text="XI - XII" />
                    </td>
                    <td rowspan="2">
                        <asp:GridView ID="GridViewOfferedFee" runat="server">
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7"></td>
                    <td class="auto-style8"></td>
                    <td class="auto-style9">
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                            <asp:ListItem Selected="True">Foundation</asp:ListItem>
                            <asp:ListItem>Inter.</asp:ListItem>
                            <asp:ListItem>Final</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    <td class="auto-style10">
                        <asp:CheckBoxList ID="CheckBoxList2" runat="server">
                            <asp:ListItem>EET</asp:ListItem>
                            <asp:ListItem>Executive</asp:ListItem>
                            <asp:ListItem>Professional</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    <td class="auto-style13">
                        <asp:CheckBoxList ID="CheckBoxList3" runat="server">
                            <asp:ListItem>CBSE - XI</asp:ListItem>
                            <asp:ListItem>CBSE - XII</asp:ListItem>
                            <asp:ListItem>State - XI</asp:ListItem>
                            <asp:ListItem>State - XII</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style4"><span style="color: rgb(33, 37, 41); font-family: Roboto, &quot;Open Sans&quot;, Arial, sans-serif; font-size: 12.48px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: right; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Batch:</span></td>
                    <td class="auto-style6">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>Nov 2020 - Feb 2021</asp:ListItem>
                            <asp:ListItem>Mar 2021 - June 2021</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style12">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style8">Referred by (Name):</td>
                    <td class="auto-style9">
                        <asp:TextBox ID="txtRefByName" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style10">Referred By (Mobile)</td>
                    <td class="auto-style13">
                        <asp:TextBox ID="txtRefByMobileNo" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style11">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7"></td>
                    <td class="auto-style8"></td>
                    <td class="auto-style9"></td>
                    <td class="auto-style10"></td>
                    <td class="auto-style13"></td>
                    <td class="auto-style11">
                        <asp:GridView ID="GridViewScholarShipOffered" runat="server">
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style8">Enquiry Owner:</td>
                    <td class="auto-style9">
                        <asp:TextBox ID="txtEnquiryOwner" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style11">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style8">Remarks:</td>
                    <td colspan="3" rowspan="2">
                        <asp:TextBox ID="txt_Remarks" runat="server" Height="30px" Width="535px"></asp:TextBox>
                    </td>
                    <td class="auto-style11">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                    <td class="auto-style11">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                    <td class="auto-style9">&nbsp;</td>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style11">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style8">
                      <asp:Button ID="btn_EnquirySave" runat="server" OnClick="btn_EnquirySave_Click" Text="Save" Width="100px" />
                    </td>
                    <td class="auto-style9">&nbsp;</td>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style11">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style11" colspan="4">
                        <asp:Label ID="lab_message" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style11">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                    <td class="auto-style9">&nbsp;</td>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style11">&nbsp;</td>
                </tr>
            </table>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
