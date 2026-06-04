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
                    Lọc
                </button>
            </div>  
            
             <span style="padding-left:20px;"></span>
            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
                Thêm mới
                </button>

             <span style="padding-left:20px;"></span>
            <button class="btn btn-primary" type="button" runat="server" onserverclick="Sys_SlocMCS_Click">
                Đồng bộ kho MCS
                </button>



                <!-- import file excel -->
                <!-- ADD A FILE UPLOAD CONTROL AND A BUTTON TO EXECUTE. -->
                <div style="font: 14px Verdana; float: right">
                    <p style="margin-top: 0px; margin-left: 20px;">
                       Chọn file để upload:
                        <asp:FileUpload ID="FileUpload" Width="450px" runat="server" />
                    </p>
                    <p style="margin-top: 0px; margin-left: 20px;">
                        <input type="button" value="Import data to Excel" runat="server" onserverclick="ImportFromExcel" class="btn btn-primary" /> 

                        <button type="button" class="btn btn-primary float-right" style="margin-right: 5px;" runat="server" onserverclick="btnDownloadClick"> 
                            Tải file mẫu upload
                        </button>
                    </p>
                    <p>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </p>

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
                 <td>
                     <a href="#" class="btn btn-primary" title="Edit" onclick="openEditModal6('<%= rows["Id"].ToString() %>','<%= rows["Plant"].ToString() %>','<%= rows["Sloc"].ToString() %>')">Edit</a>
                     <a href="#" class="btn btn-danger" title="Delete" onclick="openEditModal7('<%= rows["Id"].ToString() %>','<%= rows["Sloc"].ToString() %>')">Delete</a>
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

                        <!-- Modal -->
<div class="modal" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Add New Sloc</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">×</span>
        </button>
      </div>
      <div class="modal-body">
                         <div class="row">                               
                                <div class="col-md-6">
                                    <div class="form-group">
                                       <label for="exampleInputEmail1">Plant</label>
                                        <span style="color: red; font-size: 11px; font-style: italic;">You must input!(*)</span>
                                        <asp:TextBox ID="idplan" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>                                        
                                    </div>
                                </div>
                              <div class="col-md-6">
                                     <div class="form-group">
                                         <label for="ID">Sloc</label>
                                         <span style="color: red; font-size: 11px; font-style: italic;">You must input!(*)</span>
                                         <asp:TextBox ID="idsloc" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                                     </div>
                                 </div>
                            </div>                    
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary" runat="server" onserverclick="themhanghoa">Save</button>
      </div>
    </div>
  </div>
</div>


        <div class="modal" id="myModal6">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div>
                        <h4 class="modal-title" id="headerTag6" style="float: left">Dou you want Edit Sloc ?</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                </div>
            </div>

            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <label for="ID">Plan</label>
                        <asp:TextBox ID="Plantid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ID">Sloc</label>
                        <asp:TextBox ID="Slocid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label for="ID">ID</label>
                        <asp:TextBox ID="txtid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                       
                    </div>
                </div>
                

                <!-- Lặp lại thêm các dòng -->
            </div>

            <%-- Modal footer --%>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                <button type="button" runat="server" id="Button4" onserverclick="UpdateDocumentNo" class="btn btn-primary">
                    Update
                </button>
            </div>
        </div>
    </div>
</div>


                <div class="modal" id="myModal7">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div>
                        <h4 class="modal-title" id="headerTag7" style="float: left">Dou you want delete Sloc ?</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                </div>
            </div>

            <div class="modal-body">
               
                <div class="row">
                    <div class="col-md-6">
                        <label for="ID">ID</label>
                        <asp:TextBox ID="txtid7" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                       <label for="ID">ID</label>
                        <asp:TextBox ID="sloc7" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                </div>
                

                <!-- Lặp lại thêm các dòng -->
            </div>

            <%-- Modal footer --%>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                <button type="button" runat="server" id="Button1" onserverclick="DeleteslocMCS" class="btn btn-primary">
                    Update
                </button>
            </div>
        </div>
    </div>
</div>


    </form>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtid').prop("readonly", true);
            $('#txtid7').prop("readonly", true);
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

        function openEditModal6(id, Plant, Sloc) {
            $("#txtid").val(id);
            $("#Plantid").val(Plant);
            $("#Slocid").val(Sloc);
            $('#myModal6').modal('show');
        }

        function openEditModal7(id, Sloc) {
            $("#txtid7").val(id);
            $("#sloc7").val(Sloc);            
            $('#myModal7').modal('show');
        }


    </script>

</body>


</html>
