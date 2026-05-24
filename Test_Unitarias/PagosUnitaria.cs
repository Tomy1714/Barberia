using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class PagosUnitaria
    {
        private IConexion?  iConexion;
        private Pagos?  entidad;

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

            var lista = this.iConexion.Pagos!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Pagos");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Pagos()
            {
                IdCita = 1,
                Monto = 30000,
                Descuento = 0,
                Total = 30000,
                NombreMetodo = "Tarjeta",
                EstadoPago = "Pendiente",
                FechaPago = DateTime.Now
            };

            this.iConexion.Pagos!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdPago != 0)
                return;
            throw new Exception("No se guardo el registro en Pagos");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.EstadoPago = "Aprobado";

            var entry = this.iConexion!.Entry<Pagos>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdPago != 0)
                return;
            throw new Exception("No se modifico el registro en Pagos");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Pagos!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
