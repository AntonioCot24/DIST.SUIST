var DatatableSp = {
    "sProcessing": "Procesando...",
    "sLengthMenu": "Mostrar _MENU_ registros",
    "sZeroRecords": "No se encontraron resultados",
    "sEmptyTable": "Ningún dato disponible en esta tabla",
    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
    "sInfoPostFix": "",
    "sSearch": "Buscar:",
    "sInfoThousands": ",",
    "sLoadingRecords": "Cargando...",
    "oPaginate": {
        "sFirst": "Primero",
        "sLast": "Último",
        "sNext": "Siguiente",
        "sPrevious": "Anterior"
    },
    "oAria": {
        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
    }
};

function LimpiarFiltros() {
    $('input[type=text]').val(''); $("select").children().removeAttr("selected");
    $('select :first-child').attr("selected", "selected");
    $('#chkflg').attr('checked', true); $('label[for="' + $('#chkflg').attr('id') + '"]').addClass('checked');
    $('textarea').val('');
}

function LimpiarDiv(idDiv) {
    $('#' + idDiv + ' input[type=text]').val('');
    $('#' + idDiv + ' select').children().removeAttr("selected");
    $('#' + idDiv + ' select :first-child').attr("selected", "selected");
    $('#' + idDiv + ' input[type=check]').attr('checked', false);
    $('#' + idDiv + ' input[type=checkbox]').attr('checked', false);
    $('#' + idDiv + ' input[type=radio]').attr('checked', false);
    $('#' + idDiv + ' textarea').val('');
}

function BloquearDiv(idDiv, limp) {
    $("#" + idDiv + " button").prop("disabled", true);
    $("#" + idDiv + " select").prop("disabled", true);
    $("#" + idDiv + " span").prop("disabled", true);
    $("#" + idDiv + " span>input[type=file]").prop("disabled", true).addClass("btn-default").removeClass("btn-success");
    $("#" + idDiv + " input[type=text]").prop("disabled", true);
    $("#" + idDiv + " input[type=radio]").prop('disabled', true);
    $("#" + idDiv + " input[type=radio]").siblings("i").addClass("txt-default");
    $("#" + idDiv + " input[type=checkbox]").prop('disabled', true);
    $("#" + idDiv + " input[type=checkbox]").siblings("i").addClass("txt-default");
    $("#" + idDiv + " input[type=button]").prop('disabled', true);
    $("#" + idDiv + " table tr td input[type=button]").prop('disabled', true);
    $("#" + idDiv + " input[type=check]").prop('disabled', true);
    $("#" + idDiv + " textarea").prop("disabled", true);

    if (limp)
        LimpiarDiv(idDiv);
}

function ActivarDiv(idDiv, limp) {
    $("#" + idDiv + " button").prop("disabled", false);
    $("#" + idDiv + " select").prop("disabled", false);
    $("#" + idDiv + " span").prop("disabled", false);
    $("#" + idDiv + " span>input[type=file]").prop("disabled", false).removeClass("btn-default").addClass("btn-success");
    $("#" + idDiv + " input[type=text]").prop("disabled", false);
    $("#" + idDiv + " input[type=radio]").prop('disabled', false);
    $("#" + idDiv + " input[type=radio]").siblings("i").removeClass("txt-default");
    $("#" + idDiv + " input[type=checkbox]").prop('disabled', false);
    $("#" + idDiv + " input[type=checkbox]").siblings("i").removeClass("txt-default");
    $("#" + idDiv + " input[type=button]").prop('disabled', false);
    $("#" + idDiv + " table tr td input[type=button]").prop('disabled', false);
    $("#" + idDiv + " input[type=check]").prop('disabled', false);
    $("#" + idDiv + " textarea").prop("disabled", false);

    if (limp)
        LimpiarDiv(idDiv);
}

var opvisibleJud = true;

function ActivarBackspace(div) {
    $("div#" + div).find("input[type=text], input[type=search], textarea").on("keydown", function (e) {
        e.stopPropagation();
        if (e != null) {
            var code = e.keyCode || e.which;
            return true;
        }
    });
}

