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
using System;
using System.Data.SqlClient;
using System.Data.Common;

namespace MATERIAL_IN_OUT.AppCode
{

    public class DataConn
    {
        public DataConn()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static int day1;
        public static string month1;
        public static int year1;
        public static int month2;
        public static string E_Month;
        public static string source;
        private static SqlConnection con;
        public static int gio;
        public static DataAdapter adapter;
        // static String result;
        public static string path = "~/CELL_MG/Employee_Images/";
        public static string path2 = "~/Images/";
        public static string[] extensions = { ".gif", ".jpg", ".bmp", ".jpeg", ".png" };
        public static string ImagePath(string MaNV)
        {
            FileInfo file;
            for (int e = 0; e < extensions.Length; e++)
            {
                file = new FileInfo(path + MaNV + extensions[e]);
                if (file.Exists)
                {
                    return path + MaNV + extensions[e];
                }
            }
            return path + "Noimg.jpg";
        }
        public static DateTime chiaca()
        {
            string HT1;
            gio = DateTime.Now.Hour;
            if (gio == 0 || gio == 1 || System.DateTime.Now.Hour == 2 || gio == 3 || gio == 4 || gio == 5 || gio == 6)
            {
                HT1 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }
            else
            {
                HT1 = DateTime.Now.ToString("yyyy-MM-dd");
            }

            return Convert.ToDateTime(HT1);
        }
        public static DateTime chiaca1()
        {
            DateTime HT1;
            gio = DateTime.Now.Hour;
            if (gio == 0 || gio == 1 || DateTime.Now.Hour == 2 || gio == 3 || gio == 4 || gio == 5 || gio == 6)
            {
                HT1 = DateTime.Now.AddDays(-1);
            }
            else
            {
                HT1 = DateTime.Now;
            }
            return HT1;
        }
        static DataConn()
        {
            // source = @"Data Source=ADMINISTRATOR;Initial Catalog=PRICE_SANCTION;User ID=sa;Password=cvn@2019";
            source = @"Data Source=10.92.186.30;Initial Catalog=Issue_MaterialInOut;User ID=sa;Password=Psnvdb2013";
            //source = @"Data Source=.\;Initial Catalog=Issue_MaterialInOut;Integrated Security=True";

            con = new SqlConnection(source);
            try

            {
                con.Open();
            }
            catch
            {
            }
        }
        public static DataTable DataTable_Sql(string sql)
        {
            using (SqlConnection conn = new SqlConnection(source))
            {
                using (SqlDataAdapter dap = new SqlDataAdapter(sql, conn))
                {
                    using (DataSet ds = new DataSet())
                    {
                        dap.Fill(ds);
                        conn.Close();
                        conn.Dispose();
                        return ds.Tables[0];
                    }
                }
            }
        }



        public static SqlDataReader GetData(string commandText)
        {
            using (SqlConnection conn = new SqlConnection(source))
            {
                conn.Open();
                using (SqlCommand sqlcmd = new SqlCommand(commandText, conn))
                {
                    SqlDataReader dr = sqlcmd.ExecuteReader();
                    conn.Close();
                    conn.Dispose();
                    return dr;
                }
            }
        }
        public static int Execute_NonSQL(string sql)
        {
            SqlTransaction sqltran = null;
            //try
            //{
            SqlConnection conn = new SqlConnection(source);

            int row = 0;
            conn.Open();
            sqltran = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand(sql, conn, sqltran);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            row = cmd.ExecuteNonQuery();
            sqltran.Commit();
            conn.Dispose();
            conn.Close();
            return row;
            //}
            //catch (Exception ex)
            //{
            //    // throw new Exception(ex.Message);
            //    sqltran.Rollback();
            //    var _ex = new Exception(ex.Message + "Chỗ này sai rồi... : '" + sql + "'");
            //    throw _ex;
            //}
        }
        public static DataTable StoreFillDS(string query_object, CommandType type, params object[] obj)
        {
            SqlConnection conn = new SqlConnection(source);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query_object, conn);
            cmd.CommandType = type;
            SqlCommandBuilder.DeriveParameters(cmd);
            for (int i = 1; i <= obj.Length; i++)
            {
                cmd.Parameters[i].Value = obj[i - 1];
            }
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet(); 
            dap.Fill(ds);
            conn.Dispose();
            conn.Close();
            return ds.Tables[0];
        }
        private static string key = "1prt56";

