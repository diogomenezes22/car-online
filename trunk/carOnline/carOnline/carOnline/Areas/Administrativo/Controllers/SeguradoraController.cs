using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using carOnline.Models;
namespace carOnline.Areas.Administrativo.Controllers
{
    public class SeguradoraController : Controller
    {
        //
        // GET: /Administrativo/Seguradora/

        public ActionResult Cadastro()
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        public ActionResult Cadastrar(tblSeguradora seguradora, FormCollection formulario)
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                if (Ferramenta.ValidarEmail(seguradora.email))
                {
                    if (!Ferramenta.EmailCadastrado(seguradora.email, "SEGURADORA"))
                    {
                        using (CarOnlineEntities DB = new CarOnlineEntities())
                        {
                            seguradora.dataCadastro = DateTime.Now;
                            DB.tblSeguradora.AddObject(seguradora);
                            DB.SaveChanges();
                        }
                        TempData["mensagemRetorno"] = "Seguradora " + seguradora.nome + " cadastrado(a) com sucesso!";
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
                tblSeguradora seguradora = new tblSeguradora();
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    seguradora = DB.tblSeguradora.FirstOrDefault(a => a.idSeguradora.Equals(id));
                    return View(seguradora);
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        public ActionResult Alterar(tblSeguradora seguradora,FormCollection formulario)
        {
            int idSeguradora = Convert.ToInt32(formulario["idSeguradora"]);
            if (Session["ADMINISTRADOR"] != null)
            {
                if (Ferramenta.ValidarEmail(seguradora.email))
                {
                    using (CarOnlineEntities DB = new CarOnlineEntities())
                    {
                        seguradora = DB.tblSeguradora.FirstOrDefault(a => a.idSeguradora.Equals(idSeguradora));
                        seguradora.email    = formulario["email"];
                        seguradora.celular  = formulario["celular"];
                        seguradora.telefone = formulario["telefone"];
                        seguradora.nome     = formulario["nome"];
                      
                        DB.SaveChanges();
                    }
                    TempData["mensagemRetorno"] = "Seguradora " + seguradora.nome + " alterado(a) com sucesso!";
                    return RedirectToAction("Consulta");
                }
                else
                {
                    TempData["mensagemRetorno"] = "O formato do email é inválido!";
                    return RedirectToAction("Alteracao", new { id = idSeguradora });
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
                    List<tblSeguradora> listaSeguradoras = DB.tblSeguradora.OrderBy(a => a.nome).ToList<tblSeguradora>();
                    return View(listaSeguradoras);
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        public ActionResult DeletarSeguradora(int id)
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                tblSeguradora seguradora = new tblSeguradora();
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    seguradora = DB.tblSeguradora.FirstOrDefault(a => a.idSeguradora.Equals(id));
                    DB.DeleteObject(seguradora);
                    DB.SaveChanges();
                    //TempData["mensagemRetorno"] = "seguradora " + seguradora.nome + " deletado(a) com sucesso!";
                    return Json(null);
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
    }
}
