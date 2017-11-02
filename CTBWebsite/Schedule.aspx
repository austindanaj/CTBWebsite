<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Schedule.aspx.cs" Inherits="CTBWebsite.Schedule" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section id="middle">
        <div class="clients-area wow fadeInDown">
            <div class="row">
                <div style="display: inline-block; padding-bottom: 55px; padding-left: 15px">
                    <h1 style="font-weight: 700; font-size: 50px;">Intern schedule</h1>
                    <asp:DropDownList ID="ddlSelectScheduleDay" runat="server" CssClass="form-control" OnSelectedIndexChanged="changeScheduleDay" AutoPostBack="true">
                        <asp:ListItem Text="Monday" />
                        <asp:ListItem Text="Tuesday" />
                        <asp:ListItem Text="Wednesday" />
                        <asp:ListItem Text="Thursday" />
                        <asp:ListItem Text="Friday" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-10" style="padding-right: 6em">
                    <asp:GridView ID="dgvSchedule" ForeColor="WhiteSmoke" runat="server" CssClass="table table-bordered text-center" OnRowDataBound="color" />
                </div>
                <div class="col-md-2">
                    <div class="row">
                        <h1 style="float: left; color: whitesmoke">Add Schedule</h1>
                    </div>
                    <div class="row">
                        <h2 style="float: left; color: whitesmoke">Select a Day</h2>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlDay" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Monday" />
                                <asp:ListItem Text="Tuesday" />
                                <asp:ListItem Text="Wednesday" />
                                <asp:ListItem Text="Thursday" />
                                <asp:ListItem Text="Friday" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <h2 style="float: left; color: whitesmoke">Start Time</h2>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <div class='input-group date' id='datetimepicker1'>
                                <input type='text' id="txtStartTime" runat="server" class="form-control" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <h2 style="float: left; color: whitesmoke">End Time</h2>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <div class='input-group date' id='datetimepicker2'>
                                <input type='text' id="txtEndTime" runat="server" class="form-control" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <asp:Button ID="btnConfirmTime" runat="server" CssClass="btn btn-signin btn-lg" Text="Save" Width="100%" OnClick="saveOrDelete" />
                        </div>
                    </div>
                   
                        <div class="row">
                            <h1 style="float: left; color: whitesmoke">Delete Schedule</h1>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlScheduledHours" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Button ID="btnDeleteTime" runat="server" CssClass="btn btn-signin btn-lg" Width="100%" Text="Delete" OnClick="saveOrDelete" />
                            </div>
                        
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
    </section>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker1').datetimepicker({
                format: 'LT'
            });
            $('#datetimepicker2').datetimepicker({
                format: 'LT'
            });
            $("#datetimepicker1").on("dp.change", function (e) {

            });
            $("#datetimepicker2").on("dp.change", function (e) {

            });
        });
    </script>

</asp:Content>
