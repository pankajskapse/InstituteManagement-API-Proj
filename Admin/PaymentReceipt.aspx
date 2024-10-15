<%@ Page Title="Payment Receipt" Language="C#" MasterPageFile="~/MasterPage/Standard.Master" AutoEventWireup="true" CodeBehind="PaymentReceipt.aspx.cs" Inherits="InstituteManagement.Admin.PaymentReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .setVisibleFalse {
            display: none;
        }
    </style>

    <script type="text/javascript">
        function OpenAdmissionList() {
            window.open('../Admin/AdmissionList.aspx?ForPopup=true', this, 'width=800,height=640,toolbar=no,statusbar=no,model=yes, scrollbars=yes,');
            return false;
        }

        function loadAdmissionData(AdmKey, AdmCode, AdmDate, fName, lName, MobileNo, batchKey, admissionTypeKey, DOB, emailID, altPhNp, gender, collegeName, AdmissionOwner,
        TotalFee, firstInstallment, secondInstallment, thirdInstallment, fourthInstallment, address, remark, source, fifthInstallment, sixthInstallment) {
            try {
                debugger;
                document.getElementById("<%=txtAddmissionKey.ClientID%>").value = AdmKey;
                document.getElementById("<%=Txt_AddmissionID.ClientID%>").value = AdmCode;
                document.getElementById("<%=Txt_Fname.ClientID%>").value = fName;
                document.getElementById("<%=Txt_lName.ClientID%>").value = lName;
                document.getElementById("<%=Txt_MobileNo.ClientID%>").value = MobileNo;
                document.getElementById("<%=DDL_Batch.ClientID%>").value = batchKey;

                document.getElementById("<%=txtAltPhoneNo.ClientID%>").value = altPhNp;

                document.getElementById("<%=Txt_FinalFees.ClientID%>").value = TotalFee;
                document.getElementById("<%=txtFirstInstallment.ClientID%>").value = "First - " + firstInstallment;
                document.getElementById("<%=txtSecondInstallment.ClientID%>").value = "Second - " + secondInstallment;
                document.getElementById("<%=txtThirdInstallment.ClientID%>").value = "Third - " + thirdInstallment;
                document.getElementById("<%=txtFourthInstallment.ClientID%>").value = "Fourth - " + fourthInstallment;
                document.getElementById("<%=txtFifthInstallment.ClientID%>").value = "Fifth - " + fifthInstallment;
                document.getElementById("<%=txtSixthInstallment.ClientID%>").value = "Sixth - " + sixthInstallment;

                if (document.getElementById("<%=txtAddmissionKey.ClientID%>").value != "")
                    document.getElementById("<%=chkCheckBounce.ClientID%>").disabled = false;
                else document.getElementById("<%=chkCheckBounce.ClientID%>").disabled = true;

                document.getElementById("<%=Txt_PayAmt.ClientID%>").value = 0;

                __doPostBack("Txt_AddmissionID", "TextChanged");
            }
            catch (err) {

            }
        }

        function OpenPaymentReceipt(receiptNo, AdmissionKey, reportType) {
            window.open('../Reports/ReportViewerPage.aspx?ReceiptNo=' + receiptNo + '&AdmissionKey=' + AdmissionKey + '&ReportName=' + reportType, this, 'width=800,height=640,toolbar=no,statusbar=no,model=yes, scrollbars=yes,');
            return false;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="row divBottomMargin">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:Label ID="lab_message" runat="server" ForeColor="Red" Text=""></asp:Label>
            </div>
        </div>
        <div class="row divBottomMargin">
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lab_AddmissionID" runat="server" Text="Addmission ID:"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:TextBox ID="txtAddmissionKey" runat="server" Width="100%" CssClass="setVisibleFalse"></asp:TextBox>
                <asp:TextBox ID="Txt_AddmissionID" runat="server" Width="100%" OnTextChanged="Txt_AddmissionID_TextChanged" AutoPostBack="true"></asp:TextBox>
            </div>
            <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2">
                <asp:Button ID="btn_SearchAdmission" runat="server" Text="Search Admission" UseSubmitBehavior="false" OnClientClick="return OpenAdmissionList();" />
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:Button ID="btnShowPaymentHistory" runat="server" Text="Show Payment History" OnClick="btnShowPaymentHistory_Click"></asp:Button>
            </div>

        </div>
        <div class="row divBottomMargin">
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lab_ReceiptNo" runat="server" Text="Receipt No."></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:TextBox ID="Txt_ReceiptNo" runat="server" Width="100%"></asp:TextBox>
            </div>
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lab_ReceiptDt" runat="server" Text="Receipt Date:"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:TextBox ID="Txt_ReceiptDt" runat="server" TextMode="Date" Width="100%"></asp:TextBox>
            </div>
        </div>
        <div class="row divBottomMargin">
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lab_Fname" runat="server" Text="First Name:"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:TextBox ID="Txt_Fname" runat="server" Width="100%"></asp:TextBox>
            </div>
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="Lab_lName" runat="server" Text="Last Name:"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:TextBox ID="Txt_lName" runat="server" Width="100%"></asp:TextBox>
            </div>
        </div>
        <div class="row divBottomMargin">
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lab_MobileNo" runat="server" Text="Mobile No."></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:TextBox ID="Txt_MobileNo" runat="server" Width="100%" TextMode="Number"></asp:TextBox>
            </div>
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lab_AltMobileNo" runat="server" Text="Alt. Phone No."></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:TextBox ID="txtAltPhoneNo" runat="server" Width="100%"></asp:TextBox>
            </div>
        </div>
        <div class="row divBottomMargin">
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lab_Batch" runat="server" Text="Batch:"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:DropDownList ID="DDL_Batch" runat="server" DataTextField="batchDesc" DataValueField="batchKey" Width="100%">
                </asp:DropDownList>
            </div>
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lab_finalFees" runat="server" Text="Final Fees:"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:TextBox ID="Txt_FinalFees" runat="server" Width="100%"></asp:TextBox>
            </div>
        </div>
        <div class="row divBottomMargin">
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lab_PayMode" runat="server" Text="Payment Mode:"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:DropDownList ID="DDL_PayMode" runat="server" Width="100%" OnSelectedIndexChanged="DDL_PayMode_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="Cash" Value="Cash" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Cheque" Value="Cheque"></asp:ListItem>
                    <asp:ListItem Text="Online/RTGS/NEFT" Value="Online"></asp:ListItem>
                    <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lab_PayAmt" runat="server" Text="Pay Amount:"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:TextBox ID="Txt_PayAmt" runat="server" Width="100%" TextMode="Number" OnTextChanged="Txt_PayAmt_TextChanged" AutoPostBack="true"></asp:TextBox>
            </div>
        </div>
        <div class="row divBottomMargin" id="divChequeDetails" runat="server">
            <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lblChequeNo" runat="server" Text="Cheque No:"></asp:Label>
            </div>
            <div class="col-xs-6 col-sm-12 col-md-4 col-lg-4">
                <asp:TextBox ID="txtChequeNo" runat="server" Width="100%" MaxLength="6"></asp:TextBox>
            </div>
            <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lblBankName" runat="server" Text="Bank/Branch Name:"></asp:Label>
            </div>
            <div class="col-xs-6 col-sm-12 col-md-4 col-lg-4">
                <asp:TextBox ID="txtBankName" runat="server" Width="100%"></asp:TextBox>
            </div>
            <%-- <div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date:"></asp:Label>
                    </div>--%>
            <%--<div class="col-xs-6 col-sm-12 col-md-4 col-lg-2">
                        <asp:TextBox ID="txtTransactionDate" runat="server" Width="100%" TextMode="Date"></asp:TextBox>
                    </div>--%>
        </div>
        <div class="row divBottomMargin">
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lab_TranDt" runat="server" Text="Transaction Date:"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:TextBox ID="Txt_TranDt" runat="server" Width="100%"></asp:TextBox>
            </div>
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lblBalanceAmount" runat="server" Text="Balance Amount"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:TextBox ID="txtBalanceAmount" runat="server" Width="100%" TextMode="Number" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lab_gstAmt" runat="server" Text="GST/Other Tax" CssClass="setVisibleFalse"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:TextBox ID="Txt_gstAmt" runat="server" Width="100%" TextMode="Number" OnTextChanged="Txt_gstAmt_TextChanged" AutoPostBack="true" CssClass="setVisibleFalse"></asp:TextBox>
            </div>
        </div>
        <div class="row divBottomMargin" style="display: none">
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:Label ID="Label3" runat="server"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lab_totalAmt" runat="server" Text="Total Amount:"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:TextBox ID="Txt_TotalAmt" runat="server" Width="100%" TextMode="Number"></asp:TextBox>
            </div>
        </div>
        <div class="row divBottomMargin">
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lab_tranDetails" runat="server" Text="Transaction Details:"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:TextBox ID="Txt_TranDetails" runat="server" TextMode="MultiLine" Width="100%" Height="30px"></asp:TextBox>
            </div>

            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:TextBox ID="txtFirstInstallment" runat="server"></asp:TextBox>
            </div>
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:TextBox ID="txtSecondInstallment" runat="server"></asp:TextBox>
            </div>
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:TextBox ID="txtThirdInstallment" runat="server"></asp:TextBox>
            </div>

        </div>
        <div class="row divBottomMargin">
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-6">
                <asp:CheckBox ID="chkCheckBounce" runat="server" Text="Check Bounced" OnCheckedChanged="chkCheckBounce_CheckedChanged"  Font-Bold="false" AutoPostBack="true"></asp:CheckBox>
            </div>            
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:TextBox ID="txtFourthInstallment" runat="server"></asp:TextBox>
            </div>
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:TextBox ID="txtFifthInstallment" runat="server"></asp:TextBox>
            </div>
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:TextBox ID="txtSixthInstallment" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row" runat="server" id="divBounceDetails">
                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-2">
                    <asp:Label ID="lblBounceAmount" runat="server" Text="Amount:" Enabled="false"></asp:Label>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-2">
                    <asp:TextBox ID="txtBounceAmount" runat="server" TextMode="Number" Enabled="false" Text="0"></asp:TextBox>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-2">
                    <asp:Label ID="lblBounceRemark" runat="server" Text="Bounce Remark:" Enabled="false"></asp:Label>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                    <asp:TextBox ID="txtBounceRemark" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                </div>
        </div>
        <div class="row">
            <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                <asp:Label ID="lblInstallmentNo" runat="server" Text="Installment No:" Visible="false"></asp:Label>
            </div>
            <div class="col-xs-5 col-sm-10 col-md-6 col-lg-4">
                <asp:DropDownList ID="ddlInstallmentNo" runat="server" Width="100%" Visible="false">
                    <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row divBottomMargin" id="divPayHistory" runat="server">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:GridView ID="grdPaymentHistory" runat="server" ForeColor="Black" BorderStyle="solid"
                    CssClass="auto-style5" BorderColor="#999999" BackColor="White" GridLines="Both" CellPadding="4" Height="100px"
                    BorderWidth="1px" AutoGenerateColumns="false" AllowSorting="True" CellSpacing="0" AllowPaging="true"
                    PageSize="7" ViewStateMode="Enabled" ShowFooter="false" AutoGenerateSelectButton="true" DataKeyNames="paymentreceiptKey"
                    OnPageIndexChanging="grdPaymentHistory_PageIndexChanging" OnSelectedIndexChanged="grdPaymentHistory_SelectedIndexChanged">

                    <FooterStyle BackColor="#CCCCCC"></FooterStyle>
                    <RowStyle BackColor="LightGray" VerticalAlign="Middle" HorizontalAlign="Center" />

                    <Columns>

                        <asp:BoundField DataField="paymentreceiptKey" HeaderText="Receipt ID" />

                        <asp:BoundField DataField="receiptDate" HeaderText="Payment Date" ApplyFormatInEditMode="false" DataFormatString="{0:dd/MMM/yyyy}" />
                        <asp:BoundField DataField="fees" HeaderText="Amount Received" ApplyFormatInEditMode="false" />
                        <asp:BoundField DataField="paymentMode" HeaderText="Payment Mode" ApplyFormatInEditMode="false" />
                        <asp:BoundField DataField="transactionDetails" HeaderText="Details" ApplyFormatInEditMode="false" />
                        <asp:BoundField DataField="transactionDate" HeaderText="transactionDate" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="InstallmentNo" HeaderText="Installment No" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="ChequeNo" HeaderText="Cheque No" />
                        <asp:BoundField DataField="BankBranchName" HeaderText="Bank/Branch" />
                        <asp:BoundField DataField="feeTypeDetails" HeaderText="feeTypeDetails" />
                        <asp:BoundField DataField="FeeType" HeaderText="FeeType" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <%--<asp:BoundField DataField="IsCheckBounced" HeaderText="Is Bounced" />--%>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblIsBounced" runat="server" Text="Is Bounced" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsBounced" runat="server" Checked='<%# Eval("IsCheckBounced") %>' Enabled="false"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:BoundField DataField="BounceAmount" HeaderText="Bounce Amount" />
                         <asp:BoundField DataField="BounceRemark" HeaderText="Bounce Remark" />
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
        <%--<div style="read"></div>--%>
        <asp:Panel ID="pnlSubjectDetails" runat="server" Enabled="false">
            <div class="row divBottomMargin">
                <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                    <asp:Label ID="lblCourse" runat="server" Text="Select Course:*"></asp:Label>
                </div>
                <div class="col-xs-5 col-sm-10 col-md-6 col-lg-9">
                    <asp:RadioButtonList ID="rdoListCourse" runat="server" DataTextField="courseCode" DataValueField="courseKey" RepeatColumns="5" RepeatLayout="Table"
                        CssClass="test" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row divBottomMargin">
                <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                    <asp:Label ID="lblSubCourse" runat="server" Text="Subcourse/Subject:*"></asp:Label>
                </div>
                <div class="col-xs-5 col-sm-10 col-md-6 col-lg-9">
                    <asp:RadioButtonList ID="rdoSubCourseList" runat="server" DataTextField="subcourseCode" DataValueField="subcourseKey" RepeatColumns="4" RepeatLayout="Table"
                        CssClass="test" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row divBottomMargin" runat="server" id="divGroupList">
                <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                    <asp:Label ID="lblGroup" runat="server" Text="Select Group:*"></asp:Label>
                </div>
                <div class="col-xs-5 col-sm-10 col-md-6 col-lg-9">
                    <asp:RadioButtonList ID="rdoGroupList" runat="server" DataTextField="groupsubcourseCode" DataValueField="groupsubcourseKey" RepeatColumns="4" RepeatLayout="Table"
                        CssClass="test" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row divBottomMargin" id="divSubjectDetails" runat="server">
                <div class="col-xs-5 col-sm-12 col-md-4 col-lg-2">
                    <asp:Label ID="lblSubject" runat="server" Text="Select Subject:*"></asp:Label>
                </div>
                <div class="col-xs-12 col-sm-7 col-md-8 col-lg-9">
                    <asp:CheckBoxList ID="chkSubjectList" runat="server" DataTextField="subjectCode" DataValueField="subjectKey" RepeatColumns="1" RepeatLayout="Table"
                        CssClass="test" BorderStyle="Solid" RepeatDirection="Horizontal" Width="100%">
                    </asp:CheckBoxList>
                </div>
                <div class="col-xs-12 col-sm-5 col-md-4 col-lg-2">
                </div>
            </div>
        </asp:Panel>
        <div class="row divBottomMargin divTopMargin">
            <div class="col-xs-5 col-sm-4 col-md-4 col-lg-2">
                <asp:Button ID="Btn_AddNew" runat="server" Text="Add New" Width="100px" OnClick="Btn_AddNew_Click" />
            </div>
            <div class="col-xs-5 col-sm-4 col-md-4 col-lg-2">
                <asp:Button ID="Btn_Save" runat="server" Text="Save" Width="100px" OnClick="Btn_Save_Click" />
            </div>
            <div class="col-xs-5 col-sm-4 col-md-4 col-lg-2">
                <asp:Button ID="btnEdit" runat="server" Text="Update" Width="100px" OnClick="btnEdit_Click" />
            </div>
            <div class="col-xs-5 col-sm-4 col-md-4 col-lg-2">
                <asp:Button ID="Btn_Delete" runat="server" Text="Delete" Width="100px" OnClick="Btn_Delete_Click" />
            </div>
            <div class="col-xs-5 col-sm-4 col-md-4 col-lg-2">
                <asp:Button ID="btnPrint" runat="server" Text="Print Receipt" Width="100px" OnClick="btnPrint_Click" />
            </div>
             <div class="col-xs-5 col-sm-4 col-md-4 col-lg-2">
                <asp:Button ID="btnPrintBounceCharge" runat="server" Text="Bounce Receipt" Width="120px" OnClick="btnPrintBounceCharge_Click" />
            </div>
        </div>
    </div>



</asp:Content>
