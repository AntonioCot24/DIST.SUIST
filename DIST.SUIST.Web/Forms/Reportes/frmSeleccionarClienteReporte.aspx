<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSeleccionarClienteReporte.aspx.cs" Inherits="DIST.SUIST.Web.frmSeleccionarClienteReporte" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="frmSeleccionarClienteReporte" class="form-horizontal formu form-label-left" runat="server">
        <div class="well">
            <div class="text-center">
                <h3 class="page-header">Seleccionar Cliente</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <div class="box">
                    <div id="bhClientes" class="box-header" icon="fa fa-users" titulo="Clientes"></div>
                    <div id="bbClientes" class="box-content">
                        <div class="dFiltros">
                            <div class="form-group">
                                <div class="col-sm-12 text-right">
                                    <ul class="ds-btn">
                                        <asp:Literal ID="ltClientes" runat="server"></asp:Literal>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var frmReporteClienteHoras = "<%= frmReporteClienteHoras %>";

        $(function () {
            fValidarFormulario();
            fAgregarEventosBotones();
        });

        function fValidarFormulario() {

        }

        function fAgregarEventosBotones() {
            OpenWindow('.btnSelectClient', frmReporteClienteHoras, "idCliente", false, false, '', "");
        }
    </script>
</body>
</html>
