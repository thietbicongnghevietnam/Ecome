<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PackingList.aspx.cs" Inherits="MATERIAL_IN_OUT.PackingList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function openInNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () { window.document.forms[0].target = ''; }, 0);
        }
    </script>
    <div class="mailbox-view-area mg-tb-15">

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="hpanel email-compose mailbox-view mg-b-15">
                <div class="data-table-area mg-tb-15">
                    <div class="container-fluid" id="DivPrint" style="line-height: 20px">
                        <div class="row">
                            <center>  <asp:Label ID ="lblTitle" runat ="server" Font-Bold ="true"  Font-Underline ="true" Font-Size ="25px">PACKING LIST</asp:Label>
                                    </center>
                        </div>
                        <div class="row">

                            <div class="col-md-6 col-md-6 col-sm-6 col-xs-12" style="left: 0px; top: 0px">
                                <div class="panel-body">
                                    <b>PANASONIC SYSTEM NETWORK VIET NAM CO.,LTD</b>
                                    <br />
                                    LotJ1/2 Thang Long Industrial Park, Dong Anh district, Hanoi, Vietnam<br />
                                    Tel: 84-24-395500057    &nbsp; &nbsp; &nbsp; Fax : 84-24-39550097
                                        <br />
                                    Tax No: 010182423-001
                                    <br />
                                    <table data-toggle="table" style="height: 170px">
                                        <thead>
                                            <tr style="background-color: burlywood">
                                                <th><b>BILL- TO PARTRY</b></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td><b>
                                                    <asp:Label ID="lblvendor_To" runat="server"></asp:Label>
                                                </b>
                                                    <br />
                                                    <asp:Label ID="lblVendorName" runat="server"></asp:Label>
                                                    <br />
                                                  <b> Address:</b> &nbsp;  <asp:Label ID="lblVendorAddress" runat="server"></asp:Label>
                                                    <br />
                                                   <b>Attn:</b> &nbsp; <asp:Label ID="lblVendorPICTo" runat="server"></asp:Label>

                                                </td>
                                            </tr>
                                        </tbody>

                                    </table>
                                    <b>Plant: &nbsp;<asp:Label ID="lblPlant" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <b>Currency: &nbsp;<asp:Label ID="lblCurrency" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <b>Trade Term &nbsp;
                                        <asp:Label ID="lblTrade" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <b>Freight:&nbsp;<asp:Label ID="lblFeight" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <b>Payment: &nbsp;<asp:Label ID="lblPayment" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <br />
                                </div>
                            </div>
                            <div class="col-md-6 col-md-6 col-sm-6 col-xs-12" style="left: 0px; top: 0px">
                                <div class="panel-body">
                                    <b>No:<asp:Label ID="lblInvoiceNo" runat="server"></asp:Label></b>
                                    <br />
                                    Date:
                                    <asp:Label ID="lblDate" runat="server"></asp:Label><br />
                                    Description:<asp:Label ID="lblDescription" runat="server"></asp:Label><br />
                                    Note:<asp:Label ID="lblNote" runat="server"></asp:Label>
                                    <br />
                                    <table data-toggle="table" style="height: 170px">
                                        <thead>
                                            <tr style="background-color: burlywood">
                                                <th><b>SHIP-TO PARTY</b></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td><b>
                                                    <asp:Label ID="lblVendorCodeShip" runat="server"></asp:Label>
                                                </b>
                                                    <br />
                                                     <asp:Label ID="lblVendorNameShip" runat="server"></asp:Label>
                                                    <br />
                                                 <b>Address:</b> &nbsp; <asp:Label ID="lblVendorAddressShip" runat="server"></asp:Label>
                                                    <br />
                                                  <b>Attn: </b> &nbsp; <asp:Label ID="lblVendorPICShip" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>

                                    </table>
                                    <b>Destination:&nbsp;
                                        <asp:Label ID="lblDestination" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <b>Carrier: &nbsp;
                                          <asp:Label ID="lblCarrier" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <b>PO Number: &nbsp;
                                        <asp:Label ID="lblPONo" runat="server"></asp:Label></b>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="datatable-dashv1-list custom-datatable-overright">
                                        <asp:Repeater ID="rptData" runat="server"  >
                                            <HeaderTemplate>
                                               
                                                    <table id="dtPackList" data-toggle="table">
                                                        <tr >
                                                            <th data-field="id">Number</th>
                                                            <th>ItemNo</th>
                                                            <th>Item Description</th>
                                                            <th>Country of origin</th>
                                                            <th>Qty</th>
                                                            <th>NO. OF CTN</th>
                                                            <th>NO OF PALLET</th>
                                                            <th>NET WGT(KG)</th>
                                                            <th>GRS WGT(KG)</th>
                                                            <th>CTN NO</th>
                                                            <th>Dimensions 
                                                         (L*W*H)</th>
                                                        </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                              
                                                <tr>
                                                    <td>  <%# Eval("No") %></td>
                                                    
                                                    <td>
                                                      <asp:Label ID ="lblItemNo"  Text ='<%# Eval("Item_No") %>' runat ="server"></asp:Label>
                                                        
                                                    </td>
                                                    <td>
                                                        <%# Eval("Item_Decription") %>
                                                        
                                                    </td>
                                                    <td>
                                                       <%# Eval("CountryofOrigin") %>
                                                        
                                                    </td>
                                                    <td>
                                                        <%# Eval("Qty") %>
                                                    </td>

                                                    <td>
                                                        <div class="form-outline">
                                                            <asp:TextBox ID="txtNO_OF_CTN" CssClass="form-control" Text='<%#Eval("NO_OF_CTN")%>' runat="server"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="form-outline">
                                                            <asp:TextBox ID="txtNO_OF_PALLET" CssClass="form-control" Text='<%#Eval("NO_OF_PALLET")%>' runat="server"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="form-outline">
                                                            <asp:TextBox ID="txtNET_WGT_KG" CssClass="form-control" Text='<%#Eval("NET_WGT_KG")%>'  runat="server"></asp:TextBox>

                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="form-outline">
                                                            <asp:TextBox ID="txtGRS_WGT_KG" CssClass="form-control" Text='<%#Eval("GRS_WGT_KG")%>'  runat="server"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="form-outline">
                                                            <asp:TextBox ID="txtCTN_NO" CssClass="form-control" Text='<%#Eval("CTN_NO")%>' runat="server"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="form-outline">
                                                            <asp:TextBox ID="txtDimensions_L" CssClass="form-control" Text='<%#Eval("DimensionsL*W*H")%>'  runat="server"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                           
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>


                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <br />
                                    REMARKS:
                                    <asp:Label ID="lblRemark" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="height: 150px">
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2" style="float: right; font-size: 25px;">
                                    <asp:Label ID="lblAGM_GM" runat="server" Text="AGM/GM" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <div class="btn-group">
                        <div class="col-lg-12"  style ="float:right; font-size:25px;">
                            <asp:FileUpload ID ="fUpload" CssClass="btn btn-default"  runat ="server" />
                                  &nbsp;
                                <asp:Button ID="bttUpload" runat="server" Text="Upload" CssClass="btn btn-info"  OnClick ="bttUpload_Click" />
                                  &nbsp;
                                <asp:Button ID="bttTemUpload" runat="server" Text="Tem File Upload" CssClass="btn btn-info"  OnClick ="bttTemUpload_Click" />
                                  &nbsp;
                                 <input type="button" id ="bttPrint" onclick="printDiv('DivPrint')" class="btn btn-info" value="Print" />
                                 &nbsp;
                                <asp:Button ID="bttDownload" CssClass="btn btn-info" Text="Download" runat="server" OnClick="bttDownload_Click" />
                                  &nbsp;
                                <asp:Button ID="bttSave" runat="server" Text="Save" CssClass="btn btn-info" OnClick="bttSave_Click" />
                             
                                </div>
                        </div>
                    </div>

                    <%-- Modal footer --%>
                </div>
            </div>



        </div>

    </div>


    <script type="text/javascript"> 
        function printDiv(divName) {
            //if (document.getElementById("status") != null) {
            //    var idPost = document.getElementById("status").innerHTML;
            //}
            debugger;
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
         
    </script>

</asp:Content>
