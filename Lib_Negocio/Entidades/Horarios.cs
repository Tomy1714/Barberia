namespace Lib_Negocio.Entidades
{
    public class Horarios
    {
        public int  IdHorario   { get; set; }
        public int  IdEmpleado  { get; set; }   // FK
        public int  HoraEntrada { get; set; }
        public int  HoraSalida  { get; set; }
        public bool Activo      { get; set; } = true;

        // Navegacion
        public Empleados?        Empleado    { get; set; }
        public List<HorariosDias> HorarioDias { get; set; } = new();
    }
}
