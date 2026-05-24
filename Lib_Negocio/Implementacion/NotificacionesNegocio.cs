using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class NotificacionesNegocio : INotificacionesNegocio
    {
        private Conexion? IConexion = null;

        public NotificacionesNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Notificaciones> Listar()
        {
            return this.IConexion!.Notificaciones
                .Include(e => e.Cliente)
                .Include(e => e.Cita)
                .Take(50).ToList();
        }

        public Notificaciones? Guardar(Notificaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdNotificacion != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Notificaciones.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Notificaciones? Modificar(Notificaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdNotificacion == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Notificaciones.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Notificaciones? Borrar(Notificaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdNotificacion == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Notificaciones.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public bool Enviar(Notificaciones entidad)
        {
            entidad.Estado    = "Enviada";
            entidad.FechaEnvio = DateTime.Now;
            this.IConexion!.SaveChanges();
            return true;
        }
    }
}
