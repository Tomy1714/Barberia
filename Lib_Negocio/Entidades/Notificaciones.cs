using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class Notificaciones
    {
        [Key] public int       IdNotificacion { get; set; }
        public int       IdCliente      { get; set; }   // FK
        public int       IdCita         { get; set; }   // FK
        public string    Tipo           { get; set; } = string.Empty;
        public string    Canal          { get; set; } = string.Empty;
        public string    Mensaje        { get; set; } = string.Empty;
        public string    Estado         { get; set; } = "Pendiente";
        public DateTime? FechaEnvio     { get; set; }
        public DateTime  FechaCreacion  { get; set; } = DateTime.Now;

        // Navegacion


        [ForeignKey(nameof(IdCita))]
        public Citas? Cita { get; set; }

        [ForeignKey(nameof(IdCliente))]
        public Clientes? Cliente { get; set; }
    }
}
