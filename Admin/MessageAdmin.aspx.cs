using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Admin
{
    public partial class MessageAdmin : System.Web.UI.Page
    {
        string Message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (DDLMessage.SelectedValue == "Select" || DDLMessage.SelectedValue == "")
            {
                Lab_message.Text = "Please select Message Type";
                return;
            }         
            else if (txt_Message.Text == "")
            {
                Lab_message.Text = "Please enter the message";
                return;
            }
            else
            {
                string mode = "INSERT";
                // Save Course Master DATA Insert into m_course table//         

                Message = string.Empty;
                BAL.Class.SmartInstitute.MessageClass o_SaveMessage = new BAL.Class.SmartInstitute.MessageClass();

                o_SaveMessage.messageType = Convert.ToString(DDLMessage.SelectedValue);
                o_SaveMessage.message = Convert.ToString(txt_Message.Text);
                // o_SaveNotesInward.createdBy = 99;
                //o_SaveNotesInward.createdOn = System.DateTime.Now;
                //o_SaveNotesInward.modifiedBy = 0;
                //o_SaveNotesInward.modifiedOn = Convert.ToString(System.DateTime.Now);


                int retval = o_SaveMessage.save(ref Message, mode);
                if (retval > 0)
                {
                    Lab_message.Text = "New message saved successfully.";
                    lab_CreatedByText.Text = "#99";
                    lab_CreatedOnText.Text = Convert.ToString(System.DateTime.Now);
                    lab_ModifiedByText.Text = "#99";
                    lab_ModifiedOnText.Text = Convert.ToString(System.DateTime.Now);

                }
                else
                {
                    Lab_message.Text = Convert.ToString(Message);
                }
            }

        }
    }
}