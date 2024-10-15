using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Admin
{
    public partial class FollowUpEnqPage : System.Web.UI.Page
    {
        string Message = string.Empty;
        static bool bEditRights = true;
        static bool bAddRights = true;
        int iMenuKey = 11;
        protected void Page_Load(object sender, EventArgs e)
        {
            Message = string.Empty;            
            
            if (!IsPostBack)
            {
                LoadEnquiryTypeList();
                LoadBatchList();
                txtFollowUpdate.Text = DateTime.Now.ToString("yyyy-MM-dd"); //DateTime.Now.ToShortDateString();
                txtEnquiryOwner.Text = Session["LoginEmpName"].ToString();
                txtEnqID.Attributes.Add("readonly", "readonly");
                txtMobileNo.Attributes.Add("readonly", "readonly");
                txtLastName.Attributes.Add("readonly", "readonly");
                txtFirstName.Attributes.Add("readonly", "readonly");
                txtEnquiryDate.Attributes.Add("readonly", "readonly");
                txtemailID.Attributes.Add("readonly", "readonly");
                ddlBatchList.Attributes.Add("readonly", "readonly");

                txtEnquiryType.Attributes.Add("readonly", "readonly");
                txtBatch.Attributes.Add("readonly", "readonly");
                // ddlEnquiryType.Attributes.Add("disabled", "disabled");

                // ddlEnquiryType.Attributes.Add("readonly", "readonly");
                txtCollegeName.Attributes.Add("readonly", "readonly");
                txtaltmobileno.Attributes.Add("readonly", "readonly");
                txtRefByMobileNo.Attributes.Add("readonly", "readonly");
                txtRefByName.Attributes.Add("readonly", "readonly");
                txtEnquiryRemark.Attributes.Add("readonly", "readonly");
                RadioButtonF.Attributes.Add("readonly", "readonly");
                RadioButtonM.Attributes.Add("readonly", "readonly");

                if (txtEnqID.Text.Trim() == "")
                    txtEnqID.Attributes.Add("placeholder", "Select From Enquiry List");

                ApplyLoginEmpRights();
               
                //btnEdit.Enabled = false;
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
                    if (row != null)
                    {
                        if (Convert.ToBoolean(row["erview"]))
                        {

                            if (Convert.ToBoolean(row["eradd"]))
                            {
                                btnAdd.Enabled = true;
                                btn_FollowupSave.Enabled = true;
                                bAddRights = true;
                            }
                            else
                            {
                                btnAdd.Enabled = false;
                                btn_FollowupSave.Enabled = true;
                                bAddRights = false;
                            }

                            if (Convert.ToBoolean(row["eredit"]))
                            {
                                bEditRights = true;
                                //btnEdit.Enabled = true;
                            }
                            else
                            {
                                bEditRights = false;
                                //btnEdit.Enabled = false;
                            }
                          
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
            ddlBatchList.DataSource = o_Batch.GetBatchList(ref Message);
            ddlBatchList.DataBind();
        }
        private void ClearControls()
        {
            txtEnqID.Text = "";
            txtEnqKey.Text = "0";
            txtaltmobileno.Text = "";
            txtCollegeName.Text = "";
            txtemailID.Text = "";
            txtEnquiryOwner.Text = Session["LoginEmpName"].ToString();
            txtFirstName.Text = "";
            txtFollowUpdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtLastName.Text = "";
            txtMobileNo.Text = "";
            txtRefByMobileNo.Text = "";
            txtRefByName.Text = "";
            txt_Remarks.Text = "";

            txtEnquiryType.Text = "";
            txtBatch.Text = "";
            txtEnquiryRemark.Text = "";
            lab_message.Text = "";
            
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearControls();
            btn_FollowupSave.Enabled = true;
            grdFollowUp.DataSource = null;
            grdFollowUp.DataBind();

            //btnEdit.Enabled = false;
            if (bAddRights)
                btn_FollowupSave.Enabled = true;
        }

        private bool ValidateData()
        {
            lab_message.Text = "";
            if (txt_Remarks.Text.Trim() == "")
                lab_message.Text = "Remark is required field";

            if (lab_message.Text.Trim() == "")
                return true;
            else return false;
        }
        protected void btn_FollowupSave_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                string mode = "INSERT";

                Message = string.Empty;
                BAL.Class.SmartInstitute.enqfollowupClass o_SaveEnqFollowup = new BAL.Class.SmartInstitute.enqfollowupClass();

                // int EnquiryKey = 99; //Temp
                if (txtEnqKey.Text != "" && txtEnqKey.Text != "0")
                {
                    o_SaveEnqFollowup.enquirykey = Convert.ToInt32(txtEnqKey.Text);
                    o_SaveEnqFollowup.followupowner = Convert.ToInt32(Session["LoginEmpKey"]);
                    o_SaveEnqFollowup.followupdate = Convert.ToDateTime(txtFollowUpdate.Text);
                    o_SaveEnqFollowup.followupremark = txt_Remarks.Text;
                    o_SaveEnqFollowup.createdBy = Convert.ToInt32(Session["LoginEmpKey"]);
                    o_SaveEnqFollowup.modifiedBy = Convert.ToInt32(Session["LoginEmpKey"]);

                    Message = string.Empty;

                    int iEnqKey = o_SaveEnqFollowup.save(ref Message, mode);
                    if (iEnqKey > 0)
                    {
                        //SaveCoursesData(iEnqKey, mode);
                        lab_message.Text = "Follow up information saved successfully.";
                        // txtEnqID.Text = o_SaveEnquiry.GetEnquiryIDGenerated(iEnqKey, ref Message);
                        btn_FollowupSave.Enabled = false;
                        //lab_CreatedByText.Text = "#99";
                        //lab_CreatedOnText.Text = Convert.ToString(System.DateTime.Now);
                        //lab_ModifiedByText.Text = "#99";
                        //lab_ModifiedOnText.Text = Convert.ToString(System.DateTime.Now);
                        FillFollowupGrid();
                        btn_FollowupSave.Enabled = false;
                    }
                    else
                    {
                        lab_message.Text = Convert.ToString(Message);
                    }
                }
                else
                {
                    lab_message.Text = "select enquiry to add follow up";
                }
            }
        }

        protected void btnEquSearch_Click(object sender, EventArgs e)
        {
            //if (txtEnqID.Text == "")
            //{
            //    lab_message.Text = "Please enter Enquiry ID";
            //    return;
            //}
            //else
            //{
            //    string enqID = txtEnqID.Text;
            //    BAL.Class.SmartInstitute.enqfollowupClass getEnqInformation = new BAL.Class.SmartInstitute.enqfollowupClass();
            //    DataTable dtEnqInfo = getEnqInformation.GeEnquInfo(ref Message, enqID);
            //    txtMobileNo.Text = Convert.ToString(dtEnqInfo.Columns[MobileNo]);
            //}
        }
        private void FillFollowupGrid()
        {
            int iEnqKey = 0;
            if (txtEnqKey.Text.Trim() != "" && txtEnqKey.Text.Trim() != "0")
                iEnqKey = Convert.ToInt32(txtEnqKey.Text);
            //ddlBatchList.SelectedValue
            BAL.Class.SmartInstitute.enqfollowupClass o_GetFollowUp = new BAL.Class.SmartInstitute.enqfollowupClass();
            DataTable dtBatch = o_GetFollowUp.GetAllFollowUpByEnqKey(ref Message, iEnqKey);
            grdFollowUp.DataSource = dtBatch;
            grdFollowUp.DataBind();
            //ddlEnquiryType.SelectedItem.Text
        }
        protected void txtEnqID_TextChanged(object sender, EventArgs e)
        {
            if (txtEnqKey.Text.Trim() != "" && txtEnqKey.Text.Trim() != "0")
                FillFollowupGrid();
            ////else
            ////{

            ////}
        }

        protected void grdFollowUp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFollowUp.PageIndex = e.NewPageIndex;
            grdFollowUp.DataBind();
        }

        protected void grdFollowUp_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnShowOldFollowUp_Click(object sender, EventArgs e)
        {
            if (txtEnqKey.Text.Trim() != "" && txtEnqKey.Text.Trim() != "0")
                FillFollowupGrid();
        }
    }
}