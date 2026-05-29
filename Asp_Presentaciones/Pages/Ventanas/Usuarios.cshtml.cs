using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class UsuariosModel : PaginaBase
    {
        private readonly UsuariosPresentacion _negocio = new();

        public List<Usuarios> Lista { get; private set; } = new();
        public List<Personas> Personas { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        public static readonly string[] Roles = { "Administrador", "Recepcionista", "Barbero", "Cliente" };

        [BindProperty] public Usuarios Item { get; set; } = new();
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
                if (Item.IdUsuario == 0) await _negocio.Guardar(Item, RolActual);
                else _negocio.Modificar(Item, RolActual);
                Mensaje = "Usuario guardado correctamente.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        public IActionResult OnPostEliminar()
        {
            var redir = ValidarAcceso("Administrador");
            if (redir != null) return redir;
            try { _negocio.Eliminar(Item, RolActual); Mensaje = "Usuario eliminado."; }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        private void Cargar()
        {
            try
            {
                Lista = _negocio.Consultar(RolActual) ?? new();
                Personas = new PersonasPresentacion().Consultar(RolActual) ?? new();
                Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                    .Select(a => new FilaAuditoria
                    {
                        IdAuditoria = a.IdAuditoria, IdReferencia = a.IdUsuario,
                        Accion = a.Accion ?? "", Fecha = a.Fecha
                    }).ToList();
            }
            catch (Exception ex) { ErrorMsg = "No se pudo conectar con el API: " + ex.Message; }
        }

        public string NombrePersona(int id)
        {
            var p = Personas.FirstOrDefault(x => x.IdPersona == id);
            return p == null ? $"#{id}" : $"{p.Nombres} {p.Apellidos}";
        }
    }
}
