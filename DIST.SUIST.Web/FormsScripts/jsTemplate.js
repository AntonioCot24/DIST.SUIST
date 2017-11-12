var xhr;
var timerUltPos;
var timerMoni;
var LANG = "ES";
var OptCrear = "N";
var OptEditar = "E";
var MsjEmptyRecords = "No hay resultados para mostrar";
var OptionSelectAll = function () { return $("<option>").val("").text("--Seleccionar--") };
var OptionSelectOpt = function () { return $("<option>").val("").text("--Todos--") };
var DialogPopUp;

/// Implement Filter in IE8
if (!Array.prototype.filter) {
    Array.prototype.filter = function (fun /*, thisp */) {
        "use strict";

        if (this === void 0 || this === null)
            throw new TypeError();

        var t = Object(this);
        var len = t.length >>> 0;
        if (typeof fun !== "function")
            throw new TypeError();

        var res = [];
        var thisp = arguments[1];
        for (var i = 0; i < len; i++) {
            if (i in t) {
                var val = t[i]; // in case fun mutates this
                if (fun.call(thisp, val, i, t))
                    res.push(val);
            }
        }

        return res;
    };
}
// Salir Aplicativo
function exitApp() { window.location.href = $(location).attr('protocol') + "//" + $(location).attr('host') + "/" + (FolderApp ? FolderApp + "/" : "") + 'frmLogeo.aspx'; }
function exitAppAjax() { window.location.href = 'frmLogeo.aspx'; }
function errorAppAjax(f, m) {
    var w = $(f);
    if (w.length) {
        if (w.parent().hasClass("ui-dialog-content")) CloseDialog(w.parent());
        else $("div#BodyPanel").empty();
    }
    showMensaje(m, true);
}
function getInternetExplorerVersion() {
    var rv = -1;
    if (navigator.appName == 'Microsoft Internet Explorer') {
        var ua = navigator.userAgent;
        var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
        if (re.exec(ua) != null)
            rv = parseFloat(RegExp.$1);
    }
    else if (navigator.appName == 'Netscape') {
        var ua = navigator.userAgent;
        var re = new RegExp("Trident/.*rv:([0-9]{1,}[\.0-9]{0,})");
        if (re.exec(ua) != null)
            rv = parseFloat(RegExp.$1);
    }
    return rv;
}
// Permisos Ventana
/*
    w => Ventana.
    JsApp => Json Lista Objetos Controlados en Ventana.
    JsPerfil => Json Lista Objetos Permitos en Ventana para Perfil. 
*/
function SetPermisos(w, JsApp, JsPerfil) {
    if (JsApp) {
        var lstApp = jQuery.parseJSON(JsApp);
        $.each(lstApp, function () {
            w.find("#" + this.Objeto).attr("NoAllow", "NoAllow");
            w.find("#" + this.Objeto).css("visibility", "hidden");
        });
        if (JsPerfil) {
            var lstPerfil = jQuery.parseJSON(JsPerfil);
            $.each(lstPerfil, function () {
                w.find("#" + this.Objeto).removeAttr("NoAllow", "NoAllow");
                w.find("#" + this.Objeto).css("visibility", "visible");
            });
        }
    }
}

function CheckObjectoAllow() { return $(this).attr("NoAllow") === null || typeof $(this).attr("NoAllow") === 'undefined'; }
function CheckObjectoAllowByPerfil(JsApp, JsPerfil, Object) {
    if (JsApp) {
        var lstApp = jQuery.parseJSON(JsApp);
        var vResult = lstApp.filter(function (obj) { return obj.Objeto == Object });
        if (vResult.length) {
            if (JsPerfil) {
                var lstPerfil = jQuery.parseJSON(JsPerfil);
                var vResultp = lstPerfil.filter(function (obj) { return obj.Objeto == Object });
                if (vResultp.length)
                    return true;
            }
            return false;
        }
        return true;
    }
    return true;
}
//**    MODAL   **/
/*  Función Abrir Pagina : General || Modal
    b => Boton trigger pagina.
    u => Ruta de pagina.
    p => Propiedades en boton  a enviarse como parametros a la pagina : "prop1 prop2"
    fs => Indica si oculta el menu o se mantiene
    m => True | False ; Si la pagina se abre en modo Modal o dentro del contenedor
    w => Ancho Modal.
    h => Altura Modal.,
    t => Titulo Modal
*/
function OpenWindow(b, u, p, fs, m, w, t) {
    $(function () {
        $(b).on('click', function (e) {
            e.preventDefault();
            var dir = u;
            var btn = $(this);
            var idm = btn.attr("id");
            p = $.trim(p);
            if (p && p != null) {
                var arrparam = new Array(); var arrp = p.split(" ");
                arrparam.push("idmodal=dialog-modal-" + idm);
                // Crear cadena con parametros
                $.each(arrp, function (i, nmp) {
                    nmp = $.trim(nmp);
                    vlp = btn.attr(nmp);
                    if (typeof vlp != 'undefined') arrparam.push(nmp + "=" + vlp);
                });
                dir = u + "?" + arrparam.join("&");
            }
            if (m) {
                return OpenDialog(idm, dir, w, t, true);
            }
            else {
                OpenLink(dir, fs);
            }
        });
    });
}

