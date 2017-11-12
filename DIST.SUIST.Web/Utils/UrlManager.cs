using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIST.SUIST.Web
{
    public class UrlManager
    {
        #region MENU_PRINCIPAL

        //Inicio
        public const string WEB_INICIO = "WEB_INICIO";
        public const string WEB_CAMBIAR_CONTRSENIA = "WEB_CAMBIAR_CONTRSENIA";

        //Administrar
        public const string WEB_ADMINISTRAR = "WEB_ADMINISTRAR";

        public const string WEB_ADMINISTRAR_CLIENTES = "WEB_ADMINISTRAR_CLIENTES";
        public const string WEB_ADMINISTRAR_PROYECTOS = "WEB_ADMINISTRAR_PROYECTOS";
        public const string WEB_ADMINISTRAR_ACTIVIDAD = "WEB_ADMINISTRAR_ACTIVIDAD";

        //Control Tiempos
        public const string WEB_CONTROL_TIEMPO = "WEB_CONTROL_TIEMPO";

        //Control Gastos
        public const string WEB_CONTROL_GASTO = "WEB_CONTROL_GASTO";

        //Reportes
        public const string WEB_REPORTES = "WEB_REPORTES";
        public const string WEB_REPORTES_FACTURACION_CLIENTE = "WEB_REPORTES_FACTURACION_CLIENTE";
        public const string WEB_REPORTES_PRODUCTIVIDAD = "WEB_REPORTES_PRODUCTIVIDAD";

        //Maestros
        public const string WEB_MAESTROS = "WEB_MAESTROS";

        public const string WEB_MAESTROS_USUARIOS = "WEB_MAESTROS_USUARIOS";

        /*** URL ***/
        //Inicio
        public const string URL_INICIO = "/frmInicio.aspx";
        public const string URL_CAMBIAR_CONTRSENIA = "/Forms/Seguridad/frmCambiarContrasenia.aspx";

        //Administrar
        public const string URL_ADMINISTRAR = "";
        public const string URL_ADMINISTRAR_CLIENTES = "/Forms/Administrar/Clientes/frmAdministrarClientes.aspx";
        public const string URL_ADMINISTRAR_PROYECTOS = "/Forms/Administrar/Proyectos/frmAdministrarProyectos.aspx";
        public const string URL_ADMINISTRAR_ACTIVIDAD = "/Forms/Administrar/Actividades/frmAdministrarTipoActividades.aspx";

        //Control Tiempos
        public const string URL_CONTROL_TIEMPO = "/Forms/ControlTiempo/frmAdministrarTiempos.aspx";

        //Control Gastos
        public const string URL_CONTROL_GASTO = "/Forms/ControlGastos/frmAdministrarGastos.aspx";

        //Reportes
        public const string URL_REPORTES = "";
        public const string URL_REPORTES_FACTURACION_CLIENTE = "/Forms/Reportes/frmSeleccionarClienteReporte.aspx";
        public const string URL_REPORTES_PRODUCTIVIDAD = "/Forms/Reportes/frmReporteProductividad.aspx";

        //Maestros
        public const string URL_MAESTROS = "";

        public const string URL_MAESTROS_USUARIOS = "/Forms/Maestros/Usuarios/frmMantenerUsuarios.aspx";

        #endregion

        #region COMUN

        public const string WEB_DESCARGAR_EXCEL = "WEB_DESCARGAR_EXCEL";
        public const string WEB_FILEUPLOAD = "WEB_FILEUPLOAD";
        public const string WEB_ENVIAR_CORREO = "WEB_ENVIAR_CORREO";

        public const string URL_DESCARGAR_EXCEL = "/Forms/Utiles/frmDescargarExcel.aspx";
        public const string URL_FILEUPLOAD = "/WebServices/wsFileUpload.ashx";
        public const string URL_ENVIAR_CORREO = "/Forms/frmEnviarCorreo.aspx";

        /*** URL_WEBSERVICES  ***/
        public const string WEB_WEBSERVICE_FILEUPLOAD = "WEB_WEBSERVICE_FILEUPLOAD";

        //Web Services
        public const string URL_WEBSERVICE_FILEUPLOAD = "/api/fileupload/uploadfile";

        #endregion

        #region SEGURIDAD

        public const string WEB_PRINCIPAL = "WEB_PRINCIPAL";

        /*** URL ***/
        public const string URL_PRINCIPAL = "/Forms/frmInicio.aspx";

        #endregion

        #region ADMINISTRAR

        public const string WEB_ADMINISTRAR_CLIENTES_REGISTRO = "WEB_ADMINISTRAR_CLIENTES_REGISTRO";
        public const string WEB_ADMINISTRAR_PROYECTOS_REGISTRO = "WEB_ADMINISTRAR_PROYECTOS_REGISTRO";
        public const string WEB_ADMINISTRAR_ACTIVIDAD_REGISTRO = "WEB_ADMINISTRAR_ACTIVIDAD_REGISTRO";
        public const string WEB_ADMINISTRAR_CONTACTOS_LISTA = "WEB_ADMINISTRAR_CONTACTOS_LISTA";
        public const string WEB_ADMINISTRAR_CONTACTOS_REGISTRO = "WEB_ADMINISTRAR_CONTACTOS_REGISTRO";
        public const string WEB_ADMINISTRAR_FACTURACION_REGISTRO = "WEB_ADMINISTRAR_FACTURACION_REGISTRO";

        /*** URL ***/

        public const string URL_ADMINISTRAR_CLIENTES_REGISTRO = "/Forms/Administrar/Clientes/frmRegistrarClientes.aspx";
        public const string URL_ADMINISTRAR_PROYECTOS_REGISTRO = "/Forms/Administrar/Proyectos/frmRegistroProyectos.aspx";
        public const string URL_ADMINISTRAR_ACTIVIDAD_REGISTRO = "/Forms/Administrar/Actividades/frmRegistroTipoActividades.aspx";
        public const string URL_ADMINISTRAR_CONTACTOS_LISTA = "/Forms/Administrar/Clientes/frmListaContactos.aspx";
        public const string URL_ADMINISTRAR_CONTACTOS_REGISTRO = "/Forms/Administrar/Clientes/frmRegistrarContactos.aspx";
        public const string URL_ADMINISTRAR_FACTURACION_REGISTRO = "/Forms/Administrar/Facturacion/frmRegistrarFacturacion.aspx";

        #endregion

        #region CONTROL TIEMPOS

        public const string WEB_CONTROL_TIEMPO_REGITRO_ACTIVIDAD = "WEB_CONTROL_TIEMPO_REGITRO_ACTIVIDAD";

        /*** URL ***/

        public const string URL_CONTROL_TIEMPO_REGITRO_ACTIVIDAD = "/Forms/ControlTiempo/frmRegistrarActividad.aspx";

        #endregion

        #region CONTROL GASTO

        public const string WEB_CONTROL_GASTO_REGITRO = "WEB_CONTROL_GASTO_REGITRO";

        /*** URL ***/

        public const string URL_CONTROL_GASTO_REGITRO = "/Forms/ControlGastos/frmRegistrarGasto.aspx";

        #endregion

        #region REPORTE

        public const string WEB_REPORTES_CLIENTE_HORAS = "WEB_REPORTES_CLIENTE_HORAS";

        /*** URL ***/

        public const string URL_REPORTES_CLIENTE_HORAS = "/Forms/Reportes/frmReporteClienteHoras.aspx";

        #endregion

        #region MAESTROS

        public const string WEB_MAESTROS_USUARIOS_REGISTRO = "WEB_MAESTROS_USUARIOS_REGISTRO";

        /*** URL ***/

        public const string URL_MAESTROS_USUARIOS_REGISTRO = "/Forms/Maestros/Usuarios/frmRegistroUsuarios.aspx";

        #endregion

        #region WEBSERVICES

        /*** URL_WEBSERVICES  ***/
        public const string WEB_WEBSERVICE_SEGURIDAD = "WEB_WEBSERVICE_SEGURIDAD";

        public const string WEB_WEBSERVICE_ADMINISTRAR_PROYECTO = "WEB_WEBSERVICE_ADMINISTRAR_PROYECTO";
        public const string WEB_WEBSERVICE_ADMINISTRAR_TIPO_ACTIVIDAD = "WEB_WEBSERVICE_ADMINISTRAR_TIPO_ACTIVIDAD";
        public const string WEB_WEBSERVICE_ADMINISTRAR_CLIENTE = "WEB_WEBSERVICE_ADMINISTRAR_CLIENTE";
        public const string WEB_WEBSERVICE_ADMINISTRAR_CONTACTO = "WEB_WEBSERVICE_ADMINISTRAR_CONTACTO";
        public const string WEB_WEBSERVICE_ADMINISTRAR_FACTURACION = "WEB_WEBSERVICE_ADMINISTRAR_FACTURACION";

        public const string WEB_WEBSERVICE_CONTROL_TIEMPO_ACTIVIDAD = "WEB_WEBSERVICE_CONTROL_TIEMPO_ACTIVIDAD";

        public const string WEB_WEBSERVICE_CONTROL_GASTO = "WEB_WEBSERVICE_CONTROL_GASTO";

        public const string WEB_WEBSERVICE_MAESTROS_USUARIO = "WEB_WEBSERVICE_MAESTROS_USUARIO";
        public const string WEB_WEBSERVICE_MAESTROS_MONEDA = "WEB_WEBSERVICE_MAESTROS_MONEDA";

        public const string WEB_WEBSERVICE_REPORTE = "WEB_WEBSERVICE_REPORTE";

        //Web Services
        public const string URL_WEBSERVICE_SEGURIDAD = "/WebServices/wsSeguridad.asmx";

        public const string URL_WEBSERVICE_ADMINISTRAR_PROYECTO = "/WebServices/wsProyecto.asmx";
        public const string URL_WEBSERVICE_ADMINISTRAR_TIPO_ACTIVIDAD = "/WebServices/wsTipoActividad.asmx";
        public const string URL_WEBSERVICE_ADMINISTRAR_CLIENTE = "/WebServices/wsCliente.asmx";
        public const string URL_WEBSERVICE_ADMINISTRAR_CONTACTO = "/WebServices/wsContacto.asmx";
        public const string URL_WEBSERVICE_ADMINISTRAR_FACTURACION = "/WebServices/wsFacturacion.asmx";

        public const string URL_WEBSERVICE_CONTROL_TIEMPO_ACTIVIDAD = "/WebServices/wsActividad.asmx";

        public const string URL_WEBSERVICE_CONTROL_GASTO = "/WebServices/wsGasto.asmx";

        public const string URL_WEBSERVICE_MAESTROS_USUARIO = "/WebServices/wsUsuario.asmx";
        public const string URL_WEBSERVICE_MAESTROS_MONEDA = "/WebServices/wsMoneda.asmx";

        public const string URL_WEBSERVICE_REPORTE = "/WebServices/wsReporte.asmx";

        #endregion

        public static string getURL(string codigo)
        {
            switch (codigo)
            {
                case WEB_INICIO: return URL_INICIO;
                case WEB_CAMBIAR_CONTRSENIA: return URL_CAMBIAR_CONTRSENIA;

                //Menu Principal
                case WEB_ADMINISTRAR: return URL_ADMINISTRAR;
                case WEB_ADMINISTRAR_CLIENTES: return URL_ADMINISTRAR_CLIENTES;
                case WEB_ADMINISTRAR_PROYECTOS: return URL_ADMINISTRAR_PROYECTOS;
                case WEB_ADMINISTRAR_ACTIVIDAD: return URL_ADMINISTRAR_ACTIVIDAD;

                case WEB_CONTROL_TIEMPO: return URL_CONTROL_TIEMPO;
                case WEB_CONTROL_GASTO: return URL_CONTROL_GASTO;

                case WEB_REPORTES: return URL_REPORTES;
                case WEB_REPORTES_FACTURACION_CLIENTE: return URL_REPORTES_FACTURACION_CLIENTE;
                case WEB_REPORTES_PRODUCTIVIDAD: return URL_REPORTES_PRODUCTIVIDAD;

                case WEB_MAESTROS: return URL_MAESTROS;
                case WEB_MAESTROS_USUARIOS: return URL_MAESTROS_USUARIOS;

                // Común
                case WEB_DESCARGAR_EXCEL: return URL_DESCARGAR_EXCEL;
                case WEB_FILEUPLOAD: return URL_FILEUPLOAD;
                    
                //Administración
                case WEB_ADMINISTRAR_PROYECTOS_REGISTRO: return URL_ADMINISTRAR_PROYECTOS_REGISTRO;
                case WEB_ADMINISTRAR_ACTIVIDAD_REGISTRO: return URL_ADMINISTRAR_ACTIVIDAD_REGISTRO;
                case WEB_ADMINISTRAR_CLIENTES_REGISTRO: return URL_ADMINISTRAR_CLIENTES_REGISTRO;
                case WEB_ADMINISTRAR_CONTACTOS_LISTA: return URL_ADMINISTRAR_CONTACTOS_LISTA;
                case WEB_ADMINISTRAR_CONTACTOS_REGISTRO: return URL_ADMINISTRAR_CONTACTOS_REGISTRO;
                case WEB_ADMINISTRAR_FACTURACION_REGISTRO: return URL_ADMINISTRAR_FACTURACION_REGISTRO;

                //Control Tiempos
                case WEB_CONTROL_TIEMPO_REGITRO_ACTIVIDAD: return URL_CONTROL_TIEMPO_REGITRO_ACTIVIDAD;

                //Control Gastos
                case WEB_CONTROL_GASTO_REGITRO: return URL_CONTROL_GASTO_REGITRO;

                //Reportes
                case WEB_REPORTES_CLIENTE_HORAS: return URL_REPORTES_CLIENTE_HORAS;

                //Maestros
                case WEB_MAESTROS_USUARIOS_REGISTRO: return URL_MAESTROS_USUARIOS_REGISTRO;

                //Web Services
                //case WEB_WEBSERVICE_FILEUPLOAD: return URL_WEBSERVICE_FILEUPLOAD;
                case WEB_WEBSERVICE_SEGURIDAD: return URL_WEBSERVICE_SEGURIDAD;

                case WEB_WEBSERVICE_ADMINISTRAR_PROYECTO: return URL_WEBSERVICE_ADMINISTRAR_PROYECTO;
                case WEB_WEBSERVICE_ADMINISTRAR_TIPO_ACTIVIDAD: return URL_WEBSERVICE_ADMINISTRAR_TIPO_ACTIVIDAD;
                case WEB_WEBSERVICE_ADMINISTRAR_CLIENTE: return URL_WEBSERVICE_ADMINISTRAR_CLIENTE;
                case WEB_WEBSERVICE_ADMINISTRAR_CONTACTO: return URL_WEBSERVICE_ADMINISTRAR_CONTACTO;
                case WEB_WEBSERVICE_ADMINISTRAR_FACTURACION: return URL_WEBSERVICE_ADMINISTRAR_FACTURACION;

                case WEB_WEBSERVICE_CONTROL_TIEMPO_ACTIVIDAD: return URL_WEBSERVICE_CONTROL_TIEMPO_ACTIVIDAD;

                case WEB_WEBSERVICE_CONTROL_GASTO: return URL_WEBSERVICE_CONTROL_GASTO;

                case WEB_WEBSERVICE_MAESTROS_USUARIO: return URL_WEBSERVICE_MAESTROS_USUARIO;
                case WEB_WEBSERVICE_MAESTROS_MONEDA: return URL_WEBSERVICE_MAESTROS_MONEDA;

                case WEB_WEBSERVICE_REPORTE: return URL_WEBSERVICE_REPORTE;

                default: return "";
            }
        }

        public static string getURLEncodeHTML(string code)
        {
            string vURL = getURL(code);
            return HttpUtility.HtmlEncode((!"".Equals(vURL) ? GetSiteRoot() + vURL : vURL));
        }

        public static string getURLEncodeHTMLSinServer(string code)
        {
            return HttpUtility.HtmlEncode(HttpContext.Current.Request.ApplicationPath + getURL(code));
        }

        public static string GetSiteRoot()
        {
            string port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (port == null || port == "80" || port == "443")
                port = "";
            else
                port = ":" + port;

            string protocol = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (protocol == null || protocol == "0")
                protocol = "http://";
            else
                protocol = "https://";

            string sOut = protocol + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port + HttpContext.Current.Request.ApplicationPath;

            if (sOut.EndsWith("/"))
            {
                sOut = sOut.Substring(0, sOut.Length - 1);
            }

            return sOut;
        }
    }
}