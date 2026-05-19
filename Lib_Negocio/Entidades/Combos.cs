namespace Lib_Negocio.Entidades
{
    public class Combos
    {
        public int     IdCombo        { get; set; }
        public int     IdServicio     { get; set; }   // FK
        public decimal DescuentoCombo { get; set; }
        public string  Descripcion    { get; set; } = string.Empty;

        // Navegacion
        public Servicios? Servicio { get; set; }
    }
}
