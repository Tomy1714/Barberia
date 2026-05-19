namespace Lib_Negocio.Entidades
{
    public class Facturas
    {
        public int      IdFactura     { get; set; }
        public int      IdPago        { get; set; }   // FK
        public int      IdCliente     { get; set; }   // FK
        public string   CodigoFactura { get; set; } = string.Empty;
        public DateTime FechaEmision  { get; set; } = DateTime.Now;
        public decimal  Subtotal      { get; set; }
        public decimal  Impuestos     { get; set; }
        public decimal  Total         { get; set; }

        // Navegacion
        public Pagos?    Pago    { get; set; }
        public Clientes? Cliente { get; set; }
    }
}
