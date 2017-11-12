using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DIST.SUIST.Web
{
    public partial class frmAdministrarClientes : PageValidation
    {
        #region Metodos Publicos

        protected string wsMantenimientoCliente = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_ADMINISTRAR_CLIENTE");
        protected string urlRegistroCliente = UrlManager.getURLEncodeHTML("WEB_ADMINISTRAR_CLIENTES_REGISTRO");
        protected string urlListarContactos = UrlManager.getURLEncodeHTML("WEB_ADMINISTRAR_CONTACTOS_LISTA");
        protected string urlRegistroFactura = UrlManager.getURLEncodeHTML("WEB_ADMINISTRAR_FACTURACION_REGISTRO");

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