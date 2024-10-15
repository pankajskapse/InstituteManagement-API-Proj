using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Admin
{
    public partial class AdmissionList : System.Web.UI.Page
    {
        string Message = string.Empty;
        static DataTable dtAdmission;
        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Request.QueryString["ForPopup"] == null && !Convert.ToBoolean(Request.QueryString["ForPopup"]))
                this.MasterPageFile = "~/MasterPage/Standard.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lab_message.Text = "";
            if (!IsPostBack)
            {
                FillAdmissionGrid();
                LoadBatchList();
                LoadCouseList();
                chkCourse_CheckedChanged(null, null);
                chkSubCourse_CheckedChanged(null, null);
                chkGroup_CheckedChanged(null, null);
                chkSubject_CheckedChanged(null, null);
            }

        }
        private void FillAdmissionGrid()
        {
            BAL.Class.SmartInstitute.AdmissionClass o_GetAdmission = new BAL.Class.SmartInstitute.AdmissionClass();
            DataSet dsAdmission = o_GetAdmission.GetAdmissionList(ref Message);
            grdAdmissionList.DataSource = dsAdmission;
            grdAdmissionList.DataBind();

            if (dsAdmission != null && dsAdmission.Tables != null && dsAdmission.Tables[0].Rows.Count > 0)
            {
                lblRecordCount.Text = dsAdmission.Tables[0].Rows.Count.ToString();
                dtAdmission = dsAdmission.Tables[0].Copy();
            }
            else
            {
                lblRecordCount.Text = "0";
                dtAdmission = null;
            }

            if (!IsPostBack)
            {
                SetFromToPanelState();
                SetBatchDropDownState();
            }
        }
        private void LoadCouseList()
        {
            BAL.Class.SmartInstitute.CourseClass o_GetCourse = new BAL.Class.SmartInstitute.CourseClass();
            DataTable dtCourse = new DataTable();
            dtCourse = o_GetCourse.GetCourseList(ref Message);
            ddlCourse.DataSource = dtCourse;
            ddlCourse.DataBind();

        }
        private void LoadSubCourseList()
        {
            BAL.Class.SmartInstitute.SubCourseClass o_GetSubCourse = new BAL.Class.SmartInstitute.SubCourseClass();

            DataTable dtChild = null;

            if (ddlCourse.SelectedItem != null && ddlCourse.SelectedItem.Value != null)
            {
                dtChild = o_GetSubCourse.GetSubCourseListForCourse(Convert.ToInt32(ddlCourse.SelectedItem.Value), ref Message);
                ddlSubCourse.DataSource = dtChild;
                ddlSubCourse.DataBind();

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

                if (chkCourse.Checked && ddlCourse.SelectedItem != null)
                    iCoursekey = Convert.ToInt32(ddlCourse.SelectedValue);
                if (chkSubCourse.Checked && ddlSubCourse.SelectedItem != null)
                    iSubCourseKey = Convert.ToInt32(ddlSubCourse.SelectedValue);
                if (chkGroup.Checked && ddlGroup.SelectedItem != null && divGroupList.Visible)
                    iGroupKey = Convert.ToInt32(ddlGroup.SelectedValue);

                ddlBatchList.DataSource = o_Batch.GetBatchListByFilters(iCoursekey, iSubCourseKey, iGroupKey, ref Message);
                ddlBatchList.DataBind();
            }
            catch (Exception err)
            {
                lab_message.Text = err.Message;
            }
        }
        protected void grdAdmissionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if(grdAdmissionList.DataSource==null)
                grdAdmissionList.DataSource = dtAdmission;
            grdAdmissionList.PageIndex = e.NewPageIndex;
            grdAdmissionList.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string whereCondition = "";
            if (chkFromToDateFilter.Checked)
                whereCondition = "where admissionDate BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "'";

            if (chkBatchWise.Checked)
            {
                if (ddlBatchList.SelectedValue != null && ddlBatchList.SelectedItem != null)
                    if (whereCondition.Trim() != "")
                        whereCondition = whereCondition + " and ";
                    else
                        whereCondition = " where";

                if (ddlBatchList.SelectedValue != "" && ddlBatchList.SelectedItem != null)
                    whereCondition = whereCondition + " batchKey=" + ddlBatchList.SelectedValue;
            }
            if (chkCourse.Checked)
            {
                if (ddlCourse.SelectedValue != "" && ddlCourse.SelectedItem != null)
                    if (whereCondition.Trim() != "")
                        whereCondition = whereCondition + " and ";
                    else
                        whereCondition = " where admissionKey in (select admissionKey from d_admissionDetails where ";

                if (ddlCourse.SelectedValue != "" && ddlCourse.SelectedItem != null)
                    whereCondition = whereCondition + " courseKey=" + ddlCourse.SelectedValue;
            }
            if (chkSubCourse.Checked)
            {
                if (ddlSubCourse.SelectedValue != "" && ddlSubCourse.SelectedItem != null)
                    if (whereCondition.Trim() != "")
                        whereCondition = whereCondition + " and ";
                    else
                        whereCondition = " where admissionKey in (select admissionKey from d_admissionDetails where";

                if (ddlSubCourse.SelectedValue != "" && ddlSubCourse.SelectedItem != null)
                    whereCondition = whereCondition + " subcourseKey=" + ddlSubCourse.SelectedValue;
            }
            if (chkGroup.Checked)
            {
                if (ddlGroup.SelectedValue != "" && ddlGroup.SelectedItem != null)
                    if (whereCondition.Trim() != "")
                        whereCondition = whereCondition + " and ";
                    else
                        whereCondition = " where admissionKey in (select admissionKey from d_admissionDetails where";

                if (ddlGroup.SelectedValue != "" && ddlGroup.SelectedItem != null)
                    whereCondition = whereCondition + " groupKey=" + ddlGroup.SelectedValue;
            }
            if (whereCondition.Contains("(") && !chkSubject.Checked)
                whereCondition = whereCondition + " )";

            if (chkSubject.Checked)
            {
               
                if (ddlSubject.SelectedValue != "" && ddlSubject.SelectedItem != null)
                {
                    if (whereCondition.Trim() != "" && whereCondition.Contains("d_admissionDetails"))
                        whereCondition = whereCondition + " and subjectKey=" + ddlSubject.SelectedValue + ")";
                    else
                        whereCondition = whereCondition + " and admissionKey in (select admissionKey from d_admissionDetails where subjectKey=" + ddlSubject.SelectedValue + ")";
                }
                else
                    whereCondition = " where enquiryKey in (select admissionKey from d_admissionDetails where subjectKey=" + ddlSubject.SelectedValue + ")";

                //if (ddlSubject.SelectedValue != "" && ddlSubject.SelectedItem != null)
                //    whereCondition = whereCondition + " subjectKey=" + ddlSubject.SelectedValue;
            }


            BAL.Class.SmartInstitute.AdmissionClass o_GetAdmission = new BAL.Class.SmartInstitute.AdmissionClass();
            DataSet dsAdmission = o_GetAdmission.GetAdmissionListWithFilters(whereCondition, ref Message);
            grdAdmissionList.DataSource = dsAdmission;
            grdAdmissionList.DataBind();

            if (dsAdmission != null && dsAdmission.Tables != null && dsAdmission.Tables[0].Rows.Count > 0)
            {
                lblRecordCount.Text = dsAdmission.Tables[0].Rows.Count.ToString();
                dtAdmission = dsAdmission.Tables[0].Copy();
            }
            else
            {
                dtAdmission = null;
                lblRecordCount.Text = "0";
            }
        }
        private void SetFromToPanelState()
        {
            if (chkFromToDateFilter.Checked)
            {
                pnlFromToDate.Enabled = true;
                if (txtFromDate.Text == "")
                    txtFromDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                if (txtToDate.Text == "")
                    txtToDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else pnlFromToDate.Enabled = false;
        }
        protected void chkFromToDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            SetFromToPanelState();
        }
        private void SetBatchDropDownState()
        {
            if (chkBatchWise.Checked)
                ddlBatchList.Enabled = true;
            else ddlBatchList.Enabled = false;
        }
        protected void chkBatchWise_CheckedChanged(object sender, EventArgs e)
        {
            SetBatchDropDownState();
        }

        protected void grdAdmissionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdAdmissionList.SelectedRow != null)
            {
                string AdmKey = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[1].Text);
                string AdmCode = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[2].Text);
                DateTime AdmDate = Convert.ToDateTime(grdAdmissionList.SelectedRow.Cells[3].Text);
                string fName = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[4].Text);
                string lName = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[5].Text);
                string MobileNo = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[6].Text).Trim();
                string batchKey = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[7].Text);
                string admissionTypeKey = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[9].Text);
                DateTime DOB = Convert.ToDateTime(grdAdmissionList.SelectedRow.Cells[10].Text);
                string emailID = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[11].Text);
                string altPhNp = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[12].Text).Trim();
               
                string gender = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[13].Text);
                string collegeName = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[14].Text);
                string AdmissionOwner = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[15].Text);
                string TotalFee = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[16].Text).Trim().Trim();
                decimal firstInstallment = Convert.ToDecimal(Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[17].Text).Trim());
                decimal secondInstallment = Convert.ToDecimal(Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[18].Text).Trim());
                decimal thirdInstallment = Convert.ToDecimal(Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[19].Text).Trim());
                decimal fourthInstallment = Convert.ToDecimal(Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[20].Text).Trim());
                string address = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[21].Text);
                string remark = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[22].Text);
                string source = Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[23].Text);

                decimal fifthInstallment = Convert.ToDecimal(Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[24].Text).Trim());
                decimal sixthInstallment = Convert.ToDecimal(Server.HtmlDecode(grdAdmissionList.SelectedRow.Cells[25].Text).Trim());

                StringBuilder script = new StringBuilder();
                script.Append("<script type='text/javascript'>");
                //script.Append("window.opener.loadEnquiryDataOne('" + EnqID +"');");
                script.Append("window.opener.loadAdmissionData('" + AdmKey + "','" + AdmCode + "','" + AdmDate + "', '" + fName + "','" + lName + "','" +
                    MobileNo + "','" + batchKey + "','" + admissionTypeKey + "','" + DOB + "','" + emailID + "','" + altPhNp + "','" +
                    gender + "','" + collegeName + "','" + AdmissionOwner + "','" + TotalFee + "','" + firstInstallment + "','" + secondInstallment + "','" + thirdInstallment
                    + "','" + fourthInstallment + "','" + address + "','" + remark + "', '"+ source + "', '"+ fifthInstallment + "', '"+sixthInstallment+"');");
                script.Append("window.close();");
                script.Append("</script>");

                if (Request.QueryString["ForPopup"] != null && Convert.ToBoolean(Request.QueryString["ForPopup"]))
                    Page.RegisterStartupScript("test", script.ToString());

                //RegisterWindowsCloseScript(script.ToString());
            }
        }

        private void LoadGroupList()
        {
            if (ddlSubCourse.SelectedValue != null && ddlSubCourse.SelectedValue.Trim() != "")
            {
                int iSelectedGroupInx = 0;
                if (ddlGroup.SelectedIndex != null && ddlGroup.SelectedItem != null)
                    iSelectedGroupInx = ddlGroup.SelectedIndex;

                BAL.Class.SmartInstitute.GroupSubCourseClass o_GetGroupSubject = new BAL.Class.SmartInstitute.GroupSubCourseClass();
                DataTable dtChildgrp = o_GetGroupSubject.GetGroupsListForSubCode(Convert.ToInt32(ddlSubCourse.SelectedValue), ref Message);

                ddlGroup.DataSource = dtChildgrp;
                ddlGroup.DataBind();
                //if (!IsPostBack)
                //if (dtChildgrp == null || dtChildgrp.Rows.Count <= 0)
                //{
                //    divGroupList.Visible = false;
                //    LoadSubjectData();
                //}
                //else
                //{
                //    divGroupList.Visible = true;
                //    ddlGroup.SelectedIndex = iSelectedGroupInx;
                //}
            }
        }
        protected void chkCourse_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCourse.Checked)
            {
                ddlCourse.Enabled = true;
                LoadSubCourseList();
                LoadGroupList();
                LoadSubjectData();
                LoadBatchList();
            }
            else ddlCourse.Enabled = false;
        }

        protected void chkSubCourse_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubCourse.Checked)
            {
                //if (chkCourse.Checked)
                ddlSubCourse.Enabled = true;
                LoadGroupList();
                LoadSubjectData();
                LoadBatchList();
            }
            else ddlSubCourse.Enabled = false;
        }

        protected void chkGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGroup.Checked)
            {
                ddlGroup.Enabled = true;
                LoadSubjectData();
                LoadBatchList();
            }
            else ddlGroup.Enabled = false;
        }

        protected void ddlSubCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkSubCourse.Checked && chkCourse.Checked)
            {
                LoadGroupList();
                LoadSubjectData();
                LoadBatchList();
            }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkCourse.Checked)
            {
                LoadSubCourseList();
                LoadGroupList();
                LoadSubjectData();
                LoadBatchList();
            }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkCourse.Checked && chkSubCourse.Checked && chkGroup.Checked)
            {
                LoadSubjectData();
                LoadBatchList();
            }
        }

        private void LoadSubjectData()
        {
            int iCourseKey = 0;
            int iSubCourseKey = 0;
            int iGroupKey = 0;

            if (ddlCourse.SelectedItem != null && ddlCourse.SelectedValue.Trim() != "")
                iCourseKey = Convert.ToInt32(ddlCourse.SelectedValue);
            if (ddlSubCourse.SelectedItem != null && ddlSubCourse.SelectedValue.Trim() != "")
                iSubCourseKey = Convert.ToInt32(ddlSubCourse.SelectedValue);
            if (ddlGroup.SelectedItem != null && ddlGroup.SelectedValue.Trim() != "")
               iGroupKey = Convert.ToInt32(ddlGroup.SelectedValue);

            if (ddlGroup.SelectedValue != null && ddlGroup.SelectedValue.Trim() != "")
            {
                BAL.Class.SmartInstitute.SubjectClass o_GetSubject = new BAL.Class.SmartInstitute.SubjectClass();
                DataTable dtChildSubc = o_GetSubject.GetSubjectListForGroup(iCourseKey, iSubCourseKey, iGroupKey, ref Message);
                ddlSubject.DataSource = dtChildSubc;
                ddlSubject.DataBind();

            }
            else if (ddlSubCourse.SelectedValue != null && ddlSubCourse.SelectedValue.Trim() != "")
            {
                BAL.Class.SmartInstitute.SubjectClass o_GetSubject = new BAL.Class.SmartInstitute.SubjectClass();
                DataTable dtChildSubc = o_GetSubject.GetSubjectListForSubCourse(iCourseKey, iSubCourseKey, ref Message);
                ddlSubject.DataSource = dtChildSubc;
                ddlSubject.DataBind();

            }
            //if (!IsPostBack)
            //    chkSubjectList.SelectedIndex = 0;
        }
        protected void chkSubject_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubject.Checked)
                ddlSubject.Enabled = true;
            else ddlSubject.Enabled = false;
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            lab_message.Text = "";
            GridView gvexport = new GridView();

            if (dtAdmission != null && dtAdmission.Rows.Count > 0)
            {
                for (int i = 0; i < dtAdmission.Columns.Count; i++)
                {
                    if (dtAdmission.Columns[i].Caption.ToLower().Contains("key"))
                        //if (gvexport.Columns[i].HeaderText.ToLower().Contains("key"))
                        dtAdmission.Columns.Remove(dtAdmission.Columns[i].Caption);
                }

                gvexport.DataSource = dtAdmission;
                gvexport.DataBind();


                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "AdmissionList.xls";
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
                lab_message.Text = "No records to export";
        }
    }
}