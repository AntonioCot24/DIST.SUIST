<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRegistrarGasto.aspx.cs" Inherits="DIST.SUIST.Web.frmRegistrarGasto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="frmRegistrarGasto" class="form-horizontal formu form-label-left" runat="server">
        <input id="hfIdGasto" type="hidden" runat="server" />
        <input id="hfIdUsuario" type="hidden" runat="server" />
        <div class="panel panel-default" style="padding-top: 5px; padding-bottom: 0; margin-bottom: 0">
            <div class="panel-body" style="padding-bottom: 0px;">
                <div class="form-group">
                    <label class="col-sm-3 text-right">Abogado: <span class="required">*</span></label>
                    <div class="col-sm-8">
                        <input id="txtAbogado" runat="server" type="text" class="form-control validate[required]" disabled="disabled" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 text-right">Cliente: <span class="required">*</span></label>
                    <div class="col-sm-8">
                        <select id="slCliente" runat="server" class="form-control validate[required] chosen" />
                    </div>
                </div>
                <div id="divProyecto" class="form-group">
                    <label class="col-sm-3 text-right">Proyecto:</label>
                    <div class="col-sm-8">
                        <select id="slProyecto" runat="server" class="form-control chosen" disabled="disabled" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 text-right">Fecha: <span class="required">*</span></label>
                    <div class="col-sm-8">
                        <div class="input-group">
                            <input id="txtFechaGasto" runat="server" type="text" class="form-control date-pick-popup validate[required]" />
                            <span id="btnFechaGasto" class="input-group-addon" style="cursor: pointer;"><span class="fa fa-calendar"></span></span>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 text-right">Monto: <span class="required">*</span></label>
                    <div class="col-sm-8">
                        <input id="txtMonto" runat="server" type="text" class="form-control validate[required] validate-input-popup decimal-input" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 text-right">Glosa: <span class="required">*</span></label>
                    <div class="col-sm-8">
                        <textarea id="taGlosa" runat="server" type="text" rows="3" class="form-control validate[required]" />
                    </div>
                </div>
                <div class="form-gruop text-center">
                    <button id="btnAceptar" type="button" class="btn btn-primary"><i class="fa fa-check"></i>&nbsp;Aceptar</button>
                    <button id="btnCancelar" type="button" class="btn btn-danger"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
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
            $("#slCliente").on("change", function () {
                var idCliente = $(this).val();

                fValidarCliente();
                fCargarComboProyecto(idCliente);
            });
        }

        function fAgregarEventosBotones() {
            $("#btnAceptar").click(function (e) {
                var isValidate = true;
                isValidate = $("#frmRegistrarGasto").validationEngine('validate');
                if (!isValidate) return false;

                OpenConfirmation(null, null, '¿Est&aacute; seguro de realizar la acción de guardar?', 'Aceptar', 'Espere mientras termina la operaci&oacute;n.', 'fGuardarGasto');
            });

            $("#btnCancelar").click(function (e) {
                CloseDialog($("#frmRegistrarGasto").parent());
            });
        }

        function fValidarCliente() {
            if ($("#slCliente").val() == "") {
                BloquearDiv("divProyecto", true);
            }
            else {
                ActivarDiv("divProyecto", true);
            }
        }

        function fCargarComboProyecto(IdCliente) {
            var parametros = "{ 'idCliente': " + IdCliente + "}";
            var url = wsAdministrarProyecto + "/ListarProyectoCliente";

            fLoadSelect(url, $("#slProyecto"), parametros, "IdProyecto", "NombreProyecto", "", "", true, "");
        }

        function fGuardarGasto(dialogConfirm) {
            var mensajeResult = "Ocurri&oacute; un error al acceder al servicio web, intente nuevamente";

            var objGastoBE = new GastoBE();
            objGastoBE.IdGasto = $("#hfIdGasto").val() != "" ? $.trim($("#hfIdGasto").val()) : 0;
            objGastoBE.IdCliente = $("#slCliente").val() != "" ? $.trim($("#slCliente").val()) : 0;
            objGastoBE.IdProyecto = $("#slProyecto").val() != "" ? $.trim($("#slProyecto").val()) : 0;
            objGastoBE.IdUsuario = $("#hfIdUsuario").val() != "" ? $.trim($("#hfIdUsuario").val()) : 0;
            objGastoBE.Fecha = $("#txtFechaGasto").val() != "" ? formatDate($.trim($("#txtFechaGasto").val())) : null;
            objGastoBE.Glosa = $("#taGlosa").val() != "" ? $.trim($("#taGlosa").val()) : "";
            objGastoBE.Monto = $("#txtMonto").val() != "" ? $.trim($("#txtMonto").val()) : 0;

            var param = "{ 'oGasto': " + JSON.stringify(objGastoBE) + "}";
            $.ajax({
                type: "POST",
                url: wsControlGasto + "/GuardarGasto",
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
                        fListarGastos();
                        $("#hfIdGasto").val("");
                        CloseDialog($("#frmRegistrarGasto").parent());
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
