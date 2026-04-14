<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSlocMCS.aspx.cs" Inherits="MATERIAL_IN_OUT.frmSlocMCS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Control Sloc MCS</title>
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
            <h3><b style="font-size: 30px;">List sloc MCS</b></h3>
            <br />
            <p style="color: blue;">
                <asp:Label ID="lblConfirm" Text="" runat="server"></asp:Label>
            </p>
        </div>
        <div class="col-sm-12">
            <div style="float: left; padding-right: 10px;">
                From date:
               <%--<input type="text" id="datepicker" runat="server">--%>
                <input type="date" id="Date1" name="date" runat="server" />
                To date:                                    
               <input type="date" id="ngaychiid" name="date" runat="server" />
            </div>


            <div style="float: left; padding-right: 10px;">
                Sloc:
                <input type="text" id="filterSloc" runat="server" placeholder="Input Sloc" style="height: 34px;" />
            </div>          

            <div style="float: left; padding-right: 10px;">
                <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click">
                    Filter
                </button>
            </div>           
        </div>
    </div>

 <div>
     <table id="example" class="table table-striped table-bordered" style="width: 100%">
         <thead>

             <tr role="row">
                 <th>IDNO</th>
                 <th>Plant</th>
                 <th>Category</th>                 
                 <th>Sloc</th>
                 <th>Type</th>
                 <th>Address</th>
                 <th>Description</th>
                 <th>CreatedBy</th>                 
                 <th>Actions</th>
             </tr>

         </thead>
         <tbody>
             <% int i = 0; %>
             <% foreach (System.Data.DataRow rows in dt.Rows)
                 { %>
             <% i++; %>
             <tr>
                 <td><%= i %></td>
                 <td><%= rows["Plant"].ToString() %></td>
                 <td><%= rows["Category"].ToString() %></td>                                 
                 <td><%= rows["Sloc"].ToString() %></td>
                 <td><%= rows["Type"].ToString() %></td>
                 <td><%= rows["Address"].ToString() %></td>                                  
                 <td><%= rows["Description"].ToString() %></td>                                  
                 <td><%= rows["CreatedBy"].ToString() %></td>                                  
                 <td></td>                                  
                 <td>                     
                 </td>
             </tr>
             <% } %>
         </tbody>
         <tfoot>
             <tr>
                 <th>IDNO</th>
                <th>Plant</th>
                <th>Category</th>                 
                <th>Sloc</th>
                <th>Type</th>
                <th>Address</th>
                <th>Description</th>
                <th>CreatedBy</th>                 
                <th>Actions</th>

             </tr>
         </tfoot>
     </table>
 </div>

</div>


    </form>

    <script type="text/javascript">
        $(document).ready(function () {
            //$('#RequestNoid').prop("readonly", true);
            //$('#TypeFormid').prop("readonly", true);
            //$('#RequestNo4').prop("readonly", true);
            //$('#TypeForm4').prop("readonly", true);
            //$('#RequestNo5').prop("readonly", true);
            //$('#TypeForm5').prop("readonly", true);
            //$('#RequestNo6').prop("readonly", true);
            //$('#TypeForm6').prop("readonly", true)
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
    </script>

</body>


</html>
