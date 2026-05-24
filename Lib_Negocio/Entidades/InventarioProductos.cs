using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class InventarioProductos
    {
        [Key] public int IdInventarioProducto { get; set; }
        public int IdInventario         { get; set; }   // FK
        public int IdProducto           { get; set; }   // FK
        public int Cantidad             { get; set; }

        // Navegacion


        [ForeignKey(nameof(IdProducto))]
        public Productos? Producto { get; set; }

        [ForeignKey(nameof(IdInventario))]
        public Inventarios? Inventario { get; set; }
    }
}
