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
    public partial class frmReporteClienteHoras : PageValidation
    {

        #region Metodos Publicos

        protected string wsReporte = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_REPORTE");
        protected string frmSeleccionarClienteReporte = UrlManager.getURLEncodeHTML("WEB_REPORTES_FACTURACION_CLIENTE");

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["idCliente"] != null)
                {
                    int Id_Cliente = Convert.ToInt32(Request.QueryString["idCliente"]);
                    hfIdCliente.Value = Id_Cliente.ToString();

                    CargarCombo();
                    EmpresaBE objEmpresa = (EmpresaBE)Session[Constantes.Sesion_Empresa];
                    imgEmpresaLogo.Src = Page.ResolveUrl("~/" + Constantes.Carp_LogosEmpresa + "/" + objEmpresa.LogoDocumentos);

                    using (ClienteBL objClienteBL = new ClienteBL())
                    {
                        ClienteBE objClienteBE = objClienteBL.ObtenerCliente(Id_Cliente);

                        if (objClienteBE != null && objClienteBE.IdCliente != 0)
                        {
                            lblCliente.Text = objClienteBE.NombreCompleto;
                            Session[Constantes.Sesion_NombreClienteReporte] = objClienteBE.NombreCompleto;

                            using (FacturacionBL objFacturacionBL = new FacturacionBL())
                            {
                                CargarTiposMoneda(objFacturacionBL.ObtenerFacturacion(0, Id_Cliente).lstMonedaFacturacion);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Metodos Privados

        protected void CargarCombo()
        {
            using (ProyectoBL objProyectoBL = new ProyectoBL())
            {
                int IdCliente = Convert.ToInt32(hfIdCliente.Value);
                CompletarCombos(slProyecto, objProyectoBL.ListarProyectosCliente(IdCliente), "IdProyecto", "NombreProyecto", EnumeradoresBE.enumTipoCombo.Seleccionar);
            }
        }

        private void CargarTiposMoneda(List<MonedaFacturacionBE> lstMonedaFacturacionBE)
        {
            if (lstMonedaFacturacionBE.Count > 0)
            {
                StringBuilder sbTipoMoneda = new StringBuilder();
                string strTipoCambio = string.Empty;

                sbTipoMoneda.Append("<div class='form-group'><div class='col-sm-12 text-right'>");

                foreach (MonedaFacturacionBE objMonedaFacturacionBE in lstMonedaFacturacionBE)
                {
                    if (objMonedaFacturacionBE.Moneda.TipoCambio)
                    {
                        sbTipoMoneda.Append("<label class='control-label col-sm-2'>" + objMonedaFacturacionBE.Moneda.Descripcion + ": <span class='required'>*</span></label>" +
                                            "<div class='col-sm-4'>" +
                                                "<div class='input-group'>" +
                                                    "<span id = 'btnTipoCambi" + objMonedaFacturacionBE.Moneda.Descripcion + "' class='input-group-addon'>" +
                                                        "<span>" + objMonedaFacturacionBE.Moneda.Signo + "</span>" +
                                                    "</span>" +
                                                    "<input id='txtTipoCambio" + objMonedaFacturacionBE.Moneda.Descripcion + "' type='text' class='form-control validate[required] txtTipoCambio' runat='server' />" +
                                                "</div>" +
                                            "</div>");
                        if (objMonedaFacturacionBE.Moneda.Predeteminado)
                            strTipoCambio += "txtTipoCambio" + objMonedaFacturacionBE.Moneda.Descripcion + "-1|" + objMonedaFacturacionBE.Moneda.Signo + ",";
                        else
                            strTipoCambio += "txtTipoCambio" + objMonedaFacturacionBE.Moneda.Descripcion + "-0|" + objMonedaFacturacionBE.Moneda.Signo + ",";
                    }
                    else
                    {
                        if (objMonedaFacturacionBE.Moneda.Predeteminado)
                            strTipoCambio += "nn-1|" + objMonedaFacturacionBE.Moneda.Signo + ",";
                        else
                            strTipoCambio += "nn-0|" + objMonedaFacturacionBE.Moneda.Signo + ",";
                    }
                }

                sbTipoMoneda.Append("</div></div>");

                hfTiposCambio.Value = strTipoCambio.Substring(0, strTipoCambio.Length - 1);
                ltTipoCambio.Text = sbTipoMoneda.ToString();
            }
        }

        #endregion
    }
}