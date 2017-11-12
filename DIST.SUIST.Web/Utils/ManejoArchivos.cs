using DIST.SUIST.BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DIST.SUIST.Web
{
    public class ManejoArchivos
    {
        public static void CrearCarpeta(string nomCarpeta)
        {
            string folderName = HttpContext.Current.Server.MapPath(@"\" + ParametrosConfiguracionBE.AplicacionFolder + @"\" + ParametrosConfiguracionBE.RutaDocumento);

            string pathString = Path.Combine(folderName, nomCarpeta);

            if (!Directory.Exists(pathString))
            {
                Directory.CreateDirectory(pathString);
            }
        }

        public static void MoverArchivo(string nomArchivoTemp, string rutaDestino, string NomArchivoDet)
        {
            string folderTempName = Path.Combine(HttpContext.Current.Server.MapPath(@"\" + ParametrosConfiguracionBE.AplicacionFolder + @"\" + ParametrosConfiguracionBE.RutaDocumentoTemp), nomArchivoTemp);
            string folderDestName = Path.Combine(HttpContext.Current.Server.MapPath(@"\" + ParametrosConfiguracionBE.AplicacionFolder + @"\" + ParametrosConfiguracionBE.RutaDocumento), rutaDestino, NomArchivoDet);

            CrearCarpeta(rutaDestino);

            if (!File.Exists(folderTempName))
                return;

            if (File.Exists(folderDestName))
                File.Delete(folderDestName);

            File.Move(folderTempName, folderDestName);
        }

        public static string ObtenerRutaArchivo(string rutaArchivo, string nombreArchivo)
        {
            string rutaarchivo = "";

            string folderFile = Path.Combine(ParametrosConfiguracionBE.RutaDocumento, rutaArchivo, nombreArchivo);

            rutaarchivo = string.IsNullOrEmpty(ParametrosConfiguracionBE.AplicacionFolder) ? "/" + folderFile.Replace("\\", "/") : "/" + ParametrosConfiguracionBE.AplicacionFolder + "/" + folderFile.Replace("\\", "/");

            return rutaarchivo;
        }

        public static void EliminarArchivo(string rutaArchivo, string nombreArchivo)
        {
            string rutaFile = Path.Combine(HttpContext.Current.Server.MapPath(@"\" + ParametrosConfiguracionBE.AplicacionFolder + @"\" + ParametrosConfiguracionBE.RutaDocumento), rutaArchivo, nombreArchivo.Trim()).Replace("\\", "/");

            if (File.Exists(rutaFile))
                File.Delete(rutaFile);
        }

        public static bool EliminarArchivoTemp(string nombreArchivo)
        {
            string rutaFile = Path.Combine(HttpContext.Current.Server.MapPath(@"\" + ParametrosConfiguracionBE.AplicacionFolder + @"\" + ParametrosConfiguracionBE.RutaDocumento), nombreArchivo.Trim()).Replace("\\", "/");

            if (File.Exists(rutaFile))
            {
                File.Delete(rutaFile);
                return true;
            }
            else
                return false;
        }
    }
}