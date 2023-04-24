function SeleccionarCliente(id, nombre) {
    $("#clienteId").val(id);
    $("#clienteNombre").val(nombre);
}


$(document).ready(function () {
    $("#tablaClientes").DataTable({
        "language": {
            "url": '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'
        }
    });
});