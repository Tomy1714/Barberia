using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class ComboServiciosPresentacion : IComboServiciosPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<ComboServicios> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/ComboServicios?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<ComboServicios>();

            return JsonConvert.DeserializeObject<List<ComboServicios>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<ComboServiciosAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/ComboServicios/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<ComboServiciosAuditoria>();

            return JsonConvert.DeserializeObject<List<ComboServiciosAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<ComboServicios> Guardar(ComboServicios entidad, string rol)
        {
            if (entidad.IdComboServicio != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/ComboServicios?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new ComboServicios();

            return JsonConvert.DeserializeObject<ComboServicios>(
                respuesta["Valor"].ToString()!)!;
        }

        public ComboServicios Modificar(ComboServicios entidad, string rol)
        {
            if (entidad.IdComboServicio == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/ComboServicios?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new ComboServicios();

            return JsonConvert.DeserializeObject<ComboServicios>(
                respuesta["Valor"].ToString()!)!;
        }

        public ComboServicios Eliminar(ComboServicios entidad, string rol)
        {
            if (entidad.IdComboServicio == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/ComboServicios?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new ComboServicios();

            return JsonConvert.DeserializeObject<ComboServicios>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
