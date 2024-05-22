using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using N1_2Bi___LP1.DAO;
using N1_2Bi___LP1.Models;
using System.Data.SqlTypes;
using System.Reflection;

namespace N1_2Bi___LP1.Controllers
{

    public class PedidoItemController : PadraoController<PedidoItemViewModel>
    {

        protected override bool ExigeAutenticacao { get; set; } = true;
        protected override bool ExigeAdmin { get; set; } = false;

        public PedidoItemController()
        {
            DAO = new PedidoItemDAO();
            GeraProximoId = true;
        }


    }




}
