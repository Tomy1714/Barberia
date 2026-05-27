using System.ComponentModel.DataAnnotations;

namespace Lib_Negocio.Entidades
{
    public class UsuariosAuditoria
    {
        [Key] public int      IdAuditoria  { get; set; }
        public int IdUsuario { get; set; }
        public string?   Accion       { get; set; } //= string.Empty;  // Insertar / Modificar / Borrar
        public DateTime? Fecha        { get; set; } //= DateTime.Now;
    }
}
