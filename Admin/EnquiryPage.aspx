<%@ Page Title="Enquiry" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" AutoEventWireup="true" CodeBehind="EnquiryPage.aspx.cs" Inherits="InstituteManagement.Admin.EnquiryPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        /*.test tr input {
             margin-right: 10px;
             padding-right: 10px;
         }*/
        .test td {
            padding-right: 5%;
        }
          .setVisibleFalse {
            display: none;
        }
    </style>

    <script type="text/javascript">
  

    <%--Jquery Code For Check/UnCheck the Checkboxes of Treeview--%>

        $(function () {
            $("[id*=treeviewCourse] input[type=checkbox]").bind("click", function () {
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

        function loadEnquiryData(enqKey, enqID, fName, lName, emailID, mobileNo, gender, collegeName, enquiryTypeKey, batchKey, EnqDate, EnqRemark, EstimatedFee, FinalFees,
            referByName, referByPhoneNo, altPhoneNo)
        {
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
                document.getElementById("<%=txtFinalFees.ClientID%>").value = FinalFees;
                document.getElementById("<%=lblFinalFeeFromDB.ClientID%>").value = "Final Fee from DB="+FinalFees;

                document.getElementById("<%=txtRefByName.ClientID%>").value = referByName;
                document.getElementById("<%=txtRefByMobileNo.ClientID%>").value = referByPhoneNo;
                document.getElementById("<%=txt_Remarks.ClientID%>").value = EnqRemark;
                document.getElementById("<%=txtEnquiryDate.ClientID%>").value = EnqDate;
                document.getElementById("<%=txtaltmobileno.ClientID%>").value = altPhoneNo;

                if (gender == "Male")
                    document.getElementById("RadioButtonM").checked = true;
                else document.getElementById("RadioButtonF").checked = true;

                __doPostBack("txtEnqID", "TextChanged");
            }
            catch (err) {

            }
        }

        function showConfirm() {
            debugger;
            if (confirm("There are some follow up against this eqnuiry. Do you want to delete this eqnuiry with all follow up's?") == true) {
                document.getElementById('<%= btnDeleteForceFully.ClientID %>').click();
           // __doPostBack("btnDeleteForceFully", "OnClick");
            return true;
        }
        return false;
}
    </script>

    <div class="container-fluid">
        <div class="row divBottomMargin">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:Label ID="lab_message" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblEnquiryID" runat="server" Text="Enquiry Unique ID:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                        <asp:TextBox ID="txtEnqKey" runat="server" Width="100%" CssClass="setVisibleFalse"></asp:TextBox>
                        <asp:TextBox ID="txtEnqID" runat="server" Width="100%" OnTextChanged="txtEnqID_TextChanged" AutoPostBack="true" ClientIDMode="Static"></asp:TextBox>
                    </div>
                     <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2">
                        <asp:Button runat="server" ID="btnEnquiryList" UseSubmitBehavior="false" Text="Select Enquiry" OnClientClick="return OpenEnquiryList();" />
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="labMobileNo" runat="server" Text="Mobile Number:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-4 col-lg-4">
                        <asp:TextBox ID="txtMobileNo" runat="server" Width="100%" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-2 col-lg-2">
                        <asp:Button ID="btnSendOTP" runat="server" Text="Send OTP" enable="false" OnClick="btnSendOTP_Click" />
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblBlank1" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-4 col-lg-4">
                        <asp:Label ID="labOTP" runat="server" Text=""></asp:Label>
                    </div>

                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblOTPVerify" runat="server" Text="Enter Generated OTP"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-4 col-lg-4">
                        <asp:TextBox ID="txtOTP" runat="server" Width="100%"></asp:TextBox>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-2 col-lg-2">
                        <asp:Button ID="btnVerifyOTP" runat="server" Text="Verify OTP" OnClick="btnVerifyOTP_Click" />
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblBlank2" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-4 col-lg-4">
                        <asp:Label ID="labOTPVeriMessage" runat="server" ForeColor="#990000"></asp:Label>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblEnquiryDate" runat="server" Text="Enquiry Date:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                        <asp:TextBox ID="txtEnquiryDate" runat="server" TextMode="Date" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblEnquiryType" runat="server" Text="Enquiry Type:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                        <asp:DropDownList ID="ddlEnquiryType" runat="server" DataTextField="enquiryTypeCode" DataValueField="enquiryTypeKey" Width="100%">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                        <asp:TextBox ID="txtFirstName" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblLastName" runat="server" Text="Last Name:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                        <asp:TextBox ID="txtLastName" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                        <asp:TextBox ID="txtemailID" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblAltPhoneNo" runat="server" Text="Alt Phone/Mobile No:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                        <asp:TextBox ID="txtaltmobileno" runat="server" Width="100%" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblGender" runat="server" Text="Gender:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-3 col-lg-2">
                        <asp:RadioButton ID="RadioButtonM" runat="server" Text="Male" Checked="true" GroupName="gGender" ClientIDMode="Static" />
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-3 col-lg-2">
                        <asp:RadioButton ID="RadioButtonF" runat="server" Text="Female" GroupName="gGender" ClientIDMode="Static"/>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblCollegeName" runat="server" Text="College Name:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txtCollegeName" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblCourse" runat="server" Text="Select Course:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-9">
                        <asp:RadioButtonList ID="rdoListCourse" runat="server" DataTextField="courseCode" DataValueField="courseKey" RepeatColumns="4" RepeatLayout="Table"
                            CssClass="test" OnSelectedIndexChanged="rdoListCourse_SelectedIndexChanged" AutoPostBack="true" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblSubCourse" runat="server" Text="Subcourse/Subject:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-9">
                        <asp:RadioButtonList ID="rdoSubCourseList" runat="server" DataTextField="subcourseCode" DataValueField="subcourseKey" RepeatColumns="3" RepeatLayout="Table"
                            CssClass="test" OnSelectedIndexChanged="rdoSubCourseList_SelectedIndexChanged" AutoPostBack="true" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row divBottomMargin" runat="server" id="divGroupList">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblGroup" runat="server" Text="Select Group:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-9">
                        <asp:RadioButtonList ID="rdoGroupList" runat="server" DataTextField="groupsubcourseCode" DataValueField="groupsubcourseKey" RepeatColumns="3" RepeatLayout="Table"
                            CssClass="test" OnSelectedIndexChanged="rdoGroupList_SelectedIndexChanged" AutoPostBack="true" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-12 col-sm-5 col-md-4 col-lg-2">
                        <asp:Label ID="lblSubject" runat="server" Text="Select Subject:*"></asp:Label>
                    </div>
                    <div class="col-xs-12 col-sm-7 col-md-8 col-lg-9">
                        <asp:CheckBox ID="chkSelectAllSubject" runat="server" Text="Select All Subject" AutoPostBack="true" OnCheckedChanged="chkSelectAllSubject_CheckedChanged"
                            Width="100%"></asp:CheckBox>
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

                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblTotalCalculatedFees" runat="server" Text="Estimated Fees:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-4">
                        <asp:TextBox ID="txtEstimatedFees" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblFinalFees" runat="server" Text="Final Fees:*"></asp:Label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-4">
                        <asp:TextBox ID="txtFinalFees" runat="server" TextMode="Number" ></asp:TextBox>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-4">
                        <asp:Label ID="lblFinalFeeFromDB" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblBatch" runat="server" Text="Batch:*"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                        <asp:DropDownList ID="ddlBatchList" runat="server" DataTextField="batchDesc" DataValueField="batchKey" Width="100%">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblRefBy" runat="server" Text="Referred by (Name):"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                        <asp:TextBox ID="txtRefByName" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblRefByMobile" runat="server" Text="Referred By (Mobile):"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                        <asp:TextBox ID="txtRefByMobileNo" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblEnqOwner" runat="server" Text="Enquiry Owner:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                        <asp:Label ID="txtEnquiryOwner" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="Label1" runat="server" Text="Remarks:"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-6">
                        <asp:TextBox ID="txt_Remarks" runat="server" TextMode="MultiLine" Height="30px" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row divBottomMargin">
                    <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add New" Width="100px" />
                    </div>
                    <div class="col-xs-5 col-sm-10 col-md-6 col-lg-2">
                        <asp:Button ID="btn_EnquirySave" runat="server" OnClick="btn_EnquirySave_Click" Text="Save" Width="100px" />
                    </div>
                      <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Button ID="btnEdit" runat="server" Text="Update" Width="100px" OnClick="btnEdit_Click" /><%--OnClick="btn_EnquirySave_Click"--%>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="100px" OnClick="btnDelete_Click" /><%--OnClick="btn_EnquirySave_Click"--%>
                        <asp:Button ID="btnDeleteForceFully" runat="server" Width="100px" OnClick="btnDeleteForceFully_Click" CssClass="setVisibleFalse"/>
                    </div>
                </div>

            </div>

        </div>
    </div>
    <asp:TreeView ID="treeviewCourse" runat="server" ShowCheckBoxes="All" ShowLines="true" Visible="false">
    </asp:TreeView>

</asp:Content>
