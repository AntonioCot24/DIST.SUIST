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
    public partial class frmRegistrarFacturacion : PageValidation
    {
        #region Metodos Publicos

        protected string wsAdministrarFacturacion = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_ADMINISTRAR_FACTURACION");
        protected string wsMantenimientoMoneda = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_MAESTROS_MONEDA");

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["idFacturacion"] != null || Request.QueryString["idCliente"] != null)
                {
                    int Id_Facturacion = Convert.ToInt32(Request.QueryString["idFacturacion"]);
                    int Id_Cliente = Convert.ToInt32(Request.QueryString["idCliente"]);

                    CargarCombo(Id_Cliente);

                    using (FacturacionBL objFacturacionBL = new FacturacionBL())
                    {
                        FacturacionBE objFacturacionBE = objFacturacionBL.ObtenerFacturacion(Id_Facturacion, Id_Cliente);

                        if (objFacturacionBE != null && objFacturacionBE.IdFacturacion != 0)
                            CargarDatos(objFacturacionBE);      
                        else
                        {
                            string mensajeFact;
                            FacturacionBE oFacturacionBE = new FacturacionBE
                            {
                                Cliente = new ClienteBE { IdCliente = Id_Cliente },
                                Contacto = new ContactoBE(),
                                Auditoria = Session[Constantes.Sesion_Auditoria] as AuditoriaBE
                            };

                            if (!objFacturacionBL.GuardarFacturacion(oFacturacionBE, out mensajeFact))
                            {
                                hfIdFacturacion.Value = oFacturacionBE.ToString();
                            }
                        }                  
                    }
                }
            }
        }

        #endregion

        #region Metodos Privados

        void CargarCombo(int IdCliente)
        {
            using (MonedaBL objMonedaBL = new MonedaBL())
            {
                CompletarCombos(slTipoMoneda, objMonedaBL.ListarMonedas(), "IdMoneda", "Descripcion", EnumeradoresBE.enumTipoCombo.Seleccionar);
            }

            using (ContactoBL objContactoBL = new ContactoBL())
            {
                CompletarCombos(slContactoFactura, objContactoBL.ListarContactos(IdCliente), "IdContacto", "Nombre", EnumeradoresBE.enumTipoCombo.Seleccionar);
            }
        }

        private void CargarDatos(FacturacionBE objFacturacion)
        {
            hfIdFacturacion.Value = objFacturacion.IdFacturacion.ToString();

            switch (objFacturacion.TipoFacturacion)
            {
                case 1:
                    rbTarifaPlana.Checked = true;
                    txtMontoTarifaPlana.Value = objFacturacion.TarifaFija.ToString();
                    break;
                case 2:
                    rbTarifaHoras.Checked = true;
                    txtMontoTarifaHoras.Value = objFacturacion.TarifaHoras.ToString();
                    break;
                case 3:
                    rbMixto.Checked = true;
                    txtMontoTarifaMixta.Value = objFacturacion.TarifaFija.ToString();
                    txtMontoTarifaHora.Value = objFacturacion.TarifaHoras.ToString();
                    txtMontoAdicional.Value = objFacturacion.TarifaHorasAdicionales.ToString();
                    break;
                default:
                    rbTarifaPlana.Checked = true;
                    txtMontoTarifaPlana.Value = objFacturacion.TarifaFija.ToString();
                    break;
            }

            txtMontoFlat.Value = objFacturacion.MontoFlat > 0 ? objFacturacion.MontoFlat.ToString() : "";
            txtFechaFacturacion.Value = objFacturacion.FechaFactura > 0 ? objFacturacion.FechaFactura.ToString() : "";
            txtDireccionFactura.Value = !string.IsNullOrEmpty(objFacturacion.Direccion) ? objFacturacion.Direccion : "";
            slContactoFactura.Value = objFacturacion.Contacto.IdContacto > 0 ? objFacturacion.Contacto.IdContacto.ToString() : "";
        }

        #endregion
    }
}