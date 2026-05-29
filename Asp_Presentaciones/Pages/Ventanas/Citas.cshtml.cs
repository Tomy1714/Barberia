using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class CitasModel : PaginaBase
    {
        private readonly CitasPresentacion _negocio = new();

        public List<Citas> Lista { get; private set; } = new();
        public List<Clientes> Clientes { get; private set; } = new();
        public List<Barberos> Barberos { get; private set; } = new();
        public List<Servicios> Servicios { get; private set; } = new();
        public List<Personas> Personas { get; private set; } = new();
        public List<Empleados> Empleados { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        public static readonly string[] Estados = { "Pendiente", "Confirmada", "EnProceso", "Completada", "Cancelada" };

        [BindProperty] public Citas Item { get; set; } = new();
        [TempData] public string? Mensaje { get; set; }
        [TempData] public string? ErrorMsg { get; set; }

        public IActionResult OnGet()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista", "Barbero", "Cliente");
            if (redir != null) return redir;
            Cargar();
            return Page();
        }

        public async Task<IActionResult> OnPostGuardarAsync()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista", "Cliente");
            if (redir != null) return redir;
            try
            {
                if (Item.IdCita == 0) await _negocio.Guardar(Item, RolActual);
                else _negocio.Modificar(Item, RolActual);
                Mensaje = "Cita guardada correctamente.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        public IActionResult OnPostEliminar()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista");
            if (redir != null) return redir;
            try { _negocio.Eliminar(Item, RolActual); Mensaje = "Cita eliminada."; }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        private void Cargar()
        {
            try
            {
                Lista = _negocio.Consultar(RolActual) ?? new();
                Clientes = new ClientesPresentacion().Consultar(RolActual) ?? new();
                Barberos = new BarberosPresentacion().Consultar(RolActual) ?? new();
                Servicios = new ServiciosPresentacion().Consultar(RolActual) ?? new();
                Personas = new PersonasPresentacion().Consultar(RolActual) ?? new();
                Empleados = new EmpleadosPresentacion().Consultar(RolActual) ?? new();
                if (EsAdministrador)
                    Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                        .Select(a => new FilaAuditoria
                        {
                            IdAuditoria = a.IdAuditoria, IdReferencia = a.IdCita,
                            Accion = a.Accion ?? "", Fecha = a.Fecha
                        }).ToList();
            }
            catch (Exception ex) { ErrorMsg = "No se pudo conectar con el API: " + ex.Message; }
        }

        public string Cliente(int idCliente)
        {
            var c = Clientes.FirstOrDefault(x => x.IdCliente == idCliente);
            if (c == null) return $"#{idCliente}";
            var p = Personas.FirstOrDefault(x => x.IdPersona == c.IdPersona);
            return p == null ? $"Cliente #{idCliente}" : $"{p.Nombres} {p.Apellidos}";
        }

        public string Barbero(int idBarbero)
        {
            var b = Barberos.FirstOrDefault(x => x.IdBarbero == idBarbero);
            if (b == null) return $"#{idBarbero}";
            var e = Empleados.FirstOrDefault(x => x.IdEmpleado == b.IdEmpleado);
            return e == null ? $"Barbero #{idBarbero}" : $"{e.Cargo} (#{idBarbero})";
        }

        public string Servicio(int idServicio) => Servicios.FirstOrDefault(x => x.IdServicio == idServicio)?.Nombre ?? $"#{idServicio}";

        public string EstadoClase(string? estado) => estado switch
        {
            "Completada" or "Confirmada" => "activo",
            "Cancelada" => "inactivo",
            _ => "ins"
        };
    }
}
