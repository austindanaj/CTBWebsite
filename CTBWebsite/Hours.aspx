<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Hours.aspx.cs" Inherits="CTBWebsite.Hours" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <section id="headerContent" style="height: 530px" class="no-margin slider">
        <div class="carousel slide">
            <div class="carousel-inner">
                <div class="item active" style="background-image: url(images/slider/projecthours.jpg);">
                    <div class="banner-overlay">
                        <div class="container">
                            <div class="carousel-content">
                                <div class="center wow fadeInDown">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <h2 style="font-size: 60px; color: white">Project Hours</h2>
                                    <asp:Label ID="lblWeekOf" runat="server" Text="Week Of: 0/00/0000" Font-Size="36px" />
                                    <br />
                                    <br />
                                    <div style="display: inline-block">

                                        <asp:DropDownList ID="ddlselectWeek" Width="200px" Font-Size="14px" Height="40px" CssClass="form-control" runat="server" />
                                    </div>

                                    <div style="display: inline-block">
                                        <asp:Button ID="btnselectWeek" Width="50px" Height="40px" AutoPostBack="true" CssClass="btn btn-lg btn-primary btn-block btn-signin" runat="server" Text="Go" OnClick="TriggerEvent" />


                                    </div>
                                    <br />
                                    <br />
                                    <div class="form-group">
                                        <div class="checkbox">
                                            <asp:CheckBox ID="chkInactive" OnCheckedChanged="TriggerEvent" runat="server" Style="color: white;" Text=" Show Inactives" />
                                        </div>

                                    </div>


                                    <br />
                                    <%-- <p class="lead">Easy to use, Responsive features, Mobile-first approach <br> Browser compatibility Bootstrap is compatible with all modern browsers</p>--%>
                                </div>

                            </div>
                        </div>
                        <!--/.services-->
                    </div>
                    <!--/.row-->
                </div>
            </div>
        </div>

        <!--/.container-->
    </section>
    <section id="main-slider" class="no-margin">
        <div class="carousel slide">
            <div class="carousel-inner">
                <div class="item active" style="background-color: gainsboro">
                    <div class="container">
                        <div class="carousel-content">
                            <div id="pnlAddHours" runat="server" class="col-md-6 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                                <div class="col-lg-10">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <h1 runat="server" id="lblProjectTitle" style="font-weight: 700; color: #525252; font-size: 50px;">Project Hours</h1>
                                        <br />
                                        <br />
                                        <asp:DropDownList ID="ddlProjects" CssClass="form-control" runat="server" />
                                        <br />
                                        <br />
                                        <asp:DropDownList ID="ddlHours" CssClass="form-control" runat="server" />
                                        <br />
                                        <br />
                                        <asp:Button ID="btnSubmitPercent" runat="server" OnClick="TriggerEvent" Text="Submit" CssClass="btn btn-lg btn-primary btn-block" Text-Align="Center" />
                                        <br />
                                        <asp:Label ID="lblUserHours" runat="server" ForeColor="#999999" Font-Size="X-Small" Text="Your Hours: 0/40" />
                                    </asp:Panel>
                                </div>
                            </div>
                            <div id="pnlVehicleHours" visible="false" runat="server" class="col-md-6  wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                                <div class="col-lg-10">
                                    <div class="feature-wrap">

                                        <asp:Panel runat="server">
                                            <h1 runat="server" id="lblVehicleTitle" style="font-weight: 700; color: #525252; font-size: 50px;" visible="false">Vehicle Hours</h1>
                                            <br />
                                            <br />
                                            <asp:DropDownList ID="ddlVehicles" CssClass="form-control" runat="server" Visible="false" />
                                            <br />
                                            <br />
                                            <asp:DropDownList ID="ddlHoursVehicles" runat="server" CssClass="form-control" Visible="false" />
                                            <br />
                                            <br />
                                            <asp:Button ID="btnSubmitVehicles" AutoPostBack="true" OnClick="TriggerEvent" runat="server" Text="Submit" CssClass="btn btn-lg btn-primary btn-block" Text-Align="Center" Visible="false" />
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div id="pnlDeleteHours" visible="false" runat="server" class="col-md-6 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                                <div class="col-lg-10">
                                    <div class="feature-wrap">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <h1 runat="server" id="H1" style="font-weight: 700; color: #525252; font-size: 50px;">Delete Hours</h1>
                                            <br />
                                            <br />
                                            <asp:DropDownList ID="ddlWorkedHours" CssClass="form-control" runat="server" />
                                            <br />
                                            <br />
                                            <asp:TextBox ID="txtDelete" runat="server" CssClass="form-control" placeholder="Type YES to confirm." />
                                            <br />
                                            <br />
                                            <asp:Button ID="btnDelete" AutoPostBack="true" runat="server" OnClick="TriggerEvent" Text="Delete" CssClass="btn btn-lg btn-primary btn-block" BackColor="#d9534f" Visible="true" />

                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>


        <!--/.carousel-->
        <a id="leftButton" class="prev hidden-xs" runat="server" onserverclick="TriggerEvent" visible="false">
            <i class="fa fa-chevron-left"></i>
        </a>
        <a id="rightButton" class="next hidden-xs" runat="server" onserverclick="TriggerEvent" visible="false">
            <i class="fa fa-chevron-right"></i>
        </a>
    </section>
    <!--/#main-slider-->


















    <!--/#feature-->
    <section id="middle">
        <div class="clients-area center wow fadeInDown">

            <div class="center wow fadeInDown animated">
                <asp:GridView ID="dgvProject" runat="server" ForeColor="WhiteSmoke" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" AllowPaging="true" PageSize="30" OnPageIndexChanging="dgvProject_PageIndexChanged" CssClass="table table-bordered table-hover table-responsive"
                    OnRowDataBound="color" />
            </div>
            <div class="center wow fadeInDown animated">
                <asp:GridView ID="dgvCars" runat="server" ForeColor="WhiteSmoke" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" CssClass="table table-bordered table-hover table-responsive"
                    Visible="false" />

            </div>


        </div>
    </section>
</asp:Content>
