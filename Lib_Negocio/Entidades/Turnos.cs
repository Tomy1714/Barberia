using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class Turnos
    {
        [Key] public int      IdTurno    { get; set; }
        public int      IdBarbero  { get; set; }   // FK
        public int      IdSede     { get; set; }   // FK
        public DateTime FechaTurno { get; set; }
        public int      HoraInicio { get; set; }
        public int      HoraFin    { get; set; }
        public string   Estado     { get; set; } = "Programado";

        // Navegacion


        [ForeignKey(nameof(IdBarbero))]
        public Barberos? Barbero { get; set; }

        [ForeignKey(nameof(IdSede))]
        public Sedes? Sede { get; set; }
    }
}
