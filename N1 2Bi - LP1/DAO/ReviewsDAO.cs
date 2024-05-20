using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using N1_2Bi___LP1.Models;
using System.Data;
using System.Data.SqlClient;

namespace N1_2Bi___LP1.DAO
{
    public class ReviewsDAO : PadraoDAO<ReviewsViewModel>
    {
        protected override SqlParameter[] CriaParametros(ReviewsViewModel model)
        {
            SqlParameter[] parametros =
            {
            new SqlParameter("id", model.Id),
            new SqlParameter("ProdutoId", model.ProdutoId),
            new SqlParameter("UsuarioId", model.UsuarioId),
            new SqlParameter("Pontuacao", model.Pontuacao),
            new SqlParameter("Descricao", model.Descricao)
            };
            return parametros;
        }

        protected override ReviewsViewModel MontaModel(DataRow registro)
        {
            ReviewsViewModel c = new ReviewsViewModel()
            {
                Id = Convert.ToInt32(registro["id"]),
                ProdutoId = Convert.ToInt32(registro["ProdutoId"]),
                UsuarioId = Convert.ToInt32(registro["UsuarioId"]),
                Pontuacao = Convert.ToInt32(registro["Pontuacao"]),
                Descricao = registro["Descricao"].ToString()
            };
            return c;
        }

        protected override void SetTabela()
        {
            Tabela = "Reviews";
        }
    }
}
