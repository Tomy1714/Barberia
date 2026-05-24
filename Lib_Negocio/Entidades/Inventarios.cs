using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class Inventarios
    {
        [Key] public int      IdInventario       { get; set; }
        public int      IdSede             { get; set; }   // FK
        public int      StockMinimo        { get; set; }
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;

        // Navegacion


        [ForeignKey(nameof(IdSede))]
        public Sedes? Sede { get; set; }
    }
}
