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
            //Obtendo a quantidade de elementos selecionado
            int quantidadeOpcoes = idOpcaoEnquete != null? idOpcaoEnquete.Length : 0;

            //Verificando se existem opções selecionadas
            if (quantidadeOpcoes > 0)
            {
                //Entidade
                CarOnlineEntities entity = new CarOnlineEntities();
                
                //Varrendo as opções selecionadas, quando for radio sempre será apenas uma
                for (int i = 0; i < quantidadeOpcoes; i++)
                {
                    //Caso venha um Radio o ID selecionado virá na posição 0 do vetor
                    //Caso venha um ou um conjunto de checkbox eles viram com seu id seguidos de seu estado - exemplo - 13 true
                    //Caso seu valor seja true a palavra True é removida caso seja false a palavra continua e é inválidada pela função IsInt32
                    if (idOpcaoEnquete[i].Contains("true"))
                        idOpcaoEnquete[i] = idOpcaoEnquete[i].ToLower().Replace("true", "").Trim(); 

                    //Verifica se o valor corrente pode ser um Int32
                    if (!Ferramenta.IsInt32(idOpcaoEnquete[i]))
                        continue;

                    //Convertendo a opção selecionada para Int32
                    int idOpcao = Convert.ToInt32(idOpcaoEnquete[i]);
                    
                    //Obtendo os dados da opção selecionada
                    var enquete = entity.tblOpcaoEnquete.Where(w => w.idOpcaoEnquete == idOpcao).FirstOrDefault();
                    
                    //Incrementando o total de votos
                    enquete.quantidadeVotos++;
                }

                //Efetuando o update
                entity.SaveChanges();
            }
            return View("Index");
        }

    }
}
