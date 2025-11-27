using System;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;

namespace MATERIAL_IN_OUT
{
    public class PDF_Tranfer
    {
        public PDF_Tranfer()
        {
        }

        public static Document Doc;
        public static DataTable dt;
        public static DataTable dtStatus;
        public static string path;
        public static Table table;
        public static Table Table_Approved;
        public static TextFrame detailFrame;
        public static String TitleReport;
        public PDF_Tranfer(DataTable dt_Data, DataTable dt_Status, String str_Titles)
        {
            dt = dt_Data;
            dtStatus = dt_Status;
            TitleReport = str_Titles;
        }
        public Document CreateDocument()
        {
            // Create a new MigraDoc document
            Doc = new Document();
            Doc.Info.Title = "Internal Tranfer Material ";
            Doc.Info.Subject = "";
            Doc.Info.Author = "NGUYEN THI DAO";

            DefineStyles();

            CreatePage();

            FillContent();

            return Doc;
        }
        public static void DefineStyles()
        {
            // Get the predefined style Normal.
            Style style = Doc.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif";

            style = Doc.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("30cm", TabAlignment.Right);

            style = Doc.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("30cm", TabAlignment.Center);

            style = Doc.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif";
            style.Font.Size = 9;

            // Create a new style called Reference based on style Normal
            style = Doc.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("30cm", TabAlignment.Right);
        }
        public static void CreatePage()
        {
            // Each MigraDoc document needs at least one section.
            Section section = Doc.AddSection();
            section.PageSetup = Doc.DefaultPageSetup.Clone();
            section.PageSetup.PageFormat = PageFormat.Letter; // Has no effect after Clone(), just for documentation purposes.
            section.PageSetup.PageWidth = Unit.FromPoint(1100);
            section.PageSetup.PageHeight = Unit.FromPoint(750);
            section.PageSetup.TopMargin = "0.5cm";
            section.PageSetup.LeftMargin = "1cm";
            section.PageSetup.RightMargin = "1cm";
            section.PageSetup.BottomMargin = "0.5cm";


            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "2cm";
            paragraph.Style = "Normal";
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.Format.Font.Size = 7;
            paragraph.Format.SpaceAfter = 3;
            paragraph.AddLineBreak();

            // Detail
            var DetailReaquest = section.AddParagraph();
            DetailReaquest.Format.Font.Size = 14;
            DetailReaquest.AddFormattedText("Panasonic System Network VietNam Co.,Ltd", TextFormat.Bold);
            DetailReaquest.AddSpace(80);
            DetailReaquest.AddFormattedText("TRANSFER REQUEST", TextFormat.Bold);
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddText("Plot J1-J2, Thang Long industrial Park");
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddText("Dong Anh Dist- Ha Noi");
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddFormattedText("Issue Date:" + dtStatus.Rows[0]["IssueDate"].ToString(), TextFormat.NotBold);
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddFormattedText("Slip No:" + dtStatus.Rows[0]["SlipNo"].ToString(), TextFormat.NotBold);
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddFormattedText("Tran.Code: " + dtStatus.Rows[0]["TranCode"].ToString(), TextFormat.NotBold);
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddFormattedText("Mv.Type: " + dtStatus.Rows[0]["MvType"].ToString(), TextFormat.NotBold);
            DetailReaquest.AddSpace(1);
            //----------------------------------------------------
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "0.1cm";
            paragraph.AddSpace(20);
            //----------------------------------------------------
            // Create footer
            //Define the of table approved

            var Table_Approved = section.AddTable();
            Table_Approved.Style = "Table";
            Table_Approved.Borders.Color = TableBorder;
            Table_Approved.Borders.Width = 0.1;
            Table_Approved.Borders.Left.Width = 0.1;
            Table_Approved.Borders.Right.Width = 0.1;
            Table_Approved.Rows.LeftIndent = 480;
            Table_Approved.Rows.Height = "1cm";
            Table_Approved.LeftPadding = 2;
            Table_Approved.RightPadding = 2;
            Table_Approved.TopPadding = 2;

            //  Define the columns
            Column column_ = Table_Approved.AddColumn("3cm");
            column_.Format.Alignment = ParagraphAlignment.Center;
            column_ = Table_Approved.AddColumn("3cm");
            column_.Format.Alignment = ParagraphAlignment.Center;
            column_ = Table_Approved.AddColumn("3cm");
            column_.Format.Alignment = ParagraphAlignment.Center;
            column_ = Table_Approved.AddColumn("3cm");
            column_.Format.Alignment = ParagraphAlignment.Center;
            column_ = Table_Approved.AddColumn("3cm");
            column_.Format.Alignment = ParagraphAlignment.Center;
            column_ = Table_Approved.AddColumn("3cm");
            column_.Format.Alignment = ParagraphAlignment.Center;
           
            //  Define the rows
            Row row_ = Table_Approved.AddRow();
            row_.HeadingFormat = true;
            row_.Height = 25;
            row_.Format.Alignment = ParagraphAlignment.Center;
            row_.Format.Font.Bold = true;
            row_.Shading.Color = TableBlue;
            row_.Cells[0].AddParagraph("");
            row_.Cells[0].Format.Font.Bold = true;
            row_.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row_.Cells[1].AddParagraph("Department");
            row_.Cells[1].Format.Font.Bold = true;
            row_.Cells[1].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[1].MergeRight = 1;

            row_.Cells[3].AddParagraph("Section Incharge");
            row_.Cells[3].Format.Font.Bold = true;
            row_.Cells[3].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[3].MergeRight = 2;

            //
            row_ = Table_Approved.AddRow();
            row_.HeadingFormat = true;
            row_.Format.Alignment = ParagraphAlignment.Center;
            row_.Format.Font.Bold = false;
            row_.Cells[0].AddParagraph("");
            row_.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[1].AddParagraph("Issued by");
            row_.Cells[1].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[2].AddParagraph("Approved by");
            row_.Cells[2].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[3].AddParagraph("Input");
            row_.Cells[3].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[4].AddParagraph("Checked by");
            row_.Cells[4].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[5].AddParagraph("Approved by");
            row_.Cells[5].VerticalAlignment = VerticalAlignment.Center;



            //Create Row2
            row_ = Table_Approved.AddRow();
            row_.HeadingFormat = true;
            row_.Format.Alignment = ParagraphAlignment.Center;
            row_.Format.Font.Bold = false;
            row_.Cells[0].AddParagraph("Status");
            row_.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[1].AddParagraph(dtStatus.Rows[0]["StatusInchage_Dept"].ToString());
            row_.Cells[1].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[2].AddParagraph(dtStatus.Rows[0]["StatusApproved_Dept"].ToString());
            row_.Cells[2].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[3].AddParagraph(dtStatus.Rows[0]["StatusInput_Incharge"].ToString());
            row_.Cells[3].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[4].AddParagraph(dtStatus.Rows[0]["StatusCheck_Incharge"].ToString());
            row_.Cells[4].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[5].AddParagraph(dtStatus.Rows[0]["StatusApproved_Incharge"].ToString());
            row_.Cells[5].VerticalAlignment = VerticalAlignment.Center;
            //Create Row3
            row_ = Table_Approved.AddRow();
            row_.HeadingFormat = false;
            row_.Format.Alignment = ParagraphAlignment.Center;
            row_.Cells[0].AddParagraph("Name");
            row_.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[1].AddParagraph(dtStatus.Rows[0]["NameInchage_Dept"].ToString());
            row_.Cells[1].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[2].AddParagraph(dtStatus.Rows[0]["NameApproved_Dept"].ToString());
            row_.Cells[2].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[3].AddParagraph(dtStatus.Rows[0]["NameInput_Incharge"].ToString());
            row_.Cells[3].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[4].AddParagraph(dtStatus.Rows[0]["NameCheck_Incharge"].ToString());
            row_.Cells[4].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[5].AddParagraph(dtStatus.Rows[0]["NameApproved_Incharge"].ToString());
            row_.Cells[5].VerticalAlignment = VerticalAlignment.Center;
          
            //Create Row4
            row_ = Table_Approved.AddRow();
            row_.HeadingFormat = false;
            row_.Format.Alignment = ParagraphAlignment.Center;
            row_.Format.Font.Bold = false;
            row_.Cells[0].AddParagraph("Date Time");
            row_.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[1].AddParagraph(dtStatus.Rows[0]["DateInchage_Dept"].ToString());
            row_.Cells[1].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[2].AddParagraph(dtStatus.Rows[0]["DateApproved_Dept"].ToString());
            row_.Cells[2].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[3].AddParagraph(dtStatus.Rows[0]["DateInput_Incharge"].ToString());
            row_.Cells[3].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[4].AddParagraph(dtStatus.Rows[0]["DateCheck_Incharge"].ToString());
            row_.Cells[4].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[5].AddParagraph(dtStatus.Rows[0]["DateApproved_Incharge"].ToString());
            row_.Cells[5].VerticalAlignment = VerticalAlignment.Center;
            Table_Approved.SetEdge(0, Table_Approved.Rows.Count - 2, 6, 2, Edge.Box, BorderStyle.Single, 0.1, Color.Empty);

            //---------------------------

            //---------------------------z
            Paragraph paragraph_Date = section.AddParagraph();
            paragraph_Date.Format.SpaceBefore = "2cm";
            paragraph_Date.Style = "Reference";
            paragraph_Date.AddFormattedText("Request Detail", TextFormat.Bold);
            paragraph_Date.AddTab();
            paragraph_Date.AddText("Date, ");
            paragraph_Date.AddDateField("dd.MM.yyyy");


            // Create the item table
            table = section.AddTable();
            table.Style = "Table";
            
            table.Borders.Color = TableBorder;
            table.Borders.Width = 0.1;
            table.Borders.Left.Width = 0.1;
            table.Borders.Right.Width = 0.1;

            table.Rows.Height = "1cm";
            table.LeftPadding = 2;
            table.RightPadding = 2;
            table.TopPadding = 1;
            table.BottomPadding = 1;
            // Before you can add a row, you must define the columns
            Column column;
            foreach (DataColumn col in dt.Columns)
            {
                column = table.AddColumn(Unit.FromCentimeter(3));
                column.Format.Alignment = ParagraphAlignment.Center;
                
            }
            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.VerticalAlignment = VerticalAlignment.Bottom;
            row.Format.Font.Bold = false;
        


            for (int i = 0; i < dt.Columns.Count; i++)
            {

                row.Cells[i].AddParagraph(dt.Columns[i].ColumnName);
                row.Cells[i].Format.Font.Bold = false;
                row.Cells[i].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[i].VerticalAlignment = VerticalAlignment.Bottom;


            }
            table.SetEdge(1, 1, dt.Columns.Count, 1, Edge.Box, BorderStyle.Single, 0.1, Color.Empty);
        }
        public static void FillContent()
        {
            // Fill address in address text frame
            Row row1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row1 = table.AddRow();

                row1.TopPadding = 2.5;


                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    //row1.Cells[j].Shading.Color = TableGray;
                    row1.Cells[j].VerticalAlignment = VerticalAlignment.Center;
                    row1.Cells[j].Format.Alignment = ParagraphAlignment.Left;
                    row1.Cells[j].Format.FirstLineIndent = 5;
                    row1.Cells[j].AddParagraph(dt.Rows[i][j].ToString());
                    table.SetEdge(0, table.Rows.Count - 2, dt.Columns.Count, 1, Edge.Box, BorderStyle.Single, 0.1);
                }
            }

        }
        // Some pre-defined colors
#if true
        // RGB colors
        readonly static Color TableBorder = new Color(81, 125, 192);
        readonly static Color TableBlue = new Color(235, 240, 249);
        readonly static Color TableGray = new Color(242, 242, 242);
#else
            // CMYK colors
            readonly static Color tableBorder = Color.FromCmyk(100, 50, 0, 30);
            readonly static Color tableBlue = Color.FromCmyk(0, 80, 50, 30);
            readonly static Color tableGray = Color.FromCmyk(30, 0, 0, 0, 100);
#endif
    }
}