using System;
using System.Collections.Generic;
using System.Linq;

namespace WayPro.Gear.Encriptacion {

    /// <summary>
    /// Clase que provee funcionalidades del algoritmo de encriptacion Complex.
    /// </summary>
    /// <remarks>
    /// El algoritmo Complex nace de la necesidad de contar con un método de encriptación reversible que no sea conocido por terceros o externos a WayPro.
    /// </remarks>
    /// <bitacora>
    /// GEL.20190911: Se desconoce la fecha de implementación.
    /// GEL.20200114: Se exporta desde la version anterior de la dll.
    /// </bitacora>
    public static class Complex {

        /// <summary>
        /// Metodo que permite establecer o determinar si una cadena fue encriptada con el método Complex.
        /// </summary>
        /// <param name="vCadena">Cadena que se desea evaluar.</param>
        /// <returns>true cuando la cadena efectivamente fue encriptada con el método. En cuyo caso se puede desencriptar y false cuando no.</returns>
        public static bool EstaEncriptado(string vCadena) { 
            try {
                Desencripta(vCadena);
                return true;
            } catch (Exception) {
                return false;
            }
        }

        /// <summary>
        /// Permite encriptar una cadena bajo el algoritmo Complex.
        /// </summary>
        /// <param name="vSinEncriptar">Candena que se desea encriptar.</param>
        /// <returns>Cadena encriptada bajo el método Complex</returns>        
        public static string Encripta(string vSinEncriptar)
        {
            if (string.IsNullOrEmpty(vSinEncriptar)) throw new ArgumentException("No se puede encriptar una cadena vácia o nula.");

            string vCadena = Base64.Encripta(vSinEncriptar);
            int vLargo = vCadena.Length / 2; //siempre deberia ser par, por lo que al dividirlo por 2 debe ser integer
            string vAux = string.Empty;

            for (int i = 0; i < vLargo; i++) {
                vAux = vAux + vCadena[i] + vCadena[vLargo + i];
            }
            return Base64.Encripta(vAux);
        }

        /// <summary>
        /// Permite volver a su estado original una cadena que anteriormente fue encriptda con el método Complex.
        /// </summary>
        /// <param name="vEncriptado">Cadena de texto encriptada con Complex.</param>
        /// <returns>Cadena de texto desencriptada.</returns>
        public static string Desencripta(string vEncriptado)
        {
            if (string.IsNullOrEmpty(vEncriptado)) throw new ArgumentException("No se puede desencriptar una cadena vácia o nula.");
            try {
                string vCadena = Base64.Desencripta(vEncriptado);
                int vLargo = vCadena.Length;
                string vAux = string.Empty, vAux2 = string.Empty;
                for (int i = 0; i < vLargo; i+=2){
                    vAux += vCadena[i];
                    vAux2 += vCadena[i + 1];
                }
                return Base64.Desencripta(vAux + vAux2);
            } catch (Exception) {
                throw new Exception("La cadena parece no encriptada con este método.");
            }
        }

    }
}
