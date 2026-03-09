<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPMSOutSap.aspx.cs" Inherits="MATERIAL_IN_OUT.frmPMSOutSap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PMS Out SAP</title>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        <div class="card">
            <div class="card-header">
                <div class="col-sm-12">
                    <h3><b style="font-size: 30px;">List Issue In-Out PMS Out SAP</b></h3>
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
                        RequestNO:
                        <input type="text" id="filterRequestNo" runat="server" placeholder="Input Request No" style="height: 34px;" />
                    </div>
                    <div style="float: left; padding-right: 10px;">
                        Sanction:
                        <input type="text" id="filterSanctionNo" runat="server" placeholder="Input Sanction No" style="height: 34px;" />
                    </div>

                    <div style="float: left; padding-right: 10px;">
                        <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click">
                            Filter
                        </button>
                    </div>

                    <!-- Checkbox + Download All -->
                    <div style="float: left; padding-right: 10px; display: flex; align-items: center; gap: 8px;">
                        <button class="btn btn-primary" type="button" runat="server" onserverclick="Dowload_All_Click">
                            Download All
                        </button>
                        <label style="margin: 0; display: flex; align-items: center; gap: 4px; height: 34px; cursor: pointer;">
                            <asp:CheckBox ID="chkDownloadDetail" runat="server" />
                            Download Detail
                        </label>
                    </div>


                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
             
                 <!-- import file excel -->
                    <!-- ADD A FILE UPLOAD CONTROL AND A BUTTON TO EXECUTE. -->
                    <div style="font: 14px Verdana; float: right">
                        <p style="margin-top: 0px; margin-left: 20px;">
                            Chose file to upload All Document No:
        <asp:FileUpload ID="FileUpload" Width="450px" runat="server" />
                        </p>
                        <p style="margin-top: 0px; margin-left: 20px;">
                            <input type="button" value="Upload document no" runat="server" onserverclick="ImportFromExcel" class="btn btn-primary" />

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
            <table id="example" class="table table-striped table-bordered" style="width: 100%">
                <thead>

                    <tr role="row">
                        <th>IDNO</th>
                        <th>RequestNo</th>
                        <th>Department</th>
                        <th>Status_RQ</th>
                        <th>Status_Approved_OutSAP</th>
                        <th>TypeForm</th>
                        <th>Sanction Name</th>
                        <th>Document No</th>

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
                        <td><%= rows["RequestNo"].ToString() %></td>
                        <td><%= rows["Department"].ToString() %></td>
                        <td><%= rows["Status_RQ"].ToString() %></td>
                        <td><%= rows["Status_Approved_OutSAP"].ToString() %></td>
                        <td><%= rows["TypeForm"].ToString() %></td>
                        <td><%= rows["SanctionName"].ToString() %></td>
                        <td><%= rows["DocumentNo"].ToString() %></td>

                        <td>
                            <a href="#" class="btn btn-info btn-sm" title="Detail" onclick="openEditModal2('<%= rows["RequestNo"].ToString() %>','<%= rows["TypeForm"].ToString() %>')">Detail</a>
                            <a href="#" class="btn btn-info btn-sm" title="Out201_98" onclick="openEditModal3('<%= rows["RequestNo"].ToString() %>','<%= rows["TypeForm"].ToString() %>','<%= rows["SanctionName"].ToString() %>')">Out201_98</a>
                            <a href="#" class="btn btn-info btn-sm" title="Tranfer_G98" onclick="openEditModal4('<%= rows["RequestNo"].ToString() %>','<%= rows["TypeForm"].ToString() %>','<%= rows["SanctionName"].ToString() %>')">Tranfer_G99</a>
                            <a href="#" class="btn btn-info btn-sm" title="Out Scrap" onclick="openEditModal5('<%= rows["RequestNo"].ToString() %>','<%= rows["TypeForm"].ToString() %>','<%= rows["SanctionName"].ToString() %>')">Out Scrap</a>
                            <a href="#" class="btn btn-primary" title="Update DocumentNo" onclick="openEditModal6('<%= rows["RequestNo"].ToString() %>','<%= rows["TypeForm"].ToString() %>')">Update DocumentNo</a>

                        </td>
                    </tr>
                    <% } %>
                </tbody>
                <tfoot>
                    <tr>
                        <th>IDNO</th>
                        <th>RequestNo</th>
                        <th>Department</th>
                        <th>Status_RQ</th>
                        <th>Status_Approved_OutSAP</th>
                        <th>TypeForm</th>
                        <th>Sanction Name</th>
                        <th>Document No</th>

                        <th>Actions</th>

                    </tr>
                </tfoot>
            </table>
        </div>

        <div class="modal fade" id="detailModal" tabindex="-1">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">

                    <div class="modal-header">
                        <h4 class="modal-title">Request No Detail</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>

                    <div class="modal-body" style="max-height: 500px; overflow-y: auto;">

                        <table class="table table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th>NO</th>
                                    <th>Material</th>
                                    <th>Plant</th>
                                    <th>Vendor</th>
                                    <th>CostCenter</th>
                                    <th>Sloc</th>
                                    <th>Qty</th>
                                    <th>UnitPriceST</th>
                                    <th>AmountST</th>
                                    <th>DocumentNo</th>
                                    <th>SanctionName</th>
                                </tr>
                            </thead>

                            <tbody id="detailTableBody"></tbody>

                        </table>

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
                                <h4 class="modal-title" id="headerTag1" style="float: left">Dou you want export Out_201 from 98 ?</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                        </div>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">RequestNo</label>
                                <asp:TextBox ID="RequestNoid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">TypeForm</label>
                                <asp:TextBox ID="TypeFormid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">Sanction Name</label>
                                <asp:TextBox ID="sanctionid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">MVT</label>
                                <asp:TextBox ID="MVTid" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>

                        </div>

                        <!-- Lặp lại thêm các dòng -->
                    </div>

                    <%-- Modal footer --%>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <button type="button" runat="server" id="Button1" onserverclick="Exportfrom98" class="btn btn-primary">
                            Export
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal" id="myModal4">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div>
                                <h4 class="modal-title" id="headerTag4" style="float: left">Dou you want export Issue Tranfer to G99 ?</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                        </div>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">RequestNo</label>
                                <asp:TextBox ID="RequestNo4" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">TypeForm</label>
                                <asp:TextBox ID="TypeForm4" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">Sanction Name</label>
                                <asp:TextBox ID="sanction4" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">MVT</label>
                                <asp:TextBox ID="MVT4" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>

                        </div>

                        <!-- Lặp lại thêm các dòng -->
                    </div>

                    <%-- Modal footer --%>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <button type="button" runat="server" id="Button2" onserverclick="Exporttranfer99" class="btn btn-primary">
                            Export
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal" id="myModal5">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div>
                                <h4 class="modal-title" id="headerTag5" style="float: left">Dou you want export Out Scrap ?</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                        </div>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">RequestNo</label>
                                <asp:TextBox ID="RequestNo5" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">TypeForm</label>
                                <asp:TextBox ID="TypeForm5" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">Sanction Name</label>
                                <asp:TextBox ID="sanction5" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">MVT</label>
                                <asp:TextBox ID="MVT5" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>

                        </div>

                        <!-- Lặp lại thêm các dòng -->
                    </div>

                    <%-- Modal footer --%>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <button type="button" runat="server" id="Button3" onserverclick="ExportOutscrap" class="btn btn-primary">
                            Export
                        </button>
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
                                <h4 class="modal-title" id="headerTag6" style="float: left">Dou you want update Document No ?</h4>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right; margin-left: 300px;">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                        </div>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">RequestNo</label>
                                <asp:TextBox ID="RequestNo6" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="ID">TypeForm</label>
                                <asp:TextBox ID="TypeForm6" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="ID">Document No <i style="color: red;">(* This field is not null! *)</i></label>
                                <asp:TextBox ID="DocumentNo6" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <%--<label for="ID">TypeForm</label>
                        <asp:TextBox ID="TextBox4" CssClass="form-control" placeholder="" runat="server"></asp:TextBox>--%>
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
            $('#RequestNoid').prop("readonly", true);
            $('#TypeFormid').prop("readonly", true);
            $('#RequestNo4').prop("readonly", true);
            $('#TypeForm4').prop("readonly", true);
            $('#RequestNo5').prop("readonly", true);
            $('#TypeForm5').prop("readonly", true);
            $('#RequestNo6').prop("readonly", true);
            $('#TypeForm6').prop("readonly", true)
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

        function openEditModal2(requestNo, typeForm) {
            $.ajax({
                url: "frmPMSOutSap.aspx?action=getDetail",
                data: { requestNo: requestNo, typeForm: typeForm },
                dataType: "json",
                success: function (data) {
                    $("#detailModal .modal-title").text("Request No: " + requestNo);

                    // Kiểm tra data trước khi dùng
                    if (!Array.isArray(data)) {
                        console.error("Data không phải array:", data);
                        alert("Lỗi dữ liệu: " + JSON.stringify(data));
                        return;
                    }

                    var html = "";
                    for (var i = 0; i < data.length; i++) {
                        html += "<tr>";
                        html += "<td>" + (i + 1) + "</td>";  // Số thứ tự bắt đầu từ 1
                        html += "<td>" + data[i].Material + "</td>";
                        html += "<td>" + data[i].Plant + "</td>";
                        html += "<td>" + data[i].VendorCode + "</td>";
                        html += "<td>" + data[i].CostCenter + "</td>";
                        html += "<td>" + data[i].Sloc + "</td>";
                        html += "<td>" + data[i].IssueQty + "</td>";
                        html += "<td>" + data[i].UnitPrice_ST + "</td>";
                        html += "<td>" + data[i].Amount_ST + "</td>";
                        html += "<td>" + data[i].DocumentNo + "</td>";
                        html += "<td>" + data[i].SanctionName + "</td>";
                        html += "</tr>";
                    }
                    $("#detailTableBody").html(html);
                    $("#detailModal").modal("show");
                },
                error: function (xhr) {
                    console.error("AJAX error:", xhr.responseText);
                    alert("Lỗi server: " + xhr.status);
                }
            });
        }



        function openEditModal3(RequestNo, TypeForm, sanctionname) {
            $("#RequestNoid").val(RequestNo);
            $("#TypeFormid").val(TypeForm);
            $("#sanctionid").val(sanctionname);
            $('#myModal3').modal('show');
        }
        function openEditModal4(RequestNo, TypeForm, sanctionname) {
            $("#RequestNo4").val(RequestNo);
            $("#TypeForm4").val(TypeForm);
            $("#sanction4").val(sanctionname);
            $('#myModal4').modal('show');
        }
        function openEditModal5(RequestNo, TypeForm, sanctionname) {
            $("#RequestNo5").val(RequestNo);
            $("#TypeForm5").val(TypeForm);
            $("#sanction5").val(sanctionname);
            $('#myModal5').modal('show');
        }
        function openEditModal6(RequestNo, TypeForm) {
            $("#RequestNo6").val(RequestNo);
            $("#TypeForm6").val(TypeForm);
            $('#myModal6').modal('show');
        }





    </script>

    <%--<script src="/plugins/jquery/jquery-ui.js"></script>--%>
    <script type="text/javascript">    

</script>
</body>
</html>
