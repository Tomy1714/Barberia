namespace Lib_Negocio.Entidades
{
    public class Servicios
    {
        public int     IdServicio      { get; set; }
        public string  Nombre          { get; set; } = string.Empty;
        public string  Descripcion     { get; set; } = string.Empty;
        public decimal PrecioBase      { get; set; }
        public int     DuracionMinutos { get; set; }
        public bool    Activo          { get; set; } = true;
    }
}
