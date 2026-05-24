using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class Usuarios
    {
        [Key] public int      IdUsuario     { get; set; }
        public int      IdPersona     { get; set; }   
        public string   Email         { get; set; } = string.Empty;
        public string   Contrasena    { get; set; } = string.Empty;
        public string   Rol           { get; set; } = "Cliente";
        public bool     Activo        { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;




        [ForeignKey(nameof(IdPersona))]
        public Personas? Persona { get; set; }
    }
}
