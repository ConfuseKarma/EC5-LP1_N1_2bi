using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using N1_2Bi___LP1.DAO;
using N1_2Bi___LP1.Enums;
using N1_2Bi___LP1.Models;

namespace N1_2Bi___LP1.Controllers
{

    public class UsuarioController : PadraoController<UsuarioViewModel>
    {
        public IActionResult Index()
        {
            UsuarioDAO dao = new UsuarioDAO();
            List<UsuarioViewModel> lista = dao.Listagem();
            return View(lista);
        }

        public IActionResult Create()
        {
            try
            {
                ViewBag.Operacao = "I";
                UsuarioViewModel usuario = new UsuarioViewModel();
                UsuarioDAO dao = new UsuarioDAO();
                usuario.Id = dao.ProximoId();
                PreparaListaEstadosParaCombo();
                return View("Form", usuario);
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
                UsuarioDAO dao = new UsuarioDAO();
                UsuarioViewModel usuario = dao.Consulta(id);
                PreparaListaEstadosParaCombo();
                if (usuario == null)
                    return RedirectToAction("index");
                else
                    return View("Form", usuario);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Salvar(UsuarioViewModel usuario, string Operacao)
        {
            try
            {
                ValidaDados(usuario, Operacao);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    PreparaListaEstadosParaCombo();
                    return View("Form", usuario);
                }
                else
                {
                    UsuarioDAO dao = new UsuarioDAO();
                    if (Operacao == "I")
                        dao.Inserir(usuario);
                    else
                        dao.Alterar(usuario);
                    return RedirectToAction("index");
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        private void ValidaDados(UsuarioViewModel usuario, string operacao)
        {
            ModelState.Clear(); // Limpa os erros criados automaticamente pelo ASP.NET

            UsuarioDAO dao = new UsuarioDAO();

            // Verifica se o código já está em uso
            if (operacao == "I" && dao.Consulta(usuario.Id) != null)
                ModelState.AddModelError("Id", "Código já está em uso.");

            // Verifica se o usuario existe para operações de alteração
            if (operacao == "A" && dao.Consulta(usuario.Id) == null)
                ModelState.AddModelError("Id", "Usuario não existe.");

            // Verifica se o ID é válido
            if (usuario.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");

            // Verifica se o nome foi preenchido
            if (string.IsNullOrEmpty(usuario.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome.");

            // Verifica se o CPF foi preenchido
            if (string.IsNullOrEmpty(usuario.Cpf))
                ModelState.AddModelError("Cpf", "Preencha o CPF.");
            else if (!ValidaCPF.IsCpf(usuario.Cpf)) // Verifica se o CPF é válido utilizando a classe ValidaCPF
                ModelState.AddModelError("Cpf", "CPF inválido.");

            // Verifica se o telefone foi preenchido
            if (string.IsNullOrEmpty(usuario.Telefone))
                ModelState.AddModelError("Telefone", "Preencha o telefone.");

            // Verifica se o email foi preenchido
            if (string.IsNullOrEmpty(usuario.Email))
                ModelState.AddModelError("Email", "Preencha o email.");

            // Verifica se a senha foi preenchida
            if (string.IsNullOrEmpty(usuario.Senha))
                ModelState.AddModelError("Senha", "Preencha a Senha.");

            // Verifica se o endereço foi preenchido
            if (string.IsNullOrEmpty(usuario.Endereco))
                ModelState.AddModelError("Endereco", "Preencha o endereço.");

            // Verifica se a cidade foi preenchida
            if (string.IsNullOrEmpty(usuario.Cidade))
                ModelState.AddModelError("Cidade", "Preencha a cidade.");

            // Verifica se o estado foi preenchido
            if (string.IsNullOrEmpty(usuario.Estado))
                ModelState.AddModelError("Estado", "Preencha o estado.");

            // Verifica se o CEP foi preenchido
            if (string.IsNullOrEmpty(usuario.Cep))
                ModelState.AddModelError("Cep", "Preencha o CEP.");

            // Verifica se o número da casa foi preenchido
            if (string.IsNullOrEmpty(usuario.Numero))
                ModelState.AddModelError("Numero", "Preencha o número da casa.");
        }

        public static class ValidaCPF
        {
            public static bool IsCpf(string cpf)
            {
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                    return false;
                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cpf.EndsWith(digito);
            }
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
