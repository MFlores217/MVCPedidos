function SeleccionarProducto(id, nombre) {
    $("#productoId").val(id);
    $("#productoNombre").val(nombre);
}

$(document).ready(function () {
    $("#tablaProductos").DataTable({
        "language": {
            "url": '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'
        }
    });
});