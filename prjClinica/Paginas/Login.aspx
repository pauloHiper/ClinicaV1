<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="prjClinica.Paginas.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p></p>
    <h1> <asp:Label ID="lbTitulo" runat="server"  /></h1>


    <p />
    <p />
    <p />
    <p />
    <hr />
    <table>
        <tr>
            <td>
                <asp:Label ID="lbUsuario" runat="server" Width='50px'/>
            </td>
            <td>
                <asp:TextBox ID="txUsuario" runat="server" Width='150px'/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbSenha" runat="server" Width='50px'/>
            </td>
            <td>
                <asp:TextBox ID="txSenha" runat="server" Width='150px' TextMode="Password"/>
            </td>
        </tr>
        <tr>
            <td><br /></td>
        </tr>
        <tr>
            <td colspan='2'>
                <asp:Button ID="btOk" runat="server" Width='150px' OnClick="btOk_Click" Text="OK"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="mensagem" runat="server" Width='100px'> </asp:Label>
            </td>
        </tr>
    </table> 
</asp:Content>
