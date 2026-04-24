using MATERIAL_IN_OUT.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MATERIAL_IN_OUT
{
    public partial class frmMaterAB : System.Web.UI.Page
    {
        public DataTable dt_image = new DataTable();
        public DataTable dt_update = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            dt_image = DataConn.StoreFillDS("Select_form_AB", System.Data.CommandType.StoredProcedure);
        }

        protected void Search_Date_Click(object sender, EventArgs e)
        {
            string _fromdate = Request.Form[Date1.UniqueID];
            string _todate = Request.Form[ngaychiid.UniqueID];
            string filtertypename = filterMaterial.Value;

            dt_image = DataConn.StoreFillDS("Select_form_AB_loc", System.Data.CommandType.StoredProcedure, filtertypename);

        }

        public void themhanghoa(object sender, EventArgs e)
        {
            string TypeID = TypeIDid.Text;
            string TypeName = TypeNameid.Text;
            string Decription = Decriptionid.Text;
            string NameTemplate = NameTemplateid.Text;
            string AccountCost = AccountCostid.Text;
            string AccountName = AccountNameid.Text;

            string MVTout    = outid.Text.Trim();
            string MVTin    = inid.Text.Trim();

            ////string userid = Session["username"].ToString();

            if (TypeID != "" && TypeName != "" && AccountCost != "" && MVTout != "" && MVTin != "" & NameTemplate != "")
            {
                DataTable dtinsert = new DataTable();
                //dtinsert = DataConn.StoreFillDS("Insert_Form_AB", System.Data.CommandType.StoredProcedure, TypeID, TypeName, Decription, NameTemplate, AccountCost, AccountName);
                dtinsert = DataConn.StoreFillDS("Insert_Form_AB2", System.Data.CommandType.StoredProcedure, TypeID, TypeName, Decription, NameTemplate, AccountCost, AccountName, MVTout, MVTin);
                if (dtinsert.Rows[0][0].ToString() == "1")
                {
                    dt_image = DataConn.StoreFillDS("Select_form_AB", System.Data.CommandType.StoredProcedure);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, check again!'); ", true);
                }
            }
            else 
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG Data null!, check again!'); ", true);
            }            
        }

        public void Updatethongtin(object sender, EventArgs e)
        {
            string TypeID = IDTypeID.Text;
            string TypeName = idTypeName.Text;
            string Decription = idDecription.Text;
            string NameTemplate = idNameTemplate.Text;
            string AccountCost = idAccountCost.Text;
            string AccountName = idAccountName.Text;

            string mvtout = idout.Text.Trim();
            string vmtin = idin.Text.Trim();

            string id = idID.Text;

            ////string userid = Session["username"].ToString();

            if (id != "" && AccountCost != "" && TypeName != "")
            {
                DataTable dtupdate = new DataTable();
                //dtupdate = DataConn.StoreFillDS("Update_Form_AB", System.Data.CommandType.StoredProcedure, TypeID, TypeName, Decription, NameTemplate, AccountCost, AccountName, id);
                dtupdate = DataConn.StoreFillDS("Update_Form_AB2", System.Data.CommandType.StoredProcedure, TypeID, TypeName, Decription, NameTemplate, AccountCost, AccountName, id, mvtout, vmtin);

                if (dtupdate.Rows[0][0].ToString() == "1")
                {
                    dt_image = DataConn.StoreFillDS("Select_form_AB", System.Data.CommandType.StoredProcedure);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Check again!'); ", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, data null!'); ", true);
            }
        }

        public void Xoathongtin(object sender, EventArgs e)
        {
            string id = txtid_del.Text;
            //string material = txMaterialName_del.Text;
            //////string username = Session["username"].ToString();
            //////string role_ = Session["role"].ToString();

            DataTable dtupdate = new DataTable();
            dtupdate = DataConn.StoreFillDS("Delete_from_AB", System.Data.CommandType.StoredProcedure, id);  //username
            if (dtupdate.Rows[0][0].ToString() == "1")
            {
                dt_image = DataConn.StoreFillDS("Select_form_AB", System.Data.CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Success!!!');", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG, Check again!'); ", true);
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

        public static int GetIntValueFromExcel(object value)
        {
            if (value == DBNull.Value || value == null)
                return 0;

            string strValue = value.ToString().Trim();
            if (string.IsNullOrEmpty(strValue))
                return 0;

            if (int.TryParse(strValue, out int result))
                return result;

            // Nếu không parse được (ví dụ: "abc"), trả về 0 hoặc xử lý tùy ý
            return 0;
        }

    }
}