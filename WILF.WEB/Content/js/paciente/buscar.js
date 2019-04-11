function Buscar() {
    $("#divtabla").attr('style', 'display:none');
    $("#tblpaciente tbody").empty();
    $.ajax({
        url: "/Paciente/Buscar",
        data: { dni: $("#txtdni").val(), nombre: $("#txtnombre").val()},
        dataType: "json",
        type: "GET",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        if (typeof (data) === 'object' && data.length > 0) {
            $("#divtabla").attr('style', 'display:block');
            var estato = '';
            $.each(data, function (i, el) {
                $("#tblpaciente tbody").append('<tr><td>' + el.Dni + '</td><td>' +
                    el.Nombre + '</td><td>' +
                    el.Direccion + '</td><td>' +
                    el.Telefono + '</td><td>' +
                    el.Email + '</td>' +
                    '<td><span onclick="AddHistorial(' + el.Estado + ')" style="cursor:pointer" class="btneditar" >Ver historial Clínico</span</td>' +
                    '</tr>'
                );
            });
        }
    }).fail(function (xhr, status, error) {
        var err = status + ", " + error;
        alert(err);
        });
    return false;
}

function AddHistorial(id) {
    if (id != undefined && id != '') {
        $("#idpaciente").val(id);
        RazaHisto();
        $("#idhistorial").val(0);
        $("#exampleModal").modal('show');
        $("#divtablah").attr('style', 'display:none');
        $("#tblhisto tbody").empty();
        $.ajax({
            url: "/Paciente/GetHistoPaciId",
            data: { idpaciente: id },
            dataType: "json",
            type: "GET",
            contentType: "application/json; charset=utf-8"
        }).done(function (data) {
            if (typeof (data) === 'object' && data.length > 0) {
                $("#divtablah").attr('style', 'display:block');
                var estato = '';
                $.each(data, function (i, el) {
                    estato = el.Estado == 1 ? '<td class="process">Activo</td>' : '<td class="denied">Deshabilitado</td>';
                    $("#tblhisto tbody").append('<tr><td>' + el.Tratamiento + '</td><td>' +
                        el.Detalle + '</td>' +
                        estato + '<td><span onclick="Edit(' + el.IdHistoria + ')" style="cursor:pointer">Editar</span</td>' +
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

function Save() {
    var s = '';
    if ($("#txttrata").val() == '') s = 'Debe de ingresar el tratamiento\n';
    if ($("#ardetalle").val() == '') s += 'Debe de indresar el detalle\n';
    if (s != '') { alert(s); return false; }
    var cli = {};
    cli.IdPaciente = $("#idpaciente").val();
    cli.IdHistoria = $("#idhistorial").val();
    cli.Tratamiento = $("#txttrata").val();
    cli.Detalle = $("#ardetalle").val();
    cli.Estado = $("#selestado").val();
    $.ajax({
        url: "/Paciente/SaveHisto",
        data: cli,
        type: "POST"
    }).done(function (data) {
        if (data) {
            alert('Se agregó correctamente el tratamiento');
            AddHistorial($("#idpaciente").val());
        }
    }).fail(function (xhr, status, error) {
        var err = status + ", " + error;
        alert(err);
    });

    return false;
}

function RazaHisto() {
    var id = $("#idpaciente").val();
    $('#frmhisto').trigger("reset");
    $("#idpaciente").val(id);
}

function Edit(id) {
    if (id != undefined && id != '') {
        $.ajax({
            url: "/Paciente/GetHistoId",
            data: { idhistorial: id },
            dataType: "json",
            type: "GET",
            contentType: "application/json; charset=utf-8"
        }).done(function (cli) {
            if (cli) {
                $("#idhistorial").val(id);
                $("#txttrata").val(cli.Tratamiento);
                $("#ardetalle").val(cli.Detalle);
                $("#selestado").val(cli.Estado);
            }
        }).fail(function (xhr, status, error) {
            var err = status + ", " + error;
            alert(err);
        });
    }
}