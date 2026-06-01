namespace Asp_Presentaciones.Infraestructura
{

    public class FilaAuditoria
    {
        public int       IdAuditoria { get; set; }
        public int       IdReferencia { get; set; }
        public string    Accion { get; set; } = "";
        public DateTime? Fecha { get; set; }
    }
}
