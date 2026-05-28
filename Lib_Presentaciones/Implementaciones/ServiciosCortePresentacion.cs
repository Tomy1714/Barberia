using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class ServiciosCortePresentacion : IServiciosCortePresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<ServiciosCorte> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/ServiciosCorte?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<ServiciosCorte>();

            return JsonConvert.DeserializeObject<List<ServiciosCorte>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<ServiciosCorteAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/ServiciosCorte/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<ServiciosCorteAuditoria>();

            return JsonConvert.DeserializeObject<List<ServiciosCorteAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<ServiciosCorte> Guardar(ServiciosCorte entidad, string rol)
        {
            if (entidad.IdServicioCorte != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/ServiciosCorte?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new ServiciosCorte();

            return JsonConvert.DeserializeObject<ServiciosCorte>(
                respuesta["Valor"].ToString()!)!;
        }

        public ServiciosCorte Modificar(ServiciosCorte entidad, string rol)
        {
            if (entidad.IdServicioCorte == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/ServiciosCorte?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new ServiciosCorte();

            return JsonConvert.DeserializeObject<ServiciosCorte>(
                respuesta["Valor"].ToString()!)!;
        }

        public ServiciosCorte Eliminar(ServiciosCorte entidad, string rol)
        {
            if (entidad.IdServicioCorte == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/ServiciosCorte?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new ServiciosCorte();

            return JsonConvert.DeserializeObject<ServiciosCorte>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
