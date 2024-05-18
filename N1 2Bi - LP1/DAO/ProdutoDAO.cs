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
            // Se houver necessidade de mais campos, você pode adicioná-los aqui

            return produto;
        }


        public override void Insert(ProdutoViewModel produto)
        {
            HelperDAO.ExecutaProc("spInsert_Produtos", CriaParametros(produto));
        }

        public override void Update(ProdutoViewModel produto)
        {
            HelperDAO.ExecutaProc("spUpdate_Produtos", CriaParametros(produto));
        }

        protected override void SetTabela()
        {
            Tabela = "Produtos";
        }

        public List<ProdutoViewModel> ConsultaAvancadaProduto(string nome)
        {
            SqlParameter[] parametros = {new SqlParameter("nome", nome)  };
            var tabela = HelperDAO.ExecutaProcSelect("spConsultaAvancadaProdutosPorNome", parametros);
            var lista = new List<ProdutoViewModel>();
            foreach (DataRow dr in tabela.Rows)
                lista.Add(MontaModel(dr));
            return lista;
        }

    }

}
