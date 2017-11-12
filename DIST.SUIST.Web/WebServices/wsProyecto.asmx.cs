using Newtonsoft.Json;
using DIST.SUIST.BE;
using DIST.SUIST.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DIST.SUIST.Web
{
    /// <summary>
    /// Descripción breve de wsProyecto
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsProyecto : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE ListarProyecto()
        {
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            List<ProyectoBE> lstProyecto = new List<ProyectoBE>();
            List<ListProyectosBE> lstListProyectosBE = new List<ListProyectosBE>();

            try
            {
                using (ProyectoBL objProyectoBL = new ProyectoBL())
                {
                    lstProyecto = objProyectoBL.ListarProyectos();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                vResultado.Mensaje = "Ocurrio un error inesperado";
                goto Termino;
            }

            if (lstProyecto.Count > 0)
            {
                foreach (ProyectoBE objProyectoBE in lstProyecto)
                {
                    ListProyectosBE oListProyectosBE = new ListProyectosBE();

                    oListProyectosBE.col_IdProyecto = objProyectoBE.IdProyecto != 0 ? objProyectoBE.IdProyecto : 0;
                    oListProyectosBE.col_Cliente = !string.IsNullOrEmpty(objProyectoBE.Cliente.NombreCompleto) ? objProyectoBE.Cliente.NombreCompleto : "";
                    oListProyectosBE.col_NombreProyecto = !string.IsNullOrEmpty(objProyectoBE.NombreProyecto) ? objProyectoBE.NombreProyecto : "";
                    oListProyectosBE.col_Precio = objProyectoBE.Precio != 0 ? objProyectoBE.Precio.ToString() : "";

                    lstListProyectosBE.Add(oListProyectosBE);
                }

                vResultado.Resultado = "OK";
                vResultado.Listado = JsonConvert.SerializeObject(lstListProyectosBE, Formatting.Indented);
            }
            else
            {
                vResultado.Mensaje = "No se encontraron registros solicitados";
                vResultado.Listado = JsonConvert.SerializeObject(lstListProyectosBE, Formatting.Indented);
            }

            Termino:
            return vResultado;
        }

        [WebMethod(EnableSession = true)]
        public List<ProyectoBE> ListarProyectoCliente(int idCliente)
        {
            List<ProyectoBE> lstProyecto = new List<ProyectoBE>();
            
            try
            {
                using (ProyectoBL objProyectoBL = new ProyectoBL())
                {
                    return objProyectoBL.ListarProyectosCliente(idCliente);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<ProyectoBE>();
            }
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE ExportarProyecto()
        {
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            List<ProyectoBE> lstProyecto = new List<ProyectoBE>();
            List<ListProyectosBE> lstListProyectosBE = new List<ListProyectosBE>();

            try
            {
                using (ProyectoBL objProyectoBL = new ProyectoBL())
                {
                    lstProyecto = objProyectoBL.ListarProyectos();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                vResultado.Mensaje = "Ocurrio un error inesperado";
                goto Termino;
            }

            if (lstProyecto.Count > 0)
            {
                int cont = 1;

                foreach (ProyectoBE objProyectoBE in lstProyecto)
                {
                    ListProyectosBE oListProyectosBE = new ListProyectosBE();

                    oListProyectosBE.Nro = cont;
                    oListProyectosBE.col_IdProyecto = objProyectoBE.IdProyecto != 0 ? objProyectoBE.IdProyecto : 0;
                    oListProyectosBE.col_Cliente = !string.IsNullOrEmpty(objProyectoBE.Cliente.NombreCompleto) ? objProyectoBE.Cliente.NombreCompleto : "";
                    oListProyectosBE.col_NombreProyecto = !string.IsNullOrEmpty(objProyectoBE.NombreProyecto) ? objProyectoBE.NombreProyecto : "";
                    oListProyectosBE.col_Precio = objProyectoBE.Precio != 0 ? objProyectoBE.Precio.ToString() : "";

                    lstListProyectosBE.Add(oListProyectosBE);
                    cont++;
                }

                vResultado.Resultado = "OK";

                DataTable dtProyectos = Globales.ToDataTable(lstListProyectosBE);

                //Crear cabecera
                dtProyectos.DefaultView.Sort = "Nro ASC";
                dtProyectos.Columns["Nro"].ColumnName = "Nº";
                dtProyectos.Columns.Remove("col_IdProyecto");
                dtProyectos.Columns["col_Cliente"].ColumnName = "Cliente";
                dtProyectos.Columns["col_NombreProyecto"].ColumnName = "Nombre";
                dtProyectos.Columns["col_Precio"].ColumnName = "Precio";

                Session[Constantes.Sesion_DtExcel] = dtProyectos;
            }
            else
            {
                vResultado.Mensaje = "No se encontraron registros solicitados";
                vResultado.Listado = JsonConvert.SerializeObject(lstListProyectosBE, Formatting.Indented);
            }

            Termino:
            return vResultado;
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE GuardarProyecto(ProyectoBE oProyecto)
        {
            string strError = string.Empty;
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            vResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (ProyectoBL objProyectoBL = new ProyectoBL())
                {
                    string mensajeout;

                    oProyecto.Auditoria = Session[Constantes.Sesion_Auditoria] as AuditoriaBE;

                    if (objProyectoBL.GuardarProyecto(oProyecto, out mensajeout))
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
        public MensajeWrapperBE EliminarProyecto(int IdProyecto)
        {
            string strError = string.Empty;
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            vResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (ProyectoBL objProyectoBL = new ProyectoBL())
                {
                    string mensajeout;

                    if (objProyectoBL.EliminarProyecto(IdProyecto, out mensajeout))
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
