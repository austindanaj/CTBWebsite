<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CTBWebsite._Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">



    <section id="main-slider" class="no-margin">
        <div class="carousel slide">
            <ol class="carousel-indicators">
                <li data-target="#main-slider" data-slide-to="0" class="active"></li>
                <li data-target="#main-slider" data-slide-to="1"></li>
                <li data-target="#main-slider" data-slide-to="2"></li>
            </ol>
            <div class="carousel-inner">

                <div class="item active" style="background-image: url(images/slider/Gradient.jpg)">
                    <div class="banner-overlay">
                        <div class="container">
                            <div class="row slide-margin">
                                <div class="col-sm-6">
                                    <div class="carousel-content">
                                        <h1>Project Hours</h1>
                                        <div class="progress-wrap animation animated-item-1">
                                            <h3 style="color: #fff;">Advance Dev</h3>
                                            <br />
                                            <div class="progress">
                                                <div id="prgAdvDev" runat="server" class="progress-bar  color1" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                                    <span id="spanAdvDev" runat="server" class="bar-width">0%</span>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="progress-wrap animation animated-item-2">
                                            <h3 style="color: #fff;">Production Dev (Auto)</h3>
                                            <br />
                                            <div class="progress">
                                                <div id="prgProd" runat="server" class="progress-bar color2" role="progressbar" aria-valuenow="20" aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                                    <span id="spanProd" runat="server" class="bar-width">0%</span>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="progress-wrap animation animated-item-3">
                                            <h3 style="color: #fff;">Design in Market (Non-Auto)</h3>
                                            <br />
                                            <div class="progress">
                                                <div id="prgDiM" runat="server" class="progress-bar color3" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                                    <span id="spanDiM" runat="server" class="bar-width">0%</span>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="progress-wrap animation animated-item-4">
                                            <h3 style="color: #fff;">Time Off</h3>
                                            <br />
                                            <div class="progress">
                                                <div id="prgTOff" runat="server" class="progress-bar color4" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                                    <span id="spanTOff" runat="server" class="bar-width">0%</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--/.item-->

                <div class="item" style="background-image: url(images/slider/slide2.jpg)">
                      <div class="banner-overlay">
                    <div class="container">
                        <div class="row slide-margin">
                            <div class="col-sm-6">
                                <div class="carousel-content">
                                      <h1>Employees Out This Week</h1>
                                      <asp:GridView ID="dgvOffThisWeek" Width="80%" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" AllowPaging="true" PageSize="5" OnPageIndexChanging="dgvOffThisWeek_PageIndexChanged" runat="server" ForeColor="WhiteSmoke"  CssClass="table table-bordered table-hover" />
                                   
                                </div>
                            </div>

                            <div class="col-sm-6 hidden-xs animation animated-item-4">
                              
                            </div>
                              </div>
                        </div>
                    </div>
                </div>
                <!--/.item-->

                <div class="item" style="background-image: url(images/slider/slide3.jpg)">
                         <div class="banner-overlay">
                    <div class="container">
                        <div class="row slide-margin">
                           <div class="form-group" style="display: inline-block">
                                    <h1 style="font-weight: 700; font-size: 50px;">Dowload Hours</h1>
                                    <asp:DropDownList ID="ddlselectWeek" runat="server" CssClass="form-control"/><br />
                                    <asp:Button runat="server" OnClick="download" Text="Download" Width="100%" ForeColor="WhiteSmoke" CssClass="btn btn-signin btn-lg" />
                                   </div>
                        </div>
                    </div>
                </div>
                <!--/.item-->
            </div>
            <!--/.carousel-inner-->
        </div>
        <!--/.carousel-->
        <a class="prev hidden-xs" href="#main-slider" data-slide="prev">
            <i class="fa fa-chevron-left"></i>
        </a>
        <a class="next hidden-xs" href="#main-slider" data-slide="next">
            <i class="fa fa-chevron-right"></i>
        </a>
    </section>
    <!--/#main-slider-->
    <section id="feature">
        <div class="container">
            <div class="center wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                <h2>Intern Schedule</h2>
                <div style="width: 200px; text-align: center; align-content: center; display: inline-block">
                    <asp:DropDownList ID="ddlSelectScheduleDay" runat="server" CssClass="form-control " OnSelectedIndexChanged="changeScheduleDay" AutoPostBack="true">
                        <asp:ListItem Text="Monday" />
                        <asp:ListItem Text="Tuesday" />
                        <asp:ListItem Text="Wednesday" />
                        <asp:ListItem Text="Thursday" />
                        <asp:ListItem Text="Friday" />
                    </asp:DropDownList>
                </div>
                <br />
                <br />
                <div class="row">

                    <asp:GridView ID="dgvSchedule" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" runat="server" CssClass="table table-bordered" OnRowDataBound="color" />
                </div>

                <%-- 
                <div class="features">


                    <div class="col-md-4 col-sm-6 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="feature-wrap">
                            <i class="fa fa-th-list"></i>
                            <h2>Menu or Navbar</h2>
                            <h3>A standard navigation class navbar navbar-default</h3>
                        </div>
                    </div><!--/.col-md-4-->

                    <div class="col-md-4 col-sm-6 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="feature-wrap">
                            <i class="fa fa-th"></i>
                            <h2>Grid System</h2>
                            <h3>grid system allows up to 12 columns across the page</h3>
                        </div>
                    </div><!--/.col-md-4-->

                    <div class="col-md-4 col-sm-6 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="feature-wrap">
                            <i class="fa fa-cloud-download"></i>
                            <h2>Easy to customize</h2>
                            <h3>Bootstrap Grid system has four classes - xs, sm, md & lg</h3>
                        </div>
                    </div><!--/.col-md-4-->
                
                    <div class="col-md-4 col-sm-6 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="feature-wrap">
                            <i class="fa fa-comment"></i>
                            <h2>Modal & Tooltip</h2>
                            <h3>Modal is a dialog box/popup, Tooltip is small pop-up box</h3>
                        </div>
                    </div><!--/.col-md-4-->

                    <div class="col-md-4 col-sm-6 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="feature-wrap">
                            <i class="fa fa-cogs"></i>
                            <h2>Grid Settings</h2>
                            <h3>xs (<768px), sm (>=768px), md (>=992px), lg (>=1200px)</h3>
                        </div>
                    </div><!--/.col-md-4-->

                    <div class="col-md-4 col-sm-6 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="feature-wrap">
                            <i class="fa fa-heart"></i>
                            <h2>The Carousel Plugin</h2>
                            <h3>The Carousel plugin is a Slideshow</h3>
                        </div>
                    </div><!--/.col-md-4-->
                </div><!--/.services-->
                --%>
            </div>
            <!--/.row-->
        </div>
        <!--/.container-->
    </section>
    <!--/#feature-->
    


</asp:Content>

