using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Admin
{
    public partial class courseSubjectPage : System.Web.UI.Page
    {
        string Message = string.Empty;
        string Mode = "INSERT";
        static bool bEditRights = true;
        static bool bAddRights = true;
        static bool bDeleteRights = true;
        static DataTable dtSubject;

        int iMenuKey = 6;
        protected void Page_Load(object sender, EventArgs e)
        {
            Lab_message.Text = "";
            LoadCourseList();
            
            if (CourseDDL.SelectedValue != null && CourseDDL.SelectedValue != "")
            {
                int iCourseKey = Convert.ToInt32(CourseDDL.SelectedValue);
                if (CourseDDL.SelectedValue != null && CourseDDL.SelectedValue != "")
                {
                    //int iSubCourseKey = Convert.ToInt32(SubCourseDDL.SelectedValue);
                    LoadSubCourseData(iCourseKey);
                    FillGridData();

                }
            }
            if (!IsPostBack)
            {
                FillGridData();
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
        private void LoadCourseList()
        {
            int iCourseKey = 0;
            if (IsPostBack && CourseDDL.SelectedValue != null && CourseDDL.SelectedValue != "")
                iCourseKey = Convert.ToInt32(CourseDDL.SelectedValue);
            DataTable dtCourse = new DataTable();
            BAL.Class.SmartInstitute.CourseClass o_GetCourse = new BAL.Class.SmartInstitute.CourseClass();
            dtCourse = o_GetCourse.GetCourseList(ref Message);
            CourseDDL.DataSource = dtCourse;
            CourseDDL.DataBind();

            if (IsPostBack && iCourseKey > 0)
            {
                if (CourseDDL.Items.Count > 0)
                    CourseDDL.SelectedValue = iCourseKey.ToString();

            }
        }
        private void LoadSubCourseData(int iCourseKey)
        {
            int iSubCourseKey = 0;
            string SubCourse = "";
            if (SubCourseDDL.SelectedItem != null)
                SubCourse = SubCourseDDL.SelectedItem.Text;
            if (IsPostBack && SubCourseDDL.SelectedValue != null && SubCourseDDL.SelectedValue != "")
                iSubCourseKey = Convert.ToInt32(SubCourseDDL.SelectedValue);

            DataTable dtSubCourse = new DataTable();
            BAL.Class.SmartInstitute.SubCourseClass o_GetSubCourse = new BAL.Class.SmartInstitute.SubCourseClass();
            dtSubCourse = o_GetSubCourse.GetSubCourseListForCourse(iCourseKey, ref Message);

            SubCourseDDL.DataSource = null;
            SubCourseDDL.DataBind();
            SubCourseDDL.DataSource = dtSubCourse;
            SubCourseDDL.DataBind();

            ListItem subCourse = new ListItem();
            subCourse.Value = iSubCourseKey.ToString();
            subCourse.Text = SubCourse;
            if (IsPostBack && iSubCourseKey > 0)
            {
                if (SubCourseDDL.Items.Contains(subCourse))
                    SubCourseDDL.SelectedValue = iSubCourseKey.ToString();
            }
            //if (IsPostBack && iSubCourseKey > 0)
            //    SubCourseDDL.SelectedValue = iSubCourseKey.ToString();
            if (SubCourseDDL.SelectedValue != null && SubCourseDDL.SelectedValue != "")
            {
                iSubCourseKey = Convert.ToInt32(SubCourseDDL.SelectedValue);
                LoadGroupCourseList(iSubCourseKey);
            }

        }
        private void LoadGroupCourseList(int iSubCourseKey)
        {
            DataTable dtCourse = new DataTable();
            BAL.Class.SmartInstitute.GroupSubCourseClass o_GetGroup = new BAL.Class.SmartInstitute.GroupSubCourseClass();
            dtCourse = o_GetGroup.GetGroupsListForSubCode(iSubCourseKey, ref Message);

            string groupName = "";
            int groupKey = 0;
            if (GroupDDL.SelectedItem != null)
                groupName = GroupDDL.SelectedItem.Text;
            if (IsPostBack && GroupDDL.SelectedValue != null && GroupDDL.SelectedValue != "")
                groupKey = Convert.ToInt32(GroupDDL.SelectedValue);

            GroupDDL.DataSource = null;
            GroupDDL.DataBind();
            GroupDDL.DataSource = dtCourse;
            GroupDDL.DataBind();

            ListItem groupSubCourse = new ListItem();
            groupSubCourse.Value = groupKey.ToString();
            groupSubCourse.Text = groupName;
            if (IsPostBack && groupKey > 0)
            {
                if (GroupDDL.Items.Contains(groupSubCourse))
                    GroupDDL.SelectedValue = groupKey.ToString();
            }
        }
        private bool ValidateData()
        {
            Lab_message.Text = "";
            if (CourseDDL.SelectedItem == null || CourseDDL.SelectedValue == "")
                Lab_message.Text = "Course is compulsary field.";
            else if (SubCourseDDL.SelectedItem == null || SubCourseDDL.SelectedValue == "")
                Lab_message.Text = "SubCourse is compulsary field.";
            else if (txt_SubjectCourse.Text.Trim() == "")
                Lab_message.Text = "Subject Code is compulsary field.";
            else if (txt_SubjectCourseDesc.Text.Trim() == "")
                Lab_message.Text = "Subject Desc is compulsary field.";

            if (Lab_message.Text.Trim() == "")
                return true;
            else return false;
        }
        private void ClearControls()
        {
            CourseDDL.SelectedIndex = 0;
            if (SubCourseDDL.Items.Count > 0)
                SubCourseDDL.SelectedIndex = 0;
            if (GroupDDL.Items.Count > 0)
                GroupDDL.SelectedIndex = 0;

            txtSubjectKeyForEditMode.Text = "0";
            txt_SubjectCourse.Text = "";
            txt_SubjectCourseDesc.Text = "";
            lab_CreatedByText.Text = "";
            lab_ModifiedByText.Text = "";
            lab_CreatedOnText.Text = "";
            lab_CreatedOnText.Text = "";
            txtSubjectFee.Value = "0";
            Mode = "INSERT";
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            Lab_message.Text = "";
            if (ValidateData())
            {
                if (txtSubjectKeyForEditMode.Text != "" && txtSubjectKeyForEditMode.Text != "0")
                    Mode = "UPDATE";
                else Mode = "INSERT";

                Message = string.Empty;
                BAL.Class.SmartInstitute.SubjectClass o_SaveSubject = new BAL.Class.SmartInstitute.SubjectClass();

                if (SubCourseDDL.SelectedItem != null)
                    o_SaveSubject.SubCourseKey = Convert.ToInt32(SubCourseDDL.SelectedValue);
                else
                    o_SaveSubject.SubCourseKey = 0;


                if (GroupDDL.SelectedItem != null && GroupDDL.SelectedValue != "")
                    o_SaveSubject.groupsubcourseKey = Convert.ToInt32(GroupDDL.SelectedValue);
                else
                    o_SaveSubject.groupsubcourseKey = 0;

                if (o_SaveSubject.CheckDuplicateSubject(txt_SubjectCourse.Text.Trim(), o_SaveSubject.SubCourseKey, o_SaveSubject.groupsubcourseKey, Mode, ref Message))
                {
                    Lab_message.Text = "This Subject already exists in this Sub-Course/Group. Please enter another";
                    txt_SubjectCourse.Text = "";
                }
                else
                {
                    if (CourseDDL.SelectedItem != null)
                        o_SaveSubject.CourseKey = Convert.ToInt32(CourseDDL.SelectedValue);
                    else
                        o_SaveSubject.CourseKey = 0;

                    

                    o_SaveSubject.subjectCode = Server.HtmlDecode(txt_SubjectCourse.Text);
                    o_SaveSubject.subjectDesc = Server.HtmlDecode(txt_SubjectCourseDesc.Text);

                    //if (txtSubjectFee.Value == "")
                    //{
                    //    txtSubjectFee.Value = "0";

                    //}
                    try
                    {
                        o_SaveSubject.subjectFees = Convert.ToDouble(txtSubjectFee.Value);
                    }
                    catch
                    {
                        o_SaveSubject.subjectFees = 0;
                    }



                    if (Mode == "UPDATE")
                        o_SaveSubject.subjectKey = Convert.ToInt32(txtSubjectKeyForEditMode.Text);

                    o_SaveSubject.createdBy = Convert.ToInt32(Session["LoginEmpKey"]); ;
                    o_SaveSubject.modifiedBy = Convert.ToInt32(Session["LoginEmpKey"]);

                    int retval = o_SaveSubject.save(ref Message, Mode);
                    if (retval > 0)
                    {
                        if (Mode == "UPDATE")
                            Lab_message.Text = "Subject updated successfully.";
                        else
                            Lab_message.Text = "Subject saved successfully.";

                        Mode = "INSERT";
                        //btn_Save.Enabled = false;
                        //// lab_CreatedByText.Text = "#99";
                        // lab_CreatedOnText.Text = Convert.ToString(System.DateTime.Now);
                        //// lab_ModifiedByText.Text = "#99";
                        // lab_ModifiedOnText.Text = Convert.ToString(System.DateTime.Now);
                        ClearControls();
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
            FillGridData();
            
        }
        private void FillGridData()
        {
            BAL.Class.SmartInstitute.SubjectClass o_GetSubj = new BAL.Class.SmartInstitute.SubjectClass();
            DataTable dtSubj = o_GetSubj.GetSubjectList(ref Message);
            grdSubject.DataSource = dtSubj;
            grdSubject.DataBind();

            dtSubject = dtSubj.Copy();
        }
        protected void btn_Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/WellcomePage.aspx");
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

        protected void grdSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bEditRights)
            {
                Lab_message.Text = "";
                Mode = "UPDATE";
                try
                {
                    grdSubject.SelectedRow.Focus();
                    txtSubjectKeyForEditMode.Text = grdSubject.SelectedRow.Cells[1].Text;
                    txt_SubjectCourse.Text = Server.HtmlDecode(grdSubject.SelectedRow.Cells[2].Text);
                    txt_SubjectCourseDesc.Text = Server.HtmlDecode(grdSubject.SelectedRow.Cells[3].Text);
                    CourseDDL.SelectedValue = grdSubject.SelectedRow.Cells[4].Text;
                    LoadSubCourseData(Convert.ToInt32(CourseDDL.SelectedValue));
                    SubCourseDDL.SelectedValue = grdSubject.SelectedRow.Cells[6].Text;
                    LoadGroupCourseList(Convert.ToInt32(SubCourseDDL.SelectedValue));

                    if (Server.HtmlDecode(grdSubject.SelectedRow.Cells[8].Text) != "")
                        GroupDDL.SelectedValue = grdSubject.SelectedRow.Cells[8].Text;

                    lab_CreatedByText.Text = Server.HtmlDecode(grdSubject.SelectedRow.Cells[12].Text);
                    lab_CreatedOnText.Text = Server.HtmlDecode(grdSubject.SelectedRow.Cells[11].Text);
                    lab_ModifiedOnText.Text = Server.HtmlDecode(grdSubject.SelectedRow.Cells[13].Text);
                    lab_ModifiedByText.Text = Server.HtmlDecode(grdSubject.SelectedRow.Cells[14].Text);

                    txtSubjectFee.Value = Server.HtmlDecode(grdSubject.SelectedRow.Cells[10].Text);

                    try
                    {
                        int iGroupKey = Convert.ToInt32(grdSubject.SelectedRow.Cells[8].Text);
                        if (iGroupKey > 0)
                            GroupDDL.SelectedValue = iGroupKey.ToString();
                    }
                    catch (Exception err)
                    { }

                    btnEdit.Enabled = true;
                    btn_Delete.Enabled = true;
                    btn_Save.Enabled = false;
                }
                catch (Exception err)
                { }
            }
            else
            {
                Lab_message.Text = "You are not having edit rights";
            }

        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            if (bDeleteRights)
            {
                Lab_message.Text = "";
                BAL.Class.SmartInstitute.SubjectClass o_DeleteSubject = new BAL.Class.SmartInstitute.SubjectClass();
                if (txtSubjectKeyForEditMode.Text != "" && txtSubjectKeyForEditMode.Text != "0")
                {
                    if (o_DeleteSubject.CheckForDependancy(Convert.ToInt32(txtSubjectKeyForEditMode.Text), ref Message))
                        Lab_message.Text = Message;
                    else
                    {
                        o_DeleteSubject.subjectKey = Convert.ToInt32(txtSubjectKeyForEditMode.Text);
                        int retval = o_DeleteSubject.save(ref Message, "DELETE");
                        Lab_message.Text = "Record deleted successfully";
                    }
                }
                else
                {
                    Lab_message.Text = "Please select record to delete";
                }
                Mode = "INSERT";
                FillGridData();
                ClearControls();
                btnEdit.Enabled = false;
                btn_Delete.Enabled = false;
                btn_Save.Enabled = true;
            }
            else
            {
                Lab_message.Text = "You are not allowed to delete record. Please contact administrator.";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Lab_message.Text = "";
            ClearControls();
            btnEdit.Enabled = false;
            if (bAddRights)
                btn_Save.Enabled = true;
            //btn_Save.Enabled = true;
        }

        protected void CourseDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lab_message.Text = "";
            if (CourseDDL.SelectedValue != null && CourseDDL.SelectedValue != "")
            {
                int iCourseKey = Convert.ToInt32(CourseDDL.SelectedValue);
                LoadSubCourseData(iCourseKey);
            }

        }

        protected void SubCourseDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lab_message.Text = "";
            if (SubCourseDDL.SelectedValue != null && SubCourseDDL.SelectedValue != "")
            {
                int iSubCourseKey = Convert.ToInt32(SubCourseDDL.SelectedValue);
                LoadGroupCourseList(iSubCourseKey);
            }
        }

        protected void grdSubject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (grdSubject.DataSource == null)
                grdSubject.DataSource = dtSubject;
            grdSubject.PageIndex = e.NewPageIndex;
            grdSubject.DataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            btn_Save_Click(sender, e);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BAL.Class.SmartInstitute.SubjectClass o_GetSubj = new BAL.Class.SmartInstitute.SubjectClass();
            DataTable dtSubj = o_GetSubj.GetSubjectListFilters(txtSubjectCodeSearch.Text.Trim(),ref Message);
            grdSubject.DataSource = dtSubj;
            grdSubject.DataBind();

            dtSubject = dtSubj.Copy();
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSubjectCodeSearch.Text = "";
            BAL.Class.SmartInstitute.SubjectClass o_GetSubj = new BAL.Class.SmartInstitute.SubjectClass();
            DataTable dtSubj = o_GetSubj.GetSubjectList(ref Message);
            grdSubject.DataSource = dtSubj;
            grdSubject.DataBind();

            dtSubject = dtSubj.Copy();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            GridView gvexport = new GridView();
            if (dtSubject != null && dtSubject.Rows.Count > 0)
            {
                for (int i = 0; i < dtSubject.Columns.Count; i++)
                {
                    if (dtSubject.Columns[i].Caption.ToLower().Contains("key"))
                        //if (gvexport.Columns[i].HeaderText.ToLower().Contains("key"))
                        dtSubject.Columns.Remove(dtSubject.Columns[i].Caption);
                }

                gvexport.DataSource = dtSubject;
                gvexport.DataBind();


                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "SubjectList.xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                Response.ContentType = "application/vnd.xls"; // This was where the mistake was in my old code Response.ContentType = "application/vnd.ms-excel";
                //Response.ContentType = "application/ms-excel";
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                gvexport.GridLines = GridLines.Both;
                gvexport.HeaderStyle.Font.Bold = true;
                gvexport.RenderControl(htmltextwrtter);
                Response.Write(strwritter.ToString());
                Response.End();
            }
            else
                Lab_message.Text = "No records to export";
        }
    }
}