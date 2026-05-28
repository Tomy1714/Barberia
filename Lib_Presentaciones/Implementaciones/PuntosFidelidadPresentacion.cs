using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class PuntosFidelidadPresentacion : IPuntosFidelidadPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<PuntosFidelidad> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/PuntosFidelidad?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<PuntosFidelidad>();

            return JsonConvert.DeserializeObject<List<PuntosFidelidad>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<PuntosFidelidadAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/PuntosFidelidad/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<PuntosFidelidadAuditoria>();

            return JsonConvert.DeserializeObject<List<PuntosFidelidadAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<PuntosFidelidad> Guardar(PuntosFidelidad entidad, string rol)
        {
            if (entidad.IdPuntos != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/PuntosFidelidad?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new PuntosFidelidad();

            return JsonConvert.DeserializeObject<PuntosFidelidad>(
                respuesta["Valor"].ToString()!)!;
        }

        public PuntosFidelidad Modificar(PuntosFidelidad entidad, string rol)
        {
            if (entidad.IdPuntos == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/PuntosFidelidad?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new PuntosFidelidad();

            return JsonConvert.DeserializeObject<PuntosFidelidad>(
                respuesta["Valor"].ToString()!)!;
        }

        public PuntosFidelidad Eliminar(PuntosFidelidad entidad, string rol)
        {
            if (entidad.IdPuntos == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/PuntosFidelidad?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new PuntosFidelidad();

            return JsonConvert.DeserializeObject<PuntosFidelidad>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
