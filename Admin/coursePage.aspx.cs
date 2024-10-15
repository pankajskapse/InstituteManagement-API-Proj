using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace InstituteManagement.Admin
{
    public partial class coursePage : System.Web.UI.Page
    {
        string Message = string.Empty;
        string Mode = "INSERT";
        static bool bEditRights = true;
        static bool bAddRights = true;
        static bool bDeleteRights = true;
        int iMenuKey = 5;
        static DataTable dtCourse;
        protected void Page_Load(object sender, EventArgs e)
        {
            Lab_message.Text = "";

            if (!IsPostBack)
            {
                ApplyLoginEmpRights();
                FillGridViewData();
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
                                btn_Add.Enabled = true;
                                btn_Save.Enabled = true;
                                bAddRights = true;
                            }
                            else
                            {
                                btn_Add.Enabled = false;
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
        private void FillGridViewData()
        {
            BAL.Class.SmartInstitute.CourseClass o_GetCourse = new BAL.Class.SmartInstitute.CourseClass();
            DataTable dtBatch = o_GetCourse.GetCourseList(ref Message);
            grdCourse.DataSource = dtBatch;
            grdCourse.DataBind();

            dtCourse = dtBatch.Copy();
        }

        private bool ValidateData()
        {
            Lab_message.Text = "";
            if (txt_Course.Text.Trim() == "")
                Lab_message.Text = "Course code is compulsory field.";
            else if (txt_CourseDesc.Text.Trim() == "")
                Lab_message.Text = "Course Description is compulsory field.";

            if (Lab_message.Text.Trim() == "")
                return true;
            return false;
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            BAL.Class.SmartInstitute.CourseClass o_SaveCourse = new BAL.Class.SmartInstitute.CourseClass();
            Lab_message.Text = "";
            //string mode = "INSERT";           
            //string mode = "";
            if (txtCourseKeyForEditMode.Text != "" && txtCourseKeyForEditMode.Text != "0")
                Mode = "UPDATE";
            else Mode = "INSERT";

            if (ValidateData())
            {
                if (o_SaveCourse.CheckDuplicateCourse(txt_Course.Text.Trim(), Mode, ref Message))
                {
                    Lab_message.Text = "This Course already exists. Please enter another";
                    txt_Course.Text = "";
                }
                else
                {
                    Message = string.Empty;


                    if (Mode == "UPDATE")
                        o_SaveCourse.CourseKey = Convert.ToInt32(txtCourseKeyForEditMode.Text);

                    o_SaveCourse.CourseCode = Server.HtmlDecode(txt_Course.Text);
                    o_SaveCourse.CourseDesc = Server.HtmlDecode(txt_CourseDesc.Text);
                    o_SaveCourse.createdBy = Convert.ToInt32(Session["LoginEmpKey"]);
                    //o_SaveBatch.createdOn = System.DateTime.Now;
                    o_SaveCourse.modifiedBy = Convert.ToInt32(Session["LoginEmpKey"]);
                    //o_SaveBatch.modifiedOn = Convert.ToString(System.DateTime.Now);
                    //o_SaveBatch.Other1 = "";
                    //o_SaveBatch.Other2 = "";
                    //o_SaveBatch.Other3 = "";
                    //o_SaveBatch.Other4 = "";

                    int retval = o_SaveCourse.save(ref Message, Mode);
                    if (retval > 0)
                    {
                        if (Mode == "UPDATE")
                            Lab_message.Text = "Course updated successfully.";
                        else
                            Lab_message.Text = "Course saved successfully.";
                        Mode = "INSERT";
                        //lab_CreatedByText.Text = "#99";
                        // lab_CreatedOnText.Text = Convert.ToString(System.DateTime.Now);
                        //lab_ModifiedByText.Text = "#99";
                        // lab_ModifiedOnText.Text = Convert.ToString(System.DateTime.Now);
                        ClearControls();
                        btnEdit.Enabled = false;
                    }
                    else
                    {
                        Lab_message.Text = Convert.ToString(Message);
                    }
                }
                // btn_Add.Enabled = true;
            }
            FillGridViewData();
            foreach (GridViewRow row1 in grdCourse.Rows)
            {
                row1.BackColor = System.Drawing.Color.LightGray;
                row1.ForeColor = System.Drawing.Color.Black;
            }
        }

        protected void btn_Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/WellcomePage.aspx");
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            Lab_message.Text = "";
            ClearControls();
            btnEdit.Enabled = false;
            btn_Delete.Enabled = false;
            if (bAddRights)
                btn_Save.Enabled = true;
        }
        private void ClearControls()
        {
            txtCourseKeyForEditMode.Text = "0";
            txt_Course.Text = "";
            txt_CourseDesc.Text = "";
            lab_CreatedByText.Text = "";
            lab_ModifiedByText.Text = "";
            lab_CreatedOnText.Text = "";
            lab_ModifiedOnText.Text = "";
            Mode = "INSERT";
        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            if (bDeleteRights)
            {
                try
                {
                    Lab_message.Text = "";
                    BAL.Class.SmartInstitute.CourseClass o_DeleteCourse = new BAL.Class.SmartInstitute.CourseClass();
                    if (txtCourseKeyForEditMode.Text != "" && txtCourseKeyForEditMode.Text != "0")
                    {
                        if (o_DeleteCourse.CheckForDependancy(Convert.ToInt32(txtCourseKeyForEditMode.Text), ref Message))
                            Lab_message.Text = Message;
                        else
                        {
                            o_DeleteCourse.CourseKey = Convert.ToInt32(txtCourseKeyForEditMode.Text);
                            int retval = o_DeleteCourse.save(ref Message, "DELETE");
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
                catch (Exception err)
                {
                    Lab_message.Text = Message;
                }
            }
            else
            {
                Lab_message.Text = "You are not allowed to delete record. Please contact administrator.";
            }

        }


        protected void grdCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bEditRights)
            {
                if (grdCourse.SelectedRow != null)
                {
                    Lab_message.Text = "";
                    Mode = "UPDATE";
                    grdCourse.SelectedRow.Focus();
                    txtCourseKeyForEditMode.Text = grdCourse.SelectedRow.Cells[1].Text;
                    txt_Course.Text = Server.HtmlDecode(grdCourse.SelectedRow.Cells[2].Text);
                    txt_CourseDesc.Text = Server.HtmlDecode(grdCourse.SelectedRow.Cells[3].Text);
                    lab_CreatedByText.Text = Server.HtmlDecode(grdCourse.SelectedRow.Cells[5].Text);
                    lab_CreatedOnText.Text = Server.HtmlDecode(grdCourse.SelectedRow.Cells[4].Text);
                    lab_ModifiedOnText.Text = Server.HtmlDecode(grdCourse.SelectedRow.Cells[6].Text);
                    lab_ModifiedByText.Text = Server.HtmlDecode(grdCourse.SelectedRow.Cells[7].Text);
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

        protected void grdCourse_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (grdCourse.DataSource == null)
                grdCourse.DataSource = dtCourse;
            grdCourse.PageIndex = e.NewPageIndex;
            grdCourse.DataBind();
        }

        protected void grdCourse_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            btn_Save_Click(sender, e);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BAL.Class.SmartInstitute.CourseClass o_GetCourse = new BAL.Class.SmartInstitute.CourseClass();
            DataTable dtBatch = o_GetCourse.GetCourseListFilters(txtCourseCodeSearch.Text.Trim(), ref Message);
            grdCourse.DataSource = dtBatch;
            grdCourse.DataBind();

            dtCourse = dtBatch.Copy();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            GridView gvexport = new GridView();
            if (dtCourse != null && dtCourse.Rows.Count > 0)
            {
                for (int i = 0; i < dtCourse.Columns.Count; i++)
                {
                    if (dtCourse.Columns[i].Caption.ToLower().Contains("key"))
                        //if (gvexport.Columns[i].HeaderText.ToLower().Contains("key"))
                        dtCourse.Columns.Remove(dtCourse.Columns[i].Caption);
                }

                gvexport.DataSource = dtCourse;
                gvexport.DataBind();


                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "CourseList.xls";
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

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtCourseCodeSearch.Text = "";
            BAL.Class.SmartInstitute.CourseClass o_GetCourse = new BAL.Class.SmartInstitute.CourseClass();
            DataTable dtBatch = o_GetCourse.GetCourseList(ref Message);
            grdCourse.DataSource = dtBatch;
            grdCourse.DataBind();

            dtCourse = dtBatch.Copy();
        }
    }
}
