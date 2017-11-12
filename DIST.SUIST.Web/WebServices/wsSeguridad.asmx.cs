using DIST.SUIST.BE;
using DIST.SUIST.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DIST.SUIST.Web
{
    /// <summary>
    /// Descripción breve de wsSeguridad
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsSeguridad : WebService
    {
        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE LogearUsuario(UsuarioBE objUsuario)
        {
            string strError = string.Empty;
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };

            using (SeguridadBL objSeguridadBL = new SeguridadBL())
            {
                UsuarioBE oUsuario = new UsuarioBE();

                oUsuario = objSeguridadBL.ValidarUsuario(objUsuario);

                if (oUsuario.IdUsuario != 0)
                    goto GetSesion;


                vResultado.Resultado = "ERROR";
                vResultado.Mensaje = "Credenciales ingresadas no son correctas o se encuentran inhabilitadas";

                goto Termino;

                GetSesion:
                using (SeguridadBL oSeguridadBL = new SeguridadBL())
                {
                    Session[Constantes.USER_SESSION] = oUsuario;
                    Session[Constantes.Sesion_IdUsuario] = oUsuario.IdUsuario;
                    Session[Constantes.Sesion_Usuario] = oUsuario.Usuario;
                    Session[Constantes.Sesion_NombreUsuario] = oUsuario.NombreCompleto;
                    Session[Constantes.Sesion_Perfil] = oUsuario.Perfil;
                    Session[Constantes.Sesion_Empresa] = oUsuario.Empresa;
                    Session[Constantes.Sesion_Auditoria] = new AuditoriaBE { Usuario = oUsuario.Usuario };

                    vResultado.Resultado = "OK";
                    vResultado.Mensaje = "Credenciales correctas";

                    goto Termino;
                }
            }
            Termino:
            return vResultado;
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE ActualizarContraseniaUsuario(UsuarioBE oUsuario)
        {
            string strError = string.Empty;
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            vResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un problema inesperado");

            try
            {
                using (SeguridadBL objSeguridadBL = new SeguridadBL())
                {
                    string mensajeout;

                    oUsuario.Auditoria = Session[Constantes.Sesion_Auditoria] as AuditoriaBE;

                    if (objSeguridadBL.ActualizarContraseniaUsuario(oUsuario, out mensajeout))
                    {
                        UsuarioBE objUsuarioBE = Session[Constantes.USER_SESSION] as UsuarioBE;
                        objUsuarioBE.Contrasenia = oUsuario.Contrasenia;
                        Session[Constantes.USER_SESSION] = objUsuarioBE;

                        vResultado.Resultado = "OK";
                        vResultado.Mensaje = mensajeout;
                        goto Termino;
                    }
                    else
                    {
                        vResultado.Mensaje = mensajeout;
                        goto Termino;
                    }
                }
            }
            catch (Exception ex)
            {
                vResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un problema guardando la información.");
                throw ex;
            }

            Termino:
            return vResultado;
        }
    }
}
