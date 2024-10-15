using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Admin
{
    public partial class PaymentReceipt : System.Web.UI.Page
    {
        string Message = string.Empty;
        int iMenuKey = 19;
        string Mode = "INSERT";
        static bool bEditRights = true;
        static bool bAddRights = true;
        static bool bDeleteRights = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            Message = string.Empty;
            LoadBatchList();
            if (!IsPostBack)
            {
                ApplyLoginEmpRights();
                Btn_Save.Enabled = true;
                Txt_ReceiptNo.Attributes.Add("readonly", "readonly");
                if (Txt_ReceiptNo.Text.Trim() == "")
                    Txt_ReceiptNo.Attributes.Add("placeholder", "Auto generated after saving");

                Txt_TranDt.Text = DateTime.Now.ToString("yyyy-MM-dd");
                Txt_ReceiptDt.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //txtTransactionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                Txt_AddmissionID.Attributes.Add("readonly", "readonly");
                txtFirstInstallment.Attributes.Add("readonly", "readonly");
                txtSecondInstallment.Attributes.Add("readonly", "readonly");
                txtThirdInstallment.Attributes.Add("readonly", "readonly");
                txtFourthInstallment.Attributes.Add("readonly", "readonly");
                txtFifthInstallment.Attributes.Add("readonly", "readonly");
                txtSixthInstallment.Attributes.Add("readonly", "readonly");

                if (Txt_AddmissionID.Text.Trim() == "")
                    Txt_AddmissionID.Attributes.Add("placeholder", "Select From Admission List");

                DDL_PayMode_SelectedIndexChanged(null, null);
                chkCheckBounce.Text = "Payment Against Bounce Cheque";
                divBounceDetails.Visible = true;
            }
        }
        private void ApplyLoginEmpRights()
        {
            if (Session["LoginEmpID"].ToString() != "#99")
            {
                BAL.Class.SmartInstitute.EmprightsClass o_GetEmployee = new BAL.Class.SmartInstitute.EmprightsClass();
                DataTable dtRights = o_GetEmployee.GetMenuRightsListForEmpMenu(Convert.ToInt32(Session["LoginEmpKey"]), iMenuKey, ref Message);
                if (dtRights != null && dtRights.Rows.Count > 0)
                {
                    DataRow row = dtRights.Rows[0];
                    if (Convert.ToBoolean(row["erview"]))
                    {

                        if (Convert.ToBoolean(row["eradd"]))
                        {
                            Btn_AddNew.Enabled = true;
                            Btn_Save.Enabled = true;
                            bAddRights = true;
                        }
                        else
                        {
                            Btn_AddNew.Enabled = false;
                            Btn_Save.Enabled = true;
                            bAddRights = false;
                        }

                        if (Convert.ToBoolean(row["eredit"]))
                        {
                            bEditRights = true;
                            btnEdit.Enabled = true;
                        }
                        else
                        {
                            bEditRights = false;
                            btnEdit.Enabled = false;
                        }
                        if (Convert.ToBoolean(row["erdelete"]))
                        {
                            bDeleteRights = true;
                            Btn_Delete.Enabled = true;
                        }
                        else
                        {
                            bDeleteRights = false;
                            Btn_Delete.Enabled = false;
                        }
                    }
                }
            }
        }

        private void LoadBatchList()
        {
            BAL.Class.SmartInstitute.BatchClass o_Batch = new BAL.Class.SmartInstitute.BatchClass();
            DDL_Batch.DataSource = o_Batch.GetBatchList(ref Message);
            DDL_Batch.DataBind();
        }
        private bool ValidateData()
        {
            lab_message.Text = "";
            if (Txt_AddmissionID.Text.Trim() == "")
                lab_message.Text = "Addmission ID is required field";
            if (txtAddmissionKey.Text.Trim() == "")
                lab_message.Text = "Addmission ID is required field";
            else if (Txt_Fname.Text.Trim() == "")
                lab_message.Text = "First name is required field";
            else if (Txt_PayAmt.Text.Trim() == "")
                lab_message.Text = "Pay Amount is required field";
            else if (txtBalanceAmount.Text.Trim() != "")
            {
                if (Convert.ToDecimal(txtBalanceAmount.Text) < 0)
                {
                    lab_message.Text = "Balanace amount is not less than zero. Please enter valid fee amount.";
                }
            }
            if (chkCheckBounce.Text == "Payment Against Bounce Cheque" && chkCheckBounce.Checked && lab_message.Text.Trim() == "")
            {
                if(txtBounceRemark.Text.Trim()=="")
                    lab_message.Text = "Please enter bounce remark.";
            }
            if (lab_message.Text.Trim() == "")
                return true;
            else return false;
        }

        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                string mode = "INSERT";


                Message = string.Empty;
                BAL.Class.SmartInstitute.PaymentReceiptClass o_SavePayReceipt = new BAL.Class.SmartInstitute.PaymentReceiptClass();

                if (Txt_ReceiptNo.Text.Trim() != "")
                {
                    o_SavePayReceipt.paymentreceiptkey = Convert.ToInt32(Txt_ReceiptNo.Text);
                    mode = "UPDATE";
                }
                else
                    o_SavePayReceipt.paymentreceiptkey = 0;
                o_SavePayReceipt.admissionKey = Convert.ToInt32(txtAddmissionKey.Text);
                o_SavePayReceipt.receiptdate = Convert.ToDateTime(Txt_ReceiptDt.Text);
                o_SavePayReceipt.fees = Convert.ToInt32(Txt_PayAmt.Text);
                o_SavePayReceipt.gstothertaxes = 0;// Convert.ToInt32(Txt_gstAmt.Text);
                o_SavePayReceipt.totalfees = 0;// Convert.ToInt32(Txt_TotalAmt.Text);
                if (chkCheckBounce.Text == "Payment Against Bounce Cheque")
                    o_SavePayReceipt.IsCheckBounced = false;
                else
                o_SavePayReceipt.IsCheckBounced = chkCheckBounce.Checked;
                o_SavePayReceipt.BounceAmount = Convert.ToDecimal(txtBounceAmount.Text);
                o_SavePayReceipt.BounceRemark = txtBounceRemark.Text;

                if (DDL_PayMode.SelectedItem != null)
                    o_SavePayReceipt.paymentmode = DDL_PayMode.SelectedValue;
                else
                    o_SavePayReceipt.paymentmode = "";
                o_SavePayReceipt.transactiondate = Txt_TranDt.Text;
                o_SavePayReceipt.transactiondetails = Txt_TranDetails.Text;

                if (Txt_AddmissionID.Text.Trim() == "")
                    txtAddmissionKey.Text = "0";
                o_SavePayReceipt.admissionKey = Convert.ToInt32(txtAddmissionKey.Text);


                if (Txt_FinalFees.Text == "")
                    Txt_FinalFees.Text = "0";

                o_SavePayReceipt.BankBranchName = txtBankName.Text;
                o_SavePayReceipt.ChequeNo = txtChequeNo.Text;

                o_SavePayReceipt.createdBy = Convert.ToInt32(Session["LoginEmpKey"]);
                o_SavePayReceipt.modifiedBy = Convert.ToInt32(Session["LoginEmpKey"]);

                if (ddlInstallmentNo.SelectedItem != null && ddlInstallmentNo.SelectedValue != "")
                    o_SavePayReceipt.InstallmentNo = Convert.ToInt32(ddlInstallmentNo.SelectedValue);
                else
                    o_SavePayReceipt.InstallmentNo = 1;

                Message = string.Empty;

                int iRepKey = o_SavePayReceipt.save(ref Message, mode);
                if (iRepKey > 0)
                {
                    Txt_ReceiptNo.Text = iRepKey.ToString();
                    lab_message.Text = "Payment saved successfully";
                    Btn_Save.Enabled = false;
                    FillPAymentHistoryGrid();

                }
                else
                {
                    lab_message.Text = Convert.ToString(Message);
                }
            }
        }
        private void LoadSubCourseList()
        {
            int iSubCourseKey = 0;
            string SubCourse = "";
            if (rdoSubCourseList.SelectedItem != null)
                SubCourse = rdoSubCourseList.SelectedItem.Text;
            if (IsPostBack && rdoSubCourseList.SelectedValue != null && rdoSubCourseList.SelectedValue != "")
                iSubCourseKey = Convert.ToInt32(rdoSubCourseList.SelectedValue);

            BAL.Class.SmartInstitute.SubCourseClass o_GetSubCourse = new BAL.Class.SmartInstitute.SubCourseClass();

            DataTable dtChild = null;


            if (rdoListCourse.SelectedItem != null && rdoListCourse.SelectedItem.Value != null)
            {
                dtChild = o_GetSubCourse.GetSubCourseListForCourse(Convert.ToInt32(rdoListCourse.SelectedItem.Value), ref Message);
                rdoSubCourseList.DataSource = dtChild;
                rdoSubCourseList.DataBind();

                ListItem subCourse = new ListItem();
                subCourse.Value = iSubCourseKey.ToString();
                subCourse.Text = SubCourse;
                if (IsPostBack && iSubCourseKey > 0)
                {
                    if (rdoSubCourseList.Items.Contains(subCourse))
                        rdoSubCourseList.SelectedValue = iSubCourseKey.ToString();
                }
                //if (!IsPostBack)
                //rdoSubCourseList.SelectedIndex = iSelectedSubCourseInx;

            }
        }

        private void LoadCouseList()
        {
            BAL.Class.SmartInstitute.CourseClass o_GetCourse = new BAL.Class.SmartInstitute.CourseClass();
            DataTable dtCourse = new DataTable();
            dtCourse = o_GetCourse.GetCourseList(ref Message);

            int iSelectedCourseInx = 0;
            if (rdoListCourse.SelectedIndex != null && rdoListCourse.SelectedItem != null)
                iSelectedCourseInx = rdoListCourse.SelectedIndex;
            rdoListCourse.DataSource = dtCourse;
            rdoListCourse.DataBind();

            //////if (!IsPostBack)
            rdoListCourse.SelectedIndex = iSelectedCourseInx;

        }
        private void SetDataOfAdmissionForEdit()
        {
            LoadCouseList();
            BAL.Class.SmartInstitute.AdmissionClass o_GetAdmDetails = new BAL.Class.SmartInstitute.AdmissionClass();
            DataTable dtAdmDetails = o_GetAdmDetails.GetAdmissionDetailsByID(Convert.ToInt32(txtAddmissionKey.Text), ref Message);

            if (dtAdmDetails != null && dtAdmDetails.Rows.Count > 0)
            {
                DataTable dtCourseKey = dtAdmDetails.AsEnumerable().GroupBy(r => r.Field<int>("coursekey")).Select(g => g.First()).CopyToDataTable();
                DataRow rowCourse = null;
                if (dtCourseKey != null && dtCourseKey.Rows.Count > 0)
                    rowCourse = dtCourseKey.Rows[0];
                if (rowCourse != null)
                    rdoListCourse.SelectedValue = rowCourse["courseKey"].ToString();


                LoadSubCourseList();
                DataTable dtsubCourseKey = dtAdmDetails.AsEnumerable().GroupBy(r => r.Field<int>("subcoursekey")).Select(g => g.First()).CopyToDataTable();
                DataRow rowSubCourse = null;
                if (dtsubCourseKey != null && dtsubCourseKey.Rows.Count > 0)
                    rowSubCourse = dtsubCourseKey.Rows[0];
                if (rowSubCourse != null)
                {
                    ListItem itm = new ListItem();
                    itm.Value = rowSubCourse["subcoursekey"].ToString();
                    //  if(rdoSubCourseList.Items.Contains())
                    rdoSubCourseList.SelectedValue = rowSubCourse["subcoursekey"].ToString();
                }

                LoadGroupList();
                DataTable dtgroupCourseKey = dtAdmDetails.AsEnumerable().GroupBy(r => r.Field<int>("groupsubcoursekey")).Select(g => g.First()).CopyToDataTable();
                DataRow rowGroupCourse = null;
                if (dtgroupCourseKey != null && dtgroupCourseKey.Rows.Count > 0)
                    rowGroupCourse = dtgroupCourseKey.Rows[0];
                if (rowGroupCourse != null && rowGroupCourse["groupsubcoursekey"] != null && rowGroupCourse["groupsubcoursekey"].ToString() != "0")
                {
                    divGroupList.Visible = true;
                    rdoGroupList.SelectedValue = rowGroupCourse["groupsubcoursekey"].ToString();
                }
                else divGroupList.Visible = false;

                LoadSubjectData();

                foreach (DataRow rowSubj in dtAdmDetails.Rows)
                {
                    foreach (ListItem item in chkSubjectList.Items)
                    {
                        if (item.Value == rowSubj["subjectKey"].ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                        //else item.Selected = false;
                    }

                }
                // else divGroupList.Visible = false;
            }
        }
        private void LoadGroupList()
        {
            if (rdoSubCourseList.SelectedValue != null && rdoSubCourseList.SelectedValue.Trim() != "")
            {
                int iSelectedGroupInx = 0;
                if (rdoGroupList.SelectedIndex != null && rdoGroupList.SelectedItem != null)
                    iSelectedGroupInx = rdoGroupList.SelectedIndex;

                BAL.Class.SmartInstitute.GroupSubCourseClass o_GetGroupSubject = new BAL.Class.SmartInstitute.GroupSubCourseClass();
                DataTable dtChildgrp = o_GetGroupSubject.GetGroupsListForSubCode(Convert.ToInt32(rdoSubCourseList.SelectedValue), ref Message);

                rdoGroupList.DataSource = dtChildgrp;
                rdoGroupList.DataBind();
                //if (!IsPostBack)
                if (dtChildgrp == null || dtChildgrp.Rows.Count <= 0)
                {
                    divGroupList.Visible = false;
                }
                else
                {
                    divGroupList.Visible = true;
                    rdoGroupList.SelectedIndex = iSelectedGroupInx;
                }
            }
        }
        private void LoadSubjectData()
        {
            int iCourseKey = 0;
            int iSubCourseKey = 0;
            int iGroupKey = 0;

            if (rdoListCourse.SelectedItem != null && rdoListCourse.SelectedValue.Trim() != "")
                iCourseKey = Convert.ToInt32(rdoListCourse.SelectedValue);
             if (rdoSubCourseList.SelectedItem != null && rdoSubCourseList.SelectedValue.Trim() != "")
                iSubCourseKey = Convert.ToInt32(rdoSubCourseList.SelectedValue);
            if (rdoGroupList.SelectedItem != null && rdoGroupList.SelectedValue.Trim() != "")
                iGroupKey = Convert.ToInt32(rdoGroupList.SelectedValue);

            if (rdoGroupList.SelectedValue != null && rdoGroupList.SelectedValue.Trim() != "")
            {
                BAL.Class.SmartInstitute.SubjectClass o_GetSubject = new BAL.Class.SmartInstitute.SubjectClass();
                DataTable dtChildSubc = o_GetSubject.GetSubjectListForGroup(iCourseKey, iSubCourseKey, iGroupKey, ref Message);
                chkSubjectList.DataSource = dtChildSubc;
                chkSubjectList.DataBind();

                if (dtChildSubc != null && dtChildSubc.Rows.Count > 0)
                    divSubjectDetails.Visible = true;
                else
                    divSubjectDetails.Visible = false;
            }
            else if (rdoSubCourseList.SelectedValue != null && rdoSubCourseList.SelectedValue.Trim() != "")
            {
                BAL.Class.SmartInstitute.SubjectClass o_GetSubject = new BAL.Class.SmartInstitute.SubjectClass();
                DataTable dtChildSubc = o_GetSubject.GetSubjectListForSubCourse(iCourseKey, iSubCourseKey, ref Message);
                chkSubjectList.DataSource = dtChildSubc;
                chkSubjectList.DataBind();

                if (dtChildSubc != null && dtChildSubc.Rows.Count > 0)
                    divSubjectDetails.Visible = true;
                else
                    divSubjectDetails.Visible = false;
            }
            //if (!IsPostBack)
            //    chkSubjectList.SelectedIndex = 0;
        }
        protected void Txt_AddmissionID_TextChanged(object sender, EventArgs e)
        {
            if (bEditRights)
            {
                if (txtAddmissionKey.Text != "" && txtAddmissionKey.Text != "0")
                {
                    // btnEdit.Enabled = true;
                    //Btn_Save.Enabled = false;
                    SetDataOfAdmissionForEdit();
                    GetBalanceAmount();
                    FillPAymentHistoryGrid();
                    divPayHistory.Visible = false;
                    btnShowPaymentHistory.Text = "Show Payment History";
                    chkCheckBounce.Text = "Payment Against Bounce Cheque";
                    txtBounceAmount.Text = "0";
                    txtBounceRemark.Text = "";
                    chkCheckBounce.Checked = false;
                    lab_message.Text = "";
                }
            }
            else
            {
                //btnEdit.Enabled = false;
                lab_message.Text = "You are not having edit rights";
            }
        }

        protected void Txt_PayAmt_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalAmount();
        }
        private void GetBalanceAmount()
        {
            try
            {
                BAL.Class.SmartInstitute.PaymentReceiptClass o_GetPayBalance = new BAL.Class.SmartInstitute.PaymentReceiptClass();
                decimal TotalPaidFees = o_GetPayBalance.GetFeePaidForAdmission(Convert.ToInt32(txtAddmissionKey.Text), ref Message);
                decimal PayAmt = 0;

                if (Txt_PayAmt.Text.Trim() != "")
                    PayAmt = Convert.ToDecimal(Txt_PayAmt.Text);
                else PayAmt = 0;

                if (Txt_FinalFees.Text.Trim() != "")
                {
                    decimal iTotalAmt = Convert.ToDecimal(Txt_FinalFees.Text);
                    txtBalanceAmount.Text = Convert.ToString(iTotalAmt - (TotalPaidFees + PayAmt));
                }
            }
            catch(Exception err)
            { }
        }
        private void CalculateTotalAmount()
        {
            try
            {
                decimal GSTAmt = 0;
                decimal PayAmt = 0;
                if (Txt_gstAmt.Text.Trim() != "")
                    GSTAmt = Convert.ToDecimal(Txt_gstAmt.Text);
                else GSTAmt = 0;

                if (Txt_PayAmt.Text.Trim() != "")
                    PayAmt = Convert.ToDecimal(Txt_PayAmt.Text);
                else PayAmt = 0;

                Txt_TotalAmt.Text = Convert.ToString(GSTAmt + PayAmt);

                GetBalanceAmount();
            }
            catch(Exception err)
            { }
        }
        private void FillPAymentHistoryGrid()
        {
            if (txtAddmissionKey.Text.Trim() != "")
            {
                BAL.Class.SmartInstitute.PaymentReceiptClass o_GetPayHistory = new BAL.Class.SmartInstitute.PaymentReceiptClass();
                DataTable dtPayHistory = o_GetPayHistory.GetPaymentHistoryDetails(Convert.ToInt32(txtAddmissionKey.Text), ref Message);
                grdPaymentHistory.DataSource = dtPayHistory;
                grdPaymentHistory.DataBind();
            }
        }
        protected void Txt_gstAmt_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalAmount();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (Txt_ReceiptNo.Text.Trim() != "")
            {
                string open = "OpenPaymentReceipt(" + Txt_ReceiptNo.Text + "," + txtAddmissionKey.Text + ",'ReceiptPrint')";
                ClientScript.RegisterStartupScript(this.GetType(), "script", open, true);
                //Response.Redirect("../Reports/ReportViewer.aspx?ReceiptNo=" + Txt_ReceiptNo.Text + "&AdmissionKey=" + txtAddmissionKey.Text + "&ReportName=ReceiptPrint");
            }
            else lab_message.Text = "Select or save receipt first to print";
        }

        protected void btnShowPaymentHistory_Click(object sender, EventArgs e)
        {
            if (divPayHistory.Visible)
            {
                btnShowPaymentHistory.Text = "Show Payment History";
                divPayHistory.Visible = false;
            }
            else
            {
                btnShowPaymentHistory.Text = "Hide Payment History";
                divPayHistory.Visible = true;
            }
        }

        protected void grdPaymentHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPaymentHistory.PageIndex = e.NewPageIndex;
            grdPaymentHistory.DataBind();
        }

        private void ClearControls()
        {
            txtAddmissionKey.Text = "0";
            txtAltPhoneNo.Text = "";
            txtBalanceAmount.Text = "0";
            Txt_AddmissionID.Text = "";
            Txt_FinalFees.Text = "0";
            Txt_Fname.Text = "";
            Txt_gstAmt.Text = "0";
            Txt_lName.Text = "";
            Txt_MobileNo.Text = "";
            Txt_PayAmt.Text = "0";
            Txt_ReceiptDt.Text= DateTime.Now.ToString("yyyy-MM-dd");
            Txt_ReceiptNo.Text = "";
            Txt_TotalAmt.Text = "0";
            Txt_TranDetails.Text = "";
            Txt_TranDt.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DDL_PayMode.SelectedIndex = 0;
            txtChequeNo.Text = "";
            chkCheckBounce.Checked = false;
            chkCheckBounce.Text = "Payment Against Bounce Cheque";
            txtBounceAmount.Text = "0";
            txtBounceRemark.Text = "";
            divBounceDetails.Visible = true;
            txtBankName.Text = "";
            lab_message.Text = "";
            btnShowPaymentHistory_Click(null, null);
        }
        protected void Btn_AddNew_Click(object sender, EventArgs e)
        {
            ClearControls();
            Btn_Save.Enabled = true;
            btnEdit.Enabled = false;
            Btn_Delete.Enabled = false;
        }

        protected void grdPaymentHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            lab_message.Text = "";
            if (bEditRights)
            {
                if (grdPaymentHistory.SelectedRow != null)
                {
                    lab_message.Text = "";
                    Mode = "UPDATE";
                    grdPaymentHistory.SelectedRow.Focus();
                    Txt_ReceiptNo.Text = grdPaymentHistory.SelectedRow.Cells[1].Text;
                    Txt_ReceiptDt.Text = Convert.ToDateTime(Server.HtmlDecode(grdPaymentHistory.SelectedRow.Cells[2].Text)).ToString("yyyy-MM-dd"); 
                    Txt_PayAmt.Text = Server.HtmlDecode(grdPaymentHistory.SelectedRow.Cells[3].Text);
                    DDL_PayMode.SelectedValue = Server.HtmlDecode(grdPaymentHistory.SelectedRow.Cells[4].Text);
                    Txt_TranDetails.Text = Server.HtmlDecode(grdPaymentHistory.SelectedRow.Cells[5].Text);
                    Txt_TranDt.Text = Convert.ToDateTime(Server.HtmlDecode(grdPaymentHistory.SelectedRow.Cells[6].Text)).ToString("yyyy-MM-dd");
                    txtChequeNo.Text = Server.HtmlDecode(grdPaymentHistory.SelectedRow.Cells[8].Text);
                    txtBankName.Text = Server.HtmlDecode(grdPaymentHistory.SelectedRow.Cells[9].Text);
                    btnEdit.Enabled = true;
                    Btn_Delete.Enabled = true;
                    Btn_Save.Enabled = false;

                    DDL_PayMode_SelectedIndexChanged(null, null);
                    chkCheckBounce.Text = "Check Bounced";
                    if (DDL_PayMode.SelectedValue == "Cheque")
                    {
                        chkCheckBounce.Enabled = true;
                        divBounceDetails.Visible = false;                        
                    }
                    else
                    {
                        chkCheckBounce.Enabled = false;
                        divBounceDetails.Visible = false;   
                    }
                    if (grdPaymentHistory.SelectedRow.Cells[12].Text != "&nbsp;" && grdPaymentHistory.SelectedRow.Cells[12].Text == "True")
                        chkCheckBounce.Checked = true;
                    else chkCheckBounce.Checked = false;

                    if (Convert.ToDecimal(grdPaymentHistory.SelectedRow.Cells[13].Text) > 0)
                    {
                        txtBounceAmount.Text = Server.HtmlDecode(grdPaymentHistory.SelectedRow.Cells[13].Text);
                        txtBounceRemark.Text= Server.HtmlDecode(grdPaymentHistory.SelectedRow.Cells[14].Text);
                        chkCheckBounce.Checked = true;
                        divBounceDetails.Visible = true;
                    }
                    else
                    {
                        txtBounceAmount.Text = "0.00";
                        txtBounceRemark.Text = "";
                        //chkCheckBounce.Checked = false;
                        divBounceDetails.Visible = false;
                    }
                    if (Server.HtmlDecode(grdPaymentHistory.SelectedRow.Cells[11].Text)=="1")
                    {
                        Btn_Delete.Enabled = false;
                        btnEdit.Enabled = false;
                        lab_message.Text = "This admission charges are not editable";
                    }
                }
            }
            else
            {
                lab_message.Text = "You are not having edit rights";
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Btn_Save_Click(sender, e);
        }

        protected void Btn_Delete_Click(object sender, EventArgs e)
        {
            if (bDeleteRights)
            {
                try
                {
                    lab_message.Text = "";
                    BAL.Class.SmartInstitute.PaymentReceiptClass o_DeletePayment = new BAL.Class.SmartInstitute.PaymentReceiptClass();
                    if (Txt_ReceiptNo.Text != "" && Txt_ReceiptNo.Text != "0")
                    {
                        o_DeletePayment.paymentreceiptkey = Convert.ToInt32(Txt_ReceiptNo.Text);
                        int retval = o_DeletePayment.save(ref Message, "DELETE");
                        lab_message.Text = "Record deleted successfully";

                    }
                    else
                    {
                        lab_message.Text = "Please select record to delete";

                    }
                    FillPAymentHistoryGrid();
                    //ClearControls();
                    btnEdit.Enabled = false;
                    Btn_Delete.Enabled = false;
                    Btn_Save.Enabled = false;
                }
                catch (Exception err)
                {
                    lab_message.Text = Message;
                }
            }
            else
            {
                lab_message.Text = "You are not allowed to delete record. Please contact administrator.";
            }
        }

        protected void DDL_PayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_PayMode.SelectedIndex != null && DDL_PayMode.SelectedValue != "")
            {
                if (DDL_PayMode.SelectedValue == "Cheque")
                {
                    divChequeDetails.Visible = true;
                }
                else
                    divChequeDetails.Visible = false;
            }
        }

        protected void chkCheckBounce_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheckBounce.Text == "Payment Against Bounce Cheque" && chkCheckBounce.Checked)
            {
                txtBounceAmount.Enabled = true;
                txtBounceRemark.Enabled = true;
            }
            else
            {
                txtBounceAmount.Enabled = false;
                txtBounceRemark.Enabled = false;
            }
        }

        protected void btnPrintBounceCharge_Click(object sender, EventArgs e)
        {
            if (Txt_ReceiptNo.Text.Trim() != "" && txtBounceAmount.Text!="" && Convert.ToDecimal(txtBounceAmount.Text)>0)
            {
                string open = "OpenPaymentReceipt(" + Txt_ReceiptNo.Text + "," + txtAddmissionKey.Text + ",'BounceReceiptPrint')";
                ClientScript.RegisterStartupScript(this.GetType(), "script", open, true);
                //Response.Redirect("../Reports/ReportViewer.aspx?ReceiptNo=" + Txt_ReceiptNo.Text + "&AdmissionKey=" + txtAddmissionKey.Text + "&ReportName=ReceiptPrint");
            }
            else if(Txt_ReceiptNo.Text.Trim() == "")
                lab_message.Text = "Select or save receipt first to print.";
            else if(txtBounceAmount.Text=="" || Convert.ToDecimal(txtBounceAmount.Text)==0)
                lab_message.Text = "Please enter bouncing charges to print bounced receipt.";
        }
    }
}