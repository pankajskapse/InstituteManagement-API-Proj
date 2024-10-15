using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Admin
{
    public partial class EnquiryListNew : System.Web.UI.Page
    {
        string Message = string.Empty;
        static DataTable dtAdmission;
        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Request.QueryString["ForPopup"] == null && !Convert.ToBoolean(Request.QueryString["ForPopup"]))
            {
                this.MasterPageFile = "~/MasterPage/Standard.master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lab_message.Text = "";
            //btnSearch_Click(null, null);
            if (!IsPostBack)
            {
                FillEnquiryGrid();
                if (Request.QueryString["ForPopup"] != null && Convert.ToBoolean(Request.QueryString["ForPopup"]))
                {
                    chkClosed.Visible = false;
                }
                LoadBatchList();
                LoadCouseList();
                chkCourse_CheckedChanged(null, null);
                chkSubCourse_CheckedChanged(null, null);
                chkGroup_CheckedChanged(null, null);
                chkSubject_CheckedChanged(null, null);
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
        private void FillEnquiryGrid()
        {
            BAL.Class.SmartInstitute.EnquiryClass o_GetEnq = new BAL.Class.SmartInstitute.EnquiryClass();
            dtAdmission = o_GetEnq.GetEnquiryList(ref Message);
            grdEnquiryList.DataSource = dtAdmission;
            grdEnquiryList.DataBind();

            if (dtAdmission != null && dtAdmission.Rows.Count > 0)
                lblRecordCount.Text = dtAdmission.Rows.Count.ToString();
            else lblRecordCount.Text = "0";

            if (!IsPostBack)
            {
                SetFromToPanelState();
                SetBatchDropDownState();
               
            }
        }
        protected void RegisterWindowsCloseScript(string script)
        {
            Response.ClearContent();
            Response.ContentEncoding = Encoding.UTF8;
            Response.ContentType = "text/html";
            Response.Write(script);
            Response.End();
            Response.Close();
        }
        protected void grdEnquiryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (grdEnquiryList.DataSource == null)
                grdEnquiryList.DataSource = dtAdmission;
            grdEnquiryList.PageIndex = e.NewPageIndex;
            grdEnquiryList.DataBind();
        }

        protected void grdEnquiryList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Contains("Select"))
            {
                //sDesc = ((Label)(grdEnquiryList.Cells[12].Controls[1])).Text;//+ " " + ((Label)(grdRow.Cells[4].Controls[1])).Text + " " + ((Label)(grdRow.Cells[13].Controls[1])).Text;
                //sLocation = ((Label)(grdEnquiryList.Cells[7].Controls[1])).Text;
                //sFloorNo = ((Label)(grdEnquiryList.Cells[8].Controls[1])).Text;
                //sRoomNo = ((Label)(grdEnquiryList.Cells[9].Controls[1])).Text;
            }
        }
        private void SetFromToPanelState()
        {
            if (chkFromToDateFilter.Checked)
            {
                pnlFromToDate.Enabled = true;
                if(txtFromDate.Text=="")
                    txtFromDate.Text= DateTime.Now.ToString("yyyy-MM-dd");
                if(txtToDate.Text=="")
                    txtToDate.Text= DateTime.Now.ToString("yyyy-MM-dd");
            }
            else pnlFromToDate.Enabled = false;
        }
        protected void grdEnquiryList_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void grdEquip_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdEquip_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdEquip_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void grdEnquiryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdEnquiryList.SelectedRow != null)
            {
                string EnqKey = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[1].Text);
                string EnqID = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[2].Text);
                string fName = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[3].Text);
                string lName = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[4].Text);
                string MobileNo = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[5].Text);
                string emailID = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[7].Text);
                //int gender = Convert.ToInt32(Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[8].Text));
                string gender = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[8].Text);
                string collegeName = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[9].Text);
                string enquiryTypeKey = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[12].Text);
                string batchKey = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[13].Text);
                string EnqRemark = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[14].Text);

                string EstimatedFee = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[15].Text);
                string FinalFees = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[16].Text);

                string referByName = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[20].Text);
                string referByPhoneNo = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[21].Text);
                string altPhoneNo = Server.HtmlDecode(grdEnquiryList.SelectedRow.Cells[22].Text);

                CultureInfo provider = CultureInfo.InvariantCulture;
                DateTime EnqDate;
                string sEnqDate = "";
                if (grdEnquiryList.SelectedRow.Cells[6].Text != null)
                {
                    EnqDate = Convert.ToDateTime(grdEnquiryList.SelectedRow.Cells[6].Text);
                    sEnqDate = EnqDate.ToString("yyyy-MM-dd");
                    //DateTime.TryParseExact(grdEnquiryList.SelectedRow.Cells[6].Text, "MM/dd/yyyy", provider, DateTimeStyles.None, out EnqDate);

                }
                else
                {
                    EnqDate = DateTime.ParseExact(System.DateTime.Now.ToString(), "yyyy-MM-dd", provider);
                    sEnqDate= EnqDate.ToString("yyyy-MM-dd");
                }
                StringBuilder script = new StringBuilder();
                script.Append("<script type='text/javascript'>");
                //script.Append("window.opener.loadEnquiryDataOne('" + EnqID +"');");
                script.Append("window.opener.loadEnquiryData('" + EnqKey + "','" + EnqID + "','" + fName + "','" + lName + "','" + emailID + "','" + MobileNo + "','" + gender +
                    "','" + collegeName + "','" + enquiryTypeKey + "','" + batchKey + "', '" + sEnqDate + "', '" + EnqRemark + "','" + EstimatedFee + "','" + FinalFees +
                    "', '" + referByName + "','" + referByPhoneNo + "','" + altPhoneNo + "');");
                script.Append("window.close();");
                script.Append("</script>");

                Page.RegisterStartupScript("test", script.ToString());

                //RegisterWindowsCloseScript(script.ToString());
            }
        }

        protected void chkBatchWise_CheckedChanged(object sender, EventArgs e)
        {
            SetBatchDropDownState();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string whereCondition = "";
            if (chkFromToDateFilter.Checked)
                whereCondition = "where enquiryDate BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "'";

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
                        whereCondition = " where enquiryKey in (select enquiryKey from d_enquiryDetails where ";

                if (ddlCourse.SelectedValue != "" && ddlCourse.SelectedItem != null)
                    whereCondition = whereCondition + " courseKey=" + ddlCourse.SelectedValue;
            }
            if (chkSubCourse.Checked)
            {
                if (ddlSubCourse.SelectedValue != "" && ddlSubCourse.SelectedItem != null)
                    if (whereCondition.Trim() != "")
                        whereCondition = whereCondition + " and ";
                    else
                        whereCondition = " where enquiryKey in (select enquiryKey from d_enquiryDetails where";

                if (ddlSubCourse.SelectedValue != "" && ddlSubCourse.SelectedItem != null)
                    whereCondition = whereCondition + " subcourseKey=" + ddlSubCourse.SelectedValue;
            }
            if (chkGroup.Checked)
            {
                if (ddlGroup.SelectedValue != "" && ddlGroup.SelectedItem != null)
                    if (whereCondition.Trim() != "")
                        whereCondition = whereCondition + " and ";
                    else
                        whereCondition = " where enquiryKey in (select enquiryKey from d_enquiryDetails where";

                if (ddlGroup.SelectedValue != "" && ddlGroup.SelectedItem != null)
                    whereCondition = whereCondition + " groupKey=" + ddlGroup.SelectedValue;
            }
            if (whereCondition.Contains("(") && !chkSubject.Checked)
                whereCondition = whereCondition + " )";

            if (chkSubject.Checked)
            {
                if (ddlSubject.SelectedValue != "" && ddlSubject.SelectedItem != null)
                {
                    if (whereCondition.Trim() != "" && whereCondition.Contains("d_enquiryDetails"))
                        whereCondition = whereCondition + " and subjectKey=" + ddlSubject.SelectedValue + ")";
                    else
                        whereCondition = whereCondition + " and enquiryKey in (select enquiryKey from d_enquiryDetails where subjectKey=" + ddlSubject.SelectedValue + ")";
                }
                else
                    whereCondition = " where enquiryKey in (select enquiryKey from d_enquiryDetails where subjectKey=" + ddlSubject.SelectedValue + ")";

                //if (ddlSubject.SelectedValue != "" && ddlSubject.SelectedItem != null)
                //    whereCondition = whereCondition + " subjectKey=" + ddlSubject.SelectedValue;
            }



            if (!chkClosed.Checked)
            {
                if (whereCondition.Trim() == "")
                    whereCondition = " where ";
                else whereCondition += " and ";

                whereCondition += " ISNULL(Closed,0)=0";
            }

            BAL.Class.SmartInstitute.EnquiryClass o_GetEnq = new BAL.Class.SmartInstitute.EnquiryClass();
           DataSet dsAdmission = o_GetEnq.GetEnquiryListWithFilters(whereCondition, ref Message);

            grdEnquiryList.DataSource = dsAdmission;
            grdEnquiryList.DataBind();


            if (dsAdmission != null && dsAdmission.Tables != null && dsAdmission.Tables[0].Rows.Count > 0)
            {
                dtAdmission = dsAdmission.Tables[0].Copy();
                lblRecordCount.Text = dsAdmission.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lblRecordCount.Text = "0";
                dtAdmission = null;
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
        private void SetBatchDropDownState()
        {
            if (chkBatchWise.Checked)
                ddlBatchList.Enabled = true;
            else ddlBatchList.Enabled = false;
        }
        protected void chkFromToDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            SetFromToPanelState();
        }

        protected void chkCourse_CheckedChanged(object sender, EventArgs e)
        {
            lab_message.Text = "";
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
            lab_message.Text = "";
            if (chkSubCourse.Checked)
            {
                //if (chkCourse.Checked)
                    ddlSubCourse.Enabled = true;
                LoadGroupList();
                LoadSubjectData();
                LoadBatchList();
                //else
                //    lab_message.Text = "Please select course first";
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
        public override void VerifyRenderingInServerForm(Control control)
        {

        }


        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
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
                string FileName = "EnquiryList.xls";
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




            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=EnquiryList.xls");
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //using (StringWriter sw = new StringWriter())
            //{
            //    HtmlTextWriter hw = new HtmlTextWriter(sw);


            //    //grdEnquiryList.HeaderRow.BackColor = Color.White;
            //    foreach (TableCell cell in grdEnquiryList.HeaderRow.Cells)
            //    {
            //        cell.BackColor = grdEnquiryList.HeaderStyle.BackColor;
            //    }
            //    foreach (GridViewRow row in grdEnquiryList.Rows)
            //    {
            //        //row.BackColor = Color.White;
            //        foreach (TableCell cell in row.Cells)
            //        {
            //            if (row.RowIndex % 2 == 0)
            //            {
            //                cell.BackColor = grdEnquiryList.AlternatingRowStyle.BackColor;
            //            }
            //            else
            //            {
            //                cell.BackColor = grdEnquiryList.RowStyle.BackColor;
            //            }
            //            cell.CssClass = "textmode";
            //        }
            //    }

            //    grdEnquiryList.RenderControl(hw);

            //    //style to format numbers to string
            //    string style = @"<style> .textmode { } </style>";
            //    Response.Write(style);
            //    Response.Output.Write(sw.ToString());
            //    Response.Flush();
            //    Response.End();
            //}
        }
    }
}