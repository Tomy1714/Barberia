namespace Lib_Negocio.Entidades
{
    public class ComboServicios
    {
        public int IdComboServicio { get; set; }
        public int IdCombo         { get; set; }   // FK
        public int IdServicio      { get; set; }   // FK

        // Navegacion
        public Combos?    Combo    { get; set; }
        public Servicios? Servicio { get; set; }
    }
}
