namespace Lib_Negocio.Entidades
{
    public class Inventarios
    {
        public int      IdInventario       { get; set; }
        public int      IdSede             { get; set; }   // FK
        public int      StockMinimo        { get; set; }
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;

        // Navegacion
        public Sedes? Sede { get; set; }
    }
}
