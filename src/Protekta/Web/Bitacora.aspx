<%@ Page Title="Bitacora" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="Web.Bitacora" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h3>
            <asp:Literal
                ID="EncabezadoPrincipal"
                runat="server"
                Text="<%$ Resources:Labels, Bitacora.Encabezado %>" />
        </h3>
        <asp:GridView ID="GridViewBitacora" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%"
            AllowPaging="true"
            PageSize="10"
            AutoGenerateColumns="False"
            OnPageIndexChanging="GridViewBitacora_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            <Columns>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                <asp:BoundField DataField="FechaEvento" HeaderText="Fecha Evento" />
                <asp:BoundField DataField="UsuarioLogin" HeaderText="Usuario" />
                <asp:BoundField DataField="BitacoraTipoEvento" HeaderText="Tipo Evento" />
                <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
                <asp:BoundField DataField="DVH" HeaderText="DVH" Visible="False" />
            </Columns>
    </asp:GridView>
    </main>
</asp:Content>
