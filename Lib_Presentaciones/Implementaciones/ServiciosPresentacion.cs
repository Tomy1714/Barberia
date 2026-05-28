using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class ServiciosPresentacion : IServiciosPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Servicios> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Servicios?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Servicios>();

            return JsonConvert.DeserializeObject<List<Servicios>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<ServiciosAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Servicios/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<ServiciosAuditoria>();

            return JsonConvert.DeserializeObject<List<ServiciosAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Servicios> Guardar(Servicios entidad, string rol)
        {
            if (entidad.IdServicio != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Servicios?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Servicios();

            return JsonConvert.DeserializeObject<Servicios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Servicios Modificar(Servicios entidad, string rol)
        {
            if (entidad.IdServicio == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Servicios?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Servicios();

            return JsonConvert.DeserializeObject<Servicios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Servicios Eliminar(Servicios entidad, string rol)
        {
            if (entidad.IdServicio == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Servicios?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Servicios();

            return JsonConvert.DeserializeObject<Servicios>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
