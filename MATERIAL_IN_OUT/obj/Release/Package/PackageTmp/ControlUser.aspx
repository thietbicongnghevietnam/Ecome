<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlUser.aspx.cs" Inherits="MATERIAL_IN_OUT.ControlUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <%--<div class="container-fluid">--%>
       <div class="mailbox-view-area mg-tb-15">
                <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                      <asp:LinkButton ID="lnkUMateral" runat="server" Text=" User Issue IN-OUT" OnClick="lnkUMateral_Click"></asp:LinkButton><%--</li>--%>
                                            &nbsp&nbsp&nbsp
                                            <asp:LinkButton ID="lnkUTranfer" runat="server" Text="UserTranfer" OnClick="lnkUTranfer_Click"></asp:LinkButton><%--</li>--%>
                                
                                    <asp:MultiView ID="mvtUserIssueMaterial" runat="server">
                                        <asp:View ID="vUserIssue" runat="server">
                                        
                                  <div class="container-fluid">
                                                <div class="row">
                                                    <div class="container-fluid">
                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                <center>
                                                             <asp:Label ID = "lblContent" runat ="server"  Font-Size ="25px" Text ="List User Issue In-Out" Font-Bold ="true" >

                                                             </asp:Label>
                                                            </center>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <%--<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">--%>
                                                            <div class="container-fluid">
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="Username">User name</label>
                                                                            <span style="color: green; font-size: 11px; font-style: italic;">(Number only)</span>
                                                                            <asp:TextBox ID="txtUser" CssClass="form-control" OnTextChanged="txtUser_TextChanged" AutoPostBack="true" placeholder="Enter user name" runat="server" onkeyup="if (/\D/g.test(this.value)) this.value = this.value.replace(/\D/g,'')"></asp:TextBox>
                                                                            <%--onserverclick="btnAddClick"--%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="exampleInputEmail1">Full name</label>
                                                                            <asp:TextBox ID="txtFullName" CssClass="form-control" placeholder="Enter full name" runat="server"></asp:TextBox>
                                                                            <%-- end add Modal --%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="exampleInputPassword1">Password</label>
                                                                            <asp:TextBox ID="txtPassword" type="Password" CssClass="form-control" placeholder="Password" runat="server"></asp:TextBox>
                                                                            <%-- The edit Modal --%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="exampleInputPassword2">
                                                                                Confirm Password</label>
                                                                            <asp:TextBox ID="txtRepassword" type="Password" CssClass="form-control" placeholder="Re-Password" runat="server"></asp:TextBox>
                                                                            <span id="span_repassword" style="color: brown; font-style: italic; font-size: 11px;"></span>
                                                                            <%-- Modal Header --%>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="exampleInputEmail1">Email address</label>
                                                                            <asp:TextBox ID="txtEmail" CssClass="form-control" placeholder="Enter email" runat="server"></asp:TextBox>
                                                                            <span id="span_email1" style="color: brown; font-style: italic; font-size: 11px;"></span>
                                                                            <%-- Modal body --%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="Group">Deparment</label>
                                                                            <asp:DropDownList ID="ddl_Dept" runat="server"
                                                                                AppendDataBoundItems="true"
                                                                                CssClass="custom-select custom-select-sm form-control form-control-sm">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="Group">Role</label>
                                                                            <asp:DropDownList ID="ddl_Role" runat="server"
                                                                                AppendDataBoundItems="true"
                                                                                CssClass="custom-select custom-select-sm form-control form-control-sm">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="Group">Role Dept</label>
                                                                            <asp:DropDownList ID="ddl_RoleDept" runat="server"
                                                                                AppendDataBoundItems="true"
                                                                                CssClass="custom-select custom-select-sm form-control form-control-sm">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="Group">Role Page</label>
                                                                            <asp:DropDownList ID="ddl_Rolepage" runat="server"
                                                                                AppendDataBoundItems="true"
                                                                                CssClass="custom-select custom-select-sm form-control form-control-sm">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="exampleInputEmail1">Stock</label>
                                                                            <asp:TextBox ID="txtStock" CssClass="form-control" placeholder="Enter Stock" runat="server"></asp:TextBox>
                                                                            <span id="span_Stock" style="color: brown; font-style: italic; font-size: 11px;"></span>
                                                                            <%-- Modal body --%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="panel-footer">
                                                                    <div class="btn-group">
                                                                        <asp:FileUpload ID="FileUpload1" class="btn btn-default" runat="server" />
                                                                        &nbsp
                                                                <asp:Button ID="bttUpload" class="btn btn-primary" runat="server" Text="Upload" OnClick="bttUpload_Click" />&nbsp
                                                                <asp:Button ID="bttTempFile" class="btn btn-primary" Text="Download Tempfile" runat="server" OnClick="bttTempFile_Click" />&nbsp
                                                                <asp:Button ID="bttPrint" class="btn btn-primary" Text="Report PDF" runat="server" OnClick="bttPrint_Click" />&nbsp
                                                                <button type="button" runat="server" id="btnadd" onserverclick="btnadd_Click" class="btn btn-primary">Save</button>
                                                                    </div>
                                                                    <div class="pull-right">
                                                                        <div class="btn-group" style="">
                                                                            <span style="color: green; font-size: 15px; font-style: oblique;">
                                                                                <asp:Label ID="lblTotal" runat="server"> </asp:Label>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <%--</div>--%>
                                                        </div>


                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                                                                <div class="datatable-dashv1-list custom-datatable-overright">
                                                                    <div id="toolbar">
                                                                        <select class="form-control">
                                                                            <option value="">Export Basic</option>
                                                                            <option value="all">Export All</option>
                                                                            <option value="selected">Export Selected</option>
                                                                        </select>
                                                                    </div>
                                                                    <div style="overflow-y: scroll; height: 350px">
                                                                        <asp:Repeater ID="rptData" runat="server" OnItemCommand="rptData_ItemCommand">
                                                                            <HeaderTemplate>
                                                                                <table id="table" data-toggle="table" data-pagination="true" data-search="true" data-show-columns="true" data-show-pagination-switch="true" data-show-refresh="true" data-key-events="true" data-show-toggle="true" data-resizable="true" data-cookie="true"
                                                                                    data-cookie-id-table="saveId" data-show-export="true" data-click-to-select="true" data-toolbar="#toolbar">
                                                                                    <tr>

                                                                                        <th>User</th>
                                                                                        <th>FullName</th>
                                                                                        <th>Password</th>
                                                                                        <th>Cost Center</th>
                                                                                        <th>Dept</th>
                                                                                        <th>Email</th>
                                                                                        <th>Role Funtion</th>
                                                                                        <th>Role Dept</th>
                                                                                        <th>Stock</th>
                                                                                        <th>Date Create</th>
                                                                                        <th>Action</th>

                                                                                    </tr>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>

                                                                                    <td>
                                                                                        <asp:Label ID="lblUserLogin" runat="server" Text='<%#Eval("UserLogin") %>'></asp:Label></td>
                                                                                    <td><%#Eval("FullName") %></td>
                                                                                    <td><%#Eval("Password") %></td>
                                                                                    <td><%#Eval("Dept") %></td>
                                                                                    <td><%#Eval("DeptName") %></td>
                                                                                    <td><%#Eval("Email") %></td>
                                                                                    <td><%#Eval("RoleName") %></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblRoleName" runat="server" Text='<%#Eval("RoleDept") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblStock" runat="server" Text='<%#Eval("Stock") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td><%#Eval("DateCreate") %></td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="lnkDelete" CommandName="Delete" runat="server" Text="Delete" CssClass="btn btn-danger far fa-trash-alt" OnClientClick="return confirm('Do you want to delete this User?');"></asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                </table>
                                                                            </FooterTemplate>
                                                                        </asp:Repeater>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                  </div>
                                        </asp:View>

                                    </asp:MultiView>

                                    <asp:MultiView ID="mtvUserTranfer" runat="server">
                                        <asp:View ID="vUserTranfer" runat="server">
                                       <div class="container-fluid">
                                                <div class="row">
                                                    <div class="container-fluid">
                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                              <center>
                                                             <asp:Label ID ="lblTitle" runat ="server"  Font-Size ="30px"   Text ="List User Tranfer" Font-Bold ="true" ></asp:Label>
                                                             </center>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <%--<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">--%>
                                                            <div class="container-fluid">
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="Username">User name</label>
                                                                            <span style="color: green; font-size: 11px; font-style: italic;">(Number only)</span>
                                                                            <asp:TextBox ID="txtUser_Tranfer" CssClass="form-control" OnTextChanged="txtUser_Tranfer_TextChanged" AutoPostBack="true" placeholder="Enter user name" runat="server" onkeyup="if (/\D/g.test(this.value)) this.value = this.value.replace(/\D/g,'')"></asp:TextBox>
                                                                            <%--onserverclick="btnAddClick"--%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="exampleInputEmail1">Full name</label>
                                                                            <asp:TextBox ID="txtFull_Tranfer" CssClass="form-control" placeholder="Enter full name" runat="server"></asp:TextBox>
                                                                            <%-- end add Modal --%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="exampleInputPassword1">Password</label>
                                                                            <asp:TextBox ID="txtPass_Tranfer" type="Password" CssClass="form-control" placeholder="Password" runat="server"></asp:TextBox>
                                                                            <%-- The edit Modal --%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="exampleInputPassword2">
                                                                                Confirm Password</label>
                                                                            <asp:TextBox ID="txtRePass_Tranfer" type="Password" CssClass="form-control" placeholder="Re-Password" runat="server"></asp:TextBox>
                                                                            <span id="span_repassword12" style="color: brown; font-style: italic; font-size: 11px;"></span>
                                                                            <%-- Modal Header --%>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="exampleInputEmail1">Email address</label>
                                                                            <asp:TextBox ID="txtEmail_Tranfer" CssClass="form-control" placeholder="Enter email" runat="server"></asp:TextBox>
                                                                            <span id="span_email12" style="color: brown; font-style: italic; font-size: 11px;"></span>
                                                                            <%-- Modal body --%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="Group">Deparment</label>
                                                                            <asp:DropDownList ID="ddlDept_Tranfer" runat="server"
                                                                                AppendDataBoundItems="true"
                                                                                CssClass="custom-select custom-select-sm form-control form-control-sm">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="Group">Role</label>
                                                                            <asp:DropDownList ID="ddlRole_Tranfer" runat="server"
                                                                                AppendDataBoundItems="true"
                                                                                CssClass="custom-select custom-select-sm form-control form-control-sm">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="Group">Role Dept</label>
                                                                            <asp:DropDownList ID="ddlRoleDept_Tranfer" runat="server"
                                                                                AppendDataBoundItems="true"
                                                                                CssClass="custom-select custom-select-sm form-control form-control-sm">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="Group">Role Page</label>
                                                                            <asp:DropDownList ID="ddlRolePage_Tranfer" runat="server"
                                                                                AppendDataBoundItems="true"
                                                                                CssClass="custom-select custom-select-sm form-control form-control-sm">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label for="exampleInputEmail1">Stock</label>
                                                                            <asp:TextBox ID="txtStock_Tranfer" CssClass="form-control" placeholder="Enter Stock" runat="server"></asp:TextBox>
                                                                            <span id="span_Stock12" style="color: brown; font-style: italic; font-size: 11px;"></span>
                                                                            <%-- Modal body --%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="panel-footer">
                                                                    <div class="btn-group">
                                                                        <asp:FileUpload ID="UploadTranfer" class="btn btn-default" runat="server" />
                                                                        &nbsp
                                                                <asp:Button ID="bttUpload_Tranfer" class="btn btn-primary" runat="server" Text="Upload" OnClick="bttUpload_Tranfer_Click" />&nbsp
                                                                <asp:Button ID="bttDownloadTemp_Tranfer" class="btn btn-primary" Text="Download Tempfile" runat="server" OnClick="bttDownloadTemp_Tranfer_Click" />&nbsp
                                                                
                                                                <asp:Button ID="bttPrint_Tranfer" class="btn btn-primary" Text="Report PDF" runat="server" OnClick="bttPrint_Tranfer_Click" />&nbsp
                                                                <button type="button" runat="server" id="bttAdd_Tranfer" class="btn btn-primary" onserverclick="btnadd_Click">
                                                                    >Save</button>
                                                                    </div>
                                                                    <div class="pull-right">
                                                                        <div class="btn-group" style="">
                                                                            <span style="color: green; font-size: 15px; font-style: oblique;">
                                                                                <asp:Label ID="lblTotal_Tranfer" runat="server"> </asp:Label>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <%--</div>--%>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                                                                <div class="datatable-dashv1-list custom-datatable-overright">
                                                                    <div id="toolbar">
                                                                        <select class="form-control">
                                                                            <option value="">Export Basic</option>
                                                                            <option value="all">Export All</option>
                                                                            <option value="selected">Export Selected</option>
                                                                        </select>
                                                                    </div>
                                                                    <div style="overflow-y: scroll; height: 350px">
                                                                        <asp:Repeater ID="rptUser_Tranfer" runat="server" OnItemCommand="rptUser_Tranfer_ItemCommand">
                                                                            <HeaderTemplate>
                                                                                <table id="table" data-toggle="table" data-pagination="true" data-search="true" data-show-columns="true" data-show-pagination-switch="true" data-show-refresh="true" data-key-events="true" data-show-toggle="true" data-resizable="true" data-cookie="true"
                                                                                    data-cookie-id-table="saveId" data-show-export="true" data-click-to-select="true" data-toolbar="#toolbar">
                                                                                    <tr>

                                                                                        <th>User</th>
                                                                                        <th>FullName</th>
                                                                                        <th>Password</th>
                                                                                        <th>Cost Center</th>
                                                                                        <th>Dept</th>
                                                                                        <th>Email</th>
                                                                                        <th>Role Funtion</th>
                                                                                        <th>Role Dept</th>
                                                                                        <th>Stock</th>
                                                                                        <th>Date Create</th>
                                                                                        <th>Action</th>

                                                                                    </tr>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>

                                                                                    <td>
                                                                                        <asp:Label ID="lblUser_Tranfer" runat="server" Text='<%#Eval("UserLogin") %>'></asp:Label></td>
                                                                                    <td><%#Eval("FullName") %></td>
                                                                                    <td><%#Eval("Password") %></td>
                                                                                    <td><%#Eval("Dept") %></td>
                                                                                    <td><%#Eval("DeptName") %></td>
                                                                                    <td><%#Eval("Email") %></td>
                                                                                    <td><%#Eval("RoleName") %></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblRoleName_Tranfer" runat="server" Text='<%#Eval("RoleDept") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblStock_Tranfer" runat="server" Text='<%#Eval("Stock") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td><%#Eval("DateCreate") %></td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="lnkDelete_Tranfer" CommandName="Delete" runat="server" Text="Delete" CssClass="btn btn-danger far fa-trash-alt" OnClientClick="return confirm('Do you want to delete this User?');"></asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                </table>
                                                                            </FooterTemplate>
                                                                        </asp:Repeater>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                    </div>
                                        </asp:View>
                                    </asp:MultiView>
                                    
                            </div>
                        </div>
                    </div>
                
            </div>
   
</asp:Content>

