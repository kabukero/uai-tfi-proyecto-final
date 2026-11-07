<%@ Page Title="Carrito" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoFactura.aspx.cs" Inherits="Web.ListadoFactura" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h3>
            <asp:Literal
                ID="EncabezadoPrincipal"
                runat="server"
                Text="<%$ Resources:Labels, Factura.Encabezado %>" />
        </h3>

		<asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
			<p class="text-danger">
				<asp:Literal runat="server" ID="FailureText" />
			</p>
		</asp:PlaceHolder>

        <asp:GridView ID="GridViewFacturas" runat="server"
            CellPadding="4"
            ForeColor="#333333"
            GridLines="None"
            Width="100%"
            AllowPaging="true"
            PageSize="10"
            AutoGenerateColumns="False"
            DataKeyNames="Id"
            OnRowDataBound="GridViewFacturas_RowDataBound">

            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

            <Columns>

                <%-- Parent: Factura --%>

                <asp:TemplateField>

                    <ItemTemplate>

                        <table width="100%" cellpadding="5" cellspacing="0">

                            <tr style="background-color:#E2EAF3;">
                                <td colspan="6">
                                    <strong>Factura #<%# Eval("Id") %></strong>
                                </td>
                            </tr>

                            <tr>
                                <td><strong>Fecha:</strong></td>
                                <td><%# Eval("Fecha", "{0:dd/MM/yyyy}") %></td>

                                <td><strong>Usuario:</strong></td>
                                <td>
                                    <%# Eval("Usuario.Nombre") %>
                                    <%# Eval("Usuario.Apellido") %>
                                </td>
                                <td><strong>Acciones:</strong></td>
                                <td align="right">
                                    <asp:HyperLink
                                        ID="hlComprobante"
                                        runat="server"
                                        NavigateUrl='<%# "FacturaComprobante.aspx?Id=" + Eval("Id") %>'
                                        Text="Ver comprobante"
                                        Target="_blank"
                                        CssClass="btn btn-primary btn-md" />
                                </td>
                            </tr>
                        </table>

                        <%-- Child: FacturaDetalle --%>

                        <asp:GridView ID="GridViewDetalleFactura" runat="server"
                            CellPadding="4"
                            ForeColor="#333333"
                            GridLines="Horizontal"
                            Width="98%"
                            HorizontalAlign="Right"
                            AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True">

                            <HeaderStyle BackColor="#D9E2F3"
                                Font-Bold="True"
                                ForeColor="#333333" />

                            <RowStyle BackColor="White" ForeColor="#333333" />

                            <Columns>

                                <asp:BoundField DataField="Id"
                                    HeaderText="Detalle ID" />

                                <asp:BoundField DataField="Subscripcion.Id"
                                    HeaderText="Suscripción ID" />

                                <asp:BoundField DataField="Subscripcion.Titulo"
                                    HeaderText="Suscripción" />

                                <asp:BoundField DataField="Subscripcion.Descripcion"
                                    HeaderText="Descripción" />

                            </Columns>

                        </asp:GridView>

                        <br />

                    </ItemTemplate>

                </asp:TemplateField>

            </Columns>

        </asp:GridView>

    </main>
</asp:Content>
