using DIST.SUIST.BE;
using DIST.SUIST.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DIST.SUIST.Web
{
    public partial class frmAdministrarTiempos : PageValidation
    {

        #region Metodos Publicos

        protected string wsMantenimientoActividad = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_CONTROL_TIEMPO_ACTIVIDAD");
        protected string wsMantenimientoCliente = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_ADMINISTRAR_CLIENTE");
        protected string wsMantenimientoProyecto = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_ADMINISTRAR_PROYECTO");
        protected string wsMantenimientoTipoActividad = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_ADMINISTRAR_TIPO_ACTIVIDAD");
        protected string wsMantenimientoContacto = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_ADMINISTRAR_CONTACTO");
        protected string urlRegistroActividad = UrlManager.getURLEncodeHTML("WEB_CONTROL_TIEMPO_REGITRO_ACTIVIDAD");

        public List<ActividadBE> lstActividades { get; set; }

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {
                using (ActividadBL objActividadBL = new ActividadBL())
                {
                    int IdUsuario = 0;

                    switch (IdUsuario)
                    {
                        case (int)EnumeradoresBE.enumPerfiles.Administrador:
                        case (int)EnumeradoresBE.enumPerfiles.Jefe:
                        case (int)EnumeradoresBE.enumPerfiles.Secretaria:
                            IdUsuario = 0;
                            break;
                        default:
                            IdUsuario = (int)Session[Constantes.Sesion_IdUsuario];
                            break;
                    }

                    if (Session[Constantes.Session_ListaActividades] != null)
                        lstActividades = (List<ActividadBE>)Session[Constantes.Session_ListaActividades];
                    else
                        Session[Constantes.Session_ListaActividades] = objActividadBL.ListarActividades(IdUsuario);
                }
            }
        }

        #endregion

        #region Metodos Privados

        #endregion
    }
}