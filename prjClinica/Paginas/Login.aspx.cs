using prjClinica.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjClinica.Paginas
{
    public partial class Login : System.Web.UI.Page
    {
        private void inicializaLabels()
        {
            lbSenha.Text = "Senha";
            lbUsuario.Text = "Usuário";
            lbTitulo.Text = "Bem Vindo!";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            inicializaLabels();
        }
        protected void btOk_Click(object sender, EventArgs e)
        {
            using(Conexao con = new Conexao(null))
            {
                con.open();
                Usuario u = Usuario.buscaPorNome(txUsuario.Text, txSenha.Text, con);
                if(u != null)
                {
                    Session["usuario"] = u;
                    Response.Redirect("~/Default.aspx", false);
                    return;
                }
                else
                {
                    mensagem.Text = "Usuário não encontrado!";
                    return;
                }
            }
        }
    }
}