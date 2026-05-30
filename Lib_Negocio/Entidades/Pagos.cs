using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class Pagos
    {
        [Key] public int      IdPago        { get; set; }
        public int      IdCita        { get; set; }   
        public decimal  Monto         { get; set; }
        public decimal  Descuento     { get; set; }
        public decimal  Total         { get; set; }
        public string?   NombreMetodo  { get; set; }
        public string?   EstadoPago    { get; set; }
        public DateTime? FechaPago     { get; set; } 
        public string?   Observaciones { get; set; } 

        


        [ForeignKey(nameof(IdCita))]
        public Citas? Cita { get; set; }
    }
}
