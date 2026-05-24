using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class Pagos
    {
        [Key] public int      IdPago        { get; set; }
        public int      IdCita        { get; set; }   // FK
        public decimal  Monto         { get; set; }
        public decimal  Descuento     { get; set; }
        public decimal  Total         { get; set; }
        public string?   NombreMetodo  { get; set; }// = string.Empty;
        public string?   EstadoPago    { get; set; }// = "Pendiente";
        public DateTime? FechaPago     { get; set; } //= DateTime.Now;
        public string?   Observaciones { get; set; } //= string.Empty;

        // Navegacion


        [ForeignKey(nameof(IdCita))]
        public Citas? Cita { get; set; }
    }
}
