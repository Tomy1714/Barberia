using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class ServiciosCorteUnitaria
    {
        private IConexion?  iConexion;
        private ServiciosCorte?  entidad;

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

            var lista = this.iConexion.ServiciosCorte!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en ServiciosCorte");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new ServiciosCorte()
            {
                IdServicio = 1,
                TipoCorte = "Clasico",
                IncluyeBarba = false,
                NivelComplejidad = 1,
                RecargoComplejidad = 5000
            };

            this.iConexion.ServiciosCorte!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdServicioCorte != 0)
                return;
            throw new Exception("No se guardo el registro en ServiciosCorte");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.TipoCorte = "Degradado";

            var entry = this.iConexion!.Entry<ServiciosCorte>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdServicioCorte != 0)
                return;
            throw new Exception("No se modifico el registro en ServiciosCorte");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.ServiciosCorte!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
