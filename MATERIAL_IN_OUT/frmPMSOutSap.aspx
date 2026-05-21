<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPMSOutSap.aspx.cs" Inherits="MATERIAL_IN_OUT.frmPMSOutSap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PMS Out SAP</title>
    <!-- Bootstrap 4 -->
    <link rel="stylesheet" href="/LibNew/bootstrap.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="/LibNew/all.min.css">
    <!-- AdminLTE -->
    <link rel="stylesheet" href="/LibNew/adminlte.min.css">
    <!-- DataTables -->
    <link rel="stylesheet" href="/LibNew/dataTables.bootstrap4.min.css">
    <link rel="stylesheet" href="/LibNew/responsive.bootstrap4.min.css">
    <!-- jQuery UI -->
    <link rel="stylesheet" href="/LibNew/jquery-ui.css">
    <!-- Toastr -->
    <link rel="stylesheet" href="/LibNew/toastr.min.css">



    <!-- jQuery -->
    <script src="/LibNew/jquery-3.6.4.min.js"></script>
    <!-- jQuery UI -->
    <script src="/LibNew/jquery-ui.min.js"></script>
    <!-- Bootstrap 4 -->
    <script src="/LibNew/bootstrap.bundle.min.js"></script>
    <!-- DataTables -->
    <script src="/LibNew/jquery.dataTables.min.js"></script>
    <script src="/LibNew/dataTables.bootstrap4.min.js"></script>
    <script src="/LibNew/dataTables.responsive.min.js"></script>
    <script src="/LibNew/responsive.bootstrap4.min.js"></script>

    <!-- AdminLTE -->
    <script src="/LibNew/adminlte.min.js"></script>

    <!-- Toastr -->
    <script src="/LibNew/toastr.min.js"></script>

    <style>
        .btn-out201 { background-color: #78fafa; color: #000000; }
        .btn-scrap  { background-color: #fbfcd9; color: #000000; }
        .btn-other  { background-color: #e6fafb; color: #000000; }
        .btn-sub    { background-color: #fbe2e2; color: #000000; }

        .row-sub    td { background-color: #fbe2e2 !important; color: #000000; }
        .row-other  td { background-color: #e6fafb !important; color: #000000; }
        .row-scrap  td { background-color: #fbfcd9 !important; color: #000000; }
        .row-sloc98 td { background-color: #78fafa !important; color: #000000; }

        /*th:first-child,
        td:first-child {
            text-align: center;
            width: 40px;
        }*/
    </style>

</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        <div class="card">
            <div class="card-header">
                <div class="col-sm-12">
                    <h3><b style="font-size: 30px;">List Issue In-Out PMS Out SAP</b></h3>
 
<div class="horizontal-radio-group">
    <b>Type Issue out Dept:</b> &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:RadioButton ID="rbPMS" runat="server" GroupName="rblOptions" Text="PMS" Checked="true" AutoPostBack="true" OnCheckedChanged="Radio_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:RadioButton ID="rbMCS" runat="server" GroupName="rblOptions" Text="MCS" AutoPostBack="true" OnCheckedChanged="Radio_CheckedChanged"  /> &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:RadioButton ID="rbACC" runat="server" GroupName="rblOptions" Text="ACC" AutoPostBack="true" OnCheckedChanged="Radio_CheckedChanged"  />   
</div>
                    <p style="color: blue;">
                        <asp:Label ID="lblConfirm" Text="" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="col-sm-12">
                    <div style="float: left; padding-right: 10px;">
                        From date:
                       <%--<input type="text" id="datepicker" runat="server">--%>
                        <input type="date" id="Date1" name="date" runat="server" />
                        To date:                                    
                       <input type="date" id="ngaychiid" name="date" runat="server" />
                    </div>


                    <div style="float: left; padding-right: 10px;">
                       <%-- RequestNO:--%>
                        <input type="text" id="filterRequestNo" runat="server" placeholder="Input Request No" style="height: 34px;" />
                    </div>
                    <div style="float: left; padding-right: 10px;">
                       <%-- Sanction:--%>
                        <input type="text" id="filterSanctionNo" runat="server" placeholder="Input Sanction No" style="height: 34px;" />
                    </div>

                     <div style="float: left; padding-right: 10px;">
                         <%--TypeSapPMS:--%>
                         <input type="text" id="filterSapPMS" runat="server" placeholder="Input type sapPMS" style="height: 34px;" />
                     </div>

                    <div class="col-md-1" style="float: left;padding-right: 10px;">
                        <div class="form-group">                        
                           <asp:DropDownList ID="dr_filter_section" runat="server"
                               AppendDataBoundItems="true"
                               DataTextField="DeptCode"
                               DataValueField="DeptCode"
                               CssClass="custom-select custom-select-sm form-control form-control-sm" Style="height:39px;" OnSelectedIndexChanged="dr_filter_section_SelectedIndexChanged" AutoPostBack="True" />
       
                       </div>
                    </div>

                     <div style="float: left; padding-right: 10px;">
                                   <b>Status Out Dept:</b> 
                                    <asp:DropDownList ID="ddlStatus" runat="server" style="height: 34px;">
                        <asp:ListItem Text="ALL" Value="ALL" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                        </asp:DropDownList>
                     </div>

                    <div style="float: left; padding-right: 10px;">
                        <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click">
                            Filter
                        </button>
                    </div>

                    <!-- Checkbox + Download All -->
                    <div style="float: left; padding-right: 10px; display: flex; align-items: center; gap: 8px;">
                        <asp:HiddenField ID="hfSelectedRequest" runat="server" />

                        <%--<button class="btn btn-primary" type="button" runat="server" onserverclick="Dowload_All_Click" onclick="return collectCheckedRows();">
                            Export
                        </button>--%>

                        <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary" Text="Export" OnClientClick="return collectCheckedRows();" OnClick="Dowload_All_Click" />
                        
                        <label style="margin: 0; display: flex; align-items: center; gap: 4px; height: 34px; cursor: pointer;">
                            <asp:RadioButton ID="optDownloadDetail" runat="server" GroupName="downloadOption" Checked="true" />
                            Format pending_done list
                        </label>

                        <label style="margin: 0; display: flex; align-items: center; gap: 4px; height: 34px; cursor: pointer;">
                            <asp:RadioButton ID="optDownloadTongIssueOut" runat="server" GroupName="downloadOption" />
                            CSV format tổng(issue out)
                        </label>
                        <label style="margin: 0; display: flex; align-items: center; gap: 4px; height: 34px; cursor: pointer;">
                            <asp:RadioButton ID="optDownloadTong" runat="server" GroupName="downloadOption" />
                            CSV format tổng (Transfer)
                        </label>
                        <label style="margin: 0; display: flex; align-items: center; gap: 4px; height: 34px; cursor: pointer;">
                            <asp:RadioButton ID="optReportLog" runat="server" GroupName="downloadOption" />
                            Report LOG
                        </label>
                    
                                         


                    </div>
                    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
             
                 <!-- import file excel -->
                    <!-- ADD A FILE UPLOAD CONTROL AND A BUTTON TO EXECUTE. -->
                    <div style="font: 14px Verdana; float: right">
                        <p style="margin-top: 0px; margin-left: 20px;">
                            Chose file to upload All Document No:
        <asp:FileUpload ID="FileUpload" Width="450px" runat="server" />
                        </p>
                        <p style="margin-top: 0px; margin-left: 20px;">
                            <input type="button" value="Upload document no" runat="server" onserverclick="ImportFromExcel" class="btn btn-primary" />

                           

         &nbsp;&nbsp;&nbsp;
                             <button class="btn btn-primary" type="button" runat="server" onserverclick="Dowload_All_Sub">
     Export_sub
 </button>
                            <button type="button" class="btn btn-primary float-right" style="margin-right: 5px;" runat="server" onserverclick="Dowloadtemplate">
                            Download template
                            </button>
                        </p>
                        <p>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </p>

                    </div>


                </div>
                
              

            </div>

        </div>


        <div>
            <table id="example" class="table table-striped table-bordered" style="width: 100%">
                <thead>

                    <tr role="row">
                        <!-- Checkbox all -->
                <th>
                    <input type="checkbox" id="checkAll" />
                </th>

                        <th>IDNO</th>
                        <th>RequestNo</th>
                        <th>Department</th>
                        <%--<th>Status_RQ</th>--%>
                        <th>Status_Approved_OutSAP</th>
                        <th>TypeForm</th>
                        <th>Sanction Name</th>
                        <th>Document No</th>

                        <th>TypeSapPMS</th>
                        <th>UserCreate</th>
                        <th>TypeID</th>
                        <th>Commentlog</th>
                        <th>Linkfile</th>
                        <th>Comment_PMS</th>
                        <th>Status Tranfer SAP</th>
                        <th>Status_Finish_PMS</th>

                        <th>Actions</th>

                    </tr>

                </thead>
                <tbody>
                    <% int i = 0; %>
                    <% foreach (System.Data.DataRow rows in dt.Rows)
                        { %>
                    <% i++; %>
                    <%
                        string type = rows["TypeSapPMS"]?.ToString();
                        string rowClass = "";

                        if (type == "Sub") rowClass = "row-sub";
                        else if (type == "Other") rowClass = "row-other";
                        else if (type == "Scrap") rowClass = "row-scrap";
                        else if (type == "Sloc98") rowClass = "row-sloc98";
                    %>
                    <tr class="<%= rowClass %>">
                        <!-- Checkbox từng dòng -->
                        <td> <input type="checkbox" class="row-check" value="<%= rows["RequestNo"] %>" /> </td>

                        <td><%= i %></td>
                        <td><%= rows["RequestNo"].ToString() %></td>
                        <td><%= rows["Department"].ToString() %></td>
                        <%--<td><%= rows["Status_RQ"].ToString() %></td>--%>
                        <td>
                            <%--<%= rows["Status_Approved_OutSAP"].ToString() %>--%>
                             <%= string.IsNullOrEmpty(rows["Status_Approved_OutSAP"]?.ToString()) 
                            ? "pending" 
                            : rows["Status_Approved_OutSAP"].ToString() %>
                        </td>
                        <td><%= rows["TypeForm"].ToString() %></td>
                        <td><%= rows["SanctionName"].ToString() %></td>
                        <td><%= rows["DocumentNo"].ToString() %></td>

                        <td><%= rows["TypeSapPMS"].ToString() %></td>
                        <td><%= rows["UserCreate"].ToString() %></td>
                        <td><%= rows["TypeID"].ToString() %></td>
                        <td><%= rows["commentlog"].ToString() %></td>
                        <td>
                            <% if (!string.IsNullOrEmpty(rows["Linkfile"]?.ToString())) { %>
                                <a href="javascript:void(0);" onclick="openPathModal('<%= rows["Linkfile"].ToString().Replace(@"\", "\\\\") %>')">Path</a>
                            <% } %>
                        </td>
                        <td><%= rows["CommentPMS"].ToString() %></td>
                        <td><%= rows["Satatu_tranferPMS"].ToString() %></td>
                        <td><%= rows["Status_Finish_PMS"].ToString() %></td>
                        <%--<td>
                            <a href="#" class="btn btn-primary" title="Update Status SAP" onclick="openEditModal10('<%= rows["RequestNo"].ToString() %>','<%= rows["TypeForm"].ToString() %>','<%= rows["Satatu_tranferPMS"].ToString() %>','<%= rows["Status_Finish_PMS"].ToString() %>')">Update_StatusSAP</a>
                            <a href="#" class="btn btn-primary" title="Update Doc" onclick="openEditModal6('<%= rows["RequestNo"].ToString() %>','<%= rows["TypeForm"].ToString() %>')">Update_Doc</a>
                            <a href="#" class="btn btn-info btn-sm" title="Detail" onclick="openEditModal2('<%= rows["RequestNo"].ToString() %>','<%= rows["TypeForm"].ToString() %>')">Detail</a>
                            <a href="#" class="btn btn-info btn-sm" title="Out201_98" onclick="openEditModal3('<%= rows["RequestNo"].ToString() %>','<%= rows["TypeForm"].ToString() %>','<%= rows["SanctionName"].ToString() %>')">Out201_98</a>
                            <% if (rows["TypeSapPMS"] != DBNull.Value && rows["TypeSapPMS"].ToString() == "Scrap") { %>
                                <a href="#" class="btn btn-info btn-sm" title="Tranfer_G99"
                                   onclick="openEditModal4('<%= rows["RequestNo"] %>','<%= rows["TypeForm"] %>','<%= rows["SanctionName"] %>')">
                                   Tranfer_G99
                                </a>
                            <% } %>                            
                            <a href="#" class="btn btn-info btn-sm" title="Out Scrap" onclick="openEditModal5('<%= rows["RequestNo"].ToString() %>','<%= rows["TypeForm"].ToString() %>','<%= rows["SanctionName"].ToString() %>')">Out Scrap</a>
                            <a href="#" class="btn btn-info btn-sm" title="Out Scrap" onclick="openEditModal7('<%= rows["RequestNo"].ToString() %>','<%= rows["TypeForm"].ToString() %>','<%= rows["SanctionName"].ToString() %>')">Other Type</a>
                            <a href="#" class="btn btn-info btn-sm" title="Out Sub" onclick="openEditModal9('<%= rows["RequestNo"].ToString() %>','<%= rows["TypeForm"].ToString() %>','<%= rows["SanctionName"].ToString() %>')">Sub</a>
                            <a href="#" class="btn btn-info btn-sm" title="feedback" onclick="openEditModal8('<%= rows["RequestNo"].ToString() %>','<%= rows["TypeForm"].ToString() %>','<%= rows["SanctionName"].ToString() %>')">Feedback</a>
                        </td>--%>
                        <td>
                            <!-- Các nút mặc định luôn hiện -->
                            <a href="#" class="btn btn-info btn-sm" title="Update Status SAP"
                                onclick="openEditModal10('<%= rows["RequestNo"] %>',
                                '<%= rows["TypeForm"] %>',
                                '<%= rows["Satatu_tranferPMS"] %>',
                                '<%= rows["Status_Finish_PMS"] %>')">Update_StatusSAP
                            </a>

                            <a href="#" class="btn btn-info btn-sm" title="Update Doc"
                                onclick="openEditModal6('<%= rows["RequestNo"] %>',
                               '<%= rows["TypeForm"] %>')">Update_Doc
                            </a>

                            <a href="#" class="btn btn-info btn-sm" title="Detail"
                                onclick="openEditModal2('<%= rows["RequestNo"] %>',
                               '<%= rows["TypeForm"] %>')">Detail
                            </a>

                            <a href="#" class="btn btn-info btn-sm" title="Feedback"
                                onclick="openEditModal8('<%= rows["RequestNo"] %>',
                               '<%= rows["TypeForm"] %>',
                               '<%= rows["SanctionName"] %>')">Feedback
                           </a>

                            <!-- Type = Sloc98 -->
                            <% if (type == "Sloc98")
                            { %>
                            <a href="#" class="btn btn-out201 btn-sm" title="Out201_98"
                                onclick="openEditModal3('<%= rows["RequestNo"] %>',
                                   '<%= rows["TypeForm"] %>',
                                   '<%= rows["SanctionName"] %>')">Out201_98
                             </a>
                            <% } %>

                            <!-- Type = Scrap -->
                            <% if (type == "Scrap")
                            { %>

                            <a href="#" class="btn btn-info btn-sm" title="Tranfer_G99"
                                onclick="openEditModal4('<%= rows["RequestNo"] %>',
                                   '<%= rows["TypeForm"] %>',
                                   '<%= rows["SanctionName"] %>')">Tranfer_G99
                            </a>

                            <a href="#" class="btn btn-scrap btn-sm" title="Out Scrap"
                                onclick="openEditModal5('<%= rows["RequestNo"] %>',
                                   '<%= rows["TypeForm"] %>',
                                   '<%= rows["SanctionName"] %>')">Out Scrap
                            </a>

                            <% } %>

                            <!-- Type = Other -->
                            <% if (type == "Other")
                            { %>

                            <a href="#" class="btn btn-other btn-sm" title="Other Type"
                                onclick="openEditModal7('<%= rows["RequestNo"] %>',
                                   '<%= rows["TypeForm"] %>',
                                   '<%= rows["SanctionName"] %>')">Other Type
                            </a>

                            <% } %>

                            <!-- Type = Sub -->
                            <% if (type == "Sub")
                            { %>

                            <a href="#" class="btn btn-sub btn-sm" title="Sub"
                                onclick="openEditModal9('<%= rows["RequestNo"] %>',
                                   '<%= rows["TypeForm"] %>',
                                   '<%= rows["SanctionName"] %>')">Sub
                            </a>

                            <% } %>

                        </td>
                    </tr>
                    <% } %>
                </tbody>
                <tfoot>
                    <tr>
                        <!-- Checkbox all -->
                        <th>
                   
                        </th>


                        <th>IDNO</th>
                        <th>RequestNo</th>
                        <th>Department</th>
                        <%--<th>Status_RQ</th>--%>
                        <th>Status_Approved_OutSAP</th>
                        <th>TypeForm</th>
                        <th>Sanction Name</th>
                        <th>Document No</th>

                        <th>TypeSapPMS</th>
                        <th>UserCreate</th>
                        <th>TypeID</th>
                        <th>commentlog</th>
                        <th>Linkfile</th>
                        <th>Comment_PMS</th>
                        <th>Status Tranfer SAP</th>
                        <th>Status_Finish_PMS</th>
                        <th>Actions</th>

                    </tr>
                </tfoot>
            </table>
        </div>

        <div id="pathModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">

                    <div class="modal-header">
                        <h4>Link Path File</h4>
                    </div>

                    <div class="modal-body">
                        <input type="text" id="txtPath" class="form-control" placeholder="" />
                    </div>

                    <div class="modal-footer">
                       <%-- <button type="button" class="btn btn-success" onclick="savePath()">Save</button>--%>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>

                </div>
            </div>
        </div>

        <div class="modal fade" id="detailModal" tabindex="-1">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">

                    <div class="modal-header">
                        <h4 class="modal-title">Request No Detail</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>

                    <div class="modal-body" style="max-height: 500px; overflow-y: auto;">

                        <table class="table table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th>NO</th>
                                    <th>Material</th>
                                    <th>Plant</th>
                                    <th>MVT</th>
                                    <th>Vendor</th>
                                    <th>CostCenter</th>
                                    <th>Sloc</th>
                                    <th>Qty</th>
                                    <th>UnitPriceST</th>
                                    <th>AmountST</th>
                                    <th>DocumentNo</th>
                                    <th>SanctionName</th>
                                    <th>Reason</th>
                                </tr>
                            </thead>

                            <tbody id="detailTableBody"></tbody>

                        </table>

                    </div>

                </div>
            </div>
        </div>

        <div class="modal" id="myModal3">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div>
                                <h4 class="modal-title" id="headerTag1" style="float: left">Dou you want export Out_201 from 98 ?</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                        </div>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">RequestNo</label>
                                <asp:TextBox ID="RequestNoid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">TypeForm</label>
                                <asp:TextBox ID="TypeFormid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">Sanction Name</label>
                                <asp:TextBox ID="sanctionid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">MVT</label>
                                <asp:TextBox ID="MVTid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>

                        </div>

                        <!-- Lặp lại thêm các dòng -->
                    </div>

                    <%-- Modal footer --%>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <button type="button" runat="server" id="Button1" onserverclick="Exportfrom98" class="btn btn-primary">
                            Export
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal" id="myModal4">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div>
                                <h4 class="modal-title" id="headerTag4" style="float: left">Dou you want export Issue Tranfer to G99 ?</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                        </div>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">RequestNo</label>
                                <asp:TextBox ID="RequestNo4" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">TypeForm</label>
                                <asp:TextBox ID="TypeForm4" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">Sanction Name</label>
                                <asp:TextBox ID="sanction4" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">MVT</label>
                                <asp:TextBox ID="MVT4" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>

                        </div>

                        <!-- Lặp lại thêm các dòng -->
                    </div>

                    <%-- Modal footer --%>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <button type="button" runat="server" id="Button2" onserverclick="Exporttranfer99" class="btn btn-primary">
                            Export
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal" id="myModal5">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div>
                                <h4 class="modal-title" id="headerTag5" style="float: left">Dou you want export Out Scrap ?</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                        </div>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">RequestNo</label>
                                <asp:TextBox ID="RequestNo5" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">TypeForm</label>
                                <asp:TextBox ID="TypeForm5" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">Sanction Name</label>
                                <asp:TextBox ID="sanction5" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">MVT</label>
                                <asp:TextBox ID="MVT5" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>

                        </div>

                        <!-- Lặp lại thêm các dòng -->
                    </div>

                    <%-- Modal footer --%>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <button type="button" runat="server" id="Button3" onserverclick="ExportOutscrap" class="btn btn-primary">
                            Export
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal" id="myModal7">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div>
                                <h4 class="modal-title" id="headerTag7" style="float: left">Dou you want export Other Issue Out ?</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                        </div>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">RequestNo</label>
                                <asp:TextBox ID="RequestNo7" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">TypeForm</label>
                                <asp:TextBox ID="TypeForm7" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">Sanction Name</label>
                                <asp:TextBox ID="sanction7" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">MVT</label>
                                <asp:TextBox ID="MVT7" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>

                        </div>

                        <!-- Lặp lại thêm các dòng -->
                    </div>

                    <%-- Modal footer --%>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <button type="button" runat="server" id="Button5" onserverclick="ExportOtherIssue" class="btn btn-primary">
                            Export
                        </button>
                    </div>
                </div>
            </div>
        </div>

  <div class="modal" id="myModal9">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div>
                        <h4 class="modal-title" id="headerTag9" style="float: left">Dou you want export Issue Out For Sub ?</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                </div>
            </div>

            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <label for="ID">RequestNo</label>
                        <asp:TextBox ID="RequestNo9" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ID">TypeForm</label>
                        <asp:TextBox ID="TypeForm9" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label for="ID">Sanction Name</label>
                        <asp:TextBox ID="sanction9" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ID">MVT</label>
                        <asp:TextBox ID="MVT9" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>

                </div>

                <!-- Lặp lại thêm các dòng -->
            </div>

            <%-- Modal footer --%>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                <button type="button" runat="server" id="Button9" onserverclick="ExportSubIssue" class="btn btn-primary">
                    Export
                </button>
            </div>
        </div>
    </div>
