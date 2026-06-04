using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using DocumentFormat.OpenXml.Spreadsheet;

//using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Wordprocessing;
using MATERIAL_IN_OUT.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MATERIAL_IN_OUT
{
    public partial class frmSlocMCS : System.Web.UI.Page
    {
        public DataTable dt = new DataTable();
        public DataTable dt_update = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Date1.Value = DateTime.Now.ToString("yyyy-MM-dd");
            Date1.Value = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            ngaychiid.Value = DateTime.Now.ToString("yyyy-MM-dd");

            string slocname = filterSloc.Value;

            if (!IsPostBack)
            {
                dt = DataConn.StoreFillDS("Select_mater_slocMCS", System.Data.CommandType.StoredProcedure, slocname);
            }
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            //string _fromdate = Request.Form[Date1.UniqueID];
            //string _todate = Request.Form[ngaychiid.UniqueID];

            string slocname = filterSloc.Value;

            dt = DataConn.StoreFillDS("Select_mater_slocMCS", System.Data.CommandType.StoredProcedure, slocname);

        }

        protected void Sys_SlocMCS_Click(object sender, EventArgs e) 
        {
            //update thong tin sloc tu kho MCS sang he thong Issue OUT
            DataTable dt_sloc_mcs = new DataTable();
            DataTable dt_update = new DataTable();
            //da chan cac kho SMT & PMD khong thuoc kho MCS ==> not in ('PMD','SMT') 
            dt_sloc_mcs = DataConn.StoreFillDS_MCS("Select_mater_slocMCS_SWMS", System.Data.CommandType.StoredProcedure);
            if (dt_sloc_mcs.Rows.Count > 0)
            {                
                for (int i = 0; i < dt_sloc_mcs.Rows.Count; i++)
                {
                    string plant = dt_sloc_mcs.Rows[i]["Plant"].ToString();
                    string Category = dt_sloc_mcs.Rows[i]["Category"].ToString();
                    string Sloc = dt_sloc_mcs.Rows[i]["Sloc"].ToString();
                    string Type = dt_sloc_mcs.Rows[i]["Type"].ToString();
                    string Address = dt_sloc_mcs.Rows[i]["Address"].ToString();
                    string Description = dt_sloc_mcs.Rows[i]["Description"].ToString();
                    string CreatedBy = "SYS";
                    //update kho neu chua co
                    //neu type=SMT khong thuoc kho MCS thi sua trong sotre****
                    dt_update = DataConn.StoreFillDS("Update_mater_slocMCS", System.Data.CommandType.StoredProcedure, plant, Category, Sloc, Type, Address, Description, CreatedBy);                    
                }
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Update Sloc Sucess !!!');", true);
            }
            else 
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Data NULL!'); ", true);
            }

            //load grid
            string slocname = filterSloc.Value;
            dt = DataConn.StoreFillDS("Select_mater_slocMCS", System.Data.CommandType.StoredProcedure, slocname);

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

                    DataConn.SetTargetExcel(Server, FileUpload.FileName);
                    string excelConnStr = DataConn.target_excel;

                    OleDbConnection excelConn = null;
                    OleDbDataReader objBulkReader = null;

                    try
                    {                        
                        DataTable dt_checkupload = new DataTable();
                        DataTable dt_insert = new DataTable();
                        DataTable dt_new = new DataTable();
                        int countlap = 0;
                        int countok = 0;
                       
                        // Open connection to Excel file.
                        excelConn = new OleDbConnection(excelConnStr);
                        excelConn.Open();

                        // Get data from Excel sheet.
                        OleDbCommand objOleDB = new OleDbCommand("SELECT * FROM [Sheet1$]", excelConn);
                        objBulkReader = objOleDB.ExecuteReader();

                        // Check if there is data to process.
                        if (objBulkReader.HasRows)
                        {
                            // Prepare DataTable to hold Excel data.
                            DataTable dtExcelData = new DataTable();
                            dtExcelData.Load(objBulkReader); // Load data into DataTable.

                            // Check if the data already exists in database.
                            //string ngaysanxuat = dtExcelData.Rows[2][3].ToString();
                            string Plant = "";
                            string Category = "";
                            string Sloc = "";
                            string Type = "";
                            string diachi = "";
                            string Description = "";
                            string CreatedBy = "";
                                                       
                            for (int i = 0; i < dtExcelData.Rows.Count; i++)
                            {                               
                                //kiem tra xem tren csdl co chua? chua co thi moi them
                                Plant = dtExcelData.Rows[i][1].ToString();
                                Category = dtExcelData.Rows[i][2].ToString();
                                Sloc = dtExcelData.Rows[i][3].ToString();
                                Type = dtExcelData.Rows[i][4].ToString();
                                diachi = dtExcelData.Rows[i][5].ToString();
                                Description = dtExcelData.Rows[i][6].ToString();
                                CreatedBy = "upload";
                                
                                dt_insert = DataConn.StoreFillDS("Insert_master_slocMCS", System.Data.CommandType.StoredProcedure, Plant, Category, Sloc, Type, diachi, Description, CreatedBy);
                                if (dt_insert.Rows[0][0].ToString() == "1")
                                {
                                    //thanh cong
                                    countok = countok + 1;
                                }
                                else 
                                {
                                    countlap = countlap + 1;
                                }
                                
                                // Dừng vòng lặp khi các cột STT, Plan va sloc đều rỗng
                                if (dtExcelData.Rows[i][0].ToString()=="" && dtExcelData.Rows[i][1].ToString() == "" && dtExcelData.Rows[i][3].ToString() == "")
                                {
                                    break;
                                }
                            }
                            
                            if (countok > 0)
                            {
                                lblConfirm.Text = "DATA IMPORTED SUCCESSFULLY, row insert : " + countok + "/ ban ghi trung: "+ countlap ;
                                lblConfirm.Attributes.Add("style", "color:green");
                            }
                            else
                            {
                                //lblConfirm.Text = "DATA IMPORTED SUCCESSFULLY.";
                                //lblConfirm.Attributes.Add("style", "color:green");
                            }

                            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Thành công!');", true);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('OK, Upload thành công!');", true);

                            string slocname = filterSloc.Value;
                            dt = DataConn.StoreFillDS("Select_mater_slocMCS", System.Data.CommandType.StoredProcedure, slocname);


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
                        //dt_phanca = Db_connect.StoreFillDS("HR_List_phanca", System.Data.CommandType.StoredProcedure);
                    }
                }
            }
        }

        protected void btnDownloadClick(Object sender, EventArgs e)
        {
            try
            {
                string fileName = "MaterSlocMCS.xlsx";
                string fileExtension = ".xlsx";

                // Set Response.ContentType
                Response.ContentType = GetContentType(fileExtension);

                // Append header
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

                // Write the file to the Response
                Response.TransmitFile(Server.MapPath("~/Template/" + fileName));
                //Response.TransmitFile(Server.MapPath("~/Uploads/" + fileName));
                Response.End();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetContentType(string fileExtension)
        {
            if (string.IsNullOrEmpty(fileExtension))
                return string.Empty;

            string contentType = string.Empty;
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                    contentType = "text/HTML";
                    break;
                case ".csv":
                case ".txt":
                    contentType = "text/plain";
                    break;

                case ".doc":
                case ".rtf":
                case ".docx":
                    contentType = "Application/msword";
                    break;

                case ".xls":
                case ".xlsx":
                    contentType = "Application/x-msexcel";
                    break;

                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;

                case ".gif":
                    contentType = "image/GIF";
                    break;

                case ".pdf":
                    contentType = "application/pdf";
                    break;
            }
            return contentType;
        }

        public void UpdateDocumentNo(object sender, EventArgs e)
        {
            string idno = txtid.Text;
            string sloc = Slocid.Text;
            string plant = Plantid.Text;
            string userid = Session["UserName"].ToString();

            string slocname = "";

            if (sloc != "" )
            {
                dt_update = DataConn.StoreFillDS("Update_Sloc_MCS", System.Data.CommandType.StoredProcedure, idno, sloc, plant, userid);

                if (dt_update.Rows[0][0].ToString() == "1")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Update Document Sucess !!!');", true);
                    dt = DataConn.StoreFillDS("Select_mater_slocMCS", System.Data.CommandType.StoredProcedure, slocname);
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

        public void DeleteslocMCS(object sender, EventArgs e)
        {
            string idno = txtid7.Text;            
            string userid = Session["UserName"].ToString();

            string slocname = "";

            dt_update = DataConn.StoreFillDS("Delete_Sloc_MCS", System.Data.CommandType.StoredProcedure, idno, userid);

            if (dt_update.Rows[0][0].ToString() == "1")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Delete Sloc Sucess !!!');", true);
                dt = DataConn.StoreFillDS("Select_mater_slocMCS", System.Data.CommandType.StoredProcedure, slocname);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Check again!'); ", true);
            }
        }

        public void themhanghoa(object sender, EventArgs e)
        {
            string userid = Session["UserName"].ToString();
            string sloc = idsloc.Text;
            string plant = idplan.Text;
            string slocname = "";


            DataTable dtinsert = new DataTable();
            dtinsert = DataConn.StoreFillDS("Insert_sloc_MCS", System.Data.CommandType.StoredProcedure, userid, sloc, plant);
            if (dtinsert.Rows[0][0].ToString() == "1")
            {
                dt = DataConn.StoreFillDS("Select_mater_slocMCS", System.Data.CommandType.StoredProcedure, slocname);
                // dt_leadtime = DataConn.StoreFillDS("Select_mater_listime_cate", System.Data.CommandType.StoredProcedure, cateid_);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Sloc da ton tai roi, kiem tra lai!'); ", true);
            }

        }


    }
}