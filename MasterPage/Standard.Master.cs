using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.MasterPage
{
    public partial class Standard : System.Web.UI.MasterPage
    {
        string Message = string.Empty;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["LoginEmpName"] == null || Session["LoginEmpName"].ToString() == "")
            {
                Response.Redirect("../Login.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginEmpName"] != null)
            {
                lblEmpName.Text = Session["LoginEmpName"].ToString();
                LoadRightsForMenuVisibility();
            }
            else
                Response.Redirect("../Login.aspx");
        }

        private void LoadRightsForMenuVisibility()
        {
            if (Session["LoginEmpID"].ToString() != "#99")
            {
                BAL.Class.SmartInstitute.EmprightsClass o_GetEmployee = new BAL.Class.SmartInstitute.EmprightsClass();
                DataTable dtRights = o_GetEmployee.GetMenuRightsListForEmp(Convert.ToInt32(Session["LoginEmpKey"]), ref Message);
                if (dtRights != null && dtRights.Rows.Count > 0)
                {
                    int i = 1;
                    foreach (DataRow row in dtRights.Rows)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuAdmissionList.Visible = true;
                            else mnuAdmissionList.Visible = false;
                        }
                        else if (i == 2)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuAdmission.Visible = true;
                            else mnuAdmission.Visible = false;
                        }
                        else if (i == 3)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuBatch.Visible = true;
                            else mnuBatch.Visible = false;
                        }
                        else if (i == 4)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuChangePassword.Visible = true;
                            else mnuChangePassword.Visible = false;
                        }
                        else if (i == 5)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuCourse.Visible = true;
                            else mnuCourse.Visible = false;
                        }
                        else if (i == 6)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuSubject.Visible = true;
                            else mnuSubject.Visible = false;
                        }
                        else if (i == 7)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuEmployee.Visible = true;
                            else mnuEmployee.Visible = false;
                        }

                        else if (i == 8)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuEnquiryList.Visible = true;
                            else mnuEnquiryList.Visible = false;
                        }
                        else if (i == 9)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuEnquiry.Visible = true;
                            else mnuEnquiry.Visible = false;
                        }
                        else if (i == 10)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuFaculty.Visible = true;
                            else mnuFaculty.Visible = false;
                        }
                        else if (i == 11)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuFollowUp.Visible = true;
                            else mnuFollowUp.Visible = false;
                        }
                        else if (i == 12)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuGroupSubCourse.Visible = true;
                            else mnuGroupSubCourse.Visible = false;
                        }
                        else if (i == 13)
                        {
                            //if (Convert.ToBoolean(row["erview"]))
                            //    mnuMessageAdmin.Visible = true;
                            //else mnuMessageAdmin.Visible = false;
                        }
                        else if (i == 14)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuSubCourse.Visible = true;
                            else mnuSubCourse.Visible = false;
                        }
                        else if (i == 15)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuEmployeeRights.Visible = true;
                            else mnuEmployeeRights.Visible = false;
                        }
                        else if (i == 16)
                        {
                            //if (Convert.ToBoolean(row["erview"]))
                            //    mnuNotesInward.Visible = true;
                            //else mnuNotesInward.Visible = false;
                        }
                        else if (i == 17)
                        {
                            //if (Convert.ToBoolean(row["erview"]))
                            //    mnuNotesOutward.Visible = true;
                            //else mnuNotesOutward.Visible = false;
                        }
                        //else if (i == 15)
                        //{
                        //    if (Convert.ToBoolean(row["erview"]))
                        //        mnuSubCourse.Visible = true;
                        //    else mnuSubCourse.Visible = false;
                        //}
                        else if (i == 18)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuFeeByFargation.Visible = true;
                            else mnuFeeByFargation.Visible = false;
                        }
                        else if (i == 19)
                        {
                            if (Convert.ToBoolean(row["erview"]))
                                mnuPaymentReceived.Visible = true;
                            else mnuPaymentReceived.Visible = false;
                        }
                        i++;
                    }
                }
                else
                {
                    mnuAdmissionList.Visible = false;
                    mnuAdmission.Visible = false;
                    mnuBatch.Visible = false;
                    mnuChangePassword.Visible = false;
                    mnuCourse.Visible = false;
                    mnuSubject.Visible = false;
                    mnuEmployee.Visible = false;
                    mnuEnquiryList.Visible = false;
                    mnuEnquiry.Visible = false;
                    mnuFaculty.Visible = false;
                    mnuFollowUp.Visible = false;
                    mnuGroupSubCourse.Visible = false;
                    //mnuMessageAdmin.Visible = false;
                    mnuSubCourse.Visible = false;
                    mnuEmployeeRights.Visible = false;
                    //mnuNotesInward.Visible = false;
                    //mnuNotesOutward.Visible = false;
                    mnuPaymentReceived.Visible = false;
                    mnuFeeByFargation.Visible = false;
                }
            }
           
        }
    }
}