using DIST.SUIST.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DIST.SUIST.Web
{
    public partial class frmCambiarContrasenia : PageValidation
    {
        #region Metodos Publicos

        protected string urlWebServiceSeguridad = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_SEGURIDAD");

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {
                UsuarioBE oUsuario = Session[Constantes.USER_SESSION] as UsuarioBE;

                if (oUsuario != null)
                {
                    hfIdUsuario.Value = oUsuario.IdUsuario.ToString();

                    if (!string.IsNullOrEmpty(oUsuario.Contrasenia))
                    {
                        txtConstraseniaActual.Value = oUsuario.Contrasenia;
                        txtConstraseniaActual.Attributes["type"] = "password";
                    }
                }
            }
        }

        #endregion
    }
}