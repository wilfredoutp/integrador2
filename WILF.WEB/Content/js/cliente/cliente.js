$(function () {
    $("#idpersona").val('0');
    GetCliente();
    GetEspecie();
    $("#file-input").on('change', uploadcliente);
    $("#selespecie").on('change', function () {
        GetRaza(this.value);
    });
    $("#fileinputr").on('change', uploadpaciente);

});
function save() {
    var s = '';
    if ($("#txtdni").val() == '') s = 'Debe de ingresar número de DNI\n';
    if ($("#txtnombre").val() == '') s += 'Debe de ingresar un nombre\n';
    if ($("#txtappaterno").val() == '') s += 'Debe de ingresar un apellido paterno\n';
    if ($("#txtemail").val() == '') s += 'Debe de ingresar el correo electronico\n';
    if (s != '') { alert(s); return false; }
    var cli = {};
    cli.IdPersona = $("#idpersona").val();
    cli.Dni = $("#txtdni").val();
    cli.Nombre = $("#txtnombre").val();
    cli.ApPaterno = $("#txtappaterno").val();
    cli.ApMaterno = $("#txtapmaterno").val();
    cli.Direccion = $("#txtdireccion").val();
    cli.Telefono = $("#txttelefono").val();
    cli.Email = $("#txtemail").val();
    cli.Estado = $("#selestado").val();
    cli.RutaImagen = $("#txtruta").val();
    $.ajax({
        url: "/Cliente/Save",
        data: cli,
        type: "POST"
    }).done(function (data) {
        if (data) {
            $('#frmpersona').trigger("reset");
            GetCliente();
            $("#idpersona").val('0');
            alert('Se agregó correctamente');
        }
    }).fail(function (xhr, status, error) {
        var err = status + ", " + error;
        alert(err);
    });

    return false;
}

function GetCliente() {
    $("#divtabla").attr('style', 'display:none');
    $("#tblcliente tbody").empty();
    $.ajax({
        url: "/Cliente/GetCliente",
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
                $("#tblcliente tbody").append('<tr><td>' + el.Dni + '</td><td>' +
                    el.Nombre + '</td><td>' +
                    el.ApPaterno + '</td><td>' +
                    el.ApMaterno + '</td><td>' +
                    el.Direccion + '</td><td>' +
                    el.Telefono + '</td><td>' +
                    el.Email + '</td>' + estato + '<td><span  onclick="GetClienteid(' + el.IdPersona + ')" style="cursor:pointer">Editar</span></td>' +
                    '<td><span onclick="GetAdd(' + el.IdPersona + ')" style="cursor:pointer" class="regpaciente">Registrar Paciente</span></td>' +
                    '</tr>'
                );
            });
        }
    }).fail(function (xhr, status, error) {
        var err = status + ", " + error;
        alert(err);
    });
}

function GetClienteid(id) {
    if (id != undefined && id != '') {
        $.ajax({
            url: "/Cliente/GetClienteId",
            data: { idpersona: id },
            dataType: "json",
            type: "GET",
            contentType: "application/json; charset=utf-8"
        }).done(function (cli) {
            if (cli) {
                $("#txtdni").val(cli.Dni);
                $("#txtnombre").val(cli.Nombre);
                $("#txtappaterno").val(cli.ApPaterno);
                $("#txtapmaterno").val(cli.ApMaterno);
                $("#txtdireccion").val(cli.Direccion);
                $("#txttelefono").val(cli.Telefono);
                $("#txtemail").val(cli.Email);
                $("#txtruta").val(cli.RutaImagen);
                $("#selestado").val(cli.Estado);
            }
        }).fail(function (xhr, status, error) {
            var err = status + ", " + error;
            alert(err);
        });
    }
}

function uploadcliente() {
    var i = 0, len = this.files.length, img, reader, file;
    var formdata = false;
    if (window.FormData) {
        formdata = new FormData();
    }
    for (; i < len; i++) {
        file = this.files[i];
        if (!!file.type.match(/image.*/)) {
            if (window.FileReader) {
                reader = new FileReader();
                reader.onloadend = function (e) {
                    //showUploadedItem(e.target.result, file.fileName);
                };
                reader.readAsDataURL(file);
            }

            if (formdata) {
                formdata.append("image", file);
            }
            if (formdata) {
                jQuery.ajax({
                    url: "/Cliente/uploadcliente",
                    type: "POST",
                    data: formdata,
                    processData: false,
                    contentType: false
                }).done(function (data) {
                    if (typeof (data) === 'string') {
                        $("#txtruta").val(data);
                    }
                });
            }
        }
        else {
            alert('Not a vaild image!');
        }
    }
}

