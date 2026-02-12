<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MATERIAL_IN_OUT.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="x-ua-compatible" content="ie=edge">
    <title>Login  - Control Material System </title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- favicon
		============================================ -->
    <link rel="shortcut icon" type="image/x-icon" href="img/favicon.ico">
    <!-- Google Fonts
		============================================ -->
    <link href="https://fonts.googleapis.com/css?family=Play:400,700" rel="stylesheet">
    <!-- Bootstrap CSS
		============================================ -->
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <!-- Bootstrap CSS
		============================================ -->
    <link rel="stylesheet" href="css/font-awesome.min.css">
    <!-- owl.carousel CSS
		============================================ -->
    <link rel="stylesheet" href="css/owl.carousel.css">
    <link rel="stylesheet" href="css/owl.theme.css">
    <link rel="stylesheet" href="css/owl.transitions.css">
    <!-- animate CSS
		============================================ -->
    <link rel="stylesheet" href="css/animate.css">
    <!-- normalize CSS
		============================================ -->
    <link rel="stylesheet" href="css/normalize.css">
    <!-- main CSS
		============================================ -->
    <link rel="stylesheet" href="css/main.css">
    <!-- morrisjs CSS
		============================================ -->
    <link rel="stylesheet" href="css/morrisjs/morris.css">
    <!-- mCustomScrollbar CSS
		============================================ -->
    <link rel="stylesheet" href="css/scrollbar/jquery.mCustomScrollbar.min.css">
    <!-- metisMenu CSS
		============================================ -->
    <link rel="stylesheet" href="css/metisMenu/metisMenu.min.css">
    <link rel="stylesheet" href="css/metisMenu/metisMenu-vertical.css">
    <!-- calendar CSS
		============================================ -->
    <link rel="stylesheet" href="css/calendar/fullcalendar.min.css">
    <link rel="stylesheet" href="css/calendar/fullcalendar.print.min.css">
    <!-- forms CSS
		============================================ -->
    <link rel="stylesheet" href="css/form/all-type-forms.css">
    <!-- style CSS
		============================================ -->
    <link rel="stylesheet" href="style.css">
    <!-- responsive CSS
		============================================ -->
    <link rel="stylesheet" href="css/responsive.css">
    <!-- modernizr JS
		============================================ -->
    <script src="js/vendor/modernizr-2.8.3.min.js"></script>
</head>
   
<body style="margin:0;">

<form id="form1" runat="server">

