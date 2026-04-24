<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMaterAB.aspx.cs" Inherits="MATERIAL_IN_OUT.frmMaterAB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
             <h3><b style="font-size: 30px;">Setting Master Type From ACC</b></h3>
             <br />
             <p style="color: blue;">
                 <asp:Label ID="lblConfirm" Text="" runat="server"></asp:Label>
             </p>
         </div>
         <div class="col-sm-12">
             <div style="float: left; padding-right: 10px;">
                 To Date:
                       <%--<input type="text" id="datepicker" runat="server">--%>
                 <input type="date" id="Date1" name="date" runat="server">
                 From Date:                                    
                       <input type="date" id="ngaychiid" name="date" runat="server">
             </div>

             <div style="float: left; padding-right: 10px;">
                 <input type="text" id="filterMaterial" runat="server" placeholder="Nhập Type Name" style="height: 34px;" />
             </div>

             <div style="float: left;">
                 <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click">
                     Filter
                 </button>
             </div>

             <div style="float: left; padding-left: 10px;">
                 <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
                     Add
                 </button>
             </div>

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
                     <th>TypeID</th>
                    <th>TypeName</th>
                    <th>Decription</th>
                    <th>NameTemplate</th>
                    <th>AccountCost</th>                 
                    <th>AccountName</th>
                    <th>Out</th>
                    <th>In</th>
                    <th>Action</th>
                 </tr>
           
         </thead>
         <tbody>
             <%int i = 0; %>
             <% foreach (System.Data.DataRow rows in dt_image.Rows)
                 {

         %>
         <%i++;%>
             <tr role="row" >  <%--style="background-color: <%= backgroundColor %>"--%>
                 <td><%=rows["TypeID"].ToString()%></td>
                 <td><%=rows["TypeName"].ToString()%></td>
                 <td><%=rows["Decription"].ToString()%></td>
                 <td><%=rows["NameTemplate"].ToString()%></td>
                 <td><%=rows["AccountCost"].ToString()%></td>                 
                 <td><%=rows["AccountName"].ToString()%></td>                 
                 <td><%=rows["MVTOut"].ToString()%></td>                 
                 <td><%=rows["MVTIn"].ToString()%></td>                 
                 <td>
                     <a href="#" class="btn btn-info btn-sm" title="eidt item" onclick="openEditModal3('<%= rows["TypeID"].ToString() %>','<%= rows["TypeName"].ToString() %>','<%=rows["Decription"].ToString() %>','<%=rows["NameTemplate"].ToString() %>','<%=rows["AccountCost"].ToString() %>','<%=rows["AccountName"].ToString() %>','<%=rows["ID"].ToString() %>','<%=rows["MVTOut"].ToString()%>','<%=rows["MVTIn"].ToString()%>')">Edit</a>
                     <a href="#" style="background-color: #dc3545; color: white;" class="btn btn-info btn-sm" title="eidt item" onclick="openEditModal4('<%= rows["ID"].ToString() %>')">Delete</a>

                 </td>
             </tr>
             <%} %>
         </tbody>
         <tfoot>             
             <tr role="row">
                   <th>TypeID</th>
                   <th>TypeName</th>
                   <th>Decription</th>
                   <th>NameTemplate</th>
                   <th>AccountCost</th>                 
                   <th>AccountName</th>
                   <th>Out</th>
                   <th>In</th>
                   <th>Action</th>            
             </tr>
         </tfoot>
     </table>
 </div>


        <!-- Modal -->
<div class="modal" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Add infor form A & B</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>

            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <label for="ID">TypeID</label>
                        <asp:TextBox ID="TypeIDid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ID">TypeName</label>
                        <asp:TextBox ID="TypeNameid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label for="ID">Decription</label>
                        <asp:TextBox ID="Decriptionid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ID">NameTemplate</label>
                        <asp:TextBox ID="NameTemplateid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <label for="ID">AccountCost</label>
                        <asp:TextBox ID="AccountCostid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ID">AccountName</label>
                        <asp:TextBox ID="AccountNameid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <label for="ID">MVT OUT</label>
                        <asp:TextBox ID="outid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ID">MVT IN</label>
                        <asp:TextBox ID="inid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                </div>

                <!-- Lặp lại thêm các dòng -->
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-primary" runat="server" onserverclick="themhanghoa">save</button>
            </div>
        </div>
    </div>
