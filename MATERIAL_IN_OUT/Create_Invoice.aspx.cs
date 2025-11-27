
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
    public partial class Create_Invoice : System.Web.UI.Page
    {
        public DataTable dtListCreateRQ = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string REQUESTID = Request.QueryString["RQ"].ToString();
                lblRQ_Show.Text = REQUESTID.ToString();

                BindCurrency();
                BindDescription();
                BindFreight();
                BindPayment();
                BindTrade();

                if (lblRQ_Show.Text.ToString() != "")
                {
                    LoadDataRQ(lblRQ_Show.Text.ToString());
                    Auto_CreateRQ();
                }
                bttDownload.Visible = false;
                bttExportInvoice.Visible = false;
            }

            if (lblRQ_Show.Text.ToString() != "")
            {
                LoadDataRQ(lblRQ_Show.Text.ToString());
                Auto_CreateRQ();
            }


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
        protected void LoadDataRQ(string RQ)
        {
            string StrPlant = "";
            dtListCreateRQ = DataConn.FillStore("SP_Invoice_LoadData_RefeRQMaterial", CommandType.StoredProcedure, RQ);
            if(dtListCreateRQ.Rows.Count  > 0)
            {
                lblVendorCode_To.Text = dtListCreateRQ.Rows[0]["VendorCode"].ToString();
                lblNameVendor_to.Text = dtListCreateRQ.Rows[0]["VendorName"].ToString();
                lblAddressVendor_To.Text = dtListCreateRQ.Rows[0]["Vendor_Address"].ToString();
                lblPIC_vendorTo.Text = dtListCreateRQ.Rows[0]["PIC_Vendor"].ToString();
                lblDate.Text = dtListCreateRQ.Rows[0]["DateVoucher"].ToString();
            }
            DataTable dt = DataConn.FillStore("SP_Invoice_LoadRQMaterial_PlantDetail", CommandType.StoredProcedure, RQ);
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
        protected void Auto_CreateRQ()
        {
            
            string DateCreate = lblDate.Text.ToString().Trim();
            string DayInvoice = null;
            if (DateCreate.Length > 0 )
            {
                string yearInvoice = DateCreate.Substring(DateCreate.Length - 2, 2);//Lay nam
                string monthIncoive = DateCreate.Substring(3, 2);//Lay thang
                string dateInvoice = DateCreate.Substring(0, 2);//Lay ngay
                 DayInvoice = yearInvoice + monthIncoive + dateInvoice;

            }
            string Note = "";
            string Plant = lblPlant.Text.Trim().Substring(1, 1); // Lay ky tu thu 2 cuar Plant
            string VendorCode = lblVendorCode_To.Text.Trim();
            if (txtNote.Text.Length >= 1)
            {
                 Note = "-" + txtNote.Text.Trim().ToUpper();
                lblInvoiceNO.Text = "IPUS" + Plant + DayInvoice + VendorCode + Note;
            }
            else
            {
                lblInvoiceNO.Text =  "IPUS" + Plant + DayInvoice + VendorCode + Note;
            }

        }
        protected void bttSaveInvoice_Click(object sender, EventArgs e)
        {

            dtListCreateRQ = DataConn.FillStore("SP_Invoice_LoadData_RefeRQMaterial", CommandType.StoredProcedure, lblRQ_Show.Text.ToString());
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
            string AddressVendor_Ship = txtAddress_Shipment.Value.ToString();
            string PICVendor_Ship = txtAnt_Shipment.Text.ToString();
            string VendorShip = txtVendor_Shipment.Text.ToString();
            string Remark = txtRemark.Text.ToString();
            string Invoice_No = lblInvoiceNO.Text.ToString();
            string Vencode_To = lblVendorCode_To.Text.ToString();
            string VendorName_To = lblNameVendor_to.Text.ToString();
            string Addresss_To = lblAddressVendor_To.Text.ToString();
            string PiC_VenodorTo = txtAttn_BIllTO.Text.ToString();

            string sql_ = "";
            string sql_1 = ""; string sql_2 = "";
            int flag_Insert = 0;
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
          
            if(dtListCreateRQ.Rows.Count > 0)
            {
                for (int i = 0; i < dtListCreateRQ.Rows.Count - 1; i++)
                {
                    string Refer_RQ = dtListCreateRQ.Rows[i]["RequestNo"].ToString();
                    string ItemNo = dtListCreateRQ.Rows[i]["Material"].ToString();
                    string Item_Description = dtListCreateRQ.Rows[i]["ItemDescription"].ToString();
                    float Qty = float.Parse(dtListCreateRQ.Rows[i]["IssueQty"].ToString());
                    float UnitPrice = float.Parse(dtListCreateRQ.Rows[i]["UnitPrice_AC"].ToString());
                    float Amount = float.Parse(dtListCreateRQ.Rows[i]["Amount_AC"].ToString());
                    string CountryofOrigin = dtListCreateRQ.Rows[i]["CountryofOrigin"].ToString();



                    DataTable DtCheck = DataConn.FillStore("SP_Invoice_CheckExit", CommandType.StoredProcedure,  Invoice_No);

                    if (CheckUpdate == true)
                    {

                        if (DtCheck.Rows[0]["TotalCheck"].ToString() == "0")
                        {

                            sql_ = sql_ + " INSERT INTO tbl_Comercial_Invoice (Invoice_No,Refer_RQ_Material,Item_No,Item_Decription,Plant,Qty,Unit_Price,Amount,Date_Invoice,";
                            sql_ = sql_ + "Currentcy,Trade_Tearm,Freight,Payment,Destination,Carrier,PO_Number,Note,Ship_To_PartyName,Address_ShipParty,Attn_ShipParty,";
                            sql_ = sql_ + "Remask,Status_Finish,Vendor_BillTo,VendorName_BillTo,Vendor_AddressTo,Vendor_PIC_To,CountryofOrigin,Description)";
                            sql_ = sql_ + "values('" + Invoice_No + "','" + Refer_RQ + "','" + ItemNo + "','" + Item_Description + "','" + Plan + "','" + Qty + "','" + UnitPrice + "','" + Amount + "',";
                            sql_ = sql_ + "'" + DateInvoice_ + "',N'" + Currency + "','" + TradeTeam + "','" + Freight + "','" + Payment + "',N'" + Destination + "','" + Carrier + "',";
                            sql_ = sql_ + "'" + PONo + "',N'" + Note + "','" + txtVendor_Shipment.Text.ToString() + "','"+ AddressVendor_Ship + "',N'" + PICVendor_Ship + "',N'" + Remark + "','Finish',";
                            sql_ = sql_ + "'" + Vencode_To + "','" + VendorName_To + "','" + Addresss_To + "','" + PiC_VenodorTo + "','" + CountryofOrigin + "','" + Description + "')";

                            Session["Invoice_No"] = Invoice_No;
                            sql_1 = sql_1 + "Update  tbl_RQ_MaterialIssue  set  Status_IVoice  = 'Y' where  RequestNo in  ('" + Refer_RQ + "');";
                            sql_2 = sql_2 + "Update  [tbl_RQ_MaterialIssueB]  set Status_IVoice  =  'Y' where  RequestNo  in  ('" + Refer_RQ + "')";
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('NG. Because Invoice already insert before.');", true);
                            break;
                        }

                        flag_Insert = i;
                        int TotalRow = dtListCreateRQ.Rows.Count - 2;
                        if (flag_Insert == TotalRow)
                        {
                            DataConn.Execute_NonSQL(sql_);
                          
                            DataConn.Execute_NonSQL(sql_1);
                            DataConn.Execute_NonSQL(sql_2);
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Create Commercial invoice has sucessfully');", true);
                            bttSaveInvoice.Visible = false;
                            bttExportInvoice.Visible = true;
                            bttDownload.Visible = true;

                        }
                    }
                }

            }
           

        }
        protected void txtNote_TextChanged(object sender, EventArgs e)
        {
            Auto_CreateRQ();
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
            if (Session["Invoice_No"].ToString()!= "") 
            {
                Response.Redirect("Detail_Commercial_Invoice.aspx?Invoice="+ Session["Invoice_No"].ToString(), false);
            }

        }
    }
}