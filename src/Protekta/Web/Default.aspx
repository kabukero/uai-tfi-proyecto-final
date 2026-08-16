<%@ Page Title="<%$ Resources:Labels, Default.TituloPagina %>" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1>
                <asp:Literal
                    ID="headingTitle"
                    runat="server"
                    Text="<%$ Resources:Labels, Default.EncabezadoPrincipal %>" />
            </h1>
            <p>
                <asp:Literal
                    ID="EmpresaDescripcion"
                    runat="server"
                    Text="<%$ Resources:Labels, Default.DescripcionEmpresa %>" />
            </p>
        </section>

        <div class="row">
            <section class="col-md-4" aria-labelledby="gettingStartedTitle">
                <h2>
                    <asp:Literal
                        ID="EncabezadoPrevencionTradicional"
                        runat="server"
                        Text="<%$ Resources:Labels, Default.EncabezadoPrevencionTradicional %>" />
                </h2>
                <p>
                    <asp:Literal
                        ID="DescriptionPrevencionTradicional"
                        runat="server"
                        Text="<%$ Resources:Labels, Default.DescripcionPrevencionTradicional %>" />
                </p>
                <p><asp:Button runat="server" Text="<%$ Resources:Labels, Default.BotonContratar %>" CssClass="btn btn-primary btn-md" /></p>
            </section>
            <section class="col-md-4" aria-labelledby="librariesTitle">
                <h2>
                    <asp:Literal
                        ID="EncabezadoGestionDigital"
                        runat="server"
                        Text="<%$ Resources:Labels, Default.EncabezadoGestionDigital %>" />
                </h2>
                <p>
                    <asp:Literal
                        ID="DescripcionGestionDigital"
                        runat="server"
                        Text="<%$ Resources:Labels, Default.DescripcionGestionDigital %>" />
                </p>
                <p><asp:Button runat="server" Text="<%$ Resources:Labels, Default.BotonContratar %>" CssClass="btn btn-primary btn-md" /></p>
            </section>
            <section class="col-md-4" aria-labelledby="hostingTitle">
                <h2>
                    <asp:Literal
                        ID="EncabezadoPrevencionIA"
                        runat="server"
                        Text="<%$ Resources:Labels, Default.EncabezadoPrevencionIA %>" />
                </h2>
                <p>
                    <asp:Literal
                        ID="DescripcionPrevencionIA"
                        runat="server"
                        Text="<%$ Resources:Labels, Default.DescripcionPrevencionIA %>" />
                </p>
                <p><asp:Button runat="server" Text="<%$ Resources:Labels, Default.BotonContratar %>" CssClass="btn btn-primary btn-md" /></p>
            </section>
        </div>
    </main>
</asp:Content>
