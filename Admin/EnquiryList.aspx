<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnquiryList.aspx.cs" Inherits="InstituteManagement.Admin.EnquiryList" EnableEventValidation="false" ValidateRequest="false" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- Tell the browser to be responsive to screen width -->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
     <link href="../Styles/MainCss.css" rel="stylesheet" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" />
    <link href="../Styles/jquery-ui.min.css" rel="stylesheet" />
    <link href="../Scripts/style.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/jquery-3.3.1.min.js"></script>
  <script src="../assets/libs/popper.js/dist/umd/popper.min.js"></script>
    <script src="../assets/libs/bootstrap/dist/js/bootstrap.min.js"></script>
    <script src="../assets/libs/perfect-scrollbar/dist/perfect-scrollbar.jquery.min.js"></script>
    <script src="../assets/extra-libs/sparkline/sparkline.js"></script>

     <script src="../js/jquery-ui.min.js"></script>
    <script src="../js/waves.js"></script>
    <!--Menu sidebar -->
    <script src="../js/sidebarmenu.js"></script>
    <!--Custom JavaScript -->
    <script src="../js/custom.min.js"></script>
    <!--This page JavaScript -->
    <!-- <script src="../../dist/js/pages/dashboards/dashboard1.js"></script> -->
    <!-- Charts js Files -->
    <script src="../assets/libs/flot/excanvas.js"></script>
    <script src="../assets/libs/flot/jquery.flot.js"></script>
    <script src="../assets/libs/flot/jquery.flot.pie.js"></script>
    <script src="../assets/libs/flot/jquery.flot.time.js"></script>
    <script src="../assets/libs/flot/jquery.flot.stack.js"></script>
    <script src="../assets/libs/flot/jquery.flot.crosshair.js"></script>
    <script src="../assets/libs/flot.tooltip/js/jquery.flot.tooltip.min.js"></script>
    <%--<script src="../js/pages/chart/chart-page-init.js"></script>--%>
    <link href="../assets/libs/flot/css/float-chart.css" rel="stylesheet" />

     <script type="text/javascript">
         function loadEnquiryDataOne() {
             alert("hiii");
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            
        <div class="row">
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6">
                <div class="row divBottomMargin">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <asp:GridView ID="grdEnquiryList" runat="server" ForeColor="Black" BorderStyle="solid" Width="100%"
                    CssClass="col" BorderColor="#999999" BackColor="White" GridLines="Both" CellPadding="4" Height="100px"
                    BorderWidth="1px" AutoGenerateColumns="false" AllowSorting="True" CellSpacing="0" AllowPaging="true" PageSize="10"
                    EnablePersistedSelection="True" DataKeyNames="enquiryKey" ViewStateMode="Enabled" AutoGenerateSelectButton="true" 
                    OnSelectedIndexChanged="grdEnquiryList_SelectedIndexChanged" OnPageIndexChanging="grdEnquiryList_PageIndexChanging">

                    <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                    <RowStyle BackColor="LightGray" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <%-- <RowStyle HorizontalAlign="Center" BorderColor="#CCCCCC" />--%>
                    <Columns>
                      
                        <asp:BoundField DataField="enquiryKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />

                        <asp:BoundField DataField="enquiryCode" HeaderText="Enquiry Code" />
                        <asp:BoundField DataField="fName" HeaderText="First Name" />
                        <asp:BoundField DataField="lName" HeaderText="Last Name" />
                        <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                        <asp:BoundField DataField="enquiryDate" HeaderText="Enquiry Date" DataFormatString="{0:dd/MMM/yyyy}"/>
                        <asp:BoundField DataField="emailID" HeaderText="Email" />
                        <asp:BoundField DataField="gender" HeaderText="Gender" />
                        <asp:BoundField DataField="collegeName" HeaderText="college Name" />
                        <asp:BoundField DataField="CreatedOn" HeaderText="Created On" DataFormatString="{0:dd/MMM/yyyy}"/>
                        <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On" DataFormatString="{0:dd/MMM/yyyy}"/>
                        <asp:BoundField DataField="enquiryTypeKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="batchKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />

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
            </div>
        </div>
</div>
   <%-- <div>

        <uc1:ucEnquiryList runat="server" ID="ucEnquiryList" />
    </div>--%>
    </form>
</body>
</html>
