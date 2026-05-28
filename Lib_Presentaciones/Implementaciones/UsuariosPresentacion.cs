using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class UsuariosPresentacion : IUsuariosPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Usuarios> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Usuarios?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Usuarios>();

            return JsonConvert.DeserializeObject<List<Usuarios>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<UsuariosAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Usuarios/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<UsuariosAuditoria>();

            return JsonConvert.DeserializeObject<List<UsuariosAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Usuarios> Guardar(Usuarios entidad, string rol)
        {
            if (entidad.IdUsuario != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Usuarios?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Usuarios();

            return JsonConvert.DeserializeObject<Usuarios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Usuarios Modificar(Usuarios entidad, string rol)
        {
            if (entidad.IdUsuario == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Usuarios?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Usuarios();

            return JsonConvert.DeserializeObject<Usuarios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Usuarios Eliminar(Usuarios entidad, string rol)
        {
            if (entidad.IdUsuario == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Usuarios?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Usuarios();

            return JsonConvert.DeserializeObject<Usuarios>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
