using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayPro.Gear.Encriptacion {

    /// <summary>
    /// Clase que provee encriptación en el algoritmo Base64. Este metodo es reversible.
    /// </summary>
    /// <bitacora>
    /// GEL.20171127: Se desconoce la fecha de creacion. Se sabe que lo hizo RRL.
    /// GEL.20171127: Se agrega el parametro vURLSecure que permite obtener la cadena en base64 pero en un formato compatible con URLs
    /// </bitacora>
    public static class Base64 {

        static readonly char[] vPadding = { '=' };

        /// <summary>
        /// Encripta un texto o cadena en Base64.
        /// </summary>
        /// <param name="vSinEncriptar">Cadena que se desea encriptar.</param>
        /// <param name="vURLSecure">Permite establecer si la cadena resultante puede o no tener caracteres inválidos para ura url como lo serian + y /.</param>
        /// <returns>Cadena encriptada en Base64</returns>
        public static string Encripta(string vSinEncriptar, bool vURLSecure = false) {
            byte[] b = Encoding.UTF8.GetBytes(vSinEncriptar);
            if (vURLSecure)
                return Convert.ToBase64String(b).TrimEnd(vPadding).Replace('+', '-').Replace('/', '_');
            else
                return Convert.ToBase64String(b);
        }

        /// <summary>
        /// Desencripta una cadena encriptada en Base64.
        /// </summary>
        /// <param name="vEncriptado">Cadena encriptada en Base64</param>
        /// <returns>Cadena desencriptada.</returns>
        public static string Desencripta(string vEncriptado) {
            string incoming = vEncriptado.Replace('_', '/').Replace('-', '+');
            switch (vEncriptado.Length % 4) {
                case 2: incoming += "=="; break;
                case 3: incoming += "="; break;
            }

            byte[] b = Convert.FromBase64String(incoming);
            return Encoding.UTF8.GetString(b);
        }

    }
}
