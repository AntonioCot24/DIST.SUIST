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
    public partial class frmRegistroTipoActividades : PageValidation
    {
        #region Metodos Publicos

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["idTipoActividad"] != null)
                {
                    int Id_TipoActividad = Convert.ToInt32(Request.QueryString["idTipoActividad"]);

                    using (TipoActividadBL objTipoActividadBL = new TipoActividadBL())
                    {
                        TipoActividadBE objTipoActividadBE = objTipoActividadBL.ObtenerTipoActividad(Id_TipoActividad);

                        if (objTipoActividadBE != null && objTipoActividadBE.IdTipoActividad != 0)
                        {
                            CargarDatos(objTipoActividadBE);
                        }
                    }
                }
            }
        }

        #endregion

        #region Metodos Privados

        private void CargarDatos(TipoActividadBE objTipoActividad)
        {
            txtNombreTipoActividad.Value = !string.IsNullOrEmpty(objTipoActividad.Nombre) ? objTipoActividad.Nombre : "";
            //hfIdPrecio.Value = (objTipoActividad.Precio.IdPrecio > 0) ? objTipoActividad.Precio.IdPrecio.ToString() : "";
            //txtPrecio.Value = (objTipoActividad.Precio.Monto > 0) ? objTipoActividad.Precio.Monto.ToString() : "";
        }

        #endregion
    }
}