namespace Lib_Negocio.Entidades
{
    public class Notificaciones
    {
        public int       IdNotificacion { get; set; }
        public int       IdCliente      { get; set; }   // FK
        public int       IdCita         { get; set; }   // FK
        public string    Tipo           { get; set; } = string.Empty;
        public string    Canal          { get; set; } = string.Empty;
        public string    Mensaje        { get; set; } = string.Empty;
        public string    Estado         { get; set; } = "Pendiente";
        public DateTime? FechaEnvio     { get; set; }
        public DateTime  FechaCreacion  { get; set; } = DateTime.Now;

        // Navegacion
        public Clientes? Cliente { get; set; }
        public Citas?    Cita    { get; set; }
    }
}
