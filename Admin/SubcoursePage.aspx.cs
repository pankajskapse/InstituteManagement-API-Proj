using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Admin
{
    public partial class SubcoursePage : System.Web.UI.Page
    {
        string Message = string.Empty;
        string Mode = "INSERT";
        static bool bEditRights = true;
        static bool bAddRights = true;
        static bool bDeleteRights = true;
        int iMenuKey = 14;
        static DataTable dtSubCourse;
        protected void Page_Load(object sender, EventArgs e)
        {
            Lab_message.Text = "";
            LoadCourseList();
            
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
            if (IsPostBack && CourseDDL.SelectedValue != null)
                iCourseKey = Convert.ToInt32(CourseDDL.SelectedValue);

            DataTable dtCourse = new DataTable();
            BAL.Class.SmartInstitute.CourseClass o_GetCourse = new BAL.Class.SmartInstitute.CourseClass();
            dtCourse = o_GetCourse.GetCourseList(ref Message);
            CourseDDL.DataSource = dtCourse;
            CourseDDL.DataBind();

            if (IsPostBack && iCourseKey > 0)
                CourseDDL.SelectedValue = iCourseKey.ToString();
        }
        protected void CourseDDL_Load(object sender, EventArgs e)
        {
            DAL.DAL dal = new DAL.DAL();
            SqlDataReader CourseList = null;
            CourseList = dal.GetReader("Select courseKey,courseCode from m_course", ref Message);

            while (CourseList.Read())
            {
                CourseDDL.Items.Add(CourseList["courseCode"].ToString());
            }
        }
        private bool ValidateData()
        {
            Lab_message.Text = "";
            if (txt_SubCourse.Text.Trim() == "")
                Lab_message.Text = "Sub Course code is required field";
            else if (CourseDDL.SelectedItem == null)
                Lab_message.Text = "Select Course first";

            if (Lab_message.Text.Trim() != "")
                return false;
            else return true;
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            Lab_message.Text = "";
            BAL.Class.SmartInstitute.SubCourseClass o_SaveSubCourse = new BAL.Class.SmartInstitute.SubCourseClass();
            if (ValidateData())
            {
                object CourseKey = null;
                if (CourseDDL.SelectedItem != null)
                    CourseKey = Convert.ToString(CourseDDL.SelectedValue);
                else CourseKey = 0;

                if (o_SaveSubCourse.CheckDuplicateSubCourse(txt_SubCourse.Text.Trim(), Convert.ToInt32(CourseKey), Mode, ref Message))
                {
                    Lab_message.Text = "This Sub-Course already exists for course" + CourseDDL.SelectedItem.Text + ". Please enter another";
                    txt_SubCourse.Text = "";
                }
                else
                {
                    if (txtSubCourseKeyForEditMode.Text != "" && txtSubCourseKeyForEditMode.Text != "0")
                        Mode = "UPDATE";
                    else Mode = "INSERT";
                    // Save Course Master DATA Insert into m_course table//         

                    Message = string.Empty;
                    DAL.DAL dal = new DAL.DAL();
                   
                    o_SaveSubCourse.CourseKey = Convert.ToInt32(CourseKey);
                    o_SaveSubCourse.SubCourseCode = Server.HtmlDecode(txt_SubCourse.Text);
                    o_SaveSubCourse.SubCourseDesc = Server.HtmlDecode(txt_SubCourseDesc.Text);
                    o_SaveSubCourse.createdBy = Convert.ToInt32(Session["LoginEmpKey"]);
                    o_SaveSubCourse.modifiedBy = Convert.ToInt32(Session["LoginEmpKey"]);

                    if (Mode == "UPDATE")
                        o_SaveSubCourse.SubCourseKey = Convert.ToInt32(txtSubCourseKeyForEditMode.Text);

                    int retval = o_SaveSubCourse.save(ref Message, Mode);
                    if (retval > 0)
                    {
                        if (Mode == "UPDATE")
                            Lab_message.Text = "Sub-Course updated successfully.";
                        else Lab_message.Text = "Sub-Course saved successfully.";

                        Mode = "INSERT";
                        //btn_Save.Enabled = false;
                        //lab_CreatedByText.Text = "#99";
                        //lab_CreatedOnText.Text = Convert.ToString(System.DateTime.Now);
                        //lab_ModifiedByText.Text = "#99";
                        //lab_ModifiedOnText.Text = Convert.ToString(System.DateTime.Now);
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

        protected void grdSubCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bEditRights)
            {
                if (grdSubCourse.SelectedRow != null)
                {
                    // btn_Save.Enabled = true;
                    Mode = "UPDATE";
                    grdSubCourse.SelectedRow.Focus();
                    txtSubCourseKeyForEditMode.Text = grdSubCourse.SelectedRow.Cells[1].Text;
                    txt_SubCourse.Text = grdSubCourse.SelectedRow.Cells[2].Text;
                    txt_SubCourseDesc.Text = grdSubCourse.SelectedRow.Cells[3].Text;

                    CourseDDL.Text = grdSubCourse.SelectedRow.Cells[4].Text;

                    lab_CreatedByText.Text = Server.HtmlDecode(grdSubCourse.SelectedRow.Cells[8].Text);
                    lab_CreatedOnText.Text = Server.HtmlDecode(grdSubCourse.SelectedRow.Cells[7].Text);
                    lab_ModifiedOnText.Text = Server.HtmlDecode(grdSubCourse.SelectedRow.Cells[9].Text);
                    lab_ModifiedByText.Text = Server.HtmlDecode(grdSubCourse.SelectedRow.Cells[10].Text);
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
            txtSubCourseKeyForEditMode.Text = "0";
            txt_SubCourse.Text = "";
            txt_SubCourseDesc.Text = "";
            Mode = "INSERT";
        }
        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            if (bDeleteRights)
            {
                Lab_message.Text = "";
                BAL.Class.SmartInstitute.SubCourseClass o_DeleteSubCls = new BAL.Class.SmartInstitute.SubCourseClass();
                if (txtSubCourseKeyForEditMode.Text != "" && txtSubCourseKeyForEditMode.Text != "0")
                {
                    if (o_DeleteSubCls.CheckForDependancy(Convert.ToInt32(txtSubCourseKeyForEditMode.Text), ref Message))
                        Lab_message.Text = Message;
                    else
                    {
                        o_DeleteSubCls.SubCourseKey = Convert.ToInt32(txtSubCourseKeyForEditMode.Text);
                        int retval = o_DeleteSubCls.save(ref Message, "DELETE");
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
                /* Commented by Pankaj to to fix an issue after click on Delete button Save button disable
                 * and npot able to save new record */
                // btn_Save.Enabled = false;    
                // btn_Delete.Enabled = false;    
            }
            else
            {
                Lab_message.Text = "You are not allowed to delete record. Please contact administrator.";
            }

        }
        private void FillGridData()
        {
            BAL.Class.SmartInstitute.SubCourseClass o_GetSubCls = new BAL.Class.SmartInstitute.SubCourseClass();
            DataTable dtGrp = o_GetSubCls.GetSubCourseList(ref Message);
            grdSubCourse.DataSource = dtGrp;
            grdSubCourse.DataBind();

            dtSubCourse = dtGrp.Copy();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //btn_Save.Enabled = true;
            Lab_message.Text = "";
            ClearControls();
            btnEdit.Enabled = false;
            btn_Delete.Enabled = false;
            if (bAddRights)
                btn_Save.Enabled = true;
        }

        protected void grdSubCourse_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (grdSubCourse.DataSource == null)
                grdSubCourse.DataSource = dtSubCourse;
            grdSubCourse.PageIndex = e.NewPageIndex;
            grdSubCourse.DataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            btn_Save_Click(sender, e);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BAL.Class.SmartInstitute.SubCourseClass o_GetSubCls = new BAL.Class.SmartInstitute.SubCourseClass();
            DataTable dtGrp = o_GetSubCls.GetSubCourseListFilters(txtSubCourseCodeSearch.Text.Trim(),ref Message);
            grdSubCourse.DataSource = dtGrp;
            grdSubCourse.DataBind();

            dtSubCourse = dtGrp.Copy();
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSubCourseCodeSearch.Text = "";
            BAL.Class.SmartInstitute.SubCourseClass o_GetSubCls = new BAL.Class.SmartInstitute.SubCourseClass();
            DataTable dtGrp = o_GetSubCls.GetSubCourseList(ref Message);
            grdSubCourse.DataSource = dtGrp;
            grdSubCourse.DataBind();

            dtSubCourse = dtGrp.Copy();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            GridView gvexport = new GridView();
            if (dtSubCourse != null && dtSubCourse.Rows.Count > 0)
            {
                for (int i = 0; i < dtSubCourse.Columns.Count; i++)
                {
                    if (dtSubCourse.Columns[i].Caption.ToLower().Contains("key"))
                        //if (gvexport.Columns[i].HeaderText.ToLower().Contains("key"))
                        dtSubCourse.Columns.Remove(dtSubCourse.Columns[i].Caption);
                }

                gvexport.DataSource = dtSubCourse;
                gvexport.DataBind();


                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "SubCourseList.xls";
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