using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using N1_2Bi___LP1.DAO;
using N1_2Bi___LP1.Models;
using System.Data.SqlTypes;
using System.Reflection;

namespace N1_2Bi___LP1.Controllers
{

    public class PedidoController : PadraoController<PedidoViewModel>
    {

        protected override bool ExigeAutenticacao { get; set; } = true;
        protected override bool ExigeAdmin { get; set; } = false;

        public PedidoController()
        {
            DAO = new PedidoDAO();
            GeraProximoId = true;
        }


    }




}
