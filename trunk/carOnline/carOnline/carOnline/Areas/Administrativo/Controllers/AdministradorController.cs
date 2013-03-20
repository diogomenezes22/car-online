using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using carOnline.Models;
using System.Transactions;
namespace carOnline.Areas.Administrativo.Controllers
{
    public class AdministradorController : Controller
    {
        //
        // GET: /Administrativo/Administrador/

        public ActionResult Cadastro()
        {
            if (Session["ADMINISTRADOR"] != null)
                return View();
            else
                return RedirectToAction("Index", "Login", new { area="administrativo" });
        }
        [HttpPost]
        public ActionResult Cadastrar(tblAdministrador administrador)
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    administrador.dataCadastro = DateTime.Now;
                    DB.tblAdministrador.AddObject(administrador);
                    DB.SaveChanges();
                }
                return RedirectToAction("Consulta");
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        public ActionResult Alteracao(int id)
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                tblAdministrador administrador = new tblAdministrador();
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    administrador = DB.tblAdministrador.FirstOrDefault(a => a.idAdministrador.Equals(id));
                    return View(administrador);
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        [HttpPost]
        public ActionResult Alterar(tblAdministrador administrador,FormCollection formulario)
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    int idAdministrador = Convert.ToInt32(formulario["idAdministrador"]);
                    administrador = DB.tblAdministrador.FirstOrDefault(a => a.idAdministrador.Equals(idAdministrador));
                    administrador.nome = formulario["nome"];
                    administrador.senha = formulario["senha"];
                    administrador.email = formulario["email"];
                    DB.SaveChanges();
                }
                return RedirectToAction("Consulta");
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        public ActionResult Consulta()
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    IEnumerable<tblAdministrador> listaAdministradores = DB.tblAdministrador.ToList();
                    return View(listaAdministradores);
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        public ActionResult DeletarAdministrador(int id)
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                tblAdministrador administrador = new tblAdministrador();
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    administrador = DB.tblAdministrador.FirstOrDefault(a => a.idAdministrador.Equals(id));
                    DB.DeleteObject(administrador);
                    DB.SaveChanges();
                    return RedirectToAction("Consulta");
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
    }
}
