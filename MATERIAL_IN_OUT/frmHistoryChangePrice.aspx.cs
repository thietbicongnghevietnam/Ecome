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
    public partial class frmHistoryChangePrice : System.Web.UI.Page
    {
        public DataTable dt = new DataTable();
        public DataTable dt_update = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            dt = DataConn.StoreFillDS("Select_hisorty_changepriceSAP", System.Data.CommandType.StoredProcedure);
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];
            string filtertypename = filterMaterial.Value;
            string rquestNO = filterRequestNo.Value;

            dt = DataConn.StoreFillDS("Select_hisorty_changepriceSAP_loc", System.Data.CommandType.StoredProcedure, _fromdate, _todate, filtertypename, rquestNO);

            //string category = dr_filter_Cate.SelectedValue;
            //if (_fromdate == "" || _todate == "")
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Date is null!'); ", true);
            //}
            //else
            //{
            //    //dt_image = DataConn.StoreFillDS("Select_holiday_vessel", System.Data.CommandType.StoredProcedure, _fromdate, _todate);

            //}

        }


    }
}