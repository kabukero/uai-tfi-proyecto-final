<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LogoutControl.ascx.cs" Inherits="Web.LogoutControl" %>

<asp:HyperLink
    ID="HyperLink5"
    runat="server"
    CssClass="nav-link"
    NavigateUrl="~/Logout"
    Text="<%$ Resources:Labels, Master.TextoLogout %>" />
