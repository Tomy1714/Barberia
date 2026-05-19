namespace Lib_Negocio.Entidades
{
    public class ServiciosCorte
    {
        public int     IdServicioCorte    { get; set; }
        public int     IdServicio         { get; set; }   // FK
        public string  TipoCorte          { get; set; } = string.Empty;
        public bool    IncluyeBarba       { get; set; }
        public int     NivelComplejidad   { get; set; }
        public decimal RecargoComplejidad { get; set; }

        // Navegacion
        public Servicios? Servicio { get; set; }
    }
}
