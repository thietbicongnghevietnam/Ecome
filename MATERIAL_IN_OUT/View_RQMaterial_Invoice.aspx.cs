using MATERIAL_IN_OUT.AppCode;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MATERIAL_IN_OUT
{
    public partial class View_RQMaterial_Invoiceaspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string RequestNO = Request.QueryString["RQ"].ToString();

                string titleReport = "View Report Issue IN-OUT Material";
                DataTable dtView = new DataTable();
                DataTable dt_Status = new DataTable();

                dtView = DataConn.StoreFillDS("[SP_Invoice_MaterialIssue_View]", CommandType.StoredProcedure, RequestNO);
                dt_Status = DataConn.StoreFillDS("[SP_Invoice_ViewRQ_Status]", CommandType.StoredProcedure, RequestNO);


                PDF DataPDF = new PDF(dtView, dt_Status, titleReport);
                // Create a MigraDoc document
                Document document = DataPDF.CreateDocument();
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
    }
}