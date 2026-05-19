namespace Lib_Negocio.Entidades
{
    public class Calificaciones
    {
        public int      IdCalificacion    { get; set; }
        public int      IdCita            { get; set; }   // FK
        public int      IdBarbero         { get; set; }   // FK
        public int      Puntaje           { get; set; }
        public string   Comentario        { get; set; } = string.Empty;
        public DateTime FechaCalificacion { get; set; } = DateTime.Now;

        // Navegacion
        public Citas?    Cita    { get; set; }
        public Barberos? Barbero { get; set; }
    }
}
