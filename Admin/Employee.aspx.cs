using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace InstituteManagement.Admin
{
    public partial class Employee : System.Web.UI.Page
    {
        string Message = string.Empty;
        string Mode = "INSERT";
        static bool bEditRights = true;
        static bool bAddRights = true;
        static bool bDeleteRights = true;
        static DataTable dtEmployee;
        int iMenuKey = 7;
        protected void Page_Load(object sender, EventArgs e)
        {
            FillGridViewData();
            grdEmployee.DataBind();
            lblDepartment.Visible = false;
            txtDepartment.Visible = false;
            lblDesignation.Visible = false;
            txtDesignation.Visible = false;
            lblEmail.Visible = false;
            txtEmail.Visible = false;
            lblGender.Visible = false;
            rdoFemale.Visible = false;
            rdoMale.Visible = false;
            lblPhone.Visible = false;
            txtPhone.Visible = false;

            if (!IsPostBack)
            {
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
                                btn_Add.Enabled = true;
                                btn_SaveEmployee.Enabled = true;
                                bAddRights = true;
                            }
                            else
                            {
                                btn_Add.Enabled = false;
                                btn_SaveEmployee.Enabled = true;
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
            BAL.Class.SmartHR.AddEmployeeClass o_GetEmployee = new BAL.Class.SmartHR.AddEmployeeClass();
            DataTable dtBatch = o_GetEmployee.GetEmployeeList(ref Message);
            grdEmployee.DataSource = dtBatch;
            grdEmployee.DataBind();

            dtEmployee = dtBatch.Copy();
        }
        private bool ValidateData()
        {
            if (txtEmployeeName.Text == "")
                Lab_message.Text = "Please select Employee Name";
            else if (txtUserID.Text.Trim() == "")
                Lab_message.Text = "Employee ID is compulsary field";

            if (Lab_message.Text.Trim() == "")
                return true;
            return false;
        }
        protected void btn_SaveEmployee_Click(object sender, EventArgs e)
        {
            Lab_message.Text = "";
            if (ValidateData())
            {
                //string mode = "INSERT";           
                //string mode = "";
                if (txtEmployeeKeyForEditMode.Text != "" && txtEmployeeKeyForEditMode.Text != "0")
                    Mode = "UPDATE";
                else Mode = "INSERT";

                Message = string.Empty;
                BAL.Class.SmartHR.AddEmployeeClass o_SaveEmployee = new BAL.Class.SmartHR.AddEmployeeClass();

                if (o_SaveEmployee.CheckDuplicateEmployee(txtUserID.Text.Trim(), Mode, ref Message))
                {
                    Lab_message.Text = "This Employee Code already exists. Please enter another";
                    txtEmployeeName.Text = "";
                }
                else
                {
                    o_SaveEmployee.employeeName = txtEmployeeName.Text;
                    o_SaveEmployee.address = txtAddress.Text;

                    o_SaveEmployee.EmployeeID = txtUserID.Text;
                    //if (mode == "UPDATE")
                    string sEncryptedPassword = txtPassword.Text;

                    sEncryptedPassword = CCryptography.Encrypt(txtPassword.Text, ConfigurationManager.AppSettings["EncryptionKey"]);
                    o_SaveEmployee.EmpPassword = sEncryptedPassword;

                    o_SaveEmployee.createdBy = Convert.ToInt32(Session["LoginEmpKey"]);
                    o_SaveEmployee.modifiedBy = Convert.ToInt32(Session["LoginEmpKey"]);

                    if (Mode == "UPDATE")
                        o_SaveEmployee.employeeKey = Convert.ToInt32(txtEmployeeKeyForEditMode.Text);


                    int retval = o_SaveEmployee.save(ref Message, Mode);
                    if (retval > 0)
                    {
                        if (Mode == "UPDATE")
                            Lab_message.Text = "Employee updated successfully.";
                        else
                            Lab_message.Text = "New employee saved successfully.";
                        ClearControls();
                        Mode = "INSERT";
                        //lab_CreatedByText.Text = "#99";
                        //lab_CreatedOnText.Text = Convert.ToString(System.DateTime.Now);
                        //lab_ModifiedByText.Text = "#99";
                        //lab_ModifiedOnText.Text = Convert.ToString(System.DateTime.Now);
                        btnEdit.Enabled = false;
                        btn_Delete.Enabled = false;
                        btn_SaveEmployee.Enabled = true;
                        txtPassword.Enabled = true;
                    }
                    else
                    {
                        Lab_message.Text = Convert.ToString(Message);
                    }
                }
            }
            FillGridViewData();

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
            if (bEditRights)
            {
                if (grdEmployee.SelectedRow != null)
                {
                    Lab_message.Text = "";
                    Mode = "UPDATE";
                    grdEmployee.SelectedRow.Focus();
                    txtEmployeeKeyForEditMode.Text = grdEmployee.SelectedRow.Cells[1].Text;
                    txtUserID.Text = Server.HtmlDecode(grdEmployee.SelectedRow.Cells[2].Text);
                    txtEmployeeName.Text = Server.HtmlDecode(grdEmployee.SelectedRow.Cells[3].Text);
                    txtAddress.Text = Server.HtmlDecode(grdEmployee.SelectedRow.Cells[5].Text);
                    Session["EmpPassword"] = grdEmployee.SelectedRow.Cells[4].Text;
                    txtPassword.Enabled = false;
                    btnEdit.Enabled = true;
                    btn_Delete.Enabled = true;
                    btn_SaveEmployee.Enabled = false;
                }
            }
            else
            {
                Lab_message.Text = "You are not having edit rights";
            }
            //txtPassword.Text= grdEmployee.SelectedRow.Cells[4].Text;
        }
        private void ClearControls()
        {
            txtEmployeeName.Text = "";
            txtAddress.Text = "";
            txtUserID.Text = "";
            txtPassword.Text = "";
            txtEmployeeKeyForEditMode.Text = "";
        }
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            Lab_message.Text = "";
            ClearControls();
            txtPassword.Enabled = true;
            Mode = "INSERT";
            btnEdit.Enabled = false;
            btn_Delete.Enabled = false;
            if (bAddRights)
                btn_SaveEmployee.Enabled = true;
        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            if (bDeleteRights)
            {
                Lab_message.Text = "";
                BAL.Class.SmartHR.AddEmployeeClass o_DeleteEmp = new BAL.Class.SmartHR.AddEmployeeClass();
                o_DeleteEmp.employeeKey = Convert.ToInt32(txtEmployeeKeyForEditMode.Text);
                int retval = o_DeleteEmp.save(ref Message, "DELETE");
                FillGridViewData();
                ClearControls();
                btnEdit.Enabled = false;
                btn_Delete.Enabled = false;
                btn_SaveEmployee.Enabled = true;
            }
            else
            {
                Lab_message.Text = "You are not allowed to delete record. Please contact administrator.";
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            btn_SaveEmployee_Click(sender, e);
        }
    }
}