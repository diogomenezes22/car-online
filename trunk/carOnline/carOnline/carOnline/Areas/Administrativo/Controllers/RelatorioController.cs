﻿using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using carOnline.Models;
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

                @ViewBag.ListaMarcas = new SelectList(marcas, "idMarcaVeiculo", "descricao");
                @ViewBag.ListaModelos = new SelectList(modelos, "idModeloVeiculo", "descricao");
            }
            return View();
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

        public ActionResult RelatorioCarrosCadastradosPDF(FormCollection formulario)
        {
            List<RelatorioCarrosCadastrados_Result> listaCarros = new List<RelatorioCarrosCadastrados_Result>();

            int marca  = Convert.ToInt32(formulario["ListaMarcas"]);
            int modelo = Convert.ToInt32(formulario["ListaModelos"]);

            string porMarca  = formulario["porMarca"];
            string porModelo = formulario["porModelo"];

            if (porMarca == null)
                marca  = 0;
            if (porModelo == null)
                modelo = 0;

            using (CarOnlineEntities DB = new CarOnlineEntities())
            {
                
                listaCarros = DB.RelatorioCarrosCadastrados(formulario["dataInicial"], formulario["dataFinal"],marca,modelo).ToList<RelatorioCarrosCadastrados_Result>();
            }
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