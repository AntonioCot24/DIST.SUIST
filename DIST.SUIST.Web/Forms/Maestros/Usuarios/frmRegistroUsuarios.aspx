<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRegistroUsuarios.aspx.cs" Inherits="DIST.SUIST.Web.frmRegistroUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="frmRegistroUsuarios" class="form-horizontal formu form-label-left" runat="server">
        <div class="panel panel-default" style="padding-top: 5px; padding-bottom: 0; margin-bottom: 0">
            <div class="panel-body">
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Nombre: <span class="required">*</span></label>
                        <div class="col-sm-7">
                            <input id="txtNombreUsuario" type="text" class="form-control col-md-7 col-xs-12 validate[required]" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Perfil: <span class="required">*</span></label>
                        <div class="col-sm-7">
                            <select id="slPerfil" class="form-control col-md-7 col-xs-12 validate[required]" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">E-mail: <span class="required">*</span></label>
                        <div class="col-sm-7">
                            <input id="txtEmail" type="text" class="form-control col-md-7 col-xs-12 validate[required],custom[email]" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Contraseña: <span class="required">*</span></label>
                        <div class="col-sm-7">
                            <input id="txtContrasenia" type="text" class="form-control col-md-7 col-xs-12 validate[required]" runat="server" />
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

        }

        function fAgregarEventosBotones() {
            $("#btnAceptar").click(function (e) {
                var isValidate = true;
                isValidate = $("#frmRegistroUsuarios").validationEngine('validate');
                if (!isValidate) return false;

                OpenConfirmation(null, null, '¿Est&aacute; seguro de realizar la acción de guardar?', 'Aceptar', 'Espere mientras termina la operaci&oacute;n.', 'fGuardarUsuario');
            });

            $("#btnCancelar").click(function (e) {
                CloseDialog($("#frmRegistroUsuarios").parent());
            });
        }

        function fGuardarUsuario(dialogConfirm) {
            var mensajeResult = "Ocurri&oacute; un error al acceder al servicio web, intente nuevamente";

            var objPerfilBE = new PerfilBE();
            objPerfilBE.IdPerfil = $("#slPerfil").val() != "" ? $.trim($("#slPerfil").val()) : 0;

            var objUsuarioBE = new UsuarioBE();
            objUsuarioBE.IdUsuario = $("#hfIdUsuario").val() != "" ? $.trim($("#hfIdUsuario").val()) : 0;
            objUsuarioBE.Usuario = $("#txtEmail").val() != "" ? $.trim($("#txtEmail").val()) : "";
            objUsuarioBE.Contrasenia = $("#txtContrasenia").val() != "" ? $.trim($("#txtContrasenia").val()) : 0;
            objUsuarioBE.NombreCompleto = $("#txtNombreUsuario").val() != "" ? $.trim($("#txtNombreUsuario").val()) : "";
            objUsuarioBE.Perfil = objPerfilBE;

            var param = "{ 'oUsuario': " + JSON.stringify(objUsuarioBE) + "}";
            $.ajax({
                type: "POST",
                url: wsMantenimientoUsuario + "/GuardarUsuario",
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
                        fListarUsuarios();
                        $("#hfIdUsuario").val("");
                        CloseDialog($("#frmRegistroUsuarios").parent());
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
