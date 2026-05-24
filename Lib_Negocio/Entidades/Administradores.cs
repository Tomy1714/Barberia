using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class Administradores
    {
         [Key]  public int       IdAdministrador      { get; set; }
        public int       IdEmpleado           { get; set; }   
        public string    NombreNegocio        { get; set; } = string.Empty;
        public decimal   PorcentajeUtilidades { get; set; }
        public DateTime? FechaFundacion       { get; set; }
        public decimal   UtilidadesUltimoMes  { get; set; }

      

        [ForeignKey(nameof(IdEmpleado))]
        public Empleados? Empleado { get; set; }
    }
}
