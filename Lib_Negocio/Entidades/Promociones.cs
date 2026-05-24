using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class Promociones
    {
        [Key] public int      IdPromocion         { get; set; }
        public int      IdServicio          { get; set; }   // FK
        public string   Nombre              { get; set; } = string.Empty;
        public string   Descripcion         { get; set; } = string.Empty;
        public decimal  PorcentajeDescuento { get; set; }
        public DateTime FechaInicio         { get; set; }
        public DateTime FechaFin            { get; set; }
        public bool     Activa              { get; set; } = true;

        // Navegacion


        [ForeignKey(nameof(IdServicio))]
        public Servicios? Servicio { get; set; }
    }
}
