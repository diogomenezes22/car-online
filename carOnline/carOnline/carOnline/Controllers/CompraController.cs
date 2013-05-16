using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using carOnline.Models;

namespace carOnline.Controllers
{
    public class CompraController : BaseController
    {
        //
        // GET: /Compra/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detalhes()
        {
            ViewBag.IdCarro = Request.QueryString[Ferramenta.QueryStrings.IdCarro] != null ? Request.QueryString[Ferramenta.QueryStrings.IdCarro].ToString() : null;

            if ((ViewBag.IdCarro == null) || (!Ferramenta.IsInt32(ViewBag.IdCarro)))
                Menssagem(ViewBag,"Por favor, escolha um carro");

            return View();
        }

    }
}
