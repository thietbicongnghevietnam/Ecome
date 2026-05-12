using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace MATERIAL_IN_OUT
{
    public partial class import_excel_test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImportFromExcel(object sender, EventArgs e)
        {
            if (!FileUpload.HasFile || FileUpload.PostedFile.ContentLength == 0)
                return;

            string excelPath = Server.MapPath("~/") + FileUpload.FileName;
            FileUpload.SaveAs(excelPath);

            //string excelConnStr =
            //    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath +
            //    ";Extended Properties='Excel 12.0;HDR=YES;'";
            string excelConnStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                       Server.MapPath(".") + "\\" + FileUpload.FileName +
                       @";Extended Properties=""Excel 12.0;HDR=YES;IMEX=1"";";

            OleDbConnection excelConn = null;
            OleDbDataReader reader = null;

            try
            {
                excelConn = new OleDbConnection(excelConnStr);
                excelConn.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", excelConn);
                reader = cmd.ExecuteReader();

                DataTable dtExcel = new DataTable();
                dtExcel.Load(reader);

                // ===== DATATABLE BULK =====
                DataTable dtBulk = new DataTable();
                dtBulk.Columns.Add("STR_MEMBER_CODE", typeof(string));
                dtBulk.Columns.Add("TMR_DATE", typeof(DateTime));
                dtBulk.Columns.Add("STR_PROCESS", typeof(string));
                dtBulk.Columns.Add("STR_PROCESS_FACTORY", typeof(string));
                dtBulk.Columns.Add("STR_SERIAL", typeof(string));
                dtBulk.Columns.Add("COD_SCALE_NO", typeof(string));
                dtBulk.Columns.Add("bShrinked", typeof(bool));
                dtBulk.Columns.Add("STATUS", typeof(byte));
                dtBulk.Columns.Add("OUTER_CARTON", typeof(string));
                dtBulk.Columns.Add("SERIAL_OUTER", typeof(string));
                dtBulk.Columns.Add("LOT_NAME", typeof(string));
                dtBulk.Columns.Add("STATUS_FA", typeof(byte));
                dtBulk.Columns.Add("STATUS_BF", typeof(string));
                dtBulk.Columns.Add("DATEINSERT", typeof(DateTime));

                string user = Session["UserName"].ToString();

                for (int i = 0; i < dtExcel.Rows.Count; i++)
                {
                    if (string.IsNullOrWhiteSpace(dtExcel.Rows[i][1].ToString()) &&
                        string.IsNullOrWhiteSpace(dtExcel.Rows[i][5].ToString()))
                        break;

                    DataRow dr = dtBulk.NewRow();
                    dr["STR_MEMBER_CODE"] = dtExcel.Rows[i][0]?.ToString() ?? "";
                    //dr["TMR_DATE"] = dtExcel.Rows[i][1].ToString();
                    DateTime tmrDate;
                    if (DateTime.TryParse(dtExcel.Rows[i][1].ToString(), out tmrDate))
                    {
                        dr["TMR_DATE"] = tmrDate;
                    }
                    else
                    {
                        dr["TMR_DATE"] = DBNull.Value; // hoặc DateTime.Now tùy bạn
                    }
                    dr["STR_PROCESS"] = dtExcel.Rows[i][2].ToString();
                    dr["STR_PROCESS_FACTORY"] = dtExcel.Rows[i][3].ToString();
                    dr["STR_SERIAL"] = dtExcel.Rows[i][4].ToString();
                    dr["COD_SCALE_NO"] = dtExcel.Rows[i][5].ToString();
                    //dr["bShrinked"] = dtExcel.Rows[i][6].ToString();
                    string shrinkValue = dtExcel.Rows[i][6].ToString().Trim();
                    if (shrinkValue == "1" || shrinkValue.ToLower() == "true")
                    {
                        dr["bShrinked"] = true;
                    }
                    else if (shrinkValue == "0" || shrinkValue.ToLower() == "false")
                    {
                        dr["bShrinked"] = false;
                    }
                    else
                    {
                        dr["bShrinked"] = DBNull.Value;
                    }

                    //dr["STATUS"] = dtExcel.Rows[i][7].ToString();
                    if (byte.TryParse(dtExcel.Rows[i][7].ToString(), out byte status))
                    {
                        dr["STATUS"] = status;
                    }
                    else
                    {
                        dr["STATUS"] = DBNull.Value;
                    }

                    dr["OUTER_CARTON"] = dtExcel.Rows[i][8].ToString();
                    dr["SERIAL_OUTER"] = dtExcel.Rows[i][9].ToString();
                    dr["LOT_NAME"] = dtExcel.Rows[i][10].ToString();
                    //dr["STATUS_FA"] = dtExcel.Rows[i][11].ToString();
                    if (byte.TryParse(dtExcel.Rows[i][11].ToString(), out byte statusFA))
                    {
                        dr["STATUS_FA"] = statusFA;
                    }
                    else
                    {
                        dr["STATUS_FA"] = DBNull.Value;
                    }

                    dr["STATUS_BF"] = dtExcel.Rows[i][12].ToString();
                    //dr["DATEINSERT"] = dtExcel.Rows[i][12].ToString();
                    DateTime dateInsert;
                    if (DateTime.TryParse(dtExcel.Rows[i][13].ToString(), out dateInsert))
                    {
                        dr["DATEINSERT"] = dateInsert;
                    }
                    else
                    {
                        dr["DATEINSERT"] = DBNull.Value;
                    }

                    dtBulk.Rows.Add(dr);
                }

                //source = @"Data Source=192.168.128.1;Initial Catalog=Issue_MaterialInOut;User ID=sa;Password=Psnvdb2013";
                // ===== SQL BULK =====
                using (SqlConnection conn = new SqlConnection(
                       ConfigurationManager.ConnectionStrings["FOSS1"].ConnectionString))
                {
                    conn.Open();

                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1️⃣ XÓA DỮ LIỆU CŨ
                            //SqlCommand cmdTruncate = new SqlCommand(
                            //    "TRUNCATE TABLE dbo.TBLPRODUCT_MW_BK2026", conn, tran);
                            //cmdTruncate.ExecuteNonQuery();

                            // 2️⃣ BULK INSERT
                            using (SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran))
                            {
                                bulk.DestinationTableName = "dbo.TBLPRODUCT_MW_BK2026";

                                bulk.ColumnMappings.Add("STR_MEMBER_CODE", "STR_MEMBER_CODE");
                                bulk.ColumnMappings.Add("TMR_DATE", "TMR_DATE");
                                bulk.ColumnMappings.Add("STR_PROCESS", "STR_PROCESS");
                                bulk.ColumnMappings.Add("STR_PROCESS_FACTORY", "STR_PROCESS_FACTORY");
                                bulk.ColumnMappings.Add("STR_SERIAL", "STR_SERIAL");
                                bulk.ColumnMappings.Add("COD_SCALE_NO", "COD_SCALE_NO");
                                bulk.ColumnMappings.Add("bShrinked", "bShrinked");
                                bulk.ColumnMappings.Add("STATUS", "STATUS");
                                bulk.ColumnMappings.Add("OUTER_CARTON", "OUTER_CARTON");
                                bulk.ColumnMappings.Add("SERIAL_OUTER", "SERIAL_OUTER");
                                bulk.ColumnMappings.Add("LOT_NAME", "LOT_NAME");
                                bulk.ColumnMappings.Add("STATUS_FA", "STATUS_FA");
                                bulk.ColumnMappings.Add("STATUS_BF", "STATUS_BF");
                                bulk.ColumnMappings.Add("DATEINSERT", "DATEINSERT");

                                bulk.BatchSize = 5000;
                                bulk.BulkCopyTimeout = 0;

                                bulk.WriteToServer(dtBulk);
                            }

                            tran.Commit();
                        }
                        catch
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }

                lblConfirm.Text = "IMPORT THÀNH CÔNG: " + dtBulk.Rows.Count + " dòng";
                lblConfirm.Attributes.Add("style", "color:green");
            }
            catch (Exception ex)
            {
                lblConfirm.Text = ex.Message;
                lblConfirm.Attributes.Add("style", "color:red");
            }
            finally
            {
                if (reader != null) reader.Close();
                if (excelConn != null) excelConn.Close();
                if (File.Exists(excelPath)) File.Delete(excelPath);
            }
        }


    }
}