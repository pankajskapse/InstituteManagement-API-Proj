<%@ Page Title="Admission" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="AdmissionPage.aspx.cs" Inherits="InstituteManagement.Admin.AdmissionPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .test td {
            padding-right: 18px;
        }

        .setVisibleFalse {
            display: none;
        }
    </style>
    <script type="text/javascript">
          <%--Jquery Code For Check/UnCheck the Checkboxes of Treeview--%>

        $(function () {
            $("[id*=treeviewSubCourse] input[type=checkbox]").bind("click", function () {
                var table = $(this).closest("table");
                if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                    var childDiv = table.next();
                    var isChecked = $(this).is(":checked");
                    $("input[type=checkbox]", childDiv).each(function () {
                        if (isChecked) {
                            $(this).attr("checked", "checked");
                        } else {
                            $(this).removeAttr("checked");
                        }
                    });
                } else {
                    var parentDIV = $(this).closest("DIV");
                    if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                        $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                    } else {
                        $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                    }
                }
            });
        })

        function OpenEnquiryList() {
            window.open('../Admin/EnquiryListNew.aspx?ForPopup=true', this, 'width=800,height=640,toolbar=no,statusbar=no,model=yes, scrollbars=yes,');
            return false;
            //var popupStyle = "dialogheight=300px;dialogwidth=450px;dialogleft:200px;dialogtop:200px;status:no;help:no;";
            //var var1 = window.showModalDialog('../Admin/EnquiryList.aspx', this, '', popupStyle);
        }

        function OpenPaymentReceipt(receiptNo, AdmissionKey, reportType) {
            if (reportType == 'ReceiptPrint') {
                window.open('../Reports/ReportViewerPage.aspx?ReceiptNo=' + receiptNo + '&AdmissionKey=' + AdmissionKey + '&ReportName=ReceiptPrint', this, 'width=800,height=640,toolbar=no,statusbar=no,model=yes, scrollbars=yes,');
                return false;
            }
            else
            {
                window.open('../Reports/ReportViewerPage.aspx?AdmissionKey=' + AdmissionKey + '&ReportName=AdmissionPrint', this, 'width=800,height=640,toolbar=no,statusbar=no,model=yes, scrollbars=yes,');
                return false;
            }
            //var popupStyle = "dialogheight=300px;dialogwidth=450px;dialogleft:200px;dialogtop:200px;status:no;help:no;";
            //var var1 = window.showModalDialog('../Admin/EnquiryList.aspx', this, '', popupStyle);
        }


        function OpenAdmissionList() {
            window.open('../Admin/AdmissionList.aspx?ForPopup=true', this, 'width=800,height=640,toolbar=no,statusbar=no,model=yes, scrollbars=yes,');
            return false;
            //var popupStyle = "dialogheight=300px;dialogwidth=450px;dialogleft:200px;dialogtop:200px;status:no;help:no;";
            //var var1 = window.showModalDialog('../Admin/EnquiryList.aspx', this, '', popupStyle);
        }

        function loadEnquiryData(enqKey, enqID, fName, lName, emailID, mobileNo, gender, collegeName, enquiryTypeKey, batchKey, EnqDate, EnqRemark, EstimatedFee, FinalFees, referByName ,referByPhoneNo , altPhoneNo) {
            try {
                debugger;
                document.getElementById("<%=txtEnqKey.ClientID%>").value = enqKey;
                document.getElementById("<%=txtEnqID.ClientID%>").value = enqID;
                document.getElementById("<%=txtMobileNo.ClientID%>").value = mobileNo;
                document.getElementById("<%=txtFirstName.ClientID%>").value = fName;
                document.getElementById("<%=txtLastName.ClientID%>").value = lName;
                document.getElementById("<%=txtemailID.ClientID%>").value = emailID;
                document.getElementById("<%=txtCollegeName.ClientID%>").value = collegeName;
                document.getElementById("<%=ddlAdmissionType.ClientID%>").value = enquiryTypeKey;
                document.getElementById("<%=ddlBatchList.ClientID%>").value = batchKey;

                document.getElementById("<%=txtEstimatedFees.ClientID%>").value = EstimatedFee;
                document.getElementById("<%=txtFinalFees.ClientID%>").value = FinalFees;
                document.getElementById("<%=txtaltmobileno.ClientID%>").value = altPhoneNo;

                if (gender == "Male")
                    document.getElementById("RadioButtonM").checked = true;
                else document.getElementById("RadioButtonF").checked = true;

                __doPostBack("txtEnqID", "TextChanged");
            }
            catch (err) {

            }
        }

        function loadAdmissionData(AdmKey, AdmCode, AdmDate, fName, lName, MobileNo, batchKey, admissionTypeKey, DOB, emailID, altPhNp, gender, collegeName, AdmissionOwner,
        TotalFee, firstInstallment, secondInstallment, thirdInstallment, fourthInstallment, address, remark, source, fifthInstallment, sixthInstallment) {
            try {
                debugger;
                document.getElementById("<%=txtAdmissionKey.ClientID%>").value = AdmKey;
                document.getElementById("<%=txtAdmissionID.ClientID%>").value = AdmCode;
                //document.getElementById("<%=txtAdmissionDate.ClientID%>").value = AdmDate;
                document.getElementById("<%=txtFirstName.ClientID%>").value = fName;
                document.getElementById("<%=txtLastName.ClientID%>").value = lName;
                document.getElementById("<%=ddlAdmissionType.ClientID%>").value = admissionTypeKey;
                document.getElementById("<%=txtMobileNo.ClientID%>").value = MobileNo;
                document.getElementById("<%=ddlBatchList.ClientID%>").value = batchKey;
                document.getElementById("<%=txtemailID.ClientID%>").value = emailID;
                //document.getElementById("<%=txtDOB.ClientID%>").value = DOB;
                document.getElementById("<%=txtaltmobileno.ClientID%>").value = altPhNp;
                document.getElementById("<%=txtCollegeName.ClientID%>").value = collegeName;
                document.getElementById("<%=txtAdmissionOwner.ClientID%>").value = AdmissionOwner;

                document.getElementById("<%=txtFinalFees.ClientID%>").value = TotalFee;
                document.getElementById("<%=txtFirstFee.ClientID%>").value = firstInstallment;
                document.getElementById("<%=txtSecondFee.ClientID%>").value = secondInstallment;
                document.getElementById("<%=txtThirdFee.ClientID%>").value = thirdInstallment;
                document.getElementById("<%=txtFourthFee.ClientID%>").value = fourthInstallment;
                document.getElementById("<%=txtAddress.ClientID%>").value = address;
                document.getElementById("<%=txt_Remarks.ClientID%>").value = remark;

                document.getElementById("<%=txtFifthFee.ClientID%>").value = fifthInstallment;
                document.getElementById("<%=txtSixthFee.ClientID%>").value = sixthInstallment;

                document.getElementById("<%=lblFinalFeeFromDB.ClientID%>").value = "Final Fee from DB=" + TotalFee;
                

                if (gender == "Male")
                    document.getElementById("RadioButtonM").checked = true;
                else document.getElementById("RadioButtonF").checked = true;

                __doPostBack("txtAdmissionID", "TextChanged");
            }
            catch (err) {

            }
        }

        function SetAdmissionCharge() {
            debugger;
            var check = document.getElementById("<%=divAdmCharge.ClientID%>");
            if ($('#<%=CheckBox_AdminCharges.ClientID %>').is((':checked')) == true) {

                //$('#divAdmCharge').hide()
                check.style.display = 'block';
                if (document.getElementById("<%=txtAdmissionCharges.ClientID%>").value == 0)
                    document.getElementById("<%=txtAdmissionCharges.ClientID%>").value = 500;
                //check.style.display = 'none';
            }
            else {
                check.style.display = 'none';
                document.getElementById("<%=txtAdmissionCharges.ClientID%>").value = 0;
                //check.style.display = 'block';
            }
        }

        function DDL_PayMode_SelectedIndexChanged() {
            var check = document.getElementById("<%=divChequeDetails.ClientID%>");
            if (document.getElementById("<%=DDL_PayMode.ClientID%>").value == "Cheque") {
                check.style.display = 'block';
            }
            else check.style.display = 'block';
        }

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }


    </script>

    <div class="container-fluid">
        <div class="row divBottomMargin">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:Label ID="lab_message" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-sm-12 col-md-12 col-lg-12">
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblEnquiryID" runat="server" Text="Enquiry ID:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                        <asp:TextBox ID="txtEnqKey" runat="server" Width="100%" CssClass="setVisibleFalse"></asp:TextBox>
                        <asp:TextBox ID="txtEnqID" runat="server" Width="100%" OnTextChanged="txtEnqID_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2">
                        <asp:Button runat="server" ID="btnBrowseEqnuiry" UseSubmitBehavior="false" Text="Select Enquiry" OnClientClick="return OpenEnquiryList();" Width="165px" />
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblAdmissionID" runat="server" Text="Admission ID:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                        <asp:TextBox ID="txtAdmissionKey" runat="server" Width="100%" CssClass="setVisibleFalse"></asp:TextBox>
                        <asp:TextBox ID="txtAdmissionID" runat="server" Width="100%" AutoPostBack="true" OnTextChanged="txtAdmissionID_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2">
                        <asp:Button runat="server" ID="btnAdmissionList" UseSubmitBehavior="false" Text="Select Admission" OnClientClick="return OpenAdmissionList();" Width="165px" />
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblMobileNo" runat="server" Text="MobileNo:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-3">
                        <asp:TextBox ID="txtMobileNo" runat="server" Width="100%" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-2 col-lg-2">
                        <asp:Label ID="lblAltPhoneNo" runat="server" Text="Alt Phone No:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-2 col-lg-4">
                        <asp:TextBox ID="txtaltmobileno" runat="server" Width="100%" TextMode="Number"></asp:TextBox>
                    </div>
                    <%-- <div class="col-xs-2 col-sm-2 col-md-2 col-lg-3">
                        <asp:button runat="server" ID="btnSendOTP" text="Send OPT" />
                    </div>--%>
                </div>
                <%--  <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:label id="labOTP" runat="server" text="---------"></asp:label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-5 col-lg-3">
                        <asp:button ID="btnVerifyOTP" runat="server" text="Verify OTP" />
                    </div>
                    <div class="col-xs-2 col-sm-2 col-md-3 col-lg-6">
                        <asp:textbox ID="txtOTP" runat="server" width="100%"></asp:textbox>
                    </div>
                </div>--%>
                <%-- <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-3">
                        <asp:label id="lblBlank1" runat="server" text=""></asp:label>
                    </div>
                    <div class="col-xs-7 col-sm-12 col-md-8 col-lg-9">
                        <asp:label id="labOTPVeriMessage" runat="server" forecolor="#990000"></asp:label>
                    </div>

                </div>--%>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblAdmissionDate" runat="server" Text="Admission Date:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:TextBox ID="txtAdmissionDate" runat="server" TextMode="Date" Width="100%"></asp:TextBox>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-2 col-lg-2">
                        <asp:Label ID="lblAdmissionType" runat="server" Text="Admission Type:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-2 col-lg-4">
                        <asp:DropDownList ID="ddlAdmissionType" runat="server" DataTextField="enquiryTypeCode" DataValueField="enquiryTypeKey" Width="100%">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:TextBox ID="txtFirstName" runat="server" Width="100%"></asp:TextBox>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-2 col-lg-2">
                        <asp:Label ID="lblLastName" runat="server" Text="Last Name:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-2 col-lg-4">
                        <asp:TextBox ID="txtLastName" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblDOB" runat="server" Text="Date Of Birth:*"></asp:Label>

                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:TextBox ID="txtDOB" runat="server" Width="100%" TextMode="Date"></asp:TextBox>

                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblGender" runat="server" Text="Gender:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:RadioButton ID="RadioButtonM" runat="server" Text="Male" Checked="true" GroupName="gGender" ClientIDMode="Static" />
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-2 col-lg-2">
                        <asp:RadioButton ID="RadioButtonF" runat="server" Text="Female" GroupName="gGender" ClientIDMode="Static" />
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-8 col-lg-4">
                        <asp:TextBox ID="txtemailID" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblCollegeName" runat="server" Text="College Name:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-6">
                        <asp:TextBox ID="txtCollegeName" runat="server" Width="100%"></asp:TextBox>
                    </div>

                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblAddress" runat="server" Text="Address:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-6">
                        <asp:TextBox ID="txtAddress" runat="server" Width="100%" TextMode="MultiLine" Height="50%"></asp:TextBox>
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
                <div class="row divBottomMargin" id="divSubjectDetails" runat="server">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblSubject" runat="server" Text="Select Subject:*"></asp:Label>
                    </div>
                    <div class="col-xs-12 col-sm-7 col-md-8 col-lg-3">
                        <asp:CheckBox ID="chkSelectAllSubject" runat="server" Text="Select All Subject" AutoPostBack="true" OnCheckedChanged="chkSelectAllSubject_CheckedChanged"
                            Width="100%"></asp:CheckBox>
                    </div>
                    <div class="col-xs-12 col-sm-7 col-md-8 col-lg-2">
                        <asp:Button ID="btnShowFaculty" runat="server" Text="Show Subject Details" OnClick="btnShowFaculty_Click"></asp:Button>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-12 col-sm-5 col-md-4 col-lg-2">
                        <asp:Label ID="lblBlank3" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-12 col-sm-7 col-md-8 col-lg-9">
                        <asp:CheckBoxList ID="chkSubjectList" runat="server" DataTextField="subjectCode" DataValueField="subjectKey" RepeatColumns="1" RepeatLayout="Table"
                            CssClass="test" AutoPostBack="true" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%" OnSelectedIndexChanged="chkSubjectList_SelectedIndexChanged">
                        </asp:CheckBoxList>
                    </div>
                    <div class="col-xs-12 col-sm-5 col-md-4 col-lg-2">
                    </div>
                </div>
                <div class="row divBottomMargin" runat="server" id="divFacultyWiseSubj">
                    <div class="col-xs-12 col-sm-5 col-md-4 col-lg-2">
                        <asp:Label ID="lblFacultyWise" runat="server" Text="Select Faculty"></asp:Label>
                    </div>
                    <div class="col-xs-12 col-sm-7 col-md-8 col-lg-9">
                        <asp:GridView ID="grdFacultyWiseSubject" runat="server" ForeColor="Black" BorderStyle="solid"
                            CssClass="auto-style5" BorderColor="#999999" BackColor="White" GridLines="Both" CellPadding="4" Height="100px"
                            BorderWidth="1px" AutoGenerateColumns="false" AllowSorting="True" CellSpacing="0" AllowPaging="true"
                            PageSize="7" ViewStateMode="Enabled" OnRowDataBound="grdFacultyWiseSubject_RowDataBound" ShowFooter="true"
                            DataKeyNames="subjectKey, facultyKey" OnRowCommand="grdFacultyWiseSubject_RowCommand" OnSelectedIndexChanged="grdFacultyWiseSubject_SelectedIndexChanged"
                            OnRowEditing="grdFacultyWiseSubject_RowEditing" OnRowCancelingEdit="grdFacultyWiseSubject_RowCancelingEdit"
                            OnRowUpdating="grdFacultyWiseSubject_RowUpdating" OnPageIndexChanging="grdFacultyWiseSubject_PageIndexChanging">

                            <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                            <RowStyle BackColor="LightGray" VerticalAlign="Middle" HorizontalAlign="Center" />

                            <Columns>


                                <asp:BoundField DataField="subjectKey" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />


                                <asp:BoundField DataField="facultyKey" HeaderText="facultyKey" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                <asp:TemplateField HeaderText="Faculty Name" Visible="false">

                                    <ItemTemplate>
                                        <asp:Label ID="lblFacultyName" runat="server" Text='<%# Eval("FacultyName") %>'></asp:Label>
                                        <asp:DropDownList ID="ddlFacultyName" runat="server" DataTextField="FacultyNameSubject" DataValueField="FacultyKey"
                                            Width="100%">
                                        </asp:DropDownList>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:BoundField DataField="subjectCode" HeaderText="Subject Code" ApplyFormatInEditMode="false" />
                                <asp:BoundField DataField="subjectFees" HeaderText="Subject Fee" ApplyFormatInEditMode="false" />

                                <asp:ButtonField Text="SingleClick" CommandName="Update" Visible="False" />
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
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblBatch" runat="server" Text="Batch:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:DropDownList ID="ddlBatchList" runat="server" DataTextField="batchDesc" DataValueField="batchKey" Width="100%"
                            OnSelectedIndexChanged="ddlBatchList_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblAdmissionOwner" runat="server" Text="Admission Owner:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-9">
                        <asp:Label ID="txtAdmissionOwner" runat="server" Width="100%"></asp:Label>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblSource" runat="server" Text="Source:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-9">
                        <asp:CheckBoxList ID="chkSourceList" runat="server" RepeatColumns="6" CssClass="test" AutoPostBack="false" BorderStyle="Solid" RepeatDirection="Horizontal"
                            Width="100%">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblTotalCalculatedFees" runat="server" Text="Estimated Fees:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:TextBox ID="txtEstimatedFees" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblFinalFees" runat="server" Text="Final Fees:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:TextBox ID="txtFinalFees" runat="server" OnTextChanged="txtFinalFees_TextChanged" onkeypress="return NumberOnly()"  AutoPostBack="true" Width="100%"></asp:TextBox>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblFinalFeeFromDB" runat="server"></asp:Label>
                    </div>
                    <%--onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"--%>
                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtFinalFees" runat="server"
                        ErrorMessage="Only Numbers allowed" ValidationExpression="\d+">
                    </asp:RegularExpressionValidator>--%>
                </div>

                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblFeeInstallment" runat="server" Text="Installment:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblFirstInstallment" runat="server" Text="First" Width="100%"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblSecondInstallment" runat="server" Text="Second" Width="100%"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblThirdInstallment" runat="server" Text="Third" Width="100%"></asp:Label>
                    </div>

                    <%-- <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblFifthInstallment" runat="server" Text="Fifth"></asp:Label>
                    </div>--%>
                </div>

                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblInstallmentAmount" runat="server" Text="Amount:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:TextBox ID="txtFirstFee" runat="server" TextMode="Number" Width="100%" AutoPostBack="true" OnTextChanged="txtFirstFee_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:TextBox ID="txtSecondFee" runat="server" TextMode="Number" Width="100%" AutoPostBack="true" OnTextChanged="txtSecondFee_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:TextBox ID="txtThirdFee" runat="server" TextMode="Number" Width="100%" AutoPostBack="true" OnTextChanged="txtThirdFee_TextChanged"></asp:TextBox>
                    </div>

                    <%--<div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:TextBox ID="txtFifthFee" runat="server" TextMode="Number" Width="100%"></asp:TextBox>
                    </div>--%>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblFourthInstallment" runat="server" Text="Fourth" Width="100%"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblFifthInstallment" runat="server" Text="Fifth" Width="100%"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblSixthInstallment" runat="server" Text="Sixth" Width="100%"></asp:Label>
                    </div>

                </div>

                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:TextBox ID="txtFourthFee" runat="server" TextMode="Number" Width="100%" AutoPostBack="true" OnTextChanged="txtFourthFee_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:TextBox ID="txtFifthFee" runat="server" TextMode="Number" Width="100%" AutoPostBack="true" OnTextChanged="txtFifthFee_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:TextBox ID="txtSixthFee" runat="server" TextMode="Number" Width="100%" AutoPostBack="true" OnTextChanged="txtSixthFee_TextChanged"></asp:TextBox>
                    </div>
                </div>
                <asp:Panel ID="pnlPayment" runat="server">
                    <div class="row divBottomMargin" id="divAdmissionCheckBox" runat="server">
                        <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                            <asp:Label ID="Label3" runat="server" Text="Admin:" CssClass="setVisibleFalse"></asp:Label>
                        </div>
                        <div class="col-xs-6 col-sm-12 col-md-4 col-lg-4">
                            <asp:CheckBox ID="CheckBox_AdminCharges" runat="server" Text="&nbsp; *500 Extra for Admin Charges (One Time)" onclick="SetAdmissionCharge();" />
                        </div>
                        <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3" runat="server" id="divAdmCharge">
                            <asp:TextBox ID="txtAdmissionCharges" runat="server" TextMode="Number" Width="100%" Text="500"></asp:TextBox>
                        </div>
                        <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3 ">
                            <asp:CheckBox ID="CheckBox_AddedInApp" runat="server" Text=" # Added in the App" />
                        </div>
                    </div>
                    <div class="row divBottomMargin" runat="server" id="divAmountPay">
                        <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                            <asp:Label ID="lblAmountToPay" runat="server" Text="Amount to Pay:*"></asp:Label>
                        </div>
                        <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                            <asp:TextBox ID="txtAmountToPay" runat="server" TextMode="Number" Width="100%"></asp:TextBox>
                        </div>
                        <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                            <asp:Label ID="lblPaymentMode" runat="server" Text="Payment Mode:"></asp:Label>
                        </div>
                        <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                            <asp:DropDownList ID="DDL_PayMode" runat="server" Width="100%" OnSelectedIndexChanged="DDL_PayMode_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Cash" Value="Cash" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Cheque" Value="Cheque"></asp:ListItem>
                                <asp:ListItem Text="Online/RTGS/NEFT" Value="Online"></asp:ListItem>
                                <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row divBottomMargin" id="divChequeDetails" runat="server">
                        <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                            <asp:Label ID="lblChequeNo" runat="server" Text="Cheque No:"></asp:Label>
                        </div>
                        <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                            <asp:TextBox ID="txtChequeNo" runat="server" Width="100%" MaxLength="6"></asp:TextBox>
                        </div>
                        <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                            <asp:Label ID="lblBankName" runat="server" Text="Bank/Branch Name:"></asp:Label>
                        </div>
                        <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                            <asp:TextBox ID="txtBankName" runat="server" Width="100%"></asp:TextBox>
                        </div>
                        <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date:"></asp:Label>
                        </div>
                        <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                            <asp:TextBox ID="txtTransactionDate" runat="server" Width="100%" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>
                </asp:Panel>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblRemark" runat="server" Text="Remarks:"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-9">
                        <asp:TextBox ID="txt_Remarks" runat="server" Height="30px" Width="100%" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" Width="100px" OnClick="btnAdd_Click" /><%--OnClick="btnAdd_Click"--%>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Button ID="btn_EnquirySave" runat="server" Text="Save" Width="100px" OnClick="btn_EnquirySave_Click" /><%--OnClick="btn_EnquirySave_Click"--%>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Button ID="btnEdit" runat="server" Text="Update" Width="100px" OnClick="btnEdit_Click" /><%--OnClick="btn_EnquirySave_Click"--%>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="100px" OnClick="btnDelete_Click" Enabled="false" /><%--OnClick="btn_EnquirySave_Click"--%>
                    </div>
                    
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Button ID="btnReceipt" runat="server" Text="Print Admission Form" Width="100%" OnClick="btnReceipt_Click" /><%--OnClick="btn_EnquirySave_Click"--%>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Button ID="btnAdmissionChargePrint" runat="server" Text="Print Admission Charge Receipt" Width="100%" OnClick="btnAdmissionChargePrint_Click" /><%--OnClick="btnAdd_Click"--%>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-3">
                        <asp:Button ID="btnPrintAdmissionAmountPrint" runat="server" Text="Print Admission Payment Receipt" Width="100%" OnClick="btnPrintAdmissionAmountPrint_Click" /><%--OnClick="btn_EnquirySave_Click"--%>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <asp:TreeView ID="treeviewSubCourse" runat="server" ShowCheckBoxes="All" ShowLines="true" Visible="false">
    </asp:TreeView>
</asp:Content>
