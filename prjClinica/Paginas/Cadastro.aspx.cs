using prjClinica.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjClinica.Paginas
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public static Clinica clinica;
        public Usuario usuario;

        private void inicializaLabels()
        {
            lbNome.Text = "Nome";
            lbSenha.Text = "Senha";
            lbUsuario.Text = "Usuario";
            lbADM.Text = "Usuário ADM";
        }
        private void inicializaBotoes()
        {
            btOk.Text = "Cadastrar";
            btVoltar.Text = "Sair";
            btRetorna.Text = "Voltar";
            rbADM.Checked = false;
        }
        private bool valida()
        {
            if (txNome.Text == "")
            {
                mensagem.Text = "Campo Nome é Obrigatório!";
                txSenha.Focus();
                return false;
            }
            if (txSenha.Text == "")
            {
                mensagem.Text = "Campo Senha é Obrigatória!";
                txSenha.Focus();
                return false;
            }
            if (txUsuario.Text == "")
            {
                mensagem.Text = "Campo Usuário é Obrigatório!";
                txUsuario.Focus();
                return false;
            }
            return true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            mensagem.Text = "";
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Paginas/Login.aspx");
                return;
            }
            usuario = (Usuario)Session["usuario"];
            try
            {
                using (Conexao con = new Conexao(null))
                {
                    con.open();
                }
                if (clinica == null)
                {

                    using (Conexao con = new Conexao(usuario))
                    {
                        con.open();
                        clinica = new Clinica("Clínica de estética Corporal do Santa Cecilia", con);
                    }

                }
                inicializaLabels();
                inicializaBotoes();
            }
            catch (Exception e1)
            {
                mensagem.Text = "Erro iniciando sistema: " + e1.Message;
            }
        }

        protected void btOk_Click(object sender, EventArgs e)
        {
            if (!valida())
                return;

            Usuario usuarioNovo = new Usuario(txNome.Text,txUsuario.Text,txSenha.Text, rbADM.Checked ? "A" : "B");

            clinica.entraUsuario(usuario);

            try
            {
                using (Conexao con = new Conexao(usuario))
                {
                    con.open();
                    usuarioNovo.insere(con);
                    clinica.carregaLista(con, 'a');
                    Session["usuario"] = usuarioNovo;
                    Response.Redirect("~/Default.aspx");
                }
            }
            catch (Exception ex)
            {
                mensagem.Text = "Erro acessando tabela de usuarios";
                if (usuario.idUsuario == 1)
                    mensagem.Text += "<br />" + ex.Message;
            }
        }

        protected void btVoltar_Click(object sender, EventArgs e)
        {
            Session["usuario"] = null;
            Response.Redirect("~/Paginas/Login.aspx", false);
            return;
        }

        protected void btRetorna_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false); ;
            return;
        }
    }
}