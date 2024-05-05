using Microsoft.AspNetCore.Mvc;
using N1_2Bi___LP1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace N1_2Bi___LP1.DAO
{
    public class ClienteDAO : PadraoDAO<ClienteViewModel>
    {
        protected override SqlParameter[] CriaParametros(ClienteViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("nome", model.Nome);
            parametros[1] = new SqlParameter("cpf", model.Cpf);
            parametros[2] = new SqlParameter("telefone", model.Telefone);
            parametros[3] = new SqlParameter("email", model.Email);
            parametros[4] = new SqlParameter("endereco", model.Endereco);

            return parametros;
        }

        protected override ClienteViewModel MontaModel(DataRow registro)
        {
            ClienteViewModel cliente = new ClienteViewModel();
            cliente.Nome = registro["nome"].ToString();
            cliente.Cpf = registro["cpf"].ToString();
            cliente.Telefone = registro["telefone"].ToString();
            cliente.Email = registro["email"].ToString();
            cliente.Endereco = registro["endereco"].ToString();
            return cliente;
        }

        protected override void SetTabela()
        {
            Tabela = "Clientes";
        }
    }
}
