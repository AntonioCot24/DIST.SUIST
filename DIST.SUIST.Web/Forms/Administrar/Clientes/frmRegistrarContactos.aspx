<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRegistrarContactos.aspx.cs" Inherits="DIST.SUIST.Web.frmRegistrarContactos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="frmRegistrarContactos" class="form-horizontal formu form-label-left" runat="server">
        <input id="hfOpcionContacto" type="hidden" runat="server"/>
        <div class="panel panel-default" style="padding-top: 5px; padding-bottom: 0; margin-bottom: 0">
            <div class="panel-body">
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Nombre: <span class="required">*</span></label>
                        <div class="col-sm-7">
                            <input id="txtNombreContacto" type="text" class="form-control validate[required]" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Dirección:</label>
                        <div class="col-sm-7">
                            <input id="txtDireccionContacto" type="text" class="form-control" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Telf. Fijo (Anexo):</label>
                        <div class="col-sm-7">
                            <input id="txtTelfFijoContacto" type="text" class="form-control" maxlength="30" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Telf. Celular:</label>
                        <div class="col-sm-7">
                            <input id="txtTelfCelularContacto" type="text" class="form-control validate-input-popup numeric-input" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Correo:</label>
                        <div class="col-sm-7">
                            <input id="txtCorreoContacto" type="text" class="form-control validate[custom[email]]" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Cargo:</label>
                        <div class="col-sm-7">
                            <input id="txtCargoContacto" type="text" class="form-control" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Cont. Principal:</label>
                        <div class="col-sm-7">
                            <div class="radio-inline" style="padding-top: 0;">
                                <label>
                                    <input id="rbSiPrincial" type="radio" name="radio-inline-contacto" runat="server" />Si<i class="fa fa-circle-o small"></i>
                                </label>
                            </div>
                            <div class="radio-inline" style="padding-top: 0;">
                                <label>
                                    <input id="rbNoPrincial" type="radio" name="radio-inline-contacto" runat="server" />No<i class="fa fa-circle-o small"></i>
                                </label>
                            </div>
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
        $(function () {
            fValidarFormulario();
            fAgregarEventosBotones();
        });

        function fValidarFormulario() {
            if ($("#hfIdContacto").val() == "")
                $("#rbNoPrincial").prop('checked', true);
        }

        function fAgregarEventosBotones() {
            $("#btnAceptar").click(function (e) {
                var isValidate = true;
                isValidate = $("#frmRegistrarContactos").validationEngine('validate');
                if (!isValidate) return false;

                OpenConfirmation(null, null, '¿Est&aacute; seguro de realizar la acción de guardar?', 'Aceptar', 'Espere mientras termina la operaci&oacute;n.', 'fGuardarContacto');
            });

            $("#btnCancelar").click(function (e) {
                CloseDialog($("#frmRegistrarContactos").parent());
            });
        }

        function fGuardarContacto(dialogConfirm) {
            var mensajeResult = "Ocurri&oacute; un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            var objClienteBE = new ClienteBE();
            objClienteBE.IdCliente = $("#hfIdClienteContacto").val() != "" ? $.trim($("#hfIdClienteContacto").val()) : 0;

            var objContactoBE = new ContactoBE();
            if ($("#hfOpcionContacto").val() == 'E') {
                objContactoBE.IdContacto = $("#hfIdContacto").val() != "" ? $.trim($("#hfIdContacto").val()) : 0;
            }
            objContactoBE.Nombre = $("#txtNombreContacto").val() != "" ? $.trim($("#txtNombreContacto").val()) : "";
            objContactoBE.Direccion = $("#txtDireccionContacto").val() != "" ? $.trim($("#txtDireccionContacto").val()) : "";
            objContactoBE.TelefonoFijo = $("#txtTelfFijoContacto").val() != "" ? $.trim($("#txtTelfFijoContacto").val()) : "";
            objContactoBE.TelefonoCelular = $("#txtTelfCelularContacto").val() != "" ? $.trim($("#txtTelfCelularContacto").val()) : "";
            objContactoBE.Correo = $("#txtCorreoContacto").val() != "" ? $.trim($("#txtCorreoContacto").val()) : "";
            objContactoBE.Cargo = $("#txtCargoContacto").val() != "" ? $.trim($("#txtCargoContacto").val()) : "";
            objContactoBE.Principal = $("#rbSiPrincial").is(':checked') ? true : false;
            objContactoBE.Cliente = objClienteBE;

            var param = "{ 'oContacto': " + JSON.stringify(objContactoBE) + "}";
            $.ajax({
                type: "POST",
                url: wsMantenimientoContacto + "/GuardarContacto",
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
                        if ($("#frmListaContactos").length) {
                            fListarContactos();
                        }
                        if ($("#frmRegistrarActividad").length) {
                            fCargarComboContactos($.trim($("#hfIdCliente").val()));
                        }

                        $("#hfIdContacto").val("");
                        CloseDialog($("#frmRegistrarContactos").parent());
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
