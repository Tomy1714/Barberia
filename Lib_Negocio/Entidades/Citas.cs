using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class Citas
    {
        [Key] public int      IdCita          { get; set; }
        public int      IdCliente       { get; set; }   // FK
        public int      IdBarbero       { get; set; }   // FK
        public int      IdServicio      { get; set; }   // FK
        public DateTime? FechaHoraInicio { get; set; }
        public string?   Estado          { get; set; } //= "Pendiente";
        public string?   Observaciones   { get; set; } //= string.Empty;
        public DateTime? FechaCreacion   { get; set; } = DateTime.Now;

        // Navegacion
   

        [ForeignKey(nameof(IdCliente))]
        public Clientes? Cliente { get; set; }

        [ForeignKey(nameof(IdBarbero))]
        public Barberos? Barbero { get; set; }

        [ForeignKey(nameof(IdServicio))]
        public Servicios? Servicio { get; set; }
    }
}
