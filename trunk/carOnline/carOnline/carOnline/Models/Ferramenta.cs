﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using carOnline.Models;
namespace carOnline.Models
{
    public class Ferramenta
    {

        #region Utils
        /// <summary>
        /// Diogo - Converte um valor em bytes para mbytes, levando em consideração que 1mb = 1048576 bytes
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        /// <returns>Retorna o valor de entrada convertido em bytes</returns>
        public static double ConvertByteToMB(double value)
        {
            double mbValueInByte = 1048576;

            return value / mbValueInByte;
        }

        public static bool EmailCadastrado(string email, string tabela)
        {
            if (tabela == "ADMINISTRADOR")
            {
                using (CarOnlineEntities DB = new CarOnlineEntities())
                {
                    var query = DB.tblAdministrador.Where(a => a.email.Equals(email));
                    if (query.Count() >= 1)
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

        #endregion
        
        #region Validations

        public static bool ValidarEmail(string email)
        {
            Regex rg = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$");

            if (rg.IsMatch(email))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Diogo - Verifica se um valor de entrada pode ser do tipo Int32
        /// </summary>
        /// <param name="strInt"></param>
        /// <returns></returns>
        public static bool IsInt32(string strInt)
        {
            int number;
            return Int32.TryParse(strInt, out number);
        }

        /// <summary>
        /// Diogo - Verifica se um valor de entrada pode ser do tipo DateTime
        /// </summary>
        /// <param name="strInt"></param>
        /// <returns></returns>
        public static bool IsDateTime(string strDateTime)
        {
            DateTime date;
            return DateTime.TryParse(strDateTime, out date);
        }

        #endregion


    }
}