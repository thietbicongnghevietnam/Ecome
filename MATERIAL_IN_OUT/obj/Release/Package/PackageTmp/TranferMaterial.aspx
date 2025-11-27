<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TranferMaterial.aspx.cs" Inherits="MATERIAL_IN_OUT.TranferMaterial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function openInNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () { window.document.forms[0].target = ''; }, 0);
        }
    </script>
    <div class="mailbox-view-area mg-tb-15">
        <div class="container-fluid">
            <div class="row">
                <h1>
                    <center>TRANSFER REQUEST</center>
                </h1>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" style="float: right; margin-right: 150px">
                    <table class="table table-bordered table-striped table-hover">
                        <tr>
                            <th></th>
                            <th colspan="2" style="text-align: center">
                                <h4>Department</h4>
                            </th>
                            <th colspan="3" style="text-align: center">
                                <h4>Section Incharge
                                </h4>
                            </th>
                        </tr>
                        <tr>
                            <th style="height: 34px; text-align: center"></th>
                            <th style="height: 34px; text-align: center">Issued by</th>
                            <th style="height: 34px; text-align: center">Approved by</th>
                            <th style="height: 34px; text-align: center">Checked by</th>
                            <th style="height: 34px; text-align: center">Approved by</th>
                            <th style="height: 34px; text-align: center">Input</th>

                        </tr>
                        <tbody>

                            <tr>
                                <td>
                                    <h5>Status</h5>
                                </td>
                                <td>
                                    <asp:Image ID="img_IssueDept" runat="server" />
                                    <asp:HiddenField ID="hdf_IssueDept" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="img_ApprovedDept" runat="server" />
                                    <asp:HiddenField ID="hdf_ApprovedDept" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="img_CheckSectionTo" runat="server" />
                                    <asp:HiddenField ID="hdf_CheckSectionTo" runat="server" />

                                </td>
                                <td>
                                    <asp:Image ID="img_ApprovedSectionTo" runat="server" />
                                    <asp:HiddenField ID="hdf_ApprovedSectionTo" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="img_InputSectionTo" runat="server" />
                                    <asp:HiddenField ID="hdf_InputSectionTo" runat="server" />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <h5>Name</h5>
                                </td>
                                <td>
                                    <asp:Label ID="lblIssueDept_Name" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblApprovedDept_Name" runat="server"> </asp:Label></td>
                                <td>

                                    <asp:Label ID="lblCheckSectionTo_Name" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblApprovedSectionTo_Name" runat="server"> </asp:Label></td>
                                <td>

                                    <asp:Label ID="lblInputSectionTo_Name" runat="server"> </asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <h5>Date</h5>
                                </td>
                                <td>
                                    <asp:Label ID="lblIssueDept_Date" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblApprovedDept_Date" runat="server"> </asp:Label></td>
                                <td>
                                    <asp:Label ID="lblCheck_SectionTo_Date" runat="server"> </asp:Label></td>
                                <td><asp:Label ID="lblApproved_SectionTo_Date" runat="server"> </asp:Label></td>
                               <td> <asp:Label ID="lblInput_SectionTo_Date" runat="server"> </asp:Label></td>
                            </tr>

                        </tbody>
                    </table>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4" style="float: left; margin-left: 20px; left: 0px; top: 0px;">

                    <table class="table table-bordered table-striped table-hover">
                        <tbody>

                            <tr>
                                <td colspan="2">Issue Date (dd/MM/yyyy)
                                </td>
                                <td>
                                    <asp:Label ID="lblIssueDate" runat="server"></asp:Label>

                                </td>

                            </tr>
                            <tr>
                                <td>Slip.No</td>
                                <td colspan="2">
                                    <asp:Label ID="lblSlipNo" runat="server"></asp:Label>

                                </td>

                            </tr>
                            <tr>
                                <td>Tran.Code</td>
                                <td colspan="2">
                                    <asp:Label ID="lblTranCode" runat="server"></asp:Label>

                                </td>

                            </tr>
                            <tr>
                                <td>Move Type</td>
                                <td colspan="2">
                                    <asp:Label ID="lblMoveType" runat="server"></asp:Label>
                                </td>

                            </tr>
                        </tbody>
                    </table>

                </div>
            </div>

            <div class="col-md-3 col-md-3 col-sm-3 col-xs-12" style="left: 0px; top: 0px">

                <div class="panel-body">
                    <a class="btn btn-success compose-btn btn-block m-b-md">Control Request Issue</a>
                    <div class="sparkline10-graph">
                        <div class="adminpro-content">
                            <asp:TreeView ID="treeNOT_Approved" runat="server" OnSelectedNodeChanged="treeNOT_Approved_SelectedNodeChanged">
                            </asp:TreeView>
                        </div>
                    </div>

                    <hr>
                    <div class="sparkline10-graph">
                        <div class="adminpro-content">

                            <asp:TreeView ID="treeApproved" runat="server" OnSelectedNodeChanged="treeApproved_SelectedNodeChanged">
                            </asp:TreeView>
                        </div>
                    </div>

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
                                                    <th>Request No</th>
                                                    <th>Issue Sloc</th>
                                                    <th>Material</th>
                                                    <th>Issue Qty</th>
                                                    <th>Recv.Sloc</th>
                                                    <th>Reason</th>
                                                    <th>Remask</th>
                                                    <th>Status</th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%int i = 1;%>
                                                <%foreach (System.Data.DataRow rows in dtTranfer.Rows)

                                                    {%>
                                                <tr>
                                                    <td><%= i++%></td>
                                                    <td><%=rows["RequestNo"].ToString() %></td>
                                                    <td><%=rows["IssueSloc"].ToString() %></td>
                                                    <td><%=rows["Material"].ToString() %></td>
                                                    <td><%=rows["IssueQty"].ToString() %></td>
                                                    <td><%=rows["RecvSloc"].ToString() %></td>
                                                    <td><%=rows["Reason"].ToString() %></td>
                                                    <td><%=rows["Note"].ToString() %></td>
                                                    <td><%=rows["Status_RQ"].ToString() %></td>

                                                </tr>
                                                <% } %>
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
                                <asp:Button ID="bttUpload" class="btn btn-info" runat="server" Text="Upload" OnClick="bttUpload_Click" />
                                <asp:Button ID="bttTempFile" class="btn btn-info" Text="Download Tempfile" runat="server" />
                                <asp:Button ID="bttPrint"  OnClientClick="openInNewTab()"   CssClass="btn btn-info" Text="Report PDF" runat="server" OnClick="bttPrint_Click" />
                            </div>
                            <div class="pull-right">
                                <div class="btn-group" style="">
                                    <asp:Button ID="bttApproved" CssClass="btn btn-info" runat="server" OnClick="bttApproved_Click" />
                                    &nbsp; 
                                 <asp:Button ID="bttReject" CssClass="btn btn-info" Text="Reject" runat="server" OnClick="bttReject_Click" />
                                    &nbsp; 
                                <asp:Button ID="bttReset" CssClass="btn btn-info" Text="Reset" runat="server" OnClick="bttReset_Click" />
                                    &nbsp; 
                                <button type="button" id="bttComment" onclick="openAdd('<%=hdfRequest.Value.ToString()%>','<%=hdfRoleDeptUpdate.Value.ToString()%>','<%=hdfRoleupdate.Value.ToString()%>','<%=hdfUserUpdate.Value.ToString()%>')" class="btn btn-info">Comment</button>

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
                                        </div>
                                        <div class="panel-body">
                                            <textarea id="txt_Comment" runat="server" class="form-control" placeholder="Write a comment..." rows="3"></textarea>

                                            <br>
                                            <%--onserverclick="bttSend_Click"--%>
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
                <asp:HiddenField ID="hdfRoleDeptUpdate" runat="server" />
                <asp:HiddenField ID="hdfRoleupdate" runat="server" />
                <asp:HiddenField ID="hdfRequest" runat="server" />
                <asp:HiddenField ID="hdfUserUpdate" runat="server" />
                <asp:HiddenField ID="hdfStockIssue" runat="server" />
                <asp:HiddenField ID="hdfStockReice" runat="server" />


            </div>

        </div>
    </div>


    <script type="text/javascript"> 
        function openAdd(lbl_RQ, RoleDept, Role, User) {

            $('#<%=lblRequestUpdate.ClientID%>').html(lbl_RQ);
            $('#<%=hdfRoleDeptUpdate.ClientID%>').html(RoleDept);
            $('#<%=hdfRoleDeptUpdate.ClientID%>').html(Role);
            $('#<%=hdfUserUpdate.ClientID%>').html(User);
            //  debugger;
            $('#myNew').modal('show');
        }


    </script>
</asp:Content>
