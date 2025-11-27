<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detail_Commercial_Invoice.aspx.cs" Inherits="MATERIAL_IN_OUT.Commercial_Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                    <div class="container-fluid" id="DivPrint" style ="line-height:20px">
                        <div class="row">
                            <center>  <asp:Label ID ="lblTitle" runat ="server" Font-Bold ="true"  Font-Underline ="true" Font-Size ="25px"> COMMERCIAL INVOICE</asp:Label>
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
                                         <table data-toggle="table" style="height:170px">
                                             <thead>
                                                 <tr style ="background-color:burlywood">
                                                     <th  ><b>BILL- TO PARTRY</b></th>
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
                                                         <asp:Label ID="lblVendorAddress" runat="server"></asp:Label>
                                                         <br />
                                                         <asp:Label ID="lblVendorPICTo" runat="server"></asp:Label>

                                                     </td>
                                                 </tr>
                                             </tbody>

                                         </table>
                                    <b>Plant:<asp:Label ID="lblPlant" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <b>Currency:<asp:Label ID="lblCurrency" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <b>Trade Term
                                        <asp:Label ID="lblTrade" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <b>Freight:<asp:Label ID="lblFeight" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <b>Payment:<asp:Label ID="lblPayment" runat="server"></asp:Label>
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
                                    <table data-toggle="table" style="height: 170px" >
                                        <thead>
                                            <tr style ="background-color:burlywood">
                                                <th ><b>SHIP-TO PARTY</b></th>
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
                                                    <asp:Label ID="lblVendorAddressShip" runat="server"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblVendorPICShip" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>

                                    </table>
                                    <b>Destination: <asp:Label ID="lblDestination" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <b>Carrier: 
                                        <asp:Label ID="lblCarrier" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <b>PO Number: 
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
                                        <table id="table" data-toggle="table">
                                            <thead>
                                                <tr>
                                                    <th data-field="id">Number</th>
                                                    <th>ItemNo./Cust Item No.</th>
                                                    <th>Item Description</th>
                                                    <th>Country of origin</th>
                                                    <th>Total Qty</th>
                                                    <th>Unit Price(USD)</th>
                                                    <th>Amount(USD)</th>
                                                </tr>
                                            </thead>
                                            
                                            <tbody>
                                                <%int i = 1;%>

                                                <%foreach (System.Data.DataRow rows in dtDetailRQ.Rows)
                                                    {%>
                                                <tr>

                                                        <td><%= i++%></td>
                                                    
                                                    <td>
                                                        <%=rows["Item_No"].ToString()%>
                                                    </td>
                                                    <td>
                                                        <%=rows["Item_No"].ToString()%>
                                                    </td>
                                                    <td>
                                                        <%=rows["Destination"].ToString()%>
                                                    </td>
                                                    <td>
                                                        <%=rows["Qty"].ToString()%>
                                                    </td>
                                                    <td>
                                                        <%=rows["Unit_Price"].ToString()%>
                                                    </td>
                                                    <td>
                                                        <%=rows["Amount"].ToString()%>
                                                    </td>
                                                </tr>
                                                <% } %>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <br />
                                    REMARKS: <asp:Label ID ="lblRemark" runat ="server"></asp:Label>
                                    </div>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style ="height:150px">
                                    </div>
                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2"  style ="float:right; font-size:25px;">
                                    <asp:Label ID ="lblAGM_GM"    runat ="server" Text ="AGM/GM" Font-Bold ="true"></asp:Label>
                                    </div>
                                </div>
                             </div>
                    </div>
                    <div class="panel-footer">
                        <div class="btn-group">
                           <div class="col-lg-12"  style ="float:right; font-size:25px;">
                            <input type="button" onclick="printDiv('DivPrint')" class="btn btn-info" value="Print" />
                            &nbsp;&nbsp;
                                <asp:Button ID="bttDownload" CssClass="btn btn-info" Text="Download" runat="server"  OnClick ="bttDownload_Click"/>
                            &nbsp; 
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
