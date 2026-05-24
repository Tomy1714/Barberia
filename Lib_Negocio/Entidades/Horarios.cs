using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class Horarios
    {
        [Key] public int IdHorario { get; set; }
        public int IdEmpleado { get; set; }   // FK
        public int HoraEntrada { get; set; }
        public int HoraSalida { get; set; }
        public bool Activo { get; set; } = true;

        // Navegacion


        [ForeignKey(nameof(IdEmpleado))]
        public Empleados? Empleado { get; set; }
        public List<HorarioDias> HorarioDias { get; set; } = new();
    }
}
