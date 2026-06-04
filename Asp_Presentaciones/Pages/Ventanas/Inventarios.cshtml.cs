using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class InventariosModel : PaginaBase
    {
        private readonly InventariosPresentacion _negocio = new();

        public List<Inventarios> Lista { get; private set; } = new();
        public List<Sedes> Sedes { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        [BindProperty]
        public Inventarios Item { get; set; } = new();

        [TempData]
        public string? Mensaje { get; set; }

        [TempData]
        public string? ErrorMsg { get; set; }

        public IActionResult OnGet()
        {
            var redir = ValidarAcceso("Administrador");
            if (redir != null) return redir;

            Cargar();
            return Page();
        }

        public async Task<IActionResult> OnPostGuardarAsync()
        {
            var redir = ValidarAcceso("Administrador");
            if (redir != null) return redir;

            try
            {
                if (Item.IdSede == 0)
                    throw new Exception("Debes seleccionar una sede.");

                if (Item.StockMinimo < 0)
                    throw new Exception("El stock mínimo no puede ser negativo.");

                Item.FechaActualizacion = DateTime.Now;

                if (Item.IdInventario == 0)
                {
                    await _negocio.Guardar(Item, RolActual);
                    Mensaje = "Inventario registrado correctamente.";
                }
                else
                {
                    _negocio.Modificar(Item, RolActual);
                    Mensaje = "Inventario actualizado correctamente.";
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }

            return RedirectToPage();
        }

        public IActionResult OnPostEliminar()
        {
            var redir = ValidarAcceso("Administrador");
            if (redir != null) return redir;

            try
            {
                _negocio.Eliminar(Item, RolActual);
                Mensaje = "Inventario eliminado correctamente.";
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }

            return RedirectToPage();
        }

        public string NombreSede(int idSede)
        {
            var sede = Sedes.FirstOrDefault(s => s.IdSede == idSede);

            return sede == null
                ? $"Sede #{idSede}"
                : sede.Nombre;
        }

        private void Cargar()
        {
            try
            {
                Lista = _negocio.Consultar(RolActual) ?? new();
                Sedes = new SedesPresentacion().Consultar(RolActual) ?? new();

                Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                    .Select(a => new FilaAuditoria
                    {
                        IdAuditoria = a.IdAuditoria,
                        IdReferencia = a.IdInventario,
                        Accion = a.Accion ?? "",
                        Fecha = a.Fecha
                    }).ToList();
            }
            catch (Exception ex)
            {
                ErrorMsg = "No se pudo conectar con el API: " + ex.Message;
            }
        }
    }
}