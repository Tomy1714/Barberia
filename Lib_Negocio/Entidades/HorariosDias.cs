namespace Lib_Negocio.Entidades
{
    public class HorariosDias
    {
        public int    IdHorarioDia { get; set; }
        public int    IdHorario    { get; set; }   // FK
        public string Dia          { get; set; } = string.Empty;

        // Navegacion
        public Horarios? Horario { get; set; }
    }
}
