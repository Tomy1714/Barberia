namespace Asp_Presentaciones.Infraestructura
{
    /// <summary>
    /// Proyeccion uniforme de cualquier *Auditoria para mostrarla en una tabla.
    /// </summary>
    public class FilaAuditoria
    {
        public int       IdAuditoria { get; set; }
        public int       IdReferencia { get; set; }
        public string    Accion { get; set; } = "";
        public DateTime? Fecha { get; set; }
    }
}
