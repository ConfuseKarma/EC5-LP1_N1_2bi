using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using N1_2Bi___LP1.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace N1_2Bi___LP1.Controllers
{
    public class CarrinhoController : Controller
    {
        public IActionResult EfetuarPedido()
        {
            try
            {
                using (var transacao = new System.Transactions.TransactionScope())
                {
                    PedidoViewModel pedido = new PedidoViewModel();
                    pedido.Data = DateTime.Now;
                    PedidoDAO pedidoDAO = new PedidoDAO();
                    int idPedido = pedidoDAO.Insert(pedido);
                    PedidoItemDAO itemDAO = new PedidoItemDAO();
                    var carrinho = ObtemCarrinhoNaSession();
                    foreach (var elemento in carrinho)
                    {
                        PedidoItemViewModel item = new PedidoItemViewModel();
                        item.PedidoId = idPedido;
                        item.CidadeId = elemento.CidadeId;
                        item.Qtde = elemento.Quantidade;
                        itemDAO.Insert(item);
                    }
                    transacao.Complete();
                }
                HelperControllers.LimparCarrinho(HttpContext.Session);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }


    }

}
