<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmReporteProductividad.aspx.cs" Inherits="DIST.SUIST.Web.frmReporteProductividad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="frmReporteProductividad" class="form-horizontal formu form-label-left" runat="server">
        <input id="hfIdProyecto" type="hidden" />
        <div class="well">
            <div class="text-center">
                <h3 class="page-header">Reporte de Productividad</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <div class="box">
                    <div id="bhOpciones" class="box-header" icon="fa fa-cog" titulo="Opciones"></div>
                    <div id="bbOpciones" class="box-content">
                        <div class="dFiltros">
                            <div class="form-group" style="margin-top: 0; margin-bottom: 0">
                                <div class="col-sm-12">
                                    <label class="col-sm-2 text-right">Fecha:</label>
                                    <div class="col-sm-2">
                                        <div class="input-group">
                                            <input id="txtFechaInicio" type="text" class="form-control date-pick" placeholder="Desde" runat="server" />
                                            <span id="btnFechaIncio" class="input-group-addon" style="cursor: pointer;">
                                                <span class="fa fa-calendar"></span>
                                            </span>
                                        </div>
                                        <div class="input-group">
                                            <input id="txtFechaFin" type="text" class="form-control date-pick" placeholder="Hasta" runat="server" />
                                            <span id="btnFechaFin" class="input-group-addon" style="cursor: pointer;">
                                                <span class="fa fa-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                    <label class="col-sm-2 text-right">Usuario:</label>
                                    <div class="col-sm-2">
                                        <select id="slUsuario" class="form-control" runat="server" />
                                    </div>
                                    <label class="col-sm-2 text-right">Cliente:</label>
                                    <div class="col-sm-2">
                                        <select id="slCliente" class="form-control" runat="server"></select>
                                    </div>
                                    <label class="col-sm-2 text-right">Tipo Actividad:</label>
                                    <div class="col-sm-2">
                                        <select id="slTipoActividad" class="form-control" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" style="margin-top: 0; margin-bottom: 0">
                                <div class="col-sm-12 text-right">
                                    <button id="btnExportarProductividad" type="button" class="btn btn-success" nomarchivoexcel="Reporte de Productividad"><i class="fa fa-file-excel-o"></i>&nbsp;Exportar</button>
                                    <button id="btnLimpiar" type="button" class="btn btn-info"><i class="fa fa-eraser"></i>&nbsp; Limpiar</button>
                                    <button id="btnBuscar" type="button" class="btn btn-primary"><i class="fa fa-search"></i>&nbsp; Buscar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12">
                <div class="box">
                    <div id="bhListaProductividad" class="box-header" icon="fa fa-table" titulo="Listado de Productividad"></div>
                    <div class="box-content">
                        <div class="form-group">
                            <div id="dtProductividad"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var wsReporte = "<%= wsReporte %>";

        var UsuarioBE = function () {
            this.IdUsuario = 0;
        }

        var ClienteBE = function () {
            this.IdCliente = 0;
        }

        var TipoActividadBE = function () {
            this.IdTipoActividad = 0;
        }

        var ActividadBE = function () {
            this.IdActividad = 0;
            this.FechaInicio = null;
            this.FechaFin = null;

            this.Usuario = new UsuarioBE();
            this.Cliente = new ClienteBE();
            this.TipoActividad = new TipoActividadBE();
        }

        $(function () {
            fListarProductividad();
            fValidarFormulario();
            fAgregarEventosBotones();
        });

        function fValidarFormulario() {

        }

        function fAgregarEventosBotones() {
            $("#btnExportarProductividad").on("click", function (e) {
                var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
                var eserror = true;

                var objActividad = new ActividadBE();
                var objUsuarioBE = new UsuarioBE();
                var objClienteBE = new ClienteBE();
                var objTipoActividadBE = new TipoActividadBE();

                objActividad.FechaInicio = $("#txtFechaInicio").val() != "" ? formatDate($("#txtFechaInicio").val()) : null;
                objActividad.FechaFin = $("#txtFechaFin").val() != "" ? formatDate($("#txtFechaFin").val()) : null;
                objUsuarioBE.IdUsuario = $("#slUsuario").val() != "" ? $.trim($("#slUsuario").val()) : 0;
                objClienteBE.IdCliente = $("#slCliente").val() != "" ? $.trim($("#slCliente").val()) : 0;
                objTipoActividadBE.IdTipoActividad = $("#slTipoActividad").val() != "" ? $.trim($("#slTipoActividad").val()) : 0;

                objActividad.Usuario = objUsuarioBE;
                objActividad.Cliente = objClienteBE;
                objActividad.TipoActividad = objTipoActividadBE;

                var param = "{ 'objActividadBE': " + JSON.stringify(objActividad) + "}";

                $.ajax({
                    type: "POST",
                    url: wsReporte + "/ExportarProductividad",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    cache: false,
                    data: param,
                    success: function (resp) {
                        var vResult = (typeof resp.d) == 'string' ? eval('(' + resp.d + ')') : resp.d;
                        var eserror = true;
                        if (vResult != null) {
                            if (vResult.Resultado == 'OK')
                                eserror = false;
                            mensajeResult = htmlDecode(vResult.Mensaje);
                        }
                        if (!eserror) {
                            OpenWindowTabClick($("#btnExportarProductividad"), urlDescargarExcel, "nomArchivoExcel");
                        }
                        return true;
                    },
                    error: function (request, status, error) {
                        showMensaje(mensajeResult, eserror);
                        return true;
                    }
                });
            });

            $("#btnLimpiar").click(function (e) {
                fListarProductividad(false);
                LimpiarFiltros();
            });

            $("#btnBuscar").click(function (e) {
                fListarProductividad(true);
            });
        }

        function fListarProductividad(filtrar) {
            var objActividad = new ActividadBE();

            if (filtrar) {
                var objUsuarioBE = new UsuarioBE();
                var objClienteBE = new ClienteBE();
                var objTipoActividadBE = new TipoActividadBE();

                objActividad.FechaInicio = $("#txtFechaInicio").val() != "" ? formatDate($("#txtFechaInicio").val()) : null;
                objActividad.FechaFin = $("#txtFechaFin").val() != "" ? formatDate($("#txtFechaFin").val()) : null;
                objUsuarioBE.IdUsuario = $("#slUsuario").val() != "" ? $.trim($("#slUsuario").val()) : 0;
                objClienteBE.IdCliente = $("#slCliente").val() != "" ? $.trim($("#slCliente").val()) : 0;
                objTipoActividadBE.IdTipoActividad = $("#slTipoActividad").val() != "" ? $.trim($("#slTipoActividad").val()) : 0;

                objActividad.Usuario = objUsuarioBE;
                objActividad.Cliente = objClienteBE;
                objActividad.TipoActividad = objTipoActividadBE;
            }

            var param = "{ 'objActividadBE': " + JSON.stringify(objActividad) + "}";

            $("#dtProductividad").kendoGrid({
                dataSource: {
                    transport: {
                        read: {
                            url: wsReporte + "/ListarProductividad",
                            contentType: "application/json; charset=utf-8",
                            type: "POST",
                            dataType: "json"
                        },
                        parameterMap: function (data, type) {
                            if (type = "read")
                                return param;
                        }
                    },
                    schema: {
                        data: "d",
                        model: {
                            id: "Productividad",
                            fields: {
                                Nro: { type: "number" },
                                col_NombreUsuario: { type: "string" },
                                col_NombreCliente: { type: "string" },
                                col_NombreProyecto: { type: "string" },
                                col_NombreTipoActividad: { type: "string" },
                                col_Fecha: { type: "string" },
                                col_Horas: { type: "number" }
                            }
                        }
                    },
                    group: {
                        field: "col_NombreUsuario", aggregates: [{ field: "col_Horas", aggregate: "sum" }]
                    },
                    aggregate: [{ field: "col_Horas", aggregate: "sum" }],
                    pageSize: 20
                },
                height: 550,
                filterable: false,
                sortable: true,
                pageable: true,
                groupable: true,
                columns: [
                    { field: "Nro", title: "Nº" },
                    { field: "col_NombreUsuario", title: "Usuario" },
                    { field: "col_NombreCliente", title: "Cliente" },
                    { field: "col_NombreProyecto", title: "Proyecto" },
                    { field: "col_NombreTipoActividad", title: "Actividad" },
                    { field: "col_Fecha", title: "Fecha" },
                    { field: "col_Horas", title: "Tiempo", aggregates: ["sum"], groupFooterTemplate: "SubTotal: #= sum #", footerTemplate: "Total: #= sum #" }
                ]
            });
        }
    </script>
</body>
</html>
