using Microsoft.AspNetCore.Mvc;
using N1_2Bi___LP1.DAO;
using N1_2Bi___LP1.Models;

namespace N1_2Bi___LP1.Controllers
{
    public class ReviewsController : PadraoController<ReviewsViewModel>
    {
        public ReviewsController()
        {
            DAO = new ReviewsDAO();
            GeraProximoId = true;
            RedirectController = "Produto";
        }

        public override IActionResult Create(int? produtoId)
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("UserId");

                if (userId.HasValue)
                {
                    ViewBag.UserId = userId.Value;
                }
                else
                {
                    ViewBag.UserId = null;
                }

                ViewBag.Operacao = "I";
                ReviewsViewModel review = new ReviewsViewModel();
                if(produtoId != null)
                    PreencheDadosParaView("I", review, produtoId);
                else
                    PreencheDadosParaView("I", review);
                return View(NomeViewForm, review);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        //sobrecarga para o método PreencheDadosParaView

        protected override void PreencheDadosParaView(string Operacao, ReviewsViewModel model)
        {
            if (GeraProximoId && Operacao == "I")
                model.Id = DAO.ProximoId();
        }

         
        protected  void PreencheDadosParaView(string Operacao, ReviewsViewModel model, int? produtoId)
        {
            if (GeraProximoId && Operacao == "I")
                model.Id = DAO.ProximoId();
            if (produtoId != null)
                model.ProdutoId = (int)produtoId;
        }

        protected override void ValidaDados(ReviewsViewModel reviews, string operacao)
        {
            base.ValidaDados(reviews, operacao); // Chama o método da classe base para validar dados básicos

            // Verifica se a descrição foi preenchida
            if (string.IsNullOrEmpty(reviews.Descricao))
                ModelState.AddModelError("Descricao", "Preencha a descrição.");

            if (reviews.Pontuacao <= 0)
                ModelState.AddModelError("Pontuacao", "Selecione uma Pontuação.");
        }
    }
}
