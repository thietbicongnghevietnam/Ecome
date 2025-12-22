<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Material_Issues_FormA.aspx.cs" Inherits="MATERIAL_IN_OUT.Material_Issues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function openInNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () { window.document.forms[0].target = ''; }, 0);
        }
    </script>
    <style type="text/css">
        .treeNode {
            color: blue;
            font: 14px Arial, Sans-Serif;
        }
        .rootNode {
            font-size: 18px;
            width: 100%;
            border-bottom: Solid 1px black;
        }
        .leafNode {
            border: Dotted 2px black;
            padding: 4px;
            background-color: #eeeeee;
            font-weight: bold;
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

                    <table class="table table-bordered table-striped table-hover">
                        <tbody>

                            <tr>
                                <td colspan="2">Voucher Date (dd/MM/yyyy)
                                </td>
                                <td>
                                    <div class="date-picker-inner">
                                        <div class="form-group data-custon-pick" id="data_2">
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <input id="txtDateInput" type="text" class="form-control" placeholder="dd-mm-yyyy" runat="server" />


                                            </div>
                                        </div>
                                    </div>
                                    <%--<asp:TextBox ID="txtDateVoucher" CssClass="form-control" AutoPostBack="true" runat="server" type="date" />--%>
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
                    </table>
                    <asp:HiddenField ID="hdfStock" runat="server" />
                </div>
            </div>

            <div class="col-md-3 col-md-3 col-sm-3 col-xs-12">
                <div class="panel-body">
                    <a class="btn btn-success compose-btn btn-block m-b-md">Control Request Issue</a>
                    
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
                                        <div id="toolbar">
                                            <select class="form-control">
                                                <option value="">Export Basic</option>
                                                <option value="all">Export All</option>
                                                <option value="selected">Export Selected</option>
                                            </select>
                                        </div>
                                        <table id="table" data-toggle="table" data-pagination="true" data-search="true" data-show-columns="true" data-show-pagination-switch="true" data-show-refresh="true" data-key-events="true" data-show-toggle="true" data-resizable="true" data-cookie="true"
                                            data-cookie-id-table="saveId" data-show-export="true" data-click-to-select="true" data-toolbar="#toolbar">
                                            <thead>
                                                <tr>

                                                    <th data-field="id">No.</th>
                                                    <th>RequestNo</th>
                                                    <th>Material</th>
                                                    <th>Voucher Date</th>
                                                    <th>Sloc</th>
                                                    <th>Issue Qty</th>
                                                    <th>Unit Price(ST)</th>
                                                    <th>Amount(ST)</th>
                                                    <th>Reason</th>
                                                    <th>Status</th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                            
                                                <asp:Repeater runat="server" ID="dtIssueMaterial">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%#Eval("No") %></td>
                                                            <td><%#Eval("RequestNo") %></td>
                                                            <td><%#Eval("Material") %></td>
                                                            <td><%#Eval("DateVoucher") %></td>
                                                            <td><%#Eval("Sloc") %></td>
                                                            <td><%#Eval("IssueQty") %></td>
                                                             <td><%#Eval("UnitPrice_ST") %></td>
                                                            <td><%#Eval("Amount_ST") %></td>
                                                            <td><%#Eval("Note") %></td>
                                                            <td><%#Eval("Status_RQ") %></td>
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
                                            <ul class="list-inline events">
                                                <%int j = 1;%>
                                                <%foreach (System.Data.DataRow rows in dt_Comment.Rows)
                                                    {%>
                                                <li class="list-inline-item event-list" style="padding-top: 10px">
                                                    <div class="px-4">
                                                        <div class="event-date bg-soft-success text-success" style="font-size: 9px; color: black"><%=rows["DateUpdate"].ToString() %></div>
                                                        <h5 class="font-size-16"><%=rows["FullName"].ToString() %></h5>
                                                        <p class="text-muted"><%=rows["Content_Comment"].ToString() %></p>
                                                    </div>
                                                </li>
                                                <% } %>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="panel-footer">
                            <div class="btn-group">
                                <asp:FileUpload ID="FileUpload1" class="btn btn-default" runat="server" />
                                <asp:Button ID="bttUpload" class="btn btn-info" runat="server" Text="Upload(File A)" OnClick="bttUpload_Click" />
                                <asp:Button ID="bttTempFile" class="btn btn-info" Text="Download Tempfile" runat="server" OnClick="bttTempFile_Click" />
                                <asp:Button ID="bttPrint" OnClientClick="openInNewTab()" CssClass="btn btn-info" Text="Report PDF" runat="server" OnClick="bttPrint_Click" />
                                <asp:Button ID ="bttDownExcel" CssClass="btn btn-info" OnClick ="bttDownExcel_Click"  runat  ="server"  Text ="Download Excel"/>
                                
                                <%--OnClick="bttPrint_Click"--%>
                            </div>
                            <div class="pull-right">
                               <div class="btn-group" >
                                    
                                    <asp:Button ID="bttApproved" CssClass="btn btn-info" runat="server" OnClick="bttApproved_Click" />
                                    &nbsp;
                                 <asp:Button ID="bttReject" CssClass="btn btn-info" Text="Reject" runat="server" OnClick="bttReject_Click" />
                                    &nbsp; 
                                <asp:Button ID="bttReset"  CssClass="btn btn-warning" Text="Reset" runat="server" OnClick="bttReset_Click" />
                                   &nbsp;
                                 <asp:Button ID="bttDelete" CssClass="btn btn-danger" Text="Delete" runat="server"  OnClick ="bttDelete_Click" />

                                <button type="button" id="Comment" onclick="openAdd('<%=hdfRequest.Value.ToString()%>','<%=hdfControlRQ.Value.ToString()%>','<%=hdfControlACC.Value.ToString()%>','<%=hdfControlStore.Value.ToString()%>','<%=hdfUserUpdate.Value.ToString()%>')" class="btn btn-info">Comment</button>
                                </div>
                                   
                            </div>
                        </div>
                        <asp:HiddenField ID="hdfStatus_Upload" runat="server" />


                        <%-- Modal footer --%>
                    </div>
                </div>
                <div class="modal" id="myNew">
                    <div class="container">
                        <div class="row bootstrap snippets bootdeys">
                            <div class="col-md-8 col-sm-12">
                                <div class="comment-wrapper">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Comments -  ( RequestNo:
                                      <asp:Label ID="lblRequestUpdate" runat="server"></asp:Label>)
                                            <asp:HiddenField ID  ="hdfRQ_UpdateComent" runat ="server" />
                                        </div>
                                        <div class="panel-body">
                                            <textarea id="txt_Comment" runat="server" class="form-control" placeholder="Write a comment..." rows="3"></textarea>
                                            <br>
                                            <button type="button" id="bttSend" class="btn btn-info pull-right" runat="server" onserverclick="bttSend_Click">Send </button>
                                            <%--onserverclick="bttSend_Click"--%>
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

        </div>
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
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
</asp:Content>




