using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class Combos
    {
        [Key] public int     IdCombo        { get; set; }
        public int     IdServicio     { get; set; }   // FK
        public decimal DescuentoCombo { get; set; }
        public string  Descripcion    { get; set; } = string.Empty;

        // Navegacion


        [ForeignKey(nameof(IdServicio))]
        public Servicios? Servicio { get; set; }
    }
}