</div>

 <div class="modal" id="myModal4">
     <div class="modal-dialog">
         <div class="modal-content">
             <div class="modal-header">
                 <div class="row">
                     <div>
                         <h4 class="modal-title" id="headerTag11" style="float: left">Delete record?</h4>
                         <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                             <span aria-hidden="true">&times;</span>
                         </button>
                     </div>

                 </div>
             </div>

             <%-- Modal footer --%>
             <div class="modal-body">
                 <div class="container-fluid">
                     <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label for="ID">ID</label>
                                 <span style="color: green; font-size: 11px; font-style: italic;">(Read only)</span>
                                 <asp:TextBox ID="txtid_del" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                <%-- <label for="exampleInputEmail1">Model</label>
                                 <asp:TextBox ID="txMaterialName_del" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>--%>
                             </div>
                         </div>
                     </div>
                     
                 </div>
             </div>
             <div class="modal-footer">
                 <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                 <button type="button" runat="server" id="Button2" onserverclick="Xoathongtin" class="btn btn-primary">                     
                     Save
                 </button>
             </div>
         </div>
     </div>
 </div>

 <div class="modal" id="myModal3">
     <div class="modal-dialog modal-lg">
         <div class="modal-content">
             <div class="modal-header">
                 <div class="row">
                     <div>
                         <h4 class="modal-title" id="headerTag1" style="float: left">Update infor Form A & B</h4>
                         <%--<h6 class="modal-title" id="headerTag" style="float: left; color:red"><b><i>Chi tiết tồn kho!</i></b></h6>--%>

                         <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                             <span aria-hidden="true">&times;</span>
                         </button>
                     </div>

                 </div>
             </div>

             <div class="modal-body">
                 <div class="row">
                     <div class="col-md-6">
                         <label for="ID">TypeID</label>
                         <asp:TextBox ID="IDTypeID" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                     </div>
                     <div class="col-md-6">
                         <label for="ID">TypeName</label>
                         <asp:TextBox ID="idTypeName" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                     </div>

                 </div>
                 <div class="row">
                     <div class="col-md-6">
                         <label for="ID">Decription</label>
                         <asp:TextBox ID="idDecription" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                     </div>
                     <div class="col-md-6">
                         <label for="ID">NameTemplate</label>
                         <asp:TextBox ID="idNameTemplate" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                     </div>
                 </div>

                 <div class="row">
                    <div class="col-md-6">
                        <label for="ID">AccountCost</label>
                        <asp:TextBox ID="idAccountCost" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ID">AccountName</label>
                        <asp:TextBox ID="idAccountName" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                </div>
                  <div class="row">
                    <div class="col-md-6">
                        <label for="ID">MVT OUT</label>
                        <asp:TextBox ID="idout" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ID">MVT IN</label>
                        <asp:TextBox ID="idin" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                </div>
                  <div class="row">
                    <div class="col-md-6">
                        <label for="ID">ID</label>
                        <asp:TextBox ID="idID" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        
                    </div>
                </div>
                 <!-- Lặp lại thêm các dòng -->
             </div>

             <%-- Modal footer --%>
             <div class="modal-footer">
                 <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                 <button type="button" runat="server" id="Button1" onserverclick="Updatethongtin" class="btn btn-primary">                     
                     Save
                 </button>
             </div>
         </div>
     </div>
 </div>
    </form>

  

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
        $('#IDTypeID').prop("readonly", true);
        $('#idID').prop("readonly", true);
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

    function openEditModal3(TypeID, TypeName, Decription, NameTemplate, AccountCost, AccountName,ID,mvtout,mvtin) {
        $("#IDTypeID").val(TypeID);
        $("#idTypeName").val(TypeName);
        $("#idDecription").val(Decription);
        $("#idNameTemplate").val(NameTemplate);
        $("#idAccountCost").val(AccountCost);
        $("#idAccountName").val(AccountName);
        $("#idID").val(ID);
        $("#idout").val(mvtout);
        $("#idin").val(mvtin);

        $('#myModal3').modal('show');
    }

    function openEditModal4(id) {
        $("#txtid_del").val(id);
        //$("#txMaterialName_del").val(material);

        $('#myModal4').modal('show');
    }



</script>

<%--<script src="/plugins/jquery/jquery-ui.js"></script>--%>
<script type="text/javascript">    

</script>
</body>
</html>
