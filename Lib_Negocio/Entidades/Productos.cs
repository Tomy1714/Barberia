using System.ComponentModel.DataAnnotations;

namespace Lib_Negocio.Entidades
{
    public class Productos
    {
        [Key] public int     IdProducto   { get; set; }
        public string  Nombre       { get; set; } = string.Empty;
        public string  Marca        { get; set; } = string.Empty;
        public string  Categoria    { get; set; } = string.Empty;
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta  { get; set; }
        public int     StockActual  { get; set; }
        public bool    Activo       { get; set; } = true;
    }
}