</div>

        <div class="modal" id="myModal6">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div>
                                <h4 class="modal-title" id="headerTag6" style="float: left">Dou you want update Document No ?</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                        </div>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">RequestNo</label>
                                <asp:TextBox ID="RequestNo6" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">TypeForm</label>
                                <asp:TextBox ID="TypeForm6" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">Document No <i style="color: red;">(* This field is not null! *)</i></label>
                                <asp:TextBox ID="DocumentNo6" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <%--<label for="ID">TypeForm</label>
                        <asp:TextBox ID="TextBox4" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>--%>
                            </div>
                        </div>

                        <!-- Lặp lại thêm các dòng -->
                    </div>

                    <%-- Modal footer --%>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <button type="button" runat="server" id="Button4" onserverclick="UpdateDocumentNo" class="btn btn-primary">
                            Update
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal" id="myModal10">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div>
                        <h4 class="modal-title" id="headerTag10" style="float: left">Do you want to update Status PMS ?</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                </div>
            </div>

            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <label for="ID">RequestNo</label>
                        <asp:TextBox ID="RequestNo10" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ID">TypeForm</label>
                        <asp:TextBox ID="TypeForm10" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label for="ID">Status Tranfer SAP<i style="color: green;">(* Tranfer SAP done! *)</i></label>
                        <asp:TextBox ID="DocumentNo10" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ID">Status_Finish_PMS<i style="color: green;">(* Request is done *)</i></label>
                <asp:TextBox ID="DocumentNo11" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                </div>

                <!-- Lặp lại thêm các dòng -->
            </div>

            <%-- Modal footer --%>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                <button type="button" runat="server" id="Button7" onserverclick="UpdateDocumentNo10" class="btn btn-primary">
                    Update
                </button>
            </div>
        </div>
    </div>
