<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmHistoryChangePrice.aspx.cs" Inherits="MATERIAL_IN_OUT.frmHistoryChangePrice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>History Change Price SAP</title>
        <!-- Bootstrap 4 -->
    <link rel="stylesheet" href="/LibNew/bootstrap.min.css">
<!-- Font Awesome -->
<link rel="stylesheet" href="/LibNew/all.min.css">
<!-- AdminLTE -->
<link rel="stylesheet" href="/LibNew/adminlte.min.css">
<!-- DataTables -->
<link rel="stylesheet" href="/LibNew/dataTables.bootstrap4.min.css">
<link rel="stylesheet" href="/LibNew/responsive.bootstrap4.min.css">
<!-- jQuery UI -->
<link rel="stylesheet" href="/LibNew/jquery-ui.css">
<!-- Toastr -->
<link rel="stylesheet" href="/LibNew/toastr.min.css">



<!-- jQuery -->
<script src="/LibNew/jquery-3.6.4.min.js"></script>
<!-- jQuery UI -->
<script src="/LibNew/jquery-ui.min.js"></script>
<!-- Bootstrap 4 -->
<script src="/LibNew/bootstrap.bundle.min.js"></script>
<!-- DataTables -->
<script src="/LibNew/jquery.dataTables.min.js"></script>
<script src="/LibNew/dataTables.bootstrap4.min.js"></script>
<script src="/LibNew/dataTables.responsive.min.js"></script>
<script src="/LibNew/responsive.bootstrap4.min.js"></script>

<!-- AdminLTE -->
<script src="/LibNew/adminlte.min.js"></script>

<!-- Toastr -->
<script src="/LibNew/toastr.min.js"></script>

</head>

<body>
    <form id="form1" runat="server">
        <div class="card">
     <div class="card-header">
         <div class="col-sm-12">
             <h3><b style="font-size: 30px;">History Change Price SAP</b></h3>
             <br />
             <p style="color: blue;">
                 <asp:Label ID="lblConfirm" Text="" runat="server"></asp:Label>
             </p>
         </div>
         <div class="col-sm-12">
             <div style="float: left; padding-right: 10px;">
                 Từ ngày:
                       <%--<input type="text" id="datepicker" runat="server">--%>
                 <input type="date" id="Date1" name="date" runat="server">
                 Đến ngày:                                    
                       <input type="date" id="ngaychiid" name="date" runat="server">
             </div>

             <div style="float: left; padding-right: 10px;">
                 <input type="text" id="filterMaterial" runat="server" placeholder="Nhập Material" style="height: 34px;" />
             </div>

             <div style="float: left;">
                 <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click">
                     <i class="fa fa-fw fa-lg fa-search"></i>Lọc
                 </button>
             </div>

            <%-- <div style="float: left; padding-left: 10px;">
                 <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
                     Add
                 </button>
             </div>--%>

            <%-- <div style="clear: both;"></div>--%>

         

             <%--<button class="btn btn-primary" type="button" runat="server" style="margin-left: 20px;"><i class="fa fa-download"></i>&nbsp; Export</button>&nbsp;&nbsp;&nbsp;--%>             
      <%--onserverclick="btnExport_Click"--%>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                       

         </div>

     </div>
 </div>


 <div>
     <table id="example" class="table table-striped table-bordered" style="width: 100%">
         <thead>
          
                 <tr role="row">
                     <th>IDNO</th>
                    <th>Request_NO</th>
                    <th>Plant</th>
                    <th>Material</th>
                    <th>QtyIssue</th>                 
                    <th>QtyStock</th>                 
                    <th>Price OLD</th>
                    <th>Amount OLD</th>
                    <th>Price NEW</th>
                    <th>Amount NEW</th>
                    <th>CreateDate</th>
                    
                 </tr>
           
         </thead>
         <tbody>
             <% int i = 0; %>
<% foreach (System.Data.DataRow rows in dt.Rows) { %>
    <% i++; %>
    <tr>
        <td><%= i %></td>
        <td><%= rows["Request_NO"].ToString() %></td>
        <td><%= rows["Plant"].ToString() %></td>
        <td><%= rows["Material"].ToString() %></td>
        <td><%= rows["QtyIssue"].ToString() %></td>
        <td><%= rows["QtyStock"].ToString() %></td>
        <td><%= rows["giacu"].ToString() %></td>
        <td><%= rows["amountcu"].ToString() %></td>
        <td><%= rows["giamoi"].ToString() %></td>
        <td><%= rows["amountmoi"].ToString() %></td>
        <td><%= rows["CreateDate"].ToString() %></td>
    </tr>
<% } %>
         </tbody>
         <tfoot>
             <tr>
                    < <th>IDNO</th>
                    <th>Request_NO</th>
                    <th>Plant</th>
                    <th>Material</th>
                    <th>QtyIssue</th>                 
                    <th>QtyStock</th>                 
                    <th>Price OLD</th>
                    <th>Amount OLD</th>
                    <th>Price NEW</th>
                    <th>Amount NEW</th>
                    <th>CreateDate</th>
                    
             </tr>
         </tfoot>
     </table>
 </div>


    

    </form>

<%--    <script src="/plugins/jquery/jquery.min.js"></script>
<script src="/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
<script src="/plugins/datatables/jquery.dataTables.min.js"></script>
<script src="/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js"></script>
<script src="/plugins/datatables-responsive/js/dataTables.responsive.min.js"></script>
<script src="/plugins/datatables-responsive/js/responsive.bootstrap4.min.js"></script>
<script src="/dist/js/adminlte.min.js"></script>
<script src="/dist/js/demo.js"></script>--%>

<script>

    //$(function () {
    //    $("#btnExport_normal").click(function () {
    //        $("#example1").table2excel({
    //            filename: "Report_inspection_normal"
    //        });
    //    })
    //});
</script>

<script type="text/javascript">  
    $(document).ready(function () {
        $('#IDedit').prop("readonly", true);
        $('#txtid_del').prop("readonly", true);
    });

    $(function () {
        $("#example").DataTable({
            //"responsive": true,
            "autoWidth": true,
            scrollX: true,
            //"order": [[7, "desc"]],
            "pageLength": 50
            //"ordering": true,
            //"paging": true,
            //"lengthChange": false,
            //"searching": false,
            //"info": true,                    
        });

    });

    //function openEditModal3(id, NameTemplate, TypeID, TypeName, AccountCost, Description) {
    //    $("#IDedit").val(id);
    //    $("#idNameTemplate").val(NameTemplate);
    //    $("#idTypeID").val(TypeID);
    //    $("#idTypeName").val(TypeName);
    //    $("#idAccountCost").val(AccountCost);
    //    $("#idDescription").val(Description);

    //    $('#myModal3').modal('show');
    //}

    //function openEditModal4(id) {
    //    $("#txtid_del").val(id);
    //    //$("#txMaterialName_del").val(material);

    //    $('#myModal4').modal('show');
    //}



</script>

<%--<script src="/plugins/jquery/jquery-ui.js"></script>--%>
<script type="text/javascript">    

</script>
</body>
</html>
