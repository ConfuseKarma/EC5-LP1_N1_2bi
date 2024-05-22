using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using N1_2Bi___LP1.DAO;
using N1_2Bi___LP1.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace N1_2Bi___LP1.Controllers
{

    public class CarrinhoController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                ProdutoDAO dao = new ProdutoDAO();
                var listaDeProdutos = dao.Listagem();
                var carrinho = ObtemCarrinhoNaSession();
                ViewBag.TotalCarrinho = 0;
                foreach (var c in carrinho)
                {
                    ViewBag.TotalCarrinho += c.Quantidade;
                }
                return View(listaDeProdutos);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Detalhes(int idProduto)
        {
            try
            {
                List<ItemCarrinhoViewModel> carrinho = ObtemCarrinhoNaSession();
                ProdutoDAO prodDao = new ProdutoDAO();
                var modelProduto = prodDao.Consulta(idProduto);
                ItemCarrinhoViewModel carrinhoModel = carrinho.Find(c => c.ProdutoId == idProduto);
                if (carrinhoModel == null)
                {
                    carrinhoModel = new ItemCarrinhoViewModel();
                    carrinhoModel.ProdutoId = idProduto;
                    carrinhoModel.Nome = modelProduto.Nome;
                    carrinhoModel.Quantidade = 0;
                }
                // preenche a imagem
                carrinhoModel.Imagem = modelProduto.ImagemEmBase64;
                return View(carrinhoModel);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        private List<ItemCarrinhoViewModel> ObtemCarrinhoNaSession()
        {
            List<ItemCarrinhoViewModel> carrinho = new List<ItemCarrinhoViewModel>();
            string carrinhoJson = HttpContext.Session.GetString("carrinho");
            if (carrinhoJson != null)
            {
                carrinho = JsonConvert.DeserializeObject<List<ItemCarrinhoViewModel>>(carrinhoJson);
            }
            return carrinho;
        }

        public IActionResult AdicionarCarrinho(int ProdutoId, int Quantidade)
        {
            try
            {
                List<ItemCarrinhoViewModel> carrinho = ObtemCarrinhoNaSession();
                ItemCarrinhoViewModel carrinhoModel = carrinho.Find(c => c.ProdutoId == ProdutoId);
                if (carrinhoModel != null && Quantidade == 0)
                {
                    //tira do carrinho
                    carrinho.Remove(carrinhoModel);
                }
                else if (carrinhoModel == null && Quantidade > 0)
                {
                    //não havia no carrinho, vamos adicionar
                    ProdutoDAO cidDao = new ProdutoDAO();
                    var modelProduto = cidDao.Consulta(ProdutoId);
                    carrinhoModel = new ItemCarrinhoViewModel();
                    carrinhoModel.ProdutoId = ProdutoId;
                    carrinhoModel.Nome = modelProduto.Nome;
                    carrinho.Add(carrinhoModel);
                }
                if (carrinhoModel != null)
                {
                    carrinhoModel.Quantidade = Quantidade;
                }
                string carrinhoJson = JsonConvert.SerializeObject(carrinho);
                HttpContext.Session.SetString("carrinho", carrinhoJson);
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Visualizar()
        {
            try
            {
                ProdutoDAO dao = new ProdutoDAO();
                var carrinho = ObtemCarrinhoNaSession();
                foreach (var item in carrinho)
                {
                    var cid = dao.Consulta(item.ProdutoId);
                    item.Imagem = cid.ImagemEmBase64;
                }
                return View(carrinho);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!HelperControllers.VerificaUserLogado(HttpContext.Session))
            {
                context.Result = RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Logado = true;
                base.OnActionExecuting(context);
            }
        }

        //
        public IActionResult EfetuarPedido()
        {
            // Obtém o ID do usuário logado da sessão
            int usuarioId = HttpContext.Session.GetInt32("UserId") ?? 0;

            // Verifica se o usuarioId obtido é válido
            if (usuarioId == 0)
            {
                // Se o usuário não estiver logado, redireciona para a página de login ou exibe uma mensagem de erro
                return RedirectToAction("Index", "Login");
            }

            try
            {
                using (var transacao = new System.Transactions.TransactionScope())
                {
                    PedidoViewModel pedido = new PedidoViewModel();
                    pedido.UsuarioId = usuarioId; // Define o ID do usuário logado
                    pedido.Data = DateTime.Now;
                    PedidoDAO pedidoDAO = new PedidoDAO();
                    int idPedido = pedidoDAO.ProximoId();
                    pedido.Id = idPedido;
                    pedidoDAO.Insert(pedido);

                    PedidoItemDAO itemDAO = new PedidoItemDAO();
                    var carrinho = ObtemCarrinhoNaSession();
                    foreach (var elemento in carrinho)
                    {
                        PedidoItemViewModel item = new PedidoItemViewModel();
                        item.Id = itemDAO.ProximoId();
                        item.PedidoId = idPedido;
                        item.ProdutoId = elemento.ProdutoId; // Define o ID do produto do carrinho
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

    //

}

