$(function () {
    $("#idpersona").val('0');
    GetEspecie();
});
function SaveEspecie() {
    var s = '';
    if ($("#txtdescripcion").val() == '') s = 'Debe de ingresar la descripción\n';
    if ($("#selestado").val() == '') s += 'Debe de seleccionar un estado\n';
    if (s != '') { alert(s); return false; }
    var cli = {};
    cli.IdEspecie = $("#idespecie").val();
    cli.Descripcion = $("#txtdescripcion").val();
    cli.Estado = $("#selestado").val();
    $.ajax({
        url: "/General/Save",
        data: cli,
        type: "POST"
    }).done(function (data) {
        if (data) {
            $('#frmespecie').trigger("reset");
            GetEspecie();
            $("#idespecie").val('0');
            alert('Esto es un exito');
        }
    }).fail(function (xhr, status, error) {
        var err = status + ", " + error;
        alert(err);
    });

    return false;
}

function GetEspecie() {
    $("#divtabla").attr('style', 'display:none');
    $("#tblespecie tbody").empty();
    $.ajax({
        url: "/General/GetEspecie",
        data: {},
        dataType: "json",
        type: "GET",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        if (typeof (data) === 'object' && data.length > 0) {
            $("#divtabla").attr('style', 'display:block');
            var estato = '';
            $.each(data, function (i, el) {
                estato = el.Estado == 1 ? '<td class="process">Activo</td>' : '<td class="denied">Deshabilitado</td>';
                $("#tblespecie tbody").append('<tr><td>' + el.IdEspecie + '</td><td>' +
                    el.Descripcion + '</td>' +
                    estato + '<td><span onclick="GetEspecieid(' + el.IdEspecie + ')" style="cursor:pointer">Editar</span</td>' +
                    '<td><span onclick="GetEspecieRaza(' + el.IdEspecie + ')" style="cursor:pointer">Add</span</td>'+
                    '</tr>'
                );
            });
        }
    }).fail(function (xhr, status, error) {
        var err = status + ", " + error;
        alert(err);
    });
}

function GetEspecieid(id) {
    if (id != undefined && id != '') {
        $.ajax({
            url: "/General/GetEspecieId",
            data: { idespecie: id },
            dataType: "json",
            type: "GET",
            contentType: "application/json; charset=utf-8"
        }).done(function (cli) {
            if (cli) {
                $("#idespecie").val(cli.IdEspecie);
                $("#txtdescripcion").val(cli.Descripcion);
                $("#selestado").val(cli.Estado);
            }
        }).fail(function (xhr, status, error) {
            var err = status + ", " + error;
            alert(err);
        });
    }
}

function GetEspecieRaza(id) {
    if (id != undefined && id != '') {
        $("#idespecier").val(id);
        RazaReset();
        $("#exampleModal").modal('show');
        $("#divtablar").attr('style', 'display:none');
        $("#tblraza tbody").empty();
        $.ajax({
            url: "/General/GetEspecieRaza",
            data: { idespecie: id },
            dataType: "json",
            type: "GET",
            contentType: "application/json; charset=utf-8"
        }).done(function (data) {
            if (typeof(data)==='object' && data.length>0) {
                $("#divtablar").attr('style', 'display:block');
                var estato = '';
                $.each(data, function (i, el) {
                    estato = el.Estado == 1 ? '<td class="process">Activo</td>' : '<td class="denied">Deshabilitado</td>';
                    $("#tblraza tbody").append('<tr><td>' + el.IdRaza + '</td><td>' +
                        el.Descripcion + '</td>' +
                        estato + '<td><span onclick="GetRazaid(' + el.IdRaza + ')" style="cursor:pointer">Editar</span</td>' +
                        '</tr>'
                    );
                });
            }
        }).fail(function (xhr, status, error) {
            var err = status + ", " + error;
            alert(err);
        });
    }
}
function SaveRaza() {
    var s = '';
    if ($("#txtdescripcionr").val() == '') s = 'Debe de ingresar la descripción\n';
    if ($("#selestador").val() == '') s += 'Debe de seleccionar un estado\n';
    if (s != '') { alert(s); return false; }
    var cli = {};
    cli.IdEspecie = $("#idespecier").val();
    cli.IdRaza = $("#idraza").val();
    cli.Descripcion = $("#txtdescripcionr").val();
    cli.Estado = $("#selestador").val();
    $.ajax({
        url: "/General/SaveRaza",
        data: cli,
        type: "POST"
    }).done(function (data) {
        if (data) {
            alert('Esto es un exito');
            //$("#exampleModal").modal('hide');
            GetEspecieRaza($("#idespecier").val());
            $("#idraza").val('0');
        }
    }).fail(function (xhr, status, error) {
        var err = status + ", " + error;
        alert(err);
    });

    return false;
}

function RazaReset() {
    var id = $("#idespecier").val();
    $('#frmraza').trigger("reset");
    $("#idespecier").val(id);
}
function GetRazaid(id) {
    if (id != undefined && id != '') {
        $.ajax({
            url: "/General/GetRazaId",
            data: { idraza: id },
            dataType: "json",
            type: "GET",
            contentType: "application/json; charset=utf-8"
        }).done(function (cli) {
            if (cli) {
                $("#idraza").val(cli.IdRaza);
                $("#idespecier").val(cli.IdEspecie);
                $("#txtdescripcionr").val(cli.Descripcion);
                $("#selestador").val(cli.Estado);
            }
        }).fail(function (xhr, status, error) {
            var err = status + ", " + error;
            alert(err);
        });
    }
}