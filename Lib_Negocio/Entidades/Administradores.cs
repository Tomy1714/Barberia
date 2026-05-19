namespace Lib_Negocio.Entidades
{
    public class Administradores
    {
        public int       IdAdministrador      { get; set; }
        public int       IdEmpleado           { get; set; }   // FK
        public string    NombreNegocio        { get; set; } = string.Empty;
        public decimal   PorcentajeUtilidades { get; set; }
        public DateTime? FechaFundacion       { get; set; }
        public decimal   UtilidadesUltimoMes  { get; set; }

        // Navegacion
        public Empleados? Empleado { get; set; }
    }
}
