using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using MATERIAL_IN_OUT.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Interop;

namespace MATERIAL_IN_OUT
{
    public partial class frmPMSOutSap : System.Web.UI.Page
    {
        public DataTable dt = new DataTable();
        public DataTable dt_update = new DataTable();

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

            dt = DataConn.StoreFillDS("Select_PMS_OutSAP_search", System.Data.CommandType.StoredProcedure, _fromdate, _todate, requestno, sanctionno);

            if (dt.Rows.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('search successfully!');", true);
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
            string userid = Session["UserName"].ToString();

            if (Requestno != "" && TypeName != "")
            {
                DataTable dtexport = DataConn.StoreFillDS("Export_Form_98",
                    System.Data.CommandType.StoredProcedure, Requestno, TypeName, userid);

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
                        "Message", "toastr.error('NG, Check again!');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(),
                    "Message", "toastr.error('NG, data null!');", true);
            }
        }

        public void Exporttranfer99(object sender, EventArgs e)
        {
            string Requestno = RequestNo4.Text;
            string TypeName = TypeForm4.Text;
            string userid = Session["UserName"].ToString();

            if (Requestno != "" && TypeName != "")
            {
                DataTable dtexport = DataConn.StoreFillDS("Export_Tranfer_99",
                    System.Data.CommandType.StoredProcedure, Requestno, TypeName, userid);

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

        public void ExportOutscrap(object sender, EventArgs e)
        {
            string Requestno = RequestNo5.Text;
            string TypeName = TypeForm5.Text;
            string userid = Session["UserName"].ToString();

            if (Requestno != "" && TypeName != "")
            {
                DataTable dtexport = DataConn.StoreFillDS("Export_Outscrap",
                    System.Data.CommandType.StoredProcedure, Requestno, TypeName, userid);

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
                string sql = $@"SELECT Material, Plant, VendorCode, 
                        CostCenter, Sloc, IssueQty, UnitPrice_ST, Amount_ST,DocumentNo,NameSacntion
                        FROM {tableName} WHERE RequestNo = @RequestNo";

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
                            NameSacntion = dr["NameSacntion"].ToString()
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


    }
}