
using MATERIAL_IN_OUT.AppCode;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MATERIAL_IN_OUT
{
    public partial class PUR_Control_InOut_CDS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTree_View();
                LoadData();
                
            }
        }
        public void LoadData()
        {
            var dt = DataConn.StoreFillDS("SP_Invoice_ListRQ", CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                prtLoadRQ_CD.DataSource = dt;
                prtLoadRQ_CD.DataBind();
                
            }
        }

        public void LoadTree_View()
        {
            treeRQ_OutSap.Nodes.Clear();
            
            
            TreeNode treeNodes = new TreeNode
            {
                Text = "Management RQ Out SAP"
            };

            treeRQ_OutSap.Nodes.Add(treeNodes);
            DataTable table_Tree1 = new DataTable();
           
            table_Tree1 = DataConn.StoreFillDS("SP_Issue_Material_Approved_OutSAP_NotApproved", CommandType.StoredProcedure);
            if (table_Tree1.Rows.Count > 0)
            {
                TreeNode treeChild1 = new TreeNode
                {
                    Text = "List RQ Not Approve"
                };

                treeNodes.ChildNodes.Add(treeChild1);
                for (int i = 0; i < table_Tree1.Rows.Count; i++)
                {
                    TreeNode treeChild3 = new TreeNode
                    {
                        Text = table_Tree1.Rows[i]["RequestNo"].ToString()
                    };
                    treeChild1.ChildNodes.Add(treeChild3);
                }
            }

            DataTable table_Tree2 = new DataTable();
            table_Tree2 = DataConn.StoreFillDS("SP_Issue_Material_Approved_OutSAP_Approved", CommandType.StoredProcedure);
            if (table_Tree2.Rows.Count > 0)
            {
                TreeNode treeChild2 = new TreeNode
                {
                    Text = "List RQ Approved"
                };

                treeNodes.ChildNodes.Add(treeChild2);
                for (int i = 0; i < table_Tree2.Rows.Count; i++)
                {
                    TreeNode treeChild4 = new TreeNode
                    {
                        Text = table_Tree2.Rows[i]["RequestNo"].ToString()
                    };
                    treeChild2.ChildNodes.Add(treeChild4);
                    // Search(table_Tree2.Rows[i]["RequestNo"].ToString());
                }
            }
        }
        protected void prtLoadRQ_CD_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
           

                 //case "Approved":


                 //   if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                 //   {
                 //       string CDNo = (item.FindControl("txtCDNO") as TextBox).Text;

                 //       string DateCD = (item.FindControl("txtCDSDate") as TextBox).Text;



                 //       int status = DataConn.ExecuteStore("SP_Issue_Material_Approved_OutSAP", CommandType.StoredProcedure, RQ, CDNo, DateCD);

                 //       if (status == 0)
                 //       {
                 //           Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Save CD  & CDS Date successfully');", true);
                 //       }
                 //       else
                 //       {
                 //           Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG.Save have error');", true);
                 //       }
                 //   }

                 //   break;



            }

        protected void bttApproved_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;
            string RQ  = (item.FindControl("lblRQ") as Label).Text;
            DataTable dtUserCheck = DataConn.DataTable_Sql("select RoleID from  [dbo].[tbl_UserIssueRQ]  where  Dept = 'PUR' and UserLogin = '"+Session["UserName"].ToString()+"' ");
            
            if(dtUserCheck.Rows.Count > 0 ) 
            {
                string RoleUser = dtUserCheck.Rows[0]["RoleID"].ToString();
                if(RoleUser == "3")
                {
                    int status = DataConn.ExecuteStore("[SP_Issue_Material_Approved_OutSAP]", CommandType.StoredProcedure, RQ, Session["UserName"].ToString());

                    if (status == 0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Approved Out SAP successfully');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG.Approved have error');", true);
                    }
                    LoadData();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('You don not permistion out SAP.');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('You don not permistion out SAP.');", true);
            }

            
        }

        protected void bttUpdate_Click(object sender, EventArgs e)
        {
            
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;
            string RQ = (item.FindControl("lblRQ") as Label).Text;
            string CDNo = (item.FindControl("txtCDNO") as TextBox).Text;
            string DateCD = (item.FindControl("txtCDSDate") as TextBox).Text;
            Boolean check = true ;
            if (CDNo == null || CDNo  =="")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('CDNo is NULL. Please input value again. ');", true);
                check = false;
            }

            if (DateCD == null || DateCD =="")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('Date CD is NULL. Please input value again. ');", true);
                check = false;
            }
            else
            {
                string pattern = "^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}$";
                Match match = Regex.Match(DateCD, pattern);
                if (match.Success)
                {
                    check = true;
                }    
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('Check date format it must be in dd/mm/yyyy');", true);
                    check = false;
                }    
                
            }


            if (check == true)
            {
                int status = DataConn.ExecuteStore("SP_Issue_Material_Update_CDS", CommandType.StoredProcedure, RQ, CDNo, DateCD);

                if (status == 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Save CD  & CDS Date successfully');", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('NG.Save have error');", true);
                }
            }    
           
            LoadData();
        }

        protected void bttPrintPDF_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;
            string RQ = (item.FindControl("lblRQ") as Label).Text;
            string titleReport = "Report Issue IN-OUT Material";
            DataTable dt_ReportAll = new DataTable();
            DataTable dt_Status = new DataTable();
            dt_ReportAll = DataConn.StoreFillDS("SP_Issue_Material_Report_PUR", CommandType.StoredProcedure, RQ);
            dt_Status = DataConn.StoreFillDS("SP_Issue_Material_RQStatus_PUR", CommandType.StoredProcedure, RQ);
            if (dt_ReportAll.Rows.Count > 0 && dt_Status.Rows.Count > 0)
            {
                PUR_Report PUR_Report_ = new PUR_Report(dt_ReportAll, dt_Status, titleReport);
                // Create a MigraDoc document
                Document document = PUR_Report_.CreateDocument();
                document.UseCmykColor = true;
                PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always)
                {
                    Document = document
                };
                pdfRenderer.RenderDocument();
                string filename = "REPORT FOR ISSUE IN-OUT MATERIAl";
                string lastfile = ".pdf";
                string link_report = Server.MapPath("~/Out_Report/" + filename + "_" + DateTime.Now.ToString("yyyy-MM-dd") + lastfile);
                // I don't want to close the document constantly...
                pdfRenderer.Save(link_report);
                //Process.Start(link_report);
                string path = Server.MapPath("~/Out_Report/" + filename + "" + "_" + "" + DateTime.Now.ToString("yyyy-MM-dd") + "" + lastfile + " ");
                WebClient client = new WebClient();
                byte[] buffer = client.DownloadData(path);
                if (buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                }
                File.Delete(link_report);

            }

            
        }

        protected void treeRQ_OutSap_SelectedNodeChanged(object sender, EventArgs e)
        {
            string RQ = treeRQ_OutSap.SelectedNode.Value.ToString();
            var dt = DataConn.StoreFillDS("SP_Invoice_ListRQ_OutSAP_Search", CommandType.StoredProcedure, RQ);
            if (dt.Rows.Count > 0)
            {
                prtLoadRQ_CD.DataSource = dt;
                prtLoadRQ_CD.DataBind();

            }
        }
    }
}