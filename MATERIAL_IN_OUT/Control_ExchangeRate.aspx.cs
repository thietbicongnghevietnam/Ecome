
using MATERIAL_IN_OUT.AppCode;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using ClosedXML.Excel;

namespace MATERIAL_IN_OUT
{
    
    public partial class Control_ExchangeRate : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMaterCurreny();
                LoadData();
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                
            }
                
        }

        public void BindMaterCurreny()
        {
            var dt = DataConn.StoreFillDS("SP_Bind_CurrencyMaster", CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                ddlExchange.Items.Clear();
                DataRow dr = dt.NewRow();
                ddlExchange.DataSource = dt;
                ddlExchange.DataValueField = "Currency";
                ddlExchange.DataTextField = "Currency";
                ddlExchange.DataBind();
            }
        }

        public void LoadData()
        {
            var dt = DataConn.StoreFillDS("SP_BindCurrencyDaily_Load", CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                prtExchange.DataSource = dt;
                prtExchange.DataBind();
            }
        }

        protected void bttSave_Click(object sender, EventArgs e)
        {
            Boolean Check = true;
             if(txtDate.Text.ToString()== "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Date Input  is NULL');", true);
                txtDate.Focus();
                Check = false;
                return;
            }
            if (txtExchangeInput.Text.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Exchange Rate  is NULL.Please Input again');", true);
                txtExchangeInput.Focus();
                Check = false;
                return;
            }
            if (Check == true)
            {
                int row = DataConn.ExecuteStore("SP_BindCurrencyDaily_Save", CommandType.StoredProcedure,txtDate.Text.ToString(),ddlExchange.SelectedValue.ToString(),txtExchangeInput.Text.ToString(), Session["UserName"]);
                if (row == 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Save sucessfully');", true);
                }

                LoadData();
            }
           

        }
    }
}