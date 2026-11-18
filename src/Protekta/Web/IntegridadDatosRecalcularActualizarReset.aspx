<%@ Page Title="Reset DV" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IntegridadDatosRecalcularActualizarReset.aspx.cs" Inherits="Web.IntegridadDatosRecalcularActualizarReset" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %></h2>

        <asp:Label ID="lblEmail" runat="server" Text="Enter your E-mail:" />
         <asp:TextBox
            ID="txtEmail"
            runat="server"
            CssClass="form-control" />
        <br />

        <asp:Label ID="lblPassword" runat="server" Text="Enter your Password:" />
        <asp:TextBox
            ID="txtPassword"
            runat="server"
            CssClass="form-control"
            TextMode="Password" />
        <br />

        <asp:Button
            ID="btnLogin"
            runat="server"
            Text="Submit"
            CssClass="btn btn-primary btn-md"
            OnClick="btnLogin_Click" />
    </main>
</asp:Content>