function GetAdd(id) {
    if (id != undefined && id != '') {
        $("#idpersonap").val(id);
        PacienteReset();
        $("#exampleModal").modal('show');
        $("#divpaciente").attr('style', 'display:none');
        $("#tblpaciente tbody").empty();
        $.ajax({
            url: "/Cliente/GetPacientePersonaId",
            data: { idpersona: id },
            dataType: "json",
            type: "GET",
            contentType: "application/json; charset=utf-8"
        }).done(function (data) {
            if (typeof (data) === 'object' && data.length > 0) {
                $("#divpaciente").attr('style', 'display:block');
                var estato = '';
                $.each(data, function (i, el) {
                    estato = el.Estado == 1 ? '<td class="process">Activo</td>' : '<td class="denied">Deshabilitado</td>';
                    $("#tblpaciente tbody").append('<tr><td>' + el.Nombre + '</td><td>' +
                        getdate(el.FecNacimiento) + '</td><td>' +
                        el.Edad + '</td>' +
                        estato + '<td><span onclick="EditPaciente(' + el.IdPaciente + ')" style="cursor:pointer" class="btneditar">Editar</span></td>' +
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
function EditPaciente(id) {
    if (id != undefined && id != '') {
        $.ajax({
            url: "/Cliente/GetPacienteId",
            data: { idpaciente: id },
            dataType: "json",
            type: "GET",
            contentType: "application/json; charset=utf-8"
        }).done(function (cli) {
            if (typeof (cli) === 'object') {
                $("#idpaciente").val(cli.IdPaciente);
                $("#selespecie").val(cli.IdEspecie);
                GetRaza(cli.IdEspecie);
                $("#selraza").val(cli.IdRaza);
                $("#txtnombrera").val(cli.Nombre);
                $("#txtnacimiento").val(getdate(cli.FecNacimiento));
                $("#txteda").val(cli.Edad);
                $("#txtrutap").val(cli.RutaImagen);
                $("#selestador").val(cli.Estado);
            }
        }).fail(function (xhr, status, error) {
            var err = status + ", " + error;
            alert(err);
        });
    }
}
function SavePaciente() {
    var s = '';
    if ($("#selespecie").val() == '') s = 'Debe de seleccionar una especie\n';
    if ($("#selraza").val() == '') s += 'Debe de seleccionar una raza\n';
    if ($("#txtnombrera").val() == '') s += 'Debe de ingresar un nombre\n';
    if ($("#txtnacimiento").val() == '') s += 'Debe de ingresar una fecha de nacimiento\n';
    if (s != '') { alert(s); return false; }
    var cli = {};
    cli.IdPaciente = $("#idpaciente").val();
    cli.IdPersona = $("#idpersonap").val();
    cli.IdRaza = $("#selraza").val();
    cli.Nombre = $("#txtnombrera").val();
    cli.FecNacimiento = $("#txtnacimiento").val();
    cli.Edad = $("#txteda").val();
    cli.RutaImagen = $("#txtrutap").val();
    cli.Estado = $("#selestador").val();
    $.ajax({
        url: "/Cliente/SavePaciente",
        data: cli,
        type: "POST"
    }).done(function (data) {
        if (data) {
            alert('Se agregó correctamente');
            GetAdd($("#idpersonap").val());
        }
    }).fail(function (xhr, status, error) {
        var err = status + ", " + error;
        alert(err);
    });

    return false;
}

function PacienteReset() {
    var id = $("#idpersonap").val();
    $('#frmpaciente').trigger("reset");
    $("#idpersonap").val(id);
    $("#idpaciente").val(0);
}

function GetEspecie() {
    $("#selespecie").empty().append('<option value="">Seleccionar</option>');
    $.ajax({
        url: "/Cliente/GetEspecie",
        data: {},
        dataType: "json",
        type: "GET",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        if (typeof (data) === 'object' && data.length > 0) {
            $.each(data, function (i, el) {
                $("#selespecie").append('<option value="' + el.IdEspecie + '">' + el.Descripcion + '</option>');
            });
        }
    }).fail(function (xhr, status, error) {
        var err = status + ", " + error;
        alert(err);
    });
}

function GetRaza(id) {
    $("#selraza").empty().append('<option value="">Seleccionar</option>');
    if (id != undefined && id != '') {
        $.ajax({
            url: "/Cliente/GetRaza",
            data: { idespacie: id },
            async: false,
            dataType: "json",
            type: "GET",
            contentType: "application/json; charset=utf-8"
        }).done(function (data) {
            if (typeof (data) === 'object' && data.length > 0) {
                $.each(data, function (i, el) {
                    $("#selraza").append('<option value="' + el.IdRaza + '">' + el.Descripcion + '</option>');
                });
            }
        }).fail(function (xhr, status, error) {
            var err = status + ", " + error;
            alert(err);
        });
    }
}

function uploadpaciente() {
    var i = 0, len = this.files.length, img, reader, file;
    var formdata = false;
    if (window.FormData) {
        formdata = new FormData();
    }
    for (; i < len; i++) {
        file = this.files[i];
        if (!!file.type.match(/image.*/)) {
            if (window.FileReader) {
                reader = new FileReader();
                reader.onloadend = function (e) {
                    //showUploadedItem(e.target.result, file.fileName);
                };
                reader.readAsDataURL(file);
            }

            if (formdata) {
                formdata.append("image", file);
            }
            if (formdata) {
                jQuery.ajax({
                    url: "/Cliente/uploadpaciente",
                    type: "POST",
                    data: formdata,
                    processData: false,
                    contentType: false
                }).done(function (data) {
                    if (typeof (data) === 'string') {
                        $("#txtrutap").val(data);
                    }
                });
            }
        }
        else {
            alert('Not a vaild image!');
        }
    }
}

function getdate(date) {
    if (date == null || date == '')
        return '';
    if (date.indexOf('/Date') != -1) {
        var newDate = new Date(parseInt(date.substr(6))),
         anno = newDate.getFullYear(),
         mes = newDate.getMonth() + 1,
         dia = newDate.getDate(),
         hora = newDate.getHours(),
         minutos = newDate.getMinutes();
        mes = (mes < 10) ? ("0" + mes) : mes,
        dia = (dia < 10) ? ("0" + dia) : dia,
        hora = (hora < 10) ? ("0" + hora) : hora,
        minutos = (minutos < 10) ? ("0" + minutos) : minutos;
        return dia + '/' + mes + '/' + anno;// + ' ' + hora + ':' + minutos;
        //return newDate;
    }
    else { return date; }
}