<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogeo.aspx.cs" Inherits="DIST.SUIST.Web.frmLogeo" %>

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

    <!-- Bootstrap core CSS -->
    <link href="<%= Page.ResolveUrl("~/Content/bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/font-awesome.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/style_v2.css") %>" rel="stylesheet" type="text/css" />

    <!-- Custom styling plus plugins -->
    <link href="<%= Page.ResolveUrl("~/Content/validationEngine.jquery.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveUrl("~/Content/validationEngine.template.css") %>" rel="stylesheet" type="text/css" />

    <script src="<%= Page.ResolveUrl("~/Scripts/jquery-3.2.1.min.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/validator/jquery.validationEngine-es.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/validator/jquery.validationEngine.min.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/jquery.center.min.js") %>" type="text/javascript"></script>

    <script src="<%= Page.ResolveUrl("~/FormsScripts/jsTemplate.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/FormsScripts/jsModulo.js") %>"></script>
</head>
<body>
    <form id="frmLogeo" class="form-horizontal formu" role="form" runat="server">
        <div class="container-fluid">
            <div id="page-login" class="row">
                <div class="col-xs-12 col-md-4 col-md-offset-4 col-sm-6 col-sm-offset-3">
                    <%--<div class="text-right">
                        <a href="page_register.html" class="txt-default">Need an account?</a>
                    </div>--%>
                    <div class="box" style="border-radius: 15px; -moz-border-radius: 15px; -webkit-border-radius: 15px">
                        <div class="box-content" style="border-radius: 15px; -moz-border-radius: 15px; -webkit-border-radius: 15px">
                            <div class="text-center">
                                <h3 class="page-header">Iniciar Sesión</h3>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <label class="control-label">Usuario o Correo:</label>
                                    <input id="txtUsuarioLogin" type="text" class="form-control validate[required,custom[email]]" placeholder="Usuario" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <label class="control-label">Contraseña:</label>
                                    <input id="txtContraseniaLogin" type="password" class="form-control validate[required]" placeholder="Contraseña" runat="server" />
                                </div>
                            </div>
                            <div class="text-center">
                                <button id="btnIniciarSesion" type="button" class="btn btn-primary submit">Acceder <i class="fa fa-arrow-right"></i></button>
                                <a class="reset_pass" style="display: none" href="#">¿Perdiste tu contraseña?</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var wsSeguridad = "<%= wsSeguridad %>";

        var UsuarioBE = function () {
            this.Usuario = "";
            this.Contrasenia = "";
        };

        $(function () {
            fValidarFormulario();
            fAgregarEventosBotones();
        });

        function fValidarFormulario() {
            $('#txtUsuarioLogin').on('keypress', function (e) {
                var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
                if (tecla == 13)
                    $("#txtContraseniaLogin").focus();
                else
                    return true;
            });

            $('#txtContraseniaLogin').on('keypress', function (e) {
                var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
                if (tecla == 13)
                    $("#btnIniciarSesion").trigger("click");
                else
                    return true;
            });
        }

        function fAgregarEventosBotones() {
            $("#btnIniciarSesion").on("click", function (e) {
                var isValidate = true;
                isValidate = $("#frmLogeo").validationEngine('validate');
                if (!isValidate) return false;

                var objUsuarioBE = new UsuarioBE();
                objUsuarioBE.Usuario = $("#txtUsuarioLogin").val() != "" ? $("#txtUsuarioLogin").val() : "";
                objUsuarioBE.Contrasenia = $("#txtContraseniaLogin").val() != "" ? $("#txtContraseniaLogin").val() : "";

                var param = "{ 'objUsuario': " + JSON.stringify(objUsuarioBE) + "}";
                $.ajax({
                    type: "POST",
                    url: wsSeguridad + "/LogearUsuario",
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
                            $(location).attr('href', 'frmInicio.aspx');
                        }
                        else {
                            showMensaje(mensajeResult, eserror);
                        }
                        return true;
                    },
                    error: function (request, status, error) {
                        alert("Error al acceder al metodo");
                        return true;
                    }
                });
            });
        }
    </script>
</body>
</html>
