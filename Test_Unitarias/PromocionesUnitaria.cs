using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class PromocionesUnitaria
    {
        private IConexion?  iConexion;
        private Promociones?  entidad;

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

            var lista = this.iConexion.Promociones!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Promociones");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Promociones()
            {
                IdServicio = 1,
                Nombre = "UT-Promo-" + DateTime.Now.Ticks.ToString(),
                Descripcion = "Descuento de prueba",
                PorcentajeDescuento = 10,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(30),
                Activa = true
            };

            this.iConexion.Promociones!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdPromocion != 0)
                return;
            throw new Exception("No se guardo el registro en Promociones");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.PorcentajeDescuento = 20;

            var entry = this.iConexion!.Entry<Promociones>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdPromocion != 0)
                return;
            throw new Exception("No se modifico el registro en Promociones");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Promociones!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
