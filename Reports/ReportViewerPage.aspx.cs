using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

namespace InstituteManagement.Reports
{
    public partial class ReportViewerPage : System.Web.UI.Page
    {
        public string CallFromPage { get; set; }
        public string returnErrorMsgToParent { get; set; }
        string Message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ReportName"] != null)
            {
                if (Request.QueryString["ReportName"] == "AdmissionPrint")
                    LoadAdmissionPrintReport();
                else if (Request.QueryString["ReportName"] == "ReceiptPrint" || Request.QueryString["ReportName"] == "BounceReceiptPrint")
                    LoadReceiptPrintReport();
            }
        }

        private void LoadReceiptPrintReport()
        {
            if (Request.QueryString["ReceiptNo"] != null && Request.QueryString["AdmissionKey"] != null)
            {
                string sReceiptNo = Request.QueryString["ReceiptNo"].ToString();
                string sAdmissionKey = Request.QueryString["AdmissionKey"].ToString();
                if (sReceiptNo != "")
                    BindReceiptReport(Convert.ToInt32(sReceiptNo), Convert.ToInt32(sAdmissionKey));
            }
            else
            {
                returnErrorMsgToParent = "Please select record to generate report";
            }
        }
        private void LoadAdmissionPrintReport()
        {
            if (Request.QueryString["AdmissionKey"] != null)
            {
                string sAdmissionKey = Request.QueryString["AdmissionKey"].ToString();
                if (sAdmissionKey != "")
                    BindAdmissionReport(Convert.ToInt32(sAdmissionKey));
            }
            else
            {
                returnErrorMsgToParent = "Please select record to generate report";
            }
        }
        private decimal GetBalanceAmount(int iAdmissionKey, decimal totFee)
        {
            decimal PayAmt = 0;
            try
            {
                BAL.Class.SmartInstitute.PaymentReceiptClass o_GetPayBalance = new BAL.Class.SmartInstitute.PaymentReceiptClass();
                decimal TotalPaidFees = o_GetPayBalance.GetFeePaidForAdmission(iAdmissionKey, ref Message);

                PayAmt = totFee - TotalPaidFees;
                //if (Txt_PayAmt.Text.Trim() != "")
                //    PayAmt = Convert.ToDecimal(Txt_PayAmt.Text);
                //else PayAmt = 0;

                //if (Txt_FinalFees.Text.Trim() != "")
                //{
                //    decimal iTotalAmt = Convert.ToDecimal(Txt_FinalFees.Text);
                //    txtBalanceAmount.Text = Convert.ToString(iTotalAmt - (TotalPaidFees));
                //}
            }
            catch (Exception err)
            { }
            return PayAmt;
        }
        private void BindReceiptReport(int iReceiptNo, int iAdmissionKey)
        {
            try
            {
                DataSets.PaymentReceipt dsMain = new DataSets.PaymentReceipt();

                DAL.DAL ddl = new DAL.DAL();
                string connstr = ddl.GetConnectionString(ref Message);
                SqlConnection conn = new SqlConnection(connstr);
                SqlDataAdapter adpt = new SqlDataAdapter("select * from PaymentReceiptReport where paymentreceiptKey=" + iReceiptNo, conn);
                DataSet ds = new DataSet();


                BAL.Class.SmartInstitute.PaymentReceiptClass o_GetPayReceipt = new BAL.Class.SmartInstitute.PaymentReceiptClass();
                decimal totFeesPaid = o_GetPayReceipt.GetFeePaidForAdmission(iAdmissionKey, ref Message);
                string totalFeesPAidInWord = NumberToWords.ConvertAmount(Convert.ToDouble(totFeesPaid));

                BAL.Class.SmartInstitute.AdmissionClass o_GetAdmissionData = new BAL.Class.SmartInstitute.AdmissionClass();
                DataSet dsAdm = o_GetAdmissionData.GetAdmissionData(iAdmissionKey, ref Message);


                adpt.Fill(dsMain.Tables["PaymentReceiptData"]);

                if (dsMain != null && dsMain.Tables != null && dsMain.Tables["PaymentReceiptData"].Rows.Count > 0)
                {
                    DataRow rowMain = dsMain.Tables["PaymentReceiptData"].Rows[0];
                    if (Request.QueryString["ReportName"] == "BounceReceiptPrint")
                    {
                        rowMain["fees"] = rowMain["BounceAmount"];
                        totalFeesPAidInWord = NumberToWords.ConvertAmount(Convert.ToDouble(rowMain["fees"]));
                    }
                    if (dsAdm != null && dsAdm.Tables != null && dsAdm.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = dsAdm.Tables[0].Rows[0];
                        decimal totFee=Convert.ToDecimal(rowMain["totalfee"].ToString());
                       decimal balAmt= GetBalanceAmount(iAdmissionKey, totFee);
                        if (row != null)
                        {
                            rowMain["FeesPaidInWord"] = totalFeesPAidInWord;
                            //if (totFeesPaid < Convert.ToDecimal(row["firstInstallment"]))
                            //    rowMain["NextInstallmentAmount"] = Convert.ToDecimal(row["firstInstallment"]) - totFeesPaid;
                            //else if (totFeesPaid < (Convert.ToDecimal(row["firstInstallment"]) + Convert.ToDecimal(row["secondInstallment"])))
                            //    rowMain["NextInstallmentAmount"] = (Convert.ToDecimal(row["secondInstallment"]) + Convert.ToDecimal(row["firstInstallment"])) - totFeesPaid;

                            //else if (totFeesPaid < (Convert.ToDecimal(row["thirdInstallment"]) + Convert.ToDecimal(row["firstInstallment"]) + Convert.ToDecimal(row["secondInstallment"])))
                            //    rowMain["NextInstallmentAmount"] = (Convert.ToDecimal(row["thirdInstallment"]) + Convert.ToDecimal(row["firstInstallment"]) + Convert.ToDecimal(row["secondInstallment"])) - totFeesPaid;

                            //else if (totFeesPaid < (Convert.ToDecimal(row["thirdInstallment"]) + Convert.ToDecimal(row["firstInstallment"]) + Convert.ToDecimal(row["secondInstallment"]) + Convert.ToDecimal(row["fourthInstallment"])))
                            //    rowMain["NextInstallmentAmount"] = (Convert.ToDecimal(row["thirdInstallment"]) + Convert.ToDecimal(row["firstInstallment"]) + Convert.ToDecimal(row["secondInstallment"]) + Convert.ToDecimal(row["fourthInstallment"])) - totFeesPaid;

                            //else if (totFeesPaid < (Convert.ToDecimal(row["thirdInstallment"]) + Convert.ToDecimal(row["firstInstallment"]) + Convert.ToDecimal(row["secondInstallment"]) + Convert.ToDecimal(row["fourthInstallment"]) + Convert.ToDecimal(row["fifthInstallment"])))
                            //    rowMain["NextInstallmentAmount"] = (Convert.ToDecimal(row["thirdInstallment"]) + Convert.ToDecimal(row["firstInstallment"]) + Convert.ToDecimal(row["secondInstallment"]) + Convert.ToDecimal(row["fourthInstallment"]) + Convert.ToDecimal(row["fifthInstallment"])) - totFeesPaid;

                            //else if (totFeesPaid < (Convert.ToDecimal(row["thirdInstallment"]) + Convert.ToDecimal(row["firstInstallment"]) + Convert.ToDecimal(row["secondInstallment"]) + Convert.ToDecimal(row["fourthInstallment"]) + Convert.ToDecimal(row["fifthInstallment"]) + Convert.ToDecimal(row["sixthInstallment"])))
                            //    rowMain["NextInstallmentAmount"] = (Convert.ToDecimal(row["thirdInstallment"]) + Convert.ToDecimal(row["firstInstallment"]) + Convert.ToDecimal(row["secondInstallment"]) + Convert.ToDecimal(row["fourthInstallment"]) + Convert.ToDecimal(row["fifthInstallment"]) + Convert.ToDecimal(row["sixthInstallment"])) - totFeesPaid;

                            //else
                            //    rowMain["NextInstallmentAmount"] = "0";

                            rowMain["NextInstallmentAmount"] = balAmt;
                        }
                    }
                }

                ReportDocument rd = new ReportDocument();
                rd.Load(Server.MapPath("~/Reports/PaymentReceiptReport.rpt"));
                SerDBCredentials(rd);
                rd.SetDataSource(dsMain);
                CrystalReportViewer1.ReportSource = rd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception err)
            { }
        }
        private void BindAdmissionReport(int iAdmissionKey)
        {
            try
            {
                DataSets.AdmissionReceipt dsMain = new DataSets.AdmissionReceipt();

                DAL.DAL ddl = new DAL.DAL();
                string connstr = ddl.GetConnectionString(ref Message);
                SqlConnection conn = new SqlConnection(connstr);
                SqlDataAdapter adpt = new SqlDataAdapter("select * from AdmissionMainList where admissionKey=" + iAdmissionKey, conn);
                SqlDataAdapter adpt2 = new SqlDataAdapter("select * from AdmissionDetailsList where admissionKey=" + iAdmissionKey, conn);
                //DataSet ds = new DataSet();
                adpt.Fill(dsMain.Tables["Admission"]);
                adpt2.Fill(dsMain.Tables["AdmissionDetails"]);

                string subjects = "";
                int iRowNo = 1;
                if (dsMain != null && dsMain.Tables["AdmissionDetails"] != null)
                {
                    foreach (DataRow row in dsMain.Tables["AdmissionDetails"].Rows)
                    {
                        subjects = subjects + "    " + iRowNo + ") " + row["subjectCode"].ToString();
                        iRowNo++;
                    }
                    DataRow rowAdm = dsMain.Tables["Admission"].Rows[0];
                    rowAdm["SubjectList"] = subjects;
                    dsMain.AcceptChanges();
                }


                ReportDocument rd = new ReportDocument();
                rd.Load(Server.MapPath("~/Reports/AdmissionPrint.rpt"));
                SerDBCredentials(rd);
                rd.SetDataSource(dsMain);
                CrystalReportViewer1.ReportSource = rd;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception err)
            { }
        }

