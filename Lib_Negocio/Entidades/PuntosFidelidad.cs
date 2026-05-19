namespace Lib_Negocio.Entidades
{
    public class PuntosFidelidad
    {
        public int      IdPuntos           { get; set; }
        public int      IdCliente          { get; set; }   // FK
        public int      PuntosAcumulados   { get; set; }
        public decimal  FactorConversion   { get; set; }
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;

        // Navegacion
        public Clientes? Cliente { get; set; }
    }
}
