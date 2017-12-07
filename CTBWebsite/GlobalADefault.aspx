<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GlobalADefault.aspx.cs" Inherits="CTBWebsite.GlobalADefault" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <section id="headerContent" style="height: 530px" class="no-margin slider">
        <div class="carousel slide">
            <div class="carousel-inner">
                <div class="item active" style="background-image: url(images/slider/vehiclehours.jpg)">
                    <div class="banner-overlay">
                        <div class="container">
                            <div class="carousel-content">
                                <div class="center wow fadeInDown">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <h2 style="font-size: 60px; color: white">BLE KeyPass Global A</h2>                                   
                                  
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

    </section>
    <section id="main-slider" class="no-margin">
        <div class="carousel slide">
            <div class="carousel-inner">
                <div class="item active" style="background-color: gainsboro">

                    <div class="container">
                        <div class="carousel-content">
                            
                            
                        </div>
                    </div>
                </div>
                    
            </div>
        </div>
   
        <a id="leftButton" class="prev hidden-xs" runat="server"  visible="false">
            <i class="fa fa-chevron-left"></i>
        </a>
        <a id="rightButton" class="next hidden-xs" runat="server" visible="false">
            <i class="fa fa-chevron-right"></i>
        </a>
    </section>
    <!--/#main-slider-->


















    <!--/#feature-->
    <section id="middle">
        <div class="clients-area center wow fadeInDown">
            <div class="center wow fadeInDown animated">
            </div>
            <div class="center wow fadeInDown animated">
            </div>

        </div>
    </section>



</asp:Content>
