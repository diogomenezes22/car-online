using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using carOnline.Models;
namespace carOnline.Models
{
    public class Ferramenta
    {
        public static bool ValidarEmail(string email)
        {
            Regex rg = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$");

            if (rg.IsMatch(email))
                return true;
            else
                return false;
        }
        public static bool EmailCadastrado(string email,string tabela)
        {
            if(tabela=="ADMINISTRADOR")
            {
                using(CarOnlineEntities DB = new CarOnlineEntities())
                {
                    var query = DB.tblAdministrador.Where(a => a.email.Equals(email));
                    if (query.Count()>=1)
                        return true;
                    else
                        return false;
                }
            }
            else if (tabela == "FUNCIONARIO")
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    var query = DB.tblFuncionario.Where(a => a.email.Equals(email));
                    if (query.Count() >= 1)
                        return true;
                    else
                        return false;
                }
            }
            else if (tabela == "SEGURADORA")
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    var query = DB.tblSeguradora.Where(a => a.email.Equals(email));
                    if (query.Count() >= 1)
                        return true;
                    else
                        return false;
                }
            }
            else
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    var query = DB.tblUsuario.Where(a => a.email.Equals(email));
                    if (query.Count() >= 1)
                        return true;
                    else
                        return false;
                }
            }
        }
    }
}