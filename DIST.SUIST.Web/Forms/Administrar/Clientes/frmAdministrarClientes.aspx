<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAdministrarClientes.aspx.cs" Inherits="DIST.SUIST.Web.frmAdministrarClientes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="frmAdministrarClientes" class="form-horizontal formu" runat="server">
        <input id="hfIdCliente" type="hidden" />
        <div class="well">
            <div class="text-center">
                <h3 class="page-header">Administrar Clientes</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <div class="box">
                    <div id="bhOpciones" class="box-header" icon="fa fa-cog" titulo="Opciones"></div>
                    <div id="bbOpciones" class="box-content">
                        <div class="form-group" style="margin-top: 0; margin-bottom: 0">
                            <div class="col-sm-12 text-right">
                                <button id="btnNuevoCliente" type="button" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;Nuevo</button>
                                <button id="btnExportarClientes" type="button" class="btn btn-success" nomarchivoexcel="Listado de Clientes"><i class="fa fa-file-excel-o"></i>&nbsp;Exportar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12">
                <div class="box">
                    <div id="bhListaMarca" class="box-header" icon="fa fa-table" titulo="Listado de Clientes"></div>
                    <div class="box-content">
                        <div class="form-group">
                            <div id="dtCliente" class="col-xs-12 col-sm-12"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var wsMantenimientoCliente = "<%= wsMantenimientoCliente %>";
        var urlRegistroCliente = "<%= urlRegistroCliente %>";
        var urlListarContactos = "<%= urlListarContactos %>";
        var urlRegistroFactura = "<%= urlRegistroFactura %>";

        var TipoClienteBE = function () {
            this.IdTipoCliente = 0;
            this.Descripcion = "";
        }

        var ClienteBE = function () {
            this.IdCliente = 0;
            this.DocumentoIdentidad = "";
            this.NombreCompleto = "";
            this.Email = "";
            this.Telefono = "";
            this.SitioWeb = "";
            this.Direccion = "";
            this.FechaInicioContrato = null;
            this.FechaFinContrato = null;
            this.Color = "";

            this.TipoCliente = new TipoClienteBE();
        }

        $(function () {
            fListarClientes();
            fValidarFormulario();
            fAgregarEventosBotones();
        });

        function fValidarFormulario() {

        }

        function fAgregarEventosBotones() {
            OpenWindow('#btnNuevoCliente', urlRegistroCliente, "", false, true, 'n', "Registrar Cliente");
            
            $("#btnExportarClientes").on("click", function (e) {
                var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
                var eserror = true;

                $.ajax({
                    type: "POST",
                    url: wsMantenimientoCliente + "/ExportarCliente",
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
                            OpenWindowTabClick($("#btnExportarClientes"), urlDescargarExcel, "nomArchivoExcel");
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

        function fListarClientes(filtrar) {
            var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            $.ajax({
                type: "POST",
                url: wsMantenimientoCliente + "/ListarCliente",
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
                    $("#hfIdCliente").val("");
                    if (!eserror) {
                        fConsultarClientes(vResult.Listado);
                    }
                    else {
                        fConsultarClientes(vResult.Listado);
                    }
                    return true;
                },
                error: function (request, status, error) {
                    showMensaje(mensajeResult, eserror);
                    return true;
                }
            });
        }

        function fConsultarClientes(result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            var data = jQuery.parseJSON(result);

            $('#dtCliente').html('<table cellpadding="0" cellspacing="0" border="0" id="tbCliente" class="table table-striped table-bordered"></table>');
            $('#tbCliente').dataTable({
                "sProcessing": true,
                "bPaginate": true,
                "sPaginationType": "full_numbers",
                "bJQueryUI": false,
                "bLengthChange": true,
                "ordering": true,
                "searching": true,
                //"sScrollX": "100%",
                //"sScrollXInner": "100%",
                //"sScrollY": 300,
                //"bScrollCollapse": true,
                "bAutoWidth": false,
                "fnDrawCallback": function (oSettings) {
                    ActivarBackspace("tbCliente_filter");
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                    $("td:first", nRow).html(iDisplayIndex + 1);
                    return nRow;
                },
                "oLanguage": DatatableSp,
                "aaData": data,
                "aoColumns": [
                { "mDataProp": "col_IdCliente", "sTitle": "col_IdCliente", "bSearchable": false, "bVisible": false },
                { "mDataProp": null, "sTitle": "Nº", "bSearchable": false, "bVisible": true },
                { "mDataProp": "col_Descripcion", "sTitle": "Tipo", "bSearchable": true, "bVisible": true },
                { "mDataProp": "col_DocumentoIdentidad", "sTitle": "Doc. Identidad", "bSearchable": true, "bVisible": true },
                { "mDataProp": "col_NombreCompleto", "sTitle": "Nombre", "bSearchable": true, "bVisible": true },
                { "mDataProp": "col_FechaInicioContrato", "sTitle": "Inicio Contrato", "bSearchable": true, "bVisible": true },
                { "mDataProp": "col_FechaFinContrato", "sTitle": "Fin Contrato", "bSearchable": true, "bVisible": true },
                { "mDataProp": null, "sTitle": "Contactos", "bSearchable": true, "sClass": "text-center", "bVisible": true },
                { "mDataProp": null, "sTitle": "Facturación", "bSearchable": true, "sClass": "text-center", "bVisible": true },
                { "mDataProp": null, "sTitle": "Opciones", "bSearchable": false, "sClass": "text-center", "bVisible": true }
                ],
                "aoColumnDefs": [
                    {
                        "mRender": function (data, type, row) {
                            return "<button id='btn_" + row['col_IdCliente'] + "_cont' type='button' class='btn btn-info btn-sm btnContactos' idCliente=" + row['col_IdCliente'] + " title='Contactos'><i class='fa fa-users'></i></button>";
                        },
                        "aTargets": [7]
                    },
                    {
                        "mRender": function (data, type, row) {
                            return "<button id='btn_" + row['col_IdCliente'] + "_fact ' type='button' class='btn btn-success btn-sm btnFacturacion' idCliente=" + row['col_IdCliente'] + " title='Facturación'><i class='fa fa-money'></i></button>";
                        },
                        "aTargets": [8]
                    },
                {
                    "mRender": function (data, type, row) {
                        var btnEditar = "", btnEliminar = "";

                        btnEditar = "<button id='btn_" + row['col_IdCliente'] + "_edit' type='button' class='btn btn-primary btn-sm btnEditar' idCliente=" + row['col_IdCliente'] + " title='Editar'><i class='fa fa-edit'></i></button>";

                        btnEliminar = "<button id='btn_" + row['col_IdCliente'] + "_elm' type='button' class='btn btn-danger btn-sm btnEliminar' idCliente=" + row['col_IdCliente'] + " title='Eliminar'><i class='fa fa-times'></i></button>";

                        return btnEditar + ' ' + btnEliminar;
                    },
                    "aTargets": [9]
                }
                ]
            });

            $('#tbCliente tbody').on('click', 'button', function () {
                $("#hfIdCliente").val($(this).attr("idCliente"));

                if ($(this).hasClass('btnContactos')) {
                    OpenWindowClick($(this), urlListarContactos, "idCliente", false, true, 'n', "Listado de Contactos");
                }

                if ($(this).hasClass('btnFacturacion')) {
                    OpenWindowClick($(this), urlRegistroFactura, "idCliente", false, true, 'n', "Registrar Facturación");
                }

                if ($(this).hasClass('btnEditar')) {
                    OpenWindowClick($(this), urlRegistroCliente, "idCliente", false, true, 'n', "Editar Cliente");
                }

                if ($(this).hasClass('btnEliminar')) {
                    OpenConfirmation(null, null, '¿Est&aacute; seguro de eliminar el registro seleccionado?', 'Aceptar', 'Espere mientras termina la operaci&oacute;n.', 'fEliminarCliente');
                }
            });
        }

        function fEliminarCliente(dialogConfirm) {
            var mensajeResult = "Ocurri&oacute, un error al acceder al servicio web, intente nuevamente";
            var eserror = true;

            var IdCliente = $("#hfIdCliente").val() != "" ? $.trim($("#hfIdCliente").val()) : 0;

            var param = "{ 'IdCliente': " + IdCliente + "}";
            $.ajax({
                type: "POST",
                url: wsMantenimientoCliente + "/EliminarCliente",
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
                        $("#hfIdCliente").val("");
                        fListarClientes();
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
