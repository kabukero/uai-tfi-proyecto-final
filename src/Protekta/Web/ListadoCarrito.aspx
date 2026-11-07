<%@ Page Title="Carrito" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoCarrito.aspx.cs" Inherits="Web.ListadoCarrito" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h3>
            <asp:Literal
                ID="EncabezadoPrincipal"
                runat="server"
                Text="<%$ Resources:Labels, Carrito.Encabezado %>" />
        </h3>

		<asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
			<p class="text-danger">
				<asp:Literal runat="server" ID="FailureText" />
			</p>
		</asp:PlaceHolder>

        <asp:GridView ID="GridViewCarrito" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%"
            AllowPaging="true"
            PageSize="10"
            AutoGenerateColumns="False"
            OnRowCommand="gvCarrito_RowCommand">
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
                <asp:BoundField DataField="Subscripcion.Id" HeaderText="Id" Visible="False" />
                <asp:BoundField DataField="Subscripcion.Titulo" HeaderText="Plan" />
                <asp:BoundField DataField="Subscripcion.Precio" HeaderText="Precio" />
                <asp:BoundField DataField="Subscripcion.Descripcion" HeaderText="Descripcion" />
				<asp:TemplateField HeaderText="Acción">
					<ItemTemplate>
						<asp:Button ID="BtnEliminar" runat="server" CausesValidation="false" CommandName="EliminarDetalle" Text="<%$ Resources:Labels, Carrito.Remover %>" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-primary" />
					</ItemTemplate>
				</asp:TemplateField>
            </Columns>
        </asp:GridView>
		<h3><asp:Label id="LblTotal" runat="server" Text="Total: $" /></h3>
        <asp:DropDownList
            ID="ddlMedioPago"
            runat="server"
            CssClass="form-control">
        </asp:DropDownList>
		<br />
		<asp:Button ID="BtnFinalizarCompra" runat="server" Text="Finalizar compra" OnClick="BtnFinalizarCompra_Click" CssClass="btn btn-primary" />
        <h3><asp:Label id="LblResultado" runat="server" Text="<%$ Resources:Labels, Carrito.CarritoVacio %>" /></h3>
    </main>
</asp:Content>
