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
            ReviewsViewModel review = new ReviewsViewModel();
            review.Id = Convert.ToInt32(registro["id"]);
            review.ProdutoId = Convert.ToInt32(registro["ProdutoId"]);
            review.UsuarioId = Convert.ToInt32(registro["UsuarioId"]);
            review.Pontuacao = Convert.ToInt32(registro["Pontuacao"]);
            review.Descricao = registro["descricao"].ToString();

            if (registro.Table.Columns.Contains("NomeUsuario"))
                review.NomeUser = registro["NomeUsuario"].ToString();

            if (registro.Table.Columns.Contains("NomeProduto"))
                review.NomeProduto = registro["NomeProduto"].ToString();

            // Se houver necessidade de mais campos, você pode adicioná-los aqui

            return review;
        }

        protected override void SetTabela()
        {
            Tabela = "Reviews";
        }

        protected override string NomeSpListagem { get; set; } = "spListarReviewsPorProduto";


        public override List<ReviewsViewModel> Listagem(int id)
        {
            var p = new SqlParameter[]
            {
                 new SqlParameter("ProdutoId", id)

            };
            DataTable tabela = HelperDAO.ExecutaProcSelect(NomeSpListagem, p);
            List<ReviewsViewModel> lista = new List<ReviewsViewModel>();
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;
        }
    }
}
