using DocumentFormat.OpenXml.Bibliography;
using MATERIAL_IN_OUT.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MATERIAL_IN_OUT
{
    public partial class ControlPriceSAP2 : System.Web.UI.Page
    {
        public DataTable dt_Price = new DataTable();
        public DataTable dt_model = new DataTable();
        public DataTable dtplan = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Date1.Value = DateTime.Now.ToString("yyyy-MM-dd");
                ngaychiid.Value = DateTime.Now.ToString("yyyy-MM-dd");

                //string fromdate = txtDate.Text.ToString();
                //string todate = txtDate2.Text.ToString();

                dt_Price = DataConn.StoreFillDS("Select_PriceSAP_IssueOut", System.Data.CommandType.StoredProcedure);

                //danh sach plan
                dtplan = DataConn.StoreFillDS("pro_get_Plan", System.Data.CommandType.StoredProcedure);
                DataRow newRow1 = dtplan.NewRow();
                newRow1["Plant"] = "==Plan==";
                dtplan.Rows.InsertAt(newRow1, 0);
                dr_filter_plan.DataSource = dtplan;
                dr_filter_plan.DataBind();

                //dtcate = DataConn.StoreFillDS("pro_get_categogy_hangthieu", System.Data.CommandType.StoredProcedure);
                //DataRow newRow2 = dtcate.NewRow();
                //newRow2["cat"] = "==Categogy==";
                //dtcate.Rows.InsertAt(newRow2, 0);
                //dr_filter_Cate.DataSource = dtcate;
                //dr_filter_Cate.DataBind();

                //ExcelPackage.LicenseContext = LicenseContext.Commercial;
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            }
        }

        protected void Search_Date_Click2(object sender, EventArgs e)
        {
            //string _date = Request.Form[Date1.UniqueID];
            //string _todate = Request.Form[ngaychiid.UniqueID];
            string Plant = dr_filter_plan.Text;
            string material = filtermaterial.Value;
            if (dr_filter_plan.Text != "==Plan==")
            {
                dt_Price = DataConn.StoreFillDS("Select_PriceSAP_IssueOut_Fill", System.Data.CommandType.StoredProcedure, Plant, material);
            }
            else 
            {
                dt_Price = DataConn.StoreFillDS("Select_PriceSAP_IssueOut", System.Data.CommandType.StoredProcedure);
            }

            

        }

        public static string SanitizeSheetName(string sheetName)
        {
            // Loại bỏ các ký tự không hợp lệ cho tên sheet trong Excel
            // Các ký tự không hợp lệ bao gồm: :, \, /, ?, *, [, ], và dấu cách đầu hoặc cuối
            string pattern = @"[^a-zA-Z0-9\s]";  // Giữ lại chữ cái, số và dấu cách
            sheetName = Regex.Replace(sheetName, pattern, "");

            // Cắt tên sheet nếu quá dài (tối đa 31 ký tự)
            if (sheetName.Length > 31)
            {
                sheetName = sheetName.Substring(0, 31);
            }

            // Đảm bảo rằng tên sheet kết thúc với dấu $
            return sheetName;
        }

        private double SafeDouble(object value)
        {
            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
                return 0; // Hoặc giá trị mặc định
            double result;
            return double.TryParse(value.ToString(), out result) ? result : 0;
        }

        private decimal SafeDecimal(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0;

            decimal result;
            if (decimal.TryParse(value.ToString(), out result))
                return result;

            return 0;
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
                dtBulk.Columns.Add("Plant", typeof(string));
                dtBulk.Columns.Add("Component", typeof(string));
                dtBulk.Columns.Add("Material_Type", typeof(string));
                dtBulk.Columns.Add("Price_STD", typeof(decimal));
                dtBulk.Columns.Add("Price2", typeof(double));
                dtBulk.Columns.Add("Qty", typeof(double));
                dtBulk.Columns.Add("StockQty", typeof(double));
                dtBulk.Columns.Add("Sloc", typeof(string));
                dtBulk.Columns.Add("DateInsert", typeof(DateTime));
                dtBulk.Columns.Add("UserUpdate", typeof(string));
                dtBulk.Columns.Add("Updatetime", typeof(DateTime));

                string user = Session["UserName"].ToString();

                for (int i = 0; i < dtExcel.Rows.Count; i++)
                {
                    if (string.IsNullOrWhiteSpace(dtExcel.Rows[i][1].ToString()) &&
                        string.IsNullOrWhiteSpace(dtExcel.Rows[i][5].ToString()))
                        break;

                    DataRow dr = dtBulk.NewRow();
                    dr["Plant"] = dtExcel.Rows[i][1]?.ToString() ?? "";
                    dr["Component"] = dtExcel.Rows[i][5].ToString();
                    dr["Material_Type"] = dtExcel.Rows[i][4].ToString();
                    //dr["Price_STD"] = SafeDouble(dtExcel.Rows[i][30]);
                    dr["Price_STD"] = SafeDecimal(dtExcel.Rows[i][30]);
                    dr["Price2"] = 0;
                    dr["Qty"] = 0;
                    //dr["StockQty"] = SafeDouble(dtExcel.Rows[i][41]);  //request Ngoc ACC tong ton kho = 4 cot + lai voi nhau
                    dr["StockQty"] = SafeDouble(dtExcel.Rows[i][41]) + SafeDouble(dtExcel.Rows[i][42]) + SafeDouble(dtExcel.Rows[i][43]) + SafeDouble(dtExcel.Rows[i][44]);

                    dr["Sloc"] = dtExcel.Rows[i][2].ToString();
                    dr["DateInsert"] = DateTime.Now;
                    dr["UserUpdate"] = user;
                    dr["Updatetime"] = DateTime.Now;
                    dtBulk.Rows.Add(dr);
                }

                //source = @"Data Source=192.168.128.1;Initial Catalog=Issue_MaterialInOut;User ID=sa;Password=Psnvdb2013";
                // ===== SQL BULK =====
                using (SqlConnection conn = new SqlConnection(
                       ConfigurationManager.ConnectionStrings["Issue_MaterialInOut"].ConnectionString))
                {
                    conn.Open();

                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1️⃣ XÓA DỮ LIỆU CŨ
                            SqlCommand cmdTruncate = new SqlCommand(
                                "TRUNCATE TABLE dbo.tbl_Material_PriceSAP", conn, tran);
                            cmdTruncate.ExecuteNonQuery();

                            // 2️⃣ BULK INSERT
                            using (SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran))
                            {
                                bulk.DestinationTableName = "dbo.tbl_Material_PriceSAP";

                                bulk.ColumnMappings.Add("Plant", "Plant");
                                bulk.ColumnMappings.Add("Component", "Component");
                                bulk.ColumnMappings.Add("Material_Type", "Material_Type");
                                bulk.ColumnMappings.Add("Price_STD", "Price_STD");
                                bulk.ColumnMappings.Add("Price2", "Price2");
                                bulk.ColumnMappings.Add("Qty", "Qty");
                                bulk.ColumnMappings.Add("StockQty", "StockQty");
                                bulk.ColumnMappings.Add("Sloc", "Sloc");
                                bulk.ColumnMappings.Add("DateInsert", "DateInsert");
                                bulk.ColumnMappings.Add("UserUpdate", "UserUpdate");
                                bulk.ColumnMappings.Add("Updatetime", "Updatetime");

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


        protected void ImportFromExcel1(object sender, EventArgs e)
        {
            //string checkupload = "0";
            DataTable dtcheck = new DataTable();
            // CHECK IF A FILE HAS BEEN SELECTED.
            if (FileUpload.HasFile)
            {
                if (FileUpload.PostedFile.ContentLength > 0)
                {
                    // Save the uploaded file to the server.
                    FileUpload.SaveAs(Server.MapPath(".") + "\\" + FileUpload.FileName);

                    // Set connection string with the Excel file.
                    string excelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" +
                                          Server.MapPath(".") + "\\" + FileUpload.FileName +
                                          "; Extended Properties=Excel 12.0;";

                    OleDbConnection excelConn = null;
                    OleDbDataReader objBulkReader = null;

                    try
                    {
                        //string tenbophan = dr_filter_bophan.Text;
                        //string phongban = dr_filter_phongban.Text;
                        DataTable dt_checkupload = new DataTable();
                        //DataTable dt_new = new DataTable();
                        int countlap = 0;
                        int countinsert = 0;

                        //dt_new.Columns.Add("id", typeof(Int32));
                        //dt_new.Columns.Add("Productdate", typeof(String));                       

                        // Open connection to Excel file.
                        excelConn = new OleDbConnection(excelConnStr);
                        excelConn.Open();

                        // Lấy danh sách các sheet trong Excel
                        DataTable sheets = excelConn.GetSchema("Tables");

                        // Lấy tên sheet đầu tiên (vì chỉ có một sheet)
                        //string sheetName = sheets.Rows[0]["TABLE_NAME"].ToString();
                        //Console.WriteLine("Tên sheet: " + sheetName);

                        string sheetName = "Sheet1$";

                        // Xử lý tên sheet (nếu có ký tự đặc biệt)
                        string sanitizedSheetName = SanitizeSheetName(sheetName);
                        // Tạo câu truy vấn SQL với tên sheet đã xử lý
                        OleDbCommand objOleDB = new OleDbCommand($"SELECT * FROM [{sanitizedSheetName}$]", excelConn);

                        // Get data from Excel sheet.
                        //OleDbCommand objOleDB = new OleDbCommand("SELECT * FROM [Sheet1$]", excelConn);
                        //OleDbCommand objOleDB = new OleDbCommand("SELECT * FROM [{sanitizedSheetName}$]", excelConn);                       

                        objBulkReader = objOleDB.ExecuteReader();

                        // Check if there is data to process.
                        if (objBulkReader.HasRows)
                        {
                            // Prepare DataTable to hold Excel data.
                            DataTable dtExcelData = new DataTable();
                            dtExcelData.Load(objBulkReader); // Load data into DataTable.

                            //SELECT CONVERT(varchar, CONVERT(DATE, Productdate, 103), 23) FROM [OQC].[dbo].[tblDailyInspection]  ==> convert tren sql
                            //select convert(date, cast(Shipmentdate as datetime)) FROM[OQC].[dbo].[tblDailyInspection]      ==> convert tren sql                          
                            string Plant = "";
                            string Component = "";
                            float Price_STD = 0;
                            float price2 = 0;
                            string sloc = "";
                            string type_material = "";
                            //string userid = "2012757";
                            string userid = Session["UserName"].ToString();
                           
                            for (int i = 0; i < dtExcelData.Rows.Count; i++)
                            {

                                Plant = dtExcelData.Rows[i][1].ToString();
                                Component = dtExcelData.Rows[i][5].ToString();
                                Price_STD = float.Parse(dtExcelData.Rows[i][30].ToString());
                                price2 = float.Parse(dtExcelData.Rows[i][41].ToString());  // gia tri ton kho 
                                sloc = dtExcelData.Rows[i][2].ToString();
                                type_material = dtExcelData.Rows[i][4].ToString();

                                //kiem tra xem tren csdl co chua? chua co thi moi them
                                dt_checkupload = DataConn.StoreFillDS("Update_price_SAP", System.Data.CommandType.StoredProcedure, Plant, Component, Price_STD, price2, userid, sloc, type_material);
                                if (dt_checkupload.Rows[0][0].ToString() == "1")
                                {
                                    countlap = countlap + 1;
                                }
                                else
                                {
                                    countinsert = countinsert + 1;
                                }

                                // Dừng vòng lặp khi các cột cần kiểm tra (cột 0, 2, 3) đều rỗng
                                if (dtExcelData.Rows[i][0].ToString() == "" && dtExcelData.Rows[i][2].ToString() == "" && dtExcelData.Rows[i][30].ToString() == "")
                                {
                                    break;
                                }
                            }

                            if (countlap > 0)
                            {
                                lblConfirm.Text = "DATA IMPORTED SUCCESSFULLY! Tong ban ghi update : " + countlap;
                                lblConfirm.Attributes.Add("style", "color:green");
                            }
                            else
                            {
                                //lblConfirm.Text = "Not update.";
                                //lblConfirm.Attributes.Add("style", "color:red");
                                lblConfirm.Text = "DATA IMPORTED SUCCESSFULLY! Tong ban ghi insert : " + countinsert;
                                lblConfirm.Attributes.Add("style", "color:green");
                            }

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('OK, Upload thành công!');", true);
                            dt_Price = DataConn.StoreFillDS("Select_PriceSAP_IssueOut", System.Data.CommandType.StoredProcedure);

                        }

                        // Close the reader and Excel connection.
                        //objBulkReader.Close();
                        //excelConn.Close();
                    }
                    catch (Exception ex)
                    {
                        lblConfirm.Text = ex.Message;
                        lblConfirm.Attributes.Add("style", "color:red");
                    }
                    finally
                    {
                        // Close and dispose objects.
                        if (objBulkReader != null && !objBulkReader.IsClosed)
                        {
                            objBulkReader.Close();
                        }
                        if (excelConn != null && excelConn.State == ConnectionState.Open)
                        {
                            excelConn.Close();
                        }

                        // Delete the uploaded file (optional).
                        File.Delete(Server.MapPath(".") + "\\" + FileUpload.FileName);

                        // Reload grid or perform other necessary actions.
                        dt_Price = DataConn.StoreFillDS("Select_PriceSAP_IssueOut", System.Data.CommandType.StoredProcedure);
                    }
                }
            }
        }

    }
}