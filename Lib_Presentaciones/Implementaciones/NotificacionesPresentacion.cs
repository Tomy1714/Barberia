using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class NotificacionesPresentacion : INotificacionesPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Notificaciones> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Notificaciones?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Notificaciones>();

            return JsonConvert.DeserializeObject<List<Notificaciones>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<NotificacionesAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Notificaciones/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<NotificacionesAuditoria>();

            return JsonConvert.DeserializeObject<List<NotificacionesAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Notificaciones> Guardar(Notificaciones entidad, string rol)
        {
            if (entidad.IdNotificacion != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Notificaciones?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Notificaciones();

            return JsonConvert.DeserializeObject<Notificaciones>(
                respuesta["Valor"].ToString()!)!;
        }

        public Notificaciones Modificar(Notificaciones entidad, string rol)
        {
            if (entidad.IdNotificacion == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Notificaciones?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Notificaciones();

            return JsonConvert.DeserializeObject<Notificaciones>(
                respuesta["Valor"].ToString()!)!;
        }

        public Notificaciones Eliminar(Notificaciones entidad, string rol)
        {
            if (entidad.IdNotificacion == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Notificaciones?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Notificaciones();

            return JsonConvert.DeserializeObject<Notificaciones>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
