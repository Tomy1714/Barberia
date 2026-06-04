using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class TurnosModel : PaginaBase
    {
        private readonly TurnosPresentacion _negocio = new();

        public List<Turnos> Lista { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

 
        public List<Empleados> Barberos { get; set; } = new();
        public List<Sedes> Sedes { get; set; } = new();
        public List<Horarios> Horarios { get; set; } = new();

        [BindProperty] public Turnos Item { get; set; } = new();

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
            var redir = ValidarAcceso("Administrador", "Barbero");
            if (redir != null) return redir;

            try
            {
     
                if (Item.HoraInicio >= Item.HoraFin)
                {
                    ErrorMsg = "La hora de inicio debe ser menor a la de fin.";
                    return RedirectToPage();
                }

                if (Item.IdBarbero == 0 || Item.IdSede == 0)
                {
                    ErrorMsg = "Debe seleccionar barbero y sede.";
                    return RedirectToPage();
                }

                if (Item.IdTurno == 0)
                    await _negocio.Guardar(Item, RolActual);
                else
                    _negocio.Modificar(Item, RolActual);

                Mensaje = "Turno guardado correctamente.";
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }

            return RedirectToPage();
        }

        public IActionResult OnPostEliminar()
        {
            var redir = ValidarAcceso("Administrador", "Barbero");
            if (redir != null) return redir;

            try
            {
                _negocio.Eliminar(Item, RolActual);
                Mensaje = "Turno eliminado correctamente.";
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }

            return RedirectToPage();
        }

        private void Cargar()
        {
            try
            {
                Lista = _negocio.Consultar(RolActual) ?? new();

                Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                    .Select(a => new FilaAuditoria
                    {
                        IdAuditoria = a.IdAuditoria,
                        IdReferencia = a.IdTurno,
                        Accion = a.Accion ?? "",
                        Fecha = a.Fecha
                    }).ToList();


                Barberos = ObtenerBarberos();
                Sedes = ObtenerSedes();
                Horarios = ObtenerHorarios();
            }
            catch (Exception ex)
            {
                ErrorMsg = "Error cargando datos: " + ex.Message;
            }
        }

      
        private List<Empleados> ObtenerBarberos()
        {
            try
            {
                var emp = new EmpleadosPresentacion();
                return emp.Consultar(RolActual)?
                    .Where(x => x.Cargo == "Barbero")
                    .ToList() ?? new List<Empleados>();
            }
            catch
            {
                return new List<Empleados>();
            }
        }

        private List<Sedes> ObtenerSedes()
        {
            try
            {
                var sed = new SedesPresentacion();
                return sed.Consultar(RolActual) ?? new List<Sedes>();
            }
            catch
            {
                return new List<Sedes>();
            }
        }

        private List<Horarios> ObtenerHorarios()
        {
            try
            {
                var hor = new HorariosPresentacion();
                return hor.Consultar(RolActual) ?? new List<Horarios>();
            }
            catch
            {
                return new List<Horarios>();
            }
        }
    }
}