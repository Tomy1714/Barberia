using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class CalificacionesPresentacion : ICalificacionesPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Calificaciones> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Calificaciones?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Calificaciones>();

            return JsonConvert.DeserializeObject<List<Calificaciones>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<CalificacionesAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Calificaciones/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<CalificacionesAuditoria>();

            return JsonConvert.DeserializeObject<List<CalificacionesAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Calificaciones> Guardar(Calificaciones entidad, string rol)
        {
            if (entidad.IdCalificacion != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Calificaciones?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Calificaciones();

            return JsonConvert.DeserializeObject<Calificaciones>(
                respuesta["Valor"].ToString()!)!;
        }

        public Calificaciones Modificar(Calificaciones entidad, string rol)
        {
            if (entidad.IdCalificacion == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Calificaciones?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Calificaciones();

            return JsonConvert.DeserializeObject<Calificaciones>(
                respuesta["Valor"].ToString()!)!;
        }

        public Calificaciones Eliminar(Calificaciones entidad, string rol)
        {
            if (entidad.IdCalificacion == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Calificaciones?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Calificaciones();

            return JsonConvert.DeserializeObject<Calificaciones>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