//**    MODAL   **/
/*  Función Abrir Pagina : General || Modal
    b => Boton trigger pagina.
    u => Ruta de pagina.
    p => Propiedades en boton  a enviarse como parametros a la pagina : "prop1 prop2"
    fs => Indica si oculta el menu o se mantiene
    m => True | False ; Si la pagina se abre en modo Modal o dentro del contenedor
    w => Ancho Modal.
    h => Altura Modal.,
    t => Titulo Modal
*/
function OpenWindowClick(b, u, p, fs, m, w, t) {
    $(function () {
        var dir = u;
        var btn = b;
        var idm = b.prop("id");
        if (p && p != null) {
            var arrparam = new Array(); var arrp = p.split(" ");
            arrparam.push("idmodal=dialog-modal-" + idm);
            // Crear cadena con parametros
            $.each(arrp, function (i, nmp) {
                nmp = $.trim(nmp);
                vlp = btn.attr(nmp);
                if (typeof vlp != 'undefined') arrparam.push(nmp + "=" + vlp);
            });
            dir = u + "?" + arrparam.join("&");
        }
        if (m) {
            return OpenDialog(idm, dir, w, t, true);
        }
        else {
            OpenLink(dir, fs);
        }
    });
}

function OpenWindowTab(b, u, p) {
    $(function () {
        $(b).on('click', function (e) {
            e.preventDefault();
            var dir = u;
            var btn = $(this);
            if (p && p != null) {
                var arrParameters = new Array();
                var arrParam = p.split(" ");
                $.each(arrParam, function (i, nmp) {
                    nmp = $.trim(nmp);
                    vlp = btn.attr(nmp);
                    if (typeof vlp != 'undefined')
                        arrParameters.push(nmp + "=" + vlp);
                });
                dir = u + "?" + arrParameters.join("&");
            }
            /*var dialogToClose = ($(this).parents('form:first')).parent();
            if (typeof dialogToClose != "undefined" && dialogToClose != null)
                CloseDialog(dialogToClose);*/

            window.open(dir);
        });
    });
}

function OpenWindowTabClick(b, u, p) {
    $(function () {
        //$(b).on('click', function (e) {
        //e.preventDefault();
        var dir = u;
        var btn = b;
        if (p && p != null) {
            var arrParameters = new Array();
            var arrParam = p.split(" ");
            $.each(arrParam, function (i, nmp) {
                nmp = $.trim(nmp);
                vlp = btn.attr(nmp);
                if (typeof vlp != 'undefined')
                    arrParameters.push(nmp + "=" + vlp);
            });
            dir = u + "?" + arrParameters.join("&");
        }
        /*var dialogToClose = ($(this).parents('form:first')).parent();
        if (typeof dialogToClose != "undefined" && dialogToClose != null)
            CloseDialog(dialogToClose);*/

        window.open(dir);
        //});
    });
}

/*  Función Abrir Modal
    m => Object Modal.
    w => Ancho Modal.
    h => Altura Modal.
    t => Titulo Modal.
    d => Se destruye al cerrar (true,false).
*/
function OpenDialog(idm, dir, w, t, d) {
    var modalSize = BootstrapDialog.SIZE_NORMAL;
    if (w == "a") modalSize = BootstrapDialog.SIZE_WIDE;

    return BootstrapDialog.show({
        title: t,
        type: BootstrapDialog.TYPE_PRIMARY,
        message: $("<div id='dialog-modal-" + idm + "'></div>").load(dir),
        draggable: true,
        closable: true,
        closeByBackdrop: false,
        closeByKeyboard: false,
        size: modalSize,
        onshown: function (dialogRef) {
            /* Oculta Mensaje */
            if ($("#alertmsg").length)
                $("#alertmsg").hide();
            /* Oculta Validaciones */
            var fm = $(this).find('.formu');
            if (fm.length)
                fm.validationEngine('hideAll');
            /*fix IE 8*/
            var thisModal = $(this).parent();
            var zind = 102 + $('.ui-dialog').length;
            thisModal.css('z-index', (zind + 1)).parent().find('.ui-widget-overlay').filter(function () { return typeof $(this).attr("level") === 'undefined'; }).css('z-index', zind).attr("level", "1");;
            /* Change Title Descrip */
            thisModal.find('.ui-dialog-titlebar-close').attr('title', "Cerrar Ventana");
            /* Validaciones */
            dates(".date-pick-popup", ".time-pick-popup");
            letters(".letters-input-popup");
            alphaNumerics(".alphaNumeric-input-popup");
            validateInput(".validate-input-popup");
            spinner();
            crearBoxHeader();
            crearPanelHeader();
            inputmask();
            selectedChosen();
            //WinMove();
            if (!$("label").hasClass("text-right")) {
                if (!$("label").hasClass("no-text-right"))
                    $("label").addClass("text-right");
                //$("label").addClass("text-capitalize");
            }
            /* keydown */
            thisModal.find("input[type=password], input[type=text], textarea").on("keydown", function (e) {
                e.stopPropagation();
                if (e != null) {
                    var code = e.keyCode || e.which;
                    if (code == 13) {
                        e.preventDefault();
                        return false;
                    }
                    return true;
                }
            });
        },
    });
}

