using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class Calificaciones
    {
        [Key] public int      IdCalificacion    { get; set; }
        public int      IdCita            { get; set; }   // FK
        public int      IdBarbero         { get; set; }   // FK
        public int      Puntaje           { get; set; }
        public string   Comentario        { get; set; } = string.Empty;
        public DateTime FechaCalificacion { get; set; } = DateTime.Now;

   

        [ForeignKey(nameof(IdCita))]
        public Citas? Cita { get; set; }

        [ForeignKey(nameof(IdBarbero))]
        public Barberos? Barbero { get; set; }
    }
}
