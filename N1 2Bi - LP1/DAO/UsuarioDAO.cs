using Microsoft.AspNetCore.Mvc;
using N1_2Bi___LP1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace N1_2Bi___LP1.DAO
{
    public class UsuarioDAO : PadraoDAO<UsuarioViewModel>
    {
        protected override SqlParameter[] CriaParametros(UsuarioViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[12]; // Agora são 11 parâmetros
            parametros[0] = new SqlParameter("Id", model.Id);
            parametros[1] = new SqlParameter("nome", model.Nome);
            parametros[2] = new SqlParameter("cpf", model.Cpf);
            parametros[3] = new SqlParameter("telefone", model.Telefone);
            parametros[4] = new SqlParameter("email", model.Email);
            parametros[5] = new SqlParameter("endereco", model.Endereco);
            parametros[6] = new SqlParameter("numero", model.Numero);
            parametros[7] = new SqlParameter("senha", model.Senha);
            parametros[8] = new SqlParameter("cidade", model.Cidade);
            parametros[9] = new SqlParameter("estado", model.Estado);
            parametros[10] = new SqlParameter("cep", model.Cep);
            parametros[11] = new SqlParameter("isAdmin", model.IsAdmin);

            return parametros;
        }

        protected override UsuarioViewModel MontaModel(DataRow registro)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            usuario.Id = Convert.ToInt32(registro["Id"]);
            usuario.Nome = registro["Nome"].ToString();
            usuario.Cpf = registro["Cpf"].ToString();
            usuario.Telefone = registro["Telefone"].ToString();
            usuario.Email = registro["Email"].ToString();
            usuario.Senha = registro["Senha"].ToString();
            usuario.Endereco = registro["Endereco"].ToString();
            usuario.Numero = registro["Numero"].ToString();
            usuario.Cidade = registro["Cidade"].ToString();
            usuario.Estado = registro["Estado"].ToString();
            usuario.Cep = registro["Cep"].ToString();
            usuario.IsAdmin = Convert.ToBoolean(registro["IsAdmin"]);

            return usuario;
        }




        public void Inserir(UsuarioViewModel usuario)
        {
            HelperDAO.ExecutaProc("spInsert_Usuarios", CriaParametros(usuario));
        }

        public void Alterar(UsuarioViewModel usuario)
        {
            HelperDAO.ExecutaProc("spUpdate_Usuarios", CriaParametros(usuario));
        }

        public void Excluir(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id)
            };
            HelperDAO.ExecutaProc("spDelete", p);
        }

        protected override void SetTabela()
        {
            Tabela = "Usuarios";
        }


        public List<UsuarioViewModel> ConsultaAvancadaUsuario(string nomeUsuario,
                                                     string cpf,
                                                     int tipo)
        {
            SqlParameter[] parametros = {
              new SqlParameter("nomeCliente", nomeUsuario),
              new SqlParameter("cnpj", cpf),
              new SqlParameter("tipo", tipo)  };

            var tabela = HelperDAO.ExecutaProcSelect("spConsultaAvancadaUsuarios", parametros);
            var lista = new List<UsuarioViewModel>();
            foreach (DataRow dr in tabela.Rows)
                lista.Add(MontaModel(dr));
            return lista;
        }


    }

}
