using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class UsuariosUnitaria
    {
        private IConexion?  iConexion;
        private Usuarios?  entidad;

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

            var lista = this.iConexion.Usuarios!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Usuarios");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Usuarios()
            {
                IdPersona = 1,
                Email = "ut" + DateTime.Now.Ticks.ToString() + "@test.com",
                Contrasena = "1234",
                Rol = "Cliente",
                Activo = true,
                FechaCreacion = DateTime.Now
            };

            this.iConexion.Usuarios!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdUsuario != 0)
                return;
            throw new Exception("No se guardo el registro en Usuarios");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.Rol = "Administrador";

            var entry = this.iConexion!.Entry<Usuarios>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdUsuario != 0)
                return;
            throw new Exception("No se modifico el registro en Usuarios");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Usuarios!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
