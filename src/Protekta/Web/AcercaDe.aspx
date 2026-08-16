<%@ Page Title="Protekta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AcercaDe.aspx.cs" Inherits="Web.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>
            <asp:Literal
                ID="EncabezadoPrincipal"
                runat="server"
                Text="<%$ Resources:Labels, Default.EncabezadoPrincipal %>" />
        </h3>
        <p>
            <asp:Literal
                ID="Literal1"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.DescripcionAcercaDe %>" />
        </p>
        <h3>
            <asp:Literal
                ID="Literal2"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoServicio %>" />
        </h3>
        <h5>
            <asp:Literal
                ID="Literal3"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno %>" />
        </h5>
    <ul>
        <li>
            <asp:Literal
                ID="Literal19"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno1 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal20"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno2 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal21"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno3 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal22"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno4 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal23"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno5 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal24"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno6 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal25"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno7 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal26"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno8 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal27"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno9 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal28"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno10 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal29"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno11 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal30"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno12 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal31"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno13 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal32"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno14 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal33"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno15 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal34"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno16 %>" />
        </li>
        <li>
            <asp:Literal
                ID="Literal35"
                runat="server"
                Text="<%$ Resources:Labels, AcercaDe.TextoAsesoramientoExterno17 %>" />
        </li>
    </ul>
    <h5>
        <asp:Literal
            ID="Literal4"
            runat="server"
            Text="<%$ Resources:Labels, AcercaDe.TextoServicioIA %>" />
    </h5>
    <ul>
    <li>
        <asp:Literal
        ID="Literal5"
        runat="server"
        Text="<%$ Resources:Labels, AcercaDe.TextoServicioIA1 %>" />
    </li>
    <li>
        <asp:Literal
        ID="Literal6"
        runat="server"
        Text="<%$ Resources:Labels, AcercaDe.TextoServicioIA2 %>" />
    </li>
    <li>
    <asp:Literal
        ID="Literal7"
        runat="server"
        Text="<%$ Resources:Labels, AcercaDe.TextoServicioIA3 %>" />
    </li>
    <li>
        <asp:Literal
        ID="Literal8"
        runat="server"
        Text="<%$ Resources:Labels, AcercaDe.TextoServicioIA4 %>" />
    </li>
    <li>
        <asp:Literal
        ID="Literal9"
        runat="server"
        Text="<%$ Resources:Labels, AcercaDe.TextoServicioIA5 %>" />
    </li>
    <li>
    <asp:Literal
        ID="Literal10"
        runat="server"
        Text="<%$ Resources:Labels, AcercaDe.TextoServicioIA6 %>" />
    </li>

        <li>
        <asp:Literal
        ID="Literal11"
        runat="server"
        Text="<%$ Resources:Labels, AcercaDe.TextoServicioIA7 %>" />
    </li>
    <li>
        <asp:Literal
        ID="Literal12"
        runat="server"
        Text="<%$ Resources:Labels, AcercaDe.TextoServicioIA8 %>" />
    </li>
    <li>
    <asp:Literal
        ID="Literal13"
        runat="server"
        Text="<%$ Resources:Labels, AcercaDe.TextoServicioIA9 %>" />
    </li>
        <li>
        <asp:Literal
        ID="Literal14"
        runat="server"
        Text="<%$ Resources:Labels, AcercaDe.TextoServicioIA10 %>" />
    </li>
    <li>
        <asp:Literal
        ID="Literal15"
        runat="server"
        Text="<%$ Resources:Labels, AcercaDe.TextoServicioIA11 %>" />
    </li>
    <li>
    <asp:Literal
        ID="Literal16"
        runat="server"
        Text="<%$ Resources:Labels, AcercaDe.TextoServicioIA12 %>" />
    </li>
        <li>
        <asp:Literal
        ID="Literal17"
        runat="server"
        Text="<%$ Resources:Labels, AcercaDe.TextoServicioIA13 %>" />
    </li>
    <li>
        <asp:Literal
        ID="Literal18"
        runat="server"
        Text="<%$ Resources:Labels, AcercaDe.TextoServicioIA14 %>" />
    </li>
</ul>
    </main>
</asp:Content>
