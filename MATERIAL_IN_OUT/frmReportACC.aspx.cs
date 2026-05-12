using MATERIAL_IN_OUT.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace MATERIAL_IN_OUT
{
    public partial class frmReportACC : System.Web.UI.Page
    {
        public DataTable dt_report = new DataTable();
        public DataTable dtsection = new DataTable();
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Datefrom1.Value = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                Dateto1.Value = DateTime.Now.ToString("yyyy-MM-dd");

                dt_report = DataConn.StoreFillDS("Select_PMS_OutSAP_ReportACC", System.Data.CommandType.StoredProcedure);

                //danh sach bophan
                dtsection = DataConn.StoreFillDS("pro_get_section", System.Data.CommandType.StoredProcedure);
                DataRow newRow1 = dtsection.NewRow();
                newRow1["DeptCode"] = "==Section==";
                dtsection.Rows.InsertAt(newRow1, 0);
                dr_filter_section.DataSource = dtsection;
                dr_filter_section.DataBind();

                // Xử lý AJAX request riêng
                if (Request["action"] == "getDetailcoment")
                {
                    HandleGetDetail();
                    Response.End();
                    return;
                }
                if (Request["action"] == "nextSign")
                {
                    HandleNextSign();
                    return;
                }
            }
        }

        // ===== METHOD MỚI: Xử lý Next Sign qua Request["action"] =====
        private void HandleNextSign()
        {
            Response.Clear();
            Response.ContentType = "application/json";

            try
            {
                // Kiểm tra session
                if (Session == null || Session["UserName"] == null)
                {
                    Response.Write("{\"result\":\"SESSION_EXPIRED\"}");
                    Response.Flush();
                    Response.SuppressContent = true;
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    return;
                }

                string requestNo = Request["requestNo"];
                string typeForm = Request["typeForm"];
                string department = Request["department"];
                string user_ = Session["UserName"].ToString();

                DataTable dt = DataConn.StoreFillDS(
                    "sp_NextSign_Process",
                    CommandType.StoredProcedure,
                    requestNo,
                    typeForm,
                    department
                );

                if (dt.Rows.Count > 0)
                {
                    string nextStep = dt.Rows[0]["NextSignature"].ToString().Replace("\"", "'");
                    string usernext = dt.Rows[0]["level_sign"].ToString().Replace("\"", "'");

                    Response.Write("{\"result\":\"OK\",\"nextStep\":\"" + nextStep + "\",\"userNext\":\"" + usernext + "\"}");
                }
                else
                {
                    Response.Write("{\"result\":\"NG\"}");
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.Write("{\"result\":\"ERROR\",\"message\":\"" + ex.Message.Replace("\"", "'") + "\"}");
            }
            finally
            {
                Response.Flush();
                Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        // =============================================================

        private void HandleGetDetail()
        {
            Response.Clear(); // Xóa buffer trước
            Response.ContentType = "application/json";

            try
            {
                string requestNo = Request["requestNo"];
                string typeForm = Request["typeForm"];

                string tableName = typeForm == "B" ? "tbl_RQ_MaterialB_Comment" : "tbl_RQMaterial_Comment";
                string sql = $@"SELECT RQ, UserCode, FullName,Content_Comment, DateUpdate FROM {tableName} WHERE RQ = @RequestNo";

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
                            RQ = dr["RQ"].ToString(),
                            UserCode = dr["UserCode"].ToString(),
                            FullName = dr["FullName"].ToString(),
                            Content_Comment = dr["Content_Comment"].ToString(),
                            DateUpdate = dr["DateUpdate"].ToString(),              
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

        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public static string NextSign(string requestNo, string typeForm, string department)
        {
            try
            {
                // Kiểm tra session
                if (HttpContext.Current.Session == null ||
                    HttpContext.Current.Session["UserName"] == null)
                {
                    return "SESSION_EXPIRED";
                }

                string user_ = HttpContext.Current.Session["UserName"].ToString();

                DataTable dt = DataConn.StoreFillDS(
                    "sp_NextSign_Process",
                    CommandType.StoredProcedure,
                    requestNo,
                    typeForm,
                    department
                );

                if (dt.Rows.Count > 0)
                {
                    string nextStep = dt.Rows[0]["NextSignature"].ToString();
                    string usernext = dt.Rows[0]["level_sign"].ToString();
                    return "OK-" + nextStep + "-" + usernext;
                }
                return "NG";
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }

        protected void dr_filter_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            string section = dr_filter_section.SelectedValue;
            dt_report = DataConn.StoreFillDS("Select_PMS_OutSAP_ReportACC_section", System.Data.CommandType.StoredProcedure, section);

        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string trangthai = ddlStatus.SelectedValue;
            string _fromdate = Request.Form[Datefrom1.UniqueID];
            string _todate = Request.Form[Dateto1.UniqueID];
            dt_report = DataConn.StoreFillDS("Select_PMS_OutSAP_ReportACC_trangthai", System.Data.CommandType.StoredProcedure, trangthai, _fromdate, _todate);
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Datefrom1.UniqueID];
            string _todate = Request.Form[Dateto1.UniqueID];
            string userid = Session["UserName"].ToString();

            string requestno = filterRequestNo.Value;
            string sanctionno = filterSanctionNo.Value;
            string typeSapPMS = filterSapPMS.Value;

            dt_report = DataConn.StoreFillDS("Select_PMS_OutSAP_ReportACC_search", System.Data.CommandType.StoredProcedure, _fromdate, _todate, userid, requestno, sanctionno, typeSapPMS);


            if (dt_report.Rows.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('search successfully!');", true);
                //Date1.Value = _fromdate.ToString();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Data null!');", true);
            }

        }

        protected void Dowload_All_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Datefrom1.UniqueID];
            string _todate = Request.Form[Dateto1.UniqueID];
            string userid = Session["UserName"].ToString();

            string requestno = filterRequestNo.Value;
            string sanctionno = filterSanctionNo.Value;
            string typeSapPMS = filterSapPMS.Value;

            DataTable dtexport = DataConn.StoreFillDS("Select_PMS_OutSAP_ReportACC_download", System.Data.CommandType.StoredProcedure, _fromdate, _todate, userid);

            //phai loc nhung truong hop NG ra ****
            if (dtexport.Rows.Count > 0)
            {
                ExportToCSV(dtexport, "ACC_report_issueout" + ".csv");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG,Data null, Check again!');", true);
            }
        }

        protected void Report_All_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Datefrom1.UniqueID];
            string _todate = Request.Form[Dateto1.UniqueID];
            string userid = Session["UserName"].ToString();

            string requestno = filterRequestNo.Value;
            string sanctionno = filterSanctionNo.Value;
            string typeSapPMS = filterSapPMS.Value;

            DataTable dtexport = DataConn.StoreFillDS("Select_PMS_OutSAP_ReportACC_download2", System.Data.CommandType.StoredProcedure, _fromdate, _todate, userid);

            //phai loc nhung truong hop NG ra ****
            if (dtexport.Rows.Count > 0)
            {
                ExportToCSV(dtexport, "Report_Status_issueout" + ".csv");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(),
                            "Message", "toastr.error('NG,Data null, Check again!');", true);
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

            for (int i = 0; i < dt1.Columns.Count; i++)
            {
                sb.Append(dt1.Columns[i].ColumnName + ",");
            }
            sb.AppendLine();
            // xu ly loi xuong dong => cot comment log  //[tbl_RQMaterial_Comment] where RQ='RQA-COS-0426-38'
            //da sua **** 22.04.2026
            foreach (DataRow row in dt1.Rows)
            {
                for (int i = 0; i < dt1.Columns.Count; i++)
                {
                    string value = row[i].ToString();

                    // Gộp xuống dòng
                    value = value.Replace("\r\n", " ")
                                 .Replace("\n", " ")
                                 .Replace("\r", " ");

                    // Escape dấu "
                    value = value.Replace("\"", "\"\"");

                    sb.Append($"\"{value}\",");
                }
                sb.AppendLine();
            }

            Response.Write(sb.ToString());
            Response.End();
        }


       
       



    }
}