function CloseDialog(m) {
    if (m.length)
        m.dialog("close"); //m.dialog("destroy");
}
/*  Abre Pagina en contenedor
    u : URL de la pagina a abrir.
    f : Indica si abre en modo oculta o conserva el menu. true | false; 
*/
function OpenLink(u, fs) {
    timerUltPos = window.clearInterval(timerUltPos);
    timerMoni = window.clearInterval(timerMoni);
    /* Oculta Mensaje */
    if ($("#alertmsg").length)
        $("#alertmsg").hide();
    /* Oculta Validaciones */
    if ($('.formu').length)
        $('.formu').validationEngine('hideAll');
    // Clear Panel
    $('#BodyPanel').remove();
    $('#FilterPanel').remove();
    $('html,body').animate({ scrollLeft: 0 }, 'fast');
    // Set Loading 
    //$("div#content").append('<div id="BodyPanel" class="col-md-10 col-sm-10"></div><div id="FilterPanel" class="col-md-2 col-sm-2"></div>');
    $("div#content").append('<div id="BodyPanel" class="col-md-12 col-sm-12"></div>');
    $('div#BodyPanel').append("<div id='overlayload'> </div>");
    $('#overlayload').css({ "display": "block", "opacity": 0.4, "position": "relative", "left": 0, "top": 0, "background-color": "#aaa", "z-index": 999, "width": $("#HeadPanel").width(), "height": $(window).height() - $("#nav-superior").height() - 50 });
    $('#overlayload').append("<div id='overlayContent'><img src='Content/img/progress-circular.gif' alt='Cargando' border='0' /> </div>");
    $("#overlayContent").css({ "display": "block", "position": "relative", "z-index": 9999, "width": 120, "height": 120 }).center({ against: 'parent' });
    //$("#overlayContent").css({ "display": "block", "position": "relative", "z-index": 9999, "width": 120, "height": 120 });
    $('#overlayContent').append("<h2>Cargando...</h2>");
    $('body').css({ "overflow": "hidden" });
    //var ml = $("div#co").css('padding-left');
    //if (ml != '5px') { $("#co").css('padding-top', '45px'); $("#co").css('padding-left', '5px'); $("#co").css('padding-right', '5px'); $("#co").css('padding-bottom', '10px'); }
    cleanDOM();
    if (xhr != null) { xhr.abort(); }
    xhr = $.ajax({
        url: u,
        type: 'POST',
        cache: false,
        error: function (xhr, ajaxOptions, thrownError) { },
        success: function (respuesta) {
            $("div#BodyPanel").empty().append(respuesta);
            $("div#BodyPanel").height('auto');
            $('body').css({ "overflow": "auto" });
            //Full Screen
            //$("#toggleMenu").trigger("click");
            var fm = $("div#BodyPanel").find('.formu');
            if (fm.length)
                fm.validationEngine();
            dates(".date-pick", ".time-pick");
            letters(".letters-input");
            alphaNumerics(".alphaNumeric-input");
            validateInput(".validate-input");
            spinner();
            crearBoxHeader();
            crearPanelHeader();
            inputmask();
            selectedChosen();
            //WinMove();
            if (!$("label").hasClass("text-right")) {
                if (!$("label").hasClass("no-text-right"))
                    $("label").addClass("text-right");
                //$("label").addClass("text-capitalize");
            }
            /* keydown */
            $("div#BodyPanel").find("input[type=password], input[type=text], textarea").on("keydown", function (e) {
                e.stopPropagation();
                if (e != null) {
                    var code = e.keyCode || e.which;
                    if (code == 13) {
                        e.preventDefault();
                        return false;
                    }
                    return true;
                }
            });
        }
    });
    $('html,body').animate({ scrollTop: 0 }, 'slow');
}

