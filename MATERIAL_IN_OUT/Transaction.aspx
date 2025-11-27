<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transaction.aspx.cs" Inherits="MATERIAL_IN_OUT.Transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mailbox-view-area mg-tb-15">



        <div class="container-fluid">

            <div class="row">
                <h1>
                    <center> TRANSACTION REQUEST</center>
                </h1>

                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-10" style="float: right; margin-right: 30px">


                    <table class="table table-bordered table-striped table-hover">
                        <tr>
                            <th></th>
                            <th colspan="2" style="text-align: center">
                                <h4>Issue Dept</h4>
                            </th>
                            <th colspan="2" style="text-align: center">
                                <h4>Section Incharge

                                </h4>
                            </th>

                        </tr>
                        <tr>
                            <th></th>
                            <th>Charge by</th>
                            <th>Approved by</th>
                            <th>Check by</th>
                            <th>Approved by</th>

                        </tr>

                        <tbody>
                            <tr>
                                <td style="height: 41px">
                                    <h5>Status</h5>
                                </td>
                                <td style="height: 41px">
                                    <asp:Image ID="img_IssueCharge" runat="server" /></td>
                                <td style="height: 41px">
                                    <asp:Image ID="img_IssueApproved" runat="server" /></td>
                                <td style="height: 41px">
                                    <asp:Image ID="img_SectionCheck" runat="server" /></td>
                                <td style="height: 41px">
                                    <asp:Image ID="img_SectionApproved" runat="server" /></td>



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

                            </tr>
                            <tr>
                                <td>
                                    <h5>Date</h5>
                                </td>
                                <td>
                                    <asp:Label ID="lblIssueCharge_Date" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblIssueApproved_Date" runat="server"> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSectionCheck_Date" runat="server"> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSectionApproved_Date" runat="server"> </asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="float: left; margin-left: 30px">

                    <table class="table table-bordered table-striped table-hover">
                        <tbody>

                            <tr>
                                <td>RequestNo
                                            
                                </td>

                                <td>
                                    <asp:Label ID="lblRequestNo" runat="server"></asp:Label>

                                </td>


                            </tr>
                            <tr>

                                <td>Issue Date (dd/MM/yyyy)
                                </td>
                                <td>
                                    <asp:Label ID="lblIssueDate" runat="server"></asp:Label>

                                </td>

                            </tr>
                            <tr>
                                <td>Slip No</td>
                                <td>
                                    <asp:Label ID="lblSlipNo" runat="server"></asp:Label>

                                </td>



                            </tr>
                            <tr>
                                <td>Tran.Code </td>
                                <td>
                                    <asp:Label ID="lblTranCode" runat="server"></asp:Label>

                                </td>


                            </tr>
                            <tr>
                                <td>Mv.Type</td>
                                <td>
                                    <asp:Label ID="lblMvType" runat="server"></asp:Label>

                                </td>


                            </tr>

                        </tbody>
                    </table>

                </div>

            </div>
            <div class="row">
            </div>
            <div class="col-md-3 col-md-3 col-sm-3 col-xs-12" style="left: 0px; top: 0px">

                <div class="panel-body">
                    <a class="btn btn-success compose-btn btn-block m-b-md">Control Request Transaction</a>
                    <div class="sparkline10-graph">
                        <div class="adminpro-content">

                            <asp:TreeView ID="TreeView1" runat="server">
                            </asp:TreeView>
                        </div>
                    </div>

                    <hr>
                    <div class="sparkline10-graph">
                        <div class="adminpro-content">
                            <asp:TreeView ID="TreeView2" runat="server">
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
                                    <div class="sparkline13-list">
                                        <div class="sparkline13-hd">
                                            <div class="main-sparkline13-hd">
                                                <h1>Products <span class="table-project-n">Data</span> Table</h1>
                                            </div>
                                        </div>
                                        <div class="sparkline13-graph">
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

                                                            <th data-field="id">ID</th>
                                                            <th data-field="name" data-editable="false">Product Title</th>
                                                            <th data-field="company" data-editable="false">Stock</th>
                                                            <th data-field="price" data-editable="false">Price</th>
                                                            <th data-field="date" data-editable="false">Date</th>
                                                            <th data-field="task" data-editable="false">Status</th>
                                                            <th data-field="email" data-editable="false">Total Sales</th>
                                                            <th data-field="action">Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>

                                                            <td>1</td>
                                                            <td>Product Title</td>
                                                            <td>Out Of Stock</td>
                                                            <td>$54</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>2</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>3</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>4</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>5</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>6</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>7</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>8</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>9</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>10</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>11</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>12</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>13</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>14</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>15</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>16</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>17</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>18</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>19</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>20</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock hhhhhh</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>21</td>
                                                            <td>Product Title</td>
                                                            <td>In Of Stock</td>
                                                            <td>$5</td>
                                                            <td>Jul 14, 2017</td>
                                                            <td>Active</td>
                                                            <td>$700</td>
                                                            <td class="datatable-ct"><i class="fa fa-check"></i>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer text-right">
                        <div class="btn-group">
                            <asp:FileUpload ID="FileUpload1" class="btn btn-default" runat="server" />
                            &nbsp;
                            <asp:Button ID="bttUpload" runat="server" class="btn btn-default" Text="Upload" />
                            <button class="btn btn-default"><i class="fa fa-reply"></i>Reject</button>
                            <button class="btn btn-default"><i class="fa fa-arrow-right"></i>Approved</button>
                            <button class="btn btn-default"><i class="fa fa-print"></i>Print</button>
                            
                        </div>
                    </div>


                </div>
            </div>
        </div>



    </div>


</asp:Content>
