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
    public partial class frmRegistrarGasto : PageValidation
    {
        #region Metodos Publicos

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {
                CargarCombo();

                if (Request.QueryString["idGasto"] != null)
                {
                    int Id_Gasto = Convert.ToInt32(Request.QueryString["idGasto"]);

                    using (GastoBL objGastoBL = new GastoBL())
                    {
                        GastoBE objGastoBE = objGastoBL.ObtenerGasto(Id_Gasto);

                        if (objGastoBE != null && objGastoBE.IdGasto != 0)
                        {
                            CargarDatos(objGastoBE);
                        }
                    }
                }
                else
                {
                    UsuarioBE objUsuario = (UsuarioBE)Session[Constantes.USER_SESSION];
                    hfIdUsuario.Value = objUsuario.IdUsuario.ToString();
                    txtAbogado.Value = objUsuario.NombreCompleto;
                }
            }
        }

        #endregion

        #region Metodos Privados

        void CargarCombo()
        {
            using (ClienteBL objClienteBL = new ClienteBL())
            {
                CompletarCombos(slCliente, objClienteBL.ListarClientes(), "IdCliente", "NombreCompleto", EnumeradoresBE.enumTipoCombo.Seleccionar);
            }

            CompletarCombos(slProyecto, new List<ProyectoBE>(), "IdProyecto", "NombreProyecto", EnumeradoresBE.enumTipoCombo.Seleccionar);
        }

        private void CargarDatos(GastoBE objGasto)
        {
            hfIdGasto.Value = (objGasto.IdGasto != 0) ? objGasto.IdGasto.ToString() : string.Empty;
            hfIdUsuario.Value = (objGasto.IdUsuario != 0) ? objGasto.IdUsuario.ToString() : string.Empty;
            txtAbogado.Value = !string.IsNullOrEmpty(objGasto.NombreAbogado) ? objGasto.NombreAbogado : string.Empty;
            slCliente.Value = (objGasto.IdCliente != 0) ? objGasto.IdCliente.ToString() : string.Empty;

            if (objGasto.IdCliente > 0)
            {
                using (ProyectoBL objProyectoBL = new ProyectoBL())
                {
                    CompletarCombos(slProyecto, objProyectoBL.ListarProyectosCliente(objGasto.IdCliente), "IdProyecto", "NombreProyecto", EnumeradoresBE.enumTipoCombo.Seleccionar);
                    slProyecto.Value = (objGasto.IdProyecto != 0) ? objGasto.IdProyecto.ToString() : "";
                }
            }

            txtFechaGasto.Value = (objGasto.Fecha != null) ? objGasto.Fecha.Value.ToString("dd/MM/yyyy") : string.Empty;
            txtMonto.Value = (objGasto.Monto != 0) ? objGasto.Monto.ToString() : string.Empty;
            taGlosa.Value = !string.IsNullOrEmpty(objGasto.Glosa) ? objGasto.Glosa : string.Empty;
        }

        #endregion
    }
}