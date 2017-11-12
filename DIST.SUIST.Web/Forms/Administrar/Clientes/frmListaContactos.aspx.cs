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
    public partial class frmListaContactos : PageValidation
    {
        #region Metodos Publicos

        protected string wsMantenimientoContacto = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_ADMINISTRAR_CONTACTO");
        protected string urlRegistroContacto = UrlManager.getURLEncodeHTML("WEB_ADMINISTRAR_CONTACTOS_REGISTRO");

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["idCliente"] != null)
                {
                    hfIdClienteContacto.Value = Convert.ToInt32(Request.QueryString["idCliente"]).ToString();
                }
            }
        }

        #endregion

        #region Metodos Privados

        #endregion
    }
}