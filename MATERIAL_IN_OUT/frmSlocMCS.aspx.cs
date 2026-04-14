using AjaxControlToolkit.HTMLEditor.ToolbarButton;
//using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.VariantTypes;
using MATERIAL_IN_OUT.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                dt = DataConn.StoreFillDS("Select_PMS_OutSAP", System.Data.CommandType.StoredProcedure);
            }
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];
            


            //dt = DataConn.StoreFillDS("Select_PMS_OutSAP_search2_new2", System.Data.CommandType.StoredProcedure, _fromdate, _todate, requestno, sanctionno, typesapPMS, status_issueout, selectedOption);

            //if (dt.Rows.Count > 0)
            //{
            //    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('search successfully!');", true);
            //    Date1.Value = _fromdate.ToString();
            //}
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Data null!');", true);
            //}

        }


    }
}