<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRegistrarClientes.aspx.cs" Inherits="DIST.SUIST.Web.frmRegistrarClientes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="frmRegistrarClientes" class="form-horizontal formu form-label-left" runat="server">
        <input id="hfOpcionCliente" type="hidden" runat="server"/>
        <div class="panel panel-default" style="padding-top: 5px; padding-bottom: 0; margin-bottom: 0">
            <div class="panel-body">
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Tipo: <span class="required">*</span></label>
                        <div class="col-sm-7">
                            <select id="slTipoCliente" class="form-control validate[required]" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Doc. Identidad: <span class="required">*</span></label>
                        <div class="col-sm-7">
                            <input id="txtDocumentoIdentidad" type="text" class="form-control validate[required]" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Nombre: <span class="required">*</span></label>
                        <div class="col-sm-7">
                            <input id="txtNombreCliente" type="text" class="form-control validate[required]" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">E-Mail:</label>
                        <div class="col-sm-7">
                            <input id="txtEmail" type="text" class="form-control validate[custom[email]]" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Teléfono:</label>
                        <div class="col-sm-7">
                            <input id="txtTelefono" type="text" class="form-control" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Sitio Web:</label>
                        <div class="col-sm-7">
                            <input id="txtSitioWeb" type="text" class="form-control" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Dirección:</label>
                        <div class="col-sm-7">
                            <input id="txtDireccion" type="text" class="form-control" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Inicio Contrato:</label>
                        <div class="col-sm-7">
                            <div class="input-group">
                                <input id="txtInicioContrato" type="text" class="form-control date-pick-popup" runat="server" />
                                <span id="btnInicioContrato" class="input-group-addon" style="cursor: pointer;">
                                    <span class="fa fa-calendar"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Fin Contrato:</label>
                        <div class="col-sm-7">
                            <div class="input-group">
                                <input id="txtFinContrato" type="text" class="form-control date-pick-popup" runat="server" />
                                <span id="btnFinContrato" class="input-group-addon" style="cursor: pointer;">
                                    <span class="fa fa-calendar"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12 col-sm-12">
                        <label class="col-sm-4 text-right">Color:</label>
                        <div class="col-sm-7">
                            <div id="gColor" class="input-group">
                                <input id="txtColor" type="text" class="form-control" readonly runat="server" />
                                <span id="btnColor" class="input-group-addon" style="cursor: pointer;"><i></i></span>
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
            var inputColor = $("#txtColor").val();
            
            if (inputColor != "")
                $("#gColor").colorpicker({
                    color: inputColor
                });
            else
                $("#gColor").colorpicker();
        }

        function fAgregarEventosBotones() {
            $("#btnAceptar").click(function (e) {
                var isValidate = true;
                isValidate = $("#frmRegistrarClientes").validationEngine('validate');
                if (!isValidate) return false;

                OpenConfirmation(null, null, '¿Est&aacute; seguro de realizar la acción de guardar?', 'Aceptar', 'Espere mientras termina la operaci&oacute;n.', 'fGuardarCliente');
            });

            $("#btnCancelar").click(function (e) {
                CloseDialog($("#frmRegistrarClientes").parent());
            });
        }

        function fGuardarCliente(dialogConfirm) {
            var mensajeResult = "Ocurri&oacute; un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            var objTipoClienteBE = new TipoClienteBE();
            objTipoClienteBE.IdTipoCliente = $("#slTipoCliente").val() != "" ? $.trim($("#slTipoCliente").val()) : 0;

            var objClienteBE = new ClienteBE();
            if ($("#hfOpcionCliente").val() == 'E') {
                objClienteBE.IdCliente = $("#hfIdCliente").val() != "" ? $.trim($("#hfIdCliente").val()) : 0;
            }
            objClienteBE.DocumentoIdentidad = $("#txtDocumentoIdentidad").val() != "" ? $.trim($("#txtDocumentoIdentidad").val()) : "";
            objClienteBE.NombreCompleto = $("#txtNombreCliente").val() != "" ? $.trim($("#txtNombreCliente").val()) : "";
            objClienteBE.Email = $("#txtEmail").val() != "" ? $.trim($("#txtEmail").val()) : "";
            objClienteBE.Telefono = $("#txtTelefono").val() != "" ? $.trim($("#txtTelefono").val()) : "";
            objClienteBE.SitioWeb = $("#txtSitioWeb").val() != "" ? $.trim($("#txtSitioWeb").val()) : "";
            objClienteBE.Direccion = $("#txtDireccion").val() != "" ? $.trim($("#txtDireccion").val()) : "";
            objClienteBE.FechaInicioContrato = $("#txtInicioContrato").val() != "" ? formatDate($("#txtInicioContrato").val()) : "";
            objClienteBE.FechaFinContrato = $("#txtFinContrato").val() != "" ? formatDate($("#txtFinContrato").val()) : "";
            objClienteBE.Color = $("#txtColor").val() != "" ? $.trim($("#txtColor").val()) : "";
            objClienteBE.TipoCliente = objTipoClienteBE;

            var param = "{ 'oCliente': " + JSON.stringify(objClienteBE) + "}";
            $.ajax({
                type: "POST",
                url: wsMantenimientoCliente + "/GuardarCliente",
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
                        if ($("#frmAdministrarClientes").length) {
                            fListarClientes();
                        }
                        if ($("#frmRegistrarActividad").length) {
                            fCargarComboClientes();
                        }
                        $("#hfIdCliente").val("");
                        CloseDialog($("#frmRegistrarClientes").parent());
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
