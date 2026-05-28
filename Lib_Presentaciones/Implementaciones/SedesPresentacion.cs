using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class SedesPresentacion : ISedesPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Sedes> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Sedes?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Sedes>();

            return JsonConvert.DeserializeObject<List<Sedes>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<SedesAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Sedes/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<SedesAuditoria>();

            return JsonConvert.DeserializeObject<List<SedesAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Sedes> Guardar(Sedes entidad, string rol)
        {
            if (entidad.IdSede != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Sedes?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Sedes();

            return JsonConvert.DeserializeObject<Sedes>(
                respuesta["Valor"].ToString()!)!;
        }

        public Sedes Modificar(Sedes entidad, string rol)
        {
            if (entidad.IdSede == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Sedes?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Sedes();

            return JsonConvert.DeserializeObject<Sedes>(
                respuesta["Valor"].ToString()!)!;
        }

        public Sedes Eliminar(Sedes entidad, string rol)
        {
            if (entidad.IdSede == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Sedes?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Sedes();

            return JsonConvert.DeserializeObject<Sedes>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
