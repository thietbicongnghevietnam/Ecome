
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

namespace MATERIAL_IN_OUT
{
    public partial class Material_Issues : System.Web.UI.Page
    {
        public DataTable dt_IssueMaterial = new DataTable();
        public DataTable dt_Comment = new DataTable();
        public DataTable dtUserCurrrent = new DataTable();

        private string Stock = "";
        private string EmailNext = "";
        private string Email_Pres = "";
        public string RQ = "";
        public static string header_;
        public DataTable dtPreEmail = new DataTable();
        public DataTable dtNextMail = new DataTable();
        public DataTable dtTreeRQ = new DataTable();
        public DataTable dtCheckStatus = new DataTable();
        public static string REQUESTID;
        public static string USER_NAME;
        public object __o;
        public string User_next = "";
        public string RoleOutStock = null;
        public string RoleMakeRQ = null;
        public string Role = null;
        public string Public_Dept = "";
        public string Request_NO = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string REQUESTID = Request.QueryString["RQ_NO"].ToString();
                User_next = Request.QueryString["UserName"].ToString();
                string ROLEID = Request.QueryString["Role"].ToString();
                string CostCenter = Request.QueryString["DeptID"].ToString();
                string Stock = Request.QueryString["Stock"].ToString();
                string RoleOutStock_ = Request.QueryString["RoleD"].ToString();
                string RoleMakeRQ_ = Request.QueryString["RoleD"].ToString();
                txtDateInput.Value = DateTime.Now.ToString("dd/MM/yyyy");

