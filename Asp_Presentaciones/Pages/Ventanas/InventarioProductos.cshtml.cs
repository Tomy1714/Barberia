using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class InventarioProductosModel : PaginaBase
    {
        private readonly InventarioProductosPresentacion _negocio = new();

        public List<InventarioProductos> Lista { get; private set; } = new();
        public List<Inventarios> Inventarios { get; private set; } = new();
        public List<Productos> Productos { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        [BindProperty]
        public InventarioProductos Item { get; set; } = new();

        [TempData]
        public string? Mensaje { get; set; }

        [TempData]
        public string? ErrorMsg { get; set; }

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
                if (Item.IdInventario == 0)
                    throw new Exception("Debes seleccionar un inventario.");

                if (Item.IdProducto == 0)
                    throw new Exception("Debes seleccionar un producto.");

                if (Item.Cantidad < 0)
                    throw new Exception("La cantidad no puede ser negativa.");

                if (Item.IdInventarioProducto == 0)
                {
                    await _negocio.Guardar(Item, RolActual);
                    Mensaje = "Producto agregado al inventario.";
                }
                else
                {
                    _negocio.Modificar(Item, RolActual);
                    Mensaje = "Inventario producto actualizado.";
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }

            return RedirectToPage();
        }

        public IActionResult OnPostEliminar()
        {
            var redir = ValidarAcceso("Administrador");
            if (redir != null) return redir;

            try
            {
                _negocio.Eliminar(Item, RolActual);
                Mensaje = "Inventario producto eliminado.";
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }

            return RedirectToPage();
        }

        public string NombreProducto(int idProducto)
        {
            var producto = Productos.FirstOrDefault(p => p.IdProducto == idProducto);

            return producto == null
                ? $"Producto #{idProducto}"
                : producto.Nombre;
        }

        private void Cargar()
        {
            try
            {
                Lista = _negocio.Consultar(RolActual) ?? new();

                Inventarios = new InventariosPresentacion().Consultar(RolActual) ?? new();

                Productos = new ProductosPresentacion().Consultar(RolActual) ?? new();

                Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                    .Select(a => new FilaAuditoria
                    {
                        IdAuditoria = a.IdAuditoria,
                        IdReferencia = a.IdInventarioProducto,
                        Accion = a.Accion ?? "",
                        Fecha = a.Fecha
                    }).ToList();
            }
            catch (Exception ex)
            {
                ErrorMsg = "No se pudo conectar con el API: " + ex.Message;
            }
        }
    }
}