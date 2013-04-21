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
            tblFuncionario dadosFuncionario = new tblFuncionario();

            if (Ferramenta.ValidarEmail(formulario["emailAdministrador"]))
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    string emailAdministrador = formulario["emailAdministrador"];
                    string senhaAdministrador = formulario["senhaAdministrador"];
                    var query = DB.tblAdministrador.FirstOrDefault(a => a.email.Equals(emailAdministrador) && a.senha.Equals(senhaAdministrador));
                    if (query != null)
                    {
                        dadosAdministrador = (tblAdministrador)query;
                        Session["ADMINISTRADOR"] = dadosAdministrador;
                        return RedirectToAction("Index", "Principal", new { area = "administrativo" });
                    }
                    else
                    {
                        var query2 = DB.tblFuncionario.FirstOrDefault(a => a.email.Equals(emailAdministrador) && a.senha.Equals(senhaAdministrador));
                        if (query2 != null)
                        {
                            dadosFuncionario = (tblFuncionario)query2;
                            Session["FUNCIONARIO"] = dadosFuncionario;
                            return RedirectToAction("Index", "Principal", new { area = "administrativo" });
                        }
                        else
                        {
                            TempData["mensagemRetorno"] = "Email ou senha inválidos!";
                            return RedirectToAction("Index", "Login", new { area = "administrativo" });
                        }
                    }
                }
            }
            else
            {
                TempData["mensagemRetorno"] = "O formato do email é inválido!";
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
