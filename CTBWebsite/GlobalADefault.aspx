<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GlobalADefault.aspx.cs" Inherits="CTBWebsite.GlobalADefault" %>

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
        <ul class="nav nav-tabs" role="tablist">
            <li class="active"><a runat="server" id="tabReports" onclick="changeTextReport()" aria-controls="settings" role="tab" data-toggle="tab">Reports</a></li>
            <li><a runat="server" id="tabFiles" onclick="changeTextFiles()" aria-controls="info" role="tab" data-toggle="tab">Files</a></li>
            <li><a runat="server" id="tabImages" onclick="changeTextImages()" aria-controls="info" role="tab" data-toggle="tab">Images</a></li>
            <li><a runat="server" id="tabTools" onclick="changeTextTools()" aria-controls="info" role="tab" data-toggle="tab">Tools</a></li>
        </ul>
    </div>

    <aside class="callout">
        <div class="text-vertical-center">
            <h1 id="lblMainTitle" runat="server">Reports</h1>
        </div>
    </aside>
    <section id="services" class="services bg-primary">
        <div class="container">
            <div class="row">
                <div>
                    <asp:Panel ID="pnlReports" runat="server">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlVehicles" ValidationGroup="ReportGroup" CssClass="form-control" runat="server">
                                <asp:ListItem Text="-- Select a Vehicle --" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red"
                                ControlToValidate="ddlVehicles" InitialValue="-- Select a Vehicle --"
                                ValidationGroup="ReportGroup"
                                ErrorMessage="Select a vehicle."
                                runat="Server">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlPhones" CssClass="form-control" runat="server" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnCalibrationFile" runat="server" Text="Calibration File" CssClass="btn btn-lg btn-block" OnClick="Trigger_FileUpload" Text-Align="Center" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnTD1" runat="server" Text="TD1 File" CssClass="btn btn-lg btn-block" OnClick="Trigger_FileUpload" Text-Align="Center" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnTD2" runat="server" Text="TD2 File" CssClass="btn btn-lg btn-block" OnClick="Trigger_FileUpload" Text-Align="Center" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnTD3" runat="server" Text="TD3 File" CssClass="btn btn-lg btn-block" OnClick="Trigger_FileUpload" Text-Align="Center" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnTD4" runat="server" Text="TD4 File" CssClass="btn btn-lg btn-block" OnClick="Trigger_FileUpload" Text-Align="Center" />
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlAuthor1" CssClass="form-control" runat="server" />
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlAuthor2" CssClass="form-control" runat="server" />
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="TextBox1" placeholder="Comment" runat="server" CssClass="form-control" Font-Size="Medium" Height="206px" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <div class='input-group date' id='datetimepicker1'>
                                <input type='text' class="form-control" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlFiles" runat="server" DefaultButton="UploadClicked" hidden="true">

                        <asp:Label ID="lblTitle" runat="server" Style="font-size: 20px; font: bold;" Text="Upload File" />
                        <br />
                        <div>
                            <asp:DropDownList ID="ddlFileVehicle" runat="server" ValidationGroup="UploadGroup" CssClass="form-control">
                                <asp:ListItem Text="-- Select a Vehicle --" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red"
                                ControlToValidate="ddlFileVehicle" InitialValue="-- Select a Vehicle --"
                                ValidationGroup="UploadGroup"
                                ErrorMessage="Select a vehicle."
                                runat="Server">
                            </asp:RequiredFieldValidator>


                            <asp:DropDownList ID="ddlFilePhone" runat="server" ValidationGroup="UploadGroup" CssClass="form-control">
                                <asp:ListItem Text="-- Select a Phone --" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red"
                                ControlToValidate="ddlFilePhone" InitialValue="-- Select a Phone --"
                                ValidationGroup="UploadGroup"
                                ErrorMessage="Select a Phone."
                                runat="Server">
                            </asp:RequiredFieldValidator>


                            <input type="text" enableviewstate="false" runat="server" id="inputUsername" class="form-control" validationgroup="UploadGroup" value="TD1" />

                            <div class="form-group">
                                <div class='input-group date' id='datetimepicker2'>
                                    <input type='text' class="form-control" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>


                            <asp:DropDownList ID="ddlFileAuthor1" runat="server" ValidationGroup="UploadGroup" CssClass="form-control">
                                <asp:ListItem Text="-- Select Author 1 --" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red"
                                ControlToValidate="ddlFileAuthor1" InitialValue="-- Select Author 1 --"
                                ValidationGroup="UploadGroup"
                                ErrorMessage="Select an Author."
                                runat="Server">
                            </asp:RequiredFieldValidator>

                            <asp:DropDownList ID="ddlFileAuthor2" runat="server" ValidationGroup="UploadGroup" CssClass="form-control">
                                <asp:ListItem Text="-- Select Author 2 --" />
                            </asp:DropDownList>

                            <div class="form-group">
                                <asp:TextBox ID="txtFileComment" placeholder="Comment" runat="server" CssClass="form-control" Height="206px" TextMode="MultiLine"></asp:TextBox>
                            </div>

                            <asp:Button ID="UploadClicked" class="btn btn-lg btn-primary btn-block btn-signin" runat="server" Text="Sign In" type="submit" ValidationGroup="UploadGroup" />
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlImages" runat="server" hidden="true">
                    </asp:Panel>

                    <asp:Panel ID="pnlTools" runat="server" hidden="true">
                        <asp:ListView ID="lstTools" runat="server" GroupItemCount="3" OnItemCommand="lstTools_OnItemCommand">
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
                                    <div>
                                        <img src='<%# "/images/icons/" + Eval("IconType") %>'
                                            height="120" width="120" />
                                    </div>
                                    <br />
                                    <div>
                                        <b>
                                            <asp:LinkButton ID="lblToolClick" CssClass="itemTitle" CommandName="Download_Tool" CommandArgument='<%#Eval("Tool_ID") %>' runat="server" Text='<%#Eval("Tool_Name")%>' />
                                        </b>
                                    </div>
                                    <div>
                                        <div>
                                            <span class="itemText">
                                                <b>Updated: </b><%#:Eval("FormDate")%>
                                            </span>
                                            <br />
                                            <span class="itemText">
                                                <%#:Eval("Tool_Version")%>
                                            </span>
                                            <pre style="height: 100px; overflow: auto; text-align: left; border: transparent; white-space: pre-line; background: gainsboro">
                                                <asp:Label runat="server" Text='Release Notes:'></asp:Label>
                                                <asp:Label runat="server" Text='<%#:Eval("Tool_Description")%>'></asp:Label>
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

                        <div class="form-group">
                            <asp:TextBox ID="txtFileName" placeholder="Tool Name" runat="server" CssClass="form-control" Font-Size="Medium"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtFileDescription" placeholder="Tool Description" runat="server" CssClass="form-control" Font-Size="Medium" Height="206px" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtVersion" placeholder="Tool Version" runat="server" CssClass="form-control" Font-Size="Medium"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:FileUpload ID="fileUpload" runat="server" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnUploadTool" OnClick="btnUploadTool_Click" class="btn btn-lg btn-primary btn-block btn-signin" runat="server" Text="Add New Tool" type="submit" />
                        </div>
                    </asp:Panel>
                </div>

            </div>
            <!-- /.row -->
        </div>
        <!-- /.container -->
    </section>

    <!--
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

    
    



    <asp:Button ID="hiddenButton" runat="server" Style="display: none" />

    <ajaxToolkit:ModalPopupExtender ID="mpeReports" runat="server" TargetControlID="hiddenButton" PopupControlID="addReport" BackgroundCssClass="modalBackground" CancelControlID="CancelClicked" />
    <ajaxToolkit:ModalPopupExtender ID="mpeFiles" runat="server" TargetControlID="hiddenButton" PopupControlID="addFile" BackgroundCssClass="modalBackground" CancelControlID="CancelClicked" />
    <ajaxToolkit:ModalPopupExtender ID="mpeImages" runat="server" TargetControlID="hiddenButton" PopupControlID="addImage" BackgroundCssClass="modalBackground" CancelControlID="CancelClicked" />
    <ajaxToolkit:ModalPopupExtender ID="mpeTools" runat="server" TargetControlID="hiddenButton" PopupControlID="addTool" BackgroundCssClass="modalBackground" CancelControlID="CancelClicked" />
    

        <asp:Panel ID="addReport" runat="server" DefaultButton="OkClicked" Style="display:none;">
            <div class="modal fade loginSocialModal in" data-backdrop="static" id="loginModal" tabindex="-1" aria-hidden="false" style="display: block">
                <div class="card card-container">
                    <div class="form-signin">
                        <div class="modalHeader">
                            <asp:Label ID="Label1" runat="server" Style="font-size: 20px; font: bold;" Text="Sign In" />
                            <span id="CancelClicked" class="close">&times;</span>
                            <br />
                        </div>
                        <div>
                            <asp:DropDownList ID="ddl" runat="server" ValidationGroup="LoginGroup" CssClass="form-control">
                                <asp:ListItem Text="-- Select yourself --" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red"
                                ControlToValidate="ddl" InitialValue="-- Select yourself --"
                                ValidationGroup="LoginGroup"
                                ErrorMessage="Select a name."
                                runat="Server">
                            </asp:RequiredFieldValidator>

                            <input type="text" runat="server" id="Text1" class="form-control" validationgroup="LoginGroup" placeholder="Username" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                ForeColor="Red"
                                ControlToValidate="inputUsername"
                                ValidationGroup="LoginGroup"
                                ErrorMessage="Enter a username."
                                runat="Server">
                            </asp:RequiredFieldValidator>
                            <input type="password" runat="server" id="inputPassword" class="form-control" validationgroup="LoginGroup" placeholder="Password" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                ForeColor="Red"
                                ControlToValidate="inputPassword"
                                ValidationGroup="LoginGroup"
                                ErrorMessage="Enter a password."
                                runat="Server">
                            </asp:RequiredFieldValidator>

                            <asp:Button ID="OkClicked" class="btn btn-lg btn-primary btn-block btn-signin" OnClick="Login_Clicked" runat="server" Text="Sign In" type="submit" ValidationGroup="LoginGroup" />
                        </div>
                    </div>
                </div>
            </div>


        </asp:Panel>













    <!--/#feature
    <section id="middle">
        <div class="clients-area center wow fadeInDown">
            <div class="center wow fadeInDown animated">
            </div>
            <div class="center wow fadeInDown animated">
            </div>

        </div>
    </section> -->


    <asp:Button ID="hiddleFilebutton" runat="server" Style="display: none" />
    <input type="hidden" id="lblFileDateSelected" value="" runat="server" />
    <input type="hidden" id="lblDateSelected" value="" runat="server" />



    <script type="text/javascript">
        $(function () {
            $('#datetimepicker1').datetimepicker();
            $("#datetimepicker1").on("dp.change", function (e) {
                var x = e.date._d.toDateString();
                var y = document.getElementById('<%= lblDateSelected.ClientID %>');

                y.value = x;

            });
            $('#datetimepicker2').datetimepicker();
            $("#datetimepicker2").on("dp.change", function (e) {

                var x = e.date._d.toDateString();
                var y = document.getElementById('<%=lblFileDateSelected.ClientID %>');

                y.value = x;

            });
        });
    </script>
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


</asp:Content>
