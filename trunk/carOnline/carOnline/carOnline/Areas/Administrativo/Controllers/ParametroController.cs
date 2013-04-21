using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using carOnline.Models;
using System.Transactions;
namespace carOnline.Areas.Administrativo.Controllers
{
    public class ParametroController : Controller
    {
        //
        // GET: /Administrativo/Parametro/

        public ActionResult Cadastro()
        {
            using (CarOnlineEntities DB = new CarOnlineEntities())
            {
                List<tblTipoParametro> tipoParametro = DB.tblTipoParametro.ToList();
                ViewBag.TipoParametro = new SelectList(tipoParametro, "idTipoParametro", "descricao");
                return View();
            }
        }
        public ActionResult CadastrarParametroImagem(tblParametro parametro,FormCollection formulario)
        {
            using(TransactionScope transacao = new TransactionScope())
            {
                try
                {
                    using(CarOnlineEntities DB = new CarOnlineEntities())
                    {
                        parametro.idTipoParametro = Convert.ToInt32(formulario["TipoParametro"]);
                        DB.tblParametro.AddObject(parametro);
                        DB.SaveChanges();
                        TempData["mensagemRetorno"] = "Parâmetro cadastrado com sucesso!";
                        transacao.Complete();
                        return RedirectToAction("Consulta");
                    }
                }
                catch(Exception ex)
                {
                    transacao.Dispose();
                    TempData["mensagemRetorno"] = "Não foi possível cadastrar o parâmetro!";
                    return RedirectToAction("Cadastro");
                }
            }
        }
        public ActionResult Consulta()
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    List<ConsultarParametro_Result> listaParametro = DB.ConsultarParametro().ToList<ConsultarParametro_Result>();
                    return View(listaParametro);
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        public ActionResult Alteracao(int id)
        {
            using (CarOnlineEntities DB = new CarOnlineEntities())
            {
                List<tblTipoParametro> tipoParametro = DB.tblTipoParametro.ToList();
                ViewBag.TipoParametro = new SelectList(tipoParametro, "idTipoParametro", "descricao");

                tblParametro parametro = DB.tblParametro.FirstOrDefault(p => p.idParametro.Equals(id));
                return View(parametro);
            }
        }
        public ActionResult Alterar(tblParametro parametro, FormCollection formulario)
        {
            int idParametro = Convert.ToInt32(formulario["idParametro"]);
            if (Session["ADMINISTRADOR"] != null)
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    parametro                 = DB.tblParametro.FirstOrDefault(a => a.idParametro.Equals(idParametro));
                    parametro.idTipoParametro = Convert.ToInt32(formulario["TipoParametro"]);
                    parametro.descricao       = formulario["descricao"];
                    parametro.valor           = formulario["valor"];
                    DB.SaveChanges();
                }
                TempData["mensagemRetorno"] = "Parâmetro alterado com sucesso!";
                return RedirectToAction("Consulta");
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        public ActionResult Cargo()
        {
            using (CarOnlineEntities DB = new CarOnlineEntities())
            {
                List<tblCargo> cargos = DB.tblCargo.ToList<tblCargo>();
                return View(cargos);
            }
        }
        [HttpPost]
        public ActionResult AlterarParametroCargo(int idCargo, string ativo)
        {
            string mensagem = "";
            using(TransactionScope transacao = new TransactionScope())
            {
                tblCargo cargo = new tblCargo();
                try
                {
                    using (CarOnlineEntities DB = new CarOnlineEntities())
                    {
                        cargo       = DB.tblCargo.FirstOrDefault(c => c.idCargo.Equals(idCargo));
                        cargo.ativo = ativo;
                        DB.SaveChanges();
                        transacao.Complete();
                        return Json("");
                    }
                }
                catch
                {
                    transacao.Dispose();
                    mensagem = "Não foi possível salvar o parâmetro.Contate o administrador e tente mais tarde";
                    var data = new { mensagem };
                    return Json(data);
                }
            }
        }
    }
}
