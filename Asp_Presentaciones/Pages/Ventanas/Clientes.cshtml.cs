using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class ClientesModel : PaginaBase
    {
        private readonly ClientesPresentacion _negocio = new();

        public List<Clientes> Lista { get; private set; } = new();
        public List<Personas> Personas { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        [BindProperty] public Clientes Item { get; set; } = new();
        [TempData] public string? Mensaje { get; set; }
        [TempData] public string? ErrorMsg { get; set; }

        public IActionResult OnGet()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista");
            if (redir != null) return redir;
            Cargar();
            return Page();
        }

        public async Task<IActionResult> OnPostGuardarAsync()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista");
            if (redir != null) return redir;
            try
            {
                if (Item.IdCliente == 0) await _negocio.Guardar(Item, RolActual);
                else _negocio.Modificar(Item, RolActual);
                Mensaje = "Cliente guardado correctamente.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        public IActionResult OnPostEliminar()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista");
            if (redir != null) return redir;
            try { _negocio.Eliminar(Item, RolActual); Mensaje = "Cliente eliminado."; }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        private void Cargar()
        {
            try
            {
                Lista = _negocio.Consultar(RolActual) ?? new();
                Personas = new PersonasPresentacion().Consultar(RolActual) ?? new();
                if (EsAdministrador)
                    Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                        .Select(a => new FilaAuditoria
                        {
                            IdAuditoria = a.IdAuditoria, IdReferencia = a.IdCliente,
                            Accion = a.Accion ?? "", Fecha = a.Fecha
                        }).ToList();
            }
            catch (Exception ex) { ErrorMsg = "No se pudo conectar con el API: " + ex.Message; }
        }

        public string NombrePersona(int idPersona)
        {
            var p = Personas.FirstOrDefault(x => x.IdPersona == idPersona);
            return p == null ? $"#{idPersona}" : $"{p.Nombres} {p.Apellidos}";
        }
    }
}
