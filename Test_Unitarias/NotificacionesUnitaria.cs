using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class NotificacionesUnitaria
    {
        private IConexion?  iConexion;
        private Notificaciones?  entidad;

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

            var lista = this.iConexion.Notificaciones!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Notificaciones");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Notificaciones()
            {
                IdCliente = 1,
                IdCita = 1,
                Tipo = "Confirmacion",
                Canal = "Correo",
                Mensaje = "Cita confirmada",
                Estado = "Pendiente",
                FechaCreacion = DateTime.Now
            };

            this.iConexion.Notificaciones!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdNotificacion != 0)
                return;
            throw new Exception("No se guardo el registro en Notificaciones");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.Estado = "Enviada";

            var entry = this.iConexion!.Entry<Notificaciones>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdNotificacion != 0)
                return;
            throw new Exception("No se modifico el registro en Notificaciones");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Notificaciones!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
