// Abre un modal de formulario. Si 'datos' es null => modo "nuevo" (limpia campos).
// Si trae objeto => modo "editar" (rellena por name="Item.<clave>").
function bbAbrirModal(idModal, datos) {
    var modal = document.getElementById(idModal);
    if (!modal) return;
    var form = modal.querySelector('form');

    form.querySelectorAll('input, select, textarea').forEach(function (el) {
        if (el.name && el.name.indexOf('__') === 0) return; // token antifalsificacion
        if (el.type === 'checkbox') el.checked = false;
        else el.value = '';
    });

    if (datos) {
        Object.keys(datos).forEach(function (k) {
            var el = form.querySelector('[name="Item.' + k + '"]');
            if (!el) return;
            if (el.type === 'checkbox') {
                el.checked = (datos[k] === true || datos[k] === 'true' || datos[k] === 1);
            } else if (el.type === 'datetime-local' && datos[k]) {
                el.value = String(datos[k]).substring(0, 16);
            } else {
                el.value = (datos[k] === null || datos[k] === undefined) ? '' : datos[k];
            }
        });
    }

    var etiqueta = modal.querySelector('.modo-titulo');
    if (etiqueta) etiqueta.textContent = datos ? 'Editar' : 'Nuevo';

    new bootstrap.Modal(modal).show();
}

// Confirmacion para los formularios de borrado.
function bbConfirmarBorrado(ev) {
    if (!confirm('¿Eliminar este registro? Esta acción no se puede deshacer.')) {
        ev.preventDefault();
        return false;
    }
    return true;
}
