<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="prjClinica.Paginas.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="menu"></asp:Label>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="margin-left: 0px; margin-right: 0px" Text="Cadastrar" Width="226px" />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Entrar" Width="232px" />
</asp:Content>
