var dataTable;

$(document).ready(function () {
    cargarDataTable();
});

function cargarDataTable() {
    dataTable = $("#tblSliders").DataTable({
        "ajax": {
            "url": "/admin/slider/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "nombre", "width": "45%" },
            /*EMPIEZA EDICION ESTADO*/
            {
                "data": "estado",
                "render": function (estadoActual) {
                    if (estadoActual == true) {
                        return "Activo"
                    } else {
                        return "Inactivo"
                    }
                },
                "width": "20%"
            },
            /*CIERRA EDICION ESTADO*/
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Slider/Update/${data}" class="btn btn-info text-white" style="cursor:pointer; width:100px;">
                                <i class="fa-solid fa-pen-to-square"></i> Editar</a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Slider/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                <i class="fa-solid fa-trash-can"></i> Borrar</a>
                            </div>
                           `;
                }, "width":"30%"
            }
        ],  
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}
function Delete(url) {
    swal({
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}