using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Admin
{
    public partial class AdmissionPage : System.Web.UI.Page
    {
        string Message = string.Empty;
        int iMenuKey = 2;
        string Mode = "INSERT";
        static bool bEditRights = true;
        static bool bAddRights = true;
        static bool bDeleteRights = true;
        static bool ChangeFinalFee = true;
        int iNoOfInstallment = 0;
        bool bResetCalculation = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            lab_message.ForeColor = System.Drawing.Color.Red;
            Message = string.Empty;
            try {
                if (!IsPostBack)
                {
                    bResetCalculation = true;
                    LoadEnquiryTypeList();
                    ApplyLoginEmpRights();
                    LoadCouseList();
                    LoadSubCourseList();
                    LoadGroupList();
                    LoadSubjectData();
                    LoadBatchList();
                    FillSourceCheckBoxList();
                    divFacultyWiseSubj.Visible = false;
                    if (txtAdmissionID.Text.Trim() == "")
                        txtAdmissionID.Attributes.Add("placeholder", "Auto Generated After Save");
                    btnEdit.Enabled = false;
                    btn_EnquirySave.Enabled = true;

                    txtAdmissionDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); //DateTime.Now.ToShortDateString();
                                                                                 //PopulateSubCourseView(dtChild, 0, null);
                    txtDOB.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txtAdmissionOwner.Text = Session["LoginEmpName"].ToString();
                    txtEnqID.Attributes.Add("readonly", "readonly");

                    if (txtEnqID.Text.Trim() == "")
                        txtEnqID.Attributes.Add("placeholder", "Select From Enquiry List");

                    txtTransactionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    btnEdit.Enabled = false;
                    btn_EnquirySave.Enabled = true;
                    DDL_PayMode_SelectedIndexChanged(null, null);
                    ddlBatchList_SelectedIndexChanged(null, null);

                    pnlPayment.Enabled = true;

                }
                if (CheckBox_AdminCharges.Checked)
                    divAdmCharge.Style.Add("display", "block");// = false;
                else divAdmCharge.Style.Add("display", "none");// = false;
            }
            catch(Exception err)
            {
                lab_message.Text = err.Message;
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
                            btnAdd.Enabled = true;
                            btn_EnquirySave.Enabled = true;
                            bAddRights = true;
                        }
                        else
                        {
                            btnAdd.Enabled = false;
                            btn_EnquirySave.Enabled = true;
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
                            btnDelete.Visible = true;
                        }
                        else
                        {
                            bDeleteRights = false;
                            btnDelete.Visible = false;
                        }

                    }
                }
            }
        }
        
        private void LoadEnquiryTypeList()
        {
            BAL.Class.SmartInstitute.EnquiryTypeMaster o_EqnuiryType = new BAL.Class.SmartInstitute.EnquiryTypeMaster();
            ddlAdmissionType.DataSource = o_EqnuiryType.GetEnquiryTypeList(ref Message);
            ddlAdmissionType.DataBind();
        }
        private void LoadBatchList()
        {
            try {
                BAL.Class.SmartInstitute.BatchClass o_Batch = new BAL.Class.SmartInstitute.BatchClass();
                int iCoursekey = 0;
                int iSubCourseKey = 0;
                int iGroupKey = 0;

                if (rdoListCourse.SelectedItem != null)
                    iCoursekey = Convert.ToInt32(rdoListCourse.SelectedValue);
                if (rdoSubCourseList.SelectedItem != null)
                    iSubCourseKey = Convert.ToInt32(rdoSubCourseList.SelectedValue);
                if (rdoGroupList.SelectedItem != null && divGroupList.Visible)
                    iGroupKey = Convert.ToInt32(rdoGroupList.SelectedValue);

                ddlBatchList.DataSource = o_Batch.GetBatchListByFilters(iCoursekey, iSubCourseKey, iGroupKey, ref Message);
                ddlBatchList.DataBind();
            }
            catch(Exception err)
            {
                lab_message.Text = err.Message;
            }
        }
        private void LoadCouseList()
        {
            BAL.Class.SmartInstitute.CourseClass o_GetCourse = new BAL.Class.SmartInstitute.CourseClass();
            DataTable dtCourse = new DataTable();
            dtCourse = o_GetCourse.GetCourseList(ref Message);

            rdoListCourse.DataSource = dtCourse;
            rdoListCourse.DataBind();



        }
        private void LoadGroupList()
        {
            try
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
                        try {
                            rdoGroupList.SelectedIndex = iSelectedGroupInx;
                        }
                        catch(Exception err)
                        {
                            lab_message.Text = err.Message;
                        }
                    }
                }
                FillSubjectGridAndFee();
            }
            catch(Exception err)
            {
                lab_message.Text = err.Message;
            }
        }
        private void LoadSubjectData()
        {
            try {
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

                    chkSelectAllSubject.Checked = false;
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

                    chkSelectAllSubject.Checked = false;
                }
            }
            catch(Exception err)
            {
                lab_message.Text = err.Message;
            }
            //if (!IsPostBack)
            //    chkSubjectList.SelectedIndex = 0;
        }
        private void LoadSubCourseList()
        {
            try {
                int iSubCourseKey = 0;
                string SubCourse = "";
                if (rdoSubCourseList.SelectedItem != null)
                    SubCourse = rdoSubCourseList.SelectedItem.Text;
                if (IsPostBack && rdoSubCourseList.SelectedValue != null && rdoSubCourseList.SelectedValue != "")
                    iSubCourseKey = Convert.ToInt32(rdoSubCourseList.SelectedValue);



                //int iSelectedSubCourseInx = 0;
                //if (rdoSubCourseList.SelectedIndex != null && rdoSubCourseList.SelectedItem != null)
                //    iSelectedSubCourseInx = rdoSubCourseList.SelectedIndex;

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
                    else
                    {
                        if (rdoSubCourseList.Items.Count > 0)
                            rdoSubCourseList.SelectedIndex = 0;
                    }
                    //if (!IsPostBack)
                    //rdoSubCourseList.SelectedIndex = iSelectedSubCourseInx;

                }
            }
            catch(Exception err)
            {
                lab_message.Text = err.Message;
            }
        }

        private void LoadFacultyWiseSubject(int iSubjectKey, DataTable dtFinal)
        {
            BAL.Class.SmartInstitute.SubjectClass o_GetSubject = new BAL.Class.SmartInstitute.SubjectClass();
            DataTable dtChildFac = o_GetSubject.GetSubjectListForSubCourseFacultyWise(iSubjectKey, ref Message);


            if (dtChildFac != null && dtChildFac.Rows.Count > 0)
            {
                foreach (DataRow row in dtChildFac.Rows)
                {
                    DataRow rowFinal = dtFinal.NewRow();
                    rowFinal["FacultyKey"] = row["facultyKey"].ToString();
                    rowFinal["FacultyNameSubject"] = row["facultyName"].ToString();
                    dtFinal.Rows.Add(rowFinal);
                }
            }
        }



        protected void rdoListCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                rdoSubCourseList.DataSource = null;
                rdoGroupList.DataSource = null;
                chkSubjectList.DataSource = null;
                rdoSubCourseList.DataBind();
                rdoGroupList.DataBind();
                chkSubjectList.DataBind();

                BAL.Class.SmartInstitute.SubCourseClass o_GetSubCourse = new BAL.Class.SmartInstitute.SubCourseClass();
                DataTable dtChild = o_GetSubCourse.GetSubCourseListForCourse(Convert.ToInt32(rdoListCourse.SelectedValue), ref Message);
                rdoSubCourseList.DataSource = dtChild;
                rdoSubCourseList.DataBind();
                if (dtChild != null && dtChild.Rows.Count > 0)
                    rdoSubCourseList.SelectedIndex = 0;
                LoadGroupList();
                LoadSubjectData();
                LoadBatchList();
                FillSubjectGridAndFee();
            }
            catch(Exception err)
            {
                lab_message.Text = err.Message;
            }
        }

        private void FillSourceCheckBoxList()
        {
            chkSourceList.Items.Clear();
            chkSourceList.Items.Add("Newspaper");
            chkSourceList.Items.Add("Hoardings");
            chkSourceList.Items.Add("Friends");
            chkSourceList.Items.Add("Brochure");
            chkSourceList.Items.Add("Internet");
            chkSourceList.DataBind();

        }
        private void ClearControls()
        {
            txtEnqKey.Text = "0";
            txtEnqID.Text = "";
            txtAdmissionID.Text = "";
            txtAdmissionKey.Text = "0";
            txtFirstFee.Text = "0";
            txtSecondFee.Text = "0";
            txtThirdFee.Text = "0";
            txtFourthFee.Text = "0";
            txtFifthFee.Text = "0";
            txtSixthFee.Text = "0";
            txtCollegeName.Text = "";
            txtAddress.Text = "";
            txtAdmissionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtDOB.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtAdmissionOwner.Text = Session["LoginEmpName"].ToString();
            txtaltmobileno.Text = "";
            txtMobileNo.Text = "";
            txtemailID.Text = "";
            txtEstimatedFees.Text = "0";
            txtFinalFees.Text = "0";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txt_Remarks.Text = "";
            if (ddlAdmissionType.Items.Count > 0)
                ddlAdmissionType.SelectedIndex = 0;
            if (ddlBatchList.Items.Count > 0)
                ddlBatchList.SelectedIndex = 0;

            txtAmountToPay.Text = "0";
            txtAdmissionCharges.Text = "0";
            CheckBox_AddedInApp.Checked = false;
            CheckBox_AdminCharges.Checked = false;
            if (CheckBox_AdminCharges.Checked)
                divAdmCharge.Style.Add("display", "block");// = false;
            else divAdmCharge.Style.Add("display", "none");// = false;

            chkSelectAllSubject.Checked = false;
            chkSelectAllSubject_CheckedChanged(null, null);
            lblFinalFeeFromDB.Text = "";
            //lab_message.Text = "";
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearControls();
            pnlPayment.Enabled = true;
            btn_EnquirySave.Enabled = true;
            Mode = "INSERT";
            lab_message.Text = "";
        }
        private bool ValidateInstallemntTotal()
        {
            if (txtFirstFee.Text == "")
                txtFirstFee.Text = "0";          

            if (txtSecondFee.Text == "")
                txtSecondFee.Text = "0";

            if (txtThirdFee.Text == "")
                txtThirdFee.Text = "0";

            if (txtFourthFee.Text == "")
                txtFourthFee.Text = "0";

            if (txtFifthFee.Text == "")
                txtFifthFee.Text = "0";

            if (txtSixthFee.Text == "")
                txtSixthFee.Text = "0";

            decimal TotInstall = Convert.ToDecimal(txtFirstFee.Text) + Convert.ToDecimal(txtSecondFee.Text) + Convert.ToDecimal(txtThirdFee.Text) +
                Convert.ToDecimal(txtFourthFee.Text) + Convert.ToDecimal(txtFifthFee.Text) + Convert.ToDecimal(txtSixthFee.Text);

            if (txtFinalFees.Text.Trim() == "")
                txtFinalFees.Text = "0";
            if (TotInstall != Convert.ToDecimal(txtFinalFees.Text))
                return false;
            else return true;
        }
        private bool ValidateData()
        {
            lab_message.Text = "";
            if (Server.HtmlDecode(txtMobileNo.Text).Trim() == "")
                lab_message.Text = "Mobile Number is required field";
            else if (Server.HtmlDecode(txtFirstName.Text).Trim() == "")
                lab_message.Text = "First name is required field";
            else if (Server.HtmlDecode(rdoListCourse.Text).Trim() == "")
                lab_message.Text = "Course is required field";
            else if (Server.HtmlDecode(rdoSubCourseList.Text).Trim() == "")
                lab_message.Text = "SubCourse is required field";
            else if (Server.HtmlDecode(chkSubjectList.Text).Trim() == "")
                lab_message.Text = "Subject is required field";
            else if (Server.HtmlDecode(ddlBatchList.Text).Trim() == "")
                lab_message.Text = "Batch is required field";
            else if (Server.HtmlDecode(txtEstimatedFees.Text).Trim() == "")
                lab_message.Text = "Estimated Fees is required field";
            else if (Server.HtmlDecode(txtFinalFees.Text).Trim() == "")
                lab_message.Text = "Final Fees is required field";
            else if (Server.HtmlDecode(txtAmountToPay.Text).Trim() == "" && Mode == "INSERT")
                lab_message.Text = "Amount to pay is required field";


            else if ((Convert.ToDecimal(txtFinalFees.Text.Trim())) < Convert.ToDecimal(txtAmountToPay.Text.Trim()) && Mode == "INSERT")
            {
                if (CheckBox_AdminCharges.Checked && txtAdmissionCharges.Text.Trim() != "" && Convert.ToDecimal(txtAdmissionCharges.Text.Trim()) > 0)
                {
                    decimal Final = Convert.ToDecimal(txtFinalFees.Text.Trim()) + Convert.ToDecimal(txtAdmissionCharges.Text.Trim());
                    if (Final < Convert.ToDecimal(txtAmountToPay.Text.Trim()))
                        lab_message.Text = "Amount to be pay is always less or equal to Final fee";
                }
                else
                    lab_message.Text = "Amount to be pay is always less or equal to Final fee";
            }
            else if (CheckBox_AdminCharges.Checked)
            {
                if (txtAdmissionCharges.Text.Trim() == "" || Convert.ToDecimal(txtAdmissionCharges.Text.Trim()) <= 0)
                    lab_message.Text = "Admission charges of Rs 500 is not blank";
            }
            if (!ValidateInstallemntTotal())
                lab_message.Text = "Installment addition is not equal to final amount. Please review.";
            if (lab_message.Text.Trim() == "")
                return true;
            else return false;
        }
        protected void btn_EnquirySave_Click(object sender, EventArgs e)
        {
            //foreach (GridViewRow row in grdFacultyWiseSubject.Rows)
            //{
            //    string obj = row.Cells[4].Text;
            //    //row.Cells[4].Controls
            //}

            if (ValidateData())
            {

                // Save Course Master DATA Insert into m_course table//         

                Message = string.Empty;
                BAL.Class.SmartInstitute.AdmissionClass o_SaveAdmission = new BAL.Class.SmartInstitute.AdmissionClass();

                // int EnquiryKey = 99; //Temp
                if (txtAdmissionKey.Text.Trim() != "" && txtAdmissionKey.Text.Trim() != "0")
                {
                    o_SaveAdmission.admissionkey = Convert.ToInt16(txtAdmissionKey.Text.Trim());
                    o_SaveAdmission.admissionCode = txtAdmissionID.Text.Trim();
                    Mode = "UPDATE";
                }
                else
                {
                    o_SaveAdmission.admissionCode = "";
                    o_SaveAdmission.admissionkey = 0;
                    Mode = "INSERT";
                }



                o_SaveAdmission.mobileno = txtMobileNo.Text;
                o_SaveAdmission.admissionDate = Convert.ToDateTime(txtAdmissionDate.Text);
                o_SaveAdmission.DOB = Convert.ToDateTime(txtDOB.Text);

                if (ddlAdmissionType.SelectedItem != null)
                    o_SaveAdmission.admissionTypeKey = Convert.ToInt32(ddlAdmissionType.SelectedValue);
                else
                    o_SaveAdmission.admissionTypeKey = 0;

                o_SaveAdmission.firstName = txtFirstName.Text;
                o_SaveAdmission.lastName = txtLastName.Text;
                o_SaveAdmission.emailID = txtemailID.Text;
                o_SaveAdmission.mobilenoAlt = txtaltmobileno.Text;

                if (txtEnqKey.Text.Trim() == "")
                    txtEnqKey.Text = "0";
                o_SaveAdmission.enquirykey = Convert.ToInt32(txtEnqKey.Text);

                string source = "";
                foreach (ListItem item in chkSourceList.Items)
                {
                    if (item.Selected)
                    {
                        if (source.Trim() != "")
                            source = source + "," + item.Text;
                        else source = item.Text;
                    }
                }
                o_SaveAdmission.Source = source;

                if (RadioButtonM.Checked)
                    o_SaveAdmission.gender = 1;
                else
                   if (RadioButtonF.Checked)
                    o_SaveAdmission.gender = 2;

                o_SaveAdmission.collegeName = txtCollegeName.Text;
                o_SaveAdmission.address = txtAddress.Text;
                if (txtFinalFees.Text == "")
                    txtFinalFees.Text = "0";

                o_SaveAdmission.TotalFees = Convert.ToDecimal(txtFinalFees.Text);

                if (txtFirstFee.Text == "")
                    txtFirstFee.Text = "0";

                o_SaveAdmission.firstInstallment = Convert.ToDecimal(txtFirstFee.Text);

                if (txtSecondFee.Text == "")
                    txtSecondFee.Text = "0";
                o_SaveAdmission.secondInstallment = Convert.ToDecimal(txtSecondFee.Text);

                if (txtThirdFee.Text == "")
                    txtThirdFee.Text = "0";
                o_SaveAdmission.thirdInstallment = Convert.ToDecimal(txtThirdFee.Text);

                if (txtFourthFee.Text == "")
                    txtFourthFee.Text = "0";
                o_SaveAdmission.fourthInstallment = Convert.ToDecimal(txtFourthFee.Text);

                if (txtFifthFee.Text == "")
                    txtFifthFee.Text = "0";
                o_SaveAdmission.fifthInstallment = Convert.ToDecimal(txtFifthFee.Text);

                if (txtSixthFee.Text == "")
                    txtSixthFee.Text = "0";
                o_SaveAdmission.sixthInstallment = Convert.ToDecimal(txtSixthFee.Text);

                

                o_SaveAdmission.remarks = txt_Remarks.Text;

                if (ddlBatchList.SelectedItem != null)
                    o_SaveAdmission.batchKey = Convert.ToInt32(ddlBatchList.SelectedValue);
                else
                    o_SaveAdmission.batchKey = 0;

                o_SaveAdmission.createdBy = Convert.ToInt32(Session["LoginEmpKey"]);
                o_SaveAdmission.modifiedBy = Convert.ToInt32(Session["LoginEmpKey"]);

                if (CheckBox_AdminCharges.Checked != null)
                    o_SaveAdmission.AdminChargesInclude = CheckBox_AdminCharges.Checked;
                else
                    o_SaveAdmission.AdminChargesInclude = false;

                if (CheckBox_AddedInApp.Checked != null)
                    o_SaveAdmission.AddedInApp = CheckBox_AddedInApp.Checked;
                else
                    o_SaveAdmission.AddedInApp = false;

               Message = string.Empty;

                int iAdmKey = o_SaveAdmission.save(ref Message, Mode);
                if (iAdmKey > 0)
                {
                    txtAdmissionKey.Text = iAdmKey.ToString();
                    if (Mode == "INSERT")
                        SavePaymentReceipt();
                    // SaveCoursesData(iEnqKey, mode);
                    if (SaveAdmissionDetails(iAdmKey, Mode, ref Message))
                    {
                        lab_message.Text = "Admission saved successfully.";
                        lab_message.ForeColor = System.Drawing.Color.Green;
                        txtAdmissionID.Text = o_SaveAdmission.GetAdmissionIDGenerated(iAdmKey, ref Message);
                        
                        //ClearControls();
                        // mode = "INSERT";
                        btnEdit.Enabled = true;
                        btn_EnquirySave.Enabled = false;
                    }
                    else
                    {
                        lab_message.Text = Message;
                    }
                    //lab_CreatedByText.Text = "#99";
                    //lab_CreatedOnText.Text = Convert.ToString(System.DateTime.Now);
                    //lab_ModifiedByText.Text = "#99";
                    //lab_ModifiedOnText.Text = Convert.ToString(System.DateTime.Now);

                }
                else
                {
                    lab_message.Text = Convert.ToString(Message);
                }
            }
        }
        public enum FeeType
        {
            Installment = 0,
            AdmissionCharges = 1,
            AmountToPayAtAdmission = 2
        }
        private void SavePaymentReceipt()
        {
            try {
                BAL.Class.SmartInstitute.PaymentReceiptClass o_SavePayReceipt = new BAL.Class.SmartInstitute.PaymentReceiptClass();


                o_SavePayReceipt.paymentreceiptkey = 0;
                o_SavePayReceipt.admissionKey = Convert.ToInt32(txtAdmissionKey.Text);
                o_SavePayReceipt.receiptdate = System.DateTime.Now;


                o_SavePayReceipt.gstothertaxes = 0;// Convert.ToInt32(Txt_gstAmt.Text);
                o_SavePayReceipt.totalfees = 0;// Convert.ToInt32(Txt_TotalAmt.Text);

                if (DDL_PayMode.SelectedItem != null)
                    o_SavePayReceipt.paymentmode = DDL_PayMode.SelectedValue;
                else
                    o_SavePayReceipt.paymentmode = "";
                o_SavePayReceipt.transactiondate = txtTransactionDate.Text;
                o_SavePayReceipt.transactiondetails = txt_Remarks.Text;

                if (txtAdmissionKey.Text.Trim() == "")
                    txtAdmissionKey.Text = "0";
                o_SavePayReceipt.admissionKey = Convert.ToInt32(txtAdmissionKey.Text);


                o_SavePayReceipt.createdBy = Convert.ToInt32(Session["LoginEmpKey"]);
                o_SavePayReceipt.modifiedBy = Convert.ToInt32(Session["LoginEmpKey"]);



                o_SavePayReceipt.BankBranchName = txtBankName.Text;
                o_SavePayReceipt.ChequeNo = txtChequeNo.Text;




                o_SavePayReceipt.InstallmentNo = 1;

                Message = string.Empty;

                if (Mode == "INSERT" && CheckBox_AdminCharges.Checked)
                {
                    o_SavePayReceipt.FeeType = (int)FeeType.AdmissionCharges;

                    if (txtAdmissionCharges.Text.Trim() != "")
                        o_SavePayReceipt.fees = Convert.ToInt32(txtAdmissionCharges.Text);
                    else
                        o_SavePayReceipt.fees = 0;

                    o_SavePayReceipt.save(ref Message, "INSERT");
                }

                if (txtAmountToPay.Text.Trim() != "")
                    o_SavePayReceipt.fees = Convert.ToInt32(txtAmountToPay.Text);
                else
                    o_SavePayReceipt.fees = 0;

                o_SavePayReceipt.FeeType = (int)FeeType.AmountToPayAtAdmission;

                int iRepKey = o_SavePayReceipt.save(ref Message, "INSERT");
            }
            catch(Exception err)
            {
                lab_message.Text = err.Message;
            }
        }
        private bool SaveAdmissionDetails(int iAdmissionKey, string mode, ref string Message)
        {
            try
            {
                int iCourseKey = 0;
                int isubCourseKey = 0;
                int iSubjectKey = 0;
                int isubGroupKey = 0;

                BAL.Class.SmartInstitute.AdmissionDetails bal = new BAL.Class.SmartInstitute.AdmissionDetails();
                bal.DeleteAllDetailsOfAdmission(iAdmissionKey, ref Message);

                if (rdoListCourse.SelectedItem != null)
                    iCourseKey = Convert.ToInt32(rdoListCourse.SelectedValue);
                if (rdoSubCourseList.SelectedItem != null)
                    isubCourseKey = Convert.ToInt32(rdoSubCourseList.SelectedValue);
                if (rdoGroupList.SelectedItem != null && divGroupList.Visible)
                    isubGroupKey = Convert.ToInt32(rdoGroupList.SelectedValue);

                foreach (ListItem item in chkSubjectList.Items)
                {
                    if (item.Selected)
                    {
                        iSubjectKey = Convert.ToInt32(item.Value);
                        SaveEnquiryDetails(0, iAdmissionKey, iCourseKey, isubCourseKey, isubGroupKey, iSubjectKey, 0, "INSERT");
                    }
                }
                Message = "";
                return true;

            }
            catch (Exception err)
            {
                Message = err.Message.ToString();
                return false;
            }
        }
        private void SaveEnquiryDetails(int admissionDetailKey, int admissionkey, int CourseKey, int subcourseKey, int groupsubcourseKey, int subjectKey, int facultyKey, string mode)
        {
            int retval = 0;
            try
            {
                BAL.Class.SmartInstitute.AdmissionDetails o_SaveAdmissiondtl = new BAL.Class.SmartInstitute.AdmissionDetails();
                BAL.Class.SmartInstitute.SubjectClass o_subjectFee = new BAL.Class.SmartInstitute.SubjectClass();

                o_SaveAdmissiondtl.admissionDetailKey = admissionDetailKey;
                o_SaveAdmissiondtl.admissionkey = admissionkey;
                o_SaveAdmissiondtl.CourseKey = CourseKey;
                o_SaveAdmissiondtl.subcourseKey = subcourseKey;
                o_SaveAdmissiondtl.groupsubcourseKey = groupsubcourseKey;
                o_SaveAdmissiondtl.subjectKey = subjectKey;
                o_SaveAdmissiondtl.facultyKey = facultyKey;
                o_SaveAdmissiondtl.subjectFees = o_subjectFee.GetSubjectFees(subjectKey, ref Message);


                retval = o_SaveAdmissiondtl.save(ref Message, mode);
            }
            catch (Exception err)
            {
                lab_message.Text = err.Message;
            }


        }
        protected void txtEnqID_TextChanged(object sender, EventArgs e)
        {
            ChangeFinalFee = false;
            if (txtEnqKey.Text.Trim() != "")
            {
                lblFinalFeeFromDB.Text = "Final Fee from DB=" + txtFinalFees.Text;
                setTreeViewNodesAsPerEnquiry();
            }
            ChangeFinalFee = true;
        }
        private void setTreeViewNodesAsPerEnquiry()
        {
            
            BAL.Class.SmartInstitute.EnquiryDetails o_GetEnqDetails = new BAL.Class.SmartInstitute.EnquiryDetails();
            DataTable dtEnqDetails = o_GetEnqDetails.GetEnquiryDetailsByID(Convert.ToInt32(txtEnqKey.Text), ref Message);
            if (dtEnqDetails != null && dtEnqDetails.Rows.Count > 0)
            {
                DataTable dtCourseKey = dtEnqDetails.AsEnumerable().GroupBy(r => r.Field<int>("coursekey")).Select(g => g.First()).CopyToDataTable();
                DataRow rowCourse = null;
                if (dtCourseKey != null && dtCourseKey.Rows.Count > 0)
                    rowCourse = dtCourseKey.Rows[0];
                if (rowCourse != null)
                    rdoListCourse.SelectedValue = rowCourse["courseKey"].ToString();


                LoadSubCourseList();
                DataTable dtsubCourseKey = dtEnqDetails.AsEnumerable().GroupBy(r => r.Field<int>("subcoursekey")).Select(g => g.First()).CopyToDataTable();
                DataRow rowSubCourse = null;
                if (dtsubCourseKey != null && dtsubCourseKey.Rows.Count > 0)
                    rowSubCourse = dtsubCourseKey.Rows[0];
                if (rowSubCourse != null)
                {
                    ListItem itm = new ListItem();
                    itm.Value = rowSubCourse["subcoursekey"].ToString();
                    //if (rdoSubCourseList.Items.Contains(itm))
                    try {
                        rdoSubCourseList.SelectedValue = rowSubCourse["subcoursekey"].ToString();
                    }
                    catch(Exception err)
                    {
                        lab_message.Text = err.Message;
                    }
                }

                LoadGroupList();
                DataTable dtgroupCourseKey = dtEnqDetails.AsEnumerable().GroupBy(r => r.Field<int>("groupsubcoursekey")).Select(g => g.First()).CopyToDataTable();
                DataRow rowGroupCourse = null;
                if (dtgroupCourseKey != null && dtgroupCourseKey.Rows.Count > 0)
                    rowGroupCourse = dtgroupCourseKey.Rows[0];
                if (rowGroupCourse != null && rowGroupCourse["groupsubcoursekey"] != null && rowGroupCourse["groupsubcoursekey"].ToString() != "0")
                {
                    divGroupList.Visible = true;
                    if (rdoGroupList.Items.Count > 0)
                        rdoGroupList.SelectedValue = rowGroupCourse["groupsubcoursekey"].ToString();
                }
                else divGroupList.Visible = false;

                LoadSubjectData();

                foreach (DataRow rowSubj in dtEnqDetails.Rows)
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
            LoadBatchList();

            BAL.Class.SmartInstitute.EnquiryClass o_GetEnq = new BAL.Class.SmartInstitute.EnquiryClass();
            int batchKey = o_GetEnq.GetBatchKeyForEnquiry(Convert.ToInt32(txtEnqKey.Text), ref Message);
            if (batchKey != 0)
            {
                if (ddlBatchList.Items.Count > 0)
                    ddlBatchList.SelectedValue = batchKey.ToString();
            }
            ddlBatchList_SelectedIndexChanged(null, null);
            FillSubjectGridAndFee();
            
        }
        protected void btnBrowseEqnuiry_Click(object sender, EventArgs e)
        {

        }

        protected void chkSubjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (divFacultyWiseSubj.Visible)
            {
                btnShowFaculty_Click(null, null);
            }
            bool isAllChecked = true;
            foreach (ListItem item in chkSubjectList.Items)
            {
                if (!item.Selected)
                {
                    isAllChecked = false;
                    break;
                }
            }

            if (isAllChecked)
                chkSelectAllSubject.Checked = true;
            else chkSelectAllSubject.Checked = false;

            FillSubjectGridAndFee();
        }

        protected void rdoGroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubjectData();
            LoadBatchList();
            FillSubjectGridAndFee();
        }

        protected void rdoSubCourseList_SelectedIndexChanged(object sender, EventArgs e)
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

            BAL.Class.SmartInstitute.SubjectClass o_GetSubject = new BAL.Class.SmartInstitute.SubjectClass();
            DataTable dtChildSubc = o_GetSubject.GetSubjectListForSubCourse(iCourseKey, iSubCourseKey, ref Message);
            chkSubjectList.DataSource = dtChildSubc;
            chkSubjectList.DataBind();

            LoadGroupList();
            LoadSubjectData();
            LoadBatchList();
            FillSubjectGridAndFee();
        }

        protected void chkSelectAllSubject_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = false;
            if (chkSelectAllSubject.Checked)
            {
                isChecked = true;
            }
            foreach (ListItem item in chkSubjectList.Items)
            {
                item.Selected = isChecked;
            }

            FillSubjectGridAndFee();
        }
        private DataTable GetSubjectData()
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

            DataTable dtChildSubc = null;
            if (rdoGroupList.SelectedValue != null && rdoGroupList.SelectedValue.Trim() != "")
            {
                BAL.Class.SmartInstitute.SubjectClass o_GetSubject = new BAL.Class.SmartInstitute.SubjectClass();
                dtChildSubc = o_GetSubject.GetSubjectListForGroup(iCourseKey,iSubCourseKey, iGroupKey, ref Message);

            }
            else if (rdoSubCourseList.SelectedValue != null && rdoSubCourseList.SelectedValue.Trim() != "")
            {
                BAL.Class.SmartInstitute.SubjectClass o_GetSubject = new BAL.Class.SmartInstitute.SubjectClass();
                dtChildSubc = o_GetSubject.GetSubjectListForSubCourse(iCourseKey, iSubCourseKey, ref Message);

            }
            return dtChildSubc;
        }
        DataTable dtFinal = new DataTable();

        private void FillSubjectGridAndFee()
        {
            DataTable dt = GetSubjectData();
            if (dt != null)
            {
                DataTable dtFinalSub = dt.Clone();
                foreach (ListItem item in chkSubjectList.Items)
                {
                    if (item.Selected)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["subjectKey"].ToString() == item.Value)
                            {
                                dtFinalSub.ImportRow(row);
                                break;
                            }
                        }
                    }
                }

                grdFacultyWiseSubject.DataSource = dtFinalSub;
                grdFacultyWiseSubject.DataBind();

                if (dtFinalSub.Rows.Count > 0)
                {
                    decimal total = dtFinalSub.AsEnumerable().Sum(row => row.Field<decimal>("subjectFees"));
                    grdFacultyWiseSubject.FooterRow.Cells[0].Text = "Total";
                    grdFacultyWiseSubject.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                    grdFacultyWiseSubject.FooterRow.Cells[1].Text = total.ToString("N2");

                    txtEstimatedFees.Text = total.ToString("N2");

                    if (ChangeFinalFee)//if (txtFinalFees.Text == "" || Convert.ToDecimal(txtFinalFees.Text.Trim()) == 0)
                        txtFinalFees.Text = total.ToString("N2");

                    CalculateInstallment();
                }
                else
                {
                    txtEstimatedFees.Text = "0";
                    if (ChangeFinalFee)
                        txtFinalFees.Text = "0";
                    CalculateInstallment();
                }
            }
            else
            {
                txtEstimatedFees.Text = "0";
                if (ChangeFinalFee)
                    txtFinalFees.Text = "0";
                CalculateInstallment();
            }
        }
        protected void btnShowFaculty_Click(object sender, EventArgs e)
        {
            if (divFacultyWiseSubj.Visible)
            {
                divFacultyWiseSubj.Visible = false;
                btnShowFaculty.Text = "Show Subject Details";
            }
            else
            {
                divFacultyWiseSubj.Visible = true;
                btnShowFaculty.Text = "Hide Subject Details";
            }
            //foreach (ListItem item in chkSubjectList.Items)
            //{
            //    if (item.Selected)
            //    {
            //        BAL.Class.SmartInstitute.SubjectClass o_GetSubject = new BAL.Class.SmartInstitute.SubjectClass();
            //        DataTable dtChildFac = o_GetSubject.GetSubjectListForGroupWithFaculty(iSubjectKey, ref Message);
            //    }
            FillSubjectGridAndFee();
        }
        protected void grdFacultyWiseSubject_OnRowDataBound(object sender, GridViewRowEventArgs e)

        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdFacultyWiseSubject, "Edit$" + e.Row.RowIndex);
            //    e.Row.Attributes["style"] = "cursor:pointer";
            //}
        }

        protected void grdFacultyWiseSubject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if ((e.Row.RowState & DataControlRowState.Normal) > 0)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (dtFinal.Columns.Count <= 0)
                {
                    dtFinal.Columns.Add("FacultyKey", typeof(int));
                    dtFinal.Columns.Add("FacultyNameSubject", typeof(string));
                }

                //foreach (ListItem item in chkSubjectList.Items)
                //{
                //    if (item.Selected)
                //    {
                //        LoadFacultyWiseSubject(Convert.ToInt32(item.Value), dtFinal);
                //    }
                //    //else item.Selected = false;
                //}
                dtFinal.Rows.Clear();
                // GridViewRow row=e.Row["subjectKey"];

                if (e.Row.Cells[0].Text != "")
                    LoadFacultyWiseSubject(Convert.ToInt32(e.Row.Cells[0].Text), dtFinal);
                // Bind the DropDownList with the DataSet field with "Qualification" details.
                DropDownList ddlQual = new DropDownList();
                ddlQual = (DropDownList)e.Row.FindControl("ddlFacultyName");

                if (ddlQual != null && dtFinal.Rows.Count > 0)
                {
                    ddlQual.DataSource = dtFinal;

                    ddlQual.DataBind();
                    ddlQual.SelectedIndex = 0;
                    // Assign the seleted row value (Qalification Code) to the DropDownList selected value.
                    //((DropDownList)e.Row.FindControl("ddlFacultyName")).SelectedValue =
                    //    DataBinder.Eval(e.Row.DataItem, "ddlFacultyName").ToString();
                }
                else ddlQual.Enabled = false;

                //// Get the LinkButton control in the first cell
                //LinkButton _singleClickButton = (LinkButton)e.Row.Cells[0].Controls[0];
                //// Get the javascript which is assigned to this LinkButton
                //string _jsSingle = ClientScript.GetPostBackClientHyperlink(
                //    _singleClickButton, "");

                ////// Add events to each editable cell
                ////for (int columnIndex = _firstEditCellIndex; columnIndex <
                ////    e.Row.Cells.Count; columnIndex++)
                ////{
                //    // Add the column index as the event argument parameter
                //    string js = _jsSingle.Insert(_jsSingle.Length - 2,
                //        "4");
                //    // Add this javascript to the onclick Attribute of the cell
                //    e.Row.Cells[4].Attributes["onclick"] = js;
                //    // Add a cursor style to the cells
                //    e.Row.Cells[4].Attributes["style"] +=
                //        "cursor:pointer;cursor:hand;";
                ////}

            }

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdFacultyWiseSubject, "Edit$" + e.Row.RowIndex);
            //    e.Row.Attributes["style"] = "cursor:pointer";
            //}
        }

        protected void grdFacultyWiseSubject_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //            int _rowIndex = int.Parse(e.CommandArgument.ToString());
            //            int _columnIndex = int.Parse(Request.Form["__EVENTARGUMENT"]);

            //            Control _displayControl =
            //grdFacultyWiseSubject.Rows[_rowIndex].Cells[_columnIndex].Controls[1];
            //            _displayControl.Visible = false;

            //            // Get the edit control for the selected cell and make it visible
            //            Control _editControl =
            //                grdFacultyWiseSubject.Rows[_rowIndex].Cells[_columnIndex].Controls[3];
            //            _editControl.Visible = true;
            //            // Clear the attributes from the selected cell to remove the click event
            //            grdFacultyWiseSubject.Rows[_rowIndex].Cells[_columnIndex].Attributes.Clear();


            //            ClientScript.RegisterStartupScript(GetType(), "SetFocus",
            //    "<script>document.getElementById('" + _editControl.ClientID + "').focus();</ script > ");
            //            // If the edit control is a dropdownlist set the
            //            // SelectedValue to the value of the display control
            //            if (_editControl is DropDownList && _displayControl is Label)
            //            {

            //                   ( (Label)_displayControl).Text = ((DropDownList)_editControl).SelectedValue;
            //            }

            //if (e.CommandName == "Update")
            //{
            //    int _rowIndex = int.Parse(e.CommandArgument.ToString());
            //    //int _columnIndex = int.Parse(Request.Form["__EVENTARGUMENT"]);

            //    int index = Convert.ToInt32(e.CommandArgument);
            //    GridViewRow selectedRow= grdFacultyWiseSubject.Rows[index];
            //    Control _editControl = selectedRow.FindControl("ddlFacultyName");
            //    Control _displayControl =   grdFacultyWiseSubject.Rows[_rowIndex].Cells[2].Controls[1];
            //    _displayControl.Visible = false;
            //    if (_editControl is DropDownList && _displayControl is Label)
            //    {
            //        if (((DropDownList)_editControl).SelectedItem != null)
            //            grdFacultyWiseSubject.Rows[index].Cells[2].Text = ((DropDownList)_editControl).SelectedItem.Text;
            //    }
            //}

        }

        protected void grdFacultyWiseSubject_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdFacultyWiseSubject.EditIndex = e.NewEditIndex;

            grdFacultyWiseSubject.DataSource = GetSubjectData();
            grdFacultyWiseSubject.DataBind();
        }

        protected void grdFacultyWiseSubject_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdFacultyWiseSubject.EditIndex = -1;
            grdFacultyWiseSubject.DataSource = GetSubjectData();
            grdFacultyWiseSubject.DataBind();
        }

        protected void grdFacultyWiseSubject_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //e.OldValues

        }

        protected void ddlFacultyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //grdFacultyWiseSubject.UpdateRow();
        }

        protected void grdFacultyWiseSubject_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void CalculateInstallment()
        {
            if (bResetCalculation)
            {
                try
                {
                    if (txtFinalFees.Text.Trim() != "")
                    {
                        BAL.Class.SmartInstitute.BatchClass o_GetBatch = new BAL.Class.SmartInstitute.BatchClass();
                        iNoOfInstallment = GetNoOfInstallment();
                        // double dd = Math.Round(Convert.ToDecimal(txtFinalFees.Text) / 2, 2).ToString();
                        if (iNoOfInstallment > 0)
                            txtFirstFee.Text = Math.Round(Convert.ToDecimal(txtFinalFees.Text) / iNoOfInstallment, 2).ToString();
                        else
                            txtFirstFee.Text = "0";
                        if (iNoOfInstallment > 1)
                            txtSecondFee.Text = Math.Round(Convert.ToDecimal(txtFinalFees.Text) / iNoOfInstallment, 2).ToString();
                        else
                            txtSecondFee.Text = "0";
                        if (iNoOfInstallment > 2)
                            txtThirdFee.Text = Math.Round(Convert.ToDecimal(txtFinalFees.Text) / iNoOfInstallment, 2).ToString();
                        else txtThirdFee.Text = "0";
                        if (iNoOfInstallment > 3)
                            txtFourthFee.Text = Math.Round(Convert.ToDecimal(txtFinalFees.Text) / iNoOfInstallment, 2).ToString();
                        else txtFourthFee.Text = "0";
                        if (iNoOfInstallment > 4)
                            txtFifthFee.Text = Math.Round(Convert.ToDecimal(txtFinalFees.Text) / iNoOfInstallment, 2).ToString();
                        else txtFifthFee.Text = "0";
                        if (iNoOfInstallment > 5)
                            txtSixthFee.Text = Math.Round(Convert.ToDecimal(txtFinalFees.Text) / iNoOfInstallment, 2).ToString();
                        else txtSixthFee.Text = "0";
                    }
                }
                catch (Exception err)
                {
                    lab_message.Text = err.Message;
                }
            }
        }
        protected void txtFinalFees_TextChanged(object sender, EventArgs e)
        {
            CalculateInstallment();
        }

        protected void btnReceipt_Click(object sender, EventArgs e)
        {
            if (txtAdmissionKey.Text.Trim() != "")
            {
                //string open = "window.open(../Reports/ReportViewerPage.aspx?AdmissionKey=" + Convert.ToInt32(txtAdmissionKey.Text) + " &ReportName=AdmissionPrint ,popup_window width=300,height=100,left=100,top=100,resizable=yes,toolbar=no,model=yes, scrollbars=yes,');";
                string open = "OpenPaymentReceipt(0," + txtAdmissionKey.Text + ",'AdmissionPrint')";
                ClientScript.RegisterStartupScript(this.GetType(), "script", open, true);
                //Response.Redirect("../Reports/ReportViewer.aspx?AdmissionKey=" + txtAdmissionKey.Text + "&ReportName=AdmissionPrint");
            }
            else lab_message.Text = "Save Admission first to generate receipt";
        }

        protected void grdFacultyWiseSubject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFacultyWiseSubject.PageIndex = e.NewPageIndex;
            grdFacultyWiseSubject.DataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Mode = "UPDATE";
            btn_EnquirySave_Click(sender, e);
        }
       
        private void SetDataOfAdmissionForEdit()
        {
            try {
                BAL.Class.SmartInstitute.AdmissionClass o_GetAdmDetails = new BAL.Class.SmartInstitute.AdmissionClass();
                DataTable dtAdmDetails = o_GetAdmDetails.GetAdmissionDetailsByID(Convert.ToInt32(txtAdmissionKey.Text), ref Message);
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
                    LoadBatchList();

                    BAL.Class.SmartInstitute.AdmissionClass o_GetEnq = new BAL.Class.SmartInstitute.AdmissionClass();
                    int batchKey = o_GetEnq.GetBatchKeyForAdmission(Convert.ToInt32(txtAdmissionKey.Text), ref Message);
                    try {
                        if (batchKey != 0)
                        {
                            if (ddlBatchList.Items.Count > 0)
                                ddlBatchList.SelectedValue = batchKey.ToString();
                        }
                    }
                    catch (Exception err)
                    { }
                    ddlBatchList_SelectedIndexChanged(null, null);
                    FillSubjectGridAndFee();
                    // else divGroupList.Visible = false;
                }
            }
            catch(Exception err)
            {
                lab_message.Text = err.Message;
            }
        }

        private void SetAdmissionReceiptData()
        {
            try {
                BAL.Class.SmartInstitute.PaymentReceiptClass o_GetPayRec = new BAL.Class.SmartInstitute.PaymentReceiptClass();
                DataTable dtPay = o_GetPayRec.GetAdmissionTypeFeePayment(Convert.ToInt32(txtAdmissionKey.Text), (int)FeeType.AmountToPayAtAdmission, ref Message);

                if (dtPay != null && dtPay.Rows.Count > 0)
                {
                    DataRow row = dtPay.Rows[0];
                    txtBankName.Text = row["BankBranchName"].ToString();
                    txtChequeNo.Text = row["ChequeNo"].ToString();
                    txtTransactionDate.Text = Convert.ToDateTime(Server.HtmlDecode(row["transactionDate"].ToString())).ToString("yyyy-MM-dd");
                    txtAmountToPay.Text = row["Fees"].ToString();
                    DDL_PayMode.Text = row["paymentMode"].ToString();
                    DDL_PayMode_SelectedIndexChanged(null, null);
                }
                else
                {
                    txtBankName.Text = "";
                    txtChequeNo.Text = "";
                    txtTransactionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txtAmountToPay.Text = "0";
                    DDL_PayMode.SelectedIndex = 0;
                    DDL_PayMode_SelectedIndexChanged(null, null);
                }

                txtAdmissionCharges.Text = o_GetPayRec.GetAdmissionCharges(Convert.ToInt32(txtAdmissionKey.Text), (int)FeeType.AdmissionCharges, ref Message).ToString();
            }
            catch(Exception err)
            {
                lab_message.Text = err.Message;
            }
        }
        private void setAdmissionData()
        {
            try {
                BAL.Class.SmartInstitute.EnquiryClass o_GetEnq = new BAL.Class.SmartInstitute.EnquiryClass();
                BAL.Class.SmartInstitute.AdmissionClass o_GetAdmission = new BAL.Class.SmartInstitute.AdmissionClass();
                DataSet dsAdmission = o_GetAdmission.GetAdmissionData(Convert.ToInt32(txtAdmissionKey.Text), ref Message);
                if (dsAdmission != null && dsAdmission.Tables != null && dsAdmission.Tables[0].Rows.Count > 0)
                {
                    DataRow row = dsAdmission.Tables[0].Rows[0];
                    txtEnqKey.Text = row["enquiryKey"].ToString();
                    if (txtEnqKey.Text.Trim() != "" && txtEnqKey.Text.Trim() != "0")
                        txtEnqID.Text = o_GetEnq.GetEnquiryIDGenerated(Convert.ToInt32(txtEnqKey.Text), ref Message);

                    if (row["AdminChargesInclude"] != null)
                        CheckBox_AdminCharges.Checked = Convert.ToBoolean(row["AdminChargesInclude"].ToString());
                    else
                        CheckBox_AdminCharges.Checked = false;

                    if (row["AddedInApp"] != null)
                        CheckBox_AddedInApp.Checked = Convert.ToBoolean(row["AddedInApp"].ToString());
                    else
                        CheckBox_AddedInApp.Checked = false;

                    txtAdmissionDate.Text = Convert.ToDateTime(row["admissionDate"]).ToString("yyyy-MM-dd");
                    txtDOB.Text = Convert.ToDateTime(row["DOB"]).ToString("yyyy-MM-dd");
                    string[] source = row["source"].ToString().Split(new string[] { "," }, StringSplitOptions.None);

                    chkSourceList.ClearSelection();
                    for (int i = 0; i < source.Length; i++)
                    {
                        foreach (ListItem item in chkSourceList.Items)
                        {
                            if (item.Text == source[i])
                                item.Selected = true;
                            //else item.Selected = false;
                            //if (item.Text)
                            //{

                            //}
                        }
                    }
                }
                SetAdmissionReceiptData();
            }
            catch(Exception err)
            {
                lab_message.Text = err.Message;
            }
        }
        protected void txtAdmissionID_TextChanged(object sender, EventArgs e)
        {
            if (bEditRights)
            {
                this.txtFirstFee.TextChanged -= this.txtFirstFee_TextChanged;
                this.txtSecondFee.TextChanged -= this.txtSecondFee_TextChanged;
                this.txtThirdFee.TextChanged -= this.txtThirdFee_TextChanged;
                this.txtFourthFee.TextChanged -= this.txtFourthFee_TextChanged;
                this.txtFifthFee.TextChanged -= this.txtFifthFee_TextChanged;
                this.txtSixthFee.TextChanged -= this.txtSixthFee_TextChanged;

                bResetCalculation = false;
                ChangeFinalFee = false;
                if (txtAdmissionKey.Text != "" && txtAdmissionKey.Text != "0")
                {
                    lblFinalFeeFromDB.Text = "Final Fee from DB=" + txtFinalFees.Text;
                    btnEdit.Enabled = true;
                    btn_EnquirySave.Enabled = false;
                    setAdmissionData();
                    SetDataOfAdmissionForEdit();
                    btnShowFaculty_Click(null, null);
                    ddlBatchList_SelectedIndexChanged(null, null);

                    pnlPayment.Enabled = false;
                }
                ChangeFinalFee = true;
            }
            else
            {
                btnEdit.Enabled = false;
                lab_message.Text = "You are not having edit rights";
            }
            //this.txtFirstFee.TextChanged += this.txtFirstFee_TextChanged;
            //this.txtSecondFee.TextChanged += this.txtSecondFee_TextChanged;
            //this.txtThirdFee.TextChanged += this.txtThirdFee_TextChanged;
            //this.txtFourthFee.TextChanged += this.txtFourthFee_TextChanged;
            //this.txtFifthFee.TextChanged += this.txtFifthFee_TextChanged;
            //this.txtSixthFee.TextChanged += this.txtSixthFee_TextChanged;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (bDeleteRights)
            {
                try
                {
                    if (txtAdmissionKey.Text != "" && txtAdmissionKey.Text != "0")
                    {
                        BAL.Class.SmartInstitute.AdmissionClass o_GetAdmission = new BAL.Class.SmartInstitute.AdmissionClass();
                        o_GetAdmission.delete(ref Message, "DELETE", Convert.ToInt32(txtAdmissionKey.Text));
                        lab_message.Text = "Admission deleted successfully";
                    }
                    else
                    {
                        lab_message.Text = "select admission to delete";
                    }
                }
                catch (Exception err)
                {
                    lab_message.Text = err.Message.ToString();
                }
            }
            else
            {
                lab_message.Text = "You are not allowed to delete records. Please contact administrator";
            }
        }
        private void SetAllInstallmentDisable()
        {
            lblFirstInstallment.Enabled = false;
            txtFirstFee.Enabled = false;
            lblSecondInstallment.Enabled = false;
            txtSecondFee.Enabled = false;
            lblThirdInstallment.Enabled = false;
            txtThirdFee.Enabled = false;
            lblFourthInstallment.Enabled = false;
            txtFourthFee.Enabled = false;
            lblFifthInstallment.Enabled = false;
            txtFifthFee.Enabled = false;
            lblSixthInstallment.Enabled = false;
            txtSixthFee.Enabled = false;
        }
        protected void ddlBatchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBatchList.SelectedItem != null && ddlBatchList.SelectedValue != "")
            {
                SetAllInstallmentDisable();
                BAL.Class.SmartInstitute.BatchClass o_GetBatch = new BAL.Class.SmartInstitute.BatchClass();
                iNoOfInstallment = o_GetBatch.GetNoOfInstallmentAsPerBatch(Convert.ToInt32(ddlBatchList.SelectedValue), ref Message);
                if (iNoOfInstallment > 0)
                {
                    
                    for (int i = 1; i <= iNoOfInstallment; i++)
                    {
                        if (i == 1)
                        {
                            lblFirstInstallment.Enabled = true;
                            txtFirstFee.Enabled = true;
                        }
                        else if(i==2)
                        {
                            lblSecondInstallment.Enabled = true;
                            txtSecondFee.Enabled = true;
                        }
                        else if (i == 3)
                        {
                            lblThirdInstallment.Enabled = true;
                            txtThirdFee.Enabled = true;
                        }
                        else if (i == 4)
                        {
                            lblFourthInstallment.Enabled = true;
                            txtFourthFee.Enabled = true;
                        }
                        else if (i == 5)
                        {
                            lblFifthInstallment.Enabled = true;
                            txtFifthFee.Enabled = true;
                        }
                        else if (i == 6)
                        {
                            lblSixthInstallment.Enabled = true;
                            txtSixthFee.Enabled = true;
                        }
                    }
                    CalculateInstallment();
                }
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
        private int GetNoOfInstallment()
        {
            BAL.Class.SmartInstitute.BatchClass o_GetBatch = new BAL.Class.SmartInstitute.BatchClass();
            iNoOfInstallment = 0;
            if (ddlBatchList.SelectedItem != null && ddlBatchList.SelectedValue.Trim() != "")
            {
                iNoOfInstallment = o_GetBatch.GetNoOfInstallmentAsPerBatch(Convert.ToInt32(ddlBatchList.SelectedValue), ref Message);
            }
            return iNoOfInstallment;
        }
       
        private void DistributeNextInstallmentEqually(int iInstallmentNo)
        {
            iNoOfInstallment = GetNoOfInstallment();
            decimal remainAmt = 0;
            if(iInstallmentNo==1)
            {
                remainAmt = Convert.ToDecimal(txtFinalFees.Text.Trim()) - Convert.ToDecimal(txtFirstFee.Text.Trim());
                if(remainAmt<0)
                {
                    lab_message.Text = "Installment addition is not equal to final amount. Please review.";
                    txtSecondFee.Text = "0";
                    txtThirdFee.Text = "0";
                    txtFourthFee.Text = "0";
                    txtFifthFee.Text = "0";
                    txtSixthFee.Text = "0";
                    return;
                }
                if (remainAmt > 0)
                {
                    decimal EqualAmt = Math.Round(remainAmt / (iNoOfInstallment - 1), 2);
                    if (iNoOfInstallment > 1)
                        txtSecondFee.Text = EqualAmt.ToString();
                    if (iNoOfInstallment > 2)
                        txtThirdFee.Text = EqualAmt.ToString();
                    if (iNoOfInstallment > 3)
                        txtFourthFee.Text = EqualAmt.ToString();
                    if (iNoOfInstallment > 4)
                        txtFifthFee.Text = EqualAmt.ToString();
                    if (iNoOfInstallment > 5)
                        txtSixthFee.Text = EqualAmt.ToString();
                }
                else
                {
                    txtSecondFee.Text = "0";
                    txtThirdFee.Text = "0";
                    txtFourthFee.Text = "0";
                    txtFifthFee.Text = "0";
                    txtSixthFee.Text = "0";
                }
            }
            else if (iInstallmentNo == 2)
            {
                remainAmt = Convert.ToDecimal(txtFinalFees.Text.Trim()) - (Convert.ToDecimal(txtSecondFee.Text.Trim())+Convert.ToDecimal(txtFirstFee.Text.Trim()));
                if (remainAmt < 0)
                {
                    lab_message.Text = "Installment addition is not equal to final amount. Please review.";
                    txtThirdFee.Text = "0";
                    txtFourthFee.Text = "0";
                    txtFifthFee.Text = "0";
                    txtSixthFee.Text = "0";
                    return;
                }
                if (remainAmt > 0)
                {
                    decimal EqualAmt = Math.Round(remainAmt / (iNoOfInstallment - 2), 2);
                    if (iNoOfInstallment > 2)
                        txtThirdFee.Text = EqualAmt.ToString();
                    if (iNoOfInstallment > 3)
                        txtFourthFee.Text = EqualAmt.ToString();
                    if (iNoOfInstallment > 4)
                        txtFifthFee.Text = EqualAmt.ToString();
                    if (iNoOfInstallment > 5)
                        txtSixthFee.Text = EqualAmt.ToString();
                }
                else
                {
                    txtThirdFee.Text = "0";
                    txtFourthFee.Text = "0";
                    txtFifthFee.Text = "0";
                    txtSixthFee.Text = "0";
                }
            }
            else if (iInstallmentNo == 3)
            {
                remainAmt = Convert.ToDecimal(txtFinalFees.Text.Trim()) - (Convert.ToDecimal(txtThirdFee.Text.Trim())+Convert.ToDecimal(txtSecondFee.Text.Trim())+Convert.ToDecimal(txtFirstFee.Text.Trim()));
                if (remainAmt < 0)
                {
                    lab_message.Text = "Installment addition is not equal to final amount. Please review.";
                    txtFourthFee.Text = "0";
                    txtFifthFee.Text = "0";
                    txtSixthFee.Text = "0";
                    return;
                }
                if (remainAmt > 0)
                {
                    decimal EqualAmt = Math.Round(remainAmt / (iNoOfInstallment - 3), 2);
                    if (iNoOfInstallment > 3)
                        txtFourthFee.Text = EqualAmt.ToString();
                    if (iNoOfInstallment > 4)
                        txtFifthFee.Text = EqualAmt.ToString();
                    if (iNoOfInstallment > 5)
                        txtSixthFee.Text = EqualAmt.ToString();
                }
                else
                {
                    txtFourthFee.Text = "0";
                    txtFifthFee.Text = "0";
                    txtSixthFee.Text = "0";
                }
            }
            else if (iInstallmentNo == 4)
            {
                remainAmt = Convert.ToDecimal(txtFinalFees.Text.Trim()) - (Convert.ToDecimal(txtFourthFee.Text.Trim())+Convert.ToDecimal(txtThirdFee.Text.Trim())+Convert.ToDecimal(txtSecondFee.Text.Trim())+Convert.ToDecimal(txtFirstFee.Text.Trim()));
                if (remainAmt < 0)
                {
                    lab_message.Text = "Installment addition is not equal to final amount. Please review.";
                    txtFifthFee.Text = "0";
                    txtSixthFee.Text = "0";
                    return;
                }
                if (remainAmt > 0)
                {
                    decimal EqualAmt = Math.Round(remainAmt / (iNoOfInstallment - 4), 2);
                    if (iNoOfInstallment > 4)
                        txtFifthFee.Text = EqualAmt.ToString();
                    if (iNoOfInstallment > 5)
                        txtSixthFee.Text = EqualAmt.ToString();
                }
                else
                {
                    txtFifthFee.Text = "0";
                    txtSixthFee.Text = "0";
                }
            }
            else if (iInstallmentNo == 5)
            {
                remainAmt = Convert.ToDecimal(txtFinalFees.Text.Trim()) - (Convert.ToDecimal(txtFifthFee.Text.Trim())+Convert.ToDecimal(txtFourthFee.Text.Trim())+Convert.ToDecimal(txtThirdFee.Text.Trim())+Convert.ToDecimal(txtSecondFee.Text.Trim())+Convert.ToDecimal(txtFirstFee.Text.Trim()));
                if (remainAmt < 0)
                {
                    lab_message.Text = "Installment addition is not equal to final amount. Please review.";
                    txtSixthFee.Text = "0";
                    return;
                }
                if (remainAmt > 0)
                {
                    decimal EqualAmt = Math.Round(remainAmt / (iNoOfInstallment - 5), 2);
                    if (iNoOfInstallment > 5)
                        txtSixthFee.Text = EqualAmt.ToString();
                }
                else txtSixthFee.Text = "0";
            }
        }
        protected void txtFirstFee_TextChanged(object sender, EventArgs e)
        {
            lab_message.Text = "";
            if (txtFinalFees.Text.Trim() != "")
            {
                if (txtFirstFee.Text.Trim() != "")
                {
                    if (Convert.ToDecimal(txtFinalFees.Text.Trim()) >= Convert.ToDecimal(txtFirstFee.Text.Trim()))
                    {
                        DistributeNextInstallmentEqually(1);
                    }
                    else
                        lab_message.Text = "Installment addition is not equal to final amount. Please review.";
                }
            }
            
        }
        

        protected void txtSecondFee_TextChanged(object sender, EventArgs e)
        {
            lab_message.Text = "";
            if (txtFinalFees.Text.Trim() != "")
            {
                if (txtSecondFee.Text.Trim() != "")
                {
                    if (Convert.ToDecimal(txtFinalFees.Text.Trim()) >= Convert.ToDecimal(txtSecondFee.Text.Trim()))
                    {
                        DistributeNextInstallmentEqually(2);
                    }
                    else
                        lab_message.Text = "Installment addition is not equal to final amount. Please review.";

                }
            }
        }

        protected void txtThirdFee_TextChanged(object sender, EventArgs e)
        {
            lab_message.Text = "";
            if (txtFinalFees.Text.Trim() != "")
            {
                if (txtThirdFee.Text.Trim() != "")
                {
                    if (Convert.ToDecimal(txtFinalFees.Text.Trim()) >= Convert.ToDecimal(txtThirdFee.Text.Trim()))
                    {
                        DistributeNextInstallmentEqually(3);
                    }
                    else
                        lab_message.Text = "Installment addition is not equal to final amount. Please review.";

                }
            }
        }

        protected void txtFourthFee_TextChanged(object sender, EventArgs e)
        {
            lab_message.Text = "";
            if (txtFinalFees.Text.Trim() != "")
            {
                if (txtFourthFee.Text.Trim() != "")
                {
                    if (Convert.ToDecimal(txtFinalFees.Text.Trim()) >= Convert.ToDecimal(txtFourthFee.Text.Trim()))
                    {
                        DistributeNextInstallmentEqually(4);
                    }
                    else
                        lab_message.Text = "Installment addition is not equal to final amount. Please review.";
                }
            }
        }

        protected void txtFifthFee_TextChanged(object sender, EventArgs e)
        {
            lab_message.Text = "";
            if (txtFinalFees.Text.Trim() != "")
            {
                if (txtFifthFee.Text.Trim() != "")
                {
                    if (Convert.ToDecimal(txtFinalFees.Text.Trim()) >= Convert.ToDecimal(txtFifthFee.Text.Trim()))
                    {
                        DistributeNextInstallmentEqually(5);
                    }
                    else
                        lab_message.Text = "Installment addition is not equal to final amount. Please review.";
                }
            }
        }

        protected void txtSixthFee_TextChanged(object sender, EventArgs e)
        {
            lab_message.Text = "";
            if (txtFinalFees.Text.Trim() != "")
            {
                if (txtSixthFee.Text.Trim() != "")
                {
                    if (Convert.ToDecimal(txtFinalFees.Text.Trim()) >= Convert.ToDecimal(txtSixthFee.Text.Trim()))
                    {
                        DistributeNextInstallmentEqually(6);
                    }
                    else
                        lab_message.Text = "Installment addition is not equal to final amount. Please review.";

                }
            }
        }

        protected void btnAdmissionChargePrint_Click(object sender, EventArgs e)
        {
            if (CheckBox_AdminCharges.Checked && txtAdmissionCharges.Text.Trim() != "" && Convert.ToDecimal(txtAdmissionCharges.Text) > 0)
            {
                PrintAdmissionPaymentReceipt(1);
            }
            else lab_message.Text = "Admission charges are not paid";
        }

        private void PrintAdmissionPaymentReceipt(int iFeeType)
        {
            if (txtAdmissionKey.Text.Trim() != "" && txtAdmissionKey.Text.Trim() != "0")
            {
                BAL.Class.SmartInstitute.PaymentReceiptClass o_GetReceiptNo = new BAL.Class.SmartInstitute.PaymentReceiptClass();
                int ReceiptNo = o_GetReceiptNo.GetReceiptNoOfPayment(Convert.ToInt32(txtAdmissionKey.Text), iFeeType, ref Message);

                //string open = "window.open(../Reports/ReportViewerPage.aspx?ReceiptNo=" + ReceiptNo + " &AdmissionKey=" + Convert.ToInt32(txtAdmissionKey.Text) + " &ReportName=ReceiptPrint ,popup_window width=300,height=100,left=100,top=100,resizable=yes,toolbar=no,model=yes, scrollbars=yes,');";
                string open = "OpenPaymentReceipt(" + ReceiptNo + "," + txtAdmissionKey.Text + ",'ReceiptPrint')";
                ClientScript.RegisterStartupScript(this.GetType(), "script", open, true);


                //Response.Redirect("../Reports/ReportViewerPage.aspx?ReceiptNo=" + Txt_ReceiptNo.Text + "&AdmissionKey=" + txtAddmissionKey.Text + "&ReportName=ReceiptPrint");
            }
            else lab_message.Text = "Save Admission first to generate receipt";
        }
        protected void btnPrintAdmissionAmountPrint_Click(object sender, EventArgs e)
        {
            if (txtAmountToPay.Text.Trim() != "" && Convert.ToDecimal(txtAmountToPay.Text) > 0)
            {
                PrintAdmissionPaymentReceipt(2);
            }
            else
            {
                lab_message.Text = "No amount is paid at the time of admission";
            }
        }
    }
}