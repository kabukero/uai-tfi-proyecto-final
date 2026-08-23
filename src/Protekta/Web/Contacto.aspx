<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="Web.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <img src="<%= ResolveUrl("~/Content/Images/protekta-logo.jpeg") %>" alt="Logo Protekta" style="max-height:200px;max-width:200px;" />
        <h3>
            <asp:Literal
                ID="EncabezadoPrincipal"
                runat="server"
                Text="<%$ Resources:Labels, Contacto.Encabezado %>" />
        </h3>
        <address>
            <asp:Literal
                ID="Literal1"
                runat="server"
                Text="<%$ Resources:Labels, Contacto.DireccionTitulo %>" /><br />
            <asp:Literal
                ID="Literal2"
                runat="server"
                Text="<%$ Resources:Labels, Contacto.Direccion %>" /><br />
            <abbr title="Phone">P:</abbr>
            <asp:Literal
                ID="Literal3"
                runat="server"
                Text="<%$ Resources:Labels, Contacto.Telefono %>" />
        </address>

        <address>
            <strong>
                <asp:Literal
                ID="Literal4"
                runat="server"
                Text="<%$ Resources:Labels, Contacto.TextoSoporte %>" /></strong>
                <asp:HyperLink
                    ID="LinkMailSoporte"
                    runat="server"
                    NavigateUrl="mailto:<%$ Resources:Labels, Contacto.SoporteEmail %>"
                    Text="<%$ Resources:Labels, Contacto.SoporteEmail %>" />
                <br />
            <strong>
                <asp:Literal
                    ID="Literal5"
                    runat="server"
                    Text="<%$ Resources:Labels, Contacto.TextoMarketing %>" />
            </strong>
            <asp:HyperLink
                ID="HyperLink1"
                runat="server"
                NavigateUrl="mailto:<%$ Resources:Labels, Contacto.MarketingMail %>"
                Text="<%$ Resources:Labels, Contacto.MarketingMail %>" />
        </address>
    </main>
</asp:Content>
