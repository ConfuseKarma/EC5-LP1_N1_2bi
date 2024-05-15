using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using N1_2Bi___LP1.DAO;
using N1_2Bi___LP1.Models;
using System.Data;

namespace N1_2Bi___LP1.Controllers
{

    public class UsuarioController : PadraoController<UsuarioViewModel>
    {
        protected override bool ExigeAutenticacao { get; set; } = false;
        public UsuarioController()
        {
            DAO = new UsuarioDAO();
            GeraProximoId = true;
        }

        public IActionResult ListarUsuario()
        {
            List<UsuarioViewModel> usuario = new List<UsuarioViewModel>();
            return View("Index", usuario);
        }

        protected override void ValidaDados(UsuarioViewModel usuario, string operacao)
        {
            base.ValidaDados(usuario, operacao); // Chama o método da classe base para validar dados básicos

            // Verifica se o nome foi preenchido
            if (string.IsNullOrEmpty(usuario.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome.");

            // Verifica se o CPF foi preenchido e se é válido
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


        public IActionResult ObtemDadosConsultaAvancada(string nomeUsuario,
                                                string cpf,
                                                int tipo)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                if (string.IsNullOrEmpty(nomeUsuario))
                    nomeUsuario = "";
                if (string.IsNullOrEmpty(cpf))
                    cpf = "";

                List<UsuarioViewModel> lista = dao.ConsultaAvancadaUsuario(nomeUsuario, cpf, tipo);
                return PartialView("_ListUsuario", lista);
            }
            catch (Exception erro)
            {
                return Json(new { erro = true, msg = erro.Message });
            }
        }


        /*
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
        */

    }



}
