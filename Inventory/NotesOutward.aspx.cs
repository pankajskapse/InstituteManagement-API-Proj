using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Inventory
{ 
    public partial class NotesOutward : System.Web.UI.Page
    {
        string Message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (CourseDDL.SelectedValue == "Select" || CourseDDL.SelectedValue == "")
            {
                Lab_message.Text = "Please select Course";
                return;
            }
            else if (SubCourseDDL.SelectedValue == "Select" || SubCourseDDL.SelectedValue == "")
            {
                Lab_message.Text = "Please select Sub-Course";
                return;
            }
            else if (GroupDDL.SelectedValue == "Select" || GroupDDL.SelectedValue == "")
            {
                Lab_message.Text = "Please select group";
                return;
            }
            else if (SubjectDDL.SelectedValue == "Select" || SubjectDDL.SelectedValue == "")
            {
                Lab_message.Text = "Please select subject";
                return;
            }
            else if (BatchDDL.SelectedValue == "Select" || BatchDDL.SelectedValue == "")
            {
                Lab_message.Text = "Please select Batch";
                return;
            }
            else if (TextNotesOutQty.Text == "")
            {
                Lab_message.Text = "Please select Quentity";
                return;
            }
            else
            {
                string mode = "INSERT";
                // Save Course Master DATA Insert into m_course table//         

                Message = string.Empty;
                BAL.Class.Inventory.NotesOutwardClass o_SaveNotesOutward = new BAL.Class.Inventory.NotesOutwardClass();

                int DefaultKey = 99; //Temp
                o_SaveNotesOutward.notesoutwardKey = DefaultKey;
                o_SaveNotesOutward.batchKey = DefaultKey;
                o_SaveNotesOutward.subjectKey = DefaultKey;
                o_SaveNotesOutward.groupsubcourseKey = DefaultKey;
                o_SaveNotesOutward.SubCourseKey = DefaultKey;
                o_SaveNotesOutward.CourseKey = DefaultKey;
                o_SaveNotesOutward.notesoutwardQty = Convert.ToInt32(TextNotesOutQty.Text);
                o_SaveNotesOutward.remarks = Convert.ToString(TextRemarks.Text);
                // o_SaveNotesInward.createdBy = 99;
                //o_SaveNotesInward.createdOn = System.DateTime.Now;
                //o_SaveNotesInward.modifiedBy = 0;
                //o_SaveNotesInward.modifiedOn = Convert.ToString(System.DateTime.Now);


                int retval = o_SaveNotesOutward.save(ref Message, mode);
                if (retval > 0)
                {
                    Lab_message.Text = "Notes in Quentity saved successfully.";
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
        protected void btn_Back_Click(object sender, EventArgs e)
        {

        }

        protected void CourseDDL_Load(object sender, EventArgs e)
        {

        }

        protected void SubCourseDDL_Load(object sender, EventArgs e)
        {

        }

        protected void GroupDDL_Load(object sender, EventArgs e)
        {

        }
    }
}