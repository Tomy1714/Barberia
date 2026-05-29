using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class RecepcionistasModel : PaginaBase
    {
        private readonly RecepcionistasPresentacion _negocio = new();

        public List<Recepcionistas> Lista { get; private set; } = new();
        public List<Empleados> Empleados { get; private set; } = new();
        public List<Sedes> Sedes { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        [BindProperty] public Recepcionistas Item { get; set; } = new();
        [TempData] public string? Mensaje { get; set; }
        [TempData] public string? ErrorMsg { get; set; }

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
                if (Item.IdRecepcionista == 0) await _negocio.Guardar(Item, RolActual);
                else _negocio.Modificar(Item, RolActual);
                Mensaje = "Recepcionista guardado correctamente.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        public IActionResult OnPostEliminar()
        {
            var redir = ValidarAcceso("Administrador");
            if (redir != null) return redir;
            try { _negocio.Eliminar(Item, RolActual); Mensaje = "Recepcionista eliminado."; }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        private void Cargar()
        {
            try
            {
                Lista = _negocio.Consultar(RolActual) ?? new();
                Empleados = new EmpleadosPresentacion().Consultar(RolActual) ?? new();
                Sedes = new SedesPresentacion().Consultar(RolActual) ?? new();
                Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                    .Select(a => new FilaAuditoria
                    {
                        IdAuditoria = a.IdAuditoria, IdReferencia = a.IdRecepcionista,
                        Accion = a.Accion ?? "", Fecha = a.Fecha
                    }).ToList();
            }
            catch (Exception ex) { ErrorMsg = "No se pudo conectar con el API: " + ex.Message; }
        }

        public string Empleado(int id) => Empleados.FirstOrDefault(x => x.IdEmpleado == id)?.Cargo is string c ? $"{c} (#{id})" : $"#{id}";
        public string Sede(int id) => Sedes.FirstOrDefault(x => x.IdSede == id)?.Nombre ?? $"#{id}";
    }
}
