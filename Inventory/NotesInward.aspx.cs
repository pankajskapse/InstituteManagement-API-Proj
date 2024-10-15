using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Inventory
{
    public partial class NotesInward : System.Web.UI.Page
    {
        string Message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ApplyLoginEmpRights();
                LoadCouseList();
                LoadSubCourseList();
                LoadGroupList();
                LoadBatchList();
            }
        }
        private void LoadBatchList()
        {
            try
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
            catch (Exception err)
            {
                lab_message.Text = err.Message;
            }
        }
        private void LoadSubCourseList()
        {
            try
            {
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
            catch (Exception err)
            {
                lab_message.Text = err.Message;
            }
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (rdoListCourse.SelectedIndex < 0 || rdoListCourse.SelectedValue == "")
            {
                lab_message.Text = "Please select Course";
                return;
            }
            else if (rdoSubCourseList.SelectedIndex <0 || rdoSubCourseList.SelectedValue == "")
            {
                lab_message.Text = "Please select Sub-Course";
                return;
            }
            else if (rdoGroupList.SelectedIndex <0 || rdoGroupList.SelectedValue == "")
            {
                lab_message.Text = "Please select group";
                return;
            }
            
            else if (ddlBatchList.SelectedIndex < 0 || ddlBatchList.SelectedValue == "")
            {
                lab_message.Text = "Please select Batch";
                return;
            }
            //else if (TextNotesInQty.Text == "")
            //{
            //    lab_message.Text = "Please select Quentity";
            //    return;
            //}
            else
            {
                string mode = "INSERT";
                // Save Course Master DATA Insert into m_course table//         

                Message = string.Empty;
                BAL.Class.Inventory.NotesInwardClass o_SaveNotesInward = new BAL.Class.Inventory.NotesInwardClass();

                int DefaultKey = 99; //Temp
                o_SaveNotesInward.notesinwardKey = DefaultKey;
                o_SaveNotesInward.batchKey = DefaultKey;
                o_SaveNotesInward.subjectKey = DefaultKey;
                o_SaveNotesInward.groupsubcourseKey = DefaultKey;
                o_SaveNotesInward.SubCourseKey = DefaultKey;
                o_SaveNotesInward.CourseKey = DefaultKey;
                //o_SaveNotesInward.notesinwardQty = Convert.ToInt32(TextNotesInQty.Text);
                //o_SaveNotesInward.remarks = Convert.ToString(TextRemarks.Text);
                // o_SaveNotesInward.createdBy = 99;
                //o_SaveNotesInward.createdOn = System.DateTime.Now;
                //o_SaveNotesInward.modifiedBy = 0;
                //o_SaveNotesInward.modifiedOn = Convert.ToString(System.DateTime.Now);


                int retval = o_SaveNotesInward.save(ref Message, mode);
                if (retval > 0)
                {
                    lab_message.Text = "Notes in Quentity saved successfully.";
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

        protected void btn_Back_Click(object sender, EventArgs e)
        {

        }

        protected void CourseDDL_Load(object sender, EventArgs e)
        {

        }

        protected void SubCourseDDL_Load(object sender, EventArgs e)
        {

        }

        protected void GroupDDL_Load(object sender, EventArgs e)
        {

        }

        protected void rdoSubCourseList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void rdoGroupList_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                        try
                        {
                            rdoGroupList.SelectedIndex = iSelectedGroupInx;
                        }
                        catch (Exception err)
                        {
                            lab_message.Text = err.Message;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                lab_message.Text = err.Message;
            }
        }
        protected void rdoListCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
                LoadBatchList();
            }
            catch (Exception err)
            {
                lab_message.Text = err.Message;
            }
        }

        protected void ddlBatchList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}