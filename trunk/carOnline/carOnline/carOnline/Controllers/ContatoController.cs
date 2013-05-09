using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using carOnline.Models;

namespace carOnline.Controllers
{
    public class ContatoController : BaseController
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
                if (Ferramenta.ValidarEmail(Email))
                {
                    Ferramenta.EnviarEmail(Email, "geraldo.estudos.trabalho@gmail.com", Assunto, Mensagem);
                    TempData["mensagemContato"] = "Contato enviado com sucesso! Em breve responderemos.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["mensagemContato"] = "O formato do e-mail é inválido.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["mensagemContato"] = "Não foi possível enviar o email.Contate o administrador ou tente mais tarde!";
                return RedirectToAction("Index");
            }
        }
    }
}
