using System.ComponentModel.DataAnnotations;

namespace Lib_Negocio.Entidades
{
    public class CombosAuditoria
    {
        [Key] public int      IdAuditoria  { get; set; }
        public int IdCombo { get; set; }
        public string?   Accion       { get; set; } //= string.Empty;  // Insertar / Modificar / Borrar
        public DateTime? Fecha        { get; set; } //= DateTime.Now;
    }
}
