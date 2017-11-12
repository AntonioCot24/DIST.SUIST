<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAdministrarTipoActividades.aspx.cs" Inherits="DIST.SUIST.Web.frmAdministrarTipoActividades" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="frmAdministrarTipoActividades" class="form-horizontal formu" runat="server">
        <input id="hfIdTipoActividad" type="hidden" />
        <div class="well">
            <div class="text-center">
                <h3 class="page-header">Administrar Actividades</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <div class="box">
                    <div id="bhOpciones" class="box-header" icon="fa fa-cog" titulo="Opciones"></div>
                    <div id="bbOpciones" class="box-content">
                            <div class="form-group" style="margin-top:0;margin-bottom:0">
                                <div class="col-sm-12 text-right">
                                    <button id="btnNuevoTipoActividad" type="button" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;Nuevo</button>
                                    <button id="btnExportarTipoActividades" type="button" class="btn btn-success" nomarchivoexcel="Listado de Actividades"><i class="fa fa-file-excel-o"></i>&nbsp;Exportar</button>
                                </div>
                            </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12">
                <div class="box">
                    <div id="bhListaMarca" class="box-header" icon="fa fa-table" titulo="Listado de Actividades"></div>
                    <div class="box-content">
                        <div class="form-group">
                            <div id="dtTipoActividad" class="col-xs-12 col-sm-12"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>        
    </form>
    <script type="text/javascript">
        var wsMantenimientoTipoActividad = "<%= wsMantenimientoTipoActividad %>";
        var urlRegistroTipoActividad = "<%= urlRegistroTipoActividad %>";
        var urlMainMantenimientos = "<%= urlMainMantenimientos %>";

        var PrecioBE = function () {
            this.IdPrecio = 0;
            this.Monto = 0;
        }

        var TipoActividadBE = function () {
            this.IdTipoActividad = 0;
            this.Nombre = "";

            this.Precio = new PrecioBE();
        }

        $(function () {
            fListarTipoActividades();
            fValidarFormulario();
            fAgregarEventosBotones();
        });

        function fValidarFormulario() {

        }

        function fAgregarEventosBotones() {
            OpenWindow('#btnNuevoTipoActividad', urlRegistroTipoActividad, "", false, true, 'n', "Registrar Actividad");
            //OpenWindow('#btnRegresar', urlMainMantenimientos, "", false, false, 0, 0, "");

            $("#btnExportarTipoActividades").on("click", function (e) {
                var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
                var eserror = true;

                $.ajax({
                    type: "POST",
                    url: wsMantenimientoTipoActividad + "/ExportarTipoActividad",
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
                            OpenWindowTabClick($("#btnExportarTipoActividades"), urlDescargarExcel, "nomArchivoExcel");
                        }
                        return true;
                    },
                    error: function (request, status, error) {
                        showMensaje(mensajeResult, eserror);
                        return true;
                    }
                });
            });
        }

        function fListarTipoActividades(filtrar) {
            var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            $.ajax({
                type: "POST",
                url: wsMantenimientoTipoActividad + "/ListarTipoActividad",
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
                        fConsultarTipoActividades(vResult.Listado);
                    }
                    else {
                        fConsultarTipoActividades(vResult.Listado);
                    }
                    return true;
                },
                error: function (request, status, error) {
                    showMensaje(mensajeResult, eserror);
                    return true;
                }
            });
        }

        function fConsultarTipoActividades(result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            var data = jQuery.parseJSON(result);

            $('#dtTipoActividad').html('<table cellpadding="0" cellspacing="0" border="0" id="tbTipoActividad" class="table table-striped table-bordered"></table>');
            $('#tbTipoActividad').dataTable({
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
                    ActivarBackspace("tbTipoActividad_filter");
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                    $("td:first", nRow).html(iDisplayIndex + 1);
                    return nRow;
                },
                "oLanguage": DatatableSp,
                "aaData": data,
                "aoColumns": [
                { "mDataProp": "col_IdTipoActividad", "sTitle": "col_IdTipoActividad", "bSearchable": false, "bVisible": false },
                { "mDataProp": null, "sTitle": "Nº", "bSearchable": false, "bVisible": true },
                { "mDataProp": "col_Nombre", "sTitle": "Descripción", "bSearchable": true, "bVisible": true },
                { "mDataProp": null, "sTitle": "Opciones", "bSearchable": false, "sClass": "text-center", "bVisible": true }
                ],
                "aoColumnDefs": [
                {
                    "mRender": function (data, type, row) {
                        var btnEditar = "", btnEliminar = "";

                        btnEditar = "<button id='btn_" + row['col_IdTipoActividad'] + "_edit' type='button' class='btn btn-primary btn-sm btnEditar' idTipoActividad=" + row['col_IdTipoActividad'] + " title='Editar'><i class='fa fa-edit'></i></button>";

                        btnEliminar = "<button id='btn_" + row['col_IdTipoActividad'] + "_elm' type='button' class='btn btn-danger btn-sm btnEliminar' idTipoActividad=" + row['col_IdTipoActividad'] + " title='Eliminar'><i class='fa fa-times'></i></button>";

                        return btnEditar + ' ' + btnEliminar;
                    },
                    "aTargets": [3]
                }
                ]
            });

            $('#tbTipoActividad tbody').on('click', 'button', function () {
                $("#hfIdTipoActividad").val($(this).attr("idTipoActividad"));
                if ($(this).hasClass('btnEditar')) {
                    OpenWindowClick($(this), urlRegistroTipoActividad, "idTipoActividad", false, true, 'n', "Editar Actividad");
                }

                if ($(this).hasClass('btnEliminar')) {
                    OpenConfirmation(null, null, '¿Est&aacute; seguro de eliminar el registro seleccionado?', 'Aceptar', 'Espere mientras termina la operaci&oacute;n.', 'fEliminarTipoActividad');
                }
            });
        }

        function fEliminarTipoActividad(dialogConfirm) {
            var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            var IdTipoActividad = $("#hfIdTipoActividad").val() != "" ? $.trim($("#hfIdTipoActividad").val()) : 0;

            var param = "{ 'IdTipoActividad': " + IdTipoActividad + "}";
            $.ajax({
                type: "POST",
                url: wsMantenimientoTipoActividad + "/EliminarTipoActividad",
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
                        $("#hfIdTipoActividad").val("");
                        fListarTipoActividades();
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
