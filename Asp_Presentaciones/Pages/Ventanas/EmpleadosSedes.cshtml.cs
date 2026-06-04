using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class EmpleadosPorSedeModel : PaginaBase
    {
        private readonly EmpleadosSedesPresentacion _negocio = new();

        public List<EmpleadoSede> Lista { get; private set; } = new();
        public List<Empleados> Empleados { get; private set; } = new();
        public List<Sedes> Sedes { get; private set; } = new();

        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        [BindProperty] public EmpleadoSede Item { get; set; } = new();

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
                if (Item.IdEmpleado == 0 || Item.IdSede == 0)
                    throw new Exception("Debe seleccionar empleado y sede.");

                Item.FechaAsignacion = DateTime.Now;

              
                var yaExiste = Lista.Any(x =>
                    x.IdEmpleado == Item.IdEmpleado &&
                    x.IdSede == Item.IdSede &&
                    x.IdEmpleadoSede != Item.IdEmpleadoSede);

                if (yaExiste)
                    throw new Exception("Este empleado ya está asignado a esta sede.");

               
                var conflicto = Lista.Any(x =>
                    x.IdEmpleado == Item.IdEmpleado &&
                    x.IdEmpleadoSede != Item.IdEmpleadoSede);

                if (conflicto)
                    throw new Exception("El empleado ya está asignado a otra sede.");

                if (Item.IdEmpleadoSede == 0)
                    await _negocio.Guardar(Item, RolActual);
                else
                    _negocio.Modificar(Item, RolActual);

                Mensaje = "Asignación guardada correctamente.";
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
                Mensaje = "Asignación eliminada.";
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }

            return RedirectToPage();
        }

      
        public string NombreEmpleado(int id)
        {
            var emp = Empleados.FirstOrDefault(x => x.IdEmpleado == id);
            return emp != null ? $"Empleado #{emp.IdEmpleado}" : $"Empleado #{id}";
        }

        public string NombreSede(int id)
        {
            var sede = Sedes.FirstOrDefault(x => x.IdSede == id);
            return sede != null ? $"Sede #{sede.IdSede}" : $"Sede #{id}";
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
                        IdAuditoria = a.IdAuditoria,
                        IdReferencia = a.IdEmpleadoSede,
                        Accion = a.Accion ?? "",
                        Fecha = a.Fecha
                    }).ToList();
            }
            catch (Exception ex)
            {
                ErrorMsg = "Error conexión API: " + ex.Message;
            }
        }
    }
}