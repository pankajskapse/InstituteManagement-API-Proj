using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Admin
{
    public partial class BatchNew : System.Web.UI.Page
    {
        string Message = string.Empty;
        string Mode = "INSERT";
        static bool bEditRights = true;
        static bool bAddRights = true;
        static bool bDeleteRights = true;

        int iMenuKey = 3;
        protected void Page_Load(object sender, EventArgs e)
        {
            FillGridViewData();
            if (!IsPostBack)
            {
                txtBatchStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtBatchEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ApplyLoginEmpRights();
                LoadCouseList();
                LoadSubCourseList();
                LoadGroupList();

                btnEdit.Enabled = false;
                btn_Delete.Enabled = false;
            }
        }
        private void LoadCouseList()
        {
            BAL.Class.SmartInstitute.CourseClass o_GetCourse = new BAL.Class.SmartInstitute.CourseClass();
            DataTable dtCourse = new DataTable();
            dtCourse = o_GetCourse.GetCourseList(ref Message);

            //int iSelectedCourseInx = 0;
            //if (rdoListCourse.SelectedIndex != null && rdoListCourse.SelectedItem != null)
            //    iSelectedCourseInx = rdoListCourse.SelectedIndex;
            rdoListCourse.DataSource = dtCourse;
            rdoListCourse.DataBind();

            //////if (!IsPostBack)
            rdoListCourse.SelectedIndex = 0;

        }
        private void FillGridViewData()
        {
            BAL.Class.SmartInstitute.BatchClass o_GetBatch = new BAL.Class.SmartInstitute.BatchClass();
            DataTable dtBatch = o_GetBatch.GetBatchList(ref Message);
            grdBatch.DataSource = dtBatch;
            grdBatch.DataBind();
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
                                btn_Save.Enabled = true;
                                bAddRights = true;
                            }
                            else
                            {
                                btnAdd.Enabled = false;
                                btn_Save.Enabled = true;
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
                                btn_Delete.Visible = true;
                            }
                            else
                            {
                                bDeleteRights = false;
                                btn_Delete.Visible = false;
                            }

                        }
                    }
                }
            }
        }
        private bool ValidateData()
        {
            Lab_message.Text = "";
            if (txt_BatchCode.Text.Trim() == "")
                Lab_message.Text = "Batch code is compulsory field.";
            else if (txt_BatchDesc.Text.Trim() == "")
                Lab_message.Text = "Batch Description is compulsory field.";

            if (Lab_message.Text.Trim() == "")
                return true;
            return false;
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            BAL.Class.SmartInstitute.BatchClass o_SaveBatch = new BAL.Class.SmartInstitute.BatchClass();
            
            if (txtBatchKeyForEditMode.Text != "" && txtBatchKeyForEditMode.Text != "0")
                Mode = "UPDATE";
            else Mode = "INSERT";

            if (ValidateData())
            {
                if (o_SaveBatch.CheckDuplicateBatch(txt_BatchCode.Text.Trim(),Mode, ref Message))
                {
                    Lab_message.Text = "This batch code already exists. Please enter another";
                    txt_BatchCode.Text = "";
                }
                else
                {
                    Message = string.Empty;


                    if (Mode == "UPDATE")
                        o_SaveBatch.batchKey = Convert.ToInt32(txtBatchKeyForEditMode.Text);
                    o_SaveBatch.batchCode = Convert.ToString(txt_BatchCode.Text);
                    o_SaveBatch.batchDesc = Convert.ToString(txt_BatchDesc.Text);

                    o_SaveBatch.BatchStartDate = Convert.ToDateTime(txtBatchStartDate.Text);
                    o_SaveBatch.BatchEndDate = Convert.ToDateTime(txtBatchEndDate.Text);
                    o_SaveBatch.NoOfInstallment = Convert.ToInt32(ddlNoOfInstallment.SelectedValue);
                    if (chkClosed.Checked != null)
                        o_SaveBatch.Closed = chkClosed.Checked;
                    else
                        o_SaveBatch.Closed = false;

                    int iCourseKey = 0;
                    int isubCourseKey = 0;
                    int isubGroupKey = 0;
                    
                    if (rdoListCourse.SelectedItem != null)
                        iCourseKey = Convert.ToInt32(rdoListCourse.SelectedValue);
                    if (rdoSubCourseList.SelectedItem != null)
                        isubCourseKey = Convert.ToInt32(rdoSubCourseList.SelectedValue);
                    if (rdoGroupList.SelectedItem != null && divGroupList.Visible)
                        isubGroupKey = Convert.ToInt32(rdoGroupList.SelectedValue);

                    o_SaveBatch.courseKey = iCourseKey;
                    o_SaveBatch.subCourseKey = isubCourseKey;
                    o_SaveBatch.groupKey = isubGroupKey;

                    o_SaveBatch.createdBy = Convert.ToInt32(Session["LoginEmpKey"]);
                    o_SaveBatch.createdOn = Convert.ToString(System.DateTime.Now);
                    o_SaveBatch.modifiedBy = Convert.ToInt32(Session["LoginEmpKey"]);
                    //o_SaveBatch.modifiedOn = Convert.ToString(System.DateTime.Now);
                    //o_SaveBatch.Other1 = "";
                    //o_SaveBatch.Other2 = "";
                    //o_SaveBatch.Other3 = "";
                    //o_SaveBatch.Other4 = "";

                    int retval = o_SaveBatch.save(ref Message, Mode);
                    //int retval = retval;
                    if (retval > 0)
                    {
                        if (Mode == "UPDATE")
                            Lab_message.Text = "Batch updated successfully.";
                        else
                            Lab_message.Text = "Batch saved successfully.";

                        Mode = "INSERT";
                        //lab_CreatedByText.Text = "#99";
                        //lab_CreatedOnText.Text = Convert.ToString(System.DateTime.Now);
                        //lab_ModifiedByText.Text = "#99";
                        //lab_ModifiedOnText.Text = Convert.ToString(System.DateTime.Now);
                        ClearControls();
                        btnEdit.Enabled = false;
                    }
                    else
                    {
                        Lab_message.Text = Convert.ToString(Message);
                    }
                }
            }
            FillGridViewData();
            foreach (GridViewRow row1 in grdBatch.Rows)
            {
                row1.BackColor = System.Drawing.Color.LightGray;
                row1.ForeColor = System.Drawing.Color.Black;
            }

            
        }

        protected void btn_Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/WellcomePage.aspx");
        }

        protected void grdBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bEditRights)
            {
                Lab_message.Text = "";
                Mode = "UPDATE";
                grdBatch.SelectedRow.Focus();
                txtBatchKeyForEditMode.Text = grdBatch.SelectedRow.Cells[1].Text;
                txt_BatchCode.Text = Server.HtmlDecode(grdBatch.SelectedRow.Cells[2].Text);
                txt_BatchDesc.Text = Server.HtmlDecode(grdBatch.SelectedRow.Cells[3].Text);
                lab_CreatedByText.Text = Server.HtmlDecode(grdBatch.SelectedRow.Cells[5].Text);
                lab_CreatedOnText.Text = Server.HtmlDecode(grdBatch.SelectedRow.Cells[4].Text);
                lab_ModifiedOnText.Text = Server.HtmlDecode(grdBatch.SelectedRow.Cells[6].Text);
                lab_ModifiedByText.Text = Server.HtmlDecode(grdBatch.SelectedRow.Cells[7].Text);
                if (Server.HtmlDecode(grdBatch.SelectedRow.Cells[8].Text).Trim() == "")
                    ddlNoOfInstallment.SelectedValue = "1";
                else
                    ddlNoOfInstallment.SelectedValue = Server.HtmlDecode(grdBatch.SelectedRow.Cells[8].Text);

                if(Server.HtmlDecode(grdBatch.SelectedRow.Cells[9].Text).Trim()=="")
                    txtBatchStartDate.Text= DateTime.Now.ToString("yyyy-MM-dd");
                else
                txtBatchStartDate.Text = Convert.ToDateTime(grdBatch.SelectedRow.Cells[9].Text).ToString("yyyy-MM-dd");

                if (Server.HtmlDecode(grdBatch.SelectedRow.Cells[10].Text).Trim() == "")
                    txtBatchEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                else
                    txtBatchEndDate.Text = Convert.ToDateTime(grdBatch.SelectedRow.Cells[10].Text).ToString("yyyy-MM-dd");

                if (Server.HtmlDecode(grdBatch.SelectedRow.Cells[11].Text).Trim() != "" && Server.HtmlDecode(grdBatch.SelectedRow.Cells[11].Text) != "0")
                    rdoListCourse.SelectedValue = Server.HtmlDecode(grdBatch.SelectedRow.Cells[11].Text);
                else
                {
                    if (rdoListCourse.Items.Count > 0)
                        rdoListCourse.SelectedIndex = 0;
                }

                LoadSubCourseList();
                if (Server.HtmlDecode(grdBatch.SelectedRow.Cells[13].Text).Trim() != "" && Server.HtmlDecode(grdBatch.SelectedRow.Cells[13].Text) != "0")
                    rdoSubCourseList.SelectedValue = Server.HtmlDecode(grdBatch.SelectedRow.Cells[13].Text);
                else
                {
                    if (rdoSubCourseList.Items.Count > 0)
                        rdoSubCourseList.SelectedIndex = 0;
                }

                LoadGroupList();
                if (Server.HtmlDecode(grdBatch.SelectedRow.Cells[15].Text).Trim() != "" && Server.HtmlDecode(grdBatch.SelectedRow.Cells[15].Text) != "0")
                    rdoGroupList.SelectedValue = Server.HtmlDecode(grdBatch.SelectedRow.Cells[15].Text);
                else
                {
                    if (rdoGroupList.Items.Count > 0)
                        rdoGroupList.SelectedIndex = 0;
                }

                btnEdit.Enabled = true;
                btn_Delete.Enabled = true;
                btn_Save.Enabled = false;
            }
            else
            {
                Lab_message.Text = "You are not having edit rights";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Lab_message.Text = "";
            btnEdit.Enabled = false;
            btn_Delete.Enabled = false;
            if (bAddRights)
                btn_Save.Enabled = true;
            ClearControls();
        }
        private void ClearControls()
        {
            txtBatchKeyForEditMode.Text = "0";
            txt_BatchCode.Text = "";
            txt_BatchDesc.Text = "";
            lab_CreatedByText.Text = "";
            lab_ModifiedByText.Text = "";
            lab_CreatedOnText.Text = "";
            lab_CreatedOnText.Text = "";
            Mode = "INSERT";
            chkClosed.Checked = false;
        }
        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            if (bDeleteRights)
            {
                Lab_message.Text = "";
                BAL.Class.SmartInstitute.BatchClass o_DeleteBatch = new BAL.Class.SmartInstitute.BatchClass();
                if (txtBatchKeyForEditMode.Text != "" && txtBatchKeyForEditMode.Text != "0")
                {
                    if (o_DeleteBatch.CheckForDependancy(Convert.ToInt32(txtBatchKeyForEditMode.Text), ref Message))
                        Lab_message.Text = Message;
                    else
                    {
                        o_DeleteBatch.batchKey = Convert.ToInt32(txtBatchKeyForEditMode.Text);
                        int retval = o_DeleteBatch.save(ref Message, "DELETE");
                        Lab_message.Text = "Record deleted successfully";

                    }
                }
                else
                {
                    Lab_message.Text = "Please select record to delete";
                }
                FillGridViewData();
                ClearControls();
                btnEdit.Enabled = false;
                btn_Delete.Enabled = false;
                btn_Save.Enabled = true;
            }
            else
            {
                Lab_message.Text = "You are not allowed to delete records.Please contact administrator.";
            }
        }

        protected void grdBatch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdBatch.PageIndex = e.NewPageIndex;
            grdBatch.DataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            btn_Save_Click(sender, e);
        }

        protected void rdoSubCourseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGroupList();
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
                
            }
        }

        private void LoadSubCourseList()
        {
            int iSubCourseKey = 0;
            string SubCourse = "";
            //if (rdoSubCourseList.SelectedItem != null)
            //    SubCourse = rdoSubCourseList.SelectedItem.Text;
            //if (IsPostBack && rdoSubCourseList.SelectedValue != null && rdoSubCourseList.SelectedValue != "")
            //    iSubCourseKey = Convert.ToInt32(rdoSubCourseList.SelectedValue);

            BAL.Class.SmartInstitute.SubCourseClass o_GetSubCourse = new BAL.Class.SmartInstitute.SubCourseClass();

            DataTable dtChild = null;


            if (rdoListCourse.SelectedItem != null && rdoListCourse.SelectedItem.Value != null)
            {
                dtChild = o_GetSubCourse.GetSubCourseListForCourse(Convert.ToInt32(rdoListCourse.SelectedItem.Value), ref Message);
                rdoSubCourseList.DataSource = dtChild;
                rdoSubCourseList.DataBind();

                rdoSubCourseList.SelectedIndex = 0;
                //ListItem subCourse = new ListItem();
                //subCourse.Value = iSubCourseKey.ToString();
                //subCourse.Text = SubCourse;
                //if (IsPostBack && iSubCourseKey > 0)
                //{
                //    if (rdoSubCourseList.Items.Contains(subCourse))
                //        rdoSubCourseList.SelectedValue = iSubCourseKey.ToString();
                //}
               
            }
        }
        protected void rdoListCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdoSubCourseList.DataSource = null;
            rdoGroupList.DataSource = null;
            rdoSubCourseList.DataBind();
            rdoGroupList.DataBind();

            BAL.Class.SmartInstitute.SubCourseClass o_GetSubCourse = new BAL.Class.SmartInstitute.SubCourseClass();
            DataTable dtChild = o_GetSubCourse.GetSubCourseListForCourse(Convert.ToInt32(rdoListCourse.SelectedValue), ref Message);
            rdoSubCourseList.DataSource = dtChild;
            rdoSubCourseList.DataBind();
            if (dtChild != null && dtChild.Rows.Count > 0)
                rdoSubCourseList.SelectedIndex = 0;
            LoadGroupList();
        }
    }
}