using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using carOnline.Models;

namespace carOnline.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Votar(string[] idOpcaoEnquete) 
        {
            //Quantidade de elementos selecionados
            int quantidadeOpcoes = idOpcaoEnquete.Length;

            if (quantidadeOpcoes > 0)
            {
                CarOnlineEntities entity = new CarOnlineEntities();
                for (int i = 0; i < quantidadeOpcoes; i++)
                {
                    if (!Ferramenta.IsInt32(idOpcaoEnquete[i]))
                        continue;

                    int idOpcao = Convert.ToInt32(idOpcaoEnquete[i]);
                    var enquete = entity.tblOpcaoEnquete.Where(w => w.idOpcaoEnquete == idOpcao).FirstOrDefault();
                    enquete.quantidadeVotos++;
                }
                entity.SaveChanges();
            }
            return View("Index");
        }

    }
}
