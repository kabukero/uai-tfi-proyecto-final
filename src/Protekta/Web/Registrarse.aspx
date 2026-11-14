<%@ Page Title="Registrarse" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="Web.Registrarse" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="row">
        <div class="col-md-12">
            <section id="loginForm">
                <div class="form-horizontal">
                    
                    <h4>
                        <asp:Literal
                            ID="EncabezadoPrincipal"
                            runat="server"
                            Text="<%$ Resources:Labels, Registrase.Encabezado %>" />
                    </h4>
                    <hr />

                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>

                    <div class="form-group mb-4">
                        <asp:Label runat="server" AssociatedControlID="TxtNombre" CssClass="col-md-2 control-label" Text="<%$ Resources:Labels, Registrarse.TextoNombre %>"></asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="TxtNombre" CssClass="form-control" MaxLength="50" />

                            <asp:RequiredFieldValidator
                                runat="server"
                                ControlToValidate="TxtNombre"
                                ErrorMessage="<%$ Resources:Labels, Registrarse.ErrorMensajeValidacionNombre %>"
                                Display="Dynamic"
                                CssClass="text-danger" />
                        </div>
                    </div>

                    <div class="form-group mb-4">
                        <asp:Label runat="server" AssociatedControlID="TxtApellido" CssClass="col-md-2 control-label" Text="<%$ Resources:Labels, Registrarse.TextoApellido %>"></asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="TxtApellido" CssClass="form-control" MaxLength="50" />

                            <asp:RequiredFieldValidator
                                runat="server"
                                ControlToValidate="TxtApellido"
                                ErrorMessage="<%$ Resources:Labels, Registrarse.ErrorMensajeValidacionApellido %>"
                                Display="Dynamic"
                                CssClass="text-danger" />
                        </div>
                    </div>

                    <div class="form-group mb-4">
                        <asp:Label runat="server" AssociatedControlID="TxtEmail" CssClass="col-md-2 control-label" Text="<%$ Resources:Labels, Login.TextoEmail %>"></asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="TxtEmail" CssClass="form-control" TextMode="Email" MaxLength="50" />
                            <asp:RequiredFieldValidator
                                runat="server"
                                ControlToValidate="TxtEmail"
                                CssClass="text-danger d-block"
                                ErrorMessage="<%$ Resources:Labels, Login.ErrorMensajeValidacionEmail %>"
                                ValidationGroup="Registrarse" />
                            <asp:RegularExpressionValidator
                                runat="server"
                                ControlToValidate="TxtEmail"
                                CssClass="text-danger d-block"
                                ValidationGroup="Registrarse"
                                ErrorMessage="<%$ Resources:Labels, Login.ErrorMensajeEmailFormatoIncorrecto %>"
                                ValidationExpression="^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$" />
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="TxtPassword" CssClass="col-md-2 control-label" Text="<%$ Resources:Labels, Login.TextoPassword %>"></asp:Label>

                        <div class="col-md-10">

                            <asp:TextBox runat="server" ID="TxtPassword" TextMode="Password" CssClass="form-control" MaxLength="50" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtPassword" CssClass="text-danger" 
                                ErrorMessage="<%$ Resources:Labels, Login.ErrorMensajeValidacionPassword %>" ValidationGroup="Login" />

                            <asp:RegularExpressionValidator runat="server"
                                ControlToValidate="TxtPassword"
                                CssClass="text-danger"
                                ValidationExpression="^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9\s]).{8,}$"
                                ErrorMessage="<%$ Resources:Labels, Registrarse.TextoValidacionPasswordsNoCoinciden %>"
                                ValidationGroup="Registrarse"
                                Display="Dynamic" />

                        </div>
                    </div>

                    <div class="form-group mb-4">
                        <asp:Label runat="server"
                            AssociatedControlID="TxtConfirmPassword"
                            CssClass="col-md-2 control-label"
                            Text="<%$ Resources:Labels, Registrarse.TextConfirmacionPassword %>" />

                        <div class="col-md-10">
                            <asp:TextBox runat="server"
                                ID="TxtConfirmPassword"
                                TextMode="Password"
                                CssClass="form-control" MaxLength="50" />

                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="TxtConfirmPassword"
                                CssClass="text-danger"
                                ErrorMessage="<%$ Resources:Labels, Registrarse.TextoValidacionConfirmacionPassword %>"
                                ValidationGroup="Registrarse"
                                Display="Dynamic" />

                            <asp:CompareValidator runat="server"
                                ControlToValidate="TxtConfirmPassword"
                                ControlToCompare="TxtPassword"
                                Operator="Equal"
                                Type="String"
                                CssClass="text-danger"
                                ErrorMessage="<%$ Resources:Labels, Registrarse.TextoValidacionPasswordsNoCoinciden %>"
                                ValidationGroup="Registrarse"
                                Display="Dynamic" />
                        </div>
                    </div>

                    <div class="form-group mb-4">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="Registrar" Text="<%$ Resources:Labels, Registrarse.TextoRegistrar %>" CssClass="btn btn-primary btn-md" ValidationGroup="Login" />
                        </div>
                    </div>

                    <div class="form-group mb-4">
                        <div class="col-md-offset-2 col-md-10">
                            <br />
                            <asp:Panel ID="pnlRegistrarseError" runat="server"
                                CssClass="alert alert-danger alert-dismissible fade show"
                                Visible="false">
                                <strong>Error:</strong>
                                <asp:Label ID="lblRegistrarseError" runat="server"></asp:Label>
                                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                            </asp:Panel>
                        </div>
                    </div>

                </div>
            </section>
        </div>
    </div>

</asp:Content>
