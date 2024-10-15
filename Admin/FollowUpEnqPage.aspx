<%@ Page Title="Follow Up" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="FollowUpEnqPage.aspx.cs" Inherits="InstituteManagement.Admin.FollowUpEnqPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
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
        /*.test tr input {
             margin-right: 10px;
             padding-right: 10px;
         }*/
        .test td {
            padding-right: 18px;
        }

        .setVisibleFalse {
            display: none;
        }
    </style>
    <script type="text/javascript">
          <%--Jquery Code For Check/UnCheck the Checkboxes of Treeview--%>

        function OpenEnquiryList() {
            window.open('../Admin/EnquiryListNew.aspx?ForPopup=true', this, 'width=750,height=640,toolbar=no,statusbar=no,model=yes, scrollbars=yes,');
            return false;
            //var popupStyle = "dialogheight=300px;dialogwidth=450px;dialogleft:200px;dialogtop:200px;status:no;help:no;";
            //var var1 = window.showModalDialog('../Admin/EnquiryList.aspx', this, '', popupStyle);
        }

        function loadEnquiryData(enqKey, enqID, fName, lName, emailID, mobileNo, gender, collegeName, enquiryTypeKey, batchKey, EnqDate, EnqRemark, EstimatedFee, FinalFees,
        referByName, referByPhoneNo, altPhoneNo) {
            try {
                debugger;
                document.getElementById("<%=txtEnqKey.ClientID%>").value = enqKey;
                document.getElementById("<%=txtEnqID.ClientID%>").value = enqID;
                document.getElementById("<%=txtMobileNo.ClientID%>").value = mobileNo;
                document.getElementById("<%=txtFirstName.ClientID%>").value = fName;
                document.getElementById("<%=txtLastName.ClientID%>").value = lName;
                document.getElementById("<%=txtemailID.ClientID%>").value = emailID;
                document.getElementById("<%=txtCollegeName.ClientID%>").value = collegeName;
                document.getElementById("<%=ddlEnquiryType.ClientID%>").value = enquiryTypeKey;
                document.getElementById("<%=ddlBatchList.ClientID%>").value = batchKey;
                document.getElementById("<%=txtEnquiryDate.ClientID%>").value = EnqDate;
                document.getElementById("<%=txtEnquiryRemark.ClientID%>").value = EnqRemark;
                document.getElementById("<%=txtRefByName.ClientID%>").value = referByName;
                document.getElementById("<%=txtRefByMobileNo.ClientID%>").value = referByPhoneNo;
                document.getElementById("<%=txtaltmobileno.ClientID%>").value = altPhoneNo;

                var ddlEnqType = document.getElementById("<%=ddlEnquiryType.ClientID %>");
                document.getElementById("<%=txtEnquiryType.ClientID%>").value = ddlEnqType.options[ddlEnqType.selectedIndex].innerHTML;

                 var ddlBatch = document.getElementById("<%=ddlBatchList.ClientID %>");
                document.getElementById("<%=txtBatch.ClientID%>").value = ddlBatch.options[ddlBatch.selectedIndex].innerHTML;
                
                if (gender == "Male")
                    document.getElementById("RadioButtonM").checked = true;
                    //document.getElementById("<%=RadioButtonF.ClientID%>").value = true;
                else
                    document.getElementById("RadioButtonF").checked = true;
                    //$('#RadioButtonM').prop('checked', false);
                    //document.getElementById("<%=RadioButtonM.ClientID%>").value = true;

                __doPostBack("txtEnqID", "TextChanged");
            }
            catch (err) {

            }
        }

    </script>


    <div class="container-fluid">
        <div class="row divBottomMargin">
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-4">
                <asp:Label ID="lab_message" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-sm-12 col-md-6 col-lg-6">
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblEnquiryID" runat="server" Text="Enquiry ID:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6" id="divEnqID" runat="server" >
                        <asp:TextBox ID="txtEnqKey" runat="server" Width="100%" CssClass="setVisibleFalse"></asp:TextBox>
                        <asp:TextBox ID="txtEnqID" runat="server" Width="100%" OnTextChanged="txtEnqID_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 col-sm-2 col-md-2 col-lg-3">
                        <asp:Button ID="btnEquSearch" runat="server" Text="Enquiry List" UseSubmitBehavior="false" OnClientClick="return OpenEnquiryList();" />
                    </div>
                </div>
                
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="labMobileNo" runat="server" Text="Mobile Number:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txtMobileNo" runat="server" Width="100%" ></asp:TextBox>
                    </div>
                    <div class="col-xs-2 col-sm-2 col-md-2 col-lg-3">
                        <%--<asp:Button ID="btnShowOldFollowUp" runat="server" Text="Old Follow" OnClick="btnShowOldFollowUp_Click" />--%>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblEnquiryDate" runat="server" Text="Enquiry Date:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txtEnquiryDate" runat="server" TextMode="Date" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblEnquiryType" runat="server" Text="Enquiry Type:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txtEnquiryType" runat="server" Width="100%"></asp:TextBox>
                        
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-2 col-lg-3" style="display:none">
                        <asp:DropDownList ID="ddlEnquiryType" runat="server" DataTextField="enquiryTypeCode" DataValueField="enquiryTypeKey" Width="100%" ClientIDMode="Static"
                            >
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txtFirstName" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblLastName" runat="server" Text="Last Name:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txtLastName" runat="server" Width="100%" ></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txtemailID" runat="server" Width="100%" ></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblAltPhoneNo" runat="server" Text="Alt Phone/Mobile No:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txtaltmobileno" runat="server" Width="100%" ></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblGender" runat="server" Text="Gender:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-3 col-lg-3">
                        <asp:RadioButton ID="RadioButtonM" ClientIDMode="Static" runat="server" Text="Male" Checked="true" GroupName="gGender" />
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-3 col-lg-3">
                        <asp:RadioButton ID="RadioButtonF" ClientIDMode="Static" runat="server" Text="Female" GroupName="gGender" />
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblCollegeName" runat="server" Text="College Name:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txtCollegeName" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblBatch" runat="server" Text="Batch:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txtBatch" runat="server" Width="100%"></asp:TextBox>
                        
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-2 col-lg-3" style="display:none">
                        <asp:DropDownList ID="ddlBatchList" runat="server" DataTextField="batchDesc" DataValueField="batchKey" Width="100%" ClientIDMode="Static">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblRefBy" runat="server" Text="Referred by (Name):"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txtRefByName" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="Label1" runat="server" Text="Referred By (Mobile):"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txtRefByMobileNo" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="Label2" runat="server" Text="Enquiry Owner:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:Label ID="txtEnquiryOwner" runat="server" Width="100%" ClientIDMode="Static"></asp:Label>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="Label3" runat="server" Text="Date:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txtFollowUpdate" runat="server" TextMode="Date" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="lblEnqRemark" runat="server" Text="Enquiry Remarks:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txtEnquiryRemark" runat="server" Height="30px" Width="100%" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:Label ID="Label4" runat="server" Text="Remarks:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txt_Remarks" runat="server" Height="30px" Width="100%" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-7 col-md-6 col-lg-6">
                <asp:GridView ID="grdFollowUp" runat="server" ForeColor="Black" BorderStyle="solid" Width="100%"
                    CssClass="auto-style5" BorderColor="#999999" BackColor="White" GridLines="Both" CellPadding="4" Height="100px"
                    BorderWidth="1px" AutoGenerateColumns="false" AllowSorting="True" CellSpacing="0" AllowPaging="true" 
                    PageSize="7" OnPageIndexChanging="grdFollowUp_PageIndexChanging" ViewStateMode="Enabled"
                    EnablePersistedSelection="True" DataKeyNames="enqfollowupKey">
                    <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                    <RowStyle BackColor="LightGray" VerticalAlign="Middle" HorizontalAlign="Center" />

                    <Columns>


                        <asp:BoundField DataField="enqfollowupKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />


                        <asp:BoundField DataField="followupdate" HeaderText="Follow Up Date" DataFormatString="{0:dd/MMM/yyyy}"/>


                        <asp:BoundField DataField="followupremark" HeaderText="Follow Up Remark" />
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
            <div class="col-xs-6 col-sm-12 col-md-3 col-lg-2">
                <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="btnAdd_Click" Width="100px" />
            </div>
            <div class="col-xs-6 col-sm-12 col-md-3 col-lg-2">

                <asp:Button ID="btn_FollowupSave" runat="server" Text="Save" OnClick="btn_FollowupSave_Click" Width="100px" />
            </div>

        </div>
        


    </div>
</asp:Content>