function OpenLinkParams(u, p, fs) {
    timerUltPos = window.clearInterval(timerUltPos);
    timerMoni = window.clearInterval(timerMoni);
    var dir = '';
    p = $.trim(p);
    if (p && p != null) {
        var arrparam = new Array(); var arrp = p.split(" ");
        //arrparam.push("idmodal=dialog-modal-" + idm);
        // Crear cadena con parametros
        $.each(arrp, function (i, nmp) {
            nmp = $.trim(nmp);
            var param = nmp.split(":");
            if (typeof param[1] != 'undefined') arrparam.push(param[0] + "=" + param[1]);
        });
        dir = u + "?" + arrparam.join("&");
    }
    /* Oculta Mensaje */
    if ($("#alertmsg").length)
        $("#alertmsg").hide();
    /* Oculta Validaciones */
    if ($('.formu').length)
        $('.formu').validationEngine('hideAll');
    // Clear Panel
    $('div#BodyPanel').remove();
    $('html,body').animate({ scrollLeft: 0 }, 'fast');
    // Set Loading 
    $("div#content").append('<div id="BodyPanel"></div>');
    $('div#BodyPanel').append("<div id='overlayload'> </div>");
    $('#overlayload').css({ "display": "block", "opacity": 0.4, "position": "relative", "left": 0, "top": 0, "background-color": "#aaa", "z-index": 999, "width": $("#HeadPanel").width(), "height": $(window).height() - $("#nav-superior").height() - 50 });
    $('#overlayload').append("<div id='overlayContent'><img src='Content/img/progress-circular.gif' alt='Cargando' border='0' /> </div>");
    $("#overlayContent").css({ "display": "block", "position": "relative", "z-index": 9999, "width": 120, "height": 120 }).center({ against: 'parent' });
    //$("#overlayContent").css({ "display": "block", "position": "relative", "z-index": 9999, "width": 120, "height": 120 });
    $('#overlayContent').append("<h2>Cargando...</h2>");
    $('body').css({ "overflow": "hidden" });
    //var ml = $("div#co").css('padding-left');
    //if (ml != '5px') { $("#co").css('padding-top', '45px'); $("#co").css('padding-left', '5px'); $("#co").css('padding-right', '5px'); $("#co").css('padding-bottom', '10px'); }
    cleanDOM();
    if (xhr != null) { xhr.abort(); }
    xhr = $.ajax({
        url: dir,
        type: 'POST',
        cache: false,
        error: function (xhr, ajaxOptions, thrownError) { },
        success: function (respuesta) {
            $("div#BodyPanel").empty().append(respuesta);
            $("div#BodyPanel").height('auto');
            $('body').css({ "overflow": "auto" });
            //Full Screen
            //$("#toggleMenu").trigger("click");
            var fm = $("div#BodyPanel").find('.formu');
            if (fm.length)
                fm.validationEngine();
            dates(".date-pick", ".time-pick");
            letters(".letters-input");
            alphaNumerics(".alphaNumeric-input");
            validateInput(".validate-input");
            spinner();
            crearBoxHeader();
            crearPanelHeader();
            inputmask();
            selectedChosen();
            //WinMove();
            if (!$("label").hasClass("text-right")) {
                if (!$("label").hasClass("no-text-right"))
                    $("label").addClass("text-right");
                //$("label").addClass("text-capitalize");
            }
            /* keydown */
            $("div#BodyPanel").find("input[type=password], input[type=text], textarea").on("keydown", function (e) {
                e.stopPropagation();
                if (e != null) {
                    var code = e.keyCode || e.which;
                    if (code == 13) {
                        e.preventDefault();
                        return false;
                    }
                    return true;
                }
            });
        }
    });
    $('html,body').animate({ scrollTop: 0 }, 'slow');
}

/*  Crea Taps en contenedor
    u : URL de la pagina a abrir.
    f : Indica si abre en modo oculta o conserva el menu. true | false; 
*/
function OpenTaps(u, fs) {
    timerUltPos = window.clearInterval(timerUltPos);
    timerMoni = window.clearInterval(timerMoni);
    /* Oculta Mensaje */
    if ($("#alertmsg").length)
        $("#alertmsg").hide();
    /* Oculta Validaciones */
    if ($('.formu').length)
        $('.formu').validationEngine('hideAll');
    // Limpia panel
    $('div#TabPanel').remove();
    $('html,body').animate({ scrollLeft: 0 }, 'fast');
    /*
    if (parseBoolean(fs)) {
        $('#page_content').append('<div id="BodyPanel" style="margin-left: 1px; "></div>');
    }
    else { $('#page_content').append('<div id="BodyPanel" style="margin-left: 190px; "></div>'); }
    */
    $("#tcEtapas").append('<div id="TabPanel"></div>');
    var preloaderdiv = '<div class="preloader"><img src="Images/progress_circular_small.gif" /></div>';
    $('div#TabPanel').html(preloaderdiv);
    //var ml = $("div#co").css('padding-left');
    //if (ml != '5px') { $("#co").css('padding-top', '45px'); $("#co").css('padding-left', '5px'); $("#co").css('padding-right', '5px'); $("#co").css('padding-bottom', '10px'); }
    cleanDOM();
    if (xhr != null) { xhr.abort(); }
    xhr = $.ajax({
        url: u,
        type: 'POST',
        cache: false,
        error: function (xhr, ajaxOptions, thrownError) { },
        success: function (respuesta) {
            $("div#TabPanel").height('auto');
            $("div#TabPanel").empty().append(respuesta);

            var fm = $(this).find('.formu');
            if (fm.length)
                fm.validationEngine();

            //dates(".date-pick", ".time-pick");
            //letters(".letters-input");
            //alphaNumerics(".alphaNumeric-input");
            //validateInput(".validate-input");
            //spinner();
            //addControlInput();
            //addControlDocuments();
            //addControlDest();
            $("div#TabPanel").find("input[type=password], input[type=text], textarea").on("keydown", function (e) {
                e.stopPropagation();
                if (e != null) {
                    var code = e.keyCode || e.which;
                    return true;
                }
            });
        }
    });
    $('html,body').animate({ scrollTop: 0 }, 'slow');
}

