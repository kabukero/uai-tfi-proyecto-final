<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.Login" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="row">
        <div class="col-md-12">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h4>
                        <asp:Literal
                            ID="EncabezadoPrincipal"
                            runat="server"
                            Text="<%$ Resources:Labels, Login.Encabezado %>" />
                    </h4>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="TxtEmail" CssClass="col-md-2 control-label" Text="<%$ Resources:Labels, Login.TextoEmail %>"></asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="TxtEmail" CssClass="form-control" TextMode="Email" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtEmail" CssClass="text-danger" 
                                ErrorMessage="<%$ Resources:Labels, Login.ErrorMensajeValidacionEmail %>" ValidationGroup="Login" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="TxtPassword" CssClass="col-md-2 control-label" Text="<%$ Resources:Labels, Login.TextoPassword %>"></asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="TxtPassword" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtPassword" CssClass="text-danger" 
                                ErrorMessage="<%$ Resources:Labels, Login.ErrorMensajeValidacionPassword %>" ValidationGroup="Login" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="<%$ Resources:Labels, Login.BotonLoginTexto %>" CssClass="btn btn-primary btn-md" ValidationGroup="Login" />
                        </div>
                    </div>
<%--                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <br />
                            <asp:HyperLink
                                ID="HyperLink1"
                                runat="server"
                                NavigateUrl="~/Registro.aspx"
                                Text="<%$ Resources:Labels, Login.TextoRegistrase %>" />
                        </div>
                    </div>--%>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <br />
                            <asp:Panel ID="pnlLoginError" runat="server"
                                CssClass="alert alert-danger alert-dismissible fade show"
                                Visible="false">
                                <strong>Error:</strong>
                                <asp:Label ID="lblLoginError" runat="server"></asp:Label>
                                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
