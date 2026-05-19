namespace Lib_Negocio.Entidades
{
    public class Citas
    {
        public int      IdCita          { get; set; }
        public int      IdCliente       { get; set; }   // FK
        public int      IdBarbero       { get; set; }   // FK
        public int      IdServicio      { get; set; }   // FK
        public DateTime FechaHoraInicio { get; set; }
        public string   Estado          { get; set; } = "Pendiente";
        public string   Observaciones   { get; set; } = string.Empty;
        public DateTime FechaCreacion   { get; set; } = DateTime.Now;

        // Navegacion
        public Clientes?  Cliente  { get; set; }
        public Barberos?  Barbero  { get; set; }
        public Servicios? Servicio { get; set; }
    }
}