<div style="
    position:fixed;
    top:0;
    left:0;
    width:100%;
    height:100%;
    background: linear-gradient(135deg,#004b8d,#0070c0);
    display:flex;
    align-items:center;
    justify-content:center;
">

    <div style="
        width:100%;
        max-width:420px;
        background:white;
        padding:40px;
        border-radius:15px;
        box-shadow:0 10px 30px rgba(0,0,0,0.3);
    ">

        <div style="text-align:center;">
            <h2 style="color:#004b8d; font-weight:bold; margin:0;">
                Panasonic
            </h2>

            <h4 style="margin-top:10px; color:#0070c0;">
                DIGITAL SIGNATURE SYSTEM
            </h4>

            <hr />
        </div>

        <div class="form-group">
            <label><b>Username</b></label>
            <asp:TextBox ID="txtUserName" runat="server"
                CssClass="form-control"
                placeholder="Enter username"></asp:TextBox>
        </div>

        <div class="form-group">
            <label><b>Password</b></label>
            <asp:TextBox ID="txtPassword" runat="server"
                TextMode="Password"
                CssClass="form-control"
                placeholder="Enter password"></asp:TextBox>
        </div>

        <div style="text-align:center;">
            <asp:Label ID="lblMessge"
                runat="server"
                ForeColor="Red"
                Font-Bold="true"></asp:Label>
        </div>

        <asp:Button ID="bttLogin"
            runat="server"
            CssClass="btn btn-block"
            Style="background-color:#0070c0; color:white; font-weight:bold; height:45px;"
            Text="SIGN IN"
            OnClick="bttLogin_Click1" />

        <div style="text-align:center; margin-top:20px; font-size:13px; color:gray;">
            © <%: DateTime.Now.Year %> Panasonic System Networks Viet Nam
        </div>

    </div>

</div>

</form>
</body>




<%--<body>--%>
   <%-- <form id="form1" runat="server">
           <div class="color-line"></div>    
    <div class="container-fluid" style ="margin-top:150px">
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"></div>
            <div class="col-md-4 col-md-4 col-sm-4 col-xs-12">
                <div class="text-center m-b-md custom-login">
                    <h3>LOGIN SYSTEM</h3>
                    <p>&nbsp;</p>
                </div>
                <div class="hpanel">
                    <div class="panel-body">                     
                        <div id="loginForm">
                            <div class="form-group">
                                <label class="control-label" for="username">Username</label>
                                <asp:TextBox ID ="txtUserName" runat ="server"  class="form-control" placeholder="Please enter you username." title="Please enter you username"></asp:TextBox>
                                <span class="help-block small">Your input username to app</span>
                            </div>
                            <div class="form-group">
                                <label class="control-label" for="password">Password</label>
                                <asp:TextBox ID ="txtPassword" runat ="server"  TextMode ="Password" class="form-control" title="Please enter your password" placeholder="******"></asp:TextBox>
                                <span class="help-block small">Your input  password</span>
                            </div>
                            <div class="form-group">
                                
                                <asp:Label ID ="lblMessge" CssClass ="control-label"   ForeColor ="Red" Font-Bold ="true"  Font-Size ="15px"   runat ="server"></asp:Label>
                             </div>
                            
                          <asp:Button ID ="bttLogin" runat ="server" class="btn btn-success btn-block loginbtn" 
                                OnClick="bttLogin_Click1" Text ="LOGIN"  />
                       </div>                        
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"></div>
        </div>
        <div class="row">
            <div class="col-md-12 col-md-12 col-sm-12 col-xs-12 text-center">                
                <p style="text-align:center;">© 11-02-2026 Panasonic System Networks Viet Nam. All rights reserved.</p>
            </div>
        </div>
    </div> 
    </form>--%>

     <!-- jquery
		============================================ -->
 <script src="js/vendor/jquery-1.11.3.min.js"></script>
 <!-- bootstrap JS
		============================================ -->
 <script src="js/bootstrap.min.js"></script>
 <!-- wow JS
		============================================ -->
 <script src="js/wow.min.js"></script>
 <!-- price-slider JS
		============================================ -->
 <script src="js/jquery-price-slider.js"></script>
 <!-- meanmenu JS
		============================================ -->
 <script src="js/jquery.meanmenu.js"></script>
 <!-- owl.carousel JS
		============================================ -->
 <script src="js/owl.carousel.min.js"></script>
 <!-- sticky JS
		============================================ -->
 <script src="js/jquery.sticky.js"></script>
 <!-- scrollUp JS
		============================================ -->
 <script src="js/jquery.scrollUp.min.js"></script>
 <!-- mCustomScrollbar JS
		============================================ -->
 <script src="js/scrollbar/jquery.mCustomScrollbar.concat.min.js"></script>
 <%--<script src="js/scrollbar/mCustomScrollbar-active.js"></script>--%>
 <!-- metisMenu JS
		============================================ -->
 <script src="js/metisMenu/metisMenu.min.js"></script>
 <script src="js/metisMenu/metisMenu-active.js"></script>
 <!-- tab JS
		============================================ -->
 <script src="js/tab.js"></script>
 <!-- icheck JS
		============================================ -->
 <script src="js/icheck/icheck.min.js"></script>
 <script src="js/icheck/icheck-active.js"></script>
 <!-- plugins JS
		============================================ -->
 <script src="js/plugins.js"></script>
 <!-- main JS
		============================================ -->
 <script src="js/main.js"></script>

<%--</body>--%>

</html>
