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
    public partial class frmRegistrarActividad : PageValidation
    {

        #region Metodos Publicos

        protected string urlRegistroCliente = UrlManager.getURLEncodeHTML("WEB_ADMINISTRAR_CLIENTES_REGISTRO");
        protected string urlRegistroProyecto = UrlManager.getURLEncodeHTML("WEB_ADMINISTRAR_PROYECTOS_REGISTRO");
        protected string urlRegistroTipoActividad = UrlManager.getURLEncodeHTML("WEB_ADMINISTRAR_ACTIVIDAD_REGISTRO");
        protected string urlRegistroContacto = UrlManager.getURLEncodeHTML("WEB_ADMINISTRAR_CONTACTOS_REGISTRO");

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {
                CargarCombo();

                hfIdUsuario.Value = Session[Constantes.Sesion_IdUsuario] != null ? Session[Constantes.Sesion_IdUsuario].ToString() : "";
                txtUsuario.Value = Session[Constantes.Sesion_NombreUsuario] != null ? Session[Constantes.Sesion_NombreUsuario].ToString() : "";

                if (Request.QueryString["IdActividad"] != null)
                {
                    int IdActividad = Convert.ToInt32(Request.QueryString["IdActividad"]);

                    if (IdActividad == 0)
                    {
                        if (Request.QueryString["startField"] != null && Request.QueryString["endField"] != null)
                        {
                            string[] strFechaInicio = Request.QueryString["startField"].ToString().Split(' ');
                            string[] strFechaFin = Request.QueryString["endField"].ToString().Split(' ');

                            txtFechaInicio.Value = strFechaInicio[0];
                            txtFechaFin.Value = strFechaFin[0];

                            txtHoraInicio.Value = strFechaInicio[1];
                            txtHoraFin.Value = strFechaFin[1];
                        }
                        return;
                    }

                    using (ActividadBL objActividadBL = new ActividadBL())
                    {
                        CargarActividad(objActividadBL.ObtenerActividad(Convert.ToInt32(Request.QueryString["IdActividad"])));
                    }
                }
            }
        }

        #endregion

        #region Metodos Privados

        protected void CargarCombo()
        {
            using (ClienteBL objClienteBL = new ClienteBL())
            {
                CompletarCombos(slCliente, objClienteBL.ListarClientes(), "IdCliente", "NombreCompleto", EnumeradoresBE.enumTipoCombo.Seleccionar);
            }

            CompletarCombos(slProyecto, new List<ProyectoBE>(), "IdProyecto", "NombreProyecto", EnumeradoresBE.enumTipoCombo.Seleccionar);

            using (TipoActividadBL objTipoActividadBL = new TipoActividadBL())
            {
                CompletarCombos(slTipoActividad, objTipoActividadBL.ListarTipoActividades(), "IdTipoActividad", "Nombre", EnumeradoresBE.enumTipoCombo.Seleccionar);
            }

            CompletarCombos(slContacto, new List<ContactoBE>(), "IdContacto", "Nombre", EnumeradoresBE.enumTipoCombo.Seleccionar);
        }

        protected void CargarActividad(ActividadBE objActividadBE)
        {
            hfIdActividad.Value = (objActividadBE.IdActividad > 0) ? objActividadBE.IdActividad.ToString() : "";
            hfIdUsuario.Value = (objActividadBE.Usuario.IdUsuario > 0) ? objActividadBE.Usuario.IdUsuario.ToString() : "";
            txtUsuario.Value = !string.IsNullOrEmpty(objActividadBE.Usuario.NombreCompleto) ? objActividadBE.Usuario.NombreCompleto : "";
            slCliente.Value = (objActividadBE.Cliente.IdCliente > 0) ? objActividadBE.Cliente.IdCliente.ToString() : "";

            if (objActividadBE.Cliente.IdCliente > 0)
            {
                using (ProyectoBL objProyectoBL = new ProyectoBL())
                {
                    CompletarCombos(slProyecto, objProyectoBL.ListarProyectosCliente(objActividadBE.Cliente.IdCliente), "IdProyecto", "NombreProyecto", EnumeradoresBE.enumTipoCombo.Seleccionar);
                    slProyecto.Value = (objActividadBE.Proyecto.IdProyecto > 0) ? objActividadBE.Proyecto.IdProyecto.ToString() : "";
                }

                using (ContactoBL objContactoBL = new ContactoBL())
                {
                    CompletarCombos(slContacto, objContactoBL.ListarContactos(objActividadBE.Cliente.IdCliente), "IdContacto", "Nombre", EnumeradoresBE.enumTipoCombo.Seleccionar);
                    slContacto.Value = (objActividadBE.Contacto.IdContacto > 0) ? objActividadBE.Contacto.IdContacto.ToString() : "";
                }
            }

            slTipoActividad.Value = (objActividadBE.TipoActividad.IdTipoActividad > 0) ? objActividadBE.TipoActividad.IdTipoActividad.ToString() : "";
            taGlosa.Value = !string.IsNullOrEmpty(objActividadBE.Glosa) ? objActividadBE.Glosa : "";
            txtFechaInicio.Value = (objActividadBE.FechaInicio != null) ? objActividadBE.FechaInicio.Value.ToString("dd/MM/yyyy") : "";
            txtHoraInicio.Value = (objActividadBE.FechaInicio != null) ? objActividadBE.FechaInicio.Value.ToString("HH:mm") : "";
            txtFechaFin.Value = (objActividadBE.FechaFin != null) ? objActividadBE.FechaFin.Value.ToString("dd/MM/yyyy") : "";
            txtHoraFin.Value = (objActividadBE.FechaFin != null) ? objActividadBE.FechaFin.Value.ToString("HH:mm") : "";

            if (objActividadBE.Facturable)
                cbFacturable.Checked = true;
            else
                cbFacturable.Checked = false;

        }

        #endregion
    }
}