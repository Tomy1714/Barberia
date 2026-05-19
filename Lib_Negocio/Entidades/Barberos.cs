namespace Lib_Negocio.Entidades
{
    public class Barberos
    {
        public int     IdBarbero          { get; set; }
        public int     IdEmpleado         { get; set; }   // FK
        public string  Especialidad       { get; set; } = string.Empty;
        public decimal PorcentajeComision { get; set; }
        public int     ServiciosMes       { get; set; }
        public decimal ValorPromServicio  { get; set; }
        public bool    Activo             { get; set; } = true;

        // Navegacion
        public Empleados? Empleado { get; set; }
    }
}
