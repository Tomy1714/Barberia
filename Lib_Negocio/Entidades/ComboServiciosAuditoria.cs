using System.ComponentModel.DataAnnotations;

namespace Lib_Negocio.Entidades
{
    public class ComboServiciosAuditoria
    {
        [Key] public int      IdAuditoria  { get; set; }
        public int IdComboServicio { get; set; }
        public string?   Accion       { get; set; } //= string.Empty;  // Insertar / Modificar / Borrar
        public DateTime? Fecha        { get; set; } //= DateTime.Now;
    }
}