function ActivarDesactivarElemento(elem, est, limp) {
    $(elem).prop("disabled", est);
    $(elem).prop("readonly", est);

    if ($(elem).prop("type") == "radio" || $(elem).prop("type") == "checkbox" || $(elem).prop("type") == "check") {
        if (est)
            $(elem).siblings("i").addClass("txt-default");
        else
            $(elem).siblings("i").removeClass("txt-default");
    }

    if (limp) {
        if ($(elem).is("input")) {
            if ($(elem).prop("type") == "text")
                $(elem).val('');
            if ($(elem).prop("type") == "check")
                $(elem).attr('checked', false);
            if ($(elem).prop("type") == "checkbox")
                $(elem).attr('checked', false);
            if ($(elem).prop("type") == "radio")
                $(elem).attr('checked', false);
        }
        if ($(elem).is("textarea"))
            $(elem).val('');
        if ($(elem).is("select")) {
            $(elem).children().removeAttr("selected");
            $(elem).attr("selected", "selected");
        }
    }
}

function CargarGaleriaEffect() {
    $('.fancybox').fancybox({
        openEffect: 'none',
        closeEffect: 'none'
    });
}

function CallSpinner() {
    spinner();
}

function CallValidate() {
    validateInput();
}

/*function ValidarJqGrid(IdGrid, Col) {
    var gridData = $('#' + IdGrid).jqGrid('getRowData');
    var NumRows = $('#' + IdGrid).jqGrid('getGridParam', 'records');
    var b = false;
    if (NumRows > 0) {
        for (var j = 0; j < NumRows; j++) {
            var val = gridData[j].Col;
            if (val.trim() != '')
                b = true; 
        }
        return b;
    }
}*/

/*
 * Habilitar o Deshabilitar CheckBox y Radios
 * rc: El check o radio a habilitar o deshabilitar
 * est: es el estado si es "true" es para habilitar y si es "false" es para dehabilitarlo)
 */
function fHabDesRadioandCheck(rc, est) {
    $(rc).closest("i").remove();
    if (est) {
        $(rc).removeProp("disabled");
    }
}

function padL(number, length) { var str = '' + number; while (str.length < length) { str = '0' + str; } return str; }

function parseBoolean(string) { switch (String(string).toLowerCase()) { case "true": case "1": case "yes": case "y": return true; case "false": case "0": case "no": case "n": return false; default: return undefined; } }
/***    ACCESO VARIABLE SESION     ***/
var webMethod_result;
function WebMethod_OnError(result, response) {
    //debugger
}

function WebMethod_OnSuccess(result, response) {
    //Dummy function for page method ajax call
    var oResult = (typeof result.d) == 'string' ? eval('(' + result.d + ')') : result.d;
    if (oResult != null) {
        if (oResult.Resultado == 'OK') {
            webMethod_result = oResult.Mensaje;
        }
        else {
            if (oResult.Mensaje != "") {
                MensajeLogin(oResult.Mensaje, true);
            }
            else {
                showMensaje("La sesión ha finalizado, vuelva a ingresar a la aplicación", true);
                exitApp();
            }
        }
    }
    else {
        showMensaje("Ocurrio un error en la sesión, vuelva a ingresar a la aplicación", true);
        exitApp();
    }
}

function CallWebMethod(MethodName, ObjParams, isAsync, OnSuccessHandler, OnErrorHandler) {
    try {
        //Set the callback methods for success and error
        if (OnSuccessHandler == undefined || typeof (OnSuccessHandler) == "undefined") {
            OnSuccessHandler = WebMethod_OnSuccess
        }

        if (OnErrorHandler == undefined || typeof (OnErrorHandler) == "undefined") {
            OnErrorHandler = WebMethod_OnError
        }
        var serializedParams = "";
        serializedParams = JSON.stringify(ObjParams);
        return $.ajax(
            {
                type: "POST",
                async: isAsync,
                url: "Services/Login/wsLogin.asmx/" + MethodName,
                contentType: "application/json; charset=utf-8",
                data: serializedParams,
                dataType: "json",
                success: OnSuccessHandler,
                error: OnErrorHandler
            });
    }
    catch (e) {
        //suppress error
    }
    return;
}

