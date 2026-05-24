using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class ComboServicios
    {
        [Key] public int IdComboServicio { get; set; }
        public int IdCombo         { get; set; }   // FK
        public int IdServicio      { get; set; }   // FK

        // Navegacion


        [ForeignKey(nameof(IdCombo))]
        public Combos? Combo { get; set; }

        [ForeignKey(nameof(IdServicio))]
        public Servicios? Servicio { get; set; }
    }
}
