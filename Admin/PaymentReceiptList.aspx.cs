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
    public partial class PaymentReceiptList : System.Web.UI.Page
    {
        string Message = string.Empty;
        static DataTable dtBatchWiseFeeDetails;
        int iMenuKey = 18;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadCouseList();
                LoadSubCourseList();
                LoadGroupList();
                LoadBatchList();
                ApplyLoginEmpRights();
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
            if (rdoListCourse.Items.Count > 0)
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
                    if (rdoGroupList.Items.Count >= iSelectedGroupInx)
                        rdoGroupList.SelectedIndex = iSelectedGroupInx;
                }
            }
        }
        private void LoadSubCourseList()
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
        private void LoadBatchList()
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
        protected void rdoGroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadSubjectData();
            LoadBatchList();
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

            LoadGroupList();
            LoadBatchList();
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
            LoadBatchList();
        }

        protected void btnCalCulateRatio_Click(object sender, EventArgs e)
        {
            grdSubjectList.Visible = true;
            grdStudentWiseFragment.Visible = false;
            lab_message.Text = "";
            int iCoursekey = 0;
            int iSubCourseKey = 0;
            int iGroupKey = 0;

            if (rdoListCourse.SelectedItem != null)
                iCoursekey = Convert.ToInt32(rdoListCourse.SelectedValue);
            if (rdoSubCourseList.SelectedItem != null)
                iSubCourseKey = Convert.ToInt32(rdoSubCourseList.SelectedValue);
            if (rdoGroupList.SelectedItem != null && divGroupList.Visible)
                iGroupKey = Convert.ToInt32(rdoGroupList.SelectedValue);

            if (ddlBatchList.SelectedIndex != null && ddlBatchList.SelectedValue.Trim() != "")
            {
                BAL.Class.SmartInstitute.AdmissionDetails o_GetSubjectList = new BAL.Class.SmartInstitute.AdmissionDetails();
                DataTable dtSubj = o_GetSubjectList.GetSubjectListBatchWise(Convert.ToInt32(ddlBatchList.SelectedValue), ref Message);                

                decimal TotalReceivedFees = 0;
                if (dtSubj != null && dtSubj.Rows.Count > 0)
                {
                    dtSubj.Columns.Add("FeeByfargation", typeof(decimal));
                    dtSubj.Columns["FeeByfargation"].ReadOnly = false;
                    decimal TotSubjectFees = dtSubj.AsEnumerable().Sum(row => row.Field<decimal>("subjectFees"));
                    

                    BAL.Class.SmartInstitute.PaymentReceiptClass o_GetRecvFee = new BAL.Class.SmartInstitute.PaymentReceiptClass();
                    TotalReceivedFees = o_GetRecvFee.GetFeePaidBatchWise(Convert.ToInt32(ddlBatchList.SelectedValue), ref Message);
                    if (TotalReceivedFees > 0)
                    {
                        foreach(DataRow row in dtSubj.Rows)
                        {
                            decimal CalcRatio = TotalReceivedFees / TotSubjectFees * Convert.ToDecimal(row["subjectFees"]);
                            row["FeeByfargation"] = Convert.ToString(Math.Round(CalcRatio, 2));
                        }
                        grdSubjectList.DataSource = dtSubj;
                        grdSubjectList.DataBind();

                        grdSubjectList.FooterRow.Cells[0].Text = "Total";
                        grdSubjectList.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                        grdSubjectList.FooterRow.Cells[1].Text = TotSubjectFees.ToString("N2");
                        decimal TotFeesRatio = dtSubj.AsEnumerable().Sum(row => row.Field<decimal>("FeeByfargation"));
                        grdSubjectList.FooterRow.Cells[2].Text = TotFeesRatio.ToString("N2");

                        dtBatchWiseFeeDetails = dtSubj.Copy();
                    }
                    else
                    {
                        lab_message.Text = "No fees received against this batch";
                        grdSubjectList.DataSource = null;
                        grdSubjectList.DataBind();
                        dtBatchWiseFeeDetails = null;
                    }
                }
            }
        }

        protected void btnExportStudentWise_Click(object sender, EventArgs e)
        {
            grdStudentWiseFragment.Visible = true;
            grdSubjectList.Visible = false;

            lab_message.Text = "";
            int iCoursekey = 0;
            int iSubCourseKey = 0;
            int iGroupKey = 0;

            if (rdoListCourse.SelectedItem != null)
                iCoursekey = Convert.ToInt32(rdoListCourse.SelectedValue);
            if (rdoSubCourseList.SelectedItem != null)
                iSubCourseKey = Convert.ToInt32(rdoSubCourseList.SelectedValue);
            if (rdoGroupList.SelectedItem != null && divGroupList.Visible)
                iGroupKey = Convert.ToInt32(rdoGroupList.SelectedValue);

            if (ddlBatchList.SelectedIndex != null && ddlBatchList.SelectedValue.Trim() != "")
            {
                BAL.Class.SmartInstitute.AdmissionDetails o_GetSubjectList = new BAL.Class.SmartInstitute.AdmissionDetails();
                DataTable dtAdmission = o_GetSubjectList.GetStudentAdmissionBatchWise(Convert.ToInt32(ddlBatchList.SelectedValue), ref Message);
                grdStudentWiseFragment.DataSource = dtAdmission;
                grdStudentWiseFragment.DataBind();

                if (dtAdmission != null && dtAdmission.Rows.Count > 0)
                    dtBatchWiseFeeDetails = dtAdmission.Copy();
                else dtBatchWiseFeeDetails = null;

            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            lab_message.Text = "";
            GridView gvexport = new GridView();

            if (dtBatchWiseFeeDetails != null && dtBatchWiseFeeDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dtBatchWiseFeeDetails.Columns.Count; i++)
                {
                    if (dtBatchWiseFeeDetails.Columns[i].Caption.ToLower().Contains("key"))
                        //if (gvexport.Columns[i].HeaderText.ToLower().Contains("key"))
                        dtBatchWiseFeeDetails.Columns.Remove(dtBatchWiseFeeDetails.Columns[i].Caption);
                }

                gvexport.DataSource = dtBatchWiseFeeDetails;
                gvexport.DataBind();


                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "BatchWisePaidFeesDetails.xls";
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

        protected void grdStudentWiseFragment_Sorting(object sender, GridViewSortEventArgs e)
        {
            dtBatchWiseFeeDetails.DefaultView.Sort = e.SortExpression;
            grdStudentWiseFragment.DataSource = dtBatchWiseFeeDetails;
            grdStudentWiseFragment.DataBind();
        }
    }
}