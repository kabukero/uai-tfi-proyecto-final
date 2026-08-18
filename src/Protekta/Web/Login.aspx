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
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label" Text="<%$ Resources:Labels, Login.TextoEmail %>"></asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email" CssClass="text-danger" 
                                ErrorMessage="<%$ Resources:Labels, Login.ErrorMensajeValidacionEmail %>" ValidationGroup="Login" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label" Text="<%$ Resources:Labels, Login.TextoPassword %>"></asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" 
                                ErrorMessage="<%$ Resources:Labels, Login.ErrorMensajeValidacionPassword %>" ValidationGroup="Login" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="<%$ Resources:Labels, Login.BotonLoginTexto %>" CssClass="btn btn-primary btn-md" ValidationGroup="Login" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <br />
                            <asp:HyperLink
                                ID="HyperLink1"
                                runat="server"
                                NavigateUrl="~/Registro.aspx"
                                Text="<%$ Resources:Labels, Login.TextoRegistrase %>" />
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
