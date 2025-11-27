
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
using System.Web.UI.WebControls;
namespace MATERIAL_IN_OUT
{
    public partial class ReviseInvoice : System.Web.UI.Page
    {
        public DataTable dtListCreateRQ = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string InvoiceNo = Request.QueryString["Invoice"].ToString();
                lblRQ_Show.Text = InvoiceNo.ToString();

                BindCurrency();
                BindDescription();
                BindFreight();
                BindPayment();
                BindTrade();

                if (lblRQ_Show.Text.ToString() != "")
                {
                    LoadInvoice(lblRQ_Show.Text.ToString());
                    UpdateStatus(lblRQ_Show.Text.ToString());
                    txtNote.ReadOnly = true;

                }
                bttDownload.Visible = false;
                bttExportInvoice.Visible = false;
            }
            txtNote.ReadOnly = true;


        }
        public  void UpdateStatus(String Invoice)
        {
            int rows = DataConn.ExecuteStore("SP_Invoice_ReviseIV_Sent_FPT", CommandType.StoredProcedure, Invoice);
        }

        protected void BindTrade()
        {
            DataTable dt = new DataTable();
            dt = DataConn.FillStore("SP_Invoice_TradeTermsMaster", CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                ddlTrade_Tearm.Items.Clear();
                DataRow dr = dt.NewRow();
                dr[0] = "--Select TradeTerm--";
                dt.Rows.InsertAt(dr, 0);
                ddlTrade_Tearm.DataTextField = "TradeTerms";
                ddlTrade_Tearm.DataValueField = "TradeTerms";
                ddlTrade_Tearm.DataSource = dt;
                ddlTrade_Tearm.DataBind();
            }

        }
        protected void BindCurrency()
        {
            DataTable dt = new DataTable();
            dt = DataConn.FillStore("SP_Invoice_CurrencyMaster", CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                ddlCurrency.Items.Clear();
                DataRow dr = dt.NewRow();
                dr[1] = "--Select Currency--";
                dt.Rows.InsertAt(dr, 0);
                ddlCurrency.DataTextField = "Currency";
                ddlCurrency.DataValueField = "Currency";
                ddlCurrency.DataSource = dt;
                ddlCurrency.DataBind();
            }

        }
        protected void BindDescription()
        {
            DataTable dt = new DataTable();
            dt = DataConn.FillStore("SP_Invoice_DescriptionMaster", CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                ddlDescription.Items.Clear();
                DataRow dr = dt.NewRow();
                dr[1] = "--Select Description--";
                dt.Rows.InsertAt(dr, 0);
                ddlDescription.DataTextField = "Description";
                ddlDescription.DataValueField = "Description";
                ddlDescription.DataSource = dt;
                ddlDescription.DataBind();
            }

        }
        protected void BindFreight()
        {
            DataTable dt = new DataTable();
            dt = DataConn.FillStore("SP_Invoice_FreightMaster", CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                ddlFeight.Items.Clear();
                DataRow dr = dt.NewRow();
                dr[0] = "--Select Freight--";
                dt.Rows.InsertAt(dr, 0);
                ddlFeight.DataTextField = "Freight";
                ddlFeight.DataValueField = "Freight";
                ddlFeight.DataSource = dt;
                ddlFeight.DataBind();
            }

        }
        protected void BindPayment()
        {
            DataTable dt = new DataTable();
            dt = DataConn.FillStore("SP_Invoice_PaymentMaster", CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                ddlPayment.Items.Clear();
                DataRow dr = dt.NewRow();
                dr[1] = "--Select Payment--";
                dt.Rows.InsertAt(dr, 0);
                ddlPayment.DataTextField = "Payment";
                ddlPayment.DataValueField = "Payment";
                ddlPayment.DataSource = dt;
                ddlPayment.DataBind();
            }

        }
        protected void LoadInvoice(string invoce)
        {

            dtListCreateRQ = DataConn.FillStore("SP_Invoice_DetailInvoicePrint", CommandType.StoredProcedure, invoce);
            if(dtListCreateRQ.Rows.Count  > 0)
            {
                lblVendorCode_To.Text = dtListCreateRQ.Rows[0]["Vendor_BillTo"].ToString();
                lblNameVendor_to.Text = dtListCreateRQ.Rows[0]["VendorName_BillTo"].ToString();
                lblAddressVendor_To.Text = dtListCreateRQ.Rows[0]["Vendor_AddressTo"].ToString();
                txtAttn_BIllTO.Text = dtListCreateRQ.Rows[0]["Vendor_PIC_To"].ToString();
                 lblPlant.Text = dtListCreateRQ.Rows[0]["Plant"].ToString();
                ddlCurrency.SelectedValue = dtListCreateRQ.Rows[0]["Currentcy"].ToString();
                ddlTrade_Tearm.SelectedValue= dtListCreateRQ.Rows[0]["Trade_Tearm"].ToString();
                ddlFeight.SelectedValue= dtListCreateRQ.Rows[0]["Freight"].ToString();
                ddlPayment.SelectedValue= dtListCreateRQ.Rows[0]["Payment"].ToString();
                txtDestination.Text = dtListCreateRQ.Rows[0]["Destination"].ToString();
                txtCarrier.Text= dtListCreateRQ.Rows[0]["Carrier"].ToString();
                txtPONumber.Text= dtListCreateRQ.Rows[0]["PO_Number"].ToString();
                lblDate.Text= dtListCreateRQ.Rows[0]["Date_Invoice"].ToString();
                ddlDescription.SelectedValue= dtListCreateRQ.Rows[0]["Description"].ToString();
                txtNote.Text= dtListCreateRQ.Rows[0]["Note"].ToString();
                txtAddress_Shipment.Value= dtListCreateRQ.Rows[0]["Address_ShipParty"].ToString();
                txtAnt_Shipment.Text= dtListCreateRQ.Rows[0]["Attn_ShipParty"].ToString();
                txtVendor_Shipment.Text = dtListCreateRQ.Rows[0]["Ship_To_PartyName"].ToString();
                txtRemark.Text= dtListCreateRQ.Rows[0]["Remask"].ToString();
                lblInvoiceNO.Text= dtListCreateRQ.Rows[0]["Invoice_No"].ToString();
                

            }
          
        }
     
        protected void bttSaveInvoice_Click(object sender, EventArgs e)
        {

            dtListCreateRQ = DataConn.FillStore("SP_Invoice_DetailInvoicePrint", CommandType.StoredProcedure, lblRQ_Show.Text.ToString());
            Boolean CheckUpdate = true;
            string Plan = lblPlant.Text.ToString().Trim();
            string Currency = ddlCurrency.SelectedValue.ToString();
            string TradeTeam = ddlTrade_Tearm.SelectedValue.ToString();
            string Freight = ddlFeight.SelectedValue.ToString();
            string Payment = ddlPayment.SelectedValue.ToString();
            string Destination = txtDestination.Text.ToString();
            string Carrier = txtCarrier.Text.ToString().Trim();
            string PONo = txtPONumber.Text.ToString().Trim();
            string DateInvoice_ = lblDate.Text.ToString();
            string Description = ddlDescription.SelectedValue.ToString();
            string Note = txtNote.Text.ToString();
            string AddressVendor = txtAddress_Shipment.Value.ToString();
            string PICVendor = txtAnt_Shipment.Text.ToString();
            string Remark = txtRemark.Text.ToString();
            string Invoice_No = lblInvoiceNO.Text.ToString();
            string Vencode_To = lblVendorCode_To.Text.ToString();
            string VendorName_To = lblNameVendor_to.Text.ToString();
            string Addresss_To = lblAddressVendor_To.Text.ToString();
            string PiC_VenodorTo = txtAttn_BIllTO.Text.ToString();
            string VendorShip = txtVendor_Shipment.Text.ToString();
            string AddressShip = txtAddress_Shipment.Value.ToString();
            string AntShipment = txtAnt_Shipment.Text.ToString();
            
            string sql_ = "";
            string DateUpdate;
            if (Currency == "--Select Currency--")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('NG.Please input Currency.');", true);
                CheckUpdate = false;
                ddlCurrency.Focus();
                return;
            }
            else if (TradeTeam == "--Select TradeTerm--")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('NG.Please input TradeTearm.');", true);
                CheckUpdate = false;
                ddlTrade_Tearm.Focus();
                return;
            }
            else if (Freight == "--Select Freight--")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('NG.Please input Freight.');", true);
                CheckUpdate = false;
                ddlFeight.Focus();
                return;

            }
            else if (Freight == "----Select Payment----")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('NG.Please input Payment.');", true);
                CheckUpdate = false;
                ddlFeight.Focus();
                return;
            }
            else if (Freight == "------Select Description------")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('NG.Please input Description.');", true);
                CheckUpdate = false;
                ddlFeight.Focus();
                return;
            }
            else if (txtAddress_Shipment.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('NG.Please input Address_Shipment.');", true);
                CheckUpdate = false;
                txtAddress_Shipment.Focus();
                return;
            }
            else if (txtAnt_Shipment.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('NG.Please input Ant Shipment.');", true);
                CheckUpdate = false;
                txtAddress_Shipment.Focus();
                return;
            }
            else if (txtVendor_Shipment.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('NG.Please input Vendor Shipment.');", true);
                CheckUpdate = false;
                txtVendor_Shipment.Focus();
                return;
            }
            else if (txtDestination.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('NG.Please input Destination.');", true);
                CheckUpdate = false;
                txtDestination.Focus();
                return;
            }
            else if (txtCarrier.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('NG.Please input Carrier.');", true);
                CheckUpdate = false;
                txtCarrier.Focus();
                return;
            }
          
       
                     string StatusInvoice = "Finish";
                     DateUpdate = DateTime.Now.ToString("yyyy-MM-dd");
            
                    if (CheckUpdate == true)
                    {

                        sql_ = sql_ + " UPDATE tbl_Comercial_Invoice  SET ";
                        sql_ = sql_ + " Currentcy =  '" + Currency + "' ,Trade_Tearm =  '" + TradeTeam + "' ,  Freight   =  '" + Freight + "' ,";
                        sql_ = sql_ + " Payment =  '" + Payment + "' ,Destination =  N'" + Destination + "' ,  Carrier   =  '" + Carrier + "' ,";
                        sql_ = sql_ + " PO_Number =  '" + PONo + "' ,Note =  N'" + Note + "' ,  Ship_To_PartyName   =  '" + txtVendor_Shipment.Text.ToString() + "' ,";
                        sql_ = sql_ + " Address_ShipParty =  '" + AddressShip + "' ,Attn_ShipParty =  N'" + AntShipment + "' ,  Remask   =  N'" + Remark + "' ,";
                        sql_ = sql_ + " Status_Finish =  '" + StatusInvoice + "'  ,  VendorName_BillTo   =  N'" + VendorName_To + "',Vendor_BillTo=   N'" + Vencode_To + "',";
                        sql_ = sql_ + " Vendor_AddressTo =  '" + Addresss_To + "' ,Vendor_PIC_To =  N'" + PiC_VenodorTo + "', ";
                        sql_ = sql_ + " Date_ReviseIV =  '" + DateUpdate + "' ,User_ReviseIV = '" + Session["UserName"].ToString() + "' ";
                        sql_ = sql_ + " where Invoice_No =  '" + Invoice_No + "'";

                // flag_Insert = i;
                //int TotalRow = dtListCreateRQ.Rows.Count - 2;
                // if (flag_Insert == TotalRow)
                //{

                // }
                       DataConn.Execute_NonSQL(sql_);
                       Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Revise Commercial invoice has sucessfully');", true);
                       bttSaveInvoice.Visible = false;
                       bttExportInvoice.Visible = true;
                       bttDownload.Visible = true;
                    }
              //  }

           // }
           

        }
        protected void txtNote_TextChanged(object sender, EventArgs e)
        {
            
        }
        protected void bttDownload_Click(object sender, EventArgs e)
        {
            DataTable dt_ExportExcel = new DataTable();
            {
                dt_ExportExcel = DataConn.FillStore("SP_Invoice_DetailInvoicePrint", CommandType.StoredProcedure, lblInvoiceNO.Text.ToString());
            }
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ExportInvoice.xls");
            Response.Charset = "";
            Response.ContentType = "application/ms-excel";
            if (dt_ExportExcel.Rows.Count  > 0)
            {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt_ExportExcel, "InOut_FormB");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=DownloadInvoice.xlsx");
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

        protected void bttExportInvoice_Click(object sender, EventArgs e)
        {
            
                Response.Redirect("Detail_Commercial_Invoice.aspx?Invoice="+ lblRQ_Show.Text.ToString(), false);
            

        }
    }
}