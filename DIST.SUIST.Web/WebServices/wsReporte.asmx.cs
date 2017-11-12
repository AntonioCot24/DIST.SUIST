using ClosedXML.Excel;
using Kendo.DynamicLinq;
using Newtonsoft.Json;
using DIST.SUIST.BE;
using DIST.SUIST.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

namespace DIST.SUIST.Web
{
    /// <summary>
    /// Descripción breve de wsReporte
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsReporte : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public FacturacionBE ObtenerFacturacionCliente(int idCliente)
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
            return objFacturacionBE;
        }

        [WebMethod(EnableSession = true)]
        public List<ActividadBE> ListarActividades(ActividadBE oActividad)
        {
            List<ActividadBE> lstActividad = new List<ActividadBE>();

            try
            {
                using (ActividadBL objActividadBL = new ActividadBL())
                {
                    lstActividad = (from a
                                    in objActividadBL.ListarActividadesFechas(oActividad)
                                    select new ActividadBE
                                    {
                                        IdActividad = a.IdActividad,
                                        strUsuario = ObtenerIniciales(a.Usuario.NombreCompleto),
                                        strNombreCompleto = "(" + ObtenerIniciales(a.Usuario.NombreCompleto) + ") " + a.Usuario.NombreCompleto,
                                        Cliente = a.Cliente,
                                        TipoActividad = a.TipoActividad,
                                        strFechaTitulo = a.FechaFin.Value.ToString("dd/MM/yyyy"),
                                        strFecha = a.FechaFin.Value.ToString("MM/dd/yyyy"),
                                        strFechaInicio = a.FechaInicio.Value.ToString("MM/dd/yyyy HH:mm"),
                                        strFechaFin = a.FechaFin.Value.ToString("MM/dd/yyyy HH:mm"),
                                        strFechaAcumulada = a.FechaFin.Value.ToString("MM/dd/yyyy") + " " + string.Format("{0:00}", a.TotalHoras) + ":" + string.Format("{0:00}", a.TotalMinutos)
                                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return lstActividad.Count > 0 ? lstActividad : null;
        }

        [WebMethod(EnableSession = true)]
        public List<ListActividadesBE> ListarProductividad(ActividadBE objActividadBE)
        {
            List<ActividadBE> lstActividad = new List<ActividadBE>();
            List<ListActividadesBE> lstListActividadesBE = new List<ListActividadesBE>();

            try
            {
                using (ReporteBL objReporteBL = new ReporteBL())
                {
                    lstActividad = objReporteBL.ReporteProductividad(objActividadBE);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                goto Termino;
            }

            if (lstActividad.Count > 0)
            {
                int cont = 1;
                foreach (ActividadBE oActividadBE in lstActividad)
                {
                    ListActividadesBE oListActividadesBE = new ListActividadesBE();

                    DateTime fechaInicio = new DateTime(oActividadBE.FechaInicio.Value.Year, oActividadBE.FechaInicio.Value.Month, oActividadBE.FechaInicio.Value.Day, oActividadBE.FechaInicio.Value.Hour, oActividadBE.FechaInicio.Value.Minute, oActividadBE.FechaInicio.Value.Second);
                    DateTime fechaFin = new DateTime(oActividadBE.FechaFin.Value.Year, oActividadBE.FechaFin.Value.Month, oActividadBE.FechaFin.Value.Day, oActividadBE.FechaFin.Value.Hour, oActividadBE.FechaFin.Value.Minute, oActividadBE.FechaFin.Value.Second);
                    TimeSpan fechaResult = fechaFin - fechaInicio;

                    oListActividadesBE.Nro = cont++;
                    oListActividadesBE.col_NombreUsuario = !string.IsNullOrEmpty(oActividadBE.Usuario.NombreCompleto) ? oActividadBE.Usuario.NombreCompleto : "";
                    oListActividadesBE.col_NombreCliente = !string.IsNullOrEmpty(oActividadBE.Cliente.NombreCompleto) ? oActividadBE.Cliente.NombreCompleto : "";
                    oListActividadesBE.col_NombreProyecto = !string.IsNullOrEmpty(oActividadBE.Proyecto.NombreProyecto) ? oActividadBE.Proyecto.NombreProyecto : "";
                    oListActividadesBE.col_NombreTipoActividad = !string.IsNullOrEmpty(oActividadBE.TipoActividad.Nombre) ? oActividadBE.TipoActividad.Nombre : "";
                    oListActividadesBE.col_Fecha = (oActividadBE.FechaInicio != null) ? oActividadBE.FechaInicio.Value.ToString("dd/MM/yyyy") : "";
                    oListActividadesBE.col_Horas = (oActividadBE.FechaInicio != null) && (oActividadBE.FechaFin != null) ? fechaResult.TotalHours : 0;

                    lstListActividadesBE.Add(oListActividadesBE);
                }
            }

            Termino:
            return lstListActividadesBE;
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE ExportarProductividad(ActividadBE objActividadBE)
        {
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            List<ActividadBE> lstActividades = new List<ActividadBE>();
            List<ListActividadesBE> lstListActividadesBE = new List<ListActividadesBE>();

            try
            {
                using (ReporteBL objReporteBL = new ReporteBL())
                {
                    lstActividades = objReporteBL.ReporteProductividad(objActividadBE);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                vResultado.Mensaje = "Ocurrio un error inesperado";
                goto Termino;
            }

            if (lstActividades.Count > 0)
            {
                int cont = 1;

                foreach (ActividadBE oActividadBE in lstActividades)
                {
                    ListActividadesBE oListActividadesBE = new ListActividadesBE();

                    DateTime fechaInicio = new DateTime(oActividadBE.FechaInicio.Value.Year, oActividadBE.FechaInicio.Value.Month, oActividadBE.FechaInicio.Value.Day, oActividadBE.FechaInicio.Value.Hour, oActividadBE.FechaInicio.Value.Minute, oActividadBE.FechaInicio.Value.Second);
                    DateTime fechaFin = new DateTime(oActividadBE.FechaFin.Value.Year, oActividadBE.FechaFin.Value.Month, oActividadBE.FechaFin.Value.Day, oActividadBE.FechaFin.Value.Hour, oActividadBE.FechaFin.Value.Minute, oActividadBE.FechaFin.Value.Second);
                    TimeSpan fechaResult = fechaFin - fechaInicio;

                    oListActividadesBE.Nro = cont++;
                    oListActividadesBE.col_NombreUsuario = !string.IsNullOrEmpty(oActividadBE.Usuario.NombreCompleto) ? oActividadBE.Usuario.NombreCompleto : "";
                    oListActividadesBE.col_NombreCliente = !string.IsNullOrEmpty(oActividadBE.Cliente.NombreCompleto) ? oActividadBE.Cliente.NombreCompleto : "";
                    oListActividadesBE.col_NombreProyecto = !string.IsNullOrEmpty(oActividadBE.Proyecto.NombreProyecto) ? oActividadBE.Proyecto.NombreProyecto : "";
                    oListActividadesBE.col_NombreTipoActividad = !string.IsNullOrEmpty(oActividadBE.TipoActividad.Nombre) ? oActividadBE.TipoActividad.Nombre : "";
                    oListActividadesBE.col_Fecha = (oActividadBE.FechaInicio != null) ? oActividadBE.FechaInicio.Value.ToString("dd/MM/yyyy") : "";
                    oListActividadesBE.col_Horas = (oActividadBE.FechaInicio != null) && (oActividadBE.FechaFin != null) ? fechaResult.TotalHours : 0;

                    cont++;

                    lstListActividadesBE.Add(oListActividadesBE);
                }

                vResultado.Resultado = "OK";

                DataTable dtActividades = Globales.ToDataTable(lstListActividadesBE);

                //Crear cabecera
                dtActividades.DefaultView.Sort = "Nro ASC";
                dtActividades.Columns["Nro"].ColumnName = "Nº";
                dtActividades.Columns["col_NombreUsuario"].ColumnName = "Abogado";
                dtActividades.Columns["col_NombreCliente"].ColumnName = "Cliente";
                dtActividades.Columns["col_NombreProyecto"].ColumnName = "Proyecto";
                dtActividades.Columns["col_NombreTipoActividad"].ColumnName = "Tipo Actividad";
                dtActividades.Columns["col_Fecha"].ColumnName = "Fecha";
                dtActividades.Columns["col_Horas"].ColumnName = "Horas";

                Session[Constantes.Sesion_DtExcel] = dtActividades;
            }
            else
            {
                vResultado.Mensaje = "No se encontraron registros solicitados";
                vResultado.Listado = JsonConvert.SerializeObject(lstListActividadesBE, Formatting.Indented);
            }

            Termino:
            return vResultado;
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE ExportarReporteCliente(ActividadExportarBE objActividadExportar)
        {
            MensajeWrapperBE vResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };

            try
            {
                XLWorkbook wb = new XLWorkbook();
                int fila = 1;

                var ws = wb.Worksheets.Add("Reporte Horas");

                //ws.Cell("A" + fila).Value = objActividadExportar.Titulo;
                //var rngLogo = ws.Range("A" + fila, "E" + fila);
                //rngLogo.Merge();
                //rngLogo.Style.Font.Bold = true;
                //rngLogo.Style.Font.FontSize = 18;
                //rngLogo.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //rngLogo.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                //fila = fila + 1;

                ws.Cell("A" + fila).Value = objActividadExportar.Titulo;
                var rngTitulo = ws.Range("A" + fila, "E" + fila);
                rngTitulo.Merge();
                rngTitulo.Style.Font.Bold = true;
                rngTitulo.Style.Font.FontSize = 18;
                rngTitulo.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                rngTitulo.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                fila = fila + 1;

                ws.Cell("A" + fila).Value = objActividadExportar.Cliente;
                var rngCliente = ws.Range("A" + fila, "E" + fila);
                rngCliente.Merge();
                rngCliente.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                rngCliente.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                fila = fila + 1;

                ws.Cell("A" + fila).DataType = XLCellValues.Text;
                ws.Cell("A" + fila).Value = "'" + objActividadExportar.Fecha;
                var rngFecha = ws.Range("A" + fila, "E" + fila);
                rngFecha.Merge();
                rngFecha.Style.Font.Bold = true;
                rngFecha.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                rngFecha.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                rngFecha.DataType = XLCellValues.Text;
                fila = fila + 2;

                //Cabecera del listado
                ws.Cell("A" + fila).Value = "Fecha";
                ws.Cell("B" + fila).Value = "Cliente";
                ws.Cell("C" + fila).Value = "Tema";
                ws.Cell("D" + fila).Value = "Tiempo";
                ws.Cell("E" + fila).Value = "Tiempo Acumulado";


                int filaInicio = fila;
                fila = fila + 1;
                foreach (ListaActividadesBE objLista in objActividadExportar.ListaActividadesBE)
                {
                    ws.Cell("A" + fila).Value = "'" + objLista.Fecha;
                    ws.Cell("B" + fila).Value = "'" + objLista.Cliente;
                    ws.Cell("C" + fila).Value = "'" + objLista.Tema;
                    ws.Cell("D" + fila).Value = "'" + objLista.Tiempo;
                    ws.Cell("E" + fila).Value = "'" + objLista.TiempoAcumulado;
                    fila++;
                }
                int filaFin = fila - 1;
                var rngListado = ws.Range("A" + filaInicio, "E" + filaFin);
                rngListado.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                rngListado.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                rngListado.Style.Border.InsideBorderColor = XLColor.Gray;
                rngListado.Style.Border.OutsideBorderColor = XLColor.Gray;
                rngListado.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                rngListado.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                rngListado.FirstRow().Style.Fill.BackgroundColor = XLColor.DarkGray;
                rngListado.FirstRow().Style.Font.Bold = true;

                fila = fila + 1;

                if (objActividadExportar.totalHorasMes.Contains(":"))
                {
                    ws.Cell("A" + fila).Value = objActividadExportar.totalHorasMes;
                    var rngTotalHorasMes = ws.Range("A" + fila, "E" + fila);
                    rngTotalHorasMes.Merge();
                    rngTotalHorasMes.Style.Font.Bold = true;
                    rngTotalHorasMes.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    rngTotalHorasMes.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    fila = fila + 1;
                }
                if (objActividadExportar.totalMontoContrato.Contains(":"))
                {
                    ws.Cell("A" + fila).Value = objActividadExportar.totalMontoContrato;
                    var rngTotalMontoContrato = ws.Range("A" + fila, "E" + fila);
                    rngTotalMontoContrato.Merge();
                    rngTotalMontoContrato.Style.Font.Bold = true;
                    rngTotalMontoContrato.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    rngTotalMontoContrato.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    fila = fila + 1;
                }
                if (objActividadExportar.totalMontoProyecto.Contains(":"))
                {
                    ws.Cell("A" + fila).Value = objActividadExportar.totalMontoProyecto;
                    var rngTotalMontoProyecto = ws.Range("A" + fila, "E" + fila);
                    rngTotalMontoProyecto.Merge();
                    rngTotalMontoProyecto.Style.Font.Bold = true;
                    rngTotalMontoProyecto.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    rngTotalMontoProyecto.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    fila = fila + 1;
                }
                if (objActividadExportar.totalHorasRetainer.Contains(":"))
                {
                    ws.Cell("A" + fila).Value = objActividadExportar.totalHorasRetainer;
                    var rngTotalHorasRetainer = ws.Range("A" + fila, "E" + fila);
                    rngTotalHorasRetainer.Merge();
                    rngTotalHorasRetainer.Style.Font.Bold = true;
                    rngTotalHorasRetainer.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    rngTotalHorasRetainer.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    fila = fila + 1;
                }
                if (objActividadExportar.totalHorasExceso.Contains(":"))
                {
                    ws.Cell("A" + fila).Value = objActividadExportar.totalHorasExceso;
                    var rngTotalHorasExceso = ws.Range("A" + fila, "E" + fila);
                    rngTotalHorasExceso.Merge();
                    rngTotalHorasExceso.Style.Font.Bold = true;
                    rngTotalHorasExceso.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    rngTotalHorasExceso.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    fila = fila + 1;
                }
                if (objActividadExportar.totalMontoRetainer.Contains(":"))
                {
                    ws.Cell("A" + fila).Value = objActividadExportar.totalMontoRetainer;
                    var rngTotalMontoRetainer = ws.Range("A" + fila, "E" + fila);
                    rngTotalMontoRetainer.Merge();
                    rngTotalMontoRetainer.Style.Font.Bold = true;
                    rngTotalMontoRetainer.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    rngTotalMontoRetainer.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    fila = fila + 1;
                }
                if (objActividadExportar.totalMontoHoraExceso.Contains(":"))
                {
                    ws.Cell("A" + fila).Value = objActividadExportar.totalMontoHoraExceso;
                    var rngTotalMontoHoraExceso = ws.Range("A" + fila, "E" + fila);
                    rngTotalMontoHoraExceso.Merge();
                    rngTotalMontoHoraExceso.Style.Font.Bold = true;
                    rngTotalMontoHoraExceso.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    rngTotalMontoHoraExceso.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    fila = fila + 1;
                }
                if (objActividadExportar.totalMontoFlat.Contains(":"))
                {
                    ws.Cell("A" + fila).Value = objActividadExportar.totalMontoFlat;
                    var rngTotalMontoFlat = ws.Range("A" + fila, "E" + fila);
                    rngTotalMontoFlat.Merge();
                    rngTotalMontoFlat.Style.Font.Bold = true;
                    rngTotalMontoFlat.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    rngTotalMontoFlat.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    fila = fila + 1;
                }
                if (objActividadExportar.totalPagar.Contains(":"))
                {
                    ws.Cell("A" + fila).Value = objActividadExportar.totalPagar;
                    var rngTotalPagar = ws.Range("A" + fila, "E" + fila);
                    rngTotalPagar.Merge();
                    rngTotalPagar.Style.Font.Bold = true;
                    rngTotalPagar.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    rngTotalPagar.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    fila = fila + 1;
                }

                ws.Columns(1, 5).AdjustToContents();
                Session[Constantes.Sesion_ExpExcel] = wb;
                vResultado.Resultado = "OK";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                vResultado.Mensaje = "No se encontraron registros solicitados";
            }

            return vResultado;
        }

        private string ObtenerIniciales(string strNombre)
        {
            StringBuilder sbIniciales = new StringBuilder();

            string[] ArrayString = strNombre.Split(' ');
            int contArray = 1;

            foreach (string strCadena in ArrayString)
            {
                if (ArrayString.Length != contArray)
                    sbIniciales.Append(strCadena.Substring(0, 1) + ". ");
                else
                    sbIniciales.Append(strCadena.Substring(0, 1) + ".");

                contArray++;
            }

            return sbIniciales.ToString();
        }
    }
}
