using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class ServiciosTratamientoPresentacion : IServiciosTratamientoPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<ServiciosTratamiento> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/ServiciosTratamiento?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<ServiciosTratamiento>();

            return JsonConvert.DeserializeObject<List<ServiciosTratamiento>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<ServiciosTratamientoAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/ServiciosTratamiento/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<ServiciosTratamientoAuditoria>();

            return JsonConvert.DeserializeObject<List<ServiciosTratamientoAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<ServiciosTratamiento> Guardar(ServiciosTratamiento entidad, string rol)
        {
            if (entidad.IdServicioTratamiento != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/ServiciosTratamiento?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new ServiciosTratamiento();

            return JsonConvert.DeserializeObject<ServiciosTratamiento>(
                respuesta["Valor"].ToString()!)!;
        }

        public ServiciosTratamiento Modificar(ServiciosTratamiento entidad, string rol)
        {
            if (entidad.IdServicioTratamiento == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/ServiciosTratamiento?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new ServiciosTratamiento();

            return JsonConvert.DeserializeObject<ServiciosTratamiento>(
                respuesta["Valor"].ToString()!)!;
        }

        public ServiciosTratamiento Eliminar(ServiciosTratamiento entidad, string rol)
        {
            if (entidad.IdServicioTratamiento == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/ServiciosTratamiento?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new ServiciosTratamiento();

            return JsonConvert.DeserializeObject<ServiciosTratamiento>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