</div>


  <div class="modal" id="myModal8">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div>
                        <h4 class="modal-title" id="headerTag8" style="float: left">Feedback for User</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                </div>
            </div>

            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <label for="ID">RequestNo</label>
                        <asp:TextBox ID="RequestNo8" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ID">TypeForm</label>
                        <asp:TextBox ID="TypeForm8" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label for="ID">Sanction Name</label>
                        <asp:TextBox ID="sanction8" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ID">Feedback Contents</label>
                        <asp:TextBox ID="contents_feedback" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>

                </div>

                <!-- Lặp lại thêm các dòng -->
            </div>

            <%-- Modal footer --%>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                <button type="button" runat="server" id="Button6" onserverclick="btn_feedback_click" class="btn btn-primary">
                    Feedback
                </button>
            </div>
        </div>
    </div>
</div>


    </form>

    <%--    <script src="/plugins/jquery/jquery.min.js"></script>
<script src="/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
<script src="/plugins/datatables/jquery.dataTables.min.js"></script>
<script src="/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js"></script>
<script src="/plugins/datatables-responsive/js/dataTables.responsive.min.js"></script>
<script src="/plugins/datatables-responsive/js/responsive.bootstrap4.min.js"></script>
<script src="/dist/js/adminlte.min.js"></script>
<script src="/dist/js/demo.js"></script>--%>

    <script>

        //$(function () {
        //    $("#btnExport_normal").click(function () {
        //        $("#example1").table2excel({
        //            filename: "Report_inspection_normal"
        //        });
        //    })
        //});
    </script>

    <script>

        function collectCheckedRows() {

            //alert('ok');

            let selected = [];

            document.querySelectorAll(".row-check:checked")
                .forEach(function (checkbox) {

                    selected.push(checkbox.value);

                });

            // Gán vào hidden field
            document.getElementById("<%= hfSelectedRequest.ClientID %>").value =
                selected.join(",");

            // Nếu chưa chọn
            //if (selected.length === 0) {

            //    alert("Please select at least one request!");
            //    return false;
            //}

            return true;
        }

