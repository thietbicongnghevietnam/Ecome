
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
    public partial class Material_Issues : System.Web.UI.Page
    {
        //public DataTable dt_IssueMaterial = new DataTable();
       // public DataTable dt_IssueMaterial;
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
        public string RoleDept = null;
        public string Role = null;
        public string Public_Dept = "";
        public string Request_NO = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string REQUESTID = Request.QueryString["RQ"].ToString();
                User_next = Request.QueryString["User"].ToString();
                string Dept = Request.QueryString["Dept"].ToString();
                string Stock = Request.QueryString["Stock"].ToString();
                string RoleDept = Request.QueryString["RoleRQ"].ToString();
                string RoleRQ_Apr = Request.QueryString["RoleRQ_Apr"].ToString();
                string RoleStore = Request.QueryString["RoleStore"].ToString();
                string RoleStore_Apr = Request.QueryString["RoleStore_Apr"].ToString();


                txtDateInput.Value = DateTime.Now.ToString("dd/MM/yyyy");

                if (REQUESTID != "" && Dept != null && User_next != "" && Stock != "" && (RoleDept != "" || RoleRQ_Apr != "" || RoleStore != "" || RoleStore_Apr != ""))
                {
                    //Session["RequestID_1RQ"] = DataConn.Decrypt(REQUESTID);
                    //Session["UserName"] = DataConn.Decrypt(User_next);
                    //Session["CostCenter"] = DataConn.Decrypt(Dept);
                    //Session["Stock"] = DataConn.Decrypt(Stock);
                    //Session["RoleOutStock"] = DataConn.Decrypt(RoleStore);
                    //Session["Role_Dept"] = DataConn.Decrypt(RoleDept);
                    //Session["Role_Aproved_Dept"] = DataConn.Decrypt(RoleRQ_Apr);
                    //Session["Role_Aproved_Stock"] = DataConn.Decrypt(RoleStore_Apr);

                    Session["RequestID_1RQ"] = REQUESTID;
                    Session["UserName"] = User_next;
                    Session["CostCenter"] = Dept;
                    Session["Stock"] = Stock;
                    Session["RoleOutStock"] = RoleStore;
                    Session["Role_Dept"] = RoleDept;
                    Session["Role_Aproved_Dept"] = RoleRQ_Apr;
                    Session["Role_Aproved_Stock"] = RoleStore_Apr;
                    hdfControlACC.Value = RoleDept;
                    hdfControlRQ.Value = RoleDept;
                    hdfControlStore.Value = RoleStore;
                    Public_Dept = Session["CostCenter"].ToString().Trim();
                    Load_TreeView_Search(Session["RequestID_1RQ"].ToString(), Session["Role_Dept"].ToString().Trim(), Session["RoleOutStock"].ToString().Trim());
                    LoadData_1RQ_FromEmail(Session["RequestID_1RQ"].ToString(), Session["Role_Dept"].ToString(), Session["Role_Aproved_Dept"].ToString(), Session["RoleOutStock"].ToString(), Session["Role_Aproved_Stock"].ToString().Trim());
                    
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
                        Public_Dept = Session["CostCenter"].ToString().Trim();
                        Load_Treeview_Management();
                        LoadData();
                    }
                }

            }
         
        }
        public void Load_TreeView_Search(string RQ, string RoleRQ, string RoleStore)
        {
            treeRQ_InMaterial.Nodes.Clear();
            treeRQ_OutMateial.Nodes.Clear();

            if (RoleRQ == "RQ" || RoleRQ == "ACC-CHECK")
            {
                TreeNode treeNodes = new TreeNode
                {
                    Text = "Management RQ Issue Dept"
                };

                treeRQ_InMaterial.Nodes.Add(treeNodes);


                if (RQ.ToString() != "")
                {
                    TreeNode treeChild1 = new TreeNode
                    {
                        Text = "List RQ Not Approve"
                    };
                    treeNodes.ChildNodes.Add(treeChild1);
                    TreeNode treeChild3 = new TreeNode
                    {
                        Text = RQ.ToString()
                    };
                    treeChild1.ChildNodes.Add(treeChild3);

                }

            }

            if (RoleStore == "STORE")
            {
                // BIND DATA TO LIST APPOROVED
                TreeNode treeOutMaterial = new TreeNode
                {
                    Text = "Management RQ Issue Out Dept"
                };
                treeRQ_OutMateial.Nodes.Add(treeOutMaterial);


                if (RQ.ToString() != "")
                {
                    TreeNode treeChild_Apporved1 = new TreeNode
                    {
                        Text = "List RQ Not Approve"
                    };

                    treeOutMaterial.ChildNodes.Add(treeChild_Apporved1);

                    TreeNode treeChild3 = new TreeNode
                    {
                        Text = RQ.ToString().Trim()
                    };
                    treeChild_Apporved1.ChildNodes.Add(treeChild3);

                }
            }

        }
       
        public void LoadData_1RQ_FromEmail(string RQ, string RoleRQ, string RoleApprovedRQ, string RoleStore, string RoleApprovedStore)
        {
            if (RoleRQ == "RQ" || RoleRQ == "ACC-CHECK")
            {
                Search(RQ, RoleApprovedRQ, RoleRQ);
                LoadButton_IN(RQ, RoleApprovedRQ, RoleRQ);
            }
            if (RoleStore == "STORE")

            {
                Search(RQ, RoleApprovedStore, RoleStore);
                LoadButton_Out(RQ, RoleApprovedStore, RoleStore);

            }
        }

        public void Load_Treeview_Management()
        {

            treeRQ_InMaterial.Nodes.Clear();
            treeRQ_OutMateial.Nodes.Clear();
            Public_Dept = Session["CostCenter"].ToString().Trim();
            TreeNode treeNodes = new TreeNode
            {
                Text = "Management RQ Issue Dept"
            };

            treeRQ_InMaterial.Nodes.Add(treeNodes);
            DataTable table_Tree1 = new DataTable();
            string Stock = Session["Stock"].ToString().Trim();
            string RoleApprove = Session["Role_Aproved_Dept"].ToString().Trim();
            string RoleRQ = Session["Role_Dept"].ToString().Trim();
            string User = Session["UserName"].ToString().Trim();

            table_Tree1 = DataConn.StoreFillDS("SP_Issue_Material_BindRQ_NOTApproved", CommandType.StoredProcedure, Public_Dept, Stock, RoleRQ, User, RoleApprove);
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
            table_Tree2 = DataConn.StoreFillDS("SP_Issue_Material_BindRQ_Approved", CommandType.StoredProcedure, Public_Dept, Stock, RoleRQ, User, RoleApprove);
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

            // BIND DATA TO LIST APPOROVED
            TreeNode treeOutMaterial = new TreeNode
            {
                Text = "Management RQ Issue Out Dept"
            };
            treeRQ_OutMateial.Nodes.Add(treeOutMaterial);

            DataTable dt_RequestApproved = new DataTable();
            string Stock2 = Session["Stock"].ToString().Trim();
            string RoleApprove2 = Session["Role_Aproved_Stock"].ToString().Trim();
            string RoleOutStock = Session["RoleOutStock"].ToString().Trim();
            string User2 = Session["UserName"].ToString().Trim();
            dt_RequestApproved = DataConn.StoreFillDS("SP_Issue_Material_BindRQ_NOTApproved_OutStock", CommandType.StoredProcedure, Public_Dept, Stock2, RoleOutStock, RoleApprove2, User2);
            if (dt_RequestApproved.Rows.Count > 0)
            {
                TreeNode treeChild_Apporved1 = new TreeNode
                {
                    Text = "List RQ Not Approve"
                };

                treeOutMaterial.ChildNodes.Add(treeChild_Apporved1);
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
            dt_RequestApproved2 = DataConn.StoreFillDS("SP_Issue_Material_BindRQ_Approved_OutStock", CommandType.StoredProcedure, Public_Dept, Stock2, RoleOutStock, RoleApprove2, User2);
            if (dt_RequestApproved2.Rows.Count > 0)
            {
                TreeNode treeChild_Approved2 = new TreeNode
                {
                    Text = "List RQ Approved"

                };

                treeOutMaterial.ChildNodes.Add(treeChild_Approved2);
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
            }
        }
    
        public void Search(string RQ, string RoleAproved, string RoleDept)
        {
            Public_Dept = Session["CostCenter"].ToString().Trim();
            DataTable dt_IssueMaterial = new DataTable();
            dt_IssueMaterial = DataConn.FillStore("SP_Issue_Material_Search", CommandType.StoredProcedure, RQ);
            dtIssueMaterial.DataSource = dt_IssueMaterial;
            dtIssueMaterial.DataBind();
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



                Loadstatus(RQ);


            }
            else
            {
                if (RoleDept == "RQ")
                {
                    if (int.Parse(RoleAproved) == 1) // Show status button permistion Requester
                    {
                        bttUpload.Visible = true;
                        FileUpload1.Visible = true;
                        bttReset.Visible = true;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;

                        bttPrint.Visible = true;
                    }
                    if (int.Parse(RoleAproved) == 2)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = false;
                        bttReject.Visible = false;
                        bttPrint.Visible = false;
                    }
                    if (int.Parse(RoleAproved) == 3) // Show status button permistion Requester

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
                    if (int.Parse(RoleAproved) == 1)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = false;
                        bttReject.Visible = false;

                        bttPrint.Visible = false;
                    }
                    if (int.Parse(RoleAproved) == 2)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = false;
                        bttReject.Visible = false;

                        bttPrint.Visible = false;
                    }
                    if (int.Parse(RoleAproved) == 3)// Show status button permistion Requester

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
        public void LoadData()
        {
            Public_Dept = Session["CostCenter"].ToString().Trim();
            if (( Session["Role_Dept"].ToString().Trim() == "RQ" && Session["RoleOutStock"].ToString().Trim() == "") || (Session["Role_Dept"].ToString().Trim() == "RQ" && Session["RoleOutStock"].ToString().Trim() == "STORE"))
            {

                if (treeRQ_InMaterial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {
                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString().Trim());

                        }


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
                    Request_NO = treeRQ_InMaterial.SelectedNode.Value.ToString();
                }

                

            }
            if ( Session["Role_Dept"].ToString().Trim() == "ACC-CHECK")
            {
                if (treeRQ_InMaterial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {

                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString().Trim());

                        }

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
                    Request_NO = treeRQ_InMaterial.SelectedNode.Value.ToString();
                }
              
            }
            if ( Session["RoleOutStock"].ToString().Trim() == "STORE" && Session["Role_Dept"].ToString().Trim() == "")
            {

                if (treeRQ_OutMateial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {
                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["RoleOutStock"].ToString().Trim());
                        }

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
                    Request_NO = treeRQ_OutMateial.SelectedNode.Value.ToString();
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////
               
            }


            DataTable dt_IssueMaterial = new DataTable();
            dt_IssueMaterial = DataConn.FillStore("SP_Issue_Material_Search", CommandType.StoredProcedure, Request_NO);
            dt_Comment = DataConn.FillStore("SP_Issue_Material_Comment_Load", CommandType.StoredProcedure, Request_NO);
           dtIssueMaterial.DataSource = dt_IssueMaterial;
            dtIssueMaterial.DataBind();
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
                if (Session["RoleOutStock"].ToString().Trim() == "STORE" && Session["Role_Dept"].ToString().Trim() != "")
                {
                    Loadstatus(Request_NO);
                    LoadButton_Out(Request_NO, Session["Role_Aproved_Dept"].ToString().Trim(), Session["Role_Dept"].ToString().Trim());
                }
                if ((Session["Role_Dept"].ToString().Trim() == "RQ") || (Session["Role_Dept"].ToString().Trim() == "ACC-CHECK")||(Session["Role_Dept"].ToString().Trim() == "RQ" && Session["RoleOutStock"].ToString().Trim() == "STORE" ))
                {
                    Loadstatus(Request_NO);
                    LoadButton_IN(Request_NO, Session["Role_Aproved_Dept"].ToString().Trim(), Session["Role_Dept"].ToString().Trim());

                }
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
                if (RoleDept == "ACC-CHECK")
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
                if (RoleDept == "STORE")
                {
                    if (int.Parse(Role) == 1)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttApproved.Text = "Check OutStore";
                        bttPrint.Visible = true;
                    }
                    if (int.Parse(Role) == 2)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttApproved.Text = "MGR OutStore";
                        bttPrint.Visible = true;
                    }
                    if (int.Parse(Role) == 3)// Show status button permistion Requester

                    {
                        bttUpload.Visible = false;
                        FileUpload1.Visible = false;
                        bttReset.Visible = false;
                        bttApproved.Visible = true;
                        bttReject.Visible = true;
                        bttApproved.Text = "GM OutStore";
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
            Public_Dept = Session["CostCenter"].ToString().Trim();
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
               // if (FileUpload1.FileName == "UploadMaterInOut_A.xlsx" || FileUpload1.FileName == "UploadMaterInOut_A.xls")
               // {
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
                            string CountryOfOrgin = null; string ItemDescription = null;

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
                            if (dt.Rows[i][10].ToString() != "")
                            {
                                CountryOfOrgin = dt.Rows[i][10].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Country of Orgin at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }

                            if (dt.Rows[i][11].ToString() != "")
                            {
                                ItemDescription = dt.Rows[i][11].ToString();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('This data Item Description at row :(" + (i + 1).ToString() + ") is null');", true);
                                return;
                            }

                           RQ_Reset = dt.Rows[i][12].ToString();
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
                          txtDateInput.Value  = DateTime.Now.ToString("dd/MM/yyyy");
                            if (txtDateInput.Value.ToString() == "")
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

                            if (Public_Dept != "PUR") // Nếu Phòng PUR có thể upload tất cả các kho bộ phận khác vẫn phải theo danh sách kho.
                            {
                                if (!StockUser.Contains(stringToCheck))
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Upload NG... You can not upload Stock (" + Sloc + ") in  list stock (" + StockUser + ") you control.');", true);
                                    return;
                                }
                            }    
                            
                            ////--1. Kiểm tra CostCenter trong file excel phải giống với cost Center của user.
                            //if ((string.Compare(CostCenter.ToString().Trim(), Session["CostCenter"].ToString().Trim())) != 0)
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
                                            if (int.Parse(dtCheckRQ.Rows[0]["CheckRQ"].ToString()) > 0)
                                            {
                                                sql_Reset = sql_Reset + " UPDATE tbl_RQ_MaterialIssue ";
                                                sql_Reset = sql_Reset + " SET  [TypeID] = '" + TypeRQ + "'  ,[IssueQty] =  " + Qty + " ,[UnitPrice_ST] = " + UnitPriceST + "  ,[MvType] = '" + Mvtype + "',";
                                                sql_Reset = sql_Reset + "MvName =  '" + MVName + "' ,[Plant] = '" + Plant + "' ,Note = N'" + Note + "',DateVoucher =  '" + DateVoucher + "' ,Amount_ST = " + Amount + "  ,VendorCode = '" + VendorCode + "' ,CostCenter = '" + CostCenter + "' ,Sloc = '" + Sloc + "',";
                                                sql_Reset = sql_Reset + "[UserUpdate] = '" + Session["UserName"].ToString() + "',[DateUpdate] = '" + DateTime.Now.ToString() + "'";
                                                sql_Reset = sql_Reset + "[CountryofOrigin] = '" + CountryOfOrgin + "', [ItemDescription]  = '" +ItemDescription + "'";
                                              sql_Reset = sql_Reset + "Where[RequestNo]  = '" + RQ_Reset + "' and [Material] = '" + Material + "'  and  Status_RQ = 'Pending' ";
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
                                                sql_ = sql_ + " INSERT INTO tbl_RQ_MaterialIssue ([RequestNo],[Material],[TypeID], [IssueQty],[UnitPrice_ST],[MvType],MvName,[Plant],Note,DateVoucher,Amount_ST , VendorCode,CostCenter,Sloc,UserCreate,RQDeptID,CountryofOrigin,ItemDescription) ";
                                                sql_ = sql_ + " VALUES( '" + Request_NO + "','" + Material + "','" + TypeRQ + "'," + Qty + "," + UnitPriceST + " ,'" + Mvtype + "','" + MVName + "','" + Plant + "' ,N'" + Note + "', '" + DateVoucher + "'," + Amount + ",'" + VendorCode + "','" + CostCenter + "','" + Sloc + "','" + Session["UserName"].ToString() + "','"+ Public_Dept + "','" + CountryOfOrgin + "','" + ItemDescription + "') ";
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
                    if (sql_ != "")
                    {
                        DataConn.Execute_NonSQL(sql_);
                        Load_Treeview_Management();
                        Search(Session["RQ_Upload"].ToString(), Session["Role_Aproved_Dept"].ToString().Trim(), "RQ");
                        

                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Upload successfully....');", true);
                    }
                    else if (sql_Reset != "")
                    {
                        DataConn.Execute_NonSQL(sql_Reset);
                        Load_Treeview_Management();
                        Search(Session["RQ_Upload"].ToString(), Session["Role_Aproved_Dept"].ToString().Trim(), "RQ");
                        
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('Upload data for Reset successfully....');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('NG.Upload unsuccessfully ....');", true);
                        LoadData();
                    }
                    hdfStatus_Upload.Value = "OK";

               // }
                //else
                //{
                //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Please you change the name of the excel file with the name: UploadMaterInOut_A');", true);

                //}
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

        public void SendEmail_Next(string RQ, string UserCurrent, string DeptID, string Stock, string RoleRQ, string RoleApprovedRQ, string RoleStore, string RoleApprovedStore, string subject, string Content_Comment)
        {

            dtNextMail = DataConn.StoreFillDS("[SP_Issue_Material_NextUser]", CommandType.StoredProcedure, UserCurrent, DeptID, Stock, RoleRQ, RoleApprovedRQ, RoleStore, RoleApprovedStore);
            if (dtNextMail.Rows.Count > 0)
            {
                for (int j = 0; j < dtNextMail.Rows.Count; j++)
                {
                    string Request = RQ.ToString().Trim();
                    string UserNext = dtNextMail.Rows[j]["UserLogin"].ToString();
                    string DeptNext = dtNextMail.Rows[j]["Dept"].ToString();
                    string Stock_ = hdfStock.Value.ToString();
                    string RoleRQ_Next = dtNextMail.Rows[j]["RoleRQ"].ToString();
                    string RoleAppRQ_Next = dtNextMail.Rows[j]["RoleAprovedRQ"].ToString();
                    string RoleStore_Next = dtNextMail.Rows[j]["RoleStore"].ToString();
                    string RoleAppStore_Next = dtNextMail.Rows[j]["RoleAprovedStock"].ToString();
                    string Email_NextUser = dtNextMail.Rows[j]["Email"].ToString();
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

                    //header_ = header_.Replace("RQV", DataConn.Encrypt(RQID));
                    //header_ = header_.Replace("UserV", DataConn.Encrypt(UserNext_));
                    //header_ = header_.Replace("DeptV", DataConn.Encrypt(Public_Dept));
                    //header_ = header_.Replace("StockV", DataConn.Encrypt(Kho));
                    //header_ = header_.Replace("RoleRQV", DataConn.Encrypt(RoleDept));
                    //header_ = header_.Replace("RoleRQ_AprV", DataConn.Encrypt(RoleApproved));
                    //header_ = header_.Replace("RoleStoreV", DataConn.Encrypt(RoleOutStock));
                    //header_ = header_.Replace("RoleStore_AprV", DataConn.Encrypt(RoleApproved_Stock));
                    header_ = header_.Replace("RQV", Request);
                    header_ = header_.Replace("UserV", UserNext);
                    header_ = header_.Replace("DeptV", DeptNext);
                    header_ = header_.Replace("StockV", Stock_);
                    header_ = header_.Replace("value5", RoleRQ_Next);
                    header_ = header_.Replace("RoleRQ_AprV", RoleAppRQ_Next);
                    header_ = header_.Replace("RoleStoreV", RoleStore_Next);
                    header_ = header_.Replace("RoleStore_AprV", RoleAppStore_Next);

                    objMessage.Body = header_;
                    objMessage.To.Add(new MailAddress(Email_NextUser));
                    objClient.Send(objMessage);

                }
            }

        }
        public void SendEmail_Prv(string RequestID, string DeptID, string user_Current, string subject, string ToEmail, string Content_Comment)
        {

            //1 Get Role_Next , User_Next, RoleDept_Next
            Public_Dept = Session["CostCenter"].ToString().Trim();
            string UserNext = null; string RoleNext = null; string RoleDeptNext = null;
            if (Session["RoleOutStock"].ToString().Trim() != "" && Session["Role_Aproved_Stock"].ToString().Trim() != "") 
            {
                dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), Session["RoleOutStock"].ToString().Trim(), Session["Role_Aproved_Dept"].ToString().Trim(), RequestID);
            }
            if (Session["Role_Dept"].ToString().Trim() != "" && Session["Role_Aproved_Dept"].ToString().Trim() != "") 
            {
                dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), Session["Role_Dept"].ToString().Trim(), Session["Role_Aproved_Dept"].ToString().Trim(), RequestID);
            }

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

            string RQID = RequestID.ToString().Trim();
            string UserNext_ = UserNext.ToString().Trim();
            string Kho = hdfStock.Value.ToString();
            string RoleDept = Session["Role_Dept"].ToString().Trim();
            string RoleApproved = Session["Role_Aproved_Dept"].ToString().Trim();
            string RoleOutStock = Session["RoleOutStock"].ToString().Trim();
            string RoleApproved_Stock = Session["Role_Aproved_Stock"].ToString().Trim();
            //header_ = header_.Replace("RQV", DataConn.Encrypt(RQID));
            //header_ = header_.Replace("UserV", DataConn.Encrypt(UserNext_));
            //header_ = header_.Replace("DeptV", DataConn.Encrypt(Public_Dept));
            //header_ = header_.Replace("StockV", DataConn.Encrypt(Kho));
            //header_ = header_.Replace("value5", DataConn.Encrypt(RoleDept));
            //header_ = header_.Replace("RoleRQ_AprV", DataConn.Encrypt(RoleApproved));
            //header_ = header_.Replace("RoleStoreV", DataConn.Encrypt(RoleOutStock));
            //header_ = header_.Replace("RoleStore_AprV", DataConn.Encrypt(RoleApproved_Stock));

            header_ = header_.Replace("RQV", RQID);
            header_ = header_.Replace("UserV", UserNext_);
            header_ = header_.Replace("DeptV", Public_Dept);
            header_ = header_.Replace("StockV", Kho);
            header_ = header_.Replace("value5", RoleDept);
            header_ = header_.Replace("RoleRQ_AprV", RoleApproved);
            header_ = header_.Replace("RoleStoreV", RoleOutStock);
            header_ = header_.Replace("RoleStore_AprV", RoleApproved_Stock);

            objMessage.Body = header_;
            foreach (string Email_To in ToEmailList)
            {
                objMessage.To.Add(new MailAddress(Email_To));
                objClient.Send(objMessage);
            }
        }
        public void Loadstatus(string RequestNo)
        {
            Public_Dept = Session["CostCenter"].ToString().Trim();
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
        public void LoadButton_IN(string RequestNo, string Role, string RoleIN)
        {

            Public_Dept = Session["CostCenter"].ToString().Trim();
            string Issue_Incharge = "", Issue_MGR = "", Issue_GM = "", ACC_Check = "", ACC_MGR = "", ACC_GM = "", Out_MGR = "", Out_Incharge = "", Out_GM = "";
            bttReset.Visible = false;
            bttUpload.Visible = false;
            FileUpload1.Visible = false;
            bttTempFile.Visible = false;
            bttDelete.Visible = false;

            if (RequestNo == "" && RoleIN.ToString().Trim() == "RQ")
            {

                if (int.Parse(Role) == 1)
                {
                    bttApproved.Text = "Sent Report";
                    bttReject.Visible = false;
                    bttApproved.Enabled = true;
                    bttTempFile.Visible = true;
                    bttReset.Visible = false;
                    bttDelete.Visible = false;
                    bttUpload.Visible = true;
                    FileUpload1.Visible = true;


                }
                if (int.Parse(Role) == 2)
                {
                    bttApproved.Text = "Check Request";
                    bttReject.Enabled = true;
                    bttApproved.Enabled = true;

                    bttReset.Visible = false;
                    bttDelete.Visible = false;
                }
                if (int.Parse(Role) == 3)
                {
                    bttApproved.Text = "Approved";
                    bttReject.Enabled = true;
                    bttApproved.Enabled = true;
                    bttDelete.Visible = false;
                    bttReset.Visible = false;
                }

            }
            if (RequestNo != "" && (RoleIN.ToString().Trim() == "RQ" || RoleIN.ToString().Trim() == "ACC-CHECK"))
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
                if ((int.Parse(Role) == 1) && (RoleIN.ToString().Trim() == "RQ"))// Show status button permistion Requester
                {
                    if ((Issue_MGR == "NG") || (Issue_GM == "NG") || (ACC_Check == "NG") || (ACC_MGR == "NG") || (Out_MGR == "NG") || (Out_Incharge == "NG") || (Out_GM == "NG"))
                    {
                        bttReset.Visible = true;
                        bttUpload.Visible = true;
                        FileUpload1.Visible = true;
                        bttTempFile.Visible = true;
                    }
                    if ((Issue_MGR == "NG") || (Issue_GM == "NG") )
                    {
                        bttReset.Visible = true;
                        bttDelete.Visible = true;
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
                if (RoleIN == "RQ")
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
                            bttReject.Visible = false;
                        }
                        if (Issue_Incharge == "NG")
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
                            bttReject.Enabled = true;

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
                if (RoleIN == "ACC-CHECK")
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
                            bttApproved.Enabled = false;
                            bttReject.Enabled = false;
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

            }

        }

        public void LoadButton_Out(string RequestNo, string Role, string RoleOut)
        {

            Public_Dept = Session["CostCenter"].ToString().Trim();

            string Issue_Incharge = "", Issue_MGR = "", Issue_GM = "", ACC_Check = "", ACC_MGR = "", ACC_GM = "", Out_MGR = "", Out_Incharge = "", Out_GM = "";
            bttReset.Visible = false;
            bttUpload.Visible = false;
            FileUpload1.Visible = false;
            bttTempFile.Visible = false;
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
                if (RoleOut == "STORE")
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
        //public void SendEmail_PrvACC(string RequestID, string DeptID, string user_Current, string subject, string ToEmail, string Content_Comment)
        //{

        //    //1 Get Role_Next , User_Next, RoleDept_Next
        //    string UserNext = null; string RoleNext = null; string RoleDeptNext = null;
        //    dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), Session["Role_Dept"].ToString().Trim(), Session["Role_Aproved_Dept"].ToString().Trim(), RequestID);
        //    if (dtPreEmail.Rows.Count > 0)
        //    {
        //        UserNext = dtPreEmail.Rows[0]["UserLogin"].ToString();
        //        RoleNext = dtPreEmail.Rows[0]["RoleID"].ToString();
        //        RoleDeptNext = dtPreEmail.Rows[0]["RoleDept"].ToString();
        //    }
        //    System.Net.Mail.SmtpClient objClient = new System.Net.Mail.SmtpClient("157.8.1.131");
        //    subject = subject.Replace('\r', ' ').Replace('\n', ' ');
        //    System.Net.Mail.MailAddress mail = new System.Net.Mail.MailAddress("tax.psnv@vn.panasonic.com", "Issue In-Out Material");
        //    System.Net.Mail.MailMessage objMessage = new System.Net.Mail.MailMessage();
        //    string[] ToEmailList = ToEmail.Split(',');


        //    objMessage.Priority = MailPriority.High;
        //    objMessage.IsBodyHtml = true;
        //    objMessage.From = mail;
        //    objMessage.Subject = subject;
        //    objMessage.IsBodyHtml = true;
        //    //Get data Header Images to Content of Email
        //    string Header_Email = "";
        //    Header_Email += "";
        //    using (StreamReader reader2 = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Fomat_SendEmail/EmailApproved_B.html")))
        //    {
        //        header_ = reader2.ReadToEnd();
        //    }
        //    string RQID = REQUESTID.ToString().Trim();
        //    string UserNext_ = User_next.ToString().Trim();
        //    string Kho = hdfStock.Value.ToString();
        //    string RoleDept = Session["Role_Dept"].ToString().Trim();
        //    string RoleApproved = Session["Role_Aproved_Dept"].ToString().Trim();
        //    string RoleOutStock = Session["RoleOutStock"].ToString().Trim();
        //    string RoleApproved_Stock = Session["Role_Aproved_Stock"].ToString().Trim();
        //    //header_ = header_.Replace("RQV", DataConn.Encrypt(RQID));
        //    //header_ = header_.Replace("UserV", DataConn.Encrypt(UserNext_));
        //    //header_ = header_.Replace("DeptV", DataConn.Encrypt(Public_Dept));
        //    //header_ = header_.Replace("StockV", DataConn.Encrypt(Kho));
        //    //header_ = header_.Replace("RoleRQV", DataConn.Encrypt(RoleDept));
        //    //header_ = header_.Replace("RoleRQ_AprV", DataConn.Encrypt(RoleApproved));
        //    //header_ = header_.Replace("RoleStoreV", DataConn.Encrypt(RoleOutStock));
        //    //header_ = header_.Replace("RoleStore_AprV", DataConn.Encrypt(RoleApproved_Stock));

        //    header_ = header_.Replace("RQV", RQID);
        //    header_ = header_.Replace("UserV", UserNext_);
        //    header_ = header_.Replace("DeptV", Public_Dept);
        //    header_ = header_.Replace("StockV", Kho);
        //    header_ = header_.Replace("RoleRQV", RoleDept);
        //    header_ = header_.Replace("RoleRQ_AprV", RoleApproved);
        //    header_ = header_.Replace("RoleStoreV", RoleOutStock);
        //    header_ = header_.Replace("RoleStore_AprV", RoleApproved_Stock);

        //    objMessage.Body = header_;
        //    foreach (string Email_To in ToEmailList)
        //    {
        //        objMessage.To.Add(new MailAddress(Email_To));
        //        objClient.Send(objMessage);
        //    }
        //}
        protected void bttApproved_Click(object sender, EventArgs e)
        {
            string Request_NO = "";
            Public_Dept = Session["CostCenter"].ToString().Trim();
            ///////////////////////////////1. Lấy thông tin RQ cua Make RQ va STORE///////////////////////////////
            if (hdfControlRQ.Value.ToString().Trim() == "RQ" && Session["Role_Dept"].ToString().Trim() == "RQ")
            {

                if (treeRQ_InMaterial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {
                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString().Trim());
                          
                        }
                       

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
                    Request_NO = treeRQ_InMaterial.SelectedNode.Value.ToString();
                }

                //2
                dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), Session["Role_Dept"].ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
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

            }
            if (hdfControlACC.Value.ToString().Trim() == "ACC-CHECK" && Session["Role_Dept"].ToString().Trim() == "ACC-CHECK")
            {
                if (treeRQ_InMaterial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {

                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString().Trim());
                           
                        }
                       
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
                    Request_NO = treeRQ_InMaterial.SelectedNode.Value.ToString();
                }
                dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), Session["Role_Dept"].ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
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
            }
            if (hdfControlStore.Value.ToString() == "STORE" && Session["RoleOutStock"].ToString().Trim() == "STORE")
            {

                if (treeRQ_OutMateial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {
                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["RoleOutStock"].ToString().Trim());
                        }
                       
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
                    Request_NO = treeRQ_OutMateial.SelectedNode.Value.ToString();
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////
                dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfControlStore.Value.ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
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
            }

            /// 2 Approved
            if (Request_NO == "" || Request_NO == null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Choose RQ in list control RQ again.');", true);
                return;
            }
            else
            {

                DataTable dtStatus = DataConn.StoreFillDS("SP_Issue_Material_CheckStatus", CommandType.StoredProcedure, Request_NO);
                if (dtStatus.Rows.Count < 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Check Information of RequestNo.');", true);
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
                    if (hdfControlRQ.Value.ToString().Trim() == "RQ" && Session["Role_Dept"].ToString().Trim() == "RQ")
                    {
                        string RoleApproved = Session["Role_Aproved_Dept"].ToString();


                        if ((int.Parse(RoleApproved) == 1 && Issue_Incharge == "") || (int.Parse(RoleApproved) == 1 && Issue_Incharge == null))
                        {
                            int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfControlRQ.Value.ToString().Trim(), RoleApproved.ToString().Trim());
                            if (row == 0)
                            {
                                //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                string Subject = " [Issue Out] - Request Approval for " + Request_NO;
                                string Comment = txt_Comment.Value;
                                SendEmail_Next(Request_NO, Session["UserName"].ToString(), Public_Dept, hdfStock.Value.ToString(), hdfControlRQ.Value.ToString(), RoleApproved.ToString().Trim(), "", "", Subject, Comment);
                                Search(Request_NO, RoleApproved.ToString().Trim(), hdfControlRQ.Value.ToString().Trim());
                                LoadButton_IN(Request_NO, RoleApproved.ToString().Trim(), hdfControlRQ.Value.ToString().Trim());
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has submited sucessfully');", true);
                            }

                        }

                        if ((int.Parse(RoleApproved) == 2 && string.Equals(Issue_Incharge, "") == true) || (int.Parse(RoleApproved) == 2 && string.Equals(Issue_Incharge, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);

                            return;
                        }
                        else
                        {
                            if ((int.Parse(RoleApproved) == 2 && Issue_Incharge == "OK" && Issue_MGR == "") || (int.Parse(RoleApproved) == 2 && Issue_Incharge == "OK" && Issue_MGR == null))
                            {

                                int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfControlRQ.Value.ToString().Trim(), RoleApproved.ToString().Trim());
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                    string Subject = " [Issue Out] - Request Approval for " + Request_NO;
                                    string Comment = txt_Comment.Value;
                                    SendEmail_Next(Request_NO, Session["UserName"].ToString(), Public_Dept, hdfStock.Value.ToString(), hdfControlRQ.Value.ToString(), RoleApproved.ToString().Trim(), "", "", Subject, Comment);
                                    Search(Request_NO, RoleApproved.ToString().Trim(), hdfControlRQ.Value.ToString().Trim());
                                    LoadButton_IN(Request_NO, RoleApproved.ToString().Trim(), hdfControlRQ.Value.ToString().Trim());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has checked sucessfully');", true);

                                }

                            }

                        }


                        if ((int.Parse(RoleApproved) == 3 && string.Equals(Issue_MGR, "") == true) || (int.Parse(RoleApproved) == 3 && string.Equals(Issue_MGR, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);

                            return;

                        }

                        else
                        {
                            if ((int.Parse(RoleApproved) == 3 && Issue_MGR == "OK" && Issue_GM == "") || (int.Parse(RoleApproved) == 3 && Issue_MGR == "OK" && Issue_GM == null))
                            {
                                int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfControlRQ.Value.ToString().Trim(), RoleApproved.ToString().Trim());
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                    string Subject = " [Issue Out] - Request Approval for " + Request_NO;
                                    string Comment = txt_Comment.Value;
                                    SendEmail_Next(Request_NO, Session["UserName"].ToString(), Public_Dept, hdfStock.Value.ToString(), hdfControlRQ.Value.ToString(), RoleApproved.ToString().Trim(), "", "", Subject, Comment);
                                    Search(Request_NO, RoleApproved.ToString().Trim(), hdfControlRQ.Value.ToString().Trim());
                                    LoadButton_IN(Request_NO, RoleApproved.ToString().Trim(), hdfControlRQ.Value.ToString().Trim());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has approved sucessfully');", true);

                                }
                            }
                        }

                    }
                    ////////////////////////
                    if (hdfControlACC.Value.ToString().Trim() == "ACC-CHECK" && Session["Role_Dept"].ToString().Trim() == "ACC-CHECK")
                    {
                        string RoleApproved = Session["Role_Aproved_Dept"].ToString();
                        /////////////////////////////////////////////
                        if ((int.Parse(RoleApproved) == 1 && string.Equals(Issue_GM, "") == true) || (int.Parse(RoleApproved) == 1 && string.Equals(Issue_GM, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);


                            return;
                        }
                        else
                        {
                            if ((int.Parse(RoleApproved) == 1 && Issue_GM == "OK" && ACC_Check == "") || (int.Parse(RoleApproved) == 1 && Issue_GM == "OK" && ACC_Check == null))
                            {

                                int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfControlACC.Value.ToString().Trim(), RoleApproved.ToString().Trim());
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                    string Subject = " [Issue Out] - Request Approval for " + Request_NO;

                                    string Comment = txt_Comment.Value;
                                    SendEmail_Next(Request_NO, Session["UserName"].ToString(), Public_Dept, hdfStock.Value.ToString(), hdfControlACC.Value.ToString(), RoleApproved.ToString().Trim(), "", "", Subject, Comment);
                                    Search(Request_NO, RoleApproved.ToString().Trim(), hdfControlACC.Value.ToString().Trim());
                                    LoadButton_IN(Request_NO, RoleApproved.ToString().Trim(), hdfControlACC.Value.ToString().Trim());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has  ACC Checked sucessfully');", true);

                                }

                            }

                        }


                        if ((int.Parse(RoleApproved) == 2 && string.Equals(ACC_Check, "") == true) || (int.Parse(RoleApproved) == 2 && string.Equals(ACC_Check, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                            return;

                        }

                        else
                        {
                            if ((int.Parse(RoleApproved) == 2 && ACC_Check == "OK" && ACC_MGR == "") || (int.Parse(RoleApproved) == 2 && ACC_Check == "OK" && ACC_MGR == null))
                            {
                                int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfControlACC.Value.ToString().Trim(), RoleApproved.ToString().Trim());
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";

                                    string Subject = " [Issue Out] - Request Approval for " + Request_NO;
                                    string Comment = txt_Comment.Value;
                                    SendEmail_Next(Request_NO, Session["UserName"].ToString(), Public_Dept, hdfStock.Value.ToString(), hdfControlACC.Value.ToString(), RoleApproved.ToString().Trim(), "", "", Subject, Comment);
                                    Search(Request_NO, RoleApproved.ToString().Trim(), hdfControlACC.Value.ToString().Trim());
                                    LoadButton_IN(Request_NO, RoleApproved.ToString().Trim(), hdfControlACC.Value.ToString().Trim());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has   MGR'ACC checked  sucessfully');", true);

                                }
                            }
                        }

                        if ((int.Parse(RoleApproved) == 3 && string.Equals(ACC_MGR, "") == true) || (int.Parse(RoleApproved) == 3 && string.Equals(ACC_MGR, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                            return;

                        }

                        else
                        {
                            if ((int.Parse(RoleApproved) == 3 && ACC_MGR == "OK" && ACC_GM == "") || (int.Parse(RoleApproved) == 2 && ACC_MGR == "OK" && ACC_GM == null))
                            {
                                int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfControlACC.Value.ToString().Trim(), RoleApproved.ToString().Trim());
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                    string Subject = " [Issue Out] - Request Approval for " + Request_NO;
                                    string Comment = txt_Comment.Value;
                                    Search(Request_NO, RoleApproved.ToString().Trim(), hdfControlACC.Value.ToString().Trim());
                                    LoadButton_IN(Request_NO, RoleApproved.ToString().Trim(), hdfControlACC.Value.ToString().Trim());
                                    DataTable dtCheckDept = new DataTable();
                                    dtCheckDept = DataConn.StoreFillDS("SP_Issue_Material_CheckDeptPUR", CommandType.StoredProcedure, Request_NO);
                                    if (dtCheckDept.Rows[0]["RQDeptID"].ToString() == "PUR")// PHONG PUR THI TU DONG APROVED RQ PHAN OUTSTOCK
                                    {
                                        int rowPUR = DataConn.ExecuteStore("SP_Issue_Material_Approved_PUR", CommandType.StoredProcedure, Request_NO, hdfControlACC.Value.ToString().Trim(), RoleApproved.ToString().Trim());
                                        //  SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                        Loadstatus(Request_NO);
                                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has approved sucessfully');", true);

                                    }
                                    else
                                    {
                                        SendEmail_Next(Request_NO, Session["UserName"].ToString(), Public_Dept, hdfStock.Value.ToString(), hdfControlACC.Value.ToString(), RoleApproved.ToString().Trim(), "", "", Subject, Comment);
                                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has approved sucessfully');", true);
                                    }


                                }
                            }
                        }
                    }
                    if (hdfControlStore.Value.ToString().Trim() == "STORE" && Session["RoleOutStock"].ToString().Trim() == "STORE")
                    {
                        string RoleApproved = Session["Role_Aproved_Stock"].ToString().Trim();
                        if ((int.Parse(RoleApproved) == 1 && string.Equals(ACC_GM, "") == true) || (int.Parse(RoleApproved) == 1 && string.Equals(ACC_GM, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                            return;
                        }
                        else
                        {
                            if ((int.Parse(RoleApproved) == 1 && ACC_GM == "OK" && Out_Incharge == "") || (int.Parse(RoleApproved) == 1 && ACC_GM == "OK" && Out_Incharge == null))
                            {

                                int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfControlStore.Value.ToString().Trim(), RoleApproved.ToString().Trim());
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                    string Subject = " [Issue Out] - Request Approval for " + Request_NO;
                                    string Comment = txt_Comment.Value;
                                    SendEmail_Next(Request_NO, Session["UserName"].ToString(), Public_Dept, hdfStock.Value.ToString(), "", "", hdfControlStore.Value.ToString().Trim(), RoleApproved.ToString().Trim(), Subject, Comment);
                                    Search(Request_NO, RoleApproved.ToString().Trim(), hdfControlStore.Value.ToString().Trim());
                                    LoadButton_Out(Request_NO, RoleApproved.ToString().Trim(), hdfControlStore.Value.ToString().Trim());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has approved sucessfully');", true);

                                }

                            }

                        }


                        if ((int.Parse(RoleApproved) == 2 && string.Equals(Out_Incharge, "") == true) || (int.Parse(RoleApproved) == 2 && string.Equals(Out_Incharge, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);

                            return;

                        }

                        else
                        {
                            if ((int.Parse(RoleApproved) == 2 && Out_Incharge == "OK" && Out_MGR == "") || (int.Parse(RoleApproved) == 2 && Out_Incharge == "OK" && Out_MGR == null))
                            {
                                int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfControlStore.Value.ToString().Trim(), RoleApproved.ToString().Trim());
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                    string Subject = " [Issue Out] - Request Approval for " + Request_NO;
                                    string Comment = txt_Comment.Value;
                                    SendEmail_Next(Request_NO, Session["UserName"].ToString(), Public_Dept, hdfStock.Value.ToString(), "", "", hdfControlStore.Value.ToString().Trim(), RoleApproved.ToString().Trim(), Subject, Comment);
                                    Search(Request_NO, RoleApproved.ToString().Trim(), hdfControlStore.Value.ToString().Trim());
                                    LoadButton_Out(Request_NO, RoleApproved.ToString().Trim(), hdfControlStore.Value.ToString().Trim());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has Issue Out's MGR checked sucessfully');", true);

                                }
                            }
                        }
                        if ((int.Parse(RoleApproved) == 3 && string.Equals(Out_MGR, "") == true) || (int.Parse(RoleApproved) == 3 && string.Equals(Out_MGR, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);

                            return;

                        }

                        else
                        {
                            if ((int.Parse(RoleApproved) == 3 && Out_MGR == "OK" && Out_GM == "") || (int.Parse(RoleApproved) == 2 && Out_MGR == "OK" && Out_GM == null))
                            {
                                int row = DataConn.ExecuteStore("SP_Issue_Material_Approved", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfControlStore.Value.ToString().Trim(), RoleApproved.ToString().Trim());
                                if (row == 0)
                                {
                                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                                    string Subject = " [Issue Out] - Request Approval for " + Request_NO;
                                    string Comment = txt_Comment.Value;
                                    //SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                    Search(Request_NO, RoleApproved.ToString().Trim(), hdfControlStore.Value.ToString().Trim());
                                    LoadButton_Out(Request_NO, RoleApproved.ToString().Trim(), hdfControlStore.Value.ToString().Trim());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has  approved sucessfully');", true);
                                }
                            }
                        }

                    }
                }
            }
        }
        protected void bttSend_Click(object sender, EventArgs e)
        {
            Public_Dept = Session["CostCenter"].ToString().Trim();
            Request_NO = hdfRQ_UpdateComent.Value.ToString().Trim();
            string Role_RQ = hdfRoleRQ_UpdateComment.Value.ToString().Trim();
            string Role_ACC_CHECK = hdfRoleACC_UpdateComment.Value.ToString().Trim();
            string Role_STORE_CHECk = hdfRoleSTORE_UpdateComment.Value.ToString().Trim();

            if (Role_RQ == "" &&  Role_ACC_CHECK == "" && Role_STORE_CHECk == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.error('Please chosen RQ in list control');", true);

            }
            else
            {

                if (hdfControlRQ.Value.ToString().Trim() == "RQ" && Session["Role_Dept"].ToString().Trim() == "RQ")
                {
                    dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), Session["Role_Dept"].ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
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
                    if (hdfControlRQ.Value.ToString().Trim() == "RQ" && Session["Role_Aproved_Dept"].ToString() != null)
                    {
                        Role = Session["Role_Aproved_Dept"].ToString();
                        RoleDept = Role_RQ;
                    }

                    int row = DataConn.ExecuteStore("SP_Issue_Material_Comment_Update", CommandType.StoredProcedure, Request_NO, Comment, Session["UserName"].ToString(), RoleDept, Role);
                    if (row == 0)
                    {
                        string Subject = " Request for comment-" + Request_NO + ".";
                        dt_Comment = DataConn.FillStore("SP_Issue_Material_Comment_Load", CommandType.StoredProcedure, Request_NO);
                        if (RoleDept == "RQ")
                        {

                            if ((int.Parse(Role) == 1))
                            {
                                SendEmail_Next(Request_NO, Session["UserName"].ToString(), Public_Dept, hdfStock.Value.ToString(), hdfControlRQ.Value.ToString(), Role, "", "", Subject, Comment);
                            }
                            else
                            {
                                SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                            }
                        }
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This comment has been sent sucessfully');", true);
                    }
                    txt_Comment.Value = "";
                    Search(Request_NO, Role, RoleDept.Trim());
                    LoadButton_IN(Request_NO, Role, RoleDept);
                }
                if (hdfControlACC.Value.ToString().Trim() == "ACC-CHECK" && Session["Role_Dept"].ToString().Trim() == "ACC-CHECK")
                {
                    dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), Session["Role_Dept"].ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
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
                    if (hdfControlACC.Value.ToString().Trim() == "ACC-CHECK" && Session["Role_Dept"].ToString() != null)
                    {

                        Role = Session["Role_Aproved_Dept"].ToString();
                        RoleDept = Role_ACC_CHECK;
                    }

                    int row = DataConn.ExecuteStore("[SP_Issue_Material_Comment_Update]", CommandType.StoredProcedure, Request_NO, Comment, Session["UserName"].ToString(), RoleDept, Role);
                    if (row == 0)
                    {
                        //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                        string Subject = " Request for Comment " + Request_NO + ".";
                        dt_Comment = DataConn.FillStore("SP_Issue_Material_Comment_Load", CommandType.StoredProcedure, Request_NO);
                        SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This comment has been sent sucessfully');", true);
                    }
                    txt_Comment.Value = "";
                    Search(Request_NO, Role, Role_ACC_CHECK);
                    LoadButton_IN(Request_NO, Role, RoleDept);
                }
                if (hdfControlStore.Value.ToString().Trim() == "STORE" && Session["RoleOutStock"].ToString().Trim() == "STORE")
                {
                    dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfControlStore.Value.ToString().Trim(), Session["Role_Aproved_Stock"].ToString(), Request_NO);
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

                    if (hdfControlStore.Value.ToString().Trim() == "STORE" && Session["Role_Aproved_Stock"].ToString() != null)
                    {
                        Role = Session["Role_Aproved_Stock"].ToString();
                        RoleDept = Role_STORE_CHECk;
                    }


                    int row = DataConn.ExecuteStore("[SP_Issue_Material_Comment_Update]", CommandType.StoredProcedure, Request_NO, Comment, Session["UserName"].ToString(), RoleDept, Role);
                    if (row == 0)
                    {

                        //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                        string Subject = " [Issue Out] - Request Comment for " + Request_NO;
                        dt_Comment = DataConn.FillStore("SP_Issue_Material_Comment_Load", CommandType.StoredProcedure, Request_NO);
                        SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);

                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This comment has been sent sucessfully');", true);

                    }
                    txt_Comment.Value = "";
                    Search(Request_NO, Role, RoleDept);
                    LoadButton_Out(Request_NO, Role, RoleDept);
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////

                }
            }
        }
        protected void bttPrint_Click(object sender, EventArgs e)
        {
            Public_Dept = Session["CostCenter"].ToString().Trim();
            DataTable dt_ReportAll = new DataTable();
            DataTable dt_Status = new DataTable();
            string Request_NO = "";
          
            ///////////////////////////////1. Lấy thông tin RQ cua Make RQ va STORE///////////////////////////////
            
            if (hdfControlRQ.Value.ToString().Trim() == "RQ" && Session["Role_Dept"].ToString().Trim() == "RQ")
            {

                if (treeRQ_InMaterial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {
                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString().Trim());
                            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), Session["Role_Dept"].ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
                        }
                        else
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), hdfControlRQ.Value);
                            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfControlRQ.Value.ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
                        }

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
                    Request_NO = treeRQ_InMaterial.SelectedNode.Value.ToString();
                }

                //2


            }
            if (hdfControlACC.Value.ToString().Trim() == "ACC-CHECK" && Session["Role_Dept"].ToString().Trim() == "ACC-CHECK")
            {
                if (treeRQ_InMaterial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {

                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString().Trim());
                            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), Session["Role_Dept"].ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
                        }
                        else
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), hdfControlACC.Value);
                            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfControlACC.Value.ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
                        }
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
                    Request_NO = treeRQ_InMaterial.SelectedNode.Value.ToString();
                }
            }
            if (hdfControlStore.Value.ToString() == "STORE" && Session["RoleOutStock"].ToString().Trim() == "STORE")
            {

                if (treeRQ_OutMateial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {
                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["RoleOutStock"].ToString().Trim());
                            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), Session["RoleOutStock"].ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
                        }
                        else
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), hdfControlACC.Value);
                            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfControlStore.Value.ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
                        }
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
                    Request_NO = treeRQ_OutMateial.SelectedNode.Value.ToString();
                }
               
            }
            if(Session["RoleOutStock"].ToString().Trim() == "STORE" && Session["Role_Dept"].ToString().Trim() == "RQ")
            {
                dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString().Trim());
                Request_NO = dtTreeRQ.Rows[0]["RequestNo"].ToString();
            }

            string titleReport = "Report Issue IN-OUT Material";
            if (Public_Dept != "PUR") // Nếu không phải PUR thì không cần hiển thị phần out SAP
            {
                dt_ReportAll = DataConn.StoreFillDS("SP_Issue_Material_Report", CommandType.StoredProcedure, Request_NO);
                dt_Status = DataConn.StoreFillDS("SP_Issue_Material_RQStatus", CommandType.StoredProcedure, Request_NO);
                if (dt_ReportAll.Rows.Count > 0 && dt_Status.Rows.Count > 0)
                {
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
            }
            if (Public_Dept == "PUR") // Nếu PUR thì hiển thị phần out stock

            {
                dt_ReportAll = DataConn.StoreFillDS("SP_Issue_Material_Report_PUR", CommandType.StoredProcedure, Request_NO);
                dt_Status = DataConn.StoreFillDS("SP_Issue_Material_RQStatus_PUR", CommandType.StoredProcedure, Request_NO);
                if (dt_ReportAll.Rows.Count > 0 && dt_Status.Rows.Count > 0)
                {
                    PUR_Report  DataPDF = new PUR_Report(dt_ReportAll, dt_Status, titleReport);
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
        protected void bttReject_Click(object sender, EventArgs e)
        {
            Public_Dept = Session["CostCenter"].ToString().Trim();
           
            ///////////////////////////////1. Lấy thông tin RQ cua Make RQ va STORE///////////////////////////////
            if (hdfControlRQ.Value.ToString().Trim() == "RQ" && Session["Role_Dept"].ToString().Trim() == "RQ")
            {

                if (treeRQ_InMaterial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {
                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString().Trim());
                        }

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
                    Request_NO = treeRQ_InMaterial.SelectedNode.Value.ToString();
                }

                //2
                dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), Session["Role_Dept"].ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
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
            }
            if (hdfControlACC.Value.ToString().Trim() == "ACC-CHECK" && Session["Role_Dept"].ToString().Trim() == "ACC-CHECK")
            {
                if (treeRQ_InMaterial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {

                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString().Trim());
                        }
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
                    Request_NO = treeRQ_InMaterial.SelectedNode.Value.ToString();
                }
                dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), Session["Role_Dept"].ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
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
            }
            if (hdfControlStore.Value.ToString() == "STORE" && Session["RoleOutStock"].ToString().Trim() == "STORE")
            {

                if (treeRQ_OutMateial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {
                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["RoleOutStock"].ToString().Trim());
                        }
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
                    Request_NO = treeRQ_OutMateial.SelectedNode.Value.ToString();
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////
                dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfControlStore.Value.ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
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
            }


            if (Request_NO == "" || Request_NO == null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Choose RQ in list control RQ again.');", true);
                return;
            }
            else
            {
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



                    if (hdfControlRQ.Value.ToString().Trim() == "RQ" && Session["Role_Dept"].ToString().Trim() == "RQ")
                    {
                        string Role = Session["Role_Aproved_Dept"].ToString().Trim();
                        string Role_RQ = Session["Role_Dept"].ToString().Trim();
                        if ((int.Parse(Role) == 2 && string.Equals(Issue_Incharge, "") == true) || (int.Parse(Role) == 2 && string.Equals(Issue_Incharge, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);

                            return;
                        }
                        else
                        {
                            if ((int.Parse(Role) == 2 && Issue_Incharge == "OK" && Issue_MGR == "") || (int.Parse(Role) == 2 && Issue_Incharge == "OK" && Issue_MGR == null))
                            {

                                int row = DataConn.ExecuteStore("[SP_Issue_Material_Reject]", CommandType.StoredProcedure, Session["UserName"], Request_NO, Role_RQ, Role);
                                if (row == 0)
                                {

                                    string Subject = " [Issue Out] - Request Reject for " + Request_NO;
                                    string Comment = txt_Comment.Value;
                                    SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                    Search(Request_NO, Session["Role_Aproved_Dept"].ToString().Trim(), hdfControlRQ.Value.ToString().Trim());
                                    LoadButton_IN(Request_NO, Session["Role_Aproved_Dept"].ToString().Trim(), hdfControlRQ.Value.ToString().Trim());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);


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
                                int row = DataConn.ExecuteStore("SP_Issue_Material_Reject", CommandType.StoredProcedure, Session["UserName"], Request_NO, Role_RQ, Role);
                                if (row == 0)
                                {


                                    string Subject = " [Issue Out] - Request Reject for " + Request_NO;
                                    string Comment = txt_Comment.Value;
                                    Search(Request_NO, Role, hdfControlRQ.Value.ToString().Trim());
                                    LoadButton_IN(Request_NO, Role, hdfControlRQ.Value.ToString().Trim());
                                    SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);

                                }
                            }
                        }

                    }
                    if (hdfControlACC.Value.ToString().Trim() == "ACC-CHECK" && Session["Role_Dept"].ToString().Trim() == "ACC-CHECK")

                    {
                        string Role = Session["Role_Aproved_Dept"].ToString().Trim();
                        string Role_RQ = Session["Role_Dept"].ToString().Trim();
                        if ((int.Parse(Role) == 1 && string.Equals(Issue_GM, "") == true) || (int.Parse(Role) == 2 && string.Equals(Issue_GM, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);

                            return;
                        }
                        else
                        {
                            if ((int.Parse(Role) == 1 && Issue_GM == "OK" && ACC_Check == "") || (int.Parse(Role) == 1 && Issue_GM == "OK" && ACC_Check == null))
                            {

                                int row = DataConn.ExecuteStore("SP_Issue_Material_Reject", CommandType.StoredProcedure, Session["UserName"], Request_NO, Role_RQ, Role);
                                if (row == 0)
                                {


                                    string Subject = " [Issue Out] - Request Reject for " + Request_NO;
                                    string Comment = txt_Comment.Value;


                                    SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);
                                    Search(Request_NO, Role, hdfControlACC.Value.ToString().Trim());
                                    LoadButton_IN(Request_NO, Role, hdfControlACC.Value.ToString().Trim());
                                }

                            }

                        }


                        if ((int.Parse(Role) == 2 && string.Equals(ACC_Check, "") == true) || (int.Parse(Role) == 2 && string.Equals(ACC_Check, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                            return;
                        }

                        else
                        {
                            if ((int.Parse(Role) == 2 && ACC_Check == "OK" && ACC_MGR == "") || (int.Parse(Role) == 2 && ACC_Check == "OK" && ACC_MGR == null))
                            {
                                int row = DataConn.ExecuteStore("SP_Issue_Material_Reject", CommandType.StoredProcedure, Session["UserName"], Request_NO, Role_RQ, Role);
                                if (row == 0)
                                {


                                    string Subject = " [Issue Out] - Request Reject for " + Request_NO;
                                    string Comment = txt_Comment.Value;
                                    SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                    Search(Request_NO, Role, hdfControlACC.Value.ToString().Trim());
                                    LoadButton_IN(Request_NO, Role, hdfControlACC.Value.ToString().Trim());
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);
                                }
                            }
                        }
                        if ((int.Parse(Role) == 3 && string.Equals(ACC_MGR, "") == true) || (int.Parse(Role) == 3 && string.Equals(ACC_MGR, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                            return;
                        }

                        else
                        {
                            if ((int.Parse(Role) == 3 && ACC_MGR == "OK" && ACC_GM == "") || (int.Parse(Role) == 2 && ACC_MGR == "OK" && ACC_GM == null))
                            {
                                int row = DataConn.ExecuteStore("SP_Issue_Material_Reject", CommandType.StoredProcedure, Session["UserName"], Request_NO, Role_RQ, Role);
                                if (row == 0)
                                {

                                    string Subject = " [Issue Out] - Request Reject for " + Request_NO;
                                    string Comment = txt_Comment.Value;
                                    Search(Request_NO, Role, hdfControlACC.Value.ToString().Trim());
                                    LoadButton_IN(Request_NO, Role, hdfControlACC.Value.ToString().Trim());
                                    SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);

                                }
                            }
                        }
                    }
                    if (hdfControlStore.Value.ToString().Trim() == "STORE" && Session["RoleOutStock"].ToString().Trim() == "STORE")
                    {
                        string Role = Session["Role_Aproved_Stock"].ToString().Trim();
                        string Role_RQ = Session["RoleOutStock"].ToString().Trim();
                        if ((int.Parse(Role) == 1 && string.Equals(ACC_GM, "") == true) || (int.Parse(Role) == 1 && string.Equals(ACC_GM, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);

                            return;
                        }
                        else
                        {
                            if ((int.Parse(Role) == 1 && ACC_GM == "OK" && Out_Incharge == "") || (int.Parse(Role) == 1 && ACC_GM == "OK" && Out_Incharge == null))
                            {

                                int row = DataConn.ExecuteStore("SP_Issue_Material_Reject", CommandType.StoredProcedure, Session["UserName"], Request_NO, Role_RQ, Role);
                                if (row == 0)
                                {

                                    string Subject = " [Issue Out] - Request Reject for " + Request_NO;
                                    string Comment = txt_Comment.Value;
                                    Search(Request_NO, Role, hdfControlStore.Value.ToString().Trim());
                                    LoadButton_Out(Request_NO, Role, hdfControlStore.Value.ToString().Trim());
                                    SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);

                                }

                            }

                        }


                        if ((int.Parse(Role) == 2 && string.Equals(Out_Incharge, "") == true) || (int.Parse(Role) == 2 && string.Equals(Out_Incharge, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                            return;
                        }

                        else
                        {
                            if ((int.Parse(Role) == 2 && Out_Incharge == "OK" && Out_MGR == "") || (int.Parse(Role) == 2 && Out_Incharge == "OK" && Out_MGR == null))
                            {
                                int row = DataConn.ExecuteStore("SP_Issue_Material_Reject", CommandType.StoredProcedure, Session["UserName"], Request_NO, Role_RQ, Role);
                                if (row == 0)
                                {

                                    string Subject = " [Issue Out] - Request Reject for " + Request_NO;
                                    string Comment = txt_Comment.Value;
                                    Search(Request_NO, Role, hdfControlStore.Value.ToString().Trim());
                                    LoadButton_Out(Request_NO, Role, hdfControlStore.Value.ToString().Trim());
                                    SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);
                                }
                            }
                        }
                        if ((int.Parse(Role) == 3 && string.Equals(Out_MGR, "") == true) || (int.Parse(Role) == 3 && string.Equals(Out_MGR, null) == true))
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Request(OK) -> Check(OK) -> Approve(OK)');", true);
                            return;
                        }

                        else
                        {
                            if ((int.Parse(Role) == 3 && Out_MGR == "OK" && Out_GM == "") || (int.Parse(Role) == 2 && Out_MGR == "OK" && Out_GM == null))
                            {
                                int row = DataConn.ExecuteStore("SP_Issue_Material_Reject", CommandType.StoredProcedure, Session["UserName"], Request_NO, hdfStock.Value.ToString());
                                if (row == 0)
                                {
                                    string Subject = " [Issue Out] - Request Reject for " + Request_NO;
                                    string Comment = txt_Comment.Value;
                                    Search(Request_NO, Role, hdfControlStore.Value.ToString().Trim());
                                    LoadButton_Out(Request_NO, Role, hdfControlStore.Value.ToString().Trim());
                                    SendEmail_Prv(Request_NO, Public_Dept, Session["UserName"].ToString(), Subject, Email_Pres, Comment);
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has been reject sucessfully');", true);
                                }
                            }
                        }
                    }
                }
            }
        }
        protected void bttTempFile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Template/UploadMaterInOut_A.xlsx");
        }
        protected void bttReset_Click(object sender, EventArgs e)
        {
            Public_Dept = Session["CostCenter"].ToString().Trim();
            /// 2 Approved

            if (Session["Role_Aproved_Dept"].ToString().Trim() == "1" && Session["Role_Dept"].ToString().Trim() == "RQ")
            {

                if (treeRQ_InMaterial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {
                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString().Trim());
                        }
                    }
                    else
                    {
                        Request_NO = Session["RequestID_1RQ"].ToString();
                    }
                }
                else
                {
                    Request_NO = treeRQ_InMaterial.SelectedNode.Value.ToString();
                }
                if (Request_NO == "")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Chosen RQ need to approved in list RQ.');", true);
                    return;
                }
                else
                {
                    if (Request_NO == "" || Request_NO == null)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Choose RQ in list control RQ again.');", true);
                        return;
                    }
                    else
                    {

                        int row = DataConn.ExecuteStore("[SP_Issue_Material_Reset]", CommandType.StoredProcedure, Request_NO);
                        if (row == 0)
                        {
                            //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                            string Subject = "[Issue out] - RESET-(RQ:" + Request_NO + ") has been  successfully";
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has reset sucessfully');", true);
                            // Kiem tra thuoc Role nao thi Reset cua Incharde makeRQ
                            Search(Request_NO, Session["Role_Aproved_Dept"].ToString().Trim(), Session["Role_Dept"].ToString().Trim());
                            LoadButton_IN(Request_NO, Session["Role_Aproved_Dept"].ToString().Trim(), hdfControlRQ.Value.ToString().Trim());

                        }
                    }
                }
            }
            
        }

        protected void treeRQ_InMaterial_SelectedNodeChanged(object sender, EventArgs e)
        {
          
            if (Session["Role_Dept"].ToString().Trim() == "ACC-CHECK")
            {
                hdfControlACC.Value = "ACC-CHECK";
            }
            if (Session["Role_Dept"].ToString().Trim() == "RQ")
            {
                hdfControlRQ.Value = "RQ";
            }
            Search(treeRQ_InMaterial.SelectedNode.Value.ToString(), Session["Role_Aproved_Dept"].ToString().Trim(), Session["Role_Dept"].ToString().Trim());
            LoadButton_IN(treeRQ_InMaterial.SelectedNode.Value.ToString(), Session["Role_Aproved_Dept"].ToString().Trim(), Session["Role_Dept"].ToString().Trim());

        }

        protected void treeRQ_OutMateial_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (Session["RoleOutStock"].ToString().Trim() == "STORE")
            {
                hdfControlStore.Value = "STORE";
            }
            //hdftreeview.Value = treeRQ_OutMateial.SelectedNode.Value.ToString();
            Search(treeRQ_OutMateial.SelectedNode.Value.ToString(), Session["Role_Aproved_Stock"].ToString().Trim(), Session["RoleOutStock"].ToString().Trim());
            LoadButton_Out(treeRQ_OutMateial.SelectedNode.Value.ToString(), Session["Role_Aproved_Stock"].ToString().Trim(), Session["RoleOutStock"].ToString().Trim());

        }

        protected void bttDownExcel_Click(object sender, EventArgs e)
        {
            string Request_NO = "";
            Public_Dept = Session["CostCenter"].ToString().Trim();

            ///////////////////////////////1. Lấy thông tin RQ cua Make RQ va STORE///////////////////////////////

            if (hdfControlRQ.Value.ToString().Trim() == "RQ" && Session["Role_Dept"].ToString().Trim() == "RQ")
            {
                if (treeRQ_InMaterial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {
                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString().Trim());
                            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), Session["Role_Dept"].ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
                        }
                        else
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), hdfControlRQ.Value);
                            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfControlRQ.Value.ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
                        }

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
                    Request_NO = treeRQ_InMaterial.SelectedNode.Value.ToString();
                }

                //2


            }
            if (hdfControlACC.Value.ToString().Trim() == "ACC-CHECK" && Session["Role_Dept"].ToString().Trim() == "ACC-CHECK")
            {
                if (treeRQ_InMaterial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {

                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString().Trim());
                            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), Session["Role_Dept"].ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
                        }
                        else
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), hdfControlACC.Value);
                            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfControlACC.Value.ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
                        }
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
                    Request_NO = treeRQ_InMaterial.SelectedNode.Value.ToString();
                }
            }
            if (hdfControlStore.Value.ToString() == "STORE" && Session["RoleOutStock"].ToString().Trim() == "STORE")
            {

                if (treeRQ_OutMateial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {
                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["RoleOutStock"].ToString().Trim());
                            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), Session["RoleOutStock"].ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
                        }
                        else
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), hdfControlACC.Value);
                            dtPreEmail = DataConn.StoreFillDS("SP_BindPreviewtUser", CommandType.StoredProcedure, Session["UserName"].ToString(), hdfControlStore.Value.ToString().Trim(), Session["Role_Aproved_Dept"].ToString(), Request_NO);
                        }
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
                    Request_NO = treeRQ_OutMateial.SelectedNode.Value.ToString();
                }

            }
            if (Session["RoleOutStock"].ToString().Trim() == "STORE" && Session["Role_Dept"].ToString().Trim() == "RQ")
            {
                dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString().Trim());
                Request_NO = dtTreeRQ.Rows[0]["RequestNo"].ToString();
            }

            DataTable dt_ExportExcel = new DataTable();
            dt_ExportExcel = DataConn.FillStore("SP_Issue_Material_Report", CommandType.StoredProcedure, Request_NO);

            if (dt_ExportExcel.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt_ExportExcel, "InOut_FormA");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Download_InOut_FormA.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }

            }
        }

        protected void bttDelete_Click(object sender, EventArgs e)
        {
            Public_Dept = Session["CostCenter"].ToString().Trim();
            if (Request_NO == "" || Request_NO == null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.warning('Choose RQ in list control RQ again.');", true);
                return;
            }
            else
            
            { 
           if (Session["Role_Aproved_Dept"].ToString().Trim() == "1" && Session["Role_Dept"].ToString().Trim() == "RQ")
            {

                if (treeRQ_InMaterial.SelectedNode == null)
                {
                    if (string.IsNullOrEmpty((string)Session["RequestID_1RQ"]))
                    {
                        if (hdfControlRQ.Value == "")
                        {
                            dtTreeRQ = DataConn.StoreFillDS("SP_Issue_Material_RQMAX", CommandType.StoredProcedure, Public_Dept, Session["Stock"].ToString(), Session["Role_Dept"].ToString().Trim());
                        }
                    }
                    else
                    {
                        Request_NO = Session["RequestID_1RQ"].ToString();
                    }
                }
                else
                {
                    Request_NO = treeRQ_InMaterial.SelectedNode.Value.ToString();
                }
                int row = DataConn.ExecuteStore("SP_Issue_Material_Delete", CommandType.StoredProcedure, Request_NO);
                if (row == 0)
                {
                    //  string Subject = "SUBMITED REQUEST- SCM REQUEST ON PRICE TRANSACTION SYS";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "toastr.success('This request has delete sucessfully');", true);

                    Load_Treeview_Management();
                    LoadData();

                }
            }
            }
        }
    }
}