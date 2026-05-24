using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class FacturasUnitaria
    {
        private IConexion?  iConexion;
        private Facturas?  entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Facturas!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Facturas");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Facturas()
            {
                IdPago = 1,
                IdCliente = 1,
                CodigoFactura = "FAC-UT-" + DateTime.Now.Ticks.ToString(),
                FechaEmision = DateTime.Now,
                Subtotal = 30000,
                Impuestos = 5700,
                Total = 35700
            };

            this.iConexion.Facturas!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdFactura != 0)
                return;
            throw new Exception("No se guardo el registro en Facturas");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.Subtotal = 35000;

            var entry = this.iConexion!.Entry<Facturas>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdFactura != 0)
                return;
            throw new Exception("No se modifico el registro en Facturas");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Facturas!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
