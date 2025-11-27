
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
    public partial class ControlRQ_Invoice : System.Web.UI.Page
    {
        public DataTable dtListRQ = new DataTable();
        public DataTable dtInvoice = new DataTable();
        public DataTable dtCheckDate = new DataTable();
        public DataTable dtCurrency = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRQ_Material();
                BindDept();
                BindVendor();
                ListInvoice();
                //txtDateVoucher.Value = DateTime.Now.ToString("dd/MM/yyyy");
                //E-Invoice
                // lblStatus.Text = "Status_Feed_EInvoice";
                // lblStatus.ToolTip = "Detail_Feed_EInvoice";
                // If Status_EInvoice = 0 =. hien thi =1 => disable
            }
        }
        public void LoadRQ_Material()
        {
            dtListRQ = DataConn.FillStore("SP_Invoice_RQMaterial", CommandType.StoredProcedure);
        }

        public void ListInvoice()
        {
            dtInvoice = DataConn.FillStore("SP_Invoice_ListInvoice", CommandType.StoredProcedure);
            RptListInvoice.DataSource = dtInvoice;
            RptListInvoice.DataBind();
        }
        protected void BindVendor()
        {
            DataTable dt = new DataTable();
            dt = DataConn.FillStore("SP_Invoice_VendorMaster", CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                ddlVendor.Items.Clear();
                DataRow dr = dt.NewRow();
                dr[0] = "--Select Vendor--";
                dt.Rows.InsertAt(dr, 0);
                ddlVendor.DataTextField = "VendorCode";
                ddlVendor.DataValueField = "VendorCode";
                ddlVendor.DataSource = dt;
                ddlVendor.DataBind();
            }


        }
        protected void BindDept()
        {
            DataTable dt = new DataTable();
            dt = DataConn.DataTable_Sql("select DeptCode,DeptName from  [dbo].[tbl_DeptMaster]");
            if (dt.Rows.Count > 0)
            {
                ddlDept.Items.Clear();
                DataRow dr = dt.NewRow();
                dr[0] = "--Select Dept--";
                dt.Rows.InsertAt(dr, 0);
                ddlDept.DataTextField = "DeptName";
                ddlDept.DataValueField = "DeptCode";
                ddlDept.DataSource = dt;
                ddlDept.DataBind();
            }

        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DateVourcher = txtDateVoucher.Value.ToString();
            string VendorCode = ddlVendor.SelectedValue.ToString();
            string DeptCode = ddlDept.SelectedValue.ToString();
            dtListRQ = DataConn.FillStore("[SP_Invoice_SearhRQMaterial]", CommandType.StoredProcedure, VendorCode, DeptCode, DateVourcher);
        }

        protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DateVourcher = txtDateVoucher.Value.ToString();
            string VendorCode = ddlVendor.SelectedValue.ToString();
            string DeptCode = ddlDept.SelectedValue.ToString();
            dtListRQ = DataConn.FillStore("[SP_Invoice_SearhRQMaterial]", CommandType.StoredProcedure, VendorCode, DeptCode, DateVourcher);
        }

        [WebMethod]
        public static string CreateInvoice_test(string Invoice)
        {
            try
            {
                object[] obj = new object[] { Invoice };
                int status = DataConn.ExecuteStore("SP_Create_EInvoice", CommandType.StoredProcedure, obj);
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        protected void bttSearch_Click(object sender, EventArgs e)
        {
            string DateVourcher = txtDateVoucher.Value.ToString();
            string VendorCode = ddlVendor.SelectedValue.ToString();
            string DeptCode = ddlDept.SelectedValue.ToString();
            dtListRQ = DataConn.FillStore("[SP_Invoice_SearhRQMaterial]", CommandType.StoredProcedure, VendorCode, DeptCode, DateVourcher);
        }

        protected void bttViewReport_ServerClick(object sender, EventArgs e)
        {


        }
        protected void ViewRQMaterial(int index)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("index = " +
                 index);
            }
            catch (Exception ex)
            {

            }
        }

        protected void CreateEInvoice_Click(object sender, EventArgs e)
        {
            //  int Update   =   DataConn.StoreFillDS("SP_Create_EInvoice", CommandType.StoredProcedure, );
        }

        protected void RptListInvoice_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string  InvoiceNo = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "Update":

                    
                     dtCurrency = DataConn.DataTable_Sql("select top 1 Currentcy  as Rate from  [dbo].[tbl_Comercial_Invoice] where Invoice_No  = '" + InvoiceNo + "'");
                    string Currency = dtCurrency.Rows[0]["Rate"].ToString();
                     dtCheckDate = DataConn.FillStore("SP_EInvoice_CheckDate_Input", CommandType.StoredProcedure, DateTime.Today.ToString("yyyy-MM-dd"),Currency);
                    if (dtCheckDate.Rows.Count > 0)
                    {
                        int status = DataConn.ExecuteStore("SP_Create_EInvoice", CommandType.StoredProcedure, InvoiceNo);
                        if (status == 0)
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Create E-Invoice successfully');", true);
                            LoadRQ_Material();
                        }

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('Please input exchange rate today');", true);
                    }
                    break;
            }
            ListInvoice();
        }
    }
}