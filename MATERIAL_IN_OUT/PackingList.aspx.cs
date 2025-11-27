using ClosedXML.Excel;
using MATERIAL_IN_OUT.AppCode;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MATERIAL_IN_OUT
{
    public partial class PackingList : System.Web.UI.Page
    {
        public DataTable dtPackingList = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string invoice = Request.QueryString["Invoice"].ToString();
                LoadData(invoice);
            }
        }
        public void LoadData(string Invoice)
        {
            string StrPlant = "";
            dtPackingList = DataConn.FillStore("SP_Invoice_DetailPackingListPrint", CommandType.StoredProcedure, Invoice);
            rptData.DataSource = dtPackingList;
            rptData.DataBind();

            if (dtPackingList.Rows.Count > 0)
            {
                //lblvendor_To.Text = dtPackingList.Rows[0]["Vendor_BillTo"].ToString();
                //lblVendorName.Text = dtPackingList.Rows[0]["VendorName_BillTo"].ToString();
                //lblVendorAddress.Text = dtPackingList.Rows[0]["Vendor_AddressTo"].ToString();
                //lblVendorPICTo.Text = dtPackingList.Rows[0]["Vendor_PIC_To"].ToString();
                //lblVendorNameShip.Text = dtPackingList.Rows[0]["Vendor_PIC_To"].ToString();

                //lblVendorCodeShip.Text = dtPackingList.Rows[0]["Ship_To_PartyName"].ToString();
                //lblVendorNameShip.Text = dtPackingList.Rows[0]["Address_ShipParty"].ToString();
                //lblVendorPICShip.Text = dtPackingList.Rows[0]["Address_ShipParty"].ToString();
                //lblDate.Text = dtPackingList.Rows[0]["Date_Invoice"].ToString();
                //lblCarrier.Text = dtPackingList.Rows[0]["Carrier"].ToString();
                //lblCurrency.Text = dtPackingList.Rows[0]["Currentcy"].ToString();
                //lblDestination.Text = dtPackingList.Rows[0]["Destination"].ToString();
                //lblFeight.Text = dtPackingList.Rows[0]["Freight"].ToString();
                //lblInvoiceNo.Text = dtPackingList.Rows[0]["Invoice_No"].ToString();
                //lblNote.Text = dtPackingList.Rows[0]["Note"].ToString();
                //lblPayment.Text = dtPackingList.Rows[0]["Payment"].ToString();
                //lblDescription.Text = dtPackingList.Rows[0]["Description"].ToString();

                lblvendor_To.Text = dtPackingList.Rows[0]["Vendor_BillTo"].ToString();
                lblVendorName.Text = dtPackingList.Rows[0]["VendorName_BillTo"].ToString();
                lblVendorAddress.Text = dtPackingList.Rows[0]["Vendor_AddressTo"].ToString();
                lblVendorPICTo.Text = dtPackingList.Rows[0]["Vendor_PIC_To"].ToString();

                lblVendorNameShip.Text = dtPackingList.Rows[0]["Ship_To_PartyName"].ToString();
                lblVendorAddressShip.Text = dtPackingList.Rows[0]["Address_ShipParty"].ToString();
                lblVendorPICShip.Text = dtPackingList.Rows[0]["Attn_ShipParty"].ToString();
                lblDate.Text = dtPackingList.Rows[0]["Date_Invoice"].ToString();
                lblCarrier.Text = dtPackingList.Rows[0]["Carrier"].ToString();
                lblCurrency.Text = dtPackingList.Rows[0]["Currentcy"].ToString();
                lblDestination.Text = dtPackingList.Rows[0]["Destination"].ToString();
                lblFeight.Text = dtPackingList.Rows[0]["Freight"].ToString();
                lblInvoiceNo.Text = dtPackingList.Rows[0]["Invoice_No"].ToString();
                lblNote.Text = dtPackingList.Rows[0]["Note"].ToString();
                lblPayment.Text = dtPackingList.Rows[0]["Payment"].ToString();
                lblDescription.Text = dtPackingList.Rows[0]["Description"].ToString();
                lblTrade.Text = dtPackingList.Rows[0]["Trade_Tearm"].ToString();

            }
            DataTable dt = DataConn.FillStore("SP_Invoice_PlantPrint", CommandType.StoredProcedure, Invoice);
            if (dt.Rows.Count > 0) // Liệt kê Plant theo RQ Có trường hợp nhiều RQ Materia
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    StrPlant = StrPlant + ',' + dt.Rows[j]["Plant"].ToString();

                }
                if (StrPlant != "")
                {
                    int index = StrPlant.Length - 1;
                    StrPlant = StrPlant.Substring(1, index);// lOẠI bo dau, strStore = strStore.Substring(2,));// lOẠI bo dau,
                }
                this.lblPlant.Text = StrPlant;

            }
        }

        protected void bttSave_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptData.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {

                    string Item_No = (item.FindControl("lblItemNo") as Label).Text;
                    string txtNO_OF_CTN = (item.FindControl("txtNO_OF_CTN") as TextBox).Text;
                    string txtNO_OF_PALLET = (item.FindControl("txtNO_OF_PALLET") as TextBox).Text;
                    string txtNET_WGT_KG = (item.FindControl("txtNET_WGT_KG") as TextBox).Text;
                    string txtGRS_WGT_KG = (item.FindControl("txtGRS_WGT_KG") as TextBox).Text;
                    string txtCTN_NO = (item.FindControl("txtCTN_NO") as TextBox).Text;
                    string txtDimensions_L = (item.FindControl("txtDimensions_L") as TextBox).Text;

                    int rows = DataConn.ExecuteStore("SP_Invoice_CreatePackingList", CommandType.StoredProcedure,
                   lblInvoiceNo.Text.ToString(), txtNO_OF_CTN, txtNET_WGT_KG, txtGRS_WGT_KG, txtCTN_NO, txtDimensions_L, Item_No, txtNO_OF_PALLET);
                    if(rows ==0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Update  Packing List has sucessfully');", true);
                        //bttSave.Visible = false;
                       // LoadData(lblInvoiceNo.Text);
                    }
                }
             }

            LoadData(lblInvoiceNo.Text);
        }


        protected void bttDownload_Click(object sender, EventArgs e)
        {

            DataTable dtDowload = DataConn.FillStore("SP_Invoice_DetailPackingListPrint", CommandType.StoredProcedure, lblInvoiceNo.Text.ToString());
            if (dtDowload.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtDowload, "Download_PackingList");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Download_PackingList.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }

            }

        }

        protected void bttUpload_Click(object sender, EventArgs e)
        {
            System.Data.OleDb.OleDbConnection MyConnection;
            System.Data.DataSet DtSet;
            System.Data.OleDb.OleDbDataAdapter MyCommand;
            DataTable dt = new DataTable();
            dt = null;
            if (fUpload.HasFile)
            {
                string strFileType = Path.GetExtension(fUpload.FileName).ToLower();
                string path = fUpload.PostedFile.FileName;
                string link_path = Server.MapPath("~/Upload_Request/" + DateTime.Now.ToString("yyyyMMdd") + "_" + fUpload.FileName);
                //if (fUpload.FileName == "Upload_PackingList.xlsx" || fUpload.FileName == "Upload_PackingList.xls")
                //{
                    fUpload.SaveAs(link_path);
                    //Connection String to Excel Workbook            
                    if (strFileType.Trim() == ".xls")
                    {
                        //connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + link_path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"");
                        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]  where InvoiceNo is not null", MyConnection);
                        MyCommand.TableMappings.Add("Table", "Sheet1");
                        DtSet = new System.Data.DataSet();
                        MyCommand.Fill(DtSet);
                        dt = DtSet.Tables[0];
                        MyConnection.Close();
                    }
                    else if (strFileType.Trim() == ".xlsx")
                    {
                        //connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + link_path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"");
                        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$] where InvoiceNo is not null", MyConnection);
                        MyCommand.TableMappings.Add("Table", "Sheet1");
                        DtSet = new System.Data.DataSet();
                        MyCommand.Fill(DtSet);
                        dt = DtSet.Tables[0];
                        MyConnection.Close();
                    }
                    string sql_ = "";
                    if (dt.Rows.Count > 0)
                    {
                        DataTable DtIssuse = new DataTable();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //RequestNo,PartNo,MOQ,LeadTime,CustomerCode, Deadline,Remask,Peson_Incharge,Date_Incharge,Insert_user,Update_Date
                            string InvoiceNo = null; string ItemNo = null; string OFCTN = null;string NETWGT_KG; string GRSWGT_KG; string CTNNO = null; string Dimension = null; 
                            string No_OFCTN = null;


                            if (dt.Rows[i][0].ToString() != "")
                            {
                                InvoiceNo = dt.Rows[i][0].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('InvoiceNo :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }

                            if (dt.Rows[i][0].ToString() != "")
                            {
                                ItemNo = dt.Rows[i][1].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('ItemNo :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }
                            
                            OFCTN = dt.Rows[i][2].ToString();
                            No_OFCTN = dt.Rows[i][3].ToString();
                            NETWGT_KG = dt.Rows[i][4].ToString();
                            GRSWGT_KG = dt.Rows[i][5].ToString();
                            CTNNO = dt.Rows[i][6].ToString();
                            Dimension = dt.Rows[i][7].ToString();

                            DataTable dtCheckMV = DataConn.DataTable_Sql(" select count(*) as CheckNO from [dbo].[tbl_Comercial_Invoice] where Invoice_No =  '" + InvoiceNo + "'  and  Item_No = '" + ItemNo + "'" );
                            if (int.Parse(dtCheckMV.Rows[0]["CheckNO"].ToString()) > 0)
                            {

                                sql_ = sql_ + " UPDATE tbl_Comercial_Invoice ";
                                sql_ = sql_ + " SET  [NO_OF_CTN] = " + OFCTN + "  ,[NET_WGT_KG] =  " + NETWGT_KG + " ,[GRS_WGT_KG] = " + GRSWGT_KG + " ,[CTN_NO] = N'" + CTNNO + "'  ,[DimensionsL*W*H]= N'" + Dimension + "',";
                                sql_ = sql_ + "NO_OF_PALLET =   '" + No_OFCTN + "'  , User_Update_PK =  '" + Session["UserName"].ToString() + "' ,Date_Update_PK = '"+ DateTime.Now.ToShortDateString().ToString() + "' ";
                                sql_ = sql_ + "Where Invoice_No   = '" + InvoiceNo + "' and Item_No = '" + ItemNo + "'  ";
                                        Session["Invoice_No"] = InvoiceNo;

                            }
                            else
                            {

                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('NG.Upload because not exits InvoiceNo at row :(" + (i + 1).ToString() + ")');", true);
                                return;

                            }
                            
                        }

                    }
                    if (sql_ != "")
                    {
                        DataConn.Execute_NonSQL(sql_);
                        LoadData(Session["Invoice_No"].ToString());
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Upload successfully....');", true);
                    }

               // }
               // else
               // {
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please you change the name of the excel file with the name: UploadMaterInOut_A');", true);

              //  }
                // Delete file upload
                if (System.IO.File.Exists(link_path))
                {
                    System.IO.File.Delete(link_path);
                }
            }
            else
            {

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning(Please check data excel file !!!');", true);
            }

        }

        protected void bttTemUpload_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Template/Upload_PackingList.xlsx");
        }


        //protected void txtNET_WGT_KG_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox txtNET_WGT_KG = (sender as TextBox);
        //    string NET_WGT_KG = txtNET_WGT_KG.Text;
        //    string ids = txtNET_WGT_KG.AccessKey;

        //}

    }
}