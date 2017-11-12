using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIST.SUIST.Web
{
    public class Constantes
    {
        #region Seguridad

        public const string RUTA_DOCUMENTO = "Session_Documento";
        public const string NUMDOCUMENTO = "NomDocumento";
        public const string JAVASCRIPT_KEY2 = "KeyJS2";
        public const char EspacioEnBlanco = ' ';
        public const string OptCrear = "N";
        public const string OptEditar = "E";
        public const int EstadoAcivado = 1;
        public const int EstadoDesacivado = 0;
        public const bool EstadoSesionActiva = true;
        public const bool EstadoSesionInactiva = false;

        //Sesiones
        public const string USER_SESSION = "OBJUSUARIO";
        public const string Sesion_IdUsuario = "ID_USUARIO";
        public const string Sesion_Usuario = "USUARIO";
        public const string Sesion_NombreUsuario = "NOMBRE";
        public const string Sesion_Perfil = "PERFIL";
        public const string Sesion_Empresa = "EMPRESA";
        public const string Sesion_Auditoria = "AUDITORIA";
        public const string Sesion_CorreoUsuario = "CORREOUSUARIO";
        public const string Session_ListaActividades = "ACTIVIDADES";
        public const string Sesion_DtExcel = "DTEXCEL";
        public const string Sesion_ExpExcel = "EXP_EXCEL";
        public const string Sesion_NombreClienteReporte = "NOMBRE_CLIENTE_REPORTE";

        #endregion

        #region Combos

        public const string VALOR_SELECCION_COMBO = "";
        public const string TEXTO_SELECCION_COMBO = "---Seleccione---";
        public const string VALOR_TODOS_COMBO = "-1";
        public const string TEXTO_TODOS_COMBO = "---Todos---";

        #endregion

        #region Otros

        /// <summary>Valor que se almacenara en la BD indicando que el registro ha sido eliminado de forma logica(INACTIVO)</summary>
        public const bool INDICADOR_REGISTRO_ELIMINADO = true;

        /// <summary>Valor que se almacenara en la BD indicando que el registro no ha sido eliminado de forma logica(ACTIVO)</summary>
        public const bool INDICADOR_REGISTRO_NOELIMINADO = false;

        #endregion

        #region Carpetas

        public const string Carp_LogosEmpresa = "ImagenFormato";

        #endregion
    }
}