                if (REQUESTID != "" && REQUESTID != null && User_next != "" && ROLEID != "")
                {
                    Session["RequestID_1RQ"] = DataConn.Decrypt(REQUESTID);
                    Session["UserName"] = DataConn.Decrypt(User_next);
                    Session["Role"] = DataConn.Decrypt(ROLEID);
                    Session["Dept"] = DataConn.Decrypt(CostCenter);
                    Session["Stock"] = DataConn.Decrypt(Stock);
                    Session["RoleMakeRQ"] = DataConn.Decrypt(RoleMakeRQ_);
                    Session["RoleOutStock"] = DataConn.Decrypt(RoleOutStock_);
                    Load_TreeView_Search(Session["RequestID_1RQ"].ToString());
                    if (Session["RoleMakeRQ"].ToString() == "RQ" || Session["RoleMakeRQ"].ToString() == "ACC-CHECK")
                    {
                        LoadData_CreateRQ();
                    }
                    else
                    {
                        LoadData_Store();
                    }
                    Public_Dept = Session["Dept"].ToString().Trim();
                }
                else
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]) && string.IsNullOrEmpty((string)Session["Dept"]) && string.IsNullOrEmpty((string)Session["UserName"]) ||
                  string.IsNullOrEmpty((string)Session["Role"]) && string.IsNullOrEmpty((string)Session["Stock"]) && string.IsNullOrEmpty((string)Session["Role_Dept"]))
                    {
                        Response.Redirect("Login.aspx", false);
                    }
                    else
                    {
                        Public_Dept = Session["Dept"].ToString().Trim();
                        Load_Treeview_Management();
                        if (Session["RoleMakeRQ"].ToString() == "RQ" || Session["RoleMakeRQ"].ToString() == "ACC-CHECK")
                        {
                            LoadData_CreateRQ();
                        }
                        else
                        {
                            LoadData_Store();

                        }
                    }
                }
            }
        }
        public void Load_Treeview_Management()
        {
            treeNOT_Approved.Nodes.Clear();// Delete Node before ==> Load node again
            Public_Dept =Session["Dept"].ToString().Trim();
            TreeNode treeNodes = new TreeNode
            {
                Text = "Management IN-OUT Material Request"
            };
            treeNOT_Approved.Nodes.Add(treeNodes);
            DataTable table_Tree1 = new DataTable();
            table_Tree1 = DataConn.StoreFillDS("[SP_Issue_Material_BindRQ_NOTApproved]", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["RoleMakeRQ"].ToString(), Session["UserName"].ToString());
            if (table_Tree1.Rows.Count > 0)
            {
                TreeNode treeChild1 = new TreeNode
                {
                    Text = "List RQ(Not Approved)_Issue In"
                };

                treeNodes.ChildNodes.Add(treeChild1);
                for (int i = 0; i < table_Tree1.Rows.Count; i++)
                {
                    TreeNode treeChild3 = new TreeNode
                    {
                        Text = table_Tree1.Rows[i]["RequestNo"].ToString()
                    };
                    treeChild1.ChildNodes.Add(treeChild3);

                   // Search(table_Tree1.Rows[i]["RequestNo"].ToString());

                }
            }
            DataTable table_Tree2 = new DataTable();
            table_Tree2 = DataConn.StoreFillDS("SP_Issue_Material_BindRQ_NOTApproved_OutStock", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["RoleOutStock"].ToString(), Session["UserName"].ToString());
            if (table_Tree2.Rows.Count > 0)
            {
                TreeNode treeChild2 = new TreeNode
                {
                    Text = "List RQ(Not Approved)_Issue OutStock"
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



            DataTable dt_RequestApproved = new DataTable();
            dt_RequestApproved = DataConn.StoreFillDS("SP_Issue_Material_BindRQ_Approved", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["RoleMakeRQ"].ToString(), Session["UserName"].ToString());
            if (dt_RequestApproved.Rows.Count > 0)
            {
                TreeNode treeChild_Apporved1 = new TreeNode
                {
                    Text = "List RQ Approved)_Issue In"
                };

                treeNodes.ChildNodes.Add(treeChild_Apporved1);
                for (int i = 0; i < dt_RequestApproved.Rows.Count; i++)
                {
                    TreeNode treeChild3 = new TreeNode
                    {
                        Text = dt_RequestApproved.Rows[i]["RequestNo"].ToString()
                    };
                    treeChild_Apporved1.ChildNodes.Add(treeChild3);

                    // Search(table_Tree1.Rows[i]["RequestNo"].ToString());

                }
            }
            DataTable dt_RequestApproved2 = new DataTable();
            dt_RequestApproved2 = DataConn.StoreFillDS("SP_Issue_Material_BindRQ_Approved_OutStock", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["RoleOutStock"].ToString(), Session["UserName"].ToString());
            if (dt_RequestApproved2.Rows.Count > 0)
            {
                TreeNode treeChild_Approved2 = new TreeNode
                {
                    Text = "List RQ(Not Approved)_Issue OutStock"
                };

                treeNodes.ChildNodes.Add(treeChild_Approved2);
                for (int i = 0; i < dt_RequestApproved2.Rows.Count; i++)
                {
                    TreeNode treeChild5 = new TreeNode
                    {
                        Text = dt_RequestApproved2.Rows[i]["RequestNo"].ToString()
                    };
                    treeChild_Approved2.ChildNodes.Add(treeChild5);

                    // Search(table_Tree2.Rows[i]["RequestNo"].ToString());

                }
            }
        } //Load tree view list Request\
        public void Check_StatusFuntion_1Request(string RequestID, string User_next)
        {

            if (REQUESTID != "" && User_next != "")
            {

                Loadstatus(Session["RequestID_1RQ"].ToString());
                LoadButton(Session["RequestID_1RQ"].ToString());
            }

        }
        public void Load_TreeView_Search(string RQ)
        {

            Public_Dept =Session["Dept"].ToString().Trim();
            treeNOT_Approved.Nodes.Clear();// Delete Node before ==> Load node again
            TreeNode treeNodes = new TreeNode
            {
                Text = "Management In-Out Request"
            };
            // string RQ_1 = Session["RequestID_1RQ"].ToString();

            if (RQ.ToString() == null)
            {

                DataTable dt = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString());
                if (dt.Rows.Count > 0)
                {
                    RQ = dt.Rows[0]["RequestNo"].ToString();
                }

            }

            treeNOT_Approved.Nodes.Add(treeNodes);

            TreeNode treeChild1 = new TreeNode
            {
                Text = "List RQ Need Approve"
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
        public void Search(string RQ)
        {
            Public_Dept =Session["Dept"].ToString().Trim();
            dt_IssueMaterial = DataConn.FillStore("[SP_Issue_Material_Search]", CommandType.StoredProcedure, RQ);
            dt_Comment = DataConn.FillStore("SP_Issue_Material_Comment_Load", CommandType.StoredProcedure, RQ);
            if (dt_IssueMaterial.Rows.Count > 0)
            {

                lblPlant.Text = dt_IssueMaterial.Rows[0]["Plant"].ToString();
                lblPlantName.Text = dt_IssueMaterial.Rows[0]["NamePlant"].ToString();
              //  txtVoucherDate.Value.ToString() = dt_IssueMaterial.Rows[0]["DateVoucher"].ToString();
                lblMvTYpe.Text = dt_IssueMaterial.Rows[0]["MvType"].ToString();
                lblIN.Text = dt_IssueMaterial.Rows[0]["MvName"].ToString();
                lblCostCenter.Text = dt_IssueMaterial.Rows[0]["CostCenter"].ToString();
                lblCostCenterName.Text = dt_IssueMaterial.Rows[0]["DeptName"].ToString();
                lblVendorCode.Text = "Vendor:" + dt_IssueMaterial.Rows[0]["VendorCode"].ToString();
                lblvendorName.Text = dt_IssueMaterial.Rows[0]["VendorName"].ToString();
                lblAcount.Text = dt_IssueMaterial.Rows[0]["AccountCost"].ToString();
                lblAccountName.Text = dt_IssueMaterial.Rows[0]["AccountName"].ToString();
                hdfStock.Value = dt_IssueMaterial.Rows[0]["Sloc"].ToString();
                hdfRequest.Value = dt_IssueMaterial.Rows[0]["RequestNo"].ToString();
                hdfUserUpdate.Value = Session["UserName"].ToString();
                lblRequest.Text = dt_IssueMaterial.Rows[0]["TypeName"].ToString();


                dtUserCurrrent = DataConn.StoreFillDS("[SP_RQMaterial_CurrentUser]", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStock.Value.ToString(), Public_Dept);
                if (dtUserCurrrent.Rows.Count > 0)
                {
                    RoleDept = dtUserCurrrent.Rows[0]["RoleDept"].ToString();
                    Role = dtUserCurrrent.Rows[0]["RoleID"].ToString();
                    hdfRoleDeptUpdate.Value = RoleDept;
                    hdfRoleupdate.Value = Role;
                }

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
                    }
                    if (int.Parse(Role) == 3) // Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = false;
                        bttReject.Visible = false;

                        bttPrint.Visible = false;
                    }
                }
                if (RoleDept == "ACC-CHECK" || RoleDept == "STORE")
                {
                    if (int.Parse(Role) == 1)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = false;
                        bttReject.Visible = false;

                        bttPrint.Visible = false;
                    }
                    if (int.Parse(Role) == 2)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = false;
                        bttReject.Visible = false;

                        bttPrint.Visible = false;
                    }
                    if (int.Parse(Role) == 3)// Show status button permistion Requester

                    {
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
        public void LoadData_CreateRQ()
        {
            Public_Dept =Session["Dept"].ToString().Trim();
            if (treeNOT_Approved.SelectedNode == null)
            {
                if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                {
                    DataTable dt = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["RoleMakeRQ"].ToString());
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

            dt_IssueMaterial = DataConn.FillStore("SP_Issue_Material_Search", CommandType.StoredProcedure, RQ);
            dt_Comment = DataConn.FillStore("SP_Issue_Material_Comment_Load", CommandType.StoredProcedure, RQ);


            if (dt_IssueMaterial.Rows.Count > 0)
            {

                lblPlant.Text = dt_IssueMaterial.Rows[0]["Plant"].ToString();
                lblPlantName.Text = dt_IssueMaterial.Rows[0]["NamePlant"].ToString();
               // lblVoucherDate.Text = dt_IssueMaterial.Rows[0]["DateVoucher"].ToString();
                lblMvTYpe.Text = dt_IssueMaterial.Rows[0]["MvType"].ToString();
                lblIN.Text = dt_IssueMaterial.Rows[0]["MvName"].ToString();
                lblCostCenter.Text = dt_IssueMaterial.Rows[0]["CostCenter"].ToString();
                lblCostCenterName.Text = dt_IssueMaterial.Rows[0]["DeptName"].ToString();
                lblVendorCode.Text = "Vendor:" + dt_IssueMaterial.Rows[0]["VendorCode"].ToString();
                lblvendorName.Text = dt_IssueMaterial.Rows[0]["VendorName"].ToString();
                lblAcount.Text = dt_IssueMaterial.Rows[0]["AccountCost"].ToString();
                lblAccountName.Text = dt_IssueMaterial.Rows[0]["AccountName"].ToString();
                hdfStock.Value = dt_IssueMaterial.Rows[0]["Sloc"].ToString();
                hdfRequest.Value = dt_IssueMaterial.Rows[0]["RequestNo"].ToString();
                hdfUserUpdate.Value = Session["UserName"].ToString();
                lblRequest.Text = dt_IssueMaterial.Rows[0]["TypeName"].ToString();
                dtUserCurrrent = DataConn.StoreFillDS("[SP_RQMaterial_CurrentUser]", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStock.Value.ToString(), Public_Dept);
                if (dtUserCurrrent.Rows.Count > 0)
                {
                    RoleDept = dtUserCurrrent.Rows[0]["RoleDept"].ToString();
                    Role = dtUserCurrrent.Rows[0]["RoleID"].ToString();
                    hdfRoleDeptUpdate.Value = RoleDept;
                    hdfRoleupdate.Value = Role;
                }
                Loadstatus(RQ);
                LoadButton(RQ);
            }


            else
            {
                if (RoleDept == "" || RoleDept is null)
                {
                    bttApproved.Text = "Send RQ";
                }

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
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttPrint.Visible = true;
                        bttApproved.Text = "Check RQ";
                    }
                    if (int.Parse(Role) == 3) // Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttApproved.Text = "Approved RQ";
                        bttPrint.Visible = true;
                    }
                }
                if (RoleDept == "ACC-CHECK" || RoleDept == "STORE")
                {
                    if (int.Parse(Role) == 1)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttApproved.Text = "Check RQ";
                        bttPrint.Visible = true;
                    }
                    if (int.Parse(Role) == 2)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttApproved.Text = "MGR Approved";
                        bttPrint.Visible = true;
                    }
                    if (int.Parse(Role) == 3)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttApproved.Text = "GM Approved";
                        bttPrint.Visible = true;
                    }
                }

            }

        }

        public void LoadData_Store()
        {
            Public_Dept = Session["Dept"].ToString().Trim();
            if (treeNOT_Approved.SelectedNode == null)
            {
                if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                {
                    DataTable dt = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["RoleOutStock"].ToString());
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

            dt_IssueMaterial = DataConn.FillStore("SP_Issue_Material_Search", CommandType.StoredProcedure, RQ);
            dt_Comment = DataConn.FillStore("SP_Issue_Material_Comment_Load", CommandType.StoredProcedure, RQ);


            if (dt_IssueMaterial.Rows.Count > 0)
            {

                lblPlant.Text = dt_IssueMaterial.Rows[0]["Plant"].ToString();
                lblPlantName.Text = dt_IssueMaterial.Rows[0]["NamePlant"].ToString();
                // lblVoucherDate.Text = dt_IssueMaterial.Rows[0]["DateVoucher"].ToString();
                lblMvTYpe.Text = dt_IssueMaterial.Rows[0]["MvType"].ToString();
                lblIN.Text = dt_IssueMaterial.Rows[0]["MvName"].ToString();
                lblCostCenter.Text = dt_IssueMaterial.Rows[0]["CostCenter"].ToString();
                lblCostCenterName.Text = dt_IssueMaterial.Rows[0]["DeptName"].ToString();
                lblVendorCode.Text = "Vendor:" + dt_IssueMaterial.Rows[0]["VendorCode"].ToString();
                lblvendorName.Text = dt_IssueMaterial.Rows[0]["VendorName"].ToString();
                lblAcount.Text = dt_IssueMaterial.Rows[0]["AccountCost"].ToString();
                lblAccountName.Text = dt_IssueMaterial.Rows[0]["AccountName"].ToString();
                hdfStock.Value = dt_IssueMaterial.Rows[0]["Sloc"].ToString();
                hdfRequest.Value = dt_IssueMaterial.Rows[0]["RequestNo"].ToString();
                hdfUserUpdate.Value = Session["UserName"].ToString();
                lblRequest.Text = dt_IssueMaterial.Rows[0]["TypeName"].ToString();

                dtUserCurrrent = DataConn.StoreFillDS("[SP_RQMaterial_CurrentUser]", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStock.Value.ToString(), Public_Dept);
                if (dtUserCurrrent.Rows.Count > 0)
                {
                    RoleDept = dtUserCurrrent.Rows[0]["RoleDept"].ToString();
                    Role = dtUserCurrrent.Rows[0]["RoleID"].ToString();
                    hdfRoleDeptUpdate.Value = RoleDept;
                    hdfRoleupdate.Value = Role;
                }
                Loadstatus(RQ);
                LoadButton(RQ);
            }


            else
            {
                if (RoleDept == "" || RoleDept is null)
                {
                    bttApproved.Text = "Send RQ";
                }

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
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttPrint.Visible = true;
                        bttApproved.Text = "Check RQ";
                    }
                    if (int.Parse(Role) == 3) // Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttApproved.Text = "Approved RQ";
                        bttPrint.Visible = true;
                    }
                }
                if (RoleDept == "ACC-CHECK" || RoleDept == "STORE")
                {
                    if (int.Parse(Role) == 1)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttApproved.Text = "Check RQ";
                        bttPrint.Visible = true;
                    }
                    if (int.Parse(Role) == 2)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttApproved.Text = "MGR Approved";
                        bttPrint.Visible = true;
                    }
                    if (int.Parse(Role) == 3)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttApproved.Text = "GM Approved";
                        bttPrint.Visible = true;
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
            Public_Dept =Session["Dept"].ToString().Trim();
            string TypeRQID = Session["TypeID"].ToString().Trim();
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
                if (FileUpload1.FileName == "UploadMaterInOut_A.xlsx" || FileUpload1.FileName == "UploadMaterInOut_A.xls")
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

                    string sql_ = "";
                    string sql_Reset = "";
                    if (dt.Rows.Count > 0)
                    {
                        DataTable DtIssuse = new DataTable();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //RequestNo,PartNo,MOQ,LeadTime,CustomerCode, Deadline,Remask,Peson_Incharge,Date_Incharge,Insert_user,Update_Date
                            string TypeRQ = null; string Material = null; string Sloc = null; float Qty = 0; string Mvtype = null; string Plant = null; float UnitPriceST = 0;
                            string CostCenter = null; string VendorCode = null; string Note = null; string DateVoucher = null; float Amount = 0; string MVName = null; string RQ_Reset = null;


                            if (dt.Rows[i][0].ToString() != "")
                            {
                                TypeRQ = dt.Rows[i][0].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Type RQ at row :(" + (i + 1).ToString() + ") is null');", true);

                                return;
                            }
                            if (dt.Rows[i][1].ToString() != "")
                            {
                                Material = dt.Rows[i][1].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Material at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }
                            if (dt.Rows[i][2].ToString() != "")
                            {
                                Sloc = dt.Rows[i][2].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Sloc at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }
                            if (dt.Rows[i][3].ToString() != "")
                            {
                                Qty = float.Parse(dt.Rows[i][3].ToString());
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Qty at row :(" + (i + 1).ToString() + ") is null');", true);

                                return;
                            }


                            if (dt.Rows[i][4].ToString() != "")
                            {
                                Mvtype = dt.Rows[i][4].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data MVType at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }


                            if (dt.Rows[i][5].ToString() != "")
                            {
                                MVName = dt.Rows[i][5].ToString();
                                DataTable dtCheckMV = new DataTable();
                                dtCheckMV = DataConn.StoreFillDS("SP_Issue_Material_Check_MVType", CommandType.StoredProcedure, Mvtype, MVName);
                                // Kiểm tra MV-TypeName không đúng Mv TypeID
                                if (int.Parse(dtCheckMV.Rows[0]["TotalMV"].ToString()) == 0)
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data MVName at row :(" + (i + 1).ToString() + ") is incorrect with MVType.');", true);
                                    return;
                                }
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data MVName at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }
                            if (dt.Rows[i][6].ToString() != "")
                            {
                                Plant = dt.Rows[i][6].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Plant at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }


                            if (dt.Rows[i][7].ToString() != "")
                            {
                                CostCenter = dt.Rows[i][7].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Cost Center at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }


                            string TypeCheck = TypeRQID;
                            string[] stringTypeID = { "2", "3", "4", "5", "8", "21" }; // Những Type sẽ phải điền Vendorcode

                            foreach (string x in stringTypeID)
                            {
                                int CheckTrung = string.Compare(TypeCheck.ToString().ToUpper().Trim(), x.ToUpper().Trim());

                                if (CheckTrung == 0)
                                {
                                    if (dt.Rows[i][9].ToString() != "")
                                    {
                                        VendorCode = dt.Rows[i][9].ToString();
                                    }
                                    else
                                    {
                                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Vendor code at row :(" + (i + 1).ToString() + ") is null');", true);
                                        return;
                                    }
                                    break;
                                }

                            }

                            Note = dt.Rows[i][9].ToString();

                            RQ_Reset = dt.Rows[i][10].ToString();
                            DataTable dtPrice = new DataTable();

                            dtPrice = DataConn.StoreFillDS("SP_MaterialIssue_GetPrice_SAP", CommandType.StoredProcedure, Plant, Material);
                            if (dtPrice.Rows.Count > 0)
                            {
                                UnitPriceST = float.Parse(dtPrice.Rows[0]["Price_STD"].ToString());
                                Amount = (float)Math.Round((UnitPriceST * Qty), 5);
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Upload NG... Reason is material (" + Material + ") of (" + Plant + ") not exits at table link SAP at row :(" + (i + 1).ToString() + ") ');", true);
                                return;
                            }
                            //if (dtPrice.Rows.Count >= 2)
                            //{
                            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Upload NG..Reason is material (" + Material + ") of (" + Plant + ") have 2 row in  table link SAP at row :(" + (i + 1).ToString() + ")  ');", true);
                            //    return;
                            //}

                            if(txtDateInput.Value.ToString() == "")
                            {

                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please chosen Date Voucher.');", true);
                                return;
                            }
                            else
                            {
                                DateVoucher = txtDateInput.Value.ToString();
                            }

                            DataTable dtMax = DataConn.DataTable_Sql("select IsNull(max( cast( right(RequestNo, CHARINDEX('-',REVERSE(RequestNo))-1) as int)),0)  from  [dbo].tbl_RQ_MaterialIssue  ");
                            int Max_ID = int.Parse(dtMax.Rows[0][0].ToString());
                            if (dtMax.Rows[0][0].ToString() == "0")
                            {
                                Max_ID = 1;
                            }
                            else
                            {
                                Max_ID = Max_ID + 1;
                            }


                            string Request_NO = "RQA-" + Public_Dept + "-" + DateTime.Now.ToString("MMyy") + "-" + Max_ID.ToString();
                            DataTable dtCheckType = DataConn.DataTable_Sql("select count(isnull(TypeID,0)) as CheckID from [dbo].[tbl_TypeMater]  where TypeID = '" + TypeRQ + "'");
                            string stringToCheck = Sloc; // Stock in excel file upload

                            string StockUser = Session["Stock"].ToString().Trim();

                            if (StockUser.Contains(stringToCheck))
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Upload NG... You can not upload Stock (" + Sloc + ") in  list stock (" + StockUser + ") you control.');", true);
                                return;
                            }
                            ////--1. Kiểm tra CostCenter trong file excel phải giống với cost Center của user.
                            //if ((string.Compare(CostCenter.ToString().Trim(),Session["Dept"].ToString().Trim())) != 0)
                            //{
                            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Upload NG...CostCenter of user not sample Cost Center upload.');", true);
                            //    return;
                            //}
                            //--2. Kiểm tra TypeID trong file excel phải giống với Type ID  của user khi upload.
                            else if ((string.Compare(TypeRQ.ToString().Trim(), TypeRQID.ToString().Trim())) != 0)
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Upload NG...TypeID (" + TypeRQID + ") of user chosen not sample TypeID in file upload.');", true);
                                return;
                            }
                            //--2. Kiểm tra TypeID trong file excel trong master TypeID 
                            else
                            {

                                if (int.Parse(dtCheckType.Rows[0]["CheckID"].ToString()) < 0)
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Type RQ not exits at row :(" + (i + 1).ToString() + ")');", true);
                                    return;
                                }
                                else // Đúng định dạng của RQ
                                {
                                    DataTable dtCheckMV = DataConn.DataTable_Sql("select count(isnull(MVTypeCode,0)) as CheckMV from tbl_MV_Master  where  MVTypeCode =  '" + Mvtype + "'  and TypeID  = '" + TypeRQ + "'");
                                    if (int.Parse(dtCheckMV.Rows[0]["CheckMV"].ToString()) > 0)
                                    {
                                        DataTable dtCheckRQ = DataConn.DataTable_Sql("select count(isnull(RequestNo,0)) as CheckRQ from tbl_RQ_MaterialIssue  where  [RequestNo]  = '" + RQ_Reset + "'");

                                        if (TypeRQ != "" && RQ_Reset != "") // Upload RQ
                                        {
                                            if (int.Parse(dtCheckRQ.Rows[0]["CheckRQ"].ToString())  >0 )
                                            {
                                                sql_Reset = sql_Reset + " UPDATE tbl_RQ_MaterialIssue ";
                                                sql_Reset = sql_Reset + " SET  [TypeID] = '" + TypeRQ + "'  ,[IssueQty] =  " + Qty + " ,[UnitPrice_ST] = " + UnitPriceST + "  ,[MvType] = '" + Mvtype + "',";
                                                sql_Reset = sql_Reset + "MvName =  '" + MVName + "' ,[Plant] = '" + Plant + "' ,Note = N'" + Note + "',DateVoucher =  '" + DateVoucher + "' ,Amount_ST = " + Amount + "  ,VendorCode = '" + VendorCode + "' ,CostCenter = '" + CostCenter + "' ,Sloc = '" + Sloc + "',";
                                                sql_Reset = sql_Reset + "[UserUpdate] = '" + Session["UserName"].ToString() + "',[DateUpdate] = '" + DateTime.Now.ToString() + "'";
                                                sql_Reset = sql_Reset + "Where[RequestNo]  = '" + RQ_Reset + "' and [Material] = '" + Material + "'  ";
                                                Session["RQ_Upload"] = RQ_Reset;
                                            }
                                            else
                                            {
                                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('NG.Upload because not exits RQ at row :(" + (i + 1).ToString() + ") for Type RQID');", true);
                                                return;
                                            }

                                        }
                                        else
                                        {

                                            if (TypeRQ != "" || Material != "" || Sloc != "" || Mvtype != "" || Plant != "" || CostCenter != "")
                                            {
                                                sql_ = sql_ + " INSERT INTO tbl_RQ_MaterialIssue ([RequestNo],[Material],[TypeID], [IssueQty],[UnitPrice_ST],[MvType],MvName,[Plant],Note,DateVoucher,Amount_ST , VendorCode,CostCenter,Sloc,UserCreate) ";
                                                sql_ = sql_ + " VALUES( '" + Request_NO + "','" + Material + "','" + TypeRQ + "'," + Qty + "," + UnitPriceST + " ,'" + Mvtype + "','" + MVName + "','" + Plant + "' ,N'" + Note + "', '" + DateVoucher + "'," + Amount + ",'" + VendorCode + "','" + CostCenter + "','" + Sloc + "','" + Session["UserName"].ToString() + "') ";
                                                Session["RQ_Upload"] = Request_NO;
                                            }
                                        }

                                    }
                                    else
                                    {

                                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data (MVType or AccountCode ) not correct  at row :(" + (i + 1).ToString() + ") for Type RQID');", true);
                                        return;

                                    }
                                }

                            }
                        }

                    }
                    if ( sql_ != "")
                    {
                        DataConn.Execute_NonSQL(sql_);
                        Search(Session["RQ_Upload"].ToString());
                        Load_TreeView_Search(Session["RQ_Upload"].ToString());
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Upload successful....');", true);
                    }
                    else if ( sql_Reset != "")
                    {
                        DataConn.Execute_NonSQL(sql_Reset);
                        Search(Session["RQ_Upload"].ToString());
                        Load_TreeView_Search(Session["RQ_Upload"].ToString());
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Upload successful....');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('NG.Upload unsuccessful ....');", true);
                        LoadData();
                    }
                    hdfStatus_Upload.Value = "OK";
                  
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please you change the name of the excel file with the name: UploadMaterInOut_A');", true);

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
        protected void treeNOT_Approved_SelectedNodeChanged(object sender, EventArgs e)
        {

            Search(treeNOT_Approved.SelectedNode.Value.ToString());

        }
        protected void treeApproved_SelectedNodeChanged(object sender, EventArgs e)
        {
            Search(treeApproved.SelectedNode.Value.ToString());
        }
        public void SendEmail_Next(string RequestID, string DeptID, string user_Current, string subject, string Content_Comment)
        {
            //1 Get Role_Next , User_Next, RoleDept_Next
            string Email_NextUser;
            Public_Dept =Session["Dept"].ToString().Trim();
            string UserNext = null; string RoleNext = null; string RoleDeptNext = null; string Stock = null;
            dtNextMail = DataConn.StoreFillDS("[SP_Issue_Material_NextUser]", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStock.Value.ToString(), DeptID);
            if (dtNextMail.Rows.Count > 0)
            {// Có thể gửi cho nhiều người 
                for (int j = 0; j < dtNextMail.Rows.Count; j++)
                {

                    UserNext = dtNextMail.Rows[j]["UserLogin"].ToString();
                    RoleNext = dtNextMail.Rows[j]["RoleID"].ToString();
                    RoleDeptNext = dtNextMail.Rows[j]["RoleDept"].ToString();
                    Email_NextUser = dtNextMail.Rows[j]["Email"].ToString();
                    Stock = hdfStock.Value.ToString();



                    System.Net.Mail.SmtpClient objClient = new System.Net.Mail.SmtpClient("157.8.1.131");
                    subject = subject.Replace('\r', ' ').Replace('\n', ' ');
                    System.Net.Mail.MailAddress mail = new System.Net.Mail.MailAddress("tax.psnv@vn.panasonic.com", "Issue In-Out Material");
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
                    using (StreamReader reader2 = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Fomat_SendEmail/EmailApproved_A.html")))
                    {
                        header_ = reader2.ReadToEnd();
                    }

                    header_ = header_.Replace("RQID", DataConn.Encrypt(RequestID.ToString()));
                    header_ = header_.Replace("USID", DataConn.Encrypt(UserNext.ToString()));
                    header_ = header_.Replace("RoleUser", DataConn.Encrypt(RoleNext.ToString()));
                    header_ = header_.Replace("RoleDept", DataConn.Encrypt(RoleDeptNext.ToString()));
                    header_ = header_.Replace("CostCenter", DataConn.Encrypt(Public_Dept));
                    header_ = header_.Replace("StockOut", DataConn.Encrypt(hdfStock.Value.ToString()));
                    objMessage.Body = header_;
                    // string[] ToEmailList = ToEmail.Split(',');
                    // foreach (string Email_To in ToEmailList)
                    // {
                    objMessage.To.Add(new MailAddress(Email_NextUser));
                    objClient.Send(objMessage);
                    // }
                }
            }
        }
        public void SendEmail_Prv(string RequestID, string DeptID, string user_Current, string subject, string ToEmail, string Content_Comment)
        {

            //1 Get Role_Next , User_Next, RoleDept_Next
            Public_Dept =Session["Dept"].ToString().Trim();
            string UserNext = null; string RoleNext = null; string RoleDeptNext = null;
            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStock.Value.ToString(), DeptID, RequestID);
            if (dtPreEmail.Rows.Count > 0)
            {
                UserNext = dtPreEmail.Rows[0]["UserLogin"].ToString();
                RoleNext = dtPreEmail.Rows[0]["RoleID"].ToString();
                RoleDeptNext = dtPreEmail.Rows[0]["RoleDept"].ToString();
            }
            System.Net.Mail.SmtpClient objClient = new System.Net.Mail.SmtpClient("157.8.1.131");
            subject = subject.Replace('\r', ' ').Replace('\n', ' ');
            System.Net.Mail.MailAddress mail = new System.Net.Mail.MailAddress("tax.psnv@vn.panasonic.com", "Issue In-Out Material");
            System.Net.Mail.MailMessage objMessage = new System.Net.Mail.MailMessage();
            string[] ToEmailList = ToEmail.Split(',');


            objMessage.Priority = MailPriority.High;
            objMessage.IsBodyHtml = true;
            objMessage.From = mail;
            objMessage.Subject = subject;
            objMessage.IsBodyHtml = true;
            //Get data Header Images to Content of Email
            string Header_Email = "";
            Header_Email += "";
            using (StreamReader reader2 = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Fomat_SendEmail/EmailApproved_A.html")))
            {
                header_ = reader2.ReadToEnd();
            }
            header_ = header_.Replace("RQID", DataConn.Encrypt(RequestID.ToString()));
            header_ = header_.Replace("USID", DataConn.Encrypt(UserNext.ToString()));
            header_ = header_.Replace("RoleUser", DataConn.Encrypt(RoleNext.ToString()));
            header_ = header_.Replace("RoleDept", DataConn.Encrypt(RoleDeptNext.ToString()));
            header_ = header_.Replace("CostCenter", DataConn.Encrypt(Public_Dept));
            header_ = header_.Replace("StockOut", DataConn.Encrypt(hdfStock.Value.ToString()));


            objMessage.Body = header_;
            foreach (string Email_To in ToEmailList)
            {
                objMessage.To.Add(new MailAddress(Email_To));
                objClient.Send(objMessage);
            }
        }
        public void Loadstatus(string RequestNo)
        {
            Public_Dept =Session["Dept"].ToString().Trim();
            dtCheckStatus = DataConn.StoreFillDS("[SP_Issue_Material_CheckStatus]", CommandType.StoredProcedure, RequestNo);
            if (dtCheckStatus.Rows.Count > 0)
            {
                //---
                lblIssueCharge_Name.Text = dtCheckStatus.Rows[0]["NameInchage_Issue"].ToString();
                lblIssueMGR_Name.Text = dtCheckStatus.Rows[0]["NameMGR_Issue"].ToString();
                lblIssueGM_Name.Text = dtCheckStatus.Rows[0]["NameGM_Issue"].ToString();
                lblIssueCharge_Date.Text = dtCheckStatus.Rows[0]["DateInchage_Issue"].ToString();
                lblIssueMGR_Date.Text = dtCheckStatus.Rows[0]["DateMGR_Issue"].ToString();
                lblIssueGM_Date.Text = dtCheckStatus.Rows[0]["DateGM_Issue"].ToString();
                hdf_IssueCharge.Value = dtCheckStatus.Rows[0]["StatusInchage_Issue"].ToString();
                hdf_IssueGM.Value = dtCheckStatus.Rows[0]["StatusGM_Issue"].ToString();
                hdf_IssueMGR.Value = dtCheckStatus.Rows[0]["StatusMGR_Issue"].ToString();
                hdf_ACCCheck.Value = dtCheckStatus.Rows[0]["StatusACC_Check"].ToString();
                hdf_ACCMGR.Value = dtCheckStatus.Rows[0]["StatusACC_MGR"].ToString();
                hdf_ACCGM.Value = dtCheckStatus.Rows[0]["StatusACC_GM"].ToString();
                hdf_OutCharge.Value = dtCheckStatus.Rows[0]["StatusInchage_Out"].ToString();
                hdf_OutGM.Value = dtCheckStatus.Rows[0]["StatusGM_Out"].ToString();
                hdf_OutMGR.Value = dtCheckStatus.Rows[0]["StatusMGR_Out"].ToString();

                if (hdf_IssueCharge.Value == "OK")
                {
                    img_IssueCharge.ImageUrl = "~/images/Approved.png";
                }
                else if (hdf_IssueCharge.Value == "NG")
                {
                    img_IssueCharge.ImageUrl = "~/images/Reject.png";
                }

                else if (hdf_IssueCharge.Value == "" || hdf_IssueCharge.Value == null)
                {
                    img_IssueCharge.ImageUrl = "";
                }

                if (hdf_IssueMGR.Value == "OK")
                {
                    img_IssueMGR.ImageUrl = "~/images/Approved.png";
                }
                else if (hdf_IssueMGR.Value == "NG")
                {
                    img_IssueMGR.ImageUrl = "~/images/Reject.png";
                }
                else if (hdf_IssueMGR.Value == "" || hdf_IssueMGR.Value == null)
                {
                    img_IssueMGR.ImageUrl = "";
                }
                if (hdf_IssueGM.Value == "OK")
                {
                    img_IssueGM.ImageUrl = "~/images/Approved.png";
                }
                else if (hdf_IssueGM.Value == "NG")
                {
                    img_IssueGM.ImageUrl = "~/images/Reject.png";
                }
                else if (hdf_IssueGM.Value == "" || hdf_IssueGM.Value == null)
                {
                    img_IssueGM.ImageUrl = "";
                }
                //--
                lblACCCheck_Name.Text = dtCheckStatus.Rows[0]["NameACC_Check"].ToString();
                lblACCGM_Name.Text = dtCheckStatus.Rows[0]["NameACC_GM"].ToString();
                lblMGR_Name.Text = dtCheckStatus.Rows[0]["NameACC_MGR"].ToString();
                lblACCGM_Date.Text = dtCheckStatus.Rows[0]["DateACC_GM"].ToString();
                lblACCCheck_Date.Text = dtCheckStatus.Rows[0]["DateACC_Check"].ToString();
                lblACCMGR_Date.Text = dtCheckStatus.Rows[0]["DateACC_MGR"].ToString();
                if (hdf_ACCCheck.Value == "OK")
                {
                    img_ACCCheck.ImageUrl = "~/images/Approved.png";
                }
                else if (hdf_ACCCheck.Value == "NG")
                {
                    img_ACCCheck.ImageUrl = "~/images/Reject.png";
                }

                else if (hdf_ACCCheck.Value == "" || hdf_ACCCheck.Value == null)
                {
                    img_ACCCheck.ImageUrl = "";
                }

                if (hdf_ACCMGR.Value == "OK")
                {
                    img_ACCMGR.ImageUrl = "~/images/Approved.png";
                }
                else if (hdf_ACCMGR.Value == "NG")
                {
                    img_ACCMGR.ImageUrl = "~/images/Reject.png";
                }
                else if (hdf_ACCMGR.Value == "" || hdf_ACCMGR.Value == null)
                {
                    img_ACCMGR.ImageUrl = "";
                }
                if (hdf_ACCGM.Value == "OK")
                {
                    img_ACCCGM.ImageUrl = "~/images/Approved.png";
                }
                else if (hdf_ACCGM.Value == "NG")
                {
                    img_ACCCGM.ImageUrl = "~/images/Reject.png";
                }
                else if (hdf_ACCGM.Value == "" || hdf_ACCGM.Value == null)
                {
                    img_ACCCGM.ImageUrl = "";
                }
                //-
                lblOutCharge_Name.Text = dtCheckStatus.Rows[0]["NameInchage_Out"].ToString();
                lblOutCheck_Name.Text = dtCheckStatus.Rows[0]["NameMGR_Out"].ToString();
                lblOutGM_Name.Text = dtCheckStatus.Rows[0]["NameGM_Out"].ToString();
                lblOutCharge_Date.Text = dtCheckStatus.Rows[0]["DateInchage_Out"].ToString();
                lblOutCheck_Date.Text = dtCheckStatus.Rows[0]["DateMGR_Out"].ToString();
                lblOutGM_Date.Text = dtCheckStatus.Rows[0]["DateGM_Out"].ToString();
                if (hdf_OutCharge.Value == "OK")
                {
                    img_OutCharge.ImageUrl = "~/images/Approved.png";
                }
                else if (hdf_OutCharge.Value == "NG")
                {
                    img_OutCharge.ImageUrl = "~/images/Reject.png";
                }

                else if (hdf_OutCharge.Value == "" || hdf_OutCharge.Value == null)
                {
                    img_OutCharge.ImageUrl = "";
                }

                if (hdf_OutMGR.Value == "OK")
                {
                    img_OutMGR.ImageUrl = "~/images/Approved.png";
                }
                else if (hdf_OutMGR.Value == "NG")
                {
                    img_OutMGR.ImageUrl = "~/images/Reject.png";
                }
                else if (hdf_OutMGR.Value == "" || hdf_OutMGR.Value == null)
                {
                    img_OutMGR.ImageUrl = "";
                }
                if (hdf_OutGM.Value == "OK")
                {
                    img_OutGM.ImageUrl = "~/images/Approved.png";
                }
                else if (hdf_OutGM.Value == "NG")
                {
                    img_OutGM.ImageUrl = "~/images/Reject.png";
                }
                else if (hdf_OutGM.Value == "" || hdf_OutGM.Value == null)
                {
                    img_OutGM.ImageUrl = "";
                }

            }

        }
        public void LoadButton(string RequestNo)
        {
            string RoleDept = null; string Role = null;
            Public_Dept =Session["Dept"].ToString().Trim();
            string Issue_Incharge = "", Issue_MGR = "", Issue_GM = "", ACC_Check = "", ACC_MGR = "", ACC_GM = "", Out_MGR = "", Out_Incharge = "", Out_GM = "";
            bttReset.Visible = false;
            bttUpload.Visible = false;
            FileUpload1.Visible = false;
            bttTempFile.Visible = false;
            dtUserCurrrent = DataConn.StoreFillDS("[SP_RQMaterial_CurrentUser]", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStock.Value.ToString(), Public_Dept);
            if (dtUserCurrrent.Rows.Count > 0)
            {
                RoleDept = dtUserCurrrent.Rows[0]["RoleDept"].ToString();
                Role = dtUserCurrrent.Rows[0]["RoleID"].ToString();
                hdfRoleDeptUpdate.Value = RoleDept;
                hdfRoleupdate.Value = Role;
            }
            if (RequestNo == "")
            {

                if (int.Parse(Role) == 1)
                {
                    bttApproved.Text = "Sent Report";
                    bttReject.Visible = false;
                    bttApproved.Enabled = true;
                    bttTempFile.Visible = true;
                    bttReset.Visible = false;
                    bttUpload.Visible = true;
                    FileUpload1.Visible = true;


                }
                if (int.Parse(Role) == 2)
                {
                    bttApproved.Text = "Check Request";
                    bttReject.Enabled = true;
                    bttApproved.Enabled = true;

                    bttReset.Visible = false;
                }
                if (int.Parse(Role) == 3)
                {
                    bttApproved.Text = "Approved";
                    bttReject.Enabled = true;
                    bttApproved.Enabled = true;

                    bttReset.Visible = false;
                }

            }
            else
            {
                dtCheckStatus = DataConn.StoreFillDS("SP_Issue_Material_CheckStatus", CommandType.StoredProcedure, RequestNo);
                if (dtCheckStatus.Rows.Count > 0)
                {
                    Issue_Incharge = dtCheckStatus.Rows[0]["StatusInchage_Issue"].ToString();
                    Issue_MGR = dtCheckStatus.Rows[0]["StatusMGR_Issue"].ToString();
                    Issue_GM = dtCheckStatus.Rows[0]["StatusGM_Issue"].ToString();
                    ACC_Check = dtCheckStatus.Rows[0]["StatusACC_Check"].ToString();
                    ACC_MGR = dtCheckStatus.Rows[0]["StatusACC_MGR"].ToString();
                    ACC_GM = dtCheckStatus.Rows[0]["StatusACC_GM"].ToString();
                    Out_MGR = dtCheckStatus.Rows[0]["StatusMGR_Out"].ToString();
                    Out_Incharge = dtCheckStatus.Rows[0]["StatusInchage_Out"].ToString();
                    Out_GM = dtCheckStatus.Rows[0]["StatusGM_Out"].ToString();
                }
                dtUserCurrrent = DataConn.StoreFillDS("[SP_RQMaterial_CurrentUser]", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStock.Value.ToString(), Public_Dept);
                if (dtUserCurrrent.Rows.Count > 0)
                {
                    RoleDept = dtUserCurrrent.Rows[0]["RoleDept"].ToString();
                    Role = dtUserCurrrent.Rows[0]["RoleID"].ToString();
                    hdfRoleDeptUpdate.Value = RoleDept;
                    hdfRoleupdate.Value = Role;
                }
                //if (Session["UserName"].ToString() != null && Session["Role"].ToString() != null && Session["Role_Dept"].ToString() != null)

                //{
                //    hdfRoleDeptUpdate.Value = Session["Role_Dept"].ToString();
                //    hdfRoleupdate.Value = Session["Role"].ToString();
                //    Role = Session["Role"].ToString();
                //    RoleDept = Session["Role_Dept"].ToString();
                //}
                if ((int.Parse(Role) == 1) && (RoleDept == "RQ"))// Show status button permistion Requester
                {
                    if ((Issue_MGR == "NG") || (Issue_GM == "NG") || (ACC_Check == "NG") || (ACC_MGR == "NG") || (Out_MGR == "NG") || (Out_Incharge == "NG") || (Out_GM == "NG"))
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
                        if (Issue_Incharge == "OK")
                        {
                            bttApproved.Text = "Sent Request";
                            bttReject.Visible = false;
                            bttApproved.Enabled = false;

                        }
                        else if ((string.Equals(Issue_Incharge, null) == true) || (string.Equals(Issue_Incharge, "") == true))
                        {

                            bttApproved.Text = "Sent Request";
                            bttApproved.Enabled = true;
                            bttApproved.Visible = true;
                            bttReject.Visible = true;
                        }
                        if (Issue_Incharge == "OK")
                        {
                            bttApproved.Text = "Sent Request";
                            bttReject.Visible = false;
                            bttApproved.Enabled = false;

                        }

                    }
                    if (int.Parse(Role) == 2) // Show status button permistion Checker
                    {


                        if (Issue_MGR == "OK")
                        {
                            bttApproved.Text = "Issue'dept Check";
                            bttApproved.Enabled = false;
                            bttReject.Enabled = false;

                        }
                        else if ((string.Equals(Issue_MGR, null) == true) || string.Equals(Issue_MGR, "") == true)
                        {
                            bttApproved.Text = "Issue'dept Check ";
                            bttReject.Visible = true;
                            bttReject.Enabled = true;
                            bttApproved.Enabled = true;
                            bttApproved.Visible = true;

                        }
                        else if (Issue_MGR == "NG")
                        {
                            bttApproved.Text = "Issue'dept Check";
                            bttReject.Enabled = false;
                            bttApproved.Enabled = false;

                        }

                    }
                    if (int.Parse(Role) == 3) // Show status button permistion Approved
                    {


                        if (Issue_GM == "OK")
                        {
                            bttApproved.Text = "GM Approved";
                            bttApproved.Enabled = false;
                            bttReject.Enabled = false;



                        }
                        else if ((string.Equals(Issue_GM, null) == true) || (string.Equals(Issue_GM, "") == true))
                        {
                            bttApproved.Text = "GM Approved";
                            bttReject.Visible = true;
                            bttReject.Enabled = true;
                            bttApproved.Enabled = true;
                            bttApproved.Visible = true;


                        }
                        if (Issue_GM == "NG")
                        {
                            bttApproved.Text = "GM Approved";
                            bttReject.Enabled = false;
                            bttApproved.Enabled = false;



                        }
                    }

                }
                if (RoleDept == "ACC-CHECK")
                {

                    if (int.Parse(Role) == 1) // Show status button permistion Requester
                    {

                        if (ACC_Check == "OK")
                        {
                            bttApproved.Text = "Check";
                            bttReject.Enabled = false;
                            bttApproved.Enabled = false;

                        }
                        else if ((string.Equals(ACC_Check, null) == true) || (string.Equals(ACC_Check, "") == true))
                        {


                            bttApproved.Text = "Check ";
                            bttReject.Visible = true;
                            bttReject.Enabled = true;
                            bttApproved.Enabled = true;
                            bttApproved.Visible = true;
                        }

                        else if (ACC_Check == "NG")
                        {

                            bttApproved.Text = "ACC Check";
                            bttApproved.Enabled = true;
                            bttReject.Enabled = true;
                        }
                    }
                    if (int.Parse(Role) == 2) // Show status button permistion Checker
                    {
                        if (ACC_MGR == "OK")
                        {
                            bttApproved.Text = "MGR Check";
                            bttApproved.Enabled = false;
                            bttReject.Enabled = false;


                        }

                        else if ((string.Equals(ACC_MGR, null) == true) || string.Equals(ACC_MGR, "") == true)
                        {
                            bttApproved.Text = "MGR Check ";
                            bttReject.Visible = true;
                            bttReject.Enabled = true;
                            bttApproved.Enabled = true;
                            bttApproved.Visible = true;


                        }

                        else if (ACC_MGR == "NG")
                        {
                            bttApproved.Text = "MGR Check";
                            bttReject.Enabled = false;
                            bttApproved.Enabled = false;

                        }

                    }
                    if (int.Parse(Role) == 3) // Show status button permistion Approved
                    {
                        if (ACC_GM == "OK")
                        {
                            bttApproved.Text = "GM Approved";
                            bttApproved.Enabled = false;
                            bttReject.Enabled = false;


                        }
                        else if ((string.Equals(ACC_GM, null) == true) || (string.Equals(ACC_GM, "") == true))
                        {
                            bttApproved.Text = "GM Approved";
                            bttReject.Visible = true;
                            bttReject.Enabled = true;
                            bttApproved.Enabled = true;
                            bttApproved.Visible = true;


                        }

                        if (ACC_GM == "NG")
                        {
                            bttApproved.Text = "GM Approved";
                            bttReject.Enabled = false;
                            bttApproved.Enabled = false;



                        }
                    }

                }
                if (RoleDept == "STORE")
                {
                    if (int.Parse(Role) == 1) // Show status button permistion Requester
                    {

                        if ((Out_Incharge == "OK") || (Out_Incharge == "OK"))
                        {
                            bttApproved.Text = "Out'dept Check";
                            bttReject.Enabled = false;
                            bttApproved.Enabled = false;

                        }

                        else if ((string.Equals(Out_Incharge, null) == true) || (string.Equals(Out_Incharge, "") == true))
                        {

                            bttApproved.Text = "Out'dept Check";
                            bttApproved.Enabled = true;
                            bttApproved.Visible = true;
                            bttReject.Visible = true;
                            bttReject.Enabled = true;

                        }

                        else if (Out_Incharge == "NG")
                        {

                            bttApproved.Enabled = false;
                            bttReject.Enabled = false;
                            bttReset.Enabled = false;
                        }
                    }
                    if (int.Parse(Role) == 2) // Show status button permistion Checker
                    {
                        if (Out_MGR == "OK")
                        {
                            bttApproved.Text = "Approved MGR ";
                            bttApproved.Enabled = false;
                            bttReject.Enabled = false;

                        }

                        else if (((string.Equals(Out_MGR, null) == true) || string.Equals(Out_MGR, "") == true))
                        {
                            bttApproved.Text = "Approved MGR ";
                            bttReject.Visible = true;
                            bttReject.Enabled = true;
                            bttApproved.Enabled = true;
                            bttApproved.Visible = true;

                        }

                        else if (Out_MGR == "NG")
                        {
                            bttApproved.Text = "Approved MGR";
                            bttReject.Enabled = false;
                            bttApproved.Enabled = false;

                        }

                    }
                    if (int.Parse(Role) == 3) // Show status button permistion Approved
                    {
                        if (Out_GM == "OK")
                        {
                            bttApproved.Text = "Approved GM";
                            bttApproved.Enabled = false;
                            bttReject.Enabled = false;

                        }

                        else if (((string.Equals(Out_GM, null) == true) || (string.Equals(Out_GM, "") == true)))
                        {
                            bttApproved.Text = "Approved GM";
                            bttReject.Visible = true;
                            bttReject.Enabled = true;
                            bttApproved.Enabled = true;
                            bttApproved.Visible = true;


                        }

                        if (Out_GM == "NG")
                        {
                            bttApproved.Text = "Approved GM";
                            bttReject.Enabled = false;
                            bttApproved.Enabled = false;
                        }
                    }

                }
            }

        }
        protected void bttApproved_Click(object sender, EventArgs e)
        {
            string Request_NO = "";
            Public_Dept =Session["Dept"].ToString().Trim();
            dtNextMail = DataConn.StoreFillDS("[SP_Issue_Material_NextUser]", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStock.Value.ToString(), Public_Dept);

            dtUserCurrrent = DataConn.StoreFillDS("[SP_RQMaterial_CurrentUser]", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStock.Value.ToString(), Public_Dept);
            if (dtUserCurrrent.Rows.Count > 0)
            {
                RoleDept = dtUserCurrrent.Rows[0]["RoleDept"].ToString();
                Role = dtUserCurrrent.Rows[0]["RoleID"].ToString();
                hdfRoleDeptUpdate.Value = RoleDept;
                hdfRoleupdate.Value = Role;
            }

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
            if (treeNOT_Approved.SelectedNode == null)
            {
                if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                {
                    dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString());
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
            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStock.Value.ToString(), Public_Dept, Request_NO);
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
            DataTable dtStatus = DataConn.StoreFillDS("SP_Issue_Material_CheckStatus", CommandType.StoredProcedure, Request_NO);
            if (dtStatus.Rows.Count < 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please check information of RequestNo');", true);
                return;

            }
            else
            {
                string Issue_Incharge = dtStatus.Rows[0]["StatusInchage_Issue"].ToString();
                string Issue_MGR = dtStatus.Rows[0]["StatusMGR_Issue"].ToString();
                string Issue_GM = dtStatus.Rows[0]["StatusGM_Issue"].ToString();
                string ACC_Check = dtStatus.Rows[0]["StatusACC_Check"].ToString();
                string ACC_MGR = dtStatus.Rows[0]["StatusACC_MGR"].ToString();
                string ACC_GM = dtStatus.Rows[0]["StatusACC_GM"].ToString();
                string Out_Incharge = dtStatus.Rows[0]["StatusInchage_Out"].ToString();
                string Out_MGR = dtStatus.Rows[0]["StatusMGR_Out"].ToString();
                string Out_GM = dtStatus.Rows[0]["StatusGM_Out"].ToString();
                if (RoleDept == "RQ")
                {

                    if ((int.Parse(Role) == 1 && Issue_Incharge == "") || (int.Parse(Role) == 1 && Issue_Incharge == null))
                    {
                        int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                        if (row == 0)
                        {
                            //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                            string Subject = "[Issue out] - Request " + Request_NO + " has been submitted successfully";
                            string Comment = txt_Comment.Value;
                            SendEmail_Next(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Comment);
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has submited sucessfully');", true);

                        }
                    }

                    if ((int.Parse(Role) == 2 && string.Equals(Issue_Incharge, "") == true) || (int.Parse(Role) == 2 && string.Equals(Issue_Incharge, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        Search(Request_NO);
                        return;
                    }
                    else
                    {
                        if ((int.Parse(Role) == 2 && Issue_Incharge == "OK" && Issue_MGR == "") || (int.Parse(Role) == 2 && Issue_Incharge == "OK" && Issue_MGR == null))
                        {

                            int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = "[Issue out] - Request approval for" + Request_NO + ".";
                                string Comment = txt_Comment.Value;
                                SendEmail_Next(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has checked sucessfully');", true);

                            }

                        }

                    }


                    if ((int.Parse(Role) == 3 && string.Equals(Issue_MGR, "") == true) || (int.Parse(Role) == 3 && string.Equals(Issue_MGR, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        return;

                    }

                    else
                    {
                        if ((int.Parse(Role) == 3 && Issue_MGR == "OK" && Issue_GM == "") || (int.Parse(Role) == 3 && Issue_MGR == "OK" && Issue_GM == null))
                        {
                            int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = "Request check for" + Request_NO + ".";
                                string Comment = txt_Comment.Value;
                                SendEmail_Next(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has GM'Issue approved sucessfully');", true);

                            }
                        }
                    }

                }
                if (RoleDept.ToString() == "ACC-CHECK")
                {
                    if ((int.Parse(Role) == 1 && string.Equals(Issue_GM, "") == true) || (int.Parse(Role) == 1 && string.Equals(Issue_GM, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        Search(Request_NO);
                        return;
                    }
                    else
                    {
                        if ((int.Parse(Role) == 1 && Issue_GM == "OK" && ACC_Check == "") || (int.Parse(Role) == 1 && Issue_GM == "OK" && ACC_Check == null))
                        {

                            int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = "Request check for" + Request_NO + ".";

                                string Comment = txt_Comment.Value;
                                SendEmail_Next(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has  ACC Checked sucessfully');", true);

                            }

                        }

                    }


                    if ((int.Parse(Role) == 2 && string.Equals(ACC_Check, "") == true) || (int.Parse(Role) == 2 && string.Equals(ACC_Check, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        Search(Request_NO);
                        return;

                    }

                    else
                    {
                        if ((int.Parse(Role) == 2 && ACC_Check == "OK" && ACC_MGR == "") || (int.Parse(Role) == 2 && ACC_Check == "OK" && ACC_MGR == null))
                        {
                            int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";

                                string Subject = "Request approval for " + Request_NO + ".";
                                string Comment = txt_Comment.Value;
                                SendEmail_Next(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has   MGR'ACC checked  sucessfully');", true);

                            }
                        }
                    }
                    if ((int.Parse(Role) == 3 && string.Equals(ACC_MGR, "") == true) || (int.Parse(Role) == 3 && string.Equals(ACC_MGR, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        Search(Request_NO);
                        return;

                    }

                    else
                    {
                        if ((int.Parse(Role) == 3 && ACC_MGR == "OK" && ACC_GM == "") || (int.Parse(Role) == 2 && ACC_MGR == "OK" && ACC_GM == null))
                        {
                            int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = "Request check for" + Request_NO + ".";
                                string Comment = txt_Comment.Value;
                                SendEmail_Next(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has GM'ACC approved sucessfully');", true);

                            }
                        }
                    }
                }
                if (RoleDept.ToString() == "STORE")
                {
                    if ((int.Parse(Role) == 1 && string.Equals(ACC_GM, "") == true) || (int.Parse(Role) == 1 && string.Equals(ACC_GM, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        Search(Request_NO);
                        return;
                    }
                    else
                    {
                        if ((int.Parse(Role) == 1 && ACC_GM == "OK" && Out_Incharge == "") || (int.Parse(Role) == 1 && ACC_GM == "OK" && Out_Incharge == null))
                        {

                            int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = "Request check for" + Request_NO + ".";
                                string Comment = txt_Comment.Value;
                                SendEmail_Next(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has Issue Out's Incharge checked sucessfully');", true);

                            }

                        }

                    }


                    if ((int.Parse(Role) == 2 && string.Equals(Out_Incharge, "") == true) || (int.Parse(Role) == 2 && string.Equals(Out_Incharge, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        Search(Request_NO);
                        return;

                    }

                    else
                    {
                        if ((int.Parse(Role) == 2 && Out_Incharge == "OK" && Out_MGR == "") || (int.Parse(Role) == 2 && Out_Incharge == "OK" && Out_MGR == null))
                        {
                            int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = "Request approval for " + Request_NO + ".";
                                string Comment = txt_Comment.Value;
                                SendEmail_Next(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has Issue Out's MGR checked sucessfully');", true);

                            }
                        }
                    }
                    if ((int.Parse(Role) == 3 && string.Equals(Out_MGR, "") == true) || (int.Parse(Role) == 3 && string.Equals(Out_MGR, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        Search(Request_NO);
                        return;

                    }

                    else
                    {
                        if ((int.Parse(Role) == 3 && Out_MGR == "OK" && Out_GM == "") || (int.Parse(Role) == 2 && Out_MGR == "OK" && Out_GM == null))
                        {
                            int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = " Request " + Request_NO + " has been approved sucessfully.";
                                string Comment = txt_Comment.Value;
                                SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has  approved sucessfully');", true);
                            }
                        }
                    }

                }

            }

            Search(Request_NO);


        }
        protected void bttSend_Click(object sender, EventArgs e)
        {
            Public_Dept =Session["Dept"].ToString().Trim();
            string Request_NO = "";
            if (treeNOT_Approved.SelectedNode == null)
            {

                if (hdfRequest.Value.ToString() != null || hdfRequest.Value.ToString() != "")
                {
                    Request_NO = hdfRequest.Value.ToString();
                }

            }
            else
            {
                Request_NO = treeNOT_Approved.SelectedNode.Value.ToString();
            }

            //dtNextMail = DataConn.StoreFillDS("[SP_Issue_Material_NextUser]", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStock.Value.ToString(), Public_Dept);
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

            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStock.Value.ToString(), Public_Dept, Request_NO);
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
            string Comment = txt_Comment.Value;

            if (Session["UserName"].ToString() != null && Session["Role"].ToString() != null && Session["Role_Dept"].ToString() != null)

            {
                hdfRoleDeptUpdate.Value = Session["Role_Dept"].ToString();
                hdfRoleupdate.Value = Session["Role"].ToString();
                Role = Session["Role"].ToString();
                RoleDept = Session["Role_Dept"].ToString();
            }



            int row = DataConn.ExecuteStore("[SP_Issue_Material_Comment_Update]", CommandType.StoredProcedure, Request_NO, Comment, Session["UserName"].ToString(), RoleDept, Role);
            if (row == 0)
            {

                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                string Subject = " Request for comment-" + Request_NO + ".";
                dt_Comment = DataConn.FillStore("SP_Issue_Material_Comment_Load", CommandType.StoredProcedure, Request_NO);
                if (RoleDept == "RQ")
                {

                    if ((int.Parse(Role) == 1))
                    {
                        SendEmail_Next(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Comment);
                    }
                }
                else
                {
                    SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                }

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This comment has been sent sucessfully');", true);

            }
            txt_Comment.Value = "";
            Search(Request_NO);

        }
        protected void bttPrint_Click(object sender, EventArgs e)
        {
            Public_Dept =Session["Dept"].ToString().Trim();
            DataTable dt_ReportAll = new DataTable();
            DataTable dt_Status = new DataTable();


            if (treeNOT_Approved.SelectedNode == null)
            {
                if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                {
                    dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString());
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
            string titleReport = "Report Issue IN-OUT Material";
            dt_ReportAll = DataConn.StoreFillDS("SP_Issue_Material_Report", CommandType.StoredProcedure, Request_NO);
            dt_Status = DataConn.StoreFillDS("SP_Issue_Material_RQStatus", CommandType.StoredProcedure, Request_NO);


            PDF_A DataPDF = new PDF_A(dt_ReportAll, dt_Status, titleReport);

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
        protected void bttReject_Click(object sender, EventArgs e)
        {
            Public_Dept =Session["Dept"].ToString().Trim();
            string Request_NO = "";
            if (treeNOT_Approved.SelectedNode == null)
            {
                if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                {
                    dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString());
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

            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfStock.Value.ToString(), Public_Dept, Request_NO);

            if (Session["UserName"].ToString() != null && Session["Role"].ToString() != null && Session["Role_Dept"].ToString() != null)

            {

                hdfRoleDeptUpdate.Value = Session["Role_Dept"].ToString();
                hdfRoleupdate.Value = Session["Role"].ToString();
                Role = Session["Role"].ToString();
                RoleDept = Session["Role_Dept"].ToString();
            }
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


            DataTable dtStatus = DataConn.StoreFillDS("SP_Issue_Material_CheckStatus", CommandType.StoredProcedure, Request_NO);
            if (dtStatus.Rows.Count < 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please check information of RequestNo');", true);
                return;

            }
            else
            {
                string Issue_Incharge = dtStatus.Rows[0]["StatusInchage_Issue"].ToString();
                string Issue_MGR = dtStatus.Rows[0]["StatusMGR_Issue"].ToString();
                string Issue_GM = dtStatus.Rows[0]["StatusGM_Issue"].ToString();
                string ACC_Check = dtStatus.Rows[0]["StatusACC_Check"].ToString();
                string ACC_MGR = dtStatus.Rows[0]["StatusACC_MGR"].ToString();
                string ACC_GM = dtStatus.Rows[0]["StatusACC_GM"].ToString();
                string Out_Incharge = dtStatus.Rows[0]["StatusInchage_Out"].ToString();
                string Out_MGR = dtStatus.Rows[0]["StatusMGR_Out"].ToString();
                string Out_GM = dtStatus.Rows[0]["StatusGM_Out"].ToString();



                if (RoleDept.ToString() == "RQ")
                {

                    if ((int.Parse(Role) == 2 && string.Equals(Issue_Incharge, "") == true) || (int.Parse(Role) == 2 && string.Equals(Issue_Incharge, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        Search(Request_NO);
                        return;
                    }
                    else
                    {
                        if ((int.Parse(Role) == 2 && Issue_Incharge == "OK" && Issue_MGR == "") || (int.Parse(Role) == 2 && Issue_Incharge == "OK" && Issue_MGR == null))
                        {

                            int row = DataConn.ExecuteStore("[SP_Issue_Material_Reject]", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = "Request " + Request_NO + " has rejected and submit again.";
                                string Comment = txt_Comment.Value;
                                SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);


                            }

                        }

                    }


                    if ((int.Parse(Role) == 3 && string.Equals(Issue_MGR, "") == true) || (int.Parse(Role) == 3 && string.Equals(Issue_MGR, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        Search(Request_NO);
                        return;


                    }

                    else
                    {
                        if ((int.Parse(Role) == 3 && Issue_MGR == "OK" && Issue_GM == "") || (int.Parse(Role) == 3 && Issue_MGR == "OK" && Issue_GM == null))
                        {
                            int row = DataConn.ExecuteStore("[SP_Issue_Material_Reject]", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = "Request " + Request_NO + " has rejected and submit again.";
                                string Comment = txt_Comment.Value;
                                SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);

                            }
                        }
                    }

                }
                if (RoleDept.ToString() == "ACC-CHECK")
                {
                    if ((int.Parse(Role) == 1 && string.Equals(Issue_GM, "") == true) || (int.Parse(Role) == 2 && string.Equals(Issue_GM, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        Search(Request_NO);
                        return;
                    }
                    else
                    {
                        if ((int.Parse(Role) == 1 && Issue_GM == "OK" && ACC_Check == "") || (int.Parse(Role) == 1 && Issue_GM == "OK" && ACC_Check == null))
                        {

                            int row = DataConn.ExecuteStore("[SP_Issue_Material_Reject]", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = "Request " + Request_NO + " has rejected and submit again.";
                                string Comment = txt_Comment.Value;
                                SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);

                            }

                        }

                    }


                    if ((int.Parse(Role) == 2 && string.Equals(ACC_Check, "") == true) || (int.Parse(Role) == 2 && string.Equals(ACC_Check, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        Search(Request_NO);
                        return;

                    }

                    else
                    {
                        if ((int.Parse(Role) == 2 && ACC_Check == "OK" && ACC_MGR == "") || (int.Parse(Role) == 2 && ACC_Check == "OK" && ACC_MGR == null))
                        {
                            int row = DataConn.ExecuteStore("[SP_Issue_Material_Reject]", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = "Request " + Request_NO + " has rejected and submit again.";
                                string Comment = txt_Comment.Value;
                                SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);

                            }
                        }
                    }
                    if ((int.Parse(Role) == 3 && string.Equals(ACC_MGR, "") == true) || (int.Parse(Role) == 3 && string.Equals(ACC_MGR, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        Search(Request_NO);
                        return;

                    }

                    else
                    {
                        if ((int.Parse(Role) == 3 && ACC_MGR == "OK" && ACC_GM == "") || (int.Parse(Role) == 2 && ACC_MGR == "OK" && ACC_GM == null))
                        {
                            int row = DataConn.ExecuteStore("[SP_Issue_Material_Reject]", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = "Request " + Request_NO + " has rejected and submit again.";
                                string Comment = txt_Comment.Value;
                                SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);

                            }
                        }
                    }
                }
                if (RoleDept.ToString() == "STORE")
                {
                    if ((int.Parse(Role) == 1 && string.Equals(ACC_GM, "") == true) || (int.Parse(Role) == 1 && string.Equals(ACC_GM, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        Search(Request_NO);
                        return;
                    }
                    else
                    {
                        if ((int.Parse(Role) == 1 && ACC_GM == "OK" && Out_Incharge == "") || (int.Parse(Role) == 1 && ACC_GM == "OK" && Out_Incharge == null))
                        {

                            int row = DataConn.ExecuteStore("[SP_Issue_Material_Reject]", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = "Request " + Request_NO + " has rejected and submit again.";
                                string Comment = txt_Comment.Value;
                                SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);

                            }

                        }

                    }


                    if ((int.Parse(Role) == 2 && string.Equals(Out_Incharge, "") == true) || (int.Parse(Role) == 2 && string.Equals(Out_Incharge, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        Search(Request_NO);

                        return;


                    }

                    else
                    {
                        if ((int.Parse(Role) == 2 && Out_Incharge == "OK" && Out_MGR == "") || (int.Parse(Role) == 2 && Out_Incharge == "OK" && Out_MGR == null))
                        {
                            int row = DataConn.ExecuteStore("[SP_Issue_Material_Reject]", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = "Request " + Request_NO + " has rejected and submit again.";
                                string Comment = txt_Comment.Value;
                                SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);

                            }
                        }
                    }
                    if ((int.Parse(Role) == 3 && string.Equals(Out_MGR, "") == true) || (int.Parse(Role) == 3 && string.Equals(Out_MGR, null) == true))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                        Search(Request_NO);

                        return;


                    }

                    else
                    {
                        if ((int.Parse(Role) == 3 && Out_MGR == "OK" && Out_GM == "") || (int.Parse(Role) == 2 && Out_MGR == "OK" && Out_GM == null))
                        {
                            int row = DataConn.ExecuteStore("[SP_Issue_Material_Reject]", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = "Request " + Request_NO + " has rejected and submit again.";
                                string Comment = txt_Comment.Value;
                                SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);

                            }
                        }
                    }
                }



            }
            Search(Request_NO);
        }
        protected void bttTempFile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Template/UploadMaterInOut_A.xlsx");
        }
        protected void bttReset_Click(object sender, EventArgs e)
        {
            string Request_NO = "";
            Public_Dept =Session["Dept"].ToString().Trim();
            if (treeNOT_Approved.SelectedNode == null)
            {
                if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                {
                    dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString());
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

            int row = DataConn.ExecuteStore("[SP_Issue_Material_Reset]", CommandType.StoredProcedure, Request_NO);
            if (row == 0)
            {
                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                string Subject = "[Issue out] - Request " + Request_NO + " has been reset successfully";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has reset sucessfully');", true);
                Search(Request_NO);
            }
        }
    }
}