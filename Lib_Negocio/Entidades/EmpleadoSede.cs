using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class EmpleadoSede
    {
        [Key] public int      IdEmpleadoSede  { get; set; }
        public int      IdEmpleado      { get; set; }   // FK
        public int      IdSede          { get; set; }   // FK
        public DateTime FechaAsignacion { get; set; } = DateTime.Now;

        // Navegacion


        [ForeignKey(nameof(IdEmpleado))]
        public Empleados? Empleado { get; set; }

        [ForeignKey(nameof(IdSede))]
        public Sedes? Sede { get; set; }
    }
}
