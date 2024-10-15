using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Admin
{
    public partial class ChangePasswordPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMessage.Text = "";
            if (!IsPostBack)
            {
                txtEmpID.Text = Session["LoginEmpID"].ToString();
            }
        }
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (txtEmpID.Text.Trim() == "")
            {
                lblErrorMessage.Text = "Please enter Employee ID of which you want to change password";
            }
            else if (txtOldPassword.Text.Trim() == "")
            {
                lblErrorMessage.Text = "Please enter old password";
            }
            else if (txtNewPassword.Text.Trim() == "")
            {
                lblErrorMessage.Text = "Please enter old password";
            }
            else if (txtConfirmPassword.Text.Trim() == "")
            {
                lblErrorMessage.Text = "Please enter old password";
            }
            else if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                lblErrorMessage.Text = "New Password and Confirm password does not match...";
            }
            else
            {
                string Message = string.Empty;
                lblErrorMessage.Text = "";
                BAL.Class.SmartHR.AddEmployeeClass o_LoginEmp = new BAL.Class.SmartHR.AddEmployeeClass();
                string sEncryptedPassword = txtOldPassword.Text;

                sEncryptedPassword = CCryptography.Encrypt(txtOldPassword.Text, ConfigurationManager.AppSettings["EncryptionKey"]);
                o_LoginEmp.EmployeeID = txtEmpID.Text;
                o_LoginEmp.EmpPassword = sEncryptedPassword;

                o_LoginEmp.ValidateLoginEmployee(o_LoginEmp, ref Message);

                if (o_LoginEmp.employeeKey > 0)
                {
                    sEncryptedPassword = CCryptography.Encrypt(txtNewPassword.Text, ConfigurationManager.AppSettings["EncryptionKey"]);
                    int retVal = o_LoginEmp.ChangePassword(o_LoginEmp.employeeKey, sEncryptedPassword,Convert.ToInt32(Session["LoginEmpKey"]), ref Message);
                    if (retVal > 0)
                        lblErrorMessage.Text = "Password Changed Successfully ";
                    else
                        lblErrorMessage.Text = Message;
                }
                else
                {
                    lblErrorMessage.Text = "Employee not exists with this Employee ID and Password";
                }

            }
        }
    }
}