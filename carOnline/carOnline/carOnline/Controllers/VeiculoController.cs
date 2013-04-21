using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using carOnline.Models;
using System.Web.Helpers;
using System.Transactions;
using System.IO;
using System.Text;
namespace carOnline.Controllers
{
    public class VeiculoController : Controller
    {
        //
        // GET: /Veiculo/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastro()
        {
            if (Session["USUARIO"] != null)
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    tblTipoVeiculo tipo = new tblTipoVeiculo();
                    tipo.descricao     = "---Selecione---";
                    tipo.idTipoVeiculo = 999;
                    List<tblTipoVeiculo> tipoVeiculo         = DB.tblTipoVeiculo.OrderBy(v=> v.descricao).ToList();
                    tipoVeiculo.Add(tipo);

                    List<tblTipoCombustivel> tipoCombustivel = DB.tblTipoCombustivel.OrderBy(v => v.descricao).ToList();
                    List<tblCor> cor = DB.tblCor.OrderBy(v => v.descricao).ToList();
                    List<int> ano = new List<int>();
                    ano.Add(2010);
                    ano.Add(2011);
                    ano.Add(2012);
                    ano.Add(2013);

                    ViewBag.TipoVeiculo     = new SelectList(tipoVeiculo, "idTipoVeiculo", "descricao",999);
                    ViewBag.TipoCombustivel = new SelectList(tipoCombustivel, "idTipoCombustivel", "descricao");
                    ViewBag.Ano = new SelectList(ano);
                    ViewBag.Cor = new SelectList(cor, "idCor", "descricao");

                    return View();
                }
            }
            else
                return RedirectToAction("Index", "Home");
        }


        public ActionResult Cadastrar(tblVeiculo veiculo, FormCollection formulario)
        {
            if (Session["USUARIO"] != null)
            {
                tblUsuario usuario = (tblUsuario)Session["USUARIO"];
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    veiculo.dataCadastro = DateTime.Now;
                    veiculo.idCor = Convert.ToInt32(formulario["Cor"]);
                    veiculo.idModeloVeiculo = Convert.ToInt32(formulario["modelos"]);
                    veiculo.idTipoCombustivel = Convert.ToInt32(formulario["TipoCombustivel"]);
                    veiculo.idUsuario = usuario.idUsuario;
                    veiculo.preco = Convert.ToSingle(formulario["preco"]);
                    veiculo.quilometragem = Convert.ToSingle(formulario["quilometragem"]);

                    DB.tblVeiculo.AddObject(veiculo);
                    DB.SaveChanges();
                    TempData["mensagemRetorno"] = "Veículo cadastrado(a) com sucesso!";
                    return RedirectToAction("Consulta");
                }
            }
            else
                return RedirectToAction("Index", "Home");
        }


        public ActionResult Alteracao(int id)
        {
            List<string> fotos = new List<string>();
            if (Session["USUARIO"] != null)
            {
                tblVeiculo veiculo = new tblVeiculo();
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    fotos          = DB.tblFotosVeiculo.Where(v => v.idVeiculo.Equals(id)).Select(v => v.urlFoto).ToList<string>();
                    @ViewBag.fotos = fotos;
                    veiculo = DB.tblVeiculo.FirstOrDefault(v => v.idVeiculo.Equals(id));

                    tblTipoVeiculo tipo = new tblTipoVeiculo();
                    tipo.descricao = "---Selecione---";
                    tipo.idTipoVeiculo = 999;
                    List<tblTipoVeiculo> tipoVeiculo = DB.tblTipoVeiculo.OrderBy(v => v.descricao).ToList();
                    //tipoVeiculo.Add(tipo);

                    List<tblTipoCombustivel> tipoCombustivel = DB.tblTipoCombustivel.OrderBy(v => v.descricao).ToList();
                    List<tblCor> cor = DB.tblCor.OrderBy(v => v.descricao).ToList();
                    List<int> ano = new List<int>();
                    ano.Add(2010);
                    ano.Add(2011);
                    ano.Add(2012);
                    ano.Add(2013);

                    List<tblMarcaVeiculo> marcas = DB.tblMarcaVeiculo.ToList();
                    List<tblModeloVeiculo> modelos = DB.tblModeloVeiculo.ToList();

                    ViewBag.marcas = new SelectList(marcas, "idMarcaVeiculo", "descricao", veiculo.tblModeloVeiculo.tblMarcaVeiculo.idMarcaVeiculo);
                    ViewBag.modelos = new SelectList(modelos, "idModeloVeiculo", "descricao", veiculo.tblModeloVeiculo.idModeloVeiculo);
                   
                    ViewBag.TipoVeiculo = new SelectList(tipoVeiculo, "idTipoVeiculo", "descricao", veiculo.tblModeloVeiculo.tblMarcaVeiculo.tblTipoVeiculo.idTipoVeiculo);
                    ViewBag.TipoCombustivel = new SelectList(tipoCombustivel, "idTipoCombustivel", "descricao",veiculo.idTipoCombustivel);
                    ViewBag.Ano = new SelectList(ano,veiculo.ano);
                    ViewBag.Cor = new SelectList(cor, "idCor", "descricao",veiculo.idCor);
                    return View(veiculo);
                }
            }
            else
                return RedirectToAction("Index", "Home");
        }


        public ActionResult Alterar(tblVeiculo veiculo, FormCollection formulario)
        {
            int idVeiculo = Convert.ToInt32(formulario["idVeiculo"]);
            if (Session["USUARIO"] != null)
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    veiculo = DB.tblVeiculo.FirstOrDefault(v => v.idVeiculo.Equals(idVeiculo));
                    veiculo.ano = Convert.ToInt32(formulario["Ano"]);
                    veiculo.descricao = formulario["descricao"];
                    veiculo.idCor = Convert.ToInt32(formulario["Cor"]);
                    veiculo.idModeloVeiculo = Convert.ToInt32(formulario["modelos"]);
                    veiculo.idTipoCombustivel = Convert.ToInt32(formulario["TipoCombustivel"]);
                    veiculo.motor = formulario["motor"];
                    veiculo.portas = Convert.ToInt32(formulario["portas"]);
                    veiculo.preco = Convert.ToSingle(formulario["preco"]);
                    veiculo.quilometragem = Convert.ToSingle(formulario["quilometragem"]);
                    DB.SaveChanges();
                    TempData["mensagemRetorno"] = "Veículo alterado(a) com sucesso!";
                }
                return RedirectToAction("Consulta");
            }
            else
                return RedirectToAction("Index", "Home");
        }


        public ActionResult Consulta()
        {
            if (Session["USUARIO"] != null)
            {
                tblUsuario usuario = (tblUsuario)Session["USUARIO"];
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    List<ConsultaVeiculo_Result> listaVeiculos = DB.ConsultaVeiculo(usuario.idUsuario).ToList<ConsultaVeiculo_Result>();
                    return View(listaVeiculos);
                }
            }
            else
                return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult BuscarMarca(int id)
        {
            if (Session["USUARIO"] != null)
            {
                string[] nomeMarca;
                int[] idMarca;
                int[] idTipoVeiculoMarca;
                int i = 0;
                int tamanho;
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    var query = DB.tblMarcaVeiculo.Where(c => c.idTipoVeiculo.Equals(id));
                    nomeMarca = new string[query.Count()];
                    idMarca = new int[query.Count()];
                    idTipoVeiculoMarca = new int[query.Count()];
                    tamanho = query.Count();
                    foreach (var item in query)
                    {
                        nomeMarca[i] = item.descricao;
                        idMarca[i] = item.idMarcaVeiculo;
                        idTipoVeiculoMarca[i] = item.idTipoVeiculo;
                        i++;
                    }
                }
                var data = new { idMarca,nomeMarca, tamanho,idTipoVeiculoMarca };
                return Json(data);
            }
            else
                return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult BuscarModelo(int id)
        {
            if (Session["USUARIO"] != null)
            {
                string[] nomeModelo;
                int[] idModelo;
                int[] idMarca;
                int i = 0;
                int tamanho;
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    var query = DB.tblModeloVeiculo.Where(c => c.idMarcaVeiculo.Equals(id));
                    nomeModelo = new string[query.Count()];
                    idModelo = new int[query.Count()];
                    idMarca = new int[query.Count()];
                    tamanho = query.Count();
                    foreach (var item in query)
                    {
                        nomeModelo[i] = item.descricao;
                        idModelo[i] = item.idModeloVeiculo;
                        idMarca[i] = item.idMarcaVeiculo;
                        i++;
                    }
                }
                var data = new { idModelo, nomeModelo, tamanho,idMarca };
                return Json(data);
            }
            else
                return RedirectToAction("Index", "Home");
        }


        public ActionResult DeletarVeiculo(int id)
        {
            if (Session["USUARIO"] != null)
            {
                tblVeiculo veiculo = new tblVeiculo();
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    veiculo = DB.tblVeiculo.FirstOrDefault(a => a.idVeiculo.Equals(id));
                    DB.DeleteObject(veiculo);
                    DB.SaveChanges();
                    return Json(null);
                }
            }
            else
                return RedirectToAction("Index", "Home");
        }

        /// <summary>
        ///     Função que remove o arquivo selecionado
        /// </summary>
        /// <param name="idEmpresa">int</param>
        /// <param name="idBriefing">int</param>
        /// <param name="idGrupo">int</param>
        /// <param name="idComponente">int</param>
        /// <param name="idItem">int</param>
        /// <param name="nomeArquivo">string</param>
        /// <returns>Retorna uma tabela html com os arquivos da tabela atualizados.</returns>
        [HttpPost]
        public ActionResult DeletarArquivo(int idVeiculo, string nomeArquivo)
        {
            string tabelaArquivos = "";
            string mensagem = "";
            //Busca o caminho físico do arquivo com o nome do arquivo
            string caminhoFisico  = RetornarCaminhoArquivo(idVeiculo, "F") + nomeArquivo;
            string caminhoVirtual = RetornarCaminhoArquivo(idVeiculo, "V") + nomeArquivo;
            //Busca o arquivo
            FileInfo arquivo = new FileInfo(caminhoFisico);

            using (TransactionScope transacao = new TransactionScope())
            {
                try
                {
                    //Deleta o arquivo
                    arquivo.Delete();
                    using (CarOnlineEntities DB = new CarOnlineEntities())
                    {
                        int idFoto = DB.tblFotosVeiculo.FirstOrDefault(r => r.urlFoto.Equals(caminhoVirtual)).idFotoVeiculo;
                        tblFotosVeiculo foto = DB.tblFotosVeiculo.FirstOrDefault(r => r.idFotoVeiculo.Equals(idFoto));
                        DB.tblFotosVeiculo.DeleteObject(foto);
                        DB.SaveChanges();
                        transacao.Complete();
                        mensagem = "Arquivo (" + nomeArquivo.ToUpper() + ") deletado com sucesso!";
                        //Busca o caminho físico sem o nome do arquivo
                        caminhoFisico = RetornarCaminhoArquivo(idVeiculo, "F");
                        //Retorna a tabela de arquivos atualizada
                        tabelaArquivos = MontarTabelaArquivo(caminhoFisico, idVeiculo);
                        //Retorna a tabela de arquivos para a página
                    }
                }
                catch (Exception ex)
                {
                    transacao.Dispose();
                    mensagem = "Não foi possível deletar o arquivo " + nomeArquivo + ".Contate o administrador ou tente mais tarde.";
                }
            }
            var data = new { tabelaArquivos, mensagem };
            return Json(data);
        }

        /// <summary>
        ///     Função que retorna o caminho onde fica os arquivos do briefing do cliente
        /// </summary>
        /// <param name="idEmpresa">int</param>
        /// <param name="idBriefing">int</param>
        /// <param name="idGrupo">int</param>
        /// <param name="idComponente">int</param>
        /// <param name="tipoCaminho">string F -> caminho físico, V -> caminho virtual</param>
        /// <returns>Retorna um caminho do tipo string pode ser físico ou virtual</returns>
        public string RetornarCaminhoArquivo(int idVeiculo, string tipoCaminho)
        {
            if (tipoCaminho == "F")
            {
               // return @"C:/Users/Wellington/Documents/Visual Studio 2010/Projects/carOnline/carOnline/Content/images/veiculos/" + idVeiculo + "/";
                //return @"C:/Users/geraldo.borges/Pictures/carOnline/carOnline/Content/images/veiculos/" + idVeiculo + "/";
                 return @"C:/Users/geraldo/Pictures/carOnline/carOnline/Content/images/veiculos/" + idVeiculo + "/";
                
            }
            else
            {
                return @"~/Content/images/veiculos/" + idVeiculo + "/";
            }
        }
        /// <summary>
        ///     Função que retorna uma tabela de arquivos do briefing da empresa
        /// </summary>
        /// <param name="caminhoFisico">string</param>
        /// <param name="idItem">int</param>
        /// <param name="idBriefing">int</param>
        /// <param name="idGrupo">int</param>
        /// <param name="idComponente">int</param>
        /// <returns>Retorna uma tabela html com o nome do arquivo e um botão deletar</returns>
        public string MontarTabelaArquivo(string caminhoFisico, int idVeiculo)
        {
            if (Directory.Exists(caminhoFisico))//Se o caminho existe
            {
                string nomeFinalArquivo = "";
                char[] quebraCaminho = { '/' };
                StringBuilder tabela = new StringBuilder();

                //Cria a tabela 
                tabela.AppendLine("<table  class='tabelaArquivosFileList' border='1'>");
                //Cria o cabeçalho da tabela
                tabela.AppendLine("<tr class='tituloTabelaArquivosFileList'>");
                tabela.AppendLine("<th>Arquivo</td><th>Remover</td>");
                tabela.AppendLine("</tr>");

                //Busca todos os arquivos do diretório para preencher a tabela
                foreach (string item in Directory.GetFiles(caminhoFisico))
                {
                    //Pega somente o nome do arquivo
                    nomeFinalArquivo = item.Split(quebraCaminho)[item.Split(quebraCaminho).Length - 1];
                    //Insere uma linha na tabela
                    tabela.AppendLine("<tr data-nomeFinalArquivo=" + nomeFinalArquivo + ">");
                    tabela.AppendLine("<td>" + nomeFinalArquivo + "</td><td><a data-idVeiculo=" + idVeiculo + " href='#' data-Url=" + Url.Action("DeletarArquivo", "Veiculo") + "  data-nomeFinalArquivo=" + nomeFinalArquivo + "  class='Links botaoRemoverArquivoFileList'><img src=" + Url.Content("~/Content/images/deletar16px.png") + " title='Remover o arquivo'></a></td>");
                    tabela.AppendLine("</tr>");
                }
                //Fecha a tabela
                tabela.AppendLine("</table>");

                //Retorna a tabela montada
                return tabela.ToString();
            }
            else
                return "";
        }
        /// <summary>
        ///     Busca as tabelas de arquivos de um determinado briefing,grupo e componente
        /// </summary>
        /// <param name="idEmpresa">int</param>
        /// <param name="idBriefing">int</param>
        /// <param name="idGrupo">int</param>
        /// <param name="idComponente">int</param>
        /// <param name="idItem">int</param>
        /// <returns>Retorna uma tabela html com os arquivos</returns>
        [HttpPost]
        public ActionResult BuscarTabelaArquivo(int idVeiculo)
        {
            string tabelaArquivos = "";
            //Retorna o caminho dos arquivos que podem ser físico ou virtual
            string caminhoFisico = RetornarCaminhoArquivo(idVeiculo, "F");
            if (Directory.Exists(caminhoFisico))//Se o caminho existe
            {
                //Busca a tabela com os arquivos
                tabelaArquivos = MontarTabelaArquivo(caminhoFisico, idVeiculo);
            }

            var data = new { tabelaArquivos };
            //Retorna a tabela para página
            return Json(data);
        }
        
        [HttpPost]
        public ActionResult GravarFotos(HttpPostedFileBase file, int idVeiculo, int tamanhoArquivo, FormCollection formularioArquivo)
        {
            #region Variáveis e Objetos
            char[] quebraCaminho = { '\\' };
            string caminhoFisico = RetornarCaminhoArquivo(idVeiculo, "F");
            string caminhoVirtual = RetornarCaminhoArquivo(idVeiculo, "V");
            string nomeArquivo = file.FileName.Split(quebraCaminho)[file.FileName.Split(quebraCaminho).Length - 1];
            nomeArquivo = nomeArquivo.Replace(" ", "");
            string path = Path.Combine(Server.MapPath(caminhoVirtual), nomeArquivo);
            string mensagem = "";
            //string tabelaArquivos   = "";
            FileInfo arquivo = new FileInfo(caminhoFisico + nomeArquivo);
            bool salvarArquivo;
            #endregion

            //Busca o tamanho máximo do arquivo da tabela de parâmetros
            using (CarOnlineEntities DB = new CarOnlineEntities())
            {
                tblParametro parametros = DB.tblParametro.FirstOrDefault(p => p.idParametro.Equals(1));
                tamanhoArquivo = Convert.ToInt32(parametros.valor);
            }


            if ((file.ContentType == "image/jpeg") || (file.ContentType == "image/gif") || (file.ContentType == "image/png"))
            {
                //Se o tamanho do arquivo é compatível com o tamanho definido do componente
                if (!(file.ContentLength > tamanhoArquivo))
                {
                    //Se o caminho físico não existir cria o mesmo
                    if (!Directory.Exists(caminhoFisico))
                        Directory.CreateDirectory(caminhoFisico);
                    using (TransactionScope transacao = new TransactionScope())
                    {
                        try
                        {
                            //Salva o arquivo no caminho virtual da empresa
                            file.SaveAs(path);
                            using (CarOnlineEntities DB = new CarOnlineEntities())
                            {
                                tblFotosVeiculo foto = new tblFotosVeiculo();
                                foto.dataCadastro = DateTime.Now;
                                foto.urlFoto = caminhoVirtual + nomeArquivo;
                                foto.idVeiculo = idVeiculo;
                                DB.tblFotosVeiculo.AddObject(foto);
                                DB.SaveChanges();

                                transacao.Complete();
                                //Retorna a tabela para a página
                                //return Content(MontarTabelaArquivo(caminhoFisico, idVeiculo));
                                return RedirectToAction("Alteracao", new { id = idVeiculo });
                            }
                        }
                        catch (Exception ex)
                        {
                            transacao.Dispose();
                            mensagem = "Não foi possível gravar o arquivo.Contate o administrador ou tente mais tarde.";
                            return Content(mensagem);
                        }
                    }
                }
                else
                {
                    mensagem = "O tamanho do arquivo é inválido.Verifique o tamanho máximo permitido.";
                    return Content(mensagem);
                }
            }

            else
            {
                mensagem = "Esse arquivo não é uma imagem!";
                return Content(mensagem);
            }
        }

    }
}
