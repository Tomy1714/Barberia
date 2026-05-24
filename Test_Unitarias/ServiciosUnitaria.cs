using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class ServiciosUnitaria
    {
        private IConexion?  iConexion;
        private Servicios?  entidad;

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

            var lista = this.iConexion.Servicios!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Servicios");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Servicios()
            {
                Nombre = "UT-Servicio-" + DateTime.Now.Ticks.ToString(),
                Descripcion = "Corte de prueba",
                PrecioBase = 25000,
                DuracionMinutos = 40,
                Activo = true
            };

            this.iConexion.Servicios!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdServicio != 0)
                return;
            throw new Exception("No se guardo el registro en Servicios");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.PrecioBase = 30000;

            var entry = this.iConexion!.Entry<Servicios>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdServicio != 0)
                return;
            throw new Exception("No se modifico el registro en Servicios");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Servicios!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
