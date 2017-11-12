using DIST.SUIST.BE;
using DIST.SUIST.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DIST.SUIST.Web
{
    public partial class frmAdministrarGastos : PageValidation
    {
        #region Metodos Publicos

        protected string wsControlGasto = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_CONTROL_GASTO");
        protected string wsAdministrarProyecto = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_ADMINISTRAR_PROYECTO");
        protected string urlRegistroGasto = UrlManager.getURLEncodeHTML("WEB_CONTROL_GASTO_REGITRO");

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