        private void SerDBCredentials(ReportDocument rd)
        {
            DAL.DAL dal = new DAL.DAL();
            string conn = dal.GetConnectionString(ref Message);
            //string conn = ConfigurationManager.ConnectionStrings["MMConnectionString"].ConnectionString;


            SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder(conn);

            string ServerName = SqlConnectionStringBuilder.DataSource;
            string DatabaseName = SqlConnectionStringBuilder.InitialCatalog;
            Boolean IntegratedSecurity = SqlConnectionStringBuilder.IntegratedSecurity;
            string UserID = SqlConnectionStringBuilder.UserID;
            string Password = SqlConnectionStringBuilder.Password;

            foreach (CrystalDecisions.CrystalReports.Engine.Table Table in rd.Database.Tables)
            {

                CrystalDecisions.Shared.TableLogOnInfo TableLogOnInfo = Table.LogOnInfo;

                TableLogOnInfo.ConnectionInfo.ServerName = ServerName;
                TableLogOnInfo.ConnectionInfo.DatabaseName = DatabaseName;
                TableLogOnInfo.ConnectionInfo.AllowCustomConnection = true;
                TableLogOnInfo.ConnectionInfo.IntegratedSecurity = IntegratedSecurity;

                if (IntegratedSecurity != true)
                {
                    TableLogOnInfo.ConnectionInfo.UserID = UserID;
                    TableLogOnInfo.ConnectionInfo.Password = Password;
                }

                Table.ApplyLogOnInfo(TableLogOnInfo);
            }
        }
    }
}