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
    public partial class frmRegistroProyectos : PageValidation
    {
        #region Metodos Publicos

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {
                CargarCombo();

                if (Request.QueryString["idProyecto"] != null)
                {
                    int Id_Proyecto = Convert.ToInt32(Request.QueryString["idProyecto"]);

                    using (ProyectoBL objProyectoBL = new ProyectoBL())
                    {
                        ProyectoBE objProyectoBE = objProyectoBL.ObtenerProyecto(Id_Proyecto);

                        if (objProyectoBE != null && objProyectoBE.IdProyecto != 0)
                        {
                            CargarDatos(objProyectoBE);
                        }
                    }
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
        }

        private void CargarDatos(ProyectoBE objProyecto)
        {
            slCliente.Value = (objProyecto.Cliente.IdCliente > 0) ? objProyecto.Cliente.IdCliente.ToString() : "";
            txtNombreProyecto.Value = !string.IsNullOrEmpty(objProyecto.NombreProyecto) ? objProyecto.NombreProyecto : "";
            txtPrecio.Value = (objProyecto.Precio > 0) ? objProyecto.Precio.ToString() : "";
        }

        #endregion
    }
}