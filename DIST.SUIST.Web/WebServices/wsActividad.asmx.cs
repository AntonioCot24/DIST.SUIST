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
    /// Descripción breve de wsActividad
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsActividad : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public List<EventoBE> ListarEventos()
        {
            List<ActividadBE> lstActividad = new List<ActividadBE>();
            List<EventoBE> lstEventos = new List<EventoBE>();

            try
            {
                using (ActividadBL objActividadBL = new ActividadBL())
                {
                    PerfilBE perfil = Session[Constantes.Sesion_Perfil] as PerfilBE;
                    int IdUsuario = (int)Session[Constantes.Sesion_IdUsuario]; 

                    switch (perfil.IdPerfil)
                    {
                        case (int)EnumeradoresBE.enumPerfiles.Administrador:
                        case (int)EnumeradoresBE.enumPerfiles.Jefe:
                        case (int)EnumeradoresBE.enumPerfiles.Secretaria:
                            IdUsuario = 0;
                            break;
                    }

                    lstActividad = objActividadBL.ListarActividades(IdUsuario);
                    Session[Constantes.Session_ListaActividades] = lstActividad;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (lstActividad.Count > 0)
            {
                foreach (ActividadBE objActividadBE in lstActividad)
                {
                    EventoBE objEventoBE = new EventoBE
                    {
                        id = objActividadBE.IdActividad,
                        start = objActividadBE.FechaInicio.Value,
                        end = objActividadBE.FechaFin.Value,
                        title = objActividadBE.Cliente.NombreCompleto,
                        description = objActividadBE.TipoActividad.Nombre
                    };

                    lstEventos.Add(objEventoBE);
                }
            }

            return lstEventos;
        }

        [WebMethod(EnableSession = true)]
        public List<RecursosEventoBE> ListarRecursos()
        {
            List<ClienteBE> lstClienteBE = new List<ClienteBE>();
            List<RecursosEventoBE> lstEventos = new List<RecursosEventoBE>();

            try
            {
                using (ClienteBL objClienteBL = new ClienteBL())
                {
                    lstClienteBE = objClienteBL.ListarClientes();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (lstClienteBE.Count > 0)
            {
                int cont = 1;
                foreach (ClienteBE objClienteBE in lstClienteBE)
                {
                    RecursosEventoBE objRecursosEventoBE = new RecursosEventoBE
                    {
                        value = cont,
                        text = objClienteBE.NombreCompleto,
                        color = string.IsNullOrEmpty(objClienteBE.Color) ? "#318A39" : objClienteBE.Color
                    };

                    lstEventos.Add(objRecursosEventoBE);
                    cont++;
                }
            }

            return lstEventos;
        }

        [WebMethod(EnableSession = true)]
        public double ObtenerMontoContrato(int idCliente)
        {
            FacturacionBE objFacturacionBE = new FacturacionBE();
            if (idCliente.Equals(0))
                goto Retorno;

            try
            {
                using (FacturacionBL objFacturacionBL = new FacturacionBL())
                {
                    objFacturacionBE = objFacturacionBL.ObtenerFacturacion(0, idCliente);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Retorno:
            return objFacturacionBE.TarifaHoras;
        }

        [WebMethod(EnableSession = true)]
        public double ObtenerMontoActividad(int idTipoActividad)
        {
            PrecioBE objPrecioBE = new PrecioBE();
            if (idTipoActividad.Equals(0))
                goto Retorno;

            try
            {
                using (PrecioBL objPrecioBL = new PrecioBL())
                {
                    objPrecioBE = objPrecioBL.ObtenerPrecio(new PrecioBE
                    {
                        Cliente = new ClienteBE(),
                        TipoActividad = new TipoActividadBE { IdTipoActividad = idTipoActividad }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Retorno:
            return objPrecioBE.Monto; ;
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE GuardarActividad(ActividadBE oActividad)
        {
            string strError = string.Empty;
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            vResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (ActividadBL objActividadBL = new ActividadBL())
                {
                    string mensajeout;

                    oActividad.Auditoria = Session[Constantes.Sesion_Auditoria] as AuditoriaBE;

                    if (objActividadBL.GuardarActividad(oActividad, out mensajeout))
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
        public MensajeWrapperBE EliminarActividad(int IdActividad)
        {
            string strError = string.Empty;
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            vResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (ActividadBL objActividadBL = new ActividadBL())
                {
                    string mensajeout;

                    if (objActividadBL.EliminarActividad(IdActividad, out mensajeout))
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
