<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMantenerUsuarios.aspx.cs" Inherits="DIST.SUIST.Web.frmMantenerUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="frmMantenerUsuarios" class="form-horizontal formu form-label-left" runat="server">
        <input id="hfIdUsuario" type="hidden" />
        <div class="well">
            <div class="text-center">
                <h3 class="page-header">Mantenimiento de Usuarios</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <div class="box">
                    <div id="bhOpciones" class="box-header" icon="fa fa-cog" titulo="Opciones"></div>
                    <div id="bbOpciones" class="box-content">
                        <div class="form-group" style="margin-top: 0; margin-bottom: 0">
                            <div class="col-sm-12 text-right">
                                <button id="btnNuevoUsuario" type="button" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;Nuevo</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12">
                <div class="box">
                    <div id="bhListaMarca" class="box-header" icon="fa fa-table" titulo="Listado de Usuarios"></div>
                    <div class="box-content">
                        <div class="form-group">
                            <div id="dtUsuario" class="col-xs-12 col-sm-12"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var wsMantenimientoUsuario = "<%= wsMantenimientoUsuario %>";
        var urlRegistroUsuario = "<%= urlRegistroUsuario %>";

        var PerfilBE = function () {
            this.IdPerfil = 0;
        }

        var UsuarioBE = function () {
            this.IdUsuario = 0;
            this.Usuario = "";
            this.Contrasenia = "";
            this.NombreCompleto = "";

            this.Perfil = new PerfilBE();
        }

        $(function () {
            fListarUsuarios();
            fValidarFormulario();
            fAgregarEventosBotones();
        });

        function fValidarFormulario() {

        }

        function fAgregarEventosBotones() {
            OpenWindow('#btnNuevoUsuario', urlRegistroUsuario, "", false, true, 'n', "Registrar Usuario");
            //OpenWindow('#btnRegresar', urlMainMantenimientos, "", false, false, 0, 0, "");
        }

        function fListarUsuarios(filtrar) {
            var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            $.ajax({
                type: "POST",
                url: wsMantenimientoUsuario + "/ListarUsuarios",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (resp) {
                    var vResult = (typeof resp.d) == 'string' ? eval('(' + resp.d + ')') : resp.d;
                    var eserror = true;
                    if (vResult != null) {
                        if (vResult.Resultado == 'OK')
                            eserror = false;
                        mensajeResult = htmlDecode(vResult.Mensaje);
                    }
                    if (!eserror) {
                        fConsultarUsuarios(vResult.Listado);
                    }
                    else {
                        fConsultarUsuarios(vResult.Listado);
                    }
                    return true;
                },
                error: function (request, status, error) {
                    showMensaje(mensajeResult, eserror);
                    return true;
                }
            });
        }

        function fConsultarUsuarios(result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            var data = jQuery.parseJSON(result);

            $('#dtUsuario').html('<table cellpadding="0" cellspacing="0" border="0" id="tbUsuario" class="table table-striped table-bordered"></table>');
            $('#tbUsuario').dataTable({
                "sProcessing": true,
                "bPaginate": true,
                "sPaginationType": "full_numbers",
                "bJQueryUI": false,
                "bLengthChange": true,
                "ordering": true,
                "searching": true,
                //"sScrollX": true,
                //"sScrollY": 300,
                "bAutoWidth": false,
                "fnDrawCallback": function (oSettings) {
                    ActivarBackspace("tbUsuario_filter");
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                    $("td:first", nRow).html(iDisplayIndex + 1);
                    return nRow;
                },
                "oLanguage": DatatableSp,
                "aaData": data,
                "aoColumns": [
                { "mDataProp": "col_IdUsuario", "sTitle": "col_IdUsuario", "bSearchable": false, "bVisible": false },
                { "mDataProp": "col_Estado", "sTitle": "col_Estado", "bSearchable": false, "bVisible": false },
                { "mDataProp": null, "sTitle": "Nº", "bSearchable": false, "bVisible": true },
                { "mDataProp": "col_NombreCompleto", "sTitle": "Nombre", "bSearchable": true, "bVisible": true },
                { "mDataProp": "col_Denominacion", "sTitle": "Denominación", "bSearchable": true, "bVisible": true },
                { "mDataProp": "col_srtEtado", "sTitle": "Estado", "bSearchable": true, "bVisible": true },
                { "mDataProp": null, "sTitle": "Opciones", "bSearchable": false, "sClass": "text-center", "bVisible": true }
                ],
                "aoColumnDefs": [
                {
                    "mRender": function (data, type, row) {
                        var btnEditar = "", btnEliminar = "";

                        btnEditar = "<button id='btn_" + row['col_IdUsuario'] + "_edit' type='button' class='btn btn-primary btnEditar' idUsuario=" + row['col_IdUsuario'] + " estado=" + row['col_Estado'] + " title='Editar'><i class='fa fa-edit'></i></button>";
                        btnEliminar = "<button id='btn_" + row['col_IdUsuario'] + "_elm' type='button' class='btn btn-danger btnEliminar' idUsuario=" + row['col_IdUsuario'] + " Estado=" + row['col_Estado'] + " title='Eliminar'><i class='fa fa-times'></i></button>";

                        return btnEditar + ' ' + btnEliminar;
                    },
                    "aTargets": [6]
                }
                ]
            });

            $('#tbUsuario tbody').on('click', 'button', function () {
                $("#hfIdUsuario").val($(this).attr("idUsuario"));
                if ($(this).hasClass('btnEditar')) {
                    OpenWindowClick($(this), urlRegistroUsuario, "idUsuario", false, true, 'n', "Editar Usuario");
                }

                if ($(this).hasClass('btnEliminar')) {
                    OpenConfirmation(null, null, '¿Est&aacute; seguro de eliminar el registro seleccionado?', 'Aceptar', 'Espere mientras termina la operaci&oacute;n.', 'fEliminarUsuario');
                }
            });
        }

        function fEliminarUsuario(dialogConfirm) {
            var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            var IdUsuario = $("#hfIdUsuario").val() != "" ? $.trim($("#hfIdUsuario").val()) : 0;

            var param = "{ 'IdUsuario': " + IdUsuario + "}";
            $.ajax({
                type: "POST",
                url: wsMantenimientoUsuario + "/EliminarUsuario",
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
                        $("#hfIdUsuario").val("");
                        fListarUsuarios();
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
