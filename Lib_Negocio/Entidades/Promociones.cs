namespace Lib_Negocio.Entidades
{
    public class Promociones
    {
        public int      IdPromocion         { get; set; }
        public int      IdServicio          { get; set; }   // FK
        public string   Nombre              { get; set; } = string.Empty;
        public string   Descripcion         { get; set; } = string.Empty;
        public decimal  PorcentajeDescuento { get; set; }
        public DateTime FechaInicio         { get; set; }
        public DateTime FechaFin            { get; set; }
        public bool     Activa              { get; set; } = true;

        // Navegacion
        public Servicios? Servicio { get; set; }
    }
}
