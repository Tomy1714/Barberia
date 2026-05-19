namespace Lib_Negocio.Entidades
{
    public class Clientes
    {
        public int  IdCliente    { get; set; }
        public int  IdPersona    { get; set; }   // FK
        public int  TotalVisitas { get; set; }
        public bool Activo       { get; set; } = true;

        // Navegacion
        public Personas? Persona { get; set; }
    }
}
