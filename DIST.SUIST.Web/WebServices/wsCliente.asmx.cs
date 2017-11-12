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
    /// Descripción breve de wsCliente
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsCliente : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE ListarCliente()
        {
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            List<ClienteBE> lstCliente = new List<ClienteBE>();
            List<ListClientesBE> lstListClientesBE = new List<ListClientesBE>();

            try
            {
                using (ClienteBL objClienteBL = new ClienteBL())
                {
                    lstCliente = objClienteBL.ListarClientes();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                vResultado.Mensaje = "Ocurrio un error inesperado";
                goto Termino;
            }

            if (lstCliente.Count > 0)
            {
                foreach (ClienteBE objClienteBE in lstCliente)
                {
                    ListClientesBE oListClientesBE = new ListClientesBE();

                    oListClientesBE.col_IdCliente = objClienteBE.IdCliente != 0 ? objClienteBE.IdCliente : 0;
                    oListClientesBE.col_Descripcion = !string.IsNullOrEmpty(objClienteBE.TipoCliente.Descripcion) ? objClienteBE.TipoCliente.Descripcion : "";
                    oListClientesBE.col_DocumentoIdentidad = !string.IsNullOrEmpty(objClienteBE.DocumentoIdentidad) ? objClienteBE.DocumentoIdentidad : "";
                    oListClientesBE.col_NombreCompleto = !string.IsNullOrEmpty(objClienteBE.NombreCompleto) ? objClienteBE.NombreCompleto : "";
                    oListClientesBE.col_FechaInicioContrato = (objClienteBE.FechaInicioContrato != null) ? objClienteBE.FechaInicioContrato.Value.ToString("dd/MM/yyyy") : "";
                    oListClientesBE.col_FechaFinContrato = (objClienteBE.FechaFinContrato != null) ? objClienteBE.FechaFinContrato.Value.ToString("dd/MM/yyyy") : "";

                    lstListClientesBE.Add(oListClientesBE);
                }

                vResultado.Resultado = "OK";
                vResultado.Listado = JsonConvert.SerializeObject(lstListClientesBE, Formatting.Indented);
            }
            else
            {
                vResultado.Mensaje = "No se encontraron registros solicitados";
                vResultado.Listado = JsonConvert.SerializeObject(lstListClientesBE, Formatting.Indented);
            }

            Termino:
            return vResultado;
        }

        [WebMethod(EnableSession = true)]
        public List<ClienteBE> ListarComboCliente()
        {
            try
            {
                using (ClienteBL objClienteBL = new ClienteBL())
                {
                    return objClienteBL.ListarClientes();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<ClienteBE>();
            }
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE ExportarCliente()
        {
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            List<ClienteBE> lstCliente = new List<ClienteBE>();
            List<ContactoBE> lstContacto = new List<ContactoBE>();
            List<ListClientesBE> lstListClientesBE = new List<ListClientesBE>();

            try
            {
                using (ClienteBL objClienteBL = new ClienteBL())
                {
                    lstCliente = objClienteBL.ListarClientes();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                vResultado.Mensaje = "Ocurrio un error inesperado";
                goto Termino;
            }

            if (lstCliente.Count > 0)
            {
                ListClientesBE oListClienteBE;
                int cont = 1;

                foreach (ClienteBE objClienteBE in lstCliente)
                {
                    oListClienteBE = new ListClientesBE();

                    oListClienteBE.Nro = cont++;
                    oListClienteBE.col_Tipo = "Cliente";
                    oListClienteBE.col_Descripcion = !string.IsNullOrEmpty(objClienteBE.TipoCliente.Descripcion) ? objClienteBE.TipoCliente.Descripcion : "";
                    oListClienteBE.col_DocumentoIdentidad = !string.IsNullOrEmpty(objClienteBE.DocumentoIdentidad) ? objClienteBE.DocumentoIdentidad.Trim() : "";
                    oListClienteBE.col_NombreCompleto = !string.IsNullOrEmpty(objClienteBE.NombreCompleto) ? objClienteBE.NombreCompleto : "";
                    oListClienteBE.col_Direccion = !string.IsNullOrEmpty(objClienteBE.Direccion) ? objClienteBE.Direccion : "";
                    oListClienteBE.col_Email = !string.IsNullOrEmpty(objClienteBE.Email) ? objClienteBE.Email : "";
                    oListClienteBE.col_TelefonoFijo = !string.IsNullOrEmpty(objClienteBE.Telefono) ? objClienteBE.Telefono : "";
                    oListClienteBE.col_SitioWeb = !string.IsNullOrEmpty(objClienteBE.SitioWeb) ? objClienteBE.SitioWeb : "";
                    oListClienteBE.col_FechaInicioContrato = (objClienteBE.FechaInicioContrato != null) ? objClienteBE.FechaInicioContrato.Value.ToString("dd/MM/yyyy") : "";
                    oListClienteBE.col_FechaFinContrato = (objClienteBE.FechaFinContrato != null) ? objClienteBE.FechaFinContrato.Value.ToString("dd/MM/yyyy") : "";

                    lstListClientesBE.Add(oListClienteBE);

                    using (ContactoBL objContactoBL = new ContactoBL())
                    {
                        lstContacto = objContactoBL.ListarContactos(objClienteBE.IdCliente);
                    }

                    if (lstContacto.Count > 0)
                    {
                        foreach (ContactoBE objContacoBE in lstContacto)
                        {
                            oListClienteBE = new ListClientesBE();

                            oListClienteBE.Nro = cont++;
                            oListClienteBE.col_Tipo = "Contacto";
                            oListClienteBE.col_NombreCompleto = !string.IsNullOrEmpty(objContacoBE.Nombre) ? objContacoBE.Nombre : "";
                            oListClienteBE.col_Direccion = !string.IsNullOrEmpty(objContacoBE.Direccion) ? objContacoBE.Direccion : "";
                            oListClienteBE.col_Email = !string.IsNullOrEmpty(objContacoBE.Correo) ? objContacoBE.Correo : "";
                            oListClienteBE.col_TelefonoFijo = !string.IsNullOrEmpty(objContacoBE.TelefonoFijo) ? objContacoBE.TelefonoFijo : "";
                            oListClienteBE.col_TelefonoCelular = !string.IsNullOrEmpty(objContacoBE.TelefonoCelular) ? objContacoBE.TelefonoCelular : "";
                            oListClienteBE.col_Cargo = !string.IsNullOrEmpty(objContacoBE.Cargo) ? objContacoBE.Cargo : "";
                            oListClienteBE.col_Principal = (objContacoBE.Principal) ? "Si" : "No";
                            
                            lstListClientesBE.Add(oListClienteBE);
                        }
                    }
                    
                }

                vResultado.Resultado = "OK";

                DataTable dtClientees = Globales.ToDataTable(lstListClientesBE);

                //Crear cabecera
                dtClientees.DefaultView.Sort = "Nro ASC";
                dtClientees.Columns["Nro"].ColumnName = "Nº";
                dtClientees.Columns.Remove("col_IdCliente");
                dtClientees.Columns["col_Tipo"].ColumnName = "Tipo";
                dtClientees.Columns["col_Descripcion"].ColumnName = "Descripción";
                dtClientees.Columns["col_DocumentoIdentidad"].ColumnName = "Doc. Identidad";
                dtClientees.Columns["col_NombreCompleto"].ColumnName = "Nombre";
                dtClientees.Columns["col_Direccion"].ColumnName = "Dirección";
                dtClientees.Columns["col_Email"].ColumnName = "Correo";
                dtClientees.Columns["col_TelefonoFijo"].ColumnName = "Telf. Fijo";
                dtClientees.Columns["col_TelefonoCelular"].ColumnName = "Telf. Celular";
                dtClientees.Columns["col_SitioWeb"].ColumnName = "Sitio Web";
                dtClientees.Columns["col_Cargo"].ColumnName = "Cargo";
                dtClientees.Columns["col_Principal"].ColumnName = "Cont.Principal";
                dtClientees.Columns["col_FechaInicioContrato"].ColumnName = "Inicio Contrato";
                dtClientees.Columns["col_FechaFinContrato"].ColumnName = "Fin Contrato";

                Session[Constantes.Sesion_DtExcel] = dtClientees;
            }
            else
            {
                vResultado.Mensaje = "No se puede realizar la exportación";
            }

            Termino:
            return vResultado;
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE GuardarCliente(ClienteBE oCliente)
        {
            string strError = string.Empty;
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            vResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (ClienteBL objClienteBL = new ClienteBL())
                {
                    string mensajeout;

                    oCliente.Auditoria = Session[Constantes.Sesion_Auditoria] as AuditoriaBE;

                    if (objClienteBL.GuardarCliente(oCliente, out mensajeout))
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
        public MensajeWrapperBE EliminarCliente(int IdCliente)
        {
            string strError = string.Empty;
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            vResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (ClienteBL objClienteBL = new ClienteBL())
                {
                    string mensajeout;

                    if (objClienteBL.EliminarCliente(IdCliente, out mensajeout))
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
