using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace prjClinica.Classes
{
    public class Usuario : TabelaBase, IComparable<Usuario>
    {
       private static string tabela = "Usuario";
       private static string nomeId = "id" + tabela;
        private static string loginUser = "login";
       private static string nomeTabela = prefixo + tabela;

       private static string campos = "nome, login, senha, perfil";

       public int idUsuario;
       public String login { get; set; }
       public String nome { get; set; }
       public String senha { get; set; }
       public String perfil { get; set; }
       public Usuario(String nome, String login, String senha, String perfil)
       {
           this.nome = nome;
           this.login = login;
           this.senha = senha;
           this.perfil = perfil;
       }
        public Usuario()
        {
            this.nome =default(String);
            this.login = default(String); 
            this.senha = default(String); 
            this.perfil = default(String); 
        }
        public static Usuario busca(string id, Conexao con)
       {
           int iId;
           if (Int32.TryParse(id, out iId))
           {
               return busca(iId, con);
           }
           else
           {
               throw new Exception("Usuário " + id + " não encontrado: " + tabela);
           }
       }

    public static Usuario buscaPorNome(string usuario, string senha, Conexao con)
    {
        int iId;
        if (usuario != "" && senha != "")
        {

            return buscaPorNomeUsuario(usuario, senha, con);
        }
        else
        {
            throw new Exception("Usuário " + usuario + " não encontrado: " + tabela);
        }
    }

        public int insere(Conexao con) 
       {
           try
           {
               String sql = String.Concat("INSERT INTO ", nomeTabela, " (", campos, ",stAtivo) Values ('", nome, "','", login, "','", senha + "','" + perfil + "',1)");
               idUsuario = Conexao.executaQuery(con, sql, prefixo + tabela);
               criando(con, tabela, idUsuario);
               return idUsuario;
           }
           catch (Exception)
           {
               throw;
           }
       }

       public int atualiza(Conexao con)
       {
           try
           {
               StringBuilder sql = new StringBuilder("UPDATE " + nomeTabela + " SET ");
               sql.Append("nome='" + nome + "',");
               sql.Append("login='" + login + "',");
               sql.Append("senha='" + senha + "',");
               sql.Append("perfil='" + perfil + "' "); 
               sql.Append(" WHERE id" + tabela + "=" + idUsuario);
               atualizando(con, tabela, idUsuario);
               return idUsuario;
           }
           catch (Exception)
           {
               throw;
           }
       }

       public static Usuario busca(int id, Conexao con)
       {
           try
           {
               String sql = String.Concat("SELECT ", campos,  " FROM ", nomeTabela, " WHERE ", nomeId, "=", id);
               DataTable dt = Conexao.executaSelect(con, sql);
               if (dt.Rows.Count == 0) throw new Exception("Erro inesperado, id" + tabela + " não encontrado");
               DataRow[] r = dt.Select();
               Usuario item = new Usuario(r[0][0].ToString(), r[0][1].ToString(), r[0][2].ToString(), r[0][3].ToString());
          //   item.carregaLog(con, tabela, id);
               item.idUsuario = id;
               return item;
           }
           catch (Exception)
           {
               throw;
           }
       }

        public static Usuario buscaPorNomeUsuario(string usuario, string senha, Conexao con)
        {
            try
            {
                String sql = String.Concat("SELECT id", tabela, " FROM ", nomeTabela, " WHERE ", loginUser, "='", usuario, "' and senha = '", senha, "'"); ;
                DataTable dt = Conexao.executaSelect(con, sql);
                if (dt.Rows.Count == 0) return null;
                DataRow[] r = dt.Select();

                //   item.carregaLog(con, tabela, id);
                return busca(r[0][0].ToString(), con);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Usuario> listaUsuarios(Conexao con, char order)
        {
            try
            {
                String sql = String.Concat("SELECT id", tabela, " FROM ", nomeTabela, " where stAtivo=1 ORDER BY id", tabela);

                List<Usuario> lista = new List<Usuario>();

                DataTable dt = Conexao.executaSelect(con, sql);

                if (dt.Rows.Count == 0) return lista;

                DataRow[] ids = dt.Select();

                foreach (DataRow row in ids)
                {
                    lista.Add(Usuario.busca(row[0].ToString(), con));
                }


                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Usuario busca(int id, List<Usuario> lista)
        {
            Usuario u = new Usuario();

            u.idUsuario = id;
            int bus = lista.BinarySearch(u);
            if(bus >= 0) return lista[bus];
            return null;
        }

        public int CompareTo(Usuario other)
        {
            return idUsuario - other.idUsuario;
        }
    }
}