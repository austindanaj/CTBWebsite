﻿<%@ Page Language="C#" ValidateRequest="false" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GlobalADefault.aspx.cs" Inherits="CTBWebsite.GlobalADefault" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


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
                                    <div class="row" style="text-align: center">
                                        <div class="col-md-3" style="text-align: center">
                                        </div>
                                        <div class="col-md-3" style="text-align: center">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="CreateReport" CssClass="linkButton" OnClick="uploadPanel" runat="server" Text='Create Report' />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-md-3" style="text-align: center">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="UploadFile" CssClass="linkButton" OnClick="uploadPanel" runat="server" Text='Upload File' />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-md-3" style="text-align: center">
                                        </div>

                                    </div>
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3" style="text-align: center">
                                        </div>
                                        <div class="col-md-3" style="text-align: center">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="UploadImage" CssClass="linkButton" OnClick="uploadPanel" runat="server" Text='Upload Image' />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-md-3" style="text-align: center">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="UploadTool" CssClass="linkButton" OnClick="uploadPanel" runat="server" Text='Upload Tool' />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-md-3" style="text-align: center">
                                        </div>

                                    </div>

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

    <!--
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <h2>ASP.NET is great for building standards-based websites with HTML5, CSS3, and JavaScript. </h2>
                    <p class="lead">ASP.NET supports three approaches for making web sites.<a target="_blank" href="http://www.asp.net/get-started"> it's all One ASP.NET</a>.</p>
                    <p class="lead">ASP.NET Web Forms uses controls and an event-model for component-based development. ASP.NET MVC values separation of concerns and enables easier test-driven development.</p>
                </div>
            </div>
        </div>
        -->
    <div id="Tabs" role="tabpanel" style="text-align: center;">
        <!-- Nav tabs -->
        <ul class="nav nav-tabs" id="gaTabs" role="tablist">
            <li class="active"><a href="#Reports" runat="server" id="tabReports" aria-controls="settings" data-toggle="tab">Reports</a></li>
            <li><a href="#Files" runat="server" id="tabFiles" aria-controls="info" data-toggle="tab">Files</a></li>
            <li><a href="#Images" runat="server" id="tabImages" aria-controls="info" data-toggle="tab">Images</a></li>
            <li><a href="#Tools" runat="server" id="tabTools" aria-controls="info" data-toggle="tab">Tools</a></li>
        </ul>
    </div>

    <aside class="callout">
        <div class="text-vertical-center">
            <h1 id="lblMainTitle" runat="server">Reports</h1>
        </div>
    </aside>
    <section id="services" class="services bg-primary">

        <div style="padding-left: 10%; padding-right: 10%;">
            <div class="row tab-content">
                <div class="tab-pane active" id="Reports">
                    <!-- Report Tabs -->
                    <asp:UpdatePanel runat="server" ID="udpReport" ChildrenAsTriggers="True">
                        <ContentTemplate>
                            <div class="row">
                                <div class="container">
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlVehicleReportFilter" OnSelectedIndexChanged="applyFilter" CssClass="form-control" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="-- Vehicle Filter --" Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlPhoneReportFilter" OnSelectedIndexChanged="applyFilter" CssClass="form-control" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="-- Phone Filter --" Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlEmployeeReportFilter" OnSelectedIndexChanged="applyFilter" CssClass="form-control" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="-- Author Filter --" Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <div class='input-group date' id='datetimepicker4'>
                                            <asp:TextBox runat="server" placeholder="-- Date Filter --" OnTextChanged="applyFilter" ID="txtReportFilterDate" CssClass="form-control" Style="margin-bottom: 0px;" />
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <br />
                            <div class="row">

                                <asp:Panel ID="pnlReports" runat="server">
                                    <asp:GridView ID="dgvReports" runat="server"
                                        HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-HorizontalAlign="Center"
                                        RowStyle-VerticalAlign="Middle"
                                        RowStyle-HorizontalAlign="Center"
                                        AllowPaging="true"
                                        AllowSorting="True"
                                        OnSorting="sort"
                                        PageSize="30"
                                        OnRowCommand="onRowCommand"
                                        AutoGenerateColumns="False"
                                        OnPageIndexChanging="nextPage"
                                        CssClass="table table-bordered table-responsive">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" AutoPostBack="true" ID="lnkEditReport"
                                                        CommandName="Edit_Report" CommandArgument='<%#Eval("ID")%>'
                                                        Text='Edit'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" SortExpression="ID">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Report" SortExpression="Report_Name">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" AutoPostBack="true" ID="lnkReportFile"
                                                        CommandName="Download_File" CommandArgument='<%#"R_" + Eval("ID")%>'
                                                        Text='<%# Eval("Report_Name") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vehicle" SortExpression="Vehicle_Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Vehicle_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phone" SortExpression="Phone_Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Phone_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Calibration" SortExpression="Calibration_Name">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkReportCalibration"
                                                        CommandName="Download_File" CommandArgument='<%#"F_" + Eval("TD0_ID")%>'
                                                        Text='<%# Eval("TD0_Name") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TD1" SortExpression="TD1_Name">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkReportTD1"
                                                        CommandName="Download_File" CommandArgument='<%#"F_" + Eval("TD1_ID")%>'
                                                        Text='<%# Eval("TD1_Name") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TD2" SortExpression="TD2_Name">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkReportTD2"
                                                        CommandName="Download_File" CommandArgument='<%#"F_" + Eval("TD2_ID") %>'
                                                        Text='<%# Eval("TD2_Name") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TD3" SortExpression="TD3_Name">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkReportTD3"
                                                        CommandName="Download_File" CommandArgument='<%#"F_" + Eval("TD3_ID") %>'
                                                        Text='<%# Eval("TD3_Name") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TD4" SortExpression="TD4_Name">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkReportTD4"
                                                        CommandName="Download_File" CommandArgument='<%#"F_" + Eval("TD4_ID")%>'
                                                        Text='<%# Eval("TD4_Name") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Author 1" SortExpression="Author_1">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Author_1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Author 2" SortExpression="Author_2">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Author_2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Created" SortExpression="Date_Created">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("FormDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Uploaded" SortExpression="Date_Inserted">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Date_Inserted") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Updated" SortExpression="Date_Updated">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Date_Updated") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comment" SortExpression="Comment">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Comment") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active" SortExpression="Active">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Active") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="tab-pane" id="Files">
                    <!-- Files Tabs -->
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="container">

                                    <div style="width: 20%" class="col-md-2">
                                        <asp:DropDownList ID="ddlFileVehicleFilter" OnSelectedIndexChanged="applyFilter" CssClass="form-control" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="-- Vehicle Filter --" Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                    <div style="width: 20%" class="col-md-2">
                                        <asp:DropDownList ID="ddlFilePhoneFilter" OnSelectedIndexChanged="applyFilter" CssClass="form-control" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="-- Phone Filter --" Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                    <div style="width: 20%" class="col-md-2">
                                        <asp:DropDownList ID="ddlFileFilterType" OnSelectedIndexChanged="applyFilter" CssClass="form-control" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="-- File Type Filter --" Value="-1" />
                                            <asp:ListItem Text="Calibration" Value="0" />
                                            <asp:ListItem Text="TD1" Value="1" />
                                            <asp:ListItem Text="TD2" Value="2" />
                                            <asp:ListItem Text="TD3" Value="3" />
                                            <asp:ListItem Text="TD4" Value="4" />
                                        </asp:DropDownList>
                                    </div>
                                    <div style="width: 20%" class="col-md-2">
                                        <asp:DropDownList ID="ddlFileAuthorFilter" OnSelectedIndexChanged="applyFilter" CssClass="form-control" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="-- Author Filter --" Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                    <div style="width: 20%" class="col-md-2">
                                        <div class='input-group date' id='datetimepicker5'>
                                            <asp:TextBox runat="server" placeholder="-- Date Filter --" OnTextChanged="applyFilter" ID="txtFileDateFilter" CssClass="form-control" Style="margin-bottom: 0px;" />
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <br />
                            <div class="row">

                                <asp:Panel ID="pnlFiles" runat="server">
                                    <asp:GridView ID="dgvFiles" runat="server"
                                        HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-HorizontalAlign="Center"
                                        RowStyle-VerticalAlign="Middle"
                                        RowStyle-HorizontalAlign="Center"
                                        AllowPaging="true"
                                        OnSorting="sort"
                                        AllowSorting="true"
                                        PageSize="30"
                                        OnRowCommand="onRowCommand"
                                        AutoGenerateColumns="False"
                                        OnPageIndexChanging="nextPage"
                                        CssClass="table table-bordered table-responsive">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" AutoPostBack="true" ID="lnkEditFile"
                                                        CommandName="Edit_File" CommandArgument='<%#Eval("ID")%>'
                                                        Text='Edit'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" SortExpression="ID">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" SortExpression="F_Name">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkDownload"
                                                        CommandName="Download_File" CommandArgument='<%#"F_" + Eval("ID")%>'
                                                        Text='<%# Eval("F_Name") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Vehicle" SortExpression="Vehicle_Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Vehicle_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phone" SortExpression="Phone_Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Phone_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File Type" SortExpression="Type">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Type") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Author 1" SortExpression="Author_1">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Author_1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Author 2" SortExpression="Author_2">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Author_2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Created" SortExpression="Date_Created">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Date_Created") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Uploaded" SortExpression="Date_Inserted">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Date_Inserted") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Updated" SortExpression="Date_Updated">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Date_Updated") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comment" SortExpression="Comment">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Comment") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active" SortExpression="Active">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                               Text='<%# Eval("Active") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane" id="Images">
                    <!-- Images Tabs -->

                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>


                            <div class="row">
                                <div class="container">

                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlImageVehicleFilter" OnSelectedIndexChanged="applyFilter" CssClass="form-control" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="-- Vehicle Filter --" Value="-1" />
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlImageAuthorFilter" OnSelectedIndexChanged="applyFilter" CssClass="form-control" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="-- Author Filter --" Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <div class='input-group date' id='datetimepicker6'>
                                            <asp:TextBox runat="server" placeholder="-- Date Filter --" OnTextChanged="applyFilter" ID="txtImageDateFilter" CssClass="form-control" Style="margin-bottom: 0px;" />
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <br />
                            <div class="row">
                                <asp:Panel ID="pnlImages" runat="server">
                                    <asp:GridView ID="dgvImages" runat="server"
                                        HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-HorizontalAlign="Center"
                                        RowStyle-VerticalAlign="Middle"
                                        RowStyle-HorizontalAlign="Center"
                                        AllowPaging="true"
                                        OnSorting="sort"
                                        AllowSorting="True"
                                        PageSize="30"
                                        OnRowCommand="onRowCommand"
                                        AutoGenerateColumns="False"
                                        OnPageIndexChanging="nextPage"
                                        CssClass="table table-bordered table-responsive">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" AutoPostBack="true" ID="lnkEditImage"
                                                        CommandName="Edit_Image" CommandArgument='<%#Eval("ID")%>'
                                                        Text='Edit'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" SortExpression="ID">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkImageFile"
                                                        CommandName="Download_File" CommandArgument='<%#"I_" + Eval("ID")%>'
                                                        Text='<%# Eval("Image_Name") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Vehicle" SortExpression="F">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Vehicle_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Author 1" SortExpression="Author_1">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Author_1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Created" SortExpression="Date_Created">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Date_Created") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Uploaded" SortExpression="Date_Inserted">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Date_Inserted") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Updated" SortExpression="Date_Updated">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Date_Updated") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comment" SortExpression="Comment">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Text='<%# Eval("Comment") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active" SortExpression="Active">
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                               Text='<%# Eval("Active") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane" id="Tools">
                    <!-- Tools Tabs -->
                    <asp:Panel ID="pnlTools" runat="server">
                        <asp:ListView ID="lstTools" runat="server" GroupItemCount="3" OnItemCommand="lstTools_OnItemCommand" DataKeyNames="Alna_num">
                            <EmptyDataTemplate>
                                <table>
                                    <tr>
                                        <td>No data was returned</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <EmptyItemTemplate>
                                <td />
                            </EmptyItemTemplate>
                            <GroupTemplate>
                                <div style="clear: both;">
                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
                                </div>
                            </GroupTemplate>
                            <ItemTemplate>
                                <div class="productItem">
                                    <asp:LinkButton ID="lnkEditClicked" Font-Size="12px" ForeColor="#00358c" CssClass="closeNotification" CommandName="Edit_Tool" CommandArgument='<%#Eval("ID") %>' runat="server" Text='Edit' />
                                    <div>
                                        <img src='<%# "images/icons/" + Eval("IconType") %>'
                                            height="120" width="120" />

                                    </div>
                                    <br />
                                    <div>
                                        <b>
                                            <asp:LinkButton ID="lblToolClick" CssClass="itemTitle" CommandName="Download_Tool" CommandArgument='<%#Eval("ID") %>' runat="server" Text='<%#Eval("Folder_Name")%>' />
                                        </b>
                                    </div>
                                    <div>
                                        <div>
                                            <span class="itemText">
                                                <b>Updated: </b><%#:Eval("FormDate")%>
                                            </span>
                                            <br />
                                            <span class="itemText">
                                                <%#:Eval("Version")%>
                                            </span>
                                            <pre style="height: 125px; overflow: auto; text-align: left; border: transparent; white-space: pre-line; background: gainsboro">
                                                <asp:Label runat="server" Text='Release Notes:'></asp:Label>
                                                <asp:Label runat="server" Text='<%#:Eval("Comment")%>'></asp:Label>
                                               </pre>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <ItemSeparatorTemplate>
                                <div class="itemSeparator"></div>
                            </ItemSeparatorTemplate>
                            <GroupSeparatorTemplate>
                                <div class="groupSeparator"></div>
                            </GroupSeparatorTemplate>
                            <LayoutTemplate>
                                <div style="width: 100%;">
                                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder" />
                                </div>
                            </LayoutTemplate>
                        </asp:ListView>


                    </asp:Panel>
                </div>

            </div>
            <!-- /.row -->
        </div>
        <!-- /.container -->
    </section>

    <%--
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

        <a id="leftButton" class="prev hidden-xs" runat="server" visible="false">
            <i class="fa fa-chevron-left"></i>
        </a>
        <a id="rightButton" class="next hidden-xs" runat="server" visible="false">
            <i class="fa fa-chevron-right"></i>
        </a>
    </section>
   /#main-slider

    
    
    --%>

    <asp:Button ID="hiddenButton1" runat="server" Style="display: none" />
    <asp:Button ID="hiddenButton2" runat="server" Style="display: none" />
    <asp:Button ID="hiddenButton3" runat="server" Style="display: none" />
    <asp:Button ID="hiddenButton4" runat="server" Style="display: none" />


    <ajaxToolkit:ModalPopupExtender ID="mpeReports" runat="server" TargetControlID="hiddenButton1" PopupControlID="addReport" BackgroundCssClass="modalBackground" CancelControlID="CancelClickedReport" />
    <ajaxToolkit:ModalPopupExtender ID="mpeFiles" runat="server" TargetControlID="hiddenButton2" PopupControlID="addFile" BackgroundCssClass="modalBackground" CancelControlID="CancelClickedFile" />
    <ajaxToolkit:ModalPopupExtender ID="mpeImages" runat="server" TargetControlID="hiddenButton3" PopupControlID="addImage" BackgroundCssClass="modalBackground" CancelControlID="CancelClickedImage" />
    <ajaxToolkit:ModalPopupExtender ID="mpeTools" runat="server" TargetControlID="hiddenButton4" PopupControlID="addTool" BackgroundCssClass="modalBackground" CancelControlID="CancelClickedTool" />

    <!--Panel Add Report -->
    <asp:Panel ID="addReport" runat="server" Style="display: none;" DefaultButton="btnSubmitReport">

        <div class="modal fade reportModal in" data-backdrop="static" tabindex="-1" aria-hidden="false" style="display: block">
            <div class="report-card report-container">
                <div class="form-signin">
                    <div class="modalReportHeader">
                        <div class="row" style="padding-left: 15px;">

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblReportTitle" runat="server" Style="font-size: 32px; font: bold;" Text="Create Report" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <span id="CancelClickedReport" runat="server" class="close">&times;</span>
                        </div>

                        <div class="row" style="padding-left: 15px;">
                            <asp:Label ID="Label5" runat="server" Style="font-size: 12px; font: bold;" Text="*You need to upload the files before creating/updating a report" />
                        </div>


                    </div>

                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>



                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlVehicles" ValidationGroup="ReportGroup" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="onSelectedIndexChanged">
                                            <asp:ListItem Text="-- Select a Vehicle --" Value="-1" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red"
                                            ControlToValidate="ddlVehicles" InitialValue="-1"
                                            ValidationGroup="ReportGroup"
                                            ErrorMessage="Select a vehicle."
                                            runat="Server">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlCalibration" ValidationGroup="ReportGroup" CssClass="form-control" runat="server">
                                        <asp:ListItem Text="-- Select a Calibration --" Value="-1" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red"
                                        ControlToValidate="ddlCalibration" InitialValue="-1"
                                        ValidationGroup="ReportGroup"
                                        ErrorMessage="Select a calibration."
                                        runat="Server">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlPhones" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="onSelectedIndexChanged" runat="server">
                                            <asp:ListItem Text="-- Select a Phone --" Value="-1" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red"
                                            ControlToValidate="ddlPhones" InitialValue="-1"
                                            ValidationGroup="ReportGroup"
                                            ErrorMessage="Select a phone."
                                            runat="Server">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlTD1" ValidationGroup="ReportGroup" CssClass="form-control" runat="server">
                                        <asp:ListItem Text="-- Select a TD1 File --" Value="-1" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ForeColor="Red"
                                        ControlToValidate="ddlTD1" InitialValue="-1"
                                        ValidationGroup="ReportGroup"
                                        ErrorMessage="Select a TD1."
                                        runat="Server">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlAuthor1" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="-- Select Author 1 --" Value="-1" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red"
                                            ControlToValidate="ddlAuthor1" InitialValue="-1"
                                            ValidationGroup="ReportGroup"
                                            ErrorMessage="Select a Name."
                                            runat="Server">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlTD2" ValidationGroup="ReportGroup" CssClass="form-control" runat="server">
                                        <asp:ListItem Text="-- Select a TD2 File --" Value="-1" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ForeColor="Red"
                                        ControlToValidate="ddlTD2" InitialValue="-1"
                                        ValidationGroup="ReportGroup"
                                        ErrorMessage="Select a TD2."
                                        runat="Server">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlAuthor2" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="-- Select Author 2 --" Value="-2" />
                                            <asp:ListItem Text="N/A" Value="-1" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ForeColor="Red"
                                            ControlToValidate="ddlAuthor2" InitialValue="-2"
                                            ValidationGroup="ReportGroup"
                                            ErrorMessage="Select a Name."
                                            runat="Server">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlTD3" ValidationGroup="ReportGroup" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="-- Select a TD3 File --" Value="-1" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ForeColor="Red"
                                            ControlToValidate="ddlTD3" InitialValue="-1"
                                            ValidationGroup="ReportGroup"
                                            ErrorMessage="Select a TD3."
                                            runat="Server">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <div class='input-group date' id='datetimepicker1'>
                                            <input id="txtReportDate" runat="server" runtype='text' placeholder="-- Date Report Completed --" style="margin-bottom: 0px;" class="form-control" />
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ForeColor="Red"
                                            ControlToValidate="txtReportDate" InitialValue=""
                                            ValidationGroup="ReportGroup"
                                            ErrorMessage="Select a Date."
                                            runat="Server">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlTD4" ValidationGroup="ReportGroup" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="-- Select a TD4 File --" Value="-1" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ForeColor="Red"
                                            ControlToValidate="ddlTD4" InitialValue="-1"
                                            ValidationGroup="ReportGroup"
                                            ErrorMessage="Select a TD4."
                                            runat="Server">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding-right: 15px; padding-left: 15px;" runat="server">

                                <div class="form-group" id="rfuDiv" runat="server">
                                    <label class="btn btn-block form-control" style="cursor: pointer;">
                                        Browse...
                                        <input runat="server" style="display: none" onchange="setfile(0)" id="rfu" type="file" />
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ForeColor="Red"
                                        ControlToValidate="fileSelected"
                                        InitialValue=""
                                        ValidationGroup="ReportGroup"
                                        ErrorMessage="Select a File."
                                        runat="Server">
                                    </asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group" style="display: none" id="rfuHasFile" runat="server">
                                    <asp:Label ID="lblRFU" runat="server" Style="font-size: 20px; font: bold;" Text="Testing" />
                                    <span onclick="clearfile(0)" style="color: #333333" class="close">&times;</span>
                                </div>

                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtReportComment" placeholder="Comment" runat="server" CssClass="form-control" Font-Size="Medium" Style="max-height: 200px; max-width: 100%;" Height="200px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:CheckBox runat="server" Text="Active" ID="chkReportActive" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnSubmitReport" class="btn btn-lg btn-primary btn-block btn-signin" runat="server" OnClick="btnSubmitReport_OnClick" Text="Submit Report" type="submit" ValidationGroup="ReportGroup" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSubmitReport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>


    </asp:Panel>

    <!--Panel Add File -->
    <asp:Panel ID="addFile" runat="server" DefaultButton="UploadClicked" Style="display: none;">
        <div class="modal fade reportModal in" data-backdrop="static" tabindex="-1" aria-hidden="false" style="display: block">
            <div class="report-card report-container">
                <div class="form-signin">
                    <div class="modalHeader">
                        <asp:Label ID="Label2" runat="server" Style="font-size: 32px; font: bold;" Text="Upload File" />
                        <span id="CancelClickedFile" class="close">&times;</span>
                        <br />
                    </div>
                    <div>
                        <div>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlFileVehicle" runat="server" ValidationGroup="UploadGroup" CssClass="form-control">
                                                    <asp:ListItem Text="-- Select a Vehicle --" Value="-1" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red"
                                                    ControlToValidate="ddlFileVehicle" InitialValue="-1"
                                                    ValidationGroup="UploadGroup"
                                                    ErrorMessage="Select a vehicle."
                                                    runat="Server">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-6">

                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlFileAuthor1" runat="server" ValidationGroup="UploadGroup" CssClass="form-control">
                                                    <asp:ListItem Text="-- Select Author 1 --" Value="-1" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red"
                                                    ControlToValidate="ddlFileAuthor1" InitialValue="-1"
                                                    ValidationGroup="UploadGroup"
                                                    ErrorMessage="Select an Author."
                                                    runat="Server">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">


                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlFilePhone" runat="server" ValidationGroup="UploadGroup" CssClass="form-control">
                                                    <asp:ListItem Text="-- Select a Phone --" Value="-1" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red"
                                                    ControlToValidate="ddlFilePhone" InitialValue="-1"
                                                    ValidationGroup="UploadGroup"
                                                    ErrorMessage="Select a Phone."
                                                    runat="Server">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlFileAuthor2" runat="server" ValidationGroup="UploadGroup" CssClass="form-control">
                                                    <asp:ListItem Text="-- Select Author 2 --" Value="-2" />
                                                    <asp:ListItem Text="N/A" Value="-1" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ForeColor="Red"
                                                    ControlToValidate="ddlFileAuthor2" InitialValue="-2"
                                                    ValidationGroup="UploadGroup"
                                                    ErrorMessage="Select a Name."
                                                    runat="Server">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlFileType" runat="server" ValidationGroup="UploadGroup" CssClass="form-control">
                                                    <asp:ListItem Text="-- Select a File Type --" Value="-1" />
                                                    <asp:ListItem Text="Calibration" Value="0" />
                                                    <asp:ListItem Text="TD1" Value="1" />
                                                    <asp:ListItem Text="TD2" Value="2" />
                                                    <asp:ListItem Text="TD3" Value="3" />
                                                    <asp:ListItem Text="TD4" Value="4" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red"
                                                    ControlToValidate="ddlFileType" InitialValue="-1"
                                                    ValidationGroup="UploadGroup"
                                                    ErrorMessage="Select a File Type."
                                                    runat="Server">
                                                </asp:RequiredFieldValidator>
                                            </div>



                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <div class='input-group date' id='datetimepicker2'>
                                                    <input type='text' id="txtFileDate" runat="server" placeholder="-- Date Test Completed --" class="form-control" style="margin-bottom: 0px;" />
                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ForeColor="Red"
                                                    ControlToValidate="txtFileDate" InitialValue=""
                                                    ValidationGroup="UploadGroup"
                                                    ErrorMessage="Select a Date."
                                                    runat="Server">
                                                </asp:RequiredFieldValidator>
                                            </div>


                                        </div>
                                    </div>

                                    <div class="row">

                                        <div class="form-group" style="padding-left: 15px; padding-right: 15px;" id="ffuDiv" runat="server">
                                            <label class="btn btn-block form-control" style="cursor: pointer;">
                                                Browse...
                                            <input runat="server" style="display: none" onchange="setfile(1)" id="ffu" type="file" />
                                            </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ForeColor="Red"
                                                ControlToValidate="fileSelected"
                                                InitialValue=""
                                                ValidationGroup="UploadGroup"
                                                ErrorMessage="Select a File."
                                                runat="Server">
                                            </asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group" style="padding-left: 15px; padding-right: 15px; display: none" id="ffuHasFile" runat="server">
                                            <asp:Label ID="lblFFU" runat="server" Style="font-size: 20px; font: bold;" Text="Testing" />
                                            <span onclick="clearfile(1)" style="color: #333333" class="close">&times;</span>

                                        </div>



                                    </div>

                                    <div class="form-group">
                                        <asp:TextBox ID="txtFileComment" placeholder="Comment" runat="server" Style="max-height: 200px; max-width: 100%;" Height="200px" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:CheckBox runat="server" Text="Active" ID="chkFileActive" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="UploadClicked" class="btn btn-lg btn-primary btn-block btn-signin" OnClick="btnSubmitFile_OnClick" runat="server" Text="Upload File" type="submit" ValidationGroup="UploadGroup" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="UploadClicked" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                </div>
            </div>
        </div>


    </asp:Panel>

    <!--Panel Add Image -->
    <asp:Panel ID="addImage" runat="server" Style="display: none;">
        <div class="modal fade reportModal in" data-backdrop="static" tabindex="-1" aria-hidden="false" style="display: block">
            <div class="tool-card tool-container">
                <div class="form-signin">
                    <div class="modalHeader">
                        <asp:Label ID="Label3" runat="server" Style="font-size: 32px; font: bold;" Text="Upload Image" />
                        <span id="CancelClickedImage" class="close">&times;</span>
                        <br />
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="row" style="padding-right: 15px; padding-left: 15px;">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlImageVehicle" runat="server" ValidationGroup="ImageGroup" CssClass="form-control">
                                            <asp:ListItem Text="-- Select a Vehicle --" Value="-1" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ForeColor="Red"
                                            ControlToValidate="ddlImageVehicle" InitialValue="-1"
                                            ValidationGroup="ImageGroup"
                                            ErrorMessage="Select a vehicle."
                                            runat="Server">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row" style="padding-right: 15px; padding-left: 15px;">
                                    <div class="form-group">
                                        <div class='input-group date' id='datetimepicker3'>
                                            <input type='text' id="txtImageDate" runat="server" placeholder="-- Date Picture was Taken --" class="form-control" style="margin-bottom: 0px;" />
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ForeColor="Red"
                                            ControlToValidate="txtImageDate" InitialValue=""
                                            ValidationGroup="ImageGroup"
                                            ErrorMessage="Select a Date."
                                            runat="Server">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>


                                <div class="row" style="padding-right: 15px; padding-left: 15px;">
                                    <div class="form-group" id="ifuDiv" runat="server">
                                        <label class="btn btn-block form-control" style="cursor: pointer;">
                                            Browse...
                                        <input runat="server" style="display: none" onchange="setfile(2)" id="ifu" type="file" name="file" />
                                        </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ForeColor="Red"
                                            ControlToValidate="fileSelected"
                                            InitialValue=""
                                            ValidationGroup="ImageGroup"
                                            ErrorMessage="Select a File."
                                            runat="Server">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group" style="display: none" id="ifuHasFile" runat="server">
                                        <asp:Label ID="lblIFU" runat="server" Style="font-size: 20px; font: bold;" Text="Testing" />
                                        <span onclick="clearfile(2)" style="color: #333333" class="close">&times;</span>
                                    </div>
                                </div>

                                <div class="row" style="padding-right: 15px; padding-left: 15px;">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtImageComment" placeholder="Comment" runat="server" Style="max-height: 200px; max-width: 100%;" Height="200px" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <asp:CheckBox runat="server" Text="Active" ID="chkImageActive" />
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnUploadImage" OnClick="uploadImage" class="btn btn-lg btn-primary btn-block btn-signin" runat="server" Text="Upload Image" type="submit" ValidationGroup="ImageGroup" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnUploadImage" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>


    </asp:Panel>

    <!--Panel Add Tool -->
    <asp:Panel ID="addTool" runat="server" DefaultButton="btnUploadTool" Style="display: none;">
        <div class="modal fade reportModal in" data-backdrop="static" tabindex="-1" aria-hidden="false" style="display: block">
            <div class="tool-card tool-container">
                <div class="form-signin">
                    <div class="modalHeader">
                        <asp:Label ID="Label4" runat="server" Style="font-size: 32px; font: bold;" Text="Upload Tool" />
                        <span id="CancelClickedTool" class="close">&times;</span>
                        <br />
                    </div>
                    <div>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="row" style="padding-right: 15px; padding-left: 15px;">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtToolName" Style="margin-bottom: 0px;" placeholder="Tool Name" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ForeColor="Red"
                                            ControlToValidate="txtToolName" InitialValue=""
                                            ValidationGroup="ToolGroup"
                                            ErrorMessage="Enter a name."
                                            runat="Server">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>


                                <div class="row" style="padding-right: 15px; padding-left: 15px;">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtVersion" Style="margin-bottom: 0px;" placeholder="Version" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ForeColor="Red"
                                            ControlToValidate="txtVersion" InitialValue=""
                                            ValidationGroup="ToolGroup"
                                            ErrorMessage="Enter a version number."
                                            runat="Server">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row" style="padding-right: 15px; padding-left: 15px;">


                                    <div class="form-group" id="tfuDiv" runat="server">
                                        <label class="btn btn-block form-control" style="cursor: pointer;">
                                        Browse...
                                        <input runat="server" style="display: none" onchange="setfile(3)" id="tfu" type="file" name="file" />
                                            </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" ForeColor="Red"
                                            InitialValue=""
                                            ControlToValidate="fileSelected"
                                            ValidationGroup="ToolGroup"
                                            ErrorMessage="Select a File."
                                            runat="Server">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group" style="display: none" id="tfuHasFile" runat="server">
                                        <asp:Label ID="lblTFU" runat="server" Style="font-size: 20px; font: bold;" Text="Testing" />
                                        <span onclick="clearfile(3)" style="color: #333333" class="close">&times;</span>
                                    </div>
                                </div>

                                <div class="row" style="padding-right: 15px; padding-left: 15px;">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtReleaseNotes" placeholder="Release Notes" runat="server" CssClass="form-control" Style="max-height: 200px; max-width: 100%;" Height="200px" TextMode="MultiLine"></asp:TextBox>

                                    </div>
                                </div>


                                <div class="form-group">
                                    <asp:CheckBox runat="server" Text="Active" ID="chkToolActive" />
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnUploadTool" ValidationGroup="ToolGroup" OnClick="btnUploadTool_Click" class="btn btn-lg btn-primary btn-block btn-signin" runat="server" Text="Add New Tool" type="submit" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnUploadTool" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>


    <%--

    /#feature
    <section id="middle">
        <div class="clients-area center wow fadeInDown">
            <div class="center wow fadeInDown animated">
            </div>
            <div class="center wow fadeInDown animated">
            </div>

        </div>
    </section> --%>


    <asp:Button ID="hiddleFilebutton" runat="server" Style="display: none" />
    <input type="hidden" id="lblFileDateSelected" value="" runat="server" />
    <input type="hidden" id="lblDateSelected" value="" runat="server" />

    <asp:UpdatePanel Style="display: none" runat="server">
        <ContentTemplate>
            <asp:TextBox Style="display: none" ID="fileSelected" Text="" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function clearfile(tagID) {
            document.getElementById('<%=fileSelected.ClientID %>').value = '';
            switch (tagID) {
                case 0:
                    document.getElementById('<%=rfuDiv.ClientID %>').innerHTML = document.getElementById('<%=rfuDiv.ClientID %>').innerHTML;
                    document.getElementById('<%=rfuHasFile.ClientID %>').style.display = 'none';
                    document.getElementById('<%=rfuDiv.ClientID %>').style.display = 'block';

                    break;
                case 1:
                    document.getElementById('<%=ffuDiv.ClientID %>').innerHTML = document.getElementById('<%=ffuDiv.ClientID %>').innerHTML;
                    document.getElementById('<%=ffuHasFile.ClientID %>').style.display = 'none';
                    document.getElementById('<%=ffuDiv.ClientID %>').style.display = 'block';

                    break;
                case 2:
                    document.getElementById('<%=ifuDiv.ClientID %>').innerHTML = document.getElementById('<%=ifuDiv.ClientID %>').innerHTML;
                    document.getElementById('<%=ifuHasFile.ClientID %>').style.display = 'none';
                    document.getElementById('<%=ifuDiv.ClientID %>').style.display = 'block';

                    break;
                case 3:
                    document.getElementById('<%=tfuDiv.ClientID %>').innerHTML = document.getElementById('<%=tfuDiv.ClientID %>').innerHTML;
                    document.getElementById('<%=tfuHasFile.ClientID %>').style.display = 'none';
                    document.getElementById('<%=tfuDiv.ClientID %>').style.display = 'block';

                    break;


            }

        }


        //Trigger now when you have selected any file 
        function setfile(tagID) {
            switch (tagID) {
                case 0:
                    document.getElementById('<%=lblRFU.ClientID %>').innerHTML = document.getElementById('<%=rfu.ClientID %>').files[0].name;
                    document.getElementById('<%=rfuHasFile.ClientID %>').style.display = 'block';
                document.getElementById('<%=rfuDiv.ClientID %>').style.display = 'none';
                document.getElementById('<%=fileSelected.ClientID %>').value = document.getElementById('<%=rfu.ClientID %>').files[0].name;
                break;
                case 1:
                    document.getElementById('<%=lblFFU.ClientID %>').innerHTML = document.getElementById('<%=ffu.ClientID %>').files[0].name;
                    document.getElementById('<%=ffuHasFile.ClientID %>').style.display = 'block';
                document.getElementById('<%=ffuDiv.ClientID %>').style.display = 'none';
                document.getElementById('<%=fileSelected.ClientID %>').value = document.getElementById('<%=ffu.ClientID %>').files[0].name;
                break;
            case 2:
                document.getElementById('<%=lblIFU.ClientID %>').innerHTML = document.getElementById('<%=ifu.ClientID %>').files[0].name;
                    document.getElementById('<%=ifuHasFile.ClientID %>').style.display = 'block';
                document.getElementById('<%=ifuDiv.ClientID %>').style.display = 'none';
                document.getElementById('<%=fileSelected.ClientID %>').value = document.getElementById('<%=ifu.ClientID %>').files[0].name;
                break;
            case 3:
                document.getElementById('<%=lblTFU.ClientID %>').innerHTML = document.getElementById('<%=tfu.ClientID %>').files[0].name;
                    document.getElementById('<%=tfuHasFile.ClientID %>').style.display = 'block';
                document.getElementById('<%=tfuDiv.ClientID %>').style.display = 'none';
                document.getElementById('<%=fileSelected.ClientID %>').value = document.getElementById('<%=tfu.ClientID %>').files[0].name;
                break;


            }

        }




        $(document).ready(function () {
            // bind your jQuery events here initially
            bindEvents();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (e, a) {
            bindEvents();
            document.getElementById('<%=fileSelected.ClientID %>').innerHTML = '';
        });

        function bindEvents() {
            $('#datetimepicker1').datetimepicker({
                format: 'MM/DD/YYYY',
                useCurrent: false,
                maxDate: new Date
            });
            $("#datetimepicker1").on("dp.change",
                function (e) {
                    var x = e.date._d.toDateString();
                    var y = document.getElementById('<%= lblDateSelected.ClientID %>');

                    y.value = x;

                });
            $('#datetimepicker2').datetimepicker({
                format: 'MM/DD/YYYY',
                useCurrent: false,
                maxDate: new Date

            });
            $("#datetimepicker2").on("dp.change",
                function (e) {

                    var x = e.date._d.toDateString();
                    var y = document.getElementById('<%=lblDateSelected.ClientID %>');

                    y.value = x;

                });
            $('#datetimepicker3').datetimepicker({
                format: 'MM/DD/YYYY',
                useCurrent: false,
                maxDate: new Date
            });
            $("#datetimepicker3").on("dp.change",
                function (e) {

                    var x = e.date._d.toDateString();
                    var y = document.getElementById('<%=lblDateSelected.ClientID %>');

                    y.value = x;

                });
            $('#datetimepicker4').datetimepicker({
                format: 'MM/DD/YYYY',
                useCurrent: false,
                maxDate: new Date
            });
            $("#datetimepicker4").on("dp.change",
                function (e) {
                    __doPostBack('<%= txtReportFilterDate.ClientID %>', '');

                });
            $('#datetimepicker5').datetimepicker({
                format: 'MM/DD/YYYY',
                useCurrent: false,
                maxDate: new Date
            });
            $("#datetimepicker5").on("dp.change",
                function (e) {
                    __doPostBack('<%= txtFileDateFilter.ClientID %>', '');

                });
            $('#datetimepicker6').datetimepicker({
                format: 'MM/DD/YYYY',
                useCurrent: false,
                maxDate: new Date
            });
            $("#datetimepicker6").on("dp.change",
                function (e) {
                    __doPostBack('<%= txtImageDateFilter.ClientID %>', '');

                });
        }

    </script>
    <script type="text/javascript">


        $('#gaTabs a').click(function (e) {
            $(this).tab('show');
        });

        // store the currently selected tab in the hash value
        $("ul.nav-tabs > li > a").on("shown.bs.tab", function (e) {
            localStorage.setItem('activeTab', $(e.target).attr('href'));
            var id = $(e.target).attr("href").substr(1);
            window.location.hash = id;
            document.getElementById('<%=lblMainTitle.ClientID%>').innerHTML = id;

        });

        var activeTab = localStorage.getItem('activeTab');
        if (activeTab) {
            $('#gaTabs a[href="' + activeTab + '"]').tab('show');
        }

        // on load of the page: switch to the currently selected tab

    </script>
    <%--
    <script type="text/javascript">
     
        function changeTextReport() {
           document.getElementById('<%=lblMainTitle.ClientID%>').innerHTML = "Reports";
            document.getElementById('<%=pnlReports.ClientID%>').hidden = false;
            document.getElementById('<%=pnlFiles.ClientID%>').hidden = true;
          document.getElementById('<%=pnlImages.ClientID%>').hidden = true;
           document.getElementById('<%=pnlTools.ClientID%>').hidden = true;
        }
        function changeTextFiles() {
            document.getElementById('<%=lblMainTitle.ClientID%>').innerHTML = "Files";
            document.getElementById('<%=pnlReports.ClientID%>').hidden = true;
            document.getElementById('<%=pnlFiles.ClientID%>').hidden = false;
            document.getElementById('<%=pnlImages.ClientID%>').hidden = true;
            document.getElementById('<%=pnlTools.ClientID%>').hidden = true;
        }

        function changeTextImages() {
            document.getElementById('<%=lblMainTitle.ClientID%>').innerHTML = "Images";
            document.getElementById('<%=pnlReports.ClientID%>').hidden = true;
            document.getElementById('<%=pnlFiles.ClientID%>').hidden = true;
            document.getElementById('<%=pnlImages.ClientID%>').hidden = false;
            document.getElementById('<%=pnlTools.ClientID%>').hidden = true;
        }

        function changeTextTools() {
            document.getElementById('<%=lblMainTitle.ClientID%>').innerHTML = "Tools";
            document.getElementById('<%=pnlReports.ClientID%>').hidden = true;
            document.getElementById('<%=pnlFiles.ClientID%>').hidden = true;
            document.getElementById('<%=pnlImages.ClientID%>').hidden = true;
            document.getElementById('<%=pnlTools.ClientID%>').hidden = false;
        }
        

    </script>

    --%>
</asp:Content>
