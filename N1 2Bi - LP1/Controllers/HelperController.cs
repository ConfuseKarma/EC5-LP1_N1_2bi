using Microsoft.AspNetCore.Mvc;
using N1_2Bi___LP1.Models;
using System.Diagnostics;

namespace N1_2Bi___LP1.Controllers
{
    public class HelperControllers: Controller
    {
        public static Boolean VerificaUserLogado(ISession session)
        {
            string logado = session.GetString("Logado");
            if (logado == null)
                return false;
            else
                return true;
        }



        public static void LimparCarrinho(ISession session)
        {
            session.Remove("carrinho");
        }
    }

}
