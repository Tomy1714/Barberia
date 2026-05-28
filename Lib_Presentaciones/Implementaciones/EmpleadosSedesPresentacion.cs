using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class EmpleadosSedesPresentacion : IEmpleadosSedesPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<EmpleadoSede> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/EmpleadosSedes?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<EmpleadoSede>();

            return JsonConvert.DeserializeObject<List<EmpleadoSede>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<EmpleadosSedesAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/EmpleadosSedes/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<EmpleadosSedesAuditoria>();

            return JsonConvert.DeserializeObject<List<EmpleadosSedesAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<EmpleadoSede> Guardar(EmpleadoSede entidad, string rol)
        {
            if (entidad.IdEmpleadoSede != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/EmpleadosSedes?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new EmpleadoSede();

            return JsonConvert.DeserializeObject<EmpleadoSede>(
                respuesta["Valor"].ToString()!)!;
        }

        public EmpleadoSede Modificar(EmpleadoSede entidad, string rol)
        {
            if (entidad.IdEmpleadoSede == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/EmpleadosSedes?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new EmpleadoSede();

            return JsonConvert.DeserializeObject<EmpleadoSede>(
                respuesta["Valor"].ToString()!)!;
        }

        public EmpleadoSede Eliminar(EmpleadoSede entidad, string rol)
        {
            if (entidad.IdEmpleadoSede == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/EmpleadosSedes?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new EmpleadoSede();

            return JsonConvert.DeserializeObject<EmpleadoSede>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
