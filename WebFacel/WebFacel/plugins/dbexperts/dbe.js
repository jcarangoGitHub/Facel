
//Metodo para navegar a otra vista
function gotoView(url) {
    window.location.href = url;
};

//Intenta descomponer una excepcion para mostrar mensaje de error con titulo y subtitulo
function handleError(ajaxError) {
    var posIni = 0;
    var posFin = 0;
    var textSearch = "<br><br>";
    var msg = ajaxError.responseText;
    var errorMsg = errorObject(msg);

    if (ajaxError.status == "0") {
        errorMsg.message = "No hay conexión con el servidor";
    }
    if (ajaxError.status == "404") {
        posIni = msg.indexOf(textSearch);
        posFin = msg.lastIndexOf(textSearch);
        errorMsg.title = "No se encuentra el recurso";
        errorMsg.subtitle = "Error de servidor en la aplicación";
        errorMsg.message = "El recurso solicitado (o una de sus dependencias) se puede haber quitado, haber cambiado de nombre o no estar disponible temporalmente.  ";

        if ((posIni > 0) && (posFin > 0)) {
            msg = msg.substring(posIni, posFin).replace("<b>", "").replace("</b>", "");
            errorMsg.message = errorMsg.message + msg.replace(textSearch, "");
        }
    }

    showError(errorMsg.title, errorMsg.subtitle, errorMsg.message);
}; //function handleError(errorObject)

//Muestra ventana de Error en forma modal
function showError(title, message) {
    swal({
        title: title,
        text: message,
        type: "error"
    });
};

function errorObject(message) {
    var errorMsg = {
        title: "Mensaje de Error",
        subtitle: "Se ha producido un error en la aplicación",
        message: message
    };

    return errorMsg;
} //function errorObject()

//Metodo para establecer la propiedad readonly de un control
function setReadOnly(nombreControl, readonly) {
    var control = $("#" + nombreControl);

    if (readonly) {
        control.attr("readonly", "readonly");
    } else {
        control.removeAttr("readonly");
    }
}

//Metodo para establecer la propiedad readonly de uno o varios controles
function ControlesReadOnly(nombreControles, readonly) {
    var arreglo = nombreControles.split(',');

    if (arreglo.length > 0) {
        for (var i = 0; i < arreglo.length; i++) {
            setReadOnly(arreglo[i].trim(), readonly);
        }
    }
}

//Metodo para habilitar o desabilitar un control
function HabilitarControl(nombreControl, enabled) {
    var control = $("#" + nombreControl);

    if (enabled) {
        control.removeAttr('disabled');
    } else {
        control.attr("disabled", "disabled");
    }
}

//Metodo para habilitar o desabilitar uno o varios controles
function HabilitarControles(nombreControles, enabled) {
    var arreglo = nombreControles.split(',');

    if (arreglo.length > 0) {
        for (var i = 0; i < arreglo.length; i++) {
            HabilitarControl(arreglo[i].trim(), enabled);
        }
    }
}

// Valida la obligatoriedad de un control especificado por el nombre
function requiredValidate(controlName) {
    var errorMsg = "";
    var controlObj = $("#" + controlName);
    var input = controlObj.val();
    var req = controlObj.attr("required");

    if ((input == "") && (req == "required")) {
        var label = $('label[for="' + controlName + '"]')[0];
        errorMsg = "'" + label.innerText + "' es un valor requerido";
    }

    return errorMsg;
};

// Valida la obligatoriedad de varios controles especificados por el nombre
function requiredValidateControls(controlsName) {
    var errorMsg = "";
    var arreglo = controlsName.split(',');

    if (arreglo.length > 0) {
        for (var i = 0; i < arreglo.length; i++) {
            errorMsg = requiredValidate(arreglo[i].trim());
            if (errorMsg.length > 0) {
                break;
            }
        }
    }

    return errorMsg;
};

//Crea celda td con el contenido especificado
function new_td(value) {
    var td = '<td>' + value + '</td>';

    return td;
};

//Realiza una depuracion de las entrads del usuario
function inputDebug(text) {
    text = text.replace(/</g, "&lt;");
    text = text.replace(/>/g, "&gt;");

    return text;
};

function getObjUser() {
    objUser = localStorage.getItem('objUser');

    if (objUser) {
        objUser = JSON.parse(objUser);
        $('#lblUserName').text(objUser.UserName);
    }
};

function openLogin() {
    $("#lblError").text("");
    $("#password_txt").val("");
    $('#loginModal').modal({
        show: 'false'
    });
};

function login() {
    var email = $('#email_txt').val().trim();
    var pwd = $('#password_txt').val().trim();
    var urlRoot = $('#txtUrlRoot').val().trim();

    if ((email.length > 0) && (pwd.length > 0)) {
        var titleError = "Error en el Ingreso";
        var wsCode = {};

        $("#divProcessing").show();
        wsCode.url = urlRoot + "/Code/Login";
        wsCode.type = "POST";
        wsCode.data = JSON.stringify({ user: email, password: pwd });
        wsCode.datatype = "json";
        wsCode.contentType = "application/json";
        
        wsCode.success = function (rpta) {
            var result = JSON.parse(rpta);
            var row = result[0];
            var errorMsg = row.Error;            

            $("#divProcessing").hide();
            if (errorMsg.length > 0) {   
                $('#lblUserName').text('noname');
                $("#lblError").text(errorMsg);
            } else {
                $('#lblUserName').text(row.UserName);                
                localStorage.setItem('objUser', JSON.stringify(row));
                $("#loginModal").modal('hide');
            }
        };

        wsCode.error = function (request, status, error) {
            $("#divProcessing").hide();
            showError(titleError, error);
        };

        $.ajax(wsCode);
    }
};

function logout() {
    objUser = {};
    localStorage.removeItem('objUser');
    $('#lblUserName').text('noname');
};

//Funcion que captura la fecha-hora del servidor
function getServerTime() {
    var urlRoot = $("#txtUrlRoot").val();
    var wsFechaHora = {};
    var unMinuto = 60000;
    wsFechaHora.url = urlRoot + "/Home/ObtenerFechaHora";
    wsFechaHora.type = "POST";
    wsFechaHora.data = JSON.stringify({});
    wsFechaHora.datatype = "json";
    wsFechaHora.contentType = "application/json";

    wsFechaHora.success = function (rpta) {
        if (rpta.length > 0) {
            var lblFechaHora = $("#lblFechaHora");
            lblFechaHora.text(rpta);
        }

        setTimeout(getServerTime, unMinuto);
    };

    wsFechaHora.error = function (request, status, error) {
        setTimeout(getServerTime, unMinuto);
    };

    $.ajax(wsFechaHora);
};
