using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class ProductosModel : PaginaBase
    {
        private readonly ProductosPresentacion _negocio = new();

        public List<Productos> Lista { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();
        public int ProductosCriticos { get; private set; }

        public static readonly List<string> Categorias = new()
        {
            "Fijación", "Tratamiento", "Coloración", "Cuidado barba",
            "Shampoo", "Acondicionador", "Herramientas", "Otros"
        };

        [BindProperty] public Productos Item { get; set; } = new();
        [TempData] public string? Mensaje { get; set; }
        [TempData] public string? ErrorMsg { get; set; }

        public decimal? MargenCalculado { get; private set; }
        public decimal? PorcentajeMargen { get; private set; }
        public string NombreProducto { get; private set; } = "";

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
                if (Item.PrecioVenta < Item.PrecioCompra)
                    throw new Exception("El precio de venta no puede ser menor al precio de compra.");
                if (Item.StockActual < 0)
                    throw new Exception("El stock no puede ser negativo.");

                if (Item.IdProducto == 0) await _negocio.Guardar(Item, RolActual);
                else _negocio.Modificar(Item, RolActual);
                Mensaje = "Producto guardado correctamente.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        public IActionResult OnPostEliminar()
        {
            var redir = ValidarAcceso("Administrador");
            if (redir != null) return redir;
            try { _negocio.Eliminar(Item, RolActual); Mensaje = "Producto eliminado."; }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        public IActionResult OnPostCalcularMargen()
        {
            var redir = ValidarAcceso("Administrador");
            if (redir != null) return redir;
            try
            {
                Cargar();
                if (Item.PrecioCompra <= 0) throw new Exception("Precio de compra inválido.");
                MargenCalculado = Item.PrecioVenta - Item.PrecioCompra;
                PorcentajeMargen = (MargenCalculado / Item.PrecioCompra) * 100;
                NombreProducto = Item.Nombre;
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return Page();
        }

        private void Cargar()
        {
            try
            {
                Lista = _negocio.Consultar(RolActual) ?? new();
                ProductosCriticos = Lista.Count(p => p.StockActual <= 0);
                if (EsAdministrador)
                    Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                        .Select(a => new FilaAuditoria
                        {
                            IdAuditoria = a.IdAuditoria,
                            IdReferencia = a.IdProducto,
                            Accion = a.Accion ?? "",
                            Fecha = a.Fecha
                        }).ToList();
            }
            catch (Exception ex) { ErrorMsg = "No se pudo conectar con el API: " + ex.Message; }
        }
    }
}