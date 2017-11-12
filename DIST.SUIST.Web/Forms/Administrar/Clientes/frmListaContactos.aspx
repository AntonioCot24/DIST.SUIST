<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmListaContactos.aspx.cs" Inherits="DIST.SUIST.Web.frmListaContactos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="frmListaContactos" class="form-horizontal formu" runat="server">
        <input id="hfIdContacto" type="hidden" runat="server"/>
        <input id="hfIdClienteContacto" type="hidden" runat="server"/>
        <div class="panel panel-default" style="padding-top: 5px; padding-bottom: 0; margin-bottom: 0">
            <div class="panel-body">
                <div class="form-group" style="margin-top:0; margin-bottom:0">
                    <div id="dtContacto" class="col-xs-12 col-sm-12"></div>
                </div>
                <div class="form-gruop">
                    <div class="col-xs-12 col-sm-12 text-center">
                        <button id="btnNuevoContacto" type="button" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;Nuevo</button>
                        <button id="btnCancelarContacto" type="button" class="btn btn-danger"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var wsMantenimientoContacto = "<%= wsMantenimientoContacto %>";
        var urlRegistroContacto = "<%= urlRegistroContacto %>";

        var ContactoBE = function () {
            this.IdContacto = 0;
            this.Nombre = "";
            this.Direccion = "";
            this.TelefonoFijo = "";
            this.TelefonoCelular = "";
            this.Correo = "";
            this.Cargo = "";
            this.Principal = true;

            this.Cliente = new ClienteBE();
        }

        $(function () {
            fListarContactos();
            fValidarFormulario();
            fAgregarEventosBotones();
        });

        function fValidarFormulario() {

        }

        function fAgregarEventosBotones() {
            OpenWindow('#btnNuevoContacto', urlRegistroContacto, "", false, true, 'n', "Registrar Contacto");

            $("#btnCancelarContacto").click(function (e) {
                CloseDialog($("#frmListaContactos").parent());
            });
        }

        function fListarContactos() {
            var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            var param = "{ 'IdCliente': " + $("#hfIdClienteContacto").val() + "}";
            $.ajax({
                type: "POST",
                url: wsMantenimientoContacto + "/ListarContacto",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: param,
                cache: false,
                success: function (resp) {
                    var vResult = (typeof resp.d) == 'string' ? eval('(' + resp.d + ')') : resp.d;
                    var eserror = true;
                    if (vResult != null) {
                        if (vResult.Resultado == 'OK')
                            eserror = false;
                        mensajeResult = htmlDecode(vResult.Mensaje);
                    }
                    $("#hfIdContacto").val("");
                    if (!eserror) {
                        fConsultarContactos(vResult.Listado);
                    }
                    else {
                        fConsultarContactos(vResult.Listado);
                    }
                    return true;
                },
                error: function (request, status, error) {
                    showMensaje(mensajeResult, eserror);
                    return true;
                }
            });
        }

        function fConsultarContactos(result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            var data = jQuery.parseJSON(result);

            $('#dtContacto').html('<table cellpadding="0" cellspacing="0" border="0" id="tbContacto" class="table table-striped table-bordered"></table>');
            $('#tbContacto').dataTable({
                "sProcessing": true,
                "iDisplayLength": 5,
                "bPaginate": true,
                "sPaginationType": "full_numbers",
                "bJQueryUI": false,
                "bLengthChange": false,
                "ordering": true,
                "searching": true,
                //"sScrollX": true,
                //"sScrollY": 300,
                //"bAutoWidth": false,
                "fnDrawCallback": function (oSettings) {
                    ActivarBackspace("tbContacto_filter");
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                    $("td:first", nRow).html(iDisplayIndex + 1);
                    return nRow;
                },
                "oLanguage": DatatableSp,
                "aaData": data,
                "aoColumns": [
                { "mDataProp": "col_IdContacto", "sTitle": "col_IdContacto", "bSearchable": false, "bVisible": false },
                { "mDataProp": "col_Principal", "sTitle": "col_Principal", "bSearchable": false, "bVisible": false },
                { "mDataProp": null, "sTitle": "Nº", "bSearchable": false, "bVisible": true },
                { "mDataProp": "col_NombreCompleto", "sTitle": "Nombre", "bSearchable": true, "bVisible": true },
                { "mDataProp": null, "sTitle": "Opciones", "bSearchable": false, "sClass": "text-center", "bVisible": true }
                ],
                "aoColumnDefs": [
                    {
                        "mRender": function (data, type, row) {
                            var btnEditar = "", btnEliminar = "";

                            btnEditar = "<button id='btn_" + row['col_IdContacto'] + "_edit' type='button' class='btn btn-primary btn-sm btnEditar' idContacto=" + row['col_IdContacto'] + " title='Editar'><i class='fa fa-edit'></i></button>";

                            btnEliminar = "<button id='btn_" + row['col_IdContacto'] + "_elm' type='button' class='btn btn-danger btn-sm btnEliminar' idContacto=" + row['col_IdContacto'] + " title='Eliminar'><i class='fa fa-times'></i></button>";

                            return btnEditar + ' ' + btnEliminar;
                        },
                        "aTargets": [4]
                    }
                ]
            });

            $('#tbContacto tbody').on('click', 'button', function () {
                $("#hfIdContacto").val($(this).attr("idContacto"));

                if ($(this).hasClass('btnEditar')) {
                    OpenWindowClick($(this), urlRegistroContacto, "idContacto", false, true, 'n', "Editar Contacto");
                }

                if ($(this).hasClass('btnEliminar')) {
                    OpenConfirmation(null, null, '¿Est&aacute; seguro de eliminar el registro seleccionado?', 'Aceptar', 'Espere mientras termina la operaci&oacute;n.', 'fEliminarContacto');
                }
            });
        }

        function fEliminarContacto(dialogConfirm) {
            var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            var IdContacto = $("#hfIdContacto").val() != "" ? $.trim($("#hfIdContacto").val()) : 0;

            var param = "{ 'IdContacto': " + IdContacto + "}";
            $.ajax({
                type: "POST",
                url: wsMantenimientoContacto + "/EliminarContacto",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
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
                        $("#hfIdContacto").val("");
                        fListarContactos();
                        showMensaje(mensajeResult, eserror);
                        dialogConfirm.close();
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
