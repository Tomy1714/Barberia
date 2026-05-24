using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class EmpleadosSedesUnitaria
    {
        private IConexion?  iConexion;
        private EmpleadoSede?  entidad;

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

            var lista = this.iConexion.EmpleadoSede!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en EmpleadosSedes");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new EmpleadoSede()
            {
                IdEmpleado = 1,
                IdSede = 1,
                FechaAsignacion = DateTime.Now
            };

            this.iConexion.EmpleadoSede!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdEmpleadoSede != 0)
                return;
            throw new Exception("No se guardo el registro en EmpleadosSedes");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.IdSede = 1;

            var entry = this.iConexion!.Entry<EmpleadoSede>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdEmpleadoSede != 0)
                return;
            throw new Exception("No se modifico el registro en EmpleadosSedes");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.EmpleadoSede!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
