using MATERIAL_IN_OUT.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

        protected void ImportFromExcel(object sender, EventArgs e)
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