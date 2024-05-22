using Microsoft.AspNetCore.Mvc;
using N1_2Bi___LP1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace N1_2Bi___LP1.DAO
{
    public class PedidoItemDAO : PadraoDAO<PedidoItemViewModel>
    {
        protected override SqlParameter[] CriaParametros(PedidoItemViewModel model)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("@Id", model.Id),
                new SqlParameter("@PedidoId", model.PedidoId),
                new SqlParameter("@ProdutoId", model.ProdutoId),
                new SqlParameter("@Qtde", model.Qtde)
    };
            return parametros;
        }

        protected override PedidoItemViewModel MontaModel(DataRow registro)
        {
            PedidoItemViewModel c = new PedidoItemViewModel()
            {
                Id = Convert.ToInt32(registro["Id"]),
                PedidoId = Convert.ToInt32(registro["PedidoId"]),
                ProdutoId = Convert.ToInt32(registro["ProdutoId"]),
                Qtde = Convert.ToInt32(registro["Qtde"])
            };
            return c;
        }



        public override List<PedidoItemViewModel> Listagem(int id = 0)
        {
            string sql = "EXEC spListagem @Tabela, @Ordem";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@Tabela", "PedidoItem"),
                new SqlParameter("@Ordem", "Id")
            };

            var tabela = HelperDAO.ExecutaSelect(sql, parametros);
            List<PedidoItemViewModel> lista = new List<PedidoItemViewModel>();
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;
        }

        protected override void SetTabela()
        {
            Tabela = "PedidoItem";
            ChaveIdentity = true;
        }
    }

}
