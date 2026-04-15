using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using DocumentFormat.OpenXml.Spreadsheet;

//using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Wordprocessing;
using MATERIAL_IN_OUT.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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