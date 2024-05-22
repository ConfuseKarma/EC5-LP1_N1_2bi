using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
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
            PedidoViewModel pedido = new PedidoViewModel();
            pedido.Id = Convert.ToInt32(registro["id"]);
            pedido.Data = Convert.ToDateTime(registro["DataPedido"]);
            pedido.UsuarioId = Convert.ToInt32(registro["UsuarioId"]);

            if (registro.Table.Columns.Contains("NomeUsuario"))
                pedido.NomeUsuario = registro["NomeUsuario"].ToString();

            if (registro.Table.Columns.Contains("QuantidadeProdutos"))
                pedido.QuantidadeProdutos = Convert.ToInt32(registro["QuantidadeProdutos"]);

            if (registro.Table.Columns.Contains("ValorTotal"))
                pedido.ValorTotal = Convert.ToDecimal(registro["ValorTotal"]);

            return pedido;
        }

        protected override string NomeSpListagem { get; set; } = "spListarPedidoDetalhes";

        public override List<PedidoViewModel> Listagem(int id = 0)
        {
            string sql = "EXEC spListarPedidos;";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
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
