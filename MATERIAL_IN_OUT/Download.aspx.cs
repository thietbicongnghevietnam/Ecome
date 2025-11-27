
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
    
    public partial class Download : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                txtDateFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");

            }
                
        }

        protected void bttDownload_InOut_Click(object sender, EventArgs e)
        {
            DataTable dt_ExportExcel = new DataTable();
            string FromDate = txtDateFrom.Text.ToString();
            string ToDate = txtDateTo.Text.ToString();
            dt_ExportExcel = DataConn.FillStore("Download_IssueIn_Out", CommandType.StoredProcedure, FromDate, ToDate);

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Download_Material_InOut.xls");
            Response.Charset = "";
            Response.ContentType = "application/ms-excel";

            if (dt_ExportExcel != null)
            {
                foreach (DataColumn dc in dt_ExportExcel.Columns)
                {
                    Response.Write(dc.ToString().Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty) + "\t");
                    //Response.Write(dc.ToString() + "\t");

                }
                Response.Write(System.Environment.NewLine);
                foreach (DataRow dr in dt_ExportExcel.Rows)
                {
                    for (int i = 0; i < dt_ExportExcel.Columns.Count; i++)
                    {
                        Response.Write(dr[i].ToString().Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty) + "\t");
                        //Response.Write(dr[i].ToString() + "\t");
                    }
                    Response.Write("\n");
                }

            }
            Response.End();
        }

        protected void bttDownloadInvoice_Click(object sender, EventArgs e)
        {

            DataTable dt_ExportExcel = new DataTable();
            string FromDate = txtDateFrom.Text.ToString();
            string ToDate = txtDateTo.Text.ToString();
            dt_ExportExcel = DataConn.FillStore("Download_InvoiceComnution", CommandType.StoredProcedure, FromDate, ToDate);

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Download_Invoice.xls");
            Response.Charset = "";
            Response.ContentType = "application/ms-excel";

            if (dt_ExportExcel != null)
            {
                foreach (DataColumn dc in dt_ExportExcel.Columns)
                {
                    Response.Write(dc.ToString().Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty) + "\t");
                    //Response.Write(dc.ToString() + "\t");

                }
                Response.Write(System.Environment.NewLine);
                foreach (DataRow dr in dt_ExportExcel.Rows)
                {
                    for (int i = 0; i < dt_ExportExcel.Columns.Count; i++)
                    {
                        Response.Write(dr[i].ToString().Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty) + "\t");
                        //Response.Write(dr[i].ToString() + "\t");
                    }
                    Response.Write("\n");
                }

            }
            Response.End();

        }
    }
}