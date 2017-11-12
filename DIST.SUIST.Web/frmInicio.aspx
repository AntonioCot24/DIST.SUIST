<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmInicio.aspx.cs" Inherits="DIST.SUIST.Web.frmInicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="expires" content="-1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>SCT</title>

    <!-- Core CSS -->
    <link href="<%= Page.ResolveUrl("~/Content/bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/font-awesome.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/style_v2.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/layout.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/jquery-ui-1.10.3.theme.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/jquery-ui-1.10.3.custom.css") %>" rel="stylesheet" type="text/css" />

    <link href="<%= Page.ResolveUrl("~/styles/kendo.common-bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/styles/kendo.bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/styles/kendo.dataviz.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/styles/kendo.dataviz.bootstrap.min.css") %>" rel="stylesheet" type="text/css" />

    <link href="<%= Page.ResolveUrl("~/Content/validationEngine.jquery.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/validationEngine.template.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/bootstrap-dialog.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/fancybox/jquery.fancybox.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/jquery.bootstrap-touchspin.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/chosen.bootstrap.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/DataTables/css/dataTables.bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/DataTables/css/buttons.bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/DataTables/css/colReorder.bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/DataTables/css/fixedColumns.bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/DataTables/css/fixedHeader.bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/bootstrap-colorpicker/css/bootstrap-colorpicker.min.css") %>" rel="stylesheet" type="text/css" />

    <!--[if lt IE 9]>
    <link href="<%= Page.ResolveUrl("~/Content/jquery.ui.1.10.3.ie.css") %>" rel="stylesheet" type="text/css" />
    <![endif]-->

    <!-- Core JS -->
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery-3.2.1.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery-ui-1.12.1.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.center.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/bootstrap.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/devoops.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/html5.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/json3.min.js") %>" type="text/javascript"></script>

    <!-- js plugins -->
    <script src="<%= Page.ResolveUrl("~/Scripts/kendo/kendo.all.min.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/kendo/cultures/kendo.culture.es.min.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/kendo/cultures/kendo.culture.es-ES.min.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/kendo/messages/kendo.messages.es-ES.min.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/date.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/utils/jquery.ui.datepicker.js")%>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/validator/jquery.validationEngine-es.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/validator/jquery.validationEngine.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/utils/date.validation.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/bootstrap-dialog.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.bootstrap-touchspin.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/DataTables/jquery.dataTables.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/DataTables/dataTables.bootstrap.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/DataTables/dataTables.fixedColumns.min.js") %>" type="text/javascript"></script>
    <%--<script src="<%= Page.ResolveUrl("~/Scripts/progressbar/bootstrap-progressbar.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/icheck/icheck.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/nicescroll/jquery.nicescroll.min.js") %>" type="text/javascript"></script>--%>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.session.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/respond.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.cookie.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.print.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/chosen.jquery.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/chosen.proto.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/bootstrap-colorpicker.js") %>" type="text/javascript"></script>

    <script src="<%= Page.ResolveUrl("~/FormsScripts/jsTemplate.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/FormsScripts/jsModulo.js") %>"></script>
</head>
<body class="nav-md">
    <form id="frmInicio" runat="server">
        <header class="navbar">
            <div class="container-fluid expanded-panel">
                <div class="row">
                    <div id="logo" class="col-xs-12 col-sm-2">
                        <a id="btnInicio" href="#">Timer</a>
                    </div>
                    <div id="top-panel" class="col-xs-12 col-sm-10">
                        <div class="row">
                            <div class="col-xs-8 col-sm-4">
                                <a href="#" class="show-sidebar">
                                    <i class="fa fa-bars"></i>
                                </a>
                            </div>
                            <div class="col-xs-4 col-sm-8 top-panel-right">
                                <ul class="nav navbar-nav pull-right panel-menu">
                                    <li class="dropdown">
                                        <a href="#" class="dropdown-toggle account" data-toggle="dropdown">
                                            <i class="fa fa-angle-down pull-right"></i>
                                            <div class="user-mini pull-right">
                                                <span class="welcome" style="margin-right: 80px;">Bienvenido,</span>
                                                <span>
                                                    <%= NombreCompleto %>
                                                </span>
                                            </div>
                                        </a>
                                        <ul class="dropdown-menu">
                                            <li>
                                                <a id="btnCambiarContrasenia" href="#">
                                                    <i class="fa fa-pencil-square-o"></i>
                                                    <span>Cambiar Contraseña</</span>
                                                </a>
                                            </li>
                                            <li>
                                                <a id="btnCerrarSesion" href="#">
                                                    <i class="fa fa-power-off"></i>
                                                    <span>Cerrar Sesi&oacute;n</</span>
                                                </a>
                                            </li>
                                        </ul>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>
        <div id="main" class="container-fluid">
            <div class="row">
                <div id="main2" class="container-fluid">
                    <div class="row">
                        <div id="sidebar-left" class="col-xs-2 col-sm-2">
                            <asp:Literal ID="ltrMenuOpciones" runat="server"></asp:Literal>
                        </div>
                        <!--Start Content-->
                        <div id="content" class="col-xs-12 col-sm-10">
                            <div id="BodyPanel" class="row"></div>
                        </div>
                        <!--End Content-->
                    </div>
                </div>
            </div>
        </div>
    </form>
    <div id="modalbox">
        <div class="devoops-modal">
            <div class="devoops-modal-header">
                <div class="modal-header-name">
                    <span>Basic table</span>
                </div>
                <div class="box-icons">
                    <a class="close-link">
                        <i class="fa fa-times"></i>
                    </a>
                </div>
            </div>
            <div class="devoops-modal-inner">
            </div>
            <div class="devoops-modal-bottom">
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var urlCambiarContrasenia = '<%= urlCambiarContrasenia %>';
        var urlDescargarExcel = '<%= urlDescargarExcel %>'
        $(function () {
            fValidarFormulario();
            fAgregarEventosBotones();
        });

        function fValidarFormulario() {
            kendo.culture("es-ES");

            $(document).keydown(function (e) {
                var code = e.keyCode || e.which;
                if (code === 8) {
                    e.preventDefault();
                    return false;
                }
                if (code === 116) { //F5 button
                    event.returnValue = false;
                    event.keyCode = 0;
                    return false;
                }
                if (code === 82) { //R button
                    if (event.ctrlKey) {
                        event.returnValue = false;
                        event.keyCode = 0;
                        return false;
                    }
                }
            });
        }

        function fAgregarEventosBotones() {
            /*$(".url_link").on("click", function (e) {
                e.preventDefault();
                var mOpcion = $(this);
                var urlOpcion = mOpcion.attr("urlOpcion");
                var urlApp = urlOpcion;

                if (urlOpcion.search("http://") == -1 && urlOpcion.search("https://") == -1 && urlOpcion)
                    urlApp = $(location).attr('protocol') + "//" + $(location).attr('host') + urlOpcion;
                // Activación opción menu
                //$(".nav li").removeClass("menuLink-active");
                //mOpcion.parent().addClass('menuLink-active');
                if (urlOpcion && urlOpcion != "ExitLogin") {
                    OpenLink(urlApp, false);
                }
                else if (urlOpcion == "ExitLogin")
                    exitApp();
                return false;
            });*/

            OpenWindow('#btnCambiarContrasenia', urlCambiarContrasenia, "", false, true, 'n', "Cambiar Contraseña");

            $("#btnCerrarSesion").on("click", function (e) {
                $(location).attr('href', 'frmLogeo.aspx');
            });
        }
    </script>
</body>
</html>
