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
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("nome", model.Nome);
            parametros[1] = new SqlParameter("cpf", model.Cpf);
            parametros[2] = new SqlParameter("telefone", model.Telefone);
            parametros[3] = new SqlParameter("email", model.Email);
            parametros[4] = new SqlParameter("endereco", model.Endereco);

            return parametros;
        }

        protected override UsuarioViewModel MontaModel(DataRow registro)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            usuario.Nome = registro["nome"].ToString();
            usuario.Cpf = registro["cpf"].ToString();
            usuario.Telefone = registro["telefone"].ToString();
            usuario.Email = registro["email"].ToString();
            usuario.Endereco = registro["endereco"].ToString();
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
    }

}
