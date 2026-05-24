using System.ComponentModel.DataAnnotations;

namespace Lib_Negocio.Entidades
{
    public class Personas
    {
        [Key] public int       IdPersona       { get; set; }
        public string?    Identificacion  { get; set; } //= string.Empty;
        public string?    Nombres         { get; set; } //= string.Empty;
        public string?    Apellidos       { get; set; } //= string.Empty;
        public string?    Telefono        { get; set; } //= string.Empty;
        public string?    Correo          { get; set; } //= string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public string?    Direccion       { get; set; } //= string.Empty;
        public DateTime?  FechaRegistro   { get; set; } = DateTime.Now;
    }
}
