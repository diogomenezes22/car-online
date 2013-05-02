using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using carOnline.Models;

namespace carOnline.Controllers
{
    public class ContatoController : Controller
    {
        //
        // GET: /Contato/

        public ActionResult Index()
        {
            return View();
        }
        

        [HttpPost]
        public ActionResult EnviarEmail(string Email, string Assunto, string Mensagem)
        {
            try
            {
                Ferramenta.EnviarEmail(Email, "wellington_fernands@yahoo.com.br", Assunto, Mensagem);
                ViewBag.Msg = "Contato enviado com sucesso! Em breve responderemos.";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
}
