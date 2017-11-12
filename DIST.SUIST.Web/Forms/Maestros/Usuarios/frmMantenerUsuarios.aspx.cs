using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DIST.SUIST.Web
{
    public partial class frmMantenerUsuarios : PageValidation
    {
        #region Metodos Publicos

        protected string wsMantenimientoUsuario = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_MAESTROS_USUARIO");
        protected string urlRegistroUsuario = UrlManager.getURLEncodeHTML("WEB_MAESTROS_USUARIOS_REGISTRO");

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