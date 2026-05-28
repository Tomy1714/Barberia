using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class PersonasPresentacion : IPersonasPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Personas> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Personas?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Personas>();

            return JsonConvert.DeserializeObject<List<Personas>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<PersonasAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Personas/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<PersonasAuditoria>();

            return JsonConvert.DeserializeObject<List<PersonasAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Personas> Guardar(Personas entidad, string rol)
        {
            if (entidad.IdPersona != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Personas?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Personas();

            return JsonConvert.DeserializeObject<Personas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Personas Modificar(Personas entidad, string rol)
        {
            if (entidad.IdPersona == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Personas?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Personas();

            return JsonConvert.DeserializeObject<Personas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Personas Eliminar(Personas entidad, string rol)
        {
            if (entidad.IdPersona == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Personas?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Personas();

            return JsonConvert.DeserializeObject<Personas>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
