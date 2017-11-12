<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCambiarContrasenia.aspx.cs" Inherits="DIST.SUIST.Web.frmCambiarContrasenia" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="frmCambiarContrasenia" class="form-horizontal formu form-label-left" runat="server">
    <input id="hfIdUsuario" type="hidden" runat="server" />
        <div class="panel panel-default" style="padding-top: 5px; padding-bottom: 0; margin-bottom: 0">
            <div class="panel-body">
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Contrase&ntilde;a Actual:</label>
                        <div class="col-sm-7">
                            <div class="input-group">
                                <input id="txtConstraseniaActual" type="text" class="form-control validate[required] contrasenia" readonly="true" runat="server" />
                                <span class="input-group-btn">
                                    <button class="btn btn-info reveal" type="button" style="font-size: small;margin: 0;line-height: inherit;"><i class="fa fa-eye"></i></button>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Contrase&ntilde;a Nueva:</label>
                        <div class="col-sm-7">
                            <div class="input-group">
                                <input id="txtContraseniaNueva" type="password" class="form-control validate[required] contrasenia" runat="server" />
                                <span class="input-group-btn">
                                    <button class="btn btn-info reveal" type="button" style="font-size: small;margin: 0;line-height:inherit;"><i class="fa fa-eye"></i></button>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Confirmar Contrase&ntilde;a:</label>
                        <div class="col-sm-7">
                            <div class="input-group">
                                <input id="txtConfirmarContrsenia" type="password" class="form-control validate[required] contrasenia" runat="server" />
                                <span class="input-group-btn">
                                    <button class="btn btn-info reveal" type="button" style="font-size: small;margin: 0;line-height: inherit;"><i class="fa fa-eye"></i></button>
                                </span>
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
        var UsuarioBE = function () {
            this.Idusuario = 0;
            this.Contrasenia = "";
        }
        
        var urlWebServiceSeguridad = '<%= urlWebServiceSeguridad %>';

        $(function () {
            $(".contrasenia").on("blur", function (e) {
                e.preventDefault();
                fValidarFormulario();
            });

            fAgregarEventosBotones();
        });

        function fValidarFormulario() {
            var passAct = $("#txtConstraseniaActual").val(), passNue = $("#txtContraseniaNueva").val(), passCof = $("#txtConfirmarContrsenia").val();

            if (passAct == passNue) {
                showMensaje("La contraseña nuevo no puede ser igual a la anterior.", true);
                return false;
            }
            else if (passCof != "" && passNue != passCof) {
                showMensaje("Las contraseñas no coinciden favor de verificar.", true);
                return false;
            }
            else
                return true;
        }

        function fAddBackSpace() {
            $("#frmCambiarContrasenia").find("input, textarea").on("keydown", function (e) {
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
        }

        function fAgregarEventosBotones() {
            $(".reveal").mousedown(function () {
                var parent = $(this).parent().parent();
                var input = $(parent).find("input");
                fAddBackSpace();

                $(input).replaceWith($(input).clone().prop('type', 'text')).focus();
            })
            .mouseup(function () {
                var parent = $(this).parent().parent();
                var input = $(parent).find("input");
                fAddBackSpace();

                $(input).replaceWith($(input).prop('type', 'password')).focus();
            })
            .mouseout(function () {
                var parent = $(this).parent().parent();
                var input = $(parent).find("input");
                fAddBackSpace();

                $(input).replaceWith($(input).prop('type', 'password')).focus();

            });

            $("#btnAceptar").click(function (e) {
                var isValidate = true;
                isValidate = $("#frmCambiarContrasenia").validationEngine('validate');
                if (!isValidate) return false;

                if (!fValidarFormulario())
                    return;

                OpenConfirmation(null, null, '¿Est&aacute; seguro de cambiar su contraseña?', 'Aceptar', 'Espere mientras termina la operaci&oacute;n.', 'fCambiarContrasenia');
            });

            $("#btnCancelar").click(function (e) {
                CloseDialog($("#frmCambiarContrasenia").parent());
            });
        }

        function fCambiarContrasenia(dialogConfirm) {
            var mensajeResult = "Ocurri&oacute; un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            var objUsuarioBE = new UsuarioBE();
            objUsuarioBE.Idusuario = $("#hfIdUsuario").val() != "" ? $.trim($("#hfIdUsuario").val()) : 0;
            objUsuarioBE.Contrasenia = $("#txtContraseniaNueva").val() != "" ? $.trim($("#txtContraseniaNueva").val()) : "";

            var param = "{ 'oUsuario': " + JSON.stringify(objUsuarioBE) + "}";
            $.ajax({
                type: "POST",
                url: urlWebServiceSeguridad + "/ActualizarContraseniaUsuario",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: param,
                success: function (resp) {
                    var vResult = (typeof resp.d) == 'string' ? eval('(' + resp.d + ')') : resp.d;
                    var msj = vResult.Mensaje.split("|");
                    if (vResult != null) {
                        if (vResult.Resultado == 'OK')
                            eserror = false;
                        dialogConfirm.close();
                        mensajeResult = htmlDecode(msj[0]);
                    }
                    if (!eserror) {
                        dialogConfirm.close();
                        showMensaje(mensajeResult, eserror);
                        CloseDialog($("#frmCambiarContrasenia").parent());
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
