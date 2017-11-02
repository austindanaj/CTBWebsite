<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="CTBWebsite.Admin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section id="middle">
        <div class="clients-area center wow fadeInDown">
            <div class="row">
                <asp:TextBox ID="txtSuccessBox" runat="server" Text="Success." Visible="false" ReadOnly="true" CssClass="feedback-textbox" />
            </div>
            <div class="row">
                <br />
                <div class="col-md-3 center">
                    <div style="display: inline-block">
                        <h2 style="color: whitesmoke">Add User</h2>
                        <br />
                        <asp:TextBox Class="form-control" ID="txtAlna" Width="300px" runat="server" placeholder="Alna number"></asp:TextBox>
                        <br />
                        <asp:TextBox Class="form-control" ID="txtName" Width="300px" runat="server" placeholder="First and Last Name"></asp:TextBox>
                        <br />
                        <asp:CheckBox ID="chkPartTime" runat="server" Text=" Part Time and use vehicles" Style="color: white" />
                        <br />
                        <asp:CheckBox ID="chkUseVehicle" runat="server" Text=" Only use vehicles" Style="color: white" />
                        <br />
                        <asp:Button Class="btn btn-signin btn-lg" ForeColor="whitesmoke"  Width="300px" ID="btnName" runat="server" OnClick="User_Clicked" Text="Add User"></asp:Button>
                    </div>
                </div>
                <div class="col-md-3 center">
                    <div style="display: inline-block">
                        <h2 style="color: whitesmoke">Add Project</h2>
                        <br />
                        <asp:TextBox Class="form-control" ID="txtProject" Width="300px" runat="server" placeholder="Project Name"></asp:TextBox>
                         <br />                         
                        <asp:DropDownList ID="category" runat="server"   Width="300px" CssClass="form-control">
                            <asp:ListItem Text="A – Advanced Development Project" />
                            <asp:ListItem Text="B – Time Off" />
                            <asp:ListItem Text="C – Production Development" />
                            <asp:ListItem Text="D – Design in Market (Non-Auto)" />
                        </asp:DropDownList>
                        <br />
                        <asp:TextBox CssClass="form-control" Width="300px" ID="txtAbbreviation" runat="server" placeholder="Abbreviation" />
                        <br />
                        <asp:Button Class="btn btn-signin btn-lg" Width="300px" ForeColor="whitesmoke" ID="btnProject" runat="server" OnClick="Project_Clicked" Text="Add Project"></asp:Button>
                    </div>
                </div>
                <div class="col-md-3 center">
                    <div style="display: inline-block">
                           <h2 style="color: whitesmoke">Add Vehicle</h2>
                        <br />
                        <asp:TextBox Class="form-control" Width="300px" ID="txtCar" runat="server" placeholder="Vehicle Name"></asp:TextBox>
                        <br />
                        <asp:TextBox Class="form-control" Width="300px" ID="txtCarAbbreviation" runat="server" placeholder="Abbreviation"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button Class="btn btn-signin btn-lg"  Width="300px" ForeColor="whitesmoke" ID="btnCar" runat="server" OnClick="Car_Clicked" Text="Add Vehicle"></asp:Button>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-3 center">
                    <div style="display: inline-block">
                          <h2 style="color: whitesmoke">Remove User</h2>
                        <br />
                        <asp:TextBox Class="form-control" Width="300px" ID="txtRemoveUser" runat="server" placeholder="Alna_num" />
                        <br />
                        <br />
                        <asp:Button Class="btn btn-signin btn-lg"  Width="300px" ForeColor="whitesmoke" ID="btnRemoveUser" runat="server" OnClick="remove" Text="Remove User"></asp:Button>
                    </div>
                </div>
                <div class="col-md-3 center">
                    <div style="display: inline-block">
                          <h2 style="color: whitesmoke">Remove Project</h2>
                          <br />
                        <asp:TextBox Class="form-control" Width="300px" ID="txtRemoveProject" runat="server" placeholder="Project ID"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button Class="btn btn-signin btn-lg" Width="300px"  ForeColor="whitesmoke" ID="btnRemoveProject" runat="server" OnClick="remove" Text="Remove Project"></asp:Button>
                    </div>
                </div>
                <div class="col-md-3 center">
                    <div style="display: inline-block">
                          <h2 style="color: whitesmoke">Remove Vehicle</h2>
                        <br />
                        <asp:TextBox Class="form-control" Width="300px" ID="txtRemoveVehicle" runat="server" placeholder="Vehicle ID"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button Class="btn btn-signin btn-lg"  Width="300px" ForeColor="whitesmoke" ID="btnRemoveVehicle" runat="server" OnClick="remove" Text="Remove Vehicle"></asp:Button>
                    </div>
                </div>
                <div class="col-md-3 center">
                    <div style="display: inline-block">
                           <h2 style="color: whitesmoke">Remove Issues</h2>
                        <br />
                        <asp:TextBox Class="form-control" Width="300px" ID="txtRemoveIssue" runat="server" placeholder="Issue ID" />
                        <br />
                        <br />
                        <asp:Button Class="btn btn-signin btn-lg" Width="300px" ForeColor="whitesmoke" ID="btnRemoveIssue" runat="server" OnClick="remove" Text="Remove Issue"></asp:Button>
                    </div>
                </div>
            </div>
            <div class="row">
                <br />
                <div class="col-md-3 center">
                    <asp:GridView ID="dgvUsers" HorizontalAlign="Center" runat="server" Width="350"  ForeColor="WhiteSmoke"
                        CssClass="table table-bordered table-hover"
                        AutoGenerateColumns="true" />
                </div>
                <div class="col-md-3 center">
                    <asp:GridView ID="dgvProjects" HorizontalAlign="Center" runat="server" Width="300" ForeColor="WhiteSmoke"
                        CssClass="table table-bordered table-hover"
                        AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
                <div class="col-md-3 center">
                    <asp:GridView ID="dgvCars" HorizontalAlign="Center" runat="server" Width="200" ForeColor="WhiteSmoke"
                        CssClass="table table-bordered table-hover"
                        AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
                <div class="col-md-3 center">
                    <asp:GridView ID="dgvIssues" HorizontalAlign="Center" runat="server" Width="200"  ForeColor="WhiteSmoke"
                        CssClass="table table-bordered table-hover"
                        AutoGenerateColumns="true" />
                </div>
                <br />
            </div>
        </div>
    </section>
</asp:Content>
