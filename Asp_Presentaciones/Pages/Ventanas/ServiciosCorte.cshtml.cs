using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class ServiciosCorteModel : PaginaBase
    {
        private readonly ServiciosCortePresentacion _negocio = new();

        public List<ServiciosCorte> Lista { get; private set; } = new();
        public List<Servicios> Servicios { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        [BindProperty] public ServiciosCorte Item { get; set; } = new();
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
                // Validar nivel de complejidad 1-5
                if (Item.NivelComplejidad < 1 || Item.NivelComplejidad > 5)
                    throw new Exception("El nivel de complejidad debe estar entre 1 y 5.");

                // Calcular recargo automaticamente: Nivel x $5.000
                Item.RecargoComplejidad = Item.NivelComplejidad * 5000;

                if (Item.IdServicioCorte == 0) await _negocio.Guardar(Item, RolActual);
                else _negocio.Modificar(Item, RolActual);

                Mensaje = "Servicio de corte guardado correctamente.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        public IActionResult OnPostEliminar()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista");
            if (redir != null) return redir;
            try
            {
                _negocio.Eliminar(Item, RolActual);
                Mensaje = "Servicio de corte eliminado correctamente.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        // Nombre del servicio para mostrar en la tabla
        public string NombreServicio(int idServicio)
        {
            var s = Servicios.FirstOrDefault(x => x.IdServicio == idServicio);
            return s == null ? $"#{idServicio}" : $"{s.Nombre} (${s.PrecioBase:N0})";
        }

        private void Cargar()
        {
            try
            {
                Lista = _negocio.Consultar(RolActual) ?? new();
                Servicios = new ServiciosPresentacion().Consultar(RolActual) ?? new();
                if (EsAdministrador)
                    Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                        .Select(a => new FilaAuditoria
                        {
                            IdAuditoria = a.IdAuditoria,
                            IdReferencia = a.IdServicioCorte,
                            Accion = a.Accion ?? "",
                            Fecha = a.Fecha
                        }).ToList();
            }
            catch (Exception ex) { ErrorMsg = "No se pudo conectar con el API: " + ex.Message; }
        }
    }
}
