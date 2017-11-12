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
    public partial class frmRegistrarClientes : PageValidation
    {
        #region Metodos Publicos

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {
                CargarCombo();

                if (Request.QueryString["idCliente"] != null)
                {
                    hfOpcionCliente.Value = "E";
                    int Id_Cliente = Convert.ToInt32(Request.QueryString["idCliente"]);

                    using (ClienteBL objClienteBL = new ClienteBL())
                    {
                        ClienteBE objClienteBE = objClienteBL.ObtenerCliente(Id_Cliente);

                        if (objClienteBE != null && objClienteBE.IdCliente != 0)
                        {
                            CargarDatos(objClienteBE);
                        }
                    }
                }
                else
                    hfOpcionCliente.Value = "N";
            }
        }

        #endregion

        #region Metodos Privados

        void CargarCombo()
        {
            using (ClienteBL objClienteBL = new ClienteBL())
            {
                CompletarCombos(slTipoCliente, objClienteBL.ListarTipoClientes(), "IdTipoCliente", "Descripcion", EnumeradoresBE.enumTipoCombo.Seleccionar);
            }
        }

        private void CargarDatos(ClienteBE objClienteBE)
        {
            slTipoCliente.Value = (objClienteBE.TipoCliente.IdTipoCliente > 0) ? objClienteBE.TipoCliente.IdTipoCliente.ToString() : "";
            txtDocumentoIdentidad.Value = !string.IsNullOrEmpty(objClienteBE.DocumentoIdentidad) ? objClienteBE.DocumentoIdentidad : "";
            txtNombreCliente.Value = !string.IsNullOrEmpty(objClienteBE.NombreCompleto) ? objClienteBE.NombreCompleto : "";
            txtEmail.Value = !string.IsNullOrEmpty(objClienteBE.Email) ? objClienteBE.Email : "";
            txtTelefono.Value = !string.IsNullOrEmpty(objClienteBE.Telefono) ? objClienteBE.Telefono : "";
            txtSitioWeb.Value = !string.IsNullOrEmpty(objClienteBE.SitioWeb) ? objClienteBE.SitioWeb : "";
            txtDireccion.Value = !string.IsNullOrEmpty(objClienteBE.Direccion) ? objClienteBE.Direccion : "";
            txtInicioContrato.Value = (objClienteBE.FechaInicioContrato != null) ? objClienteBE.FechaInicioContrato.Value.ToString("dd/MM/yyy") : "";
            txtFinContrato.Value = (objClienteBE.FechaFinContrato != null) ? objClienteBE.FechaFinContrato.Value.ToString("dd/MM/yyy") : "";
            txtColor.Value = !string.IsNullOrEmpty(objClienteBE.Color) ? objClienteBE.Color : "";
        }

        #endregion
    }
}