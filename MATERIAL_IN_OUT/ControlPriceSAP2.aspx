<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlPriceSAP2.aspx.cs" Inherits="MATERIAL_IN_OUT.ControlPriceSAP2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
     <title>Control SAP price</title>
    <!-- Bootstrap 4 -->
<%--<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css">--%>
    <link rel="stylesheet" href="/LibNew/bootstrap.min.css">
<!-- Font Awesome -->
<%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">--%>
<link rel="stylesheet" href="/LibNew/all.min.css">
<!-- AdminLTE -->
<%--<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/admin-lte@3.2/dist/css/adminlte.min.css">--%>
<link rel="stylesheet" href="/LibNew/adminlte.min.css">
<!-- DataTables -->
<%--<link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/dataTables.bootstrap4.min.css">--%>
<link rel="stylesheet" href="/LibNew/dataTables.bootstrap4.min.css">
<%--<link rel="stylesheet" href="https://cdn.datatables.net/responsive/2.5.0/css/responsive.bootstrap4.min.css">--%>
<link rel="stylesheet" href="/LibNew/responsive.bootstrap4.min.css">
<!-- jQuery UI -->
<%--<link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">--%>
<link rel="stylesheet" href="/LibNew/jquery-ui.css">
<!-- Toastr -->
<%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css">--%>
<link rel="stylesheet" href="/LibNew/toastr.min.css">



<!-- jQuery -->
<%--<script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>--%>
<script src="/LibNew/jquery-3.6.4.min.js"></script>
<!-- jQuery UI -->
<%--<script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>--%>
<script src="/LibNew/jquery-ui.min.js"></script>
<!-- Bootstrap 4 -->
<%--<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>--%>
<script src="/LibNew/bootstrap.bundle.min.js"></script>
<!-- DataTables -->
<%--<script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>--%>
<script src="/LibNew/jquery.dataTables.min.js"></script>
<%--<script src="https://cdn.datatables.net/1.13.6/js/dataTables.bootstrap4.min.js"></script>--%>
<script src="/LibNew/dataTables.bootstrap4.min.js"></script>
<%--<script src="https://cdn.datatables.net/responsive/2.5.0/js/dataTables.responsive.min.js"></script>--%>
<script src="/LibNew/dataTables.responsive.min.js"></script>
<%--<script src="https://cdn.datatables.net/responsive/2.5.0/js/responsive.bootstrap4.min.js"></script>--%>
<script src="/LibNew/responsive.bootstrap4.min.js"></script>

<!-- AdminLTE -->
<%--<script src="https://cdn.jsdelivr.net/npm/admin-lte@3.2/dist/js/adminlte.min.js"></script>--%>
<script src="/LibNew/adminlte.min.js"></script>

<!-- Toastr -->
<%--<script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>--%>
<script src="/LibNew/toastr.min.js"></script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="card">
            <div class="card-header">
                <h1>Upload SAP Price</h1>
                <br />
                <p style="color:blue;">
                    <asp:Label ID="lblConfirm" Text="" runat="server"></asp:Label>
                </p>
                <%--class="card-title"--%>
                <div class="col-sm-12">
                    <div style="float:left;">
                        Từ ngày:
                                    <%--<input type="text" id="datepicker" runat="server">--%>
                                    <input type="date" id="Date1" name="date" runat="server">
                        Đến ngày:                                    
                                    <input type="date" id="ngaychiid" name="date" runat="server">
                                    
                    
                   <%-- Bàn / Phòng:                                    
                                    <input type="text" id="banphongid" runat="server">--%>
                    </div>
                    <div style="float: left; padding-right: 10px; padding-left:10px;">
    <input type="text" id="filtermaterial" runat="server" placeholder="Material" style="height: 34px;" />
