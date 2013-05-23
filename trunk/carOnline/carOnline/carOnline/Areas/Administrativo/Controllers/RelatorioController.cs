using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using carOnline.Models;
using carOnline.Models.ComplexTypes;
using Rotativa;
using Rotativa.Options;
namespace carOnline.Areas.Administrativo.Controllers
{
    public class RelatorioController : Controller
    {

        #region Relatório de Carros Cadastrados
        public ActionResult FormularioRelatorioCarrosCadastrados()
        {
            using(CarOnlineEntities DB = new CarOnlineEntities())
            {
                List<tblMarcaVeiculo> marcas = DB.tblMarcaVeiculo.ToList<tblMarcaVeiculo>();
                List<tblModeloVeiculo> modelos = DB.tblModeloVeiculo.ToList<tblModeloVeiculo>();
                var query = DB.tblMarcaVeiculo.Select(m => new { m.descricao, m.idMarcaVeiculo });
                Dictionary<int, string> todasMarcas = new Dictionary<int, string>();

                foreach (var item in query)
                {
                    todasMarcas.Add(item.idMarcaVeiculo,item.descricao);
                }
                @ViewBag.ListaCheckBoxMarcas = todasMarcas;
                @ViewBag.ListaMarcas = new SelectList(marcas, "idMarcaVeiculo", "descricao");
                @ViewBag.ListaModelos = new SelectList(modelos, "idModeloVeiculo", "descricao");
            }
            return View();
        }
        public ActionResult BuscarTodosModelos()
        {
            using (CarOnlineEntities DB = new CarOnlineEntities())
            {
                List<ModelosRelatorio> modelos = new List<ModelosRelatorio>();
                var query = DB.tblModeloVeiculo;
                foreach (var item in query)
                {
                    ModelosRelatorio m = new ModelosRelatorio();
                    m.idModelo = item.idModeloVeiculo;
                    m.descricao = item.descricao;
                    modelos.Add(m);
                }
                var data = new { modelos };
                return Json(data);
            }

        }
        [HttpPost]
        public ActionResult BuscarModelosFiltrados(string[] marcas)
        {
            using (CarOnlineEntities DB = new CarOnlineEntities())
            {
                List<ModelosRelatorio> modelos = new List<ModelosRelatorio>();
                var query = DB.tblModeloVeiculo.Where(x => marcas.Contains(x.tblMarcaVeiculo.descricao));

                foreach (var item in query)
                {
                    ModelosRelatorio m = new ModelosRelatorio();
                    m.idModelo = item.idModeloVeiculo;
                    m.descricao = item.descricao;
                    modelos.Add(m);
                }
                var data = new { modelos };
                return Json(data);
            }

        }
        public ActionResult RelatorioCarrosCadastros()
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult RelatorioCarrosCadastradosPDF(string dataInicial,string dataFinal,string[] modelos,FormCollection formulario)
        {
            var allvalues = formulario["modelos"].Split(',').Select(x => int.Parse(x));

            foreach (var item in allvalues)
            {
                string teste = item.ToString();
            }

            List<RelatorioCarrosCadastrados_Result> listaCarros = new List<RelatorioCarrosCadastrados_Result>();

            using (CarOnlineEntities DB = new CarOnlineEntities())
            {
                
                listaCarros = DB.RelatorioCarrosCadastrados(dataInicial, dataFinal).ToList<RelatorioCarrosCadastrados_Result>();
            }
            if(modelos!=null)
                listaCarros.Where(lc => modelos.Contains(lc.Modelo));
            var pdf = new ViewAsPdf
            {
                ViewName = "RelatorioCarrosCadastrados",
                Model = listaCarros
            };
            return pdf;
        }
        #endregion

        #region Relatório de Carros Vendidos
        public ActionResult FormularioRelatorioCarrosVendidos()
        {
            using (CarOnlineEntities DB = new CarOnlineEntities())
            {
                List<tblMarcaVeiculo> marcas = DB.tblMarcaVeiculo.ToList<tblMarcaVeiculo>();
                List<tblModeloVeiculo> modelos = DB.tblModeloVeiculo.ToList<tblModeloVeiculo>();

                @ViewBag.ListaMarcas = new SelectList(marcas, "idMarcaVeiculo", "descricao");
                @ViewBag.ListaModelos = new SelectList(modelos, "idModeloVeiculo", "descricao");
            }
            return View();
        }
        public ActionResult RelatorioCarrosVendidos()
        {
            if (Session["ADMINISTRADOR"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }
        public ActionResult RelatorioCarrosVendidosPDF(FormCollection formulario)
        {
            List<RelatorioCarrosVendidos_Result> listaVendas = new List<RelatorioCarrosVendidos_Result>();
            int marca  = Convert.ToInt32(formulario["ListaMarcas"]);
            int modelo = Convert.ToInt32(formulario["ListaModelos"]);
            string porMarca = formulario["porMarca"];
            string porModelo = formulario["porModelo"];

            if (porMarca == null)
                marca = 0;
            if (porModelo == null)
                modelo = 0;
            using (CarOnlineEntities DB = new CarOnlineEntities())
            {
                listaVendas = DB.RelatorioCarrosVendidos(formulario["dataInicial"], formulario["dataFinal"],marca,modelo).ToList<RelatorioCarrosVendidos_Result>();
                double TotalVenda = 0;
                foreach (var registro in listaVendas)
                {
                    TotalVenda += registro.ValorVenda;
                }
                @ViewBag.TotalVenda = TotalVenda;
            }
            var pdf = new ViewAsPdf
            {
                ViewName = "RelatorioCarrosVendidos",
                Model = listaVendas
            };
            return pdf;
        }
        #endregion
    }
}
