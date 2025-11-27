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
using System.Web.Services;

namespace MATERIAL_IN_OUT
{
    public partial class ControlUser : System.Web.UI.Page
    {
        public DataTable dtUser = new DataTable();
        public DataTable dtUserTranfer = new DataTable();
        private string Stock = "";
        private string EmailNext = "";
        private string Email_Pres = "";
        public string RQ = "";
        public static string header_;
        public DataTable dtNextMail = new DataTable();
        public DataTable dtCheckStatus = new DataTable();
        public object __o;
        public string User_next = "";
        public string RoleDept = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                    if (Session["UserName"].ToString() != null)
                        {

                        this.mtvUserTranfer.ActiveViewIndex = -1;
                        mvtUserIssueMaterial.ActiveViewIndex = 0;
                        Loadata_MaterialIssue();

                        }
                
                //catch
                //{
                //    Response.Redirect("Login.aspx", false);
                //}

            }
        }

        public void Loadata_MaterialIssue()
        {

            BindRoleDept_Material();
            BindRoleAction_Material();

            BindRolePage_Issuer();
            BindDept_Material();

            dtUser = DataConn.FillStore("SP_UserControl_Material", CommandType.StoredProcedure);
            rptData.DataSource = dtUser;
            rptData.DataBind();
            lblTotal.Text = "Total User:" + dtUser.Rows.Count;
        }

        public void Loadata_Tranfer()
        {
            BindRoleDept_Tranfer();
            BindRoleAction_Tranfer();
            BindRolePage_Tranfer();
            BindDept_Tranfer();

            dtUser = DataConn.FillStore("SP_UserControl_Tranfer", CommandType.StoredProcedure);
            rptUser_Tranfer.DataSource = dtUser;
            rptUser_Tranfer.DataBind();
            lblTotal_Tranfer.Text = "Total User:" + dtUser.Rows.Count;
        }
        [WebMethod]
        protected void BindRoleDept_Material()
        {
            DataTable dt = new DataTable();
            dt = DataConn.DataTable_Sql("select ID,RoleName from [dbo].[tblRoleDept]");
            if (dt.Rows.Count > 0)
            {
                ddl_RoleDept.Items.Clear();
                ddl_RoleDept.DataTextField = "RoleName";
                ddl_RoleDept.DataValueField = "RoleName";
                ddl_RoleDept.DataSource = dt;
                ddl_RoleDept.DataBind();
            }
        }
        [WebMethod]
        protected void BindRoleDept_Tranfer()
        {
            DataTable dt = new DataTable();
            dt = DataConn.DataTable_Sql("select ID,RoleName from [dbo].[tblRoleDept]");
            if (dt.Rows.Count > 0)
            {

                ddlRoleDept_Tranfer.Items.Clear();
                ddlRoleDept_Tranfer.DataTextField = "RoleName";
                ddlRoleDept_Tranfer.DataValueField = "RoleName";
                ddlRoleDept_Tranfer.DataSource = dt;
                ddlRoleDept_Tranfer.DataBind();
            }
        }

        [WebMethod]
        protected void BindRoleAction_Material()
        {
            DataTable dt = new DataTable();
            dt = DataConn.DataTable_Sql("select RoleID,RoleName from  [dbo].[tbl_RoleMaster]");
            if (dt.Rows.Count > 0)
            {
                ddl_Role.Items.Clear();
                ddl_Role.DataTextField = "RoleName";
                ddl_Role.DataValueField = "RoleID";
                ddl_Role.DataSource = dt;
                ddl_Role.DataBind();
            }

        }
        [WebMethod]
        protected void BindRoleAction_Tranfer()
        {
            DataTable dt = new DataTable();
            dt = DataConn.DataTable_Sql("select RoleID,RoleName from  [dbo].[tbl_RoleMaster]");
            if (dt.Rows.Count > 0)
            {
                ddlRole_Tranfer.Items.Clear();
                ddlRole_Tranfer.DataTextField = "RoleName";
                ddlRole_Tranfer.DataValueField = "RoleID";
                ddlRole_Tranfer.DataSource = dt;
                ddlRole_Tranfer.DataBind();
            }

        }

        [WebMethod]
        protected void BindRolePage_Issuer()
        {
            DataTable dt = new DataTable();
            dt = DataConn.DataTable_Sql("select ID,RoleName from [dbo].[tblRolePage]");
            if (dt.Rows.Count > 0)
            {
                ddl_Rolepage.Items.Clear();
                ddl_Rolepage.DataTextField = "RoleName";
                ddl_Rolepage.DataValueField = "RoleName";
                ddl_Rolepage.DataSource = dt;
                ddl_Rolepage.DataBind();
            }
        }
        [WebMethod]
        protected void BindRolePage_Tranfer()
        {
            DataTable dt = new DataTable();
            dt = DataConn.DataTable_Sql("select ID,RoleName from [dbo].[tblRolePage]");
            if (dt.Rows.Count > 0)
            {
                ddlRolePage_Tranfer.Items.Clear();
                ddlRolePage_Tranfer.DataTextField = "RoleName";
                ddlRolePage_Tranfer.DataValueField = "RoleName";
                ddlRolePage_Tranfer.DataSource = dt;
                ddlRolePage_Tranfer.DataBind();
            }
        }

        [WebMethod]
        protected void BindDept_Material()
        {
            DataTable dt = new DataTable();
            dt = DataConn.DataTable_Sql("select DeptCode,DeptName from  [dbo].[tbl_DeptMaster]");
            if (dt.Rows.Count > 0)
            {
                ddl_Dept.Items.Clear();
                DataRow dr = dt.NewRow();
                dr[0] = "--Select Dept--";
                dt.Rows.InsertAt(dr, 0);
                ddl_Dept.DataTextField = "DeptName";
                ddl_Dept.DataValueField = "DeptCode";
                ddl_Dept.DataSource = dt;
                ddl_Dept.DataBind();
            }
        }
        [WebMethod]
        protected void BindDept_Tranfer()
        {
            DataTable dt = new DataTable();
            dt = DataConn.DataTable_Sql("select DeptCode,DeptName from  [dbo].[tbl_DeptMaster]");
            if (dt.Rows.Count > 0)
            {
                ddl_Dept.Items.Clear();
                DataRow dr = dt.NewRow();
                dr[0] = "--Select Dept--";
                dt.Rows.InsertAt(dr, 0);
                ddl_Dept.DataTextField = "DeptName";
                ddl_Dept.DataValueField = "DeptCode";
                ddl_Dept.DataSource = dt;
                ddl_Dept.DataBind();
            }

        }

        protected void btnadd_Click(object sender, EventArgs e)
        {

            string User = txtUser.Text.ToString();
            string FullName = txtFullName.Text.ToString();
            string Password = txtPassword.Text.ToString().Trim();
            string Repassword = txtRepassword.Text.ToString().Trim();
            string Role = ddl_Role.SelectedValue.ToString();
            string Email = txtEmail.Text.ToString();
            string RoleDept = ddl_RoleDept.SelectedValue.ToString();
            string RoleID = ddl_Role.SelectedValue.ToString();
            string DeptID = ddl_Dept.SelectedValue.ToString();
            string Stock = txtStock.Text.ToString();
            string RolePage = ddl_Rolepage.SelectedValue.ToString();
            bool CheckSave = true;
            if (User == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please input information User.');", true);
                CheckSave = false;
                return;
            }
            if (FullName == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please input information FullName.');", true);
                CheckSave = false;
                return;
            }
            if (Password == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please input information Password.');", true);
                CheckSave = false;
                return;
            }
            else
            {
                if ((string.Compare(Password, Repassword) != 0))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please input information Password not sample RePassword.');", true);
                    CheckSave = false;
                    return;
                }

            }
            if (ddl_RoleDept.SelectedValue.ToString() == "STORE")
            {
                if (txtStock.Text.ToString() == "")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please input information Stock.');", true);
                    CheckSave = false;
                    return;
                }
            }



            if (CheckSave == true)
            {
                // DataTable dtCheck = DataConn.StoreFillDS("SP_UserControl_Exits", CommandType.StoredProcedure, User);

                // if (int.Parse(dtCheck.Rows.Count) == 0 )
                //{

                //    int row = DataConn.ExecuteStore("SP_UserControl_Update", CommandType.StoredProcedure, User, FullName, Password, Role, Email, RoleDept, RoleID, DeptID, Stock, RolePage);
                //    if (row == 0)
                //    {
                //        //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Update information user sucessfully');", true);
                //        dtUser = DataConn.StoreFillDS("SP_UserControl_Material", System.Data.CommandType.StoredProcedure, txtUser.Text.ToString());
                //    }

                //}
                // else
                {
                    int row = DataConn.ExecuteStore("SP_UserControl_Insert", CommandType.StoredProcedure, User, FullName, Password, Role, Email, RoleDept, RoleID, DeptID, Stock, RolePage);
                    if (row == 0)
                    {
                        //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";

                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Insert information user sucessfully');", true);
                        //dtUser = DataConn.StoreFillDS("SP_UserControl_Material", System.Data.CommandType.StoredProcedure, txtUser.Text.ToString());
                    }


                }

            }


        }
        protected void btnaddTranfer_Click(object sender, EventArgs e)
        {

            string User = txtUser_Tranfer.Text.ToString();
            string FullName = txtFull_Tranfer.Text.ToString();
            string Password = txtPass_Tranfer.Text.ToString().Trim();
            string Repassword = txtRePass_Tranfer.Text.ToString().Trim();
            string Role = ddlRole_Tranfer.SelectedValue.ToString();
            string Email = txtEmail_Tranfer.Text.ToString();
            string RoleDept = ddlRoleDept_Tranfer.SelectedValue.ToString();
            string RoleID = ddlRole_Tranfer.SelectedValue.ToString();
            string DeptID = ddlDept_Tranfer.SelectedValue.ToString();
            string Stock = txtStock_Tranfer.Text.ToString();
            string RolePage = ddlRolePage_Tranfer.SelectedValue.ToString();
            bool CheckSave = true;
            if (User == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please input information User.');", true);
                CheckSave = false;
                return;
            }
            if (FullName == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please input information FullName.');", true);
                CheckSave = false;
                return;
            }
            if (Password == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please input information Password.');", true);
                CheckSave = false;
                return;
            }
            else
            {
                if ((string.Compare(Password, Repassword) != 0))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please input information Password not sample RePassword.');", true);
                    CheckSave = false;
                    return;
                }

            }
            if (ddlRoleDept_Tranfer.SelectedValue.ToString() == "STORE")
            {
                if (txtStock_Tranfer.Text.ToString() == "")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please input information Stock.');", true);
                    CheckSave = false;
                    return;
                }
            }

            if (CheckSave == true)
            {
                
                {
                    int row = DataConn.ExecuteStore("SP_UserControl_Tranfer_Insert", CommandType.StoredProcedure, User, FullName, Password, Role, Email, RoleDept, RoleID, DeptID, Stock, RolePage);
                    if (row == 0)
                    {
                        //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";

                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Insert information user sucessfully');", true);
                        dtUser = DataConn.StoreFillDS("SP_UserControl_Tranfer_Search", System.Data.CommandType.StoredProcedure, txtUser.Text.ToString());
                    }


                }

            }


        }

        protected void txtUser_TextChanged(object sender, EventArgs e)
        {
            LoadDataExit_Issuemateril();
        }

        public void LoadDataExit_Issuemateril()
        {
            DataTable dt = DataConn.StoreFillDS("SP_UserControl_Material", System.Data.CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                txtFullName.Text = dt.Rows[0]["FullName"].ToString();
                txtPassword.Text = dt.Rows[0]["Password"].ToString();
                txtRepassword.Text = dt.Rows[0]["Password"].ToString();
                txtStock.Text = dt.Rows[0]["Stock"].ToString();
                ddl_Dept.SelectedValue = dt.Rows[0]["DeptName"].ToString();
                ddl_Role.SelectedValue = dt.Rows[0]["RoleID"].ToString();
                ddl_Rolepage.SelectedValue = dt.Rows[0]["RolePage"].ToString();
                txtStock.Text = dt.Rows[0]["Stock"].ToString();
                ddl_RoleDept.Text = dt.Rows[0]["RoleDept"].ToString();
             //  dtUser = DataConn.StoreFillDS("SP_UserControl_Material", System.Data.CommandType.StoredProcedure, txtUser.Text.ToString());
            }
        }
        public void LoadDataExit_TranferMaterial()
        {
            DataTable dt = DataConn.StoreFillDS("SP_UserControl_Tranfer_Search", System.Data.CommandType.StoredProcedure, txtUser_Tranfer.Text.ToString());
            if (dt.Rows.Count > 0)
            {
                txtFull_Tranfer.Text = dt.Rows[0]["FullName"].ToString();
                txtPass_Tranfer.Text = dt.Rows[0]["Password"].ToString();
                txtRepassword.Text = dt.Rows[0]["Password"].ToString();
                txtStock_Tranfer.Text = dt.Rows[0]["Stock"].ToString();
                ddlDept_Tranfer.SelectedValue = dt.Rows[0]["CostCenter"].ToString();
                ddlRole_Tranfer.SelectedValue = dt.Rows[0]["RoleID"].ToString();
                ddlRolePage_Tranfer.SelectedValue = dt.Rows[0]["RolePage"].ToString();
                txtStock_Tranfer.Text = dt.Rows[0]["Stock"].ToString();
                ddlRoleDept_Tranfer.Text = dt.Rows[0]["RoleDept"].ToString();
                dtUser = DataConn.StoreFillDS("SP_UserControl_Tranfer_Search", System.Data.CommandType.StoredProcedure, txtUser.Text.ToString());
            }
        }

        public void LoadData_Material (string DateUpload)
        {
            dtUser = DataConn.StoreFillDS("SP_UserControl_LoadUpload", System.Data.CommandType.StoredProcedure, DateUpload);
        }
        public void LoadData_Tranfer(string DateUpload)
        {
            dtUserTranfer = DataConn.StoreFillDS("SP_UserControl_Tranfer_LoadUpload", System.Data.CommandType.StoredProcedure, DateUpload);
        }


        protected void bttUpload_Click(object sender, EventArgs e)
        {
            Upload_Material();
        }
        public void Upload_Material()
        {

            string Flag_UploadMaterial = "NG";
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
                
                if (FileUpload1.FileName == "Upload_User_Material.xlsx" || FileUpload1.FileName == "Upload_User_Material.xls")
                {
                    FileUpload1.SaveAs(link_path);
                    //Connection String to Excel Workbook            
                    if (strFileType.Trim() == ".xls")
                    {
                        //connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + link_path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"");
                        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]  where UserLogin is not null", MyConnection);
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
                        //MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$] where UserLogin is not null", MyConnection);
                        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection);
                        MyCommand.TableMappings.Add("Table", "TestTable");
                        DtSet = new System.Data.DataSet();
                        MyCommand.Fill(DtSet);
                        dt = DtSet.Tables[0];
                        MyConnection.Close();
                    }

                    string sql_ = "";
                    string sql_2 = "";
                    string sql_3 = "";
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtUser = new DataTable();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //RequestNo,PartNo,MOQ,LeadTime,CustomerCode, Deadline,Remask,Peson_Incharge,Date_Incharge,Insert_user,Update_Date
                            string UserLogin = null; string FullName = null; string Pass = null; string CostCenter  = null; string Email = null;
                            string RoleFuntion = null; string RoleDept = null; string Stock = null; string Rolepage = null;string DeptName;


                            if (dt.Rows[i][0].ToString() != "")
                            {
                                UserLogin = dt.Rows[i][0].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data UserLogin at row :(" + (i + 1).ToString() + ") is null');", true);

                                return;
                            }
                            if (dt.Rows[i][1].ToString() != "")
                            {
                                FullName = dt.Rows[i][1].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data FullName at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }
                            if (dt.Rows[i][2].ToString() != "")
                            {
                                Pass = dt.Rows[i][2].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data PassWord at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }
                            if (dt.Rows[i][3].ToString() != "")
                            {
                                CostCenter = dt.Rows[i][3].ToString();
                                    
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Cost Center at row :(" + (i + 1).ToString() + ") is null');", true);

                                return;
                            }

                            if (dt.Rows[i][4].ToString() != "")
                            {
                                Email = dt.Rows[i][4].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Email at row :(" + (i + 1).ToString() + ") is null');", true);

                                return;
                            }

                            if (dt.Rows[i][5].ToString() != "")
                            {
                                RoleFuntion = dt.Rows[i][5].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Role Funtion at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }

                            if (dt.Rows[i][6].ToString() != "")
                            {
                                RoleDept = dt.Rows[i][6].ToString().ToUpper();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Role Dept at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }
                            Stock  = dt.Rows[i][7].ToString();

                            if (dt.Rows[i][8].ToString() != "")
                            {
                                Rolepage = dt.Rows[i][8].ToString();
                                if (Rolepage == "Tranfer" || Rolepage.ToUpper() == "TRANFER") // Material can't insert data Tranfer
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Canont insert user of Tranfer at row :(" + (i + 1).ToString() + ") ');", true);
                                    return;
                                }
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Role Page at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }
                            if (dt.Rows[i][9].ToString() != "")
                            {
                                DeptName = dt.Rows[i][9].ToString();

                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data DeptName at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }

                            DataTable dtCheckType = DataConn.DataTable_Sql("select count(isnull(AccountCost ,0)) as CheckDeptID from [tbl_ACC_Master]  where AccountCost =  '" + CostCenter + "'");

                            if (int.Parse(dtCheckType.Rows[0]["CheckDeptID"].ToString()) < 0)
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Cost Center not exits at row :(" + (i + 1).ToString() + ")');", true);
                                return;
                            }
                            else // Đúng định dạng của RQ
                            {
                                if (UserLogin != "" )
                                {
                                    //1.Kiểm tra User nếu không tồn tại trong bảng RQ( tài khoản login thì thêm vào)
                                    if (RoleDept == "RQ" || RoleDept == "ACC-CHECK")
                                    {
                                        DataTable dtCheckUser = DataConn.DataTable_Sql("select count(isnull(UserLogin ,0)) as LoginExit  from [dbo].[tbl_UserIssueRQ] where UserLogin  = '" + UserLogin + "' ");
                                        if (int.Parse(dtCheckUser.Rows[0]["LoginExit"].ToString()) == 0)
                                        {

                                            sql_ = sql_ + " INSERT INTO [tbl_UserIssueRQ] (UserLogin,[Password],FullName, CostCenter,Email,RoleID,RoleDept,RolePage,[DeptName])";
                                            sql_ = sql_ + " VALUES( '" + UserLogin + "','" + Pass + "',N'" + FullName + "','" + CostCenter + "','" + Email + "','" + RoleFuntion + "','" + RoleDept + "' ,'" + Rolepage + "','" + Rolepage + "')";

                                        }
                                    }
                                    if (Stock != "" && RoleDept.Trim() =="STORE")
                                    {
                                     
                                        //1.Kiểm tra Store nếu không tồn tại trong bảng Store( tài khoản login thì thêm vào)

                                        DataTable dtCheckStock = DataConn.DataTable_Sql("select count(isnull(UserName ,0)) as ExitsStock  from [tbl_UserStock_IssueMaterial]  where [UserName]  = '" + UserLogin + "' and  Stock = '" + Stock + "'");
                                        if (int.Parse(dtCheckStock.Rows[0]["ExitsStock"].ToString()) == 0)
                                        {

                                            sql_2 = sql_2 + " INSERT INTO [tbl_UserStock_IssueMaterial] (UserName,RoleID,RoleDept, Stock)";
                                            sql_2 = sql_2 + " VALUES( '" + UserLogin + "','" + RoleFuntion + "','" + RoleDept + "','" + Stock + "') ";
                                            //1.Kiểm tra User nếu không tồn tại trong bảng RQ( tài khoản login thì thêm vào)
                                            DataTable dtCheckUser = DataConn.DataTable_Sql("select count(isnull(UserLogin ,0)) as LoginExit  from [dbo].[tbl_UserIssueRQ] where UserLogin  = '" + UserLogin + "'");
                                            if (int.Parse(dtCheckUser.Rows[0]["LoginExit"].ToString()) == 0)
                                            {
                                                sql_3 = sql_3 + " INSERT INTO [tbl_UserIssueRQ] (UserLogin,[Password],FullName, CostCenter,Email,RoleID,RoleDept,RolePage)";
                                                sql_3 = sql_3 + " VALUES( '" + UserLogin + "','" + Pass + "',N'" + FullName + "','" + CostCenter + "','" + Email + "','" + RoleFuntion + "','RQ' ,'" + Rolepage + "')";
                                            }
                                        }
                                        else
                                        {
                                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data already Sock Material at row :(" + (i + 1).ToString() + ")');", true);
                                            return;
                                        }

                                    }

                                    if (sql_2 != "" || sql_ != "" || sql_3 != "")
                                    {
                                        
                                        if (sql_ != "")
                                        {
                                            DataConn.Execute_NonSQL(sql_);
                                            sql_ = "";
                                            Flag_UploadMaterial = "OK";
                                        }
                                        if (sql_3 != "")
                                        {
                                            DataConn.Execute_NonSQL(sql_3);
                                            sql_3 = "";
                                            Flag_UploadMaterial = "OK";
                                        }
                                        if (sql_2 != "")
                                        {
                                            DataConn.Execute_NonSQL(sql_2);
                                            sql_2 = "";
                                            Flag_UploadMaterial = "OK";
                                        }
                                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Upload successfull....');", true);
                                    }


                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data have null at row :(" + (i + 1).ToString() + ")');", true);
                                    return;
                                   
                                }

                            }

                        }

                    }
                  
                    if (Flag_UploadMaterial == "OK")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Upload successfull....');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Can not upload  data ==> NG.');", true);
                        return;
                    }

                    //   Loadata_MaterialIssue();;
                    Loadata_MaterialIssue();

                if (System.IO.File.Exists(link_path))
                    {
                        System.IO.File.Delete(link_path);
                    }
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please you change the name of the excel file with the name: Upload_User_Material');", true);
                    return;

                }

                if (System.IO.File.Exists(link_path))
                {
                    System.IO.File.Delete(link_path);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please check data excel file !!!');", true);
                return;
            }



        }
        public void Upload_Tranfer()
        {
            string Flag_Upload = "NG";
            System.Data.OleDb.OleDbConnection MyConnection;
            System.Data.DataSet DtSet;
            System.Data.OleDb.OleDbDataAdapter MyCommand;
            DataTable dt = new DataTable();
            dt = null;
            if (UploadTranfer.HasFile)
            {
                string strFileType = Path.GetExtension(UploadTranfer.FileName).ToLower();
                string path = UploadTranfer.PostedFile.FileName;
                string link_path = Server.MapPath("~/Upload_Request/" + DateTime.Now.ToString("yyyyMMdd") + "_" + UploadTranfer.FileName);

                if (UploadTranfer.FileName == "Upload_User_Tranfer.xlsx" || UploadTranfer.FileName == "Upload_User_Tranfer.xls")
                {
                    UploadTranfer.SaveAs(link_path);
                    //Connection String to Excel Workbook            
                    if (strFileType.Trim() == ".xls")
                    {
                        //connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + link_path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"");
                        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]  where UserLogin is not null", MyConnection);
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
                        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$] where UserLogin is not null", MyConnection);
                        MyCommand.TableMappings.Add("Table", "TestTable");
                        DtSet = new System.Data.DataSet();
                        MyCommand.Fill(DtSet);
                        dt = DtSet.Tables[0];
                        MyConnection.Close();
                    }

                    //string sql_ = "";
                    //string sql_2 = "";
                    //string sql_3 = "";
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtUser = new DataTable();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string sql_ = "";
                            string sql_2 = "";
                            string sql_3 = "";
                            //RequestNo,PartNo,MOQ,LeadTime,CustomerCode, Deadline,Remask,Peson_Incharge,Date_Incharge,Insert_user,Update_Date
                            string UserLogin = null; string FullName = null; string Pass = null; string CostCenter = null; string Email = null;
                            string RoleFuntion = null; string RoleDept = null; string Stock = null; string Rolepage = null;


                            if (dt.Rows[i][0].ToString() != "")
                            {
                                UserLogin = dt.Rows[i][0].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data UserLogin at row :(" + (i + 1).ToString() + ") is null');", true);

                                return;
                            }
                            if (dt.Rows[i][1].ToString() != "")
                            {
                                FullName = dt.Rows[i][1].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data FullName at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }
                            if (dt.Rows[i][2].ToString() != "")
                            {
                                Pass = dt.Rows[i][2].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data PassWord at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }
                            if (dt.Rows[i][3].ToString() != "")
                            {
                                CostCenter = dt.Rows[i][3].ToString();

                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Cost Center at row :(" + (i + 1).ToString() + ") is null');", true);

                                return;
                            }

                            if (dt.Rows[i][4].ToString() != "")
                            {
                                Email = dt.Rows[i][4].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Email at row :(" + (i + 1).ToString() + ") is null');", true);

                                return;
                            }

                            if (dt.Rows[i][5].ToString() != "")
                            {
                                RoleFuntion = dt.Rows[i][5].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Role Funtion at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }

                            if (dt.Rows[i][6].ToString() != "")
                            {
                                RoleDept = dt.Rows[i][6].ToString().ToUpper();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Role Dept at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }
                            Stock = dt.Rows[i][7].ToString();

                            if (dt.Rows[i][8].ToString() != "")
                            {
                                Rolepage = dt.Rows[i][8].ToString();
                                
                                if (Rolepage == "Material" || Rolepage.ToUpper() == "MATERIAL") // Material can't insert data Tranfer
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Canont insert user of Material at row :(" + (i + 1).ToString() + ") ');", true);
                                    return;

                                }
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Role Page at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }


                            DataTable dtCheckType = DataConn.DataTable_Sql("select count(isnull(AccountCost ,0)) as CheckDeptID from [tbl_ACC_Master]  where AccountCost =  '" + CostCenter + "'");

                            if (int.Parse(dtCheckType.Rows[0]["CheckDeptID"].ToString()) < 0)
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Cost Center not exits at row :(" + (i + 1).ToString() + ")');", true);
                                return;
                            }
                            else // Đúng định dạng của RQ
                            {
                                if (UserLogin != "")
                                {
                                    //1.Kiểm tra User nếu không tồn tại trong bảng RQ( tài khoản login thì thêm vào)
                                    if (RoleDept == "RQ" || RoleDept == "ACC-CHECK")
                                    {
                                        DataTable dtCheckUser = DataConn.DataTable_Sql("select count(isnull(UserLogin ,0)) as LoginExit  from [dbo].[tbl_UserIssueRQ] where UserLogin  = '" + UserLogin + "' and CostCenter = '" + CostCenter + "'");
                                        if (int.Parse(dtCheckUser.Rows[0]["LoginExit"].ToString()) == 0)
                                        {


                                            sql_ = sql_ + " INSERT INTO [tbl_UserIssueRQ] (UserLogin,[Password],FullName, CostCenter,Email,RoleID,RoleDept,RolePage)";
                                            sql_ = sql_ + " VALUES( '" + UserLogin + "','" + Pass + "','" + FullName + "','" + CostCenter + "','" + Email + "','" + RoleFuntion + "','" + RoleDept + "' ,'" + Rolepage + "')";



                                        }
                                    }
                                    if (Stock != "" && RoleDept == "STORE")
                                    {

                                        //1.Kiểm tra Store nếu không tồn tại trong bảng Store( tài khoản login thì thêm vào)

                                        DataTable dtCheckStock = DataConn.DataTable_Sql("select count(isnull(UserName ,0)) as ExitsStock  from  tbl_UserStock_Tranfer  where [UserName]  = '" + UserLogin + "' and  Stock = '" + Stock + "'");
                                        if (int.Parse(dtCheckStock.Rows[0]["ExitsStock"].ToString()) == 0)
                                        {

                                            sql_2 = sql_2 + " INSERT INTO [tbl_UserStock_Tranfer] (UserName,RoleID,RoleDept, Stock)";
                                            sql_2 = sql_2 + " VALUES( '" + UserLogin + "','" + RoleFuntion + "','" + RoleDept + "','" + Stock + "') ";
                                            //1.Kiểm tra User nếu không tồn tại trong bảng RQ( tài khoản login thì thêm vào)
                                            DataTable dtCheckUser = DataConn.DataTable_Sql("select count(isnull(UserLogin ,0)) as LoginExit  from [dbo].[tbl_UserIssueRQ] where UserLogin  = '" + UserLogin + "'");
                                            if (int.Parse(dtCheckUser.Rows[0]["LoginExit"].ToString()) == 0)
                                            {
                                                sql_3 = sql_3 + " INSERT INTO [tbl_UserIssueRQ] (UserLogin,[Password],FullName, CostCenter,Email,RoleID,RoleDept,RolePage)";
                                                sql_3 = sql_3 + " VALUES( '" + UserLogin + "','" + Pass + "',N'" + FullName + "','" + CostCenter + "','" + Email + "','" + RoleFuntion + "','RQ' ,'" + Rolepage + "')";
                                            }
                                        }
                                        else
                                        {
                                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data already Stock Tranfer at row :(" +  (i + 1).ToString() + ")');", true);
                                                return;
                                        }
                                    }

                                    if (sql_2 != "" || sql_ != "" || sql_3 != "")
                                    {
                                        if (sql_2 != "")
                                        {
                                            DataConn.Execute_NonSQL(sql_2);
                                            sql_2 = "";
                                            Flag_Upload = "OK";
                                        }
                                        if (sql_ != "")
                                        {
                                            DataConn.Execute_NonSQL(sql_);
                                            sql_ = "";
                                            Flag_Upload = "OK";
                                        }
                                        if (sql_3 != "")
                                        {
                                            DataConn.Execute_NonSQL(sql_3);
                                            sql_3 = "";
                                            Flag_Upload = "OK";
                                        }
                                    }

                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data have null at row :(" + (i + 1).ToString() + ")');", true);
                                    return;

                                }

                            }

                        }

                    }
                     if(Flag_Upload == "OK")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Upload successfull....');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Can not upload  data ==> NG.');", true);
                    }


                    Loadata_Tranfer();

                    if (System.IO.File.Exists(link_path))
                    {
                        System.IO.File.Delete(link_path);
                    }
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please you change the name of the excel file with the name: Upload_User_Tranfer');", true);

                }

                if (System.IO.File.Exists(link_path))
                {
                    System.IO.File.Delete(link_path);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please check data excel file !!!');", true);
            }
        }

        protected void bttTempFile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Template/Upload_User_Material.xlsx");
        }

        protected void bttPrint_Click(object sender, EventArgs e)
        {

            DataTable dt_ReportAll = new DataTable();
            DataTable dt_Status = new DataTable();
            string Request_NO;

            String titleReport = "Report list User IN-OUT Material";
            dt_ReportAll = DataConn.StoreFillDS("SP_UserControl_Material_report", CommandType.StoredProcedure );
            Users DataPDF = new Users(dt_ReportAll, titleReport);

            // Create a MigraDoc document
            Document document = DataPDF.CreateDocument();
            document.UseCmykColor = true;
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();
            string filename = "REPORT LIST USER IN-OUT MATERIAl";
            string lastfile = ".pdf";
            string link_report = Server.MapPath("~/Out_Report/" + filename + "_" + DateTime.Now.ToString("yyyy-MM-dd") + lastfile);
            // I don't want to close the document constantly...
            pdfRenderer.Save(link_report);
            //Process.Start(link_report);
            string path = Server.MapPath("~/Out_Report/" + filename + "" + "_" + "" + DateTime.Now.ToString("yyyy-MM-dd") + "" + lastfile + " ");
            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(path);
            if (buffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }
            File.Delete(link_report);

        }

        protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                
                string Usser = ((Label)e.Item.FindControl("lblUserLogin")).Text;
                string Stock = ((Label)e.Item.FindControl("lblStock")).Text;
                string Role = ((Label)e.Item.FindControl("lblRoleName")).Text;
            
                int row = DataConn.ExecuteStore("SP_UserControl_Delete", CommandType.StoredProcedure, Usser, Stock, Role);
                if (row == 0)
                {
                    Loadata_MaterialIssue();;
                       Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Delete user successfull....');", true);
                }
            }
        }

        protected void txtUser_Tranfer_TextChanged(object sender, EventArgs e)
        {
            LoadDataExit_TranferMaterial();

        }

        protected void bttUpload_Tranfer_Click(object sender, EventArgs e)
        {
            Upload_Tranfer();

        }

       
        protected void bttPrint_Tranfer_Click(object sender, EventArgs e)
        {
            DataTable dt_ReportAll = new DataTable();
            DataTable dt_Status = new DataTable();

            String titleReport = "Report list User Tranfer";
            dt_ReportAll = DataConn.StoreFillDS("SP_UserControl_Tranfer_report", CommandType.StoredProcedure);
            Users DataPDF = new Users(dt_ReportAll, titleReport);

            // Create a MigraDoc document
            Document document = DataPDF.CreateDocument();
            document.UseCmykColor = true;
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();
            string filename = "REPORT LIST USER TRANFER MATERIAl";
            string lastfile = ".pdf";
            string link_report = Server.MapPath("~/Out_Report/" + filename + "_" + DateTime.Now.ToString("yyyy-MM-dd") + lastfile);
            // I don't want to close the document constantly...
            pdfRenderer.Save(link_report);
            //Process.Start(link_report);
            string path = Server.MapPath("~/Out_Report/" + filename + "" + "_" + "" + DateTime.Now.ToString("yyyy-MM-dd") + "" + lastfile + " ");
            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(path);
            if (buffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }
            File.Delete(link_report);

        }

        protected void bttDownloadTemp_Tranfer_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Template/Upload_User_Tranfer.xlsx");
        }

        protected void rptUser_Tranfer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {

                string Usser = ((Label)e.Item.FindControl("lblUser_Tranfer")).Text;
                string Stock = ((Label)e.Item.FindControl("lblStock_Tranfer")).Text;
                string Role = ((Label)e.Item.FindControl("lblRoleName_Tranfer")).Text;

                int row = DataConn.ExecuteStore("SP_UserControl_Tranfer_Delete", CommandType.StoredProcedure, Usser, Stock, Role);
                if (row == 0)
                {
                    Loadata_Tranfer();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Delete user successfull....');", true);
                }
            }
        }

      
        protected void lnkUMateral_Click(object sender, EventArgs e)
            {
                mvtUserIssueMaterial.ActiveViewIndex = 0;
                mtvUserTranfer.ActiveViewIndex = -1;
                Loadata_MaterialIssue();

            }
        

        protected void lnkUTranfer_Click(object sender, EventArgs e)
        {

                this.mtvUserTranfer.ActiveViewIndex = 0;
                mvtUserIssueMaterial.ActiveViewIndex = -1;

                Loadata_Tranfer();
            }
    }
}
