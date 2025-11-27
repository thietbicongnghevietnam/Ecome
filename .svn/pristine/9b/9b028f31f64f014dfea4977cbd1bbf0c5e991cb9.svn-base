<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MATERIAL_IN_OUT.Index2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
       <div class="container-fluid">

            <div class="row">
                </div>
           </div>
        <div class="section-admin container-fluid">
            <div class="row admin text-center">
                <div class="col-md-12">
                    <div class="row">
                    
                    </div>
                </div>
            </div>
        </div>
        <div class="product-sales-area mg-tb-30">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-12">
                        <div class="product-sales-chart">
                            <div class="portlet-title">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <div class="caption pro-sl-hd" style="font-weight: bold; font-size: 20px; text-transform: uppercase">
                                            TYPE OF REQUEST IN&nbsp; MATERIAL IN-OUT SYSTEM</div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <div class="actions graph-rp">
                                            <div class="btn-group" data-toggle="buttons">
                                                <label class="btn btn-grey active">
													<input type="radio" name="options" class="toggle" id="option1" checked="">Today</label>
                                                <label class="btn btn-grey">
													<input type="radio" name="options" class="toggle" id="option2">Week</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <ul class="list-inline cus-product-sl-rp">
                                <li>
                                    <h5><i class="fa fa-circle" style="color: #24caa1;"></i>Issue Material (Form A)</h5>
                                </li>
                                <li>
                                    <h5><i class="fa fa-circle" style="color: #00b5c2;"></i>Issue Material (Form B)</h5>
                                </li>
                                <li>
                                    <h5><i class="fa fa-circle" style="color: #ff7f5a;"></i>Tranfer Material</h5>
                                </li>
                            </ul>
                            <div id="morris-area-chart" style="height: 356px;"></div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                        <div class="white-box analytics-info-cs mg-b-10 res-mg-t-30">
                            <h3 class="box-title">Total Depts</h3>
                            <ul class="list-inline two-part-sp">
                                <li>
                                    <div id="sparkline..0dash"></div>
                                </li>
                                <li class="text-right sp-cn-r">&nbsp;<span class="counter text-success"><asp:Label ID ="lblTotalDept" runat ="server"></asp:Label></span></li>
                            </ul>
                        </div>
                        <div class="white-box analytics-info-cs mg-b-10">
                            <h3 class="box-title">Total User</h3>
                            <ul class="list-inline two-part-sp">
                                <li>
                                    <div id="sparklinedash2"></div>
                                </li>
                                <li class="text-right"> <span class="counter text-purple">
                                    <asp:Label ID ="lblTotalUser" runat ="server"></asp:Label>
                                                                                                         </span></li>
                            </ul>
                        </div>
                        <div class="white-box analytics-info-cs mg-b-10">
                            <h3 class="box-title">Total Vendor</h3>
                            <ul class="list-inline two-part-sp">
                                <li>
                                    <div id="sparklinedash3"></div>
                                </li>
                                <li class="text-right"> <span class="counter text-info">
                                    <asp:Label ID ="lblTotalVendor" runat ="server"></asp:Label>
                                                                                                         </span></li>
                            </ul>
                        </div>
                        <div class="white-box analytics-info-cs">
                            <h3 class="box-title">Total RQ</h3>
                            <ul class="list-inline two-part-sp">
                                <li>
                                    <div id="sparklinedash4"></div>
                                </li>
                                <li class="text-right"> <span class="text-danger"><asp:Label ID ="lblTotalRQ" runat ="server"></asp:Label></span></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="traffic-analysis-area">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                        <div class="white-box tranffic-als-inner">
                            <h3 class="box-title"><small class="pull-right m-t-10 text-success"><i class="fa fa-sort-asc"></i> </small> Issue Material(Form A)</h3>
                            <div class="stats-row">
                                <div class="stat-item">
                                    <h6>Total RQ in Month</h6>
                                    <b><%=total_RQMaterialA_Month%></b></div>
                                <div class="stat-item">
                                    <h6>Total RQ on Day</h6>
                                    <b><%=Total_MaterialA_Day %></b></div>
                            </div>
                            <div id="sparkline8"></div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                        <div class="white-box tranffic-als-inner res-mg-t-30">
                            <h3 class="box-title"><small class="pull-right m-t-10 text-danger"><i class="fa fa-sort-desc"></i> </small>Issue Material (Form B)</h3>
                            <div class="stats-row">
                               
                                <div class="stat-item">
                                    <h6>Total RQ in Month</h6>
                                    <b><%=total_RQMaterialB_Month%></b></div>
                                <div class="stat-item">
                                    <h6>Total RQ on Day</h6>
                                    <b><%=Total_MaterialB_Day%></b></div>
                            </div>
                            <div id="sparkline9"></div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                        <div class="white-box tranffic-als-inner res-mg-t-30">
                            <h3 class="box-title"><small class="pull-right m-t-10 text-success"><i class="fa fa-sort-asc"></i> </small>Tranfer Material</h3>
                            <div class="stats-row">
                                <div class="stat-item">
                                  
                                <div class="stat-item">
                                    <h6>Total RQ in Month</h6>
                                    <b><%=total_Tranfer_Month%></b></div>
                                <div class="stat-item">
                                    <h6>Total RQ on Day</h6>
                                    <b><%=Total_Tranfer_Day%></b></div>
                            </div>
                            <div id="sparkline10"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
       
        <div class="calender-area mg-tb-30">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="calender-inner">
                            <div id='calendar'></div>
                        </div>
                    </div>
                </div>
            </div>
        </div> 
</asp:Content>
