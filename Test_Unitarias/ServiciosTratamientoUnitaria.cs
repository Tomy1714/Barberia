using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class ServiciosTratamientoUnitaria
    {
        private IConexion?  iConexion;
        private ServiciosTratamiento?  entidad;

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

            var lista = this.iConexion.ServiciosTratamiento!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en ServiciosTratamiento");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new ServiciosTratamiento()
            {
                IdServicio = 1,
                TipoTratamiento = "Hidratacion",
                CostoProducto = 15000,
                SesionesRequeridas = 3
            };

            this.iConexion.ServiciosTratamiento!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdServicioTratamiento != 0)
                return;
            throw new Exception("No se guardo el registro en ServiciosTratamiento");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.TipoTratamiento = "Keratina";

            var entry = this.iConexion!.Entry<ServiciosTratamiento>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdServicioTratamiento != 0)
                return;
            throw new Exception("No se modifico el registro en ServiciosTratamiento");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.ServiciosTratamiento!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
