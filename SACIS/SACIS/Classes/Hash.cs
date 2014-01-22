
///<summary>
/// Classe para implementação do hash
///
/// @author Fabio Augusto
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Security.Cryptography;
using System.IO;
using System.Security.Cryptography;

namespace SACIS.Classes
{
    public class Hash
    {
        ///<summary>
        ///
        /// Método para retornar o hash de uma string passada utilizando SHA512
        ///
        ///</summary>
        public static string hashing(string text)
        {

            UTF8Encoding cod = new UTF8Encoding();
            SHA512Managed hasher = new SHA512Managed();
            byte[] data = hasher.ComputeHash(cod.GetBytes(text));

            // armazena os bytes criando uma string
            StringBuilder texto = new StringBuilder();

            // cria uma string hexadecimal para evitar erro nas mensagens transmitidas
            for (int i = 0; i < data.Length; i++) texto.Append(data[i].ToString("x2"));

            // retorna a string em hexadecimal
            return texto.ToString();

        }

    }
}
