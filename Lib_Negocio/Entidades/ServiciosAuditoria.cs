using System.ComponentModel.DataAnnotations;

namespace Lib_Negocio.Entidades
{
    public class ServiciosAuditoria
    {
        [Key] public int      IdAuditoria  { get; set; }
        public int IdServicio { get; set; }
        public string?   Accion       { get; set; } //= string.Empty;  // Insertar / Modificar / Borrar
        public DateTime? Fecha        { get; set; } //= DateTime.Now;
    }
}
