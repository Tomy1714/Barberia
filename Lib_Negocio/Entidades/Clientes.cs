using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class Clientes
    {
        [Key] public int  IdCliente    { get; set; }
        public int  IdPersona    { get; set; }   // FK


        public int  TotalVisitas { get; set; }
        public bool Activo       { get; set; } = true;

     



        [ForeignKey(nameof(IdPersona))]
        public Personas? Persona { get; set; }
    }
}
