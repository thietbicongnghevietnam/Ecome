<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReviseInvoice.aspx.cs" Inherits="MATERIAL_IN_OUT.ReviseInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function openInNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () { window.document.forms[0].target = ''; }, 0);
        }
    </script>
    <style type="text/css">
        .textbox {
            /*display: block;*/
            display: inline;
            width: 80%;
            height: 34px;
            padding: 3px 6px;
            margin-bottom: 5px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s
        }

        .Area {
            /*display: block;*/
            width: 80%;
            height: 70px;
            padding: 3px 6px;
            margin-bottom: 5x;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s
        }

        .tittle {
            width: 50px;
            height: 34px;
            margin-bottom: 5px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
        }

        .lable {
            /*display: block;*/
            width: 80%;
            height: 34px;
            padding: 3px 6px;
            margin-bottom: 10px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
        }

        .droplist {
            /*display: block;*/
            width: 50%;
            height: 34px;
            padding: 3px 6px;
            margin-bottom: 5px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
        }
    </style>
    <div class="mailbox-view-area mg-tb-15">
        <div class="container-fluid">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="hpanel email-compose mailbox-view mg-b-15">
                    <div class="data-table-area mg-tb-15">
                        <div class="container-fluid">
                            <div class="row">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close" style="font-size: 20px">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="row">
                                <center>
                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Underline="true" Font-Size="25px"> COMMERCIAL INVOICE</asp:Label>
                                    <br />
                                    <p style="color: brown">(RQ Material:<asp:Label ID="lblRQ_Show" runat="server" Font-Size="Small"></asp:Label>) </p>
                                </center>
                            </div>
                            <div class="row">
                                <div class="col-md-6 col-md-6 col-sm-6 col-xs-12" style="left: 0px; top: 0px">
                                    <div class="panel-body">
                                        <div class="col-75">
                                            <b>PANASONIC SYSTEM NETWORK VIET NAM CO.,LTD</b>
                                        </div>

                                        <div class="col-75">
                                            LotJ1/2 Thang Long Industrial Park, Dong Anh district, Hanoi, Vietnam<br />
                                        </div>
                                        <div class="col-75">
                                            Tel: 84-24-395500057    &nbsp; &nbsp; &nbsp; Fax : 84-24-39550097
                                        </div>
                                        <div class="col-75">
                                            Tax No: 010182423-001
                                        </div>
                                        <br />

                                        <table data-toggle="table">
                                            <thead>
                                                <tr>
                                                    <th><b>BILL- TO PARTY</b></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <b>
                                                            <asp:Label ID="lblVendorCode_To" CssClass="lable" runat="server"></asp:Label>
                                                        </b>
                                                        <div class="col-75">
                                                            <asp:Label ID="lblNameVendor_to" CssClass="lable" runat="server"></asp:Label><br />
                                                        </div>
                                                        <div class="col-75">
                                                            <asp:Label ID="lblAddress" runat="server" CssClass="tittle" Font-Bold="true" Text="Address: "></asp:Label>
                                                        </div>
                                                        <div class="col-75">
                                                            <asp:Label ID="lblAddressVendor_To" CssClass="lable" runat="server"></asp:Label><br />
                                                        </div>
                                                        <div class="col-75">
                                                            <asp:Label ID="lblPIC_vendorTo" CssClass="lable" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-25">
                                                            <b>Attn:</b>
                                                        </div>
                                                        <div class="col-75">
                                                            <asp:TextBox ID="txtAttn_BIllTO" class="textbox" runat="server"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>

                                        </table>


                                        <div class="col-25">
                                            <asp:Label runat="server" Font-Bold="true" Text="Plant:"></asp:Label>
                                        </div>
                                        <div class="col-75">
                                            <asp:Label ID="lblPlant" Font-Bold="true" CssClass="lable" runat="server"></asp:Label>
                                        </div>

                                        <div class="col-25">
                                            <asp:Label runat="server" Font-Bold="true" CssClass="tittle" Text="Current:"></asp:Label>
                                        </div>
                                        <div class="col-75">
                                            <asp:DropDownList ID="ddlCurrency" CssClass="droplist" runat="server"></asp:DropDownList>
                                        </div>

                                        <div class="col-25">
                                            <asp:Label runat="server" Font-Bold="true" CssClass="tittle" Text="TRADE TERMS :"></asp:Label>

                                        </div>
                                        <div class="col-75">
                                            <asp:DropDownList ID="ddlTrade_Tearm" CssClass="droplist" runat="server"></asp:DropDownList>
                                        </div>

                                        <div class="col-25">
                                            <asp:Label runat="server" Font-Bold="true" CssClass="tittle" Text="Feight:"></asp:Label>
                                        </div>
                                        <div class="col-75">
                                            <asp:DropDownList ID="ddlFeight" CssClass="droplist" runat="server"></asp:DropDownList>
                                        </div>

                                        <div class="col-25">
                                            <asp:Label runat="server" Font-Bold="true" CssClass="tittle" Text="PAYMENT"></asp:Label>
                                        </div>
                                        <div class="col-75">
                                            <asp:DropDownList ID="ddlPayment" CssClass="droplist" runat="server"></asp:DropDownList>
                                        </div>

                                        <div class="col-25">
                                            <b>PO Number:</b>
                                        </div>
                                        <div class="col-75">
                                            <asp:TextBox ID="txtPONumber" class="textbox" runat="server"></asp:TextBox>
                                        </div>

                                        <br />

                                    </div>


                                </div>
                                <div class="col-md-6 col-md-6 col-sm-6 col-xs-12" style="left: 0px; top: 0px">
                                    <div class="panel-body">

                                        <div class="col-75">
                                            <asp:Label runat="server" Font-Bold="true" CssClass="tittle" Text="NO:" ForeColor="Blue" Font-Size="18px"></asp:Label>
                                            <asp:Label ID="lblInvoiceNO" Font-Bold="true" CssClass="lable" ForeColor="Blue" Font-Size="18px" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdfInvoicePrint" runat="server" />
                                        </div>


                                        <div class="col-75">
                                            <asp:Label runat="server" Font-Bold="true" CssClass="tittle" Text="Date:"></asp:Label>
                                            <asp:Label ID="lblDate" Font-Bold="true" CssClass="lable" runat="server"></asp:Label>
                                        </div>

                                        <div class="col-25">
                                            <asp:Label runat="server" Font-Bold="true" CssClass="tittle" Text=" Description::"></asp:Label>
                                        </div>
                                        <div class="col-75">
                                            <asp:DropDownList ID="ddlDescription" CssClass="textbox" runat="server"></asp:DropDownList>
                                        </div>

                                        <div class="col-25">
                                            <asp:Label runat="server" Font-Bold="true" CssClass="tittle" Text=" Note:"></asp:Label>
                                        </div>
                                        <div class="col-75">
                                            <asp:TextBox ID="txtNote" Font-Bold="true" class="textbox" runat="server" AutoPostBack="true" OnTextChanged="txtNote_TextChanged"></asp:TextBox>
                                        </div>


                                        <div class="col-25">
                                            <asp:Label runat="server" Font-Bold="true" CssClass="tittle" Text="  Tax No:"></asp:Label>
                                        </div>
                                        <div class="col-75">
                                            <asp:TextBox ID="txtTax" class="textbox" runat="server"></asp:TextBox>
                                        </div>

                                        <table data-toggle="table">
                                            <thead>
                                                <tr>
                                                    <th><b>SHIP-TO PARTY</b></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <div class="col-25">
                                                            <asp:Label runat="server" Font-Bold="true" CssClass="tittle" Text="Name:"></asp:Label>
                                                        </div>
                                                        <div class="col-75">
                                                            <asp:TextBox ID="txtVendor_Shipment" class="textbox" runat="server"></asp:TextBox>
                                                        </div>


                                                        <div class="col-25">
                                                            <asp:Label runat="server" Font-Bold="true" CssClass="tittle" Text="  Addrees:"></asp:Label>
                                                        </div>
                                                        <div class="col-75">
                                                            <textarea id="txtAddress_Shipment" class="Area" runat="server"></textarea>
                                                        </div>




                                                        <div class="col-25">
                                                            <asp:Label runat="server" Font-Bold="true" CssClass="tittle" Text="Attn: "></asp:Label>
                                                        </div>
                                                        <div class="col-75">
                                                            <asp:TextBox ID="txtAnt_Shipment" class="textbox" runat="server"></asp:TextBox><br />
                                                        </div>

                                                    </td>

                                                </tr>
                                            </tbody>

                                        </table>
                                        <div class="col-25">
                                            <b>Destination</b>
                                        </div>
                                        <div class="col-75">
                                            <asp:TextBox ID="txtDestination" Font-Bold="true" class="textbox" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-25">
                                            <b>Carrier</b>
                                        </div>
                                        <div class="col-75">
                                            <asp:TextBox ID="txtCarrier" class="textbox" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-25">
                                            <asp:Label runat="server" Font-Bold="true" CssClass="tittle" Text="Remark:"></asp:Label>
                                        </div>
                                        <div class="col-75">
                                            <asp:TextBox ID="txtRemark" class="textbox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="datatable-dashv1-list custom-datatable-overright">
                                        <table id="table" data-toggle="table" data-pagination="true" data-show-export="true" data-toolbar="#toolbar">
                                            <thead>
                                                <tr>
                                                    <th data-field="id">Number</th>
                                                    <th>Request No</th>
                                                    <th>ItemNo./Cust Item No.</th>
                                                    <th>Item Description</th>
                                                    <th>Country of origin</th>
                                                    <th>Total Qty</th>
                                                    <th>Unit Price(USD)</th>
                                                    <th>Amount(USD)</th>
                                                </tr>
                                            </thead>
                                            <%int i = 1;%>
                                            <tbody>

                                                <%foreach (System.Data.DataRow rows in dtListCreateRQ.Rows)
                                                    {%>
                                                <tr>

                                                    <td><%= i++%></td>

                                                    <td>
                                                        <%=rows["Refer_RQ_Material"].ToString()%>
                                                    </td>
                                                    <td>
                                                        <%=rows["Item_No"].ToString()%>
                                                    </td>
                                                    <td>
                                                        <%=rows["Item_Decription"].ToString()%>
                                                    </td>
                                                    <td>
                                                        <%=rows["CountryofOrigin"].ToString()%>
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
                        <div class="panel-footer">
                            <div class="btn-group">
                                <div class="col-lg-12" style="float: left; font-size: 25px;">
                                    <asp:Button ID="bttDownload" CssClass="btn btn-info" Text="Download" runat="server" OnClick="bttDownload_Click" />
                                    &nbsp;
                                  <asp:Button ID="bttExportInvoice" runat="server" Text="PDF Invoice" CssClass="btn btn-info" OnClick="bttExportInvoice_Click" />
                                    &nbsp;
                                  <asp:Button ID="bttSaveInvoice" CssClass="btn btn-info" Text="Save Invoice" runat="server" OnClick="bttSaveInvoice_Click" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="Scripts/jquery.min.js"></script>
</asp:Content>
