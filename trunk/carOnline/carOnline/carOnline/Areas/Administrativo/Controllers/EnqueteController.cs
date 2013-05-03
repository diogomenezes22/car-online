using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using carOnline.Models;
using System.Transactions;
namespace carOnline.Areas.Administrativo.Controllers
{
    public class EnqueteController : Controller
    {
        //
        // GET: /Administrativo/Enquete/

        public ActionResult Cadastro()
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        [HttpPost]
        public ActionResult Cadastrar()
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                using (TransactionScope transacao = new TransactionScope())
                {
                    try
                    {
                        using (CarOnlineEntities DB = new CarOnlineEntities())
                        {
                            DB.SaveChanges();
                            transacao.Complete();
                            return View("Consulta");
                        }
                    }
                    catch (Exception ex)
                    {
                        transacao.Dispose();
                        return View("Cadastro");
                    }
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        public ActionResult Alteracao()
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        [HttpPost]
        public ActionResult Alterar()
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                using (TransactionScope transacao = new TransactionScope())
                {
                    try
                    {
                        using (CarOnlineEntities DB = new CarOnlineEntities())
                        {
                            DB.SaveChanges();
                            transacao.Complete();
                            return View("Consulta");
                        }
                    }
                    catch (Exception ex)
                    {
                        transacao.Dispose();
                        return View("Cadastro");
                    }
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        public ActionResult Consulta()
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        [HttpPost]
        public ActionResult DeletarEnquete(int id)
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                using (TransactionScope transacao = new TransactionScope())
                {
                    try
                    {
                        using (CarOnlineEntities DB = new CarOnlineEntities())
                        {
                            DB.SaveChanges();
                            transacao.Complete();
                            return View("Consulta");
                        }
                    }
                    catch (Exception ex)
                    {
                        transacao.Dispose();
                        return View("Cadastro");
                    }
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
    }
}
