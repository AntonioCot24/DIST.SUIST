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
    public partial class frmSeleccionarClienteReporte : PageValidation
    {

        #region Metodos Publicos

        protected string frmReporteClienteHoras = UrlManager.getURLEncodeHTML("WEB_REPORTES_CLIENTE_HORAS");

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {
                CargarClientes();
            }
        }

        #endregion

        #region Metodos Privados

        private void CargarClientes()
        {
            StringBuilder sbClientes = new StringBuilder();

            using (ClienteBL objClienteBL = new ClienteBL())
            {
                foreach (ClienteBE oCliente in objClienteBL.ListarClientes().OrderBy(c => c.NombreCompleto))
                {
                    string styleColor = string.Empty;

                    if (!string.IsNullOrEmpty(oCliente.Color))
                    {
                        styleColor = "style='background-color: " + oCliente.Color +"; border-color: #525252;'";
                    }
                    sbClientes.Append("<li>" +
                                        "<a idCliente=" + oCliente.IdCliente + " class='btn btn-lg btn-primary btnSelectClient' " + styleColor + " >" +
                                            "<i class='fa fa-user fa-3x pull-left'></i><span>" + oCliente.NombreCompleto + "</span>" +
                                        "</a>" +
                                      "</li>");
                }
            }

            ltClientes.Text = sbClientes.ToString();
        }

        #endregion
    }
}