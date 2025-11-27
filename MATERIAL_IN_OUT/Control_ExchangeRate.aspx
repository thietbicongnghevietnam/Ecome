<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Control_ExchangeRate.aspx.cs" Inherits="MATERIAL_IN_OUT.Control_ExchangeRate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="mailbox-view-area mg-tb-15">

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="hpanel email-compose mailbox-view mg-b-15">
                <div class="data-table-area mg-tb-15">
                    <div class="container-fluid" id="DivPrint" style="line-height: 50px">
                        <div class="row" style="padding-bottom: 50px">
                            <center>
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Font-Underline="true" Font-Size="25px">CONTROL INPUT EXCHANGE</asp:Label>
                            </center>

                        </div>

                        <div class="row">


                            <label for="TimeDate" class="col-sm-1 col-form-label">Ngày Nhập</label>
                            <div class="col-sm-2">
                                <div class="input-group date" id="Datetime" data-target-input="nearest">
                                    <asp:TextBox ID="txtDate" runat="server" TextMode="Date" CssClass="custom-select custom-select-sm form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>

                            <label for="TypeExchange" class="col-sm-1 col-form-label">Loại tiền</label>
                            <div class="col-sm-2">

                                <asp:DropDownList runat="server" ID="ddlExchange"
                                    class="form-control select2bs4" Style="width: 100%;"
                                    AppendDataBoundItems="true"
                                    DataTextField="Currency"
                                    DataValueField="Currency"
                                    CssClass="custom-select custom-select-sm form-control form-control-sm">
                                </asp:DropDownList>
                            </div>


                            <label for="ExchangeInput" class="col-sm-1 col-form-label">Nhập tiền</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtExchangeInput" runat="server" CssClass="form-control" Style="height: 38px; width: 100%; text-align: center; font-weight: bold;"></asp:TextBox>
                            </div>

                        </div>
                        <div class="row" style="text-align: right">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            </div>

                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                <asp:Button ID="bttSave" runat="server" OnClick="bttSave_Click" Text="SAVE" CssClass="btn btn-warning" />
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            </div>
                        </div>
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
                                                <th>Exchange Unit</th>
                                                <th>Exchange Rate</th>
                                                <th>Date</th>
                                                <th>User Input</th>
                                                <th>Date Input</th>


                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater runat="server" ID="prtExchange">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#Eval("No") %></td>
                                                        <td><%#Eval("Currency") %></td>
                                                        <td><%#Eval("Rate") %></td>
                                                        <td><%#Eval("Date_Date") %></td>
                                                        <td><%#Eval("User_Insert") %></td>
                                                        <td><%#Eval("Insert_Date") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtid').prop("readonly", true);
            $('#txtmaterial').prop("readonly", true);
        });
        $(function () {
            //Datemask dd/mm/yyyy
            $('#datemask').inputmask('dd-mm-yyyy', { 'placeholder': 'dd-mm-yyyy' });
            //Datemask2 mm/dd/yyyy
            $('#datemask2').inputmask('mm-dd-yyyy', { 'placeholder': 'dd-mm-yyyy' });
            //Money Euro
            $('[data-mask]').inputmask()
            //Date range picker
            $('#Dateinput').datetimepicker({
                format: 'DD-MM-YYYY'
            });

        });


        function openInNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () { window.document.forms[0].target = ''; }, 0);
        }
    </script>
</asp:Content>
