using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class HorariosDiasPresentacion : IHorariosDiasPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<HorarioDias> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/HorariosDias?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<HorarioDias>();

            return JsonConvert.DeserializeObject<List<HorarioDias>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<HorariosDiasAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/HorariosDias/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<HorariosDiasAuditoria>();

            return JsonConvert.DeserializeObject<List<HorariosDiasAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<HorarioDias> Guardar(HorarioDias entidad, string rol)
        {
            if (entidad.IdHorarioDia != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/HorariosDias?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new HorarioDias();

            return JsonConvert.DeserializeObject<HorarioDias>(
                respuesta["Valor"].ToString()!)!;
        }

        public HorarioDias Modificar(HorarioDias entidad, string rol)
        {
            if (entidad.IdHorarioDia == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/HorariosDias?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new HorarioDias();

            return JsonConvert.DeserializeObject<HorarioDias>(
                respuesta["Valor"].ToString()!)!;
        }

        public HorarioDias Eliminar(HorarioDias entidad, string rol)
        {
            if (entidad.IdHorarioDia == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/HorariosDias?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new HorarioDias();

            return JsonConvert.DeserializeObject<HorarioDias>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
