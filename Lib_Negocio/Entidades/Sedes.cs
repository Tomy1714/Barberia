namespace Lib_Negocio.Entidades
{
    public class Sedes
    {
        public int       IdSede          { get; set; }
        public string    Nombre          { get; set; } = string.Empty;
        public string    Direccion       { get; set; } = string.Empty;
        public string    Ciudad          { get; set; } = string.Empty;
        public string    Telefono        { get; set; } = string.Empty;
        public string    Correo          { get; set; } = string.Empty;
        public int       CapacidadMaxima { get; set; }
        public bool      Activa          { get; set; } = true;
        public DateTime? FechaApertura   { get; set; }
    }
}
