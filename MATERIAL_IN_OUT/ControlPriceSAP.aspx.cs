using MATERIAL_IN_OUT.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class ControlPriceSAP : System.Web.UI.Page
    {
        public DataTable dt_Price = new DataTable();
        public DataTable dt_model = new DataTable();
        public DataTable dtcate = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtDate2.Text = DateTime.Now.ToString("yyyy-MM-dd");

                string fromdate = txtDate.Text.ToString();
                string todate = txtDate2.Text.ToString();

                dt_Price = DataConn.StoreFillDS("Select_PriceSAP_IssueOut", System.Data.CommandType.StoredProcedure);

                //dt_model = DataConn.StoreFillDS("Get_model_pickup", System.Data.CommandType.StoredProcedure, fromdate, todate);
                //DataRow newRow1 = dt_model.NewRow();
                //newRow1["STR_PROCESS_FACTORY"] = "==select==";
                //dt_model.Rows.InsertAt(newRow1, 0);
                //dr_filter_model.DataSource = dt_model;
                //dr_filter_model.DataBind();

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

        protected void dr_filter_Plan_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy giá trị đã chọn
            //string category = dr_filter_Cate.SelectedValue;
            //string tungay = Request.Form[txtDate.UniqueID];
            //string denngay = Request.Form[txtDate2.UniqueID];

            //if (category == "==Categogy==")
            //{
            //    dt_Price = DataConn.StoreFillDS("Select_Hangthieu", System.Data.CommandType.StoredProcedure);
            //}
            //else
            //{
            //    dt_Price = DataConn.StoreFillDS("Select_Hangthieu_loctheocate", System.Data.CommandType.StoredProcedure, category, tungay, denngay);
            //}
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            //string _date = Request.Form[txtDate.UniqueID];
            //string _todate = Request.Form[txtDate2.UniqueID];

            //dt_Price = DataConn.StoreFillDS("Select_Hangthieu_loctheongay", System.Data.CommandType.StoredProcedure, _date, _todate);
        }

        public void btnXuatExcel_Click(object sender, EventArgs e)
        {
            //string tungay = Request.Form[txtDate.UniqueID];
            //string denngay = Request.Form[txtDate2.UniqueID];
            //string cate_ = dr_filter_Cate.Text.ToString();
            ////string localPath = "DailyReportForm.xlsx";
            //string relativePath = "maudanhsachhangthieu.xlsx"; // Thay đổi theo cấu trúc thư mục của bạn
            //                                                   // Đường dẫn tới file Excel gốc            
            //string localPath = Server.MapPath(relativePath);

            //// Đường dẫn để lưu file Excel mới
            //string newFileName = "Hangthieu.xlsx"; // Tên file mới
            //string newFilePath = Server.MapPath("Textfile/" + newFileName); // Đường dẫn đầy đủ

            //// Gọi phương thức để xử lý file Excel và lưu file mới
            //ProcessExcelFile(localPath, newFilePath, tungay, denngay, cate_);

            //// Tải xuống file mới
            //DownloadFile(newFilePath, newFileName);
        }

        static void ProcessExcelFile(string filePath, string newFilePath, string tungay, string denngay, string category)
        {
            //FileInfo fileInfo = new FileInfo(filePath);

            //// Đảm bảo file tồn tại
            //if (!fileInfo.Exists)
            //{
            //    throw new FileNotFoundException("File không tồn tại", filePath);
            //}

            //// Tạo file mới để lưu kết quả
            //FileInfo newFileInfo = new FileInfo(newFilePath);

            //DataTable dtexcel = new DataTable();

            //if (category == "==Categogy==")
            //{
            //    dtexcel = DataConn.StoreFillDS("Select_Hangthieu_loctheongay", System.Data.CommandType.StoredProcedure, tungay, denngay);
            //}
            //else
            //{
            //    //loc theo category
            //    dtexcel = DataConn.StoreFillDS("Select_Hangthieu_loctheocate", System.Data.CommandType.StoredProcedure, category, tungay, denngay);
            //}

            ////dtexcel = DataConn.StoreFillDS("Select_Hangthieu_loctheongay", System.Data.CommandType.StoredProcedure, tungay, denngay);

            //using (var package = new ExcelPackage(fileInfo))
            //{
            //    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            //    //worksheet.Cells["D5"].Value = tungay;// "Thông tin mới";              
            //    int row = 2;
            //    int i = 0;
            //    DateTime currentDate = DateTime.Today;
            //    // Lấy ngày hiện tại +1, +2, +3, ...
            //    DateTime datePlus1 = currentDate.AddDays(1);
            //    DateTime datePlus2 = currentDate.AddDays(2);
            //    DateTime datePlus3 = currentDate.AddDays(3);
            //    DateTime datePlus4 = currentDate.AddDays(4);
            //    DateTime datePlus5 = currentDate.AddDays(5);

            //    // Ghi ngày vào các ô trong Excel (chú ý rằng chỉ số cột bắt đầu từ 1)
            //    worksheet.Cells[1, 15].Value = currentDate.ToString("dd");
            //    worksheet.Cells[1, 16].Value = datePlus1.ToString("dd");
            //    worksheet.Cells[1, 17].Value = datePlus3.ToString("dd");
            //    worksheet.Cells[1, 18].Value = datePlus4.ToString("dd");
            //    worksheet.Cells[1, 19].Value = datePlus5.ToString("dd");

            //    foreach (DataRow dataRow in dtexcel.Rows)
            //    {
            //        i++;
            //        worksheet.Cells[row, 1].Value = dataRow["cat"];
            //        worksheet.Cells[row, 2].Value = dataRow["Model"];
            //        worksheet.Cells[row, 3].Value = dataRow["inventory"];
            //        worksheet.Cells[row, 4].Value = dataRow["si"];
            //        worksheet.Cells[row, 5].Value = dataRow["kho"]; //
            //        worksheet.Cells[row, 6].Value = dataRow["ngh"];
            //        worksheet.Cells[row, 7].Value = "";  //gio can giao
            //        worksheet.Cells[row, 8].Value = dataRow["shipmode"] != DBNull.Value ? dataRow["shipmode"] : "";
            //        worksheet.Cells[row, 9].Value = "";
            //        worksheet.Cells[row, 10].Value = ""; //OQC confirm                   
            //        worksheet.Cells[row, 11].Value = dataRow["StockFA"];
            //        worksheet.Cells[row, 12].Value = dataRow["DiffFA"];
            //        worksheet.Cells[row, 13].Value = dataRow["Balance"];
            //        worksheet.Cells[row, 14].Value = dataRow["FAconfirm"];

            //        worksheet.Cells[row, 15].Value = dataRow["Ngay1"];
            //        worksheet.Cells[row, 16].Value = dataRow["Ngay2"];
            //        worksheet.Cells[row, 17].Value = dataRow["Ngay3"];
            //        worksheet.Cells[row, 18].Value = dataRow["Ngay4"];
            //        worksheet.Cells[row, 19].Value = dataRow["Ngay5"];



            //        if (dataRow["inventory"] != DBNull.Value)
            //        {
            //            decimal inventoryValue;
            //            // Kiểm tra nếu giá trị có thể chuyển sang decimal
            //            if (Decimal.TryParse(dataRow["inventory"].ToString(), out inventoryValue))
            //            {
            //                worksheet.Cells[row, 3].Value = inventoryValue;  // Đặt giá trị vào cột inventory
            //                worksheet.Cells[row, 3].Style.Numberformat.Format = "#,##0";  // Định dạng số (có thể dùng "#,##0.00" nếu bạn muốn số thập phân)
            //            }
            //            else
            //            {
            //                worksheet.Cells[row, 3].Value = "";  // Nếu không phải số, đặt giá trị là rỗng
            //            }
            //        }
            //        else
            //        {
            //            worksheet.Cells[row, 3].Value = "";  // Nếu cột inventory là DBNull, đặt giá trị rỗng
            //        }

            //        row++;
            //    }
            //    // Lưu vào file mới
            //    package.SaveAs(newFileInfo);
            //}
        }

        private void DownloadFile(string filePath, string fileName)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (fileInfo.Exists)
            {
                // Đặt các header cho tải xuống
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.AddHeader("Content-Length", fileInfo.Length.ToString());

                // Ghi nội dung file vào response
                Response.WriteFile(fileInfo.FullName);
                Response.End();
            }
            else
            {
                Response.Write("File không tồn tại.");
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
                        string sheetName = sheets.Rows[0]["TABLE_NAME"].ToString();
                        Console.WriteLine("Tên sheet: " + sheetName);

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
                            string userid = "2012757";
                          
                            for (int i = 0; i < dtExcelData.Rows.Count; i++)
                            {

                                Plant = dtExcelData.Rows[i][1].ToString();
                                Component = dtExcelData.Rows[i][5].ToString();
                                Price_STD = float.Parse(dtExcelData.Rows[i][30].ToString());
                                price2 = float.Parse(dtExcelData.Rows[i][41].ToString());
                                               
                                //kiem tra xem tren csdl co chua? chua co thi moi them
                                dt_checkupload = DataConn.StoreFillDS("Update_price_SAP", System.Data.CommandType.StoredProcedure, Plant, Component, Price_STD, price2, userid);
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