// Limpiar Eventos Botones
function cleanDOM() {
    $('#clean').unbind('click'); $('#delete').unbind('click'); $('#restore').unbind('click'); $('#add').unbind('click');
    $('#search').unbind('click'); $('.bt-edit').unbind('click'); $('.bt-delete').unbind('click'); $('.bt-restore').unbind('click');
    $('#si').unbind('click'); $('#no').unbind('click');
    $('.btn-hide').unbind('click'); $('.numeric-input').unbind('keypress keyup'); $(".letters-input").unbind('keypress keyup'); $(".alphaNumeric-input").unbind('keypress keyup');
    $('input, textarea').unbind('keydown');
}
// Crear Checkboxes
function styleCHK() { $(function () { $('input[type=checkbox],input[type=radio]').prettyCheckboxes(); }); }
// Crear Tabs
function tabs() { $("#tabs").tabs(); $("#tabss").tabs(); }

function crearBoxHeader() {
    $(function () {
        var titulo = "";
        var icon = "";

        var lstBoxHeader = $(".box-header");

        $.each(lstBoxHeader, function (i, val) {
            var id = $(val).prop("id");

            titulo = $("#" + id).attr("titulo");
            icon = $("#" + id).attr("icon");

            $("#" + id).append("<div class='box-name'><i class='" + icon + "'></i><span>" + titulo + "</span></div>" +
                "<div class='box-icons'>" +
                "<a class='collapse-link'><i class='fa fa-chevron-up'></i></a>" +
                //"<a class='expand-link'><i class='fa fa-expand'></i></a>" +
                "</div>" +
                "<div class='no-move'></div>");
        });
    });
}

function crearPanelHeader() {
    $(function () {
        var titulo = "";
        var icon = "";

        var lstBoxHeader = $(".panel");

        $.each(lstBoxHeader, function (i, val) {
            var id = $(val).prop("id");

            titulo = $("#" + id).attr("titulo");
            icon = $("#" + id).attr("icon");

            $("#" + id).append("<i class='" + icon + "'>&nbsp;</i><span>" + titulo + "</span>");
        });

        var lstBoxHeader = $(".panel-heading");

        $.each(lstBoxHeader, function (i, val) {
            var id = $(val).prop("id");

            titulo = $("#" + id).attr("titulo");
            icon = $("#" + id).attr("icon");

            $("#" + id).append("<i class='" + icon + "'>&nbsp;</i><span>" + titulo + "</span>");
        });
    });
}

function inputmask() {
    $.each($(".mask_Placa"), function (i, val) {
        var id = $(val).prop("id");

        $("#" + id).css("text-transform", "uppercase");
        $("#" + id).mask("***-***", { placeholder: " " });
    });
    $.each($(".mask_Anio"), function (i, val) {
        var id = $(val).prop("id");

        $("#" + id).mask("9999", { placeholder: " " });
    });
}

