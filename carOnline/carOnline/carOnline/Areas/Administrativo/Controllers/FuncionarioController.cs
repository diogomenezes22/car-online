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
                    List<tblCargo> cargos = DB.tblCargo.Where(c=> c.ativo.Equals("S")).ToList();
                    List<tblPais> paises = DB.tblPais.ToList();
                    List<tblEstado> estados = DB.tblEstado.ToList();
                    List<tblCidade> cidades = DB.tblCidade.ToList();
                    List<tblTipoPermissao> tipoPermissao = DB.tblTipoPermissao.ToList();
                    ViewBag.Estados = new SelectList(estados, "idEstado", "nome");
                    ViewBag.Paises  = new SelectList(paises, "idPais", "nome");
                    ViewBag.Cidades = new SelectList(cidades,"idCidade","nome");
                    ViewBag.Cargos  = new SelectList(cargos, "idCargo", "descricao");
                    ViewBag.TipoPermissao = new SelectList(tipoPermissao, "idTipoPermissao", "descricao");
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
                int[] idEstado;
                int i = 0;
                int tamanho;
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    var query = DB.tblCidade.Where(c => c.idEstado.Equals(id));
                    nomeCidade = new string[query.Count()];
                    idCidade = new int[query.Count()];
                    idEstado = new int[query.Count()];
                    tamanho = query.Count();
                    foreach (var item in query)
                    {
                        nomeCidade[i] = item.nome;
                        idCidade[i] = item.idCidade;
                        idEstado[i] = item.idEstado;
                        i++;
                    }
                }
                var data = new { idCidade, nomeCidade, tamanho ,idEstado};
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
                if (Ferramenta.ValidarEmail(funcionario.email))
                {
                    if (!Ferramenta.EmailCadastrado(funcionario.email, "FUNCIONARIO"))
                    {
                        using (CarOnlineEntities DB = new CarOnlineEntities())
                        {
                            funcionario.idCidade = Convert.ToInt32(formulario["Cidades"]);
                            funcionario.idCargo = Convert.ToInt32(formulario["Cargos"]);
                            funcionario.idTipoPermissao = Convert.ToInt32(formulario["TipoPermissao"]);
                            funcionario.dataCadastro = DateTime.Now;
                            DB.tblFuncionario.AddObject(funcionario);
                            DB.SaveChanges();
                        }
                        TempData["mensagemRetorno"] = "Funcionário " + funcionario.nome + " cadastrado(a) com sucesso!";
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
                tblFuncionario funcionario = new tblFuncionario();
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    List<tblCargo> cargos = DB.tblCargo.Where(c => c.ativo.Equals("S")).ToList();
                    List<tblPais> paises = DB.tblPais.ToList();
                    List<tblEstado> estados = DB.tblEstado.ToList();
                    List<tblCidade> cidades = DB.tblCidade.ToList();
                    List<tblTipoPermissao> tipoPermissao = DB.tblTipoPermissao.ToList();
                    ViewBag.Paises = new SelectList(paises, "idPais", "nome");
                    funcionario = DB.tblFuncionario.FirstOrDefault(a => a.idFuncionario.Equals(id));
                    ViewBag.Estados = new SelectList(estados, "idEstado", "nome", funcionario.tblCidade.idEstado);
                    ViewBag.Cargos = new SelectList(cargos, "idCargo", "descricao", funcionario.idCargo);
                    ViewBag.Cidades = new SelectList(cidades, "idCidade", "nome",funcionario.idCidade);
                    ViewBag.TipoPermissao = new SelectList(tipoPermissao, "idTipoPermissao", "descricao",funcionario.idTipoPermissao);
                    return View(funcionario);
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
        [HttpPost]
        public ActionResult Alterar(tblFuncionario funcionario, FormCollection formulario)
        {
            int idFuncionario = Convert.ToInt32(formulario["idFuncionario"]);
            if (Session["ADMINISTRADOR"] != null)
            {
                if (Ferramenta.ValidarEmail(funcionario.email))
                {
                    using (CarOnlineEntities DB = new CarOnlineEntities())
                    {
                        funcionario = DB.tblFuncionario.FirstOrDefault(a => a.idFuncionario.Equals(idFuncionario));
                        funcionario.bairro = formulario["bairro"];
                        funcionario.endereco = formulario["endereco"];
                        funcionario.email = formulario["email"];
                        funcionario.senha = formulario["senha"];
                        funcionario.celular = formulario["celular"];
                        funcionario.telefone = formulario["telefone"];
                        funcionario.nome = formulario["nome"];
                        funcionario.idCargo = Convert.ToInt32(formulario["Cargos"]);
                        funcionario.idCidade = Convert.ToInt32(formulario["Cidades"]);
                        funcionario.idTipoPermissao = Convert.ToInt32(formulario["TipoPermissao"]);
                        DB.SaveChanges();
                    }
                    TempData["mensagemRetorno"] = "Funcionário " + funcionario.nome + " alterado(a) com sucesso!";
                    return RedirectToAction("Consulta");
                }
                else
                {
                    TempData["mensagemRetorno"] = "O formato do email é inválido!";
                    return RedirectToAction("Alteracao", new { id=idFuncionario });
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
                    List<tblFuncionario> listaFuncionarios = DB.tblFuncionario.OrderBy(a => a.nome).ToList<tblFuncionario>();
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
                    //TempData["mensagemRetorno"] = "Funcionario " + funcionario.nome + " deletado(a) com sucesso!";
                    return Json(null);
                }
            }
            else
                return RedirectToAction("Index", "Login", new { area = "administrativo" });
        }
    }
}
