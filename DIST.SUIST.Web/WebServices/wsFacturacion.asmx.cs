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
    /// Descripción breve de wsFacturacion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsFacturacion : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE GuardarFacturacion(FacturacionBE oFacturacion)
        {
            string strError = string.Empty;
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            vResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (FacturacionBL objFacturacionBL = new FacturacionBL())
                {
                    string mensajeout;

                    oFacturacion.Auditoria = Session[Constantes.Sesion_Auditoria] as AuditoriaBE;

                    if (objFacturacionBL.GuardarFacturacion(oFacturacion, out mensajeout))
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
        public MensajeWrapperBE GuardarMonedaFacturacion(MonedaFacturacionBE oMonedaFacturacionBE)
        {
            string strError = string.Empty;
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            vResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (FacturacionBL objFacturacionBL = new FacturacionBL())
                {
                    string mensajeout;

                    oMonedaFacturacionBE.Auditoria = Session[Constantes.Sesion_Auditoria] as AuditoriaBE;

                    if (objFacturacionBL.GuardarMonedaFacturacion(oMonedaFacturacionBE, out mensajeout))
                    {
                        vResultado.Resultado = "OK";
                        vResultado.Mensaje = HttpUtility.HtmlEncode(mensajeout) + "&" + oMonedaFacturacionBE.IdMonedaFacturacion;
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
        public MensajeWrapperBE EliminarMonedaFacturacion(int IdMonedaFacturacion)
        {
            string strError = string.Empty;
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            vResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (FacturacionBL objFacturacionBL = new FacturacionBL())
                {
                    string mensajeout;

                    if (objFacturacionBL.EliminarMonedaFacturacion(IdMonedaFacturacion, out mensajeout))
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
                vResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un problema eliminando la información.");
                throw ex;
            }

            Termino:
            return vResultado;
        }

        [WebMethod(EnableSession = true)]
        public List<MonedaFacturacionBE> ListarMonedaFacturacion(int IdFacturacion)
        {
            List<MonedaFacturacionBE> lstMonedaFacturacionBE = new List<MonedaFacturacionBE>();

            try
            {
                using (FacturacionBL objFacturacionBL = new FacturacionBL())
                {
                    lstMonedaFacturacionBE = objFacturacionBL.ListarMonedaFacturacion(IdFacturacion);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<MonedaFacturacionBE>();
            }

            return lstMonedaFacturacionBE;
        }
    }
}
