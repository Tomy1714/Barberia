using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class HorarioDias
    {
        [Key] public int    IdHorarioDia { get; set; }
        public int    IdHorario    { get; set; }   
        public string Dia          { get; set; } = string.Empty;

       


        [ForeignKey(nameof(IdHorario))]
        public Horarios? Horario { get; set; }
    }
}
