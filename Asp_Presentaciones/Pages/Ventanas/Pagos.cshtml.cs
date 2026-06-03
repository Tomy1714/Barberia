using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class PagosModel : PaginaBase
    {
        private readonly PagosPresentacion _negocio = new();
        private readonly FacturasPresentacion _facturas = new();

        public List<Pagos> Lista { get; private set; } = new();
        public List<Citas> Citas { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        [BindProperty] public Pagos Item { get; set; } = new();
        [TempData] public string? Mensaje { get; set; }
        [TempData] public string? ErrorMsg { get; set; }

       
        public static readonly List<string> MetodosPago = new()
        {
            "Efectivo", "Tarjeta débito", "Tarjeta crédito",
            "Transferencia" 
        };

        public IActionResult OnGet()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista", "Cliente");
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
                if (Item.IdCita == 0)
                    throw new Exception("Debes seleccionar una cita.");
                if (string.IsNullOrEmpty(Item.NombreMetodo))
                    throw new Exception("Debes seleccionar un método de pago.");
                if (Item.Monto <= 0)
                    throw new Exception("El monto debe ser mayor a cero.");

               
                Item.Total = Item.Monto - Item.Descuento;
                Item.EstadoPago = "Aprobado";
                Item.FechaPago = DateTime.Now;

                Pagos pagoGuardado;
                if (Item.IdPago == 0)
                    pagoGuardado = await _negocio.Guardar(Item, RolActual);
                else
                {
                    pagoGuardado = _negocio.Modificar(Item, RolActual);
                    Mensaje = "Pago actualizado correctamente.";
                    return RedirectToPage();
                }

              
                var cita = Citas.Count > 0
                    ? Citas.FirstOrDefault(c => c.IdCita == Item.IdCita)
                    : new CitasPresentacion().Consultar(RolActual)
                        .FirstOrDefault(c => c.IdCita == Item.IdCita);

                if (cita != null)
                {
                    decimal subtotal = pagoGuardado.Total;
                    decimal impuestos = subtotal * 0.19m;
                    decimal total = subtotal + impuestos;

                    var factura = new Facturas
                    {
                        IdPago = pagoGuardado.IdPago,
                        IdCliente = cita.IdCliente,
                        CodigoFactura = $"FAC-{DateTime.Now:yyyyMMdd}-{pagoGuardado.IdPago}",
                        Subtotal = subtotal,
                        Impuestos = impuestos,
                        Total = total,
                        FechaEmision = DateTime.Now
                    };
                    await _facturas.Guardar(factura, RolActual);
                    Mensaje = "Pago procesado y factura con IVA: ${total:N0}";
                }
                else
                {
                    Mensaje = "Pago registrado correctamente.";
                }
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
                Mensaje = "Pago eliminado correctamente.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        public IActionResult OnPostProcesar()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista");
            if (redir != null) return redir;
            try
            {
                Cargar();
                var pago = Lista.FirstOrDefault(p => p.IdPago == Item.IdPago);
                if (pago == null) throw new Exception("Pago no encontrado.");
                if (pago.EstadoPago == "Aprobado")
                    throw new Exception("Este pago ya fue aprobado.");

                pago.EstadoPago = "Aprobado";
                pago.Total = pago.Monto - pago.Descuento;
                _negocio.Modificar(pago, RolActual);
                Mensaje = $"Pago #{pago.IdPago} aprobado. Total: ${pago.Total:N0}";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        public string NombreCita(int idCita)
        {
            var c = Citas.FirstOrDefault(x => x.IdCita == idCita);
            return c == null ? $"Cita #{idCita}"
                : $"Cita #{c.IdCita} — Cliente #{c.IdCliente} ({c.FechaHoraInicio?.ToString("dd/MM/yyyy HH:mm") ?? "-"})";
        }

        private void Cargar()
        {
            try
            {
                Lista = _negocio.Consultar(RolActual) ?? new();
                Citas = new CitasPresentacion().Consultar(RolActual) ?? new();
                if (EsAdministrador)
                    Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                        .Select(a => new FilaAuditoria
                        {
                            IdAuditoria = a.IdAuditoria,
                            IdReferencia = a.IdPago,
                            Accion = a.Accion ?? "",
                            Fecha = a.Fecha
                        }).ToList();
            }
            catch (Exception ex) { ErrorMsg = "No se pudo conectar con el API: " + ex.Message; }
        }
    }
}