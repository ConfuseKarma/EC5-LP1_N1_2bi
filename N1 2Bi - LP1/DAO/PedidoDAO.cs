using Microsoft.AspNetCore.Mvc;
using N1_2Bi___LP1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace N1_2Bi___LP1.DAO
{
    public class PedidoDAO : PadraoDAO<PedidoViewModel>
    {
        protected override SqlParameter[] CriaParametros(PedidoViewModel model)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("id", model.Id),
                new SqlParameter("data", model.Data),
                new SqlParameter("usuarioId", model.UsuarioId) 
            };
            return parametros;
        }

        protected override PedidoViewModel MontaModel(DataRow registro)
        {
            PedidoViewModel c = new PedidoViewModel()
            {
                Id = Convert.ToInt32(registro["id"]),
                Data = Convert.ToDateTime(registro["data"]),
                UsuarioId = Convert.ToInt32(registro["usuarioId"])
            };
            return c;
        }

        public override List<PedidoViewModel> Listagem(int id = 0)
        {
            string sql = "EXEC spListagem @Tabela, @Ordem";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Tabela", "Pedidos"),
                new SqlParameter("@Ordem", "Id")
            };

            var tabela = HelperDAO.ExecutaSelect(sql, parametros);
            List<PedidoViewModel> lista = new List<PedidoViewModel>();
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;
        }
        protected override void SetTabela()
        {
            Tabela = "Pedidos";
            ChaveIdentity = true;
        }
    }

}
