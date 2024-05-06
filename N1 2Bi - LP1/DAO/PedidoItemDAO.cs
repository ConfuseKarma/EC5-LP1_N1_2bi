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
            new SqlParameter("id", model.Id),
            new SqlParameter("PedidoId", model.PedidoId),
            new SqlParameter("CidadeId", model.CidadeId),
            new SqlParameter("Qtde", model.Qtde)
        };
            return parametros;
        }

        protected override PedidoItemViewModel MontaModel(DataRow registro)
        {
            PedidoItemViewModel c = new PedidoItemViewModel()
            {
                Id = Convert.ToInt32(registro["id"]),
                CidadeId = Convert.ToInt32(registro["Cidadeid"]),
                PedidoId = Convert.ToInt32(registro["PedidoId"]),
                Qtde = Convert.ToInt32(registro["id"]),
            };
            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "PedidoItem";
            ChaveIdentity = true;
        }
    }

}
