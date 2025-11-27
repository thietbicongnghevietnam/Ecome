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
    public class PDF_A
    {

        public PDF_A()
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
        public PDF_A(DataTable dt_Data, DataTable dt_Status, String str_Titles)
        {
            dt = dt_Data;
            dtStatus = dt_Status;
            TitleReport = str_Titles;
        }
        public Document CreateDocument()
        {
            // Create a new MigraDoc document
            Doc = new Document();
            Doc.Info.Title = "Issue IN-OUT Material ";
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
            DetailReaquest.AddFormattedText("REPORT ISSUE IN-OUT MATERIAL", TextFormat.Bold);
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddText("Plot J1-J2, Thang Long industrial Park");
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddText("Dong Anh Dist- Ha Noi");
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddFormattedText("Voucher Date:" + dtStatus.Rows[0]["DateVoucher"].ToString(), TextFormat.NotBold);
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddFormattedText("MvTYpe:" + dtStatus.Rows[0]["MvType"].ToString(), TextFormat.NotBold);
            DetailReaquest.AddSpace(1);
            DetailReaquest.AddFormattedText(" - " + dtStatus.Rows[0]["MvName"].ToString(), TextFormat.NotBold);
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddFormattedText("Plant: " + dtStatus.Rows[0]["Plant"].ToString(), TextFormat.NotBold);
            DetailReaquest.AddSpace(1);
            DetailReaquest.AddFormattedText(" - " + dtStatus.Rows[0]["NamePlant"].ToString(), TextFormat.NotBold);
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddFormattedText("Account: " + dtStatus.Rows[0]["AccountCost"].ToString(), TextFormat.NotBold);
            DetailReaquest.AddSpace(1);
            DetailReaquest.AddFormattedText(" - " + dtStatus.Rows[0]["AccountName"].ToString(), TextFormat.NotBold);
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddFormattedText("Cost center: " + dtStatus.Rows[0]["CostCenter"].ToString(), TextFormat.NotBold);
            DetailReaquest.AddSpace(1);
            DetailReaquest.AddFormattedText(" - " + dtStatus.Rows[0]["DeptName"].ToString(), TextFormat.NotBold);
            DetailReaquest.AddLineBreak();
            DetailReaquest.AddFormattedText("Vendor Code: " + dtStatus.Rows[0]["VendorCode"].ToString(), TextFormat.NotBold);
            //----------------------------------------------------
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "0.1cm";
            paragraph.AddSpace(20);
            //----------------------------------------------------
            // Create footer
            //Define the of table approved

            var Table_Approved = section.AddTable();
            Table_Approved.Style = "Table";
          //  Table_Approved.Borders.Color = TableBorder;
            Table_Approved.Borders.Width = 0.1;
            Table_Approved.Borders.Left.Width = 0.1;
            Table_Approved.Borders.Right.Width = 0.1;
            Table_Approved.Rows.LeftIndent = 170;
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
           // row_.Shading.Color = TableBorder;
            row_.Cells[0].AddParagraph("");
            row_.Cells[0].Format.Font.Bold = true;
            row_.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row_.Cells[1].AddParagraph("Issue Dept");
            row_.Cells[1].Format.Font.Bold = true;
            row_.Cells[1].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[1].MergeRight = 2;
            

            row_.Cells[4].AddParagraph("Accounting");
            row_.Cells[4].Format.Font.Bold = true;
            row_.Cells[4].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[4].MergeRight = 2;

            row_.Cells[7].AddParagraph("Issue Out Dept");
            row_.Cells[7].Format.Font.Bold = true;
            row_.Cells[7].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[7].MergeRight = 2;
            //--------------------
            row_ = Table_Approved.AddRow();
            row_.HeadingFormat = true;
            row_.Format.Alignment = ParagraphAlignment.Center;
            row_.Format.Font.Bold = false;
            row_.Cells[0].AddParagraph("");
            row_.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[1].AddParagraph("Incharge");
            row_.Cells[1].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[2].AddParagraph("Check");
            row_.Cells[2].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[3].AddParagraph("MGR(GM) Approved");
            row_.Cells[3].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[4].AddParagraph("Check");
            row_.Cells[4].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[5].AddParagraph("MGR Approved");
            row_.Cells[5].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[6].AddParagraph("GM Approved");
            row_.Cells[6].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[7].AddParagraph("Check");
            row_.Cells[7].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[8].AddParagraph("MGR Approved");
            row_.Cells[8].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[9].AddParagraph("GM Approved");
            row_.Cells[9].VerticalAlignment = VerticalAlignment.Center;

            //Create Row2
            row_ = Table_Approved.AddRow();
            row_.HeadingFormat = true;
            row_.Format.Alignment = ParagraphAlignment.Center;
            row_.Format.Font.Bold = false;
            row_.Cells[0].AddParagraph("Status");
            row_.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[1].AddParagraph(dtStatus.Rows[0]["StatusInchage_Issue"].ToString());
            row_.Cells[1].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[2].AddParagraph(dtStatus.Rows[0]["StatusMGR_Issue"].ToString());
            row_.Cells[2].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[3].AddParagraph(dtStatus.Rows[0]["StatusGM_Issue"].ToString());
            row_.Cells[3].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[4].AddParagraph(dtStatus.Rows[0]["StatusACC_Check"].ToString());
            row_.Cells[4].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[5].AddParagraph(dtStatus.Rows[0]["StatusACC_MGR"].ToString());
            row_.Cells[5].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[6].AddParagraph(dtStatus.Rows[0]["StatusACC_GM"].ToString());
            row_.Cells[6].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[7].AddParagraph(dtStatus.Rows[0]["StatusInchage_Out"].ToString());
            row_.Cells[7].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[8].AddParagraph(dtStatus.Rows[0]["StatusMGR_Out"].ToString());
            row_.Cells[8].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[9].AddParagraph(dtStatus.Rows[0]["StatusGM_Out"].ToString());
            row_.Cells[9].VerticalAlignment = VerticalAlignment.Center;
            //Create Row3
            row_ = Table_Approved.AddRow();
            row_.HeadingFormat = false;
            row_.Format.Alignment = ParagraphAlignment.Center;
            row_.Cells[0].AddParagraph("Name");
            row_.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[1].AddParagraph(dtStatus.Rows[0]["NameInchage_Issue"].ToString());
            row_.Cells[1].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[2].AddParagraph(dtStatus.Rows[0]["NameMGR_Issue"].ToString());
            row_.Cells[2].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[3].AddParagraph(dtStatus.Rows[0]["NameGM_Issue"].ToString());
            row_.Cells[3].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[4].AddParagraph(dtStatus.Rows[0]["NameACC_Check"].ToString());
            row_.Cells[4].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[5].AddParagraph(dtStatus.Rows[0]["NameACC_MGR"].ToString());
            row_.Cells[5].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[6].AddParagraph(dtStatus.Rows[0]["NameACC_GM"].ToString());
            row_.Cells[6].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[7].AddParagraph(dtStatus.Rows[0]["NameInchage_Out"].ToString());
            row_.Cells[7].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[8].AddParagraph(dtStatus.Rows[0]["NameMGR_Out"].ToString());
            row_.Cells[8].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[9].AddParagraph(dtStatus.Rows[0]["NameGM_Out"].ToString());
            row_.Cells[9].VerticalAlignment = VerticalAlignment.Center;

            //Create Row4
            row_ = Table_Approved.AddRow();
            row_.HeadingFormat = false;
            row_.Format.Alignment = ParagraphAlignment.Center;
            row_.Format.Font.Bold = false;
            row_.Cells[0].AddParagraph("Date Time");
            row_.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[1].AddParagraph(dtStatus.Rows[0]["DateInchage_Issue"].ToString());
            row_.Cells[1].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[2].AddParagraph(dtStatus.Rows[0]["DateMGR_Issue"].ToString());
            row_.Cells[2].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[3].AddParagraph(dtStatus.Rows[0]["DateGM_Issue"].ToString());
            row_.Cells[3].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[4].AddParagraph(dtStatus.Rows[0]["DateACC_Check"].ToString());
            row_.Cells[4].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[5].AddParagraph(dtStatus.Rows[0]["DateACC_MGR"].ToString());
            row_.Cells[5].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[6].AddParagraph(dtStatus.Rows[0]["DateACC_GM"].ToString());
            row_.Cells[6].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[7].AddParagraph(dtStatus.Rows[0]["DateInchage_Out"].ToString());
            row_.Cells[7].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[8].AddParagraph(dtStatus.Rows[0]["DateMGR_Out"].ToString());
            row_.Cells[8].VerticalAlignment = VerticalAlignment.Center;
            row_.Cells[9].AddParagraph(dtStatus.Rows[0]["DateGM_Out"].ToString());
            row_.Cells[9].VerticalAlignment = VerticalAlignment.Center;
            Table_Approved.SetEdge(0, Table_Approved.Rows.Count - 2, 10, 2, Edge.Box, BorderStyle.Single, 0.1, Color.Empty);

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
          //  table.Borders.Color = TableBorder;
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
                column = table.AddColumn(Unit.FromCentimeter(4.0));
                column.Format.Alignment = ParagraphAlignment.Center;
               // column.Shading.Color = TableBorder;
            }
            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = false;


            for (int i = 0; i < dt.Columns.Count; i++)
            {

                row.Cells[i].AddParagraph(dt.Columns[i].ColumnName);
                row.Cells[i].Format.Font.Bold = false;
                row.Cells[i].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[i].VerticalAlignment = VerticalAlignment.Bottom;
            }
            table.SetEdge(0, 0, dt.Columns.Count, 1, Edge.Box, BorderStyle.Single, 0.1, Color.Empty);


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
            readonly static Color TableBorder = Color.FromCmyk(0, 80, 50, 30);
            readonly static Color tableGray = Color.FromCmyk(30, 0, 0, 0, 100);
#endif

    }
}