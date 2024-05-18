using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using N1_2Bi___LP1.DAO;
using N1_2Bi___LP1.Models;

namespace N1_2Bi___LP1.Controllers
{

    public class ProdutoController : PadraoController<ProdutoViewModel>
    {

        protected override bool ExigeAutenticacao { get; set; } = false;

        public ProdutoController()
        {
            DAO = new ProdutoDAO();
            GeraProximoId = true;
        }
        public IActionResult ListarProduto()
        {
            List<ProdutoViewModel> produto = new List<ProdutoViewModel>();
            return View("Index", produto);
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

            // Imagem será obrigatória apenas na inclusão.
            // Na alteração iremos considerar a que já estava salva.
            if (operacao == "I" && string.IsNullOrEmpty(produto.ImagemEmBase64))
                ModelState.AddModelError("Imagem", "Escolha uma imagem.");

            // Verifica o tamanho da imagem, caso esteja presente
            if (!string.IsNullOrEmpty(produto.ImagemEmBase64) && Convert.FromBase64String(produto.ImagemEmBase64).Length / 1024 / 1024 >= 2)
                ModelState.AddModelError("Imagem", "Imagem limitada a 2 MB.");
        }

        public IActionResult ObtemDadosConsultaAvancada(string nome)
        {
            try
            {
                ProdutoDAO dao = new ProdutoDAO(); 
                if (string.IsNullOrEmpty(nome))
                    nome = "";

                List<ProdutoViewModel> lista = dao.ConsultaAvancadaProduto(nome); 
                return PartialView("_ListProd", lista); 
            }
            catch (Exception erro)
            {
                return Json(new { erro = true, msg = erro.Message });
            }
        }



    }




}
