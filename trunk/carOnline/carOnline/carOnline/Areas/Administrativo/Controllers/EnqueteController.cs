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
        public ActionResult Cadastrar(string nomeEnquete,string tipoOpcao,DateTime dataVigencia,string[] opcoesEnquete)
        {
            int idEnquete = 0;
            if (Session["ADMINISTRADOR"] != null)
            {
                tblAdministrador administrador = (tblAdministrador)Session["ADMINISTRADOR"];
                tblEnquete novaEnquete = new tblEnquete();
                using (TransactionScope transacao = new TransactionScope())
                {
                    try
                    {
                        using (CarOnlineEntities DB = new CarOnlineEntities())
                        {
                            //Salva a enquete
                            novaEnquete.dataCadastro = DateTime.Now;
                            novaEnquete.dataVigencia = dataVigencia;
                            novaEnquete.descricao = nomeEnquete;
                            novaEnquete.idAdministrador = administrador.idAdministrador;
                            DB.tblEnquete.AddObject(novaEnquete);
                            DB.SaveChanges();

                            idEnquete = novaEnquete.idEnquete;
                            
                            //Salva as opções da enquete
                            for (int i = 0; i < opcoesEnquete.Length; i++)
                            {
                                tblOpcaoEnquete novaOpcaoEnquete  = new tblOpcaoEnquete();
                                novaOpcaoEnquete.descricao        = opcoesEnquete[i].ToString();
                                novaOpcaoEnquete.idEnquete        = idEnquete;
                                novaOpcaoEnquete.quantidadeVotos  = 0;
                                novaOpcaoEnquete.tipo             = tipoOpcao;
                                DB.tblOpcaoEnquete.AddObject(novaOpcaoEnquete);
                            }

                            DB.SaveChanges();
                            transacao.Complete();
                            return Json(null);
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
