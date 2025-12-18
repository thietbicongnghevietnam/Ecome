<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlPriceSAP.aspx.cs" Inherits="MATERIAL_IN_OUT.ControlPriceSAP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <script type="text/javascript">
      $(document).ready(function () {
          $('#dataTable').DataTable({
              //dom: 'Bfrtip',
              //buttons: [
              //    'copyHtml5',
              //    'excelHtml5',
              //    'csvHtml5',
              //    'pdfHtml5',
              //    'print'
              //],
              "responsive": true,
              "autoWidth": false,
              "pageLength": 50
          });
      });

      </script>



      <div class="form-group">
      <div class="container">
          <div class="row">
              <%--<h3><%: Page.Title %></h3>--%>
              <h3>Upload Price SAP</h3>

              <div class="col-md-2">
              </div>

          </div>
          <p style="color: blue;">
              <asp:Label ID="lblConfirm" Text="" runat="server"></asp:Label>
          </p>
      </div>
  </div>
  <div class="card mb-4">
      <div class="card-body">
          <div class="dataTables_wrapper dt-bootstrap4">
              <%-- <div class="table-responsive">--%>
              <div class="col-sm-12">
                  <div class="col-md-1" style="float: left">
                      <b>From Date:</b>
                      <asp:TextBox ID="txtDate" runat="server" CssClass="custom-select custom-select-sm form-control form-control-sm"></asp:TextBox>
                  </div>
                  <div class="col-md-1" style="float: left">
                      To Date:
              <asp:TextBox ID="txtDate2" runat="server" CssClass="custom-select custom-select-sm form-control form-control-sm"></asp:TextBox>
                  </div>

                  <div class="col-md-1" style="float: left">
                      <div class="form-group">
                          <label for="Group">Filter Model</label>
                         <%-- <asp:DropDownList ID="dr_filter_model" runat="server"
                              AppendDataBoundItems="true"
                              DataTextField="STR_PROCESS_FACTORY"
                              DataValueField="STR_PROCESS_FACTORY"
                              CssClass="custom-select custom-select-sm form-control form-control-sm" />--%>
                          <%--AutoPostBack="True" OnSelectedIndexChanged="ddr_filter_Plan_SelectedIndexChanged"--%>
                      </div>
                  </div>

                  <div class="col-md-1" style="float: left">
                      <div class="form-group">
                          <label for="Group">Filter Cate</label>
                          <%--<asp:DropDownList ID="dr_filter_Cate" runat="server"
                              AppendDataBoundItems="true"
                              DataTextField="cat"
                              DataValueField="cat"
                              CssClass="custom-select custom-select-sm form-control form-control-sm" OnSelectedIndexChanged="dr_filter_Plan_SelectedIndexChanged" AutoPostBack="True" />--%>
                      </div>
                  </div>

                  <asp:Button ID="Button7" runat="server" Text="Filter" class="btn btn-primary" OnClick="Search_Date_Click"
                      Style="margin-left: 20px; margin-right: 20px;" />

                  <span style="padding-left: 20px;"></span>
                  <asp:Button ID="Button1" runat="server" Text="Export excel" CssClass="btn btn-primary" OnClick="btnXuatExcel_Click" />


                  <div style="font: 14px Verdana; float: right">
                      <p style="margin-top: 0px; margin-left: 20px;">
                          Chọn file để update:
                          <asp:FileUpload ID="FileUpload" Width="450px" runat="server" />
                      </p>
                      <p style="margin-top: 0px; margin-left: 20px;">
                          <input type="button" value="Update Price SAP" runat="server" onserverclick="ImportFromExcel" class="btn btn-primary" />

                         <%-- <button type="button" class="btn btn-primary float-right" style="margin-right: 5px;" runat="server">--%>
                              <%--onserverclick="btnDownloadClick" --%>
                             <%-- <i class="fas fa-download"></i>Tải file mẫu upload--%>
                         <%-- </button>--%>
                      </p>
                      <p>
                          <asp:Label ID="Label1" runat="server"></asp:Label>
                      </p>
                  </div>


              </div>


              <table class="table table-bordered" id="dataTable">
                  <thead>
                      <tr>
                          <th>IDNO</th>
                          <th>Plant</th>
                          <th>Component</th>
                          <th>Price_STD</th>                          
                          <th>Action</th>
                          
                      </tr>
                  </thead>
                  <tbody>
                       <%int i = 0; %>
                   
                       <% foreach (System.Data.DataRow rows in dt_Price.Rows)
                           {
                      %>
                      <% i++; %>
                      <tr role="row" >
                          <td><%=i %></td>
                          <td><%=rows["Plant"].ToString() %></td>
                          <td><%=rows["Component"].ToString() %></td>
                          <td><%=rows["Price_STD"].ToString() %></td>                                                    
                          <td>
                             
                          </td>
                                               
                      </tr>
                      <% } %>
                  </tbody>
                  <tfoot>
                      <tr>
                        <th>IDNO</th>
                        <th>Plant</th>
                        <th>Component</th>
                        <th>Price_STD</th>
                        <th>Action</th>
                      </tr>
                  </tfoot>
              </table>


          </div>
      </div>
  </div>

  <script>
      $(function () {
      //$("#datepicker").datepicker({ dateFormat: 'yy-mm-dd' });
          //$("#<%= txtDate.ClientID %>").datepicker();
          $("#<%= txtDate.ClientID %>").datepicker({ dateFormat: 'yy-mm-dd' });

          $("#<%= txtDate2.ClientID %>").datepicker({ dateFormat: 'yy-mm-dd' });
      });
  </script>





</asp:Content>
