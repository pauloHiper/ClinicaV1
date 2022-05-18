<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="prjClinica.Paginas.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p />
    <p />
    <p />
    <p />
    <hr />

    <h2 style="text-align: center;">Cadastrar Novo Usuário</h2>
    <hr />

    <table>
        <tr>
            <td>
                <asp:Label ID="lbNome" runat="server" Width='50px'/>
            </td>
            <td>
                <asp:TextBox ID="txNome" runat="server" Width='150px' />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbUsuario" runat="server" Width='50px'/>
            </td>
            <td>
                <asp:TextBox ID="txUsuario" runat="server" Width='150px' />
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
            <td>
                <asp:Label ID="lbADM" runat="server" />
            </td>
            <td>
                <asp:RadioButton ID="rbADM" runat="server" GroupName="sexo" />
            </td>
        </tr>
        <tr>
            <td><br /></td>
        </tr>
        <tr>
            <td colspan='2'>
                <asp:Button ID="btOk" runat="server" Width='100px' OnClick="btOk_Click" />
                &nbsp;&nbsp;<asp:Button ID="btVoltar" runat="server" Width='100px' OnClick="btVoltar_Click" />
                &nbsp;&nbsp;<asp:Button ID="btRetorna" runat="server" Width='100px' OnClick="btRetorna_Click" />
            </td>
        </tr>
        <tr>
            <td><br /></td>
        </tr>
        <tr>
            <asp:Literal ID="tabela" runat="server" />
        </tr>
        <tr>
            <td>
                <asp:Label ID="mensagem" runat="server" Width='100px'> </asp:Label>
            </td>
        </tr>
    </table> 
</asp:Content>
