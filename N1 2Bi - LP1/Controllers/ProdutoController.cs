using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using N1_2Bi___LP1.DAO;
using N1_2Bi___LP1.Models;
using System.Data.SqlTypes;
using System.Reflection;

namespace N1_2Bi___LP1.Controllers
{

    public class ProdutoController : PadraoController<ProdutoViewModel>
    {

        protected override bool ExigeAutenticacao { get; set; } = true;

        public ProdutoController()
        {
            DAO = new ProdutoDAO();
            GeraProximoId = true;
        }

        public byte[] ConvertImageToByte(IFormFile file)
        {
            if (file != null)
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            else
                return null;
        }

        protected override void ValidaDados(ProdutoViewModel produto, string operacao)
        {
            base.ValidaDados(produto, operacao); // Chama o método da classe base para validar dados básicos

            // Verifica se o nome foi preenchido
            if (string.IsNullOrEmpty(produto.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome.");

            // Verifica se a descrição foi preenchida
            if (string.IsNullOrEmpty(produto.Descricao))
                ModelState.AddModelError("Descricao", "Preencha a descrição.");

            // Verifica se o preço é maior que zero
            if (produto.Preco <= 0)
                ModelState.AddModelError("Preco", "O preço deve ser maior que zero.");

            if (produto.Imagem == null && operacao == "I")
                ModelState.AddModelError("Imagem", "Escolha uma imagem.");
            if (produto.Imagem != null && produto.Imagem.Length / 1024 / 1024 >= 2)
                ModelState.AddModelError("Imagem", "Imagem limitada a 2 mb.");

            if (ModelState.IsValid)
            {
                //na alteração, se não foi informada a imagem, iremos manter a que já estava salva.
                if (operacao == "A" && produto.Imagem == null)
                {
                    ProdutoViewModel func = DAO.Consulta(produto.Id);
                    produto.ImagemEmByte = func.ImagemEmByte;
                }
                else
                {
                    produto.ImagemEmByte = ConvertImageToByte(produto.Imagem);
                }
            }
        }

        public IActionResult ExibeConsultaAvancada()
        {
            try
            {
                List<ProdutoViewModel> produto = new List<ProdutoViewModel>();
                return View("ConsultaAvancada", produto);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }

        }

        public IActionResult ObtemDadosConsultaAvancada(string nome, int analises,
                                                        decimal? precoMenor,
                                                        decimal? precoMaior)
        {
            try
            {
                ProdutoDAO dao = new ProdutoDAO(); 
                if (string.IsNullOrEmpty(nome))
                    nome = "";
                if (precoMenor == null)
                    precoMenor = 0;
                if (precoMaior == null)
                    precoMaior = 100000;


                List<ProdutoViewModel> lista = dao.ConsultaAvancadaProduto(nome, analises, precoMenor, precoMaior); 
                return PartialView("pvGridProdutos", lista); 
            }
            catch (Exception erro)
            {
                return Json(new { erro = true, msg = erro.Message });
            }
        }



    }




}
