<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmReportACC.aspx.cs" Inherits="MATERIAL_IN_OUT.frmReportACC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report ACC</title>
    <!-- Bootstrap 4 -->
    <link rel="stylesheet" href="/LibNew/bootstrap.min.css"/>
    <!-- Font Awesome -->
    <link rel="stylesheet" href="/LibNew/all.min.css"/>
    <!-- AdminLTE -->
    <link rel="stylesheet" href="/LibNew/adminlte.min.css"/>
    <!-- DataTables -->
    <link rel="stylesheet" href="/LibNew/dataTables.bootstrap4.min.css"/>
    <link rel="stylesheet" href="/LibNew/responsive.bootstrap4.min.css"/>
    <!-- jQuery UI -->
    <link rel="stylesheet" href="/LibNew/jquery-ui.css"/>
    <!-- Toastr -->
    <link rel="stylesheet" href="/LibNew/toastr.min.css" />


    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
    <!-- Thu vien Icon =>  <i class="fa fa-arrow-right"> ....-->
     <%--<link rel="stylesheet" href="/LibNew/all.min.css" />--%>



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
                    <h3><b style="font-size: 30px;">Report ACC</b></h3>
                </div>
                <div class="col-sm-12">
                    <div style="float: left; padding-right: 10px;">
                        From date:
                        <input type="date" id="Datefrom1" name="date" runat="server" />
                        To date:                                    
                       <input type="date" id="Dateto1" name="date" runat="server" />
                    </div>

                    <div class="col-md-1" style="float: left;padding-right: 10px;">
                        <div class="form-group">                        
                           <asp:DropDownList ID="dr_filter_section" runat="server"
                               AppendDataBoundItems="true"
                               DataTextField="DeptCode"
                               DataValueField="DeptCode"
                               CssClass="custom-select custom-select-sm form-control form-control-sm" OnSelectedIndexChanged="dr_filter_section_SelectedIndexChanged" AutoPostBack="True" />
                           
                       </div>
                    </div>
                    <div style="float: left; padding-right: 10px;">
                        <b>STATUS ISSUE:</b> 
                        <asp:DropDownList ID="ddlStatus" runat="server" style="height: 34px;" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="ALL" Value="ALL" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="OK" Value="OK"></asp:ListItem>
                            <asp:ListItem Text="NG" Value="NG"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div style="float: left; padding-right: 10px;">
                       
                        <input type="text" id="filterRequestNo" runat="server" placeholder="Input RequestNo" style="height: 34px;" />
                    </div>
                    <div style="float: left; padding-right: 10px;">
                       
                        <input type="text" id="filterSanctionNo" runat="server" placeholder="Input IAF" style="height: 34px;" />
                    </div>

                     <div style="float: left; padding-right: 10px;">
                         
                         <input type="text" id="filterSapPMS" runat="server" placeholder="Input TypeSapPMS" style="height: 34px;" />
                     </div>

                    <div style="float: left; padding-right: 10px;">
                        <button class="btn btn-primary" type="button" runat="server" onserverclick="Search_Date_Click">
                            Filter
                        </button>
                    </div>

                    <div style="float: left; padding-right: 10px;">
                        <button class="btn btn-primary" type="button" runat="server" onserverclick="Dowload_All_Click">
                             Export ACC
                         </button>
                    </div>

                    <div style="float: left; padding-right: 10px;">
                        <button class="btn btn-primary" type="button" runat="server" onserverclick="Report_LOG_Click">
                             Report LOG
                         </button>
                    </div>

                    <div style="float: left; padding-right: 10px;">
                    <button class="btn btn-primary" type="button" runat="server" onserverclick="Report_All_Click">
                         Report ALL
                     </button>
                </div>
    
                </div>

            </div>
        </div>


        <div>
            <table id="example" class="table table-striped table-bordered" style="width: 100%">
                <thead>

                    <tr role="row">
                        <th>Actions</th>
                        <th>IDNO</th>
                        <th>RequestNo</th>
                        <th>UserCreate</th>
                        <th>TypeID</th>  
                        <th>DocumentNo</th>
                        <th>TypeSapPMS</th>
                        <th>GL</th>
                        <th>CostCenter</th>
                        <th>IAF</th>
                        <th>QTY</th>
                        <th>AmoutST</th>
                        <th>Department</th>
                        <th>TypeForm</th>

                        <th>Signature1</th>
                        <th>Signature2</th>
                        <th>Signature3</th>
                        <th>Signature4</th>
                        <th>Signature5</th>
                        <th>Signature6</th>
                        <th>Signature7</th>
                        <th>Signature8</th>
                        <th>Signature9</th>

                        

                    </tr>

                </thead>
                <tbody>
                    <% int i = 0; %>
                    <% foreach (System.Data.DataRow rows in dt_report.Rows)
                        { %>
                    <% i++; %>
                  
                    <tr>
                         <td>
                            <a href="#" class="btnNextSign"
   data-requestno="<%= rows["RequestNo"] %>"
   data-typeform="<%= rows["TypeForm"] %>"
   data-department="<%= rows["Department"] %>"
   title="Next Sign">

    <i class="fa fa-arrow-right"></i> Next Sign|
</a>

                            <a href="#" title="Commnet LOG"  onclick="openEditModal2('<%= rows["RequestNo"] %>',
'<%= rows["TypeForm"] %>')"> <i class="fa fa-comment-dots"></i> CommentLOG </a>|
                             <asp:Button ID="bttPrint" OnClientClick="openInNewTab()" CssClass="btn btn-info" Text="Report PDF" runat="server" OnClick="bttPrint_Click" />
                             

                        </td>
                        <td><%= i %></td>
                        <td><%= rows["RequestNo"].ToString() %></td>
                        <td><%= rows["UserCreate"].ToString() %></td>
                        <td><%= rows["TypeID"].ToString() %></td>
                        <td><%= rows["DocumentNo"].ToString() %></td>
                        <td><%= rows["TypeSapPMS"].ToString() %></td>
                        <td><%= rows["GL"].ToString() %></td>
                        <td><%= rows["CostCenter"].ToString() %></td>

                        <td><%= rows["IAF"].ToString() %></td>
                        <td><%= rows["QTY"].ToString() %></td>
                        <td><%= rows["AmoutST"].ToString() %></td>
                        <td><%= rows["Department"].ToString() %></td>
                        <td><%= rows["TypeForm"].ToString() %></td>

                        <td><%= rows["Signature1"].ToString() %></td>
                        <td><%= rows["Signature2"].ToString() %></td>
                        <td><%= rows["Signature3"].ToString() %></td>
                        <td><%= rows["Signature4"].ToString() %></td>
                        <td><%= rows["Signature5"].ToString() %></td>
                        <td><%= rows["Signature6"].ToString() %></td>
                        <td><%= rows["Signature7"].ToString() %></td>
                        <td><%= rows["Signature8"].ToString() %></td>
                        <td><%= rows["Signature9"].ToString() %></td>
                        
                        
                    </tr>
                    <% } %>
                </tbody>
                <tfoot>
                    <tr>
                        <th>Actions</th>
                       <th>IDNO</th>
                        <th>RequestNo</th>
                        <th>UserCreate</th>
                        <th>TypeID</th>  
                        <th>DocumentNo</th>
                        <th>TypeSapPMS</th>
                        <th>GL</th>
                        <th>CostCenter</th>
                        <th>IAF</th>
                        <th>QTY</th>
                        <th>AmoutST</th>
                        <th>Department</th>
                        <th>TypeForm</th>

                        <th>Signature1</th>
                        <th>Signature2</th>
                        <th>Signature3</th>
                        <th>Signature4</th>
                        <th>Signature5</th>
                        <th>Signature6</th>
                        <th>Signature7</th>
                        <th>Signature8</th>
                        <th>Signature9</th>

                        
                      

                    </tr>
                </tfoot>
            </table>
        </div>

        <div class="modal fade" id="detailModal" tabindex="-1">
                <div class="modal-dialog modal-xl">
                    <div class="modal-content">

                        <div class="modal-header">
                            <h4 class="modal-title">Comment Request No Detail</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>

                        <div class="modal-body" style="max-height: 500px; overflow-y: auto;">

                            <table class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>NO</th>
                                        <th>RQ</th>
                                        <th>UserCode</th>
                                        <th>FullName</th>
                                        <th>Content_Comment</th>
                                        <th>DateUpdate</th>
                            
                                    </tr>
                                </thead>

                                <tbody id="detailTableBody"></tbody>

                            </table>

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
        function openInNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () { window.document.forms[0].target = ''; }, 0);
        }
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

        $(document).on("click", ".btnNextSign", function (e) {
            e.preventDefault();

            var requestNo = $(this).data("requestno");
            var typeForm = $(this).data("typeform");
            var department = $(this).data("department");

            $.ajax({
                type: "GET",
                url: "frmReportACC.aspx",
                data: {
                    action: "nextSign",
                    requestNo: requestNo,
                    typeForm: typeForm,
                    department: department
                },
                dataType: "json",
                success: function (response) {
                    if (response.result === "SESSION_EXPIRED") {
                        alert("Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.");
                        window.location.href = "Login.aspx";
                        return;
                    }

                    if (response.result === "OK") {
                        alert("Next step: " + response.nextStep + "\nUser next: " + response.userNext);
                        // Nếu muốn reload table:
                        // location.reload();
                        return;
                    }

                    if (response.result === "NG") {
                        alert("Không có dữ liệu next sign.");
                        return;
                    }

                    if (response.result === "ERROR") {
                        alert("Lỗi: " + response.message);
                    }
                },
                error: function (xhr, status, error) {
                    console.log("Status:", xhr.status);
                    console.log("StatusText:", xhr.statusText);
                    console.log("Response:", xhr.responseText);
                    console.log("ContentType:", xhr.getResponseHeader("Content-Type"));
                    alert("Lỗi server: " + xhr.status + "\n" + xhr.responseText);
                }
            });
        });

        function openEditModal2(requestNo, typeForm) {
            $.ajax({
                url: "frmReportACC.aspx?action=getDetailcoment",
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
                        html += "<td>" + data[i].RQ + "</td>";
                        html += "<td>" + data[i].UserCode + "</td>";
                        html += "<td>" + data[i].FullName + "</td>";
                        html += "<td>" + data[i].Content_Comment + "</td>";
                        html += "<td>" + data[i].DateUpdate + "</td>";
                        
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

        

    </script>

    <%--<script src="/plugins/jquery/jquery-ui.js"></script>--%>
    <script type="text/javascript">    

</script>
</body>
</html>
