namespace Lib_Negocio.Entidades
{
    public class Turnos
    {
        public int      IdTurno    { get; set; }
        public int      IdBarbero  { get; set; }   // FK
        public int      IdSede     { get; set; }   // FK
        public DateTime FechaTurno { get; set; }
        public int      HoraInicio { get; set; }
        public int      HoraFin    { get; set; }
        public string   Estado     { get; set; } = "Programado";

        // Navegacion
        public Barberos? Barbero { get; set; }
        public Sedes?    Sede    { get; set; }
    }
}
