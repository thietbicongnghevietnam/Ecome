using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using MATERIAL_IN_OUT.AppCode;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using PdfSharp.Drawing.BarCodes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Media3D;
using MigraDoc.DocumentObjectModel.Shapes;
using DocumentFormat.OpenXml.Drawing;
using System.Net.Mail;

namespace MATERIAL_IN_OUT
{
    public partial class frmPMSOutSap : System.Web.UI.Page
    {
        public DataTable dt = new DataTable();
        public DataTable dt_update = new DataTable();

        public string listUserMCS = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Date1.Value = DateTime.Now.ToString("yyyy-MM-dd");
            Date1.Value = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            ngaychiid.Value = DateTime.Now.ToString("yyyy-MM-dd");
            
            // Xử lý AJAX request riêng
            if (Request["action"] == "getDetail")
            {
                HandleGetDetail();
                Response.End();
                return;
            }
            dt = DataConn.StoreFillDS("Select_PMS_OutSAP", System.Data.CommandType.StoredProcedure);            
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];
            string requestno = filterRequestNo.Value;
            string sanctionno = filterSanctionNo.Value;

            string typesapPMS = filterSapPMS.Value;


            //if (rbPMS.Checked)
            //{
                
            //}
            //else if (rbMCS.Checked)
            //{
               
            //}

            //dt = DataConn.StoreFillDS("Select_PMS_OutSAP_search", System.Data.CommandType.StoredProcedure, _fromdate, _todate, requestno, sanctionno);
            dt = DataConn.StoreFillDS("Select_PMS_OutSAP_search2", System.Data.CommandType.StoredProcedure, _fromdate, _todate, requestno, sanctionno, typesapPMS);

