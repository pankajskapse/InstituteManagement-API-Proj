using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Admin
{
    public partial class FacultyPage : System.Web.UI.Page
    {
        string Message = string.Empty;
        string Mode = "INSERT";
        static bool bEditRights = true;
        static bool bAddRights = true;
        static bool bDeleteRights = true;
        int iMenuKey = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSubjectList();
            FillGridViewData();
            if (!IsPostBack)
            {
                LoadCouseList();
                LoadSubCourseList();
                LoadGroupList();
                LoadSubjectData();

                ApplyLoginEmpRights();
                btnEdit.Enabled = false;
                btn_Delete.Enabled = false;
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

            try
            {
                if (rdoGroupList.SelectedValue != null && rdoGroupList.SelectedValue.Trim() != "")
                {
                    BAL.Class.SmartInstitute.SubjectClass o_GetSubject = new BAL.Class.SmartInstitute.SubjectClass();
                    DataTable dtChildSubc = o_GetSubject.GetSubjectListForGroup(iCourseKey, iSubCourseKey, iGroupKey, ref Message);

                    DDL_Subject.SelectedValue = null;
                    DDL_Subject.DataBind();

                    DDL_Subject.DataSource = dtChildSubc;
                    DDL_Subject.DataBind();

                }
                else if (rdoSubCourseList.SelectedValue != null && rdoSubCourseList.SelectedValue.Trim() != "")
                {
                    BAL.Class.SmartInstitute.SubjectClass o_GetSubject = new BAL.Class.SmartInstitute.SubjectClass();
                    DataTable dtChildSubc = o_GetSubject.GetSubjectListForSubCourse(iCourseKey, iSubCourseKey, ref Message);

                    DDL_Subject.SelectedValue = null;
                    DDL_Subject.DataBind();

                    DDL_Subject.DataSource = dtChildSubc;
                    DDL_Subject.DataBind();
                }
            }
            catch (Exception err)
            { }
            //if (!IsPostBack)
            //    chkSubjectList.SelectedIndex = 0;
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
                if (rdoSubCourseList.SelectedItem == null || rdoSubCourseList.SelectedValue == "")
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
        private void FillGridViewData()
        {
            BAL.Class.SmartInstitute.FacultyClass o_GetFaculty = new BAL.Class.SmartInstitute.FacultyClass();
            DataTable dtFaculty = o_GetFaculty.GetFacultyList(ref Message);
            grdFaculty.DataSource = dtFaculty;
            grdFaculty.DataBind();
        }
        private void LoadSubjectList()
        {
            try {
                int iCourseKey = 0;
                if (IsPostBack && DDL_Subject.SelectedValue != "")
                    iCourseKey = Convert.ToInt32(DDL_Subject.SelectedValue);

                DataTable dtSubject = new DataTable();
                BAL.Class.SmartInstitute.SubjectClass o_GetSubject = new BAL.Class.SmartInstitute.SubjectClass();
                dtSubject = o_GetSubject.GetSubjectList(ref Message);

                DDL_Subject.SelectedValue = null;
                DDL_Subject.DataBind();

                DDL_Subject.DataSource = dtSubject;
                DDL_Subject.DataBind();

                if (IsPostBack && iCourseKey > 0)
                    DDL_Subject.SelectedValue = iCourseKey.ToString();
            }
            catch(Exception err)
            { }
        }

        protected void DDL_Subject_Load(object sender, EventArgs e)
        {
            DAL.DAL dal = new DAL.DAL();
            SqlDataReader SubjectList = null;
            SubjectList = dal.GetReader("Select subjectKey,subjectCode from m_subject", ref Message);

            while (SubjectList.Read())
            {
                DDL_Subject.Items.Add(SubjectList["subjectCode"].ToString());
            }

        }

        private bool ValidateData()
        {
            Lab_message.Text = "";
            if (txt_FacultyCode.Text.Trim() == "")
                Lab_message.Text = "Faculty code is compulsory field.";
            else if (txt_FacultyDesc.Text.Trim() == "")
                Lab_message.Text = "Faculty Description is compulsory field.";

            if (Lab_message.Text.Trim() == "")
                return true;
            return false;
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            //string mode = "";
            if (txtFacultyKeyForEditMode.Text != "" && txtFacultyKeyForEditMode.Text != "0")
                Mode = "UPDATE";
            else Mode = "INSERT";

            if (ValidateData())
            {
                Message = string.Empty;
                BAL.Class.SmartInstitute.FacultyClass o_SaveFaculty = new BAL.Class.SmartInstitute.FacultyClass();

                if (DDL_Subject.SelectedItem != null)
                    o_SaveFaculty.SubjectKey = Convert.ToInt32(DDL_Subject.SelectedValue);
                else
                    o_SaveFaculty.SubjectKey = 0;

                if (o_SaveFaculty.CheckDuplicateFaculty(txt_FacultyCode.Text.Trim(), o_SaveFaculty.SubjectKey, Mode, ref Message))
                {
                    Lab_message.Text = "This Faculty code already exists. Please enter another";
                    txt_FacultyCode.Text = "";
                }
                else
                {
                    if (Mode == "UPDATE")
                        o_SaveFaculty.FacultyKey = Convert.ToInt32(txtFacultyKeyForEditMode.Text);
                    o_SaveFaculty.FacultyName = Convert.ToString(txt_FacultyCode.Text);
                    o_SaveFaculty.FacultyDesc = Convert.ToString(txt_FacultyDesc.Text);

                    // o_SaveBatch.createdBy = 99;
                    //o_SaveBatch.createdOn = Convert.ToString(System.DateTime.Now);
                    //o_SaveBatch.modifiedBy = 0;
                    //o_SaveBatch.modifiedOn = Convert.ToString(System.DateTime.Now);
                    //o_SaveBatch.Other1 = "";
                    //o_SaveBatch.Other2 = "";
                    //o_SaveBatch.Other3 = "";
                    //o_SaveBatch.Other4 = "";

                    int retval = o_SaveFaculty.save(ref Message, Mode);
                    //int retval = retval;
                    if (retval > 0)
                    {
                        if (Mode == "UPDATE")
                            Lab_message.Text = "Faculty updated successfully.";
                        else
                            Lab_message.Text = "Faculty saved successfully.";
                        lab_CreatedByText.Text = "#99";
                        lab_CreatedOnText.Text = Convert.ToString(System.DateTime.Now);
                        lab_ModifiedByText.Text = "#99";
                        lab_ModifiedOnText.Text = Convert.ToString(System.DateTime.Now);

                        Mode = "INSERT";
                        btnEdit.Enabled = false;
                        btn_Delete.Enabled = false;
                        btn_Save.Enabled = true;
                    }
                    else
                    {
                        Lab_message.Text = Convert.ToString(Message);
                    }
                }
            }
            FillGridViewData();
            foreach (GridViewRow row1 in grdFaculty.Rows)
            {
                row1.BackColor = System.Drawing.Color.LightGray;
                row1.ForeColor = System.Drawing.Color.Black;
            }
            ClearControls();

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearControls();
            btnEdit.Enabled = false;
            btn_Delete.Enabled = false;
            if (bAddRights)
                btn_Save.Enabled = true;
        }

        private void ClearControls()
        {
            txtFacultyKeyForEditMode.Text = "0";
            txt_FacultyCode.Text = "";
            txt_FacultyDesc.Text = "";
            lab_CreatedByText.Text = "";
            lab_ModifiedByText.Text = "";
            lab_CreatedOnText.Text = "";
            lab_CreatedOnText.Text = "";
            Mode = "INSERT";
        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            if (bDeleteRights)
            {
                BAL.Class.SmartInstitute.FacultyClass o_DeleteFaculty = new BAL.Class.SmartInstitute.FacultyClass();
                if (txtFacultyKeyForEditMode.Text != "" && txtFacultyKeyForEditMode.Text != "0")
                {
                    o_DeleteFaculty.FacultyKey = Convert.ToInt32(txtFacultyKeyForEditMode.Text);
                    int retval = o_DeleteFaculty.save(ref Message, "DELETE");
                }
                else
                {
                    Lab_message.Text = "Please select record to delete";
                }
                FillGridViewData();
                ClearControls();
                btnEdit.Enabled = false;
                btn_Delete.Enabled = false;
            }
            else
            {
                Lab_message.Text = "You are not allowed to delete record. Please contact administrator.";
            }

        }

        protected void grdFaculty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFaculty.PageIndex = e.NewPageIndex;
            grdFaculty.DataBind();
        }

        protected void grdFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bEditRights)
            {
                if (grdFaculty.SelectedRow != null)
                {
                    Mode = "UPDATE";
                    txtFacultyKeyForEditMode.Text = grdFaculty.SelectedRow.Cells[1].Text;
                    txt_FacultyCode.Text = Server.HtmlDecode(grdFaculty.SelectedRow.Cells[2].Text).Trim();
                    txt_FacultyDesc.Text = Server.HtmlDecode(grdFaculty.SelectedRow.Cells[3].Text).Trim();
                    if (Server.HtmlDecode(grdFaculty.SelectedRow.Cells[5].Text).Trim() != "")
                        lab_CreatedOnText.Text = Convert.ToDateTime(Server.HtmlDecode(grdFaculty.SelectedRow.Cells[5].Text).Trim()).ToString("yyyy-MM-dd");
                    else
                        lab_CreatedOnText.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    if (Server.HtmlDecode(grdFaculty.SelectedRow.Cells[6].Text).Trim() != "")
                        lab_ModifiedOnText.Text = Convert.ToDateTime(Server.HtmlDecode(grdFaculty.SelectedRow.Cells[6].Text).Trim()).ToString("yyyy-MM-dd");
                    else lab_ModifiedOnText.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    if (Server.HtmlDecode(grdFaculty.SelectedRow.Cells[7].Text).Trim() != "")
                        DDL_Subject.SelectedValue = Server.HtmlDecode(grdFaculty.SelectedRow.Cells[7].Text);

                    BAL.Class.SmartInstitute.SubjectClass subCls = new BAL.Class.SmartInstitute.SubjectClass();

                    if (DDL_Subject.SelectedItem != null && DDL_Subject.SelectedValue.Trim() != "")
                    {
                        DataTable dtDetails = subCls.GetSubjectListForSubCourseFacultyWise(Convert.ToInt32(DDL_Subject.SelectedValue), ref Message);
                        DataRow row = null;
                        if (dtDetails != null && dtDetails.Rows.Count > 0)
                            row = dtDetails.Rows[0];
                        //iCourseKey = row["CourseKey"].ToString();
                        rdoListCourse.SelectedValue = row["CourseKey"].ToString();
                        LoadSubCourseList();
                        rdoSubCourseList.SelectedValue = row["SubCourseKey"].ToString();

                        LoadGroupList();
                        if (divGroupList.Visible = true)
                        {
                            int iGroupKey = subCls.GetGroupKeyfromSubject(Convert.ToInt32(DDL_Subject.SelectedValue), ref Message);
                            if (iGroupKey > 0)
                                rdoGroupList.SelectedValue = iGroupKey.ToString();
                        }
                    }
                    btnEdit.Enabled = true;
                    btn_Delete.Enabled = true;
                    btn_Save.Enabled = false;
                }
            }
            else
            {
                Lab_message.Text = "You are not having edit rights";
            }
        }

        private void LoadGroupListBySubjectKey()
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
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            btn_Save_Click(sender, e);
        }

        protected void rdoListCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdoSubCourseList.DataSource = null;
            rdoGroupList.DataSource = null;
            DDL_Subject.DataSource = null;
            rdoSubCourseList.DataBind();
            rdoGroupList.DataBind();
            DDL_Subject.DataBind();

            BAL.Class.SmartInstitute.SubCourseClass o_GetSubCourse = new BAL.Class.SmartInstitute.SubCourseClass();
            DataTable dtChild = o_GetSubCourse.GetSubCourseListForCourse(Convert.ToInt32(rdoListCourse.SelectedValue), ref Message);
            rdoSubCourseList.DataSource = dtChild;
            rdoSubCourseList.DataBind();
            if (dtChild != null && dtChild.Rows.Count > 0)
                rdoSubCourseList.SelectedIndex = 0;
            LoadGroupList();
            LoadSubjectData();
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
            DDL_Subject.SelectedValue = null;
            DDL_Subject.DataSource = null;
            DDL_Subject.DataBind();
            DDL_Subject.DataSource = dtChildSubc;
            DDL_Subject.DataBind();

            LoadGroupList();
            LoadSubjectData();
        }

        protected void rdoGroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubjectData();
        }
    }
}