using System;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Diagnostics;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using MATERIAL_IN_OUT.AppCode;

namespace MATERIAL_IN_OUT
{
    public partial class Changepassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUser.Text = Session["UserName"].ToString();

        }
      
        protected void bttChange_Click1(object sender, EventArgs e)
        {
            bool CheckSave = true;
            if (txtUser.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please input information User.');", true);
                CheckSave = false;
                return;
            }
            if (txtPassword.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please input information Current Password.');", true);
                CheckSave = false;
                return;
            }
            if (txtNewPassword.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please input information New Password.');", true);
                CheckSave = false;
                return;
            }
            if (CheckSave == true)
            {
                int row = DataConn.ExecuteStore("SP_UserControl_ChangePWD", CommandType.StoredProcedure, txtUser.Text.ToString(), txtPassword.Text.ToString(), txtNewPassword.Text.ToString());
                if (row == 0)
                {
                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";

                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Changing password sucessfully');", true);

                }


            }
        }
    }
    
}