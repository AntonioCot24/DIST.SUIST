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
    public partial class frmRegistrarContactos : PageValidation
    {
        #region Metodos Publicos

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["idContacto"] != null)
                {
                    hfOpcionContacto.Value = "E";
                    int Id_Contacto = Convert.ToInt32(Request.QueryString["idContacto"]);

                    using (ContactoBL objContactoBL = new ContactoBL())
                    {
                        ContactoBE objContactoBE = objContactoBL.ObtenerContacto(Id_Contacto);

                        if (objContactoBE != null && objContactoBE.IdContacto != 0)
                        {
                            CargarDatos(objContactoBE);
                        }
                    }
                }
                else
                    hfOpcionContacto.Value = "N";
            }
        }

        #endregion

        #region Metodos Privados

        private void CargarDatos(ContactoBE objContacto)
        {
            txtNombreContacto.Value = !string.IsNullOrEmpty(objContacto.Nombre) ? objContacto.Nombre : "";
            txtDireccionContacto.Value = !string.IsNullOrEmpty(objContacto.Direccion) ? objContacto.Direccion : string.Empty;
            txtTelfFijoContacto.Value = !string.IsNullOrEmpty(objContacto.TelefonoFijo) ? objContacto.TelefonoFijo : "";
            txtTelfCelularContacto.Value = !string.IsNullOrEmpty(objContacto.TelefonoCelular) ? objContacto.TelefonoCelular : "";
            txtCorreoContacto.Value = !string.IsNullOrEmpty(objContacto.Correo) ? objContacto.Correo : "";
            txtCargoContacto.Value = !string.IsNullOrEmpty(objContacto.Cargo) ? objContacto.Cargo : "";
            if (objContacto.Principal) rbSiPrincial.Checked = true; else rbNoPrincial.Checked = true;
        }

        #endregion
    }
}