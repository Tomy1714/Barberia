namespace Lib_Negocio.Entidades
{
    public class EmpleadosSedes
    {
        public int      IdEmpleadoSede  { get; set; }
        public int      IdEmpleado      { get; set; }   // FK
        public int      IdSede          { get; set; }   // FK
        public DateTime FechaAsignacion { get; set; } = DateTime.Now;

        // Navegacion
        public Empleados? Empleado { get; set; }
        public Sedes?     Sede     { get; set; }
    }
}
