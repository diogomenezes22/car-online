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
                if (Ferramenta.ValidarEmail(administrador.email))
                {
                    if (!Ferramenta.EmailCadastrado(administrador.email, "ADMINISTRADOR"))
                    {
                        using (CarOnlineEntities DB = new CarOnlineEntities())
                        {
                            administrador.dataCadastro = DateTime.Now;
                            administrador.idTipoPermissao = 1;
                            DB.tblAdministrador.AddObject(administrador);
                            DB.SaveChanges();
                        }
                        TempData["mensagemRetorno"] = "Administrador "+administrador.nome+" cadastrado(a) com sucesso!";
                        return RedirectToAction("Consulta");
                    }
                    else
                    {
                        TempData["mensagemRetorno"] = "O email informado já está cadastrado!";
                        return RedirectToAction("Cadastro");
                    }
                }
                else
                {
                    TempData["mensagemRetorno"] = "O formato do email é inválido!";
                    return RedirectToAction("Cadastro");
                }
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
            int idAdministrador = Convert.ToInt32(formulario["idAdministrador"]);
            if (Session["ADMINISTRADOR"] != null)
            {
                if (Ferramenta.ValidarEmail(administrador.email))
                {
                    using (CarOnlineEntities DB = new CarOnlineEntities())
                    {

                        administrador = DB.tblAdministrador.FirstOrDefault(a => a.idAdministrador.Equals(idAdministrador));
                        administrador.nome = formulario["nome"];
                        administrador.senha = formulario["senha"];
                        administrador.email = formulario["email"];
                        administrador.idTipoPermissao = 1;
                        DB.SaveChanges();
                    }
                    TempData["mensagemRetorno"] = "Administrador " + administrador.nome + " alterado(a) com sucesso!";
                    return RedirectToAction("Consulta");
                }
                else
                {
                    TempData["mensagemRetorno"] = "O formato do email é inválido!";
                    return RedirectToAction("Alteracao", new {id = idAdministrador });
                }
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
                    List<tblAdministrador> listaAdministradores = DB.tblAdministrador.OrderBy(a=> a.nome).ToList<tblAdministrador>();
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
                    //TempData["mensagemRetorno"] = "Administrador " + administrador.nome + " deletado(a) com sucesso!";
                    return Json(null);
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
    }
}
