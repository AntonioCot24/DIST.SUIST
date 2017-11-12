<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRegistroProyectos.aspx.cs" Inherits="DIST.SUIST.Web.frmRegistroProyectos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="frmRegistroProyectos" class="form-horizontal formu form-label-left" runat="server">
        <div class="panel panel-default" style="padding-top: 5px; padding-bottom: 0; margin-bottom: 0">
            <div class="panel-body">
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Clientes: <span class="required">*</span></label>
                        <div class="col-sm-7">
                            <select id="slCliente" class="form-control col-md-7 col-xs-12 validate[required]" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Nombre Proyecto: <span class="required">*</span></label>
                        <div class="col-sm-7">
                            <input id="txtNombreProyecto" type="text" class="form-control col-md-7 col-xs-12 validate[required]" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Precio: <span class="required">*</span></label>
                        <div class="col-sm-7">
                            <input id="txtPrecio" type="text" class="form-control col-md-7 col-xs-12 validate[required]" runat="server" />
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
                isValidate = $("#frmRegistroProyectos").validationEngine('validate');
                if (!isValidate) return false;

                OpenConfirmation(null, null, '¿Est&aacute; seguro de realizar la acción de guardar?', 'Aceptar', 'Espere mientras termina la operaci&oacute;n.', 'fGuardarProyecto');
            });

            $("#btnCancelar").click(function (e) {
                CloseDialog($("#frmRegistroProyectos").parent());
            });
        }

        function fGuardarProyecto(dialogConfirm) {
            var mensajeResult = "Ocurri&oacute; un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            var objClienteBE = new ClienteBE();
            objClienteBE.IdCliente = $("#slCliente").val() != "" ? $.trim($("#slCliente").val()) : 0;

            var objProyectoBE = new ProyectoBE();
            objProyectoBE.IdProyecto = $("#hfIdProyecto").val() != "" ? $.trim($("#hfIdProyecto").val()) : 0;
            objProyectoBE.NombreProyecto = $("#txtNombreProyecto").val() != "" ? $.trim($("#txtNombreProyecto").val()) : "";
            objProyectoBE.Precio = $("#txtPrecio").val() != "" ? $.trim($("#txtPrecio").val()) : 0;
            objProyectoBE.Cliente = objClienteBE;

            var param = "{ 'oProyecto': " + JSON.stringify(objProyectoBE) + "}";
            $.ajax({
                type: "POST",
                url: wsMantenimientoProyecto + "/GuardarProyecto",
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
                        if ($("#frmAdministrarProyectos").length) {
                            fListarProyectos();
                        }
                        if ($("#frmRegistrarActividad").length) {
                            fCargarComboProyecto($.trim($("#slCliente").val()));
                        }
                        $("#hfIdProyecto").val("");
                        CloseDialog($("#frmRegistroProyectos").parent());
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
