using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

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
            WebMail.SmtpServer = "smtp.live.com";
            WebMail.SmtpPort = 587;
            WebMail.EnableSsl = false;
            WebMail.UserName = "fw_vitor@hotmail.com";
            WebMail.From = Email;
            WebMail.Password = "";
            WebMail.Send("lecogrunge@gmail.com", Assunto, Mensagem);
            
            return View();
        }
    }
}
