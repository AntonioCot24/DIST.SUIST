using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class ParametrosConfiguracionBE
    {
        /**Cadena Conexion**/
        private static readonly string _strConexion = "Conexion";
        public static string CadenaConexion
        {
            get { return ConfigurationManager.ConnectionStrings[_strConexion].ConnectionString; }
        }

        /**Esquema SCT**/
        private static readonly string esquemaaplicacion = "EsquemaAplicacionKey";
        public static string EsquemaAplicacion
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings[esquemaaplicacion]); }
        }

        /**Nombre Aplicacion**/
        private static readonly string aplicacionNombre = "AplicacionNombreKey";
        public static string AplicacionNombre
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings[aplicacionNombre]); }
        }

        /*Folder WebSite**/
        private static readonly string aplicacionfolder = "AplicacionFolderKey";
        public static string AplicacionFolder
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings[aplicacionfolder]); }
        }

        /**Ruta Carpeta Documentos**/
        private static readonly string rutaDocumento = "RutaArchivoKey";
        public static string RutaDocumento
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings[rutaDocumento]); }
        }

        /**Ruta Carpeta Documentos Temporal**/
        private static readonly string rutaDocumentoTemp = "RutaTempArchivoKey";
        public static string RutaDocumentoTemp
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings[rutaDocumentoTemp]); }
        }
        
        /**Cuenta Correo**/
        private static readonly string cuentacorreo = "AplicacionCorreo";
        public static string CuentaCorreo
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings[cuentacorreo]); }
        }

        /**Password Correo**/
        private static readonly string passwordcorreo = "AplicacionPasswordCorreo";
        public static string PasswordCorreo
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings[passwordcorreo]); }
        }

        /**Host Correo**/
        private static readonly string hostcorreo = "AplicacionHostCorreo";
        public static string HostCorreo
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings[hostcorreo]); }
        }

        /**Port Correo**/
        private static readonly string portcorreo = "AplicacionPortCorreo";
        public static string PortCorreo
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings[portcorreo]); }
        }
    }
}
