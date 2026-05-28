using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class HorariosPresentacion : IHorariosPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Horarios> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Horarios?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Horarios>();

            return JsonConvert.DeserializeObject<List<Horarios>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<HorariosAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Horarios/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<HorariosAuditoria>();

            return JsonConvert.DeserializeObject<List<HorariosAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Horarios> Guardar(Horarios entidad, string rol)
        {
            if (entidad.IdHorario != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Horarios?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Horarios();

            return JsonConvert.DeserializeObject<Horarios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Horarios Modificar(Horarios entidad, string rol)
        {
            if (entidad.IdHorario == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Horarios?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Horarios();

            return JsonConvert.DeserializeObject<Horarios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Horarios Eliminar(Horarios entidad, string rol)
        {
            if (entidad.IdHorario == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Horarios?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Horarios();

            return JsonConvert.DeserializeObject<Horarios>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
