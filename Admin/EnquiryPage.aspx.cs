using BAL.Class.SmartInstitute;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Admin
{
    public partial class EnquiryPage : System.Web.UI.Page
    {
        string Message = string.Empty;
        int iMenuKey = 9;
        string Mode = "INSERT";
        static bool bEditRights = true;
        static bool bAddRights = true;
        static bool bDeleteRights = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            Message = string.Empty;
            btnSendOTP.Enabled = false;
            btnVerifyOTP.Enabled = false;
            lblCollegeName.Text = "Previous School Name";
            //txtCollegeName.Visible = false;

            lab_message.ForeColor = System.Drawing.Color.Red;
            if (!IsPostBack)
            {
                LoadEnquiryTypeList();
                LoadCouseList();
                LoadSubCourseList();
                LoadGroupList();
                LoadSubjectData();
                LoadBatchList();
                ApplyLoginEmpRights();
            
                txtEnquiryDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); //DateTime.Now.ToShortDateString();
                if (txtEnqID.Text.Trim() == "")
                    txtEnqID.Attributes.Add("placeholder", "Auto Generated After Save");

                txtEnquiryOwner.Text = Session["LoginEmpName"].ToString();
                txtEnqID.Attributes.Add("readonly", "readonly");
                btnEdit.Enabled = false;
                btn_EnquirySave.Enabled = true;
                btnDelete.Enabled = false;
            }

            //GridView1.DataBind();
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
                            btnDelete.Enabled = true;
                        }
                        else
                        {
                            bDeleteRights = false;
                            btnDelete.Enabled = false;
                        }

                    }
                }
            }
        }
        private void LoadEnquiryTypeList()
        {
            BAL.Class.SmartInstitute.EnquiryTypeMaster o_EqnuiryType = new BAL.Class.SmartInstitute.EnquiryTypeMaster();
            ddlEnquiryType.DataSource = o_EqnuiryType.GetEnquiryTypeList(ref Message);
            ddlEnquiryType.DataBind();
        }
        private void LoadBatchList()
        {
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
                    chkSelectAllSubject.Visible = true;
                else chkSelectAllSubject.Visible = false;
            }
            else if (rdoSubCourseList.SelectedValue != null && rdoSubCourseList.SelectedValue.Trim() != "")
            {
                BAL.Class.SmartInstitute.SubjectClass o_GetSubject = new BAL.Class.SmartInstitute.SubjectClass();
                DataTable dtChildSubc = o_GetSubject.GetSubjectListForSubCourse(iCourseKey, iSubCourseKey, ref Message);
                chkSubjectList.DataSource = dtChildSubc;
                chkSubjectList.DataBind();
                if (dtChildSubc != null && dtChildSubc.Rows.Count > 0)
                    chkSelectAllSubject.Visible = true;
                else chkSelectAllSubject.Visible = false;
            }
            //if (!IsPostBack)
            //    chkSubjectList.SelectedIndex = 0;
        }
        private void LoadSubCourseList()
        {
            int iSelectedSubCourseInx = 0;
            if (rdoSubCourseList.SelectedIndex != null && rdoSubCourseList.SelectedItem != null)
                iSelectedSubCourseInx = rdoSubCourseList.SelectedIndex;

            BAL.Class.SmartInstitute.SubCourseClass o_GetSubCourse = new BAL.Class.SmartInstitute.SubCourseClass();

            DataTable dtChild = null;


            if (rdoListCourse.SelectedItem != null && rdoListCourse.SelectedItem.Value != null)
            {
                dtChild = o_GetSubCourse.GetSubCourseListForCourse(Convert.ToInt32(rdoListCourse.SelectedItem.Value), ref Message);
                rdoSubCourseList.DataSource = dtChild;
                rdoSubCourseList.DataBind();
                //if (!IsPostBack)
                rdoSubCourseList.SelectedIndex = iSelectedSubCourseInx;

            }
        }
        
        public static string GetResponse(string sURL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
            request.MaximumAutomaticRedirections = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream(
                );
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string sResponse = readStream.ReadToEnd();
                var serializer = new JavaScriptSerializer();
                // C2SMSJsonClass myDeserializedObj = (C2SMSJsonClass)JavaScriptConverter.DeserializeObject(Request["jsonString"], typeof(C2SMSJsonClass));
                var deserializedResult = serializer.Deserialize<List<C2SMSJsonClass>>(sResponse);
                //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //var jsonObject = serializer.DeserializeObject(readStream.ReadToEnd());

                response.Close();
                readStream.Close();
                return sResponse;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        private int GenerateOPT()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
        private void SetSMSCredentials()
        {
            string sAPIKey = "sNW7KTE5lEui7pqp69myoA";
            string sNumber = txtMobileNo.Text;
            int iOTP = GenerateOPT();
            Session["OTP"] = iOTP;
            string sMessage = iOTP + " is OTP to validate your mobile number";
            //string sSenderID = "VPAedu";
            //string sChannel = "Trans";
            //string sRoute = "15";
            string sURLNew = "http://neo.c2sms.com/api/mt/SendSMS?user=vpangp&password=Vpa@1234&senderid=VPAedu&channel=Trans&DCS=0&flashsms=0&number=" + sNumber
                + "&text=" + sMessage + "&route=15";
            //string sURL = "http://neo.c2sms.com/api/mt/SendSMS?APIKEY=" + sAPIKey + "&senderid=" + sSenderID + "&channel=" + sChannel + "&DCS=0&flashsms=0&number=" + sNumber + "&text=" + sMessage + "&route=" + sRoute + ";";
            string sResponse = GetResponse(sURLNew);

            var serializer = new JavaScriptSerializer();
            // C2SMSJsonClass myDeserializedObj = (C2SMSJsonClass)JavaScriptConverter.DeserializeObject(Request["jsonString"], typeof(C2SMSJsonClass));
            var deserializedResult = serializer.Deserialize<List<C2SMSJsonClass>>(sResponse);

            labOTP.Text = "Generated OTP succesfully";


        }
        protected void btnSendOTP_Click(object sender, EventArgs e)
        {
            if (txtMobileNo.Text.Trim() != "")
            {
                //SetSMSCredentials();               
            }
            else
            {
                lab_message.Text = "Enter Mobile number";               
            }
            //labOTP.Text = "2222";
        }

        protected void btnVerifyOTP_Click(object sender, EventArgs e)
        {
            lab_message.Text = "";
            if (Session["OTP"] != null)
            {
                if (txtOTP.Text.Trim() != "")
                {
                    if (txtOTP.Text == Session["OTP"].ToString())
                    //if (Convert.ToString(labOTP.Text) == Convert.ToString(txtOTP.Text))
                    {
                        labOTPVeriMessage.Text = "OTP verified successfully";
                    }
                    else
                    {
                        labOTPVeriMessage.Text = "Inalid OTP";
                    }
                }
                else
                    lab_message.Text = "Please enter OTP to verify.";
            }
            else
            {
                lab_message.Text = "OTP is not generated";
            }
        }
        private bool ValidateData()
        {
            lab_message.Text = "";
            if (Server.HtmlDecode(txtMobileNo.Text).Trim() == "")
                lab_message.Text = "Mobile Number is required field";
            else if (Server.HtmlDecode(txtFirstName.Text).Trim() == "")
                lab_message.Text = "First name is required field";

            else if (Server.HtmlDecode(chkSubjectList.Text).Trim() == "")
            {
                lab_message.Text = "Subject is required field";
                return false;
            }
            else if (Server.HtmlDecode(txtEstimatedFees.Text).Trim() == "")
            {
                lab_message.Text = "Estimated Fees is required field";
                return false;
            }
            else if (Server.HtmlDecode(txtFinalFees.Text).Trim() == "")
            {
                lab_message.Text = "Final Fees is required field";
                return false;
            }
            else if ((Convert.ToDecimal(txtFinalFees.Text)) > (Convert.ToDecimal(txtEstimatedFees.Text)))
            {
                lab_message.Text = "Final Fees should be less than Estimated fees";
                return false;
            }
            else if (Server.HtmlDecode(ddlBatchList.Text).Trim() == "")
            {
                lab_message.Text = "Batch is required field";
                return false;
            }

            if (lab_message.Text.Trim() == "")
                return true;
            else return false;
        }
        protected void btn_EnquirySave_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                //string mode = "INSERT";
                // Save Course Master DATA Insert into m_course table//         

                Message = string.Empty;

                BAL.Class.SmartInstitute.EnquiryClass o_SaveEnquiry = new BAL.Class.SmartInstitute.EnquiryClass();

                // int EnquiryKey = 99; //Temp
                if (txtEnqKey.Text.Trim() != "" && txtEnqKey.Text.Trim() != "0")
                {
                    o_SaveEnquiry.enquirykey = Convert.ToInt16(txtEnqKey.Text.Trim());
                    o_SaveEnquiry.enquiryCode = txtEnqID.Text.Trim();
                    Mode = "UPDATE";
                }
                else
                {
                    o_SaveEnquiry.enquiryCode = "";
                    o_SaveEnquiry.enquirykey = 0;
                    Mode = "INSERT";
                }

                o_SaveEnquiry.mobileno = txtMobileNo.Text;
                o_SaveEnquiry.enquiryDate = Convert.ToDateTime(txtEnquiryDate.Text);

                if (ddlEnquiryType.SelectedItem != null)
                    o_SaveEnquiry.enquiryTypeKey = Convert.ToInt32(ddlEnquiryType.SelectedValue);
                else
                    o_SaveEnquiry.enquiryTypeKey = 0;

                o_SaveEnquiry.firstName = Server.HtmlDecode(txtFirstName.Text);
                o_SaveEnquiry.lastName = Server.HtmlDecode(txtLastName.Text);
                o_SaveEnquiry.emailID = txtemailID.Text;
                o_SaveEnquiry.mobilenoAlt = txtaltmobileno.Text;
                if (RadioButtonM.Checked)
                    o_SaveEnquiry.gender = 1;
                else
                   if (RadioButtonF.Checked)
                    o_SaveEnquiry.gender = 2;

                if (txtFinalFees.Text == "")
                    txtFinalFees.Text = "0";

                o_SaveEnquiry.TotalFees = Convert.ToDecimal(txtFinalFees.Text);

                if (txtEstimatedFees.Text == "")
                    txtEstimatedFees.Text = "0";

                o_SaveEnquiry.EstimatedFees = Convert.ToDecimal(txtEstimatedFees.Text);

                o_SaveEnquiry.collegeName = txtCollegeName.Text;
                o_SaveEnquiry.refByName = txtRefByName.Text;
                o_SaveEnquiry.refByPhoneNo = txtRefByMobileNo.Text;
                o_SaveEnquiry.Notes = txt_Remarks.Text;

                if (ddlBatchList.SelectedItem != null)
                    o_SaveEnquiry.batchKey = Convert.ToInt32(ddlBatchList.SelectedValue);
                else
                    o_SaveEnquiry.batchKey = 0;

                o_SaveEnquiry.createdBy = Convert.ToInt32(Session["LoginEmpKey"]);
                o_SaveEnquiry.modifiedBy = Convert.ToInt32(Session["LoginEmpKey"]);

                Message = string.Empty;

                int iEnqKey = o_SaveEnquiry.save(ref Message, Mode);
                if (iEnqKey > 0)
                {

                    // SaveCoursesData(iEnqKey, mode);
                    if (SaveEnquiryDetails(iEnqKey, Mode, ref Message))
                    {
                        if (Mode == "INSERT")
                            lab_message.Text = "Enquiry saved successfully.";
                        else if (Mode == "UPDATE")
                            lab_message.Text = "Enquiry updated successfully.";
                        lab_message.ForeColor = System.Drawing.Color.Green;

                        txtEnqKey.Text = iEnqKey.ToString();
                        txtEnqID.Text = o_SaveEnquiry.GetEnquiryIDGenerated(iEnqKey, ref Message);
                        btn_EnquirySave.Enabled = false;
                        btnEdit.Enabled = true;
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
        private bool SaveEnquiryDetails(int iEqnuiryKey, string mode, ref string Message)
        {
            try
            {
                int iCourseKey = 0;
                int isubCourseKey = 0;
                int iSubjectKey = 0;
                int isubGroupKey = 0;

                EnquiryDetails bal = new EnquiryDetails();
                bal.DeleteAllDetailsOfEnquiry(iEqnuiryKey, ref Message);

                if (rdoListCourse.SelectedItem != null)
                    iCourseKey = Convert.ToInt32(rdoListCourse.SelectedValue);
                if (rdoSubCourseList.SelectedItem != null)
                    isubCourseKey = Convert.ToInt32(rdoSubCourseList.SelectedValue);
                if (rdoGroupList.SelectedItem != null && divGroupList.Visible)
                    isubGroupKey = Convert.ToInt32(rdoGroupList.SelectedValue);
                
                if (Message.Trim() == "")
                {
                    foreach (ListItem item in chkSubjectList.Items)
                    {
                        if (item.Selected)
                        {
                            iSubjectKey = Convert.ToInt32(item.Value);
                            SaveEnquiryDetails(0, iEqnuiryKey, iCourseKey, isubCourseKey, isubGroupKey, iSubjectKey, "INSERT");
                        }
                    }
                }
                else
                {
                    lab_message.Text = Message;
                }
                return true;

            }
            catch (Exception err)
            {
                Message = err.Message.ToString();
                return false;
            }
        }
        
        private void SaveEnquiryDetails(int enquiryDetailKey, int enquirykey, int CourseKey, int subcourseKey, int groupsubcourseKey, int subjectKey, string mode)
        {
            int retval = 0;
            try
            {
                BAL.Class.SmartInstitute.EnquiryDetails o_SaveEnquirydtl = new BAL.Class.SmartInstitute.EnquiryDetails();

                o_SaveEnquirydtl.enquiryDetailKey = enquiryDetailKey;
                o_SaveEnquirydtl.enquirykey = enquirykey;
                o_SaveEnquirydtl.CourseKey = CourseKey;
                o_SaveEnquirydtl.subcourseKey = subcourseKey;
                o_SaveEnquirydtl.groupsubcourseKey = groupsubcourseKey;
                o_SaveEnquirydtl.subjectKey = subjectKey;

                retval = o_SaveEnquirydtl.save(ref Message, mode);
            }
            catch (Exception err)
            { }


        }
        protected void btn_Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/WellcomePage.aspx");
        }

        protected void rdoListCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            //PopulateSubCourseView(dtChild, 0, null);
        }
        private void ClearControls()
        {
            txtEnqID.Text = "";
            if (ddlBatchList.Items.Count > 0)
                ddlBatchList.SelectedIndex = 0;
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtMobileNo.Text = "";
            txtaltmobileno.Text = "";
            txtCollegeName.Text = "";
            txtemailID.Text = "";
            txtEnquiryDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEnquiryOwner.Text = Session["LoginEmpName"].ToString();
            txtOTP.Text = "";
            txtRefByMobileNo.Text = "";
            txtRefByName.Text = "";
            txt_Remarks.Text = "";
            rdoListCourse.SelectedIndex = 0;
            ddlEnquiryType.SelectedIndex = 0;
            labOTPVeriMessage.Text = "";
            txtFinalFees.Text = "0";
            txtEstimatedFees.Text = "0";
            lab_message.Text = "";

            ClearSubjectCheckBox();
            chkSelectAllSubject.Checked = false;
            txtEnqKey.Text = "0";
            lblFinalFeeFromDB.Text = "";
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearControls();
            btn_EnquirySave.Enabled = true;
            btnEdit.Enabled = false;
            btn_EnquirySave.Enabled = true;
            Mode = "INSERT";
            
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
        }

        protected void rdoGroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubjectData();
            LoadBatchList();

        }
        private void ClearSubjectCheckBox()
        {
            foreach (ListItem item in chkSubjectList.Items)
            {
                item.Selected = false;
            }
        }
        protected void chkSubjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BAL.Class.SmartInstitute.SubjectClass o_GetSubject = new BAL.Class.SmartInstitute.SubjectClass();
            decimal dEstimatedFees = 0;
            decimal dSubjectFees = 0;
            foreach (ListItem item in chkSubjectList.Items)
            {
                if (item.Selected)
                {
                    dSubjectFees = o_GetSubject.GetSubjectFees(Convert.ToInt32(item.Value), ref Message);
                    dEstimatedFees = dEstimatedFees + dSubjectFees;
                    int iSubjectKey = Convert.ToInt32(item.Value);
                    //SaveEnquiryDetails(0, iEqnuiryKey, iCourseKey, isubCourseKey, isubGroupKey, iSubjectKey, mode);
                }
            }
            txtEstimatedFees.Text = dEstimatedFees.ToString();
            if (sender == null)
                lblFinalFeeFromDB.Text = "Final Fee from DB=" + txtFinalFees.Text;

            if (sender != null)// if (txtFinalFees.Text.Trim() == "" || Convert.ToDecimal(txtFinalFees.Text.Trim()) <= 0)
                txtFinalFees.Text = txtEstimatedFees.Text;

            SetCheckAllStatus();
        }
        private void SetCheckAllStatus()
        {
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
        }
        protected void chkSelectAllSubject_CheckedChanged(object sender, EventArgs e)
        {
            BAL.Class.SmartInstitute.SubjectClass o_GetSubject = new BAL.Class.SmartInstitute.SubjectClass();
            decimal dEstimatedFees = 0;
            decimal dSubjectFees = 0;            

            bool isChecked = false;
            if (chkSelectAllSubject.Checked)
            {
                isChecked = true;
            }
            foreach (ListItem item in chkSubjectList.Items)
            {
                item.Selected = isChecked;
                if (isChecked)
                {
                    dSubjectFees = o_GetSubject.GetSubjectFees(Convert.ToInt32(item.Value), ref Message);
                    dEstimatedFees = dEstimatedFees + dSubjectFees;
                }
            }
            txtEstimatedFees.Text = dEstimatedFees.ToString();
            txtFinalFees.Text = dEstimatedFees.ToString();

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Mode = "UPDATE";
            btn_EnquirySave_Click(sender, e);
        }

        protected void txtEnqID_TextChanged(object sender, EventArgs e)
        {

            if (txtEnqKey.Text.Trim() != "")
            {
                setTreeViewNodesAsPerEnquiry();
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btn_EnquirySave.Enabled = false;
            }
            if (!bEditRights)
            {
                btnEdit.Enabled = false;                
                lab_message.Text = "Yoy are not having edit rights";
            }
            else if(!bDeleteRights)
            {
                btnDelete.Enabled = false;
                lab_message.Text = "Yoy are not having delete rights";
            }
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
                    //  if(rdoSubCourseList.Items.Contains())
                    try
                    {
                        rdoSubCourseList.SelectedValue = rowSubCourse["subcoursekey"].ToString();
                    }
                    catch(Exception err)
                    { }
                }

                LoadGroupList();
                DataTable dtgroupCourseKey = dtEnqDetails.AsEnumerable().GroupBy(r => r.Field<int>("groupsubcoursekey")).Select(g => g.First()).CopyToDataTable();
                DataRow rowGroupCourse = null;
                if (dtgroupCourseKey != null && dtgroupCourseKey.Rows.Count > 0)
                    rowGroupCourse = dtgroupCourseKey.Rows[0];
                if (rowGroupCourse != null && rowGroupCourse["groupsubcoursekey"] != null && rowGroupCourse["groupsubcoursekey"].ToString() != "0")
                {
                    divGroupList.Visible = true;
                    try
                    {
                        rdoGroupList.SelectedValue = rowGroupCourse["groupsubcoursekey"].ToString();
                    }
                    catch(Exception err)
                    { }
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
                chkSubjectList_SelectedIndexChanged(null, null);
                LoadBatchList();

                BAL.Class.SmartInstitute.EnquiryClass o_GetEnq = new BAL.Class.SmartInstitute.EnquiryClass();
                int batchKey = o_GetEnq.GetBatchKeyForEnquiry(Convert.ToInt32(txtEnqKey.Text), ref Message);
                if (batchKey != 0)
                {
                    if (ddlBatchList.Items.Count > 0)
                        ddlBatchList.SelectedValue = batchKey.ToString();
                }
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lab_message.Text = "";
            if(bDeleteRights)
            {
                BAL.Class.SmartInstitute.EnquiryClass o_DeleteEnq = new BAL.Class.SmartInstitute.EnquiryClass();
                if (txtEnqKey.Text != "" && txtEnqKey.Text != "0")
                {
                    if (o_DeleteEnq.CheckForAdmissionDependancy(Convert.ToInt32(txtEnqKey.Text), ref Message))
                    {
                        lab_message.Text = Message;
                    }
                    else if (o_DeleteEnq.CheckForDependancy(Convert.ToInt32(txtEnqKey.Text), ref Message))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "showConfirm();", true);
                    }
                    else
                    {
                        btnDeleteForceFully_Click(sender,e);
                    }
                    
                }
                else
                {
                    lab_message.Text = "Please select record to delete";
                }
                
            }
            else
            {
                lab_message.Text = "Yoy are not having delete rights";
            }
        }

        protected void btnDeleteForceFully_Click(object sender, EventArgs e)
        {
            BAL.Class.SmartInstitute.EnquiryClass o_DeleteEnq = new BAL.Class.SmartInstitute.EnquiryClass();
            o_DeleteEnq.enquirykey = Convert.ToInt32(txtEnqKey.Text);
            int retval = o_DeleteEnq.save(ref Message, "DELETE");
            ClearControls();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btn_EnquirySave.Enabled = true;
            lab_message.Text = "Enquiry deleted successfully.";

        }
    }
}