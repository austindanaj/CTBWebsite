<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="TimeOff.aspx.cs" Inherits="CTBWebsite.TimeOff" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="txtSuccessBox" runat="server" Text="Success." Visible="false" ReadOnly="true" CssClass="feedback-textbox" />
    <section id="middle">
        <div class="container">
            <div class="row">
                <div class="wow fadeInDown">
                    <div class="center skill">
                        <h1>Time Off</h1>
                        <div class="col-md-6 wow fadeInDown">
                            <div class="row">
                                <h2 style="font-size: large;">Current Time Off</h2>
                                <asp:GridView ID="gdvUser" runat="server" AllowPaging="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" CssClass="table table-bordered text-center" PageSize="7" Width="90%" ForeColor="WhiteSmoke" OnPageIndexChanging="gdvUser_PageIndexChanged">
                                </asp:GridView>
                            </div>
                            <div class="row">
                                <div class="center" style="display: inline-block">
                                    <h2 style="font-size: large;">Remove Time Off</h2>
                                    <asp:DropDownList ID="ddlTimeTakenOff" CssClass="form-control" Width="250px" runat="server" />
                                    <br />
                                    <asp:Button ID="btnRemoveTimeOff" runat="server" OnClick="removeTimeOff" Text="Remove Time Off" Width="250px" ForeColor="WhiteSmoke" CssClass="btn btn-signin btn-lg" />
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6 wow fadeInDown">
                            <div class="accordion">
                                <h2 style="font-size: large;">Add Time Off</h2>
                                <div class="panel-group" id="accordion1">
                                    <div class="panel panel-default">
                                        <div class="panel-heading active">
                                            <h3 class="panel-title">
                                                <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#collapseOne1">1: Select A Start Date
                                                    <i class="fa fa-angle-right pull-right"></i>
                                                </a>
                                            </h3>
                                        </div>
                                        <div id="collapseOne1" class="panel-collapse collapse in">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div id='datetimepicker6'>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">
                                                <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#collapseTwo1">2: Select An End Date
                                                <i class="fa fa-angle-right pull-right"></i>
                                                </a>
                                            </h3>
                                        </div>
                                        <div id="collapseTwo1" class="panel-collapse collapse">
                                            <div class="panel-body">

                                                <div class="form-group">
                                                    <div id='datetimepicker7'>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">
                                                <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#collapseThree1">3: Verify Correct Dates
                                                    <i class="fa fa-angle-right pull-right"></i>
                                                </a>
                                            </h3>
                                        </div>
                                        <div id="collapseThree1" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <asp:Label runat="server" Text="Start Date:" />
                                                    <asp:Label ID="lblStartDate" runat="server" ForeColor="DodgerBlue" Text="No Date Selected" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" Text="End Date:" />
                                                    <asp:Label ID="lblEndDate" runat="server" ForeColor="DodgerBlue" Text="No Date Selected" />
                                                </div>
                                                <div class="form-group">
                                                    <div class="checkbox">
                                                        <asp:CheckBox ID="chkBusinessTrip" runat="server" Text=" This is a business trip" />
                                                    </div>
                                                </div>
                                                <div class="form-inline">
                                                    <asp:Button ID="btnAddTimeOff" runat="server" OnClick="addTimeOff" ForeColor="White" Width="200px" Text="Add Time Off" CssClass="btn btn-signin btn-lg" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--/#accordion1-->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--/.row-->
        </div>
        <!--/.container-->
    </section>
    <input type="hidden" id="startTime" value="" runat="server" />
       <input type="hidden" id="endTime" value=""  runat="server" />
    <section id="portfolio">
        <div class="container">
            <div class="center">
                <h2>Current Week</h2>
            </div>
            <div class="row">
                <asp:GridView ID="gv" runat="server" CssClass="table table-striped table-bordered table-hover" />
            </div>
        </div>
    </section>

    <script type="text/javascript">
        $(function () {
            $('#datetimepicker6').datetimepicker({
                inline: true,
                viewMode: 'days',
                format: 'MM/DD/YYYY'

            });
            $('#datetimepicker7').datetimepicker({
                inline: true,
                viewMode: 'days',
                format: 'MM/DD/YYYY',
                useCurrent: false //Important! See issue #1075
            });
            $("#datetimepicker6").on("dp.change", function (e) {
                $('#datetimepicker7').data("DateTimePicker").minDate(e.date);
                var x = e.date._d.toDateString();
                var y = document.getElementById('<%= startTime.ClientID %>');
                document.getElementById('<%=lblStartDate.ClientID %>').innerText =x;
                y.value = x;

            });
            $("#datetimepicker7").on("dp.change", function (e) {
                $('#datetimepicker6').data("DateTimePicker").maxDate(e.date);
                var x = e.date._d.toDateString();
                var y = document.getElementById('<%= endTime.ClientID %>');
                document.getElementById('<%=lblEndDate.ClientID %>').innerText =x;
                y.value = x;
            });
        });
    </script>
</asp:Content>
