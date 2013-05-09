using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace carOnline.Models
{
    /// <summary>
    /// Diogo - Controle base para se adicionar implementações aos controles padrões
    /// </summary>
    public class BaseController : System.Web.Mvc.Controller
    {
        /// <summary>
        /// Construtor Padrão
        /// </summary>
        public BaseController():base() { }


        /// <summary>
        /// Exibe uma mensagem na tela
        /// </summary>
        /// <param name="ViewBag"></param>
        /// <param name="mensagem"></param>
        /// <param name="titulo"></param>
        public static void Menssagem(dynamic ViewBag ,string mensagem, string titulo = "Aviso") 
        {
            ViewBag.mensagemModal = mensagem;
            ViewBag.mensagemTitulo = titulo;
        }
    }
}