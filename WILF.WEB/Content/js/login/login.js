$(function () { });
function Logeo() {
    var s = '';
    if ($("#txtuser").val() == "") s += "Debe de ingresar un usuario\n";
    if ($("#txtpassword").val() == "") s += "Debe de ingresar un password\n";
    if (s != "") { alert(s); }
    else {
        $.getJSON("/Home/ValidaUsuario", { user: $("#txtuser").val(), pass: $("#txtpassword").val()}
            , function (data) {
                if (typeof(data) === 'object') {
                    if (data.IdPerfil == 1) window.location.href = "/Cliente/index";
                    else if (data.IdPerfil == 2) window.location.href = "/Cliente/Cliente";
                }
                else {
                    alert(data);
                }
                data = undefined;
            }).fail(function (jqxhr, textStatus, error) {
                var err = textStatus + ", " + error;
                alert(err);
            });
    }
    return false;
}