</script>

    <script type="text/javascript">  
        $(document).ready(function () {
            $('#RequestNoid').prop("readonly", true);
            $('#TypeFormid').prop("readonly", true);
            $('#RequestNo4').prop("readonly", true);
            $('#TypeForm4').prop("readonly", true);
            $('#RequestNo5').prop("readonly", true);
            $('#TypeForm5').prop("readonly", true);
            $('#RequestNo6').prop("readonly", true);
            $('#TypeForm6').prop("readonly", true)
        });

        $(function () {
            $("#example").DataTable({
                //"responsive": true,
                "autoWidth": true,
                scrollX: true,
                //"order": [[7, "desc"]],
                "pageLength": 50
                //"ordering": true,
                //"paging": true,
                //"lengthChange": false,
                //"searching": false,
                //"info": true,                    
            });

        });

        function openPathModal(pathfile) {
            $('#txtPath').val(pathfile);   // gán giá trị vào textbox
            $('#pathModal').modal('show');
        }

        function openEditModal2(requestNo, typeForm) {
            $.ajax({
                url: "frmPMSOutSap.aspx?action=getDetail",
                data: { requestNo: requestNo, typeForm: typeForm },
                dataType: "json",
                success: function (data) {
                    $("#detailModal .modal-title").text("Request No: " + requestNo);

                    // Kiểm tra data trước khi dùng
                    if (!Array.isArray(data)) {
                        console.error("Data không phải array:", data);
                        alert("Lỗi dữ liệu: " + JSON.stringify(data));
                        return;
                    }

                    var html = "";
                    for (var i = 0; i < data.length; i++) {
                        html += "<tr>";
                        html += "<td>" + (i + 1) + "</td>";  // Số thứ tự bắt đầu từ 1
                        html += "<td>" + data[i].Material + "</td>";
                        html += "<td>" + data[i].Plant + "</td>";
                        html += "<td>" + data[i].MvType + "</td>";
                        html += "<td>" + data[i].VendorCode + "</td>";
                        html += "<td>" + data[i].CostCenter + "</td>";
                        html += "<td>" + data[i].Sloc + "</td>";
                        html += "<td>" + data[i].IssueQty + "</td>";
                        html += "<td>" + data[i].UnitPrice_ST + "</td>";
                        html += "<td>" + data[i].Amount_ST + "</td>";
                        html += "<td>" + data[i].DocumentNo + "</td>";
                        html += "<td>" + data[i].SanctionName + "</td>";
                        html += "<td>" + data[i].Reason + "</td>";
                        html += "</tr>";
                    }
                    $("#detailTableBody").html(html);
                    $("#detailModal").modal("show");
                },
                error: function (xhr) {
                    console.error("AJAX error:", xhr.responseText);
                    alert("Lỗi server: " + xhr.status);
                }
            });
        }

        function openEditModal3(RequestNo, TypeForm, sanctionname) {
            $("#RequestNoid").val(RequestNo);
            $("#TypeFormid").val(TypeForm);
            $("#sanctionid").val(sanctionname);
            $('#myModal3').modal('show');
        }
        function openEditModal4(RequestNo, TypeForm, sanctionname) {
            $("#RequestNo4").val(RequestNo);
            $("#TypeForm4").val(TypeForm);
            $("#sanction4").val(sanctionname);
            $('#myModal4').modal('show');
        }
        function openEditModal5(RequestNo, TypeForm, sanctionname) {
            $("#RequestNo5").val(RequestNo);
            $("#TypeForm5").val(TypeForm);
            $("#sanction5").val(sanctionname);
            $('#myModal5').modal('show');
        }

        function openEditModal7(RequestNo, TypeForm, sanctionname) {
            $("#RequestNo7").val(RequestNo);
            $("#TypeForm7").val(TypeForm);
            $("#sanction7").val(sanctionname);
            $('#myModal7').modal('show');
        }

        function openEditModal9(RequestNo, TypeForm, sanctionname) {
            $("#RequestNo9").val(RequestNo);
            $("#TypeForm9").val(TypeForm);
            $("#sanction9").val(sanctionname);
            $('#myModal9').modal('show');
        }

        function openEditModal8(RequestNo, TypeForm, sanctionname) {
            $("#RequestNo8").val(RequestNo);
            $("#TypeForm8").val(TypeForm);
            $("#sanction8").val(sanctionname);
            $('#myModal8').modal('show');
        }

        function openEditModal6(RequestNo, TypeForm) {
            $("#RequestNo6").val(RequestNo);
            $("#TypeForm6").val(TypeForm);
            $('#myModal6').modal('show');
        }

        function openEditModal10(RequestNo, TypeForm, DocumentNo10,trangthaifinish) {
            $("#RequestNo10").val(RequestNo);
            $("#TypeForm10").val(TypeForm);
            $("#DocumentNo10").val(DocumentNo10)
            $("#DocumentNo11").val(trangthaifinish)
            $('#myModal10').modal('show');
        }

    </script>

    <%--<script src="/plugins/jquery/jquery-ui.js"></script>--%>
    <script type="text/javascript">    

</script>
</body>
</html>
