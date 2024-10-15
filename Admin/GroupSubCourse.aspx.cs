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
    public partial class GroupSubCourse : System.Web.UI.Page
    {
        string Message = string.Empty;
        string Mode = "INSERT";
        static bool bEditRights = true;
        static bool bAddRights = true;
        static bool bDeleteRights = true;
        static DataTable dtGroup;
        int iMenuKey = 12;
        protected void Page_Load(object sender, EventArgs e)
        {
            Lab_message.Text = "";
            LoadCourseList();
            
            if (CourseDDL.SelectedItem != null)
            {
                int iCourseKey = Convert.ToInt32(CourseDDL.SelectedValue);
                LoadSubCourseData(iCourseKey);
            }
            if(!IsPostBack)
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
            if (IsPostBack && CourseDDL.SelectedItem != null)
                iCourseKey = Convert.ToInt32(CourseDDL.SelectedValue);

            DataTable dtCourse = new DataTable();
            BAL.Class.SmartInstitute.CourseClass o_GetCourse = new BAL.Class.SmartInstitute.CourseClass();
            dtCourse = o_GetCourse.GetCourseList(ref Message);
            CourseDDL.DataSource = dtCourse;
            CourseDDL.DataBind();

            if (IsPostBack && iCourseKey > 0)
                CourseDDL.SelectedValue = iCourseKey.ToString();
        }
        private void LoadSubCourseData(int iCourseKey)
        {
            int isubCourseKey = 0;
            string SubCourse = "";
            if (SubCourseDDL.SelectedItem != null)
                SubCourse = SubCourseDDL.SelectedItem.Text;

            if (IsPostBack && SubCourseDDL.SelectedItem != null)
                isubCourseKey = Convert.ToInt32(SubCourseDDL.SelectedValue);

            DataTable dtSubCourse = new DataTable();
            SubCourseDDL.DataSource = null;
            SubCourseDDL.DataBind();

            BAL.Class.SmartInstitute.SubCourseClass o_GetSubCourse = new BAL.Class.SmartInstitute.SubCourseClass();
            dtSubCourse = o_GetSubCourse.GetSubCourseListForCourse(iCourseKey, ref Message);
            SubCourseDDL.DataSource = dtSubCourse;
            SubCourseDDL.DataBind();


            ListItem subCourse = new ListItem();
            subCourse.Value = isubCourseKey.ToString();
            subCourse.Text = SubCourse;
            if (IsPostBack && isubCourseKey > 0)
            {
                if (SubCourseDDL.Items.Contains(subCourse))
                    SubCourseDDL.SelectedValue = isubCourseKey.ToString();
            }

            

        }
        protected void CourseDDL_Load(object sender, EventArgs e)
        {

        }
        private bool ValidateData()
        {
            Lab_message.Text = "";
            if (CourseDDL.SelectedItem == null)
                Lab_message.Text = "Course is compulsary field.";
            else if (SubCourseDDL.SelectedItem == null)
                Lab_message.Text = "SubCourse is compulsary field.";
            else if (txt_GroupSubCourse.Text.Trim() == "")
                Lab_message.Text = "Group Code is compulsary field.";
            else if (txt_GroupSubCourseDesc.Text.Trim() == "")
                Lab_message.Text = "Group Desc is compulsary field.";

            if (Lab_message.Text.Trim() == "")
                return true;
            else return false;
        }

        private void FillGridData()
        {
            BAL.Class.SmartInstitute.GroupSubCourseClass o_GetSubGrp = new BAL.Class.SmartInstitute.GroupSubCourseClass();
            DataTable dtGrp = o_GetSubGrp.GetGroupsList(ref Message);
            grdGroup.DataSource = dtGrp;
            grdGroup.DataBind();

            dtGroup = dtGrp.Copy();
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            BAL.Class.SmartInstitute.GroupSubCourseClass o_SaveGroupSubCourse = new BAL.Class.SmartInstitute.GroupSubCourseClass();
            if (txtGroupKeyForEditMode.Text != "" && txtGroupKeyForEditMode.Text != "0")
                Mode = "UPDATE";
            else Mode = "INSERT";

            if (ValidateData())
            {
                Message = string.Empty;

                if (SubCourseDDL.SelectedItem != null)
                    o_SaveGroupSubCourse.SubCourseKey = Convert.ToInt32(SubCourseDDL.SelectedValue);
                else
                    o_SaveGroupSubCourse.SubCourseKey = 0;

                if (o_SaveGroupSubCourse.CheckDuplicateGroup(txt_GroupSubCourse.Text.Trim(), o_SaveGroupSubCourse.SubCourseKey, Mode, ref Message))
                {
                    Lab_message.Text = "This Group already exists in Sub-Course "+ SubCourseDDL.SelectedItem.Text + ". Please enter another";
                    txt_GroupSubCourse.Text = "";
                }
                else
                {                  

                    o_SaveGroupSubCourse.groupsubcourseCode = Server.HtmlDecode(txt_GroupSubCourse.Text);
                    o_SaveGroupSubCourse.groupsubcourseDesc = Server.HtmlDecode(txt_GroupSubCourseDesc.Text);

                    if (Mode == "UPDATE")
                        o_SaveGroupSubCourse.groupsubcourseKey = Convert.ToInt32(txtGroupKeyForEditMode.Text);

                    o_SaveGroupSubCourse.createdBy = Convert.ToInt32(Session["LoginEmpKey"]);
                    o_SaveGroupSubCourse.modifiedBy = Convert.ToInt32(Session["LoginEmpKey"]);

                    int retval = o_SaveGroupSubCourse.save(ref Message, Mode);
                    if (retval > 0)
                    {
                        if (Mode == "UPDATE")
                            Lab_message.Text = "Group updated successfully.";
                        else
                            Lab_message.Text = "Group saved successfully.";

                        Mode = "INSERT";
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
        protected void btn_Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/WellcomePage.aspx");
        }

        protected void SubCourseDDL_Load(object sender, EventArgs e)
        {

        }

        protected void grdGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bEditRights)
            {
                if (grdGroup.SelectedRow != null)
                {
                    Mode = "UPDATE";
                    grdGroup.SelectedRow.Focus();
                    txtGroupKeyForEditMode.Text = grdGroup.SelectedRow.Cells[1].Text;
                    txt_GroupSubCourse.Text = grdGroup.SelectedRow.Cells[2].Text;
                    txt_GroupSubCourseDesc.Text = grdGroup.SelectedRow.Cells[3].Text;

                    int iCourseKey = 0;
                    if (IsPostBack && CourseDDL.SelectedValue != null && CourseDDL.SelectedValue != "")
                        iCourseKey = Convert.ToInt32(CourseDDL.SelectedValue);

                    CourseDDL.SelectedValue = grdGroup.SelectedRow.Cells[6].Text;

                    //if (IsPostBack && iCourseKey > 0)
                    //    CourseDDL.SelectedValue = iCourseKey.ToString();

                    LoadSubCourseData(Convert.ToInt32(CourseDDL.SelectedValue));
                    SubCourseDDL.Text = grdGroup.SelectedRow.Cells[4].Text;

                    lab_CreatedByText.Text = Server.HtmlDecode(grdGroup.SelectedRow.Cells[9].Text);
                    lab_CreatedOnText.Text = Server.HtmlDecode(grdGroup.SelectedRow.Cells[8].Text);
                    lab_ModifiedOnText.Text = Server.HtmlDecode(grdGroup.SelectedRow.Cells[10].Text);
                    lab_ModifiedByText.Text = Server.HtmlDecode(grdGroup.SelectedRow.Cells[11].Text);
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
        private void ClearControls()
        {
            txt_GroupSubCourseDesc.Text = "";
            txtGroupKeyForEditMode.Text = "";
            txt_GroupSubCourse.Text = "";
            lab_CreatedByText.Text = "";
            lab_ModifiedByText.Text = "";
            lab_CreatedOnText.Text = "";
            lab_CreatedOnText.Text = "";
            Mode = "INSERT";
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearControls();
            btnEdit.Enabled = false;
            btn_Delete.Enabled = false;
            if (bAddRights)
                btn_Save.Enabled = true;
        }


        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            if (bDeleteRights)
            {
                BAL.Class.SmartInstitute.GroupSubCourseClass o_DeleteGroup = new BAL.Class.SmartInstitute.GroupSubCourseClass();
                if (txtGroupKeyForEditMode.Text != "" && txtGroupKeyForEditMode.Text != "0")
                {
                    if (o_DeleteGroup.CheckForDependancy(Convert.ToInt32(txtGroupKeyForEditMode.Text), ref Message))
                        Lab_message.Text = Message;
                    else
                    {
                        o_DeleteGroup.groupsubcourseKey = Convert.ToInt32(txtGroupKeyForEditMode.Text);
                        int retval = o_DeleteGroup.save(ref Message, "DELETE");
                        Lab_message.Text = "Record deleted successfully";
                    }
                }
                else
                {
                    Lab_message.Text = "Please select record to delete";
                }
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

        protected void CourseDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iCourse = Convert.ToInt32(CourseDDL.SelectedValue);
            LoadSubCourseData(iCourse);
        }

        protected void grdGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (grdGroup.DataSource == null)
                grdGroup.DataSource = dtGroup;
            grdGroup.PageIndex = e.NewPageIndex;
            grdGroup.DataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            btn_Save_Click(sender, e);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BAL.Class.SmartInstitute.GroupSubCourseClass o_GetSubGrp = new BAL.Class.SmartInstitute.GroupSubCourseClass();
            DataTable dtGrp = o_GetSubGrp.GetGroupsListFilter(txtGroupCodeSearch.Text.Trim(),ref Message);
            grdGroup.DataSource = dtGrp;
            grdGroup.DataBind();

            dtGroup = dtGrp.Copy();
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtGroupCodeSearch.Text = "";
            BAL.Class.SmartInstitute.GroupSubCourseClass o_GetSubGrp = new BAL.Class.SmartInstitute.GroupSubCourseClass();
            DataTable dtGrp = o_GetSubGrp.GetGroupsList(ref Message);
            grdGroup.DataSource = dtGrp;
            grdGroup.DataBind();

            dtGroup = dtGrp.Copy();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            GridView gvexport = new GridView();
            if (dtGroup != null && dtGroup.Rows.Count > 0)
            {
                for (int i = 0; i < dtGroup.Columns.Count; i++)
                {
                    if (dtGroup.Columns[i].Caption.ToLower().Contains("key"))
                        //if (gvexport.Columns[i].HeaderText.ToLower().Contains("key"))
                        dtGroup.Columns.Remove(dtGroup.Columns[i].Caption);
                }

                gvexport.DataSource = dtGroup;
                gvexport.DataBind();


                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "GroupList.xls";
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