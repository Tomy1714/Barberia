using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class HorariosModel : PaginaBase
    {
        private readonly HorariosPresentacion _negocio = new();

        public List<Horarios> Lista { get; private set; } = new();
        public List<Empleados> Empleados { get; private set; } = new();
        public List<Personas> Personas { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        [BindProperty] public Horarios Item { get; set; } = new();
        [TempData] public string? Mensaje { get; set; }
        [TempData] public string? ErrorMsg { get; set; }

        public IActionResult OnGet()
        {
            var redir = ValidarAcceso("Administrador", "Barbero");
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
                if (Item.IdEmpleado == 0)
                    throw new Exception("Selecciona un empleado.");

                if (Item.HoraSalida <= Item.HoraEntrada)
                    throw new Exception("Horario inválido.");

                if (Item.IdHorario == 0)
                    await _negocio.Guardar(Item, RolActual);
                else
                    _negocio.Modificar(Item, RolActual);

                Mensaje = "Horario guardado correctamente.";
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
                Mensaje = "Horario eliminado correctamente.";
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }

            return RedirectToPage();
        }

        public string NombreEmpleado(int idEmpleado)
        {
            var emp = Empleados.FirstOrDefault(x => x.IdEmpleado == idEmpleado);
            if (emp == null) return $"Empleado #{idEmpleado}";

            var per = Personas.FirstOrDefault(x => x.IdPersona == emp.IdPersona);
            return per == null ? $"Empleado #{idEmpleado}" : $"{per.Nombres} {per.Apellidos}";
        }

        private void Cargar()
        {
            Lista = _negocio.Consultar(RolActual) ?? new();

            Empleados = new EmpleadosPresentacion()
                .Consultar(RolActual) ?? new();

            Personas = new PersonasPresentacion()
                .Consultar(RolActual) ?? new();

            Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                .Select(a => new FilaAuditoria
                {
                    IdAuditoria = a.IdAuditoria,
                    IdReferencia = a.IdHorario,
                    Accion = a.Accion ?? "",
                    Fecha = a.Fecha
                }).ToList();
        }
    }
}