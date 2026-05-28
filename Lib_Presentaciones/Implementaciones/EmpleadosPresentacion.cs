using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class EmpleadosPresentacion : IEmpleadosPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Empleados> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Empleados?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Empleados>();

            return JsonConvert.DeserializeObject<List<Empleados>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<EmpleadosAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Empleados/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<EmpleadosAuditoria>();

            return JsonConvert.DeserializeObject<List<EmpleadosAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Empleados> Guardar(Empleados entidad, string rol)
        {
            if (entidad.IdEmpleado != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Empleados?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Empleados();

            return JsonConvert.DeserializeObject<Empleados>(
                respuesta["Valor"].ToString()!)!;
        }

        public Empleados Modificar(Empleados entidad, string rol)
        {
            if (entidad.IdEmpleado == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Empleados?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Empleados();

            return JsonConvert.DeserializeObject<Empleados>(
                respuesta["Valor"].ToString()!)!;
        }

        public Empleados Eliminar(Empleados entidad, string rol)
        {
            if (entidad.IdEmpleado == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Empleados?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Empleados();

            return JsonConvert.DeserializeObject<Empleados>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
