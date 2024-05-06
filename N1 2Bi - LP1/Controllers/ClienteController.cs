using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using N1_2Bi___LP1.DAO;
using N1_2Bi___LP1.Enums;
using N1_2Bi___LP1.Models;

namespace N1_2Bi___LP1.Controllers
{

    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            ClienteDAO dao = new ClienteDAO();
            List<ClienteViewModel> lista = dao.Listagem();
            return View(lista);
        }

        public IActionResult Create()
        {
            try
            {
                ViewBag.Operacao = "I";
                ClienteViewModel cliente = new ClienteViewModel();
                ClienteDAO dao = new ClienteDAO();
                cliente.Id = dao.ProximoId();
                PreparaListaEstadosParaCombo();
                return View("Form", cliente);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                ClienteDAO dao = new ClienteDAO();
                ClienteViewModel cliente = dao.Consulta(id);
                PreparaListaEstadosParaCombo();
                if (cliente == null)
                    return RedirectToAction("index");
                else
                    return View("Form", cliente);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Salvar(ClienteViewModel cliente, string Operacao)
        {
            try
            {
                ValidaDados(cliente, Operacao);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    PreparaListaEstadosParaCombo();
                    return View("Form", cliente);
                }
                else
                {
                    ClienteDAO dao = new ClienteDAO();
                    if (Operacao == "I")
                        dao.Inserir(cliente);
                    else
                        dao.Alterar(cliente);
                    return RedirectToAction("index");
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        private void ValidaDados(ClienteViewModel cliente, string operacao)
        {
            ModelState.Clear(); // Limpa os erros criados automaticamente pelo ASP.NET

            ClienteDAO dao = new ClienteDAO();

            // Verifica se o código já está em uso
            if (operacao == "I" && dao.Consulta(cliente.Id) != null)
                ModelState.AddModelError("Id", "Código já está em uso.");

            // Verifica se o cliente existe para operações de alteração
            if (operacao == "A" && dao.Consulta(cliente.Id) == null)
                ModelState.AddModelError("Id", "Cliente não existe.");

            // Verifica se o ID é válido
            if (cliente.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");

            // Verifica se o nome foi preenchido
            if (string.IsNullOrEmpty(cliente.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome.");

            // Verifica se o CPF foi preenchido
            if (string.IsNullOrEmpty(cliente.Cpf))
                ModelState.AddModelError("Cpf", "Preencha o CPF.");

            // Verifica se o telefone foi preenchido
            if (string.IsNullOrEmpty(cliente.Telefone))
                ModelState.AddModelError("Telefone", "Preencha o telefone.");

            // Verifica se o email foi preenchido
            if (string.IsNullOrEmpty(cliente.Email))
                ModelState.AddModelError("Email", "Preencha o email.");

            // Verifica se o endereço foi preenchido
            if (string.IsNullOrEmpty(cliente.Endereco))
                ModelState.AddModelError("Endereco", "Preencha o endereço.");

            // Verifica se a cidade foi preenchida
            if (string.IsNullOrEmpty(cliente.Cidade))
                ModelState.AddModelError("Cidade", "Preencha a cidade.");

            // Verifica se o estado foi preenchido
            if (string.IsNullOrEmpty(cliente.Estado))
                ModelState.AddModelError("Estado", "Preencha o estado.");

            // Verifica se o CEP foi preenchido
            if (string.IsNullOrEmpty(cliente.Cep))
                ModelState.AddModelError("Cep", "Preencha o CEP.");
        }


        private void PreparaListaEstadosParaCombo()
        {
            var estados = Enum.GetValues(typeof(EstadosEnum)).Cast<EstadosEnum>();

            List<SelectListItem> listaEstados = new List<SelectListItem>();
            listaEstados.Add(new SelectListItem("Selecione um estado...", "0"));

            foreach (var estado in estados)
            {
                SelectListItem item = new SelectListItem(estado.ToString(), estado.ToString());
                listaEstados.Add(item);
            }

            ViewBag.Estados = listaEstados;
        }


    }



}
