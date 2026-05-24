using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class Recepcionistas
    {
        [Key] public int     IdRecepcionista     { get; set; }
        public int     IdEmpleado          { get; set; }   // FK
        public int     IdSede              { get; set; }   // FK
        public string  TurnoAsignado       { get; set; } = string.Empty;
        public int     CitasGestionadasMes { get; set; }
        public decimal BonoPorMeta         { get; set; }

        // Navegacion


        [ForeignKey(nameof(IdEmpleado))]
        public Empleados? Empleado { get; set; }

        [ForeignKey(nameof(IdSede))]
        public Sedes? Sede { get; set; }
    }
}
