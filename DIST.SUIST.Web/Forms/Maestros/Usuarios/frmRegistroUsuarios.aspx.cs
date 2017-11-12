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
    public partial class frmRegistroUsuarios : PageValidation
    {
        #region Metodos Publicos

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {
                CargarCombo();

                if (Request.QueryString["idUsuario"] != null)
                {
                    int Id_Usuario = Convert.ToInt32(Request.QueryString["idUsuario"]);

                    using (UsuarioBL objUsuarioBL = new UsuarioBL())
                    {
                        UsuarioBE objUsuarioBE = objUsuarioBL.ObtenerUsuario(Id_Usuario);

                        if (objUsuarioBE != null && objUsuarioBE.IdUsuario != 0)
                        {
                            CargarDatos(objUsuarioBE);
                        }
                    }
                }
            }
        }

        #endregion

        #region Metodos Privados
        
        void CargarCombo()
        {
            using (PerfilBL objPerfilBL = new PerfilBL())
            {
                UsuarioBE SUsuario = (UsuarioBE)Session[Constantes.USER_SESSION];
                List<PerfilBE> lstPerfiles = objPerfilBL.ListarPerfiles();

                if (!SUsuario.MasterAdmin)
                {
                    lstPerfiles = lstPerfiles.Where(p => p.IdPerfil != Convert.ToInt32(EnumeradoresBE.enumPerfiles.Administrador)).ToList();
                }

                CompletarCombos(slPerfil, lstPerfiles, "IdPerfil", "Denominacion", EnumeradoresBE.enumTipoCombo.Seleccionar);
            }
        }


        private void CargarDatos(UsuarioBE objUsuario)
        {
            txtNombreUsuario.Value = !string.IsNullOrEmpty(objUsuario.NombreCompleto) ? objUsuario.NombreCompleto : "";
            slPerfil.Value = objUsuario.Perfil.IdPerfil > 0 ? objUsuario.Perfil.IdPerfil.ToString() : string.Empty;
            txtEmail.Value = !string.IsNullOrEmpty(objUsuario.Usuario) ? objUsuario.Usuario : "";
            txtContrasenia.Value = !string.IsNullOrEmpty(objUsuario.Contrasenia) ? objUsuario.Contrasenia : "";
        }

        #endregion
    }
}