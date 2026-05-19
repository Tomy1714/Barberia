namespace Lib_Negocio.Entidades
{
    public class Empleados
    {
        public int      IdEmpleado   { get; set; }
        public int      IdPersona    { get; set; }   // FK
        public string   Cargo        { get; set; } = string.Empty;
        public decimal  SalarioBase  { get; set; }
        public bool     Activo       { get; set; } = true;
        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        // Navegacion
        public Personas? Persona { get; set; }
    }
}
