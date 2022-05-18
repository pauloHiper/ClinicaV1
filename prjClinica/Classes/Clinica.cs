using prjClinica.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjClinica.Classes
{
  
    public class Clinica
    {
        private String nome;
        public int idClinica = 1;
        private List<Usuario> listaUsers = new List<Usuario>();
        private List<Paciente> lista = new List<Paciente>();
        public Clinica(String nome, Conexao con)
        {
            this.nome = nome;
            lista = Paciente.listaPacientes(con, 'a');
            listaUsers = Usuario.listaUsuarios(con, 'a');
        }

        public void carregaLista(Conexao con, char order)
        {
            lista = Paciente.listaPacientes(con, order);
            listaUsers = Usuario.listaUsuarios(con, order);
        }

        public string getNome()
        {
            return nome;
        }

        public void entraPaciente(Paciente p)
        {
            lista.Add(p);
        }
        public void entraUsuario(Usuario u)
        {
            listaUsers.Add(u);
        }

        public String relatorio()  
        {
            Tabela tab = new Tabela(lista.Count, 6);
            int i = 0;
            foreach (Paciente u in lista)
            {
              
                tab.celula[i, 0] = u.getId()+"";
                tab.celula[i, 1] = u.getNome();
                tab.celula[i, 2] = u.getPeso()+"";
                tab.celula[i, 3] = u.getAltura()+"";
                tab.celula[i, 4] = u.getDataNascimento().ToString("dd/MM/yyyy");
                tab.celula[i, 5] = u.diagnosticoPeso();

                i++;
            }

            string[] hd = { "Seq", "Nome", "Peso", "Altura","Data<br />Nascimento", "Diagnóstico" };
            return tab.tabela(hd);
        }
        public String relatorioUsers()
        {
            Tabela tab = new Tabela(listaUsers.Count, 2);
            int i = 0;
            foreach (Usuario u in listaUsers)
            {

                tab.celula[i, 0] = u.login;
                tab.celula[i, 1] = u.nome;
                i++;
            }
            string[] hd = { "Login", "Nome" };
            return tab.tabela(hd);
        }
    }
}