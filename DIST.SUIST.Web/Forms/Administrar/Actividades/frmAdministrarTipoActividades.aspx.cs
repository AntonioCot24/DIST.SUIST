using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DIST.SUIST.Web
{
    public partial class frmAdministrarTipoActividades : PageValidation
    {
        #region Metodos Publicos

        protected string wsMantenimientoTipoActividad = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_ADMINISTRAR_TIPO_ACTIVIDAD");
        protected string urlRegistroTipoActividad = UrlManager.getURLEncodeHTML("WEB_ADMINISTRAR_ACTIVIDAD_REGISTRO");
        protected string urlMainMantenimientos = UrlManager.getURLEncodeHTML("WEB_ADMINISTRAR");

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {

            }
        }

        #endregion

        #region Metodos Privados
        
        #endregion
    }
}