<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmReporteClienteHoras.aspx.cs" Inherits="DIST.SUIST.Web.frmReporteClienteHoras" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="frmReporteClienteHoras" class="form-horizontal formu form-label-left" runat="server">
        <input id="hfIdCliente" type="hidden" runat="server" />
        <input id="hfFacturar" type="hidden" runat="server" />
        <input id="hfTiposCambio" type="hidden" runat="server" />
        <div class="well">
            <div class="text-center">
                <h3 class="page-header">Reporte Cliente</h3>
            </div>
        </div>
        <div class="row">
            <div class="box">
                <div id="bhOpciones" class="box-header" icon="fa fa-cog" titulo="Opciones"></div>
                <div id="bbOpciones" class="box-content">
                    <div class="form-group">
                        <div class="col-sm-12 text-right">
                            <label class="control-label col-sm-2">Inicio Mes: <span class="required">*</span></label>
                            <div class="col-sm-4">
                                <div class="input-group">
                                    <input id="txtInicioMes" type="text" class="form-control date-pick validate[required]" runat="server" />
                                    <span id="btnInicioMes" class="input-group-addon" style="cursor: pointer;">
                                        <span class="fa fa-calendar"></span>
                                    </span>
                                </div>
                            </div>
                            <label class="control-label col-sm-2">Fin Mes: <span class="required">*</span></label>
                            <div class="col-sm-4">
                                <div class="input-group">
                                    <input id="txtFinMes" type="text" class="form-control date-pick validate[required]" runat="server" />
                                    <span id="btnFinMes" class="input-group-addon" style="cursor: pointer;">
                                        <span class="fa fa-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Literal ID="ltTipoCambio" runat="server"></asp:Literal>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <label class="control-label col-sm-2">Proyecto:</label>
                            <div class="col-sm-4">
                                <select id="slProyecto" class="form-control chosen" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group text-right" style="">
                        <div class="col-sm-12 text-right">
                            <button id="btnImprimir" type="button" class="btn btn-info" disabled="disabled"><i class="fa fa-print"></i>&nbsp;Imprimir</button>
                            <button id="btnExportar" type="button" class="btn btn-success" disabled="disabled" nomarchivoexcel="Reporte de Horas"><i class="fa fa-file-excel-o"></i>&nbsp;Exportar</button>
                            <button id="btnMostrar" type="button" class="btn btn-primary"><i class="fa fa-eye"></i>&nbsp;Mostrar</button>
                            <button id="btnRegresar" type="button" class="btn btn-warning"><i class="fa fa-home"></i>&nbsp;Regresar</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box">
                <div id="bhReporteCliente" class="box-header" icon="fa fa-line-chart" titulo="Reporte Horas"></div>
                <div class="box-content">
                    <div class="form-group">
                        <div id="divPrint" class="col-sm-12">
                            <div class="form-group">
                                <div class="text-center">
                                    <img id="imgEmpresaLogo" style="width: 300px; height: 70px;" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="text-center">
                                    <h3 id="hTitle">RELACION HORARIA</h3>
                                    <h4>
                                        <asp:Label ID="lblCliente" runat="server"></asp:Label></h4>
                                    <h4>
                                        <label id="lblMesAnio"></label>
                                    </h4>
                                </div>
                            </div>
                            <div class="form-group">
                                <table id="tTiempos" class="table table-bordered">
                                    <thead style="background-color: #333; color: #f7f7f7;">
                                        <tr>
                                            <th style="width: 100px; text-align: center;">Fecha</th>
                                            <th style="width: 200px; text-align: center;">Cliente</th>
                                            <th style="width: 300px; text-align: center;">Tema</th>
                                            <th style="width: 100px; text-align: center;">Tiempo</th>
                                            <th style="width: 100px; text-align: center;">Tiempo Acumulado</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbTiempos">
                                    </tbody>
                                </table>
                            </div>
                            <div id="divTotales" class="form-group">
                                <label class="totalHorasMes">Total de horas del mes</label>
                                <label id="totalHorasMes" class="no-text-right totalHorasMes"></label>
                                <br />
                                <label class="totalMontoContrato">Monto contrato</label>
                                <label id="totalMontoContrato" class="no-text-right totalMontoContrato"></label>
                                <br class="totalMontoContrato" />
                                <label class="totalMontoProyecto">Monto proyecto</label>
                                <label id="totalMontoProyecto" class="no-text-right totalMontoProyecto"></label>
                                <br class="totalMontoProyecto" />
                                <label class="totalHorasRetainer">Horas del retainer</label>
                                <label id="totalHorasRetainer" class="no-text-right totalHorasRetainer"></label>
                                <br class="totalHorasRetainer" />
                                <label class="totalHorasExceso">Horas en exceso</label>
                                <label id="totalHorasExceso" class="no-text-right totalHorasExceso"></label>
                                <br class="totalHorasExceso" />
                                <label class="totalMontoRetainer">Monto de retainer</label>
                                <label id="totalMontoRetainer" class="no-text-right totalMontoRetainer"></label>
                                <br class="totalMontoRetainer" />
                                <label class="totalMontoHoraExceso">Monto por hora de exceso</label>
                                <label id="totalMontoHoraExceso" class="no-text-right totalMontoHoraExceso"></label>
                                <br class="totalMontoHoraExceso" />
                                <label class="totalMontoHorasExceso">Monto total por horas de exceso</label>
                                <label id="totalMontoHorasExceso" class="no-text-right totalMontoHorasExceso"></label>
                                <br class="totalMontoHorasExceso" />
                                <label class="totalMontoFlat">Monto Flat</label>
                                <label id="totalMontoFlat" class="no-text-right totalMontoFlat"></label>
                                <br class="totalMontoFlat" />
                                <label class="totalPagar">Total a Pagar</label>
                                <label id="totalPagar" class="no-text-right totalPagar"></label>
                            </div>
                            <div class="form-group">
                                <label id="lblAbogados" style="font-weight: normal"></label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var wsReporte = "<%= wsReporte %>";
        var frmSeleccionarClienteReporte = "<%= frmSeleccionarClienteReporte %>";
        var TotalHorasMes = "";
        var FechaGeneral = "";
        var msecPerMinute = 1000 * 60;
        var msecPerHour = msecPerMinute * 60;
        var msecPerDay = msecPerHour * 24;

        var ClienteBE = function () {
            this.IdCliente = 0;
        }

        var ActividadBE = function () {
            this.IdActividad = 0;
            this.FechaInicio = null;
            this.FechaFin = null;
            this.IdProyecto = 0;

            this.Cliente = new ClienteBE();
        }


        var ListaActividadesBE = function () {
            this.Fecha = "";
            this.Cliente = "";
            this.Tema = "";
            this.Tiempo = "";
            this.TiempoAcumulado = "";
        }

        var ActividadExportarBE = function () {
            this.Titulo = "";
            this.Cliente = "";
            this.Fecha = "";
            this.totalHorasMes = "";
            this.totalMontoContrato = "";
            this.totalMontoProyecto = "";
            this.totalHorasRetainer = "";
            this.totalHorasExceso = "";
            this.totalMontoRetainer = "";
            this.totalMontoHoraExceso = "";
            this.totalMontoFlat = "";
            this.totalPagar = "";

            this.ListaActividadesBE = new Array();
        }

        $(function () {
            fValidarFormulario();
            fAgregarEventosBotones();
        });

        function fValidarFormulario() {
            $("#tTiempos").hide();
            $("#divTotales").hide();
        }

        function fAgregarEventosBotones() {
            OpenWindow('#btnRegresar', frmSeleccionarClienteReporte, "", false, false, 'a', "");

            $("#btnImprimir").on("click", function () {
                $("#divPrint").print();
            });

            $("#btnExportar").on("click", function () {
                var objActividadExportar = new ActividadExportarBE();
                var lstListaActividadesBE = new Array();

                var pos = $("#tbTiempos tr").length;
                for (var i = 1; i <= pos; i++) {
                    var objListaActividadesBE = new ListaActividadesBE();

                    objListaActividadesBE.Fecha = $("#colFecha" + i).text();
                    objListaActividadesBE.Cliente = $("#colCliente" + i).text();
                    objListaActividadesBE.Tema = $("#colActividad" + i).text();
                    objListaActividadesBE.Tiempo = $("#colTiempoRes" + i).text();
                    objListaActividadesBE.TiempoAcumulado = $("#colTiempoAcum" + i).text();

                    lstListaActividadesBE.push(objListaActividadesBE);
                }

                objActividadExportar.Titulo = $("#hTitle").text();
                objActividadExportar.Cliente = $("#lblCliente").text();
                objActividadExportar.Fecha = $("#lblMesAnio").text();

                objActividadExportar.totalHorasMes = $(".totalHorasMes").text();
                objActividadExportar.totalMontoContrato = $(".totalMontoContrato").text();
                objActividadExportar.totalMontoProyecto = $(".totalMontoProyecto").text();
                objActividadExportar.totalHorasRetainer = $(".totalHorasRetainer").text();
                objActividadExportar.totalHorasExceso = $(".totalHorasExceso").text();
                objActividadExportar.totalMontoRetainer = $(".totalMontoRetainer").text();
                objActividadExportar.totalMontoHoraExceso = $(".totalMontoHoraExceso").text();
                objActividadExportar.totalMontoFlat = $(".totalMontoFlat").text();
                objActividadExportar.totalPagar = $(".totalPagar").text();
                objActividadExportar.ListaActividadesBE = lstListaActividadesBE;

                var param = "{ 'objActividadExportar': " + JSON.stringify(objActividadExportar) + "}";
                $.ajax({
                    type: "POST",
                    url: wsReporte + "/ExportarReporteCliente",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: param,
                    success: function (resp) {
                        var vResult = (typeof resp.d) == 'string' ? eval('(' + resp.d + ')') : resp.d;
                        var eserror = true;
                        var mensajeResult = "";
                        if (vResult != null) {
                            if (vResult.Resultado == 'OK')
                                eserror = false;
                            mensajeResult = htmlDecode(vResult.Mensaje);
                        }
                        if (!eserror) {
                            OpenWindowTabClick($("#btnExportar"), urlDescargarExcel, "nomArchivoExcel");
                        }
                        return true;
                    },
                    error: function (request, status, error) {
                        return true;
                    }
                });
            });

            $("#btnMostrar").on("click", function () {
                var isValidate = true;
                isValidate = $("#frmReporteClienteHoras").validationEngine('validate');
                if (!isValidate) return false;

                $("#lblMesAnio").text("");
                fValidarFormulario();

                var objClienteBE = new ClienteBE();
                objClienteBE.IdCliente = $("#hfIdCliente").val() != "" ? $.trim($("#hfIdCliente").val()) : 0;

                var objActividadBE = new ActividadBE();
                objActividadBE.FechaInicio = $("#txtInicioMes").val() != "" ? formatDate($("#txtInicioMes").val()) : null;
                objActividadBE.FechaFin = $("#txtFinMes").val() != "" ? formatDate($("#txtFinMes").val()) : null;
                objActividadBE.IdProyecto = $("#slProyecto").val() != "" ? $.trim($("#slProyecto").val()) : 0;
                objActividadBE.Cliente = objClienteBE;

                var param = "{ 'oActividad': " + JSON.stringify(objActividadBE) + "}";
                $.ajax({
                    type: "POST",
                    url: wsReporte + "/ListarActividades",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: param,
                    success: function (resp) {
                        $("#tTiempos").show();
                        $("#divTotales").show();
                        $("#tbTiempos").empty();
                        $("#btnImprimir").prop('disabled', false);
                        $("#btnExportar").prop('disabled', false);

                        var monthNames = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
                        var tiempoacumulado = "";
                        var numColumna = 1;
                        var arrayAbogados = new Array();

                        console.log(resp.d);

                        if (resp.d == null) {
                            showMensaje("No existen datos para mostrar.", true);
                            return;
                        }

                        $.each(resp.d, function (i, d) {
                            var fechainicio = new Date(d.strFechaInicio), fechafin = new Date(d.strFechaFin);
                            var tiemporesultado = fCalcularHoras(fechainicio, fechafin);
                            var fecha = d.strFechaInicio.split(" ");

                            if (FechaGeneral == "") {
                                FechaGeneral = d.strFecha;
                            }

                            if ($("#lblMesAnio").text() == "") {
                                $("#lblMesAnio").text(monthNames[fechainicio.getMonth()] + " de " + fechainicio.getFullYear());
                            }

                            if (tiempoacumulado == "") {
                                tiempoacumulado = tiemporesultado;
                            }
                            else {
                                tiempoacumulado = fCalcularHorasAcumuladas(tiemporesultado, tiempoacumulado);
                            }

                            $("#tbTiempos").append("<tr>" +
                                   "<td id='colFecha" + numColumna + "' style='text-align:center;'>" + d.strFechaTitulo + "</td>" +
                                   "<td id='colCliente" + numColumna + "' style='text-align:center;'>" + d.Cliente.NombreCompleto + "</td>" +
                                   "<td id='colActividad" + numColumna + "' style='text-align:center;'>" + d.TipoActividad.Nombre + "<br />(" + d.strUsuario + ")</td>" +
                                   "<td id='colTiempoRes" + numColumna + "' style='text-align:center;'>" + tiemporesultado + "</td>" +
                                   "<td id='colTiempoAcum" + numColumna + "' style='text-align:center;'>" + tiempoacumulado + "</td>" +
                               "</tr>");
                            numColumna++;

                            if (arrayAbogados.indexOf(d.strNombreCompleto) == -1)
                                arrayAbogados.push(d.strNombreCompleto);
                        });

                        var abogados = "";

                        $.each(arrayAbogados, function (i, d) {
                            abogados += d + ", ";
                        });

                        $("#lblAbogados").text(abogados.substr(0, abogados.length - 2));

                        TotalHorasMes = tiempoacumulado;

                        $("#totalHorasMes").text(": " + tiempoacumulado + " horas.");
                        fObtenerFacturacionCliente();
                        return true;
                    },
                    error: function (request, status, error) {
                        return true;
                    }
                });
            });
        }

        function fObtenerFacturacionCliente() {
            var param = "{ 'idCliente': " + $("#hfIdCliente").val() + "}";
            $.ajax({
                type: "POST",
                url: wsReporte + "/ObtenerFacturacionCliente",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: param,
                success: function (resp) {
                    var objFacturacion = resp.d;

                    var MontoFlat = 0, TotalPagar = 0, MontoRetainer = 0, MontoTotalHoraExeco = 0;

                    var shoras = TotalHorasMes.split(':');
                    var horas = parseInt(shoras[0]);
                    var minutos = parseInt(shoras[1]);
                    var tarifa = 0;

                    var arryTipoCambio = $("#hfTiposCambio").val().split(",");

                    $(".totalMontoContrato").hide();
                    $(".totalMontoProyecto").hide();
                    $(".totalHorasRetainer").hide();
                    $(".totalHorasExceso").hide();
                    $(".totalMontoRetainer").hide();
                    $(".totalMontoHoraExceso").hide();
                    $(".totalMontoHorasExceso").hide();
                    $(".totalMontoFlat").hide();

                    if ($("#slProyecto").val() != "") {
                        tarifa = objFacturacion.PrecioProyecto;
                        if (arryTipoCambio.length > 0) {
                            $("#totalMontoProyecto").text(fCalcularTipoCambio(arryTipoCambio, tarifa) + " más IGV.");
                        } else {
                            $("#totalMontoProyecto").text(": US $. " + tarifa + " más IGV.");
                        }
                        $(".totalMontoProyecto").show();
                        TotalPagar = tarifa;
                    }
                    else {
                        if (objFacturacion.TipoFacturacion == 1) {
                            tarifa = objFacturacion.TarifaFija;
                            if (arryTipoCambio.length > 0) {
                                $("#totalMontoContrato").text(fCalcularTipoCambio(arryTipoCambio, tarifa) + " más IGV.");
                            } else {
                                $("#totalMontoContrato").text(": US $. " + tarifa + " más IGV.");
                            }
                            $(".totalMontoContrato").show();
                        }
                        if (objFacturacion.TipoFacturacion == 2) {
                            var montohora = 0, montominuto = 0;
                            montohora = objFacturacion.TarifaHoras;
                            montominuto = Math.round((montohora / 60) * 100) / 100;
                            if (arryTipoCambio.length > 0) {
                                $("#totalMontoContrato").text(fCalcularTipoCambio(arryTipoCambio, montohora) + " por hora.");
                            } else {
                                $("#totalMontoContrato").text(": US $. " + montohora + " por hora.");
                            }
                            $(".totalMontoContrato").show();
                            tarifa = (montohora * horas) + (montominuto * minutos);
                        }
                        if (objFacturacion.TipoFacturacion == 3) {
                            var HorasRetainer = objFacturacion.TarifaHoras.toString();
                            var strHorasRet = HorasRetainer.split(":");

                            if (strHorasRet.length == 1) {
                                HorasRetainer = HorasRetainer + ":00";
                            }

                            var tRetainer = HorasRetainer.split(":");
                            var tAcumulado = TotalHorasMes.split(":");
                            var hExceso = parseInt(tAcumulado[0]) - parseInt(tRetainer[0]);
                            var mExceso = parseInt(tAcumulado[1]) - parseInt(tRetainer[1]);

                            if (mExceso >= 60) {
                                mExceso = mExceso - 60;
                                hExceso = hExceso + 1;
                            }

                            if (hExceso <= 0) {
                                hExceso = 0;
                                mExceso = 0;
                            }

                            var HorasExceso = padL(hExceso, 2) + ":" + padL(mExceso, 2);
                            var MontoHoraExceso = objFacturacion.TarifaHorasAdicionales;
                            var montohora = 0, montominuto = 0;
                            var strHorasExceso = HorasExceso.split(":");
                            var horas = parseInt(strHorasExceso[0]), minutos = parseInt(strHorasExceso[1]);
                            montohora = MontoHoraExceso;
                            montominuto = Math.round((montohora / 60) * 100) / 100;

                            MontoRetainer = objFacturacion.TarifaFija;
                            MontoTotalHoraExeco = (montohora * horas) + (montominuto * minutos);

                            if (arryTipoCambio.length > 0) {
                                $("#totalMontoRetainer").text(fCalcularTipoCambio(arryTipoCambio, MontoRetainer) + " más IGV.");
                                $("#totalMontoHoraExceso").text(fCalcularTipoCambio(arryTipoCambio, MontoHoraExceso) + " más IGV.");
                                $("#totalMontoHorasExceso").text(fCalcularTipoCambio(arryTipoCambio, MontoTotalHoraExeco) + " más IGV.");
                                $("#totalMontoContrato").text(+ " por hora.");
                            } else {

                                $("#totalMontoRetainer").text(": US $. " + MontoRetainer + " más IGV.");
                                $("#totalMontoHoraExceso").text(": US $. " + MontoHoraExceso + " más IGV.");
                                $("#totalMontoHorasExceso").text(": US $. " + MontoTotalHoraExeco + " más IGV.");
                            }

                            $("#totalHorasRetainer").text(": " + padL(HorasRetainer, 2) + ":00 horas.");
                            $("#totalHorasExceso").text(": " + HorasExceso + " horas.");
                            $(".totalHorasRetainer").show();
                            $(".totalHorasExceso").show();
                            $(".totalMontoRetainer").show();
                            $(".totalMontoHoraExceso").show();
                            $(".totalMontoHorasExceso").show();
                        }

                        if (objFacturacion.MontoFlat > 0) {
                            MontoFlat = objFacturacion.MontoFlat;
                            if (arryTipoCambio.length > 0) {
                                $("#totalMontoFlat").text(fCalcularTipoCambio(arryTipoCambio, MontoFlat) + " más IGV.");
                            } else {
                                $("#totalMontoFlat").text(": US $. " + MontoFlat + " más IGV.");
                            }
                            $(".totalMontoFlat").show();
                        }

                        TotalPagar = tarifa + MontoFlat + MontoRetainer + MontoTotalHoraExeco;
                    }
                    if (arryTipoCambio.length > 0) {
                        $("#totalPagar").text(fCalcularTipoCambio(arryTipoCambio, TotalPagar) + " más IGV.");
                    } else {

                        $("#totalPagar").text(": US $. " + TotalPagar + " más IGV.");
                    }
                    return true;
                },
                error: function (request, status, error) {
                    return true;
                }
            });
        }

        function fCalcularHoras(t1, t2) {
            var tiempo1 = t1.getTime(), tiempo2 = t2.getTime();
            var tiempoR = tiempo2 - tiempo1;

            var horas = Math.floor(tiempoR / msecPerHour);
            tiempoR = tiempoR - (horas * msecPerHour);
            var minutos = Math.floor(tiempoR / msecPerMinute);

            if (horas < 0) {
                horas = 0;
                minutos = 0;
            }

            return padL(horas, 2) + ":" + padL(minutos, 2);
        }

        function fCalcularHorasAcumuladas(tr, ta) {
            var tiempo1 = tr.split(":"), tiempo2 = ta.split(":");
            var horasRes = parseInt(tiempo1[0]), minutosRes = parseInt(tiempo1[1]), horasAcum = parseInt(tiempo2[0]), minutosAcum = parseInt(tiempo2[1]);

            var minutosTotal = minutosRes + minutosAcum;
            var horasTotal = 0;

            if (minutosTotal >= 60) {
                horasTotal = horasRes + horasAcum + 1;
                minutosTotal = minutosTotal - 60;
            }
            else
                horasTotal = horasRes + horasAcum;

            return padL(horasTotal, 2) + ":" + padL(minutosTotal, 2);
        }

        function fCalcularTipoCambio(arryTipoCambio, monto) {
            var texto = ": ";
            var tipocambio = 0;
            $.each(arryTipoCambio, function (i, d) {
                var moneda = d.split("|");
                var valpred = moneda[0].split("-");

                if (tipocambio == 0) {
                    var valortipocambio = $("#" + valpred[0]).val();
                    tipocambio = parseFloat(valortipocambio);
                    texto += moneda[1] + " " + monto.toFixed(2) + " ó ";
                }
                else {
                    var montotipocambio = monto * tipocambio;
                    texto += moneda[1] + " " + montotipocambio.toFixed(2) + " ó ";
                }

            });
            return texto.substr(0, texto.length - 3);
        }
    </script>
</body>
</html>
