<%@ Page Title="Backup" Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Backup.aspx.cs" Inherits="Web.Backup" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h3>
            <asp:Literal
                ID="EncabezadoPrincipal"
                runat="server"
                Text="<%$ Resources:Labels, Backup.Encabezado %>" />
        </h3>

        <asp:Button ID="BtnBackup" runat="server" OnClick="BtnBackup_Click" Text="<%$ Resources:Labels, Backup.RealizarBackup %>" CssClass="btn btn-primary btn-md" />

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <br />
                <asp:Panel ID="pnlBackupError" runat="server"
                    CssClass="alert alert-danger alert-dismissible fade show"
                    Visible="false">
                    <strong>Error:</strong>
                    <asp:Label ID="lblBackupError" runat="server"></asp:Label>
                    <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                </asp:Panel>
            </div>
        </div>

        <asp:GridView ID="GridViewBackup" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%"
            AllowPaging="true"
            PageSize="10"
            AutoGenerateColumns="False">
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
                <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha" />
                <asp:BoundField DataField="NombreArchivo" HeaderText="Archivo" />
                <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
            </Columns>
        </asp:GridView>

    </main>
</asp:Content>
