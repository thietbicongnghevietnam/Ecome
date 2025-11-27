<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Download.aspx.cs" Inherits="MATERIAL_IN_OUT.Download" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="mailbox-view-area mg-tb-15">

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="hpanel email-compose mailbox-view mg-b-15">
                <div class="data-table-area mg-tb-15">
                    <div class="container-fluid" id="DivPrint" style="line-height: 50px">
                        <div class="row" style="padding-bottom: 50px">
                            <center>
                                <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Font-Underline="true" Font-Size="25px">DOWNLOAD ALL DATA </asp:Label>
                            </center>

                        </div>

                        <div class="row">


                            <label for="TimeDate" class="col-sm-1 col-form-label">From Date</label>
                            <div class="col-sm-2">
                                <div class="input-group date" id="FromDate" data-target-input="nearest">
                                    <asp:TextBox ID="txtDateFrom" runat="server" TextMode="Date" CssClass="custom-select custom-select-sm form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                               <label for="TimeDate" class="col-sm-1 col-form-label">From Date</label>
                            <div class="col-sm-2">
                                <div class="input-group date" id="ToDate" data-target-input="nearest">
                                    <asp:TextBox ID="txtDateTo" runat="server" TextMode="Date" CssClass="custom-select custom-select-sm form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="bttDownload_InOut" runat="server"  OnClick ="bttDownload_InOut_Click" Text="Download IN-Out" CssClass="btn btn-warning" />
                                </div>

                            <div class="col-sm-2">
                                <asp:Button ID="bttDownloadInvoice" runat="server"  OnClick ="bttDownloadInvoice_Click"  Text="Download Invoice" CssClass="btn btn-warning" />
                                </div>


                        </div>
                        <div class="row" style="padding-bottom: 500px">
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
