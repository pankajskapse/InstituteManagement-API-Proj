using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Admin
{
    public partial class Enquiry_Form : System.Web.UI.Page
    {
        string Message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
                

        protected void btnSendOTP_Click(object sender, EventArgs e)
        {
            labOTP.Text = "2222";
        }

        protected void btnVerifyOTP_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(labOTP.Text) == Convert.ToString(txtOTP.Text))
            {
                labOTPVeriMessage.Text = "OTP verified successfully";               
            }
            else
            {
                labOTPVeriMessage.Text = "Inalid OTP";
            }
        }

        protected void btn_EnquirySave_Click(object sender, EventArgs e)
        {
            
        }

        protected void btn_Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/WellcomePage.aspx");
        }
    }
}