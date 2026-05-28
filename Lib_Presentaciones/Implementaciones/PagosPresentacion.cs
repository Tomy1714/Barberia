using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class PagosPresentacion : IPagosPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Pagos> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Pagos?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Pagos>();

            return JsonConvert.DeserializeObject<List<Pagos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<PagosAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Pagos/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<PagosAuditoria>();

            return JsonConvert.DeserializeObject<List<PagosAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Pagos> Guardar(Pagos entidad, string rol)
        {
            if (entidad.IdPago != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Pagos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Pagos();

            return JsonConvert.DeserializeObject<Pagos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Pagos Modificar(Pagos entidad, string rol)
        {
            if (entidad.IdPago == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Pagos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Pagos();

            return JsonConvert.DeserializeObject<Pagos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Pagos Eliminar(Pagos entidad, string rol)
        {
            if (entidad.IdPago == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Pagos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Pagos();

            return JsonConvert.DeserializeObject<Pagos>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
