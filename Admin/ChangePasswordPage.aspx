<%@ Page Title="ChangePassword" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" AutoEventWireup="true" CodeBehind="ChangePasswordPage.aspx.cs" Inherits="InstituteManagement.Admin.ChangePasswordPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="row ">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <asp:requiredfieldvalidator runat="server" id="RFVOldPassword" controltovalidate="txtOldPassword"
                    errormessage="Old Password should not be empty" />
            </div>
            <div class="col-xs-12 col-sm-12 col-md-12">
                <asp:requiredfieldvalidator runat="server" id="RFVNewPassword" controltovalidate="txtNewPassword"
                    errormessage="New Password should not be empty" />
            </div>
            <div class="col-xs-12 col-sm-12 col-md-12">
                <asp:requiredfieldvalidator runat="server" id="RFVConfirmPassword" controltovalidate="txtConfirmPassword"
                    errormessage="Confirm Password should not be empty" />
            </div>
            <div id="tabs" style="width: 50%;">
                <div class="form-group">
                    <div class="row mb-1">
                        <div class="col-xs-12 col-sm-12 col-md-12">
                            <label for="UserName">User Name</label>
                            <div class="input-group ">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fa fa-user fa"></i></span>
                                </div>
                                <asp:textbox id="txtEmpID" class="form-control uppercase" readonly="true" runat="server"></asp:textbox>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-xs-12 col-sm-12 col-md-12">
                            <label for="Password">Old Password</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fa fa-lock fa-lg"></i></span>
                                </div>
                                <asp:textbox id="txtOldPassword" class="form-control" runat="server"></asp:textbox>
                            </div>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-xs-12 col-sm-12 col-md-12">
                            <label for="Password">New Password</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fa fa-lock fa-lg"></i></span>
                                </div>
                                <asp:textbox id="txtNewPassword" class="form-control" runat="server"></asp:textbox>
                            </div>
                        </div>
                    </div>

                    <div class="row mb-4">
                        <div class="col-xs-12 col-sm-12 col-md-12">
                            <label for="Password">Confirm Password</label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class="fa fa-lock fa-lg"></i></span>
                                </div>
                                <asp:textbox id="txtConfirmPassword" class="form-control" runat="server"></asp:textbox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

         
        <div class="row ">
            <div class="col-xs-6 col-sm-4 col-md-4">
                <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" OnClick="btnChangePassword_Click" />
                <%--<asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" />--%>
            </div>
        </div>

    </div>
</asp:Content>
