<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAdministrarTiempos.aspx.cs" Inherits="DIST.SUIST.Web.frmAdministrarTiempos" %>

<%@ Import Namespace="DIST.SUIST.Web" %>
<%@ Import Namespace="DIST.SUIST.BE" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="frmAdministrarTiempos" class="form-horizontal formu form-label-left" runat="server">
        <input id="hdIdActividad" type="hidden" runat="server" />
        <div class="well">
            <div class="text-center">
                <h3 class="page-header">Control de Tiempos</h3>
            </div>
        </div>
        <div class="row full-calendar">
            <div class="box">
                <div id="bhCalendario" class="box-header" icon="fa fa-calendar" titulo="Calendario"></div>
                <div id="bbCalendario" class="box-content">
                    <div class="text-right">
                        <button id="btnSelectCalendar" type="button" class="btn btn-primary" style="display: none">&nbsp;Seleccionar</button>
                        <button id="btnNuevoActividad" type="button" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;Nuevo</button>
                    </div>
                    <div id="dvCalendar"></div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var wsMantenimientoActividad = "<%= wsMantenimientoActividad %>";
        var wsMantenimientoCliente = "<%= wsMantenimientoCliente %>";
        var wsMantenimientoProyecto = "<%= wsMantenimientoProyecto %>";
        var wsMantenimientoTipoActividad = "<%= wsMantenimientoTipoActividad %>";
        var wsMantenimientoContacto = "<%= wsMantenimientoContacto %>";
        var urlRegistroActividad = "<%= urlRegistroActividad %>";

        var UsuarioBE = function () {
            this.IdUsuario = 0;
            this.NombreCompleto = 0;
        }

        var TipoClienteBE = function () {
            this.IdTipoCliente = 0;
            this.Descripcion = "";
        }

        var ClienteBE = function () {
            this.IdCliente = 0;
            this.DocumentoIdentidad = "";
            this.NombreCompleto = "";
            this.Email = "";
            this.Telefono = "";
            this.SitioWeb = "";
            this.Direccion = "";
            this.FechaInicioContrato = null;
            this.FechaFinContrato = null;

            this.TipoCliente = new TipoClienteBE();
        }

        var ProyectoBE = function () {
            this.IdProyecto = 0;
            this.NombreProyecto = "";
            this.Miembros = false;

            this.Cliente = new ClienteBE();
        }

        var PrecioBE = function () {
            this.IdPrecio = 0;
            this.Monto = 0;
        }

        var TipoActividadBE = function () {
            this.IdTipoActividad = 0;
            this.Nombre = "";

            this.Precio = new PrecioBE();
        }

        var ContactoBE = function () {
            this.IdContacto = 0;
            this.Nombre = "";
            this.Direccion = "";
            this.TelefonoFijo = "";
            this.TelefonoCelular = "";
            this.Correo = "";
            this.Cargo = "";
            this.Principal = false;

            this.Cliente = new ClienteBE();
        }

        var ActividadBE = function () {
            this.IdActividad = 0;
            this.Glosa = "";
            this.FechaInicio = null;
            this.FechaFin = null;
            this.TotalHoras = 0;
            this.TotalMinutos = 0;
            this.Facturable = false;

            this.Usuario = new UsuarioBE();
            this.Cliente = new ClienteBE();
            this.Proyecto = new ProyectoBE();
            this.TipoActividad = new TipoActividadBE();
            this.Contacto = new ContactoBE();
        }

        var eventData;

        $(function () {
            fValidarFormulario();
            fAgregarEventosBotones();
            fCargarCalendario();
        });

        function fValidarFormulario() {
        }

        function fAgregarEventosBotones() {
            OpenWindow('#btnNuevoActividad', urlRegistroActividad, "", false, true, 'a', "Registrar Actividad");
        }

        function fGetCalendarFormatDate(date) {
            return new Date(date);
        }

        function fActualizarCalendario() {
            var scheduler = $("#calendar").data("kendoScheduler");
            scheduler.dataSource.read();
            scheduler.refresh();
        }

        function fCargarCalendario() {
            if (!$("#calendar").length) {
                $("#dvCalendar").append("<div id='calendar'></div>");

                var date = new Date();
                $("#calendar").kendoScheduler({
                    date: date,
                    startTime: new Date(date.getFullYear(), date.getMonth(), date.getDate(), 07),
                    culture: "es-ES",
                    height: 600,
                    selectable: true,
                    change: scheduler_change,
                    remove: scheduler_remove,
                    views: [
                        "day",
                        { type: "workWeek", selected: true },
                        "week",
                        "month",
                    ],
                    dataSource: {
                        batch: true, // Enable batch updates
                        transport: {
                            read: {
                                url: wsMantenimientoActividad + "/ListarEventos",
                                contentType: "application/json; charset=utf-8",
                                type: "POST",
                                dataType: "json"
                            }/*,
                            update: {
                                url: "http://demos.telerik.com/kendo-ui/service/tasks/update",
                                dataType: "jsonp"
                            },
                            create: {
                                url: "http://demos.telerik.com/kendo-ui/service/tasks/create",
                                dataType: "jsonp"
                            },
                            destroy: {
                                url: "http://demos.telerik.com/kendo-ui/service/tasks/destroy",
                                dataType: "jsonp"
                            },
                            parameterMap: function (data, type) {
                                if (type = "read")
                                    return param;
                            }*/
                        },
                        schema: {
                            data: "d",
                            model: {
                                id: "id", // The "id" of the event is the "taskId" field
                                fields: {
                                    // Describe the scheduler event fields and map them to the fields returned by the remote service
                                    id: {
                                        from: "id", // The 'TaskID' server-side field is mapped to the 'taskId' client-side field
                                        type: "number"
                                    },
                                    title: { from: "title", defaultValue: "Sin Titulo" },
                                    start: { type: "date", from: "start" },
                                    end: { type: "date", from: "end" },
                                    description: { from: "description" }
                                    /*recurrenceId: { from: "RecurrenceID" },
                                    recurrenceRule: { from: "RecurrenceRule" },
                                    recurrenceException: { from: "RecurrenceException" },
                                    isAllDay: { type: "boolean", from: "IsAllDay" }*/
                                }
                            }
                        }
                    }
                    /*resources: [
                        {
                            field: "id",
                            name: "title",
                            title: "Titulos",
                            dataSource: {
                                type: "json",
                                transport:
                                {
                                    read: {
                                        url: wsMantenimientoActividad + "/ListarRecursos",
                                        contentType: "application/json; charset=utf-8",
                                        type: "POST",
                                        dataType: "json"
                                    }
                                }
                            }
                        }
                        ]*/
                    //timezone: "Etc/UTC"
                });

                var cont = 0;

                function scheduler_remove(e) {
                    var mensajeResult = "Ocurri&oacute; un error al acceder al servicio web, intente nuevamente";

                    var objActividadBE = new ActividadBE();
                    objActividadBE.IdActividad = $("#hdIdActividad").val() != "" ? $.trim($("#hdIdActividad").val()) : 0;

                    var param = "{ 'oActividad': " + JSON.stringify(objActividadBE) + "}";
                    $.ajax({
                        type: "POST",
                        url: wsMantenimientoActividad + "/EliminarActividad",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: param,
                        success: function (resp) {
                            var vResult = (typeof resp.d) == 'string' ? eval('(' + resp.d + ')') : resp.d;
                            var eserror = true;
                            if (vResult != null) {
                                if (vResult.Resultado == 'OK')
                                    eserror = false;
                                dialogConfirm.close();
                                mensajeResult = vResult.Mensaje;
                            }
                            if (!eserror) {
                                dialogConfirm.close();
                                showMensaje(mensajeResult, eserror);
                                $("#hdIdActividad").val("");
                                fActualizarCalendario();
                                CloseDialog($("#frmRegistrarActividad").parent());
                            }
                            else {
                                dialogConfirm.close();
                                showMensaje(mensajeResult, eserror);
                            }
                            return true;
                        },
                        error: function (request, status, error) {
                            dialogConfirm.close();
                            showMensaje(mensajeResult, eserror);
                            return true;
                        }
                    });
                }

                function scheduler_change(e) {
                    e.preventDefault();
                    $("#calendar").mouseup(function (mu) {
                        mu.preventDefault();
                        var events = e.events;
                        var strFechaInicio = e.start.toLocaleString().split(' ');
                        var fechaInicio = strFechaInicio[0].split('/');
                        var horaInicio = strFechaInicio[1].split(':');
                        var strFechaFin = e.end.toLocaleString().split(' ');
                        var fechaFin = strFechaFin[0].split('/');
                        var horaFin = strFechaFin[1].split(':');

                        $("#btnSelectCalendar").attr("IdActividad", 0);

                        if (events.length) {
                            $("#btnSelectCalendar").attr("IdActividad", events[events.length - 1].id);
                        }

                        $("#btnSelectCalendar").attr("startField", padL(fechaInicio[0], 2) + "/" + padL(fechaInicio[1], 2) + "/" + padL(fechaInicio[2], 2) + " " + padL(horaInicio[0], 2) + ":" + padL(horaInicio[1], 2));
                        $("#btnSelectCalendar").attr("endField", padL(fechaFin[0], 2) + "/" + padL(fechaFin[1], 2) + "/" + padL(fechaFin[2], 2) + " " + padL(horaFin[0], 2) + ":" + padL(horaFin[1], 2));

                        OpenWindowClick($("#btnSelectCalendar"), urlRegistroActividad, "IdActividad startField endField", false, true, 'a', "Registrar Actividad");
                    });
                    return;
                }
            }
        }
    </script>
</body>
</html>
