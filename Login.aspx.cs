using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                //if (txtUserID.Text.Trim() == "#99" && txtPassword.Text.Trim() == "99")
                //{
                //    Session["LoginEmpID"] = "#99";
                //    Session["LoginEmpName"] = "Admin";
                //    Session["LoginEmpKey"] = 0;
                //    Response.Redirect("~/Admin/WellcomePage.aspx");
                //}
                //else
                //{
                    string Message = string.Empty;
                    string sEncryptedPassword = txtPassword.Text;

                    sEncryptedPassword = CCryptography.Encrypt(txtPassword.Text, ConfigurationManager.AppSettings["EncryptionKey"]);
                    string sEmail = txtUserID.Text;
                    BAL.Class.SmartHR.AddEmployeeClass o_LoginEmp = new BAL.Class.SmartHR.AddEmployeeClass();
                    o_LoginEmp.EmployeeID = txtUserID.Text;
                    o_LoginEmp.EmpPassword = sEncryptedPassword;

                    o_LoginEmp.ValidateLoginEmployee(o_LoginEmp, ref Message);
                lblErrorMessage.Text = Message;
                    if (o_LoginEmp.employeeKey > 0)
                    {
                        Session["LoginEmpID"] = o_LoginEmp.EmployeeID;
                        Session["LoginEmpName"] = o_LoginEmp.employeeName;
                        Session["LoginEmpKey"] = o_LoginEmp.employeeKey;
                        Response.Redirect("~/Admin/WellcomePage.aspx");
                    }
                   /* else
                    {
                        lblErrorMessage.Text = "Invalid User Name/Password";
                    }*/
                //}
            }
            else
            {
                lblErrorMessage.Text = "User Name or Password ";
            }
        }
    }
}