using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class PersonasUnitaria
    {
        private IConexion?  iConexion;
        private Personas?  entidad;

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

            var lista = this.iConexion.Personas!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Personas");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Personas()
            {
                Identificacion = "10325873475",
                Nombres = "Juan",
                Apellidos = "Perez",
                Telefono = "3001234567",
                Correo = "ut" + DateTime.Now.Ticks.ToString() + "@mail.com",
                Direccion = "Calle 10",
                FechaRegistro = DateTime.Now
            };

            this.iConexion.Personas!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdPersona != 0)
                return;
            throw new Exception("No se guardo el registro en Personas");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.Nombres = "Carlos";

            var entry = this.iConexion!.Entry<Personas>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdPersona != 0)
                return;
            throw new Exception("No se modifico el registro en Personas");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Personas!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
