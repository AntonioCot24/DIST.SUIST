<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAdministrarProyectos.aspx.cs" Inherits="DIST.SUIST.Web.frmAdministrarProyectos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="frmAdministrarProyectos" class="form-horizontal formu form-label-left" runat="server">
        <input id="hfIdProyecto" type="hidden" />
        <div class="well">
            <div class="text-center">
                <h3 class="page-header">Administrar Proyectos</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <div class="box">
                    <div id="bhOpciones" class="box-header" icon="fa fa-cog" titulo="Opciones"></div>
                    <div id="bbOpciones" class="box-content">
                        <div class="form-group" style="margin-top: 0; margin-bottom: 0">
                            <div class="col-sm-12 text-right">
                                <button id="btnNuevoProyecto" type="button" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;Nuevo</button>
                                <button id="btnExportarProyectos" type="button" class="btn btn-success" nomarchivoexcel="Listado de Proyectos"><i class="fa fa-file-excel-o"></i>&nbsp;Exportar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12">
                <div class="box">
                    <div id="bhListaMarca" class="box-header" icon="fa fa-table" titulo="Listado de Proyectos"></div>
                    <div class="box-content">
                        <div class="form-group">
                            <div id="dtProyecto" class="col-xs-12 col-sm-12"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var wsMantenimientoProyecto = "<%= wsMantenimientoProyecto %>";
        var urlRegistroProyecto = "<%= urlRegistroProyecto %>";
        var urlMainMantenimientos = "<%= urlMainMantenimientos %>";

        var ClienteBE = function () {
            this.IdCliente = 0;
        }

        var ProyectoBE = function () {
            this.IdProyecto = 0;
            this.NombreProyecto = "";
            this.Precio = 0;

            this.Cliente = new ClienteBE();
        }

        $(function () {
            fListarProyectos();
            fValidarFormulario();
            fAgregarEventosBotones();
        });

        function fValidarFormulario() {

        }

        function fAgregarEventosBotones() {
            OpenWindow('#btnNuevoProyecto', urlRegistroProyecto, "", false, true, 'n', "Registrar Proyecto");
           
            $("#btnExportarProyectos").on("click", function (e) {
                var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
                var eserror = true;

                $.ajax({
                    type: "POST",
                    url: wsMantenimientoProyecto + "/ExportarProyecto",
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
                            OpenWindowTabClick($("#btnExportarProyectos"), urlDescargarExcel, "nomArchivoExcel");
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

        function fListarProyectos(filtrar) {
            var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            $.ajax({
                type: "POST",
                url: wsMantenimientoProyecto + "/ListarProyecto",
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
                        fConsultarProyectos(vResult.Listado);
                    }
                    else {
                        fConsultarProyectos(vResult.Listado);
                    }
                    return true;
                },
                error: function (request, status, error) {
                    showMensaje(mensajeResult, eserror);
                    return true;
                }
            });
        }

        function fConsultarProyectos(result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            var data = jQuery.parseJSON(result);

            $('#dtProyecto').html('<table cellpadding="0" cellspacing="0" border="0" id="tbProyecto" class="table table-striped table-bordered"></table>');
            $('#tbProyecto').dataTable({
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
                    ActivarBackspace("tbProyecto_filter");
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                    $("td:first", nRow).html(iDisplayIndex + 1);
                    return nRow;
                },
                "oLanguage": DatatableSp,
                "aaData": data,
                "aoColumns": [
                { "mDataProp": "col_IdProyecto", "sTitle": "col_IdProyecto", "bSearchable": false, "bVisible": false },
                { "mDataProp": null, "sTitle": "Nº", "bSearchable": false, "bVisible": true },
                { "mDataProp": "col_Cliente", "sTitle": "Cliente", "bSearchable": true, "bVisible": true },
                { "mDataProp": "col_NombreProyecto", "sTitle": "Descrición", "bSearchable": true, "bVisible": true },
                { "mDataProp": "col_Precio", "sTitle": "Precio", "bSearchable": true, "bVisible": true },
                { "mDataProp": null, "sTitle": "Opciones", "bSearchable": false, "sClass": "text-center", "bVisible": true }
                ],
                "aoColumnDefs": [
                {
                    "mRender": function (data, type, row) {
                        var btnEditar = "", btnEliminar = "";

                        btnEditar = "<button id='btn_" + row['col_IdProyecto'] + "_edit' type='button' class='btn btn-primary btn-sm btnEditar' idProyecto=" + row['col_IdProyecto'] + " title='Editar'><i class='fa fa-edit'></i></button>";

                        btnEliminar = "<button id='btn_" + row['col_IdProyecto'] + "_elm' type='button' class='btn btn-danger btn-sm btnEliminar' idProyecto=" + row['col_IdProyecto'] + " title='Eliminar'><i class='fa fa-times'></i></button>";

                        return btnEditar + ' ' + btnEliminar;
                    },
                    "aTargets": [5]
                }
                ]
            });

            $('#tbProyecto tbody').on('click', 'button', function () {
                $("#hfIdProyecto").val($(this).attr("idProyecto"));
                if ($(this).hasClass('btnEditar')) {
                    OpenWindowClick($(this), urlRegistroProyecto, "idProyecto", false, true, 'n', "Editar Proyecto");
                }

                if ($(this).hasClass('btnEliminar')) {
                    OpenConfirmation(null, null, '¿Est&aacute; seguro de eliminar el registro seleccionado?', 'Aceptar', 'Espere mientras termina la operaci&oacute;n.', 'fEliminarProyecto');
                }
            });
        }

        function fEliminarProyecto(dialogConfirm) {
            var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            var IdProyecto = $("#hfIdProyecto").val() != "" ? $.trim($("#hfIdProyecto").val()) : 0;

            var param = "{ 'IdProyecto': " + IdProyecto + "}";
            $.ajax({
                type: "POST",
                url: wsMantenimientoProyecto + "/EliminarProyecto",
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
                        $("#hfIdProyecto").val("");
                        fListarProyectos();
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
