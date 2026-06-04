using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class CalificacionesModel : PaginaBase
    {
        private readonly CalificacionesPresentacion _negocio = new();

        public List<Calificaciones> Lista { get; private set; } = new();
        public List<Citas> Citas { get; private set; } = new();
        public List<Barberos> Barberos { get; private set; } = new();
        public List<Empleados> Empleados { get; private set; } = new();
        public List<Personas> Personas { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        [BindProperty] public Calificaciones Item { get; set; } = new();
        [TempData] public string? Mensaje { get; set; }
        [TempData] public string? ErrorMsg { get; set; }

        // Promedio por barbero
        public Dictionary<int, double> PromediosBarbero { get; private set; } = new();

        public IActionResult OnGet()
        {
            var redir = ValidarAcceso("Administrador", "Barbero", "Cliente");
            if (redir != null) return redir;
            Cargar();
            return Page();
        }

        public async Task<IActionResult> OnPostGuardarAsync()
        {
            var redir = ValidarAcceso("Administrador", "Cliente");
            if (redir != null) return redir;
            try
            {
                if (Item.IdCita == 0)
                    throw new Exception("Debes seleccionar una cita.");
                if (Item.IdBarbero == 0)
                    throw new Exception("Debes seleccionar un barbero.");
                if (Item.Puntaje < 1 || Item.Puntaje > 5)
                    throw new Exception("El puntaje debe estar entre 1 y 5 estrellas.");

                Item.FechaCalificacion = DateTime.Now;

                if (Item.IdCalificacion == 0) await _negocio.Guardar(Item, RolActual);
                else _negocio.Modificar(Item, RolActual);

                Mensaje = $"Calificación de {Item.Puntaje} estrella(s) guardada correctamente.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        public IActionResult OnPostEliminar()
        {
            var redir = ValidarAcceso("Administrador");
            if (redir != null) return redir;
            try
            {
                _negocio.Eliminar(Item, RolActual);
                Mensaje = "Calificación eliminada.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        public string NombreBarbero(int idBarbero)
        {
            var b = Barberos.FirstOrDefault(x => x.IdBarbero == idBarbero);
            if (b == null) return $"Barbero #{idBarbero}";
            var e = Empleados.FirstOrDefault(x => x.IdEmpleado == b.IdEmpleado);
            if (e == null) return $"Barbero #{idBarbero}";
            var p = Personas.FirstOrDefault(x => x.IdPersona == e.IdPersona);
            return p == null ? $"Barbero #{idBarbero}" : $"{p.Nombres} {p.Apellidos}";
        }

        public string NombreCita(int idCita)
        {
            var c = Citas.FirstOrDefault(x => x.IdCita == idCita);
            return c == null ? $"Cita #{idCita}"
                : $"Cita #{idCita} — {c.FechaHoraInicio?.ToString("dd/MM/yyyy HH:mm") ?? "-"}";
        }

        public string Estrellas(int puntaje) =>
            string.Concat(Enumerable.Range(1, 5).Select(i => i <= puntaje ? "★" : "☆"));

        private void Cargar()
        {
            try
            {
                Lista = _negocio.Consultar(RolActual) ?? new();
                Citas = new CitasPresentacion().Consultar(RolActual) ?? new();
                Barberos = new BarberosPresentacion().Consultar(RolActual) ?? new();
                Empleados = new EmpleadosPresentacion().Consultar(RolActual) ?? new();
                Personas = new PersonasPresentacion().Consultar(RolActual) ?? new();

             
                PromediosBarbero = Lista
                    .GroupBy(c => c.IdBarbero)
                    .ToDictionary(g => g.Key, g => g.Average(c => c.Puntaje));

                if (EsAdministrador)
                    Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                        .Select(a => new FilaAuditoria
                        {
                            IdAuditoria = a.IdAuditoria,
                            IdReferencia = a.IdCalificacion,
                            Accion = a.Accion ?? "",
                            Fecha = a.Fecha
                        }).ToList();
            }
            catch (Exception ex) { ErrorMsg = "No se pudo conectar con el API: " + ex.Message; }
        }
    }
}