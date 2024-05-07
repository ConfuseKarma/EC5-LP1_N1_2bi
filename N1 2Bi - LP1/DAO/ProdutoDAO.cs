﻿using Microsoft.AspNetCore.Mvc;
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
            SqlParameter[] parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("id", model.Id);
            parametros[1] = new SqlParameter("nome", model.Nome);
            parametros[2] = new SqlParameter("descricao", model.Descricao);
            parametros[3] = new SqlParameter("preco", model.Preco);

            return parametros;
        }

        protected override ProdutoViewModel MontaModel(DataRow registro)
        {
            ProdutoViewModel produto = new ProdutoViewModel();
            produto.Id = Convert.ToInt32(registro["id"]);
            produto.Nome = registro["nome"].ToString();
            produto.Descricao = registro["descricao"].ToString();
            produto.Preco = Convert.ToDecimal(registro["preco"]);
            return produto;
        }

        public void Inserir(ProdutoViewModel produto)
        {
            HelperDAO.ExecutaProc("spInsert_Produtos", CriaParametros(produto));
        }

        public void Alterar(ProdutoViewModel produto)
        {
            HelperDAO.ExecutaProc("spUpdate_Produtos", CriaParametros(produto));
        }

        public void Excluir(int id)
        {
            var p = new SqlParameter[]
            {
            new SqlParameter("id", id)
            };
            HelperDAO.ExecutaProc("spDelete_Produtos", p);
        }

        protected override void SetTabela()
        {
            Tabela = "Produtos";
        }
    }

}