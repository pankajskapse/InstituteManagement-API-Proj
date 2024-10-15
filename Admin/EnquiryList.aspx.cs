using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InstituteManagement.Admin
{
    public partial class EnquiryList : System.Web.UI.Page
    {
        string Message = string.Empty;

        void Page_PreInit(Object sender, EventArgs e)
        {
            this.MasterPageFile = "~/MasterPage/Standard.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            FillEnquiryGrid();
        }
        private void FillEnquiryGrid()
        {
            BAL.Class.SmartInstitute.EnquiryClass o_GetEnq = new BAL.Class.SmartInstitute.EnquiryClass();
            DataTable dtEnq = o_GetEnq.GetEnquiryList(ref Message);
            grdEnquiryList.DataSource = dtEnq;
            grdEnquiryList.DataBind();
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

                StringBuilder script = new StringBuilder();
                script.Append("<script type='text/javascript'>");
                //script.Append("window.opener.loadEnquiryDataOne('" + EnqID +"');");
                script.Append("window.opener.loadEnquiryData('" + EnqKey + "','" + EnqID + "','" + fName + "','" + lName + "','" + emailID + "','" + MobileNo + "','" + gender +
                    "','" + collegeName + "','" + enquiryTypeKey + "','" + batchKey + "');");
                script.Append("window.close();");
                script.Append("</script>");

                Page.RegisterStartupScript("test", script.ToString());

                //RegisterWindowsCloseScript(script.ToString());
            }
        }
    }
}