            if (dt.Rows.Count > 0)
            {
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('search successfully!');", true);
                Date1.Value = _fromdate.ToString();
            }
            else 
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Data null!');", true);
            }

            

        }

        protected void Dowload_All_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];
            string userid = Session["UserName"].ToString();

            string requestno = filterRequestNo.Value;
            string sanctionno = filterSanctionNo.Value;

            bool isDownloadDetail = chkDownloadDetail.Checked;
            string typedownload = "0";

            if (isDownloadDetail)
            {
                // Download kèm detail
                typedownload = "1";
            }
            else 
            {
                // Download bình thường
                typedownload = "0";
            }

            DataTable dtexport = DataConn.StoreFillDS("Select_PMS_OutSAP_download", System.Data.CommandType.StoredProcedure, _fromdate, _todate, userid, typedownload);

            if (dtexport.Rows.Count > 0)
            {
                if (dtexport.Rows[0][0].ToString() == "1")
                {
                    //DataTable dt = DataConn.StoreFillDS("Select_PMS_OutSAP",
                    //    System.Data.CommandType.StoredProcedure, Requestno, TypeName);

                    if (dtexport.Rows.Count > 0)
                    {
                        ExportToCSV(dtexport, "PMS_dowload_all_issueout" + ".csv");
                    }
                }
                else if (dtexport.Rows[0][0].ToString() == "2")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(),
                        "Message", "toastr.error('NG, User do not PMS!');", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(),
                        "Message", "toastr.error('NG, Check again!');", true);
                }
            }
            else 
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG,Data null, Check again!');", true);
            }
        }

        public void Exportfrom98(object sender, EventArgs e)
        {
            string Requestno = RequestNoid.Text;
            string TypeName = TypeFormid.Text;
            string Sanction = sanctionid.Text;
            string MVT = MVTid.Text;
            string userid = Session["UserName"].ToString();
            
            listUserMCS = File.ReadAllText(Server.MapPath("~/Template/list_user_MCS.txt"));

            List<string> lst = listUserMCS.Split(',').ToList();
            bool exists = lst.Contains(userid);

            if (exists)
            {
                // userid có trong list  //user cua MCS
                if (Requestno != "" && TypeName != "")
                {
                    DataTable dtexport = DataConn.StoreFillDS("Export_Form_98_MCS",
                        System.Data.CommandType.StoredProcedure, Requestno, TypeName, userid, Sanction, MVT);

                    if (dtexport.Rows[0][0].ToString() == "1")
                    {
                        //DataTable dt = DataConn.StoreFillDS("Select_PMS_OutSAP",
                        //    System.Data.CommandType.StoredProcedure, Requestno, TypeName);

                        if (dtexport.Rows.Count > 0)
                        {
                            ExportToCSV(dtexport, "PMS_OutSAP_98_" + Requestno + ".csv");
                        }
                    }
                    else if (dtexport.Rows[0][0].ToString() == "2")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, User do not PMS!');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, Data null, Check again!');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(),
                        "Message", "toastr.error('NG, data null!');", true);
                }

            }
            else
            {
                // userid không có trong list  //user cua PMS
                if (Requestno != "" && TypeName != "" && Sanction != "")
                {
                    DataTable dtexport = DataConn.StoreFillDS("Export_Form_98",
                        System.Data.CommandType.StoredProcedure, Requestno, TypeName, userid, Sanction, MVT);

                    if (dtexport.Rows[0][0].ToString() == "1")
                    {
                        //DataTable dt = DataConn.StoreFillDS("Select_PMS_OutSAP",
                        //    System.Data.CommandType.StoredProcedure, Requestno, TypeName);

                        if (dtexport.Rows.Count > 0)
                        {
                            ExportToCSV(dtexport, "PMS_OutSAP_98_" + Requestno + ".csv");
                        }
                    }
                    else if (dtexport.Rows[0][0].ToString() == "2")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, User do not PMS!');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, Data null, Check again!');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(),
                        "Message", "toastr.error('NG, data null!');", true);
                }
            }

            
        }

        public void Exporttranfer99(object sender, EventArgs e)
        {
            string Requestno = RequestNo4.Text;
            string TypeName = TypeForm4.Text;
            string Sanction = sanction4.Text;
            string MVT = MVT4.Text;
            string userid = Session["UserName"].ToString();

            listUserMCS = File.ReadAllText(Server.MapPath("~/Template/list_user_MCS.txt"));

            List<string> lst = listUserMCS.Split(',').ToList();
            bool exists = lst.Contains(userid);

            if (exists)
            {
                //user MCS
                if (Requestno != "" && TypeName != "")
                {
                    DataTable dtexport = DataConn.StoreFillDS("Export_Tranfer_99_MCS",
                        System.Data.CommandType.StoredProcedure, Requestno, TypeName, userid, Sanction, MVT);

                    if (dtexport.Rows[0][0].ToString() == "1")
                    {
                        //DataTable dt = DataConn.StoreFillDS("Select_PMS_OutSAP",
                        //    System.Data.CommandType.StoredProcedure, Requestno, TypeName);

                        if (dtexport.Rows.Count > 0)
                        {
                            ExportToCSV(dtexport, "PMS_Tranfer_99_" + Requestno + ".csv");
                        }
                    }
                    else if (dtexport.Rows[0][0].ToString() == "2")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, User do not PMS!');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, Check again!');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(),
                        "Message", "toastr.error('NG, data null!');", true);
                }

            }
            else 
            {
                //user PMS
                if (Requestno != "" && TypeName != "" && Sanction != "")
                {
                    DataTable dtexport = DataConn.StoreFillDS("Export_Tranfer_99",
                        System.Data.CommandType.StoredProcedure, Requestno, TypeName, userid, Sanction, MVT);

                    if (dtexport.Rows[0][0].ToString() == "1")
                    {
                        //DataTable dt = DataConn.StoreFillDS("Select_PMS_OutSAP",
                        //    System.Data.CommandType.StoredProcedure, Requestno, TypeName);

                        if (dtexport.Rows.Count > 0)
                        {
                            ExportToCSV(dtexport, "PMS_Tranfer_99_" + Requestno + ".csv");
                        }
                    }
                    else if (dtexport.Rows[0][0].ToString() == "2")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, User do not PMS!');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, Check again!');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(),
                        "Message", "toastr.error('NG, data null!');", true);
                }
            }
            
        }

        public void ExportOutscrap(object sender, EventArgs e)
        {
            string Requestno = RequestNo5.Text;
            string TypeName = TypeForm5.Text;
            string Sanction = sanction5.Text;
            string MVT = MVT5.Text;
            string userid = Session["UserName"].ToString();

            listUserMCS = File.ReadAllText(Server.MapPath("~/Template/list_user_MCS.txt"));

            List<string> lst = listUserMCS.Split(',').ToList();
            bool exists = lst.Contains(userid);

            if (exists)
            {
                //User MCS
                if (Requestno != "" && TypeName != "")
                {
                    DataTable dtexport = DataConn.StoreFillDS("Export_Outscrap_MCS",
                        System.Data.CommandType.StoredProcedure, Requestno, TypeName, userid, Sanction, MVT);

                    if (dtexport.Rows[0][0].ToString() == "1")
                    {
                        //DataTable dt = DataConn.StoreFillDS("Select_PMS_OutSAP",
                        //    System.Data.CommandType.StoredProcedure, Requestno, TypeName);

                        if (dtexport.Rows.Count > 0)
                        {
                            ExportToCSV(dtexport, "PMS_ExportOutscrap_" + Requestno + ".csv");
                        }
                    }
                    else if (dtexport.Rows[0][0].ToString() == "2")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, User do not PMS!');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, Check again!');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(),
                        "Message", "toastr.error('NG, data null!');", true);
                }
            }
            else 
            {
                //user PMS
                if (Requestno != "" && TypeName != "" && Sanction != "")
                {
                    DataTable dtexport = DataConn.StoreFillDS("Export_Outscrap",
                        System.Data.CommandType.StoredProcedure, Requestno, TypeName, userid, Sanction, MVT);

                    if (dtexport.Rows[0][0].ToString() == "1")
                    {
                        //DataTable dt = DataConn.StoreFillDS("Select_PMS_OutSAP",
                        //    System.Data.CommandType.StoredProcedure, Requestno, TypeName);

                        if (dtexport.Rows.Count > 0)
                        {
                            ExportToCSV(dtexport, "PMS_ExportOutscrap_" + Requestno + ".csv");
                        }
                    }
                    else if (dtexport.Rows[0][0].ToString() == "2")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, User do not PMS!');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, Check again!');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(),
                        "Message", "toastr.error('NG, data null!');", true);
                }
            }

            
        }

        public void ExportOtherIssue(object sender, EventArgs e)
        {
            string Requestno = RequestNo7.Text;
            string TypeName = TypeForm7.Text;
            string Sanction = sanction7.Text;
            string MVT = MVT7.Text;
            string userid = Session["UserName"].ToString();

            listUserMCS = File.ReadAllText(Server.MapPath("~/Template/list_user_MCS.txt"));

            List<string> lst = listUserMCS.Split(',').ToList();
            bool exists = lst.Contains(userid);

            if (exists)
            {
                //user MCS
                if (Requestno != "" && TypeName != "")
                {
                    DataTable dtexport = DataConn.StoreFillDS("Export_OtherIssueSAP_MCS",
                        System.Data.CommandType.StoredProcedure, Requestno, TypeName, userid, Sanction, MVT);

                    if (dtexport.Rows[0][0].ToString() == "1")
                    {
                        //DataTable dt = DataConn.StoreFillDS("Select_PMS_OutSAP",
                        //    System.Data.CommandType.StoredProcedure, Requestno, TypeName);

                        if (dtexport.Rows.Count > 0)
                        {
                            ExportToCSV(dtexport, "PMS_ExportOther_" + Requestno + ".csv");
                        }
                    }
                    else if (dtexport.Rows[0][0].ToString() == "2")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, User do not PMS!');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, Check again!');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(),
                        "Message", "toastr.error('NG, data null!');", true);
                }
            }
            else 
            {
                //user PMS
                if (Requestno != "" && TypeName != "" && Sanction != "")
                {
                    DataTable dtexport = DataConn.StoreFillDS("Export_OtherIssueSAP",
                        System.Data.CommandType.StoredProcedure, Requestno, TypeName, userid, Sanction, MVT);

                    if (dtexport.Rows[0][0].ToString() == "1")
                    {
                        //DataTable dt = DataConn.StoreFillDS("Select_PMS_OutSAP",
                        //    System.Data.CommandType.StoredProcedure, Requestno, TypeName);

                        if (dtexport.Rows.Count > 0)
                        {
                            ExportToCSV(dtexport, "PMS_ExportOther_" + Requestno + ".csv");
                        }
                    }
                    else if (dtexport.Rows[0][0].ToString() == "2")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, User do not PMS!');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, Check again!');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(),
                        "Message", "toastr.error('NG, data null!');", true);
                }
            }

            
        }

        public void ExportSubIssue(object sender, EventArgs e)
        {
            string Requestno = RequestNo9.Text;
            string TypeName = TypeForm9.Text;
            string Sanction = sanction9.Text;
            string MVT = MVT9.Text;
            string userid = Session["UserName"].ToString();

            listUserMCS = File.ReadAllText(Server.MapPath("~/Template/list_user_MCS.txt"));

            List<string> lst = listUserMCS.Split(',').ToList();
            bool exists = lst.Contains(userid);

            if (exists)
            {
                //user MCS
                if (Requestno != "" && TypeName != "")
                {
                    DataTable dtexport = DataConn.StoreFillDS("Export_SubIssueSAP_MCS",
                        System.Data.CommandType.StoredProcedure, Requestno, TypeName, userid, Sanction, MVT);

                    if (dtexport.Rows[0][0].ToString() == "1")
                    {
                        //DataTable dt = DataConn.StoreFillDS("Select_PMS_OutSAP",
                        //    System.Data.CommandType.StoredProcedure, Requestno, TypeName);

                        if (dtexport.Rows.Count > 0)
                        {
                            ExportToCSV(dtexport, "PMS_ExportSub_" + Requestno + ".csv");
                        }
                    }
                    else if (dtexport.Rows[0][0].ToString() == "2")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, User do not PMS!');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, Check again!');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(),
                        "Message", "toastr.error('NG, data null!');", true);
                }
            }
            else
            {
                //user PMS
                if (Requestno != "" && TypeName != "" && Sanction != "")
                {
                    DataTable dtexport = DataConn.StoreFillDS("Export_SubIssueSAP",
                        System.Data.CommandType.StoredProcedure, Requestno, TypeName, userid, Sanction, MVT);

                    if (dtexport.Rows[0][0].ToString() == "1")
                    {
                        //DataTable dt = DataConn.StoreFillDS("Select_PMS_OutSAP",
                        //    System.Data.CommandType.StoredProcedure, Requestno, TypeName);

                        if (dtexport.Rows.Count > 0)
                        {
                            ExportToCSV(dtexport, "PMS_ExporSub_" + Requestno + ".csv");
                        }
                    }
                    else if (dtexport.Rows[0][0].ToString() == "2")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, User do not PMS!');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG, Check again!');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(),
                        "Message", "toastr.error('NG, data null!');", true);
                }
            }


        }
        public void UpdateDocumentNo(object sender, EventArgs e)
        {
            string Requestno = RequestNo6.Text;
            string TypeName = TypeForm6.Text;
            string DocumentNo = DocumentNo6.Text;
            string userid = Session["UserName"].ToString();

            if (Requestno != "" && TypeName != "" && DocumentNo != "")
            {
                DataTable dtexport = DataConn.StoreFillDS("Update_Document_No", System.Data.CommandType.StoredProcedure, Requestno, TypeName, DocumentNo, userid);

                if (dtexport.Rows[0][0].ToString() == "1")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Update Document Sucess !!!');", true);
                    dt = DataConn.StoreFillDS("Select_PMS_OutSAP", System.Data.CommandType.StoredProcedure);
                }
                else 
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Check again!'); ", true);
                }    
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(),
                    "Message", "toastr.error('NG, data is not null!');", true);
            }
        }

        public void btn_feedback_click(object sender, EventArgs e)
        {
            string Requestno = RequestNo8.Text;
            string TypeName = TypeForm8.Text;
            string sanction = sanction8.Text;
            string contentsPMS = contents_feedback.Text;
            string userid = Session["UserName"].ToString();

            if (Requestno != "" && TypeName != "" && contentsPMS != "")
            {
                DataTable dtexport = DataConn.StoreFillDS("Update_Feedback_PMS", System.Data.CommandType.StoredProcedure, Requestno, TypeName, sanction, userid, contentsPMS);

                //gui mail thong tin cho nguowi tao requset
                //--------------------------------------------//gui mail thong tin cho nguowi tao requset //-------------------------
                string subject = "PMS FeedBack request: " + Requestno;
                string ToEmail = dtexport.Rows[0][2].ToString();    // mail request
                string UserNext = dtexport.Rows[0][1].ToString();  // user tạo request
                string conntents_mail = contentsPMS;               // nội dung chi tiết email

                // Đọc template HTML
                string templatePath = Server.MapPath("~/Fomat_SendEmail/FeedBackPMS.html");
                string emailBody = "";
                using (StreamReader reader = new StreamReader(templatePath))
                {
                    emailBody = reader.ReadToEnd();
                }

                // Thay placeholder bằng giá trị thật
                emailBody = emailBody.Replace("{subject}", subject)
                                     .Replace("{RQNO}", Requestno.ToString())
                                     .Replace("{UserNext}", UserNext)
                                     .Replace("{conntents_mail}", conntents_mail);

                // Tạo mail message
                System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress("tax.psnv@vn.panasonic.com", "Issue In-Out Material");
                System.Net.Mail.MailMessage objMessage = new System.Net.Mail.MailMessage();
                objMessage.From = fromAddress;
                objMessage.Subject = subject;
                objMessage.Body = emailBody;
                objMessage.IsBodyHtml = true;
                objMessage.Priority = MailPriority.High;

                // Gửi cho tất cả email
                string[] ToEmailList = ToEmail.Split(',');
                foreach (string emailTo in ToEmailList)
                {
                    objMessage.To.Add(new MailAddress(emailTo));
                }

                // Smtp client
                System.Net.Mail.SmtpClient objClient = new System.Net.Mail.SmtpClient("157.8.1.131");
                objClient.Send(objMessage);
                //---------------------------// end send mail //-------------------------
                if (dtexport.Rows[0][0].ToString() == "1")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Update Document Sucess !!!');", true);
                    dt = DataConn.StoreFillDS("Select_PMS_OutSAP", System.Data.CommandType.StoredProcedure);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Check again!'); ", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(),
                    "Message", "toastr.error('NG, data is not null!');", true);
            }
        }

        public void ExportToCSV(DataTable dt1, string fileName)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.UTF8;
            Response.BinaryWrite(Encoding.UTF8.GetPreamble());

            StringBuilder sb = new StringBuilder();

            // Header (bỏ cột 0)
            for (int i = 1; i < dt1.Columns.Count; i++)
            {
                sb.Append(dt1.Columns[i].ColumnName + ",");
            }
            sb.AppendLine();

            // Data (bỏ cột 0)
            foreach (DataRow row in dt1.Rows)
            {
                for (int i = 1; i < dt1.Columns.Count; i++)
                {
                    sb.Append(row[i].ToString().Replace(",", " ") + ",");
                }
                sb.AppendLine();
            }

            Response.Write(sb.ToString());
            Response.End();
        }


        private void HandleGetDetail()
        {
            Response.Clear(); // Xóa buffer trước
            Response.ContentType = "application/json";

            try
            {
                string requestNo = Request["requestNo"];
                string typeForm = Request["typeForm"];

                string tableName = typeForm == "B" ? "tbl_RQ_MaterialIssueB" : "tbl_RQ_MaterialIssue";
                string sql = $@"SELECT Material, Plant, VendorCode,CostCenter, Sloc, IssueQty, UnitPrice_ST, Amount_ST,DocumentNo,SanctionName,MvType FROM {tableName} WHERE RequestNo = @RequestNo";

                var list = new List<object>();
                using (SqlConnection con = new SqlConnection(DataConn.source))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@RequestNo", requestNo);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        list.Add(new
                        {
                            Material = dr["Material"].ToString(), 
                            Plant = dr["Plant"].ToString(),
                            VendorCode = dr["VendorCode"].ToString(),
                            CostCenter = dr["CostCenter"].ToString(),
                            Sloc = dr["Sloc"].ToString(),
                            IssueQty = dr["IssueQty"].ToString(),
                            UnitPrice_ST = dr["UnitPrice_ST"].ToString(),
                            Amount_ST = dr["Amount_ST"].ToString(),
                            DocumentNo = dr["DocumentNo"].ToString(),
                            SanctionName = dr["SanctionName"].ToString(),
                            MvType = dr["MvType"].ToString()
                        });
                    }
                }

                Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.Write("{\"error\": \"" + ex.Message.Replace("\"", "'") + "\"}");
            }
            finally
            {
                Response.Flush();
                Response.SuppressContent = true;  // Ngăn ASP.NET render thêm HTML
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Thay thế Response.End()
            }
        }

        //[WebMethod(EnableSession = true)]
        //public static List<object> GetRequestDetail(string requestNo, string typeForm)
        //{
        //    List<object> list = new List<object>();

        //    string tableName = typeForm == "B"
        //        ? "tbl_RQ_MaterialIssueB"
        //        : "tbl_RQ_MaterialIssue";

        //    string sql = $@"SELECT 
        //                Material,
        //                ItemDescription,
        //                Plant,
        //                VendorCode,
        //                CostCenter,
        //                Sloc,
        //                IssueQty,
        //                UnitPrice_ST,
        //                Amount_ST
        //            FROM {tableName}
        //            WHERE RequestNo = @RequestNo";

        //    using (SqlConnection con = new SqlConnection(DataConn.source))
        //    {
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        cmd.Parameters.AddWithValue("@RequestNo", requestNo);

        //        con.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();

        //        while (dr.Read())
        //        {
        //            list.Add(new
        //            {
        //                Material = dr["Material"].ToString(),
        //                ItemDescription = dr["ItemDescription"].ToString(),
        //                Plant = dr["Plant"].ToString(),
        //                VendorCode = dr["VendorCode"].ToString(),
        //                CostCenter = dr["CostCenter"].ToString(),
        //                Sloc = dr["Sloc"].ToString(),
        //                IssueQty = dr["IssueQty"].ToString(),
        //                UnitPrice_ST = dr["UnitPrice_ST"].ToString(),
        //                Amount_ST = dr["Amount_ST"].ToString()
        //            });
        //        }
        //    }

        //    return list;
        //}

        //[WebMethod(EnableSession = true)]
        //public static string GetRequestDetail(string requestNo, string typeForm)
        //{
        //    String daresult = null;
        //    DataTable yourDatable = new DataTable();
        //    DataSet ds = new DataSet();
        //    yourDatable = DataConn.StoreFillDS("Get_List_dactinh_doichieu", System.Data.CommandType.StoredProcedure, requestNo, typeForm);

        //    DataTable dt2 = new DataTable();
        //    dt2 = yourDatable.Copy();

        //    ds.Tables.Add(dt2);
        //    daresult = DataSetToJSON(ds);
        //    return daresult;

        //}

        //public static string DataSetToJSON(DataSet ds)
        //{
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    foreach (DataTable dt in ds.Tables)
        //    {
        //        object[] arr = new object[dt.Rows.Count + 1];

        //        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //        {
        //            arr[i] = dt.Rows[i].ItemArray;
        //        }

        //        //dict.Add(dt.TableName, arr);
        //        dict.Add(dt.TableName, arr);
        //    }

        //    JavaScriptSerializer json = new JavaScriptSerializer();
        //    return json.Serialize(dict);
        //}

        protected void ImportFromExcel(object sender, EventArgs e)
        {
            if (!FileUpload.HasFile)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Ban Chon file!');", true);
                return;
            }

            string filePath = Server.MapPath(".") + "\\" + FileUpload.FileName;

            try
            {
                FileUpload.SaveAs(filePath);

                DataTable dtExcelData = new DataTable();
                int count_record = 0;

                using (StreamReader sr = new StreamReader(filePath))
                {
                    int rowIndex = 0;

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] rows = line.Split(',');

                        if (rowIndex >= 1) // bỏ dòng header
                        {
                            if (rows.Length >= 7)
                            {
                                string requestno = rows[0].Trim();
                                string department = rows[1].Trim();
                                string typeform = rows[4].Trim();
                                string sanctionname = rows[5].Trim();
                                string documentno = rows[6].Trim();

                                if (requestno != "" && department != "" && typeform != "" && documentno != "")
                                {
                                    DataTable dt_checkupload = DataConn.StoreFillDS(
                                        "Upload_Document_PMS",
                                        System.Data.CommandType.StoredProcedure,
                                        requestno,
                                        department,
                                        typeform,
                                        sanctionname,
                                        documentno
                                    );

                                    if (dt_checkupload.Rows.Count > 0 && dt_checkupload.Rows[0][0].ToString() == "1")
                                    {
                                        count_record++;
                                    }
                                }
                            }
                        }

                        rowIndex++;
                    }
                }

                if (count_record > 0)
                {
                    lblConfirm.Text = "DATA IMPORTED SUCCESSFULLY.";
                    lblConfirm.Attributes.Add("style", "color:green");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('OK, SUCCESSFULLY!');", true);
                    dt = DataConn.StoreFillDS("Select_PMS_OutSAP", System.Data.CommandType.StoredProcedure);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, check again!');", true);
                }
            }
            catch (Exception ex)
            {
                lblConfirm.Text = "Lỗi : " + ex.Message;
                lblConfirm.Attributes.Add("style", "color:red");
            }
            finally
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

        }



    }
}