using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class SedesUnitaria
    {
        private IConexion?  iConexion;
        private Sedes?  entidad;

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

            var lista = this.iConexion.Sedes!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Sedes");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Sedes()
            {
                Nombre = "UT-Sede-" + DateTime.Now.Ticks.ToString(),
                Direccion = "Calle 10",
                Ciudad = "Medellin",
                Telefono = "3001234567",
                Correo = "ut@sede.com",
                CapacidadMaxima = 5,
                Activa = true
            };

            this.iConexion.Sedes!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdSede != 0)
                return;
            throw new Exception("No se guardo el registro en Sedes");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.Ciudad = "Bogota";

            var entry = this.iConexion!.Entry<Sedes>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdSede != 0)
                return;
            throw new Exception("No se modifico el registro en Sedes");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Sedes!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
