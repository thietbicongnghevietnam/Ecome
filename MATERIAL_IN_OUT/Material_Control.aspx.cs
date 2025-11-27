
using MATERIAL_IN_OUT.AppCode;
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

namespace MATERIAL_IN_OUT
{
    public partial class Material_Control : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                    BindType();
            }

        }

        
        protected void BindType()
        {
            DataTable dt = new DataTable();
            dt = DataConn.DataTable_Sql("select TypeID, TypeName  from   [dbo].[tbl_TypeMater]   where FlagDelete = 'N'");
            if (dt.Rows.Count > 0)
            {
                
                DataRow dr = dt.NewRow();
                dr[1] = "-- Select TypeID  --";
                dt.Rows.InsertAt(dr, 0);
                ddlMasterType.DataSource = dt;
                ddlMasterType.DataTextField = "TypeName";
                ddlMasterType.DataValueField = "TypeID";
                ddlMasterType.DataBind();
                
            }
           
        }
        protected void BindData()
        {
            
            DataTable dt2 = new DataTable();
            dt2 = DataConn.DataTable_Sql("select a.TypeID, a.TypeName, a.Decription  from[dbo].[tbl_TypeMater] a  where a.TypeID = '" + ddlMasterType.SelectedValue.ToString() + "' ");
            if (dt2.Rows.Count > 0 )
            {
                lblDecription.Text = dt2.Rows[0]["Decription"].ToString();
                lblTypeID.Text = dt2.Rows[0]["TypeID"].ToString();

            }
        }

        protected void ddlMasterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void bttChosenType_Click(object sender, EventArgs e)
        {

            DataTable dt2 = new DataTable();
            dt2 = DataConn.DataTable_Sql("select a.TypeID, a.TypeName, a.Decription  from[dbo].[tbl_TypeMater] a  where a.TypeID = '" + ddlMasterType.SelectedValue.ToString() + "' ");
            if (dt2.Rows.Count > 0)
            {
                lblDecription.Text = dt2.Rows[0]["Decription"].ToString();
                lblTypeID.Text = dt2.Rows[0]["TypeID"].ToString();
                Session["TypeID"] = lblTypeID.Text;
                Session["NameType"] = dt2.Rows[0]["TypeName"].ToString();
                string stringToCheck = lblTypeID.Text.ToString();
                string[] stringArrayA = {"1", "6", "9", "12", "14", "15", "16", "17", "19", "22", "24", "25" };
                string[] stringArrayB = { "2", "3", "4", "5", "7", "8", "10", "11", "13", "18", "20", "21", "23" };
                foreach (string x in stringArrayA)
                {

                    if (stringToCheck.Contains(x))
                    {
                        Response.Redirect("Material_Issues_FormA.aspx?RQ=&User=&Dept=&Stock=&RoleRQ=&RoleRQ_Apr=&RoleStore=&RoleStore_Apr=", false);
                        
                        break;
                    }
                    
                }

                foreach (string x in stringArrayB)
                {
                    if (stringToCheck.Contains(x))
                    {
                        Response.Redirect("Material_Issue_FormB.aspx?RQ=&User=&Dept=&Stock=&RoleRQ=&RoleRQ_Apr=&RoleStore=&RoleStore_Apr=", false);
                        break;
                    }
                    
                }

            }
        }
    }
}