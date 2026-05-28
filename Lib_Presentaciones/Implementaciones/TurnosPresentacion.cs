using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class TurnosPresentacion : ITurnosPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Turnos> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Turnos?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Turnos>();

            return JsonConvert.DeserializeObject<List<Turnos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<TurnosAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Turnos/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<TurnosAuditoria>();

            return JsonConvert.DeserializeObject<List<TurnosAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Turnos> Guardar(Turnos entidad, string rol)
        {
            if (entidad.IdTurno != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Turnos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Turnos();

            return JsonConvert.DeserializeObject<Turnos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Turnos Modificar(Turnos entidad, string rol)
        {
            if (entidad.IdTurno == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Turnos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Turnos();

            return JsonConvert.DeserializeObject<Turnos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Turnos Eliminar(Turnos entidad, string rol)
        {
            if (entidad.IdTurno == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Turnos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Turnos();

            return JsonConvert.DeserializeObject<Turnos>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