function GetSessionValue(SessionName, OnSuccessHandler, OnErrorHandler) {
    //set web method parameters; should be same as parameter name else the web 
    //method wont be called 
    var methodParams = new Object();
    methodParams.Name = SessionName;
    CallWebMethod("GetSessionValue", methodParams, false, OnSuccessHandler, OnErrorHandler);
    //get the response 
    return webMethod_result;
}

function showMensaje(mContent, mType) {
    var cssType = 'alert-success';
    if (mType == "success" || mType == false)
        cssType = "alert-success";
    else if (mType == "error" || mType == true)
        cssType = "alert-danger";
    else if (mType == "info")
        cssType = "alert-info";
    else if (mType == "warning")
        cssType = "alert-warning";
    var divMsg = $("#alertmsg");
    if (divMsg.length) {
        divMsg.find("#closemsg").unbind("click");
        divMsg.empty();
        divMsg.removeClass("alert-success alert-danger alert-info alert-warning");
    }
    else {
        $('body').append('<div id="alertmsg" class="alert" style="position:absolute; width: 600px; z-index: 99999;  display: none;"></div>');
        divMsg = $("#alertmsg");
    }
    divMsg.addClass(cssType);
    divMsg.append('<a href="#" id="closemsg" class="close" style="margin-left: 5px;" data-dismiss="alert">&times;</a><strong>' + mContent + '</strong>');
    divMsg.center();
    divMsg.show();
    divMsg.find("#closemsg").click(function () { $("#alertmsg").alert(); });

    window.setTimeout(function () {
        $('#alertmsg').stop(false, true).fadeOut('fast');
    }, 1500);
}

/** Funciones Generales **/

function GetFechaActual() {
    var c = new Date(); var d = padL(c.getDate(), 2); var m = padL((c.getMonth()), 2); var a = (c.getFullYear());
    return $.datepicker.formatDate($.datepicker.regional[LANG].dateFormat, new Date(a, m, d));
}

function GetHoraActual() {
    var f = new Date();
    var h = padL(f.getHours(), 2) + ':' + padL(f.getMinutes(), 2);
    if (CheckEsHora(h)) return h;
    else return '00:00';
}

function SetFormatFecha(d) {
    return padL(d.getDay(), 2) + "/" + padL(d.getMonth(), 2) + "/" + padL(d.getFullYear(), 2);
}

function SetFormatHora(h) {
    return padL(h.getHours(), 2) + ":" + padL(h.getMinutes(), 2);
}

function OnlyNumeros() { if ((window.event.keyCode < 48) || (window.event.keyCode > 57)) { window.event.returnValue = 0; } }

function CheckEsHora(h) {
    var error = true;
    var formatoIdioma = "00:00";
    if (h.length > 5 || h.length != 5) {
        error = false;
    } else {
        a = h.charAt(0); b = h.charAt(1); c = h.charAt(3);
        if (a >= 2 && b > 3) {
            h = formatoIdioma;
            error = false;
        } else {
            if (c > 5) {
                h = formatoIdioma;
                error = false;
            }
        }
    }
    return error;
}

