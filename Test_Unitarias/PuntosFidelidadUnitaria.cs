using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class PuntosFidelidadUnitaria
    {
        private IConexion?  iConexion;
        private PuntosFidelidad?  entidad;

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

            var lista = this.iConexion.PuntosFidelidad!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en PuntosFidelidad");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new PuntosFidelidad()
            {
                IdCliente = 5,
                PuntosAcumulados = 0,
                FactorConversion = 100,
                FechaActualizacion = DateTime.Now
            };

            this.iConexion.PuntosFidelidad!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdPuntos != 0)
                return;
            throw new Exception("No se guardo el registro en PuntosFidelidad");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.PuntosAcumulados = 50;

            var entry = this.iConexion!.Entry<PuntosFidelidad>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdPuntos != 0)
                return;
            throw new Exception("No se modifico el registro en PuntosFidelidad");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.PuntosFidelidad!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
