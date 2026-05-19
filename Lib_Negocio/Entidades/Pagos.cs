namespace Lib_Negocio.Entidades
{
    public class Pagos
    {
        public int      IdPago        { get; set; }
        public int      IdCita        { get; set; }   // FK
        public decimal  Monto         { get; set; }
        public decimal  Descuento     { get; set; }
        public decimal  Total         { get; set; }
        public string   NombreMetodo  { get; set; } = string.Empty;
        public string   EstadoPago    { get; set; } = "Pendiente";
        public DateTime FechaPago     { get; set; } = DateTime.Now;
        public string   Observaciones { get; set; } = string.Empty;

        // Navegacion
        public Citas? Cita { get; set; }
    }
}
