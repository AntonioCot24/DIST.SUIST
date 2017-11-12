<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRegistrarActividad.aspx.cs" Inherits="DIST.SUIST.Web.frmRegistrarActividad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="frmRegistrarActividad" class="form-horizontal formu form-label-left" runat="server">
        <input id="hfIdActividad" type="hidden" runat="server" />
        <input id="hfIdUsuario" type="hidden" runat="server" />
        <input id="hfIdProyecto" type="hidden" />
        <input id="hfIdCliente" type="hidden" />
        <input id="hfIdContacto" type="hidden" />
        <input id="hfIdTipoActividad" type="hidden" />
        <div class="panel panel-default" style="padding-top: 5px; padding-bottom: 0; margin-bottom: 0">
            <div class="panel-body">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="col-sm-3 text-right">Usuario: <span class="required">*</span></label>
                        <div class="col-sm-9">
                            <input id="txtUsuario" type="text" class="form-control validate[required]" disabled="disabled" runat="server" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 text-right">Cliente: <span class="required">*</span></label>
                        <div class="col-sm-9">
                            <div class="col-sm-10 fix-padding-right fix-padding-left">
                                <select id="slCliente" class="form-control chosen validate[required]" runat="server" />
                            </div>
                            <div class="col-sm-2">
                                <button id="btnNuevoCliente" type="button" class="btn btn-primary" title="Agregar Cliente"><i class="fa fa-plus"></i></button>
                            </div>
                        </div>
                    </div>
                    <div id="divProyecto" class="form-group">
                        <label class="control-label col-sm-3">
                            Proyecto:
                        </label>
                        <div class="col-sm-9">
                            <div class="col-sm-10 fix-padding-right fix-padding-left">
                                <select id="slProyecto" class="form-control chosen" runat="server" />
                            </div>
                            <div class="col-sm-2">
                                <button id="btnNuevoProyecto" type="button" class="btn btn-primary" title="Agregar Proyecto"><i class="fa fa-plus"></i></button>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3">
                            Actividad: <span class="required">*</span>
                        </label>
                        <div class="col-sm-9">
                            <div class="col-sm-10 fix-padding-right fix-padding-left">
                                <select id="slTipoActividad" class="form-control chosen" runat="server" />
                            </div>
                            <div class="col-sm-2">
                                <button id="btnNuevoTipoActividad" type="button" class="btn btn-primary" title="Agregar Actividad"><i class="fa fa-plus"></i></button>
                            </div>
                        </div>
                    </div>
                    <div id="divContacto" class="form-group">
                        <label class="control-label col-sm-3">
                            Contacto:
                        </label>
                        <div class="col-sm-9">
                            <div class="col-sm-10 fix-padding-right fix-padding-left">
                                <select id="slContacto" class="form-control chosen" runat="server" />
                            </div>
                            <div class="col-sm-2">
                                <button id="btnNuevoContacto" type="button" class="btn btn-primary" title="Agregar Contacto"><i class="fa fa-plus"></i></button>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3">
                            Glosa: <span class="required">*</span>
                        </label>
                        <div class="col-sm-9">
                            <textarea id="taGlosa" class="form-control validate[required]" rows="4" style="resize: none" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-3">
                            F. Inicio: <span class="required">*</span>
                        </label>
                        <div class="col-sm-4">
                            <div class="input-group">
                                <input id="txtFechaInicio" type="text" class="form-control date-pick-popup validate[required]" runat="server" />
                                <span id="btnFechaInicio" class="input-group-addon" style="cursor: pointer;">
                                    <span class="fa fa-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <label class="control-label col-sm-2" style="padding-left: 0px">
                            Hora: <span class="required">*</span>
                        </label>
                        <div class="col-sm-3">
                            <div class="input-group">
                                <input id="txtHoraInicio" type="text" class="form-control time-pick-popup validate[required]" runat="server" />
                                <span id="btnHoraInicio" class="input-group-addon" style="cursor: pointer;">
                                    <span class="fa fa-clock-o"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3">
                            F. Fin: <span class="required">*</span>
                        </label>
                        <div class="col-sm-4">
                            <div class="input-group">
                                <input id="txtFechaFin" type="text" class="form-control date-pick-popup validate[required]" runat="server" />
                                <span id="btnFechaFin" class="input-group-addon" style="cursor: pointer;">
                                    <span class="fa fa-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <label class="control-label col-sm-2" style="padding-left: 0px">
                            Hora: <span class="required">*</span>
                        </label>
                        <div class="col-sm-3">
                            <div class="input-group">
                                <input id="txtHoraFin" type="text" class="form-control time-pick-popup validate[required]" runat="server" />
                                <span id="btnHoraFin" class="input-group-addon" style="cursor: pointer;">
                                    <span class="fa fa-clock-o"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3">
                            Total Horas:
                        </label>
                        <div class="col-sm-3">
                            <input id="txtTotalHoras" type="text" class="form-control validate[required] validate-input-popup numeric-input" disabled="disabled" runat="server" />
                        </div>
                    </div>
                    <div class="from-group">
                        <div class="checkbox-inline text-danger" style="padding-top: 0;">
                            <label>
                                <input id="cbFacturable" type="checkbox" class="cbFacturable" value="3" name="checkbox-inline-facturable" runat="server" checked="checked" />Facturable<i class="fa fa-square-o small"></i>
                            </label>
                        </div>
                    </div>
                </div>
                <div class="form-gruop">
                    <div class="col-xs-12 col-sm-12 text-center">
                        <button id="btnGrabarActividad" type="button" class="btn btn-primary"><i class="fa fa-check"></i>&nbsp;Grabar</button>
                        <button id="btnEliminarActividad" type="button" class="btn btn-warning"><i class="fa fa-ban"></i>&nbsp;Eliminar</button>
                        <button id="btnCancelarActividad" type="button" class="btn btn-danger"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var urlRegistroCliente = "<%= urlRegistroCliente %>";
        var urlRegistroProyecto = "<%= urlRegistroProyecto %>";
        var urlRegistroTipoActividad = "<%= urlRegistroTipoActividad %>";
        var urlRegistroContacto = "<%= urlRegistroContacto %>";

        $(function () {
            fValidarFormulario();
            fAgregarEventosBotones();
        });

        function fValidarFormulario() {
            $("#frmRegistrarActividad").find("input[type=password], input[type=text], textarea").on("keydown", function (e) {
                e.stopPropagation();
                if (e != null) {
                    var code = e.keyCode || e.which;
                    if (code == 13) {
                        e.preventDefault();
                        return false;
                    }
                    return true;
                }
            });

            if ($("#hfIdActividad").val() != 0)
                $("#btnEliminarActividad").show();
            else
                $("#btnEliminarActividad").hide();

            fValidarCliente();
            fValidarFechas();
            fValidarTotalHoras();
        }

        function fAgregarEventosBotones() {
            OpenWindow('#btnNuevoCliente', urlRegistroCliente, "", false, true, 'n', "Registrar Cliente");
            OpenWindow('#btnNuevoProyecto', urlRegistroProyecto, "", false, true, 'n', "Registrar Proyecto");
            OpenWindow('#btnNuevoTipoActividad', urlRegistroTipoActividad, "", false, true, 'n', "Registrar Actividad");
            OpenWindow('#btnNuevoContacto', urlRegistroContacto, "", false, true, 'n', "Registrar Contacto");

            $("#slCliente").on("change", function () {
                var idCliente = $(this).val();

                $("#hfIdCliente").val(idCliente);
                fValidarCliente();
                fCargarComboProyecto(idCliente);
                fCargarComboContactos(idCliente);
            });
                        
            $("#txtHoraFin").on("change", function () {
                fValidarTotalHoras();
            });

            $("#btnGrabarActividad").click(function (e) {
                var isValidate = true;
                isValidate = $("#frmRegistrarActividad").validationEngine('validate');
                if (!isValidate) return false;

                if ($("#slCliente").val() == "") {
                    showMensaje("Debe seleccionar un Cliente", true);
                    return;
                }

                if ($("#slTipoActividad").val() == "") {
                    showMensaje("Debe seleccionar una Actividad", true);
                    return;
                }
                
                OpenConfirmation(null, null, '¿Est&aacute; seguro de realizar la acción de guardar?', 'Aceptar', 'Espere mientras termina la operaci&oacute;n.', 'fGuardarActividad');
            });

            $("#btnEliminarActividad").click(function (e) {
                var isValidate = true;
                isValidate = $("#frmRegistrarActividad").validationEngine('validate');
                if (!isValidate) return false;

                OpenConfirmation(null, null, '¿Est&aacute; seguro de realizar la acción de guardar?', 'Aceptar', 'Espere mientras termina la operaci&oacute;n.', 'fEliminarActividad');
            });

            $("#btnCancelarActividad").click(function (e) {
                CloseDialog($("#frmRegistrarActividad").parent());
            });
        }

        function fValidarCliente() {
            if ($("#slCliente").val() == "") {
                BloquearDiv("divProyecto", true);
                BloquearDiv("divContacto", true);
            }
            else {
                ActivarDiv("divProyecto", true);
                ActivarDiv("divContacto", true);
            }
        }

        function fValidarFechas() {
            if ($("#txtFechaInicio").val() == "")
                $("#txtFechaInicio").val(GetFechaActual())

            if ($("#txtHoraInicio").val() == "")
                $("#txtHoraInicio").val(GetHoraActual())

            if ($("#txtFechaFin").val() == "")
                $("#txtFechaFin").val(GetFechaActual())

            if ($("#txtHoraFin").val() == "") {
                var hora1 = $("#txtHoraInicio").val();
                var t1 = new Date(), tr = new Date();
                var aHora1 = hora1.split(":");

                var horas = parseInt(aHora1[0]) + 2;

                tr.setHours(horas, t1.getMinutes());

                $("#txtHoraFin").val(SetFormatHora(tr));
            }
        }

        function fValidarTotalHoras() {
            var msecPerMinute = 1000 * 60;
            var msecPerHour = msecPerMinute * 60;
            var msecPerDay = msecPerHour * 24;

            var fecha1 = formatDate($("#txtFechaInicio").val()) + " " + $("#txtHoraInicio").val() + ":00", fecha2 = formatDate($("#txtFechaFin").val()) + " " + $("#txtHoraFin").val() + ":00";
            var t1 = new Date(fecha1), t2 = new Date(fecha2);
            var tiempo1 = t1.getTime(), tiempo2 = t2.getTime();
            var tiempoR = tiempo2 - tiempo1;

            var horas = Math.floor(tiempoR / msecPerHour);
            tiempoR = tiempoR - (horas * msecPerHour);
            var minutos = Math.floor(tiempoR / msecPerMinute);
            tiempoR = tiempoR - (minutos * msecPerMinute);

            if ($("#txtHoraFin").val() == "")
                $("#txtTotalHoras").val("00:00");
            else
                $("#txtTotalHoras").val(padL(horas, 2) + ":" + padL(minutos, 2));
        }

        function fCargarComboClientes() {
            //var parametros = "{ 'idCliente': " + IdCliente + "}";
            var url = wsMantenimientoCliente + "/ListarClientes";

            fLoadSelect(url, $("#slCliente"), "", "IdCliente", "Descripcion", "", "", true, "");
        }

        function fCargarComboProyecto(IdCliente) {
            var parametros = "{ 'idCliente': " + IdCliente + "}";
            var url = wsMantenimientoProyecto + "/ListarProyectoCliente";

            fLoadSelect(url, $("#slProyecto"), parametros, "IdProyecto", "NombreProyecto", "", "", true, "");
        }

        function fCargarComboTipoActividad() {
            //var parametros = "{ 'idCliente': " + IdCliente + "}";
            var url = wsMantenimientoTipoActividad + "/ListarComboTipoActividad";

            fLoadSelect(url, $("#slTipoActividad"), "", "IdTipoActividad", "Nombre", "", "", true, "");
        }

        function fCargarComboContactos(IdCliente) {
            var parametros = "{ 'idCliente': " + IdCliente + "}";
            var url = wsMantenimientoContacto + "/ListarContactoCliente";

            fLoadSelect(url, $("#slContacto"), parametros, "IdContacto", "Nombre", "", "", true, "");
        }

        function fGuardarActividad(dialogConfirm) {
            var mensajeResult = "Ocurri&oacute; un error al acceder al servicio web, intente nuevamente";

            var objUsuarioBE = new UsuarioBE();
            objUsuarioBE.IdUsuario = $("#hfIdUsuario").val() != "" ? $.trim($("#hfIdUsuario").val()) : 0;

            var objClienteBE = new ClienteBE();
            objClienteBE.IdCliente = $("#slCliente").val() != "" ? $.trim($("#slCliente").val()) : 0;

            var objProyectoBE = new ProyectoBE();
            objProyectoBE.IdProyecto = $("#slProyecto").val() != "" ? $.trim($("#slProyecto").val()) : 0;

            var objTipoActividadBE = new TipoActividadBE();
            objTipoActividadBE.IdTipoActividad = $("#slTipoActividad").val() != "" ? $.trim($("#slTipoActividad").val()) : 0;

            var objContactoBE = new ContactoBE();
            objContactoBE.IdContacto = $("#slContacto").val() != "" ? $.trim($("#slContacto").val()) : 0;
            
            var objActividadBE = new ActividadBE();
            objActividadBE.IdActividad = $("#hfIdActividad").val() != "" ? $.trim($("#hfIdActividad").val()) : 0;
            objActividadBE.Glosa = $("#taGlosa").val() != "" ? $.trim($("#taGlosa").val()) : "";
            var fechaInicio = $("#txtFechaInicio").val() != "" ? formatDate($("#txtFechaInicio").val()) + " " + $("#txtHoraInicio").val() : null;
            objActividadBE.FechaInicio = fechaInicio;
            var fechaFin = $("#txtFechaFin").val() != "" ? formatDate($("#txtFechaFin").val()) + " " + $("#txtHoraFin").val() : null;
            objActividadBE.FechaFin = fechaFin
            var TotalHoras = $("#txtTotalHoras").val() != "" ? $.trim($("#txtTotalHoras").val()) : "00:00";
            var stotalhoras = TotalHoras.split(":");
            objActividadBE.TotalHoras = stotalhoras[0];
            objActividadBE.TotalMinutos = stotalhoras[1];
            objActividadBE.Facturable = $("#cbFacturable").is(':checked') ? true : false;
            objActividadBE.Usuario = objUsuarioBE;
            objActividadBE.Cliente = objClienteBE;
            objActividadBE.Proyecto = objProyectoBE;
            objActividadBE.TipoActividad = objTipoActividadBE;
            objActividadBE.Contacto = objContactoBE;

            var param = "{ 'oActividad': " + JSON.stringify(objActividadBE) + "}";
            $.ajax({
                type: "POST",
                url: wsMantenimientoActividad + "/GuardarActividad",
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
                        $("#hfIdActividad").val("");
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

        function fEliminarActividad(dialogConfirm) {
            var mensajeResult = "Ocurri&oacute; un error al acceder al servicio web, intente nuevamente";

            var IdActividad = $("#hfIdActividad").val() != "" ? $.trim($("#hfIdActividad").val()) : 0;

            var param = "{ 'IdActividad': " + IdActividad + "}";
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
                        $("#hfIdActividad").val("");
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
    </script>
</body>
</html>
