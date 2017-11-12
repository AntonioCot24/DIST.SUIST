<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAdministrarGastos.aspx.cs" Inherits="DIST.SUIST.Web.frmAdministrarGastos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="frmAdministrarGastos" runat="server" class="form-horizontal formu form-label-left">
        <input id="hfIdGasto" type="hidden" />
        <div class="well">
            <div class="text-center">
                <h3 class="page-header">Control de Gastos</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <div class="box">
                    <div id="bhOpciones" class="box-header" icon="fa fa-cog" titulo="Opciones"></div>
                    <div id="bbOpciones" class="box-content">
                        <div class="form-group" style="margin-top: 0; margin-bottom: 0">
                            <div class="col-sm-12 text-right">
                                <button id="btnNuevoGasto" type="button" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;Nuevo</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12">
                <div class="box">
                    <div id="bhListaGastos" class="box-header" icon="fa fa-table" titulo="Listado de Gastos"></div>
                    <div class="box-content">
                        <div class="form-group">
                            <div id="dtGasto" class="col-xs-12 col-sm-12"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var wsControlGasto = "<%= wsControlGasto %>";
        var wsAdministrarProyecto = "<%= wsAdministrarProyecto %>";
        var urlRegistroGasto = "<%= urlRegistroGasto %>";
        var popUpRegistroGasto;

        var GastoBE = function () {
            this.IdGasto = 0;
            this.IdCliente = 0;
            this.IdProyecto = 0;
            this.IdUsuario = 0;
            this.Fecha = null;
            this.Glosa = "";
            this.Monto = 0;

        }

        $(function () {
            fListarGastos();
            fValidarFormulario();
            fAgregarEventosBotones();
        });

        function fValidarFormulario() {

        }

        function fAgregarEventosBotones() {
            popUpRegistroGasto = OpenWindow('#btnNuevoGasto', urlRegistroGasto, "", false, true, 'n', "Registrar Gasto");
            console.log(popUpRegistroGasto);
        }

        function fListarGastos(filtrar) {
            var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            $.ajax({
                type: "POST",
                url: wsControlGasto + "/ListarGastos",
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
                        fConsultarGastos(vResult.Listado);
                    }
                    else {
                        fConsultarGastos(vResult.Listado);
                    }
                    return true;
                },
                error: function (request, status, error) {
                    showMensaje(mensajeResult, eserror);
                    return true;
                }
            });
        }

        function fConsultarGastos(result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            var data = jQuery.parseJSON(result);

            $('#dtGasto').html('<table cellpadding="0" cellspacing="0" border="0" id="tbGasto" class="table table-striped table-bordered"></table>');
            $('#tbGasto').dataTable({
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
                    ActivarBackspace("tbGasto_filter");
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                    $("td:first", nRow).html(iDisplayIndex + 1);
                    return nRow;
                },
                "oLanguage": DatatableSp,
                "aaData": data,
                "aoColumns": [
                    { "mDataProp": "col_IdGasto", "sTitle": "col_IdGasto", "bSearchable": false, "bVisible": false },
                    { "mDataProp": null, "sTitle": "Nº", "bSearchable": false, "bVisible": true },
                    { "mDataProp": "col_NombreCliente", "sTitle": "Cliente", "bSearchable": true, "bVisible": true },
                    { "mDataProp": "col_NombreProyecto", "sTitle": "Proyecto", "bSearchable": true, "bVisible": true },
                    { "mDataProp": "col_NombreAbogado", "sTitle": "Abogado", "bSearchable": true, "bVisible": true },
                    { "mDataProp": "col_Fecha", "sTitle": "Fecha", "bSearchable": true, "bVisible": true },
                    { "mDataProp": "col_Monto", "sTitle": "Monto", "bSearchable": true, "bVisible": true },
                    { "mDataProp": null, "sTitle": "Opciones", "bSearchable": false, "sClass": "text-center", "bVisible": true }
                ],
                "aoColumnDefs": [
                    {
                        "mRender": function (data, type, row) {
                            var btnEditar = "", btnEliminar = "";

                            btnEditar = "<button id='btn_" + row['col_IdGasto'] + "_edit' type='button' class='btn btn-primary btnEditar' idGasto=" + row['col_IdGasto'] + " title='Editar'><i class='fa fa-edit'></i></button>";
                            btnEliminar = "<button id='btn_" + row['col_IdGasto'] + "_elm' type='button' class='btn btn-danger btnEliminar' idGasto=" + row['col_IdGasto'] + " title='Eliminar'><i class='fa fa-times'></i></button>";

                            return btnEditar + ' ' + btnEliminar;
                        },
                        "aTargets": [7]
                    }
                ]
            });

            $('#tbGasto tbody').on('click', 'button', function () {
                $("#hfIdGasto").val($(this).attr("idGasto"));
                if ($(this).hasClass('btnEditar')) {
                    OpenWindowClick($(this), urlRegistroGasto, "idGasto", false, true, 'n', "Editar Gasto");
                }

                if ($(this).hasClass('btnEliminar')) {
                    OpenConfirmation(null, null, '¿Est&aacute; seguro de eliminar el registro seleccionado?', 'Aceptar', 'Espere mientras termina la operaci&oacute;n.', 'fEliminarGasto');
                }
            });
        }

        function fEliminarGasto(dialogConfirm) {
            var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            var IdGasto = $("#hfIdGasto").val() != "" ? $.trim($("#hfIdGasto").val()) : 0;

            var param = "{ 'IdGasto': " + IdGasto + "}";
            $.ajax({
                type: "POST",
                url: wsControlGasto + "/EliminarGasto",
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
                        $("#hfIdGasto").val("");
                        fListarGastos();
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
