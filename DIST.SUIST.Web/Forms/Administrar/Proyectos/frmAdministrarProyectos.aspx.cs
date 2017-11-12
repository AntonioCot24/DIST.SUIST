using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DIST.SUIST.Web
{
    public partial class frmAdministrarProyectos : PageValidation
    {
        #region Metodos Publicos

        protected string wsMantenimientoProyecto = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_ADMINISTRAR_PROYECTO");
        protected string urlRegistroProyecto = UrlManager.getURLEncodeHTML("WEB_ADMINISTRAR_PROYECTOS_REGISTRO");
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