<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Material_Issue_FormB.aspx.cs" Inherits="MATERIAL_IN_OUT.ClaimScap_Material" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function openInNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () { window.document.forms[0].target = ''; }, 0);
        }
    </script>

    <style>
        table tbody tr.price-sap td {
            background-color: #f8d7da !important; /* đỏ nhạt */
            /*color: #842029;*/ /* chữ đỏ đậm */
        }
    </style>

    <div class="mailbox-view-area mg-tb-15">
        <div class="container-fluid">
            <div class="row">
                <h1>
                    <center>
                        <asp:Label ID="lblRequest" runat="server"></asp:Label></center>
                </h1>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" style="float: right; margin-right: 150px">
                    <table class="table table-bordered table-striped table-hover">
                        <tr>
                            <th></th>
                            <th colspan="3" style="text-align: center">
                                <h4>Issue Dept</h4>
                            </th>
                            <th colspan="3" style="text-align: center">
                                <h4>Accounting
                                </h4>
                            </th>
                            <th colspan=" 3" style="text-align: center">
                                <h4>Issue Out Dept</h4>
                            </th>
                        </tr>
                        <tr>
                            <th></th>
                            <th>Charge</th>
                            <th>AM/Manager</th>
                            <th>GM</th>
                            <th>Check</th>
                            <th>MGR</th>
                            <th>GM</th>
                            <th>Charge</th>
                            <th>AM/Manager</th>
                            <th>GM</th>
                        </tr>
                        <tbody>
                            <tr>
                                <td>
                                    <h5>Status</h5>
                                </td>
                                <td>
                                    <asp:Image ID="img_IssueCharge" runat="server" />
                                    <asp:HiddenField ID="hdf_IssueCharge" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="img_IssueMGR" runat="server" />
                                    <asp:HiddenField ID="hdf_IssueMGR" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="img_IssueGM" runat="server" />
                                    <asp:HiddenField ID="hdf_IssueGM" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="img_ACCCheck" runat="server" />
                                    <asp:HiddenField ID="hdf_ACCCheck" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="img_ACCMGR" runat="server" />
                                    <asp:HiddenField ID="hdf_ACCMGR" runat="server" />
                                </td>

                                <td>
                                    <asp:Image ID="img_ACCCGM" runat="server" />
                                    <asp:HiddenField ID="hdf_ACCGM" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="img_OutCharge" runat="server" />
                                    <asp:HiddenField ID="hdf_OutCharge" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="img_OutMGR" runat="server" />
                                    <asp:HiddenField ID="hdf_OutMGR" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="img_OutGM" runat="server" />
                                    <asp:HiddenField ID="hdf_OutGM" runat="server" />
                                </td>

                            </tr>

                            <tr>
                                <td>
                                    <h5>Name</h5>
                                </td>
                                <td>
                                    <asp:Label ID="lblIssueCharge_Name" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblIssueMGR_Name" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblIssueGM_Name" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblACCCheck_Name" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblMGR_Name" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblACCGM_Name" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblOutCharge_Name" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblOutCheck_Name" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblOutGM_Name" runat="server"> </asp:Label></td>

                            </tr>
                            <tr>
                                <td>
                                    <h5>Date</h5>
                                </td>
                                <td>
                                    <asp:Label ID="lblIssueCharge_Date" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblIssueMGR_Date" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblIssueGM_Date" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblACCCheck_Date" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblACCMGR_Date" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblACCGM_Date" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblOutCharge_Date" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblOutCheck_Date" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblOutGM_Date" runat="server"> </asp:Label></td>

                            </tr>

                        </tbody>
                    </table>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4" style="float: left; margin-left: 20px; left: 0px; top: 0px;">


                    <%--<table class="table table-bordered table-striped table-hover">
                        <tbody>

                            <tr>
                                <td colspan="2">Voucher Date (dd/MM/yyyy)
                                </td>
                                <td>
                                   <div class="date-picker-inner">
                                        <div class="form-group data-custon-pick" id="data_2">
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <input id="txtDateInput" type="text" class="form-control" placeholder="dd-mm-yyyy"  runat ="server"/>

                                            </div>
                                        </div>
                                    </div>
                                    

                                </td>

                            </tr>
                            <tr>
                                <td>MV Type</td>
                                <td>
                                    <asp:Label ID="lblMvTYpe" runat="server"></asp:Label>

                                </td>

                                <td>
                                    <asp:Label ID="lblIN" runat="server"></asp:Label>

                                </td>

                            </tr>
                            <tr>
                                <td>Plant</td>
                                <td>
                                    <asp:Label ID="lblPlant" runat="server"></asp:Label>

                                </td>
                                <td>
                                    <asp:Label ID="lblPlantName" runat="server"></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Account</td>
                                <td>
                                    <asp:Label ID="lblAcount" runat="server"></asp:Label>

                                </td>
                                <td>
                                    <asp:Label ID="lblAccountName" runat="server"></asp:Label></td>

                            </tr>
                            <tr>
                                <td>Cost Center</td>
                                <td>
                                    <asp:Label ID="lblCostCenter" runat="server"></asp:Label>

                                </td>
                                <td>
                                    <asp:Label ID="lblCostCenterName" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblVendorCode" runat="server"></asp:Label></td>
                                <td colspan="2">
                                    <asp:Label ID="lblvendorName" runat="server"></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>--%>

                    <table class="table table-bordered table-striped table-hover">
                        <tbody>

                            <!-- Voucher Date -->
                            <tr>
                                <td colspan="2"><b>Voucher Date (dd/MM/yyyy)</b></td>
                                <td colspan="7">
                                    <%--<asp:Label ID="lblVoucherDate" runat="server"></asp:Label>--%>
                                    <div class="date-picker-inner">
                                        <div class="form-group data-custon-pick" id="data_2">
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <input id="txtDateInput" type="text" class="form-control" placeholder="dd-mm-yyyy" runat="server" />

                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>

                            <!-- MV Type -->
                            <tr>
                                <td colspan="2"><b>MV Type</b></td>
                                <td colspan="7">
                                    <asp:Label ID="lblMvTYpe" runat="server"></asp:Label>
                                    -
                                <asp:Label ID="lblIN" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <!-- Cost Center Header -->
                            <tr>
                                <td colspan="2"><b>Plan</b></td>
                                <td><asp:Label ID="lblCC_VR01" runat="server" Text="VR01" /></td>
                                <td><asp:Label ID="lblCC_VE01" runat="server" Text="VE01" /></td>
                                <td><asp:Label ID="lblCC_VG01" runat="server" Text="VG01" /></td>
                                <td><asp:Label ID="lblCC_V501" runat="server" Text="V501" /></td>
                                <td><asp:Label ID="lblCC_VB01" runat="server" Text="VB01" /></td>
                                <td><asp:Label ID="lblCC_VY01" runat="server" Text="VY01" /></td>
                                <td><asp:Label ID="lblCC_tong" runat="server" Text="Total" /></td>
                            </tr>

                            <!-- Quantity -->
                            <tr>
                                <td colspan="2"><b>Quantity</b></td>
                                <td>
                                    <asp:Label ID="lblQtyVR01" runat="server" /></td>
                                <td>
                                    <asp:Label ID="lblQtyVE01" runat="server" /></td>
                                <td>
                                    <asp:Label ID="lblQtyVG01" runat="server" /></td>
                                <td>
                                    <asp:Label ID="lblQtyV501" runat="server" /></td>
                                <td>
                                    <asp:Label ID="lblQtyVB01" runat="server" /></td>
                                <td><asp:Label ID="lblQtyVY01" runat="server" /></td>
                                <td><asp:Label ID="lblQtyTong" runat="server" /></td>
                            </tr>

                            <!-- Amount -->
                            <tr>
                                <td colspan="2"><b>Amount (ST)</b></td>
                                <td>
                                    <asp:Label ID="lblAmtVR01" runat="server" /></td>
                                <td>
                                    <asp:Label ID="lblAmtVE01" runat="server" /></td>
                                <td>
                                    <asp:Label ID="lblAmtVG01" runat="server" /></td>
                                <td>
                                    <asp:Label ID="lblAmtV501" runat="server" /></td>
                                <td>
                                    <asp:Label ID="lblAmtVB01" runat="server" /></td>
                                <td><asp:Label ID="lblAmtVY01" runat="server" /></td>
                                <td><asp:Label ID="lblAmttong" runat="server" /></td>
                            </tr>

                            <!-- Vendor -->
                            <tr>
                                <td colspan="2">
                                    <b>
                                        <asp:Label ID="lblVendorCode" runat="server"></asp:Label>
                                    </b>
                                </td>
                                <td colspan="6">
                                    <asp:Label ID="lblvendorName" runat="server"></asp:Label>
                                </td>
                            </tr>

                        </tbody>
                    </table>




                    <asp:HiddenField ID="hdfStock" runat="server" />
                </div>
            </div>

            <div class="col-md-3 col-md-3 col-sm-3 col-xs-12" style="left: 0px; top: 0px">

                <div class="panel-body">
                    <a class="btn btn-success compose-btn btn-block m-b-md" style="background-color: #004B8D; border-color: #004B8D; color: #fff;">Control Request Issue</a>

                    <asp:TreeView ID="treeRQ_InMaterial" runat="server" ForeColor="Blue" OnSelectedNodeChanged="treeRQ_InMaterial_SelectedNodeChanged">
                    </asp:TreeView>

                    <asp:TreeView ID="treeRQ_OutMateial" ForeColor="#000066" runat="server" OnSelectedNodeChanged="treeRQ_OutMateial_SelectedNodeChanged">
                    </asp:TreeView>
                </div>


            </div>

            <div class="col-md-9 col-md-9 col-sm-9 col-xs-12">
                <div class="hpanel email-compose mailbox-view mg-b-15">
                    <div class="data-table-area mg-tb-15">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                                    <div class="datatable-dashv1-list custom-datatable-overright">

                                        <table id="table" data-toggle="table" data-pagination="true" data-search="true" data-show-columns="true" data-show-pagination-switch="true" data-show-refresh="true" data-key-events="true" data-show-toggle="true" data-resizable="true" data-cookie="true"
                                            data-cookie-id-table="saveId" data-show-export="true" data-click-to-select="true" data-toolbar="#toolbar">
                                            <thead>
                                                <tr>

                                                    <th data-field="id">No.</th>
                                                    <th>RequestNo</th>
                                                    <th>Material</th>
                                                    <th>Plan</th>
                                                    <th>Sloc</th>
                                                    <th>Issue Qty</th>
                                                    <th>Unit Price(ST)</th>
                                                    <th>Amount(ST)</th>
                                                    <th>Unit Price(AC)</th>
                                                    <th>Amount(AC)</th>

                                                    <th>Costcenter</th>
                                                    <th>AccountCost</th>

                                                    <th>Reason</th>
                                                    <th>Rosh/Halb</th>
                                                    <th>Status</th>
                                                   <%-- <th>Flag</th>--%>
                                                    <%--<th style="text-align: center">--%>
                                                        <%--<input type="checkbox" class="chk-edit-price" onclick="toggleAll(this)" />--%>
                                                    <%--</th>--%>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="dtIssueMaterial">
                                                    <ItemTemplate>
                                                        <tr class='<%# Eval("Flag_price_sap") != DBNull.Value 
                                                                && Convert.ToInt32(Eval("Flag_price_sap")) == 1 
                                                                ? "price-sap" 
                                                                : "" %>'>
                                                            <td><%# Eval("No") %></td>
                                                            <td><%# Eval("RequestNo") %></td>
                                                            <td><%# Eval("Material") %></td>
                                                            <td><%# Eval("Plant") %></td>
                                                            <td><%# Eval("Sloc") %></td>
                                                            <td><%# Eval("IssueQty") %></td>
                                                            <td><%# Eval("UnitPrice_ST") %></td>
                                                            <td><%# Eval("Amount_ST") %></td>
                                                            <td><%# Eval("UnitPrice_AC") %></td>
                                                            <td><%# Eval("Amount_AC") %></td>
                                                            <td><%# Eval("CostCenter") %></td>
                                                            <td><%# Eval("AccountCost") %></td>
                                                            <td><%# Eval("Reason") %></td>
                                                            <td><%# Eval("Type_Rosh_Halb") %></td>
                                                            <td><%# Eval("Status_RQ") %></td>
                                                           <%-- <td><%# Eval("Flag_price_sap") %></td>--%>
                                                            <!-- CHECKBOX -->
                                                           <%-- <td style="text-align: center">
                                                                     <asp:CheckBox ID="chkSelect" runat="server" CssClass="chk-edit-price"
                                                                    Attributes='data-id=<%# Eval("ID") %>
                                                                                data-price-st=<%# Eval("UnitPrice_ST") %>
                                                                                data-price-ac=<%# Eval("UnitPrice_AC") %>' />
                                                            </td>--%>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>


                                            </tbody>
                                        </table>

                                       
            

                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="container-fluid">
                            <div class="row">

                                <div class="card">
                                    <div class="card-body">
                                        <h4 class="card-title mb-5">Horizontal Comments</h4>
                                        <div class="hori-timeline" dir="ltr">
                                            <%--<ul class="list-inline events">--%>
                                               <%int j = 1;%>
                                                <%foreach (System.Data.DataRow rows in dt_Comment.Rows)
                                                    {%>
                                                <div class="event-date bg-soft-success text-success" style="font-size: 20px; color: black">
                                                    <%=rows["FullName"].ToString() %>:<%=rows["DateUpdate"].ToString() %> -> <%=rows["Content_Comment"].ToString() %> 

                                                </div>
                                                <% } %>
                                           <%-- </ul>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="panel-footer">
                            <div class="btn-group" style="font-size: 25px;">
                                <asp:FileUpload ID="FileUpload1" CssClass="btn btn-default" runat="server" />
                                <asp:Button ID="bttUpload" CssClass="btn btn-info " runat="server" Text="Upload(File B)" OnClick="bttUpload_Click" />
                                <asp:Button ID="bttTempFile" CssClass="btn btn-info" Text="TempUpload" runat="server" OnClick="bttTempFile_Click" />
                                <asp:Button ID="bttPrint" OnClientClick="openInNewTab()" CssClass="btn btn-info" Text="Down PDF" runat="server" OnClick="bttPrint_Click" />
                                <asp:Button ID="bttDownExcel" CssClass="btn btn-info" runat="server" OnClick="bttDownExcel_Click" Text="Down Excel" />
                                <asp:Button ID="bttCheckPrice" CssClass="btn btn-info" runat="server" OnClick="bttCheckPrice_Click" Text="Check SAP" />
                            </div>
                            <div class="pull-right">
                                <div class="btn-group">
                                    <%--<div class="col-lg-8"  style ="float:left; font-size:25px;">--%>
                                    <asp:Button ID="bttApproved" CssClass="btn btn-info" runat="server" OnClick="bttApproved_Click" />&nbsp;
                                    <asp:Button ID="bttReject" CssClass="btn btn-info" Text="Reject" runat="server" OnClick="bttReject_Click" />&nbsp;
                                    <asp:Button ID="bttReset" CssClass="btn btn-warning" Text="Reset" runat="server" OnClick="bttReset_Click" />&nbsp;
                                    <asp:Button ID="bttDelete" CssClass="btn btn-danger" Text="Delete" runat="server" OnClick="bttDelete_Click" />
                                    <button type="button" id="Comment" onclick="openAdd('<%=hdfRequest.Value.ToString()%>','<%=hdfControlRQ.Value.ToString()%>','<%=hdfControlACC.Value.ToString()%>','<%=hdfControlStore.Value.ToString()%>','<%=hdfUserUpdate.Value.ToString()%>')" class="btn btn-info">Comment</button>
                                    <%--</div>--%>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <%--<div class="btn-group" style="font-size: 25px;">
                                <asp:Button ID="Button1" CssClass="btn btn-info " runat="server" Text="Export Scrap List" OnClick="btnExport_ScrapList" />
                            </div>--%>
                            <div class="btn-group" style="font-size:25px;">
                                <asp:Button ID="btnOpenPopup"
                                    CssClass="btn btn-info"
                                    runat="server"
                                    Text="Input IAF"
                                    OnClientClick="showExportPopup(); return false;" />
                            </div>

                            <div class="btn-group" style="font-size:25px;">
                                <asp:Button ID="btnOpenPopup2"
                                    CssClass="btn btn-info"
                                    runat="server"
                                    Text="Delete Request"
                                    OnClientClick="showExportPopup2(); return false;" />
                            </div>

                            <div class="btn-group" style="font-size:25px;">                                
                                <button type="button" id="update path" onclick="openAdd2('<%=hdfRequest.Value.ToString()%>','<%=hdfControlRQ.Value.ToString()%>')" class="btn btn-info">Attached Link File</button>
                            </div>

                        </div>
                        
                    </div>
                    <%-- Modal footer --%>
                </div>
                <asp:HiddenField ID="hdfStatus_Upload" runat="server" />
            </div>

            <!-- Modal export scrap list -->
            <div id="exportModal" class="modal fade" role="dialog">
                <div class="modal-dialog">

                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Export Scrap Form B</h4>
                        </div>

                        <div class="modal-body">
                            <label>Input Name Sanction</label>
                            <asp:TextBox ID="txtNameSanction" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnExport"
                                runat="server"
                                Text="Export"
                                CssClass="btn btn-success"
                                OnClick="btnExport_ScrapList" />

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>

                </div>
            </div>

            <!-- Modal delete requestno -->
            <div id="exportModal2" class="modal fade" role="dialog">
                <div class="modal-dialog">

                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Delete request no!</h4>
                        </div>

                        <div class="modal-body">
                            <label>Input RequesetNo form B</label>
                            <asp:TextBox ID="txtrequestno" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="Button2"
                                runat="server"
                                Text="Delete"
                                CssClass="btn btn-success"
                                OnClick="btnDelete_requestno" />

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>

                </div>
            </div>

            <div id="exportModal3" class="modal fade" role="dialog">
                <div class="modal-dialog">

                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Input path file for attack file!</h4>
                        </div>
                          <div class="panel-heading">
                                  RequestNo:
                            <asp:Label ID="lblrequest_att" runat="server"></asp:Label>      
                              <asp:HiddenField ID="hdRequestNo" runat="server" />
                              </div>

                        <div class="modal-body">
                            <label>Path File:</label>
                            <asp:TextBox ID="txtpathfile" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="modal-footer">                               
                             <asp:Button ID="Button1"
                                 runat="server"
                                 Text="Save"
                                 CssClass="btn btn-success"
                                 OnClick="btnupdate_linkpath" />

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>

                </div>
            </div>

            <div class="modal fade" id="myNew">
                <div class="container">
                    <div class="row bootstrap snippets bootdeys">
                        <div class="col-md-8 col-sm-12">
                            <div class="comment-wrapper">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        Comments -  ( RequestNo:
                                    <asp:Label ID="lblRequestUpdate" runat="server"></asp:Label>)
                                          <asp:HiddenField ID="hdfRQ_UpdateComent" runat="server" />
                                    </div>
                                    <div class="panel-body">
                                        <textarea id="txt_Comment" runat="server" class="form-control" placeholder="Write a comment..." rows="3"></textarea>
                                        <br>
                                        <button type="button" id="bttSend" class="btn btn-info pull-right" runat="server" onserverclick="bttSend_Click">Send </button>
                                        <button type="button" class="btn btn-info pull-right" data-dismiss="modal">Close</button>
                                        <div class="clearfix"></div>
                                        <hr>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
            <asp:HiddenField ID="hdfRoleRQ_UpdateComment" runat="server" />
            <asp:HiddenField ID="hdfRoleACC_UpdateComment" runat="server" />
            <asp:HiddenField ID="hdfRoleSTORE_UpdateComment" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />
            <asp:HiddenField ID="hdfRoleupdate" runat="server" />
            <asp:HiddenField ID="hdfRequest" runat="server" />
            <asp:HiddenField ID="hdfUserUpdate" runat="server" />
            <asp:HiddenField ID="hdfControlRQ" runat="server" />
            <asp:HiddenField ID="hdfControlStore" runat="server" />
            <asp:HiddenField ID="hdfControlACC" runat="server" />
            <asp:HiddenField ID="hdftreeview" runat="server" />

            


        </div>
        <
    </div>

    <script type="text/javascript"> 
        function openAdd(lbl_RQ, RoleRQ, RoleACC, RoleStock, User) {
            $('#<%=lblRequestUpdate.ClientID%>').val(lbl_RQ);
            $('#<%=hdfRoleRQ_UpdateComment.ClientID%>').val(RoleRQ);
            $('#<%=hdfRoleACC_UpdateComment.ClientID%>').val(RoleACC);
            $('#<%=hdfRoleSTORE_UpdateComment.ClientID%>').val(RoleStock);
            $('#<%=hdfUserUpdate.ClientID%>').html(User);
            $('#<%=hdfRQ_UpdateComent.ClientID%>').val(lbl_RQ);

            $('#myNew').modal('show');

            
        }
    </script>

    <script>
        //function toggleAll(source) {
        //    let boxes = document.querySelectorAll('input[type=checkbox][id*="chkSelect"]');
        //    for (let i = 0; i < boxes.length; i++) {
        //        boxes[i].checked = source.checked;
        //    }
        //}
    </script>

    <script>
         function showExportPopup() {
             $('#exportModal').modal('show');
        }

        function showExportPopup2() {
            $('#exportModal2').modal('show');
        }

        function openAdd2(lbl_RQ, RoleRQ) {
            console.log(lbl_RQ);
            $('#MainContent_lblrequest_att').text(lbl_RQ);

            // set vào hidden field để gửi về server
            $('#MainContent_hdRequestNo').val(lbl_RQ);

            $('#exportModal3').modal('show');
        }

    </script>





</asp:Content>
