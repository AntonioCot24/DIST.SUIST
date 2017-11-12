using Newtonsoft.Json;
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
    /// Descripción breve de wsUsuario
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsUsuario : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE ListarUsuarios()
        {
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            List<UsuarioBE> lstUsuario = new List<UsuarioBE>();
            List<ListUsuariosBE> lstListUsuariosBE = new List<ListUsuariosBE>();

            try
            {
                using (UsuarioBL objUsuarioBL = new UsuarioBL())
                {
                    lstUsuario = objUsuarioBL.ListarUsuarios();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                vResultado.Mensaje = "Ocurrio un error inesperado";
                goto Termino;
            }

            if (lstUsuario.Count > 0)
            {
                UsuarioBE SUsuario = (UsuarioBE)Session[Constantes.USER_SESSION];

                if (!SUsuario.MasterAdmin)
                {
                    lstUsuario = lstUsuario.Where(u => u.Perfil.IdPerfil != Convert.ToInt32(EnumeradoresBE.enumPerfiles.Administrador)).ToList();
                }

                foreach (UsuarioBE objUsuarioBE in lstUsuario)
                {
                    ListUsuariosBE oListUsuariosBE = new ListUsuariosBE();

                    oListUsuariosBE.col_IdUsuario = objUsuarioBE.IdUsuario != 0 ? objUsuarioBE.IdUsuario : 0;
                    oListUsuariosBE.col_Estado = objUsuarioBE.Estado == true ? 1 : 0;
                    oListUsuariosBE.col_NombreCompleto = !string.IsNullOrEmpty(objUsuarioBE.NombreCompleto) ? objUsuarioBE.NombreCompleto : "";
                    oListUsuariosBE.col_Denominacion = !string.IsNullOrEmpty(objUsuarioBE.Perfil.Denominacion) ? objUsuarioBE.Perfil.Denominacion : "";
                    oListUsuariosBE.col_srtEtado = !string.IsNullOrEmpty(objUsuarioBE.strEstado) ? objUsuarioBE.strEstado : "";

                    lstListUsuariosBE.Add(oListUsuariosBE);
                }

                vResultado.Resultado = "OK";
                vResultado.Listado = JsonConvert.SerializeObject(lstListUsuariosBE, Formatting.Indented);
            }
            else
            {
                vResultado.Mensaje = "No se encontraron registros solicitados";
                vResultado.Listado = JsonConvert.SerializeObject(lstListUsuariosBE, Formatting.Indented);
            }

        Termino:
            return vResultado;
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE GuardarUsuario(UsuarioBE oUsuario)
        {
            string strError = string.Empty;
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            vResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (UsuarioBL objUsuarioBL = new UsuarioBL())
                {
                    string mensajeout;

                    oUsuario.Auditoria = Session[Constantes.Sesion_Auditoria] as AuditoriaBE;

                    if (objUsuarioBL.GuardarUsuario(oUsuario, out mensajeout))
                    {
                        vResultado.Resultado = "OK";
                        vResultado.Mensaje = HttpUtility.HtmlEncode(mensajeout);
                        goto Termino;
                    }
                    else
                    {
                        vResultado.Mensaje = mensajeout;
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

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE EliminarUsuario(int IdUsuario)
        {
            string strError = string.Empty;
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            vResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (UsuarioBL objUsuarioBL = new UsuarioBL())
                {
                    string mensajeout;

                    if (objUsuarioBL.EliminarUsuario(IdUsuario, out mensajeout))
                    {
                        vResultado.Resultado = "OK";
                        vResultado.Mensaje = HttpUtility.HtmlEncode(mensajeout);
                        goto Termino;
                    }
                    else
                    {
                        vResultado.Mensaje = mensajeout;
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
