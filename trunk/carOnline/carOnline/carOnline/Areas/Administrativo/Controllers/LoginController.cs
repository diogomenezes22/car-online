using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using carOnline.Models;
namespace carOnline.Areas.Administrativo.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Administrativo/Login/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ValidarLogin(FormCollection formulario)
        {
            tblAdministrador dadosAdministrador = new tblAdministrador();
            using (CarOnlineEntities DB = new CarOnlineEntities())
            {
                string emailAdministrador = formulario["emailAdministrador"];
                string senhaAdministrador = formulario["senhaAdministrador"];
                var query = DB.tblAdministrador.FirstOrDefault(a => a.email.Equals(emailAdministrador) && a.senha.Equals(senhaAdministrador));
                if(query!=null)
                {
                    dadosAdministrador = (tblAdministrador)query;
                    Session["ADMINISTRADOR"] = dadosAdministrador;
                    return RedirectToAction("Index", "Principal", new { area = "administrativo" });
                }
                else
                    return RedirectToAction("Index", "Login", new { area = "administrativo" });
            }
        }
        public ActionResult Deslogar()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
    }
}
