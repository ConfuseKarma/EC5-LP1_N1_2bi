using Microsoft.AspNetCore.Mvc;
using N1_2Bi___LP1.Models;

namespace N1_2Bi___LP1.Controllers
{
    public class ReviewsController : PadraoController<ReviewsViewModel>
    {
        public override IActionResult Create()
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
                PreencheDadosParaView("I", review);
                return View(NomeViewForm, review);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
    }
}