/*  Validacion de inputs    */
function validateInput(n) {
    $(n).on("keypress", function (e) {
        e.stopPropagation();
        var tecla = e.keyCode || e.which;
        var which = e.which;
        if (typeof which != "undefined") {
            if (which == 8 || which == 13 || which == 0)
                return true;
        }
        var patron = /[.0-9a-zñáéíóúÁÉÍÓÚ\s]/gi;
        var input = $(this);
        if (input.hasClass("numeric-input"))
            patron = /[0-9]/ig;
        else if (input.hasClass("decimal-input"))
            patron = /^\d{1,8}(\.\d{0,2})?$/;
        else if (input.hasClass("decimal-factor-input"))
            patron = /^\d{1,4}(\.\d{0,6})?$/;
        else if (input.hasClass("phone-input"))
            patron = /[\*\#0-9]/i;
        else if (input.hasClass("address-input"))
            patron = /[\-\#.0-9a-zñáéíóúÁÉÍÓÚ\s]/gi;
        else if (input.hasClass("nroexpediente-input"))
            patron = /[\/\-\_.0-9a-z ]/gi;
        else if (input.hasClass("nrodocumento-input"))
            patron = /[\-0-9a-z]/gi;
        var te = String.fromCharCode(tecla);

        var maxlen = input.attr("maxlength");

        if (input.hasClass("decimal-input") || input.hasClass("decimal-factor-input")) {
            var decimallength = input.attr("scale-decimal");
            te = input.val() + te;

        }

        return patron.test(te);
    });
    $(n).on('paste', function () {
        var input = $(this); //save reference to element for use laster
        setTimeout(function () { //break the callstack to let the event finish
            var newval = '';
            var patron = /[^.0-9a-zñáéíóúÁÉÍÓÚ\s]/gi;
            if (input.hasClass("numeric-input"))
                patron = /[^0-9]/ig;
            else if (input.hasClass("decimal-input"))
                patron = /^[0-9]*\.?[0-9]*$/;
            else if (input.hasClass("phone-input"))
                patron = /[^\*\#0-9]/gi;
            else if (input.hasClass("address-input"))
                patron = /[^\-\#.0-9a-zñáéíóúÁÉÍÓÚ\s]/gi;
            else if (input.hasClass("nroexpediente-input"))
                patron = /[^\/\-\_0-9a-z]/gi;
            else if (input.hasClass("nrodocumento-input"))
                patron = /[^\-0-9a-z]/gi;
            newval = input.val().replace(patron, '');
            var maxlen = input.attr("maxlength");
            if (typeof maxlen != 'undefined' && maxlen != null && newval.length > 0) {
                newval = newval.substr(0, maxlen);
            }
            input.val(newval);
        }, 0);
    });
}

function ValidateInputDate(d) {
    var o = $(d);
    if (o.length) {
        if (o.hasClass("hasDatepicker") && o.val()) {
            var max = o.datepicker("option", "maxDate");
            var min = o.datepicker("option", "minDate");
            var val = new Date(formatDate(o.val()));
            if (max != null) {
                if (val > max)
                    return false;
            }
            if (min != null) {
                if (min > val)
                    return false;
            }
        }
    }
    return true;
}

/*  Validacion de Fechas    */
function dates(d, t) {
    $(function () {
        //$(".date-pick").prop("readonly", true);
        //$(d).prop("readonly", true);
        $(d).parent().find(".input-group-addon").click(function () { $(this).parent().find(d).trigger("focus"); });
        $(d).datepicker({
            //showOn: 'button', buttonImage: 'Images/icons/calendar.png', buttonImageOnly: true, 
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1,
            showAnim: "fadeIn",
            yearRange: 'c-30:c+30',
            /* fix buggy IE focus functionality */
            fixFocusIE: false,
            /* blur needed to correctly handle placeholder text */
            onSelect: function (dateText, inst) {
                this.fixFocusIE = true;
                var SetMaxDate = $(this).attr("SetMaxDate");
                var SetMinDate = $(this).attr("SetMinDate");

                if (typeof SetMaxDate != "undefined" && SetMaxDate != "") {
                    var arrSetMaxDate = $(this).attr("SetMaxDate").split(" ");
                    $.each(arrSetMaxDate, function (i, txtSetMaxDate) {
                        if (txtSetMaxDate != "")
                            $("#" + txtSetMaxDate).datepicker("option", "maxDate", dateText);
                    });

                }
                if (typeof SetMinDate != "undefined" && SetMinDate != "") {
                    var arrSetMinDate = $(this).attr("SetMinDate").split(" ");
                    $.each(arrSetMinDate, function (i, txtSetMinDate) {
                        if (txtSetMinDate != "")
                            $("#" + txtSetMinDate).datepicker("option", "minDate", dateText);
                    });
                }
                if ($(this).hasClass("validate[required]"))
                    $(this).validationEngine('validate');
                $(this).change().focus();

            },
            onClose: function (dateText, inst) {
                this.fixFocusIE = true;
                this.focus();
            },
            beforeShow: function (input, inst) {
                var result = getInternetExplorerVersion() > -1 ? !this.fixFocusIE : true;
                this.fixFocusIE = false;
                return result;
            }
        });
        $(d).datepicker("option", $.datepicker.regional[LANG]);
        $(d).each(function () {
            var MaxDate = $(this).attr("MaxDate");
            var MinDate = $(this).attr("MinDate");

            if (typeof MaxDate != "undefined" && MaxDate != "")
                $(this).datepicker("option", "maxDate", new Date(formatDate(MaxDate)));
            if (typeof MinDate != "undefined" && MinDate != "")
                $(this).datepicker("option", "minDate", new Date(formatDate(MinDate)));
        });
        /*
        if ($(d).val() == "" || $(d).val() == "undefined")
            $(d).val(GetFechaActual());
        */
        $(d).attr('maxlength', 10);
        //$(d).css('width', '75px');
        /*
        if ($.browser.msie && $.browser.version == '7.0') { $(".ui-datepicker-trigger").css('margin-bottom', '1px'); }
        else { $(".ui-datepicker-trigger").css('margin-bottom', '-4px'); }
        */
        $(".ui-datepicker-trigger").css('margin-left', '-20px');
        $(d).keypress(function () {
            OnlyNumeros(); if (($(this).val().length == 2) || ($(this).val().length == 5)) { $(this).val($(this).val() + $.datepicker.regional[LANG].separator); }
            if ($(this).val().length = $(this).attr('maxlength') - 1) { return; }
        });
        $(d).blur(function () {
            if (!isDate($(this).val())) {
                $(this).val(""); //GetFechaActual()
            }
        });
        if (t != "" && (typeof t != "undefined")) {
            $(t).attr('maxlength', 5);
            $(t).css('width', '50px');
            $(t).keypress(function () {
                OnlyNumeros(); if ($(this).val().length == 2) { $(this).val($(this).val() + ':'); } if ($(this).val().length = $(this).attr('maxlength') - 1) { return; }
            });
            $(t).blur(function () {
                if (!CheckEsHora($(this).val())) { $(this).val('00:00'); }
            });
        }
    });
}

function spinner() {
    $.each($(".spinner"), function (i, val) {
        $(val).TouchSpin({
            verticalbuttons: false,
            min: 0,
            max: 999999,
            buttondown_class: "btn btn-info",
            buttonup_class: "btn btn-info"
        });
    });

    $.each($(".spinner-anio"), function (i, val) {
        $(val).TouchSpin({
            verticalbuttons: false,
            min: 1950,
            max: 9999,
            buttondown_class: "btn btn-info",
            buttonup_class: "btn btn-info"
        });
    });

    $.each($(".spinner-km"), function (i, val) {
        $(val).TouchSpin({
            verticalbuttons: false,
            min: 0,
            max: 1000000,
            buttondown_class: "btn btn-info",
            buttonup_class: "btn btn-info"
        });
    });

    $.each($(".spinner-precio-dolares"), function (i, val) {
        $(val).TouchSpin({
            verticalbuttons: false,
            min: 0,
            max: 9999999,
            prefix: '$',
            buttondown_class: "btn btn-info",
            buttonup_class: "btn btn-info"
        });
    });

    $.each($(".spinner-precio-soles"), function (i, val) {
        $(val).TouchSpin({
            verticalbuttons: false,
            min: 0,
            max: 9999999,
            prefix: 'S/.',
            buttondown_class: "btn btn-default",
            buttonup_class: "btn btn-default"
        });
    });

    $.each($(".spinner-pisos"), function (i, val) {
        $(val).TouchSpin({
            verticalbuttons: false,
            min: 0,
            max: 8,
            step: 2,
            buttondown_class: "btn btn-info",
            buttonup_class: "btn btn-info"
        });
    });

    $.each($(".spinner-tapas"), function (i, val) {
        $(val).TouchSpin({
            verticalbuttons: false,
            min: 0,
            max: 5,
            buttondown_class: "btn btn-info",
            buttonup_class: "btn btn-info"
        });
    });
}
/*Funcion que permite validar solo letras en las cajas de texto*/
function letters(n) {
    $(n).on("keypress", function (e) {
        e.stopPropagation();
        var tecla = e.keyCode || e.which;
        var which = e.which;
        if (typeof which != "undefined") {
            if (which == 8 || which == 13 || which == 0)
                return true;
        }
        var patron = /[.a-zñáéíóúÁÉÍÓÚ\s]/i;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    });
    $(n).on('paste', function () {
        var input = $(this); //save reference to element for use laster
        setTimeout(function () { //break the callstack to let the event finish
            var newval = '';
            var maxlen = input.attr("maxlength");
            newval = input.val().replace(/[^.a-zñáéíóúÁÉÍÓÚ\s]/gi, '');
            if (typeof maxlen != 'undefined' && maxlen != null && newval.length > 0) {
                newval = newval.substr(0, maxlen);
            }
            input.val(newval);
        }, 0);
    });
}

function alphaNumerics(n) {
    $(n).on("keypress", function (e) {
        e.stopPropagation();
        var tecla = e.keyCode || e.which;
        var which = e.which;
        if (typeof which != "undefined") {
            if (which == 8 || which == 13 || which == 0)
                return true;
        }
        var patron = /[\-.0-9a-zñáéíóúÁÉÍÓÚ\s]/gi;
        var te = String.fromCharCode(tecla);
        return patron.test(te);
    });
    $(n).on('paste', function () {
        var input = $(this); //save reference to element for use laster
        setTimeout(function () { //break the callstack to let the event finish
            var newval = '';
            var maxlen = input.attr("maxlength");
            newval = input.val().replace(/[^\-.0-9a-zñáéíóúÁÉÍÓÚ\s]/gi, '');
            if (typeof maxlen != 'undefined' && maxlen != null && newval.length > 0) {
                newval = newval.substr(0, maxlen);
            }
            input.val(newval);
        }, 0);
    });
}

function NumberDecimal(n) {

    $(n).on("keypress", function (e) {
        e.stopPropagation();
        var tecla = e.keyCode || e.which;
        var which = e.which;

        if (typeof which != "undefined") {
            if (which == 8 || which == 13 || which == 0)
                return true;
        }
        var patron = /[0-9]+(\.[0-9][0-9]?)?/;
        var te = String.fromCharCode(tecla);

        return patron.match(te);
    });
}

function selectedChosen() {
    $(".chosen").chosen({
        disable_search: false,
        allow_single_deselect: true,
        no_results_text: "No se encontraron registros.",
        placeholder_text_single: ""
    });
}

function SetRowNumGrid(Grid) {
    var g = $(Grid);
    if (g.length) {
        if (g[0].grid) {
            var id = g.getGridParam("pager");
            var d = id.substr(1, id.length);
            var n = $(id);
            // Only exists data in grid 
            $("#" + d + "_center .ui-pg-table").children("tbody").children("tr").find("#td_rowNumGrid").remove();
            if (g.jqGrid('getGridParam', 'records') > 0) {
                // Add Input Nro Registros
                $("#" + d + "_center .ui-pg-table").children("tbody").children("tr").append("<td id='td_rowNumGrid'>&nbsp;&nbsp;Nro. Registros: <input type='text' size='2' style='height:18px;' id='" + d + "_RowNum' maxlength='3' IdGrid='" + Grid + "' class='validate-input numeric-input'/></td>");
                var i = $("#" + d + "_RowNum");
                // Set Input RowNum Grilla
                i.val(g.getGridParam("rowNum"));
                // Events To Input
                i.on("keydown", function (e) {
                    e.stopPropagation();
                    if (e != null) {
                        var code = e.keyCode || e.which;
                        if (code == 13) {
                            e.preventDefault();
                            var nr = $(this).val();
                            var ig = $(this).attr("IdGrid");
                            $(ig).setGridParam({ rowNum: parseInt(nr) });
                            $(ig).trigger("reloadGrid");
                            return false;
                        }
                        else {
                            return true;
                        }
                    }
                });
            }
        }
    }
}

/*  Funcion que permite limpiar un formulario
    f   => Formulario a limpiar
*/
function CleanForm(f) {
    $(":input", f).not(':button, :submit, :reset, :hidden')
        .val('')
        .removeAttr('checked')
        .removeAttr('selected');
}

/*  Funcion que permite abrir un modal de confirmacion
    c   => Control
    p   => Parametros
    m   => Mensaje de Confirmacion.
    lb  => Etiqueta(Texto) del Boton Aceptar.
    me  => Mensaje de Espera.
    fc  => Funcion a llamar
*/
function OpenConfirmation(c, p, m, lb, me, fc) {

    var arrParam = [];
    var arrParameters = new Array();
    var vlp;
    if (typeof c != "undefined" && c != null) {
        p = $.trim(p);
        if (p && p != null)
            arrParam = p.split(" ");
        $.each(arrParam, function (ind, namep) {
            namep = $.trim(namep);
            vlp = c.attr(namep);
            if (typeof vlp != 'undefined' && vlp != null)
                arrParameters.push(vlp);
        });
    }

    BootstrapDialog.show({
        title: 'Mensaje de Confirmaci&oacute;n',
        type: BootstrapDialog.TYPE_PRIMARY,
        message: m,
        buttons: [{
            label: lb,
            cssClass: 'btn btn-primary',
            icon: 'fa fa-check',
            hotkey: 13,
            autospin: true,
            action: function (dialogConfirm) {
                dialogConfirm.enableButtons(false);
                dialogConfirm.setClosable(false);
                dialogConfirm.getModalBody().html(me);
                var efunction = window[fc];
                if (arrParameters.length > 0)
                    efunction(dialogConfirm, arrParameters.join());
                else
                    efunction(dialogConfirm);
            }
        }, {
            label: 'Cancelar',
            cssClass: 'btn btn-danger',
            icon: 'fa fa-times',
            action: function (dialogConfirm) {
                dialogConfirm.close();
            }
        }]
    });
}
/********************************************************/
//funciones para mostrar mensaje de no se encontraron registros
function LoadComplete(gridname) {
    if ($(gridname).getGridParam('records') == 0)
        DisplayEmptyText(true, gridname);
    else
        DisplayEmptyText(false, gridname);
}

function DisplayEmptyText(display, gridname) {
    var grid = $(gridname);
    var emptyText = 'No se encontraron registros.';

    var container = grid.parents('.ui-jqgrid-view');

    if (display) {
        container.find('.ui-jqgrid-bdiv').hide();
        container.find('#EmptyData1').remove();
        container.append("<span id='EmptyData1'>" + emptyText + "</span>")

    }
    else {
        container.find('.ui-jqgrid-bdiv').show();
        container.find('#EmptyData1').remove();
    }
}