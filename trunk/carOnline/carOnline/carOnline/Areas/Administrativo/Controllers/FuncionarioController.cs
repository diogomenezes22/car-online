using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using carOnline.Models;
using System.Transactions;
namespace carOnline.Areas.Administrativo.Controllers
{
    public class FuncionarioController : Controller
    {
        //
        // GET: /Administrativo/Funcionario/

        public ActionResult Cadastro()
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    List<tblCargo> cargos = DB.tblCargo.ToList();
                    List<tblPais> paises = DB.tblPais.ToList();
                    List<tblEstado> estados = DB.tblEstado.ToList();
                    ViewBag.Estados = new SelectList(estados, "idEstado", "nome");
                    ViewBag.Paises  = new SelectList(paises, "idPais", "nome");
                    ViewBag.Cargos  = new SelectList(cargos, "idCargo", "descricao");
                    return View();
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        [HttpPost]
        public ActionResult BuscarCidade(int id)
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                string[] nomeCidade;
                int[] idCidade;
                int i = 0;
                int tamanho;
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    var query = DB.tblCidade.Where(c => c.idEstado.Equals(id));
                    nomeCidade = new string[query.Count()];
                    idCidade = new int[query.Count()];
                    tamanho = query.Count();
                    foreach (var item in query)
                    {
                        nomeCidade[i] = item.nome;
                        idCidade[i] = item.idCidade;
                        i++;
                    }
                }
                var data = new { idCidade, nomeCidade, tamanho };
                return Json(data);
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        [HttpPost]
        public ActionResult Cadastrar(tblFuncionario funcionario,FormCollection formulario)
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    funcionario.idCidade = Convert.ToInt32(formulario["Cidades"]);
                    funcionario.idCargo = Convert.ToInt32(formulario["Cargos"]);
                    funcionario.dataCadastro = DateTime.Now;
                    DB.tblFuncionario.AddObject(funcionario);
                    DB.SaveChanges();
                }
                return RedirectToAction("Cadastro");
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        public ActionResult Alteracao(int id)
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                tblFuncionario funcionario = new tblFuncionario();
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    funcionario = DB.tblFuncionario.FirstOrDefault(a => a.idFuncionario.Equals(id));
                    return View(funcionario);
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        [HttpPost]
        public ActionResult Alterar(tblFuncionario funcionario, FormCollection formulario)
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    int idFuncionario = Convert.ToInt32(formulario["idFuncionario"]);
                    funcionario = DB.tblFuncionario.FirstOrDefault(a => a.idFuncionario.Equals(idFuncionario));
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
                    IEnumerable<tblFuncionario> listaFuncionarios = DB.tblFuncionario.ToList();
                    return View(listaFuncionarios);
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        public ActionResult DeletarFuncionario(int id)
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                tblFuncionario funcionario = new tblFuncionario();
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    funcionario = DB.tblFuncionario.FirstOrDefault(a => a.idFuncionario.Equals(id));
                    DB.DeleteObject(funcionario);
                    DB.SaveChanges();
                    return RedirectToAction("Consulta");
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
    }
}
