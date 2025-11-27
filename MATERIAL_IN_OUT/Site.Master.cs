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
    public partial class SiteMaster : MasterPage
    {
        public string RolePage = null;
        public  DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((string)Session["UserName"] == null && (string)Session["password"] == null && (string)Session["Name"] == null && (string)Session["CostCenter"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                DataTable dt = DataConn.StoreFillDS("SP_Login_GetName", CommandType.StoredProcedure, Session["UserName"].ToString());
                string UserName = dt.Rows[0]["FullName"].ToString();
                if (UserName.ToString() != "")
                {
                    lblUsername.Text = UserName.ToString();
                    LoadMenu();
                }
            }

        }
        public void LoadMenu()
        {
            DataTable dt = DataConn.StoreFillDS("SP_Login_GetName", CommandType.StoredProcedure, Session["UserName"].ToString());
            string RolePage = dt.Rows[0]["RolePage"].ToString();

                if (RolePage == "Material")
                {

                    //linkTranfer.Visible = false;
                    linkUser.Visible = false;
                    linkInvoice.Visible = false;
                    linkPUR_CDS.Visible = false;
                    linkIncharge.Visible = false;
                    if(Session["Role_Dept"].ToString().Trim()=="RQ" && Session["Role_Aproved_Dept"].ToString() == "1") // Quyeen Incharge
                    {
                        linkIssueControl.Enabled = true;
                          linkMaterialA.Visible = false;
                        linkMaterialB.Visible = false;
                    } 
                    else // All quyeen vao trang luon
                    {
                    linkIssueControl.Visible = false ;
                    linkMaterialA.Visible = true;
                    linkMaterialB.Visible = true;
                    }

                  }


            if (RolePage == "Material" && Session["CostCenter"].ToString().Trim()=="PUR") // Quyền vào funtion Issule In- Out và INvoice và Create Invoice
            {

                //linkTranfer.Visible = false;
                linkUser.Visible = false;
                linkInvoice.Visible = true;
                linkPUR_CDS.Visible = true;
                linkIncharge.Visible = false;
                if (Session["Role_Dept"].ToString().Trim() == "RQ" && Session["Role_Aproved_Dept"].ToString() == "1") // Quyeen Incharge
                {
                    linkIssueControl.Enabled = true;
                    linkMaterialA.Visible = false;
                    linkMaterialB.Visible = false;
                }
                else // All quyeen vao trang luon
                {
                    linkIssueControl.Visible = false;
                    linkMaterialA.Visible = true;
                    linkMaterialB.Visible = true;
                }

            }

            if (RolePage == "Tranfer")
                {

                    linkIssueControl.Visible = false;
                    //linkTranfer.Visible = true;
                    linkUser.Visible = false;
                    linkInvoice.Visible = false;    
                    linkIssueControl.Visible = false;
                    linkMaterialA.Visible = false;
                    linkMaterialB.Visible = false;
                    linkPUR_CDS.Visible = false;
                    linkIncharge.Visible = false;

                }

                if (RolePage == "Both1")
                {
                    //linkTranfer.Visible = true;
                    linkUser.Visible = false;
                    linkInvoice.Visible = false;
                    linkPUR_CDS.Visible = false;
                    linkIncharge.Visible = false;
                if (Session["Role_Dept"].ToString().Trim() == "RQ" && Session["Role_Aproved_Dept"].ToString() == "1") // Quyeen Incharge
                    {
                        linkIssueControl.Visible = true;
                        linkMaterialA.Visible = false;
                        linkMaterialB.Visible = false;
                    }
                    else // All quyeen vao trang luon
                    {
                        linkIssueControl.Visible = false;
                        linkMaterialA.Visible = true;
                        linkMaterialB.Visible = true;
                    }
                 
                 }


                if (RolePage == "Both2")
                {
                    //linkTranfer.Visible = false;
                    linkUser.Visible = false;
                    linkInvoice.Visible = true;
                    linkPUR_CDS.Visible = false;
                    linkIncharge.Visible = false;
             
                    if (Session["Role_Dept"].ToString().Trim() == "RQ" && Session["Role_Aproved_Dept"].ToString() == "1") // Quyeen Incharge
                    {
                        linkIssueControl.Visible = true;
                        linkMaterialA.Visible = false;
                        linkMaterialB.Visible = false;
                    }
                    else // All quyeen vao trang luon
                    {
                        linkIssueControl.Visible = false;
                        linkMaterialA.Visible = true;
                        linkMaterialB.Visible = true;
                    }
            }

             if (RolePage == "Admin")
                {
                
                if (Session["Role_Dept"].ToString().Trim() == "RQ" && Session["Role_Aproved_Dept"].ToString() == "1") // Quyeen Incharge
                    {
                        linkIssueControl.Visible = true;
                        linkMaterialA.Visible = false;
                        linkMaterialB.Visible = false;
                        //linkTranfer.Visible = true;
                        linkUser.Visible = true;
                        linkInvoice.Visible = true;
                        linkPUR_CDS.Visible = true;
                        linkIncharge.Visible = true;
                      }
                    else // All quyeen vao trang luon
                    {
                        linkIssueControl.Visible = false;
                        linkMaterialA.Visible = true;
                        linkMaterialB.Visible = true;
                        //linkTranfer.Visible = true;
                        linkUser.Visible = true;
                        linkInvoice.Visible = true;
                        linkPUR_CDS.Visible = true;
                        linkIncharge.Visible = true;

                }
                   

            }


        }
    }
}