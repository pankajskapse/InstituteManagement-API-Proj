using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Admin
{
    public partial class EmployeeRights : System.Web.UI.Page
    {
        string Message = string.Empty;
        static DataTable dtRights;
        static DataTable dtEmployee;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                FillEmployeeGrid();
                FillRightsGrid();
            }
        }

        protected void grdEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (grdEmployee.DataSource == null)
                grdEmployee.DataSource = dtEmployee;
            grdEmployee.PageIndex = e.NewPageIndex;
            grdEmployee.DataBind();
        }

        protected void grdEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iEmpKey = 0;
            if (grdEmployee.SelectedRow != null && Server.HtmlDecode(grdEmployee.SelectedRow.Cells[1].Text).Trim() != "")
                iEmpKey = Convert.ToInt32(grdEmployee.SelectedRow.Cells[1].Text);

            BAL.Class.SmartInstitute.EmprightsClass o_GetEmployee = new BAL.Class.SmartInstitute.EmprightsClass();
            if (o_GetEmployee.CheckEmpRightsExists(iEmpKey, ref Message))
                FillRightsGridForEmp(iEmpKey);
            else FillRightsGrid();
        }
        private void FillRightsGrid()
        {
            int iEmpKey = 0;
            if (grdEmployee.SelectedRow != null && Server.HtmlDecode(grdEmployee.SelectedRow.Cells[1].Text).Trim()!="")
                iEmpKey = Convert.ToInt32(grdEmployee.SelectedRow.Cells[1].Text);
            BAL.Class.SmartInstitute.EmprightsClass o_GetEmployee = new BAL.Class.SmartInstitute.EmprightsClass();
            DataTable dtBatch = o_GetEmployee.GetMenuRightsList(ref Message);
            grdRights.DataSource = dtBatch;
            grdRights.DataBind();
            dtRights = dtBatch.Copy();
        }
        private void FillRightsGridForEmp(int iEmpKey)
        {
            BAL.Class.SmartInstitute.EmprightsClass o_GetEmployee = new BAL.Class.SmartInstitute.EmprightsClass();
            DataTable dtBatch = o_GetEmployee.GetMenuRightsListForEmp(iEmpKey,ref Message);
            grdRights.DataSource = dtBatch;
            grdRights.DataBind();

            dtRights = dtBatch.Copy();
        }
        private void FillEmployeeGrid()
        {
            BAL.Class.SmartHR.AddEmployeeClass o_GetEmployee = new BAL.Class.SmartHR.AddEmployeeClass();
            DataTable dtBatch = o_GetEmployee.GetEmployeeList(ref Message);
            grdEmployee.DataSource = dtBatch;
            grdEmployee.DataBind();
            dtEmployee = dtBatch.Copy();
            //grdEmployee.SelectedIndex = 0;

        }
        protected void grdRights_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (grdRights.DataSource == null)
                grdRights.DataSource = dtRights;
            grdRights.PageIndex = e.NewPageIndex;
            grdRights.DataBind();
        }

        protected void btn_SaveRights_Click(object sender, EventArgs e)
        {
            bool bView = false;
            bool bAdd = false;
            bool bEdit = false;
            bool bDelete = false;
            int iMenuKey = 0;

            if(grdEmployee.SelectedRow!=null)
            {
                BAL.Class.SmartInstitute.EmprightsClass empRights = new BAL.Class.SmartInstitute.EmprightsClass();
                if (grdEmployee.SelectedRow.Cells[1].Text != "")
                    empRights.employeekey = Convert.ToInt32(grdEmployee.SelectedRow.Cells[1].Text);

                
                string mode = "INSERT";
                empRights.createdBy = Convert.ToInt32(Session["LoginEmpKey"]);
                empRights.modifiedBy= Convert.ToInt32(Session["LoginEmpKey"]);

                
                    foreach (GridViewRow row in grdRights.Rows)
                    {                       
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            empRights.menukey = Convert.ToInt32(row.Cells[1].Text);
                            CheckBox chkRow = (row.Cells[0].FindControl("chkView") as CheckBox);
                            if (chkRow.Checked)
                                empRights.erview = true;
                            else empRights.erview = false;

                            chkRow = (row.Cells[0].FindControl("chkAdd") as CheckBox);
                            if (chkRow.Checked)
                                empRights.erAdd = true;
                            else empRights.erAdd = false;

                            chkRow = (row.Cells[0].FindControl("chkEdit") as CheckBox);
                            if (chkRow.Checked)
                                empRights.eredit = true;
                            else empRights.eredit = false;

                            chkRow = (row.Cells[0].FindControl("chkDelete") as CheckBox);
                            if (chkRow.Checked)
                                empRights.erdelete = true;
                            else empRights.erdelete = false;

                            if (Server.HtmlDecode(row.Cells[0].Text).Trim() != "" && Server.HtmlDecode(row.Cells[0].Text).Trim() != "0")
                            {
                                empRights.emprightsKey = Convert.ToInt32(row.Cells[0].Text);
                                empRights.save(ref Message, "UPDATE");
                            }
                            else empRights.save(ref Message, "INSERT");

                        }

                    }
                
                FillRightsGridForEmp(empRights.employeekey);
            }
            else
            {
                lab_message.Text = "Please select Employee to which you are applying rights";
            }
        }
        private void CheckViewRights(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int index = row.RowIndex;
            CheckBox chkAddrow = (CheckBox)grdRights.Rows[index].FindControl("chkAdd");
            CheckBox chkEditrow = (CheckBox)grdRights.Rows[index].FindControl("chkEdit");
            CheckBox chkDeleterow = (CheckBox)grdRights.Rows[index].FindControl("chkDelete");
            CheckBox chkViewrow = (CheckBox)grdRights.Rows[index].FindControl("chkView");
            //string yourvalue = chkAdd.Text;

            if (chkAddrow.Checked || chkEditrow.Checked || chkDeleterow.Checked)
                chkViewrow.Checked = true;
           
            else chkViewrow.Checked = false;

        }
        protected void chkAdd_CheckedChanged(object sender, EventArgs e)
        {
            CheckViewRights(sender, e);
            //string yourvalue = chkView.Text;
        }

        protected void chkEdit_CheckedChanged(object sender, EventArgs e)
        {
            CheckViewRights(sender, e);
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckViewRights(sender, e);
        }
    }
}