        public static string Encrypt(string Encryptval)
        {
            byte[] SrctArray;
            byte[] EnctArray = UTF8Encoding.UTF8.GetBytes(Encryptval);
            SrctArray = UTF8Encoding.UTF8.GetBytes(key);
            TripleDESCryptoServiceProvider objt = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objcrpt = new MD5CryptoServiceProvider();
            SrctArray = objcrpt.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            objcrpt.Clear();
            objt.Key = SrctArray;
            objt.Mode = CipherMode.ECB;
            objt.Padding = PaddingMode.PKCS7;
            ICryptoTransform crptotrns = objt.CreateEncryptor();
            byte[] resArray = crptotrns.TransformFinalBlock(EnctArray, 0, EnctArray.Length);
            objt.Clear();
            return Convert.ToBase64String(resArray, 0, resArray.Length);
        }

        public  static string Decrypt(string DecryptText)
        {
            byte[] SrctArray;


            DecryptText = DecryptText.Replace(" ", "+");
            int mod4 = DecryptText.Length % 4;
            if (mod4 > 0)
            {
                DecryptText += new string('=', 4 - mod4);
            }
            byte[] DrctArray = Convert.FromBase64String(DecryptText);
          //  byte[] DrctArray = Encoding.ASCII.GetBytes(DecryptText);
            SrctArray = UTF8Encoding.UTF8.GetBytes(key);
            TripleDESCryptoServiceProvider objt = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objmdcript = new MD5CryptoServiceProvider();
            SrctArray = objmdcript.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            objmdcript.Clear();
            objt.Key = SrctArray;
            objt.Mode = CipherMode.ECB;
            objt.Padding = PaddingMode.PKCS7;
            ICryptoTransform crptotrns = objt.CreateDecryptor();
            byte[] resArray = crptotrns.TransformFinalBlock(DrctArray, 0, DrctArray.Length);
            objt.Clear();
            return UTF8Encoding.UTF8.GetString(resArray);
        }

  
        //Store Procedure tra ve datatable
        public static int ExecuteStore(string query_object, CommandType type, params object[] obj)
        {
            int row = 0;
            SqlConnection conn = new SqlConnection(source);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query_object, conn);
            cmd.CommandType = type;
            SqlCommandBuilder.DeriveParameters(cmd);
            for (int i = 1; i <= obj.Length; i++)
            {
                cmd.Parameters[i].Value = obj[i - 1];
            }
            cmd.ExecuteNonQuery();
            conn.Dispose();
            conn.Close();
            return row;
        }

        //Store Procedure tra ve datatable
        public static DataTable FillStore(string storename, CommandType type, params object[] obj)
        {
            SqlConnection conn = new SqlConnection(source);
            conn.Open();
            SqlCommand cmd = new SqlCommand(storename, conn);
            cmd.CommandType = type;
            SqlCommandBuilder.DeriveParameters(cmd);
            for (int i = 1; i <= obj.Length; i++)
            {
                cmd.Parameters[i].Value = obj[i - 1];
            }
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            //cmd.ExecuteNonQuery();
            dap.Fill(ds);
            conn.Dispose();
            conn.Close();
            return ds.Tables[0];

        }

        public static void ShowNoResultFound(DataTable source, GridView gv)
        {
            source.Rows.Add(source.NewRow()); // create a new blank row to the DataTable
                                              // Bind the DataTable which contain a blank row to the GridView
            gv.DataSource = source;
            gv.DataBind();
            // Get the total number of columns in the GridView to know what the Column Span should be
            //int columnsCount = gv.Columns.Count;
            int columnsCount = gv.Rows[0].Cells.Count;
            gv.Rows[0].Cells.Clear();// clear all the cells in the row
            gv.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
            gv.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell
            //You can set the styles here
            gv.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            gv.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
            gv.Rows[0].Cells[0].Font.Bold = true;
            //set No Results found to the new added cell
            gv.Rows[0].Cells[0].Text = "NO RECORD FOUND!";
            //gv.Rows[0].Cells[1].Text = "NO RESULT FOUND!";
            //gvDetails.DataSource = ds;
            //gvDetails.DataBind();
            //int columncount = gvDetails.Rows[0].Cells.Count;
            //gvDetails.Rows[0].Cells.Clear();
            //gvDetails.Rows[0].Cells.Add(new TableCell());
            //gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
            //gvDetails.Rows[0].Cells[0].Text = "No Records Found";
        }
        public static string returnQuater()
        {
            string q = "Q1";
            int h = DateTime.Now.Hour;
            if (h == 8 || h == 9 || h == 21 || h == 22)
            {
                q = "Q1";
            }
            if (h == 10 || h == 11 || h == 23 || h == 0)
            {
                q = "Q2";
            }
            if (h == 13 || h == 14 || h == 2 || h == 3)
            {
                q = "Q3";
            }
            if (h == 15 || h == 16 || h == 4 || h == 6)
            {
                q = "Q4";
            }
            return q;
        }
        //Ham lay ngay cuoi cung cua thang truyen vao
        public static DateTime GetLastDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }


    }

}