</div>
                    
                 <div class="col-md-1" style="float: left">
                     <div class="form-group">                        
                        <asp:DropDownList ID="dr_filter_plan" runat="server"
                            AppendDataBoundItems="true"
                            DataTextField="Plant"
                            DataValueField="Plant"
                            CssClass="custom-select custom-select-sm form-control form-control-sm" />
                        <%--OnSelectedIndexChanged="dr_filter_Plan_SelectedIndexChanged" AutoPostBack="True"--%>
                    </div>
                 </div>

                    <span style="padding-left:20px;"></span>
                    <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click2" >                 

                        <i class="fa fa-fw fa-lg fa-search"></i>Lọc</button>

                    <!-- import file excel -->
                <!-- ADD A FILE UPLOAD CONTROL AND A BUTTON TO EXECUTE. -->
                <div style="font: 14px Verdana; float: right">
                    <p style="margin-top: 0px; margin-left: 20px;">
                       Chọn file để upload:
                        <asp:FileUpload ID="FileUpload" Width="450px" runat="server" />
                    </p>
                    <p style="margin-top: 0px; margin-left: 20px;">
                        <input type="button" value="Update Price SAP" runat="server" onserverclick="ImportFromExcel" class="btn btn-primary" /> 

                        &nbsp;&nbsp;&nbsp;<%--<input type="button" value="Import DECT" runat="server" onserverclick="ImportFromExcel1" class="btn btn-primary" />--%>

                         &nbsp;&nbsp;&nbsp;
                        <%--<button type="button" class="btn btn-primary float-right" style="margin-right: 5px;" runat="server"> --%>
                          <%--  <i class="fas fa-download"></i>Tải file mẫu upload--%>
                        <%--</button>--%>
                    </p>
                    <p>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </p>

                </div>


                </div>
    
            </div>
        </div>


             <div>
            <table id="example" class="table table-striped table-bordered" style="width:100%">
        <thead>
         
                 <tr role="row">
                    <th>IDNO</th>
                    <th>Plant</th>
                    <th>Material</th>
                    <th>Price_STD</th>                          
                    <th>Price2</th>                          
                    <th>Date Insert</th>                          
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
                                        <td><%=rows["Price2"].ToString() %></td>                                                    
                                        <td><%=rows["Updatetime"].ToString() %></td>                                                    
                                        <td>
       
                                        </td>
                         
                                    </tr>
                                    <% } %>
       </tbody>              

    </table>
        </div>



        </div>

    </form>

    <%-- <script src="../../Scripts/jquery.min.js"></script>
    <script src="../../Scripts/bootstrap.bundle.min.js"></script>

    <script src="../../Content/jquery.dataTables.js"></script>
    <script src="../../Scripts/dataTables.buttons.js"></script>
    
    <script src="../../Scripts/jquery.dataTables.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap4.min.js"></script>

    <script src="../../Scripts/dataTables.responsive.min.js"></script>
    <script src="../../Scripts/responsive.bootstrap4.min.js"></script>
    <script src="../../Scripts/adminlte.min.js"></script>
    <script src="../../Scripts/demo.js"></script>--%>

    <script type="text/javascript">
    // Hàm để chọn hoặc bỏ chọn tất cả các checkbox
    function selectAllCheckboxes(source) {
        var checkboxes = document.querySelectorAll('.selectCheckbox');
        checkboxes.forEach(function(checkbox) {
            checkbox.checked = source.checked;
        });
    }

        function submitSelectedRows() {
        var selectedIds = [];
        
        // Lấy tất cả các checkbox được chọn
        var checkboxes = document.querySelectorAll('.selectCheckbox:checked');
        
        // Lặp qua tất cả các checkbox đã chọn và lấy giá trị id
        checkboxes.forEach(function(checkbox) {
            selectedIds.push(checkbox.value); // value của checkbox là id của dòng
        });
        
        if (selectedIds.length > 0) {
            // Gửi các ID đã chọn tới server (ví dụ qua Ajax)
            sendToBackend(selectedIds);
        } else {
            alert('Vui lòng chọn ít nhất một dòng!');
        }
    }

    

</script>

     <script>
   $(document).ready(function () {            
      // $('#txtid').prop("readonly", true);  
       //$('#txtmodel').prop("readonly", true);  
      //$('#txtproductiondate').prop("readonly", true);  
       //$('#txtline').prop("readonly", true);  

      // $("#txtid2").prop("readonly", true);  
        //$("#txtmodel2").prop("readonly", true);  
            //$("#txtngaysx2").prop("readonly", true);  
            //$("#txtline2").prop("readonly", true);  
                    

         });
               
            $(function () {
                $("#example").DataTable({                           
                            "responsive": true,
                            "autoWidth": true,
                            //"order": [[7, "desc"]],
                            "pageLength": 50,
                            "buttons": ['copy', 'csv', 'excel'],
                            //"ordering": true,
                            //"paging": true,
                            //"lengthChange": false,
                            //"searching": false,
                            //"info": true,                    
                });
                

         });



         //$('#myModal').modal()
        
    </script>

     <%--<script src="../Scripts/jquery-ui.js"></script>--%>
    <script type="text/javascript">
        //$(function () {
        //    var onlyDate, today = new Date();
        //    var dateNewFormat = '';

        //    onlyDate = today.getDate();
        //    if (onlyDate.toString().length == 2) {

        //        dateNewFormat = onlyDate;
        //    }
        //    else {
        //        dateNewFormat = '0' + onlyDate;
        //    }

        //    dateNewFormat = dateNewFormat + '-';

        //    if (today.getMonth().length == 2) {

        //        dateNewFormat += (today.getMonth() + 1);
        //    }
        //    else {
        //        //dateNewFormat += '0' + (today.getMonth() + 1);
        //        dateNewFormat += (today.getMonth() + 1);
        //    }

        //    dateNewFormat = dateNewFormat + '-' + today.getFullYear();
        //    //dateNewFormat = today.getFullYear() + '-';

        //    //$('#datepicker').val(dateNewFormat);
           

        //    //$("#datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
          
        //    $("#Date1").datepicker({ dateFormat: 'dd-mm-yy' });
        //    $("#ngaychiid").datepicker({ dateFormat: 'dd-mm-yy' });

        //});


    </script>


</body>
</html>
