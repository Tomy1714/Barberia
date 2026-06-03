using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio.Entidades
{
    public class Facturas
    {
        [Key] public int      IdFactura     { get; set; }
        public int      IdPago        { get; set; }   
        public int      IdCliente     { get; set; }   
        public string?   CodigoFactura { get; set; } 
        public DateTime FechaEmision  { get; set; } 
        public decimal  Subtotal      { get; set; }
        public decimal  Impuestos     { get; set; }
        public decimal  Total         { get; set; }

   


        [ForeignKey(nameof(IdPago))]
        public Pagos? Pago { get; set; }

        [ForeignKey(nameof(IdCliente))]
        public Clientes? Cliente { get; set; }
    }
}
