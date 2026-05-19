namespace Lib_Negocio.Entidades
{
    public class Recepcionistas
    {
        public int     IdRecepcionista     { get; set; }
        public int     IdEmpleado          { get; set; }   // FK
        public int     IdSede              { get; set; }   // FK
        public string  TurnoAsignado       { get; set; } = string.Empty;
        public int     CitasGestionadasMes { get; set; }
        public decimal BonoPorMeta         { get; set; }

        // Navegacion
        public Empleados? Empleado { get; set; }
        public Sedes?     Sede     { get; set; }
    }
}
