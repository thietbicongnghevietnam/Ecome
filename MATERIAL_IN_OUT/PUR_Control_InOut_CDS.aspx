<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PUR_Control_InOut_CDS.aspx.cs" Inherits="MATERIAL_IN_OUT.PUR_Control_InOut_CDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function openInNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () { window.document.forms[0].target = ''; }, 0);
        }
    </script>
    
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div class="hpanel email-compose mailbox-view mg-b-15">
            <div class="data-table-area mg-tb-15">
                <div class="container-fluid" id="DivPrint" style="line-height: 20px">
                    <div class="row" style="padding-bottom: 30px">
                        <center>
                            <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Font-Underline="true" Font-Size="25px">Control RQ In-Out Material out SAP</asp:Label>
                        </center>
                    </div>
                    <div class="col-md-3 col-md-3 col-sm-3 col-xs-12" style="left: 0px; top: 0px">

                <div class="panel-body">
                     <a class="btn btn-success compose-btn btn-block m-b-md">Control Request OutSAP</a>
                    
                    <asp:TreeView ID="treeRQ_OutSap" runat="server" ForeColor="Blue"  OnSelectedNodeChanged  ="treeRQ_OutSap_SelectedNodeChanged" >

                    </asp:TreeView>
               
                  
                  </div>
                </div>
                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">

                    <div class="datatable-dashv1-list custom-datatable-overright">
                        <table id="table" data-toggle="table" data-pagination="true" data-search="true" data-show-columns="true" data-show-pagination-switch="true" data-show-refresh="true" data-key-events="true" data-show-toggle="true" data-resizable="true" data-cookie="true"
                            data-cookie-id-table="saveId" data-show-export="true" data-click-to-select="true" data-toolbar="#toolbar">
                            <thead>
                                <tr>
                                    <th>Request No</th>
                                    <th>Số TK</th>
                                    <th>Ngày TK(dd/MM/yyyy)</th>
                                    <th>Funtion</th>

                                </tr>
                            </thead>
                            <tbody>

                                <asp:Repeater runat="server" ID="prtLoadRQ_CD" OnItemCommand="prtLoadRQ_CD_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                              
                                            <td>
                                                <a href='View_RQMaterial_Invoice.aspx?RQ=<%#Eval("RequestNo").ToString()%>' target="openInNewTab()"><i class="fa fa-folder-open-o"></i>
                                                    <asp:Label ID="lblRQ" runat="server" Text='<%#Eval("RequestNo") %>'></asp:Label>
                                                </a>
                                            </td>
                                            <td>
                                                <div class="form-outline">
                                                    <asp:TextBox ID="txtCDNO" CssClass="form-control" Text='<%#Eval("CDS_No")%>' runat="server"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-outline">

                                                    <%--<asp:TextBox ID="txtCDSDate" CssClass="form-control" Text='<%# string.IsNullOrEmpty(Eval("CDS_Date").ToString())?string.Empty:Convert.ToDateTime(Eval("CDS_Date")).ToString("dd/MM/yyyy")%>' runat="server" TextMode="Date"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtCDSDate" CssClass="form-control" Text='<%# string.IsNullOrEmpty(Eval("CDS_Date").ToString())?string.Empty:Eval("CDS_Date")%>' runat="server" ></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:LinkButton runat="server" ID="bttUpdate" CssClass="btn btn btn-primary" OnClick="bttUpdate_Click" CommandArgument='<%#Eval("RequestNo") %>' ToolTip="Save" Visible='<%#Eval("Status_Approved_OutSAP").ToString()=="Pending"?true:false %>'><i class="fa fa-edit"></i> Save</asp:LinkButton>
                    
                                                <asp:LinkButton runat="server" ID="bttApproved"   CssClass="btn btn-warning" OnClick="bttApproved_Click" CommandArgument='<%#Eval("RequestNo") %>' ToolTip="Approved" Visible='<%#Eval("CDS_No").ToString() != "" && Eval("Status_Approved_OutSAP").ToString()!="Approved"?true:false %>' OnClientClick="return confirm('Are you sure you want to Approved RQ?')"><i class="fa fa-edit"></i> Approved</asp:LinkButton>

                                                <asp:LinkButton runat="server" ID="bttPrintPDF" CssClass="btn btn-info"  OnClick ="bttPrintPDF_Click"  OnClientClick="openInNewTab()"  CommandArgument='<%#Eval("RequestNo") %>' ToolTip="Print PDF" ><i class="fa fa-edit"></i> Print PDF</asp:LinkButton>

                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                        </table>
                    </div>


                </div>
                    </div>
            </div>
        </div>
    </div>
</asp:Content>