function htmlDecode(s) {
    var c, m, d = s;
    if (!d)
        return "";
    if ((d === null) || d.length == 0 || /^\s+$/.test(d))
        return "";

    var arr1 = new Array();
    var arr2 = new Array();
    arr1 = ['&nbsp;', '&iexcl;', '&cent;', '&pound;', '&curren;', '&yen;', '&brvbar;', '&sect;', '&uml;', '&copy;', '&ordf;', '&laquo;', '&not;', '&shy;', '&reg;', '&macr;', '&deg;', '&plusmn;', '&sup2;', '&sup3;', '&acute;', '&micro;', '&para;', '&middot;', '&cedil;', '&sup1;', '&ordm;', '&raquo;', '&frac14;', '&frac12;', '&frac34;', '&iquest;', '&Agrave;', '&Aacute;', '&Acirc;', '&Atilde;', '&Auml;', '&Aring;', '&AElig;', '&Ccedil;', '&Egrave;', '&Eacute;', '&Ecirc;', '&Euml;', '&Igrave;', '&Iacute;', '&Icirc;', '&Iuml;', '&ETH;', '&Ntilde;', '&Ograve;', '&Oacute;', '&Ocirc;', '&Otilde;', '&Ouml;', '&times;', '&Oslash;', '&Ugrave;', '&Uacute;', '&Ucirc;', '&Uuml;', '&Yacute;', '&THORN;', '&szlig;', '&agrave;', '&aacute;', '&acirc;', '&atilde;', '&auml;', '&aring;', '&aelig;', '&ccedil;', '&egrave;', '&eacute;', '&ecirc;', '&euml;', '&igrave;', '&iacute;', '&icirc;', '&iuml;', '&eth;', '&ntilde;', '&ograve;', '&oacute;', '&ocirc;', '&otilde;', '&ouml;', '&divide;', '&oslash;', '&ugrave;', '&uacute;', '&ucirc;', '&uuml;', '&yacute;', '&thorn;', '&yuml;', '&quot;', '&amp;', '&lt;', '&gt;', '&OElig;', '&oelig;', '&Scaron;', '&scaron;', '&Yuml;', '&circ;', '&tilde;', '&ensp;', '&emsp;', '&thinsp;', '&zwnj;', '&zwj;', '&lrm;', '&rlm;', '&ndash;', '&mdash;', '&lsquo;', '&rsquo;', '&sbquo;', '&ldquo;', '&rdquo;', '&bdquo;', '&dagger;', '&Dagger;', '&permil;', '&lsaquo;', '&rsaquo;', '&euro;', '&fnof;', '&Alpha;', '&Beta;', '&Gamma;', '&Delta;', '&Epsilon;', '&Zeta;', '&Eta;', '&Theta;', '&Iota;', '&Kappa;', '&Lambda;', '&Mu;', '&Nu;', '&Xi;', '&Omicron;', '&Pi;', '&Rho;', '&Sigma;', '&Tau;', '&Upsilon;', '&Phi;', '&Chi;', '&Psi;', '&Omega;', '&alpha;', '&beta;', '&gamma;', '&delta;', '&epsilon;', '&zeta;', '&eta;', '&theta;', '&iota;', '&kappa;', '&lambda;', '&mu;', '&nu;', '&xi;', '&omicron;', '&pi;', '&rho;', '&sigmaf;', '&sigma;', '&tau;', '&upsilon;', '&phi;', '&chi;', '&psi;', '&omega;', '&thetasym;', '&upsih;', '&piv;', '&bull;', '&hellip;', '&prime;', '&Prime;', '&oline;', '&frasl;', '&weierp;', '&image;', '&real;', '&trade;', '&alefsym;', '&larr;', '&uarr;', '&rarr;', '&darr;', '&harr;', '&crarr;', '&lArr;', '&uArr;', '&rArr;', '&dArr;', '&hArr;', '&forall;', '&part;', '&exist;', '&empty;', '&nabla;', '&isin;', '&notin;', '&ni;', '&prod;', '&sum;', '&minus;', '&lowast;', '&radic;', '&prop;', '&infin;', '&ang;', '&and;', '&or;', '&cap;', '&cup;', '&int;', '&there4;', '&sim;', '&cong;', '&asymp;', '&ne;', '&equiv;', '&le;', '&ge;', '&sub;', '&sup;', '&nsub;', '&sube;', '&supe;', '&oplus;', '&otimes;', '&perp;', '&sdot;', '&lceil;', '&rceil;', '&lfloor;', '&rfloor;', '&lang;', '&rang;', '&loz;', '&spades;', '&clubs;', '&hearts;', '&diams;'];
    arr2 = ['&#160;', '&#161;', '&#162;', '&#163;', '&#164;', '&#165;', '&#166;', '&#167;', '&#168;', '&#169;', '&#170;', '&#171;', '&#172;', '&#173;', '&#174;', '&#175;', '&#176;', '&#177;', '&#178;', '&#179;', '&#180;', '&#181;', '&#182;', '&#183;', '&#184;', '&#185;', '&#186;', '&#187;', '&#188;', '&#189;', '&#190;', '&#191;', '&#192;', '&#193;', '&#194;', '&#195;', '&#196;', '&#197;', '&#198;', '&#199;', '&#200;', '&#201;', '&#202;', '&#203;', '&#204;', '&#205;', '&#206;', '&#207;', '&#208;', '&#209;', '&#210;', '&#211;', '&#212;', '&#213;', '&#214;', '&#215;', '&#216;', '&#217;', '&#218;', '&#219;', '&#220;', '&#221;', '&#222;', '&#223;', '&#224;', '&#225;', '&#226;', '&#227;', '&#228;', '&#229;', '&#230;', '&#231;', '&#232;', '&#233;', '&#234;', '&#235;', '&#236;', '&#237;', '&#238;', '&#239;', '&#240;', '&#241;', '&#242;', '&#243;', '&#244;', '&#245;', '&#246;', '&#247;', '&#248;', '&#249;', '&#250;', '&#251;', '&#252;', '&#253;', '&#254;', '&#255;', '&#34;', '&#38;', '&#60;', '&#62;', '&#338;', '&#339;', '&#352;', '&#353;', '&#376;', '&#710;', '&#732;', '&#8194;', '&#8195;', '&#8201;', '&#8204;', '&#8205;', '&#8206;', '&#8207;', '&#8211;', '&#8212;', '&#8216;', '&#8217;', '&#8218;', '&#8220;', '&#8221;', '&#8222;', '&#8224;', '&#8225;', '&#8240;', '&#8249;', '&#8250;', '&#8364;', '&#402;', '&#913;', '&#914;', '&#915;', '&#916;', '&#917;', '&#918;', '&#919;', '&#920;', '&#921;', '&#922;', '&#923;', '&#924;', '&#925;', '&#926;', '&#927;', '&#928;', '&#929;', '&#931;', '&#932;', '&#933;', '&#934;', '&#935;', '&#936;', '&#937;', '&#945;', '&#946;', '&#947;', '&#948;', '&#949;', '&#950;', '&#951;', '&#952;', '&#953;', '&#954;', '&#955;', '&#956;', '&#957;', '&#958;', '&#959;', '&#960;', '&#961;', '&#962;', '&#963;', '&#964;', '&#965;', '&#966;', '&#967;', '&#968;', '&#969;', '&#977;', '&#978;', '&#982;', '&#8226;', '&#8230;', '&#8242;', '&#8243;', '&#8254;', '&#8260;', '&#8472;', '&#8465;', '&#8476;', '&#8482;', '&#8501;', '&#8592;', '&#8593;', '&#8594;', '&#8595;', '&#8596;', '&#8629;', '&#8656;', '&#8657;', '&#8658;', '&#8659;', '&#8660;', '&#8704;', '&#8706;', '&#8707;', '&#8709;', '&#8711;', '&#8712;', '&#8713;', '&#8715;', '&#8719;', '&#8721;', '&#8722;', '&#8727;', '&#8730;', '&#8733;', '&#8734;', '&#8736;', '&#8743;', '&#8744;', '&#8745;', '&#8746;', '&#8747;', '&#8756;', '&#8764;', '&#8773;', '&#8776;', '&#8800;', '&#8801;', '&#8804;', '&#8805;', '&#8834;', '&#8835;', '&#8836;', '&#8838;', '&#8839;', '&#8853;', '&#8855;', '&#8869;', '&#8901;', '&#8968;', '&#8969;', '&#8970;', '&#8971;', '&#9001;', '&#9002;', '&#9674;', '&#9824;', '&#9827;', '&#9829;', '&#9830;'];

    var re;
    if (arr1 && arr2) {
        //ShowDebug("in swapArrayVals arr1.length = " + arr1.length + " arr2.length = " + arr2.length)
        // array lengths must match
        if (arr1.length == arr2.length) {
            for (var x = 0, i = arr1.length; x < i; x++) {
                re = new RegExp(arr1[x], 'g');
                d = d.replace(re, arr2[x]); //swap arr1 item with matching item from arr2	
            }
        }
    }
    // look for numerical entities &#34;
    var arr = d.match(/&#[0-9]{1,5};/g);
    // if no matches found in string then skip
    if (arr != null) {
        for (var x = 0; x < arr.length; x++) {
            m = arr[x];
            c = m.substring(2, m.length - 1); //get numeric part which is refernce to unicode character
            // if its a valid number we can decode
            if (c >= -32768 && c <= 65535) {
                // decode every single match within string
                d = d.replace(m, String.fromCharCode(c));
            } else {
                d = d.replace(m, ""); //invalid so replace with nada
            }
        }
    }
    return d;
}
//---------------------------------------------//
function formatDate(d) {
    d = $.trim(d);
    if (d.length == 0)
        return null;

    if (d.split("/").length == 3) {
        var dia = $.trim(d.split("/")[0]);
        var mes = $.trim(d.split("/")[1]);
        var anio = $.trim(d.split("/")[2]);
        if (dia.length == 0) return null;
        if (mes.length == 0) return null;
        if (anio.length == 0) return null;

        return mes + "/" + dia + "/" + anio;
    } else {
        return null;
    }
}

function GetDateHTML(d) {
    // Today date time which will used to set as default date.
    var DateHTML = '';
    if (d instanceof Date) {
        DateHTML = ("0" + d.getDate()).slice(-2) + "/" +
                    ("0" + (d.getMonth() + 1)).slice(-2) + "/" +
                    d.getFullYear();
        /* + " " +
        ("0" + d.getHours()).slice(-2) + ":" +
        ("0" + d.getMinutes()).slice(-2);
        */
    }
    return DateHTML;
}

function SetFechaInput(o, f) {
    if (typeof f != 'undefined' && f != null) {
        f = f.replace(/[/]/g, '');
    }
    else
        return false;

    if (f) {
        try {
            d = eval("new " + f);
            if (o.length)
                o.val(GetDateHTML(d));
            return true;
        }
        catch (er) {
            return false;
        }
    }
}

function fEmptyOptionSelect(s) {
    s.append($("<option>").val("").text("---Seleccione---"));
}

function fLoadSelect(url, select, param, id, description, valueSelected, labelSelected, addVoidOption) {
    var k = new Date().getTime();
    var options = '';
    select.children().remove();

    $.ajax({
        type: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        data: param,
        success: function (data) {

            if (addVoidOption) {
                fEmptyOptionSelect(select);
            }
            if (typeof valueSelected != "undefined" && valueSelected != "" && typeof valueSelected != 'undefined' && valueSelected != "") {
                var optionExists = false;
                if (data.d != null && data.d.length > 0) {
                    for (var i = 0; i < data.d.length; i++) {
                        if (data.d[i].Value == valueSelected) {
                            optionExists = true;
                            select.append($("<option>").val(eval(("data.d[i]." + id))).text(eval(("data.d[i]." + description))));
                        } else {
                            select.append($("<option>").val(eval(("data.d[i]." + id))).text(eval(("data.d[i]." + description))));
                        }
                    }
                }

            } else {
                if (data.d != null && data.d.length > 0) {
                    for (var i = 0; i < data.d.length; i++) {
                        select.append($("<option>").val(eval(("data.d[i]." + id))).text(eval(("data.d[i]." + description))));
                    }
                }
            }

            if (select.hasClass("chosen")) {
                select.trigger('chosen:updated');
            }
        },
        error: function () {
            showMensaje("Error al intentar cargar el combo", true);
            fEmptyOptionSelect(select)
        }
    });
    //select.trigger('change');
}