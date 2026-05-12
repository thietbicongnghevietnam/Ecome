<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="import_excel_test.aspx.cs" Inherits="MATERIAL_IN_OUT.import_excel_test" %>

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


</head><body>
    <form id="form1" runat="server">
        
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
                    
                    
               

                 

                    <!-- import file excel -->
                <!-- ADD A FILE UPLOAD CONTROL AND A BUTTON TO EXECUTE. -->
                <div style="font: 14px Verdana; float: right">
                    <p style="margin-top: 0px; margin-left: 20px;">
                       Chọn file để upload:
                        <asp:FileUpload ID="FileUpload" Width="450px" runat="server" />
                    </p>
                    <p style="margin-top: 0px; margin-left: 20px;">
                        <input type="button" value="Update excel test" runat="server" onserverclick="ImportFromExcel" class="btn btn-primary" /> 

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


    


    </form>
</body>
</html>
