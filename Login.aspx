<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="InstituteManagement.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
       <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" type="text/css" href="assets/css/bootstrap.css"/>

    <!-- Website CSS style -->
    <link rel="stylesheet" type="text/css" href="assets/css/main.css"/>

    <!-- Fonts -->
    <link href="Scripts/font-awesome.min.css" rel="stylesheet" />
    <link href="Styles/Oxygen.css" rel="stylesheet" />
    <link href="Styles/Passion.css" rel="stylesheet" />
    <link href="Scripts/Login.css" rel="stylesheet" />
    <link href="Scripts/icons/font-awesome/css/fontawesome-all.css" rel="stylesheet" />
    <link href="Scripts/icons/font-awesome/css/fontawesome-all.min.css" rel="stylesheet" />
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
    <link rel="icon" type="image/png" sizes="16x16" href="../Images/VPALogo.png" />
    <style>
        .backColor
        {
            background-color:orange;
        }
        .uppercase {
            text-transform: uppercase;
        }
    </style>
</head>
<body class="bg-image " style="background-image: url('./Images/InstituteImg2.jpg')" >
    <div class="container">
        <div class="row main ">
            <div class="panel-heading">
                <div class="panel-title text-center">
                    <br>
                    <img src="Images/VPA logo 2020.png" alt="Alternate Text" class="img-logo" />
                    <br>
                    <br>
                </div>
            </div>
            <div class="main-login main-center">
                <form id="frmLogin" runat="server">
                    <div class="form-group">
                        <label for="username" class="cols-sm-2 control-label">Username</label>
                        <div class="cols-sm-10">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                <%--<input type="text" class="form-control" name="username" id="username" placeholder="Enter your Username" />--%>
                                <asp:TextBox ID="txtUserID" CssClass="form-control uppercase" runat="server" Width="100%" MaxLength="200"
                                   ></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="password" class="cols-sm-2 control-label">Password</label>
                        <div class="cols-sm-10">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
                                <%--<input type="password" class="form-control" name="password" id="password" placeholder="Enter your Password" />--%>
                                <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" Width="100%" MaxLength="100" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row ">
                        <div class="col-xs-12 col-sm-12 col-md-12">
                            <asp:RequiredFieldValidator runat="server" ID="txtUserIDRequiredValidator" ControlToValidate="txtUserID"
                                ErrorMessage="User ID/E-Mail ID should not be empty" ForeColor="Red" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12">
                            <asp:RequiredFieldValidator runat="server" ID="txtPasswordRequiredValidator" ControlToValidate="txtPassword"
                                ErrorMessage="Password should not be empty" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12">
                            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>

                    <div class="form-group ">
                        <asp:Button ID="btnLogin" CssClass="btn btn-primary btn-lg btn-block login-button" runat="server" Text="Login" OnClick="btnLogin_Click" />
                    </div>
                </form>
            </div>
        </div>
    </div>


</body>
</html>
