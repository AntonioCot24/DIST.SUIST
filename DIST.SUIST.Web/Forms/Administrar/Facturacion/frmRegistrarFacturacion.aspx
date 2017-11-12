<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRegistrarFacturacion.aspx.cs" Inherits="DIST.SUIST.Web.frmRegistrarFacturacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="frmRegistrarFacturacion" class="form-horizontal formu form-label-left" runat="server">
        <input id="hfIdFacturacion" type="hidden" runat="server" />
        <input id="hfTipoFacturacion" type="hidden" runat="server" />
        <input id="hfIdMonedaFacturacion" type="hidden" runat="server" />
        <div class="panel panel-default" style="padding-top: 5px; padding-bottom: 0; margin-bottom: 0">
            <div class="panel-body">
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <div class="col-sm-4">
                            <div class="radio-inline" style="padding-top: 0;">
                                <label>
                                    <input id="rbTarifaPlana" type="radio" class="rbTipoTarifa" value="1" name="radio-inline-contacto" runat="server" />Tarifa Plana<i class="fa fa-circle-o small"></i>
                                </label>
                            </div>
                        </div>
                        <div class="col-sm-7">
                            <input id="txtMontoTarifaPlana" type="text" class="form-control validate[required] validate-input-popup decimal-input iTarifaPlana" placeholder="Monto Tarifa Plana" disabled="disabled" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <div class="col-sm-4">
                            <div class="radio-inline" style="padding-top: 0;">
                                <label>
                                    <input id="rbTarifaHoras" type="radio" class="rbTipoTarifa" value="2" name="radio-inline-contacto" runat="server" />Tarifa por Hora<i class="fa fa-circle-o small"></i>
                                </label>
                            </div>
                        </div>
                        <div class="col-sm-7">
                            <input id="txtMontoTarifaHoras" type="text" class="form-control validate[required] validate-input-popup decimal-input iTarifaHoras" placeholder="Monto por Horas" disabled="disabled" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <div class="col-sm-4">
                            <div class="radio-inline" style="padding-top: 0;">
                                <label>
                                    <input id="rbMixto" type="radio" class="rbTipoTarifa" value="3" name="radio-inline-contacto" runat="server" />Tarifa Mixta<i class="fa fa-circle-o small"></i>
                                </label>
                            </div>
                        </div>
                        <div class="col-sm-7">
                            <div class="col-sm-6" style="padding-left: 0;">
                                <input id="txtMontoTarifaMixta" type="text" class="form-control validate[required] validate-input-popup decimal-input iTarifaMixta" placeholder="Monto Tarifa" disabled="disabled" runat="server" />
                            </div>
                            <div class="col-sm-6" style="padding-right: 0">
                                <input id="txtMontoTarifaHora" type="text" class="form-control validate[required] validate-input-popup decimal-input iTarifaMixta" placeholder="Cant. Horas" disabled="disabled" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <div class="col-sm-offset-4 col-sm-7">
                            <input id="txtMontoAdicional" type="text" class="form-control validate[required] validate-input-popup decimal-input iTarifaMixta" placeholder="Monto Adicional" disabled="disabled" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Monto Flat:</label>
                        <div class="col-sm-7">
                            <input id="txtMontoFlat" type="text" class="form-control validate-input-popup decimal-input" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Moneda: <span class="required">*</span></label>
                        <div class="col-sm-7">
                            <div class="col-sm-10" style="padding-left: 0px; padding-right: 0px;">
                                <select id="slTipoMoneda" class="form-control" runat="server" />
                            </div>
                            <div class="col-sm-2">
                                <button id="btnAgregarMoneda" type="button" class="btn btn-primary btn-sm"><i class="fa fa-plus"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divMoneda"></div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Día Facturación: <span class="required">*</span></label>
                        <div class="col-sm-7">
                            <input id="txtFechaFacturacion" type="text" class="form-control validate-input-popup numeric-input validate[required]" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Dirección: </label>
                        <div class="col-sm-7">
                            <input id="txtDireccionFactura" type="text" class="form-control" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Contacto: <span class="required">*</span></label>
                        <div class="col-sm-7">
                            <select id="slContactoFactura" class="form-control select-chosen validate[required]" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-gruop">
                    <div class="col-xs-12 col-sm-12 text-center">
                        <button id="btnAceptar" type="button" class="btn btn-primary"><i class="fa fa-check"></i>&nbsp;Aceptar</button>
                        <button id="btnCancelar" type="button" class="btn btn-danger"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var wsAdministrarFacturacion = "<%= wsAdministrarFacturacion %>";
        var wsMantenimientoMoneda = "<%= wsMantenimientoMoneda %>";
        var valMoneda = false;

        var ContactoBE = function () {
            this.IdContacto = 0;
        }

        var MonedaFacturacionBE = function () {
            this.IdMonedaFacturacion = 0;
            this.IdMoneda = 0;
            this.IdFacturacion = 0;
            this.Monto = 0;
        }

        var FacturacionBE = function () {
            this.IdFacturacion = 0;
            this.TipoFacturacion = 0;
            this.TarifaFija = 0.0;
            this.TarifaHoras = 0.0;
            this.TarifaHorasAdicionales = 0.0;
            this.MontoFlat = 0.0;
            this.FechaFactura = 0;
            this.Direccion = "";

            this.Contacto = new ContactoBE();
            this.Cliente = new ClienteBE();
            this.lstMonedaFacturacion = new Array();
        }

        $(function () {
            fValidarFormulario();
            fAgregarEventosBotones();
        });

        function fValidarFormulario() {
            if ($("#hfIdFacturacion").val() == "")
                $("#rbTarifaPlana").prop("checked", true);
            fValidarChecks();
            fListarMonedaFacturacion();
        }

        function fAgregarEventosBotones() {
            $(".rbTipoTarifa").on("change", function (e) {
                var chk = $(this);

                fValidarChecks();
                $("#hfTipoFacturacion").val(chk.val());
            });

            $("#btnAgregarMoneda").click(function (e) {
                if ($("#slTipoMoneda").val() == "")
                    return;

                var idTipoMoneda = $("#slTipoMoneda").val();
                var idDivMonera = "", idtxtMoneda = "", idSpanMoneda = "", signoMoneda = "", nombreMoneda = "", mensajeMoneda = "";

                var param = "{ 'IdMoneda': " + idTipoMoneda + "}";
                $.ajax({
                    type: "POST",
                    url: wsMantenimientoMoneda + "/ObtenerMoneda",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: param,
                    success: function (resp) {
                        var oMoneda = resp.d;

                        idDivMonera = "divMoneda" + oMoneda.Descripcion;
                        idtxtMoneda = "txtMoneda" + oMoneda.Descripcion;
                        idSpanMoneda = "spMoneda" + oMoneda.Descripcion;
                        signoMoneda = oMoneda.Signo;
                        nombreMoneda = oMoneda.Descripcion + ":";
                        mensajeMoneda = "Facturar en " + oMoneda.Descripcion;

                        if (!$("#" + idtxtMoneda).length || !$("#" + idtxtMoneda).length) {
                            var objMonedaFacturacionBE = new MonedaFacturacionBE();
                            objMonedaFacturacionBE.IdFacturacion = $("#hfIdFacturacion").val();
                            objMonedaFacturacionBE.IdMoneda = idTipoMoneda;

                            var param = "{ 'oMonedaFacturacionBE': " + JSON.stringify(objMonedaFacturacionBE) + "}";
                            $.ajax({
                                type: "POST",
                                url: wsAdministrarFacturacion + "/GuardarMonedaFacturacion",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: param,
                                success: function (resp) {
                                    var vResult = (typeof resp.d) == 'string' ? eval('(' + resp.d + ')') : resp.d;
                                    var msj = vResult.Mensaje.split('&');

                                    if (vResult != null) {
                                        if (vResult.Resultado == 'OK')
                                            eserror = false;

                                        mensajeResult = msj[0];
                                    }
                                    if (!eserror) {
                                        $("#divMoneda").append("<div id='" + idDivMonera + "' class='form-group'>" +
                                                        "<div class='col-xs-12 col-sm-12'>" +
                                                           "<label class='col-sm-4 text-right'><span class='required'></span></label>" +
                                                           "<div class='col-sm-7'>" +
                                                             "<div class='col-sm-10' style='padding-left: 0px; padding-right: 0px;'>" +
                                                               "<div class='input-group'>" +
                                                                  "<span id='" + idSpanMoneda + "' class='input-group-addon'>" + signoMoneda + "</span>" +
                                                                  "<input id='" + idtxtMoneda + "' type='text' class='form-control validate-input-popup decimal-input validate[required]' disabled value='" + mensajeMoneda + "' />" +
                                                                "</div>" +
                                                              "</div>" +
                                                              "<div class='col-sm-2'>" +
                                                                "<button id='btnQuitarMoneda' type='button' idDivMoneda='" + idDivMonera + "' idMonedaFacturacion='" + msj[1] + "' class='btn btn-danger btn-sm btnQuitarMoneda'><i class='fa fa-minus-circle'></i></button>" +
                                                              "</div>" +
                                                           "</div>" +
                                                        "</div>" +
                                                    "</div>");

                                        $(".btnQuitarMoneda").click(function (e) {
                                            var idDiv = $(this).attr("idDivMoneda");
                                            var idMonedaFac = $(this).attr("idMonedaFacturacion");

                                            EliminarMonedaFacturacion(idMonedaFac, idDiv);
                                        });
                                    }

                                    return true;
                                },
                                error: function (request, status, error) {
                                    showMensaje(mensajeResult, eserror);
                                    return true;
                                }
                            });
                        }

                        return true;
                    },
                    error: function (request, status, error) {
                        showMensaje("Error al crear moneda", true);
                        return true;
                    }
                });
            });

            $("#btnAceptar").click(function (e) {
                var isValidate = true;
                isValidate = $("#frmRegistrarFacturacion").validationEngine('validate');
                if (!isValidate) return false;

                var divChildMoneda = $("#divMoneda").children();

                if (!divChildMoneda.length > 0) {
                    showMensaje("Debe agregar una moneda.", true);
                    return;
                }

                OpenConfirmation(null, null, '¿Est&aacute; seguro de realizar la acción de guardar?', 'Aceptar', 'Espere mientras termina la operaci&oacute;n.', 'fGuardarFacturacion');
            });

            $("#btnCancelar").click(function (e) {
                CloseDialog($("#frmRegistrarFacturacion").parent());
            });
        }

        function EliminarMonedaFacturacion(idMonedaFac, idDiv) {
            var param = "{ 'IdMonedaFacturacion': " + idMonedaFac + "}";
            $.ajax({
                type: "POST",
                url: wsAdministrarFacturacion + "/EliminarMonedaFacturacion",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: param,
                success: function (resp) {
                    var vResult = (typeof resp.d) == 'string' ? eval('(' + resp.d + ')') : resp.d;

                    if (vResult != null) {
                        if (vResult.Resultado == 'OK')
                            eserror = false;

                        mensajeResult = vResult.Mensaje;
                    }
                    if (!eserror) {
                        $("#" + idDiv).remove();
                    }
                    return true;
                },
                error: function (request, status, error) {
                    showMensaje(mensajeResult, eserror);
                    return true;
                }
            });
        }

        function fListarMonedaFacturacion() {
            var param = "{ 'IdFacturacion': " + $("#hfIdFacturacion").val() + "}";
            $.ajax({
                type: "POST",
                url: wsAdministrarFacturacion + "/ListarMonedaFacturacion",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: param,
                success: function (resp) {
                    $(resp.d).each(function (i, data) {
                        var idTipoMoneda = data.IdMoneda;
                        var idDivMonera = "", idtxtMoneda = "", idSpanMoneda = "", signoMoneda = "", nombreMoneda = "", mensajeMoneda = "";

                        var param = "{ 'IdMoneda': " + idTipoMoneda + "}";
                        $.ajax({
                            type: "POST",
                            url: wsMantenimientoMoneda + "/ObtenerMoneda",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: param,
                            success: function (resp) {
                                var oMoneda = resp.d;

                                idDivMonera = "divMoneda" + oMoneda.Descripcion;
                                idtxtMoneda = "txtMoneda" + oMoneda.Descripcion;
                                idSpanMoneda = "spMoneda" + oMoneda.Descripcion;
                                signoMoneda = oMoneda.Signo;
                                nombreMoneda = oMoneda.Descripcion + ":";
                                mensajeMoneda = "Facturar en " + oMoneda.Descripcion;

                                $("#divMoneda").append("<div id='" + idDivMonera + "' class='form-group'>" +
                                                "<div class='col-xs-12 col-sm-12'>" +
                                                   "<label class='col-sm-4 text-right'><span class='required'></span></label>" +
                                                   "<div class='col-sm-7'>" +
                                                     "<div class='col-sm-10' style='padding-left: 0px; padding-right: 0px;'>" +
                                                       "<div class='input-group'>" +
                                                          "<span id='" + idSpanMoneda + "' class='input-group-addon'>" + signoMoneda + "</span>" +
                                                          "<input id='" + idtxtMoneda + "' type='text' class='form-control validate-input-popup decimal-input validate[required]' disabled value='" + mensajeMoneda + "' />" +
                                                        "</div>" +
                                                      "</div>" +
                                                      "<div class='col-sm-2'>" +
                                                        "<button id='btnQuitarMoneda' type='button' idDivMoneda='" + idDivMonera + "' idMonedaFacturacion='" + data.IdMonedaFacturacion + "' class='btn btn-danger btn-sm btnQuitarMoneda'><i class='fa fa-minus-circle'></i></button>" +
                                                      "</div>" +
                                                   "</div>" +
                                                "</div>" +
                                            "</div>");

                                $(".btnQuitarMoneda").click(function (e) {
                                    var idDiv = $(this).attr("idDivMoneda");
                                    var idMonedaFac = $(this).attr("idMonedaFacturacion");

                                    EliminarMonedaFacturacion(idMonedaFac, idDiv);
                                });
                                return true;
                            },
                            error: function (request, status, error) {
                                showMensaje("Error al crear moneda", true);
                                return true;
                            }
                        });
                    });
                    return true;
                },
                error: function (request, status, error) {
                    showMensaje("Error al cargar las monedas.", true);
                    return true;
                }
            });
        }

        function fValidarChecks() {
            $(".iTarifaPlana").prop("disabled", true);
            $(".iTarifaHoras").prop("disabled", true);
            $(".iTarifaMixta").prop("disabled", true);

            if ($("#rbTarifaPlana").is(':checked')) {
                $(".iTarifaPlana").prop("disabled", false);

                $(".iTarifaHoras").val("");
                $(".iTarifaMixta").val("");

                $("#hfTipoFacturacion").val($("#rbTarifaPlana").val());
            }
            else if ($("#rbTarifaHoras").is(':checked')) {
                $(".iTarifaHoras").prop("disabled", false);

                $(".iTarifaPlana").val("");
                $(".iTarifaMixta").val("");

                $("#hfTipoFacturacion").val($("#rbTarifaHoras").val());
            }
            else if ($("#rbMixto").is(':checked')) {
                $(".iTarifaMixta").prop("disabled", false);

                $(".iTarifaPlana").val("");
                $(".iTarifaHoras").val("");

                $("#hfTipoFacturacion").val($("#rbMixto").val());
            }
        }

        function fGuardarFacturacion(dialogConfirm) {
            var eserror = true;
            var mensajeResult = "Ocurri&oacute; un error al acceder al servicio web, intente nuevamente";

            var objClienteBE = new ClienteBE();
            objClienteBE.IdCliente = $("#hfIdCliente").val() != "" ? $.trim($("#hfIdCliente").val()) : 0;

            var objContactoBE = new ContactoBE();
            objContactoBE.IdContacto = $("#slContactoFactura").val() != "" ? $.trim($("#slContactoFactura").val()) : "";

            var objFacturacionBE = new FacturacionBE();
            objFacturacionBE.IdFacturacion = $("#hfIdFacturacion").val() != "" ? $.trim($("#hfIdFacturacion").val()) : 0;

            var tipoFacturacion = $("#hfTipoFacturacion").val();

            if (tipoFacturacion == 1) {
                objFacturacionBE.TipoFacturacion = 1;
                objFacturacionBE.TarifaFija = $("#txtMontoTarifaPlana").val() != "" ? $.trim($("#txtMontoTarifaPlana").val()) : "";
            }
            else if (tipoFacturacion == 2) {
                objFacturacionBE.TipoFacturacion = 2;
                objFacturacionBE.TarifaHoras = $("#txtMontoTarifaHoras").val() != "" ? $.trim($("#txtMontoTarifaHoras").val()) : "";
            }
            else if (tipoFacturacion == 3) {
                objFacturacionBE.TipoFacturacion = 3;
                objFacturacionBE.TarifaFija = $("#txtMontoTarifaMixta").val() != "" ? $.trim($("#txtMontoTarifaMixta").val()) : "";
                objFacturacionBE.TarifaHoras = $("#txtMontoTarifaHora").val() != "" ? $.trim($("#txtMontoTarifaHora").val()) : "";
                objFacturacionBE.TarifaHorasAdicionales = $("#txtMontoAdicional").val() != "" ? $.trim($("#txtMontoAdicional").val()) : "";
            }

            objFacturacionBE.MontoFlat = $("#txtMontoFlat").val() != "" ? $.trim($("#txtMontoFlat").val()) : 0;
            objFacturacionBE.FechaFactura = $("#txtFechaFacturacion").val() != "" ? $.trim($("#txtFechaFacturacion").val()) : 0;
            objFacturacionBE.Direccion = $("#txtDireccionFactura").val() != "" ? $.trim($("#txtDireccionFactura").val()) : "";
            objFacturacionBE.Contacto = objContactoBE;
            objFacturacionBE.Cliente = objClienteBE;

            var param = "{ 'oFacturacion': " + JSON.stringify(objFacturacionBE) + "}";
            $.ajax({
                type: "POST",
                url: wsAdministrarFacturacion + "/GuardarFacturacion",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: param,
                success: function (resp) {
                    var vResult = (typeof resp.d) == 'string' ? eval('(' + resp.d + ')') : resp.d;

                    if (vResult != null) {
                        if (vResult.Resultado == 'OK')
                            eserror = false;
                        dialogConfirm.close();
                        mensajeResult = vResult.Mensaje;
                    }
                    if (!eserror) {
                        dialogConfirm.close();
                        showMensaje(mensajeResult, eserror);
                        CloseDialog($("#frmRegistrarFacturacion").parent());
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
