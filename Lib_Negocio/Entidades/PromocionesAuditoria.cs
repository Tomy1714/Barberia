using System.ComponentModel.DataAnnotations;

namespace Lib_Negocio.Entidades
{
    public class PromocionesAuditoria
    {
        [Key] public int      IdAuditoria  { get; set; }
        public int IdPromocion { get; set; }
        public string?   Accion       { get; set; } //= string.Empty;  // Insertar / Modificar / Borrar
        public DateTime? Fecha        { get; set; } //= DateTime.Now;
    }
}
