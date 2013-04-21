using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using carOnline.Models;
using System.Transactions;
namespace carOnline.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/

        public ActionResult Cadastro()
        {
            if (Session["Usuario"] != null)
                return RedirectToAction("Area", "Usuario");
            else
                return View();
        }


        public ActionResult Cadastrar(tblUsuario usuario)
        {
            if (Ferramenta.ValidarEmail(usuario.email))
            {
                if (!Ferramenta.EmailCadastrado(usuario.email, "USUARIO"))
                {
                    using (CarOnlineEntities DB = new CarOnlineEntities())
                    {
                        usuario.dataCadastro = DateTime.Now;
                        DB.tblUsuario.AddObject(usuario);
                        DB.SaveChanges();
                    }
                    TempData["mensagemRetorno"] = "Usuário " + usuario.nome + " cadastrado(a) com sucesso!";
                    if (Session["USUARIO"] == null && (Session["ADMINISTRADOR"] != null || Session["FUNCIONARIO"] != null))
                        return RedirectToAction("Consulta");
                    else
                        return RedirectToAction("Cadastro", "Usuario");
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


        public ActionResult Alteracao(int id)
        {
            tblUsuario usuario = new tblUsuario();
            using (CarOnlineEntities DB = new CarOnlineEntities())
            {
                usuario = DB.tblUsuario.FirstOrDefault(u => u.idUsuario.Equals(id));
            }
            return View(usuario);
        }


        public ActionResult Alterar(tblUsuario usuario, FormCollection formulario)
        {
            int idUsuario = Convert.ToInt32(formulario["idUsuario"]);
            if (Ferramenta.ValidarEmail(usuario.email))
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    var query = DB.tblUsuario.Where(a => a.email.Equals(usuario.email)).First().idUsuario;
                    if (query == usuario.idUsuario)
                    {
                        usuario = DB.tblUsuario.FirstOrDefault(a => a.idUsuario.Equals(idUsuario));
                        usuario.nome = formulario["nome"];
                        usuario.senha = formulario["senha"];
                        usuario.email = formulario["email"];
                        usuario.telefone = formulario["telefone"];
                        usuario.celular = formulario["celular"];
                        DB.SaveChanges();
                        TempData["mensagemRetorno"] = "Usuário " + usuario.nome + " alterado(a) com sucesso!";
                        if (Session["USUARIO"] == null && (Session["ADMINISTRADOR"] != null || Session["FUNCIONARIO"] != null))
                            return RedirectToAction("Consulta");
                        else
                            return RedirectToAction("Area", "Usuario");
                    }
                    else
                    {
                        TempData["mensagemRetorno"] = "O email informado já está cadastrado!";
                        return RedirectToAction("Alteracao", new { id = usuario.idUsuario });
                    }
                }
            }
            else
            {
                TempData["mensagemRetorno"] = "O formato do email é inválido!";
                return RedirectToAction("Alteracao", new { id = idUsuario });
            }
        }


        public ActionResult Consulta()
        {
            List<tblUsuario> usuarios = new List<tblUsuario>();
            using (CarOnlineEntities DB = new CarOnlineEntities())
            {
                usuarios = DB.tblUsuario.ToList<tblUsuario>();
            }
            return View(usuarios);
        }


        public ActionResult DeletarUsuario(int id)
        {
            tblUsuario usuario = new tblUsuario();
            using (CarOnlineEntities DB = new CarOnlineEntities())
            {
                usuario = DB.tblUsuario.FirstOrDefault(a => a.idUsuario.Equals(id));
                DB.DeleteObject(usuario);
                DB.SaveChanges();
                //TempData["mensagemRetorno"] = "Usuário " + usuario.nome + " deletado(a) com sucesso!";
                return Json(null);
            }
        }

        public ActionResult Area()
        {
            if (Session["USUARIO"] != null)
                return View();
            else
                return RedirectToAction("Index","Home");
        }


        //MÉTODOS DE LOGIN
        public ActionResult ValidarLogin(FormCollection formulario)
        {
            tblUsuario dadosUsuario = new tblUsuario();

            if (Ferramenta.ValidarEmail(formulario["emailUsuario"]))
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    string emailUsuario = formulario["emailUsuario"];
                    string senhaUsuario = formulario["senhaUsuario"];
                    var query = DB.tblUsuario.FirstOrDefault(a => a.email.Equals(emailUsuario) && a.senha.Equals(senhaUsuario));
                    if (query != null)
                    {
                        dadosUsuario = (tblUsuario)query;
                        Session["USUARIO"] = dadosUsuario;
                        return RedirectToAction("Area", "Usuario");
                    }
                    else
                    {
                        TempData["mensagemRetornoLogin"] = "Email ou senha inválidos!";
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                TempData["mensagemRetornoLogin"] = "O formato do email é inválido!";
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult Deslogar()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}
