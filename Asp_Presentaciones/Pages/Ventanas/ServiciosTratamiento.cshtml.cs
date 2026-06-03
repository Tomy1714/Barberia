using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class ServiciosTratamientoModel : PaginaBase
    {
        private readonly ServiciosTratamientoPresentacion _negocio = new();

        public List<ServiciosTratamiento> Lista { get; private set; } = new();
        public List<Servicios> Servicios { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        [BindProperty] public ServiciosTratamiento Item { get; set; } = new();
        [TempData] public string? Mensaje { get; set; }
        [TempData] public string? ErrorMsg { get; set; }

      
        public decimal? PrecioFinalCalculado { get; private set; }
        public string NombreTratamiento { get; private set; } = "";

        public IActionResult OnGet()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista", "Cliente", "Barbero");
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
            
                if (Item.SesionesRequeridas < 1)
                    throw new Exception("El número de sesiones debe ser al menos 1.");

              
                if (Item.CostoProducto < 0)
                    throw new Exception("El costo del producto no puede ser negativo.");

                if (Item.IdServicioTratamiento == 0) await _negocio.Guardar(Item, RolActual);
                else _negocio.Modificar(Item, RolActual);

                Mensaje = "Tratamiento capilar guardado correctamente.";
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
                Mensaje = "Tratamiento capilar eliminado correctamente.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

      
        public IActionResult OnPostCalcularPrecio()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista");
            if (redir != null) return redir;
            try
            {
                Cargar();
                var servicio = Servicios.FirstOrDefault(s => s.IdServicio == Item.IdServicio);
                if (servicio == null) throw new Exception("Servicio no encontrado.");

                PrecioFinalCalculado = servicio.PrecioBase
                    + (Item.CostoProducto * Item.SesionesRequeridas);
                NombreTratamiento = $"{Item.TipoTratamiento} — {Item.SesionesRequeridas} sesión(es)";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return Page();
        }

      
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
                            IdReferencia = a.IdServicioTratamiento,
                            Accion = a.Accion ?? "",
                            Fecha = a.Fecha
                        }).ToList();
            }
            catch (Exception ex) { ErrorMsg = "No se pudo conectar con el API: " + ex.Message; }
        }
    }
}
