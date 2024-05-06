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
            new SqlParameter("data", model.Data)
        };
            return parametros;
        }

        protected override PedidoViewModel MontaModel(DataRow registro)
        {
            PedidoViewModel c = new PedidoViewModel()
            {
                Id = Convert.ToInt32(registro["id"]),
                Data = Convert.ToDateTime(registro["data"])
            };
            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "Pedido";
            ChaveIdentity = true;
        }
    }

}
