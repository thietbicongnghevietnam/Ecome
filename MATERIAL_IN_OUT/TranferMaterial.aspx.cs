
using MATERIAL_IN_OUT.AppCode;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MATERIAL_IN_OUT
{
    public partial class TranferMaterial : System.Web.UI.Page
    {
        public DataTable dtTranfer = new DataTable();
        public DataTable dt_Comment = new DataTable();
        private string Dept = "";
        private string DeptName = "ISD";
        private string ACCLOGIN = "1";
        private string Stock = "";
        private string EmailNext = "";
        private string Email_Pres = "";
        public string RQ = "";
        public static string header_;
        public DataTable dtPreEmail = new DataTable();
        public DataTable dtNextMail = new DataTable();
        public DataTable dtUserCurrrent = new DataTable();
        public DataTable dtTreeRQ = new DataTable();
        public DataTable dtCheckStatus = new DataTable();
        public DataTable dtLoadStock = new DataTable();
        public static string REQUESTID;
        public static string USER_NAME;
        public static string Public_Dept = "";
        public static string RequestNO = "";

        public object __o;
        public string User_next = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

            {
                string REQUESTID = Request.QueryString["RQ_NO"].ToString();
                User_next = Request.QueryString["UserName"].ToString();
                string ROLEID = Request.QueryString["Role"].ToString();
                string CostCenter = Request.QueryString["DeptID"].ToString();
                string StockFrom = Request.QueryString["StockFrom"].ToString();
                string StockTo = Request.QueryString["StockTo"].ToString();


                if (REQUESTID != "" && REQUESTID != null && User_next != "" && ROLEID != "")
                {
                    Session["RequestID_1RQ"] = DataConn.Decrypt(REQUESTID);
                    Session["UserName"] = DataConn.Decrypt(User_next);
                    Session["Role"] = DataConn.Decrypt(ROLEID);
                   Session["CostCenter"] = DataConn.Decrypt(CostCenter);
                    string StockFrom_ = DataConn.Decrypt(StockFrom);
                    string StockTo_ = DataConn.Decrypt(StockTo);
                    //1 Khi load thông tin của chuỗi mảng '2021,2022' sẽ thành '2021','2022' như sau:
                    if (StockFrom_.Length > 5)
                    {
                        Session["StockFrom"] = StockTo_.Split(',').ToList();
                    }


                    Load_TreeView_Search(Session["RequestID_1RQ"].ToString());
                    LoadData();
                    Public_Dept =Session["CostCenter"].ToString().Trim();

                }
                else
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]) && string.IsNullOrEmpty((string)Session["CostCenter"]) && string.IsNullOrEmpty((string)Session["UserName"]) ||
                     string.IsNullOrEmpty((string)Session["Role"]) && string.IsNullOrEmpty((string)Session["Stock"]) && string.IsNullOrEmpty((string)Session["Role_Dept"]))
                    {
                        Response.Redirect("Login.aspx", false);
                    }
                    else

                    {
                        Load_Treeview_Management();
                        LoadData();
                        Public_Dept =Session["CostCenter"].ToString().Trim();
                    }
                }
            }

        }

        public void Search(string RQ)
        {
            Public_Dept =Session["CostCenter"].ToString().Trim();
            dtTranfer = DataConn.FillStore("SP_MaterialTranfer_Search", CommandType.StoredProcedure, RQ);
            dt_Comment = DataConn.FillStore("SP_MaterialTranfer_Comment_Load", CommandType.StoredProcedure, RQ);
            if (dtTranfer.Rows.Count > 0)
            {
                hdfStockIssue.Value = dtTranfer.Rows[0]["IssueSloc"].ToString();
                hdfStockReice.Value = dtTranfer.Rows[0]["RecvSloc"].ToString();
            }

            // Get Role current of user

            dtUserCurrrent = DataConn.StoreFillDS("SP_Tranfer_CurrentUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept);
            string RoleDept = null;
            string Role = null;
            if (dtUserCurrrent.Rows.Count > 0)
            {
                RoleDept = dtUserCurrrent.Rows[0]["RoleDept"].ToString();
                Role = dtUserCurrrent.Rows[0]["RoleID"].ToString();
            }


            if (dtTranfer.Rows.Count > 0)
            {

                lblIssueDate.Text = dtTranfer.Rows[0]["IssueDate"].ToString();
                hdfStockIssue.Value = dtTranfer.Rows[0]["IssueSloc"].ToString();
                hdfStockReice.Value = dtTranfer.Rows[0]["RecvSloc"].ToString();
                hdfRequest.Value = dtTranfer.Rows[0]["RequestNo"].ToString();
                hdfRoleDeptUpdate.Value = Role.ToString();
                hdfRoleupdate.Value = RoleDept.ToString();
                hdfUserUpdate.Value = Session["UserName"].ToString();
                lblSlipNo.Text = dtTranfer.Rows[0]["SlipNo"].ToString();
                lblTranCode.Text = dtTranfer.Rows[0]["TranCode"].ToString();
                lblMoveType.Text = dtTranfer.Rows[0]["MvType"].ToString();
                Loadstatus(RQ);
                LoadButton(RQ);
            }

            else
            {
                if (RoleDept == "RQ")
                {
                    if (int.Parse(Role) == 1) // Show status button permistion Requester
                    {
                        bttUpload.Visible = true;
                        FileUpload1.Visible = true;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;

                        bttPrint.Visible = true;
                    }


                    if (int.Parse(Role) == 2) // Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;

                        bttPrint.Visible = true;
                    }
                }
                if (RoleDept == "STORE")
                {
                    if (int.Parse(Role) == 1)// Show status button permistion Requester
                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttPrint.Visible = true;
                    }
                    if (int.Parse(Role) == 2)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttPrint.Visible = true;
                    }
                    if (int.Parse(Role) == 3)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttPrint.Visible = true;
                    }
                }

            }
        }
        public void Loadstatus(string RequestNo)
        {
            // Get RequestNo
            Public_Dept =Session["CostCenter"].ToString().Trim();
            dtCheckStatus = DataConn.StoreFillDS("SP_MaterialTranfer_CheckStatus", CommandType.StoredProcedure, RequestNo);
            if (dtCheckStatus.Rows.Count > 0)
            {
                //---

                lblIssueDept_Name.Text = dtCheckStatus.Rows[0]["NameInchage_Dept"].ToString();
                lblApprovedDept_Name.Text = dtCheckStatus.Rows[0]["NameApproved_Dept"].ToString();
                lblInputSectionTo_Name.Text = dtCheckStatus.Rows[0]["NameInput_Incharge"].ToString();
                lblCheckSectionTo_Name.Text = dtCheckStatus.Rows[0]["NameCheck_Incharge"].ToString();
                lblApprovedSectionTo_Name.Text = dtCheckStatus.Rows[0]["NameApproved_Incharge"].ToString();
                hdf_IssueDept.Value = dtCheckStatus.Rows[0]["StatusInchage_Dept"].ToString();
                hdf_ApprovedDept.Value = dtCheckStatus.Rows[0]["StatusApproved_Dept"].ToString();
                hdf_InputSectionTo.Value = dtCheckStatus.Rows[0]["StatusInput_Incharge"].ToString();
                hdf_CheckSectionTo.Value = dtCheckStatus.Rows[0]["StatusCheck_Incharge"].ToString();
                hdf_ApprovedSectionTo.Value = dtCheckStatus.Rows[0]["StatusApproved_Incharge"].ToString();
                lblIssueDept_Date.Text = dtCheckStatus.Rows[0]["DateInchage_Dept"].ToString();
                lblApprovedDept_Date.Text = dtCheckStatus.Rows[0]["DateApproved_Dept"].ToString();

                lblInput_SectionTo_Date.Text = dtCheckStatus.Rows[0]["DateInput_Incharge"].ToString();
                lblApproved_SectionTo_Date.Text = dtCheckStatus.Rows[0]["DateApproved_Incharge"].ToString();
                lblCheck_SectionTo_Date.Text = dtCheckStatus.Rows[0]["DateCheck_Incharge"].ToString();



                if (hdf_IssueDept.Value == "OK")
                {
                    img_IssueDept.ImageUrl = "~/images/Approved.png";
                }
                else if (hdf_IssueDept.Value == "NG")
                {
                    img_IssueDept.ImageUrl = "~/images/Reject.png";
                }

                else if (hdf_IssueDept.Value == "" || hdf_IssueDept.Value == null)
                {
                    img_IssueDept.ImageUrl = "";
                }

                if (hdf_ApprovedDept.Value == "OK")
                {
                    img_ApprovedDept.ImageUrl = "~/images/Approved.png";
                }
                else if (hdf_ApprovedDept.Value == "NG")
                {
                    img_ApprovedDept.ImageUrl = "~/images/Reject.png";
                }
                else if (hdf_ApprovedDept.Value == "" || hdf_ApprovedDept.Value == null)
                {
                    img_ApprovedDept.ImageUrl = "";
                }
                if (hdf_InputSectionTo.Value == "OK")
                {
                    img_InputSectionTo.ImageUrl = "~/images/Approved.png";
                }
                else if (hdf_InputSectionTo.Value == "NG")
                {
                    img_InputSectionTo.ImageUrl = "~/images/Reject.png";
                }
                else if (hdf_InputSectionTo.Value == "" || hdf_InputSectionTo.Value == null)
                {
                    img_InputSectionTo.ImageUrl = "";
                }
                //--

                if (hdf_CheckSectionTo.Value == "OK")
                {
                    img_CheckSectionTo.ImageUrl = "~/images/Approved.png";
                }
                else if (hdf_CheckSectionTo.Value == "NG")
                {
                    img_CheckSectionTo.ImageUrl = "~/images/Reject.png";
                }

                else if (hdf_CheckSectionTo.Value == "" || hdf_CheckSectionTo.Value == null)
                {
                    img_CheckSectionTo.ImageUrl = "";
                }

                if (hdf_ApprovedSectionTo.Value == "OK")
                {
                    img_ApprovedSectionTo.ImageUrl = "~/images/Approved.png";
                }
                else if (hdf_ApprovedSectionTo.Value == "NG")
                {
                    img_ApprovedSectionTo.ImageUrl = "~/images/Reject.png";
                }
                else if (hdf_ApprovedSectionTo.Value == "" || hdf_ApprovedSectionTo.Value == null)
                {
                    img_ApprovedSectionTo.ImageUrl = "";
                }



            }

        }
        public void LoadButton(string RequestNo)
        {

            string Issue_Dept = "", Approved_Dept = "", Input_Store = "", Check_Store = "", MGR_Store = "";
            bttReset.Visible = false;
            bttUpload.Visible = false;
            FileUpload1.Visible = false;
            bttTempFile.Visible = false;
            string RQStatus = "";
            // Get Stock in pro
            dtLoadStock = DataConn.StoreFillDS("[SP_MaterialTranfer_Search]", CommandType.StoredProcedure, RequestNo);
            if (dtLoadStock.Rows.Count > 0)
            {
                hdfStockIssue.Value = dtLoadStock.Rows[0]["IssueSloc"].ToString();
                hdfStockReice.Value = dtLoadStock.Rows[0]["RecvSloc"].ToString();
                RQStatus = dtLoadStock.Rows[0]["Status_RQ"].ToString();
            }

            // Get Role current of user

            dtUserCurrrent = DataConn.StoreFillDS("SP_Tranfer_CurrentUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept);
            string RoleDept = null;
            string Role = null;

            if (dtUserCurrrent.Rows.Count > 0)
            {
                RoleDept = dtUserCurrrent.Rows[0]["RoleDept"].ToString();
                Role = dtUserCurrrent.Rows[0]["RoleID"].ToString();
            }



            if (RequestNo == "")
            {



                if (int.Parse(Role) == 1 && RoleDept == "RQ")
                {
                    bttApproved.Text = "Sent Request";
                    bttReject.Visible = false;
                    bttApproved.Enabled = true;
                    bttTempFile.Visible = true;
                    bttReset.Visible = false;
                }

                if (int.Parse(Role) == 2 && RoleDept == "RQ")
                {
                    bttApproved.Text = "Approved by";
                    bttReject.Enabled = true;
                    bttApproved.Enabled = true;
                    bttReset.Visible = false;
                }

                if (int.Parse(Role) == 1 && RoleDept == "STORE")
                {
                    bttApproved.Text = "Checked by";
                    bttReject.Visible = false;
                    bttApproved.Enabled = true;
                    bttTempFile.Visible = true;
                    bttReset.Visible = false;
                }

                if (int.Parse(Role) == 2 && RoleDept == "STORE")
                {
                    bttApproved.Text = "Approved by";
                    bttReject.Visible = false;
                    bttApproved.Enabled = true;
                    bttTempFile.Visible = true;
                    bttReset.Visible = false;
                }
                if (int.Parse(Role) == 3 && RoleDept == "STORE")
                {
                    bttApproved.Text = "Input";
                    bttReject.Enabled = true;
                    bttApproved.Enabled = true;
                    bttReset.Visible = false;
                }

            }
            else
            {
                dtCheckStatus = DataConn.StoreFillDS("SP_Tranfer_CheckStatus", CommandType.StoredProcedure, RequestNo);
                if (dtCheckStatus.Rows.Count > 0)
                {
                    Issue_Dept = dtCheckStatus.Rows[0]["StatusInchage_Dept"].ToString();
                    Approved_Dept = dtCheckStatus.Rows[0]["StatusApproved_Dept"].ToString();
                    Input_Store = dtCheckStatus.Rows[0]["StatusInput_Incharge"].ToString();
                    Check_Store = dtCheckStatus.Rows[0]["StatusCheck_Incharge"].ToString();
                    MGR_Store = dtCheckStatus.Rows[0]["StatusApproved_Incharge"].ToString();

                }


                if ((int.Parse(Role) == 1) && (RoleDept == "RQ"))// Show status button permistion Requester
                {
                    if ((Approved_Dept == "NG") || (Input_Store == "NG") || (Check_Store == "NG") || (MGR_Store == "NG"))
                    {
                        bttReset.Visible = true;
                        bttUpload.Visible = true;
                        FileUpload1.Visible = true;
                        bttTempFile.Visible = true;
                    }
                    else
                    {
                        bttUpload.Visible = true;
                        FileUpload1.Visible = true;

                    }

                }

                if (RoleDept == "RQ")
                {
                    if (int.Parse(Role) == 1) // Show status button permistion Requester
                    {
                        bttUpload.Visible = true;
                        FileUpload1.Visible = true;
                        bttTempFile.Visible = true;
                        if (Issue_Dept == "OK")
                        {
                            bttApproved.Text = "Sent Request";
                            bttReject.Visible = false;
                            bttApproved.Enabled = false;

                        }

                        else if ((string.Equals(Issue_Dept, null) == true) || (string.Equals(Issue_Dept, "") == true))
                        {

                            bttApproved.Text = "Sent Request";
                            bttApproved.Enabled = true;
                            bttApproved.Visible = true;
                            bttReject.Visible = true;
                        }

                        if (Issue_Dept == "NG")
                        {
                            bttApproved.Text = "Sent Request";
                            bttReject.Visible = false;
                            bttApproved.Enabled = false;

                        }

                    }
                    if (int.Parse(Role) == 2) // Show status button permistion Approved
                    {


                        if (Approved_Dept == "OK")
                        {
                            bttApproved.Text = "Approved by";
                            bttApproved.Enabled = false;
                            bttReject.Enabled = false;



                        }
                        else if ((string.Equals(Approved_Dept, null) == true) || (string.Equals(Approved_Dept, "") == true))
                        {
                            bttApproved.Text = "Approved by";
                            bttReject.Visible = true;
                            bttReject.Enabled = true;
                            bttApproved.Enabled = true;
                            bttApproved.Visible = true;


                        }
                        if (Approved_Dept == "NG")
                        {
                            bttApproved.Text = "Approved by";
                            bttReject.Enabled = false;
                            bttApproved.Enabled = false;

                        }
                    }
                    if (int.Parse(Role) == 3) // Show status button permistion Approved
                    {
                        bttApproved.Text = "Canot acction";
                        bttReject.Enabled = false;
                        bttApproved.Enabled = false;
                        bttPrint.Enabled = true;

                    }

                }
                if (RoleDept == "STORE")
                {

                    if (int.Parse(Role) == 1) // Show status button permistion Requester
                    {

                        if (Check_Store == "OK")
                        {
                            bttApproved.Text = "Checked by";
                            bttReject.Enabled = false;
                            bttApproved.Enabled = false;

                        }



                        else if ((string.Equals(Check_Store, null) == true) || (string.Equals(Check_Store, "") == true))
                        {

                            bttApproved.Text = "Checked by";
                            bttApproved.Enabled = true;
                            bttApproved.Visible = true;
                            bttReject.Visible = true;
                            bttReject.Enabled = true;

                        }

                        else if (Check_Store == "NG")
                        {

                            bttApproved.Enabled = false;
                            bttReject.Enabled = false;
                            bttReset.Enabled = false;
                        }
                    }
                    if (int.Parse(Role) == 2) // Show status button permistion Checker
                    {
                        if (MGR_Store == "OK")
                        {
                            bttApproved.Text = "Approved by";
                            bttApproved.Enabled = false;
                            bttReject.Enabled = false;

                        }

                        else if (((string.Equals(MGR_Store, null) == true) || string.Equals(MGR_Store, "") == true))
                        {
                            bttApproved.Text = "Approved by";
                            bttReject.Visible = true;
                            bttReject.Enabled = true;
                            bttApproved.Enabled = true;
                            bttApproved.Visible = true;

                        }

                        else if (MGR_Store == "NG")
                        {
                            bttApproved.Text = "Approved by";
                            bttReject.Enabled = false;
                            bttApproved.Enabled = false;

                        }

                    }
                    if (int.Parse(Role) == 3) // Show status button permistion Approved
                    {
                        if (Input_Store == "OK")
                        {
                            bttApproved.Text = "Input";
                            bttApproved.Enabled = false;
                            bttReject.Enabled = false;

                        }
                        else if (((string.Equals(Input_Store, null) == true) || (string.Equals(Input_Store, "") == true)))
                        {
                            bttApproved.Text = "Input";
                            bttReject.Visible = true;
                            bttReject.Enabled = true;
                            bttApproved.Enabled = true;
                            bttApproved.Visible = true;

                        }
                        if (Input_Store == "NG")
                        {
                            bttApproved.Text = "Input";
                            bttReject.Enabled = false;
                            bttApproved.Enabled = false;
                        }
                    }

                }




            }

        }
        protected void bttUpload_Click(object sender, EventArgs e)
        {
            Upload();

        }
        public void Upload()
        {
            //try
            //{
            Public_Dept =Session["CostCenter"].ToString().Trim();
            System.Data.OleDb.OleDbConnection MyConnection;
            System.Data.DataSet DtSet;
            System.Data.OleDb.OleDbDataAdapter MyCommand;
            DataTable dt = new DataTable();
            dt = null;
            if (FileUpload1.HasFile)
            {
                string strFileType = Path.GetExtension(FileUpload1.FileName).ToLower();
                string path = FileUpload1.PostedFile.FileName;
                string link_path = Server.MapPath("~/Upload_Request/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileUpload1.FileName);
                if (FileUpload1.FileName == "UploadTranfer.xlsx" || FileUpload1.FileName == "UploadTranfer.xls")
                {
                    FileUpload1.SaveAs(link_path);
                    //Connection String to Excel Workbook            
                    if (strFileType.Trim() == ".xls")
                    {
                        //connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + link_path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"");
                        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]  where Material is not null", MyConnection);
                        MyCommand.TableMappings.Add("Table", "TestTable");
                        DtSet = new System.Data.DataSet();
                        MyCommand.Fill(DtSet);
                        dt = DtSet.Tables[0];
                        MyConnection.Close();
                    }
                    else if (strFileType.Trim() == ".xlsx")
                    {
                        //connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + link_path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"");
                        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$] where Material is not null", MyConnection);
                        MyCommand.TableMappings.Add("Table", "TestTable");
                        DtSet = new System.Data.DataSet();
                        MyCommand.Fill(DtSet);
                        dt = DtSet.Tables[0];
                        MyConnection.Close();
                    }

                    string sql_ = ""; string SQL_2 = "";
                    if (dt.Rows.Count > 0)
                    {
                        DataTable DtIssuse = new DataTable();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //RequestNo,PartNo,MOQ,LeadTime,CustomerCode, Deadline,Remask,Peson_Incharge,Date_Incharge,Insert_user,Update_Date
                            string SlipNo = null; string TranCode = null; string MVType = null; string Material = null; float Qty = 0; string IssueSloc = null; string ReccvSloc = null; string Plant = null;
                            string Reason = null; string Remask = null; string DateInsert = null;string RQReset = null;


                            if (dt.Rows[i][0].ToString() != "")
                            {
                                SlipNo = dt.Rows[i][0].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data SlipNo at row :(" + (i + 1).ToString() + ") is null');", true);

                                return;
                            }

                            if (dt.Rows[i][1].ToString() != "")
                            {
                                TranCode = dt.Rows[i][1].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data TranCode at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }
                            if (dt.Rows[i][2].ToString() != "")
                            {
                                MVType = dt.Rows[i][2].ToString();
                                DataTable dtCheckMV = DataConn.DataTable_Sql("select count(isnull(MVTypeCode,0)) as CheckMV from tbl_MV_Master  where   MVTypeCode =  '" + MVType + "'");
                                if (dtCheckMV.Rows.Count == 0)
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data MVType at row :(" + (i + 1).ToString() + ") Incorrect.');", true);
                                    return;
                                }

                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data MVType at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }
                            if (dt.Rows[i][3].ToString() != "")
                            {
                                Material = dt.Rows[i][3].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Material at row :(" + (i + 1).ToString() + ") is null');", true);

                                return;
                            }

                            if (dt.Rows[i][4].ToString() != "")
                            {
                                Qty = float.Parse(dt.Rows[i][4].ToString());
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Qty at row :(" + (i + 1).ToString() + ") is null');", true);

                                return;
                            }



                            if (dt.Rows[i][5].ToString() != "")
                            {
                                IssueSloc = dt.Rows[i][5].ToString();
                                DataTable DtCheckIssueStock = DataConn.StoreFillDS("SP_MaterialTranfer_CheckStock", CommandType.StoredProcedure, IssueSloc, Session["UserName"].ToString(),Session["CostCenter"]);
                                if (int.Parse(DtCheckIssueStock.Rows[0]["StockCount"].ToString()) == 0)
                                {

                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Upload NG...StockIssue not exits at row  :(" + (i + 1).ToString() + ")');", true);
                                    return;

                                }
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data IssueSloc at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }

                            if (dt.Rows[i][6].ToString() != "")
                            {
                                ReccvSloc = dt.Rows[i][6].ToString();
                                DataTable DtCheckReceiveStock = DataConn.StoreFillDS("SP_MaterialTranfer_CheckStock", CommandType.StoredProcedure, ReccvSloc, "NULL");
                                if (int.Parse(DtCheckReceiveStock.Rows[0]["StockCount"].ToString()) == 0)
                                {

                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Upload NG...ReciveStock not exits at row  :(" + (i + 1).ToString() + ")');", true);
                                    return;

                                }
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Receive Sloc at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }

                            if (dt.Rows[i][7].ToString() != "")
                            {
                                Plant = dt.Rows[i][7].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Plant at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }

                            if (dt.Rows[i][8].ToString() != "")
                            {
                                Reason = dt.Rows[i][8].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Reason at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }

                            if (dt.Rows[i][9].ToString() != "")
                            {
                                Remask = dt.Rows[i][9].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Remask at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }

                            RQReset = dt.Rows[i][10].ToString();
                            DateInsert = DateTime.Now.ToString("dd/MM/yyyy");

                            int Max_ID = 0;
                            DataTable dtMax = DataConn.DataTable_Sql("select IsNull( max(cast(right(RequestNo, CHARINDEX('-', REVERSE(RequestNo)) - 1) as int)),0)  from[dbo].[tbl_RQ_Tranfer]  ");

                            if (dtMax.Rows.Count <= 0)
                            {
                                Max_ID = 1;
                            }
                            else
                            {
                                Max_ID = int.Parse(dtMax.Rows[0][0].ToString());
                                Max_ID = Max_ID + 1;
                            }


                            string Request_NO = "RQT-" + Public_Dept + "-" + DateTime.Now.ToString("MMyy") + "-" + Max_ID.ToString();




                            if (IssueSloc.Trim() == ReccvSloc.Trim())
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('The IssueSloc not sample  RecvSloc at row  :(" + (i + 1).ToString() + ")');", true);
                                return;
                            }
                            DataTable dtMVType = DataConn.DataTable_Sql("select MVTypeCode from [dbo].[tbl_MV_Master] where MVTypeCode =  '" + MVType + "'");
                            if(dtMVType.Rows.Count <= 0)
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('MV Type at row  :(" + (i + 1).ToString() + ") not correct.');", true);
                                return;
                            }
                            else
                            {
                                if (TranCode != ""  && RQReset != "")
                                {
                                    DataTable dtRQMV  = DataConn.DataTable_Sql("select * from [dbo].[tbl_RQ_Tranfer] where RequestNo = '" + RQReset + "'");
                                    if (dtRQMV.Rows.Count > 0)
                                    {
                                        SQL_2 = SQL_2 + "UPDATE   tbl_RQ_Tranfer  SET ";
                                        SQL_2 = SQL_2 + " [IssueQty] = '" + int.Parse(Qty.ToString()) + "' ,SlipNo =  '" + SlipNo + "' ,MvType =  " + MVType + " ,TranCode = '" + TranCode + "', IssueSloc='" + IssueSloc + "' , RecvSloc = '" + ReccvSloc + "' ,";
                                        SQL_2 = SQL_2 + " Reason =  '" + Reason + "' ,Note = N'" + Remask + "',IssueDate ='" + DateInsert + "',UserCreate = '" + Session["UserName"].ToString() + "' ,CodeCenter = '" +Session["CostCenter"].ToString() + "' ";
                                        SQL_2 = SQL_2 + "WHERE [RequestNo] = '" + RQReset + "' and  [Material] =  '" + Material + "'  ";
                                        Session["RQ_Upload"] = RQReset;
                                    }
                                    else
                                    {
                                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Upload NG.Because request at row  :(" + (i + 1).ToString() + ") not exits.');", true);
                                        return;

                                    }

                                }
                                else
                            
                                {
                                    sql_ = sql_ + " INSERT INTO tbl_RQ_Tranfer ([RequestNo],[Material],[IssueQty], SlipNo,MvType,TranCode,IssueSloc,RecvSloc,Reason , Note,IssueDate,UserCreate,CodeCenter) ";
                                    sql_ = sql_ + " VALUES( '" + Request_NO + "','" + Material + "','" + int.Parse(Qty.ToString()) + "','" + SlipNo + "'," + MVType + " ,'" + TranCode + "','" + IssueSloc + "' ,'" + ReccvSloc + "', N'" + Reason + "','" + Remask + "','" + DateInsert + "','" + Session["UserName"].ToString() + "','" +Session["CostCenter"].ToString() + "') ";
                                    Session["RQ_Upload"] = Request_NO;
                                }

                            }

                        }

                    }
                    
                    if(sql_ != "")
                        {
                        DataConn.Execute_NonSQL(sql_);
                        Search(Session["RQ_Upload"].ToString());
                        Load_TreeView_Search(Session["RQ_Upload"].ToString());
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Upload successfull....');", true);
                    }
                    else if (SQL_2 !="")
                     {
                        DataConn.Execute_NonSQL(SQL_2);
                        Search(Session["RQ_Upload"].ToString());
                        Load_TreeView_Search(Session["RQ_Upload"].ToString());
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Upload successfull....');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Upload NG.');", true);
                    }
                    // Set StatusUpload ==> Reload tree view 
                    hdfStatus_Upload.Value = "OK";

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning(Please you change the name of the excel file with the name: UploadTranfer');", true);

                }
                // Delete file upload
                if (System.IO.File.Exists(link_path))
                {
                    System.IO.File.Delete(link_path);
                }
            }
            else
            {

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning(Please check data excel file !!!');", true);
            }

        }
        public void Load_Treeview_Management()
        {
            Public_Dept =Session["CostCenter"].ToString().Trim();
            treeNOT_Approved.Nodes.Clear();// Delete Node before ==> Load node again
            TreeNode treeNodes = new TreeNode
            {
                Text = "Management Request"
            };
            treeNOT_Approved.Nodes.Add(treeNodes);
            DataTable table_Tree1 = new DataTable();
            table_Tree1 = DataConn.StoreFillDS("SP_MaterialTranfer_BindRQ_NOTApproved", CommandType.StoredProcedure, Public_Dept, Session["StockFrom"].ToString(), Session["StockTo"].ToString());
            if (table_Tree1.Rows.Count > 0)
            {
                TreeNode treeChild1 = new TreeNode
                {
                    Text = "List RQ Need Approved"
                };
                treeNodes.ChildNodes.Add(treeChild1);
                for (int i = 0; i < table_Tree1.Rows.Count; i++)
                {
                    TreeNode treeChild3 = new TreeNode
                    {
                        Text = table_Tree1.Rows[i]["RequestNo"].ToString()
                    };
                    treeChild1.ChildNodes.Add(treeChild3);

                    Search(table_Tree1.Rows[i]["RequestNo"].ToString());

                }
            }
            DataTable dt_RequestApproved = new DataTable();
            dt_RequestApproved = DataConn.StoreFillDS("SP_MaterialTranfer_BindRQ_Approved", CommandType.StoredProcedure, Public_Dept, Session["StockFrom"].ToString(), Session["StockTo"].ToString());
            if (dt_RequestApproved.Rows.Count > 0)
            {
                TreeNode treeChild2 = new TreeNode
                {
                    Text = "List RQ Already Approved "
                };
                treeNodes.ChildNodes.Add(treeChild2);
                for (int i = 0; i < dt_RequestApproved.Rows.Count; i++)
                {
                    TreeNode treeChild4 = new TreeNode
                    {
                        Text = dt_RequestApproved.Rows[i]["RequestNo"].ToString()
                    };
                    treeChild2.ChildNodes.Add(treeChild4);
                    DataTable dt_LoadApprroved = new DataTable();

                    Search(dt_RequestApproved.Rows[i]["RequestNo"].ToString());
                }
            }
        } //Load tree view list Request\
        public void Check_StatusFuntion_1Request(string RequestID, string User_next)
        {
            Public_Dept =Session["CostCenter"].ToString().Trim();

            if (REQUESTID != "" && User_next != "")
            {

                Loadstatus(Session["RequestID_1RQ"].ToString());
                LoadButton(Session["RequestID_1RQ"].ToString());
            }

        }
        public void Load_TreeView_Search(string RQ)
        {
            Public_Dept =Session["CostCenter"].ToString().Trim();
            treeNOT_Approved.Nodes.Clear();// Delete Node before ==> Load node again
            TreeNode treeNodes = new TreeNode
            {
                Text = "Management In-Out Request"
            };
            // string RQ_1 = Session["RequestID_1RQ"].ToString();

            if (RQ.ToString() == null)
            {

                DataTable dt = DataConn.StoreFillDS("SP_MaterialTranfer_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["StockFrom"].ToString(), Session["StockTo"].ToString());
                if (dt.Rows.Count > 0)
                {
                    RQ = dt.Rows[0]["RequestNo"].ToString();
                }


            }

            treeNOT_Approved.Nodes.Add(treeNodes);

            TreeNode treeChild1 = new TreeNode
            {
                Text = "List RQ Need Approved"
            };

            treeNodes.ChildNodes.Add(treeChild1);
            if (RQ.ToString() != "")
            {
                TreeNode treeChild3 = new TreeNode
                {
                    Text = RQ.ToString()
                };
                treeChild1.ChildNodes.Add(treeChild3);
                DataTable table_Tree2 = new DataTable();

                Search(RQ);

            }
        }
        public void LoadData()

        {
            Public_Dept =Session["CostCenter"].ToString().Trim();
            string RoleDept = null;
            string Role = null;

            if (treeNOT_Approved.SelectedNode == null)
            {

                if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                {
                    DataTable dt = DataConn.StoreFillDS("SP_MaterialTranfer_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["StockFrom"].ToString(), Session["StockTo"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        RQ = dt.Rows[0]["RequestNo"].ToString();
                    }

                }
                else
                {
                    RQ = Session["RequestID_1RQ"].ToString();
                }

            }
            else
            {
                RQ = treeNOT_Approved.SelectedNode.Value.ToString();
            }

            dtTranfer = DataConn.FillStore("SP_MaterialTranfer_Search", CommandType.StoredProcedure, RQ);
            dt_Comment = DataConn.FillStore("SP_MaterialTranfer_Comment_Load", CommandType.StoredProcedure, RQ);

            if (dtTranfer.Rows.Count > 0)
            {
                hdfStockIssue.Value = dtTranfer.Rows[0]["IssueSloc"].ToString();
                hdfStockReice.Value = dtTranfer.Rows[0]["RecvSloc"].ToString();
            }

            // Get Role current of user

            dtUserCurrrent = DataConn.StoreFillDS("SP_Tranfer_CurrentUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept);

            if (dtUserCurrrent.Rows.Count > 0)
            {
                RoleDept = dtUserCurrrent.Rows[0]["RoleDept"].ToString();
                Role = dtUserCurrrent.Rows[0]["RoleID"].ToString();
            }


            if (dtTranfer.Rows.Count > 0 && dtUserCurrrent.Rows.Count > 0)
            {

                lblIssueDate.Text = dtTranfer.Rows[0]["IssueDate"].ToString();
                hdfStockIssue.Value = dtTranfer.Rows[0]["IssueSloc"].ToString();
                hdfStockReice.Value = dtTranfer.Rows[0]["RecvSloc"].ToString();
                hdfRequest.Value = dtTranfer.Rows[0]["RequestNo"].ToString();
                hdfRoleDeptUpdate.Value = Role.ToString();
                hdfRoleupdate.Value = RoleDept.ToString();
                hdfUserUpdate.Value = Session["UserName"].ToString();
                lblSlipNo.Text = dtTranfer.Rows[0]["SlipNo"].ToString();
                lblTranCode.Text = dtTranfer.Rows[0]["TranCode"].ToString();
                lblMoveType.Text = dtTranfer.Rows[0]["MvType"].ToString();

                Loadstatus(RQ);
                LoadButton(RQ);
            }

            else
            {
                if (RoleDept == "RQ")
                {
                    if (int.Parse(Role) == 1) // Show status button permistion Requester
                    {
                        bttUpload.Visible = true;
                        FileUpload1.Visible = true;
                        bttReset.Visible = true;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttApproved.Text = "Send RQ";
                        bttPrint.Visible = true;
                    }
                    if (int.Parse(Role) == 2)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = false;
                        bttReject.Visible = false;
                        bttPrint.Visible = false;
                        bttApproved.Text = "Check by";
                    }
                    if (int.Parse(Role) == 3) // Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = false;
                        bttReject.Visible = false;
                        bttApproved.Text = "Approved by";
                        bttPrint.Visible = false;
                    }
                }
                if (RoleDept == "STORE")
                {
                    if (int.Parse(Role) == 1)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = false;
                        bttReject.Visible = false;
                        bttApproved.Text = "Check by";
                        bttPrint.Visible = false;
                    }
                    if (int.Parse(Role) == 2)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = false;
                        bttReject.Visible = false;
                        bttApproved.Text = "Approved by";
                        bttPrint.Visible = false;
                    }
                    if (int.Parse(Role) == 3)// Show status button permistion Requester

                    {
                        bttApproved.Text = "Input";
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = false;
                        bttReject.Visible = false;
                        bttPrint.Visible = false;
                    }
                }

            }

        }
        protected void treeNOT_Approved_SelectedNodeChanged(object sender, EventArgs e)
        {
            Search(treeNOT_Approved.SelectedNode.Value.ToString());
        }
        protected void treeApproved_SelectedNodeChanged(object sender, EventArgs e)
        {
            Search(treeNOT_Approved.SelectedNode.Value.ToString());
        }
        public void SendEmail_Next(string RequestID, string DeptID, string user_Current, string subject, string Content_Comment, string StoreIssue, string StoreReceive)
        {
            Public_Dept =Session["CostCenter"].ToString().Trim();
            //1 Get Role_Next , User_Next, RoleDept_Next
            string UserNext = null; string RoleNext = null; string EmailNext = null;
            dtNextMail = DataConn.StoreFillDS("SP_Tranfer_NextUser", CommandType.StoredProcedure, Session["UserName"].ToString(), StoreIssue, StoreReceive, DeptID);
            if (dtNextMail.Rows.Count > 0)
            {
                for (int j = 0; j < dtNextMail.Rows.Count; j++)
                {

                    UserNext = dtNextMail.Rows[j]["UserLogin"].ToString();
                    RoleNext = dtNextMail.Rows[j]["RoleID"].ToString();
                    EmailNext = dtNextMail.Rows[j]["Email"].ToString();


                    System.Net.Mail.SmtpClient objClient = new System.Net.Mail.SmtpClient("157.8.1.131");
                    subject = subject.Replace('\r', ' ').Replace('\n', ' ');
                    System.Net.Mail.MailAddress mail = new System.Net.Mail.MailAddress("tax.psnv@vn.panasonic.com", "Tranfer Material");
                    System.Net.Mail.MailMessage objMessage = new System.Net.Mail.MailMessage
                    {
                        Priority = MailPriority.High,
                        IsBodyHtml = true,

                        From = mail,
                        Subject = subject
                    };
                    objMessage.IsBodyHtml = true;
                    //Get data Header Images to Content of Email
                    string Header_Email = "";
                    Header_Email += "";
                    using (StreamReader reader2 = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Fomat_SendEmail/TranferStock.html")))
                    {
                        header_ = reader2.ReadToEnd();
                    }
                    header_ = header_.Replace("RQID", DataConn.Encrypt(RequestID.ToString()));
                    header_ = header_.Replace("USID", DataConn.Encrypt(UserNext.ToString()));
                    header_ = header_.Replace("RoleUser", DataConn.Encrypt(RoleNext.ToString()));
                    header_ = header_.Replace("CostCenter", DataConn.Encrypt(Public_Dept));
                    header_ = header_.Replace("Stock_From", DataConn.Encrypt(hdfStockIssue.Value.ToString()));
                    header_ = header_.Replace("Stock_To", DataConn.Encrypt(hdfStockReice.Value.ToString()));
                    objMessage.Body = header_;
                    //string[] ToEmailList = ToEmail.Split(',');
                    //foreach (string Email_To in ToEmailList)
                    //{
                    objMessage.To.Add(new MailAddress(EmailNext));
                    objClient.Send(objMessage);
                    // }
                }
            }
        }
        public void SendEmail_Prv(string RequestID, string DeptID, string user_Current, string subject, string PreEmail, string Content_Comment, string StoreIssue, string StoreReceive)
        {
            Public_Dept =Session["CostCenter"].ToString().Trim();
            string UserNext = "", RoleNext = "";
            dtPreEmail = DataConn.StoreFillDS("SP_Tranfer_PreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept, RequestID);
            if (dtNextMail.Rows.Count > 0)
            {
                UserNext = dtNextMail.Rows[0]["UserLogin"].ToString();
                RoleNext = dtNextMail.Rows[0]["RoleID"].ToString();
            }
            System.Net.Mail.SmtpClient objClient = new System.Net.Mail.SmtpClient("157.8.1.131");
            subject = subject.Replace('\r', ' ').Replace('\n', ' ');
            System.Net.Mail.MailAddress mail = new System.Net.Mail.MailAddress("tax.psnv@vn.panasonic.com", "Tranfer Material");
            System.Net.Mail.MailMessage objMessage = new System.Net.Mail.MailMessage
            {
                Priority = MailPriority.High,
                IsBodyHtml = true,

                From = mail,
                Subject = subject
            };
            objMessage.IsBodyHtml = true;
            //Get data Header Images to Content of Email
            string Header_Email = "";
            Header_Email += "";
            using (StreamReader reader2 = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Fomat_SendEmail/TranferStock.html")))
            {
                header_ = reader2.ReadToEnd();
            }
            header_ = header_.Replace("RQID", DataConn.Encrypt(RequestID.ToString()));
            header_ = header_.Replace("USID", DataConn.Encrypt(UserNext.ToString()));
            header_ = header_.Replace("RoleUser", DataConn.Encrypt(RoleNext.ToString()));
            header_ = header_.Replace("CostCenter", DataConn.Encrypt(Public_Dept));
            header_ = header_.Replace("Stock_From", DataConn.Encrypt(hdfStockIssue.Value.ToString()));
            header_ = header_.Replace("Stock_To", DataConn.Encrypt(hdfStockReice.Value.ToString()));
            objMessage.Body = header_;
            string[] ToEmailList = PreEmail.Split(',');
            foreach (string Email_To in ToEmailList)
            {
                objMessage.To.Add(new MailAddress(Email_To));
                objClient.Send(objMessage);
            }

        }

        protected void bttApproved_Click(object sender, EventArgs e)
        {
            Public_Dept =Session["CostCenter"].ToString().Trim();
            string Request_NO = "";
            if (treeNOT_Approved.SelectedNode == null)
            {
                if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                {
                    dtTreeRQ = DataConn.StoreFillDS("SP_MaterialTranfer_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["StockFrom"].ToString(), Session["StockTo"].ToString());
                    if (dtTreeRQ.Rows.Count > 0)
                    {
                        Request_NO = dtTreeRQ.Rows[0]["RequestNo"].ToString();
                    }

                }
                else
                {
                    Request_NO = Session["RequestID_1RQ"].ToString();
                }

            }
            else
            {
                Request_NO = treeNOT_Approved.SelectedNode.Value.ToString();
            }
            // Load Stock in
            dtLoadStock = DataConn.StoreFillDS("[SP_MaterialTranfer_Search]", CommandType.StoredProcedure, Request_NO);
            if (dtLoadStock.Rows.Count > 0)
            {
                hdfStockIssue.Value = dtLoadStock.Rows[0]["IssueSloc"].ToString();
                hdfStockReice.Value = dtLoadStock.Rows[0]["RecvSloc"].ToString();
            }
            dtPreEmail = DataConn.StoreFillDS("SP_Tranfer_PreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept, Request_NO);
            if (dtPreEmail.Rows.Count > 0)
            {
                for (int i = 0; i < dtPreEmail.Rows.Count; i++)
                {
                    if (Email_Pres == null || Email_Pres == "")
                    {
                        Email_Pres = dtPreEmail.Rows[i]["Email"].ToString();

                    }
                    else
                    {
                        Email_Pres = Email_Pres + ',' + dtPreEmail.Rows[i]["Email"].ToString();

                    }

                }

            }
            //dtNextMail = DataConn.StoreFillDS("SP_Tranfer_NextUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept);
            //if (dtNextMail.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtNextMail.Rows.Count; i++)
            //    {
            //        if (EmailNext == null || EmailNext == "")
            //        {
            //            EmailNext = dtNextMail.Rows[i]["Email"].ToString();

            //        }
            //        else
            //        {
            //            EmailNext = EmailNext + ',' + dtNextMail.Rows[i]["Email"].ToString();

            //        }

            //    }
            //}
            // Get Role current of user
            dtUserCurrrent = DataConn.StoreFillDS("SP_Tranfer_CurrentUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept);
            string RoleDept = null;
            string Role = null;
            if (dtUserCurrrent.Rows.Count > 0)
            {
                RoleDept = dtUserCurrrent.Rows[0]["RoleDept"].ToString();
                Role = dtUserCurrrent.Rows[0]["RoleID"].ToString();
            }
            DataTable dtStatus = DataConn.StoreFillDS("SP_Tranfer_CheckStatus", CommandType.StoredProcedure, Request_NO);
            if (dtStatus.Rows.Count < 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please check information of RequestNo');", true);

                return;

            }
            else
            {
                string Issue_Dept = dtStatus.Rows[0]["StatusInchage_Dept"].ToString();
                string Approved_Dept = dtStatus.Rows[0]["StatusApproved_Dept"].ToString();
                string Input_Store = dtStatus.Rows[0]["StatusInput_Incharge"].ToString();
                string Check_Store = dtStatus.Rows[0]["StatusCheck_Incharge"].ToString();
                string MGR_Store = dtStatus.Rows[0]["StatusApproved_Incharge"].ToString();
                if (Approved_Dept == "NG" || Input_Store == "NG" || Check_Store == "NG" || MGR_Store == "NG")
                {

                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                    Search(Request_NO);
                    return;
                }
                else
                {
                    if (RoleDept == "RQ")
                    {

                        if ((int.Parse(Role) == 1 && Issue_Dept == "") || (int.Parse(Role) == 1 && Issue_Dept == null))
                        {
                            int row = DataConn.ExecuteStore("SP_Tranfer_Approved", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept, Request_NO);
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = "[Tranfer] - Request " + Request_NO + " has been submitted successfully";
                                string Comment = txt_Comment.Value;
                                SendEmail_Next(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Comment, hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString());
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has submited sucessfully');", true);

                            }
                        }

                        if ((int.Parse(Role) == 2 && string.Equals(Issue_Dept, "") == true) || (int.Parse(Role) == 3 && string.Equals(Issue_Dept, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Approve(OK)');", true);
                            Search(Request_NO);
                            return;
                        }
                        else
                        {
                            if ((int.Parse(Role) == 2 && Issue_Dept == "OK" && Approved_Dept == "") || (int.Parse(Role) == 3 && Issue_Dept == "OK" && Approved_Dept == null))
                            {

                                int row = DataConn.ExecuteStore("SP_Tranfer_Approved", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept, Request_NO);
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                    string Subject = "[Tranfer] - Request approval for" + Request_NO + ".";
                                    string Comment = txt_Comment.Value;
                                    SendEmail_Next(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Comment, hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has approved sucessfully');", true);

                                }

                            }

                        }


                    }
                    if (RoleDept == "STORE")
                    {


                        if ((int.Parse(Role) == 1 && string.Equals(Approved_Dept, "") == true) || (int.Parse(Role) == 1 && string.Equals(Approved_Dept, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Approve(OK)');", true);
                            Search(Request_NO);
                            return;
                        }
                        else
                        {
                            if ((int.Parse(Role) == 1 && Approved_Dept == "OK" && Check_Store == "") || (int.Parse(Role) == 1 && Approved_Dept == "OK" && Check_Store == null))
                            {

                                int row = DataConn.ExecuteStore("SP_Tranfer_Approved", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept, Request_NO);
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                    string Subject = "[Tranfer] - Request approval for" + Request_NO + ".";
                                    string Comment = txt_Comment.Value;
                                    SendEmail_Next(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Comment, hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has approved sucessfully');", true);

                                }

                            }

                        }

                        if ((int.Parse(Role) == 2 && string.Equals(Check_Store, "") == true) || (int.Parse(Role) == 2 && string.Equals(Check_Store, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) ->Check(OK) -> Approve(OK)');", true);
                            Search(Request_NO);
                            return;
                        }
                        else
                        {
                            if ((int.Parse(Role) == 2 && Check_Store == "OK" && MGR_Store == "") || (int.Parse(Role) == 2 && Check_Store == "OK" && MGR_Store == null))
                            {

                                int row = DataConn.ExecuteStore("SP_Tranfer_Approved", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept, Request_NO);
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                    string Subject = "[Tranfer] - Request approval for" + Request_NO + ".";
                                    string Comment = txt_Comment.Value;
                                    SendEmail_Next(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Comment, hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has approved sucessfully');", true);

                                }

                            }

                        }

                        if ((int.Parse(Role) == 3 && string.Equals(MGR_Store, "") == true) || (int.Parse(Role) == 3 && string.Equals(MGR_Store, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) ->Check(OK) -> Approve(OK)');", true);
                            Search(Request_NO);
                            return;
                        }
                        else
                        {
                            if ((int.Parse(Role) == 3 && MGR_Store == "OK" && Input_Store == "") || (int.Parse(Role) == 3 && MGR_Store == "OK" && Input_Store == null))
                            {

                                int row = DataConn.ExecuteStore("SP_Tranfer_Approved", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept, Request_NO);
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                    string Subject = "[Tranfer] - Request approval for" + Request_NO + ".";
                                    string Comment = txt_Comment.Value;
                                    SendEmail_Next(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Comment, hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has approved sucessfully');", true);

                                }

                            }

                        }

                    }
                }

            }
            Search(Request_NO);
        }

        protected void bttReject_Click(object sender, EventArgs e)
        {
            Public_Dept =Session["CostCenter"].ToString().Trim();
            string Request_NO = "";
            if (treeNOT_Approved.SelectedNode == null)
            {
                if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                {
                    dtTreeRQ = DataConn.StoreFillDS("SP_MaterialTranfer_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["StockFrom"].ToString(), Session["StockTo"].ToString());
                    if (dtTreeRQ.Rows.Count > 0)
                    {
                        Request_NO = dtTreeRQ.Rows[0]["RequestNo"].ToString();
                    }

                }
                else
                {
                    Request_NO = Session["RequestID_1RQ"].ToString();
                }
                //(Session["UserName"].ToString(), Request_NO);
            }
            else
            {
                Request_NO = treeNOT_Approved.SelectedNode.Value.ToString();
            }
            // Load Stock in
            dtLoadStock = DataConn.StoreFillDS("[SP_MaterialTranfer_Search]", CommandType.StoredProcedure, Request_NO);
            if (dtLoadStock.Rows.Count > 0)
            {
                hdfStockIssue.Value = dtLoadStock.Rows[0]["IssueSloc"].ToString();
                hdfStockReice.Value = dtLoadStock.Rows[0]["RecvSloc"].ToString();
            }
            dtPreEmail = DataConn.StoreFillDS("SP_Tranfer_PreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept, Request_NO);
            if (dtPreEmail.Rows.Count > 0)
            {
                for (int i = 0; i < dtPreEmail.Rows.Count; i++)
                {
                    if (Email_Pres == null || Email_Pres == "")
                    {
                        Email_Pres = dtPreEmail.Rows[i]["Email"].ToString();

                    }
                    else
                    {
                        Email_Pres = Email_Pres + ',' + dtPreEmail.Rows[i]["Email"].ToString();

                    }

                }

            }
            dtNextMail = DataConn.StoreFillDS("SP_Tranfer_NextUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept);
            if (dtNextMail.Rows.Count > 0)
            {
                for (int i = 0; i < dtNextMail.Rows.Count; i++)
                {
                    if (EmailNext == null || EmailNext == "")
                    {
                        EmailNext = dtNextMail.Rows[i]["Email"].ToString();

                    }
                    else
                    {
                        EmailNext = EmailNext + ',' + dtNextMail.Rows[i]["Email"].ToString();

                    }

                }
            }
            // Get Role current of user
            dtUserCurrrent = DataConn.StoreFillDS("SP_Tranfer_CurrentUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept);
            string RoleDept = null;
            string Role = null;
            if (dtUserCurrrent.Rows.Count > 0)
            {
                RoleDept = dtUserCurrrent.Rows[0]["RoleDept"].ToString();
                Role = dtUserCurrrent.Rows[0]["RoleID"].ToString();
            }
            DataTable dtStatus = DataConn.StoreFillDS("SP_Tranfer_CheckStatus", CommandType.StoredProcedure, Request_NO);
            if (dtStatus.Rows.Count < 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please check information of RequestNo');", true);
                return;

            }
            else
            {
                string Issue_Dept = dtStatus.Rows[0]["StatusInchage_Dept"].ToString();
                string Approved_Dept = dtStatus.Rows[0]["StatusApproved_Dept"].ToString();
                string Input_Store = dtStatus.Rows[0]["StatusInput_Incharge"].ToString();
                string Check_Store = dtStatus.Rows[0]["StatusCheck_Incharge"].ToString();
                string MGR_Store = dtStatus.Rows[0]["StatusApproved_Incharge"].ToString();
                if (Approved_Dept == "NG" || Input_Store == "NG" || Check_Store == "NG" || MGR_Store == "NG")
                {

                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                    Search(Request_NO);
                    return;
                }
                else
                {
                    if (RoleDept == "RQ")
                    {



                        if ((int.Parse(Role) == 2 && string.Equals(Issue_Dept, "") == true) || (int.Parse(Role) == 2 && string.Equals(Issue_Dept, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Approve(OK)');", true);

                            Search(Request_NO);
                            return;
                        }
                        else
                        {
                            if ((int.Parse(Role) == 2 && Issue_Dept == "OK" && Approved_Dept == "") || (int.Parse(Role) == 2 && Issue_Dept == "OK" && Approved_Dept == null))
                            {

                                int row = DataConn.ExecuteStore("SP_Tranfer_Reject", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept, Request_NO);
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                    string Subject = "[Tranfer] " + Request_NO + " has rejected and submit again.";
                                    string Comment = txt_Comment.Value;
                                    SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment, hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);
                                }

                            }

                        }


                    }
                    if (RoleDept == "STORE")
                    {


                        if ((int.Parse(Role) == 1 && string.Equals(Approved_Dept, "") == true) || (int.Parse(Role) == 1 && string.Equals(Approved_Dept, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Approve(OK)');", true);
                            Search(Request_NO);
                            return;
                        }
                        else
                        {
                            if ((int.Parse(Role) == 1 && Approved_Dept == "OK" && Check_Store == "") || (int.Parse(Role) == 1 && Approved_Dept == "OK" && Check_Store == null))
                            {

                                int row = DataConn.ExecuteStore("SP_Tranfer_Reject", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept, Request_NO);
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                    string Subject = "[Tranfer] - Request approval for" + Request_NO + ".";
                                    string Comment = txt_Comment.Value;
                                    SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment, hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);

                                }

                            }

                        }

                        if ((int.Parse(Role) == 2 && string.Equals(Check_Store, "") == true) || (int.Parse(Role) == 2 && string.Equals(Check_Store, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) ->Check(OK) -> Approve(OK)');", true);
                            Search(Request_NO);
                            return;
                        }
                        else
                        {
                            if ((int.Parse(Role) == 2 && Check_Store == "OK" && MGR_Store == "") || (int.Parse(Role) == 2 && Check_Store == "OK" && MGR_Store == null))
                            {

                                int row = DataConn.ExecuteStore("SP_Tranfer_Reject", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept, Request_NO);
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                    string Subject = "[Tranfer] - Request approval for" + Request_NO + ".";
                                    string Comment = txt_Comment.Value;
                                    SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment, hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);

                                }

                            }

                        }

                        if ((int.Parse(Role) == 3 && string.Equals(MGR_Store, "") == true) || (int.Parse(Role) == 3 && string.Equals(MGR_Store, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) ->Check(OK) -> Approve(OK)');", true);
                            Search(Request_NO);
                            return;
                        }
                        else
                        {
                            if ((int.Parse(Role) == 3 && MGR_Store == "OK" && Input_Store == "") || (int.Parse(Role) == 3 && MGR_Store == "OK" && Input_Store == null))
                            {

                                int row = DataConn.ExecuteStore("SP_Tranfer_Reject", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept, Request_NO);
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                    string Subject = "[Tranfer] - Request approval for" + Request_NO + ".";
                                    string Comment = txt_Comment.Value;
                                    SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment, hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);

                                }

                            }

                        }

                    }
                }

            }
            Search(Request_NO);

        }

        protected void bttReset_Click(object sender, EventArgs e)
        {
            Public_Dept =Session["CostCenter"].ToString().Trim();
            string Request_NO = "";
            if (treeNOT_Approved.SelectedNode == null)
            {
                if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                {
                    dtTreeRQ = DataConn.StoreFillDS("SP_MaterialTranfer_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["StockFrom"].ToString(), Session["StockTo"].ToString());
                    if (dtTreeRQ.Rows.Count > 0)
                    {
                        Request_NO = dtTreeRQ.Rows[0]["RequestNo"].ToString();
                    }

                }
                else
                {
                    Request_NO = Session["RequestID_1RQ"].ToString();
                }
                //(Session["UserName"].ToString(), Request_NO);
            }
            else
            {
                Request_NO = treeNOT_Approved.SelectedNode.Value.ToString();
            }

            DataTable dtStatus = DataConn.StoreFillDS("SP_Tranfer_CheckStatus", CommandType.StoredProcedure, Request_NO);
            if (dtStatus.Rows.Count < 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please check information of RequestNo');", true);
                return;

            }
            else
            {
                string Issue_Dept = dtStatus.Rows[0]["StatusInchage_Dept"].ToString();
                string Approved_Dept = dtStatus.Rows[0]["StatusApproved_Dept"].ToString();
                string Input_Store = dtStatus.Rows[0]["StatusInput_Incharge"].ToString();
                string Check_Store = dtStatus.Rows[0]["StatusCheck_Incharge"].ToString();
                string MGR_Store = dtStatus.Rows[0]["StatusApproved_Incharge"].ToString();
                if (Approved_Dept == "NG" || Input_Store == "NG" || Check_Store == "NG" || MGR_Store == "NG")
                {
                    int row = DataConn.ExecuteStore("SP_Tranfer_Reset", CommandType.StoredProcedure, Request_NO);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been Reset sucessfully');", true);
                    Search(Request_NO);
                }
            }


        }

        protected void bttPrint_Click(object sender, EventArgs e)
        {
            Public_Dept =Session["CostCenter"].ToString().Trim();

            DataTable dt_ReportAll = new DataTable();
            DataTable dt_Status = new DataTable();


            if (treeNOT_Approved.SelectedNode == null)
            {
                if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                {
                    dtTreeRQ = DataConn.StoreFillDS("SP_MaterialTranfer_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["StockFrom"].ToString(), Session["StockTo"].ToString());
                    if (dtTreeRQ.Rows.Count > 0)
                    {
                        RequestNO = dtTreeRQ.Rows[0]["RequestNo"].ToString();
                    }

                }
                else
                {
                    RequestNO = Session["RequestID_1RQ"].ToString();
                }
                //(Session["UserName"].ToString(), Request_NO);
            }
            else
            {
                RequestNO = treeNOT_Approved.SelectedNode.Value.ToString();
            }
            string titleReport = "TRANSFER REQUEST";


            dt_ReportAll = DataConn.StoreFillDS("SP_MaterialTranfer_Report", CommandType.StoredProcedure, RequestNO);

            dt_Status = DataConn.StoreFillDS("SP_MaterialTranfer_ReportStatus", CommandType.StoredProcedure, RequestNO);


            PDF_Tranfer DataPDF = new PDF_Tranfer(dt_ReportAll, dt_Status, titleReport);

            // Create a MigraDoc document
            Document document = DataPDF.CreateDocument();
            document.UseCmykColor = true;
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always)
            {
                Document = document
            };
            pdfRenderer.RenderDocument();
            string filename = "TRANSFER REQUEST";
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

        protected void bttSend_Click(object sender, EventArgs e)
        {
            Public_Dept =Session["CostCenter"].ToString().Trim();
            string Request_NO = "";
            if (treeNOT_Approved.SelectedNode == null)
            {
                if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                {
                    dtTreeRQ = DataConn.StoreFillDS("SP_MaterialTranfer_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["StockFrom"].ToString(), Session["StockTo"].ToString());
                    if (dtTreeRQ.Rows.Count > 0)
                    {
                        Request_NO = dtTreeRQ.Rows[0]["RequestNo"].ToString();
                    }

                }
                else
                {
                    Request_NO = Session["RequestID_1RQ"].ToString();
                }
                //(Session["UserName"].ToString(), Request_NO);
            }
            else
            {
                Request_NO = treeNOT_Approved.SelectedNode.Value.ToString();
            }
            // Load Stock in
            dtLoadStock = DataConn.StoreFillDS("[SP_MaterialTranfer_Search]", CommandType.StoredProcedure, Request_NO);
            if (dtLoadStock.Rows.Count > 0)
            {
                hdfStockIssue.Value = dtLoadStock.Rows[0]["IssueSloc"].ToString();
                hdfStockReice.Value = dtLoadStock.Rows[0]["RecvSloc"].ToString();
            }
            dtPreEmail = DataConn.StoreFillDS("SP_Tranfer_PreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept, Request_NO);
            if (dtPreEmail.Rows.Count > 0)
            {
                for (int i = 0; i < dtPreEmail.Rows.Count; i++)
                {
                    if (Email_Pres == null || Email_Pres == "")
                    {
                        Email_Pres = dtPreEmail.Rows[i]["Email"].ToString();

                    }
                    else
                    {
                        Email_Pres = Email_Pres + ',' + dtPreEmail.Rows[i]["Email"].ToString();

                    }

                }

            }
            dtNextMail = DataConn.StoreFillDS("SP_Tranfer_NextUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept);
            if (dtNextMail.Rows.Count > 0)
            {
                for (int i = 0; i < dtNextMail.Rows.Count; i++)
                {
                    if (EmailNext == null || EmailNext == "")
                    {
                        EmailNext = dtNextMail.Rows[i]["Email"].ToString();

                    }
                    else
                    {
                        EmailNext = EmailNext + ',' + dtNextMail.Rows[i]["Email"].ToString();

                    }

                }
            }
            // Get Role current of user
            dtUserCurrrent = DataConn.StoreFillDS("SP_Tranfer_CurrentUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString(), Public_Dept);
            string RoleDept = null;
            string Role = null;
            if (dtUserCurrrent.Rows.Count > 0)
            {
                RoleDept = dtUserCurrrent.Rows[0]["RoleDept"].ToString();
                Role = dtUserCurrrent.Rows[0]["RoleID"].ToString();
            }

            string Comment = txt_Comment.Value;


            int row = DataConn.ExecuteStore("[SP_Tranfer_Comment_Update]", CommandType.StoredProcedure, Request_NO, Comment, Session["UserName"].ToString(), RoleDept, Role);
            if (row == 0)
            {

                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                string Subject = " Request for comment-" + Request_NO + ".";
                dt_Comment = DataConn.FillStore("[SP_MaterialTranfer_Comment_Load]", CommandType.StoredProcedure, Request_NO);
                if (RoleDept == "RQ")
                {

                    if (Role == "1")
                    {
                        SendEmail_Next(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Comment, hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString());
                    }
                }
                else
                {
                    SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment, hdfStockIssue.Value.ToString(), hdfStockReice.Value.ToString());
                }

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This comment has been sent sucessfully');", true);

            }
            txt_Comment.Value = "";

            Search(Request_NO);
        }
    }


}