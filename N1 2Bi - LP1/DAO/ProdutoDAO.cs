using Microsoft.AspNetCore.Mvc;
using N1_2Bi___LP1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace N1_2Bi___LP1.DAO
{
    public class ProdutoDAO : PadraoDAO<ProdutoViewModel>
    {
        protected override SqlParameter[] CriaParametros(ProdutoViewModel model)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("id", model.Id);
            parametros[1] = new SqlParameter("nome", model.Nome);
            parametros[2] = new SqlParameter("descricao", model.Descricao);
            parametros[3] = new SqlParameter("preco", model.Preco);
            parametros[4] = new SqlParameter("imagem", model.ImagemEmByte);

            return parametros;
        }

        protected override ProdutoViewModel MontaModel(DataRow registro)
        {
            ProdutoViewModel produto = new ProdutoViewModel();
            produto.Id = Convert.ToInt32(registro["id"]);
            produto.Nome = registro["nome"].ToString();
            produto.Descricao = registro["descricao"].ToString();
            produto.Preco = Convert.ToDecimal(registro["preco"]);

            if (registro["imagem"] != DBNull.Value)
            {
                // Aqui você converte a imagem em byte[]
                produto.ImagemEmByte = registro["imagem"] as byte[];
            }

            if (registro.Table.Columns.Contains("avaliacao"))
                produto.Avaliacao = Convert.ToDecimal(registro["avaliacao"]);

            if (registro.Table.Columns.Contains("numeroAvaliacao"))
                produto.NumeroAvaliacoes = Convert.ToInt32(registro["numeroAvaliacao"]);

            // Se houver necessidade de mais campos, você pode adicioná-los aqui

            return produto;
        }


        public override void Update(ProdutoViewModel produto)
        {
            HelperDAO.ExecutaProc("spUpdate_Produtos", CriaParametros(produto));
        }

        protected override void SetTabela()
        {
            Tabela = "Produtos";
        }

        public override List<ProdutoViewModel> Listagem(int id = 0)
        {
            string sql = "EXEC spListagem_Produtos;";
            var tabela = HelperDAO.ExecutaSelect(sql, null);
            List<ProdutoViewModel> lista = new List<ProdutoViewModel>();
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;
        }

        public List<ProdutoViewModel> ConsultaAvancadaProduto(string nome, int analises,
                                                        decimal? precoMenor,
                                                        decimal? precoMaior)
        {
            SqlParameter[] parametros = {
                new SqlParameter("nome", nome),
                new SqlParameter("analises", analises),
                new SqlParameter("precoMenor", precoMenor),
                new SqlParameter("precoMaior", precoMaior),
            };
            var tabela = HelperDAO.ExecutaProcSelect("spConsultaAvancadaProdutos", parametros);
            var lista = new List<ProdutoViewModel>();
            foreach (DataRow dr in tabela.Rows)
                lista.Add(MontaModel(dr));
            return lista;
        }


    }

}
