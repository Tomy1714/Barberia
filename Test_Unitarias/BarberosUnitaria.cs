using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class BarberosUnitaria
    {
        private IConexion?  iConexion;
        private Barberos?  entidad;

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

            var lista = this.iConexion.Barberos!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Barberos");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Barberos()
            {
                IdEmpleado = 1,
                Especialidad = "Degradados",
                PorcentajeComision = 15,
                ServiciosMes = 0,
                ValorPromServicio = 30000,
                Activo = true
            };

            this.iConexion.Barberos!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdBarbero != 0)
                return;
            throw new Exception("No se guardo el registro en Barberos");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.Especialidad = "Clasicos";

            var entry = this.iConexion!.Entry<Barberos>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdBarbero != 0)
                return;
            throw new Exception("No se modifico el registro en Barberos");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Barberos!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
