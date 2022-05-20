using prjClinica.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 

namespace prjClinica
{
    public partial class _Default : Page
    {
        public static Clinica clinica;

        public Usuario usuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            mensagem.Text = "";
            if(Session["usuario"] == null)
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
                    btEdita.Visible = btExclui.Visible = Session["paciente"] != null;
                    btOk.Enabled = !btEdita.Visible;
                }
                if (clinica == null)
                {

                    using (Conexao con = new Conexao(usuario))
                    {
                        con.open();
                        clinica = new Clinica("Clínica de estética Paulo Galvao da Silva", con);
                    }
                    txRelatorio.Text = clinica.relatorio();

                }
                inicializaLabels();
            }
            catch (Exception e1)
            {
                mensagem.Text = "Erro iniciando sistema: " + e1.Message;
            }
        }

        private void inicializaLabels()
        {
            lbAltura.Text = "Altura";
            lbDataNascimento.Text = "Data Nascimento";
            lbFem.Text = "Fem";
            lbMasc.Text = "Masc";
            lbNome.Text = "Nome";
            lbPeso.Text = "Peso";
            lbSexo.Text = "Sexo";
            lbTitulo.Text = clinica.getNome();
            lbBuscarPeloNome.Text = "Buscar pelo nome";
            lbBuscarPeloId.Text = "Buscar pelo ID";
            lbCadastrar.Text = "Cadastrar";
        }

        private void reset()
        {
            txAltura.Text = "";
            txDataNascimento.Text = "";
            txNome.Text = "";
            txPeso.Text = "";
            rbFem.Checked = false;
            rbMasc.Checked = false;
            buscarPeloId.Text = "";
            buscarPeloNome.Text = "";
            btEdita.Visible = btExclui.Visible = false;
        }

        private bool valida(out double altura, out double peso, out DateTime dataNascimento)
        {
            
            if (!Double.TryParse(txAltura.Text, out altura))
            {
                mensagem.Text = "Erro na altura digitada";
                txAltura.Focus();
                altura =   peso = 0;
                dataNascimento = DateTime.Now;
                return false;
            }
            if (!Double.TryParse(txPeso.Text, out peso))
            {
                mensagem.Text = "Erro no peso digitado";
                txPeso.Focus();
                peso = 0;               
                dataNascimento = DateTime.Now;
                return false;
            }          

            if (!DateTime.TryParse(txDataNascimento.Text, out dataNascimento))
            {
                mensagem.Text = "Erro na data de nascimento";
                txDataNascimento.Focus();
                return false;
            }

            if (rbFem.Checked == false && rbMasc.Checked == false)
            {
                mensagem.Text = "Selecione o sexo";
                return false;
            }

            if (txNome.Text == "")
            {
                mensagem.Text = "Difgite o nome";
                return false;
            }

            return true;
        }


        protected void btOk_Click(object sender, EventArgs e)
        {
            mensagem.Text = "";
            double peso, altura;

            DateTime dataNascimento;

            if (!valida(out altura, out peso, out dataNascimento))
                return;

            Paciente paciente = new Paciente(txNome.Text, rbMasc.Checked ? 'M' : 'F', (float)peso, (float)altura, dataNascimento);

            clinica.entraPaciente(paciente);

            try
            {
                using (Conexao con = new Conexao(usuario))
                {
                    con.open();
                    paciente.insere(con);
                    clinica.carregaLista(con, rbOrderId.Checked ? 'i' : 'n');
                    txRelatorio.Text = clinica.relatorio();
                    Session["paciente"] = paciente;
                    reset();
                }
            }
            catch (Exception ex)
            {
                mensagem.Text = "Erro acessando tabela de pacientes";
                if (usuario.idUsuario == 1)
                    mensagem.Text += "<br />" + ex.Message;
            }
        }

        private void popula(Paciente paciente)
        {
            txNome.Text = paciente.getNome();
            txDataNascimento.Text = paciente.getDataNascimento().ToString("dd/MM/yyyy");
            txPeso.Text = (paciente.getPeso() + "").Replace(".", ",");
            txAltura.Text = (paciente.getAltura() + "").Replace(".", ",");
            rbFem.Checked = paciente.getSexo() == 'F';
            rbMasc.Checked = paciente.getSexo() == 'M';
        }

        protected void btBuscarPeloNome_Click(object sender, EventArgs e)
        {
            mensagem.Text = "";
            if (buscarPeloNome.Text.Trim().Length < 3)
            {
                mensagem.Text = "Utilize ao menos 3 digitos para a busca.";
                return;
            }
            try
            {
                using (Conexao con = new Conexao(usuario))
                {
                    con.open();
                    List<Paciente> lista = Paciente.buscaLike(buscarPeloNome.Text, con);

                    if (lista.Count == 0)
                    {
                        mensagem.Text = "Nenhum paciente corresponde a sua busca";
                        return;
                    }

                    if (lista.Count > 1)
                    {
                        mensagem.Text = "Mais de um paciente corresponde a sua busca";
                        return;
                    }
                    Session["paciente"] = lista[0];
                    popula(lista[0]);
                    buscarPeloId.Text = lista[0].getId().ToString();
                    btEdita.Visible = btExclui.Visible = true;
                    btOk.Enabled = !btEdita.Visible;
                }
            }
            catch (Exception ex)
            {
                mensagem.Text = "Erro acessando tabela de pacientes";
                if (usuario.idUsuario == 1)
                    mensagem.Text += "<br />" + ex.Message;
            }
        }
        protected void btBuscarPeloId_Click(object sender, EventArgs e)
        {
            mensagem.Text = "";
            int id;

            if (!Int32.TryParse(buscarPeloId.Text, out id))
            {
                mensagem.Text = "Id digitado inválido";
                return;
            }
            try
            {
                using (Conexao con = new Conexao(usuario))
                {
                    con.open();
                    Paciente paciente = Paciente.busca(id, con);
                    if (paciente == null)
                    {
                        mensagem.Text = "Paciente id: " + id + " não encontrado";
                        return;
                    }
                    Session["paciente"] = paciente;
                    popula(paciente);
                    btEdita.Visible = btExclui.Visible = true;
                    btOk.Enabled = !btEdita.Visible;
                }
            }
            catch (Exception ex)
            {
                mensagem.Text = "Erro acessando tabela de pacientes";
                if (usuario.idUsuario == 1)
                    mensagem.Text += "<br />" + ex.Message;
            }
        }

        protected void btEdita_Click(object sender, EventArgs e)
        {
            mensagem.Text = "";
            Paciente paciente = (Paciente)Session["paciente"];
            if (paciente == null)
            {
                mensagem.Text = "Erro inesperado E200";
                return;
            }
            try
            {
                using (Conexao con = new Conexao(usuario))
                {
                    con.open();
                    double peso, altura;

                    DateTime dataNascimento;

                    if (!valida(out altura, out peso, out dataNascimento))
                        return;
                    paciente.setPeso((float)peso);
                    paciente.setAltura((float)altura);
                    paciente.setNome(txNome.Text);
                    paciente.setSexo(rbMasc.Checked ? 'M' : 'F');
                    paciente.setDataNascimento(dataNascimento);

                    paciente.atualiza(con);
                    clinica.carregaLista(con, rbOrderId.Checked ? 'i' : 'n');
                    txRelatorio.Text = clinica.relatorio();
                }
            }
            catch (Exception ex)
            {
                mensagem.Text = "Erro acessando tabela de pacientes";
                if (usuario.idUsuario == 1)
                    mensagem.Text += "<br />" + ex.Message;
            }
        }

        protected void btExclui_Click(object sender, EventArgs e)
        {
            mensagem.Text = "";
            Paciente paciente = (Paciente)Session["paciente"];
            if (paciente == null)
            {
                mensagem.Text = "Erro inesperado E230";
                return;
            }
            try
            {
                using (Conexao con = new Conexao(usuario))
                {
                    con.open();
                    paciente.deleta(con);
                    clinica.carregaLista(con, rbOrderId.Checked ? 'i' : 'n');
                    txRelatorio.Text = clinica.relatorio();
                    reset();
                }
            }
            catch (Exception ex)
            {
                mensagem.Text = "Erro acessando tabela de pacientes";
                if (usuario.idUsuario == 1)
                    mensagem.Text += "<br />" + ex.Message;
            }
        }

        protected void rbOrderName_CheckedChanged(object sender, EventArgs e)
        {
            using (Conexao con = new Conexao(usuario))
            {
                con.open();
                clinica.carregaLista(con, 'n');
                txRelatorio.Text = clinica.relatorio();
                reset();
            }
        }

        protected void rbOrderId_CheckedChanged(object sender, EventArgs e)
        {
            using (Conexao con = new Conexao(usuario))
            {
                con.open();
                clinica.carregaLista(con, 'i');
                txRelatorio.Text = clinica.relatorio();
                reset();
            }
        }

        protected void btSair_Click(object sender, EventArgs e)
        {
            Session["usuario"] = null;
            Response.Redirect("~/Paginas/Login.aspx", false);
            return;
        }
    }
}