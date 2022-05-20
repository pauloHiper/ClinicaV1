<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="prjClinica._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Scripts/MascaraDigitacao.js"></script>

    <p></p>
    <hr class="linhaEscura" />
      <h1> <asp:Label ID="lbTitulo" runat="server"  /></h1>
    <hr class="linhaEscura" />

    <p></p>
    <p></p>
    <p></p>

    <ul style="list-style-type: none">

      
        <li>
          <hr class="linhaEscura" />
        </li>

        <li>
            <asp:Label ID="lbNome" runat="server" Width="130px" /><asp:TextBox ID="txNome" runat="server" Width="300px"></asp:TextBox>
        </li>
        <li>
            <asp:Label ID="lbSexo" runat="server" Width="130px" /> 
            <asp:Label ID="lbMasc" runat="server"/>&nbsp;<asp:RadioButton ID="rbMasc" runat="server" GroupName="sexo" />&nbsp;
            <asp:Label ID="lbFem" runat="server" />&nbsp;<asp:RadioButton ID="rbFem"  runat="server" GroupName="sexo" />&nbsp;
        </li>
        <li>
            <asp:Label ID="lbDataNascimento" runat="server" Width="130px" /><asp:TextBox ID="txDataNascimento" runat="server" Width="100px"></asp:TextBox>
        </li>
        <li>
             <asp:Label ID="lbPeso" runat="server" Width="130px" /><asp:TextBox ID="txPeso" runat="server" Width="100px" ></asp:TextBox>
        </li>
        <li>
             <asp:Label ID="lbAltura" runat="server" Width="130px" /><asp:TextBox ID="txAltura" runat="server" Width="100px" ></asp:TextBox>
        </li>
         <li>
          <hr class="linhaEscura" />
        </li>
        <li>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lbCadastrar" runat="server"  />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lbBuscarPeloNome" runat="server"  />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lbBuscarPeloId" runat="server"  />
                    </td>
                    <TD style="width: 16px">
                        &nbsp;
                    </TD>
                    <TD style="width: 200px">
                        &nbsp;
                    <asp:Button ID="btExclui" runat="server" Text="Exclui" Width="162px" visible="false" OnClick="btExclui_Click" />
                    </TD>
                    <TD style="width: 210px">
                        &nbsp;
                    </TD>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btOk" runat="server" Text="OK" Width="40px" OnClick="btOk_Click" /></td>
                    <td style="width: 40px">&nbsp;</td>
                    <td>
                        
                        <asp:TextBox ID="buscarPeloNome" runat="server" Width="100px"></asp:TextBox>
                        <asp:Button ID="btBuscarPeloNome" runat="server" Text="OK" Width="40px" OnClick="btBuscarPeloNome_Click" />
                    </td>
                     <td style="width: 40px">&nbsp;</td>
                    <td>                        
                        <asp:TextBox ID="buscarPeloId" runat="server" Width="50px"></asp:TextBox>
                        <asp:Button ID="btBuscarPeloId" runat="server" Text="OK" Width="40px" OnClick="btBuscarPeloId_Click"  />
                    </td>
                    <TD style="width:16px">
                        &nbsp;&nbsp;&nbsp;&nbsp;</TD>
                    <td style="width:200px; margin-left: 80px;">
                        &nbsp;&nbsp;<asp:Button ID="btEdita" runat="server" Text="Edita" Width="162px" visible="false" OnClick="btEdita_Click"/>
                    </td>
                    <td style="width:210px">
                        &nbsp;&nbsp;</td>
                </tr>
            </table>
        </li>
        <br />
        <li>
            <asp:RadioButton ID="rbOrderName" runat="server" Text="Ordenar por Nome" AutoPostBack="true" OnCheckedChanged="rbOrderName_CheckedChanged" GroupName="ordena"/>
            <asp:RadioButton ID="rbOrderId" runat="server" Text="Ordenar por ID" AutoPostBack="true" OnCheckedChanged="rbOrderId_CheckedChanged" GroupName="ordena"/>
        </li>
        <li>
           <hr class="linhaEscura" />     
        </li>
        <li>
            <asp:Label ID="mensagem" runat="server"  /> 
        </li>
        <li>
            <asp:Literal ID="txRelatorio" runat="server"></asp:Literal>
        </li>
        <li>
            <asp:Button ID="btSair" runat="server" Text="Sair" Width="70px" OnClick="btSair_Click"/>
        </li>
    </ul>

   

</asp:Content>