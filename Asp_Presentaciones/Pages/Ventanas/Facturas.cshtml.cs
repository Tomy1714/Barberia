using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class FacturasModel : PaginaBase
    {
        private readonly FacturasPresentacion _negocio = new();

        public List<Facturas> Lista { get; private set; } = new();
        public List<Pagos> Pagos { get; private set; } = new();
        public List<Clientes> Clientes { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        [BindProperty] public Facturas Item { get; set; } = new();
        [TempData] public string? Mensaje { get; set; }
        [TempData] public string? ErrorMsg { get; set; }

        public IActionResult OnGet()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista", "Cliente");
            if (redir != null) return redir;
            Cargar();
            return Page();
        }

      
        public IActionResult OnPostEliminar()
        {
            var redir = ValidarAcceso("Administrador");
            if (redir != null) return redir;
            try
            {
                _negocio.Eliminar(Item, RolActual);
                Mensaje = "Factura eliminada correctamente.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        public string NombrePago(int idPago)
        {
            var p = Pagos.FirstOrDefault(x => x.IdPago == idPago);
            return p == null ? $"Pago #{idPago}" : $"Pago #{p.IdPago} — ${p.Total:N0} ({p.NombreMetodo})";
        }

        public string NombreCliente(int idCliente)
        {
            var c = Clientes.FirstOrDefault(x => x.IdCliente == idCliente);
            return c == null ? $"Cliente #{idCliente}" : $"Cliente #{idCliente}";
        }

        private void Cargar()
        {
            try
            {
                Lista = _negocio.Consultar(RolActual) ?? new();
                Pagos = new PagosPresentacion().Consultar(RolActual) ?? new();
                Clientes = new ClientesPresentacion().Consultar(RolActual) ?? new();
                if (EsAdministrador)
                    Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                        .Select(a => new FilaAuditoria
                        {
                            IdAuditoria = a.IdAuditoria,
                            IdReferencia = a.IdFactura,
                            Accion = a.Accion ?? "",
                            Fecha = a.Fecha
                        }).ToList();
            }
            catch (Exception ex) { ErrorMsg = "No se pudo conectar con el API: " + ex.Message; }
        }
    }
}