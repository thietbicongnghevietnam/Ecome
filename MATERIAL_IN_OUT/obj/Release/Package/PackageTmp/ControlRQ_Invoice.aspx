<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlRQ_Invoice.aspx.cs" Inherits="MATERIAL_IN_OUT.ControlRQ_Invoice" %>

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
            <div class="row">
                <center> 
                    <asp:Label ID ="lblTitle" runat ="server" Font-Bold ="true"  Font-Underline ="true" Font-Size ="25px"> CONTROL COMMERCIAL INVOICE</asp:Label>
                </center>
            </div>
            <div class="row" style="margin-top: 20px; margin-bottom: 20px;">

                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="float: right">

                    <table class="table table-bordered table-striped table-hover">
                        <tr>
                            <td>Date Voucher (dd/MM/yyyy) 
                                <div class="date-picker-inner">
                                    <div class="form-group data-custon-pick" id="data_2">
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <input id="txtDateVoucher" class="form-control" type="text" placeholder="dd-mm-yyyy" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <label>Dept Name</label>

                                <div class="chosen-select-single">
                                    <asp:DropDownList ID="ddlDept" class="form-control" runat="server" 
                                      OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack ="true" ></asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                 <td>
                                <label>Vendor Code</label>

                                <div class="chosen-select-single">
                                    <asp:DropDownList ID="ddlVendor" class="form-control" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                
                                <div style="margin-top: 25px">
                                    <asp:Button ID="bttSearch" runat="server" class="btn btn-custon-rounded-four btn-primary" Text="Search" OnClick="bttSearch_Click" />

                                    <button type ="button" id="bttCreate" target ="openInNewTab()"  class="btn btn-success"> <i  class="fa fa-edit"></i> Create Invoice  </button>

                                  
                                </div>


                                <asp:HiddenField ID ="hdfListRQ" runat ="server" />
                            </td>
                        </tr>

                    </table>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <asp:Panel ID="P_RQMaterial" runat="server" CssClass="glyphicon-font: tranffic-als-inner"   GroupingText="LIST RQ ISSUE MATERIAL IN-OUT">
                        <div class="datatable-dashv1-list custom-datatable-overright">
                            <table id="listRQ" data-toggle="table" data-pagination="true"  data-show-refresh="true"  data-search="true" >
                                <thead>
                                    <tr>
                                        <th>Check</th>
                                        <th>RQ Material</th>
                                        <th>Vendor</th>
                                        <th>Dept Code</th>
                                        <th>Date Voucher </th>
                                        <th>Funtion</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%foreach (System.Data.DataRow rows in dtListRQ.Rows)
                                        {%>
                                    <tr>
                                        <td>
                                            <input type="checkbox" id="Check_RQ" value="<%=rows["RequestNo"].ToString()%>">
                                        </td>
                                        <td>
                                            <%=rows["RequestNo"].ToString()%>
                                        </td>
                                        <td>
                                            <%=rows["VendorCode"].ToString()%>
                                        </td>
                                        
                                        <td>
                                            <%=rows["CostCenter"].ToString()%>
                                        </td>
                                        <td>
                                            <%=rows["DateVoucher"].ToString()%>
                                        </td>
                                        <td>
                                               <a href="View_RQMaterial_Invoiceaspx.aspx?RQ=<%=rows["RequestNo"].ToString()%>"   target ="openInNewTab()" class="btn btn-info" <i class="fa fa-folder-open-o"></i>Detail RQ</a>
                                        </td>

                                    </tr>
                                    <% } %>
                                </tbody>
                            </table>
                            
                        </div>
                    </asp:Panel>
                </div>

                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <asp:Panel ID="Panel1" runat="server" CssClass="glyphicon-font: tranffic-als-inner" GroupingText="LIST COMMERCIAL INVOICE & PACKING LIST">
                        <div class="datatable-dashv1-list custom-datatable-overright">
                            <table   data-toggle="table" data-pagination="true" data-search="true" >
                                <thead>
                                    <tr>
                                       
                                        <th>Invoice No</th>
                                        <th>Date Invoice</th>
                                        <th>View Invoice</th>
                                        <th>View Pack List</th>
                                    </tr>
                                </thead>
                                <tbody>
                                     
                                    <%foreach (System.Data.DataRow rows in dtInvoice.Rows)
                                        {%>
                                    <tr>
                                       
                                        <td>
                                            <%=rows["Invoice_No"].ToString()%>
                                        </td>
                                         <td>
                                            <%=rows["Date_Invoice"].ToString()%>
                                        </td>
                                      
                                        <td>
                                            <a href="Detail_Commercial_Invoice.aspx?Invoice=<%=rows["Invoice_No"].ToString()%>"   target ="openInNewTab()" class="btn btn-success"><i class="fa fa-folder-open-o"></i>Detail Invoice</a>
                                        </td>
                                        <td>
                                            <a href="PackingList.aspx?Invoice=<%=rows["Invoice_No"].ToString()%>"   target ="openInNewTab()" class="btn btn-success"><i class="fa fa-folder-open-o"></i>PACKING LIST </a>
                                        </td>

                                    </tr>
                                    <% } %>
                                </tbody>
                            </table>
                        </div>
                    </asp:Panel>

                </div>


            </div>
        </div>
    </div>
       
     <script src="Scripts/jquery.min.js"></script>

      <script type="text/javascript"> 
          
          $("#bttCreate").click(function () {

              //if ("#listRQ input[type=checkbox]:checked") {
              //    {
              //        if (document.getElementById('Check_RQ').checked) {
              if ("#listRQ input[type=checkbox]:checked") {
                              var message = "";
                              var ketqua = "";
                              debugger;
                              //Loop through all checked CheckBoxes in GridView.
                              $("#listRQ input[type=checkbox]:checked").each(function () {
                                  message += $(this).closest('tr').find('input:checkbox').val() + ",";
                                  //message += "\n";                
                              });
                              ketqua = message.substr(0, message.length - 1)

                              //window.location.href = "Create_Invoice.aspx?RQ=" + ketqua;
                              //         window.open(window.location.href = "Create_Invoice.aspx?RQ=" + ketqua), '_blank');
                              window.open("Create_Invoice.aspx?RQ=" + ketqua, '_blank');
                              return false;
                          }
                      //}
                    else
                 {
                        toastr.error('Please you check RequestID before create Commercial Invoice.');
                    return false;

                 }
             
                 
             
              
              
             

          });
          </script>  


</asp:Content>
