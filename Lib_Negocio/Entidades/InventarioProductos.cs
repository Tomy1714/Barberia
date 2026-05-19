namespace Lib_Negocio.Entidades
{
    public class InventarioProductos
    {
        public int IdInventarioProducto { get; set; }
        public int IdInventario         { get; set; }   // FK
        public int IdProducto           { get; set; }   // FK
        public int Cantidad             { get; set; }

        // Navegacion
        public Inventarios? Inventario { get; set; }
        public Productos?   Producto   { get; set; }
    }
}
