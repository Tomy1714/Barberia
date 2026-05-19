namespace Lib_Negocio.Entidades
{
    public class ServiciosTratamiento
    {
        public int     IdServicioTratamiento { get; set; }
        public int     IdServicio            { get; set; }   // FK
        public string  TipoTratamiento       { get; set; } = string.Empty;
        public decimal CostoProducto         { get; set; }
        public int     SesionesRequeridas    { get; set; }

        // Navegacion
        public Servicios? Servicio { get; set; }
    }
}
