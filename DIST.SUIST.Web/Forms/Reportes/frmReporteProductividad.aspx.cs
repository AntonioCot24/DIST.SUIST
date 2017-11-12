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
    public partial class frmReporteProductividad : PageValidation
    {
        #region Metodos Publicos

        protected string wsReporte = UrlManager.getURLEncodeHTML("WEB_WEBSERVICE_REPORTE");

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {
                CargarCombo();
            }
        }

        #endregion

        #region Metodos Privados

        void CargarCombo()
        {
            using (UsuarioBL objUsuarioBL = new UsuarioBL())
            {
                CompletarCombos(slUsuario, objUsuarioBL.ListarUsuarios(), "IdUsuario", "NombreCompleto", EnumeradoresBE.enumTipoCombo.Seleccionar);
            }

            using (ClienteBL objClienteBL = new ClienteBL())
            {
                CompletarCombos(slCliente, objClienteBL.ListarClientes(), "IdCliente", "NombreCompleto", EnumeradoresBE.enumTipoCombo.Seleccionar);
            }
            
            using (TipoActividadBL objTipoActividadBL = new TipoActividadBL())
            {
                CompletarCombos(slTipoActividad, objTipoActividadBL.ListarTipoActividades(), "IdTipoActividad", "Nombre", EnumeradoresBE.enumTipoCombo.Seleccionar);
            }
        }

        #endregion
    }
}