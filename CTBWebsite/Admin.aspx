<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="CTBWebsite.Admin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section id="middle">
        <div class="clients-area center wow fadeInDown">
            <div class="row">
                <asp:TextBox ID="txtSuccessBox" runat="server" Text="Success." Visible="false" ReadOnly="true" CssClass="feedback-textbox" />
            </div>
            <div class="row">
                <br />
                <div class="col-md-4 center">
                    <div style="display: inline-block">
                        <h2 style="color: whitesmoke">Add User</h2>
                        <br />
                        <asp:TextBox Class="form-control" ID="txtAlna" Width="300px" runat="server" placeholder="Alna number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red"
                                                    ControlToValidate="txtAlna" InitialValue=""
                                                    ValidationGroup="AddEmployeeGroup"
                                                    ErrorMessage="Enter an alna number."
                                                    runat="Server">
                        </asp:RequiredFieldValidator>
                        <br />
                        <asp:TextBox Class="form-control" ID="txtName" Width="300px" runat="server" placeholder="First and Last Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red"
                                                    ControlToValidate="txtName" InitialValue=""
                                                    ValidationGroup="AddEmployeeGroup"
                                                    ErrorMessage="Enter employee name."
                                                    runat="Server">
                        </asp:RequiredFieldValidator>
                        <br />
                        <asp:CheckBox ID="chkPartTime" runat="server" Text=" Part Time and use vehicles" Style="color: white" />
                        <br />
                        <asp:CheckBox ID="chkUseVehicle" runat="server" Text=" Only use vehicles" Style="color: white" />
                        <br />
                        <asp:Button Class="btn btn-signin btn-lg" ForeColor="whitesmoke"  Width="300px" ID="btnName" runat="server" OnClick="User_Clicked" ValidationGroup="AddEmployeeGroup" Text="Add User"></asp:Button>
                    </div>
                </div>
                <div class="col-md-4 center">
                    <div style="display: inline-block">
                        <h2 style="color: whitesmoke">Add Project</h2>
                        <br />
                        <asp:TextBox Class="form-control" ID="txtProject" Width="300px" runat="server" placeholder="Project Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red"
                                                    ControlToValidate="txtProject" InitialValue=""
                                                    ValidationGroup="AddProjectGroup"
                                                    ErrorMessage="Enter project name."
                                                    runat="Server">
                        </asp:RequiredFieldValidator>
                         <br />                         
                        <asp:DropDownList ID="category" runat="server"   Width="300px" CssClass="form-control">
                            <asp:ListItem Text="-- Select a Category --" Value="-1" />
                            <asp:ListItem Text="A – Advanced Development Project" Value="0"  />
                            <asp:ListItem Text="B – Time Off" Value="1"  />
                            <asp:ListItem Text="C – Production Development" Value="2"  />
                            <asp:ListItem Text="D – Design in Market (Non-Auto)" Value="3"  />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red"
                                                    ControlToValidate="category" InitialValue="-1"
                                                    ValidationGroup="AddProjectGroup"
                                                    ErrorMessage="Select a category."
                                                    runat="Server">
                        </asp:RequiredFieldValidator>
                        <br />
                        <asp:TextBox CssClass="form-control" Width="300px" ID="txtAbbreviation" runat="server" placeholder="Abbreviation" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red"
                                                    ControlToValidate="txtAbbreviation" InitialValue=""
                                                    ValidationGroup="AddProjectGroup"
                                                    ErrorMessage="Enter an abbreviation."
                                                    runat="Server">
                        </asp:RequiredFieldValidator>
                        <br />
                        <asp:Button Class="btn btn-signin btn-lg" Width="300px" ForeColor="whitesmoke" ID="btnProject" runat="server" OnClick="Project_Clicked" ValidationGroup="AddProjectGroup" Text="Add Project"></asp:Button>
                    </div>
                </div>
                <div class="col-md-4 center">
                    <div style="display: inline-block">
                           <h2 style="color: whitesmoke">Add Vehicle</h2>
                        <br />
                        <asp:TextBox Class="form-control" Width="300px" ID="txtCar" runat="server" placeholder="Vehicle Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red"
                                                    ControlToValidate="txtCar" InitialValue=""
                                                    ValidationGroup="AddVehicleGroup"
                                                    ErrorMessage="Enter an vehicle name."
                                                    runat="Server">
                        </asp:RequiredFieldValidator>
                        <br />
                        <asp:TextBox Class="form-control" Width="300px" ID="txtCarAbbreviation" runat="server" placeholder="Abbreviation"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red"
                                                    ControlToValidate="txtCarAbbreviation" InitialValue=""
                                                    ValidationGroup="AddVehicleGroup"
                                                    ErrorMessage="Enter an abbreviation."
                                                    runat="Server">
                        </asp:RequiredFieldValidator>
                        <br />
                        <br />
                        <asp:Button Class="btn btn-signin btn-lg"  Width="300px" ForeColor="whitesmoke" ID="btnCar" runat="server" OnClick="Car_Clicked" ValidationGroup="AddVehicleGroup" Text="Add Vehicle"></asp:Button>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-4 center">
                    <div style="display: inline-block">
                          <h2 style="color: whitesmoke">Remove User</h2>
                        <br />
                        <asp:TextBox Class="form-control" Width="300px" ID="txtRemoveUser" runat="server" placeholder="Alna_num" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ForeColor="Red"
                                                    ControlToValidate="txtRemoveUser" InitialValue=""
                                                    ValidationGroup="RemoveEmployeeGroup"
                                                    ErrorMessage="Enter an alna number."
                                                    runat="Server">
                        </asp:RequiredFieldValidator>
                        <br />
                        <br />
                        <asp:Button Class="btn btn-signin btn-lg"  Width="300px" ForeColor="whitesmoke" ID="btnRemoveUser" runat="server" OnClick="remove" ValidationGroup="RemoveEmployeeGroup" Text="Remove User"></asp:Button>
                    </div>
                </div>
                <div class="col-md-4 center">
                    <div style="display: inline-block">
                          <h2 style="color: whitesmoke">Remove Project</h2>
                          <br />
                        <asp:TextBox Class="form-control" Width="300px" ID="txtRemoveProject" runat="server" placeholder="Project ID"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ForeColor="Red"
                                                    ControlToValidate="txtRemoveProject" InitialValue=""
                                                    ValidationGroup="RemoveProjectGroup"
                                                    ErrorMessage="Enter a project id."
                                                    runat="Server">
                        </asp:RequiredFieldValidator>
                        <br />
                        <br />
                        <asp:Button Class="btn btn-signin btn-lg" Width="300px"  ForeColor="whitesmoke" ID="btnRemoveProject" runat="server" OnClick="remove" ValidationGroup="RemoveProjectGroup" Text="Remove Project"></asp:Button>
                    </div>
                </div>
                <div class="col-md-4 center">
                    <div style="display: inline-block">
                          <h2 style="color: whitesmoke">Remove Vehicle</h2>
                        <br />
                        <asp:TextBox Class="form-control" Width="300px" ID="txtRemoveVehicle" runat="server" placeholder="Vehicle ID"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ForeColor="Red"
                                                    ControlToValidate="txtRemoveVehicle" InitialValue=""
                                                    ValidationGroup="RemoveVehicleGroup"
                                                    ErrorMessage="Enter a vehicle id."
                                                    runat="Server">
                        </asp:RequiredFieldValidator>
                        <br />
                        <br />
                        <asp:Button Class="btn btn-signin btn-lg"  Width="300px" ForeColor="whitesmoke" ID="btnRemoveVehicle" runat="server" ValidationGroup="RemoveVehicleGroup" OnClick="remove" Text="Remove Vehicle"></asp:Button>
                    </div>
                </div>
            
            </div>
            <div class="row">
                <br />
                <div class="col-md-4 center">
                    <asp:GridView ID="dgvUsers" style="overflow: auto;" HorizontalAlign="Center" runat="server" Width="350"  ForeColor="WhiteSmoke"
                        CssClass="table table-bordered table-hover"
                        AutoGenerateColumns="true" />
                </div>
                <div class="col-md-4 center">
                    <asp:GridView ID="dgvProjects" style="overflow: auto;" HorizontalAlign="Center" runat="server" Width="300" ForeColor="WhiteSmoke"
                        CssClass="table table-bordered table-hover"
                        AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
                <div class="col-md-4 center">
                    <asp:GridView ID="dgvCars" style="overflow: auto;" HorizontalAlign="Center" runat="server" Width="200" ForeColor="WhiteSmoke"
                        CssClass="table table-bordered table-hover"
                        AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
                
                
            </div>
        </div>
    </section>
</asp:Content>
